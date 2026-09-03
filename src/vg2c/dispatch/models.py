from __future__ import annotations

from dataclasses import dataclass
from typing import Any

from vg2c.dataflow.models import AnalyzedProgram
from vg2c.frontend.models import copy_dataclass_fields
from vg2c.resolver.models import ResolvedBlock


@dataclass(frozen=True, slots=True)
class ReaderTarget:
    record_name: str | None
    record_version: str | None
    node: str
    instance: str | None
    site: str = ""  # leading literal site token of `node` (e.g. "KM"), "" if unresolvable


@dataclass(frozen=True, slots=True)
class SQLFilter:
    step_name: str
    attributes: tuple[str, ...]
    sql_statement: str


@dataclass(frozen=True, slots=True)
class DispatchedBlock(ResolvedBlock):
    reader_cls: type[Any]
    reader_kwargs: dict[str, Any]
    reader_target: ReaderTarget
    rewritten_sql: str
    step_name: str
    sql_filters: tuple[SQLFilter, ...] = ()

    def __init__(
        self,
        resolved: ResolvedBlock,
        reader_cls: type[Any],
        reader_kwargs: dict[str, Any],
        reader_target: ReaderTarget,
        rewritten_sql: str,
        step_name: str,
        sql_filters: tuple[SQLFilter, ...] = (),
    ) -> None:
        copy_dataclass_fields(resolved, self, ResolvedBlock)

        object.__setattr__(self, "reader_cls", reader_cls)
        object.__setattr__(self, "reader_kwargs", reader_kwargs)
        object.__setattr__(self, "reader_target", reader_target)
        object.__setattr__(self, "rewritten_sql", rewritten_sql)
        object.__setattr__(self, "step_name", step_name)
        object.__setattr__(self, "sql_filters", sql_filters)


@dataclass(frozen=True, slots=True)
class DispatchedProgram:
    analyzed: AnalyzedProgram
    dispatched: tuple[DispatchedBlock, ...]
