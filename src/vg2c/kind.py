from enum import Enum


class Kind(str, Enum):  # noqa: UP042 - Python 3.11 enum string semantics
    SQL_QUERY = "SQL_QUERY"
    SQLITE_QUERY = "SQLITE_QUERY"
    HTML_REPORT = "HTML_REPORT"
    MACRO_CONTROL = "MACRO_CONTROL"
    ROWS_IN_FILE = "ROWS_IN_FILE"
    AED = "AED"
