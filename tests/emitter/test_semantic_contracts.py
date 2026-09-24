from pathlib import Path

import pytest

from vg2c import compile_document
from vg2c.editing import SemanticChange, project_changes
from vg2c.operands import IfThen
from vg2c.semantics import CONDITION_OPERATORS
from vg2c.workflow import project_document


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
    model = project_document(result)

    row_count = next(op for op in model.operations if op.kind == "check-row-count")
    assert row_count.display_name == "Check Row Count"
    assert {binding.name for binding in row_count.bindings} == {"path", "target"}
    assert not any(op.kind == "macro.set_named" for op in model.operations)

    count = next(symbol for symbol in model.symbols if symbol.display_name == "COUNT")
    assert count.kind == "macro"
    assert count.introduction is not None
    assert count.introduction.operation_id == row_count.id


def test_source_comments_attach_to_visible_operation_once(tmp_path):
    result = _compile(
        tmp_path,
        "<OPTIONS>\n/WRITE-FILE=Y\n/CSV=note.txt\n</OPTIONS>\n"
        "# Review this output before sending\ncontent\n<---- New Query ---->\n",
    )
    operations = project_document(result).operations
    assert len(operations) == 1
    assert operations[0].comments == ("Review this output before sending",)


def test_condition_uses_authoritative_operator_table_and_supports_compound_edit(tmp_path):
    result = _compile(tmp_path, _condition_source())
    model = project_document(result)
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
        op for op in project_document(result).operations if op.kind == "condition"
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
    model = project_document(result)
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
        op for op in project_document(result).operations if op.kind == "macro-loop"
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
        op for op in project_document(result).operations if op.kind == "embedded-python"
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
    model = project_document(result)

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
    model = project_document(result)

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
    model = project_document(result)
    condition = next(op for op in model.operations if op.kind == "condition")
    by_name = {binding.name: binding for binding in condition.bindings}
    count = next(symbol for symbol in model.symbols if symbol.display_name == "COUNT")

    assert by_name["lhs"].capabilities == ("symbol-or-literal",)
    assert by_name["rhs"].capabilities == ("symbol-or-literal",)
    assert by_name["op"].capabilities == ("condition-operator",)
    assert by_name["conj"].capabilities == ("condition-connector",)
    assert by_name["lhs"].symbol_id == count.id
    assert by_name["rhs"].symbol_id is None
    assert count.value_binding_id is None
    assert by_name["lhs"].default_symbol_id == count.id


def test_symbol_identity_resolves_for_each_condition_operator(tmp_path):
    result = _compile(tmp_path, _condition_source())
    model = project_document(result)
    condition = next(op for op in model.operations if op.kind == "condition")
    bindings = {binding.name: binding for binding in condition.bindings}
    count = next(symbol for symbol in model.symbols if symbol.display_name == "COUNT")

    for operator, token in (("GT", 'COUNT'), ("EQS", 'VAR(COUNT)')):
        projection = project_changes(result, [
            SemanticChange(bindings["op"].id, operator),
            SemanticChange(bindings["rhs"].id, symbol_id=count.id),
        ])
        assert projection.valid
        assert projection.effective_values[bindings["rhs"].id] == token
        assert "ctx.macro.named('COUNT')" in projection.source

    assert not project_changes(result, [
        SemanticChange(bindings["rhs"].id, "literal", symbol_id=count.id)
    ]).valid
    assert not project_changes(result, [
        SemanticChange(bindings["rhs"].id, symbol_id="symbol:MISSING")
    ]).valid

    operator_only = project_changes(result, [SemanticChange(bindings["op"].id, "EQS")])
    assert operator_only.valid
    assert "ctx.macro.named('COUNT') == '0'" in operator_only.source


def test_rows_in_file_target_macro_is_editable(tmp_path):
    result = _compile(tmp_path, _condition_source())
    row_count = next(
        op for op in project_document(result).operations if op.kind == "check-row-count"
    )
    target = next(binding for binding in row_count.bindings if binding.name == "target")

    projected = project_changes(result, [SemanticChange(target.id, "TOTAL")])

    assert projected.valid
    assert "ctx.macro.set_named('TOTAL'," in projected.source


def test_repeated_identical_condition_headers_keep_distinct_edit_ranges(tmp_path):
    result = _compile(
        tmp_path,
        """<OPTIONS>
/UTILITIES={ROWS-IN-FILE} "input.csv" "COUNT" "N"
</OPTIONS>
<---- New Query ---->
<OPTIONS>
/UTILITIES={IF-THEN} "COUNT" "GT" "0" "" "" "" ""
</OPTIONS>
<---- New Query ---->
<OPTIONS>
/WRITE-FILE=Y
/CSV=first.txt
</OPTIONS>
first
<---- New Query ---->
<OPTIONS>
/UTILITIES={END-IF}
</OPTIONS>
<---- New Query ---->
<OPTIONS>
/UTILITIES={IF-THEN} "COUNT" "GT" "0" "" "" "" ""
</OPTIONS>
<---- New Query ---->
<OPTIONS>
/WRITE-FILE=Y
/CSV=second.txt
</OPTIONS>
second
<---- New Query ---->
<OPTIONS>
/UTILITIES={END-IF}
</OPTIONS>
<---- New Query ---->
""",
    )
    conditions = [
        operation
        for operation in project_document(result).operations
        if operation.kind == "condition"
    ]
    assert len(conditions) == 2
    assert conditions[0].source_range != conditions[1].source_range

    rhs = next(binding for binding in conditions[1].bindings if binding.name == "rhs")
    projected = project_changes(result, [SemanticChange(rhs.id, "1")])

    assert projected.valid
    assert projected.source.count("int(ctx.macro.named('COUNT')) > int('0')") == 1
    assert projected.source.count("int(ctx.macro.named('COUNT')) > int('1')") == 1


def test_email_contract_declares_bulk_toggle_and_attachment_capabilities(tmp_path):
    result = _compile(
        tmp_path,
        '<OPTIONS>\n/UTILITIES="SQLPathFinder_Email.va" '
        '"person@example.com" "Report" "Body"\n</OPTIONS>\n'
        '<---- New Query ---->\n',
    )
    operation = next(
        op for op in project_document(result).operations
        if "email" in op.capabilities
    )
    bindings = {binding.name: binding for binding in operation.bindings}

    assert operation.display_name == "Send Email"
    assert bindings["enabled"].value is True
    assert bindings["enabled"].editable
    assert bindings["enabled"].schema is not None
    assert bindings["enabled"].schema.kind == "boolean"
    assert "file-input" in bindings["attachments"].capabilities

    projected = project_changes(
        result,
        [SemanticChange(bindings["enabled"].id, False)],
    )
    assert projected.valid
    assert "enabled=False" in projected.source



def test_required_parameter_default_is_generated_reset_value(tmp_path):
    result = _compile(
        tmp_path,
        "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\n"
        "SELECT 1 AS value\n"
        "<---- New Query ---->\n",
    )
    model = project_document(result)
    sql = next(
        binding
        for operation in model.operations
        for binding in operation.bindings
        if "structured-sql" in binding.capabilities
    )
    overridden = project_document(result, [SemanticChange(sql.id, "SELECT 2 AS value")])
    overridden_sql = next(
        binding
        for operation in overridden.operations
        for binding in operation.bindings
        if binding.id == sql.id
    )

    assert sql.required
    assert sql.default == "SELECT 1 AS value"
    assert overridden_sql.value == "SELECT 2 AS value"
    assert overridden_sql.default == "SELECT 1 AS value"


def test_multiline_editor_hint_is_preserved_as_semantic_capability(tmp_path):
    result = _compile(
        tmp_path,
        """<OPTIONS>
/WRITE-FILE=Y
/CSV=out.txt
</OPTIONS>
first line
second line
<---- New Query ---->
""",
    )
    operation = next(
        item for item in project_document(result).operations
        if item.display_name == "Write File"
    )
    template = next(binding for binding in operation.bindings if binding.name == "template")

    assert "multiline" in template.capabilities
    assert "\n" in str(template.value)
