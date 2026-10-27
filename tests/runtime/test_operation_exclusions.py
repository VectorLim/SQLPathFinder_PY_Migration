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


def test_every_action_method_is_tagged_and_any_tag_can_be_excluded(
    tmp_path, monkeypatch
):
    tags = {
        name: getattr(member, "operation", None)
        for name, member in vars(JobRuntime).items()
    }
    assert tags["send_mail"] == "email" and tags["submit_mms"] == "mms"
    assert tags["aed"] == tags["csr_lots"] == "csr"
    assert tags["sql"] == "sql" and tags["copy_file"] == "copy_file"
    assert tags["row_count"] is None and tags["read_macro_row"] is None
    monkeypatch.setattr(
        "vg2c.runtime.job.write_file", lambda *a, **kw: pytest.fail("excluded")
    )
    job = JobRuntime(
        tmp_path,
        tmp_path / "out",
        environ={"ENV_MODE": "test"},
        test_mode_excluded={"write_file"},
    )
    assert job.write_file("a.txt", "x") is None
    job = JobRuntime(tmp_path, tmp_path / "out", environ={"ENV_MODE": "test"})
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
