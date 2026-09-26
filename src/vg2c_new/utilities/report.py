from __future__ import annotations

import csv
import html
import re
import subprocess
import sys
from collections.abc import Callable
from dataclasses import dataclass, field
from pathlib import Path
from typing import TYPE_CHECKING, Any

from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState

_ROW_DELIM = "<\\\\>"
_TRUE_VALUES = frozenset({"Y", "YES", "TRUE", "1"})
_REPORT_TOKEN_RE = re.compile(r"(?im)^([ \t]*)HTMI?C?:([^\r\n]+)[ \t]*$")
_CW_TOKEN_RE = re.compile(r"<<<spf-cw#-ctr>>>", re.IGNORECASE)


@dataclass(slots=True)
class ReportState:
    """Per-execution state shared by the direct-runtime report commands.

    ScriptHost stored this information across SPFGlobals, task instances, temp
    report-spec files and gHTMDelete.  The direct runtime keeps only semantic
    report state here; there is no process-global or class-global execution state.
    """

    instance: str | None = None
    css_file: str | None = None
    styles: dict[str, list[str]] = field(default_factory=dict)
    deferred: dict[str, dict[str, Any]] = field(default_factory=dict)
    created_files: list[Path] = field(default_factory=list)
    window_counter: int = 1
    chart_counter: int = 1

    def remember_file(self, path: Path) -> None:
        resolved = path.resolve(strict=False)
        if resolved not in self.created_files:
            self.created_files.append(resolved)

    def reset(self) -> None:
        self.instance = None
        self.css_file = None
        self.styles.clear()
        self.deferred.clear()
        self.created_files.clear()
        self.window_counter = 1
        self.chart_counter = 1


class ReportUtility(Utility):
    """Portable authority for the current /REPORT command family.

    Source characterization:
    - task routing/lifecycle: ScriptHost SPFSQL3.py HTML*Task classes;
    - report-spec delimiter and CSS/table semantics: SPFUtilities/utils.py
      Save_Report_Spec, Generate_Style_Sheet, Generate_HTML_Report;
    - layout semantics: Create_HTML_Window/Create_HTML_Tab_or_Menu;
    - inline plot semantics: HTMLPyPlotTask/HTMLRPlotTask/HTMLGNUPlotTask.

    Port mode is intentionally mixed by target.  Generic report rendering is an
    amended focused port because the original methods still depend on the giant
    Utilities(SPFGlobals) host, MemTable, app-server distribution, UI viewers and
    shared counters.  Plot commands retain their script semantics while replacing
    those transports with direct local processes.

    The standalone ScriptHost PyPlot_Class/PyGraphingMethods/AutoComm_HTML_Report
    modules remain available to scripts, but they implement specialized plotting
    and AutoCommonality APIs rather than the generic /REPORT task contract, so
    wrapping them here would add an unrelated compatibility layer.
    """

    def __init__(self, runner: Callable[..., Any] = subprocess.run):
        self._runner = runner

    def apply(self, command: Command, state: RuntimeState) -> None:
        target = command.utility_type
        if target == "report.html_run":
            self._html_run(command, state)
        elif target == "report.html_defer":
            self._html_defer(command, state)
        elif target == "report.html_layout":
            self._html_layout(command, state)
        elif target == "report.html_tab_layout":
            self._html_tab_or_menu(command, state, mode="TAB")
        elif target == "report.html_menu_layout":
            self._html_tab_or_menu(command, state, mode="MENU")
        elif target == "report.delete":
            self._delete(state)
        elif target == "report.js_defer":
            self._js_defer(command, state)
        elif target == "report.js_show":
            self._js_show(command, state)
        elif target in {"report.pyplot", "report.pyplot_show"}:
            self._run_python_plot(command, state)
        elif target in {"report.rplot", "report.rplot_show"}:
            self._run_r_plot(command, state)
        elif target in {"report.gnuplot", "report.gnuplot_show"}:
            self._run_gnuplot(command, state)
        else:
            raise ValueError(f"Unsupported report target {target!r}.")

    def _html_run(self, command: Command, state: RuntimeState) -> None:
        report = state.report
        self._remember_instance(command, state)
        body = state.substitute(command.body)
        spec = _parse_spec(body)
        report_type = _first(spec.get("TYPE")).upper()

        if report_type == "CSS":
            self._apply_css_spec(spec, state, command)
            return
        if report_type in {"HTML", "HTMLI5"}:
            rendered = self._render_table(spec, state, command)
            output = _first(spec.get("OUTPUT-FILE")) or "sqlpathfinder.htm"
            path = _report_path(output, command, state)
            document = _html_document(
                rendered,
                title="SQLPathFinder Report",
                css_decl=self._css_declaration(state, command, spec=spec),
            )
            self._write(path, document, report)
            return
        raise ValueError(f"HTML-RUN requires TYPE CSS, HTML, or HTMLI5; got {report_type!r}.")

    def _html_defer(self, command: Command, state: RuntimeState) -> None:
        report = state.report
        self._remember_instance(command, state)
        report_id = state.substitute(command.option("ID") or "").strip()
        if not report_id:
            raise ValueError("HTML-DEFER requires /ID.")
        body = state.substitute(command.body)
        report.deferred[report_id.upper()] = {
            "kind": "html",
            "body": body,
            "spec": _parse_spec(body),
        }

    def _html_layout(self, command: Command, state: RuntimeState) -> None:
        report = state.report
        self._remember_instance(command, state)
        template = state.substitute(command.body)
        directives, body = _split_directives(template)

        def replace_report(match: re.Match[str]) -> str:
            indent, report_id = match.groups()
            payload = report.deferred.get(report_id.strip().upper())
            if payload is None:
                return ""
            if payload.get("kind") == "html":
                rendered = self._render_table(payload["spec"], state, command)
            else:
                rendered = self._render_deferred_js(payload)
            return _indent(rendered, indent)

        body = _REPORT_TOKEN_RE.sub(replace_report, body)
        css_decl = self._css_declaration(state, command, directives=directives)
        title = directives.get("TITLE") or "SQLPathFinder Report"
        if "<html" not in body.lower():
            body = _html_document(body, title=title, css_decl=css_decl)
        elif css_decl:
            body = _inject_before_head_end(body, css_decl)

        output = directives.get("FILE") or "report.html"
        if output.lower().startswith("email:"):
            output = self._email_fallback_filename(report)
        path = _report_path(output, command, state)
        self._write(path, body, report)
        report.window_counter += 1

    def _html_tab_or_menu(self, command: Command, state: RuntimeState, *, mode: str) -> None:
        """Portable replacement for missing legacy tab/menu template files.

        The original Create_HTML_Tab_or_Menu reads tab_template_v2.htm or
        menu_template.htm from the ScriptHost installation.  Those template files
        are not present in the vendored source.  Preserve the current directives
        and resulting navigable HTML without reconstructing that installation.
        """
        report = state.report
        self._remember_instance(command, state)
        directives, _ = _split_directives(state.substitute(command.body))
        output = directives.get("FILE") or f"{mode.lower()}_layout.html"
        title = directives.get("TITLE") or directives.get("WINTITLE") or "SQLPathFinder Report"

        if mode == "TAB":
            labels, urls = self._tab_items(directives, command, state)
            items = "\n".join(
                f'<li><a href="{html.escape(url, quote=True)}">{html.escape(label)}</a></li>'
                for label, url in zip(labels, urls, strict=True)
            )
            extra = directives.get("DIV", "").replace("<cr>", "\n")
            body = f'<ul class="spf-tabs">\n{items}\n</ul>\n{extra}'
        else:
            menu_tree, menu_url = self._menu_items(directives, command, state)
            body = (
                '<nav class="spf-menu">\n'
                f"{menu_tree}\n"
                "</nav>\n"
                + (
                    f'<iframe src="{html.escape(menu_url, quote=True)}" title="{html.escape(title)}"></iframe>'
                    if menu_url
                    else ""
                )
            )

        path = _report_path(output, command, state)
        self._write(path, _html_document(body, title=title, css_decl=""), report)

    def _tab_items(
        self, directives: dict[str, str], command: Command, state: RuntimeState
    ) -> tuple[list[str], list[str]]:
        source = directives.get("IN", "").strip()
        if source:
            path = _report_path(source, command, state)
            if not path.exists():
                raise FileNotFoundError(path)
            delimiter = "\t" if path.suffix.lower() in {".tab", ".tsv"} else ","
            with path.open("r", encoding="utf-8-sig", newline="") as handle:
                rows = list(csv.DictReader(handle, delimiter=delimiter))
            labels: list[str] = []
            urls: list[str] = []
            for row in rows:
                lowered = {str(k).lower(): v for k, v in row.items() if k}
                label = str(lowered.get("label") or "").strip()
                url = str(lowered.get("url") or "").strip()
                if label and url:
                    labels.append(label)
                    urls.append(url)
            if not labels:
                raise ValueError("HTML-TAB-LAYOUT input must contain label,url rows.")
            return labels, urls

        li = directives.get("LI", "").replace("<cr>", "\n")
        urls_text = directives.get("URL", "").replace("<cr>", "\n")
        labels = re.findall(r"<li[^>]*>(.*?)</li>", li, flags=re.IGNORECASE | re.DOTALL)
        urls = [
            item.strip().strip("'\"")
            for item in re.split(r"[,\r\n]+", urls_text)
            if item.strip()
        ]
        if labels and len(labels) == len(urls):
            return [_strip_tags(label) for label in labels], urls
        if li:
            return [_strip_tags(li)], [urls[0] if urls else "#"]
        return [], []

    def _menu_items(
        self, directives: dict[str, str], command: Command, state: RuntimeState
    ) -> tuple[str, str]:
        source = directives.get("IN", "").strip()
        if source:
            path = _report_path(source, command, state)
            if not path.exists():
                raise FileNotFoundError(path)
            text = path.read_text(encoding="utf-8", errors="replace").replace("\r\n", "\n")
            htm_pos = text.find("htm=")
            menu_pos = text.find("menu=")
            if htm_pos < 0 or menu_pos <= htm_pos:
                raise ValueError("HTML-MENU-LAYOUT input requires ordered htm= and menu= tokens.")
            return text[menu_pos + 5 :].strip(), text[htm_pos + 4 : menu_pos].strip()
        return (
            directives.get("TREE", "").replace("<cr>", "\n"),
            directives.get("URL", "").replace("<cr>", "\n"),
        )

    def _delete(self, state: RuntimeState) -> None:
        report = state.report
        for path in tuple(report.created_files):
            try:
                if path.is_file():
                    path.unlink()
            except OSError:
                # Historical HTML-DELETE is cleanup. A stale/missing output must not
                # turn cleanup into a new job failure.
                pass
        report.reset()

    def _js_defer(self, command: Command, state: RuntimeState) -> None:
        self._remember_instance(command, state)
        report_id = state.substitute(command.option("ID") or "").strip()
        if not report_id:
            raise ValueError("HTML-JS-DEFER requires /ID.")
        # ScriptHost's defer branch writes the chart specification verbatim and
        # leaves generation to the later report/layout phase.
        state.report.deferred[report_id.upper()] = {
            "kind": "js",
            "body": state.substitute(command.body),
        }

    def _js_show(self, command: Command, state: RuntimeState) -> None:
        """Render an explicit JavaScript payload without restoring the jqx host stack.

        ijs_Generate_Chart historically turns an SQLPathFinder chart option table
        into jqx JavaScript using installation assets that are not in this repo.
        For direct JS source, preserve show semantics locally.  Structured legacy
        chart specs fail clearly rather than being silently mis-rendered.
        """
        self._remember_instance(command, state)
        body = state.substitute(command.body).strip()
        if _looks_like_report_spec(body):
            raise RuntimeError(
                "HTML-JS-SHOW structured jqx chart specs require the legacy jqx "
                "template/assets, which are not present in the vendored ScriptHost source."
            )
        output = _report_path("sqlpathfinder.htm", command, state)
        payload = f'<script type="text/javascript">\n{body}\n</script>'
        self._write(
            output,
            _html_document(payload, title="SQLPathFinder Chart", css_decl=""),
            state.report,
        )
        state.report.chart_counter += 1

    @staticmethod
    def _render_deferred_js(payload: dict[str, Any]) -> str:
        body = str(payload.get("body") or "").strip()
        if _looks_like_report_spec(body):
            raise RuntimeError(
                "Deferred structured jqx chart specs require the legacy jqx "
                "template/assets, which are not present in the vendored ScriptHost source."
            )
        return f'<script type="text/javascript">\n{body}\n</script>'

    def _run_python_plot(self, command: Command, state: RuntimeState) -> None:
        script = self._plot_script(command, state)
        self._runner(
            [sys.executable, "-c", script],
            cwd=working_directory_for(command.option("WORKDIR"), state),
            check=True,
        )

    def _run_r_plot(self, command: Command, state: RuntimeState) -> None:
        script = self._plot_script(command, state)
        self._runner(
            ["Rscript", "-e", script],
            cwd=working_directory_for(command.option("WORKDIR"), state),
            check=True,
        )

    def _run_gnuplot(self, command: Command, state: RuntimeState) -> None:
        script = self._plot_script(command, state)
        self._runner(
            ["gnuplot"],
            input=script,
            text=True,
            cwd=working_directory_for(command.option("WORKDIR"), state),
            check=True,
        )

    @staticmethod
    def _plot_script(command: Command, state: RuntimeState) -> str:
        script = state.substitute(command.body)
        if not script.strip():
            raise ValueError(f"{command.command_type} contains no plot script.")
        if _CW_TOKEN_RE.search(script):
            script = _CW_TOKEN_RE.sub(str(state.report.chart_counter), script)
            state.report.chart_counter += 1
        return script

    def _remember_instance(self, command: Command, state: RuntimeState) -> None:
        instance = command.option("INSTANCE")
        if instance is not None and instance.strip():
            state.report.instance = state.substitute(instance).strip()

    def _apply_css_spec(
        self, spec: dict[str, str | list[str]], state: RuntimeState, command: Command
    ) -> None:
        report = state.report
        css_file = _first(spec.get("CSS"))
        if css_file:
            report.css_file = css_file

        for row in _iter_rows(state.substitute(command.body)):
            if len(row) >= 3 and row[0].upper() == "FORMAT" and row[1]:
                report.styles[row[1]] = [value for value in row[2:] if value]

        if report.css_file:
            path = _report_path(report.css_file, command, state)
            self._write(path, _build_css(report.styles), report)

    def _css_declaration(
        self,
        state: RuntimeState,
        command: Command,
        *,
        directives: dict[str, str] | None = None,
        spec: dict[str, str | list[str]] | None = None,
    ) -> str:
        directives = directives or {}
        requested = directives.get("CSS") or (_first(spec.get("CSS")) if spec else "")
        css_file = requested or state.report.css_file or ""
        embed = (directives.get("CSSEMBED") or "").strip().upper() in _TRUE_VALUES
        if not css_file:
            css = _build_css(state.report.styles)
            return f'<style type="text/css">\n{css}\n</style>' if css else ""

        path = _report_path(css_file, command, state)
        if embed:
            css = (
                path.read_text(encoding="utf-8", errors="replace")
                if path.exists()
                else _build_css(state.report.styles)
            )
            return f'<style type="text/css">\n{css}\n</style>' if css else ""
        return f'<link rel="stylesheet" type="text/css" href="{html.escape(css_file, quote=True)}" />'

    def _render_table(
        self,
        spec: dict[str, str | list[str]],
        state: RuntimeState,
        command: Command,
    ) -> str:
        columns = _as_list(spec.get("COLUMN-DATA"))
        headers = _as_list(spec.get("COLUMN-HEADERS"))
        if not headers:
            headers = columns
        alignments = _as_list(spec.get("COLUMN-ALIGNMENT"))
        if len(alignments) < len(columns):
            alignments.extend(["middle-left"] * (len(columns) - len(alignments)))

        rows = self._load_rows(_first(spec.get("INPUT-FILE")), state, command)
        out = ['<table class="tblin">', *("<COL>" for _ in columns), "<thead>", "<tr id='colhdr'>"]
        out.extend(f"<th>{html.escape(value)}</th>" for value in headers)
        out.extend(["</tr>", "</thead>"])

        for index, row in enumerate(rows):
            cell_class = "tblin" if index % 2 == 0 else "alt"
            out.append("<tr>")
            lowered = {str(key).lower(): value for key, value in row.items() if key}
            for column_index, column in enumerate(columns):
                value = _format_cell(column, lowered.get(column.lower(), ""))
                vertical, horizontal = _alignment(alignments[column_index])
                out.append(
                    f'<td class="{cell_class}" '
                    f'style="vertical-align:{vertical};text-align:{horizontal};">{value}</td>'
                )
            out.append("</tr>")

        out.extend(["<tfoot>", "</tfoot>", "</table>"])
        top = _first(spec.get("AT-TOP-OF-REPORT"))
        if top:
            out.insert(0, f'<p class="at-top-of-report">\n{html.escape(top)}</p>')
        return "\n".join(out)

    @staticmethod
    def _load_rows(
        raw_path: str, state: RuntimeState, command: Command
    ) -> list[dict[str, str]]:
        if not raw_path:
            return []
        path = _report_path(raw_path, command, state)
        if not path.is_file():
            return []
        suffix = path.suffix.lower()
        delimiter = "\t" if suffix in {".tab", ".tsv", ".hive-tab", ".hive-sequence"} else ","
        with path.open("r", encoding="utf-8-sig", errors="replace", newline="") as handle:
            return [dict(row) for row in csv.DictReader(handle, delimiter=delimiter)]

    @staticmethod
    def _write(path: Path, content: str, report: ReportState) -> None:
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_text(content, encoding="utf-8")
        report.remember_file(path)

    @staticmethod
    def _email_fallback_filename(report: ReportState) -> str:
        fallback = "report.html"
        for payload in report.deferred.values():
            spec = payload.get("spec")
            if isinstance(spec, dict):
                output = _first(spec.get("OUTPUT-FILE"))
                if output:
                    fallback = output
                    break
        fallback = fallback.lower()
        return f"{report.instance}_{fallback}" if report.instance else fallback


def _iter_rows(template: str):
    for line in template.splitlines():
        if not line.strip() or _ROW_DELIM not in line:
            continue
        yield [part.strip() for part in line.split(_ROW_DELIM)]


def _parse_spec(template: str) -> dict[str, str | list[str]]:
    values: dict[str, str | list[str]] = {}
    for parts in _iter_rows(template):
        if len(parts) < 2:
            continue
        key = parts[0].strip().upper()
        if key == "TYPE" and parts[1].strip().upper() == "KEY":
            continue
        candidates = parts[2:] if parts[1] == "" else parts[1:]
        nonempty = [item for item in candidates if item]
        if not nonempty:
            values[key] = ""
        elif len(nonempty) == 1:
            values[key] = nonempty[0]
        else:
            values[key] = nonempty
    return values


def _first(value: str | list[str] | None) -> str:
    if value is None:
        return ""
    if isinstance(value, list):
        return value[0] if value else ""
    return str(value)


def _as_list(value: str | list[str] | None) -> list[str]:
    if value is None:
        return []
    return list(value) if isinstance(value, list) else [str(value)]


def _split_directives(template: str) -> tuple[dict[str, str], str]:
    directives: dict[str, str] = {}
    body: list[str] = []
    for line in template.splitlines():
        stripped = line.strip()
        if stripped.startswith(":"):
            key, separator, value = stripped[1:].partition(":")
            if separator:
                directives[key.strip().upper()] = value.strip()
                continue
        body.append(line)
    return directives, "\n".join(body)


def _report_path(value: str, command: Command, state: RuntimeState) -> Path:
    return resolve_path(value, state, base=working_directory_for(command.option("WORKDIR"), state))


def _alignment(value: str) -> tuple[str, str]:
    parts = value.lower().split("-", 1)
    if len(parts) == 2:
        return parts[0] or "middle", parts[1] or "left"
    return "middle", parts[0] if parts and parts[0] else "left"


def _format_cell(column: str, value: Any) -> str:
    if value is None:
        return "&nbsp;"
    text = str(value).strip()
    if not text or text.lower() == "nan":
        return "&nbsp;"
    if text.endswith("%"):
        return html.escape(text)
    lower = column.lower()
    if "ce%" in lower or "percent" in lower:
        try:
            return f"{float(text) * 100:.2f}%"
        except ValueError:
            pass
    return html.escape(text)


def _build_css(styles: dict[str, list[str]]) -> str:
    selectors = {
        "COLUMN-BORDER": "table.tblin, td.tblin, th, td.alt",
        "COLUMN-HEADERS": "th, #colhdr",
        "COLUMN-DATA": "td.tblin, caption, table.tblin",
        "COLUMN-ALT-ROW": "td.alt",
        "AT-TOP-OF-REPORT": "p.at-top-of-report",
        "AT-BOT-OF-REPORT": "tr.at-bot-of-report, td.at-bot-of-report",
        "AT-TOP-OF-COL1": "p.at-top-of-col1",
        "AT-TOP-OF-COL2": "p.at-top-of-col2",
        "AT-TOP-OF-COL3": "p.at-top-of-col3",
        "JQX-ALL-ICHART-TEXT": (
            ".jqx-chart-axis-text, .jqx-chart-label-text, .jqx-chart-legend-text, "
            ".jqx-chart-axis-description, .jqx-chart-title-text, "
            ".jqx-chart-title-description"
        ),
    }
    blocks: list[str] = []
    for name, declarations in styles.items():
        selector = selectors.get(name.upper(), f".{_css_identifier(name)}")
        formatted: list[str] = []
        for declaration in declarations:
            text = declaration.strip().rstrip(";")
            if ":" not in text:
                continue
            key, value = (part.strip() for part in text.split(":", 1))
            if key.lower() == "font-size" and value.isdigit():
                value = f"{value}px"
            formatted.append(f"     {key}:{value};")
        if formatted:
            blocks.append(f"{selector}\n{{\n" + "\n".join(formatted) + "\n}")
    if styles.get("COLUMN-BORDER"):
        blocks.append("td.tblin,th,td.alt\n{\n      padding:5px;\n}")
        blocks.append("table.tblin\n{\n     caption-side:top;\n}")
    return "\n\n".join(blocks)


def _css_identifier(value: str) -> str:
    return re.sub(r"[^a-zA-Z0-9_-]+", "-", value.strip()).strip("-").lower() or "spf-style"


def _html_document(body: str, *, title: str, css_decl: str) -> str:
    return (
        "<html>\n<head>\n"
        f"<title>{html.escape(title)}</title>\n"
        '<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">\n'
        f"{css_decl}\n"
        "<style type=\"text/css\">\n"
        "table.tblout, td.tblout, tr.tblout { border-width:0; border-collapse:collapse; "
        "border-style:none; text-align:left; vertical-align:top; }\n"
        "td.tblout { padding:10px; }\n"
        "img { vertical-align:top; }\n"
        "a { text-decoration:none; }\n"
        "tr th, tr td { border:1px solid #e6e6e6; }\n"
        "tr th { background-color:#f5f5f5; }\n"
        "</style>\n</head>\n<body>\n"
        f"{body}\n"
        "</body>\n</html>\n"
    )


def _inject_before_head_end(document: str, content: str) -> str:
    match = re.search(r"</head\s*>", document, flags=re.IGNORECASE)
    if match is None:
        return f"{content}\n{document}"
    return document[: match.start()] + content + "\n" + document[match.start() :]


def _indent(value: str, prefix: str) -> str:
    return "\n".join(prefix + line if line else line for line in value.splitlines())


def _strip_tags(value: str) -> str:
    return html.unescape(re.sub(r"<[^>]+>", "", value)).strip()


def _looks_like_report_spec(value: str) -> bool:
    upper = value.upper()
    return _ROW_DELIM in value and (
        "TYPE" + _ROW_DELIM in upper
        or "CHART" + _ROW_DELIM in upper
        or "COLUMN-" in upper
    )
