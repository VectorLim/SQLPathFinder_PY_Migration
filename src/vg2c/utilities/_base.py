from __future__ import annotations

import inspect
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
    from vg2c.emitter.globals import GlobalValues
    from vg2c.emitter.models import CodeExpr
    from vg2c.frontend.models import BlockOptions


__all__ = ["EmitterUtility", "UtilitySpec"]


class UtilitySpec(ABC):
    """Base contract for all embeddable utilities and their semantic operations."""

    utility_name: ClassVar[str]
    handles: ClassVar[tuple[Kind, ...]] = ()
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
    def registered(cls) -> tuple[type[UtilitySpec], ...]:
        """Return loaded utilities in deterministic registration order."""
        return tuple(cls._registry.values())

    @classmethod
    def for_name(cls, name: str) -> type[UtilitySpec] | None:
        return cls._registry.get(name)

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
