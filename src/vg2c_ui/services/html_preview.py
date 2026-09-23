from __future__ import annotations

import re
from collections.abc import Callable
from dataclasses import dataclass
from pathlib import Path
from types import SimpleNamespace
from typing import Literal

from vg2c.semantics import SemanticModel
from vg2c.utilities.html_report import HtmlReport


@dataclass(frozen=True, slots=True)
class HtmlPreviewResult:
    state: Literal["exact", "approximate", "error"]
    html: str = ""
    output_path: Path | None = None
    message: str | None = None


class _PreviewMacro:
    def __init__(self, resolver: Callable[[str | None], Path]) -> None:
        self._resolver = resolver
        self.approximate = False
        self.missing_inputs: set[str] = set()

    def resolve_file_path(self, value: str) -> Path:
        path = self._resolver(value)
        if not path.is_file():
            self.missing_inputs.add(value)
        return path

    def substitute(self, value: str) -> str:
        if "VAR(" in value or "{{" in value or "}}" in value:
            self.approximate = True
        return value


def preview_html_report(
    model: SemanticModel,
    *,
    target_operation_id: str,
    resolve_path: Callable[[str | None], Path],
) -> HtmlPreviewResult:
    """Safely replay HTML report state and render one layout in memory."""
    target = next(
        (item for item in model.operations if item.id == target_operation_id),
        None,
    )
    if target is None or target.kind != "html_report.layout":
        return HtmlPreviewResult(
            state="error",
            message="Select a Generate HTML Report operation to preview.",
        )

    report = HtmlReport()
    preview_macro = _PreviewMacro(resolve_path)
    context = SimpleNamespace(macro=preview_macro)

    for operation in model.operations:
        if not operation.kind.startswith("html_report."):
            continue

        bindings = {item.name: item.value for item in operation.bindings}
        if operation.kind == "html_report.run":
            report.run(
                instance=bindings.get("instance"),
                prompt_text=bindings.get("prompt_text"),
                app_server_default=bindings.get("app_server_default"),
                template=bindings.get("template"),
            )
            continue
        if operation.kind == "html_report.defer":
            report.defer(
                id=str(bindings.get("id") or ""),
                instance=bindings.get("instance"),
                prompt_text=bindings.get("prompt_text"),
                app_server_default=bindings.get("app_server_default"),
                template=bindings.get("template"),
            )
            continue
        if operation.kind == "html_report.delete":
            report.delete(instance=bindings.get("instance"))
            continue
        if operation.id != target_operation_id:
            continue

        template = bindings.get("template")
        if not isinstance(template, str):
            return HtmlPreviewResult(
                state="error",
                message="HTML layout template is unavailable.",
            )

        try:
            filename, html = report.render_layout(
                context,
                template,
                instance=bindings.get("instance"),
                css_resolver=resolve_path,
                write_css=False,
            )
            output_path = resolve_path(filename)
        except (OSError, ValueError) as exc:
            return HtmlPreviewResult(
                state="error",
                message=f"Missing preview input or blocked workspace resource: {exc}",
            )

        has_external_resources = bool(
            re.search(
                r"""(?:src|href)\s*=\s*["'](?!data:|#)""",
                html,
                flags=re.IGNORECASE,
            )
        )
        state: Literal["exact", "approximate", "error"] = (
            "approximate"
            if preview_macro.approximate or has_external_resources
            else "exact"
        )
        message = None
        if preview_macro.missing_inputs:
            state = "error"
            message = "Missing preview input: " + ", ".join(
                sorted(preview_macro.missing_inputs)
            )
        elif preview_macro.approximate:
            message = (
                "Preview contains runtime values that cannot be resolved before execution."
            )
        elif has_external_resources:
            message = "External report resources are blocked in preview."

        return HtmlPreviewResult(
            state=state,
            html=html,
            output_path=output_path,
            message=message,
        )

    return HtmlPreviewResult(
        state="error",
        message="HTML layout operation could not be replayed safely.",
    )


__all__ = ["HtmlPreviewResult", "preview_html_report"]
