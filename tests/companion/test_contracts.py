from __future__ import annotations

import json
import os
import queue
import subprocess
import threading
import time
from pathlib import Path
from types import SimpleNamespace

import pytest

from vg2c.emitter.models import SourceRange
from vg2c.frontend.models import SourceSpan
from vg2c_companion.compiler import CompilationWorker, compile_source
from vg2c_companion.export import export_preview, save_copy
from vg2c_companion.models import EditCopy, ObserverSnapshot, Selection
from vg2c_companion.native import inspect_native
from vg2c_companion.observer import NativeTarget, _find_compiler, _helper_build_command, parse_refresh, parse_snapshot, parse_targets
from vg2c_companion.source import (
    SourceMapping,
    map_designer_label,
    map_source_offset,
    python_to_utf16_offset,
    utf16_to_python_offset,
)


def _fake_result():
    blocks = [
        SimpleNamespace(index=0, span=SourceSpan(None, 1, 1)),
        SimpleNamespace(index=1, span=SourceSpan(None, 2, 2)),
    ]
    steps = {
        0: SimpleNamespace(
            source_range=SourceRange(10, 20),
            parameters=(SimpleNamespace(artifact_role=object(), value="sql/query.sql"),),
        ),
        1: SimpleNamespace(source_range=SourceRange(30, 40), parameters=()),
    }
    emitted = SimpleNamespace(
        assets=(("sql/query.sql", "SELECT 1\n"),),
        step_for_block=lambda index: steps.get(index),
    )
    return SimpleNamespace(dispatched=SimpleNamespace(resolved=SimpleNamespace(blocks=blocks)), emitted=emitted)


def test_compile_valid_synthetic_source_and_export(tmp_path: Path):
    source = "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT 1\n"
    result = compile_source(source, "synthetic.txt")
    destination = export_preview(result, tmp_path / "compiled")
    assert (destination / "main.py").is_file()


def test_utf16_offsets_and_duplicate_source_lines_map_to_distinct_steps():
    text = "same 😀\nsame 😀\n"
    first = map_source_offset(_fake_result(), text, text.index("😀"))
    second_offset = text.index("😀", text.index("😀") + 1)
    second = map_source_offset(_fake_result(), text, second_offset)
    assert first.block_index == 0
    assert second.block_index == 1
    assert first.generated_range == (10, 20)
    assert first.asset_names == ("sql/query.sql",)
    assert utf16_to_python_offset("abc\n", 0) == 0
    assert utf16_to_python_offset("abc\n", 4) == 4
    separator = "first\n<---- New Query ---->\nsecond\n"
    assert map_source_offset(_fake_result(), separator, separator.index("New Query")).block_index is None
    assert python_to_utf16_offset("😀x", 1) == 2
    assert utf16_to_python_offset("😀x", 2) == 1


def test_dirty_edit_copy_marks_new_source_revision_without_overwrite():
    copy = EditCopy(source_identity=1, text="original generated source")
    copy.source_refreshed(2)
    assert copy.text == "original generated source"
    assert copy.dirty
    assert copy.pending_identity == 2


def test_export_accepts_empty_directory_and_rejects_nonempty_or_traversal(tmp_path: Path):
    result = SimpleNamespace(
        emitted=SimpleNamespace(source="print('preview')\n", assets=(("sql/query.sql", "SELECT 1\n"),))
    )
    destination = tmp_path / "export"
    destination.mkdir()
    assert export_preview(result, destination) == destination
    assert (destination / "main.py").read_text() == "print('preview')\n"
    with pytest.raises(FileExistsError):
        export_preview(result, destination)
    unsafe = SimpleNamespace(emitted=SimpleNamespace(source="", assets=(("../escape.sql", "x"),)))
    with pytest.raises(ValueError):
        export_preview(unsafe, tmp_path / "unsafe")
    aliases = SimpleNamespace(emitted=SimpleNamespace(source="", assets=(("A.sql", "x"), ("a.SQL", "y"))))
    with pytest.raises(ValueError):
        export_preview(aliases, tmp_path / "aliases")
    original = tmp_path / "original.txt"
    original.write_text("source")
    with pytest.raises(ValueError):
        save_copy(original, "changed", (original,))
    copy_path = tmp_path / "copy.txt"
    assert save_copy(copy_path, "copy") == copy_path
    with pytest.raises(FileExistsError):
        save_copy(copy_path, "second")


def test_observer_build_command_uses_framework_root_and_msaa_refs(tmp_path: Path):
    windows = tmp_path / "Windows"
    compiler = windows / "Microsoft.NET" / "Framework64" / "v4.0.30319" / "csc.exe"
    compiler.parent.mkdir(parents=True)
    compiler.write_text("")
    for name in ("Accessibility.dll", "System.Runtime.Serialization.dll"):
        (compiler.parent / name).write_text("")
    found = _find_compiler(windows)
    assert found == compiler
    command = _helper_build_command(compiler, tmp_path / "observer.exe", tmp_path / "observer.cs")
    assert str(compiler) == command[0]
    assert any(item.endswith("Accessibility.dll") for item in command)
    assert not any("UIAutomation" in item or "WindowsBase" in item for item in command)
    assert not any(item.endswith("csc.exe\\csc.exe") for item in command)


def test_native_snapshot_reads_bom_and_quoted_csv(tmp_path: Path):
    native = tmp_path / "sample.VG2"
    native.write_text('\ufeffVERSION:,"7.1"\nTABLEGRID,"1. Main Query","a,b"\nQUERYGRID,"x,y"\n', encoding="utf-8")
    snapshot = inspect_native(native)
    assert snapshot.version == "7.1"
    assert snapshot.sections == ("TABLEGRID", "QUERYGRID")


def test_main_query_mapping_requires_exact_context_and_unique_query_step():
    source = "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT 1\n"
    result = compile_source(source, "main-query.txt")
    query_steps = tuple(
        step for step in result.emitted.steps
        if str(getattr(step, "functional_kind", "")).lower() in {
            "sql_query", "sql-query", "sqlite_query", "sqlite-query"
        }
    )
    assert len(query_steps) == 1
    mapped = map_designer_label(
        result,
        "1. Main Query",
        observed_pid=42,
        compiled_pid=42,
        observed_title="Cayman",
        compiled_title="Cayman",
    )
    assert mapped.block_index == query_steps[0].block_index
    assert map_designer_label(
        result,
        "Other",
        observed_pid=42,
        compiled_pid=42,
        observed_title="Cayman",
        compiled_title="Cayman",
    ).block_index is None


def test_observer_frame_validation_rejects_malformed_and_oversized_payloads():
    with pytest.raises(ValueError):
        parse_snapshot("[]")
    with pytest.raises(ValueError):
        parse_snapshot(json.dumps({"sourceText": 4}))
    with pytest.raises(ValueError):
        parse_snapshot("{" + "x" * 300_001 + "}")


def test_observer_frame_keeps_unicode_source_and_utf16_selection_contract():
    snapshot = parse_snapshot(
        json.dumps(
            {
                "targetPid": 7,
                "windowTitle": "Editor - 😀",
                "sourceText": "SELECT '😀'",
                "selection": {"text": "😀", "start": 8, "end": 10, "truncated": False},
                "designerValues": [],
                "status": "ok",
            },
            ensure_ascii=False,
        )
    )
    assert snapshot.source_text == "SELECT '😀'"
    assert snapshot.selection.start == 8


def test_refresh_frame_accepts_identical_regenerated_source_and_validates_status():
    result = parse_refresh(
        json.dumps(
            {
                "targetPid": 42,
                "designerWindowTitle": "SQLPathFinder 3 - [sample.VG2]",
                "designerFingerprint": "designer-state",
                "sourceText": "<OPTIONS>\nSELECT 1\n",
                "status": "ok",
            }
        )
    )
    assert result.target_pid == 42
    assert result.designer_fingerprint == "designer-state"
    assert result.source_text == "<OPTIONS>\nSELECT 1\n"
    assert parse_refresh(json.dumps({"status": "editor-open", "error": "close it"})).status == "editor-open"
    assert parse_refresh(json.dumps({"status": "ambiguous", "designerFingerprint": None, "error": "choose one"})).designer_fingerprint == ""
    with pytest.raises(ValueError):
        parse_refresh(json.dumps({"status": "ok", "sourceText": 7}))
    with pytest.raises(ValueError):
        parse_refresh(json.dumps({"status": "ok", "sourceText": "SELECT 1"}))


def test_target_frame_preserves_exact_pid_hwnd_and_document_identity():
    targets = parse_targets(json.dumps({
        "status": "ok",
        "targets": [{
            "targetPid": 23048,
            "designerHwnd": 1181596,
            "designerWindowTitle": "SQLPathFinder 3 - [2763.vg2 (L1)]",
            "documentName": "2763.vg2",
        }],
    }))
    assert targets[0].target_pid == 23048
    assert targets[0].designer_hwnd == 1181596
    assert targets[0].document_name == "2763.vg2"
    with pytest.raises(ValueError):
        parse_targets(json.dumps({"status": "ok", "targets": [{"targetPid": 1, "designerHwnd": 2}]}))


def test_target_frame_keeps_duplicate_documents_selectable_by_pid_and_hwnd():
    targets = parse_targets(json.dumps({
        "status": "ok",
        "targets": [
            {"targetPid": 10, "designerHwnd": 100, "designerWindowTitle": "SQLPathFinder - [same.vg2]", "documentName": "same.vg2"},
            {"targetPid": 11, "designerHwnd": 200, "designerWindowTitle": "SQLPathFinder - [same.vg2]", "documentName": "same.vg2"},
        ],
    }))
    assert [(target.target_pid, target.designer_hwnd) for target in targets] == [(10, 100), (11, 200)]


def test_observer_source_compiles_with_real_framework_csc(tmp_path: Path):
    if os.name != "nt":
        pytest.skip("The native observer requires Windows.")
    windows = Path(os.environ.get("WINDIR", r"C:\Windows"))
    compiler = _find_compiler(windows)
    if compiler is None:
        pytest.skip(".NET Framework csc.exe is unavailable.")
    output = tmp_path / "observer.exe"
    source = Path(__file__).parents[2] / "src" / "vg2c_companion" / "observer.cs"
    completed = subprocess.run(
        _helper_build_command(compiler, output, source),
        capture_output=True,
        text=True,
        encoding="utf-8",
        errors="replace",
        timeout=30,
        check=False,
    )
    assert completed.returncode == 0, completed.stderr or completed.stdout


def test_refresh_completion_is_correlated_and_compiles_fresh_source_once():
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.observer import RefreshResult

    submitted = []
    status = SimpleNamespace(configure=lambda **kwargs: setattr(status, "text", kwargs["text"]))
    app = object.__new__(CompanionApp)
    app._closed = False
    app._refresh_request = 7
    app._refresh_busy = True
    app._refresh_button = None
    app._refresh_compile_identity = None
    app._poll_suspended = False
    app.status = status
    app.diagnostics_tab = None
    app.source_tab = None
    app.worker = SimpleNamespace(submit=lambda path, text: submitted.append((path.name, text)) or 12)
    app._set_source = lambda text: setattr(app, "shown_source", text)
    app._select_tab = lambda _tab: None
    app._set_diagnostics = lambda _text: None
    app.edit_copies = [EditCopy(4, "independent")]
    app.current_result = object()
    source = "<OPTIONS>\nSELECT 1\n"

    app._finish_refresh(6, RefreshResult(status="ok", source_text="stale"))
    assert submitted == []
    app._finish_refresh(
        7,
        RefreshResult(
            target_pid=42,
            designer_window_title="SQLPathFinder 3 - [sample.VG2]",
            designer_fingerprint="fp",
            source_text=source,
            status="ok",
        ),
    )
    assert submitted == [("sqlpathfinder-preview.txt", source)]
    assert app.shown_source == source
    assert app.current_result is not None
    assert app.edit_copies[0].text == "independent"


def test_refresh_completion_is_ignored_after_document_selection_changes():
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.observer import RefreshResult

    app = object.__new__(CompanionApp)
    app._refresh_request = 3
    app._refresh_busy = True
    app._refresh_button = None
    app._refresh_compile_identity = None
    app._poll_suspended = False
    app.current_identity = 8
    app.current_result = object()
    app._invalidate_requests()
    app._finish_refresh(3, RefreshResult(status="ok", source_text="old"))
    assert app.current_result is not None
    assert app.current_identity == -4
    assert not app._refresh_busy


def test_refresh_failure_retains_last_preview_and_releases_passive_polling():
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.observer import RefreshResult

    status = SimpleNamespace(configure=lambda **kwargs: setattr(status, "text", kwargs["text"]))
    app = object.__new__(CompanionApp)
    app._closed = False
    app._refresh_request = 4
    app._refresh_busy = True
    app._refresh_button = None
    app._refresh_compile_identity = None
    app._poll_suspended = True
    app._set_diagnostics = lambda _text: None
    app._select_tab = lambda _tab: None
    app.diagnostics_tab = None
    app.status = status
    app.current_result = object()

    app._finish_refresh(4, RefreshResult(status="editor-open", error="Close the existing editor."))

    assert app.current_result is not None
    assert not app._poll_suspended
    assert "last good preview retained" in status.text


def test_passive_snapshot_is_discarded_while_refresh_compile_is_pending():
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.models import ObserverSnapshot

    app = object.__new__(CompanionApp)
    app._poll_suspended = True
    app._position_beside = lambda _bounds: None
    app._submit_compile = lambda *_args: (_ for _ in ()).throw(AssertionError("stale frame compiled"))
    frame = ObserverSnapshot(target_pid=42, source_text="old source")

    app._apply_snapshot(frame)


def test_passive_snapshot_is_ignored_without_an_exact_selected_target():
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.models import ObserverSnapshot

    app = object.__new__(CompanionApp)
    app._poll_suspended = False
    app._selected_target = None
    app._position_beside = lambda _bounds: (_ for _ in ()).throw(AssertionError("unselected frame positioned"))

    app._apply_snapshot(ObserverSnapshot(target_pid=7, bounds=(1, 2, 3, 4), source_text="old"))


def test_worker_target_epoch_discards_same_pid_frames_from_previous_window():
    from vg2c_companion.observer import ObserverWorker

    worker = object.__new__(ObserverWorker)
    worker._target_lock = threading.Lock()
    worker._target_epoch = 0
    worker._pid = 7
    worker._hwnd = 100
    worker._frames = queue.Queue(maxsize=1)
    worker._frames.put_nowait((0, ObserverSnapshot(target_pid=7, source_text="old")))

    worker.select_target(7, 200)

    assert worker.drain() is None
    worker._frames.put_nowait((1, ObserverSnapshot(target_pid=7, source_text="new")))
    assert worker.drain().source_text == "new"


def test_open_vg2_mismatch_clears_previous_native_target():
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.observer import NativeTarget

    selected = []
    status = SimpleNamespace(configure=lambda **kwargs: setattr(status, "text", kwargs["text"]))
    app = object.__new__(CompanionApp)
    app._targets = (NativeTarget(7, 100, "SQLPathFinder - [a.vg2]", "a.vg2"),)
    app._selected_target = app._targets[0]
    app._refresh_request = 1
    app.current_identity = 1
    app._refresh_busy = False
    app._poll_suspended = False
    app._refresh_compile_identity = None
    app._refresh_button = None
    app._observer_worker = SimpleNamespace(select_target=lambda pid, hwnd: selected.append((pid, hwnd)))
    app.status = status

    app._match_native_target(Path("b.vg2"))

    assert app._selected_target is None
    assert selected[-1] == (None, None)
    assert "No matching" in status.text


def test_discovery_result_is_applied_by_tk_poll_queue():
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.observer import NativeTarget

    target = NativeTarget(7, 100, "SQLPathFinder - [a.vg2]", "a.vg2")
    applied = []
    scheduled = []
    app = object.__new__(CompanionApp)
    app._closed = False
    app._target_results = queue.SimpleQueue()
    app._refresh_results = queue.SimpleQueue()
    app._results = queue.Queue()
    app._observer_worker = SimpleNamespace(drain=lambda: None)
    app._set_targets = lambda targets, error=None: applied.append((targets, error))
    app.root = SimpleNamespace(after=lambda *args: scheduled.append(args))
    app._poll_after = None
    app._target_results.put(( (target,), None ))

    app._discover_targets = lambda: None
    app._poll()

    assert applied == [((target,), None)]
    assert scheduled


@pytest.mark.parametrize("renamed", [False, True])
def test_rediscovery_updates_descriptor_and_invalidates_only_changed_document(renamed):
    from vg2c_companion.app import CompanionApp

    old = NativeTarget(7, 100, "SQLPathFinder - [a.vg2 (L1)]", "a.vg2")
    name = "b.vg2" if renamed else "a.vg2"
    new = NativeTarget(7, 100, f"SQLPathFinder - [{name} (L2)]", name)
    selected = []
    invalidated = []
    combo = SimpleNamespace(
        configure=lambda **_kwargs: None,
        current=lambda *args: 0,
        set=lambda value: None,
    )
    app = object.__new__(CompanionApp)
    app._closed = False
    app._selected_target = old
    app._target_discovery_busy = True
    app.current_native_path = None
    app.native_paths = ()
    app.current_source = None
    app.pid = None
    app.target_combo = combo
    app.status = SimpleNamespace(configure=lambda **_kwargs: None)
    app._observer_worker = SimpleNamespace(select_target=lambda *args: selected.append(args))
    app._invalidate_requests = lambda: invalidated.append(True)

    app._set_targets((new,))

    assert app._selected_target is new
    assert bool(invalidated) is renamed
    assert selected == ([(7, 100)] if renamed else [])


def test_discovery_does_not_attach_or_display_native_target_over_offline_preview():
    from vg2c_companion.app import CompanionApp

    shown = []
    app = object.__new__(CompanionApp)
    app._closed = False
    app._selected_target = None
    app.current_native_path = None
    app.native_paths = ()
    app.pid = None
    app.current_source = Path("offline.txt")
    app.target_combo = SimpleNamespace(
        configure=lambda **_kwargs: None,
        current=lambda *_args: None,
        set=shown.append,
    )
    app.status = SimpleNamespace(configure=lambda **_kwargs: None)

    app._set_targets((NativeTarget(7, 100, "SQLPathFinder - [a.vg2]", "a.vg2"),))

    assert app._selected_target is None
    assert app.current_source == Path("offline.txt")
    assert shown == [""]


def test_refresh_worker_completion_is_queued_until_tk_poll():
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.observer import NativeTarget, RefreshResult

    target = NativeTarget(7, 100, "SQLPathFinder - [a.vg2]", "a.vg2")
    result = RefreshResult(target_pid=7, designer_window_title=target.designer_window_title, source_text="SELECT 1", status="ok")
    applied = []
    app = object.__new__(CompanionApp)
    app.observer = SimpleNamespace(refresh=lambda *_args: result)
    app._refresh_results = queue.SimpleQueue()
    app._run_refresh(4, target)
    assert app._refresh_results.get_nowait() == (4, result)

    app._closed = False
    app._target_results = queue.SimpleQueue()
    app._results = queue.Queue()
    app._observer_worker = SimpleNamespace(drain=lambda: None)
    app.root = SimpleNamespace(after=lambda *_args: None)
    app._poll_after = None
    app._finish_refresh = lambda request, value: applied.append((request, value))
    app._refresh_results.put((4, result))
    app._poll()
    assert applied == [(4, result)]


def test_target_switch_discards_observation_already_in_flight():
    from vg2c_companion.observer import ObserverWorker

    started = threading.Event()
    release = threading.Event()

    def poll(pid, hwnd):
        if hwnd == 100:
            started.set()
            release.wait(2)
        return ObserverSnapshot(target_pid=pid, source_text=str(hwnd))

    client = SimpleNamespace(poll=poll, close=release.set)
    worker = ObserverWorker(client, 7, 100)
    try:
        assert started.wait(2)
        worker.select_target(7, 200)
        release.set()
        deadline = time.monotonic() + 2
        frame = None
        while frame is None and time.monotonic() < deadline:
            frame = worker.drain()
            time.sleep(0.01)
        assert frame is not None
        assert frame.source_text == "200"
    finally:
        worker.close()


def test_tk_update_pipeline_and_rediscovery_use_current_document(monkeypatch):
    tk = pytest.importorskip("tkinter")
    try:
        root = tk.Tk()
    except tk.TclError:
        pytest.skip("Tk display is unavailable")
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.observer import RefreshResult

    root.withdraw()
    source = "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT 1\n"
    target = NativeTarget(7, 100, "SQLPathFinder - [a.vg2]", "a.vg2")
    calls = []
    observer = SimpleNamespace(last_target_error=None, targets=(target,))
    observer.list_targets = lambda: observer.targets

    def refresh(pid, hwnd, name):
        calls.append((pid, hwnd, name))
        return RefreshResult(pid, observer.targets[0].designer_window_title, "fp", source, "ok")

    observer.refresh = refresh
    monkeypatch.setattr("vg2c_companion.app.ObserverClient", lambda: observer)
    monkeypatch.setattr("vg2c_companion.app.ObserverWorker", lambda *_args: SimpleNamespace(
        drain=lambda: None, close=lambda: None, select_target=lambda *_args: None,
        invalidate_frames=lambda: None,
    ))
    app = CompanionApp(root)

    def wait_until(condition):
        deadline = time.monotonic() + 5
        while not condition() and time.monotonic() < deadline:
            root.update()
            time.sleep(0.01)
        assert condition()

    try:
        wait_until(lambda: app._selected_target is not None)
        app.current_source = Path("offline.txt")
        app._select_target()
        assert app.current_source is None
        app.edit_copies.append(EditCopy(0, "keep my edits"))
        app.notebook.select(app.source_tab)
        app.update_python()
        wait_until(lambda: app.current_result is not None and not app._refresh_busy)
        assert app.notebook.select() == str(app.generated_tab)
        assert app.edit_copies[0].text == "keep my edits"
        assert app.compiled_source_text == source

        observer.targets = (NativeTarget(7, 100, "SQLPathFinder - [b.vg2]", "b.vg2"),)
        app.refresh_targets()
        wait_until(lambda: not app._target_discovery_busy)
        assert app._selected_target.document_name == "b.vg2"
        app.update_python()
        wait_until(lambda: not app._refresh_busy)
        assert calls == [(7, 100, "a.vg2"), (7, 100, "b.vg2")]
        assert "b.vg2" in app.compiled_origin
    finally:
        app.edit_copies.clear()
        app.close()


@pytest.mark.parametrize("status", ["missing", "exited", "inaccessible"])
def test_observer_frames_are_typed_and_preserve_process_boundaries(status: str):
    snapshot = parse_snapshot(
        json.dumps(
            {
                "targetPid": 42,
                "windowTitle": "SQLPathFinder",
                "sourceText": "<OPTIONS>\n</OPTIONS>",
                "sourceTruncated": True,
                "selection": {"text": "SELECT", "start": 2, "end": 8, "truncated": False},
                "designerValues": ["TABLEGRID"],
                "fingerprint": "abc",
                "status": status,
                "error": "window unavailable",
                "bounds": [1, 2, 3, 4],
            }
        )
    )
    assert snapshot.target_pid == 42
    assert snapshot.source_truncated
    assert snapshot.selection.start == 2
    assert snapshot.status == status
    assert snapshot.bounds == (1, 2, 3, 4)


def test_worker_drops_out_of_order_snapshot(monkeypatch, tmp_path: Path):
    import vg2c_companion.compiler as compiler

    first_started = threading.Event()
    release_first = threading.Event()
    delivered = []

    def fake_compile(source_text: str, source_name: str):
        if source_text == "first":
            first_started.set()
            release_first.wait(2)
        return SimpleNamespace(emitted=SimpleNamespace(source=source_text), diagnostics=())

    monkeypatch.setattr(compiler, "compile_source", fake_compile)
    worker = CompilationWorker(delivered.append)
    try:
        worker.submit(tmp_path / "source.txt", "first")
        assert first_started.wait(2)
        second_id = worker.submit(tmp_path / "source.txt", "second")
        release_first.set()
        deadline = threading.Event()
        deadline.wait(0.2)
        assert delivered and delivered[-1].identity == second_id
        assert delivered[-1].source_text == "second"
    finally:
        worker.close()


def test_worker_rejects_submission_after_close(tmp_path: Path):
    worker = CompilationWorker(lambda _outcome: None)
    worker.close()
    with pytest.raises(RuntimeError):
        worker.submit(tmp_path / "source.txt", "later")


def test_tk_main_query_mapping_persists_and_respects_native_selection():
    tk = pytest.importorskip("tkinter")
    try:
        root = tk.Tk()
    except tk.TclError:
        pytest.skip("Tk display is unavailable")
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.models import ObserverSnapshot
    from tkinter import ttk

    try:
        root.withdraw()
        source = "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT 1\n"
        result = compile_source(source, "main-query.txt")
        app = object.__new__(CompanionApp)
        app.root = root
        app.generated = tk.Text(root)
        app.generated.tag_configure("selection", background="#fff2a8")
        app.status = tk.Label(root)
        app.selection_status = tk.Label(root)
        app.current_result = result
        app.current_assets = {"sql/query.sql": "SELECT 1\n"}
        app.selected_generated_name = "main.py"
        app.current_identity = 1
        app.compiled_source_text = source
        app.compiled_pid = 42
        app.compiled_designer_title = "Cayman"
        app.compiled_designer_fingerprint = "fp"
        app.designer_fingerprint = "fp"
        app.last_live_source = ""
        app._pending_pid = None
        app._pending_designer_title = ""
        app._pending_designer_fingerprint = ""
        app._last_designer_label = ""
        app._last_mapping_key = None
        app._active_mapping = None
        app._poll_suspended = False
        app._selected_target = NativeTarget(42, 100, "Cayman", "sample.vg2")
        app._positioned = True
        app.worker = type("Worker", (), {"submit": lambda self, path, text: calls.append((path, text)) or 2})()
        calls = []
        app.source = tk.Text(root, state="disabled")
        app.notebook = ttk.Notebook(root)
        app.generated_tab = ttk.Frame(app.notebook)
        app.source_tab = ttk.Frame(app.notebook)
        app.diagnostics_tab = ttk.Frame(app.notebook)
        app.notebook.add(app.generated_tab, text="Generated code")
        app.notebook.add(app.source_tab, text="Source")
        app.notebook.add(app.diagnostics_tab, text="Diagnostics")

        frame = dict(
            target_pid=42,
            window_title="Editor - [Untitled]",
            source_text="",
            source_truncated=False,
            selection={"text": "", "truncated": False},
            designer_values=["1. Main Query focused <TreeItem>"],
            designer_fingerprint="fp",
            designer_window_title="Cayman",
            status="ok",
        )

        def observe(values, fingerprint="fp", source_text=""):
            return ObserverSnapshot(
                target_pid=frame["target_pid"],
                window_title=frame["window_title"],
                source_text=source_text,
                source_truncated=frame["source_truncated"],
                designer_values=tuple(values),
                designer_fingerprint=fingerprint,
                designer_window_title=frame["designer_window_title"],
            )

        app._apply_snapshot(observe(frame["designer_values"]))
        first = app.generated.tag_ranges("selection")
        assert first
        app._apply_snapshot(observe(frame["designer_values"]))
        assert app.generated.tag_ranges("selection") == first
        app._apply_snapshot(observe(frame["designer_values"], "changed"))
        assert "press Update Python" in app.status.cget("text")
        app._apply_snapshot(observe(frame["designer_values"]))
        assert app.status.cget("text") == "Preview ready"
        app._apply_snapshot(observe([]))
        assert not app.generated.tag_ranges("selection")
        app._apply_snapshot(observe(frame["designer_values"]))
        assert app.generated.tag_ranges("selection")
        app.selected_generated_name = "sql/query.sql"
        app._show_generated("sql/query.sql")
        app._apply_snapshot(observe(frame["designer_values"]))
        assert app.selected_generated_name == "sql/query.sql"
        assert not app.generated.tag_ranges("selection")
        app._pending_pid = 42
        app._pending_designer_title = "Cayman"
        app._pending_designer_fingerprint = "fp"
        app._apply_snapshot(observe(frame["designer_values"], "next", "<OPTIONS>\nSELECT 1\n"))
        app._apply_snapshot(observe(frame["designer_values"], "next", "<OPTIONS>\nSELECT 1\n"))
        assert len(calls) == 1
        assert app._pending_designer_fingerprint == "next"
        app.notebook.select(app.source_tab)
        app._apply_snapshot(observe(frame["designer_values"], "next"))
        assert app.notebook.select() == str(app.source_tab)
    finally:
        root.destroy()


def test_tk_unmapped_source_caret_clears_and_next_mapping_restores_highlight():
    tk = pytest.importorskip("tkinter")
    try:
        root = tk.Tk()
    except tk.TclError:
        pytest.skip("Tk display is unavailable")
    from vg2c_companion.app import CompanionApp
    from vg2c_companion.models import ObserverSnapshot

    try:
        root.withdraw()
        source = "first\n<---- New Query ---->\nsecond\n"
        app = object.__new__(CompanionApp)
        app.root = root
        app.generated = tk.Text(root)
        app.generated.insert("1.0", "0123456789" * 5)
        app.generated.tag_configure("selection", background="#fff2a8")
        app.status = tk.Label(root, text="Preview ready")
        app.selection_status = tk.Label(root)
        app.current_result = _fake_result()
        app.current_assets = {"sql/query.sql": "SELECT 1\n"}
        app.current_identity = 1
        app.compiled_source_text = source
        app.compiled_pid = 42
        app.compiled_designer_title = "Cayman"
        app.compiled_designer_fingerprint = "fp"
        app.designer_fingerprint = "fp"
        app.last_live_source = source
        app._pending_pid = 42
        app._pending_designer_title = "Cayman"
        app._pending_designer_fingerprint = "fp"
        app._last_designer_label = ""
        app._last_mapping_key = None
        app._active_mapping = None
        app.selected_generated_name = "main.py"
        app._poll_suspended = False
        app._selected_target = NativeTarget(42, 100, "Cayman", "sample.vg2")

        def observe(offset):
            return ObserverSnapshot(
                target_pid=42,
                source_text=source,
                selection=Selection(text="caret", start=offset, end=offset),
                designer_fingerprint="fp",
                designer_window_title="Cayman",
            )

        app._apply_snapshot(observe(1))
        assert tuple(map(str, app.generated.tag_ranges("selection"))) == ("1.10", "1.20")

        app._apply_snapshot(observe(source.index("New Query")))
        assert not app.generated.tag_ranges("selection")
        assert "Query separator has no source block mapping." in app.selection_status.cget("text")

        app._apply_snapshot(observe(1))
        assert tuple(map(str, app.generated.tag_ranges("selection"))) == ("1.10", "1.20")
    finally:
        root.destroy()


def test_tk_companion_initial_layout_and_user_sash_resize(monkeypatch):
    tk = pytest.importorskip("tkinter")
    try:
        root = tk.Tk()
    except tk.TclError:
        pytest.skip("Tk display is unavailable")
    from tkinter import ttk

    class IdleObserver:
        def __init__(self, *_args):
            pass

        def drain(self):
            return None

        def close(self):
            pass

    monkeypatch.setattr("vg2c_companion.app.ObserverWorker", IdleObserver)
    from vg2c_companion.app import CompanionApp

    try:
        root.attributes("-alpha", 0.0)
    except tk.TclError:
        root.destroy()
        pytest.skip("Transparent Tk windows are unavailable")
    app = CompanionApp(root)
    try:
        root.deiconify()
        root.update()
        root.update_idletasks()
        assert 200 <= app.tree.winfo_width() <= 260
        assert app.generated.winfo_width() > app.tree.winfo_width()

        panes = next(child for child in root.winfo_children() if isinstance(child, ttk.Panedwindow))
        panes.sashpos(0, 320)
        root.update_idletasks()
        root.geometry("1100x700")
        root.update_idletasks()
        assert abs(panes.sashpos(0) - 320) <= 1
    finally:
        app.close()


def test_tk_programmatic_tree_selection_does_not_steal_notebook_tab(monkeypatch):
    tk = pytest.importorskip("tkinter")
    try:
        root = tk.Tk()
    except tk.TclError:
        pytest.skip("Tk display is unavailable")

    class IdleObserver:
        def __init__(self, *_args):
            pass

        def drain(self):
            return None

        def close(self):
            pass

    monkeypatch.setattr("vg2c_companion.app.ObserverWorker", IdleObserver)
    from vg2c_companion.app import CompanionApp

    try:
        root.attributes("-alpha", 0.0)
    except tk.TclError:
        root.destroy()
        pytest.skip("Transparent Tk windows are unavailable")
    app = CompanionApp(root)
    try:
        source = "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT 1\n"
        app.current_result = compile_source(source, "main-query.txt")
        app.current_assets = {"sql/query.sql": "SELECT 1\n"}
        app.compiled_source_text = source
        app._populate_generated_tree()
        root.deiconify()
        root.update()

        app._show_generated("main.py", select_tab=False)
        app.notebook.select(app.source_tab)
        root.update()
        assert app.notebook.select() == str(app.source_tab)

        app.notebook.select(app.diagnostics_tab)
        root.update()
        assert app.notebook.select() == str(app.diagnostics_tab)

        app.tree.selection_set("asset:sql/query.sql")
        app.tree.event_generate("<<TreeviewSelect>>")
        root.update()
        assert app.selected_generated_name == "sql/query.sql"
        assert app.notebook.select() == str(app.generated_tab)
    finally:
        app.close()


def test_tk_source_mapping_replaces_previous_highlight():
    tk = pytest.importorskip("tkinter")
    try:
        root = tk.Tk()
    except tk.TclError:
        pytest.skip("Tk display is unavailable")
    from vg2c_companion.app import CompanionApp

    try:
        root.withdraw()
        app = object.__new__(CompanionApp)
        app.generated = tk.Text(root)
        app.generated.insert("1.0", "0123456789" * 4)
        app.generated.tag_configure("selection", background="#fff2a8")
        app._active_mapping = None
        app._show_mapping(SourceMapping(0, 2, (2, 8)))
        app._show_mapping(SourceMapping(1, 20, (20, 28)))
        assert tuple(map(str, app.generated.tag_ranges("selection"))) == ("1.20", "1.28")
        assert app._active_mapping == SourceMapping(1, 20, (20, 28))
    finally:
        root.destroy()


def test_tk_unchanged_source_mapping_preserves_manual_scroll():
    tk = pytest.importorskip("tkinter")
    try:
        root = tk.Tk()
    except tk.TclError:
        pytest.skip("Tk display is unavailable")
    from vg2c_companion.app import CompanionApp

    try:
        root.attributes("-alpha", 0.0)
    except tk.TclError:
        root.destroy()
        pytest.skip("Transparent Tk windows are unavailable")
    try:
        root.geometry("240x150")
        app = object.__new__(CompanionApp)
        app.generated = tk.Text(root, height=5)
        app.generated.pack(fill="both", expand=True)
        app.generated.insert("1.0", "line\n" * 200)
        app.generated.tag_configure("selection", background="#fff2a8")
        app._active_mapping = None
        root.deiconify()
        root.update()

        top = SourceMapping(0, 0, (0, 4))
        app._show_mapping(top)
        app.generated.yview_moveto(1.0)
        root.update_idletasks()
        bottom_view = app.generated.yview()
        assert bottom_view[0] > 0.8

        app._show_mapping(top)
        assert app.generated.yview() == bottom_view

        app.generated.tag_remove("selection", "1.0", "end")
        app._show_mapping(top)
        assert app.generated.tag_ranges("selection")
        assert app.generated.yview()[0] < bottom_view[0]

        app.generated.yview_moveto(1.0)
        root.update_idletasks()
        bottom_view = app.generated.yview()
        middle = SourceMapping(1, 400, (400, 408))
        app._show_mapping(middle)
        assert app.generated.yview()[0] < bottom_view[0]
        assert tuple(map(str, app.generated.tag_ranges("selection"))) == (
            app.generated.index("1.0+400c"),
            app.generated.index("1.0+408c"),
        )
    finally:
        root.destroy()


def test_position_beside_shrinks_to_available_right_space():
    from vg2c_companion.app import CompanionApp

    class Root:
        def __init__(self):
            self.geometry_calls = []

        def winfo_screenwidth(self):
            return 1920

        def winfo_screenheight(self):
            return 1080

        def geometry(self, value):
            self.geometry_calls.append(value)

    root = Root()
    app = object.__new__(CompanionApp)
    app.root = root
    app._positioned = False
    app._position_beside((10, 100, 995, 700))
    geometry = root.geometry_calls[0]
    size, position = geometry.split("+")[0], geometry.split("+")[1:]
    width, height = (int(item) for item in size.split("x"))
    assert 800 <= width <= 960
    assert int(position[0]) >= 1007
    assert int(position[0]) + width <= 1908
    assert height == 650


def test_generated_tree_preserves_nested_asset_paths_and_refreshes_without_orphans():
    from vg2c_companion.app import CompanionApp

    class Tree:
        def __init__(self):
            self.nodes = {"": {"parent": None, "text": ""}}
            self.columns = []

        def exists(self, iid):
            return iid in self.nodes

        def insert(self, parent, _where, iid, **kwargs):
            self.nodes[iid] = {"parent": parent, **kwargs}

        def get_children(self, parent):
            return tuple(iid for iid, data in self.nodes.items() if data.get("parent") == parent)

        def item(self, iid, option):
            return self.nodes[iid][option]

        def parent(self, iid):
            return self.nodes[iid].get("parent", "")

        def delete(self, iid):
            descendants = [
                child for child, data in self.nodes.items()
                if child == iid
                or (isinstance(data.get("parent"), str) and data["parent"].startswith(iid + "/"))
            ]
            for child in descendants:
                self.nodes.pop(child, None)

        def column(self, *args, **kwargs):
            self.columns.append((args, kwargs))

    app = object.__new__(CompanionApp)
    app.tree = Tree()
    app.current_assets = {
        "sql/query/main.sql": "SELECT 1\n",
        "sql/query/parts/where.sql": "WHERE 1\n",
        "html/report/index.html": "<main />\n",
        "styles/report.css": "body {}\n",
    }
    app._tree_paths = {}
    app._tree_assets = {}
    app._tree_asset_files = set()
    app._tree_font = SimpleNamespace(measure=lambda value: len(value) * 7)
    app._tree_indent = 18

    app._populate_generated_tree()
    assert app.tree.nodes["asset:sql/query/main.sql"]["parent"] == "asset:sql/query"
    assert app.tree.nodes["asset:sql/query/parts/where.sql"]["parent"] == "asset:sql/query/parts"
    assert "asset:html/report" in app.tree.nodes
    assert "asset:styles/report.css" in app._tree_asset_files
    app._add_path(Path("designer.VG2"))
    assert app.tree.nodes["input:0"]["parent"] == "sources"
    assert app.tree.nodes["input:0"]["text"].startswith("designer.VG2")

    app.current_assets = {"html/updated/index.html": "<updated />\n"}
    app._populate_generated_tree()
    assert "asset:sql/query/main.sql" not in app.tree.nodes
    assert "asset:html/updated/index.html" in app.tree.nodes
    assert all("sql/" not in name for name in app._tree_assets.values())


def test_generated_tree_sync_is_stable_and_generated_asset_restores_source():
    from vg2c_companion.app import CompanionApp

    class Tree:
        def __init__(self):
            self.selected = ()
            self.focused = ""
            self.seen = []

        def exists(self, iid):
            return iid == "asset:sql/query.sql"

        def selection(self):
            return self.selected

        def focus(self, iid=None):
            if iid is not None:
                self.focused = iid
            return self.focused

        def selection_set(self, iid):
            self.selected = (iid,)

        def see(self, iid):
            self.seen.append(iid)

    class Text:
        def __init__(self):
            self.value = ""

        def configure(self, **_kwargs):
            pass

        def delete(self, *_args):
            self.value = ""

        def insert(self, _index, value):
            self.value = value

        def tag_remove(self, *_args):
            pass

        def tag_add(self, *_args):
            pass

    class Header:
        def __init__(self):
            self.text = ""

        def configure(self, **kwargs):
            self.text = kwargs["text"]

    class Notebook:
        def __init__(self):
            self.selected = None

        def select(self, tab=None):
            if tab is not None:
                self.selected = tab
            return self.selected

    app = object.__new__(CompanionApp)
    app.tree = Tree()
    app._tree_assets = {"asset:sql/query.sql": "sql/query.sql"}
    app._tree_asset_files = {"asset:sql/query.sql"}
    app._last_tree_selection = ()
    app.generated = Text()
    app.source = Text()
    app.generated_header = Header()
    app.generated_origin = Header()
    app.notebook = Notebook()
    app.generated_tab = object()
    app.current_result = SimpleNamespace(emitted=SimpleNamespace(source="print(1)\n"))
    app.current_assets = {"sql/query.sql": "SELECT 1\n"}
    app.compiled_source_text = "<OPTIONS>\nSELECT 1\n"
    app.compiled_origin = "Source: preview.txt"

    app.selected_generated_name = "sql/query.sql"
    app._show_generated("sql/query.sql")
    assert app.source.value == app.compiled_source_text
    assert app.generated_header.text == "Generated preview: sql/query.sql"
    assert app.generated_origin.text == "Origin: Source: preview.txt"
    assert app.notebook.selected is app.generated_tab
    assert app.tree.selected == ("asset:sql/query.sql",)
    seen_count = len(app.tree.seen)
    app._sync_generated_selection("sql/query.sql")
    assert len(app.tree.seen) == seen_count

