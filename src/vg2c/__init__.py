from pathlib import Path
import tempfile

from importlib import import_module

_EXPORT_MODULES = {
    "CompilationDiagnostic": "compilation",
    "CompilationResult": "compilation",
    "compile_document": "compilation",
    "dispatch": "dispatch",
    "emit": "emitter",
    "ClassifiedBlock": "frontend",
    "ParsedBlock": "frontend",
    "classify": "frontend",
    "parse": "frontend",
    "Kind": "kind",
    "resolve": "resolver",
}


def __getattr__(name: str):
    module = _EXPORT_MODULES.get(name)
    if module is None:
        raise AttributeError(name)
    import_module(f"vg2c.{module}")
    # Importing compiler submodules assigns names such as dispatch on this package.
    # Restore the public functions once the compiler facade is requested.
    for export, target in _EXPORT_MODULES.items():
        globals()[export] = getattr(import_module(f"vg2c.{target}"), export)
    return globals()[name]

__all__ = [
    "ClassifiedBlock",
    "Kind",
    "ParsedBlock",
    "classify",
    "CompilationDiagnostic",
    "CompilationResult",
    "compile_document",
    "parse",
    "dispatch",
    "emit",
    "resolve",
    "translate",
]


def translate(input_path: Path, out_dir: Path | None = None) -> Path:
    """Write an editable project and return <root>/<source-name>/main.py.

    Args:
        input_path: Path to the source .txt file.
        out_dir: Project root, defaulting to the source's parent directory.
    """
    from vg2c.compilation import compile_document
    from vg2c.project_paths import project_main_path

    input_path = Path(input_path)
    root = (Path(out_dir) if out_dir is not None else input_path.parent).resolve()
    dest = project_main_path(input_path, root).parent
    if dest.exists() and (not dest.is_dir() or any(dest.iterdir())):
        raise FileExistsError(f"Nonempty project or sanitized-name collision: {dest}")
    if dest.resolve().parent != root:
        raise ValueError(f"Project destination escapes root: {dest}")
    result = compile_document(input_path)
    errors = [item for item in result.diagnostics if item.level == "error"]
    if errors:
        raise ValueError("\n".join(f"{item.location}: {item.message}" for item in errors))
    root.mkdir(parents=True, exist_ok=True)
    with tempfile.TemporaryDirectory(prefix=".vg2c-", dir=root) as temporary:
        project = Path(temporary) / dest.name
        project.mkdir()
        for name, content in (("main.py", result.emitted.source), *result.emitted.assets):
            path = project / name
            path.parent.mkdir(parents=True, exist_ok=True)
            path.write_text(content, encoding="utf-8", newline="")
        if dest.exists():
            dest.rmdir()
        project.rename(dest)
    return dest / "main.py"
