"""RowsInFile - count rows in a CSV and store the result in a macro variable."""

from __future__ import annotations

import re

from vg2c.emitter.models import CodeExpr
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import split_utility_command, strip_quotes


class RowsInFile(EmitterUtility):
    """Count rows in a CSV file and store the count in a named macro variable.

    VG2 syntax::

        /UTILITIES={ROWS-IN-FILE} "<csv_path>" "<var_name>" "<prompt_off>"

    The third argument (``Y``/``N``) is the original VG2 prompt-suppression
    flag; the generated Python never prompts the user, so it is parsed but
    intentionally ignored during emission.
    """

    utility_name = "rows_in_file"
    handles = (Kind.ROWS_IN_FILE,)
    _TOKEN_RE = re.compile(r"^\s*\{ROWS-IN-FILE\}", re.IGNORECASE)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        utilities = options.lookup.get("UTILITIES", "")
        if RowsInFile._TOKEN_RE.match(utilities):
            return Kind.ROWS_IN_FILE, "/UTILITIES is {ROWS-IN-FILE}"
        return None

    @classmethod
    def emit_block(cls, block) -> tuple[str, list[str]] | None:
        from vg2c.utilities.csv_io import CsvIO
        from vg2c.utilities.macro_state import MacroState

        utilities = block.resolved_options.lookup.get("UTILITIES", "")
        argv = split_utility_command(utilities)
        # argv[0] = '{ROWS-IN-FILE}', argv[1] = csv_path, argv[2] = var_name
        csv_path_expr = MacroState.to_code_expr(argv[1] if len(argv) > 1 else None)
        var_name = strip_quotes(argv[2]).upper() if len(argv) > 2 else ""

        row_count_call = CsvIO.row_count.render(csv_path_expr)
        stmt = MacroState.set_named.render(var_name, CodeExpr(f"str({row_count_call})"))
        return "rows_in_file", [stmt]
