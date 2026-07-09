# Auto-generated Python script from VG2
"""Pipeline implementation."""


from __future__ import annotations
from abc import ABC
from contextlib import contextmanager
from dataclasses import dataclass
from email.message import EmailMessage
from functools import partial
from pathlib import Path
from typing import Any
from typing import Any, Callable
from typing import Any, ClassVar
from typing import Any, ContextManager
from typing import Any, Iterator
from typing import Callable
from typing import Iterator, Protocol
import csv
import inspect
import os
import pandas
import pandas as pd
import re
import shutil
import smtplib
import subprocess

_CLASS_SIG_RE = re.compile(r"^(\s*class\s+\w+)\(.*\):\s*$")

def _strip_embed_artifacts(source: str, class_name: str) -> str:
    lines = source.split("\n")

    while lines and lines[0].lstrip().startswith("@"):
        lines.pop(0)

    if not lines:
        return ""

    lines[0] = _CLASS_SIG_RE.sub(r"\1:", lines[0])
    lines[0] = lines[0].replace("(UtilitySpec):", ":")
    lines[0] = lines[0].replace(f"({class_name}, UtilitySpec):", f"({class_name}):")

    lines = [line for line in lines if not line.lstrip().startswith("handles =")]

    return "\n".join(lines).rstrip()

class UtilitySpec(ABC):
    """Base contract for all embeddable utilities."""

    utility_name: ClassVar[str]
    handles: ClassVar[tuple[Kind, ...]] = ()
    _registry: ClassVar[dict[str, type[UtilitySpec]]] = {}
    _kind_handlers: ClassVar[dict[Kind, type[UtilitySpec]]] = {}

    def __init_subclass__(cls, **kwargs: Any) -> None:
        super().__init_subclass__(**kwargs)

        raw_name = cls.__dict__.get("utility_name")
        if not isinstance(raw_name, str):
            return

        name = raw_name.strip()
        if not name:
            raise ValueError(f"{cls.__name__}: utility_name must be non-empty")

        existing = UtilitySpec._registry.get(name)
        if existing is not None and existing is not cls:
            raise ValueError(f"duplicate utility_name: {name}")

        UtilitySpec._registry[name] = cls

        for handled_kind in tuple(getattr(cls, "handles", ())):
            owner = UtilitySpec._kind_handlers.get(handled_kind)
            if owner is not None and owner is not cls:
                raise ValueError(
                    "duplicate handler for "
                    f"{handled_kind}: {owner.__name__} and {cls.__name__}"
                )
            UtilitySpec._kind_handlers[handled_kind] = cls

    @classmethod
    def get_source(cls) -> str:
        custom = getattr(cls, "__vg2c_source__", None)
        if custom is not None:
            return str(custom).rstrip()

        source = inspect.getsource(cls)
        return _strip_embed_artifacts(source, cls.__name__)

    @staticmethod
    def emit_block(
        block: Any, dispatched: Any
    ) -> tuple[str, str] | None:
        return None

PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")

NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")

class RawExpr:
    source: str

def strip_quotes(value: str) -> str:
    value = value.strip()
    if len(value) >= 2 and value[0] == value[-1] and value[0] in {'"', "'"}:
        return value[1:-1]
    return value

def option_to_python_expr(value: str | None) -> str:
    if value is None:
        return "None"
    return placeholders_to_python_expr(strip_quotes(value))

def resolve_output_path(block: Any) -> str:
    csv_value = block.resolved_options.lookup.get("CSV")
    if csv_value:
        return strip_quotes(csv_value)

    write_file_value = block.resolved_options.lookup.get("WRITE-FILE")
    if write_file_value:
        candidate = strip_quotes(write_file_value)
        if candidate.upper() not in {"Y", "N"}:
            return candidate

    suffix = "txt" if block.kind is Kind.WRITE_FILE else "csv"
    return f"step_{block.parsed.index:04d}.{suffix}"

def _normalize_macro_name(raw: str) -> str:
    name = raw.strip()
    if name.startswith("<<<") and name.endswith(">>>"):
        name = name[3:-3]
    return name.strip().upper()

def macro_token_to_python_expr(raw: str) -> str:
    return f'ctx.macro.named("{_normalize_macro_name(raw)}")'

def placeholders_to_python_expr(text: str) -> str:
    if not text:
        return repr("")

    parts: list[str] = []
    cursor = 0

    for match in PLACEHOLDER_RE.finditer(text):
        literal = text[cursor : match.start()]
        if literal:
            parts.append(repr(literal))

        named = match.group(1)
        if named is not None:
            parts.append(macro_token_to_python_expr(named))
        else:
            parts.append("ctx.macro.positional()")

        cursor = match.end()

    tail = text[cursor:]
    if tail:
        parts.append(repr(tail))

    if not parts:
        return repr(text)
    if len(parts) == 1:
        return parts[0]
    return " + ".join(parts)

def _render_value(value: Any) -> str:
    if isinstance(value, RawExpr):
        return value.source
    return repr(value)

def render_method_call(
    utility_name: str,
    method_name: str,
    *,
    args: tuple[Any, ...] = (),
    kwargs: dict[str, Any] | None = None,
) -> str:
    """Render a Python method-call expression for the generated script."""
    receiver = "ctx" if utility_name == "ctx" else f"ctx.{utility_name}"
    parts: list[str] = [_render_value(arg) for arg in args]
    for key, value in (kwargs or {}).items():
        parts.append(f"{key}={_render_value(value)}")
    return f"{receiver}.{method_name}({', '.join(parts)})"

def _step_name(block: Any, suffix: str) -> str:
    return f"step_{block.parsed.index:04d}_{suffix}"

def _emit_step_source(name: str, body_lines: list[str]) -> tuple[str, str]:
    lines = [f"def {name}(ctx) -> None:"]
    if body_lines:
        for body_line in body_lines:
            for line in body_line.split("\n"):
                if line.strip():
                    lines.append(f"    {line}")
                else:
                    lines.append("")
    else:
        lines.append("    pass")
    return "\n".join(lines), f"{name}(ctx)"

class CrosstabUtility:
    utility_name = "crosstab"
    TOKEN = "CrossTab->[["
    TOKEN_RE = re.compile(
        r"(?P<prefix>,?)\s*CrossTab->\[\[\s*(?P<alias>[A-Za-z_][A-Za-z0-9_]*)\s*,\s*(?P<instance>[^;\]]+)\s*;\s*:(?P<mode>[YyNn])\s*\]\](?P<suffix>,?)"
    )

    @classmethod
    def has_token(cls, value: str | None) -> bool:
        return bool(value and cls.TOKEN in value)

    @staticmethod
    def _extract_selected_columns_by_alias(sql: str) -> dict[str, set[str]]:
        by_alias: dict[str, set[str]] = {}
        match = re.search(
            r"\bSELECT\b(?P<select_part>.*?)\bFROM\b",
            sql,
            flags=re.IGNORECASE | re.DOTALL,
        )
        if not match:
            return by_alias

        select_part = match.group("select_part")
        col_ref_re = re.compile(
            r"\b([A-Za-z_][A-Za-z0-9_]*)\s*\.\s*(?:\[([^\]]+)\]|\"([^\"]+)\"|([A-Za-z_][A-Za-z0-9_]*))"
        )
        for col_match in col_ref_re.finditer(select_part):
            alias = col_match.group(1).lower()
            col_name = col_match.group(2) or col_match.group(3) or col_match.group(4)
            if not col_name:
                continue
            by_alias.setdefault(alias, set()).add(col_name.lower())

        return by_alias

    @staticmethod
    def extract_options(block) -> dict[str, Any] | None:
        ctrow = strip_quotes(block.resolved_options.lookup.get("CTROW", ""))
        ctheader = strip_quotes(block.resolved_options.lookup.get("CTHEADER", ""))
        ctvalue = strip_quotes(block.resolved_options.lookup.get("CTVALUE", ""))
        if not (ctrow and ctheader and ctvalue):
            return None
        row_keys = [c.strip() for c in ctrow.split(",") if c.strip()]
        return {
            "row_keys": row_keys,
            "header_key": ctheader,
            "value_key": ctvalue,
        }

    @classmethod
    def substitute_sql(
        cls,
        sql: str,
        alias_columns_lookup: Callable[[str], list[str]] | None = None,
    ) -> str:
        if alias_columns_lookup is None or not cls.has_token(sql):
            return sql

        selected_by_alias = cls._extract_selected_columns_by_alias(sql)

        def _replace(match: re.Match[str]) -> str:
            prefix = match.group("prefix")
            suffix = match.group("suffix")
            alias = match.group("alias").strip()
            mode = match.group("mode").upper()
            all_cols = alias_columns_lookup(alias)
            selected = selected_by_alias.get(alias.lower(), set())
            dynamic_cols = [c for c in all_cols if c.lower() not in selected]

            if not dynamic_cols:
                return ""

            if mode == "N":
                body = ",".join(dynamic_cols)
                return f"{prefix}{body}{suffix}"

            body = "\n         ,".join(
                f"{alias}.[{c}] AS [{c}]" for c in dynamic_cols
            )
            return f"{prefix}{body}{suffix}"

        return cls.TOKEN_RE.sub(_replace, sql)

    def apply(
        self,
        rows: pd.DataFrame,
        row_keys: list[str],
        header_key: str,
        value_key: str,
    ) -> Any:
        """Pivot row-oriented data into SQLPathFinder-style crosstab output."""
        if rows.empty or not row_keys or not header_key or not value_key:
            return pd.DataFrame(columns=row_keys)

        ci_lookup = {str(c).casefold(): c for c in rows.columns}
        rename_map = {
            ci_lookup[k.casefold()]: k for k in (*row_keys, header_key, value_key)
        }
        df = rows.rename(columns=rename_map)

        df = df[df[header_key].notna() & (df[header_key].astype(str) != "")]
        if df.empty:
            return pd.DataFrame(columns=row_keys)

        result = (
            df.groupby([*row_keys, header_key], dropna=False)[value_key]
            .first()
            .unstack(header_key, fill_value="")
            .reset_index()
            .rename_axis(columns=None)
        )
        result.columns = [str(col).lower() for col in result.columns]
        return result

class CsvIO:
    """Read and write CSV files relative to ``cwd``."""

    utility_name = "csv_io"

    # ------------------------------------------------------------------
    # Read
    # ------------------------------------------------------------------

    def iter(self, name: str) -> Iterator[dict[str, str]]:
        """Yield each data row as a dict keyed by header names."""
        path = Path(name)
        with path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.DictReader(fh)
            yield from reader

    def read(self, name: str) -> Path:
        """Return the resolved Path (used when a downstream step needs a file reference)."""
        return Path(name).resolve()

    def row_count(self, name: str) -> int:
        """Count data rows (excludes header); 0 if file missing."""
        path = Path(name)
        if not path.exists():
            return 0
        with path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.reader(fh)
            next(reader, None)  # skip header
            return sum(1 for _ in reader)

    def iter_chunks(
        self, input_name: str, chunk_name: str, chunk_size: int
    ) -> Iterator[Path]:
        """Stream *input_name* in fixed-size chunks, materializing each batch to *chunk_name*.

        Yields the chunk file path once per batch. The header of *input_name* is
        re-written at the top of each chunk so downstream readers can use it.
        """
        if chunk_size <= 0:
            chunk_size = 1
        in_path = Path(input_name)
        out_path = Path(chunk_name)
        out_path.parent.mkdir(parents=True, exist_ok=True)
        with in_path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.reader(fh)
            header = next(reader, None)
            batch: list[list[str]] = []
            for row in reader:
                batch.append(row)
                if len(batch) >= chunk_size:
                    self._write_chunk(out_path, header, batch)
                    yield out_path
                    batch = []
            if batch:
                self._write_chunk(out_path, header, batch)
                yield out_path

    @staticmethod
    def _write_chunk(
        path: Path, header: list[str] | None, rows: list[list[str]]
    ) -> None:
        with path.open("w", newline="", encoding="utf-8") as fh:
            writer = csv.writer(fh)
            if header is not None:
                writer.writerow(header)
            writer.writerows(rows)

    # ------------------------------------------------------------------
    # Write
    # ------------------------------------------------------------------

    def write(self, name: str, content: Any, header: list[str] | None = None) -> None:
        """Write *content* to a CSV file.

        *content* can be:
        - a list of dicts  -> written via DictWriter (keys as header)
        - a list of lists  -> written via writer (optional *header* for first row)
        - a string         -> written as raw text (no CSV encoding)
        - a Path           -> copied verbatim
        """
        path = Path(name)
        path.parent.mkdir(parents=True, exist_ok=True)

        if isinstance(content, pandas.DataFrame):
            if header is not None:
                content = content.reindex(columns=header)
            content.to_csv(path, index=False, encoding="utf-8")
            return

        if isinstance(content, str):
            path.write_text(content, encoding="utf-8")
            return

        if isinstance(content, Path):
            import shutil

            shutil.copy2(content, path)
            return

        rows = list(content) if content is not None else []
        if not rows:
            if header is not None:
                with path.open("w", newline="", encoding="utf-8") as fh:
                    writer = csv.writer(fh)
                    writer.writerow(header)
            else:
                path.write_text("", encoding="utf-8")
            return

        with path.open("w", newline="", encoding="utf-8") as fh:
            if isinstance(rows[0], dict):
                fieldnames = header if header is not None else list(rows[0].keys())
                writer = csv.DictWriter(
                    fh, fieldnames=fieldnames, extrasaction="ignore"
                )
                writer.writeheader()
                writer.writerows(rows)
            else:
                writer_plain = csv.writer(fh)
                if header:
                    writer_plain.writerow(header)
                    if rows[0] == header:
                        rows = rows[1:]
                writer_plain.writerows(rows)

class PipelineContext:
    """Single runtime context object for generated scripts."""

    utility_name = "ctx"

    def __init__(self) -> None:
        registry = getattr(type(self), "_registry", None)
        if isinstance(registry, dict) and registry:
            candidates = list(registry.items())
        else:
            candidates = []
            for obj in globals().values():
                if not isinstance(obj, type):
                    continue
                utility_name = getattr(obj, "utility_name", None)
                if isinstance(utility_name, str):
                    candidates.append((utility_name, obj))

        for utility_name, utility_cls in candidates:
            if utility_name == self.utility_name:
                continue
            try:
                setattr(self, utility_name, utility_cls())
            except TypeError:
                continue

    def get_method(self, utility_cls: type[UtilitySpec], method_func: Callable) -> Any:
        """Get a method from a utility class."""
        if not hasattr(self, utility_cls.utility_name):
            raise AttributeError(
                f"Utility '{utility_cls.utility_name}' not found in PipelineContext."
            )

        utility_instance = getattr(self, utility_cls.utility_name)
        method = getattr(utility_instance, method_func.__name__, None)
        if method is None:
            raise AttributeError(
                f"Method '{method_func.__name__}' not found in utility '{utility_cls.utility_name}'."
            )
        return method

    def macro_scope(self, row: dict[str, str] | None = None) -> ContextManager[None]:
        return self.macro.scope(row=row)

    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        self.macro.write_file(path, template, vars=vars)

    def _read_datasyncx(self, sql: str, reader: Any):
        result = reader.read(site="KM", query=sql)
        result.columns = [col.lower() for col in result.columns]
        return result

    def run_query(
        self,
        sql,
        output: str,
        reader: Any,
        inputs: list[str] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
    ):
        sql = self.macro.substitute_sql(sql)

        if hasattr(reader, "execute"):
            result = reader.execute(sql, inputs or [])
        else:
            result = self._read_datasyncx(sql, reader)

        if crosstab:
            result = self.crosstab.apply(
                result,
                row_keys=crosstab["row_keys"],
                header_key=crosstab["header_key"],
                value_key=crosstab["value_key"],
            )

        self.csv_io.write(output, result, header=header)

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)

class ExternalProcess:
    """Thin wrapper around subprocess.run."""

    utility_name = "external"

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        if not text:
            return []
        return text.split()

    @staticmethod
    def emit_block(block, dispatched) -> tuple[str, str] | None:
        argv = ExternalProcess._utility_argv(block)
        if not argv:
            return _emit_step_source(
                _step_name(block, "external"),
                ["pass  # TODO: empty external utility command"],
            )

        stmt = ExternalProcess._emit_run(argv)
        return _emit_step_source(_step_name(block, "external"), [stmt])

    @staticmethod
    def _emit_run(argv: list[str]) -> str:
        expr_items = [option_to_python_expr(token) for token in argv]
        argv_expr = RawExpr("[" + ", ".join(expr_items) + "]")
        return render_method_call(
            "external",
            "run",
            kwargs={"argv": argv_expr},
        )

    @staticmethod
    def _resolve_exedir() -> str:
        """Return the SPF tools directory from env var VG2C_EXEDIR."""
        return os.environ.get("VG2C_EXEDIR", "")

    @staticmethod
    def _resolve_path(path: str) -> str:
        return os.path.normpath(path)

    @classmethod
    def _resolve_argv(cls, argv: list[str]) -> list[str]:
        """Substitute @EXEDIR@ tokens and normalise path-like arguments."""
        exedir = cls._resolve_exedir()
        return [
            cls._resolve_path(a) if os.sep in a else a
            for a in (arg.replace("@EXEDIR@", exedir) for arg in argv)
        ]

    def run(
        self,
        argv: list[str],
        cwd: str | Path | None = None,
        env: dict | None = None,
        check: bool = False,
    ) -> int:
        resolved = self._resolve_argv(argv)
        first = resolved[0] if resolved else ""
        use_shell = Path(first).suffix.lower() in {".bat", ".va", ".exe"}
        result = subprocess.run(
            resolved,
            cwd=str(cwd) if cwd else None,
            env=env,
            check=check,
            shell=use_shell,
        )
        return result.returncode

class FileSystemOps:

    utility_name = "fs_ops"

    @staticmethod
    def emit_block(block, dispatched) -> tuple[str, str] | None:
        if block.kind is Kind.FS_COPY:
            return FileSystemOps._emit_copy_block(block)
        if block.kind is Kind.FS_DELETE:
            return FileSystemOps._emit_delete_block(block)

        stmt = render_method_call(
            "ctx",
            "write_file",
            kwargs={
                "path": resolve_output_path(block),
                "template": block.resolved_body,
            },
        )
        return _emit_step_source(_step_name(block, "write_file"), [stmt])

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        if not text:
            return []
        return text.split()

    @staticmethod
    def _emit_copy_block(block) -> tuple[str, str]:
        argv = FileSystemOps._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "robocopy" in basename:
            stmt = FileSystemOps._emit_robocopy(argv)
        elif "spfcopy" in basename:
            stmt = FileSystemOps._emit_spf_copy(argv)
        else:
            return _emit_step_source(
                _step_name(block, "fs_copy"),
                ["pass  # TODO: unsupported FS copy utility command"],
            )
        return _emit_step_source(_step_name(block, "fs_copy"), [stmt])

    @staticmethod
    def _emit_delete_block(block) -> tuple[str, str]:
        argv = FileSystemOps._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "spfdelete" not in basename:
            return _emit_step_source(
                _step_name(block, "fs_delete"),
                ["pass  # TODO: unsupported FS delete utility command"],
            )
        stmt = FileSystemOps._emit_spf_delete(argv)
        return _emit_step_source(_step_name(block, "fs_delete"), [stmt])

    @staticmethod
    def _emit_robocopy(argv: list[str]) -> str:
        # RoboCopy.va arg layout: <file_name> <source_dir> <dest_dir> [...]
        file_name = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        source_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        dest_dir = option_to_python_expr(argv[3]) if len(argv) > 3 else repr(".")
        src_expr = RawExpr(f"str(Path({source_dir}) / {file_name})")
        dst_expr = RawExpr(dest_dir)
        return render_method_call(
            "fs_ops",
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    @staticmethod
    def _emit_spf_copy(argv: list[str]) -> str:
        # SPFCopy.bat arg layout: <source_path> <dest_dir> [recurse]
        src = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        dst_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        src_expr = RawExpr(src)
        dst_expr = RawExpr(f"str(Path({dst_dir}) / Path({src}).name)")
        return render_method_call(
            "fs_ops",
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    @staticmethod
    def _emit_spf_delete(argv: list[str]) -> str:
        raw = strip_quotes(argv[1]) if len(argv) > 1 else ""
        items = [p.strip() for p in raw.split(",") if p.strip()]
        paths_expr = RawExpr(
            "[" + ", ".join(option_to_python_expr(p) for p in items) + "]"
        )
        return render_method_call(
            "fs_ops",
            "delete",
            kwargs={"paths": paths_expr},
        )

    def copy(self, src: str | Path, dst: str | Path, recurse: bool = False) -> None:
        src, dst = Path(src), Path(dst)
        dst.parent.mkdir(parents=True, exist_ok=True)
        if src.is_dir():
            shutil.copytree(src, dst, dirs_exist_ok=True)
        else:
            shutil.copy2(src, dst)

    def rename(self, src: str | Path, dst: str | Path) -> None:
        Path(src).replace(Path(dst))

    def delete(self, paths: list[str | Path], recurse: bool = False) -> None:
        for p in paths:
            path = Path(p)
            if path.is_dir():
                if recurse:
                    shutil.rmtree(path, ignore_errors=True)
            else:
                path.unlink(missing_ok=True)

class HtmlReport:
    """Utility for generating HTML report files."""

    utility_name = "html_report"

    def __init__(self) -> None:
        self.styles: dict[str, list[str]] = {}
        self.css_file: str | None = None
        self.deferred_reports: dict[str, dict[str, Any]] = {}
        self.instance: str | None = None
        self.prompt_text: str | None = None
        self.app_server_default: str | None = None

    @staticmethod
    def emit_block(block, dispatched) -> tuple[str, str] | None:
        report_type = block.resolved_options.lookup.get("REPORT", "").upper().strip()
        if report_type == "HTML-RUN":
            return HtmlReport._emit_html_run(block)
        elif report_type == "HTML-LAYOUT":
            return HtmlReport._emit_html_layout(block)
        elif report_type == "HTML-DELETE":
            return HtmlReport._emit_html_delete(block)
        elif report_type == "HTML-DEFER":
            return HtmlReport._emit_html_defer(block)
        return None

    @staticmethod
    def _emit_method(
        block,
        method: str,
        option_keys: list[str],
        *,
        args: tuple[RawExpr, ...] = (),
        include_template: bool = False,
    ) -> tuple[str, str]:
        kwargs = {}
        for key in option_keys:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = RawExpr(
                    option_to_python_expr(val)
                )
        if include_template:
            kwargs["template"] = block.resolved_body
        stmt = render_method_call("html_report", method, args=args, kwargs=kwargs)
        return _emit_step_source(_step_name(block, "html_report"), [stmt])

    @staticmethod
    def _emit_html_defer(block) -> tuple[str, str]:
        return HtmlReport._emit_method(
            block,
            "defer",
            ["INSTANCE", "ID", "PROMPT-TEXT", "APP_SERVER_DEFAULT"],
            include_template=True,
        )

    @staticmethod
    def _emit_html_run(block) -> tuple[str, str]:
        return HtmlReport._emit_method(
            block,
            "run",
            ["INSTANCE", "PROMPT-TEXT", "APP_SERVER_DEFAULT"],
            include_template=True,
        )

    @staticmethod
    def _emit_html_layout(block) -> tuple[str, str]:
        return HtmlReport._emit_method(
            block,
            "layout",
            [
                "OUTLOOK",
                "INSTANCE",
                "JSON-ONLY",
                "CHART-INSTANCE",
                "APP_SERVER_DEFAULT",
            ],
            args=(RawExpr("ctx"),),
            include_template=True,
        )

    @staticmethod
    def _emit_html_delete(block) -> tuple[str, str]:
        return HtmlReport._emit_method(block, "delete", ["INSTANCE"])

    @staticmethod
    def _parse_template_rows(template: str | None) -> list[list[str]]:
        rows: list[list[str]] = []
        for line in (template or "").splitlines():
            if not line.strip():
                continue
            rows.append([part.strip() for part in line.split("<\\>")])
        return rows

    @staticmethod
    def _extract_options(rows: list[list[str]]) -> dict[str, Any]:
        options: dict[str, Any] = {}
        for parts in rows:
            if len(parts) < 2:
                continue
            key = parts[0].upper()
            if parts[1] == "":
                val_list = [part for part in parts[2:] if part != ""]
            else:
                val_list = [part for part in parts[1:] if part != ""]

            if len(val_list) == 0:
                options[key] = ""
            elif len(val_list) == 1:
                options[key] = val_list[0]
            else:
                options[key] = val_list
        return options

    @staticmethod
    def _as_list(val: Any) -> list[str]:
        if val is None:
            return []
        if isinstance(val, list):
            return val
        return [str(val)]

    def _ensure_parsed_payload(
        self, report: dict[str, Any]
    ) -> tuple[list[list[str]], dict[str, Any]]:
        rows = report.get("parsed_rows")
        if not isinstance(rows, list):
            rows = self._parse_template_rows(report.get("template"))
            report["parsed_rows"] = rows

        options = report.get("options")
        if not isinstance(options, dict):
            options = self._extract_options(rows)
            report["options"] = options
        return rows, options

    @staticmethod
    def _parse_layout_template(template: str) -> tuple[dict[str, str], str]:
        directives: dict[str, str] = {}
        html_lines: list[str] = []
        for line in template.splitlines():
            if line.startswith(":"):
                parts = line[1:].split(":", 1)
                if len(parts) == 2:
                    directives[parts[0].strip().upper()] = parts[1].strip()
                    continue
            html_lines.append(line)
        return directives, "\n".join(html_lines)

    @staticmethod
    def _resolve_csv_path(raw_path: str, ctx: Any) -> Path:
        if not raw_path:
            return Path("")
        if ctx and hasattr(ctx, "macro"):
            macro = ctx.macro
            if hasattr(macro, "resolve_file_path"):
                return macro.resolve_file_path(raw_path)
            resolved = macro.substitute_sql(raw_path)
        else:
            resolved = raw_path
        path = Path(resolved)
        if path.is_file() and path.exists():
            return path
        if path.is_absolute():
            rel_path = Path(path.name)
            if rel_path.is_file() and rel_path.exists():
                return rel_path
        return path

    @staticmethod
    def _iter_csv_rows(csv_path: Path, ctx: Any) -> list[dict[str, Any]]:
        rows: list[dict[str, Any]] = []
        if not csv_path.is_file() or not csv_path.exists():
            return rows

        if ctx and hasattr(ctx, "csv_io") and hasattr(ctx.csv_io, "iter"):
            for row in ctx.csv_io.iter(str(csv_path)):
                normalized_row = {k.lower(): v for k, v in row.items() if k}
                rows.append(normalized_row)
            return rows

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
            for parts in self._parse_template_rows(template):
                if len(parts) < 2:
                    continue
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
        parsed_rows = self._parse_template_rows(template)
        self.deferred_reports[id] = {
            "instance": instance,
            "prompt_text": prompt_text,
            "app_server_default": app_server_default,
            "template": template,
            "parsed_rows": parsed_rows,
            "options": self._extract_options(parsed_rows),
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

        css_rules: list[dict[str, Any]] = [
            {
                "name": "COLUMN-BORDER",
                "template": "table.tblin, td.tblin, th, td.alt \n{{\n{decls}\n}}",
                "extras": [
                    "td.tblin,th,td.alt\n{\n      padding:5px;\n}",
                    "  table.tblin \n{\n     caption-side:top;\n}",
                ],
                "tail_template": "tr.at-bot-of-report, td.at-bot-of-report {{\n{decls}\n\n}}",
            },
            {
                "name": "Column-Headers",
                "template": "th, #colhdr\n{{\n{decls}\n}}",
                "defaults": [
                    ("padding-top", "     padding-top:5px;"),
                    ("padding-bottom", "     padding-bottom:4px;"),
                ],
            },
            {
                "name": "Column-Data",
                "template": "td.tblin, caption, table.tblin \n{{\n{decls}\n}}",
                "extras": ["  caption {padding-top:5px;}"],
            },
            {
                "name": "Column-Alt-Row",
                "template": "td.alt\n{{\n{decls}\n}}",
            },
            {
                "name": "At-Top-of-Report",
                "template": "p.at-top-of-report\n{{\n{decls}\n}}",
            },
            {
                "name": "JQX-All-IChart-Text",
                "template": ".jqx-chart-axis-text, .jqx-chart-label-text, .jqx-chart-legend-text, .jqx-chart-axis-description, .jqx-chart-title-text, .jqx-chart-title-description {{\n{decls}\n}}",
                "defaults": [("fill", "     fill:black;")],
            },
        ]

        for rule in css_rules:
            decls = get_decls(rule["name"])
            if not decls:
                continue

            extra_decls = list(decls)
            for token, default_decl in rule.get("defaults", []):
                if not any(token in decl for decl in extra_decls):
                    extra_decls.append(default_decl)

            css_blocks.append(rule["template"].format(decls="\n".join(extra_decls)))
            for extra_block in rule.get("extras", []):
                css_blocks.append(extra_block)

            tail_template = rule.get("tail_template")
            if tail_template:
                css_blocks.append(tail_template.format(decls="\n".join(decls)))

        col_rules = {
            "At-Top-of-Col1": "p.at-top-of-col1 {{\n{decls}\n}}",
            "At-Top-of-Col2": "p.at-top-of-col2 {{\n{decls}\n}}",
            "At-Top-of-Col3": "p.at-top-of-col3 {{\n{decls}\n}}",
        }
        for format_name, selector in col_rules.items():
            decls = get_decls(format_name)
            if decls:
                css_blocks.append(selector.format(decls="\n".join(decls)))

        return "\n\n".join(css_blocks)

    def _render_report(self, report_id: str, ctx: Any) -> str:
        if report_id not in self.deferred_reports:
            return ""

        report = self.deferred_reports[report_id]
        _, options = self._ensure_parsed_payload(report)

        cols = self._as_list(options.get("COLUMN-DATA"))
        headers = self._as_list(options.get("COLUMN-HEADERS"))
        alignments = self._as_list(options.get("COLUMN-ALIGNMENT"))
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

        csv_path = self._resolve_csv_path(options.get("INPUT-FILE", ""), ctx)
        rows = self._iter_csv_rows(csv_path, ctx)

        table_html = []
        table_html.append('<table class="tblin">')
        table_html.append("")
        table_html.append("")
        for _ in cols:
            table_html.append("<COL>")
        table_html.append("")

        table_html.append("<thead>")
        table_html.append("<tr id='colhdr'>")
        for h in headers:
            table_html.append(f"<th>{h}</th>")
        table_html.append("</tr>")
        table_html.append("</thead>")

        for idx, row in enumerate(rows):
            cell_class = "tblin" if idx % 2 == 0 else "alt"
            table_html.append("<tr>")
            for col_idx, col in enumerate(cols):
                val = row.get(col.lower(), "")
                val_str = format_value(col, val)
                valign, halign = parse_alignment(alignments[col_idx])
                table_html.append(
                    f'<td class="{cell_class}" style="vertical-align:{valign};text-align:{halign};">{val_str}</td>'
                )
            table_html.append("</tr>")

        table_html.append("")
        table_html.append("<tfoot>")
        table_html.append("</tfoot>")
        table_html.append("</table>")

        table_content = "\n".join(table_html)

        top_report = options.get("AT-TOP-OF-REPORT")
        if top_report:
            table_content = (
                f'<p class="at-top-of-report">\n{top_report}</p>\n' + table_content
            )

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
        directives, html_content = self._parse_layout_template(template)
        path = directives.get("FILE", "report.html")
        css_file = directives.get("CSS")
        css_embed = directives.get("CSSEMBED", "").upper() in ("Y", "YES", "TRUE")
        title = directives.get("TITLE", "SQLPathFinder Report")

        # Replace HTM placeholders
        def replace_report(match: re.Match) -> str:
            report_id = match.group(1)
            if report_id in self.deferred_reports:
                return self._render_report(report_id, ctx)
            return match.group(0)

        html_content = re.sub(r"HTM:([A-Za-z0-9_]+)", replace_report, html_content)

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
                    html_content = html_content.replace(
                        "</head>", f"{css_decl}\n</head>", 1
                    )
                else:
                    html_content = f"{css_decl}\n{html_content}"

        # Resolve output filename
        out_filename = path
        if out_filename.startswith("email:") or not out_filename:
            fallback_name = "report.html"
            for report in self.deferred_reports.values():
                rows, _ = self._ensure_parsed_payload(report)
                found = False
                for parts in rows:
                    if not parts:
                        continue
                    if (
                        parts[0].upper() == "OUTPUT-FILE"
                        and len(parts) >= 2
                        and parts[1]
                    ):
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
        if ctx and hasattr(ctx, "write_file"):
            resolved_path = out_filename
            if hasattr(ctx, "macro"):
                resolved_path = ctx.macro.substitute_sql(out_filename)
            ctx.write_file(resolved_path, html_content)
        elif ctx and hasattr(ctx, "macro"):
            resolved_path = ctx.macro.substitute_sql(out_filename)
            ctx.macro.write_file(resolved_path, html_content)
        else:
            out_path = Path(out_filename)
            out_path.parent.mkdir(parents=True, exist_ok=True)
            out_path.write_text(html_content, encoding="utf-8")

class MacroState:
    """Stack of variable frames; lookups walk top-to-bottom."""

    utility_name = "macro"

    PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
    NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")

    @classmethod
    def normalize_macro_name(cls, raw: str) -> str:
        name = raw.strip()
        if name.startswith("<<<") and name.endswith(">>>"):
            name = name[3:-3]
        return name.strip().upper()

    @staticmethod
    def emit_block(block, dispatched) -> tuple[str, str] | None:
        payload = block.control_payload
        if not isinstance(payload, RowsInFile):
            return _emit_step_source(_step_name(block, "macro_control"), ["pass"])

        csv_path_expr = option_to_python_expr(payload.csv_path)
        set_name = payload.var_name.upper()
        row_count_call = render_method_call(
            "csv_io",
            "row_count",
            args=(RawExpr(csv_path_expr),),
        )
        stmt = render_method_call(
            "macro",
            "set_named",
            args=(set_name, RawExpr(f"str({row_count_call})")),
        )
        return _emit_step_source(_step_name(block, "rows_in_file"), [stmt])

    def __init__(self) -> None:
        self._stack: list[dict[str, str]] = [{}]

    def named(self, name: str) -> str:
        key = name.upper()
        for frame in reversed(self._stack):
            if key in frame:
                return frame[key]
        return ""

    def set_named(self, name: str, value: str) -> None:
        self._stack[-1][name.upper()] = value

    def positional(self) -> str:
        frame = self._stack[-1]
        cursor = frame.get("__cursor__", 0)
        pos_list: list[str] = frame.get("__positional__", [])  # type: ignore[assignment]
        if isinstance(pos_list, list) and cursor < len(pos_list):
            frame["__cursor__"] = cursor + 1
            return pos_list[cursor]
        return ""

    def substitute_sql(self, sql: str) -> str:
        if "<<<" in sql:
            sql = self.NAMED_PLACEHOLDER_RE.sub(
                lambda m: self.named(self.normalize_macro_name(m.group(1))),
                sql,
            )
        return sql

    def resolve_file_path(self, raw_path: str) -> Path:
        """Resolve a possibly-macro path with local basename fallback for abs paths."""
        if not raw_path:
            return Path("")

        resolved = self.substitute_sql(raw_path)
        path = Path(resolved)
        if path.exists() and path.is_file():
            return path

        if path.is_absolute():
            rel_path = Path(path.name)
            if rel_path.exists() and rel_path.is_file():
                return rel_path

        return path

    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        def _lookup(name: str) -> str:
            key = self.normalize_macro_name(name)
            if vars is not None:
                return vars.get(key, "")
            return self.named(key)

        def _replace(match: re.Match[str]) -> str:
            named = match.group(1)
            if named is not None:
                return _lookup(named)
            return self.positional()

        content = self.PLACEHOLDER_RE.sub(_replace, template)
        content = content.lstrip("\n")

        out = Path(path)
        out.parent.mkdir(parents=True, exist_ok=True)
        out.write_text(content, encoding="utf-8")

    def eval_condition(self, lhs: str, op: str, rhs: str) -> bool:
        lhs_val = self.named(lhs) if lhs.startswith("VAR(") else lhs
        rhs_val = self.named(rhs) if rhs.startswith("VAR(") else rhs
        return lhs_val == rhs_val

    def push_frame(self, named: dict[str, str] | None = None) -> None:
        frame: dict[str, str] = {}
        for k, v in (named or {}).items():
            if k is None:
                continue
            frame[k.upper()] = str(v)
        self._stack.append(frame)

    def pop_frame(self) -> None:
        if len(self._stack) > 1:
            self._stack.pop()

    @contextmanager
    def scope(self, row: dict[str, str] | None = None) -> Iterator[None]:
        self.push_frame(named=row)
        try:
            yield
        finally:
            self.pop_frame()

class MailService:
    """Send email. Reads connection config from environment variables."""

    utility_name = "mail"

    @classmethod
    def _emit_email(cls, ctx, argv: list[str]) -> None:
        return None

    def send(
        self,
        to: str,
        subject: str,
        body: str,
        attachments: list[str] | None = None,
        from_addr: str | None = None,
    ) -> None:
        host = os.environ.get("VG2C_SMTP_HOST", "")
        if not host:
            raise RuntimeError(
                "MailService: VG2C_SMTP_HOST is not set. "
                "Set the environment variable to your SMTP server hostname."
            )
        port = int(os.environ.get("VG2C_SMTP_PORT", "25"))
        sender = from_addr or os.environ.get("VG2C_FROM_ADDRESS", "vg2c@localhost")

        msg = EmailMessage()
        msg["Subject"] = subject
        msg["From"] = sender
        msg["To"] = to
        msg.set_content(body)

        for att_path in attachments or []:
            p = Path(att_path)
            if p.exists():
                msg.add_attachment(
                    p.read_bytes(),
                    maintype="application",
                    subtype="octet-stream",
                    filename=p.name,
                )

        with smtplib.SMTP(host, port) as smtp:
            smtp.send_message(msg)

class SqlMacros:
    """SQL macro expansion helpers used by emitted scripts."""

    utility_name = "sql_macros"

    @staticmethod
    def _read_column(path: str, column_ref: int | str) -> list[str]:
        rows: list[str] = []
        with Path(path).open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.reader(fh)
            header = next(reader, [])
            header_str = [str(h) for h in header]

            if isinstance(column_ref, int):
                idx = column_ref - 1
            else:
                col_lower = [h.lower() for h in header]
                try:
                    idx = col_lower.index(column_ref.lower())
                except ValueError:
                    return []

            seen: dict[str, None] = {}
            for row in reader:
                if [str(v) for v in row] == header_str:
                    continue
                if idx < len(row):
                    val = row[idx]
                    if val not in seen:
                        seen[val] = None
                        rows.append(val)

        return rows

    @staticmethod
    def _single_quote(value: str) -> str:
        return "'" + value.replace("'", "''") + "'"

    def sql_get_csv_list(self, path: str, column_ref: int | str, lead_in: str) -> str:
        """Return chunked IN-list clause for Oracle-style SQL.

        Oracle hard-limits IN lists to 1000 values. When there are more, the
        result is chunked: ``(v1..v1000) OR <lead_in> (v1001..)``.
        """
        values = self._read_column(path, column_ref)
        if not values:
            return "('__NO_VALUES__')"

        chunk_size = 1000
        chunks = [values[i : i + chunk_size] for i in range(0, len(values), chunk_size)]
        parts: list[str] = []
        for i, chunk in enumerate(chunks):
            quoted = ", ".join(self._single_quote(v) for v in chunk)
            parts.append(f"({quoted})")
            if i < len(chunks) - 1:
                parts.append(f"\nOR {lead_in} ")

        return "".join(parts)

class SqliteEngine:
    """Emit query calls for external and SQLite readers."""

    utility_name = "sqlite_engine"

    _SQL_MACRO_TOKEN_RE = re.compile(r"@@SQLMACRO:(\d+)@@")

    @staticmethod
    def _format_sql_literal(sql: str) -> str:
        escaped = sql.replace('"""', '\\"\\"\\"')
        return f'"""{escaped}"""'

    @staticmethod
    def _extract_sql_text(block, dispatched) -> str | RawExpr:
        sql = (
            dispatched.rewritten_sql if dispatched is not None else block.resolved_body
        )
        if "@@SQLMACRO:" not in sql:
            return RawExpr(SqliteEngine._format_sql_literal(sql))

        parts: list[str] = []
        cursor = 0
        for match in SqliteEngine._SQL_MACRO_TOKEN_RE.finditer(sql):
            literal = sql[cursor : match.start()]
            if literal:
                parts.append(SqliteEngine._format_sql_literal(literal))

            call_index = int(match.group(1))
            if call_index < 0 or call_index >= len(block.sql_macro_calls):
                parts.append(SqliteEngine._format_sql_literal(match.group(0)))
            else:
                call = block.sql_macro_calls[call_index]
                csv_path_expr = option_to_python_expr(call.csv_path)
                col_ref = repr(call.column_ref)
                lead_in = repr(call.lead_in)
                parts.append(
                    f"ctx.sql_macros.sql_get_csv_list({csv_path_expr}, {col_ref}, {lead_in})"
                )

            cursor = match.end()

        tail = sql[cursor:]
        if tail:
            parts.append(SqliteEngine._format_sql_literal(tail))

        if not parts:
            return RawExpr(SqliteEngine._format_sql_literal(sql))
        return RawExpr(" + ".join(parts))

    @staticmethod
    def _extract_table_inputs(block) -> list[str]:
        inputs: list[str] = []
        for key, value in block.resolved_options.pairs:
            if key != "TABLE":
                continue
            for table_name in value.split(","):
                table_name = strip_quotes(table_name.strip())
                if table_name:
                    inputs.append(table_name)
        return inputs

    @staticmethod
    def _extract_header(block) -> list[str] | None:
        headers_value = block.resolved_options.lookup.get("HEADERS")
        if not headers_value:
            return None
        if CrosstabUtility.has_token(headers_value):
            return None
        stripped = strip_quotes(headers_value)
        parts = [p.strip() for p in stripped.split(",")]
        return [p for p in parts if p]

    @staticmethod
    def emit_block(block, dispatched) -> tuple[str, str] | None:
        sqlite = block.kind is Kind.SQLITE_QUERY
        return SqliteEngine._emit_sql(block, dispatched, sqlite=sqlite)

    @staticmethod
    def _emit_sql(
        block,
        dispatched,
        *,
        sqlite: bool,
    ) -> tuple[str, str]:
        if dispatched is None:
            raise ValueError("SQL emission requires dispatch metadata")

        sql = SqliteEngine._extract_sql_text(block, dispatched)
        output = resolve_output_path(block)
        reader_cls = dispatched.reader_cls
        crosstab = CrosstabUtility.extract_options(block)
        header = None if crosstab else SqliteEngine._extract_header(block)

        reader_kwargs_items = [f"{k}={repr(v)}" for k, v in dispatched.reader_kwargs.items()]
        inst_expr = f"{reader_cls.__name__}({', '.join(reader_kwargs_items)})"

        kwargs: dict[str, object] = {
            "sql": sql,
            "output": output,
            "reader": RawExpr(inst_expr),
        }
        if sqlite:
            kwargs["inputs"] = SqliteEngine._extract_table_inputs(block)
        if header:
            kwargs["header"] = header
        if crosstab:
            kwargs["crosstab"] = crosstab

        stmt = render_method_call("ctx", "run_query", kwargs=kwargs)
        suffix = "sqlite_query" if sqlite else "sql_query"
        return _emit_step_source(_step_name(block, suffix), [stmt])

def step_0000_unknown(ctx) -> None:
    pass  # TODO: unhandled kind=Kind.UNKNOWN

def step_0001_html_report(ctx) -> None:
    ctx.html_report.defer(instance='10105', id='MYREPORT5', prompt_text='Step 4. Create an HTML Report', app_server_default='atd_atm.hadoop', template='\n\n\n\n\nType<\\\\>Key<\\\\>COL1<\\\\>COL2<\\\\>COL3<\\\\>COL4<\\\\>COL5<\\\\>COL6<\\\\>COL7<\\\\>COL8<\\\\>COL9<\\\\>COL10<\\\\>COL11<\\\\>COL12\nTYPE<\\\\>HTML<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nINPUT-FILE<\\\\>\\\\kmatshfs.intel.com\\kmatanalysis$\\MAOATM\\KuAT\\TCB\\atrms_percentage.csv<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nOUTPUT-FILE<\\\\>SQLPathFinder.htm<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCSS<\\\\>sqlpathfinder_style_1.css<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLSPAN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDRILLDOWN<\\\\>N<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDYNAMICSORT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDYNAMICFILTER<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nATTOPDRILLDOWN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nNOPREPROCESS<\\\\>Y<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-DATA<\\\\><\\\\>total_pcg<\\\\>total_flag<\\\\>ce%<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-HEADERS<\\\\><\\\\>Total Pcg<\\\\>Total Flag<\\\\>Ce%<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-ALIGNMENT<\\\\><\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-FORMAT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>')

def step_0002_html_report(ctx) -> None:
    ctx.html_report.defer(instance='10105', id='MYREPORT2', app_server_default='atd_atm.hadoop', template='\n\n\n\n\nType<\\\\>Key<\\\\>COL1<\\\\>COL2<\\\\>COL3<\\\\>COL4<\\\\>COL5<\\\\>COL6<\\\\>COL7<\\\\>COL8<\\\\>COL9<\\\\>COL10<\\\\>COL11<\\\\>COL12\nTYPE<\\\\>HTML<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nINPUT-FILE<\\\\>\\\\kmatshfs.intel.com\\kmatanalysis$\\MAOATM\\KuAT\\TCB\\atrms_summary.csv<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nOUTPUT-FILE<\\\\>SQLPathFinder.htm<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCSS<\\\\>sqlpathfinder_style_1.css<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLSPAN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDRILLDOWN<\\\\>N<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDYNAMICSORT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDYNAMICFILTER<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nATTOPDRILLDOWN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nNOPREPROCESS<\\\\>Y<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nAT-TOP-OF-REPORT<\\\\><\\\\>ATRMS PCG GAPS<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-DATA<\\\\><\\\\>operation<\\\\>pcg<\\\\>a01<\\\\>a15<\\\\>a48<\\\\>a90<\\\\>new_flag<\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-HEADERS<\\\\><\\\\>Operation<\\\\>Pcg<\\\\>A01<\\\\>A15<\\\\>A48<\\\\>A90<\\\\>New Flag<\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-ALIGNMENT<\\\\><\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-FORMAT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>')

def step_0003_html_report(ctx) -> None:
    ctx.html_report.run(instance='10105', app_server_default='atd_atm.hadoop', template='\n\n\n\n\n\nType<\\\\>Key<\\\\>COL1<\\\\>COL2<\\\\>COL3<\\\\>COL4<\\\\>COL5<\\\\>COL6<\\\\>COL7<\\\\>COL8\nTYPE<\\\\>CSS<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCSS<\\\\>sqlpathfinder_style_1.css<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nFORMAT<\\\\>Column-Headers<\\\\>background-color:#dbd9c0<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>text-decoration:normal<\\\\>vertical-align:middle\nFORMAT<\\\\>Column-Data<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>text-align:left<\\\\>vertical-align:middle<\\\\>\nFORMAT<\\\\>Column-Alt-Row<\\\\>background-color:#f7f5dc<\\\\>color:#333<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>text-align:left<\\\\>vertical-align:middle<\\\\>\nFORMAT<\\\\>At-Top-of-Report<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:15<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:center<\\\\>vertical-align:middle\nFORMAT<\\\\>At-Top-of-Col1<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>At-Top-of-Col2<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>At-Top-of-Col3<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>JQX-All-IChart-Text<\\\\>background-color:white<\\\\>color:black<\\\\>font-family:Verdana<\\\\>font-size:11<\\\\>font-style:normal<\\\\>font-weight:normal<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>COLUMN-BORDER<\\\\>border-color:#cc9<\\\\>border-collapse:collapse<\\\\>border-style:solid<\\\\>border-width:1px<\\\\>border-spacing:4px<\\\\><\\\\><\\\\>')

def step_0004_html_report(ctx) -> None:
    ctx.html_report.layout(ctx, outlook='N', instance='10105', json_only='N', chart_instance='6403', app_server_default='atd_atm.hadoop', template='<table class="tblout"><tr class="tblout"><td class="tblout" valign="top">\n:FILE:email:self\n:CSS:sqlpathfinder_style_1.css\n:CSSEMBED:N\n:RR:NO\n:B:N\n:EM-A:\\\\kmatshfs.intel.com\\kmatanalysis$\\MAOATM\\KuAT\\TCB\\atrms_raw.csv\n:EM-S:\n:SEC:Y\n:TITLE:ATRMS PCG GAPS\n<table class="tblout">\n<tr class="tblout">\n<td class="tblout">\nHTM:MYREPORT5\n</td>\n</tr>\n<tr class="tblout">\n<td class="tblout">\nHTM:MYREPORT2\n</td>\n</tr>\n<tr class="tblout">\n<td class="tblout">\n<p style="text-align: left" style="background-color: white"><i><font face="intelone display light" size="4" color="gray">Rev2-12012025<br>Exclude A04 & A06 data</font></i>\n</td>\n</tr>\n</table>\n</td><td class="tblout" valign="top">\n<table class="tblout">\n<tr class="tblout"><td class="tblout"></td></tr>\n</table>\n</td></tr></table>')

def step_0005_html_report(ctx) -> None:
    ctx.html_report.delete(instance='10105')

def run() -> None:
    ctx = PipelineContext()
    step_0000_unknown(ctx)
    step_0001_html_report(ctx)
    step_0002_html_report(ctx)
    step_0003_html_report(ctx)
    step_0004_html_report(ctx)
    step_0005_html_report(ctx)

if __name__ == "__main__":
    run()