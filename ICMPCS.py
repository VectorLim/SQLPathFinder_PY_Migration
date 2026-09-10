# SQL statements containing filters:
# - step_0016_sqlite_query (Line 1057): filters on a0.icmpcs
# - step_0043_sql_query (Line 1093): filters on f0.history_deleted_flag, f0.operation, f0.out_date, f0.owner, f4.history_deleted_flag, f4.unique_flag, f5.history_deleted_flag, f5.transaction, la.attribute_number, p.latest_version
# - step_0044_sql_query (Line 1096): filters on bams0.lot, bams0.operation, yeuchuan_a1_22697.tab
# - step_0046_sqlite_query (Line 1140): filters on FlagLot, a0.rowid, lot_1

# Auto-generated Python script from VG2
"""Pipeline implementation."""

from collections.abc import Callable
from collections.abc import Iterator
from collections.abc import Mapping
from collections.abc import Sequence
from contextlib import contextmanager
from datasyncx import AriesReader
from datasyncx import MarsReader
from email.message import EmailMessage
from pathlib import Path
from typing import Any
from typing import ClassVar
import csv
import keyring
import logging
import os
import pandas
import pandas as pd
import re
import shutil
import smtplib
import sqlite3
import subprocess
import sys

EMAIL_TO = 'alex.chin.hooi.lee@intel.com'
ICMPCS = 'ICMPCS'
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

class ExternalProcess:

    @staticmethod
    def _resolve_exedir() -> 'str':
        """Return the SPF tools directory from env var VG2C_EXEDIR."""
        return os.environ.get('VG2C_EXEDIR', '')

    @staticmethod
    def _resolve_path(path: 'str') -> 'str':
        return os.path.normpath(path)

    @classmethod
    def _resolve_argv(cls, argv: 'list[str]') -> 'list[str]':
        """Substitute @EXEDIR@ tokens and normalise path-like arguments."""
        exedir = cls._resolve_exedir()
        return [cls._resolve_path(a) if os.sep in a else a for a in (arg.replace('@EXEDIR@', exedir) for arg in argv)]

    def run(self, argv: 'list[str]', cwd: 'str | Path | None'=None, env: 'dict | None'=None, check: 'bool'=False) -> 'int':
        resolved = self._resolve_argv(argv)
        first = resolved[0] if resolved else ''
        use_shell = Path(first).suffix.lower() in {'.bat', '.va', '.exe'}
        result = subprocess.run(resolved, cwd=str(cwd) if cwd else None, env=env, check=check, shell=use_shell)
        return result.returncode

class FileSystemOps:

    def copy(self, src: 'str | Path', dst: 'str | Path', recurse: 'bool'=False) -> 'None':
        src, dst = (Path(src), Path(dst))
        dst.parent.mkdir(parents=True, exist_ok=True)
        if src.is_dir():
            shutil.copytree(src, dst, dirs_exist_ok=True)
        else:
            shutil.copy2(src, dst)

    def rename(self, src: 'str | Path', dst: 'str | Path') -> 'None':
        Path(src).replace(Path(dst))

    def delete(self, paths: 'list[str | Path]', recurse: 'bool'=False) -> 'None':
        for p in paths:
            path = Path(p)
            if path.is_dir():
                if recurse:
                    shutil.rmtree(path, ignore_errors=True)
            else:
                path.unlink(missing_ok=True)

    def write_file(self, path: 'str | Path', content: 'str') -> 'None':
        out = resolve_path(path, for_write=True)
        out.parent.mkdir(parents=True, exist_ok=True)
        out.write_text(content, encoding='utf-8')

class HtmlReport:
    _ROW_DELIM = '<\\\\>'
    _TRUE_VALUES = ('Y', 'YES', 'TRUE')
    _HTML_SCAFFOLD = '<html>\n<head>\n<title>{title}</title>\n<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">\n{css_decl}\n<!--@SPF-JS-HEADER@-->\n<style type="text/css">\ntable.tblout, td.tblout, tr.tblout {{\n    border-width:0px;\n    border-collapse:collapse;\n    border-style:none;\n    text-align:left;\n    vertical-align:top;\n}}\ntd.tblout {{ padding:10px; }}\nimg {{ vertical-align:top; }}\na {{ text-decoration:none; color:#464feb; }}\ntr th, tr td {{ border:1px solid #e6e6e6; }}\ntr th {{ background-color:#f5f5f5; }}\n</style>\n</head>\n<body>\n{body}\n</body>\n</html>\n'
    _CSS_RULES: 'list[dict[str, Any]]' = [{'name': 'COLUMN-BORDER', 'template': 'table.tblin, td.tblin, th, td.alt \n{{\n{decls}\n}}', 'extras': ['td.tblin,th,td.alt\n{\n      padding:5px;\n}', '  table.tblin \n{\n     caption-side:top;\n}'], 'tail_template': 'tr.at-bot-of-report, td.at-bot-of-report {{\n{decls}\n\n}}'}, {'name': 'Column-Headers', 'template': 'th, #colhdr\n{{\n{decls}\n}}', 'defaults': [('padding-top', '     padding-top:5px;'), ('padding-bottom', '     padding-bottom:4px;')]}, {'name': 'Column-Data', 'template': 'td.tblin, caption, table.tblin \n{{\n{decls}\n}}', 'extras': ['  caption {padding-top:5px;}']}, {'name': 'Column-Alt-Row', 'template': 'td.alt\n{{\n{decls}\n}}'}, {'name': 'At-Top-of-Report', 'template': 'p.at-top-of-report\n{{\n{decls}\n}}'}, {'name': 'JQX-All-IChart-Text', 'template': '.jqx-chart-axis-text, .jqx-chart-label-text, .jqx-chart-legend-text, .jqx-chart-axis-description, .jqx-chart-title-text, .jqx-chart-title-description {{\n{decls}\n}}', 'defaults': [('fill', '     fill:black;')]}, {'name': 'At-Top-of-Col1', 'template': 'p.at-top-of-col1\n{{\n{decls}\n}}'}, {'name': 'At-Top-of-Col2', 'template': 'p.at-top-of-col2\n{{\n{decls}\n}}'}, {'name': 'At-Top-of-Col3', 'template': 'p.at-top-of-col3\n{{\n{decls}\n}}'}]

    def __init__(self) -> 'None':
        self.styles: 'dict[str, list[str]]' = {}
        self.css_file: 'str | None' = None
        self.deferred_reports: 'dict[str, dict[str, Any]]' = {}
        self.instance: 'str | None' = None

    @classmethod
    def _iter_rows(cls, template: 'str | None'):
        """Yield non-empty rows split on the SPF delimiter."""
        for line in (template or '').splitlines():
            if not line.strip():
                continue
            parts = [p.strip() for p in line.split(cls._ROW_DELIM)]
            if len(parts) >= 2:
                yield parts

    @classmethod
    def _parse_options(cls, template: 'str | None') -> 'dict[str, Any]':
        """Parse an SPF options template into {KEY: value|list-of-values}."""
        options: 'dict[str, Any]' = {}
        for parts in cls._iter_rows(template):
            key = parts[0].upper()
            vals = [p for p in (parts[2:] if parts[1] == '' else parts[1:]) if p]
            if not vals:
                options[key] = ''
            elif len(vals) == 1:
                options[key] = vals[0]
            else:
                options[key] = vals
        return options

    def run(self, instance: 'str | None'=None, prompt_text: 'str | None'=None, app_server_default: 'str | None'=None, template: 'str | None'=None) -> 'None':
        self.instance = instance
        for parts in self._iter_rows(template):
            key = parts[0].upper()
            if key == 'CSS':
                self.css_file = parts[1] or None
            elif key == 'FORMAT' and len(parts) >= 3:
                self.styles[parts[1]] = parts[2:]

    def defer(self, id: 'str', instance: 'str | None'=None, prompt_text: 'str | None'=None, app_server_default: 'str | None'=None, template: 'str | None'=None) -> 'None':
        self.deferred_reports[id] = {'instance': instance, 'template': template, 'options': self._parse_options(template)}

    def delete(self, instance: 'str | None'=None) -> 'None':
        self.styles.clear()
        self.css_file = None
        self.deferred_reports.clear()

    def layout(self, ctx: 'Any', template: 'str', outlook: 'str | None'=None, instance: 'str | None'=None, json_only: 'str | None'=None, chart_instance: 'str | None'=None, app_server_default: 'str | None'=None) -> 'None':
        directives, body = self._split_layout(template)
        body = re.sub('HTM:([A-Za-z0-9_]+)', lambda m: self._render_report(m.group(1), ctx) if m.group(1) in self.deferred_reports else m.group(0), body)
        css_file = directives.get('CSS') or self.css_file
        css_embed = directives.get('CSSEMBED', '').upper() in self._TRUE_VALUES
        css_decl = self._resolve_css(css_file, css_embed)
        title = directives.get('TITLE', 'SQLPathFinder Report')
        if '<html>' not in body.lower():
            body = self._HTML_SCAFFOLD.format(title=title, css_decl=css_decl, body=body)
        elif css_decl:
            if '</head>' in body:
                body = body.replace('</head>', f'{css_decl}\n</head>', 1)
            else:
                body = f'{css_decl}\n{body}'
        filename = self._resolve_output_filename(directives.get('FILE', 'report.html'), instance)
        if ctx and hasattr(ctx, 'macro'):
            filename = ctx.macro.substitute(filename)
        if ctx and hasattr(ctx, 'write_file'):
            ctx.write_file(filename, body)
        else:
            out = Path(filename)
            out.parent.mkdir(parents=True, exist_ok=True)
            out.write_text(body, encoding='utf-8')

    @staticmethod
    def _split_layout(template: 'str') -> 'tuple[dict[str, str], str]':
        """Extract ':KEY:VALUE' directives from a layout template, return (dirs, body)."""
        directives: 'dict[str, str]' = {}
        body_lines: 'list[str]' = []
        for line in template.splitlines():
            if line.startswith(':'):
                head, sep, value = line[1:].partition(':')
                if sep:
                    directives[head.strip().upper()] = value.strip()
                    continue
            body_lines.append(line)
        return (directives, '\n'.join(body_lines))

    def _resolve_css(self, css_file: 'str | None', css_embed: 'bool') -> 'str':
        """Return a <style> or <link> tag string (or empty string)."""
        content = ''
        if css_file:
            path = Path(css_file)
            if path.exists():
                content = path.read_text(encoding='utf-8', errors='replace')
            elif self.styles:
                content = self._build_css()
                try:
                    path.parent.mkdir(parents=True, exist_ok=True)
                    path.write_text(content, encoding='utf-8')
                except OSError:
                    pass
        elif self.styles:
            content = self._build_css()
        if css_embed and content:
            return f'<style type="text/css">\n{content}\n</style>'
        if css_file and (not css_embed):
            return f'<link rel="stylesheet" type="text/css" href="{css_file}" />'
        return ''

    def _build_css(self) -> 'str':

        def get_decls(name: 'str') -> 'list[str]':
            decls: 'list[str]' = []
            for d in self.styles.get(name, []):
                d = d.strip()
                if not d:
                    continue
                if ':' in d:
                    key, val = (s.strip() for s in d.split(':', 1))
                    if key == 'font-size' and val.isdigit():
                        val += 'px'
                    decls.append(f'     {key}:{val};')
                else:
                    decls.append(f'     {d};')
            return decls
        blocks: 'list[str]' = []
        for rule in self._CSS_RULES:
            decls = get_decls(rule['name'])
            if not decls:
                continue
            extras = list(decls)
            for token, default_decl in rule.get('defaults', []):
                if not any((token in d for d in extras)):
                    extras.append(default_decl)
            blocks.append(rule['template'].format(decls='\n'.join(extras)))
            blocks.extend(rule.get('extras', []))
            tail = rule.get('tail_template')
            if tail:
                blocks.append(tail.format(decls='\n'.join(decls)))
        return '\n\n'.join(blocks)

    def _render_report(self, report_id: 'str', ctx: 'Any') -> 'str':
        report = self.deferred_reports.get(report_id)
        if not report:
            return ''
        options = report.get('options')
        if not isinstance(options, dict):
            options = self._parse_options(report.get('template'))
            report['options'] = options

        def as_list(val: 'Any') -> 'list[str]':
            if val is None:
                return []
            return list(val) if isinstance(val, list) else [str(val)]
        cols = as_list(options.get('COLUMN-DATA'))
        headers = as_list(options.get('COLUMN-HEADERS'))
        alignments = as_list(options.get('COLUMN-ALIGNMENT'))
        alignments += ['middle-left'] * (len(cols) - len(alignments))
        raw_path = options.get('INPUT-FILE', '')
        if isinstance(raw_path, list):
            raw_path = raw_path[0] if raw_path else ''
        rows = self._load_csv_rows(str(raw_path), ctx)
        lines: 'list[str]' = ['<table class="tblin">']
        lines.extend(('<COL>' for _ in cols))
        lines.append('<thead>')
        lines.append("<tr id='colhdr'>")
        lines.extend((f'<th>{h}</th>' for h in headers))
        lines.append('</tr>')
        lines.append('</thead>')
        for idx, row in enumerate(rows):
            cell_class = 'tblin' if idx % 2 == 0 else 'alt'
            lines.append('<tr>')
            for ci, col in enumerate(cols):
                val_str = self._format_cell(col, row.get(col.lower(), ''))
                valign, halign = self._parse_alignment(alignments[ci])
                lines.append(f'<td class="{cell_class}" style="vertical-align:{valign};text-align:{halign};">{val_str}</td>')
            lines.append('</tr>')
        lines.append('<tfoot>')
        lines.append('</tfoot>')
        lines.append('</table>')
        content = '\n'.join(lines)
        top = options.get('AT-TOP-OF-REPORT')
        if top:
            top_str = top if isinstance(top, str) else ' '.join(top)
            content = f'<p class="at-top-of-report">\n{top_str}</p>\n{content}'
        return content

    @staticmethod
    def _parse_alignment(align: 'str') -> 'tuple[str, str]':
        parts = align.split('-')
        if len(parts) >= 2:
            return (parts[0], parts[1])
        return ('middle', parts[0] if parts else 'left')

    @staticmethod
    def _format_cell(col_name: 'str', val: 'Any') -> 'str':
        if val is None:
            return '&nbsp;'
        s = str(val).strip()
        if s == '' or s.lower() == 'nan':
            return '&nbsp;'
        if s.endswith('%'):
            return s
        low = col_name.lower()
        if 'ce%' in low or 'percent' in low:
            try:
                return f'{float(s) * 100:.2f}%'
            except ValueError:
                pass
        return s

    @staticmethod
    def _load_csv_rows(raw_path: 'str', ctx: 'Any') -> 'list[dict[str, Any]]':
        if not raw_path:
            return []
        if ctx and hasattr(ctx, 'macro'):
            path = ctx.macro.resolve_file_path(raw_path)
        else:
            path = resolve_path(raw_path)
        if not (path and path.is_file()):
            return []
        if ctx and hasattr(ctx, 'csv_io') and hasattr(ctx.csv_io, 'iter'):
            source = ctx.csv_io.iter(str(path))
        else:
            with path.open(newline='', encoding='utf-8', errors='replace') as fh:
                source = list(csv.DictReader(fh))
        return [{k.lower(): v for k, v in row.items() if k} for row in source]

    def _resolve_output_filename(self, path: 'str', instance: 'str | None') -> 'str':
        if path and (not path.startswith('email:')):
            return path
        fallback = 'report.html'
        for report in self.deferred_reports.values():
            options = report.get('options')
            if not isinstance(options, dict):
                options = self._parse_options(report.get('template'))
                report['options'] = options
            out = options.get('OUTPUT-FILE')
            if isinstance(out, list):
                out = out[0] if out else None
            if out:
                fallback = out
                break
        instance_id = instance or self.instance
        base = fallback.lower()
        return f'{instance_id}_{base}' if instance_id else base

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

class MailService:
    KEYRING_SERVICE = 'SMTP'
    DEFAULT_SMTP_HOST = 'smtpauth.intel.com'
    DEFAULT_SMTP_PORT = 587

    @classmethod
    def _load_credential(cls) -> 'keyring.credentials.Credential':
        cred = keyring.get_credential(cls.KEYRING_SERVICE, None)
        if cred is None:
            raise RuntimeError(f"No credential found for service '{cls.KEYRING_SERVICE}' in Windows Credential Manager. Add a generic credential with:\n  cmdkey /generic:{cls.KEYRING_SERVICE} /user:<email> /pass:<password>")
        return cred

    def send(self, to: 'str', subject: 'str', body: 'str', attachments: 'list[str] | None'=None, from_addr: 'str | None'=None) -> 'None':
        cred = self._load_credential()
        sender = from_addr or cred.username
        msg = EmailMessage()
        msg['Subject'] = subject
        msg['From'] = sender
        msg['To'] = to
        msg.set_content(self._resolve_body(body))
        for att_path in attachments or []:
            p = Path(att_path)
            if p.exists():
                msg.add_attachment(p.read_bytes(), maintype='application', subtype='octet-stream', filename=p.name)
        try:
            with smtplib.SMTP(self.DEFAULT_SMTP_HOST, self.DEFAULT_SMTP_PORT) as smtp:
                smtp.ehlo()
                smtp.starttls()
                smtp.ehlo()
                smtp.login(cred.username, cred.password)
                smtp.send_message(msg)
        except smtplib.SMTPAuthenticationError as exc:
            raise RuntimeError('SMTP authentication failed. Check the SMTP credential stored in Windows Credential Manager.') from exc
        except (smtplib.SMTPException, OSError) as exc:
            raise RuntimeError(f'SMTP send failed: {exc}') from exc

    @staticmethod
    def _resolve_body(body: 'str') -> 'str':
        path = Path(body)
        if body and path.exists() and path.is_file():
            return path.read_text(encoding='utf-8', errors='replace')
        return body

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

class SmartAppend:

    def append(self, destination: 'str | Path', source: 'str | Path') -> 'None':
        """Create or extend *destination* with source data rows exactly once."""
        source_path = resolve_path(source)
        destination_path = resolve_path(destination, for_write=True)
        destination_path.parent.mkdir(parents=True, exist_ok=True)
        with source_path.open(newline='', encoding='utf-8', errors='replace') as source_fh:
            reader = csv.reader(source_fh)
            header = next(reader, None)
            if header is None:
                return
            write_header = not destination_path.exists() or destination_path.stat().st_size == 0
            with destination_path.open('w' if write_header else 'a', newline='', encoding='utf-8') as destination_fh:
                writer = csv.writer(destination_fh)
                if write_header:
                    writer.writerow(header)
                writer.writerows(reader)

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
def step_0000_html_report(ctx) -> None:
    ctx.html_report.run(instance='22697', prompt_text='Step 1-1. create revision footer', app_server_default='atd_atm.hadoop', template='\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\nType<\\\\>Key<\\\\>COL1<\\\\>COL2<\\\\>COL3<\\\\>COL4<\\\\>COL5<\\\\>COL6<\\\\>COL7<\\\\>COL8\nTYPE<\\\\>CSS<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCSS<\\\\>sqlpathfinder_style_1.css<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nFORMAT<\\\\>Column-Headers<\\\\>background-color:#dbd9c0<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>text-decoration:normal<\\\\>vertical-align:middle\nFORMAT<\\\\>Column-Data<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>text-align:left<\\\\>vertical-align:middle<\\\\>\nFORMAT<\\\\>Column-Alt-Row<\\\\>background-color:#f7f5dc<\\\\>color:#333<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>text-align:left<\\\\>vertical-align:middle<\\\\>\nFORMAT<\\\\>At-Top-of-Report<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:15<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:center<\\\\>vertical-align:middle\nFORMAT<\\\\>At-Top-of-Col1<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>At-Top-of-Col2<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>At-Top-of-Col3<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>JQX-All-IChart-Text<\\\\>background-color:white<\\\\>color:black<\\\\>font-family:Verdana<\\\\>font-size:11<\\\\>font-style:normal<\\\\>font-weight:normal<\\\\>text-align:left<\\\\>vertical-align:middle\nFORMAT<\\\\>COLUMN-BORDER<\\\\>border-color:#cc9<\\\\>border-collapse:collapse<\\\\>border-style:solid<\\\\>border-width:1px<\\\\>border-spacing:4px<\\\\><\\\\><\\\\>')

def step_0001_html_report(ctx) -> None:
    ctx.html_report.layout(ctx, outlook='N', instance='22697', json_only='N', chart_instance='3450', app_server_default='atd_atm.hadoop', template='<table class="tblout"><tr class="tblout"><td class="tblout" valign="top">\n:FILE:revision.htm\n:CSS:sqlpathfinder_style_1.css\n:CSSEMBED:Y\n:RR:NO\n:B:Y\n:EM-A:\n:EM-S:\n:SEC:Y\n:TITLE:revision\n<table class="tblout">\n<tr class="tblout">\n<td class="tblout">\n<p style="text-align: left" style="background-color: white"><i><font face="arial" size="2.66666666666667" color="silver"><script filename><br>- <changes made></font></i>\n</td>\n</tr>\n</table>\n</td><td class="tblout" valign="top">\n<table class="tblout">\n<tr class="tblout"><td class="tblout"></td></tr>\n</table>\n</td></tr></table>')

def step_0002_html_report(ctx) -> None:
    ctx.html_report.delete(instance='22697')

def step_0003_write_file(ctx) -> None:
    ctx.write_file(path='macrotmp.csv', template='\nSfolder,underDEV,useCSR,useMMS\nICMPCS_CWFNCO_CSR_IAM,N,Y,Y')

def step_0004_write_file(ctx) -> None:
    ctx.write_file(path='getcsrsu.bat', template='\n@echo off\nset PriCSR="\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset SecCSR="\\\\KMATSHFS.intel.com\\KMATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset BakCSR="\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\%username%\\Patrol\\*.___"\ncopy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .\nren setsiteparam.___ setsiteparam.exe')

def step_0005_external(ctx) -> None:
    ctx.external.run(argv=['getcsrsu.bat'])

def step_0007_external(ctx) -> None:
    ctx.external.run(argv=['setsiteparam.exe', 'KM', ctx.macro.named('SFOLDER'), ctx.macro.named('UNDERDEV'), ctx.macro.named('USECSR'), ctx.macro.named('USEMMS')])

def step_0008_write_file(ctx) -> None:
    ctx.write_file(path='TT', template='\nSST Rev3g')

def step_0010_fs_delete(ctx) -> None:
    ctx.fs_ops.delete(paths=['macrotmp.csv', 'getcsrsu.bat', 'setsiteparam.exe', 'csrsu.txt'])

def step_0012_rows_in_file(ctx) -> None:
    ctx.macro.set_named('CONFIG', str(ctx.csv_io.row_count('ICMPCS_config.csv')))

def step_0014_email(ctx) -> None:
    ctx.email.send(to=EMAIL_TO, subject='Critical: ICMPCS config file not found - Path: \\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\' + ctx.macro.named('SFOLDER') + '\\KM\\Config', body='')

def step_0016_sqlite_query(ctx) -> None:
    ctx.run_query(sql="\nSELECT /*L10*/  DISTINCT \n          [icmpcs] AS [icmpcs]\n         ,[parameter] AS [parameter]\n         ,Max([value]) AS [value]\n         ,[STARTTS] AS [STARTTS]\n         ,[UTC] AS [UTC]\n         ,[TZONE] AS [TZONE]\n         ,[TZS] AS [TZS]\n         ,[SFOLDER] AS [SFOLDER]\n         ,[FAC] AS [FAC]\n         ,[MARS] AS [MARS]\n         ,[MARSN] AS [MARSN]\n         ,[RIMS] AS [RIMS]\n         ,[EIMS] AS [EIMS]\n         ,[ARIES] AS [ARIES]\n         ,[OASYS] AS [OASYS]\n         ,[MONGO] AS [MONGO]\n         ,[MMS] AS [MMS]\n         ,[MMSI] AS [MMSI]\n         ,[TOOLLOG] AS [TOOLLOG]\n         ,[VFMARS] AS [VFMARS]\n         ,[VFARIES] AS [VFARIES]\n         ,[VFMONGO] AS [VFMONGO]\n         ,[CSRPATH] AS [CSRPATH]\n         ,[MMSPATH] AS [MMSPATH]\n         ,[MIPPATH] AS [MIPPATH]\n         ,[LURL] AS [LURL]\n         ,[IREPOP] AS [IREPOP]\n         ,[UNDERDEV] AS [UNDERDEV]\n         ,[CSRV] AS [CSRV]\n         ,[MMSV] AS [MMSV]\nFROM\n(\nSELECT /*L0*/  \n          a0.[icmpcs] AS [icmpcs]\n         ,a0.[parameter] AS [parameter]\n         ,a0.[value] AS [value]\n         ,'<<<STARTTS>>>' AS [STARTTS]\n         ,'<<<UTC>>>' AS [UTC]\n         ,'<<<TZONE>>>' AS [TZONE]\n         ,'<<<TZS>>>' AS [TZS]\n         ,'<<<SFOLDER>>>' AS [SFOLDER]\n         ,'<<<FAC>>>' AS [FAC]\n         ,'<<<MARS>>>' AS [MARS]\n         ,'<<<MARSN>>>' AS [MARSN]\n         ,'<<<RIMS>>>' AS [RIMS]\n         ,'<<<EIMS>>>' AS [EIMS]\n         ,'<<<ARIES>>>' AS [ARIES]\n         ,'<<<OASYS>>>' AS [OASYS]\n         ,'<<<MONGO>>>' AS [MONGO]\n         ,'<<<MMS>>>' AS [MMS]\n         ,'<<<MMSI>>>' AS [MMSI]\n         ,'<<<TOOLLOG>>>' AS [TOOLLOG]\n         ,'<<<VFMARS>>>' AS [VFMARS]\n         ,'<<<VFARIES>>>' AS [VFARIES]\n         ,'<<<VFMONGO>>>' AS [VFMONGO]\n         ,'<<<CSRPATH>>>' AS [CSRPATH]\n         ,'<<<MMSPATH>>>' AS [MMSPATH]\n         ,'<<<MIPPATH>>>' AS [MIPPATH]\n         ,'<<<LURL>>>' AS [LURL]\n         ,'<<<IREPOP>>>' AS [IREPOP]\n         ,'<<<UNDERDEV>>>' AS [UNDERDEV]\n         ,'<<<CSRV>>>' AS [CSRV]\n         ,'<<<MMSV>>>' AS [MMSV]\nFROM \n[ICMPCS_config] a0\nWHERE\n              a0.[icmpcs] = " + SqliteEngine.global_sql(ICMPCS, False) + ' \n) t /*L0*/\nGROUP BY \n          [icmpcs]\n         ,[parameter]\n         ,[STARTTS]\n         ,[UTC]\n         ,[TZONE]\n         ,[TZS]\n         ,[SFOLDER]\n         ,[FAC]\n         ,[MARS]\n         ,[MARSN]\n         ,[RIMS]\n         ,[EIMS]\n         ,[ARIES]\n         ,[OASYS]\n         ,[MONGO]\n         ,[MMS]\n         ,[MMSI]\n         ,[TOOLLOG]\n         ,[VFMARS]\n         ,[VFARIES]\n         ,[VFMONGO]\n         ,[CSRPATH]\n         ,[MMSPATH]\n         ,[MIPPATH]\n         ,[LURL]\n         ,[IREPOP]\n         ,[UNDERDEV]\n         ,[CSRV]\n         ,[MMSV]\n', output='configsets.csv', reader=SqliteReader(), inputs=['ICMPCS_config.csv'], crosstab={'row_keys': ['icmpcs', 'STARTTS', 'UTC', 'TZONE', 'TZS', 'SFOLDER', 'FAC', 'MARS', 'MARSN', 'RIMS', 'EIMS', 'ARIES', 'OASYS', 'MONGO', 'MMS', 'MMSI', 'TOOLLOG', 'VFMARS', 'VFARIES', 'VFMONGO', 'CSRPATH', 'MMSPATH', 'MIPPATH', 'LURL', 'IREPOP', 'UNDERDEV', 'CSRV', 'MMSV'], 'header_key': 'parameter', 'value_key': 'value'})

def step_0017_rows_in_file(ctx) -> None:
    ctx.macro.set_named('CONFIGSETS', str(ctx.csv_io.row_count('configsets.csv')))

def step_0019_email(ctx) -> None:
    ctx.email.send(to=EMAIL_TO, subject='Alert: Pls check ICMPCS (' + ctx.macro.named('SFOLDER') + ') config file as it contains not equal to 1 row', body='', attachments=['ICMPCS_config.csv', 'configsets.csv'])

def step_0023_write_file(ctx) -> None:
    ctx.write_file(path='CSRVerror.htm', template='\n<!DOCTYPE html>\n<html>\n<body>\n<p>It is detected that you cannot access to CSR depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>CSR Superuser</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong>\nPath: <<<CSRPATH>>></p>\n</body>\n</html>')

def step_0024_email(ctx) -> None:
    ctx.email.send(to=EMAIL_TO, subject='Critical: Cannot access to ' + ctx.macro.named('CSRPATH'), body='CSRVerror.htm')

def step_0027_write_file(ctx) -> None:
    ctx.write_file(path='MMSVerror.htm', template='\n<!DOCTYPE html>\n<html\n<body>\n<p>It is detected that you cannot access to MMS Signal Tracer depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>MMS Signal Tracer Admin</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong><br/>\nPath: <<<MMSPATH>>></p>\n</body>\n</html>')

def step_0028_email(ctx) -> None:
    ctx.email.send(to=EMAIL_TO, subject='Critical: Cannot access to ' + ctx.macro.named('MMSPATH'), body='MMSVerror.htm')

def step_0030_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(src=str(Path('\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\' + ctx.macro.named('SFOLDER') + '\\KM\\HIST') / 'HIST.txt'), dst='.')

def step_0031_rows_in_file(ctx) -> None:
    ctx.macro.set_named('HIST', str(ctx.csv_io.row_count('HIST.txt')))

def step_0033_write_file(ctx) -> None:
    ctx.write_file(path='HIST.csv', template='\nLOT,OUT_DATE\nDUMMY,2000-01-01 00:00:00')

def step_0034_write_file(ctx) -> None:
    ctx.write_file(path='HISTERROR.txt', template='\nERROR\nERROR\nERROR')

def step_0036_fs_copy(ctx) -> None:
    ctx.fs_ops.rename(src='HIST.txt', dst='HIST.csv')

def step_0043_sql_query(ctx) -> None:
    ctx.run_query(sql="\n/*BEGIN SQL*/\nSELECT \n          lot_1 AS lot_1\n         ,operation_1 AS operation_1\n         ,To_Char(out_date,'yyyy-mm-dd hh24:mi:ss') AS out_date\n         ,oldqty1 AS oldqty1\n         ,newqty1 AS newqty1\n         ,Replace(Replace(Replace(Replace(Replace(Replace(Interposer_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Interposer_SLI\n         ,Replace(Replace(Replace(Replace(Replace(Replace(Patch_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Patch_SLI\n         ,prodgroup3_1 AS prodgroup3_1\n         ,entity AS entity\n         ,transaction AS transaction\nFROM\n(\nSELECT  \n          f0.lot AS lot_1\n         ,f0.operation AS operation_1\n         ,f0.out_date AS out_date\n         ,f0.oldqty1 AS oldqty1\n         ,f0.newqty1 AS newqty1\n         ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = " + SqliteEngine.global_sql(ATTRIBUTE_NUMBER, True) + ' AND la.src_erase_date IS NULL AND rownum <= ' + SqliteEngine.global_sql(ROWNUM_MAX, True) + ') AS Interposer_SLI\n         ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = ' + SqliteEngine.global_sql(ATTRIBUTE_NUMBER_2, True) + ' AND la.src_erase_date IS NULL AND rownum <= ' + SqliteEngine.global_sql(ROWNUM_MAX, True) + ") AS Patch_SLI\n         ,p.prodgroup3 AS prodgroup3_1\n         ,f4.entity AS entity\n         ,f5.transaction AS transaction\nFROM \n@[]@.F_LotHist f0\nLEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version\nINNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot\nLEFT JOIN @[]@.F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = " + SqliteEngine.global_sql(UNIQUE_FLAG, False) + "\n AND      f4.entity Like 'IAM%' \nLEFT JOIN @[]@.F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource\nLEFT JOIN @[]@.F_LotTxnHist f5 ON f5.lot = f0.lot AND f5.operation = f0.operation AND f5.prevout_date = f0.prevout_date AND NVL(f5.history_deleted_flag,'N') = 'N'\n AND      f5.transaction = " + SqliteEngine.global_sql(TRANSACTION, False) + " \nWHERE\nNVL(f0.history_deleted_flag,'N') = 'N'\nAND      f0.owner <> " + SqliteEngine.global_sql(OWNER, False) + '\n AND      f0.operation = ' + SqliteEngine.global_sql(OPERATION, False) + " \n AND      f0.out_date >= TRUNC(SYSDATE) - 2 \n AND      p.prodgroup3 Like 'CWF%' \n-- Tail A\n)\nWHERE\n              Interposer_SLI Is Not Null  \n/*END SQL*/\n\n", output='yeuchuan_a1_22697.tab', reader=MarsReader(), header=['lot_1', 'operation_1', 'out_date', 'oldqty1', 'newqty1', 'Interposer_SLI', 'Patch_SLI', 'prodgroup3_1', 'entity', 'transaction'])

def step_0044_sql_query(ctx) -> None:
    ctx.run_query(sql="\n/*BEGIN SQL*/\nSELECT \n          facility AS facility\n         ,operation AS operation\n         ,module_name AS module_name\n         ,tool_entity AS tool_entity\n         ,primary_entity AS primary_entity\n         ,To_Char(processing_start_date,'yyyy-mm-dd hh24:mi:ss') AS processing_start_date\n         ,To_Char(processing_end_date,'yyyy-mm-dd hh24:mi:ss') AS processing_end_date\n         ,lot AS lot\n         ,product AS product\n         ,prodgroup3 AS prodgroup3\n         ,Replace(Replace(Replace(Replace(Replace(Replace(product_desc,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS product_desc\n         ,owner AS owner\n         ,visual_id AS visual_id\n         ,ws_loss_code AS ws_loss_code\n         ,media_in_x AS media_in_x\n         ,media_in_y AS media_in_y\n         ,parameter AS parameter\n         ,Max(numeric_value) AS numeric_value\nFROM\n(\nSELECT  \n          bams0.facility AS facility\n         ,bams0.operation AS operation\n         ,bams0.module_name AS module_name\n         ,bams0.tool_entity AS tool_entity\n         ,bams0.primary_entity AS primary_entity\n         ,bams0.processing_start_time AS processing_start_date\n         ,bams0.processing_end_time AS processing_end_date\n         ,bams0.lot AS lot\n         ,ml.product AS product\n         ,mp.prodgroup3 AS prodgroup3\n         ,mp.product_description AS product_desc\n         ,ml.owner AS owner\n         ,bams2.visual_id AS visual_id\n         ,bams2.ws_loss_code AS ws_loss_code\n         ,bams2.media_in_x AS media_in_x\n         ,bams2.media_in_y AS media_in_y\n         ,bams3.parameter AS parameter\n         ,bams3.numeric_value AS numeric_value\nFROM \nARIES_Views.AV_BAMS_SESSION bams0\nLEFT JOIN A_MARS_Lot ml ON bams0.lot=ml.lot\nLEFT JOIN A_MARS_Product mp ON ml.product = mp.product AND ml.mars_schema=mp.mars_schema AND mp.facility=bams0.facility\nINNER JOIN ARIES_Views.AV_BAMS_MEDIA_TESTING bams1 ON bams1.lao_start_ww = bams0.lao_start_ww AND bams1.obj_s_id = bams0.obj_s_id\nINNER JOIN ARIES_Views.AV_BAMS_UNIT_TESTING bams2 ON bams2.lao_start_ww = bams1.lao_start_ww AND bams2.obj_s_id = bams1.obj_s_id AND bams2.obj_mt_id = bams1.obj_mt_id\nLEFT JOIN ARIES_Views.AV_BAMS_DEVICE_RESULTS bams3 ON bams3.lao_start_ww = bams2.lao_start_ww AND bams3.obj_s_id = bams2.obj_s_id AND bams3.obj_mt_id = bams2.obj_mt_id AND bams3.obj_ut_id = bams2.obj_ut_id\nWHERE\n              (bams0.lot In \n" + ctx.csv_io.sql_get_csv_list('.\\yeuchuan_a1_22697.tab', 'lot_1', 'bams0.lot In') + ')' + ' \n AND      bams0.operation = ' + SqliteEngine.global_sql(OPERATION, False) + ' \n)\nGROUP BY \n          facility\n         ,operation\n         ,module_name\n         ,tool_entity\n         ,primary_entity\n         ,processing_start_date\n         ,processing_end_date\n         ,lot\n         ,product\n         ,prodgroup3\n         ,product_desc\n         ,owner\n         ,visual_id\n         ,ws_loss_code\n         ,media_in_x\n         ,media_in_y\n         ,parameter\n/*END SQL*/\n\n', output='yeuchuan_a0_22697.tab', reader=AriesReader(), crosstab={'row_keys': ['facility', 'operation', 'module_name', 'tool_entity', 'primary_entity', 'processing_start_date', 'processing_end_date', 'lot', 'product', 'prodgroup3', 'product_desc', 'owner', 'visual_id', 'ws_loss_code', 'media_in_x', 'media_in_y'], 'header_key': 'parameter', 'value_key': 'numeric_value'})

def step_0045_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""

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
    """, output='PARMI_IPM_RAW.csv', reader=SqliteReader(), inputs=['yeuchuan_a1_22697.tab', 'yeuchuan_a0_22697.tab'])

def step_0046_sqlite_query(ctx) -> None:
    ctx.run_query(sql="\n\nDROP TABLE IF EXISTS T_L0_Init;\nCREATE TABLE T_L0_Init AS\nSELECT /*L0*/  \n          a0.[lot_1] AS [lot_1]\n         ,a0.[newqty1] AS [newqty1]\n         ,a0.[facility] AS [facility]\n         ,a0.[operation] AS [operation]\n         ,a0.[tool_entity] AS [tool_entity]\n         ,a0.[primary_entity] AS [primary_entity]\n         ,a0.[processing_end_date] AS [processing_end_date]\n         ,a0.[lot] AS [lot]\n         ,a0.[prodgroup3] AS [prodgroup3]\n         ,a0.[product] AS [product]\n         ,a0.[visual_id] AS [visual_id]\n         ,a0.[ws_loss_code] AS [ws_loss_code]\n         ,a0.[media_in_x] AS [media_in_x]\n         ,a0.[media_in_y] AS [media_in_y]\n         ,a0.[height] AS [height]\n         ,a0.[patch_lift_roi1] AS [patch_lift_roi1]\n         ,a0.[patch_lift_roi2] AS [patch_lift_roi2]\n         ,a0.[patch_lift_roi3] AS [patch_lift_roi3]\n         ,a0.[patch_lift_roi4] AS [patch_lift_roi4]\n         ,a0.[patch_lift_roi5] AS [patch_lift_roi5]\n         ,a0.[patch_lift_roi6] AS [patch_lift_roi6]\n         ,a0.[patch_lift_roi7] AS [patch_lift_roi7]\n         ,a0.[patch_lift_roi8] AS [patch_lift_roi8]\n         ,a0.[patch_lift_roi_max] AS [patch_lift_roi_max]\n         ,a0.[patch_sli] AS [patch_sli]\n         ,a0.[interposer_sli] AS [interposer_sli]\n         ,CASE  WHEN a0.[patch_lift_roi4]  >= 2000 AND  a0.[patch_lift_roi_max]  >= 2030 THEN '1' WHEN a0.[patch_lift_roi8] >= 2000 AND  a0.[patch_lift_roi_max]  >= 2030 THEN '1' ELSE '0' END AS [NCO_Risk]\nFROM \n[PARMI_IPM_RAW] a0\n;\n\nDROP TABLE IF EXISTS T_L0_1_1;\nCREATE TABLE T_L0_1_1 AS\nSELECT COUNT(DISTINCT  visual_id) AS AF$S1\n,lot_1 AS AF$PB1\nFROM T_L0_Init GROUP BY \nAF$PB1\n;\nCREATE INDEX T_L0_1_1_Idx ON T_L0_1_1 (AF$PB1);\nDROP TABLE IF EXISTS T_L0_1_Result;\nCREATE TABLE T_L0_1_Result AS\nSELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [VIDCount]\nFROM T_L0_Init a0 LEFT JOIN T_L0_1_1 a1 ON \nlot_1 = a1.AF$PB1\n;\nDROP TABLE IF EXISTS T_L0_1_1;\n\nCREATE INDEX T_L0_1_Result_Idx ON T_L0_1_Result (orig_rowid);\nDROP TABLE IF EXISTS T_L0_Result;\nCREATE TABLE T_L0_Result AS\nSELECT\n\n[lot_1]\n,[newqty1]\n,[facility]\n,[operation]\n,[tool_entity]\n,[primary_entity]\n,[processing_end_date]\n,[lot]\n,[prodgroup3]\n,[product]\n,[visual_id]\n,[ws_loss_code]\n,[media_in_x]\n,[media_in_y]\n,[height]\n,[patch_lift_roi1]\n,[patch_lift_roi2]\n,[patch_lift_roi3]\n,[patch_lift_roi4]\n,[patch_lift_roi5]\n,[patch_lift_roi6]\n,[patch_lift_roi7]\n,[patch_lift_roi8]\n,[patch_lift_roi_max]\n,[patch_sli]\n,[interposer_sli]\n,[NCO_Risk]\n,[VIDCount]\nFROM T_L0_Init a0\nLEFT JOIN T_L0_1_Result a1 ON a0.rowid = a1.orig_rowid\n;\nDROP TABLE IF EXISTS T_L0_1_Result;\nDROP TABLE IF EXISTS T_L0_Init;\n\nSELECT /*L3*/ \n          [lot_1] AS [lot_1]\n         ,[newqty1] AS [newqty1]\n         ,[facility] AS [facility]\n         ,[operation] AS [operation]\n         ,[tool_entity] AS [tool_entity]\n         ,[primary_entity] AS [primary_entity]\n         ,[processing_end_date] AS [processing_end_date]\n         ,[lot] AS [lot]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[product] AS [product]\n         ,[visual_id] AS [visual_id]\n         ,[ws_loss_code] AS [ws_loss_code]\n         ,[media_in_x] AS [media_in_x]\n         ,[media_in_y] AS [media_in_y]\n         ,[height] AS [height]\n         ,[patch_lift_roi1] AS [patch_lift_roi1]\n         ,[patch_lift_roi2] AS [patch_lift_roi2]\n         ,[patch_lift_roi3] AS [patch_lift_roi3]\n         ,[patch_lift_roi4] AS [patch_lift_roi4]\n         ,[patch_lift_roi5] AS [patch_lift_roi5]\n         ,[patch_lift_roi6] AS [patch_lift_roi6]\n         ,[patch_lift_roi7] AS [patch_lift_roi7]\n         ,[patch_lift_roi8] AS [patch_lift_roi8]\n         ,[patch_lift_roi_max] AS [patch_lift_roi_max]\n         ,[patch_sli] AS [patch_sli]\n         ,[interposer_sli] AS [interposer_sli]\n         ,[NCO_Risk] AS [NCO_Risk]\n         ,[VIDCount] AS [VIDCount]\n         ,[FlagLot] AS [FlagLot]\nFROM\n(\nSELECT /*L2*/ \n          [lot_1] AS [lot_1]\n         ,[newqty1] AS [newqty1]\n         ,[facility] AS [facility]\n         ,[operation] AS [operation]\n         ,[tool_entity] AS [tool_entity]\n         ,[primary_entity] AS [primary_entity]\n         ,[processing_end_date] AS [processing_end_date]\n         ,[lot] AS [lot]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[product] AS [product]\n         ,[visual_id] AS [visual_id]\n         ,[ws_loss_code] AS [ws_loss_code]\n         ,[media_in_x] AS [media_in_x]\n         ,[media_in_y] AS [media_in_y]\n         ,[height] AS [height]\n         ,[patch_lift_roi1] AS [patch_lift_roi1]\n         ,[patch_lift_roi2] AS [patch_lift_roi2]\n         ,[patch_lift_roi3] AS [patch_lift_roi3]\n         ,[patch_lift_roi4] AS [patch_lift_roi4]\n         ,[patch_lift_roi5] AS [patch_lift_roi5]\n         ,[patch_lift_roi6] AS [patch_lift_roi6]\n         ,[patch_lift_roi7] AS [patch_lift_roi7]\n         ,[patch_lift_roi8] AS [patch_lift_roi8]\n         ,[patch_lift_roi_max] AS [patch_lift_roi_max]\n         ,[patch_sli] AS [patch_sli]\n         ,[interposer_sli] AS [interposer_sli]\n         ,[NCO_Risk] AS [NCO_Risk]\n         ,[VIDCount] AS [VIDCount]\n         ,CASE WHEN  [NCO_Risk]  = '1' OR  [VIDCount] < 2 THEN '1' ELSE '0' END AS [FlagLot]\nFROM\n(\nSELECT /*L1*/ \n          [lot_1] AS [lot_1]\n         ,[newqty1] AS [newqty1]\n         ,[facility] AS [facility]\n         ,[operation] AS [operation]\n         ,[tool_entity] AS [tool_entity]\n         ,[primary_entity] AS [primary_entity]\n         ,[processing_end_date] AS [processing_end_date]\n         ,[lot] AS [lot]\n         ,[prodgroup3] AS [prodgroup3]\n         ,[product] AS [product]\n         ,[visual_id] AS [visual_id]\n         ,[ws_loss_code] AS [ws_loss_code]\n         ,[media_in_x] AS [media_in_x]\n         ,[media_in_y] AS [media_in_y]\n         ,[height] AS [height]\n         ,[patch_lift_roi1] AS [patch_lift_roi1]\n         ,[patch_lift_roi2] AS [patch_lift_roi2]\n         ,[patch_lift_roi3] AS [patch_lift_roi3]\n         ,[patch_lift_roi4] AS [patch_lift_roi4]\n         ,[patch_lift_roi5] AS [patch_lift_roi5]\n         ,[patch_lift_roi6] AS [patch_lift_roi6]\n         ,[patch_lift_roi7] AS [patch_lift_roi7]\n         ,[patch_lift_roi8] AS [patch_lift_roi8]\n         ,[patch_lift_roi_max] AS [patch_lift_roi_max]\n         ,[patch_sli] AS [patch_sli]\n         ,[interposer_sli] AS [interposer_sli]\n         ,[NCO_Risk] AS [NCO_Risk]\n         ,[VIDCount] AS [VIDCount]\nFROM\n(\nT_L0_Result\n)\n) t /*L1*/\n) t /*L2*/\nWHERE\n              [FlagLot] = " + SqliteEngine.global_sql(FLAGLOT, False) + ' \n;\n', output='IPM_Data.csv', reader=SqliteReader(), inputs=['PARMI_IPM_RAW.csv'], header=['lot_1', 'newqty1', 'facility', 'operation', 'tool_entity', 'primary_entity', 'processing_end_date', 'lot', 'prodgroup3', 'product', 'visual_id', 'ws_loss_code', 'media_in_x', 'media_in_y', 'height', 'patch_lift_roi1', 'patch_lift_roi2', 'patch_lift_roi3', 'patch_lift_roi4', 'patch_lift_roi5', 'patch_lift_roi6', 'patch_lift_roi7', 'patch_lift_roi8', 'patch_lift_roi_max', 'patch_sli', 'interposer_sli', 'NCO_Risk', 'VIDCount', 'FlagLot'])

def step_0047_sqlite_query(ctx) -> None:
    ctx.run_query(sql='\nSELECT /*L0*/  DISTINCT \n          a0.[lot] AS [Lot_NCORisk]\nFROM \n[IPM_Data] a0\nWHERE\n NOT          (a0.[lot] In \n' + ctx.csv_io.sql_get_csv_list('.\\HIST.csv', 1, 'a0.[lot] In') + ')' + '\n', output='DATA.csv', reader=SqliteReader(), inputs=['IPM_Data.csv'], header=['Lot_NCORisk'])

def step_0048_rows_in_file(ctx) -> None:
    ctx.macro.set_named('SIGNAL', str(ctx.csv_io.row_count('data.csv')))

def step_0051_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""
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


    """, output='RESULT_<<<%username%>>>_CWFNCO{TS}.csv', reader=SqliteReader(), inputs=[('data.csv', 'T0')], header=['HOLD_LOT', 'AUTO_CONTAIN', 'LOT', 'EMAIL', 'CCB_NUMBER', 'HOLD_NOTE', 'HOLDCATEGORY', 'Module'])

def step_0052_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(src=str(Path('.') / 'RESULT_*.csv'), dst=ctx.macro.named('CSRPATH'))

def step_0054_html_report(ctx) -> None:
    ctx.html_report.defer(instance='22697', id='MYREPORT3', prompt_text='Step 6-6. sending notification', app_server_default='atd_atm.hadoop', template='\n\n\nType<\\\\>Key<\\\\>COL1<\\\\>COL2<\\\\>COL3<\\\\>COL4<\\\\>COL5<\\\\>COL6<\\\\>COL7<\\\\>COL8<\\\\>COL9<\\\\>COL10<\\\\>COL11<\\\\>COL12<\\\\>COL13<\\\\>COL14<\\\\>COL15<\\\\>COL16<\\\\>COL17<\\\\>COL18<\\\\>COL19<\\\\>COL20<\\\\>COL21<\\\\>COL22<\\\\>COL23<\\\\>COL24<\\\\>COL25<\\\\>COL26\nTYPE<\\\\>HTML<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nINPUT-FILE<\\\\>IPM_Data.csv<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nOUTPUT-FILE<\\\\>SQLPathFinder.htm<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCSS<\\\\>sqlpathfinder_style_1.css<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLSPAN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDRILLDOWN<\\\\>N<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDYNAMICSORT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nDYNAMICFILTER<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nATTOPDRILLDOWN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nNOPREPROCESS<\\\\>Y<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nAT-TOP-OF-REPORT<\\\\><\\\\>CWF_MLINCO_LOTHOLD<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>\nCOLUMN-DATA<\\\\><\\\\>facility<\\\\>operation<\\\\>tool_entity<\\\\>primary_entity<\\\\>processing_end_date<\\\\>lot<\\\\>prodgroup3<\\\\>product<\\\\>visual_id<\\\\>ws_loss_code<\\\\>media_in_x<\\\\>media_in_y<\\\\>height<\\\\>patch_lift_roi1<\\\\>patch_lift_roi2<\\\\>patch_lift_roi3<\\\\>patch_lift_roi4<\\\\>patch_lift_roi5<\\\\>patch_lift_roi6<\\\\>patch_lift_roi7<\\\\>patch_lift_roi8<\\\\>patch_lift_roi_max<\\\\>lot_1<\\\\>patch_sli<\\\\>interposer_sli<\\\\>nco_risk\nCOLUMN-HEADERS<\\\\><\\\\>Facility<\\\\>Operation<\\\\>Tool Entity<\\\\>Primary Entity<\\\\>Processing End Date<\\\\>Lot<\\\\>Prodgroup3<\\\\>Product<\\\\>Visual Id<\\\\>Ws Loss Code<\\\\>Media In X<\\\\>Media In Y<\\\\>Height<\\\\>Patch Lift Roi1<\\\\>Patch Lift Roi2<\\\\>Patch Lift Roi3<\\\\>Patch Lift Roi4<\\\\>Patch Lift Roi5<\\\\>Patch Lift Roi6<\\\\>Patch Lift Roi7<\\\\>Patch Lift Roi8<\\\\>Patch Lift Roi Max<\\\\>Lot 1<\\\\>Patch Sli<\\\\>Interposer Sli<\\\\>Nco Risk\nCOLUMN-ALIGNMENT<\\\\><\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left\nCOLUMN-FORMAT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>')

def step_0055_html_report(ctx) -> None:
    ctx.html_report.layout(ctx, outlook='N', instance='22697', json_only='N', chart_instance='30819', app_server_default='atd_atm.hadoop', template='<table class="tblout"><tr class="tblout"><td class="tblout" valign="top">\n:FILE:EMAIL:<<<dEmail>>>\n:CSS:sqlpathfinder_style_1.css\n:CSSEMBED:Y\n:RR:NO\n:B:Y\n:EM-A:\n:EM-S:\n:SEC:Y\n:TITLE:CWF_MLINCO_LOTHOLD\n<table class="tblout">\n<tr class="tblout">\n<td class="tblout">\n<p style="text-align: left" style="background-color: white"><font face="intel clear" size="3.66666666666667" color="black">Lot on hold due to suspected MLI NCO. Lot will need to go through 100% XRAY (2846)</font>\n</td>\n</tr>\n<tr class="tblout">\n<td class="tblout">\nHTM:MYREPORT3\n</td>\n</tr>\n<tr class="tblout">\n<td class="tblout">\n<p style="text-align: left" style="background-color: white"><font face="intel clear" size="3.66666666666667" color="black"><strong>Configurations applied</strong><br>CSR enabled: <<<DCSR>>><br>ICMPCS MMS Workflow enabled: <<<DMWF>>><br>MMS Signal Tracer enabled: <<<DMST>>></font>\n</td>\n</tr>\n<tr class="tblout">\n<td class="tblout">\nIHJ:revision.htm\n</td>\n</tr>\n</table>\n</td><td class="tblout" valign="top">\n<table class="tblout">\n<tr class="tblout"><td class="tblout"></td></tr>\n</table>\n</td></tr></table>')

def step_0056_html_report(ctx) -> None:
    ctx.html_report.delete(instance='22697')

def step_0057_rows_in_file(ctx) -> None:
    ctx.macro.set_named('HISTERR', str(ctx.csv_io.row_count('HISTERROR.txt')))

def step_0059_smart_append(ctx) -> None:
    ctx.smart_append.append('HIST.csv', 'data.csv')

def step_0060_fs_copy(ctx) -> None:
    ctx.fs_ops.rename(src='HIST.csv', dst='HIST.txt')

def step_0061_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(src=str(Path('.') / 'HIST.txt'), dst=ctx.macro.named('IREPOP') + '\\' + ctx.macro.named('SFOLDER') + '\\KM\\HIST')

def step_0064_rows_in_file(ctx) -> None:
    ctx.macro.set_named('SIGNAL', str(ctx.csv_io.row_count('data.csv')))

def step_0067_write_file(ctx) -> None:
    ctx.write_file(path='spfheaderfile.csv', template='DATE,EMAIL,MODULE,SIGNALTYPE,COMPONENT_ID,HOLD_LOT,AUTO_CONTAIN,SHOULD_EMAIL,MMS,ALARMTYPE,SUB_ENTITY_1,SUB_ENTITY_2,SUB_ENTITY_3,FACILITY,PRODGROUP3,ENTITY,DEFECT_SIDE,DEFECT_MODE,LOT_OWNR,IMPACT_LOT_VI,IMPACT_VIDS\n')

def step_0068_sqlite_query(ctx) -> None:
    ctx.run_query(sql="""
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

    """, output='SPFMMSTMP.csv', reader=SqliteReader(), inputs=[('IPM_Data.csv', 'T0')], header=['MODULE', 'SIGNALTYPE', 'ALARMTYPE', 'EMAIL', 'HOLD_LOT', 'AUTO_CONTAIN', 'PRODGROUP3', 'IMPACT_LOT_VI', 'IMPACT_VIDS'])

def step_0069_smart_append(ctx) -> None:
    ctx.smart_append.append('spfheaderfile.csv', 'SPFMMSTMP.csv')

def step_0070_fs_copy(ctx) -> None:
    ctx.fs_ops.rename(src='spfheaderfile.csv', dst='<TS>_' + ctx.macro.named('%USERNAME%') + '_output.pickle.csv')

def step_0071_fs_delete(ctx) -> None:
    ctx.fs_ops.delete(paths=['SPFMMSTMP.csv'])

def step_0072_fs_copy(ctx) -> None:
    ctx.fs_ops.copy(src=str(Path('.') / '*_output.pickle.csv'), dst=ctx.macro.named('MIPPATH'))

def step_0075_write_file(ctx) -> None:
    ctx.write_file(path='update.bat', template='\n@echo off\nset currd=%date:~10,4%-%date:~4,2%-%date:~7,2%\nset currms=%time:~2,6%\nset /a currh=%time:~0,2%\nif %currh% LSS 10 set currh=0%currh%\nset currt=%currh%%currms%\nset PriConfig=<<<IREPOP>>>\\<<<SFOLDER>>>\n\n\nREM ************** edit here only if needed **************\n\nset record=<<<DCSR>>>,<<<DMST>>>,<<<DMWF>>>\n\n\n\n\nREM *************** do not edit below here ***************\n\ncall :retry\ngoto:eof\n\n:retry\nset /a tries=300\n\n:loop\nif %tries% LEQ 0 goto return\necho <<<STARTTS>>>,%currd% %currt%,<<<UTC>>>,KM,<<<%username%>>>,<<<UNDERDEV>>>,%record%>>%PriConfig%\\VF_CE.txt && goto return || set /a tries-=1 && ping -n 1 127.0.0.1>nul 2>&1 && goto loop\n\n:return\n@exit /B')

def step_0076_external(ctx) -> None:
    ctx.external.run(argv=['update.bat'])

def step_0077_fs_delete(ctx) -> None:
    ctx.fs_ops.delete(paths=['update.bat'])

# <vg2c:steps:end>

# <vg2c:workflow:start>
def run() -> None:
    Logger.basicConfig(level=Logger.INFO)
    OracleClient.configure()
    ctx = PipelineContext({'crosstab': CrosstabUtility(), 'csv_io': CsvIO(), 'email': MailService(), 'external': ExternalProcess(), 'fs_ops': FileSystemOps(), 'html_report': HtmlReport(), 'macro': MacroState(), 'smart_append': SmartAppend()})
    step_0000_html_report(ctx)
    step_0001_html_report(ctx)
    step_0002_html_report(ctx)
    step_0003_write_file(ctx)
    step_0004_write_file(ctx)
    step_0005_external(ctx)
    with ctx.macro.scope(ctx.csv_io.single_row('macrotmp.csv')):
        step_0007_external(ctx)
        step_0008_write_file(ctx)
    step_0010_fs_delete(ctx)
    with ctx.macro.scope(ctx.csv_io.single_row('ctime.csv')):
        step_0012_rows_in_file(ctx)
        if Logger.condition('/PROMPT-TEXT=Step 1-12. TRUE if config file not found', int(ctx.macro.named('CONFIG')) <= int('0')):
            step_0014_email(ctx)
        else:
            step_0016_sqlite_query(ctx)
            step_0017_rows_in_file(ctx)
            if Logger.condition('/PROMPT-TEXT=Step 1-16. TRUE if config file converted output >1 row', int(ctx.macro.named('CONFIGSETS')) != int('1')):
                step_0019_email(ctx)
            else:
                with ctx.macro.scope(ctx.csv_io.single_row('configsets.csv')):
                    if Logger.condition('/PROMPT-TEXT=Step 1-19. Validate CSR accessibility', ctx.macro.named('CSRV') == 'FAIL' and ctx.macro.named('UNDERDEV') == 'N'):
                        step_0023_write_file(ctx)
                        step_0024_email(ctx)
                    if Logger.condition('/PROMPT-TEXT=Step 1-22. Validate MMS accessibility', ctx.macro.named('MMSV') == 'FAIL' and ctx.macro.named('UNDERDEV') == 'N'):
                        step_0027_write_file(ctx)
                        step_0028_email(ctx)
                    step_0030_fs_copy(ctx)
                    step_0031_rows_in_file(ctx)
                    if Logger.condition('/PROMPT-TEXT=Step 1-27. TRUE if HIST.txt is not found', int(ctx.macro.named('HIST')) <= int('0')):
                        step_0033_write_file(ctx)
                        step_0034_write_file(ctx)
                    else:
                        step_0036_fs_copy(ctx)
    with ctx.macro.scope(ctx.csv_io.single_row('configsets.csv')):
        step_0043_sql_query(ctx)
        step_0044_sql_query(ctx)
        step_0045_sqlite_query(ctx)
        step_0046_sqlite_query(ctx)
        step_0047_sqlite_query(ctx)
        step_0048_rows_in_file(ctx)
        if Logger.condition('/PROMPT-TEXT=Step 6-2. TRUE if signal is found', int(ctx.macro.named('SIGNAL')) > int('0')):
            if Logger.condition('/PROMPT-TEXT=Step 6-3. TRUE if CSR is enabled', ctx.macro.named('DCSR') == 'Y'):
                step_0051_sqlite_query(ctx)
                step_0052_fs_copy(ctx)
            step_0054_html_report(ctx)
            step_0055_html_report(ctx)
            step_0056_html_report(ctx)
            step_0057_rows_in_file(ctx)
            if Logger.condition('/PROMPT-TEXT=Step 6-8. TRUE if HISTERROR.txt is not found', int(ctx.macro.named('HISTERR')) <= int('0')):
                step_0059_smart_append(ctx)
                step_0060_fs_copy(ctx)
                step_0061_fs_copy(ctx)
        step_0064_rows_in_file(ctx)
        if Logger.condition('/PROMPT-TEXT=Step 7-2. TRUE if signal is found', int(ctx.macro.named('SIGNAL')) > int('0')):
            if Logger.condition('/PROMPT-TEXT=Step 7-3. TRUE if ICMPCS MMS Workflow is enabled', ctx.macro.named('DMWF') == 'Y'):
                step_0067_write_file(ctx)
                step_0068_sqlite_query(ctx)
                step_0069_smart_append(ctx)
                step_0070_fs_copy(ctx)
                step_0071_fs_delete(ctx)
                step_0072_fs_copy(ctx)
        step_0075_write_file(ctx)
        step_0076_external(ctx)
        step_0077_fs_delete(ctx)
# <vg2c:workflow:end>

if __name__ == "__main__":
    run()