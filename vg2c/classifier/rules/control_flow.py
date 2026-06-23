from __future__ import annotations

from vg2c.classifier.coerce import as_bool_yn, split_shell_args
from vg2c.classifier.model import (
    BlockGroupCloseSpec,
    BlockGroupOpenSpec,
    IfCloseSpec,
    IfElseSpec,
    IfThenSpec,
    Kind,
    LoopCloseSpec,
    LoopOpenSpec,
    MacroCloseSpec,
    MacroOpenSpec,
    Role,
    RowsInFileSpec,
)
from vg2c.classifier.rules.base import Match
from vg2c.model import ParsedBlock


class ControlFlowRule:
    """Match control flow tokens in UTILITIES option."""

    name = "control_flow"

    def match(self, b: ParsedBlock) -> Match | None:
        """Match control flow constructs."""
        utilities = b.options.get("UTILITIES", "").strip()
        if not utilities:
            return None

        first_token = utilities.split()[0].upper() if utilities.split() else ""

        # {IF-THEN}
        if first_token == "{IF-THEN}":
            remainder = utilities[len("{IF-THEN}") :].strip()
            exe, args = split_shell_args(remainder)
            spec = IfThenSpec(
                lhs=args[0] if len(args) > 0 else "",
                op=args[1] if len(args) > 1 else "",
                rhs=args[2] if len(args) > 2 else "",
                connector=args[3] if len(args) > 3 else "",
                lhs2=args[4] if len(args) > 4 else "",
                op2=args[5] if len(args) > 5 else "",
                rhs2=args[6] if len(args) > 6 else "",
                prompt=b.options.get("PROMPT-TEXT"),
            )
            return Match(Kind.IF_OPEN, Role.OPENER, spec, "control-flow token {IF-THEN}")

        # {IF-ELSE} or {ELSE-IF}
        if first_token in {"{IF-ELSE}", "{ELSE-IF}", "{ELSE}"}:
            spec = IfElseSpec(prompt=b.options.get("PROMPT-TEXT"))
            return Match(Kind.IF_ELSE, Role.OPENER, spec, f"control-flow token {first_token}")

        # {END-IF}
        if first_token == "{END-IF}":
            spec = IfCloseSpec(prompt=b.options.get("PROMPT-TEXT"))
            return Match(Kind.IF_CLOSE, Role.CLOSER, spec, "control-flow token {END-IF}")

        # {START-MACRO}
        if first_token == "{START-MACRO}":
            remainder = utilities[len("{START-MACRO}") :].strip()
            exe, args = split_shell_args(remainder)
            csv_driver = args[0] if args else ""
            nested = as_bool_yn(args[1] if len(args) > 1 else "N")
            spec = MacroOpenSpec(
                csv_driver=csv_driver,
                nested=nested,
                prompt=b.options.get("PROMPT-TEXT"),
            )
            return Match(Kind.MACRO_OPEN, Role.OPENER, spec, "control-flow token {START-MACRO}")

        # {END-MACRO}
        if first_token == "{END-MACRO}":
            spec = MacroCloseSpec(prompt=b.options.get("PROMPT-TEXT"))
            return Match(Kind.MACRO_CLOSE, Role.CLOSER, spec, "control-flow token {END-MACRO}")

        # Loops
        loop_tokens = {
            "FOR-LOOP": "for",
            "{FOR-LOOP}": "for",
            "SITE-LOOP": "site",
            "{SITE-LOOP}": "site",
            "RUN-LOOP": "run",
            "{RUN-LOOP}": "run",
        }
        if first_token in loop_tokens:
            remainder = utilities[len(first_token) :].strip()
            exe, args = split_shell_args(remainder)
            spec = LoopOpenSpec(
                loop_kind=loop_tokens[first_token],  # type: ignore
                csv_file=args[0] if args else "",
                column=args[1] if len(args) > 1 else "",
                prompt=b.options.get("PROMPT-TEXT"),
            )
            return Match(Kind.LOOP_OPEN, Role.OPENER, spec, f"control-flow token {first_token}")

        # {END-LOOP}
        if first_token in {"{END-LOOP}", "END-LOOP"}:
            spec = LoopCloseSpec(prompt=b.options.get("PROMPT-TEXT"))
            return Match(Kind.LOOP_CLOSE, Role.CLOSER, spec, "control-flow token {END-LOOP}")

        # {BEGIN-BLOCK-GROUP}
        if first_token == "{BEGIN-BLOCK-GROUP}":
            spec = BlockGroupOpenSpec(prompt=b.options.get("PROMPT-TEXT"))
            return Match(
                Kind.BLOCK_GROUP_OPEN,
                Role.OPENER,
                spec,
                "control-flow token {BEGIN-BLOCK-GROUP}",
            )

        # {END-BLOCK-GROUP}
        if first_token == "{END-BLOCK-GROUP}":
            spec = BlockGroupCloseSpec(prompt=b.options.get("PROMPT-TEXT"))
            return Match(
                Kind.BLOCK_GROUP_CLOSE,
                Role.CLOSER,
                spec,
                "control-flow token {END-BLOCK-GROUP}",
            )

        # {ROWS-IN-FILE}
        if first_token == "{ROWS-IN-FILE}":
            remainder = utilities[len("{ROWS-IN-FILE}") :].strip()
            exe, args = split_shell_args(remainder)
            spec = RowsInFileSpec(
                file_path=args[0] if args else "",
                var_name=args[1] if len(args) > 1 else "",
                prompt=b.options.get("PROMPT-TEXT"),
            )
            return Match(Kind.ROWS_IN_FILE, Role.LEAF, spec, "control-flow token {ROWS-IN-FILE}")

        return None
