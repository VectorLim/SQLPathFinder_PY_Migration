from __future__ import annotations

from typing import Callable

from vg2c.frontend.models import BlockOptions, ClassifiedBlock, Diagnostic, Kind, ParsedBlock

RuleFn = Callable[[BlockOptions, str], tuple[Kind, str] | None]


def classify(blocks: list[ParsedBlock]) -> tuple[list[ClassifiedBlock], list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []
    classified: list[ClassifiedBlock] = []
    aries_noted = False

    for block in blocks:
        result = _classify_one(block.options, block.body)
        if result is None:
            classified.append(ClassifiedBlock(parsed=block, kind=Kind.UNKNOWN, reason="no rule matched"))
            diagnostics.append(
                Diagnostic(
                    severity="warning",
                    code="unknown-kind",
                    message="Block did not match any known Stage 1 classification rule.",
                    block_index=block.index,
                    span=block.span,
                )
            )
            continue

        kind, reason = result
        classified.append(ClassifiedBlock(parsed=block, kind=kind, reason=reason))
        if kind is Kind.ARIES_READ and not aries_noted:
            diagnostics.append(
                Diagnostic(
                    severity="info",
                    code="aries-rule-untested",
                    message="ARIES classification rule fired; this path has no fixture coverage yet.",
                    block_index=block.index,
                    span=block.span,
                )
            )
            aries_noted = True

    return classified, diagnostics


def _classify_one(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    for rule in _RULES:
        outcome = rule(options, body)
        if outcome is not None:
            return outcome
    return None


def _rule_html_report(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    report = options.lookup.get("REPORT")
    if report and report.upper().startswith("HTML-"):
        return Kind.HTML_REPORT, "/REPORT starts with HTML-"
    return None


def _rule_write_file(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    if options.lookup.get("WRITE-FILE", "").upper() == "Y":
        return Kind.WRITE_FILE, "/WRITE-FILE=Y"
    return None


def _rule_macro_control(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    utilities = options.lookup.get("UTILITIES")
    if utilities and utilities.lstrip().startswith("{"):
        return Kind.MACRO_CONTROL, "/UTILITIES starts with {"
    return None


def _rule_utility(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    if "UTILITIES" in options.lookup:
        return Kind.UTILITY, "/UTILITIES present"
    return None


def _rule_sqlite(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    if options.lookup.get("OLEDB", "").upper() == "SQLITE":
        return Kind.SQLITE_QUERY, "/OLEDB=SQLite"
    if options.lookup.get("ENGINE", "").upper() == "SQLITE":
        return Kind.SQLITE_QUERY, "/ENGINE=SQLite"
    return None


def _rule_mars(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    node = options.lookup.get("NODE", "")
    engine = options.lookup.get("ENGINE", "")
    if _node_matches(node, "MARS") and engine.upper() == "VA":
        return Kind.MARS_READ, "/NODE indicates MARS and /ENGINE=VA"
    return None


def _rule_oasys(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    node = options.lookup.get("NODE", "")
    engine = options.lookup.get("ENGINE", "")
    if _node_matches(node, "OASYS") and engine.upper() == "VA":
        return Kind.OASYS_READ, "/NODE indicates OASYS and /ENGINE=VA"
    return None


def _rule_aries(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    node = options.lookup.get("NODE", "")
    engine = options.lookup.get("ENGINE", "")
    if _node_matches(node, "ARIES") and engine.upper() == "VA":
        return Kind.ARIES_READ, "/NODE indicates ARIES and /ENGINE=VA"
    return None


def _node_matches(node_value: str, token: str) -> bool:
    node = node_value.upper().strip()
    return (
        node.endswith(token)
        or node.endswith(f".{token}")
        or f"<<<{token}>>>" in node
    )


_RULES: tuple[RuleFn, ...] = (
    _rule_html_report,
    _rule_write_file,
    _rule_macro_control,
    _rule_utility,
    _rule_sqlite,
    _rule_mars,
    _rule_oasys,
    _rule_aries,
)
