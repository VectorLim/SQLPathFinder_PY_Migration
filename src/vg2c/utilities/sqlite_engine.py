"""SqliteEngine - execute SQL joins over CSV inputs."""

from __future__ import annotations

import json
from dataclasses import replace
from pathlib import Path

from vg2c.emitter.models import BlockEmission, CodeExpr
from vg2c.emitter.operations import EmittableOperation
from vg2c.kind import Kind
from vg2c.project_paths import project_name
from vg2c.utilities._base import EmitterUtility, UtilitySpec
from vg2c.utilities._emit_helpers import (
    extract_crosstab_options,
    native_argument,
    native_macro_expression,
    parse_table_binding,
    resolve_output_path,
)
from vg2c.runtime.values import strip_quotes
from vg2c.utilities.runtime_adapters import CrosstabUtility


class SqliteEngine(EmitterUtility):
    """Emit query calls for external, SQLite, and DuckDB readers."""

    utility_name = "sqlite_engine"
    handles = (Kind.SQL_QUERY, Kind.SQLITE_QUERY, Kind.DUCKDB_QUERY)

    @staticmethod
    def check(options) -> Kind | None:
        if (
            options.lookup.get("OLEDB", "").upper() == "DUCKDB"
            or options.lookup.get("ENGINE", "").upper() == "DUCKDB"
        ):
            return Kind.DUCKDB_QUERY
        if options.lookup.get("OLEDB", "").upper() == "SQLITE":
            return Kind.SQLITE_QUERY
        if options.lookup.get("ENGINE", "").upper() == "SQLITE":
            return Kind.SQLITE_QUERY

        node = options.lookup.get("NODE", "")
        engine = options.lookup.get("ENGINE", "")
        oledb = options.lookup.get("OLEDB", "")
        if engine.upper() not in {"VA"} and oledb.upper() not in {"SQLPLUS"}:
            return None

        if any(
            SqliteEngine._node_matches(node, token)
            for token in ("MARS", "OASYS", "ARIES")
        ):
            return Kind.SQL_QUERY
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
    def _sql_source(block) -> str:
        sql = getattr(block, "rewritten_sql", None)
        return block.resolved_body if sql is None else sql

    @staticmethod
    def _extract_table_inputs(block) -> list[str | tuple[str, str]]:
        inputs: list[str | tuple[str, str]] = []
        for key, value in block.resolved_options.pairs:
            if key != "TABLE":
                continue
            for table_spec in value.split(","):
                path, table_name = parse_table_binding(table_spec.strip())
                if path:
                    inputs.append((path, table_name) if table_name else path)
        return inputs

    @staticmethod
    def _extract_duckdb_inputs(block) -> list[tuple[str, str]]:
        node = block.resolved_options.lookup.get("NODE", "")
        prefix = "DUCKDB:"
        if not node.upper().startswith(prefix):
            raise ValueError("DuckDB query requires /NODE=DUCKDB:<file>:<table>")
        inputs = []
        for binding in node.split("@@@@@"):
            if not binding.upper().startswith(prefix):
                raise ValueError("DuckDB /NODE entries must start with DUCKDB:")
            binding = binding[len(prefix) :]
            path, separator, table_name = binding.rpartition(":")
            if not separator or not path or not table_name:
                raise ValueError("DuckDB /NODE entries must be <file>:<table>")
            inputs.append((path, table_name))
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
    def emit_block(cls, block, *, macros: str = "macros") -> BlockEmission:
        reader = block.reader
        imports = ()
        # JobRuntime.sql infers these from the node: none -> SQLite, ARIES/MARS -> datasyncx.
        inferred = reader.utility_name == "sqlite_reader" or (
            reader.module == "datasyncx"
            and reader.name in {"AriesReader", "MarsReader"}
            and not block.reader_kwargs
        )
        if reader.utility_name == "sqlite_reader":
            runtime_exports = ()
        elif reader.utility_name == "duckdb_reader":
            runtime_exports = ("DuckDbReader",)
        else:
            if not inferred:
                imports = (f"from {reader.module} import {reader.name}",)
            runtime_exports = ("OracleClient",)

        name = f"sql/query_{block.index:03d}_{project_name(Path(resolve_output_path(block)).stem)}.sql"
        sql = cls._sql_source(block)
        kwargs = {}
        if not inferred:
            kwargs["reader"] = CodeExpr(
                f"{reader.name}("
                + ", ".join(
                    f"{key}={value!r}" for key, value in block.reader_kwargs.items()
                )
                + ")"
            )
        kwargs["output"] = resolve_output_path(block)
        if block.kind in {Kind.SQLITE_QUERY, Kind.DUCKDB_QUERY}:
            inputs = (
                cls._extract_table_inputs(block)
                if block.kind is Kind.SQLITE_QUERY
                else cls._extract_duckdb_inputs(block)
            )
            if inputs:
                kwargs["inputs"] = inputs
        else:
            node = block.reader_target.node
            kwargs["node"] = CodeExpr(native_macro_expression(node, macros), node)

        crosstab = extract_crosstab_options(block)
        if crosstab:
            kwargs.update(crosstab)
        else:
            header = cls._extract_header(block)
            if header:
                # SQL owns both its projection and any original /HEADERS override.
                # Preserve ScriptHost's by-name output semantics without exposing
                # an extra header= argument or a separate JSON sidecar in main.py.
                sql = (
                    "-- vg2c-output-headers: "
                    + json.dumps(header, ensure_ascii=False)
                    + "\n"
                    + sql
                )

        definition = UtilitySpec.operation_definition("ctx", "run_query")
        sql_definition = definition.parameter("sql")
        path_definition = replace(sql_definition, name="path")
        definition = replace(
            definition,
            parameters=tuple(
                path_definition if parameter.name == "sql" else parameter
                for parameter in definition.parameters
            ),
        )
        call = EmittableOperation.render_method_call(
            definition,
            function="job.sql",
            args=(CodeExpr(repr(name)),),
            kwargs={
                key: native_argument(value, macros) for key, value in kwargs.items()
            },
        )
        return BlockEmission(
            lines=(call,),
            imports=imports,
            runtime_exports=runtime_exports,
            assets=((name, sql),),
        )
