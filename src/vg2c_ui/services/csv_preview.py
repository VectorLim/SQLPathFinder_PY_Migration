from __future__ import annotations

import csv
import io
from collections.abc import Callable
from pathlib import Path

from vg2c_ui.domain.models import CsvPreview

MAX_PREVIEW_BYTES = 2 * 1024 * 1024
MAX_PREVIEW_ROWS = 200


class CsvPreviewService:
    def __init__(self, resolve: Callable[[str | Path], Path]) -> None:
        self._resolve = resolve

    def preview(self, source_path: str, csv_path: str) -> CsvPreview:
        source = self._resolve(source_path)
        candidate = Path(csv_path)
        path = self._resolve(candidate if candidate.is_absolute() else source.parent / candidate)
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
        return CsvPreview(
            path=str(path),
            columns=columns,
            rows=visible[1:] if visible else [],
            truncated=byte_truncated or row_truncated or size > len(payload),
            size_bytes=size,
        )


__all__ = ["CsvPreviewService", "MAX_PREVIEW_BYTES", "MAX_PREVIEW_ROWS"]
