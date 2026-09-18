from pathlib import Path

import pytest

from vg2c import compile_document
from vg2c.editing import SemanticChange, project_changes
from vg2c.operands import IfThen
from vg2c.semantics import CONDITION_OPERATORS, build_semantic_model


def _condition_source() -> str:
    return """<OPTIONS>
/UTILITIES={ROWS-IN-FILE} "input.csv" "COUNT" "N"
/PROMPT-TEXT=Count rows
</OPTIONS>

<---- New Query ---->

<OPTIONS>
/UTILITIES={IF-THEN} "COUNT" "GT" "0" "" "" "" ""
/PROMPT-TEXT=Check count
</OPTIONS>

<---- New Query ---->

<OPTIONS>
/WRITE-FILE=Y
/CSV=inside.txt
</OPTIONS>
inside

<---- New Query ---->

<OPTIONS>
/UTILITIES={END-IF}
</OPTIONS>

<---- New Query ---->
"""


def _compile(tmp_path: Path, text: str):
    source = tmp_path / "script.txt"
    source.write_text(text, encoding="utf-8")
    return compile_document(source)


def test_rows_in_file_is_one_composite_operation_and_defines_symbol(tmp_path):
    result = _compile(tmp_path, _condition_source())
    model = build_semantic_model(result)

    row_count = next(op for op in model.operations if op.kind == "check-row-count")
    assert row_count.display_name == "Check Row Count"
    assert {binding.name for binding in row_count.bindings} == {"path", "target"}
    assert not any(op.kind == "macro.set_named" for op in model.operations)

    count = next(symbol for symbol in model.symbols if symbol.display_name == "COUNT")
    assert count.kind == "macro"
    assert count.introduction is not None
    assert count.introduction.operation_id == row_count.id


def test_condition_uses_authoritative_operator_table_and_supports_compound_edit(tmp_path):
    result = _compile(tmp_path, _condition_source())
    model = build_semantic_model(result)
    condition = next(op for op in model.operations if op.kind == "condition")
    bindings = {binding.name: binding for binding in condition.bindings}

    assert CONDITION_OPERATORS == (
        ("EQS", "==", "string"),
        ("NES", "!=", "string"),
        ("LE", "<=", "numeric"),
        ("LT", "<", "numeric"),
        ("GE", ">=", "numeric"),
        ("GT", ">", "numeric"),
        ("EQ", "==", "numeric"),
        ("NE", "!=", "numeric"),
    )
    assert condition.validation_state == "valid"

    projected = project_changes(
        result,
        [
            SemanticChange(bindings["op"].id, "LE"),
            SemanticChange(bindings["conj"].id, "AND"),
            SemanticChange(bindings["lhs2"].id, "COUNT"),
            SemanticChange(bindings["op2"].id, "NE"),
            SemanticChange(bindings["rhs2"].id, "10"),
        ],
    )
    assert projected.valid
    assert (
        "int(ctx.macro.named('COUNT')) <= int('0') and "
        "int(ctx.macro.named('COUNT')) != int('10')"
        in projected.source
    )


def test_condition_rejects_unresolved_manual_symbol_without_rewriting_source(tmp_path):
    result = _compile(tmp_path, _condition_source())
    condition = next(
        op for op in build_semantic_model(result).operations if op.kind == "condition"
    )
    lhs = next(binding for binding in condition.bindings if binding.name == "lhs")

    projected = project_changes(result, [SemanticChange(lhs.id, "MISSING_SYMBOL")])

    assert not projected.valid
    assert projected.source == result.emitted.source
    assert [(issue.code, issue.binding_id) for issue in projected.issues] == [
        ("unresolved-symbol", lhs.id)
    ]


def test_macro_row_headers_are_exposed_as_symbols_when_input_is_static(tmp_path):
    (tmp_path / "rows.csv").write_text("LOT,PRODUCT\nL1,P1\n", encoding="utf-8")
    result = _compile(
        tmp_path,
        """<OPTIONS>
/UTILITIES={START-MACRO} "rows.csv" "N"
</OPTIONS>
<---- New Query ---->
<OPTIONS>
/WRITE-FILE=Y
/CSV=inside.txt
</OPTIONS>
inside
<---- New Query ---->
<OPTIONS>
/UTILITIES={END-MACRO}
</OPTIONS>
<---- New Query ---->
""",
    )
    model = build_semantic_model(result)
    symbols = {symbol.display_name: symbol.kind for symbol in model.symbols}
    assert symbols["LOT"] == "macro-row"
    assert symbols["PRODUCT"] == "macro-row"


def test_control_file_bindings_edit_through_shared_renderer(tmp_path):
    result = _compile(
        tmp_path,
        """<OPTIONS>
/UTILITIES={START-MACRO} "before.csv" "N"
</OPTIONS>
<---- New Query ---->
<OPTIONS>
/WRITE-FILE=Y
/CSV=inside.txt
</OPTIONS>
inside
<---- New Query ---->
<OPTIONS>
/UTILITIES={END-MACRO}
</OPTIONS>
<---- New Query ---->
""",
    )
    operation = next(
        op for op in build_semantic_model(result).operations if op.kind == "macro-loop"
    )
    binding = operation.bindings[0]

    projected = project_changes(result, [SemanticChange(binding.id, "after.csv")])

    assert projected.valid
    assert "ctx.csv_io.single_row('after.csv')" in projected.source


def test_embedded_python_is_a_focused_validated_binding(tmp_path):
    result = _compile(
        tmp_path,
        """<OPTIONS>
/WRITE-FILE=Y
/CSV=embedded.py
</OPTIONS>
print("before")
<---- New Query ---->
""",
    )
    operation = next(
        op for op in build_semantic_model(result).operations if op.kind == "embedded-python"
    )
    source = operation.bindings[0]

    projected = project_changes(
        result, [SemanticChange(source.id, 'print("after")')]
    )
    assert projected.valid
    assert 'print("after")' in projected.source

    invalid = project_changes(result, [SemanticChange(source.id, "if :")])
    assert not invalid.valid
    assert any(issue.code == "invalid-python" for issue in invalid.issues)


def test_shared_generated_global_records_each_semantic_operation_reference(tmp_path):
    mail = (
        '<OPTIONS>\n/UTILITIES="SQLPathFinder_Email.va" '
        '"person@example.com" "Report" "Body"\n</OPTIONS>\n'
        '<---- New Query ---->\n'
    )
    result = _compile(tmp_path, mail + mail)
    model = build_semantic_model(result)

    email_operations = [op for op in model.operations if op.display_name == "Send Email"]
    recipient = next(symbol for symbol in model.symbols if symbol.display_name == "EMAIL_TO")

    assert len(email_operations) == 2
    assert {ref.operation_id for ref in recipient.references} == {
        op.id for op in email_operations
    }
    assert {ref.binding_id for ref in recipient.references} == {"global:EMAIL_TO"}


def test_macro_placeholder_reference_is_captured_before_python_rendering(tmp_path):
    result = _compile(
        tmp_path,
        """<OPTIONS>
/UTILITIES={ROWS-IN-FILE} "input.csv" "COUNT" "N"
</OPTIONS>
<---- New Query ---->
<OPTIONS>
/UTILITIES=@EXEDIR@\\WaitFile.va "<<<COUNT>>>.csv" "30"
</OPTIONS>
<---- New Query ---->
""",
    )
    model = build_semantic_model(result)

    count = next(symbol for symbol in model.symbols if symbol.display_name == "COUNT")
    wait = next(op for op in model.operations if op.display_name == "Wait for File")

    assert count.kind == "macro"
    assert count.introduction is not None
    assert {ref.operation_id for ref in count.references} >= {wait.id}
    assert all(ref.binding_id for ref in count.references)


@pytest.mark.parametrize(
    ("code", "symbol", "operand_type"),
    CONDITION_OPERATORS,
)
def test_all_condition_operators_keep_authoritative_rendering(code, symbol, operand_type):
    lhs = "VAR(COUNT)" if operand_type == "string" else "COUNT"
    expression = IfThen(lhs, code, "10", None, None, None, None, "")._build_condition_expr()

    assert f" {symbol} " in expression
    assert "ctx.macro.named('COUNT')" in expression
    if operand_type == "numeric":
        assert expression == f"int(ctx.macro.named('COUNT')) {symbol} int('10')"
    else:
        assert expression == f"ctx.macro.named('COUNT') {symbol} '10'"


def test_condition_operand_characterization_covers_placeholder_literal_empty_and_or():
    placeholder = IfThen("<<<NAME>>>", "EQS", "ready", None, None, None, None, "")
    assert placeholder._build_condition_expr() == "ctx.macro.named('NAME') == 'ready'"

    empty = IfThen("", "EQS", "", None, None, None, None, "")
    assert empty._build_condition_expr() == "'' == ''"

    compound = IfThen("VAR(A)", "EQS", "x", "OR", "B", "GT", "1", "")
    assert compound._build_condition_expr() == (
        "ctx.macro.named('A') == 'x' or "
        "int(ctx.macro.named('B')) > int('1')"
    )


def test_condition_bindings_and_symbols_are_frontend_ready(tmp_path):
    result = _compile(tmp_path, _condition_source())
    model = build_semantic_model(result)
    condition = next(op for op in model.operations if op.kind == "condition")
    by_name = {binding.name: binding for binding in condition.bindings}
    count = next(symbol for symbol in model.symbols if symbol.display_name == "COUNT")

    assert by_name["lhs"].capabilities == ("symbol-or-literal",)
    assert by_name["rhs"].capabilities == ("symbol-or-literal",)
    assert by_name["op"].capabilities == ("condition-operator",)
    assert by_name["conj"].capabilities == ("condition-connector",)
    assert count.condition_value == "VAR(COUNT)"


def test_rows_in_file_target_macro_is_editable(tmp_path):
    result = _compile(tmp_path, _condition_source())
    row_count = next(
        op for op in build_semantic_model(result).operations if op.kind == "check-row-count"
    )
    target = next(binding for binding in row_count.bindings if binding.name == "target")

    projected = project_changes(result, [SemanticChange(target.id, "TOTAL")])

    assert projected.valid
    assert "ctx.macro.set_named('TOTAL'," in projected.source
