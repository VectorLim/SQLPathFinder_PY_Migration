"""Compile submission and generated/source preview panes."""

from __future__ import annotations

from pathlib import Path

from ..compiler import CompilationOutcome
from ..export import export_preview
from ..models import NativeSnapshot
from ..native import inspect_native
from ..source import generated_asset_links
from .highlight import highlight_ranges, mode_for_name


class PreviewMixin:
    def open_preview(self) -> None:
        selected = self.filedialog.askopenfilename(
            filetypes=[("Preview source", "*.txt *.spfsql"), ("All files", "*.*")]
        )
        if selected:
            self._invalidate_requests()
            self._detach_native_target()
            path = Path(selected)
            self._add_path(path)
            self._load_path(path)

    def _load_path(self, path: Path) -> None:
        if path.suffix.lower() == ".vg2":
            self.current_native_path = path
            self._match_native_target(path)
            try:
                info: NativeSnapshot = inspect_native(path)
                self.current_source = None
                self._set_source(
                    f"Native snapshot: {info.path}\nVERSION: {info.version or 'unknown'}\n"
                    f"Sections: {', '.join(info.sections) or 'none'}\nLines: {info.lines}"
                )
                self._select_tab(getattr(self, "source_tab", None))
                if self._selected_target is not None:
                    self.status.configure(
                        text="VG2 selected; press Update Python to refresh from SQLPathFinder."
                    )
            except OSError as exc:
                self.status.configure(text=f"Native snapshot unavailable: {exc}")
            return
        try:
            text = path.read_text(encoding="utf-8", errors="replace")
        except OSError as exc:
            self.status.configure(text=f"Preview source unavailable: {exc}")
            return
        self.current_source = path
        self.current_identity = self._submit_compile(path, text, None, "", "")
        self._set_source(text)
        self._select_tab(getattr(self, "source_tab", None))
        self.status.configure(text=f"Compiling {path.name}…")

    def _submit_compile(
        self,
        path: Path,
        text: str,
        pid: int | None,
        title: str,
        fingerprint: str,
    ) -> int:
        self._pending_pid = pid
        self._pending_designer_title = title
        self._pending_designer_fingerprint = fingerprint
        return self.worker.submit(path, text)

    def _show_generated(self, name: str, *, select_tab: bool = True) -> None:
        if self.current_result is None and name not in self.runtime_files:
            return
        text = self._artifact_text(name)
        self._set_generated(text, self._mode_for_name(name))
        if self.compiled_source_text:
            self._set_source(self.compiled_source_text)
        header = getattr(self, "generated_header", None)
        if header is not None:
            label = (
                "Runtime library" if name in self.runtime_files else "Generated preview"
            )
            header.configure(text=f"{label}: {name}")
        origin = getattr(self, "generated_origin", None)
        if origin is not None:
            source = (
                "installed vg2c/runtime (read-only)"
                if name in self.runtime_files
                else self.compiled_origin or "retained preview"
            )
            origin.configure(text=f"Origin: {source}")
        if select_tab:
            self._select_tab(getattr(self, "generated_tab", None))
        self._sync_generated_selection(name)

    def _artifact_text(self, name: str) -> str:
        if name == "main.py":
            return self.current_result.emitted.source
        if name in self.current_assets:
            return self.current_assets[name]
        return self.runtime_files[name]

    @staticmethod
    def _mode_for_name(name: str) -> str:
        return mode_for_name(name)

    def _on_outcome(self, outcome: CompilationOutcome) -> None:
        if outcome.identity != self.current_identity:
            return
        refresh_completed = outcome.identity == self._refresh_compile_identity
        if refresh_completed:
            self._refresh_compile_identity = None
            self._poll_suspended = False
            self._refresh_busy = False
            if hasattr(getattr(self, "_observer_worker", None), "invalidate_frames"):
                self._observer_worker.invalidate_frames()
        if outcome.error or outcome.result is None:
            self._set_diagnostics(
                "Compiler error\n\n"
                + (outcome.error or "No compiler details were returned.")
            )
            self._select_tab(getattr(self, "diagnostics_tab", None))
            self._last_compile_status = "Compiler error; last good preview retained"
            self.status.configure(text=self._last_compile_status)
            self._run_queued_update()
            return
        self.current_result = outcome.result
        self.compiled_source_text = outcome.source_text
        self.compiled_pid = self._pending_pid
        self.compiled_designer_title = self._pending_designer_title
        self.compiled_designer_fingerprint = self._pending_designer_fingerprint
        if outcome.source_path.name.lower() == "sqlpathfinder-preview.txt":
            self.compiled_origin = (
                "Live Show Query: " + self.compiled_designer_title
                if self.compiled_designer_title
                else "Live Show Query"
            )
        else:
            self.compiled_origin = "Source: " + str(outcome.source_path)
        self.current_assets = dict(outcome.result.emitted.assets)
        if (
            self.selected_generated_name != "main.py"
            and self.selected_generated_name not in self.current_assets
            and self.selected_generated_name not in self.runtime_files
        ):
            self.selected_generated_name = "main.py"
        self._populate_generated_tree()
        for copy in self.edit_copies:
            copy.source_refreshed(outcome.identity)
        self._show_generated(self.selected_generated_name, select_tab=refresh_completed)
        diagnostics = []
        for item in outcome.result.diagnostics:
            location = f" ({item.location})" if item.location else ""
            diagnostics.append(f"{item.level}: [{item.code}] {item.message}{location}")
        self._set_diagnostics(
            "\n\n".join(diagnostics) if diagnostics else "No diagnostics."
        )
        if diagnostics:
            self._last_compile_status = (
                f"Compiled with diagnostics: {len(diagnostics)} item(s)"
            )
        else:
            self._last_compile_status = "Preview ready"
        if any(copy.pending_identity is not None for copy in self.edit_copies):
            self._last_compile_status += "; independent edit copy preserved"
        self.status.configure(text=self._last_compile_status)
        self._run_queued_update()

    def _set_generated(self, text: str, mode: str) -> None:
        links = (
            self._asset_links()
            if self.selected_generated_name == "main.py"
            and self.current_result is not None
            else None
        )
        render_key = (text, mode, id(links) if links else None)
        if getattr(self, "_generated_render_key", None) == render_key:
            self.generated.configure(cursor="")
            self.generated.tag_remove("selection", "1.0", "end")
            return
        self.generated.configure(state="normal")
        self.generated.configure(cursor="")
        self.generated.delete("1.0", "end")
        self.generated.tag_remove("selection", "1.0", "end")
        self.generated.insert("1.0", text)
        for tag, start, end in highlight_ranges(text, mode):
            self.generated.tag_add(tag, f"1.0+{start}c", f"1.0+{end}c")
        if links is not None:
            for link in links:
                start, end = link.source_range
                self.generated.tag_add("asset_link", f"1.0+{start}c", f"1.0+{end}c")
        self.generated.configure(state="disabled")
        self._generated_render_key = render_key

    def _asset_links(self):
        result = self.current_result
        cached = getattr(self, "_asset_link_cache", None)
        if cached is not None and cached[0] is result:
            return cached[1]
        links = generated_asset_links(result)
        self._asset_link_cache = (result, links)
        return links

    def _enter_asset_link(self, _event) -> None:
        self.generated.configure(cursor="hand2")

    def _leave_asset_link(self, _event) -> None:
        self.generated.configure(cursor="")

    def _follow_asset_link(self, event):
        if self.selected_generated_name != "main.py" or self.current_result is None:
            return None
        index = self.generated.index(f"@{event.x},{event.y}")
        offset = len(self.generated.get("1.0", index))
        matches = tuple(
            link.name
            for link in self._asset_links()
            if link.source_range[0] <= offset < link.source_range[1]
        )
        name = matches[0] if len(matches) == 1 else None
        if name is not None:
            self.selected_generated_name = name
            self._show_generated(name)
            return "break"
        return None

    def _set_source(self, text: str) -> None:
        self.source.configure(state="normal")
        self.source.delete("1.0", "end")
        self.source.insert("1.0", text)
        self.source.configure(state="disabled")

    def _clear_selection(self) -> None:
        self.generated.tag_remove("selection", "1.0", "end")
        self._active_mapping = None

    def _show_mapping(self, mapping) -> None:
        if mapping.generated_range is None or mapping.generated_offset is None:
            return
        if mapping == self._active_mapping and self.generated.tag_ranges("selection"):
            return
        self.generated.tag_remove("selection", "1.0", "end")
        self.generated.tag_add(
            "selection",
            f"1.0+{mapping.generated_range[0]}c",
            f"1.0+{mapping.generated_range[1]}c",
        )
        self.generated.see(f"1.0+{mapping.generated_offset}c")
        self._active_mapping = mapping

    def export(self) -> None:
        if self.current_result is None:
            self.status.configure(text="Compile a preview before exporting.")
            return
        selected = self.filedialog.askdirectory(mustexist=False)
        if not selected:
            return
        try:
            destination = export_preview(self.current_result, Path(selected))
        except (FileExistsError, OSError, ValueError) as exc:
            self.messagebox.showerror("Export refused", str(exc))
            return
        self.status.configure(text=f"Exported generated snapshot to {destination}")
