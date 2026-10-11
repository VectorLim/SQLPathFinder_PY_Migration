# Session 05 — final integration, legacy retirement and Linux deployment handoff

**Repository:** `VectorLim/SQLPathFinder_PY_Migration`  
**Only changed branch:** `refactor/generated-code/implementation`  
**Exact required Session 04 parent / verified initial remote HEAD:** `a6ec051708d972ddb5f3831c6e1355a5fce14a68`  
**Verified tested implementation checkpoint:** `c1e9c2e5991d441d8cdfde09e03c128480835e4f`  
**README updated checkpoint:** `e7ec964cdfbbd0f2ec0486fbb48f16c3880aa39e`  
**Final pushed SHA:** report in the agent's final response, not in this self-referential committed file.

**Disposition:** **Supported offline native-generated-project backend: no unexpected regressions in the executed suite; installed-wheel Linux synthetic probe passed.** Full repository suite remains **RED** with the **six inherited UI/JMP tests** listed below. Original proprietary ScriptHost execution parity, actual deployment-container DataSyncX/AED services, and a rigorously fixed-input pre/post-cleanup output differential are **NOT verified**. Do not call this an unconditional production release certification.

## 1. Scope refinement and repository integrity

The user explicitly authorized deleting legacy `MacroState`, `PipelineContext`, stateful `HtmlReport` rendering, and non-core UI/embedding paths, while keeping whatever directly supports the core translator. This later instruction supersedes the earlier requirement to preserve an unused embedded-runtime engine and its historical tests.

At entry, the connected GitHub API showed the exact SHA `a6ec051708d972ddb5f3831c6e1355a5fce14a68`. The remote tree and committed plans/handoffs were inspected. The local container could not resolve github.com, so `git fetch`, local worktree dirtiness, and a local clone were **not** verified. Writes were made through GitHub blob/tree/commit APIs with the exact expected previous SHA, normal one-parent commits and non-force expected-SHA ref updates; a few subsequent single-file edits used the existing-blob-SHA content API on the same branch. No merge, rebase, force push, reset, or main-branch edit occurred. GitHub Actions checked out exact commits in clean Linux runners.

## 2. Keep/remove ownership inventory

| Decision | Code | Consumer/evidence and resulting owner |
| --- | --- | --- |
| **Remove** | `utilities/macro_state.py` (`MacroState` frame-stack engine) | Direct generated code already uses `runtime.macros.MacroStore`. `MacroControl` in `utilities/macro_control.py` retains only VG2 control classification and `@emittable` compiler metadata used by source/semantic editors and utility emission. No second mutable macro state remains. |
| **Remove** | `utilities/pipeline_context.py` (`PipelineContext` legacy query/files engine) | All actual generated SQL and file operations already execute via instance-scoped `runtime.job.JobRuntime` and canonical runtime functions. `utilities/direct_operations.py` retains metadata definitions for historical compiler operation IDs `ctx.run_query` / `ctx.write_file`, including file effects and editable parameter IDs, but contains **no query/file implementation**. Preserving their normal semantic visibility is important for dataflow/source projection. |
| **Remove rendering** | Stateful formatting/report generation from `utilities/html_report.py` | `runtime/html.py` remains the sole generated-project HTML renderer. The `HtmlReport` **class name remains, but is compiler-only**: `check`, `_iter_rows`, `_parse_options`, `_split_layout`, scaffold, and method metadata directly support classification, source-derived HTML assembly and editing/source offset projection. Its methods intentionally cannot execute the obsolete renderer. |
| **Remove** | `vg2c/embedding/{__init__,index,renderer,resolver,topology}.py`, old `UtilitySpec.dispatch_and_emit` / wrapper machinery | The authoritative emitter is `emitter/project.py`; actual emitted projects import `vg2c.runtime` rather than self-embedding classes. Obsolete tests exclusively asserting source-embedding internals were retired rather than asserting unsupported behavior. |
| **Remove** | Old `StartMacro.render_header` / `emit_scope` wrapper | Native control headers are emitted by `emitter.project.control_header`; the `StartMacro` data object continues to parse and build lexical scope nodes. |
| **Keep** | `runtime/values.py` substitution + `runtime/macros.py` `MacroStore` | Single authoritative token substitution policy with a scoped storage object. MacroStore delegates resolution to `values.substitute`; the old differing missing-value and leading-newline semantics were not reintroduced. |
| **Keep** | `runtime/{job,query,sqlite_reader,crosstab,csv_io,sql_text}.py` | Actual installed-runtime SQL/binds/CSV joins/crosstab/CTARRAY owner; compiler utility wrappers produce metadata/options, not rival algorithms. |
| **Keep** | `runtime/{html,html_format,files,controls}.py` | One renderer and explicit per-run file/control operations, with asset roots distinct from output roots. |
| **Keep** | Compiler editing/dataflow metadata, externalized SQL/HTML/CSS, `EmittedScript.assets`, `StepEmission`, `EmittedParameter` | Required for supported translation and backend editing. No frontend feature rewrite; UI-only preview remains unsupported following legacy renderer retirement. |

`utility_name="ctx"` and emitted-operation IDs such as `ctx.run_query` are **compiler metadata identifiers only**. Generated `main.py` does **not** instantiate `ctx`, `PipelineContext`, `MacroState`, or `HtmlReport`.

The scoped file count comparison against the starting SHA showed **632 added / 3,699 deleted diff lines** before this handoff (including tests/docs). In particular the unused embedding package alone removed approximately **1,433 physical source lines**, and `utilities/html_report.py` lost 306 old lines while retaining compiler-visible parsing metadata. Diff totals include rewritten/retired tests and therefore are **not** comparable with test-count changes as a coverage score.

## 3. Exact changed-file summary

- **Removed runtime engines / embeddings:** `src/vg2c/utilities/{macro_state,pipeline_context}.py`, entire `src/vg2c/embedding/`.
- **New compiler-only specs:** `src/vg2c/utilities/{macro_control,direct_operations}.py`.
- **Existing compiler updated:** `utilities/{__init__,_base,_emit_helpers,html_report,fs_ops,sqlite_engine,rows_in_file}.py`, `operands/{base,macro}.py`, `semantics.py`. Kept accurate operation IDs, layout parsing, source ranges and native control emission.
- **Runtime:** No behavior change to `src/vg2c/runtime/{job,query,crosstab,html,macros}.py` or public `JobRuntime` API was needed.
- **New standalone test:** `tests/deployment/verify_linux_wheel.py`; **new CI:** `.github/workflows/session05-installed-linux.yml`; **docs:** `README.md`, this handoff.
- **Retired obsolete tests:** `tests/runtime/test_macro_state.py`, `tests/emitter/{test_symbol_resolution,test_topo_sort}.py`. `tests/runtime/test_html_report.py`, `test_write_file_and_readers.py`, and `tests/emitter/{test_utility_filtering,test_utility_import_collection,test_macro_emit_scope,test_python_embed}.py` now assert modern compiler/runtime behavior rather than removed legacy execution. The two valid `CrossTab` SQL substitution tests formerly in `test_macro_state.py` were retained in `tests/runtime/test_crosstab_sql_rewrites.py`. A legacy embedded-self-contained-runtime test was removed from `test_generated_symbols.py`; actual wheel-only runtime execution is now checked in CI instead.
- **Backend expectations retained:** dataflow/source-effect tests and emitter metadata tests now reference `DirectOperationMetadata` but still assert the same operation identity, schema and file effects. Source-only HTML layout extraction was restored because core workflow projection uses it.

The reduction in total pytest cases is deliberate: dozens of cases targeted retired embedding internals, old `MacroState` methods, `PipelineContext` execution, and legacy HTML preview behavior. It is **not** evidence of improved coverage. The maintained runtime/emitter and standalone generated-project tests are the supported correctness gates.

## 4. Supported public contract

```python
from pathlib import Path
from vg2c.runtime import JobRuntime, SqliteReader, csv_report

BASE_DIR = Path(__file__).resolve().parent

def run(workdir=BASE_DIR / "output"):
    job = JobRuntime(BASE_DIR, workdir)
    macros = job.macros
    # Native Python if/for/with macros.scope() describe the control flow.
    job.sql("sql/query_000_out.sql", reader=SqliteReader(),
            output="out.csv", inputs=[])
    job.reports["R"] = csv_report("out.csv")
    job.html("html/report_002.html", output="report.html")
```

Every `run()` constructs new `JobRuntime` / `MacroStore`. `BASE_DIR` locates editable asset files; `workdir` resolves transient/output data. Source SQL/HTML/CSS is reread at execution; report column choice/order/labels/alignment live in HTML markup, not emitted report JSON or Python snapshots. Existing `EmittedScript` steps/parameter spans still correspond to actual `main.py` source. The runtime import test rejects unexpected compiler/frontend imports.

**Representative generated-code shape, rather than invented emitted snippets:** `job.sql('sql/query_000_out.sql', reader=SqliteReader(), output='out.csv', inputs=[], pivot_columns='metric', pivot_values='value')` was logged by the previous and current CI benchmark. Existing tests assert `with macros.scope(row)`, direct `macros['COUNT']`, `job.write_file(...)`, and linked/embedded HTML generation from separately editable assets.

## 5. Source-grounded semantic boundaries

- Original Python ScriptHost `scripthost-utilities-decompiled/SPSQL3_py/` remains the **source-observed semantic authority**, but no original executable differential comparison ran here.
- Preserve crosstab inferred rows, ScriptHost-entry 50,000-row chunk behavior, positional FIRST/LAST and cross-chunk `combine_first`, CTARRAY and downstream `CrossTab->[[...]]`. The canonical runtime does not preserve prior vg2c's incorrect first-non-null and blank-filter pivots. Normalizing pivot headers to uppercase and rejecting collisions is a documented **intentional repair**.
- HTML is HTML-first: literal declarative dollar values (including mixed-case `DATA-REPORT`, `DATA-FIELD`, `DATA-PATTERN`) must remain literal; explicit `${NAME}` slots, guarded old named slots, HTML escaping and valid structural slots remain supported. Do not bring back `.report.json` emission.
- Missing ordinary macros are errors; known reserved/global tokens follow current `runtime.values.substitute` semantics. Native Python conditionals, first-row CSV macro scope guards, exception restoration and per-run job isolation remain authoritative.
- Advanced original `COLUMN-FORMAT`, HTMI/HTMLI5/HTMIC, HPC, JMP/JSL, production AED/Oracle, broad distributed report/email flows and true original-engine byte-differential parity remain unsupported/unverified.

## 6. CI, reproducibility and execution evidence

**Executed baseline (Session 04 SHA):** CI run [38118228943](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38118228943): 172 focused, 481 runtime/emitter, 659 pass / 6 fail full repository, compilation and scoped Ruff pass. Those six failures existed before Session 05.

**Executed cleanup checkpoint `c1e9c2e5991d441d8cdfde09e03c128480835e4f`:** [test workflow 38126561640](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38126561640) on clean Ubuntu/Python 3.12 / pandas 3.0.3:
- `python -m compileall -q src` — PASS.
- `ruff check --select F821,F823 src/vg2c/runtime src/vg2c/emitter src/vg2c/utilities` — PASS.
- `python -m pytest -q tests/runtime/test_html_assets_session04.py tests/runtime/test_direct_html.py tests/runtime/test_table_semantics_session03.py tests/runtime/test_crosstab_script_host_parity.py tests/runtime/test_crosstab_corrective_pass.py tests/runtime/test_csv_io.py tests/runtime/test_direct_runtime.py tests/runtime/test_e2e_fixtures.py tests/runtime/test_job_runtime.py tests/emitter/test_generated_project.py tests/emitter/test_sqlite_table_bindings.py` — **172 passed**.
- `python -m pytest -q tests/runtime tests/emitter` — **399 passed**.
- Synthetic SQLite join and pandas pivot 10k/75k rows — PASS.
- Fresh ICMPCS compile-only readability metrics and asset count — PASS.
- `python -m pytest -q` — **577 passed, exactly six inherited UI/JMP failed**. Do not call this green.
- No compiler/runtime tests newly fail at that checkpoint.

**Executed standalone installed-wheel probe:** [38126561642](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38126561642) — **PASS**. This is independent of the checkout used only to **compile the test input**. The target subprocess runs `-I` using the newly installed wheel in its own venv, with no `PYTHONPATH`, from a relocated Unicode-named generated directory and another unrelated CWD, after deleting the VG2 input and its compiler-stage generated directory. Checks include:
- wheel module manifest for runtime modules;
- `vg2c.runtime` loaded from venv `site-packages` without compiler/emitter/frontend imports;
- `python main.py`, `run(workdir=...)`, changed CWD;
- SQLite SQL -> pivot -> CTARRAY -> downstream SQL -> two HTML layouts;
- linked CSS, embedded CSS, source file SQL/HTML/CSS edits between runs;
- Unicode workdir and CSV, source file read-only permission, no source asset overwrite;
- negative missing SQL asset fails with `FileNotFoundError`.
The exact script is versioned as `tests/deployment/verify_linux_wheel.py`. The workflow is `.github/workflows/session05-installed-linux.yml`.

**How to reproduce the wheel check (Linux):**
```bash
python -m pip install build wheel setuptools pandas==3.0.3 keyring==25.7.0
python -m build --wheel --no-isolation --outdir /tmp/vg2c-dist
python -m venv /tmp/vg2c-venv
/tmp/vg2c-venv/bin/python -m pip install pandas==3.0.3
/tmp/vg2c-venv/bin/python -m pip install --no-deps /tmp/vg2c-dist/*.whl
python tests/deployment/verify_linux_wheel.py \
  --python /tmp/vg2c-venv/bin/python --wheel /tmp/vg2c-dist/*.whl
```

This is intentionally an **offline-supported SQLite subset**: the wheel's full declared `datasyncx`, `uv`, `keyring` dependencies are not all installed into the runtime venv. Do **not** claim full dependency resolution, proprietary DataSyncX/AED services, target-container deployment or production reader certification from this probe. A full target container must install its pinned dependencies from authorized internal sources and validate its actual connectors.

## 7. Same-example readability scorecard

Fresh `ICMPCS.txt` compiler output measured with the same metrics used for the checked-in historical `ICMPCS/main.py` (CI job; no production resource execution):

| Metric | Historical checked-in main | Current fresh compile |
| --- | ---: | ---: |
| Physical `splitlines()` | 748 | **137** |
| Simple AST statements | 75 | **59** |
| Longest simple statement (chars) | 3,028 | **748** |
| Inline style assignment lines | 9 | **0** |
| `job.html(` calls | 0 | **2** |
| External SQL assets (new emission) | — | **8** |
| External HTML pages (new emission) | — | **2** |
| External CSS assets (new emission) | — | **1** |
| New report JSON sidecars | — | **0** |

Current generated code contains no numbered macro snapshot dictionaries or repeated `workdir=workdir, values=job_values, macros=...` trio. The physical line reductions originated in Sessions 01–04; Session 05 primarily eliminated non-authoritative implementations. The longest statement and test reductions do **not** establish source-level equivalence; mandatory business configuration remains visible in the runtime invocation or adjacent editable assets.

## 8. Remaining full-suite failures (not hidden, skipped or changed)

1. `tests/ui/test_document_store.py::test_shared_global_edits_persist_across_steps`
2. `tests/ui/test_document_store.py::test_reorder_persists_execution_order_and_generation_state`
3. `tests/ui/test_document_store.py::test_html_preview_is_safe_exact_approximate_and_path_bounded` — old HTML preview no longer has a legacy `HtmlReport.render_layout`; this UI endpoint needs separately authorized redesign using the authoritative runtime or removal.
4. `tests/ui/test_html_preview.py::test_html_preview_replays_safely_and_does_not_write_outputs` (JMP/JSL unsupported)
5. `tests/ui/test_workspace_sessions.py::test_sql_column_choices_read_uploaded_server_csv_headers`
6. `tests/ui/test_workspace_sessions.py::test_file_backed_sql_filter_uses_workspace_choices_through_save_and_generate`

The same six test IDs failed at the unchanged Session 04 parent. Their detailed exception text may differ after deleting the old HTML preview implementation, which is an **intentional unsupported UI behavior change**, not proof that the UI is working. They were **not** skipped, muted or deleted.

## 9. Blocked release gates and final decision

- **No local repo checkout** in the agent container: GitHub DNS resolution failed; GitHub Actions provided the clean Linux execution route.
- **No actual original ScriptHost executable differential**, no private production data/resources, no external Oracle/DataSyncX/AED service integration, no full target-container dependency certification, no production mail credentials.
- **No complete fixed-input before/after cleanup output diff** against the Session 04 wheel with all macro/SQL/report/error combinations. Existing focused functional tests, exact-SHA Linux wheel execution and ICMPCS AST/readability comparisons are supportive but do not replace that differential.
- **No full-tree Ruff pass claimed**: CI runs the scoped F821/F823 check only.
- **UI release not certified** and the entire suite remains six failures red. Remove or rewrite old HTML preview only in a future explicitly authorized UI scope if desired.

**Assessment:** The supported native compiler -> generated-project -> installed `vg2c.runtime` offline execution path is ready for core review and controlled testing, with no observed new backend test failure in the cited run. Do **not** present Session 05 as unconditional production-certified or as full ScriptHost parity. No merge to `main` or `html-and-sql-rework` was performed or authorized.

**Final exact commit and CI:** The handoff commit cannot contain its own SHA. The final agent report must verify the remote branch HEAD against its pushed SHA and cite the final exact-commit test and Linux workflow results.
