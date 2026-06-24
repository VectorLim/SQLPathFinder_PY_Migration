# SQL Formatting Fix (2026-06-24)

## Root Cause

- SQL emission in the emitter was using repr(sql) in src/vg2c/emitter/handlers.py.
- repr converts multiline SQL into escaped one-line text (\n), which removes readable indentation/formatting in emitted Python source.

## Minimal Fix Applied

- Added a small helper in src/vg2c/emitter/handlers.py:
- _python_multiline_literal(text)
- Returns triple-quoted Python literals for multiline SQL while keeping single-line behavior unchanged.
- Updated _sql_to_python_expr to use this helper for:
- full SQL bodies without @@SQLMACRO tokens
- literal segments around @@SQLMACRO tokens

## Validation

- Emitter tests: 20 passed.
- Full test suite: 161 passed.

## Scope

- Minimal and localized change: only src/vg2c/emitter/handlers.py.
- No API/model changes.
