"""PipelineContext - runtime context container."""

from __future__ import annotations

from typing import Any, ContextManager

from datasyncx.readers import AriesReader, MarsReader, OracleReader

from vg2c.emitter.utilities._base import UtilitySpec


class PipelineContext(UtilitySpec):
    """Single runtime context object for generated scripts."""

    utility_name = "ctx"
    DATASYNCX_READER_MAP = {
        "MARS": MarsReader,
        "OASYS": OracleReader,
        "ARIES": AriesReader,
    }

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

    def __getattr__(self, name: str):
        def _missing(*args: Any, **kwargs: Any) -> None:
            print("not implemented yet")

        return _missing

    def macro_scope(self, row: dict[str, str] | None = None) -> ContextManager[None]:
        return self.macro.scope(row=row)

    def write_file(
        self,
        path: str,
        template: str,
        vars: dict[str, str] | None = None,
    ) -> None:
        self.macro.write_file(path, template, vars=vars)

    def _read_datasyncx(self, sql: str, source_type: str):
        source_type_u = source_type.upper()
        if source_type_u not in self.DATASYNCX_READER_MAP:
            raise ValueError(f"Unsupported database type: {source_type!r}")
        result = self.DATASYNCX_READER_MAP[source_type_u]().read(
            site="KM",
            query=sql,
        )
        result.columns = [col.lower() for col in result.columns]
        return result

    def run_query(
        self,
        sql,
        output: str,
        source_type: str,
        inputs: list[str] | None = None,
        header: list[str] | None = None,
        crosstab: dict | None = None,
    ):
        sql = self.macro.substitute_sql(sql)

        if source_type.lower() == "sqlite":
            result = self.sqlite_engine.execute(sql, inputs or [])
        else:
            result = self._read_datasyncx(sql, source_type)

        if crosstab:
            result = self.crosstab.apply(
                result,
                row_keys=crosstab["row_keys"],
                header_key=crosstab["header_key"],
                value_key=crosstab["value_key"],
            )

        self.csv_io.write(output, result, header=header)

    def eval_condition(self, lhs: str, op: str, rhs: str, *args: Any) -> bool:
        return self.macro.eval_condition(lhs, op, rhs)
