"""Shared primitives for resolver operand payloads.

Contains:

* Private helpers reused by all operand modules (``_quoted_args``,
  ``_operand_expr`` and its operator table).
* Structural types (``ScopeIdSource``, ``ParseChildrenFn``, ``MacroFrame``)
  that operand payloads consume when building the scope tree.
* ``ScopeNode`` — the recursive scope-tree node. It refers to
  ``MacroControlPayload`` only via a ``TYPE_CHECKING`` forward reference,
  so this module can be imported without pulling in the concrete payloads.
"""

from __future__ import annotations

import re
from collections.abc import Callable, Mapping
from dataclasses import dataclass
from typing import TYPE_CHECKING, Literal

from vg2c.frontend.models import ClassifiedBlock, SourceSpan

if TYPE_CHECKING:
    from vg2c.emitter.indent_writer import IndentWriter
    from vg2c.operands import MacroControlPayload


# ---------------------------------------------------------------------------
# Condition-expression helpers (used by conditional payloads)
# ---------------------------------------------------------------------------

_OPERATOR_TABLE: dict[str, tuple[str, str]] = {
    "EQS": ("==", "string"),
    "NES": ("!=", "string"),
    "LE": ("<=", "numeric"),
    "LT": ("<", "numeric"),
    "GE": (">=", "numeric"),
    "GT": (">", "numeric"),
    "EQ": ("==", "numeric"),
    "NE": ("!=", "numeric"),
}

_BARE_IDENT_RE = re.compile(r"^[A-Za-z_][A-Za-z0-9_]*$")


def _int_expr(expr: str) -> str:
    return f"int({expr})"


def _operand_expr(operand: str, numeric: bool, allow_bare_macro: bool) -> str:
    """Render a single condition operand as a Python expression string."""
    # Local import to avoid circular dependency at module load time.
    from vg2c.utilities._emit_helpers import normalize_macro_name
    from vg2c.utilities.macro_state import MacroState

    value = operand.strip()

    if not value:
        return _int_expr("0") if numeric else repr("")

    if value.startswith("VAR(") and value.endswith(")"):
        base = MacroState.named.render(normalize_macro_name(value[4:-1].strip()))
        return _int_expr(base) if numeric else base

    if MacroState.NAMED_PLACEHOLDER_RE.fullmatch(value):
        base = MacroState.named.render(normalize_macro_name(value))
        return _int_expr(base) if numeric else base

    if allow_bare_macro and _BARE_IDENT_RE.match(value):
        base = MacroState.named.render(normalize_macro_name(value))
        return _int_expr(base) if numeric else base

    if numeric:
        return _int_expr(repr(value))
    return repr(value)


# ---------------------------------------------------------------------------
# Option-parsing helper
# ---------------------------------------------------------------------------


def _quoted_args(value: str) -> list[str]:
    """Extract double-quoted argument strings from a UTILITIES option value."""
    return re.findall(r'"([^"]*)"', value)


# ---------------------------------------------------------------------------
# Scope-building protocol types
# ---------------------------------------------------------------------------


class ScopeIdSource:
    """Protocol-style duck type expected by ``build_scope`` methods.

    ``scope_builder._ScopeBuilderState`` satisfies this interface.
    """

    def new_scope_id(self) -> int:  # pragma: no cover
        raise NotImplementedError


# Recursive parse_children callable injected into ``build_scope``.
ParseChildrenFn = Callable[
    [
        "list[ClassifiedBlock]",  # blocks
        int,  # start index
        "set[str] | None",  # stop_tokens
        ScopeIdSource,  # state
    ],
    "tuple[list[ScopeNode], int, str | None]",
]


# ---------------------------------------------------------------------------
# Macro-frame payload (runtime metadata used by the emitter)
# ---------------------------------------------------------------------------


@dataclass(frozen=True, slots=True)
class MacroFrame:
    frame_id: int
    kind: Literal["row-iter", "if", "static-vars"]
    csv_path: str | None
    csv_headers: tuple[str, ...] | None
    named_vars: Mapping[str, str]
    positional_cursor: int
    source_span: SourceSpan


# ---------------------------------------------------------------------------
# Scope tree node
# ---------------------------------------------------------------------------


@dataclass(frozen=True, slots=True)
class ScopeNode:
    scope_id: int
    kind: Literal["program", "macro", "loop", "if", "if-branch", "else-branch", "leaf"]
    start_index: int
    end_index: int
    children: tuple[ScopeNode, ...]
    block_index: int | None
    control_payload: MacroControlPayload | None

    def emit(
        self,
        writer: IndentWriter,
        walk: Callable[[ScopeNode], None],
    ) -> None:
        """Delegate structural emission to the control payload, or walk children transparently.

        Leaf nodes are intentionally excluded — the walker handles them via
        UtilitySpec dispatch, which is not a payload concern.
        """
        if self.control_payload is not None and hasattr(
            self.control_payload, "emit_scope"
        ):
            self.control_payload.emit_scope(writer, walk, self.children)  # type: ignore[union-attr]
        else:
            # Transparent nodes: program, if-branch, else-branch
            for child in self.children:
                walk(child)
