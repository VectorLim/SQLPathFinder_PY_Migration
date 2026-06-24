"""Unit tests for MacroState."""

from __future__ import annotations

import pytest

from vg2c_runtime.macro import MacroState


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
