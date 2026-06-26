# Stage 7 - SQL Macro Parenthesis Fix (ORA-00907)

Date: 2026-06-25

## Problem
Runtime-expanded SQL for `SQL_Get_CSV_List(...)` could produce invalid Oracle syntax when the IN list exceeded 1000 values and was chunked. The previous chunk join logic emitted a second `lead_in` predicate without a boolean connector, which can surface as `ORA-00907: missing right parenthesis` in the generated SQL.

## Root Cause
In `src/vg2c_runtime/sql_macros.py`, `SqlMacros.sql_get_csv_list()` built multi-chunk output as:
- `(<chunk1>)`
- `\n<lead_in> `
- `(<chunk2>)`

This yields adjacent predicates with no operator between chunk groups.

## Minimal Fix Implemented
Updated chunk connector generation to insert `OR` between chunked predicates:
- `\nOR <lead_in> `

This preserves existing architecture (resolver/emitter unchanged) and only fixes runtime macro assembly.

## Files Changed
- `src/vg2c_runtime/sql_macros.py`
  - Updated docstring to match real call shape (`<column> In SQL_Get_CSV_List(...)`).
  - Changed multi-chunk connector from `\n{lead_in} ` to `\nOR {lead_in} `.
- `tests/runtime/test_sql_macros.py`
  - Strengthened `test_chunking_at_1000` to assert:
    - `OR v In` connector is present
    - generated chunk expression has balanced top-level chunk parentheses count

## Verification
Ran targeted tests:
- `tests/runtime/test_sql_macros.py`
- Result: 6 passed, 0 failed.

## Notes
- Per request, ignored standalone copied digit artifacts (for example isolated `3`) and focused on the missing right parenthesis translation path.
- This is the minimal correction for chunk-join SQL validity; no resolver/emitter behavior was broadened.
