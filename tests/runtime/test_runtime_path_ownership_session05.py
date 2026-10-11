"""Regression checks for the compiler-independent runtime import boundary."""

from pathlib import Path
import subprocess
import sys

from vg2c.runtime.values import resolve_path, strip_quotes


def test_resolve_path_preserves_workdir_and_legacy_cwd_fallback(tmp_path, monkeypatch):
    workdir = tmp_path / "output"
    workdir.mkdir()
    local = tmp_path / "data.csv"
    local.write_text("id\n1\n")
    monkeypatch.chdir(tmp_path)

    assert resolve_path("data.csv", workdir=workdir) == workdir / "data.csv"
    assert resolve_path("data.csv", for_write=True, workdir=workdir) == workdir / "data.csv"
    assert resolve_path(local) == local
    assert resolve_path("data.csv") == Path("data.csv")
    assert resolve_path("missing.csv") == tmp_path / "missing.csv"
    assert resolve_path("missing.csv", for_write=True) == tmp_path / "missing.csv"
    assert resolve_path(tmp_path / "not-there" / "data.csv") == Path("data.csv")
    assert strip_quotes(' "name" ') == "name"
    assert strip_quotes("'name'") == "name"


def test_importing_runtime_does_not_load_compiler_utilities():
    code = (
        "import sys; import vg2c.runtime; "
        "assert not any(name.startswith(('vg2c.utilities', 'vg2c.frontend', "
        "'vg2c.emitter', 'vg2c.compilation', 'vg2c.resolver')) "
        "for name in sys.modules), sorted(sys.modules)"
    )
    result = subprocess.run([sys.executable, "-I", "-c", code],
                            capture_output=True, text=True, check=False)
    assert result.returncode == 0, result.stderr
