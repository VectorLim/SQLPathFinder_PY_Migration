"""Embeddable utility classes for generated scripts.

All concrete ``UtilitySpec`` subclasses are auto-registered via
``UtilitySpec.__init_subclass__`` and emitted into generated scripts.
"""

from __future__ import annotations

import logging

from vg2c.utilities._base import EmitterUtility, UtilitySpec

# Concrete utility classes are imported lazily by the compiler embedding pass
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
__all__ = ["ensure_utility_checks_loaded", "EmitterUtility", "UtilitySpec"]
