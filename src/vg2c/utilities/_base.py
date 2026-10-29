from __future__ import annotations

import inspect
from abc import ABC, abstractmethod
from typing import TYPE_CHECKING, Any, ClassVar

from vg2c.emitter.models import BlockEmission, UtilityOperationDefinition
from vg2c.emitter.operations import EmittableOperation
from vg2c.kind import Kind

if TYPE_CHECKING:
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
    def for_name(cls, name: str) -> type[UtilitySpec] | None:
        return cls._registry.get(name)

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
    def emit_block(block: Any, *, macros: str = "macros") -> BlockEmission:
        raise ValueError("Unsupported utility operation")


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
        cls, block: Any, *, macros: str = "macros"
    ) -> BlockEmission:
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
