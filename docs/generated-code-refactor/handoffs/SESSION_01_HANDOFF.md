# Session 01 handoff — Macro state and substitution

**Repository:** `VectorLim/SQLPathFinder_PY_Migration`  
**Session branch:** `refactor/generated-code/s01-macros`  
**Exact immutable parent:** `d9211c81c4f72651329611aabfb2bc5c6abba504` (`html-and-sql-rework`, published plan commit)  
**Source/test implementation checkpoint:** `7c070c2e282a97ccc4dc603cb23f9adfedb2757e`  
**Measured CI revision:** `3ecd81c7dddc2f1689629ec706f16e76f42d3f03`  
**Final branch SHA:** the final remote HEAD is reported in the session response, because recording a commit's own SHA in the same committed file is self-referential.

**Status: WIP / relative-regression gate passed, full-suite acceptance gate incomplete.** The six full-suite UI test failures were reproduced against the unchanged parent under identical CI dependencies. No new failing tests were observed, but the full suite is still red. Session 02 must not treat this as a green full-suite handoff.

## Implemented changes / file ownership

- `src/vg2c/runtime/macros.py`: new compiler-independent, per-run `MacroStore`, with case-normalized frame stack, explicit snapshot-values ownership, raw `.items()` for existing runtime operations, dictionary-style read/write and `.scope()` / `.substitute()`.
- `src/vg2c/runtime/__init__.py`: public `MacroStore` export.
- `src/vg2c/emitter/project.py`: emit one `macros` store per `run()`, native Python `if`/`else`/`for`, `with macros.scope(row)` overlays for macro/loop bodies, `macros['NAME']` expressions and assignment, and `macros.substitute(...)` for full expressions/paths. Retain explicit CSV first-row guard. SQL/HTML/file runtime call signatures unchanged; no unrelated assets modified.
- `src/vg2c/semantics.py`, `src/vg2c/editing.py`: preserve control-header location lookup and semantic edit projection using the new unnumbered store, rather than references to obsolete `macro_values_N` names.
- `tests/runtime/test_scoped_macros.py`, `tests/emitter/test_macro_store_project.py`: new behavior/source-shape tests for nested first-row CSV scopes, zero-row suppression, exception restoration, independent runs, native control flow, token policy and metadata ranges.
- `tests/emitter/test_semantic_contracts.py`, `tests/ui/test_document_store.py`: update **only emitted-syntax assertions**, retaining behavior and symbol-selection requirements. No UI/frontend implementation modified.
- Temporary GitHub Actions workflow used only to collect test/metric evidence on the session branch; keep production and other branches untouched.

## Exact public macro contract

`MacroStore(values=job_values, initial=aed_config_or_None)` is constructed once within each generated `run()`; it does not use mutable module globals. `values` is a fresh independent snapshot, not a second mutable macro store.

`macros['key']` is a case-insensitive named macro read resolved through the **existing single** `vg2c.runtime.values.substitute` token resolver. Unknown ordinary names raise `ValueError("Unknown value ...")`; reserved `SPF-`, `SPF$`, `!` and `SPF_DATETIME` tokens preserve the direct-runtime rule; `CL_`, `SPF-JOB-`, `SPF-DEFAULT-`, and `%ENV%` resolve from snapshot values, including the original error behavior for absent environment names. `None` expands as empty, and newlines are preserved. Unsupported positional `<<>>` still errors. `macros.get(..., default)` may be used for optional lookups, but generated control expressions use strict bracket reads. Assignment `macros['key'] = value` writes into the top frame.

`with macros.scope(row_or_None):` pushes a normalized overlay, supports shadowing/writes, and restores the previous frame in `finally` even during exceptions or loop exits. CSV `START-MACRO` uses `read_macro_row` with `if row is not None:`, so header-only files execute no child code. Static START-MACRO and each FOR/SITE/RUN iteration have their own scoped frame. No per-scope copied dictionary or second substitution implementation was added. Old compiler/legacy `utilities.MacroState` remains for its existing users and retains its older blank-missing behavior; it is **not** used by directly generated runtime.

SQL, HTML and file operations still receive `workdir=workdir`, `values=job_values`, and `macros=macros` where already required. Reducing those runtime-call arguments is explicitly Session 02's task. Generated direct jobs do not import ScriptHost or require original VG2 input.

## Verified generated-code readability

The CI comparison compiled **the same sanitized nine-block fixture** from the exact parent and Session 01 revision, without using private production output.

| Measure | Parent | Session 01 |
| --- | ---: | ---: |
| Generated physical lines | 26 | 26 |
| Numbered `macro_values_<id>` mentions | 8 | **0** |
| `substitute(` calls | 3 | 2 |
| `.scope(` contexts | 0 | 2 |
| Direct `macros['COUNT']` references | 0 | 2 |

Representative *old shape*:

```python
macro_values_39 = {**macro_values, **{key.upper(): value for key, value in macro_row_39.items()}}
macro_values_39['CONFIG'] = str(row_count('ICMPCS_config.csv', workdir=workdir))
if int(substitute('<<<CONFIG>>>', values=job_values, macros=macro_values_39)) > int('0'):
    ...
```

Representative *new shape* (syntax verified by emitted synthetic tests; ellipses are illustrative):

```python
with macros.scope(macro_row_39):
    macros['CONFIG'] = str(row_count('ICMPCS_config.csv', workdir=workdir))
    if int(macros['CONFIG']) > int('0'):
        ...
```

The original committed `ICMPCS/main.py` reference remains unchanged and has not been regenerated or executed. Its planning-time baseline was 749 lines, 57 numbered macro-map occurrences and 33 `substitute(` calls. The synthetic example had **no physical LOC reduction**; complexity moved to the small central runtime store (72 source lines) and its existing canonical resolver rather than disappearing. More argument simplification belongs to Session 02.

## Exact verification evidence

**GitHub Actions run:** https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38057360962. The CI runner used Python 3.12 and installed the source as an editable package (`pip install --no-deps -e .`) for isolated `python -I` execution tests; pandas, pytest, oracledb, fastapi, httpx2 and other test-only dependencies were installed separately.

- `python -m compileall -q src`: **PASS**.
- `ruff check src/vg2c/runtime/macros.py`: **PASS**, `All checks passed!`.
- `python -m pytest -q tests/runtime/test_scoped_macros.py tests/emitter/test_macro_store_project.py tests/runtime/test_macro_state.py tests/runtime/test_control_parity.py tests/runtime/test_placeholder_substitution.py tests/emitter/test_generated_project.py tests/emitter/test_semantic_contracts.py`: **65 passed**.
- `python -m pytest -q` on unchanged parent SHA `d9211...`: **535 passed, 7 failed**.
- `python -m pytest -q` on Session 01 source: **541 passed, 6 failed**. All six remaining failures were also present and identically named in the parent run.
- Sanitized static emission metrics above were measured on both commits in CI.
- Runtime import isolation, SQL/HTML generated-project tests, source-range tests and emitted control tests ran as part of the passing focused/full-suite selections.

**Identical, pre-existing failing tests (not changed, removed, xfailed or weakened):**

1. `tests/ui/test_document_store.py::test_shared_global_edits_persist_across_steps`
2. `tests/ui/test_document_store.py::test_reorder_persists_execution_order_and_generation_state`
3. `tests/ui/test_document_store.py::test_html_preview_is_safe_exact_approximate_and_path_bounded`
4. `tests/ui/test_html_preview.py::test_html_preview_replays_safely_and_does_not_write_outputs` (unsupported JMP/JSL)
5. `tests/ui/test_workspace_sessions.py::test_sql_column_choices_read_uploaded_server_csv_headers`
6. `tests/ui/test_workspace_sessions.py::test_file_backed_sql_filter_uses_workspace_choices_through_save_and_generate`

The parent has one additional stale UI assertion expecting a legacy `ctx.macro.named` spelling that the parent direct emitter itself does not output. Session 01 changed that assertion to check the current readable bracket-syntax invariant and it passes.

**Verification limits:** the execution container's direct Git network checkout was unavailable, so local `git remote -v`, `git fetch --prune`, `git status --porcelain`, or existing worktrees could not be inspected; GitHub API instead verified the published parent and target branch ancestry. GitHub Actions checked out the exact SHAs in clean runners. There was no unsafe checkout overwrite, reset, force push or merge. Private ICMPCS production resources were not run; broad ScriptHost differential execution, non-synthetic installed-Linux system integration, complete Ruff over the entire pre-existing tree, and complete original binary/output parity are **not claimed**. These limitations keep the session labeled WIP rather than full acceptance.

## Required next-session conditions

1. Fetch current remote `refactor/generated-code/s01-macros` and verify its SHA against the final report; do **not** branch Session 02 from the old planning/inspection SHA.
2. Review Session 01 diff and all six inherited UI full-suite failures. Either obtain explicit acceptance of the inherited baseline failures or resolve them in a separately authorized scope; do not silently certify a green full suite.
3. Re-run both the macro-focused and full suites in the target deployment environment, retaining runtime-generated assets and source/edit metadata invariants.
4. Preserve `MacroStore` per-run state, canonical substitution/error rules, first-row CSV guard, exception-safe lexical restoration and native Python control flow. Never reintroduce numbered macro dictionaries or broad runtime wrappers here.
5. Session 02 owns runtime-call simplification, not substitution semantics. No Session 02 branch, merge or implementation was started by this session.
