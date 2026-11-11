"""Hermetic Linux wheel check: compile once, execute only the installed runtime.

This probe generates synthetic data locally and never accesses production inputs.
Invoke from CI after installing the built wheel into an isolated venv.
"""

from __future__ import annotations

import argparse
import ast
import os
from pathlib import Path
import shutil
import subprocess
import tempfile
import zipfile

from vg2c import translate

D = "<" + "\\" * 2 + ">"


def block(options: str, body: str = "") -> str:
    return (
        "<OPTIONS>\n" + options + "\n</OPTIONS>\n" + body + "\n<---- New Query ---->\n"
    )


def run_python(python: Path, args: list[str], cwd: Path, *, success: bool = True):
    env = os.environ.copy()
    env.pop("PYTHONPATH", None)
    env.pop("PYTHONHOME", None)
    result = subprocess.run(
        [str(python), "-I", *args],
        cwd=cwd,
        env=env,
        capture_output=True,
        text=True,
        encoding="utf-8",
        check=False,
    )
    if (result.returncode == 0) != success:
        raise AssertionError(
            f"Installed interpreter returned {result.returncode}: {args}\n"
            f"stdout:\n{result.stdout}\nstderr:\n{result.stderr}"
        )
    return result


def main(python: Path, wheel: Path) -> None:
    with zipfile.ZipFile(wheel) as archive:
        names = set(archive.namelist())
        retired = {
            "vg2c/runtime/aed.py",
            "vg2c/runtime/aed_updater.py",
            "vg2c/runtime/csr.py",
            "vg2c/resolver/csr.py",
            "vg2c/utilities/csr.py",
            "vg2c/runtime/append.py",
        }
        assert not names.intersection(retired), "Wheel contains retired runtime modules"
        for required in (
            "vg2c/project_bundle.py",
            "vg2c/project_template/Dockerfile",
            "vg2c/project_template/.dockerignore",
            "vg2c/project_template/.gitignore",
            "vg2c/project_template/.github/workflows/release.yml",
            "vg2c/project_template/release.yaml",
            "vg2c/project_template/automation/__main__.py",
            "vg2c/project_template/automation/cluster.py",
            "vg2c/project_template/Start-Release-Runner.cmd",
            "vg2c/project_template/runner/Dockerfile",
            "vg2c/project_template/runner/start-runner.ps1",
            "vg2c/project_template/dependencies/requirements.txt",
            "vg2c/project_template/dependencies/aed_client_apikey-6.2.0-py3-none-any.whl",
            "vg2c/runtime/__init__.py",
            "vg2c/runtime/job.py",
            "vg2c/runtime/html.py",
            "vg2c/runtime/query.py",
            "vg2c/runtime/crosstab.py",
            "vg2c/runtime/macros.py",
            "vg2c/runtime/aed_client.py",
            "vg2c/runtime/aed_api.py",
        ):
            assert required in names, f"Wheel omits {required}"

    with tempfile.TemporaryDirectory(prefix="vg2c-s05-") as temporary:
        root = Path(temporary).resolve()
        staging = root / "compiler-input"
        staging.mkdir()
        css = "\n".join(
            [
                D.join(["TYPE", "CSS"]),
                D.join(["CSS", "report.css"]),
                D.join(["FORMAT", "Column-Headers", "color:blue"]),
            ]
        )
        report = "\n".join(
            [
                D.join(["TYPE", "HTML"]),
                D.join(["INPUT-FILE", "final.csv"]),
                D.join(["COLUMN-DATA", "", "LOT", "A", "B"]),
                D.join(["COLUMN-HEADERS", "", "Lot", "Alpha", "Beta"]),
            ]
        )
        source = staging / "synthetic.txt"
        source.write_text(
            block(
                "/ENGINE=SQLite\n/CSV=long.csv",
                "SELECT '雪' AS lot, 'A' AS metric, '1' AS reading "
                "UNION ALL SELECT '雪','B','2'",
            )
            + block(
                "/ENGINE=SQLite\n/TABLE=long.csv\n/CSV=wide.csv"
                "\n/CTROW=lot\n/CTHEADER=metric\n/CTVAL=reading\n/CTARRAY=a0,42",
                "SELECT lot, metric, reading FROM [long]",
            )
            + block(
                "/ENGINE=SQLite\n/TABLE=wide.csv\n/CSV=final.csv",
                "SELECT a0.LOT, CrossTab->[[a0,42;:A]] FROM [wide] a0",
            )
            + block("/REPORT=HTML-RUN", css)
            + block("/REPORT=HTML-DEFER\n/ID=R", report)
            + block(
                "/REPORT=HTML-LAYOUT",
                ":FILE:linked.html\n:CSS:report.css\n:CSSEMBED:N\n<h1>Original</h1>\nHTM:R",
            )
            + block(
                "/REPORT=HTML-LAYOUT",
                ":FILE:embedded.html\n:CSS:report.css\n:CSSEMBED:Y\n<h1>Original</h1>\nHTM:R",
            ),
            encoding="utf-8",
        )
        emitted_main = translate(source)
        project = root / "生成專案 with spaces"
        shutil.copytree(emitted_main.parent, project)
        source.unlink()
        shutil.rmtree(emitted_main.parent)
        shutil.rmtree(staging)
        main_file = project / "main.py"
        assert ast.parse(main_file.read_text(encoding="utf-8"))
        source_assets = sorted(p for p in project.rglob("*") if p.is_file())
        assert any(p.suffix == ".sql" for p in source_assets)
        assert any(p.suffix == ".css" for p in source_assets)
        assert len(list((project / "html").glob("*.html"))) == 2
        assert not list(project.rglob("*.report.json"))
        assert (project / "Dockerfile").is_file()
        assert (project / "release.yaml").is_file()
        assert (project / ".github/workflows/release.yml").is_file()
        assert (project / "automation/__main__.py").is_file()
        assert not (project / "entrypoint.py").exists()
        assert not (project / "func.json").exists()
        assert "def handle(" in main_file.read_text(encoding="utf-8")
        assert (project / "dependencies/vg2c/runtime/job.py").is_file()
        assert not (project / "dependencies/vg2c/compilation.py").exists()

        unrelated = root / "unrelated-cwd"
        unrelated.mkdir()
        import_test = (
            "import pathlib,sys,vg2c.runtime as runtime;"
            "p=pathlib.Path(runtime.__file__).resolve();"
            "assert 'site-packages' in str(p), p;"
            "assert not any(name.startswith(('vg2c.compilation','vg2c.emitter',"
            "'vg2c.frontend','vg2c.resolver','vg2c.utilities'))"
            " for name in sys.modules), list(sys.modules);"
            "print('INSTALLED_RUNTIME_PATH',p)"
        )
        print(run_python(python, ["-c", import_test], unrelated).stdout.strip())

        run_python(python, [str(main_file)], project)
        default = project / "output"
        assert (default / "final.csv").read_text(encoding="utf-8").splitlines()[
            0
        ] == "LOT,A,B"
        assert "雪" in (default / "final.csv").read_text(encoding="utf-8")
        assert (default / "42_A0.ini").read_text(encoding="utf-8") == "A\tB"
        assert 'href="report.css"' in (default / "linked.html").read_text(
            encoding="utf-8"
        )
        assert "color:blue" in (default / "embedded.html").read_text(encoding="utf-8")
        assert "Alpha" in (default / "linked.html").read_text(encoding="utf-8")

        sql_source = next(
            p
            for p in (project / "sql").glob("*.sql")
            if "'1' AS reading" in p.read_text()
        )
        sql_source.write_text(
            sql_source.read_text().replace("'1' AS reading", "'9' AS reading")
        )
        css_source = next((project / "styles").glob("*.css"))
        css_source.write_text("th {color:purple;}\n", encoding="utf-8")
        html_sources = list((project / "html").glob("*.html"))
        for path in html_sources:
            path.write_text(
                path.read_text().replace("Original", "Edited"), encoding="utf-8"
            )
        fragment = next((project / "html" / "reports").glob("*.table.html"))
        fragment.write_text(
            fragment.read_text().replace("Beta</th>", "Edited Beta</th>"),
            encoding="utf-8",
        )
        css_source.chmod(0o444)
        changed = {p.relative_to(project): p.read_bytes() for p in source_assets}
        edited_project = root / "修改專案 with spaces"
        shutil.copytree(
            project, edited_project, ignore=shutil.ignore_patterns("output")
        )
        custom = edited_project / "output"
        run_entrypoint = (
            "import inspect,runpy,sys;"
            "run=runpy.run_path(sys.argv[1],run_name='wheel_probe')['run'];"
            "assert not inspect.signature(run).parameters;run()"
        )
        run_python(
            python, ["-c", run_entrypoint, str(edited_project / "main.py")], unrelated
        )
        assert "9" in (custom / "final.csv").read_text(encoding="utf-8")
        assert "Edited Beta" in (custom / "linked.html").read_text(encoding="utf-8")
        assert "Edited" in (custom / "embedded.html").read_text(encoding="utf-8")
        assert "color:purple" in (custom / "embedded.html").read_text(encoding="utf-8")
        assert "color:purple" in (custom / "report.css").read_text(encoding="utf-8")
        assert {
            p.relative_to(project): p.read_bytes() for p in source_assets
        } == changed
        assert "color:blue" in (default / "embedded.html").read_text(encoding="utf-8")

        unicode_sql = project / "sql" / "été.sql"
        shutil.copy2(sql_source, unicode_sql)
        unicode_test = (
            "import sys;from vg2c.runtime import JobRuntime,SqliteReader;"
            "JobRuntime(sys.argv[1],sys.argv[2]).sql('sql/été.sql',"
            "reader=SqliteReader(),output='résultat.csv')"
        )
        run_python(python, ["-c", unicode_test, str(project), str(custom)], unrelated)
        assert "雪" in (custom / "résultat.csv").read_text(encoding="utf-8")

        missing = sql_source.with_suffix(".sql.missing")
        sql_source.rename(missing)
        failed = run_python(python, [str(main_file)], unrelated, success=False)
        assert "FileNotFoundError" in failed.stderr, failed.stderr
        missing.rename(sql_source)

        # Unlike the synthetic probe above, this translates the *actual* fixture
        # and runs the relocated output under the installed, isolated wheel.
        real_fixture = (
            Path(__file__).resolve().parents[1] / "fixtures" / "script_short.txt"
        )
        assert real_fixture.is_file(), real_fixture
        fixture_root = root / "real-fixture-source"
        fixture_root.mkdir()
        real_source = fixture_root / real_fixture.name
        shutil.copyfile(real_fixture, real_source)
        real_main = translate(real_source)
        real_project = root / "relocated VG2 fixture"
        shutil.copytree(real_main.parent, real_project)
        shutil.rmtree(fixture_root)
        real_workdir = real_project / "output"
        real_workdir.mkdir()
        (real_workdir / "ww_yield.csv").write_text(
            "owner\nALICE\nBOB\nALICE\n雪\n", encoding="utf-8"
        )
        run_python(python, [str(real_project / "main.py")], unrelated)
        with (real_workdir / "owner.csv").open(encoding="utf-8", newline="") as stream:
            import csv

            actual = list(csv.reader(stream))
        assert actual[0] == ["owner"], actual
        assert sorted(actual[1:]) == [["ALICE"], ["BOB"], ["雪"]], actual
        print("STAGE2_REAL_FIXTURE_INSTALLED_WHEEL_PASS: script_short.txt")

        print(
            "SESSION05_INSTALLED_LINUX_PASS: wheel-only imports; moved project; "
            "SQL/pivot/CTARRAY; two HTML pages; CSS; edits; alternate CWD; "
            "parameterless run; Unicode; read-only source; missing-asset error"
        )


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("--python", required=True, type=Path)
    parser.add_argument("--wheel", required=True, type=Path)
    arguments = parser.parse_args()
    main(arguments.python, arguments.wheel.resolve())
