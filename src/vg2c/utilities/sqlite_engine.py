"""SqliteEngine - execute SQL joins over CSV inputs."""

from __future__ import annotations

import json
import re
from dataclasses import replace
from pathlib import Path

from vg2c.emitter.models import BlockEmission, CodeExpr, ExternalParameter
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
    scan_sql_get_csv_list_calls,
    to_code_expr,
)
from vg2c.utilities._runtime_helpers import strip_quotes
from vg2c.utilities.crosstab import CrosstabUtility
from vg2c.utilities.csv_io import CsvIO


class SqliteEngine(EmitterUtility):
    """Emit query calls for external, SQLite, and DuckDB readers."""

    utility_name = "sqlite_engine"
    handles = (Kind.SQL_QUERY, Kind.SQLITE_QUERY, Kind.DUCKDB_QUERY)
    _SQL_NUMBER = r"[+-]?(?:\d+(?:\.\d*)?|\.\d+)(?:[eE][+-]?\d+)?"

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if (
            options.lookup.get("OLEDB", "").upper() == "DUCKDB"
            or options.lookup.get("ENGINE", "").upper() == "DUCKDB"
        ):
            return Kind.DUCKDB_QUERY, "/OLEDB=DuckDB or /ENGINE=DuckDB"
        if options.lookup.get("OLEDB", "").upper() == "SQLITE":
            return Kind.SQLITE_QUERY, "/OLEDB=SQLite"
        if options.lookup.get("ENGINE", "").upper() == "SQLITE":
            return Kind.SQLITE_QUERY, "/ENGINE=SQLite"

        node = options.lookup.get("NODE", "")
        engine = options.lookup.get("ENGINE", "")
        oledb = options.lookup.get("OLEDB", "")
        if engine.upper() not in {"VA"} and oledb.upper() not in {"SQLPLUS"}:
            return None

        if any(
            SqliteEngine._node_matches(node, token)
            for token in ("MARS", "OASYS", "ARIES")
        ):
            return (
                Kind.SQL_QUERY,
                "/NODE indicates Oracle dialect and /ENGINE=VA or /OLEDB=SQLPlus",
            )
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
    def _format_sql_literal(sql: str) -> str:
        escaped = sql.replace("\\", "\\\\").replace('"""', '\\"\\"\\"')
        return f'"""{escaped}"""'

    @staticmethod
    def _sql_source(block) -> str:
        sql = getattr(block, "rewritten_sql", None)
        return block.resolved_body if sql is None else sql

    @classmethod
    def extract_globals(cls, block) -> dict[str, object]:
        from vg2c.utilities._sql_globals import extract_sql_globals

        return {
            item.key: item.value for item in extract_sql_globals(cls._sql_source(block))
        }

    @classmethod
    def _extract_sql_text(cls, block, global_refs=None) -> CodeExpr:
        from vg2c.utilities._sql_globals import extract_sql_globals

        sql = cls._sql_source(block)
        calls = scan_sql_get_csv_list_calls(sql)
        replacements: list[tuple[int, int, str]] = []
        references: list[str] = []
        for item in extract_sql_globals(sql) if global_refs else ():
            if any(call.start < item.end and call.end > item.start for call in calls):
                continue
            reference = global_refs[item.key]
            replacements.append(
                (
                    item.start,
                    item.end,
                    f"SqliteEngine.global_sql({reference.source}, {item.numeric!r})",
                )
            )
            references.extend(reference.global_names)
        for call in calls:
            csv_path_expr = to_code_expr(call.source_path)
            expr = CsvIO.sql_get_csv_list.render(
                csv_path_expr,
                call.column_ref,
                call.lead_in,
                chunk_size=CodeExpr("VG2C_SQL_GET_CSV_LIST_CHUNK_SIZE"),
            )
            if call.needs_closing_paren:
                expr += " + ')'"
            replacements.append((call.start, call.end, expr))
        if not replacements:
            return CodeExpr(cls._format_sql_literal(sql), sql)

        parts: list[str] = []
        cursor = 0
        for start, end, expression in sorted(replacements):
            literal = sql[cursor:start]
            if literal:
                parts.append(repr(literal))
            parts.append(expression)
            cursor = end
        if sql[cursor:]:
            parts.append(repr(sql[cursor:]))
        return CodeExpr(
            " + ".join(parts), global_names=tuple(dict.fromkeys(references))
        )

    @staticmethod
    def global_sql(value, numeric: bool = False) -> str:
        """Render editable operands with their original SQL literal category."""
        if isinstance(value, list):
            if not value:
                raise ValueError("SQL IN globals must contain at least one value")
            return ", ".join(SqliteEngine.global_sql(item, numeric) for item in value)
        if numeric or type(value) is int:
            text = str(value)
            if not re.fullmatch(SqliteEngine._SQL_NUMBER, text):
                raise ValueError(f"Invalid SQL numeric global: {value!r}")
            return text
        if not isinstance(value, str):
            raise ValueError(f"Expected a SQL string or number, got {value!r}")
        return "'" + value.replace("'", "''") + "'"

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
        path_definition = replace(
            sql_definition, name="path", display_label="SQL asset", capabilities=()
        )
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
        external = ExternalParameter(
            definition=sql_definition,
            value=sql,
            editor_type="multiline",
            read_only_reason="SQL is an external editable asset; generated Python offsets cannot edit it.",
        )
        return BlockEmission(
            lines=(call,),
            imports=imports,
            runtime_exports=runtime_exports,
            assets=((name, sql),),
            external_parameters=(external,),
        )
