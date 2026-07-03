"""PipelineContext - runtime context container."""

from __future__ import annotations

from typing import Any, ContextManager

from vg2c.emitter.readers import ReaderRuntime
from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import register_utility
from vg2c.emitter.utilities.crosstab import CrosstabUtility
from vg2c.emitter.utilities.csv_io import CsvIO
from vg2c.emitter.utilities.external import ExternalProcess
from vg2c.emitter.utilities.fs_ops import FileSystemOps
from vg2c.emitter.utilities.macro_state import MacroState
from vg2c.emitter.utilities.mail import MailService
from vg2c.emitter.utilities.sql_macros import SqlMacros
from vg2c.emitter.utilities.sqlite_engine import SqliteEngine


@register_utility
class PipelineContext(UtilitySpec):
    """Single runtime context object for generated scripts."""

    utility_name = "ctx"
    utility_imports = ("from typing import Any, ContextManager",)
    utility_dependencies = (
        "macro",
        "csv_io",
        "sqlite_engine",
        "sql_macros",
        "fs_ops",
        "mail",
        "external",
        "crosstab",
        "reader_runtime",
    )

    def __init__(self) -> None:
        self.macro = MacroState()
        self.csv_io = CsvIO()
        self.sqlite_engine = SqliteEngine()
        self.sql_macros = SqlMacros()
        self.fs_ops = FileSystemOps()
        self.mail = MailService()
        self.external = ExternalProcess()
        self.reader_runtime = ReaderRuntime()
        self.crosstab = CrosstabUtility()

    def macro_scope(self, row: dict[str, str] | None = None) -> ContextManager[None]:
        return self.macro.scope(row=row)

    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        self.macro.write_file(path, template, vars=vars)

    def read(self, sql: str, db_type: str):
        return self.reader_runtime.read(
            sql=sql, db_type=db_type, macro_state=self.macro
        )

    def run_query(
        self,
        sql,
        output: str,
        source_type: str,
        inputs: list[str] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
    ):
        sql = self.macro.substitute_sql(sql)

        if source_type.lower() == "sqlite":
            result = self.sqlite_engine.execute(sql, inputs or [])
        else:
            result = self.reader_runtime.read(
                sql=sql, db_type=source_type, macro_state=None
            )

        if crosstab:
            result = self.crosstab.apply(
                result,
                row_keys=crosstab["row_keys"],
                header_key=crosstab["header_key"],
                value_key=crosstab["value_key"],
            )

        self.csv_io.write(output, result, header=header)

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)
