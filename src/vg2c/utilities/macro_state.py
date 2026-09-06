"""MacroState - runtime macro variable storage and substitution."""

from __future__ import annotations

import re
from collections.abc import Iterator
from contextlib import contextmanager
from pathlib import Path

from vg2c.emitter.models import CodeExpr, emittable
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import (
    normalize_macro_name,
    resolve_path,
    strip_quotes,
)


class MacroState(EmitterUtility):
    """Stack of variable frames; lookups walk top-to-bottom."""

    utility_name = "macro"
    handles = (Kind.MACRO_CONTROL,)

    PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
    NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")
    _MACRO_CONTROL_TOKEN_RE = re.compile(
        r"^\s*\{(START-MACRO|END-MACRO|IF-THEN|ELSE|END-IF|RUN-LOOP|END-LOOP)\}",
        re.IGNORECASE,
    )

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        utilities = options.lookup.get("UTILITIES", "")
        if MacroState._MACRO_CONTROL_TOKEN_RE.match(utilities):
            return Kind.MACRO_CONTROL, "/UTILITIES is a macro control token"
        return None

    @classmethod
    def to_py_expr(cls, value: str | None) -> str:
        if value is None:
            return "None"
        return cls.placeholders_to_python_expr(strip_quotes(value))

    @classmethod
    def to_code_expr(cls, value: str | None) -> CodeExpr:
        if value is None:
            return CodeExpr("None")
        text = strip_quotes(value)
        source = cls.placeholders_to_python_expr(text)
        if cls.PLACEHOLDER_RE.search(text):
            return CodeExpr(source)
        return CodeExpr(source, text)

    @classmethod
    def list_code_expr(cls, values: list[str]) -> CodeExpr:
        items = [cls.to_code_expr(value) for value in values]
        source = "[" + ", ".join(item.source for item in items) + "]"
        if all(item.has_value for item in items):
            return CodeExpr(source, [item.value for item in items])
        return CodeExpr(source)

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
                parts.append(cls.named.render(normalize_macro_name(named)))
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
        return "macro_control", ["pass"]

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
