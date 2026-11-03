"""Observer bridge data types and frame validation."""

from __future__ import annotations

import json
from dataclasses import dataclass

from ..models import ObserverSnapshot, Selection

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
    active_control = data.get("activeControl", "unknown")
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
    if not isinstance(active_control, str) or active_control not in {
        "editor",
        "designer",
        "unknown",
    }:
        raise ValueError("Observer activeControl is invalid.")
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
    if not isinstance(raw_values, list) or not all(
        isinstance(item, str) for item in raw_values
    ):
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
        and all(
            isinstance(item, int) and not isinstance(item, bool) for item in raw_bounds
        )
    ):
        bounds = tuple(raw_bounds)  # type: ignore[assignment]
    else:
        raise ValueError("Observer bounds must be a four-integer list or null.")
    if designer_bounds_value is None:
        designer_bounds = None
    elif (
        isinstance(designer_bounds_value, list)
        and len(designer_bounds_value) == 4
        and all(
            isinstance(item, int) and not isinstance(item, bool)
            for item in designer_bounds_value
        )
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
        active_control=active_control,
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
    if (
        not isinstance(title, str)
        or not isinstance(designer_fingerprint, str)
        or not isinstance(source, str)
    ):
        raise ValueError("Refresh title and sourceText must be strings.")
    if len(source) > 200_000:
        raise ValueError("Refresh sourceText exceeds the helper bound.")
    if not isinstance(status, str) or status not in _REFRESH_STATUS:
        raise ValueError("Refresh status is invalid.")
    if error is not None and not isinstance(error, str):
        raise ValueError("Refresh error must be a string or null.")
    if status == "ok" and (
        target_pid is None or target_pid <= 0 or not title or not source
    ):
        raise ValueError(
            "Successful refresh must include a target PID, designer title, and source text."
        )
    return RefreshResult(target_pid, title, designer_fingerprint, source, status, error)


def parse_targets(payload: str) -> tuple[NativeTarget, ...]:
    if not isinstance(payload, str) or len(payload) > _MAX_OUTPUT:
        raise ValueError("Target frame exceeds the maximum supported size.")
    try:
        data = json.loads(payload)
    except json.JSONDecodeError as exc:
        raise ValueError(f"Target frame is not valid JSON: {exc.msg}") from exc
    if not isinstance(data, dict) or data.get("status") != "ok":
        raise ValueError(
            str(data.get("error", "Target discovery failed."))
            if isinstance(data, dict)
            else "Target frame must be an object."
        )
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
            isinstance(pid, bool)
            or not isinstance(pid, int)
            or pid <= 0
            or isinstance(hwnd, bool)
            or not isinstance(hwnd, int)
            or hwnd <= 0
            or not isinstance(title, str)
            or not isinstance(document, str)
            or not title
            or not document
        ):
            raise ValueError("Target entry has invalid identity fields.")
        targets.append(NativeTarget(pid, hwnd, title, document))
    return tuple(targets)
