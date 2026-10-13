"""Execute the actual generated artifact without access to vg2c imports."""

from __future__ import annotations

import csv
import os
import shutil
import subprocess
import sys
from pathlib import Path

from vg2c.compilation import compile_document
from vg2c import translate
import pytest

RUNNER = """
import importlib.abc, runpy, sys
class NoCompiler(importlib.abc.MetaPathFinder):
    def find_spec(self, fullname, path=None, target=None):
        if fullname.startswith('vg2c.') and not fullname.startswith('vg2c.runtime'):
            raise AssertionError('Generated script imported compiler: ' + fullname)
sys.meta_path.insert(0, NoCompiler())
runpy.run_path(sys.argv[1], run_name='generated_job')['run']()
"""


def run_generated(tmp_path, text, *, runner=RUNNER):
    path = tmp_path / "workflow.txt"
    path.write_text(text, encoding="utf-8")
    result = compile_document(path)
    output = translate(path)
    path.unlink()
    workdir = output.parent / "output"
    workdir.mkdir()
    for input_file in tmp_path.iterdir():
        if input_file.is_file():
            shutil.copyfile(input_file, workdir / input_file.name)
    unrelated = tmp_path / "unrelated"
    unrelated.mkdir()
    env = os.environ.copy()
    env.pop("PYTHONPATH", None)
    env["DATASYNCX_ORACLE_CLIENT"] = "home"
    process = subprocess.run(
        [sys.executable, "-I", "-c", runner, str(output)],
        cwd=unrelated,
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
    with (tmp_path / "workflow" / "output" / "owner.csv").open(newline="", encoding="utf-8") as stream:
        assert list(csv.reader(stream)) == [["owner"], ["Ada"], ["Bob"]]
    assert "class SqliteEngine" not in result.emitted.source
    assert "def check(" not in result.emitted.source


def test_smart_append_generated_workflow_executes_twice_without_duplicate_header(tmp_path):
    (tmp_path / "source.csv").write_text("id,value\n1,first\n", encoding="utf-8")
    block = '<OPTIONS>\n/UTILITIES=SmartAppend.va "dest.csv" "source.csv"\n</OPTIONS>\n'
    result = run_generated(tmp_path, block + "<---- New Query ---->\n" + block)
    with (tmp_path / "workflow" / "output" / "dest.csv").open(newline="", encoding="utf-8") as stream:
        assert list(csv.reader(stream)) == [["id", "value"], ["1", "first"], ["1", "first"]]
    assert "class SmartAppend:" not in result.emitted.source
    assert result.emitted.source.count("smart_append(") == 2


def test_embedded_python_uses_explicit_runtime_and_local_values(tmp_path):
    text = """<OPTIONS>
/WRITE-FILE=Y
/CSV=code.py
</OPTIONS>
from vg2c.runtime import write_file, copy_file
macros = {'NAME': 'Bob'}
write_file('hello.txt', 'Hello <<<NAME>>>', workdir=workdir, macros=macros)
copy_file('hello.txt', 'copied.txt', workdir=workdir)
"""
    result = run_generated(tmp_path, text)
    assert (tmp_path / "workflow" / "output" / "copied.txt").read_text() == "Hello Bob"
    assert "job_entrypoint" not in result.emitted.source
    assert "def run():" in result.emitted.source
    assert "job = JobRuntime(" in result.emitted.source
    assert "ctx" not in result.emitted.source


def test_retired_context_in_embedded_python_has_explicit_diagnostic(tmp_path):
    path = tmp_path / "legacy.txt"
    path.write_text("<OPTIONS>\n/WRITE-FILE=Y\n/CSV=code.py\n</OPTIONS>\nctx.write_file('x', 'y')\n")
    with pytest.raises(ValueError, match="retired ctx API"):
        compile_document(path)


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
                if span is None:
                    assert parameter.definition is not None
                    assert not parameter.definition.required or not parameter.editable
                    continue
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
    text = ("<OPTIONS>\n/REPORT=HTML-DEFER\n/ID=REPORT1\n</OPTIONS>\n"
            + report + "<---- New Query ---->\n"
            + "<OPTIONS>\n/REPORT=HTML-LAYOUT\n</OPTIONS>\n" + layout)
    result = run_generated(tmp_path, text)
    content = (tmp_path / "workflow" / "output" / "report.html").read_text(encoding="utf-8")
    assert "Ada" in content and "85.00%" in content
    assert "<title>Generated report</title>" in content
    assert "job.html(" in result.emitted.source
    assert "ctx" not in result.emitted.source
    assert "def emit_block" not in result.emitted.source


def test_generated_datasyncx_reader_with_controlled_double(tmp_path):
    # The generated source uses its normal DataSyncX import and runtime reader path.
    runner = RUNNER.replace(
        "runpy.run_path(sys.argv[1], run_name='generated_job')['run']()",
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
runpy.run_path(sys.argv[1], run_name='generated_job')['run']()
""",
    )
    text = (
        "<OPTIONS>\n/NODE=KM.MARS\n/ENGINE=VA\n/CSV=result.csv\n</OPTIONS>\n"
        "select 42 as VALUE from dual\n"
    )
    run_generated(tmp_path, text, runner=runner)
    with (tmp_path / "workflow" / "output" / "result.csv").open(newline="", encoding="utf-8") as stream:
        assert list(csv.reader(stream)) == [["value"], ["42"]]


