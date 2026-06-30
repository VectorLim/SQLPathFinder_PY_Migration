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

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)


def step_0000_step_1_1_create_an_html_report(ctx):
    pass  # HTML report not translated


def step_0001_html_report(ctx):
    pass  # HTML report not translated


def step_0002_html_report(ctx):
    pass  # HTML report not translated


def step_0003_step_1_2_create_macro_tmp_update_script_name_here(ctx):
    ctx.write_file(path='macrotmp.csv', template='\nSfolder,underDEV,useCSR,useMMS\nICMPCS_SUBPLANE_CSR_DLA,Y,Y,Y')


def step_0004_step_1_4_create_getcsrsu_bat(ctx):
    ctx.write_file(path='getcsrsu.bat', template='\n@echo off\nset PriCSR="\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset SecCSR="\\\\KMATSHFS.intel.com\\KMATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset BakCSR="\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\%username%\\Patrol\\*.___"\ncopy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .\nren setsiteparam.___ setsiteparam.exe')


def step_0005_step_1_5_run_getcsrsu_bat(ctx):
    ctx.external.run(['getcsrsu.bat'])


def step_0007_step_1_7_run_setsiteparam_exe(ctx):
    ctx.external.run(['setsiteparam.exe', 'KM', ctx.macro.named("SFOLDER"), ctx.macro.named("UNDERDEV"), ctx.macro.named("USECSR"), ctx.macro.named("USEMMS")])


def step_0009_step_1_8_delete_temporary_files(ctx):
    ctx.fs_ops.delete(paths=['"macrotmp.csv', 'getcsrsu.bat', 'setsiteparam.exe', 'csrsu.txt"'])


def step_0011_rows_in_file(ctx):
    ctx.macro.set_named('CONFIG', str(ctx.csv_io.row_count('ICMPCS_config.csv')))


def step_0013_step_1_12_trigger_if_config_file_not_found(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0016_rows_in_file(ctx):
    ctx.macro.set_named('CONFIGSETS', str(ctx.csv_io.row_count('configsets.csv')))


def step_0018_step_1_16_trigger_if_converted_config_file_contains_not_equal_to_1_row(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0022_step_1_19_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx):
    ctx.write_file(path='CSRVerror.htm', template='\n<!DOCTYPE html>\n<html>\n<body>\n<p>It is detected that you cannot access to CSR depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>CSR Superuser</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong>\nPath: <<<CSRPATH>>></p>\n</body>\n</html>')


def step_0023_step_1_20_email_when_user_have_no_access_to_csr(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0026_step_1_22_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx):
    ctx.write_file(path='MMSVerror.htm', template='\n<!DOCTYPE html>\n<html\n<body>\n<p>It is detected that you cannot access to MMS Signal Tracer depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>MMS Signal Tracer Admin</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong><br/>\nPath: <<<MMSPATH>>></p>\n</body>\n</html>')


def step_0027_step_1_23_email_when_user_have_no_access_to_mms_signal_tracer(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0029_step_1_24_robocopy_hist_txt(ctx):
    ctx.fs_ops.copy(src='HIST.txt', dst='\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\' + ctx.macro.named("SFOLDER") + '\\KM\\HIST')


def step_0030_rows_in_file(ctx):
    ctx.macro.set_named('HIST', str(ctx.csv_io.row_count('HIST.txt')))


def step_0032_step_1_27_create_dummy_hist_csv(ctx):
    ctx.write_file(path='HIST.csv', template='\nLOT,OUT_DATE\nDUMMY,2000-01-01 00:00:00')


def step_0033_step_1_28_create_histerror_txt(ctx):
    ctx.write_file(path='HISTERROR.txt', template='\nERROR\nERROR\nERROR')


def step_0035_step_1_29_convert_hist_txt_to_hist_csv(ctx):
    pass  # TODO: unhandled utility shape=unknown


def step_0042_step_4_1_copy_files_folders(ctx):
    ctx.fs_ops.copy(src='\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\ICMPCS_SUBPLANE_CSR_DLA\\Product_Lookup.csv', dst='.\\')


def step_0045_rows_in_file(ctx):
    ctx.macro.set_named('LOTS', str(ctx.csv_io.row_count('CSR_Server_OIS_subplane_lotlist.csv')))


def step_0052_rows_in_file(ctx):
    ctx.macro.set_named('FLAG', str(ctx.csv_io.row_count('CSR_Server_OIS_subplane_output.csv')))


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
            step_0042_step_4_1_copy_files_folders(ctx)
            step_0045_rows_in_file(ctx)
            if int(ctx.macro.named("LOTS")) > int('0'):
                step_0052_rows_in_file(ctx)
                if int(ctx.macro.named("FLAG")) > int('0'):

if __name__ == "__main__":
    run()