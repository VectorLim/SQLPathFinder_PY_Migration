from __future__ import annotations

import html
import re
import shutil
import subprocess
import sys
import tempfile
from pathlib import Path
from typing import TYPE_CHECKING

import pandas as pd

from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.report_state import DeferredReport
from vg2c_new.utilities.base import Utility
from vg2c_new.utilities.csv import CsvUtility

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState

_REPORT_DELIMITER = re.compile(r"<\\+>")
_TRUE_VALUES = {"Y", "YES", "TRUE", "1"}
_OUTPUT_MARKER = re.compile(r"(?im)^\s*#SPF-REQUIRED-OUT:\s*(.*?)\s*$")
_INPUT_MARKER = re.compile(r"(?im)^\s*#SPF-REQUIRED-CSV:\s*(.*?)\s*$")
_CHART_COUNTER = re.compile(r"<<<spf-cw#-ctr>>>", re.IGNORECASE)

_HTML_SCAFFOLD = """\
<!doctype html>
<html>
<head>
<meta charset="utf-8">
<title>{title}</title>
{refresh}
{css}
<style>
table.tblout, td.tblout, tr.tblout {{ border:0; border-collapse:collapse; text-align:left; vertical-align:top; }}
td.tblout {{ padding:10px; }}
table.tblin {{ border-collapse:collapse; }}
tr th, tr td {{ border:1px solid #e6e6e6; padding:5px; }}
tr th {{ background:#f5f5f5; }}
img {{ vertical-align:top; max-width:100%; }}
.vg2-chart {{ margin:1rem 0; }}
.vg2-chart svg {{ width:100%; max-width:900px; min-height:260px; overflow:visible; }}
.vg2-chart .axis {{ stroke:currentColor; stroke-width:1; }}
.vg2-chart .series {{ fill:none; stroke:currentColor; stroke-width:2; }}
</style>
</head>
<body>
{body}
</body>
</html>
"""

_CSS_RULES: tuple[tuple[str, str, tuple[str, ...]], ...] = (
    (
        "COLUMN-BORDER",
        "table.tblin, td.tblin, th, td.alt",
        (
            "td.tblin, th, td.alt { padding:5px; }",
            "table.tblin { caption-side:top; }",
        ),
    ),
    ("COLUMN-HEADERS", "th, #colhdr", ()),
    ("COLUMN-DATA", "td.tblin, caption, table.tblin", ("caption { padding-top:5px; }",)),
    ("COLUMN-ALT-ROW", "td.alt", ()),
    ("AT-TOP-OF-REPORT", "p.at-top-of-report", ()),
    (
        "JQX-ALL-ICHART-TEXT",
        ".jqx-chart-axis-text, .jqx-chart-label-text, .jqx-chart-legend-text, "
        ".jqx-chart-axis-description, .jqx-chart-title-text, .jqx-chart-title-description",
        (),
    ),
    ("AT-TOP-OF-COL1", "p.at-top-of-col1", ()),
    ("AT-TOP-OF-COL2", "p.at-top-of-col2", ()),
    ("AT-TOP-OF-COL3", "p.at-top-of-col3", ()),
)


def _base(command: Command, state: RuntimeState) -> Path:
    return working_directory_for(command.option("WORKDIR"), state)


def _path(command: Command, state: RuntimeState, value: str) -> Path:
    return resolve_path(value, state, base=_base(command, state))


def _instance(command: Command, state: RuntimeState) -> str:
    value = command.option("INSTANCE") or state.lookup("SPF_INSTANCE") or "vg2"
    return state.substitute(value).strip() or "vg2"


def _required_option(command: Command, state: RuntimeState, name: str) -> str:
    value = command.option(name)
    if value is None or not state.substitute(value).strip():
        raise ValueError(f"/{name} is required for {command.command_type}.")
    return state.substitute(value).strip()


def _iter_report_rows(template: str) -> list[list[str]]:
    """Amended port of Save_Report_Spec + MemTable.Load_Table_Design parsing.

    Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Save_Report_Spec
    and SPSQL3_py/SPFLib/SPFUtilities/memtable.py :: MemTable.Load_Table_Design.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED PORT.
    Preserved: the literal <\\> report delimiter and Type/Key/COLn row model.
    Amendments: parses directly without a temporary SQLite MemTable.
    Discarded: EANImport compatibility and mutable table-global state.
    """
    rows: list[list[str]] = []
    for line in template.splitlines():
        if not line.strip() or not _REPORT_DELIMITER.search(line):
            continue
        parts = [part.strip() for part in _REPORT_DELIMITER.split(line)]
        if len(parts) < 2:
            continue
        if parts[0].casefold() == "type" and parts[1].casefold() == "key":
            continue
        rows.append(parts)
    return rows


def _options(template: str) -> dict[str, str | list[str]]:
    options: dict[str, str | list[str]] = {}
    for parts in _iter_report_rows(template):
        key = parts[0].upper()
        values = [value for value in (parts[2:] if parts[1] == "" else parts[1:]) if value]
        if not values:
            options[key] = ""
        elif len(values) == 1:
            options[key] = values[0]
        else:
            options[key] = values
    return options


def _first(value: str | list[str] | None, default: str = "") -> str:
    if value is None:
        return default
    if isinstance(value, list):
        return value[0] if value else default
    return value


def _many(value: str | list[str] | None) -> list[str]:
    if value is None:
        return []
    return list(value) if isinstance(value, list) else [value]


def _report_type(template: str) -> str:
    report_type = _first(_options(template).get("TYPE")).upper()
    if not report_type:
        raise ValueError("Report template does not define TYPE.")
    return report_type


def _save_report_spec(path: Path, template: str) -> None:
    """Amended port of ScriptHost Utilities.Save_Report_Spec.

    Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Save_Report_Spec.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED PORT.
    Preserved: persisted report-spec file and conversion of <\\> fields to tabs.
    Amendments: UTF-8 and pathlib; no implicit process CWD/global instance.
    Discarded: COL9 compatibility padding needed only by the historical MemTable loader.
    """
    path.parent.mkdir(parents=True, exist_ok=True)
    normalized = "\n".join(
        _REPORT_DELIMITER.sub("\t", line) for line in template.strip().splitlines()
    )
    path.write_text(normalized + ("\n" if normalized else ""), encoding="utf-8")


def _format_rows(template: str) -> dict[str, list[str]]:
    styles: dict[str, list[str]] = {}
    for row in _iter_report_rows(template):
        if row[0].upper() == "FORMAT" and len(row) >= 3 and row[1]:
            styles[row[1].upper()] = [value for value in row[2:] if value]
    return styles


def _build_css(template: str) -> str:
    """Amended port of ScriptHost Utilities.Generate_Style_Sheet.

    Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Generate_Style_Sheet.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED PORT.
    Preserved: current FORMAT rows and the core report selectors used by HTML layouts.
    Amendments: emits compact standards-based CSS directly from the report spec.
    Discarded: historical browser-specific declarations and copied Intel web assets.
    """
    styles = _format_rows(template)
    blocks: list[str] = []
    for key, selector, extras in _CSS_RULES:
        declarations: list[str] = []
        for declaration in styles.get(key, []):
            item = declaration.strip().rstrip(";")
            if not item:
                continue
            if ":" in item:
                name, value = (part.strip() for part in item.split(":", 1))
                if name.casefold() == "font-size" and value.isdigit():
                    value += "px"
                declarations.append(f"  {name}:{value};")
            else:
                declarations.append(f"  {item};")
        if not declarations:
            continue
        if key == "COLUMN-HEADERS":
            if not any(line.lstrip().startswith("padding-top:") for line in declarations):
                declarations.append("  padding-top:5px;")
            if not any(line.lstrip().startswith("padding-bottom:") for line in declarations):
                declarations.append("  padding-bottom:4px;")
        if key == "JQX-ALL-ICHART-TEXT" and not any(
            line.lstrip().startswith("fill:") for line in declarations
        ):
            declarations.append("  fill:black;")
        blocks.append(f"{selector} {{\n" + "\n".join(declarations) + "\n}")
        blocks.extend(extras)
        if key == "COLUMN-BORDER":
            blocks.append(
                "tr.at-bot-of-report, td.at-bot-of-report {\n" + "\n".join(declarations) + "\n}"
            )
    return "\n\n".join(blocks) + ("\n" if blocks else "")


def _resolve_report_file(
    command: Command, state: RuntimeState, options: dict[str, str | list[str]], key: str
) -> Path:
    raw = _first(options.get(key))
    if not raw:
        raise ValueError(f"Report template does not define {key}.")
    return _path(command, state, raw)


def _render_table_body(command: Command, state: RuntimeState, template: str) -> str:
    """Amended port of ScriptHost Utilities.Generate_HTML_Report body generation.

    Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Generate_HTML_Report
    and Utilities.Process_HTM.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED PORT.
    Preserved: INPUT-FILE, selected columns, display headers, alignment, top text,
    alternating rows, empty cells and percentage presentation used by current specs.
    Amendments: pandas/CsvUtility replaces MemTable and HTML is emitted as UTF-8.
    Discarded: drilldown/dynamic-sort web assets and historical browser script branches.
    """
    options = _options(template)
    source = _resolve_report_file(command, state, options, "INPUT-FILE")
    if not source.exists():
        raise FileNotFoundError(f"HTML report input file not found: {source}")
    frame = CsvUtility.read_dataframe(source)

    requested = _many(options.get("COLUMN-DATA"))
    if requested:
        by_casefold = {str(column).casefold(): str(column) for column in frame.columns}
        columns = [by_casefold.get(column.casefold(), column) for column in requested]
    else:
        columns = [str(column) for column in frame.columns]

    headers = _many(options.get("COLUMN-HEADERS"))
    if len(headers) < len(columns):
        headers.extend(columns[len(headers) :])
    alignments = _many(options.get("COLUMN-ALIGNMENT"))
    if len(alignments) < len(columns):
        alignments.extend(["middle-left"] * (len(columns) - len(alignments)))

    lines: list[str] = []
    top = _many(options.get("AT-TOP-OF-REPORT"))
    if top:
        lines.append('<p class="at-top-of-report">' + " ".join(top) + "</p>")
    lines.append('<table class="tblin">')
    lines.append('<thead><tr id="colhdr">')
    lines.extend(f"<th>{html.escape(header)}</th>" for header in headers[: len(columns)])
    lines.append("</tr></thead><tbody>")
    for row_index, (_, row) in enumerate(frame.iterrows()):
        cell_class = "tblin" if row_index % 2 == 0 else "alt"
        lines.append("<tr>")
        for index, column in enumerate(columns):
            raw = row[column] if column in frame.columns else ""
            value = str(raw).strip()
            if not value:
                rendered = "&nbsp;"
            elif value.endswith("%"):
                rendered = html.escape(value)
            elif "%" in column.casefold() or "percent" in column.casefold():
                try:
                    rendered = f"{float(value) * 100:.2f}%"
                except ValueError:
                    rendered = html.escape(value)
            else:
                rendered = html.escape(value)
            alignment = alignments[index].split("-")
            if len(alignment) >= 2:
                vertical, horizontal = alignment[0], alignment[1]
            else:
                vertical, horizontal = "middle", alignment[0] if alignment else "left"
            lines.append(
                f'<td class="{cell_class}" style="vertical-align:{html.escape(vertical)};'
                f'text-align:{html.escape(horizontal)};">{rendered}</td>'
            )
        lines.append("</tr>")
    lines.append("</tbody><tfoot></tfoot></table>")
    return "\n".join(lines)


def _css_tag(command: Command, state: RuntimeState, css_file: str, embed: bool) -> str:
    if not css_file:
        return ""
    path = _path(command, state, css_file)
    if embed:
        if not path.exists():
            raise FileNotFoundError(f"HTML report CSS file not found: {path}")
        return (
            '<style type="text/css">\n'
            + path.read_text(encoding="utf-8", errors="replace")
            + "\n</style>"
        )
    return f'<link rel="stylesheet" type="text/css" href="{html.escape(css_file, quote=True)}">'


def _standalone_report(command: Command, state: RuntimeState, template: str) -> tuple[Path, str]:
    options = _options(template)
    output = _first(options.get("OUTPUT-FILE"), "SQLPathFinder.htm")
    css = _first(options.get("CSS"))
    body = _render_table_body(command, state, template)
    title = _first(options.get("AT-TOP-OF-REPORT"), "SQLPathFinder Report")
    return _path(command, state, output), _HTML_SCAFFOLD.format(
        title=html.escape(title),
        refresh="",
        css=_css_tag(command, state, css, False),
        body=body,
    )


def _split_layout(template: str) -> tuple[dict[str, str], list[str]]:
    directives: dict[str, str] = {}
    body: list[str] = []
    for line in template.splitlines():
        stripped = line.strip()
        if stripped.startswith(":"):
            key, sep, value = stripped[1:].partition(":")
            if sep:
                directives[key.strip().upper()] = value.strip()
                continue
        body.append(line)
    return directives, body


def _layout_output(
    command: Command, state: RuntimeState, directives: dict[str, str], default: str
) -> Path:
    raw = state.substitute(directives.get("FILE", default)).strip()
    lower = raw.casefold()
    if lower.startswith("email:") or lower.startswith("email-a:"):
        raw = f"{_instance(command, state)}_sqlpathfinder.htm"
    elif lower.startswith(("http://", "https://")):
        raise RuntimeError(
            "HTTP/SharePoint report distribution is not part of the portable direct runtime."
        )
    elif lower.endswith((".ppt", ".pptx")):
        raise RuntimeError("PowerPoint report distribution is intentionally not restored.")
    return _path(command, state, raw)


def _render_deferred(
    command: Command,
    state: RuntimeState,
    reference: str,
    *,
    chart_id: str,
) -> str:
    report = state.report.lookup(reference)
    if report is None:
        return ""
    if report.kind == "js":
        return _render_portable_js_chart(command, state, report.template, chart_id=chart_id)
    report_type = _report_type(report.template)
    if report_type not in {"HTML", "HTMLI5"}:
        raise ValueError(f"Deferred report {reference!r} has unsupported TYPE {report_type!r}.")
    return _render_table_body(command, state, report.template)


def _render_layout(command: Command, state: RuntimeState) -> tuple[Path, str]:
    """Amended port of Create_HTML_Window + Process_HTM.

    Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Create_HTML_Window,
    Utilities.Get_Rpt_Style_Sheet and Utilities.Process_HTM.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED PORT.
    Preserved: ordered HTM/HTMI/HTMIC insertion, IMG elements, title/CSS/refresh,
    local output naming, email-body materialization, and layout-cycle counters.
    Amendments: direct UTF-8 rendering with explicit ReportSession state.
    Discarded: browser launch, Outlook sending, SharePoint/PPT distribution,
    copied jqWidgets/Plotly assets and ScriptHost global report state.
    """
    directives, body_lines = _split_layout(state.substitute(command.body))
    rendered_lines: list[str] = []
    section_index = 0
    for line in body_lines:
        stripped = line.strip()
        upper = stripped.upper()
        matched = False
        for prefix in ("HTMIC:", "HTMI:", "HTM:"):
            if upper.startswith(prefix):
                section_index += 1
                report_id = stripped[len(prefix) :].strip()
                rendered_lines.append(
                    _render_deferred(
                        command,
                        state,
                        report_id,
                        chart_id=f"{state.report.window_counter}_{section_index}",
                    )
                )
                matched = True
                break
        if matched:
            continue
        if upper.startswith("IMG:"):
            value = stripped[len("IMG:") :].strip()
            rendered_lines.append(
                f'<img src="{html.escape(value, quote=True)}" alt="SQLPathFinder image">'
            )
            continue
        if stripped and not stripped.startswith(":"):
            rendered_lines.append(line)

    title = state.substitute(directives.get("TITLE", "SQLPathFinder Report"))
    css_file = state.substitute(directives.get("CSS", ""))
    embed_css = directives.get("CSSEMBED", "").strip().upper() in _TRUE_VALUES
    refresh_value = directives.get("RR", "").strip()
    refresh = ""
    if refresh_value and refresh_value.upper() != "NO":
        refresh = f'<meta http-equiv="refresh" content="{html.escape(refresh_value, quote=True)}">'
    output = _layout_output(command, state, directives, "sqlpathfinder.htm")
    page = _HTML_SCAFFOLD.format(
        title=html.escape(title),
        refresh=refresh,
        css=_css_tag(command, state, css_file, embed_css),
        body="\n".join(rendered_lines),
    )
    return output, page


def _token(template: str, name: str) -> str:
    """Direct port of ScriptHost Utilities.ijs_Extract_Token.

    Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.ijs_Extract_Token.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: DIRECT PORT.
    Preserved: case-insensitive <token>value</token> extraction.
    Amendments: DOTALL permits multi-line portable specs.
    Discarded: task-global abort mutation.
    """
    match = re.search(
        rf"<{re.escape(name)}>(.*?)</{re.escape(name)}>",
        template,
        re.IGNORECASE | re.DOTALL,
    )
    return "" if match is None else match.group(1).strip()


def _sql_list(template: str, marker: str) -> list[str]:
    match = re.search(rf"(?im)^\s*{re.escape(marker)}(.*?)\s*$", template)
    if match is None or not match.group(1).strip():
        return []
    return [value.strip() for value in match.group(1).split(";") if value.strip()]


def _render_portable_js_chart(
    command: Command, state: RuntimeState, template: str, *, chart_id: str
) -> str:
    """Portable rewrite of the current ijs_Gen_JS_Chart data-to-chart semantic.

    Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.ijs_Gen_JS_Chart.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: REWRITE.
    Preserved: CSV input, X/Y series selection, title, requested dimensions, ordered
    deferred embedding and a visual chart result.
    Amendments: self-contained SVG replaces jqWidgets, generated JSON/JS files and
    Intel-hosted web assets so the command is portable on Linux.
    Discarded: legacy jqWidgets option matrix, browser display and SQLPFaaS assets.
    """
    input_name = state.substitute(_token(template, "in-file"))
    if not input_name:
        raise ValueError("HTML-JS report requires an <in-file> token.")
    source = _path(command, state, input_name)
    if not source.exists():
        raise FileNotFoundError(f"HTML-JS input file not found: {source}")
    frame = CsvUtility.read_dataframe(source)
    if not len(frame.columns):
        raise ValueError(f"HTML-JS input file has no columns: {source}")

    by_casefold = {str(column).casefold(): str(column) for column in frame.columns}
    requested_x = state.substitute(_token(template, "x"))
    x_column = by_casefold.get(requested_x.casefold()) if requested_x else None
    if x_column is None:
        x_column = str(frame.columns[0])

    series_candidates = _sql_list(template, "#SQLVAR=")
    series = [
        by_casefold[value.casefold()]
        for value in series_candidates
        if value.casefold() in by_casefold and value.casefold() != x_column.casefold()
    ]
    if not series:
        for column in frame.columns:
            column_name = str(column)
            if column_name.casefold() == x_column.casefold():
                continue
            converted = pd.to_numeric(frame[column_name], errors="coerce")
            if converted.notna().any():
                series.append(column_name)
    if not series:
        raise ValueError("HTML-JS report has no numeric Y series to render.")

    numeric: dict[str, pd.Series] = {
        column: pd.to_numeric(frame[column], errors="coerce") for column in series
    }
    all_values = pd.concat(list(numeric.values()), ignore_index=True).dropna()
    if all_values.empty:
        raise ValueError("HTML-JS report Y series contain no numeric values.")
    y_min = float(all_values.min())
    y_max = float(all_values.max())
    if y_min == y_max:
        y_min -= 0.5
        y_max += 0.5

    width = _positive_int(_token(template, "size-x"), 640)
    height = _positive_int(_token(template, "size-y"), 360)
    left, right, top, bottom = 55.0, 20.0, 35.0, 45.0
    plot_width = max(float(width) - left - right, 1.0)
    plot_height = max(float(height) - top - bottom, 1.0)
    count = max(len(frame), 1)

    def point(index: int, value: float) -> tuple[float, float]:
        x = left + (0.0 if count <= 1 else index * plot_width / (count - 1))
        y = top + (y_max - value) * plot_height / (y_max - y_min)
        return x, y

    polylines: list[str] = []
    legend: list[str] = []
    for series_index, column in enumerate(series):
        points = [
            point(index, float(value))
            for index, value in enumerate(numeric[column])
            if not pd.isna(value)
        ]
        if points:
            encoded = " ".join(f"{x:.2f},{y:.2f}" for x, y in points)
            polylines.append(
                f'<polyline class="series series-{series_index}" points="{encoded}"></polyline>'
            )
        legend.append(f'<span data-series="{series_index}">{html.escape(column)}</span>')

    title = state.substitute(_token(template, "title")) or f"{series[0]} vs {x_column}"
    x_first = "" if frame.empty else html.escape(str(frame.iloc[0][x_column]))
    x_last = "" if frame.empty else html.escape(str(frame.iloc[-1][x_column]))
    return (
        f'<figure id="vg2-chart-{html.escape(chart_id, quote=True)}" class="vg2-chart">'
        f"<figcaption><strong>{html.escape(title)}</strong></figcaption>"
        f'<svg viewBox="0 0 {width} {height}" role="img" '
        f'aria-label="{html.escape(title, quote=True)}">'
        f'<line class="axis" x1="{left}" y1="{top}" x2="{left}" '
        f'y2="{top + plot_height}"></line>'
        f'<line class="axis" x1="{left}" y1="{top + plot_height}" '
        f'x2="{left + plot_width}" y2="{top + plot_height}"></line>'
        + "".join(polylines)
        + f'<text x="{left}" y="{height - 12}" font-size="11">{x_first}</text>'
        + f'<text x="{left + plot_width}" y="{height - 12}" text-anchor="end" '
        f'font-size="11">{x_last}</text>'
        + f'<text x="8" y="{top + 10}" font-size="11">{y_max:g}</text>'
        + f'<text x="8" y="{top + plot_height}" font-size="11">{y_min:g}</text>'
        + '</svg><div class="vg2-chart-legend">'
        + " | ".join(legend)
        + "</div></figure>"
    )


def _positive_int(value: str, default: int) -> int:
    try:
        parsed = int(value)
    except (TypeError, ValueError):
        return default
    return parsed if parsed > 0 else default


def _write_page(path: Path, content: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(content, encoding="utf-8")


def _tab_entries(
    command: Command, state: RuntimeState, directives: dict[str, str]
) -> list[tuple[str, str]]:
    input_name = state.substitute(directives.get("IN", "")).strip()
    if input_name:
        source = _path(command, state, input_name)
        if not source.exists():
            raise FileNotFoundError(f"Tab layout input file not found: {source}")
        frame = CsvUtility.read_dataframe(source)
        lower = {str(column).casefold(): str(column) for column in frame.columns}
        if set(lower) != {"label", "url"} or len(frame.columns) != 2:
            raise ValueError('Tab input file must contain exactly "label" and "url" columns.')
        return [(str(row[lower["label"]]), str(row[lower["url"]])) for _, row in frame.iterrows()]

    li = directives.get("LI", "").replace("<cr>", "\n")
    labels = re.findall(r"<li[^>]*>(.*?)</li>", li, re.IGNORECASE | re.DOTALL)
    urls_text = directives.get("URL", "").replace("<cr>", "\n")
    urls = re.findall(r"['\"]([^'\"]+)['\"]", urls_text)
    if not urls and urls_text.strip():
        urls = [part.strip() for part in urls_text.split(",") if part.strip()]
    return list(zip(labels, urls, strict=False))


def _render_tab_page(
    command: Command, state: RuntimeState, directives: dict[str, str], body: list[str]
) -> str:
    entries = _tab_entries(command, state, directives)
    title = state.substitute(
        directives.get("WINTITLE") or directives.get("TITLE") or "SQLPathFinder Tabs"
    )
    buttons: list[str] = []
    panels: list[str] = []
    for index, (label, url) in enumerate(entries):
        hidden = "" if index == 0 else " hidden"
        buttons.append(
            f'<button type="button" data-tab="tab-{index}" '
            f'onclick="vg2Tab({index})">{html.escape(label)}</button>'
        )
        panels.append(
            f'<section id="tab-{index}" class="vg2-tab"{hidden}>'
            f'<iframe src="{html.escape(url, quote=True)}" '
            f'title="{html.escape(label, quote=True)}"></iframe></section>'
        )
    extra = "\n".join(line for line in body if line.strip())
    page_body = (
        '<nav class="vg2-tabs">'
        + "\n".join(buttons)
        + "</nav>\n"
        + "\n".join(panels)
        + ("\n" + extra if extra else "")
        + """
<script>
function vg2Tab(index) {
  document.querySelectorAll('.vg2-tab').forEach((node, i) => { node.hidden = i !== index; });
}
</script>
<style>.vg2-tab iframe { width:100%; min-height:70vh; border:0; }</style>
"""
    )
    return _HTML_SCAFFOLD.format(title=html.escape(title), refresh="", css="", body=page_body)


def _menu_source(
    command: Command, state: RuntimeState, directives: dict[str, str]
) -> tuple[str, str]:
    url = directives.get("URL", "").replace("<cr>", "\n").strip()
    tree = directives.get("TREE", "").replace("<cr>", "\n").strip()
    input_name = state.substitute(directives.get("IN", "")).strip()
    if input_name:
        source = _path(command, state, input_name)
        if not source.exists():
            raise FileNotFoundError(f"Menu layout input file not found: {source}")
        raw = source.read_text(encoding="utf-8", errors="replace").replace("\r\n", "\r")
        htm_pos = raw.casefold().find("htm=")
        menu_pos = raw.casefold().find("menu=")
        if htm_pos < 0 or menu_pos < 0 or menu_pos <= htm_pos:
            raise ValueError("Menu input file must contain ordered htm= and menu= tokens.")
        url = raw[htm_pos + 4 : menu_pos].strip()
        tree = raw[menu_pos + 5 :].strip()
    return url, tree


def _render_menu_page(
    command: Command, state: RuntimeState, directives: dict[str, str], body: list[str]
) -> str:
    url, tree = _menu_source(command, state, directives)
    title = state.substitute(
        directives.get("WINTITLE") or directives.get("TITLE") or "SQLPathFinder Menu"
    )
    extra = "\n".join(line for line in body if line.strip())
    iframe = (
        f'<iframe src="{html.escape(url, quote=True)}" '
        f'title="{html.escape(title, quote=True)}"></iframe>'
        if url
        else ""
    )
    page_body = (
        '<div class="vg2-menu-layout"><nav class="vg2-menu">'
        + tree
        + '</nav><main class="vg2-menu-content">'
        + iframe
        + "</main></div>"
        + ("\n" + extra if extra else "")
        + "<style>.vg2-menu-layout{display:flex;gap:1rem}"
        ".vg2-menu-content{flex:1}"
        ".vg2-menu-content iframe{width:100%;min-height:70vh;border:0}</style>"
    )
    return _HTML_SCAFFOLD.format(title=html.escape(title), refresh="", css="", body=page_body)


def _replace_chart_counter(state: RuntimeState, script: str) -> str:
    if not _CHART_COUNTER.search(script):
        return script
    value = str(state.report.chart_counter)
    state.report.chart_counter += 1
    return _CHART_COUNTER.sub(value, script)


def _marked_path(
    command: Command, state: RuntimeState, script: str, marker: re.Pattern[str]
) -> Path | None:
    match = marker.search(script)
    if match is None or not match.group(1).strip():
        return None
    return _path(command, state, match.group(1).strip().strip("\"'"))


def _run_temp_script(
    command: Command,
    state: RuntimeState,
    script: str,
    *,
    suffix: str,
    executable: list[str],
) -> None:
    base = _base(command, state)
    base.mkdir(parents=True, exist_ok=True)
    with tempfile.NamedTemporaryFile(
        mode="w",
        suffix=suffix,
        prefix="vg2-report-",
        dir=base,
        encoding="utf-8",
        delete=False,
    ) as handle:
        handle.write(script)
        temp_path = Path(handle.name)
    try:
        subprocess.run([*executable, str(temp_path)], cwd=base, check=True)
    finally:
        temp_path.unlink(missing_ok=True)


class HtmlRunUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of current ScriptHost HTMLRunTask.executeTaskCommand.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: HTMLRunTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Save_Report_Spec,
        Generate_Style_Sheet, Generate_HTML_Report, ijs_Generate_HTML_Report.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: CSS report generation and immediate HTML/HTMLI5 report creation.
        Amendments: direct parsing/rendering; HTMLI5 uses the portable table result.
        Discarded: MemTable, jqWidgets transports, temp task globals and browser launch.
        """
        template = state.substitute(command.body)
        if not template.strip():
            raise ValueError("HTML-RUN report template is empty.")
        report_type = _report_type(template)
        if report_type == "CSS":
            options = _options(template)
            output = _resolve_report_file(command, state, options, "CSS")
            output.parent.mkdir(parents=True, exist_ok=True)
            output.write_text(_build_css(template), encoding="utf-8")
            return
        if report_type in {"HTML", "HTMLI5"}:
            output, page = _standalone_report(command, state, template)
            _write_page(output, page)
            return
        raise ValueError(f"Unsupported HTML-RUN report TYPE {report_type!r}.")


class HtmlDeferUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of current ScriptHost HTMLDeferTask.executeTaskCommand.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: HTMLDeferTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Save_Report_Spec.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: /ID keyed deferred report spec and <instance>_<id>_tmp_.ini artifact.
        Amendments: ReportSession owns lookup/cleanup instead of process globals.
        Discarded: telemetry and MemTable state.
        """
        report_id = _required_option(command, state, "ID")
        template = state.substitute(command.body)
        if not template.strip():
            raise ValueError("HTML-DEFER report template is empty.")
        _report_type(template)
        path = _base(command, state) / f"{_instance(command, state)}_{report_id}_tmp_.ini"
        _save_report_spec(path, template)
        state.report.defer(report_id, DeferredReport("html", template, path))


class HtmlLayoutUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of current ScriptHost HTMLLayoutTask.executeTaskCommand.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: HTMLLayoutTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Create_HTML_Window / Process_HTM.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: ordered layout finalization, output/CSS/title metadata and counter reset.
        Amendments: portable local HTML; email pseudo-target becomes its local body file.
        Discarded: Outlook/SharePoint/PPT distribution, browser GUI and global counters.
        """
        if not command.body.strip():
            raise ValueError("HTML-LAYOUT body is empty.")
        output, page = _render_layout(command, state)
        _write_page(output, page)
        state.report.complete_layout()


class HtmlTabMenuLayoutUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Portable rewrite of current ScriptHost HTMLTaborMenuLayoutTask.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: HTMLTaborMenuLayoutTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Create_HTML_Tab_or_Menu.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: REWRITE.
        Preserved: :FILE/:IN/:TITLE/:LI/:DIV/:URL/:TREE inputs, two-column tab CSV,
        menu htm=/menu= input format and generated tab/menu HTML artifact.
        Amendments: self-contained templates replace ScriptHost template files.
        Discarded: SharePoint distribution and browser-launch behavior.
        """
        directives, body = _split_layout(state.substitute(command.body))
        mode = "menu" if command.utility_type == "report.html_menu_layout" else "tab"
        output = _layout_output(command, state, directives, f"{mode}.html")
        page = (
            _render_menu_page(command, state, directives, body)
            if mode == "menu"
            else _render_tab_page(command, state, directives, body)
        )
        _write_page(output, page)


class HtmlDeleteUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of current ScriptHost HTMLDeleteTask.executeTaskCommand.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: HTMLDeleteTask.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: delete accumulated report-temporary artifacts then clear the list.
        Amendments: only this RuntimeState's explicit ReportSession paths are touched.
        Discarded: process-global gHTMDelete and generic SPFDelete transport helpers.
        """
        for path in tuple(state.report.cleanup_paths):
            if path.is_file():
                path.unlink()
        state.report.clear_artifacts()


class HtmlJsUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Portable rewrite of current ScriptHost HTMLJSTask.executeTaskCommand.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: HTMLJSTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: ijs_Generate_Chart / ijs_Gen_JS_Chart.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: REWRITE.
        Preserved: SHOW standalone output, DEFER-by-ID, chart ordering and CSV chart data.
        Amendments: inline SVG replaces jqWidgets/generated JS asset transport.
        Discarded: hosted Intel JS assets, generated JSON/init files and browser GUI.
        """
        template = state.substitute(command.body)
        if not template.strip():
            raise ValueError("HTML-JS report body is empty.")
        if command.utility_type == "report.js_defer":
            report_id = _required_option(command, state, "ID")
            path = _base(command, state) / f"{_instance(command, state)}_{report_id}_tmp_.ini"
            path.parent.mkdir(parents=True, exist_ok=True)
            path.write_text(template, encoding="utf-8")
            state.report.defer(report_id, DeferredReport("js", template, path))
        else:
            chart = _render_portable_js_chart(
                command,
                state,
                template,
                chart_id=f"{state.report.window_counter}_{state.report.chart_counter}",
            )
            output = _base(command, state) / "sqlpathfinder.htm"
            page = _HTML_SCAFFOLD.format(
                title="SQLPathFinder Chart", refresh="", css="", body=chart
            )
            _write_page(output, page)
        state.report.chart_counter += 1


class HtmlPyPlotUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended portable port of current ScriptHost HTMLPyPlotTask / Run_Python.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: HTMLPyPlotTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Run_Python.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: inline Python execution, <<<spf-cw#-ctr>>> substitution and output marker.
        Amendments: current sys.executable + temporary script in the report working directory.
        Discarded: old Python engines, spfviewer/browser GUI and Windows helper scripts.
        """
        script = _replace_chart_counter(state, state.substitute(command.body))
        if not script.strip():
            raise ValueError("HTML-PYPLOT body is empty.")
        output = _marked_path(command, state, script, _OUTPUT_MARKER)
        _run_temp_script(command, state, script, suffix=".py", executable=[sys.executable])
        if output is not None:
            if not output.exists():
                raise FileNotFoundError(f"HTML-PYPLOT did not create declared output: {output}")
            state.report.track_cleanup(output)


class HtmlRPlotUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended portable port of current ScriptHost HTMLRPlotTask / Run_R.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: HTMLRPlotTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Run_R.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: inline R execution, chart-counter substitution and declared output asset.
        Amendments: direct Rscript replaces Windows Rgui/Linux crossover transport.
        Discarded: viewer GUI, app-server crossover and historical R routing branches.
        """
        script = _replace_chart_counter(state, state.substitute(command.body))
        if not script.strip():
            raise ValueError("HTML-RPLOT body is empty.")
        executable = shutil.which("Rscript")
        if executable is None:
            raise RuntimeError("HTML-RPLOT requires the external 'Rscript' executable.")
        output = _marked_path(command, state, script, _OUTPUT_MARKER)
        _run_temp_script(command, state, script, suffix=".R", executable=[executable])
        if output is not None:
            if not output.exists():
                raise FileNotFoundError(f"HTML-RPLOT did not create declared output: {output}")
            state.report.track_cleanup(output)


class HtmlGnuPlotUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended portable port of current ScriptHost HTMLGNUPlotTask / Run_GNU.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: HTMLGNUPlotTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Run_GNU.
        Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: required CSV/output markers, header-to-1-based-column substitution,
        $spf-in-file$ replacement and GNUPlot script execution.
        Amendments: portable gnuplot executable discovery and temporary script.
        Discarded: wgnuplot.exe, spfviewer, Windows path helpers and SQL prep transport.
        """
        script = state.substitute(command.body)
        input_path = _marked_path(command, state, script, _INPUT_MARKER)
        if input_path is None:
            return
        if not input_path.exists():
            raise FileNotFoundError(f"HTML-GNUPLOT input file not found: {input_path}")
        headers = list(CsvUtility.read_dataframe(input_path, nrows=0).columns)
        if not headers:
            raise ValueError(f"HTML-GNUPLOT input file is empty: {input_path}")
        for match in re.findall(r"\{(.*?)\}", script):
            if match not in headers:
                raise ValueError(f"HTML-GNUPLOT column header not found: {match}")
            script = script.replace("{" + match + "}", str(headers.index(match) + 1))
        script = script.replace("$spf-in-file$", str(input_path))
        executable = shutil.which("gnuplot")
        if executable is None:
            raise RuntimeError("HTML-GNUPLOT requires the external 'gnuplot' executable.")
        output = _marked_path(command, state, script, _OUTPUT_MARKER)
        _run_temp_script(command, state, script, suffix=".gp", executable=[executable])
        if output is not None:
            if not output.exists():
                raise FileNotFoundError(f"HTML-GNUPLOT did not create declared output: {output}")
            state.report.track_cleanup(output)
