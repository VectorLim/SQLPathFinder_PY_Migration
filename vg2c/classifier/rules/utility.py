from __future__ import annotations

from pathlib import Path

from vg2c.classifier.coerce import as_bool_yn, split_shell_args
from vg2c.classifier.model import Kind, Role, RunUtilitySpec
from vg2c.classifier.rules.base import Match
from vg2c.classifier.utilities import KNOWN_UTILITY_REGISTRY
from vg2c.model import ParsedBlock


class UtilityRule:
    """Match /UTILITIES= blocks."""

    name = "utility"

    def match(self, b: ParsedBlock) -> Match | None:
        """Match utility execution."""
        utilities = b.options.get("UTILITIES", "").strip()
        if not utilities:
            return None

        # Skip control flow tokens (handled by ControlFlowRule)
        first_token = utilities.split()[0].upper() if utilities.split() else ""
        if first_token.startswith("{") or first_token.endswith("-LOOP"):
            return None

        exe, args = split_shell_args(utilities)
        basename = Path(exe.lstrip("@").replace("EXEDIR@", "").lstrip("\\/")).name.lower()

        # Check known utilities
        if basename in KNOWN_UTILITY_REGISTRY:
            handler = KNOWN_UTILITY_REGISTRY[basename]
            spec = handler.spec_builder(exe, args, b.options, b.body)
            return Match(
                handler.kind,
                Role.LEAF,
                spec,
                f"known utility {basename}",
            )

        # Generic utility
        spec = RunUtilitySpec(
            executable=exe,
            args=args,
            workdir=b.options.get("WORKDIR"),
            outlook=as_bool_yn(b.options.get("OUTLOOK"), default=True),
            prompt=b.options.get("PROMPT-TEXT"),
        )
        return Match(
            Kind.RUN_UTILITY,
            Role.LEAF,
            spec,
            f"generic utility {basename or 'unknown'}",
        )
