"""Original ScriptHost execution with small portable OS/transport operations.

Use worker.run_job for production: one fresh isolated child process per VG2 job.
PortableScriptHostRuntime is the in-process entry used inside that child and by
characterization tests; it is not safe for concurrent threads.
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
