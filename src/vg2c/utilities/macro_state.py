"""MacroState - runtime macro variable storage and substitution."""

from __future__ import annotations

import re
from contextlib import contextmanager
from pathlib import Path
from typing import Iterator, Protocol

from vg2c.emitter.models import emittable
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import (
    normalize_macro_name,
    resolve_path,
    strip_quotes,
)
from vg2c.kind import Kind
from vg2c.operands import RowsInFile


class MacroState(EmitterUtility):
    """Stack of variable frames; lookups walk top-to-bottom."""

    utility_name = "macro"
    handles = (Kind.MACRO_CONTROL,)

    PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
    NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        utilities = options.lookup.get("UTILITIES")
        if utilities and utilities.lstrip().startswith("{"):
            return Kind.MACRO_CONTROL, "/UTILITIES starts with {"
        return None

    @classmethod
    def to_py_expr(cls, value: str | None) -> str:
        if value is None:
            return "None"
        return cls.placeholders_to_python_expr(strip_quotes(value))

    @classmethod
    def placeholders_to_python_expr(cls, text: str) -> str:
        if not text:
            return repr("")

        parts: list[str] = []
        cursor = 0

        for match in cls.PLACEHOLDER_RE.finditer(text):
            literal = text[cursor : match.start()]
            if literal:
                parts.append(repr(literal))

            named = match.group(1)
            if named is not None:
                parts.append(cls.named.render(repr(normalize_macro_name(named))))
            else:
                parts.append(cls.positional.render())

            cursor = match.end()

        tail = text[cursor:]
        if tail:
            parts.append(repr(tail))

        if not parts:
            return repr(text)
        if len(parts) == 1:
            return parts[0]
        return " + ".join(parts)

    @classmethod
    def emit_block(cls, block) -> tuple[str, list[str]] | None:
        payload = block.control_payload
        if not isinstance(payload, RowsInFile):
            return "macro_control", ["pass"]

        csv_path_expr = cls.to_py_expr(payload.csv_path)
        set_name = payload.var_name.upper()

        from vg2c.utilities.csv_io import CsvIO

        row_count_call = CsvIO.row_count.render(csv_path_expr)
        stmt = cls.set_named.render(repr(set_name), f"str({row_count_call})")
        return "rows_in_file", [stmt]

    def __init__(self) -> None:
        self._stack: list[dict[str, str]] = [{}]

    @emittable
    def named(self, name: str) -> str:
        key = name.upper()
        for frame in reversed(self._stack):
            if key in frame:
                return frame[key]
        return ""

    @emittable
    def set_named(self, name: str, value: str) -> None:
        self._stack[-1][name.upper()] = value

    @emittable
    def positional(self) -> str:
        frame = self._stack[-1]
        cursor = frame.get("__cursor__", 0)
        pos_list: list[str] = frame.get("__positional__", [])  # type: ignore[assignment]
        if isinstance(pos_list, list) and cursor < len(pos_list):
            frame["__cursor__"] = cursor + 1
            return pos_list[cursor]
        return ""

    @emittable
    def substitute(self, text: str, vars: dict[str, str] | None = None) -> str:
        if not text:
            return ""

        def _lookup(name: str) -> str:
            key = normalize_macro_name(name)
            if vars is not None:
                return vars.get(key, "")
            return self.named(key)

        def _replace(match: re.Match[str]) -> str:
            named = match.group(1)
            if named is not None:
                return _lookup(named)
            return self.positional()

        content = self.PLACEHOLDER_RE.sub(_replace, text)
        return content.lstrip("\n")

    def resolve_file_path(self, raw_path: str) -> Path:
        """Resolve a possibly-macro path with local basename fallback for abs paths."""
        if not raw_path:
            return Path("")
        resolved = self.substitute(raw_path)
        return resolve_path(resolved)

    @emittable
    def eval_condition(self, lhs: str, op: str, rhs: str) -> bool:
        lhs_val = self.named(lhs) if lhs.startswith("VAR(") else lhs
        rhs_val = self.named(rhs) if rhs.startswith("VAR(") else rhs
        return lhs_val == rhs_val

    def push_frame(self, named: dict[str, str] | None = None) -> None:
        frame: dict[str, str] = {}
        for k, v in (named or {}).items():
            if k is None:
                continue
            frame[k.upper()] = str(v)
        self._stack.append(frame)

    def pop_frame(self) -> None:
        if len(self._stack) > 1:
            self._stack.pop()

    @emittable
    @contextmanager
    def scope(self, row: dict[str, str] | None = None) -> Iterator[None]:
        self.push_frame(named=row)
        try:
            yield
        finally:
            self.pop_frame()
