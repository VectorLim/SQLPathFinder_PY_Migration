from __future__ import annotations

import pytest

from vg2c.emitter.utilities._topo_sort import topological_sort


def test_topological_sort_linear_chain() -> None:
    nodes = {"a": object(), "b": object(), "c": object()}
    edges = {"a": {"b"}, "b": {"c"}, "c": set()}

    ordered = topological_sort(nodes, edges)

    assert ordered == ["c", "b", "a"]


def test_topological_sort_diamond() -> None:
    nodes = {"a": object(), "b": object(), "c": object(), "d": object()}
    edges = {"a": {"b", "c"}, "b": {"d"}, "c": {"d"}, "d": set()}

    ordered = topological_sort(nodes, edges)

    assert ordered.index("d") < ordered.index("b")
    assert ordered.index("d") < ordered.index("c")
    assert ordered.index("b") < ordered.index("a")
    assert ordered.index("c") < ordered.index("a")


def test_topological_sort_cycle_raises() -> None:
    nodes = {"a": object(), "b": object()}
    edges = {"a": {"b"}, "b": {"a"}}

    with pytest.raises(ValueError, match="Dependency cycle detected"):
        topological_sort(nodes, edges)


def test_topological_sort_no_dependencies_is_alphabetical() -> None:
    nodes = {"z": object(), "a": object(), "m": object()}
    edges = {"z": set(), "a": set(), "m": set()}

    ordered = topological_sort(nodes, edges)

    assert ordered == ["a", "m", "z"]
