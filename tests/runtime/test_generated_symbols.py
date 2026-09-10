"""Execute the actual generated artifact without access to vg2c imports."""

from __future__ import annotations

import csv
import os
import subprocess
import sys
from pathlib import Path

from vg2c.compilation import compile_document
from vg2c.utilities import assemble_utilities

RUNNER = """
import importlib.abc, runpy, sys
class NoCompiler(importlib.abc.MetaPathFinder):
    def find_spec(self, fullname, path=None, target=None):
        if fullname == 'vg2c' or fullname.startswith('vg2c.'):
            raise AssertionError('Generated script imported compiler: ' + fullname)
sys.meta_path.insert(0, NoCompiler())
runpy.run_path(sys.argv[1], run_name='__main__')
"""


def run_generated(tmp_path, text, *, runner=RUNNER):
    path = tmp_path / "workflow.txt"
    path.write_text(text, encoding="utf-8")
    result = compile_document(path)
    output = path.with_suffix(".py")
    output.write_text(result.emitted.source, encoding="utf-8")
    env = os.environ.copy()
    env.pop("PYTHONPATH", None)
    env["DATASYNCX_ORACLE_CLIENT"] = "home"
    process = subprocess.run(
        [sys.executable, "-I", "-c", runner, str(output)],
        cwd=tmp_path,
        env=env,
        capture_output=True,
        text=True,
        timeout=30,
    )
    assert process.returncode == 0, process.stdout + process.stderr
    return result


def test_sqlite_generated_script_executes_and_writes_exact_rows(tmp_path):
    fixture = Path(__file__).parents[1] / "fixtures/script_short.txt"
    (tmp_path / "ww_yield.csv").write_text("owner\nAda\nBob\nAda\n", encoding="utf-8")
    result = run_generated(tmp_path, fixture.read_text(encoding="utf-8"))
    with (tmp_path / "owner.csv").open(newline="", encoding="utf-8") as stream:
        assert list(csv.reader(stream)) == [["owner"], ["Ada"], ["Bob"]]
    assert "class SqliteEngine" not in result.emitted.source
    assert "def check(" not in result.emitted.source


def test_smart_append_generated_workflow_executes_twice_without_duplicate_header(tmp_path):
    (tmp_path / "source.csv").write_text("id,value\n1,first\n", encoding="utf-8")
    block = '<OPTIONS>\n/UTILITIES=SmartAppend.va "dest.csv" "source.csv"\n</OPTIONS>\n'
    result = run_generated(tmp_path, block + "<---- New Query ---->\n" + block)
    with (tmp_path / "dest.csv").open(newline="", encoding="utf-8") as stream:
        assert list(csv.reader(stream)) == [["id", "value"], ["1", "first"], ["1", "first"]]
    assert result.emitted.source.count("class SmartAppend:") == 1


def test_embedded_python_uses_transitive_macro_filesystem_and_report_methods(tmp_path):
    text = """<OPTIONS>
/WRITE-FILE=Y
/CSV=code.py
</OPTIONS>
ctx.macro.set_named('NAME', 'Ada')
with ctx.macro.scope({'NAME': 'Bob'}):
    ctx.write_file('hello.txt', 'Hello <<<NAME>>>')
ctx.fs_ops.copy('hello.txt', 'copied.txt')
ctx.html_report.delete('missing')
assert ctx.macro.named('NAME') == 'Ada'
"""
    result = run_generated(tmp_path, text)
    assert (tmp_path / "hello.txt").read_text() == "Hello Bob"
    assert (tmp_path / "copied.txt").read_text() == "Hello Bob"
    assert "def pop_frame" in result.emitted.source
    assert "def emit_block" not in result.emitted.source


def test_repeat_compilation_is_identical_and_metadata_slices_match(tmp_path):
    fixture = Path(__file__).parents[1] / "fixtures/script_short.txt"
    first = compile_document(fixture).emitted
    second = compile_document(fixture).emitted
    assert first == second
    for step in first.steps:
        assert (
            first.source[step.source_range.start_offset : step.source_range.end_offset]
            == step.source
        )
        for invocation in step.invocations:
            for parameter in invocation.parameters:
                span = parameter.source_range
                assert first.source[span.start_offset : span.end_offset] == parameter.source


def test_generated_html_layout_resolves_nested_callbacks_and_context_helpers(tmp_path):
    (tmp_path / "data.csv").write_text("name,ce%\nAda,0.85\n", encoding="utf-8")
    delimiter = "<\\\\>"
    report = (
        f"INPUT-FILE{delimiter}data.csv\n"
        f"COLUMN-DATA{delimiter}{delimiter}name{delimiter}ce%\n"
        f"COLUMN-HEADERS{delimiter}{delimiter}Name{delimiter}Yield\n"
    )
    layout = ":FILE:report.html\n:TITLE:Generated report\nHTM:REPORT1\n"
    text = (
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=code.py\n</OPTIONS>\n"
        f"ctx.html_report.defer('REPORT1', template={report!r})\n"
        f"ctx.html_report.layout(ctx, {layout!r})\n"
    )
    result = run_generated(tmp_path, text)
    content = (tmp_path / "report.html").read_text(encoding="utf-8")
    assert "Ada" in content and "85.00%" in content
    assert "<title>Generated report</title>" in content
    assert "def _render_report" in result.emitted.source
    assert "def emit_block" not in result.emitted.source


def test_generated_datasyncx_reader_with_controlled_double(tmp_path):
    # The generated source uses its normal DataSyncX import and runtime reader path.
    runner = RUNNER.replace(
        "runpy.run_path(sys.argv[1], run_name='__main__')",
        """
import types, pandas
for name in ('datasyncx', 'datasyncx.readers', 'datasyncx.readers.mars_reader'):
    sys.modules[name] = types.ModuleType(name)
class MarsReader:
    def __init__(self, **kwargs): pass
    def read(self, *, site, query):
        assert site == 'KM'
        assert 'select' in query.lower()
        return pandas.DataFrame({'VALUE': [42]})
sys.modules['datasyncx.readers.mars_reader'].MarsReader = MarsReader
sys.modules['datasyncx'].MarsReader = MarsReader
driver = types.ModuleType('oracledb')
driver.is_thin_mode = lambda: True
sys.modules['oracledb'] = driver
runpy.run_path(sys.argv[1], run_name='__main__')
""",
    )
    text = (
        "<OPTIONS>\n/NODE=KM.MARS\n/ENGINE=VA\n/CSV=result.csv\n</OPTIONS>\n"
        "select 42 as VALUE from dual\n"
    )
    run_generated(tmp_path, text, runner=runner)
    with (tmp_path / "result.csv").open(newline="", encoding="utf-8") as stream:
        assert list(csv.reader(stream)) == [["value"], ["42"]]


def test_uncalled_runtime_methods_execute_in_standalone_output(tmp_path):
    # The calls below are deliberately added after assembly: they cannot influence
    # dependency roots or rescue methods that the embedding pass incorrectly prunes.
    embedded = assemble_utilities(
        step_emissions=(),
        workflow_source=(
            "def run(ctx):\n"
            "    ctx.macro.named('NAME')\n"
            "    ctx.fs_ops.write_file('unused', '')\n"
            "    ctx.csv_io.row_count('unused')\n"
        ),
        reader_names=set(),
    )
    assertions = """
macro = MacroState()
macro.set_named('NAME', 'Ada')
with macro.scope({'NAME': 'Bob'}):
    assert macro.substitute('Hello <<<NAME>>>') == 'Hello Bob'
assert macro.named('NAME') == 'Ada'
fs = FileSystemOps()
fs.write_file('original.txt', 'hello')
fs.copy('original.txt', 'copy.txt')
fs.rename('copy.txt', 'renamed.txt')
assert Path('renamed.txt').read_text() == 'hello'
fs.delete(['original.txt', 'renamed.txt'])
assert not Path('original.txt').exists() and not Path('renamed.txt').exists()
csv_io = CsvIO()
csv_io.write('rows.csv', [{'name': 'Ada'}, {'name': 'Bob'}])
assert list(csv_io.iter('rows.csv')) == [{'name': 'Ada'}, {'name': 'Bob'}]
csv_io.write('single.csv', [{'name': 'Ada'}])
assert csv_io.single_row('single.csv') == {'name': 'Ada'}
chunks = [Path(path).read_text() for path in csv_io.iter_chunks('rows.csv', 'chunk.csv', 1)]
assert chunks == ['name\\nAda\\n', 'name\\nBob\\n']
"""
    output = tmp_path / "runtime.py"
    output.write_text(
        "\n\n".join([*embedded.imports, *embedded.sources, assertions]), encoding="utf-8"
    )
    process = subprocess.run(
        [sys.executable, "-I", "-c", RUNNER, str(output)],
        cwd=tmp_path,
        capture_output=True,
        text=True,
        timeout=30,
    )
    assert process.returncode == 0, process.stdout + process.stderr
