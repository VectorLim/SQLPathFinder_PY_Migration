"""Non-executing inspection through the original ScriptHost router and tree builder."""

from __future__ import annotations

import json
import subprocess
import sys
import tempfile
from dataclasses import asdict, dataclass
from pathlib import Path

from .runtime import _spf_manager_type
from .task_inputs import TaskInput

_PROGRAM_FILE = "program.txt"
_RESULT_FILE = "program.json"


@dataclass(frozen=True, slots=True)
class TaskDescriptor:
    """One original task object: identity, lexical input and its children in the original tree."""

    block_index: int
    task_type: str
    class_name: str
    input: TaskInput
    is_control_start: bool
    is_control_end: bool
    nest_level: int
    children: tuple[TaskDescriptor, ...] = ()


@dataclass(frozen=True, slots=True)
class ProgramDescriptor:
    """Root task sequence that SPFManager.Process_Query builds for one program."""

    tasks: tuple[TaskDescriptor, ...]


def inspect_task(raw_block: str, block_index: int) -> TaskDescriptor:
    manager = _spf_manager_type()()
    return _describe(manager.GetQuery("", raw_block, block_index, False))


def inspect_program(text: str) -> ProgramDescriptor:
    """Build the original task tree in a fresh process and scratch cwd; no task is parsed or executed."""
    with tempfile.TemporaryDirectory(
        prefix="scripthost-inspect-", ignore_cleanup_errors=True
    ) as scratch:
        Path(scratch, _PROGRAM_FILE).write_text(text, encoding="utf-8")
        completed = subprocess.run(
            [sys.executable, "-m", __name__, scratch],
            cwd=scratch,
            capture_output=True,
            text=True,
            check=False,
        )
        result = Path(scratch, _RESULT_FILE)
        if not result.is_file():
            raise RuntimeError(
                f"ScriptHost inspection exited without a result (exit code {completed.returncode}): "
                f"{completed.stderr.strip()[-2000:]}"
            )
        payload = json.loads(result.read_text(encoding="utf-8"))
    if "error" in payload:
        raise ValueError(f"ScriptHost cannot build the task tree: {payload['error']}")
    return ProgramDescriptor(tuple(_from_json(task) for task in payload["tasks"]))


def _build_program(text: str) -> ProgramDescriptor:
    manager = _spf_manager_type()()
    blocks = text.split(manager.SQLFILE_DELIM)
    roots = manager.Process_Query(
        0, len(blocks), blocks, "", "", None, len(blocks), "", "", ""
    )
    return ProgramDescriptor(tuple(_describe(task) for task in roots))


def _describe(task) -> TaskDescriptor:
    return TaskDescriptor(
        task.SPFTaskItemIdx,
        task.SPFTaskType,
        type(task).__name__,
        TaskInput.parse(task.SPFTaskItem),
        task.isControlerStartTask,
        task.isControlerEndTask,
        task.nestLevel,
        tuple(_describe(child) for child in task.childTasksList),
    )


def _from_json(data: dict) -> TaskDescriptor:
    task_input = TaskInput(
        tuple(map(tuple, data["input"]["options"])), data["input"]["command"]
    )
    children = tuple(_from_json(child) for child in data["children"])
    return TaskDescriptor(**{**data, "input": task_input, "children": children})


def _main(scratch: Path) -> None:
    try:
        program = _build_program(
            scratch.joinpath(_PROGRAM_FILE).read_text(encoding="utf-8")
        )
        payload = {"tasks": [asdict(task) for task in program.tasks]}
    except Exception as error:
        payload = {"error": f"{type(error).__name__}: {error}"}
    scratch.joinpath(_RESULT_FILE).write_text(json.dumps(payload), encoding="utf-8")


if __name__ == "__main__":
    _main(Path(sys.argv[1]))
