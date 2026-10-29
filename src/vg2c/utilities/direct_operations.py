"""Compiler metadata for direct job operations; no executable pipeline context.

The name 'ctx' is a historical metadata identity consumed by the compiler's
edit/source-map model. Emitted jobs invoke JobRuntime, never this class.
"""

from __future__ import annotations

from typing import Any

from vg2c.emitter.operations import emittable
from vg2c.utilities._base import UtilitySpec


class DirectOperationMetadata(UtilitySpec):
    utility_name = "ctx"

    @emittable
    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        raise NotImplementedError("Compiler-only metadata; use JobRuntime.write_file")

    @emittable(artifact_roles={"output": True, "inputs": True})
    def run_query(
        self,
        sql: str,
        output: str,
        reader: Any = None,
        inputs: list[str | tuple[str, str]] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
        node: str | None = None,
        pivot_columns: str | None = None,
        pivot_values: str | list[str] | None = None,
        pivot_duplicate: str = "first",
        pivot_missing: str = "",
        pivot_dot: bool = False,
        pivot_sort: str | None = None,
        pivot_header_ref: str | None = None,
        pivot_legacy_headers: bool = False,
    ):
        raise NotImplementedError("Compiler-only metadata; use JobRuntime.sql")
