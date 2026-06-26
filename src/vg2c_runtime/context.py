"""PipelineContext — singleton that wires all runtime helpers together."""

from __future__ import annotations

from typing import Any, ContextManager, Literal

from vg2c_runtime.csv_io import CsvIO
from vg2c_runtime.external import ExternalProcess
from vg2c_runtime.fs_ops import FileSystemOps
from vg2c.emitter.macro import MacroState
from vg2c_runtime.mail import MailService
from vg2c_runtime.sql_macros import SqlMacros
from vg2c_runtime.sqlite_engine import SqliteEngine
from datasyncx.core.base_component import Reader
from datasyncx.readers import MarsReader, AriesReader, OracleReader

DB_TYPE_LIST = Literal["MARS", "OASYS", "ARIES"]

DATABASE_TYPE_MAP: dict[str, type[Reader]] = {
    "MARS": MarsReader,
    "OASYS": OracleReader,
    "ARIES": AriesReader,
}


class PipelineContext:
    """Single runtime context object imported by generated scripts as ``ctx``."""

    def __init__(self) -> None:
        self._macro = MacroState()
        self.csv_io = CsvIO()
        self.sqlite_engine = SqliteEngine()
        self.sql_macros = SqlMacros()
        self.fs_ops = FileSystemOps()
        self.mail = MailService()
        self.external = ExternalProcess()

    # ------------------------------------------------------------------
    # Macro state
    # ------------------------------------------------------------------

    @property
    def macro(self) -> MacroState:
        return self._macro

    # ------------------------------------------------------------------
    # Macro scope context manager
    # ------------------------------------------------------------------

    def macro_scope(self, row: dict[str, str] | None = None) -> ContextManager[None]:
        """Delegate macro scoping to MacroState."""
        return self._macro.scope(row=row)

    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        self._macro.write_file(path, template, vars=vars)

    # ------------------------------------------------------------------
    # eval_condition (legacy — emitter no longer calls this, kept for
    # scripts generated before Stage 6)
    # ------------------------------------------------------------------

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self._macro.eval_condition(lhs, op, rhs)
