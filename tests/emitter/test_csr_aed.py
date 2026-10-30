"""Source-driven CSR translation and offline generated-project execution."""

import ast
import os
from pathlib import Path
from types import SimpleNamespace
from urllib.parse import quote

import pytest

from tests.fixtures.generated_job import capture_jobs
from vg2c import compile_document, translate
from vg2c.resolver.csr import csr_request
from vg2c.runtime import AEDClient

HEADERS = "HOLD_LOT,AUTO_CONTAIN,LOT,EMAIL,CCB_NUMBER,HOLD_NOTE,HOLDCATEGORY,Module"
PRODUCER_SQL = (
    "select distinct 'Y' as HOLD_LOT, 'N' as AUTO_CONTAIN, [lot] as LOT, "
    "'recipient@example.test' as EMAIL, '42' as CCB_NUMBER, 'note & detail' as HOLD_NOTE, "
    "'NCO' as HOLDCATEGORY, 'IAM' as Module from [T0] where 1=1"
)


@pytest.fixture(autouse=True)
def isolated_aed_environment(monkeypatch):
    for name in tuple(os.environ):
        if name.startswith("AED_") or name == "ENV_MODE":
            monkeypatch.delenv(name, raising=False)
    monkeypatch.setenv("ENV_MODE", "prod")


def block(options, body=""):
    return f"<OPTIONS>\n{options}\n</OPTIONS>\n{body}\n<---- New Query ---->\n"


def source(*, ambiguous=False, sql=PRODUCER_SQL):
    producer = block(
        "/ENGINE=SQLite\n/CSV=RESULT_<<<LOT>>>.csv\n/TABLE=requests.csv:T0\n/HEADERS="
        + HEADERS,
        sql,
    )
    return (
        block('/UTILITIES={START-MACRO} "config.csv" "N"')
        + block('/UTILITIES={IF-THEN} "SIGNAL" "GT" "0" "" "" "" ""')
        + block('/UTILITIES={IF-THEN} "VAR(<<<DCSR>>>)" "EQS" "Y" "" "" "" ""')
        + producer
        + (producer if ambiguous else "")
        + block(
            '/UTILITIES=RoboCopy.va "RESULT_*.csv" "." "<<<CSRPATH>>>" "5" "30" "N" "/MOV" "N"'
        )
        + block("/UTILITIES={END-IF}")
        + block(
            "/REPORT=HTML-LAYOUT",
            ":FILE:EMAIL:<<<EMAIL>>>\n:TITLE:CSR outcome\n<p>Lot on hold due to NCO.</p>",
        )
        + block('/UTILITIES={IF-THEN} "HISTERR" "LE" "0" "" "" "" ""')
        + block("/WRITE-FILE=Y\n/CSV=history-written.txt", "history")
        + block("/UTILITIES={END-IF}")
        + block("/UTILITIES={END-IF}")
        + block("/UTILITIES={END-MACRO}")
        + block("/WRITE-FILE=Y\n/CSV=after.txt", "after")
    )


HOLD_CALL = "aed.lot_hold(lotid=lot, operation='', holdcode='', comment=comment)"
SET_ATTR = "aed.lot_set_attr(lotid=lot, attributeid='1234', attributevalue='Y')"


def configure(main, namespace, old, new):
    """Edit the generated AED call in main.py the way a user would."""
    code = main.read_text()
    assert old in code
    code = code.replace(old, new, 1)
    main.write_text(code)
    job_factory = namespace.get("JobRuntime")
    exec(code, namespace)
    if job_factory is not None:
        namespace["JobRuntime"] = job_factory


def hold_call(operation="2303", holdcode="NCO"):
    return (
        f"aed.lot_hold(lotid=lot, operation={operation!r}, "
        f"holdcode={holdcode!r}, comment=comment)"
    )


def fake_client(calls, fail=lambda method, args: False):
    """Real AEDClient over an AEDAPIClient-shaped fake that records positional calls."""

    class Upstream:
        def __getattr__(self, method):
            def call(*args):
                calls.append((method, args))
                if fail(method, args):
                    raise OSError("synthetic failure")
                return {"Success": True}

            return call

    return lambda facility, **kwargs: AEDClient(
        facility, **kwargs, client_factory=lambda f: Upstream()
    )


def use_aed(monkeypatch, factory):
    monkeypatch.setattr("vg2c.runtime.job.AEDClient", factory)


def generated(tmp_path, text=None):
    path = tmp_path / "workflow.txt"
    path.write_text(text or source())
    main = translate(path)
    namespace = {"__file__": str(main), "__name__": "offline_job"}
    exec(main.read_text(), namespace)
    work = main.parent / "output"
    work.mkdir()
    (work / "config.csv").write_text(
        "LOT,SIGNAL,DCSR,HISTERR,EMAIL,CSRPATH,FAC\n"
        "L1,1,Y,0,recipient@example.test,never-copy,A15\n"
    )
    (work / "requests.csv").write_text("Lot,other\nL1,x\nL1,y\n")
    configure(main, namespace, HOLD_CALL, hold_call())
    capture_jobs(namespace)
    return main, namespace, work


def test_actual_sources_detect_csr_and_keep_queries():
    root = Path(__file__).resolve().parents[2]
    for name in ("CSR_IAM_v2.txt", "test_ICMPCS_GE_Signal.txt"):
        result = compile_document(root / name)
        assert len(result.resolved.csr_submissions) == 1
        assert ".env" not in dict(result.emitted.assets)
        assert "aed = job.aed(macros['FAC'])" in result.emitted.source
        assert "mode=" not in result.emitted.source
        assert "test_mode_excluded=job" not in result.emitted.source
        assert "on_write" not in result.emitted.source
        assert HOLD_CALL in result.emitted.source
        assert "csr_submission" not in result.emitted.source
        assert not any("RESULT" in path for path, _ in result.emitted.assets)
        assert "AED_FACILITY" not in result.emitted.source
        assert "AED_MODE" not in result.emitted.source
        assert "bootstrap_aed(" not in result.emitted.source
        sql = "\n".join(
            value for path, value in result.emitted.assets if path.endswith(".sql")
        )
        expected_window = (
            "SYSDATE - 1"
            if name == "CSR_IAM_v2.txt"
            else "To_Char(SYSDATE,'yyyy-mm-dd hh24:mi:ss')"
        )
        assert expected_window in sql
        if name == "CSR_IAM_v2.txt":
            assert "[NCO_Risk] = '1'" in sql
        assert "MIPPATH" in result.emitted.source
        assert "1064" not in result.emitted.source


def test_detection_does_not_depend_on_block_numbers(tmp_path):
    path = tmp_path / "shifted.txt"
    path.write_text(block("/WRITE-FILE=Y\n/CSV=unrelated.txt", "unrelated") + source())
    result = compile_document(path)
    assert len(result.resolved.csr_submissions) == 1
    assert "job.load_file" in result.emitted.source


@pytest.mark.parametrize(
    "sql, expected",
    [
        (
            PRODUCER_SQL,
            dict(lot_column="lot", hold=True, contain=False, note="note & detail"),
        ),
        (
            "SELECT DISTINCT [Lot_X] AS [HOLD_LOT], 'Y' AS [AUTO_CONTAIN], [Lot_X] AS [LOT], "
            "'<<<dCCB>>>' AS [CCB_NUMBER] FROM [T0] WHERE 1=1",
            dict(lot_column="Lot_X", hold=True, contain=True, ccb="<<<dCCB>>>"),
        ),
        (
            "SELECT 'Y' AS AUTO_CONTAIN, lot AS LOT, '42' AS CCB_NUMBER FROM T0",
            dict(lot_column="lot", hold=False, contain=True),
        ),
    ],
)
def test_producer_sql_becomes_lot_column_and_actions(sql, expected):
    producer = SimpleNamespace(
        resolved_body=sql,
        resolved_options=SimpleNamespace(pairs=(("TABLE", "in.csv:T0"),)),
    )
    request = csr_request(producer)
    assert request["input_file"] == "in.csv"
    assert {key: request[key] for key in expected} == expected


@pytest.mark.parametrize(
    "sql, message",
    [
        (PRODUCER_SQL.replace("where 1=1", "where lot > 0"), "SELECT"),
        (PRODUCER_SQL.replace("'42'", "''"), "CCB_NUMBER"),
        (PRODUCER_SQL.replace("[lot] as LOT", "'L1' as LOT"), "LOT"),
        (
            PRODUCER_SQL.replace("'N' as AUTO_CONTAIN", "[c] as AUTO_CONTAIN"),
            "AUTO_CONTAIN",
        ),
        (PRODUCER_SQL.replace("'Y' as HOLD_LOT", "[other] as HOLD_LOT"), "Conflicting"),
        (PRODUCER_SQL.replace("'Y' as HOLD_LOT", "'N' as HOLD_LOT"), "neither"),
        (PRODUCER_SQL.replace("[T0]", "[T9]"), "T9"),
    ],
)
def test_unsupported_producer_sql_fails_at_compile_time(tmp_path, sql, message):
    path = tmp_path / "bad.txt"
    path.write_text(source(sql=sql))
    with pytest.raises(ValueError, match=message):
        compile_document(path)


def test_generated_call_is_direct_and_editable(tmp_path, monkeypatch):
    main, namespace, work = generated(tmp_path)
    calls = []
    use_aed(monkeypatch, fake_client(calls))
    monkeypatch.setenv("ENV_MODE", "prod")
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    namespace["run"]()
    assert namespace["_job"].wait_file("history-written.txt", timeout=0)
    configure(main, namespace, hold_call(), SET_ATTR)
    namespace["run"]()
    assert [name for name, args in calls] == ["lot_hold", "lot_set_attr"]
    assert calls[0][1] == (
        "L1",
        "2303",
        "NCO",
        quote("note & detail\nCCB: 42", safe=""),
    )
    assert calls[1][1] == ("L1", "1234", "Y")
    assert not (main.parent / "aed_config.json").exists()


def test_combined_hold_and_containment_stops_at_failure_and_skips_history(
    tmp_path,
    monkeypatch,
):
    text = source().replace("'N' as AUTO_CONTAIN", "'Y' as AUTO_CONTAIN")
    main, namespace, work = generated(tmp_path, text)
    configure(
        main,
        namespace,
        "operation='', reference_number=''",
        "operation='2303', reference_number='REF42'",
    )
    calls = []
    use_aed(
        monkeypatch,
        fake_client(calls, fail=lambda method, args: method == "lot_contain"),
    )
    monkeypatch.setenv("ENV_MODE", "prod")
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    with pytest.raises(RuntimeError, match="lot_contain request failed"):
        namespace["run"]()
    assert [name for name, args in calls] == ["lot_hold", "lot_contain"]
    assert not (work / "AED_results.csv").exists()
    assert not namespace["_job"].wait_file("history-written.txt", timeout=0)
    assert not namespace["_job"].wait_file("after.txt", timeout=0)


def test_macros_supply_facility_operation_and_hold_code(tmp_path, monkeypatch):
    main, namespace, work = generated(tmp_path)
    monkeypatch.setenv("ENV_MODE", "prod")
    configure(
        main,
        namespace,
        hold_call(),
        "aed.lot_hold(lotid=lot, operation=macros['dHoldOperation'], "
        "holdcode=macros['dPHQ'], comment=comment)",
    )
    (work / "config.csv").write_text(
        "LOT,SIGNAL,DCSR,HISTERR,EMAIL,CSRPATH,FAC,dHoldOperation,dPHQ\n"
        "L1,1,Y,0,owner@example.test,never-copy,A16,2846,P123\n"
    )
    calls = []

    class Upstream:
        def lot_hold(self, *args):
            calls.append(args)
            return {"Success": True}

    facilities = []

    def factory(facility, **kwargs):
        facilities.append(facility)
        return AEDClient(facility, **kwargs, client_factory=lambda f: Upstream())

    use_aed(monkeypatch, factory)
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    namespace["run"]()
    assert calls == [("L1", "2846", "P123", quote("note & detail\nCCB: 42", safe=""))]
    assert facilities == ["A16"]


@pytest.mark.parametrize(
    "call, error",
    [
        (SET_ATTR.replace("attributeid='1234'", "attributeid=''"), ValueError),
        (
            SET_ATTR.replace("attributevalue='Y'", "attributevalue='Y', extra='bad'"),
            TypeError,
        ),
        (hold_call(holdcode=""), ValueError),
    ],
)
def test_invalid_call_records_failure_and_stops_workflow(
    tmp_path, monkeypatch, call, error
):
    main, namespace, work = generated(tmp_path)
    configure(main, namespace, hold_call(), call)
    calls = []
    use_aed(monkeypatch, fake_client(calls))
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    with pytest.raises(error):
        namespace["run"]()
    assert calls == []
    assert not (work / "AED_results.csv").exists()
    assert not namespace["_job"].wait_file("history-written.txt", timeout=0)
    assert not namespace["_job"].wait_file("after.txt", timeout=0)


def test_csr_step_has_source_metadata(tmp_path):
    path = tmp_path / "metadata.txt"
    path.write_text(
        (block("/WRITE-FILE=Y\n/CSV=unrelated.txt", "unrelated") + source()).replace(
            "RESULT_<<<LOT>>>.csv", "RESULT_requests.csv"
        )
    )
    result = compile_document(path)
    submission = result.resolved.csr_submissions[0]
    step = result.emitted.step_for_block(submission.block_index)
    assert step.functional_kind == "CSR_SUBMISSION"
    assert step.block_index == submission.block_index
    generated_step = result.emitted.source[
        step.source_range.start_offset : step.source_range.end_offset
    ]
    assert "job.csr_lots('requests.csv', 'lot')" in generated_step
    assert result.emitted.step_for_block(submission.producer_index) is None


def test_history_is_gated_by_csr_enablement_without_run_state(tmp_path):
    path = tmp_path / "single.txt"
    path.write_text(source())
    emitted = compile_document(path).emitted.source
    assert "if job.enabled('csr') and macros['DCSR'] == 'Y':" in emitted
    assert "if job.enabled('csr') and (" in emitted
    for removed in ("csr_history_allowed", "begin_csr", "csr_submission", "job.sql("):
        assert removed not in emitted


def test_history_is_skipped_in_test_mode(tmp_path, monkeypatch):
    main, namespace, work = generated(tmp_path)
    monkeypatch.setenv("ENV_MODE", "test")
    use_aed(monkeypatch, lambda *a, **kw: pytest.fail("AED in test mode"))
    namespace["run"]()
    assert not namespace["_job"].wait_file("history-written.txt", timeout=0)
    assert namespace["_job"].wait_file("after.txt", timeout=0)


@pytest.mark.parametrize(
    ("has_slot", "expected_order"),
    [
        (False, ("css_file", "values")),
        (True, ("values", "css_file")),
    ],
)
def test_csr_html_layout_preserves_css_and_values_keyword_order(
    tmp_path, has_slot, expected_order
):
    path = tmp_path / "csr-layout.txt"
    body = ":CSS:report.css\n:TITLE:CSR outcome\n<p>Lot on hold due to NCO.</p>"
    if has_slot:
        body += "\n<p><<<SIGNAL>>></p>"
    text = source().replace(":TITLE:CSR outcome\n<p>Lot on hold due to NCO.</p>", body)
    path.write_text(text)
    emitted = compile_document(path).emitted
    tree = ast.parse(emitted.source)
    call = next(
        node.value
        for node in ast.walk(tree)
        if isinstance(node, ast.Assign)
        and any(
            isinstance(target, ast.Name) and target.id == "csr_report"
            for target in node.targets
        )
    )
    keywords = tuple(item.arg for item in call.keywords)
    positions = tuple(keywords.index(key) for key in expected_order)
    assert positions == tuple(sorted(positions))


def test_ambiguous_producer_fails_with_source_location(tmp_path):
    path = tmp_path / "ambiguous.txt"
    path.write_text(source(ambiguous=True))
    with pytest.raises(ValueError, match="exactly one preceding") as error:
        compile_document(path)
    assert str(path) in str(error.value)


def test_else_branch_cannot_claim_positive_csr_gates(tmp_path):
    path = tmp_path / "else.txt"
    path.write_text(
        source().replace(
            '/UTILITIES={IF-THEN} "VAR(<<<DCSR>>>)" "EQS" "Y" "" "" "" ""\n</OPTIONS>',
            '/UTILITIES={IF-THEN} "VAR(<<<DCSR>>>)" "EQS" "Y" "" "" "" ""\n</OPTIONS>'
            + "\n<---- New Query ---->\n<OPTIONS>\n/UTILITIES={ELSE}\n</OPTIONS>",
        )
    )
    with pytest.raises(ValueError, match="gates"):
        compile_document(path)


def test_unrelated_copies_and_mms_remain_file_operations(tmp_path):
    path = tmp_path / "copy.txt"
    path.write_text(block('/UTILITIES=RoboCopy.va "RESULT_*.csv" "." "<<<MIPPATH>>>"'))
    result = compile_document(path)
    assert not result.resolved.csr_submissions
    assert "job.submit_mms(" in result.emitted.source
    assert "AEDClient" not in result.emitted.source


def test_non_csr_payload_copy_to_csr_path_remains_a_file_operation(tmp_path):
    path = tmp_path / "ordinary.txt"
    path.write_text(
        block(
            "/ENGINE=SQLite\n/CSV=RESULT_log.csv\n/HEADERS=LOG",
            "select 'ordinary log' as LOG",
        )
        + block('/UTILITIES=RoboCopy.va "RESULT_*.csv" "." "<<<CSRPATH>>>"')
    )
    result = compile_document(path)
    assert not result.resolved.csr_submissions
    assert "job.copy_file(" in result.emitted.source


def test_relocated_test_job_skips_csr_query_and_email(tmp_path, monkeypatch):
    import shutil

    main, namespace, work = generated(tmp_path)
    moved = tmp_path / "relocated"
    shutil.move(main.parent, moved)
    namespace["__file__"] = str(moved / "main.py")
    work = moved / "output"
    monkeypatch.setenv("ENV_MODE", "test")
    # A skipped CSR must never read its lot input or construct the AED client.
    (work / "requests.csv").unlink()
    use_aed(monkeypatch, lambda *a, **kw: pytest.fail("AED constructed in test mode"))
    monkeypatch.setattr(
        "vg2c.runtime.job.send_mail", lambda *a, **kw: pytest.fail("test mail")
    )
    monkeypatch.chdir(tmp_path)
    namespace["run"]()
    assert not list(work.glob("RESULT_*.csv"))
    assert not (work / "AED_results.csv").exists()
    assert not namespace["_job"].wait_file("history-written.txt", timeout=0)
    assert namespace["_job"].wait_file("after.txt", timeout=0)


def test_entrypoint_exclusions_can_enable_csr_without_enabling_email(
    tmp_path, monkeypatch
):
    _, namespace, work = generated(tmp_path)
    monkeypatch.setenv("ENV_MODE", "test")
    namespace["TEST_MODE_EXCLUDED"] = {"email", "mms"}
    calls = []
    use_aed(monkeypatch, fake_client(calls))
    monkeypatch.setattr(
        "vg2c.runtime.job.send_mail", lambda *a, **kw: pytest.fail("test mail")
    )
    namespace["run"]()
    assert [method for method, args in calls] == ["lot_hold"]
    assert namespace["_job"].wait_file("history-written.txt", timeout=0)


@pytest.mark.parametrize("excluded", [{"mms"}, set()])
def test_mms_guard_skips_entire_preparation_query(tmp_path, monkeypatch, excluded):
    path = tmp_path / "mms.txt"
    path.write_text(
        block('/UTILITIES={START-MACRO} "config.csv" "N"')
        + block('/UTILITIES={IF-THEN} "VAR(<<<DMWF>>>)" "EQS" "Y" "" "" "" ""')
        + block("/ENGINE=SQLite\n/CSV=mms.csv", "select 1 as value")
        + block('/UTILITIES=RoboCopy.va "mms.csv" "." "<<<MIPPATH>>>"')
        + block("/UTILITIES={END-IF}")
        + block("/UTILITIES={END-MACRO}")
    )
    main = translate(path)
    work = main.parent / "output"
    work.mkdir()
    (work / "config.csv").write_text("DMWF,MIPPATH\nY,missing-destination\n")
    namespace = {"__file__": str(main), "__name__": "offline_job"}
    exec(main.read_text(), namespace)
    namespace["TEST_MODE_EXCLUDED"] = excluded
    monkeypatch.setenv("ENV_MODE", "test")
    calls = []
    monkeypatch.setattr("vg2c.runtime.job.copy_file", lambda *a, **kw: calls.append(kw))
    namespace["run"]()
    assert (work / "mms.csv").exists() == ("mms" not in excluded)
    assert len(calls) == (0 if "mms" in excluded else 1)


def test_failure_stops_next_lot_history_and_following_operations(tmp_path, monkeypatch):
    main, namespace, work = generated(tmp_path)
    mails = []
    monkeypatch.setattr(
        "vg2c.runtime.job.send_mail", lambda *args, **kwargs: mails.append(args)
    )
    use_aed(monkeypatch, fake_client([], fail=lambda method, args: True))
    monkeypatch.setenv("ENV_MODE", "prod")
    with pytest.raises(RuntimeError, match="lot_hold request failed"):
        namespace["run"]()
    assert not (work / "AED_results.csv").exists()
    assert not namespace["_job"].wait_file("history-written.txt", timeout=0)
    assert not namespace["_job"].wait_file("after.txt", timeout=0)
    assert mails == []


def test_config_validation_and_disabled_gate(tmp_path, monkeypatch):
    main, namespace, work = generated(tmp_path)
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    configure(main, namespace, hold_call(), hold_call(holdcode=""))
    with pytest.raises(ValueError, match="hold code"):
        namespace["run"]()
    (work / "config.csv").write_text(
        "LOT,SIGNAL,DCSR,HISTERR,EMAIL,CSRPATH\n"
        "L1,1,N,0,recipient@example.test,never-copy\n"
    )
    namespace["run"]()
    assert namespace["_job"].wait_file("history-written.txt", timeout=0)


def test_mocked_production_partial_failure_keeps_completed_actions(
    tmp_path, monkeypatch
):
    main, namespace, work = generated(tmp_path)
    (work / "requests.csv").write_text("lot\nL1\nL2\nL3\n")
    calls = []
    use_aed(monkeypatch, fake_client(calls, fail=lambda method, args: args[0] == "L2"))
    monkeypatch.setenv("ENV_MODE", "prod")
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    with pytest.raises(RuntimeError, match="lot_hold request failed"):
        namespace["run"]()
    assert [args[0] for name, args in calls] == ["L1", "L2"]
    assert not (work / "AED_results.csv").exists()
    assert not namespace["_job"].wait_file("history-written.txt", timeout=0)


def test_shared_mode_is_reloaded_but_facility_and_arguments_come_from_vg2(
    tmp_path, monkeypatch
):
    _, namespace, work = generated(tmp_path)
    calls = []

    class Upstream:
        def lot_hold(self, *args):
            calls.append(args)
            return {"Success": True}

    use_aed(
        monkeypatch,
        lambda facility, **kwargs: AEDClient(
            facility, **kwargs, client_factory=lambda f: Upstream()
        ),
    )
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    monkeypatch.setenv("ENV_MODE", "prod")
    monkeypatch.setenv("AED_FACILITY", "A99")
    monkeypatch.setenv("AED_MODE", "test")
    (work / "config.csv").write_text(
        "LOT,SIGNAL,DCSR,HISTERR,EMAIL,CSRPATH,FAC\n"
        "L1,1,Y,0,recipient@example.test,never-copy,A16\n"
    )
    namespace["run"]()
    assert calls == [("L1", "2303", "NCO", "note%20%26%20detail%0ACCB%3A%2042")]
    assert namespace["_job"].wait_file("history-written.txt", timeout=0)

    monkeypatch.setenv("ENV_MODE", "test")
    namespace["run"]()
    assert len(calls) == 1
    assert not (work / "AED_results.csv").exists()
    assert not namespace["_job"].wait_file("history-written.txt", timeout=0)


@pytest.mark.parametrize("env_mode", [None, "test", "prod"])
def test_generated_aed_is_built_only_in_prod_by_default(
    tmp_path, monkeypatch, env_mode
):
    main, namespace, work = generated(tmp_path)
    built = []

    def client(facility, **kwargs):
        built.append(facility)
        return AEDClient(
            facility,
            **kwargs,
            client_factory=lambda f: SimpleNamespace(
                lot_hold=lambda *args: {"Success": True}
            ),
        )

    use_aed(monkeypatch, client)
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    monkeypatch.delenv("ENV_MODE", raising=False)
    if env_mode is not None:
        monkeypatch.setenv("ENV_MODE", env_mode)
    namespace["run"]()
    assert built == (["A15"] if env_mode == "prod" else [])
    assert namespace["_job"].wait_file("history-written.txt", timeout=0) == (
        env_mode == "prod"
    )


@pytest.mark.parametrize("value", ["", "invalid value"])
def test_invalid_shared_mode_does_not_fall_back_to_test(tmp_path, monkeypatch, value):
    _, namespace, work = generated(tmp_path)
    monkeypatch.setenv("ENV_MODE", value)
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    with pytest.raises(ValueError):
        namespace["run"]()
    assert not (work / "AED_results.csv").exists()
    assert "_job" not in namespace


@pytest.mark.parametrize("fail", [False, True])
def test_user_can_change_generated_call_to_attribute_action(
    tmp_path,
    monkeypatch,
    fail,
):
    main, namespace, work = generated(tmp_path)
    code = main.read_text()
    configure(main, namespace, hold_call(), SET_ATTR)
    monkeypatch.setenv("ENV_MODE", "prod")
    calls, mails = [], []

    class Upstream:
        def lot_set_attr(self, *args):
            calls.append(args)
            if fail:
                raise RuntimeError("synthetic attribute failure")
            return {"Success": True}

    use_aed(
        monkeypatch,
        lambda facility, **kwargs: AEDClient(
            facility, **kwargs, client_factory=lambda f: Upstream()
        ),
    )
    monkeypatch.setattr(
        "vg2c.runtime.job.send_mail",
        lambda *args, **kwargs: mails.append((args, kwargs)),
    )
    if fail:
        with pytest.raises(RuntimeError, match="synthetic attribute failure"):
            namespace["run"]()
    else:
        namespace["run"]()
    assert calls == [("L1", "1234", "Y")]
    assert main.read_text() != code
    assert not (work / "AED_results.csv").exists()
    assert namespace["_job"].wait_file("history-written.txt", timeout=0) == (not fail)
    assert namespace["_job"].wait_file("after.txt", timeout=0) == (not fail)
    assert len(mails) == (0 if fail else 1)
    if not fail:
        report = Path(mails[0][0][2]).read_text()
        assert "AED accepted 1 request(s)" in report
        assert "hold requests" not in report
