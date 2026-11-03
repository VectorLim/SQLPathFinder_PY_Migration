from __future__ import annotations

import json
import queue
import threading
import time
from pathlib import Path
from types import SimpleNamespace

from vg2c_companion import compiler
from vg2c_companion.app import CompanionApp
from vg2c_companion.bridge import client as client_module
from vg2c_companion.bridge import worker as worker_module
from vg2c_companion.models import ObserverSnapshot
from vg2c_companion.bridge.protocol import NativeTarget
from vg2c_companion.ui import preview as preview_module
from vg2c_companion.ui import targets as targets_module
from vg2c_companion.ui.preview import PreviewMixin


def _bare_app(cls):
    app = object.__new__(cls)
    app.runtime_files = {}
    app._refresh_active = None
    app._refresh_again = False
    app._update_after_discovery = False
    app._next_target_scan = float("inf")
    app.current_source = None
    app._closed = False
    return app


def _wait_for_frame(worker, timeout: float = 0.3):
    deadline = time.monotonic() + timeout
    while time.monotonic() < deadline:
        frame = worker.drain()
        if frame is not None:
            return frame
        threading.Event().wait(0.005)
    return worker.drain()


def test_worker_waits_without_hwnd_then_polls_selected_target(monkeypatch):
    monkeypatch.setattr(worker_module, "_IDLE_WAIT_SECONDS", 0.005)
    monkeypatch.setattr(worker_module, "_POLL_INTERVAL_SECONDS", 0.01)

    class FakeClient:
        def __init__(self):
            self.calls = []
            self.polled = threading.Event()
            self.frame = ObserverSnapshot(target_pid=7, source_text="SELECT 1")

        def should_poll(self, _pid, *, first=False):
            return True

        def poll(self, pid, hwnd):
            self.calls.append((pid, hwnd))
            self.polled.set()
            return self.frame

        def close(self):
            pass

    client = FakeClient()
    worker = worker_module.ObserverWorker(client)
    try:
        assert not client.polled.wait(0.2)
        assert client.calls == []

        worker.select_target(7, 100)
        assert client.polled.wait(0.3)
        assert _wait_for_frame(worker) is client.frame
        assert client.calls == [(7, 100)]
    finally:
        worker.close()


def test_worker_polls_once_per_epoch_when_should_poll_requires_first(monkeypatch):
    monkeypatch.setattr(worker_module, "_POLL_INTERVAL_SECONDS", 0.01)
    monkeypatch.setattr(worker_module, "_IDLE_WAIT_SECONDS", 0.005)
    monkeypatch.setattr(worker_module, "_HEARTBEAT_SECONDS", 60.0)

    class FakeClient:
        def __init__(self):
            self.calls = []
            self.first_poll = threading.Event()
            self.second_poll = threading.Event()
            self.third_poll = threading.Event()

        def should_poll(self, _pid, *, first=False):
            return first

        def poll(self, pid, hwnd):
            self.calls.append((pid, hwnd))
            count = len(self.calls)
            if count == 1:
                self.first_poll.set()
            elif count == 2:
                self.second_poll.set()
            else:
                self.third_poll.set()
            return ObserverSnapshot(target_pid=pid)

        def close(self):
            pass

    client = FakeClient()
    worker = worker_module.ObserverWorker(client, 7, 100)
    try:
        assert client.first_poll.wait(0.3)
        assert not client.second_poll.wait(0.3)
        assert len(client.calls) == 1

        worker.invalidate_frames()
        assert client.second_poll.wait(0.3)
        assert not client.third_poll.wait(0.2)
        assert len(client.calls) == 2
    finally:
        worker.close()


def test_observer_client_should_poll_tracks_refresh_and_foreground(monkeypatch):
    client = client_module.ObserverClient()
    during_refresh = []

    def fake_refresh(_pid, _hwnd, _document):
        during_refresh.append(client.should_poll(7, first=True))
        return object()

    monkeypatch.setattr(client, "_refresh", fake_refresh)
    client.refresh(7, 100, "sample.vg2")
    assert during_refresh == [False]
    assert client.should_poll(7, first=True)

    monkeypatch.setattr(client_module, "foreground_pid", lambda: 7)
    assert client.should_poll(7)
    monkeypatch.setattr(client_module, "foreground_pid", lambda: 8)
    assert not client.should_poll(7)
    monkeypatch.setattr(client_module, "foreground_pid", lambda: None)
    assert client.should_poll(7)


def test_observer_client_poll_reuses_and_recreates_serve_session(monkeypatch, tmp_path):
    frame = json.dumps(
        {
            "targetPid": 7,
            "windowTitle": "SQLPathFinder",
            "sourceText": "SELECT 1",
            "status": "ok",
        }
    ).encode("utf-8") + b"\n"
    sessions = []

    class FakeProcess:
        def __init__(self):
            self.returncode = None

        def poll(self):
            return self.returncode

        def kill(self):
            self.returncode = -9

        def wait(self, timeout=None):
            return self.returncode

    class FakeSession:
        def __init__(self, _helper):
            self.process = FakeProcess()
            self.index = len(sessions)
            self.requests = 0
            sessions.append(self)

        def request(self, _pid, _hwnd):
            self.requests += 1
            if self.index == 0 and self.requests <= 2:
                return frame
            if self.index == 0:
                raise queue.Empty
            return None

    helper = tmp_path / "observer.exe"
    client = client_module.ObserverClient(tmp_path)
    monkeypatch.setattr(client, "ensure_helper", lambda: helper)
    monkeypatch.setattr(client_module, "_ServeSession", FakeSession)

    first = client.poll(7, 100)
    second = client.poll(7, 100)
    assert first.status == second.status == "ok"
    assert first.target_pid == 7
    assert first.source_text == "SELECT 1"
    assert len(sessions) == 1

    timed_out = client.poll(7, 100)
    assert timed_out.status == "error"
    assert timed_out.error == "Observer helper timed out."
    assert len(sessions) == 1

    exited = client.poll(7, 100)
    assert exited.status == "error"
    assert exited.error == "Observer helper exited unexpectedly."
    assert len(sessions) == 2


def test_target_discovery_scan_interval_backs_off_and_resets(monkeypatch):
    class FakeCombo:
        def configure(self, **_kwargs):
            pass

        def current(self, _index=None):
            pass

        def set(self, _value):
            pass

    app = _bare_app(CompanionApp)
    app._targets = ()
    app._selected_target = None
    app._target_discovery_busy = True
    app._target_scan_interval = 2.0
    app.current_native_path = None
    app.native_paths = ()
    app.current_result = None
    app.pid = None
    app.target_combo = FakeCombo()
    app.status = SimpleNamespace(configure=lambda **_kwargs: None)
    app._select_target = lambda _event=None: None

    target = NativeTarget(7, 100, "SQLPathFinder - [sample.vg2]", "sample.vg2")
    targets = (target,)
    expected_intervals = (2.0, 4.0, 8.0, 8.0)
    for expected in expected_intervals:
        app._set_targets(targets)
        assert app._target_scan_interval == expected

    app._set_targets((NativeTarget(8, 200, "SQLPathFinder - [other.vg2]", "other.vg2"),))
    assert app._target_scan_interval == 2.0

    class NoopThread:
        def __init__(self, **_kwargs):
            pass

        def start(self):
            pass

    monkeypatch.setattr(targets_module.threading, "Thread", NoopThread)
    app._target_scan_interval = 8.0
    app._target_discovery_busy = False
    app.refresh_targets(quiet=False)
    assert app._target_scan_interval == 2.0


def test_generated_preview_skips_identical_render_and_clears_selection(monkeypatch):
    class FakeText:
        def __init__(self):
            self.calls = []

        def configure(self, **kwargs):
            self.calls.append(("configure", kwargs))

        def delete(self, start, end):
            self.calls.append(("delete", start, end))

        def tag_remove(self, tag, start, end):
            self.calls.append(("tag_remove", tag, start, end))

        def insert(self, start, text):
            self.calls.append(("insert", start, text))

        def tag_add(self, *args):
            self.calls.append(("tag_add", *args))

    monkeypatch.setattr(preview_module, "highlight_ranges", lambda _text, _mode: ())
    app = object.__new__(PreviewMixin)
    app.generated = FakeText()
    app.selected_generated_name = "runtime file"
    app.current_result = None

    app._set_generated("print('ready')\n", "python")
    first_call_count = len(app.generated.calls)
    app._set_generated("print('ready')\n", "python")

    repeated_calls = app.generated.calls[first_call_count:]
    assert not any(call[0] in {"delete", "insert"} for call in repeated_calls)
    assert ("tag_remove", "selection", "1.0", "end") in repeated_calls


def test_runtime_sources_returns_fresh_dict_each_call():
    first = compiler.runtime_sources()
    second = compiler.runtime_sources()

    assert first == second
    assert first is not second
    first.pop(next(iter(first)))
    assert first != second