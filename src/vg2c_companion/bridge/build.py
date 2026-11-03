"""Native observer helper discovery and compilation."""

from __future__ import annotations

import hashlib
import importlib.resources
import os
import shutil
import subprocess
import uuid
from collections.abc import Sequence
from pathlib import Path


def _hidden_flags() -> int:
    return getattr(subprocess, "CREATE_NO_WINDOW", 0)


def _find_compiler(windows_root: Path) -> Path | None:
    for relative in (
        Path("Microsoft.NET/Framework64/v4.0.30319"),
        Path("Microsoft.NET/Framework/v4.0.30319"),
    ):
        candidate = windows_root / relative / "csc.exe"
        if candidate.is_file():
            return candidate
    return None


def _reference_paths(compiler: Path) -> tuple[Path, ...]:
    root = Path(compiler).parent
    return tuple(
        root / name
        for name in ("Accessibility.dll", "System.Runtime.Serialization.dll")
    )


def _helper_build_command(
    compiler: Path, output: Path, sources: Sequence[Path]
) -> list[str]:
    return [
        str(compiler),
        "/nologo",
        "/target:exe",
        f"/out:{output}",
        *[f"/reference:{path}" for path in _reference_paths(compiler)],
        *[str(path) for path in sources],
    ]


def build_helper(cache_dir: Path, helper_path: Path) -> Path:
    if os.name != "nt":
        raise OSError("The SQLPathFinder observer requires Windows native window APIs.")
    source_directory = importlib.resources.files("vg2c_companion").joinpath(
        "observer_helper"
    )
    sources = sorted(
        (
            source
            for source in source_directory.iterdir()
            if source.is_file() and source.name.endswith(".cs")
        ),
        key=lambda source: source.name,
    )
    source_texts = [
        (source.name, source.read_text(encoding="utf-8")) for source in sources
    ]
    digest_builder = hashlib.sha256()
    for name, text in source_texts:
        digest_builder.update(name.encode("utf-8") + b"\0")
        digest_builder.update(text.encode("utf-8") + b"\0")
    digest = digest_builder.hexdigest()
    stamp = cache_dir / "observer.sha256"
    if (
        helper_path.is_file()
        and helper_path.stat().st_size > 0
        and stamp.is_file()
        and stamp.read_text().strip() == digest
    ):
        return helper_path
    windows = Path(os.environ.get("WINDIR", r"C:\Windows"))
    compiler = _find_compiler(windows)
    if compiler is None:
        raise FileNotFoundError("Could not locate .NET Framework csc.exe.")
    references = _reference_paths(compiler)
    missing = [str(path) for path in references if not path.is_file()]
    if missing:
        raise FileNotFoundError(
            "Missing .NET Accessibility assemblies: " + ", ".join(missing)
        )
    cache_dir.mkdir(parents=True, exist_ok=True)
    token = uuid.uuid4().hex
    temporary_sources = cache_dir / f".observer-{token}"
    temporary_helper = cache_dir / f".observer-{token}.exe"
    temporary_stamp = cache_dir / f".observer-{token}.sha256"
    try:
        temporary_sources.mkdir()
        source_paths = []
        for name, text in source_texts:
            source_path = temporary_sources / name
            source_path.write_text(text, encoding="utf-8", newline="")
            source_paths.append(source_path)
        completed = subprocess.run(
            _helper_build_command(compiler, temporary_helper, source_paths),
            capture_output=True,
            text=True,
            encoding="utf-8",
            errors="replace",
            timeout=30,
            creationflags=_hidden_flags(),
            check=False,
        )
        if completed.returncode:
            detail = (completed.stderr or completed.stdout or "csc failed").strip()
            raise RuntimeError(f"Could not compile observer helper: {detail[-2000:]}")
        temporary_stamp.write_text(digest, encoding="ascii", newline="")
        os.replace(temporary_helper, helper_path)
        os.replace(temporary_stamp, stamp)
    finally:
        shutil.rmtree(temporary_sources, ignore_errors=True)
        for path in (temporary_helper, temporary_stamp):
            try:
                path.unlink()
            except FileNotFoundError:
                pass
    return helper_path
