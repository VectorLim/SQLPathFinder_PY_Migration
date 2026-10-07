"""Compiler support policy over task identities supplied by ScriptHost."""

from __future__ import annotations

from scripthost_portable.script_api import _block, _query_task_options
from scripthost_portable.task_introspection import inspect_task
from vg2c.diagnostics import fail
from vg2c.frontend.models import ClassifiedBlock, ParsedBlock
from vg2c.kind import Kind

_SUPPORTED_TASKS = {
    "NQ_SQLITE_TASK": Kind.SQLITE_QUERY,
    "NQ_ORACLE_TASK": Kind.SQL_QUERY,
    "HTML-RUN": Kind.HTML_REPORT,
    "HTML-DEFER": Kind.HTML_REPORT,
    "HTML-LAYOUT": Kind.HTML_REPORT,
    "HTML-DELETE": Kind.HTML_REPORT,
    "{START-MACRO}": Kind.MACRO_CONTROL,
    "{END-MACRO}": Kind.MACRO_CONTROL,
    "{IF-THEN}": Kind.MACRO_CONTROL,
    "{END-IF}": Kind.MACRO_CONTROL,
    "{ROWS-IN-FILE}": Kind.ROWS_IN_FILE,
    "{AED}": Kind.AED,
}


def classify(blocks: list[ParsedBlock]) -> list[ClassifiedBlock]:
    classified = []
    for block in blocks:
        options = dict(block.options.lookup)
        code = "unsupported-block"
        if "REPORT" in options:
            code = "unsupported-report"
        elif "UTILITIES" in options:
            code = "unsupported-utility"
        elif "ENGINE" in options or "OLEDB" in options:
            code = "unsupported-engine"
            engine, oledb = options.get("ENGINE", ""), options.get("OLEDB", "")
            if ("ENGINE" in options and not engine) or ("OLEDB" in options and not oledb):
                fail(code, "Explicit ENGINE/OLEDB values cannot be empty.", block)
            # Preserve the existing shorthand through the facade's own defaults.
            engine = engine or {"SQLITE": "SQLite", "SQLPLUS": "VA"}.get(oledb.upper(), "")
            try:
                defaults = _query_task_options(
                    output=options.get("CSV"), engine=engine,
                    **({"oledb": oledb} if oledb else {}),
                )
            except (TypeError, ValueError) as error:
                fail(code, str(error), block)
            options = {**defaults, **options}
        try:
            task = inspect_task(_block(block.body, options), block.index)
        except Exception as error:
            fail(code, f"ScriptHost cannot inspect this block: {error}", block)
        kind = _SUPPORTED_TASKS.get(task.task_type)
        if kind is None:
            fail(code, f"Unsupported ScriptHost task: {task.task_type} ({task.class_name}).", block)
        classified.append(ClassifiedBlock(block, kind, task))
    return classified
