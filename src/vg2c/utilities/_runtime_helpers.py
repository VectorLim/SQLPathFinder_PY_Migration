"""Compiler-only macro helper; filesystem and quote rules are owned by runtime.values."""

from __future__ import annotations

from vg2c.runtime.values import resolve_path, strip_quotes



def normalize_macro_name(raw: str) -> str:
    name = raw.strip()
    if name.startswith("<<<") and name.endswith(">>>"):
        name = name[3:-3]
    return name.strip().upper()
