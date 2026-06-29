"""Unit tests for MacroState."""

from __future__ import annotations

import pytest

from vg2c.emitter.macro import MacroState, apply_crosstab, substitute_crosstab


def test_set_and_get_named():
    m = MacroState()
    m.set_named("FOO", "bar")
    assert m.named("FOO") == "bar"
    assert m.named("foo") == "bar"  # case-insensitive


def test_missing_returns_empty_string():
    m = MacroState()
    assert m.named("MISSING") == ""


def test_push_pop_frame_top_down_lookup():
    m = MacroState()
    m.set_named("X", "base")
    m.push_frame(named={"X": "top"})
    assert m.named("X") == "top"
    m.pop_frame()
    assert m.named("X") == "base"


def test_set_named_writes_to_top_frame():
    m = MacroState()
    m.push_frame()
    m.set_named("Y", "inner")
    m.pop_frame()
    assert m.named("Y") == ""  # inner frame gone


def test_scope_context_manager():
    m = MacroState()
    m.set_named("A", "outer")
    with m.scope(row={"A": "inner", "B": "new"}):
        assert m.named("A") == "inner"
        assert m.named("B") == "new"
    assert m.named("A") == "outer"
    assert m.named("B") == ""


def test_base_frame_never_removed():
    m = MacroState()
    m.pop_frame()  # should be a no-op
    m.pop_frame()  # still safe
    m.set_named("Z", "ok")
    assert m.named("Z") == "ok"


def test_frame_variables_uppercased_on_push():
    m = MacroState()
    m.push_frame(named={"sfolder": "value"})
    assert m.named("SFOLDER") == "value"
    m.pop_frame()


def test_substitute_sql_expands_crosstab_projection():
    m = MacroState()
    sql = "SELECT a0.[facility], CrossTab->[[a0,15507;:Y]] FROM [t] a0"
    out = m.substitute_sql(
        sql,
        crosstab_alias_columns=lambda alias: [
            "facility",
            "SUBPLANEANGLEX",
            "SUBPLANEANGLEY",
        ],
    )
    assert "CrossTab->" not in out
    assert "a0.[SUBPLANEANGLEX] AS [SUBPLANEANGLEX]" in out
    assert "a0.[SUBPLANEANGLEY] AS [SUBPLANEANGLEY]" in out


def test_substitute_crosstab_header_mode_n():
    sql = "CrossTab->[[a0,15507;:N]]"
    out = substitute_crosstab(
        sql,
        alias_columns_lookup=lambda alias: ["SUBPLANEANGLEX", "SUBPLANEANGLEY"],
    )
    assert out == "SUBPLANEANGLEX,SUBPLANEANGLEY"


def test_apply_crosstab_pivots_rows_for_downstream_join():
    import pandas as pd
    
    rows = pd.DataFrame([
        {
            "facility": "KM",
            "lot": "L1",
            "operation": "2090",
            "test_name": "SUBPLANEANGLEX",
            "Sub_plane": "1.5",
        },
        {
            "facility": "KM",
            "lot": "L1",
            "operation": "2090",
            "test_name": "SUBPLANEANGLEY",
            "Sub_plane": "-0.5",
        },
    ])

    out = apply_crosstab(
        rows,
        row_keys=["facility", "lot", "operation"],
        header_key="test_name",
        value_key="Sub_plane",
    )

    assert len(out) == 1
    assert out.iloc[0]["facility"] == "KM"
    assert out.iloc[0]["lot"] == "L1"
    assert out.iloc[0]["operation"] == "2090"
    assert out.iloc[0]["SUBPLANEANGLEX"] == "1.5"
    assert out.iloc[0]["SUBPLANEANGLEY"] == "-0.5"
