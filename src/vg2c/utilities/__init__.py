"""Compiler utility handlers and their on-demand registration.

Concrete handlers classify and emit project calls. Generated projects execute
``vg2c.runtime`` directly; handler classes are not embedded in ``main.py``.
"""

from __future__ import annotations

import logging


def __getattr__(name: str):
    if name not in {"EmitterUtility", "UtilitySpec"}:
        raise AttributeError(name)
    from vg2c.utilities import _base

    return getattr(_base, name)

# Import concrete utility handlers only when the compiler needs them.
# Keep the package facade lazy to avoid import cycles.


_CONCRETE_UTILS_LOADED = False
log = logging.getLogger("vg2c.utilities")


def ensure_utility_checks_loaded() -> None:
    """Import concrete utility modules once to register check/emit handlers."""

    global _CONCRETE_UTILS_LOADED
    if _CONCRETE_UTILS_LOADED:
        return

    from vg2c.logger import Logger  # noqa: F401
    from vg2c.utilities.csv_io import CsvIO, SmartAppend  # noqa: F401
    from vg2c.utilities.external import ExternalProcess  # noqa: F401
    from vg2c.utilities.fs_ops import FileSystemOps  # noqa: F401
    from vg2c.utilities.html_report import HtmlReport  # noqa: F401
    from vg2c.utilities.macro_control import MacroControl  # noqa: F401
    from vg2c.utilities.mail import MailService  # noqa: F401
    from vg2c.utilities.direct_operations import DirectOperationMetadata  # noqa: F401
    from vg2c.utilities.runtime_adapters import (  # noqa: F401
        CrosstabUtility,
        DuckDbReader,
        OracleClient,
        SqliteReader,
    )
    from vg2c.utilities.rows_in_file import RowsInFile  # noqa: F401
    from vg2c.utilities.sqlite_engine import SqliteEngine  # noqa: F401

    _CONCRETE_UTILS_LOADED = True
    log.debug("Loaded concrete utility modules for check/emit registration.")


__all__ = ["ensure_utility_checks_loaded", "EmitterUtility", "UtilitySpec"]
