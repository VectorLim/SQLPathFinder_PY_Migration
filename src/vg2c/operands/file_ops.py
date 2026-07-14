"""File-op payloads: ``{ROWS-IN-FILE}``.

Leaf-only payload (no ``build_scope``/``emit_scope`` — dispatched by the
walker via ``UtilitySpec``).
"""

from __future__ import annotations

from dataclasses import dataclass

from vg2c.frontend.models import ClassifiedBlock

from vg2c.operands.base import _quoted_args


@dataclass(frozen=True, slots=True)
class RowsInFile:
    csv_path: str
    var_name: str
    prompt_off: bool

    @classmethod
    def from_block(cls, block: ClassifiedBlock) -> RowsInFile:
        """Parse a {ROWS-IN-FILE} block into a RowsInFile payload."""
        args = _quoted_args(block.options.lookup.get("UTILITIES", ""))
        csv_path = args[0] if args else ""
        var_name = args[1] if len(args) > 1 else ""
        prompt_flag = args[2] if len(args) > 2 else "N"
        return cls(
            csv_path=csv_path,
            var_name=var_name,
            prompt_off=prompt_flag.upper() == "Y",
        )
