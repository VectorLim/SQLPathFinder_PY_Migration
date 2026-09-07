from __future__ import annotations

from enum import Enum


class Kind(str, Enum):  # noqa: UP042 - preserve existing Enum string semantics
    SQL_QUERY = "SQL_QUERY"
    SQLITE_QUERY = "SQLITE_QUERY"
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
    UNKNOWN = "UNKNOWN"

    @property
    def is_csv_producer(self) -> bool:
        """Return True if this kind is an explicit CSV producer."""
        return self in {
            Kind.SQL_QUERY,
            Kind.SQLITE_QUERY,
            Kind.WRITE_FILE,
            Kind.PYTHON_EMBED,
        }

    @property
    def is_external_utility(self) -> bool:
        """Return True if this kind represents an external utility/system command block."""
        return self in {
            Kind.EMAIL,
            Kind.EXTERNAL_RUN,
            Kind.FS_COPY,
            Kind.FS_DELETE,
            Kind.WAIT_FILE,
        }
