from __future__ import annotations

from dataclasses import FrozenInstanceError
from pathlib import Path

import pytest

from vg2c_new.model import CommandKind
from vg2c_new.parser import RESOLVER_MANIFEST, Vg2ParseError, parse, parse_utility_arguments

DELIM = "<---- New Query ---->"


def block(*options: str, body: str = "") -> str:
    option_text = "\n".join(options)
    return f"<OPTIONS>\n{option_text}\n</OPTIONS>\n{body}"


def script(*blocks: str) -> str:
    return f"\n{DELIM}\n".join(blocks)


def test_bom_crlf_and_source_span() -> None:
    text = "\ufeff" + block('/UTILITIES=@Echo "hello world"')
    commands = parse(text.replace("\n", "\r\n"), source=Path("demo.txt"))
    assert commands[0].arguments == ("hello world",)
    assert commands[0].span.file == Path("demo.txt")
    assert commands[0].span.start_line == 1


def test_script_host_utility_argument_grammar() -> None:
    assert parse_utility_arguments(r'@Echo "a b" "" c') == ("@Echo", "a b", "", "c")


def test_report_precedence_over_other_routing_options() -> None:
    command = parse(block('/REPORT=HTML-RUN', '/UTILITIES=@Echo "ignored"'))[0]
    assert command.utility_type == "report.html_run"


def test_normal_query_resolution_matches_getquery() -> None:
    sqlite = parse(block('/NODE=.\\', '/UN=', '/OLEDB=SQLite', '/ENGINE=SQLite', body='select 1'))[0]
    oracle = parse(block('/NODE=KM.MARS', '/UN=user', '/OLEDB=SQLPlus', '/ENGINE=VA', body='select 1'))[0]
    assert sqlite.kind is CommandKind.QUERY
    assert sqlite.utility_type == "query.sqlite"
    assert oracle.utility_type == "query.oracle"


def test_unknown_utility_is_located_and_has_no_shell_fallback() -> None:
    with pytest.raises(Vg2ParseError, match=r"demo\.txt:1:1: Unsupported /UTILITIES command"):
        parse(block('/UTILITIES=totally-unknown.exe "x"'), source=Path("demo.txt"))


def test_command_is_immutable() -> None:
    command = parse(block('/UTILITIES=@Echo "x"'))[0]
    with pytest.raises(FrozenInstanceError):
        command.body = "changed"  # type: ignore[misc]


def test_nested_if_else_macro_and_loop_tree() -> None:
    commands = parse(
        script(
            block('/UTILITIES={START-MACRO} "m.csv" "Y"'),
            block('/UTILITIES={IF-THEN} "VAR(1)" "EQ" "1"'),
            block('/UTILITIES={FOR-LOOP} "0" "2" "1" "x" "N"'),
            block('/UTILITIES=@Echo "yes"'),
            block('/UTILITIES={END-LOOP}'),
            block('/UTILITIES={ELSE}'),
            block('/UTILITIES=@Echo "no"'),
            block('/UTILITIES={END-IF}'),
            block('/UTILITIES={END-MACRO}'),
        )
    )
    assert len(commands) == 1
    macro = commands[0]
    assert macro.kind is CommandKind.MACRO
    if_command = macro.children[0]
    assert if_command.kind is CommandKind.IF
    assert if_command.children[0].kind is CommandKind.FOR_LOOP
    assert if_command.else_children[0].utility_type == "echo"


def test_mismatched_controller_is_source_located() -> None:
    with pytest.raises(Vg2ParseError, match=r"<input>:1:1: Unclosed \{IF-THEN\}"):
        parse(script(block('/UTILITIES={IF-THEN} "VAR(1)" "EQ" "1"'), block('/UTILITIES=@Echo "x"')))


def test_hpc_scope_is_accepted_as_flattenable_syntax() -> None:
    commands = parse(script(block('/UTILITIES={BEGIN-HPC}'), block('/UTILITIES=@Echo "x"'), block('/UTILITIES={END-HPC}')))
    assert commands[0].kind is CommandKind.SCOPE
    assert commands[0].children[0].utility_type == "echo"


def test_22844_control_topology_and_empty_if_placeholders() -> None:
    # Routing/control skeleton taken from scripthost-utilities-decompiled/22844.spfsql.
    text = script(
        block(r'/UTILITIES={ROWS-IN-FILE} "products.json" "RowsInFile" "N" ""'),
        block(r'/UTILITIES={IF-THEN} "RowsInFile" "LT" "0" "" "" "" ""'),
        block(r'/UTILITIES=@EXEDIR@\SQLPathFinder_Email.va "" "self" "Missing config" "" "" "" "" "N" "N"'),
        block('/UTILITIES={ELSE}'),
        block(r'/UTILITIES=@EXEDIR@\RoboCopy.va "products.json" "dest" "." "10" "10" "N" "" "N"'),
        block('/UTILITIES={END-IF}'),
        block('/NODE=KM.[A15_PROD_21.].MARS', '/UN=//', '/OLEDB=SQLPlus', '/ENGINE=VA', body='select 1'),
        block('/NODE=KM.ARIES', '/UN=//', '/OLEDB=SQLPlus', '/ENGINE=VA', body='select 1'),
        block('/NODE=.\\', '/UN=', '/OLEDB=SQLite', '/ENGINE=SQLite', '/TABLE=a.tab,b.tab', body='select 1'),
        block(r'/UTILITIES=@EXEDIR@\WaitFile.va "XRAY_results.csv" "30"'),
        block('/WRITE-FILE=Y', '/CSV=payload.txt', body='payload'),
        block(r'/UTILITIES=@EXEDIR@\Run_Python_Script.va "xray.py" "--csv XRAY_results.csv" "N" "host" "Python-v3"'),
        block('/UTILITIES={START-MACRO} "configsets.csv" "Y"'),
        block('/UTILITIES={ROWS-IN-FILE} "XRAY_results.csv" "vid" "N" ""'),
        block('/UTILITIES={IF-THEN} "vid" "GT" "0" "" "" "" ""'),
        block('/NODE=.\\', '/UN=', '/OLEDB=SQLite', '/ENGINE=SQLite', '/TABLE=XRAY_results.csv', body='select 1'),
        block('/UTILITIES={END-IF}'),
        block('/UTILITIES={END-MACRO}'),
    )
    commands = parse(text)
    assert len(commands) == 9
    assert commands[1].kind is CommandKind.IF
    assert len(commands[1].children) == 1
    assert len(commands[1].else_children) == 1
    macro = commands[-1]
    assert macro.kind is CommandKind.MACRO
    assert macro.children[1].kind is CommandKind.IF
    assert macro.children[1].children[0].utility_type == "query.sqlite"


def test_manifest_is_seeded_for_session_2_and_contains_current_sample_aliases() -> None:
    aliases = {row[0].upper() for row in RESOLVER_MANIFEST}
    assert r"@EXEDIR@\RUN_PYTHON_SCRIPT.VA" in aliases
    assert r"@EXEDIR@\SQLPATHFINDER_EMAIL.VA" in aliases
    assert "{ROWS-IN-FILE}" in aliases
    assert len(RESOLVER_MANIFEST) == 108


def test_manifest_has_terminal_session_2_status_for_all_108_entries() -> None:
    statuses = {
        "implemented",
        "flattened obsolete transport",
        "historical version dropped",
        "explicitly retired capability",
        "documented current-platform gap",
    }
    assert len(RESOLVER_MANIFEST) == 108
    assert all(len(row) == 7 for row in RESOLVER_MANIFEST)
    assert {row[6] for row in RESOLVER_MANIFEST} <= statuses
    assert all(row[6] for row in RESOLVER_MANIFEST)
