"""CSV report declarations and declarative report-table rendering."""

from __future__ import annotations

import csv
from dataclasses import dataclass, replace
from html import escape
from html.parser import HTMLParser
from pathlib import Path
import re

from vg2c.runtime.csv_io import _CsvIO
from vg2c.runtime.html_format import format_cell, parse_alignment
from vg2c.runtime.values import job_path, substitute
from vg2c.runtime._html_template import _Slots, _literal_dollars

@dataclass(frozen=True)
class CSVReport:
    input_file: str
    columns: tuple[str, ...]
    headers: tuple[str, ...] = ()
    alignment: tuple[str, ...] = ()
    formats: tuple[str, ...] = ()
    output_file: str | None = None
    table_template: str | None = None


def csv_report(input_file, *, columns=(), headers=None, alignment=None, formats=None,
               output_file=None, table_template=None):
    """Capture raw paths/options; CSV data is read only when rendering is reached."""
    if any(formats or ()):
        raise ValueError("Active column formats require validated ScriptHost parity")
    return CSVReport(str(input_file), tuple(columns), tuple(headers or ()),
                     tuple(alignment or ()), tuple(formats or ()), output_file,
                     str(table_template) if table_template is not None else None)

def _rows(report, *, workdir, values, macros):
    path = job_path(substitute(report.input_file, values=values, macros=macros), workdir)
    if not path.is_file():
        raise FileNotFoundError(f"Report input CSV does not exist: {path}")
    lines = []
    for index, row in enumerate(_CsvIO(workdir=workdir).iter(str(path))):
        row = {key.lower(): value for key, value in row.items() if key}
        lines.append("<tr>")
        for column_index, column in enumerate(report.columns):
            column = substitute(column, values=values, macros=macros)
            value = row.get(column.lower(), "")
            content = "&nbsp;" if value is None or str(value).strip().lower() in {"", "nan"} else escape(format_cell(column, value), quote=True)
            alignment = substitute(report.alignment[column_index], values=values, macros=macros) if column_index < len(report.alignment) else "middle-left"
            vertical, horizontal = parse_alignment(alignment)
            if vertical not in {"top", "middle", "bottom", "baseline"} or horizontal not in {"left", "center", "right", "justify"}:
                raise ValueError(f"Unsupported column alignment {alignment!r}")
            cell_class = "tblin" if index % 2 == 0 else "alt"
            lines.append(f'<td class="{cell_class}" style="vertical-align:{vertical};text-align:{horizontal};">{content}</td>')
        lines.append("</tr>")
    return "\n".join(lines)



# Only marked tables are interpreted. Unmarked authored HTML is never rewritten.
# Source semantics: SPFUtilities/utils.py:9324-9539 (COLUMN-DATA patterns,
# COLUMN-HEADERS and COLUMN-ALIGNMENT), 14003-14125 (ordered HTML fragments).
_PATTERNS = {"STARTS WITH:", "ENDS WITH:", "CONTAINS:", "STARTS/ENDS WITH (%):"}


class _ReportTables(HTMLParser):
    """Locate marked table cells/body by offsets; retain source formatting."""

    _VOID = _Slots._VOID

    def __init__(self, source):
        super().__init__(convert_charrefs=False)
        self.source = source
        self.starts = [0]
        for match in re.finditer("\n", source):
            self.starts.append(match.end())
        self.stack = []
        self.active = None
        self.pending_th = None
        self.tables = []

    def absolute_offset(self):
        line, column = self.getpos()
        return self.starts[line - 1] + column

    def handle_starttag(self, tag, attrs):
        data = dict(attrs)
        if tag == "table" and "data-report" in data:
            if len(data) != len(attrs):
                raise ValueError("Duplicate declarative report table attributes")
            if self.active is not None:
                raise ValueError("Nested declarative report tables are unsupported")
            if not data["data-report"]:
                raise ValueError("data-report requires a nonempty report ID")
            start = self.absolute_offset()
            self.active = {"name": data["data-report"], "columns": [],
                           "tbody": None, "depth": len(self.stack) + 1,
                           "start": start, "open_end": start + len(self.get_starttag_text())}
        elif self.active is not None and tag == "th" and "thead" in self.stack:
            if self.pending_th is not None:
                raise ValueError("Nested report column declarations are invalid")
            if len(data) != len(attrs):
                raise ValueError("Duplicate HTML report column attributes")
            field = data.get("data-field")
            if not field:
                raise ValueError("Each report <th> requires data-field")
            start = self.absolute_offset()
            self.pending_th = {"start": start, "attrs": data,
                               "depth": len(self.stack) + 1,
                               "open_end": start + len(self.get_starttag_text())}
        elif self.active is not None and tag == "tbody" and len(self.stack) >= self.active["depth"]:
            if self.active["tbody"] is not None:
                raise ValueError("Report table must have exactly one tbody")
            self.active["tbody"] = [self.absolute_offset() + len(self.get_starttag_text()), None]
        if tag not in self._VOID:
            self.stack.append(tag)

    def handle_endtag(self, tag):
        if not self.stack or self.stack[-1] != tag:
            if self.active is not None:
                raise ValueError(f"Malformed declarative report HTML near </{tag}>")
            return
        position = self.absolute_offset()
        if tag == "th" and self.pending_th and len(self.stack) == self.pending_th["depth"]:
            column = self.pending_th
            column["end"] = self.source.find(">", position) + 1
            self.active["columns"].append(column)
            self.pending_th = None
        if tag == "tbody" and self.active and self.active["tbody"] is not None:
            self.active["tbody"][1] = position
        if tag == "table" and self.active and len(self.stack) == self.active["depth"]:
            if self.active["tbody"] is None or self.active["tbody"][1] is None:
                raise ValueError(f"Report {self.active['name']!r} needs a closed tbody")
            if not self.active["columns"]:
                raise ValueError(f"Report {self.active['name']!r} needs <th data-field=...>")
            self.tables.append(self.active)
            self.active = None
        self.stack.pop()


def _literal_declaration_attributes(source):
    """Protect authored report identifiers for pre-render slot validation."""
    if "data-report" not in source.lower():
        return source
    parser = _ReportTables(source)
    parser.feed(source)
    parser.close()
    spans = [(table["start"], table["open_end"]) for table in parser.tables]
    spans += [(column["start"], column["open_end"])
              for table in parser.tables for column in table["columns"]]
    for start, end in sorted(spans, reverse=True):
        source = source[:start] + _literal_dollars(source[start:end], ()) + source[end:]
    return source


def _csv_headers(report, *, workdir, values, macros):
    path = job_path(substitute(report.input_file, values=values, macros=macros), workdir)
    if not path.is_file():
        raise FileNotFoundError(f"Report input CSV does not exist: {path}")
    with path.open(newline="", encoding="utf-8", errors="replace") as stream:
        names = next(csv.reader(stream), None)
    if not names:
        raise ValueError(f"Report input CSV has no header row: {path}")
    folded = [str(name).casefold() for name in names]
    if len(set(folded)) != len(folded):
        raise ValueError(f"Report input CSV has duplicate case-insensitive headers: {path}")
    return names


def _columns_from_html(table, headers, *, values, macros, source):
    fields = {name.casefold(): name for name in headers}
    selected = []
    alignments = []
    edits = []
    for column in table["columns"]:
        attrs = column["attrs"]
        requested = substitute(attrs["data-field"], values=values, macros=macros)
        align = substitute(attrs.get("data-align", "middle-left"), values=values, macros=macros)
        vertical, horizontal = parse_alignment(align)
        if vertical not in {"top", "middle", "bottom", "baseline"} or horizontal not in {"left", "center", "right", "justify"}:
            raise ValueError(f"Unsupported report column alignment {align!r}")
        if requested.upper() in _PATTERNS:
            pattern = attrs.get("data-pattern")
            if not pattern:
                raise ValueError(f"Report pattern {requested!r} requires data-pattern")
            pattern = substitute(pattern, values=values, macros=macros)
            if len(pattern) > 256:
                raise ValueError("Report column pattern is too long")
            if requested.upper() == "STARTS WITH:":
                matches = [name for name in headers if name.casefold().startswith(pattern.casefold())]
            elif requested.upper() == "ENDS WITH:":
                matches = [name for name in headers if name.casefold().endswith(pattern.casefold())]
            elif requested.upper() == "CONTAINS:":
                matches = [name for name in headers if pattern.casefold() in name.casefold()]
            elif "%" not in pattern:
                # SPFUtilities/utils.py:9430-9443: no-percent means starts-with.
                matches = [name for name in headers if name.casefold().startswith(pattern.casefold())]
            else:
                # Literal metacharacters are an intentional safety repair to
                # the unrestricted ScriptHost regex.
                regex = re.compile("^" + re.escape(pattern).replace("%", ".*") + "$", re.I)
                matches = [name for name in headers if regex.fullmatch(name)]
            # Check_Column_Pattern() defaults to capitalized/underscore-expanded
            # display names for matched headers, unless COLUMN-FORMAT overrides.
            new_headers = []
            for actual in matches:
                extras = "".join(
                    f' {key}="{_literal_dollars(escape(str(value or ""), quote=True), ())}"'
                    for key, value in attrs.items() if key not in {"data-field", "data-pattern"}
                )
                label = actual.capitalize().replace("_", " ")
                new_headers.append(
                    f'<th data-field="{escape(actual, quote=True).replace("$", "$$")}"{extras}>{escape(label).replace("$", "$$")}</th>'
                )
            edits.append((column["start"], column["end"], "".join(new_headers)))
            selected.extend(matches)
            alignments.extend([align] * len(matches))
        else:
            actual = fields.get(requested.casefold())
            if actual is None:
                raise ValueError(
                    f"Report {table['name']!r} requests missing CSV field {requested!r}; "
                    f"available fields: {headers!r}"
                )
            selected.append(actual)
            alignments.append(align)
            # data-field and other authored declaration attributes are CSV metadata,
            # not implicit bare Template slots. Keep original tag formatting.
            start, end = column["start"], column["open_end"]
            edits.append((start, end, _literal_dollars(source[start:end], ())))
    return selected, alignments, edits


def _render_report_tables(source, reports, *, workdir, values, macros):
    parser = _ReportTables(source)
    parser.feed(source)
    parser.close()
    if parser.active is not None:
        raise ValueError("Unclosed declarative report table")
    edits = []
    consumed = set()
    generated_rows = {}
    for table in parser.tables:
        name = table["name"]
        if name not in reports:
            raise ValueError(f"Undefined HTML report {name!r}")
        report = reports[name]
        headers = _csv_headers(report, workdir=workdir, values=values, macros=macros)
        columns, alignments, head_edits = _columns_from_html(
            table, headers, values=values, macros=macros, source=source
        )
        edits.extend(head_edits)
        # Protect literal dollars in the table's own declaration as well.
        start, end = table["start"], table["open_end"]
        edits.append((start, end, _literal_dollars(source[start:end], ())))
        lines = _rows(replace(report, columns=tuple(columns),
                              alignment=tuple(alignments)),
                      workdir=workdir, values=values, macros=macros)
        slot = f"VG2C_DYNAMIC_ROWS_{len(generated_rows) + 1}"
        while "${" + slot + "}" in source:
            slot += "_NEXT"
        generated_rows[slot] = lines
        start, end = table["tbody"]
        authored = source[start:end]
        # The marked tbody belongs to the CSV renderer. Preserve nonsemantic
        # whitespace/comments and one legacy row slot; reject content that
        # would otherwise disappear without warning.
        row_slot = re.compile(r"\$(?:\{" + re.escape(name) +
                              r"_ROWS\}|" + re.escape(name) + r"_ROWS\b)")
        markers = list(row_slot.finditer(authored))
        remainder = row_slot.sub("", authored)
        if (len(markers) > 1 or
                re.sub(r"<!--.*?-->", "", remainder, flags=re.S).strip()):
            raise ValueError(
                f"Report {name!r} tbody is renderer-owned; only whitespace, "
                "HTML comments and one matching ROWS slot are supported"
            )
        token = "${" + slot + "}"
        body = row_slot.sub(lambda _: token, authored) if markers else authored + token
        edits.append((start, end, body))
        consumed.add(name)
    for start, end, content in sorted(edits, reverse=True):
        source = source[:start] + content + source[end:]
    return source, consumed, generated_rows


def _include_report_fragments(source, reports):
    def fragment(match):
        name = match.group(1)
        report = reports.get(name)
        if report is None:
            raise ValueError(f"Undefined HTML report {name!r}")
        if not report.table_template:
            raise ValueError(f"Report {name!r} has no reusable HTML table fragment")
        path = Path(report.table_template)
        content = path.read_text(encoding="utf-8")
        if re.search(r"\$\{[A-Za-z_][A-Za-z_0-9]*_TABLE\}", content):
            raise ValueError(f"Nested HTML table fragments are unsupported: {path}")
        return content
    return re.sub(r"\$\{([A-Za-z_][A-Za-z_0-9]*)_TABLE\}", fragment, source)
