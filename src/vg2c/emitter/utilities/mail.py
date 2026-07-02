"""MailService — send email via stdlib smtplib."""

from __future__ import annotations

import os
import smtplib
from email.message import EmailMessage
from pathlib import Path

from vg2c.emitter.codegen import FunctionDef
from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import register_utility


@register_utility
class MailService(UtilitySpec):
    """Send email. Reads connection config from environment variables."""

    utility_name = "mail"
    utility_imports = (
        "import os",
        "import smtplib",
        "from email.message import EmailMessage",
        "from pathlib import Path",
    )
    utility_command_contains = (("email", ("email", "sqlpathfinder_email")),)

    @classmethod
    def emit(
        cls,
        ctx,
        block,
        dispatched,
    ) -> tuple[str, str]:
        fdef = FunctionDef.from_body(
            FunctionDef.name_for(block, "utility"),
            ["pass  # TODO: utility shape not translated: email"],
        )
        return fdef.source, fdef.call_site

    def send(
        self,
        to: str,
        subject: str,
        body: str,
        attachments: list[str] | None = None,
        from_addr: str | None = None,
    ) -> None:
        host = os.environ.get("VG2C_SMTP_HOST", "")
        if not host:
            raise RuntimeError(
                "MailService: VG2C_SMTP_HOST is not set. "
                "Set the environment variable to your SMTP server hostname."
            )
        port = int(os.environ.get("VG2C_SMTP_PORT", "25"))
        sender = from_addr or os.environ.get("VG2C_FROM_ADDRESS", "vg2c@localhost")

        msg = EmailMessage()
        msg["Subject"] = subject
        msg["From"] = sender
        msg["To"] = to
        msg.set_content(body)

        for att_path in attachments or []:
            p = Path(att_path)
            if p.exists():
                msg.add_attachment(
                    p.read_bytes(),
                    maintype="application",
                    subtype="octet-stream",
                    filename=p.name,
                )

        with smtplib.SMTP(host, port) as smtp:
            smtp.send_message(msg)
