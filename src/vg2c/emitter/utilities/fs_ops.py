"""FileSystemOps — copy / rename / delete via pathlib + shutil."""

from __future__ import annotations

import shutil
from pathlib import Path

from vg2c.emitter.semtypes import (
    OutputPath,
    RawExpr,
    WriteFileTemplate,
    option_to_python_expr,
)
from vg2c.emitter.utilities._base import UtilityShape, UtilitySpec
from vg2c.emitter.utilities._emit_helpers import (
    _emit_step_source,
    _step_name,
    render_method_call,
)
from vg2c.emitter.utilities._registry import register_utility
from vg2c.frontend.models import Kind


@register_utility
class FileSystemOps(UtilitySpec):

    utility_name = "fs_ops"
    handles = (Kind.WRITE_FILE,)
    utility_imports = (
        "import shutil",
        "from pathlib import Path",
    )

    @classmethod
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        stmt = render_method_call(
            ctx,
            "ctx",
            "write_file",
            kwargs={
                "path": OutputPath.extract(block, None),
                "template": WriteFileTemplate.extract(block, None),
            },
        )
        return _emit_step_source(_step_name(block, "write_file"), [stmt])

    @classmethod
    def _emit_robocopy(cls, ctx, argv: list[str]) -> str:
        # RoboCopy.va arg layout: <file_name> <source_dir> <dest_dir> [...]
        file_name = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        source_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        dest_dir = option_to_python_expr(argv[3]) if len(argv) > 3 else repr(".")
        src_expr = RawExpr(f"str(Path({source_dir}) / {file_name})")
        dst_expr = RawExpr(dest_dir)
        return render_method_call(
            ctx,
            cls.utility_name,
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    @classmethod
    def _emit_spf_copy(cls, ctx, argv: list[str]) -> str:
        # SPFCopy.bat arg layout: <source_path> <dest_dir> [recurse]
        src = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        dst_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        src_expr = RawExpr(src)
        dst_expr = RawExpr(f"str(Path({dst_dir}) / Path({src}).name)")
        return render_method_call(
            ctx,
            cls.utility_name,
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    @classmethod
    def _emit_spf_delete(cls, ctx, argv: list[str]) -> str:
        raw = argv[1] if len(argv) > 1 else ""
        items = [p.strip() for p in raw.split(",") if p.strip()]
        paths_expr = RawExpr(
            "[" + ", ".join(option_to_python_expr(p) for p in items) + "]"
        )
        return render_method_call(
            ctx,
            cls.utility_name,
            "delete",
            kwargs={"paths": paths_expr},
        )

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


FileSystemOps.utility_shapes = (
    UtilityShape(
        name="robocopy",
        contains=("robocopy",),
        emit=FileSystemOps._emit_robocopy,
    ),
    UtilityShape(
        name="spf-copy",
        contains=("spfcopy",),
        emit=FileSystemOps._emit_spf_copy,
    ),
    UtilityShape(
        name="spf-delete",
        contains=("spfdelete",),
        emit=FileSystemOps._emit_spf_delete,
    ),
)
