# Session 02 handoff — per-run runtime and concise generated calls

**Status: WIP — all Session 02 focused tests passed; full suite retains six inherited UI failures.**
**Repository:** `VectorLim/SQLPathFinder_PY_Migration`
**Branch:** `refactor/generated-code/implementation` (shared sequential Sessions 02–05)
**Exact Session 01 starting parent:** `4b4e0be7389fb38e866b481e14a8f757d639e65e`
**Revised-plan synchronization commit:** `b1eac4ce6e40882608692f8e4a5c905192ca5044`
**Verified implementation/checkpoint SHA before this handoff:** `07d21b4fce683bfa7a0fbff0ff4943b215096469`
**Final pushed SHA:** Reported in the session's final response; the tracked handoff cannot self-reference its own SHA.

## 1. Starting state, docs and ownership

- Verified GitHub remote Session 01 HEAD matches the preceding agent's exact final SHA. Read `SESSION_01_HANDOFF.md`, latest master, `02_RUNTIME_CONTEXT.md`, and updated prompt/README from `html-and-sql-rework`.
- Created the implementation branch **once**, directly at that verified Session 01 SHA. Compared and imported only the revised master, Sessions 02–05 planning Markdown, README and Sessions 02–05 prompt Markdown. No branch merge/rebase and no changes to Session 01's implementation or handoff.
- Network DNS prevented a local `git fetch`/checkout/working-tree inspection. GitHub's API verified remote ref and ancestry; GitHub Actions checked out the actual pushed SHA into a clean Linux runner. No claim of local dirty-state verification is made.

## 2. Implementation and changed code

- New `src/vg2c/runtime/job.py` (108 lines) implements **one** compiler-independent `JobRuntime` per invocation with distinct project asset root, output/work root, fresh global/environment snapshot, Session 01 `MacroStore`, report definitions, styles, and CSS filename. Exported via `src/vg2c/runtime/__init__.py`.
- `src/vg2c/emitter/project.py` initializes `job = JobRuntime(BASE_DIR, workdir)` (passing original AED snapshot/initial macros when applicable), keeps `macros = job.macros` where used, and emits `job.sql`, `job.html`, `job.write_file`, `job.row_count`, `job.read_macro_row`, `job.csv_chunks`, `job.copy_file`, `job.rename_file`, `job.delete_files`, `job.wait_file`, `job.run_program`, `job.smart_append`, `job.send_mail`, and `job.process_candidates` instead of repeated root/value/macro context kwargs.
- `JobRuntime` delegates to existing SQL, HTML, CSV, filesystem, mail, and AED implementations; no query/pivot/HTML semantics were duplicated. Backend SQL reader objects, node/site, binds, table inputs, headers and crosstab specifications stay explicit in generated Python.
- Relative SQL/HTML source assets are anchored to `assets_root` with project-contained relative asset guards. Runtime CSV inputs, outputs, and transient paths remain anchored to `workdir`; source files are re-read per invocation. Absolute native asset paths remain supported; Windows absolute/UNC asset references on Linux raise an explicit error rather than being treated as relative POSIX paths.
- Macro scope, native `if`/`else`/`for`, `EmittedScript.assets`, `StepEmission`, editable argument metadata and invocation offsets remain as in the existing compiler. HTML report/style state now belongs to the per-run job. Existing `bootstrap_aed` still precedes job construction and remains explicit; candidate processing is delegated through the job, without AED feature expansion.
- Added `tests/runtime/test_job_runtime.py` with independent concurrent contexts/output roots/global snapshots/macros, source asset edit/reread, changed CWD, first-row macro CSV handling, path guards, readable AST, executable generated workflow with `run(workdir=override)`, and source/invocation offset checks. Updated ten tests whose *source spellings* referred to retired function calls, keeping their observable behavior and editing assertions. The AED test still uses an explicit offline stub through the new runtime module.
- Did not modify frontend implementation, data/pandas/crosstab contracts, SQL headers, CSS externalization, HPC/JMP/JSL, or ScriptHost/AED infrastructure.

## 3. Public runtime and emitter contract

```python
from vg2c.runtime import JobRuntime, SqliteReader

job = JobRuntime(assets_root=BASE_DIR, workdir=workdir,
                 values=None, environ=None, initial_macros=None)
macros = job.macros
job.sql("sql/query.sql", reader=SqliteReader(), output="result.csv",
        inputs=["input.csv"], node=None, params=None, header=None, crosstab=None)
job.html("html/report.html", output="result.html", values=None,
         instance=None, css_file=None, embed_css=False)
job.write_file("out.txt", "<<<LABEL>>>", vars=None)
job.read_macro_row("config.csv")
job.csv_chunks("input.csv", "chunk.csv", 500)
job.row_count("result.csv")
```

Additional direct methods: `copy_file(src,dst,recurse=False)`, `rename_file(src,dst)`, `delete_files(paths,recurse=False)`, `wait_file(path,timeout=30,interval=5)`, `run_program(argv,cwd=None,env=None,check=False,exedir=None)`, `smart_append(destination,source)`, `send_mail(to,subject,body,attachments=None,from_addr=None,enabled=True)`, `process_candidates(path,config=...,service_factory=...,logger=None)`.

`job.values`, `job.macros`, `job.assets_root`, `job.workdir`, `job.reports`, `job.styles`, and `job.css_file` remain inspectable. Each new job gets independent state. Do not convert this back to ambient globals/contextvars or drop explicit SQL routing. Existing direct runtime functions remain for real users and tests.

### Representative emitted Python

Before (Session 01): `execute_sql(BASE_DIR / 'sql/query_000_result.sql', reader=SqliteReader(), output='result.csv', workdir=workdir, values=job_values, macros=macros, inputs=[])`

After: `job.sql('sql/query_000_result.sql', reader=SqliteReader(), output='result.csv', inputs=[])`

Before: `write_file(path='chosen.txt', template='<<<LABEL>>>', workdir=workdir, values=job_values, macros=macros)`

After: `job.write_file(path='chosen.txt', template='<<<LABEL>>>')`

Real sanitized nine-block fixture after Session 02, measured by checked-out GitHub Actions:
```python
def run(workdir=WORK_DIR):
    workdir = Path(workdir).resolve()
    job = JobRuntime(BASE_DIR, workdir)
    macros = job.macros
    macro_row_7 = job.read_macro_row('config.csv')
    if macro_row_7 is not None:
        with macros.scope(macro_row_7):
            macros['COUNT'] = str(job.row_count('rows.csv'))
            if int(macros['COUNT']) > int('0'):
                job.write_file(path='chosen.txt', template='<<<LABEL>>>')
```
(Other generated blocks omitted from the excerpt; the full generated text was printed in CI run 38059358746.)

**Metrics for identical sanitized nine-block fixture:** Session 01 handoff baseline **26 physical lines**; Session 02 **26 physical lines**, 0 `workdir=workdir`, 0 `values=job_values`, 0 numbered macro-map identifiers, 5 `job.*` calls, 2 bracket reads. This session improves expression readability, **not physical LOC** on this small fixture. The new runtime module adds 108 lines of delegated per-run context; `emitter/project.py` removes more code than it adds (about 17 net lines). The committed `ICMPCS/main.py` remains an unchanged 749-line reference; it was not regenerated, read from network shares or executed.

## 4. Verification — clean GitHub Actions Linux runner

**Executed (Python 3.12, `PYTHONPATH=src:.`, project installed editable with pinned runtime/test dependencies):**

- `python -m compileall -q src` — **passed**
- `ruff check src/vg2c/runtime/job.py` — **passed**
- `python -m pytest -q tests/runtime/test_job_runtime.py tests/runtime/test_direct_runtime.py tests/runtime/test_e2e_fixtures.py tests/runtime/test_control_parity.py tests/runtime/test_generated_symbols.py tests/runtime/test_scoped_macros.py tests/emitter/test_generated_project.py tests/emitter/test_emission_metadata.py tests/emitter/test_editing.py tests/emitter/test_no_vg2c_leak.py tests/emitter/test_macro_store_project.py tests/emitter/test_semantic_contracts.py` — **123 passed**.
- `python -m pytest -q` — **545 passed, six failed**, the same six inherited from Session 01's **541 passed, six failed** baseline (four Session 02 tests added). Exact new code checkpoint: `07d21b4fce683bfa7a0fbff0ff4943b215096469`; CI run `38059358746` and job `114234275895`. No new behavioral failure or softened test was observed.

**Exact inherited failures, left unchanged as out of scope:**
1. `tests/ui/test_document_store.py::test_shared_global_edits_persist_across_steps`
2. `tests/ui/test_document_store.py::test_reorder_persists_execution_order_and_generation_state`
3. `tests/ui/test_document_store.py::test_html_preview_is_safe_exact_approximate_and_path_bounded`
4. `tests/ui/test_html_preview.py::test_html_preview_replays_safely_and_does_not_write_outputs` (unsupported JMP/JSL)
5. `tests/ui/test_workspace_sessions.py::test_sql_column_choices_read_uploaded_server_csv_headers`
6. `tests/ui/test_workspace_sessions.py::test_file_backed_sql_filter_uses_workspace_choices_through_save_and_generate`

Previous intermediate CI runs identified source-shape assertions expecting `execute_sql(`/`render_html(` and one Ruff import-order issue, which were corrected. The last run proves the focused suite/compileall/Ruff pass. There has been **no authoritative original proprietary ScriptHost/production output differential**, local checkout, or real external reader/Oracle service integration run. A generated installed-runtime test uses isolated Python and deterministic stubs, not production external systems. Therefore final full acceptance remains **WIP**.

## 5. Next-session instructions — Session 03

- Fetch remote `refactor/generated-code/implementation` and verify it matches the **exact final SHA in the Session 02 final response**, not this file's preceding-checkpoint SHA. Continue this **same branch**, serially, without new branch/merge/rebase. Read latest master, `03_CSV_TABLE_PANDAS.md`, README, Session 03 prompt, and this handoff.
- Begin with focused + full suite; record six inherited failures without changing UI/JMP/JSL. Do not treat `job.*` source spelling as an earlier direct function call.
- Keep JobRuntime as compiler-independent sole per-run owner, existing MacroStore semantics, native Python control flow and metadata offsets. Keep SQLite execution, explicit reader/node/binds, `SQL_Get_CSV_List`, CSV header/schema identity, crosstab keys and file-path ownership correct. Reuse existing pandas where worthwhile; Session 03 alone owns table-option refactor. No CSS/HTML externalization until Session 04.
- Use isolated fixtures and read-after-edit tests. Do not interpret physical line-count changes as evidence of parity. Record all actual test outcomes and the final pushed SHA.

