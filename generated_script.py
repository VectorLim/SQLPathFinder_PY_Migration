# Auto-generated Python script from VG2
"""Pipeline implementation."""


from __future__ import annotations
from contextlib import contextmanager
from dataclasses import dataclass
from datasyncx.readers import AriesReader, MarsReader, OracleReader
from email.message import EmailMessage
from pathlib import Path
from typing import Any
from typing import Any, Callable
from typing import Any, ContextManager
from typing import Any, Iterator
from typing import Iterator, Protocol
import csv
import os
import pandas
import pandas as pd
import re
import shutil
import smtplib
import sqlite3
import subprocess

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
    from vg2c.emitter.utilities._emit_helpers import placeholders_to_python_expr

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

PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")

NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")

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
    ctx: Any,
    utility_name: str,
    method_name: str,
    *,
    args: tuple[Any, ...] = (),
    kwargs: dict[str, Any] | None = None,
) -> str:
    receiver = "ctx" if utility_name == "ctx" else f"ctx.{utility_name}"
    parts: list[str] = [_render_value(arg) for arg in args]
    for key, value in (kwargs or {}).items():
        parts.append(f"{key}={_render_value(value)}")
    return f"{receiver}.{method_name}({', '.join(parts)})"

def _step_name(block, suffix: str) -> str:
    return f"step_{block.parsed.index:04d}_{suffix}"

def _emit_step_source(name: str, body_lines: list[str]) -> tuple[str, str]:
    lines = [f"def {name}(ctx) -> None:"]
    if body_lines:
        lines.extend([f"    {line}" for line in body_lines])
    else:
        lines.append("    pass")
    return "\n".join(lines), f"{name}(ctx)"

def emit_block(ctx: Any, block: Any, dispatched: Any) -> tuple[str, str]:
    handler_cls = UtilitySpec._kind_handlers.get(block.kind)
    if handler_cls is not None:
        emitted = handler_cls.emit_block(ctx, block, dispatched)
        if emitted is not None:
            return emitted

    if block.kind is Kind.UTILITY:
        return _emit_step_source(
            _step_name(block, "utility"),
            ["pass  # TODO: utility command not classified"],
        )
    if block.kind is Kind.HTML_REPORT:
        return _emit_step_source(
            _step_name(block, "html_report"),
            ["pass  # HTML report not translated"],
        )
    return _emit_step_source(
        _step_name(block, "unknown"),
        [f"pass  # TODO: unhandled kind={block.kind}"],
    )

class CrosstabUtility:
    utility_name = "crosstab"

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
            path.write_text("", encoding="utf-8")
            return

        with path.open("w", newline="", encoding="utf-8") as fh:
            if rows and isinstance(rows[0], dict):
                fieldnames = list(rows[0].keys())
                writer = csv.DictWriter(fh, fieldnames=fieldnames)
                writer.writeheader()
                writer.writerows(rows)
            else:
                writer_plain = csv.writer(fh)
                if header:
                    writer_plain.writerow(header)
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

    def read(self, sql: str, db_type: str):
        return self.reader_runtime.read(
            sql=sql, db_type=db_type, macro_state=self.macro
        )

    def run_query(
        self,
        sql,
        output: str,
        source_type: str,
        inputs: list[str] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
    ):
        sql = self.macro.substitute_sql(sql)

        if source_type.lower() == "sqlite":
            result = self.sqlite_engine.execute(sql, inputs or [])
        else:
            result = self.reader_runtime.read(
                sql=sql, db_type=source_type, macro_state=None
            )

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
        return render_method_call(
            ctx,
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

        stmt = render_method_call(
            ctx,
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
        return render_method_call(
            ctx,
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
        return render_method_call(
            ctx,
            cls.utility_name,
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    @classmethod
    def _emit_spf_delete(cls, ctx, argv: list[str]) -> str:
        raw = argv[1] if len(argv) > 1 else ""
        items = [p.strip() for p in raw.split(",") if p.strip()]
        paths_expr = RawExpr(
            "[" + ", ".join(option_to_python_expr(p) for p in items) + "]"
        )
        return render_method_call(
            ctx,
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
                # if recurse:
                # shutil.rmtree(path, ignore_errors=True)
                pass
            else:
                # path.unlink(missing_ok=True)
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
        row_count_call = render_method_call(
            ctx,
            "csv_io",
            "row_count",
            args=(RawExpr(csv_path_expr),),
        )
        stmt = render_method_call(
            ctx,
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

class ReaderRuntime:
    utility_name = "reader_runtime"
    DATABASE_TYPE_MAP = {
        "MARS": MarsReader,
        "OASYS": OracleReader,
        "ARIES": AriesReader,
    }

    def read(self, sql, db_type, macro_state=None):
        """Run *sql* against the Reader registered for *db_type*."""
        if macro_state is not None:
            sql = macro_state.substitute_sql(sql)
        if db_type not in self.DATABASE_TYPE_MAP:
            raise ValueError(f"Unsupported database type: {db_type!r}")
        result = self.DATABASE_TYPE_MAP[db_type]().read(site="KM", query=sql)
        result.columns = [col.lower() for col in result.columns]
        return result

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
    """Run SQL joins over CSV files using in-memory SQLite."""

    utility_name = "sqlite_engine"

    CROSSTAB_RE = re.compile(
        r"(?:,CrossTab->\[\[\s*([A-Za-z_][A-Za-z0-9_]*)\s*,\s*([^;\]]+)\s*;\s*:([YyNn])\s*\]\])",
        re.IGNORECASE,
    )
    STMT_SPLIT_RE = re.compile(
        r"(?:'[^']*'|\"[^\"]*\"|\[[^\]]*\]|`[^`]*`|[^;])+",
        re.DOTALL,
    )

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

    @classmethod
    def _substitute_crosstab(
        cls,
        sql: str,
        alias_columns_lookup: Callable[[str], list[str]] | None = None,
    ) -> str:
        if alias_columns_lookup is None or "CrossTab->[[" not in sql:
            return sql

        selected_by_alias = cls._extract_selected_columns_by_alias(sql)

        def _replace(match: re.Match[str]) -> str:
            alias = match.group(1)
            mode = match.group(3).upper()
            all_cols = alias_columns_lookup(alias)
            selected = selected_by_alias.get(alias.lower(), set())
            dynamic_cols = [c for c in all_cols if c.lower() not in selected]

            if not dynamic_cols:
                return ""

            if mode == "N":
                return "," + ",".join(dynamic_cols)

            return "," + "\n         ,".join(
                f"{alias}.[{c}] AS [{c}]" for c in dynamic_cols
            )

        return cls.CROSSTAB_RE.sub(_replace, sql)

    @staticmethod
    def _load_csv_as_table(conn: sqlite3.Connection, csv_path: str) -> str:
        path = Path(csv_path)
        table_name = path.stem

        with path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.DictReader(fh)
            rows = list(reader)

        if reader.fieldnames is None:
            conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
            conn.execute(f'CREATE TABLE "{table_name}" ("_empty" TEXT)')
            return table_name

        cols = list(reader.fieldnames)
        if not cols:
            conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
            conn.execute(f'CREATE TABLE "{table_name}" ("_empty" TEXT)')
            return table_name

        col_defs = ", ".join(f'"{c}" TEXT' for c in cols)
        conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
        conn.execute(f'CREATE TABLE "{table_name}" ({col_defs})')

        header_str = [str(c) for c in cols]
        filtered_rows = [
            row for row in rows if [str(row.get(c, "")) for c in cols] != header_str
        ]

        if filtered_rows:
            placeholders = ", ".join("?" for _ in cols)
            conn.executemany(
                f'INSERT INTO "{table_name}" VALUES ({placeholders})',
                [[row.get(c, "") for c in cols] for row in filtered_rows],
            )

        return table_name

    @classmethod
    def _split_statements(cls, sql: str) -> list[str]:
        return [
            match.group(0).strip()
            for match in cls.STMT_SPLIT_RE.finditer(sql)
            if match.group(0).strip()
        ]

    @staticmethod
    def _extract_sql_text(block, dispatched) -> str | RawExpr:
        sql = (
            dispatched.rewritten_sql if dispatched is not None else block.resolved_body
        )
        if "@@SQLMACRO:" not in sql:
            return sql

        parts: list[str] = []
        cursor = 0
        for match in _SQL_MACRO_TOKEN_RE.finditer(sql):
            literal = sql[cursor : match.start()]
            if literal:
                parts.append(repr(literal))

            call_index = int(match.group(1))
            if call_index < 0 or call_index >= len(block.sql_macro_calls):
                parts.append(repr(match.group(0)))
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
            parts.append(repr(tail))

        if not parts:
            return sql
        return RawExpr(" + ".join(parts))

    @staticmethod
    def _extract_source_type(dispatched) -> str:
        if dispatched is None:
            return "MARS"

        db_by_dialect = {
            "oracle_mars": "MARS",
            "oracle_oasys": "OASYS",
            "oracle_aries": "ARIES",
            "sqlite": "sqlite",
        }
        return db_by_dialect.get(
            dispatched.dialect,
            dispatched.reader_target.database_arg or "MARS",
        )

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
        if "CrossTab->[[" in headers_value:
            return None
        stripped = strip_quotes(headers_value)
        parts = [p.strip() for p in stripped.split(",")]
        return [p for p in parts if p]

    @staticmethod
    def _extract_crosstab(block) -> dict[str, Any] | None:
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
        source_type = "sqlite" if sqlite else cls._extract_source_type(dispatched)
        crosstab = cls._extract_crosstab(block)
        header = None if crosstab else cls._extract_header(block)

        kwargs: dict[str, object] = {
            "sql": sql,
            "output": output,
            "source_type": source_type,
        }
        if sqlite:
            kwargs["inputs"] = cls._extract_table_inputs(block)
        if header:
            kwargs["header"] = header
        if crosstab:
            kwargs["crosstab"] = crosstab

        stmt = render_method_call(ctx, "ctx", "run_query", kwargs=kwargs)
        suffix = "sqlite_query" if sqlite else "sql_query"
        return _emit_step_source(_step_name(block, suffix), [stmt])

    def execute(self, sql: str, inputs: list[str]) -> pd.DataFrame:
        conn = sqlite3.connect(":memory:")
        conn.row_factory = sqlite3.Row

        for csv_path in inputs:
            self._load_csv_as_table(conn, csv_path)

        stmts = self._split_statements(sql)
        if not stmts:
            conn.close()
            return pd.DataFrame()

        for stmt in stmts[:-1]:
            try:
                conn.execute(stmt)
            except sqlite3.Error:
                pass

        final_stmt = stmts[-1]

        alias_to_table: dict[str, str] = {}
        alias_map_re = re.compile(
            r"\b(?:FROM|JOIN)\s+(?:\[([^\]]+)\]|\"([^\"]+)\"|([A-Za-z_][A-Za-z0-9_]*))\s+([A-Za-z_][A-Za-z0-9_]*)\b",
            re.IGNORECASE,
        )
        for match in alias_map_re.finditer(final_stmt):
            table_name = match.group(1) or match.group(2) or match.group(3)
            alias = match.group(4)
            if table_name and alias:
                alias_to_table[alias.lower()] = table_name

        def _lookup_alias_columns(alias: str) -> list[str]:
            table_name = alias_to_table.get(alias.lower())
            if not table_name:
                return []
            pragma_rows = conn.execute(f'PRAGMA table_info("{table_name}")').fetchall()
            return [str(row[1]) for row in pragma_rows if len(row) > 1]

        final_stmt = self._substitute_crosstab(
            final_stmt,
            alias_columns_lookup=_lookup_alias_columns,
        )

        try:
            cursor = conn.execute(final_stmt)
            rows = cursor.fetchall()
            col_names = [d[0] for d in cursor.description] if cursor.description else []
        except sqlite3.Error as exc:
            conn.close()
            raise RuntimeError(
                f"SQLite error in execute: {exc}\nSQL:\n{final_stmt}"
            ) from exc

        conn.close()

        if not rows or not col_names:
            return pd.DataFrame()

        data = [{col_names[i]: row[i] for i in range(len(col_names))} for row in rows]
        return pd.DataFrame(data)

def step_0000_html_report(ctx) -> None:
    pass  # HTML report not translated

def step_0001_html_report(ctx) -> None:
    pass  # HTML report not translated

def step_0002_html_report(ctx) -> None:
    pass  # HTML report not translated

def step_0003_write_file(ctx) -> None:
    ctx.write_file(path='macrotmp.csv', template='\nSfolder,underDEV,useCSR,useMMS\nICMPCS_SUBPLANE_CSR_DLA,Y,Y,Y')

def step_0004_write_file(ctx) -> None:
    ctx.write_file(path='getcsrsu.bat', template='\n@echo off\nset PriCSR="\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset SecCSR="\\\\KMATSHFS.intel.com\\KMATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset BakCSR="\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\%username%\\Patrol\\*.___"\ncopy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .\nren setsiteparam.___ setsiteparam.exe')

def step_0005_external(ctx) -> None:
    ctx.external.run(argv=['getcsrsu.bat'])

def step_0007_external(ctx) -> None:
    ctx.external.run(argv=['setsiteparam.exe', 'KM', ctx.macro.named("SFOLDER"), ctx.macro.named("UNDERDEV"), ctx.macro.named("USECSR"), ctx.macro.named("USEMMS")])

def step_0009_fs_delete(ctx) -> None:
    ctx.fs_ops.delete(paths=['"macrotmp.csv', 'getcsrsu.bat', 'setsiteparam.exe', 'csrsu.txt"'])

def step_0011_rows_in_file(ctx) -> None:
    ctx.macro.set_named('CONFIG', str(ctx.csv_io.row_count('ICMPCS_config.csv')))

def step_0013_utility(ctx) -> None:
    pass  # TODO: utility command not classified

def step_0015_sqlite_query(ctx) -> None:
    ctx.run_query(sql="\nSELECT /*L10*/  DISTINCT \n          [icmpcs] AS [icmpcs]\n         ,[parameter] AS [parameter]\n         ,Max([value]) AS [value]\n         ,[STARTTS] AS [STARTTS]\n         ,[UTC] AS [UTC]\n         ,[SFOLDER] AS [SFOLDER]\n         ,[FAC] AS [FAC]\n         ,[MARS] AS [MARS]\n         ,[RIMS] AS [RIMS]\n         ,[EIMS] AS [EIMS]\n         ,[ARIES] AS [ARIES]\n         ,[OASYS] AS [OASYS]\n         ,[MMS] AS [MMS]\n         ,[MMSI] AS [MMSI]\n         ,[TOOLLOG] AS [TOOLLOG]\n         ,[VFMARS] AS [VFMARS]\n         ,[VFARIES] AS [VFARIES]\n         ,[CSRPATH] AS [CSRPATH]\n         ,[MMSPATH] AS [MMSPATH]\n         ,[UNDERDEV] AS [UNDERDEV]\n         ,[CSRV] AS [CSRV]\n         ,[MMSV] AS [MMSV]\nFROM\n(\nSELECT /*L0*/  \n          a0.[icmpcs] AS [icmpcs]\n         ,a0.[parameter] AS [parameter]\n         ,a0.[value] AS [value]\n         ,'<<<STARTTS>>>' AS [STARTTS]\n         ,'<<<UTC>>>' AS [UTC]\n         ,'<<<SFOLDER>>>' AS [SFOLDER]\n         ,'<<<FAC>>>' AS [FAC]\n         ,'<<<MARS>>>' AS [MARS]\n         ,'<<<RIMS>>>' AS [RIMS]\n         ,'<<<EIMS>>>' AS [EIMS]\n         ,'<<<ARIES>>>' AS [ARIES]\n         ,'<<<OASYS>>>' AS [OASYS]\n         ,'<<<MMS>>>' AS [MMS]\n         ,'<<<MMSI>>>' AS [MMSI]\n         ,'<<<TOOLLOG>>>' AS [TOOLLOG]\n         ,'<<<VFMARS>>>' AS [VFMARS]\n         ,'<<<VFARIES>>>' AS [VFARIES]\n         ,'<<<CSRPATH>>>' AS [CSRPATH]\n         ,'<<<MMSPATH>>>' AS [MMSPATH]\n         ,'<<<UNDERDEV>>>' AS [UNDERDEV]\n         ,'<<<CSRV>>>' AS [CSRV]\n         ,'<<<MMSV>>>' AS [MMSV]\nFROM \n[ICMPCS_config] a0\nWHERE\n              a0.[icmpcs] = 'ICMPCS' \n) t /*L0*/\nGROUP BY \n          [icmpcs]\n         ,[parameter]\n         ,[STARTTS]\n         ,[UTC]\n         ,[SFOLDER]\n         ,[FAC]\n         ,[MARS]\n         ,[RIMS]\n         ,[EIMS]\n         ,[ARIES]\n         ,[OASYS]\n         ,[MMS]\n         ,[MMSI]\n         ,[TOOLLOG]\n         ,[VFMARS]\n         ,[VFARIES]\n         ,[CSRPATH]\n         ,[MMSPATH]\n         ,[UNDERDEV]\n         ,[CSRV]\n         ,[MMSV]\n", output='configsets.csv', source_type='sqlite', inputs=['ICMPCS_config.csv'], crosstab={'row_keys': ['icmpcs', 'STARTTS', 'UTC', 'SFOLDER', 'FAC', 'MARS', 'RIMS', 'EIMS', 'ARIES', 'OASYS', 'MMS', 'MMSI', 'TOOLLOG', 'VFMARS', 'VFARIES', 'CSRPATH', 'MMSPATH', 'UNDERDEV', 'CSRV', 'MMSV'], 'header_key': 'parameter', 'value_key': 'value'})

def step_0016_rows_in_file(ctx) -> None:
    ctx.macro.set_named('CONFIGSETS', str(ctx.csv_io.row_count('configsets.csv')))

def step_0018_utility(ctx) -> None:
    pass  # TODO: utility command not classified

def step_0022_write_file(ctx) -> None:
    ctx.write_file(path='CSRVerror.htm', template='\n<!DOCTYPE html>\n<html>\n<body>\n<p>It is detected that you cannot access to CSR depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>CSR Superuser</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong>\nPath: <<<CSRPATH>>></p>\n</body>\n</html>')

def step_0023_utility(ctx) -> None:
    pass  # TODO: utility command not classified

def step_0026_write_file(ctx) -> None:
    ctx.write_file(path='MMSVerror.htm', template='\n<!DOCTYPE html>\n<html\n<body>\n<p>It is detected that you cannot access to MMS Signal Tracer depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>MMS Signal Tracer Admin</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong><br/>\nPath: <<<MMSPATH>>></p>\n</body>\n</html>')

def step_0027_utility(ctx) -> None:
    pass  # TODO: utility command not classified

def step_0029_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(src=str(Path('\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\' + ctx.macro.named("SFOLDER") + '\\KM\\HIST') / 'HIST.txt'), dst='.')

def step_0030_rows_in_file(ctx) -> None:
    ctx.macro.set_named('HIST', str(ctx.csv_io.row_count('HIST.txt')))

def step_0032_write_file(ctx) -> None:
    ctx.write_file(path='HIST.csv', template='\nLOT,OUT_DATE\nDUMMY,2000-01-01 00:00:00')

def step_0033_write_file(ctx) -> None:
    ctx.write_file(path='HISTERROR.txt', template='\nERROR\nERROR\nERROR')

def step_0035_utility(ctx) -> None:
    pass  # TODO: utility command not classified

def step_0042_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(src='\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\ICMPCS_SUBPLANE_CSR_DLA\\Product_Lookup.csv', dst=str(Path('.\\') / Path('\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\ICMPCS_SUBPLANE_CSR_DLA\\Product_Lookup.csv').name))

def step_0043_sqlite_query(ctx) -> None:
    ctx.run_query(sql='\nSELECT /*L0*/ \n          a0.[site] AS [site]\n         ,a0.[prodgroup3] AS [prodgroup3]\n         ,a0.[upper_y_limit] AS [upper_y_limit]\n         ,a0.[lower_y_limit] AS [lower_y_limit]\n         ,a0.[upper_x_limit] AS [upper_x_limit]\n         ,a0.[lower_x_limit] AS [lower_x_limit]\nFROM \n[Product_Lookup] a0\n', output='CSR_Server_OIS_Product_List.csv', source_type='sqlite', inputs=['Product_Lookup.csv'], header=['site', 'prodgroup3', 'upper_y_limit', 'lower_y_limit', 'upper_x_limit', 'lower_x_limit'])

def step_0044_sql_query(ctx) -> None:
    ctx.run_query(sql="\n/*BEGIN SQL*/\nSELECT  DISTINCT \n          c0.ww AS site_work_week\n         ,f0.lot AS lot\n         ,f0.operation AS operation\n         ,To_Char(f0.load_date,'yyyy-mm-dd hh24:mi:ss') AS out_date\n         ,f0.route AS route\n         ,f0.owner AS owner\n         ,f0.oldqty1 AS oldqty1\n         ,f0.newqty1 AS newqty1\n         ,f4.entity AS entity\n         ,p.prodgroup3 AS prodgroup3\n         ,f0.facility AS facility\nFROM \n@[]@.F_LotHist f0\nINNER JOIN @[]@.F_Calendar c0 ON f0.last_action_date BETWEEN c0.start_date AND c0.end_date AND c0.event_code = 'S' AND decode(f0.facility,'RA3','AAL',f0.facility)= c0.facility\nLEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version\nINNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot\nLEFT JOIN @[]@.F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = 'Y'\n AND      f4.entity Like 'DIA%' \nLEFT JOIN @[]@.F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource\nLEFT JOIN @[]@.F_Entity en ON f4.entity = en.entity AND f4.facility = en.facility\nWHERE\nNVL(f0.history_deleted_flag,'N') = 'N'\nAND      f0.owner <> 'EMPTYFOUP'\n AND      p.prodgroup3 In \n" + ctx.sql_macros.sql_get_csv_list('.\\CSR_Server_OIS_Product_List.csv', 2, 'p.prodgroup3 In') + " \n AND      f0.operation In ('2090'\n,'1960') \n AND      f0.load_date >= (SYSDATE - 8/24) \n AND      f0.movedout_txn In ('MVOU') \n-- Tail A\n/*END SQL*/\n\n", output='CSR_Server_OIS_subplane_lotlist.csv', source_type='MARS', header=['site_work_week', 'lot', 'operation', 'out_date', 'route', 'owner', 'oldqty1', 'newqty1', 'entity', 'prodgroup3', 'facility'])

def step_0045_rows_in_file(ctx) -> None:
    ctx.macro.set_named('LOTS', str(ctx.csv_io.row_count('CSR_Server_OIS_subplane_lotlist.csv')))

def step_0047_sql_query(ctx) -> None:
    ctx.run_query(sql="\n/*BEGIN SQL*/\nSELECT \n          facility AS facility\n         ,lot AS lot\n         ,operation AS operation\n         ,To_Char(Max(test_end_date),'yyyy-mm-dd hh24:mi:ss') AS test_end_date\n         ,tester_id AS tester_id\n         ,program_name AS program_name\n         ,prodgroup3 AS prodgroup3\n         ,visual_id AS visual_id\n         ,tray_or_carrier_id AS tray_or_carrier_id\n         ,test_name AS test_name\n         ,ws_loss_code AS ws_loss_code\n         ,carrier_x AS carrier_x\n         ,carrier_y AS carrier_y\n         ,lane_number AS lane_number\n         ,Max(Sub_plane) AS Sub_plane\nFROM\n(\nSELECT \n          facility AS facility\n         ,lot AS lot\n         ,operation AS operation\n         ,test_end_date AS test_end_date\n         ,tester_id AS tester_id\n         ,program_name AS program_name\n         ,prodgroup3 AS prodgroup3\n         ,visual_id AS visual_id\n         ,tray_or_carrier_id AS tray_or_carrier_id\n         ,test_name AS test_name\n         ,ws_loss_code AS ws_loss_code\n         ,carrier_x AS carrier_x\n         ,carrier_y AS carrier_y\n         ,lane_number AS lane_number\n         ,TO_CHAR(  carrier_y   ||   carrier_x   ) AS Socket\n         ,Sub_plane AS Sub_plane\nFROM\n(\nSELECT \n          facility AS facility\n         ,lot AS lot\n         ,operation AS operation\n         ,test_end_date AS test_end_date\n         ,tester_id AS tester_id\n         ,program_name AS program_name\n         ,prodgroup3 AS prodgroup3\n         ,visual_id AS visual_id\n         ,tray_or_carrier_id AS tray_or_carrier_id\n         ,test_name AS test_name\n         ,ws_loss_code AS ws_loss_code\n         ,carrier_x AS carrier_x\n         ,carrier_y AS carrier_y\n         ,lane_number AS lane_number\n         ,Sub_plane AS Sub_plane\nFROM\n(\nSELECT  \n          ats.facility AS facility\n         ,ats.lot AS lot\n         ,ats.operation AS operation\n         ,ats.test_end_date_time AS test_end_date\n         ,ats.tester_id AS tester_id\n         ,ats.program_name AS program_name\n         ,mp.prodgroup3 AS prodgroup3\n         ,di.visual_id AS visual_id\n         ,dt.testing_session_tray_id AS tray_or_carrier_id\n         ,t.test_name AS test_name\n         ,dt.ws_loss_code AS ws_loss_code\n         ,dt.carrier_x AS carrier_x\n         ,dt.carrier_y AS carrier_y\n         ,dt.lane_number AS lane_number\n         ,CASE WHEN ctr.string_value IS NULL THEN to_char(ctr.numeric_result) ELSE ctr.string_value END AS Sub_plane\nFROM \nA_Testing_Session ats\nLEFT JOIN A_MARS_Lot ml ON ats.lot=ml.lot\nLEFT JOIN A_MARS_Product mp ON ml.product = mp.product AND ml.mars_schema=mp.mars_schema AND ats.facility = mp.facility\nINNER JOIN A_All_Component_Testing_Result ctr ON ctr.lao_start_ww = ats.lao_start_ww AND ctr.ts_id = ats.ts_id AND (ctr.numeric_result IS NOT NULL or ctr.string_value is NOT NULL)\nINNER JOIN A_Test t ON t.t_id = ctr.t_id\nINNER JOIN A_Device_Testing dt ON dt.lao_start_ww = ats.lao_start_ww AND dt.ts_id = ats.ts_id\nAND dt.lao_start_ww = ctr.lao_start_ww AND dt.ts_id = ctr.ts_id AND dt.dt_id = ctr.dt_id\nLEFT JOIN A_Device_Item di ON di.di_id = dt.di_id\nWHERE ats.data_domain='METROLOGY'\n AND      (ats.lot In \n" + ctx.sql_macros.sql_get_csv_list('.\\CSR_Server_OIS_subplane_lotlist.csv', 2, 'ats.lot In') + ') \n AND      (ats.operation In \n' + ctx.sql_macros.sql_get_csv_list('.\\CSR_Server_OIS_subplane_lotlist.csv', 3, 'ats.operation In') + ") \n AND      (ats.tester_id LIKE  'OIS%'\n) \n AND      t.test_name In ('SUBPLANEANGLEX'\n,'SUBPLANEANGLEY') \n AND      dt.ws_loss_code Is Null  \n)\n)\n)\nGROUP BY \n          facility\n         ,lot\n         ,operation\n         ,tester_id\n         ,program_name\n         ,prodgroup3\n         ,visual_id\n         ,tray_or_carrier_id\n         ,test_name\n         ,ws_loss_code\n         ,carrier_x\n         ,carrier_y\n         ,lane_number\n/*END SQL*/\n\n", output='yeuchuan_a0_15507.tab', source_type='ARIES', crosstab={'row_keys': ['facility', 'lot', 'operation', 'test_end_date', 'tester_id', 'program_name', 'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code', 'carrier_x', 'carrier_y', 'lane_number'], 'header_key': 'test_name', 'value_key': 'Sub_plane'})

def step_0048_sql_query(ctx) -> None:
    ctx.run_query(sql='\n/*BEGIN SQL*/\nSELECT  DISTINCT \n          z0.primary_entity AS entity\n         ,z2.bonding_station AS bond_station\n         ,z0.lot AS lot_2\n         ,z8.visual_id AS visual_id_1\nFROM \nARIES_Views.AV_dia_session z0\nLEFT JOIN ARIES_Views.AV_dia_media_testing z2 ON z2.lao_start_ww = z0.lao_start_ww AND z2.obj_s_id = z0.obj_s_id\nINNER JOIN ARIES_Views.AV_dia_Unit_Testing z8 ON z8.lao_start_ww = z2.lao_start_ww AND z8.obj_s_id = z2.obj_s_id AND z8.obj_mt_id = z2.obj_mt_id\nWHERE\n              (z0.lot In \n' + ctx.sql_macros.sql_get_csv_list('.\\yeuchuan_a0_15507.tab', 'lot', 'z0.lot In') + ") \n AND      z0.tool_entity Like 'TGB%' \n AND      (z0.operation In \n" + ctx.sql_macros.sql_get_csv_list('.\\yeuchuan_a0_15507.tab', 'operation', 'z0.operation In') + ') \n/*END SQL*/\n\n', output='yeuchuan_a2_15507.tab', source_type='ARIES', header=['entity', 'bond_station', 'lot_2', 'visual_id_1'])

def step_0049_sqlite_query(ctx) -> None:
    ctx.run_query(sql="\n\nDROP INDEX IF EXISTS IdxA2;\nCreate Index IF NOT EXISTS IdxA2 ON [yeuchuan_a2_15507] ([visual_id_1]);\n\nSELECT /*L0*/  DISTINCT \n          a0.[facility] AS [facility]\n         ,a0.[lot] AS [lot]\n         ,a0.[operation] AS [operation]\n         ,a0.[test_end_date] AS [test_end_date]\n         ,a0.[tester_id] AS [tester_id]\n         ,a0.[program_name] AS [program_name]\n         ,a0.[prodgroup3] AS [prodgroup3]\n         ,a0.[visual_id] AS [visual_id]\n         ,a0.[tray_or_carrier_id] AS [tray_or_carrier_id]\n         ,a0.[ws_loss_code] AS [ws_loss_code]\n         ,a2.[entity] AS [entity]\n         ,a2.[bond_station] AS [bond_station]\n         ,a0.[carrier_x] AS [carrier_x]\n         ,a0.[carrier_y] AS [carrier_y]\n         ,a0.[lane_number] AS [lane_number]\n         ,CrossTab->[[a0,15507;:Y]]\n         ,[entity]  ||  '_' || [bond_station]  ||  '_' ||  [carrier_x]  ||   '_' || [carrier_y] AS [Entity_BS_X_Y]\nFROM \n           [yeuchuan_a0_15507] a0\n LEFT OUTER JOIN [yeuchuan_a2_15507] a2\n  ON a0.[visual_id] = a2.[visual_id_1]\n", output='CSR_Server_OIS_subplane.csv', source_type='sqlite', inputs=['yeuchuan_a0_15507.tab', 'yeuchuan_a2_15507.tab'])

def step_0050_sqlite_query(ctx) -> None:
    ctx.run_query(sql="\n\nDROP INDEX IF EXISTS IdxA0;\nCreate Index IF NOT EXISTS IdxA0 ON [CSR_Server_OIS_Product_List] ([prodgroup3],[site]);\n\nSELECT /*L3*/  DISTINCT \n          [facility] AS [facility]\n         ,[lot] AS [lot]\n         ,[operation] AS [operation]\n         ,[test_end_date] AS [test_end_date]\n         ,[tester_id] AS [tester_id]\n         ,[program_name] AS [program_name]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[visual_id] AS [visual_id]\n         ,[tray_or_carrier_id] AS [tray_or_carrier_id]\n         ,[ws_loss_code] AS [ws_loss_code]\n         ,[entity] AS [entity]\n         ,[bond_station] AS [bond_station]\n         ,[carrier_x] AS [carrier_x]\n         ,[carrier_y] AS [carrier_y]\n         ,[lane_number] AS [lane_number]\n         ,[entity_bs_x_y] AS [entity_bs_x_y]\n         ,[site] AS [site]\n         ,[prodgroup3_1] AS [prodgroup3_1]\n         ,[sub_plane_x] AS [sub_plane_x]\n         ,[sub_plane_y] AS [sub_plane_y]\n         ,[lower_x_limit] AS [lower_x_limit]\n         ,[upper_x_limit] AS [upper_x_limit]\n         ,[lower_y_limit] AS [lower_y_limit]\n         ,[upper_y_limit] AS [upper_y_limit]\n         ,[Set_Limit_plane_X] AS [Set_Limit_plane_X]\n         ,[Set_Limit_plane_Y] AS [Set_Limit_plane_Y]\n         ,[Flag] AS [Flag]\n         ,DENSE_RANK () OVER (PARTITION BY  [entity_bs_x_y]  ORDER BY    [visual_id]    ASC) AS [Dense_rank]\nFROM\n(\nSELECT /*L2*/ \n          [facility] AS [facility]\n         ,[lot] AS [lot]\n         ,[operation] AS [operation]\n         ,[test_end_date] AS [test_end_date]\n         ,[tester_id] AS [tester_id]\n         ,[program_name] AS [program_name]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[visual_id] AS [visual_id]\n         ,[tray_or_carrier_id] AS [tray_or_carrier_id]\n         ,[ws_loss_code] AS [ws_loss_code]\n         ,[entity] AS [entity]\n         ,[bond_station] AS [bond_station]\n         ,[carrier_x] AS [carrier_x]\n         ,[carrier_y] AS [carrier_y]\n         ,[lane_number] AS [lane_number]\n         ,[entity_bs_x_y] AS [entity_bs_x_y]\n         ,[site] AS [site]\n         ,[prodgroup3_1] AS [prodgroup3_1]\n         ,[sub_plane_x] AS [sub_plane_x]\n         ,[sub_plane_y] AS [sub_plane_y]\n         ,[lower_x_limit] AS [lower_x_limit]\n         ,[upper_x_limit] AS [upper_x_limit]\n         ,[lower_y_limit] AS [lower_y_limit]\n         ,[upper_y_limit] AS [upper_y_limit]\n         ,[Set_Limit_plane_X] AS [Set_Limit_plane_X]\n         ,[Set_Limit_plane_Y] AS [Set_Limit_plane_Y]\n         ,CASE  WHEN   [Set_Limit_plane_Y]  = 'Y_flag' AND   [Set_Limit_plane_X]   <> 'X_flag' THEN 'Y_flag_only'  ELSE '' END AS [BeyondY_Flag]\n         ,CASE  WHEN  [Set_Limit_plane_Y]    = 'Y_flag' THEN 'flag'   ELSE '' END AS [Flag]\nFROM\n(\nSELECT /*L1*/ \n          [facility] AS [facility]\n         ,[lot] AS [lot]\n         ,[operation] AS [operation]\n         ,[test_end_date] AS [test_end_date]\n         ,[tester_id] AS [tester_id]\n         ,[program_name] AS [program_name]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[visual_id] AS [visual_id]\n         ,[tray_or_carrier_id] AS [tray_or_carrier_id]\n         ,[ws_loss_code] AS [ws_loss_code]\n         ,[entity] AS [entity]\n         ,[bond_station] AS [bond_station]\n         ,[carrier_x] AS [carrier_x]\n         ,[carrier_y] AS [carrier_y]\n         ,[lane_number] AS [lane_number]\n         ,[entity_bs_x_y] AS [entity_bs_x_y]\n         ,[site] AS [site]\n         ,[prodgroup3_1] AS [prodgroup3_1]\n         ,[sub_plane_x] AS [sub_plane_x]\n         ,[sub_plane_y] AS [sub_plane_y]\n         ,[lower_x_limit] AS [lower_x_limit]\n         ,[upper_x_limit] AS [upper_x_limit]\n         ,[lower_y_limit] AS [lower_y_limit]\n         ,[upper_y_limit] AS [upper_y_limit]\n         ,CASE WHEN     [sub_plane_x]    Not Between    [lower_x_limit]  AND     [upper_x_limit]  THEN 'X_flag' ELSE '' END AS [Set_Limit_plane_X]\n         ,CASE WHEN     [sub_plane_y]    Not Between    [lower_y_limit]    AND      [upper_y_limit]  THEN 'Y_flag' ELSE '' END AS [Set_Limit_plane_Y]\nFROM\n(\nSELECT /*L0*/  \n          a1.[facility] AS [facility]\n         ,a1.[lot] AS [lot]\n         ,a1.[operation] AS [operation]\n         ,a1.[test_end_date] AS [test_end_date]\n         ,a1.[tester_id] AS [tester_id]\n         ,a1.[program_name] AS [program_name]\n         ,a1.[prodgroup3] AS [prodgroup3]\n         ,a1.[visual_id] AS [visual_id]\n         ,a1.[tray_or_carrier_id] AS [tray_or_carrier_id]\n         ,a1.[ws_loss_code] AS [ws_loss_code]\n         ,a1.[entity] AS [entity]\n         ,a1.[bond_station] AS [bond_station]\n         ,a1.[carrier_x] AS [carrier_x]\n         ,a1.[carrier_y] AS [carrier_y]\n         ,a1.[lane_number] AS [lane_number]\n         ,a1.[entity_bs_x_y] AS [entity_bs_x_y]\n         ,a0.[site] AS [site]\n         ,a0.[prodgroup3] AS [prodgroup3_1]\n         ,CASE WHEN a1.[subplaneanglex] = '' THEN NULL ELSE CAST (a1.[subplaneanglex] AS REAL) END AS [sub_plane_x]\n         ,CASE WHEN a1.[subplaneangley] = '' THEN NULL ELSE CAST (a1.[subplaneangley] AS REAL) END AS [sub_plane_y]\n         ,CASE WHEN a0.[lower_x_limit] = '' THEN NULL ELSE CAST (a0.[lower_x_limit] AS REAL) END AS [lower_x_limit]\n         ,CASE WHEN a0.[upper_x_limit] = '' THEN NULL ELSE CAST (a0.[upper_x_limit] AS REAL) END AS [upper_x_limit]\n         ,CASE WHEN a0.[lower_y_limit] = '' THEN NULL ELSE CAST (a0.[lower_y_limit] AS REAL) END AS [lower_y_limit]\n         ,CASE WHEN a0.[upper_y_limit] = '' THEN NULL ELSE CAST (a0.[upper_y_limit] AS REAL) END AS [upper_y_limit]\nFROM \n           [CSR_Server_OIS_subplane] a1\n LEFT OUTER JOIN [CSR_Server_OIS_Product_List] a0\n  ON a0.[prodgroup3] = a1.[prodgroup3] \n AND a0.[site] = a1.[facility] \n) t /*L0*/\n) t /*L1*/\n) t /*L2*/\nWHERE\n              [Flag] = 'flag'\n", output='CSR_Server_OIS_subplane_interim.csv', source_type='sqlite', inputs=['CSR_Server_OIS_subplane.csv', 'CSR_Server_OIS_Product_List.csv'], header=['facility', 'lot', 'operation', 'test_end_date', 'tester_id', 'program_name', 'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code', 'entity', 'bond_station', 'carrier_x', 'carrier_y', 'lane_number', 'entity_bs_x_y', 'site', 'prodgroup3_1', 'sub_plane_x', 'sub_plane_y', 'lower_x_limit', 'upper_x_limit', 'lower_y_limit', 'upper_y_limit', 'Set_Limit_plane_X', 'Set_Limit_plane_Y', 'Flag', 'Dense_rank'])

def step_0051_sqlite_query(ctx) -> None:
    ctx.run_query(sql="\nSELECT /*L0*/ \n          a0.[facility] AS [facility]\n         ,a0.[lot] AS [lot]\n         ,a0.[operation] AS [operation]\n         ,a0.[test_end_date] AS [test_end_date]\n         ,a0.[tester_id] AS [tester_id]\n         ,a0.[program_name] AS [program_name]\n         ,a0.[prodgroup3] AS [prodgroup3]\n         ,a0.[visual_id] AS [visual_id]\n         ,a0.[tray_or_carrier_id] AS [tray_or_carrier_id]\n         ,a0.[ws_loss_code] AS [ws_loss_code]\n         ,a0.[entity] AS [entity]\n         ,a0.[bond_station] AS [bond_station]\n         ,a0.[carrier_x] AS [carrier_x]\n         ,a0.[carrier_y] AS [carrier_y]\n         ,a0.[lane_number] AS [lane_number]\n         ,a0.[entity_bs_x_y] AS [entity_bs_x_y]\n         ,a0.[site] AS [site]\n         ,a0.[prodgroup3_1] AS [prodgroup3_1]\n         ,a0.[sub_plane_x] AS [sub_plane_x]\n         ,a0.[sub_plane_y] AS [sub_plane_y]\n         ,a0.[lower_x_limit] AS [lower_x_limit]\n         ,a0.[upper_x_limit] AS [upper_x_limit]\n         ,a0.[lower_y_limit] AS [lower_y_limit]\n         ,a0.[upper_y_limit] AS [upper_y_limit]\n         ,a0.[set_limit_plane_x] AS [set_limit_plane_x]\n         ,a0.[set_limit_plane_y] AS [set_limit_plane_y]\n         ,a0.[flag] AS [flag]\n         ,a0.[dense_rank] AS [dense_rank]\n         ,'CSR_HOLD' AS [CSR_trigger]\nFROM \n[CSR_Server_OIS_subplane_interim] a0\nWHERE\n              a0.[dense_rank] Not In ('1'\n,'2')\n", output='CSR_Server_OIS_subplane_output.csv', source_type='sqlite', inputs=['CSR_Server_OIS_subplane_interim.csv'], header=['facility', 'lot', 'operation', 'test_end_date', 'tester_id', 'program_name', 'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code', 'entity', 'bond_station', 'carrier_x', 'carrier_y', 'lane_number', 'entity_bs_x_y', 'site', 'prodgroup3_1', 'sub_plane_x', 'sub_plane_y', 'lower_x_limit', 'upper_x_limit', 'lower_y_limit', 'upper_y_limit', 'set_limit_plane_x', 'set_limit_plane_y', 'flag', 'dense_rank', 'CSR_trigger'])

def step_0052_rows_in_file(ctx) -> None:
    ctx.macro.set_named('FLAG', str(ctx.csv_io.row_count('CSR_Server_OIS_subplane_output.csv')))

def step_0054_sqlite_query(ctx) -> None:
    ctx.run_query(sql='\nSELECT /*L0*/ \n          a0.[facility] AS [facility]\n         ,a0.[lot] AS [lot]\n         ,a0.[prodgroup3] AS [prodgroup3]\n         ,a0.[operation] AS [DLA_operation]\n         ,a0.[entity] AS [entity]\n         ,a0.[bond_station] AS [bond_station]\n         ,a0.[carrier_x] AS [carrier_x]\n         ,a0.[carrier_y] AS [carrier_y]\n         ,a0.[visual_id] AS [visual_id]\n         ,a0.[sub_plane_x] AS [sub_plane_x]\n         ,a0.[sub_plane_y] AS [sub_plane_y]\n         ,a0.[lower_x_limit] AS [lower_x_limit]\n         ,a0.[upper_x_limit] AS [upper_x_limit]\n         ,a0.[lower_y_limit] AS [lower_y_limit]\n         ,a0.[upper_y_limit] AS [upper_y_limit]\nFROM \n[CSR_Server_OIS_subplane_output] a0\nWHERE\n NOT          (a0.[lot] In \n' + ctx.sql_macros.sql_get_csv_list('.\\HIST.csv', 1, 'a0.[lot] In') + ')\n', output='yeuchuan_SQL_15507.tab', source_type='sqlite', inputs=['CSR_Server_OIS_subplane_output.csv'], header=['facility', 'lot', 'prodgroup3', 'DLA_operation', 'entity', 'bond_station', 'carrier_x', 'carrier_y', 'visual_id', 'sub_plane_x', 'sub_plane_y', 'lower_x_limit', 'upper_x_limit', 'lower_y_limit', 'upper_y_limit'])

def step_0055_sql_query(ctx) -> None:
    ctx.run_query(sql="\n/*BEGIN SQL*/\nSELECT \n          f0.lot AS lot_1\n         ,f0.operation AS Current_operation\n         ,f0.movedin AS movedin\n         ,f0.onrework AS onrework\n         ,f0.onhold AS onhold\n         ,f0.route AS route\n         ,f0.qty1 AS quantity\nFROM \n@[]@.F_Lot f0\nWHERE f0.owner <> 'EMPTYFOUP'\n AND      f0.terminated = 'N' \n AND      f0.qty1 > 0 \n AND      f0.src_erase_date Is Null  \n AND      (f0.lot In \n" + ctx.sql_macros.sql_get_csv_list('.\\yeuchuan_SQL_15507.tab', 'lot', 'f0.lot In') + ') \n/*END SQL*/\n\n', output='yeuchuan_a1_15507.tab', source_type='MARS', header=['lot_1', 'Current_operation', 'movedin', 'onrework', 'onhold', 'route', 'quantity'])

def step_0056_sqlite_query(ctx) -> None:
    ctx.run_query(sql="\n\nDROP INDEX IF EXISTS IdxA1;\nCreate Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_15507] ([lot_1]);\n\nSELECT /*L1*/  DISTINCT \n          [facility] AS [facility]\n         ,[lot] AS [lot]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[DLA_operation] AS [DLA_operation]\n         ,[lot_1] AS [lot_1]\n         ,[Current_operation] AS [Current_operation]\n         ,[movedin] AS [movedin]\n         ,[onrework] AS [onrework]\n         ,[onhold] AS [onhold]\n         ,[route] AS [route]\n         ,[quantity] AS [quantity]\n         ,[Lot_MVIN_CURE] AS [Lot_MVIN_CURE]\n         ,[entity] AS [entity]\n         ,[bond_station] AS [bond_station]\n         ,[carrier_x] AS [carrier_x]\n         ,[carrier_y] AS [carrier_y]\n         ,[visual_id] AS [visual_id]\n         ,[sub_plane_x] AS [sub_plane_x]\n         ,[sub_plane_y] AS [sub_plane_y]\n         ,[lower_x_limit] AS [lower_x_limit]\n         ,[upper_x_limit] AS [upper_x_limit]\n         ,[lower_y_limit] AS [lower_y_limit]\n         ,[upper_y_limit] AS [upper_y_limit]\nFROM\n(\nSELECT /*L0*/  \n          sql.[facility] AS [facility]\n         ,sql.[lot] AS [lot]\n         ,sql.[prodgroup3] AS [prodgroup3]\n         ,sql.[DLA_operation] AS [DLA_operation]\n         ,a1.[lot_1] AS [lot_1]\n         ,a1.[Current_operation] AS [Current_operation]\n         ,a1.[movedin] AS [movedin]\n         ,a1.[onrework] AS [onrework]\n         ,a1.[onhold] AS [onhold]\n         ,a1.[route] AS [route]\n         ,a1.[quantity] AS [quantity]\n         ,CASE  WHEN [Current_operation]  IN ('1266') THEN 'N' WHEN [Current_operation]  IN ('1501') THEN 'N' WHEN [Current_operation]  IN ('1366') THEN 'N' WHEN [Current_operation]  IN ('1265') THEN 'N' WHEN [Current_operation]  IN ('1264') THEN 'N'  ELSE 'Y' END AS [Lot_MVIN_CURE]\n         ,sql.[entity] AS [entity]\n         ,sql.[bond_station] AS [bond_station]\n         ,sql.[carrier_x] AS [carrier_x]\n         ,sql.[carrier_y] AS [carrier_y]\n         ,sql.[visual_id] AS [visual_id]\n         ,sql.[sub_plane_x] AS [sub_plane_x]\n         ,sql.[sub_plane_y] AS [sub_plane_y]\n         ,sql.[lower_x_limit] AS [lower_x_limit]\n         ,sql.[upper_x_limit] AS [upper_x_limit]\n         ,sql.[lower_y_limit] AS [lower_y_limit]\n         ,sql.[upper_y_limit] AS [upper_y_limit]\nFROM \n           [yeuchuan_SQL_15507] sql\n LEFT OUTER JOIN [yeuchuan_a1_15507] a1\n  ON sql.[lot] = a1.[lot_1] \n) t /*L0*/\nWHERE\n              [Lot_MVIN_CURE] = 'Y'\n", output='Data.csv', source_type='sqlite', inputs=['yeuchuan_SQL_15507.tab', 'yeuchuan_a1_15507.tab'], header=['facility', 'lot', 'prodgroup3', 'DLA_operation', 'lot_1', 'Current_operation', 'movedin', 'onrework', 'onhold', 'route', 'quantity', 'Lot_MVIN_CURE', 'entity', 'bond_station', 'carrier_x', 'carrier_y', 'visual_id', 'sub_plane_x', 'sub_plane_y', 'lower_x_limit', 'upper_x_limit', 'lower_y_limit', 'upper_y_limit'])

def run() -> None:
    ctx = PipelineContext()
    step_0000_html_report(ctx)
    step_0001_html_report(ctx)
    step_0002_html_report(ctx)
    step_0003_write_file(ctx)
    step_0004_write_file(ctx)
    step_0005_external(ctx)
    for __row in ctx.csv_io.iter('macrotmp.csv'):
        with ctx.macro_scope(__row):
            step_0007_external(ctx)
    step_0009_fs_delete(ctx)
    for __row in ctx.csv_io.iter('ctime.csv'):
        with ctx.macro_scope(__row):
            step_0011_rows_in_file(ctx)
            if int(ctx.macro.named("CONFIG")) <= int('0'):
                step_0013_utility(ctx)
            else:
                step_0015_sqlite_query(ctx)
                step_0016_rows_in_file(ctx)
                if int(ctx.macro.named("CONFIGSETS")) != int('1'):
                    step_0018_utility(ctx)
                else:
                    for __row in ctx.csv_io.iter('configsets.csv'):
                        with ctx.macro_scope(__row):
                            if ctx.macro.named("CSRV") == 'FAIL' and ctx.macro.named("UNDERDEV") == 'N':
                                step_0022_write_file(ctx)
                                step_0023_utility(ctx)
                            if ctx.macro.named("MMSV") == 'FAIL' and ctx.macro.named("UNDERDEV") == 'N':
                                step_0026_write_file(ctx)
                                step_0027_utility(ctx)
                            step_0029_fs_copy(ctx)
                            step_0030_rows_in_file(ctx)
                            if int(ctx.macro.named("HIST")) <= int('0'):
                                step_0032_write_file(ctx)
                                step_0033_write_file(ctx)
                            else:
                                step_0035_utility(ctx)
    for __row in ctx.csv_io.iter('configsets.csv'):
        with ctx.macro_scope(__row):
            step_0042_fs_copy(ctx)
            step_0043_sqlite_query(ctx)
            step_0044_sql_query(ctx)
            step_0045_rows_in_file(ctx)
            if int(ctx.macro.named("LOTS")) > int('0'):
                step_0047_sql_query(ctx)
                step_0048_sql_query(ctx)
                step_0049_sqlite_query(ctx)
                step_0050_sqlite_query(ctx)
                step_0051_sqlite_query(ctx)
                step_0052_rows_in_file(ctx)
                if int(ctx.macro.named("FLAG")) > int('0'):
                    step_0054_sqlite_query(ctx)
                    step_0055_sql_query(ctx)
                    step_0056_sqlite_query(ctx)

if __name__ == "__main__":
    run()