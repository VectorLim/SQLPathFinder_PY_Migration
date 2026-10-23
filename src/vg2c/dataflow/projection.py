"""File-effect projection from operation declarations and source syntax."""
from __future__ import annotations

from dataclasses import replace
from typing import TYPE_CHECKING, Any

from vg2c.dataflow.file_effects import FileEffect, FileEndpoint, bind_effect_definitions
from vg2c.semantic.models import EditableBinding, WorkflowOperation
from vg2c.utilities._emit_helpers import scan_sql_get_csv_list_calls
from vg2c.utilities.html_report import HtmlReport

if TYPE_CHECKING:
    from vg2c.compilation import CompilationResult


def project_file_effects(
    result: CompilationResult,
    operations: tuple[WorkflowOperation, ...],
    values: dict[str, Any],
) -> tuple[FileEffect, ...]:
    """Bind declarations and enrich inputs derived from SQL and report syntax."""
    flags = _scope_flags(result)
    blocks = {block.index: block for block in result.resolved.blocks}
    steps = {step.block_index: step for step in result.emitted.steps}
    invocations = {
        invocation.id: invocation
        for step in result.emitted.steps
        for invocation in step.invocations
    }
    effects = []
    for operation in operations:
        block = blocks.get(operation.block_index)
        scope_id = block.scope_id if block is not None else 0
        conditional, in_loop = flags.get(scope_id, (False, False))
        step = steps.get(operation.block_index)
        step_id = step.function_name if step else operation.id
        invocation = invocations.get(operation.id)
        parameters = invocation.parameters if invocation else operation.bindings
        bound = list(bind_effect_definitions(
            operation.file_effects, parameters, values, operation_id=operation.id,
            step_id=step_id, block_index=operation.block_index, scope_id=scope_id,
            conditional=conditional, in_loop=in_loop,
        ))
        bound.extend(_html_effects(
            operation, step_id=step_id, scope_id=scope_id,
            conditional=conditional, in_loop=in_loop,
        ))
        bound.extend(_sql_input_effects(
            operation, block, step_id=step_id, scope_id=scope_id,
            conditional=conditional, in_loop=in_loop,
        ))
        start = len(effects)
        effects.extend(replace(effect, order=start + index) for index, effect in enumerate(bound))
    return tuple(effects)


def _sql_input_effects(operation, block, *, step_id, scope_id, conditional, in_loop):
    if block is None or operation.kind != "ctx.run_query":
        return ()
    effects = []
    for index, call in enumerate(scan_sql_get_csv_list_calls(block.resolved_body)):
        effect_id = f"{operation.id}:effect:sql-get-csv-list:{index}"
        binding_id = f"{operation.id}:sql-file-list:{index}"
        binding = next((item for item in operation.bindings if item.id == binding_id), None)
        effects.append(FileEffect(
            effect_id, operation.id, step_id, operation.block_index, scope_id, len(effects),
            "read", (FileEndpoint(
                f"{effect_id}:prior:path:0", binding_id if binding else None,
                binding.value if binding else call.source_path,
                None, "runtime-search", "prior",
            ),), (), conditional, in_loop,
            "SQL_Get_CSV_List reads this file while resolving SQL input.",
        ))
    return tuple(effects)


def _html_effects(
    operation: WorkflowOperation,
    *,
    step_id: str,
    scope_id: int,
    conditional: bool,
    in_loop: bool,
) -> tuple[FileEffect, ...]:
    template = _binding(operation, "template")
    if template is None or not isinstance(template.value, str):
        return ()

    effects: list[FileEffect] = []
    if operation.kind == "html_report.defer":
        options = HtmlReport.parse_options(template.value)
        raw_inputs = options.get("INPUT-FILE", ())
        inputs = raw_inputs if isinstance(raw_inputs, list) else [raw_inputs]
        paths = [item for item in inputs if isinstance(item, str) and item]
        if paths:
            effect_id = f"{operation.id}:effect:html-inputs"
            effects.append(
                FileEffect(
                    effect_id,
                    operation.id,
                    step_id,
                    operation.block_index,
                    scope_id,
                    0,
                    "read",
                    tuple(
                        FileEndpoint(
                            f"{effect_id}:prior:input:{index}",
                            template.id,
                            path,
                            None,
                            "runtime-search",
                            "prior",
                        )
                        for index, path in enumerate(paths)
                    ),
                    (),
                    conditional,
                    in_loop,
                    "Deferred report rows are read from these input files.",
                )
            )

    if operation.kind == "html_report.layout":
        directives, _ = HtmlReport.split_layout(template.value)
        output = directives.get("FILE")
        effect_id = f"{operation.id}:effect:html-output"
        if output:
            outputs = (
                FileEndpoint(
                    f"{effect_id}:next:file:0",
                    template.id,
                    output,
                    None,
                    "script-directory",
                    "next",
                ),
            )
        else:
            outputs = (
                FileEndpoint(
                    f"{effect_id}:next:file:0",
                    template.id,
                    None,
                    "HTML output resolved from deferred report state",
                    "script-directory",
                    "next",
                    status="dynamic",
                ),
            )
        effects.append(
            FileEffect(
                effect_id,
                operation.id,
                step_id,
                operation.block_index,
                scope_id,
                len(effects),
                "write",
                (),
                outputs,
                conditional,
                in_loop,
                "HTML layout writes the resolved report output.",
            )
        )
    return tuple(effects)


def _binding(operation: WorkflowOperation, name: str) -> EditableBinding | None:
    return next((item for item in operation.bindings if item.name == name), None)


def _scope_flags(result: CompilationResult) -> dict[int, tuple[bool, bool]]:
    flags: dict[int, tuple[bool, bool]] = {}

    def visit(node, conditional=False, in_loop=False):
        conditional = conditional or node.kind in {"if-branch", "else-branch"}
        in_loop = in_loop or node.kind in {"macro", "loop"}
        flags[node.scope_id] = conditional, in_loop
        for child in node.children:
            visit(child, conditional, in_loop)

    visit(result.resolved.scope_tree)
    return flags


