from __future__ import annotations

from vg2c.utilities.csv_io import CsvIO


def test_column_by_name_parsed_as_string() -> None:
    body = 'WHERE SQL_Get_CSV_List(".\\f.tab", lot, "v1.lot In")'
    calls = CsvIO.scan_sql_get_csv_list_calls(body)
    assert len(calls) == 1
    assert calls[0].csv_path == ".\\f.tab"
    assert calls[0].column_ref == "lot"
    assert calls[0].lead_in == "v1.lot In"
    assert calls[0].needs_closing_paren is False


def test_column_by_index_parsed_as_int() -> None:
    body = 'WHERE SQL_Get_CSV_List(".\\f.csv", "2", "p.prodgroup3 In")'
    calls = CsvIO.scan_sql_get_csv_list_calls(body)
    assert calls[0].column_ref == 2


def test_lead_in_with_commas_preserved() -> None:
    body = 'WHERE SQL_Get_CSV_List(".\\f.csv", "2", "x In, y In")'
    calls = CsvIO.scan_sql_get_csv_list_calls(body)
    assert calls[0].lead_in == "x In, y In"


def test_two_calls_captured_with_source_order_spans() -> None:
    body = (
        'A SQL_Get_CSV_List(".\\a.csv", "1", "x In") '
        'OR SQL_Get_CSV_List(".\\b.csv", col, "y In")'
    )
    calls = CsvIO.scan_sql_get_csv_list_calls(body)
    assert len(calls) == 2
    assert calls[0].csv_path == ".\\a.csv"
    assert calls[1].csv_path == ".\\b.csv"
    assert calls[0].end <= calls[1].start


def test_unknown_sql_macro_not_captured() -> None:
    body = "WHERE SQL_Time_Range('x')"
    assert CsvIO.scan_sql_get_csv_list_calls(body) == []


def test_wrapped_call_site_flags_closing_paren() -> None:
    body = 'WHERE (ats.lot In \nSQL_Get_CSV_List(".\\f.tab", "2", "ats.lot In")'
    calls = CsvIO.scan_sql_get_csv_list_calls(body)
    assert len(calls) == 1
    assert calls[0].needs_closing_paren is True


def test_unwrapped_call_site_has_no_flag() -> None:
    body = (
        'WHERE p.prodgroup3 In \nSQL_Get_CSV_List(".\\f.csv", "2", "p.prodgroup3 In")'
    )
    calls = CsvIO.scan_sql_get_csv_list_calls(body)
    assert len(calls) == 1
    assert calls[0].needs_closing_paren is False


def test_malformed_arg_count_skipped() -> None:
    body = 'WHERE SQL_Get_CSV_List(".\\f.csv", "2")'
    assert CsvIO.scan_sql_get_csv_list_calls(body) == []


def test_spans_slice_back_to_call_text() -> None:
    body = 'A SQL_Get_CSV_List(".\\a.csv", "1", "x In") B'
    calls = CsvIO.scan_sql_get_csv_list_calls(body)
    assert body[calls[0].start : calls[0].end].startswith("SQL_Get_CSV_List")
    assert body[calls[0].start : calls[0].end].endswith(")")
