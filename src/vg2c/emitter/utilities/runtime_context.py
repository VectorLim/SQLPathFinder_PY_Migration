"""PipelineContext — runtime context for generated scripts (embeddable)."""

from __future__ import annotations

from typing import Any, ContextManager

from vg2c.emitter.utilities._registry import register_utility


@register_utility(
    "ctx",
    imports=(
        "from typing import Any, ContextManager",
    ),
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

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)
