from __future__ import annotations

from pathlib import Path

from vg2c.frontend.reader import normalize_collapsed_lines, read_vg2


def test_utf16_with_bom_is_decoded(tmp_path: Path) -> None:
    raw = "\ufeff<OPTIONS>\n/REPORT=HTML-RUN\n</OPTIONS>\nSELECT 1\n".encode("utf-16")
    file_path = tmp_path / "sample_utf16.txt"
    file_path.write_bytes(raw)

    decoded = read_vg2(file_path)

    assert decoded == "<OPTIONS>\n/REPORT=HTML-RUN\n</OPTIONS>\nSELECT 1\n"


def test_line_endings_are_normalized(tmp_path: Path) -> None:
    file_path = tmp_path / "line_endings.txt"
    file_path.write_bytes(b"A\r\nB\rC\n")

    decoded = read_vg2(file_path)

    assert "\r" not in decoded
    assert decoded == "A\nB\nC\n"


def test_collapsed_line_repair_triggers_only_when_needed() -> None:
    one_delimiter = "<---- New Query ----> /REPORT=HTML-RUN SELECT 1"
    unchanged = normalize_collapsed_lines(one_delimiter)
    assert unchanged == one_delimiter

    two_delimiters = "<---- New Query ---->/REPORT=R1<---- New Query ---->/REPORT=R2"
    repaired = normalize_collapsed_lines(two_delimiters)
    assert "\n" in repaired
    assert "\n<---- New Query ---->" in repaired
    assert "\n/REPORT=" in repaired


def test_collapsed_line_repair_inserts_breaks_before_all_anchors() -> None:
    anchors = [
        "<---- New Query ---->",
        "<OPTIONS>",
        "</OPTIONS>",
        "/REPORT=",
        "/WRITE-FILE=",
        "/NODE=",
        "/WORKDIR=",
        "/UTILITIES=",
    ]
    collapsed = "x".join(anchors) + "<---- New Query ---->"
    repaired = normalize_collapsed_lines(collapsed)

    for anchor in anchors:
        assert f"\n{anchor}" in repaired


def test_no_repair_when_file_has_normal_newlines() -> None:
    text = "<---- New Query ---->\n/REPORT=A\n<---- New Query ---->\n/REPORT=B\n"
    assert normalize_collapsed_lines(text) == text
