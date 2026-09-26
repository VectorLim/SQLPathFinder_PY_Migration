"""Linux-safe helpers extracted from current ScriptHost utility implementations.

These functions are intentionally stateless. Historical Utilities methods delegate here so
the direct runtime and ScriptHost reference path share one algorithm without SPFGlobals.
"""

from __future__ import annotations

import os
import shutil
import zipfile
from pathlib import Path
from typing import Iterable


def get_file_delimiter(file_name: str | os.PathLike[str], mode: str = "I") -> str:
    """Return the current ScriptHost delimiter for a supported file extension.

    Extracted from Utilities.GetFileDLM. mode="O" preserves the historical empty
    delimiter for .txt outputs; callers requiring CSV output may adapt it.
    """
    if file_name is None:
        raise ValueError("FileName cannot be None")

    suffix = Path(file_name).suffix.casefold()
    if suffix in {".sdb", ".json", ".pmpk"}:
        raise ValueError(f"File extension not supported to have a delimiter: {file_name}")
    if suffix in {".tab", ".hive-tab", ".hive-sequence"}:
        return "\t"
    if suffix == ".asc":
        return "|"
    if suffix == ".txt":
        return "," if mode.upper() == "I" else ""
    if suffix == ".plus":
        return "+"
    return ","


def zip_files(
    files_to_zip: Iterable[str | os.PathLike[str]],
    archive_name: str | os.PathLike[str],
    *,
    retain_relative_path: bool = True,
    delete_sources: bool = False,
    source_exists_check_done: bool = True,
) -> Path:
    """Create an archive using the current ScriptHost ZipFiles2 semantics."""
    files = [Path(item) for item in files_to_zip]
    if not source_exists_check_done:
        files = [item for item in files if item.exists()]

    archive = Path(archive_name)
    archive.parent.mkdir(parents=True, exist_ok=True)
    with zipfile.ZipFile(
        archive,
        "w",
        compression=zipfile.ZIP_DEFLATED,
        allowZip64=True,
        compresslevel=9,
    ) as writer:
        for item in files:
            if not item.exists() or not item.is_file():
                continue
            if retain_relative_path and not item.is_absolute():
                arcname = item.relative_to(Path("."))
            else:
                arcname = item.name
            writer.write(item, arcname)

    if delete_sources:
        for item in files:
            item.unlink(missing_ok=True)
    return archive


def zip_folder(
    source_folder: str | os.PathLike[str],
    archive_name: str | os.PathLike[str],
    *,
    delete_source_folder: bool = False,
) -> Path:
    """Create a ZIP from a folder using current ScriptHost ZipFolder2 behavior."""
    source = Path(source_folder)
    archive = Path(archive_name)
    archive.parent.mkdir(parents=True, exist_ok=True)
    if archive.exists():
        archive.unlink()

    base_name = archive.with_suffix("")
    created = Path(shutil.make_archive(str(base_name), "zip", root_dir=source))
    if created != archive:
        if archive.exists():
            archive.unlink()
        created.replace(archive)

    if delete_source_folder:
        shutil.rmtree(source)
    return archive
