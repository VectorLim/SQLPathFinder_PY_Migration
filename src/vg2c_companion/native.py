"""Read-only identification for native SQLPathFinder designer snapshots."""

from __future__ import annotations

import csv
import io
from pathlib import Path

from .models import NativeSnapshot


def inspect_native(path: Path) -> NativeSnapshot:
    path = Path(path).resolve()
    text = path.read_text(encoding="utf-8-sig", errors="replace")
    rows = list(csv.reader(io.StringIO(text, newline="")))
    version = None
    sections: list[str] = []
    for row in rows:
        if not row:
            continue
        marker = row[0].strip()
        upper = marker.upper()
        if upper.startswith("VERSION:"):
            inline = marker.split(":", 1)[1].strip()
            version = inline or (row[1].strip() if len(row) > 1 else "") or None
        elif upper in {"VERSION", "VERSION:"} and len(row) > 1:
            version = row[1].strip() or None
        if upper.startswith("TABLEGRID") or upper.startswith("QUERYGRID"):
            sections.append(marker.split(":", 1)[0])
    return NativeSnapshot(path, version, tuple(dict.fromkeys(sections)), len(text.splitlines()))

