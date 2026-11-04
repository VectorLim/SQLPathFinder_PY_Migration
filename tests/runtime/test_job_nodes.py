"""Site/database routing for JobRuntime.sql."""

import pytest

from vg2c.runtime import JobRuntime
from vg2c.runtime.nodes import site_facility, site_node


@pytest.mark.parametrize(
    ("database", "site", "expected"),
    [
        ("MARS", "KM", "KM.[A15_PROD_21.].MARS"),
        ("mars", "cd", "CD.[A48_PROD_21.].MARS"),
        ("MARS", "PG", "PG.[A12_PROD_0.].MARS"),
        ("MARS", "KM8", "KM8.[A88_PROD_21.].MARS"),
        ("MARS", "VN", "VN.[A90_PROD_21.].MARS"),
        ("ARIES", "KM", "KM.ARIES"),
        ("oasys", "vn", "VN.OASYS"),
    ],
)
def test_site_node(database, site, expected):
    assert site_node(database, site) == expected


@pytest.mark.parametrize(("database", "site"), [("MARS", "XX"), ("FOO", "KM")])
def test_site_node_rejects_unknown(database, site):
    with pytest.raises(ValueError):
        site_node(database, site)


def test_site_facility():
    assert site_facility("km") == "A15"
    assert site_facility("VN") == "A90"


def _job(tmp_path, site=None, environ=None):
    return JobRuntime(tmp_path, tmp_path / "out", environ=environ or {}, site=site)


def test_default_site_from_environment(tmp_path):
    assert _job(tmp_path, environ={"SITE": "vn"}).site == "VN"


def test_script_site_overrides_environment(tmp_path):
    assert _job(tmp_path, site="CD", environ={"SITE": "VN"}).site == "CD"


def test_blank_environment_site_is_ignored(tmp_path):
    assert _job(tmp_path, environ={"SITE": "  "}).site is None


def test_sql_uses_default_site_and_per_query_override(tmp_path):
    job = _job(tmp_path, site="km")
    assert job._resolve_node(None, "MARS", None) == "KM.[A15_PROD_21.].MARS"
    assert job._resolve_node(None, "ARIES", "CD") == "CD.ARIES"
    assert job._resolve_node("RAW.NODE", None, None) == "RAW.NODE"
    assert job._resolve_node(None, None, None) is None


@pytest.mark.parametrize(
    ("site", "node", "database", "query_site"),
    [
        (None, None, "MARS", None),
        ("KM", "RAW.NODE", "MARS", None),
        ("KM", None, None, "CD"),
    ],
)
def test_sql_routing_errors(tmp_path, site, node, database, query_site):
    with pytest.raises(ValueError):
        _job(tmp_path, site)._resolve_node(node, database, query_site)


def test_unknown_default_site_fails_fast(tmp_path):
    with pytest.raises(ValueError, match="Unknown site"):
        _job(tmp_path, site="XX")


def test_aed_facility_from_site_unless_macro_set(tmp_path, monkeypatch):
    monkeypatch.setattr("vg2c.runtime.job.AEDClient", lambda facility, **_: facility)
    environ = {"ENV_MODE": "prod"}
    assert _job(tmp_path, site="CD", environ=environ).aed == "A48"
    job = _job(tmp_path, site="CD", environ=environ)
    job.macros["FACILITY"] = "A15"
    assert job.aed == "A15"
    with pytest.raises(ValueError, match="No AED facility"):
        _ = _job(tmp_path, environ=environ).aed
