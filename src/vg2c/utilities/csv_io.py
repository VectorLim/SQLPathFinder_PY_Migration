"""Compiler operation metadata referencing runtime CSV signatures."""

from __future__ import annotations

from pathlib import Path

from vg2c.emitter.models import BlockEmission
from vg2c.emitter.operations import emittable
from vg2c.kind import Kind
from vg2c.runtime.csv_io import _CsvIO as RuntimeCsvIO
from vg2c.runtime.values import resolve_path
from vg2c.utilities._base import EmitterUtility, UtilitySpec
from vg2c.utilities._emit_helpers import (
    lower_native_call,
    split_utility_command,
    to_code_expr,
)


class CsvIO(RuntimeCsvIO, UtilitySpec):
    utility_name = "csv_io"
    iter = emittable(RuntimeCsvIO.iter)

    single_row = emittable(RuntimeCsvIO.single_row)

    sql_get_csv_list = emittable(RuntimeCsvIO.sql_get_csv_list)

    row_count = emittable(RuntimeCsvIO.row_count)

    iter_chunks = emittable(RuntimeCsvIO.iter_chunks)

    write = emittable(RuntimeCsvIO.write)


class SmartAppend(EmitterUtility):
    """Append CSV data while preserving one destination header."""

    utility_name = "smart_append"
    handles = (Kind.SMART_APPEND,)
    _COMMAND_NAME = "smartappend.va"

    @staticmethod
    def check(options) -> Kind | None:
        argv = split_utility_command(options.lookup.get("UTILITIES", ""))
        if not argv:
            return None
        basename = argv[0].replace("/", "\\").rsplit("\\", 1)[-1].lower()
        if basename == SmartAppend._COMMAND_NAME:
            return Kind.SMART_APPEND
        return None

    @classmethod
    def emit_block(cls, block, *, macros: str = "macros") -> BlockEmission:
        argv = split_utility_command(block.resolved_options.lookup.get("UTILITIES", ""))
        destination = to_code_expr(argv[1] if len(argv) > 1 else "")
        source = to_code_expr(argv[2] if len(argv) > 2 else "")
        call = lower_native_call(
            cls.append.render(destination, source),
            function="job.smart_append",
            macros=macros,
        )
        return BlockEmission((call,))

    @emittable
    def append(self, destination: str | Path, source: str | Path) -> None:
        from vg2c.runtime.files import smart_append

        smart_append(destination, source, workdir=resolve_path("."))
