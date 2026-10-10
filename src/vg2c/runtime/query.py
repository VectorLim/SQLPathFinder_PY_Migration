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
                inputs=None, header=None, crosstab=None, node=None, params=None,
                pivot_columns=None, pivot_values=None, pivot_duplicate="first",
                pivot_missing="", pivot_dot=False, pivot_sort=None,
                pivot_header_ref=None, pivot_legacy_headers=False):
    """Reread the asset on every call; routing is separate from SQL binds."""
    path = job_path(path, workdir)
    with path.open(encoding="utf-8", newline="") as stream:
        sql = stream.read()
    try:
        return run_query(sql, reader=reader, output=output, workdir=workdir,
                         values=values, macros=macros, inputs=inputs, header=header,
                         crosstab=crosstab, node=node, params=params,
                         pivot_columns=pivot_columns, pivot_values=pivot_values,
                         pivot_duplicate=pivot_duplicate, pivot_missing=pivot_missing,
                         pivot_dot=pivot_dot, pivot_sort=pivot_sort,
                         pivot_header_ref=pivot_header_ref,
                         pivot_legacy_headers=pivot_legacy_headers)
    except (ValueError, RuntimeError) as exc:
        raise type(exc)(f"{path}: {exc}") from exc


def run_query(sql, *, reader, output, workdir, values=None, macros=None,
              inputs=None, header=None, crosstab=None, node=None, params=None,
              pivot_columns=None, pivot_values=None, pivot_duplicate="first",
              pivot_missing="", pivot_dot=False, pivot_sort=None,
              pivot_header_ref=None, pivot_legacy_headers=False):
    """Single query body extracted from PipelineContext.run_query."""
    configured_pivot = pivot_columns is not None or pivot_values is not None
    if crosstab is not None and (configured_pivot or pivot_header_ref is not None):
        raise ValueError("Cannot specify both legacy crosstab and new pivot arguments")
    if (pivot_columns is None) != (pivot_values is None):
        raise ValueError("pivot_columns and pivot_values must be supplied together")
    if not configured_pivot and (
        pivot_header_ref is not None or pivot_sort is not None
        or pivot_missing != "" or pivot_dot or pivot_legacy_headers
        or pivot_duplicate != "first"
    ):
        raise ValueError("Pivot modifiers require pivot_columns and pivot_values")
    if crosstab is not None:
        if not isinstance(crosstab, dict):
            raise ValueError("Legacy crosstab must be a mapping")
        keys = ("row_keys", "header_key", "value_key")
        if any(key not in crosstab for key in keys):
            raise ValueError("Legacy crosstab requires row_keys, header_key, value_key")
        unknown = sorted(set(crosstab) - set(keys))
        if unknown:
            raise ValueError(
                f"Unsupported legacy crosstab configuration fields: {unknown}. "
                "Use pivot_columns/pivot_values for ScriptHost-style options."
            )
        if (not isinstance(crosstab["row_keys"], list)
                or not all(isinstance(k, str) and k for k in crosstab["row_keys"])
                or not all(isinstance(crosstab[k], str) and crosstab[k]
                           for k in ("header_key", "value_key"))):
            raise ValueError("Legacy crosstab keys must be strings; row_keys a string list")
    if header is not None and (
        not isinstance(header, list)
        or not all(isinstance(item, str) for item in header)
    ):
        raise ValueError("SQL header must be a list of strings")
    sql = substitute(sql, values=values, macros=macros)
    sql = CrosstabUtility.substitute_header_lists(sql, workdir)
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
    if crosstab is not None:
        resolved_crosstab = {
            key: [substitute(item, values=values, macros=macros) if isinstance(item, str) else item for item in value]
            if isinstance(value, list) else substitute(value, values=values, macros=macros)
            if isinstance(value, str) else value for key, value in crosstab.items()
        }
        result = CrosstabUtility().apply(result, **resolved_crosstab)
    elif configured_pivot:
        pivot_columns = substitute(pivot_columns, values=values, macros=macros)
        if isinstance(pivot_values, list):
            pivot_values = [substitute(item, values=values, macros=macros)
                            for item in pivot_values]
        else:
            pivot_values = substitute(pivot_values, values=values, macros=macros)
        result = CrosstabUtility().apply(
            result, pivot_columns=pivot_columns, pivot_values=pivot_values,
            duplicate=pivot_duplicate, missing=pivot_missing, dot=pivot_dot,
            sort=pivot_sort, legacy_headers=pivot_legacy_headers,
        )
        if pivot_header_ref is not None:
            ref = substitute(pivot_header_ref, values=values, macros=macros)
            CrosstabUtility.write_header_list(
                workdir, ref, result.attrs.get("pivot_headers", [])
            )
    if header:
        header = [substitute(item, values=values, macros=macros) for item in header]
    CsvIO(workdir=workdir).write(str(job_path(substitute(str(output), values=values, macros=macros), workdir)),
                  result, header=header)
    return result
