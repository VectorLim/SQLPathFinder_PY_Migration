"""Handler for the SQL_Get_CSV_List macro."""

from __future__ import annotations

import re

from vg2c.frontend.models import SourceSpan
from vg2c.dataflow.models import CSVGenerationCall
from vg2c.dataflow.sql_macros.base import (
    MacroExpansion,
    MacroParseError,
    SqlMacroHandler,
    parse_column_ref,
    unquote_arg,
)

# Detects the `(<col> In ` wrap that some VG2 scripts put before
# SQL_Get_CSV_List(...) - an unmatched `(` that relies on the macro expansion
# to close it. Anchored to the end of before_text.
_CALL_SITE_WRAP_RE = re.compile(r"\(\s*[A-Za-z_][\w.\[\]@]*\s+In\s*$", re.IGNORECASE)


class SqlGetCsvListHandler(SqlMacroHandler):
    """Handler for SQL_Get_CSV_List(csv_path, column_ref, lead_in)."""

    name = "SQL_Get_CSV_List"

    def build_call(
        self,
        args: list[str],
        span: SourceSpan,
        before_text: str,
    ) -> MacroExpansion | MacroParseError:
        if len(args) != 3:
            return MacroParseError(
                "SQL_Get_CSV_List requires exactly 3 arguments; " f"got {len(args)}."
            )

        csv_path_raw = unquote_arg(args[0])
        column_raw = args[1].strip()
        lead_in = unquote_arg(args[2])
        column_ref = parse_column_ref(column_raw)

        call = CSVGenerationCall(
            name=self.name,
            csv_path=csv_path_raw,
            column_ref=column_ref,
            lead_in=lead_in,
            source_span=span,
        )

        # Detect if the call site has an unmatched opening paren that relies on
        # the macro expansion to close it, e.g. `(<col> In SQL_Get_CSV_List(...)`.
        appended = ")" if _CALL_SITE_WRAP_RE.search(before_text) else ""

        return MacroExpansion(
            call=call,
            appended_text=appended,
        )
