"""Compiler-only VG2 macro-control classification and emission metadata.

Runtime macro storage and substitution belong exclusively to vg2c.runtime.MacroStore.
"""

from __future__ import annotations

import re

from vg2c.emitter.models import BlockEmission, RenderedCall
from vg2c.emitter.operations import emittable
from vg2c.kind import Kind
from vg2c.runtime.values import NAMED_MACRO_RE
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import native_call_arguments


class MacroControl(EmitterUtility):
    utility_name = "macro"
    handles = (Kind.MACRO_CONTROL,)

    PLACEHOLDER_RE = re.compile(r"<<<(.+?)>>>|<<>>", re.DOTALL)
    NAMED_PLACEHOLDER_RE = NAMED_MACRO_RE
    _TOKEN = re.compile(
        r"^\s*\{(START-MACRO|END-MACRO|IF-THEN|ELSE|END-IF|RUN-LOOP|FOR-LOOP|SITE-LOOP|END-LOOP)\}",
        re.IGNORECASE,
    )

    @staticmethod
    def check(options) -> Kind | None:
        if MacroControl._TOKEN.match(options.lookup.get("UTILITIES", "")):
            return Kind.MACRO_CONTROL
        return None

    @classmethod
    def emit_block(cls, block, *, macros: str = "macros") -> BlockEmission:
        raise ValueError("Unexpected or unmatched control token")

    @classmethod
    def render_named_assignment(cls, call: RenderedCall, macros: str) -> RenderedCall:
        arguments, _ = native_call_arguments(call, macros)
        name, value = arguments
        source = f"{macros}[{name.source}] = {value.source}"
        return source

    @emittable
    def named(self, name: str) -> str:
        raise NotImplementedError("Compiler metadata only; use MacroStore at runtime")

    @emittable
    def set_named(self, name: str, value: str) -> None:
        raise NotImplementedError("Compiler metadata only; use MacroStore at runtime")

    @emittable
    def positional(self) -> str:
        raise NotImplementedError("Positional macros have no supported source cursor")
