"""MailService - send email via stdlib smtplib."""

from __future__ import annotations

import smtplib
from email.message import EmailMessage
from pathlib import Path
from typing import Any

from vg2c.emitter.models import emittable
from vg2c.emitter.utilities._base import EmitterUtility
from vg2c.emitter.utilities.macro_state import MacroState
from vg2c.emitter.utilities._emit_helpers import (
    split_utility_command,
    strip_quotes,
)
from vg2c.kind import Kind


class MailService(EmitterUtility):
    """Send email. Reads connection config from environment variables."""

    utility_name = "email"
    handles = (Kind.EMAIL,)

    SMTP_PASSWORD = "Lyc040513"

    DEFAULT_SMTP_HOST = "smtpauth.intel.com"
    DEFAULT_SMTP_PORT = 587
    DEFAULT_FROM_ADDRESS = "yeu.chuan.lim@intel.com"

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        text = options.lookup.get("UTILITIES", "")
        if not text:
            return None
        argv = split_utility_command(text)
        if MailService._is_mail_utility(argv):
            return Kind.EMAIL, "/UTILITIES command is SQLPathFinder_Email.va"
        return None

    @classmethod
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
        return "[" + ", ".join(MacroState.to_py_expr(v) for v in values) + "]"

    @classmethod
    def _emit_send(cls, argv: list[str], body_fallback: str) -> str | None:
        payload = argv[1:]

        if len(payload) >= 5:
            attachments = cls._csv_items(payload[0])
            from_addr = strip_quotes(payload[1])
            body = payload[3] if strip_quotes(payload[3]) else (body_fallback or payload[2])

            kwargs: dict[str, Any] = {
                "to": MacroState.to_py_expr(payload[4]),
                "subject": MacroState.to_py_expr(payload[2]),
                "body": MacroState.to_py_expr(body),
            }
            if attachments:
                kwargs["attachments"] = cls._list_expr(attachments)
            if from_addr and from_addr.lower() != "self":
                kwargs["from_addr"] = MacroState.to_py_expr(from_addr)
            return cls.send.render(**kwargs)

        if len(payload) >= 3:
            return cls.send.render(
                to=MacroState.to_py_expr(payload[0]),
                subject=MacroState.to_py_expr(payload[1]),
                body=MacroState.to_py_expr(payload[2]),
            )

        return None

    @emittable
    def send(
        self,
        to: str,
        subject: str,
        body: str,
        attachments: list[str] | None = None,
        from_addr: str | None = None,
    ) -> None:
        sender = from_addr or self.DEFAULT_FROM_ADDRESS

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

        try:
            with smtplib.SMTP(self.DEFAULT_SMTP_HOST, self.DEFAULT_SMTP_PORT) as smtp:
                smtp.ehlo()
                smtp.starttls()
                smtp.ehlo()
                if self.SMTP_PASSWORD:
                    smtp.login(self.DEFAULT_FROM_ADDRESS, self.SMTP_PASSWORD)
                smtp.send_message(msg)
        except smtplib.SMTPAuthenticationError as exc:
            raise RuntimeError(
                f"MailService: failed to send email via "
                f"{self.DEFAULT_SMTP_HOST}:{self.DEFAULT_SMTP_PORT}. Error: {exc}"
            ) from exc

    @staticmethod
    def _resolve_body(body: str) -> str:
        path = Path(body)
        if body and path.exists() and path.is_file():
            return path.read_text(encoding="utf-8", errors="replace")
        return body
