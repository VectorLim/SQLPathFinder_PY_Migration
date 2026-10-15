"""Bounded Windows bridge for observation and explicit Show Query requests."""

from __future__ import annotations

import hashlib
import importlib.resources
import json
import os
import queue
import subprocess
import threading
import uuid
from dataclasses import dataclass
from pathlib import Path

from .models import ObserverSnapshot, Selection

_PROCESS_NAME = "sqlpathfinder3"
_MAX_OUTPUT = 300_000
_WINDOWS_STATUS = {"ok", "missing", "exited", "ambiguous", "inaccessible", "error"}
_REFRESH_STATUS = _WINDOWS_STATUS | {"editor-open", "unsupported", "timeout", "empty"}


@dataclass(frozen=True, slots=True)
class RefreshResult:
    """Result of one explicit native Show Query refresh request."""

    target_pid: int | None = None
    designer_window_title: str = ""
    designer_fingerprint: str = ""
    source_text: str = ""
    status: str = "error"
    error: str | None = None


@dataclass(frozen=True, slots=True)
class NativeTarget:
    """One exact, currently open SQLPathFinder designer target."""

    target_pid: int
    designer_hwnd: int
    designer_window_title: str
    document_name: str


def _hidden_flags() -> int:
    return getattr(subprocess, "CREATE_NO_WINDOW", 0)


def _optional_int(value: object, field: str) -> int | None:
    if value is None:
        return None
    if isinstance(value, bool) or not isinstance(value, int):
        raise ValueError(f"Observer field {field} must be an integer or null.")
    return value


def parse_snapshot(payload: str) -> ObserverSnapshot:
    if not isinstance(payload, str) or len(payload) > _MAX_OUTPUT:
        raise ValueError("Observer frame exceeds the maximum supported size.")
    try:
        data = json.loads(payload)
    except json.JSONDecodeError as exc:
        raise ValueError(f"Observer frame is not valid JSON: {exc.msg}") from exc
    if not isinstance(data, dict):
        raise ValueError("Observer frame must be a JSON object.")

    target_pid = _optional_int(data.get("targetPid"), "targetPid")
    window_title = data.get("windowTitle", "")
    source_text = data.get("sourceText", "")
    source_truncated = data.get("sourceTruncated", False)
    fingerprint = data.get("fingerprint", "")
    designer_fingerprint = data.get("designerFingerprint", "")
    if designer_fingerprint is None:
        designer_fingerprint = ""
    designer_window_title = data.get("designerWindowTitle", "")
    designer_bounds_value = data.get("designerBounds")
    status = data.get("status", "ok")
    error = data.get("error")
    if not isinstance(window_title, str) or not isinstance(source_text, str):
        raise ValueError("Observer windowTitle and sourceText must be strings.")
    if len(source_text) > 200_000:
        raise ValueError("Observer sourceText exceeds the helper bound.")
    if not isinstance(designer_window_title, str):
        raise ValueError("Observer designerWindowTitle must be a string.")
    if not isinstance(source_truncated, bool):
        raise ValueError("Observer sourceTruncated must be boolean.")
    if not isinstance(fingerprint, str) or not isinstance(designer_fingerprint, str):
        raise ValueError("Observer fingerprints must be strings.")
    if not isinstance(status, str) or status not in _WINDOWS_STATUS:
        raise ValueError("Observer status is invalid.")
    if error is not None and not isinstance(error, str):
        raise ValueError("Observer error must be a string or null.")

    raw_selection = data.get("selection", {})
    if not isinstance(raw_selection, dict):
        raise ValueError("Observer selection must be an object.")
    selection_text = raw_selection.get("text", "")
    selection_truncated = raw_selection.get("truncated", False)
    if not isinstance(selection_text, str) or not isinstance(selection_truncated, bool):
        raise ValueError("Observer selection text/truncated fields have invalid types.")
    if len(selection_text) > _MAX_OUTPUT:
        raise ValueError("Observer selection exceeds the maximum supported size.")
    raw_values = data.get("designerValues", [])
    if not isinstance(raw_values, list) or not all(isinstance(item, str) for item in raw_values):
        raise ValueError("Observer designerValues must be a string list.")
    if len(raw_values) > 100:
        raise ValueError("Observer designerValues exceeds the bounded item count.")
    if any(len(item) > 1024 for item in raw_values):
        raise ValueError("Observer designerValues contains an oversized item.")
    raw_bounds = data.get("bounds")
    bounds: tuple[int, int, int, int] | None
    if raw_bounds is None:
        bounds = None
    elif (
        isinstance(raw_bounds, list)
        and len(raw_bounds) == 4
        and all(isinstance(item, int) and not isinstance(item, bool) for item in raw_bounds)
    ):
        bounds = tuple(raw_bounds)  # type: ignore[assignment]
    else:
        raise ValueError("Observer bounds must be a four-integer list or null.")
    if designer_bounds_value is None:
        designer_bounds = None
    elif (
        isinstance(designer_bounds_value, list)
        and len(designer_bounds_value) == 4
        and all(isinstance(item, int) and not isinstance(item, bool) for item in designer_bounds_value)
    ):
        designer_bounds = tuple(designer_bounds_value)  # type: ignore[assignment]
    else:
        raise ValueError("Observer designerBounds must be a four-integer list or null.")

    return ObserverSnapshot(
        target_pid=target_pid,
        window_title=window_title,
        source_text=source_text,
        source_truncated=source_truncated,
        selection=Selection(
            text=selection_text,
            start=_optional_int(raw_selection.get("start"), "selection.start"),
            end=_optional_int(raw_selection.get("end"), "selection.end"),
            truncated=selection_truncated,
        ),
        designer_values=tuple(raw_values),
        fingerprint=fingerprint,
        designer_fingerprint=designer_fingerprint,
        designer_window_title=designer_window_title,
        designer_bounds=designer_bounds,
        status=status,
        error=error,
        bounds=bounds,
    )


def parse_refresh(payload: str) -> RefreshResult:
    if not isinstance(payload, str) or len(payload) > _MAX_OUTPUT:
        raise ValueError("Refresh frame exceeds the maximum supported size.")
    try:
        data = json.loads(payload)
    except json.JSONDecodeError as exc:
        raise ValueError(f"Refresh frame is not valid JSON: {exc.msg}") from exc
    if not isinstance(data, dict):
        raise ValueError("Refresh frame must be a JSON object.")
    target_pid = _optional_int(data.get("targetPid"), "targetPid")
    title = data.get("designerWindowTitle", "")
    designer_fingerprint = data.get("designerFingerprint", "")
    if designer_fingerprint is None:
        designer_fingerprint = ""
    source = data.get("sourceText", "")
    status = data.get("status", "error")
    error = data.get("error")
    if not isinstance(title, str) or not isinstance(designer_fingerprint, str) or not isinstance(source, str):
        raise ValueError("Refresh title and sourceText must be strings.")
    if len(source) > 200_000:
        raise ValueError("Refresh sourceText exceeds the helper bound.")
    if not isinstance(status, str) or status not in _REFRESH_STATUS:
        raise ValueError("Refresh status is invalid.")
    if error is not None and not isinstance(error, str):
        raise ValueError("Refresh error must be a string or null.")
    if status == "ok" and (target_pid is None or target_pid <= 0 or not title or not source):
        raise ValueError("Successful refresh must include a target PID, designer title, and source text.")
    return RefreshResult(target_pid, title, designer_fingerprint, source, status, error)


def parse_targets(payload: str) -> tuple[NativeTarget, ...]:
    if not isinstance(payload, str) or len(payload) > _MAX_OUTPUT:
        raise ValueError("Target frame exceeds the maximum supported size.")
    try:
        data = json.loads(payload)
    except json.JSONDecodeError as exc:
        raise ValueError(f"Target frame is not valid JSON: {exc.msg}") from exc
    if not isinstance(data, dict) or data.get("status") != "ok":
        raise ValueError(str(data.get("error", "Target discovery failed.")) if isinstance(data, dict) else "Target frame must be an object.")
    raw_targets = data.get("targets", [])
    if not isinstance(raw_targets, list) or len(raw_targets) > 100:
        raise ValueError("Target list is invalid or oversized.")
    targets: list[NativeTarget] = []
    for raw in raw_targets:
        if not isinstance(raw, dict):
            raise ValueError("Target entry must be an object.")
        pid = raw.get("targetPid")
        hwnd = raw.get("designerHwnd")
        title = raw.get("designerWindowTitle", "")
        document = raw.get("documentName", "")
        if (
            isinstance(pid, bool) or not isinstance(pid, int) or pid <= 0
            or isinstance(hwnd, bool) or not isinstance(hwnd, int) or hwnd <= 0
            or not isinstance(title, str) or not isinstance(document, str)
            or not title or not document
        ):
            raise ValueError("Target entry has invalid identity fields.")
        targets.append(NativeTarget(pid, hwnd, title, document))
    return tuple(targets)


def _find_compiler(windows_root: Path) -> Path | None:
    for relative in (
        Path("Microsoft.NET/Framework64/v4.0.30319"),
        Path("Microsoft.NET/Framework/v4.0.30319"),
    ):
        candidate = windows_root / relative / "csc.exe"
        if candidate.is_file():
            return candidate
    return None


def _reference_paths(compiler: Path) -> tuple[Path, ...]:
    root = Path(compiler).parent
    return tuple(root / name for name in ("Accessibility.dll", "System.Runtime.Serialization.dll"))


def _helper_build_command(compiler: Path, output: Path, source: Path) -> list[str]:
    return [
        str(compiler),
        "/nologo",
        "/target:exe",
        f"/out:{output}",
        *[f"/reference:{path}" for path in _reference_paths(compiler)],
        str(source),
    ]


class ObserverClient:
    def __init__(self, cache_dir: Path | None = None) -> None:
        local_app_data = Path(os.environ.get("LOCALAPPDATA", Path.home() / "AppData/Local"))
        self.cache_dir = Path(cache_dir or local_app_data / "vg2c-companion")
        self.helper_path = self.cache_dir / "sqlpathfinder-observer.exe"
        self._helper_lock = threading.Lock()
        self._active_lock = threading.Lock()
        self._active_processes: set[subprocess.Popen[bytes]] = set()
        self.last_target_error: str | None = None

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
        if os.name != "nt":
            raise OSError("The SQLPathFinder observer requires Windows native window APIs.")
        source = importlib.resources.files("vg2c_companion").joinpath("observer.cs")
        source_text = source.read_text(encoding="utf-8")
        digest = hashlib.sha256(source_text.encode("utf-8")).hexdigest()
        stamp = self.cache_dir / "observer.sha256"
        if (
            self.helper_path.is_file()
            and self.helper_path.stat().st_size > 0
            and stamp.is_file()
            and stamp.read_text().strip() == digest
        ):
            return self.helper_path
        windows = Path(os.environ.get("WINDIR", r"C:\Windows"))
        compiler = _find_compiler(windows)
        if compiler is None:
            raise FileNotFoundError("Could not locate .NET Framework csc.exe.")
        references = _reference_paths(compiler)
        missing = [str(path) for path in references if not path.is_file()]
        if missing:
            raise FileNotFoundError("Missing .NET Accessibility assemblies: " + ", ".join(missing))
        self.cache_dir.mkdir(parents=True, exist_ok=True)
        token = uuid.uuid4().hex
        source_path = self.cache_dir / f".observer-{token}.cs"
        temporary_helper = self.cache_dir / f".observer-{token}.exe"
        temporary_stamp = self.cache_dir / f".observer-{token}.sha256"
        try:
            source_path.write_text(source_text, encoding="utf-8", newline="")
            completed = subprocess.run(
                _helper_build_command(compiler, temporary_helper, source_path),
                capture_output=True,
                text=True,
                encoding="utf-8",
                errors="replace",
                timeout=30,
                creationflags=_hidden_flags(),
                check=False,
            )
            if completed.returncode:
                detail = (completed.stderr or completed.stdout or "csc failed").strip()
                raise RuntimeError(f"Could not compile observer helper: {detail[-2000:]}")
            temporary_stamp.write_text(digest, encoding="ascii", newline="")
            os.replace(temporary_helper, self.helper_path)
            os.replace(temporary_stamp, stamp)
        finally:
            for path in (source_path, temporary_helper, temporary_stamp):
                try:
                    path.unlink()
                except FileNotFoundError:
                    pass
        return self.helper_path

    def poll(self, pid: int | None = None, hwnd: int | None = None) -> ObserverSnapshot:
        try:
            helper = self.ensure_helper()
            command = [str(helper), "--process", _PROCESS_NAME, "--once"]
            if pid is not None:
                command.extend(("--pid", str(pid)))
            if hwnd is not None:
                command.extend(("--hwnd", str(hwnd)))
            process = subprocess.Popen(
                command,
                stdout=subprocess.PIPE,
                stderr=subprocess.PIPE,
                creationflags=_hidden_flags(),
            )
            self._track_process(process)
            try:
                stdout, stderr = process.communicate(timeout=5)
            except subprocess.TimeoutExpired:
                process.kill()
                stdout, stderr = process.communicate()
                return ObserverSnapshot(status="error", error="Observer helper timed out.")
            finally:
                self._untrack_process(process)
            output = stdout.decode("utf-8", errors="replace")
            error_output = stderr.decode("utf-8", errors="replace")
            if len(output) > _MAX_OUTPUT:
                return ObserverSnapshot(status="error", error="Observer frame exceeds the maximum supported size.")
            if output.strip():
                try:
                    return parse_snapshot(output)
                except ValueError:
                    return ObserverSnapshot(status="error", error="Observer frame failed validation.")
            if process.returncode:
                return ObserverSnapshot(
                    status="error",
                    error=(error_output or "Observer helper returned an error.").strip(),
                )
            return ObserverSnapshot(status="error", error="Observer returned an empty frame.")
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
            raise RuntimeError((completed.stderr or "Target helper returned no result.").strip())
        except Exception as exc:
            self.last_target_error = str(exc)
            return ()

    def refresh(self, target_pid: int, designer_hwnd: int, document_name: str) -> RefreshResult:
        """Request a fresh Show Query source without running on the Tk thread."""
        try:
            helper = self.ensure_helper()
            command = [
                str(helper), "--refresh", "--pid", str(target_pid),
                "--hwnd", str(designer_hwnd), "--document", Path(document_name).name,
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
                return RefreshResult(status="timeout", error="Show Query refresh timed out.")
            finally:
                self._untrack_process(process)
            output = stdout.decode("utf-8", errors="replace")
            if len(output) > _MAX_OUTPUT:
                return RefreshResult(status="error", error="Refresh frame exceeds the maximum supported size.")
            if output.strip():
                try:
                    return parse_refresh(output)
                except ValueError as exc:
                    return RefreshResult(status="error", error=str(exc))
            return RefreshResult(
                status="error",
                error=(stderr.decode("utf-8", errors="replace") or "Refresh helper returned no result.").strip(),
            )
        except Exception as exc:
            return RefreshResult(status="error", error=str(exc))


class ObserverWorker:
    """Poll the native helper away from Tk and retain only the newest frame."""

    def __init__(self, client: ObserverClient, pid: int | None = None, hwnd: int | None = None) -> None:
        self._client = client
        self._pid = pid
        self._hwnd = hwnd
        self._target_lock = threading.Lock()
        self._target_epoch = 0
        self._frames: queue.Queue[tuple[int, ObserverSnapshot]] = queue.Queue(maxsize=1)
        self._stop = threading.Event()
        self._thread = threading.Thread(target=self._run, name="vg2c-observer", daemon=True)
        self._thread.start()

    def drain(self) -> ObserverSnapshot | None:
        latest = None
        with self._target_lock:
            epoch = self._target_epoch
            while True:
                try:
                    frame_epoch, frame = self._frames.get_nowait()
                    if frame_epoch == epoch:
                        latest = frame
                except queue.Empty:
                    return latest

    def close(self) -> None:
        self._stop.set()
        self._client.close()
        self._thread.join(timeout=2)

    def select_target(self, pid: int | None, hwnd: int | None) -> None:
        with self._target_lock:
            self._pid = pid
            self._hwnd = hwnd
            self._target_epoch += 1
            self._discard_frames()

    def invalidate_frames(self) -> None:
        with self._target_lock:
            self._target_epoch += 1
            self._discard_frames()

    def _discard_frames(self) -> None:
        while True:
            try:
                self._frames.get_nowait()
            except queue.Empty:
                return

    def _run(self) -> None:
        while not self._stop.is_set():
            with self._target_lock:
                pid, hwnd = self._pid, self._hwnd
                epoch = self._target_epoch
            frame = self._client.poll(pid, hwnd)
            with self._target_lock:
                if epoch != self._target_epoch:
                    continue
            try:
                self._frames.put_nowait((epoch, frame))
            except queue.Full:
                try:
                    self._frames.get_nowait()
                except queue.Empty:
                    pass
                try:
                    self._frames.put_nowait((epoch, frame))
                except queue.Full:
                    pass
            self._stop.wait(1)


__all__ = [
    "ObserverClient",
    "NativeTarget",
    "ObserverWorker",
    "RefreshResult",
    "parse_refresh",
    "parse_targets",
    "parse_snapshot",
    "_find_compiler",
    "_helper_build_command",
]
