from __future__ import annotations

import ast
import inspect
import re
from abc import ABC, abstractmethod
from typing import TYPE_CHECKING, Any, ClassVar

from vg2c.kind import Kind

if TYPE_CHECKING:
    from vg2c.frontend.models import BlockOptions


__all__ = ["EmitterUtility", "UtilitySpec"]


_CLASS_SIG_RE = re.compile(r"^(\s*class\s+\w+)\(.*\):\s*$")


def _find_class_def(source: str, class_name: str) -> ast.ClassDef | None:
    tree = ast.parse(source)
    for node in ast.walk(tree):
        if isinstance(node, ast.ClassDef) and node.name == class_name:
            return node
    return None


def _strip_embed_artifacts(source: str, class_name: str) -> str:
    lines = source.split("\n")

    while lines and lines[0].lstrip().startswith("@"):
        lines.pop(0)

    if not lines:
        return ""

    lines[0] = _CLASS_SIG_RE.sub(r"\1:", lines[0])
    lines[0] = lines[0].replace(f"({EmitterUtility.__name__}):", ":")
    lines[0] = lines[0].replace(f"({UtilitySpec.__name__}):", ":")
    lines[0] = lines[0].replace(
        f"({class_name}, {UtilitySpec.__name__}):", f"({class_name}):"
    )

    lines = [
        line
        for line in lines
        if not line.lstrip().startswith("handles =") and "@emittable" not in line
    ]

    return "\n".join(lines).rstrip()


class UtilitySpec(ABC):
    """Base contract for all embeddable utilities."""

    utility_name: ClassVar[str]
    handles: ClassVar[tuple[Kind, ...]] = ()
    # Forced-in regardless of which block Kinds the workflow uses (e.g. PipelineContext/Logger
    # are referenced unconditionally by every generated script).
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
                    "duplicate handler for "
                    f"{handled_kind}: {owner.__name__} and {cls.__name__}"
                )
            UtilitySpec._emit_handlers[handled_kind] = cls

    @classmethod
    def get_source(cls, source_override: str | None = None) -> str:
        """Return this utility's embeddable source.

        ``source_override``, when given, is a whole-file source string (already
        cleaned of promoted inline imports -- see ``utilities/__init__.py``) to
        extract this class's definition from instead of live ``inspect.getsource``.
        """
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
        """Return the loaded utility registered under *name*."""

        return cls._registry.get(name)

    @classmethod
    def for_kind(cls, kind: Kind) -> type[UtilitySpec] | None:
        """Return the loaded emitter for *kind*, falling back to UNKNOWN."""

        return cls._emit_handlers.get(kind) or cls._emit_handlers.get(Kind.UNKNOWN)

    @staticmethod
    def emit_block(block: Any) -> list[str] | tuple[str, list[str]] | None:
        return None

    @staticmethod
    def _step_name(block: Any, suffix: str) -> str:
        return f"step_{block.index:04d}_{suffix}"

    @staticmethod
    def _emit_step_source(name: str, body_lines: list[str]) -> tuple[str, str]:
        lines = [f"def {name}(ctx) -> None:"]
        if body_lines:
            for body_line in body_lines:
                for line in body_line.split("\n"):
                    if line.strip():
                        lines.append(f"    {line}")
                    else:
                        lines.append("")
        else:
            lines.append("    pass")
        return "\n".join(lines), f"{name}(ctx)"

    @classmethod
    def _wrap_in_step(
        cls, subclass: type[UtilitySpec], block: Any, result: Any
    ) -> tuple[str, str] | None:
        if result is None:
            return None
        if (
            isinstance(result, tuple)
            and len(result) == 2
            and isinstance(result[1], list)
        ):
            suffix, body_lines = result
        else:
            suffix = getattr(subclass, "utility_name", "utility")
            body_lines = result
        return cls._emit_step_source(cls._step_name(block, suffix), body_lines)

    @classmethod
    def dispatch_and_emit(cls, block: Any) -> tuple[str, str]:
        handler_cls = cls._emit_handlers.get(block.kind)
        if handler_cls is not None:
            emitted = handler_cls.emit_block(block)
            if emitted is not None:
                wrapped = cls._wrap_in_step(handler_cls, block, emitted)
                if wrapped is not None:
                    return wrapped
        return "", ""


class EmitterUtility(UtilitySpec):
    """Utility that participates in Stage 1 classification and block emission."""

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
        return tuple(cls._check_handlers)
