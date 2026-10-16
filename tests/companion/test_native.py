"""Exercise the real native helper against our own offscreen WinForms process."""

from __future__ import annotations

import os
import subprocess
import time
from pathlib import Path
from types import SimpleNamespace

import pytest

from vg2c_companion.observer import ObserverClient, _find_compiler, _hidden_flags

pytestmark = pytest.mark.skipif(os.name != "nt", reason="Windows native UI integration")
SOURCE = "<OPTIONS>\r\n/OLEDB=SQLite\r\n/CSV=out.csv\r\n</OPTIONS>\r\nSELECT 1\r\n"


@pytest.fixture(scope="module")
def native_tools(tmp_path_factory):
    directory = tmp_path_factory.mktemp("companion-native")
    compiler = _find_compiler(Path(os.environ.get("WINDIR", r"C:\Windows")))
    if compiler is None:
        pytest.skip(".NET Framework csc.exe is unavailable")
    executable = directory / "sqlpathfinder3.exe"
    fixture = Path(__file__).parent / "fixtures" / "native_designer.cs"
    result = subprocess.run(
        [str(compiler), "/nologo", "/target:winexe", f"/out:{executable}",
         "/reference:System.Windows.Forms.dll", "/reference:System.Drawing.dll", str(fixture)],
        capture_output=True, text=True, timeout=30, creationflags=_hidden_flags(),
    )
    assert result.returncode == 0, result.stdout + result.stderr
    client = ObserverClient(directory / "helper")
    client.ensure_helper()
    yield executable, client
    client.close()


@pytest.fixture
def start_designer(native_tools, tmp_path):
    executable, client = native_tools
    processes = []

    def start(name="sample.vg2", source=SOURCE, mode="normal"):
        source_file = tmp_path / f"source-{len(processes)}.txt"
        source_file.write_text(source, encoding="utf-8", newline="")
        log = tmp_path / f"events-{len(processes)}.txt"
        process = subprocess.Popen(
            [str(executable), name, str(source_file), str(log), mode],
            creationflags=_hidden_flags(),
        )
        processes.append(process)
        deadline = time.monotonic() + 5
        while time.monotonic() < deadline:
            assert process.poll() is None, "Owned WinForms test process exited"
            targets = tuple(target for target in client.list_targets() if target.target_pid == process.pid)
            events = log.read_text().splitlines() if log.exists() else []
            if targets and "ready" in events and (mode in ("normal", "unowned", "tree", "nested-tree") or "created" in events):
                return client, targets, log, source_file
            time.sleep(0.05)
        pytest.fail(f"Owned WinForms designer did not appear: {client.last_target_error}")

    yield start
    for process in processes:
        if process.poll() is None:
            process.kill()
        process.wait(timeout=5)


@pytest.mark.parametrize("mode", ["normal", "unowned"], ids=["owned", "unowned"])
def test_repeated_refresh_captures_fresh_source_and_closes_editor(start_designer, mode):
    client, targets, log, _ = start_designer(name="file (copy) [1].vg2", mode=mode)
    target, = targets
    assert target.document_name == "file (copy) [1].vg2"
    for _ in range(3):
        result = client.refresh(target.target_pid, target.designer_hwnd, target.document_name)
        assert result.status == "ok", result.error
        assert result.source_text == SOURCE
        assert client.poll(target.target_pid, target.designer_hwnd).source_text == ""
    assert log.read_text().splitlines() == ["ready"] + ["created", "closed"] * 3


def test_existing_editor_and_wrong_target_return_actionable_errors(start_designer):
    client, targets, log, _ = start_designer(mode="existing")
    target, = targets
    result = client.refresh(target.target_pid, target.designer_hwnd, target.document_name)
    assert result.status == "editor-open"
    assert "Close the existing" in result.error
    assert log.read_text().splitlines() == ["ready", "created"]
    for hwnd, name in ((1, target.document_name), (target.designer_hwnd, "other" + target.document_name)):
        assert client.refresh(target.target_pid, hwnd, name).status == "ambiguous"


@pytest.mark.parametrize("source,status", [("", "empty"), ("x" * 200_001, "error")], ids=["empty", "oversized"])
def test_failed_capture_closes_only_its_new_editor(start_designer, source, status):
    client, targets, log, _ = start_designer(source=source)
    target, = targets
    result = client.refresh(target.target_pid, target.designer_hwnd, target.document_name)
    assert result.status == status, result.error
    assert not result.source_text
    assert log.read_text().splitlines() == ["ready", "created", "closed"]


def test_passive_observation_does_not_read_another_designers_editor(start_designer):
    client, targets, _, _ = start_designer(mode="other-editor")
    main = next(target for target in targets if target.document_name == "sample.vg2")
    other = next(target for target in targets if target.document_name == "other.vg2")
    assert client.poll(main.target_pid, main.designer_hwnd).source_text == ""
    assert client.poll(other.target_pid, other.designer_hwnd).source_text == SOURCE


@pytest.mark.parametrize("mode", ["tree", "nested-tree"])
def test_passive_observation_tracks_selected_designer_tree_leaf(start_designer, mode):
    client, targets, log, _ = start_designer(mode=mode)
    target, = targets
    seen = set()
    first_fingerprint = client.poll(target.target_pid, target.designer_hwnd).designer_fingerprint

    def labels():
        return {
            value.split("=", 1)[0].removesuffix(" focused").strip()
            for value in client.poll(target.target_pid, target.designer_hwnd).designer_values
        }

    deadline = time.monotonic() + 4
    while time.monotonic() < deadline and "15.0. Text (SQLite)" not in seen:
        seen.update(labels())
        time.sleep(0.05)
    assert "15.0. Text (SQLite)" in seen
    Path(str(log) + ".next").write_text("next")
    deadline = time.monotonic() + 4
    while time.monotonic() < deadline and "15.1. Next step" not in seen:
        seen.update(labels())
        time.sleep(0.05)
    assert "15.1. Next step" in seen
    snapshot = client.poll(target.target_pid, target.designer_hwnd)
    assert snapshot.designer_fingerprint == first_fingerprint
    assert all("inactive_parent" not in value for value in snapshot.designer_values)


def test_companion_autotranslates_and_update_pulls_new_native_source(start_designer, monkeypatch):
    tk = pytest.importorskip("tkinter")
    try:
        root = tk.Tk()
    except tk.TclError:
        pytest.skip("Tk display is unavailable")
    root.withdraw()
    client, targets, log, source_file = start_designer()
    target, = targets
    from vg2c_companion.app import CompanionApp

    # Restrict automatic discovery/actions to the process this test owns.
    monkeypatch.setattr("vg2c_companion.app.ObserverClient", lambda: SimpleNamespace(
        list_targets=lambda: tuple(item for item in client.list_targets() if item.target_pid == target.target_pid),
        last_target_error=None, refresh=client.refresh,
    ))
    monkeypatch.setattr("vg2c_companion.app.ObserverWorker", lambda *_args: SimpleNamespace(
        drain=lambda: None, close=lambda: None, select_target=lambda *_args: None,
        invalidate_frames=lambda: None,
    ))
    app = CompanionApp(root)

    def wait_until_source(expected):
        deadline = time.monotonic() + 10
        while app.compiled_source_text != expected and time.monotonic() < deadline:
            root.update()
            time.sleep(0.01)
        assert app.compiled_source_text == expected, app.diagnostics.get("1.0", "end")

    try:
        wait_until_source(SOURCE)
        assert app.tree.exists("asset:vg2c/runtime/job.py")
        assert not app._refresh_button.instate(["disabled"])
        latest = SOURCE.replace("SELECT 1", "SELECT 2")
        source_file.write_text(latest, encoding="utf-8", newline="")
        app._refresh_button.invoke()
        wait_until_source(latest)
        assert any("SELECT 2" in value for name, value in app.current_assets.items() if name.endswith(".sql"))
        assert log.read_text().splitlines() == ["ready", "created", "closed", "created", "closed"]
    finally:
        app.close()
