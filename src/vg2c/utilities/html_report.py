"""VG2 HTML report classification, parsing and emitted-operation metadata.

Generated jobs render only through vg2c.runtime.html. This compiler helper does
not retain the historical stateful HTML preview/rendering engine.
"""
from __future__ import annotations

from typing import Any

from vg2c.emitter.models import emittable
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility


class HtmlReport(EmitterUtility):
    """Compiler-only HTML report syntax and source-metadata definitions."""

    utility_name = "html_report"
    handles = (Kind.HTML_REPORT,)

    # SPF template row delimiter is the literal 4-character sequence "<\\>"
    # (angle, backslash, backslash, angle). In a Python source string this must
    # be written with four backslashes so the runtime value is two backslashes.
    _ROW_DELIM = "<\\\\>"
    _TRUE_VALUES = ("Y", "YES", "TRUE")

    _HTML_SCAFFOLD = """\
<html>
<head>
<title>{title}</title>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
{css_decl}
<!--@SPF-JS-HEADER@-->
<style type="text/css">
table.tblout, td.tblout, tr.tblout {{
    border-width:0px;
    border-collapse:collapse;
    border-style:none;
    text-align:left;
    vertical-align:top;
}}
td.tblout {{ padding:10px; }}
img {{ vertical-align:top; }}
a {{ text-decoration:none; color:#464feb; }}
tr th, tr td {{ border:1px solid #e6e6e6; }}
tr th {{ background-color:#f5f5f5; }}
</style>
</head>
<body>
{body}
</body>
</html>
"""



    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        report = options.lookup.get("REPORT")
        if report and report.upper().startswith("HTML-"):
            return Kind.HTML_REPORT, "/REPORT starts with HTML-"
        return None

    @classmethod
    def emit_block(cls, block, *, global_refs=None):
        # emitter.project is the sole direct-project HTML emitter.
        return None

    @classmethod
    def _iter_rows(cls, template: str | None):
        """Yield non-empty rows split on the SPF delimiter."""
        for line in (template or "").splitlines():
            if not line.strip():
                continue
            parts = [p.strip() for p in line.split(cls._ROW_DELIM)]
            if len(parts) >= 2:
                yield parts

    @classmethod
    def _parse_options(cls, template: str | None) -> dict[str, Any]:
        """Parse an SPF options template into {KEY: value|list-of-values}."""
        options: dict[str, Any] = {}
        for parts in cls._iter_rows(template):
            key = parts[0].upper()
            # 2nd column is an optional sub-key. If blank, values start at [2].
            vals = [p for p in (parts[2:] if parts[1] == "" else parts[1:]) if p]
            if not vals:
                options[key] = ""
            elif len(vals) == 1:
                options[key] = vals[0]
            else:
                options[key] = vals
        return options


    @emittable(display_name="Start HTML Report", file_effects=())
    def run(
        self, instance: str | None = None, prompt_text: str | None = None,
        app_server_default: str | None = None, template: str | None = None,
    ) -> None:
        raise NotImplementedError("Compiler metadata only; use JobRuntime.html")

    @emittable(display_name="Define HTML Report Section", file_effects=())
    def defer(
        self, id: str, instance: str | None = None,
        prompt_text: str | None = None, app_server_default: str | None = None,
        template: str | None = None,
    ) -> None:
        raise NotImplementedError("Compiler metadata only; use JobRuntime.report")

    @emittable(display_name="Clear HTML Report State", file_effects=())
    def delete(self, instance: str | None = None) -> None:
        raise NotImplementedError("Compiler metadata only; use JobRuntime.delete_html")

    @emittable(
        display_name="Generate HTML Report", capabilities=("html-preview",),
        parameter_visibility={"ctx": "internal"}, file_effects=(),
    )
    def layout(
        self, ctx: Any, template: str, outlook: str | None = None,
        instance: str | None = None, json_only: str | None = None,
        chart_instance: str | None = None,
        app_server_default: str | None = None,
    ) -> None:
        raise NotImplementedError("Compiler metadata only; use JobRuntime.html")
