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
from typing import Iterator, Protocol

__all__ = [
    "PLACEHOLDER_RE",
    "NAMED_PLACEHOLDER_RE",
    "MacroLookup",
    "MacroState",
    "normalize_macro_name",
    "write_file",
    "placeholders_to_python_expr",
    "macro_token_to_python_expr",
]


# ---------------------------------------------------------------------------
# Placeholder patterns
# ---------------------------------------------------------------------------

PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")


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

    def substitute_sql(self, sql: str) -> str:
        """Substitute ``<<<NAME>>>`` placeholders in *sql* using current state.

        Positional ``<<>>`` placeholders are intentionally left alone in SQL
        bodies — they have no defined semantic for raw SQL text.
        """
        if "<<<" not in sql:
            return sql
        return NAMED_PLACEHOLDER_RE.sub(
            lambda m: self.named(normalize_macro_name(m.group(1))),
            sql,
        )

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
