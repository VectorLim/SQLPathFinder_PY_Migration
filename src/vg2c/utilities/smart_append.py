"""SmartAppend.va support for header-aware CSV appends."""

from __future__ import annotations

import csv
from pathlib import Path

from vg2c.emitter.models import emittable
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import split_utility_command, to_code_expr
from vg2c.utilities._runtime_helpers import resolve_path
from vg2c.utility_metadata import FileEffectDefinition


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

    @emittable(
        display_name="Append File",
        file_effects=(
            FileEffectDefinition(
                "append",
                "append",
                inputs=("source",),
                outputs=("destination",),
                reason="Creates or appends when source has a header; preserves prior destination content.",
            ),
        )
    )
    def append(self, destination: str | Path, source: str | Path) -> None:
        from vg2c.runtime.append import smart_append
        smart_append(destination, source, workdir=resolve_path("."))
