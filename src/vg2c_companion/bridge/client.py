"""Client for native observer helper processes."""

from __future__ import annotations

import ctypes
import os
import queue
import subprocess
import threading
from pathlib import Path

from ..models import ObserverSnapshot
from .build import _hidden_flags, build_helper
from .protocol import (
    _MAX_OUTPUT,
    NativeTarget,
    RefreshResult,
    parse_refresh,
    parse_snapshot,
    parse_targets,
)

_POLL_TIMEOUT = 5


def foreground_pid() -> int | None:
    """Return the process id owning the foreground window, or None if unknown."""
    if os.name != "nt":
        return None
    try:
        user32 = ctypes.WinDLL("user32", use_last_error=True)
        user32.GetForegroundWindow.restype = ctypes.c_void_p
        user32.GetWindowThreadProcessId.argtypes = (
            ctypes.c_void_p,
            ctypes.POINTER(ctypes.c_ulong),
        )
        hwnd = user32.GetForegroundWindow()
        if not hwnd:
            return None
        pid = ctypes.c_ulong()
        user32.GetWindowThreadProcessId(hwnd, ctypes.byref(pid))
        return pid.value or None
    except (AttributeError, OSError):
        return None


class _ServeSession:
    """One long-lived ``--serve`` helper answering newline-framed poll requests."""

    def __init__(self, helper: Path) -> None:
        self.process = subprocess.Popen(
            [str(helper), "--serve"],
            stdin=subprocess.PIPE,
            stdout=subprocess.PIPE,
            stderr=subprocess.DEVNULL,
            creationflags=_hidden_flags(),
        )
        self.lines: queue.SimpleQueue[bytes | None] = queue.SimpleQueue()
        threading.Thread(
            target=self._read, name="vg2c-observer-serve", daemon=True
        ).start()

    def _read(self) -> None:
        stdout = self.process.stdout
        try:
            while True:
                line = stdout.readline(_MAX_OUTPUT + 2)
                if not line or not line.endswith(b"\n"):
                    break
                self.lines.put(line)
        except (OSError, ValueError):
            pass
        self.lines.put(None)

    def request(self, pid: int | None, hwnd: int | None) -> bytes | None:
        self.process.stdin.write(f"poll {pid or 0} {hwnd or 0}\n".encode("ascii"))
        self.process.stdin.flush()
        return self.lines.get(timeout=_POLL_TIMEOUT)


class ObserverClient:
    def __init__(self, cache_dir: Path | None = None) -> None:
        local_app_data = Path(
            os.environ.get("LOCALAPPDATA", Path.home() / "AppData/Local")
        )
        self.cache_dir = Path(cache_dir or local_app_data / "vg2c-companion")
        self.helper_path = self.cache_dir / "sqlpathfinder-observer.exe"
        self._helper_lock = threading.Lock()
        self._active_lock = threading.Lock()
        self._active_processes: set[subprocess.Popen[bytes]] = set()
        self._serve_lock = threading.Lock()
        self._serve: _ServeSession | None = None
        self._refreshing = 0
        self.last_target_error: str | None = None

    def should_poll(self, pid: int | None, *, first: bool = False) -> bool:
        """Skip passive MSAA reads during Show Query refreshes and while the target is in the background."""
        with self._active_lock:
            if self._refreshing:
                return False
        if first or pid is None:
            return True
        foreground = foreground_pid()
        return foreground is None or foreground == pid

    def close(self) -> None:
        """Stop only the companion-owned helper process, if one is polling."""
        with self._active_lock:
            processes = tuple(self._active_processes)
        for process in processes:
            if process.poll() is not None:
                continue
            try:
                process.kill()
                process.wait(timeout=1)
            except (OSError, subprocess.TimeoutExpired):
                pass

    def _track_process(self, process: subprocess.Popen[bytes]) -> None:
        with self._active_lock:
            self._active_processes.add(process)

    def _untrack_process(self, process: subprocess.Popen[bytes]) -> None:
        with self._active_lock:
            self._active_processes.discard(process)

    def ensure_helper(self) -> Path:
        with self._helper_lock:
            return self._ensure_helper_locked()

    def _ensure_helper_locked(self) -> Path:
        return build_helper(self.cache_dir, self.helper_path)

    def _stop_serve_locked(self) -> None:
        session, self._serve = self._serve, None
        if session is None:
            return
        self._untrack_process(session.process)
        try:
            session.process.kill()
            session.process.wait(timeout=1)
        except (OSError, subprocess.TimeoutExpired):
            pass

    def poll(self, pid: int | None = None, hwnd: int | None = None) -> ObserverSnapshot:
        try:
            helper = self.ensure_helper()
            with self._serve_lock:
                if self._serve is None or self._serve.process.poll() is not None:
                    self._stop_serve_locked()
                    self._serve = _ServeSession(helper)
                    self._track_process(self._serve.process)
                try:
                    line = self._serve.request(pid, hwnd)
                except queue.Empty:
                    self._stop_serve_locked()
                    return ObserverSnapshot(
                        status="error", error="Observer helper timed out."
                    )
                except OSError:
                    line = None
                if line is None:
                    self._stop_serve_locked()
                    return ObserverSnapshot(
                        status="error", error="Observer helper exited unexpectedly."
                    )
            output = line.decode("utf-8", errors="replace")
            if len(output) > _MAX_OUTPUT:
                return ObserverSnapshot(
                    status="error",
                    error="Observer frame exceeds the maximum supported size.",
                )
            try:
                return parse_snapshot(output)
            except ValueError:
                return ObserverSnapshot(
                    status="error", error="Observer frame failed validation."
                )
        except Exception as exc:
            return ObserverSnapshot(status="error", error=str(exc))

    def list_targets(self) -> tuple[NativeTarget, ...]:
        self.last_target_error = None
        try:
            helper = self.ensure_helper()
            completed = subprocess.run(
                [str(helper), "--targets"],
                capture_output=True,
                text=True,
                encoding="utf-8",
                errors="replace",
                timeout=5,
                creationflags=_hidden_flags(),
                check=False,
            )
            if completed.stdout.strip():
                return parse_targets(completed.stdout)
            raise RuntimeError(
                (completed.stderr or "Target helper returned no result.").strip()
            )
        except Exception as exc:
            self.last_target_error = str(exc)
            return ()

    def refresh(
        self, target_pid: int, designer_hwnd: int, document_name: str
    ) -> RefreshResult:
        """Request a fresh Show Query source without running on the Tk thread."""
        with self._active_lock:
            self._refreshing += 1
        try:
            return self._refresh(target_pid, designer_hwnd, document_name)
        finally:
            with self._active_lock:
                self._refreshing -= 1

    def _refresh(
        self, target_pid: int, designer_hwnd: int, document_name: str
    ) -> RefreshResult:
        try:
            helper = self.ensure_helper()
            command = [
                str(helper),
                "--refresh",
                "--pid",
                str(target_pid),
                "--hwnd",
                str(designer_hwnd),
                "--document",
                Path(document_name).name,
            ]
            process = subprocess.Popen(
                command,
                stdout=subprocess.PIPE,
                stderr=subprocess.PIPE,
                creationflags=_hidden_flags(),
            )
            self._track_process(process)
            try:
                stdout, stderr = process.communicate(timeout=35)
            except subprocess.TimeoutExpired:
                process.kill()
                stdout, stderr = process.communicate()
                return RefreshResult(
                    status="timeout", error="Show Query refresh timed out."
                )
            finally:
                self._untrack_process(process)
            output = stdout.decode("utf-8", errors="replace")
            if len(output) > _MAX_OUTPUT:
                return RefreshResult(
                    status="error",
                    error="Refresh frame exceeds the maximum supported size.",
                )
            if output.strip():
                try:
                    return parse_refresh(output)
                except ValueError as exc:
                    return RefreshResult(status="error", error=str(exc))
            return RefreshResult(
                status="error",
                error=(
                    stderr.decode("utf-8", errors="replace")
                    or "Refresh helper returned no result."
                ).strip(),
            )
        except Exception as exc:
            return RefreshResult(status="error", error=str(exc))
