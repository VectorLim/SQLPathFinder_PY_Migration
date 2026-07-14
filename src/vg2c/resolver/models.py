"""Resolver output model.

High-level types produced by the resolver stage: parsed SQL-macro calls,
a per-block ``ResolvedBlock``, and the top-level ``ResolvedProgram`` tree.

Operand payloads (``StartMacro``, ``IfThen``, ``RunLoop`` …) and the
``ScopeNode`` / ``MacroControlPayload`` primitives live in
``vg2c.resolver.operands``.
"""

from __future__ import annotations

from dataclasses import dataclass
from typing import Literal

from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    Diagnostic,
    SourceSpan,
    copy_dataclass_fields,
)
from vg2c.resolver.operands import MacroControlPayload, ScopeNode


@dataclass(frozen=True, slots=True)
class SqlGetCsvListCall:
    """Parsed SQL_Get_CSV_List macro call."""

    name: Literal["SQL_Get_CSV_List"]
    csv_path: str
    column_ref: int | str
    lead_in: str
    source_span: SourceSpan

    def consumed_csv_paths(self) -> tuple[str, ...]:
        """Return CSV paths consumed by this call."""
        return (self.csv_path,)


# Union grows as more macros are added (e.g. SqlGetCsvListCall | SqlTimeRangeCall)
SqlMacroCall = SqlGetCsvListCall


@dataclass(frozen=True, slots=True)
class ResolvedBlock(ClassifiedBlock):
    resolved_options: BlockOptions
    resolved_body: str
    sql_macro_calls: tuple[SqlMacroCall, ...]
    control_payload: MacroControlPayload | None
    scope_id: int

    def __init__(
        self,
        classified: ClassifiedBlock,
        resolved_options: BlockOptions,
        resolved_body: str,
        sql_macro_calls: tuple[SqlMacroCall, ...],
        control_payload: MacroControlPayload | None,
        scope_id: int,
    ) -> None:
        copy_dataclass_fields(classified, self, ClassifiedBlock)

        object.__setattr__(self, "resolved_options", resolved_options)
        object.__setattr__(self, "resolved_body", resolved_body)
        object.__setattr__(self, "sql_macro_calls", sql_macro_calls)
        object.__setattr__(self, "control_payload", control_payload)
        object.__setattr__(self, "scope_id", scope_id)


@dataclass(frozen=True, slots=True)
class ResolvedProgram:
    blocks: tuple[ResolvedBlock, ...]
    scope_tree: ScopeNode
    diagnostics: tuple[Diagnostic, ...]
