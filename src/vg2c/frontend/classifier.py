from __future__ import annotations

from typing import Callable

from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    Diagnostic,
    ParsedBlock,
)
from vg2c.kind import Kind

RuleFn = Callable[[BlockOptions, str], tuple[Kind, str] | None]


def classify(
    blocks: list[ParsedBlock],
) -> tuple[list[ClassifiedBlock], list[Diagnostic]]:
    diagnostics: list[Diagnostic] = []
    classified: list[ClassifiedBlock] = []

    for block in blocks:
        result = _classify_one(block.options, block.body)
        if result is None:
            classified.append(
                ClassifiedBlock(
                    parsed=block, kind=Kind.UNKNOWN, reason="no rule matched"
                )
            )
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
    utilities = options.lookup.get("UTILITIES")
    if not utilities:
        return None

    first_token = utilities.strip().split(maxsplit=1)[0].strip().strip('"')
    basename = first_token.split("/")[-1].split("\\")[-1].lower()

    if "robocopy" in basename or "spfcopy" in basename:
        return Kind.FS_COPY, "/UTILITIES command maps to FS copy"
    if "spfdelete" in basename:
        return Kind.FS_DELETE, "/UTILITIES command maps to FS delete"
    if "run_python_script" in basename or basename.endswith((".bat", ".exe")):
        return Kind.EXTERNAL_RUN, "/UTILITIES command maps to external run"

    if "UTILITIES" in options.lookup:
        return Kind.UTILITY, "/UTILITIES present"
    return None


def _rule_sqlite(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    if options.lookup.get("OLEDB", "").upper() == "SQLITE":
        return Kind.SQLITE_QUERY, "/OLEDB=SQLite"
    if options.lookup.get("ENGINE", "").upper() == "SQLITE":
        return Kind.SQLITE_QUERY, "/ENGINE=SQLite"
    return None


def _rule_oracle(options: BlockOptions, body: str) -> tuple[Kind, str] | None:
    node = options.lookup.get("NODE", "")
    engine = options.lookup.get("ENGINE", "")
    oledb = options.lookup.get("OLEDB", "")
    if engine.upper() not in {"VA"} and oledb.upper() not in {"SQLPLUS"}:
        return None

    if any(_node_matches(node, token) for token in ("MARS", "OASYS", "ARIES")):
        return (
            Kind.SQL_QUERY,
            "/NODE indicates Oracle dialect and /ENGINE=VA or /OLEDB=SQLPlus",
        )
    return None


def _node_matches(node_value: str, token: str) -> bool:
    node = node_value.upper().strip()
    return (
        node.endswith(token) or node.endswith(f".{token}") or f"<<<{token}>>>" in node
    )


_RULES: tuple[RuleFn, ...] = (
    _rule_html_report,
    _rule_write_file,
    _rule_macro_control,
    _rule_utility,
    _rule_sqlite,
    _rule_oracle,
)
