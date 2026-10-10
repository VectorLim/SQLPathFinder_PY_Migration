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
from vg2c.runtime.html import render_html
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

        value = json.loads(self.asset_path(path).read_text(encoding="utf-8"))
        if not isinstance(value, (list, dict)):
            raise ValueError(f"Table option asset must hold a mapping or list: {path}")
        return value

    def sql(self, path, *, reader, output, inputs=None, header=None,
            crosstab=None, node=None, params=None):
        return execute_sql(self.asset_path(path), reader=reader, output=output,
                           workdir=self.workdir, values=self.values, macros=self.macros,
                           inputs=inputs, header=header, crosstab=crosstab,
                           node=node, params=params)

    def html(self, path, *, output, values=None, instance=None,
             css_file=None, embed_css=False):
        slots = self.values if values is None else {**self.values, **values}
        return render_html(self.asset_path(path), output=output,
                           workdir=self.workdir, reports=self.reports, values=slots,
                           macros=self.macros, styles=self.styles,
                           css_file=self.css_file if css_file is None else css_file,
                           embed_css=embed_css, instance=instance)

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
