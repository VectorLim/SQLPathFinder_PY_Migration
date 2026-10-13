"""Conventional templates and deferred CSV rows; no report DSL at execution."""

from __future__ import annotations

from html import escape
import os
from pathlib import Path
import re
from string import Template
import tempfile

from vg2c.runtime.html_format import build_css
from vg2c.runtime.values import job_path, substitute
from vg2c.runtime._html_template import (
    _Slots,
    _identifiers,
    _literal_dollars,
    _validate_slots,
)
from vg2c.runtime._html_reports import (
    CSVReport,
    _ReportTables,
    _columns_from_html,
    _csv_headers,
    _include_report_fragments,
    _literal_declaration_attributes,
    _render_report_tables,
    _rows,
    csv_report,
)

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
    reports = reports or {}
    # Bare Template slots are retained only for known legacy generated forms,
    # never for arbitrary runtime keys (e.g. USD or B in authored HTML).
    legacy_names = ({"VG2C_CSS"} |
                    {f"{name}_ROWS" for name in reports} |
                    {f"{name}_HEADER_{index + 1}" for name, report in reports.items()
                     for index in range(len(report.headers))} |
                    {name for name in (values or {}) if re.fullmatch(r"VALUE_[1-9][0-9]*", name)})
    _validate_slots(_literal_dollars(_literal_declaration_attributes(source), legacy_names))
    source = _include_report_fragments(source, reports)
    _validate_slots(_literal_dollars(_literal_declaration_attributes(source), legacy_names))
    # Parse user-editable data-field/data-pattern attributes before escaping
    # template dollars, so CSV fields such as A$B retain their exact identity.
    source, declarative_used, generated_rows = _render_report_tables(
        source, reports, workdir=workdir, values=values, macros=macros
    )
    source = _literal_dollars(source, legacy_names)
    _validate_slots(source)
    slots = _identifiers(source)
    reserved = {f"{name}_ROWS" for name in reports} | {slot for slot in slots if slot.endswith("_ROWS")} | {"VG2C_CSS"} | set(generated_rows)
    forbidden_values = {name for name in (values or {}) if (
        name == "VG2C_CSS" or name.startswith("VG2C_DYNAMIC_ROWS_") or
        name.endswith(("_ROWS", "_TABLE",)) or
        re.fullmatch(r".+_HEADER_[0-9]+", name))}
    if forbidden_values:
        raise ValueError(f"Caller values collide with renderer-owned HTML slots: {sorted(forbidden_values)}")
    replacements = dict(generated_rows)
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
        used_reports.update(declarative_used)
        used_reports.update(name for name in reports if f"{name}_ROWS" in slots)
    return destination
