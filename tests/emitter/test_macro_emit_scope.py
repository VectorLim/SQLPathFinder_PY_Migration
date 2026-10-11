"""Native macro headers are compiler-owned, not emitted through legacy MacroState."""

from vg2c.emitter.project import control_header
from vg2c.operands.macro import StartMacro


def test_csv_macro_header_uses_runtime_job():
    assert control_header(StartMacro("configsets.csv", False), 7) == (
        "macro_row_7 = job.read_macro_row('configsets.csv')"
    )


def test_static_macro_header_uses_runtime_store():
    assert control_header(StartMacro("", False), 7) == "with macros.scope():"
