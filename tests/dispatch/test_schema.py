from __future__ import annotations

import pytest

from vg2c.dispatch.models import DispatchConfig
from vg2c.dispatch.schema import substitute
from vg2c.frontend.models import SourceSpan

_SPAN = SourceSpan(file=None, start_line=1, end_line=1)
_BLOCK = 0


# --- OASYS substitution ---


def test_oasys_substitutes_all_occurrences() -> None:
    body = "SELECT * FROM @OASYSSCHEMA@P_A v1, @OASYSSCHEMA@P_B v2, @OASYSSCHEMA@P_C v3"
    new_body, diags = substitute(body, "oracle_oasys", DispatchConfig(oasys_schema="OASYS_OWN"), _SPAN, _BLOCK)
    assert "@OASYSSCHEMA@" not in new_body
    assert new_body.count("OASYS_OWN.P_") == 3
    assert not diags


def test_oasys_schema_empty_explicit_emits_warning() -> None:
    body = "SELECT @OASYSSCHEMA@P_X x"
    new_body, diags = substitute(body, "oracle_oasys", DispatchConfig(oasys_schema=""), _SPAN, _BLOCK)
    assert "@OASYSSCHEMA@" in new_body  # placeholder left in place
    assert any(d.code == "dispatch-oasys-schema-unset" and d.severity == "warning" for d in diags)


def test_oasys_no_config_emits_error() -> None:
    body = "SELECT @OASYSSCHEMA@P_X x"
    new_body, diags = substitute(body, "oracle_oasys", None, _SPAN, _BLOCK)
    assert "@OASYSSCHEMA@" in new_body
    assert any(d.code == "dispatch-oasys-schema-unset" and d.severity == "error" for d in diags)


# --- MARS @[]@ preservation ---


def test_mars_preserves_square_bracket_marker() -> None:
    body = "FROM @[]@F_LotHist f0 JOIN @[]@F_Calendar c0"
    new_body, diags = substitute(body, "oracle_mars", DispatchConfig(oasys_schema=""), _SPAN, _BLOCK)
    assert new_body == body  # unchanged
    assert not diags


# --- Dialect mismatch ---


def test_mars_body_with_oasys_token_emits_mismatch() -> None:
    body = "SELECT @OASYSSCHEMA@P_X x"
    new_body, diags = substitute(body, "oracle_mars", DispatchConfig(oasys_schema="S"), _SPAN, _BLOCK)
    assert new_body == body  # not substituted — wrong dialect
    assert any(d.code == "dispatch-placeholder-dialect-mismatch" for d in diags)


def test_oasys_body_with_mars_token_emits_mismatch() -> None:
    body = "FROM @[]@F_Something f0"
    new_body, diags = substitute(body, "oracle_oasys", DispatchConfig(oasys_schema="S"), _SPAN, _BLOCK)
    assert new_body == body
    assert any(d.code == "dispatch-placeholder-dialect-mismatch" for d in diags)


# --- SQL macro placeholder coexistence ---


def test_sql_macro_placeholder_not_touched() -> None:
    body = "SELECT @OASYSSCHEMA@P_X x WHERE id IN (@@SQLMACRO:0@@)"
    new_body, diags = substitute(body, "oracle_oasys", DispatchConfig(oasys_schema="SCHEMA"), _SPAN, _BLOCK)
    assert "@@SQLMACRO:0@@" in new_body
    assert "@OASYSSCHEMA@" not in new_body
    assert "SCHEMA.P_X" in new_body


# --- Body with no placeholders ---


def test_no_placeholder_returns_body_unchanged() -> None:
    body = "SELECT 1 FROM DUAL"
    new_body, diags = substitute(body, "oracle_mars", None, _SPAN, _BLOCK)
    assert new_body == body
    assert not diags
