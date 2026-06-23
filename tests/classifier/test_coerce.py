from __future__ import annotations

from vg2c.classifier.coerce import (
    as_bool_yn,
    as_csv_list,
    as_int,
    as_path_string,
    as_record_ref,
    split_shell_args,
)


def test_as_int_valid() -> None:
    assert as_int("42") == 42
    assert as_int("0") == 0


def test_as_int_invalid() -> None:
    assert as_int(None) is None
    assert as_int("abc") is None
    assert as_int("") is None


def test_as_bool_yn_true() -> None:
    assert as_bool_yn("Y") is True
    assert as_bool_yn("y") is True
    assert as_bool_yn("YES") is True
    assert as_bool_yn("yes") is True


def test_as_bool_yn_false() -> None:
    assert as_bool_yn("N") is False
    assert as_bool_yn("n") is False
    assert as_bool_yn("NO") is False
    assert as_bool_yn("no") is False


def test_as_bool_yn_default() -> None:
    assert as_bool_yn(None) is False
    assert as_bool_yn(None, default=True) is True
    assert as_bool_yn("invalid") is False
    assert as_bool_yn("invalid", default=True) is True


def test_as_csv_list_basic() -> None:
    assert as_csv_list("a,b,c") == ["a", "b", "c"]
    assert as_csv_list("foo, bar, baz") == ["foo", "bar", "baz"]


def test_as_csv_list_empty() -> None:
    assert as_csv_list(None) == []
    assert as_csv_list("") == []
    assert as_csv_list("  ") == []


def test_as_csv_list_with_empties() -> None:
    assert as_csv_list("a,,c") == ["a", "c"]
    assert as_csv_list("a, , c") == ["a", "c"]


def test_as_record_ref_valid() -> None:
    ref = as_record_ref("WIP_Lot_History_v2@1.0.0.0")
    assert ref is not None
    assert ref.name == "WIP_Lot_History_v2"
    assert ref.version == "1.0.0.0"


def test_as_record_ref_invalid() -> None:
    assert as_record_ref(None) is None
    assert as_record_ref("NoAtSymbol") is None
    assert as_record_ref("") is None


def test_as_path_string_no_quotes() -> None:
    assert as_path_string("foo.csv") == "foo.csv"
    assert as_path_string(r"\\server\share\file.csv") == r"\\server\share\file.csv"


def test_as_path_string_with_quotes() -> None:
    assert as_path_string('"foo.csv"') == "foo.csv"
    assert as_path_string("'foo.csv'") == "foo.csv"


def test_as_path_string_empty() -> None:
    assert as_path_string(None) is None
    assert as_path_string("") is None


def test_split_shell_args_basic() -> None:
    exe, args = split_shell_args("foo.va arg1 arg2")
    assert exe == "foo.va"
    assert args == ["arg1", "arg2"]


def test_split_shell_args_with_quotes() -> None:
    exe, args = split_shell_args('foo.va "arg with spaces" bar')
    assert exe == "foo.va"
    assert args == ["arg with spaces", "bar"]


def test_split_shell_args_empty() -> None:
    exe, args = split_shell_args("")
    assert exe == ""
    assert args == []


def test_split_shell_args_complex() -> None:
    exe, args = split_shell_args(
        '@EXEDIR@\\Run_Python_Script.va "lich.py" "" "N" "atd_atm.hadoop" "Python-v3"'
    )
    assert exe == "@EXEDIR@\\Run_Python_Script.va"
    assert args == ["lich.py", "", "N", "atd_atm.hadoop", "Python-v3"]
