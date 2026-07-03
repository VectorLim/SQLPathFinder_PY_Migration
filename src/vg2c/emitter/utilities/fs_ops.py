"""FileSystemOps — copy / rename / delete via pathlib + shutil."""

from __future__ import annotations

import shutil
from pathlib import Path

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
