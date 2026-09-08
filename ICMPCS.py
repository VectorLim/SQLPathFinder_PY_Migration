# SQL statements containing filters:
# - step_0016_sqlite_query (Line 2412): filters on a0.icmpcs
# - step_0043_sql_query (Line 2548): filters on f0.history_deleted_flag, f0.operation, f0.out_date, f0.owner, f4.history_deleted_flag, f4.unique_flag, f5.history_deleted_flag, f5.transaction, la.attribute_number, p.latest_version
# - step_0044_sql_query (Line 2598): filters on bams0.lot, bams0.operation, yeuchuan_a1_22697.tab
# - step_0046_sqlite_query (Line 2716): filters on FlagLot, a0.rowid, lot_1

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
import ast
import csv
import inspect
import keyring
import logging
import oracledb
import os
import pandas
import pandas as pd
import re
import shlex
import shutil
import smtplib
import sqlite3
import subprocess
import sys


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
    SMART_APPEND = "SMART_APPEND"
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
                columns = {
                    str(column).casefold(): column for column in content.columns
                }
                content = content.reindex(
                    columns=[columns.get(column.casefold(), column) for column in header]
                )
                content.columns = header
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


class OracleClient:
    """Select an Oracle client before DataSyncX opens its first connection."""

    utility_name = "oracle_client"
    _reported_client = False
    _selected_instant_client: Path | None = None

    @classmethod
    def configure(cls) -> str | None:
        """Prepare the current process for the configured DataSyncX Oracle client.

        Set ``DATASYNCX_ORACLE_CLIENT=instant`` to opt in.  The normal
        ORACLE_HOME-based setup remains untouched when it is unset or ``home``.
        """

        mode = os.getenv("DATASYNCX_ORACLE_CLIENT", "home").strip().lower()
        if mode in {"", "home"}:
            return None
        if mode != "instant":
            raise RuntimeError(
                "DATASYNCX_ORACLE_CLIENT must be 'home' or 'instant', " f"not {mode!r}."
            )
        if sys.platform != "win32":
            raise RuntimeError(
                "DataSyncX 1.1.6 initializes python-oracledb without lib_dir. "
                "On Linux, configure Instant Client with ldconfig (preferred) or "
                "LD_LIBRARY_PATH before starting Python; on macOS, update DataSyncX "
                "to pass lib_dir before using this selector."
            )

        client_dir = cls._find_instant_client()
        network_dir = cls._configure_network_files(client_dir)
        if network_dir is not None:
            pass

            oracledb.defaults.config_dir = str(network_dir)
        os.environ.pop("ORACLE_HOME", None)
        cls._prepend_path(client_dir)
        cls._selected_instant_client = client_dir
        return str(client_dir)

    @classmethod
    def log_active_client(cls) -> None:
        """Print the initialized Oracle client once for terminal diagnostics."""

        if cls._reported_client:
            return

        pass

        if oracledb.is_thin_mode():
            return

        try:
            version = ".".join(str(part) for part in oracledb.clientversion())
        except oracledb.Error:
            return
        source = (
            f"Instant Client ({cls._selected_instant_client})"
            if cls._selected_instant_client
            else f"ORACLE_HOME ({os.getenv('ORACLE_HOME', 'PATH')})"
        )
        print("\n" + "=" * 72)
        print(f" Oracle client: {version} | mode=thick | source={source}")
        print("=" * 72)
        cls._reported_client = True

    @staticmethod
    def _find_instant_client() -> Path:
        configured = os.getenv("DATASYNCX_INSTANT_CLIENT_DIR") or os.getenv(
            "ORACLE_INSTANT_CLIENT_DIR"
        )
        candidates = (
            [configured] if configured else os.getenv("PATH", "").split(os.pathsep)
        )
        for candidate in candidates:
            if not candidate:
                continue
            path = Path(candidate).expanduser()
            if "instantclient" in path.name.lower() and (path / "oci.dll").is_file():
                return path.resolve()

        raise RuntimeError(
            "Oracle Instant Client was requested but no usable directory was "
            "found. "
            "Set DATASYNCX_INSTANT_CLIENT_DIR to the directory containing "
            "oci.dll."
        )

    @staticmethod
    def _configure_network_files(client_dir: Path) -> Path | None:
        configured = os.getenv("DATASYNCX_ORACLE_NET_CONFIG_DIR")
        network_dir = (
            Path(configured).expanduser()
            if configured
            else client_dir / "network" / "admin"
        )
        if configured and not network_dir.is_dir():
            raise RuntimeError(
                "DATASYNCX_ORACLE_NET_CONFIG_DIR does not exist or is not a "
                "directory: "
                f"{network_dir}"
            )
        if network_dir.is_dir():
            network_dir = network_dir.resolve()
            os.environ["TNS_ADMIN"] = str(network_dir)
            return network_dir
        return None

    @staticmethod
    def _prepend_path(client_dir: Path) -> None:
        entries = [entry for entry in os.getenv("PATH", "").split(os.pathsep) if entry]
        selected = str(client_dir)
        os.environ["PATH"] = os.pathsep.join(
            [selected, *(entry for entry in entries if Path(entry) != client_dir)]
        )


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
        try:
            result = reader.read(site=node, query=sql)
        finally:
            OracleClient.log_active_client()
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
            node
            or self.macro.named("NODE")
            or os.environ.get("VG2C_DEFAULT_NODE", "KM")
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

        pass

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
        pass
        pass

        utilities = block.resolved_options.lookup.get("UTILITIES", "")
        argv = split_utility_command(utilities)
        # argv[0] = '{ROWS-IN-FILE}', argv[1] = csv_path, argv[2] = var_name
        csv_path_expr = MacroState.to_py_expr(argv[1] if len(argv) > 1 else None)
        var_name = strip_quotes(argv[2]).upper() if len(argv) > 2 else ""

        row_count_call = CsvIO.row_count.render(csv_path_expr)
        stmt = MacroState.set_named.render(repr(var_name), f"str({row_count_call})")
        return "rows_in_file", [stmt]


class SmartAppend:
    """Append CSV data while preserving one destination header."""

    utility_name = "smart_append"
    _COMMAND_NAME = "smartappend.va"

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        argv = split_utility_command(options.lookup.get("UTILITIES", ""))
        if not argv:
            return None
        basename = argv[0].replace("/", "\\").rsplit("\\", 1)[-1].lower()
        if basename == SmartAppend._COMMAND_NAME:
            return Kind.SMART_APPEND, "/UTILITIES command maps to SmartAppend"
        return None

    @classmethod
    def emit_block(cls, block) -> tuple[str, list[str]]:
        argv = split_utility_command(block.resolved_options.lookup.get("UTILITIES", ""))
        destination = MacroState.to_py_expr(argv[1] if len(argv) > 1 else "")
        source = MacroState.to_py_expr(argv[2] if len(argv) > 2 else "")
        return "smart_append", [cls.append.render(destination, source)]

    def append(self, destination: str | Path, source: str | Path) -> None:
        """Create or extend *destination* with source data rows exactly once."""
        source_path = resolve_path(source)
        destination_path = resolve_path(destination, for_write=True)
        destination_path.parent.mkdir(parents=True, exist_ok=True)

        with source_path.open(
            newline="", encoding="utf-8", errors="replace"
        ) as source_fh:
            reader = csv.reader(source_fh)
            header = next(reader, None)
            if header is None:
                return

            write_header = (
                not destination_path.exists() or destination_path.stat().st_size == 0
            )
            with destination_path.open(
                "w" if write_header else "a", newline="", encoding="utf-8"
            ) as destination_fh:
                writer = csv.writer(destination_fh)
                if write_header:
                    writer.writerow(header)
                writer.writerows(reader)


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


class SqliteReader:
    """Run SQL joins over CSV files using in-memory SQLite."""

    utility_name = "sqlite_reader"

    STMT_SPLIT_RE = re.compile(
        r"(?:'[^']*'|\"[^\"]*\"|\[[^\]]*\]|`[^`]*`|[^;])+",
        re.DOTALL,
    )

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

        final_stmt = CrosstabUtility.substitute_sql(
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


# <vg2c:dependencies:end>
# <vg2c:steps:start>
def step_0000_html_report(ctx) -> None:
    ctx.html_report.run(
        instance="22697",
        prompt_text="Step 1-1. create revision footer",
        app_server_default="atd_atm.hadoop",
        template="\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nType<\\\\>Key<\\\\>COL1<\\\\>COL2<\\\\>COL3<\\\\>COL4<\\\\>COL5<\\\\>COL6<\\\\>COL7<\\\\>COL8\nTYPE<\\\\>CSS<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCSS<\\\\>sqlpathfinder_style_1.css<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nFORMAT<\\\\>Column-Headers<\\\\>background-color:#dbd9c0<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>text-decoration:normal<\\\\>vertical-align:middle\nFORMAT<\\\\>Column-Data<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>text-align:left<\\\\>vertical-align:middle<\\\\>\nFORMAT<\\\\>Column-Alt-Row<\\\\>background-color:#f7f5dc<\\\\>color:#333<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>text-align:left<\\\\>vertical-align:middle<\\\\>\nFORMAT<\\\\>At-Top-of-Report<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:15<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:center<\\\\>vertical-align:middle\nFORMAT<\\\\>At-Top-of-Col1<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>At-Top-of-Col2<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>At-Top-of-Col3<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>JQX-All-IChart-Text<\\\\>background-color:white<\\\\>color:black<\\\\>font-family:Verdana<\\\\>font-size:11<\\\\>font-style:normal<\\\\>font-weight:normal<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>COLUMN-BORDER<\\\\>border-color:#cc9<\\\\>border-collapse:collapse<\\\\>border-style:solid<\\\\>border-width:1px<\\\\>border-spacing:4px<\\\\><\\\\><\\\\>",
    )


def step_0001_html_report(ctx) -> None:
    ctx.html_report.layout(
        ctx,
        outlook="N",
        instance="22697",
        json_only="N",
        chart_instance="3450",
        app_server_default="atd_atm.hadoop",
        template='<table class="tblout"><tr class="tblout"><td class="tblout" valign="top">\n:FILE:revision.htm\n:CSS:sqlpathfinder_style_1.css\n:CSSEMBED:Y\n:RR:NO\n:B:Y\n:EM-A:\n:EM-S:\n:SEC:Y\n:TITLE:revision\n<table class="tblout">\n<tr class="tblout">\n<td class="tblout">\n<p style="text-align: left" style="background-color: white"><i><font face="arial" size="2.66666666666667" color="silver"><script filename><br>- <changes made></font></i>\n</td>\n</tr>\n</table>\n</td><td class="tblout" valign="top">\n<table class="tblout">\n<tr class="tblout"><td class="tblout"></td></tr>\n</table>\n</td></tr></table>',
    )


def step_0002_html_report(ctx) -> None:
    ctx.html_report.delete(instance="22697")


def step_0003_write_file(ctx) -> None:
    ctx.write_file(
        path="macrotmp.csv",
        template="\nSfolder,underDEV,useCSR,useMMS\nICMPCS_CWFNCO_CSR_IAM,N,Y,Y",
    )


def step_0004_write_file(ctx) -> None:
    ctx.write_file(
        path="getcsrsu.bat",
        template='\n@echo off\nset PriCSR="\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset SecCSR="\\\\KMATSHFS.intel.com\\KMATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset BakCSR="\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\%username%\\Patrol\\*.___"\ncopy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .\nren setsiteparam.___ setsiteparam.exe',
    )


def step_0005_external(ctx) -> None:
    ctx.external.run(argv=["getcsrsu.bat"])


def step_0007_external(ctx) -> None:
    ctx.external.run(
        argv=[
            "setsiteparam.exe",
            "KM",
            ctx.macro.named("SFOLDER"),
            ctx.macro.named("UNDERDEV"),
            ctx.macro.named("USECSR"),
            ctx.macro.named("USEMMS"),
        ]
    )


def step_0008_write_file(ctx) -> None:
    ctx.write_file(path="TT", template="\nSST Rev3g")


def step_0010_fs_delete(ctx) -> None:
    ctx.fs_ops.delete(
        paths=["macrotmp.csv", "getcsrsu.bat", "setsiteparam.exe", "csrsu.txt"]
    )


def step_0012_rows_in_file(ctx) -> None:
    ctx.macro.set_named("CONFIG", str(ctx.csv_io.row_count("ICMPCS_config.csv")))


def step_0014_email(ctx) -> None:
    ctx.email.send(
        to="yeu.chuan.lim@intel.com",
        subject="Critical: ICMPCS config file not found - Path: \\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\"
        + ctx.macro.named("SFOLDER")
        + "\\KM\\Config",
        body="",
    )


def step_0016_sqlite_query(ctx) -> None:
    ctx.run_query(
        sql="""
    SELECT /*L10*/  DISTINCT 
              [icmpcs] AS [icmpcs]
             ,[parameter] AS [parameter]
             ,Max([value]) AS [value]
             ,[STARTTS] AS [STARTTS]
             ,[UTC] AS [UTC]
             ,[TZONE] AS [TZONE]
             ,[TZS] AS [TZS]
             ,[SFOLDER] AS [SFOLDER]
             ,[FAC] AS [FAC]
             ,[MARS] AS [MARS]
             ,[MARSN] AS [MARSN]
             ,[RIMS] AS [RIMS]
             ,[EIMS] AS [EIMS]
             ,[ARIES] AS [ARIES]
             ,[OASYS] AS [OASYS]
             ,[MONGO] AS [MONGO]
             ,[MMS] AS [MMS]
             ,[MMSI] AS [MMSI]
             ,[TOOLLOG] AS [TOOLLOG]
             ,[VFMARS] AS [VFMARS]
             ,[VFARIES] AS [VFARIES]
             ,[VFMONGO] AS [VFMONGO]
             ,[CSRPATH] AS [CSRPATH]
             ,[MMSPATH] AS [MMSPATH]
             ,[MIPPATH] AS [MIPPATH]
             ,[LURL] AS [LURL]
             ,[IREPOP] AS [IREPOP]
             ,[UNDERDEV] AS [UNDERDEV]
             ,[CSRV] AS [CSRV]
             ,[MMSV] AS [MMSV]
    FROM
    (
    SELECT /*L0*/  
              a0.[icmpcs] AS [icmpcs]
             ,a0.[parameter] AS [parameter]
             ,a0.[value] AS [value]
             ,'<<<STARTTS>>>' AS [STARTTS]
             ,'<<<UTC>>>' AS [UTC]
             ,'<<<TZONE>>>' AS [TZONE]
             ,'<<<TZS>>>' AS [TZS]
             ,'<<<SFOLDER>>>' AS [SFOLDER]
             ,'<<<FAC>>>' AS [FAC]
             ,'<<<MARS>>>' AS [MARS]
             ,'<<<MARSN>>>' AS [MARSN]
             ,'<<<RIMS>>>' AS [RIMS]
             ,'<<<EIMS>>>' AS [EIMS]
             ,'<<<ARIES>>>' AS [ARIES]
             ,'<<<OASYS>>>' AS [OASYS]
             ,'<<<MONGO>>>' AS [MONGO]
             ,'<<<MMS>>>' AS [MMS]
             ,'<<<MMSI>>>' AS [MMSI]
             ,'<<<TOOLLOG>>>' AS [TOOLLOG]
             ,'<<<VFMARS>>>' AS [VFMARS]
             ,'<<<VFARIES>>>' AS [VFARIES]
             ,'<<<VFMONGO>>>' AS [VFMONGO]
             ,'<<<CSRPATH>>>' AS [CSRPATH]
             ,'<<<MMSPATH>>>' AS [MMSPATH]
             ,'<<<MIPPATH>>>' AS [MIPPATH]
             ,'<<<LURL>>>' AS [LURL]
             ,'<<<IREPOP>>>' AS [IREPOP]
             ,'<<<UNDERDEV>>>' AS [UNDERDEV]
             ,'<<<CSRV>>>' AS [CSRV]
             ,'<<<MMSV>>>' AS [MMSV]
    FROM 
    [ICMPCS_config] a0
    WHERE
                  a0.[icmpcs] = 'ICMPCS' 
    ) t /*L0*/
    GROUP BY 
              [icmpcs]
             ,[parameter]
             ,[STARTTS]
             ,[UTC]
             ,[TZONE]
             ,[TZS]
             ,[SFOLDER]
             ,[FAC]
             ,[MARS]
             ,[MARSN]
             ,[RIMS]
             ,[EIMS]
             ,[ARIES]
             ,[OASYS]
             ,[MONGO]
             ,[MMS]
             ,[MMSI]
             ,[TOOLLOG]
             ,[VFMARS]
             ,[VFARIES]
             ,[VFMONGO]
             ,[CSRPATH]
             ,[MMSPATH]
             ,[MIPPATH]
             ,[LURL]
             ,[IREPOP]
             ,[UNDERDEV]
             ,[CSRV]
             ,[MMSV]
    """,
        output="configsets.csv",
        reader=SqliteReader(),
        inputs=["ICMPCS_config.csv"],
        crosstab={
            "row_keys": [
                "icmpcs",
                "STARTTS",
                "UTC",
                "TZONE",
                "TZS",
                "SFOLDER",
                "FAC",
                "MARS",
                "MARSN",
                "RIMS",
                "EIMS",
                "ARIES",
                "OASYS",
                "MONGO",
                "MMS",
                "MMSI",
                "TOOLLOG",
                "VFMARS",
                "VFARIES",
                "VFMONGO",
                "CSRPATH",
                "MMSPATH",
                "MIPPATH",
                "LURL",
                "IREPOP",
                "UNDERDEV",
                "CSRV",
                "MMSV",
            ],
            "header_key": "parameter",
            "value_key": "value",
        },
    )


def step_0017_rows_in_file(ctx) -> None:
    ctx.macro.set_named("CONFIGSETS", str(ctx.csv_io.row_count("configsets.csv")))


def step_0019_email(ctx) -> None:
    ctx.email.send(
        to="yeu.chuan.lim@intel.com",
        subject="Alert: Pls check ICMPCS ("
        + ctx.macro.named("SFOLDER")
        + ") config file as it contains not equal to 1 row",
        body="",
        attachments=["ICMPCS_config.csv", "configsets.csv"],
    )


def step_0023_write_file(ctx) -> None:
    ctx.write_file(
        path="CSRVerror.htm",
        template="\n<!DOCTYPE html>\n<html>\n<body>\n<p>It is detected that you cannot access to CSR depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>CSR Superuser</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong>\nPath: <<<CSRPATH>>></p>\n</body>\n</html>",
    )


def step_0024_email(ctx) -> None:
    ctx.email.send(
        to="yeu.chuan.lim@intel.com",
        subject="Critical: Cannot access to " + ctx.macro.named("CSRPATH"),
        body="CSRVerror.htm",
    )


def step_0027_write_file(ctx) -> None:
    ctx.write_file(
        path="MMSVerror.htm",
        template="\n<!DOCTYPE html>\n<html\n<body>\n<p>It is detected that you cannot access to MMS Signal Tracer depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>MMS Signal Tracer Admin</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong><br/>\nPath: <<<MMSPATH>>></p>\n</body>\n</html>",
    )


def step_0028_email(ctx) -> None:
    ctx.email.send(
        to="yeu.chuan.lim@intel.com",
        subject="Critical: Cannot access to " + ctx.macro.named("MMSPATH"),
        body="MMSVerror.htm",
    )


def step_0030_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(
        src=str(
            Path(
                "\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\"
                + ctx.macro.named("SFOLDER")
                + "\\KM\\HIST"
            )
            / "HIST.txt"
        ),
        dst=".",
    )


def step_0031_rows_in_file(ctx) -> None:
    ctx.macro.set_named("HIST", str(ctx.csv_io.row_count("HIST.txt")))


def step_0033_write_file(ctx) -> None:
    ctx.write_file(
        path="HIST.csv", template="\nLOT,OUT_DATE\nDUMMY,2000-01-01 00:00:00"
    )


def step_0034_write_file(ctx) -> None:
    ctx.write_file(path="HISTERROR.txt", template="\nERROR\nERROR\nERROR")


def step_0036_fs_copy(ctx) -> None:
    ctx.fs_ops.rename(src="HIST.txt", dst="HIST.csv")


def step_0043_sql_query(ctx) -> None:
    ctx.run_query(
        sql="""
    /*BEGIN SQL*/
    SELECT 
              lot_1 AS lot_1
             ,operation_1 AS operation_1
             ,To_Char(out_date,'yyyy-mm-dd hh24:mi:ss') AS out_date
             ,oldqty1 AS oldqty1
             ,newqty1 AS newqty1
             ,Replace(Replace(Replace(Replace(Replace(Replace(Interposer_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Interposer_SLI
             ,Replace(Replace(Replace(Replace(Replace(Replace(Patch_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Patch_SLI
             ,prodgroup3_1 AS prodgroup3_1
             ,entity AS entity
             ,transaction AS transaction
    FROM
    (
    SELECT  
              f0.lot AS lot_1
             ,f0.operation AS operation_1
             ,f0.out_date AS out_date
             ,f0.oldqty1 AS oldqty1
             ,f0.newqty1 AS newqty1
             ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 5005 AND la.src_erase_date IS NULL AND rownum <= 1) AS Interposer_SLI
             ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 5001 AND la.src_erase_date IS NULL AND rownum <= 1) AS Patch_SLI
             ,p.prodgroup3 AS prodgroup3_1
             ,f4.entity AS entity
             ,f5.transaction AS transaction
    FROM 
    @[]@.F_LotHist f0
    LEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
    INNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot
    LEFT JOIN @[]@.F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = 'Y'
     AND      f4.entity Like 'IAM%' 
    LEFT JOIN @[]@.F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource
    LEFT JOIN @[]@.F_LotTxnHist f5 ON f5.lot = f0.lot AND f5.operation = f0.operation AND f5.prevout_date = f0.prevout_date AND NVL(f5.history_deleted_flag,'N') = 'N'
     AND      f5.transaction = 'MVOU' 
    WHERE
    NVL(f0.history_deleted_flag,'N') = 'N'
    AND      f0.owner <> 'EMPTYFOUP'
     AND      f0.operation = '2303' 
     AND      f0.out_date >= TRUNC(SYSDATE) - 2 
     AND      p.prodgroup3 Like 'CWF%' 
    -- Tail A
    )
    WHERE
                  Interposer_SLI Is Not Null  
    /*END SQL*/

    """,
        output="yeuchuan_a1_22697.tab",
        reader=MarsReader(),
        header=[
            "lot_1",
            "operation_1",
            "out_date",
            "oldqty1",
            "newqty1",
            "Interposer_SLI",
            "Patch_SLI",
            "prodgroup3_1",
            "entity",
            "transaction",
        ],
    )


def step_0044_sql_query(ctx) -> None:
    ctx.run_query(
        sql="""
    /*BEGIN SQL*/
    SELECT 
              facility AS facility
             ,operation AS operation
             ,module_name AS module_name
             ,tool_entity AS tool_entity
             ,primary_entity AS primary_entity
             ,To_Char(processing_start_date,'yyyy-mm-dd hh24:mi:ss') AS processing_start_date
             ,To_Char(processing_end_date,'yyyy-mm-dd hh24:mi:ss') AS processing_end_date
             ,lot AS lot
             ,product AS product
             ,prodgroup3 AS prodgroup3
             ,Replace(Replace(Replace(Replace(Replace(Replace(product_desc,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS product_desc
             ,owner AS owner
             ,visual_id AS visual_id
             ,ws_loss_code AS ws_loss_code
             ,media_in_x AS media_in_x
             ,media_in_y AS media_in_y
             ,parameter AS parameter
             ,Max(numeric_value) AS numeric_value
    FROM
    (
    SELECT  
              bams0.facility AS facility
             ,bams0.operation AS operation
             ,bams0.module_name AS module_name
             ,bams0.tool_entity AS tool_entity
             ,bams0.primary_entity AS primary_entity
             ,bams0.processing_start_time AS processing_start_date
             ,bams0.processing_end_time AS processing_end_date
             ,bams0.lot AS lot
             ,ml.product AS product
             ,mp.prodgroup3 AS prodgroup3
             ,mp.product_description AS product_desc
             ,ml.owner AS owner
             ,bams2.visual_id AS visual_id
             ,bams2.ws_loss_code AS ws_loss_code
             ,bams2.media_in_x AS media_in_x
             ,bams2.media_in_y AS media_in_y
             ,bams3.parameter AS parameter
             ,bams3.numeric_value AS numeric_value
    FROM 
    ARIES_Views.AV_BAMS_SESSION bams0
    LEFT JOIN A_MARS_Lot ml ON bams0.lot=ml.lot
    LEFT JOIN A_MARS_Product mp ON ml.product = mp.product AND ml.mars_schema=mp.mars_schema AND mp.facility=bams0.facility
    INNER JOIN ARIES_Views.AV_BAMS_MEDIA_TESTING bams1 ON bams1.lao_start_ww = bams0.lao_start_ww AND bams1.obj_s_id = bams0.obj_s_id
    INNER JOIN ARIES_Views.AV_BAMS_UNIT_TESTING bams2 ON bams2.lao_start_ww = bams1.lao_start_ww AND bams2.obj_s_id = bams1.obj_s_id AND bams2.obj_mt_id = bams1.obj_mt_id
    LEFT JOIN ARIES_Views.AV_BAMS_DEVICE_RESULTS bams3 ON bams3.lao_start_ww = bams2.lao_start_ww AND bams3.obj_s_id = bams2.obj_s_id AND bams3.obj_mt_id = bams2.obj_mt_id AND bams3.obj_ut_id = bams2.obj_ut_id
    WHERE
                  (bams0.lot In 
    """
        + ctx.csv_io.sql_get_csv_list(
            ".\\yeuchuan_a1_22697.tab", "lot_1", "bams0.lot In"
        )
        + """)"""
        + """ 
     AND      bams0.operation = '2303' 
    )
    GROUP BY 
              facility
             ,operation
             ,module_name
             ,tool_entity
             ,primary_entity
             ,processing_start_date
             ,processing_end_date
             ,lot
             ,product
             ,prodgroup3
             ,product_desc
             ,owner
             ,visual_id
             ,ws_loss_code
             ,media_in_x
             ,media_in_y
             ,parameter
    /*END SQL*/

    """,
        output="yeuchuan_a0_22697.tab",
        reader=AriesReader(),
        crosstab={
            "row_keys": [
                "facility",
                "operation",
                "module_name",
                "tool_entity",
                "primary_entity",
                "processing_start_date",
                "processing_end_date",
                "lot",
                "product",
                "prodgroup3",
                "product_desc",
                "owner",
                "visual_id",
                "ws_loss_code",
                "media_in_x",
                "media_in_y",
            ],
            "header_key": "parameter",
            "value_key": "numeric_value",
        },
    )


def step_0045_sqlite_query(ctx) -> None:
    ctx.run_query(
        sql="""

    DROP INDEX IF EXISTS IdxA0;
    Create Index IF NOT EXISTS IdxA0 ON [yeuchuan_a0_22697] ([lot],[operation]);

    SELECT /*L0*/  DISTINCT 
              a1.[lot_1] AS [lot_1]
             ,a1.[operation_1] AS [operation_1]
             ,a1.[out_date] AS [out_date]
             ,a1.[oldqty1] AS [oldqty1]
             ,a1.[newqty1] AS [newqty1]
             ,a0.[facility] AS [facility]
             ,a0.[operation] AS [operation]
             ,a0.[module_name] AS [module_name]
             ,a0.[tool_entity] AS [tool_entity]
             ,a0.[primary_entity] AS [primary_entity]
             ,a0.[processing_start_date] AS [processing_start_date]
             ,a0.[processing_end_date] AS [processing_end_date]
             ,a0.[lot] AS [lot]
             ,a0.[product] AS [product]
             ,a0.[prodgroup3] AS [prodgroup3]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a0.[product_desc],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [product_desc]
             ,a0.[owner] AS [owner]
             ,a0.[visual_id] AS [visual_id]
             ,a0.[ws_loss_code] AS [ws_loss_code]
             ,a0.[media_in_x] AS [media_in_x]
             ,a0.[media_in_y] AS [media_in_y]
             ,CrossTab->[[a0,22697;:Y]]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a1.[Interposer_SLI],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [Interposer_SLI]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a1.[Patch_SLI],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [Patch_SLI]
             ,a1.[prodgroup3_1] AS [prodgroup3_1]
             ,a1.[entity] AS [entity]
             ,a1.[transaction] AS [transaction]
    FROM 
               [yeuchuan_a1_22697] a1
     LEFT OUTER JOIN [yeuchuan_a0_22697] a0
      ON a1.[lot_1] = a0.[lot] 
     AND a1.[operation_1] = a0.[operation]
    """,
        output="PARMI_IPM_RAW.csv",
        reader=SqliteReader(),
        inputs=["yeuchuan_a1_22697.tab", "yeuchuan_a0_22697.tab"],
    )


def step_0046_sqlite_query(ctx) -> None:
    ctx.run_query(
        sql="""

    DROP TABLE IF EXISTS T_L0_Init;
    CREATE TABLE T_L0_Init AS
    SELECT /*L0*/  
              a0.[lot_1] AS [lot_1]
             ,a0.[newqty1] AS [newqty1]
             ,a0.[facility] AS [facility]
             ,a0.[operation] AS [operation]
             ,a0.[tool_entity] AS [tool_entity]
             ,a0.[primary_entity] AS [primary_entity]
             ,a0.[processing_end_date] AS [processing_end_date]
             ,a0.[lot] AS [lot]
             ,a0.[prodgroup3] AS [prodgroup3]
             ,a0.[product] AS [product]
             ,a0.[visual_id] AS [visual_id]
             ,a0.[ws_loss_code] AS [ws_loss_code]
             ,a0.[media_in_x] AS [media_in_x]
             ,a0.[media_in_y] AS [media_in_y]
             ,a0.[height] AS [height]
             ,a0.[patch_lift_roi1] AS [patch_lift_roi1]
             ,a0.[patch_lift_roi2] AS [patch_lift_roi2]
             ,a0.[patch_lift_roi3] AS [patch_lift_roi3]
             ,a0.[patch_lift_roi4] AS [patch_lift_roi4]
             ,a0.[patch_lift_roi5] AS [patch_lift_roi5]
             ,a0.[patch_lift_roi6] AS [patch_lift_roi6]
             ,a0.[patch_lift_roi7] AS [patch_lift_roi7]
             ,a0.[patch_lift_roi8] AS [patch_lift_roi8]
             ,a0.[patch_lift_roi_max] AS [patch_lift_roi_max]
             ,a0.[patch_sli] AS [patch_sli]
             ,a0.[interposer_sli] AS [interposer_sli]
             ,CASE  WHEN a0.[patch_lift_roi4]  >= 2000 AND  a0.[patch_lift_roi_max]  >= 2030 THEN '1' WHEN a0.[patch_lift_roi8] >= 2000 AND  a0.[patch_lift_roi_max]  >= 2030 THEN '1' ELSE '0' END AS [NCO_Risk]
    FROM 
    [PARMI_IPM_RAW] a0
    ;

    DROP TABLE IF EXISTS T_L0_1_1;
    CREATE TABLE T_L0_1_1 AS
    SELECT COUNT(DISTINCT  visual_id) AS AF$S1
    ,lot_1 AS AF$PB1
    FROM T_L0_Init GROUP BY 
    AF$PB1
    ;
    CREATE INDEX T_L0_1_1_Idx ON T_L0_1_1 (AF$PB1);
    DROP TABLE IF EXISTS T_L0_1_Result;
    CREATE TABLE T_L0_1_Result AS
    SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [VIDCount]
    FROM T_L0_Init a0 LEFT JOIN T_L0_1_1 a1 ON 
    lot_1 = a1.AF$PB1
    ;
    DROP TABLE IF EXISTS T_L0_1_1;

    CREATE INDEX T_L0_1_Result_Idx ON T_L0_1_Result (orig_rowid);
    DROP TABLE IF EXISTS T_L0_Result;
    CREATE TABLE T_L0_Result AS
    SELECT

    [lot_1]
    ,[newqty1]
    ,[facility]
    ,[operation]
    ,[tool_entity]
    ,[primary_entity]
    ,[processing_end_date]
    ,[lot]
    ,[prodgroup3]
    ,[product]
    ,[visual_id]
    ,[ws_loss_code]
    ,[media_in_x]
    ,[media_in_y]
    ,[height]
    ,[patch_lift_roi1]
    ,[patch_lift_roi2]
    ,[patch_lift_roi3]
    ,[patch_lift_roi4]
    ,[patch_lift_roi5]
    ,[patch_lift_roi6]
    ,[patch_lift_roi7]
    ,[patch_lift_roi8]
    ,[patch_lift_roi_max]
    ,[patch_sli]
    ,[interposer_sli]
    ,[NCO_Risk]
    ,[VIDCount]
    FROM T_L0_Init a0
    LEFT JOIN T_L0_1_Result a1 ON a0.rowid = a1.orig_rowid
    ;
    DROP TABLE IF EXISTS T_L0_1_Result;
    DROP TABLE IF EXISTS T_L0_Init;

    SELECT /*L3*/ 
              [lot_1] AS [lot_1]
             ,[newqty1] AS [newqty1]
             ,[facility] AS [facility]
             ,[operation] AS [operation]
             ,[tool_entity] AS [tool_entity]
             ,[primary_entity] AS [primary_entity]
             ,[processing_end_date] AS [processing_end_date]
             ,[lot] AS [lot]
             ,[prodgroup3] AS [prodgroup3]
             ,[product] AS [product]
             ,[visual_id] AS [visual_id]
             ,[ws_loss_code] AS [ws_loss_code]
             ,[media_in_x] AS [media_in_x]
             ,[media_in_y] AS [media_in_y]
             ,[height] AS [height]
             ,[patch_lift_roi1] AS [patch_lift_roi1]
             ,[patch_lift_roi2] AS [patch_lift_roi2]
             ,[patch_lift_roi3] AS [patch_lift_roi3]
             ,[patch_lift_roi4] AS [patch_lift_roi4]
             ,[patch_lift_roi5] AS [patch_lift_roi5]
             ,[patch_lift_roi6] AS [patch_lift_roi6]
             ,[patch_lift_roi7] AS [patch_lift_roi7]
             ,[patch_lift_roi8] AS [patch_lift_roi8]
             ,[patch_lift_roi_max] AS [patch_lift_roi_max]
             ,[patch_sli] AS [patch_sli]
             ,[interposer_sli] AS [interposer_sli]
             ,[NCO_Risk] AS [NCO_Risk]
             ,[VIDCount] AS [VIDCount]
             ,[FlagLot] AS [FlagLot]
    FROM
    (
    SELECT /*L2*/ 
              [lot_1] AS [lot_1]
             ,[newqty1] AS [newqty1]
             ,[facility] AS [facility]
             ,[operation] AS [operation]
             ,[tool_entity] AS [tool_entity]
             ,[primary_entity] AS [primary_entity]
             ,[processing_end_date] AS [processing_end_date]
             ,[lot] AS [lot]
             ,[prodgroup3] AS [prodgroup3]
             ,[product] AS [product]
             ,[visual_id] AS [visual_id]
             ,[ws_loss_code] AS [ws_loss_code]
             ,[media_in_x] AS [media_in_x]
             ,[media_in_y] AS [media_in_y]
             ,[height] AS [height]
             ,[patch_lift_roi1] AS [patch_lift_roi1]
             ,[patch_lift_roi2] AS [patch_lift_roi2]
             ,[patch_lift_roi3] AS [patch_lift_roi3]
             ,[patch_lift_roi4] AS [patch_lift_roi4]
             ,[patch_lift_roi5] AS [patch_lift_roi5]
             ,[patch_lift_roi6] AS [patch_lift_roi6]
             ,[patch_lift_roi7] AS [patch_lift_roi7]
             ,[patch_lift_roi8] AS [patch_lift_roi8]
             ,[patch_lift_roi_max] AS [patch_lift_roi_max]
             ,[patch_sli] AS [patch_sli]
             ,[interposer_sli] AS [interposer_sli]
             ,[NCO_Risk] AS [NCO_Risk]
             ,[VIDCount] AS [VIDCount]
             ,CASE WHEN  [NCO_Risk]  = '1' OR  [VIDCount] < 2 THEN '1' ELSE '0' END AS [FlagLot]
    FROM
    (
    SELECT /*L1*/ 
              [lot_1] AS [lot_1]
             ,[newqty1] AS [newqty1]
             ,[facility] AS [facility]
             ,[operation] AS [operation]
             ,[tool_entity] AS [tool_entity]
             ,[primary_entity] AS [primary_entity]
             ,[processing_end_date] AS [processing_end_date]
             ,[lot] AS [lot]
             ,[prodgroup3] AS [prodgroup3]
             ,[product] AS [product]
             ,[visual_id] AS [visual_id]
             ,[ws_loss_code] AS [ws_loss_code]
             ,[media_in_x] AS [media_in_x]
             ,[media_in_y] AS [media_in_y]
             ,[height] AS [height]
             ,[patch_lift_roi1] AS [patch_lift_roi1]
             ,[patch_lift_roi2] AS [patch_lift_roi2]
             ,[patch_lift_roi3] AS [patch_lift_roi3]
             ,[patch_lift_roi4] AS [patch_lift_roi4]
             ,[patch_lift_roi5] AS [patch_lift_roi5]
             ,[patch_lift_roi6] AS [patch_lift_roi6]
             ,[patch_lift_roi7] AS [patch_lift_roi7]
             ,[patch_lift_roi8] AS [patch_lift_roi8]
             ,[patch_lift_roi_max] AS [patch_lift_roi_max]
             ,[patch_sli] AS [patch_sli]
             ,[interposer_sli] AS [interposer_sli]
             ,[NCO_Risk] AS [NCO_Risk]
             ,[VIDCount] AS [VIDCount]
    FROM
    (
    T_L0_Result
    )
    ) t /*L1*/
    ) t /*L2*/
    WHERE
                  [FlagLot] = '1' 
    ;
    """,
        output="IPM_Data.csv",
        reader=SqliteReader(),
        inputs=["PARMI_IPM_RAW.csv"],
        header=[
            "lot_1",
            "newqty1",
            "facility",
            "operation",
            "tool_entity",
            "primary_entity",
            "processing_end_date",
            "lot",
            "prodgroup3",
            "product",
            "visual_id",
            "ws_loss_code",
            "media_in_x",
            "media_in_y",
            "height",
            "patch_lift_roi1",
            "patch_lift_roi2",
            "patch_lift_roi3",
            "patch_lift_roi4",
            "patch_lift_roi5",
            "patch_lift_roi6",
            "patch_lift_roi7",
            "patch_lift_roi8",
            "patch_lift_roi_max",
            "patch_sli",
            "interposer_sli",
            "NCO_Risk",
            "VIDCount",
            "FlagLot",
        ],
    )


def step_0047_sqlite_query(ctx) -> None:
    ctx.run_query(
        sql="""
    SELECT /*L0*/  DISTINCT 
              a0.[lot] AS [Lot_NCORisk]
    FROM 
    [IPM_Data] a0
    WHERE
     NOT          (a0.[lot] In 
    """
        + ctx.csv_io.sql_get_csv_list(".\\HIST.csv", 1, "a0.[lot] In")
        + """)"""
        + """
    """,
        output="DATA.csv",
        reader=SqliteReader(),
        inputs=["IPM_Data.csv"],
        header=["Lot_NCORisk"],
    )


def step_0048_rows_in_file(ctx) -> None:
    ctx.macro.set_named("SIGNAL", str(ctx.csv_io.row_count("data.csv")))


def step_0051_sqlite_query(ctx) -> None:
    ctx.run_query(
        sql="""
    SELECT DISTINCT 
     [Lot_NCORisk] AS [HOLD_LOT]
    ,'Y' AS [AUTO_CONTAIN]
    ,[lot] AS [LOT]
    ,'<<<dEmail>>>' AS [EMAIL]
    ,'<<<dCCB>>>' AS [CCB_NUMBER]
    ,'<<<dHNote>>>' AS [HOLD_NOTE]
    ,'<<<dHCat>>>' AS [HOLDCATEGORY]
    ,'IAM' AS [Module]
    FROM [T0] 
    WHERE 1=1


    """,
        output="RESULT_<<<%username%>>>_CWFNCO{TS}.csv",
        reader=SqliteReader(),
        inputs=["data.csv:T0"],
        header=[
            "HOLD_LOT",
            "AUTO_CONTAIN",
            "LOT",
            "EMAIL",
            "CCB_NUMBER",
            "HOLD_NOTE",
            "HOLDCATEGORY",
            "Module",
        ],
    )


def step_0052_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(src=str(Path(".") / "RESULT_*.csv"), dst=ctx.macro.named("CSRPATH"))


def step_0054_html_report(ctx) -> None:
    ctx.html_report.defer(
        instance="22697",
        id="MYREPORT3",
        prompt_text="Step 6-6. sending notification",
        app_server_default="atd_atm.hadoop",
        template="\n\n\nType<\\\\>Key<\\\\>COL1<\\\\>COL2<\\\\>COL3<\\\\>COL4<\\\\>COL5<\\\\>COL6<\\\\>COL7<\\\\>COL8<\\\\>COL9<\\\\>COL10<\\\\>COL11<\\\\>COL12<\\\\>COL13<\\\\>COL14<\\\\>COL15<\\\\>COL16<\\\\>COL17<\\\\>COL18<\\\\>COL19<\\\\>COL20<\\\\>COL21<\\\\>COL22<\\\\>COL23<\\\\>COL24<\\\\>COL25<\\\\>COL26\nTYPE<\\\\>HTML<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nINPUT-FILE<\\\\>IPM_Data.csv<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nOUTPUT-FILE<\\\\>SQLPathFinder.htm<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCSS<\\\\>sqlpathfinder_style_1.css<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLSPAN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDRILLDOWN<\\\\>N<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDYNAMICSORT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDYNAMICFILTER<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nATTOPDRILLDOWN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nNOPREPROCESS<\\\\>Y<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nAT-TOP-OF-REPORT<\\\\><\\\\>CWF_MLINCO_LOTHOLD<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-DATA<\\\\><\\\\>facility<\\\\>operation<\\\\>tool_entity<\\\\>primary_entity<\\\\>processing_end_date<\\\\>lot<\\\\>prodgroup3<\\\\>product<\\\\>visual_id<\\\\>ws_loss_code<\\\\>media_in_x<\\\\>media_in_y<\\\\>height<\\\\>patch_lift_roi1<\\\\>patch_lift_roi2<\\\\>patch_lift_roi3<\\\\>patch_lift_roi4<\\\\>patch_lift_roi5<\\\\>patch_lift_roi6<\\\\>patch_lift_roi7<\\\\>patch_lift_roi8<\\\\>patch_lift_roi_max<\\\\>lot_1<\\\\>patch_sli<\\\\>interposer_sli<\\\\>nco_risk\nCOLUMN-HEADERS<\\\\><\\\\>Facility<\\\\>Operation<\\\\>Tool Entity<\\\\>Primary Entity<\\\\>Processing End Date<\\\\>Lot<\\\\>Prodgroup3<\\\\>Product<\\\\>Visual Id<\\\\>Ws Loss Code<\\\\>Media In X<\\\\>Media In Y<\\\\>Height<\\\\>Patch Lift Roi1<\\\\>Patch Lift Roi2<\\\\>Patch Lift Roi3<\\\\>Patch Lift Roi4<\\\\>Patch Lift Roi5<\\\\>Patch Lift Roi6<\\\\>Patch Lift Roi7<\\\\>Patch Lift Roi8<\\\\>Patch Lift Roi Max<\\\\>Lot 1<\\\\>Patch Sli<\\\\>Interposer Sli<\\\\>Nco Risk\nCOLUMN-ALIGNMENT<\\\\><\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left\nCOLUMN-FORMAT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>",
    )


def step_0055_html_report(ctx) -> None:
    ctx.html_report.layout(
        ctx,
        outlook="N",
        instance="22697",
        json_only="N",
        chart_instance="30819",
        app_server_default="atd_atm.hadoop",
        template='<table class="tblout"><tr class="tblout"><td class="tblout" valign="top">\n:FILE:EMAIL:<<<dEmail>>>\n:CSS:sqlpathfinder_style_1.css\n:CSSEMBED:Y\n:RR:NO\n:B:Y\n:EM-A:\n:EM-S:\n:SEC:Y\n:TITLE:CWF_MLINCO_LOTHOLD\n<table class="tblout">\n<tr class="tblout">\n<td class="tblout">\n<p style="text-align: left" style="background-color: white"><font face="intel clear" size="3.66666666666667" color="black">Lot on hold due to suspected MLI NCO. Lot will need to go through 100% XRAY (2846)</font>\n</td>\n</tr>\n<tr class="tblout">\n<td class="tblout">\nHTM:MYREPORT3\n</td>\n</tr>\n<tr class="tblout">\n<td class="tblout">\n<p style="text-align: left" style="background-color: white"><font face="intel clear" size="3.66666666666667" color="black"><strong>Configurations applied</strong><br>CSR enabled: <<<DCSR>>><br>ICMPCS MMS Workflow enabled: <<<DMWF>>><br>MMS Signal Tracer enabled: <<<DMST>>></font>\n</td>\n</tr>\n<tr class="tblout">\n<td class="tblout">\nIHJ:revision.htm\n</td>\n</tr>\n</table>\n</td><td class="tblout" valign="top">\n<table class="tblout">\n<tr class="tblout"><td class="tblout"></td></tr>\n</table>\n</td></tr></table>',
    )


def step_0056_html_report(ctx) -> None:
    ctx.html_report.delete(instance="22697")


def step_0057_rows_in_file(ctx) -> None:
    ctx.macro.set_named("HISTERR", str(ctx.csv_io.row_count("HISTERROR.txt")))


def step_0059_smart_append(ctx) -> None:
    ctx.smart_append.append("HIST.csv", "data.csv")


def step_0060_fs_copy(ctx) -> None:
    ctx.fs_ops.rename(src="HIST.csv", dst="HIST.txt")


def step_0061_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(
        src=str(Path(".") / "HIST.txt"),
        dst=ctx.macro.named("IREPOP")
        + "\\"
        + ctx.macro.named("SFOLDER")
        + "\\KM\\HIST",
    )


def step_0064_rows_in_file(ctx) -> None:
    ctx.macro.set_named("SIGNAL", str(ctx.csv_io.row_count("data.csv")))


def step_0067_write_file(ctx) -> None:
    ctx.write_file(
        path="spfheaderfile.csv",
        template="DATE,EMAIL,MODULE,SIGNALTYPE,COMPONENT_ID,HOLD_LOT,AUTO_CONTAIN,SHOULD_EMAIL,MMS,ALARMTYPE,SUB_ENTITY_1,SUB_ENTITY_2,SUB_ENTITY_3,FACILITY,PRODGROUP3,ENTITY,DEFECT_SIDE,DEFECT_MODE,LOT_OWNR,IMPACT_LOT_VI,IMPACT_VIDS\n",
    )


def step_0068_sqlite_query(ctx) -> None:
    ctx.run_query(
        sql="""
    SELECT DISTINCT 
     'BA' AS [MODULE]
    ,'ICMPCS' AS [SIGNALTYPE]
    ,'IAM_CWF_NCO' AS [ALARMTYPE]
    ,'<<<dEmail>>>' AS [EMAIL]
    ,'Y' AS [HOLD_LOT]
    ,'N' AS [AUTO_CONTAIN]
    ,[prodgroup3] AS [PRODGROUP3]
    ,[lot] AS [IMPACT_LOT_VI]
    ,[visual_id] AS [IMPACT_VIDS]
                   ,'Y' AS [MMS]
                   ,'Y' AS [SHOULD_EMAIL]
                   ,CAST(DATETIME('now','localtime') AS TEXT)||'.000000' AS [DATE]
                   ,'' AS [FACILITY]
    FROM [T0]
    WHERE 1=1

    """,
        output="SPFMMSTMP.csv",
        reader=SqliteReader(),
        inputs=["IPM_Data.csv:T0"],
        header=[
            "MODULE",
            "SIGNALTYPE",
            "ALARMTYPE",
            "EMAIL",
            "HOLD_LOT",
            "AUTO_CONTAIN",
            "PRODGROUP3",
            "IMPACT_LOT_VI",
            "IMPACT_VIDS",
        ],
    )


def step_0069_smart_append(ctx) -> None:
    ctx.smart_append.append("spfheaderfile.csv", "SPFMMSTMP.csv")


def step_0070_fs_copy(ctx) -> None:
    ctx.fs_ops.rename(
        src="spfheaderfile.csv",
        dst="<TS>_" + ctx.macro.named("%USERNAME%") + "_output.pickle.csv",
    )


def step_0071_fs_delete(ctx) -> None:
    ctx.fs_ops.delete(paths=["SPFMMSTMP.csv"])


def step_0072_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(
        src=str(Path(".") / "*_output.pickle.csv"), dst=ctx.macro.named("MIPPATH")
    )


def step_0075_write_file(ctx) -> None:
    ctx.write_file(
        path="update.bat",
        template="\n@echo off\nset currd=%date:~10,4%-%date:~4,2%-%date:~7,2%\nset currms=%time:~2,6%\nset /a currh=%time:~0,2%\nif %currh% LSS 10 set currh=0%currh%\nset currt=%currh%%currms%\nset PriConfig=<<<IREPOP>>>\\<<<SFOLDER>>>\n\n\nREM ************** edit here only if needed **************\n\nset record=<<<DCSR>>>,<<<DMST>>>,<<<DMWF>>>\n\n\n\n\nREM *************** do not edit below here ***************\n\ncall :retry\ngoto:eof\n\n:retry\nset /a tries=300\n\n:loop\nif %tries% LEQ 0 goto return\necho <<<STARTTS>>>,%currd% %currt%,<<<UTC>>>,KM,<<<%username%>>>,<<<UNDERDEV>>>,%record%>>%PriConfig%\\VF_CE.txt && goto return || set /a tries-=1 && ping -n 1 127.0.0.1>nul 2>&1 && goto loop\n\n:return\n@exit /B",
    )


def step_0076_external(ctx) -> None:
    ctx.external.run(argv=["update.bat"])


def step_0077_fs_delete(ctx) -> None:
    ctx.fs_ops.delete(paths=["update.bat"])


# <vg2c:steps:end>


# <vg2c:workflow:start>
def run() -> None:
    OracleClient.configure()
    ctx = PipelineContext()
    step_0000_html_report(ctx)
    step_0001_html_report(ctx)
    step_0002_html_report(ctx)
    step_0003_write_file(ctx)
    step_0004_write_file(ctx)
    step_0005_external(ctx)
    with ctx.macro.scope(ctx.csv_io.single_row("macrotmp.csv")):
        step_0007_external(ctx)
        step_0008_write_file(ctx)
    step_0010_fs_delete(ctx)
    with ctx.macro.scope(ctx.csv_io.single_row("ctime.csv")):
        step_0012_rows_in_file(ctx)
        if int(ctx.macro.named("CONFIG")) <= int("0"):
            step_0014_email(ctx)
        else:
            step_0016_sqlite_query(ctx)
            step_0017_rows_in_file(ctx)
            if int(ctx.macro.named("CONFIGSETS")) != int("1"):
                step_0019_email(ctx)
            else:
                with ctx.macro.scope(ctx.csv_io.single_row("configsets.csv")):
                    if (
                        ctx.macro.named("CSRV") == "FAIL"
                        and ctx.macro.named("UNDERDEV") == "N"
                    ):
                        step_0023_write_file(ctx)
                        step_0024_email(ctx)
                    if (
                        ctx.macro.named("MMSV") == "FAIL"
                        and ctx.macro.named("UNDERDEV") == "N"
                    ):
                        step_0027_write_file(ctx)
                        step_0028_email(ctx)
                    step_0030_fs_copy(ctx)
                    step_0031_rows_in_file(ctx)
                    if int(ctx.macro.named("HIST")) <= int("0"):
                        step_0033_write_file(ctx)
                        step_0034_write_file(ctx)
                    else:
                        step_0036_fs_copy(ctx)
    with ctx.macro.scope(ctx.csv_io.single_row("configsets.csv")):
        step_0043_sql_query(ctx)
        step_0044_sql_query(ctx)
        step_0045_sqlite_query(ctx)
        step_0046_sqlite_query(ctx)
        step_0047_sqlite_query(ctx)
        step_0048_rows_in_file(ctx)
        if int(ctx.macro.named("SIGNAL")) > int("0"):
            if ctx.macro.named("DCSR") == "Y":
                step_0051_sqlite_query(ctx)
                # step_0052_fs_copy(ctx)
            step_0054_html_report(ctx)
            step_0055_html_report(ctx)
            step_0056_html_report(ctx)
            step_0057_rows_in_file(ctx)
            if int(ctx.macro.named("HISTERR")) <= int("0"):
                step_0059_smart_append(ctx)
                step_0060_fs_copy(ctx)
                step_0061_fs_copy(ctx)
        step_0064_rows_in_file(ctx)
        if int(ctx.macro.named("SIGNAL")) > int("0"):
            pass
            # if ctx.macro.named("DMWF") == "Y":
            #     step_0067_write_file(ctx)
            #     step_0068_sqlite_query(ctx)
            #     step_0069_smart_append(ctx)
            #     step_0070_fs_copy(ctx)
            #     step_0071_fs_delete(ctx)
            #     step_0072_fs_copy(ctx)
        step_0075_write_file(ctx)
        step_0076_external(ctx)
        step_0077_fs_delete(ctx)


# <vg2c:workflow:end>

if __name__ == "__main__":
    # run()
    OracleClient.configure()
    ctx = PipelineContext()
    step_0045_sqlite_query(ctx)
    print("A")
