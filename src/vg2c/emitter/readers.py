"""Embedded reader runtime for emitted pipeline scripts.

Stage 5 injects ``READER_SNIPPET`` into translated VG2 scripts so the
generated file can dispatch SQL reads on its own, without depending on
``vg2c_runtime.context.PipelineContext.read``.

The actual runtime code lives in ``_reader.py`` as real Python (for tooling
and editing). This module slices the snippet out of that file at import time
between the ``BEGIN`` / ``END`` sentinel comments.

To support a new database type, add an entry to ``DATABASE_TYPE_MAP`` inside
``_reader.py``. The top-level ``read(sql, db_type, macro_state=None)``
function in the emitted script will dispatch to it.
"""

from __future__ import annotations

from pathlib import Path

from vg2c.emitter.models import EmitContext

__all__ = ["READER_SNIPPET", "register_reader_emission"]


_READER_SOURCE_PATH = Path(__file__).parent / "_reader.py"
_BEGIN_SENTINEL = "# --- Embedded reader runtime"
_END_SENTINEL = "# --- end embedded reader runtime"


def _load_reader_snippet() -> str:
    """Read ``_reader.py`` and return the runtime block between sentinels."""
    source = _READER_SOURCE_PATH.read_text(encoding="utf-8")
    lines = source.splitlines(keepends=True)
    start = next(
        (i for i, line in enumerate(lines) if line.startswith(_BEGIN_SENTINEL)),
        None,
    )
    end = next(
        (i for i, line in enumerate(lines) if line.startswith(_END_SENTINEL)),
        None,
    )
    if start is None or end is None or end < start:
        raise RuntimeError(f"Reader sentinels not found in {_READER_SOURCE_PATH}")
    return "".join(lines[start : end + 1])


READER_SNIPPET = _load_reader_snippet()


def register_reader_emission(ctx: EmitContext) -> None:
    """Mark the emitted script as needing the reader snippet."""
    ctx.needs_reader = True
