"""Native macro headers are compiler-owned, not emitted through legacy MacroState."""

from vg2c.operands.macro import StartMacro


def test_csv_macro_header_uses_runtime_job():
    assert StartMacro("configsets.csv", False).render_header(7) == (
        "macro_row_7 = job.read_macro_row('configsets.csv')"
    )


def test_static_macro_header_uses_runtime_store():
    assert StartMacro("", False).render_header(7) == "with macros.scope():"
