"""Source-driven CSR translation and offline generated-project execution."""

from pathlib import Path

import pytest

from vg2c import compile_document, translate


HEADERS = "HOLD_LOT,AUTO_CONTAIN,LOT,EMAIL,CCB_NUMBER,HOLD_NOTE,HOLDCATEGORY,Module"


def block(options, body=""):
    return f"<OPTIONS>\n{options}\n</OPTIONS>\n{body}\n<---- New Query ---->\n"


def source(*, ambiguous=False):
    producer = block('/ENGINE=SQLite\n/CSV=RESULT_<<<LOT>>>.csv\n/HEADERS=' + HEADERS,
                     "select '<<<LOT>>>' as HOLD_LOT, 'Y' as AUTO_CONTAIN, '<<<LOT>>>' as LOT, "
                     "'recipient@example.test' as EMAIL, '42' as CCB_NUMBER, 'note & detail' as HOLD_NOTE, "
                     "'NCO' as HOLDCATEGORY, 'IAM' as Module")
    return (
        block('/UTILITIES={START-MACRO} "config.csv" "N"')
        + block('/UTILITIES={IF-THEN} "SIGNAL" "GT" "0" "" "" "" ""')
        + block('/UTILITIES={IF-THEN} "VAR(<<<DCSR>>>)" "EQS" "Y" "" "" "" ""')
        + producer + (producer if ambiguous else "")
        + block('/UTILITIES=RoboCopy.va "RESULT_*.csv" "." "<<<CSRPATH>>>" "5" "30" "N" "/MOV" "N"')
        + block('/UTILITIES={END-IF}')
        + block('/REPORT=HTML-LAYOUT', ':FILE:EMAIL:<<<EMAIL>>>\n:TITLE:CSR outcome\n<p>Lot on hold due to NCO.</p>')
        + block('/UTILITIES={IF-THEN} "HISTERR" "LE" "0" "" "" "" ""')
        + block('/WRITE-FILE=Y\n/CSV=history-written.txt', "history")
        + block('/UTILITIES={END-IF}')
        + block('/UTILITIES={END-IF}')
        + block('/UTILITIES={END-MACRO}')
        + block('/WRITE-FILE=Y\n/CSV=after.txt', "after")
    )


def generated(tmp_path):
    path = tmp_path / "workflow.txt"
    path.write_text(source())
    main = translate(path)
    namespace = {"__file__": str(main), "__name__": "offline_job"}
    exec(main.read_text(), namespace)
    work = main.parent / "output"
    work.mkdir()
    (work / "config.csv").write_text("LOT,SIGNAL,DCSR,HISTERR,EMAIL,CSRPATH\n"
                                   "L1,1,Y,0,recipient@example.test,never-copy\n")
    namespace.update(AED_FACILITY="A15", AED_HOLD_OPERATION="2303", AED_HOLD_CODE="NCO", AED_MODE="test")
    return main, namespace, work


def test_actual_sources_detect_csr_and_keep_queries():
    root = Path(__file__).resolve().parents[2]
    for name in ("CSR_IAM_v2.txt", "ICMPCS.txt"):
        result = compile_document(root / name)
        assert len(result.resolved.csr_submissions) == 1
        assert "aed.lot_hold(" in result.emitted.source
        assert "bootstrap_aed(" not in result.emitted.source
        sql = "\n".join(value for path, value in result.emitted.assets if path.endswith(".sql"))
        expected_window = "SYSDATE - 1" if name == "CSR_IAM_v2.txt" else "TRUNC(SYSDATE) - 2"
        assert expected_window in sql
        if name == "CSR_IAM_v2.txt":
            assert "[NCO_Risk] = '1'" in sql
        assert "MIPPATH" in result.emitted.source
        assert "1064" not in result.emitted.source


def test_detection_does_not_depend_on_block_numbers(tmp_path):
    path = tmp_path / "shifted.txt"
    path.write_text(block('/WRITE-FILE=Y\n/CSV=unrelated.txt', "unrelated") + source())
    result = compile_document(path)
    assert len(result.resolved.csr_submissions) == 1
    assert "job.write_file" in result.emitted.source


def test_ambiguous_producer_fails_with_source_location(tmp_path):
    path = tmp_path / "ambiguous.txt"
    path.write_text(source(ambiguous=True))
    with pytest.raises(ValueError, match="exactly one preceding") as error:
        compile_document(path)
    assert str(path) in str(error.value)


def test_else_branch_cannot_claim_positive_csr_gates(tmp_path):
    path = tmp_path / "else.txt"
    path.write_text(source().replace(
        '/UTILITIES={IF-THEN} "VAR(<<<DCSR>>>)" "EQS" "Y" "" "" "" ""\n</OPTIONS>',
        '/UTILITIES={IF-THEN} "VAR(<<<DCSR>>>)" "EQS" "Y" "" "" "" ""\n</OPTIONS>'
        + '\n<---- New Query ---->\n<OPTIONS>\n/UTILITIES={ELSE}\n</OPTIONS>',
    ))
    with pytest.raises(ValueError, match="gates"):
        compile_document(path)


def test_unrelated_copies_and_mms_remain_file_operations(tmp_path):
    path = tmp_path / "copy.txt"
    path.write_text(block('/UTILITIES=RoboCopy.va "RESULT_*.csv" "." "<<<MIPPATH>>>"'))
    result = compile_document(path)
    assert not result.resolved.csr_submissions
    assert "job.copy_file(" in result.emitted.source
    assert "AEDClient" not in result.emitted.source


def test_non_csr_payload_copy_to_csr_path_remains_a_file_operation(tmp_path):
    path = tmp_path / "ordinary.txt"
    path.write_text(block('/ENGINE=SQLite\n/CSV=RESULT_log.csv\n/HEADERS=LOG',
                          "select 'ordinary log' as LOG")
                    + block('/UTILITIES=RoboCopy.va "RESULT_*.csv" "." "<<<CSRPATH>>>"'))
    result = compile_document(path)
    assert not result.resolved.csr_submissions
    assert "job.copy_file(" in result.emitted.source


def test_relocated_generated_dry_run_sends_outcome_email_without_writes(tmp_path, monkeypatch):
    import shutil
    main, namespace, work = generated(tmp_path)
    moved = tmp_path / "relocated"
    shutil.move(main.parent, moved)
    namespace["__file__"] = str(moved / "main.py")
    work = moved / "output"
    calls = []
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: calls.append((args, kwargs)))
    monkeypatch.chdir(tmp_path)
    namespace["run"]()
    assert (work / "AED_results.csv").is_file()
    assert "dry_run" in (work / "AED_results.csv").read_text()
    assert not (work / "history-written.txt").exists()
    assert (work / "after.txt").exists()
    report = next(work.glob("*.html")).read_text()
    assert "Dry run: no AED" in report
    assert "Lot on hold" not in report
    assert "CCB: 42" in report
    assert len(calls) == 1


def test_failure_stops_next_lot_history_and_following_operations(tmp_path, monkeypatch):
    main, namespace, work = generated(tmp_path)
    mails = []
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: mails.append(args))
    class FailingClient:
        mode = "prod"
        def __init__(self, **kwargs):
            pass
        def lot_hold(self, **kwargs):
            raise RuntimeError("synthetic failure")
    namespace["AEDClient"] = FailingClient
    with pytest.raises(RuntimeError, match="synthetic failure"):
        namespace["run"]()
    assert "failed" in (work / "AED_results.csv").read_text()
    assert not (work / "history-written.txt").exists()
    assert not (work / "after.txt").exists()
    assert len(mails) == 1


def test_config_validation_and_disabled_gate(tmp_path, monkeypatch):
    main, namespace, work = generated(tmp_path)
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    namespace["AED_HOLD_CODE"] = None
    with pytest.raises(ValueError, match="hold code"):
        namespace["run"]()
    (work / "config.csv").write_text("LOT,SIGNAL,DCSR,HISTERR,EMAIL,CSRPATH\n"
                                   "L1,1,N,0,recipient@example.test,never-copy\n")
    namespace["run"]()
    assert (work / "history-written.txt").exists()


def test_mocked_production_partial_failure_keeps_completed_actions(tmp_path, monkeypatch):
    main, namespace, work = generated(tmp_path)
    sql = next(main.parent.glob("sql/*.sql"))
    sql.write_text(sql.read_text().rstrip() + " UNION ALL "
                   "select 'L2', 'Y', 'L2', 'recipient@example.test', '42', 'note', 'NCO', 'IAM'\n")
    calls = []
    class Client:
        mode = "prod"
        def __init__(self, **kwargs):
            pass
        def lot_hold(self, **kwargs):
            calls.append(kwargs["lotid"])
            if kwargs["lotid"] == "L2":
                raise RuntimeError("second lot failed")
            return {"Success": True}
    namespace["AEDClient"] = Client
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    with pytest.raises(RuntimeError, match="second lot failed"):
        namespace["run"]()
    assert calls == ["L1", "L2"]
    results = (work / "AED_results.csv").read_text()
    assert "accepted" in results and "failed" in results
    assert not (work / "history-written.txt").exists()
