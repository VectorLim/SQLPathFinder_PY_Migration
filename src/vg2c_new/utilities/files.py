from __future__ import annotations

import csv
import glob
import re
from io import StringIO
from pathlib import Path

from vg2c_new.model import Command
from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.runtime import RuntimeState
from vg2c_new.utilities.base import Utility


class WriteFileUtility(Utility):
    """Direct/amended port of ScriptHost WriteFileTask.executeTaskCommand.

    Original: SPSQL3_py/SPFLib/SPFSQL3.py :: WriteFileTask.executeTaskCommand,
    backed by SPFUtilities/utils.py :: Utilities.DoCreateFileA.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED_PORT.
    Preserved: /CSV destination, body content, case-insensitive <EOF> truncation,
    empty-body file creation, dynamic substitution before use.
    Amendments: UTF-8 pathlib local I/O with explicit missing-/CSV error.
    Discarded: terminal \\TT mode, retry sleeps, console/global abort side effects.
    """

    def apply(self, command: Command, state: RuntimeState) -> None:
        output = command.option("CSV")
        if not output:
            raise _execution_error(command, "WRITE-FILE requires /CSV in the direct runtime.")
        base = working_directory_for(command.option("WORKDIR"), state)
        path = resolve_path(output, state, base=base)
        body = state.substitute(command.body)
        match = re.search(r"<EOF>", body, flags=re.IGNORECASE)
        if match:
            body = body[: match.start()]
        path.write_text(body, encoding="utf-8")


class DeleteFileUtility(Utility):
    """Amended portable port of SPFDeleteTask/Utilities.SPFDelete.

    Original: SPSQL3_py/SPFLib/SPFSQL3.py :: SPFDeleteTask.executeTaskCommand
    and SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.SPFDelete.
    Reference commit: 8ddd5e6463b43834d769057be48041ec657f0f9d.
    Port mode: AMENDED_PORT.
    Preserved: quoted comma-separated sources, <c> comma escape, wildcards,
    directory-contents deletion, missing targets being non-fatal, Y/YES quiet flag.
    Amendments: pathlib/glob rather than COMSPEC DEL; local filesystem only.
    Discarded: prompts, /F shell flags, Windows command-shell behavior.
    """

    def apply(self, command: Command, state: RuntimeState) -> None:
        if len(command.arguments) != 2:
            raise _execution_error(command, "SPFDelete expects source-list and quiet-mode arguments.")
        raw_sources = state.substitute(command.arguments[0])
        # Parsed for parity even though the direct runtime is non-interactive.
        _quiet = command.arguments[1].strip().upper() in {"Y", "YES"}
        try:
            sources = next(csv.reader(StringIO(raw_sources), delimiter=",", skipinitialspace=True))
        except (csv.Error, StopIteration) as exc:
            raise _execution_error(command, f"Invalid delete source list: {exc}") from exc

        base = working_directory_for(command.option("WORKDIR"), state)
        for source in sources:
            source = re.sub(r"<c>", ",", source.strip('" '), flags=re.IGNORECASE)
            if not source:
                continue
            pattern = str(resolve_path(source, state, base=base))
            matches = [Path(item) for item in glob.glob(pattern)] if glob.has_magic(pattern) else [Path(pattern)]
            for path in matches:
                if path.is_file() or path.is_symlink():
                    try:
                        path.unlink()
                    except FileNotFoundError:
                        pass
                elif path.is_dir():
                    for child in path.iterdir():
                        if child.is_file() or child.is_symlink():
                            try:
                                child.unlink()
                            except FileNotFoundError:
                                pass


def _execution_error(command: Command, message: str) -> RuntimeError:
    return RuntimeError(f"{command.span.location}: {message}")
