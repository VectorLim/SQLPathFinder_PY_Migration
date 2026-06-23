from __future__ import annotations

from vg2c.classifier.coerce import (
    as_bool_yn,
    as_csv_list,
    as_int,
    as_path_string,
    as_record_ref,
)
from vg2c.classifier.model import (
    EngineKind,
    Kind,
    Role,
    SqlFetchSpec,
    SqliteJoinSpec,
)
from vg2c.classifier.routing import route
from vg2c.classifier.rules.base import Match
from vg2c.model import ParsedBlock


class SqlExecutionRule:
    """Match SQL execution blocks (Oracle, Aries, SQLite)."""

    name = "sql_execution"

    def match(self, b: ParsedBlock) -> Match | None:
        """Match SQL execution directives."""
        record = as_record_ref(b.options.get("RECORD"))
        engine = route(b.options, b.body, record)

        # SQLite join
        if engine.kind == EngineKind.SQLITE:
            spec = SqliteJoinSpec(
                csv_out=as_path_string(b.options.get("CSV")) or "",
                tables=as_csv_list(b.options.get("TABLE")),
                headers=as_csv_list(b.options.get("HEADERS")),
                delete_patterns=as_csv_list(b.options.get("DELETE")),
                sqlite_dt=b.options.get("SQLITE_DT"),
                reset=as_bool_yn(b.options.get("RESET")),
                create_temp_table=as_bool_yn(b.options.get("T")),
                body=b.body,
                instance=as_int(b.options.get("INSTANCE")),
                prompt=b.options.get("PROMPT-TEXT"),
            )
            return Match(Kind.SQLITE_JOIN, Role.LEAF, spec, engine.reason)

        # Oracle or Aries fetch
        if engine.kind in {
            EngineKind.ORACLE_MARS,
            EngineKind.ORACLE_OASYS,
            EngineKind.ORACLE_GENERIC,
            EngineKind.ARIES,
        }:
            sql_body = b.body.strip()
            if sql_body.startswith("/*BEGIN SQL*/"):
                sql_body = sql_body[len("/*BEGIN SQL*/") :].strip()
            if sql_body.endswith("/*END SQL*/"):
                sql_body = sql_body[: -len("/*END SQL*/")].strip()

            spec = SqlFetchSpec(
                engine=engine,
                record=record,
                csv_out=as_path_string(b.options.get("CSV")) or "",
                headers=as_csv_list(b.options.get("HEADERS")),
                sql_body=sql_body,
                instance=as_int(b.options.get("INSTANCE")),
                prompt=b.options.get("PROMPT-TEXT"),
            )
            return Match(Kind.SQL_FETCH, Role.LEAF, spec, engine.reason)

        return None
