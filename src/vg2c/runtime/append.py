"""Header-aware CSV append, extracted from utilities.smart_append."""
import csv
from pathlib import Path
from vg2c.utilities._runtime_helpers import resolve_path

def smart_append(destination: str | Path, source: str | Path, *, workdir) -> None:
    """Create or extend *destination* with source data rows exactly once."""
    source_path = resolve_path(source, workdir=workdir)
    destination_path = resolve_path(destination, for_write=True, workdir=workdir)
    destination_path.parent.mkdir(parents=True, exist_ok=True)
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
