"""Apply passive observer frames: live source capture and designer/editor mapping."""

from __future__ import annotations

import re
from pathlib import Path

from ..models import ObserverSnapshot
from ..source import (
    designer_document,
    is_prepost_child,
    map_designer_label,
    map_source_offset,
    utf16_to_python_offset,
)

_LABEL_SPLIT = re.compile(r"(?:=| \[| <)")
_STEP_LABEL = re.compile(r"^[0-9]+(?:[.-][0-9A-Za-z]+)*\.\s+")
_PERSISTENT_STATUS = (
    "Compiler error", "Compiled with diagnostics:", "Exported generated snapshot",
    "Saved new copy", "Editing an independent copy",
)


def observed_step_labels(designer_values: tuple[str, ...]) -> tuple[str, ...]:
    return tuple(dict.fromkeys(
        label_value
        for raw_value in designer_values
        for label_value in (_LABEL_SPLIT.split(raw_value, maxsplit=1)[0].replace(" focused", "").strip(),)
        if _STEP_LABEL.match(label_value)
    ))


class LiveSelectionMixin:
    def _apply_snapshot(self, snapshot: ObserverSnapshot) -> None:
        if self._poll_suspended:
            return
        if self._selected_target is None:
            return
        if snapshot.target_pid != self._selected_target.target_pid:
            return
        if snapshot.designer_bounds or snapshot.bounds:
            self._position_beside(snapshot.designer_bounds or snapshot.bounds)
        if snapshot.status != "ok":
            self._last_mapping_key = None
            self._clear_selection()
            self.status.configure(text=f"Observer: {snapshot.error or snapshot.status}")
            return
        selected = snapshot.selection.text.replace("\n", " ")
        designer = "; ".join(snapshot.designer_values[:3])
        label = ("Selection: " + selected[:160]) if selected else ("Designer: " + designer if designer else "No live selection")
        self.selection_status.configure(text=label)
        live_context = (
            snapshot.target_pid,
            designer_document(snapshot.designer_window_title),
            snapshot.designer_fingerprint,
        )
        pending_context = (
            self._pending_pid,
            designer_document(self._pending_designer_title),
            self._pending_designer_fingerprint,
        )
        compiled_context = (
            self.compiled_pid,
            designer_document(self.compiled_designer_title),
            self.compiled_designer_fingerprint,
        )
        if (
            is_prepost_child(snapshot.designer_window_title, self.compiled_designer_title)
            and self.current_result is not None
            and live_context != compiled_context
            and live_context != pending_context
        ):
            self.selection_status.configure(text=label + " | Pre/Post Query retains the parent preview.")
            return
        if snapshot.source_truncated:
            self.status.configure(text="Live source is truncated; preview withheld.")
        elif snapshot.source_text and (
            snapshot.source_text != self.last_live_source or live_context != pending_context
        ):
            self.last_live_source = snapshot.source_text
            self.current_identity = self._submit_compile(
                Path("sqlpathfinder-preview.txt"), snapshot.source_text,
                snapshot.target_pid, snapshot.designer_window_title, snapshot.designer_fingerprint,
            )
            self._set_source(snapshot.source_text)
            self.status.configure(text="Captured original Show Query source; compiling preview…")
        designer_same = not self.designer_fingerprint or snapshot.designer_fingerprint == self.designer_fingerprint
        self.designer_fingerprint = snapshot.designer_fingerprint or self.designer_fingerprint
        observed_labels = observed_step_labels(snapshot.designer_values)
        mapping_key = (
            snapshot.active_control,
            observed_labels,
            self.compiled_source_text,
            self.compiled_pid,
            self.compiled_designer_title,
            self.compiled_designer_fingerprint,
        )
        if snapshot.active_control == "designer":
            if observed_labels != self._last_designer_label:
                self._last_designer_label = observed_labels
                if not observed_labels:
                    self._last_mapping_key = None
                    self._clear_selection()
        else:
            self._last_mapping_key = None
            if snapshot.active_control == "editor":
                self._last_designer_label = ()
        context_matches = live_context == compiled_context
        pending_source = bool(snapshot.source_text and snapshot.source_text != self.compiled_source_text)
        self._update_designer_status(snapshot, context_matches, pending_source)
        if (
            designer_same
            and context_matches
            and self.current_result is not None
            and not pending_source
            and not snapshot.source_truncated
            and snapshot.active_control == "designer"
            and observed_labels
            and mapping_key != self._last_mapping_key
        ):
            self._last_mapping_key = mapping_key
            self._map_designer_selection(snapshot, observed_labels, label)
        if not designer_same or not context_matches or snapshot.source_truncated:
            self._last_mapping_key = None
            self._clear_selection()
        elif self.selected_generated_name == "main.py" and self._active_mapping is not None:
            self._show_mapping(self._active_mapping)
        if (
            self.current_result is not None
            and not snapshot.source_truncated
            and snapshot.source_text
            and snapshot.source_text == self.compiled_source_text
            and snapshot.target_pid == self.compiled_pid
            and designer_document(snapshot.designer_window_title) == designer_document(self.compiled_designer_title)
            and snapshot.designer_fingerprint == self.compiled_designer_fingerprint
            and snapshot.active_control == "editor"
            and snapshot.selection.start is not None
            and self.selected_generated_name == "main.py"
        ):
            self._map_editor_caret(snapshot, label)

    def _update_designer_status(self, snapshot: ObserverSnapshot, context_matches: bool, pending_source: bool) -> None:
        status_now = self.status.cget("text")
        persistent_status = status_now.startswith(_PERSISTENT_STATUS)
        if not pending_source and not snapshot.source_truncated and self.current_result is not None and self.compiled_designer_fingerprint:
            if context_matches and status_now.startswith("Designer changed;"):
                self.status.configure(text=getattr(self, "_last_compile_status", "Preview ready"))
            elif not context_matches and not persistent_status:
                self.status.configure(text="Designer changed; press Update Python to refresh the preview.")

    def _map_designer_selection(self, snapshot: ObserverSnapshot, observed_labels: tuple[str, ...], label: str) -> None:
        self._clear_selection()
        mappings = tuple(
            map_designer_label(
                self.current_result, observed_label,
                observed_pid=snapshot.target_pid, compiled_pid=self.compiled_pid,
                observed_title=snapshot.designer_window_title, compiled_title=self.compiled_designer_title,
            )
            for observed_label in observed_labels
        )
        matched = tuple(mapping for mapping in mappings if mapping.generated_offset is not None)
        if len(matched) == 1:
            mapping = matched[0]
            self.selected_generated_name = "main.py"
            self._show_generated("main.py")
            self._show_mapping(mapping)
            self.selection_status.configure(text=label + " | " + mapping.reason)
        else:
            reason = "Designer selection is ambiguous." if len(matched) > 1 else mappings[0].reason
            self.selection_status.configure(text=label + " | " + reason)

    def _map_editor_caret(self, snapshot: ObserverSnapshot, label: str) -> None:
        offset = utf16_to_python_offset(snapshot.source_text, snapshot.selection.start)
        if offset is None:
            self._clear_selection()
            self.selection_status.configure(text=label + " | Source caret offset is unavailable.")
            return
        mapping = map_source_offset(self.current_result, snapshot.source_text, offset)
        if mapping.generated_offset is None:
            self._clear_selection()
            self.selection_status.configure(text=label + " | " + mapping.reason)
        else:
            self._show_mapping(mapping)
            self.selection_status.configure(text=label + f" | generated block {mapping.block_index}")
