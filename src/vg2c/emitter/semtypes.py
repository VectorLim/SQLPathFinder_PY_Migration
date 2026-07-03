from __future__ import annotations

import re
from dataclasses import dataclass
from typing import Any

from vg2c.dispatch.models import DispatchedBlock
from vg2c.emitter.macro import placeholders_to_python_expr
from vg2c.resolver.models import ResolvedBlock

_SQL_MACRO_TOKEN_RE = re.compile(r"@@SQLMACRO:(\d+)@@")


@dataclass(frozen=True, slots=True)
class RawExpr:
    source: str


class _SemType:
    @classmethod
    def extract(
        cls,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> Any:
        raise NotImplementedError


class SqlText(_SemType):
    @classmethod
    def extract(
        cls,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> str | RawExpr:
        sql = (
            dispatched.rewritten_sql if dispatched is not None else block.resolved_body
        )
        if "@@SQLMACRO:" not in sql:
            return sql

        parts: list[str] = []
        cursor = 0
        for match in _SQL_MACRO_TOKEN_RE.finditer(sql):
            literal = sql[cursor : match.start()]
            if literal:
                parts.append(repr(literal))

            call_index = int(match.group(1))
            if call_index < 0 or call_index >= len(block.sql_macro_calls):
                parts.append(repr(match.group(0)))
            else:
                call = block.sql_macro_calls[call_index]
                csv_path_expr = option_to_python_expr(call.csv_path)
                col_ref = repr(call.column_ref)
                lead_in = repr(call.lead_in)
                parts.append(
                    f"ctx.sql_macros.sql_get_csv_list({csv_path_expr}, {col_ref}, {lead_in})"
                )

            cursor = match.end()

        tail = sql[cursor:]
        if tail:
            parts.append(repr(tail))

        if not parts:
            return sql
        return RawExpr(" + ".join(parts))


class OutputPath(_SemType):
    @classmethod
    def extract(
        cls,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> str:
        csv_value = block.resolved_options.lookup.get("CSV")
        if csv_value:
            return strip_quotes(csv_value)

        write_file_value = block.resolved_options.lookup.get("WRITE-FILE")
        if write_file_value:
            candidate = strip_quotes(write_file_value)
            if candidate.upper() not in {"Y", "N"}:
                return candidate

        if block.kind.value == "WRITE_FILE":
            return f"step_{block.parsed.index:04d}.txt"
        return f"step_{block.parsed.index:04d}.csv"


class SourceType(_SemType):
    @classmethod
    def extract(
        cls,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> str:
        if dispatched is None:
            return "MARS"

        db_by_dialect = {
            "oracle_mars": "MARS",
            "oracle_oasys": "OASYS",
            "oracle_aries": "ARIES",
            "sqlite": "sqlite",
        }
        return db_by_dialect.get(
            dispatched.dialect,
            dispatched.reader_target.database_arg or "MARS",
        )


class TableInputs(_SemType):
    @classmethod
    def extract(
        cls,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> list[str]:
        inputs: list[str] = []
        for key, value in block.resolved_options.pairs:
            if key != "TABLE":
                continue
            for table_name in value.split(","):
                table_name = strip_quotes(table_name.strip())
                if table_name:
                    inputs.append(table_name)
        return inputs


class Header(_SemType):
    @classmethod
    def extract(
        cls,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> list[str] | None:
        headers_value = block.resolved_options.lookup.get("HEADERS")
        if not headers_value:
            return None
        if "CrossTab->[[" in headers_value:
            return None
        stripped = strip_quotes(headers_value)
        parts = [p.strip() for p in stripped.split(",")]
        return [p for p in parts if p]


class Crosstab(_SemType):
    @classmethod
    def extract(
        cls,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> dict[str, Any] | None:
        ctrow = strip_quotes(block.resolved_options.lookup.get("CTROW", ""))
        ctheader = strip_quotes(block.resolved_options.lookup.get("CTHEADER", ""))
        ctvalue = strip_quotes(block.resolved_options.lookup.get("CTVALUE", ""))
        if not (ctrow and ctheader and ctvalue):
            return None
        row_keys = [c.strip() for c in ctrow.split(",") if c.strip()]
        return {
            "row_keys": row_keys,
            "header_key": ctheader,
            "value_key": ctvalue,
        }


class Argv(_SemType):
    @classmethod
    def extract(
        cls,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        if not text:
            return []
        return text.split()


class WriteFileTemplate(_SemType):
    @classmethod
    def extract(
        cls,
        block: ResolvedBlock,
        dispatched: DispatchedBlock | None,
    ) -> str:
        return block.resolved_body


def strip_quotes(value: str) -> str:
    value = value.strip()
    if len(value) >= 2 and value[0] == value[-1] and value[0] in {'"', "'"}:
        return value[1:-1]
    return value


def option_to_python_expr(value: str | None) -> str:
    if value is None:
        return "None"
    return placeholders_to_python_expr(strip_quotes(value))
