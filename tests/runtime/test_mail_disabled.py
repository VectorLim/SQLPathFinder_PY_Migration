from __future__ import annotations

from vg2c.utilities.mail import MailService


def test_disabled_email_does_not_load_credentials_or_connect(monkeypatch):
    def fail_credentials():
        raise AssertionError("disabled email must not load credentials")

    monkeypatch.setattr(MailService, "_load_credential", staticmethod(fail_credentials))
    service = MailService()
    service.send(
        to="nobody@example.invalid",
        subject="disabled",
        body="disabled",
        attachments=["missing.png"],
        enabled=False,
    )
