from __future__ import annotations

import pytest

from vg2c.dispatch.dispatcher import build_target
from vg2c.frontend.models import BlockOptions, ClassifiedBlock, Kind, ParsedBlock, SourceSpan
from vg2c.resolver import resolve

_SPAN = SourceSpan(file=None, start_line=1, end_line=1)


def _resolved_block(index: int, kind: Kind, options: dict[str, str], body: str = ""):
    """Build a minimal ResolvedBlock via the real resolver for a single block."""
    parsed = ParsedBlock(
        index=index,
        options=BlockOptions.from_pairs(options.items()),
        body=body,
        raw=body,
        span=_SPAN,
    )
    classified = ClassifiedBlock(parsed=parsed, kind=kind, reason="test")
    resolved_prog = resolve([classified], diagnostics=[])
    return resolved_prog.blocks[0]


def test_mars_reader_target() -> None:
    block = _resolved_block(
        0,
        Kind.MARS_READ,
        {
            "NODE": "KM.[A15_PROD_21.].MARS",
            "INSTANCE": "8486",
            "RECORD": "Calendar@1.0.0.0",
        },
    )
    target, diags = build_target(block, "oracle_mars")
    assert target.dialect == "oracle_mars"
    assert target.reader_class_hint == "OracleReader"
    assert target.database_arg == "MARS"
    assert target.record_name == "Calendar"
    assert target.record_version == "1.0.0.0"
    assert target.node == "KM.[A15_PROD_21.].MARS"
    assert target.instance == "8486"
    assert not diags


def test_oasys_reader_target() -> None:
    block = _resolved_block(
        0,
        Kind.OASYS_READ,
        {
            "NODE": "KM.OASYS",
            "INSTANCE": "29397",
            "RECORD": "Spc_Chart_or_Raw@1.0.0.0",
        },
    )
    target, diags = build_target(block, "oracle_oasys")
    assert target.dialect == "oracle_oasys"
    assert target.database_arg == "OASYS"
    assert target.record_name == "Spc_Chart_or_Raw"
    assert target.record_version == "1.0.0.0"
    assert not diags


def test_sqlite_reader_target() -> None:
    block = _resolved_block(
        0,
        Kind.SQLITE_QUERY,
        {
            "NODE": ".\\",
            "OLEDB": "SQLite",
            "ENGINE": "SQLite",
            "INSTANCE": "29397",
        },
    )
    target, diags = build_target(block, "sqlite")
    assert target.dialect == "sqlite"
    assert target.reader_class_hint == "SQLiteReader"
    assert target.database_arg is None
    assert target.record_name is None
    assert target.record_version is None
    assert target.node == ".\\"
    assert not diags


def test_record_without_version_emits_malformed() -> None:
    block = _resolved_block(
        0,
        Kind.MARS_READ,
        {"NODE": "KM.MARS", "RECORD": "Calendar"},
    )
    target, diags = build_target(block, "oracle_mars")
    assert target.record_name == "Calendar"
    assert target.record_version is None
    assert any(d.code == "dispatch-record-malformed" for d in diags)


def test_missing_record_no_diagnostic() -> None:
    block = _resolved_block(
        0,
        Kind.SQLITE_QUERY,
        {"NODE": ".\\", "ENGINE": "SQLite"},
    )
    target, diags = build_target(block, "sqlite")
    assert target.record_name is None
    assert target.record_version is None
    assert not diags
