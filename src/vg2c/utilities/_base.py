from __future__ import annotations

import ast
import inspect
from abc import ABC, abstractmethod
from typing import TYPE_CHECKING, Any, ClassVar

from vg2c.emitter.models import (
    EmittableOperation,
    StepEmission,
    UtilityOperationDefinition,
    build_step_emission,
    emittable,
)
from vg2c.kind import Kind

if TYPE_CHECKING:
    from vg2c.emitter.globals import GlobalValues
    from vg2c.emitter.models import CodeExpr
    from vg2c.frontend.models import BlockOptions


__all__ = ["EmitterUtility", "UtilitySpec"]


class UtilitySpec(ABC):
    """Base contract for all embeddable utilities and their semantic operations."""

    utility_name: ClassVar[str]
    handles: ClassVar[tuple[Kind, ...]] = ()
    script_settings: ClassVar[tuple[tuple[str, object, str], ...]] = ()
    _registry: ClassVar[dict[str, type[UtilitySpec]]] = {}
    _emit_handlers: ClassVar[dict[Kind, type[UtilitySpec]]] = {}
    _script_settings: ClassVar[dict[str, tuple[object, str]]] = {}

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

        for setting in cls.__dict__.get("script_settings", ()):
            if not isinstance(setting, tuple) or len(setting) != 3:
                raise ValueError(
                    f"{cls.__name__}: script settings must be (name, default, description) tuples"
                )
            setting_name, default, description = setting
            if not isinstance(setting_name, str) or not setting_name.isidentifier():
                raise ValueError(f"{cls.__name__}: invalid script setting name {setting_name!r}")
            if not isinstance(description, str) or not description.strip():
                raise ValueError(
                    f"{cls.__name__}: script setting {setting_name!r} needs a description"
                )
            existing_setting = UtilitySpec._script_settings.get(setting_name)
            value = (default, description)
            if existing_setting is not None and existing_setting != value:
                raise ValueError(f"duplicate script setting: {setting_name}")
            UtilitySpec._script_settings[setting_name] = value

        for handled_kind in tuple(getattr(cls, "handles", ())):
            owner = UtilitySpec._emit_handlers.get(handled_kind)
            if owner is not None and owner is not cls:
                raise ValueError(
                    f"duplicate handler for {handled_kind}: {owner.__name__} and {cls.__name__}"
                )
            UtilitySpec._emit_handlers[handled_kind] = cls

    @classmethod
    def registered(cls) -> tuple[type[UtilitySpec], ...]:
        """Return loaded utilities in deterministic registration order."""
        return tuple(cls._registry.values())

    @classmethod
    def for_name(cls, name: str) -> type[UtilitySpec] | None:
        return cls._registry.get(name)

    @classmethod
    def generated_script_settings(cls) -> tuple[tuple[str, object, str], ...]:
        """Return deterministic, input-agnostic settings for generated scripts."""
        return tuple(
            (name, default, description)
            for name, (default, description) in sorted(cls._script_settings.items())
        )

    @classmethod
    def get_source(cls) -> str:
        """Return a standalone, metadata-free class source for compatibility tools.

        Runtime emission uses the symbol resolver; this helper remains useful to
        callers that inspect a utility class directly.
        """
        custom = getattr(cls, "__vg2c_source__", None)
        if custom is not None:
            return str(custom).rstrip()

        tree = ast.parse(inspect.getsource(cls))
        class_def = next(node for node in tree.body if isinstance(node, ast.ClassDef))
        class_def.bases = []
        class_def.keywords = []
        class_def.decorator_list = []
        for node in ast.walk(class_def):
            if isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef)):
                node.decorator_list = []
        return ast.unparse(class_def)

    @classmethod
    def operation_definitions(cls) -> tuple[UtilityOperationDefinition, ...]:
        """Enumerate @emittable operations directly from the registered utilities."""
        definitions: list[UtilityOperationDefinition] = []
        for utility in cls.registered():
            for name in utility.__dict__:
                raw = inspect.getattr_static(utility, name, None)
                if isinstance(raw, EmittableOperation):
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
        return raw.definition(utility) if isinstance(raw, EmittableOperation) else None

    @staticmethod
    def emit_block(
        block: Any, *, global_refs: dict[str, CodeExpr] | None = None
    ) -> list[str] | tuple[str, list[str]] | None:
        return None

    @classmethod
    def extract_globals(cls, block: Any) -> dict[str, object]:
        """Select configurable literals; extraction remains owned by each utility."""
        return {}

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
    def dispatch_and_emit(
        cls,
        block: Any,
        *,
        collected: GlobalValues | None = None,
        reserved: set[str] | frozenset[str] = frozenset(),
    ) -> StepEmission:
        handler_cls = cls._emit_handlers.get(block.kind)
        if handler_cls is not None:
            from vg2c.emitter.globals import resolve_globals

            global_refs = {}
            if collected is not None:
                global_refs = resolve_globals(
                    handler_cls.extract_globals(block), collected, block.index, reserved
                )
            emitted = handler_cls.emit_block(block, global_refs=global_refs)
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
    def emit_block(
        cls, block: Any, *, global_refs: dict[str, CodeExpr] | None = None
    ) -> list[str] | tuple[str, list[str]] | None:
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
