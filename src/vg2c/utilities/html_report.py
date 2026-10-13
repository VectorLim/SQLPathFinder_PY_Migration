"""VG2 HTML report classification, parsing and emitted-operation metadata.

Generated jobs render only through vg2c.runtime.html. This compiler helper does
not retain the historical stateful HTML preview/rendering engine.
"""
from __future__ import annotations

import logging
import re
from dataclasses import replace
from html import escape
from typing import Any

from vg2c.emitter.models import BlockEmission, CodeExpr, ExternalParameter, RenderedCall
from vg2c.emitter.operations import EmittableOperation, emittable
from vg2c.kind import Kind
from vg2c.runtime.html_format import build_css
from vg2c.utilities._base import EmitterUtility, UtilitySpec
from vg2c.utilities._emit_helpers import (
    native_argument,
    native_expression,
    native_macro_expression,
)
from vg2c.utilities.macro_control import MacroControl

# Create_HTML_Window and Process_HTM in SPFUtilities/utils.py:12175-12205,
# 14046-14125 treat HTM: report references as whole layout lines.
_HTM_LAYOUT_LINE = re.compile(
    r"(?im)^(?P<indent>[ \t]*)HTM:(?P<id>[A-Za-z_][A-Za-z0-9_]*)(?P<suffix>[ \t]*)(?=\r?$)"
)


class HtmlReport(EmitterUtility):
    """Compile HTML report blocks and own per-project report state."""

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

    def __init__(self, blocks=()):
        self.report_options = {}
        self.layout_references = {}
        self.report_definitions = {}
        for candidate in blocks:
            report_type = candidate.resolved_options.lookup.get("REPORT", "").upper()
            if candidate.kind is Kind.HTML_REPORT and report_type == "HTML-DEFER":
                name = candidate.resolved_options.lookup.get("ID", "")
                self.report_definitions[name] = self.report_definitions.get(name, 0) + 1
            if candidate.kind is Kind.HTML_REPORT and report_type == "HTML-LAYOUT":
                for match in _HTM_LAYOUT_LINE.finditer(candidate.resolved_body):
                    key = match.group("id")
                    self.layout_references[key] = self.layout_references.get(key, 0) + 1

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        report = options.lookup.get("REPORT")
        if report and report.upper().startswith("HTML-"):
            return Kind.HTML_REPORT, "/REPORT starts with HTML-"
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

    @staticmethod
    def _split_layout(template: str) -> tuple[dict[str, str], str]:
        """Parse compiler-visible layout directives, without rendering HTML."""
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

    @staticmethod
    def _list(value):
        return value if isinstance(value, list) else [value] if value else []

    def _report_options(self, block):
        options = self._parse_options(block.resolved_body)
        allowed = {
            "TYPE", "INPUT-FILE", "OUTPUT-FILE", "CSS", "COLUMN-DATA",
            "COLUMN-HEADERS", "COLUMN-ALIGNMENT", "AT-TOP-OF-REPORT", "NOPREPROCESS",
        }
        for name, value in options.items():
            if name not in allowed and value and str(value).upper() not in {"N", "NO", "FALSE"}:
                raise ValueError(f"Active report option {name} is unsupported")
        return options

    @classmethod
    def _table(cls, report_id, options):
        """Build the editable projection and presentation for a report."""
        columns = cls._list(options.get("COLUMN-DATA"))
        labels = cls._list(options.get("COLUMN-HEADERS"))
        alignment = cls._list(options.get("COLUMN-ALIGNMENT"))
        if not columns:
            raise ValueError(f"Report {report_id} requires COLUMN-DATA")
        parts = []
        for index, field in enumerate(columns):
            label = labels[index] if index < len(labels) else field
            align = alignment[index] if index < len(alignment) else "middle-left"
            # Pattern SOURCE label is a column-name matcher, not a display title.
            pattern = f' data-pattern="{escape(label, quote=True).replace("$", "&#36;")}"' if field.upper() in (
                "STARTS WITH:", "ENDS WITH:", "CONTAINS:", "STARTS/ENDS WITH (%):"
            ) else ""
            display = "Matching CSV columns" if pattern else label
            parts.append(f'<th data-field="{escape(field, quote=True).replace("$", "&#36;")}"{pattern} '
                         f'data-align="{escape(align, quote=True)}">{escape(display).replace("$", "$$")}</th>')
        top = options.get("AT-TOP-OF-REPORT")
        heading = '<p class="at-top-of-report">' + escape(" ".join(cls._list(top))).replace("$", "$$") + "</p>\n" if top else ""
        return (heading + f'<table class="tblin" data-report="{escape(report_id, quote=True)}">'
                '<thead><tr id="colhdr">' + "".join(parts) +
                '</tr></thead><tbody></tbody><tfoot></tfoot></table>')

    @staticmethod
    def _operation_definition(method):
        definition = UtilitySpec.operation_definition("html_report", method)
        if definition is None:
            raise ValueError(f"Missing compiler operation html_report.{method}")
        return definition

    @classmethod
    def _tracked(cls, lines, definition):
        return RenderedCall("\n".join(lines), definition, ())

    def _external_parameters(self, block, method):
        definition = UtilitySpec.operation_definition("html_report", method)
        if definition is None:
            return ()
        metadata = {"template": block.resolved_body}
        metadata.update({name: block.resolved_options.lookup.get(option) for name, option in
                         {"id": "ID", "instance": "INSTANCE", "prompt_text": "PROMPT-TEXT",
                          "app_server_default": "APP_SERVER_DEFAULT", "outlook": "OUTLOOK",
                          "json_only": "JSON-ONLY", "chart_instance": "CHART-INSTANCE"}.items()
                         if definition.parameter(name) is not None})
        reason = (
            "Report definitions and shells are external assets; edit the project assets or "
            "regenerate from source."
        )
        parameters = []
        for name, value in metadata.items():
            parameter = definition.parameter(name)
            if parameter is None:
                continue
            parameters.append(ExternalParameter(
                definition=parameter,
                value=value,
                editor_type="multiline" if name == "template" else "string",
                read_only_reason=reason,
            ))
        return tuple(parameters)

    @staticmethod
    def _render_layout_call(*args, macros="macros", **kwargs):
        definition = HtmlReport._operation_definition("layout")
        path = replace(
            definition.parameter("template"),
            name="template_path",
            position=0,
            display_label="HTML asset",
        )
        definition = replace(
            definition,
            parameters=tuple(
                [path]
                + [item for item in definition.parameters if item.name not in {"ctx", "template"}]
            ),
        )
        return EmittableOperation.render_method_call(
            definition,
            function="job.html",
            args=tuple(native_argument(value, macros) for value in args),
            kwargs={key: native_argument(value, macros) for key, value in kwargs.items()},
        )

    def emit_block(self, block, *, macros="macros") -> BlockEmission:
        report_type = block.resolved_options.lookup.get("REPORT", "").upper()
        method = report_type.removeprefix("HTML-").lower()
        external_parameters = self._external_parameters(block, method)
        if report_type == "HTML-DELETE":
            # SPFSQL3.py:20244-20250 deletes consumed spec files, not CSS.
            definition = self._operation_definition(method)
            call = self._tracked(["job.delete_html()"], definition)
            return BlockEmission((call,), external_parameters=external_parameters)
        if report_type == "HTML-RUN":
            return self._emit_run(block, macros, external_parameters, method)
        if report_type == "HTML-DEFER":
            return self._emit_defer(block, macros, external_parameters, method)
        if report_type == "HTML-LAYOUT":
            directives, body = self.prepare_layout(block)
            return self.emit_layout(block, directives, body, macros=macros)
        raise ValueError(f"Unsupported report type {report_type}")

    def _emit_run(self, block, macros, external_parameters, method):
        # SPFSQL3.py:20083-20100: CSS and HTML have immediate effects.
        rows = list(self._iter_rows(block.resolved_body))
        # The first TYPE/KEY/COL1... row is the spec header, not the report type.
        kind = next((parts[1].upper() for parts in rows
                     if parts[0].upper() == "TYPE" and parts[1].upper() != "KEY"), "")
        definition = self._operation_definition(method)
        if kind == "CSS":
            logical_name = next((parts[1] for parts in rows if parts[0].upper() == "CSS"), "")
            if not logical_name:
                return BlockEmission(
                    (self._tracked(["pass"], definition),),
                    external_parameters=external_parameters,
                )
            if "<<<" in logical_name:
                raise ValueError(
                    "Dynamic HTML-RUN CSS filenames require source-backed runtime generation"
                )
            styles = {parts[1]: parts[2:] for parts in rows if parts[0].upper() == "FORMAT"}
            name = f"styles/report_{block.index:03d}.css"
            return BlockEmission(
                (self._tracked([f"job.define_css({logical_name!r}, {name!r})"], definition),),
                assets=((name, build_css(styles) + "\n"),),
                external_parameters=external_parameters,
            )
        if kind == "HTML":
            options = self._report_options(block)
            report_id = f"RUN_{block.index:03d}"
            shell = self._HTML_SCAFFOLD.format(
                title="SQLPathFinder Report",
                css_decl="${VG2C_CSS}",
                body=self._table(report_id, options),
            )
            name = f"html/report_{block.index:03d}.html"
            input_file = options.get("INPUT-FILE", "")
            output = options.get("OUTPUT-FILE") or "SQLPathFinder.htm"
            call = self._tracked([
                f"job.html({name!r}, output={native_macro_expression(output, macros)}, "
                f"reports={{{report_id!r}: "
                f"csv_report({native_macro_expression(input_file, macros)})}})"
            ], definition)
            return BlockEmission(
                (call,),
                runtime_exports=("csv_report",),
                assets=((name, shell),),
                external_parameters=external_parameters,
            )
        if kind == "KEY":
            # SPFSQL3.py:20095-20109 dispatches only CSS/HTML/HTMLI5.
            return BlockEmission(
                (self._tracked(["pass"], definition),),
                external_parameters=external_parameters,
            )
        raise ValueError(f"Unsupported HTML-RUN TYPE {kind!r}; expected CSS, HTML or KEY")

    def _emit_defer(self, block, macros, external_parameters, method):
        options = self._report_options(block)
        report_id = block.resolved_options.lookup.get("ID", "")
        if not re.fullmatch(r"[A-Za-z_][A-Za-z0-9_]*", report_id):
            raise ValueError(f"Invalid report ID {report_id!r}")
        self.report_options[report_id] = options
        input_file = options.get("INPUT-FILE", "")
        output_file = options.get("OUTPUT-FILE")
        assets = ()
        if (self.layout_references.get(report_id, 0) > 1
                or self.report_definitions.get(report_id, 0) > 1):
            fragment = f"html/reports/{report_id}_{block.index:03d}.table.html"
            assets = ((fragment, self._table(report_id, options) + "\n"),)
            report_ctor = native_expression(
                f"job.report({fragment!r}, input_file={input_file!r}, "
                f"output_file={output_file!r})", macros
            )
        else:
            report_ctor = native_expression(
                f"csv_report({input_file!r}, output_file={output_file!r})", macros
            )
        call = self._tracked(
            [f"job.reports[{report_id!r}] = {report_ctor}"],
            self._operation_definition(method),
        )
        return BlockEmission(
            (call,),
            runtime_exports=("csv_report",),
            assets=assets,
            external_parameters=external_parameters,
        )

    def prepare_layout(self, block):
        """Parse directives and expand deferred reports before notification edits."""
        directives = {}
        body = []
        for line in block.resolved_body.splitlines(keepends=True):
            if line.startswith(":") and ":" in line[1:]:
                key, value = line[1:].split(":", 1)
                directives[key.upper()] = value.strip()
            else:
                body.append(line)
        unsupported = [key for key, value in directives.items()
                       if key not in {"FILE", "TITLE", "CSS", "CSSEMBED"}
                       and value and value.upper() not in {"N", "NO", "FALSE"}]
        unsupported += [key for key in ("OUTLOOK", "JSON-ONLY", "CHART-INSTANCE", "APP_SERVER_DEFAULT")
                        if (value := block.resolved_options.lookup.get(key))
                        and value.upper() not in {"N", "NO", "FALSE"}]
        if unsupported:
            # Preserve the original diagnostic category for callers that capture logs.
            logging.getLogger("vg2c.emitter.project").warning(
                "[local-html-only] %s:%s:1 (block %s): Local HTML output does not implement %s; delivery/browser/security/chart integration is outside this renderer.",
                block.span.file or "<input>", block.span.start_line, block.index,
                ", ".join(unsupported),
            )
        source = "".join(body).replace("$", "$$")
        if re.search(r"(?m)^[ \t]*HTMI(?:C)?:[^\r\n]*$", source, re.I):
            raise ValueError("HTMI/HTMIC interactive layout references are unsupported")
        for candidate in re.finditer(r"(?im)^[ \t]*HTM:[^\r\n]*", source):
            if not _HTM_LAYOUT_LINE.fullmatch(candidate.group(0)):
                raise ValueError(f"Malformed HTM: report directive {candidate.group(0)!r}")

        return directives, _HTM_LAYOUT_LINE.sub(self._insert_report, source)

    def _insert_report(self, match):
        report_id = match.group("id")
        if report_id not in self.report_options:
            raise ValueError(f"Unknown deferred report {report_id!r} in HTML-LAYOUT")
        content = (
            ("${" + report_id + "_TABLE}")
            if (self.layout_references.get(report_id, 0) > 1
                or self.report_definitions.get(report_id, 0) > 1)
            else self._table(report_id, self.report_options[report_id])
        )
        return match.group("indent") + content + match.group("suffix")

    def emit_layout(self, block, directives, body, *, macros="macros", extra_values=()):
        source = body
        if "<html" not in source.lower():
            source = self._HTML_SCAFFOLD.format(
                title=escape(directives.get("TITLE", "SQLPathFinder Report")).replace("$", "$$"),
                css_decl="${VG2C_CSS}",
                body=source,
            )
        slots = {}

        def slot(match):
            key = f"VALUE_{len(slots) + 1}"
            slots[key] = match.group(0)
            return "${" + key + "}"

        source = MacroControl.NAMED_PLACEHOLDER_RE.sub(slot, source)
        name = f"html/report_{block.index:03d}.html"
        output = directives.get("FILE", "report.html")
        instance = block.resolved_options.lookup.get("INSTANCE")
        css = directives.get("CSS")
        kwargs = {
            "output": output,
            "instance": instance,
            "embed_css": directives.get("CSSEMBED", "").upper() in self._TRUE_VALUES,
        }
        slot_values = [
            f"{key!r}: {native_macro_expression(value, macros)}"
            for key, value in slots.items()
        ]
        if slot_values:
            kwargs["values"] = CodeExpr("{" + ", ".join(slot_values) + "}")
        if css:
            kwargs["css_file"] = css
        if extra_values:
            values = slot_values + [f"{key!r}: {value}" for key, value in extra_values]
            kwargs["values"] = CodeExpr("{" + ", ".join(values) + "}")
        call = self._render_layout_call(
            CodeExpr(repr(name)), macros=macros, **kwargs
        )
        return BlockEmission(
            (call,),
            assets=((name, source),),
            external_parameters=self._external_parameters(block, "layout"),
        )

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
