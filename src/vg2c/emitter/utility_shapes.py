from __future__ import annotations

from dataclasses import dataclass
from typing import Literal

__all__ = ["UtilityShape", "classify_utility"]

UtilityShape = Literal[
    "run-python-script",
    "email",
    "robocopy",
    "spf-delete",
    "spf-copy",
    "bat-file",
    "exe-direct",
    "unknown",
]


@dataclass(frozen=True, slots=True)
class UtilityInfo:
    shape: UtilityShape
    argv: tuple[str, ...]


def classify_utility(utilities_string: str) -> UtilityInfo:
    """Classify a UTILITIES value into a recognized shape.

    Parses the first token (base name) and matches against known patterns.
    """
    if not utilities_string or not utilities_string.strip():
        return UtilityInfo(shape="unknown", argv=())

    # Split into argv-like tokens (naive; doesn't handle quoted args perfectly,
    # but good enough for the fixtures)
    tokens = utilities_string.strip().split()
    if not tokens:
        return UtilityInfo(shape="unknown", argv=())

    # Extract the base name (last component after /)
    first_token = tokens[0]
    basename = first_token.split("/")[-1].split("\\")[-1].lower()

    # Match by basename
    if "run_python_script" in basename:
        return UtilityInfo(shape="run-python-script", argv=tuple(tokens))
    if "email" in basename or "sqlpathfinder_email" in basename:
        return UtilityInfo(shape="email", argv=tuple(tokens))
    if "robocopy" in basename or "spfcopy" in basename:
        return UtilityInfo(shape="robocopy", argv=tuple(tokens))
    if "spfdelete" in basename or "spfdelete" in basename:
        return UtilityInfo(shape="spf-delete", argv=tuple(tokens))
    if "spfcopy" in basename:
        return UtilityInfo(shape="spf-copy", argv=tuple(tokens))
    if basename.endswith(".bat"):
        return UtilityInfo(shape="bat-file", argv=tuple(tokens))
    if basename.endswith(".exe"):
        return UtilityInfo(shape="exe-direct", argv=tuple(tokens))
    if basename.endswith(".va"):
        # Unrecognized .va script
        return UtilityInfo(shape="unknown", argv=tuple(tokens))

    return UtilityInfo(shape="unknown", argv=tuple(tokens))
