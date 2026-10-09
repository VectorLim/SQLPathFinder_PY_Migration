"""The compiler's generated-project naming and output contract."""

from pathlib import Path
import re


def project_name(name):
    name = re.sub(r"[^A-Za-z0-9_-]+", "_", name).strip("_") or "job"
    if re.fullmatch(r"CON|PRN|AUX|NUL|COM[1-9]|LPT[1-9]", name, re.I):
        name = "job_" + name
    return name


def project_main_path(input_path, root=None):
    input_path = Path(input_path)
    return (Path(root) if root is not None else input_path.parent).resolve() / project_name(input_path.stem) / "main.py"
