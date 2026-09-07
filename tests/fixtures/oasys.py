# SQL statements containing filters:
# - step_0000_sql_query (Line 1231): filters on v1.operation, v1.transaction_datetime, v3.latest_flag, v3.status, v4.equipment_sequence

# Auto-generated Python script from VG2
"""Pipeline implementation."""


from __future__ import annotations
from abc import ABC, abstractmethod
from collections.abc import Mapping, Sequence
from contextlib import contextmanager
from dataclasses import dataclass
from datasyncx.readers.oracle_reader import OracleReader
from enum import Enum
from pathlib import Path
from typing import Any
from typing import Any, Callable
from typing import Any, Callable, ContextManager
from typing import Any, ClassVar
from typing import Any, ClassVar, TYPE_CHECKING
from typing import Any, Iterator
from typing import Iterator, Protocol
import ast
import csv
import inspect
import logging
import os
import pandas
import pandas as pd
import re
import shlex
import shutil




class Kind(str, Enum):
    SQL_QUERY = "SQL_QUERY"
    SQLITE_QUERY = "SQLITE_QUERY"
    WRITE_FILE = "WRITE_FILE"
    PYTHON_EMBED = "PYTHON_EMBED"
    FS_COPY = "FS_COPY"
    FS_DELETE = "FS_DELETE"
    EXTERNAL_RUN = "EXTERNAL_RUN"
    WAIT_FILE = "WAIT_FILE"
    HTML_REPORT = "HTML_REPORT"
    EMAIL = "EMAIL"
    MACRO_CONTROL = "MACRO_CONTROL"
    ROWS_IN_FILE = "ROWS_IN_FILE"
    UNKNOWN = "UNKNOWN"

    @property
    def is_csv_producer(self) -> bool:
        """Return True if this kind is an explicit CSV producer."""
        return self in {
            Kind.SQL_QUERY,
            Kind.SQLITE_QUERY,
            Kind.WRITE_FILE,
            Kind.PYTHON_EMBED,
        }

    @property
    def is_external_utility(self) -> bool:
        """Return True if this kind represents an external utility/system command block."""
        return self in {
            Kind.EMAIL,
            Kind.EXTERNAL_RUN,
            Kind.FS_COPY,
            Kind.FS_DELETE,
            Kind.WAIT_FILE,
        }

_CLASS_SIG_RE = re.compile(r"^(\s*class\s+\w+)\(.*\):\s*$")

def _find_class_def(source: str, class_name: str) -> ast.ClassDef | None:
    tree = ast.parse(source)
    for node in ast.walk(tree):
        if isinstance(node, ast.ClassDef) and node.name == class_name:
            return node
    return None

def _strip_embed_artifacts(source: str, class_name: str) -> str:
    lines = source.split("\n")

    while lines and lines[0].lstrip().startswith("@"):
        lines.pop(0)

    if not lines:
        return ""

    lines[0] = _CLASS_SIG_RE.sub(r"\1:", lines[0])
    lines[0] = lines[0].replace(f"({EmitterUtility.__name__}):", ":")
    lines[0] = lines[0].replace(f"({UtilitySpec.__name__}):", ":")
    lines[0] = lines[0].replace(
        f"({class_name}, {UtilitySpec.__name__}):", f"({class_name}):"
    )

    lines = [
        line
        for line in lines
        if not line.lstrip().startswith("handles =") and "@emittable" not in line
    ]

    return "\n".join(lines).rstrip()

class UtilitySpec(ABC):
    """Base contract for all embeddable utilities."""

    utility_name: ClassVar[str]
    handles: ClassVar[tuple[Kind, ...]] = ()
    # Forced-in regardless of which block Kinds the workflow uses (e.g. PipelineContext/Logger
    # are referenced unconditionally by every generated script).
    always_include: ClassVar[bool] = False
    _registry: ClassVar[dict[str, type[UtilitySpec]]] = {}
    _emit_handlers: ClassVar[dict[Kind, type[UtilitySpec]]] = {}

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
            owner = UtilitySpec._emit_handlers.get(handled_kind)
            if owner is not None and owner is not cls:
                raise ValueError(
                    "duplicate handler for "
                    f"{handled_kind}: {owner.__name__} and {cls.__name__}"
                )
            UtilitySpec._emit_handlers[handled_kind] = cls

    @classmethod
    def get_source(cls, source_override: str | None = None) -> str:
        """Return this utility's embeddable source.

        ``source_override``, when given, is a whole-file source string (already
        cleaned of promoted inline imports -- see ``utilities/__init__.py``) to
        extract this class's definition from instead of live ``inspect.getsource``.
        """
        custom = getattr(cls, "__vg2c_source__", None)
        if custom is not None:
            return str(custom).rstrip()

        if source_override is not None:
            node = _find_class_def(source_override, cls.__name__)
            if node is not None:
                segment = ast.get_source_segment(source_override, node)
                if segment is not None:
                    return _strip_embed_artifacts(segment, cls.__name__)

        source = inspect.getsource(cls)
        return _strip_embed_artifacts(source, cls.__name__)

    @classmethod
    def registered(cls) -> tuple[type[UtilitySpec], ...]:
        """Return loaded utilities in deterministic registration order."""

        return tuple(cls._registry.values())

    @classmethod
    def for_name(cls, name: str) -> type[UtilitySpec] | None:
        """Return the loaded utility registered under *name*."""

        return cls._registry.get(name)

    @classmethod
    def for_kind(cls, kind: Kind) -> type[UtilitySpec] | None:
        """Return the loaded emitter for *kind*, falling back to UNKNOWN."""

        return cls._emit_handlers.get(kind) or cls._emit_handlers.get(Kind.UNKNOWN)

    @staticmethod
    def emit_block(block: Any) -> list[str] | tuple[str, list[str]] | None:
        return None

    @staticmethod
    def _step_name(block: Any, suffix: str) -> str:
        return f"step_{block.index:04d}_{suffix}"

    @staticmethod
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

    @classmethod
    def _wrap_in_step(
        cls, subclass: type[UtilitySpec], block: Any, result: Any
    ) -> tuple[str, str] | None:
        if result is None:
            return None
        if (
            isinstance(result, tuple)
            and len(result) == 2
            and isinstance(result[1], list)
        ):
            suffix, body_lines = result
        else:
            suffix = getattr(subclass, "utility_name", "utility")
            body_lines = result
        return cls._emit_step_source(cls._step_name(block, suffix), body_lines)

    @classmethod
    def dispatch_and_emit(cls, block: Any) -> tuple[str, str]:
        handler_cls = cls._emit_handlers.get(block.kind)
        if handler_cls is not None:
            emitted = handler_cls.emit_block(block)
            if emitted is not None:
                wrapped = cls._wrap_in_step(handler_cls, block, emitted)
                if wrapped is not None:
                    return wrapped
        return "", ""

class EmitterUtility(UtilitySpec):
    """Utility that participates in Stage 1 classification and block emission."""

    _check_handlers: ClassVar[list[type[EmitterUtility]]] = []

    def __init_subclass__(cls, **kwargs: Any) -> None:
        super().__init_subclass__(**kwargs)

        if inspect.isabstract(cls):
            return

        EmitterUtility._check_handlers.append(cls)

    @staticmethod
    @abstractmethod
    def check(options: BlockOptions) -> tuple[Kind, str] | None:
        raise NotImplementedError

    @classmethod
    @abstractmethod
    def emit_block(cls, block: Any) -> list[str] | tuple[str, list[str]] | None:
        raise NotImplementedError

    @classmethod
    def iter_checks(cls) -> tuple[type[EmitterUtility], ...]:
        return tuple(cls._check_handlers)

def strip_quotes(value: str) -> str:
    value = value.strip()
    if len(value) >= 2 and value[0] == value[-1] and value[0] in {'"', "'"}:
        return value[1:-1]
    return value

def split_utility_command(text: str) -> list[str]:
    text = text.strip()
    if not text:
        return []

    lexer = shlex.shlex(text, posix=False)
    lexer.whitespace_split = True
    lexer.commenters = ""
    return list(lexer)

def resolve_output_path(block: Any) -> str:
    csv_value = block.resolved_options.lookup.get("CSV")
    if csv_value:
        return strip_quotes(csv_value)

    write_file_value = block.resolved_options.lookup.get("WRITE-FILE")
    if write_file_value:
        candidate = strip_quotes(write_file_value)
        if candidate.upper() not in {"Y", "N"}:
            return candidate

    suffix = "txt" if block.kind in {Kind.WRITE_FILE, Kind.PYTHON_EMBED} else "csv"
    return f"step_{block.index:04d}.{suffix}"

def resolve_path(name: str | Path, *, for_write: bool = False) -> Path:
    path = Path(name)
    script_file = globals().get("__file__")
    if script_file and Path(script_file).name != "_emit_helpers.py":
        base_dir = Path(script_file).resolve().parent
    else:
        base_dir = Path.cwd()

    if path.is_absolute():
        if for_write:
            return path
        if path.exists():
            return path
        local_fallback = Path(path.name)
        if local_fallback.exists():
            return local_fallback
        return path

    base_path = base_dir / path
    if for_write:
        return base_path

    if path.exists():
        return path
    if base_path.exists():
        return base_path
    return base_path

def normalize_macro_name(raw: str) -> str:
    name = raw.strip()
    if name.startswith("<<<") and name.endswith(">>>"):
        name = name[3:-3]
    return name.strip().upper()

class Logger:
    """Shared logger utility used by translator code and generated scripts."""

    utility_name = "logger"
    always_include = True

    CRITICAL: ClassVar[int] = logging.CRITICAL
    ERROR: ClassVar[int] = logging.ERROR
    WARNING: ClassVar[int] = logging.WARNING
    INFO: ClassVar[int] = logging.INFO
    DEBUG: ClassVar[int] = logging.DEBUG
    NOTSET: ClassVar[int] = logging.NOTSET

    _logger_class_configured: ClassVar[bool] = False

    class PrettyLogger(logging.Logger):
        def table(
            self,
            rows: Sequence[Mapping[str, Any]] | Sequence[Sequence[Any]],
            *,
            headers: Sequence[str] | None = None,
            title: str | None = None,
            level: int = logging.INFO,
        ) -> None:
            self.log(
                level,
                Logger._format_table(rows, headers=headers, title=title),
            )

    @staticmethod
    def _format_table(
        rows: Sequence[Mapping[str, Any]] | Sequence[Sequence[Any]],
        headers: Sequence[str] | None = None,
        title: str | None = None,
    ) -> str:
        if not rows:
            return f"{title}\n<empty table>" if title else "<empty table>"

        first = rows[0]
        body: list[list[str]] = []

        if isinstance(first, Mapping):
            cols = list(headers) if headers else []
            if not cols:
                for row in rows:
                    if not isinstance(row, Mapping):
                        raise TypeError("Mixed table row types are not supported.")
                    for key in row:
                        key_s = str(key)
                        if key_s not in cols:
                            cols.append(key_s)
            for row in rows:
                if not isinstance(row, Mapping):
                    raise TypeError("Mixed table row types are not supported.")
                body.append([str(row.get(c, "")) for c in cols])
        else:
            cols = (
                [str(h) for h in headers]
                if headers
                else [f"col_{i+1}" for i in range(max(len(r) for r in rows))]
            )
            for row in rows:
                if isinstance(row, Mapping):
                    raise TypeError("Mixed table row types are not supported.")
                vals = [str(v) for v in row]
                if len(vals) < len(cols):
                    vals.extend([""] * (len(cols) - len(vals)))
                body.append(vals)

        widths = [len(c) for c in cols]
        for row in body:
            for i, value in enumerate(row):
                widths[i] = max(widths[i], len(value))

        border = "+" + "+".join("-" * (w + 2) for w in widths) + "+"
        header = (
            "| " + " | ".join(cols[i].ljust(widths[i]) for i in range(len(cols))) + " |"
        )
        lines = [
            "| " + " | ".join(row[i].ljust(widths[i]) for i in range(len(cols))) + " |"
            for row in body
        ]
        out = [border, header, border, *lines, border]
        return (title + "\n" if title else "") + "\n".join(out)

    @classmethod
    def _ensure_logger_class(cls) -> None:
        if cls._logger_class_configured:
            return
        logging.setLoggerClass(cls.PrettyLogger)
        cls._logger_class_configured = True

    @classmethod
    def basicConfig(
        cls,
        *,
        level: int | str = logging.INFO,
        format: str = "%(asctime)s | %(levelname)-8s | %(name)s | %(message)s",
        datefmt: str = "%Y-%m-%d %H:%M:%S",
    ) -> None:
        cls._ensure_logger_class()
        logging.basicConfig(level=level, format=format, datefmt=datefmt)

    @classmethod
    def getLogger(cls, name: str | None = None) -> PrettyLogger:
        cls._ensure_logger_class()
        return logging.getLogger(name)  # type: ignore[return-value]

    @classmethod
    def table(
        cls,
        rows: Sequence[Mapping[str, Any]] | Sequence[Sequence[Any]],
        *,
        headers: Sequence[str] | None = None,
        title: str | None = None,
        level: int = logging.INFO,
        name: str | None = None,
    ) -> None:
        cls.getLogger(name).table(rows, headers=headers, title=title, level=level)

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

            body = "\n         ,".join(f"{alias}.[{c}] AS [{c}]" for c in dynamic_cols)
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
    """Read and write CSV files relative to the runtime script directory."""

    utility_name = "csv_io"

    _CALL_RE = re.compile(r"\bSQL_Get_CSV_List\s*\(", re.IGNORECASE)

    # Detects an ``(<col> In `` wrap immediately preceding the call site -- an
    # unmatched ``(`` that historically relied on macro expansion to close it.
    _CALL_SITE_WRAP_RE = re.compile(
        r"\(\s*[A-Za-z_][\w.\[\]@]*\s+In\s*$", re.IGNORECASE
    )

    @dataclass(frozen=True, slots=True)
    class SqlGetCsvListCall:
        """A well-formed ``SQL_Get_CSV_List(csv_path, column_ref, lead_in)`` call."""

        start: int
        end: int
        csv_path: str
        column_ref: int | str
        lead_in: str
        needs_closing_paren: bool

    @staticmethod
    def scan_sql_get_csv_list_calls(body: str) -> list[CsvIO.SqlGetCsvListCall]:
        """Return every well-formed ``SQL_Get_CSV_List(...)`` call in source order.

        Malformed calls (wrong arg count, unbalanced parens) are skipped.
        """
        calls: list[CsvIO.SqlGetCsvListCall] = []
        cursor = 0
        while True:
            match = CsvIO._CALL_RE.search(body, cursor)
            if match is None:
                break
            open_paren = body.find("(", match.start())
            if open_paren == -1:
                break
            close_paren = CsvIO._find_matching_paren(body, open_paren)
            if close_paren == -1:
                break
            args = CsvIO._split_args(body[open_paren + 1 : close_paren])
            next_cursor = close_paren + 1
            if len(args) == 3:
                calls.append(
                    CsvIO.SqlGetCsvListCall(
                        start=match.start(),
                        end=next_cursor,
                        csv_path=CsvIO._unquote(args[0]),
                        column_ref=CsvIO._parse_column_ref(args[1]),
                        lead_in=CsvIO._unquote(args[2]),
                        needs_closing_paren=bool(
                            CsvIO._CALL_SITE_WRAP_RE.search(body[: match.start()])
                        ),
                    )
                )
            cursor = next_cursor
        return calls

    @staticmethod
    def _find_matching_paren(text: str, open_idx: int) -> int:
        depth = 0
        in_single = False
        in_double = False
        for i in range(open_idx, len(text)):
            ch = text[i]
            prev = text[i - 1] if i > 0 else ""
            if ch == "'" and prev != "\\" and not in_double:
                in_single = not in_single
            elif ch == '"' and prev != "\\" and not in_single:
                in_double = not in_double
            elif not in_single and not in_double:
                if ch == "(":
                    depth += 1
                elif ch == ")":
                    depth -= 1
                    if depth == 0:
                        return i
        return -1

    @staticmethod
    def _split_args(args_text: str) -> list[str]:
        args: list[str] = []
        current: list[str] = []
        depth = 0
        in_single = False
        in_double = False
        for i, ch in enumerate(args_text):
            prev = args_text[i - 1] if i > 0 else ""
            if ch == "'" and prev != "\\" and not in_double:
                in_single = not in_single
                current.append(ch)
                continue
            if ch == '"' and prev != "\\" and not in_single:
                in_double = not in_double
                current.append(ch)
                continue
            if not in_single and not in_double:
                if ch == "(":
                    depth += 1
                elif ch == ")" and depth > 0:
                    depth -= 1
                elif ch == "," and depth == 0:
                    args.append("".join(current).strip())
                    current = []
                    continue
            current.append(ch)
        if current:
            args.append("".join(current).strip())
        return args

    @staticmethod
    def _unquote(value: str) -> str:
        stripped = value.strip()
        if (
            len(stripped) >= 2
            and stripped[0] == stripped[-1]
            and stripped[0] in {"'", '"'}
        ):
            return stripped[1:-1]
        return stripped

    @staticmethod
    def _parse_column_ref(raw: str) -> int | str:
        value = CsvIO._unquote(raw)
        return int(value) if value.isdigit() else value

    # ------------------------------------------------------------------
    # Read
    # ------------------------------------------------------------------

    def iter(self, name: str) -> Iterator[dict[str, str]]:
        """Yield each data row as a dict keyed by header names."""
        path = resolve_path(name)
        with path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.DictReader(fh)
            yield from reader

    def single_row(self, name: str) -> dict[str, str]:
        """Return exactly one data row from *name*; raise on 0 or >1 rows."""
        rows = self.iter(name)
        first = next(rows, None)
        if first is None:
            raise ValueError(f"CSV '{name}' must contain exactly 1 data row; found 0")
        second = next(rows, None)
        if second is not None:
            raise ValueError(f"CSV '{name}' must contain exactly 1 data row; found >1")
        return first

    def _read_column(self, path: str, column_ref: int | str) -> list[str]:
        """Read a column from a CSV file."""
        rows: list[str] = []
        resolved_path = resolve_path(path)

        with resolved_path.open(newline="", encoding="utf-8", errors="replace") as fh:
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

    def row_count(self, name: str) -> int:
        """Count data rows (excludes header); 0 if file missing."""
        path = resolve_path(name)
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
        in_path = resolve_path(input_name)
        out_path = resolve_path(chunk_name, for_write=True)
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
        path = resolve_path(name, for_write=True)
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
            pass

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
    always_include = True

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

    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        content = self.macro.substitute(template, vars=vars)
        self.fs_ops.write_file(path, content)

    def _read_datasyncx(self, sql: str, reader: Any, node: str):
        result = reader.read(site=node, query=sql)
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
        node: str | None = None,
    ):
        sql = self.macro.substitute(sql)
        # precedence: explicit override > script default (ctx.macro "NODE") > global env setting > legacy default
        effective_node = (
            node or self.macro.named("NODE") or os.environ.get("VG2C_DEFAULT_NODE", "KM")
        )

        if hasattr(reader, "execute"):
            result = reader.execute(sql, inputs or [])
        else:
            result = self._read_datasyncx(sql, reader, effective_node)

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

class MacroState:
    """Stack of variable frames; lookups walk top-to-bottom."""

    utility_name = "macro"

    PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
    NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")
    _MACRO_CONTROL_TOKEN_RE = re.compile(
        r"^\s*\{(START-MACRO|END-MACRO|IF-THEN|ELSE|END-IF|RUN-LOOP|END-LOOP)\}",
        re.IGNORECASE,
    )

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        utilities = options.lookup.get("UTILITIES", "")
        if MacroState._MACRO_CONTROL_TOKEN_RE.match(utilities):
            return Kind.MACRO_CONTROL, "/UTILITIES is a macro control token"
        return None

    @classmethod
    def to_py_expr(cls, value: str | None) -> str:
        if value is None:
            return "None"
        return cls.placeholders_to_python_expr(strip_quotes(value))

    @classmethod
    def placeholders_to_python_expr(cls, text: str) -> str:
        if not text:
            return repr("")

        parts: list[str] = []
        cursor = 0

        for match in cls.PLACEHOLDER_RE.finditer(text):
            literal = text[cursor : match.start()]
            if literal:
                parts.append(repr(literal))

            named = match.group(1)
            if named is not None:
                parts.append(cls.named.render(repr(normalize_macro_name(named))))
            else:
                parts.append(cls.positional.render())

            cursor = match.end()

        tail = text[cursor:]
        if tail:
            parts.append(repr(tail))

        if not parts:
            return repr(text)
        if len(parts) == 1:
            return parts[0]
        return " + ".join(parts)

    @classmethod
    def emit_block(cls, block) -> tuple[str, list[str]] | None:
        return "macro_control", ["pass"]

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

    def substitute(self, text: str, vars: dict[str, str] | None = None) -> str:
        if not text:
            return ""

        def _lookup(name: str) -> str:
            key = normalize_macro_name(name)
            if vars is not None:
                return vars.get(key, "")
            return self.named(key)

        def _replace(match: re.Match[str]) -> str:
            named = match.group(1)
            if named is not None:
                return _lookup(named)
            return self.positional()

        content = self.PLACEHOLDER_RE.sub(_replace, text)
        return content.lstrip("\n")

    def resolve_file_path(self, raw_path: str) -> Path:
        """Resolve a possibly-macro path with local basename fallback for abs paths."""
        if not raw_path:
            return Path("")
        resolved = self.substitute(raw_path)
        return resolve_path(resolved)

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

class SqliteEngine:
    """Emit query calls for external and SQLite readers."""

    utility_name = "sqlite_engine"

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if options.lookup.get("OLEDB", "").upper() == "SQLITE":
            return Kind.SQLITE_QUERY, "/OLEDB=SQLite"
        if options.lookup.get("ENGINE", "").upper() == "SQLITE":
            return Kind.SQLITE_QUERY, "/ENGINE=SQLite"

        node = options.lookup.get("NODE", "")
        engine = options.lookup.get("ENGINE", "")
        oledb = options.lookup.get("OLEDB", "")
        if engine.upper() not in {"VA"} and oledb.upper() not in {"SQLPLUS"}:
            return None

        if any(
            SqliteEngine._node_matches(node, token)
            for token in ("MARS", "OASYS", "ARIES")
        ):
            return (
                Kind.SQL_QUERY,
                "/NODE indicates Oracle dialect and /ENGINE=VA or /OLEDB=SQLPlus",
            )
        return None

    @staticmethod
    def _node_matches(node_value: str, token: str) -> bool:
        node = node_value.upper().strip()
        return (
            node.endswith(token)
            or node.endswith(f".{token}")
            or f"<<<{token}>>>" in node
        )

    @staticmethod
    def _format_sql_literal(sql: str) -> str:
        escaped = sql.replace('"""', '\\"\\"\\"')
        return f'"""{escaped}"""'

    @staticmethod
    def _extract_sql_text(block) -> str:
        sql = getattr(block, "rewritten_sql", None)
        if sql is None:
            sql = block.resolved_body

        calls = CsvIO.scan_sql_get_csv_list_calls(sql)
        if not calls:
            return SqliteEngine._format_sql_literal(sql)

        parts: list[str] = []
        cursor = 0
        for call in calls:
            literal = sql[cursor : call.start]
            if literal:
                parts.append(SqliteEngine._format_sql_literal(literal))

            csv_path_expr = MacroState.to_py_expr(call.csv_path)
            expr = CsvIO.sql_get_csv_list.render(
                csv_path_expr, repr(call.column_ref), repr(call.lead_in)
            )
            parts.append(expr)
            if call.needs_closing_paren:
                parts.append(SqliteEngine._format_sql_literal(")"))
            cursor = call.end

        tail = sql[cursor:]
        if tail:
            parts.append(SqliteEngine._format_sql_literal(tail))

        return " + ".join(parts)

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
    def emit_block(cls, block) -> tuple[str, list[str]] | None:
        sqlite = block.kind is Kind.SQLITE_QUERY
        return cls._emit_sql(block, sqlite=sqlite)

    @classmethod
    def _emit_sql(
        cls,
        block,
        *,
        sqlite: bool,
    ) -> tuple[str, list[str]]:
        sql = cls._extract_sql_text(block)
        output = resolve_output_path(block)
        reader_cls = getattr(block, "reader_cls", None)
        if reader_cls is None:
            raise ValueError("SQL emission requires dispatch metadata")
        crosstab = CrosstabUtility.extract_options(block)
        header = None if crosstab else cls._extract_header(block)

        reader_kwargs = getattr(block, "reader_kwargs", {})
        reader_kwargs_items = [f"{k}={repr(v)}" for k, v in reader_kwargs.items()]
        inst_expr = f"{reader_cls.__name__}({', '.join(reader_kwargs_items)})"

        kwargs: dict[str, object] = {
            "sql": sql,
            "output": repr(output),
            "reader": inst_expr,
        }
        if sqlite:
            kwargs["inputs"] = cls._extract_table_inputs(block)
        if header:
            kwargs["header"] = header
        if crosstab:
            kwargs["crosstab"] = crosstab

        pass

        stmt = PipelineContext.run_query.render(**kwargs)
        suffix = "sqlite_query" if sqlite else "sql_query"
        return suffix, [stmt]

# <vg2c:dependencies:end>
# <vg2c:steps:start>
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

# <vg2c:steps:end>

# <vg2c:workflow:start>
def run() -> None:
    ctx = PipelineContext()
    ctx.macro.set_named("NODE", 'KM')
    step_0000_sql_query(ctx)
# <vg2c:workflow:end>

if __name__ == "__main__":
    run()