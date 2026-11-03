"""Source/generated file tree pane."""

from __future__ import annotations

from pathlib import Path


class TreeMixin:
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

    def _populate_generated_tree(self) -> None:
        entries = (
            ("generated", "Generated preview", ("main.py", *sorted(self.current_assets))),
            ("runtime", "Runtime library (installed)", tuple(self.runtime_files)),
        )
        if (
            entries == getattr(self, "_populated_tree_entries", None)
            and all(self.tree.exists(root) for root, _label, _names in entries)
        ):
            return
        for iid in tuple(self._tree_assets):
            if self.tree.exists(iid):
                self.tree.delete(iid)
        self._tree_assets.clear()
        self._tree_asset_files.clear()
        for root, label, names in entries:
            if not self.tree.exists(root):
                self.tree.insert("", "end", iid=root, text=label, open=True)
            self._populate_file_branch(root, names)
        self._resize_tree_column()
        self._populated_tree_entries = entries

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
