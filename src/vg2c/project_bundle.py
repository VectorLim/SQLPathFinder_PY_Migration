"""Write portable job projects from installed VG2C package resources."""

from importlib.resources import files
from pathlib import Path
import tempfile

RUNTIME_BOOTSTRAP = (
    "from pathlib import Path\n"
    "import sys\n\n"
    'sys.path.insert(0, str(Path(__file__).resolve().parent / "dependencies"))\n\n'
)

HOST_HANDLER = (
    "\n\ndef handle(job_id=None, context=None, header=None, body=None):\n"
    "    run()\n"
    '    return True, "Function executed successfully", "Function executed successfully"\n'
)


def _copy_resources(source, destination):
    destination.mkdir(parents=True, exist_ok=True)
    for resource in source.iterdir():
        if resource.name == "__pycache__":
            continue
        target = destination / resource.name
        if resource.is_dir():
            _copy_resources(resource, target)
        elif not resource.name.endswith((".pyc", ".pyo")):
            if target.exists():
                raise FileExistsError(
                    f"Project asset collides with bundled file: {target}"
                )
            target.write_bytes(resource.read_bytes())


def write_project(destination, source, assets=()):
    """Stage a complete project before publishing; refuse nonempty destinations."""
    destination = Path(destination).resolve()
    if destination.exists() and (
        not destination.is_dir() or any(destination.iterdir())
    ):
        raise FileExistsError(f"Nonempty project destination: {destination}")
    destination.parent.mkdir(parents=True, exist_ok=True)
    with tempfile.TemporaryDirectory(
        prefix=".vg2c-", dir=destination.parent
    ) as temporary:
        project = Path(temporary) / destination.name
        project.mkdir()
        for name, content in (("main.py", source), *assets):
            path = project / name
            if not path.resolve().is_relative_to(project):
                raise ValueError(f"Project asset escapes destination: {name}")
            if path.exists():
                raise FileExistsError(f"Duplicate project asset: {name}")
            path.parent.mkdir(parents=True, exist_ok=True)
            path.write_text(content, encoding="utf-8", newline="")
        _copy_resources(files("vg2c").joinpath("project_template"), project)
        runtime_package = project / "dependencies" / "vg2c"
        runtime_package.mkdir(parents=True, exist_ok=True)
        (runtime_package / "__init__.py").write_text("", encoding="ascii")
        _copy_resources(files("vg2c.runtime"), runtime_package / "runtime")
        if destination.exists():
            if any(destination.iterdir()):
                raise FileExistsError(f"Nonempty project destination: {destination}")
            for path in project.iterdir():
                path.rename(destination / path.name)
        else:
            project.rename(destination)
    return destination / "main.py"


def init_project(destination):
    """Create an empty job with the same Linux packaging as a translation."""
    source = (
        RUNTIME_BOOTSTRAP
        + "from vg2c.runtime import JobRuntime\n\n"
        + 'TEST_MODE_EXCLUDED = {"aed", "email", "mms"}\n\n'
        + "def run():\n"
        + "    job = JobRuntime.from_script(\n"
        + "        __file__, test_mode_excluded=TEST_MODE_EXCLUDED\n"
        + "    )\n"
        + "    return job\n"
        + HOST_HANDLER
        + '\n\nif __name__ == "__main__":\n'
        + "    run()\n"
    )
    return write_project(destination, source)
