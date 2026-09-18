from __future__ import annotations

from collections.abc import Iterable
from dataclasses import dataclass, replace
from pathlib import Path
from typing import Literal

from vg2c.compilation import CompilationResult
from vg2c.dataflow.file_effects import (
    FileEffect,
    FileEndpoint,
    bind_file_effects,
    endpoint_keys,
    order_file_effects,
)
from vg2c.editing import (
    ChangeProjection,
    ChangeValidationError,
    ParameterChange,
    project_changes,
)


@dataclass(frozen=True, slots=True)
class WorkflowProjection:
    changes: ChangeProjection
    effects: tuple[FileEffect, ...]


@dataclass(frozen=True, slots=True)
class WorkflowDocument:
    document_id: str
    output_path: Path
    workflow: WorkflowProjection


@dataclass(frozen=True, slots=True)
class WorkflowLink:
    artifact: str
    producer_document_id: str
    producer_operation_id: str
    producer_step_id: str
    consumer_document_id: str
    consumer_operation_id: str
    consumer_step_id: str
    consumer_endpoint_id: str


@dataclass(frozen=True, slots=True)
class WorkflowIssue:
    code: Literal["BROKEN_DEPENDENCY", "DUPLICATE_OUTPUT"]
    document_id: str
    step_id: str
    artifact: str
    message: str
    related_document_id: str | None = None
    related_step_id: str | None = None


def _live_producers(
    documents: Iterable[WorkflowDocument],
) -> dict[str, list[tuple[str, FileEffect]]]:
    producers: dict[str, list[tuple[str, FileEffect]]] = {}
    for document in documents:
        live: dict[str, list[FileEffect]] = {}
        for effect in document.workflow.effects:
            if (
                effect.kind in {"delete", "move"}
                and not effect.conditional
                and not effect.in_loop
            ):
                for endpoint in effect.inputs:
                    for key in endpoint_keys(endpoint, document.output_path):
                        live.pop(key, None)
            for endpoint in effect.outputs:
                for key in endpoint_keys(endpoint, document.output_path):
                    live[key] = (
                        live.get(key, [])
                        if effect.conditional or effect.in_loop
                        else []
                    ) + [effect]
        for key, effects in live.items():
            producers.setdefault(key, []).extend(
                (document.document_id, effect) for effect in effects
            )
    return producers


def workspace_links(documents: Iterable[WorkflowDocument]) -> tuple[WorkflowLink, ...]:
    documents = tuple(documents)
    producers = _live_producers(documents)
    links: list[WorkflowLink] = []
    for document in documents:
        for effect in document.workflow.effects:
            for endpoint in effect.inputs:
                if (
                    endpoint.status not in {"external", "possible"}
                    or endpoint.state_ids
                ):
                    continue
                for key in endpoint_keys(endpoint, document.output_path):
                    for producer_document, producer in producers.get(key, ()):
                        if producer_document == document.document_id:
                            continue
                        links.append(
                            WorkflowLink(
                                endpoint.path or "",
                                producer_document,
                                producer.operation_id,
                                producer.step_id,
                                document.document_id,
                                effect.operation_id,
                                effect.step_id,
                                endpoint.id,
                            )
                        )
    return tuple(dict.fromkeys(links))


def workspace_issues(
    documents: Iterable[WorkflowDocument], baseline: Iterable[WorkflowDocument]
) -> tuple[WorkflowIssue, ...]:
    documents = tuple(documents)
    issues: list[WorkflowIssue] = []
    for producers in _live_producers(documents).values():
        if len({document_id for document_id, _ in producers}) < 2:
            continue
        for document_id, effect in producers:
            related_document, related_effect = next(
                item for item in producers if item[0] != document_id
            )
            path = next(
                (endpoint.path for endpoint in effect.outputs if endpoint.path),
                "Unknown path",
            )
            issues.append(
                WorkflowIssue(
                    "DUPLICATE_OUTPUT",
                    document_id,
                    effect.step_id,
                    path,
                    f"{path} may be written by more than one open document.",
                    related_document,
                    related_effect.step_id,
                )
            )
    linked_consumers = {
        (link.consumer_document_id, link.consumer_endpoint_id)
        for link in workspace_links(documents)
    }
    current_inputs = {
        (document.document_id, endpoint.id)
        for document in documents
        for effect in document.workflow.effects
        for endpoint in effect.inputs
    }
    for link in workspace_links(baseline):
        identity = (link.consumer_document_id, link.consumer_endpoint_id)
        if identity not in current_inputs or identity in linked_consumers:
            continue
        issues.append(
            WorkflowIssue(
                "BROKEN_DEPENDENCY",
                link.consumer_document_id,
                link.consumer_step_id,
                link.artifact,
                f"Previously linked input {link.artifact} no longer has an available workspace producer.",
                link.producer_document_id,
                link.producer_step_id,
            )
        )
    return tuple(dict.fromkeys(issues))


def project_workflow(
    result: CompilationResult,
    changes: Iterable[ParameterChange] = (),
    *,
    output_path: Path | None = None,
) -> WorkflowProjection:
    projection = project_changes(result, changes)
    if not projection.valid:
        raise ChangeValidationError(projection.issues)
    values = {change.parameter_id: change.value for change in projection.values}
    flags: dict[int, tuple[bool, bool]] = {}

    def visit(node, conditional=False, in_loop=False):
        conditional = conditional or node.kind in {"if-branch", "else-branch"}
        in_loop = in_loop or node.kind in {"macro", "loop"}
        flags[node.scope_id] = conditional, in_loop
        for child in node.children:
            visit(child, conditional, in_loop)

    visit(result.resolved.scope_tree)
    steps = {step.block_index: step for step in result.emitted.steps}
    effects: list[FileEffect] = []
    for block in sorted(result.resolved.blocks, key=lambda item: item.index):
        step = steps.get(block.index)
        step_id = step.function_name if step else f"block-{block.index}"
        operation_id = (
            step.invocations[0].id
            if step and step.invocations
            else f"block-{block.index}:source"
        )
        conditional, in_loop = flags.get(block.scope_id, (False, False))
        bound: list[FileEffect] = []
        for invocation in step.invocations if step else ():
            bound.extend(
                bind_file_effects(
                    invocation,
                    values,
                    step_id=step_id,
                    block_index=block.index,
                    scope_id=block.scope_id,
                    order=0,
                    conditional=conditional,
                    in_loop=in_loop,
                )
            )
        has_declared_inputs = (
            any(
                invocation.operation.file_effects is not None
                and any(effect.inputs for effect in invocation.operation.file_effects)
                for invocation in step.invocations
            )
            if step
            else False
        )
        source_inputs = [
            consumer
            for consumer in result.analyzed.consumers
            if consumer.block_index == block.index
            and (not has_declared_inputs or consumer.consumer_kind == "sql-macro")
        ]
        if source_inputs:
            effect_id = f"{operation_id}:effect:source-inputs"
            effects.append(
                FileEffect(
                    effect_id,
                    operation_id,
                    step_id,
                    block.index,
                    block.scope_id,
                    len(effects),
                    "read",
                    tuple(
                        FileEndpoint(
                            f"{effect_id}:{index}",
                            None,
                            item.csv_path,
                            None,
                            "runtime-search",
                            "prior",
                        )
                        for index, item in enumerate(source_inputs)
                    ),
                    (),
                    conditional,
                    in_loop,
                    "Source/control dependency recorded by the compiler.",
                )
            )
        if not step:
            outputs = [
                producer
                for producer in result.analyzed.producers
                if producer.block_index == block.index
            ]
            if outputs:
                effect_id = f"{operation_id}:effect:source-outputs"
                bound.append(
                    FileEffect(
                        effect_id,
                        operation_id,
                        step_id,
                        block.index,
                        block.scope_id,
                        0,
                        "write",
                        (),
                        tuple(
                            FileEndpoint(
                                f"{effect_id}:{index}",
                                None,
                                item.csv_path,
                                None,
                                "script-directory",
                                "next",
                            )
                            for index, item in enumerate(outputs)
                        ),
                        conditional,
                        in_loop,
                        "Source/control output recorded by the compiler.",
                    )
                )
        elif not step.invocations:
            bound.append(
                FileEffect(
                    f"{operation_id}:effect:unknown",
                    operation_id,
                    step_id,
                    block.index,
                    block.scope_id,
                    0,
                    "unknown",
                    (),
                    (),
                    conditional,
                    in_loop,
                    "No declared file effects are available for this block.",
                )
            )
        start_order = len(effects)
        effects.extend(
            replace(effect, order=start_order + index)
            for index, effect in enumerate(bound)
        )
    return WorkflowProjection(
        projection,
        order_file_effects(
            tuple(effects),
            result.resolved.scope_tree,
            output_path or result.input_path.with_suffix(".py"),
        ),
    )
