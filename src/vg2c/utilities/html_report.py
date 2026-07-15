"""HtmlReport - utility for generating HTML report files."""

from __future__ import annotations

import csv
import re
from pathlib import Path
from typing import Any

from vg2c.emitter.models import emittable
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import resolve_path
from vg2c.utilities.macro_state import MacroState


class HtmlReport(EmitterUtility):
    """Utility for generating HTML report files."""

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

    _CSS_RULES: list[dict[str, Any]] = [
        {
            "name": "COLUMN-BORDER",
            "template": "table.tblin, td.tblin, th, td.alt \n{{\n{decls}\n}}",
            "extras": [
                "td.tblin,th,td.alt\n{\n      padding:5px;\n}",
                "  table.tblin \n{\n     caption-side:top;\n}",
            ],
            "tail_template": "tr.at-bot-of-report, td.at-bot-of-report {{\n{decls}\n\n}}",
        },
        {
            "name": "Column-Headers",
            "template": "th, #colhdr\n{{\n{decls}\n}}",
            "defaults": [
                ("padding-top", "     padding-top:5px;"),
                ("padding-bottom", "     padding-bottom:4px;"),
            ],
        },
        {
            "name": "Column-Data",
            "template": "td.tblin, caption, table.tblin \n{{\n{decls}\n}}",
            "extras": ["  caption {padding-top:5px;}"],
        },
        {"name": "Column-Alt-Row", "template": "td.alt\n{{\n{decls}\n}}"},
        {"name": "At-Top-of-Report", "template": "p.at-top-of-report\n{{\n{decls}\n}}"},
        {
            "name": "JQX-All-IChart-Text",
            "template": (
                ".jqx-chart-axis-text, .jqx-chart-label-text, .jqx-chart-legend-text,"
                " .jqx-chart-axis-description, .jqx-chart-title-text,"
                " .jqx-chart-title-description {{\n{decls}\n}}"
            ),
            "defaults": [("fill", "     fill:black;")],
        },
        {"name": "At-Top-of-Col1", "template": "p.at-top-of-col1\n{{\n{decls}\n}}"},
        {"name": "At-Top-of-Col2", "template": "p.at-top-of-col2\n{{\n{decls}\n}}"},
        {"name": "At-Top-of-Col3", "template": "p.at-top-of-col3\n{{\n{decls}\n}}"},
    ]

    # Emit-time dispatch: report-type -> (method, option-keys, needs-template)
    _EMIT_DISPATCH: dict[str, tuple[str, list[str], bool]] = {
        "HTML-RUN": ("run", ["INSTANCE", "PROMPT-TEXT", "APP_SERVER_DEFAULT"], True),
        "HTML-LAYOUT": (
            "layout",
            [
                "OUTLOOK",
                "INSTANCE",
                "JSON-ONLY",
                "CHART-INSTANCE",
                "APP_SERVER_DEFAULT",
            ],
            True,
        ),
        "HTML-DEFER": (
            "defer",
            ["INSTANCE", "ID", "PROMPT-TEXT", "APP_SERVER_DEFAULT"],
            True,
        ),
        "HTML-DELETE": ("delete", ["INSTANCE"], False),
    }

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        report = options.lookup.get("REPORT")
        if report and report.upper().startswith("HTML-"):
            return Kind.HTML_REPORT, "/REPORT starts with HTML-"
        return None

    def __init__(self) -> None:
        self.styles: dict[str, list[str]] = {}
        self.css_file: str | None = None
        self.deferred_reports: dict[str, dict[str, Any]] = {}
        self.instance: str | None = None

    # ------------------------------------------------------------------
    # Emit-time (code generation)
    # ------------------------------------------------------------------

    @classmethod
    def emit_block(cls, block) -> list[str] | None:
        report_type = block.resolved_options.lookup.get("REPORT", "").upper().strip()
        entry = cls._EMIT_DISPATCH.get(report_type)
        if entry is None:
            return None
        method_name, keys, needs_template = entry

        kwargs: dict[str, str] = {}
        for key in keys:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = MacroState.to_py_expr(val)
        if needs_template:
            kwargs["template"] = repr(block.resolved_body)

        method = getattr(cls, method_name)
        args = ("ctx",) if method_name == "layout" else ()
        return [method.render(*args, **kwargs)]

    # ------------------------------------------------------------------
    # Template parsing
    # ------------------------------------------------------------------

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

    # ------------------------------------------------------------------
    # Emittable runtime methods
    # ------------------------------------------------------------------

    @emittable
    def run(
        self,
        instance: str | None = None,
        prompt_text: str | None = None,
        app_server_default: str | None = None,
        template: str | None = None,
    ) -> None:
        self.instance = instance
        for parts in self._iter_rows(template):
            key = parts[0].upper()
            if key == "CSS":
                self.css_file = parts[1] or None
            elif key == "FORMAT" and len(parts) >= 3:
                self.styles[parts[1]] = parts[2:]

    @emittable
    def defer(
        self,
        id: str,
        instance: str | None = None,
        prompt_text: str | None = None,
        app_server_default: str | None = None,
        template: str | None = None,
    ) -> None:
        self.deferred_reports[id] = {
            "instance": instance,
            "template": template,
            "options": self._parse_options(template),
        }

    @emittable
    def delete(self, instance: str | None = None) -> None:
        self.styles.clear()
        self.css_file = None
        self.deferred_reports.clear()

    @emittable
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
        directives, body = self._split_layout(template)

        body = re.sub(
            r"HTM:([A-Za-z0-9_]+)",
            lambda m: (
                self._render_report(m.group(1), ctx)
                if m.group(1) in self.deferred_reports
                else m.group(0)
            ),
            body,
        )

        css_file = directives.get("CSS") or self.css_file
        css_embed = directives.get("CSSEMBED", "").upper() in self._TRUE_VALUES
        css_decl = self._resolve_css(css_file, css_embed)
        title = directives.get("TITLE", "SQLPathFinder Report")

        if "<html>" not in body.lower():
            body = self._HTML_SCAFFOLD.format(title=title, css_decl=css_decl, body=body)
        elif css_decl:
            if "</head>" in body:
                body = body.replace("</head>", f"{css_decl}\n</head>", 1)
            else:
                body = f"{css_decl}\n{body}"

        filename = self._resolve_output_filename(
            directives.get("FILE", "report.html"), instance
        )
        if ctx and hasattr(ctx, "macro"):
            filename = ctx.macro.substitute(filename)
        if ctx and hasattr(ctx, "write_file"):
            ctx.write_file(filename, body)
        else:
            out = Path(filename)
            out.parent.mkdir(parents=True, exist_ok=True)
            out.write_text(body, encoding="utf-8")

    # ------------------------------------------------------------------
    # Layout template split
    # ------------------------------------------------------------------

    @staticmethod
    def _split_layout(template: str) -> tuple[dict[str, str], str]:
        """Extract ':KEY:VALUE' directives from a layout template, return (dirs, body)."""
        directives: dict[str, str] = {}
        body_lines: list[str] = []
        for line in template.splitlines():
            if line.startswith(":"):
                head, sep, value = line[1:].partition(":")
                if sep:
                    directives[head.strip().upper()] = value.strip()
                    continue
            body_lines.append(line)
        return directives, "\n".join(body_lines)

    # ------------------------------------------------------------------
    # CSS
    # ------------------------------------------------------------------

    def _resolve_css(self, css_file: str | None, css_embed: bool) -> str:
        """Return a <style> or <link> tag string (or empty string)."""
        content = ""
        if css_file:
            path = Path(css_file)
            if path.exists():
                content = path.read_text(encoding="utf-8", errors="replace")
            elif self.styles:
                content = self._build_css()
                try:
                    path.parent.mkdir(parents=True, exist_ok=True)
                    path.write_text(content, encoding="utf-8")
                except OSError:
                    pass
        elif self.styles:
            content = self._build_css()

        if css_embed and content:
            return f'<style type="text/css">\n{content}\n</style>'
        if css_file and not css_embed:
            return f'<link rel="stylesheet" type="text/css" href="{css_file}" />'
        return ""

    def _build_css(self) -> str:
        def get_decls(name: str) -> list[str]:
            decls: list[str] = []
            for d in self.styles.get(name, []):
                d = d.strip()
                if not d:
                    continue
                if ":" in d:
                    key, val = (s.strip() for s in d.split(":", 1))
                    if key == "font-size" and val.isdigit():
                        val += "px"
                    decls.append(f"     {key}:{val};")
                else:
                    decls.append(f"     {d};")
            return decls

        blocks: list[str] = []
        for rule in self._CSS_RULES:
            decls = get_decls(rule["name"])
            if not decls:
                continue
            extras = list(decls)
            for token, default_decl in rule.get("defaults", []):
                if not any(token in d for d in extras):
                    extras.append(default_decl)
            blocks.append(rule["template"].format(decls="\n".join(extras)))
            blocks.extend(rule.get("extras", []))
            tail = rule.get("tail_template")
            if tail:
                blocks.append(tail.format(decls="\n".join(decls)))
        return "\n\n".join(blocks)

    # ------------------------------------------------------------------
    # Deferred report rendering
    # ------------------------------------------------------------------

    def _render_report(self, report_id: str, ctx: Any) -> str:
        report = self.deferred_reports.get(report_id)
        if not report:
            return ""
        options = report.get("options")
        if not isinstance(options, dict):
            options = self._parse_options(report.get("template"))
            report["options"] = options

        def as_list(val: Any) -> list[str]:
            if val is None:
                return []
            return list(val) if isinstance(val, list) else [str(val)]

        cols = as_list(options.get("COLUMN-DATA"))
        headers = as_list(options.get("COLUMN-HEADERS"))
        alignments = as_list(options.get("COLUMN-ALIGNMENT"))
        alignments += ["middle-left"] * (len(cols) - len(alignments))

        raw_path = options.get("INPUT-FILE", "")
        if isinstance(raw_path, list):
            raw_path = raw_path[0] if raw_path else ""
        rows = self._load_csv_rows(str(raw_path), ctx)

        lines: list[str] = ['<table class="tblin">']
        lines.extend("<COL>" for _ in cols)
        lines.append("<thead>")
        lines.append("<tr id='colhdr'>")
        lines.extend(f"<th>{h}</th>" for h in headers)
        lines.append("</tr>")
        lines.append("</thead>")

        for idx, row in enumerate(rows):
            cell_class = "tblin" if idx % 2 == 0 else "alt"
            lines.append("<tr>")
            for ci, col in enumerate(cols):
                val_str = self._format_cell(col, row.get(col.lower(), ""))
                valign, halign = self._parse_alignment(alignments[ci])
                lines.append(
                    f'<td class="{cell_class}" '
                    f'style="vertical-align:{valign};text-align:{halign};">'
                    f"{val_str}</td>"
                )
            lines.append("</tr>")

        lines.append("<tfoot>")
        lines.append("</tfoot>")
        lines.append("</table>")

        content = "\n".join(lines)
        top = options.get("AT-TOP-OF-REPORT")
        if top:
            top_str = top if isinstance(top, str) else " ".join(top)
            content = f'<p class="at-top-of-report">\n{top_str}</p>\n{content}'
        return content

    @staticmethod
    def _parse_alignment(align: str) -> tuple[str, str]:
        parts = align.split("-")
        if len(parts) >= 2:
            return parts[0], parts[1]
        return "middle", parts[0] if parts else "left"

    @staticmethod
    def _format_cell(col_name: str, val: Any) -> str:
        if val is None:
            return "&nbsp;"
        s = str(val).strip()
        if s == "" or s.lower() == "nan":
            return "&nbsp;"
        if s.endswith("%"):
            return s
        low = col_name.lower()
        if "ce%" in low or "percent" in low:
            try:
                return f"{float(s) * 100:.2f}%"
            except ValueError:
                pass
        return s

    @staticmethod
    def _load_csv_rows(raw_path: str, ctx: Any) -> list[dict[str, Any]]:
        if not raw_path:
            return []
        if ctx and hasattr(ctx, "macro"):
            path = ctx.macro.resolve_file_path(raw_path)
        else:
            path = resolve_path(raw_path)
        if not (path and path.is_file()):
            return []
        if ctx and hasattr(ctx, "csv_io") and hasattr(ctx.csv_io, "iter"):
            source = ctx.csv_io.iter(str(path))
        else:
            with path.open(newline="", encoding="utf-8", errors="replace") as fh:
                source = list(csv.DictReader(fh))
        return [{k.lower(): v for k, v in row.items() if k} for row in source]

    # ------------------------------------------------------------------
    # Output filename
    # ------------------------------------------------------------------

    def _resolve_output_filename(self, path: str, instance: str | None) -> str:
        if path and not path.startswith("email:"):
            return path
        fallback = "report.html"
        for report in self.deferred_reports.values():
            options = report.get("options")
            if not isinstance(options, dict):
                options = self._parse_options(report.get("template"))
                report["options"] = options
            out = options.get("OUTPUT-FILE")
            if isinstance(out, list):
                out = out[0] if out else None
            if out:
                fallback = out
                break
        instance_id = instance or self.instance
        base = fallback.lower()
        return f"{instance_id}_{base}" if instance_id else base
