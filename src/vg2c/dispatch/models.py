from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path
from typing import Any, Literal, Mapping

from vg2c.dataflow.models import AnalyzedProgram
from vg2c.frontend.models import Diagnostic

Dialect = Literal["oracle_mars", "oracle_oasys", "oracle_aries", "sqlite"]


@dataclass(frozen=True, slots=True)
class ReaderTarget:
    record_name: str | None
    record_version: str | None
    node: str
    instance: str | None


@dataclass(frozen=True, slots=True)
class DispatchedBlock:
    block_index: int
    reader_cls: type[Any]
    reader_target: ReaderTarget
    rewritten_sql: str


@dataclass(frozen=True, slots=True)
class DispatchConfig:
    oasys_schema: str = ""
    aries_schema: str | None = None
    view_registry_path: Path | None = None


@dataclass(frozen=True, slots=True)
class DispatchedProgram:
    analyzed: AnalyzedProgram
    dispatched: tuple[DispatchedBlock, ...]
    diagnostics: tuple[Diagnostic, ...]
