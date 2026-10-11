"""Compiler-only VG2 macro-control classification and emission metadata.

Runtime macro storage and substitution belong exclusively to vg2c.runtime.MacroStore.
"""
from __future__ import annotations

import re

from vg2c.emitter.models import emittable
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility


class MacroControl(EmitterUtility):
    utility_name = "macro"
    semantic_visibility = "internal"
    handles = (Kind.MACRO_CONTROL,)

    PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>|<<>>")
    NAMED_PLACEHOLDER_RE = re.compile(r"<<<([^>]+)>>>")
    _TOKEN = re.compile(
        r"^\s*\{(START-MACRO|END-MACRO|IF-THEN|ELSE|END-IF|RUN-LOOP|FOR-LOOP|SITE-LOOP|END-LOOP)\}",
        re.IGNORECASE,
    )

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if MacroControl._TOKEN.match(options.lookup.get("UTILITIES", "")):
            return Kind.MACRO_CONTROL, "/UTILITIES is a macro control token"
        return None

    @classmethod
    def emit_block(cls, block, *, global_refs=None):
        # Native control-flow emission is owned by emitter.project.
        return None

    @emittable
    def named(self, name: str) -> str:
        raise NotImplementedError("Compiler metadata only; use MacroStore at runtime")

    @emittable
    def set_named(self, name: str, value: str) -> None:
        raise NotImplementedError("Compiler metadata only; use MacroStore at runtime")

    @emittable
    def positional(self) -> str:
        raise NotImplementedError("Positional macros have no supported source cursor")
