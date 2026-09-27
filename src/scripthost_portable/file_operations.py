"""OS operations used by original ScriptHost helpers; no VG2/task parsing."""

from __future__ import annotations

import glob
import shutil
import stat
import zipfile
from pathlib import Path, PurePosixPath, PureWindowsPath


def copy_files(source: str, destination: str) -> None:
    """Replace the COPY operation after ScriptHost expands distribution tokens."""
    matches = [Path(name) for name in glob.glob(source)]
    if not matches:
        raise FileNotFoundError(source)
    target = Path(destination)
    if len(matches) > 1 and not target.is_dir():
        raise NotADirectoryError(destination)
    for path in matches:
        shutil.copy2(path, target)


def delete_files(patterns: list[str], force: bool) -> None:
    """Windows DEL's file-only operation, including a directory's immediate files."""
    for pattern in patterns:
        path = Path(pattern)
        if path.name == "*.*":
            pattern = str(path.with_name("*"))
        for name in glob.glob(pattern):
            item = Path(name)
            targets = list(item.iterdir()) if item.is_dir() else [item]
            for target in targets:
                if target.is_file() or target.is_symlink():
                    if force and not target.is_symlink():
                        target.chmod(target.stat().st_mode | stat.S_IWUSR)
                    target.unlink()


def unzip_file(source: str, destination: str, preserve_paths: bool) -> None:
    """Replace unzip -o [-j], validating all member paths before writing any."""
    root = Path(destination).resolve()
    with zipfile.ZipFile(source) as archive:
        members = []
        for info in archive.infolist():
            name = info.filename.replace("\\", "/")
            path = PurePosixPath(name)
            if (
                path.is_absolute()
                or PureWindowsPath(name).drive
                or ".." in path.parts
                or stat.S_ISLNK(info.external_attr >> 16)
            ):
                raise ValueError(f"Unsafe ZIP member path: {info.filename!r}")
            target = root.joinpath(*path.parts) if preserve_paths else root / path.name
            if not target.resolve().is_relative_to(root):
                raise ValueError(f"Unsafe ZIP member path: {info.filename!r}")
            members.append((info, target))
        root.mkdir(parents=True, exist_ok=True)
        for info, target in members:
            if info.is_dir():
                if preserve_paths:
                    target.mkdir(parents=True, exist_ok=True)
                continue
            target.parent.mkdir(parents=True, exist_ok=True)
            with archive.open(info) as reader, target.open("wb") as writer:
                shutil.copyfileobj(reader, writer)
