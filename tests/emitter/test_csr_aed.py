"""Source-driven CSR translation and offline generated-project execution."""

import ast
import csv
from dataclasses import replace
from pathlib import Path
from types import SimpleNamespace

import pytest

from vg2c import compile_document, translate
from vg2c.emitter import emit
from vg2c.resolver.csr import CsrSubmission
from vg2c.runtime import AEDClient
from vg2c.workflow import project_document

HEADERS = "HOLD_LOT,AUTO_CONTAIN,LOT,EMAIL,CCB_NUMBER,HOLD_NOTE,HOLDCATEGORY,Module"


@pytest.fixture(autouse=True)
def isolated_aed_environment(monkeypatch):
    for name in ("AED_FACILITY", "AED_HOLD_OPERATION", "AED_HOLD_CODE", "AED_MODE", "ENV_MODE"):
        monkeypatch.delenv(name, raising=False)


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


def test_csr_step_has_source_metadata_and_real_file_dependencies(tmp_path):
    path = tmp_path / "metadata.txt"
    path.write_text((block('/WRITE-FILE=Y\n/CSV=unrelated.txt', "unrelated") + source())
                    .replace("RESULT_<<<LOT>>>.csv", "RESULT_requests.csv"))
    result = compile_document(path)
    submission = result.resolved.csr_submissions[0]
    step = result.emitted.step_for_block(submission.block_index)
    model = project_document(result)
    operation = next(op for op in model.operations if op.block_index == submission.block_index)
    assert operation.kind == "csr-submission"
    assert operation.display_name == "CSR → AED"
    assert operation.validation_state == "valid"
    assert operation.source_span == result.resolved.blocks[submission.block_index].span
    assert operation.source_range == step.source_range
    assert step.functional_kind == "CSR_SUBMISSION"
    assert step.function_name.endswith("_csr_submission")
    generated_step = model.source[
        operation.source_range.start_offset:operation.source_range.end_offset
    ]
    assert "aed.lot_hold(" in generated_step
    assert all(not binding.editable for binding in operation.bindings)
    effects = [effect for effect in model.effects if effect.operation_id == operation.id]
    assert [effect.kind for effect in effects] == ["read", "write"]
    assert effects[0].inputs[0].path == "RESULT_requests.csv"
    assert effects[1].outputs[0].path == "AED_results.csv"
    assert effects[1].conditional
    producer_effects = {
        effect.id for effect in model.effects if effect.block_index == submission.producer_index
    }
    assert producer_effects.intersection(effects[0].dependency_ids)
    assert not any("CSRPATH" in (endpoint.path or endpoint.expression or "")
                   for effect in effects for endpoint in (*effect.inputs, *effect.outputs))


def test_emitter_initializes_each_csr_submission_in_shared_signal_scope(tmp_path):
    path = tmp_path / "shared-signal.txt"
    path.write_text(source())
    result = compile_document(path)
    first = result.resolved.csr_submissions[0]
    # Exercise the existing emitter ordering contract at its resolved boundary.
    second = CsrSubmission(
        block_index=first.producer_index,
        producer_index=first.producer_index,
        signal_scope_id=first.signal_scope_id,
        notification_indexes=(),
        history_scope_ids=(),
    )
    resolved = replace(result.resolved, csr_submissions=(first, second))
    emitted = emit(replace(result.dispatched, resolved=resolved))

    first_init = f"csr_history_allowed_{first.block_index} = True"
    second_init = f"csr_history_allowed_{second.block_index} = True"
    assert first_init in emitted.source
    assert second_init in emitted.source
    assert emitted.source.index(first_init) < emitted.source.index(second_init)


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
    text = source().replace(
        ":TITLE:CSR outcome\n<p>Lot on hold due to NCO.</p>", body
    )
    path.write_text(text)
    emitted = compile_document(path).emitted
    tree = ast.parse(emitted.source)
    call = next(
        node.value
        for node in ast.walk(tree)
        if isinstance(node, ast.Assign)
        and any(isinstance(target, ast.Name) and target.id == "csr_report"
                for target in node.targets)
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


def test_environment_overrides_python_defaults_on_each_run(tmp_path, monkeypatch):
    _, namespace, work = generated(tmp_path)
    calls = []

    class Service:
        def lot_hold(self, *args):
            calls.append(args)
            return SimpleNamespace(raise_for_status=lambda: None, json=lambda: {"Success": True})

    namespace["AEDClient"] = lambda **kwargs: AEDClient(
        **kwargs, service_factory=lambda facility: Service())
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    for key, value in dict(AED_FACILITY="A16", AED_HOLD_OPERATION="2846",
                           AED_HOLD_CODE="ENV", AED_MODE="prod").items():
        monkeypatch.setenv(key, value)
    namespace["run"]()
    assert calls == [("L1", "2846", "ENV", "note%20%26%20detail%0ACCB%3A%2042")]
    with (work / "AED_results.csv").open() as stream:
        assert next(csv.DictReader(stream))["FACILITY"] == "A16"
    assert (work / "history-written.txt").exists()
    (work / "history-written.txt").unlink()

    for key in ("AED_FACILITY", "AED_HOLD_OPERATION", "AED_HOLD_CODE", "AED_MODE"):
        monkeypatch.delenv(key)
    namespace["run"]()
    assert len(calls) == 1
    with (work / "AED_results.csv").open() as stream:
        row = next(csv.DictReader(stream))
    assert row["FACILITY"] == "A15"
    assert row["STATUS"] == "dry_run"
    assert not (work / "history-written.txt").exists()


@pytest.mark.parametrize("env_mode", [None, "test", "prod"])
def test_generated_mode_falls_back_to_env_mode_then_test(tmp_path, monkeypatch, env_mode):
    _, namespace, work = generated(tmp_path)
    namespace["AED_MODE"] = None
    modes = []

    def client(**kwargs):
        aed = AEDClient(**kwargs, service_factory=lambda facility: SimpleNamespace(
            lot_hold=lambda *args: SimpleNamespace(
                raise_for_status=lambda: None, json=lambda: {"Success": True})))
        modes.append(aed.mode)
        return aed

    namespace["AEDClient"] = client
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    if env_mode is not None:
        monkeypatch.setenv("ENV_MODE", env_mode)
    namespace["run"]()
    assert modes == [env_mode or "test"]
    assert (work / "history-written.txt").exists() == (env_mode == "prod")


@pytest.mark.parametrize("key", ["AED_FACILITY", "AED_HOLD_OPERATION", "AED_HOLD_CODE", "AED_MODE"])
@pytest.mark.parametrize("value", ["", "invalid value"])
def test_invalid_environment_does_not_fall_back_to_python(tmp_path, monkeypatch, key, value):
    _, namespace, work = generated(tmp_path)
    monkeypatch.setenv(key, value)
    monkeypatch.setattr("vg2c.runtime.job.send_mail", lambda *args, **kwargs: None)
    with pytest.raises(ValueError):
        namespace["run"]()
    assert "failed" in (work / "AED_results.csv").read_text()
    assert not (work / "history-written.txt").exists()
    assert not (work / "after.txt").exists()


@pytest.mark.parametrize("fail", [False, True])
def test_user_can_replace_direct_hold_with_attribute_call(tmp_path, monkeypatch, fail):
    main, namespace, work = generated(tmp_path)
    code = main.read_text()
    call = next(node for node in ast.walk(ast.parse(code)) if isinstance(node, ast.Call)
                and isinstance(node.func, ast.Attribute) and node.func.attr == "lot_hold")
    code = code.replace(ast.get_source_segment(code, call),
                        "aed.lot_set_attr(lotid=request['HOLD_LOT'], "
                        "attributeid='1234', attributevalue='Y')")
    code = code.replace("aed_action = 'lot_hold'", "aed_action = 'lot_set_attr'")
    exec(code, namespace)
    namespace.update(AED_FACILITY="A15", AED_MODE="prod")
    calls, mails = [], []

    class Service:
        def lot_set_attr(self, *args):
            calls.append(args)
            if fail:
                raise RuntimeError("synthetic attribute failure")
            return SimpleNamespace(raise_for_status=lambda: None, json=lambda: {"Success": True})

    namespace["AEDClient"] = lambda **kwargs: AEDClient(
        **kwargs, service_factory=lambda facility: Service())
    monkeypatch.setattr(
        "vg2c.runtime.job.send_mail", lambda *args, **kwargs: mails.append((args, kwargs))
    )
    if fail:
        with pytest.raises(RuntimeError, match="synthetic attribute failure"):
            namespace["run"]()
    else:
        namespace["run"]()
    assert calls == [("L1", "1234", "Y")]
    with (work / "AED_results.csv").open() as stream:
        row = next(csv.DictReader(stream))
    assert row["ACTION"] == "lot_set_attr"
    assert row["STATUS"] == ("failed" if fail else "accepted")
    assert (work / "history-written.txt").exists() == (not fail)
    assert (work / "after.txt").exists() == (not fail)
    assert len(mails) == 1
    if not fail:
        report = Path(mails[0][0][2]).read_text()
        assert "lot_set_attr accepted" in report
        assert "hold requests" not in report
