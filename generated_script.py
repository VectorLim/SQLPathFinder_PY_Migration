# Auto-generated Python script from VG2
"""Pipeline implementation."""


from __future__ import annotations
from abc import ABC
from contextlib import contextmanager
from dataclasses import dataclass
from datasyncx.readers.oracle_reader import OracleReader
from email.message import EmailMessage
from functools import partial
from pathlib import Path
from typing import Any
from typing import Any, Callable
from typing import Any, ClassVar
from typing import Any, ContextManager
from typing import Any, Iterator
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

    @classmethod
    def emit_block(
        cls, ctx: Any, block: Any, dispatched: Any
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
                writer = csv.DictWriter(fh, fieldnames=fieldnames, extrasaction="ignore")
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

    def __getattr__(self, name: str):
        def _missing(*args: Any, **kwargs: Any) -> None:
            print("not implemented yet")

        return _missing

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

    @classmethod
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        argv = cls._utility_argv(block)
        if not argv:
            return _emit_step_source(
                _step_name(block, "external"),
                ["pass  # TODO: empty external utility command"],
            )

        stmt = cls._emit_run(ctx, argv)
        return _emit_step_source(_step_name(block, "external"), [stmt])

    @classmethod
    def _emit_run(cls, ctx, argv: list[str]) -> str:
        expr_items = [option_to_python_expr(token) for token in argv]
        argv_expr = RawExpr("[" + ", ".join(expr_items) + "]")
        return ctx.render_method_call(
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

    @classmethod
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        if block.kind is Kind.FS_COPY:
            return cls._emit_copy_block(ctx, block)
        if block.kind is Kind.FS_DELETE:
            return cls._emit_delete_block(ctx, block)

        stmt = ctx.render_method_call(
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

    @classmethod
    def _emit_copy_block(cls, ctx, block) -> tuple[str, str]:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "robocopy" in basename:
            stmt = cls._emit_robocopy(ctx, argv)
        elif "spfcopy" in basename:
            stmt = cls._emit_spf_copy(ctx, argv)
        else:
            return _emit_step_source(
                _step_name(block, "fs_copy"),
                ["pass  # TODO: unsupported FS copy utility command"],
            )
        return _emit_step_source(_step_name(block, "fs_copy"), [stmt])

    @classmethod
    def _emit_delete_block(cls, ctx, block) -> tuple[str, str]:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "spfdelete" not in basename:
            return _emit_step_source(
                _step_name(block, "fs_delete"),
                ["pass  # TODO: unsupported FS delete utility command"],
            )
        stmt = cls._emit_spf_delete(ctx, argv)
        return _emit_step_source(_step_name(block, "fs_delete"), [stmt])

    @classmethod
    def _emit_robocopy(cls, ctx, argv: list[str]) -> str:
        # RoboCopy.va arg layout: <file_name> <source_dir> <dest_dir> [...]
        file_name = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        source_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        dest_dir = option_to_python_expr(argv[3]) if len(argv) > 3 else repr(".")
        src_expr = RawExpr(f"str(Path({source_dir}) / {file_name})")
        dst_expr = RawExpr(dest_dir)
        return ctx.render_method_call(
            cls.utility_name,
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    @classmethod
    def _emit_spf_copy(cls, ctx, argv: list[str]) -> str:
        # SPFCopy.bat arg layout: <source_path> <dest_dir> [recurse]
        src = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        dst_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        src_expr = RawExpr(src)
        dst_expr = RawExpr(f"str(Path({dst_dir}) / Path({src}).name)")
        return ctx.render_method_call(
            cls.utility_name,
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    @classmethod
    def _emit_spf_delete(cls, ctx, argv: list[str]) -> str:
        raw = strip_quotes(argv[1]) if len(argv) > 1 else ""
        items = [p.strip() for p in raw.split(",") if p.strip()]
        paths_expr = RawExpr(
            "[" + ", ".join(option_to_python_expr(p) for p in items) + "]"
        )
        return ctx.render_method_call(
            cls.utility_name,
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
        self.styles: dict[str, str] = {}
        self.instance: str | None = None
        self.prompt_text: str | None = None
        self.app_server_default: str | None = None

    @classmethod
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        report_type = block.resolved_options.lookup.get("REPORT", "").upper().strip()
        if report_type == "HTML-RUN":
            return cls._emit_html_run(ctx, block)
        elif report_type == "HTML-LAYOUT":
            return cls._emit_html_layout(ctx, block)
        elif report_type == "HTML-DELETE":
            return cls._emit_html_delete(ctx, block)
        return None

    @classmethod
    def _emit_html_run(cls, ctx, block) -> tuple[str, str]:
        kwargs = {}
        for key in ["INSTANCE", "PROMPT-TEXT", "APP_SERVER_DEFAULT"]:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = RawExpr(option_to_python_expr(val))
        kwargs["template"] = block.resolved_body
        stmt = ctx.render_method_call("html_report", "run", kwargs=kwargs)
        return _emit_step_source(_step_name(block, "html_report"), [stmt])

    @classmethod
    def _emit_html_layout(cls, ctx, block) -> tuple[str, str]:
        kwargs = {}
        for key in ["OUTLOOK", "INSTANCE", "JSON-ONLY", "CHART-INSTANCE", "APP_SERVER_DEFAULT"]:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = RawExpr(option_to_python_expr(val))
        kwargs["template"] = block.resolved_body
        stmt = ctx.render_method_call(
            "html_report",
            "layout",
            args=(RawExpr("ctx"),),
            kwargs=kwargs,
        )
        return _emit_step_source(_step_name(block, "html_report"), [stmt])

    @classmethod
    def _emit_html_delete(cls, ctx, block) -> tuple[str, str]:
        kwargs = {}
        for key in ["INSTANCE"]:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = RawExpr(option_to_python_expr(val))
        stmt = ctx.render_method_call("html_report", "delete", kwargs=kwargs)
        return _emit_step_source(_step_name(block, "html_report"), [stmt])

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
            for line in template.splitlines():
                if not line.strip():
                    continue
                parts = line.split("<\\>")
                if len(parts) >= 3 and parts[0].upper() == "FORMAT":
                    self.styles[parts[1]] = parts[2]

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
        path = "report.html"
        css_file = None
        css_embed = False
        html_lines = []

        for line in template.splitlines():
            if line.startswith(":"):
                parts = line[1:].split(":", 1)
                if len(parts) == 2:
                    key = parts[0].strip().upper()
                    val = parts[1].strip()
                    if key == "FILE":
                        path = val
                    elif key == "CSS":
                        css_file = val
                    elif key == "CSSEMBED":
                        css_embed = val.upper() in ("Y", "YES", "TRUE")
            else:
                html_lines.append(line)

        html_content = "\n".join(html_lines)

        if css_file:
            css_path = Path(css_file)
            if css_embed and css_path.exists():
                css_content = css_path.read_text(encoding="utf-8", errors="replace")
                style_tag = f"<style>\n{css_content}\n</style>"
                if "</head>" in html_content:
                    html_content = html_content.replace(
                        "</head>", f"{style_tag}\n</head>", 1
                    )
                else:
                    html_content = f"{style_tag}\n{html_content}"

        if ctx and hasattr(ctx, "macro"):
            resolved_path = ctx.macro.substitute_sql(path)
            ctx.macro.write_file(resolved_path, html_content)
        else:
            out_path = Path(path)
            out_path.parent.mkdir(parents=True, exist_ok=True)
            out_path.write_text(html_content, encoding="utf-8")

    def delete(self, instance: str | None = None) -> None:
        pass

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

    @classmethod
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        payload = block.control_payload
        if not isinstance(payload, RowsInFile):
            return _emit_step_source(_step_name(block, "macro_control"), ["pass"])

        csv_path_expr = option_to_python_expr(payload.csv_path)
        set_name = payload.var_name.upper()
        row_count_call = ctx.render_method_call(
            "csv_io",
            "row_count",
            args=(RawExpr(csv_path_expr),),
        )
        stmt = ctx.render_method_call(
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

    @classmethod
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        sqlite = block.kind is Kind.SQLITE_QUERY
        return cls._emit_sql(ctx, block, dispatched, sqlite=sqlite)

    @classmethod
    def _emit_sql(
        cls,
        ctx,
        block,
        dispatched,
        *,
        sqlite: bool,
    ) -> tuple[str, str]:
        if dispatched is None:
            raise ValueError("SQL emission requires dispatch metadata")

        sql = cls._extract_sql_text(block, dispatched)
        output = resolve_output_path(block)
        reader_cls = dispatched.reader_cls
        ctx.add_import(reader_cls.__module__, reader_cls.__name__)
        crosstab = CrosstabUtility.extract_options(block)
        header = None if crosstab else cls._extract_header(block)

        reader_kwargs_items = [f"{k}={repr(v)}" for k, v in dispatched.reader_kwargs.items()]
        inst_expr = f"{reader_cls.__name__}({', '.join(reader_kwargs_items)})"

        kwargs: dict[str, object] = {
            "sql": sql,
            "output": output,
            "reader": RawExpr(inst_expr),
        }
        if sqlite:
            kwargs["inputs"] = cls._extract_table_inputs(block)
        if header:
            kwargs["header"] = header
        if crosstab:
            kwargs["crosstab"] = crosstab

        stmt = ctx.render_method_call("ctx", "run_query", kwargs=kwargs)
        suffix = "sqlite_query" if sqlite else "sql_query"
        return _emit_step_source(_step_name(block, suffix), [stmt])

def step_0000_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              v1.lot AS spc_lot
             ,v1.operation AS spc_operation
             ,v4.equipment_name AS spc_entity
             ,v3.monitor_set_name AS monitor_set_name
             ,v5.reading_set_name AS measurement_set_name
             ,v7.spc_chart_subset AS spc_chart_subset
             ,v7.chart_type AS chart_type
             ,v6.value AS raw_value
             ,v6.reading_id AS reading_id
             ,To_Char(v1.transaction_datetime,'yyyy-mm-dd hh24:mi:ss') AS spc_lot_txn_date
    FROM 
         P_SPC_Batch_Lot v1
        ,P_SPC_Batch v2
        ,P_SPC_Session v3
        ,P_SPC_Equipment v4
        ,P_SPC_Reading_Set v5
        ,P_SPC_Chart_Point v7
        ,P_SPC_Reading v6
    WHERE 
                  v2.batch_id = v1.batch_id
     AND      v2.facility = v1.facility
     AND      v2.batch_id = v3.batch_id
     AND      v2.facility = v3.facility
     AND      v2.data_collection_ww = v3.data_collection_ww
     AND      v3.facility = v4.facility
     AND      v3.data_collection_ww = v4.data_collection_ww
     AND      v3.spcs_id = v4.spcs_id
     AND      v4.equipment_sequence = 1
     AND      v3.facility = v5.facility
     AND      v3.data_collection_ww = v5.data_collection_ww
     AND      v3.spcs_id = v5.spcs_id
     AND      v5.data_collection_ww = v6.data_collection_ww
     AND      v5.spcs_id = v6.spcs_id
     AND      v5.reading_set_name = v6.reading_set_name
     AND      v7.data_Collection_ww = v3.data_collection_ww
     AND      v7.spcs_id = v3.spcs_id
     AND      v7.reading_set_name = v5.reading_set_name
     AND      v3.latest_flag = 'Y' 
     AND      v3.status <> 'I' 
     AND      v1.transaction_datetime >= SYSDATE - 1 
     AND      v1.operation = '2511' 
    /*END SQL*/""", output='spc.csv', reader=OracleReader(database='OASYS'), header=['spc_lot', 'spc_operation', 'spc_entity', 'monitor_set_name', 'measurement_set_name', 'spc_chart_subset', 'chart_type', 'raw_value', 'reading_id', 'spc_lot_txn_date'])

def run() -> None:
    ctx = PipelineContext()
    step_0000_sql_query(ctx)

if __name__ == "__main__":
    run()