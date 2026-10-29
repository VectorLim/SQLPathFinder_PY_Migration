"""Compiler preview helpers kept separate from Tk and UI Automation."""

from __future__ import annotations

import queue
import tempfile
import threading
from dataclasses import dataclass
from importlib.resources import files
from pathlib import Path
from typing import Callable

from vg2c import CompilationResult, compile_document
from .models import replace_oldest


@dataclass(frozen=True, slots=True)
class CompilationOutcome:
    identity: int
    source_path: Path
    source_text: str
    result: CompilationResult | None = None
    error: str | None = None


def runtime_sources() -> dict[str, str]:
    """Read the installed runtime source without importing its execution modules."""
    root = files("vg2c").joinpath("runtime")
    return {
        f"vg2c/runtime/{source.name}": source.read_text(encoding="utf-8")
        for source in sorted(root.iterdir(), key=lambda item: item.name)
        if source.is_file() and source.name.endswith(".py")
    }


def compile_source(
    source_text: str, source_name: str = "preview.txt"
) -> CompilationResult:
    """Compile a text snapshot through the existing compiler facade."""
    with tempfile.TemporaryDirectory(prefix="vg2c-companion-") as directory:
        path = Path(directory) / Path(source_name).name
        if path.suffix.lower() not in {".txt", ".spfsql"}:
            path = path.with_suffix(".txt")
        path.write_text(source_text, encoding="utf-8", newline="")
        return compile_document(path)


class CompilationWorker:
    """One background compiler that drops obsolete snapshot requests."""

    def __init__(self, callback: Callable[[CompilationOutcome], None]) -> None:
        self._callback = callback
        self._queue: queue.Queue[tuple[int, Path, str] | None] = queue.Queue(maxsize=1)
        self._lock = threading.Lock()
        self._queue_lock = threading.Lock()
        self._closed = False
        self._latest = 0
        self._thread = threading.Thread(
            target=self._run, name="vg2c-preview", daemon=True
        )
        self._thread.start()

    def submit(self, source_path: Path, source_text: str) -> int:
        with self._lock:
            if self._closed:
                raise RuntimeError("Compilation worker is closed.")
            self._latest += 1
            identity = self._latest
        request = (identity, Path(source_path), source_text)
        with self._queue_lock:
            replace_oldest(self._queue, request)
        return identity

    def close(self) -> None:
        with self._lock:
            if self._closed:
                return
            self._closed = True
        with self._queue_lock:
            replace_oldest(self._queue, None)
        self._thread.join(timeout=2)

    def _run(self) -> None:
        while True:
            request = self._queue.get()
            if request is None:
                return
            identity, source_path, source_text = request
            try:
                result = compile_source(source_text, source_path.name)
                outcome = CompilationOutcome(
                    identity, source_path, source_text, result=result
                )
            except Exception as exc:  # compiler diagnostics are shown in the companion
                outcome = CompilationOutcome(
                    identity, source_path, source_text, error=str(exc)
                )
            with self._lock:
                if self._closed or identity != self._latest:
                    continue
            self._callback(outcome)
