from __future__ import annotations

import re
from pathlib import Path

import pytest

from vg2c.frontend import Kind, classify, parse

FIXTURE_NAMES = [
    "script_short.txt",
    "script_another.txt",
    "sql_script.txt",
    "actual_script.txt",
]


@pytest.mark.parametrize("fixture_name", FIXTURE_NAMES)
def test_fixture_parse_and_classify_basics(FIXTURES: Path, fixture_name: str) -> None:
    text = (FIXTURES / fixture_name).read_text(encoding="utf-8", errors="replace")

    blocks, parse_diagnostics = parse(text, source=FIXTURES / fixture_name)
    classified, classify_diagnostics = classify(blocks)
    diagnostics = [*parse_diagnostics, *classify_diagnostics]

    assert len(classified) >= 1
    assert [b.parsed.index for b in classified] == list(range(len(classified)))

    if fixture_name != "actual_script.txt":
        assert not [d for d in diagnostics if d.severity == "error"]


def test_script_short_has_sqlite_query(FIXTURES: Path) -> None:
    classified = _classify_fixture(FIXTURES, "script_short.txt")
    assert _has_kind(classified, Kind.SQLITE_QUERY)


def test_script_another_has_mars_write_and_utility(FIXTURES: Path) -> None:
    classified = _classify_fixture(FIXTURES, "script_another.txt")
    assert _has_kind(classified, Kind.SQL_QUERY)
    assert _has_kind(classified, Kind.WRITE_FILE)
    assert _has_any_kind(
        classified,
        (Kind.UTILITY, Kind.EXTERNAL_RUN, Kind.FS_COPY, Kind.FS_DELETE),
    )


def test_sql_script_has_mars_oasys_and_sqlite(FIXTURES: Path) -> None:
    classified = _classify_fixture(FIXTURES, "sql_script.txt")
    assert _has_kind(classified, Kind.SQL_QUERY)
    assert _has_kind(classified, Kind.SQLITE_QUERY)




def test_actual_script_has_expected_stage1_coverage(FIXTURES: Path) -> None:
    blocks, parse_diagnostics = parse(
        (FIXTURES / "actual_script.txt").read_text(encoding="utf-8", errors="replace"),
        source=FIXTURES / "actual_script.txt",
    )
    classified, classify_diagnostics = classify(blocks)
    diagnostics = [*parse_diagnostics, *classify_diagnostics]

    assert _has_kind(classified, Kind.HTML_REPORT)
    assert _has_kind(classified, Kind.SQLITE_QUERY)

    write_csv_values = [
        item.parsed.options.lookup.get("CSV", "")
        for item in classified
        if item.kind is Kind.WRITE_FILE and "CSV" in item.parsed.options.lookup
    ]
    lowered = [v.lower() for v in write_csv_values]
    assert any(v.endswith(".bat") for v in lowered)
    assert any(v.endswith(".csv") for v in lowered)
    assert any(v.endswith(".htm") for v in lowered)
    if any(v.endswith(".py") for v in lowered):
        assert any(v.endswith(".py") for v in lowered)

    macro_values = [
        item.parsed.options.lookup.get("UTILITIES", "")
        for item in classified
        if item.kind is Kind.MACRO_CONTROL
    ]
    required_tokens = [
        "{START-MACRO}",
        "{END-MACRO}",
        "{IF-THEN}",
        "{ELSE}",
        "{END-IF}",
        "{ROWS-IN-FILE}",
    ]
    for token in required_tokens:
        assert any(
            v.lstrip().startswith(token) for v in macro_values
        ), f"Missing macro token {token}"

    utility_values = [
        item.parsed.options.lookup.get("UTILITIES", "")
        for item in classified
        if item.kind is Kind.UTILITY
    ]
    external_values = [
        item.parsed.options.lookup.get("UTILITIES", "")
        for item in classified
        if item.kind is Kind.EXTERNAL_RUN
    ]
    fs_copy_values = [
        item.parsed.options.lookup.get("UTILITIES", "")
        for item in classified
        if item.kind is Kind.FS_COPY
    ]

    assert any(
        marker in value
        for value in external_values
        for marker in ("getcsrsu.bat", "setsiteparam.exe")
    )
    assert any("RoboCopy.va" in value for value in fs_copy_values)
    assert any("SQLPathFinder_Email.va" in value for value in utility_values)

    unknown_blocks = [item for item in classified if item.kind is Kind.UNKNOWN]
    assert not unknown_blocks, _unknown_failure_message(unknown_blocks)

    step_prompts: list[tuple[int, ...]] = []
    for item in classified:
        prompt = item.parsed.options.lookup.get("PROMPT-TEXT", "")
        if prompt.startswith("Step "):
            nums = tuple(int(v) for v in re.findall(r"\d+", prompt))
            if nums:
                step_prompts.append(nums)
    assert step_prompts == sorted(step_prompts)

    assert not [d for d in diagnostics if d.severity == "error"]


def _classify_fixture(fixtures: Path, file_name: str):
    text = (fixtures / file_name).read_text(encoding="utf-8", errors="replace")
    blocks, _ = parse(text, source=fixtures / file_name)
    classified, _ = classify(blocks)
    return classified


def _has_kind(classified, kind: Kind) -> bool:
    return any(item.kind is kind for item in classified)


def _has_any_kind(classified, kinds: tuple[Kind, ...]) -> bool:
    return any(item.kind in kinds for item in classified)


def _unknown_failure_message(unknown_blocks) -> str:
    snippets: list[str] = []
    for item in unknown_blocks:
        prompt = item.parsed.options.lookup.get("PROMPT-TEXT", "<missing PROMPT-TEXT>")
        snippets.append(f"prompt={prompt!r} raw={item.parsed.raw[:200]!r}")
    return "Unexpected UNKNOWN blocks: " + " | ".join(snippets)
