from __future__ import annotations

from dataclasses import dataclass

from vg2c.frontend.models import copy_dataclass_fields
from vg2c.resolver.models import ResolvedBlock, ResolvedProgram


@dataclass(frozen=True, slots=True)
class ReaderSpec:
    """Compile-time identity for a runtime reader implementation."""

    module: str
    name: str
    utility_name: str | None = None

    @property
    def id(self) -> str:
        return f"{self.module}:{self.name}"


@dataclass(frozen=True, slots=True)
class ReaderTarget:
    node: str


@dataclass(frozen=True, slots=True)
class DispatchedBlock(ResolvedBlock):
    reader: ReaderSpec
    reader_kwargs: dict[str, object]
    reader_target: ReaderTarget
    rewritten_sql: str

    def __init__(
        self,
        resolved: ResolvedBlock,
        reader: ReaderSpec,
        reader_kwargs: dict[str, object],
        reader_target: ReaderTarget,
        rewritten_sql: str,
    ) -> None:
        copy_dataclass_fields(resolved, self, ResolvedBlock)

        object.__setattr__(self, "reader", reader)
        object.__setattr__(self, "reader_kwargs", reader_kwargs)
        object.__setattr__(self, "reader_target", reader_target)
        object.__setattr__(self, "rewritten_sql", rewritten_sql)


@dataclass(frozen=True, slots=True)
class DispatchedProgram:
    resolved: ResolvedProgram
    dispatched: tuple[DispatchedBlock, ...]
