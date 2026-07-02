"""FileSystemOps — copy / rename / delete via pathlib + shutil."""

from __future__ import annotations

import shutil
from pathlib import Path

from vg2c.emitter.codegen import (
    FunctionDef,
    PyExpr,
    emit_call,
    python_literal_for_option,
    register_call_embed,
)
from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import register_utility


@register_utility
class FileSystemOps(UtilitySpec):

    utility_name = "fs_ops"
    utility_imports = (
        "import shutil",
        "from pathlib import Path",
    )
    utility_command_contains = (
        ("robocopy", ("robocopy",)),
        ("spf-copy", ("spfcopy",)),
        ("spf-delete", ("spfdelete",)),
    )

    @classmethod
    def emit(
        cls,
        ctx,
        block,
        dispatched,
    ) -> tuple[str, str]:
        shape = dispatched.shape
        argv = dispatched.argv
        call = None

        if shape in {"robocopy", "spf-copy"}:
            filename = (
                python_literal_for_option(argv[1])
                if len(argv) > 1
                else PyExpr.literal("")
            )
            src_dir = (
                python_literal_for_option(argv[2])
                if len(argv) > 2
                else PyExpr.literal("")
            )
            dst = (
                python_literal_for_option(argv[3])
                if len(argv) > 3
                else PyExpr.literal(".")
            )
            src = PyExpr.raw(f"str(Path({src_dir.source}) / {filename.source})")
            call = emit_call(FileSystemOps.copy, src=src, dst=dst)

        elif shape == "spf-delete":
            raw = argv[1] if len(argv) > 1 else ""
            items = [p.strip() for p in raw.split(",") if p.strip()]
            paths_expr = PyExpr.list_of([python_literal_for_option(p) for p in items])
            call = emit_call(FileSystemOps.delete, paths=paths_expr)

        func_name = FunctionDef.name_for(block, "utility")
        if call is None:
            fdef = FunctionDef.from_body(
                func_name,
                [f"pass  # TODO: utility shape not translated: {shape}"],
            )
            return fdef.source, fdef.call_site

        register_call_embed(ctx, call)
        fdef = FunctionDef.from_call(func_name, call)
        return fdef.source, fdef.call_site

    def copy(self, src: str | Path, dst: str | Path, recurse: bool = False) -> None:
        src, dst = Path(src), Path(dst)
        dst.parent.mkdir(parents=True, exist_ok=True)
        if src.is_dir():
            shutil.copytree(src, dst, dirs_exist_ok=True)
        else:
            shutil.copy2(src, dst)

    def rename(self, src: str | Path, dst: str | Path) -> None:
        Path(src).replace(Path(dst))

    def delete(self, paths: list[str | Path], recurse: bool = False) -> None:
        for p in paths:
            path = Path(p)
            if path.is_dir():
                # if recurse:
                # shutil.rmtree(path, ignore_errors=True)
                pass
            else:
                # path.unlink(missing_ok=True)
                pass
