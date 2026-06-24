from __future__ import annotations

from vg2c.dataflow import analyze
from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    Kind,
    ParsedBlock,
    SourceSpan,
)
from vg2c.resolver import resolve


def _block(
    index: int,
    kind: Kind,
    options: dict[str, str] | None = None,
    body: str = "",
    utilities: str | None = None,
) -> ClassifiedBlock:
    opts = dict(options or {})
    if utilities is not None:
        opts["UTILITIES"] = utilities
    parsed = ParsedBlock(
        index=index,
        options=BlockOptions.from_pairs(opts.items()),
        body=body,
        raw=body,
        span=SourceSpan(file=None, start_line=index + 1, end_line=index + 1),
    )
    return ClassifiedBlock(parsed=parsed, kind=kind, reason="test")


def _analyze_blocks(blocks: list[ClassifiedBlock]):
    resolved = resolve(blocks, diagnostics=[])
    return analyze(resolved)


def test_detects_write_file_and_table_consumer() -> None:
    program = _analyze_blocks(
        [
            _block(0, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "foo.csv"}),
            _block(1, Kind.SQLITE_QUERY, {"ENGINE": "SQLite", "TABLE": "foo.csv"}),
        ]
    )
    assert any(
        p.csv_path == "foo.csv" and p.producer_kind == "write-file"
        for p in program.producers
    )
    assert any(
        c.csv_path == "foo.csv" and c.consumer_kind == "table"
        for c in program.consumers
    )
    assert any(
        e.csv_path == "foo.csv" and e.producer is not None for e in program.edges
    )


def test_sqlite_block_can_be_producer_and_consumer() -> None:
    program = _analyze_blocks(
        [
            _block(0, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "a.csv"}),
            _block(
                1,
                Kind.SQLITE_QUERY,
                {"ENGINE": "SQLite", "TABLE": "a.csv", "CSV": "b.csv"},
            ),
        ]
    )
    assert any(
        p.csv_path == "b.csv" and p.producer_kind == "sqlite-query"
        for p in program.producers
    )
    assert any(c.csv_path == "a.csv" for c in program.consumers)


def test_emits_order_violation_for_consumer_before_producer() -> None:
    program = _analyze_blocks(
        [
            _block(0, Kind.SQLITE_QUERY, {"ENGINE": "SQLite", "TABLE": "late.csv"}),
            _block(1, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "late.csv"}),
        ]
    )
    assert any(d.code == "dataflow-order-violation" for d in program.diagnostics)


def test_external_utility_candidate_softens_missing_producer() -> None:
    program = _analyze_blocks(
        [
            _block(
                0, Kind.UTILITY, utilities='@EXEDIR@\\SPFCopy.bat "source.csv" "." "N"'
            ),
            _block(
                1, Kind.MACRO_CONTROL, utilities='{ROWS-IN-FILE} "source.csv" "CNT" "N"'
            ),
        ]
    )
    assert any(
        d.code == "dataflow-likely-external-producer" for d in program.diagnostics
    )


def test_multiple_producers_same_scope_emit_overwrite_info() -> None:
    program = _analyze_blocks(
        [
            _block(0, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "dup.csv"}),
            _block(1, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "dup.csv"}),
            _block(2, Kind.SQLITE_QUERY, {"ENGINE": "SQLite", "TABLE": "dup.csv"}),
        ]
    )
    assert any(d.code == "dataflow-overwrite-same-scope" for d in program.diagnostics)


def test_branch_exclusive_producers_emit_info() -> None:
    program = _analyze_blocks(
        [
            _block(
                0, Kind.MACRO_CONTROL, utilities='{IF-THEN} "A" "EQS" "1" "" "" "" ""'
            ),
            _block(1, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "x.csv"}),
            _block(2, Kind.MACRO_CONTROL, utilities="{ELSE}"),
            _block(3, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "x.csv"}),
            _block(4, Kind.MACRO_CONTROL, utilities="{END-IF}"),
            _block(5, Kind.SQLITE_QUERY, {"ENGINE": "SQLite", "TABLE": "x.csv"}),
        ]
    )
    assert any(
        d.code == "dataflow-branch-exclusive-producers" for d in program.diagnostics
    )


def test_unused_producer_emits_info() -> None:
    program = _analyze_blocks(
        [_block(0, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "orphan.csv"})]
    )
    assert any(d.code == "dataflow-unused-output" for d in program.diagnostics)
    assert any(p.csv_path == "orphan.csv" for p in program.unused_producers)


def test_table_comma_split_patch_from_stage2() -> None:
    program = _analyze_blocks(
        [
            _block(0, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "a.csv"}),
            _block(1, Kind.WRITE_FILE, {"WRITE-FILE": "Y", "CSV": "b.csv"}),
            _block(2, Kind.SQLITE_QUERY, {"ENGINE": "SQLite", "TABLE": "a.csv,b.csv"}),
        ]
    )
    paths = [
        c.csv_path
        for c in program.consumers
        if c.block_index == 2 and c.consumer_kind == "table"
    ]
    assert "a.csv" in paths
    assert "b.csv" in paths
