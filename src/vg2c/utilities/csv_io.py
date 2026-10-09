"""Compiler metadata for the single-sourced runtime implementation."""

from __future__ import annotations
from vg2c.runtime.csv_io import _CsvIO as RuntimeCsvIO
from vg2c.utilities._base import UtilitySpec
from collections.abc import Iterator
from pathlib import Path
from typing import Any
from vg2c.emitter.models import emittable
from vg2c.utility_metadata import FileEffectDefinition


class CsvIO(RuntimeCsvIO, UtilitySpec):
    utility_name = "csv_io"
    semantic_visibility = "internal"
    script_settings = (
        (
            "VG2C_SQL_GET_CSV_LIST_CHUNK_SIZE",
            1000,
            "Maximum values emitted in each SQL_Get_CSV_List IN clause.",
        ),
    )
    @emittable(file_effects=(FileEffectDefinition("read", "read", inputs=("name",)),))
    def iter(self, name: str) -> Iterator[dict[str, str]]:
        return RuntimeCsvIO.iter(self, name)

    @emittable(file_effects=(FileEffectDefinition("read", "read", inputs=("name",)),))
    def single_row(self, name: str) -> dict[str, str]:
        return RuntimeCsvIO.single_row(self, name)

    @emittable(file_effects=(FileEffectDefinition("read", "read", inputs=("path",)),))
    def sql_get_csv_list(
        self,
        path: str,
        column_ref: int | str,
        lead_in: str,
        chunk_size: int = 1000,
    ) -> str:
        return RuntimeCsvIO.sql_get_csv_list(self, path, column_ref, lead_in, chunk_size)

    @emittable(
        file_effects=(FileEffectDefinition("observe", "observe", inputs=("name",)),)
    )
    def row_count(self, name: str) -> int:
        return RuntimeCsvIO.row_count(self, name)

    @emittable(
        file_effects=(
            FileEffectDefinition(
                "chunks", "transform", inputs=("input_name",), outputs=("chunk_name",)
            ),
        )
    )
    def iter_chunks(
        self, input_name: str, chunk_name: str, chunk_size: int
    ) -> Iterator[Path]:
        return RuntimeCsvIO.iter_chunks(self, input_name, chunk_name, chunk_size)

    @emittable(
        parameter_visibility={"content": "internal"},
        file_effects=(
            FileEffectDefinition(
                "write",
                "write",
                outputs=("name",),
                reason="Runtime content may itself be a file; input is not statically known.",
            ),
        ),
    )
    def write(self, name: str, content: Any, header: list[str] | None = None) -> None:
        return RuntimeCsvIO.write(self, name, content, header)
