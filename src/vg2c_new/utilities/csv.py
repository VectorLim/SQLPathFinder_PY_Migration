from __future__ import annotations

import csv
import zipfile
from pathlib import Path
from typing import TYPE_CHECKING

import pandas as pd

from vg2c_new.paths import resolve_path, working_directory_for

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState


class CsvUtility:
    """Shared stateless delimited-file behavior used by direct-runtime utilities."""

    @staticmethod
    def delimiter(path: str | Path, *, output: bool = False) -> str:
        """Direct port of ScriptHost Utilities.GetFileDLM for portable file types.

        Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.GetFileDLM.
        Reference source/commit: ScriptHost source vendored at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: DIRECT PORT.
        Preserved: TAB/HIVE-TAB/HIVE-SEQUENCE, ASC, PLUS and CSV/default delimiters.
        Amendments: output .txt uses comma because csv writers require a delimiter.
        Intentionally discarded: SDB/JSON/PMPK delimiter errors outside CSV-owned paths.
        """
        suffix = Path(path).suffix.casefold()
        if suffix in {".tab", ".hive-tab", ".hive-sequence"}:
            return "\t"
        if suffix == ".asc":
            return "|"
        if suffix == ".plus":
            return "+"
        return ","

    @classmethod
    def read_dataframe(cls, path: Path, *, nrows: int | None = None) -> pd.DataFrame:
        return pd.read_csv(
            path,
            sep=cls.delimiter(path),
            dtype=str,
            keep_default_na=False,
            encoding="utf-8-sig",
            encoding_errors="replace",
            nrows=nrows,
        )

    @classmethod
    def write_dataframe(
        cls, frame: pd.DataFrame, path: Path, *, append: bool = False, header: bool = True
    ) -> None:
        path.parent.mkdir(parents=True, exist_ok=True)
        frame.to_csv(
            path,
            sep=cls.delimiter(path, output=True),
            index=False,
            mode="a" if append else "w",
            header=header,
            encoding="utf-8",
            lineterminator="\n",
        )

    @classmethod
    def row_count(
        cls, path: Path, *, limit_to_one: bool = False, archive_name: str | None = None
    ) -> int:
        """Amended port of ScriptHost Utilities.getRowCountFromFile.

        Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.getRowCountFromFile.
        Reference source/commit: ScriptHost source vendored at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: header-excluding counts, optional early count-to-one, ZIP member and Parquet support.
        Amendments: stdlib zipfile and pandas replace ScriptHost helpers.
        Intentionally discarded: codec retry/global console behavior.
        """
        if path.suffix.casefold() == ".parquet":
            frame = pd.read_parquet(path)
            return min(len(frame), 1) if limit_to_one else len(frame)
        if path.suffix.casefold() == ".zip":
            with zipfile.ZipFile(path) as archive:
                members = [name for name in archive.namelist() if not name.endswith("/")]
                if archive_name:
                    name = archive_name
                elif len(members) == 1:
                    name = members[0]
                else:
                    raise ValueError(
                        "ZIP row count requires archive member name when archive has multiple files."
                    )
                with archive.open(name) as handle:
                    lines = sum(1 for _ in handle)
            return max(0, min(lines - 1, 1) if limit_to_one else lines - 1)
        with path.open("r", encoding="utf-8-sig", errors="replace", newline="") as handle:
            reader = csv.reader(handle, delimiter=cls.delimiter(path))
            next(reader, None)
            if limit_to_one:
                return 1 if next(reader, None) is not None else 0
            return sum(1 for _ in reader)

    @classmethod
    def first_value(cls, path: Path, column: str) -> str:
        """Direct port of ScriptHost ValueInFile first-row lookup.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: ValueInFileTask.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: DIRECT PORT.
        Preserved: case-insensitive named-column lookup and first data-row value.
        Amendments: stdlib csv replaces MemTable.
        Intentionally discarded: global task state.
        """
        with path.open("r", encoding="utf-8-sig", errors="replace", newline="") as handle:
            reader = csv.DictReader(handle, delimiter=cls.delimiter(path))
            fieldnames = reader.fieldnames or []
            matches = {name.casefold(): name for name in fieldnames if name is not None}
            actual = matches.get(column.casefold())
            if actual is None:
                raise KeyError(column)
            row = next(reader, None)
            return "EMPTY" if row is None else str(row.get(actual, ""))

    @classmethod
    def sql_get_csv_list(
        cls, path: Path, column_ref: int | str, lead_in: str, *, chunk_size: int = 1000
    ) -> str:
        """Amended reuse of the current-project SQL_Get_CSV_List implementation.

        Source: src/vg2c/utilities/csv_io.py :: CsvIO.sql_get_csv_list/_read_column.
        Reference source/commit: SQLPathFinder 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: 1-based/named column selection, repeated-header suppression, de-duplication,
        quote escaping, Oracle-style 1000-value chunking and empty sentinel.
        Amendments: explicit Path input and no emitter/runtime service dependency.
        Intentionally discarded: generated-Python decorators and PipelineContext ownership.
        """
        if chunk_size < 1:
            raise ValueError("SQL_Get_CSV_List chunk size must be positive.")
        with path.open("r", encoding="utf-8-sig", errors="replace", newline="") as handle:
            reader = csv.reader(handle, delimiter=cls.delimiter(path))
            header = next(reader, [])
            if isinstance(column_ref, int):
                index = column_ref - 1
            else:
                lowered = [value.casefold() for value in header]
                try:
                    index = lowered.index(str(column_ref).casefold())
                except ValueError:
                    return "('__NO_VALUES__')"
            seen: dict[str, None] = {}
            for row in reader:
                if row == header:
                    continue
                if index < len(row):
                    seen.setdefault(row[index], None)
        values = list(seen)
        if not values:
            return "('__NO_VALUES__')"
        chunks: list[str] = []
        for offset in range(0, len(values), chunk_size):
            quoted = ", ".join(
                "'" + value.replace("'", "''") + "'"
                for value in values[offset : offset + chunk_size]
            )
            chunks.append(f"({quoted})")
        return (f"\nOR {lead_in} ").join(chunks)

    @classmethod
    def command_path(cls, command: Command, state: RuntimeState, value: str) -> Path:
        base = working_directory_for(command.option("WORKDIR"), state)
        return resolve_path(value, state, base=base)
