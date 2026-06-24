# Stage 6 Emitter Correctness - Progress (2026-06-24)

## Completed

- Fixed output routing to use declared /CSV values instead of hardcoded step_NNNN outputs for SQL/reader handlers.
- Fixed WRITE_FILE path routing to avoid using /WRITE-FILE=Y as a filepath.
- Added shared output path resolution helper to reduce duplication and keep behavior consistent.
- Added prompt-text based function naming with deterministic step prefix and bounded slug length.
- Fixed IF expression emission:
- Added typed operator table (string vs numeric ops).
- Added numeric coercion for numeric comparisons.
- Added proper handling for VAR(...), <<<...>>>, and bare numeric-side identifiers.
- Fixed macro-name normalization so emitted ctx.macro.named(...) never keeps <<< >>> delimiters.
- Added ROWS-IN-FILE emission in walker:
- Emits explicit step function that sets named macro variable from ctx.csv_io.row_count(...).
- Added runtime contract signature ctx.macro.set_named(name, value) in runtime stub.
- Replaced silent handler exception swallowing with emitted diagnostics (emit-handler-failed).
- Added SQL macro token expansion support for @@SQLMACRO:n@@ into ctx.sql_macros.sql_get_csv_list(...).
- Improved utility argument emission to preserve argv and substitute macro-bearing values into Python expressions.
- Removed restrictive body-only filter in macro substitution component to support broader placeholder contexts.

## Validation

- Emitter fixture tests: 20 passed.
- Full repository tests: 161 passed.

## Added/Updated Tests

- Extended fixture tests with correctness assertions for:
- Declared output path preservation.
- ROWS-IN-FILE generated assignments.
- Macro-name normalization (no ctx.macro.named("<<<...")).

## Remaining Follow-up Items

- Add dedicated unit files from plan for finer-grained behavior:
- output path helper tests.
- condition builder matrix tests.
- utility argv edge-case tokenizer behavior.
- SQL macro expansion ordering edge tests.
- Investigate and replace naive utility token splitting with robust quote-aware parsing.
- Expand email utility mapping to structured argument semantics once positional contract is finalized.

## Notes

- The previously observed semantic mismatch (consumers expecting producer filenames but emitter writing step_NNNN.*) is resolved in emitted source.
- Current Stage 6 scope stayed within emitter/runtime stub boundaries and did not change upstream stage models.
