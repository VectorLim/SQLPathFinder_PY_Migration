from __future__ import annotations

from typing import TYPE_CHECKING

from vg2c.emitter.models import EmittedScript

if TYPE_CHECKING:
    from vg2c.dispatch.models import DispatchedProgram


def emit(dispatched: DispatchedProgram) -> EmittedScript:
    """Emit native Python and editable assets from the single existing scope tree."""
    from vg2c.emitter.project import emit_project

    return emit_project(dispatched)
