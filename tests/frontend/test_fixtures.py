from __future__ import annotations

from pathlib import Path

from vg2c.frontend import classify, parse
from vg2c.kind import Kind


def _parse_and_classify(fixtures_dir: Path, name: str) -> list:
    path = fixtures_dir / name
    text = path.read_text(encoding="utf-8", errors="replace")
    blocks = parse(text, source=path)
    return classify(blocks)


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

    sqlite_block = classified[2]
    assert (
        sqlite_block.options.lookup.get("TABLE")
        == "yeuchuan_a0_29397.tab,yeuchuan_a1_29397.tab"
    )


def test_actual_script_expectations(FIXTURES: Path) -> None:
    classified = _parse_and_classify(FIXTURES, "actual_script.txt")

    kinds = {b.kind for b in classified}
    expected_kinds = {
        Kind.HTML_REPORT,
        Kind.MACRO_CONTROL,
        Kind.ROWS_IN_FILE,
        Kind.FS_DELETE,
        Kind.FS_COPY,
        Kind.EXTERNAL_RUN,
        Kind.EMAIL,
        Kind.SQLITE_QUERY,
        Kind.SQL_QUERY,
    }
    for expected in expected_kinds:
        assert expected in kinds, (
            f"Expected {expected} in classified kinds, but it was missing."
        )

    unknown_blocks = [b for b in classified if b.kind is Kind.UNKNOWN]
    assert not unknown_blocks, f"Expected no UNKNOWN blocks, got: {unknown_blocks}"

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
    ]
    for token in critical_tokens:
        assert any(v.lstrip().startswith(token) for v in macro_utils), (
            f"Missing macro control token coverage for: {token}"
        )


def test_oasys_and_aries_expectations(FIXTURES: Path) -> None:
    classified_oasys = _parse_and_classify(FIXTURES, "oasys.txt")
    assert len(classified_oasys) == 1
    assert classified_oasys[0].kind is Kind.SQL_QUERY

    classified_aries = _parse_and_classify(FIXTURES, "aries_simple.txt")
    assert len(classified_aries) == 1
    assert classified_aries[0].kind is Kind.SQL_QUERY
