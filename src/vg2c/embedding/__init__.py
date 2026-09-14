"""Compiler-only selection and rendering of embedded runtime code."""

from __future__ import annotations

from pathlib import Path
from typing import TYPE_CHECKING

from vg2c.embedding.index import SymbolRef
from vg2c.embedding.renderer import EmbeddedSymbols, render_symbols
from vg2c.embedding.resolver import SymbolResolver
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec

if TYPE_CHECKING:
    from vg2c.emitter.models import StepEmission


def assemble_utilities(
    *,
    step_emissions: list[StepEmission] | tuple[StepEmission, ...],
    workflow_source: str,
    reader_names: set[str],
    reader_imports: set[str] | frozenset[str] = frozenset(),
) -> EmbeddedSymbols:
    """Resolve the runtime closure required by emitted steps and workflow code."""
    ensure_utility_checks_loaded()
    utilities = {
        name: SymbolRef(cls.__module__, cls.__name__) for name, cls in UtilitySpec._registry.items()
    }
    resolver = SymbolResolver(Path(__file__).resolve().parents[2], utilities)
    for step in step_emissions:
        for invocation in step.invocations:
            operation = invocation.operation
            resolver.context_member(operation.utility_name, operation.method)
    for name in sorted(reader_names):
        resolver.seed_runtime_api(utilities[name])
    resolver.require(utilities["ctx"])
    resolver.require(utilities["logger"])
    resolver.scan_workflow(
        "\n\n".join([*(step.source for step in step_emissions), workflow_source])
    )
    resolver.drain()
    resolver.expand_runtime_apis()
    return render_symbols(resolver.drain(), external_imports=reader_imports)


__all__ = ["assemble_utilities"]
