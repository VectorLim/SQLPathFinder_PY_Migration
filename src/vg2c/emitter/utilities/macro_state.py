"""MacroState - runtime macro variable storage and substitution."""

from __future__ import annotations

import re
from contextlib import contextmanager
from pathlib import Path
from typing import Iterator, Protocol

from vg2c.emitter.models import EmitContext
from vg2c.emitter.utilities._base import CheckedUtilitySpec
from vg2c.emitter.utilities._emit_helpers import (
    option_to_python_expr,
    normalize_macro_name,
    resolve_path,
    PLACEHOLDER_RE,
    NAMED_PLACEHOLDER_RE,
)
from vg2c.kind import Kind
from vg2c.resolver.models import RowsInFile


class MacroLookup(Protocol):
    """Minimal interface for macro substitution."""

    def named(self, name: str) -> str: ...

    def positional(self) -> str: ...


class MacroState(CheckedUtilitySpec):
    """Stack of variable frames; lookups walk top-to-bottom."""

    utility_name = "macro"
    handles = (Kind.MACRO_CONTROL,)

    PLACEHOLDER_RE = PLACEHOLDER_RE
    NAMED_PLACEHOLDER_RE = NAMED_PLACEHOLDER_RE

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        utilities = options.lookup.get("UTILITIES")
        if utilities and utilities.lstrip().startswith("{"):
            return Kind.MACRO_CONTROL, "/UTILITIES starts with {"
        return None

    @classmethod
    @EmitContext.step_emitter
    def emit_block(cls, block) -> tuple[str, list[str]] | None:
        payload = block.control_payload
        if not isinstance(payload, RowsInFile):
            return "macro_control", ["pass"]

        csv_path_expr = option_to_python_expr(payload.csv_path)
        set_name = payload.var_name.upper()
        row_count_call = EmitContext.render_method_call(
            "csv_io",
            "row_count",
            args=(csv_path_expr,),
        )
        stmt = EmitContext.render_method_call(
            "macro",
            "set_named",
            args=(repr(set_name), f"str({row_count_call})"),
        )
        return "rows_in_file", [stmt]

    def __init__(self) -> None:
        self._stack: list[dict[str, str]] = [{}]

    def named(self, name: str) -> str:
        key = name.upper()
        for frame in reversed(self._stack):
            if key in frame:
                return frame[key]
        return ""

    def set_named(self, name: str, value: str) -> None:
        self._stack[-1][name.upper()] = value

    def positional(self) -> str:
        frame = self._stack[-1]
        cursor = frame.get("__cursor__", 0)
        pos_list: list[str] = frame.get("__positional__", [])  # type: ignore[assignment]
        if isinstance(pos_list, list) and cursor < len(pos_list):
            frame["__cursor__"] = cursor + 1
            return pos_list[cursor]
        return ""

    def substitute_sql(self, sql: str) -> str:
        if "<<<" in sql:
            sql = self.NAMED_PLACEHOLDER_RE.sub(
                lambda m: self.named(normalize_macro_name(m.group(1))),
                sql,
            )
        return sql

    def resolve_file_path(self, raw_path: str) -> Path:
        """Resolve a possibly-macro path with local basename fallback for abs paths."""
        if not raw_path:
            return Path("")
        resolved = self.substitute_sql(raw_path)
        return resolve_path(resolved)

    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
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

        content = self.PLACEHOLDER_RE.sub(_replace, template)
        content = content.lstrip("\n")

        out = Path(path)
        out.parent.mkdir(parents=True, exist_ok=True)
        out.write_text(content, encoding="utf-8")

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

    @contextmanager
    def scope(self, row: dict[str, str] | None = None) -> Iterator[None]:
        self.push_frame(named=row)
        try:
            yield
        finally:
            self.pop_frame()
