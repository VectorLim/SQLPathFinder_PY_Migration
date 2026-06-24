from __future__ import annotations

from vg2c.dispatch.view_registry import load, lookup


def test_load_none_path_returns_empty() -> None:
    assert load(None) == {}


def test_load_nonexistent_path_returns_empty(tmp_path) -> None:
    # Even when given a path, v1 always returns empty
    assert load(tmp_path / "missing.yaml") == {}


def test_lookup_always_returns_none() -> None:
    registry = load(None)
    assert lookup(registry, "oracle_mars", "F_LotHist") is None
    assert lookup(registry, "oracle_oasys", "P_SPC_Batch") is None
    assert lookup(registry, "sqlite", "some_table") is None
