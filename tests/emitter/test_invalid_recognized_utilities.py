"""Malformed *recognized* utility commands must not translate to silent no-ops.

These are compiler-only checks. No SMTP connection or credentials are used.
"""

from __future__ import annotations

from types import SimpleNamespace

import pytest

from vg2c import compile_document
from vg2c.utilities.mail import MailService


def test_mail_handler_rejects_malformed_recognized_command():
    block = SimpleNamespace(
        resolved_options=SimpleNamespace(
            lookup={"UTILITIES": "SQLPathFinder_Email.va only_one_argument"}
        ),
        resolved_body="",
    )
    with pytest.raises(ValueError, match="Unsupported SQLPathFinder_Email"):
        MailService.emit_block(block)


def test_malformed_recognized_email_command_is_rejected_in_compiler(tmp_path):
    # Name intentionally excludes the word 'email' so a generic exception
    # including the source path cannot satisfy the expected diagnostic.
    source = tmp_path / "source.txt"
    source.write_text(
        "<OPTIONS>\n"
        "/UTILITIES=SQLPathFinder_Email.va only_one_argument\n"
        "</OPTIONS>\n",
        encoding="utf-8",
    )
    with pytest.raises(ValueError, match="Unsupported SQLPathFinder_Email"):
        compile_document(source)
