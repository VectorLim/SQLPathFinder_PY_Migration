"""PipelineContext - runtime context container."""

from __future__ import annotations

from typing import Callable, Any, ContextManager

from vg2c.emitter.models import emittable
from vg2c.utilities._base import UtilitySpec
from vg2c.utilities.oracle_client import OracleClient


class PipelineContext(UtilitySpec):
    """Single runtime context object for generated scripts."""

    utility_name = "ctx"

    def __init__(self) -> None:
        registry = getattr(type(self), "_registry", None)
        if isinstance(registry, dict) and registry:
            candidates = list(registry.items())
        else:
            candidates = []
            for obj in globals().values():
                if not isinstance(obj, type):
                    continue
                utility_name = getattr(obj, "utility_name", None)
                if isinstance(utility_name, str):
                    candidates.append((utility_name, obj))

        for utility_name, utility_cls in candidates:
            if utility_name == self.utility_name:
                continue
            try:
                setattr(self, utility_name, utility_cls())
            except TypeError:
                continue

    def get_method(self, utility_cls: type[UtilitySpec], method_func: Callable) -> Any:
        """Get a method from a utility class."""
        if not hasattr(self, utility_cls.utility_name):
            raise AttributeError(
                f"Utility '{utility_cls.utility_name}' not found in PipelineContext."
            )

        utility_instance = getattr(self, utility_cls.utility_name)
        method = getattr(utility_instance, method_func.__name__, None)
        if method is None:
            raise AttributeError(
                f"Method '{method_func.__name__}' not found in utility '{utility_cls.utility_name}'."
            )
        return method

    @emittable
    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        content = self.macro.substitute(template, vars=vars)
        self.fs_ops.write_file(path, content)

    def _read_datasyncx(self, sql: str, reader: Any):
        try:
            result = reader.read(site="KM", query=sql)
        finally:
            OracleClient.log_active_client()
        result.columns = [col.lower() for col in result.columns]
        return result

    @emittable
    def run_query(
        self,
        sql,
        output: str,
        reader: Any,
        inputs: list[str] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
    ):
        sql = self.macro.substitute(sql)

        if hasattr(reader, "execute"):
            result = reader.execute(sql, inputs or [])
        else:
            result = self._read_datasyncx(sql, reader)

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
