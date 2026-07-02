from __future__ import annotations

import ast
from pathlib import Path

import pytest

from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.dispatch.models import DispatchConfig
from vg2c.emitter import emit
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve

FIXTURE_NAMES = [
    "script_short.txt",
    "script_another.txt",
    "sql_script.txt",
    "actual_script.txt",
]


def _run_full_pipeline(fixtures: Path, file_name: str):
    """Run all stages 1-5 on a fixture."""
    text = (fixtures / file_name).read_text(encoding="utf-8", errors="replace")
    parsed, pdiag = parse(text, source=fixtures / file_name)
    classified, cdiag = classify(parsed)
    resolved = resolve(classified, diagnostics=[*pdiag, *cdiag])
    analyzed = analyze(resolved)
    dispatched = dispatch(analyzed, config=DispatchConfig(oasys_schema="SCHEMA"))
    emitted = emit(dispatched)
    return emitted


# --- Hard contract tests ---


@pytest.mark.parametrize("fixture_name", FIXTURE_NAMES)
def test_emitted_script_is_valid_python(FIXTURES: Path, fixture_name: str) -> None:
    """Every emitted script must parse cleanly as Python."""
    emitted = _run_full_pipeline(FIXTURES, fixture_name)
    try:
        ast.parse(emitted.source)
    except SyntaxError as e:
        pytest.fail(
            f"Emitted script has syntax error at line {e.lineno}: {e.msg}\n{emitted.source}"
        )


@pytest.mark.parametrize("fixture_name", FIXTURE_NAMES)
def test_emitted_script_has_run_function(FIXTURES: Path, fixture_name: str) -> None:
    """Every emitted script must have a def run() -> None:."""
    emitted = _run_full_pipeline(FIXTURES, fixture_name)
    assert "def run() -> None:" in emitted.source


@pytest.mark.parametrize("fixture_name", FIXTURE_NAMES)
def test_emitted_script_has_main_entry(FIXTURES: Path, fixture_name: str) -> None:
    """Every emitted script must have if __name__ == "__main__":."""
    emitted = _run_full_pipeline(FIXTURES, fixture_name)
    assert 'if __name__ == "__main__":' in emitted.source


# --- Sanity checks ---


def test_script_short_emitted_is_minimal(FIXTURES: Path) -> None:
    """script_short has one SQLite block; should emit one step function."""
    emitted = _run_full_pipeline(FIXTURES, "script_short.txt")
    assert "step_" in emitted.source
    assert "def run() -> None:" in emitted.source


def test_sql_script_emitted_has_multiple_steps(FIXTURES: Path) -> None:
    """sql_script has multiple SQL blocks; should emit multiple step functions."""
    emitted = _run_full_pipeline(FIXTURES, "sql_script.txt")
    step_count = emitted.source.count("def step_")
    assert step_count >= 2  # At least MARS, OASYS, SQLite
    assert "ctx.run_query(" in emitted.source
    assert "class ReaderRuntime" in emitted.source


def test_actual_script_emitted_has_imports(FIXTURES: Path) -> None:
    """Emitted script should embed utilities, not import from vg2c_runtime."""
    emitted = _run_full_pipeline(FIXTURES, "actual_script.txt")
    assert "vg2c_runtime" not in emitted.source
    assert "class PipelineContext" in emitted.source


def test_actual_script_uses_declared_output_paths(FIXTURES: Path) -> None:
    """Known producer outputs should preserve declared /CSV paths, not step_*."""
    emitted = _run_full_pipeline(FIXTURES, "actual_script.txt")
    source = emitted.source

    # Ensure known declared producer outputs are preserved.
    assert "yeuchuan_a0_15507.tab" in source
    assert "yeuchuan_SQL_15507.tab" in source
    assert "macrotmp.csv" in source


def test_actual_script_rows_in_file_and_macro_names(FIXTURES: Path) -> None:
    """ROWS-IN-FILE should emit variable assignment and macro names should be normalized."""
    emitted = _run_full_pipeline(FIXTURES, "actual_script.txt")
    source = emitted.source

    # ROWS-IN-FILE blocks should define macro variables via runtime context.
    assert (
        "ctx.macro.set_named('CONFIG', str(ctx.csv_io.row_count('ICMPCS_config.csv')))"
        in source
    )

    # Macro lookups should not include placeholder delimiters.
    assert 'ctx.macro.named("<<<' not in source
