"""Write portable job projects from installed VG2C package resources."""

import hashlib
from importlib.resources import files
from pathlib import Path
import re
import tempfile

import yaml

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


def _configure_release(project):
    original = project.name.casefold()
    slug = re.sub(r"[^a-z0-9-]+", "-", original).strip("-")
    if not slug or slug != original or len(slug) > 32:
        digest = hashlib.sha256(project.name.encode("utf-8")).hexdigest()[:8]
        slug = f"{(slug or 'job')[:23].rstrip('-')}-{digest}"
    name = "vg2c-" + slug
    path = project / "release.yaml"
    config = yaml.safe_load(path.read_text(encoding="utf-8"))
    config["project"]["name"] = project.name
    config["funcname"] = name
    config["dnshostname"] = f"{name}.s33prod.mfg.intel.com"
    config["containers"][0][
        "image"
    ] = f"amr-registry.caas.intel.com/soia/{name}:latest"
    path.write_text(yaml.safe_dump(config, sort_keys=False), encoding="utf-8")


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
        _configure_release(project)
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
    """Create an empty job with runtime, Linux image and GitHub release kit."""
    source = (
        RUNTIME_BOOTSTRAP
        + "from vg2c.runtime import JobRuntime\n\n"
        + 'TEST_MODE_EXCLUDED = {"aed", "email", "mms"}\n\n'
        + "def run():\n"
        + "    job = JobRuntime.from_script(\n"
        + "        __file__, test_mode_excluded=TEST_MODE_EXCLUDED\n"
        + "    )\n"
        + '    # job.environ["NAME"] = ""\n'
        + '    # job.environ["PW"] = ""\n'
        + "    return job\n"
        + HOST_HANDLER
        + '\n\nif __name__ == "__main__":\n'
        + "    run()\n"
    )
    return write_project(destination, source)
