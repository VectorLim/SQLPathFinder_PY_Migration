"""Main window widget construction and placement."""

from __future__ import annotations

from .highlight import TAG_COLOURS


class LayoutMixin:
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
        for tag, colour in TAG_COLOURS:
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

    def _resize_generated_headers(self, event=None) -> None:
        width = max(160, (event.width if event is not None else self.generated_header.master.winfo_width()) - 8)
        self.generated_header.configure(wraplength=width)
        self.generated_origin.configure(wraplength=width)

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
