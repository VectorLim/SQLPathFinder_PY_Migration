from __future__ import annotations

from pathlib import Path
from unittest.mock import Mock

from vg2c_new.model import Command, CommandKind, SourceSpan
from vg2c_new.runtime import RuntimeState
from vg2c_new.utilities.email import EmailUtility


def command(
    target: str,
    args: tuple[str, ...] = (),
    *,
    options: tuple[tuple[str, str], ...] = (),
    body: str = "",
) -> Command:
    kind = CommandKind.QUERY if target.startswith("query.") else CommandKind.UTILITY
    return Command(1, kind, "TEST", target, options, body, "", args, SourceSpan(None, 1, 1))


def state(tmp_path: Path) -> RuntimeState:
    return RuntimeState(tmp_path)


def test_email_reference(tmp_path: Path) -> None:
    runtime = state(tmp_path)
    (tmp_path / "body.txt").write_text("hello", encoding="utf-8")
    send = Mock()
    EmailUtility(send).apply(
        command(
            "email",
            ("a.csv", "a@x;b@y", "sub", "body.txt", "c@x", "d@x", "", "N", "N"),
        ),
        runtime,
    )
    assert send.call_args.kwargs["to"] == ["a@x", "b@y"]
    assert send.call_args.kwargs["body"] == "hello"
