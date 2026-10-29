"""Small data contracts shared by the companion UI, observer, and tests."""

from __future__ import annotations

import queue
from contextlib import suppress
from dataclasses import dataclass, field
from pathlib import Path
from typing import TypeVar

_QueueItem = TypeVar("_QueueItem")


def replace_oldest(items: queue.Queue[_QueueItem], item: _QueueItem) -> None:
    """Enqueue *item*, discarding the oldest entry when the bounded queue is full."""
    try:
        items.put_nowait(item)
        return
    except queue.Full:
        pass
    with suppress(queue.Empty):
        items.get_nowait()
    with suppress(queue.Full):
        items.put_nowait(item)


@dataclass(frozen=True, slots=True)
class Selection:
    """A best-effort selection reported by UI Automation."""

    text: str = ""
    start: int | None = None
    end: int | None = None
    truncated: bool = False


@dataclass(frozen=True, slots=True)
class ObserverSnapshot:
    """One bounded, read-only observation of the SQLPathFinder process."""

    target_pid: int | None = None
    window_title: str = ""
    source_text: str = ""
    source_truncated: bool = False
    selection: Selection = field(default_factory=Selection)
    designer_values: tuple[str, ...] = ()
    fingerprint: str = ""
    designer_fingerprint: str = ""
    designer_window_title: str = ""
    designer_bounds: tuple[int, int, int, int] | None = None
    status: str = "ok"
    error: str | None = None
    bounds: tuple[int, int, int, int] | None = None
    active_control: str = "unknown"


@dataclass(frozen=True, slots=True)
class NativeSnapshot:
    """Identification-only information extracted from a native .VG2 file."""

    path: Path
    version: str | None
    sections: tuple[str, ...]
    lines: int


@dataclass(slots=True)
class EditCopy:
    """Independent generated buffer that records a newer source revision."""

    source_identity: int
    text: str
    artifact_name: str = "main.py"
    dirty: bool = True
    pending_identity: int | None = None

    def source_refreshed(self, identity: int) -> None:
        if identity != self.source_identity:
            self.pending_identity = identity


