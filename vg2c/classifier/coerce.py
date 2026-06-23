from __future__ import annotations

import shlex

from vg2c.classifier.model import RecordRef


def as_int(s: str | None) -> int | None:
    """Parse string to int, returning None on failure."""
    if s is None:
        return None
    try:
        return int(s)
    except (ValueError, TypeError):
        return None


def as_bool_yn(s: str | None, default: bool = False) -> bool:
    """Parse Y/N boolean, case-insensitive."""
    if s is None:
        return default
    lower = s.strip().lower()
    if lower in {"y", "yes"}:
        return True
    if lower in {"n", "no"}:
        return False
    return default


def as_csv_list(s: str | None) -> list[str]:
    """Split comma-separated string into list, dropping empties."""
    if not s:
        return []
    return [item.strip() for item in s.split(",") if item.strip()]


def as_record_ref(s: str | None) -> RecordRef | None:
    """Parse 'Name@version' into RecordRef."""
    if not s or "@" not in s:
        return None
    parts = s.split("@", 1)
    return RecordRef(name=parts[0].strip(), version=parts[1].strip())


def as_path_string(s: str | None) -> str | None:
    """Strip one matching pair of surrounding quotes from a path."""
    if not s:
        return None
    stripped = s.strip()
    if len(stripped) >= 2:
        if (stripped[0] == '"' and stripped[-1] == '"') or (
            stripped[0] == "'" and stripped[-1] == "'"
        ):
            return stripped[1:-1]
    return stripped


def split_shell_args(value: str) -> tuple[str, list[str]]:
    """Split shell command into (executable, args), stripping quotes."""
    if not value.strip():
        return ("", [])
    try:
        tokens = shlex.split(value, posix=False)
    except ValueError:
        tokens = value.split()

    clean_tokens = []
    for token in tokens:
        t = token.strip()
        if len(t) >= 2 and ((t[0] == '"' and t[-1] == '"') or (t[0] == "'" and t[-1] == "'")):
            clean_tokens.append(t[1:-1])
        else:
            clean_tokens.append(t)

    if not clean_tokens:
        return ("", [])
    return (clean_tokens[0], clean_tokens[1:])
