# SQL statements containing filters:
# - step_0000_sql_query (Line 651): filters on f0.history_deleted_flag, f0.operation, f0.out_date, f0.owner, f4.history_deleted_flag, f4.unique_flag, f5.history_deleted_flag, f5.transaction, la.attribute_number, p.latest_version
# - step_0001_sql_query (Line 654): filters on bams0.lot, bams0.operation, yeuchuan_a1_17362.tab
# - step_0003_sqlite_query (Line 698): filters on FlagLot, a0.rowid, lot_1

# Auto-generated Python script from VG2
"""Pipeline implementation."""

from collections.abc import Callable
from collections.abc import Iterator
from collections.abc import Mapping
from collections.abc import Sequence
from contextlib import contextmanager
from datasyncx import AriesReader
from datasyncx import MarsReader
from pathlib import Path
from typing import Any
from typing import ClassVar
import csv
import logging
import os
import pandas
import pandas as pd
import re
import sqlite3
import sys

ATTRIBUTE_NUMBER = 5005
ROWNUM_MAX = 1
ATTRIBUTE_NUMBER_2 = 5001
UNIQUE_FLAG = 'Y'
TRANSACTION = 'MVOU'
OWNER = 'EMPTYFOUP'
OPERATION = '2303'
FLAGLOT = '1'

class Logger:
    CRITICAL: 'ClassVar[int]' = logging.CRITICAL
    ERROR: 'ClassVar[int]' = logging.ERROR
    WARNING: 'ClassVar[int]' = logging.WARNING
    INFO: 'ClassVar[int]' = logging.INFO
    DEBUG: 'ClassVar[int]' = logging.DEBUG
    NOTSET: 'ClassVar[int]' = logging.NOTSET
    _logger_class_configured: 'ClassVar[bool]' = False

    class PrettyLogger(logging.Logger):

        def table(self, rows: 'Sequence[Mapping[str, Any]] | Sequence[Sequence[Any]]', *, headers: 'Sequence[str] | None'=None, title: 'str | None'=None, level: 'int'=logging.INFO) -> 'None':
            self.log(level, Logger._format_table(rows, headers=headers, title=title))

    @staticmethod
    def _format_table(rows: 'Sequence[Mapping[str, Any]] | Sequence[Sequence[Any]]', headers: 'Sequence[str] | None'=None, title: 'str | None'=None) -> 'str':
        if not rows:
            return f'{title}\n<empty table>' if title else '<empty table>'
        first = rows[0]
        body: 'list[list[str]]' = []
        if isinstance(first, Mapping):
            cols = list(headers) if headers else []
            if not cols:
                for row in rows:
                    if not isinstance(row, Mapping):
                        raise TypeError('Mixed table row types are not supported.')
                    for key in row:
                        key_s = str(key)
                        if key_s not in cols:
                            cols.append(key_s)
            for row in rows:
                if not isinstance(row, Mapping):
                    raise TypeError('Mixed table row types are not supported.')
                body.append([str(row.get(c, '')) for c in cols])
        else:
            cols = [str(h) for h in headers] if headers else [f'col_{i + 1}' for i in range(max((len(r) for r in rows)))]
            for row in rows:
                if isinstance(row, Mapping):
                    raise TypeError('Mixed table row types are not supported.')
                vals = [str(v) for v in row]
                if len(vals) < len(cols):
                    vals.extend([''] * (len(cols) - len(vals)))
                body.append(vals)
        widths = [len(c) for c in cols]
        for row in body:
            for i, value in enumerate(row):
                widths[i] = max(widths[i], len(value))
        border = '+' + '+'.join(('-' * (w + 2) for w in widths)) + '+'
        header = '| ' + ' | '.join((cols[i].ljust(widths[i]) for i in range(len(cols)))) + ' |'
        lines = ['| ' + ' | '.join((row[i].ljust(widths[i]) for i in range(len(cols)))) + ' |' for row in body]
        out = [border, header, border, *lines, border]
        return (title + '\n' if title else '') + '\n'.join(out)

    @classmethod
    def _ensure_logger_class(cls) -> 'None':
        if cls._logger_class_configured:
            return
        logging.setLoggerClass(cls.PrettyLogger)
        cls._logger_class_configured = True

    @classmethod
    def basicConfig(cls, *, level: 'int | str'=logging.INFO, format: 'str'='%(asctime)s | %(levelname)-8s | %(name)s | %(message)s', datefmt: 'str'='%Y-%m-%d %H:%M:%S') -> 'None':
        cls._ensure_logger_class()
        logging.basicConfig(level=level, format=format, datefmt=datefmt)

    @classmethod
    def getLogger(cls, name: 'str | None'=None) -> 'PrettyLogger':
        cls._ensure_logger_class()
        return logging.getLogger(name)

    @classmethod
    def condition(cls, prompt: 'str', value: 'bool') -> 'bool':
        cls.getLogger('vg2c.workflow').info('%s | IF evaluated to %s', prompt, value)
        return value

    @classmethod
    def table(cls, rows: 'Sequence[Mapping[str, Any]] | Sequence[Sequence[Any]]', *, headers: 'Sequence[str] | None'=None, title: 'str | None'=None, level: 'int'=logging.INFO, name: 'str | None'=None) -> 'None':
        cls.getLogger(name).table(rows, headers=headers, title=title, level=level)

def normalize_macro_name(raw: 'str') -> 'str':
    name = raw.strip()
    if name.startswith('<<<') and name.endswith('>>>'):
        name = name[3:-3]
    return name.strip().upper()

def resolve_path(name: 'str | Path', *, for_write: 'bool'=False) -> 'Path':
    path = Path(name)
    script_file = globals().get('__file__')
    if script_file and Path(script_file).name != '_runtime_helpers.py':
        base_dir = Path(script_file).resolve().parent
    else:
        base_dir = Path.cwd()
    if path.is_absolute():
        if for_write or path.exists():
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
    return base_path

class CrosstabUtility:
    TOKEN = 'CrossTab->[['
    TOKEN_RE = re.compile('(?P<prefix>,?)\\s*CrossTab->\\[\\[\\s*(?P<alias>[A-Za-z_][A-Za-z0-9_]*)\\s*,\\s*(?P<instance>[^;\\]]+)\\s*;\\s*:(?P<mode>[YyNn])\\s*\\]\\](?P<suffix>,?)')

    @classmethod
    def has_token(cls, value: 'str | None') -> 'bool':
        return bool(value and cls.TOKEN in value)

    @staticmethod
    def _extract_selected_columns_by_alias(sql: 'str') -> 'dict[str, set[str]]':
        by_alias: 'dict[str, set[str]]' = {}
        match = re.search('\\bSELECT\\b(?P<select_part>.*?)\\bFROM\\b', sql, flags=re.IGNORECASE | re.DOTALL)
        if not match:
            return by_alias
        select_part = match.group('select_part')
        col_ref_re = re.compile('\\b([A-Za-z_][A-Za-z0-9_]*)\\s*\\.\\s*(?:\\[([^\\]]+)\\]|"([^"]+)"|([A-Za-z_][A-Za-z0-9_]*))')
        for col_match in col_ref_re.finditer(select_part):
            alias = col_match.group(1).lower()
            col_name = col_match.group(2) or col_match.group(3) or col_match.group(4)
            if not col_name:
                continue
            by_alias.setdefault(alias, set()).add(col_name.lower())
        return by_alias

    @classmethod
    def substitute_sql(cls, sql: 'str', alias_columns_lookup: 'Callable[[str], list[str]] | None'=None) -> 'str':
        if alias_columns_lookup is None or not cls.has_token(sql):
            return sql
        selected_by_alias = cls._extract_selected_columns_by_alias(sql)

        def _replace(match: 're.Match[str]') -> 'str':
            prefix = match.group('prefix')
            suffix = match.group('suffix')
            alias = match.group('alias').strip()
            mode = match.group('mode').upper()
            all_cols = alias_columns_lookup(alias)
            selected = selected_by_alias.get(alias.lower(), set())
            dynamic_cols = [c for c in all_cols if c.lower() not in selected]
            if not dynamic_cols:
                return ''
            if mode == 'N':
                body = ','.join(dynamic_cols)
                return f'{prefix}{body}{suffix}'
            body = '\n         ,'.join((f'{alias}.[{c}] AS [{c}]' for c in dynamic_cols))
            return f'{prefix}{body}{suffix}'
        return cls.TOKEN_RE.sub(_replace, sql)

    def apply(self, rows: 'pd.DataFrame', row_keys: 'list[str]', header_key: 'str', value_key: 'str') -> 'Any':
        """Pivot row-oriented data into SQLPathFinder-style crosstab output."""
        if rows.empty or not row_keys or (not header_key) or (not value_key):
            return pd.DataFrame(columns=row_keys)
        ci_lookup = {str(c).casefold(): c for c in rows.columns}
        rename_map = {ci_lookup[k.casefold()]: k for k in (*row_keys, header_key, value_key)}
        df = rows.rename(columns=rename_map)
        df = df[df[header_key].notna() & (df[header_key].astype(str) != '')]
        if df.empty:
            return pd.DataFrame(columns=row_keys)
        result = df.groupby([*row_keys, header_key], dropna=False)[value_key].first().unstack(header_key, fill_value='').reset_index().rename_axis(columns=None)
        result.columns = [str(col).lower() for col in result.columns]
        return result

class CsvIO:

    def iter(self, name: 'str') -> 'Iterator[dict[str, str]]':
        """Yield each data row as a dict keyed by header names."""
        path = resolve_path(name)
        with path.open(newline='', encoding='utf-8', errors='replace') as fh:
            reader = csv.DictReader(fh)
            yield from reader

    def single_row(self, name: 'str') -> 'dict[str, str]':
        """Return exactly one data row from *name*; raise on 0 or >1 rows."""
        rows = self.iter(name)
        first = next(rows, None)
        if first is None:
            raise ValueError(f"CSV '{name}' must contain exactly 1 data row; found 0")
        second = next(rows, None)
        if second is not None:
            raise ValueError(f"CSV '{name}' must contain exactly 1 data row; found >1")
        return first

    def _read_column(self, path: 'str', column_ref: 'int | str') -> 'list[str]':
        """Read a column from a CSV file."""
        rows: 'list[str]' = []
        resolved_path = resolve_path(path)
        with resolved_path.open(newline='', encoding='utf-8', errors='replace') as fh:
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
            seen: 'dict[str, None]' = {}
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
    def _single_quote(value: 'str') -> 'str':
        return "'" + value.replace("'", "''") + "'"

    def sql_get_csv_list(self, path: 'str', column_ref: 'int | str', lead_in: 'str') -> 'str':
        """Return chunked IN-list clause for Oracle-style SQL.

        Oracle hard-limits IN lists to 1000 values. When there are more, the
        result is chunked: ``(v1..v1000) OR <lead_in> (v1001..)``.
        """
        values = self._read_column(path, column_ref)
        if not values:
            return "('__NO_VALUES__')"
        chunk_size = 1000
        chunks = [values[i:i + chunk_size] for i in range(0, len(values), chunk_size)]
        parts: 'list[str]' = []
        for i, chunk in enumerate(chunks):
            quoted = ', '.join((self._single_quote(v) for v in chunk))
            parts.append(f'({quoted})')
            if i < len(chunks) - 1:
                parts.append(f'\nOR {lead_in} ')
        return ''.join(parts)

    def row_count(self, name: 'str') -> 'int':
        """Count data rows (excludes header); 0 if file missing."""
        path = resolve_path(name)
        if not path.exists():
            return 0
        with path.open(newline='', encoding='utf-8', errors='replace') as fh:
            reader = csv.reader(fh)
            next(reader, None)
            return sum((1 for _ in reader))

    def iter_chunks(self, input_name: 'str', chunk_name: 'str', chunk_size: 'int') -> 'Iterator[Path]':
        """Stream *input_name* in fixed-size chunks, materializing each batch to *chunk_name*.

        Yields the chunk file path once per batch. The header of *input_name* is
        re-written at the top of each chunk so downstream readers can use it.
        """
        if chunk_size <= 0:
            chunk_size = 1
        in_path = resolve_path(input_name)
        out_path = resolve_path(chunk_name, for_write=True)
        out_path.parent.mkdir(parents=True, exist_ok=True)
        with in_path.open(newline='', encoding='utf-8', errors='replace') as fh:
            reader = csv.reader(fh)
            header = next(reader, None)
            batch: 'list[list[str]]' = []
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
    def _write_chunk(path: 'Path', header: 'list[str] | None', rows: 'list[list[str]]') -> 'None':
        with path.open('w', newline='', encoding='utf-8') as fh:
            writer = csv.writer(fh)
            if header is not None:
                writer.writerow(header)
            writer.writerows(rows)

    def write(self, name: 'str', content: 'Any', header: 'list[str] | None'=None) -> 'None':
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
                content = content.reindex(columns=[columns.get(column.casefold(), column) for column in header])
                content.columns = header
            content.to_csv(path, index=False, encoding='utf-8')
            return
        if isinstance(content, str):
            path.write_text(content, encoding='utf-8')
            return
        if isinstance(content, Path):
            import shutil
            shutil.copy2(content, path)
            return
        rows = list(content) if content is not None else []
        if not rows:
            if header is not None:
                with path.open('w', newline='', encoding='utf-8') as fh:
                    writer = csv.writer(fh)
                    writer.writerow(header)
            else:
                path.write_text('', encoding='utf-8')
            return
        with path.open('w', newline='', encoding='utf-8') as fh:
            if isinstance(rows[0], dict):
                fieldnames = header if header is not None else list(rows[0].keys())
                writer = csv.DictWriter(fh, fieldnames=fieldnames, extrasaction='ignore')
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
    PLACEHOLDER_RE = re.compile('<<<([^>]+)>>>|<<>>')
    NAMED_PLACEHOLDER_RE = re.compile('<<<([^>]+)>>>')

    def __init__(self) -> 'None':
        self._stack: 'list[dict[str, str]]' = [{}]

    def named(self, name: 'str') -> 'str':
        key = name.upper()
        for frame in reversed(self._stack):
            if key in frame:
                return frame[key]
        return ''

    def set_named(self, name: 'str', value: 'str') -> 'None':
        self._stack[-1][name.upper()] = value

    def positional(self) -> 'str':
        frame = self._stack[-1]
        cursor = frame.get('__cursor__', 0)
        pos_list: 'list[str]' = frame.get('__positional__', [])
        if isinstance(pos_list, list) and cursor < len(pos_list):
            frame['__cursor__'] = cursor + 1
            return pos_list[cursor]
        return ''

    def substitute(self, text: 'str', vars: 'dict[str, str] | None'=None) -> 'str':
        if not text:
            return ''

        def _lookup(name: 'str') -> 'str':
            key = normalize_macro_name(name)
            if vars is not None:
                return vars.get(key, '')
            return self.named(key)

        def _replace(match: 're.Match[str]') -> 'str':
            named = match.group(1)
            if named is not None:
                return _lookup(named)
            return self.positional()
        content = self.PLACEHOLDER_RE.sub(_replace, text)
        return content.lstrip('\n')

    def resolve_file_path(self, raw_path: 'str') -> 'Path':
        """Resolve a possibly-macro path with local basename fallback for abs paths."""
        if not raw_path:
            return Path('')
        resolved = self.substitute(raw_path)
        return resolve_path(resolved)

    def eval_condition(self, lhs: 'str', op: 'str', rhs: 'str') -> 'bool':
        lhs_val = self.named(lhs) if lhs.startswith('VAR(') else lhs
        rhs_val = self.named(rhs) if rhs.startswith('VAR(') else rhs
        return lhs_val == rhs_val

    def push_frame(self, named: 'dict[str, str] | None'=None) -> 'None':
        frame: 'dict[str, str]' = {}
        for k, v in (named or {}).items():
            if k is None:
                continue
            frame[k.upper()] = str(v)
        self._stack.append(frame)

    def pop_frame(self) -> 'None':
        if len(self._stack) > 1:
            self._stack.pop()

    @contextmanager
    def scope(self, row: 'dict[str, str] | None'=None) -> 'Iterator[None]':
        self.push_frame(named=row)
        try:
            yield
        finally:
            self.pop_frame()

class OracleClient:
    _reported_client = False
    _selected_instant_client: 'Path | None' = None

    @classmethod
    def configure(cls) -> 'str | None':
        """Prepare the current process for the configured DataSyncX Oracle client.

        Set ``DATASYNCX_ORACLE_CLIENT=instant`` to opt in. The normal
        ORACLE_HOME-based setup remains untouched when it is unset or ``home``.
        """
        mode = os.getenv('DATASYNCX_ORACLE_CLIENT', 'home').strip().lower()
        if mode in {'', 'home'}:
            return None
        if mode != 'instant':
            raise RuntimeError(f"DATASYNCX_ORACLE_CLIENT must be 'home' or 'instant', not {mode!r}.")
        if sys.platform != 'win32':
            raise RuntimeError('DataSyncX 1.1.6 initializes python-oracledb without lib_dir. On Linux, configure Instant Client with ldconfig (preferred) or LD_LIBRARY_PATH before starting Python; on macOS, update DataSyncX to pass lib_dir before using this selector.')
        client_dir = cls._find_instant_client()
        network_dir = cls._configure_network_files(client_dir)
        if network_dir is not None:
            import oracledb
            oracledb.defaults.config_dir = str(network_dir)
        os.environ.pop('ORACLE_HOME', None)
        cls._prepend_path(client_dir)
        cls._selected_instant_client = client_dir
        return str(client_dir)

    @classmethod
    def log_active_client(cls) -> 'None':
        """Print the initialized Oracle client once for terminal diagnostics."""
        if cls._reported_client:
            return
        import oracledb
        if oracledb.is_thin_mode():
            return
        try:
            version = '.'.join((str(part) for part in oracledb.clientversion()))
        except oracledb.Error:
            return
        source = f'Instant Client ({cls._selected_instant_client})' if cls._selected_instant_client else f"ORACLE_HOME ({os.getenv('ORACLE_HOME', 'PATH')})"
        print('\n' + '=' * 72)
        print(f' Oracle client: {version} | mode=thick | source={source}')
        print('=' * 72)
        cls._reported_client = True

    @staticmethod
    def _find_instant_client() -> 'Path':
        configured = os.getenv('DATASYNCX_INSTANT_CLIENT_DIR') or os.getenv('ORACLE_INSTANT_CLIENT_DIR')
        separator = ';' if sys.platform == 'win32' else os.pathsep
        candidates = [configured] if configured else os.getenv('PATH', '').split(separator)
        for candidate in candidates:
            if not candidate:
                continue
            path = Path(candidate).expanduser()
            if 'instantclient' in path.name.lower() and (path / 'oci.dll').is_file():
                return path.resolve()
        raise RuntimeError('Oracle Instant Client was requested but no usable directory was found. Set DATASYNCX_INSTANT_CLIENT_DIR to the directory containing oci.dll.')

    @staticmethod
    def _configure_network_files(client_dir: 'Path') -> 'Path | None':
        configured = os.getenv('DATASYNCX_ORACLE_NET_CONFIG_DIR')
        network_dir = Path(configured).expanduser() if configured else client_dir / 'network' / 'admin'
        if configured and (not network_dir.is_dir()):
            raise RuntimeError(f'DATASYNCX_ORACLE_NET_CONFIG_DIR does not exist or is not a directory: {network_dir}')
        if network_dir.is_dir():
            network_dir = network_dir.resolve()
            os.environ['TNS_ADMIN'] = str(network_dir)
            return network_dir
        return None

    @staticmethod
    def _prepend_path(client_dir: 'Path') -> 'None':
        separator = ';' if sys.platform == 'win32' else os.pathsep
        entries = [entry for entry in os.getenv('PATH', '').split(separator) if entry]
        selected = str(client_dir)
        os.environ['PATH'] = separator.join([selected, *(entry for entry in entries if Path(entry) != client_dir)])

class PipelineContext:

    def __init__(self, utilities: 'dict[str, object]') -> 'None':
        self.__dict__.update(utilities)

    def write_file(self, path: 'str', template: 'str', vars: 'dict[str, str] | None'=None) -> 'None':
        content = self.macro.substitute(template, vars=vars)
        self.fs_ops.write_file(path, content)

    def _read_datasyncx(self, sql: 'str', reader: 'Any', node: 'str'):
        try:
            result = reader.read(site=node, query=sql)
        finally:
            OracleClient.log_active_client()
        result.columns = [col.lower() for col in result.columns]
        return result

    def run_query(self, sql: 'str', output: 'str', reader: 'Any', inputs: 'list[str] | None'=None, header: 'list[str] | None'=None, crosstab: 'dict | None'=None, node: 'str | None'=None):
        sql = self.macro.substitute(sql)
        effective_node = node or self.macro.named('NODE') or os.environ.get('VG2C_DEFAULT_NODE', 'KM')
        if hasattr(reader, 'execute'):
            result = reader.execute(sql, inputs or [])
        else:
            result = self._read_datasyncx(sql, reader, effective_node)
        if crosstab:
            result = self.crosstab.apply(result, row_keys=crosstab['row_keys'], header_key=crosstab['header_key'], value_key=crosstab['value_key'])
        self.csv_io.write(output, result, header=header)

    def eval_condition(self, lhs: 'str', op: 'str', rhs: 'str', *args: 'Any') -> 'bool':
        return self.macro.eval_condition(lhs, op, rhs)

class SqliteEngine:
    _SQL_NUMBER = '[+-]?(?:\\d+(?:\\.\\d*)?|\\.\\d+)(?:[eE][+-]?\\d+)?'

    @staticmethod
    def global_sql(value, numeric: 'bool'=False) -> 'str':
        """Render editable operands with their original SQL literal category."""
        if isinstance(value, list):
            if not value:
                raise ValueError('SQL IN globals must contain at least one value')
            return ', '.join((SqliteEngine.global_sql(item, numeric) for item in value))
        if numeric or type(value) is int:
            text = str(value)
            if not re.fullmatch(SqliteEngine._SQL_NUMBER, text):
                raise ValueError(f'Invalid SQL numeric global: {value!r}')
            return text
        if not isinstance(value, str):
            raise ValueError(f'Expected a SQL string or number, got {value!r}')
        return "'" + value.replace("'", "''") + "'"

class SqliteReader:
    STMT_SPLIT_RE = re.compile('(?:\'[^\']*\'|\\"[^\\"]*\\"|\\[[^\\]]*\\]|`[^`]*`|[^;])+', re.DOTALL)

    @staticmethod
    def _load_csv_as_table(conn: 'sqlite3.Connection', csv_path: 'str', table_name: 'str | None'=None) -> 'str':
        path = Path(csv_path)
        table_name = table_name or path.stem
        with path.open(newline='', encoding='utf-8', errors='replace') as fh:
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
        col_defs = ', '.join((f'"{c}" TEXT' for c in cols))
        conn.execute(f'DROP TABLE IF EXISTS "{table_name}"')
        conn.execute(f'CREATE TABLE "{table_name}" ({col_defs})')
        header_str = [str(c) for c in cols]
        filtered_rows = [row for row in rows if [str(row.get(c, '')) for c in cols] != header_str]
        if filtered_rows:
            placeholders = ', '.join(('?' for _ in cols))
            conn.executemany(f'INSERT INTO "{table_name}" VALUES ({placeholders})', [[row.get(c, '') for c in cols] for row in filtered_rows])
        return table_name

    @classmethod
    def _split_statements(cls, sql: 'str') -> 'list[str]':
        return [match.group(0).strip() for match in cls.STMT_SPLIT_RE.finditer(sql) if match.group(0).strip()]

    def execute(self, sql: 'str', inputs: 'list[str | tuple[str, str]]') -> 'pd.DataFrame':
        conn = sqlite3.connect(':memory:')
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
        alias_to_table: 'dict[str, str]' = {}
        alias_map_re = re.compile('\\b(?:FROM|JOIN)\\s+(?:\\[([^\\]]+)\\]|\\"([^\\"]+)\\"|([A-Za-z_][A-Za-z0-9_]*))\\s+([A-Za-z_][A-Za-z0-9_]*)\\b', re.IGNORECASE)
        for match in alias_map_re.finditer(final_stmt):
            table_name = match.group(1) or match.group(2) or match.group(3)
            alias = match.group(4)
            if table_name and alias:
                alias_to_table[alias.lower()] = table_name

        def _lookup_alias_columns(alias: 'str') -> 'list[str]':
            table_name = alias_to_table.get(alias.lower())
            if not table_name:
                return []
            pragma_rows = conn.execute(f'PRAGMA table_info("{table_name}")').fetchall()
            return [str(row[1]) for row in pragma_rows if len(row) > 1]
        final_stmt = CrosstabUtility.substitute_sql(final_stmt, alias_columns_lookup=_lookup_alias_columns)
        try:
            cursor = conn.execute(final_stmt)
            rows = cursor.fetchall()
            col_names = [d[0] for d in cursor.description] if cursor.description else []
        except sqlite3.Error as exc:
            conn.close()
            raise RuntimeError(f'SQLite error in execute: {exc}\nSQL:\n{final_stmt}') from exc
        conn.close()
        if not rows or not col_names:
            return pd.DataFrame()
        data = [{col_names[i]: row[i] for i in range(len(col_names))} for row in rows]
        return pd.DataFrame(data)

# <vg2c:dependencies:end>
# <vg2c:steps:start>
def step_0000_sql_query(ctx) -> None:
    ctx.run_query(sql="\n/*BEGIN SQL*/\nSELECT \n          lot_1 AS lot_1\n         ,operation_1 AS operation_1\n         ,To_Char(out_date,'yyyy-mm-dd hh24:mi:ss') AS out_date\n         ,oldqty1 AS oldqty1\n         ,newqty1 AS newqty1\n         ,Replace(Replace(Replace(Replace(Replace(Replace(Interposer_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Interposer_SLI\n         ,Replace(Replace(Replace(Replace(Replace(Replace(Patch_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Patch_SLI\n         ,prodgroup3_1 AS prodgroup3_1\n         ,entity AS entity\n         ,transaction AS transaction\nFROM\n(\nSELECT  \n          f0.lot AS lot_1\n         ,f0.operation AS operation_1\n         ,f0.out_date AS out_date\n         ,f0.oldqty1 AS oldqty1\n         ,f0.newqty1 AS newqty1\n         ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = " + SqliteEngine.global_sql(ATTRIBUTE_NUMBER, True) + ' AND la.src_erase_date IS NULL AND rownum <= ' + SqliteEngine.global_sql(ROWNUM_MAX, True) + ') AS Interposer_SLI\n         ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = ' + SqliteEngine.global_sql(ATTRIBUTE_NUMBER_2, True) + ' AND la.src_erase_date IS NULL AND rownum <= ' + SqliteEngine.global_sql(ROWNUM_MAX, True) + ") AS Patch_SLI\n         ,p.prodgroup3 AS prodgroup3_1\n         ,f4.entity AS entity\n         ,f5.transaction AS transaction\nFROM \n@[]@.F_LotHist f0\nLEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version\nINNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot\nLEFT JOIN @[]@.F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = " + SqliteEngine.global_sql(UNIQUE_FLAG, False) + "\n AND      f4.entity Like 'IAM%' \nLEFT JOIN @[]@.F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource\nLEFT JOIN @[]@.F_LotTxnHist f5 ON f5.lot = f0.lot AND f5.operation = f0.operation AND f5.prevout_date = f0.prevout_date AND NVL(f5.history_deleted_flag,'N') = 'N'\n AND      f5.transaction = " + SqliteEngine.global_sql(TRANSACTION, False) + " \nWHERE\nNVL(f0.history_deleted_flag,'N') = 'N'\nAND      f0.owner <> " + SqliteEngine.global_sql(OWNER, False) + '\n AND      f0.operation = ' + SqliteEngine.global_sql(OPERATION, False) + " \n AND      f0.out_date >= TRUNC(SYSDATE) - 2 \n AND      p.prodgroup3 Like 'CWF%' \n-- Tail A\n)\nWHERE\n              Interposer_SLI Is Not Null  \n/*END SQL*/\n\n", output='yeuchuan_a1_17362.tab', reader=MarsReader(), header=['lot_1', 'operation_1', 'out_date', 'oldqty1', 'newqty1', 'Interposer_SLI', 'Patch_SLI', 'prodgroup3_1', 'entity', 'transaction'])

def step_0001_sql_query(ctx) -> None:
    ctx.run_query(sql="\n/*BEGIN SQL*/\nSELECT \n          facility AS facility\n         ,operation AS operation\n         ,module_name AS module_name\n         ,tool_entity AS tool_entity\n         ,primary_entity AS primary_entity\n         ,To_Char(processing_start_date,'yyyy-mm-dd hh24:mi:ss') AS processing_start_date\n         ,To_Char(processing_end_date,'yyyy-mm-dd hh24:mi:ss') AS processing_end_date\n         ,lot AS lot\n         ,product AS product\n         ,prodgroup3 AS prodgroup3\n         ,Replace(Replace(Replace(Replace(Replace(Replace(product_desc,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS product_desc\n         ,owner AS owner\n         ,visual_id AS visual_id\n         ,ws_loss_code AS ws_loss_code\n         ,media_in_x AS media_in_x\n         ,media_in_y AS media_in_y\n         ,parameter AS parameter\n         ,Max(numeric_value) AS numeric_value\nFROM\n(\nSELECT  \n          bams0.facility AS facility\n         ,bams0.operation AS operation\n         ,bams0.module_name AS module_name\n         ,bams0.tool_entity AS tool_entity\n         ,bams0.primary_entity AS primary_entity\n         ,bams0.processing_start_time AS processing_start_date\n         ,bams0.processing_end_time AS processing_end_date\n         ,bams0.lot AS lot\n         ,ml.product AS product\n         ,mp.prodgroup3 AS prodgroup3\n         ,mp.product_description AS product_desc\n         ,ml.owner AS owner\n         ,bams2.visual_id AS visual_id\n         ,bams2.ws_loss_code AS ws_loss_code\n         ,bams2.media_in_x AS media_in_x\n         ,bams2.media_in_y AS media_in_y\n         ,bams3.parameter AS parameter\n         ,bams3.numeric_value AS numeric_value\nFROM \nARIES_Views.AV_BAMS_SESSION bams0\nLEFT JOIN A_MARS_Lot ml ON bams0.lot=ml.lot\nLEFT JOIN A_MARS_Product mp ON ml.product = mp.product AND ml.mars_schema=mp.mars_schema AND mp.facility=bams0.facility\nINNER JOIN ARIES_Views.AV_BAMS_MEDIA_TESTING bams1 ON bams1.lao_start_ww = bams0.lao_start_ww AND bams1.obj_s_id = bams0.obj_s_id\nINNER JOIN ARIES_Views.AV_BAMS_UNIT_TESTING bams2 ON bams2.lao_start_ww = bams1.lao_start_ww AND bams2.obj_s_id = bams1.obj_s_id AND bams2.obj_mt_id = bams1.obj_mt_id\nLEFT JOIN ARIES_Views.AV_BAMS_DEVICE_RESULTS bams3 ON bams3.lao_start_ww = bams2.lao_start_ww AND bams3.obj_s_id = bams2.obj_s_id AND bams3.obj_mt_id = bams2.obj_mt_id AND bams3.obj_ut_id = bams2.obj_ut_id\nWHERE\n              (bams0.lot In \n" + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a1_17362.tab', 'lot_1', 'bams0.lot In') + ')' + ' \n AND      bams0.operation = ' + SqliteEngine.global_sql(OPERATION, False) + ' \n)\nGROUP BY \n          facility\n         ,operation\n         ,module_name\n         ,tool_entity\n         ,primary_entity\n         ,processing_start_date\n         ,processing_end_date\n         ,lot\n         ,product\n         ,prodgroup3\n         ,product_desc\n         ,owner\n         ,visual_id\n         ,ws_loss_code\n         ,media_in_x\n         ,media_in_y\n         ,parameter\n/*END SQL*/\n\n', output='yeuchuan_a0_17362.tab', reader=AriesReader(), crosstab={'row_keys': ['facility', 'operation', 'module_name', 'tool_entity', 'primary_entity', 'processing_start_date', 'processing_end_date', 'lot', 'product', 'prodgroup3', 'product_desc', 'owner', 'visual_id', 'ws_loss_code', 'media_in_x', 'media_in_y'], 'header_key': 'parameter', 'value_key': 'numeric_value'})

def step_0002_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""

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
    """, output='PARMI_IPM_RAW.csv', reader=SqliteReader(), inputs=['yeuchuan_a1_17362.tab', 'yeuchuan_a0_17362.tab'])

def step_0003_sqlite_query(ctx) -> None:
    ctx.run_query(sql="\n\nDROP TABLE IF EXISTS T_L0_Init;\nCREATE TABLE T_L0_Init AS\nSELECT /*L0*/  \n          a0.[lot_1] AS [lot_1]\n         ,a0.[newqty1] AS [newqty1]\n         ,a0.[facility] AS [facility]\n         ,a0.[operation] AS [operation]\n         ,a0.[tool_entity] AS [tool_entity]\n         ,a0.[primary_entity] AS [primary_entity]\n         ,a0.[processing_end_date] AS [processing_end_date]\n         ,a0.[lot] AS [lot]\n         ,a0.[prodgroup3] AS [prodgroup3]\n         ,a0.[product] AS [product]\n         ,a0.[visual_id] AS [visual_id]\n         ,a0.[ws_loss_code] AS [ws_loss_code]\n         ,a0.[media_in_x] AS [media_in_x]\n         ,a0.[media_in_y] AS [media_in_y]\n         ,a0.[height] AS [height]\n         ,a0.[patch_lift_roi1] AS [patch_lift_roi1]\n         ,a0.[patch_lift_roi2] AS [patch_lift_roi2]\n         ,a0.[patch_lift_roi3] AS [patch_lift_roi3]\n         ,a0.[patch_lift_roi4] AS [patch_lift_roi4]\n         ,a0.[patch_lift_roi5] AS [patch_lift_roi5]\n         ,a0.[patch_lift_roi6] AS [patch_lift_roi6]\n         ,a0.[patch_lift_roi7] AS [patch_lift_roi7]\n         ,a0.[patch_lift_roi8] AS [patch_lift_roi8]\n         ,a0.[patch_lift_roi_max] AS [patch_lift_roi_max]\n         ,a0.[patch_sli] AS [patch_sli]\n         ,a0.[interposer_sli] AS [interposer_sli]\n         ,CASE  WHEN a0.[patch_lift_roi4]  >= 2000 AND  a0.[patch_lift_roi_max]  >= 2030 THEN '1' WHEN a0.[patch_lift_roi8] >= 2000 AND  a0.[patch_lift_roi_max]  >= 2030 THEN '1' ELSE '0' END AS [NCO_Risk]\nFROM \n[PARMI_IPM_RAW] a0\n;\n\nDROP TABLE IF EXISTS T_L0_1_1;\nCREATE TABLE T_L0_1_1 AS\nSELECT COUNT(DISTINCT  visual_id) AS AF$S1\n,lot_1 AS AF$PB1\nFROM T_L0_Init GROUP BY \nAF$PB1\n;\nCREATE INDEX T_L0_1_1_Idx ON T_L0_1_1 (AF$PB1);\nDROP TABLE IF EXISTS T_L0_1_Result;\nCREATE TABLE T_L0_1_Result AS\nSELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [VIDCount]\nFROM T_L0_Init a0 LEFT JOIN T_L0_1_1 a1 ON \nlot_1 = a1.AF$PB1\n;\nDROP TABLE IF EXISTS T_L0_1_1;\n\nCREATE INDEX T_L0_1_Result_Idx ON T_L0_1_Result (orig_rowid);\nDROP TABLE IF EXISTS T_L0_Result;\nCREATE TABLE T_L0_Result AS\nSELECT\n\n[lot_1]\n,[newqty1]\n,[facility]\n,[operation]\n,[tool_entity]\n,[primary_entity]\n,[processing_end_date]\n,[lot]\n,[prodgroup3]\n,[product]\n,[visual_id]\n,[ws_loss_code]\n,[media_in_x]\n,[media_in_y]\n,[height]\n,[patch_lift_roi1]\n,[patch_lift_roi2]\n,[patch_lift_roi3]\n,[patch_lift_roi4]\n,[patch_lift_roi5]\n,[patch_lift_roi6]\n,[patch_lift_roi7]\n,[patch_lift_roi8]\n,[patch_lift_roi_max]\n,[patch_sli]\n,[interposer_sli]\n,[NCO_Risk]\n,[VIDCount]\nFROM T_L0_Init a0\nLEFT JOIN T_L0_1_Result a1 ON a0.rowid = a1.orig_rowid\n;\nDROP TABLE IF EXISTS T_L0_1_Result;\nDROP TABLE IF EXISTS T_L0_Init;\n\nSELECT /*L3*/ \n          [lot_1] AS [lot_1]\n         ,[newqty1] AS [newqty1]\n         ,[facility] AS [facility]\n         ,[operation] AS [operation]\n         ,[tool_entity] AS [tool_entity]\n         ,[primary_entity] AS [primary_entity]\n         ,[processing_end_date] AS [processing_end_date]\n         ,[lot] AS [lot]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[product] AS [product]\n         ,[visual_id] AS [visual_id]\n         ,[ws_loss_code] AS [ws_loss_code]\n         ,[media_in_x] AS [media_in_x]\n         ,[media_in_y] AS [media_in_y]\n         ,[height] AS [height]\n         ,[patch_lift_roi1] AS [patch_lift_roi1]\n         ,[patch_lift_roi2] AS [patch_lift_roi2]\n         ,[patch_lift_roi3] AS [patch_lift_roi3]\n         ,[patch_lift_roi4] AS [patch_lift_roi4]\n         ,[patch_lift_roi5] AS [patch_lift_roi5]\n         ,[patch_lift_roi6] AS [patch_lift_roi6]\n         ,[patch_lift_roi7] AS [patch_lift_roi7]\n         ,[patch_lift_roi8] AS [patch_lift_roi8]\n         ,[patch_lift_roi_max] AS [patch_lift_roi_max]\n         ,[patch_sli] AS [patch_sli]\n         ,[interposer_sli] AS [interposer_sli]\n         ,[NCO_Risk] AS [NCO_Risk]\n         ,[VIDCount] AS [VIDCount]\n         ,[FlagLot] AS [FlagLot]\nFROM\n(\nSELECT /*L2*/ \n          [lot_1] AS [lot_1]\n         ,[newqty1] AS [newqty1]\n         ,[facility] AS [facility]\n         ,[operation] AS [operation]\n         ,[tool_entity] AS [tool_entity]\n         ,[primary_entity] AS [primary_entity]\n         ,[processing_end_date] AS [processing_end_date]\n         ,[lot] AS [lot]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[product] AS [product]\n         ,[visual_id] AS [visual_id]\n         ,[ws_loss_code] AS [ws_loss_code]\n         ,[media_in_x] AS [media_in_x]\n         ,[media_in_y] AS [media_in_y]\n         ,[height] AS [height]\n         ,[patch_lift_roi1] AS [patch_lift_roi1]\n         ,[patch_lift_roi2] AS [patch_lift_roi2]\n         ,[patch_lift_roi3] AS [patch_lift_roi3]\n         ,[patch_lift_roi4] AS [patch_lift_roi4]\n         ,[patch_lift_roi5] AS [patch_lift_roi5]\n         ,[patch_lift_roi6] AS [patch_lift_roi6]\n         ,[patch_lift_roi7] AS [patch_lift_roi7]\n         ,[patch_lift_roi8] AS [patch_lift_roi8]\n         ,[patch_lift_roi_max] AS [patch_lift_roi_max]\n         ,[patch_sli] AS [patch_sli]\n         ,[interposer_sli] AS [interposer_sli]\n         ,[NCO_Risk] AS [NCO_Risk]\n         ,[VIDCount] AS [VIDCount]\n         ,CASE WHEN  [NCO_Risk]  = '1' OR  [VIDCount] < 2 THEN '1' ELSE '0' END AS [FlagLot]\nFROM\n(\nSELECT /*L1*/ \n          [lot_1] AS [lot_1]\n         ,[newqty1] AS [newqty1]\n         ,[facility] AS [facility]\n         ,[operation] AS [operation]\n         ,[tool_entity] AS [tool_entity]\n         ,[primary_entity] AS [primary_entity]\n         ,[processing_end_date] AS [processing_end_date]\n         ,[lot] AS [lot]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[product] AS [product]\n         ,[visual_id] AS [visual_id]\n         ,[ws_loss_code] AS [ws_loss_code]\n         ,[media_in_x] AS [media_in_x]\n         ,[media_in_y] AS [media_in_y]\n         ,[height] AS [height]\n         ,[patch_lift_roi1] AS [patch_lift_roi1]\n         ,[patch_lift_roi2] AS [patch_lift_roi2]\n         ,[patch_lift_roi3] AS [patch_lift_roi3]\n         ,[patch_lift_roi4] AS [patch_lift_roi4]\n         ,[patch_lift_roi5] AS [patch_lift_roi5]\n         ,[patch_lift_roi6] AS [patch_lift_roi6]\n         ,[patch_lift_roi7] AS [patch_lift_roi7]\n         ,[patch_lift_roi8] AS [patch_lift_roi8]\n         ,[patch_lift_roi_max] AS [patch_lift_roi_max]\n         ,[patch_sli] AS [patch_sli]\n         ,[interposer_sli] AS [interposer_sli]\n         ,[NCO_Risk] AS [NCO_Risk]\n         ,[VIDCount] AS [VIDCount]\nFROM\n(\nT_L0_Result\n)\n) t /*L1*/\n) t /*L2*/\nWHERE\n              [FlagLot] = " + SqliteEngine.global_sql(FLAGLOT, False) + ' \n;\n', output='IPM_Data.csv', reader=SqliteReader(), inputs=['PARMI_IPM_RAW.csv'], header=['lot_1', 'newqty1', 'facility', 'operation', 'tool_entity', 'primary_entity', 'processing_end_date', 'lot', 'prodgroup3', 'product', 'visual_id', 'ws_loss_code', 'media_in_x', 'media_in_y', 'height', 'patch_lift_roi1', 'patch_lift_roi2', 'patch_lift_roi3', 'patch_lift_roi4', 'patch_lift_roi5', 'patch_lift_roi6', 'patch_lift_roi7', 'patch_lift_roi8', 'patch_lift_roi_max', 'patch_sli', 'interposer_sli', 'NCO_Risk', 'VIDCount', 'FlagLot'])

def step_0004_sqlite_query(ctx) -> None:
    ctx.run_query(sql='\nSELECT /*L0*/  DISTINCT \n          a0.[lot] AS [Lot_NCORisk]\nFROM \n[IPM_Data] a0\nWHERE\n NOT          (a0.[lot] In \n' + ctx.csv_io.sql_get_csv_list('.\\HIST.csv', 1, 'a0.[lot] In') + ')', output='DATA.csv', reader=SqliteReader(), inputs=['IPM_Data.csv'], header=['Lot_NCORisk'])

# <vg2c:steps:end>

# <vg2c:workflow:start>
def run() -> None:
    Logger.basicConfig(level=Logger.INFO)
    OracleClient.configure()
    ctx = PipelineContext({'crosstab': CrosstabUtility(), 'csv_io': CsvIO(), 'macro': MacroState()})
    step_0000_sql_query(ctx)
    step_0001_sql_query(ctx)
    step_0002_sqlite_query(ctx)
    step_0003_sqlite_query(ctx)
    step_0004_sqlite_query(ctx)
# <vg2c:workflow:end>

if __name__ == "__main__":
    run()