"""Experimental portable facade over the decompiled ScriptHost runtime.

This package exists only for the architecture reassessment. It does not replace
or delete vg2c_new.
"""

from .query_transport import (
    DataSyncXReaderFactory,
    PortableOracleConnection,
    QueryConfigurationError,
    QueryExecutionError,
    QueryTransportUnavailable,
    UnsupportedQueryBackend,
    use_reader_factory,
)
from .runtime import PortableScriptHostRuntime

__all__ = [
    "DataSyncXReaderFactory",
    "PortableOracleConnection",
    "PortableScriptHostRuntime",
    "QueryConfigurationError",
    "QueryExecutionError",
    "QueryTransportUnavailable",
    "UnsupportedQueryBackend",
    "use_reader_factory",
]
