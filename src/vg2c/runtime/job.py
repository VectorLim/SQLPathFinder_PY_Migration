"""Per-run binding for direct job operations; no compiler or ambient context."""

from __future__ import annotations

from collections.abc import Mapping
from pathlib import Path, PureWindowsPath

from vg2c.runtime.aed import process_candidates
from vg2c.runtime.append import smart_append
from vg2c.runtime.controls import csv_chunks
from vg2c.runtime.files import (
    copy_file,
    delete_files,
    rename_file,
    row_count,
    run_program,
    wait_file,
    write_file,
)
from vg2c.runtime.html import _write_atomic, csv_report, render_html
from vg2c.runtime.values import job_path, substitute
from vg2c.runtime.macros import MacroStore
from vg2c.runtime.mail import send_mail
from vg2c.runtime.query import execute_sql
from vg2c.runtime.values import read_macro_row, snapshot_values


class JobRuntime:
    """An independent invocation with distinct project assets and output roots."""

    def __init__(self, assets_root, workdir, *, values: Mapping | None = None,
                 environ: Mapping | None = None, initial_macros: Mapping | None = None):
        self.assets_root = Path(assets_root).resolve()
        self.workdir = Path(workdir).resolve()
        self.values = snapshot_values(self.workdir, values=values, environ=environ)
        self.macros = MacroStore(values=self.values, initial=initial_macros)
        self.reports = {}
        self.styles = {}
        self.css_file = None
        self._used_html_reports = set()

    def asset_path(self, path) -> Path:
        path = Path(path)
        if not path.is_absolute() and PureWindowsPath(str(path)).is_absolute():
            raise ValueError("Windows absolute asset paths require a mounted Linux path")
        if path.is_absolute():
            return path
        resolved = (self.assets_root / path).resolve()
        if not resolved.is_relative_to(self.assets_root):
            raise ValueError("Asset path must remain inside the generated project")
        return resolved

    def table_spec(self, path):
        """Load an editable static CSV/SQL option on each invocation."""
        import json

        asset = self.asset_path(path)
        try:
            value = json.loads(asset.read_text(encoding="utf-8"))
        except (OSError, json.JSONDecodeError) as exc:
            raise ValueError(f"Cannot read SQL table option asset {asset}: {exc}") from exc
        if not isinstance(value, (list, dict)):
            raise ValueError(f"SQL table option asset must contain a list or mapping: {asset}")
        if asset.name.endswith(".crosstab.json"):
            if not isinstance(value, dict):
                raise ValueError(f"SQL crosstab asset must be a mapping: {asset}")
            if (not isinstance(value.get("row_keys"), list)
                or not all(isinstance(v, str) and v for v in value["row_keys"])
                or not all(isinstance(value.get(k), str) and value[k]
                           for k in ("header_key", "value_key"))):
                raise ValueError(
                    f"SQL crosstab asset {asset} requires row_keys (string list), "
                    "header_key (string), value_key (string)"
                )
        if asset.name.endswith(".header.json") and (
            not isinstance(value, list)
            or not all(isinstance(v, str) for v in value)
        ):
            raise ValueError(f"SQL header asset must be a list of strings: {asset}")
        return value

    def report_spec(self, path):
        """Read an editable long HTML column schema when the report is defined.

        Source options are kept distinct: CSV fields, presentation labels,
        alignment and output name (utils.py:9324-9378).
        """
        data = self.table_spec(path)
        allowed = {"input_file", "columns", "headers", "alignment", "output_file"}
        if not isinstance(data, dict) or set(data) - allowed:
            raise ValueError(f"Invalid HTML report definition {path}: unexpected fields")
        if not isinstance(data.get("input_file"), str):
            raise ValueError(f"HTML report definition {path} requires input_file string")
        for key in ("columns", "headers", "alignment"):
            if not isinstance(data.get(key), list) or not all(
                isinstance(value, str) for value in data[key]
            ):
                raise ValueError(f"HTML report definition {path} requires {key} string list")
        if data.get("output_file") is not None and not isinstance(data["output_file"], str):
            raise ValueError(f"HTML report definition {path} requires output_file string or null")
        return csv_report(data["input_file"], columns=data["columns"],
                          headers=data["headers"], alignment=data["alignment"],
                          output_file=data.get("output_file"))

    def sql(self, path, *, reader, output, inputs=None, header=None,
            crosstab=None, node=None, params=None,
            pivot_columns=None, pivot_values=None, pivot_duplicate="first",
            pivot_missing="", pivot_dot=False, pivot_sort=None,
            pivot_header_ref=None, pivot_legacy_headers=False):
        return execute_sql(self.asset_path(path), reader=reader, output=output,
                           workdir=self.workdir, values=self.values, macros=self.macros,
                           inputs=inputs, header=header, crosstab=crosstab,
                           node=node, params=params, pivot_columns=pivot_columns,
                           pivot_values=pivot_values, pivot_duplicate=pivot_duplicate,
                           pivot_missing=pivot_missing, pivot_dot=pivot_dot,
                           pivot_sort=pivot_sort, pivot_header_ref=pivot_header_ref,
                           pivot_legacy_headers=pivot_legacy_headers)

    def define_css(self, output, asset):
        """Publish an editable stylesheet when HTML-RUN CSS executes.

        SPFSQL3.py:20095-20098 and SPFUtilities/utils.py:9028-9047.
        """
        source = self.asset_path(asset)
        target = job_path(substitute(str(output), values=self.values, macros=self.macros),
                          self.workdir).resolve()
        if not target.is_relative_to(self.workdir) or target == source:
            raise ValueError(f"CSS output must stay inside the job workdir: {output!r}")
        _write_atomic(target, source.read_text(encoding="utf-8"))
        self.css_file = str(target)
        return target

    def delete_html(self):
        """Remove only used deferred reports (SPFSQL3.py:20244-20250)."""
        for report_id in self._used_html_reports:
            self.reports.pop(report_id, None)
        self._used_html_reports.clear()

    def html(self, path, *, output, values=None, instance=None,
             css_file=None, embed_css=False, reports=None):
        slots = self.values if values is None else {**self.values, **values}
        selected = self.css_file if css_file is None else css_file
        if css_file is not None and not Path(str(css_file)).is_absolute():
            candidate = job_path(substitute(str(css_file), values=slots, macros=self.macros),
                                 self.workdir)
            if candidate.is_file():
                selected = str(candidate)
        return render_html(self.asset_path(path), output=output,
                           workdir=self.workdir, reports=self.reports if reports is None else reports,
                           values=slots, macros=self.macros, styles=self.styles,
                           css_file=selected, embed_css=embed_css, instance=instance,
                           used_reports=self._used_html_reports if reports is None else None)

    def write_file(self, path, template, *, vars=None):
        return write_file(path, template, workdir=self.workdir, values=self.values,
                          macros=self.macros, vars=vars)

    def row_count(self, path):
        return row_count(path, workdir=self.workdir)

    def read_macro_row(self, path):
        return read_macro_row(self.macros.substitute(str(path)), workdir=self.workdir)

    def csv_chunks(self, input_file, chunk_file, chunk_size):
        return csv_chunks(self.macros.substitute(str(input_file)),
                          self.macros.substitute(str(chunk_file)),
                          chunk_size, workdir=self.workdir)

    def copy_file(self, src, dst, *, recurse=False):
        return copy_file(src, dst, workdir=self.workdir, recurse=recurse)

    def rename_file(self, src, dst):
        return rename_file(src, dst, workdir=self.workdir)

    def delete_files(self, paths, *, recurse=False):
        return delete_files(paths, workdir=self.workdir, recurse=recurse)

    def wait_file(self, path, timeout=30, *, interval=5):
        return wait_file(path, timeout, workdir=self.workdir, interval=interval)

    def run_program(self, argv, *, cwd=None, env=None, check=False, exedir=None):
        return run_program(argv, workdir=self.workdir, cwd=cwd, env=env,
                           check=check, exedir=exedir)

    def smart_append(self, destination, source):
        return smart_append(destination, source, workdir=self.workdir)

    def send_mail(self, to, subject, body, attachments=None,
                  from_addr=None, enabled=True):
        return send_mail(to, subject, body, attachments, from_addr, enabled,
                         workdir=self.workdir)

    def process_candidates(self, path, *, config, service_factory, logger=None):
        return process_candidates(path, config=config, workdir=self.workdir,
                                  service_factory=service_factory, logger=logger)
