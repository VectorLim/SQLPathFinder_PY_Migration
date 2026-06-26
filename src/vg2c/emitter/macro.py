"""Macro subsystem - storing values and substituting placeholders.

This module consolidates every emitter/runtime macro concern in one place:

- ``PLACEHOLDER_RE`` / ``NAMED_PLACEHOLDER_RE`` patterns used everywhere
  macros are detected.
- ``normalize_macro_name`` - canonical name extraction from any raw token.
- ``MacroState`` - runtime stack-based variable store. Owns its own
  substitution helpers (``substitute_sql`` for SQL bodies; ``write_file``
  for template files via the module-level ``write_file`` function).
- ``write_file`` - template-to-file writer with placeholder substitution.
- ``placeholders_to_python_expr`` / ``macro_token_to_python_expr`` -
  compile-time rewriting used by the emitter to lower placeholders to
  ``ctx.macro.named(...)`` / ``ctx.macro.positional()`` expressions.

Compile-stage *discovery* of placeholders (scope-aware analysis producing
``RuntimeMacroRef`` metadata) lives in :mod:`vg2c.resolver.macro_resolver`
because it is part of an earlier pipeline stage.
"""

from __future__ import annotations

import re
from contextlib import contextmanager
from pathlib import Path
from typing import Any, Callable, Iterator, Mapping, Protocol

__all__ = [
    "PLACEHOLDER_RE",
    "NAMED_PLACEHOLDER_RE",
    "CROSSTAB_RE",
    "MacroLookup",
    "MacroState",
    "normalize_macro_name",
    "apply_crosstab",
    "substitute_crosstab",
    "write_file",
    "placeholders_to_python_expr",
    "macro_token_to_python_expr",
]


# ---------------------------------------------------------------------------
# Placeholder patterns
# ---------------------------------------------------------------------------

PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")
CROSSTAB_RE = re.compile(
    r"CrossTab->\[\[\s*([A-Za-z_][A-Za-z0-9_]*)\s*,\s*([^;\]]+)\s*;\s*:([YyNn])\s*\]\]"
)


def _extract_selected_columns_by_alias(sql: str) -> dict[str, set[str]]:
    """Return selected ``alias.column`` refs from the first SELECT list in *sql*."""
    by_alias: dict[str, set[str]] = {}
    match = re.search(
        r"\bSELECT\b(?P<select_part>.*?)\bFROM\b", sql, flags=re.IGNORECASE | re.DOTALL
    )
    if not match:
        return by_alias

    select_part = match.group("select_part")
    col_ref_re = re.compile(
        r"\b([A-Za-z_][A-Za-z0-9_]*)\s*\.\s*(?:\[([^\]]+)\]|\"([^\"]+)\"|([A-Za-z_][A-Za-z0-9_]*))"
    )
    for col_match in col_ref_re.finditer(select_part):
        alias = col_match.group(1).lower()
        col_name = col_match.group(2) or col_match.group(3) or col_match.group(4)
        if not col_name:
            continue
        by_alias.setdefault(alias, set()).add(col_name.lower())

    return by_alias


def _ci_get(row: Mapping[str, Any], key: str) -> Any:
    """Case-insensitive mapping lookup; returns None when key is absent."""
    if key in row:
        return row[key]
    key_lower = key.lower()
    for k, v in row.items():
        if str(k).lower() == key_lower:
            return v
    return None


def apply_crosstab(
    rows: Any,
    row_keys: list[str],
    header_key: str,
    value_key: str,
) -> list[dict[str, Any]]:
    """Pivot row-oriented data into SQLPathFinder-style crosstab output.

    Args:
        rows: Iterable of row mappings or a pandas DataFrame.
        row_keys: Grouping columns (``/CTROW``).
        header_key: Dynamic column source (``/CTHEADER``).
        value_key: Dynamic value source (``/CTVALUE``).
    """
    if not row_keys or not header_key or not value_key:
        return list(rows) if rows is not None else []

    # Accept DataFrame-like inputs without importing pandas in this module.
    if hasattr(rows, "to_dict"):
        try:
            source_rows = list(rows.to_dict(orient="records"))  # type: ignore[attr-defined]
        except Exception:
            source_rows = list(rows) if rows is not None else []
    else:
        source_rows = list(rows) if rows is not None else []

    if not source_rows:
        return []

    grouped: dict[tuple[Any, ...], dict[str, Any]] = {}
    dynamic_cols: list[str] = []

    for row in source_rows:
        if not isinstance(row, Mapping):
            continue

        key_tuple = tuple(_ci_get(row, key) for key in row_keys)
        out = grouped.get(key_tuple)
        if out is None:
            out = {key: _ci_get(row, key) for key in row_keys}
            grouped[key_tuple] = out

        header_value = _ci_get(row, header_key)
        if header_value is None or str(header_value) == "":
            continue

        dynamic_name = str(header_value)
        if dynamic_name not in dynamic_cols:
            dynamic_cols.append(dynamic_name)

        val = _ci_get(row, value_key)
        existing = out.get(dynamic_name)
        # SQLPathFinder crosstab uses MAX-like behavior for duplicate cells.
        if existing is None or str(val) > str(existing):
            out[dynamic_name] = val

    result: list[dict[str, Any]] = []
    for out in grouped.values():
        for name in dynamic_cols:
            out.setdefault(name, "")
        result.append(out)

    return result


def substitute_crosstab(
    sql: str, alias_columns_lookup: Callable[[str], list[str]] | None = None
) -> str:
    """Expand SQLPathFinder ``CrossTab->[[alias,instance;:Y/N]]`` tokens.

    ``:Y`` expands to SQL projection expressions (``alias.[col] AS [col]``).
    ``:N`` expands to a comma-joined header list (``col1,col2``).
    """
    if alias_columns_lookup is None or "CrossTab->[[" not in sql:
        return sql

    selected_by_alias = _extract_selected_columns_by_alias(sql)

    def _replace(match: re.Match[str]) -> str:
        alias = match.group(1)
        mode = match.group(3).upper()
        all_cols = alias_columns_lookup(alias)
        selected = selected_by_alias.get(alias.lower(), set())
        dynamic_cols = [c for c in all_cols if c.lower() not in selected]

        if not dynamic_cols:
            return "NULL AS [CROSSTAB_EMPTY]" if mode == "Y" else "CROSSTAB_EMPTY"

        if mode == "N":
            return ",".join(dynamic_cols)

        return "\n         ,".join(f"{alias}.[{c}] AS [{c}]" for c in dynamic_cols)

    return CROSSTAB_RE.sub(_replace, sql)


def normalize_macro_name(raw: str) -> str:
    """Return the canonical macro name for *raw* (strips ``<<< >>>``, uppercases)."""
    name = raw.strip()
    if name.startswith("<<<") and name.endswith(">>>"):
        name = name[3:-3]
    return name.strip().upper()


class MacroLookup(Protocol):
    """Minimal interface used by template / SQL substitution."""

    def named(self, name: str) -> str: ...

    def positional(self) -> str: ...


# ---------------------------------------------------------------------------
# Runtime state
# ---------------------------------------------------------------------------


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

    def substitute_sql(
        self,
        sql: str,
        crosstab_alias_columns: Callable[[str], list[str]] | None = None,
    ) -> str:
        """Substitute ``<<<NAME>>>`` placeholders in *sql* using current state.

        Positional ``<<>>`` placeholders are intentionally left alone in SQL
        bodies — they have no defined semantic for raw SQL text.
        """
        if "<<<" in sql:
            sql = NAMED_PLACEHOLDER_RE.sub(
                lambda m: self.named(normalize_macro_name(m.group(1))),
                sql,
            )
        return substitute_crosstab(sql, alias_columns_lookup=crosstab_alias_columns)

    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        """Write a template to disk using this macro state for substitutions."""
        write_file(path, template, vars=vars, macro_state=self)

    def eval_condition(self, lhs: str, op: str, rhs: str) -> bool:
        """Legacy condition evaluation kept for backward compatibility."""
        lhs_val = self.named(lhs) if lhs.startswith("VAR(") else lhs
        rhs_val = self.named(rhs) if rhs.startswith("VAR(") else rhs
        return lhs_val == rhs_val

    # ------------------------------------------------------------------
    # Frame management (called by context.py / macro_scope)
    # ------------------------------------------------------------------

    def push_frame(self, named: dict[str, str] | None = None) -> None:
        frame: dict[str, str] = {}
        for k, v in (named or {}).items():
            if k is None:
                continue  # Guard malformed DictReader rows (e.g., blank header line)
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


# ---------------------------------------------------------------------------
# Runtime template substitution
# ---------------------------------------------------------------------------


def write_file(
    path: str,
    template: str,
    vars: dict[str, str] | None,
    macro_state: MacroLookup | None = None,
) -> None:
    """Write *template* to *path*, substituting ``<<<NAME>>>`` / ``<<>>`` placeholders.

    Named placeholders are resolved against *vars* when supplied, otherwise
    against *macro_state*. Positional placeholders only resolve when
    *macro_state* is provided.
    """

    def _lookup(name: str) -> str:
        key = normalize_macro_name(name)
        if vars is not None:
            return vars.get(key, "")
        if macro_state is not None:
            return macro_state.named(key)
        return ""

    def _replace(match: re.Match[str]) -> str:
        named = match.group(1)
        if named is not None:
            return _lookup(named)
        if macro_state is not None:
            return macro_state.positional()
        return ""

    content = PLACEHOLDER_RE.sub(_replace, template)
    content = content.lstrip("\n")

    out = Path(path)
    out.parent.mkdir(parents=True, exist_ok=True)
    out.write_text(content, encoding="utf-8")


# ---------------------------------------------------------------------------
# Compile-time placeholder rewriting (used by the emitter)
# ---------------------------------------------------------------------------


def macro_token_to_python_expr(raw: str) -> str:
    """Translate a bare ``<<<NAME>>>`` or ``NAME`` token to ``ctx.macro.named("NAME")``."""
    return f'ctx.macro.named("{normalize_macro_name(raw)}")'


def placeholders_to_python_expr(text: str) -> str:
    """Rewrite ``<<<NAME>>>`` / ``<<>>`` inside *text* to a Python expression.

    The result is a single Python source expression: a quoted literal, a
    runtime call, or a ``+``-concatenation of the two. An empty *text*
    returns ``repr("")`` to keep call sites uniform.
    """
    if not text:
        return repr("")

    parts: list[str] = []
    cursor = 0

    for match in PLACEHOLDER_RE.finditer(text):
        literal = text[cursor : match.start()]
        if literal:
            parts.append(repr(literal))

        named = match.group(1)
        if named is not None:
            parts.append(macro_token_to_python_expr(named))
        else:
            parts.append("ctx.macro.positional()")

        cursor = match.end()

    tail = text[cursor:]
    if tail:
        parts.append(repr(tail))

    if not parts:
        return repr(text)
    if len(parts) == 1:
        return parts[0]
    return " + ".join(parts)
