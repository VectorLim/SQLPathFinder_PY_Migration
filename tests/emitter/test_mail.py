from vg2c.compilation import compile_document
from vg2c.utilities.mail import MailService


def test_mail_service_delegates_to_shared_runtime(monkeypatch, tmp_path):
    calls = []
    monkeypatch.chdir(tmp_path)
    monkeypatch.setattr(
        "vg2c.runtime.mail.send_mail", lambda *args, **kwargs: calls.append((args, kwargs))
    )
    MailService().send("user@intel.com", "Hello", "Body", ["report.csv"], "sender@intel.com")
    assert calls == [
        (
            ("user@intel.com", "Hello", "Body", ["report.csv"], "sender@intel.com", True),
            {"workdir": tmp_path},
        )
    ]


def test_generated_email_uses_job_workdir(monkeypatch, tmp_path):
    source = tmp_path / "email.txt"
    source.write_text(
        '<OPTIONS>\n/UTILITIES="SQLPathFinder_Email.va" '
        '"report.csv" "self" "Report" "body.htm" "user@intel.com"\n'
        "</OPTIONS>\n<---- New Query ---->\n"
    )
    result = compile_document(source)
    assert not [d for d in result.diagnostics if d.level == "error"]
    calls = []
    monkeypatch.setattr(
        "vg2c.runtime.job.send_mail", lambda *args, **kwargs: calls.append((args, kwargs))
    )
    namespace = {"__file__": str(tmp_path / "main.py"), "__name__": "job"}
    exec(result.emitted.source, namespace)
    workdir = tmp_path / "output"
    namespace["run"]()
    assert calls == [
        (("user@intel.com", "Report", "body.htm", ["report.csv"], None, True), {"workdir": workdir})
    ]
