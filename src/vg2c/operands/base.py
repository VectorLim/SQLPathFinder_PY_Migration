"""Shared primitives for resolver operand payloads.

Contains:

* Private helpers reused by all operand modules (``_quoted_args``,
  ``_operand_expr`` and its operator table).
* Structural types (``ScopeIdSource``, ``ParseChildrenFn``)
  that operand payloads consume when building the scope tree.
* ``ScopeNode`` — the recursive scope-tree node. It refers to
  ``MacroControlPayload`` only via a ``TYPE_CHECKING`` forward reference,
  so this module can be imported without pulling in the concrete payloads.
"""

from __future__ import annotations

import re
from collections.abc import Callable, Mapping
from dataclasses import dataclass
from typing import TYPE_CHECKING, Literal, Protocol

from vg2c.frontend.models import ClassifiedBlock

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
_INT_LITERAL_RE = re.compile(r"[+-]?\d+")


def _operand_macro(operand: str, numeric: bool) -> str | None:
    """Return the macro name a condition operand reads, or None for a literal."""
    # Local import to avoid circular dependency at module load time.
    from vg2c.runtime.values import normalize_macro_name
    from vg2c.utilities.macro_control import MacroControl

    value = operand.strip()
    if value.startswith("VAR(") and value.endswith(")"):
        return normalize_macro_name(value[4:-1])
    if MacroControl.NAMED_PLACEHOLDER_RE.fullmatch(value) or (
        numeric and _BARE_IDENT_RE.match(value)
    ):
        return normalize_macro_name(value)
    return None


def _operand_expr(
    operand: str, numeric: bool, inline: Mapping[str, str] | None = None
) -> str:
    """Render a single condition operand as a Python expression string."""
    from vg2c.utilities.macro_control import MacroControl

    value = operand.strip()
    if not value:
        return "0" if numeric else repr("")

    name = _operand_macro(value, numeric)
    if name is None:
        if not numeric:
            return repr(value)
        if _INT_LITERAL_RE.fullmatch(value):
            return str(int(value))
        return f"int({value!r})"
    if numeric and inline and name in inline:
        return inline[name]
    base = MacroControl.named.render(name)
    return f"int({base})" if numeric else base


# ---------------------------------------------------------------------------
# Option-parsing helper
# ---------------------------------------------------------------------------


def _quoted_args(value: str) -> list[str]:
    """Extract double-quoted argument strings from a UTILITIES option value."""
    return re.findall(r'"([^"]*)"', value)


# ---------------------------------------------------------------------------
# Scope-building protocol types
# ---------------------------------------------------------------------------


class ScopeIdSource(Protocol):
    def new_scope_id(self) -> int: ...


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
        walk: Callable[[ScopeNode, str], None],
        *,
        macros: str,
        imports: set[str],
        runtime_imports: set[str],
    ) -> None:
        """Delegate a structural scope to its payload, or walk children transparently.

        Leaf nodes are intentionally excluded — the walker handles them via
        UtilitySpec dispatch, which is not a payload concern.
        """
        if self.kind == "leaf":
            return
        if self.control_payload is not None:
            from vg2c.operands.conditional import IfThen
            from vg2c.operands.loop import ForLoop, RunLoop, SiteLoop
            from vg2c.operands.macro import StartMacro

            if isinstance(
                self.control_payload,
                (StartMacro, IfThen, RunLoop, ForLoop, SiteLoop),
            ):
                self.control_payload.emit_scope(
                    writer,
                    walk,
                    self,
                    macros=macros,
                    imports=imports,
                    runtime_imports=runtime_imports,
                )
                return

        # Transparent nodes: program, if-branch, else-branch.
        for child in self.children:
            walk(child, macros)


def build_delimited_scope(
    payload: MacroControlPayload,
    blocks: list[ClassifiedBlock],
    start_i: int,
    state: ScopeIdSource,
    parse_children: ParseChildrenFn,
    *,
    kind: Literal["macro", "loop"],
    opening_token: str,
    closing_token: str,
    log_error: Callable[[str], None],
) -> tuple[ScopeNode, int]:
    """Build a scope consuming blocks until ``closing_token`` (bare, e.g. ``END-LOOP``)."""
    start_block = blocks[start_i]
    children, i, end_token = parse_children(
        blocks, start_i + 1, {closing_token}, state
    )
    closed = end_token == closing_token
    if closed:
        end_index, next_i = blocks[i].index, i + 1
    else:
        loc = f"{start_block.span.file or '<input>'}:{start_block.span.start_line}:1"
        log_error(
            f"[unclosed-{kind}] {loc} (block {start_block.index}): "
            f"Found {{{opening_token}}} without a matching {{{closing_token}}}; "
            "implicitly closed at EOF."
        )
        end_index, next_i = (blocks[-1].index if blocks else start_block.index), i
    node = ScopeNode(
        scope_id=state.new_scope_id(),
        kind=kind,
        start_index=start_block.index,
        end_index=end_index,
        children=tuple(children),
        block_index=None,
        control_payload=payload,
    )
    return node, next_i


def emit_children(
    writer: IndentWriter,
    walk: Callable[[ScopeNode, str], None],
    children: tuple[ScopeNode, ...],
    macros: str,
) -> None:
    """Emit a scope body and preserve the generated pass for empty branches."""
    before = len(writer.lines)
    for child in children:
        walk(child, macros)
    if len(writer.lines) == before:
        writer.write("pass")
