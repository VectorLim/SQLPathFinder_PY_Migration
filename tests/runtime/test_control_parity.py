"""Run only the original value loop with inert children; never import ScriptHost."""

import ast
import copy
from pathlib import Path
import re

import pytest

from vg2c.runtime.controls import csv_chunks, for_values
from vg2c.runtime.values import read_macro_row, snapshot_values, substitute


def original_for(args):
    path = Path(__file__).parents[2] / "scripthost-utilities-decompiled/SPSQL3_py/SPFLib/SPFSQL3.py"
    source = ast.parse(path.read_text(encoding="utf-8-sig"))
    original = next(node for node in source.body if isinstance(node, ast.ClassDef) and node.name == "ForLoopTask")
    methods = [node for node in original.body if isinstance(node, ast.FunctionDef)
               and node.name in {"executeTaskCommand", "Substitute_Loop_Counter"}]
    oracle = ast.ClassDef(name="Oracle", bases=[], keywords=[], body=methods, decorator_list=[])
    module = ast.fix_missing_locations(ast.Module(body=[oracle], type_ignores=[]))
    namespace = {"copy": copy, "re": re}
    exec(compile(module, str(path), "exec"), namespace)
    calls = []

    class Child:
        SPFTaskItem = "|".join(f"<<<spf-{name}-{args[3]}{tail}>>>" for name, tail in
                               [("start", ""), ("end", ""), ("step", ""), ("step", "-int"),
                                ("loop-ctr", ""), ("loop-ctr", "-int")])
        childTasksList = []

        def execute(self):
            calls.append(self.SPFTaskItem)

    class Log:
        def __getattr__(self, name):
            return lambda *args: None

    task = namespace["Oracle"]()
    task.MyUtilities = ["{FOR-LOOP}", *args]
    task.childTasksList = [Child()]
    task.logger = Log()
    task.getCallingFuncName = lambda *args: "test"
    task.Write_Prompt = lambda: None
    task.Console = lambda *args: None
    task.convertYNToBool = lambda value: value.upper() == "Y"
    task.IsEmptyOrNone = lambda value: value is None or value == ""
    task.executeTaskCommand()
    return calls


@pytest.mark.parametrize("args", [
    ["0", "30", "5", "1"], ["0", "5", "2", "A", "N"],
    ["1", "5", "2", "A", "N"], ["1", "5", "2", "A", "Y"],
    ["0", "2", "0.5", "A", "N"], ["0", "2", "0.5", "A", "Y"],
    ["2", "2", "1", "A"], ["0", "3", "0", "A"],
    ["0", "3", "-1", "A"], ["0", "3", "1", "A", "N", "VERSION 2"],
])
def test_for_values_match_original(args):
    actual = []
    for row in for_values(*args):
        # Original token ordering keeps the integer suffix after the loop suffix.
        keys = [f"SPF-{name}-{args[3]}".upper() for name in ["START", "END", "STEP", "LOOP-CTR"]]
        actual.append("|".join(row[key] for key in [keys[0], keys[1], keys[2],
                                                    keys[2] + "-INT", keys[3], keys[3] + "-INT"]))
    # Child marker construction above follows the same suffix rule.
    expected = original_for(args)
    assert actual == expected


def test_chunk_remainder_cleanup_missing_and_header_only(tmp_path):
    (tmp_path / "data.csv").write_text("ID\n1\n2\n3\n")
    chunks = [path.read_text() for path in csv_chunks("data.csv", "chunk.csv", 2, workdir=tmp_path)]
    assert chunks == ["ID\n1\n2\n", "ID\n3\n"]
    assert not (tmp_path / "chunk.csv").exists()
    (tmp_path / "data.csv").write_text("ID\n")
    assert list(csv_chunks("data.csv", "chunk.csv", 2, workdir=tmp_path)) == []
    assert list(csv_chunks("missing.csv", "chunk.csv", 2, workdir=tmp_path)) == []


def test_chunk_output_cannot_delete_input(tmp_path):
    path = tmp_path / "data.csv"
    path.write_text("ID\n1\n")
    with pytest.raises(ValueError, match="overwrite its input"):
        list(csv_chunks("data.csv", "data.csv", 1, workdir=tmp_path))
    assert path.read_text() == "ID\n1\n"


def test_macro_blank_none_normalization_and_snapshots(tmp_path):
    (tmp_path / "macro.csv").write_text("[NAME],B\nvalue,\n")
    assert read_macro_row("macro.csv", workdir=tmp_path) == {"(NAME)": "value", "B": ""}
    assert substitute("<<<name>>>", macros={"NAME": None}) == ""
    first = snapshot_values(tmp_path, values={"cl_site": "PG"}, environ={"TEST": "one"})
    second = snapshot_values(tmp_path, values={"cl_site": "KM"}, environ={"TEST": "two"})
    assert substitute("<<<CL_SITE>>>/<<<%TEST%>>>", values=first) == "PG/one"
    assert substitute("<<<CL_SITE>>>/<<<%TEST%>>>", values=second) == "KM/two"
    assert first["SPF-DEFAULT-DIR"].endswith(__import__("os").sep)
    with pytest.raises(ValueError, match="Unknown value"):
        substitute("<<<missing>>>")
    with pytest.raises(ValueError, match="Missing closing"):
        substitute("<<<missing")
