"""Tk companion for SQLPathFinder observation and generated-code updates."""

from __future__ import annotations

import queue
import threading
import time
from pathlib import Path

from .compiler import CompilationOutcome, CompilationWorker, runtime_sources
from .models import EditCopy, replace_oldest
from .bridge.client import ObserverClient
from .bridge.protocol import NativeTarget, RefreshResult
from .bridge.worker import ObserverWorker
from .ui.editing import EditCopyMixin
from .ui.layout import LayoutMixin
from .ui.live import LiveSelectionMixin
from .ui.preview import PreviewMixin
from .ui.refresh import RefreshMixin
from .ui.targets import TargetsMixin
from .ui.tree import TreeMixin


class CompanionApp(
    LayoutMixin,
    TreeMixin,
    TargetsMixin,
    RefreshMixin,
    PreviewMixin,
    EditCopyMixin,
    LiveSelectionMixin,
):
    def __init__(self, root, native_paths: tuple[Path, ...] = (), pid: int | None = None) -> None:
        import tkinter as tk
        from tkinter import filedialog, messagebox, ttk

        self.tk = tk
        self.filedialog = filedialog
        self.messagebox = messagebox
        self.ttk = ttk
        self.root = root
        self.pid = pid
        self.native_paths = native_paths
        self.observer = ObserverClient()
        self._observer_worker = ObserverWorker(self.observer, pid)
        self._results: queue.Queue[CompilationOutcome] = queue.Queue(maxsize=1)
        self._target_results: queue.SimpleQueue[tuple[tuple[NativeTarget, ...], str | None]] = queue.SimpleQueue()
        self._refresh_results: queue.SimpleQueue[tuple[int, RefreshResult]] = queue.SimpleQueue()
        self.worker = CompilationWorker(self._queue_result)
        self.current_result = None
        self.current_source: Path | None = None
        self.current_identity = 0
        self.compiled_source_text = ""
        self.last_live_source = ""
        self.compiled_pid: int | None = None
        self.compiled_designer_title = ""
        self.compiled_designer_fingerprint = ""
        self.compiled_origin = ""
        self._pending_pid: int | None = None
        self._pending_designer_title = ""
        self._pending_designer_fingerprint = ""
        self.designer_fingerprint = ""
        self._last_designer_label = ""
        self._last_mapping_key = None
        self._active_mapping = None
        self._last_compile_status = "Preview ready"
        self.current_assets: dict[str, str] = {}
        self.runtime_files = runtime_sources()
        self.selected_generated_name = "main.py"
        self.edit_copies: list[EditCopy] = []
        self._edit_windows: list[tuple[object, object, EditCopy]] = []
        self._tree_paths: dict[str, Path] = {}
        self._tree_assets: dict[str, str] = {}
        self._tree_asset_files: set[str] = set()
        self._last_tree_selection = ()
        self._closed = False
        self._poll_after = None
        self._refresh_request = 0
        self._refresh_busy = False
        self._refresh_again = False
        self._refresh_active: int | None = None  # Capture can outlive a document selection change.
        self._update_after_discovery = False
        self._refresh_button = None
        self.current_native_path: Path | None = None
        self._targets: tuple[NativeTarget, ...] = ()
        self._selected_target: NativeTarget | None = None
        self._poll_suspended = False
        self._refresh_compile_identity: int | None = None
        self._target_discovery_busy = False
        self._next_target_scan = time.monotonic() + 2
        self._build()
        for path in native_paths:
            self._add_path(path)
        self._populate_generated_tree()
        self._target_discovery_busy = True
        threading.Thread(target=self._discover_targets, name="vg2c-targets", daemon=True).start()
        self._poll()

    def _queue_result(self, outcome: CompilationOutcome) -> None:
        replace_oldest(self._results, outcome)

    def _poll(self) -> None:
        if self._closed:
            return
        if (
            time.monotonic() >= self._next_target_scan
            and self.current_source is None
            and not self._refresh_busy
            and self._refresh_active is None
        ):
            self.refresh_targets(quiet=True)
        while True:
            try:
                targets, error = self._target_results.get_nowait()
            except queue.Empty:
                break
            self._set_targets(targets, error)
        while True:
            try:
                request, result = self._refresh_results.get_nowait()
            except queue.Empty:
                break
            self._finish_refresh(request, result)
        snapshot = self._observer_worker.drain()
        if snapshot is not None:
            self._apply_snapshot(snapshot)
        while True:
            try:
                outcome = self._results.get_nowait()
            except queue.Empty:
                break
            self._on_outcome(outcome)
        self._poll_after = self.root.after(100, self._poll)

    def close(self) -> None:
        if any(copy.dirty for copy in self.edit_copies):
            answer = self.messagebox.askyesnocancel("Unsaved edit copy", "Save dirty copies before quitting?")
            if answer is None:
                return
            if answer:
                for window, editor, copy in tuple(self._edit_windows):
                    if copy.dirty and not self._save_edit_copy(window, editor, copy):
                        return
        self._closed = True
        if self._poll_after is not None:
            self.root.after_cancel(self._poll_after)
        self._observer_worker.close()
        self.worker.close()
        self.root.destroy()


def run(native_paths: tuple[Path, ...] = (), pid: int | None = None) -> None:
    import tkinter as tk

    root = tk.Tk()
    CompanionApp(root, native_paths, pid)
    root.mainloop()


__all__ = ["CompanionApp", "run"]
