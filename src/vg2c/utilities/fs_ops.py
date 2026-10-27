"""FileSystemOps - copy / rename / delete via pathlib + shutil."""

from __future__ import annotations

from dataclasses import replace
from pathlib import Path, PureWindowsPath

from vg2c.emitter.models import BlockEmission, CodeExpr, ExternalParameter
from vg2c.emitter.operations import EmittableOperation, emittable
from vg2c.runtime.files import copy_file, delete_files, rename_file, write_file
from vg2c.kind import Kind
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import (
    list_code_expr,
    lower_native_call,
    native_argument,
    resolve_output_path,
    split_utility_command,
    to_code_expr,
)
from vg2c.utilities._runtime_helpers import resolve_path, strip_quotes
from vg2c.utility_metadata import FileEffectDefinition


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
    def emit_block(cls, block, *, macros: str = "macros", file_asset=None) -> BlockEmission:
        if block.kind is Kind.FS_COPY:
            return cls._emit_copy_block(block, macros)
        if block.kind is Kind.FS_DELETE:
            return cls._emit_delete_block(block, macros)

        from vg2c.utilities.direct_operations import DirectOperationMetadata

        path = resolve_output_path(block)
        # ScriptHost parseTaskOptions lstrips SPFTaskCommand (SPFSQL3.py:1824).
        body = block.resolved_body.lstrip()
        asset = file_asset or cls.asset_name(block)
        original = DirectOperationMetadata.operation_definition("ctx", "write_file")
        template = original.parameter("template")
        definition = replace(original, parameters=tuple(
            replace(item, name="asset", display_label="File asset")
            if item.name == "template" else item for item in original.parameters
        ))
        method = "update_file" if "<<<" in body else "load_file"
        stmt = EmittableOperation.render_method_call(
            definition, function=f"job.{method}",
            kwargs={"path": native_argument(path, macros), "asset": asset},
        )
        return BlockEmission(
            (stmt,), assets=((asset, body),),
            external_parameters=(ExternalParameter(
                definition=template, value=body, editor_type="multiline",
                read_only_reason=f"Edit the original template in {asset}.",
            ),),
        )

    @staticmethod
    def asset_name(block):
        path = resolve_output_path(block)
        # Runtime filenames cannot be used as Windows asset filenames.
        if "<<<" in path:
            return f"files/step_{block.index:04d}.txt"
        relative = PureWindowsPath(path)
        if relative.is_absolute() or relative.drive or ".." in relative.parts:
            relative = PureWindowsPath(relative.name)
        return "files/" + relative.as_posix().lstrip("/")

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        return split_utility_command(text)

    @classmethod
    def _emit_copy_block(cls, block, macros: str) -> BlockEmission:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "robocopy" in basename:
            stmt = cls._emit_robocopy(argv)
        elif "spfcopy" in basename:
            stmt = cls._emit_spf_copy(argv)
        elif "spfrename" in basename:
            stmt = cls._emit_spf_rename(argv)
        else:
            raise ValueError(f"Unsupported filesystem copy utility: {basename}")
        target = "job.rename_file" if "spfrename" in basename else "job.copy_file"
        if "robocopy" in basename and len(argv) > 3 and "<<<MIPPATH>>>" in argv[3].upper():
            target = "job.submit_mms"
        return BlockEmission(
            (lower_native_call(stmt, function=target, macros=macros),),
        )

    @classmethod
    def _emit_delete_block(cls, block, macros: str) -> BlockEmission:
        argv = cls._utility_argv(block)
        basename = argv[0].split("/")[-1].split("\\")[-1].lower() if argv else ""
        if "spfdelete" not in basename:
            raise ValueError(f"Unsupported filesystem delete utility: {basename}")
        stmt = cls._emit_spf_delete(argv)
        return BlockEmission((lower_native_call(stmt, function="job.delete_files", macros=macros),))

    @classmethod
    def _emit_robocopy(cls, argv: list[str]) -> str:
        # RoboCopy.va arg layout: <file_name> <source_dir> <dest_dir> [...]
        file_name = to_code_expr(argv[1] if len(argv) > 1 else "")
        source_dir = to_code_expr(argv[2] if len(argv) > 2 else ".")
        dest_dir = to_code_expr(argv[3] if len(argv) > 3 else ".")
        src_expr = CodeExpr(f"str(Path({source_dir.source}) / {file_name.source})")
        return cls.copy.render(src=src_expr, dst=dest_dir)

    @classmethod
    def _emit_spf_copy(cls, argv: list[str]) -> str:
        # SPFCopy.bat arg layout: <source_path> <dest_dir> [recurse]
        src = to_code_expr(argv[1] if len(argv) > 1 else "")
        dst_dir = to_code_expr(argv[2] if len(argv) > 2 else ".")
        dst_expr = CodeExpr(f"str(Path({dst_dir.source}) / Path({src.source}).name)")
        return cls.copy.render(src=src, dst=dst_expr)

    @classmethod
    def _emit_spf_rename(cls, argv: list[str]) -> str:
        # SPFRename.va arg layout: <source_path> <dest_path>
        src = to_code_expr(argv[1] if len(argv) > 1 else "")
        dst = to_code_expr(argv[2] if len(argv) > 2 else "")
        return cls.rename.render(src=src, dst=dst)

    @classmethod
    def _emit_spf_delete(cls, argv: list[str]) -> str:
        raw = strip_quotes(argv[1]) if len(argv) > 1 else ""
        items = [p.strip() for p in raw.split(",") if p.strip()]
        return cls.delete.render(paths=list_code_expr(items))

    @emittable(
        display_name="Copy File",
        parameter_visibility={"recurse": "internal"},
        file_effects=(
            FileEffectDefinition(
                "copy",
                "copy",
                inputs=("src",),
                outputs=("dst",),
                input_base="working-directory",
                output_base="working-directory",
                reason="File or directory copy; destination interpretation depends on runtime filesystem state.",
            ),
        ),
    )
    def copy(self, src: str | Path, dst: str | Path, recurse: bool = False) -> None:
        return copy_file(src, dst, workdir=Path.cwd(), recurse=recurse)

    @emittable(
        display_name="Rename File",
        file_effects=(
            FileEffectDefinition(
                "move",
                "move",
                inputs=("src",),
                outputs=("dst",),
                input_base="working-directory",
                output_base="working-directory",
            ),
        )
    )
    def rename(self, src: str | Path, dst: str | Path) -> None:
        return rename_file(src, dst, workdir=Path.cwd())

    @emittable(
        display_name="Delete File",
        file_effects=(
            FileEffectDefinition(
                "delete",
                "delete",
                inputs=("paths",),
                input_base="working-directory",
                reason="Directories are removed only when recurse is enabled.",
            ),
        )
    )
    def delete(self, paths: list[str | Path], recurse: bool = False) -> None:
        return delete_files(paths, workdir=Path.cwd(), recurse=recurse)

    @emittable(
        display_name="Write File",
        file_effects=(FileEffectDefinition("write", "write", outputs=("path",)),)
    )
    def write_file(self, path: str | Path, content: str) -> None:
        return write_file(path, content, workdir=resolve_path(".", for_write=True))
