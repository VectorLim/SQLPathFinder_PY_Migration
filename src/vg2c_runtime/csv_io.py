"""CsvIO — lightweight CSV reader/writer over stdlib csv."""

from __future__ import annotations

import csv
from pathlib import Path
from typing import Any, Iterator

import pandas


class CsvIO:
    """Read and write CSV files relative to ``cwd``."""

    # ------------------------------------------------------------------
    # Read
    # ------------------------------------------------------------------

    def iter(self, name: str) -> Iterator[dict[str, str]]:
        """Yield each data row as a dict keyed by header names."""
        path = Path(name)
        with path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.DictReader(fh)
            yield from reader

    def read(self, name: str) -> Path:
        """Return the resolved Path (used when a downstream step needs a file reference)."""
        return Path(name).resolve()

    def row_count(self, name: str) -> int:
        """Count data rows (excludes header); 0 if file missing."""
        path = Path(name)
        if not path.exists():
            return 0
        with path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.reader(fh)
            next(reader, None)  # skip header
            return sum(1 for _ in reader)

    # ------------------------------------------------------------------
    # Write
    # ------------------------------------------------------------------

    def write(self, name: str, content: Any, header: list[str] | None = None) -> None:
        """Write *content* to a CSV file.

        *content* can be:
        - a list of dicts  → written via DictWriter (keys as header)
        - a list of lists  → written via writer (optional *header* for first row)
        - a string         → written as raw text (no CSV encoding)
        - a Path           → copied verbatim

        When *header* is provided, it becomes the authoritative column list:
        - Empty content → writes header-only CSV
        - List-of-dicts → projects each dict to *header* columns (missing keys → "")
        - DataFrame → reindexes columns to *header* (missing → "")
        - List-of-lists → writes *header* then data rows
        - Rows that duplicate the header are dropped
        """
        path = Path(name)
        path.parent.mkdir(parents=True, exist_ok=True)

        # Raw string/Path pass-through (no header processing)
        if isinstance(content, str):
            path.write_text(content, encoding="utf-8")
            return

        if isinstance(content, Path):
            import shutil

            shutil.copy2(content, path)
            return

        # DataFrame with header reindexing
        if isinstance(content, pandas.DataFrame):
            if header:
                # Reindex to declared header, fill missing with empty string
                content = content.reindex(columns=header, fill_value="")
            content.to_csv(path, index=False, encoding="utf-8")
            return

        # Tabular content (list of dicts/lists)
        rows = list(content) if content is not None else []

        # Empty content: write header-only when header is provided
        if not rows:
            if header:
                with path.open("w", newline="", encoding="utf-8") as fh:
                    csv.writer(fh).writerow(header)
            else:
                path.write_text("", encoding="utf-8")
            return

        with path.open("w", newline="", encoding="utf-8") as fh:
            if isinstance(rows[0], dict):
                # List-of-dicts: use declared header or infer from first row
                fieldnames = header if header else list(rows[0].keys())
                writer = csv.DictWriter(
                    fh, fieldnames=fieldnames, extrasaction="ignore"
                )
                writer.writeheader()
                # Project each dict to fieldnames (missing keys → "")
                filtered = _drop_duplicate_header_rows(rows, fieldnames)
                for row in filtered:
                    writer.writerow({k: row.get(k, "") for k in fieldnames})
            else:
                # List-of-lists
                writer_plain = csv.writer(fh)
                if header:
                    writer_plain.writerow(header)
                    filtered = _drop_duplicate_header_rows(rows, header)
                    writer_plain.writerows(filtered)
                else:
                    writer_plain.writerows(rows)
