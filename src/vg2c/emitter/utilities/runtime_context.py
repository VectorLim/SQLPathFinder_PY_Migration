"""PipelineContext — runtime context for generated scripts (embeddable)."""

from __future__ import annotations

from typing import Any, ContextManager

from vg2c.emitter.utilities._registry import register_utility


@register_utility(
    "ctx",
    imports=("from typing import Any, ContextManager",),
)
class PipelineContext:
    """Single runtime context object for generated scripts."""

    def __init__(self) -> None:
        self.macro = MacroState()
        self.csv_io = CsvIO()
        self.sqlite_engine = SqliteEngine()
        self.sql_macros = SqlMacros()
        self.fs_ops = FileSystemOps()
        self.mail = MailService()
        self.external = ExternalProcess()

    def macro_scope(self, row: dict[str, str] | None = None) -> ContextManager[None]:
        """Delegate macro scoping to MacroState."""
        return self.macro.scope(row=row)

    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        self.macro.write_file(path, template, vars=vars)

    def read(self, sql: str, db_type: str):
        """Run SQL through reader runtime using current macro scope."""
        return read(sql=sql, db_type=db_type, macro_state=self.macro)

    def run_query(
        self,
        sql: str,
        output: str,
        source_type: str,
        inputs: list[str] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
    ):
        """
        Unified query execution method for both SQLite and external databases.

        Args:
            sql: SQL query string (may contain macro placeholders)
            output: Output CSV path
            source_type: 'sqlite' | 'MARS' | 'ARIES' | 'OASYS'
            inputs: List of input CSV paths (required for sqlite)
            header: Optional declared header for output CSV
            crosstab: Optional crosstab config dict with keys:
                     'row_keys', 'header_key', 'value_key'
        """
        # 1. Substitute macros
        sql = self.macro.substitute_sql(sql)

        # 2. Execute query based on source_type
        if source_type.lower() == 'sqlite':
            result = self.sqlite_engine.execute(sql, inputs or [])
        else:
            # Pass pre-substituted SQL; read() will skip re-substitution
            result = read(sql=sql, db_type=source_type, macro_state=None)

        # 3. Apply crosstab if configured
        if crosstab:
            result = apply_crosstab(
                result,
                row_keys=crosstab['row_keys'],
                header_key=crosstab['header_key'],
                value_key=crosstab['value_key'],
            )

        # 4. Write output
        self.csv_io.write(output, result, header=header)

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)
