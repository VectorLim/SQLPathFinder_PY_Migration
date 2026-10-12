from __future__ import annotations

import sys

from vg2c.runtime.mail import send_mail
from vg2c.utilities.mail import MailService


def test_disabled_email_does_not_import_datasyncx_or_read_files(monkeypatch, tmp_path):
    monkeypatch.setitem(sys.modules, "datasyncx", None)
    service = MailService()
    service.send(
        to="nobody@example.invalid",
        subject="disabled",
        body="disabled",
        attachments=["missing.png"],
        enabled=False,
    )
    send_mail(
        "nobody@example.invalid",
        "disabled",
        "body.htm",
        ["missing.png"],
        enabled=False,
        workdir=tmp_path / "missing",
    )
