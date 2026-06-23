from __future__ import annotations

from dataclasses import dataclass
from enum import StrEnum
from typing import Literal

from vg2c.model import ParsedBlock, SourceSpan


class Kind(StrEnum):
    """Classification of a VG2 block's purpose."""

    SQL_FETCH = "sql_fetch"
    SQLITE_JOIN = "sqlite_join"
    WRITE_FILE = "write_file"
    RUN_UTILITY = "run_utility"
    RUN_PYTHON = "run_python"
    COPY = "copy"
    RENAME = "rename"
    DELETE_FILE = "delete_file"
    EMAIL = "email"
    ROWS_IN_FILE = "rows_in_file"
    MACRO_OPEN = "macro_open"
    MACRO_CLOSE = "macro_close"
    IF_OPEN = "if_open"
    IF_ELSE = "if_else"
    IF_CLOSE = "if_close"
    LOOP_OPEN = "loop_open"
    LOOP_CLOSE = "loop_close"
    BLOCK_GROUP_OPEN = "block_group_open"
    BLOCK_GROUP_CLOSE = "block_group_close"
    HTML_REPORT = "html_report"
    UNKNOWN = "unknown"


class Role(StrEnum):
    """Structural role of a block in the script."""

    LEAF = "leaf"
    OPENER = "opener"
    CLOSER = "closer"


class EngineKind(StrEnum):
    """Database or execution engine type."""

    ORACLE_MARS = "oracle_mars"
    ORACLE_OASYS = "oracle_oasys"
    ORACLE_GENERIC = "oracle_generic"
    ARIES = "aries"
    SQLITE = "sqlite"
    NONE = "none"


@dataclass(frozen=True)
class EngineTarget:
    """Resolved execution engine for a block."""

    kind: EngineKind
    node: str | None
    schema_hint: str | None
    reason: str


@dataclass(frozen=True)
class RecordRef:
    """SPF logical view reference with version."""

    name: str
    version: str


# Spec family
@dataclass(frozen=True)
class SqlFetchSpec:
    """SQL query execution against an Oracle or Aries database."""

    engine: EngineTarget
    record: RecordRef | None
    csv_out: str
    headers: list[str]
    sql_body: str
    instance: int | None
    prompt: str | None


@dataclass(frozen=True)
class SqliteJoinSpec:
    """SQLite local join or transformation."""

    csv_out: str
    tables: list[str]
    headers: list[str]
    delete_patterns: list[str]
    sqlite_dt: str | None
    reset: bool
    create_temp_table: bool
    body: str
    instance: int | None
    prompt: str | None


@dataclass(frozen=True)
class WriteFileSpec:
    """Write text or data to a file."""

    csv_out: str
    immediate: bool
    body: str
    instance: int | None
    prompt: str | None


@dataclass(frozen=True)
class RunUtilitySpec:
    """Execute a generic external utility."""

    executable: str
    args: list[str]
    workdir: str | None
    outlook: bool
    prompt: str | None


@dataclass(frozen=True)
class RunPythonSpec:
    """Execute a Python script."""

    script_path: str
    extra_args: list[str]
    workdir: str | None
    hadoop_server: str | None
    python_version: str | None
    prompt: str | None


@dataclass(frozen=True)
class CopySpec:
    """Copy a file from src to dst."""

    src: str
    dst: str
    continue_on_fail: bool
    prompt: str | None


@dataclass(frozen=True)
class RenameSpec:
    """Rename a file."""

    src: str
    dst: str
    prompt: str | None


@dataclass(frozen=True)
class DeleteSpec:
    """Delete a file or pattern."""

    target: str
    force: bool
    prompt: str | None


@dataclass(frozen=True)
class EmailSpec:
    """Send email with attachments."""

    attachments: list[str]
    recipients_token: str
    subject: str
    body_file: str
    recipients_list: list[str]
    prompt: str | None


@dataclass(frozen=True)
class RowsInFileSpec:
    """Count rows in a file and set environment variable."""

    file_path: str
    var_name: str
    prompt: str | None


@dataclass(frozen=True)
class MacroOpenSpec:
    """Start macro block."""

    csv_driver: str
    nested: bool
    prompt: str | None


@dataclass(frozen=True)
class MacroCloseSpec:
    """End macro block."""

    prompt: str | None


@dataclass(frozen=True)
class IfThenSpec:
    """Conditional block opener."""

    lhs: str
    op: str
    rhs: str
    connector: str
    lhs2: str
    op2: str
    rhs2: str
    prompt: str | None


@dataclass(frozen=True)
class IfElseSpec:
    """Conditional else branch."""

    prompt: str | None


@dataclass(frozen=True)
class IfCloseSpec:
    """Conditional block closer."""

    prompt: str | None


@dataclass(frozen=True)
class LoopOpenSpec:
    """Loop block opener."""

    loop_kind: Literal["for", "site", "run"]
    csv_file: str
    column: str
    prompt: str | None


@dataclass(frozen=True)
class LoopCloseSpec:
    """Loop block closer."""

    prompt: str | None


@dataclass(frozen=True)
class BlockGroupOpenSpec:
    """Begin grouped block."""

    prompt: str | None


@dataclass(frozen=True)
class BlockGroupCloseSpec:
    """End grouped block."""

    prompt: str | None


@dataclass(frozen=True)
class HtmlReportSpec:
    """HTML report generation."""

    phase: Literal["RUN", "LAYOUT", "DELETE"]
    raw_payload: str
    instance: int | None
    prompt: str | None


@dataclass(frozen=True)
class UnknownSpec:
    """Unclassified block."""

    reason: str
    options_seen: dict[str, str]


Spec = (
    SqlFetchSpec
    | SqliteJoinSpec
    | WriteFileSpec
    | RunUtilitySpec
    | RunPythonSpec
    | CopySpec
    | RenameSpec
    | DeleteSpec
    | EmailSpec
    | RowsInFileSpec
    | MacroOpenSpec
    | MacroCloseSpec
    | IfThenSpec
    | IfElseSpec
    | IfCloseSpec
    | LoopOpenSpec
    | LoopCloseSpec
    | BlockGroupOpenSpec
    | BlockGroupCloseSpec
    | HtmlReportSpec
    | UnknownSpec
)


@dataclass(frozen=True)
class ClassifiedBlock:
    """A parsed block with classification metadata."""

    parsed: ParsedBlock
    kind: Kind
    role: Role
    spec: Spec
    reason: str


@dataclass(frozen=True)
class Diagnostic:
    """A warning or error message about classification."""

    block_index: int
    span: SourceSpan
    severity: Literal["info", "warn", "error"]
    message: str


@dataclass(frozen=True)
class ClassificationReport:
    """Complete classification results for a script."""

    blocks: list[ClassifiedBlock]
    diagnostics: list[Diagnostic]
