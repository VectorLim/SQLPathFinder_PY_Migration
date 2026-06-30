"""Embeddable utility classes for generated scripts.

All classes in this package are registered for emission into the final
generated Python script via the `register_utility` decorator.
"""

from vg2c.emitter.utilities._registry import (
    UTILITIES,
    UTILITY_IMPORTS,
    register_utility,
)
from vg2c.emitter.utilities.crosstab import apply_crosstab
from vg2c.emitter.utilities.csv_io import CsvIO
from vg2c.emitter.utilities.external import ExternalProcess
from vg2c.emitter.utilities.fs_ops import FileSystemOps
from vg2c.emitter.utilities.macro_state import MacroState
from vg2c.emitter.utilities.mail import MailService
from vg2c.emitter.utilities.runtime_context import PipelineContext
from vg2c.emitter.utilities.sql_macros import SqlMacros
from vg2c.emitter.utilities.sqlite_engine import SqliteEngine
from vg2c.emitter.readers import read

__all__ = [
    "UTILITIES",
    "UTILITY_IMPORTS",
    "register_utility",
    "apply_crosstab",
    "CsvIO",
    "ExternalProcess",
    "FileSystemOps",
    "MacroState",
    "MailService",
    "PipelineContext",
    "read",
    "SqlMacros",
    "SqliteEngine",
]
