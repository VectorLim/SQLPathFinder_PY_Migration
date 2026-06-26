"""write_file - template-to-file writer with macro variable substitution."""

from __future__ import annotations

import re
from pathlib import Path
from typing import Protocol

_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")


class MacroLookup(Protocol):
    def named(self, name: str) -> str: ...

    def positional(self) -> str: ...


def write_file(
    path: str,
    template: str,
    vars: dict[str, str] | None,
    macro_state: MacroLookup | None = None,
) -> None:
    """Write template to path, substituting <<<NAME>>> and <<>> placeholders."""

    def _lookup(name: str) -> str:
        key = name.strip().upper()
        if vars is not None:
            return vars.get(key, "")
        if macro_state is not None:
            return macro_state.named(key)
        return ""

    def _replace(match: re.Match[str]) -> str:
        named = match.group(1)
        if named is not None:
            return _lookup(named)
        if macro_state is not None:
            return macro_state.positional()
        return ""

    content = _PLACEHOLDER_RE.sub(_replace, template)
    content = content.lstrip("\n")

    out = Path(path)
    out.parent.mkdir(parents=True, exist_ok=True)
    out.write_text(content, encoding="utf-8")
