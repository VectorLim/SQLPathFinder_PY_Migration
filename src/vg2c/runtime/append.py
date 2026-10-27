"""Header-aware CSV append, extracted from utilities.smart_append."""
import csv
from pathlib import Path

from vg2c.runtime.file_store import input_path
from vg2c.runtime.values import resolve_path

def smart_append(destination: str | Path, source: str | Path, *, workdir, files=None) -> None:
    """Create or extend *destination* with source data rows exactly once."""
    source_path = input_path(source, workdir, files)
    destination_path = resolve_path(destination, for_write=True, workdir=workdir)
    destination_path.parent.mkdir(parents=True, exist_ok=True)
    if files is not None and destination_path.resolve() in files:
        existing = input_path(destination, workdir, files)
        if existing.exists():
            destination_path.write_bytes(existing.read_bytes())
        files.pop(destination_path.resolve())
    with source_path.open(newline='', encoding='utf-8', errors='replace') as source_fh:
        reader = csv.reader(source_fh)
        header = next(reader, None)
        if header is None:
            return
        write_header = not destination_path.exists() or destination_path.stat().st_size == 0
        with destination_path.open('w' if write_header else 'a', newline='', encoding='utf-8') as destination_fh:
            writer = csv.writer(destination_fh)
            if write_header:
                writer.writerow(header)
            writer.writerows(reader)
