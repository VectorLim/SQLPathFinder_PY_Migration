"""Compiler metadata for direct job operations; no executable pipeline context.

The name 'ctx' is a historical metadata identity consumed by the compiler's
edit/source-map model. Emitted jobs invoke JobRuntime, never this class.
"""

from __future__ import annotations

from typing import Any

from vg2c.emitter.models import ArtifactRole
from vg2c.emitter.operations import emittable
from vg2c.utilities._base import UtilitySpec
from vg2c.utility_metadata import FileEffectDefinition, ValueSchema


class DirectOperationMetadata(UtilitySpec):
    utility_name = "ctx"

    @emittable(
        display_name="Write File",
        summary="{path}",
        file_effects=(FileEffectDefinition("write", "write", outputs=("path",)),),
    )
    def write_file(
        self, path: str, template: str, vars: dict[str, str] | None = None
    ) -> None:
        raise NotImplementedError("Compiler-only metadata; use JobRuntime.write_file")

    @emittable(
        display_name="Run Query",
        summary="Output: {output}",
        file_effects=(
            FileEffectDefinition(
                "query",
                "transform",
                inputs=("inputs",),
                outputs=("output",),
                input_base="runtime-search",
                input_format="table-binding",
            ),
        ),
        parameter_capabilities={"sql": ("structured-sql",)},
        artifact_roles={
            "output": ArtifactRole("output"),
            "inputs": ArtifactRole("input", many=True),
        },
        parameter_visibility={"reader": "internal"},
        parameter_schemas={
            "crosstab": ValueSchema(
                "object",
                nullable=True,
                properties=(
                    ("row_keys", ValueSchema("list", items=ValueSchema("string"))),
                    ("header_key", ValueSchema("string")),
                    ("value_key", ValueSchema("string")),
                ),
                required_keys=("row_keys", "header_key", "value_key"),
            ),
        },
    )
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
