from __future__ import annotations

from vg2c.emitter.models import (
    EmittedParameter,
    EmittedStep,
    RenderedCall,
    SourceRange,
    StepEmission,
)
from vg2c.kind import Kind


def record_inline_step(
    writer,
    block,
    lines,
    *,
    functional_kind: str | None = None,
) -> StepEmission:
    prefix = " " * writer.indent_depth * writer.indent_step
    fragments = []
    parameters = []
    for line in lines:
        source = str(line)
        fragment = "\n".join(
            prefix + part if part.strip() else "" for part in source.splitlines()
        )
        if isinstance(line, RenderedCall):
            parameters.extend(
                EmittedParameter(argument.value, argument.artifact_role)
                for argument in line.arguments
            )
        fragments.append(fragment)
        writer.write_block(source)
    if not fragments:
        fragments = [prefix + "pass"]
        writer.write("pass")
    suffix = (
        functional_kind.lower()
        if functional_kind
        else (
            {Kind.EXTERNAL_RUN: "external", Kind.EMAIL: "email"}.get(
                block.kind, block.kind.value.lower()
            )
        )
    )
    return StepEmission(
        function_name=f"step_{block.index:04d}_{suffix}",
        block_index=block.index,
        functional_kind=functional_kind or block.kind.value,
        source="\n".join(fragments),
        parameters=tuple(parameters),
    )


def finalize_steps(
    source: str,
    emissions: list[StepEmission],
) -> tuple[EmittedStep, ...]:
    """Attach final generated-source ranges to emitted steps."""
    finalized: list[EmittedStep] = []
    search_from = 0
    for emission in emissions:
        step_start = source.find(emission.source, search_from)
        if step_start < 0:
            raise ValueError(
                f"emitted step not found in final source: {emission.function_name}"
            )
        step_end = step_start + len(emission.source)
        search_from = step_end
        finalized.append(
            EmittedStep(
                block_index=emission.block_index,
                functional_kind=emission.functional_kind,
                source_range=SourceRange(step_start, step_end),
                parameters=emission.parameters,
            )
        )
    return tuple(finalized)


__all__ = ["finalize_steps", "record_inline_step"]
