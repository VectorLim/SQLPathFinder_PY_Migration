from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path


@dataclass(frozen=True, slots=True)
class SourceSpan:
    file: Path | None
    start_line: int
    end_line: int
