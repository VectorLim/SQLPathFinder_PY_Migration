"""Cross-document file producers, links, and workspace diagnostics."""
from __future__ import annotations

from collections.abc import Iterable

from vg2c.dataflow.file_effects import FileEffect, endpoint_keys
from vg2c.workflow_models import WorkflowDocument, WorkflowIssue, WorkflowLink


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
                f"Previously linked input {link.artifact} no longer has an available "
                "workspace producer.",
                link.producer_document_id,
                link.producer_step_id,
            )
        )
    return tuple(dict.fromkeys(issues))


