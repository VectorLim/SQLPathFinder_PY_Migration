from __future__ import annotations

from enum import Enum


class Kind(str, Enum):  # noqa: UP042 - preserve existing Enum string semantics
    SQL_QUERY = "SQL_QUERY"
    SQLITE_QUERY = "SQLITE_QUERY"
    DUCKDB_QUERY = "DUCKDB_QUERY"
    WRITE_FILE = "WRITE_FILE"
    PYTHON_EMBED = "PYTHON_EMBED"
    FS_COPY = "FS_COPY"
    FS_DELETE = "FS_DELETE"
    EXTERNAL_RUN = "EXTERNAL_RUN"
    WAIT_FILE = "WAIT_FILE"
    HTML_REPORT = "HTML_REPORT"
    EMAIL = "EMAIL"
    MACRO_CONTROL = "MACRO_CONTROL"
    ROWS_IN_FILE = "ROWS_IN_FILE"
    SMART_APPEND = "SMART_APPEND"
    UNKNOWN = "UNKNOWN"

