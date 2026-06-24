from __future__ import annotations

from vg2c.dispatch.models import Dialect, ReaderTarget
from vg2c.frontend.models import Diagnostic
from vg2c.resolver.models import ResolvedBlock

_DIALECT_TO_DATABASE: dict[Dialect, str] = {
    "oracle_mars": "MARS",
    "oracle_oasys": "OASYS",
    "oracle_aries": "ARIES",
}


def build_target(
    block: ResolvedBlock,
    dialect: Dialect,
) -> tuple[ReaderTarget, list[Diagnostic]]:
    """Build a ReaderTarget from a resolved block and its resolved dialect."""
    diags: list[Diagnostic] = []
    opts = block.resolved_options.lookup

    node = opts.get("NODE", "")
    instance = opts.get("INSTANCE")
    record_raw = opts.get("RECORD")
    record_name, record_version = _parse_record(record_raw, block, diags)

    database_arg = _DIALECT_TO_DATABASE.get(dialect)  # None for sqlite

    target = ReaderTarget(
        dialect=dialect,
        reader_class_hint="SQLiteReader" if dialect == "sqlite" else "OracleReader",
        database_arg=database_arg,
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

    # Malformed: present but not in Name@version format
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
