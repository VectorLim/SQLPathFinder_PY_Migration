"""AED manufacturing operations use inert SDK-shaped responses only."""

import csv
import json
import os
from types import SimpleNamespace
from urllib.parse import unquote

import pytest

from vg2c.runtime.aed_client import AEDClient, _read_api_key, csr_requests
from vg2c.runtime.job import JobRuntime


class Service:
    def __init__(self, result=None):
        self.calls = []
        self.value = None
        self.result = result

    def call(self, method, *args):
        self.calls.append((method, *args))
        if method == "lot_set_attr":
            self.value = unquote(args[2])
        payload = self.result or {"Success": True, "Result": {
            "Attributes": [{"ID": "1064", "Value": self.value}]}}
        return SimpleNamespace(raise_for_status=lambda: None, json=lambda: payload)

    def __getattr__(self, method):
        return lambda *args: self.call(method, *args)


def client(service, mode="prod"):
    return AEDClient("A15", mode=mode, service_factory=lambda facility: service)


def test_ordinary_calls_preserve_order_arguments_and_encode_comments():
    service = Service()
    aed = client(service)
    aed.lot_hold("L1", "2303", "NCO", "note & CCB #1 / é")
    aed.lot_release("L1", "2303", "released")
    assert aed.lot_status("L1")["Success"] is True
    assert [call[0] for call in service.calls] == ["lot_hold", "lot_release", "lot_status"]
    assert service.calls[0][1:4] == ("L1", "2303", "NCO")
    assert service.calls[0][4] == "note%20%26%20CCB%20%231%20%2F%20%C3%A9"


def test_default_write_methods_do_not_load_sdk_or_authenticate(monkeypatch):
    monkeypatch.delenv("ENV_MODE", raising=False)
    aed = AEDClient("A15", service_factory=lambda facility: pytest.fail("no manufacturing request"))
    assert aed.lot_hold("L1", "2303", "NCO", "note")["DryRun"]
    assert aed.lot_release("L1", "2303", "note")["DryRun"]
    assert aed.lot_set_attr("L1", "1064", "Y")["DryRun"]
    assert aed.update_lot_attributes("L1", ("1064",), "Y", overwrite=True) == "N"


@pytest.mark.parametrize("value", [None, "", ".", "..", "bad/lot", "bad\nlot"])
def test_invalid_lots_fail_before_service(value):
    aed = AEDClient("A15", mode="prod", service_factory=lambda facility: pytest.fail("invalid input"))
    with pytest.raises(ValueError):
        aed.lot_status(value)


def test_missing_hold_parameters_fail_even_in_test_mode():
    with pytest.raises(ValueError, match="hold operation"):
        AEDClient("A15").lot_hold("L1", None, None, "note")


@pytest.mark.parametrize("payload", [{"Success": False}, [], {"Result": {}}])
def test_unsuccessful_business_responses_fail(payload):
    service = Service()
    service.call = lambda *args: SimpleNamespace(raise_for_status=lambda: None, json=lambda: payload)
    with pytest.raises(RuntimeError, match="unsuccessful or malformed"):
        client(service).lot_status("L1")


def test_http_errors_are_sanitized():
    service = Service()
    def failure():
        raise OSError("do-not-log-response-or-key")
    service.call = lambda *args: SimpleNamespace(raise_for_status=failure)
    with pytest.raises(RuntimeError, match="request failed") as error:
        client(service).lot_status("L1")
    assert "do-not-log" not in str(error.value)


def test_update_reuses_updater_overwrite_and_already_correct():
    service = Service()
    service.value = "existing-value"
    aed = client(service)
    assert aed.update_lot_attributes("L1", ("1064",), "Y") == "N"
    assert [call[0] for call in service.calls] == ["lot_status"]
    assert aed.update_lot_attributes("L1", ("1064",), "Y", overwrite=True) == "Y"
    assert service.calls[-1] == ("lot_set_attr", "L1", "1064", "Y")
    service.calls.clear()
    assert aed.update_lot_attributes("L1", ("1064",), "Y", overwrite=True) == "Y"
    assert [call[0] for call in service.calls] == ["lot_status"]


@pytest.mark.parametrize("result,write", [(None, False), ({"Attributes": []}, True),
                                        ({"Attributes": None}, True)])
def test_missing_lot_differs_from_existing_lot_without_attributes(result, write):
    service = Service({"Success": True, "Result": result})
    client(service).update_lot_attributes("L1", ("1064",), "Y", overwrite=True)
    assert any(call[0] == "lot_set_attr" for call in service.calls) is write


def test_credentials_use_existing_key_and_reject_partial_pair(monkeypatch):
    monkeypatch.setenv("X_API_KEY", "synthetic-test-only")
    monkeypatch.setenv("API_KEY", "invalid")
    monkeypatch.delenv("API_CONFIG", raising=False)
    _read_api_key()
    monkeypatch.delenv("X_API_KEY")
    with pytest.raises(ValueError, match="both contain"):
        _read_api_key()


def test_sdk_facility_context_is_reloaded_and_host_arguments_restored(tmp_path, monkeypatch):
    import sys
    from vg2c.runtime import aed_client

    ca = tmp_path / "ca.crt"
    ca.write_text("synthetic-ca")
    monkeypatch.setenv("REQUESTS_CA_BUNDLE", str(ca))
    monkeypatch.setenv("X_API_KEY", "synthetic-key")
    contexts = []
    service = Service()
    sdk = SimpleNamespace(load_context=lambda value: contexts.append(json.loads(value)),
                          ManufacturingService=lambda: service)
    arguments = ["pytest", "--unknown-sdk-argument"]
    monkeypatch.setattr(sys, "argv", arguments)
    def version(name):
        if name == "aed-client-winauth":
            raise aed_client.metadata.PackageNotFoundError(name)
        return "6.2.0"
    def load(name):
        assert sys.argv == ["pytest"]
        return sdk
    monkeypatch.setattr(aed_client.metadata, "version", version)
    monkeypatch.setattr(aed_client.importlib, "import_module", load)
    first, second = AEDClient("A15", mode="prod"), AEDClient("A12", mode="prod")
    first.lot_status("L1")
    second.lot_status("L2")
    first.lot_status("L3")
    assert [context["properties"][1]["value"] for context in contexts] == ["A15", "A12", "A15"]
    assert sys.argv is arguments


def test_missing_ca_and_unavailable_sdk_fail_without_requests(tmp_path, monkeypatch):
    from vg2c.runtime import aed_client
    monkeypatch.delenv("REQUESTS_CA_BUNDLE", raising=False)
    with pytest.raises(ValueError, match="CA bundle"):
        AEDClient("A15").lot_status("L1")
    def missing(name):
        raise aed_client.metadata.PackageNotFoundError(name)
    monkeypatch.setattr(aed_client.metadata, "version", missing)
    with pytest.raises(RuntimeError, match="approved.*wheel"):
        aed_client._load_sdk()


def test_encrypted_environment_and_external_files_use_synthetic_credentials(tmp_path, monkeypatch):
    Fernet = pytest.importorskip("cryptography.fernet").Fernet
    key = Fernet.generate_key()
    encrypted = Fernet(key).encrypt(b"synthetic-test-key")
    monkeypatch.delenv("X_API_KEY", raising=False)
    monkeypatch.setenv("API_KEY", key.decode())
    monkeypatch.setenv("API_CONFIG", encrypted.decode())
    _read_api_key()
    assert os.environ["X_API_KEY"] == "synthetic-test-key"
    monkeypatch.delenv("X_API_KEY")
    monkeypatch.delenv("API_KEY")
    monkeypatch.delenv("API_CONFIG")
    (tmp_path / "key.key").write_bytes(key)
    (tmp_path / "config_new.json").write_bytes(encrypted)
    monkeypatch.setenv("AED_SECRETS_DIR", str(tmp_path))
    _read_api_key()
    assert os.environ["X_API_KEY"] == "synthetic-test-key"


HEADER = "HOLD_LOT,AUTO_CONTAIN,LOT,EMAIL,CCB_NUMBER,HOLD_NOTE,HOLDCATEGORY,Module\n"


def test_csr_payload_validation_deduplication_and_ccb(tmp_path):
    row = "L1,Y,L1,recipient@example.test,42,investigate,NCO,IAM\n"
    (tmp_path / "requests.csv").write_text(HEADER + row + row)
    requests = csr_requests("requests.csv", workdir=tmp_path)
    assert len(requests) == 1
    assert requests[0]["COMMENT"] == "investigate\nCCB: 42"
    (tmp_path / "requests.csv").write_text(HEADER + row + row.replace("investigate", "other"))
    with pytest.raises(ValueError, match="Conflicting"):
        csr_requests("requests.csv", workdir=tmp_path)


def test_csr_outcomes_survive_notification_failure_and_scope_reset(tmp_path, monkeypatch):
    job = JobRuntime(tmp_path, tmp_path / "output")
    request = {"HOLD_LOT": "L1", "CCB_NUMBER": "42", "EMAIL": "recipient@example.test"}
    job.record_aed_result(request, facility="A15", response={"Success": True})
    job.record_aed_result(request, facility="A15", error=RuntimeError("private"))
    monkeypatch.setattr(job, "send_mail", lambda **kwargs: (_ for _ in ()).throw(OSError("mail failed")))
    job.csr_failure_mail([request])
    with (job.workdir / "AED_results.csv").open() as stream:
        rows = list(csv.DictReader(stream))
    assert [row["STATUS"] for row in rows] == ["accepted", "failed"]
    assert "private" not in (job.workdir / "AED_results.csv").read_text()
    job.begin_csr()
    assert job.csr_status == "No CSR AED action was executed"
