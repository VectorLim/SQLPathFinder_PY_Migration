# Core pipeline large-file refactor

Branch: `refactor/core-pipeline-large-files`

This refactor was driven by responsibility ownership, not a target line count. The
starting branch had five Python modules above 500 lines in `src/vg2c`.

| Module | Before | After | Decision |
| --- | ---: | ---: | --- |
| `semantics.py` | 987 | 177 | Split models, control projection, leaf projection and symbol analysis. |
| `sql_editor/parser.py` | 799 | 352 | Split token/span mechanics, predicate parsing and FROM/JOIN parsing. |
| `workflow.py` | 780 | 210 | Split workspace links, file effects/models and reorder feasibility. |
| `editing.py` | 628 | 270 | Split edit models, validation/serialization and source projection. |
| `runtime/html.py` | 507 | 140 | Split template-slot safety and declarative report rendering. |

## Secondary large modules reviewed

- `utilities/html_report.py` — 459 lines. Retained as one module because the
  class is the authoritative compile-time HTML utility and its methods share the
  same report/emission state. Splitting it now would introduce coordination
  wrappers without removing duplicated ownership.
- `emitter/operations.py` — 462 lines. Retained because it is one descriptor
  framework: operation metadata, bound/unbound descriptor behavior, argument
  rendering and schema derivation are tightly coupled.
- `emitter/source_map.py` — 390 lines. Cohesive source-map construction and
  finalization; no unrelated responsibility found.
- `dataflow/file_effects.py` — 370 lines. Cohesive file-effect state ordering
  and binding logic.
- `runtime/crosstab.py` — 330 lines. One runtime crosstab implementation; no
  separate ownership boundary justified.
- `runtime/job.py` — 259 lines. Direct per-run runtime facade; methods are
  intentionally thin delegates over runtime utilities.

No new Python module created by the refactor exceeds 500 lines. The largest new
private modules are `_semantic_leaf_operations.py` (367) and
`runtime/_html_reports.py` (301).

## Compatibility rules

- Existing public imports remain available through `vg2c.semantics`,
  `vg2c.workflow`, `vg2c.editing`, `vg2c.sql_editor.parser`, and
  `vg2c.runtime.html`.
- Refactoring does not change ScriptHost translation semantics intentionally.
- UI tests are outside this core refactor scope.
- AED-dependent tests are excluded from the validation workflow.
- Generated runtime construction retains keyword arguments because tests and callers
  deliberately inject keyword-only `JobRuntime` factories/subclasses. The runtime test
  now scopes its no-`workdir=workdir` assertion to emitted operation calls instead of
  accidentally matching the constructor.

## Validation

The branch validation workflow compiles all sources, runs Ruff undefined-name
checks across `src/vg2c`, and runs the frontend, resolver, dispatch, emitter,
runtime, SQL-editor and dataflow suites while excluding AED-specific and UI tests.

Final core validation at `af846bb5088c9f702c54669ec8b43379bbcf88df`:
`605 passed, 1 skipped, 1 warning` with compile and Ruff checks passing.
