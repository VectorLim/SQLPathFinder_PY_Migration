from __future__ import annotations

import csv
import io
from pathlib import Path

from vg2c_ui.api.models import CsvPreviewView

MAX_PREVIEW_BYTES = 2 * 1024 * 1024
MAX_PREVIEW_ROWS = 200


def read_csv_preview(path: Path) -> CsvPreviewView:
    """Read a bounded CSV preview from an already workspace-validated path."""
    size = path.stat().st_size
    with path.open("rb") as stream:
        payload = stream.read(MAX_PREVIEW_BYTES + 1)
    byte_truncated = len(payload) > MAX_PREVIEW_BYTES
    text = payload[:MAX_PREVIEW_BYTES].decode("utf-8-sig", errors="replace")
    reader = csv.reader(io.StringIO(text, newline=""))
    rows: list[list[str]] = []
    for index, row in enumerate(reader):
        if index > MAX_PREVIEW_ROWS:
            break
        rows.append(row)
    row_truncated = len(rows) > MAX_PREVIEW_ROWS
    visible = rows[:MAX_PREVIEW_ROWS]
    columns = visible[0] if visible else []
    return CsvPreviewView(
        path=str(path),
        columns=columns,
        rows=visible[1:] if visible else [],
        truncated=byte_truncated or row_truncated or size > len(payload),
        size_bytes=size,
    )


__all__ = ["MAX_PREVIEW_BYTES", "MAX_PREVIEW_ROWS", "read_csv_preview"]
