from __future__ import annotations

import ast
from typing import Any

from vg2c.emitter.models import BlockEmission
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility


class PythonEmbed(EmitterUtility):
    """Utility class for directly embedding Python script blocks."""

    utility_name = "python_embed"
    handles = (Kind.PYTHON_EMBED,)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if options.lookup.get("WRITE-FILE", "").upper() != "Y":
            return None

        csv_value = options.lookup.get("CSV", "")
        if csv_value.lower().endswith(".py"):
            return Kind.PYTHON_EMBED, "/WRITE-FILE=Y targeting .py script"
        return None

    @classmethod
    def emit_block(cls, block: Any, *, macros: str = "macros") -> BlockEmission:
        body = ast.parse(block.resolved_body).body
        if any(isinstance(item, ast.Pass) for item in body):
            raise ValueError("Unsupported utility operation")
        if any(
            isinstance(item, ast.Name) and item.id == "ctx"
            for statement in body
            for item in ast.walk(statement)
        ):
            raise ValueError(
                "Embedded Python using the retired ctx API must use explicit runtime functions and workdir"
            )
        if any(cls._top_level_exit(statement) for statement in body):
            raise ValueError("Embedded Python return/yield must be inside an authored function")
        return BlockEmission((block.resolved_body,))

    @staticmethod
    def _top_level_exit(node):
        if isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef, ast.ClassDef)):
            return False
        return isinstance(node, (ast.Return, ast.Yield, ast.YieldFrom)) or any(
            PythonEmbed._top_level_exit(child) for child in ast.iter_child_nodes(node)
        )
