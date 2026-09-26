from __future__ import annotations

from pathlib import Path

import pytest

from vg2c_new.model import Command
from vg2c_new.parser import parse
from vg2c_new.runtime import Interpreter, RuntimeState, compare_vars
from vg2c_new.utilities import Utility
from vg2c_new.utilities.files import DeleteFileUtility, WriteFileUtility

DELIM = "<---- New Query ---->"


def block(*options: str, body: str = "") -> str:
    option_text = "\n".join(options)
    return f"<OPTIONS>\n{option_text}\n</OPTIONS>\n{body}"


def script(*blocks: str) -> str:
    return f"\n{DELIM}\n".join(blocks)


class Capture(Utility):
    def __init__(self) -> None:
        self.values: list[str] = []

    def apply(self, command: Command, state: RuntimeState) -> None:
        self.values.append("|".join(state.substitute(arg) for arg in command.arguments))


class FailOnB(Utility):
    def __init__(self) -> None:
        self.values: list[str] = []

    def apply(self, command: Command, state: RuntimeState) -> None:
        value = state.substitute(command.arguments[0])
        self.values.append(value)
        if value == "B":
            raise RuntimeError("site failed")


class AlwaysFail(Utility):
    def __init__(self) -> None:
        self.calls = 0

    def apply(self, command: Command, state: RuntimeState) -> None:
        self.calls += 1
        raise RuntimeError("intentional child failure")


class SnapshotOutput(Utility):
    def __init__(self, path: Path) -> None:
        self.path = path
        self.snapshots: list[str] = []

    def apply(self, command: Command, state: RuntimeState) -> None:
        self.snapshots.append(self.path.read_text(encoding="utf-8"))


def test_state_substitution_frames_globals_environment_and_reserved_tokens(tmp_path: Path) -> None:
    state = RuntimeState(tmp_path, globals={"G": "global"}, environment={"HOME_X": "env"})
    with state.frame({"name": "macro", "spf-site": "KM"}):
        assert state.substitute("<<<G>>>/<<<name>>>/<<<spf-site>>>/<<<%HOME_X%>>>") == "global/macro/KM/env"
        assert state.lookup("NAME") == "macro"
    assert state.lookup("name") is None
    assert state.substitute("<<<spf-unknown>>>") == "<<<spf-unknown>>>"
    with pytest.raises(RuntimeError, match="Macro variable"):
        state.substitute("<<<ordinary-missing>>>")


@pytest.mark.parametrize(
    ("left", "right", "operator", "expected"),
    [
        ("2", "2", "EQ", True),
        ("2", "3", "NE", True),
        ("2", "3", "LT", True),
        ("3", "3", "LE", True),
        ("4", "3", "GT", True),
        ("4", "4", "GE", True),
        ("abc", "ABC", "EQS", True),
        ("abc", "DEF", "NES", True),
        ("4", "1,5", "BT", True),
        ("1", "1,5", "NBT", True),
    ],
)
def test_compare_vars_current_operators(left: str, right: str, operator: str, expected: bool) -> None:
    assert compare_vars(left, right, operator) is expected


def test_compare_vars_reports_numeric_type_mismatch() -> None:
    with pytest.raises(ValueError, match="datatype mismatch"):
        compare_vars("abc", "2", "GT")


def test_if_else_two_conditions_and_22844_empty_placeholders(tmp_path: Path) -> None:
    capture = Capture()
    commands = parse(
        script(
            block('/UTILITIES={IF-THEN} "Rows" "GT" "0" "" "" "" ""'),
            block('/UTILITIES=@Echo "one"'),
            block('/UTILITIES={ELSE}'),
            block('/UTILITIES=@Echo "wrong-one"'),
            block('/UTILITIES={END-IF}'),
            block('/UTILITIES={IF-THEN} "VAR(abc)" "EQS" "ABC" "AND" "count" "GE" "2"'),
            block('/UTILITIES=@Echo "two"'),
            block('/UTILITIES={ELSE}'),
            block('/UTILITIES=@Echo "wrong-two"'),
            block('/UTILITIES={END-IF}'),
        )
    )
    Interpreter({"echo": capture}).execute(
        commands,
        RuntimeState(tmp_path, globals={"Rows": "3", "count": "2"}),
    )
    assert capture.values == ["one", "two"]


def test_if_rejects_partially_filled_second_condition(tmp_path: Path) -> None:
    commands = parse(
        script(
            block('/UTILITIES={IF-THEN} "VAR(1)" "EQ" "1" "AND" "" "EQ" "2"'),
            block('/UTILITIES=@Echo "x"'),
            block('/UTILITIES={END-IF}'),
        )
    )
    with pytest.raises(RuntimeError, match="partially specified"):
        Interpreter({"echo": Capture()}).execute(commands, RuntimeState(tmp_path))


def test_macro_uses_only_first_data_row_and_does_not_leak_state(tmp_path: Path) -> None:
    (tmp_path / "m.csv").write_text("name,value\nalpha,1\nbeta,2\n", encoding="utf-8")
    capture = Capture()
    commands = parse(
        script(
            block('/UTILITIES={START-MACRO} "m.csv" "Y"'),
            block('/UTILITIES=@Echo "<<<name>>>" "<<<value>>>"'),
            block('/UTILITIES={END-MACRO}'),
        )
    )
    state = RuntimeState(tmp_path)
    Interpreter({"echo": capture}).execute(commands, state)
    assert capture.values == ["alpha|1"]
    assert state.lookup("name") is None
    assert state.lookup("value") is None


def test_macro_missing_file_default_continues_and_strict_mode_fails(tmp_path: Path) -> None:
    default_commands = parse(
        script(
            block('/UTILITIES={START-MACRO} "missing.csv"'),
            block('/UTILITIES=@Echo "never"'),
            block('/UTILITIES={END-MACRO}'),
        )
    )
    capture = Capture()
    Interpreter({"echo": capture}).execute(default_commands, RuntimeState(tmp_path))
    assert capture.values == []

    strict_commands = parse(
        script(
            block('/UTILITIES={START-MACRO} "missing.csv" "N"'),
            block('/UTILITIES=@Echo "never"'),
            block('/UTILITIES={END-MACRO}'),
        )
    )
    with pytest.raises(RuntimeError, match="Macro file not found"):
        Interpreter({"echo": capture}).execute(strict_commands, RuntimeState(tmp_path))


def test_for_loop_forward_version_2_tokens(tmp_path: Path) -> None:
    capture = Capture()
    commands = parse(
        script(
            block('/UTILITIES={FOR-LOOP} "0" "2" "1" "x" "N"'),
            block(
                '/UTILITIES=@Echo "<<<spf-start-x>>>" "<<<spf-end-x>>>" '
                '"<<<spf-step-x>>>" "<<<spf-loop-ctr-x>>>" "<<<spf-loop-ctr-x-int>>>"'
            ),
            block('/UTILITIES={END-LOOP}'),
        )
    )
    state = RuntimeState(tmp_path)
    Interpreter({"echo": capture}).execute(commands, state)
    assert capture.values == [
        "0|2|-1.0|0.0|0",
        "0|2|-1.0|1.0|1",
        "0|2|-1.0|2.0|2",
    ]
    assert state.lookup("spf-loop-ctr-x") is None


def test_for_loop_defaults_to_reverse_and_rejects_historical_version_selector(tmp_path: Path) -> None:
    capture = Capture()
    reverse_commands = parse(
        script(
            block('/UTILITIES={FOR-LOOP} "0" "2" "1" "x"'),
            block('/UTILITIES=@Echo "<<<spf-start-x>>>" "<<<spf-end-x>>>" "<<<spf-step-x>>>" "<<<spf-loop-ctr-x>>>"'),
            block('/UTILITIES={END-LOOP}'),
        )
    )
    Interpreter({"echo": capture}).execute(reverse_commands, RuntimeState(tmp_path))
    assert capture.values == ["2|0|1.0|2.0", "2|0|1.0|1.0", "2|0|1.0|0.0"]

    historical = parse(
        script(
            block('/UTILITIES={FOR-LOOP} "0" "1" "1" "x" "N" "Y"'),
            block('/UTILITIES=@Echo "x"'),
            block('/UTILITIES={END-LOOP}'),
        )
    )
    with pytest.raises(RuntimeError, match="Historical .* version selection"):
        Interpreter({"echo": Capture()}).execute(historical, RuntimeState(tmp_path))


def test_site_loop_preserves_actual_break_after_first_failing_site(tmp_path: Path) -> None:
    fail = FailOnB()
    commands = parse(
        script(
            block('/UTILITIES={SITE-LOOP} "A,B,C"'),
            block('/UTILITIES=@Echo "<<<spf-site>>>"'),
            block('/UTILITIES={END-LOOP}'),
        )
    )
    Interpreter({"echo": fail}).execute(commands, RuntimeState(tmp_path))
    assert fail.values == ["A", "B"]


def test_run_loop_chunks_data_and_rewrites_output_for_each_chunk(tmp_path: Path) -> None:
    (tmp_path / "input.csv").write_text("id,value\n1,a\n2,b\n3,c\n", encoding="utf-8")
    snapshots = SnapshotOutput(tmp_path / "output.csv")
    commands = parse(
        script(
            block('/UTILITIES={RUN-LOOP} "input.csv" "output.csv" "2" "N"'),
            block('/UTILITIES=@Echo "snapshot"'),
            block('/UTILITIES={END-LOOP}'),
        )
    )
    Interpreter({"echo": snapshots}).execute(commands, RuntimeState(tmp_path))
    assert snapshots.snapshots == [
        "id,value\n1,a\n2,b\n",
        "id,value\n3,c\n",
    ]


def test_run_loop_error_trapping_matches_continue_flag(tmp_path: Path) -> None:
    (tmp_path / "input.csv").write_text("id\n1\n2\n3\n", encoding="utf-8")

    strict_fail = AlwaysFail()
    strict_commands = parse(
        script(
            block('/UTILITIES={RUN-LOOP} "input.csv" "out.csv" "2" "N"'),
            block('/UTILITIES=@Echo "x"'),
            block('/UTILITIES={END-LOOP}'),
        )
    )
    with pytest.raises(RuntimeError, match="intentional child failure"):
        Interpreter({"echo": strict_fail}).execute(strict_commands, RuntimeState(tmp_path))
    assert strict_fail.calls == 1

    continue_fail = AlwaysFail()
    continue_commands = parse(
        script(
            block('/UTILITIES={RUN-LOOP} "input.csv" "out.csv" "2" "Y"'),
            block('/UTILITIES=@Echo "x"'),
            block('/UTILITIES={END-LOOP}'),
        )
    )
    Interpreter({"echo": continue_fail}).execute(continue_commands, RuntimeState(tmp_path))
    assert continue_fail.calls == 2


def test_hpc_scope_is_flattened_to_local_execution(tmp_path: Path) -> None:
    capture = Capture()
    commands = parse(
        script(
            block('/UTILITIES={BEGIN-HPC}'),
            block('/UTILITIES=@Echo "inside"'),
            block('/UTILITIES={END-HPC}'),
        )
    )
    Interpreter({"echo": capture}).execute(commands, RuntimeState(tmp_path))
    assert capture.values == ["inside"]


def test_unbound_supported_utility_has_source_located_runtime_error(tmp_path: Path) -> None:
    commands = parse(block('/UTILITIES=@Echo "x"'), source=tmp_path / "demo.txt")
    with pytest.raises(RuntimeError, match=r"demo\.txt:1:1: No runtime utility bound"):
        Interpreter().execute(commands, RuntimeState(tmp_path))


def test_direct_execution_vertical_slice_and_repeatability(tmp_path: Path) -> None:
    (tmp_path / "macro.csv").write_text("name,flag\nalpha,1\nbeta,0\n", encoding="utf-8")
    commands = parse(
        script(
            block('/WRITE-FILE=Y', '/CSV=seed.txt', body="seed<EOF>ignored"),
            block('/UTILITIES={START-MACRO} "macro.csv" "N"'),
            block('/UTILITIES={IF-THEN} "flag" "GT" "0"'),
            block('/UTILITIES={FOR-LOOP} "0" "2" "1" "x" "N"'),
            block(
                '/WRITE-FILE=Y',
                '/CSV=out_<<<name>>>_<<<spf-loop-ctr-x-int>>>.txt',
                body="<<<name>>>:<<<spf-loop-ctr-x-int>>>",
            ),
            block('/UTILITIES={END-LOOP}'),
            block('/UTILITIES={ELSE}'),
            block('/WRITE-FILE=Y', '/CSV=bad.txt', body="wrong branch"),
            block('/UTILITIES={END-IF}'),
            block('/UTILITIES={END-MACRO}'),
            block(r'/UTILITIES=@EXEDIR@\SPFDelete.bat "seed.txt" "Y"'),
        )
    )
    interpreter = Interpreter(
        {
            "write_file": WriteFileUtility(),
            "delete_file": DeleteFileUtility(),
        }
    )
    state = RuntimeState(tmp_path)

    interpreter.execute(commands, state)
    interpreter.execute(commands, state)

    assert not (tmp_path / "seed.txt").exists()
    assert not (tmp_path / "bad.txt").exists()
    assert [(tmp_path / f"out_alpha_{i}.txt").read_text(encoding="utf-8") for i in range(3)] == [
        "alpha:0",
        "alpha:1",
        "alpha:2",
    ]
    assert state.lookup("name") is None
    assert state.lookup("flag") is None
    assert state.lookup("spf-loop-ctr-x") is None


def test_write_file_supports_blank_body_and_case_insensitive_eof(tmp_path: Path) -> None:
    interpreter = Interpreter({"write_file": WriteFileUtility()})
    state = RuntimeState(tmp_path)

    blank = parse(block('/WRITE-FILE=Y', '/CSV=blank.txt', body=""))[0]
    interpreter.execute((blank,), state)
    assert (tmp_path / "blank.txt").read_text(encoding="utf-8") == ""

    eof = parse(block('/WRITE-FILE=Y', '/CSV=eof.txt', body="before<eOf>after"))[0]
    interpreter.execute((eof,), state)
    assert (tmp_path / "eof.txt").read_text(encoding="utf-8") == "before"


def test_delete_supports_comma_lists_wildcards_and_missing_targets(tmp_path: Path) -> None:
    for name in ("a.tmp", "b.tmp", "keep.txt"):
        (tmp_path / name).write_text(name, encoding="utf-8")
    command = parse(r'''<OPTIONS>
/UTILITIES=@EXEDIR@\SPFDelete.bat "*.tmp,missing.file" "Y"
</OPTIONS>
''')[0]
    Interpreter({"delete_file": DeleteFileUtility()}).execute((command,), RuntimeState(tmp_path))
    assert not (tmp_path / "a.tmp").exists()
    assert not (tmp_path / "b.tmp").exists()
    assert (tmp_path / "keep.txt").exists()
