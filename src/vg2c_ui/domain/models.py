from __future__ import annotations

from typing import Any, Literal

from pydantic import BaseModel, Field

SCHEMA_VERSION = 1


class SourceSpan(BaseModel):
    file: str | None = None
    start_line: int
    end_line: int


class ParameterDescriptor(BaseModel):
    id: str
    name: str
    position: int | None = None
    source: str
    value: Any = None
    editor_type: Literal["string", "multiline", "integer", "boolean", "list", "dynamic"]
    editable: bool
    read_only_reason: str | None = None
    constraints: dict[str, Any] = Field(default_factory=dict)
    annotation: str | None = None
    required: bool = True
    default: Any = None


class UtilityDescriptor(BaseModel):
    name: str
    class_name: str
    module: str
    title: str
    description: str
    method: str | None = None
    method_description: str | None = None
    return_type: str | None = None
    fallback: bool = False


class StepNode(BaseModel):
    id: str
    node_kind: Literal["step"] = "step"
    function_name: str
    block_index: int
    source_span: SourceSpan
    functional_kind: str
    display_label: str
    icon_key: str
    description: str
    parameters: list[ParameterDescriptor] = Field(default_factory=list)
    csv_inputs: list[str] = Field(default_factory=list)
    csv_outputs: list[str] = Field(default_factory=list)
    parent_scope_id: str | None = None
    branch: Literal["true", "false"] | None = None
    validation_state: Literal["valid", "warning", "unsupported"] = "valid"
    raw_code: str | None = None
    read_only: bool = True
    utility: UtilityDescriptor


class ScopeNode(BaseModel):
    id: str
    node_kind: Literal["if", "branch", "loop"]
    scope_kind: str
    label: str
    start_index: int
    end_index: int
    parent_scope_id: str | None = None


class CsvArtifact(BaseModel):
    """File-level CSV metadata used by the contextual data-flow inspector."""

    id: str
    path: str
    label: str
    conditional: bool = False
    in_loop: bool = False
    producer_step_ids: list[str] = Field(default_factory=list)
    consumer_step_ids: list[str] = Field(default_factory=list)
    order_valid: bool = True


class Diagnostic(BaseModel):
    level: Literal["info", "warning", "error"]
    code: str
    message: str
    location: str | None = None
    node_id: str | None = None


class WorkflowOverride(BaseModel):
    step_id: str
    parameter_id: str
    value: Any


class WorkflowDocument(BaseModel):
    schema_version: int = SCHEMA_VERSION
    id: str
    source_path: str
    output_path: str
    source_hash: str
    output_hash: str
    revision: int = 1
    steps: list[StepNode]
    scopes: list[ScopeNode]
    artifacts: list[CsvArtifact]
    diagnostics: list[Diagnostic]
    overrides: list[WorkflowOverride] = Field(default_factory=list)


class WorkflowSidecar(BaseModel):
    """Persistent editor state that is independent of any presentation layout."""

    schema_version: int = SCHEMA_VERSION
    source_hash: str
    output_hash: str
    revision: int = 1
    overrides: list[WorkflowOverride] = Field(default_factory=list)


class SetParameterCommand(BaseModel):
    operation: Literal["set-parameter"] = "set-parameter"
    step_id: str
    parameter_id: str
    value: Any


class CommandBatch(BaseModel):
    source_path: str
    output_path: str
    source_hash: str
    output_hash: str
    revision: int
    commands: list[SetParameterCommand] = Field(min_length=1)


class ValidationIssue(BaseModel):
    level: Literal["warning", "error"] = "error"
    code: str
    message: str
    step_id: str | None = None
    parameter_id: str | None = None


class CommandPreview(BaseModel):
    valid: bool
    diff: str
    issues: list[ValidationIssue] = Field(default_factory=list)


class CommandResult(BaseModel):
    document: WorkflowDocument
    diff: str


class CsvPreview(BaseModel):
    path: str
    columns: list[str]
    rows: list[list[str]]
    truncated: bool
    size_bytes: int


__all__ = [
    "CommandBatch",
    "CommandPreview",
    "CommandResult",
    "CsvArtifact",
    "CsvPreview",
    "Diagnostic",
    "ParameterDescriptor",
    "SCHEMA_VERSION",
    "ScopeNode",
    "SourceSpan",
    "StepNode",
    "SetParameterCommand",
    "UtilityDescriptor",
    "ValidationIssue",
    "WorkflowDocument",
    "WorkflowOverride",
    "WorkflowSidecar",
]
