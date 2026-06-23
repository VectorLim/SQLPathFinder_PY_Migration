from __future__ import annotations

from vg2c.classifier.model import EngineKind, RecordRef
from vg2c.classifier.routing import route


def test_route_sqlite_by_engine() -> None:
    opts = {"ENGINE": "SQLite"}
    target = route(opts, "", None)
    assert target.kind == EngineKind.SQLITE


def test_route_sqlite_by_oledb() -> None:
    opts = {"OLEDB": "SQLite"}
    target = route(opts, "", None)
    assert target.kind == EngineKind.SQLITE


def test_route_aries_by_node() -> None:
    opts = {"NODE": "KM.ARIES", "ENGINE": "VA"}
    target = route(opts, "", None)
    assert target.kind == EngineKind.ARIES


def test_route_oracle_oasys_by_node() -> None:
    opts = {"NODE": "KM.OASYS", "ENGINE": "VA"}
    target = route(opts, "", None)
    assert target.kind == EngineKind.ORACLE_OASYS
    assert target.schema_hint == "@OASYSSCHEMA@"


def test_route_oracle_oasys_by_body() -> None:
    opts = {"ENGINE": "VA"}
    body = "SELECT * FROM @OASYSSCHEMA@P_SPC_Batch"
    target = route(opts, body, None)
    assert target.kind == EngineKind.ORACLE_OASYS


def test_route_oracle_oasys_by_record_prefix() -> None:
    opts = {"ENGINE": "VA"}
    record = RecordRef(name="P_SPC_Chart_or_Raw_Data", version="1.0.0.0")
    target = route(opts, "", record)
    assert target.kind == EngineKind.ORACLE_OASYS


def test_route_oracle_mars_by_body() -> None:
    opts = {"ENGINE": "VA"}
    body = "SELECT * FROM @[]@F_Lot_History"
    target = route(opts, body, None)
    assert target.kind == EngineKind.ORACLE_MARS
    assert target.schema_hint == "@[]@"


def test_route_oracle_mars_by_record_prefix() -> None:
    opts = {"OLEDB": "SQLPlus"}
    record = RecordRef(name="F_Calendar", version="1.0.0.0")
    target = route(opts, "", record)
    assert target.kind == EngineKind.ORACLE_MARS


def test_route_oracle_mars_default() -> None:
    opts = {"ENGINE": "VA"}
    target = route(opts, "", None)
    assert target.kind == EngineKind.ORACLE_MARS


def test_route_none() -> None:
    opts = {}
    target = route(opts, "", None)
    assert target.kind == EngineKind.NONE
