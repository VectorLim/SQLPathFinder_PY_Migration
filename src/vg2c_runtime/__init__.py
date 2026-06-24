"""VG2C Runtime — Stub Signatures

This module provides the runtime interface that Stage 5-emitted scripts import and call.
Stage 6 provides the concrete implementations.

This stub version raises NotImplementedError for every function — the goal is to
establish the API contract and enable Stage 5 emit testing (via ast.parse).
"""

from __future__ import annotations

from contextlib import contextmanager
from pathlib import Path
from typing import Any, Iterator

__all__ = ["ctx"]


class PipelineContext:
    """Singleton context carrying state and helper interfaces throughout a script."""

    def __init__(self):
        self._macro_state = MacroState()
        self.csv_io = CsvIO()
        self.sqlite_engine = SqliteEngine()
        self.sql_macros = SqlMacros()
        self.fs_ops = FileSystemOps()
        self.mail = MailService()
        self.external = ExternalProcess()

    @property
    def macro(self) -> MacroState:
        """Access macro variable state."""
        return self._macro_state

    def reader_mars(
        self,
        database: str,
        node: str,
        record: tuple[str, str] | None,
        instance: str | None,
    ) -> Reader:
        """Create an Oracle MARS reader."""
        raise NotImplementedError("Stage 6: reader_mars")

    def reader_oasys(
        self,
        database: str,
        node: str,
        record: tuple[str, str] | None,
        instance: str | None,
    ) -> Reader:
        """Create an Oracle OASYS reader."""
        raise NotImplementedError("Stage 6: reader_oasys")

    def reader_aries(
        self,
        database: str,
        node: str,
        record: tuple[str, str] | None,
        instance: str | None,
    ) -> Reader:
        """Create an Oracle ARIES reader."""
        raise NotImplementedError("Stage 6: reader_aries")

    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        """Write a file using a template with runtime variable substitution."""
        raise NotImplementedError("Stage 6: write_file")

    @contextmanager
    def macro_scope(self, csv_path: str | None = None, row_iter: bool = False):
        """Context manager for macro scope (static or row-iterated)."""
        raise NotImplementedError("Stage 6: macro_scope")

    def eval_condition(self, lhs: str, op: str, rhs: str, *args) -> bool:
        """Evaluate an IF-THEN condition using macro variables."""
        raise NotImplementedError("Stage 6: eval_condition")


class MacroState:
    """Macro variable state during execution."""

    def __init__(self):
        self._named: dict[str, str] = {}
        self._positional: list[str] = []
        self._cursor: int = 0

    def named(self, name: str) -> str:
        """Get a named macro variable by name (uppercased)."""
        raise NotImplementedError("Stage 6: macro.named")

    def positional(self) -> str:
        """Get the next positional macro variable."""
        raise NotImplementedError("Stage 6: macro.positional")


class Reader:
    """Abstract database reader."""

    def read(self, sql: str) -> Any:
        """Execute SQL and return result (as CSV, DataFrame, etc.)."""
        raise NotImplementedError("Stage 6: Reader.read")


class CsvIO:
    """CSV file I/O operations."""

    def iter(self, name: str) -> Iterator[dict[str, Any]]:
        """Iterate over a CSV file by row."""
        raise NotImplementedError("Stage 6: csv_io.iter")

    def read(self, name: str) -> Path:
        """Read a CSV file into memory."""
        raise NotImplementedError("Stage 6: csv_io.read")

    def write(self, name: str, content: Any) -> None:
        """Write content to a CSV file."""
        raise NotImplementedError("Stage 6: csv_io.write")

    def row_count(self, name: str) -> int:
        """Get the row count of a CSV file."""
        raise NotImplementedError("Stage 6: csv_io.row_count")


class SqliteEngine:
    """SQLite execution helper."""

    def run_join(self, sql: str, inputs: list[str], output: str) -> None:
        """Execute a SQL query with multiple input CSVs and write output."""
        raise NotImplementedError("Stage 6: sqlite_engine.run_join")


class SqlMacros:
    """SQL macro expansion helpers."""

    def sql_get_csv_list(self, path: str, column_ref: int | str, lead_in: str) -> str:
        """Expand SQL_Get_CSV_List into an IN(...) clause or equivalent."""
        raise NotImplementedError("Stage 6: sql_macros.sql_get_csv_list")


class FileSystemOps:
    """File system operations (copy, delete, etc.)."""

    def copy(self, src: str | Path, dst: str | Path) -> None:
        """Copy a file or directory."""
        raise NotImplementedError("Stage 6: fs_ops.copy")

    def rename(self, src: str | Path, dst: str | Path) -> None:
        """Rename / move a file or directory."""
        raise NotImplementedError("Stage 6: fs_ops.rename")

    def delete(self, paths: list[str | Path]) -> None:
        """Delete files or directories."""
        raise NotImplementedError("Stage 6: fs_ops.delete")


class MailService:
    """Email sending helper."""

    def send(
        self,
        to: str,
        subject: str,
        body: str,
        attachments: list[str] | None = None,
    ) -> None:
        """Send an email."""
        raise NotImplementedError("Stage 6: mail.send")


class ExternalProcess:
    """External command execution."""

    def run(
        self, argv: list[str], cwd: str | Path | None = None, env: dict | None = None
    ) -> int:
        """Run an external command and return exit code."""
        raise NotImplementedError("Stage 6: external.run")


# Singleton instance
ctx = PipelineContext()
