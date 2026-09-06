"""FileSystemOps - copy / rename / delete via pathlib + shutil."""

from __future__ import annotations

import shutil
from pathlib import Path

from vg2c.emitter.models import CodeExpr, emittable
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import (
    resolve_output_path,
    resolve_path,
    split_utility_command,
    strip_quotes,
)
from vg2c.utilities.macro_state import MacroState


class FileSystemOps(EmitterUtility):
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

        if "robocopy" in basename or "spfcopy" in basename or "spfrename" in basename:
            return Kind.FS_COPY, "/UTILITIES command maps to FS copy"
        if "spfdelete" in basename:
            return Kind.FS_DELETE, "/UTILITIES command maps to FS delete"
        return None

    @classmethod
    def emit_block(cls, block) -> tuple[str, list[str]] | None:
        if block.kind is Kind.FS_COPY:
            return cls._emit_copy_block(block)
        if block.kind is Kind.FS_DELETE:
            return cls._emit_delete_block(block)

        from vg2c.utilities.pipeline_context import PipelineContext

        stmt = PipelineContext.write_file.render(
            path=resolve_output_path(block),
            template=block.resolved_body,
        )
        return "write_file", [stmt]

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        return split_utility_command(text)

    @classmethod
    def _emit_copy_block(cls, block) -> tuple[str, list[str]]:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "robocopy" in basename:
            stmt = cls._emit_robocopy(argv)
        elif "spfcopy" in basename:
            stmt = cls._emit_spf_copy(argv)
        elif "spfrename" in basename:
            stmt = cls._emit_spf_rename(argv)
        else:
            return "fs_copy", ["pass  # TODO: unsupported FS copy utility command"]
        return "fs_copy", [stmt]

    @classmethod
    def _emit_delete_block(cls, block) -> tuple[str, list[str]]:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "spfdelete" not in basename:
            return "fs_delete", ["pass  # TODO: unsupported FS delete utility command"]
        stmt = cls._emit_spf_delete(argv)
        return "fs_delete", [stmt]

    @classmethod
    def _emit_robocopy(cls, argv: list[str]) -> str:
        # RoboCopy.va arg layout: <file_name> <source_dir> <dest_dir> [...]
        file_name = MacroState.to_code_expr(argv[1] if len(argv) > 1 else "")
        source_dir = MacroState.to_code_expr(argv[2] if len(argv) > 2 else ".")
        dest_dir = MacroState.to_code_expr(argv[3] if len(argv) > 3 else ".")
        src_expr = CodeExpr(f"str(Path({source_dir.source}) / {file_name.source})")
        return cls.copy.render(src=src_expr, dst=dest_dir)

    @classmethod
    def _emit_spf_copy(cls, argv: list[str]) -> str:
        # SPFCopy.bat arg layout: <source_path> <dest_dir> [recurse]
        src = MacroState.to_code_expr(argv[1] if len(argv) > 1 else "")
        dst_dir = MacroState.to_code_expr(argv[2] if len(argv) > 2 else ".")
        dst_expr = CodeExpr(f"str(Path({dst_dir.source}) / Path({src.source}).name)")
        return cls.copy.render(src=src, dst=dst_expr)

    @classmethod
    def _emit_spf_rename(cls, argv: list[str]) -> str:
        # SPFRename.va arg layout: <source_path> <dest_path>
        src = MacroState.to_code_expr(argv[1] if len(argv) > 1 else "")
        dst = MacroState.to_code_expr(argv[2] if len(argv) > 2 else "")
        return cls.rename.render(src=src, dst=dst)

    @classmethod
    def _emit_spf_delete(cls, argv: list[str]) -> str:
        raw = strip_quotes(argv[1]) if len(argv) > 1 else ""
        items = [p.strip() for p in raw.split(",") if p.strip()]
        return cls.delete.render(paths=MacroState.list_code_expr(items))

    @emittable
    def copy(self, src: str | Path, dst: str | Path, recurse: bool = False) -> None:
        src, dst = Path(src), Path(dst)
        dst.parent.mkdir(parents=True, exist_ok=True)
        if src.is_dir():
            shutil.copytree(src, dst, dirs_exist_ok=True)
        else:
            shutil.copy2(src, dst)

    @emittable
    def rename(self, src: str | Path, dst: str | Path) -> None:
        Path(src).replace(Path(dst))

    @emittable
    def delete(self, paths: list[str | Path], recurse: bool = False) -> None:
        for p in paths:
            path = Path(p)
            if path.is_dir():
                if recurse:
                    shutil.rmtree(path, ignore_errors=True)
            else:
                path.unlink(missing_ok=True)

    @emittable
    def write_file(self, path: str | Path, content: str) -> None:
        out = resolve_path(path, for_write=True)
        out.parent.mkdir(parents=True, exist_ok=True)
        out.write_text(content, encoding="utf-8")
