"""Embeddable utility classes for generated scripts.

All concrete ``UtilitySpec`` subclasses are auto-registered via
``UtilitySpec.__init_subclass__`` and emitted into generated scripts.
"""

from __future__ import annotations

import logging
from pathlib import Path
from typing import TYPE_CHECKING

from vg2c.utilities._base import EmitterUtility, UtilitySpec

if TYPE_CHECKING:
    from vg2c.emitter.models import StepEmission
    from vg2c.utilities._symbol_emit import EmbeddedSymbols

# Concrete utility classes are imported lazily in assemble_utilities()
# to avoid circular imports (utilities→emitter→dispatch→dataflow loop).
# Only base classes are imported here.

_CONCRETE_UTILS_LOADED = False
log = logging.getLogger("vg2c.utilities")


def ensure_utility_checks_loaded() -> None:
    """Import concrete utility modules once to register check/emit handlers."""

    global _CONCRETE_UTILS_LOADED
    if _CONCRETE_UTILS_LOADED:
        return

    from vg2c.logger import Logger  # noqa: F401
    from vg2c.utilities.crosstab import CrosstabUtility  # noqa: F401
    from vg2c.utilities.csv_io import CsvIO  # noqa: F401
    from vg2c.utilities.external import ExternalProcess  # noqa: F401
    from vg2c.utilities.fs_ops import FileSystemOps  # noqa: F401
    from vg2c.utilities.generic import UnknownUtility  # noqa: F401
    from vg2c.utilities.html_report import HtmlReport  # noqa: F401
    from vg2c.utilities.macro_state import MacroState  # noqa: F401
    from vg2c.utilities.mail import MailService  # noqa: F401
    from vg2c.utilities.oracle_client import OracleClient  # noqa: F401
    from vg2c.utilities.pipeline_context import PipelineContext  # noqa: F401
    from vg2c.utilities.python_embed import PythonEmbed  # noqa: F401
    from vg2c.utilities.rows_in_file import RowsInFile  # noqa: F401
    from vg2c.utilities.smart_append import SmartAppend  # noqa: F401
    from vg2c.utilities.sqlite_engine import SqliteEngine  # noqa: F401
    from vg2c.utilities.sqlite_reader import SqliteReader  # noqa: F401
    from vg2c.utilities.wait_file import WaitFile  # noqa: F401

    _CONCRETE_UTILS_LOADED = True
    log.debug("Loaded concrete utility modules for check/emit registration.")


def assemble_utilities(
    *,
    step_emissions: list[StepEmission] | tuple[StepEmission, ...],
    workflow_source: str,
    reader_names: set[str],
    reader_imports: set[str] | frozenset[str] = frozenset(),
) -> EmbeddedSymbols:
    """Resolve actual emitted calls, including expressions absent from invocation metadata."""
    from vg2c.utilities._symbol_emit import render_symbols
    from vg2c.utilities._symbol_index import SymbolRef
    from vg2c.utilities._symbols import SymbolResolver

    ensure_utility_checks_loaded()
    utilities = {
        name: SymbolRef(cls.__module__, cls.__name__) for name, cls in UtilitySpec._registry.items()
    }
    resolver = SymbolResolver(Path(__file__).resolve().parents[2], utilities)
    for step in step_emissions:
        for invocation in step.invocations:
            operation = invocation.operation
            resolver.context_member(operation.utility_name, operation.method)
    for name in sorted(reader_names):
        resolver.seed_runtime_api(utilities[name])
    resolver.require(utilities["ctx"])
    resolver.require(utilities["logger"])
    resolver.scan_workflow(
        "\n\n".join([*(step.source for step in step_emissions), workflow_source])
    )
    resolver.drain()
    resolver.expand_runtime_apis()
    return render_symbols(resolver.drain(), external_imports=reader_imports)


__all__ = ["ensure_utility_checks_loaded", "assemble_utilities", "EmitterUtility", "UtilitySpec"]
