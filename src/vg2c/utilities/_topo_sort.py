from __future__ import annotations

import heapq
from typing import TypeVar

T = TypeVar("T")

__all__ = ["topological_sort"]


def topological_sort(
    nodes: dict[str, T],
    edges: dict[str, set[str]],
) -> list[str]:
    """Return dependency-first order for a directed acyclic graph.

    ``edges`` maps node -> dependencies.
    """
    indegree: dict[str, int] = {name: 0 for name in nodes}
    dependents: dict[str, set[str]] = {name: set() for name in nodes}

    for name, deps in edges.items():
        if name not in nodes:
            continue
        for dep in deps:
            if dep not in nodes:
                raise ValueError(f"Unknown dependency {dep!r} for node {name!r}")
            indegree[name] += 1
            dependents[dep].add(name)

    heap = [name for name, degree in indegree.items() if degree == 0]
    heapq.heapify(heap)

    ordered: list[str] = []
    while heap:
        name = heapq.heappop(heap)
        ordered.append(name)

        for dependent in sorted(dependents[name]):
            indegree[dependent] -= 1
            if indegree[dependent] == 0:
                heapq.heappush(heap, dependent)

    if len(ordered) != len(nodes):
        cycle_members = sorted(name for name, degree in indegree.items() if degree > 0)
        raise ValueError(
            "Dependency cycle detected among utilities: " + " -> ".join(cycle_members)
        )

    return ordered
