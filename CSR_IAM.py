# %%
from datetime import datetime, timedelta

# from utils.send_msg import send_msg
from datasyncx import AriesReader
from datasyncx import email_on_exception
from datasyncx import set_log
import pandas as pd
import numpy as np
import shutil
import os
import sys
from pathlib import Path
from collections.abc import Callable
from collections.abc import Iterator
from contextlib import contextmanager
from datasyncx import AriesReader
from datasyncx import MarsReader
from pathlib import Path
from typing import Any
import csv
import os
import pandas
import pandas as pd
import re
import sqlite3
import sys

sys.path.insert(0, str(Path(__file__).parent.parent))


NODE = "KM"
SITE = ""
OPERATION = "2303"
SKIP_OPERATION = "2446"
DURATION = "TRUNC(SYSDATE) - 2"
HIST_PATH = ""
CONFIG_PATH = ""
EMAIL_RECEIVER = ""
EMAIL_SUBJECT = ""
monitorset = "'V_PRE_NDLE_OFFSTS_SING'"  # setup monset
ATTR_LIST = "1064"


# -------- CONFIG ---------------
def configure():
    global NODE, HIST_PATH, CONFIG_PATH, EMAIL_RECEIVER, EMAIL_SUBJECT, SITE

    args = sys.argv[1:]
    SITE = args[0]
    source_path = args[1]
    sys.argv = [sys.argv[0]]

    node_mapping = {
        "CD": "A48_PROD_21",
        "PG": "A12_PROD_0",
        "KM": "A15_PROD_21",
        "VN": "A90_PROD_21",
        "CR": "A61_PROD_4",
    }
    NODE = node_mapping.get(SITE)
    hist_dir = os.path.join(SITE, "HIST", "HIST.csv")
    HIST_PATH = os.path.join(source_path, hist_dir)

    # read config.txt
    # CONFIG_PATH = os.path.join(source_path, site, "config.txt")

    with open(CONFIG_PATH, "r") as f:
        columns = next(f).strip().split(",")
        lines = f.readlines()
    data = []
    for line in lines:
        parts = line.split(",", 3)
        parts += [""] * (4 - len(parts))
        data.append(
            {
                columns[0]: parts[0].strip(),
                columns[1]: parts[1].strip(),
                columns[2]: parts[2].strip(),
                columns[3]: parts[3].strip(),
            }
        )
    config_df = pd.DataFrame(data, columns=columns)

    # what to do with this
    EMAIL_RECEIVER = config_df.loc[config_df[columns[1]] == "dEmail", columns[2]].iloc[
        0
    ]
    EMAIL_SUBJECT = config_df.loc[config_df[columns[1]] == "dSubject", columns[2]].iloc[
        0
    ]
    return data, columns


# main

_TABLE_BINDING_RE = re.compile(
    "^(?P<path>.+\\.[^\\\\/:]+):(?P<table>[A-Za-z_][A-Za-z0-9_]*)$"
)


def resolve_path(name: "str | Path", *, for_write: "bool" = False) -> "Path":
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


def normalize_macro_name(raw: "str") -> "str":
    name = raw.strip()
    if name.startswith("<<<") and name.endswith(">>>"):
        name = name[3:-3]
    return name.strip().upper()


class CrosstabUtility:
    utility_name = "crosstab"
    TOKEN = "CrossTab->[["
    TOKEN_RE = re.compile(
        "(?P<prefix>,?)\\s*CrossTab->\\[\\[\\s*(?P<alias>[A-Za-z_][A-Za-z0-9_]*)\\s*,\\s*(?P<instance>[^;\\]]+)\\s*;\\s*:(?P<mode>[YyNn])\\s*\\]\\](?P<suffix>,?)"
    )

    @classmethod
    def has_token(cls, value: "str | None") -> "bool":
        return bool(value and cls.TOKEN in value)

    @staticmethod
    def _extract_selected_columns_by_alias(sql: "str") -> "dict[str, set[str]]":
        by_alias: "dict[str, set[str]]" = {}
        match = re.search(
            "\\bSELECT\\b(?P<select_part>.*?)\\bFROM\\b",
            sql,
            flags=re.IGNORECASE | re.DOTALL,
        )
        if not match:
            return by_alias
        select_part = match.group("select_part")
        col_ref_re = re.compile(
            '\\b([A-Za-z_][A-Za-z0-9_]*)\\s*\\.\\s*(?:\\[([^\\]]+)\\]|"([^"]+)"|([A-Za-z_][A-Za-z0-9_]*))'
        )
        for col_match in col_ref_re.finditer(select_part):
            alias = col_match.group(1).lower()
            col_name = col_match.group(2) or col_match.group(3) or col_match.group(4)
            if not col_name:
                continue
            by_alias.setdefault(alias, set()).add(col_name.lower())
        return by_alias

    @classmethod
    def substitute_sql(
        cls,
        sql: "str",
        alias_columns_lookup: "Callable[[str], list[str]] | None" = None,
    ) -> "str":
        if alias_columns_lookup is None or not cls.has_token(sql):
            return sql
        selected_by_alias = cls._extract_selected_columns_by_alias(sql)

        def _replace(match: "re.Match[str]") -> "str":
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
                (f"{alias}.[{c}] AS [{c}]" for c in dynamic_cols)
            )
            return f"{prefix}{body}{suffix}"

        return cls.TOKEN_RE.sub(_replace, sql)

    def apply(
        self,
        rows: "pd.DataFrame",
        row_keys: "list[str]",
        header_key: "str",
        value_key: "str",
    ) -> "Any":
        """Pivot row-oriented data into SQLPathFinder-style crosstab output."""
        if rows.empty or not row_keys or (not header_key) or (not value_key):
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
    utility_name = "csv_io"
    _CALL_RE = re.compile("\\bSQL_Get_CSV_List\\s*\\(", re.IGNORECASE)
    _CALL_SITE_WRAP_RE = re.compile(
        "\\(\\s*[A-Za-z_][\\w.\\[\\]@]*\\s+In\\s*$", re.IGNORECASE
    )

    def iter(self, name: "str") -> "Iterator[dict[str, str]]":
        """Yield each data row as a dict keyed by header names."""
        path = resolve_path(name)
        with path.open(newline="", encoding="utf-8", errors="replace") as fh:
            reader = csv.DictReader(fh)
            yield from reader

    def single_row(self, name: "str") -> "dict[str, str]":
        """Return exactly one data row from *name*; raise on 0 or >1 rows."""
        rows = self.iter(name)
        first = next(rows, None)
        if first is None:
            raise ValueError(f"CSV '{name}' must contain exactly 1 data row; found 0")
        second = next(rows, None)
        if second is not None:
            raise ValueError(f"CSV '{name}' must contain exactly 1 data row; found >1")
        return first

    def _read_column(self, path: "str", column_ref: "int | str") -> "list[str]":
        """Read a column from a CSV file."""
        rows: "list[str]" = []
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
            seen: "dict[str, None]" = {}
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
    def _single_quote(value: "str") -> "str":
        return "'" + value.replace("'", "''") + "'"

    def sql_get_csv_list(
        self, path: "str", column_ref: "int | str", lead_in: "str"
    ) -> "str":
        """Return chunked IN-list clause for Oracle-style SQL.

        Oracle hard-limits IN lists to 1000 values. When there are more, the
        result is chunked: ``(v1..v1000) OR <lead_in> (v1001..)``.
        """
        values = self._read_column(path, column_ref)
        if not values:
            return "('__NO_VALUES__')"
        chunk_size = 1000
        chunks = [values[i : i + chunk_size] for i in range(0, len(values), chunk_size)]
        parts: "list[str]" = []
        for i, chunk in enumerate(chunks):
            quoted = ", ".join((self._single_quote(v) for v in chunk))
            parts.append(f"({quoted})")
            if i < len(chunks) - 1:
                parts.append(f"\nOR {lead_in} ")
        return "".join(parts)

    def write(
        self, name: "str", content: "Any", header: "list[str] | None" = None
    ) -> "None":
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
                columns = {str(column).casefold(): column for column in content.columns}
                content = content.reindex(
                    columns=[
                        columns.get(column.casefold(), column) for column in header
                    ]
                )
                content.columns = header
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


class MacroState:
    utility_name = "macro"
    PLACEHOLDER_RE = re.compile("<<<([^>]+)>>>|<<>>")
    NAMED_PLACEHOLDER_RE = re.compile("<<<([^>]+)>>>")
    _MACRO_CONTROL_TOKEN_RE = re.compile(
        "^\\s*\\{(START-MACRO|END-MACRO|IF-THEN|ELSE|END-IF|RUN-LOOP|END-LOOP)\\}",
        re.IGNORECASE,
    )

    def __init__(self) -> "None":
        self._stack: "list[dict[str, str]]" = [{}]

    def named(self, name: "str") -> "str":
        key = name.upper()
        for frame in reversed(self._stack):
            if key in frame:
                return frame[key]
        return ""

    def positional(self) -> "str":
        frame = self._stack[-1]
        cursor = frame.get("__cursor__", 0)
        pos_list: "list[str]" = frame.get("__positional__", [])
        if isinstance(pos_list, list) and cursor < len(pos_list):
            frame["__cursor__"] = cursor + 1
            return pos_list[cursor]
        return ""

    def substitute(self, text: "str", vars: "dict[str, str] | None" = None) -> "str":
        if not text:
            return ""

        def _lookup(name: "str") -> "str":
            key = normalize_macro_name(name)
            if vars is not None:
                return vars.get(key, "")
            return self.named(key)

        def _replace(match: "re.Match[str]") -> "str":
            named = match.group(1)
            if named is not None:
                return _lookup(named)
            return self.positional()

        content = self.PLACEHOLDER_RE.sub(_replace, text)
        return content.lstrip("\n")

    def push_frame(self, named: "dict[str, str] | None" = None) -> "None":
        frame: "dict[str, str]" = {}
        for k, v in (named or {}).items():
            if k is None:
                continue
            frame[k.upper()] = str(v)
        self._stack.append(frame)

    def pop_frame(self) -> "None":
        if len(self._stack) > 1:
            self._stack.pop()

    @contextmanager
    def scope(self, row: "dict[str, str] | None" = None) -> "Iterator[None]":
        self.push_frame(named=row)
        try:
            yield
        finally:
            self.pop_frame()


class OracleClient:
    utility_name = "oracle_client"
    _reported_client = False
    _selected_instant_client: "Path | None" = None

    @classmethod
    def configure(cls) -> "str | None":
        """Prepare the current process for the configured DataSyncX Oracle client.

        Set ``DATASYNCX_ORACLE_CLIENT=instant`` to opt in. The normal
        ORACLE_HOME-based setup remains untouched when it is unset or ``home``.
        """
        mode = os.getenv("DATASYNCX_ORACLE_CLIENT", "home").strip().lower()
        if mode in {"", "home"}:
            return None
        if mode != "instant":
            raise RuntimeError(
                f"DATASYNCX_ORACLE_CLIENT must be 'home' or 'instant', not {mode!r}."
            )
        if sys.platform != "win32":
            raise RuntimeError(
                "DataSyncX 1.1.6 initializes python-oracledb without lib_dir. On Linux, configure Instant Client with ldconfig (preferred) or LD_LIBRARY_PATH before starting Python; on macOS, update DataSyncX to pass lib_dir before using this selector."
            )
        client_dir = cls._find_instant_client()
        network_dir = cls._configure_network_files(client_dir)
        if network_dir is not None:
            import oracledb

            oracledb.defaults.config_dir = str(network_dir)
        os.environ.pop("ORACLE_HOME", None)
        cls._prepend_path(client_dir)
        cls._selected_instant_client = client_dir
        return str(client_dir)

    @classmethod
    def log_active_client(cls) -> "None":
        """Print the initialized Oracle client once for terminal diagnostics."""
        if cls._reported_client:
            return
        import oracledb

        if oracledb.is_thin_mode():
            return
        try:
            version = ".".join((str(part) for part in oracledb.clientversion()))
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
    def _find_instant_client() -> "Path":
        configured = os.getenv("DATASYNCX_INSTANT_CLIENT_DIR") or os.getenv(
            "ORACLE_INSTANT_CLIENT_DIR"
        )
        separator = ";" if sys.platform == "win32" else os.pathsep
        candidates = (
            [configured] if configured else os.getenv("PATH", "").split(separator)
        )
        for candidate in candidates:
            if not candidate:
                continue
            path = Path(candidate).expanduser()
            if "instantclient" in path.name.lower() and (path / "oci.dll").is_file():
                return path.resolve()
        raise RuntimeError(
            "Oracle Instant Client was requested but no usable directory was found. Set DATASYNCX_INSTANT_CLIENT_DIR to the directory containing oci.dll."
        )

    @staticmethod
    def _configure_network_files(client_dir: "Path") -> "Path | None":
        configured = os.getenv("DATASYNCX_ORACLE_NET_CONFIG_DIR")
        network_dir = (
            Path(configured).expanduser()
            if configured
            else client_dir / "network" / "admin"
        )
        if configured and (not network_dir.is_dir()):
            raise RuntimeError(
                f"DATASYNCX_ORACLE_NET_CONFIG_DIR does not exist or is not a directory: {network_dir}"
            )
        if network_dir.is_dir():
            network_dir = network_dir.resolve()
            os.environ["TNS_ADMIN"] = str(network_dir)
            return network_dir
        return None

    @staticmethod
    def _prepend_path(client_dir: "Path") -> "None":
        separator = ";" if sys.platform == "win32" else os.pathsep
        entries = [entry for entry in os.getenv("PATH", "").split(separator) if entry]
        selected = str(client_dir)
        os.environ["PATH"] = separator.join(
            [selected, *(entry for entry in entries if Path(entry) != client_dir)]
        )


class PipelineContext:
    utility_name = "ctx"

    def __init__(self, utilities: "dict[str, object]") -> "None":
        self.__dict__.update(utilities)

    def _read_datasyncx(self, sql: "str", reader: "Any", node: "str"):
        try:
            result = reader.read(site=node, query=sql)
        finally:
            OracleClient.log_active_client()
        result.columns = [col.lower() for col in result.columns]
        return result

    def run_query(
        self,
        sql: "str",
        output: "str",
        reader: "Any",
        inputs: "list[str] | None" = None,
        header: "list[str] | None" = None,
        crosstab: "dict | None" = None,
        node: "str | None" = None,
    ):
        sql = self.macro.substitute(sql)
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


class SqliteReader:
    utility_name = "sqlite_reader"
    STMT_SPLIT_RE = re.compile(
        "(?:'[^']*'|\\\"[^\\\"]*\\\"|\\[[^\\]]*\\]|`[^`]*`|[^;])+", re.DOTALL
    )

    @staticmethod
    def _load_csv_as_table(
        conn: "sqlite3.Connection", csv_path: "str", table_name: "str | None" = None
    ) -> "str":
        path = Path(csv_path)
        table_name = table_name or path.stem
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
        col_defs = ", ".join((f'"{c}" TEXT' for c in cols))
        conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
        conn.execute(f'CREATE TABLE "{table_name}" ({col_defs})')
        header_str = [str(c) for c in cols]
        filtered_rows = [
            row for row in rows if [str(row.get(c, "")) for c in cols] != header_str
        ]
        if filtered_rows:
            placeholders = ", ".join(("?" for _ in cols))
            conn.executemany(
                f'INSERT INTO "{table_name}" VALUES ({placeholders})',
                [[row.get(c, "") for c in cols] for row in filtered_rows],
            )
        return table_name

    @classmethod
    def _split_statements(cls, sql: "str") -> "list[str]":
        return [
            match.group(0).strip()
            for match in cls.STMT_SPLIT_RE.finditer(sql)
            if match.group(0).strip()
        ]

    def execute(
        self, sql: "str", inputs: "list[str | tuple[str, str]]"
    ) -> "pd.DataFrame":
        conn = sqlite3.connect(":memory:")
        conn.row_factory = sqlite3.Row
        for input_spec in inputs:
            if isinstance(input_spec, tuple):
                csv_path, table_name = input_spec
            else:
                csv_path, table_name = (input_spec, None)
            self._load_csv_as_table(conn, csv_path, table_name)
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
        alias_to_table: "dict[str, str]" = {}
        alias_map_re = re.compile(
            '\\b(?:FROM|JOIN)\\s+(?:\\[([^\\]]+)\\]|\\"([^\\"]+)\\"|([A-Za-z_][A-Za-z0-9_]*))\\s+([A-Za-z_][A-Za-z0-9_]*)\\b',
            re.IGNORECASE,
        )
        for match in alias_map_re.finditer(final_stmt):
            table_name = match.group(1) or match.group(2) or match.group(3)
            alias = match.group(4)
            if table_name and alias:
                alias_to_table[alias.lower()] = table_name

        def _lookup_alias_columns(alias: "str") -> "list[str]":
            table_name = alias_to_table.get(alias.lower())
            if not table_name:
                return []
            pragma_rows = conn.execute(f'PRAGMA table_info("{table_name}")').fetchall()
            return [str(row[1]) for row in pragma_rows if len(row) > 1]

        final_stmt = CrosstabUtility.substitute_sql(
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
        if not rows or not col_names:
            return pd.DataFrame()
        data = [{col_names[i]: row[i] for i in range(len(col_names))} for row in rows]
        return pd.DataFrame(data)


# <vg2c:dependencies:end>
# <vg2c:steps:start>
def step_0001_sql_query(ctx) -> None:
    ctx.run_query(
        sql=f"""
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
     AND      f0.operation = {OPERATION}
     AND      f0.out_date >= {DURATION}
     AND      p.prodgroup3 Like 'CWF%' 
    -- Tail A
    )
    WHERE
                  Interposer_SLI Is Not Null  
    /*END SQL*/

    """,
        output="yeuchuan_a1_17362.tab",
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
        node=NODE,
    )


def step_0002_sql_query(ctx) -> None:
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
            ".\\yeuchuan_a1_17362.tab", "lot_1", "bams0.lot In"
        )
        + """)"""
        + f""" 
     AND      bams0.operation = {OPERATION}
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
        output="yeuchuan_a0_17362.tab",
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
        node=NODE,
    )


def step_0003_sqlite_query(ctx) -> None:
    ctx.run_query(
        sql="""

    DROP INDEX IF EXISTS IdxA0;
    Create Index IF NOT EXISTS IdxA0 ON [yeuchuan_a0_17362] ([lot],[operation]);

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
             ,CrossTab->[[a0,17362;:Y]]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a1.[Interposer_SLI],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [Interposer_SLI]
             ,Replace(Replace(Replace(Replace(Replace(Replace(a1.[Patch_SLI],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [Patch_SLI]
             ,a1.[prodgroup3_1] AS [prodgroup3_1]
             ,a1.[entity] AS [entity]
             ,a1.[transaction] AS [transaction]
    FROM 
               [yeuchuan_a1_17362] a1
     LEFT OUTER JOIN [yeuchuan_a0_17362] a0
      ON a1.[lot_1] = a0.[lot] 
     AND a1.[operation_1] = a0.[operation]
    """,
        output="PARMI_IPM_RAW.csv",
        reader=SqliteReader(),
        inputs=["yeuchuan_a1_17362.tab", "yeuchuan_a0_17362.tab"],
    )


def step_0004_sqlite_query(ctx) -> None:
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


def step_0005_sqlite_query(ctx) -> None:
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


def read_csv(path):
    try:
        if os.path.exists(path):
            hist_df = pd.read_csv(path)
            logger.info(f"Successfully read records from {path}")
            return hist_df
        else:
            return pd.DataFrame(columns=["LOT"])
    except Exception as e:
        logger.info(f"Failed to read history from {path}")
        raise e


def get_facility_lot(ctx):
    try:
        step_0001_sql_query(ctx)
        step_0002_sql_query(ctx)
        step_0003_sqlite_query(ctx)
        step_0004_sqlite_query(ctx)
        step_0005_sqlite_query(ctx)

        # compare skip_lot to hist.csv. Exclude lot that is already in hist.csv
        hist_df = read_csv(".\\HIST.csv")
        skip_lot = read_csv(".\\SKIP_LOT.csv")

        data_df = skip_lot[~skip_lot["LOT"].isin(hist_df["LOT"])]

        return data_df

    except Exception as e:
        logger.error(f"ERROR in get_facility_lot: {e}")
        raise e


@email_on_exception()
def process_facility_attribute_update(ctx):
    global ATTR_LIST, SITE, HIST_PATH
    lots_df = get_facility_lot(ctx)

    if lots_df is None or lots_df.empty:
        logger.info("No lots to process after filtering. Exiting function.")
        return
    from aed_updater import update_lot_attributes

    result = []
    try:
        total_lots = len(lots_df) - 1
        for index, row in lots_df.iterrows():
            facility = row["FACILITY"]
            lot = row["LOT"]
            # skip_value = row['OPERATION_DVI']
            skip_value = {SKIP_OPERATION}
            skip = update_lot_attributes(facility, lot, ATTR_LIST, skip_value, logger)
        result = lots_df
        # facility, lot, skip_operation=2446, attr_list=1064, skip_value=Y/N (TBC)
    except Exception as e:
        logger.error(f"ERROR in process_facility_attribute_updates: {e}")
        raise e
    finally:
        result_df = pd.DataFrame(result)
        # result_y_df = result_df.loc[result_df['Lot_Skip_7666'] == 'Y']
        # save data to csv
        current_date = datetime.now().strftime("%Y%m%d%H%M%S")
        result_name = f"{SITE}_Lot_Skip.csv".replace(".csv", f"_{current_date}.csv")
        # result_y_path = os.path.join(os.getcwd(), result_name)
        result_path = os.path.join(os.getcwd(), result_name)
        result_df.to_csv(result_path, index=False)

        # save history record
        if os.path.exists(HIST_PATH):
            hist_df = pd.read_csv(HIST_PATH)
            hist_df = pd.concat([hist_df, result_df])
            # hist_df['OUT_DATE'] = pd.to_datetime(hist_df['OUT_DATE'])
            # hist_df = hist_df.loc[hist_df['OUT_DATE']
            #                       >= datetime.now() - timedelta(days=180)]
            # hist_df.sort_values('OUT_DATE', ascending=False, inplace=True)
            # hist_df.drop_duplicates(
            #     subset=['LOT', 'Lot_Skip_7666'], keep='first', inplace=True)
            hist_df.to_csv(HIST_PATH, index=False)
        else:
            shutil.copy(result_path, HIST_PATH)
        logger.info(f"File: {result_name} saved to history path: {HIST_PATH}")


if __name__ == "__main__":
    OracleClient.configure()
    ctx = PipelineContext(
        {"crosstab": CrosstabUtility(), "csv_io": CsvIO(), "macro": MacroState()}
    )

    file_path = Path(__file__).parent.parent / "logs" / f"EPX_CSAM_{SITE}.log"
    logger = set_log(file_path)
    process_facility_attribute_update(ctx)
