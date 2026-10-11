"""Source-derived delimiter parity for real ScriptHost file extensions.

Original SPFLib/SPFUtilities/utils.py, GetFileDLM() ~478-515:
.tab files use tabs; .asc uses pipes; otherwise CSV uses commas.
This is independent of future editor metadata and external connections.
"""
from __future__ import annotations

import csv

import pandas as pd
import pytest

from vg2c.runtime.csv_io import _CsvIO


@pytest.mark.parametrize(
    ("suffix", "delimiter"),
    [(".tab", "\t"), (".asc", "|"), (".csv", ",")],
)
def test_write_and_read_dataframe_respects_file_extension(tmp_path, suffix, delimiter):
    path = tmp_path / f"result{suffix}"
    io = _CsvIO(workdir=tmp_path)
    io.write(path.name, pd.DataFrame({"id": ["01", "02"], "label": ["a", "b"]}))
    assert path.read_text(encoding="utf-8") == (
        f"id{delimiter}label\n01{delimiter}a\n02{delimiter}b\n"
    )
    assert list(io.iter(path.name)) == [
        {"id": "01", "label": "a"}, {"id": "02", "label": "b"}
    ]


@pytest.mark.parametrize(
    ("suffix", "delimiter"),
    [(".tab", "\t"), (".asc", "|")],
)
def test_write_dicts_and_header_only_respect_extension(tmp_path, suffix, delimiter):
    io = _CsvIO(workdir=tmp_path)
    file_name = f"rows{suffix}"
    io.write(file_name, [{"id": "01", "label": "first"}])
    assert (tmp_path / file_name).read_text(encoding="utf-8") == (
        f"id{delimiter}label\n01{delimiter}first\n"
    )
    io.write(file_name, [], header=["id", "label"])
    assert (tmp_path / file_name).read_text(encoding="utf-8") == (
        f"id{delimiter}label\n"
    )


def test_tab_chunks_keep_tab_delimiters_between_operations(tmp_path):
    io = _CsvIO(workdir=tmp_path)
    with (tmp_path / "input.tab").open("w", encoding="utf-8", newline="") as stream:
        csv.writer(stream, delimiter="\t").writerows(
            [["id", "label"], ["01", "a,b"], ["02", "a\tb"], ["03", "c"]]
        )
    seen: list[list[list[str]]] = []
    for chunk in io.iter_chunks("input.tab", "chunk.tab", 2):
        with chunk.open(encoding="utf-8", newline="") as stream:
            seen.append(list(csv.reader(stream, delimiter="\t")))
    assert seen == [
        [["id", "label"], ["01", "a,b"], ["02", "a\tb"]],
        [["id", "label"], ["03", "c"]],
    ]
