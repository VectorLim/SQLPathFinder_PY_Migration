from __future__ import annotations

from collections.abc import Iterable
from dataclasses import replace
from pathlib import Path

from vg2c.compilation import CompilationResult
from vg2c.dataflow.file_effects import FileEffect, FileEndpoint, bind_file_effects, order_file_effects
from vg2c.editing import ChangeValidationError, SemanticChange, project_changes
from vg2c.project_paths import project_main_path
from vg2c.semantics import _build_semantics
from vg2c.utilities._emit_helpers import scan_sql_get_csv_list_calls
from vg2c.workflow_models import (
    DocumentDiagnostic,
    EffectiveDocument,
    FileResource,
    WorkflowDocument,
    WorkflowIssue,
    WorkflowLink,
)
from vg2c._workflow_effects import (
    _chunk_effects,
    _document_diagnostics,
    _html_effects,
    _macro_effects,
    _row_count_effects,
    file_resources,
)
from vg2c._workflow_reorder import (
    _block_scope_id,
    _legal_reorder_targets,
    _reorder_safe_outputs,
    _scope_flags,
)
from vg2c._workflow_workspace import workspace_issues, workspace_links

def project_document(
    result: CompilationResult,
    changes: Iterable[SemanticChange] = (),
    *,
    output_path: Path | None = None,
) -> EffectiveDocument:
    """Build the one effective semantic document for downstream consumers."""
    requested = tuple(changes)
    if requested:
        projection = project_changes(result, requested)
        if not projection.valid:
            raise ChangeValidationError(projection.issues)
        source = projection.source
        values = projection.effective_values
    else:
        source = result.emitted.source
        values = {}

    operations, _, symbols = _build_semantics(result, values)
    flags = _scope_flags(result)
    invocations = {
        invocation.id: (step, invocation)
        for step in result.emitted.steps
        for invocation in step.invocations
    }
    steps_by_block = {step.block_index: step for step in result.emitted.steps}
    effects: list[FileEffect] = []

    for operation in operations:
        conditional, in_loop = flags.get(operation.block_index, (False, False))
        step = steps_by_block.get(operation.block_index)
        step_id = step.function_name if step else operation.id
        bound: list[FileEffect] = []

        emitted = invocations.get(operation.id)
        if emitted is not None:
            emitted_step, invocation = emitted
            bound.extend(
                bind_file_effects(
                    invocation,
                    values,
                    step_id=emitted_step.function_name,
                    block_index=operation.block_index,
                    scope_id=_block_scope_id(result, operation.block_index),
                    order=0,
                    conditional=conditional,
                    in_loop=in_loop,
                )
            )
            bound.extend(
                _html_effects(
                    operation,
                    step_id=emitted_step.function_name,
                    scope_id=_block_scope_id(result, operation.block_index),
                    conditional=conditional,
                    in_loop=in_loop,
                )
            )
        elif operation.kind == "macro-loop":
            bound.extend(
                _macro_effects(operation, step_id, _block_scope_id(result, operation.block_index), conditional, in_loop)
            )
        elif operation.kind == "chunk-loop":
            bound.extend(
                _chunk_effects(operation, step_id, _block_scope_id(result, operation.block_index), conditional, in_loop)
            )
        elif operation.kind == "check-row-count":
            bound.extend(
                _row_count_effects(operation, step_id, _block_scope_id(result, operation.block_index), conditional, in_loop)
            )
        elif operation.kind in {"embedded-python", "unsupported"}:
            bound.append(
                FileEffect(
                    f"{operation.id}:effect:unknown",
                    operation.id,
                    step_id,
                    operation.block_index,
                    _block_scope_id(result, operation.block_index),
                    0,
                    "unknown",
                    (),
                    (),
                    conditional,
                    in_loop,
                    (
                        "Embedded Python may perform arbitrary file operations."
                        if operation.kind == "embedded-python"
                        else "No declared file effects are available for this operation."
                    ),
                )
            )

        # SQL_Get_CSV_List is source syntax embedded inside SQL, not a runtime utility
        # invocation. Reuse the compiler's existing scanner and represent the read explicitly.
        block = next(
            (item for item in result.resolved.blocks if item.index == operation.block_index),
            None,
        )
        if block is not None and operation.kind == "ctx.run_query":
            for index, call in enumerate(scan_sql_get_csv_list_calls(block.resolved_body)):
                effect_id = f"{operation.id}:effect:sql-get-csv-list:{index}"
                binding_id = f"{operation.id}:sql-file-list:{index}"
                file_binding = next(
                    (item for item in operation.bindings if item.id == binding_id), None
                )
                bound.append(
                    FileEffect(
                        effect_id,
                        operation.id,
                        step_id,
                        operation.block_index,
                        block.scope_id,
                        len(bound),
                        "read",
                        (
                            FileEndpoint(
                                f"{effect_id}:prior:path:0",
                                binding_id if file_binding else None,
                                file_binding.value if file_binding else call.source_path,
                                None,
                                "runtime-search",
                                "prior",
                            ),
                        ),
                        (),
                        conditional,
                        in_loop,
                        "SQL_Get_CSV_List reads this file while resolving SQL input.",
                    )
                )

        start_order = len(effects)
        effects.extend(
            replace(effect, order=start_order + index)
            for index, effect in enumerate(bound)
        )

    ordered = order_file_effects(
        tuple(effects),
        result.resolved.scope_tree,
        output_path or project_main_path(result.input_path),
    )
    output = output_path or project_main_path(result.input_path)
    safe_outputs = _reorder_safe_outputs(result, operations, ordered, output)
    reorder_targets = _legal_reorder_targets(result, safe_outputs)
    operations = tuple(
        replace(
            operation,
            reorder_targets=reorder_targets.get(operation.scope_id, ()),
        )
        for operation in operations
    )
    return EffectiveDocument(
        source=source,
        operations=operations,
        symbols=symbols,
        effects=ordered,
        files=file_resources(ordered),
        diagnostics=_document_diagnostics(result, ordered),
    )

__all__ = [
    "FileResource",
    "WorkflowDocument",
    "WorkflowIssue",
    "WorkflowLink",
    "DocumentDiagnostic",
    "EffectiveDocument",
    "file_resources",
    "project_document",
    "workspace_issues",
    "workspace_links",
]
