from __future__ import annotations

import csv
import re
from collections import defaultdict
from dataclasses import dataclass, replace
from pathlib import Path
from typing import TYPE_CHECKING, Any, Literal

from vg2c.emitter.models import SourceRange
from vg2c.kind import Kind
from vg2c.operands import IfThen, RunLoop, ScopeNode, StartMacro
from vg2c.operands.base import _OPERATOR_TABLE
from vg2c.utilities._emit_helpers import split_utility_command
from vg2c.utilities._runtime_helpers import normalize_macro_name, strip_quotes
from vg2c.utilities.macro_state import MacroState
from vg2c.utility_metadata import ValueSchema

if TYPE_CHECKING:
    from vg2c.compilation import CompilationResult
    from vg2c.frontend.models import SourceSpan


BindingVisibility = Literal["normal", "advanced", "internal"]
ValidationState = Literal["valid", "warning", "unresolved", "unsupported"]
SymbolKind = Literal["global", "macro", "macro-row", "unresolved"]
SymbolValueState = Literal["known", "runtime", "unknown"]


@dataclass(frozen=True, slots=True)
class OperationReference:
    operation_id: str
    binding_id: str | None = None


@dataclass(frozen=True, slots=True)
class EditableBinding:
    id: str
    owner_operation_id: str
    name: str
    display_label: str
    schema: ValueSchema | None
    value: Any
    default: Any = None
    required: bool = True
    visibility: BindingVisibility = "normal"
    capabilities: tuple[str, ...] = ()
    validation_state: ValidationState = "valid"
    resettable: bool = True
    editable: bool = True
    read_only_reason: str | None = None
    source_range: SourceRange | None = None
    source_kind: str = "parameter"


@dataclass(frozen=True, slots=True)
class WorkflowOperation:
    id: str
    kind: str
    display_name: str
    description: str
    parent_operation_id: str | None
    branch: Literal["true", "false"] | None
    source_span: SourceSpan
    summary: str = ""
    bindings: tuple[EditableBinding, ...] = ()
    capabilities: tuple[str, ...] = ()
    comments: tuple[str, ...] = ()
    validation_state: ValidationState = "valid"
    visibility: BindingVisibility = "normal"
    block_index: int = -1
    source_range: SourceRange | None = None


@dataclass(frozen=True, slots=True)
class SymbolReference:
    operation_id: str
    binding_id: str


@dataclass(frozen=True, slots=True)
class Symbol:
    id: str
    display_name: str
    kind: SymbolKind
    value_state: SymbolValueState
    value: Any = None
    introduction: OperationReference | None = None
    references: tuple[SymbolReference, ...] = ()

    @property
    def condition_value(self) -> str | None:
        """Canonical condition token for selectable runtime macro symbols."""
        if self.kind in {"macro", "macro-row"}:
            return f"VAR({self.display_name})"
        return None


@dataclass(frozen=True, slots=True)
class SemanticModel:
    operations: tuple[WorkflowOperation, ...]
    bindings: tuple[EditableBinding, ...]
    symbols: tuple[Symbol, ...]

    def binding(self, binding_id: str) -> EditableBinding | None:
        return next((item for item in self.bindings if item.id == binding_id), None)

    def operation(self, operation_id: str) -> WorkflowOperation | None:
        return next((item for item in self.operations if item.id == operation_id), None)


CONDITION_OPERATORS = tuple(
    (code, symbol, operand_type)
    for code, (symbol, operand_type) in _OPERATOR_TABLE.items()
)


def build_semantic_model(
    result: CompilationResult,
    values: dict[str, Any] | None = None,
) -> SemanticModel:
    """Project one authoritative semantic model from resolver controls and emitted metadata."""
    values = values or {}
    block_by_index = {block.index: block for block in result.resolved.blocks}
    control_ranges = _control_source_ranges(result)

    operations: list[WorkflowOperation] = []
    handled_blocks: set[int] = set()

    def visit(node: ScopeNode, parent_operation: str | None = None, branch=None) -> None:
        next_parent = parent_operation
        next_branch = branch
        if node.kind in {"if", "macro", "loop"}:
            operation = _control_operation(
                result,
                node,
                block_by_index[node.start_index],
                parent_operation,
                branch,
                values,
                control_ranges.get(node.scope_id),
            )
            operations.append(operation)
            handled_blocks.add(node.start_index)
            next_parent = operation.id
            next_branch = None
        elif node.kind == "if-branch":
            next_branch = "true"
        elif node.kind == "else-branch":
            next_branch = "false"
        elif node.kind == "leaf" and node.block_index is not None:
            block = block_by_index.get(node.block_index)
            if block is not None:
                leaf_ops = _leaf_operations(
                    result,
                    block,
                    parent_operation,
                    branch,
                    values,
                )
                operations.extend(leaf_ops)
                if leaf_ops:
                    handled_blocks.add(block.index)
        for child in node.children:
            visit(child, next_parent, next_branch)

    visit(result.resolved.scope_tree)

    # Orphan/unsupported source blocks can be absent from the structural walk.
    for block in result.resolved.blocks:
        if block.index in handled_blocks:
            continue
        operations.extend(_leaf_operations(result, block, None, None, values))

    bindings = tuple(binding for operation in operations for binding in operation.bindings)
    symbols = _build_symbols(result, operations)
    operations = _apply_condition_symbol_validation(operations, symbols)
    bindings = tuple(binding for operation in operations for binding in operation.bindings)
    return SemanticModel(tuple(operations), bindings, symbols)


def _control_operation(
    result: CompilationResult,
    node: ScopeNode,
    block,
    parent_operation_id: str | None,
    branch: Literal["true", "false"] | None,
    values: dict[str, Any],
    source_range: SourceRange | None,
) -> WorkflowOperation:
    payload = node.control_payload
    operation_id = _control_operation_id(node)

    if isinstance(payload, IfThen):
        names = ("lhs", "op", "rhs", "conj", "lhs2", "op2", "rhs2")
        labels = {
            "lhs": "Left value",
            "op": "Operator",
            "rhs": "Comparison value",
            "conj": "Connector",
            "lhs2": "Second left value",
            "op2": "Second operator",
            "rhs2": "Second comparison value",
        }
        bindings = []
        for name in names:
            binding_id = f"{operation_id}:{name}"
            value = values.get(binding_id, getattr(payload, name))
            if name in {"op", "op2"}:
                schema = ValueSchema(
                    "string",
                    nullable=name == "op2",
                    choices=tuple(_OPERATOR_TABLE),
                )
            elif name == "conj":
                schema = ValueSchema("string", nullable=True, choices=("AND", "OR"))
            else:
                schema = ValueSchema("string", nullable=name.endswith("2"))
            capability = (
                "condition-operator"
                if name in {"op", "op2"}
                else "condition-connector"
                if name == "conj"
                else "symbol-or-literal"
            )
            bindings.append(
                EditableBinding(
                    id=binding_id,
                    owner_operation_id=operation_id,
                    name=name,
                    display_label=labels[name],
                    schema=schema,
                    value=value,
                    default=getattr(payload, name),
                    required=name in {"lhs", "op", "rhs"},
                    capabilities=(capability,),
                    source_kind="condition",
                )
            )
        return WorkflowOperation(
            id=operation_id,
            kind="condition",
            display_name="IF",
            description="Conditional branch using the compiler's VG2 condition semantics.",
            parent_operation_id=parent_operation_id,
            branch=branch,
            source_span=block.span,
            summary=_condition_summary(bindings),
            bindings=tuple(bindings),
            capabilities=("condition-editor",),
            block_index=block.index,
            source_range=source_range,
        )

    if isinstance(payload, StartMacro):
        binding_id = f"{operation_id}:csv_path"
        binding = EditableBinding(
            id=binding_id,
            owner_operation_id=operation_id,
            name="csv_path",
            display_label="Input file",
            schema=ValueSchema("string", path=True),
            value=values.get(binding_id, payload.csv_path),
            default=payload.csv_path,
            capabilities=("file-input",),
            source_kind="macro",
        )
        return WorkflowOperation(
            id=operation_id,
            kind="macro-loop",
            display_name="For Each Macro Row",
            description="Iterate once over each row-backed macro scope.",
            parent_operation_id=parent_operation_id,
            branch=branch,
            source_span=block.span,
            summary=_summary_value(binding.value),
            bindings=(binding,),
            capabilities=("file-input",),
            block_index=block.index,
            source_range=source_range,
        )

    if isinstance(payload, RunLoop):
        specs = (
            ("input_csv_path", "Input file", ValueSchema("string", path=True), ("file-input",)),
            ("chunk_csv_path", "Chunk file", ValueSchema("string", path=True), ("file-output",)),
            ("chunk_size", "Chunk size", ValueSchema("integer"), ()),
        )
        bindings = tuple(
            EditableBinding(
                id=f"{operation_id}:{name}",
                owner_operation_id=operation_id,
                name=name,
                display_label=label,
                schema=schema,
                value=values.get(f"{operation_id}:{name}", getattr(payload, name)),
                default=getattr(payload, name),
                capabilities=capabilities,
                source_kind="loop",
            )
            for name, label, schema, capabilities in specs
        )
        return WorkflowOperation(
            id=operation_id,
            kind="chunk-loop",
            display_name="For Each Chunk",
            description="Iterate over fixed-size chunks of an input file.",
            parent_operation_id=parent_operation_id,
            branch=branch,
            source_span=block.span,
            summary=f"{_summary_value(bindings[0].value)} · {bindings[2].value} rows",
            bindings=bindings,
            capabilities=("file-input", "file-output"),
            block_index=block.index,
            source_range=source_range,
        )

    return WorkflowOperation(
        id=operation_id,
        kind="unsupported",
        display_name="Unsupported / Unknown",
        description="Unsupported control operation.",
        parent_operation_id=parent_operation_id,
        branch=branch,
        source_span=block.span,
        validation_state="unsupported",
        block_index=block.index,
        source_range=source_range,
    )


def _leaf_operations(
    result: CompilationResult,
    block,
    parent_operation_id: str | None,
    branch: Literal["true", "false"] | None,
    values: dict[str, Any],
) -> list[WorkflowOperation]:
    step = result.emitted.step_for_block(block.index)

    if block.kind is Kind.ROWS_IN_FILE:
        return [_rows_in_file_operation(result, block, step, parent_operation_id, branch, values)]

    if block.kind is Kind.PYTHON_EMBED:
        operation_id = f"block-{block.index}:embedded-python"
        binding_id = f"{operation_id}:source"
        return [
            WorkflowOperation(
                id=operation_id,
                kind="embedded-python",
                display_name="Embedded Python",
                description="Embedded Python source for this block.",
                parent_operation_id=parent_operation_id,
                branch=branch,
                source_span=block.span,
                bindings=(
                    EditableBinding(
                        id=binding_id,
                        owner_operation_id=operation_id,
                        name="source",
                        display_label="Python source",
                        schema=ValueSchema("string"),
                        value=values.get(binding_id, block.resolved_body),
                        default=block.resolved_body,
                        capabilities=("embedded-python",),
                        source_kind="embedded-python",
                    ),
                ),
                capabilities=("embedded-python",),
                block_index=block.index,
                source_range=step.source_range if step else None,
            )
        ]

    operations: list[WorkflowOperation] = []
    if step is not None:
        for invocation in step.invocations:
            definition = invocation.operation
            if definition.visibility == "internal":
                continue
            bindings = []
            for parameter in invocation.parameters:
                definition_parameter = parameter.definition
                if (
                    definition_parameter is not None
                    and definition_parameter.visibility == "internal"
                ):
                    continue
                binding_value = values.get(parameter.id, parameter.value)
                capabilities = list(
                    definition_parameter.capabilities if definition_parameter else ()
                )
                if parameter.editor_type == "multiline":
                    capabilities.append("multiline")
                if parameter.artifact_role is not None:
                    capabilities.append(f"file-{parameter.artifact_role.direction}")
                bindings.append(
                    EditableBinding(
                        id=parameter.id,
                        owner_operation_id=invocation.id,
                        name=parameter.name,
                        display_label=(
                            definition_parameter.display_label
                            if definition_parameter else parameter.name.replace("_", " ").title()
                        ),
                        schema=definition_parameter.schema if definition_parameter else None,
                        value=binding_value,
                        default=parameter.value,
                        required=definition_parameter.required if definition_parameter else True,
                        visibility=(
                            definition_parameter.visibility if definition_parameter else "normal"
                        ),
                        capabilities=tuple(dict.fromkeys(capabilities)),
                        validation_state="valid" if parameter.editable else "warning",
                        resettable=(
                            parameter.source_range is None
                            or not parameter.id.startswith("global:")
                        ),
                        editable=parameter.editable,
                        read_only_reason=parameter.read_only_reason,
                        source_range=parameter.source_range,
                        source_kind="parameter",
                    )
                )
            operations.append(
                WorkflowOperation(
                    id=invocation.id,
                    kind=definition.id,
                    display_name=definition.display_name,
                    description=definition.method_description or definition.description,
                    parent_operation_id=parent_operation_id,
                    branch=branch,
                    source_span=block.span,
                    summary=_utility_summary(definition.summary_template, bindings),
                    bindings=tuple(bindings),
                    capabilities=definition.capabilities,
                    visibility=definition.visibility,
                    block_index=block.index,
                    source_range=invocation.source_range,
                )
            )

    if operations:
        return operations

    return [
        WorkflowOperation(
            id=f"block-{block.index}:unsupported",
            kind="unsupported",
            display_name="Unsupported / Unknown",
            description=f"{block.kind.value.replace('_', ' ').title()} block",
            parent_operation_id=parent_operation_id,
            branch=branch,
            source_span=block.span,
            validation_state="unsupported",
            block_index=block.index,
            source_range=step.source_range if step else None,
        )
    ]


def _rows_in_file_operation(
    result: CompilationResult,
    block,
    step,
    parent_operation_id: str | None,
    branch: Literal["true", "false"] | None,
    values: dict[str, Any],
) -> WorkflowOperation:
    operation_id = f"block-{block.index}:rows-in-file"
    argv = split_utility_command(block.resolved_options.lookup.get("UTILITIES", ""))
    path = strip_quotes(argv[1]) if len(argv) > 1 else ""
    target = strip_quotes(argv[2]).upper() if len(argv) > 2 else ""

    target_parameter = None
    if step is not None:
        target_parameter = next(
            (
                parameter
                for invocation in step.invocations
                for parameter in invocation.parameters
                if invocation.operation.id == "macro.set_named" and parameter.name == "name"
            ),
            None,
        )
    target_id = target_parameter.id if target_parameter is not None else f"{operation_id}:target"
    path_id = f"{operation_id}:path"
    path_range = _row_count_path_range(result.emitted.source, step.source_range if step else None, path)
    bindings = (
        EditableBinding(
            id=path_id,
            owner_operation_id=operation_id,
            name="path",
            display_label="Input file",
            schema=ValueSchema("string", path=True),
            value=values.get(path_id, path),
            default=path,
            capabilities=("file-input",),
            source_range=path_range,
            source_kind="rows-in-file",
        ),
        EditableBinding(
            id=target_id,
            owner_operation_id=operation_id,
            name="target",
            display_label="Result variable",
            schema=ValueSchema("string"),
            value=values.get(target_id, target),
            default=target,
            capabilities=("symbol-definition",),
            source_range=target_parameter.source_range if target_parameter else None,
            source_kind="parameter" if target_parameter else "rows-in-file",
        ),
    )
    return WorkflowOperation(
        id=operation_id,
        kind="check-row-count",
        display_name="Check Row Count",
        description="Count rows in a file and store the result in a macro variable.",
        parent_operation_id=parent_operation_id,
        branch=branch,
        source_span=block.span,
        summary=f"{_summary_value(bindings[0].value)} → {_summary_value(bindings[1].value)}",
        bindings=bindings,
        capabilities=("file-input", "symbol-definition"),
        block_index=block.index,
        source_range=step.source_range if step else None,
    )


def _summary_value(value: Any) -> str:
    if value is None:
        return ""
    if isinstance(value, (list, tuple)):
        text = ", ".join(str(item) for item in value)
    else:
        text = str(value)
    text = " ".join(text.split())
    return text if len(text) <= 80 else text[:77] + "…"


def _utility_summary(template: str | None, bindings: list[EditableBinding]) -> str:
    by_name = {binding.name: _summary_value(binding.value) for binding in bindings}
    if template:
        return template.format_map(defaultdict(str, by_name))
    file_binding = next(
        (
            binding
            for binding in bindings
            if any(cap.startswith("file-") for cap in binding.capabilities)
        ),
        None,
    )
    if file_binding is None:
        return ""
    return f"{file_binding.display_label}: {_summary_value(file_binding.value)}"


def _condition_summary(bindings: list[EditableBinding]) -> str:
    by_name = {binding.name: binding.value for binding in bindings}

    def clause(left: str, op: str, right: str) -> str:
        if not by_name.get(left) or not by_name.get(op):
            return ""
        operator = _OPERATOR_TABLE.get(str(by_name[op]), (str(by_name[op]), "string"))[0]
        return f"{_summary_value(by_name[left])} {operator} {_summary_value(by_name.get(right))}"

    first = clause("lhs", "op", "rhs")
    second = clause("lhs2", "op2", "rhs2")
    return f"{first} {by_name['conj']} {second}" if second and by_name.get("conj") else first


def _build_symbols(
    result: CompilationResult, operations: list[WorkflowOperation]
) -> tuple[Symbol, ...]:
    symbols: dict[str, Symbol] = {}

    def add(
        name: str,
        kind: SymbolKind,
        value_state: SymbolValueState,
        value: Any = None,
        introduction: OperationReference | None = None,
    ) -> None:
        normalized = normalize_macro_name(name)
        if not normalized:
            return
        key = normalized.upper()
        current = symbols.get(key)
        if current is None or current.kind == "unresolved":
            symbols[key] = Symbol(
                id=f"symbol:{key}",
                display_name=normalized,
                kind=kind,
                value_state=value_state,
                value=value,
                introduction=introduction,
            )

    for step in result.emitted.steps:
        for parameter in step.parameters:
            if parameter.id.startswith("global:"):
                add(
                    parameter.id.removeprefix("global:"),
                    "global",
                    "known",
                    parameter.value,
                )

    for operation in operations:
        if operation.kind == "check-row-count":
            target = next((item for item in operation.bindings if item.name == "target"), None)
            if target and isinstance(target.value, str):
                add(
                    target.value,
                    "macro",
                    "runtime",
                    introduction=OperationReference(operation.id, target.id),
                )
        if operation.kind == "macro-loop":
            path_binding = next((item for item in operation.bindings if item.name == "csv_path"), None)
            if path_binding and isinstance(path_binding.value, str):
                for header in _csv_headers(result.input_path, path_binding.value):
                    add(
                        header,
                        "macro-row",
                        "runtime",
                        introduction=OperationReference(operation.id, path_binding.id),
                    )

    references: dict[str, list[SymbolReference]] = {}
    operation_by_binding = {
        binding.id: operation
        for operation in operations
        for binding in operation.bindings
    }

    for step in result.emitted.steps:
        for parameter in step.parameters:
            operation = operation_by_binding.get(parameter.id)
            if operation is None:
                continue
            for name in parameter.symbol_names:
                key = normalize_macro_name(name).upper()
                if key not in symbols:
                    add(name, "unresolved", "unknown")
                references.setdefault(key, []).append(
                    SymbolReference(operation.id, parameter.id)
                )

    for operation in operations:
        for binding in operation.bindings:
            if not binding.id.startswith("global:"):
                continue
            key = binding.id.removeprefix("global:").upper()
            if key in symbols:
                references.setdefault(key, []).append(
                    SymbolReference(operation.id, binding.id)
                )

        if operation.kind != "condition":
            continue
        values = {item.name: item for item in operation.bindings}
        clauses = [
            (values.get("lhs"), values.get("op")),
            (values.get("rhs"), values.get("op")),
            (values.get("lhs2"), values.get("op2")),
            (values.get("rhs2"), values.get("op2")),
        ]
        for binding, operator in clauses:
            if binding is None or not isinstance(binding.value, str):
                continue
            name = _symbolic_operand(binding.value, operator.value if operator else None)
            if not name:
                continue
            key = normalize_macro_name(name).upper()
            if key not in symbols:
                add(name, "unresolved", "unknown")
            references.setdefault(key, []).append(SymbolReference(operation.id, binding.id))

    return tuple(
        replace(symbol, references=tuple(references.get(key, ())))
        for key, symbol in sorted(symbols.items())
    )


def _apply_condition_symbol_validation(
    operations: list[WorkflowOperation], symbols: tuple[Symbol, ...]
) -> list[WorkflowOperation]:
    known = {
        normalize_macro_name(symbol.display_name).upper()
        for symbol in symbols
        if symbol.kind != "unresolved"
    }
    projected: list[WorkflowOperation] = []
    for operation in operations:
        if operation.kind != "condition":
            projected.append(operation)
            continue
        by_name = {item.name: item for item in operation.bindings}
        next_bindings = []
        unresolved = False
        for binding in operation.bindings:
            operator = None
            if binding.name in {"lhs", "rhs"}:
                operator = by_name.get("op")
            elif binding.name in {"lhs2", "rhs2"}:
                operator = by_name.get("op2")
            symbol = (
                _symbolic_operand(binding.value, operator.value if operator else None)
                if isinstance(binding.value, str)
                else None
            )
            if symbol and normalize_macro_name(symbol).upper() not in known:
                binding = replace(binding, validation_state="unresolved")
                unresolved = True
            next_bindings.append(binding)
        projected.append(
            replace(
                operation,
                bindings=tuple(next_bindings),
                validation_state="unresolved" if unresolved else operation.validation_state,
            )
        )
    return projected


def _symbolic_operand(value: str, operator: str | None) -> str | None:
    text = value.strip()
    if text.startswith("VAR(") and text.endswith(")"):
        return text[4:-1].strip()
    if MacroState.NAMED_PLACEHOLDER_RE.fullmatch(text):
        return text
    operator_type = _OPERATOR_TABLE.get((operator or "").upper(), ("", "string"))[1]
    if operator_type == "numeric" and re.fullmatch(r"[A-Za-z_][A-Za-z0-9_]*", text):
        return text
    return None


def _csv_headers(source_path: Path, raw_path: str) -> tuple[str, ...]:
    path = Path(raw_path)
    if not path.is_absolute():
        path = source_path.parent / path
    if not path.is_file():
        return ()
    try:
        with path.open(newline="", encoding="utf-8", errors="replace") as handle:
            row = next(csv.reader(handle), ())
    except OSError:
        return ()
    return tuple(item.strip() for item in row if item.strip())


def _control_operation_id(node: ScopeNode) -> str:
    suffix = {"if": "condition", "macro": "macro", "loop": "loop"}.get(node.kind, node.kind)
    return f"block-{node.start_index}:control:{suffix}"


def _control_source_ranges(result: CompilationResult) -> dict[int, SourceRange]:
    """Locate control headers in emitted workflow order without parsing generated Python."""
    source = result.emitted.source
    marker = source.find("# <vg2c:workflow:start>")
    cursor = marker if marker >= 0 else 0
    ranges: dict[int, SourceRange] = {}

    def visit(node: ScopeNode) -> None:
        nonlocal cursor
        if node.kind in {"if", "macro", "loop"} and node.control_payload is not None:
            header = node.control_payload.render_header()
            start = source.find(header, cursor)
            if start >= 0:
                ranges[node.scope_id] = SourceRange(start, start + len(header))
                cursor = start + len(header)
        for child in node.children:
            visit(child)

    visit(result.resolved.scope_tree)
    return ranges


def _row_count_path_range(
    source: str, step_range: SourceRange | None, path: str
) -> SourceRange | None:
    if step_range is None:
        return None
    segment = source[step_range.start_offset : step_range.end_offset]
    marker = "ctx.csv_io.row_count("
    call = segment.find(marker)
    if call < 0:
        return None
    literal = repr(path)
    start = segment.find(literal, call + len(marker))
    if start < 0:
        return None
    absolute = step_range.start_offset + start
    return SourceRange(absolute, absolute + len(literal))


def _scope_indexes(root: ScopeNode) -> tuple[dict[int, int | None], dict[int, ScopeNode]]:
    parent: dict[int, int | None] = {}
    nodes: dict[int, ScopeNode] = {}
    stack = [(root, None)]
    while stack:
        node, parent_id = stack.pop()
        parent[node.scope_id] = parent_id
        nodes[node.scope_id] = node
        for child in reversed(node.children):
            stack.append((child, node.scope_id))
    return parent, nodes


__all__ = [
    "BindingVisibility",
    "CONDITION_OPERATORS",
    "EditableBinding",
    "OperationReference",
    "SemanticModel",
    "Symbol",
    "SymbolReference",
    "ValidationState",
    "WorkflowOperation",
    "build_semantic_model",
]
