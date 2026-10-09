"""Direct filesystem operations with explicit job roots."""

from __future__ import annotations

from pathlib import Path
import shutil
import subprocess
import sys
import time

from vg2c.runtime.csv_io import _CsvIO
from vg2c.runtime.values import job_path, substitute


def write_file(path, template, *, workdir, values=None, macros=None, vars=None):
    if vars is not None:
        macros = {**{str(key).upper(): value for key, value in (macros or {}).items()},
                  **{str(key).upper(): value for key, value in vars.items()}}
    scoped = values is not None or macros is not None
    path = job_path(substitute(str(path), values=values, macros=macros) if scoped else path, workdir)
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(substitute(template, values=values, macros=macros) if scoped else template, encoding="utf-8")


def row_count(path, *, workdir):
    return _CsvIO(workdir=workdir).row_count(str(path))


def copy_file(src, dst, *, workdir, recurse=False):
    src, dst = job_path(src, workdir), job_path(dst, workdir)
    dst.parent.mkdir(parents=True, exist_ok=True)
    if src.is_dir():
        shutil.copytree(src, dst, dirs_exist_ok=True)
    else:
        shutil.copy2(src, dst)


def rename_file(src, dst, *, workdir):
    job_path(src, workdir).replace(job_path(dst, workdir))


def delete_files(paths, *, workdir, recurse=False):
    for item in paths:
        path = job_path(item, workdir)
        if path.is_dir():
            if recurse:
                shutil.rmtree(path, ignore_errors=True)
        else:
            path.unlink(missing_ok=True)


def wait_file(path, timeout=30, *, workdir, interval=5):
    path = job_path(path, workdir)
    deadline = time.monotonic() + timeout
    while time.monotonic() < deadline:
        if path.exists():
            return True
        time.sleep(interval)
    return path.exists()


def run_program(argv, *, workdir, cwd=None, env=None, check=False, exedir=None):
    root = job_path(cwd, workdir) if cwd else Path(workdir).resolve()
    if any("@EXEDIR@" in argument for argument in argv):
        if exedir is None:
            raise ValueError("@EXEDIR@ requires an explicit executable root")
        argv = [argument.replace("@EXEDIR@", str(Path(exedir).resolve())) for argument in argv]
    first = Path(argv[0]) if argv else None
    if first is None:
        raise ValueError("Empty external command")
    if sys.platform != "win32" and first.suffix.lower() in {".bat", ".va", ".exe"}:
        raise RuntimeError(f"Windows command {first} is unsupported on {sys.platform}")
    if "/" in str(first) or "\\" in str(first):
        argv = [str(job_path(first, root)), *argv[1:]]
    result = subprocess.run(argv, cwd=str(root), env=env,
                            check=check, shell=first.suffix.lower() in {".bat", ".va", ".exe"})
    return result.returncode
