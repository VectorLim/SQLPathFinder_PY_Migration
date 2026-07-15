"""WaitFile - poll for a file to appear within a timeout window."""

from __future__ import annotations

import time
from pathlib import Path

from vg2c.emitter.models import emittable
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import split_utility_command, strip_quotes
from vg2c.utilities.macro_state import MacroState
from vg2c.kind import Kind


class WaitFile(EmitterUtility):
    """Poll for a file to appear, up to a configurable timeout (seconds).

    VG2 syntax::

        /UTILITIES=@EXEDIR@\\WaitFile.va "<file_path>" "<timeout_seconds>"

    The second argument is the timeout in seconds (default 30).  The utility
    returns as soon as the file exists or the timeout elapses — it never
    raises; callers that need a hard failure should check the file afterwards.
    """

    utility_name = "wait_file"
    handles = (Kind.WAIT_FILE,)

    _WAITFILE_NAMES = {"waitfile.va", "waitfile.bat", "waitfile.exe"}
    _DEFAULT_TIMEOUT = 30
    _POLL_INTERVAL = 5

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        text = options.lookup.get("UTILITIES", "").strip()
        if not text:
            return None
        argv = split_utility_command(text)
        if not argv:
            return None
        basename = argv[0].split("/")[-1].split("\\")[-1].lower()
        if basename in WaitFile._WAITFILE_NAMES:
            return Kind.WAIT_FILE, "/UTILITIES command maps to WaitFile poll"
        return None

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        return split_utility_command(text)

    @classmethod
    def emit_block(cls, block) -> tuple[str, list[str]]:
        argv = cls._utility_argv(block)
        # argv[0] = tool path, argv[1] = file path, argv[2] = timeout seconds
        path_expr = MacroState.to_py_expr(argv[1]) if len(argv) > 1 else repr("")
        raw_timeout = (
            strip_quotes(argv[2]) if len(argv) > 2 else str(cls._DEFAULT_TIMEOUT)
        )
        try:
            timeout_val = int(raw_timeout)
        except ValueError:
            timeout_val = cls._DEFAULT_TIMEOUT
        stmt = cls.poll.render(path_expr, timeout_val)
        return "wait_file", [stmt]

    @emittable
    def poll(
        self,
        path: str | Path,
        timeout: int = _DEFAULT_TIMEOUT,
        interval: int = _POLL_INTERVAL,
    ) -> bool:
        """Return True when *path* exists; False if timeout elapses first."""
        deadline = time.monotonic() + timeout
        while time.monotonic() < deadline:
            if Path(path).exists():
                return True
            time.sleep(interval)
        return Path(path).exists()
