from __future__ import annotations

# Compatibility shim - delegates to handlers
from vg2c.dispatch.models import Dialect, ReaderTarget
from vg2c.dispatch.registry import get_handler
from vg2c.frontend.models import Diagnostic
from vg2c.resolver.models import ResolvedBlock


def build_target(
    block: ResolvedBlock,
    dialect: Dialect,
) -> tuple[ReaderTarget, list[Diagnostic]]:
    """Build a ReaderTarget from a resolved block and its resolved dialect."""
    handler = get_handler(dialect)
    if handler is None:
        # Fallback for unknown dialects
        opts = block.resolved_options.lookup
        return (
            ReaderTarget(
                dialect=dialect,
                reader_class_hint=(
                    "SQLiteReader" if dialect == "sqlite" else "OracleReader"
                ),
                database_arg=None,
                record_name=None,
                record_version=None,
                node=opts.get("NODE", ""),
                instance=opts.get("INSTANCE"),
            ),
            [],
        )
    return handler.build_reader_target(block)
