"""Per-run named macros with lexical frames and direct-runtime substitution rules."""

from __future__ import annotations

from collections.abc import Iterator, Mapping
from contextlib import contextmanager

from vg2c.runtime.values import substitute


class MacroStore(Mapping[str, object]):
    """One per-run macro store.

    Names are case-insensitive. Reads expand exactly as `values.substitute`
    would: missing ordinary names raise ValueError, reserved SPF tokens can
    remain literal, and CL_/SPF-JOB-/SPF-DEFAULT-/%...% use the values snapshot.
    Assignment writes only into the active frame. Never share an instance
    between independent jobs.
    """

    def __init__(self, *, values: Mapping | None = None, initial: Mapping | None = None):
        self._values = {str(key).upper(): value for key, value in (values or {}).items()}
        self._frames: list[dict[str, object]] = [self._normalize(initial)]

    @staticmethod
    def _normalize(mapping: Mapping | None) -> dict[str, object]:
        return {str(key).upper(): value for key, value in (mapping or {}).items()
                if key is not None}

    def _snapshot(self) -> dict[str, object]:
        result: dict[str, object] = {}
        for frame in self._frames:
            result.update(frame)
        return result

    def __getitem__(self, name: str) -> str:
        # Use the existing, single token resolver (including its diagnostics).
        key = str(name).strip().upper()
        return substitute(f"<<<{key}>>>", values=self._values, macros=self._snapshot())

    def __setitem__(self, name: str, value: object) -> None:
        self._frames[-1][str(name).upper()] = value

    def __iter__(self) -> Iterator[str]:
        return iter(self._snapshot())

    def __len__(self) -> int:
        return len(self._snapshot())

    def items(self):
        # Runtime direct operations need raw values, not recursively expanded
        # bracket reads. This also preserves None for the canonical resolver.
        return self._snapshot().items()

    def get(self, name: str, default=None):
        key = str(name).strip().upper()
        if key not in self._snapshot() and key not in self._values:
            return default
        return self[name]

    def substitute(self, text: str) -> str:
        """Resolve using the authoritative direct-runtime token semantics."""
        return substitute(text, values=self._values, macros=self._snapshot())

    @contextmanager
    def scope(self, row: Mapping | None = None) -> Iterator[MacroStore]:
        """Push a normalized overlay, always restoring the preceding frame."""
        self._frames.append(self._normalize(row))
        try:
            yield self
        finally:
            self._frames.pop()
