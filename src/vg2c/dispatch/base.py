from __future__ import annotations

from abc import ABC, abstractmethod
from typing import ClassVar, Literal

from vg2c.dispatch.models import Dialect, DispatchConfig, ReaderTarget
from vg2c.frontend.models import Diagnostic, Kind, SourceSpan
from vg2c.resolver.models import ResolvedBlock


class DialectHandler(ABC):
    """Abstract base class for SQL dialect handlers."""

    dialect: ClassVar[Dialect]
    kind: ClassVar[Kind | None] = None
    reader_class_hint: ClassVar[Literal["OracleReader", "SQLiteReader"]]
    database_arg: ClassVar[str | None]
    schema_placeholder: ClassVar[str | None] = None
    one_shot_note: ClassVar[tuple[str, str] | None] = None

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
    def has_own_placeholders(cls, body: str) -> bool:
        """Check if body contains this dialect's schema placeholders."""
        return cls.schema_placeholder is not None and cls.schema_placeholder in body

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
            reader_class_hint=cls.reader_class_hint,
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
