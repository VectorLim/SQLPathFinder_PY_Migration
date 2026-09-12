from __future__ import annotations

import shutil
from pathlib import Path

from vg2c import translate
from vg2c.utilities._emit_helpers import parse_table_binding
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


def test_icmpcs_translation_emits_table_bindings_without_running_artifact(tmp_path) -> None:
    source = tmp_path / "ICMPCS.txt"
    shutil.copy(ROOT / "ICMPCS.txt", source)

    output = translate(source)
    generated = output.read_text(encoding="utf-8")

    assert "inputs=[('data.csv', 'T0')]" in generated
    assert "inputs=[('IPM_Data.csv', 'T0')]" in generated
    compile(generated, str(output), "exec")
