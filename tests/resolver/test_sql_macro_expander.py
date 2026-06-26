from __future__ import annotations

from vg2c.frontend.models import BlockOptions, Kind, ParsedBlock, SourceSpan
from vg2c.resolver.models import ResolvedBlock
from vg2c.resolver.sql_macro_expander import expand_sql_macros


def _resolved_sql_block(body: str, index: int = 0) -> ResolvedBlock:
    parsed = ParsedBlock(
        index=index,
        options=BlockOptions.from_pairs({"ENGINE": "SQLite"}.items()),
        body=body,
        raw=body,
        span=SourceSpan(file=None, start_line=1, end_line=10),
    )
    return ResolvedBlock(
        parsed=parsed,
        kind=Kind.SQLITE_QUERY,
        resolved_options=parsed.options,
        resolved_body=body,
        sql_macro_calls=(),
        runtime_macro_refs=(),
        control_payload=None,
        scope_id=0,
    )


def test_column_by_name_parses() -> None:
    block = _resolved_sql_block('WHERE SQL_Get_CSV_List(".\\f.tab", lot, "v1.lot In")')
    updated, _, _ = expand_sql_macros([block], {"f.tab": 1}, {})
    call = updated[0].sql_macro_calls[0]
    assert call.column_ref == "lot"


def test_column_by_index_parses() -> None:
    block = _resolved_sql_block(
        'WHERE SQL_Get_CSV_List(".\\f.csv", "2", "p.prodgroup3 In")'
    )
    updated, _, _ = expand_sql_macros([block], {"f.csv": 1}, {})
    call = updated[0].sql_macro_calls[0]
    assert call.column_ref == 2


def test_lead_in_with_commas_is_preserved() -> None:
    block = _resolved_sql_block('WHERE SQL_Get_CSV_List(".\\f.csv", "2", "x In, y In")')
    updated, _, _ = expand_sql_macros([block], {"f.csv": 1}, {})
    assert updated[0].sql_macro_calls[0].lead_in == "x In, y In"


def test_two_calls_in_one_body_get_two_placeholders() -> None:
    block = _resolved_sql_block(
        'A SQL_Get_CSV_List(".\\a.csv", "1", "x In") OR SQL_Get_CSV_List(".\\b.csv", col, "y In")'
    )
    updated, _, _ = expand_sql_macros([block], {"a.csv": 1, "b.csv": 2}, {})
    calls = updated[0].sql_macro_calls
    assert len(calls) == 2
    assert calls[0].placeholder != calls[1].placeholder
    assert calls[0].placeholder in updated[0].resolved_body
    assert calls[1].placeholder in updated[0].resolved_body


def test_unknown_sql_macro_left_untouched_with_info_diag() -> None:
    body = "WHERE SQL_Time_Range('x')"
    block = _resolved_sql_block(body)
    updated, _, diags = expand_sql_macros([block], {}, {})
    assert updated[0].resolved_body == body
    assert any(d.code == "unknown-sql-macro" for d in diags)


def test_unknown_producer_emits_info_diag() -> None:
    block = _resolved_sql_block('WHERE SQL_Get_CSV_List(".\\missing.csv", "1", "x In")')
    _, _, diags = expand_sql_macros([block], {}, {})
    assert any(d.code == "sql-macro-csv-unknown-producer" for d in diags)


def test_call_site_wrap_appends_closing_paren() -> None:
    """When the call site is `(<col> In SQL_Get_CSV_List(...)` the resolver
    must append a `)` after the placeholder to balance the unmatched `(`."""
    body = 'WHERE (ats.lot In \nSQL_Get_CSV_List(".\\f.tab", "2", "ats.lot In")'
    block = _resolved_sql_block(body)
    updated, _, _ = expand_sql_macros([block], {"f.tab": 1}, {})
    placeholder = updated[0].sql_macro_calls[0].placeholder
    assert placeholder + ")" in updated[0].resolved_body


def test_unwrapped_call_site_has_no_extra_paren() -> None:
    """A bare `<col> In SQL_Get_CSV_List(...)` must NOT gain a trailing `)`."""
    body = 'WHERE p.prodgroup3 In \nSQL_Get_CSV_List(".\\f.csv", "2", "p.prodgroup3 In")'
    block = _resolved_sql_block(body)
    updated, _, _ = expand_sql_macros([block], {"f.csv": 1}, {})
    placeholder = updated[0].sql_macro_calls[0].placeholder
    # Placeholder must not be followed by `)`.
    after = updated[0].resolved_body.split(placeholder, 1)[1]
    assert not after.startswith(")")
