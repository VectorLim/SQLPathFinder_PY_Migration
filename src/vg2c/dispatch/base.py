from __future__ import annotations

from abc import ABC, abstractmethod
from typing import Any, ClassVar

from vg2c.dispatch.models import ReaderSpec, ReaderTarget
from vg2c.kind import Kind
from vg2c.resolver.models import ResolvedBlock


class DialectHandler(ABC):
    """Abstract base class for SQL dialect handlers."""

    reader: ClassVar[ReaderSpec]
    reader_kwargs: ClassVar[dict[str, Any]] = {}
    kind: ClassVar[Kind] = Kind.SQL_QUERY

    _handlers_by_reader_id: ClassVar[dict[str, type[DialectHandler]]] = {}
    _handlers_by_kind: ClassVar[dict[Kind, type[DialectHandler]]] = {}

    def __init_subclass__(cls, **kwargs: Any) -> None:
        super().__init_subclass__(**kwargs)

        reader = getattr(cls, "reader", None)
        if reader is None:
            return
        if not isinstance(reader, ReaderSpec):
            raise ValueError(f"{cls.__name__}: reader must be a ReaderSpec")

        existing = DialectHandler._handlers_by_reader_id.get(reader.id)
        if existing is not None and existing is not cls:
            raise ValueError(f"Duplicate dialect handler registered for reader {reader.id!r}")
        DialectHandler._handlers_by_reader_id[reader.id] = cls

        kind = getattr(cls, "kind", None)
        if kind is not None and kind is not Kind.SQL_QUERY:
            existing_kind = DialectHandler._handlers_by_kind.get(kind)
            if existing_kind is not None and existing_kind is not cls:
                raise ValueError(f"Duplicate dialect handler registered for kind {kind!r}")
            DialectHandler._handlers_by_kind[kind] = cls

    @classmethod
    def for_kind(cls, kind: Kind) -> type[DialectHandler] | None:
        return cls._handlers_by_kind.get(kind)

    @classmethod
    def derive_handler_from_signals(
        cls, node: str, engine: str, oledb: str
    ) -> type[DialectHandler] | None:
        for handler in cls._handlers_by_reader_id.values():
            if handler.matches_signals(node=node, engine=engine, oledb=oledb):
                return handler
        return None

    @classmethod
    @abstractmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        """Check if this dialect matches the given option signals."""
        raise NotImplementedError

    @classmethod
    def substitute(cls, body: str) -> str:
        """Return SQL unchanged unless a dialect overrides substitution."""
        return body

    @classmethod
    def build_reader_target(cls, block: ResolvedBlock) -> ReaderTarget:
        """Build a ReaderTarget from a resolved block."""
        opts = block.resolved_options.lookup
        return ReaderTarget(node=opts.get("NODE", ""))
