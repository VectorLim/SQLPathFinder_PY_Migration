from typing import get_args

from vg2c.sql_editor.models import SqlActionName
from vg2c.sql_editor.operations import _SQL_OPERATIONS, SqlOperation, get_sql_operation


def test_operation_registry_covers_every_sql_text_action_with_one_class_each():
    text_actions = set(get_args(SqlActionName)) - {"update-file-list"}

    assert set(_SQL_OPERATIONS) == text_actions
    assert all(isinstance(operation, SqlOperation) for operation in _SQL_OPERATIONS.values())
    assert len({type(operation) for operation in _SQL_OPERATIONS.values()}) == len(text_actions)


def test_operation_registry_dispatches_to_existing_transform_behavior():
    result = get_sql_operation("add-selection").apply(
        "SELECT a FROM table1",
        {"expression": "b"},
    )

    assert result.sql == "SELECT a, b FROM table1"
