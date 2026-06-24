"""MacroState — stack-based named variable store for macro scopes."""

from __future__ import annotations

from contextlib import contextmanager
from typing import Iterator


class MacroState:
    """Stack of variable frames; lookups walk top-to-bottom (most-recent wins)."""

    def __init__(self) -> None:
        # A list of dicts; last element is the top (current) frame.
        self._stack: list[dict[str, str]] = [{}]

    # ------------------------------------------------------------------
    # Public API (matches what the emitter calls)
    # ------------------------------------------------------------------

    def named(self, name: str) -> str:
        """Return the value of a named variable, "" if not set."""
        key = name.upper()
        for frame in reversed(self._stack):
            if key in frame:
                return frame[key]
        return ""

    def set_named(self, name: str, value: str) -> None:
        """Write *value* into the current (top) frame."""
        self._stack[-1][name.upper()] = value

    def positional(self) -> str:
        """Return the next positional variable from the top frame (auto-advances)."""
        frame = self._stack[-1]
        cursor = frame.get("__cursor__", 0)
        pos_list: list[str] = frame.get("__positional__", [])  # type: ignore[assignment]
        if isinstance(pos_list, list) and cursor < len(pos_list):
            frame["__cursor__"] = cursor + 1
            return pos_list[cursor]
        return ""

    # ------------------------------------------------------------------
    # Frame management (called by context.py / macro_scope)
    # ------------------------------------------------------------------

    def push_frame(self, named: dict[str, str] | None = None) -> None:
        frame: dict[str, str] = {}
        for k, v in (named or {}).items():
            if k is None:
                continue  # guard: malformed DictReader row (e.g. blank header line)
            frame[k.upper()] = str(v)
        self._stack.append(frame)

    def pop_frame(self) -> None:
        if len(self._stack) > 1:  # never remove the base frame
            self._stack.pop()

    @contextmanager
    def scope(self, row: dict[str, str] | None = None) -> Iterator[None]:
        """Context manager that pushes a new frame (optionally pre-populated with *row*)."""
        self.push_frame(named=row)
        try:
            yield
        finally:
            self.pop_frame()
