"""write_file — template-to-file writer with macro variable substitution."""

from __future__ import annotations

import re
from pathlib import Path

_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")


def write_file(
    path: str,
    template: str,
    vars: dict[str, str] | None,
    macro_state=None,
) -> None:
    """Write *template* to *path*, substituting ``<<<NAME>>>`` placeholders.

    Substitution priority:
    1. *vars* dict (if supplied)
    2. *macro_state* (active ``MacroState`` from ``PipelineContext.macro``)
    3. Empty string

    Re-substitutes on every call so row-iter scopes write fresh content per row.
    """

    def _lookup(name: str) -> str:
        key = name.strip().upper()
        if vars is not None:
            return vars.get(key, "")
        if macro_state is not None:
            return macro_state.named(key)
        return ""

    def _replace(match: re.Match) -> str:
        named = match.group(1)
        if named is not None:
            return _lookup(named)
        # positional placeholder <<>>
        if macro_state is not None:
            return macro_state.positional()
        return ""

    content = _PLACEHOLDER_RE.sub(_replace, template)

    out = Path(path)
    out.parent.mkdir(parents=True, exist_ok=True)
    out.write_text(content, encoding="utf-8")
