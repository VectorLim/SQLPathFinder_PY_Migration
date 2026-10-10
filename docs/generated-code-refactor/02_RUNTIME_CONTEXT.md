# Session 02 — one runtime object and concise generated operations

**Status:** Plan only. **Parent:** tested final Session 01 SHA on `origin/refactor/generated-code/s01-macros`, verified against its handoff and final response. **Own branch:** create `refactor/generated-code/implementation` once at that SHA. Session 03 continues this same shared branch.

## Objective / scope

Remove pervasive `workdir=workdir`, `values=job_values`, `macros=...` from emitted business operations without hiding dependencies in global state. Clarify project asset root versus job output root, and preserve runtime state/reader ownership. Keep macro API from Session 01, native control flow, reader/site routing, diagnostics, metadata editing and direct importability.

## Current concrete paths to inspect

`src/vg2c/emitter/project.py`: main `run` preamble ~186–210, `control_header` and `_expression`, `_lower_lines` workdir injection ~102–155, `leaf` SQL runtime kwargs ~282–329, HTML call kwargs ~370–440, project header and emitted metadata ~440–484. `src/vg2c/runtime/__init__.py`: exposed operations. `runtime/{query,files,controls,values,html,append,mail,oracle_client,aed}.py`, `runtime/sqlite_reader.py`. `utilities/pipeline_context.py` contains **legacy** context semantics; do not automatically bring it back (`test_direct_runtime.py` asserts compiler-free runtime imports). `utilities/_runtime_helpers.py` has different fallback path rules. `src/vg2c/__init__.py`, `emitter/models.py`, `utility_metadata.py`, `project_paths.py`. Generated `ICMPCS/main.py` repeats the trio roughly 48/51/57 times as noted in the master plan.

Inspect tests `tests/runtime/{test_direct_runtime,test_control_parity,test_generated_symbols,test_fs_external,test_e2e_fixtures}.py` and `tests/emitter/{test_generated_project,test_emission_metadata,test_no_vg2c_leak,test_editing,test_utility_import_collection}.py`; inspect Linux runtime probe instructions in README and CI workflows. Existing explicit `run(workdir=...)` semantics are a release contract.

## Candidate evaluation and choice

| Option | Benefit | Cost / reason |
|---|---|---|
| Repeat explicit kwargs | Transparent | Clearly unacceptable emitted verbosity |
| Global/contextvars/current directory | Short calls | Hidden state, reentrancy/concurrency bugs; **reject** |
| Inject one kwargs dict or `functools.partial` per function | Low implementation effort | Boilerplate moves into setup, unclear debugging; reject as general solution |
| Reuse old compiler `PipelineContext` wholesale | Already exists | Depends on utility/compiler architecture and old macro semantics; test prohibits runtime importing compiler; reject blind reuse |
| **One light JobRuntime instance bound to explicit roots + macros** | Concise readable calls, clear ownership | Small meaningful delegation; **prefer** |

Define `JobRuntime(assets_root: Path, workdir: Path, *, values=None, environ=None, ...optional AED config...)` with immutable/fresh snapshot per run and MacroStore from 01. `job.macros`, `job.values`, and roots remain accessible for debugging. Operations read their own current assets and use the bound context to call existing `runtime` functions. Favor a small number of direct, well-named methods (`sql`, `html`, `write_file`, `copy_file`, `rename_file`, `delete_files`, `row_count`, `run_program`, `send_mail`, optional `read_macro_row`/`csv_chunks`) only where emitted code benefits; each method should add real binding/path normalization, not a chain of forwarding classes. Readers stay explicit where behavior differs: either `reader=SqliteReader()` or a transparent `reader="sqlite"` only if tested config/router can map it without ambiguity. **Never silently infer DataSyncX site/node**. Do not instantiate network clients until needed or change where errors surface without documenting it.

## Output target (illustration, exact signatures subject to parity)

```python
# Before
execute_sql(BASE_DIR / "sql/query_045_PARMI_IPM_RAW.sql", reader=SqliteReader(),
            output="PARMI_IPM_RAW.csv", workdir=workdir,
            values=job_values, macros=macro_values_75,
            inputs=["yeuchuan_a1_22697.tab", "yeuchuan_a0_22697.tab"])
# After
job.sql("sql/query_045_PARMI_IPM_RAW.sql", reader="sqlite",
        inputs=["yeuchuan_a1_22697.tab", "yeuchuan_a0_22697.tab"],
        output="PARMI_IPM_RAW.csv")
```

Constructor explicitly binds **asset root** (`main.py` directory, read-only generated `sql/html/styles`) and **work root** (`output/`, override permitted). Relative SQL/HTML asset references use `assets_root`, while CSV sources, execution outputs, copied CSS output and transient files use `workdir`. Never `os.chdir` or infer project root from runtime module `__file__`. Defer macro/path placeholders to operation time. Honor native absolute paths and distinguish Windows `\\server` and `C:\\...` conventions from Linux `Path` behavior using narrowly scoped normalization tests; don't rewrite valid SQL string literals or UNC semantics indiscriminately. Jobs must be runnable after relocation with sibling asset folders.

## Ordered implementation

1. Verify prior SHA, branch/working tree and full Session 01 tests; capture generated shape and call inventory by utility kind.
2. Write explicit state and path contract: construction, `job.values` global snapshot, `job.macros` current scope, per-run report/style state, reader factory policy, absolute/relative paths. Ensure AED `aed_config` remains scoped to one run and service factory remains explicit and optional (no network integration expansion).
3. Add a minimal compiler-free runtime module (e.g., `runtime/job.py`); bind existing direct operations without copying query/csv/html logic. Pick public names once and document signatures.
4. Refactor *only* runtime functions whose design requires it; avoid duplicate implementations. Retain existing named direct functions if real test/public call sites require them, with one underlying implementation. If no consumer needs an alias, remove it in Session 05.
5. Update emitter `run()` setup and direct-call lowering, SQL and HTML invocation creation, `read_macro_row`/loop calls and AED call path. Emit `job.*` calls with **no repetitive context trio**. Preserve `EmittableOperation` source-range/parameter metadata on bound methods; update editors/tests to appropriate semantically equivalent call shape.
6. Keep `job.sql` explicitly distinguishable from file writing and `job.html` from report registration. Reports/styles should be per-JobRuntime or small per-run locals until Session 04 externalization.
7. Add isolated two-job interleaving tests, differing globals/macro values/work roots, repeated `run()` test, movable generated project test, dynamic asset reread test, missing asset or invalid route errors, installed-wheel import isolation.
8. Run focused and all tests; record baseline vs current main.py metrics, and inspect for hidden state/service wrappers. Hand off exact commit/contract to 03.

## Negative cases and gates

- Changing CWD must not change job outputs; files/reader inputs resolved according to roots.
- SQL bind vs macro interpolation must remain distinguishable; no untrusted string automatically inserted into SQL outside existing behavior.
- `SQL_Get_CSV_List` re-reads workroot CSV with existing scanner semantics.
- `run(workdir=override)` should work when assets remain in original relocated project.
- Independent jobs must not share macros, values, styles, reports or reader configuration.
- DataSyncX reader/node/site behavior and Oracle configuration remain correct; don't make external reader automatic when `node` is required.
- No import of emitter/compiler/frontend from `vg2c.runtime` (current direct-runtime test requires this).
- Source offsets and editing parameter identities refer to actually emitted `job.*` calls.

## Acceptance criteria

One per-run explicit context; operations in generated main contain no repeated `workdir=workdir`, `values=job_values`, `macros=macro_values_...` trio; no hidden globals or untracked CWD mutations; generated business statements are directly editable Python; runtime still imports on deployed Linux without compiler; full regression suite passes. Ensure the refactor reduces **both** user-facing noise and duplicate runtime logic. Nothing in this session invents table/report semantics.

## Out of scope / handoff

No broad pandas implementation, CSS asset rewrite, parser/UI enhancements, HPC/JMP or new ScriptHost task coverage. Provide exact new API signatures and provenance, successful/failed tests, baseline vs current metrics, commit SHA and any path ambiguities for Session 03. If a path or reader simplification cannot be made correctly, keep explicit parameters and document it rather than hiding dependencies.