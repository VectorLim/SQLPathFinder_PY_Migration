"""Tests for WaitFile utility — check(), emit_block(), and poll() runtime."""

from __future__ import annotations

import time
from pathlib import Path
from types import SimpleNamespace
from unittest.mock import patch

import pytest

from vg2c.utilities.wait_file import WaitFile
from vg2c.kind import Kind

# ---------------------------------------------------------------------------
# Helpers
# ---------------------------------------------------------------------------


def _opts(utilities: str = "") -> SimpleNamespace:
    return SimpleNamespace(lookup={"UTILITIES": utilities} if utilities else {})


def _block(utilities: str, kind: Kind = Kind.WAIT_FILE) -> SimpleNamespace:
    return SimpleNamespace(
        kind=kind,
        index=4,
        resolved_options=_opts(utilities),
    )


FIXTURE_UTILITIES = (
    r"@EXEDIR@\WaitFile.va"
    r' "\\kmatshfs.intel.com\kmatanalysis$\MAOATM\KuAT\TCB\TimeDelta.csv"'
    r' "30"'
)


# ---------------------------------------------------------------------------
# check()
# ---------------------------------------------------------------------------


class TestCheck:
    def test_waitfile_va_detected(self):
        opts = _opts(FIXTURE_UTILITIES)
        result = WaitFile.check(opts)
        assert result is not None
        kind, reason = result
        assert kind is Kind.WAIT_FILE
        assert "WaitFile" in reason

    def test_case_insensitive_basename(self):
        opts = _opts(r"@EXEDIR@\WAITFILE.VA \"C:\some\file.csv\" \"60\"")
        assert WaitFile.check(opts) is not None

    def test_unknown_utility_returns_none(self):
        opts = _opts(r"@EXEDIR@\RoboCopy.bat \"src\" \"dst\"")
        assert WaitFile.check(opts) is None

    def test_empty_utilities_returns_none(self):
        assert WaitFile.check(_opts()) is None

    def test_blank_utilities_returns_none(self):
        assert WaitFile.check(_opts("   ")) is None


# ---------------------------------------------------------------------------
# emit_block()
# ---------------------------------------------------------------------------


class TestEmitBlock:
    def test_fixture_block_emits_poll_call(self):
        block = _block(FIXTURE_UTILITIES)
        result = WaitFile.emit_block(block)
        assert result is not None
        suffix, lines = result
        assert suffix == "wait_file"
        assert len(lines) == 1
        stmt = lines[0]
        # Should call poll on the wait_file utility
        assert "ctx.wait_file.poll(" in stmt
        # Timeout 30 must appear as an integer literal
        assert "30" in stmt

    def test_emits_path_expression(self):
        block = _block(FIXTURE_UTILITIES)
        _, lines = WaitFile.emit_block(block)
        assert r"kmatshfs.intel.com" in lines[0]

    def test_missing_timeout_defaults_to_30(self):
        # Only tool + path, no timeout arg
        block = _block(r'@EXEDIR@\WaitFile.va "C:\file.csv"')
        _, lines = WaitFile.emit_block(block)
        assert "30" in lines[0]

    def test_explicit_timeout_emitted(self):
        block = _block(r'@EXEDIR@\WaitFile.va "C:\file.csv" "120"')
        _, lines = WaitFile.emit_block(block)
        assert "120" in lines[0]


# ---------------------------------------------------------------------------
# poll() runtime behaviour
# ---------------------------------------------------------------------------


class TestPollRuntime:
    def test_returns_true_when_file_exists_immediately(self, tmp_path):
        target = tmp_path / "ready.csv"
        target.touch()
        assert WaitFile().poll(target, timeout=5) is True

    def test_returns_true_when_file_appears_during_poll(self, tmp_path):
        target = tmp_path / "delayed.csv"

        call_count = 0
        original_sleep = time.sleep

        def _fake_sleep(secs):
            nonlocal call_count
            call_count += 1
            if call_count == 1:
                target.touch()

        with patch(
            "vg2c.emitter.utilities.wait_file.time.sleep", side_effect=_fake_sleep
        ):
            result = WaitFile().poll(target, timeout=60, interval=1)

        assert result is True

    def test_returns_false_when_file_never_appears(self, tmp_path):
        target = tmp_path / "missing.csv"

        # Make monotonic() advance instantly past deadline after first check
        _calls = [0]
        _start = time.monotonic()

        def _fast_monotonic():
            _calls[0] += 1
            # First call sets deadline; after 2 calls pretend time has passed
            return _start if _calls[0] <= 1 else _start + 9999

        with (
            patch(
                "vg2c.emitter.utilities.wait_file.time.monotonic",
                side_effect=_fast_monotonic,
            ),
            patch("vg2c.emitter.utilities.wait_file.time.sleep"),
        ):
            result = WaitFile().poll(target, timeout=30, interval=1)

        assert result is False

    def test_poll_accepts_string_path(self, tmp_path):
        target = tmp_path / "str_path.csv"
        target.touch()
        assert WaitFile().poll(str(target), timeout=5) is True


# ---------------------------------------------------------------------------
# Registration
# ---------------------------------------------------------------------------


def test_waitfile_registered_in_utility_spec():
    from vg2c.utilities._base import UtilitySpec

    assert "wait_file" in UtilitySpec._registry
    assert UtilitySpec._registry["wait_file"] is WaitFile


def test_waitfile_registered_as_handler_for_wait_file_kind():
    from vg2c.utilities._base import UtilitySpec

    assert UtilitySpec._emit_handlers.get(Kind.WAIT_FILE) is WaitFile


# ---------------------------------------------------------------------------
# Fixture-backed emit check (lines 188-195 of TimeDelta.txt)
# ---------------------------------------------------------------------------


def test_fixture_block_expected_emit_output():
    """Validate exact emitted statement for the TimeDelta.txt Step 4 block."""
    block = _block(FIXTURE_UTILITIES, kind=Kind.WAIT_FILE)
    suffix, lines = WaitFile.emit_block(block)

    assert suffix == "wait_file"
    assert len(lines) == 1

    stmt = lines[0]
    # Must be a poll call on wait_file context object
    assert stmt.startswith("ctx.wait_file.poll(")
    # File path present
    assert "TimeDelta.csv" in stmt
    # Timeout is the integer 30
    assert ", 30)" in stmt
