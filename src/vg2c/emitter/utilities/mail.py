"""MailService - send email via stdlib smtplib."""

from __future__ import annotations

import os
import smtplib
from email.message import EmailMessage
from pathlib import Path
from typing import Any


from vg2c.emitter.models import EmitContext
from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._emit_helpers import (
    option_to_python_expr,
    split_utility_command,
    strip_quotes,
)


class MailService(UtilitySpec):
    """Send email. Reads connection config from environment variables."""

    utility_name = "email"

    @classmethod
    @EmitContext.step_emitter
    def emit_block(cls, block: Any) -> list[str] | None:
        argv = cls._utility_argv(block)
        if not cls._is_mail_utility(argv):
            return None

        stmt = cls._emit_send(argv, block.resolved_body)
        if stmt is None:
            return ["pass  # TODO: unsupported email utility command"]
        return [stmt]

    @staticmethod
    def _utility_argv(block: Any) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "")
        return split_utility_command(text)

    @staticmethod
    def _is_mail_utility(argv: list[str]) -> bool:
        if not argv:
            return False
        basename = strip_quotes(argv[0]).split("/")[-1].split("\\")[-1].lower()
        return "sqlpathfinder_email" in basename

    @staticmethod
    def _csv_items(value: str) -> list[str]:
        return [part.strip() for part in strip_quotes(value).split(",") if part.strip()]

    @staticmethod
    def _list_expr(values: list[str]) -> str:
        return "[" + ", ".join(option_to_python_expr(v) for v in values) + "]"

    @classmethod
    def _emit_send(cls, argv: list[str], body_fallback: str) -> str | None:
        payload = argv[1:]
        if len(payload) >= 5:
            attachments = cls._csv_items(payload[0])
            from_addr = strip_quotes(payload[1])
            subject = payload[2]
            body = (
                payload[3]
                if strip_quotes(payload[3])
                else (body_fallback or payload[2])
            )
            to = payload[4]

            kwargs: dict[str, str] = {
                "to": option_to_python_expr(to),
                "subject": option_to_python_expr(subject),
                "body": option_to_python_expr(body),
            }
            if attachments:
                kwargs["attachments"] = cls._list_expr(attachments)
            if from_addr and from_addr.lower() != "self":
                kwargs["from_addr"] = option_to_python_expr(from_addr)

            return EmitContext.render_method_call("email", "send", kwargs=kwargs)

        if len(payload) >= 3:
            return EmitContext.render_method_call(
                "email",
                "send",
                kwargs={
                    "to": option_to_python_expr(payload[0]),
                    "subject": option_to_python_expr(payload[1]),
                    "body": option_to_python_expr(payload[2]),
                },
            )

        return None

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
        msg.set_content(self._resolve_body(body))

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

    @staticmethod
    def _resolve_body(body: str) -> str:
        path = Path(body)
        if body and path.exists() and path.is_file():
            return path.read_text(encoding="utf-8", errors="replace")
        return body
