"""Emit one plain run() from the original ScriptHost task tree."""

from __future__ import annotations

import ast
import re
from collections.abc import Sequence
from contextlib import contextmanager
from typing import NoReturn

from scripthost_portable.task_introspection import ProgramDescriptor, TaskDescriptor
from vg2c.diagnostics import CompileError
from vg2c.emitter.controls import emit_control
from vg2c.emitter.indent_writer import IndentWriter
from vg2c.emitter.models import EmittedBlock, EmittedScript, SourceRange
from vg2c.emitter.tasks import Argument, leaf_call
from vg2c.frontend.models import SourceSpan

_LINE_LIMIT = 100
_KEYWORD = re.compile(r"[A-Za-z_]\w*=")
_MAIN_GUARD = (
    '\n\nif __name__ == "__main__":\n'
    '    raise SystemExit("Use python -m scripthost_portable.launcher <job.py> --workdir <directory>.")\n'
)


class Emitter:
    def __init__(self, spans: Sequence[SourceSpan]):
        self.spans = spans
        self.writer = IndentWriter()
        self.records: dict[int, tuple[TaskDescriptor, ...]] = {}  # first line of a call -> its blocks
        self.uses_controls = False
        self._names: list[str] = []

    def fail(self, code: str, message: str, task: TaskDescriptor) -> NoReturn:
        raise CompileError(code, message, self.spans[task.block_index], task.block_index)

    def write(self, line: str) -> None:
        self.writer.write(line)

    def push(self) -> None:
        self.writer.push_indent()

    def pop(self) -> None:
        self.writer.pop_indent()

    @contextmanager
    def scope(self, base: str):
        """A scope variable name unique among the enclosing scopes."""
        depth = sum(1 for name in self._names if name.rstrip("_0123456789") == base)
        name = base if depth == 0 else f"{base}_{depth + 1}"
        self._names.append(name)
        try:
            yield name
        finally:
            self._names.pop()

    def call(self, head: str, arguments: list[Argument], blocks: Sequence[TaskDescriptor], suffix: str = "") -> None:
        self.uses_controls |= head.startswith("with controls.")
        self.records[self.writer.line_number] = tuple(blocks)
        flat = f"{head}({', '.join(argument[0] for argument in arguments)}){suffix}"
        if all(len(argument) == 1 for argument in arguments) and "\n" not in flat \
                and self.writer.indent_depth * 4 + len(flat) <= _LINE_LIMIT:
            self.write(flat)
            return
        self.write(head + "(")
        self.push()
        positional = [argument[0] for argument in arguments if len(argument) == 1 and not _KEYWORD.match(argument[0])]
        joined = ", ".join(positional) + ","
        if len(positional) > 1 and "\n" not in joined and self.writer.indent_depth * 4 + len(joined) <= _LINE_LIMIT:
            self.write(joined)
            arguments = arguments[len(positional):]
        for argument in arguments:
            for line in argument[:-1]:
                self.write(line)
            self.write(argument[-1] + ",")
        self.pop()
        self.write(")" + suffix)

    def statements(self, tasks: Sequence[TaskDescriptor], target: str) -> None:
        index = 0
        while index < len(tasks):
            task = tasks[index]
            following = tasks[index + 1] if index + 1 < len(tasks) else None
            if task.is_control_start:
                index += 1 + emit_control(self, task, following, target)
                continue
            method, arguments = leaf_call(task)
            self.call(f"{target}.{method}", arguments, (task,))
            index += 1

    def body(self, tasks: Sequence[TaskDescriptor], target: str) -> None:
        if tasks:
            self.statements(tasks, target)
        else:
            self.write("pass")

    def block(self, tasks: Sequence[TaskDescriptor], target: str) -> None:
        self.push()
        self.body(tasks, target)
        self.pop()


def emit(program: ProgramDescriptor, spans: Sequence[SourceSpan]) -> EmittedScript:
    if not program.tasks:
        raise ValueError("Cannot compile an empty job.")
    emitter = Emitter(spans)
    emitter.write("def run():")
    emitter.block(program.tasks, "script")
    names = "controls, script" if emitter.uses_controls else "script"
    api_import = f"from scripthost_portable.script_api import {names}"
    header = api_import + "\n\n\n"
    source = header + emitter.writer.source() + _MAIN_GUARD
    shift = header.count("\n")
    records = {line + shift: blocks for line, blocks in emitter.records.items()}
    return EmittedScript(source, (api_import,), _metadata(source, records, spans))


def _metadata(source: str, records: dict[int, tuple[TaskDescriptor, ...]],
              spans: Sequence[SourceSpan]) -> tuple[EmittedBlock, ...]:
    lines = source.splitlines(keepends=True)
    offsets = [0]
    for line in lines:
        offsets.append(offsets[-1] + len(line))

    def offset(line: int, column: int) -> int:
        # AST columns count UTF-8 bytes; ranges count Python characters.
        return offsets[line - 1] + len(lines[line - 1].encode("utf-8")[:column].decode("utf-8"))

    first_calls: dict[int, ast.Call] = {}
    for node in ast.walk(ast.parse(source)):
        if isinstance(node, ast.Call) and node.lineno in records:
            known = first_calls.get(node.lineno)
            if known is None or node.col_offset < known.col_offset:
                first_calls[node.lineno] = node
    emitted = []
    for line, node in sorted(first_calls.items()):
        source_range = SourceRange(offset(node.lineno, node.col_offset), offset(node.end_lineno, node.end_col_offset))
        text = source[source_range.start_offset:source_range.end_offset]
        for task in records[line]:
            emitted.append(EmittedBlock(task.block_index, task.class_name, text, source_range, spans[task.block_index]))
    return tuple(sorted(emitted, key=lambda block: block.block_index))
