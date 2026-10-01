from __future__ import annotations

import os
import subprocess
import sys
import threading
from concurrent.futures import ThreadPoolExecutor
from pathlib import Path

import pytest

from scripthost_portable import PortableScriptHostRuntime

DELIM = "<---- New Query ---->"


def block(*options: str, body: str = "") -> str:
    option_text = "\n".join(options)
    return f"<OPTIONS>\n{option_text}\n</OPTIONS>\n{body}"


def script(*blocks: str) -> str:
    return f"\n{DELIM}\n".join(blocks)


def vertical_slice(root: Path) -> str:
    root.mkdir(parents=True, exist_ok=True)
    macro = root / "macro.csv"
    macro.write_text("name,flag\nalpha,1\nbeta,0\n", encoding="utf-8")
    output_pattern = root / "out_<<<name>>>_<<<spf-loop-ctr-x-int>>>.csv"
    first_output = root / "out_alpha_0.csv"
    verified = root / "verified.txt"
    bad = root / "bad.txt"
    return script(
        block(
            "/WRITE-FILE=Y",
            f"/CSV={root / 'seed.csv'}",
            body="id,value\n1,a\n2,b\n<EOF>ignored",
        ),
        block(f'/UTILITIES={{START-MACRO}} "{macro}" "N"'),
        block('/UTILITIES={IF-THEN} "VAR(<<<flag>>>)" "GT" "0"'),
        block('/UTILITIES={FOR-LOOP} "0" "2" "1" "x" "N"'),
        block(
            "/WRITE-FILE=Y",
            f"/CSV={output_pattern}",
            body="name,loop\n<<<name>>>,<<<spf-loop-ctr-x-int>>>\n",
        ),
        block("/UTILITIES={END-LOOP}"),
        block("/UTILITIES={ELSE}"),
        block("/WRITE-FILE=Y", f"/CSV={bad}", body="wrong branch"),
        block("/UTILITIES={END-IF}"),
        block("/UTILITIES={END-MACRO}"),
        block(f'/UTILITIES={{ROWS-IN-FILE}} "{first_output}" "PORTABLE_SPFS_ROWS" "N"'),
        block('/UTILITIES={IF-THEN} "PORTABLE_SPFS_ROWS" "GT" "0"'),
        block("/WRITE-FILE=Y", f"/CSV={verified}", body="verified"),
        block("/UTILITIES={END-IF}"),
    )


def outputs(root: Path) -> dict[str, str]:
    names = [
        "seed.csv",
        "out_alpha_0.csv",
        "out_alpha_1.csv",
        "out_alpha_2.csv",
        "verified.txt",
    ]
    return {name: (root / name).read_text(encoding="utf-8") for name in names}


def test_original_runtime_vertical_slice(tmp_path: Path) -> None:
    root = tmp_path / "original"
    try:
        assert PortableScriptHostRuntime().run_text(vertical_slice(root), root)
        actual = outputs(root)
        assert actual["seed.csv"] == "id,value\n1,a\n2,b\n"
        for i in range(3):
            assert actual[f"out_alpha_{i}.csv"] == f"name,loop\nalpha,{i}\n\n"
        assert actual["verified.txt"] == "verified\n"
        assert not (root / "bad.txt").exists()
        assert os.environ["PORTABLE_SPFS_ROWS"] == "2"
    finally:
        os.environ.pop("PORTABLE_SPFS_ROWS", None)


def test_original_runtime_task_tree_repeats_sequentially(tmp_path: Path) -> None:
    workdir = tmp_path / "repeat"
    text = vertical_slice(workdir)
    runtime = PortableScriptHostRuntime()
    try:
        assert runtime.run_text(text, workdir)
        first = outputs(workdir)
        assert runtime.run_text(text, workdir)
        assert outputs(workdir) == first
        assert not (workdir / "bad.txt").exists()
    finally:
        os.environ.pop("PORTABLE_SPFS_ROWS", None)


def test_original_runtime_process_environment_is_not_per_run_state(
    tmp_path: Path,
) -> None:
    os.environ.pop("PORTABLE_SPFS_LEAK", None)
    workdir = tmp_path / "environment-leak"
    source = workdir / "rows.csv"
    marker = workdir / "leaked.txt"
    workdir.mkdir(parents=True)
    source.write_text("id\n1\n", encoding="utf-8")
    set_env = script(
        block(f'/UTILITIES={{ROWS-IN-FILE}} "{source}" "PORTABLE_SPFS_LEAK" "N"')
    )
    observe_env = script(
        block('/UTILITIES={IF-THEN} "PORTABLE_SPFS_LEAK" "GT" "0"'),
        block("/WRITE-FILE=Y", f"/CSV={marker}", body="leaked"),
        block("/UTILITIES={END-IF}"),
    )

    runtime = PortableScriptHostRuntime()
    try:
        assert runtime.run_text(set_env, workdir)
        assert os.environ["PORTABLE_SPFS_LEAK"] == "1"
        assert runtime.run_text(observe_env, workdir)
        assert marker.read_text(encoding="utf-8") == "leaked\n"
    finally:
        os.environ.pop("PORTABLE_SPFS_LEAK", None)


def test_spfglobals_command_line_state_is_shared_between_threads() -> None:
    from SPFLib.SPFSQL3 import SPFManager

    barrier = threading.Barrier(2)

    def worker(label: str) -> tuple[str, str]:
        manager = SPFManager()
        manager.gCommandLineArguments = [label]
        barrier.wait()
        return label, manager.gCommandLineArguments[0]

    with ThreadPoolExecutor(max_workers=2) as executor:
        observed = list(executor.map(worker, ("THREAD_A", "THREAD_B")))

    assert len({value for _, value in observed}) == 1
    assert any(expected != value for expected, value in observed)


def test_original_runtime_is_safe_when_concurrent_jobs_use_process_isolation(
    tmp_path: Path,
) -> None:
    repo_root = Path(__file__).resolve().parents[2]
    jobs: list[tuple[Path, Path]] = []
    for name in ("job-a", "job-b"):
        root = tmp_path / name
        vg2 = root / "job.spfsql"
        text = vertical_slice(root)
        vg2.write_text(text, encoding="utf-8")
        jobs.append((vg2, root))

    code = (
        "from pathlib import Path; "
        "from scripthost_portable import PortableScriptHostRuntime; "
        "import sys; "
        "raise SystemExit(0 if PortableScriptHostRuntime().run_file("
        "Path(sys.argv[1]), Path(sys.argv[2])) else 1)"
    )
    child_env = dict(os.environ)
    child_env.pop("PORTABLE_SPFS_ROWS", None)
    child_env["PYTHONPATH"] = os.pathsep.join(
        [
            str(repo_root / "src"),
            str(repo_root),
            child_env.get("PYTHONPATH", ""),
        ]
    )

    def run_job(job: tuple[Path, Path]) -> subprocess.CompletedProcess[str]:
        vg2, root = job
        return subprocess.run(
            [sys.executable, "-c", code, str(vg2), str(root)],
            cwd=repo_root,
            env=child_env,
            text=True,
            capture_output=True,
            timeout=30,
            check=False,
        )

    with ThreadPoolExecutor(max_workers=2) as executor:
        results = list(executor.map(run_job, jobs))

    assert [result.returncode for result in results] == [0, 0], [
        result.stdout + result.stderr for result in results
    ]
    for _, root in jobs:
        assert outputs(root)["verified.txt"] == "verified\n"
        assert not (root / "bad.txt").exists()
    assert "PORTABLE_SPFS_ROWS" not in os.environ


def test_original_site_loop_executes(tmp_path: Path) -> None:
    original_dir = tmp_path / "site-original"
    original_dir.mkdir()

    def make_text(root: Path) -> str:
        return script(
            block('/UTILITIES={SITE-LOOP} "A,B"'),
            block(
                "/WRITE-FILE=Y",
                f"/CSV={root / 'site_<<<spf-site-for-file-name>>>.txt'}",
                body="<<<spf-site>>>",
            ),
            block("/UTILITIES={END-LOOP}"),
        )

    assert PortableScriptHostRuntime().run_text(make_text(original_dir), original_dir)

    for site in ("A", "B"):
        assert (original_dir / f"site_{site}.txt").read_text(encoding="utf-8").rstrip(
            "\n"
        ) == site


def test_original_run_loop_final_chunk(tmp_path: Path) -> None:
    original_dir = tmp_path / "run-original"
    original_dir.mkdir()
    (original_dir / "input.csv").write_text(
        "id,value\n1,a\n2,b\n3,c\n", encoding="utf-8"
    )

    def make_text(root: Path) -> str:
        return script(
            block(
                f'/UTILITIES={{RUN-LOOP}} "{root / "input.csv"}" "{root / "chunk.csv"}" "2" "N"'
            ),
            block("/UTILITIES={END-LOOP}"),
        )

    assert PortableScriptHostRuntime().run_text(make_text(original_dir), original_dir)

    original = (original_dir / "chunk.csv").read_text(encoding="utf-8-sig").strip()
    assert original == "id,value\n3,c"


def test_original_local_hpc_scope_executes(tmp_path: Path) -> None:
    original_dir = tmp_path / "hpc-original"
    original_dir.mkdir()

    def make_text(root: Path) -> str:
        return script(
            block('/UTILITIES={BEGIN-HPC} "LOCAL"'),
            block("/WRITE-FILE=Y", f"/CSV={root / 'inside.txt'}", body="inside"),
            block("/UTILITIES={END-HPC}"),
        )

    assert PortableScriptHostRuntime().run_text(make_text(original_dir), original_dir)
    assert (original_dir / "inside.txt").read_text(encoding="utf-8").rstrip(
        "\n"
    ) == "inside"


def test_original_getquery_representative_routing(tmp_path: Path) -> None:
    from scripthost_portable.runtime import _spf_manager_type

    manager = _spf_manager_type()()
    manager.gCommandLineArguments = [
        str(tmp_path / "SPFSQL3.py"),
        f'/MYLOCAL="{tmp_path}"',
        "/EXECMODE=UT",
    ]

    cases = (
        (
            block("/WRITE-FILE=Y", f"/CSV={tmp_path / 'x.txt'}", body="x"),
            "WriteFileTask",
            "write_file",
        ),
        (block('/UTILITIES={FOR-LOOP} "0" "1" "1" "x" "N"'), "ForLoopTask", None),
        (
            block(
                "/REPORT=HTML-RUN",
                "/WRITE-FILE=Y",
                f"/CSV={tmp_path / 'ignored.txt'}",
                body="ignored",
            ),
            "HTMLRunTask",
            "report.html_run",
        ),
    )

    for index, (text, original_class, _target) in enumerate(cases):
        original_task = manager.GetQuery(manager.gMyLocal, text, index, False)
        assert original_task.__class__.__name__ == original_class


def test_original_html_run_css_executes_on_linux(
    tmp_path: Path,
) -> None:
    root = tmp_path / "report"
    root.mkdir()
    css = root / "portable_report.css"
    repo_root = Path(__file__).resolve().parents[2]
    fixture = (repo_root / "tests" / "fixtures" / "html_test.txt").read_text(
        encoding="utf-8-sig"
    )
    report_block = next(
        segment for segment in fixture.split(DELIM) if "/REPORT=HTML-RUN" in segment
    ).strip()
    report_block = report_block.replace("sqlpathfinder_style_1.css", str(css))

    assert PortableScriptHostRuntime().run_text(report_block, root)
    generated = css.read_text(encoding="utf-8")
    assert "table.tblin" in generated
    assert "background-color" in generated


def test_windows_db_transport_fails_only_when_invoked() -> None:
    from SPFLib.SPFSQL3 import SPFManager, dbDriverBase

    assert SPFManager is not None
    with pytest.raises(RuntimeError, match="database transport is unavailable"):
        dbDriverBase()


def test_spfglobals_builtin_reset_leaves_concrete_per_run_state_shared(
    tmp_path: Path,
) -> None:
    repo_root = Path(__file__).resolve().parents[2]
    code = r"""
from SPFLib.SPFSQL3 import SPFManager

first = SPFManager()
first.gCommandLineArguments = ["SPFSQL3.py", "/EXECMODE=UT"]
first_run_id = first.gRNStr
first.g_CWCtr = 7
first.g_ChartCtr = 9

second = SPFManager()
second.gCommandLineArguments = ["SPFSQL3.py"]

print("execution_mode=" + second.gExecutionMode)
print("same_run_id=" + str(second.gRNStr == first_run_id))
print("cw_counter=" + str(second.g_CWCtr))
print("chart_counter=" + str(second.g_ChartCtr))
"""
    env = dict(os.environ)
    env["PYTHONPATH"] = os.pathsep.join(
        [
            str(repo_root / "src" / "scripthost_portable" / "_vendor" / "SPSQL3_py"),
            str(repo_root),
            env.get("PYTHONPATH", ""),
        ]
    )
    result = subprocess.run(
        [sys.executable, "-c", code],
        cwd=tmp_path,
        env=env,
        text=True,
        capture_output=True,
        timeout=30,
        check=False,
    )
    assert result.returncode == 0, result.stderr
    lines = set(result.stdout.splitlines())
    assert "execution_mode=Normal" in lines
    assert "same_run_id=True" in lines
    assert "cw_counter=7" in lines
    assert "chart_counter=9" in lines


def test_original_report_defer_layout_delete_lifecycle_characterization_on_linux(
    tmp_path: Path,
) -> None:
    root = tmp_path / "report-lifecycle"
    root.mkdir()
    (root / "schema").mkdir()
    data = root / "report.csv"
    data.write_text("total_pcg,total_flag,ce%\n10,2,80%\n20,1,95%\n", encoding="utf-8")
    css = root / "portable_report.css"
    final = root / "portable_report.htm"

    repo_root = Path(__file__).resolve().parents[2]
    fixture = (repo_root / "tests" / "fixtures" / "html_test.txt").read_text(
        encoding="utf-8-sig"
    )
    css_block = next(
        segment for segment in fixture.split(DELIM) if "/REPORT=HTML-RUN" in segment
    ).strip()
    css_block = css_block.replace("sqlpathfinder_style_1.css", str(css))

    defer_block = next(
        segment
        for segment in fixture.split(DELIM)
        if "/REPORT=HTML-DEFER" in segment and "/ID=MYREPORT5" in segment
    ).strip()
    source_line = next(
        line for line in defer_block.splitlines() if line.startswith("INPUT-FILE<\\\\>")
    )
    defer = defer_block.replace(
        source_line,
        f"INPUT-FILE<\\\\>{data}<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>",
    )
    defer = defer.replace("sqlpathfinder_style_1.css", str(css))

    layout_body = "\n".join(
        [
            '<table class="tblout"><tr class="tblout"><td class="tblout">',
            f":FILE:{final}",
            f":CSS:{css}",
            ":CSSEMBED:N",
            ":RR:NO",
            ":B:Y",
            ":TITLE:Portable ScriptHost Report",
            '<table class="tblout">',
            '<tr class="tblout">',
            '<td class="tblout">',
            "HTM:MYREPORT5",
            "</td>",
            "</tr>",
            "</table>",
            "</td></tr></table>",
        ]
    )
    layout = block("/REPORT=HTML-LAYOUT", "/OUTLOOK=N", body=layout_body)
    cleanup = block("/REPORT=HTML-DELETE", body="N/A")
    text = script(css_block, defer, layout, cleanup)

    assert PortableScriptHostRuntime().run_text(text, root)
    generated = final.read_text(encoding="utf-8-sig")
    assert "Portable ScriptHost Report" in generated
    # Preserving source path case lets original SQLite/report loading work on Linux.
    assert "80%" in generated
    assert "<table" in generated.lower()
    # Shared portable SPFDelete now also completes the original report cleanup.
    assert not list(root.glob("*_MYREPORT5_tmp_.ini"))


def test_real_22844_builds_original_task_tree_on_linux() -> None:
    from scripthost_portable.runtime import _spf_manager_type

    repo_root = Path(__file__).resolve().parents[2]
    fixture = repo_root / "tests" / "fixtures" / "22844.spfsql"
    text = fixture.read_text(encoding="utf-8-sig")

    manager = _spf_manager_type()()
    manager.gCommandLineArguments = [
        str(
            repo_root
            / "src"
            / "scripthost_portable"
            / "_vendor"
            / "SPSQL3_py"
            / "SPFSQL3.py"
        ),
        f'/MYLOCAL="{fixture.parent}"',
        "/EXECMODE=UT",
    ]
    segments = [segment for segment in text.split(DELIM) if segment.strip()]
    original = manager.Process_Query(
        0,
        len(segments),
        list(segments),
        manager.gMyLocal,
        manager.gMyEXEDir,
        None,
        len(segments),
        manager.gRNStr,
        manager.TMP_F_NAME,
        None,
        False,
    )

    def flatten_original(tasks):
        for task in tasks:
            yield task
            yield from flatten_original(task.childTasksList)

    original_tasks = list(flatten_original(original))
    assert original_tasks
    assert len(original_tasks) == len(segments)
    assert any(task.__class__.__name__ == "IfThenTask" for task in original_tasks)
    assert any(task.__class__.__name__.startswith("nq") for task in original_tasks)
