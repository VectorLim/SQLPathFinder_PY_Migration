"""Embeddable utility classes for generated scripts.

All classes in this package are registered for emission into the final
generated Python script via the `register_utility` decorator.
"""

from vg2c.emitter.utilities._emit_helpers import emit_block
from vg2c.emitter.utilities._registry import (
    UTILITIES,
    UTILITY_DEPENDENCIES,
    UTILITY_IMPORTS,
    assemble_registered_utilities,
    classify_utility_command,
    get_registered_source,
    mark_utility_used,
    register_utility,
)
from vg2c.emitter.utilities.crosstab import CrosstabUtility
from vg2c.emitter.utilities.csv_io import CsvIO
from vg2c.emitter.utilities.external import ExternalProcess
from vg2c.emitter.utilities.fs_ops import FileSystemOps
from vg2c.emitter.utilities.macro_state import MacroState
from vg2c.emitter.utilities.mail import MailService
from vg2c.emitter.utilities.pipeline_context import PipelineContext
from vg2c.emitter.utilities.sql_macros import SqlMacros
from vg2c.emitter.utilities.sqlite_engine import SqliteEngine
from vg2c.emitter.readers import ReaderRuntime

__all__ = [
    "UTILITIES",
    "UTILITY_DEPENDENCIES",
    "UTILITY_IMPORTS",
    "assemble_registered_utilities",
    "classify_utility_command",
    "emit_block",
    "get_registered_source",
    "mark_utility_used",
    "register_utility",
    "CrosstabUtility",
    "CsvIO",
    "ExternalProcess",
    "FileSystemOps",
    "MacroState",
    "MailService",
    "PipelineContext",
    "ReaderRuntime",
    "SqlMacros",
    "SqliteEngine",
]
