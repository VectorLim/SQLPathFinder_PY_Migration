from __future__ import annotations

from pathlib import Path

from vg2c import CompilationResult, compile_document


def compile_source(source_path: Path) -> CompilationResult:
    return compile_document(source_path)


def output_path_for(source_path: Path, out_dir: Path | None = None) -> Path:
    directory = out_dir if out_dir is not None else source_path.parent
    return directory / source_path.with_suffix(".py").name


__all__ = ["compile_source", "output_path_for"]
