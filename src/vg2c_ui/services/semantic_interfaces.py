from __future__ import annotations

from collections.abc import Sequence
from typing import Protocol

from vg2c_ui.domain.models import WorkflowDocument
from vg2c_ui.domain.semantic_models import (
    EffectiveDocumentInput,
    JoinPatch,
    PredicatePatch,
    ProjectGraphSnapshot,
    SelectionPatch,
    SqlAttributeOption,
    SqlEditableModel,
    SqlEntityRef,
    SqlEntityResolution,
    SqlFilterValueOption,
    SqlJoinCandidate,
    SqlJoinKeyOption,
    SqlMetadataCapabilities,
    SqlMetadataContext,
    SqlSchemaInfo,
    SqlSourceOption,
    SqlTransformResult,
    WorkspaceDocumentSummary,
)


class SqlModelService(Protocol):
    def parse(self, sql_text: str) -> SqlEditableModel: ...
    def add_selection(self, sql_text: str, expression: str) -> SqlTransformResult: ...

    def update_selection(
        self, sql_text: str, ref: SqlEntityRef, patch: SelectionPatch
    ) -> SqlTransformResult: ...

    def remove_selection(self, sql_text: str, ref: SqlEntityRef) -> SqlTransformResult: ...

    def reorder_selection(
        self, sql_text: str, ref: SqlEntityRef, target_index: int
    ) -> SqlTransformResult: ...

    def add_filter(self, sql_text: str, patch: PredicatePatch) -> SqlTransformResult: ...

    def update_filter(
        self, sql_text: str, ref: SqlEntityRef, patch: PredicatePatch
    ) -> SqlTransformResult: ...

    def remove_filter(self, sql_text: str, ref: SqlEntityRef) -> SqlTransformResult: ...

    def add_join(
        self, sql_text: str, patch: JoinPatch, predicate: PredicatePatch
    ) -> SqlTransformResult: ...

    def update_join(
        self, sql_text: str, ref: SqlEntityRef, patch: JoinPatch
    ) -> SqlTransformResult: ...

    def update_join_predicate(
        self, sql_text: str, ref: SqlEntityRef, patch: PredicatePatch
    ) -> SqlTransformResult: ...

    def remove_join(self, sql_text: str, ref: SqlEntityRef) -> SqlTransformResult: ...

    def update_source(
        self, sql_text: str, ref: SqlEntityRef, source: str
    ) -> SqlTransformResult: ...


class EffectiveDocumentService(Protocol):
    def build(self, inputs: EffectiveDocumentInput) -> WorkflowDocument: ...


class ProjectGraphService(Protocol):
    def build(self, documents: Sequence[EffectiveDocumentInput]) -> ProjectGraphSnapshot: ...


class WorkspaceCatalog(Protocol):
    def list_documents(self) -> Sequence[WorkspaceDocumentSummary]: ...
    def open_document(self, document_id: str) -> WorkflowDocument: ...


class MetadataService(Protocol):
    async def capabilities(self, context: SqlMetadataContext) -> SqlMetadataCapabilities: ...

    async def available_attributes(
        self, context: SqlMetadataContext
    ) -> Sequence[SqlAttributeOption]: ...

    async def available_sources(
        self, context: SqlMetadataContext
    ) -> Sequence[SqlSourceOption]: ...

    async def join_candidates(
        self, context: SqlMetadataContext
    ) -> Sequence[SqlJoinCandidate]: ...

    async def join_keys(
        self,
        context: SqlMetadataContext,
        left_source: str,
        right_source: str,
    ) -> Sequence[SqlJoinKeyOption]: ...

    async def filter_values(
        self, context: SqlMetadataContext, expression: str
    ) -> Sequence[SqlFilterValueOption]: ...

    async def schema(self, context: SqlMetadataContext, source: str) -> SqlSchemaInfo | None: ...


class SqlEntityResolver(Protocol):
    def resolve(self, model: SqlEditableModel, ref: SqlEntityRef) -> SqlEntityResolution: ...
