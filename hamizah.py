# SQL statements containing filters:
# - step_0000_sql_query (Line 2144): filters on f0.history_deleted_flag, f0.owner, f5.from_to, f5.history_deleted_flag
# - step_0001_sql_query (Line 2164): filters on f0.history_deleted_flag, f0.owner, f4.history_deleted_flag, f4.unique_flag, p.latest_version
# - step_0004_sql_query (Line 2214): filters on f0.history_deleted_flag, f0.out_date, f0.owner, f4.entity, f4.history_deleted_flag, f4.unique_flag, la.attribute_number, p.latest_version, p.prodgroup3, wtb, yeuchuan_SQL_5547.tab
# - step_0006_sqlite_query (Line 2300): filters on lot_in_scope
# - step_0007_sql_query (Line 2374): filters on f0.history_deleted_flag, f0.owner, la.attribute_number, p.latest_version, wtb
# - step_0011_sql_query (Line 2470): filters on f0.history_deleted_flag, f0.owner, p.latest_version
# - step_0018_sql_query (Line 2563): filters on ats.data_domain
# - step_0020_sql_query (Line 2638): filters on dt.within_lao_latest_flag, v0.lao_start_ww, v0.ts_id, v0.valid_flag
# - step_0021_sql_query (Line 2664): filters on u1.attr_name
# - step_0022_sql_query (Line 2687): filters on u1.attr_name
# - step_0023_sql_query (Line 2721): filters on u0.ws_operation
# - step_0024_sql_query (Line 2778): filters on f0.history_deleted_flag, f0.owner, la.attribute_number
# - step_0032_sql_query (Line 2900): filters on ats.data_domain
# - step_0035_sql_query (Line 3013): filters on u0.visualid, u0.ws_operation, u1.attr_name, yeuchuan_a1_5547.tab
# - step_0038_sql_query (Line 3132): filters on dt.within_lao_latest_flag, v0.lao_start_ww, v0.ts_id, v0.valid_flag

# Auto-generated Python script from VG2
"""Pipeline implementation."""


from __future__ import annotations
from abc import ABC, abstractmethod
from collections.abc import Mapping, Sequence
from contextlib import contextmanager
from dataclasses import dataclass
from datasyncx.readers.aries_reader import AriesReader
from datasyncx.readers.mars_reader import MarsReader
from email.message import EmailMessage
from enum import Enum
from pathlib import Path
from typing import Any
from typing import Any, Callable
from typing import Any, Callable, ContextManager
from typing import Any, ClassVar
from typing import Any, ClassVar, TYPE_CHECKING
from typing import Any, Iterator
from typing import Iterator, Protocol
from vg2c.dispatch.dialects.sqlite import SqliteReader
import csv
import inspect
import keyring
import logging
import os
import pandas
import pandas as pd
import re
import shlex
import shutil
import smtplib
import subprocess
import time




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
    def get_source(cls) -> str:
        custom = getattr(cls, "__vg2c_source__", None)
        if custom is not None:
            return str(custom).rstrip()

        source = inspect.getsource(cls)
        return _strip_embed_artifacts(source, cls.__name__)

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

    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        content = self.macro.substitute(template, vars=vars)
        self.fs_ops.write_file(path, content)

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
        sql = self.macro.substitute(sql)

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

class MailService:
    """Send email. Credentials are read from Windows Credential Manager (service: SMTP)."""

    utility_name = "email"
    KEYRING_SERVICE = "SMTP"
    DEFAULT_SMTP_HOST = "smtpauth.intel.com"
    DEFAULT_SMTP_PORT = 587

    # ------------------------------------------------------------------
    # Credential retrieval
    # ------------------------------------------------------------------

    @classmethod
    def _load_credential(cls) -> keyring.credentials.Credential:
        cred = keyring.get_credential(cls.KEYRING_SERVICE, None)
        if cred is None:
            raise RuntimeError(
                f"No credential found for service '{cls.KEYRING_SERVICE}' in Windows "
                "Credential Manager. Add a generic credential with:\n"
                f"  cmdkey /generic:{cls.KEYRING_SERVICE} /user:<email> /pass:<password>"
            )
        return cred

    # ------------------------------------------------------------------
    # Stage-1 classification
    # ------------------------------------------------------------------

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        text = options.lookup.get("UTILITIES", "")
        if not text:
            return None
        if MailService._is_mail_utility(split_utility_command(text)):
            return Kind.EMAIL, "/UTILITIES command is SQLPathFinder_Email.va"
        return None

    # ------------------------------------------------------------------
    # Emission
    # ------------------------------------------------------------------

    @classmethod
    def emit_block(cls, block: Any) -> list[str] | None:
        argv = cls._utility_argv(block)
        if not cls._is_mail_utility(argv):
            return None
        stmt = cls._emit_send(argv, block.resolved_body)
        if stmt is None:
            return ["pass  # TODO: unsupported email utility command"]
        return [stmt]

    @staticmethod
    def _utility_argv(block: Any) -> list[str]:
        return split_utility_command(block.resolved_options.lookup.get("UTILITIES", ""))

    @staticmethod
    def _is_mail_utility(argv: list[str]) -> bool:
        if not argv:
            return False
        basename = strip_quotes(argv[0]).split("/")[-1].split("\\")[-1].lower()
        return "sqlpathfinder_email" in basename

    @staticmethod
    def _csv_items(value: str) -> list[str]:
        return [p.strip() for p in strip_quotes(value).split(",") if p.strip()]

    @staticmethod
    def _list_expr(values: list[str]) -> str:
        return "[" + ", ".join(MacroState.to_py_expr(v) for v in values) + "]"

    @classmethod
    def _emit_send(cls, argv: list[str], body_fallback: str) -> str | None:
        payload = argv[1:]

        if len(payload) >= 5:
            attachments = cls._csv_items(payload[0])
            from_addr = strip_quotes(payload[1])
            body = (
                payload[3]
                if strip_quotes(payload[3])
                else (body_fallback or payload[2])
            )

            kwargs: dict[str, Any] = {
                "to": MacroState.to_py_expr(payload[4]),
                "subject": MacroState.to_py_expr(payload[2]),
                "body": MacroState.to_py_expr(body),
            }
            if attachments:
                kwargs["attachments"] = cls._list_expr(attachments)
            if from_addr and from_addr.lower() != "self":
                kwargs["from_addr"] = MacroState.to_py_expr(from_addr)
            return cls.send.render(**kwargs)

        if len(payload) >= 3:
            return cls.send.render(
                to=MacroState.to_py_expr(payload[0]),
                subject=MacroState.to_py_expr(payload[1]),
                body=MacroState.to_py_expr(payload[2]),
            )

        return None

    # ------------------------------------------------------------------
    # Runtime send (emittable)
    # ------------------------------------------------------------------

    def send(
        self,
        to: str,
        subject: str,
        body: str,
        attachments: list[str] | None = None,
        from_addr: str | None = None,
    ) -> None:
        cred = self._load_credential()
        sender = from_addr or cred.username

        msg = EmailMessage()
        msg["Subject"] = subject
        msg["From"] = sender
        msg["To"] = to
        msg.set_content(self._resolve_body(body))

        for att_path in attachments or []:
            p = Path(att_path)
            if p.exists():
                msg.add_attachment(
                    p.read_bytes(),
                    maintype="application",
                    subtype="octet-stream",
                    filename=p.name,
                )

        try:
            with smtplib.SMTP(self.DEFAULT_SMTP_HOST, self.DEFAULT_SMTP_PORT) as smtp:
                smtp.ehlo()
                smtp.starttls()
                smtp.ehlo()
                smtp.login(cred.username, cred.password)
                smtp.send_message(msg)
        except Exception as exc:
            pass

    @staticmethod
    def _resolve_body(body: str) -> str:
        path = Path(body)
        if body and path.exists() and path.is_file():
            return path.read_text(encoding="utf-8", errors="replace")
        return body

class ExternalProcess:
    """Execute generic shell command or script block."""

    utility_name = "external"

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        text = options.lookup.get("UTILITIES", "").strip()
        if not text:
            return None
        argv = split_utility_command(text)
        if not argv:
            return None
        basename = argv[0].split("/")[-1].split("\\")[-1].lower()
        if "run_python_script" in basename or basename.endswith((".bat", ".exe")):
            return Kind.EXTERNAL_RUN, "/UTILITIES command maps to external run"
        return None

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        return split_utility_command(text)

    @classmethod
    def emit_block(cls, block) -> list[str] | None:
        argv = cls._utility_argv(block)
        if not argv:
            return ["pass  # TODO: empty external utility command"]

        # basename = argv[0].split("/")[-1].split("\\")[-1].lower()
        # if "run_python_script" in basename:
        #     return ["pass  # Python script embedded directly, external run omitted"]

        stmt = cls._emit_run(argv)
        return [stmt]

    @classmethod
    def _emit_run(cls, argv: list[str]) -> str:
        expr_items = [MacroState.to_py_expr(token) for token in argv]
        argv_expr = "[" + ", ".join(expr_items) + "]"
        return cls.run.render(argv=argv_expr)

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
    def check(options) -> tuple[Kind, str] | None:
        if options.lookup.get("WRITE-FILE", "").upper() == "Y":
            csv_value = options.lookup.get("CSV", "")
            if csv_value.lower().endswith(".py"):
                return None
            return Kind.WRITE_FILE, "/WRITE-FILE=Y"

        utilities = options.lookup.get("UTILITIES")
        if not utilities:
            return None

        first_token = utilities.strip().split(maxsplit=1)[0].strip().strip('"')
        basename = first_token.split("/")[-1].split("\\")[-1].lower()

        if "robocopy" in basename or "spfcopy" in basename or "spfrename" in basename:
            return Kind.FS_COPY, "/UTILITIES command maps to FS copy"
        if "spfdelete" in basename:
            return Kind.FS_DELETE, "/UTILITIES command maps to FS delete"
        return None

    @classmethod
    def emit_block(cls, block) -> tuple[str, list[str]] | None:
        if block.kind is Kind.FS_COPY:
            return cls._emit_copy_block(block)
        if block.kind is Kind.FS_DELETE:
            return cls._emit_delete_block(block)

        from vg2c.utilities.pipeline_context import PipelineContext

        stmt = PipelineContext.write_file.render(
            path=repr(resolve_output_path(block)),
            template=repr(block.resolved_body),
        )
        return "write_file", [stmt]

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        return split_utility_command(text)

    @classmethod
    def _emit_copy_block(cls, block) -> tuple[str, list[str]]:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "robocopy" in basename:
            stmt = cls._emit_robocopy(argv)
        elif "spfcopy" in basename:
            stmt = cls._emit_spf_copy(argv)
        elif "spfrename" in basename:
            stmt = cls._emit_spf_rename(argv)
        else:
            return "fs_copy", ["pass  # TODO: unsupported FS copy utility command"]
        return "fs_copy", [stmt]

    @classmethod
    def _emit_delete_block(cls, block) -> tuple[str, list[str]]:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "spfdelete" not in basename:
            return "fs_delete", ["pass  # TODO: unsupported FS delete utility command"]
        stmt = cls._emit_spf_delete(argv)
        return "fs_delete", [stmt]

    @classmethod
    def _emit_robocopy(cls, argv: list[str]) -> str:
        # RoboCopy.va arg layout: <file_name> <source_dir> <dest_dir> [...]
        file_name = MacroState.to_py_expr(argv[1]) if len(argv) > 1 else repr("")
        source_dir = MacroState.to_py_expr(argv[2]) if len(argv) > 2 else repr(".")
        dest_dir = MacroState.to_py_expr(argv[3]) if len(argv) > 3 else repr(".")
        src_expr = f"str(Path({source_dir}) / {file_name})"
        dst_expr = dest_dir
        return cls.copy.render(src=src_expr, dst=dst_expr)

    @classmethod
    def _emit_spf_copy(cls, argv: list[str]) -> str:
        # SPFCopy.bat arg layout: <source_path> <dest_dir> [recurse]
        src = MacroState.to_py_expr(argv[1]) if len(argv) > 1 else repr("")
        dst_dir = MacroState.to_py_expr(argv[2]) if len(argv) > 2 else repr(".")
        src_expr = src
        dst_expr = f"str(Path({dst_dir}) / Path({src}).name)"
        return cls.copy.render(src=src_expr, dst=dst_expr)

    @classmethod
    def _emit_spf_rename(cls, argv: list[str]) -> str:
        # SPFRename.va arg layout: <source_path> <dest_path>
        src = MacroState.to_py_expr(argv[1]) if len(argv) > 1 else repr("")
        dst = MacroState.to_py_expr(argv[2]) if len(argv) > 2 else repr("")
        return cls.rename.render(src=src, dst=dst)

    @classmethod
    def _emit_spf_delete(cls, argv: list[str]) -> str:
        raw = strip_quotes(argv[1]) if len(argv) > 1 else ""
        items = [p.strip() for p in raw.split(",") if p.strip()]
        paths_expr = "[" + ", ".join(MacroState.to_py_expr(p) for p in items) + "]"
        return cls.delete.render(paths=paths_expr)

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

    def write_file(self, path: str | Path, content: str) -> None:
        out = resolve_path(path, for_write=True)
        out.parent.mkdir(parents=True, exist_ok=True)
        out.write_text(content, encoding="utf-8")

class HtmlReport:
    """Utility for generating HTML report files."""

    utility_name = "html_report"

    # SPF template row delimiter is the literal 4-character sequence "<\\>"
    # (angle, backslash, backslash, angle). In a Python source string this must
    # be written with four backslashes so the runtime value is two backslashes.
    _ROW_DELIM = "<\\\\>"
    _TRUE_VALUES = ("Y", "YES", "TRUE")

    _HTML_SCAFFOLD = """\
<html>
<head>
<title>{title}</title>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
{css_decl}
<!--@SPF-JS-HEADER@-->
<style type="text/css">
table.tblout, td.tblout, tr.tblout {{
    border-width:0px;
    border-collapse:collapse;
    border-style:none;
    text-align:left;
    vertical-align:top;
}}
td.tblout {{ padding:10px; }}
img {{ vertical-align:top; }}
a {{ text-decoration:none; color:#464feb; }}
tr th, tr td {{ border:1px solid #e6e6e6; }}
tr th {{ background-color:#f5f5f5; }}
</style>
</head>
<body>
{body}
</body>
</html>
"""

    _CSS_RULES: list[dict[str, Any]] = [
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
        {"name": "Column-Alt-Row", "template": "td.alt\n{{\n{decls}\n}}"},
        {"name": "At-Top-of-Report", "template": "p.at-top-of-report\n{{\n{decls}\n}}"},
        {
            "name": "JQX-All-IChart-Text",
            "template": (
                ".jqx-chart-axis-text, .jqx-chart-label-text, .jqx-chart-legend-text,"
                " .jqx-chart-axis-description, .jqx-chart-title-text,"
                " .jqx-chart-title-description {{\n{decls}\n}}"
            ),
            "defaults": [("fill", "     fill:black;")],
        },
        {"name": "At-Top-of-Col1", "template": "p.at-top-of-col1\n{{\n{decls}\n}}"},
        {"name": "At-Top-of-Col2", "template": "p.at-top-of-col2\n{{\n{decls}\n}}"},
        {"name": "At-Top-of-Col3", "template": "p.at-top-of-col3\n{{\n{decls}\n}}"},
    ]

    # Emit-time dispatch: report-type -> (method, option-keys, needs-template)
    _EMIT_DISPATCH: dict[str, tuple[str, list[str], bool]] = {
        "HTML-RUN": ("run", ["INSTANCE", "PROMPT-TEXT", "APP_SERVER_DEFAULT"], True),
        "HTML-LAYOUT": (
            "layout",
            [
                "OUTLOOK",
                "INSTANCE",
                "JSON-ONLY",
                "CHART-INSTANCE",
                "APP_SERVER_DEFAULT",
            ],
            True,
        ),
        "HTML-DEFER": (
            "defer",
            ["INSTANCE", "ID", "PROMPT-TEXT", "APP_SERVER_DEFAULT"],
            True,
        ),
        "HTML-DELETE": ("delete", ["INSTANCE"], False),
    }

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        report = options.lookup.get("REPORT")
        if report and report.upper().startswith("HTML-"):
            return Kind.HTML_REPORT, "/REPORT starts with HTML-"
        return None

    def __init__(self) -> None:
        self.styles: dict[str, list[str]] = {}
        self.css_file: str | None = None
        self.deferred_reports: dict[str, dict[str, Any]] = {}
        self.instance: str | None = None

    # ------------------------------------------------------------------
    # Emit-time (code generation)
    # ------------------------------------------------------------------

    @classmethod
    def emit_block(cls, block) -> list[str] | None:
        report_type = block.resolved_options.lookup.get("REPORT", "").upper().strip()
        entry = cls._EMIT_DISPATCH.get(report_type)
        if entry is None:
            return None
        method_name, keys, needs_template = entry

        kwargs: dict[str, str] = {}
        for key in keys:
            val = block.resolved_options.lookup.get(key)
            if val is not None:
                kwargs[key.lower().replace("-", "_")] = MacroState.to_py_expr(val)
        if needs_template:
            kwargs["template"] = repr(block.resolved_body)

        method = getattr(cls, method_name)
        args = ("ctx",) if method_name == "layout" else ()
        return [method.render(*args, **kwargs)]

    # ------------------------------------------------------------------
    # Template parsing
    # ------------------------------------------------------------------

    @classmethod
    def _iter_rows(cls, template: str | None):
        """Yield non-empty rows split on the SPF delimiter."""
        for line in (template or "").splitlines():
            if not line.strip():
                continue
            parts = [p.strip() for p in line.split(cls._ROW_DELIM)]
            if len(parts) >= 2:
                yield parts

    @classmethod
    def _parse_options(cls, template: str | None) -> dict[str, Any]:
        """Parse an SPF options template into {KEY: value|list-of-values}."""
        options: dict[str, Any] = {}
        for parts in cls._iter_rows(template):
            key = parts[0].upper()
            # 2nd column is an optional sub-key. If blank, values start at [2].
            vals = [p for p in (parts[2:] if parts[1] == "" else parts[1:]) if p]
            if not vals:
                options[key] = ""
            elif len(vals) == 1:
                options[key] = vals[0]
            else:
                options[key] = vals
        return options

    # ------------------------------------------------------------------
    # Emittable runtime methods
    # ------------------------------------------------------------------

    def run(
        self,
        instance: str | None = None,
        prompt_text: str | None = None,
        app_server_default: str | None = None,
        template: str | None = None,
    ) -> None:
        self.instance = instance
        for parts in self._iter_rows(template):
            key = parts[0].upper()
            if key == "CSS":
                self.css_file = parts[1] or None
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
        self.deferred_reports[id] = {
            "instance": instance,
            "template": template,
            "options": self._parse_options(template),
        }

    def delete(self, instance: str | None = None) -> None:
        self.styles.clear()
        self.css_file = None
        self.deferred_reports.clear()

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
        directives, body = self._split_layout(template)

        body = re.sub(
            r"HTM:([A-Za-z0-9_]+)",
            lambda m: (
                self._render_report(m.group(1), ctx)
                if m.group(1) in self.deferred_reports
                else m.group(0)
            ),
            body,
        )

        css_file = directives.get("CSS") or self.css_file
        css_embed = directives.get("CSSEMBED", "").upper() in self._TRUE_VALUES
        css_decl = self._resolve_css(css_file, css_embed)
        title = directives.get("TITLE", "SQLPathFinder Report")

        if "<html>" not in body.lower():
            body = self._HTML_SCAFFOLD.format(title=title, css_decl=css_decl, body=body)
        elif css_decl:
            if "</head>" in body:
                body = body.replace("</head>", f"{css_decl}\n</head>", 1)
            else:
                body = f"{css_decl}\n{body}"

        filename = self._resolve_output_filename(
            directives.get("FILE", "report.html"), instance
        )
        if ctx and hasattr(ctx, "macro"):
            filename = ctx.macro.substitute(filename)
        if ctx and hasattr(ctx, "write_file"):
            ctx.write_file(filename, body)
        else:
            out = Path(filename)
            out.parent.mkdir(parents=True, exist_ok=True)
            out.write_text(body, encoding="utf-8")

    # ------------------------------------------------------------------
    # Layout template split
    # ------------------------------------------------------------------

    @staticmethod
    def _split_layout(template: str) -> tuple[dict[str, str], str]:
        """Extract ':KEY:VALUE' directives from a layout template, return (dirs, body)."""
        directives: dict[str, str] = {}
        body_lines: list[str] = []
        for line in template.splitlines():
            if line.startswith(":"):
                head, sep, value = line[1:].partition(":")
                if sep:
                    directives[head.strip().upper()] = value.strip()
                    continue
            body_lines.append(line)
        return directives, "\n".join(body_lines)

    # ------------------------------------------------------------------
    # CSS
    # ------------------------------------------------------------------

    def _resolve_css(self, css_file: str | None, css_embed: bool) -> str:
        """Return a <style> or <link> tag string (or empty string)."""
        content = ""
        if css_file:
            path = Path(css_file)
            if path.exists():
                content = path.read_text(encoding="utf-8", errors="replace")
            elif self.styles:
                content = self._build_css()
                try:
                    path.parent.mkdir(parents=True, exist_ok=True)
                    path.write_text(content, encoding="utf-8")
                except OSError:
                    pass
        elif self.styles:
            content = self._build_css()

        if css_embed and content:
            return f'<style type="text/css">\n{content}\n</style>'
        if css_file and not css_embed:
            return f'<link rel="stylesheet" type="text/css" href="{css_file}" />'
        return ""

    def _build_css(self) -> str:
        def get_decls(name: str) -> list[str]:
            decls: list[str] = []
            for d in self.styles.get(name, []):
                d = d.strip()
                if not d:
                    continue
                if ":" in d:
                    key, val = (s.strip() for s in d.split(":", 1))
                    if key == "font-size" and val.isdigit():
                        val += "px"
                    decls.append(f"     {key}:{val};")
                else:
                    decls.append(f"     {d};")
            return decls

        blocks: list[str] = []
        for rule in self._CSS_RULES:
            decls = get_decls(rule["name"])
            if not decls:
                continue
            extras = list(decls)
            for token, default_decl in rule.get("defaults", []):
                if not any(token in d for d in extras):
                    extras.append(default_decl)
            blocks.append(rule["template"].format(decls="\n".join(extras)))
            blocks.extend(rule.get("extras", []))
            tail = rule.get("tail_template")
            if tail:
                blocks.append(tail.format(decls="\n".join(decls)))
        return "\n\n".join(blocks)

    # ------------------------------------------------------------------
    # Deferred report rendering
    # ------------------------------------------------------------------

    def _render_report(self, report_id: str, ctx: Any) -> str:
        report = self.deferred_reports.get(report_id)
        if not report:
            return ""
        options = report.get("options")
        if not isinstance(options, dict):
            options = self._parse_options(report.get("template"))
            report["options"] = options

        def as_list(val: Any) -> list[str]:
            if val is None:
                return []
            return list(val) if isinstance(val, list) else [str(val)]

        cols = as_list(options.get("COLUMN-DATA"))
        headers = as_list(options.get("COLUMN-HEADERS"))
        alignments = as_list(options.get("COLUMN-ALIGNMENT"))
        alignments += ["middle-left"] * (len(cols) - len(alignments))

        raw_path = options.get("INPUT-FILE", "")
        if isinstance(raw_path, list):
            raw_path = raw_path[0] if raw_path else ""
        rows = self._load_csv_rows(str(raw_path), ctx)

        lines: list[str] = ['<table class="tblin">']
        lines.extend("<COL>" for _ in cols)
        lines.append("<thead>")
        lines.append("<tr id='colhdr'>")
        lines.extend(f"<th>{h}</th>" for h in headers)
        lines.append("</tr>")
        lines.append("</thead>")

        for idx, row in enumerate(rows):
            cell_class = "tblin" if idx % 2 == 0 else "alt"
            lines.append("<tr>")
            for ci, col in enumerate(cols):
                val_str = self._format_cell(col, row.get(col.lower(), ""))
                valign, halign = self._parse_alignment(alignments[ci])
                lines.append(
                    f'<td class="{cell_class}" '
                    f'style="vertical-align:{valign};text-align:{halign};">'
                    f"{val_str}</td>"
                )
            lines.append("</tr>")

        lines.append("<tfoot>")
        lines.append("</tfoot>")
        lines.append("</table>")

        content = "\n".join(lines)
        top = options.get("AT-TOP-OF-REPORT")
        if top:
            top_str = top if isinstance(top, str) else " ".join(top)
            content = f'<p class="at-top-of-report">\n{top_str}</p>\n{content}'
        return content

    @staticmethod
    def _parse_alignment(align: str) -> tuple[str, str]:
        parts = align.split("-")
        if len(parts) >= 2:
            return parts[0], parts[1]
        return "middle", parts[0] if parts else "left"

    @staticmethod
    def _format_cell(col_name: str, val: Any) -> str:
        if val is None:
            return "&nbsp;"
        s = str(val).strip()
        if s == "" or s.lower() == "nan":
            return "&nbsp;"
        if s.endswith("%"):
            return s
        low = col_name.lower()
        if "ce%" in low or "percent" in low:
            try:
                return f"{float(s) * 100:.2f}%"
            except ValueError:
                pass
        return s

    @staticmethod
    def _load_csv_rows(raw_path: str, ctx: Any) -> list[dict[str, Any]]:
        if not raw_path:
            return []
        if ctx and hasattr(ctx, "macro"):
            path = ctx.macro.resolve_file_path(raw_path)
        else:
            path = resolve_path(raw_path)
        if not (path and path.is_file()):
            return []
        if ctx and hasattr(ctx, "csv_io") and hasattr(ctx.csv_io, "iter"):
            source = ctx.csv_io.iter(str(path))
        else:
            with path.open(newline="", encoding="utf-8", errors="replace") as fh:
                source = list(csv.DictReader(fh))
        return [{k.lower(): v for k, v in row.items() if k} for row in source]

    # ------------------------------------------------------------------
    # Output filename
    # ------------------------------------------------------------------

    def _resolve_output_filename(self, path: str, instance: str | None) -> str:
        if path and not path.startswith("email:"):
            return path
        fallback = "report.html"
        for report in self.deferred_reports.values():
            options = report.get("options")
            if not isinstance(options, dict):
                options = self._parse_options(report.get("template"))
                report["options"] = options
            out = options.get("OUTPUT-FILE")
            if isinstance(out, list):
                out = out[0] if out else None
            if out:
                fallback = out
                break
        instance_id = instance or self.instance
        base = fallback.lower()
        return f"{instance_id}_{base}" if instance_id else base

class PythonEmbed:
    """Utility class for directly embedding Python script blocks."""

    utility_name = "python_embed"

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if options.lookup.get("WRITE-FILE", "").upper() != "Y":
            return None

        csv_value = options.lookup.get("CSV", "")
        if csv_value.lower().endswith(".py"):
            return Kind.PYTHON_EMBED, "/WRITE-FILE=Y targeting .py script"
        return None

    @classmethod
    def emit_block(cls, block: Any) -> list[str] | None:
        # Wrap the original python body directly in the step function definition
        return [block.resolved_body]

class RowsInFile:
    """Count rows in a CSV file and store the count in a named macro variable.

    VG2 syntax::

        /UTILITIES={ROWS-IN-FILE} "<csv_path>" "<var_name>" "<prompt_off>"

    The third argument (``Y``/``N``) is the original VG2 prompt-suppression
    flag; the generated Python never prompts the user, so it is parsed but
    intentionally ignored during emission.
    """

    utility_name = "rows_in_file"
    _TOKEN_RE = re.compile(r"^\s*\{ROWS-IN-FILE\}", re.IGNORECASE)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        utilities = options.lookup.get("UTILITIES", "")
        if RowsInFile._TOKEN_RE.match(utilities):
            return Kind.ROWS_IN_FILE, "/UTILITIES is {ROWS-IN-FILE}"
        return None

    @classmethod
    def emit_block(cls, block) -> tuple[str, list[str]] | None:
        from vg2c.utilities.macro_state import MacroState
        from vg2c.utilities.csv_io import CsvIO

        utilities = block.resolved_options.lookup.get("UTILITIES", "")
        argv = split_utility_command(utilities)
        # argv[0] = '{ROWS-IN-FILE}', argv[1] = csv_path, argv[2] = var_name
        csv_path_expr = MacroState.to_py_expr(argv[1] if len(argv) > 1 else None)
        var_name = strip_quotes(argv[2]).upper() if len(argv) > 2 else ""

        row_count_call = CsvIO.row_count.render(csv_path_expr)
        stmt = MacroState.set_named.render(repr(var_name), f"str({row_count_call})")
        return "rows_in_file", [stmt]

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

        from vg2c.utilities.pipeline_context import PipelineContext

        stmt = PipelineContext.run_query.render(**kwargs)
        suffix = "sqlite_query" if sqlite else "sql_query"
        return suffix, [stmt]

class UnknownUtility:
    """Emit handler for unrecognised /UTILITIES commands."""

    utility_name = "utility"

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        return None

    @classmethod
    def emit_block(cls, block) -> list[str] | None:
        return ["pass  # TODO: utility command not classified"]

class WaitFile:
    """Poll for a file to appear, up to a configurable timeout (seconds).

    VG2 syntax::

        /UTILITIES=@EXEDIR@\\WaitFile.va "<file_path>" "<timeout_seconds>"

    The second argument is the timeout in seconds (default 30).  The utility
    returns as soon as the file exists or the timeout elapses — it never
    raises; callers that need a hard failure should check the file afterwards.
    """

    utility_name = "wait_file"

    _WAITFILE_NAMES = {"waitfile.va", "waitfile.bat", "waitfile.exe"}
    _DEFAULT_TIMEOUT = 30
    _POLL_INTERVAL = 5

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        text = options.lookup.get("UTILITIES", "").strip()
        if not text:
            return None
        argv = split_utility_command(text)
        if not argv:
            return None
        basename = argv[0].split("/")[-1].split("\\")[-1].lower()
        if basename in WaitFile._WAITFILE_NAMES:
            return Kind.WAIT_FILE, "/UTILITIES command maps to WaitFile poll"
        return None

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        return split_utility_command(text)

    @classmethod
    def emit_block(cls, block) -> tuple[str, list[str]]:
        argv = cls._utility_argv(block)
        # argv[0] = tool path, argv[1] = file path, argv[2] = timeout seconds
        path_expr = MacroState.to_py_expr(argv[1]) if len(argv) > 1 else repr("")
        raw_timeout = (
            strip_quotes(argv[2]) if len(argv) > 2 else str(cls._DEFAULT_TIMEOUT)
        )
        try:
            timeout_val = int(raw_timeout)
        except ValueError:
            timeout_val = cls._DEFAULT_TIMEOUT
        stmt = cls.poll.render(path_expr, timeout_val)
        return "wait_file", [stmt]

    def poll(
        self,
        path: str | Path,
        timeout: int = _DEFAULT_TIMEOUT,
        interval: int = _POLL_INTERVAL,
    ) -> bool:
        """Return True when *path* exists; False if timeout elapses first."""
        deadline = time.monotonic() + timeout
        while time.monotonic() < deadline:
            if Path(path).exists():
                return True
            time.sleep(interval)
        return Path(path).exists()

def step_0000_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              f0.lot AS test_lot
             ,f5.from_to AS from_to
             ,f5.from_to_lot AS tcb_lot
    FROM 
    @[]@.F_LotHist f0
    LEFT JOIN @[]@.F_LotTxnHist f5 ON f5.lot = f0.lot AND f5.operation = f0.operation AND f5.prevout_date = f0.prevout_date AND NVL(f5.history_deleted_flag,'N') = 'N'
     AND      f5.from_to = 'F' 
    WHERE
    NVL(f0.history_deleted_flag,'N') = 'N'
    AND      f0.owner <> 'EMPTYFOUP'
     AND      f0.lot In ('7626F034') 
    -- Tail A
    /*END SQL*/

    """, output='yeuchuan_a0_5547.tab', reader=MarsReader(), header=['test_lot', 'from_to', 'tcb_lot'])

def step_0001_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              f0.lot AS lot_1
             ,f4.entity AS entity
             ,p.prodgroup3 AS prodgroup3
    FROM 
    @[]@.F_LotHist f0
    LEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
    LEFT JOIN @[]@.F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = 'Y'
    LEFT JOIN @[]@.F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource
    WHERE
    NVL(f0.history_deleted_flag,'N') = 'N'
    AND      f0.owner <> 'EMPTYFOUP'
     AND      (f0.lot In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a0_5547.tab', 'tcb_lot', 'f0.lot In') + """)""" + """ 
     AND      f0.operation In ('1204'
    ,'2511') 
    -- Tail A
    /*END SQL*/

    """, output='yeuchuan_a1_5547.tab', reader=MarsReader(), header=['lot_1', 'entity', 'prodgroup3'])

def step_0002_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""

    DROP INDEX IF EXISTS IdxA1;
    Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_5547] ([lot_1]);

    SELECT /*L0*/  DISTINCT 
              a0.[test_lot] AS [test_lot]
             ,a0.[tcb_lot] AS [tcb_lot]
             ,a1.[entity] AS [entity]
             ,a1.[prodgroup3] AS [prodgroup3]
    FROM 
               [yeuchuan_a0_5547] a0
     INNER JOIN [yeuchuan_a1_5547] a1
      ON a0.[tcb_lot] = a1.[lot_1]
    """, output='input.csv', reader=SqliteReader(), inputs=['yeuchuan_a0_5547.tab', 'yeuchuan_a1_5547.tab'], header=['test_lot', 'tcb_lot', 'entity', 'prodgroup3'])

def step_0003_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""
    SELECT /*L0*/ 
              a0.[entity] AS [entity]
             ,a0.[prodgroup3] AS [prodgroup3]
    FROM 
    [input] a0
    """, output='yeuchuan_SQL_5547.tab', reader=SqliteReader(), inputs=['input.csv'], header=['entity', 'prodgroup3'])

def step_0004_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT  DISTINCT 
              lot AS lot
             ,To_Char(out_date,'yyyy-mm-dd hh24:mi:ss') AS out_date
             ,prodgroup3_1 AS prodgroup3_1
             ,entity_1 AS entity_1
             ,Count (Distinct attribute_operation) AS attribute_operation
             ,Replace(Replace(Replace(Replace(Replace(Replace(wtb,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS wtb
    FROM
    (
    SELECT 
              lot AS lot
             ,out_date AS out_date
             ,prodgroup3_1 AS prodgroup3_1
             ,entity_1 AS entity_1
             ,attribute_operation AS attribute_operation
             ,wtb AS wtb
    FROM
    (
    SELECT  
              f0.lot AS lot
             ,f0.out_date AS out_date
             ,p.prodgroup3 AS prodgroup3_1
             ,f4.entity AS entity_1
             ,f12.operation AS attribute_operation
             ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 1225 AND la.src_erase_date IS NULL AND rownum <= 1) AS wtb
    FROM 
    @[]@.F_LotHist f0
    LEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
    INNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot
    LEFT JOIN @[]@.F_LotAttribute f12 ON f9.lot = f12.lot
    LEFT JOIN @[]@.F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = 'Y'
     AND      (f4.entity In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'entity', 'f4.entity In') + """)""" + """ 
    LEFT JOIN @[]@.F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource
    WHERE
    NVL(f0.history_deleted_flag,'N') = 'N'
    AND      f0.owner <> 'EMPTYFOUP'
     AND      f0.operation In ('2511'
    ,'1204') 
     AND      (p.prodgroup3 In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'prodgroup3', 'p.prodgroup3 In') + """)""" + """ 
     AND      f0.out_date >= TRUNC(SYSDATE) - 30 
     AND      f12.operation In ('2303'
    ,'2511'
    ,'1204') 
    -- Tail A
    )
    WHERE
                  wtb = 'N' 
    )
    GROUP BY 
              lot
             ,out_date
             ,prodgroup3_1
             ,entity_1
             ,wtb
    /*END SQL*/

    """, output='yeuchuan_a1_5547.tab', reader=MarsReader(), header=['lot', 'out_date', 'prodgroup3_1', 'entity_1', 'attribute_operation', 'wtb'])

def step_0005_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""

    DROP INDEX IF EXISTS IdxA1;
    Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_5547] ([prodgroup3_1],[entity_1]);

    SELECT /*L0*/ 
              sql.[entity] AS [entity]
             ,sql.[prodgroup3] AS [prodgroup3]
             ,a1.[lot] AS [lot]
             ,a1.[out_date] AS [out_date]
             ,a1.[attribute_operation] AS [attribute_operation]
    FROM 
               [yeuchuan_SQL_5547] sql
     LEFT OUTER JOIN [yeuchuan_a1_5547] a1
      ON sql.[prodgroup3] = a1.[prodgroup3_1] 
     AND a1.[entity_1] = sql.[entity] 
    ORDER BY
               [entity] Asc
              ,[prodgroup3] Asc
              ,[lot] Asc
    """, output='input_all.csv', reader=SqliteReader(), inputs=['yeuchuan_SQL_5547.tab', 'yeuchuan_a1_5547.tab'], header=['entity', 'prodgroup3', 'lot', 'out_date', 'attribute_operation'])

def step_0006_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""

    DROP INDEX IF EXISTS IdxA0;
    Create Index IF NOT EXISTS IdxA0 ON [input] ([tcb_lot],[test_lot]);

    SELECT /*L3*/  DISTINCT 
              [entity_1] AS [entity_1]
             ,[prodgroup3_1] AS [prodgroup3_1]
             ,[lot] AS [lot]
             ,[out_date] AS [out_date]
             ,[sbl] AS [sbl]
             ,[attribute_operation] AS [attribute_operation]
             ,[rank] AS [rank]
             ,[lot_in_scope] AS [lot_in_scope]
    FROM
    (
    SELECT /*L2*/ 
              [test_lot] AS [test_lot]
             ,[tcb_lot] AS [tcb_lot]
             ,[entity] AS [entity]
             ,[prodgroup3] AS [prodgroup3]
             ,[entity_1] AS [entity_1]
             ,[prodgroup3_1] AS [prodgroup3_1]
             ,[lot] AS [lot]
             ,[out_date] AS [out_date]
             ,[sbl] AS [sbl]
             ,[attribute_operation] AS [attribute_operation]
             ,[rank] AS [rank]
             ,CASE WHEN  (abs([rank] - (MAX(CASE WHEN [sbl] = 1 THEN [rank]  END) OVER (PARTITION BY  [entity_1] ,  [prodgroup3_1] )))) <6  THEN 1 ELSE 0 END AS [lot_in_scope]
    FROM
    (
    SELECT /*L1*/ 
              [test_lot] AS [test_lot]
             ,[tcb_lot] AS [tcb_lot]
             ,[entity] AS [entity]
             ,[prodgroup3] AS [prodgroup3]
             ,[entity_1] AS [entity_1]
             ,[prodgroup3_1] AS [prodgroup3_1]
             ,[lot] AS [lot]
             ,[out_date] AS [out_date]
             ,[sbl] AS [sbl]
             ,[attribute_operation] AS [attribute_operation]
             ,[rank] AS [rank]
    FROM
    (
    SELECT /*L0*/  
              a0.[test_lot] AS [test_lot]
             ,a0.[tcb_lot] AS [tcb_lot]
             ,a0.[entity] AS [entity]
             ,a0.[prodgroup3] AS [prodgroup3]
             ,a1.[entity] AS [entity_1]
             ,a1.[prodgroup3] AS [prodgroup3_1]
             ,a1.[lot] AS [lot]
             ,a1.[out_date] AS [out_date]
             ,CASE WHEN  a1.[lot] == a0.[tcb_lot]  THEN 1  WHEN a1.[lot] == a0.[test_lot]  THEN 1   ELSE 0 END AS [sbl]
             ,a1.[attribute_operation] AS [attribute_operation]
             ,RANK () OVER (PARTITION BY   a1.[entity] ,   a1.[prodgroup3] ORDER BY  a1.[out_date]  ASC) AS [rank]
    FROM 
               [input_all] a1
     LEFT OUTER JOIN [input] a0
      ON a0.[tcb_lot] Like a1.[lot] 
     OR a0.[test_lot] = a1.[lot] 
    ) t /*L0*/
    ) t /*L1*/
    ) t /*L2*/
    WHERE
                  [lot_in_scope] = 1 
    ORDER BY
               [entity_1] Asc
              ,[prodgroup3_1] Asc
              ,[lot] Asc
    """, output='flag_lot.csv', reader=SqliteReader(), inputs=['input_all.csv', 'input.csv'], header=['entity_1', 'prodgroup3_1', 'lot', 'out_date', 'sbl', 'attribute_operation', 'rank', 'lot_in_scope'])

def step_0007_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT  DISTINCT 
              tcb_op AS tcb_op
             ,lot AS lot
             ,To_Char(out_date,'yyyy-mm-dd hh24:mi:ss') AS out_date
             ,Replace(Replace(Replace(Replace(Replace(Replace(TCB_Link,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS TCB_Link
             ,Replace(Replace(Replace(Replace(Replace(Replace(Subs,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Subs
             ,prodgroup3 AS prodgroup3
             ,Replace(Replace(Replace(Replace(Replace(Replace(wtb,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS wtb
             ,product AS product
    FROM
    (
    SELECT  
              f0.operation AS tcb_op
             ,f0.lot AS lot
             ,f0.out_date AS out_date
             ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 6244 AND la.src_erase_date IS NULL AND rownum <= 1) AS TCB_Link
             ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 5001 AND la.src_erase_date IS NULL AND rownum <= 1) AS Subs
             ,p.prodgroup3 AS prodgroup3
             ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 1225 AND la.src_erase_date IS NULL AND rownum <= 1) AS wtb
             ,f0.product AS product
    FROM 
    @[]@.F_LotHist f0
    LEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
    INNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot
    WHERE
    NVL(f0.history_deleted_flag,'N') = 'N'
    AND      f0.owner <> 'EMPTYFOUP'
     AND      f0.operation In ('1204'
    ,'2511') 
     AND      (f0.lot In 
    """ + ctx.csv_io.sql_get_csv_list('.\\flag_lot.csv', 3, 'f0.lot In') + """)""" + """ 
    -- Tail A
    )
    WHERE
                  wtb = 'N' 
    /*END SQL*/

    """, output='yeuchuan_a0_5547.tab', reader=MarsReader(), header=['tcb_op', 'lot', 'out_date', 'TCB_Link', 'Subs', 'prodgroup3', 'wtb', 'product'])

def step_0008_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              f2.operation AS int_op
             ,pr.product AS product_1
    FROM 
         @[]@.F_ProductRoute pr
        ,@[]@.F_RouteOper f2
    WHERE 
                  pr.src_erase_date IS NULL
     AND      f2.facility = pr.facility
     AND      f2.route = pr.route
     AND      f2.src_erase_date IS NULL
     AND      pr.facility In ('A15') 
     AND      (pr.product In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a0_5547.tab', 'product', 'pr.product In') + """)""" + """ 
     AND      f2.operation In ('1990'
    ,'2303') 
    /*END SQL*/

    """, output='yeuchuan_a1_5547.tab', reader=MarsReader(), header=['int_op', 'product_1'])

def step_0009_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""

    DROP INDEX IF EXISTS IdxA1;
    Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_5547] ([product_1]);

    SELECT /*L0*/  DISTINCT 
              a0.[tcb_op] AS [tcb_op]
             ,a0.[lot] AS [lot]
             ,a0.[out_date] AS [out_date]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a0.[TCB_Link],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [TCB_Link]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a0.[Subs],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [Subs]
             ,a0.[prodgroup3] AS [prodgroup3]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a0.[wtb],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [wtb]
             ,a1.[int_op] AS [int_op]
    FROM 
               [yeuchuan_a0_5547] a0
     LEFT OUTER JOIN [yeuchuan_a1_5547] a1
      ON a0.[product] = a1.[product_1] 
    ORDER BY
               [lot] Asc
    """, output='lotlist_route.csv', reader=SqliteReader(), inputs=['yeuchuan_a0_5547.tab', 'yeuchuan_a1_5547.tab'], header=['tcb_op', 'lot', 'out_date', 'TCB_Link', 'Subs', 'prodgroup3', 'wtb', 'int_op'])

def step_0010_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""
    SELECT /*L0*/  DISTINCT 
              a2.[prodgroup3] AS [prodgroup3_2]
    FROM 
    [lotlist_route] a2
    """, output='yeuchuan_SQL_5547.tab', reader=SqliteReader(), inputs=['lotlist_route.csv'], header=['prodgroup3_2'])

def step_0011_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT  DISTINCT 
              f0.operation AS test_op
             ,p.prodgroup3 AS prodgroup3
             ,f1.oper_short_desc AS oper_short_desc
             ,Replace(Replace(Replace(Replace(Replace(Replace(f1.oper_long_desc,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS oper_long_desc
    FROM 
    @[]@.F_LotHist f0
    INNER JOIN @[]@.F_Operation f1 ON f1.operation = f0.operation AND f1.facility = f0.facility AND f1.operation_version = f0.operation_version
    LEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
    WHERE
    NVL(f0.history_deleted_flag,'N') = 'N'
    AND      f0.owner <> 'EMPTYFOUP'
     AND      (p.prodgroup3 In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'prodgroup3_2', 'p.prodgroup3 In') + """)""" + """ 
     AND      f0.out_date >= TRUNC(SYSDATE) - 90 
     AND      (f1.oper_short_desc LIKE  'CPBI%'
    OR f1.oper_short_desc LIKE 'CRC%'
    OR f1.oper_short_desc LIKE 'HCS%'
    OR f1.oper_short_desc LIKE 'HDMX%'
    OR f1.oper_short_desc LIKE 'MPS%'
    ) 
     AND      f0.lot Like '8_______' 
     AND      f0.lot Not Like '%K' 
    -- Tail A
    /*END SQL*/

    """, output='yeuchuan_a0_5547.tab', reader=MarsReader(), header=['test_op', 'prodgroup3', 'oper_short_desc', 'oper_long_desc'])

def step_0012_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""

    DROP INDEX IF EXISTS IdxA0;
    Create Index IF NOT EXISTS IdxA0 ON [yeuchuan_a0_5547] ([prodgroup3]);

    SELECT /*L0*/ 
              a0.[test_op] AS [test_op]
             ,sql.[prodgroup3_2] AS [prodgroup3_2]
             ,a0.[prodgroup3] AS [prodgroup3]
             ,a0.[oper_short_desc] AS [oper_short_desc]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a0.[oper_long_desc],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [oper_long_desc]
    FROM 
               [yeuchuan_SQL_5547] sql
     LEFT OUTER JOIN [yeuchuan_a0_5547] a0
      ON sql.[prodgroup3_2] = a0.[prodgroup3]
    """, output='test_op.csv', reader=SqliteReader(), inputs=['yeuchuan_SQL_5547.tab', 'yeuchuan_a0_5547.tab'], header=['test_op', 'prodgroup3_2', 'prodgroup3', 'oper_short_desc', 'oper_long_desc'])

def step_0013_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""
    SELECT /*L0*/ 
              a0.[tcb_op] AS [tcb_op]
             ,a0.[lot] AS [lot]
             ,a0.[out_date] AS [out_date]
             ,a0.[tcb_link] AS [tcb_link]
             ,a0.[subs] AS [subs]
             ,a0.[prodgroup3] AS [prodgroup3]
             ,a0.[int_op] AS [int_op]
    FROM 
    [lotlist_route] a0
    WHERE
                  a0.[int_op] Not In ('2303')
    """, output='lotlist.csv', reader=SqliteReader(), inputs=['lotlist_route.csv'], header=['tcb_op', 'lot', 'out_date', 'tcb_link', 'subs', 'prodgroup3', 'int_op'])

def step_0014_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""
    SELECT /*L0*/ 
              a0.[tcb_op] AS [tcb_op]
             ,a0.[lot] AS [lot]
             ,a0.[out_date] AS [out_date]
             ,a0.[tcb_link] AS [tcb_link]
             ,a0.[subs] AS [subs]
             ,a0.[prodgroup3] AS [prodgroup3]
             ,a0.[int_op] AS [int_op]
    FROM 
    [lotlist_route] a0
    WHERE
                  a0.[int_op] In ('2303')
    """, output='lotlist_int.csv', reader=SqliteReader(), inputs=['lotlist_route.csv'], header=['tcb_op', 'lot', 'out_date', 'tcb_link', 'subs', 'prodgroup3', 'int_op'])

def step_0017_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""
    SELECT /*L0*/  DISTINCT 
              a2.[out_date] AS [tcb_out_date]
             ,a2.[tcb_link] AS [tcb_link]
             ,a2.[lot] AS [tcb_lot]
             ,a2.[prodgroup3] AS [prodgroup3]
             ,a2.[tcb_op] AS [tcb_op]
    FROM 
    [flag_lot] a2
    """, output='yeuchuan_SQL_5547.tab', reader=SqliteReader(), inputs=['flag_lot.csv'], header=['tcb_out_date', 'tcb_link', 'tcb_lot', 'prodgroup3', 'tcb_op'])

def step_0018_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              facility AS facility
             ,Bonder AS Bonder
             ,Max(BONDSTAGE) AS BONDSTAGE
             ,test_name AS test_name
             ,visual_id AS visual_id
             ,lot_1 AS lot_1
             ,tray_or_carrier_id AS tray_or_carrier_id
             ,carrier_x AS carrier_x
             ,carrier_y AS carrier_y
             ,complevel_1 AS complevel_1
             ,link_id AS link_id
    FROM
    (
    SELECT  
              ats.facility AS facility
             ,dt.test_head_id AS Bonder
             ,CASE WHEN ctr.string_value IS NULL THEN to_char(ctr.numeric_result) ELSE ctr.string_value END AS BONDSTAGE
             ,t.test_name AS test_name
             ,di.visual_id AS visual_id
             ,ats.lot AS lot_1
             ,dt.testing_session_tray_id AS tray_or_carrier_id
             ,dt.carrier_x AS carrier_x
             ,dt.carrier_y AS carrier_y
             ,ctr.complevel_1 AS complevel_1
             ,ats.link_id AS link_id
    FROM 
    A_Testing_Session ats
    INNER JOIN A_All_Component_Testing_Result ctr ON ctr.lao_start_ww = ats.lao_start_ww AND ctr.ts_id = ats.ts_id AND (ctr.numeric_result IS NOT NULL or ctr.string_value is NOT NULL)
    INNER JOIN A_Test t ON t.t_id = ctr.t_id
    INNER JOIN A_Device_Testing dt ON dt.lao_start_ww = ats.lao_start_ww AND dt.ts_id = ats.ts_id
    AND dt.lao_start_ww = ctr.lao_start_ww AND dt.ts_id = ctr.ts_id AND dt.dt_id = ctr.dt_id
    LEFT JOIN A_Device_Item di ON di.di_id = dt.di_id
    WHERE ats.data_domain='METROLOGY'
     AND      (ats.operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'tcb_op', 'ats.operation In') + """)""" + """ 
     AND      (ats.lot In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab->5', 'tcb_lot', 'ats.lot In') + """)""" + """ 
     AND      ctr.complevel_1 Like 'U_' 
    )
    GROUP BY 
              facility
             ,Bonder
             ,test_name
             ,visual_id
             ,lot_1
             ,tray_or_carrier_id
             ,carrier_x
             ,carrier_y
             ,complevel_1
             ,link_id
    /*END SQL*/

    """, output='yeuchuan_a1_5547.tab', reader=AriesReader(), crosstab={'row_keys': ['facility', 'Bonder', 'visual_id', 'lot_1', 'tray_or_carrier_id', 'carrier_x', 'carrier_y', 'complevel_1', 'link_id'], 'header_key': 'test_name', 'value_key': 'BONDSTAGE'})

def step_0019_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT  DISTINCT 
              u0.lotnum AS test_lot_lot
             ,u0.visualid AS visual_id_1
    FROM 
    A_Ube_Unit_Hist2 u0
    WHERE 1=1
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a1_5547.tab->999', 'visual_id', 'u0.visualid In') + """)""" + """ 
     AND      (u0.ws_operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\test_op.csv', 1, 'u0.ws_operation In') + """)""" + """ 
    /*END SQL*/

    """, output='yeuchuan_a4_5547.tab', reader=AriesReader(), header=['test_lot_lot', 'visual_id_1'])

def step_0020_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT /*+  use_nl (dt) use_nl (di) */ DISTINCT 
              v0.operation AS test_op
             ,v0.lot AS test_lot
             ,To_Char(lao.lao_test_end_date_time,'yyyy-mm-dd hh24:mi:ss') AS lot_latest_test_end_date
             ,di.visual_id AS visual_id_2
             ,dt.interface_bin AS interface_bin
             ,dt.functional_bin AS functional_bin
    FROM 
    A_Testing_Session v0
    INNER JOIN A_Lot_At_Operation lao ON v0.lot = lao.lot AND v0.operation = lao.Operation AND v0.facility = lao.facility
    INNER JOIN A_Device_Testing dt ON v0.lao_start_ww + 0 = dt.lao_start_ww AND v0.ts_id + 0 = dt.ts_id
    LEFT JOIN A_Device_Item di ON dt.di_id = di.di_id
    WHERE 1=1
     AND      v0.valid_flag = 'Y' 
     AND      dt.within_lao_latest_flag = 'Y' 
     AND      (v0.operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\test_op.csv', 1, 'v0.operation In') + """)""" + """ 
     AND      (v0.lot In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a4_5547.tab->10', 'test_lot_lot', 'v0.lot In') + """)""" + """ 
    /*END SQL*/

    """, output='yeuchuan_a3_5547.tab', reader=AriesReader(), header=['test_op', 'test_lot', 'lot_latest_test_end_date', 'visual_id_2', 'interface_bin', 'functional_bin'])

def step_0021_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT  DISTINCT 
              u0.visualid AS visual_id_3
             ,u0.fab_wafer_num AS fab_wafer
             ,u0.fab_lot_num AS fab_lot
             ,u0.x_location AS x_location
             ,u0.y_location AS y_location
             ,u1.attr_value AS die
    FROM 
    A_Ube_Unit_Hist2 u0
    INNER JOIN A_Ube_Unit_Hist2_Attr u1 ON u0.creation_ww = u1.creation_ww AND u0.unit_hist_key = u1.unit_hist_key AND u0.facility = u1.facility
     AND      u1.attr_name = 'MD_POSITION' 
    WHERE 1=1
     AND      (u0.lotnum In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'tcb_lot', 'u0.lotnum In') + """)""" + """ 
     AND      (u0.ws_operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'tcb_op', 'u0.ws_operation In') + """)""" + """ 
    /*END SQL*/

    """, output='yeuchuan_a0_5547.tab', reader=AriesReader(), header=['visual_id_3', 'fab_wafer', 'fab_lot', 'x_location', 'y_location', 'die'])

def step_0022_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              u0.visualid AS visual_id_4
             ,ua0.minor_id AS sub_id
             ,u1.attr_value AS supplier_ID
             ,SUBSTR(    ua0.minor_id ,9,2) AS supplier_panel
             ,SUBSTR(     ua0.minor_id ,13,2) AS supplier_panel_X
             ,SUBSTR(    ua0.minor_id ,15,2) AS suppler_panel_Y
             ,u0.lotnum AS lot_2
    FROM 
    A_Ube_Unit_Hist2 u0
    LEFT JOIN (SELECT u0.creation_ww, u0.facility, u0.unit_hist_key
    ,MAX(CASE WHEN uuha.attr_name = 'MINOR_ID' THEN uuha.attr_value END) MINOR_ID
    FROM a_ube_unit_hist2 u0, a_ube_unit_hist2_attr uuha
    WHERE u0.creation_ww = uuha.creation_ww AND u0.unit_hist_key = uuha.unit_hist_key AND u0.facility = uuha.facility
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a0_5547.tab->999', 'visual_id_3', 'u0.visualid In') + """)""" + """ 
     AND      u0.ws_operation = '0614' 
    AND uuha.attr_name IN ('dummy'
    ,'MINOR_ID'
    )
    GROUP BY u0.creation_ww, u0.unit_hist_key,u0.facility)  ua0 ON u0.creation_ww = ua0.creation_ww AND u0.unit_hist_key = ua0.unit_hist_key AND u0.facility = ua0.facility
    INNER JOIN A_Ube_Unit_Hist2_Attr u1 ON u0.creation_ww = u1.creation_ww AND u0.unit_hist_key = u1.unit_hist_key AND u0.facility = u1.facility
     AND      u1.attr_name = 'VENDOR_ID' 
    WHERE 1=1
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a0_5547.tab->999', 'visual_id_3', 'u0.visualid In') + """)""" + """ 
     AND      u0.ws_operation = '0614' 
    /*END SQL*/

    """, output='yeuchuan_a6_5547.tab', reader=AriesReader(), header=['visual_id_4', 'sub_id', 'supplier_ID', 'supplier_panel', 'supplier_panel_X', 'suppler_panel_Y', 'lot_2'])

def step_0023_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT  DISTINCT 
              u0.visualid AS visual_id_5
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|BTV=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|BTV=([^\|]*).*','\1') end AS btv
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|BTV_TILTX=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|BTV_TILTX=([^\|]*).*','\1') end AS btv_tiltx
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|BTV_TILTY=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|BTV_TILTY=([^\|]*).*','\1') end AS btv_tilty
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|BUMPCOPE_VACUUM=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|BUMPCOPE_VACUUM=([^\|]*).*','\1') end AS bumpcope_vacuum
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CAW_VACUUM=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CAW_VACUUM=([^\|]*).*','\1') end AS caw_vacuum
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV=([^\|]*).*','\1') end AS ctv
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_ANGLE=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_ANGLE=([^\|]*).*','\1') end AS ctv_angle
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_B4=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_B4=([^\|]*).*','\1') end AS ctv_b4
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_B5=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_B5=([^\|]*).*','\1') end AS ctv_b5
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_R=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_R=([^\|]*).*','\1') end AS ctv_r
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_SHAPE=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_SHAPE=([^\|]*).*','\1') end AS ctv_shape
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_SIGN=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_SIGN=([^\|]*).*','\1') end AS ctv_sign
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_TILTX=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_TILTX=([^\|]*).*','\1') end AS ctv_tiltx
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RBTV=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RBTV=([^\|]*).*','\1') end AS rbtv
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RBTV_TILTX=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RBTV_TILTX=([^\|]*).*','\1') end AS rbtv_tiltx
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RBTV_TILTY=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RBTV_TILTY=([^\|]*).*','\1') end AS rbtv_tilty
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RCTV=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RCTV=([^\|]*).*','\1') end AS rctv
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RCTV_TILTX=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RCTV_TILTX=([^\|]*).*','\1') end AS rctv_tiltx
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RCTV_TILTY=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RCTV_TILTY=([^\|]*).*','\1') end AS rctv_tilty
             ,ua0.DIE_POSITION AS die_position
    FROM 
    A_Ube_Unit_Hist2 u0
    LEFT JOIN (SELECT u0.creation_ww, u0.facility, u0.unit_hist_key
    ,MAX(CASE WHEN uuha.attr_name = 'DIE_POSITION' THEN uuha.attr_value END) DIE_POSITION
    FROM a_ube_unit_hist2 u0, a_ube_unit_hist2_attr uuha
    WHERE u0.creation_ww = uuha.creation_ww AND u0.unit_hist_key = uuha.unit_hist_key AND u0.facility = uuha.facility
     AND      u0.ws_operation = '9999' 
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a6_5547.tab->999', 'sub_id', 'u0.visualid In') + """)""" + """ 
    AND uuha.attr_name IN ('dummy'
    ,'DIE_POSITION'
    )
    GROUP BY u0.creation_ww, u0.unit_hist_key,u0.facility)  ua0 ON u0.creation_ww = ua0.creation_ww AND u0.unit_hist_key = ua0.unit_hist_key AND u0.facility = ua0.facility
    LEFT JOIN (SELECT u0.creation_ww, u0.facility, u0.unit_hist_key
    ,MAX(CASE WHEN uuha.attr_name = 'TCB_DFF_DATA' THEN '|' || DBMS_LOB.substr(uuha.attr_value,3000) END) TCB_DFF_DATA
    FROM a_ube_unit_hist2 u0, a_ube_unit_hist2_attr_clob uuha
    WHERE u0.creation_ww = uuha.creation_ww AND u0.unit_hist_key = uuha.unit_hist_key AND u0.facility = uuha.facility
     AND      u0.ws_operation = '9999' 
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a6_5547.tab->999', 'sub_id', 'u0.visualid In') + """)""" + """ 
    AND uuha.attr_name IN ('dummy'
    ,'TCB_DFF_DATA'
    )
    GROUP BY u0.creation_ww, u0.unit_hist_key,u0.facility)  ua01 ON u0.creation_ww = ua01.creation_ww AND u0.unit_hist_key = ua01.unit_hist_key AND u0.facility = ua01.facility
    WHERE 1=1
     AND      u0.ws_operation = '9999' 
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a6_5547.tab->999', 'sub_id', 'u0.visualid In') + """)""" + """ 
    /*END SQL*/

    """, output='yeuchuan_a5_5547.tab', reader=AriesReader(), header=['visual_id_5', 'btv', 'btv_tiltx', 'btv_tilty', 'bumpcope_vacuum', 'caw_vacuum', 'ctv', 'ctv_angle', 'ctv_b4', 'ctv_b5', 'ctv_r', 'ctv_shape', 'ctv_sign', 'ctv_tiltx', 'rbtv', 'rbtv_tiltx', 'rbtv_tilty', 'rctv', 'rctv_tiltx', 'rctv_tilty', 'die_position'])

def step_0024_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT  DISTINCT 
              Replace(Replace(Replace(Replace(Replace(Replace((SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 5002 AND la.src_erase_date IS NULL AND rownum <= 1),',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS sli_num
             ,f0.lot AS lot
    FROM 
    @[]@.F_LotHist f0
    INNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot
    WHERE
    NVL(f0.history_deleted_flag,'N') = 'N'
    AND      f0.owner <> 'EMPTYFOUP'
     AND      (f0.lot In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a6_5547.tab', 'lot_2', 'f0.lot In') + """)""" + """ 
     AND      f0.operation = '0614' 
    -- Tail A
    /*END SQL*/

    """, output='yeuchuan_a7_5547.tab', reader=MarsReader(), header=['sli_num', 'lot'])

def step_0025_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""

    DROP INDEX IF EXISTS IdxA1;
    Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_5547] ([visual_id],[lot_1],[complevel_1]);
    DROP INDEX IF EXISTS IdxA4;
    Create Index IF NOT EXISTS IdxA4 ON [yeuchuan_a4_5547] ([visual_id_1]);
    DROP INDEX IF EXISTS IdxA3;
    Create Index IF NOT EXISTS IdxA3 ON [yeuchuan_a3_5547] ([visual_id_2]);
    DROP INDEX IF EXISTS IdxA0;
    Create Index IF NOT EXISTS IdxA0 ON [yeuchuan_a0_5547] ([visual_id_3],[die]);
    DROP INDEX IF EXISTS IdxA6;
    Create Index IF NOT EXISTS IdxA6 ON [yeuchuan_a6_5547] ([visual_id_4],[sub_id],[lot_2]);
    DROP INDEX IF EXISTS IdxA5;
    Create Index IF NOT EXISTS IdxA5 ON [yeuchuan_a5_5547] ([visual_id_5],[die_position]);
    DROP INDEX IF EXISTS IdxA7;
    Create Index IF NOT EXISTS IdxA7 ON [yeuchuan_a7_5547] ([lot]);

    SELECT /*L0*/  DISTINCT 
              a1.[facility] AS [facility]
             ,sql.[tcb_out_date] AS [tcb_out_date]
             ,sql.[tcb_link] AS [tcb_link]
             ,a1.[Bonder] AS [Bonder]
             ,sql.[tcb_lot] AS [tcb_lot]
             ,CrossTab->[[a1,5547;:Y]]
             ,a1.[visual_id] AS [visual_id]
             ,a1.[tray_or_carrier_id] AS [tray_or_carrier_id]
             ,a1.[carrier_x] AS [carrier_x]
             ,a1.[carrier_y] AS [carrier_y]
             ,a3.[test_op] AS [test_op]
             ,a3.[test_lot] AS [test_lot]
             ,a3.[lot_latest_test_end_date] AS [lot_latest_test_end_date]
             ,a1.[complevel_1] AS [complevel_1]
             ,a6.[sub_id] AS [sub_id]
             ,a6.[supplier_ID] AS [supplier_ID]
             ,a6.[supplier_panel] AS [supplier_panel]
             ,a6.[supplier_panel_X] AS [supplier_panel_X]
             ,a6.[suppler_panel_Y] AS [suppler_panel_Y]
             ,sql.[prodgroup3] AS [prodgroup3]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a7.[sli_num],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [sli_num]
             ,a0.[fab_wafer] AS [fab_wafer]
             ,a0.[fab_lot] AS [fab_lot]
             ,a0.[x_location] AS [x_location]
             ,a0.[y_location] AS [y_location]
             ,a0.[die] AS [die]
             ,a3.[interface_bin] AS [interface_bin]
             ,a3.[functional_bin] AS [functional_bin]
             ,a5.[btv] AS [btv]
             ,a5.[btv_tiltx] AS [btv_tiltx]
             ,a5.[btv_tilty] AS [btv_tilty]
             ,a5.[bumpcope_vacuum] AS [bumpcope_vacuum]
             ,a5.[caw_vacuum] AS [caw_vacuum]
             ,a5.[ctv] AS [ctv]
             ,a5.[ctv_angle] AS [ctv_angle]
             ,a5.[ctv_b4] AS [ctv_b4]
             ,a5.[ctv_b5] AS [ctv_b5]
             ,a5.[ctv_r] AS [ctv_r]
             ,a5.[ctv_shape] AS [ctv_shape]
             ,a5.[ctv_sign] AS [ctv_sign]
             ,a5.[ctv_tiltx] AS [ctv_tiltx]
             ,a5.[rbtv] AS [rbtv]
             ,a5.[rbtv_tiltx] AS [rbtv_tiltx]
             ,a5.[rbtv_tilty] AS [rbtv_tilty]
             ,a5.[rctv] AS [rctv]
             ,a5.[rctv_tiltx] AS [rctv_tiltx]
             ,a5.[rctv_tilty] AS [rctv_tilty]
    FROM 
               [yeuchuan_SQL_5547] sql
     LEFT OUTER JOIN [yeuchuan_a1_5547] a1
      ON sql.[tcb_lot] = a1.[lot_1] 
     LEFT OUTER JOIN [yeuchuan_a4_5547] a4
      ON a4.[visual_id_1] = a1.[visual_id] 
     LEFT OUTER JOIN [yeuchuan_a3_5547] a3
      ON a3.[visual_id_2] = a1.[visual_id] 
     LEFT OUTER JOIN [yeuchuan_a0_5547] a0
      ON a1.[visual_id] = a0.[visual_id_3] 
     AND a1.[complevel_1] = a0.[die] 
     LEFT OUTER JOIN [yeuchuan_a6_5547] a6
      ON a0.[visual_id_3] = a6.[visual_id_4] 
     LEFT OUTER JOIN [yeuchuan_a5_5547] a5
      ON a5.[visual_id_5] = a6.[sub_id] 
     AND a1.[complevel_1] = a5.[die_position] 
     LEFT OUTER JOIN [yeuchuan_a7_5547] a7
      ON a6.[lot_2] = a7.[lot]
    """, output='data.csv', reader=SqliteReader(), inputs=['yeuchuan_SQL_5547.tab', 'yeuchuan_a1_5547.tab', 'yeuchuan_a4_5547.tab', 'yeuchuan_a3_5547.tab', 'yeuchuan_a0_5547.tab', 'yeuchuan_a6_5547.tab', 'yeuchuan_a5_5547.tab', 'yeuchuan_a7_5547.tab'])

def step_0026_utility(ctx) -> None:
    pass  # TODO: utility command not classified

def step_0031_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""
    SELECT /*L0*/  DISTINCT 
              a7.[out_date] AS [tcb_out_date]
             ,a7.[tcb_link] AS [tcb_link]
             ,a7.[tcb_op] AS [tcb_op]
             ,a7.[int_op] AS [int_op]
             ,a7.[lot] AS [tcb_lot]
             ,a7.[prodgroup3] AS [prodgroup3]
    FROM 
    [flag_int_lot] a7
    """, output='yeuchuan_SQL_5547.tab', reader=SqliteReader(), inputs=['flag_int_lot.csv'], header=['tcb_out_date', 'tcb_link', 'tcb_op', 'int_op', 'tcb_lot', 'prodgroup3'])

def step_0032_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              facility AS facility
             ,Bonder AS Bonder
             ,Max(BONDSTAGE) AS BONDSTAGE
             ,test_name AS test_name
             ,visual_id AS visual_id
             ,lot_1 AS lot_1
             ,tray_or_carrier_id AS tray_or_carrier_id
             ,carrier_x AS carrier_x
             ,carrier_y AS carrier_y
             ,complevel_1 AS complevel_1
             ,link_id AS link_id
    FROM
    (
    SELECT  
              ats.facility AS facility
             ,dt.test_head_id AS Bonder
             ,CASE WHEN ctr.string_value IS NULL THEN to_char(ctr.numeric_result) ELSE ctr.string_value END AS BONDSTAGE
             ,t.test_name AS test_name
             ,di.visual_id AS visual_id
             ,ats.lot AS lot_1
             ,dt.testing_session_tray_id AS tray_or_carrier_id
             ,dt.carrier_x AS carrier_x
             ,dt.carrier_y AS carrier_y
             ,ctr.complevel_1 AS complevel_1
             ,ats.link_id AS link_id
    FROM 
    A_Testing_Session ats
    INNER JOIN A_All_Component_Testing_Result ctr ON ctr.lao_start_ww = ats.lao_start_ww AND ctr.ts_id = ats.ts_id AND (ctr.numeric_result IS NOT NULL or ctr.string_value is NOT NULL)
    INNER JOIN A_Test t ON t.t_id = ctr.t_id
    INNER JOIN A_Device_Testing dt ON dt.lao_start_ww = ats.lao_start_ww AND dt.ts_id = ats.ts_id
    AND dt.lao_start_ww = ctr.lao_start_ww AND dt.ts_id = ctr.ts_id AND dt.dt_id = ctr.dt_id
    LEFT JOIN A_Device_Item di ON di.di_id = dt.di_id
    WHERE ats.data_domain='METROLOGY'
     AND      (ats.operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'tcb_op', 'ats.operation In') + """)""" + """ 
     AND      (ats.lot In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab->5', 'tcb_lot', 'ats.lot In') + """)""" + """ 
     AND      ctr.complevel_1 Like 'U_' 
    )
    GROUP BY 
              facility
             ,Bonder
             ,test_name
             ,visual_id
             ,lot_1
             ,tray_or_carrier_id
             ,carrier_x
             ,carrier_y
             ,complevel_1
             ,link_id
    /*END SQL*/

    """, output='yeuchuan_a1_5547.tab', reader=AriesReader(), crosstab={'row_keys': ['facility', 'Bonder', 'visual_id', 'lot_1', 'tray_or_carrier_id', 'carrier_x', 'carrier_y', 'complevel_1', 'link_id'], 'header_key': 'test_name', 'value_key': 'BONDSTAGE'})

def step_0033_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              u0.visualid AS visual_id_3
             ,u0.fab_wafer_num AS fab_wafer
             ,u0.fab_lot_num AS fab_lot
             ,u0.y_location AS y_location
             ,u0.x_location AS x_location
             ,u0.md_position AS md_position_stack
             ,u0.lotnum AS lot_2
    FROM 
    A_Ube_Unit_Hist2 u0
    WHERE 1=1
     AND      (u0.lotnum In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'tcb_lot', 'u0.lotnum In') + """)""" + """ 
     AND      (u0.ws_operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'tcb_op', 'u0.ws_operation In') + """)""" + """ 
    /*END SQL*/

    """, output='yeuchuan_a2_5547.tab', reader=AriesReader(), header=['visual_id_3', 'fab_wafer', 'fab_lot', 'y_location', 'x_location', 'md_position_stack', 'lot_2'])

def step_0034_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              u0.ws_operation AS ngpoi_op
             ,u0.lotnum AS interposer_lot
             ,u0.visualid AS interposer_id
             ,ua0.patch_id AS patch_id
    FROM 
    A_Ube_Unit_Hist2 u0
    LEFT JOIN (SELECT u0.creation_ww, u0.facility, u0.unit_hist_key
    ,MAX(CASE WHEN uuha.attr_name = 'PATCH_ID' THEN uuha.attr_value END) PATCH_ID
    FROM a_ube_unit_hist2 u0, a_ube_unit_hist2_attr uuha
    WHERE u0.creation_ww = uuha.creation_ww AND u0.unit_hist_key = uuha.unit_hist_key AND u0.facility = uuha.facility
     AND      (u0.ws_operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'int_op', 'u0.ws_operation In') + """)""" + """ 
     AND      (u0.lotnum In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a1_5547.tab->999', 'lot_1', 'u0.lotnum In') + """)""" + """ 
    AND uuha.attr_name IN ('dummy'
    ,'PATCH_ID'
    )
    GROUP BY u0.creation_ww, u0.unit_hist_key,u0.facility)  ua0 ON u0.creation_ww = ua0.creation_ww AND u0.unit_hist_key = ua0.unit_hist_key AND u0.facility = ua0.facility
    WHERE 1=1
     AND      (u0.ws_operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_SQL_5547.tab', 'int_op', 'u0.ws_operation In') + """)""" + """ 
     AND      (ua0.patch_id In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a1_5547.tab', 'visual_id', 'ua0.patch_id In') + """)""" + """ 
     AND      (u0.lotnum In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a1_5547.tab->999', 'lot_1', 'u0.lotnum In') + """)""" + """ 
    /*END SQL*/

    """, output='yeuchuan_a0_5547.tab', reader=AriesReader(), header=['ngpoi_op', 'interposer_lot', 'interposer_id', 'patch_id'])

def step_0035_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              visual_id_4 AS visual_id_4
             ,sub_id AS sub_id
             ,supplier_id AS supplier_id
             ,supplier_panel AS supplier_panel
             ,SUBSTR(     sub_id ,13,2) AS supplier_panel_X
             ,SUBSTR(    sub_id ,15,2) AS supplier_panel_Y
    FROM
    (
    SELECT  
              u0.visualid AS visual_id_4
             ,ua0.minor_id AS sub_id
             ,u1.attr_value AS supplier_id
             ,SUBSTR(     ua0.minor_id ,9,2) AS supplier_panel
    FROM 
    A_Ube_Unit_Hist2 u0
    LEFT JOIN (SELECT u0.creation_ww, u0.facility, u0.unit_hist_key
    ,MAX(CASE WHEN uuha.attr_name = 'MINOR_ID' THEN uuha.attr_value END) MINOR_ID
    FROM a_ube_unit_hist2 u0, a_ube_unit_hist2_attr uuha
    WHERE u0.creation_ww = uuha.creation_ww AND u0.unit_hist_key = uuha.unit_hist_key AND u0.facility = uuha.facility
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a1_5547.tab->999', 'visual_id', 'u0.visualid In') + """)""" + """ 
     AND      u0.ws_operation = '0614' 
    AND uuha.attr_name IN ('dummy'
    ,'MINOR_ID'
    )
    GROUP BY u0.creation_ww, u0.unit_hist_key,u0.facility)  ua0 ON u0.creation_ww = ua0.creation_ww AND u0.unit_hist_key = ua0.unit_hist_key AND u0.facility = ua0.facility
    INNER JOIN A_Ube_Unit_Hist2_Attr u1 ON u0.creation_ww = u1.creation_ww AND u0.unit_hist_key = u1.unit_hist_key AND u0.facility = u1.facility
     AND      u1.attr_name = 'VENDOR_ID' 
    WHERE 1=1
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a1_5547.tab->999', 'visual_id', 'u0.visualid In') + """)""" + """ 
     AND      u0.ws_operation = '0614' 
    )
    /*END SQL*/

    """, output='yeuchuan_a6_5547.tab', reader=AriesReader(), header=['visual_id_4', 'sub_id', 'supplier_id', 'supplier_panel', 'supplier_panel_X', 'supplier_panel_Y'])

def step_0036_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT 
              ua0.sli_num AS sli_num
             ,u0.visualid AS visual_id_5
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|BTV=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|BTV=([^\|]*).*','\1') end AS btv
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|BTV_TILTX=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|BTV_TILTX=([^\|]*).*','\1') end AS btv_tiltx
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|BTV_TILTY=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|BTV_TILTY=([^\|]*).*','\1') end AS btv_tilty
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|BUMPCOPE_VACUUM=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|BUMPCOPE_VACUUM=([^\|]*).*','\1') end AS bumpcope_vacuum
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CAW_VACUUM=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CAW_VACUUM=([^\|]*).*','\1') end AS caw_vacuum
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV=([^\|]*).*','\1') end AS ctv
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_ANGLE=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_ANGLE=([^\|]*).*','\1') end AS ctv_angle
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_B4=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_B4=([^\|]*).*','\1') end AS ctv_b4
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_B5=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_B5=([^\|]*).*','\1') end AS ctv_b5
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_R=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_R=([^\|]*).*','\1') end AS ctv_r
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_SHAPE=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_SHAPE=([^\|]*).*','\1') end AS ctv_shape
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_SIGN=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_SIGN=([^\|]*).*','\1') end AS ctv_sign
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_TILTX=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_TILTX=([^\|]*).*','\1') end AS ctv_tiltx
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|CTV_TILTY=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|CTV_TILTY=([^\|]*).*','\1') end AS ctv_tilty
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RBTV=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RBTV=([^\|]*).*','\1') end AS rbtv
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RBTV_TILTX=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RBTV_TILTX=([^\|]*).*','\1') end AS rbtv_tiltx
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RBTV_TILTY=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RBTV_TILTY=([^\|]*).*','\1') end AS rbtv_tilty
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RCTV=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RCTV=([^\|]*).*','\1') end AS rctv
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RCTV_TILTX=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RCTV_TILTX=([^\|]*).*','\1') end AS rctv_tiltx
             ,case when instr('|'||ua01.TCB_DFF_DATA, '|RCTV_TILTY=')>=1 then regexp_replace(ua01.TCB_DFF_DATA,'.*\|RCTV_TILTY=([^\|]*).*','\1') end AS rctv_tilty
             ,ua0.DIE_POSITION AS die_position
    FROM 
    A_Ube_Unit_Hist2 u0
    LEFT JOIN (SELECT u0.creation_ww, u0.facility, u0.unit_hist_key
    ,MAX(CASE WHEN uuha.attr_name = 'SLI_NUM' THEN uuha.attr_value END) SLI_NUM
    ,MAX(CASE WHEN uuha.attr_name = 'DIE_POSITION' THEN uuha.attr_value END) DIE_POSITION
    FROM a_ube_unit_hist2 u0, a_ube_unit_hist2_attr uuha
    WHERE u0.creation_ww = uuha.creation_ww AND u0.unit_hist_key = uuha.unit_hist_key AND u0.facility = uuha.facility
     AND      u0.ws_operation In ('9999') 
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a6_5547.tab->999', 'sub_id', 'u0.visualid In') + """)""" + """ 
    AND uuha.attr_name IN ('dummy'
    ,'SLI_NUM'
    ,'DIE_POSITION'
    )
    GROUP BY u0.creation_ww, u0.unit_hist_key,u0.facility)  ua0 ON u0.creation_ww = ua0.creation_ww AND u0.unit_hist_key = ua0.unit_hist_key AND u0.facility = ua0.facility
    LEFT JOIN (SELECT u0.creation_ww, u0.facility, u0.unit_hist_key
    ,MAX(CASE WHEN uuha.attr_name = 'TCB_DFF_DATA' THEN '|' || DBMS_LOB.substr(uuha.attr_value,3000) END) TCB_DFF_DATA
    FROM a_ube_unit_hist2 u0, a_ube_unit_hist2_attr_clob uuha
    WHERE u0.creation_ww = uuha.creation_ww AND u0.unit_hist_key = uuha.unit_hist_key AND u0.facility = uuha.facility
     AND      u0.ws_operation In ('9999') 
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a6_5547.tab->999', 'sub_id', 'u0.visualid In') + """)""" + """ 
    AND uuha.attr_name IN ('dummy'
    ,'TCB_DFF_DATA'
    )
    GROUP BY u0.creation_ww, u0.unit_hist_key,u0.facility)  ua01 ON u0.creation_ww = ua01.creation_ww AND u0.unit_hist_key = ua01.unit_hist_key AND u0.facility = ua01.facility
    WHERE 1=1
     AND      u0.ws_operation In ('9999') 
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a6_5547.tab->999', 'sub_id', 'u0.visualid In') + """)""" + """ 
    /*END SQL*/

    """, output='yeuchuan_a5_5547.tab', reader=AriesReader(), header=['sli_num', 'visual_id_5', 'btv', 'btv_tiltx', 'btv_tilty', 'bumpcope_vacuum', 'caw_vacuum', 'ctv', 'ctv_angle', 'ctv_b4', 'ctv_b5', 'ctv_r', 'ctv_shape', 'ctv_sign', 'ctv_tiltx', 'ctv_tilty', 'rbtv', 'rbtv_tiltx', 'rbtv_tilty', 'rctv', 'rctv_tiltx', 'rctv_tilty', 'die_position'])

def step_0037_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT  DISTINCT 
              u0.lotnum AS test_lot_lot
             ,u0.visualid AS visual_id_1
    FROM 
    A_Ube_Unit_Hist2 u0
    WHERE 1=1
     AND      (u0.visualid In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a0_5547.tab->999', 'interposer_id', 'u0.visualid In') + """)""" + """ 
     AND      (u0.ws_operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\test_op.csv', 1, 'u0.ws_operation In') + """)""" + """ 
    /*END SQL*/

    """, output='yeuchuan_a4_5547.tab', reader=AriesReader(), header=['test_lot_lot', 'visual_id_1'])

def step_0038_sql_query(ctx) -> None:
    ctx.run_query(sql="""
    /*BEGIN SQL*/
    SELECT /*+  use_nl (dt) use_nl (di) */ DISTINCT 
              dt.interface_bin AS interface_bin
             ,dt.functional_bin AS functional_bin
             ,v0.operation AS test_op
             ,v0.lot AS test_lot
             ,To_Char(lao.lao_test_end_date_time,'yyyy-mm-dd hh24:mi:ss') AS lot_latest_test_end_date
             ,di.visual_id AS visual_id_2
    FROM 
    A_Testing_Session v0
    INNER JOIN A_Lot_At_Operation lao ON v0.lot = lao.lot AND v0.operation = lao.Operation AND v0.facility = lao.facility
    INNER JOIN A_Device_Testing dt ON v0.lao_start_ww + 0 = dt.lao_start_ww AND v0.ts_id + 0 = dt.ts_id
    LEFT JOIN A_Device_Item di ON dt.di_id = di.di_id
    WHERE 1=1
     AND      v0.valid_flag = 'Y' 
     AND      dt.within_lao_latest_flag = 'Y' 
     AND      (v0.operation In 
    """ + ctx.csv_io.sql_get_csv_list('.\\test_op.csv', 1, 'v0.operation In') + """)""" + """ 
     AND      (v0.lot In 
    """ + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a4_5547.tab->10', 'test_lot_lot', 'v0.lot In') + """)""" + """ 
    /*END SQL*/

    """, output='yeuchuan_a3_5547.tab', reader=AriesReader(), header=['interface_bin', 'functional_bin', 'test_op', 'test_lot', 'lot_latest_test_end_date', 'visual_id_2'])

def step_0039_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""

    DROP INDEX IF EXISTS IdxA1;
    Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_5547] ([visual_id],[lot_1],[complevel_1]);
    DROP INDEX IF EXISTS IdxA2;
    Create Index IF NOT EXISTS IdxA2 ON [yeuchuan_a2_5547] ([visual_id_3],[lot_2],[md_position_stack]);
    DROP INDEX IF EXISTS IdxA0;
    Create Index IF NOT EXISTS IdxA0 ON [yeuchuan_a0_5547] ([patch_id],[interposer_id]);
    DROP INDEX IF EXISTS IdxA6;
    Create Index IF NOT EXISTS IdxA6 ON [yeuchuan_a6_5547] ([visual_id_4],[sub_id]);
    DROP INDEX IF EXISTS IdxA5;
    Create Index IF NOT EXISTS IdxA5 ON [yeuchuan_a5_5547] ([visual_id_5],[die_position]);
    DROP INDEX IF EXISTS IdxA4;
    Create Index IF NOT EXISTS IdxA4 ON [yeuchuan_a4_5547] ([visual_id_1]);
    DROP INDEX IF EXISTS IdxA3;
    Create Index IF NOT EXISTS IdxA3 ON [yeuchuan_a3_5547] ([visual_id_2]);

    SELECT /*L0*/  DISTINCT 
              a1.[facility] AS [facility]
             ,sql.[tcb_out_date] AS [tcb_out_date]
             ,sql.[tcb_link] AS [tcb_link]
             ,a1.[Bonder] AS [Bonder]
             ,CrossTab->[[a1,5547;:Y]]
             ,a3.[interface_bin] AS [interface_bin]
             ,a3.[functional_bin] AS [functional_bin]
             ,a1.[visual_id] AS [visual_id]
             ,a1.[tray_or_carrier_id] AS [tray_or_carrier_id]
             ,a1.[carrier_x] AS [carrier_x]
             ,a1.[carrier_y] AS [carrier_y]
             ,a3.[test_op] AS [test_op]
             ,a3.[test_lot] AS [test_lot]
             ,a3.[lot_latest_test_end_date] AS [lot_latest_test_end_date]
             ,a1.[complevel_1] AS [complevel_1]
             ,a6.[sub_id] AS [sub_id]
             ,a6.[supplier_id] AS [supplier_id]
             ,a6.[supplier_panel] AS [supplier_panel]
             ,a6.[supplier_panel_X] AS [supplier_panel_X]
             ,a6.[supplier_panel_Y] AS [supplier_panel_Y]
             ,a5.[sli_num] AS [sli_num]
             ,a2.[fab_wafer] AS [fab_wafer]
             ,a2.[fab_lot] AS [fab_lot]
             ,a2.[y_location] AS [y_location]
             ,a2.[x_location] AS [x_location]
             ,a2.[md_position_stack] AS [md_position_stack]
             ,a0.[ngpoi_op] AS [ngpoi_op]
             ,a0.[interposer_id] AS [interposer_id]
             ,sql.[tcb_op] AS [tcb_op]
             ,sql.[tcb_lot] AS [tcb_lot]
             ,sql.[prodgroup3] AS [prodgroup3]
             ,a5.[btv] AS [btv]
             ,a5.[btv_tiltx] AS [btv_tiltx]
             ,a5.[btv_tilty] AS [btv_tilty]
             ,a5.[bumpcope_vacuum] AS [bumpcope_vacuum]
             ,a5.[caw_vacuum] AS [caw_vacuum]
             ,a5.[ctv] AS [ctv]
             ,a5.[ctv_angle] AS [ctv_angle]
             ,a5.[ctv_b4] AS [ctv_b4]
             ,a5.[ctv_b5] AS [ctv_b5]
             ,a5.[ctv_r] AS [ctv_r]
             ,a5.[ctv_shape] AS [ctv_shape]
             ,a5.[ctv_sign] AS [ctv_sign]
             ,a5.[ctv_tiltx] AS [ctv_tiltx]
             ,a5.[ctv_tilty] AS [ctv_tilty]
             ,a5.[rbtv] AS [rbtv]
             ,a5.[rbtv_tiltx] AS [rbtv_tiltx]
             ,a5.[rbtv_tilty] AS [rbtv_tilty]
             ,a5.[rctv] AS [rctv]
             ,a5.[rctv_tiltx] AS [rctv_tiltx]
             ,a5.[rctv_tilty] AS [rctv_tilty]
    FROM 
               [yeuchuan_SQL_5547] sql
     LEFT OUTER JOIN [yeuchuan_a1_5547] a1
      ON sql.[tcb_lot] = a1.[lot_1] 
     LEFT OUTER JOIN [yeuchuan_a2_5547] a2
      ON a1.[visual_id] = a2.[visual_id_3] 
     AND sql.[tcb_lot] = a2.[lot_2] 
     AND a2.[md_position_stack] = a1.[complevel_1] 
     LEFT OUTER JOIN [yeuchuan_a0_5547] a0
      ON a0.[patch_id] = a1.[visual_id] 
     LEFT OUTER JOIN [yeuchuan_a6_5547] a6
      ON a6.[visual_id_4] = a1.[visual_id] 
     LEFT OUTER JOIN [yeuchuan_a5_5547] a5
      ON a5.[visual_id_5] = a6.[sub_id] 
     AND a1.[complevel_1] = a5.[die_position] 
     LEFT OUTER JOIN [yeuchuan_a4_5547] a4
      ON a4.[visual_id_1] = a0.[interposer_id] 
     LEFT OUTER JOIN [yeuchuan_a3_5547] a3
      ON a3.[visual_id_2] = a4.[visual_id_1]
    """, output='data.csv', reader=SqliteReader(), inputs=['yeuchuan_SQL_5547.tab', 'yeuchuan_a1_5547.tab', 'yeuchuan_a2_5547.tab', 'yeuchuan_a0_5547.tab', 'yeuchuan_a6_5547.tab', 'yeuchuan_a5_5547.tab', 'yeuchuan_a4_5547.tab', 'yeuchuan_a3_5547.tab'])

def step_0040_utility(ctx) -> None:
    pass  # TODO: utility command not classified

def step_0043_write_file(ctx) -> None:
    ctx.write_file(path='MyInput.txt', template='\ndt = Current Data Table();\n\n// Get all column references\nall_cols = dt << Get Column Names();\n\n// Create list to store columns to delete\ncols_to_delete = {};\n\n// Check each column\nfor(i = 1, i <= N Items(all_cols), i++,\n    col_ref = Column(dt, all_cols[i]);\n    values = col_ref << Get Values();\n    \n    // If all values are missing, add to deletion list\n    if(N Missing(values) == N Rows(dt),\n        Insert Into(cols_to_delete, all_cols[i]);\n    );\n);\n\n// Delete all empty columns at once\nif(N Items(cols_to_delete) > 0,\n    dt << Delete Columns(cols_to_delete);\n);\n\n\ndt<<new column("Flagged", numeric, formula(If(\n\t:functional_bin == 3410, 1,\n\tIs Missing( :functional_bin ), 2,\n\t0\n)));\n\ndt<<new column("Status", character, formula(If(\n\t:functional_bin == 3410, "Bin3410",\n\tIs Missing( :functional_bin ), "Not Tested",\n\t:interface_bin == 1, "Pass",\n\t"Other Bin"\n)));')

def run() -> None:
    ctx = PipelineContext()
    step_0000_sql_query(ctx)
    step_0001_sql_query(ctx)
    step_0002_sqlite_query(ctx)
    step_0003_sqlite_query(ctx)
    step_0004_sql_query(ctx)
    step_0005_sqlite_query(ctx)
    step_0006_sqlite_query(ctx)
    step_0007_sql_query(ctx)
    step_0008_sql_query(ctx)
    step_0009_sqlite_query(ctx)
    step_0010_sqlite_query(ctx)
    step_0011_sql_query(ctx)
    step_0012_sqlite_query(ctx)
    step_0013_sqlite_query(ctx)
    step_0014_sqlite_query(ctx)
    for __chunk_path in ctx.csv_io.iter_chunks('lotlist.csv', 'flag_lot.csv', 1):
        with ctx.macro.scope(ctx.csv_io.single_row('flag_lot.csv')):
            step_0017_sqlite_query(ctx)
            step_0018_sql_query(ctx)
            step_0019_sql_query(ctx)
            step_0020_sql_query(ctx)
            step_0021_sql_query(ctx)
            step_0022_sql_query(ctx)
            step_0023_sql_query(ctx)
            step_0024_sql_query(ctx)
            step_0025_sqlite_query(ctx)
            step_0026_utility(ctx)
    for __chunk_path in ctx.csv_io.iter_chunks('lotlist_int.csv', 'flag_int_lot.csv', 1):
        with ctx.macro.scope(ctx.csv_io.single_row('flag_int_lot.csv')):
            step_0031_sqlite_query(ctx)
            step_0032_sql_query(ctx)
            step_0033_sql_query(ctx)
            step_0034_sql_query(ctx)
            step_0035_sql_query(ctx)
            step_0036_sql_query(ctx)
            step_0037_sql_query(ctx)
            step_0038_sql_query(ctx)
            step_0039_sqlite_query(ctx)
            step_0040_utility(ctx)
    step_0043_write_file(ctx)

if __name__ == "__main__":
    run()