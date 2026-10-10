# Session 01 — scoped macro state and concise generated expressions

**Status:** Implementation instructions only; not yet executed. **Parent:** published planning HEAD of `origin/html-and-sql-rework` (inspection baseline was `afb701c2013537ffbf3c1e1b86e147ad0c900d1d`; do **not** branch from this older SHA after docs publication). **Own branch:** `refactor/generated-code/s01-macros`. **Next:** Session 02 creates its own branch from this session's tested, pushed HEAD and verified handoff.

## Objective / boundary

Replace verbose `macro_values`, `macro_values_9`, `macro_values_39`, `macro_row_*` copied-dictionary chains and repeated `substitute('<<<NAME>>>', values=..., macros=...)` **in generated business logic** with a single per-run, dictionary-like macro object and explicit scope operations. Preserve native `if`, `else`, `for` and guarded scope execution. Do not alter the frontend, query/table semantics, report assets or function-call context (Session 02).

## Code and reference inventory

Inspect `src/vg2c/emitter/project.py`: `_Expressions.visit_Call` lines ~26–45 transforms `ctx.macro.named` into `substitute(...)`; `control_header` ~48–62 constructs macro and loop expressions; `_lower_lines` ~102–155 handles `macro.set_named` as dictionary assignment; `emit_project` ~179–280 allocates numbered overlays and macro CSV scopes. Inspect `src/vg2c/runtime/values.py`: `snapshot_values`, `substitute`, `read_macro_row`; `src/vg2c/runtime/controls.py`: `for_values`, `site_values`, `csv_chunks`; `src/vg2c/utilities/macro_state.py`: `MacroState` stack, `named`, `set_named`, `scope`, `substitute`; `src/vg2c/operands/{macro,conditional,loop}.py`, `src/vg2c/resolver/{macro_resolver,scope_builder}.py`, `src/vg2c/utilities/_emit_helpers.py` and metadata models. Inspect current generated `ICMPCS/main.py` nested scopes. Consult source semantics in `scripthost-utilities-decompiled/SPSQL3_py/SPFLib/SPFSQL3.py` and reference comments already attached to `runtime/values.py` **without importing ScriptHost at runtime**.

Tests to inspect before edits: `tests/runtime/{test_macro_state,test_control_parity,test_direct_runtime,test_placeholder_substitution}.py`, `tests/emitter/{test_generated_project,test_macro_emit_scope,test_symbol_resolution,test_semantic_contracts}.py`. Existing `MacroState` has compiler/test callers; do not delete on the assumption that it is unreferenced.

## Semantics inventory (write a table before edits)

Capture direct and nested reads, writes and overwrites; case-insensitive name normalization; nested lexical overlay restoration; zero-row CSV body suppression; first CSV data row only, repeated header behavior and bracket normalization; static START-MACRO; in-scope writes invisible after pop; snapshot global values and AED/bootstrap defaults; site/for loop overlay freshness; positional `<<>>` current rejection in direct runtime; errors on missing ordinary `<<<NAME>>>` versus preservation of reserved SPF tokens; blank, None, environment `%...%`, newline preservation and malformed token errors. Critically, `utilities.MacroState.named` returns blank for unknown, while `runtime.values.substitute` raises for unknown ordinary macros and preserves special reserved tokens, and older substitution strips leading newline. Do **not** assume parity. Decide the public `__getitem__` missing-name contract explicitly and separately from strict `.substitute` contract; avoid incidental behavior changes.

## Preferred design

One `MacroStore` instance per `JobRuntime`/run, **not** a process singleton. Internal stack of normalized frames (candidate reuse: refactor existing MacroState only if runtime imports remain compiler-free; otherwise move its tested stack implementation into a small `runtime/macros.py` and migrate consumers). Expose `macros["SITE"]`, `macros["SITE"] = "KM"`, a documented `macros.get` or `named` as justified, `.scope(mapping)` with `try/finally` restoration, and `.substitute(text)` delegating to the canonical token resolver in `runtime/values.py`. Store macro values in exactly one state object; separate immutable/fresh `job_values` snapshot is not a second macro store. The resolver must implement namespace choice for SPF/CL/env tokens, which ordinary `macros[...]` must not erase. No metaclass/registry or magical globals.

For CSV START-MACRO, prefer explicit faithful Python:

```python
if (row := job.read_macro_row("config.csv")) is not None:
    with macros.scope(row):
        job.write_file("out.txt", "<<<LABEL>>>")
```

Before Session 02, keep direct runtime calls where needed; this snippet describes future output. **Do not** use `with macros.scope_csv(...)` alone to model an absent row: a context manager cannot skip its body. For static scopes `with macros.scope():` is sufficient; loops can use `with macros.scope(loop_values):` inside native `for` without names like `macro_values_39`. Keep named `scope` variable only when required by nested Python syntax/debugging, never generate numbered full dictionaries. For readable expression `if macros["CONFIG"] == "0":` / `macros["CONFIG"] = str(...)`, but use explicit runtime conversion/comparison helpers when source numeric coercion differs; avoid overemitting casts or arbitrary eager token substitution.

## Ordered implementation

1. Verify HEAD, remote ancestry, clean working tree, baseline `pytest`; prepare an isolated representative test fixture. No reset/overwrite if dirty.
2. Build a **parity table** using existing runtime value rules and old MacroState, plus original ScriptHost references where verifiable. Choose `MacroStore` missing/blank/strict substitution contracts and note exceptions.
3. Implement one focused scoped store in runtime, avoiding imports from `vg2c.emitter`, `utility_metadata`, frontend or ScriptHost. Refactor `runtime/values.substitute` into a single resolver usable by both direct runtime and MacroStore; no duplicate regex policy.
4. Add `.scope(row=None)` safe restoration across exceptions, nested loops and early exits. Keep distinct fresh per-run values snapshot and explicit environment behavior.
5. Update `_Expressions`, `control_header`, `walk`, `_lower_lines` in `emitter/project.py` to emit dictionary-like reads/writes and scoped frames; keep function/parameter `RenderedCall` tracking and source offsets correct. Preserve first-row `if row is not None` skip behavior. Ensure plain emitted Python still imports no compiler packages.
6. Update only necessary runtime exports and compiler tests/metadata that reference old spellings. If old MacroState remains for concrete compiler callers, avoid a duplicate runtime implementation; document the owner and schedule proven cleanup for Session 05.
7. Compile emitted source, run macro/control/golden tests and full baseline suite; compare before/after output CSV, files and exceptions, not merely strings. Add a test for generated macro mutation *not* leaking beyond a nested scope and for two independent `run()` calls.
8. Collect a before/after readable excerpt from `ICMPCS/main.py` or a sanitized representative fixture. Document anything that still requires redundant macro syntax in Session 02 handoff.

## Tests and failure cases

Missing `<<<ordinary>>>` must still fail with a precise error, but documented reserved tokens continue unchanged; `None` expands to `""`; `CL_` and `%NAME%` use the correct snapshot; static/dynamic names match source semantics; nested `with` frames unwind on exceptions; loop values are fresh every iteration; `read_macro_row` returns `None` for header-only CSV and does not execute the child block; invalid/unclosed placeholder and positional macro are not silently accepted; uppercase/lowercase macro names compare properly; source location diagnostics and generated invocation ranges remain valid. Test embedded Python referencing real, supported runtime operations, but don't revive retired `ctx` global.

## Acceptance criteria

- Generated control flow remains native Python and executable without original VG2.
- No `macro_values_<scope-id>` copies are emitted for supported scoped programs, and `macros["KEY"]` is the default readable syntax for simple reads/writes.
- One macro-state authority per run; no global mutable registry.
- Existing macro-related behavioral tests and complete suite pass, plus new parity edge cases; no silent regressions.
- Existing external runtime function signatures need not be rewritten yet (reserved for Session 02).
- Emitter source offsets and backend independence preserved.

## Out of scope / handoff

No sweeping pandas, HTML/CSS, SQL or broad call-signature changes. Don't implement positional macros or new ScriptHost utilities solely for aesthetics. Commit only after tests and authorized implementation workflow; send next agent: exact commit SHA/parent, status, test report, changed files, final macro contract (including missing-name and scope semantics), sample generated output, outstanding limitations. Session 02 must consume this contract unchanged unless it demonstrates a bug with a regression test.