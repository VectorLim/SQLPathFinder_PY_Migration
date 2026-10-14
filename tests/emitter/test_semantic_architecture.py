"""Architecture constraints for semantic projection ownership."""

from pathlib import Path

import pytest

import vg2c
from vg2c.semantic.models import Symbol
from vg2c.semantic.symbols import CONDITION_OPERATORS, condition_symbol_token


def test_condition_operator_contract_is_preserved():
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


def test_condition_symbol_tokens_remain_operator_typed():
    symbol = Symbol("symbol:COUNT", "COUNT", "macro", "runtime")
    assert condition_symbol_token(symbol, "GT") == "COUNT"
    assert condition_symbol_token(symbol, "EQS") == "VAR(COUNT)"
    with pytest.raises(ValueError, match="not a condition symbol"):
        condition_symbol_token(Symbol("symbol:SITE", "SITE", "global", "known"), "EQS")


def test_legacy_semantic_facades_and_imports_are_absent():
    package = Path(vg2c.__file__).parent
    assert not (package / "semantics.py").exists()
    assert not list(package.glob("_semantic_*.py"))

    legacy_imports = []
    for path in package.rglob("*.py"):
        text = path.read_text(encoding="utf-8")
        if "vg2c.semantics" in text or "vg2c._semantic_" in text:
            legacy_imports.append(path.relative_to(package))
    assert legacy_imports == []
