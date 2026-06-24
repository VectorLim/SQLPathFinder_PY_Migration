"""PipelineContext — singleton that wires all runtime helpers together."""

from __future__ import annotations

from contextlib import contextmanager
from typing import Any, Iterator

from vg2c_runtime.csv_io import CsvIO
from vg2c_runtime.external import ExternalProcess
from vg2c_runtime.fs_ops import FileSystemOps
from vg2c_runtime.macro import MacroState
from vg2c_runtime.mail import MailService
from vg2c_runtime.readers import MockReader, OracleReader, Reader
from vg2c_runtime.sql_macros import SqlMacros
from vg2c_runtime.sqlite_engine import SqliteEngine
from vg2c_runtime.write_file import write_file as _write_file


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

    @contextmanager
    def macro_scope(self, row: dict[str, str] | None = None) -> Iterator[None]:
        """Push a macro frame (optionally pre-populated with *row* values), yield, pop."""
        self._macro.push_frame(named=row)
        try:
            yield
        finally:
            self._macro.pop_frame()

    # ------------------------------------------------------------------
    # Reader factories
    # ------------------------------------------------------------------

    def reader_mars(
        self,
        database: str,
        node: str = "",
        record: tuple[str, str] | None = None,
        instance: str | None = None,
    ) -> Reader:
        return OracleReader(
            database=database,
            node=node,
            record=record,
            instance=instance,
            macro_state=self._macro,
        )

    def reader_oasys(
        self,
        database: str,
        node: str = "",
        record: tuple[str, str] | None = None,
        instance: str | None = None,
    ) -> Reader:
        return OracleReader(
            database=database,
            node=node,
            record=record,
            instance=instance,
            macro_state=self._macro,
        )

    def reader_aries(
        self,
        database: str,
        node: str = "",
        record: tuple[str, str] | None = None,
        instance: str | None = None,
    ) -> Reader:
        return OracleReader(
            database=database,
            node=node,
            record=record,
            instance=instance,
            macro_state=self._macro,
        )

    # ------------------------------------------------------------------
    # File writing
    # ------------------------------------------------------------------

    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        _write_file(path, template, vars=vars, macro_state=self._macro)

    # ------------------------------------------------------------------
    # eval_condition (legacy — emitter no longer calls this, kept for
    # scripts generated before Stage 6)
    # ------------------------------------------------------------------

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        lhs_val = self._macro.named(lhs) if lhs.startswith("VAR(") else lhs
        rhs_val = self._macro.named(rhs) if rhs.startswith("VAR(") else rhs
        return lhs_val == rhs_val
