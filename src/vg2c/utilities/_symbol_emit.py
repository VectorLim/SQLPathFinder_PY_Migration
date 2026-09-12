"""Render a resolved symbol selection; no source discovery happens here."""

from __future__ import annotations

import ast
import copy
from collections.abc import Iterable
from dataclasses import dataclass

from ._symbol_index import IMPORTS, ResolutionError, SymbolRef, bound_names
from ._symbols import Selection
from ._topo_sort import topological_sort


@dataclass(frozen=True)
class EmbeddedSymbols:
    imports: tuple[str, ...]
    sources: tuple[str, ...]
    context_expression: str


def render_symbols(
    selection: Selection, *, external_imports: Iterable[str] = ()
) -> EmbeddedSymbols:
    index = selection.index

    def container(ref: SymbolRef) -> SymbolRef:
        while index.symbol(ref).owner:
            ref = index.symbol(ref).owner
        return ref

    def render_node(ref: SymbolRef) -> list[ast.stmt]:
        nodes = copy.deepcopy(selection.nodes[ref])
        if index.symbol(ref).is_class:
            children = {child for child in selection.nodes if index.symbol(child).owner == ref}
            body = [node for child in children for node in render_node(child)]
            nodes[0].body = sorted(body, key=lambda n: n.lineno) or [ast.Pass()]
        return nodes

    roots = {ref for ref in selection.nodes if not index.symbol(ref).owner}
    if any(
        f.boundary == "module" and f.reason == "dynamic global lookup" for f in selection.fallbacks
    ):
        if len({ref.module for ref in roots}) > 1:
            raise ResolutionError(
                "Dynamic global lookup across flattened modules requires namespace identity"
            )
        for ref in roots:
            for node in index.symbol(ref).nodes:
                if isinstance(node, ast.Import) and any(index.path_for(a.name) for a in node.names):
                    raise ResolutionError(
                        "Dynamic global lookup of a project module requires namespace identity"
                    )
    for ref in roots:
        nodes = index.symbol(ref).nodes
        if len(nodes) > 1:
            start, end = nodes[0].lineno, nodes[-1].lineno
            if any(
                other != ref
                and other.module == ref.module
                and start < index.symbol(other).nodes[0].lineno < end
                for other in roots
            ):
                raise ResolutionError(f"Interleaved rebinding of {ref} cannot be safely reordered")
    edges: dict[str, set[str]] = {str(ref): set() for ref in roots}
    for ref, dependencies in selection.dependencies.items():
        owner = container(ref)
        for dep in dependencies:
            target = container(dep.target)
            if dep.eager and target != owner:
                edges[str(owner)].add(str(target))
    # Preserve module initialization order wherever effects make ordering observable.
    for module in index.modules.values():
        if module.effects:
            ordered = sorted(
                (
                    ref
                    for ref in roots
                    if ref.module == module.name and not index.symbol(ref).generated
                ),
                key=lambda ref: index.symbol(ref).nodes[0].lineno,
            )
            for before, after in zip(ordered, ordered[1:], strict=False):
                edges[str(after)].add(str(before))
    try:
        order = topological_sort({str(ref): ref for ref in roots}, edges)
    except ValueError as exc:
        raise ResolutionError(f"Unsatisfiable definition-time dependency order: {exc}") from exc
    by_name = {str(ref): ref for ref in roots}
    imports = set(external_imports)
    sources: list[str] = []
    bindings: dict[str, tuple[str, SymbolRef]] = {}

    def check_bindings(node: ast.stmt, source: str, ref: SymbolRef) -> None:
        for binding in bound_names([node]).names:
            identity = source if isinstance(node, IMPORTS) else str(ref)
            previous = bindings.get(binding)
            if previous and previous[0] != identity:
                raise ResolutionError(
                    f"Flattened binding collision for {binding!r}: {previous[1]} and {ref}"
                )
            bindings[binding] = identity, ref

    for source in sorted(imports):
        for node in ast.parse(source).body:
            check_bindings(node, ast.unparse(node), SymbolRef("<external-reader>", source))
    for name in order:
        ref = by_name[name]
        for node in render_node(ref):
            ast.fix_missing_locations(node)
            source = ast.unparse(node)
            check_bindings(node, source, ref)
            if isinstance(node, IMPORTS):
                imports.add(source)
            else:
                sources.append(source)
    instances = ", ".join(
        f"{name!r}: {ref.name}()" for name, ref in sorted(selection.context.items())
    )
    return EmbeddedSymbols(
        tuple(sorted(imports)), tuple(sources), f"PipelineContext({{{instances}}})"
    )
