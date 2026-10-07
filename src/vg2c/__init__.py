"""VG2-to-clean-Python compiler. Generated jobs depend only on ScriptHost."""

from pathlib import Path

from vg2c.compilation import CompilationResult, compile_document
from vg2c.diagnostics import CompileError

__all__ = ["CompilationResult", "CompileError", "compile_document", "translate"]


def translate(input_path: Path, out_dir: Path | None = None) -> Path:
    input_path = Path(input_path)
    result = compile_document(input_path)
    destination = Path(out_dir) if out_dir is not None else input_path.parent
    output = destination / input_path.with_suffix(".py").name
    if output.resolve() == input_path.resolve():
        raise ValueError("Generated output must not overwrite the source input.")
    destination.mkdir(parents=True, exist_ok=True)
    output.write_text(result.emitted.source, encoding="utf-8")
    return output
