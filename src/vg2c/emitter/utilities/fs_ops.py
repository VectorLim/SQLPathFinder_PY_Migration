"""FileSystemOps - copy / rename / delete via pathlib + shutil."""

from __future__ import annotations

import shutil
from pathlib import Path

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._emit_helpers import (
    RawExpr,
    _emit_step_source,
    _step_name,
    option_to_python_expr,
    resolve_output_path,
)
from vg2c.frontend.models import Kind


class FileSystemOps(UtilitySpec):

    utility_name = "fs_ops"
    handles = (Kind.WRITE_FILE, Kind.FS_COPY, Kind.FS_DELETE)

    @classmethod
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        if block.kind is Kind.FS_COPY:
            return cls._emit_copy_block(ctx, block)
        if block.kind is Kind.FS_DELETE:
            return cls._emit_delete_block(ctx, block)

        stmt = ctx.render_method_call(
            "ctx",
            "write_file",
            kwargs={
                "path": resolve_output_path(block),
                "template": block.resolved_body,
            },
        )
        return _emit_step_source(_step_name(block, "write_file"), [stmt])

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        if not text:
            return []
        return text.split()

    @classmethod
    def _emit_copy_block(cls, ctx, block) -> tuple[str, str]:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "robocopy" in basename:
            stmt = cls._emit_robocopy(ctx, argv)
        elif "spfcopy" in basename:
            stmt = cls._emit_spf_copy(ctx, argv)
        else:
            return _emit_step_source(
                _step_name(block, "fs_copy"),
                ["pass  # TODO: unsupported FS copy utility command"],
            )
        return _emit_step_source(_step_name(block, "fs_copy"), [stmt])

    @classmethod
    def _emit_delete_block(cls, ctx, block) -> tuple[str, str]:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "spfdelete" not in basename:
            return _emit_step_source(
                _step_name(block, "fs_delete"),
                ["pass  # TODO: unsupported FS delete utility command"],
            )
        stmt = cls._emit_spf_delete(ctx, argv)
        return _emit_step_source(_step_name(block, "fs_delete"), [stmt])

    @classmethod
    def _emit_robocopy(cls, ctx, argv: list[str]) -> str:
        # RoboCopy.va arg layout: <file_name> <source_dir> <dest_dir> [...]
        file_name = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        source_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        dest_dir = option_to_python_expr(argv[3]) if len(argv) > 3 else repr(".")
        src_expr = RawExpr(f"str(Path({source_dir}) / {file_name})")
        dst_expr = RawExpr(dest_dir)
        return ctx.render_method_call(
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
        return ctx.render_method_call(
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
        return ctx.render_method_call(
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
