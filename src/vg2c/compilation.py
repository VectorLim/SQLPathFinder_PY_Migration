from __future__ import annotations

import logging
import re
from dataclasses import dataclass
from pathlib import Path

from vg2c.dispatch import dispatch
from vg2c.dispatch.models import DispatchedProgram
from vg2c.emitter import emit
from vg2c.emitter.models import EmittedScript
from vg2c.frontend import classify, parse
from vg2c.resolver import resolve
from vg2c.resolver.models import ResolvedProgram

_DIAGNOSTIC_RE = re.compile(
    r"^\[(?P<code>[^]]+)]\s+(?P<location>.+?:\d+:\d+)"
    r"(?:\s+\(block\s+\d+\))?:\s+(?P<message>.*)$"
)


@dataclass(frozen=True, slots=True)
class CompilationDiagnostic:
    level: str
    code: str
    message: str
    location: str | None = None


@dataclass(frozen=True, slots=True)
class CompilationResult:
    """Authoritative result of the complete compiler semantic chain."""

    input_path: Path
    dispatched: DispatchedProgram
    emitted: EmittedScript
    diagnostics: tuple[CompilationDiagnostic, ...]

    @property
    def resolved(self) -> ResolvedProgram:
        """Resolver-stage result retained by dispatch; exposed once through the facade."""
        return self.dispatched.resolved


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
        dispatched = dispatch(resolved)
        emitted = emit(dispatched)
    finally:
        compiler_logger.removeHandler(handler)

    return CompilationResult(
        input_path=input_path.resolve(),
        dispatched=dispatched,
        emitted=emitted,
        diagnostics=tuple(handler.items),
    )


__all__ = ["CompilationDiagnostic", "CompilationResult", "compile_document"]
