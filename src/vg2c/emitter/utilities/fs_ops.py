"""FileSystemOps - copy / rename / delete via pathlib + shutil."""

from __future__ import annotations

import shutil
from pathlib import Path

from vg2c.emitter.utilities._base import CheckedUtilitySpec
from vg2c.emitter.utilities._emit_helpers import (
    RawExpr,
    _emit_step_source,
    _step_name,
    option_to_python_expr,
    render_method_call,
    resolve_output_path,
    strip_quotes,
)
from vg2c.kind import Kind


class FileSystemOps(CheckedUtilitySpec):

    utility_name = "fs_ops"
    handles = (Kind.WRITE_FILE, Kind.FS_COPY, Kind.FS_DELETE)

    @staticmethod
    def check(options) -> tuple[Kind, str] | None:
        if options.lookup.get("WRITE-FILE", "").upper() == "Y":
            csv_value = options.lookup.get("CSV", "")
            if csv_value.lower().endswith(".py"):
                return None
            return Kind.WRITE_FILE, "/WRITE-FILE=Y"

        utilities = options.lookup.get("UTILITIES")
        if not utilities:
            return None

        first_token = utilities.strip().split(maxsplit=1)[0].strip().strip('"')
        basename = first_token.split("/")[-1].split("\\")[-1].lower()

        if "robocopy" in basename or "spfcopy" in basename:
            return Kind.FS_COPY, "/UTILITIES command maps to FS copy"
        if "spfdelete" in basename:
            return Kind.FS_DELETE, "/UTILITIES command maps to FS delete"
        return None

    @staticmethod
    def emit_block(block) -> tuple[str, str] | None:
        if block.kind is Kind.FS_COPY:
            return FileSystemOps._emit_copy_block(block)
        if block.kind is Kind.FS_DELETE:
            return FileSystemOps._emit_delete_block(block)

        stmt = render_method_call(
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

    @staticmethod
    def _emit_copy_block(block) -> tuple[str, str]:
        argv = FileSystemOps._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "robocopy" in basename:
            stmt = FileSystemOps._emit_robocopy(argv)
        elif "spfcopy" in basename:
            stmt = FileSystemOps._emit_spf_copy(argv)
        else:
            return _emit_step_source(
                _step_name(block, "fs_copy"),
                ["pass  # TODO: unsupported FS copy utility command"],
            )
        return _emit_step_source(_step_name(block, "fs_copy"), [stmt])

    @staticmethod
    def _emit_delete_block(block) -> tuple[str, str]:
        argv = FileSystemOps._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "spfdelete" not in basename:
            return _emit_step_source(
                _step_name(block, "fs_delete"),
                ["pass  # TODO: unsupported FS delete utility command"],
            )
        stmt = FileSystemOps._emit_spf_delete(argv)
        return _emit_step_source(_step_name(block, "fs_delete"), [stmt])

    @staticmethod
    def _emit_robocopy(argv: list[str]) -> str:
        # RoboCopy.va arg layout: <file_name> <source_dir> <dest_dir> [...]
        file_name = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        source_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        dest_dir = option_to_python_expr(argv[3]) if len(argv) > 3 else repr(".")
        src_expr = RawExpr(f"str(Path({source_dir}) / {file_name})")
        dst_expr = RawExpr(dest_dir)
        return render_method_call(
            "fs_ops",
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    @staticmethod
    def _emit_spf_copy(argv: list[str]) -> str:
        # SPFCopy.bat arg layout: <source_path> <dest_dir> [recurse]
        src = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        dst_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        src_expr = RawExpr(src)
        dst_expr = RawExpr(f"str(Path({dst_dir}) / Path({src}).name)")
        return render_method_call(
            "fs_ops",
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    @staticmethod
    def _emit_spf_delete(argv: list[str]) -> str:
        raw = strip_quotes(argv[1]) if len(argv) > 1 else ""
        items = [p.strip() for p in raw.split(",") if p.strip()]
        paths_expr = RawExpr(
            "[" + ", ".join(option_to_python_expr(p) for p in items) + "]"
        )
        return render_method_call(
            "fs_ops",
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
                if recurse:
                    shutil.rmtree(path, ignore_errors=True)
            else:
                path.unlink(missing_ok=True)
