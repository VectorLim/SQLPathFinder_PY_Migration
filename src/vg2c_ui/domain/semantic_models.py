from __future__ import annotations

from typing import Any, Literal

from pydantic import BaseModel, ConfigDict, Field, model_serializer
from pydantic.json_schema import SkipJsonSchema

from vg2c_ui.domain.models import WorkflowDocument

SEMANTIC_SCHEMA_VERSION = 1

SqlEntityKind = Literal['selection', 'filter', 'join', 'join_predicate', 'source']
WorkflowEntityKind = Literal['document', 'step', 'parameter', 'artifact']


class _Unset:
    __slots__ = ()


_UNSET = _Unset()
OmissibleString = str | SkipJsonSchema[_Unset]


class _PatchModel(BaseModel):
    """Serialize only explicitly supplied patch fields."""

    model_config = ConfigDict(arbitrary_types_allowed=True)

    @model_serializer(mode='plain')
    def _serialize_patch(self) -> dict[str, Any]:
        return {name: getattr(self, name) for name in self.model_fields_set}


class WorkflowEntityRef(BaseModel):
    """Revision-aware reference to an existing workflow entity."""

    schema_version: Literal[1] = SEMANTIC_SCHEMA_VERSION
    document_id: str
    entity_kind: WorkflowEntityKind
    entity_id: str
    step_id: str | None = None
    document_revision: int = Field(ge=1)
    output_hash: str


class SqlEntityRef(BaseModel):
    """Resolvable reference to a parser-local structured SQL child entity."""

    schema_version: Literal[1] = SEMANTIC_SCHEMA_VERSION
    document_id: str
    step_id: str
    sql_parameter_id: str
    entity_kind: SqlEntityKind
    parsed_id: str
    fingerprint: str
    ordinal_hint: int = Field(ge=0)
    document_revision: int = Field(ge=1)
    output_hash: str


class OpenDocumentState(BaseModel):
    document_id: str
    source_hash: str
    output_hash: str
    revision: int = Field(ge=1)


class PendingEdit(BaseModel):
    document_id: str
    step_id: str
    parameter_id: str
    value: Any


class ClientWorkingState(BaseModel):
    schema_version: Literal[1] = SEMANTIC_SCHEMA_VERSION
    active_document_id: str | None = None
    selected_item_id: str | None = None
    open_documents: list[OpenDocumentState] = Field(default_factory=list)
    pending_edits: list[PendingEdit] = Field(default_factory=list)


class EffectiveDocumentInput(BaseModel):
    """Validated, non-persistent inputs for constructing an effective document view."""

    base_document: WorkflowDocument
    pending_edits: list[PendingEdit] = Field(default_factory=list)


class SqlSpan(BaseModel):
    start: int = Field(ge=0)
    end: int = Field(ge=0)


class SqlSelection(BaseModel):
    id: str
    expression: str
    alias: str | None
    raw: str
    editable: bool
    read_only_reason: str | None = None
    span: SqlSpan


class SqlSource(BaseModel):
    id: str
    expression: str
    kind: Literal['from', 'join']
    editable: bool
    read_only_reason: str | None = None
    span: SqlSpan
    join_id: str | None = None


SqlLogicalConnector = Literal['AND', 'OR']


class SqlPredicate(BaseModel):
    id: str
    left: str
    operator: str
    right: str
    connector: SqlLogicalConnector | None = None
    raw: str
    editable: bool
    read_only_reason: str | None = None
    span: SqlSpan
    connector_span: SqlSpan | None = None


class SqlJoin(BaseModel):
    id: str
    join_type: str
    source: str
    predicates: list[SqlPredicate] = Field(default_factory=list)
    editable_type: bool
    editable_source: bool
    read_only_reason: str | None = None
    span: SqlSpan
    type_span: SqlSpan
    source_span: SqlSpan


class SqlEditCapabilities(BaseModel):
    selected: bool
    filters: bool
    joins: bool
    raw_sql: Literal[True] = True


class SqlEditableModel(BaseModel):
    source: str
    statement_span: SqlSpan
    selections: list[SqlSelection] = Field(default_factory=list)
    filters: list[SqlPredicate] = Field(default_factory=list)
    joins: list[SqlJoin] = Field(default_factory=list)
    sources: list[SqlSource] = Field(default_factory=list)
    capabilities: SqlEditCapabilities
    read_only_reason: str | None = None
    select_list_span: SqlSpan | None = None
    where_clause_span: SqlSpan | None = None
    where_body_span: SqlSpan | None = None
    from_clause_span: SqlSpan | None = None


class SqlTransformResult(BaseModel):
    sql: str
    model: SqlEditableModel


class SelectionPatch(_PatchModel):
    expression: OmissibleString = Field(default_factory=lambda: _UNSET)
    alias: str | None = None


class PredicatePatch(_PatchModel):
    left: OmissibleString = Field(default_factory=lambda: _UNSET)
    operator: OmissibleString = Field(default_factory=lambda: _UNSET)
    right: OmissibleString = Field(default_factory=lambda: _UNSET)
    connector: SqlLogicalConnector | None = None


class JoinPatch(_PatchModel):
    join_type: OmissibleString = Field(default_factory=lambda: _UNSET)
    source: OmissibleString = Field(default_factory=lambda: _UNSET)


class SqlEntityResolution(BaseModel):
    status: Literal['resolved', 'not_found', 'ambiguous', 'stale']
    ref: SqlEntityRef | None = None
    reason: str | None = None


class WorkspaceDocumentSummary(BaseModel):
    document_id: str
    source_path: str
    output_path: str
    source_hash: str
    output_hash: str
    revision: int = Field(ge=1)


class ArtifactEndpoint(BaseModel):
    document_id: str
    step_id: str
    path: str


class ProjectArtifact(BaseModel):
    key: str
    path: str
    producers: list[ArtifactEndpoint] = Field(default_factory=list)
    consumers: list[ArtifactEndpoint] = Field(default_factory=list)
    conditional: bool = False
    in_loop: bool = False


class DependencyDiagnostic(BaseModel):
    severity: Literal['warning', 'error']
    code: Literal['BROKEN_DEPENDENCY', 'MISSING_INPUT', 'DUPLICATE_OUTPUT']
    message: str
    artifact: str
    document_id: str
    operation_id: str
    related_operation_id: str | None = None


class ProjectGraphSnapshot(BaseModel):
    artifacts: list[ProjectArtifact] = Field(default_factory=list)
    diagnostics: list[DependencyDiagnostic] = Field(default_factory=list)


class SqlMetadataContext(BaseModel):
    sql: str
    sources: list[str] = Field(default_factory=list)


class SqlAttributeOption(BaseModel):
    expression: str
    label: str
    source: str | None = None
    sources: list[str] | None = None
    data_type: str | None = None
    description: str | None = None


class SqlSourceOption(BaseModel):
    source: str
    label: str
    description: str | None = None


class SqlJoinCandidate(BaseModel):
    source: str
    label: str
    join_types: list[str] | None = None


class SqlJoinKeyOption(BaseModel):
    left: str
    right: str
    operator: str | None = None


class SqlFilterValueOption(BaseModel):
    value: str
    label: str | None = None


class SqlSchemaInfo(BaseModel):
    source: str
    attributes: list[SqlAttributeOption] = Field(default_factory=list)


class SqlMetadataCapabilities(BaseModel):
    model_config = ConfigDict(populate_by_name=True, serialize_by_alias=True)

    attributes: bool = False
    sources: bool = False
    join_targets: bool = False
    join_keys: bool = False
    filter_values: bool = False
    schema_available: bool = Field(False, alias='schema')
