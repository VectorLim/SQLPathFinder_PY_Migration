"""Compiler facade retained from main; runtime dispatch/embedding are excluded."""

from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path

from vg2c.emitter import emit
from vg2c.emitter.models import EmittedScript
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve
from vg2c.resolver.models import ResolvedProgram


@dataclass(frozen=True, slots=True)
class CompilationResult:
    input_path: Path
    resolved: ResolvedProgram
    emitted: EmittedScript


def compile_document(input_path: Path) -> CompilationResult:
    input_path = Path(input_path)
    resolved = resolve(classify(parse(input_path.read_bytes(), source=input_path)))
    return CompilationResult(input_path.resolve(), resolved, emit(resolved))
