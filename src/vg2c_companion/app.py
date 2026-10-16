"""Tk companion for SQLPathFinder observation and generated-code updates."""

from __future__ import annotations

import keyword
import queue
import re
import threading
import time
from pathlib import Path

from .compiler import CompilationOutcome, CompilationWorker, runtime_sources
from .export import export_preview, save_copy
from .models import EditCopy, NativeSnapshot, ObserverSnapshot
from .native import inspect_native
from .observer import NativeTarget, ObserverClient, ObserverWorker, RefreshResult
from .source import (
    designer_document,
    is_prepost_child,
    generated_asset_links,
    map_designer_label,
    map_generated_offset_to_asset,
    map_source_offset,
    utf16_to_python_offset,
)


def highlight_ranges(text: str, mode: str = "python") -> list[tuple[str, int, int]]:
    """Return simple lexical tags for a generated code or asset pane."""
    tags: list[tuple[str, int, int]] = []
    if mode == "python":
        import tokenize
        from io import StringIO

        line_starts = [0]
        for index, character in enumerate(text):
            if character == "\n":
                line_starts.append(index + 1)
        try:
            for token in tokenize.generate_tokens(StringIO(text).readline):
                start = line_starts[min(token.start[0] - 1, len(line_starts) - 1)] + token.start[1]
                end = line_starts[min(token.end[0] - 1, len(line_starts) - 1)] + token.end[1]
                if token.type == tokenize.NAME and keyword.iskeyword(token.string):
                    tags.append(("keyword", start, end))
                elif token.type == tokenize.STRING:
                    tags.append(("string", start, end))
                elif token.type == tokenize.COMMENT:
                    tags.append(("comment", start, end))
        except (IndentationError, tokenize.TokenError):
            pass
    else:
        pattern = re.compile(
            r"--[^\n]*|/\*.*?\*/|<!--[\s\S]*?-->|#[^\n]*|"
            r"'(?:''|[^'])*'|\"(?:\"\"|[^\"])*\"",
            re.MULTILINE,
        )
        for match in pattern.finditer(text):
            value = match.group(0)
            tag = "comment" if value.startswith(("--", "/*", "<!--")) else "string"
            tags.append((tag, match.start(), match.end()))
        if mode == "sql":
            for match in re.finditer(r"\b(?:SELECT|FROM|WHERE|JOIN|LEFT|RIGHT|INNER|OUTER|ON|GROUP|ORDER|BY|AS|INSERT|UPDATE|DELETE|CREATE|DROP|AND|OR|NOT|NULL)\b", text, re.IGNORECASE):
                tags.append(("keyword", match.start(), match.end()))
        elif mode == "html":
            for match in re.finditer(r"<!--[\s\S]*?-->|</?[A-Za-z][^>]*>", text):
                if not match.group(0).startswith("<!--"):
                    tags.append(("markup", match.start(), match.end()))
        elif mode == "css":
            for match in re.finditer(r"#[0-9A-Fa-f]{3,8}\b|[-A-Za-z_][\w-]*(?=\s*:)", text):
                tags.append(("number" if match.group(0).startswith("#") else "keyword", match.start(), match.end()))
    return tags


class CompanionApp:
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

    def _build(self) -> None:
        tk, ttk = self.tk, self.ttk
        self.root.title("SQLPathFinder companion")
        self.root.geometry("960x650")
        toolbar = ttk.Frame(self.root)
        toolbar.pack(fill="x", padx=6, pady=6)
        primary = ttk.Frame(toolbar)
        primary.pack(fill="x")
        ttk.Button(primary, text="Open preview", command=self.open_preview).pack(side="left")
        self._refresh_button = ttk.Button(primary, text="Update Python", command=self.update_python)
        self._refresh_button.pack(side="left")
        ttk.Button(primary, text="Edit copy", command=self.edit_copy).pack(side="left", padx=4)
        ttk.Button(primary, text="Export generated", command=self.export).pack(side="left")
        ttk.Button(primary, text="Quit", command=self.close).pack(side="right")
        target_row = ttk.Frame(toolbar)
        target_row.pack(fill="x", pady=(5, 0))
        ttk.Label(target_row, text="Open document:").pack(side="left", padx=(0, 3))
        self.target_combo = ttk.Combobox(target_row, state="readonly", width=42)
        self.target_combo.pack(side="left", fill="x", expand=True)
        self.target_combo.bind("<<ComboboxSelected>>", self._select_target)
        ttk.Button(target_row, text="Refresh documents", command=self.refresh_targets).pack(side="left", padx=(5, 0))
        panes = ttk.Panedwindow(self.root, orient="horizontal")
        panes.pack(fill="both", expand=True, padx=6)
        left = ttk.Frame(panes, width=240)
        right = ttk.Frame(panes)
        panes.add(left, weight=0)
        panes.add(right, weight=1)
        self.tree = ttk.Treeview(left, show="tree", selectmode="browse")
        tree_scroll = ttk.Scrollbar(left, orient="vertical", command=self.tree.yview)
        tree_horizontal = ttk.Scrollbar(left, orient="horizontal", command=self.tree.xview)
        self.tree.configure(yscrollcommand=tree_scroll.set)
        self.tree.configure(xscrollcommand=tree_horizontal.set)
        left.grid_propagate(False)
        self.tree.grid(row=0, column=0, sticky="nsew")
        tree_scroll.grid(row=0, column=1, sticky="ns")
        tree_horizontal.grid(row=1, column=0, sticky="ew")
        left.grid_rowconfigure(0, weight=1)
        left.grid_columnconfigure(0, weight=1)
        from tkinter import font as tkfont

        self._tree_font = tkfont.nametofont("TkDefaultFont")
        self._tree_indent = 18
        self.tree.column("#0", stretch=False)
        self.tree.insert("", "end", iid="sources", text="Sources", open=True)
        self._resize_tree_column()
        self.tree.bind("<<TreeviewSelect>>", self._select_file)
        self.notebook = ttk.Notebook(right)
        self.notebook.pack(fill="both", expand=True)
        generated_tab = ttk.Frame(self.notebook)
        source_tab = ttk.Frame(self.notebook)
        diagnostics_tab = ttk.Frame(self.notebook)
        self.generated_tab = generated_tab
        self.source_tab = source_tab
        self.diagnostics_tab = diagnostics_tab
        self.notebook.add(generated_tab, text="Generated code")
        self.notebook.add(source_tab, text="Source")
        self.notebook.add(diagnostics_tab, text="Diagnostics")
        self.generated_header = ttk.Label(generated_tab, text="Generated preview", anchor="w")
        self.generated_header.pack(fill="x", padx=4, pady=(4, 0))
        self.generated_origin = ttk.Label(generated_tab, text="", anchor="w", justify="left")
        self.generated_origin.pack(fill="x", padx=4, pady=(0, 2))
        generated_tab.bind("<Configure>", self._resize_generated_headers)
        generated_frame = ttk.Frame(generated_tab)
        generated_frame.pack(fill="both", expand=True)
        self.generated = tk.Text(
            generated_frame,
            wrap="none",
            state="disabled",
            undo=False,
            font=("Consolas", 10),
        )
        generated_y = ttk.Scrollbar(generated_frame, orient="vertical", command=self.generated.yview)
        generated_x = ttk.Scrollbar(generated_frame, orient="horizontal", command=self.generated.xview)
        self.generated.configure(yscrollcommand=generated_y.set, xscrollcommand=generated_x.set)
        self.generated.grid(row=0, column=0, sticky="nsew")
        generated_y.grid(row=0, column=1, sticky="ns")
        generated_x.grid(row=1, column=0, sticky="ew")
        generated_frame.rowconfigure(0, weight=1)
        generated_frame.columnconfigure(0, weight=1)
        for tag, colour in (("keyword", "#005cc5"), ("string", "#a31515"), ("comment", "#6a737d"), ("markup", "#795da3"), ("number", "#008080")):
            self.generated.tag_configure(tag, foreground=colour)
        self.generated.tag_configure("selection", background="#fff2a8")
        self.generated.tag_configure("asset_link", foreground="#005cc5", underline=True)
        self.generated.tag_bind("asset_link", "<Enter>", self._enter_asset_link)
        self.generated.tag_bind("asset_link", "<Leave>", self._leave_asset_link)
        self.generated.tag_bind("asset_link", "<Button-1>", self._follow_asset_link)
        ttk.Label(source_tab, text="Source selection / status").pack(anchor="w", padx=4, pady=(4, 0))
        self.selection_status = ttk.Label(source_tab, text="No live selection", anchor="w")
        self.selection_status.pack(fill="x", padx=4)
        source_frame = ttk.Frame(source_tab)
        source_frame.pack(fill="both", expand=True)
        self.source = tk.Text(source_frame, wrap="none", height=14, state="disabled", font=("Consolas", 9))
        source_y = ttk.Scrollbar(source_frame, orient="vertical", command=self.source.yview)
        source_x = ttk.Scrollbar(source_frame, orient="horizontal", command=self.source.xview)
        self.source.configure(yscrollcommand=source_y.set, xscrollcommand=source_x.set)
        self.source.grid(row=0, column=0, sticky="nsew")
        source_y.grid(row=0, column=1, sticky="ns")
        source_x.grid(row=1, column=0, sticky="ew")
        source_frame.rowconfigure(0, weight=1)
        source_frame.columnconfigure(0, weight=1)
        diagnostics_frame = ttk.Frame(diagnostics_tab)
        diagnostics_frame.pack(fill="both", expand=True, padx=4, pady=4)
        self.diagnostics = tk.Text(
            diagnostics_frame,
            wrap="word",
            state="disabled",
            height=8,
            font=("Consolas", 9),
        )
        diagnostics_scroll = ttk.Scrollbar(diagnostics_frame, orient="vertical", command=self.diagnostics.yview)
        self.diagnostics.configure(yscrollcommand=diagnostics_scroll.set)
        self.diagnostics.grid(row=0, column=0, sticky="nsew")
        diagnostics_scroll.grid(row=0, column=1, sticky="ns")
        diagnostics_frame.rowconfigure(0, weight=1)
        diagnostics_frame.columnconfigure(0, weight=1)
        self._set_diagnostics("No diagnostics.")
        self.status = ttk.Label(self.root, text="Ready", anchor="w")
        self.status.pack(fill="x", padx=6, pady=6)

    def _select_tab(self, tab) -> None:
        notebook = getattr(self, "notebook", None)
        if notebook is not None and tab is not None:
            notebook.select(tab)

    def _set_diagnostics(self, text: str) -> None:
        diagnostics = getattr(self, "diagnostics", None)
        if diagnostics is None:
            return
        diagnostics.configure(state="normal")
        diagnostics.delete("1.0", "end")
        diagnostics.insert("1.0", text)
        diagnostics.configure(state="disabled")

    def _add_path(self, path: Path) -> None:
        path = Path(path)
        if not self.tree.exists("sources"):
            self.tree.insert("", "end", iid="sources", text="Sources", open=True)
        iid = f"input:{len(self._tree_paths)}"
        self._tree_paths[iid] = path
        self.tree.insert("sources", "end", iid=iid, text=path.name)
        self._resize_tree_column()

    def _resize_tree_column(self) -> None:
        font = getattr(self, "_tree_font", None)
        if font is None:
            return
        widths = []

        def visit(parent: str) -> None:
            for iid in self.tree.get_children(parent):
                text = self.tree.item(iid, "text")
                depth = 0
                ancestor = self.tree.parent(iid)
                while ancestor:
                    depth += 1
                    ancestor = self.tree.parent(ancestor)
                widths.append(font.measure(text) + depth * self._tree_indent)
                visit(iid)

        visit("")
        width = max(widths, default=font.measure("Generated preview") + self._tree_indent)
        self.tree.column("#0", width=max(220, width + self._tree_indent), stretch=False)

    def _resize_generated_headers(self, event=None) -> None:
        width = max(160, (event.width if event is not None else self.generated_header.master.winfo_width()) - 8)
        self.generated_header.configure(wraplength=width)
        self.generated_origin.configure(wraplength=width)

    def _queue_result(self, outcome: CompilationOutcome) -> None:
        try:
            self._results.put_nowait(outcome)
        except queue.Full:
            try:
                self._results.get_nowait()
            except queue.Empty:
                pass
            try:
                self._results.put_nowait(outcome)
            except queue.Full:
                pass

    def _populate_generated_tree(self) -> None:
        for iid in tuple(self._tree_assets):
            if self.tree.exists(iid):
                self.tree.delete(iid)
        self._tree_assets.clear()
        self._tree_asset_files.clear()
        entries = [
            ("generated", "Generated preview", ("main.py", *sorted(self.current_assets))),
            ("runtime", "Runtime library (installed)", tuple(self.runtime_files)),
        ]
        for root, label, names in entries:
            if not self.tree.exists(root):
                self.tree.insert("", "end", iid=root, text=label, open=True)
            self._populate_file_branch(root, names)
        self._resize_tree_column()

    def _populate_file_branch(self, root: str, names: tuple[str, ...]) -> None:
        for name in names:
            parts = name.replace("\\", "/").split("/")
            parent_iid = root
            for index, part in enumerate(parts):
                current = "/".join(parts[: index + 1])
                iid = f"asset:{current}"
                if iid not in self._tree_assets and not self.tree.exists(iid):
                    self.tree.insert(parent_iid, "end", iid=iid, text=part, open=True)
                    self._tree_assets[iid] = current
                if index == len(parts) - 1:
                    self._tree_asset_files.add(iid)
                parent_iid = iid

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
        self._next_target_scan = time.monotonic() + 2
        if not quiet:
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

    def _select_file(self, _event=None) -> None:
        selected = tuple(self.tree.selection())
        if selected == getattr(self, "_last_tree_selection", ()):
            return
        self._last_tree_selection = selected
        if not selected:
            return
        iid = selected[0]
        if iid in self._tree_paths:
            self._invalidate_requests()
            if self._tree_paths[iid].suffix.lower() != ".vg2":
                self._detach_native_target()
            self._load_path(self._tree_paths[iid])
        elif iid in self._tree_asset_files:
            self.selected_generated_name = self._tree_assets[iid]
            self._show_generated(self.selected_generated_name)

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
                    self.status.configure(text="VG2 selected; press Update Python to refresh from SQLPathFinder.")
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
            label = "Runtime library" if name in self.runtime_files else "Generated preview"
            header.configure(text=f"{label}: {name}")
        origin = getattr(self, "generated_origin", None)
        if origin is not None:
            source = (
                "installed vg2c/runtime (read-only)"
                if name in self.runtime_files else self.compiled_origin or "retained preview"
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

    def _sync_generated_selection(self, name: str) -> None:
        tree = getattr(self, "tree", None)
        if tree is None:
            return
        iid = next(
            (candidate for candidate, asset_name in self._tree_assets.items()
             if asset_name == name and candidate in self._tree_asset_files),
            None,
        )
        if iid is None or not tree.exists(iid):
            return
        selection = tuple(tree.selection())
        focused = tree.focus()
        if selection == (iid,) and focused == iid:
            return
        self._last_tree_selection = (iid,)
        tree.selection_set(iid)
        tree.focus(iid)
        tree.see(iid)

    @staticmethod
    def _mode_for_name(name: str) -> str:
        suffix = Path(name).suffix.lower()
        return {".py": "python", ".sql": "sql", ".html": "html", ".htm": "html", ".css": "css"}.get(suffix, "text")

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
            self._set_diagnostics("Compiler error\n\n" + (outcome.error or "No compiler details were returned."))
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
                if self.compiled_designer_title else "Live Show Query"
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
        self._set_diagnostics("\n\n".join(diagnostics) if diagnostics else "No diagnostics.")
        if diagnostics:
            self._last_compile_status = f"Compiled with diagnostics: {len(diagnostics)} item(s)"
        else:
            self._last_compile_status = "Preview ready"
        if any(copy.pending_identity is not None for copy in self.edit_copies):
            self._last_compile_status += "; independent edit copy preserved"
        self.status.configure(text=self._last_compile_status)
        self._run_queued_update()

    def _set_generated(self, text: str, mode: str) -> None:
        self.generated.configure(state="normal")
        self.generated.configure(cursor="")
        self.generated.delete("1.0", "end")
        self.generated.tag_remove("selection", "1.0", "end")
        self.generated.insert("1.0", text)
        for tag, start, end in highlight_ranges(text, mode):
            self.generated.tag_add(tag, f"1.0+{start}c", f"1.0+{end}c")
        if self.selected_generated_name == "main.py" and self.current_result is not None:
            for link in generated_asset_links(self.current_result):
                start, end = link.source_range
                self.generated.tag_add("asset_link", f"1.0+{start}c", f"1.0+{end}c")
        self.generated.configure(state="disabled")

    def _enter_asset_link(self, _event) -> None:
        self.generated.configure(cursor="hand2")

    def _leave_asset_link(self, _event) -> None:
        self.generated.configure(cursor="")

    def _follow_asset_link(self, event):
        if self.selected_generated_name != "main.py" or self.current_result is None:
            return None
        index = self.generated.index(f"@{event.x},{event.y}")
        offset = len(self.generated.get("1.0", index))
        name = map_generated_offset_to_asset(self.current_result, offset)
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

    def edit_copy(self) -> None:
        if self.current_result is None:
            self.status.configure(text="There is no generated preview to edit.")
            return
        name = self.selected_generated_name
        text = self._artifact_text(name)
        window = self.tk.Toplevel(self.root)
        window.title(f"Edit copy - {name}")
        frame = self.ttk.Frame(window)
        frame.pack(fill="both", expand=True)
        editor = self.tk.Text(frame, wrap="none", undo=True, font=("Consolas", 10))
        scroll = self.ttk.Scrollbar(frame, orient="vertical", command=editor.yview)
        editor.configure(yscrollcommand=scroll.set)
        editor.pack(side="left", fill="both", expand=True)
        scroll.pack(side="right", fill="y")
        for tag, colour in (("keyword", "#005cc5"), ("string", "#a31515"), ("comment", "#6a737d"), ("markup", "#795da3"), ("number", "#008080")):
            editor.tag_configure(tag, foreground=colour)
        copy = EditCopy(self.current_identity, text, name)
        self.edit_copies.append(copy)
        self._edit_windows.append((window, editor, copy))
        editor.insert("1.0", text)
        for tag, start, end in highlight_ranges(text, self._mode_for_name(name)):
            editor.tag_add(tag, f"1.0+{start}c", f"1.0+{end}c")
        editor.edit_modified(False)

        def mark_dirty(_event=None) -> None:
            if editor.edit_modified():
                copy.text = editor.get("1.0", "end-1c")
                copy.dirty = True
                editor.tag_remove("keyword", "1.0", "end")
                editor.tag_remove("string", "1.0", "end")
                editor.tag_remove("comment", "1.0", "end")
                editor.tag_remove("markup", "1.0", "end")
                editor.tag_remove("number", "1.0", "end")
                for tag, start, end in highlight_ranges(copy.text, self._mode_for_name(copy.artifact_name)):
                    editor.tag_add(tag, f"1.0+{start}c", f"1.0+{end}c")
                editor.edit_modified(False)

        editor.bind("<<Modified>>", mark_dirty)
        buttons = self.ttk.Frame(window)
        buttons.pack(fill="x")
        self.ttk.Button(buttons, text="Save copy", command=lambda: self._save_edit_copy(window, editor, copy)).pack(side="left")
        self.ttk.Button(buttons, text="Close", command=lambda: self._close_edit_copy(window, editor, copy)).pack(side="right")
        window.protocol("WM_DELETE_WINDOW", lambda: self._close_edit_copy(window, editor, copy))
        self.status.configure(text="Editing an independent copy; refresh cannot overwrite it.")

    def _save_edit_copy(self, window, editor, copy: EditCopy) -> bool:
        selected = self.filedialog.asksaveasfilename(
            title="Save new generated copy", initialfile=Path(copy.artifact_name).name
        )
        if not selected:
            return False
        protected = tuple(self.native_paths) + ((self.current_source,) if self.current_source else ())
        try:
            save_copy(Path(selected), editor.get("1.0", "end-1c"), protected)
        except (FileExistsError, OSError, ValueError) as exc:
            self.messagebox.showerror("Save refused", str(exc))
            return False
        copy.text = editor.get("1.0", "end-1c")
        copy.dirty = False
        copy.pending_identity = None
        editor.edit_modified(False)
        self.status.configure(text=f"Saved new copy: {selected}")
        return True

    def _close_edit_copy(self, window, editor, copy: EditCopy) -> None:
        if copy.dirty:
            answer = self.messagebox.askyesnocancel("Unsaved edit copy", "Save this copy before closing?")
            if answer is None:
                return
            if answer and not self._save_edit_copy(window, editor, copy):
                return
        window.destroy()
        self._edit_windows = [item for item in self._edit_windows if item[0] != window]
        self.edit_copies = [item for item in self.edit_copies if item is not copy]

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

    def _apply_snapshot(self, snapshot: ObserverSnapshot) -> None:
        if self._poll_suspended:
            return
        if self._selected_target is None:
            return
        if snapshot.target_pid != self._selected_target.target_pid:
            return
        if snapshot.designer_bounds or snapshot.bounds:
            self._position_beside(snapshot.designer_bounds or snapshot.bounds)
        self._last_pid = snapshot.target_pid
        self._last_designer_title = snapshot.designer_window_title
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
        observed_labels = tuple(dict.fromkeys(
            label_value
            for raw_value in snapshot.designer_values
            for label_value in (re.split(r"(?:=| \[| <)", raw_value, maxsplit=1)[0].replace(" focused", "").strip(),)
            if re.match(r"^[0-9]+(?:[.-][0-9A-Za-z]+)*\.\s+", label_value)
        ))
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
        status_now = self.status.cget("text")
        persistent_status = status_now.startswith((
            "Compiler error", "Compiled with diagnostics:", "Exported generated snapshot",
            "Saved new copy", "Editing an independent copy",
        ))
        if not pending_source and not snapshot.source_truncated and self.current_result is not None and self.compiled_designer_fingerprint:
            if context_matches and status_now.startswith("Designer changed;"):
                self.status.configure(text=getattr(self, "_last_compile_status", "Preview ready"))
            elif not context_matches and not persistent_status:
                self.status.configure(text="Designer changed; press Update Python to refresh the preview.")
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
            offset = utf16_to_python_offset(snapshot.source_text, snapshot.selection.start)
            if offset is None:
                self._clear_selection()
                self.selection_status.configure(text=label + " | Source caret offset is unavailable.")
            else:
                mapping = map_source_offset(self.current_result, snapshot.source_text, offset)
                if mapping.generated_offset is None:
                    self._clear_selection()
                    self.selection_status.configure(text=label + " | " + mapping.reason)
                else:
                    self._show_mapping(mapping)
                    self.selection_status.configure(text=label + f" | generated block {mapping.block_index}")

    def _position_beside(self, bounds: tuple[int, int, int, int]) -> None:
        if getattr(self, "_positioned", False):
            return
        margin = 12
        preferred_width, minimum_width, height = 960, 800, 650
        screen_width = self.root.winfo_screenwidth()
        screen_height = self.root.winfo_screenheight()
        right_x = bounds[0] + bounds[2] + margin
        right_space = screen_width - right_x - margin
        left_space = bounds[0] - (2 * margin)
        if right_space >= minimum_width:
            width = min(preferred_width, right_space)
            x = right_x
        elif left_space >= minimum_width:
            width = min(preferred_width, left_space)
            x = bounds[0] - margin - width
        else:
            width = min(preferred_width, max(640, screen_width - (2 * margin)))
            x = max(0, min(right_x, screen_width - width - margin))
        y = min(max(0, bounds[1]), max(0, screen_height - height))
        self.root.geometry(f"{width}x{height}+{x}+{y}")
        self._positioned = True

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

