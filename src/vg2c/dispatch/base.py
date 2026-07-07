from __future__ import annotations

from abc import ABC, abstractmethod
from typing import Any, ClassVar

from vg2c.dispatch.models import Dialect, DispatchConfig, ReaderTarget
from vg2c.frontend.models import Diagnostic, Kind, SourceSpan
from vg2c.resolver.models import ResolvedBlock


class DialectHandler(ABC):
    """Abstract base class for SQL dialect handlers."""

    dialect: ClassVar[Dialect]
    kind: ClassVar[Kind | None] = None
    database_arg: ClassVar[str | None] = None
    schema_placeholder: ClassVar[str | None] = None
    datasyncx_reader_name: ClassVar[str | None] = None
    _handlers_by_dialect: ClassVar[dict[Dialect, type[DialectHandler]]] = {}
    _handlers_by_kind: ClassVar[dict[Kind, type[DialectHandler]]] = {}

    def __init_subclass__(cls, **kwargs: Any) -> None:
        super().__init_subclass__(**kwargs)

        if "dialect" not in cls.__dict__:
            return

        dialect = cls.__dict__["dialect"]
        if not isinstance(dialect, str) or not dialect.strip():
            raise ValueError(f"{cls.__name__}: dialect must be a non-empty string")

        existing = DialectHandler._handlers_by_dialect.get(dialect)
        if existing is not None and existing is not cls:
            raise ValueError(f"Duplicate dialect handler registered for {dialect!r}")
        DialectHandler._handlers_by_dialect[dialect] = cls

        kind = getattr(cls, "kind", None)
        if kind is not None and kind is not Kind.SQL_QUERY:
            existing_kind = DialectHandler._handlers_by_kind.get(kind)
            if existing_kind is not None and existing_kind is not cls:
                raise ValueError(
                    f"Duplicate dialect handler registered for kind {kind!r}"
                )
            DialectHandler._handlers_by_kind[kind] = cls

    @classmethod
    def for_dialect(cls, dialect: Dialect) -> type[DialectHandler] | None:
        return cls._handlers_by_dialect.get(dialect)

    @classmethod
    def for_kind(cls, kind: Kind) -> type[DialectHandler] | None:
        return cls._handlers_by_kind.get(kind)

    @classmethod
    def resolve_dialect(cls, kind: Kind) -> Dialect | None:
        handler = cls.for_kind(kind)
        return None if handler is None else handler.dialect

    @classmethod
    def derive_handler_from_signals(
        cls, node: str, engine: str, oledb: str
    ) -> type[DialectHandler] | None:
        for handler in cls._handlers_by_dialect.values():
            if handler.matches_signals(node=node, engine=engine, oledb=oledb):
                return handler
        return None

    @classmethod
    def derive_from_signals(cls, node: str, engine: str, oledb: str) -> Dialect | None:
        handler = cls.derive_handler_from_signals(
            node=node,
            engine=engine,
            oledb=oledb,
        )
        return None if handler is None else handler.dialect

    @classmethod
    def sql_bearing_kinds(cls) -> frozenset[Kind]:
        return frozenset(
            handler.kind
            for handler in cls._handlers_by_dialect.values()
            if handler.kind is not None
        )

    @classmethod
    @abstractmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        """Check if this dialect matches the given option signals."""
        raise NotImplementedError

    @classmethod
    @abstractmethod
    def substitute(
        cls,
        body: str,
        config: DispatchConfig | None,
        span: SourceSpan | None,
        block_index: int,
    ) -> tuple[str, list[Diagnostic]]:
        """Perform dialect-specific schema placeholder substitution."""
        raise NotImplementedError

    @classmethod
    def build_reader_target(
        cls, block: ResolvedBlock
    ) -> tuple[ReaderTarget, list[Diagnostic]]:
        """Build a ReaderTarget from a resolved block."""
        diags: list[Diagnostic] = []
        opts = block.resolved_options.lookup

        node = opts.get("NODE", "")
        instance = opts.get("INSTANCE")
        record_raw = opts.get("RECORD")
        record_name, record_version = _parse_record(record_raw, block, diags)

        target = ReaderTarget(
            dialect=cls.dialect,
            database_arg=cls.database_arg,
            record_name=record_name,
            record_version=record_version,
            node=node,
            instance=instance,
        )
        return target, diags


def _parse_record(
    record_raw: str | None,
    block: ResolvedBlock,
    diags: list[Diagnostic],
) -> tuple[str | None, str | None]:
    """Parse /RECORD=Name@version. Returns (name, version) or (raw, None) with diagnostic."""
    if record_raw is None:
        return None, None

    parts = record_raw.split("@", 1)
    if len(parts) == 2 and parts[0] and parts[1]:
        return parts[0], parts[1]

    diags.append(
        Diagnostic(
            severity="info",
            code="dispatch-record-malformed",
            message=f"/RECORD={record_raw!r} is not in Name@version format; stored raw.",
            block_index=block.parsed.index,
            span=block.parsed.span,
        )
    )
    return record_raw, None
