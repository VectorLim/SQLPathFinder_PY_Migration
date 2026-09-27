from __future__ import annotations

import codecs
from dataclasses import dataclass
from pathlib import Path

import pandas as pd
import pytest

import scripthost_portable.query_transport as query_transport
from scripthost_portable import (
    DataSyncXReaderFactory,
    PortableOracleConnection,
    PortableScriptHostRuntime,
    QueryConfigurationError,
    QueryTransportUnavailable,
    UnsupportedQueryBackend,
    use_reader_factory,
)

DELIM = "<---- New Query ---->"


@dataclass
class ReaderCall:
    backend: str
    node: str
    site: str
    query: str


class FakeReader:
    def __init__(
        self,
        backend: str,
        node: str,
        frame: pd.DataFrame,
        calls: list[ReaderCall],
    ) -> None:
        self.backend = backend
        self.node = node
        self.frame = frame
        self.calls = calls

    def read(self, *, site: str, query: str) -> pd.DataFrame:
        self.calls.append(ReaderCall(self.backend, self.node, site, query))
        return self.frame.copy()


class FakeReaderFactory:
    def __init__(self, frames: dict[str, pd.DataFrame]) -> None:
        self.frames = frames
        self.calls: list[ReaderCall] = []
        self.requested: list[tuple[str, str]] = []

    def reader_for(self, backend: str, node: str) -> FakeReader:
        self.requested.append((backend, node))
        return FakeReader(backend, node, self.frames[backend], self.calls)


def block(*options: str, body: str = "") -> str:
    option_text = "\n".join(options)
    return f"<OPTIONS>\n{option_text}\n</OPTIONS>\n{body}"


def _real_22844_segments() -> tuple[str, str, str]:
    repo_root = Path(__file__).resolve().parents[2]
    fixture = repo_root / "scripthost-utilities-decompiled" / "22844.spfsql"
    segments = [segment.strip() for segment in fixture.read_text(encoding="utf-8-sig").split(DELIM)]
    mars = next(segment for segment in segments if "/NODE=KM.[A15_PROD_21.].MARS" in segment)
    aries = next(segment for segment in segments if "/NODE=KM.ARIES" in segment)
    sqlite = next(
        segment
        for segment in segments
        if "/OLEDB=SQLite" in segment and "/CSV=XRAY_results.csv" in segment
    )
    return mars, aries, sqlite


def _mars_frame() -> pd.DataFrame:
    return pd.DataFrame(
        [
            {
                "facility": "KM",
                "site_work_week_": 202640,
                "operation": "0265",
                "prodgroup3_": "P3",
                "lot_": "LOT_A",
                "owner": "OWNER_A",
                "out_date": "2026-09-27 12:00:00",
            }
        ]
    )


def _aries_frame() -> pd.DataFrame:
    return pd.DataFrame(
        [
            {
                "site_work_week": 202640,
                "prodgroup3": "P3",
                "lot": "LOT_A",
                "visual_id": "VID_A",
                "image_name": "IMG_V1.JPG",
                "image_full_path_RAW": r"\\server\raw\IMG_V1.JPG",
                "rank": 1,
                "rank_": 1,
                "maxrank_": 1,
                "filter_": 1,
                "image_full_path": r"\\server\normalized\IMG_V1.JPG",
                "numeric_value": 5.0,
                "numeric_value_max": 5.0,
            }
        ]
    )


def test_real_22844_getquery_routes_mars_and_aries_to_original_oracle_task(
    tmp_path: Path,
) -> None:
    from scripthost_portable.runtime import _spf_manager_type

    mars, aries, _ = _real_22844_segments()
    manager = _spf_manager_type()()
    manager.gCommandLineArguments = [
        str(tmp_path / "SPFSQL3.py"),
        f'/MYLOCAL="{tmp_path}"',
        "/EXECMODE=UT",
    ]

    for index, segment in enumerate((mars, aries)):
        task = manager.GetQuery(manager.gMyLocal, segment, index, False)
        assert task.__class__.__name__ == "nqOracleTask"


def test_datasyncx_factory_distinguishes_transport_from_configuration_failure(
    monkeypatch,
) -> None:
    factory = DataSyncXReaderFactory()

    def missing_reader(*args, **kwargs):
        del args, kwargs
        raise QueryTransportUnavailable("datasyncx missing")

    monkeypatch.setattr(query_transport, "_import_first", missing_reader)
    with pytest.raises(QueryTransportUnavailable, match="datasyncx missing"):
        factory.reader_for("mars", "KM.MARS")

    class BrokenReader:
        def __init__(self) -> None:
            raise RuntimeError("bad config")

    monkeypatch.setattr(query_transport, "_import_first", lambda *args, **kwargs: BrokenReader)
    with pytest.raises(QueryConfigurationError, match="construction/configuration failed"):
        factory.reader_for("mars", "KM.MARS")


def test_portable_connection_routes_mars_aries_and_oasys(tmp_path: Path) -> None:
    factory = FakeReaderFactory(
        {
            "mars": pd.DataFrame({"value": ["mars"]}),
            "aries": pd.DataFrame({"value": ["aries"]}),
            "oasys": pd.DataFrame({"value": ["oasys"]}),
        }
    )
    cases = (
        ("KM.[A15_PROD_21.].MARS", "mars", "KM"),
        ("KM.ARIES", "aries", "KM"),
        ("PG.OASYS", "oasys", "PG"),
    )

    for index, (node, backend, site) in enumerate(cases):
        connection = PortableOracleConnection(reader_factory=factory)
        connection.openConnection("ignored", "ignored", node, None)
        output = tmp_path / f"result-{index}.tab"
        assert connection.execute("select 1", OutExcel=str(output)) == 1
        assert factory.calls[-1] == ReaderCall(backend, node, site, "select 1")
        assert output.read_text(encoding="utf-8") == f"value\n{backend}\n"


def test_portable_connection_characterizes_output_format(tmp_path: Path) -> None:
    frame = pd.DataFrame(
        {
            "first": pd.Series([None, "v"], dtype=object),
            "second": ["x", "y"],
        }
    )
    factory = FakeReaderFactory({"mars": frame})
    connection = PortableOracleConnection(reader_factory=factory)
    connection.openConnection("", "", "KM.MARS", None)
    output = tmp_path / "characterization.tab"

    assert connection.execute("select first", OutExcel=str(output), FirstConnect=True) == 2
    assert connection.execute("select second", OutExcel=str(output), FirstConnect=False) == 2

    raw = output.read_bytes()
    assert not raw.startswith(codecs.BOM_UTF8)
    assert raw.decode("utf-8").splitlines() == [
        "first\tsecond",
        "\tx",
        "v\ty",
        "\tx",
        "v\ty",
    ]


def test_portable_connection_rejects_unmapped_oracle_family() -> None:
    connection = PortableOracleConnection(reader_factory=FakeReaderFactory({}))
    with pytest.raises(UnsupportedQueryBackend, match="KM.UNKNOWN"):
        connection.openConnection("", "", "KM.UNKNOWN", None)


def test_cursor_labels_win_over_headers_for_nonempty_results(tmp_path: Path) -> None:
    """The original driver's 2.0.0.6 contract ignores MyHeaders when rows exist."""
    factory = FakeReaderFactory({"mars": pd.DataFrame({"ACTUAL_LABEL": ["é中", None, "."]})})
    connection = PortableOracleConnection(reader_factory=factory)
    connection.openConnection(None, None, "KM.MARS")
    output = tmp_path / "labels.tab"
    for first in (True, False):
        assert (
            connection.execute(
                "select", OutExcel=str(output), FirstConnect=first, MyHeaders="DIFFERENT_HEADER"
            )
            == 3
        )
    raw = output.read_bytes()
    assert not raw.startswith(codecs.BOM_UTF8)
    assert raw.decode("utf-8").splitlines() == ["ACTUAL_LABEL", "é中", '""', ".", "é中", '""', "."]


def test_empty_append_and_noheaders_do_not_add_headers(tmp_path: Path) -> None:
    factory = FakeReaderFactory({"mars": pd.DataFrame({"ACTUAL": [1]})})
    connection = PortableOracleConnection(reader_factory=factory)
    connection.openConnection(None, None, "KM.MARS")
    output = tmp_path / "noheaders.tab"
    connection.execute("select", OutExcel=str(output), ll_NoHdrs=True, MyHeaders="OTHER")
    assert output.read_text() == "1\n"
    connection._reader.frame = pd.DataFrame(columns=["ACTUAL"])
    assert connection.execute("select", OutExcel=str(output), FirstConnect=False) == 0
    assert output.read_text() == "1\n"


def test_original_oasys_preprocessing_occurs_before_fake_transport(tmp_path: Path) -> None:
    factory = FakeReaderFactory({"oasys": pd.DataFrame({"x": [1]})})
    text = block(
        "/NODE=KM.OASYS",
        "/UN=//",
        "/PW=",
        "/OLEDB=SQLPlus",
        "/ENGINE=VA",
        "/WORKDIR=.\\",
        "/T=",
        "/CSV=oasys.tab",
        "/HEADERS=x",
        body="/*BEGIN SQL*/ SELECT * FROM @OASYSSCHEMA@T /*END SQL*/",
    )

    with use_reader_factory(factory):
        assert PortableScriptHostRuntime().run_text(text, tmp_path)

    assert factory.requested == [("oasys", "KM.OASYS")]
    assert len(factory.calls) == 1
    assert "@OASYSSCHEMA@" not in factory.calls[0].query
    assert "FROM T" in factory.calls[0].query
    assert (tmp_path / "oasys.tab").read_text(encoding="utf-8") == "x\n1\n"


def test_real_22844_mars_aries_and_sqlite_progress_through_original_lifecycle(
    tmp_path: Path,
) -> None:
    mars, aries, sqlite = _real_22844_segments()
    factory = FakeReaderFactory({"mars": _mars_frame(), "aries": _aries_frame()})
    runtime = PortableScriptHostRuntime()

    with use_reader_factory(factory):
        assert runtime.run_text(mars, tmp_path)
        assert runtime.run_text(aries, tmp_path)
        assert runtime.run_text(sqlite, tmp_path)

    assert factory.requested == [
        ("mars", "KM.[A15_PROD_21.].MARS"),
        ("aries", "KM.ARIES"),
    ]
    mars_call, aries_call = factory.calls
    assert mars_call.site == "KM"
    assert "@[]@" not in mars_call.query
    assert "A15_PROD_21.F_LotHist" in mars_call.query
    assert "SQL_Get_CSV_List" not in aries_call.query
    assert "LOT_A" in aries_call.query

    mars_output = tmp_path / "yeuchuan_a0_22844.tab"
    aries_output = tmp_path / "yeuchuan_a1_22844.tab"
    sqlite_output = tmp_path / "XRAY_results.csv"
    assert mars_output.exists()
    assert aries_output.exists()
    assert sqlite_output.exists()
    assert mars_output.read_text(encoding="utf-8").splitlines()[0].split("\t") == list(
        _mars_frame().columns
    )
    assert aries_output.read_text(encoding="utf-8").splitlines()[0].split("\t") == list(
        _aries_frame().columns
    )
    mars_result = pd.read_csv(mars_output, sep="\t")
    aries_result = pd.read_csv(aries_output, sep="\t")
    assert mars_result.iloc[0]["lot_"] == "LOT_A"
    assert aries_result.iloc[0]["lot"] == "LOT_A"
    assert aries_result.iloc[0]["visual_id"] == "VID_A"

    result = pd.read_csv(sqlite_output)
    assert list(result.columns) == [
        "FACILITY",
        "SITE_WORK_WEEK",
        "PRODGROUP3",
        "LOT",
        "OWNER",
        "OUT_DATE",
        "VISUAL_ID",
        "IMAGE_NAME",
        "IMAGE_FULL_PATH_RAW",
        "RANK_",
        "MAXRANK_",
        "FILTER_",
        "IMAGE_FULL_PATH",
        "NUMERIC_VALUE_MAX",
    ]


def test_original_empty_query_result_uses_original_header_fallback(tmp_path: Path) -> None:
    factory = FakeReaderFactory({"mars": pd.DataFrame(columns=["a", "b"])})
    text = block(
        "/NODE=KM.MARS",
        "/UN=//",
        "/PW=",
        "/OLEDB=SQLPlus",
        "/ENGINE=VA",
        "/WORKDIR=.\\",
        "/T=",
        "/CSV=empty.tab",
        "/HEADERS=a,b",
        body="/*BEGIN SQL*/ SELECT a,b FROM dual WHERE 1=0 /*END SQL*/",
    )

    with use_reader_factory(factory):
        assert PortableScriptHostRuntime().run_text(text, tmp_path)

    assert (tmp_path / "empty.tab").read_text(encoding="utf-8") == "A\tB"
