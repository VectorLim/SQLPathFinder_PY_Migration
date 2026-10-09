"""Read an editable SQL asset and execute it with explicit ownership."""

from __future__ import annotations

import inspect
import re

from vg2c.runtime.csv_io import _CsvIO as CsvIO
from vg2c.runtime.crosstab import _CrosstabUtility as CrosstabUtility
from vg2c.runtime.sqlite_reader import _SqliteReader as SqliteReader
from vg2c.runtime.sql_text import scan_sql_get_csv_list_calls
from vg2c.runtime.values import job_path, substitute


def node_site(node: str) -> str:
    """Use the existing dispatch rule: the first literal node component is the site."""
    match = re.match(r"^([A-Za-z][A-Za-z0-9_]*)(?:\.|$)", node.strip())
    if match is None:
        raise ValueError(f"Cannot resolve backend site from node {node!r}")
    return match.group(1)


def execute_sql(path, *, reader, output, workdir, values=None, macros=None,
                inputs=None, header=None, crosstab=None, node=None, params=None):
    """Reread the asset on every call; routing is separate from SQL binds."""
    path = job_path(path, workdir)
    with path.open(encoding="utf-8", newline="") as stream:
        sql = stream.read()
    try:
        return run_query(sql, reader=reader, output=output, workdir=workdir,
                         values=values, macros=macros, inputs=inputs, header=header,
                         crosstab=crosstab, node=node, params=params)
    except (ValueError, RuntimeError) as exc:
        raise type(exc)(f"{path}: {exc}") from exc


def run_query(sql, *, reader, output, workdir, values=None, macros=None,
              inputs=None, header=None, crosstab=None, node=None, params=None):
    """Single query body extracted from PipelineContext.run_query."""
    sql = substitute(sql, values=values, macros=macros)
    calls = scan_sql_get_csv_list_calls(sql)
    for call in reversed(calls):
        replacement = CsvIO(workdir=workdir).sql_get_csv_list(
            call.source_path, call.column_ref, call.lead_in
        )
        if call.needs_closing_paren:
            replacement += ")"
        sql = sql[:call.start] + replacement + sql[call.end:]
    method = getattr(reader, "execute", None) or reader.read
    if params:
        if "params" not in inspect.signature(method).parameters:
            raise ValueError(f"{type(reader).__name__} does not support SQL binds")
        if len(SqliteReader._split_statements(sql)) != 1:
            raise ValueError("SQL binds require exactly one statement")
    kwargs = {"params": params} if params else {}
    if isinstance(reader, SqliteReader):
        kwargs["workdir"] = workdir
    if hasattr(reader, "execute"):
        resolved_inputs = [
            (str(job_path(substitute(item[0], values=values, macros=macros), workdir)), item[1]) if isinstance(item, tuple)
            else str(job_path(substitute(item, values=values, macros=macros), workdir)) for item in (inputs or [])
        ]
        result = reader.execute(sql, resolved_inputs, **kwargs)
    else:
        if not node:
            raise ValueError("An external reader requires an explicit node/site")
        try:
            result = reader.read(site=node_site(substitute(node, values=values, macros=macros)), query=sql, **kwargs)
        finally:
            if type(reader).__module__.startswith("datasyncx"):
                from vg2c.runtime.oracle_client import _OracleClient as OracleClient
                OracleClient.log_active_client()
        result.columns = [col.lower() for col in result.columns]
    if crosstab:
        resolved_crosstab = {
            key: [substitute(item, values=values, macros=macros) if isinstance(item, str) else item for item in value]
            if isinstance(value, list) else substitute(value, values=values, macros=macros)
            if isinstance(value, str) else value for key, value in crosstab.items()
        }
        result = CrosstabUtility().apply(result, **resolved_crosstab)
    if header:
        header = [substitute(item, values=values, macros=macros) for item in header]
    CsvIO(workdir=workdir).write(str(job_path(substitute(str(output), values=values, macros=macros), workdir)),
                  result, header=header)
    return result
