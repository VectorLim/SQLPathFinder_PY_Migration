from __future__ import annotations

import shutil
from pathlib import Path

from vg2c import translate
from vg2c.utilities._emit_helpers import parse_table_binding
from vg2c.utilities.csv_io import CsvIO
from vg2c.utilities.sqlite_reader import SqliteReader

ROOT = Path(__file__).parents[2]


def test_parse_table_binding_preserves_windows_paths() -> None:
    assert parse_table_binding(r"C:\work\data.csv") == (r"C:\work\data.csv", None)
    assert parse_table_binding(r"C:\work\data.csv:T0") == (r"C:\work\data.csv", "T0")


def test_sqlite_reader_uses_bound_table_name_and_plain_file_stem(tmp_path) -> None:
    csv_path = tmp_path / "data.csv"
    csv_path.write_text("lot\nL1\n", encoding="utf-8")
    reader = SqliteReader()

    bound = reader.execute("SELECT lot FROM [T0]", [(str(csv_path), "T0")])
    plain = reader.execute("SELECT lot FROM [data]", [str(csv_path)])

    assert bound.to_dict("records") == [{"lot": "L1"}]
    assert plain.to_dict("records") == [{"lot": "L1"}]


def test_sqlite_reader_preserves_select_columns_when_result_is_empty(tmp_path) -> None:
    left = tmp_path / "left.csv"
    right = tmp_path / "right.csv"
    left.write_text("lot,operation\n", encoding="utf-8")
    right.write_text("lot,out_date\n", encoding="utf-8")

    result = SqliteReader().execute(
        "SELECT a1.lot AS lot_1, a1.operation AS operation_1, a0.out_date "
        "FROM [left] a1 LEFT JOIN [right] a0 ON a1.lot = a0.lot",
        [str(left), str(right)],
    )

    assert result.empty
    assert result.columns.tolist() == ["lot_1", "operation_1", "out_date"]
    output = tmp_path / "output.csv"
    CsvIO().write(str(output), result)
    assert output.read_text(encoding="utf-8") == "lot_1,operation_1,out_date\n"


def test_icmpcs_translation_emits_table_bindings_without_running_artifact(tmp_path) -> None:
    source = tmp_path / "ICMPCS.txt"
    shutil.copy(ROOT / "ICMPCS.txt", source)

    output = translate(source)
    generated = output.read_text(encoding="utf-8")

    assert "inputs=[('data.csv', 'T0')]" in generated
    assert "inputs=[('IPM_Data.csv', 'T0')]" in generated
    compile(generated, str(output), "exec")
