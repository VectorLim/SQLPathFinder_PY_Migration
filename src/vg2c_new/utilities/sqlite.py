from __future__ import annotations

import csv
import json
import re
import sqlite3
from pathlib import Path
from typing import TYPE_CHECKING, Any

import pandas as pd

from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility
from vg2c_new.utilities.csv import CsvUtility

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState
_TABLE_BINDING_RE = re.compile(r"^(?P<path>.+\.[^\\/:]+):(?P<table>[A-Za-z_][A-Za-z0-9_]*)$")
_CSV_LIST_RE = re.compile(r"\bSQL_Get_CSV_List\s*\(", re.I)
_RESERVED = {"rowid", "_rowid_"}


class SqliteQueryUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended selective port of current ScriptHost MemTable/nqSQLite behavior.
        Source: SPSQL3_py/SPFLib/SPFUtilities/memtable.py :: LoadFromFile/getStandaloneCon/Run_SQLite/SQL_Get_CSV_List; SPFSQL3.py :: nqSQLiteTask.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: CSV loading, aliases, reserved columns, text/null values, portable UDFs, SQL_Get_CSV_List and headers.
        Amendments: per-operation sqlite3 connection and visible SQL errors. Discarded: MemTable singleton, sqlite.exe, .NET/schema.ini.
        """
        con = sqlite3.connect(":memory:")
        try:
            self._register_udfs(con)
            for path, table in self._table_specs(command, state):
                self._load_file(con, path, table)
            cursor = self._execute(
                con, self._expand_csv_lists(state.substitute(command.body), command, state)
            )
            if cursor.description:
                frame = pd.DataFrame(cursor.fetchall(), columns=[d[0] for d in cursor.description])
                output = command.option("CSV")
                if output:
                    CsvUtility.write_dataframe(
                        frame, _path(command, state, state.substitute(output))
                    )
        finally:
            con.close()

    def _table_specs(self, command: Command, state: RuntimeState) -> list[tuple[Path, str]]:
        values = [state.substitute(v) for name, v in command.options if name == "TABLE"]
        specs = []
        for value in values:
            for item in [x.strip() for x in value.split(",") if x.strip()]:
                m = _TABLE_BINDING_RE.match(item)
                raw = m.group("path") if m else item
                path = _path(command, state, raw)
                specs.append((path, m.group("table") if m else path.stem))
        return specs

    def _load_file(self, con: sqlite3.Connection, path: Path, table: str) -> int:
        """Amended port of MemTable.LoadFromFile without singleton/class state."""
        if not path.exists():
            raise FileNotFoundError(path)
        with path.open("r", encoding="utf-8-sig", errors="replace", newline="") as handle:
            reader = csv.reader(
                handle, delimiter=CsvUtility.delimiter(path), skipinitialspace=False
            )
            header = next(reader, None)
            if header is None:
                return 0
            header = [str(x).replace("[", "(").replace("]", ")").replace("\n", " ") for x in header]
            if len({h.casefold() for h in header}) != len(header):
                raise ValueError(f"Duplicate column header in {path}.")
            bad = [h for h in header if h.casefold() in _RESERVED]
            if bad:
                raise ValueError(f"Reserved SQLite column name(s) in {path}: {', '.join(bad)}")
            if any(not h for h in header):
                raise ValueError(f"Empty column header in {path}.")
            qtable = _quote_ident(table)
            con.execute(f"DROP TABLE IF EXISTS {qtable}")
            con.execute(
                f"CREATE TABLE {qtable} ({','.join(f'{_quote_ident(h)} TEXT' for h in header)})"
            )
            rows = []
            for row in reader:
                if not row or all(not str(v).strip() for v in row):
                    continue
                if row == header:
                    continue
                if len(row) > len(header):
                    raise ValueError(
                        f"Column count mismatch in {path}: expected {len(header)}, got {len(row)}"
                    )
                row = row + [""] * (len(header) - len(row))
                rows.append([None if v == "" else v for v in row])
            if rows:
                con.executemany(
                    f"INSERT INTO {qtable} VALUES ({','.join('?' for _ in header)})", rows
                )
            return len(rows)

    def _execute(self, con: sqlite3.Connection, sql: str) -> sqlite3.Cursor:
        """Amended port of MemTable.Run_SQLite; multi-statement SQL errors intentionally propagate."""
        cursor = con.cursor()
        statements = []
        buffer = []
        for char in sql:
            buffer.append(char)
            candidate = "".join(buffer)
            if char == ";" and sqlite3.complete_statement(candidate):
                statement = candidate.strip().rstrip(";").strip()
                if statement:
                    statements.append(statement)
                buffer = []
        tail = "".join(buffer).strip().rstrip(";").strip()
        if tail:
            statements.append(tail)
        for statement in statements:
            cursor.execute(statement)
        return cursor

    def _expand_csv_lists(self, sql: str, command: Command, state: RuntimeState) -> str:
        cursor = 0
        out = []
        while True:
            m = _CSV_LIST_RE.search(sql, cursor)
            if m is None:
                out.append(sql[cursor:])
                break
            out.append(sql[cursor : m.start()])
            open_idx = sql.find("(", m.start())
            close = _matching_paren(sql, open_idx)
            if close < 0:
                raise ValueError("Unclosed SQL_Get_CSV_List call.")
            args = _split_args(sql[open_idx + 1 : close])
            if len(args) != 3:
                raise ValueError("SQL_Get_CSV_List requires csv path, column and lead-in.")
            path = _path(command, state, _strip_quotes(args[0]))
            col = _strip_quotes(args[1])
            ref: int | str = int(col) if col.isdigit() else col
            out.append(CsvUtility.sql_get_csv_list(path, ref, _strip_quotes(args[2])))
            cursor = close + 1
        return "".join(out)

    @staticmethod
    def _register_udfs(con: sqlite3.Connection) -> None:
        con.create_function("JsonValue", 1, _json_value)
        con.create_function("JsonKeyValPair", 2, _json_key_value)
        con.create_function("SPFRegexReplace", 5, _regex_replace)
        con.create_function("SPFRegexSearch", 3, _regex_search)
        con.create_function("SPFWriteLOBToFile", 2, _write_lob)
        con.create_function(
            "SPFPrepLikeValue", 1, lambda v: str(v or "").replace("*", "%").replace("?", "_")
        )


class SqliteLoadUtility(SqliteQueryUtility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost SQLiteLoadTask -> MemTable.Run_SQLite.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT. Preserved: file/table bindings, persistent DB, optional setup SQL.
        Amendments: stdlib sqlite3 with visible errors. Discarded: preprocessing executable/external sqlite.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("SQLite-Load requires file/table list and database path.")
        db = _path(command, state, args[1])
        db.parent.mkdir(parents=True, exist_ok=True)
        con = sqlite3.connect(db)
        try:
            self._register_udfs(con)
            for item in [x.strip() for x in args[0].split(",") if x.strip()]:
                m = _TABLE_BINDING_RE.match(item)
                raw = m.group("path") if m else item
                path = _path(command, state, raw)
                self._load_file(con, path, m.group("table") if m else path.stem)
            if len(args) > 2 and args[2].strip():
                self._execute(con, args[2])
            con.commit()
        finally:
            con.close()


class SqliteDeleteUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        args = [state.substitute(v) for v in command.arguments]
        if not args:
            raise ValueError("SQLiteDelete requires a database file.")
        _path(command, state, args[0]).unlink(missing_ok=True)


def _quote_ident(value: str) -> str:
    return '"' + value.replace('"', '""') + '"'


def _json_value(value: Any) -> Any:
    try:
        parsed = json.loads(str(value))
        return (
            json.dumps(parsed, separators=(",", ":"))
            if isinstance(parsed, (dict, list))
            else parsed
        )
    except Exception:
        return None


def _json_key_value(value: Any, key: Any) -> Any:
    try:
        parsed = json.loads(str(value))
        return parsed.get(str(key)) if isinstance(parsed, dict) else None
    except Exception:
        return None


def _regex_replace(value: Any, pattern: Any, replacement: Any, flags: Any, count: Any) -> str:
    return re.sub(
        str(pattern),
        str(replacement),
        str(value or ""),
        count=int(count or 0),
        flags=re.I if "I" in str(flags).upper() else 0,
    )


def _regex_search(value: Any, pattern: Any, flags: Any) -> int:
    return (
        1
        if re.search(str(pattern), str(value or ""), flags=re.I if "I" in str(flags).upper() else 0)
        else 0
    )


def _write_lob(value: Any, path: Any) -> str:
    target = Path(str(path))
    target.parent.mkdir(parents=True, exist_ok=True)
    target.write_bytes(value) if isinstance(value, bytes) else target.write_text(
        str(value or ""), encoding="utf-8"
    )
    return str(target)


def _matching_paren(text: str, open_idx: int) -> int:
    depth = 0
    quote = None
    for i in range(open_idx, len(text)):
        ch = text[i]
        if quote:
            if ch == quote and (i == 0 or text[i - 1] != "\\"):
                quote = None
            continue
        if ch in "'\"":
            quote = ch
        elif ch == "(":
            depth += 1
        elif ch == ")":
            depth -= 1
            if depth == 0:
                return i
    return -1


def _split_args(text: str) -> list[str]:
    return next(csv.reader([text], skipinitialspace=True))


def _strip_quotes(value: str) -> str:
    text = value.strip()
    return text[1:-1] if len(text) >= 2 and text[0] == text[-1] and text[0] in "'\"" else text


def _path(command: Command, state: RuntimeState, value: str) -> Path:
    return resolve_path(value, state, base=working_directory_for(command.option("WORKDIR"), state))
