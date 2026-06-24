from __future__ import annotations

from vg2c.frontend.models import BlockOptions, ClassifiedBlock, Kind, ParsedBlock, SourceSpan
from vg2c.resolver.macro_resolver import resolve_macros
from vg2c.resolver.models import RowsInFile
from vg2c.resolver.scope_builder import build_scope_tree


def _block(
    index: int,
    kind: Kind,
    options: dict[str, str] | None = None,
    body: str = "",
) -> ClassifiedBlock:
    parsed = ParsedBlock(
        index=index,
        options=BlockOptions.from_pairs((options or {}).items()),
        body=body,
        raw="",
        span=SourceSpan(file=None, start_line=index + 1, end_line=index + 1),
    )
    return ClassifiedBlock(parsed=parsed, kind=kind, reason="test")


def test_named_placeholder_binds_to_enclosing_macro_frame() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "config.csv" "N"'}),
        _block(1, Kind.UTILITY, {"UTILITIES": "run <<<SFOLDER>>>"}),
        _block(2, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"}),
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _, _, diags = resolve_macros(blocks, tree)
    assert not [d for d in diags if d.code == "unbound-macro-var"]

    refs = resolved[1].runtime_macro_refs
    assert len(refs) == 1
    assert refs[0].name == "SFOLDER"
    assert refs[0].frame_id != -1


def test_same_name_in_sibling_macros_uses_distinct_frame_ids() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "a.csv" "N"'}),
        _block(1, Kind.UTILITY, {"UTILITIES": "<<<X>>>"}),
        _block(2, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"}),
        _block(3, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "b.csv" "N"'}),
        _block(4, Kind.UTILITY, {"UTILITIES": "<<<X>>>"}),
        _block(5, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"}),
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _, _, _ = resolve_macros(blocks, tree)

    assert resolved[1].runtime_macro_refs[0].frame_id != resolved[4].runtime_macro_refs[0].frame_id


def test_case_insensitive_named_resolution() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "a.csv" "N"'}),
        _block(1, Kind.WRITE_FILE, {"WRITE-FILE": "Y"}, body="<<<sfolder>>> <<<SFOLDER>>> <<<SFolder>>>"),
        _block(2, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"}),
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _, _, _ = resolve_macros(blocks, tree)
    names = [r.name for r in resolved[1].runtime_macro_refs]
    assert names == ["SFOLDER", "SFOLDER", "SFOLDER"]
    assert all(r.frame_id != -1 for r in resolved[1].runtime_macro_refs)


def test_rows_in_file_payload_present_but_no_scope_push() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{ROWS-IN-FILE} "f.csv" "COUNT" "N"'}),
        _block(1, Kind.UTILITY, {"UTILITIES": "echo <<<COUNT>>>"}),
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _, _, _ = resolve_macros(blocks, tree)
    assert isinstance(resolved[0].control_payload, RowsInFile)
    assert resolved[1].runtime_macro_refs[0].frame_id == 0


def test_unbound_macro_var_emits_warning() -> None:
    blocks = [_block(0, Kind.UTILITY, {"UTILITIES": "echo <<<MISSING>>>"})]
    tree, _ = build_scope_tree(blocks)
    resolved, _, _, diags = resolve_macros(blocks, tree)
    assert resolved[0].runtime_macro_refs[0].frame_id == -1
    assert any(d.code == "unbound-macro-var" for d in diags)


def test_positional_placeholder_cursor_advances() -> None:
    blocks = [
        _block(0, Kind.MACRO_CONTROL, {"UTILITIES": '{START-MACRO} "a.csv" "N"'}),
        _block(1, Kind.UTILITY, {"UTILITIES": "<<>> <<>>"}),
        _block(2, Kind.MACRO_CONTROL, {"UTILITIES": "{END-MACRO}"}),
    ]
    tree, _ = build_scope_tree(blocks)
    resolved, _, _, _ = resolve_macros(blocks, tree)
    refs = resolved[1].runtime_macro_refs
    assert len(refs) == 2
    assert all(r.name == "__POSITIONAL__" for r in refs)
    assert all(r.frame_id != -1 for r in refs)
