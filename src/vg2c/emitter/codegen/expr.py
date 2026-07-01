"""``PyExpr`` — a thin wrapper around already-valid Python source text.

The wrapper keeps emitter intent explicit: kwargs to :class:`CallSpec` are
expressions, not strings to be quoted. Constructors normalise the common
patterns (literals, multiline strings, lists, dicts, concat) so callers
never call :func:`repr` directly.
"""

from __future__ import annotations

from dataclasses import dataclass
from typing import Iterable, Mapping

__all__ = ["PyExpr"]


@dataclass(frozen=True, slots=True)
class PyExpr:
    """An already-valid Python expression rendered as source text."""

    source: str

    def __str__(self) -> str:
        return self.source

    # ------------------------------------------------------------------
    # Constructors
    # ------------------------------------------------------------------

    @classmethod
    def raw(cls, source: str) -> "PyExpr":
        """Wrap *source* verbatim. Caller asserts it is valid Python."""
        return cls(source)

    @classmethod
    def literal(cls, value: object) -> "PyExpr":
        """Render *value* via :func:`repr`.

        Suitable for ``None``, ``bool``, ``int``, ``float``, ``str``,
        ``list``, ``tuple``, ``dict`` of literals.
        """
        return cls(repr(value))

    @classmethod
    def name(cls, identifier: str) -> "PyExpr":
        """Wrap a bare identifier (no quoting)."""
        return cls(identifier)

    @classmethod
    def multiline_string(cls, text: str) -> "PyExpr":
        """Emit a readable triple-quoted string literal for multiline text.

        Single-line text falls back to :func:`repr` for predictable output.
        """
        if "\n" not in text:
            return cls(repr(text))
        escaped = text.replace('"""', '\\"\\"\\"')
        return cls(f'"""{escaped}"""')

    @classmethod
    def list_of(cls, items: Iterable["PyExpr"]) -> "PyExpr":
        rendered = ", ".join(item.source for item in items)
        return cls(f"[{rendered}]")

    @classmethod
    def dict_of(cls, pairs: Mapping[str, "PyExpr"]) -> "PyExpr":
        rendered = ", ".join(f"{key!r}: {value.source}" for key, value in pairs.items())
        return cls(f"{{{rendered}}}")

    @classmethod
    def concat(cls, parts: Iterable["PyExpr"], sep: str = " + ") -> "PyExpr":
        """Join *parts* with *sep*. Used for SQL-with-macro-tokens stitching."""
        rendered = list(parts)
        if not rendered:
            return cls(repr(""))
        if len(rendered) == 1:
            return rendered[0]
        return cls(sep.join(item.source for item in rendered))
