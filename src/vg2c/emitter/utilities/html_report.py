"""HtmlReport - utility for generating HTML files."""

from __future__ import annotations

from pathlib import Path
import re
from typing import Any

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._emit_helpers import (
    RawExpr,
    _emit_step_source,
    _step_name,
    option_to_python_expr,
)
from vg2c.frontend.models import Kind


class HtmlReport(UtilitySpec):
    """Utility for generating HTML report files."""

    utility_name = "html_report"
    handles = (Kind.HTML_REPORT,)

    def __init__(self) -> None:
        self.styles: dict[str, str] = {}
        self.instance: str | None = None
        self.prompt_text: str | None = None
        self.app_server_default: str | None = None

    @classmethod
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        report_type = block.resolved_options.lookup.get("REPORT", "").upper().strip()
        if report_type == "HTML-RUN":
            return cls._emit_html_run(ctx, block)
        elif report_type == "HTML-LAYOUT":
            return cls._emit_html_layout(ctx, block)
        elif report_type == "HTML-DELETE":
            return cls._emit_html_delete(ctx, block)
        return None

    @classmethod
    def _emit_html_run(cls, ctx, block) -> tuple[str, str]:
        kwargs = {}
        for key in ["INSTANCE", "PROMPT-TEXT", "APP_SERVER_DEFAULT"]:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = RawExpr(option_to_python_expr(val))
        kwargs["template"] = block.resolved_body
        stmt = ctx.render_method_call("html_report", "run", kwargs=kwargs)
        return _emit_step_source(_step_name(block, "html_report"), [stmt])

    @classmethod
    def _emit_html_layout(cls, ctx, block) -> tuple[str, str]:
        kwargs = {}
        for key in ["OUTLOOK", "INSTANCE", "JSON-ONLY", "CHART-INSTANCE", "APP_SERVER_DEFAULT"]:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = RawExpr(option_to_python_expr(val))
        kwargs["template"] = block.resolved_body
        stmt = ctx.render_method_call(
            "html_report",
            "layout",
            args=(RawExpr("ctx"),),
            kwargs=kwargs,
        )
        return _emit_step_source(_step_name(block, "html_report"), [stmt])

    @classmethod
    def _emit_html_delete(cls, ctx, block) -> tuple[str, str]:
        kwargs = {}
        for key in ["INSTANCE"]:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = RawExpr(option_to_python_expr(val))
        stmt = ctx.render_method_call("html_report", "delete", kwargs=kwargs)
        return _emit_step_source(_step_name(block, "html_report"), [stmt])

    def run(
        self,
        instance: str | None = None,
        prompt_text: str | None = None,
        app_server_default: str | None = None,
        template: str | None = None,
    ) -> None:
        self.instance = instance
        self.prompt_text = prompt_text
        self.app_server_default = app_server_default

        if template:
            for line in template.splitlines():
                if not line.strip():
                    continue
                parts = line.split("<\\>")
                if len(parts) >= 3 and parts[0].upper() == "FORMAT":
                    self.styles[parts[1]] = parts[2]

    def layout(
        self,
        ctx: Any,
        template: str,
        outlook: str | None = None,
        instance: str | None = None,
        json_only: str | None = None,
        chart_instance: str | None = None,
        app_server_default: str | None = None,
    ) -> None:
        path = "report.html"
        css_file = None
        css_embed = False
        html_lines = []

        for line in template.splitlines():
            if line.startswith(":"):
                parts = line[1:].split(":", 1)
                if len(parts) == 2:
                    key = parts[0].strip().upper()
                    val = parts[1].strip()
                    if key == "FILE":
                        path = val
                    elif key == "CSS":
                        css_file = val
                    elif key == "CSSEMBED":
                        css_embed = val.upper() in ("Y", "YES", "TRUE")
            else:
                html_lines.append(line)

        html_content = "\n".join(html_lines)

        if css_file:
            css_path = Path(css_file)
            if css_embed and css_path.exists():
                css_content = css_path.read_text(encoding="utf-8", errors="replace")
                style_tag = f"<style>\n{css_content}\n</style>"
                if "</head>" in html_content:
                    html_content = html_content.replace(
                        "</head>", f"{style_tag}\n</head>", 1
                    )
                else:
                    html_content = f"{style_tag}\n{html_content}"

        if ctx and hasattr(ctx, "macro"):
            resolved_path = ctx.macro.substitute_sql(path)
            ctx.macro.write_file(resolved_path, html_content)
        else:
            out_path = Path(path)
            out_path.parent.mkdir(parents=True, exist_ok=True)
            out_path.write_text(html_content, encoding="utf-8")

    def delete(self, instance: str | None = None) -> None:
        pass
