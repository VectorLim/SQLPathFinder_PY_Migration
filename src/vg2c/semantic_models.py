"""Semantic data contracts shared by projection, editing and workflow layers."""

from __future__ import annotations

from dataclasses import dataclass
from typing import TYPE_CHECKING, Any, Literal

from vg2c.emitter.models import SourceRange
from vg2c.utility_metadata import ValueSchema

if TYPE_CHECKING:
    from vg2c.frontend.models import SourceSpan

BindingVisibility = Literal["normal", "advanced", "internal"]
ValidationState = Literal["valid", "warning", "unresolved", "unsupported"]
SymbolKind = Literal["global", "macro", "macro-row", "unresolved"]
SymbolValueState = Literal["known", "runtime", "unknown"]


@dataclass(frozen=True, slots=True)
class OperationReference:
    operation_id: str
    binding_id: str | None = None


@dataclass(frozen=True, slots=True)
class EditableBinding:
    id: str
    owner_operation_id: str
    name: str
    display_label: str
    schema: ValueSchema | None
    value: Any
    symbol_id: str | None = None
    default_symbol_id: str | None = None
    default: Any = None
    required: bool = True
    visibility: BindingVisibility = "normal"
    capabilities: tuple[str, ...] = ()
    validation_state: ValidationState = "valid"
    resettable: bool = True
    editable: bool = True
    read_only_reason: str | None = None
    source_range: SourceRange | None = None
    source_kind: str = "parameter"


@dataclass(frozen=True, slots=True)
class WorkflowOperation:
    id: str
    kind: str
    display_name: str
    parent_operation_id: str | None
    branch: Literal["true", "false"] | None
    source_span: SourceSpan
    summary: str = ""
    bindings: tuple[EditableBinding, ...] = ()
    capabilities: tuple[str, ...] = ()
    comments: tuple[str, ...] = ()
    validation_state: ValidationState = "valid"
    visibility: BindingVisibility = "normal"
    block_index: int = -1
    scope_id: int | None = None
    reorder_targets: tuple[int, ...] = ()
    source_range: SourceRange | None = None


@dataclass(frozen=True, slots=True)
class SymbolReference:
    operation_id: str
    binding_id: str
    context: Literal["condition", "parameter", "global-value"] = "parameter"


@dataclass(frozen=True, slots=True)
class Symbol:
    id: str
    display_name: str
    kind: SymbolKind
    value_state: SymbolValueState
    value: Any = None
    value_binding_id: str | None = None
    introduction: OperationReference | None = None
    references: tuple[SymbolReference, ...] = ()
