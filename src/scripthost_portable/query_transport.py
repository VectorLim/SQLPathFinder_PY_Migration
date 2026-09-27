from __future__ import annotations

import importlib
import os
from collections.abc import Iterator
from contextlib import contextmanager
from contextvars import ContextVar
from pathlib import Path
from typing import Any, Protocol

import pandas as pd

_CONFIG_REFERENCE_ENV = "SCRIPTHOST_DATASYNCX_CONFIG_REFERENCE"


class QueryTransportError(RuntimeError):
    """Base error for the portable query-transport boundary."""


class QueryTransportUnavailable(QueryTransportError):
    """The portable transport or its runtime dependency is unavailable."""


class QueryConfigurationError(QueryTransportError):
    """DataSyncX transport exists but reader construction/configuration failed."""


class UnsupportedQueryBackend(QueryTransportError):
    """The requested legacy node family is not covered by this transport seam."""


class QueryExecutionError(QueryTransportError):
    """The reader was selected successfully but query execution failed."""


class ReaderFactory(Protocol):
    def reader_for(self, backend: str, node: str) -> Any: ...


_reader_factory_override: ContextVar[ReaderFactory | None] = ContextVar(
    "scripthost_reader_factory", default=None
)


@contextmanager
def use_reader_factory(factory: ReaderFactory) -> Iterator[None]:
    """Temporarily inject readers without changing ScriptHost query semantics."""
    token = _reader_factory_override.set(factory)
    try:
        yield
    finally:
        _reader_factory_override.reset(token)


class DataSyncXReaderFactory:
    """Single provisional DataSyncX-facing module for live Intel validation.

    Session 2.6A intentionally does not claim these imports or constructors are
    correct against the private package. The local validation agent should
    adjust only this class if the real DataSyncX API differs.
    """

    _CLASS_CANDIDATES = {
        "mars": (
            ("datasyncx", "MarsReader"),
            ("datasyncx.readers", "MarsReader"),
            ("datasyncx.readers.mars", "MarsReader"),
        ),
        "aries": (
            ("datasyncx", "AriesReader"),
            ("datasyncx.readers", "AriesReader"),
            ("datasyncx.readers.aries", "AriesReader"),
        ),
        "oasys": (
            ("datasyncx", "OracleReader"),
            ("datasyncx.readers", "OracleReader"),
            ("datasyncx.readers.oracle", "OracleReader"),
        ),
    }

    def reader_for(self, backend: str, node: str) -> Any:
        candidates = self._CLASS_CANDIDATES.get(backend)
        if candidates is None:
            raise UnsupportedQueryBackend(
                f"Portable ScriptHost query transport does not support node {node!r}."
            )

        reader_type = _import_first(candidates, backend)
        try:
            if backend == "oasys":
                return reader_type(database="OASYS")
            return reader_type()
        except Exception as exc:
            config_reference = os.getenv(_CONFIG_REFERENCE_ENV)
            detail = f"; config reference={config_reference!r}" if config_reference else ""
            raise QueryConfigurationError(
                "DataSyncX reader construction/configuration failed for "
                f"{backend!r}{detail}. Constructor assumptions require live validation: {exc}"
            ) from exc


class PortableOracleConnection:
    """Compatibility object matching the tiny dbDriver surface nqOracleTask uses."""

    def __init__(self, queryOptions: Any = None, reader_factory: ReaderFactory | None = None):
        self.query_options = queryOptions
        self._factory = reader_factory
        self._node: str | None = None
        self._reader: Any = None

    def openConnection(
        self,
        username: str | None,
        password: str | None,
        node: str,
        retry: Any = None,
    ) -> None:
        # Authentication and retry are intentionally left to DataSyncX. Keeping
        # these arguments preserves the original dbDriver call contract.
        del username, password, retry
        backend = _backend_for_node(node)
        factory = self._factory or _reader_factory_override.get() or DataSyncXReaderFactory()
        self._reader = factory.reader_for(backend, node)
        self._node = node

    def execute(
        self,
        query: str,
        *,
        MyFetchSize: int = 50000,
        FirstConnect: bool = True,
        ll_NoHdrs: bool = False,
        OutExcel: str | None = None,
        OutFile: str | None = None,
        WorkDir: str = ".\\",
        OutTT: bool = False,
        FinalRow: int = 0,
        incrementalRunCtr: int = 0,
        MyHeaders: str | None = None,
    ) -> int:
        del MyFetchSize, WorkDir, OutTT, FinalRow, incrementalRunCtr, MyHeaders
        if self._reader is None or self._node is None:
            raise QueryTransportUnavailable("Portable query connection was not opened.")

        site = _site_from_node(self._node)
        try:
            result = self._reader.read(site=site, query=query)
            frame = _as_frame(result)
        except QueryTransportError:
            raise
        except Exception as exc:
            raise QueryExecutionError(
                f"DataSyncX-compatible reader failed for node {self._node!r}: {exc}"
            ) from exc

        target = OutExcel or OutFile
        if target and not frame.empty:
            _write_frame(frame, Path(target), first_connect=FirstConnect, no_headers=ll_NoHdrs)
        return len(frame.index)

    def executeExPlan(self, query: str, explain_plan: Any) -> int:
        del query, explain_plan
        raise UnsupportedQueryBackend(
            "Portable ScriptHost transport does not emulate legacy explain-plan execution."
        )

    def close(self, *args: Any, **kwargs: Any) -> None:
        del args, kwargs
        close = getattr(self._reader, "close", None)
        if callable(close):
            close()
        self._reader = None


def _backend_for_node(node: str) -> str:
    upper = node.upper()
    if "MARS" in upper:
        return "mars"
    if "ARIES" in upper:
        return "aries"
    if "OASYS" in upper:
        return "oasys"
    raise UnsupportedQueryBackend(
        f"Portable ScriptHost query transport does not support Oracle node {node!r}."
    )


def _site_from_node(node: str) -> str:
    value = node.strip()
    if not value:
        raise UnsupportedQueryBackend("Portable ScriptHost query transport received an empty node.")
    return value.split(".", 1)[0]


def _as_frame(result: Any) -> pd.DataFrame:
    if isinstance(result, pd.DataFrame):
        return result.copy()
    if hasattr(result, "to_pandas"):
        result = result.to_pandas()
        if isinstance(result, pd.DataFrame):
            return result.copy()
    if result is None:
        return pd.DataFrame()
    return pd.DataFrame(result)


def _delimiter(path: Path) -> str:
    suffix = path.suffix.lower()
    if suffix in {".tab", ".hive-tab", ".hive-sequence"}:
        return "\t"
    if suffix == ".asc":
        return "|"
    if suffix == ".plus":
        return "+"
    return ","


def _write_frame(
    frame: pd.DataFrame,
    path: Path,
    *,
    first_connect: bool,
    no_headers: bool,
) -> None:
    path = path.expanduser().resolve(strict=False)
    path.parent.mkdir(parents=True, exist_ok=True)
    create_new = first_connect or not path.exists()
    frame.to_csv(
        path,
        sep=_delimiter(path),
        mode="w" if create_new else "a",
        index=False,
        header=not no_headers if create_new else False,
        na_rep="",
        encoding="utf-8",
    )


def _import_first(candidates: tuple[tuple[str, str], ...], backend: str) -> type[Any]:
    failures: list[str] = []
    for module_name, attribute in candidates:
        try:
            module = importlib.import_module(module_name)
            reader_type = getattr(module, attribute)
        except (ImportError, AttributeError) as exc:
            failures.append(f"{module_name}:{attribute} ({type(exc).__name__})")
            continue
        if isinstance(reader_type, type):
            return reader_type
        failures.append(f"{module_name}:{attribute} (not a class)")
    raise QueryTransportUnavailable(
        "DataSyncX is unavailable or its reader API differs from the Session 2.6A "
        f"provisional mapping for {backend!r}. Tried: {', '.join(failures)}"
    )
