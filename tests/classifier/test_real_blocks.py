from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.classifier import Kind, classify_all
from vg2c.frontend.parser import parse_vg2


@pytest.mark.parametrize(
    ("fixture", "expected_counts"),
    [
        (
            "script_short.txt",
            {Kind.SQLITE_JOIN: 1},
        ),
        (
            "script_another.txt",
            {Kind.SQL_FETCH: 1, Kind.WRITE_FILE: 1, Kind.RUN_PYTHON: 1},
        ),
        (
            "sql_script.txt",
            {Kind.SQL_FETCH: 2, Kind.SQLITE_JOIN: 1},
        ),
        (
            "script_from_vietnam.txt",
            {
                Kind.WRITE_FILE: 4,
                Kind.SQL_FETCH: 1,
                Kind.RUN_PYTHON: 3,
                Kind.RUN_UTILITY: 2,
                Kind.SQLITE_JOIN: 7,
            },
        ),
        (
            "actual_script.txt",
            {
                Kind.HTML_REPORT: 3,
                Kind.WRITE_FILE: 6,
                Kind.RUN_UTILITY: 10,
                Kind.MACRO_OPEN: 4,
                Kind.MACRO_CLOSE: 4,
                Kind.ROWS_IN_FILE: 5,
                Kind.IF_OPEN: 7,
                Kind.IF_ELSE: 3,
                Kind.SQLITE_JOIN: 7,
                Kind.IF_CLOSE: 7,
                Kind.SQL_FETCH: 4,
            },
        ),
    ],
)
def test_real_block_counts(
    fixture: str, expected_counts: dict[Kind, int] | None, FIXTURES: Path
) -> None:
    if expected_counts is None:
        pytest.skip("Confirm counts after first run, then update parametrize.")

    blocks = parse_vg2(FIXTURES / fixture)
    classification = classify_all(blocks)

    from collections import Counter

    actual_counts = Counter(cb.kind for cb in classification.blocks)

    for kind, count in expected_counts.items():
        assert actual_counts[kind] == count, f"Expected {count} {kind}, got {actual_counts[kind]}"


def test_no_unknown_in_fixtures(FIXTURES: Path) -> None:
    """Verify all real fixtures have zero UNKNOWN blocks."""
    for fixture_file in FIXTURES.glob("*.txt"):
        blocks = parse_vg2(fixture_file)
        classification = classify_all(blocks)

        unknown_blocks = [cb for cb in classification.blocks if cb.kind == Kind.UNKNOWN]
        assert len(unknown_blocks) == 0, (
            f"{fixture_file.name} has {len(unknown_blocks)} UNKNOWN blocks"
        )


def test_every_block_has_reason(FIXTURES: Path) -> None:
    """Verify every classified block has a non-empty reason."""
    for fixture_file in FIXTURES.glob("*.txt"):
        blocks = parse_vg2(fixture_file)
        classification = classify_all(blocks)

        for cb in classification.blocks:
            assert cb.reason, f"Block {cb.parsed.index} in {fixture_file.name} has no reason"


def test_classification_is_deterministic(FIXTURES: Path) -> None:
    """Verify classifying the same file twice produces identical results."""
    fixture = FIXTURES / "script_short.txt"
    blocks = parse_vg2(fixture)

    result1 = classify_all(blocks)
    result2 = classify_all(blocks)

    assert result1 == result2
