# Session 01 — Macro state and substitution handoff (WIP: unverified tests)

**Repository:** `VectorLim/SQLPathFinder_PY_Migration`  
**Branch:** `refactor/generated-code/s01-macros`  
**Exact parent:** `d9211c81c4f72651329611aabfb2bc5c6abba504` (published planning commit)  
**Final commit:** see final agent response / remote branch HEAD; this file is part of the same commit, so its own SHA cannot be embedded.

## Scope delivered

- Added `vg2c.runtime.MacroStore` with a single per-run frame stack, case-insensitive writes, scoped push/pop in `finally`, explicit global snapshot, and no compiler imports.
- Reused the existing `vg2c.runtime.values.substitute` implementation for both bracket reads and whole-string substitution; no second regex or token evaluator.
- Reworked direct-project emission so static/CSV START-MACRO and RUN/FOR/SITE use `with macros.scope(...)`, preserving the first-row CSV guard and native if/else/for.
- Replaced `ctx.macro.named` generated accesses with `macros['NAME']` and `macro.set_named` assignments with `macros['NAME'] = ...`; retained runtime SQL/HTML/files call keywords and metadata.
- Reused `control_header` from semantic projection with uniform `macros` context, updating old textual expectations in the semantic tests.
- Added direct MacroStore unit tests and synthetic generated-project tests for nested scopes, independent runs, zero-row guarding, branch/assignment source shape and source ranges.

## Public contract

`MacroStore(values=job_values, initial=aed_config_or_None)` owns normalized, mutable macro frames. `macros['KEY']` expands one token via the canonical direct-runtime resolver; ordinary unknown names raise `ValueError('Unknown value ...')`, rather than returning blank (the old compiler-side `MacroState.named` uses a different contract and is retained). `None` expands as blank. Reserved SPF tokens remain literal; CL_/SPF-JOB-/SPF-DEFAULT-/%...% resolve from the per-run values snapshot. `macros['KEY'] = value` writes into the top frame; `with macros.scope(row_or_None):` shadows and restores, even on exceptions. `macros.substitute(text)` applies the same `runtime.values.substitute` and preserves newlines, errors, environment and placeholder behavior. `items()` intentionally supplies raw frame values to existing direct runtime operations.

The direct runtime still receives `workdir=workdir, values=job_values, macros=macros` until Session 02. `utilities.MacroState` is left intact for compiler/legacy users, not introduced as a second generated-runtime registry.

## Representative generated readability

Before, in the committed reference `ICMPCS/main.py` (old emitted format):

```python
macro_values_39 = {**macro_values, **{key.upper(): value for key, value in macro_row_39.items()}}
macro_values_39["CONFIG"] = str(row_count("ICMPCS_config.csv", workdir=workdir))
if int(substitute("<<<CONFIG>>>", values=job_values, macros=macro_values_39)) > int("0"):
    ...
```

New output **predicted from the changed emitter** (not executed/benchmarked here):

```python
with macros.scope(macro_row_39):
    macros['CONFIG'] = str(row_count('ICMPCS_config.csv', workdir=workdir))
    if int(macros['CONFIG']) > int('0'):
        ...
```

The original `ICMPCS/main.py` is an existing generated sample and has not been regenerated or executed; baseline in the master plan: 749 lines, 57 numbered macro-map occurrences and 33 `substitute(` occurrences. **No verified after LOC/count, behavioral differential results or run artifacts are claimed.** Long SQL/HTML keyword arguments remain (Session 02).

## Verification status and blockers

- Verified GitHub parent branch points to `d9211c81c4f72651329611aabfb2bc5c6abba504`, and target branch did not previously exist.
- GitHub direct file content and plan were inspected.
- Local `git clone` / `git ls-remote` failed because the execution container cannot resolve `github.com`; no local worktree, remote `git fetch`, branch status or executable baseline was accessible.
- **NOT RUN:** `python -m pytest -q tests/runtime/test_scoped_macros.py tests/emitter/test_macro_store_project.py`
- **NOT RUN:** `python -m pytest -q tests/runtime/test_macro_state.py tests/runtime/test_control_parity.py tests/runtime/test_placeholder_substitution.py tests/emitter/test_generated_project.py tests/emitter/test_semantic_contracts.py`
- **NOT RUN:** `python -m pytest -q`, `python -m compileall -q src`, `ruff check src tests`, external differential runtime comparison and sanitized `ICMPCS` regeneration.
- Consequently this handoff is **WIP**. Do not treat it as passing the Session 01 acceptance gate or launch Session 02 before running and correcting tests in a real checkout.

## Next agent constraints

1. Clone/fetch clean checkout of this exact branch and compare to parent; inspect the diff, run baseline-equivalent full suite and focused tests.
2. Reconcile any failed assertions with actual runtime semantics; do not remove assertions to force passing.
3. Regenerate sanitized representative outputs and compare output files/exceptions and `EmittedScript` invocation/parameter source offsets.
4. Validate all new runtime imports on Linux, packaging and source/edit metadata. Preserve the CLI and unrelated compiler modules.
5. Only after focused/full tests pass and checks are committed/pushed can Session 02 start from the resulting verified final SHA.
