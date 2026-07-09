from __future__ import annotations

from dataclasses import dataclass
from typing import Literal, Mapping

from vg2c.frontend.models import (
    BlockOptions,
    ClassifiedBlock,
    Diagnostic,
    ParsedBlock,
    SourceSpan,
    copy_dataclass_fields,
)
from vg2c.kind import Kind


@dataclass(frozen=True, slots=True)
class MacroFrame:
    frame_id: int
    kind: Literal["row-iter", "if", "static-vars"]
    csv_path: str | None
    csv_headers: tuple[str, ...] | None
    named_vars: Mapping[str, str]
    positional_cursor: int
    source_span: SourceSpan


@dataclass(frozen=True, slots=True)
class StartMacro:
    csv_path: str
    prompt_off: bool


@dataclass(frozen=True, slots=True)
class EndMacro:
    pass


@dataclass(frozen=True, slots=True)
class IfThen:
    lhs: str
    op: str
    rhs: str
    conj: str | None
    lhs2: str | None
    op2: str | None
    rhs2: str | None


@dataclass(frozen=True, slots=True)
class Else:
    pass


@dataclass(frozen=True, slots=True)
class EndIf:
    pass


@dataclass(frozen=True, slots=True)
class RowsInFile:
    csv_path: str
    var_name: str
    prompt_off: bool


@dataclass(frozen=True, slots=True)
class RunLoop:
    input_csv_path: str
    chunk_csv_path: str
    chunk_size: int
    prompt_off: bool


@dataclass(frozen=True, slots=True)
class EndLoop:
    pass


MacroControlPayload = (
    StartMacro | EndMacro | IfThen | Else | EndIf | RowsInFile | RunLoop | EndLoop
)


@dataclass(frozen=True, slots=True)
class ScopeNode:
    scope_id: int
    kind: Literal["program", "macro", "loop", "if", "if-branch", "else-branch", "leaf"]
    start_index: int
    end_index: int
    children: tuple["ScopeNode", ...]
    block_index: int | None
    control_payload: MacroControlPayload | None


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
