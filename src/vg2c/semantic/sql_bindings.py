"""Semantic bindings derived from SQL_Get_CSV_List calls."""

from __future__ import annotations

import ast
from typing import Any

from vg2c.semantic.models import EditableBinding
from vg2c.emitter.models import SourceRange
from vg2c.utilities._emit_helpers import scan_sql_get_csv_list_calls
from vg2c.utility_metadata import ValueSchema


def sql_file_list_bindings(block, invocation, values: dict[str, Any]) -> list[EditableBinding]:
    """Bind proven source calls to their emitted Python path literals."""
    calls = scan_sql_get_csv_list_calls(block.resolved_body)
    sql = next((item for item in invocation.parameters if item.name == "sql"), None)
    if not calls or sql is None or sql.source_range is None:
        return []
    try:
        expression = ast.parse(sql.source, mode="eval")
    except SyntaxError:
        return []
    generated = sorted(
        (
            node for node in ast.walk(expression)
            if isinstance(node, ast.Call)
            and isinstance(node.func, ast.Attribute)
            and ast.unparse(node.func) == "ctx.csv_io.sql_get_csv_list"
            and node.args
            and isinstance(node.args[0], ast.Constant)
            and isinstance(node.args[0].value, str)
        ),
        key=lambda node: (node.lineno, node.col_offset),
    )
    if len(generated) != len(calls):
        return []

    bindings = []
    for index, (call, emitted_call) in enumerate(zip(calls, generated, strict=True)):
        if "'" in call.csv_path or '"' in call.csv_path:
            return []
        literal = emitted_call.args[0]
        generated_path = literal.value
        if _normalize_path(generated_path) != _normalize_path(call.source_path):
            return []
        start = _ast_offset(sql.source, literal.lineno, literal.col_offset)
        end = _ast_offset(sql.source, literal.end_lineno, literal.end_col_offset)
        source_range = SourceRange(
            sql.source_range.start_offset + start,
            sql.source_range.start_offset + end,
        )
        binding_id = f"{invocation.id}:sql-file-list:{index}"
        bindings.append(EditableBinding(
            id=binding_id,
            owner_operation_id=invocation.id,
            name=f"sql_file_list_{index + 1}",
            display_label=f"File list {index + 1}",
            schema=ValueSchema("string", path=True),
            value=values.get(binding_id, call.source_path),
            default=call.source_path,
            visibility="internal",
            capabilities=("file-input", "sql-file-list"),
            source_range=source_range,
            source_kind="sql-file-list",
        ))
    return bindings


def _ast_offset(source: str, line_number: int, byte_column: int) -> int:
    lines = source.splitlines(keepends=True)
    return sum(map(len, lines[:line_number - 1])) + len(
        lines[line_number - 1].encode("utf-8")[:byte_column].decode("utf-8")
    )


def _normalize_path(path: str) -> str:
    return path.replace("\\", "/").removeprefix("./")
