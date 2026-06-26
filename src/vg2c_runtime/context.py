"""PipelineContext — singleton that wires all runtime helpers together."""

from __future__ import annotations

from typing import Any, ContextManager, Literal

from vg2c_runtime.csv_io import CsvIO
from vg2c_runtime.external import ExternalProcess
from vg2c_runtime.fs_ops import FileSystemOps
from vg2c.emitter.macro import MacroState
from vg2c.emitter.sql_macro import SqlMacros
from vg2c.emitter.sqlite_engine import SqliteEngine
from vg2c_runtime.mail import MailService


class PipelineContext:
    """Single runtime context object imported by generated scripts as ``ctx``."""

    def __init__(self) -> None:
        self.macro = MacroState()
        self.csv_io = CsvIO()
        self.sqlite_engine = SqliteEngine()
        self.sql_macros = SqlMacros()
        self.fs_ops = FileSystemOps()
        self.mail = MailService()
        self.external = ExternalProcess()

    # ------------------------------------------------------------------
    # Macro scope context manager
    # ------------------------------------------------------------------

    def macro_scope(self, row: dict[str, str] | None = None) -> ContextManager[None]:
        """Delegate macro scoping to MacroState."""
        return self.macro.scope(row=row)

    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        self.macro.write_file(path, template, vars=vars)

    # ------------------------------------------------------------------
    # eval_condition (legacy — emitter no longer calls this, kept for
    # scripts generated before Stage 6)
    # ------------------------------------------------------------------

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)
