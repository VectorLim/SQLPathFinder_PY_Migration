# Macro subsystem consolidation

All emitter-side macro operations were consolidated into a single module
`src/vg2c/emitter/macro.py`. Compile-stage *discovery* (scope-aware scanning
producing `RuntimeMacroRef` metadata) intentionally stayed in
`vg2c.resolver.macro_resolver` because it belongs to an earlier pipeline
stage; the emitter module owns everything else macro-related.

## What now lives in `src/vg2c/emitter/macro.py`

- `PLACEHOLDER_RE` (named + positional) and `NAMED_PLACEHOLDER_RE` patterns.
- `MacroLookup` protocol.
- `normalize_macro_name(raw)` canonical name extractor.
- `MacroState` runtime store, with a new `substitute_sql(sql)` method that
  owns the previous `_substitute_sql_macros` behaviour.
- `write_file(path, template, vars, macro_state)` — moved from the deleted
  `write_file.py`.
- `placeholders_to_python_expr(text)` and `macro_token_to_python_expr(raw)`
  compile-time emitter helpers, replacing duplicated regex/helper blocks in
  `handlers.py` and `walker.py`.

## Files removed

- `src/vg2c/emitter/macro_subst.py` — dead `MacroSubstituter` class
  (never invoked anywhere; only instantiated and stored on `EmitContext`).
- `src/vg2c/emitter/write_file.py` — folded into `macro.py`.

## Files updated

- `src/vg2c/emitter/_reader.py` — embedded snippet now calls
  `macro_state.substitute_sql(sql)` instead of carrying its own regex; the
  inlined `import re` is gone.
- `src/vg2c/emitter/__init__.py` — no longer imports/assigns the dead
  `MacroSubstituter`.
- `src/vg2c/emitter/models.py` — removed unused `macro_subst` field from
  `EmitContext`.
- `src/vg2c/emitter/handlers.py` — uses `placeholders_to_python_expr`;
  removed local `_PLACEHOLDER_RE` and `_macro_name`.
- `src/vg2c/emitter/walker.py` — uses `macro_token_to_python_expr` and
  `NAMED_PLACEHOLDER_RE`; removed local `_MACRO_TOKEN_RE` and `_macro_name`.
- `src/vg2c_runtime/__init__.py` — `write_file` re-export points to
  `vg2c.emitter.macro`.
- `tests/runtime/test_write_file_and_readers.py` — import path updated; the
  obsolete `"import re" in READER_SNIPPET` assertion replaced with a
  `"DATABASE_TYPE_MAP" in READER_SNIPPET` check that still proves the
  embedded snippet contains the expected runtime contents.

## Dependencies

No temporary stubs or backwards-compat shims were left behind. Every former
caller of `vg2c.emitter.write_file`, `vg2c.emitter.macro_subst`, or
`_substitute_sql_macros` was redirected to the consolidated API. The
resolver-side `NAMED_PLACEHOLDER_RE` (different module, different pattern)
is unaffected.

## Verification

- Static error check on all touched files: no errors.
- Import smoke test under `.venv` succeeded; `macro.__all__` exposes the
  expected public surface and `READER_SNIPPET` still embeds the runtime
  pieces (`DATABASE_TYPE_MAP`, `substitute_sql`).
- Test suite was not executed per request.

## Additional consolidation: SqlMacros folded into macro.py

The SQL macro runtime functionality was also moved into
`src/vg2c/emitter/macro.py`:

- Added `_read_column`, `_single_quote`, and `SqlMacros.sql_get_csv_list(...)`
  to the macro module.
- Exported `SqlMacros` via `macro.__all__`.
- Updated `src/vg2c_runtime/context.py` to import `SqlMacros` from
  `vg2c.emitter.macro`.
- Updated `src/vg2c_runtime/__init__.py` to re-export `SqlMacros` from
  `vg2c.emitter.macro`.
- Removed obsolete `src/vg2c_runtime/sql_macros.py`.

Post-move checks:

- No unresolved imports/usages of `vg2c_runtime.sql_macros` remain under `src/`.
- Static diagnostics still show no errors in touched runtime/emitter files.
- Import smoke test confirms `PipelineContext().sql_macros` is present and the
  relocated `SqlMacros` class resolves correctly.

## Follow-up split: SQL-exclusive macros into `sql_macro.py`

To keep SQLPathFinder placeholder macros and SQL-only macros separated, SQL macro
runtime logic was extracted from `src/vg2c/emitter/macro.py` into a dedicated
`src/vg2c/emitter/sql_macro.py` module.

- Added `src/vg2c/emitter/sql_macro.py` with `SqlMacros` plus CSV/list helpers.
- Removed `SqlMacros` and its helper functions from `src/vg2c/emitter/macro.py`.
- Updated runtime wiring:
  - `src/vg2c_runtime/context.py` now imports `SqlMacros` from
    `vg2c.emitter.sql_macro`.
  - `src/vg2c_runtime/__init__.py` re-exports `SqlMacros` from
    `vg2c.emitter.sql_macro`.

Validation after split:

- Static diagnostics show no errors in all touched files.
- Import smoke test under `.venv` succeeded with `PipelineContext().sql_macros`
  still available and bound to the relocated class.

## Additional migration: sqlite engine moved to emitter

The SQLite join runtime was migrated from runtime package to emitter package:

- Added `src/vg2c/emitter/sqlite_engine.py` with the full `SqliteEngine`
  implementation (CSV loading, statement splitting, join execution, CSV output).
- Removed `src/vg2c_runtime/sqlite_engine.py`.
- Updated `src/vg2c_runtime/context.py` to import `SqliteEngine` from
  `vg2c.emitter.sqlite_engine`.
- Updated `src/vg2c_runtime/__init__.py` re-export path accordingly.
- Updated `tests/runtime/test_sqlite_engine.py` import path to
  `vg2c.emitter.sqlite_engine`.

Validation after migration:

- No stale imports of `vg2c_runtime.sqlite_engine` remain in `src/` or tests.
- Static diagnostics show no errors in touched files.
- Import smoke test confirms `PipelineContext().sqlite_engine` is an instance
  of `vg2c.emitter.sqlite_engine.SqliteEngine`.
