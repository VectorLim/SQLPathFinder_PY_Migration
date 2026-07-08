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
        self.styles: dict[str, list[str]] = {}
        self.css_file: str | None = None
        self.deferred_reports: dict[str, dict[str, Any]] = {}
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
        elif report_type == "HTML-DEFER":
            return cls._emit_html_defer(ctx, block)
        return None

    @classmethod
    def _emit_html_defer(cls, ctx, block) -> tuple[str, str]:
        kwargs = {}
        for key in ["INSTANCE", "ID", "PROMPT-TEXT", "APP_SERVER_DEFAULT"]:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = RawExpr(option_to_python_expr(val))
        kwargs["template"] = block.resolved_body
        stmt = ctx.render_method_call("html_report", "defer", kwargs=kwargs)
        return _emit_step_source(_step_name(block, "html_report"), [stmt])

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
                parts = [p.strip() for p in line.split("<\\>")]
                if len(parts) >= 2:
                    key = parts[0].upper()
                    if key == "CSS":
                        self.css_file = parts[1]
                    elif key == "FORMAT" and len(parts) >= 3:
                        self.styles[parts[1]] = parts[2:]

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
            "prompt_text": prompt_text,
            "app_server_default": app_server_default,
            "template": template,
        }

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

        # COLUMN-BORDER
        border_decls = get_decls("COLUMN-BORDER")
        if border_decls:
            css_blocks.append(
                "table.tblin, td.tblin, th, td.alt \n{\n" + "\n".join(border_decls) + "\n}"
            )
            css_blocks.append(
                "td.tblin,th,td.alt\n{\n      padding:5px;\n}"
            )
            css_blocks.append(
                "  table.tblin \n{\n     caption-side:top;\n}"
            )

        # Column-Headers
        header_decls = get_decls("Column-Headers")
        if header_decls:
            extra = []
            if not any("padding-top" in d for d in header_decls):
                extra.append("     padding-top:5px;")
            if not any("padding-bottom" in d for d in header_decls):
                extra.append("     padding-bottom:4px;")
            css_blocks.append(
                "th, #colhdr\n{\n" + "\n".join(header_decls + extra) + "\n}"
            )

        # Column-Data
        data_decls = get_decls("Column-Data")
        if data_decls:
            css_blocks.append(
                "td.tblin, caption, table.tblin \n{\n" + "\n".join(data_decls) + "\n}"
            )
            css_blocks.append(
                "  caption {padding-top:5px;}"
            )

        # Column-Alt-Row
        alt_decls = get_decls("Column-Alt-Row")
        if alt_decls:
            css_blocks.append(
                "td.alt\n{\n" + "\n".join(alt_decls) + "\n}"
            )

        # At-Top-of-Report
        top_decls = get_decls("At-Top-of-Report")
        if top_decls:
            css_blocks.append(
                "p.at-top-of-report\n{\n" + "\n".join(top_decls) + "\n}"
            )

        # JQX-All-IChart-Text
        chart_decls = get_decls("JQX-All-IChart-Text")
        if chart_decls:
            extra = []
            if not any("fill" in d for d in chart_decls):
                extra.append("     fill:black;")
            css_blocks.append(
                ".jqx-chart-axis-text, .jqx-chart-label-text, .jqx-chart-legend-text, .jqx-chart-axis-description, .jqx-chart-title-text, .jqx-chart-title-description {\n" + "\n".join(chart_decls + extra) + "\n}"
            )

        # tr.at-bot-of-report
        if border_decls:
            css_blocks.append(
                "tr.at-bot-of-report, td.at-bot-of-report {\n" + "\n".join(border_decls) + "\n\n}"
            )

        # At-Top-of-Col1, 2, 3
        for idx in (1, 2, 3):
            col_decls = get_decls(f"At-Top-of-Col{idx}")
            if col_decls:
                css_blocks.append(
                    f"p.at-top-of-col{idx} {{\n" + "\n".join(col_decls) + "\n}"
                )

        return "\n\n".join(css_blocks)

    def _render_report(self, report_id: str, ctx: Any) -> str:
        if report_id not in self.deferred_reports:
            return ""
        
        report = self.deferred_reports[report_id]
        template = report["template"] or ""
        
        options = {}
        for line in template.splitlines():
            if not line.strip():
                continue
            parts = [p.strip() for p in line.split("<\\>")]
            if len(parts) >= 2:
                key = parts[0].upper()
                if parts[1] == "":
                    val_list = [p for p in parts[2:] if p != ""]
                else:
                    val_list = [p for p in parts[1:] if p != ""]
                
                if len(val_list) == 0:
                    options[key] = ""
                elif len(val_list) == 1:
                    options[key] = val_list[0]
                else:
                    options[key] = val_list

        def as_list(val: Any) -> list[str]:
            if val is None:
                return []
            if isinstance(val, list):
                return val
            return [str(val)]

        cols = as_list(options.get("COLUMN-DATA"))
        headers = as_list(options.get("COLUMN-HEADERS"))
        alignments = as_list(options.get("COLUMN-ALIGNMENT"))
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

        def resolve_csv_path(raw_path: str, ctx: Any) -> Path:
            if not raw_path:
                return Path("")
            if ctx and hasattr(ctx, "macro"):
                resolved = ctx.macro.substitute_sql(raw_path)
            else:
                resolved = raw_path
            p = Path(resolved)
            if p.is_file() and p.exists():
                return p
            if p.is_absolute():
                rel_p = Path(p.name)
                if rel_p.is_file() and rel_p.exists():
                    return rel_p
            return p

        csv_path = resolve_csv_path(options.get("INPUT-FILE", ""), ctx)
        rows = []
        if csv_path.is_file() and csv_path.exists():
            import csv
            with csv_path.open(newline="", encoding="utf-8", errors="replace") as fh:
                reader = csv.DictReader(fh)
                for r in reader:
                    normalized_row = {k.lower(): v for k, v in r.items() if k}
                    rows.append(normalized_row)

        table_html = []
        table_html.append('<table class="tblin">')
        table_html.append('')
        table_html.append('')
        for _ in cols:
            table_html.append('<COL>')
        table_html.append('')
        
        table_html.append('<thead>')
        table_html.append("<tr id='colhdr'>")
        for h in headers:
            table_html.append(f'<th>{h}</th>')
        table_html.append('</tr>')
        table_html.append('</thead>')
        
        for idx, row in enumerate(rows):
            cell_class = "tblin" if idx % 2 == 0 else "alt"
            table_html.append('<tr>')
            for col_idx, col in enumerate(cols):
                val = row.get(col.lower(), "")
                val_str = format_value(col, val)
                valign, halign = parse_alignment(alignments[col_idx])
                table_html.append(
                    f'<td class="{cell_class}" style="vertical-align:{valign};text-align:{halign};">{val_str}</td>'
                )
            table_html.append('</tr>')
            
        table_html.append('')
        table_html.append('<tfoot>')
        table_html.append('</tfoot>')
        table_html.append('</table>')
        
        table_content = "\n".join(table_html)

        top_report = options.get("AT-TOP-OF-REPORT")
        if top_report:
            table_content = f'<p class="at-top-of-report">\n{top_report}</p>\n' + table_content

        return table_content

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
        title = "SQLPathFinder Report"
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
                    elif key == "TITLE":
                        title = val
            else:
                html_lines.append(line)

        html_content = "\n".join(html_lines)

        # Replace HTM placeholders
        def replace_report(match: re.Match) -> str:
            report_id = match.group(1)
            if report_id in self.deferred_reports:
                return self._render_report(report_id, ctx)
            return match.group(0)

        html_content = re.sub(r'HTM:([A-Za-z0-9_]+)', replace_report, html_content)

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
                    html_content = html_content.replace("</head>", f"{css_decl}\n</head>", 1)
                else:
                    html_content = f"{css_decl}\n{html_content}"

        # Resolve output filename
        out_filename = path
        if out_filename.startswith("email:") or not out_filename:
            fallback_name = "report.html"
            for report_id, report in self.deferred_reports.items():
                template = report.get("template") or ""
                found = False
                for line in template.splitlines():
                    if line.upper().startswith("OUTPUT-FILE"):
                        parts = [p.strip() for p in line.split("<\\>")]
                        if len(parts) >= 2 and parts[1]:
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
        if ctx and hasattr(ctx, "macro"):
            resolved_path = ctx.macro.substitute_sql(out_filename)
            ctx.macro.write_file(resolved_path, html_content)
        else:
            out_path = Path(out_filename)
            out_path.parent.mkdir(parents=True, exist_ok=True)
            out_path.write_text(html_content, encoding="utf-8")
