from __future__ import annotations

from dataclasses import replace

from vg2c.emitter.models import (
    EmittedInvocation,
    EmittedParameter,
    EmittedStep,
    ExternalParameter,
    RenderedArgument,
    RenderedCall,
    SourceRange,
    StepEmission,
    _RelativeInvocation,
)
from vg2c.kind import Kind


def record_inline_step(
    writer,
    block,
    lines,
    *,
    external_parameters: tuple[ExternalParameter, ...] = (),
) -> StepEmission:
    prefix = " " * writer.indent_depth * writer.indent_step
    fragments = []
    invocations = []
    cursor = 0
    for line in lines:
        source = str(line)
        fragment = "\n".join(
            prefix + part if part.strip() else "" for part in source.splitlines()
        )
        if isinstance(line, RenderedCall):
            start = cursor + len(prefix)
            invocations.append(
                _RelativeInvocation(
                    operation=line.definition,
                    source_range=SourceRange(start, cursor + len(fragment)),
                    arguments=tuple(
                        replace(
                            argument,
                            source_range=SourceRange(
                                start
                                + argument.source_range.start_offset
                                + len(prefix)
                                * source[: argument.source_range.start_offset].count("\n"),
                                start
                                + argument.source_range.end_offset
                                + len(prefix)
                                * source[: argument.source_range.end_offset].count("\n"),
                            ),
                        )
                        for argument in line.arguments
                    ),
                    semantic_key=line.semantic_key,
                )
            )
        fragments.append(fragment)
        writer.write_block(source)
        cursor += len(fragment) + 1
    if not fragments:
        fragments = [prefix + "pass"]
        writer.write("pass")
    suffix = {Kind.EXTERNAL_RUN: "external", Kind.EMAIL: "email"}.get(
        block.kind, block.kind.value.lower()
    )
    return StepEmission(
        function_name=f"step_{block.index:04d}_{suffix}",
        block_index=block.index,
        functional_kind=block.kind.value,
        source="\n".join(fragments),
        call_site="",
        invocations=tuple(invocations),
        external_parameters=tuple(external_parameters),
    )


def build_step_emission(
    *,
    function_name: str,
    block_index: int,
    functional_kind: str,
    body_lines: list[str],
) -> StepEmission:
    """Wrap emitted body lines while preserving top-level rendered-call metadata."""
    lines = [f"def {function_name}(ctx) -> None:"]
    invocations: list[_RelativeInvocation] = []

    if not body_lines:
        lines.append("    pass")
    else:
        for body_line in body_lines:
            rendered = body_line if isinstance(body_line, RenderedCall) else None
            call_start = _joined_length(lines) + 4
            source_text = str(body_line)
            for line in source_text.split("\n"):
                lines.append(f"    {line}" if line.strip() else "")
            if rendered is None:
                continue

            exact_call = _indent_call_source(str(rendered))[4:]
            call_end = call_start + len(exact_call)
            invocations.append(
                _RelativeInvocation(
                    operation=rendered.definition,
                    source_range=SourceRange(call_start, call_end),
                    arguments=tuple(
                        _adjust_argument_for_indentation(
                            argument, rendered, exact_call, call_start
                        )
                        for argument in rendered.arguments
                    ),
                    semantic_key=rendered.semantic_key,
                )
            )

    return StepEmission(
        function_name=function_name,
        block_index=block_index,
        functional_kind=functional_kind,
        source="\n".join(lines),
        call_site=f"{function_name}(ctx)",
        invocations=tuple(invocations),
    )


def finalize_steps(
    source: str,
    emissions: list[StepEmission],
    global_parameters: dict[str, EmittedParameter] | None = None,
) -> tuple[EmittedStep, ...]:
    """Convert emitter-relative metadata into absolute ranges in the final source."""
    global_parameters = global_parameters or {}
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
                function_name=emission.function_name,
                block_index=emission.block_index,
                functional_kind=emission.functional_kind,
                source=emission.source,
                source_range=SourceRange(step_start, step_end),
                invocations=_finalize_invocations(emission, step_start, global_parameters),
            )
        )
    return tuple(finalized)


def _finalize_invocations(
    emission: StepEmission,
    step_start: int,
    global_parameters: dict[str, EmittedParameter],
) -> tuple[EmittedInvocation, ...]:
    seen_invocation_ids: set[str] = set()
    invocations = []
    for relative in emission.invocations:
        semantic_key = relative.semantic_key or "default"
        invocation_id = (
            f"block-{emission.block_index}:{relative.operation.id}:{semantic_key}"
        )
        if invocation_id in seen_invocation_ids:
            raise ValueError(
                "ambiguous emitted invocation identity for "
                f"block {emission.block_index} and {relative.operation.id}; "
                "use RenderedCall.with_key() for repeated operations"
            )
        seen_invocation_ids.add(invocation_id)
        parameters = _finalize_parameters(
            relative,
            invocation_id,
            step_start,
            global_parameters,
            emission.external_parameters,
        )
        invocations.append(
            EmittedInvocation(
                id=invocation_id,
                operation=relative.operation,
                source_range=SourceRange(
                    step_start + relative.source_range.start_offset,
                    step_start + relative.source_range.end_offset,
                ),
                parameters=parameters,
                arguments=relative.arguments,
            )
        )
    return tuple(invocations)


def _finalize_parameters(
    relative: _RelativeInvocation,
    invocation_id: str,
    step_start: int,
    global_parameters: dict[str, EmittedParameter],
    external_parameters: tuple[ExternalParameter, ...],
) -> tuple[EmittedParameter, ...]:
    parameters: list[EmittedParameter] = []
    for argument in relative.arguments:
        if argument.global_names and argument.source in argument.global_names:
            parameters.append(
                replace(
                    global_parameters[argument.source],
                    name=argument.name,
                    position=argument.position,
                    definition=argument.definition,
                    artifact_role=(
                        argument.definition.artifact_role if argument.definition else None
                    ),
                )
            )
            continue
        key = (
            argument.definition.name
            if argument.definition
            else (
                f"pos-{argument.position}"
                if argument.position is not None
                else argument.name
            )
        )
        parameters.append(
            EmittedParameter(
                id=f"{invocation_id}:{key}",
                name=argument.name,
                position=argument.position,
                source=argument.source,
                value=argument.value,
                editor_type=argument.editor_type,
                editable=argument.editable,
                read_only_reason=argument.read_only_reason,
                definition=argument.definition,
                artifact_role=(
                    argument.definition.artifact_role if argument.definition else None
                ),
                source_range=SourceRange(
                    step_start + argument.source_range.start_offset,
                    step_start + argument.source_range.end_offset,
                ),
                symbol_names=argument.symbol_names,
            )
        )
        parameters.extend(
            global_parameters[name]
            for name in argument.global_names
            if global_parameters[name].id
            not in {parameter.id for parameter in parameters}
        )
    supplied_names = {argument.name for argument in relative.arguments}
    for definition in relative.operation.parameters:
        schema = definition.schema
        if (
            definition.name in supplied_names
            or definition.required
            or definition.visibility == "internal"
            or not definition.keyword
        ):
            continue
        if (
            schema is None
            or schema.kind == "dynamic"
            or not schema.accepts(definition.default)
        ):
            continue
        parameters.append(
            EmittedParameter(
                id=f"{invocation_id}:{definition.name}",
                name=definition.name,
                position=None,
                source=repr(definition.default),
                value=definition.default,
                editor_type=schema.kind,
                editable=True,
                read_only_reason=None,
                definition=definition,
                artifact_role=definition.artifact_role,
                source_range=None,
            )
        )
    parameters = [_typed_parameter(parameter) for parameter in parameters]
    for external in external_parameters:
        name = external.definition.name
        if any(
            parameter.name == name and parameter.source_range is not None
            for parameter in parameters
        ):
            continue
        parameters = [parameter for parameter in parameters if parameter.name != name]
        parameters.append(
            EmittedParameter(
                id=f"{invocation_id}:{name}",
                name=name,
                position=None,
                source=repr(external.value),
                value=external.value,
                editor_type=external.editor_type,
                editable=False,
                read_only_reason=external.read_only_reason,
                definition=replace(external.definition, default=external.value),
                artifact_role=None,
                source_range=None,
            )
        )
    return tuple(parameters)


def _adjust_argument_for_indentation(
    argument: RenderedArgument,
    rendered: RenderedCall,
    exact_call: str,
    call_start: int,
) -> RenderedArgument:
    start = _indent_adjusted_offset(str(rendered), argument.source_range.start_offset)
    end = _indent_adjusted_offset(str(rendered), argument.source_range.end_offset)
    return RenderedArgument(
        name=argument.name,
        position=argument.position,
        source=exact_call[start:end],
        value=argument.value,
        editor_type=argument.editor_type,
        editable=argument.editable,
        read_only_reason=argument.read_only_reason,
        definition=argument.definition,
        source_range=SourceRange(call_start + start, call_start + end),
        global_names=argument.global_names,
        symbol_names=argument.symbol_names,
    )


def _indent_call_source(source: str) -> str:
    return "\n".join(
        f"    {line}" if line.strip() else "" for line in source.split("\n")
    )


def _indent_adjusted_offset(source: str, offset: int) -> int:
    line_index = source[:offset].count("\n")
    lines = source.split("\n")
    extra = sum(4 for line in lines[1 : line_index + 1] if line.strip())
    return offset + extra


def _joined_length(lines: list[str]) -> int:
    return len("\n".join(lines)) + (1 if lines else 0)


def _typed_parameter(parameter: EmittedParameter) -> EmittedParameter:
    definition = parameter.definition
    if definition is None:
        return parameter
    if definition.visibility == "internal":
        return replace(
            parameter, editable=False, read_only_reason="Runtime/internal parameter"
        )
    schema = definition.schema
    if schema is None:
        return parameter
    if schema.kind == "dynamic":
        return replace(
            parameter, editable=False, read_only_reason="Unsupported declared type"
        )
    if parameter.read_only_reason is not None:
        return parameter
    if not schema.accepts(parameter.value):
        return replace(
            parameter,
            editable=False,
            read_only_reason="Value does not match its declared type",
        )
    editor_type = (
        "multiline"
        if parameter.editor_type == "multiline" and schema.kind == "string"
        else schema.kind
    )
    return replace(
        parameter, editor_type=editor_type, editable=True, read_only_reason=None
    )


__all__ = ["build_step_emission", "finalize_steps", "record_inline_step"]
