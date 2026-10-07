"""Internal, non-executing inspection through the original ScriptHost router."""

from dataclasses import dataclass

from .runtime import _spf_manager_type


@dataclass(frozen=True, slots=True)
class TaskDescriptor:
    block_index: int
    task_type: str
    class_name: str
    is_control_start: bool
    is_control_end: bool
    nest_level: int


def inspect_task(raw_block: str, block_index: int) -> TaskDescriptor:
    manager = _spf_manager_type()()
    task = manager.GetQuery("", raw_block, block_index, False)
    return TaskDescriptor(
        block_index, task.SPFTaskType, type(task).__name__,
        task.isControlerStartTask, task.isControlerEndTask, task.nestLevel,
    )
