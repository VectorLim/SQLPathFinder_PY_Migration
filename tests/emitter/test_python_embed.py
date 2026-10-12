"""Native Python blocks are emitted inside run(), not legacy ctx step functions."""
import ast

from vg2c import compile_document


def _emit(tmp_path, code, filename="script.py"):
    source = tmp_path / "embedded.txt"
    source.write_text(
        f"<OPTIONS>\n/WRITE-FILE=Y\n/CSV={filename}\n</OPTIONS>\n"
        + code + "\n<---- New Query ---->\n",
        encoding="utf-8",
    )
    return compile_document(source).emitted


def test_native_python_body_is_preserved(tmp_path):
    emitted = _emit(tmp_path, "import os\nprint(os.getcwd())")
    assert "import os" in emitted.source
    assert "print(os.getcwd())" in emitted.source
    assert "def step_" not in emitted.source
    assert "ctx" not in emitted.source
    ast.parse(emitted.source)


def test_nested_python_indentation_survives(tmp_path):
    emitted = _emit(
        tmp_path,
        "for i in range(10):\n    print(i)\n    if i > 5:\n        break",
        "loop.py",
    )
    ast.parse(emitted.source)
    assert "    for i in range(10):" in emitted.source
    assert "        print(i)" in emitted.source
    assert "            break" in emitted.source


def test_empty_python_block_does_not_create_retired_step(tmp_path):
    emitted = _emit(tmp_path, "", "empty.py")
    assert "def run(" in emitted.source
    assert "def step_" not in emitted.source
    ast.parse(emitted.source)
