"""Explicit "Update Python" refresh requests against the live designer."""

from __future__ import annotations

import threading
from pathlib import Path

from ..bridge.protocol import NativeTarget, RefreshResult


class RefreshMixin:
    def update_python(self) -> None:
        if self._refresh_busy or self._refresh_active is not None:
            self._refresh_again = True
            self.status.configure(text="Update queued; the latest SQLPathFinder source will be captured next.")
            return
        self._update_after_discovery = True
        self.refresh_targets()

    def _start_update(self) -> None:
        if self._refresh_busy or self._refresh_active is not None:
            self._refresh_again = True
            return
        target = self._selected_target
        if target is None:
            self.status.configure(text="Select an open SQLPathFinder document before updating Python.")
            return
        self._refresh_request += 1
        request = self._refresh_request
        self.current_identity = -request
        self._refresh_compile_identity = None
        self._refresh_busy = True
        self._refresh_active = request
        self._poll_suspended = True
        if hasattr(getattr(self, "_observer_worker", None), "invalidate_frames"):
            self._observer_worker.invalidate_frames()
        self.status.configure(text=f"Updating Python from {target.document_name}…")
        thread = threading.Thread(
            target=self._run_refresh,
            args=(request, target),
            name="vg2c-refresh",
            daemon=True,
        )
        thread.start()

    def _run_refresh(self, request: int, target: NativeTarget) -> None:
        try:
            result = self.observer.refresh(target.target_pid, target.designer_hwnd, target.document_name)
        except Exception as exc:
            result = RefreshResult(status="error", error=str(exc))
        self._refresh_results.put((request, result))

    def _finish_refresh(self, request: int, result: RefreshResult) -> None:
        if request == self._refresh_active:
            self._refresh_active = None
        if request != self._refresh_request or self._closed:
            self._run_queued_update()
            return
        if result.status != "ok" or not result.source_text:
            self._refresh_busy = False
            self._poll_suspended = False
            self._refresh_compile_identity = None
            if hasattr(getattr(self, "_observer_worker", None), "invalidate_frames"):
                self._observer_worker.invalidate_frames()
            self._set_diagnostics("Update Python failed\n\n" + (result.error or result.status))
            self._select_tab(getattr(self, "diagnostics_tab", None))
            self.status.configure(text="Update Python failed; last good preview retained")
            self._run_queued_update()
            return
        self.last_live_source = result.source_text
        self.current_identity = self._submit_compile(
            Path("sqlpathfinder-preview.txt"), result.source_text,
            result.target_pid, result.designer_window_title, result.designer_fingerprint,
        )
        self._refresh_compile_identity = self.current_identity
        self._set_source(result.source_text)
        self.status.configure(text="Captured fresh Show Query source; compiling preview…")

    def _run_queued_update(self) -> None:
        if self._refresh_again and not self._refresh_busy and self._refresh_active is None and not self._closed:
            self._refresh_again = False
            self.update_python()
