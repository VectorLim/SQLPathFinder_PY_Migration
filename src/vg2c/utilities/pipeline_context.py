"""PipelineContext - runtime context container."""

from __future__ import annotations

import os
from typing import Any

from vg2c.emitter.models import ArtifactRole, emittable
from vg2c.utilities._base import UtilitySpec
from vg2c.utilities.oracle_client import OracleClient


class PipelineContext(UtilitySpec):
    """Single runtime context object for generated scripts."""

    utility_name = "ctx"

    def __init__(self, utilities: dict[str, object]) -> None:
        self.__dict__.update(utilities)

    @emittable
    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        content = self.macro.substitute(template, vars=vars)
        self.fs_ops.write_file(path, content)

    def _read_datasyncx(self, sql: str, reader: Any, node: str):
        try:
            result = reader.read(site=node, query=sql)
        finally:
            OracleClient.log_active_client()
        result.columns = [col.lower() for col in result.columns]
        return result

    @emittable(
        parameter_capabilities={"sql": ("structured-sql",)},
        artifact_roles={
            "output": ArtifactRole("output"),
            "inputs": ArtifactRole("input", many=True),
        },
    )
    def run_query(
        self,
        sql: str,
        output: str,
        reader: Any,
        inputs: list[str] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
        node: str | None = None,
    ):
        sql = self.macro.substitute(sql)
        effective_node = (
            node or self.macro.named("NODE") or os.environ.get("VG2C_DEFAULT_NODE", "KM")
        )

        if hasattr(reader, "execute"):
            result = reader.execute(sql, inputs or [])
        else:
            result = self._read_datasyncx(sql, reader, effective_node)

        if crosstab:
            result = self.crosstab.apply(
                result,
                row_keys=crosstab["row_keys"],
                header_key=crosstab["header_key"],
                value_key=crosstab["value_key"],
            )

        self.csv_io.write(output, result, header=header)

    @emittable
    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)
