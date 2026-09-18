from pathlib import Path

from vg2c import compile_document
from vg2c.editing import SemanticChange, project_changes
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
        "ctx.macro.named('COUNT') <= 0 and ctx.macro.named('COUNT') != 10"
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
