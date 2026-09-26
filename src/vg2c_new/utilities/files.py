from __future__ import annotations

import csv
import glob
import re
import shutil
import stat
import time
import zipfile
from datetime import datetime, timedelta
from pathlib import Path
from typing import TYPE_CHECKING

import pandas as pd

from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility
from vg2c_new.utilities.csv import CsvUtility

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState


class WriteFileUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct port of ScriptHost WriteFileTask local output semantics.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: WriteFileTask.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: DIRECT PORT. Preserved: /CSV, substitution and <EOF>. Amendments: pathlib. Discarded: transport/global state.
        """
        output = command.option("CSV")
        if not output:
            raise ValueError("WRITE-FILE requires /CSV=output path.")
        path = _path(command, state, output)
        body = state.substitute(command.body)
        eof = body.upper().find("<EOF>")
        if eof >= 0:
            body = body[:eof]
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_text(body, encoding="utf-8")


class DeleteFileUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost SPFDeleteTask/Utilities.SPFDelete.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT. Preserved: comma lists, wildcard and missing no-op.
        Amendments: pathlib/glob. Intentionally discarded: BAT/DOS recursive fallback.
        """
        args = [state.substitute(v) for v in command.arguments]
        if not args:
            raise ValueError("SPFDelete requires a file or pattern.")
        values = next(csv.reader([args[0].replace("<c>", ",")], skipinitialspace=True))
        for value in values:
            raw = str(_path(command, state, value.strip()))
            matches = (
                [Path(p) for p in glob.glob(raw)] if any(ch in raw for ch in "*?[") else [Path(raw)]
            )
            for path in matches:
                if path.is_file() or path.is_symlink():
                    path.unlink(missing_ok=True)
                elif path.is_dir():
                    for child in path.iterdir():
                        if child.is_file() or child.is_symlink():
                            child.unlink(missing_ok=True)


class CopyFileUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended portable port of ScriptHost SPFCopyTask/SPFDistribute.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: SPFCopyTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.SPFDistribute/GetFilePattern.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: comma-separated sources, <c> escaping, wildcard copies,
        newest-modified folder/file tokens, destination-folder rules, and continue-on-error.
        Amendments: pathlib/glob/shutil replace COPY/BAT execution.
        Intentionally discarded: COMSPEC/service transport and console/global-abort state.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("SPFCopy requires source and destination.")
        try:
            self._copy(command, state, args[0], args[1])
        except Exception:
            if len(args) <= 2 or not _yn(args[2]):
                raise

    def _copy(
        self, command: Command, state: RuntimeState, source_value: str, dest_value: str
    ) -> None:
        source_items = [
            item.strip().replace("<c>", ",") for item in source_value.split(",") if item.strip()
        ]
        if not source_items:
            return
        dest = _path(command, state, dest_value)
        if len(source_items) > 1 and not dest.is_dir():
            raise ValueError("Multiple copy sources require an existing destination directory.")

        resolved: list[Path] = []
        for item in source_items:
            has_date_token = bool(re.search(r"<\s*(folder|file)-datelastmodified\s*>", item, re.I))
            if has_date_token and "*" in item:
                raise ValueError(
                    "Date-last-modified distribution tokens cannot be combined with '*'."
                )
            source = _path(command, state, item)
            if has_date_token:
                source = _resolve_modified_pattern(source)
            raw = str(source)
            matches = (
                [Path(p) for p in glob.glob(raw)] if any(ch in raw for ch in "*?[") else [source]
            )
            resolved.extend(path for path in matches if path.exists() and path.is_file())

        if not resolved:
            raise FileNotFoundError(source_value)
        if len(resolved) > 1 and not dest.is_dir():
            raise ValueError("Multiple copy sources require an existing destination directory.")
        if dest.is_dir() or len(resolved) > 1:
            dest.mkdir(parents=True, exist_ok=True)
            for source in resolved:
                shutil.copy2(source, dest / source.name)
        else:
            dest.parent.mkdir(parents=True, exist_ok=True)
            shutil.copy2(resolved[0], dest)


class DistributeUtility(CopyFileUtility):
    pass


class RenameFileUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost SPFRenameTask/Utilities.SPFRenameFile.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT. Preserved: missing-source no-op and TS/DAY/WW tokens.
        Amendments: pathlib.replace. Discarded: Windows lock/move loop.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("SPFRename requires source and destination.")
        raw = str(_path(command, state, args[0]))
        matches = (
            [Path(p) for p in glob.glob(raw)] if any(ch in raw for ch in "*?[") else [Path(raw)]
        )
        matches = [p for p in matches if p.exists()]
        if not matches:
            return
        if len(matches) > 1:
            raise ValueError(
                "SPFRename portable runtime supports one resolved source file per invocation."
            )
        src = matches[0]
        dest = _path(command, state, _rename_tokens(args[1], datetime.now()))
        dest.parent.mkdir(parents=True, exist_ok=True)
        if dest.exists():
            dest.unlink()
        src.replace(dest)


class AppendFileUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost AppendFileTask current delimited-file behavior.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT. Preserved: source glob append/header union/max-row tail.
        Amendments: pandas/CsvUtility. Discarded: Windows share staging.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("AppendFile requires destination and source.")
        dest = _path(command, state, args[0])
        raw = str(_path(command, state, args[1]))
        sources = (
            [Path(p) for p in glob.glob(raw)] if any(ch in raw for ch in "*?[") else [Path(raw)]
        )
        sources = [p for p in sources if p.exists() and p.is_file()]
        if not sources:
            return
        frames = [CsvUtility.read_dataframe(dest)] if dest.exists() else []
        frames.extend(CsvUtility.read_dataframe(p) for p in sources)
        result = pd.concat(frames, ignore_index=True, sort=False).fillna("")
        if len(args) > 2 and args[2].strip().isdigit() and int(args[2]) > 0:
            result = result.tail(int(args[2]))
        CsvUtility.write_dataframe(result, dest)


class RoboCopyUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Rewrite of ScriptHost RoboCopy semantic contract using shutil.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: RoboCopyTask and
        SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.SPFRoboCopy.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: REWRITE.
        Preserved: one/many patterns, retry/wait, optional lock probe, missing-source
        abort, recursive /S-/E, /MOV, and destination verification.
        Amendments: pathlib/shutil replace robocopy.exe and its exit-code table.
        Intentionally discarded: robocopy display switches and custom pass-exit-code transport.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 3:
            raise ValueError(
                "RoboCopy requires pattern, source directory and destination directory."
            )
        patterns = [
            value.strip().strip('"') for value in args[0].split(",") if value.strip().strip('"')
        ]
        src = _path(command, state, args[1])
        dst = _path(command, state, args[2])
        retries = max(1, _int(args[3], 100) if len(args) > 3 else 100)
        wait = max(0, _int(args[4], 30) if len(args) > 4 else 30)
        open_check = _yn(args[5]) if len(args) > 5 else False
        switch_text = args[6].strip().upper() if len(args) > 6 else ""
        abort_if_missing = _yn(args[7]) if len(args) > 7 else False

        if not patterns:
            return
        if not src.is_dir():
            raise FileNotFoundError(f"RoboCopy source directory not found: {src}")
        if src.resolve(strict=False) == dst.resolve(strict=False):
            return

        switches = {value for value in switch_text.split() if value}
        supported = {"/S", "/E", "/MOV", "/NP", "/IS"}
        unsupported = sorted(value for value in switches if value not in supported)
        if unsupported:
            raise RuntimeError(
                "Unsupported portable RoboCopy switch(es): " + ", ".join(unsupported)
            )
        recursive = bool(switches & {"/S", "/E"})
        move = "/MOV" in switches
        if len(patterns) > 1 or any(any(ch in pattern for ch in "*?[") for pattern in patterns):
            open_check = False

        matches: list[Path] = []
        for pattern in patterns:
            found = list(src.rglob(pattern) if recursive else src.glob(pattern))
            files = [item for item in found if item.is_file()]
            if not files and abort_if_missing:
                raise FileNotFoundError(src / pattern)
            matches.extend(files)
        if not matches:
            return

        for item in dict.fromkeys(matches):
            if open_check:
                _assert_unlocked(item)
            target = dst / (item.relative_to(src) if recursive else Path(item.name))
            target.parent.mkdir(parents=True, exist_ok=True)
            last_error: OSError | None = None
            for attempt in range(retries):
                try:
                    shutil.copy2(item, target)
                    last_error = None
                    break
                except OSError as exc:
                    last_error = exc
                    if attempt + 1 < retries and wait:
                        time.sleep(wait)
            if last_error is not None:
                raise last_error
            if abort_if_missing and not target.exists():
                raise RuntimeError(f"RoboCopy destination was not created: {target}")
            if move:
                item.unlink()


class SetFileReadOnlyUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended portable port of ScriptHost SetFileROTask.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: SetFileROTask.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: READONLY/READWRITE intent.
        Amendments: chmod replaces Windows attributes.
        Intentionally discarded: HIDDEN/SYSTEM.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("SetFileRO requires file and access mode.")
        path = _path(command, state, args[0])
        mode = args[1].upper()
        if "HIDDEN" in mode or "SYSTEM" in mode:
            raise RuntimeError(
                "HIDDEN/SYSTEM are Windows-only attributes and intentionally unsupported."
            )
        current = path.stat().st_mode
        if "READWRITE" in mode:
            path.chmod(current | stat.S_IWUSR)
        elif "READONLY" in mode:
            path.chmod(current & ~stat.S_IWUSR & ~stat.S_IWGRP & ~stat.S_IWOTH)
        else:
            raise ValueError(f"Unsupported file access mode {args[1]!r}.")


class WaitIntervalUtility(Utility):
    def __init__(self, sleeper=time.sleep):
        self._sleep = sleeper

    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct port of ScriptHost WaitIntervalTask.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: WaitIntervalTask.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: DIRECT PORT.
        Preserved: interval wait semantics.
        Amendments: injected sleeper.
        Intentionally discarded: console/global state.
        """
        args = [state.substitute(v) for v in command.arguments]
        self._sleep(max(0, _int(args[0], 10) if args else 10))


class WaitFileUtility(Utility):
    def __init__(self, sleeper=time.sleep, poll_seconds: int = 10):
        self._sleep = sleeper
        self._poll_seconds = poll_seconds

    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost WaitFileTask bounded polling.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: WaitFileTask.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: bounded polling and missing-file completion.
        Amendments: pathlib and injected sleeper.
        Intentionally discarded: Windows session probing.
        """
        args = [state.substitute(v) for v in command.arguments]
        if not args:
            raise ValueError("WaitFile requires a file path.")
        path = _path(command, state, args[0])
        polls = max(1, _int(args[1], 18) if len(args) > 1 else 18)
        for index in range(polls):
            if path.exists():
                return
            if index + 1 < polls:
                self._sleep(self._poll_seconds)


class ZipUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct portable port of current ScriptHost SPFZIP semantics.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: SPFZipTask.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: DIRECT PORT.
        Preserved: archive creation and optional source deletion.
        Amendments: stdlib zipfile.
        Intentionally discarded: helper executable transport.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("SPFZIP requires source and archive.")
        source = _path(command, state, args[0])
        archive = _path(command, state, args[1])
        delete = _yn(args[2]) if len(args) > 2 else False
        if archive.suffix.lower() != ".zip":
            archive = archive.with_suffix(".zip")
        files = (
            list(source.rglob("*"))
            if source.is_dir()
            else [Path(p) for p in glob.glob(str(source))]
        )
        archive.parent.mkdir(parents=True, exist_ok=True)
        with zipfile.ZipFile(archive, "w", zipfile.ZIP_DEFLATED) as zf:
            for item in files:
                if item.is_file():
                    zf.write(item, item.relative_to(source) if source.is_dir() else item.name)
        if delete:
            if source.is_dir():
                shutil.rmtree(source)
            elif source.exists():
                source.unlink()


class UnzipUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct portable port of current ScriptHost SPFUNZIP semantics.

        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: SPFUNZipTask.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: DIRECT PORT.
        Preserved: extraction and optional archive deletion.
        Amendments: stdlib zipfile plus zip-slip protection.
        Intentionally discarded: helper executable transport.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("SPFUNZIP requires archive and destination.")
        archive = _path(command, state, args[0])
        destination = _path(command, state, args[1])
        delete = _yn(args[2]) if len(args) > 2 else False
        destination.mkdir(parents=True, exist_ok=True)
        root = destination.resolve()
        with zipfile.ZipFile(archive) as zf:
            for member in zf.infolist():
                target = (destination / member.filename).resolve()
                if root not in target.parents and target != root:
                    raise RuntimeError(f"Unsafe ZIP member path {member.filename!r}.")
            zf.extractall(destination)
        if delete:
            archive.unlink(missing_ok=True)


def _resolve_modified_pattern(path: Path) -> Path:
    """Amended port of Utilities.GetFilePattern newest-modified path tokens.

    Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.GetFilePattern.
    Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED PORT.
    Preserved: newest modified folder/file selection and deterministic filename tie-break.
    Amendments: pathlib traversal replaces Windows path string construction.
    Intentionally discarded: console/global error side effects.
    """
    folder_token = "<folder-datelastmodified>"
    file_token = "<file-datelastmodified>"
    parts = list(path.parts)
    current = Path(path.anchor) if path.anchor else Path()
    start = 1 if path.anchor else 0
    for part in parts[start:]:
        lower = part.casefold()
        has_folder = folder_token in lower
        has_file = file_token in lower
        literal = re.sub(r"<\s*(?:folder|file)-datelastmodified\s*>", "", part, flags=re.I)
        if literal:
            current /= literal
        if has_folder:
            current = _latest_modified_child(current, directory=True)
        if has_file:
            current = _latest_modified_child(current, directory=False)
    return current


def _latest_modified_child(parent: Path, *, directory: bool) -> Path:
    if not parent.is_dir():
        raise FileNotFoundError(parent)
    candidates = [
        child for child in parent.iterdir() if (child.is_dir() if directory else child.is_file())
    ]
    if not candidates:
        kind = "folder" if directory else "file"
        raise FileNotFoundError(f"No {kind} candidates under {parent}")
    return max(candidates, key=lambda child: (child.stat().st_mtime, child.name))


def _assert_unlocked(path: Path) -> None:
    try:
        with path.open("ab"):
            pass
    except OSError as exc:
        raise RuntimeError(f"Source file appears locked: {path}") from exc


def _path(command: Command, state: RuntimeState, value: str) -> Path:
    return resolve_path(
        state.substitute(value), state, base=working_directory_for(command.option("WORKDIR"), state)
    )


def _yn(value: str) -> bool:
    return str(value).strip().upper() in {"Y", "YES", "TRUE", "1"}


def _int(value: str, default: int) -> int:
    try:
        return int(str(value).strip())
    except (TypeError, ValueError):
        return default


def _rename_tokens(value: str, now: datetime) -> str:
    result = value.replace("<TS>", now.strftime("%Y%m%d%H%M%S")).replace(
        "<DAY>", now.strftime("%Y%m%d")
    )
    return result.replace("<WW>", _intel_ww(now)) if "<WW>" in result else result


def _intel_ww(date: datetime) -> str:
    """Direct port of ScriptHost Utilities.IntelWW work-week calculation.

    Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.IntelWW.
    Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: DIRECT PORT.
    Preserved: Intel year/week boundary arithmetic.
    Amendments: locale-independent datetime arithmetic.
    Intentionally discarded: Windows locale mutation and logging globals.
    """
    d = date.date()
    year = d.year
    ww = None
    if d.month == 12:
        jan1 = d.day - 32
        dow = d.isoweekday()
        jan1 = jan1 - dow if dow < 7 else jan1
        if jan1 > -7:
            year += 1
            ww = 1
    if ww is None:
        first = datetime(year, 1, 1).date()
        dow = first.isoweekday()
        first = first - timedelta(days=dow) if dow < 7 else first
        ww = int(((d - first).days) / 7) + 1
    return str(year * 100 + ww)
