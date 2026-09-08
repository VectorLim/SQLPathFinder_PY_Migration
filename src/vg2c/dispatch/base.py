from __future__ import annotations

import re
from abc import ABC, abstractmethod
from typing import Any, ClassVar

from vg2c.dispatch.models import ReaderSpec, ReaderTarget
from vg2c.kind import Kind
from vg2c.logger import Logger
from vg2c.resolver.models import ResolvedBlock

log = Logger.getLogger("vg2c.dispatch.base")


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
    def for_reader(cls, reader: ReaderSpec | str) -> type[DialectHandler] | None:
        reader_id = reader if isinstance(reader, str) else reader.id
        return cls._handlers_by_reader_id.get(reader_id)

    @classmethod
    def for_kind(cls, kind: Kind) -> type[DialectHandler] | None:
        return cls._handlers_by_kind.get(kind)

    @classmethod
    def resolve_reader(cls, kind: Kind) -> ReaderSpec | None:
        handler = cls.for_kind(kind)
        return None if handler is None else handler.reader

    @classmethod
    def derive_handler_from_signals(
        cls, node: str, engine: str, oledb: str
    ) -> type[DialectHandler] | None:
        for handler in cls._handlers_by_reader_id.values():
            if handler.matches_signals(node=node, engine=engine, oledb=oledb):
                return handler
        return None

    @classmethod
    def derive_reader_from_signals(
        cls, node: str, engine: str, oledb: str
    ) -> ReaderSpec | None:
        handler = cls.derive_handler_from_signals(
            node=node,
            engine=engine,
            oledb=oledb,
        )
        return None if handler is None else handler.reader

    @classmethod
    def sql_bearing_kinds(cls) -> frozenset[Kind]:
        return frozenset(
            handler.kind
            for handler in cls._handlers_by_reader_id.values()
            if handler.kind is not None
        )

    @classmethod
    @abstractmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        """Check if this dialect matches the given option signals."""
        raise NotImplementedError

    @classmethod
    @abstractmethod
    def substitute(cls, body: str) -> str:
        """Perform dialect-specific SQL substitution."""
        raise NotImplementedError

    @classmethod
    def build_reader_target(cls, block: ResolvedBlock) -> ReaderTarget:
        """Build a ReaderTarget from a resolved block."""
        opts = block.resolved_options.lookup

        node = opts.get("NODE", "")
        instance = opts.get("INSTANCE")
        record_raw = opts.get("RECORD")
        record_name, record_version = cls._parse_record(record_raw, block)

        return ReaderTarget(
            record_name=record_name,
            record_version=record_version,
            node=node,
            instance=instance,
            site=cls._extract_site(node),
        )

    _SITE_RE = re.compile(r"^([A-Za-z][A-Za-z0-9_]*)(?:\.|$)")

    @classmethod
    def _extract_site(cls, node: str) -> str:
        """Extract the leading literal site token from /NODE, or ``""`` if unknown."""
        match = cls._SITE_RE.match(node)
        return match.group(1) if match else ""

    @staticmethod
    def _parse_record(
        record_raw: str | None,
        block: ResolvedBlock,
    ) -> tuple[str | None, str | None]:
        """Parse /RECORD=Name@version and retain malformed raw values diagnostically."""
        if record_raw is None:
            return None, None

        parts = record_raw.split("@", 1)
        if len(parts) == 2 and parts[0] and parts[1]:
            return parts[0], parts[1]

        loc = f"{block.span.file or '<input>'}:{block.span.start_line}:1"
        log.info(
            f"[dispatch-record-malformed] {loc} (block {block.index}): "
            f"/RECORD={record_raw!r} is not in Name@version format; stored raw."
        )
        return record_raw, None
