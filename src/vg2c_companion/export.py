"""Safe export of an in-memory compiler preview."""

from __future__ import annotations

import os
import shutil
import tempfile
from pathlib import Path, PurePosixPath
from typing import Any


def _safe_relative_name(name: str) -> PurePosixPath:
    candidate = PurePosixPath(name.replace("\\", "/"))
    if candidate.is_absolute() or not candidate.parts or ":" in candidate.parts[0]:
        raise ValueError(f"Generated asset path must be relative: {name!r}")
    if any(part in {"", ".", ".."} for part in candidate.parts):
        raise ValueError(f"Generated asset path escapes the export directory: {name!r}")
    return candidate


def export_preview(result: Any, destination: Path) -> Path:
    """Write main.py and compiler-owned assets into a new or empty directory."""
    destination = Path(destination).expanduser().resolve()
    parent = destination.parent
    if not parent.is_dir():
        raise FileNotFoundError(f"Export parent directory does not exist: {parent}")
    if destination.exists() and (not destination.is_dir() or any(destination.iterdir())):
        raise FileExistsError(f"Export destination must be new or empty: {destination}")

    files: list[tuple[PurePosixPath, str]] = [(PurePosixPath("main.py"), result.emitted.source)]
    seen = {"main.py"}
    for raw_name, content in result.emitted.assets:
        relative = _safe_relative_name(raw_name)
        key = relative.as_posix()
        if any(":" in part for part in relative.parts):
            raise ValueError(f"Generated asset path contains a Windows drive marker: {raw_name!r}")
        if key.casefold() in {item.casefold() for item in seen}:
            raise ValueError(f"Duplicate generated asset path: {raw_name!r}")
        seen.add(key)
        files.append((relative, content))

    stage = Path(tempfile.mkdtemp(prefix=".vg2c-companion-", dir=parent))
    try:
        for relative, content in files:
            target = stage.joinpath(*relative.parts)
            target.parent.mkdir(parents=True, exist_ok=True)
            target.write_text(content, encoding="utf-8", newline="")
        if destination.exists():
            destination.rmdir()
        os.replace(stage, destination)
    except Exception:
        shutil.rmtree(stage, ignore_errors=True)
        raise
    return destination


def save_copy(destination: Path, text: str, protected_paths: tuple[Path, ...] = ()) -> Path:
    """Save an explicitly chosen new copy without overwriting protected files."""
    destination = Path(destination).expanduser().resolve()
    destination_key = os.path.normcase(str(destination)).casefold()
    if any(
        destination_key == os.path.normcase(str(Path(path).expanduser().resolve())).casefold()
        for path in protected_paths
    ):
        raise ValueError(f"Refusing to save over an original source: {destination}")
    if not destination.parent.is_dir():
        raise FileNotFoundError(f"Copy parent directory does not exist: {destination.parent}")
    # Create the chosen path exclusively.  The kernel check closes the race
    # between a prior existence check and the write, and therefore cannot
    # replace a file created by another process in the meantime.
    try:
        handle = destination.open("x", encoding="utf-8", newline="")
    except FileExistsError:
        raise FileExistsError(f"Copy destination already exists: {destination}")
    created = True
    success = False
    try:
        with handle:
            handle.write(text)
        success = True
    finally:
        if created and not success:
            try:
                destination.unlink()
            except FileNotFoundError:
                pass
    return destination


__all__ = ["export_preview", "save_copy"]

