"""Independent, user-owned edit copies of generated artifacts."""

from __future__ import annotations

from pathlib import Path

from ..export import save_copy
from ..models import EditCopy
from .highlight import TAG_COLOURS, highlight_ranges


class EditCopyMixin:
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
        for tag, colour in TAG_COLOURS:
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
                for tag, _colour in TAG_COLOURS:
                    editor.tag_remove(tag, "1.0", "end")
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
