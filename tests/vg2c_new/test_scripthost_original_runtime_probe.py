from __future__ import annotations

import importlib
import subprocess
import sys
import threading
import zipfile
from pathlib import Path

import pytest

from vg2c_new.parser import parse
from vg2c_new.runtime import Interpreter, RuntimeState
from vg2c_new.utilities.file_values import RowsInFileUtility
from vg2c_new.utilities.files import WriteFileUtility

SCRIPT_HOST_ROOT = Path(__file__).resolve().parents[2] / "scripthost-utilities-decompiled"
EXTRACTED = SCRIPT_HOST_ROOT / "SPSQL3_py"
ARCHIVE = SCRIPT_HOST_ROOT / "SPSQL3_py.zip"


def _with_extracted_runtime() -> None:
    value = str(EXTRACTED)
    if value not in sys.path:
        sys.path.insert(0, value)


def test_original_archive_runtime_inventory() -> None:
    with zipfile.ZipFile(ARCHIVE) as archive:
        names = archive.namelist()

    dbdriver_entries = [name for name in names if "dbdriver" in name.lower()]
    print("dbdriver entries:", dbdriver_entries)
    print("archive python files:", sum(name.lower().endswith(".py") for name in names))
    assert names


def test_spfglobals_imports_on_linux() -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFGlobals")
    assert module.SPFGlobals is not None


def test_original_spfmanager_imports_on_linux() -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    assert module.SPFManager is not None


def test_windows_db_transport_fails_only_when_invoked_on_linux() -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    if sys.platform == "win32" or module.dbDrivers is not None:
        pytest.skip("Windows ScriptHost dbDrivers are available on this host.")
    with pytest.raises(RuntimeError, match="dbDrivers are unavailable"):
        module.dbDriverBase()


DELIM = "<---- New Query ---->"


def _block(*options: str, body: str = "") -> str:
    return "<OPTIONS>\n" + "\n".join(options) + "\n</OPTIONS>\n" + body


def _script(*blocks: str) -> str:
    return ("\n" + DELIM + "\n").join(blocks)


def _representative_script() -> str:
    return _script(
        _block("/WRITE-FILE=Y", "/CSV=seed.txt", body="seed<EOF>ignored"),
        _block('/UTILITIES={ROWS-IN-FILE} "macro.csv" "MACRO_ROWS" "N"'),
        _block('/UTILITIES={IF-THEN} "MACRO_ROWS" "EQ" "2"'),
        _block("/WRITE-FILE=Y", "/CSV=count_ok.txt", body="count ok"),
        _block("/UTILITIES={ELSE}"),
        _block("/WRITE-FILE=Y", "/CSV=count_bad.txt", body="wrong count"),
        _block("/UTILITIES={END-IF}"),
        _block('/UTILITIES={START-MACRO} "macro.csv" "N"'),
        _block('/UTILITIES={IF-THEN} "VAR(<<<flag>>>)" "GT" "0"'),
        _block('/UTILITIES={FOR-LOOP} "0" "2" "1" "x" "N"'),
        _block(
            "/WRITE-FILE=Y",
            "/CSV=out_<<<name>>>_<<<spf-loop-ctr-x-int>>>.txt",
            body="<<<name>>>:<<<spf-loop-ctr-x-int>>>",
        ),
        _block("/UTILITIES={END-LOOP}"),
        _block("/UTILITIES={ELSE}"),
        _block("/WRITE-FILE=Y", "/CSV=bad.txt", body="wrong branch"),
        _block("/UTILITIES={END-IF}"),
        _block("/UTILITIES={END-MACRO}"),
    )


def test_original_run_spfsql_vertical_slice_on_linux(tmp_path, monkeypatch) -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    monkeypatch.chdir(tmp_path)
    (tmp_path / "macro.csv").write_text("name,flag\nalpha,1\nbeta,0\n", encoding="utf-8")

    manager = module.SPFManager()
    manager.gCommandLineArguments = [
        "scripthost-probe",
        f"/MYLOCAL={tmp_path}",
        f"/EXEDIR={tmp_path}",
    ]
    manager.MySPFSQLFileData = _representative_script()

    assert manager.Run_SPFSQL() is True
    assert manager.Run_SPFSQL() is True
    assert (tmp_path / "count_ok.txt").exists()
    assert not (tmp_path / "count_bad.txt").exists()
    assert not (tmp_path / "bad.txt").exists()
    assert [
        (tmp_path / f"out_alpha_{index}.txt").read_text(encoding="utf-8") for index in range(3)
    ] == ["alpha:0\n", "alpha:1\n", "alpha:2\n"]


RUNNER = Path(__file__).resolve().parents[2] / "tools" / "scripthost_portable_runner.py"


def _write_process_fixture(root: Path, label: str) -> tuple[Path, Path]:
    root.mkdir(parents=True, exist_ok=True)
    script = root / f"{label}.spfsql"
    script.write_text(_representative_script(), encoding="utf-8")
    (root / "macro.csv").write_text("name,flag\nalpha,1\nbeta,0\n", encoding="utf-8")
    return script, root


def test_original_runtime_repeats_cleanly_in_separate_processes(tmp_path) -> None:
    first_script, first_workdir = _write_process_fixture(tmp_path / "first", "first")
    second_script, second_workdir = _write_process_fixture(tmp_path / "second", "second")

    for script, workdir in ((first_script, first_workdir), (second_script, second_workdir)):
        result = subprocess.run(
            [sys.executable, str(RUNNER), str(script), "--workdir", str(workdir)],
            cwd=Path(__file__).resolve().parents[2],
            check=False,
            capture_output=True,
            text=True,
        )
        assert result.returncode == 0, result.stderr
        assert (workdir / "out_alpha_2.txt").read_text(encoding="utf-8") == "alpha:2\n"
        assert not (workdir / "bad.txt").exists()


def test_original_runtime_runs_concurrently_in_separate_processes(tmp_path) -> None:
    fixtures = [
        _write_process_fixture(tmp_path / "parallel-a", "parallel-a"),
        _write_process_fixture(tmp_path / "parallel-b", "parallel-b"),
    ]
    processes = [
        subprocess.Popen(
            [sys.executable, str(RUNNER), str(script), "--workdir", str(workdir)],
            cwd=Path(__file__).resolve().parents[2],
            stdout=subprocess.PIPE,
            stderr=subprocess.PIPE,
            text=True,
        )
        for script, workdir in fixtures
    ]
    results = [process.communicate(timeout=30) for process in processes]

    for process, (stdout, stderr), (_, workdir) in zip(processes, results, fixtures, strict=True):
        assert process.returncode == 0, f"stdout={stdout}\nstderr={stderr}"
        assert (workdir / "out_alpha_1.txt").read_text(encoding="utf-8") == "alpha:1\n"
        assert not (workdir / "bad.txt").exists()


def test_original_globals_retain_some_state_between_in_process_runs() -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    manager = module.SPFManager()

    manager.gCommandLineArguments = ["probe", "/SPFINSTANCE=FIRST"]
    assert manager.gSPFInstance == "FIRST"

    manager.gCommandLineArguments = ["probe", "/SPFINSTANCE=SECOND"]
    assert manager.gSPFInstance == "FIRST"


def test_spfglobals_are_shared_between_manager_instances() -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    first = module.SPFManager()
    second = module.SPFManager()
    original = first.gAnyLoop
    try:
        first.gAnyLoop = "YS"
        assert second.gAnyLoop == "YS"
    finally:
        first.gAnyLoop = original


def test_spfglobals_overlap_across_threads_in_one_process() -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    first = module.SPFManager()
    second = module.SPFManager()
    original = first.gAnyLoop
    first_written = threading.Event()
    second_written = threading.Event()
    observed: list[str] = []

    def run_first() -> None:
        first.gAnyLoop = "YS"
        first_written.set()
        assert second_written.wait(timeout=5)
        observed.append(first.gAnyLoop)

    def run_second() -> None:
        assert first_written.wait(timeout=5)
        second.gAnyLoop = "YR"
        second_written.set()

    try:
        a = threading.Thread(target=run_first)
        b = threading.Thread(target=run_second)
        a.start()
        b.start()
        a.join(timeout=5)
        b.join(timeout=5)
        assert not a.is_alive()
        assert not b.is_alive()
        assert observed == ["YR"]
    finally:
        first.gAnyLoop = original


def test_original_parser_builds_repository_actual_script_task_tree(tmp_path) -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    manager = module.SPFManager()
    manager.gCommandLineArguments = [
        "scripthost-parser-probe",
        f"/MYLOCAL={tmp_path}",
        f"/EXEDIR={tmp_path}",
    ]
    script = (Path(__file__).resolve().parents[1] / "fixtures" / "actual_script.txt").read_text(
        encoding="utf-8"
    )
    blocks = script.split(manager.SQLFILE_DELIM)
    tasks = manager.Process_Query(
        0,
        len(blocks),
        list(blocks),
        str(tmp_path),
        str(tmp_path),
        None,
        len(blocks),
        "1",
        "tmp",
        None,
        False,
    )

    def task_names(items: list[object]) -> list[str]:
        names: list[str] = []
        for item in items:
            names.append(type(item).__name__)
            names.extend(task_names(getattr(item, "childTasksList", [])))
        return names

    names = task_names(tasks)
    assert "HTMLRunTask" in names
    assert "HTMLLayoutTask" in names
    assert "StartMacroTask" in names
    assert "RowsInFileTask" in names
    assert len(names) >= 40


def test_original_html_css_report_lifecycle_on_linux(tmp_path, monkeypatch) -> None:
    _with_extracted_runtime()
    module = importlib.import_module("SPFLib.SPFSQL3")
    monkeypatch.chdir(tmp_path)
    source = (Path(__file__).resolve().parents[1] / "fixtures" / "actual_script.txt").read_text(
        encoding="utf-8"
    )
    report_block = next(block for block in source.split(DELIM) if "/REPORT=HTML-RUN" in block)

    manager = module.SPFManager()
    manager.gCommandLineArguments = [
        "scripthost-report-probe",
        f"/MYLOCAL={tmp_path}",
        f"/EXEDIR={tmp_path}",
    ]
    manager.MySPFSQLFileData = report_block

    assert manager.Run_SPFSQL() is True
    stylesheet = tmp_path / "sqlpathfinder_style_1.css"
    assert stylesheet.exists()
    assert "background-color:#dbd9c0" in stylesheet.read_text(encoding="utf-8", errors="replace")
    assert not list(tmp_path.glob("*_tmp_.ini"))


def test_representative_slice_matches_vg2c_new_outputs(tmp_path) -> None:
    original_dir = tmp_path / "original"
    vg2c_dir = tmp_path / "vg2c-new"
    original_script, _ = _write_process_fixture(original_dir, "representative")
    vg2c_dir.mkdir()
    (vg2c_dir / "macro.csv").write_text("name,flag\nalpha,1\nbeta,0\n", encoding="utf-8")

    original = subprocess.run(
        [sys.executable, str(RUNNER), str(original_script), "--workdir", str(original_dir)],
        cwd=Path(__file__).resolve().parents[2],
        check=False,
        capture_output=True,
        text=True,
    )
    assert original.returncode == 0, original.stderr

    commands = parse(_representative_script())
    Interpreter({"write_file": WriteFileUtility(), "rows_in_file": RowsInFileUtility()}).execute(
        commands, RuntimeState(vg2c_dir)
    )

    def outputs(root: Path) -> dict[str, str]:
        return {
            path.name: path.read_text(encoding="utf-8")
            for path in sorted(root.glob("*.txt"))
            if path.name != "bad.txt"
        }

    original_outputs = outputs(original_dir)
    vg2c_outputs = outputs(vg2c_dir)

    assert not (original_dir / "bad.txt").exists()
    assert not (vg2c_dir / "bad.txt").exists()
    assert original_outputs.keys() == vg2c_outputs.keys()
    assert {name: value.rstrip("\n") for name, value in original_outputs.items()} == vg2c_outputs
    assert original_outputs["out_alpha_0.txt"] == "alpha:0\n"
    assert vg2c_outputs["out_alpha_0.txt"] == "alpha:0"
