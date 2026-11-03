"""Discovery and selection of open SQLPathFinder documents."""

from __future__ import annotations

import threading
import time
from pathlib import Path

from ..bridge.protocol import NativeTarget
from ..source import is_prepost_child

_SCAN_INTERVAL_MIN = 2.0
_SCAN_INTERVAL_MAX = 8.0


class TargetsMixin:
    def _discover_targets(self) -> None:
        try:
            targets = self.observer.list_targets()
            error = self.observer.last_target_error
        except Exception as exc:
            targets = ()
            error = str(exc)
        self._target_results.put((targets, error))

    def refresh_targets(self, *, quiet: bool = False) -> None:
        if self._target_discovery_busy:
            return
        self._target_discovery_busy = True
        self._next_target_scan = time.monotonic() + _SCAN_INTERVAL_MIN
        if not quiet:
            self._target_scan_interval = _SCAN_INTERVAL_MIN
            self.status.configure(text="Finding open SQLPathFinder documents…")
        threading.Thread(target=self._discover_targets, name="vg2c-targets", daemon=True).start()

    @staticmethod
    def _target_key(target: NativeTarget) -> tuple[int, int]:
        return target.target_pid, target.designer_hwnd

    def _preferred_document_name(self) -> str:
        candidates = [self.current_native_path, *self.native_paths]
        for path in candidates:
            if path is not None and path.suffix.lower() == ".vg2":
                return path.name.casefold()
        return ""

    def _set_targets(self, targets: tuple[NativeTarget, ...], error: str | None = None) -> None:
        if self._closed:
            return
        update_requested = self._update_after_discovery
        self._update_after_discovery = False
        self._target_discovery_busy = False
        previous = self._selected_target
        previous_key = self._target_key(previous) if previous else None
        interval = getattr(self, "_target_scan_interval", _SCAN_INTERVAL_MIN)
        interval = min(interval * 2, _SCAN_INTERVAL_MAX) if targets == getattr(self, "_targets", None) else _SCAN_INTERVAL_MIN
        self._target_scan_interval = interval
        self._next_target_scan = time.monotonic() + interval
        self._targets = targets
        counts: dict[str, int] = {}
        for target in targets:
            counts[target.document_name.casefold()] = counts.get(target.document_name.casefold(), 0) + 1
        seen: dict[str, int] = {}
        labels = []
        for target in targets:
            key = target.document_name.casefold()
            seen[key] = seen.get(key, 0) + 1
            labels.append(
                f"{target.document_name} (window {seen[key]})"
                if counts[key] > 1 else target.document_name
            )
        self.target_combo.configure(values=labels)
        if not targets:
            if self._selected_target is not None:
                self._invalidate_requests()
            self._selected_target = None
            self.target_combo.set("")
            if hasattr(getattr(self, "_observer_worker", None), "select_target"):
                self._observer_worker.select_target(None, None)
            message = (
                f"Document discovery failed: {error} Press Refresh documents to retry."
                if error else "No open SQLPathFinder VG2 documents found; press Refresh documents to retry."
            )
            self.status.configure(text=message)
            return
        preferred = next((index for index, target in enumerate(targets) if self._target_key(target) == previous_key), None)
        preserved = (
            preferred is not None
            and previous is not None
            and targets[preferred].document_name.casefold() == previous.document_name.casefold()
        )
        if (
            preferred is not None
            and previous is not None
            and not update_requested
            and is_prepost_child(targets[preferred].designer_window_title, previous.designer_window_title)
            and self.current_result is not None
        ):
            self.status.configure(text="Pre/Post Query is open; retaining the parent preview.")
            return
        preferred_name = self._preferred_document_name()
        if preferred is None:
            matches = [index for index, target in enumerate(targets) if target.document_name.casefold() == preferred_name]
            preferred = matches[0] if len(matches) == 1 else None
        if preferred is None and not preferred_name and self.pid is not None:
            matches = [index for index, target in enumerate(targets) if target.target_pid == self.pid]
            preferred = matches[0] if len(matches) == 1 else None
        if preferred is None and not preferred_name and len(targets) == 1:
            preferred = 0
        if preferred is None:
            if self._selected_target is not None:
                self._invalidate_requests()
            self.target_combo.set("")
            self._selected_target = None
            if hasattr(getattr(self, "_observer_worker", None), "select_target"):
                self._observer_worker.select_target(None, None)
            self.status.configure(text=error or "Select the matching open SQLPathFinder document before updating Python.")
            return
        self.target_combo.current(preferred)
        if preserved:
            self._selected_target = targets[preferred]
            if update_requested:
                self._start_update()
        else:
            if self.current_source is not None and not update_requested:
                self.target_combo.set("")
                self.status.configure(text="Offline preview selected; press Update Python to use an open VG2.")
                return
            self._select_target()

    def _detach_native_target(self) -> None:
        self._selected_target = None
        if hasattr(self, "target_combo"):
            self.target_combo.set("")
        if hasattr(getattr(self, "_observer_worker", None), "select_target"):
            self._observer_worker.select_target(None, None)

    def _match_native_target(self, path: Path) -> None:
        matches = [index for index, target in enumerate(self._targets) if target.document_name.casefold() == path.name.casefold()]
        if len(matches) == 1:
            self.target_combo.current(matches[0])
            self._select_target()
            return
        if self._selected_target is not None:
            self._invalidate_requests()
        self._detach_native_target()
        message = "No matching open SQLPathFinder document is selected."
        if len(matches) > 1:
            message = "Multiple open SQLPathFinder documents have that filename; select the matching document."
        self.status.configure(text=message)

    def _select_target(self, _event=None) -> None:
        index = self.target_combo.current()
        self._selected_target = self._targets[index] if 0 <= index < len(self._targets) else None
        if self._selected_target is not None:
            self.current_source = None
            self._invalidate_requests()
            if hasattr(getattr(self, "_observer_worker", None), "select_target"):
                self._observer_worker.select_target(self._selected_target.target_pid, self._selected_target.designer_hwnd)
            self.status.configure(text=f"Selected {self._selected_target.document_name}; press Update Python.")
            self._start_update()

    def _invalidate_requests(self) -> None:
        self._refresh_request += 1
        self.current_identity = -self._refresh_request
        self._refresh_busy = False
        self._refresh_again = False
        self._poll_suspended = False
        self._refresh_compile_identity = None
        if hasattr(getattr(self, "_observer_worker", None), "invalidate_frames"):
            self._observer_worker.invalidate_frames()
