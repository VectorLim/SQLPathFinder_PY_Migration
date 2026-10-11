"""Malformed *recognized* utility commands must not translate to silent no-ops.

This is an emitter regression, not an SMTP integration test. No email is sent.
"""

from __future__ import annotations

import pytest

from vg2c import compile_document


def test_malformed_recognized_email_command_is_rejected(tmp_path):
    fixture = tmp_path / "invalid_email.txt"
    fixture.write_text(
        "<OPTIONS>\n"
        "/UTILITIES=SQLPathFinder_Email.va only_one_argument\n"
        "</OPTIONS>\n",
        encoding="utf-8",
    )

    with pytest.raises(ValueError, match="email|Email|EMAIL"):
        compile_document(fixture)
