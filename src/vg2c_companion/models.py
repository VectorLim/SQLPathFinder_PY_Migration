"""Small data contracts shared by the companion UI, observer, and tests."""

from __future__ import annotations

from dataclasses import dataclass, field
from pathlib import Path


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


