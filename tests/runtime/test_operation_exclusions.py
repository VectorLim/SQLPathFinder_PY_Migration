"""Test-mode exclusions are job-local and editable at the generated entrypoint."""

import sys

import pytest

from vg2c.runtime import AEDClient, JobRuntime


@pytest.mark.parametrize("mode", ["test", "prod"])
@pytest.mark.parametrize("excluded", [{"csr", "email", "mms"}, {"csr"}, set()])
def test_one_policy_controls_operations_and_delivery(
    tmp_path, monkeypatch, mode, excluded
):
    job = JobRuntime(
        tmp_path,
        tmp_path / "missing",
        environ={"ENV_MODE": mode},
        test_mode_excluded=excluded,
    )
    calls = []
    monkeypatch.setattr(
        "vg2c.runtime.job.send_mail", lambda *a, **kw: calls.append("email")
    )
    monkeypatch.setattr(
        "vg2c.runtime.job.copy_file", lambda *a, **kw: calls.append("mms")
    )
    monkeypatch.setattr(
        "vg2c.runtime.job.AEDClient", lambda *a, **kw: calls.append("csr")
    )

    job.aed("A15")
    job.send_mail("owner@test", "subject", "missing.htm")
    job.submit_mms("missing.csv", "missing-destination")
    assert calls == [
        name
        for name in ("csr", "email", "mms")
        if mode == "prod" or name not in excluded
    ]


@pytest.mark.parametrize(
    "method, operation, args, kwargs",
    [
        ("run_python", "run_python", ("missing.py",), {}),
        ("sql", "sql", ("missing.sql",), {"output": "out.csv"}),
        ("define_css", "define_css", ("out.css", "missing.css"), {}),
        ("delete_html", "delete_html", (), {}),
        ("html", "html", ("missing.htm",), {"output": "out.htm"}),
        ("write_file", "write_file", ("out.txt", "content"), {}),
        ("load_file", "load_file", ("out.txt", "missing.txt"), {}),
        ("update_file", "update_file", ("out.txt", "missing.txt"), {}),
        ("copy_file", "copy_file", ("missing.csv", "out.csv"), {}),
        ("rename_file", "rename_file", ("missing.csv", "out.csv"), {}),
        ("delete_files", "delete_files", (["keep.txt"],), {}),
        ("wait_file", "wait_file", ("missing.csv",), {"timeout": 0}),
        ("run_program", "run_program", (["missing-program"],), {}),
        ("smart_append", "smart_append", ("out.csv", "missing.csv"), {}),
        ("send_mail", "email", ("owner@test", "subject", "missing.htm"), {}),
        ("submit_mms", "mms", ("missing.csv", "out.csv"), {}),
        ("csr_lots", "csr", ("missing.csv", "lot"), {}),
        ("aed", "csr", ("A15",), {}),
    ],
)
def test_excluded_operation_has_no_side_effects(
    tmp_path, method, operation, args, kwargs
):
    job = JobRuntime(
        tmp_path,
        tmp_path / "out",
        environ={"ENV_MODE": "test"},
        test_mode_excluded={operation},
    )
    job.workdir.mkdir()
    kept = job.workdir / "keep.txt"
    kept.write_text("keep")
    job.reports["keep"] = "report"
    job._used_html_reports.add("keep")

    result = getattr(job, method)(*args, **kwargs)

    if method == "csr_lots":
        assert result == []
    else:
        assert result is None
    assert kept.read_text() == "keep"
    assert list(job.workdir.iterdir()) == [kept]
    assert job.files == {}
    assert job.reports == {"keep": "report"}
    assert job._used_html_reports == {"keep"}
    assert job.css_file is None
    assert job._python_globals is None
    assert job._aed is None


def test_skipped_csr_lots_returns_a_fresh_list(tmp_path):
    job = JobRuntime(tmp_path, tmp_path / "out", environ={"ENV_MODE": "test"})
    first = job.csr_lots("missing.csv", "lot")
    first.append("changed")
    assert job.csr_lots("missing.csv", "lot") == []


def test_environment_is_loaded_into_macros_once_per_run(tmp_path, monkeypatch):
    monkeypatch.delenv("ENV_MODE", raising=False)
    (tmp_path / ".env").write_text("ENV_MODE=test\n")
    first = JobRuntime.from_script(tmp_path / "main.py")
    (tmp_path / ".env").write_text("ENV_MODE=prod\n")
    second = JobRuntime.from_script(tmp_path / "main.py")
    assert first.macros["ENV_MODE"] == first.env_mode == "test"
    assert second.macros["ENV_MODE"] == second.env_mode == "prod"
    monkeypatch.setenv("ENV_MODE", "test")
    assert JobRuntime.from_script(tmp_path / "main.py").macros["ENV_MODE"] == "test"


def test_test_mode_does_not_import_mail_adapter_or_load_csr_inputs(
    tmp_path, monkeypatch
):
    from vg2c.runtime.mail import send_mail

    monkeypatch.setenv("ENV_MODE", "test")
    monkeypatch.setitem(sys.modules, "datasyncx", None)
    job = JobRuntime(tmp_path, tmp_path / "missing")
    send_mail("owner@test", "subject", "missing.htm", workdir=job.workdir)
    assert job.csr_lots("missing.csv", "lot") == []
    job.submit_mms("missing.csv", "missing-destination")
    assert not job.workdir.exists()


def test_removing_csr_exclusion_allows_aed_in_test_mode(tmp_path):
    job = JobRuntime(
        tmp_path,
        tmp_path / "out",
        environ={"ENV_MODE": "test"},
        test_mode_excluded={"email", "mms"},
    )
    assert isinstance(job.aed("A16"), AEDClient)
    assert (
        JobRuntime(tmp_path, tmp_path / "out", environ={"ENV_MODE": "test"}).aed("A16")
        is None
    )
