"""Exercise the original SPF worker, real updater, and share/AED boundaries without live writes."""

from __future__ import annotations

import csv
import json
import os
import shutil
import types
from pathlib import Path
from unittest.mock import Mock

import pytest

from scripthost_portable import aed_api as api
from scripthost_portable import migrate_aed
from scripthost_portable.worker import ScriptHostJob, _run_child

CONFIG = (
    'icmpcs,parameter,value,comment\nICMPCS,dEmail,"one@example.test;two@example.test",note\n'
    'ICMPCS,dSubject,"Risk, detected",note\nOTHER,ignored,unused,note\n'
)


def put(path, text):
    Path(path).parent.mkdir(parents=True, exist_ok=True)
    Path(path).write_text(text, encoding="utf-8")


def config_path(share, site):
    return share / api.IAM_JOB / site / "CONFIG" / "config.txt"


@pytest.fixture
def network(monkeypatch, tmp_path):
    share = tmp_path / "mounted_share"
    for name in ("SITE", "SFOLDER", "MARS", "ARIES", "dEmail", "DEMAIL", "HIST_PATH",
                 "ATTR_LIST", "SKIP_OPERATION", "AED_ENABLED", "AED_CONFIG_PATH",
                 "AED_FACILITY"):
        monkeypatch.delenv(name, raising=False)
    monkeypatch.setenv("SITE", "KM")
    monkeypatch.setenv("ICMPCS_ROOT", str(share))
    monkeypatch.setenv("ENV_MODE", "prod")
    monkeypatch.chdir(tmp_path)
    for site in ("KM", "VN"):
        put(config_path(share, site), CONFIG)
    client = types.ModuleType("aed_client")
    service = Mock()
    values = {}

    def status(lot):
        if lot == "READFAIL":
            raise RuntimeError("fake read failure")
        response = Mock()
        response.json.return_value = {
            "Success": True, "Result": {"Attributes": [
                {"ID": "1064", "Value": values.get((client.facility, lot))},
            ]},
        }
        return response

    def update(lot, attribute, value):
        response = Mock()
        if lot == "FAIL":
            response.json.return_value = {"Success": False}
        else:
            if lot != "UNVERIFIED":
                values[client.facility, lot] = value
            response.json.return_value = {"Success": True}
        return response

    service.lot_status.side_effect = status
    service.lot_set_attr.side_effect = update
    client.ManufacturingService = Mock(return_value=service)
    monkeypatch.setitem(__import__("sys").modules, "aed_client", client)
    monkeypatch.setattr(api, "_load_client", lambda workdir: client)
    # prepare_job exports are process-local in production; restore them in this in-process test.
    environment = dict(os.environ)
    yield share, service, values
    os.environ.clear()
    os.environ.update(environment)


def candidates(tmp_path, lots):
    path = tmp_path / "candidates.csv"
    with path.open("w", newline="") as stream:
        writer = csv.writer(stream)
        writer.writerow(["FACILITY", "LOT"])
        writer.writerows(lots)
    return str(path)


def prepare(tmp_path):
    api.prepare_job(tmp_path, '/UTILITIES={AED} "candidates.csv"')


def outcomes(tmp_path):
    with (tmp_path / "AED_results.csv").open() as stream:
        return list(csv.DictReader(stream))


def test_config_csv_json_environment_and_site_selection(network, tmp_path, monkeypatch):
    share, _, _ = network
    monkeypatch.setenv("MARS", "KM.MARS_CUSTOM")
    prepare(tmp_path)
    snapshot = json.loads((tmp_path / "config.json").read_text())
    with (tmp_path / "configsets.csv").open() as stream:
        assert next(csv.DictReader(stream)) == snapshot
    assert all(os.environ[key] == value for key, value in snapshot.items())
    assert snapshot["MARS"] == "KM.MARS_CUSTOM"
    assert snapshot["AED_FACILITY"] == "A15_PROD_21"
    assert snapshot["dSubject"] == "Risk, detected"
    assert "ignored" not in snapshot
    monkeypatch.delenv("MARS")
    monkeypatch.delenv("ARIES")
    monkeypatch.delenv("HIST_PATH")
    monkeypatch.delenv("AED_FACILITY")
    monkeypatch.setenv("SITE", "VN")
    prepare(tmp_path)
    assert os.environ["ARIES"] == "VN.ARIES"
    assert os.environ["MARS"] == "VN.[A90_PROD_21.].MARS"
    assert os.environ["AED_FACILITY"] == "A90_PROD_21"
    assert Path(os.environ["HIST_PATH"]).parts[-3:] == ("VN", "HIST", "HIST.txt")


def test_share_folder_follows_script_filename(network, tmp_path):
    script = '/UTILITIES={AED} "candidates.csv"'
    api.prepare_job(tmp_path, script, str(tmp_path / f"{api.IAM_JOB}.aed.txt"))
    assert os.environ["SFOLDER"] == api.IAM_JOB
    assert Path(os.environ["HIST_PATH"]).is_relative_to(tmp_path)
    del os.environ["SFOLDER"]
    with pytest.raises(FileNotFoundError):
        api.prepare_job(tmp_path, script, str(tmp_path / "OTHER_JOB.txt"))


@pytest.mark.parametrize("text", [
    "wrong,columns\n",
    "icmpcs,parameter,value\nICMPCS,dEmail,a\nICMPCS,dEmail,b\n",
    "icmpcs,parameter,value\nICMPCS,PATH,dangerous\n",
    "icmpcs,parameter,value\nICMPCS,X_API_KEY,secret\n",
])
def test_invalid_configuration_is_rejected(text):
    with pytest.raises(ValueError):
        api.parse_config(text)


def test_prepare_failure_stops_spf_before_output(network, tmp_path):
    share, service, _ = network
    shutil.rmtree(share)
    script = '<OPTIONS>\n/UTILITIES={AED} "candidates.csv"\n</OPTIONS>'
    result = _run_child(ScriptHostJob(working_directory=str(tmp_path), script_text=script))
    assert not result.success
    assert "FileNotFoundError" in result.message
    assert not (tmp_path / "config.json").exists()
    service.lot_status.assert_not_called()


def test_updater_verification_history_and_partial_failure(network, tmp_path):
    share, service, values = network
    values["A15", "CORRECT"] = "2446"
    prepare(tmp_path)
    path = candidates(tmp_path, [
        ("A15", "NEW"), ("A15", "NEW"), ("A15", "CORRECT"), ("A15", "FAIL"),
    ])
    with pytest.raises(RuntimeError, match="One or more"):
        api.process_candidates(path)
    assert [row["STATUS"] for row in outcomes(tmp_path)] == [
        "updated", "already_correct", "failed",
    ]
    assert service.lot_set_attr.call_count == 2
    history = api._read_history(os.environ["HIST_PATH"])
    assert {row["LOT"] for row in history} == {"NEW", "CORRECT"}
    assert not list(share.rglob("*.tmp"))
    service.reset_mock()
    api.process_candidates(candidates(tmp_path, [("A15", "NEW")]))
    service.lot_status.assert_not_called()
    assert outcomes(tmp_path)[0]["STATUS"] == "history_excluded"


@pytest.mark.parametrize("lot", ["READFAIL", "UNVERIFIED"])
def test_unconfirmed_lots_never_enter_history(network, tmp_path, lot):
    share, _, _ = network
    prepare(tmp_path)
    with pytest.raises(RuntimeError):
        api.process_candidates(candidates(tmp_path, [("A15", lot)]))
    assert outcomes(tmp_path)[0]["STATUS"] == "failed"
    assert not Path(os.environ["HIST_PATH"]).exists()


def test_dry_run_never_writes_aed_or_history(network, tmp_path, monkeypatch):
    share, service, _ = network
    monkeypatch.setenv("ENV_MODE", "test")
    prepare(tmp_path)
    api.process_candidates(candidates(tmp_path, [("A15", "DRY")]))
    service.lot_set_attr.assert_not_called()
    assert not Path(os.environ["HIST_PATH"]).exists()
    assert outcomes(tmp_path)[0]["STATUS"] == "dry_run"


def test_legacy_history_and_invalid_candidates(network, tmp_path):
    share, service, _ = network
    prepare(tmp_path)
    put(os.environ["HIST_PATH"], "Lot_NCORisk,OUT_DATE\nOLD,2020-01-01\n")
    api.process_candidates(candidates(tmp_path, [("A15", "OLD")]))
    service.lot_status.assert_not_called()
    with pytest.raises(ValueError):
        api.process_candidates(candidates(tmp_path, [("A15", "URL&injection")]))
    service.lot_status.assert_not_called()


def test_original_worker_exports_before_parsing_and_routes_task(network, tmp_path):
    _, service, _ = network
    path = candidates(tmp_path, [("A15", "WORKER")])
    script = (
        '<OPTIONS>\n/WRITE-FILE=Y\n/CSV=site.txt\n</OPTIONS>\n<<<%SITE%>>>\n'
        '<---- New Query ---->\n<OPTIONS>\n/UTILITIES={AED} "' + path + '"\n</OPTIONS>\n'
    )
    result = _run_child(ScriptHostJob(working_directory=str(tmp_path), script_text=script))
    assert result.success, result.message + result.stdout + result.stderr
    assert (tmp_path / "site.txt").read_text().strip() == "KM"
    assert outcomes(tmp_path)[0]["STATUS"] == "updated"
    service.lot_set_attr.assert_called_once_with("WORKER", "1064", "2446")


def test_disabled_and_empty_candidates_make_no_requests(network, tmp_path, monkeypatch):
    share, service, _ = network
    monkeypatch.setenv("AED_ENABLED", "N")
    prepare(tmp_path)
    api.process_candidates(candidates(tmp_path, [("A15", "DISABLED")]))
    assert outcomes(tmp_path)[0]["STATUS"] == "disabled"
    api.process_candidates(candidates(tmp_path, []))
    assert outcomes(tmp_path) == []
    service.lot_status.assert_not_called()
    assert not Path(os.environ["HIST_PATH"]).exists()


def test_environment_can_supply_missing_required_config(network, tmp_path, monkeypatch):
    share, _, _ = network
    put(config_path(share, "KM"), "icmpcs,parameter,value\nICMPCS,dSubject,Risk\n")
    monkeypatch.setenv("dEmail", "override@example.test")
    prepare(tmp_path)
    assert json.loads((tmp_path / "config.json").read_text())["dEmail"] == "override@example.test"


def test_failed_history_replace_preserves_existing_history(network, tmp_path, monkeypatch):
    share, _, _ = network
    prepare(tmp_path)
    path = os.environ["HIST_PATH"]
    old = "FACILITY,LOT,OUT_DATE\nA15,OLD,2020-01-01\n"
    put(path, old)
    monkeypatch.setattr(api.os, "replace", Mock(side_effect=PermissionError))
    with pytest.raises(RuntimeError):
        api.process_candidates(candidates(tmp_path, [("A15", "NEW")]))
    assert Path(path).read_text(encoding="utf-8") == old
    assert not list(share.rglob("*.tmp"))
    assert outcomes(tmp_path)[0]["STATUS"] == "failed"


def test_unmapped_site_requires_configured_facility(network, tmp_path, monkeypatch):
    share, _, _ = network
    monkeypatch.setenv("SITE", "XX")
    put(config_path(share, "XX"), "icmpcs,parameter,value\nICMPCS,dEmail,a@example.test\n")
    with pytest.raises(ValueError, match="AED_FACILITY"):
        prepare(tmp_path)
    monkeypatch.setenv("AED_FACILITY", "X1_PROD_1")
    prepare(tmp_path)
    assert json.loads((tmp_path / "config.json").read_text())["AED_FACILITY"] == "X1_PROD_1"


def test_migrated_candidate_steps_run_in_original_worker(network, tmp_path):
    _, service, _ = network
    (tmp_path / "IPM_Data.csv").write_text("facility,lot\nA15,LOT1\nA15,LOT1\nA15,LOT2\n")
    producer = migrate_aed.parse(
        "\n<OPTIONS>\n/NODE=.\\\n/OLEDB=SQLite\n/ENGINE=SQLite\n/WORKDIR=.\\\n"
        "/CSV=DATA.csv\n/TABLE=IPM_Data.csv\n/HEADERS=Lot_NCORisk\n</OPTIONS>\n"
        "SELECT DISTINCT a0.[lot] AS [Lot_NCORisk]\nFROM [IPM_Data] a0\n"
    )
    steps = migrate_aed.signal_source(producer, {}, "\n")[0].raws
    script = migrate_aed.DELIM.join([
        '\n<OPTIONS>\n/UTILITIES={START-MACRO} "configsets.csv" "N"\n</OPTIONS>\n',
        *steps,
        "\n<OPTIONS>\n/UTILITIES={END-MACRO}\n</OPTIONS>\n",
    ])
    result = _run_child(ScriptHostJob(working_directory=str(tmp_path), script_text=script))
    assert result.success, result.message + result.stdout + result.stderr
    assert [(row["FACILITY"], row["LOT"], row["STATUS"]) for row in outcomes(tmp_path)] == [
        ("A15_PROD_21", "LOT1", "updated"), ("A15_PROD_21", "LOT2", "updated"),
    ]


@pytest.mark.parametrize("name", ["ICMPCS.txt", "tests/fixtures/aed_migration/CSR_IAM_v2.txt"])
def test_pilot_script_controller_tree(network, tmp_path, name):
    from scripthost_portable.runtime import _spf_manager_type

    root = Path(__file__).resolve().parents[2]
    path = root / name
    if not path.exists():
        path = Path("/app/jobs") / name
    script = migrate_aed.migrate(path.read_text(encoding="utf-8-sig")).text
    text = api.prepare_job(tmp_path, script)
    manager = _spf_manager_type()()
    manager.gCommandLineArguments = ["SPFSQL3.py", f'/MYLOCAL="{tmp_path}"', "/EXECMODE=UT"]
    blocks = text.split("<---- New Query ---->")
    tasks = manager.Process_Query(0, len(blocks), blocks, str(tmp_path), "", None,
                                  len(blocks), "test", "tmp", None)

    def flatten(items):
        for task in items:
            yield task
            yield from flatten(getattr(task, "childTasksList", []))

    names = [type(task).__name__ for task in flatten(tasks)]
    assert names.count("AEDTask") == 1
    assert "StartMacroTask" in names
