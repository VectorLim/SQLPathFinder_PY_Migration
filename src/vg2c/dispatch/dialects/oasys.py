from __future__ import annotations

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import DispatchConfig
from vg2c.frontend.models import Diagnostic, Kind, SourceSpan

_OASYS_PLACEHOLDER = "@OASYSSCHEMA@"


class OasysDialect(DialectHandler):
    """Handler for Oracle OASYS dialect."""

    dialect = "oracle_oasys"
    kind = Kind.SQL_QUERY
    database_arg = "OASYS"
    schema_placeholder = _OASYS_PLACEHOLDER
    datasyncx_reader_name = "OracleReader"

    @classmethod
    def matches_signals(cls, node: str, engine: str, oledb: str) -> bool:
        node_u = node.upper()
        engine_u = engine.upper()
        oledb_u = oledb.upper()
        return (engine_u == "VA" or oledb_u == "SQLPLUS") and "OASYS" in node_u

    @classmethod
    def substitute(
        cls,
        body: str,
        config: DispatchConfig | None,
        span: SourceSpan | None,
        block_index: int,
    ) -> tuple[str, list[Diagnostic]]:
        diags: list[Diagnostic] = []

        if _OASYS_PLACEHOLDER not in body:
            return body, diags

        if config is None:
            diags.append(
                Diagnostic(
                    severity="error",
                    code="dispatch-oasys-schema-unset",
                    message=(
                        "@OASYSSCHEMA@ placeholder present but no DispatchConfig provided; "
                        "placeholder left in place."
                    ),
                    block_index=block_index,
                    span=span,
                )
            )
            return body, diags

        if config.oasys_schema == "":
            diags.append(
                Diagnostic(
                    severity="warning",
                    code="dispatch-oasys-schema-unset",
                    message=(
                        "@OASYSSCHEMA@ placeholder present but oasys_schema is empty; "
                        "placeholder left in place."
                    ),
                    block_index=block_index,
                    span=span,
                )
            )
            return body, diags

        # Replace all occurrences
        new_body = body.replace(_OASYS_PLACEHOLDER, config.oasys_schema + ".")
        return new_body, diags
