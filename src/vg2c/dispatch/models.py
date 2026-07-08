from __future__ import annotations

from dataclasses import dataclass
from typing import Any

from vg2c.dataflow.models import AnalyzedProgram
from vg2c.frontend.models import Diagnostic


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
class DispatchedProgram:
    analyzed: AnalyzedProgram
    dispatched: tuple[DispatchedBlock, ...]
    diagnostics: tuple[Diagnostic, ...]
