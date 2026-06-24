from __future__ import annotations

from typing import Literal

from vg2c.resolver.models import RuntimeMacroRef

__all__ = ["MacroSubstituter"]

_PLACEHOLDER_PATTERN_NAMED = r"<<<([^>]+)>>>"
_PLACEHOLDER_PATTERN_POS = r"<<>>"


class MacroSubstituter:
    """Rewrites macro placeholders contextually."""

    def substitute(
        self,
        text: str,
        refs: tuple[RuntimeMacroRef, ...],
        context: Literal["python-expr", "python-string", "sql-body", "template-body"],
    ) -> str:
        """Rewrite macro placeholders in text based on context.

        Contexts:
        - "python-expr": naked expression context → ctx.macro.named("NAME")
        - "python-string": f-string context → {ctx.macro.named("NAME")}
        - "sql-body": SQL pass-through → leave literal <<<NAME>>> (helper handles at runtime)
        - "template-body": template text → leave literal, helper does substitution at runtime
        """
        if context in ("sql-body", "template-body"):
            # Don't rewrite — helper does substitution at runtime
            return text

        # For Python contexts, rewrite all placeholders
        result = text

        for ref in refs:
            if ref.location != "body":
                # Only process body references for now
                continue

            # Named placeholders
            named = self._extract_named_from_literal(text)
            for name in named:
                placeholder = f"<<<{name}>>>"
                if context == "python-expr":
                    replacement = f'ctx.macro.named("{name.upper()}")'
                elif context == "python-string":
                    replacement = f'{{ctx.macro.named("{name.upper()}")}}'
                else:
                    continue
                result = result.replace(placeholder, replacement)

            # Positional placeholders
            if "<<>>" in result:
                if context == "python-expr":
                    result = result.replace("<<>>", "ctx.macro.positional()")
                elif context == "python-string":
                    result = result.replace("<<>>", "{ctx.macro.positional()}")

        return result

    @staticmethod
    def _extract_named_from_literal(text: str) -> set[str]:
        """Extract all <<<NAME>>> placeholders from text."""
        import re

        matches = re.findall(r"<<<([^>]+)>>>", text)
        return set(matches)
