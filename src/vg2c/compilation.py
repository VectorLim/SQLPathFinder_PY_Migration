"""Compile one VG2 job: original ScriptHost program inspection, then Python emission."""

from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path

from scripthost_portable.task_introspection import ProgramDescriptor, inspect_program
from vg2c.diagnostics import CompileError
from vg2c.emitter import emit
from vg2c.emitter.models import EmittedScript
from vg2c.frontend import read_source
from vg2c.frontend.models import SourceSpan


@dataclass(frozen=True, slots=True)
class CompilationResult:
    input_path: Path
    program: ProgramDescriptor
    emitted: EmittedScript


def compile_document(input_path: Path) -> CompilationResult:
    input_path = Path(input_path)
    text, spans = read_source(input_path.read_bytes(), source=input_path)
    try:
        program = inspect_program(text)
    except ValueError as error:
        raise CompileError("scripthost-inspection", str(error), SourceSpan(input_path, 1, 1), 0) from error
    return CompilationResult(input_path.resolve(), program, emit(program, spans))
