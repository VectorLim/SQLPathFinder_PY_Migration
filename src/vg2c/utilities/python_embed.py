from __future__ import annotations

import ast
from pathlib import Path
from typing import Any

from vg2c.emitter.models import BlockEmission
from vg2c.kind import Kind
from vg2c.project_paths import project_name
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import resolve_output_path


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
        name = f"python/step_{block.index:03d}_{project_name(Path(resolve_output_path(block)).stem)}.py"
        uses_macros = any(
            isinstance(item, ast.Name) and item.id == "macros"
            for statement in body
            for item in ast.walk(statement)
        )
        extra = f", macros={macros}" if uses_macros else ""
        source = block.resolved_body.rstrip("\n") + "\n"
        return BlockEmission(
            (f"job.run_python({name!r}{extra})",),
            assets=((name, source),),
        )

    @staticmethod
    def _top_level_exit(node):
        if isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef, ast.ClassDef)):
            return False
        return isinstance(node, (ast.Return, ast.Yield, ast.YieldFrom)) or any(
            PythonEmbed._top_level_exit(child) for child in ast.iter_child_nodes(node)
        )
