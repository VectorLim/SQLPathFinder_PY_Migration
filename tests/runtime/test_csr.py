"""CSR lot input, comment and AED status, without a manufacturing service."""

import os
from types import SimpleNamespace

import pytest

from vg2c.runtime import AEDClient
from vg2c.runtime.job import csr_comment, csr_lots
from vg2c.runtime.environment import read_environment
from vg2c.runtime.job import JobRuntime


def lots(tmp_path, text, column="lot"):
    (tmp_path / "input.csv").write_text(text)
    return csr_lots("input.csv", column, workdir=tmp_path)


def test_lots_are_distinct_in_order_and_header_is_case_insensitive(tmp_path):
    assert lots(tmp_path, " LOT ,x\nL2,1\nL1,2\nL2,3\n") == ["L2", "L1"]


@pytest.mark.parametrize(
    "text, message",
    [
        ("other\nL1\n", "no 'lot' column"),
        ("lot,x\n,1\n", "without a lot"),
        ("lot,x\nL1,1,extra\n", "without a lot"),
        ("lot\nbad/lot\n", "CSR lot"),
    ],
)
def test_invalid_lot_input_fails(tmp_path, text, message):
    with pytest.raises(ValueError, match=message):
        lots(tmp_path, text)


def test_comment_requires_ccb():
    assert csr_comment("investigate", "42") == "investigate\nCCB: 42"
    with pytest.raises(ValueError, match="CCB"):
        csr_comment("investigate", " ")


def job_with_aed(tmp_path, mode="prod", fail_lot=None):
    (tmp_path / "output").mkdir(exist_ok=True)
    (tmp_path / "output" / "input.csv").write_text("lot\nL1\nL2\nL3\n")
    job = JobRuntime(tmp_path, tmp_path / "output", environ={"ENV_MODE": mode})
    calls = []

    def lot_set_attr(*args):
        calls.append(args)
        if args[0] == fail_lot:
            raise OSError("down")
        return {"Success": True}

    def run():
        aed = job.aed("A15")
        if aed is not None:
            aed._client_factory = lambda f: SimpleNamespace(lot_set_attr=lot_set_attr)
        for lot in job.csr_lots("input.csv", "lot"):
            aed.lot_set_attr(lotid=lot, attributeid="1064", attributevalue="Y")

    return job, calls, run


def test_status_reports_accepted_requests(tmp_path):
    job, calls, run = job_with_aed(tmp_path)
    assert job.csr_status == "No CSR AED action was executed"
    run()
    assert calls == [("L1", "1064", "Y"), ("L2", "1064", "Y"), ("L3", "1064", "Y")]
    assert job.csr_status.startswith("AED accepted 3 request(s)")


def test_test_mode_skips_lots_and_client(tmp_path):
    job, calls, run = job_with_aed(tmp_path, mode="test")
    run()
    assert calls == [] and job.csr_status == "No CSR AED action was executed"


def test_failed_write_stops_remaining_calls(tmp_path):
    job, calls, run = job_with_aed(tmp_path, fail_lot="L2")
    with pytest.raises(RuntimeError, match="lot_set_attr request failed"):
        run()
    assert len(calls) == 2


def test_new_client_resets_status(tmp_path):
    job, calls, run = job_with_aed(tmp_path)
    run()
    assert isinstance(job.aed("A15"), AEDClient)
    assert job.csr_status == "No CSR AED action was executed"


def test_dotenv_is_reloaded_with_process_override_and_no_environment_mutation(
    tmp_path,
    monkeypatch,
):
    monkeypatch.delenv("X_API_KEY", raising=False)
    monkeypatch.setenv("ENV_MODE", "test")
    path = tmp_path / ".env"
    path.write_text("ENV_MODE=prod\nX_API_KEY=synthetic-first\n")
    before = dict(os.environ)
    first = read_environment(tmp_path)
    path.write_text("X_API_KEY=synthetic-second\n")
    second = read_environment(tmp_path)
    assert first["X_API_KEY"] == "synthetic-first"
    assert second["X_API_KEY"] == "synthetic-second"
    assert first["ENV_MODE"] == second["ENV_MODE"] == "test"
    assert dict(os.environ) == before
    monkeypatch.setenv("ENV_MODE", "")
    assert read_environment(tmp_path)["ENV_MODE"] == ""


def test_shared_env_file_is_optional(tmp_path, monkeypatch):
    monkeypatch.setenv("ENV_MODE", "test")
    assert read_environment(tmp_path)["ENV_MODE"] == "test"
