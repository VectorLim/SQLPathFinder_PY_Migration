"""Classify the supported surface without importing any runtime utilities."""

from __future__ import annotations

from vg2c.diagnostics import fail
from vg2c.frontend.models import ClassifiedBlock, ParsedBlock
from vg2c.kind import Kind


def classify(blocks: list[ParsedBlock]) -> list[ClassifiedBlock]:
    classified = []
    for block in blocks:
        options = block.options.lookup
        if "UTILITIES" in options:
            command = (
                options["UTILITIES"].split(maxsplit=1)[0] if options["UTILITIES"].strip() else ""
            )
            kinds = {
                "{START-MACRO}": Kind.MACRO_CONTROL,
                "{END-MACRO}": Kind.MACRO_CONTROL,
                "{IF-THEN}": Kind.MACRO_CONTROL,
                "{END-IF}": Kind.MACRO_CONTROL,
                "{ROWS-IN-FILE}": Kind.ROWS_IN_FILE,
                "{AED}": Kind.AED,
            }
            kind = kinds.get(command.upper())
            if kind is None:
                fail("unsupported-utility", f"Unsupported utility: {command!r}", block)
            reason = command.upper()
        elif "REPORT" in options:
            reason = options["REPORT"].upper()
            if reason not in {"HTML-RUN", "HTML-DEFER", "HTML-LAYOUT", "HTML-DELETE"}:
                fail("unsupported-report", f"Unsupported report: {reason!r}", block)
            kind = Kind.HTML_REPORT
        elif "ENGINE" in options or "OLEDB" in options:
            engine = options.get("ENGINE", "").upper()
            oledb = options.get("OLEDB", "").upper()
            if ("ENGINE" in options and not engine) or ("OLEDB" in options and not oledb):
                fail("unsupported-engine", "Explicit ENGINE/OLEDB values cannot be empty.", block)
            if not engine:
                engine = {"SQLITE": "SQLITE", "SQLPLUS": "VA"}.get(oledb, "")
            expected = {"SQLITE": "SQLITE", "VA": "SQLPLUS"}.get(engine)
            if expected is None or (oledb and oledb != expected):
                fail("unsupported-engine", f"Unsupported ENGINE/OLEDB: {engine!r}/{oledb!r}", block)
            kind = Kind.SQLITE_QUERY if engine == "SQLITE" else Kind.SQL_QUERY
            reason = engine
        else:
            fail("unsupported-block", "Block is not a supported query, report or utility.", block)
        classified.append(ClassifiedBlock(block, kind, reason))
    return classified
