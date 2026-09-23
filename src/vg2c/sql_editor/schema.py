from __future__ import annotations

from collections import Counter
from dataclasses import dataclass, replace

from vg2c.sql_editor.models import SqlColumnChoice, SqlEditableModel, SqlTableChoice
from vg2c.sql_editor.parser import unquote_identifier
from vg2c.sql_lexer import SqlToken, lex_sql


@dataclass(frozen=True, slots=True)
class SqlTableSchema:
    table_name: str
    columns: tuple[str, ...]


def with_input_schemas(
    model: SqlEditableModel, schemas: tuple[SqlTableSchema, ...]
) -> SqlEditableModel:
    """Attach choices only when a parsed source matches a proven SQLite table."""
    counts = Counter(item.table_name.casefold() for item in schemas)
    by_table = {
        item.table_name.casefold(): item.columns
        for item in schemas
        if counts[item.table_name.casefold()] == 1
    }
    choices: list[SqlColumnChoice] = []
    present: set[str] = set()
    for source in model.sources:
        identity = _source_identity(source.expression)
        if identity is None:
            continue
        table_name, qualifier = identity
        present.add(table_name.casefold())
        columns = by_table.get(table_name.casefold())
        if columns is None:
            continue
        column_counts = Counter(name.casefold() for name in columns)
        for index, column in enumerate(columns):
            if not column or column_counts[column.casefold()] != 1:
                continue
            choices.append(
                SqlColumnChoice(
                    id=f"{source.id}:column:{index}",
                    label=f"{qualifier}.{column}",
                    expression=f"{_quote(qualifier)}.{_quote(column)}",
                    source_id=source.id,
                )
            )
    table_choices: list[SqlTableChoice] = []
    for index, schema in enumerate(schemas):
        table_name = schema.table_name
        if table_name.casefold() in present or counts[table_name.casefold()] != 1:
            continue
        table_id = f"table:{index}"
        table_choices.append(SqlTableChoice(table_id, table_name, _quote(table_name)))
        column_counts = Counter(name.casefold() for name in schema.columns)
        for column_index, column in enumerate(schema.columns):
            if not column or column_counts[column.casefold()] != 1:
                continue
            choices.append(
                SqlColumnChoice(
                    id=f"{table_id}:column:{column_index}",
                    label=f"{table_name}.{column}",
                    expression=f"{_quote(table_name)}.{_quote(column)}",
                    source_id=table_id,
                )
            )
    return replace(
        model, column_choices=tuple(choices), table_choices=tuple(table_choices)
    )


def _source_identity(expression: str) -> tuple[str, str] | None:
    tokens, error = lex_sql(expression)
    if error:
        return None
    significant = [token for token in tokens if token.kind != "whitespace"]
    if any(token.kind == "comment" for token in significant):
        return None
    if len(significant) == 1 and _identifier(significant[0]):
        table = unquote_identifier(significant[0].text)
        return table, table
    if len(significant) == 2 and all(_identifier(token) for token in significant):
        return unquote_identifier(significant[0].text), unquote_identifier(significant[1].text)
    if (
        len(significant) == 3
        and _identifier(significant[0])
        and significant[1].upper == "AS"
        and _identifier(significant[2])
    ):
        return unquote_identifier(significant[0].text), unquote_identifier(significant[2].text)
    return None


def _identifier(token: SqlToken) -> bool:
    return token.kind in {"word", "quoted"}


def _quote(value: str) -> str:
    return '"' + value.replace('"', '""') + '"'
