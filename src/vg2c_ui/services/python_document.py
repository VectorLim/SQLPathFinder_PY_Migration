from __future__ import annotations

import ast
from dataclasses import dataclass

from vg2c.emitter import STEPS_END, STEPS_START, WORKFLOW_END, WORKFLOW_START
from vg2c_ui.domain.models import ParameterDescriptor


class InvalidGeneratedDocument(ValueError):
    pass


@dataclass(frozen=True, slots=True)
class ParsedParameter:
    descriptor: ParameterDescriptor
    call_target: str | None
    start_offset: int
    end_offset: int


@dataclass(frozen=True, slots=True)
class ParsedFunction:
    name: str
    description: str
    raw_code: str
    parameters: tuple[ParsedParameter, ...]


@dataclass(frozen=True, slots=True)
class ParsedPythonDocument:
    source: str
    steps: dict[str, ParsedFunction]
    workflow_function: ParsedFunction

    def parameter(self, function_name: str, parameter_id: str) -> ParsedParameter | None:
        function = self.steps.get(function_name)
        if function is None:
            return None
        return next(
            (item for item in function.parameters if item.descriptor.id == parameter_id),
            None,
        )


@dataclass(frozen=True, slots=True)
class _Region:
    source: str
    start_offset: int


def parse_generated_python(source: str) -> ParsedPythonDocument:
    step_region = _region(source, STEPS_START, STEPS_END)
    workflow_region = _region(source, WORKFLOW_START, WORKFLOW_END)
    step_items = _parse_functions(step_region)
    if len({item.name for item in step_items}) != len(step_items):
        raise InvalidGeneratedDocument("step region contains duplicate function names")
    steps = {item.name: item for item in step_items}
    workflows = _parse_functions(workflow_region)
    run = next((item for item in workflows if item.name == "run"), None)
    if run is None:
        raise InvalidGeneratedDocument("workflow region does not contain run()")
    return ParsedPythonDocument(source=source, steps=steps, workflow_function=run)


def _region(source: str, start_marker: str, end_marker: str) -> _Region:
    if source.count(start_marker) != 1 or source.count(end_marker) != 1:
        raise InvalidGeneratedDocument(
            f"expected exactly one generated region: {start_marker} / {end_marker}"
        )
    start = source.index(start_marker) + len(start_marker)
    end = source.index(end_marker, start)
    if end < start:
        raise InvalidGeneratedDocument(
            f"missing or unordered generated region: {start_marker} / {end_marker}"
        )
    return _Region(source=source[start:end], start_offset=start)


def _parse_functions(region: _Region) -> list[ParsedFunction]:
    try:
        tree = ast.parse(region.source)
    except SyntaxError as exc:
        raise InvalidGeneratedDocument(f"invalid marked Python region: {exc}") from exc

    functions: list[ParsedFunction] = []
    for node in tree.body:
        if not isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef)):
            continue
        raw = ast.get_source_segment(region.source, node) or ""
        functions.append(
            ParsedFunction(
                name=node.name,
                description=ast.get_docstring(node, clean=True) or "No description provided",
                raw_code=raw,
                parameters=tuple(_extract_parameters(node, region)),
            )
        )
    return functions


def _extract_parameters(
    function: ast.FunctionDef | ast.AsyncFunctionDef, region: _Region
) -> list[ParsedParameter]:
    calls = sorted(
        (node for node in ast.walk(function) if isinstance(node, ast.Call)),
        key=lambda node: (node.lineno, node.col_offset),
    )
    parameters: list[ParsedParameter] = []
    for call_index, call in enumerate(calls):
        call_target = _call_target(call.func)
        for position, argument in enumerate(call.args):
            parameters.append(
                _parameter(
                    function.name,
                    call_index,
                    f"arg_{position + 1}",
                    position,
                    argument,
                    region,
                    call_target,
                )
            )
        for keyword in call.keywords:
            if keyword.arg is not None:
                parameters.append(
                    _parameter(
                        function.name,
                        call_index,
                        keyword.arg,
                        None,
                        keyword.value,
                        region,
                        call_target,
                    )
                )
    return parameters


def _parameter(
    function_name: str,
    call_index: int,
    name: str,
    position: int | None,
    node: ast.AST,
    region: _Region,
    call_target: str | None,
) -> ParsedParameter:
    representation = ast.get_source_segment(region.source, node) or ast.unparse(node)
    try:
        value = ast.literal_eval(node)
    except (ValueError, TypeError, SyntaxError):
        value = None
        editor_type = "dynamic"
        editable = False
        reason = "Dynamic Python expressions are read-only"
    else:
        if isinstance(value, bool):
            editor_type = "boolean"
        elif isinstance(value, int):
            editor_type = "integer"
        elif isinstance(value, list) and all(_safe_list_item(item) for item in value):
            editor_type = "list"
        elif isinstance(value, str) and "\n" in value:
            editor_type = "multiline"
        elif isinstance(value, str):
            editor_type = "string"
        else:
            editor_type = "dynamic"
        editable = editor_type != "dynamic"
        reason = None if editable else "This literal type is not supported"
        if not editable:
            value = None
    key = f"pos:{position}" if position is not None else f"kw:{name}"
    start = region.start_offset + _offset(region.source, node.lineno, node.col_offset)
    end = region.start_offset + _offset(
        region.source,
        getattr(node, "end_lineno", node.lineno),
        getattr(node, "end_col_offset", node.col_offset),
    )
    return ParsedParameter(
        descriptor=ParameterDescriptor(
            id=f"{function_name}:{call_index}:{key}",
            name=name,
            position=position,
            source=representation,
            value=value,
            editor_type=editor_type,
            editable=editable,
            read_only_reason=reason,
        ),
        call_target=call_target,
        start_offset=start,
        end_offset=end,
    )


def _call_target(node: ast.AST) -> str | None:
    if isinstance(node, ast.Name):
        return node.id
    if isinstance(node, ast.Attribute):
        parent = _call_target(node.value)
        return f"{parent}.{node.attr}" if parent else node.attr
    return None


def _offset(source: str, line_number: int, byte_column: int) -> int:
    lines = source.splitlines(keepends=True)
    line = lines[line_number - 1]
    character_column = len(line.encode("utf-8")[:byte_column].decode("utf-8"))
    return sum(len(item) for item in lines[: line_number - 1]) + character_column


def _safe_list_item(value: object) -> bool:
    return value is None or isinstance(value, (str, int, float, bool))


__all__ = [
    "InvalidGeneratedDocument",
    "ParsedFunction",
    "ParsedParameter",
    "ParsedPythonDocument",
    "parse_generated_python",
]
