from __future__ import annotations

from pathlib import Path

from vg2c._internal.logging import get_logger

_LOGGER = get_logger(__name__)

_ANCHORS: tuple[str, ...] = (
    "<---- New Query ---->",
    "<OPTIONS>",
    "</OPTIONS>",
    "/REPORT=",
    "/WRITE-FILE=",
    "/NODE=",
    "/WORKDIR=",
    "/UTILITIES=",
)


def read_vg2(path: Path) -> str:
    """Read and decode a VG2 script file with normalized line endings."""
    raw = path.read_bytes()

    decoded: str | None = None
    used_encoding: str | None = None
    for encoding in ("utf-8", "utf-16", "latin-1"):
        try:
            decoded = raw.decode(encoding)
            used_encoding = encoding
            break
        except UnicodeDecodeError:
            continue

    if decoded is None:
        raise UnicodeDecodeError("unknown", b"", 0, 1, "Unable to decode VG2 file")

    if decoded.startswith("\ufeff"):
        decoded = decoded[1:]

    if used_encoding in {"utf-16", "latin-1"}:
        _LOGGER.warning("Decoded %s using fallback encoding %s", path, used_encoding)

    return decoded.replace("\r\n", "\n").replace("\r", "\n")


def normalize_collapsed_lines(text: str) -> str:
    """Repair collapsed single-line VG2 text when delimiter patterns indicate wrapping loss."""
    if text.count("<---- New Query ---->") < 2 or text.count("\n") >= 3:
        return text

    repaired = text
    for anchor in _ANCHORS:
        repaired = repaired.replace(anchor, f"\n{anchor}")
    return repaired
