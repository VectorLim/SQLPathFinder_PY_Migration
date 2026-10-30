"""RowsInFile - count rows in a CSV and store the result in a macro variable."""

from __future__ import annotations

import re
from collections.abc import Mapping
from dataclasses import replace

from vg2c.emitter.models import BlockEmission, CodeExpr
from vg2c.kind import Kind
from vg2c.operands import ScopeNode
from vg2c.operands.base import _operand_macro
from vg2c.operands.conditional import IfThen
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import split_utility_command, to_code_expr
from vg2c.runtime.values import normalize_macro_name, strip_quotes


class RowsInFile(EmitterUtility):
    """Count rows in a CSV file and store the count in a named macro variable.

    VG2 syntax::

        /UTILITIES={ROWS-IN-FILE} "<csv_path>" "<var_name>" "<prompt_off>"

    The third argument (``Y``/``N``) is the original VG2 prompt-suppression
    flag; the generated Python never prompts the user, so it is parsed but
    intentionally ignored during emission.
    """

    utility_name = "rows_in_file"
    handles = (Kind.ROWS_IN_FILE,)
    _TOKEN_RE = re.compile(r"^\s*\{ROWS-IN-FILE\}", re.IGNORECASE)

    @staticmethod
    def check(options) -> Kind | None:
        utilities = options.lookup.get("UTILITIES", "")
        if RowsInFile._TOKEN_RE.match(utilities):
            return Kind.ROWS_IN_FILE
        return None

    @staticmethod
    def _argv(block) -> list[str]:
        # argv[0] = '{ROWS-IN-FILE}', argv[1] = csv_path, argv[2] = var_name
        return split_utility_command(block.resolved_options.lookup.get("UTILITIES", ""))

    @classmethod
    def target(cls, block) -> str:
        argv = cls._argv(block)
        return strip_quotes(argv[2]).upper() if len(argv) > 2 else ""

    @classmethod
    def path(cls, block) -> str:
        argv = cls._argv(block)
        return strip_quotes(argv[1]) if len(argv) > 1 else ""

    @staticmethod
    def call(path: str) -> str:
        from vg2c.utilities.csv_io import CsvIO

        return str(CsvIO.row_count.render(to_code_expr(path)))

    @classmethod
    def assignment(cls, target: str, path: str, macros: str):
        from vg2c.utilities.macro_control import MacroControl

        return MacroControl.render_named_assignment(
            MacroControl.set_named.render(target, CodeExpr(f"str({cls.call(path)})")),
            macros,
        )

    @classmethod
    def emit_block(cls, block, *, macros: str = "macros") -> BlockEmission:
        return BlockEmission(
            (cls.assignment(cls.target(block), cls.path(block), macros),)
        )


def reads_macro(text: str, name: str) -> bool:
    """True when text reads ``name`` as a <<<placeholder>>> or as a condition operand."""
    from vg2c.utilities.macro_control import MacroControl

    return _operand_macro(text, True) == name or any(
        match.group(1) is not None and normalize_macro_name(match.group(1)) == name
        for match in MacroControl.PLACEHOLDER_RE.finditer(text)
    )


def _block_reads_macro(block, name: str) -> bool:
    payload = getattr(block, "control_payload", None)
    if isinstance(payload, IfThen) and name in payload.macro_reads():
        return True
    return reads_macro(block.raw, name)


def inline_row_count_conditions(
    tree: ScopeNode, blocks: Mapping[int, object]
) -> tuple[ScopeNode, frozenset[int]]:
    """Fold a ROWS-IN-FILE into the next sibling IF when no other block reads its macro.

    Returns the rewritten tree and the inlined ROWS-IN-FILE block indexes.
    Emission, semantic projection and editing must all use this so headers match.
    """
    pairs = []

    def collect(node: ScopeNode) -> None:
        for count, branch in zip(node.children, node.children[1:]):
            block = blocks.get(count.block_index) if count.kind == "leaf" else None
            if (
                block is not None
                and block.kind is Kind.ROWS_IN_FILE
                and branch.kind == "if"
                and isinstance(branch.control_payload, IfThen)
                and RowsInFile.target(block)
                in branch.control_payload.inlinable_macros()
            ):
                pairs.append((block, branch.start_index))
        for child in node.children:
            collect(child)

    collect(tree)
    owners: dict[str, set[int]] = {}
    for block, if_index in pairs:
        owners.setdefault(RowsInFile.target(block), set()).update(
            (block.index, if_index)
        )
    live = {
        target
        for target, owned in owners.items()
        if any(
            index not in owned and _block_reads_macro(other, target)
            for index, other in blocks.items()
        )
    }
    inlined = {
        if_index: block
        for block, if_index in pairs
        if RowsInFile.target(block) not in live
    }

    def rewrite(node: ScopeNode) -> ScopeNode:
        changes = {}
        children = tuple(rewrite(child) for child in node.children)
        if any(new is not old for new, old in zip(children, node.children)):
            changes["children"] = children
        if node.kind == "if" and node.start_index in inlined:
            block = inlined[node.start_index]
            changes["control_payload"] = replace(
                node.control_payload,
                row_count=(RowsInFile.target(block), RowsInFile.path(block)),
            )
        return replace(node, **changes) if changes else node

    return rewrite(tree), frozenset(block.index for block in inlined.values())
