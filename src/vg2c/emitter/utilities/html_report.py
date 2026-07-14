"""HtmlReport - utility for generating HTML report files."""

from __future__ import annotations

from pathlib import Path
import re
from typing import Any

from vg2c.emitter.models import emittable
from vg2c.emitter.utilities._base import EmitterUtility
from vg2c.emitter.utilities.macro_state import MacroState
from vg2c.emitter.utilities._emit_helpers import resolve_path
from vg2c.kind import Kind






class HtmlReport(EmitterUtility):
    """Utility for generating HTML report files."""

    utility_name = "html_report"
    handles = (Kind.HTML_REPORT,)

    # ---------------------------------------------------------------------------
    # HTML scaffold for documents that don't already have an <html> wrapper
    # ---------------------------------------------------------------------------
    _HTML_SCAFFOLD = """\
    <html>
    <head>
    <title>{title}</title>
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
    {css_decl}



    <!--@SPF-JS-HEADER@-->
    <style type="text/css">

    table.tblout, td.tblout, tr.tblout
    {{
        border-width:0px;
        border-collapse:collapse;
        border-style:none;
        text-align:left;
        vertical-align:top;
    }}

    td.tblout {{
        padding:10px;
    }}
    img {{ vertical-align:top;}}


    </style>
    </head>
    <body>

    {body}
    </body>
    </html>
    """
    
    # ---------------------------------------------------------------------------
    # CSS rule definitions used by _build_css
    # ---------------------------------------------------------------------------
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
        self.prompt_text: str | None = None
        self.app_server_default: str | None = None

    # ------------------------------------------------------------------
    # Emit-time (code-generation) helpers
    # ------------------------------------------------------------------

    @classmethod
    def emit_block(cls, block) -> list[str] | None:
        report_type = block.resolved_options.lookup.get("REPORT", "").upper().strip()

        def _kwargs(keys: list[str]) -> dict[str, str]:
            out: dict[str, str] = {}
            for key in keys:
                val = block.resolved_options.lookup.get(key)
                if val is not None:
                    out[key.lower().replace("-", "_")] = MacroState.to_py_expr(val)
            return out

        if report_type == "HTML-RUN":
            kw = _kwargs(["INSTANCE", "PROMPT-TEXT", "APP_SERVER_DEFAULT"])
            kw["template"] = repr(block.resolved_body)
            return [cls.run.render(**kw)]

        if report_type == "HTML-LAYOUT":
            kw = _kwargs(["OUTLOOK", "INSTANCE", "JSON-ONLY", "CHART-INSTANCE", "APP_SERVER_DEFAULT"])
            kw["template"] = repr(block.resolved_body)
            return [cls.layout.render("ctx", **kw)]

        if report_type == "HTML-DEFER":
            kw = _kwargs(["INSTANCE", "ID", "PROMPT-TEXT", "APP_SERVER_DEFAULT"])
            kw["template"] = repr(block.resolved_body)
            return [cls.defer.render(**kw)]

        if report_type == "HTML-DELETE":
            return [cls.delete.render(**_kwargs(["INSTANCE"]))]

        return None

    # ------------------------------------------------------------------
    # Template parsing
    # ------------------------------------------------------------------

    @staticmethod
    def _parse_template_rows(template: str | None) -> list[list[str]]:
        rows: list[list[str]] = []
        for line in (template or "").splitlines():
            if not line.strip():
                continue
            rows.append([part.strip() for part in line.split("<\\>")])
        return rows

    @staticmethod
    def _extract_options(rows: list[list[str]]) -> dict[str, Any]:
        options: dict[str, Any] = {}
        for parts in rows:
            if len(parts) < 2:
                continue
            key = parts[0].upper()
            val_list = [p for p in (parts[2:] if parts[1] == "" else parts[1:]) if p != ""]
            if not val_list:
                options[key] = ""
            elif len(val_list) == 1:
                options[key] = val_list[0]
            else:
                options[key] = val_list
        return options

    @staticmethod
    def _as_list(val: Any) -> list[str]:
        if val is None:
            return []
        return val if isinstance(val, list) else [str(val)]

    def _ensure_parsed_payload(
        self, report: dict[str, Any]
    ) -> tuple[list[list[str]], dict[str, Any]]:
        rows = report.get("parsed_rows")
        if not isinstance(rows, list):
            rows = self._parse_template_rows(report.get("template"))
            report["parsed_rows"] = rows
        options = report.get("options")
        if not isinstance(options, dict):
            options = self._extract_options(rows)
            report["options"] = options
        return rows, options

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
        self.prompt_text = prompt_text
        self.app_server_default = app_server_default
        for parts in self._parse_template_rows(template):
            if len(parts) < 2:
                continue
            key = parts[0].upper()
            if key == "CSS":
                self.css_file = parts[1]
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
        parsed_rows = self._parse_template_rows(template)
        self.deferred_reports[id] = {
            "instance": instance,
            "prompt_text": prompt_text,
            "app_server_default": app_server_default,
            "template": template,
            "parsed_rows": parsed_rows,
            "options": self._extract_options(parsed_rows),
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
        directives, html_content = self._parse_layout_template(template)
        path = directives.get("FILE", "report.html")
        css_file = directives.get("CSS") or self.css_file
        css_embed = directives.get("CSSEMBED", "").upper() in ("Y", "YES", "TRUE")
        title = directives.get("TITLE", "SQLPathFinder Report")

        html_content = re.sub(
            r"HTM:([A-Za-z0-9_]+)",
            lambda m: self._render_report(m.group(1), ctx)
            if m.group(1) in self.deferred_reports
            else m.group(0),
            html_content,
        )

        css_decl = self._resolve_css(css_file, css_embed)

        if "<html>" not in html_content.lower():
            html_content = self._HTML_SCAFFOLD.format(
                title=title, css_decl=css_decl, body=html_content
            )
        elif css_decl:
            if "</head>" in html_content:
                html_content = html_content.replace("</head>", f"{css_decl}\n</head>", 1)
            else:
                html_content = f"{css_decl}\n{html_content}"

        out_filename = self._resolve_output_filename(path, instance)
        self._write_html(ctx, out_filename, html_content)

    # ------------------------------------------------------------------
    # CSS helpers
    # ------------------------------------------------------------------

    def _resolve_css(self, css_file: str | None, css_embed: bool) -> str:
        """Return a <style> or <link> tag string (or empty string)."""
        css_content = ""
        if css_file:
            css_path = Path(css_file)
            if css_path.exists():
                css_content = css_path.read_text(encoding="utf-8", errors="replace")
            elif self.styles:
                css_content = self._build_css()
                try:
                    css_path.parent.mkdir(parents=True, exist_ok=True)
                    css_path.write_text(css_content, encoding="utf-8")
                except Exception:
                    pass
        elif self.styles:
            css_content = self._build_css()

        if css_embed and css_content:
            return f'<style type="text/css">\n{css_content}\n</style>'
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
                    key, val = d.split(":", 1)
                    key, val = key.strip(), val.strip()
                    if key == "font-size" and val.isdigit():
                        val += "px"
                    decls.append(f"     {key}:{val};")
                else:
                    decls.append(f"     {d};")
            return decls

        css_blocks: list[str] = []
        for rule in self._CSS_RULES:
            decls = get_decls(rule["name"])
            if not decls:
                continue
            extra_decls = list(decls)
            for token, default_decl in rule.get("defaults", []):
                if not any(token in d for d in extra_decls):
                    extra_decls.append(default_decl)
            css_blocks.append(rule["template"].format(decls="\n".join(extra_decls)))
            for extra in rule.get("extras", []):
                css_blocks.append(extra)
            tail = rule.get("tail_template")
            if tail:
                css_blocks.append(tail.format(decls="\n".join(decls)))

        return "\n\n".join(css_blocks)

    # ------------------------------------------------------------------
    # Report rendering
    # ------------------------------------------------------------------

    @staticmethod
    def _parse_layout_template(template: str) -> tuple[dict[str, str], str]:
        directives: dict[str, str] = {}
        html_lines: list[str] = []
        for line in template.splitlines():
            if line.startswith(":"):
                parts = line[1:].split(":", 1)
                if len(parts) == 2:
                    directives[parts[0].strip().upper()] = parts[1].strip()
                    continue
            html_lines.append(line)
        return directives, "\n".join(html_lines)

    @staticmethod
    def _resolve_csv_path(raw_path: str, ctx: Any) -> Path:
        if not raw_path:
            return Path("")
        if ctx and hasattr(ctx, "macro"):
            return ctx.macro.resolve_file_path(raw_path)
        return resolve_path(raw_path)

    @staticmethod
    def _iter_csv_rows(csv_path: Path, ctx: Any) -> list[dict[str, Any]]:
        if not csv_path.is_file() or not csv_path.exists():
            return []
        if ctx and hasattr(ctx, "csv_io") and hasattr(ctx.csv_io, "iter"):
            return [
                {k.lower(): v for k, v in row.items() if k}
                for row in ctx.csv_io.iter(str(csv_path))
            ]
        import csv
        with csv_path.open(newline="", encoding="utf-8", errors="replace") as fh:
            return [
                {k.lower(): v for k, v in row.items() if k}
                for row in csv.DictReader(fh)
            ]

    def _render_report(self, report_id: str, ctx: Any) -> str:
        if report_id not in self.deferred_reports:
            return ""
        report = self.deferred_reports[report_id]
        _, options = self._ensure_parsed_payload(report)

        cols = self._as_list(options.get("COLUMN-DATA"))
        headers = self._as_list(options.get("COLUMN-HEADERS"))
        alignments = self._as_list(options.get("COLUMN-ALIGNMENT"))
        alignments += ["middle-left"] * (len(cols) - len(alignments))

        def parse_alignment(align: str) -> tuple[str, str]:
            parts = align.split("-")
            if len(parts) >= 2:
                return parts[0], parts[1]
            return "middle", parts[0] if parts else "left"

        def format_value(col_name: str, val: Any) -> str:
            if val is None:
                return "&nbsp;"
            s = str(val).strip()
            if s == "" or s.lower() == "nan":
                return "&nbsp;"
            if s.endswith("%"):
                return s
            if "ce%" in col_name.lower() or "percent" in col_name.lower():
                try:
                    return f"{float(s) * 100:.2f}%"
                except ValueError:
                    pass
            return s

        csv_path = self._resolve_csv_path(options.get("INPUT-FILE", ""), ctx)
        rows = self._iter_csv_rows(csv_path, ctx)

        lines: list[str] = ['<table class="tblin">', "", ""]
        for _ in cols:
            lines.append("<COL>")
        lines += [
            "",
            "<thead>",
            "<tr id='colhdr'>",
            *[f"<th>{h}</th>" for h in headers],
            "</tr>",
            "</thead>",
        ]
        for idx, row in enumerate(rows):
            cell_class = "tblin" if idx % 2 == 0 else "alt"
            lines.append("<tr>")
            for col_idx, col in enumerate(cols):
                val_str = format_value(col, row.get(col.lower(), ""))
                valign, halign = parse_alignment(alignments[col_idx])
                lines.append(
                    f'<td class="{cell_class}" style="vertical-align:{valign};text-align:{halign};">{val_str}</td>'
                )
            lines.append("</tr>")
        lines += ["", "<tfoot>", "</tfoot>", "</table>"]

        table_content = "\n".join(lines)
        top_report = options.get("AT-TOP-OF-REPORT")
        if top_report:
            table_content = f'<p class="at-top-of-report">\n{top_report}</p>\n' + table_content
        return table_content

    # ------------------------------------------------------------------
    # Output helpers
    # ------------------------------------------------------------------

    def _resolve_output_filename(self, path: str, instance: str | None) -> str:
        if not path.startswith("email:") and path:
            return path
        fallback_name = "report.html"
        for report in self.deferred_reports.values():
            rows, _ = self._ensure_parsed_payload(report)
            for parts in rows:
                if parts and parts[0].upper() == "OUTPUT-FILE" and len(parts) >= 2 and parts[1]:
                    fallback_name = parts[1]
                    break
            else:
                continue
            break
        instance_id = instance or self.instance
        base = fallback_name.lower()
        return f"{instance_id}_{base}" if instance_id else base

    @staticmethod
    def _write_html(ctx: Any, filename: str, content: str) -> None:
        resolved = filename
        if ctx and hasattr(ctx, "macro"):
            resolved = ctx.macro.substitute(filename)
        if ctx and hasattr(ctx, "write_file"):
            ctx.write_file(resolved, content)
        else:
            out = Path(resolved)
            out.parent.mkdir(parents=True, exist_ok=True)
            out.write_text(content, encoding="utf-8")
