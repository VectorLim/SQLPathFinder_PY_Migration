# Session 05 — final convergence, deletion, regression and Linux deployment audit

**Status:** Implementation instructions, not yet executed. **Parent:** tested, pushed commit from Session 04; **own branch:** `refactor/generated-code/s05-integration`. This is the release/maintenance gate, not a redesign of Sessions 01–04. Implementation is performed only when this prompt is launched as its own session.

## Objective

Integrate tested changes to scoped macros, one per-job runtime, lean CSV/table operations and external SQL/HTML/CSS. Remove proven dead and duplicated code, check editor invocation offsets without changing UI, validate all representative generated projects and Linux runtime, and ensure final `main.py` is measurably simpler **without** moving complexity into unjustified wrappers. Do not leave obsolete implementations as vague future cleanup.

## Inspect all changed/current surfaces

- Compiler/emitter: `src/vg2c/{compilation,editing,workflow,project_paths}.py`, `src/vg2c/emitter/{project,models,globals,walker}.py`, `src/vg2c/operands/{macro,conditional,loop}.py`, `src/vg2c/utilities/{_base,_emit_helpers,macro_state,pipeline_context,sqlite_engine,html_report}.py`, `src/vg2c/utility_metadata.py` and exports.
- Runtime: `src/vg2c/runtime/{__init__,job,values,macros,query,sql_text,sqlite_reader,crosstab,csv_io,controls,html,html_format,files,append,mail,aed,oracle_client}.py` (new modules are provisional—inspect actual Session 04 source).
- Generated: `ICMPCS/main.py`, its `sql/` and `html/` assets, new CSS assets, and synthetic representative projects. Treat checked-in `ICMPCS` as review/example unless generated artifacts are explicitly intended for update; never overwrite its environment-specific data/output unasked.
- Tests: all `tests/runtime`, `tests/emitter`, `tests/frontend`, `tests/resolver`, `tests/dispatch`, `tests/cli` and existing non-UI backend tests; UI code is out of scope, but any backend API dependency tests remain a regression guard. README, package dependencies, CI, runtime import boundary, distribution/wheel manifest and Linux probe.

## Release gate A — repository and handoff integrity

1. `git fetch --all --prune`, verify remote/local `html-and-sql-rework` ancestry and expected Session 04 commit, no uncommitted unrelated changes. Inspect all previous commit diffs and run the full test suite **before** adding cleanup commits.
2. Review signed-off design decisions from Sessions 01–04. Make a dependency diagram: compiler → emitted Python → compiler-free runtime → external SQL/HTML/CSS → output; ensure there is no runtime back-edge into compiler metadata/UI.
3. Inventory all emitted public method names and their actual consumers with `rg`/AST search; validate supported external consumers *before* removing old entrypoints.

## Release gate B — systematic dead/duplicate code audit

For each candidate old symbol/module, create a short **keep/remove table** with static call sites, dynamic registration or public use, test references, replacement, and reason. Remove only proven dead code; update import paths and tests to authoritative locations without compatibility shims. Candidate areas:

- `utilities/macro_state.MacroState` vs the Session 01 runtime store: remove duplicate behavioral engine once all true consumers are migrated. Do not delete older compile metadata registration until replaced and tested.
- `utilities/pipeline_context.PipelineContext` versus bound runtime execution and old emit helpers; establish if compiler `UtilitySpec.operation_definition("ctx", "run_query")` still requires metadata, and if so migrate metadata to new authoritative contract before deleting runtime logic.
- Utility-specific `emit_block` definitions versus direct-project logic in `emitter/project.py`: consolidate one real emission pathway, but **do not** erase special utility classifiers and authoritative metadata.
- `runtime/values.substitute` versus macro-store method: one actual substitution implementation with thin required adapters at most; remove duplicate token validation.
- `runtime/query.run_query` vs `JobRuntime.sql` vs legacy query execution: a single query semantic owner, no parallel copies of binding/site/CSV write logic.
- `runtime/html_format` vs `utilities/html_report` rendering rules: separate compiler parsing from runtime rendering, no duplicate formatter.
- Any unnecessary `__init__` aliases, facades, multi-layer `service` or `manager` objects, stale imports, unreachable compatibility functions, deprecated `ctx` calls and local legacy generated-file patterns.

Do not delete API helpers simply because their name is old; present external consumer evidence, no-regression test and explicit replacement or declare them supported. The objective is deletion of genuinely redundant code, not indiscriminate API breakage.

## Release gate C — end-to-end behavior

Use a **single deterministic offline harness** for old and new commits: synthetic/sanitized VG2 programs with macros, nested START-MACRO, IF/ELSE, FOR/SITE/RUN loops, SQLite multi-table joins, external stubbed readers, `/HEADERS`, crosstab and SQL token expansion, file copy/rename/write, and at least two HTML reports with different style states. Translate to separate temp directories, run with fixed environment/time where timestamps matter, re-run after editing SQL/HTML/CSS, and compare output/side effects/errors. Include these existing focused suites:

```bash
python -m pytest -q tests/runtime tests/emitter tests/cli tests/dispatch tests/resolver tests/frontend
python -m pytest -q
python -m compileall -q src
```

Also execute package's documented Ruff / type checks if configured. Collect coverage for failure paths, not just golden happy cases: missing macro vs reserved token, header-only CSV, missing file, bad SQL bind, duplicate headers, invalid crosstab keys, malformed HTML/CSS tokens, cross-job state leak, wrong root, CSS overwrite collision, and one independent nested exception path. Do not relax existing behavior assertions to greenwash regressions. Explain any *intentional* bug fix with original evidence and a new test.

## Release gate D — installed Linux runtime

Use Python >=3.11 compatible with `pyproject.toml`, build/install a wheel into a **clean Linux environment** matching the intended container runtime dependencies (`pandas`, `datasyncx`, any minimum runtime packages). Copy only the generated project tree and installed `vg2c` wheel, **not** the VG2 input, compiler checkout or ScriptHost. Verify:

- `python main.py` from the generated project directory and from a different CWD; `run(workdir=...)` redirects data output, not SQL/HTML/CSS source assets.
- Runtime import does not load compiler/frontend/editor modules (existing `test_runtime_import_excludes_compiler`).
- Relative paths, Linux case sensitivity, slash normalization, injected Windows `\\` path literals, directory permissions and Unicode filenames behave per documented contract.
- SQL/HTML/CSS assets remain independently editable and re-read at execution time; styling link survives output relocation; generated outputs don't overwrite source assets.
- No hidden package dependencies on repo-local fixture files, test-only external assets, notebook environment or uninstalled ScriptHost modules.
- External DataSyncX and AED production integration remain appropriately stubbed/marked as environment gated if no credentials or services are available; don't claim deployment certification without actually running a representative production environment.

If deployment is inaccessible, record blocked checks plainly and **do not claim complete Linux validation**; deliver local reproducible Linux commands and a verification list.

## Release gate E — maintainability and readability measurement

Record fresh metrics for the same `ICMPCS` generated job using the current compiler, not just the checked-in pre-refactor sample: physical LOC, AST node count, lines in execution body, numbers of context keywords and numbered macro snapshots, number of inline CSS statements / multiline HTML literals, external asset counts, call depth and remaining boilerplate. Baseline measured in planning: 749 physical lines, 48 literal `workdir=workdir`, 51 `values=job_values`, 57 uses of numbered macro names, 33 `substitute(...)` calls and nine style assignment sites. These counts are reference only: re-generate identical input before comparison.

Quality goals: the default user-facing code should show simple `job.sql(...)`, `job.html(...)`, `macros["NAME"]`, native `if`/`for`, and optional short arguments that genuinely affect business rules. Zero repeated state-parameter trio, zero copied numbered macro dictionaries and zero long static CSS-property declarations in generated main. Keep necessary SQL reader/site, crosstab row keys, outputs and true schema renames visible or directly editable in one adjacent asset. Provide 2–3 before/after excerpts explaining where complexity disappeared rather than where it was merely hidden.

Measure total runtime+compiler LOC and responsibilities too. Reject meaningless extra facade, service, registry, parameter DTO, event bus, or duplicate wrappers. Ensure type hints, docstrings, API examples, meaningful error messages, proper resource closure, source map offsets, dependency/package metadata and provenance for adapted ScriptHost algorithms. Validate `EmittedScript` metadata edits and `tests/emitter/test_editing.py` without implementing UI changes.

## Ordered completion procedure

1. Verify parent and baseline tests.
2. Perform call-site/reference audit and make candidate keep/remove matrix.
3. Consolidate and remove proven dead implementations. Make small commits and run related tests after each significant deletion.
4. Integrate cross-session behavior using goldens and representative local generated projects.
5. Review and tune generated Python readability **without changing behavior**; preserve editable SQL/HTML/CSS contract.
6. Run complete suite + installed Linux wheel probe + multi-job isolation and error cases. Publish exact commands/outcomes in handoff.
7. Update README and runtime API docs/examples, document known unsupported features and deployment constraints. No UI redesign.
8. Final review from scratch: ask if each abstraction owns real behavior, every parameter is required, every generated literal belongs there, and no test has been weakened.
9. Deliver diff summary, final SHA, branch name, dependency lock details, tests, artifact samples and any remaining objectively blocked environment-only checks. Do not merge `main` without separate approval.

## Acceptance and stop conditions

**Complete only when** tests and differential results pass, generated business code is appreciably simpler, macros and runtime ownership are singular and explicit, redundant paths are truly removed, all generated assets remain directly usable without VG2 or ScriptHost, and Linux deployment check is satisfied or clearly identified as an unverified gate. If material behavioral conflict arises, fix it in this session with a dedicated regression test, or report incomplete; do not declare success based on syntax or line count alone.

**Out of scope:** frontend UI feature work, brand-new ScriptHost command coverage, HPC/JMP/JSL expansion, live DataSyncX/AED deployment overhaul, root-branch merge. This session should not produce a new large architectural proposal: it converges already established contracts from 01–04.