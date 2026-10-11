"""Conventional templates and deferred CSV rows; no report DSL at execution."""

from __future__ import annotations

from dataclasses import dataclass
from html import escape
from html.parser import HTMLParser
from pathlib import Path
from string import Template
import os
import re
import tempfile

from vg2c.runtime.html_format import build_css, format_cell, parse_alignment
from vg2c.runtime.csv_io import _CsvIO
from vg2c.runtime.values import job_path, substitute


@dataclass(frozen=True)
class CSVReport:
    input_file: str
    columns: tuple[str, ...]
    headers: tuple[str, ...] = ()
    alignment: tuple[str, ...] = ()
    formats: tuple[str, ...] = ()
    output_file: str | None = None


def csv_report(input_file, *, columns, headers=None, alignment=None, formats=None, output_file=None):
    """Capture raw paths/options; CSV data is read only when rendering is reached."""
    if any(formats or ()):
        raise ValueError("Active column formats require validated ScriptHost parity")
    return CSVReport(str(input_file), tuple(columns), tuple(headers or ()),
                     tuple(alignment or ()), tuple(formats or ()), output_file)


def _identifiers(text):
    template = Template(text)
    if not template.is_valid():
        raise ValueError("Malformed HTML template slot; escape a literal dollar as $$")
    return template.get_identifiers()


class _Slots(HTMLParser):
    _VOID = {"area", "base", "br", "col", "embed", "hr", "img", "input", "link", "meta", "param", "source", "track", "wbr"}
    _URL = {"href", "src", "srcset", "action", "formaction", "poster", "data", "background", "xlink:href"}

    def __init__(self):
        super().__init__(convert_charrefs=False)
        self.stack = []

    def handle_starttag(self, tag, attrs):
        raw = self.get_starttag_text()
        if _identifiers(tag) or any(_identifiers(name) for name, _ in attrs):
            raise ValueError("Dynamic tag or attribute names are unsupported")
        if _identifiers(raw) and len({name for name, _ in attrs}) != len(attrs):
            raise ValueError("Dynamic slots in duplicate attributes are unsupported")
        for name, value in attrs:
            slots = _identifiers(value or "")
            if not slots:
                continue
            if (name in self._URL or name == "style" or name.startswith("on")
                    or any(slot.endswith("_ROWS") or slot == "VG2C_CSS" for slot in slots)):
                raise ValueError(f"Unsupported slot in {tag}.{name}")
            match = re.search(r"(?:\s)" + re.escape(name) + r"\s*=\s*(['\"])", raw, re.I)
            if match is None:
                raise ValueError(f"HTML slot in {name} requires a quoted attribute")
        if tag not in self._VOID:
            self.stack.append(tag)

    def handle_startendtag(self, tag, attrs):
        self.handle_starttag(tag, attrs)
        if tag not in self._VOID:
            self.stack.pop()

    def handle_endtag(self, tag):
        if tag in self.stack:
            del self.stack[len(self.stack) - 1 - self.stack[::-1].index(tag):]

    def handle_data(self, data):
        for slot in _identifiers(data):
            if any(tag in {"script", "style"} for tag in self.stack):
                raise ValueError(f"Unsupported dynamic {self.stack[-1]} content")
            if slot.endswith("_ROWS") and (not self.stack or self.stack[-1] != "tbody"):
                raise ValueError(f"Rows slot {slot} must be direct tbody content")
            if slot == "VG2C_CSS" and (not self.stack or self.stack[-1] != "head"):
                raise ValueError("CSS slot must be direct head content")

    def handle_comment(self, data):
        if _identifiers(data):
            raise ValueError("Slots in HTML comments are unsupported")

    def handle_decl(self, data):
        if _identifiers(data):
            raise ValueError("Slots in HTML declarations are unsupported")

    def handle_pi(self, data):
        if _identifiers(data):
            raise ValueError("Slots in HTML processing instructions are unsupported")


def _validate_slots(source):
    _identifiers(source)
    if re.search(r"<\s*/?\s*\$|\$\{[^}]+\}\s*=", source):
        raise ValueError("Dynamic tag or attribute names are unsupported")
    parser = _Slots()
    parser.feed(source)
    parser.close()


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


def _write_atomic(path, content):
    path.parent.mkdir(parents=True, exist_ok=True)
    with tempfile.NamedTemporaryFile(mode="w", encoding="utf-8", dir=path.parent, delete=False) as stream:
        temporary = Path(stream.name)
        stream.write(content)
    try:
        os.replace(temporary, path)
    finally:
        temporary.unlink(missing_ok=True)


def render_html(template_path, *, output, workdir, reports=None, values=None, macros=None,
                styles=None, css_file=None, embed_css=False, instance=None,
                used_reports=None):
    """Read editable assets at layout time and escape data in one Template pass."""
    template_path = job_path(template_path, workdir)
    source = template_path.read_text(encoding="utf-8")
    _validate_slots(source)
    reports = reports or {}
    slots = _identifiers(source)
    reserved = {f"{name}_ROWS" for name in reports} | {slot for slot in slots if slot.endswith("_ROWS")} | {"VG2C_CSS"}
    if reserved.intersection(values or {}):
        raise ValueError("Caller values collide with renderer-owned HTML slots")
    replacements = {}
    mapping = dict(values or {})
    for name, report in reports.items():
        for index, header in enumerate(report.headers):
            key = f"{name}_HEADER_{index + 1}"
            if key in mapping:
                raise ValueError(f"Caller values collide with report header {key}")
            mapping[key] = substitute(header, values=values, macros=macros)
    for slot in slots:
        if slot in reserved:
            continue
        if slot not in mapping:
            raise ValueError(f"Missing HTML slot {slot!r} in {template_path}")
        replacements[slot] = escape(str(mapping[slot]), quote=True)
    for name, report in reports.items():
        slot = f"{name}_ROWS"
        if slot in slots:
            replacements[slot] = _rows(report, workdir=workdir, values=values, macros=macros)
    missing_rows = {slot for slot in slots if slot.endswith("_ROWS")} - replacements.keys()
    if missing_rows:
        raise ValueError(f"Missing CSV reports for {sorted(missing_rows)}")
    if str(output).lower().startswith("email:") or not output:
        output = next((report.output_file for report in reports.values() if report.output_file), "report.html")
        output = (str(instance) + "_" if instance else "") + str(output).lower()
    destination = job_path(substitute(str(output), values=values, macros=macros), workdir)
    if destination.resolve() == template_path.resolve():
        raise ValueError("HTML output cannot overwrite its source template")
    css = ""
    css_copy = None
    if css_file:
        css_path = Path(substitute(str(css_file), values=values, macros=macros))
        if not css_path.is_absolute():
            css_path = template_path.parent / css_path
        if not css_path.is_file():
            raise FileNotFoundError(f"Required HTML stylesheet not found: {css_path}")
        content = css_path.read_text(encoding="utf-8")
        if re.search(r"\$\{\w+\}|<<<", content) or embed_css and "</style" in content.lower():
            raise ValueError("Dynamic or structural CSS is unsupported")
        if embed_css:
            css = f'<style type="text/css">\n{content}\n</style>'
        else:
            css_output = destination.parent / css_path.name
            if css_output.resolve() == destination.resolve():
                raise ValueError("CSS and HTML output paths collide")
            if css_output.resolve() != css_path.resolve():
                css_copy = (css_output, content)
            css = f'<link rel="stylesheet" type="text/css" href="{escape(css_path.name, quote=True)}" />'
    elif styles:
        content = build_css(styles)
        if re.search(r"\$\{\w+\}|<<<", content) or "</style" in content.lower():
            raise ValueError("Dynamic or structural CSS is unsupported")
        css = f'<style type="text/css">\n{content}\n</style>'
    replacements["VG2C_CSS"] = css
    result = Template(source).substitute(replacements)
    if css and "VG2C_CSS" not in slots:
        result, count = re.subn(r"</head\s*>", lambda match: css + "\n" + match.group(0), result, count=1, flags=re.I)
        if not count:
            raise ValueError("CSS requires an HTML head or VG2C_CSS slot")
    if css_copy:
        _write_atomic(*css_copy)
    _write_atomic(destination, result)
    if used_reports is not None:
        used_reports.update(name for name in reports if f"{name}_ROWS" in slots)
    return destination
