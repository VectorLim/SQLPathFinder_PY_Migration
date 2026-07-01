# Auto-generated Python script from VG2
"""Pipeline implementation."""

from contextlib import contextmanager
from email.message import EmailMessage
from pathlib import Path
from typing import Any, ContextManager
from typing import Any, Iterator
from typing import Callable
from typing import Callable, Iterator, Protocol
import csv
import os
import pandas
import pandas as pd
import re
import shutil
import smtplib
import sqlite3
import subprocess

"""CsvIO — lightweight CSV reader/writer over stdlib csv."""


import csv
from pathlib import Path
from typing import Any, Iterator

import pandas



class CsvIO:
    """Read and write CSV files relative to ``cwd``."""

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
        - a list of dicts  → written via DictWriter (keys as header)
        - a list of lists  → written via writer (optional *header* for first row)
        - a string         → written as raw text (no CSV encoding)
        - a Path           → copied verbatim
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


"""MailService — send email via stdlib smtplib."""


import os
import smtplib
from email.message import EmailMessage
from pathlib import Path



class MailService:
    """Send email. Reads connection config from environment variables."""

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


"""ExternalProcess — run system commands via subprocess."""


import subprocess
from pathlib import Path



class ExternalProcess:
    """Thin wrapper around subprocess.run."""

    def run(
        self,
        argv: list[str],
        cwd: str | Path | None = None,
        env: dict | None = None,
        check: bool = False,
    ) -> int:
        result = subprocess.run(
            argv,
            cwd=str(cwd) if cwd else None,
            env=env,
            check=check,
        )
        return result.returncode


"""FileSystemOps — copy / rename / delete via pathlib + shutil."""


import shutil
from pathlib import Path



class FileSystemOps:

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


"""MacroState — runtime macro variable storage and substitution (embeddable)."""


import re
from contextlib import contextmanager
from pathlib import Path
from typing import Callable, Iterator, Protocol



# ---------------------------------------------------------------------------
# Placeholder patterns
# ---------------------------------------------------------------------------

PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")
CROSSTAB_RE = re.compile(
    r"(?:,CrossTab->\[\[\s*([A-Za-z_][A-Za-z0-9_]*)\s*,\s*([^;\]]+)\s*;\s*:([YyNn])\s*\]\]|CrossTab->\[\[\s*([A-Za-z_][A-Za-z0-9_]*)\s*,\s*([^;\]]+)\s*;\s*:([YyNn])\s*\]\],)"
)


def _extract_selected_columns_by_alias(sql: str) -> dict[str, set[str]]:
    """Return selected ``alias.column`` refs from the first SELECT list in *sql*."""
    by_alias: dict[str, set[str]] = {}
    match = re.search(
        r"\bSELECT\b(?P<select_part>.*?)\bFROM\b", sql, flags=re.IGNORECASE | re.DOTALL
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


def normalize_macro_name(raw: str) -> str:
    """Return the canonical macro name for *raw* (strips ``<<< >>>``, uppercases)."""
    name = raw.strip()
    if name.startswith("<<<") and name.endswith(">>>"):
        name = name[3:-3]
    return name.strip().upper()


def substitute_crosstab(
    sql: str, alias_columns_lookup: Callable[[str], list[str]] | None = None
) -> str:
    """Expand SQLPathFinder ``CrossTab->[[alias,instance;:Y/N]]`` tokens."""
    if alias_columns_lookup is None or "CrossTab->[[" not in sql:
        return sql

    selected_by_alias = _extract_selected_columns_by_alias(sql)

    def _replace(match: re.Match[str]) -> str:
        alias = match.group(1)
        mode = match.group(3).upper()
        all_cols = alias_columns_lookup(alias)
        selected = selected_by_alias.get(alias.lower(), set())
        dynamic_cols = [c for c in all_cols if c.lower() not in selected]

        if not dynamic_cols:
            return ""

        if mode == "N":
            return ",".join(dynamic_cols)

        return "\n         ,".join(f"{alias}.[{c}] AS [{c}]" for c in dynamic_cols)

    return CROSSTAB_RE.sub(_replace, sql)


class MacroLookup(Protocol):
    """Minimal interface for macro substitution."""

    def named(self, name: str) -> str: ...

    def positional(self) -> str: ...


class MacroState:
    """Stack of variable frames; lookups walk top-to-bottom (most-recent wins)."""

    def __init__(self) -> None:
        self._stack: list[dict[str, str]] = [{}]

    def named(self, name: str) -> str:
        """Return the value of a named variable, "" if not set."""
        key = name.upper()
        for frame in reversed(self._stack):
            if key in frame:
                return frame[key]
        return ""

    def set_named(self, name: str, value: str) -> None:
        """Write *value* into the current (top) frame."""
        self._stack[-1][name.upper()] = value

    def positional(self) -> str:
        """Return the next positional variable from the top frame (auto-advances)."""
        frame = self._stack[-1]
        cursor = frame.get("__cursor__", 0)
        pos_list: list[str] = frame.get("__positional__", [])  # type: ignore[assignment]
        if isinstance(pos_list, list) and cursor < len(pos_list):
            frame["__cursor__"] = cursor + 1
            return pos_list[cursor]
        return ""

    def substitute_sql(
        self,
        sql: str,
        crosstab_alias_columns: Callable[[str], list[str]] | None = None,
    ) -> str:
        """Substitute ``<<<NAME>>>`` placeholders in *sql* using current state."""
        if "<<<" in sql:
            sql = NAMED_PLACEHOLDER_RE.sub(
                lambda m: self.named(normalize_macro_name(m.group(1))),
                sql,
            )
        return substitute_crosstab(sql, alias_columns_lookup=crosstab_alias_columns)

    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        """Write *template* to *path*, substituting ``<<<NAME>>>`` / ``<<>>`` placeholders."""

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

        content = PLACEHOLDER_RE.sub(_replace, template)
        content = content.lstrip("\n")

        out = Path(path)
        out.parent.mkdir(parents=True, exist_ok=True)
        out.write_text(content, encoding="utf-8")

    def eval_condition(self, lhs: str, op: str, rhs: str) -> bool:
        """Legacy condition evaluation kept for backward compatibility."""
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
        """Context manager that pushes a new frame (optionally pre-populated with *row*)."""
        self.push_frame(named=row)
        try:
            yield
        finally:
            self.pop_frame()


"""SqliteEngine — execute SQL joins over CSV inputs (embeddable)."""


import csv
import re
import sqlite3
from pathlib import Path
from typing import Callable

import pandas as pd


# --- Embedded dependencies from macro subsystem ---

CROSSTAB_RE = re.compile(
    r"(?:,CrossTab->\[\[\s*([A-Za-z_][A-Za-z0-9_]*)\s*,\s*([^;\]]+)\s*;\s*:([YyNn])\s*\]\])",
    re.IGNORECASE,
)


def _extract_selected_columns_by_alias(sql: str) -> dict[str, set[str]]:
    """Return selected ``alias.column`` refs from the first SELECT list in *sql*."""
    by_alias: dict[str, set[str]] = {}
    match = re.search(
        r"\bSELECT\b(?P<select_part>.*?)\bFROM\b", sql, flags=re.IGNORECASE | re.DOTALL
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


def _substitute_crosstab(
    sql: str, alias_columns_lookup: Callable[[str], list[str]] | None = None
) -> str:
    """Expand SQLPathFinder ``CrossTab->[[alias,instance;:Y/N]]`` tokens."""
    if alias_columns_lookup is None or "CrossTab->[[" not in sql:
        return sql

    selected_by_alias = _extract_selected_columns_by_alias(sql)

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

    return CROSSTAB_RE.sub(_replace, sql)


# --- SqliteEngine implementation ---


def _load_csv_as_table(conn: sqlite3.Connection, csv_path: str) -> str:
    """Load a CSV file into a SQLite table; return the table name (file stem)."""
    path = Path(csv_path)
    stem = path.stem
    table_name = stem

    try:
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
                r for r in rows if [str(r.get(c, "")) for c in cols] != header_str
            ]

            if filtered_rows:
                placeholders = ", ".join("?" for _ in cols)
                conn.executemany(
                    f'INSERT INTO "{table_name}" VALUES ({placeholders})',
                    [[r.get(c, "") for c in cols] for r in filtered_rows],
                )
            return table_name
    except Exception as exc:
        raise RuntimeError(f"Failed to load CSV {csv_path}: {exc}") from exc


_STMT_SPLIT_RE = re.compile(
    r"(?:'[^']*'|\"[^\"]*\"|\[[^\]]*\]|`[^`]*`|[^;])+",
    re.DOTALL,
)


def _split_statements(sql: str) -> list[str]:
    return [
        m.group(0).strip() for m in _STMT_SPLIT_RE.finditer(sql) if m.group(0).strip()
    ]


class SqliteEngine:
    """Run SQL joins over CSV files using an in-memory SQLite connection."""

    def execute(self, sql: str, inputs: list[str]) -> pd.DataFrame:
        """
        Execute SQL query over CSV inputs and return result as DataFrame.

        Args:
            sql: SQL query (may contain multiple statements separated by ';')
            inputs: List of CSV file paths to load as tables

        Returns:
            pandas DataFrame with query results
        """
        conn = sqlite3.connect(":memory:")
        conn.row_factory = sqlite3.Row

        for csv_path in inputs:
            _load_csv_as_table(conn, csv_path)

        stmts = _split_statements(sql)
        if not stmts:
            conn.close()
            return

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
        for m in alias_map_re.finditer(final_stmt):
            table_name = m.group(1) or m.group(2) or m.group(3)
            alias = m.group(4)
            if table_name and alias:
                alias_to_table[alias.lower()] = table_name

        def _lookup_alias_columns(alias: str) -> list[str]:
            table_name = alias_to_table.get(alias.lower())
            if not table_name:
                return []
            pragma_rows = conn.execute(f'PRAGMA table_info("{table_name}")').fetchall()
            return [str(r[1]) for r in pragma_rows if len(r) > 1]

        final_stmt = _substitute_crosstab(
            final_stmt, alias_columns_lookup=_lookup_alias_columns
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

        # Convert to DataFrame
        if not rows or not col_names:
            return pd.DataFrame()

        # Convert sqlite3.Row objects to list of dicts
        data = [{col_names[i]: row[i] for i in range(len(col_names))} for row in rows]
        return pd.DataFrame(data)


"""SqlMacros — SQL macro expansion helpers (embeddable)."""


import csv
from pathlib import Path



def _read_column(path: str, column_ref: int | str) -> list[str]:
    """Extract unique values from a CSV column (1-based index or header name)."""
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


def _single_quote(value: str) -> str:
    return "'" + value.replace("'", "''") + "'"


class SqlMacros:
    """SQL macro expansion helpers used by emitted scripts."""

    def sql_get_csv_list(self, path: str, column_ref: int | str, lead_in: str) -> str:
        """Return chunked IN-list clause for Oracle-style SQL.

        Oracle hard-limits IN lists to 1000 values. When there are more, the
        result is chunked: ``(v1..v1000) OR <lead_in> (v1001..)``.
        """
        values = _read_column(path, column_ref)
        if not values:
            return "('__NO_VALUES__')"

        chunk_size = 1000
        chunks = [values[i : i + chunk_size] for i in range(0, len(values), chunk_size)]
        parts: list[str] = []
        for i, chunk in enumerate(chunks):
            quoted = ", ".join(_single_quote(v) for v in chunk)
            parts.append(f"({quoted})")
            if i < len(chunks) - 1:
                parts.append(f"\nOR {lead_in} ")

        return "".join(parts)


"""PipelineContext — runtime context for generated scripts (embeddable)."""


from typing import Any, ContextManager



class PipelineContext:
    """Single runtime context object for generated scripts."""

    def __init__(self) -> None:
        self.macro = MacroState()
        self.csv_io = CsvIO()
        self.sqlite_engine = SqliteEngine()
        self.sql_macros = SqlMacros()
        self.fs_ops = FileSystemOps()
        self.mail = MailService()
        self.external = ExternalProcess()

    def macro_scope(self, row: dict[str, str] | None = None) -> ContextManager[None]:
        """Delegate macro scoping to MacroState."""
        return self.macro.scope(row=row)

    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        self.macro.write_file(path, template, vars=vars)

    def read(self, sql: str, db_type: str):
        """Run SQL through reader runtime using current macro scope."""
        return read(sql=sql, db_type=db_type, macro_state=self.macro)

    def run_query(
        self,
        sql: str,
        output: str,
        source_type: str,
        inputs: list[str] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
    ):
        """
        Unified query execution method for both SQLite and external databases.

        Args:
            sql: SQL query string (may contain macro placeholders)
            output: Output CSV path
            source_type: 'sqlite' | 'MARS' | 'ARIES' | 'OASYS'
            inputs: List of input CSV paths (required for sqlite)
            header: Optional declared header for output CSV
            crosstab: Optional crosstab config dict with keys:
                     'row_keys', 'header_key', 'value_key'
        """
        # 1. Substitute macros
        sql = self.macro.substitute_sql(sql)

        # 2. Execute query based on source_type
        if source_type.lower() == "sqlite":
            result = self.sqlite_engine.execute(sql, inputs or [])
        else:
            # Pass pre-substituted SQL; read() will skip re-substitution
            result = read(sql=sql, db_type=source_type, macro_state=None)

        # 3. Apply crosstab if configured
        if crosstab:
            result = apply_crosstab(
                result,
                row_keys=crosstab["row_keys"],
                header_key=crosstab["header_key"],
                value_key=crosstab["value_key"],
            )

        # 4. Write output
        self.csv_io.write(output, result, header=header)

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)


"""Reader runtime injected into emitted pipeline scripts.

The runtime is registered with :func:`register_utility` so emitter embedding
uses the same registry-driven flow as other utilities.

The runtime relies on ``macro_state.substitute_sql(sql)`` (provided by
:class:`vg2c.emitter.macro.MacroState`) so SQL placeholder substitution
stays owned by the macro subsystem.

To support a new database type, add an entry to ``DATABASE_TYPE_MAP`` below
and add the matching ``datasyncx`` Reader import alongside the others below.
"""




from datasyncx.readers import AriesReader, MarsReader, OracleReader

# DATABASE_TYPE_MAP is the single extension point for adding a new database
# type: map the /ENGINE= identifier used in the VG2 source to a datasyncx
# Reader subclass. ``read`` below dispatches to it.
DATABASE_TYPE_MAP = {
    "MARS": MarsReader,
    "OASYS": OracleReader,
    "ARIES": AriesReader,
}


def read(sql, db_type, macro_state=None):
    """Run *sql* against the Reader registered for *db_type*.

    ``macro_state`` (when given) substitutes ``<<<NAME>>>`` macro
    placeholders that survive into the SQL body via its own
    ``substitute_sql`` helper.
    """
    if macro_state is not None:
        sql = macro_state.substitute_sql(sql)
    if db_type not in DATABASE_TYPE_MAP:
        raise ValueError(f"Unsupported database type: {db_type!r}")
    result = DATABASE_TYPE_MAP[db_type]().read(site="KM", query=sql)
    result.columns = [col.lower() for col in result.columns]

    return result


def step_0000_step_1_1_a0_fetching_mars_data(ctx):
    ctx.run_query(
        sql="""
/*BEGIN SQL*/
SELECT 
          p.prodgroup3 AS prodgroup3
         ,f0.operation AS operation
         ,f0.lot AS lot
         ,To_Char(f0.prevout_date,'yyyy-mm-dd hh24:mi:ss') AS prevout_date
FROM 
@[]@.F_LotHist f0
LEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
WHERE
NVL(f0.history_deleted_flag,'N') = 'N'
AND      f0.owner <> 'EMPTYFOUP'
 AND      p.prodgroup3 = 'BDR' 
 AND      f0.operation = '1225' 
 AND      f0.out_date >= TRUNC(SYSDATE) - 120 
-- Tail A
/*END SQL*/

""",
        output='yeuchuan_a0_29397.tab',
        source_type='MARS', header=['prodgroup3', 'operation', 'lot', 'prevout_date'],
    )


def step_0001_step_1_1_a1_fetching_oasys_data(ctx):
    ctx.run_query(
        sql="""
/*BEGIN SQL*/
SELECT 
          v1.lot AS spc_lot
         ,v3.monitor_set_name AS monitor_set_name
FROM 
     SCHEMA.P_SPC_Batch_Lot v1
    ,SCHEMA.P_SPC_Batch v2
    ,SCHEMA.P_SPC_Session v3
WHERE 
              v2.batch_id = v1.batch_id
 AND      v2.facility = v1.facility
 AND      v2.batch_id = v3.batch_id
 AND      v2.facility = v3.facility
 AND      v2.data_collection_ww = v3.data_collection_ww
 AND      v3.latest_flag = 'Y' 
 AND      v3.status <> 'I' 
 AND      (v1.lot In 
""" + ctx.sql_macros.sql_get_csv_list('.\\yeuchuan_a0_29397.tab', 'lot', 'v1.lot In') + """) 
 AND      v1.operation = '1225' 
 AND      v3.monitor_set_name Like 'V_PRE_NDLE_OFFSTS_SING' 
/*END SQL*/

""",
        output='yeuchuan_a1_29397.tab',
        source_type='OASYS', header=['spc_lot', 'monitor_set_name'],
    )


def step_0002_sqlite_query(ctx):
    ctx.run_query(
        sql="""

DROP INDEX IF EXISTS IdxA1;
Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_29397] ([spc_lot]);

SELECT /*L0*/  DISTINCT 
          a0.[prodgroup3] AS [prodgroup3]
         ,a0.[operation] AS [operation]
         ,a0.[lot] AS [lot]
         ,a1.[spc_lot] AS [spc_lot]
         ,a1.[monitor_set_name] AS [monitor_set_name]
         ,a0.[prevout_date] AS [prevout_date]
FROM 
           [yeuchuan_a0_29397] a0
 LEFT OUTER JOIN [yeuchuan_a1_29397] a1
  ON a0.[lot] = a1.[spc_lot]""",
        output='out_26585.tab',
        source_type='sqlite',
        inputs=['yeuchuan_a0_29397.tab', 'yeuchuan_a1_29397.tab'], header=['prodgroup3', 'operation', 'lot', 'spc_lot', 'monitor_set_name', 'prevout_date'],
    )


def run() -> None:
    ctx = PipelineContext()
    step_0000_step_1_1_a0_fetching_mars_data(ctx)
    step_0001_step_1_1_a1_fetching_oasys_data(ctx)
    step_0002_sqlite_query(ctx)

if __name__ == "__main__":
    run()