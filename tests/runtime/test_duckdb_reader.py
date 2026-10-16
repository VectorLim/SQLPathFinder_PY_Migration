from __future__ import annotations

import pytest

from vg2c import compile_document
from vg2c.runtime import DuckDbReader, execute_sql


def test_duckdb_reader_runs_tsv_marker_and_shared_crosstab(tmp_path) -> None:
    source = tmp_path / "input.tab"
    source.write_text(
        "group_name\tmetric\tvalue\nA\tx\t1\nA\tx\t2\nA\ty\t3\n", encoding="utf-8"
    )
    asset = tmp_path / "query.sql"
    asset.write_text(
        "SELECT group_name, metric, SUM(value) AS value "
        "FROM <DDBTable>t0</DDBTable> GROUP BY group_name, metric",
        encoding="utf-8",
    )

    result = execute_sql(
        asset,
        reader=DuckDbReader(),
        output="output.csv",
        workdir=tmp_path,
        inputs=[(str(source), "t0")],
        crosstab={"row_keys": ["group_name"], "header_key": "metric", "value_key": "value"},
    )

    assert result.to_dict("records") == [{"GROUP_NAME": "A", "X": "3.0", "Y": "3.0"}]


@pytest.mark.parametrize("suffix, delimiter", [(".tab", "\t"), (".csv", ",")])
def test_duckdb_reader_infers_mixed_visual_ids_beyond_the_default_sample(tmp_path, suffix, delimiter):
    source = tmp_path / ("units" + suffix)
    source.write_text(
        f"visual_id{delimiter}value\n"
        + f"7600000000000{delimiter}1\n" * 32000
        + f"766B4T4101312{delimiter}1\n",
        encoding="utf-8",
    )

    frame = DuckDbReader().execute(
        "SELECT visual_id, SUM(value) AS total FROM <DDBTable>t0</DDBTable> GROUP BY visual_id",
        [(str(source), "t0")],
        workdir=tmp_path,
    )

    assert dict(zip(frame.visual_id, frame.total)) == {"7600000000000": 32000, "766B4T4101312": 1}


@pytest.mark.parametrize("sql, inputs, message", [
    ("SELECT * FROM <DDBTable>missing</DDBTable>", [], "unknown DDBTable"),
    ("SELECT * FROM <DDBTable>t0", [], "malformed DDBTable"),
    ("SELECT 1", [("source.txt", "t0")], "unsupported extension"),
    ("SELECT 1", [("source.tab", "t0"), ("other.tab", "T0")], "duplicate table"),
])
def test_duckdb_reader_rejects_invalid_local_bindings(tmp_path, sql, inputs, message) -> None:
    with pytest.raises(ValueError, match=message):
        DuckDbReader().execute(sql, inputs, workdir=tmp_path)


def test_duckdb_translation_emits_marker_reader_and_windows_bindings(tmp_path) -> None:
    source = tmp_path / "duckdb.txt"
    source.write_text(
        "<OPTIONS>\n"
        "/NODE=DUCKDB:C:\\work\\first.tab:t0@@@@@DUCKDB:C:\\work\\second.csv:t1@@@@@DUCKDB:<<<INPUT>>>.parquet:t2\n"
        "/ENGINE=DUCKDB\n"
        "/CSV=result.csv\n"
        "</OPTIONS>\n"
        "SELECT * FROM <DDBTable>t0</DDBTable>\n",
        encoding="utf-8",
    )

    result = compile_document(source)

    assert not result.diagnostics
    assert "DuckDbReader" in result.emitted.source
    assert "inputs=[('C:\\\\work\\\\first.tab', 't0'), ('C:\\\\work\\\\second.csv', 't1')," in result.emitted.source
    assert "DUCKDB:" not in result.emitted.source
    assert "INPUT" in result.emitted.source


@pytest.mark.parametrize("node", ["DUCKDB:input.tab", "DUCKDB::t0", "missing.tab:t0"])
def test_duckdb_translation_rejects_malformed_node(tmp_path, node) -> None:
    source = tmp_path / "duckdb-invalid.txt"
    source.write_text(
        f"<OPTIONS>\n/NODE={node}\n/ENGINE=DUCKDB\n/CSV=result.csv\n</OPTIONS>\nSELECT 1\n",
        encoding="utf-8",
    )

    with pytest.raises(ValueError, match="DuckDB"):
        compile_document(source)
