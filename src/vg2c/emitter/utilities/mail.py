"""MailService - send email via stdlib smtplib."""

from __future__ import annotations

import os
import smtplib
from email.message import EmailMessage
from pathlib import Path
from typing import Any, NamedTuple


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

    SMTP_PASSWORD_ENV = "Lyc040513"

    DEFAULT_SMTP_HOST = "smtpauth.intel.com"
    DEFAULT_SMTP_PORT = 587
    DEFAULT_FROM_ADDRESS = "yeu.chuan.lim@intel.com"

    class _SMTPSettings(NamedTuple):
        host: str
        port: int
        password: str
        sender: str

        @property
        def uses_authentication(self) -> bool:
            return bool(self.password)

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
    def _smtp_settings(cls, from_addr: str | None = None) -> _SMTPSettings:

        return cls._SMTPSettings(
            host=cls.DEFAULT_SMTP_HOST,
            port=cls.DEFAULT_SMTP_PORT,
            password=cls.SMTP_PASSWORD_ENV,
            sender=from_addr or cls.DEFAULT_FROM_ADDRESS,
        )

    @classmethod
    def _send_via_smtp(cls, message: EmailMessage, settings: _SMTPSettings) -> None:
        try:
            with smtplib.SMTP(settings.host, settings.port) as smtp:
                smtp.ehlo()
                smtp.starttls()
                smtp.ehlo()
                if settings.uses_authentication:
                    smtp.login(cls.DEFAULT_FROM_ADDRESS, settings.password)
                smtp.send_message(message)
        except smtplib.SMTPAuthenticationError as exc:
            raise RuntimeError(
                "MailService: failed to send email via "
                f"{settings.host}:{settings.port}. Error: {exc}"
            ) from exc

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

            kwargs: dict[str, Any] = {
                "to": MacroState.to_py_expr(to),
                "subject": MacroState.to_py_expr(subject),
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
        settings = self._smtp_settings(from_addr)

        msg = EmailMessage()
        msg["Subject"] = subject
        msg["From"] = settings.sender
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

        self._send_via_smtp(msg, settings)

    @staticmethod
    def _resolve_body(body: str) -> str:
        path = Path(body)
        if body and path.exists() and path.is_file():
            return path.read_text(encoding="utf-8", errors="replace")
        return body
