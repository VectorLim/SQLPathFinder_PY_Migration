from __future__ import annotations

import ast
import logging
import re
from collections.abc import Mapping
from dataclasses import dataclass
from pathlib import Path
from types import MappingProxyType

from vg2c.dataflow import analyze
from vg2c.dataflow.models import AnalyzedProgram, DataflowEdge
from vg2c.dispatch import dispatch
from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter import emit
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve
from vg2c.resolver.models import ResolvedBlock, ResolvedProgram

_DIAGNOSTIC_RE = re.compile(
    r"^\[(?P<code>[^]]+)]\s+(?P<location>.+?:\d+:\d+)"
    r"(?:\s+\(block\s+\d+\))?:\s+(?P<message>.*)$"
)
_STEP_NAME_RE = re.compile(r"^step_(?P<index>\d+)_")


@dataclass(frozen=True, slots=True)
class CompilationDiagnostic:
    level: str
    code: str
    message: str
    location: str | None = None


@dataclass(frozen=True, slots=True)
class CompilationResult:
    input_path: Path
    generated_python: str
    resolved: ResolvedProgram
    analyzed: AnalyzedProgram
    dispatched: DispatchedProgram
    diagnostics: tuple[CompilationDiagnostic, ...]
    function_to_block: Mapping[str, ResolvedBlock]

    @property
    def resolved_blocks(self) -> tuple[ResolvedBlock, ...]:
        return self.resolved.blocks

    @property
    def scope_tree(self):
        return self.resolved.scope_tree

    @property
    def dataflow_edges(self) -> tuple[DataflowEdge, ...]:
        return self.analyzed.edges


class _DiagnosticHandler(logging.Handler):
    def __init__(self) -> None:
        super().__init__(logging.WARNING)
        self.items: list[CompilationDiagnostic] = []

    def emit(self, record: logging.LogRecord) -> None:
        text = record.getMessage()
        match = _DIAGNOSTIC_RE.match(text)
        self.items.append(
            CompilationDiagnostic(
                level=record.levelname.lower(),
                code=match.group("code") if match else "compiler-message",
                location=match.group("location") if match else None,
                message=match.group("message") if match else text,
            )
        )


def compile_document(input_path: Path) -> CompilationResult:
    """Compile one VG2 source file without writing it to disk."""
    input_path = Path(input_path)
    text = input_path.read_text(encoding="utf-8", errors="replace")
    handler = _DiagnosticHandler()
    compiler_logger = logging.getLogger("vg2c")
    compiler_logger.addHandler(handler)
    try:
        parsed = parse(text, source=input_path)
        classified = classify(parsed)
        resolved = resolve(classified)
        analyzed = analyze(resolved)
        dispatched = dispatch(analyzed)
        emitted = emit(dispatched)
    finally:
        compiler_logger.removeHandler(handler)

    blocks_by_index = {block.index: block for block in resolved.blocks}
    function_to_block: dict[str, ResolvedBlock] = {}
    for node in ast.parse(emitted.source).body:
        if not isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef)):
            continue
        match = _STEP_NAME_RE.match(node.name)
        if match and (block := blocks_by_index.get(int(match.group("index")))):
            function_to_block[node.name] = block

    return CompilationResult(
        input_path=input_path.resolve(),
        generated_python=emitted.source,
        resolved=resolved,
        analyzed=analyzed,
        dispatched=dispatched,
        diagnostics=tuple(handler.items),
        function_to_block=MappingProxyType(function_to_block),
    )


__all__ = ["CompilationDiagnostic", "CompilationResult", "compile_document"]
