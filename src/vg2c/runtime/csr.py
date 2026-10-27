"""Lot and comment inputs for direct AEDClient CSR calls."""

from __future__ import annotations

import csv

from vg2c.runtime.file_store import input_path
from vg2c.runtime.aed_client import identifier


def csr_lots(path, column, *, workdir, files=None):
    """Return the distinct lot IDs of one CSV column (header matched case-insensitively)."""
    with input_path(path, workdir, files).open(
        encoding="utf-8-sig", newline=""
    ) as stream:
        reader = csv.DictReader(stream, strict=True)
        names = {name.strip().casefold(): name for name in reader.fieldnames or []}
        key = names.get(column.casefold())
        if key is None:
            raise ValueError(f"CSR input {path} has no {column!r} column")
        lots = []
        for row in reader:
            lot = (row.get(key) or "").strip()
            if None in row or not lot:
                raise ValueError(f"CSR input {path} has a row without a lot")
            lots.append(identifier(lot, "CSR lot"))
    return list(dict.fromkeys(lots))


def csr_comment(note, ccb):
    """Build the legacy CSR hold comment; a CCB number is mandatory."""
    if not str(ccb).strip():
        raise ValueError("CSR requests require a CCB number")
    return f"{note}\nCCB: {ccb}"
