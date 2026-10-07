"""Main's opener/closer traversal, narrowed to one macro scope and IF-THEN."""

from __future__ import annotations

import re

from vg2c.diagnostics import fail
from vg2c.frontend.models import ClassifiedBlock
from vg2c.kind import Kind
from vg2c.operands import IfThen, ScopeNode, StartMacro, utility_arguments

_PLACEHOLDER = re.compile(r"<<<(.*?)>>>")


def build_scope_tree(blocks: list[ClassifiedBlock]) -> ScopeNode:
    next_scope_id = 1
    macro_seen = False

    def parse_children(start: int, closer: str | None, in_macro: bool):
        nonlocal next_scope_id, macro_seen
        children = []
        i = start
        while i < len(blocks):
            block = blocks[i]
            text = block.body + "\n" + "\n".join(block.options.lookup.values())
            if any(not m.group(1) for m in _PLACEHOLDER.finditer(text)):
                fail("positional-macro", "Unnamed positional macros are unsupported.", block)
            token = block.reason if block.kind is Kind.MACRO_CONTROL else None
            if token in {"{END-IF}", "{END-MACRO}"}:
                if utility_arguments(block):
                    fail("control-arguments", f"{token} takes no arguments.", block)
                if token != closer:
                    fail(
                        "control-structure",
                        f"Unexpected {token}; expected {closer or 'an opener'}.",
                        block,
                    )
                return tuple(children), i
            if macro_seen and not in_macro:
                if any(not m.group(1).startswith("%") for m in _PLACEHOLDER.finditer(text)):
                    fail(
                        "macro-lifetime",
                        "Macro references outside START-MACRO are unsupported.",
                        block,
                    )
            if token in {"{START-MACRO}", "{IF-THEN}"}:
                if token == "{START-MACRO}":
                    if in_macro or macro_seen:
                        fail(
                            "macro-scope",
                            "Nested or multiple START-MACRO scopes are unsupported.",
                            block,
                        )
                    macro_seen = True
                    payload = StartMacro.from_block(block)
                    kind, end_token = "macro", "{END-MACRO}"
                else:
                    payload = IfThen.from_block(block)
                    kind, end_token = "if", "{END-IF}"
                scope_id = next_scope_id
                next_scope_id += 1
                nested, end = parse_children(i + 1, end_token, in_macro or kind == "macro")
                if not nested:
                    fail("empty-scope", "An empty control scope has no supported operation.", block)
                children.append(
                    ScopeNode(scope_id, kind, block.index, blocks[end].index, nested, None, payload)
                )
                i = end + 1
            else:
                children.append(
                    ScopeNode(
                        next_scope_id, "leaf", block.index, block.index, (), block.index, None
                    )
                )
                next_scope_id += 1
                i += 1
        if closer is not None:
            fail("unclosed-control", f"Missing {closer}.", blocks[start - 1])
        return tuple(children), i

    children, _ = parse_children(0, None, False)
    return ScopeNode(0, "program", 0, blocks[-1].index if blocks else -1, children, None, None)
