from dataclasses import replace
from types import SimpleNamespace

from vg2c.dataflow.file_effects import bind_file_effects, order_file_effects
from vg2c.emitter.models import CodeExpr, build_step_emission, finalize_steps
from vg2c.utilities.fs_ops import FileSystemOps
from vg2c.utilities.pipeline_context import PipelineContext
from vg2c.utilities.smart_append import SmartAppend


def _effects(call, values=None):
    step = build_step_emission(
        function_name="step_0000_test",
        block_index=0,
        functional_kind="TEST",
        body_lines=[call],
    )
    invocation = finalize_steps(step.source, [step])[0].invocations[0]
    return bind_file_effects(
        invocation,
        values or {},
        step_id=step.function_name,
        block_index=0,
        scope_id=0,
        order=0,
    )


def test_append_has_prior_destination_and_separate_next_state():
    effect = _effects(SmartAppend.append.render("merged.csv", "new.csv"))[0]
    assert effect.kind == "append"
    assert [item.path for item in effect.inputs] == ["new.csv", "merged.csv"]
    assert [item.path for item in effect.outputs] == ["merged.csv"]
    assert effect.inputs[-1].id != effect.outputs[0].id
    assert effect.inputs[-1].phase == "prior" and effect.outputs[0].phase == "next"


def test_file_effect_identity_survives_path_edits_and_delete_never_produces():
    call = FileSystemOps.rename.render("before.csv", "after.csv")
    baseline = _effects(call)[0]
    parameter_id = baseline.outputs[0].binding_id
    edited = _effects(call, {parameter_id: "changed.csv"})[0]
    assert baseline.id == edited.id and baseline.operation_id == edited.operation_id
    assert edited.outputs[0].path == "changed.csv"
    deleted = _effects(FileSystemOps.delete.render(["first.csv", "second.csv"]))[0]
    assert deleted.kind == "delete" and len(deleted.inputs) == 2 and not deleted.outputs


def test_query_fan_in_and_dynamic_path_are_not_inferred_from_python():
    query = PipelineContext.run_query.render(
        "select 1", "out.csv", CodeExpr("reader"), inputs=["a.csv", "b.csv", "c.csv"]
    )
    effect = _effects(query)[0]
    assert effect.kind == "transform" and len(effect.inputs) == 3
    dynamic = _effects(FileSystemOps.copy.render("a.csv", CodeExpr("destination()")))[0]
    assert dynamic.outputs[0].status == "dynamic"
    assert dynamic.outputs[0].path is None
    assert dynamic.outputs[0].expression == "destination()"


def test_read_after_unconditional_delete_does_not_reuse_live_producer(tmp_path):
    written = _effects(FileSystemOps.rename.render("a.csv", "b.csv"))[0]
    deleted = replace(_effects(FileSystemOps.delete.render(["b.csv"]))[0], order=1)
    read = replace(_effects(FileSystemOps.copy.render("b.csv", "c.csv"))[0], order=2)
    root = SimpleNamespace(scope_id=0, kind="root", children=[])
    ordered = order_file_effects(
        (written, deleted, read), root, tmp_path / "generated.py"
    )
    assert ordered[-1].inputs[0].status == "missing"
    assert written.id not in ordered[-1].dependency_ids
    assert ordered[-1].inputs[0].state_ids[0].endswith(":deleted")
    assert any(
        item.path == "b.csv" and item.status == "guaranteed"
        for item in ordered[1].available_before
    )
    assert any(
        item.path == "b.csv" and item.status == "missing"
        for item in ordered[2].available_before
    )


def test_conditional_delete_preserves_possible_prior_state(tmp_path):
    written = _effects(FileSystemOps.rename.render("a.csv", "b.csv"))[0]
    deleted = replace(
        _effects(FileSystemOps.delete.render(["b.csv"]))[0],
        order=1,
        scope_id=1,
        conditional=True,
    )
    read = replace(_effects(FileSystemOps.copy.render("b.csv", "c.csv"))[0], order=2)
    branch = SimpleNamespace(scope_id=1, kind="if-branch", children=[])
    root = SimpleNamespace(scope_id=0, kind="root", children=[branch])
    ordered = order_file_effects(
        (written, deleted, read), root, tmp_path / "generated.py"
    )
    assert ordered[-1].inputs[0].status == "possible"
    assert len(ordered[-1].inputs[0].state_ids) == 2
    assert written.id in ordered[-1].dependency_ids
    assert any(
        item.path == "b.csv" and item.status == "possible"
        for item in ordered[2].available_before
    )


def test_resource_identity_uses_the_same_normalized_keys_as_dependency_state(tmp_path):
    from vg2c.workflow import file_resources

    write = _effects(PipelineContext.write_file.render(r"Data\Out.csv", "body"))[0]
    copy = replace(
        _effects(PipelineContext.run_query.render(
            "select 1", "copy.csv", CodeExpr("reader"), inputs=["data/out.csv"]
        ))[0],
        order=1,
    )
    root = SimpleNamespace(scope_id=0, kind="root", children=[])
    ordered = order_file_effects((write, copy), root, tmp_path / "generated.py")
    produced = ordered[0].outputs[0]
    consumed = ordered[1].inputs[0]
    assert produced.file_resource_id == consumed.file_resource_id
    assert ordered[0].id in ordered[1].dependency_ids
    resources = file_resources(ordered)
    assert len(resources) == 2
    assert next(item for item in resources if item.id == produced.file_resource_id).producer_refs


def test_file_choices_combine_inventory_with_historical_availability(tmp_path):
    from vg2c_ui.services.file_choices import file_choices_by_operation

    write = _effects(PipelineContext.write_file.render("future.csv", "body"))[0]
    read = _effects(FileSystemOps.copy.render("future.csv", "copy.csv"))[0]
    delete = _effects(FileSystemOps.delete.render(["future.csv"]))[0]
    later = _effects(SmartAppend.append.render("merged.csv", "future.csv"))[0]
    root = SimpleNamespace(scope_id=0, kind="root", children=[])
    ordered = order_file_effects(
        (read, write, delete, later), root, tmp_path / "generated.py"
    )
    choices = file_choices_by_operation(
        ordered,
        ["inputs/uploaded.csv", "future.csv"],
        output_path=tmp_path / "generated.py",
        workspace_root=tmp_path,
    )
    assert "inputs/uploaded.csv" in choices[read.operation_id]
    assert "future.csv" not in choices[read.operation_id]
    assert "future.csv" in choices[delete.operation_id]
    assert "future.csv" not in choices[later.operation_id]


def test_file_choices_keep_branch_outputs_possible_and_reflect_new_uploads(tmp_path):
    from vg2c_ui.services.file_choices import file_choices_by_operation

    conditional = replace(
        _effects(PipelineContext.write_file.render("branch.csv", "body"))[0],
        scope_id=1,
        conditional=True,
    )
    read = _effects(FileSystemOps.copy.render("branch.csv", "copy.csv"))[0]
    branch = SimpleNamespace(scope_id=1, kind="if-branch", children=[])
    root = SimpleNamespace(scope_id=0, kind="root", children=[branch])
    ordered = order_file_effects(
        (conditional, read), root, tmp_path / "generated.py"
    )

    def choices(inventory):
        return file_choices_by_operation(
            ordered,
            inventory,
            output_path=tmp_path / "generated.py",
            workspace_root=tmp_path,
        )[read.operation_id]

    assert "branch.csv" not in choices(["branch.csv"])
    assert "inputs/new.csv" not in choices([])
    assert "inputs/new.csv" in choices(["inputs/new.csv", "branch.csv"])


def test_file_choices_use_server_workspace_paths_only(tmp_path):
    from vg2c_ui.services.file_choices import file_choices_by_operation

    output = tmp_path / "generated" / "script.py"
    write = _effects(PipelineContext.write_file.render("result.csv", "body"))[0]
    outside = _effects(PipelineContext.write_file.render(str(tmp_path.parent / "outside.csv"), "body"))[0]
    read = _effects(FileSystemOps.copy.render("result.csv", "copy.csv"))[0]
    root = SimpleNamespace(scope_id=0, kind="root", children=[])
    ordered = order_file_effects((write, outside, read), root, output)
    choices = file_choices_by_operation(
        ordered,
        ["inputs/uploaded.csv"],
        output_path=output,
        workspace_root=tmp_path,
    )[read.operation_id]
    assert "generated/result.csv" in choices
    assert "inputs/uploaded.csv" in choices
    assert "outside.csv" not in choices


def test_effective_workflow_uses_edited_inputs_even_when_baseline_is_empty(tmp_path):
    from vg2c import compile_document
    from vg2c.editing import SemanticChange
    from vg2c.workflow import project_document

    source = tmp_path / "source.txt"
    source.write_text(
        "<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT 1\n<---- New Query ---->\n"
    )
    result = compile_document(source)
    inputs = next(
        parameter
        for step in result.emitted.steps
        for parameter in step.parameters
        if parameter.name == "inputs"
    )
    baseline = project_document(result)
    projected = project_document(result, [SemanticChange(inputs.id, ["new.csv"])])
    assert baseline.effects[0].kind == "write"
    assert projected.effects[0].kind == "transform"
    assert projected.effects[0].inputs[0].path == "new.csv"
    assert projected.effects[0].id == baseline.effects[0].id


def test_workspace_links_use_output_directory_and_do_not_resurrect_deleted_files(
    tmp_path,
):
    from vg2c.workflow import WorkflowDocument, EffectiveDocument, workspace_links

    write = _effects(PipelineContext.write_file.render("out.csv", "body"))[0]
    read = _effects(
        PipelineContext.run_query.render(
            "select 1", "next.csv", CodeExpr("reader"), inputs=["out.csv"]
        )
    )[0]
    read = replace(
        read,
        inputs=tuple(
            replace(item, status="external", path_base="runtime-search")
            for item in read.inputs
        ),
    )
    producer = WorkflowDocument(
        "producer",
        tmp_path / "one" / "generated.py",
        EffectiveDocument(source="", effects=(write,)),
    )
    consumer = WorkflowDocument(
        "consumer",
        tmp_path / "two" / "generated.py",
        EffectiveDocument(source="", effects=(read,)),
    )
    assert not workspace_links((producer, consumer))
    consumer = replace(consumer, output_path=producer.output_path)
    assert len(workspace_links((producer, consumer))) == 1
    delete = _effects(
        FileSystemOps.delete.render([str(producer.output_path.parent / "out.csv")])
    )[0]
    producer = replace(producer, workflow=EffectiveDocument(source="", effects=(write, delete)))
    assert not workspace_links((producer, consumer))


def test_workspace_issues_follow_effects_after_output_edits(tmp_path):
    from vg2c import compile_document
    from vg2c.editing import SemanticChange
    from vg2c.workflow import WorkflowDocument, project_document, workspace_issues

    documents = []
    results = []
    shared = (tmp_path / "shared.csv").as_posix()
    for name, output, inputs in (
        ("producer", shared, ""),
        ("consumer", "final.csv", f"/TABLE={shared}:t\n"),
    ):
        source = tmp_path / f"{name}.txt"
        source.write_text(
            f"<OPTIONS>\n/OLEDB=SQLite\n/CSV={output}\n{inputs}</OPTIONS>\nSELECT 1\n<---- New Query ---->\n"
        )
        result = compile_document(source)
        results.append(result)
        documents.append(
            WorkflowDocument(name, source.with_suffix(".py"), project_document(result))
        )
    assert documents[1].workflow.effects[0].inputs[0].path == shared
    output = next(
        parameter
        for step in results[0].emitted.steps
        for parameter in step.parameters
        if parameter.name == "output"
    )
    changed = replace(
        documents[0],
        workflow=project_document(
            results[0], [SemanticChange(output.id, "renamed.csv")]
        ),
    )
    assert [
        issue.code for issue in workspace_issues((changed, documents[1]), documents)
    ] == ["BROKEN_DEPENDENCY"]


def test_workspace_fan_in_reports_only_lost_endpoint(tmp_path):
    from vg2c.workflow import (
        WorkflowDocument,
        EffectiveDocument,
        workspace_issues,
        workspace_links,
    )

    first = _effects(PipelineContext.write_file.render("first.csv", "body"))[0]
    second = _effects(PipelineContext.write_file.render("second.csv", "body"))[0]
    read = _effects(
        PipelineContext.run_query.render(
            "select 1",
            "out.csv",
            CodeExpr("reader"),
            inputs=["first.csv", "second.csv"],
        )
    )[0]
    read = replace(
        read,
        inputs=tuple(
            replace(item, status="external", path_base="runtime-search")
            for item in read.inputs
        ),
    )
    documents = tuple(
        WorkflowDocument(
            name, tmp_path / f"{name}.py", EffectiveDocument(source="", effects=(effect,))
        )
        for name, effect in (("first", first), ("second", second), ("consumer", read))
    )
    assert len(workspace_links(documents)) == 2
    renamed = replace(first, outputs=(replace(first.outputs[0], path="renamed.csv"),))
    changed = (
        replace(documents[0], workflow=EffectiveDocument(source="", effects=(renamed,))),
        *documents[1:],
    )
    issues = workspace_issues(changed, documents)
    assert [(item.code, item.artifact) for item in issues] == [
        ("BROKEN_DEPENDENCY", "first.csv")
    ]

    removed = replace(read, inputs=(read.inputs[1],))
    consumer = replace(documents[2], workflow=EffectiveDocument(source="", effects=(removed,)))
    assert not workspace_issues((*changed[:2], consumer), documents)

    replacement = replace(documents[0], document_id="replacement")
    assert not workspace_issues((*changed, replacement), documents)

