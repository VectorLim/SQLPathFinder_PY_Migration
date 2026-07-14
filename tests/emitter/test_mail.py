from __future__ import annotations

from email.message import EmailMessage
import smtplib

from vg2c.emitter.utilities.mail import MailService


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


def test_mail_service_uses_fixed_smtp_target(monkeypatch) -> None:
    _FakeSMTP.calls = []
    _FakeSMTP.sent_messages = []
    _FakeSMTP.starttls_calls = 0
    _FakeSMTP.ehlo_calls = 0
    _FakeSMTP.login_calls = []
    _FakeSMTP.send_error = None

    monkeypatch.setattr("vg2c.emitter.utilities.mail.smtplib.SMTP", _FakeSMTP)

    MailService().send(to="user@example.com", subject="Hello", body="Body")

    assert _FakeSMTP.calls == [("smtp.office365.com", 587)]
    assert len(_FakeSMTP.sent_messages) == 1
    assert _FakeSMTP.starttls_calls == 1
    assert _FakeSMTP.ehlo_calls == 2
    assert _FakeSMTP.login_calls == []
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
    monkeypatch.setattr("vg2c.emitter.utilities.mail.smtplib.SMTP", _FakeSMTP)

    MailService().send(to="user@example.com", subject="Hello", body="Body")

    assert _FakeSMTP.calls == [("smtp.office365.com", 587)]
    assert len(_FakeSMTP.sent_messages) == 1
    assert _FakeSMTP.starttls_calls == 1
    assert _FakeSMTP.ehlo_calls == 2
    assert _FakeSMTP.login_calls == []


def test_mail_service_logs_in_when_smtp_credentials_set(monkeypatch) -> None:
    _FakeSMTP.calls = []
    _FakeSMTP.sent_messages = []
    _FakeSMTP.starttls_calls = 0
    _FakeSMTP.ehlo_calls = 0
    _FakeSMTP.login_calls = []
    _FakeSMTP.send_error = None

    monkeypatch.setenv("VG2C_SMTP_PASSWORD", "svc-pass")
    monkeypatch.setattr("vg2c.emitter.utilities.mail.smtplib.SMTP", _FakeSMTP)

    MailService().send(to="user@example.com", subject="Hello", body="Body")

    assert _FakeSMTP.login_calls == [("yeu.chuan.lim@intel.com", "svc-pass")]
    assert _FakeSMTP.sent_messages[0]["From"] == "yeu.chuan.lim@intel.com"


def test_mail_service_auth_required_error_is_actionable(monkeypatch) -> None:
    _FakeSMTP.calls = []
    _FakeSMTP.sent_messages = []
    _FakeSMTP.starttls_calls = 0
    _FakeSMTP.ehlo_calls = 0
    _FakeSMTP.login_calls = []
    _FakeSMTP.send_error = smtplib.SMTPResponseException(
        530, b"Authentication required"
    )

    monkeypatch.delenv("VG2C_SMTP_PASSWORD", raising=False)
    monkeypatch.setattr("vg2c.emitter.utilities.mail.smtplib.SMTP", _FakeSMTP)

    try:
        MailService().send(to="user@example.com", subject="Hello", body="Body")
        assert False, "Expected RuntimeError"
    except RuntimeError as exc:
        assert "requires authentication" in str(exc)
        assert "VG2C_SMTP_PASSWORD" in str(exc)
