"""SmartAppend.va support for header-aware CSV appends."""

from __future__ import annotations

import csv
from pathlib import Path

from vg2c.emitter.models import emittable
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import split_utility_command, to_code_expr
from vg2c.utilities._runtime_helpers import resolve_path


class SmartAppend(EmitterUtility):
    """Append CSV data while preserving one destination header."""

    utility_name = "smart_append"
    handles = (Kind.SMART_APPEND,)
    _COMMAND_NAME = "smartappend.va"

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        argv = split_utility_command(options.lookup.get("UTILITIES", ""))
        if not argv:
            return None
        basename = argv[0].replace("/", "\\").rsplit("\\", 1)[-1].lower()
        if basename == SmartAppend._COMMAND_NAME:
            return Kind.SMART_APPEND, "/UTILITIES command maps to SmartAppend"
        return None

    @classmethod
    def emit_block(cls, block, *, global_refs=None) -> tuple[str, list[str]]:
        argv = split_utility_command(block.resolved_options.lookup.get("UTILITIES", ""))
        destination = to_code_expr(argv[1] if len(argv) > 1 else "")
        source = to_code_expr(argv[2] if len(argv) > 2 else "")
        return "smart_append", [cls.append.render(destination, source)]

    @emittable
    def append(self, destination: str | Path, source: str | Path) -> None:
        """Create or extend *destination* with source data rows exactly once."""
        source_path = resolve_path(source)
        destination_path = resolve_path(destination, for_write=True)
        destination_path.parent.mkdir(parents=True, exist_ok=True)

        with source_path.open(newline="", encoding="utf-8", errors="replace") as source_fh:
            reader = csv.reader(source_fh)
            header = next(reader, None)
            if header is None:
                return

            write_header = not destination_path.exists() or destination_path.stat().st_size == 0
            with destination_path.open(
                "w" if write_header else "a", newline="", encoding="utf-8"
            ) as destination_fh:
                writer = csv.writer(destination_fh)
                if write_header:
                    writer.writerow(header)
                writer.writerows(reader)
