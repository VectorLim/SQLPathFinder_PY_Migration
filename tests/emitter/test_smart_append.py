"""Tests for SmartAppend utility classification, emission, and CSV behavior."""

from __future__ import annotations

import csv
from pathlib import Path
from types import SimpleNamespace

from vg2c import compile_document
from vg2c.frontend import classify, parse
from vg2c.kind import Kind
from vg2c.utilities._base import UtilitySpec
from vg2c.utilities.smart_append import SmartAppend


def _options(utilities: str = "") -> SimpleNamespace:
    return SimpleNamespace(lookup={"UTILITIES": utilities} if utilities else {})


def _block(utilities: str) -> SimpleNamespace:
    return SimpleNamespace(kind=Kind.SMART_APPEND, index=5, resolved_options=_options(utilities))


def test_smartappend_classifies_by_case_insensitive_basename() -> None:
    result = SmartAppend.check(_options(r'@EXEDIR@\SMARTAPPEND.VA "dest.csv" "source.csv"'))

    assert result is not None
    assert result[0] is Kind.SMART_APPEND
    assert SmartAppend.check(_options(r'@EXEDIR@\OtherAppend.va "dest.csv" "source.csv"')) is None


def test_smartappend_emits_destination_before_source() -> None:
    _, lines = SmartAppend.emit_block(
        _block(r'@EXEDIR@\SmartAppend.va "HIST.csv" "data.csv" "Version 3"')
    )

    assert lines == ["ctx.smart_append.append('HIST.csv', 'data.csv')"]


def test_smartappend_writes_header_once_and_appends_data(tmp_path: Path, monkeypatch) -> None:
    monkeypatch.chdir(tmp_path)
    source = tmp_path / "source.csv"
    source.write_text("id,value\n1,first\n", encoding="utf-8")

    append = SmartAppend().append
    append("nested/destination.csv", "source.csv")
    source.write_text("id,value\n2,second\n", encoding="utf-8")
    append("nested/destination.csv", "source.csv")
    source.write_text("id,value\n", encoding="utf-8")
    append("nested/destination.csv", "source.csv")

    with (tmp_path / "nested" / "destination.csv").open(newline="", encoding="utf-8") as fh:
        assert list(csv.reader(fh)) == [["id", "value"], ["1", "first"], ["2", "second"]]


def test_smartappend_is_registered() -> None:
    assert UtilitySpec._registry["smart_append"] is SmartAppend
    assert UtilitySpec._emit_handlers[Kind.SMART_APPEND] is SmartAppend


def test_hamizah_smartappend_blocks_are_classified_and_emitted(FIXTURES: Path) -> None:
    source = FIXTURES / "hamizah.txt"
    blocks = classify(parse(source.read_text(encoding="utf-8"), source=source))
    smart_blocks = [
        block
        for block in blocks
        if "smartappend.va" in block.options.lookup.get("UTILITIES", "").lower()
    ]

    assert len(smart_blocks) == 2
    assert all(block.kind is Kind.SMART_APPEND for block in smart_blocks)

    generated = compile_document(source).emitted.source
    assert generated.count("ctx.smart_append.append(") == 2
    assert "pass  # TODO: utility command not classified" not in generated
