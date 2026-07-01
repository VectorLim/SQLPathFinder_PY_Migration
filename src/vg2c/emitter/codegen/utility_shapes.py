"""Shape → CallSpec dispatch for ``Kind.UTILITY`` blocks.

Each entry maps a :class:`~vg2c.emitter.utility_shapes.UtilityShape` to a
function that builds a :class:`CallSpec` (and any auxiliary lines) from the
parsed ``UtilityInfo``. ``None`` means "emit a TODO stub".

Adding a new shape = new entry in :data:`SHAPE_DISPATCH`; no handler edit.
"""

from __future__ import annotations

import os
from dataclasses import dataclass
from typing import Callable

from vg2c.emitter.codegen.call_spec import CallSpec
from vg2c.emitter.codegen.emit_call import emit_call
from vg2c.emitter.codegen.expr import PyExpr
from vg2c.emitter.codegen.options import python_literal_for_option
from vg2c.emitter.utilities import ExternalProcess, FileSystemOps
from vg2c.emitter.utility_shapes import UtilityInfo

__all__ = ["ShapeResult", "SHAPE_DISPATCH", "build_shape_result"]


@dataclass(frozen=True, slots=True)
class ShapeResult:
    """Outcome of dispatching a utility shape.

    ``call`` is the rendered call to emit; ``stub_message`` is set instead
    when the shape has no real translation yet (handler emits a ``pass``
    function with this comment).
    """

    call: CallSpec | None = None
    stub_message: str | None = None


def _argv_expr(argv: tuple[str, ...]) -> PyExpr:
    return PyExpr.list_of([python_literal_for_option(token) for token in argv])


def _shape_run_python_script(info: UtilityInfo) -> ShapeResult:
    return ShapeResult(
        call=emit_call(ExternalProcess.run, _argv_expr(info.argv)),
    )


def _shape_email(info: UtilityInfo) -> ShapeResult:
    # TODO: SQLPathFinder_Email.va argv positions are not standardised yet.
    return ShapeResult(stub_message="email utility — argv positions unresolved")


def _shape_robocopy(info: UtilityInfo) -> ShapeResult:
    # RoboCopy.va argv: <exe> <filename> <source_dir> <dest_dir> [flags...]
    filename = (
        python_literal_for_option(info.argv[1])
        if len(info.argv) > 1
        else PyExpr.literal("")
    )
    src_dir = (
        python_literal_for_option(info.argv[2])
        if len(info.argv) > 2
        else PyExpr.literal("")
    )
    dst = (
        python_literal_for_option(info.argv[3])
        if len(info.argv) > 3
        else PyExpr.literal(".")
    )
    src = PyExpr.raw(f"os.path.join({src_dir.source}, {filename.source})")
    return ShapeResult(
        call=emit_call(FileSystemOps.copy, src=src, dst=dst),
    )


def _shape_spf_delete(info: UtilityInfo) -> ShapeResult:
    # SPFDelete arg[1] is a comma-joined path list.
    raw = info.argv[1] if len(info.argv) > 1 else ""
    items = [p.strip() for p in raw.split(",") if p.strip()]
    paths_expr = PyExpr.list_of([python_literal_for_option(p) for p in items])
    return ShapeResult(
        call=emit_call(FileSystemOps.delete, paths=paths_expr),
    )


def _shape_external_argv(info: UtilityInfo) -> ShapeResult:
    return ShapeResult(
        call=emit_call(ExternalProcess.run, _argv_expr(info.argv)),
    )


def _shape_unknown(info: UtilityInfo) -> ShapeResult:
    return ShapeResult(stub_message=f"unhandled utility shape={info.shape}")


SHAPE_DISPATCH: dict[str, Callable[[UtilityInfo], ShapeResult]] = {
    "run-python-script": _shape_run_python_script,
    "email": _shape_email,
    "robocopy": _shape_robocopy,
    "spf-copy": _shape_robocopy,  # same argv shape today
    "spf-delete": _shape_spf_delete,
    "bat-file": _shape_external_argv,
    "exe-direct": _shape_external_argv,
    "unknown": _shape_unknown,
}


def build_shape_result(info: UtilityInfo) -> ShapeResult:
    """Look up the dispatcher for *info.shape* and apply it."""
    handler = SHAPE_DISPATCH.get(info.shape, _shape_unknown)
    return handler(info)


# Re-export os so handlers don't need a separate import when emitting
# robocopy results (the rendered ``os.path.join(...)`` is valid Python in
# the generated script because the script already imports ``os``).
_ = os  # noqa: F401
