"""Compiler operation metadata referencing runtime CSV signatures."""

from __future__ import annotations

from vg2c.emitter.operations import emittable
from vg2c.runtime.csv_io import _CsvIO as RuntimeCsvIO
from vg2c.utilities._base import UtilitySpec
from vg2c.utility_metadata import FileEffectDefinition


class CsvIO(UtilitySpec):
    utility_name = "csv_io"
    semantic_visibility = "internal"
    script_settings = (
        (
            "VG2C_SQL_GET_CSV_LIST_CHUNK_SIZE",
            1000,
            "Maximum values emitted in each SQL_Get_CSV_List IN clause.",
        ),
    )
    iter = emittable(
        file_effects=(FileEffectDefinition("read", "read", inputs=("name",)),)
    )(RuntimeCsvIO.iter)

    single_row = emittable(
        file_effects=(FileEffectDefinition("read", "read", inputs=("name",)),)
    )(RuntimeCsvIO.single_row)

    sql_get_csv_list = emittable(
        file_effects=(FileEffectDefinition("read", "read", inputs=("path",)),)
    )(RuntimeCsvIO.sql_get_csv_list)

    row_count = emittable(
        file_effects=(FileEffectDefinition("observe", "observe", inputs=("name",)),)
    )(RuntimeCsvIO.row_count)

    iter_chunks = emittable(
        file_effects=(
            FileEffectDefinition(
                "chunks", "transform", inputs=("input_name",), outputs=("chunk_name",)
            ),
        )
    )(RuntimeCsvIO.iter_chunks)

    write = emittable(
        parameter_visibility={"content": "internal"},
        file_effects=(
            FileEffectDefinition(
                "write",
                "write",
                outputs=("name",),
                reason="Runtime content may itself be a file; input is not statically known.",
            ),
        ),
    )(RuntimeCsvIO.write)
