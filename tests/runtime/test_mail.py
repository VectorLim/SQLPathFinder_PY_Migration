import importlib
import sys
from email import message_from_string
from pathlib import Path
from types import SimpleNamespace
from zipfile import ZipFile

import pytest

from vg2c.runtime import JobRuntime, send_mail


@pytest.fixture(autouse=True)
def production_mode(monkeypatch):
    monkeypatch.setenv("ENV_MODE", "prod")


@pytest.fixture
def delivery(monkeypatch):
    calls = []
    monkeypatch.setitem(
        sys.modules, "datasyncx", SimpleNamespace(send_msg=lambda **kw: calls.append(kw))
    )
    return calls


def test_literal_body_and_default_sender(delivery, tmp_path):
    send_mail("first@intel.com,second@intel.com", "Hello", "Unicode: café", workdir=tmp_path)
    assert delivery == [
        {
            "mail_title": "Hello",
            "mail_content": "Unicode: café",
            "receiver": "first@intel.com,second@intel.com",
            "file": None,
            "format_": "plain",
        }
    ]


@pytest.mark.parametrize(
    "filename, format_", [("body.HTM", "html"), ("body.html", "html"), ("body.txt", "plain")]
)
def test_body_and_attachment_use_job_root(delivery, tmp_path, monkeypatch, filename, format_):
    work = tmp_path / "work"
    work.mkdir()
    monkeypatch.chdir(tmp_path)
    (tmp_path / filename).write_text("wrong root")
    (work / filename).write_text("<p>café</p>", encoding="utf-8")
    attachment = work / "report.csv"
    attachment.write_bytes(b"A\n1\n")
    job = JobRuntime(tmp_path, work)
    job.send_mail(
        "user@intel.com", "Report", filename, ["missing.csv", "report.csv"], "sender@intel.com"
    )
    assert delivery == [
        {
            "mail_title": "Report",
            "mail_content": "<p>café</p>",
            "receiver": "user@intel.com",
            "file": str(attachment),
            "format_": format_,
            "sender": "sender@intel.com",
        }
    ]


def test_missing_body_file_remains_literal(delivery, tmp_path):
    send_mail("user@intel.com", "Report", "missing.htm", ["missing.csv"], workdir=tmp_path)
    assert delivery[0]["mail_content"] == "missing.htm"
    assert delivery[0]["format_"] == "plain"
    assert delivery[0]["file"] is None


@pytest.mark.parametrize("fail", [False, True])
def test_multiple_attachments_are_bundled_and_cleaned(monkeypatch, tmp_path, fail):
    files = {"first.csv": b"A\n1\n", "second.csv": b"B\n2\n"}
    for filename, content in files.items():
        (tmp_path / filename).write_bytes(content)
    sent_paths = []

    def capture(**kwargs):
        path = Path(kwargs["file"])
        sent_paths.append(path)
        assert path.name == "attachments.zip"
        with ZipFile(path) as archive:
            assert {name: archive.read(name) for name in archive.namelist()} == files
        if fail:
            raise RuntimeError("delivery failed")

    monkeypatch.setitem(sys.modules, "datasyncx", SimpleNamespace(send_msg=capture))
    if fail:
        with pytest.raises(RuntimeError, match="delivery failed"):
            send_mail("user@intel.com", "Report", "Body", list(files), workdir=tmp_path)
    else:
        send_mail("user@intel.com", "Report", "Body", list(files), workdir=tmp_path)
    assert len(sent_paths) == 1
    assert not sent_paths[0].parent.exists()


def test_duplicate_attachment_filenames_fail_before_delivery(delivery, tmp_path):
    (tmp_path / "subdir").mkdir()
    (tmp_path / "same.csv").write_text("one")
    (tmp_path / "subdir/same.csv").write_text("two")
    with pytest.raises(ValueError, match="distinct filenames"):
        send_mail(
            "user@intel.com", "Report", "Body", ["same.csv", "subdir/same.csv"], workdir=tmp_path
        )
    assert not delivery


def test_installed_datasyncx_builds_message_without_live_smtp(monkeypatch, tmp_path):
    pytest.importorskip("datasyncx")
    module = importlib.import_module("datasyncx.utils.send_mail")
    sent = []
    closed = []

    class SMTP:
        def sendmail(self, sender, recipients, message):
            sent.append((sender, recipients, message_from_string(message)))

        def quit(self):
            closed.append(True)

    monkeypatch.setattr(
        module, "get_smtp_service_module", lambda: SimpleNamespace(get_smtp_service=SMTP)
    )
    (tmp_path / "body.htm").write_text("<p>café</p>", encoding="utf-8")
    attachment = tmp_path / "report.csv"
    attachment.write_bytes(b"A\n1\n")
    send_mail(
        "first@intel.com, second@intel.com",
        "Report",
        "body.htm",
        [str(attachment)],
        workdir=tmp_path,
    )
    sender, recipients, message = sent[0]
    assert sender == "atmanalytic@intel.com"
    assert set(recipients) == {"first@intel.com", "second@intel.com"}
    assert message["Subject"] == "Report"
    body, attached = message.get_payload()
    assert body.get_content_type() == "text/html"
    assert body.get_payload(decode=True).decode(body.get_content_charset()) == "<p>café</p>"
    assert attached.get_payload(decode=True) == b"A\n1\n"
    assert "report.csv" in attached["Content-Disposition"]
    assert closed == [True]
