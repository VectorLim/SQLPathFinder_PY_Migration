from pathlib import Path

import pytest

from scripthost_portable.runtime import _spf_manager_type
from scripthost_portable.task_inputs import TaskInput
from scripthost_portable.task_introspection import _build_program, inspect_program

FIXTURES = Path(__file__).resolve().parents[1] / "fixtures" / "aed_migration"
DELIMITER = "<---- New Query ---->"


def _legacy():
    _spf_manager_type()
    import SPFLib.SPFSQL3 as legacy

    return legacy


def block(options, command=""):
    return f"\n<OPTIONS>\n{options}\n</OPTIONS>\n{command}\n"


def utility(value):
    return block(f"/WORKDIR=.\\\n/UTILITIES={value}")


LOOPS_AND_HPC = DELIMITER.join(
    [
        "\n",
        utility('{FOR-LOOP} "0" "3" "1" "1" "N"'),
        block("/WRITE-FILE=Y\n/CSV=out.txt", "<<<spf-loop-ctr-1>>>"),
        utility("{END-LOOP}"),
        utility('{SITE-LOOP} "A,B"'),
        utility('{RUN-LOOP} "in.csv" "chunk.csv" "10" "N"'),
        utility('{ROWS-IN-FILE} "chunk.csv" "ROWS" "N"'),
        utility("{END-LOOP}"),
        utility("{END-LOOP}"),
        utility(
            '{BEGIN-HPC} "SVC" "in.csv" "out.csv" "" "Y" "Y" "N" "2" "32" "8" "LOCAL"'
        ),
        block(
            "/NODE=local\n/UN=\n/PW=\n/OLEDB=SQLite\n/ENGINE=SQLite\n/CSV=r.csv",
            "SELECT 1",
        ),
        utility("{END-HPC}"),
    ]
)


def _original_shape(task):
    return (
        task.SPFTaskItemIdx,
        task.SPFTaskType,
        type(task).__name__,
        TaskInput.parse(task.SPFTaskItem),
        task.isControlerStartTask,
        task.isControlerEndTask,
        task.nestLevel,
        tuple(_original_shape(child) for child in task.childTasksList),
    )


def _shape(descriptor):
    return (
        descriptor.block_index,
        descriptor.task_type,
        descriptor.class_name,
        descriptor.input,
        descriptor.is_control_start,
        descriptor.is_control_end,
        descriptor.nest_level,
        tuple(_shape(child) for child in descriptor.children),
    )


def _count(descriptors):
    return sum(1 + _count(task.children) for task in descriptors)


@pytest.mark.parametrize(
    ("name", "blocks"), [("ICMPCS.txt", 79), ("CSR_IAM_v2.txt", 82)]
)
def test_program_matches_original_process_query(name, blocks, tmp_path, monkeypatch):
    text = (FIXTURES / name).read_text(encoding="utf-8-sig")
    program = inspect_program(text)

    monkeypatch.chdir(tmp_path)
    manager = _spf_manager_type()()
    split = text.split(manager.SQLFILE_DELIM)
    roots = manager.Process_Query(
        0, len(split), split, "", "", None, len(split), "", "", ""
    )

    assert tuple(map(_shape, program.tasks)) == tuple(map(_original_shape, roots))
    assert _count(program.tasks) == blocks


def test_tree_keeps_source_indices_and_original_else_and_end_placement():
    text = DELIMITER.join(
        [
            "\n",
            utility('{IF-THEN} "COUNT" "GT" "0" "" "" "" ""'),
            block("/WRITE-FILE=Y\n/CSV=a.txt", "A"),
            utility("{ELSE}"),
            block("/WRITE-FILE=Y\n/CSV=b.txt", "B"),
            utility("{END-IF}"),
            "   \n",
            utility('{FOR-LOOP} "0" "3" "1" "1" "N"'),
            block("/WRITE-FILE=Y\n/CSV=c.txt", "C"),
            utility("{END-LOOP}"),
        ]
    )

    def layout(descriptor):
        return (
            descriptor.block_index,
            descriptor.class_name,
            tuple(map(layout, descriptor.children)),
        )

    assert tuple(map(layout, inspect_program(text).tasks)) == (
        (
            1,
            "IfThenTask",
            (
                (2, "WriteFileTask", ()),
                (3, "ElseTask", ((4, "WriteFileTask", ()),)),
                (5, "EndIfTask", ()),
            ),
        ),
        (7, "ForLoopTask", ((8, "WriteFileTask", ()),)),
        (9, "EndLoopTask", ()),
    )


def test_inspection_never_parses_or_executes_tasks(tmp_path, monkeypatch):
    legacy = _legacy()

    def forbidden(*args, **kwargs):
        pytest.fail("program inspection must not parse or execute a task")

    names = (
        "execute",
        "executeSteps",
        "parseTaskOptions",
        "parseTaskCommand",
        "executeTaskCommand",
        "executeTaskCommandSteps",
        "executeChildTasks",
        "executeChildTasksSteps",
    )
    for cls in vars(legacy).values():
        if isinstance(cls, type) and issubclass(cls, legacy.SPFTaskBase):
            for name in names:
                if name in vars(cls):
                    monkeypatch.setattr(cls, name, forbidden)

    monkeypatch.chdir(tmp_path)
    for text in (
        (FIXTURES / "CSR_IAM_v2.txt").read_text(encoding="utf-8-sig"),
        LOOPS_AND_HPC,
    ):
        assert _build_program(text).tasks


def test_inspect_program_leaves_the_caller_directory_untouched(tmp_path, monkeypatch):
    monkeypatch.chdir(tmp_path)
    classes = [task.class_name for task in inspect_program(LOOPS_AND_HPC).tasks]
    assert classes == [
        "ForLoopTask",
        "EndLoopTask",
        "SiteLoopTask",
        "EndLoopTask",
        "BeginHPCTask",
        "EndHPCTask",
    ]
    assert list(tmp_path.iterdir()) == []


def test_unroutable_task_is_reported_without_running_anything():
    with pytest.raises(ValueError, match="Handler not found"):
        inspect_program(utility('{NO-SUCH-UTILITY} "x"'))


def test_task_input_round_trips_through_the_original_codec():
    original = block(
        '/WRITE-FILE=Y\n/csv=first.csv\n/CSV=\n/CSV=second.csv\n/PROMPT-TEXT=Grüße "quoted" 東京',
        "line 1\n  line 2\n\n",
    )
    task_input = TaskInput.parse(original)
    assert task_input.options == (
        ("WRITE-FILE", "Y"),
        ("csv", "first.csv"),
        ("CSV", ""),
        ("CSV", "second.csv"),
        ("PROMPT-TEXT", 'Grüße "quoted" 東京'),
    )
    assert task_input.command == "line 1\n  line 2\n\n\n"
    assert TaskInput.parse(task_input.encode()) == task_input


@pytest.mark.parametrize(
    ("value", "expected"),
    [
        (
            '{ROWS-IN-FILE} "data.csv" "SIGNAL" "N" ""',
            ("{ROWS-IN-FILE}", "data.csv", "SIGNAL", "N", ""),
        ),
        (
            '{IF-THEN} "VAR(<<<A>>>)" "EQS" "Y"',
            ("{IF-THEN}", "VAR(<<<A>>>)", "EQS", "Y"),
        ),
        (
            r'@EXEDIR@\SPFDelete.bat "a.csv,b.csv" "N"',
            (r"@EXEDIR@\SPFDelete.bat", "a.csv,b.csv", "N"),
        ),
        ("{END-IF}", ("{END-IF}",)),
        ("getcsrsu.bat", ("getcsrsu.bat",)),
        ("setsiteparam.exe KM <<<SFOLDER>>>", None),
        ('{ROWS-IN-FILE} data.csv "SIGNAL"', None),
        ('{ROWS-IN-FILE}  "data.csv"', None),
        ('dir | find "x"', None),
    ],
)
def test_utility_is_structured_only_when_reencoding_is_exact(value, expected):
    assert TaskInput.parse(utility(value)).utility == expected


def test_duplicate_or_missing_utilities_have_no_structured_form():
    assert (
        TaskInput.parse(block("/UTILITIES={END-IF}\n/UTILITIES={END-IF}")).utility
        is None
    )
    assert TaskInput.parse(block("/utilities={END-IF}")).utility is None
    assert TaskInput.parse(block("/WRITE-FILE=Y", "x")).utility is None
