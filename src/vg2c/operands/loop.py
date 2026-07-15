"""Loop payloads: ``{RUN-LOOP}`` / ``{END-LOOP}``."""

from __future__ import annotations

from dataclasses import dataclass
from typing import TYPE_CHECKING, Callable

from vg2c import logger
from vg2c.frontend.models import ClassifiedBlock

from vg2c.operands.base import (
    ParseChildrenFn,
    ScopeIdSource,
    ScopeNode,
    _quoted_args,
)

if TYPE_CHECKING:
    from vg2c.emitter.models import IndentWriter

log = logger.getLogger("vg2c.operands.loop")


@dataclass(frozen=True, slots=True)
class RunLoop:
    input_csv_path: str
    chunk_csv_path: str
    chunk_size: int
    prompt_off: bool

    # ------------------------------------------------------------------
    # Scope building
    # ------------------------------------------------------------------

    @classmethod
    def from_block(cls, block: ClassifiedBlock) -> RunLoop:
        """Parse a {RUN-LOOP} block into a RunLoop payload."""
        args = _quoted_args(block.options.lookup.get("UTILITIES", ""))
        input_csv = args[0] if args else ""
        chunk_csv = args[1] if len(args) > 1 else ""
        chunk_sz = int(args[2]) if len(args) > 2 and args[2].isdigit() else 0
        prompt_flag = args[3] if len(args) > 3 else "N"
        return cls(
            input_csv_path=input_csv,
            chunk_csv_path=chunk_csv,
            chunk_size=chunk_sz,
            prompt_off=prompt_flag.upper() == "Y",
        )

    def build_scope(
        self,
        blocks: list[ClassifiedBlock],
        start_i: int,
        state: ScopeIdSource,
        parse_children: ParseChildrenFn,
    ) -> tuple[ScopeNode, int]:
        """Build a 'loop' ScopeNode, consuming blocks until {END-LOOP}."""
        start_block = blocks[start_i]
        children, i, end_token = parse_children(
            blocks, start_i + 1, {"END-LOOP"}, state
        )

        if end_token != "END-LOOP":
            loc = (
                f"{start_block.span.file or '<input>'}:{start_block.span.start_line}:1"
            )
            log.error(
                f"[unclosed-loop] {loc} (block {start_block.index}): "
                "Found {RUN-LOOP} without a matching {END-LOOP}; implicitly closed at EOF."
            )
            end_index = blocks[-1].index if blocks else start_block.index
            return (
                ScopeNode(
                    scope_id=state.new_scope_id(),
                    kind="loop",
                    start_index=start_block.index,
                    end_index=end_index,
                    children=tuple(children),
                    block_index=None,
                    control_payload=self,
                ),
                i,
            )

        end_index = blocks[i].index
        return (
            ScopeNode(
                scope_id=state.new_scope_id(),
                kind="loop",
                start_index=start_block.index,
                end_index=end_index,
                children=tuple(children),
                block_index=None,
                control_payload=self,
            ),
            i + 1,
        )

    # ------------------------------------------------------------------
    # Emission
    # ------------------------------------------------------------------

    def emit_scope(
        self,
        writer: IndentWriter,
        walk: Callable[[ScopeNode], None],
        children: tuple[ScopeNode, ...],
    ) -> None:
        """Emit a chunked for-loop over the input CSV."""
        from vg2c.utilities.csv_io import CsvIO

        chunks_call = CsvIO.iter_chunks.render(
            repr(self.input_csv_path),
            repr(self.chunk_csv_path),
            int(self.chunk_size),
        )
        writer.write(f"for __chunk_path in {chunks_call}:")
        writer.push_indent()
        for child in children:
            walk(child)
        writer.pop_indent()


@dataclass(frozen=True, slots=True)
class EndLoop:
    pass
