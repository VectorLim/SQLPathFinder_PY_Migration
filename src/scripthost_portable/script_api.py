"""Small Python facade over the original ScriptHost tasks.

The launcher owns initialization; scripts only import these five objects and
define run(). CSV macros and environment variables keep their original distinct
namespaces: set() writes environment values, referenced as %NAME% in lookups.
"""

from __future__ import annotations

import csv
import io
import os
from collections import OrderedDict
from contextlib import contextmanager

from .api_contract import QUERY_OPTIONS, REPORT_OPTIONS

__all__ = ["aed", "macros", "query", "reports", "utilities"]

# ponytail: process-global binding; use the existing fresh-child worker per job.
_current_manager = None


def _check_unbound():
    if _current_manager is not None:
        raise RuntimeError("A Python ScriptHost job is already running in this process.")


def _manager():
    if _current_manager is None:
        raise RuntimeError("Script API requires a job invoked through the ScriptHost runtime.")
    return _current_manager


@contextmanager
def _bind(manager):
    global _current_manager
    _check_unbound()
    manager._script_macro_tables = OrderedDict()
    _current_manager = manager
    try:
        yield
    finally:
        _current_manager = None
        for table in manager._script_macro_tables.values():
            table.dropTable()
        manager._script_macro_tables.clear()


def _block(body: str, options: dict) -> str:
    lines = ["<OPTIONS>"]
    for name, value in options.items():
        if value is None:
            continue
        value = ("Y" if value else "N") if isinstance(value, bool) else str(value)
        if any(token in value for token in ("\n", "\r", "<OPTIONS>", "</OPTIONS>")):
            raise ValueError(f"Invalid multiline or block token in {name}.")
        lines.append(f"/{name}={value}")
    if any(token in body for token in ("<OPTIONS>", "</OPTIONS>", "<---- New Query ---->")):
        raise ValueError("Supply only the SQL or report template, without legacy task blocks.")
    return "\n".join([*lines, "</OPTIONS>", body])


def _execute(body: str, options: dict, *, substitute_macros=True):
    manager = _manager()
    text = manager.Substitute_Global_Var(_block(body, options))
    task = manager.GetQuery(manager.gMyLocal, text, 0, False)
    task.RNStr = manager.gRNStr
    if substitute_macros and manager._script_macro_tables:
        task.parentMacTables = manager._script_macro_tables
        task.substituteMacro(1, 0)
    task.execute()
    return task


def _utility(name: str, *arguments) -> str:
    stream = io.StringIO()
    csv.writer(stream, delimiter=" ", quoting=csv.QUOTE_ALL, lineterminator="").writerow(
        [str(argument) for argument in arguments]
    )
    return "{" + name + "} " + stream.getvalue()


class _Macros:
    def load_csv(self, path, continue_on_error=False) -> bool:
        manager = _manager()
        if manager._script_macro_tables:
            raise NotImplementedError("Stage 1 supports one CSV macro scope per job.")
        task = _execute(
            "", {"UTILITIES": _utility("START-MACRO", path, "Y" if continue_on_error else "N")},
            substitute_macros=False,
        )
        manager._script_macro_tables = task.parentMacTables
        return bool(task.shouldExecuteChildTasks)

    def __getitem__(self, name: str) -> str:
        return self.get(name)

    def get(self, name: str) -> str:
        return self.substitute(f"<<<{name}>>>")

    def set(self, name: str, value) -> None:
        """Set an original environment variable; read it with macros['%NAME%']."""
        _manager().setEnv(name, value)

    def substitute(self, text: str) -> str:
        manager = _manager()
        text = manager.Substitute_Global_Var(text)
        return manager.Substitute_Macro(text, manager._script_macro_tables, 1, 0)

    def compare(self, lhs: str, operator: str, rhs: str) -> bool:
        # Let the original IF task resolve ENV()/VAR() and CompareVars arguments.
        task = _execute("", {"UTILITIES": _utility("IF-THEN", lhs, operator, rhs)})
        return bool(task.shouldExecuteChildTasks)


class _Utilities:
    def rows_in_file(self, path, variable: str) -> int:
        # RowsInFileTask also preserves the original count-error -> -1 behavior.
        _execute("", {"UTILITIES": _utility("ROWS-IN-FILE", path, variable, "N")})
        return int(os.environ[variable])


_QUERY_OPTIONS = {
    public: legacy for legacy, public in QUERY_OPTIONS.items()
    if public not in {"node", "engine", "output", "tables"}
}


def _options(values: dict, allowed: dict) -> dict:
    unknown = values.keys() - allowed.keys()
    if unknown:
        raise TypeError("Unsupported Stage 1 options: " + ", ".join(sorted(unknown)))
    return {allowed[name]: value for name, value in values.items()}


def _query_task_options(*, output, engine="SQLite", node=None, tables=None, **options):
    """Prepare the same original task options for execution and inspection."""
    if engine.upper() not in {"SQLITE", "VA"}:
        raise ValueError(f"Unsupported Stage 1 query engine: {engine}")
    values = {
        "NODE": node if node is not None else ".\\", "UN": "", "PW": "",
        "OLEDB": "SQLite" if engine.upper() == "SQLITE" else "SQLPlus",
        "ENGINE": engine, "WORKDIR": ".\\", "CSV": output, "TABLE": tables,
    }
    values.update(_options(options, _QUERY_OPTIONS))
    expected_oledb = "SQLITE" if engine.upper() == "SQLITE" else "SQLPLUS"
    if values["OLEDB"].upper() != expected_oledb:
        raise ValueError(f"Unsupported Stage 1 oledb for {engine}: {values['OLEDB']}")
    return values


class _Query:
    def run(self, *, sql: str, output, engine="SQLite", node=None, tables=None, **options):
        _execute(sql, _query_task_options(
            output=output, engine=engine, node=node, tables=tables, **options,
        ))


_REPORT_OPTIONS = {public: legacy for legacy, public in REPORT_OPTIONS.items()}


class _Reports:
    def run(self, template: str, **options):
        _execute(template, {"REPORT": "HTML-RUN", **_options(options, _REPORT_OPTIONS)})

    def defer(self, template: str, *, report_id: str, **options):
        _execute(template, {
            "REPORT": "HTML-DEFER", "ID": report_id, **_options(options, _REPORT_OPTIONS),
        })

    def layout(self, template: str, **options):
        _execute(template, {"REPORT": "HTML-LAYOUT", **_options(options, _REPORT_OPTIONS)})

    def delete(self, *, instance=None):
        _execute("N/A", {"REPORT": "HTML-DELETE", "INSTANCE": instance})


class _Aed:
    def process(self, path):
        _manager()
        from .aed_api import process_candidates

        return process_candidates(macros.substitute(str(path)))


macros = _Macros()
utilities = _Utilities()
query = _Query()
reports = _Reports()
aed = _Aed()
