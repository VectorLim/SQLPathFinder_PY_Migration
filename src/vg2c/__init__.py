from pathlib import Path

from vg2c.compilation import CompilationDiagnostic, CompilationResult, compile_document
from vg2c.dataflow import analyze
from vg2c.dispatch import dispatch
from vg2c.emitter import emit
from vg2c.frontend import (
    ClassifiedBlock,
    ParsedBlock,
    classify,
    parse,
)
from vg2c.kind import Kind
from vg2c.resolver import resolve

__all__ = [
    "ClassifiedBlock",
    "Kind",
    "ParsedBlock",
    "classify",
    "CompilationDiagnostic",
    "CompilationResult",
    "compile_document",
    "parse",
    "analyze",
    "dispatch",
    "emit",
    "resolve",
    "translate",
]


def translate(input_path: Path, out_dir: Path | None = None) -> Path:
    """Translate a single .txt file and return the output .py path.

    Args:
        input_path: Path to the source .txt file.
        out_dir:    Directory for the output .py file.  Defaults to the
                    same directory as the source file.
    """
    result = compile_document(input_path)

    dest = out_dir if out_dir is not None else input_path.parent
    dest.mkdir(parents=True, exist_ok=True)
    out_path = dest / input_path.with_suffix(".py").name
    out_path.write_text(result.generated_python, encoding="utf-8")
    return out_path
