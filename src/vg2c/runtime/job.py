"""Per-run binding for direct job operations; no compiler or ambient context."""

from __future__ import annotations

import csv
from collections.abc import Callable, Mapping
from functools import wraps
from pathlib import Path, PureWindowsPath
from typing import Concatenate, ParamSpec, TypeVar, cast

from vg2c.runtime.file_store import input_path
from vg2c.runtime.aed_client import AEDClient, identifier
from vg2c.runtime.environment import DEFAULT_TEST_MODE_EXCLUDED, read_environment
from vg2c.runtime.controls import csv_chunks
from vg2c.runtime.files import (
    copy_file,
    delete_files,
    rename_file,
    row_count,
    run_program,
    smart_append,
    wait_file,
    write_file,
)
from vg2c.runtime.html import _write_atomic, csv_report, render_html
from vg2c.runtime.macros import MacroStore
from vg2c.runtime.mail import send_mail
from vg2c.runtime.query import execute_sql
from vg2c.runtime.values import job_path, read_macro_row, snapshot_values, substitute

P = ParamSpec("P")
R = TypeVar("R")


def csr_lots(path, column, *, workdir, files=None):
    """Return the distinct lot IDs of one CSV column (header matched case-insensitively)."""
    with input_path(path, workdir, files).open(
        encoding="utf-8-sig", newline=""
    ) as stream:
        reader = csv.DictReader(stream, strict=True)
        names = {name.strip().casefold(): name for name in reader.fieldnames or []}
        key = names.get(column.casefold())
        if key is None:
            raise ValueError(f"CSR input {path} has no {column!r} column")
        lots = []
        for row in reader:
            lot = (row.get(key) or "").strip()
            if None in row or not lot:
                raise ValueError(f"CSR input {path} has a row without a lot")
            lots.append(identifier(lot, "CSR lot"))
    return list(dict.fromkeys(lots))


def csr_comment(note, ccb):
    """Build the legacy CSR hold comment; a CCB number is mandatory."""
    if not str(ccb).strip():
        raise ValueError("CSR requests require a CCB number")
    return f"{note}\nCCB: {ccb}"


def operation(
    name: str,
    skipped: Callable[[], object] | None = None,
) -> Callable[
    [Callable[Concatenate["JobRuntime", P], R]],
    Callable[Concatenate["JobRuntime", P], R],
]:
    """Tag a JobRuntime method; skip it when `name` is excluded in test mode."""

    def decorate(
        method: Callable[Concatenate["JobRuntime", P], R],
    ) -> Callable[Concatenate["JobRuntime", P], R]:

        @wraps(method)
        def run(self: "JobRuntime", *args: P.args, **kwargs: P.kwargs) -> R:
            if not self.enabled(name):
                return cast(R, skipped() if skipped else None)
            return method(self, *args, **kwargs)

        setattr(run, "operation", name)
        return run

    return decorate


class JobRuntime:
    """An independent invocation with distinct project assets and output roots."""

    def __init__(
        self,
        assets_root,
        workdir,
        *,
        values: Mapping | None = None,
        environ: Mapping | None = None,
        initial_macros: Mapping | None = None,
        test_mode_excluded=DEFAULT_TEST_MODE_EXCLUDED,
    ):
        self.assets_root = Path(assets_root).resolve()
        self.workdir = Path(workdir).resolve()
        self.environ = (
            read_environment(self.assets_root) if environ is None else dict(environ)
        )
        self.env_mode = self.environ.get("ENV_MODE", "test").strip().lower()
        if self.env_mode not in {"test", "prod"}:
            raise ValueError("ENV_MODE must be test or prod")
        self.environ["ENV_MODE"] = self.env_mode
        self.test_mode_excluded = frozenset(test_mode_excluded)
        self.values = snapshot_values(self.workdir, values=values, environ=self.environ)
        self.macros = MacroStore(values=self.values, initial=initial_macros)
        self.macros["ENV_MODE"] = self.env_mode
        self.files = {}
        self.reports = {}
        self.styles = {}
        self.css_file = None
        self._used_html_reports = set()
        self._aed = None
        self._python_globals = None

    @classmethod
    def from_script(cls, script_file, *, test_mode_excluded=DEFAULT_TEST_MODE_EXCLUDED):
        """Create one fresh run bound to the generated project's own roots."""
        project_dir = Path(script_file).resolve().parent
        return cls(
            assets_root=project_dir,
            workdir=project_dir / "output",
            test_mode_excluded=test_mode_excluded,
        )

    def asset_path(self, path) -> Path:
        path = Path(path)
        if not path.is_absolute() and PureWindowsPath(str(path)).is_absolute():
            raise ValueError(
                "Windows absolute asset paths require a mounted Linux path"
            )
        if path.is_absolute():
            return path
        resolved = (self.assets_root / path).resolve()
        if not resolved.is_relative_to(self.assets_root):
            raise ValueError("Asset path must remain inside the generated project")
        return resolved

    @operation("run_python")
    def run_python(self, path, **names):
        """Run an embedded-Python asset in a namespace shared across all such assets."""
        asset = self.asset_path(path)
        if self._python_globals is None:
            self._python_globals = {
                "__name__": "__main__",
                "job": self,
                "project_dir": self.assets_root,
                "workdir": self.workdir,
            }
        self._python_globals.update(names, __file__=str(asset))
        exec(
            compile(asset.read_text(encoding="utf-8"), str(asset), "exec"),
            self._python_globals,
        )

    def table_spec(self, path):
        """Load an editable static CSV/SQL option on each invocation."""
        import json

        asset = self.asset_path(path)
        try:
            value = json.loads(asset.read_text(encoding="utf-8"))
        except (OSError, json.JSONDecodeError) as exc:
            raise ValueError(
                f"Cannot read SQL table option asset {asset}: {exc}"
            ) from exc
        if not isinstance(value, (list, dict)):
            raise ValueError(
                f"SQL table option asset must contain a list or mapping: {asset}"
            )
        if asset.name.endswith(".crosstab.json"):
            if not isinstance(value, dict):
                raise ValueError(f"SQL crosstab asset must be a mapping: {asset}")
            if (
                not isinstance(value.get("row_keys"), list)
                or not all(isinstance(v, str) and v for v in value["row_keys"])
                or not all(
                    isinstance(value.get(k), str) and value[k]
                    for k in ("header_key", "value_key")
                )
            ):
                raise ValueError(
                    f"SQL crosstab asset {asset} requires row_keys (string list), "
                    "header_key (string), value_key (string)"
                )
        if asset.name.endswith(".header.json") and (
            not isinstance(value, list) or not all(isinstance(v, str) for v in value)
        ):
            raise ValueError(f"SQL header asset must be a list of strings: {asset}")
        return value

    def report(self, template, *, input_file, output_file=None):
        """Register one editable HTML table without a second report schema.

        HTML-DEFER saves the file/spec at execution time (SPFSQL3.py:20129-20150).
        CSV rows are read later, when HTML-LAYOUT renders it.
        """
        return csv_report(
            input_file,
            output_file=output_file,
            table_template=self.asset_path(template),
        )

    @operation("sql")
    def sql(
        self,
        path,
        *,
        output,
        reader=None,
        inputs=None,
        header=None,
        crosstab=None,
        node=None,
        params=None,
        pivot_columns=None,
        pivot_values=None,
        pivot_duplicate="first",
        pivot_missing="",
        pivot_dot=False,
        pivot_sort=None,
        pivot_header_ref=None,
        pivot_legacy_headers=False,
    ):
        return execute_sql(
            self.asset_path(path),
            reader=reader,
            output=output,
            workdir=self.workdir,
            values=self.values,
            macros=self.macros,
            files=self.files,
            inputs=inputs,
            header=header,
            crosstab=crosstab,
            node=node,
            params=params,
            pivot_columns=pivot_columns,
            pivot_values=pivot_values,
            pivot_duplicate=pivot_duplicate,
            pivot_missing=pivot_missing,
            pivot_dot=pivot_dot,
            pivot_sort=pivot_sort,
            pivot_header_ref=pivot_header_ref,
            pivot_legacy_headers=pivot_legacy_headers,
        )

    @operation("define_css")
    def define_css(self, output, asset):
        """Publish an editable stylesheet when HTML-RUN CSS executes.

        SPFSQL3.py:20095-20098 and SPFUtilities/utils.py:9028-9047.
        """
        source = self.asset_path(asset)
        target = job_path(
            substitute(str(output), values=self.values, macros=self.macros),
            self.workdir,
        ).resolve()
        if not target.is_relative_to(self.workdir) or target == source:
            raise ValueError(f"CSS output must stay inside the job workdir: {output!r}")
        _write_atomic(target, source.read_text(encoding="utf-8"))
        self.files.pop(target.resolve(), None)
        self.css_file = str(target)
        return target

    @operation("delete_html")
    def delete_html(self):
        """Remove only used deferred reports (SPFSQL3.py:20244-20250)."""
        for report_id in self._used_html_reports:
            self.reports.pop(report_id, None)
        self._used_html_reports.clear()

    @operation("html")
    def html(
        self,
        path,
        *,
        output,
        values=None,
        instance=None,
        css_file=None,
        embed_css=False,
        reports=None,
    ):
        slots = self.values if values is None else {**self.values, **values}
        selected = self.css_file if css_file is None else css_file
        if css_file is not None and not Path(str(css_file)).is_absolute():
            # Prefer an executed HTML-RUN output, then a project-relative
            # editable asset; render_html still supports template siblings.
            resolved = substitute(str(css_file), values=slots, macros=self.macros)
            candidate = input_path(resolved, self.workdir, self.files)
            if candidate.is_file():
                selected = str(candidate)
            else:
                asset = self.asset_path(resolved)
                if asset.is_file():
                    selected = str(asset)
        return render_html(
            self.asset_path(path),
            output=output,
            workdir=self.workdir,
            reports=self.reports if reports is None else reports,
            values=slots,
            macros=self.macros,
            styles=self.styles,
            css_file=selected,
            embed_css=embed_css,
            instance=instance,
            used_reports=self._used_html_reports if reports is None else None,
            files=self.files,
        )

    @operation("write_file")
    def write_file(self, path, template, *, vars=None):
        return write_file(
            path,
            template,
            workdir=self.workdir,
            values=self.values,
            macros=self.macros,
            vars=vars,
            files=self.files,
        )

    @operation("load_file")
    def load_file(self, path, asset):
        """Activate an editable WRITE-FILE asset without changing it."""
        source = self.asset_path(asset)
        if not source.is_file():
            raise FileNotFoundError(str(source))
        self.files[job_path(path, self.workdir).resolve()] = source

    @operation("update_file")
    def update_file(self, path, asset):
        """Expand the original template afresh at its source execution point."""
        content = self.asset_path(asset).read_text(encoding="utf-8")
        content = substitute(content, values=self.values, macros=self.macros)
        self.files[job_path(path, self.workdir).resolve()] = content.encode("utf-8")

    def read_file(self, path):
        """Read this run's content by its logical filename."""
        return input_path(path, self.workdir, self.files).read_text(encoding="utf-8")

    def row_count(self, path):
        return row_count(path, workdir=self.workdir, files=self.files)

    def read_macro_row(self, path):
        return read_macro_row(
            self.macros.substitute(str(path)), workdir=self.workdir, files=self.files
        )

    def csv_chunks(self, input_file, chunk_file, chunk_size):
        return csv_chunks(
            self.macros.substitute(str(input_file)),
            self.macros.substitute(str(chunk_file)),
            chunk_size,
            workdir=self.workdir,
            files=self.files,
        )

    @operation("copy_file")
    def copy_file(self, src, dst, *, recurse=False):
        return copy_file(
            src, dst, workdir=self.workdir, recurse=recurse, files=self.files
        )

    @operation("rename_file")
    def rename_file(self, src, dst):
        return rename_file(src, dst, workdir=self.workdir, files=self.files)

    @operation("delete_files")
    def delete_files(self, paths, *, recurse=False):
        return delete_files(
            paths, workdir=self.workdir, recurse=recurse, files=self.files
        )

    @operation("wait_file")
    def wait_file(self, path, timeout=30, *, interval=5):
        return wait_file(
            path, timeout, workdir=self.workdir, interval=interval, files=self.files
        )

    @operation("run_program")
    def run_program(self, argv, *, cwd=None, env=None, check=False, exedir=None):
        return run_program(
            argv,
            workdir=self.workdir,
            cwd=cwd,
            env=env,
            check=check,
            exedir=exedir,
            files=self.files,
        )

    @operation("smart_append")
    def smart_append(self, destination, source):
        return smart_append(destination, source, workdir=self.workdir, files=self.files)

    @operation("email")
    def send_mail(
        self, to, subject, body, attachments=None, from_addr=None, enabled=True
    ):
        return send_mail(
            to,
            subject,
            body,
            attachments,
            from_addr,
            enabled,
            workdir=self.workdir,
            env_mode=self.env_mode,
            test_mode_excluded=self.test_mode_excluded,
            files=self.files,
        )

    def enabled(self, name):
        """False when `name` is in TEST_MODE_EXCLUDED and ENV_MODE is test."""
        return self.env_mode == "prod" or name not in self.test_mode_excluded

    @operation("mms")
    def submit_mms(self, src, dst, *, recurse=False):
        return copy_file(
            src, dst, workdir=self.workdir, recurse=recurse, files=self.files
        )

    @operation("csr", skipped=list)
    def csr_lots(self, path, column):
        """Distinct lot IDs of `column` in this run's `path` CSV."""
        return csr_lots(
            self.macros.substitute(str(path)),
            column,
            workdir=self.workdir,
            files=self.files,
        )

    def csr_comment(self, note, ccb):
        return csr_comment(note, ccb)

    @operation("csr")
    def aed(self, facility) -> AEDClient:
        self._aed = AEDClient(facility, environ=self.environ)
        return self._aed

    @property
    def csr_status(self):
        """Outcome of the latest `aed()` client; a failed call stops the run first."""
        accepted = getattr(self._aed, "accepted", 0)
        if not accepted:
            return "No CSR AED action was executed"
        return (
            f"AED accepted {accepted} request(s); "
            "resulting state has not been read back"
        )
