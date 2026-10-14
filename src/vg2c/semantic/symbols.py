"""Symbol discovery and condition-symbol validation for semantic projection."""

from __future__ import annotations

import csv
import re
from dataclasses import replace
from pathlib import Path
from typing import TYPE_CHECKING, Any

from vg2c.operands.base import _OPERATOR_TABLE
from vg2c.semantic.models import (
    OperationReference,
    Symbol,
    SymbolKind,
    SymbolReference,
    SymbolValueState,
    WorkflowOperation,
)
from vg2c.utilities._runtime_helpers import normalize_macro_name
from vg2c.utilities.macro_control import MacroControl

if TYPE_CHECKING:
    from vg2c.compilation import CompilationResult


def condition_symbol_token(symbol: Symbol, operator: str | None) -> str:
    """Render a semantic symbol using the compiler condition operand contract."""
    if symbol.kind not in {"macro", "macro-row"}:
        raise ValueError(f"{symbol.display_name} is not a condition symbol.")
    operand_type = _OPERATOR_TABLE.get((operator or "").upper(), ("", "string"))[1]
    return symbol.display_name if operand_type == "numeric" else f"VAR({symbol.display_name})"


CONDITION_OPERATORS = tuple(
    (code, symbol, operand_type)
    for code, (symbol, operand_type) in _OPERATOR_TABLE.items()
)


def build_symbols(
    result: CompilationResult,
    operations: list[WorkflowOperation],
    values: dict[str, Any],
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
                    values.get(parameter.id, parameter.value),
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
        if operation.kind == "site-loop":
            for name in ("SPF-SITE", "SPF-SITE-FOR-FILE-NAME"):
                add(
                    name,
                    "macro",
                    "runtime",
                    introduction=OperationReference(operation.id, operation.bindings[0].id),
                )
        if operation.kind == "for-loop":
            suffix = operation.bindings[0].value[3]
            for prefix, postfix in (
                ("SPF-START-", ""),
                ("SPF-END-", ""),
                ("SPF-STEP-", ""),
                ("SPF-STEP-", "-INT"),
                ("SPF-LOOP-CTR-", ""),
                ("SPF-LOOP-CTR-", "-INT"),
            ):
                add(
                    prefix + suffix + postfix,
                    "macro",
                    "runtime",
                    introduction=OperationReference(operation.id, operation.bindings[0].id),
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
                    SymbolReference(operation.id, parameter.id, "parameter")
                )

    for operation in operations:
        for binding in operation.bindings:
            if not binding.id.startswith("global:"):
                continue
            key = binding.id.removeprefix("global:").upper()
            if key in symbols:
                references.setdefault(key, []).append(
                    SymbolReference(operation.id, binding.id, "global-value")
                )

        if operation.kind != "condition":
            continue
        by_name = {item.name: item for item in operation.bindings}
        clauses = [
            (by_name.get("lhs"), by_name.get("op")),
            (by_name.get("rhs"), by_name.get("op")),
            (by_name.get("lhs2"), by_name.get("op2")),
            (by_name.get("rhs2"), by_name.get("op2")),
        ]
        for binding, operator in clauses:
            if binding is None or not isinstance(binding.value, str):
                continue
            name = symbolic_operand(binding.value, operator.value if operator else None)
            if not name:
                continue
            key = normalize_macro_name(name).upper()
            if key not in symbols:
                add(name, "unresolved", "unknown")
            references.setdefault(key, []).append(
                SymbolReference(operation.id, binding.id, "condition")
            )

    return tuple(
        replace(
            symbol,
            value_binding_id=f"global:{key}" if symbol.kind == "global" else None,
            references=tuple(references.get(key, ())),
        )
        for key, symbol in sorted(symbols.items())
    )


def apply_condition_symbol_validation(
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
                symbolic_operand(binding.value, operator.value if operator else None)
                if isinstance(binding.value, str)
                else None
            )
            if symbol:
                symbol_id = f"symbol:{normalize_macro_name(symbol).upper()}"
                default_operator = by_name.get("op2" if binding.name.endswith("2") else "op")
                default_symbol = (
                    symbolic_operand(
                        binding.default,
                        default_operator.default if default_operator else None,
                    )
                    if isinstance(binding.default, str)
                    else None
                )
                default_symbol_id = (
                    f"symbol:{normalize_macro_name(default_symbol).upper()}"
                    if default_symbol
                    else None
                )
                if normalize_macro_name(symbol).upper() not in known:
                    binding = replace(
                        binding,
                        symbol_id=symbol_id,
                        default_symbol_id=default_symbol_id,
                        validation_state="unresolved",
                    )
                    unresolved = True
                else:
                    binding = replace(
                        binding,
                        symbol_id=symbol_id,
                        default_symbol_id=default_symbol_id,
                    )
            elif isinstance(binding.default, str):
                default_operator = by_name.get("op2" if binding.name.endswith("2") else "op")
                default_symbol = symbolic_operand(
                    binding.default,
                    default_operator.default if default_operator else None,
                )
                if default_symbol:
                    binding = replace(
                        binding,
                        default_symbol_id=(
                            f"symbol:{normalize_macro_name(default_symbol).upper()}"
                        ),
                    )
            next_bindings.append(binding)
        projected.append(
            replace(
                operation,
                bindings=tuple(next_bindings),
                validation_state="unresolved" if unresolved else operation.validation_state,
            )
        )
    return projected


def symbolic_operand(value: str, operator: str | None) -> str | None:
    text = value.strip()
    if text.startswith("VAR(") and text.endswith(")"):
        return text[4:-1].strip()
    if MacroControl.NAMED_PLACEHOLDER_RE.fullmatch(text):
        return text
    operator_type = _OPERATOR_TABLE.get((operator or "").upper(), ("", "string"))[1]
    if operator_type == "numeric" and re.fullmatch(r"[A-Za-z_][A-Za-z0-9_]*", text):
        return text
    return None


def _csv_headers(source_path: Path, raw_path: str) -> tuple[str, ...]:
    root = source_path.parent.resolve()
    path = (root / raw_path).resolve()
    if path != root and root not in path.parents:
        return ()
    if not path.is_file():
        return ()
    try:
        with path.open(newline="", encoding="utf-8", errors="replace") as handle:
            row = next(csv.reader(handle), ())
    except OSError:
        return ()
    return tuple(item.strip() for item in row if item.strip())
