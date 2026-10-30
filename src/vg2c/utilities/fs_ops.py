"""FileSystemOps - copy / rename / delete via pathlib + shutil."""

from __future__ import annotations

import ast
from dataclasses import replace
from pathlib import Path, PureWindowsPath
from typing import Any

from vg2c.emitter.models import BlockEmission, CodeExpr
from vg2c.emitter.operations import EmittableOperation, emittable
from vg2c.kind import Kind
from vg2c.project_paths import project_name
from vg2c.runtime.files import (
    copy_file,
    delete_files,
    rename_file,
    wait_file as runtime_wait_file,
)
from vg2c.runtime.values import strip_quotes
from vg2c.utilities._base import EmitterUtility
from vg2c.utilities._emit_helpers import (
    list_code_expr,
    lower_native_call,
    native_argument,
    resolve_output_path,
    split_utility_command,
    to_code_expr,
)


class FileSystemOps(EmitterUtility):
    utility_name = "fs_ops"
    handles = (Kind.WRITE_FILE, Kind.FS_COPY, Kind.FS_DELETE)

    @staticmethod
    def check(options) -> Kind | None:
        if options.lookup.get("WRITE-FILE", "").upper() == "Y":
            csv_value = options.lookup.get("CSV", "")
            if csv_value.lower().endswith(".py"):
                return None
            return Kind.WRITE_FILE

        utilities = options.lookup.get("UTILITIES")
        if not utilities:
            return None

        first_token = utilities.strip().split(maxsplit=1)[0].strip().strip('"')
        basename = first_token.split("/")[-1].split("\\")[-1].lower()

        if "robocopy" in basename or "spfcopy" in basename or "spfrename" in basename:
            return Kind.FS_COPY
        if "spfdelete" in basename:
            return Kind.FS_DELETE
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
        definition = replace(original, parameters=tuple(
            replace(item, name="asset")
            if item.name == "template" else item for item in original.parameters
        ))
        method = "update_file" if "<<<" in body else "load_file"
        stmt = EmittableOperation.render_method_call(
            definition, function=f"job.{method}",
            kwargs={"path": native_argument(path, macros), "asset": asset},
        )
        return BlockEmission(
            (stmt,), assets=((asset, body),),
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

    @emittable
    def copy(self, src: str | Path, dst: str | Path, recurse: bool = False) -> None:
        return copy_file(src, dst, workdir=Path.cwd(), recurse=recurse)

    @emittable
    def rename(self, src: str | Path, dst: str | Path) -> None:
        return rename_file(src, dst, workdir=Path.cwd())

    @emittable
    def delete(self, paths: list[str | Path], recurse: bool = False) -> None:
        return delete_files(paths, workdir=Path.cwd(), recurse=recurse)


class PythonEmbed(EmitterUtility):
    """Utility class for directly embedding Python script blocks."""

    utility_name = "python_embed"
    handles = (Kind.PYTHON_EMBED,)

    @staticmethod
    def check(options) -> Kind | None:
        if options.lookup.get("WRITE-FILE", "").upper() != "Y":
            return None

        csv_value = options.lookup.get("CSV", "")
        if csv_value.lower().endswith(".py"):
            return Kind.PYTHON_EMBED
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


class WaitFile(EmitterUtility):
    """Poll for a file to appear, up to a configurable timeout (seconds)."""

    utility_name = "wait_file"
    handles = (Kind.WAIT_FILE,)

    _WAITFILE_NAMES = {"waitfile.va", "waitfile.bat", "waitfile.exe"}
    _DEFAULT_TIMEOUT = 30
    _POLL_INTERVAL = 5

    @staticmethod
    def check(options) -> Kind | None:
        text = options.lookup.get("UTILITIES", "").strip()
        if not text:
            return None
        argv = split_utility_command(text)
        if not argv:
            return None
        basename = argv[0].split("/")[-1].split("\\")[-1].lower()
        if basename in WaitFile._WAITFILE_NAMES:
            return Kind.WAIT_FILE
        return None

    @staticmethod
    def _utility_argv(block) -> list[str]:
        text = block.resolved_options.lookup.get("UTILITIES", "").strip()
        return split_utility_command(text)

    @classmethod
    def emit_block(cls, block, *, macros: str = "macros") -> BlockEmission:
        argv = cls._utility_argv(block)
        path_expr = to_code_expr(argv[1] if len(argv) > 1 else "")
        raw_timeout = (
            strip_quotes(argv[2]) if len(argv) > 2 else str(cls._DEFAULT_TIMEOUT)
        )
        try:
            timeout_val = int(raw_timeout)
        except ValueError:
            timeout_val = cls._DEFAULT_TIMEOUT
        stmt = lower_native_call(
            cls.poll.render(path_expr, timeout_val),
            function="job.wait_file",
            macros=macros,
        )
        return BlockEmission((stmt,))

    @emittable
    def poll(
        self,
        path: str | Path,
        timeout: int = _DEFAULT_TIMEOUT,
        interval: int = _POLL_INTERVAL,
    ) -> bool:
        return runtime_wait_file(path, timeout, workdir=Path.cwd(), interval=interval)
