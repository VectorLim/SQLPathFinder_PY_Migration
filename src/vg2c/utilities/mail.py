"""Email utility metadata and delegation to the DataSyncX runtime adapter."""

from __future__ import annotations

from pathlib import Path
from typing import Any

from vg2c.emitter.models import emittable
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import (
    list_code_expr,
    split_utility_command,
    to_code_expr,
)
from vg2c.utilities._runtime_helpers import strip_quotes
from vg2c.utility_metadata import FileEffectDefinition


class MailService(EmitterUtility):
    """Send email using DataSyncX's SMTP service and authentication."""

    utility_name = "email"
    handles = (Kind.EMAIL,)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        text = options.lookup.get("UTILITIES", "")
        if not text:
            return None
        if MailService._is_mail_utility(split_utility_command(text)):
            return Kind.EMAIL, "/UTILITIES command is SQLPathFinder_Email.va"
        return None

    @classmethod
    def emit_block(cls, block: Any, *, global_refs=None) -> list[str] | None:
        argv = cls._utility_argv(block)
        if not cls._is_mail_utility(argv):
            return None
        kwargs = cls._send_kwargs(argv, block.resolved_body)
        if kwargs is None:
            raise ValueError(
                "Unsupported SQLPathFinder_Email.va arguments: "
                "expected recipient, subject and message fields"
            )
        for field in ("to", "subject"):
            key = f"EMAIL_{field.upper()}"
            if global_refs and key in global_refs:
                kwargs[field] = global_refs[key]
        return [cls.send.render(**kwargs)]

    @classmethod
    def extract_globals(cls, block: Any) -> dict[str, object]:
        kwargs = cls._send_kwargs(cls._utility_argv(block), block.resolved_body) or {}
        return {
            f"EMAIL_{field.upper()}": kwargs[field].value
            for field in ("to", "subject")
            if field in kwargs and kwargs[field].has_value
        }

    @staticmethod
    def _utility_argv(block: Any) -> list[str]:
        return split_utility_command(block.resolved_options.lookup.get("UTILITIES", ""))

    @staticmethod
    def _is_mail_utility(argv: list[str]) -> bool:
        if not argv:
            return False
        basename = strip_quotes(argv[0]).split("/")[-1].split("\\")[-1].lower()
        return "sqlpathfinder_email" in basename

    @staticmethod
    def _csv_items(value: str) -> list[str]:
        return [p.strip() for p in strip_quotes(value).split(",") if p.strip()]

    @classmethod
    def _send_kwargs(cls, argv: list[str], body_fallback: str) -> dict[str, Any] | None:
        payload = argv[1:]

        if len(payload) >= 5:
            attachments = cls._csv_items(payload[0])
            from_addr = strip_quotes(payload[1])
            body = (
                payload[3]
                if strip_quotes(payload[3])
                else (body_fallback or payload[2])
            )

            kwargs: dict[str, Any] = {
                "to": to_code_expr(payload[4]),
                "subject": to_code_expr(payload[2]),
                "body": to_code_expr(body),
            }
            if attachments:
                kwargs["attachments"] = list_code_expr(attachments)
            if from_addr and from_addr.lower() != "self":
                kwargs["from_addr"] = to_code_expr(from_addr)
            return kwargs

        if len(payload) >= 3:
            return {
                "to": to_code_expr(payload[0]),
                "subject": to_code_expr(payload[1]),
                "body": to_code_expr(payload[2]),
            }

        return None

    @emittable(
        display_name="Send Email",
        summary="To: {to} · {subject}",
        capabilities=("email",),
        parameter_capabilities={"attachments": ("file-input",)},
        file_effects=(
            FileEffectDefinition(
                "attachments",
                "read",
                inputs=("attachments",),
                input_base="working-directory",
                reason=("Existing attachments are read; message body may also be read "
                        "from a runtime file."),
            ),
            FileEffectDefinition(
                "body",
                "unknown",
                reason="Message body is literal text or an existing file at runtime.",
            ),
        )
    )
    def send(
        self,
        to: str,
        subject: str,
        body: str,
        attachments: list[str] | None = None,
        from_addr: str | None = None,
        enabled: bool = True,
    ) -> None:
        from vg2c.runtime.mail import send_mail
        send_mail(to, subject, body, attachments, from_addr, enabled, workdir=Path.cwd())
