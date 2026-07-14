from __future__ import annotations

from pathlib import Path
from vg2c.frontend import parse, classify
from vg2c.kind import Kind


def _parse_and_classify(fixtures_dir: Path, name: str) -> list:
    path = fixtures_dir / name
    text = path.read_text(encoding="utf-8", errors="replace")
    blocks = parse(text, source=path)
    classified = classify(blocks)
    return classified


def test_script_short_expectations(FIXTURES: Path) -> None:
    classified = _parse_and_classify(FIXTURES, "script_short.txt")

    assert len(classified) == 1
    block = classified[0]
    assert block.kind is Kind.SQLITE_QUERY
    assert block.options.lookup.get("CSV") == "owner.csv"
    assert block.options.lookup.get("HEADERS") == "owner"


def test_script_another_expectations(FIXTURES: Path) -> None:
    classified = _parse_and_classify(FIXTURES, "script_another.txt")

    assert len(classified) == 3
    assert [b.kind for b in classified] == [
        Kind.SQL_QUERY,
        Kind.PYTHON_EMBED,
        Kind.EXTERNAL_RUN,
    ]

    # Preserve expected utility payload signature for Run_Python_Script
    assert (
        classified[2].options.lookup.get("UTILITIES")
        == '@EXEDIR@\\Run_Python_Script.va "lich.py" "" "N" "atd_atm.hadoop" "Python-v3"'
    )


def test_sql_script_expectations(FIXTURES: Path) -> None:
    classified = _parse_and_classify(FIXTURES, "sql_script.txt")

    assert len(classified) == 3
    assert [b.kind for b in classified] == [
        Kind.SQL_QUERY,
        Kind.SQL_QUERY,
        Kind.SQLITE_QUERY,
    ]

    # Expected table-list handling in SQLite block
    sqlite_block = classified[2]
    assert sqlite_block.options.lookup.get("TABLE") == "yeuchuan_a0_29397.tab,yeuchuan_a1_29397.tab"


def test_actual_script_expectations(FIXTURES: Path) -> None:
    classified = _parse_and_classify(FIXTURES, "actual_script.txt")

    # Assert specific kinds exist
    kinds = {b.kind for b in classified}
    expected_kinds = {
        Kind.HTML_REPORT,
        Kind.MACRO_CONTROL,
        Kind.FS_DELETE,
        Kind.FS_COPY,
        Kind.EXTERNAL_RUN,
        Kind.EMAIL,
        Kind.SQLITE_QUERY,
        Kind.SQL_QUERY,
    }
    for ek in expected_kinds:
        assert ek in kinds, f"Expected {ek} in classified kinds, but it was missing."

    # No UNKNOWN blocks
    unknown_blocks = [b for b in classified if b.kind is Kind.UNKNOWN]
    assert not unknown_blocks, f"Expected no UNKNOWN blocks, got: {unknown_blocks}"

    # Macro control token coverage for critical branch tokens
    macro_utils = [
        b.options.lookup.get("UTILITIES", "")
        for b in classified
        if b.kind is Kind.MACRO_CONTROL
    ]
    critical_tokens = [
        "{START-MACRO}",
        "{END-MACRO}",
        "{IF-THEN}",
        "{ELSE}",
        "{END-IF}",
        "{ROWS-IN-FILE}",
    ]
    for token in critical_tokens:
        assert any(
            v.lstrip().startswith(token) for v in macro_utils
        ), f"Missing macro control token coverage for: {token}"


def test_oasys_and_aries_expectations(FIXTURES: Path) -> None:
    # oasys.txt -> SQL_QUERY
    classified_oasys = _parse_and_classify(FIXTURES, "oasys.txt")
    assert len(classified_oasys) == 1
    assert classified_oasys[0].kind is Kind.SQL_QUERY

    # aries_simple.txt -> SQL_QUERY
    classified_aries = _parse_and_classify(FIXTURES, "aries_simple.txt")
    assert len(classified_aries) == 1
    assert classified_aries[0].kind is Kind.SQL_QUERY
