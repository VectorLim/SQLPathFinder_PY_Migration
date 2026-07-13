"""HtmlReport - utility for generating HTML files."""

from __future__ import annotations

from pathlib import Path
import re
from typing import Any

from vg2c.emitter.models import emittable
from vg2c.emitter.utilities._base import CheckedUtilitySpec
from vg2c.emitter.utilities.macro_state import MacroState
from vg2c.emitter.utilities._emit_helpers import (
    resolve_path,
)
from vg2c.kind import Kind


class HtmlReport(CheckedUtilitySpec):
    """Utility for generating HTML report files."""

    utility_name = "html_report"
    handles = (Kind.HTML_REPORT,)

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

    @classmethod
    def emit_block(cls, block) -> list[str] | None:
        report_type = block.resolved_options.lookup.get("REPORT", "").upper().strip()
        if report_type == "HTML-RUN":
            return cls._emit_html_run(block)
        elif report_type == "HTML-LAYOUT":
            return cls._emit_html_layout(block)
        elif report_type == "HTML-DELETE":
            return cls._emit_html_delete(block)
        elif report_type == "HTML-DEFER":
            return cls._emit_html_defer(block)
        return None

    @staticmethod
    def _emit_method(
        block,
        method: str,
        option_keys: list[str],
        *,
        args: tuple[str, ...] = (),
        include_template: bool = False,
    ) -> list[str]:
        kwargs = {}
        for key in option_keys:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = MacroState.to_py_expr(val)
        if include_template:
            kwargs["template"] = repr(block.resolved_body)
        method_obj = getattr(HtmlReport, method)
        stmt = method_obj.render(*args, **kwargs)
        return [stmt]

    @staticmethod
    def _emit_html_defer(block) -> list[str]:
        return HtmlReport._emit_method(
            block,
            "defer",
            ["INSTANCE", "ID", "PROMPT-TEXT", "APP_SERVER_DEFAULT"],
            include_template=True,
        )

    @staticmethod
    def _emit_html_run(block) -> list[str]:
        return HtmlReport._emit_method(
            block,
            "run",
            ["INSTANCE", "PROMPT-TEXT", "APP_SERVER_DEFAULT"],
            include_template=True,
        )

    @staticmethod
    def _emit_html_layout(block) -> list[str]:
        return HtmlReport._emit_method(
            block,
            "layout",
            [
                "OUTLOOK",
                "INSTANCE",
                "JSON-ONLY",
                "CHART-INSTANCE",
                "APP_SERVER_DEFAULT",
            ],
            args=("ctx",),
            include_template=True,
        )

    @staticmethod
    def _emit_html_delete(block) -> list[str]:
        return HtmlReport._emit_method(block, "delete", ["INSTANCE"])

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
            if parts[1] == "":
                val_list = [part for part in parts[2:] if part != ""]
            else:
                val_list = [part for part in parts[1:] if part != ""]

            if len(val_list) == 0:
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
        if isinstance(val, list):
            return val
        return [str(val)]

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
            macro = ctx.macro
            if hasattr(macro, "resolve_file_path"):
                return macro.resolve_file_path(raw_path)
            resolved = macro.substitute_sql(raw_path)
        else:
            resolved = raw_path
        return resolve_path(resolved)

    @staticmethod
    def _iter_csv_rows(csv_path: Path, ctx: Any) -> list[dict[str, Any]]:
        rows: list[dict[str, Any]] = []
        if not csv_path.is_file() or not csv_path.exists():
            return rows

        if ctx and hasattr(ctx, "csv_io") and hasattr(ctx.csv_io, "iter"):
            for row in ctx.csv_io.iter(str(csv_path)):
                normalized_row = {k.lower(): v for k, v in row.items() if k}
                rows.append(normalized_row)
            return rows

        import csv

        with csv_path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.DictReader(fh)
            for row in reader:
                normalized_row = {k.lower(): v for k, v in row.items() if k}
                rows.append(normalized_row)
        return rows

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

        if template:
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

    def _build_css(self) -> str:
        css_blocks = []

        def get_decls(name: str) -> list[str]:
            raw_decls = self.styles.get(name, [])
            decls = []
            for d in raw_decls:
                d = d.strip()
                if not d:
                    continue
                if ":" in d:
                    key, val = d.split(":", 1)
                    key = key.strip()
                    val = val.strip()
                    if key == "font-size" and val.isdigit():
                        val = val + "px"
                    decls.append(f"     {key}:{val};")
                else:
                    decls.append(f"     {d};")
            return decls

        css_rules: list[dict[str, Any]] = [
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
            {
                "name": "Column-Alt-Row",
                "template": "td.alt\n{{\n{decls}\n}}",
            },
            {
                "name": "At-Top-of-Report",
                "template": "p.at-top-of-report\n{{\n{decls}\n}}",
            },
            {
                "name": "JQX-All-IChart-Text",
                "template": ".jqx-chart-axis-text, .jqx-chart-label-text, .jqx-chart-legend-text, .jqx-chart-axis-description, .jqx-chart-title-text, .jqx-chart-title-description {{\n{decls}\n}}",
                "defaults": [("fill", "     fill:black;")],
            },
        ]

        for rule in css_rules:
            decls = get_decls(rule["name"])
            if not decls:
                continue

            extra_decls = list(decls)
            for token, default_decl in rule.get("defaults", []):
                if not any(token in decl for decl in extra_decls):
                    extra_decls.append(default_decl)

            css_blocks.append(rule["template"].format(decls="\n".join(extra_decls)))
            for extra_block in rule.get("extras", []):
                css_blocks.append(extra_block)

            tail_template = rule.get("tail_template")
            if tail_template:
                css_blocks.append(tail_template.format(decls="\n".join(decls)))

        col_rules = {
            "At-Top-of-Col1": "p.at-top-of-col1 {{\n{decls}\n}}",
            "At-Top-of-Col2": "p.at-top-of-col2 {{\n{decls}\n}}",
            "At-Top-of-Col3": "p.at-top-of-col3 {{\n{decls}\n}}",
        }
        for format_name, selector in col_rules.items():
            decls = get_decls(format_name)
            if decls:
                css_blocks.append(selector.format(decls="\n".join(decls)))

        return "\n\n".join(css_blocks)

    def _render_report(self, report_id: str, ctx: Any) -> str:
        if report_id not in self.deferred_reports:
            return ""

        report = self.deferred_reports[report_id]
        _, options = self._ensure_parsed_payload(report)

        cols = self._as_list(options.get("COLUMN-DATA"))
        headers = self._as_list(options.get("COLUMN-HEADERS"))
        alignments = self._as_list(options.get("COLUMN-ALIGNMENT"))
        alignments = alignments + ["middle-left"] * (len(cols) - len(alignments))

        def parse_alignment(align: str) -> tuple[str, str]:
            parts = align.split("-")
            valign = "middle"
            halign = "left"
            if len(parts) >= 2:
                valign = parts[0]
                halign = parts[1]
            elif len(parts) == 1:
                valign = "middle"
                halign = parts[0]
            return valign, halign

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
                    fval = float(s)
                    return f"{fval * 100:.2f}%"
                except ValueError:
                    pass
            return s

        csv_path = self._resolve_csv_path(options.get("INPUT-FILE", ""), ctx)
        rows = self._iter_csv_rows(csv_path, ctx)

        table_html = []
        table_html.append('<table class="tblin">')
        table_html.append("")
        table_html.append("")
        for _ in cols:
            table_html.append("<COL>")
        table_html.append("")

        table_html.append("<thead>")
        table_html.append("<tr id='colhdr'>")
        for h in headers:
            table_html.append(f"<th>{h}</th>")
        table_html.append("</tr>")
        table_html.append("</thead>")

        for idx, row in enumerate(rows):
            cell_class = "tblin" if idx % 2 == 0 else "alt"
            table_html.append("<tr>")
            for col_idx, col in enumerate(cols):
                val = row.get(col.lower(), "")
                val_str = format_value(col, val)
                valign, halign = parse_alignment(alignments[col_idx])
                table_html.append(
                    f'<td class="{cell_class}" style="vertical-align:{valign};text-align:{halign};">{val_str}</td>'
                )
            table_html.append("</tr>")

        table_html.append("")
        table_html.append("<tfoot>")
        table_html.append("</tfoot>")
        table_html.append("</table>")

        table_content = "\n".join(table_html)

        top_report = options.get("AT-TOP-OF-REPORT")
        if top_report:
            table_content = (
                f'<p class="at-top-of-report">\n{top_report}</p>\n' + table_content
            )

        return table_content

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
        css_file = directives.get("CSS")
        css_embed = directives.get("CSSEMBED", "").upper() in ("Y", "YES", "TRUE")
        title = directives.get("TITLE", "SQLPathFinder Report")

        # Replace HTM placeholders
        def replace_report(match: re.Match) -> str:
            report_id = match.group(1)
            if report_id in self.deferred_reports:
                return self._render_report(report_id, ctx)
            return match.group(0)

        html_content = re.sub(r"HTM:([A-Za-z0-9_]+)", replace_report, html_content)

        # Resolve CSS content
        resolved_css_file = css_file if css_file else self.css_file
        css_content = ""
        if resolved_css_file:
            css_path = Path(resolved_css_file)
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

        # Build style or link tags
        css_decl = ""
        if resolved_css_file:
            if css_embed:
                if css_content:
                    css_decl = f'<style type="text/css">\n{css_content}\n</style>'
            else:
                css_decl = f'<link rel="stylesheet" type="text/css" href="{resolved_css_file}" />'
        elif css_embed and css_content:
            css_decl = f'<style type="text/css">\n{css_content}\n</style>'

        # Wrap in full HTML document if needed
        if "<html>" not in html_content.lower():
            html_content = (
                f"<html>\n<head>\n<title>{title}</title>\n"
                f'<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">\n'
                f"{css_decl}\n\n\n\n"
                f"<!--@SPF-JS-HEADER@-->\n"
                f'<style type="text/css">\n\n'
                f"table.tblout, td.tblout, tr.tblout\n"
                f"{{\n"
                f"     border-width:0px;\n"
                f"     border-collapse:collapse;\n"
                f"     border-style:none;\n"
                f"     text-align:left;\n"
                f"     vertical-align:top;\n"
                f"}}\n\n"
                f"td.tblout {{\n"
                f"    padding:10px;\n"
                f"}}\n"
                f"img {{ vertical-align:top;}}\n\n\n"
                f"</style>\n"
                f"</head>\n"
                f"<body>\n\n"
                f"{html_content}\n"
                f"</body>\n"
                f"</html>\n"
            )
        else:
            if css_decl:
                if "</head>" in html_content:
                    html_content = html_content.replace(
                        "</head>", f"{css_decl}\n</head>", 1
                    )
                else:
                    html_content = f"{css_decl}\n{html_content}"

        # Resolve output filename
        out_filename = path
        if out_filename.startswith("email:") or not out_filename:
            fallback_name = "report.html"
            for report in self.deferred_reports.values():
                rows, _ = self._ensure_parsed_payload(report)
                found = False
                for parts in rows:
                    if not parts:
                        continue
                    if (
                        parts[0].upper() == "OUTPUT-FILE"
                        and len(parts) >= 2
                        and parts[1]
                    ):
                        fallback_name = parts[1]
                        found = True
                        break
                if found:
                    break
            instance_id = instance or self.instance
            if instance_id:
                out_filename = f"{instance_id}_{fallback_name.lower()}"
            else:
                out_filename = fallback_name.lower()

        # Write output file
        if ctx and hasattr(ctx, "write_file"):
            resolved_path = out_filename
            if hasattr(ctx, "macro"):
                resolved_path = ctx.macro.substitute_sql(out_filename)
            ctx.write_file(resolved_path, html_content)
        elif ctx and hasattr(ctx, "macro"):
            resolved_path = ctx.macro.substitute_sql(out_filename)
            ctx.macro.write_file(resolved_path, html_content)
        else:
            out_path = Path(out_filename)
            out_path.parent.mkdir(parents=True, exist_ok=True)
            out_path.write_text(html_content, encoding="utf-8")
