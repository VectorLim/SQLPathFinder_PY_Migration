"""AED manufacturing operations use inert AEDAPIClient-shaped fakes only."""

import json
import os
from pathlib import Path
import ssl
import sys
from types import SimpleNamespace

import pytest

from vg2c.runtime import aed_client
from vg2c.runtime.aed_client import AEDClient, _call_environment, _scoped_environ


class Service:
    def __init__(self):
        self.calls = []

    def call(self, method, *args):
        self.calls.append((method, *args))
        return {"Success": True}

    def __getattr__(self, method):
        return lambda *args: self.call(method, *args)


def client(service):
    aed = AEDClient("A15")
    aed._client_factory = lambda facility: service
    return aed


def test_connection_is_read_only():
    service = Service()
    aed = client(service)
    aed.lot_status("L1")
    assert aed.test_connection("L1") is True
    assert service.calls == [("lot_status", "L1"), ("lot_status", "L1")]
    assert not hasattr(aed, "accepted")


@pytest.mark.parametrize("payload", [{"Success": False}, [], {"Result": {}}])
def test_connection_rejects_unsuccessful_or_malformed_response(payload):
    service = Service()
    service.call = lambda *args: payload
    aed = client(service)
    with pytest.raises(RuntimeError, match="unsuccessful or malformed"):
        aed.test_connection("L1")


def test_connection_rejects_invalid_lot_and_sanitizes_request_errors():
    service = Service()
    aed = client(service)
    with pytest.raises(ValueError):
        aed.test_connection("bad/lot")
    assert service.calls == []

    def failure(*args):
        raise OSError("do-not-log-response-or-key")

    service.call = failure
    with pytest.raises(RuntimeError, match="request failed") as error:
        aed.test_connection("L1")
    assert "do-not-log" not in str(error.value)


def test_ordinary_calls_preserve_order_arguments_and_encode_comments():
    service = Service()
    aed = client(service)
    aed.lot_hold("L1", "2303", "NCO", "note & CCB #1 / é")
    aed.lot_release("L1", "2303", "released")
    assert aed.lot_status("L1")["Success"] is True
    assert [call[0] for call in service.calls] == [
        "lot_hold",
        "lot_release",
        "lot_status",
    ]
    assert service.calls[0][1:4] == ("L1", "2303", "NCO")
    assert service.calls[0][4] == "note%20%26%20CCB%20%231%20%2F%20%C3%A9"


@pytest.mark.parametrize("value", [None, "", ".", "..", "bad/lot", "bad\nlot"])
def test_invalid_lots_fail_before_service(value):
    aed = AEDClient("A15")
    aed._client_factory = lambda facility: pytest.fail("invalid input")
    with pytest.raises(ValueError):
        aed.lot_status(value)


def test_missing_hold_parameters_fail_even_in_test_mode():
    with pytest.raises(ValueError, match="lot_hold argument"):
        AEDClient("A15").lot_hold("L1", None, None, "note")


@pytest.mark.parametrize("payload", [{"Success": False}, [], {"Result": {}}])
def test_unsuccessful_business_responses_fail(payload):
    service = Service()
    service.call = lambda *args: payload
    with pytest.raises(RuntimeError, match="unsuccessful or malformed"):
        client(service).lot_status("L1")


def test_http_errors_are_sanitized():
    service = Service()

    def failure(*args):
        raise OSError("do-not-log-response-or-key")

    service.call = failure
    with pytest.raises(RuntimeError, match="request failed") as error:
        client(service).lot_status("L1")
    assert "do-not-log" not in str(error.value)


@pytest.fixture
def upstream(tmp_path, monkeypatch):
    """Real aed_api over a fake SDK module; records contexts, requests and keys."""
    pytest.importorskip("cryptography.fernet")
    ca = tmp_path / "ca.crt"
    ca.write_text("synthetic-ca")
    record = SimpleNamespace(ca=str(ca), argv=[], contexts=[], requests=[], keys=[])
    response = SimpleNamespace(
        raise_for_status=lambda: None, json=lambda: {"Success": True}
    )

    def lot_status(*args):
        record.requests.append(args)
        record.keys.append(os.environ.get("X_API_KEY"))
        return response

    class SDK:
        ManufacturingService = staticmethod(
            lambda: SimpleNamespace(lot_status=lot_status)
        )

        @property
        def aed_client(self):
            record.argv.append(list(sys.argv))
            return SimpleNamespace(
                load_context=lambda value: record.contexts.append(json.loads(value))
            )

    def version(name):
        if name == "aed-client-winauth":
            raise aed_client.metadata.PackageNotFoundError(name)
        return "6.2.0"

    monkeypatch.setitem(sys.modules, "aed_client", SDK())
    monkeypatch.delitem(sys.modules, "vg2c.runtime.aed_api", raising=False)
    monkeypatch.setattr(aed_client.metadata, "version", version)
    monkeypatch.delenv("X_API_KEY", raising=False)
    return record


def test_api_key_session_has_no_integrated_auth(monkeypatch):
    monkeypatch.setattr(sys, "argv", ["pytest"])
    authentication = pytest.importorskip("aed_client.sessions.apikey.Authentication")
    monkeypatch.setenv("X_API_KEY", "synthetic-key")
    session = authentication.Authentication().create_session("application/json", None)
    assert session.headers["X-API-Key"] == "synthetic-key"
    assert session.auth is None
    assert session.verify


def test_connection_uses_encrypted_key_without_sso_or_network(monkeypatch):
    monkeypatch.setattr(sys, "argv", ["pytest"])
    pytest.importorskip("aed_client.sessions.apikey.Authentication")
    fernet = pytest.importorskip("cryptography.fernet")
    import requests

    key = fernet.Fernet.generate_key()
    token = fernet.Fernet(key).encrypt(b"synthetic-encrypted-key")
    observed = []

    def send(session, request, **kwargs):
        assert request.method == "GET"
        assert request.headers["X-API-Key"] == "synthetic-encrypted-key"
        assert session.auth is None
        assert Path(kwargs["verify"]).is_file()
        assert not kwargs.get("proxies")
        observed.append(request.path_url)
        response = requests.Response()
        response.status_code = 200
        response._content = b'{"Success": true}'
        return response

    monkeypatch.setattr(requests.Session, "send", send)
    aed = AEDClient(
        "A15", environ={"API_KEY": key.decode(), "API_CONFIG": token.decode()}
    )
    assert aed.test_connection("L1") is True
    assert observed == ["/api/latest/manufacturing/lots/L1"]


def test_upstream_reloads_facility_context_and_host_arguments_restored(
    upstream, monkeypatch
):
    arguments = ["pytest", "--unknown-sdk-argument"]
    monkeypatch.setattr(sys, "argv", arguments)
    environ = {"X_API_KEY": "synthetic-key", "REQUESTS_CA_BUNDLE": upstream.ca}
    first = AEDClient("A15", environ=environ)
    second = AEDClient("A12", environ=environ)
    first.lot_status("L1")
    second.lot_status("L2")
    first.lot_status("L3")
    assert [c["properties"][1]["value"] for c in upstream.contexts] == [
        "A15",
        "A12",
        "A15",
    ]
    assert upstream.requests == [("L1",), ("L2",), ("L3",)]
    assert upstream.argv == [["pytest"]]
    assert sys.argv is arguments


@pytest.mark.parametrize("environ", [{}, {"REQUESTS_CA_BUNDLE": ""}])
def test_bundled_ca_is_used_and_environment_restored(monkeypatch, environ):
    monkeypatch.delenv("REQUESTS_CA_BUNDLE", raising=False)
    with _scoped_environ(_call_environment(environ)):
        ca = Path(os.environ["REQUESTS_CA_BUNDLE"])
        assert ca.is_absolute() and ca.is_file()
        ssl.create_default_context(cafile=str(ca))
    assert "REQUESTS_CA_BUNDLE" not in os.environ


def test_explicit_ca_overrides_bundle_and_is_restored(tmp_path, monkeypatch):
    monkeypatch.setenv("REQUESTS_CA_BUNDLE", "original-ca")
    configured = str(tmp_path / "configured-ca.crt")
    with _scoped_environ(_call_environment({"REQUESTS_CA_BUNDLE": configured})):
        assert os.environ["REQUESTS_CA_BUNDLE"] == configured
    assert os.environ["REQUESTS_CA_BUNDLE"] == "original-ca"


def test_invalid_ca_and_unavailable_sdk_fail_without_requests(tmp_path, monkeypatch):
    monkeypatch.setenv("REQUESTS_CA_BUNDLE", str(tmp_path / "missing.crt"))
    with pytest.raises(ValueError, match="CA bundle"):
        AEDClient("A15").lot_status("L1")

    def missing(name):
        raise aed_client.metadata.PackageNotFoundError(name)

    ca = tmp_path / "ca.crt"
    ca.write_text("synthetic-ca")
    monkeypatch.setattr(aed_client.metadata, "version", missing)
    with pytest.raises(RuntimeError, match="approved.*wheel"):
        AEDClient("A15", environ={"REQUESTS_CA_BUNDLE": str(ca)}).lot_status("L1")


def test_encrypted_environment_and_external_files_use_synthetic_credentials(
    upstream, tmp_path
):
    from cryptography.fernet import Fernet

    key = Fernet.generate_key()
    encrypted = Fernet(key).encrypt(b"synthetic-test-key")
    pair = {"API_KEY": key.decode(), "API_CONFIG": encrypted.decode()}
    AEDClient("A15", environ={**pair, "REQUESTS_CA_BUNDLE": upstream.ca}).lot_status(
        "L1"
    )

    (tmp_path / "key.key").write_bytes(key)
    (tmp_path / "config_new.json").write_bytes(encrypted)
    files = {"AED_SECRETS_DIR": str(tmp_path), "REQUESTS_CA_BUNDLE": upstream.ca}
    AEDClient("A15", environ=files).lot_status("L2")
    assert upstream.keys == ["synthetic-test-key", "synthetic-test-key"]
    assert "X_API_KEY" not in os.environ


@pytest.mark.parametrize(
    "credentials,message",
    [
        ({}, "Supply X_API_KEY"),
        ({"API_KEY": "partial"}, "both contain"),
        ({"AED_SECRETS_DIR": "relative"}, "absolute"),
    ],
)
def test_missing_or_partial_credentials_fail(upstream, credentials, message):
    aed = AEDClient("A15", environ={**credentials, "REQUESTS_CA_BUNDLE": upstream.ca})
    with pytest.raises(ValueError, match=message):
        aed.lot_status("L1")
    assert upstream.requests == []


@pytest.mark.parametrize("fail", [False, True])
@pytest.mark.parametrize("proxy_name", ["NO_PROXY", "no_proxy"])
def test_per_job_credentials_and_proxy_are_restored_after_call(
    monkeypatch, fail, proxy_name
):
    monkeypatch.setenv("X_API_KEY", "synthetic-original")
    monkeypatch.setenv("NO_PROXY", "original-host")
    observed = []

    class ScopedService:
        def lot_hold(self, *args):
            observed.append(os.environ["X_API_KEY"])
            assert os.environ["NO_PROXY"] == "job-host"
            os.environ["NO_PROXY"] = "changed-by-sdk"
            if fail:
                raise OSError("private SDK context")
            return {"Success": True}

    aed = AEDClient(
        "A15",
        environ={
            "X_API_KEY": "synthetic-job",
            proxy_name: "job-host",
        },
    )
    aed._client_factory = lambda facility: ScopedService()
    if fail:
        with pytest.raises(RuntimeError, match="request failed"):
            aed.lot_hold("L1", "2303", "NCO", "note")
    else:
        aed.lot_hold("L1", "2303", "NCO", "note")
    assert observed == ["synthetic-job"]
    assert os.environ["X_API_KEY"] == "synthetic-original"
    assert os.environ["NO_PROXY"] == "original-host"
