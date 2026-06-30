# Auto-generated Python script from VG2
"""Pipeline implementation."""

from contextlib import contextmanager
from email.message import EmailMessage
from pathlib import Path
from typing import Any
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

"""apply_crosstab — pivot utility for DataFrames (embeddable)."""


from typing import Any

import pandas as pd



def apply_crosstab(
    rows: pd.DataFrame,  # pandas.DataFrame
    row_keys: list[str],
    header_key: str,
    value_key: str,
) -> Any:  # pandas.DataFrame
    """Pivot row-oriented data into SQLPathFinder-style crosstab output.

    Args:
        rows: pandas DataFrame.
        row_keys: Grouping columns (``/CTROW``).
        header_key: Dynamic column source (``/CTHEADER``).
        value_key: Dynamic value source (``/CTVALUE``).

    Returns:
        pandas DataFrame with pivoted data, including row_keys as columns.
    """

    if rows.empty or not row_keys or not header_key or not value_key:
        return pd.DataFrame(columns=row_keys)

    # Resolve requested keys against actual columns case-insensitively
    # (Oracle may uppercase names) and rename to the requested casing.
    ci_lookup = {str(c).casefold(): c for c in rows.columns}
    rename_map = {
        ci_lookup[k.casefold()]: k for k in (*row_keys, header_key, value_key)
    }
    df = rows.rename(columns=rename_map)

    df = df[df[header_key].notna() & (df[header_key].astype(str) != "")]
    if df.empty:
        return pd.DataFrame(columns=row_keys)

    # dropna=False so rows with NaN in any row_key are preserved
    # (groupby's default would silently drop them, yielding an empty result
    # when even one row_key column has NaN).
    result = (
        df.groupby([*row_keys, header_key], dropna=False)[value_key]
        .first()
        .unstack(header_key, fill_value="")
        .reset_index()
        .rename_axis(columns=None)
    )
    result.columns = [str(col).lower() for col in result.columns]
    return result


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


"""SqliteEngine — execute SQL joins over CSV inputs (embeddable)."""


import csv
import re
import sqlite3
from pathlib import Path
from typing import Callable


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

    def run_join(
        self, sql: str, inputs: list[str], output: str, header: list[str] | None = None
    ) -> None:
        """
        1. Open in-memory SQLite connection.
        2. Load each *input* CSV as a table.
        3. Split *sql* on ';'; execute non-SELECT statements directly.
        4. Execute the final SELECT; write rows to *output* CSV.
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
                f"SQLite error in run_join: {exc}\nSQL:\n{final_stmt}"
            ) from exc

        conn.close()

        output_path = Path(output)
        output_path.parent.mkdir(parents=True, exist_ok=True)

        output_header = header if header else col_names

        with output_path.open("w", newline="", encoding="utf-8") as fh:
            writer = csv.writer(fh)
            if output_header:
                writer.writerow(output_header)

            if header and col_names:
                col_index = {name: idx for idx, name in enumerate(col_names)}
                header_str = [str(h) for h in header]

                for row in rows:
                    projected = [
                        row[col_index[h]] if h in col_index else "" for h in header
                    ]
                    if [str(v) for v in projected] != header_str:
                        writer.writerow(projected)
            else:
                for row in rows:
                    writer.writerow(list(row))


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


def step_0000_step_1_1_create_an_html_report(ctx):
    pass  # HTML report not translated


def step_0001_html_report(ctx):
    pass  # HTML report not translated


def step_0002_html_report(ctx):
    pass  # HTML report not translated


def step_0003_step_1_2_create_macro_tmp_update_script_name_here(ctx):
    ctx.write_file(path='macrotmp.csv', template='\nSfolder,underDEV,useCSR,useMMS\nICMPCS_Maxlidheight_CSR_DXA,N,Y,Y')


def step_0004_step_1_4_create_getcsrsu_bat(ctx):
    ctx.write_file(path='getcsrsu.bat', template='\n@echo off\nset PriCSR="\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset SecCSR="\\\\KMATSHFS.intel.com\\KMATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset BakCSR="\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\%username%\\Patrol\\*.___"\ncopy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .\nren setsiteparam.___ setsiteparam.exe')


def step_0005_step_1_5_run_getcsrsu_bat(ctx):
    ctx.external.run(['getcsrsu.bat'])


def step_0007_step_1_7_run_setsiteparam_exe(ctx):
    ctx.external.run(['setsiteparam.exe', 'VN', ctx.macro.named("SFOLDER"), ctx.macro.named("UNDERDEV"), ctx.macro.named("USECSR"), ctx.macro.named("USEMMS")])


def step_0009_step_1_8_delete_temporary_files(ctx):
    ctx.fs_ops.delete(paths=['"macrotmp.csv', 'getcsrsu.bat', 'setsiteparam.exe', 'csrsu.txt"'])


def step_0011_rows_in_file(ctx):
    ctx.macro.set_named('CONFIG', str(ctx.csv_io.row_count('ICMPCS_config.csv')))


def step_0013_step_1_12_trigger_if_config_file_not_found(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0015_step_1_13_1_transpose_config_file_to_macro_friendly_format(ctx):
    ctx.sqlite_engine.run_join(
        sql="""
SELECT /*L10*/  DISTINCT 
          [icmpcs] AS [icmpcs]
         ,[parameter] AS [parameter]
         ,Max([value]) AS [value]
         ,[STARTTS] AS [STARTTS]
         ,[UTC] AS [UTC]
         ,[TZONE] AS [TZONE]
         ,[SFOLDER] AS [SFOLDER]
         ,[FAC] AS [FAC]
         ,[MARS] AS [MARS]
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
         ,'<<<SFOLDER>>>' AS [SFOLDER]
         ,'<<<FAC>>>' AS [FAC]
         ,'<<<MARS>>>' AS [MARS]
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
         ,[SFOLDER]
         ,[FAC]
         ,[MARS]
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
         ,[UNDERDEV]
         ,[CSRV]
         ,[MMSV]
""",
        inputs=['ICMPCS_config.csv'],
        output='configsets.csv',
        header=['icmpcs', 'STARTTS', 'UTC', 'TZONE', 'SFOLDER', 'FAC', 'MARS', 'RIMS', 'EIMS', 'ARIES', 'OASYS', 'MONGO', 'MMS', 'MMSI', 'TOOLLOG', 'VFMARS', 'VFARIES', 'VFMONGO', 'CSRPATH', 'MMSPATH', 'MIPPATH', 'UNDERDEV', 'CSRV', 'MMSV'],
    )


def step_0016_rows_in_file(ctx):
    ctx.macro.set_named('CONFIGSETS', str(ctx.csv_io.row_count('configsets.csv')))


def step_0018_step_1_16_trigger_if_converted_config_file_contains_not_equal_to_1_row(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0022_step_1_19_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx):
    ctx.write_file(path='CSRVerror.htm', template='\n<!DOCTYPE html>\n<html>\n<body>\n<p>It is detected that you cannot access to CSR depository path for <strong>VN</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>CSR Superuser</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong>\nPath: <<<CSRPATH>>></p>\n</body>\n</html>')


def step_0023_step_1_20_email_when_user_have_no_access_to_csr(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0026_step_1_22_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx):
    ctx.write_file(path='MMSVerror.htm', template='\n<!DOCTYPE html>\n<html\n<body>\n<p>It is detected that you cannot access to MMS Signal Tracer depository path for <strong>VN</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>MMS Signal Tracer Admin</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong><br/>\nPath: <<<MMSPATH>>></p>\n</body>\n</html>')


def step_0027_step_1_23_email_when_user_have_no_access_to_mms_signal_tracer(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0029_step_1_24_robocopy_hist_txt(ctx):
    ctx.fs_ops.copy(src='HIST.txt', dst='\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\' + ctx.macro.named("SFOLDER") + '\\VN\\HIST')


def step_0030_rows_in_file(ctx):
    ctx.macro.set_named('HIST', str(ctx.csv_io.row_count('HIST.txt')))


def step_0032_step_1_27_create_dummy_hist_csv(ctx):
    ctx.write_file(path='HIST.csv', template='\nLOT,OUT_DATE\nDUMMY,2000-01-01 00:00:00')


def step_0033_step_1_28_create_histerror_txt(ctx):
    ctx.write_file(path='HISTERROR.txt', template='\nERROR\nERROR\nERROR')


def step_0035_step_1_29_convert_hist_txt_to_hist_csv(ctx):
    pass  # TODO: unhandled utility shape=unknown


def step_0042_step_3_delete_files(ctx):
    ctx.fs_ops.delete(paths=['MAXLIDHEIGHT.CSV'])


def step_0043_step_4_1_1_a0_fetching_mars_data(ctx):
    result = ctx.read(sql="""
/*BEGIN SQL*/
SELECT  DISTINCT 
          f0.lot AS lot
FROM 
@[]@.F_LotHist f0
WHERE
NVL(f0.history_deleted_flag,'N') = 'N'
AND      f0.owner <> 'EMPTYFOUP'
 AND      f0.operation = '2090' 
 AND      f0.out_date >= (SYSDATE - 12/24) 
 AND NOT      (f0.lot In 
""" + ctx.sql_macros.sql_get_csv_list('.\\HIST.csv', 1, 'f0.lot In') + """) 
-- Tail A
/*END SQL*/

""", db_type='MARS')

    ctx.csv_io.write('yeuchuan_a0_9164.tab', result, header=['lot'])


def step_0044_step_4_1_1_a1_fetching_mars_data(ctx):
    result = ctx.read(sql="""
/*BEGIN SQL*/
SELECT  DISTINCT 
          lot_1 AS lot_1
         ,operation AS operation
         ,transaction AS transaction
         ,Transaction_Key AS Transaction_Key
FROM
(
SELECT  
          f0.lot AS lot_1
         ,f0.operation AS operation
         ,f5.transaction AS transaction
         ,f5.transaction AS Transaction_Key
FROM 
@[]@.F_LotHist f0
LEFT JOIN @[]@.F_LotTxnHist f5 ON f5.lot = f0.lot AND f5.operation = f0.operation AND f5.prevout_date = f0.prevout_date AND NVL(f5.history_deleted_flag,'N') = 'N'
WHERE
NVL(f0.history_deleted_flag,'N') = 'N'
AND      f0.owner <> 'EMPTYFOUP'
 AND      (f0.lot In 
""" + ctx.sql_macros.sql_get_csv_list('.\\yeuchuan_a0_9164.tab', 'lot', 'f0.lot In') + """) 
 AND      f0.operation In ('1266'
,'1366') 
-- Tail A
)
WHERE
              Transaction_Key = 'MVIN' 
/*END SQL*/

""", db_type='MARS')

    ctx.csv_io.write('yeuchuan_a1_9164.tab', result, header=['lot_1', 'operation', 'transaction', 'Transaction_Key'])


def step_0045_sqlite_query(ctx):
    ctx.sqlite_engine.run_join(
        sql="""

DROP INDEX IF EXISTS IdxA1;
Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_9164] ([lot_1]);

SELECT /*L1*/  DISTINCT 
          [lot] AS [lot]
FROM
(
SELECT /*L0*/  
          a0.[lot] AS [lot]
         ,a1.[lot_1] AS [lot_1]
         ,a1.[operation] AS [operation]
         ,a1.[transaction] AS [transaction]
         ,a1.[Transaction_Key] AS [Transaction_Key]
         ,CASE WHEN  [lot]= [lot_1]   THEN 'Y' ELSE 'N' END AS [Cure_Movein_Flag]
FROM 
           [yeuchuan_a0_9164] a0
 LEFT OUTER JOIN [yeuchuan_a1_9164] a1
  ON a0.[lot] = a1.[lot_1] 
) t /*L0*/
WHERE
              [Cure_Movein_Flag] = 'Y'
""",
        inputs=['yeuchuan_a0_9164.tab', 'yeuchuan_a1_9164.tab'],
        output='DLA_LOT.csv',
        header=['lot'],
    )


def step_0046_step_5_1_1_a0_fetching_aries_data(ctx):
    result = ctx.read(sql="""
/*BEGIN SQL*/
SELECT  DISTINCT 
          lot AS lot
         ,operation AS operation
         ,visual_id AS visual_id
         ,To_Char(test_end_date,'yyyy-mm-dd hh24:mi:ss') AS test_end_date
         ,tester_id AS tester_id
         ,test_name AS test_name
         ,Max(all_value) AS all_value
         ,prodgroup3 AS prodgroup3
FROM
(
SELECT  
          ats.lot AS lot
         ,ats.operation AS operation
         ,di.visual_id AS visual_id
         ,ats.test_end_date_time AS test_end_date
         ,ats.tester_id AS tester_id
         ,t.test_name AS test_name
         ,CASE WHEN ctr.string_value IS NULL THEN to_char(ctr.numeric_result) ELSE ctr.string_value END AS all_value
         ,mp.prodgroup3 AS prodgroup3
FROM 
A_Testing_Session ats
LEFT JOIN A_MARS_Lot ml ON ats.lot=ml.lot
LEFT JOIN A_MARS_Product mp ON ml.product = mp.product AND ml.mars_schema=mp.mars_schema AND ats.facility = mp.facility
INNER JOIN A_All_Component_Testing_Result ctr ON ctr.lao_start_ww = ats.lao_start_ww AND ctr.ts_id = ats.ts_id AND (ctr.numeric_result IS NOT NULL or ctr.string_value is NOT NULL)
INNER JOIN A_Test t ON t.t_id = ctr.t_id
INNER JOIN A_Device_Testing dt ON dt.lao_start_ww = ats.lao_start_ww AND dt.ts_id = ats.ts_id
AND dt.lao_start_ww = ctr.lao_start_ww AND dt.ts_id = ctr.ts_id AND dt.dt_id = ctr.dt_id
LEFT JOIN A_Device_Item di ON di.di_id = dt.di_id
WHERE ats.data_domain='METROLOGY'
 AND      ats.operation = '2090' 
 AND      CASE WHEN ctr.string_value IS NULL THEN to_char(ctr.numeric_result) ELSE ctr.string_value END = '4' 
 AND      t.test_name = 'BINCODE' 
 AND      (ats.lot In 
""" + ctx.sql_macros.sql_get_csv_list('.\\DLA_LOT.csv', 1, 'ats.lot In') + """) 
)
GROUP BY 
          lot
         ,operation
         ,visual_id
         ,test_end_date
         ,tester_id
         ,test_name
         ,prodgroup3
/*END SQL*/

""", db_type='ARIES')
    result = apply_crosstab(result, row_keys=['lot', 'operation', 'visual_id', 'test_end_date', 'tester_id', 'prodgroup3'], header_key='test_name', value_key='all_value')

    ctx.csv_io.write('BIN4.csv', result)


def step_0047_rows_in_file(ctx):
    ctx.macro.set_named('ROWSINFILE', str(ctx.csv_io.row_count('BIN4.csv')))


def step_0050_step_9_1_1_a0_fetching_aries_data(ctx):
    result = ctx.read(sql="""
/*BEGIN SQL*/
SELECT  DISTINCT 
          lot AS lot
         ,operation AS operation
         ,visual_id AS visual_id
         ,To_Char(test_end_date,'yyyy-mm-dd hh24:mi:ss') AS test_end_date
         ,tester_id AS tester_id
         ,test_name AS test_name
         ,Max(all_value) AS all_value
         ,prodgroup3 AS prodgroup3
FROM
(
SELECT /*+  ordered */ 
          ats.lot AS lot
         ,ats.operation AS operation
         ,di.visual_id AS visual_id
         ,ats.test_end_date_time AS test_end_date
         ,ats.tester_id AS tester_id
         ,t.test_name AS test_name
         ,CASE WHEN ctr.string_value IS NULL THEN to_char(ctr.numeric_result) ELSE ctr.string_value END AS all_value
         ,mp.prodgroup3 AS prodgroup3
FROM 
A_Device_Item di
INNER JOIN A_Device_Testing dt ON dt.di_id = di.di_id
INNER JOIN A_Testing_Session ats ON ats.lao_start_ww = dt.lao_start_ww AND ats.ts_id = dt.ts_id AND ats.data_domain='METROLOGY'
LEFT JOIN A_MARS_Lot ml ON ats.lot=ml.lot
LEFT JOIN A_MARS_Product mp ON ml.product = mp.product AND ml.mars_schema=mp.mars_schema AND ats.facility = mp.facility
INNER JOIN A_All_Component_Testing_Result ctr ON ctr.lao_start_ww = dt.lao_start_ww AND ctr.ts_id = dt.ts_id AND ctr.dt_id = dt.dt_id AND (ctr.numeric_result IS NOT NULL or ctr.string_value is NOT NULL)
AND ctr.lao_start_ww = ats.lao_start_ww AND ctr.ts_id = ats.ts_id
INNER JOIN A_Test t ON t.t_id = ctr.t_id
WHERE 1=1
 AND      ats.operation = '2090' 
 AND      t.test_name In ('MAXLIDHEIGHT'
,'MINLIDHEIGHT') 
 AND      (di.visual_id In 
""" + ctx.sql_macros.sql_get_csv_list('.\\BIN_VID_temp.CSV', 3, 'di.visual_id In') + """) 
)
GROUP BY 
          lot
         ,operation
         ,visual_id
         ,test_end_date
         ,tester_id
         ,test_name
         ,prodgroup3
/*END SQL*/

""", db_type='ARIES')
    result = apply_crosstab(result, row_keys=['lot', 'operation', 'visual_id', 'test_end_date', 'tester_id', 'prodgroup3'], header_key='test_name', value_key='all_value')

    ctx.csv_io.write('MAXLIDHEIGHT_temp.csv', result)


def step_0051_step_10_smart_append_data(ctx):
    pass  # TODO: unhandled utility shape=unknown


def step_0054_rows_in_file(ctx):
    ctx.macro.set_named('ROWSINFILE', str(ctx.csv_io.row_count('MAXLIDHEIGHT.CSV')))


def step_0056_step_13_1_1_fetching_text_sqlite_data(ctx):
    ctx.sqlite_engine.run_join(
        sql="""

DROP INDEX IF EXISTS IdxA1;
Create Index IF NOT EXISTS IdxA1 ON [MAXLIDHEIGHT] ([visual_id]);


DROP TABLE IF EXISTS T_L1_Init;
CREATE TABLE T_L1_Init AS
SELECT /*L1*/ 
          [prodgroup3] AS [prodgroup3]
         ,[operation] AS [operation]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[visual_id_1] AS [visual_id_1]
         ,[maxlidheight] AS [maxlidheight]
         ,[minlidheight] AS [minlidheight]
         ,[test_end_date] AS [test_end_date]
FROM
(
SELECT /*L0*/  
          a0.[prodgroup3] AS [prodgroup3]
         ,a0.[operation] AS [operation]
         ,a0.[tester_id] AS [tester_id]
         ,a0.[lot] AS [lot]
         ,a0.[visual_id] AS [visual_id]
         ,a1.[visual_id] AS [visual_id_1]
         ,CASE WHEN a1.[maxlidheight] = '' THEN NULL ELSE CAST (a1.[maxlidheight] AS REAL) END AS [maxlidheight]
         ,CASE WHEN a1.[minlidheight] = '' THEN NULL ELSE CAST (a1.[minlidheight] AS REAL) END AS [minlidheight]
         ,a0.[test_end_date] AS [test_end_date]
FROM 
           [BIN4] a0
 LEFT OUTER JOIN [MAXLIDHEIGHT] a1
  ON a0.[visual_id] = a1.[visual_id] 
) t /*L0*/
;

DROP TABLE IF EXISTS T_L1_1_1;
CREATE TABLE T_L1_1_1 AS
SELECT P75(maxlidheight) AS AF$S1
,lot AS AF$PB1
FROM T_L1_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L1_1_1_Idx ON T_L1_1_1 (AF$PB1);
DROP TABLE IF EXISTS T_L1_1_Result;
CREATE TABLE T_L1_1_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [P75]
FROM T_L1_Init a0 LEFT JOIN T_L1_1_1 a1 ON 
lot = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L1_1_1;

CREATE INDEX T_L1_1_Result_Idx ON T_L1_1_Result (orig_rowid);
DROP TABLE IF EXISTS T_L1_2_1;
CREATE TABLE T_L1_2_1 AS
SELECT P50(maxlidheight) AS AF$S1
,lot AS AF$PB1
FROM T_L1_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L1_2_1_Idx ON T_L1_2_1 (AF$PB1);
DROP TABLE IF EXISTS T_L1_2_Result;
CREATE TABLE T_L1_2_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [P50]
FROM T_L1_Init a0 LEFT JOIN T_L1_2_1 a1 ON 
lot = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L1_2_1;

CREATE INDEX T_L1_2_Result_Idx ON T_L1_2_Result (orig_rowid);
DROP TABLE IF EXISTS T_L1_3_1;
CREATE TABLE T_L1_3_1 AS
SELECT P75(minlidheight) AS AF$S1
,lot AS AF$PB1
FROM T_L1_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L1_3_1_Idx ON T_L1_3_1 (AF$PB1);
DROP TABLE IF EXISTS T_L1_3_Result;
CREATE TABLE T_L1_3_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [Min_P75]
FROM T_L1_Init a0 LEFT JOIN T_L1_3_1 a1 ON 
lot = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L1_3_1;

CREATE INDEX T_L1_3_Result_Idx ON T_L1_3_Result (orig_rowid);
DROP TABLE IF EXISTS T_L1_4_1;
CREATE TABLE T_L1_4_1 AS
SELECT P50(minlidheight) AS AF$S1
,lot AS AF$PB1
FROM T_L1_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L1_4_1_Idx ON T_L1_4_1 (AF$PB1);
DROP TABLE IF EXISTS T_L1_4_Result;
CREATE TABLE T_L1_4_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [Min_P50]
FROM T_L1_Init a0 LEFT JOIN T_L1_4_1 a1 ON 
lot = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L1_4_1;

CREATE INDEX T_L1_4_Result_Idx ON T_L1_4_Result (orig_rowid);
DROP TABLE IF EXISTS T_L1_Result;
CREATE TABLE T_L1_Result AS
SELECT

[prodgroup3]
,[operation]
,[tester_id]
,[lot]
,[visual_id]
,[visual_id_1]
,[maxlidheight]
,[P75]
,[P50]
,[minlidheight]
,[Min_P75]
,[Min_P50]
,[test_end_date]
FROM T_L1_Init a0
LEFT JOIN T_L1_1_Result a1 ON a0.rowid = a1.orig_rowid
LEFT JOIN T_L1_2_Result a2 ON a0.rowid = a2.orig_rowid
LEFT JOIN T_L1_3_Result a3 ON a0.rowid = a3.orig_rowid
LEFT JOIN T_L1_4_Result a4 ON a0.rowid = a4.orig_rowid
;
DROP TABLE IF EXISTS T_L1_1_Result;
DROP TABLE IF EXISTS T_L1_2_Result;
DROP TABLE IF EXISTS T_L1_3_Result;
DROP TABLE IF EXISTS T_L1_4_Result;
DROP TABLE IF EXISTS T_L1_Init;

SELECT /*L4*/  DISTINCT 
          [prodgroup3] AS [prodgroup3]
         ,[operation] AS [operation]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[maxlidheight] AS [maxlidheight]
         ,[outlier] AS [outlier]
         ,[minlidheight] AS [minlidheight]
         ,[Min_outlier] AS [Min_outlier]
         ,[test_end_date] AS [test_end_date]
FROM
(
SELECT /*L3*/ 
          [prodgroup3] AS [prodgroup3]
         ,[operation] AS [operation]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[visual_id_1] AS [visual_id_1]
         ,[maxlidheight] AS [maxlidheight]
         ,[P75] AS [P75]
         ,[P50] AS [P50]
         ,[outlier] AS [outlier]
         ,[minlidheight] AS [minlidheight]
         ,[Min_P75] AS [Min_P75]
         ,[Min_P50] AS [Min_P50]
         ,[Min_outlier] AS [Min_outlier]
         ,CASE WHEN   ( [maxlidheight] > [outlier] and  [minlidheight] >  [Min_outlier] )  THEN '0' ELSE '1' END AS [outlier_screen]
         ,[test_end_date] AS [test_end_date]
FROM
(
SELECT /*L2*/ 
          [prodgroup3] AS [prodgroup3]
         ,[operation] AS [operation]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[visual_id_1] AS [visual_id_1]
         ,[maxlidheight] AS [maxlidheight]
         ,[P75] AS [P75]
         ,[P50] AS [P50]
         ,[P50] +5.5*(( [P75] - [P50] )/0.6745) AS [outlier]
         ,[minlidheight] AS [minlidheight]
         ,[Min_P75] AS [Min_P75]
         ,[Min_P50] AS [Min_P50]
         ,[Min_P50] +5.5*((  [Min_P75] -  [Min_P50] )/0.6745) AS [Min_outlier]
         ,[test_end_date] AS [test_end_date]
FROM
(
T_L1_Result
)
) t /*L2*/
) t /*L3*/
WHERE
              [outlier_screen] = '0' 
;
""",
        inputs=['BIN4.CSV', 'MAXLIDHEIGHT.CSV'],
        output='MAXLIDHEIGHT_SCREEN.csv',
        header=['prodgroup3', 'operation', 'tester_id', 'lot', 'visual_id', 'maxlidheight', 'outlier', 'minlidheight', 'Min_outlier', 'test_end_date'],
    )


def step_0057_step_14_1_1_fetching_text_sqlite_data(ctx):
    ctx.sqlite_engine.run_join(
        sql="""

DROP TABLE IF EXISTS T_L1_Init;
CREATE TABLE T_L1_Init AS
SELECT /*L1*/ 
          [prodgroup3] AS [prodgroup3]
         ,[operation] AS [operation]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[maxlidheight] AS [maxlidheight]
         ,[test_end_date] AS [test_end_date]
         ,AVG ( [maxlidheight] ) OVER (PARTITION BY  [lot]  ) AS [MEAN]
         ,[outlier] AS [outlier]
FROM
(
SELECT /*L0*/  
          a0.[prodgroup3] AS [prodgroup3]
         ,a0.[operation] AS [operation]
         ,a0.[tester_id] AS [tester_id]
         ,a0.[lot] AS [lot]
         ,a0.[visual_id] AS [visual_id]
         ,CASE WHEN a0.[maxlidheight] = '' THEN NULL ELSE CAST (a0.[maxlidheight] AS REAL) END AS [maxlidheight]
         ,a0.[test_end_date] AS [test_end_date]
         ,a0.[outlier] AS [outlier]
FROM 
[MAXLIDHEIGHT_SCREEN] a0
) t /*L0*/
;

DROP TABLE IF EXISTS T_L1_1_1;
CREATE TABLE T_L1_1_1 AS
SELECT STDEV(maxlidheight) AS AF$S1
,lot AS AF$PB1
FROM T_L1_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L1_1_1_Idx ON T_L1_1_1 (AF$PB1);
DROP TABLE IF EXISTS T_L1_1_Result;
CREATE TABLE T_L1_1_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [STD]
FROM T_L1_Init a0 LEFT JOIN T_L1_1_1 a1 ON 
lot = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L1_1_1;

CREATE INDEX T_L1_1_Result_Idx ON T_L1_1_Result (orig_rowid);
DROP TABLE IF EXISTS T_L1_Result;
CREATE TABLE T_L1_Result AS
SELECT

[prodgroup3]
,[operation]
,[tester_id]
,[lot]
,[visual_id]
,[maxlidheight]
,[test_end_date]
,[MEAN]
,[STD]
,[outlier]
FROM T_L1_Init a0
LEFT JOIN T_L1_1_Result a1 ON a0.rowid = a1.orig_rowid
;
DROP TABLE IF EXISTS T_L1_1_Result;
DROP TABLE IF EXISTS T_L1_Init;

SELECT /*L4*/  DISTINCT 
          [prodgroup3] AS [prodgroup3]
         ,[operation] AS [operation]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[maxlidheight] AS [maxlidheight]
         ,[test_end_date] AS [test_end_date]
         ,[MEAN] AS [MEAN]
         ,[STD] AS [STD]
         ,[UCL] AS [UCL]
         ,[outlier] AS [outlier]
FROM
(
SELECT /*L3*/ 
          [prodgroup3] AS [prodgroup3]
         ,[operation] AS [operation]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[maxlidheight] AS [maxlidheight]
         ,[test_end_date] AS [test_end_date]
         ,[MEAN] AS [MEAN]
         ,[STD] AS [STD]
         ,[UCL] AS [UCL]
         ,CASE WHEN  [maxlidheight] > [UCL]  THEN '1' ELSE '0' END AS [FLAG]
         ,[outlier] AS [outlier]
FROM
(
SELECT /*L2*/ 
          [prodgroup3] AS [prodgroup3]
         ,[operation] AS [operation]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[maxlidheight] AS [maxlidheight]
         ,[test_end_date] AS [test_end_date]
         ,[MEAN] AS [MEAN]
         ,[STD] AS [STD]
         ,[MEAN] +4.5* [STD] AS [UCL]
         ,[outlier] AS [outlier]
FROM
(
T_L1_Result
)
) t /*L2*/
) t /*L3*/
WHERE
              [FLAG] = 1 
;
""",
        inputs=['MAXLIDHEIGHT_SCREEN.CSV'],
        output='MAXLIDHEIGHT_FLAG.csv',
        header=['prodgroup3', 'operation', 'tester_id', 'lot', 'visual_id', 'maxlidheight', 'test_end_date', 'MEAN', 'STD', 'UCL', 'outlier'],
    )


def run() -> None:
    ctx = PipelineContext()
    step_0000_step_1_1_create_an_html_report(ctx)
    step_0001_html_report(ctx)
    step_0002_html_report(ctx)
    step_0003_step_1_2_create_macro_tmp_update_script_name_here(ctx)
    step_0004_step_1_4_create_getcsrsu_bat(ctx)
    step_0005_step_1_5_run_getcsrsu_bat(ctx)
    for __row in ctx.csv_io.iter('macrotmp.csv'):
        with ctx.macro_scope(__row):
            step_0007_step_1_7_run_setsiteparam_exe(ctx)
    step_0009_step_1_8_delete_temporary_files(ctx)
    for __row in ctx.csv_io.iter('ctime.csv'):
        with ctx.macro_scope(__row):
            step_0011_rows_in_file(ctx)
            if int(ctx.macro.named("CONFIG")) <= int('0'):
                step_0013_step_1_12_trigger_if_config_file_not_found(ctx)
            else:
                step_0015_step_1_13_1_transpose_config_file_to_macro_friendly_format(ctx)
                step_0016_rows_in_file(ctx)
                if int(ctx.macro.named("CONFIGSETS")) != int('1'):
                    step_0018_step_1_16_trigger_if_converted_config_file_contains_not_equal_to_1_row(ctx)
                else:
                    for __row in ctx.csv_io.iter('configsets.csv'):
                        with ctx.macro_scope(__row):
                            if ctx.macro.named("CSRV") == 'FAIL' and ctx.macro.named("UNDERDEV") == 'N':
                                step_0022_step_1_19_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx)
                                step_0023_step_1_20_email_when_user_have_no_access_to_csr(ctx)
                            if ctx.macro.named("MMSV") == 'FAIL' and ctx.macro.named("UNDERDEV") == 'N':
                                step_0026_step_1_22_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx)
                                step_0027_step_1_23_email_when_user_have_no_access_to_mms_signal_tracer(ctx)
                            step_0029_step_1_24_robocopy_hist_txt(ctx)
                            step_0030_rows_in_file(ctx)
                            if int(ctx.macro.named("HIST")) <= int('0'):
                                step_0032_step_1_27_create_dummy_hist_csv(ctx)
                                step_0033_step_1_28_create_histerror_txt(ctx)
                            else:
                                step_0035_step_1_29_convert_hist_txt_to_hist_csv(ctx)
    for __row in ctx.csv_io.iter('configsets.csv'):
        with ctx.macro_scope(__row):
            step_0042_step_3_delete_files(ctx)
            step_0043_step_4_1_1_a0_fetching_mars_data(ctx)
            step_0044_step_4_1_1_a1_fetching_mars_data(ctx)
            step_0045_sqlite_query(ctx)
            step_0046_step_5_1_1_a0_fetching_aries_data(ctx)
            step_0047_rows_in_file(ctx)
            if int(ctx.macro.named("ROWSINFILE")) > int('0'):
                for __chunk_path in ctx.csv_io.iter_chunks('BIN4.csv', 'BIN_VID_temp.CSV', 5000):
                    step_0050_step_9_1_1_a0_fetching_aries_data(ctx)
                    step_0051_step_10_smart_append_data(ctx)
            step_0054_rows_in_file(ctx)
            if int(ctx.macro.named("ROWSINFILE")) > int('0'):
                step_0056_step_13_1_1_fetching_text_sqlite_data(ctx)
                step_0057_step_14_1_1_fetching_text_sqlite_data(ctx)

if __name__ == "__main__":
    run()