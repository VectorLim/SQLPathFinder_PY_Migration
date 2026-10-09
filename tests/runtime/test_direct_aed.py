"""Offline AED config/candidate/readback checks; service writes are inert fakes."""

import csv
import json
import os

import pytest

from vg2c.runtime.aed import IAM_JOB, bootstrap_aed, parse_config, process_candidates


def config(tmp_path, mode="test"):
    return {"ENV_MODE": mode, "AED_ENABLED": "Y", "ATTR_LIST": "1064",
            "SKIP_OPERATION": "2446", "HIST_PATH": str(tmp_path / "history.csv")}


class Service:
    def __init__(self, confirmed=True):
        self.calls = []
        self.changed = False
        self.confirmed = confirmed

    def attributes(self, lot):
        self.calls.append(("read", lot))
        return [{"ID": "1064", "Value": "2446"}] if self.changed and self.confirmed else [{"ID": "1064", "Value": None}]

    def set_attribute(self, lot, attribute, target):
        self.calls.append(("write", lot, attribute, target))
        self.changed = True
        return True


def candidates(tmp_path):
    (tmp_path / "candidates.csv").write_text("FACILITY,LOT\nA12_PROD_0,L1\nA12_PROD_0,L1\n")


def test_test_mode_never_writes_or_records_success(tmp_path):
    candidates(tmp_path)
    service = Service()
    before = dict(os.environ)
    result = process_candidates("candidates.csv", config=config(tmp_path), workdir=tmp_path,
                                service_factory=lambda facility: service)
    assert result[0]["STATUS"] == "dry_run"
    assert service.calls == [("read", "L1"), ("read", "L1")]
    assert not (tmp_path / "history.csv").exists()
    assert dict(os.environ) == before


def test_prod_readback_history_and_repeat_exclusion(tmp_path):
    candidates(tmp_path)
    service = Service()
    factory = lambda facility: service
    result = process_candidates("candidates.csv", config=config(tmp_path, "prod"), workdir=tmp_path,
                                service_factory=factory)
    assert result[0]["STATUS"] == "updated"
    assert [item[0] for item in service.calls] == ["read", "write", "read"]
    with (tmp_path / "history.csv").open(newline="") as stream:
        assert len(list(csv.DictReader(stream))) == 1
    repeated = process_candidates("candidates.csv", config=config(tmp_path, "prod"), workdir=tmp_path,
                                  service_factory=lambda facility: pytest.fail("history must skip service"))
    assert repeated[0]["STATUS"] == "history_excluded"


def test_unconfirmed_write_never_records_history(tmp_path):
    candidates(tmp_path)
    with pytest.raises(RuntimeError, match="One or more"):
        process_candidates("candidates.csv", config=config(tmp_path, "prod"), workdir=tmp_path,
                           service_factory=lambda facility: Service(confirmed=False))
    assert not (tmp_path / "history.csv").exists()
    assert "RuntimeError" in (tmp_path / "AED_results.csv").read_text()


@pytest.mark.parametrize("before", [[], [{"ID": "1064", "Value": "keep-existing"}]])
def test_empty_or_nonempty_existing_value_is_not_overwritten(tmp_path, before):
    candidates(tmp_path)
    class NoWriteService:
        def attributes(self, lot):
            return before
        def set_attribute(self, *args):
            pytest.fail("original updater preserves nonempty values and empty lot records")
    result = process_candidates("candidates.csv", config=config(tmp_path, "prod"), workdir=tmp_path,
                                service_factory=lambda facility: NoWriteService())
    assert result[0]["STATUS"] == "skipped"
    assert not (tmp_path / "history.csv").exists()


def test_disabled_and_malformed_candidates_before_service(tmp_path):
    candidates(tmp_path)
    values = {**config(tmp_path), "AED_ENABLED": "N"}
    result = process_candidates("candidates.csv", config=values, workdir=tmp_path,
                                service_factory=lambda facility: pytest.fail("disabled"))
    assert result[0]["STATUS"] == "disabled"
    (tmp_path / "candidates.csv").write_text("LOT,FACILITY\nL1,A12_PROD_0\n")
    with pytest.raises(ValueError, match="exactly FACILITY,LOT"):
        process_candidates("candidates.csv", config=values, workdir=tmp_path, service_factory=None)


def test_bootstrap_precedence_aliases_fresh_state_and_no_secret_artifacts(tmp_path):
    environment = {"SITE": "pg", "SFOLDER": IAM_JOB, "ICMPCS_ROOT": str(tmp_path / "configs"),
                   "dEMAIL": "local@example.test", "X_API_KEY": "never-serialize"}
    original = environment.copy()
    text = "group,parameter,value\nICMPCS,dEmail,remote@example.test\nICMPCS,SITE,KM\n"
    values = bootstrap_aed(workdir=tmp_path / "job", environ=environment, read_text=lambda path: text)
    assert environment == original
    assert values["SITE"] == "PG"
    assert values["MARS"] == "PG.[A12_PROD_0.].MARS"
    assert values["DEMAIL"] == "local@example.test"
    assert values["ENV_MODE"] == "test"
    assert "never-serialize" not in (tmp_path / "job/config.json").read_text()
    assert "X_API_KEY" not in json.loads((tmp_path / "job/config.json").read_text())
    second = bootstrap_aed(workdir=tmp_path / "job2", environ={**environment, "SITE": "KM"}, read_text=lambda path: text)
    assert second["MARS"] == "KM.[A15_PROD_21.].MARS"
    assert values["SITE"] == "PG"


@pytest.mark.parametrize("text", ["group,parameter,value\nICMPCS,X_API_KEY,secret\n",
                                  "group,parameter,value\nICMPCS,foo,1\nICMPCS,FOO,2\n"])
def test_config_rejects_unsafe_or_ambiguous_parameters(text):
    with pytest.raises(ValueError):
        parse_config(text)
