from __future__ import annotations

import difflib
import glob
import re
from collections.abc import Callable
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


class RowsInFileUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct port of ScriptHost RowsInFileTask row-count semantics.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: RowsInFileTask.executeTaskCommand.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: DIRECT PORT.
        Preserved: missing/error=-1, header-excluding count and optional ZIP member.
        Amendments: CsvUtility/pathlib replace ScriptHost globals. Intentionally discarded: console/global-abort state.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("{ROWS-IN-FILE} requires file and destination variable.")
        path = _path(command, state, args[0])
        limit = _yn(args[2]) if len(args) > 2 else False
        member = args[3] if len(args) > 3 and args[3].strip() else None
        try:
            value = (
                CsvUtility.row_count(path, limit_to_one=limit, archive_name=member)
                if path.exists()
                else -1
            )
        except Exception:
            value = -1
        state.set_global(args[1], value)


class ValueInFileUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct port of ScriptHost ValueInFileTask first-row value semantics.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: ValueInFileTask.executeTaskCommand.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: DIRECT PORT.
        Preserved: case-insensitive header lookup, first row and EMPTY/ERROR sentinels.
        Amendments: CsvUtility/RuntimeState. Intentionally discarded: MemTable/process-environment mutation.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 3:
            raise ValueError("{VALUE-IN-FILE} requires file, column and destination variable.")
        path = _path(command, state, args[0])
        try:
            value = CsvUtility.first_value(path, args[1]) if path.exists() else "ERROR"
        except Exception:
            value = "ERROR"
        state.set_global(args[2], value)


class AgeOfFileUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct port of ScriptHost AgeOfFileTask portable behavior."""
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("{AGE-OF-FILE} requires file and destination variable.")
        path = _path(command, state, args[0])
        unit = args[2].strip().upper() if len(args) > 2 and args[2].strip() else "HOURS"
        if not path.exists():
            state.set_global(args[1], -1)
            return
        divisors = {
            "SECONDS": 1,
            "SECOND": 1,
            "MINUTES": 60,
            "MINUTE": 60,
            "HOURS": 3600,
            "HOUR": 3600,
            "DAYS": 86400,
            "DAY": 86400,
        }
        if unit not in divisors:
            raise ValueError(f"Unsupported file-age unit {unit!r}.")
        state.set_global(
            args[1], max(0.0, datetime.now().timestamp() - path.stat().st_mtime) / divisors[unit]
        )


class DateOfFileUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Direct port of ScriptHost DateOfFileTask mtime behavior."""
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("{DATE-OF-FILE} requires file and destination variable.")
        path = _path(command, state, args[0])
        value = (
            -1
            if not path.exists()
            else datetime.fromtimestamp(path.stat().st_mtime).strftime("%Y-%m-%d %H:%M:%S")
        )
        state.set_global(args[1], value)


class UpdateTimeUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost Utilities.Do_Update_Time.
        Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Do_Update_Time.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: site-time CSV and standard incremental offset columns.
        Amendments: RuntimeState replaces gMySPFJobTime. Intentionally discarded: .spf$data/global abort state.
        """
        args = [state.substitute(v) for v in command.arguments]
        if not args:
            raise ValueError("{UPDATE-TIME} requires an output file.")
        text = state.lookup("SPF_SITE_TIME") or state.lookup("SPF_JOB_DT")
        if not text:
            raise RuntimeError("No site time is available; execute {GET-SITE-TIME} first.")
        dt = datetime.strptime(text, "%Y-%m-%d %H:%M:%S")
        midnight = dt.replace(hour=0, minute=0, second=0, microsecond=0)
        values = {
            "last_Date": dt,
            "Last_Date-15m": dt - timedelta(minutes=15),
            "Last_Date-30m": dt - timedelta(minutes=30),
            "Last_Date-45m": dt - timedelta(minutes=45),
            "Last_Date-60m": dt - timedelta(minutes=60),
            "Last_Date-90m": dt - timedelta(minutes=90),
            "Last_Date-120m": dt - timedelta(minutes=120),
            "Last_Date-1d": dt - timedelta(days=1),
            "Last_Date-2d": dt - timedelta(days=2),
            "Last_Date-7d": dt - timedelta(days=7),
            "Last_Date-6h": dt - timedelta(hours=6),
            "Last_Date-8h": dt - timedelta(hours=8),
            "Last_Date-12h": dt - timedelta(hours=12),
            "Last_Date-1d-Midnight": midnight - timedelta(days=1),
            "Last_Date-2d-Midnight": midnight - timedelta(days=2),
            "Last_Date-7d-Midnight": midnight - timedelta(days=7),
        }
        CsvUtility.write_dataframe(
            pd.DataFrame([{k: v.strftime("%Y-%m-%d %H:%M:%S") for k, v in values.items()}]),
            _path(command, state, args[0]),
        )


class GetFilesUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost GetFilesTask portable enumeration semantics.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: GetFilesTask.executeTaskCommand.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT. Preserved: wildcard/directory enumeration and metadata CSV shape.
        Amendments: glob/pathlib. Intentionally discarded: Windows-only attributes.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("{GET-FILES} requires input pattern and output CSV.")
        raw = _path(command, state, args[0])
        recursive = _yn(args[2]) if len(args) > 2 else False
        if any(ch in str(raw) for ch in "*?["):
            candidates = [Path(p) for p in glob.glob(str(raw), recursive=recursive)]
        elif raw.is_dir():
            candidates = list(raw.rglob("*") if recursive else raw.glob("*"))
        else:
            candidates = [raw] if raw.exists() else []
        rows = []
        for path in sorted((p for p in candidates if p.is_file()), key=lambda p: str(p).casefold()):
            st = path.stat()
            rows.append(
                {
                    "Path": str(path.parent),
                    "Filename": path.name,
                    "Last_Modified_Date": datetime.fromtimestamp(st.st_mtime).strftime(
                        "%Y-%m-%d %H:%M:%S"
                    ),
                    "Last_Access_Date": datetime.fromtimestamp(st.st_atime).strftime(
                        "%Y-%m-%d %H:%M:%S"
                    ),
                    "Creation_Date": datetime.fromtimestamp(st.st_ctime).strftime(
                        "%Y-%m-%d %H:%M:%S"
                    ),
                    "Size": st.st_size,
                }
            )
        columns = [
            "Path",
            "Filename",
            "Last_Modified_Date",
            "Last_Access_Date",
            "Creation_Date",
            "Size",
        ]
        CsvUtility.write_dataframe(
            pd.DataFrame(rows, columns=columns), _path(command, state, args[1])
        )


class FileCompareUtility(Utility):
    def __init__(self, notifier: Callable[[str], None] | None = None):
        self._notifier = notifier

    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost FileCompareTask without DOS FC.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: FileCompareTask.executeTaskCommand.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT. Preserved: case/timestamp ignore, mismatch cap and continue.
        Amendments: difflib. Intentionally discarded: COMSPEC/FC execution.
        """
        args = [state.substitute(v) for v in command.arguments]
        if len(args) < 2:
            raise ValueError("{FILE-COMPARE} requires two files.")
        left, right = _path(command, state, args[0]), _path(command, state, args[1])
        ignore_case = _yn(args[2]) if len(args) > 2 else False
        ignore_ts = _yn(args[3]) if len(args) > 3 else False
        maximum = _integer(args[4], 20) if len(args) > 4 else 20
        cont = _yn(args[5]) if len(args) > 5 else False
        try:
            a = left.read_text(encoding="utf-8-sig", errors="replace").splitlines()
            b = right.read_text(encoding="utf-8-sig", errors="replace").splitlines()
            if ignore_ts:
                pat = re.compile(r"\b\d{4}-\d{2}-\d{2}[ T]\d{2}:\d{2}:\d{2}\b")
                a = [pat.sub("<TIMESTAMP>", x) for x in a]
                b = [pat.sub("<TIMESTAMP>", x) for x in b]
            if ignore_case:
                a = [x.casefold() for x in a]
                b = [x.casefold() for x in b]
            if a == b:
                return
            message = "\n".join(
                list(
                    difflib.unified_diff(a, b, fromfile=str(left), tofile=str(right), lineterm="")
                )[: max(1, maximum)]
            )
            if self._notifier:
                self._notifier(message)
            if not cont:
                raise RuntimeError(message or "Files differ.")
        except Exception:
            if cont:
                return
            raise


def _path(command: Command, state: RuntimeState, value: str) -> Path:
    return resolve_path(value, state, base=working_directory_for(command.option("WORKDIR"), state))


def _yn(value: str) -> bool:
    return str(value).strip().upper() in {"Y", "YES", "TRUE", "1"}


def _integer(value: str, default: int) -> int:
    try:
        return int(str(value).strip())
    except (TypeError, ValueError):
        return default
