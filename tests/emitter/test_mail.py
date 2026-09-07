from __future__ import annotations

import smtplib
from email.message import EmailMessage

import pytest

from vg2c.utilities.mail import MailService


class _FakeSMTP:
    calls: list[tuple[str, int]] = []
    sent_messages: list[EmailMessage] = []
    starttls_calls: int = 0
    ehlo_calls: int = 0
    login_calls: list[tuple[str, str]] = []
    send_error: Exception | None = None

    def __init__(self, host: str, port: int) -> None:
        self.host = host
        self.port = port
        type(self).calls.append((host, port))

    def __enter__(self) -> _FakeSMTP:
        return self

    def __exit__(self, exc_type, exc, tb) -> None:
        return None

    def send_message(self, message: EmailMessage) -> None:
        if type(self).send_error is not None:
            raise type(self).send_error
        type(self).sent_messages.append(message)

    def ehlo(self) -> None:
        type(self).ehlo_calls += 1

    def starttls(self) -> None:
        type(self).starttls_calls += 1

    def login(self, user: str, password: str) -> None:
        type(self).login_calls.append((user, password))


class FakeCredential:
    def __init__(self, username: str, password: str) -> None:
        self.username = username
        self.password = password


@pytest.fixture(autouse=True)
def mock_keyring(monkeypatch) -> None:
    monkeypatch.setattr(
        "vg2c.utilities.mail.keyring.get_credential",
        lambda service, username: FakeCredential("yeu.chuan.lim@intel.com", "svc-pass"),
    )


def test_mail_service_uses_fixed_smtp_target(monkeypatch) -> None:
    _FakeSMTP.calls = []
    _FakeSMTP.sent_messages = []
    _FakeSMTP.starttls_calls = 0
    _FakeSMTP.ehlo_calls = 0
    _FakeSMTP.login_calls = []
    _FakeSMTP.send_error = None

    monkeypatch.setattr("vg2c.utilities.mail.smtplib.SMTP", _FakeSMTP)

    MailService().send(to="user@example.com", subject="Hello", body="Body")

    assert _FakeSMTP.calls == [("smtpauth.intel.com", 587)]
    assert len(_FakeSMTP.sent_messages) == 1
    assert _FakeSMTP.starttls_calls == 1
    assert _FakeSMTP.ehlo_calls == 2
    assert _FakeSMTP.login_calls == [("yeu.chuan.lim@intel.com", "svc-pass")]
    assert _FakeSMTP.sent_messages[0]["From"] == "yeu.chuan.lim@intel.com"
    assert _FakeSMTP.sent_messages[0]["To"] == "user@example.com"


def test_mail_service_keeps_outlook_host_and_port_fixed(monkeypatch) -> None:
    _FakeSMTP.calls = []
    _FakeSMTP.sent_messages = []
    _FakeSMTP.starttls_calls = 0
    _FakeSMTP.ehlo_calls = 0
    _FakeSMTP.login_calls = []
    _FakeSMTP.send_error = None

    monkeypatch.setenv("VG2C_SMTP_HOST", "smtp.internal.local")
    monkeypatch.setenv("VG2C_SMTP_PORT", "2525")
    monkeypatch.setattr("vg2c.utilities.mail.smtplib.SMTP", _FakeSMTP)

    MailService().send(to="user@example.com", subject="Hello", body="Body")

    assert _FakeSMTP.calls == [("smtpauth.intel.com", 587)]
    assert len(_FakeSMTP.sent_messages) == 1
    assert _FakeSMTP.starttls_calls == 1
    assert _FakeSMTP.ehlo_calls == 2


def test_mail_service_logs_in_when_smtp_credentials_set(monkeypatch) -> None:
    _FakeSMTP.calls = []
    _FakeSMTP.sent_messages = []
    _FakeSMTP.starttls_calls = 0
    _FakeSMTP.ehlo_calls = 0
    _FakeSMTP.login_calls = []
    _FakeSMTP.send_error = None

    monkeypatch.setattr("vg2c.utilities.mail.smtplib.SMTP", _FakeSMTP)

    MailService().send(to="user@example.com", subject="Hello", body="Body")

    assert _FakeSMTP.login_calls == [("yeu.chuan.lim@intel.com", "svc-pass")]
    assert _FakeSMTP.sent_messages[0]["From"] == "yeu.chuan.lim@intel.com"


def test_mail_service_auth_required_error_is_actionable(monkeypatch) -> None:
    _FakeSMTP.calls = []
    _FakeSMTP.sent_messages = []
    _FakeSMTP.starttls_calls = 0
    _FakeSMTP.ehlo_calls = 0
    _FakeSMTP.login_calls = []
    _FakeSMTP.send_error = smtplib.SMTPAuthenticationError(
        530, "Authentication required"
    )

    monkeypatch.setattr("vg2c.utilities.mail.smtplib.SMTP", _FakeSMTP)

    with pytest.raises(RuntimeError, match="SMTP authentication failed") as exc_info:
        MailService().send(to="user@example.com", subject="Hello", body="Body")
    assert "SMTP" in str(exc_info.value)
