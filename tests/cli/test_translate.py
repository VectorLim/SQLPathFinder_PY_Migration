"""CLI tests for the interactive batch translator."""

from __future__ import annotations

import os
import shutil
import subprocess
import sys
from pathlib import Path

import pytest
import yaml

ROOT = Path(__file__).resolve().parents[2]
FIXTURES = ROOT / "tests" / "fixtures"


@pytest.mark.parametrize("operation", ["init", "translate"])
def test_project_bundles_runtime_and_linux_packaging(tmp_path, operation):
    from vg2c import init_project, translate

    if operation == "init":
        main = init_project(tmp_path / "portable")
    else:
        source = tmp_path / "portable.txt"
        source.write_text(
            "<OPTIONS>\n/ENGINE=SQLite\n/CSV=result.csv\n</OPTIONS>\nselect 1 as ID;\n"
        )
        main = translate(source)
    project = main.parent
    assert (project / "Dockerfile").is_file()
    dockerfile = (project / "Dockerfile").read_text()
    assert "# syntax=" not in dockerfile
    assert "LOG_LEVEL=INFO" in dockerfile
    assert "chmod -R a+rX /app" in dockerfile
    release = yaml.safe_load((project / "release.yaml").read_text())
    assert release["funcname"] == "vg2c-portable"
    assert release["namespace"] == "icmpcs-soims"
    # release.yaml is uploadable in the IMServerless UI: real tag, no workflow-only keys.
    assert release["containers"][0]["image"].endswith("/vg2c-portable:latest")
    assert not {"secrets", "branches"} & set(release)
    settings = yaml.safe_load((project / "automation/settings.yaml").read_text())
    assert settings["secrets"]["env"] == {
        "workflow-testing-database": ["NAME", "PW"],
        "aed-api-key": ["API_KEY", "API_CONFIG"],
    }
    for name in ("__init__", "__main__", "target", "cluster", "gateway", "release"):
        assert (project / "automation" / f"{name}.py").is_file()
    workflow = yaml.safe_load((project / ".github/workflows/release.yml").read_text())
    events = workflow.get("on", workflow.get(True))
    assert events["workflow_dispatch"]["inputs"]["deploy"]["default"] is True
    assert events["push"]["branches"] == ["**"]
    assert workflow["jobs"]["release"]["env"]["RELEASE_CONFIG"] == "release.yaml"
    assert (project / ".gitignore").is_file()
    for name in ("Start-Release-Runner.cmd", "Stop-Release-Runner.cmd", "runner/start-runner.ps1", "runner/Dockerfile"):
        assert (project / name).is_file()
    # Launchers resolve the script relative to their own folder, so they must stay at the root.
    for launcher in ("Start-Release-Runner.cmd", "Stop-Release-Runner.cmd"):
        assert '"%~dp0runner\\start-runner.ps1"' in (project / launcher).read_text()
        assert not (project / "runner" / launcher).exists()
    # Local runs bootstrap a project .venv that must stay out of the image context.
    assert "main.py" in (project / "Run-Local.cmd").read_text()
    dockerignore = (project / ".dockerignore").read_text().splitlines()
    assert {".venv", "Run-Local.cmd", "dependencies/requirements-local.txt"} <= set(dockerignore)
    assert "-r requirements.txt" in (project / "dependencies/requirements-local.txt").read_text()
    assert "soims-release" in workflow["jobs"]["release"]["runs-on"]
    assert "--labels soims-release" in (project / "runner/start-runner.ps1").read_text()
    assert not (project / "func.json").exists()
    assert not (project / "entrypoint.py").exists()
    assert (
        "def handle(job_id=None, context=None, header=None, body=None):"
        in main.read_text()
    )
    assert (project / "dependencies" / "requirements.txt").is_file()
    assert (
        project / "dependencies" / "aed_client_apikey-6.2.0-py3-none-any.whl"
    ).is_file()
    assert (project / "dependencies" / "vg2c" / "runtime" / "aed_client.py").is_file()
    assert not (project / "dependencies" / "vg2c" / "compilation.py").exists()
    process = subprocess.run(
        [
            sys.executable,
            "-I",
            "-c",
            (
                f"import runpy; namespace = runpy.run_path({str(main)!r}, run_name='import_only'); "
                "assert callable(namespace['handle']); "
                "import vg2c.runtime; "
                f"assert str(vg2c.runtime.__file__).startswith({str(project)!r})"
            ),
        ],
        cwd=tmp_path,
        capture_output=True,
        text=True,
    )
    assert process.returncode == 0, process.stderr


def test_translated_project_executes_after_move_without_compiler(tmp_path):
    from vg2c import translate

    source = tmp_path / "local.txt"
    source.write_text(
        "<OPTIONS>\n/ENGINE=SQLite\n/CSV=result.csv\n</OPTIONS>\nselect 1 as ID;\n"
    )
    main = translate(source)
    moved = tmp_path / "moved project"
    shutil.move(main.parent, moved)
    source.unlink()
    unrelated = tmp_path / "unrelated"
    unrelated.mkdir()
    code = (
        f"import runpy; runpy.run_path({str(moved / 'main.py')!r}, run_name='__main__'); "
        "import vg2c.runtime,sys; "
        f"assert str(vg2c.runtime.__file__).startswith({str(moved / 'dependencies')!r}); "
        "assert 'vg2c.compilation' not in sys.modules; "
        f"sys.path.insert(0, {str(moved)!r}); "
        "from automation.target import Target, app_spec; "
        "target = Target(branch='main'); "
        "assert target.namespace == 'icmpcs-soims'; "
        "assert target.env_secrets == {'workflow-testing-database': ['NAME','PW'], 'aed-api-key': ['API_KEY','API_CONFIG']}; "
        "assert app_spec(target, target.repository + '@sha256:' + 'a'*64, '', '1.0', False)['action'] == 'create'; "
        "sys.argv = ['automation', 'plan', '--branch', 'main']; "
        "runpy.run_module('automation', run_name='__main__')"
    )
    process = subprocess.run(
        [sys.executable, "-I", "-c", code],
        cwd=unrelated,
        capture_output=True,
        text=True,
    )
    assert process.returncode == 0, process.stderr
    assert "RELEASE=true" in process.stdout
    assert (moved / "output/result.csv").read_text() == "ID\n1\n"


@pytest.mark.parametrize("name", ["same job", "A" * 80, "\u751f\u6210\u5c08\u6848"])
def test_generated_release_names_are_valid_and_stable(tmp_path, name):
    from vg2c.project_bundle import init_project
    from vg2c.project_template.automation.target import Target, release_branches

    main = init_project(tmp_path / name)
    config = main.parent / "release.yaml"
    target = Target(config, "prod")
    other_branch = Target(config, "feature/any-name")
    assert target.function.startswith("vg2c-")
    assert target.function == yaml.safe_load(config.read_text(encoding="utf-8"))["funcname"]
    assert Target(config, "main").function == target.function + "-test"
    assert other_branch.function == target.function + "-test"
    assert len(other_branch.deployment_name("1.999")) <= 63
    assert release_branches(config) == {"prod": "", "*": "-test"}
    copied = tmp_path / "copied"
    shutil.copytree(main.parent, copied)
    assert Target(copied / "release.yaml", "prod").function == target.function


def test_generated_release_rejects_workflow_keys_in_appspec(tmp_path):
    from vg2c.project_bundle import init_project
    from vg2c.project_template.automation.target import Target

    config = init_project(tmp_path / "split-job").parent / "release.yaml"
    spec = yaml.safe_load(config.read_text(encoding="utf-8"))
    config.write_text(yaml.safe_dump({**spec, "branches": {"main": ""}}), encoding="utf-8")
    with pytest.raises(ValueError, match="automation/settings.yaml"):
        Target(config, "main")


def test_generated_release_attaches_shared_credentials_without_inline_values(tmp_path):
    import base64
    import json
    from unittest.mock import Mock

    from vg2c.project_bundle import init_project
    from vg2c.project_template.automation.cluster import configure, preflight
    from vg2c.project_template.automation.target import Target

    main = init_project(tmp_path / "shared-job")
    target = Target(main.parent / "release.yaml", "main")
    cluster = Mock()
    encoded = base64.b64encode(b"offline-value").decode()
    cluster.get.return_value = {
        "data": {key: encoded for keys in target.secrets.values() for key in keys}
    }
    preflight(cluster, target)
    assert {call.args for call in cluster.get.call_args_list} == {
        ("secret", name) for name in target.secrets
    }
    image = target.repository + "@sha256:" + "a" * 64
    deployment = {
        "metadata": {"name": target.deployment_name("1.0"), "resourceVersion": "1"},
        "spec": {
            "template": {
                "spec": {
                    "containers": [
                        {
                            "image": image,
                            "env": [
                                *[
                                    {"name": key, "value": value}
                                    for key, value in target.environment.items()
                                ],
                                {"name": "PW", "value": "stale-inline-value"},
                                {"name": "API_KEY", "value": "stale-inline-value"},
                            ],
                        }
                    ]
                }
            }
        },
    }
    configure(cluster, deployment, image, target)
    arguments = cluster.call.call_args.args
    patch = json.loads(arguments[arguments.index("--patch") + 1])
    environment = next(item["value"] for item in patch if item["path"].endswith("/env"))
    assert {entry["name"] for entry in environment} == {"ENV_MODE", "SITE"}
    env_from = next(
        item["value"] for item in patch if item["path"].endswith("/envFrom")
    )
    assert env_from == [
        {"secretRef": {"name": name, "optional": False}} for name in target.env_secrets
    ]
    assert "stale-inline-value" not in json.dumps(patch)
    cluster.get.return_value = {"data": {}}
    with pytest.raises(ValueError, match="Required nonempty keys"):
        preflight(cluster, target)


def test_initialized_and_translated_handlers_preserve_soims_contract(tmp_path):
    import pytest
    from vg2c import init_project, translate

    source = tmp_path / "local.txt"
    source.write_text(
        "<OPTIONS>\n/ENGINE=SQLite\n/CSV=result.csv\n</OPTIONS>\nselect 1 as ID;\n"
    )
    for main in (init_project(tmp_path / "blank"), translate(source)):
        namespace = {"__file__": str(main), "__name__": "job_under_test"}
        exec(compile(main.read_text(), str(main), "exec"), namespace)
        calls = []
        namespace["run"] = lambda: calls.append("run")
        assert namespace["handle"]("job", "context", "header", "body") == (
            True,
            "Function executed successfully",
            "Function executed successfully",
        )
        assert calls == ["run"]

        def failure():
            raise RuntimeError("synthetic job failure")

        namespace["run"] = failure
        with pytest.raises(RuntimeError, match="synthetic job failure"):
            namespace["handle"]()


def _run_cli(
    args: list[str], *, selection: str = "1\n", cwd: Path | None = None
) -> subprocess.CompletedProcess[str]:
    cmd = [sys.executable, "-m", "vg2c.cli", *args]
    env = os.environ.copy()
    existing = env.get("PYTHONPATH", "")
    base_paths = [str(ROOT / "src"), str(ROOT)]
    if existing:
        base_paths.append(existing)
    env["PYTHONPATH"] = os.pathsep.join(base_paths)
    return subprocess.run(
        cmd,
        cwd=cwd or ROOT,
        capture_output=True,
        text=True,
        input=selection,
        env=env,
    )


def _copy_script(directory: Path, name: str = "script_short.txt") -> Path:
    directory.mkdir(parents=True, exist_ok=True)
    target = directory / name
    shutil.copy(FIXTURES / "script_short.txt", target)
    return target


def test_translate_selected_file_to_output_directory(tmp_path):
    input_dir = tmp_path / "input"
    output_dir = tmp_path / "output"
    _copy_script(input_dir)

    proc = _run_cli([str(input_dir), str(output_dir)])

    assert proc.returncode == 0, proc.stderr
    out_file = output_dir / "script_short" / "main.py"
    assert out_file.exists()
    text = out_file.read_text(encoding="utf-8")
    assert "job_entrypoint" not in text
    assert "def run():" in text
    assert "ctx" not in text
    assert list(out_file.parent.glob("sql/*.sql"))
    assert (out_file.parent / "Dockerfile").is_file()
    assert (out_file.parent / "dependencies" / "vg2c" / "runtime" / "job.py").is_file()
    assert "dependencies" in text


def test_init_command_creates_project_and_refuses_overwrite(tmp_path):
    project = tmp_path / "new project"
    process = _run_cli(["init", str(project)], selection="")
    assert process.returncode == 0, process.stderr
    original = (project / "main.py").read_bytes()
    assert (project / "Dockerfile").is_file()
    process = _run_cli(["init", str(project)], selection="")
    assert process.returncode == 1
    assert "Nonempty project" in process.stderr
    assert (project / "main.py").read_bytes() == original


def test_init_command_defaults_to_empty_current_directory(tmp_path):
    process = _run_cli(["init"], selection="", cwd=tmp_path)
    assert process.returncode == 0, process.stderr
    assert (tmp_path / "main.py").is_file()


def test_project_asset_escape_is_rejected_without_publication(tmp_path):
    import pytest
    from vg2c.project_bundle import write_project

    with pytest.raises(ValueError, match="escapes"):
        write_project(tmp_path / "new", "", [("../outside.py", "bad")])
    assert not (tmp_path / "new").exists()
    assert not (tmp_path / "outside.py").exists()


def test_translate_defaults_output_to_input_directory(tmp_path):
    input_dir = tmp_path / "input"
    _copy_script(input_dir)

    proc = _run_cli([str(input_dir)])

    assert proc.returncode == 0, proc.stderr
    assert (input_dir / "script_short" / "main.py").exists()


def test_translate_all_selected_files(tmp_path):
    input_dir = tmp_path / "input"
    output_dir = tmp_path / "output"
    _copy_script(input_dir, "a.txt")
    _copy_script(input_dir, "b.txt")

    proc = _run_cli([str(input_dir), str(output_dir)], selection="*\n")

    assert proc.returncode == 0, proc.stderr
    assert (output_dir / "a" / "main.py").exists()
    assert (output_dir / "b" / "main.py").exists()


def test_missing_input_directory_returns_error(tmp_path):
    missing = tmp_path / "missing"
    proc = _run_cli([str(missing)])

    assert proc.returncode == 1
    assert "input directory" in proc.stderr.lower()
    assert "not found" in proc.stderr.lower()


def test_empty_input_directory_exits_cleanly(tmp_path):
    input_dir = tmp_path / "empty"
    input_dir.mkdir()

    proc = _run_cli([str(input_dir)])

    assert proc.returncode == 0
    assert "No .txt files found" in proc.stdout


def test_help_documents_batch_directory_interface():
    proc = _run_cli(["--help"], selection="")

    assert proc.returncode == 0
    assert "vg2c [input_dir] [output_dir]" in proc.stdout
    assert "translate .txt files" in proc.stdout
    assert "vg2c init [directory]" in proc.stdout


def test_batch_collision_fails_before_any_project_is_written(tmp_path):
    _copy_script(tmp_path, "a.b.txt")
    _copy_script(tmp_path, "a b.txt")
    output = tmp_path / "projects"
    process = _run_cli([str(tmp_path), str(output)], selection="*\n")
    assert process.returncode != 0
    assert "collide" in process.stderr
    assert not output.exists()
