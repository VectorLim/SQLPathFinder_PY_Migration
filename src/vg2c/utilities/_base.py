from __future__ import annotations

import ast
import inspect
import re
from abc import ABC, abstractmethod
from typing import TYPE_CHECKING, Any, ClassVar

from vg2c.emitter.models import (
    StepEmission,
    UtilityOperationDefinition,
    build_step_emission,
    emittable,
)
from vg2c.kind import Kind

if TYPE_CHECKING:
    from vg2c.frontend.models import BlockOptions


__all__ = ["EmitterUtility", "UtilitySpec"]


_CLASS_SIG_RE = re.compile(r"^(\s*class\s+\w+)\(.*\):\s*$")
_EMBED_ONLY_ASSIGNMENTS = {"handles", "check_priority"}
_EMBED_ONLY_DECORATORS = {"emittable", "operation_spec"}


def _find_class_def(source: str, class_name: str) -> ast.ClassDef | None:
    tree = ast.parse(source)
    for node in ast.walk(tree):
        if isinstance(node, ast.ClassDef) and node.name == class_name:
            return node
    return None


def _decorator_name(node: ast.expr) -> str | None:
    if isinstance(node, ast.Call):
        return _decorator_name(node.func)
    if isinstance(node, ast.Name):
        return node.id
    if isinstance(node, ast.Attribute):
        return node.attr
    return None


def _assignment_names(node: ast.stmt) -> set[str]:
    if isinstance(node, ast.Assign):
        return {target.id for target in node.targets if isinstance(target, ast.Name)}
    if isinstance(node, ast.AnnAssign) and isinstance(node.target, ast.Name):
        return {node.target.id}
    return set()


def _embed_only_lines(source: str, class_name: str) -> set[int]:
    """Return 0-based lines containing compiler-only class metadata."""
    node = _find_class_def(source, class_name)
    if node is None:
        return set()

    remove: set[int] = set()
    for child in node.body:
        if _assignment_names(child) & _EMBED_ONLY_ASSIGNMENTS:
            end = child.end_lineno or child.lineno
            remove.update(range(child.lineno - 1, end))
        if isinstance(child, (ast.FunctionDef, ast.AsyncFunctionDef)):
            for decorator in child.decorator_list:
                if _decorator_name(decorator) not in _EMBED_ONLY_DECORATORS:
                    continue
                end = decorator.end_lineno or decorator.lineno
                remove.update(range(decorator.lineno - 1, end))
    return remove


def _strip_embed_artifacts(source: str, class_name: str) -> str:
    lines = source.split("\n")
    remove = _embed_only_lines(source, class_name)
    lines = [line for index, line in enumerate(lines) if index not in remove]

    while lines and lines[0].lstrip().startswith("@"):
        lines.pop(0)

    if not lines:
        return ""

    lines[0] = _CLASS_SIG_RE.sub(r"\1:", lines[0])
    lines[0] = lines[0].replace(f"({EmitterUtility.__name__}):", ":")
    lines[0] = lines[0].replace(f"({UtilitySpec.__name__}):", ":")
    lines[0] = lines[0].replace(f"({class_name}, {UtilitySpec.__name__}):", f"({class_name}):")

    return "\n".join(lines).rstrip()


class UtilitySpec(ABC):
    """Base contract for all embeddable utilities and their semantic operations."""

    utility_name: ClassVar[str]
    handles: ClassVar[tuple[Kind, ...]] = ()
    always_include: ClassVar[bool] = False
    _registry: ClassVar[dict[str, type[UtilitySpec]]] = {}
    _emit_handlers: ClassVar[dict[Kind, type[UtilitySpec]]] = {}

    def __init_subclass__(cls, **kwargs: Any) -> None:
        super().__init_subclass__(**kwargs)

        raw_name = cls.__dict__.get("utility_name")
        if not isinstance(raw_name, str):
            return

        name = raw_name.strip()
        if not name:
            raise ValueError(f"{cls.__name__}: utility_name must be non-empty")

        existing = UtilitySpec._registry.get(name)
        if existing is not None and existing is not cls:
            raise ValueError(f"duplicate utility_name: {name}")

        UtilitySpec._registry[name] = cls

        for handled_kind in tuple(getattr(cls, "handles", ())):
            owner = UtilitySpec._emit_handlers.get(handled_kind)
            if owner is not None and owner is not cls:
                raise ValueError(
                    f"duplicate handler for {handled_kind}: {owner.__name__} and {cls.__name__}"
                )
            UtilitySpec._emit_handlers[handled_kind] = cls

    @classmethod
    def get_source(cls, source_override: str | None = None) -> str:
        """Return this utility's embeddable source."""
        custom = getattr(cls, "__vg2c_source__", None)
        if custom is not None:
            return str(custom).rstrip()

        if source_override is not None:
            node = _find_class_def(source_override, cls.__name__)
            if node is not None:
                segment = ast.get_source_segment(source_override, node)
                if segment is not None:
                    return _strip_embed_artifacts(segment, cls.__name__)

        source = inspect.getsource(cls)
        return _strip_embed_artifacts(source, cls.__name__)

    @classmethod
    def registered(cls) -> tuple[type[UtilitySpec], ...]:
        """Return loaded utilities in deterministic registration order."""
        return tuple(cls._registry.values())

    @classmethod
    def for_name(cls, name: str) -> type[UtilitySpec] | None:
        return cls._registry.get(name)

    @classmethod
    def for_kind(cls, kind: Kind) -> type[UtilitySpec] | None:
        return cls._emit_handlers.get(kind) or cls._emit_handlers.get(Kind.UNKNOWN)

    @classmethod
    def operation_definitions(cls) -> tuple[UtilityOperationDefinition, ...]:
        """Enumerate @emittable operations directly from the registered utilities."""
        definitions: list[UtilityOperationDefinition] = []
        for utility in cls.registered():
            for name in utility.__dict__:
                raw = inspect.getattr_static(utility, name, None)
                if isinstance(raw, emittable):
                    definitions.append(raw.definition(utility))
        return tuple(definitions)

    @classmethod
    def operation_definition(
        cls, utility_name: str, method_name: str
    ) -> UtilityOperationDefinition | None:
        utility = cls.for_name(utility_name)
        if utility is None:
            return None
        raw = inspect.getattr_static(utility, method_name, None)
        return raw.definition(utility) if isinstance(raw, emittable) else None

    @staticmethod
    def emit_block(block: Any) -> list[str] | tuple[str, list[str]] | None:
        return None

    @staticmethod
    def _step_name(block: Any, suffix: str) -> str:
        return f"step_{block.index:04d}_{suffix}"

    @classmethod
    def _wrap_in_step(
        cls, subclass: type[UtilitySpec], block: Any, result: Any
    ) -> StepEmission | None:
        if result is None:
            return None
        if isinstance(result, tuple) and len(result) == 2 and isinstance(result[1], list):
            suffix, body_lines = result
        else:
            suffix = getattr(subclass, "utility_name", "utility")
            body_lines = result
        function_name = cls._step_name(block, suffix)
        return build_step_emission(
            function_name=function_name,
            block_index=block.index,
            functional_kind=block.kind.value,
            body_lines=body_lines,
        )

    @classmethod
    def dispatch_and_emit(cls, block: Any) -> StepEmission:
        handler_cls = cls._emit_handlers.get(block.kind)
        if handler_cls is not None:
            emitted = handler_cls.emit_block(block)
            if emitted is not None:
                wrapped = cls._wrap_in_step(handler_cls, block, emitted)
                if wrapped is not None:
                    return wrapped
        return build_step_emission(
            function_name=cls._step_name(block, "unsupported"),
            block_index=block.index,
            functional_kind=block.kind.value,
            body_lines=[],
        )


class EmitterUtility(UtilitySpec):
    """Utility that participates in Stage 1 classification and block emission."""

    check_priority: ClassVar[int] = 0
    _check_handlers: ClassVar[list[type[EmitterUtility]]] = []

    def __init_subclass__(cls, **kwargs: Any) -> None:
        super().__init_subclass__(**kwargs)
        if inspect.isabstract(cls):
            return
        EmitterUtility._check_handlers.append(cls)

    @staticmethod
    @abstractmethod
    def check(options: BlockOptions) -> tuple[Kind, str] | None:
        raise NotImplementedError

    @classmethod
    @abstractmethod
    def emit_block(cls, block: Any) -> list[str] | tuple[str, list[str]] | None:
        raise NotImplementedError

    @classmethod
    def iter_checks(cls) -> tuple[type[EmitterUtility], ...]:
        return tuple(
            sorted(
                cls._check_handlers,
                key=lambda utility: utility.check_priority,
                reverse=True,
            )
        )
