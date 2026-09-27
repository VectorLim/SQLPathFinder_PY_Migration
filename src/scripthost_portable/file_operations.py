"""OS operations used by original ScriptHost helpers; no VG2/task parsing."""

from __future__ import annotations

import csv
import glob
import shutil
import stat
import time
import xml.etree.ElementTree as ET
import zipfile
from pathlib import Path, PurePosixPath, PureWindowsPath


def xml_to_csv(source: str, destination: str, delimiter: str) -> None:
    """Convert ScriptHost CSVToXML's Main/Item format, rejecting other XML shapes.

    The Windows Excel converter accepts additional formats; those remain unproven.
    """
    root = ET.parse(source).getroot()
    if root.tag != "Main":
        raise ValueError("Portable XMLTOCSV requires ScriptHost Main/Item XML")
    records, columns = [], []
    for item in root:
        if item.tag != "Item" or item.attrib or (item.text or "").strip():
            raise ValueError("Portable XMLTOCSV requires ScriptHost Main/Item XML")
        row = {}
        for field in item:
            if list(field) or field.attrib or field.tag in row or "}" in field.tag:
                raise ValueError("Portable XMLTOCSV requires unique, flat XML fields")
            if field.tag not in columns:
                columns.append(field.tag)
            row[field.tag] = field.text or ""
        records.append(row)
    with Path(destination).open("w", encoding="utf-8", newline="") as stream:
        writer = csv.DictWriter(stream, fieldnames=columns, delimiter=delimiter)
        if columns:
            writer.writeheader()
            writer.writerows(records)


def copy_files(source: str, destination: str) -> None:
    """Replace the COPY operation after ScriptHost expands distribution tokens."""
    if Path(source).name == "*.*":
        source = str(Path(source).with_name("*"))
    matches = [Path(name) for name in glob.glob(source)]
    if not matches:
        raise FileNotFoundError(source)
    target = Path(destination)
    if len(matches) > 1 and not target.is_dir():
        raise NotADirectoryError(destination)
    for path in matches:
        shutil.copy2(path, target)


def delete_files(patterns: list[str], force: bool) -> None:
    """Windows DEL's file-only operation, including a directory's immediate files."""
    for pattern in patterns:
        path = Path(pattern)
        if path.name == "*.*":
            pattern = str(path.with_name("*"))
        for name in glob.glob(pattern):
            item = Path(name)
            targets = list(item.iterdir()) if item.is_dir() else [item]
            for target in targets:
                if target.is_file() or target.is_symlink():
                    if force and not target.is_symlink():
                        target.chmod(target.stat().st_mode | stat.S_IWUSR)
                    target.unlink()


def robocopy_files(
    source: str, destination: str, patterns: list[str], retries: int, wait: int, switches: list[str]
) -> int:
    """Portable copy operation for the characterized /S /E /MOV /NP /IS subset.

    ScriptHost owns argument defaults, open checks, missing-source policy and
    interpretation of the returned RoboCopy status bits.
    """
    flags = {flag for flag in switches if flag}
    unknown = flags - {"/S", "/E", "/MOV", "/NP", "/IS"}
    if unknown:
        raise ValueError("Unsupported portable RoboCopy switches: " + ", ".join(sorted(unknown)))
    src, dst = Path(source), Path(destination)
    if not src.is_dir():
        raise NotADirectoryError(source)
    if src.resolve() == dst.resolve():
        return 0
    recursive = bool(flags & {"/S", "/E"})
    copied = False
    matches = set()
    for pattern in patterns:
        pattern = pattern.strip('"')
        if pattern == "*.*":
            pattern = "*"
        matches.update(src.rglob(pattern) if recursive else src.glob(pattern))
    if "/E" in flags:
        for directory in src.rglob("*"):
            if directory.is_dir():
                (dst / directory.relative_to(src)).mkdir(parents=True, exist_ok=True)
    for item in sorted(matches):
        if not item.is_file():
            continue
        target = dst / item.relative_to(src)
        same = (
            target.is_file()
            and target.stat().st_size == item.stat().st_size
            and target.stat().st_mtime_ns == item.stat().st_mtime_ns
        )
        if not same or "/IS" in flags:
            for attempt in range(max(0, retries) + 1):
                try:
                    target.parent.mkdir(parents=True, exist_ok=True)
                    shutil.copy2(item, target)
                    copied = True
                    break
                except OSError:
                    if attempt == max(0, retries):
                        raise
                    time.sleep(max(0, wait))
        if "/MOV" in flags:
            item.unlink()
    return int(copied)


def unzip_file(source: str, destination: str, preserve_paths: bool) -> None:
    """Replace unzip -o [-j], validating all member paths before writing any."""
    root = Path(destination).resolve()
    with zipfile.ZipFile(source) as archive:
        members = []
        for info in archive.infolist():
            name = info.filename.replace("\\", "/")
            path = PurePosixPath(name)
            if (
                path.is_absolute()
                or PureWindowsPath(name).drive
                or ".." in path.parts
                or stat.S_ISLNK(info.external_attr >> 16)
            ):
                raise ValueError(f"Unsafe ZIP member path: {info.filename!r}")
            target = root.joinpath(*path.parts) if preserve_paths else root / path.name
            if not target.resolve().is_relative_to(root):
                raise ValueError(f"Unsafe ZIP member path: {info.filename!r}")
            members.append((info, target))
        root.mkdir(parents=True, exist_ok=True)
        for info, target in members:
            if info.is_dir():
                if preserve_paths:
                    target.mkdir(parents=True, exist_ok=True)
                continue
            target.parent.mkdir(parents=True, exist_ok=True)
            with archive.open(info) as reader, target.open("wb") as writer:
                shutil.copyfileobj(reader, writer)


def clean_delimited_file(source: str, destination: str, delimiter: str) -> None:
    """POSIX CleanDelimsCRLF boundary for UTF-8 CSV/TAB input.

    Original ConvertDLM documents removal of quotes and replacement of embedded
    commas, tabs and CR/LF. Keep record delimiters; never reinterpret report SQL.
    """
    if delimiter not in {",", "\t"}:
        raise ValueError("UNCERTIFIED: CleanDelimsCRLF delimiter")
    with Path(source).open(encoding="utf-8-sig", newline="") as incoming:
        records = list(csv.reader(incoming, delimiter=delimiter, strict=True))
    clean = str.maketrans({'"': "", ",": ";", "\t": " ", "\r": " ", "\n": " "})
    with Path(destination).open("w", encoding="utf-8", newline="") as outgoing:
        for record in records:
            fields = [field.translate(clean) for field in record]
            if any(delimiter in field for field in fields):
                raise ValueError("UNCERTIFIED: embedded output delimiter in CleanDelimsCRLF")
            outgoing.write(delimiter.join(fields) + "\n")
