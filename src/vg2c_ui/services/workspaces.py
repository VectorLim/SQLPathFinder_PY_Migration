from __future__ import annotations

import os
import shutil
import threading
import time
import uuid
from dataclasses import dataclass
from pathlib import Path, PurePosixPath
from typing import Literal, Protocol

from fastapi import HTTPException, UploadFile

WorkspaceFileRole = Literal["source", "data", "generated"]


class WorkspacePathError(ValueError):
    pass


@dataclass(frozen=True, slots=True)
class Workspace:
    id: str
    root: Path


@dataclass(frozen=True, slots=True)
class WorkspaceFile:
    path: str
    size_bytes: int
    modified_at: float
    role: WorkspaceFileRole
    translatable: bool


class ExecutionService(Protocol):
    """Future boundary for a separately approved workspace job runner."""

    def submit(self, workspace: Workspace, source_path: str) -> str: ...


class DisabledExecutionService:
    def submit(self, workspace: Workspace, source_path: str) -> str:
        raise RuntimeError("Generated workflow execution is disabled for this deployment.")


class WorkspaceManager:
    """Own anonymous, cookie-scoped workspaces under one persistent data root."""

    cookie_name = "vg2c_workspace"
    allowed_upload_suffixes = {".txt", ".csv", ".tab", ".dat", ".xlsx", ".xls", ".png", ".jpg", ".jpeg", ".gif", ".webp"}
    translation_source_suffixes = {".txt"}

    def __init__(
        self,
        root: Path,
        *,
        retention_seconds: int = 24 * 60 * 60,
        cleanup_interval_seconds: int = 60 * 60,
        max_upload_bytes: int = 100 * 1024 * 1024,
        max_file_count: int = 100,
        max_workspace_bytes: int = 500 * 1024 * 1024,
    ) -> None:
        self.root = Path(root).resolve()
        self.root.mkdir(parents=True, exist_ok=True)
        self.retention_seconds = retention_seconds
        self.cleanup_interval_seconds = cleanup_interval_seconds
        self.max_upload_bytes = max_upload_bytes
        self.max_file_count = max_file_count
        self.max_workspace_bytes = max_workspace_bytes
        self._next_cleanup = 0.0
        self._lock = threading.Lock()

    def resolve_or_create(self, workspace_id: str | None) -> tuple[Workspace, bool]:
        if workspace_id and _valid_workspace_id(workspace_id):
            root = self.root / workspace_id
            if root.is_dir() and not root.is_symlink():
                workspace = Workspace(workspace_id, root)
                self.touch(workspace)
                self.maybe_cleanup()
                return workspace, False
        workspace_id = uuid.uuid4().hex
        workspace = Workspace(workspace_id, self.root / workspace_id)
        workspace.root.mkdir(mode=0o700)
        (workspace.root / "inputs").mkdir()
        (workspace.root / "generated").mkdir()
        self.touch(workspace)
        self.maybe_cleanup()
        return workspace, True

    def touch(self, workspace: Workspace) -> None:
        now = time.time()
        os.utime(workspace.root, (now, now))

    def maybe_cleanup(self) -> None:
        now = time.time()
        if now < self._next_cleanup:
            return
        with self._lock:
            if now < self._next_cleanup:
                return
            self._next_cleanup = now + self.cleanup_interval_seconds
            cutoff = now - self.retention_seconds
            for candidate in self.root.iterdir():
                if (
                    not candidate.is_dir()
                    or candidate.is_symlink()
                    or not _valid_workspace_id(candidate.name)
                ):
                    continue
                if candidate.stat().st_mtime < cutoff:
                    shutil.rmtree(candidate)

    def resolve_file(self, workspace: Workspace, value: str) -> Path:
        relative = _safe_relative_path(value)
        candidate = (workspace.root / relative).resolve()
        if workspace.root not in candidate.parents:
            raise WorkspacePathError("Path is outside this workspace.")
        return candidate

    async def save_upload(
        self, workspace: Workspace, upload: UploadFile, relative_path: str
    ) -> WorkspaceFile:
        relative = _safe_relative_path(relative_path)
        if Path(relative.name).suffix.lower() not in self.allowed_upload_suffixes:
            raise WorkspacePathError("Only VG2 text and supported data files may be uploaded.")
        existing = self.list_files(workspace, prefix="inputs")
        existing_size = sum(item.size_bytes for item in self.list_files(workspace))
        target_relative = PurePosixPath("inputs") / relative
        target = self.resolve_file(workspace, target_relative.as_posix())
        if not target.exists() and len(existing) >= self.max_file_count:
            raise WorkspacePathError("Workspace upload file limit reached.")
        target.parent.mkdir(parents=True, exist_ok=True)
        written = 0
        try:
            with target.open("xb") as handle:
                while chunk := await upload.read(1024 * 1024):
                    written += len(chunk)
                    if written > self.max_upload_bytes:
                        raise WorkspacePathError("Uploaded file exceeds the workspace upload limit.")
                    if existing_size + written > self.max_workspace_bytes:
                        raise WorkspacePathError("Workspace storage limit reached.")
                    handle.write(chunk)
        except FileExistsError as exc:
            raise WorkspacePathError(f"A file already exists at {relative.as_posix()}.") from exc
        except BaseException:
            target.unlink(missing_ok=True)
            raise
        self.touch(workspace)
        return self._workspace_file(target_relative.as_posix(), target.stat())

    def list_files(self, workspace: Workspace, *, prefix: str | None = None) -> list[WorkspaceFile]:
        root = self.resolve_file(workspace, prefix) if prefix else workspace.root
        if not root.exists():
            return []
        files: list[WorkspaceFile] = []
        for path in root.rglob("*"):
            if not path.is_file() or path.is_symlink() or path.name.endswith(".vg2c-ui.json"):
                continue
            relative = path.relative_to(workspace.root).as_posix()
            files.append(self._workspace_file(relative, path.stat()))
        return sorted(files, key=lambda item: item.path)

    def classify_file(self, path: str) -> tuple[WorkspaceFileRole, bool]:
        relative = PurePosixPath(path)
        if relative.parts and relative.parts[0] == "generated":
            return "generated", False
        if relative.suffix.lower() in self.translation_source_suffixes:
            return "source", True
        return "data", False

    def _workspace_file(self, path: str, stat) -> WorkspaceFile:
        role, translatable = self.classify_file(path)
        return WorkspaceFile(
            path=path,
            size_bytes=stat.st_size,
            modified_at=stat.st_mtime,
            role=role,
            translatable=translatable,
        )


def get_workspace(request) -> Workspace:
    workspace = getattr(request.state, "workspace", None)
    if workspace is None:
        raise HTTPException(status_code=500, detail="Workspace session is unavailable.")
    return workspace


def get_workspace_manager(request) -> WorkspaceManager:
    return request.app.state.workspace_manager


def _safe_relative_path(value: str) -> PurePosixPath:
    normalized = value.replace("\\", "/")
    path = PurePosixPath(normalized)
    if not normalized or path.is_absolute() or any(part in {"", ".", ".."} for part in path.parts):
        raise WorkspacePathError("A non-empty relative path is required.")
    return path


def _valid_workspace_id(value: str) -> bool:
    return len(value) == 32 and all(character in "0123456789abcdef" for character in value)


__all__ = [
    "Workspace",
    "ExecutionService",
    "DisabledExecutionService",
    "WorkspaceFile",
    "WorkspaceFileRole",
    "WorkspaceManager",
    "WorkspacePathError",
    "get_workspace",
    "get_workspace_manager",
]
