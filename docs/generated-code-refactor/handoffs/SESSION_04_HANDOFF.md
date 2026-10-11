# Session 04 handoff — ScriptHost-grounded editable HTML/CSS/SQL

**Status:** Implemented and Linux-CI-tested for the documented supported subset; WIP for full original ScriptHost HTML/report parity. **Do not automatically start Session 05.**

**Repository:** \`VectorLim/SQLPathFinder_PY_Migration\`  
**Only branch:** \`refactor/generated-code/implementation\`  
**Exact verified Session 03 parent at start:** \`a1bc15fff6ba574c1f3361ab19ca4dec4f47e4ec\`  
**Tested implementation checkpoint (before this handoff commit):** \`59fed15c1fd5e44a3fbd45d052c53c924cbaaba6\`  
**Last checkpoint CI:** https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38106469080  
**Final handoff commit SHA:** In final agent response, since this file cannot contain its own SHA.

## Starting verification and restrictions

GitHub remote branch and predecessor exact SHA were verified through the connected
GitHub API before the first change. The earlier Session 03 CI at
https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38071796475
was overall red only because of the six inherited UI/JMP failures. Fresh
pre-implementation Linux CI on the source-audit-only checkpoint
\`d2ef0c520b9b6a3e2f3da9c0fb426a23f45bb159\`, run
https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38105594249,
was **606 passed, 6 failed**.

Local git fetch/checkout was unavailable because github.com DNS access failed
in the container. Local worktree cleanliness, local \`git remote -v\` and
\`git status --porcelain\` therefore could **not** be independently verified.
Remote SHA verification, exact-parent GitHub Git API commit creation and
fast-forward ref updates were used instead. All reported test runs were on
GitHub Actions clean Linux checkouts of exact source SHAs. Original proprietary
ScriptHost was **not executed**; source-derived tests are not original-engine
differential confirmation. Production ICMPCS was neither run nor overwritten.

## Original ScriptHost: binding source observations

All relative to \`scripthost-utilities-decompiled/SPSQL3_py/SPFLib/\`:

| Source path/lines | Observed, implemented or still open |
| --- | --- |
| \`SPFSQL3.py:20048–20120\` | \`HTMLRunTask.executeTaskCommand\` immediately dispatches real TYPE=CSS through \`Generate_Style_Sheet\` and real TYPE=HTML through \`Generate_HTML_Report("H")\`. TYPE=KEY is a design-table header, not a report. HTMLI5 has a separate unsupported engine. |
| \`SPFSQL3.py:20123–20156\` | \`HTMLDeferTask.executeTaskCommand\` writes the per-instance, per-ID definition for a later layout; repeating the identifier targets that name. |
| \`SPFSQL3.py:20159–20189\` | \`HTMLLayoutTask.executeTaskCommand\` renders named references at execution time by \`Create_HTML_Window\`. |
| \`SPFSQL3.py:20224–20255\` | \`HTMLDeleteTask.executeTaskCommand\` removes report spec paths recorded by layouts, not all stylesheet and deferred state. |
| \`SPFUtilities/utils.py:8866–9050,9052–9090\` | \`Generate_Style_Sheet\` writes CSS; FORMAT keys case-insensitive, later duplicates replace earlier lookup, font-size normalization; some dynamic CSS paths unverified. |
| \`utils.py:9092–9274\` | \`Generate_HTML_Report\` requires CSV input and defaults immediate output to \`SQLPathFinder.htm\`. |
| \`utils.py:9324–9539\` | \`Check_Column_Pattern\` distinguishes COLUMN-DATA projection, COLUMN-HEADERS display, COLUMN-FORMAT and patterns based on actual runtime CSV schema. Full dynamic expansion is still WIP. |
| \`utils.py:12000–12240\` | \`Create_HTML_Window\` processes :FILE:, :TITLE:, :CSS:, :CSSEMBED: and ordered HTM/HTMI references, tracks reports consumed for deletion. |
| \`utils.py:13303–13419\` | \`Get_Rpt_LayOut_Header\` links or embeds CSS, with an original missing-file embed→link fallback. Safer failure for generated required CSS is intentional. |
| \`utils.py:14003–14125\` | \`Process_HTM\` appends authored HTML and named report fragments in declared order; interactive variants require separate support. |

See \`docs/generated-code-refactor/04_HTML_CSS_SCRIPT_HOST_DECISIONS.md\`
for task/option matrix, comparison against initial vg2c, exact/source-derived/
intentional-difference/unsupported tiers, and remaining parity gaps.

## Actual implementation, contracts and ownership

Changed \`src/vg2c/emitter/project.py\`:
- Correctly skip initial TYPE/KEY schema header when choosing report TYPE;
  representative \`ICMPCS.txt:55-66\` validated with fixture test.
- TYPE=CSS: emit \`styles/report_<block>.css\` with canonical
  \`runtime/html_format.build_css\`, and a short \`job.define_css\` action at
  the correct step; do not inject giant \`job.styles[...]\` lists.
- TYPE=HTML: create editable \`html/report_<block>.html\`, then execute
  immediate \`job.html(...,reports={...})\`. TYPE=HTMLI5 diagnostic.
- HTML-DEFER: keep source-order \`job.reports[ID]\` assignments. Small
  explicit column lists use \`csv_report\`; large report schemas (8+ fields or
  long representation) live in one editable
  \`html/report_<block>.report.json\` passed through \`job.report_spec\`.
- HTML-LAYOUT: source-to-HTML shell in \`html/report_<block>.html\`,
  short \`job.html(..., output=..., css_file=..., embed_css=...)\`, runtime
  substitution and copy/embed mode. Editor metadata/StepEmission stable.
- HTML-DELETE: one \`job.delete_html()\` call, replacing unconditional
  clearing of all report, CSS and style state.
- SQL assets from Sessions 02–03 remain \`sql/query_*.sql\`; none of the
  SQL or pivot engine files were modified in Session 04.

Changed \`src/vg2c/runtime/job.py\`:
- \`define_css(logical_output_path, asset_path)\` reads *edited source CSS*
  from the project asset root when the step executes, atomically publishes
  it into the chosen workdir, and sets the per-job active CSS filename.
  Disallow writing outside workdir or overwriting source asset.
- \`report_spec(path)\` reads and validates adjacent editable JSON on each
  report-definition execution and constructs a \`CSVReport\`; no new renderer.
- \`html(..., reports=None, css_file=None, embed_css=False)\` uses explicit
  per-call report map for immediate HTML or per-job deferred map for layouts;
  relative CSS resolves first from executed workdir output, next project
  asset root, then template-sibling fallback in \`render_html\`.
- \`delete_html()\` deletes consumed report IDs only; unconsumed reports
  and CSS remain. State is fresh for each \`JobRuntime\`.
- Existing SQL, macro, CTARRAY, workdir and user-file APIs unchanged.

Changed \`src/vg2c/runtime/html.py\` and
\`src/vg2c/runtime/html_format.py\`:
- Existing \`Template\`, HTMLParser slot validator, escaping, single renderer
  and atomic writes remain the authority.
- Missing required CSS or missing referenced input CSV fails with a precise
  path instead of a misleading empty/styled default.
- Stylesheet FORMAT name matching is case-insensitive; no new Jinja engine,
  style manager or duplicate formatting pipeline.
- Used deferred report IDs are recorded after a successful render, for
  correct later HTML-DELETE behavior.

New/extended \`tests/runtime/test_html_assets_session04.py\` verifies actual
TYPE/KEY header parsing, immediate CSS and HTML, edited source assets on rerun,
CSS embedding/links, missing files, consumed/unconsumed delete behavior,
dynamic native Python style branch/loop state, moved project and unrelated CWD,
long JSON report definitions with changed labels, and a fully connected
offline SQL→wide pivot→CTARRAY→editable downstream SQL→HTML scenario.

Updated \`.github/workflows/session03-validation.yml\` (name inherited) to
test Session 04, run F821/F823 across runtime/emitter/utilities, preserve
Session 03 benchmarks, and print read-only generated-code metrics.

### External editable artifact contract

\`\`\`text
generated_project/
  main.py                         # control flow and lean calls only
  sql/query_*.sql                 # SQL body, editable and reread at execution
  html/report_*.html              # authored shell, editable and reread on layout
  html/report_*.report.json       # only larger report schemas, editable and reread at report definition
  styles/report_*.css             # static source CSS, editable and reread by define_css
  output/                         # output workdir: copied CSS and rendered HTML/CSV
\`\`\`

Important distinction: the source under \`styles/\` is never overwritten by
the generated job; HTML-RUN **publishes a separate output CSS file** at its
original source position. Thus user edits to source CSS are honored next run.
The emitted call uses \`css_file\`, never an invented \`css\` keyword.

### Representative generated code, before and after

Before (legacy checked-in ICMPCS source, condensed):
\`\`\`python
styles["Column-Headers"] = [
    "background-color:#dbd9c0", "color:#444", "font-size:12",
    # ... many formatting entries ...
]
styles["Column-Data"] = [...]
css_file = "sqlpathfinder_style_1.css"
reports["R"] = csv_report(
    "data.csv", columns=[...], headers=[...], alignment=[...],
)
\`\`\`

After (Session 04 contract, representative; identifiers vary by block):
\`\`\`python
job.define_css("sqlpathfinder_style_1.css", "styles/report_000.css")
job.reports["R"] = job.report_spec("html/report_055.report.json")
job.html(
    "html/report_056.html",
    output="revision.htm",
    css_file="sqlpathfinder_style_1.css",
    embed_css=True,
)
job.delete_html()
\`\`\`

The second example is a representative composition of generated call shapes,
not a claim that one specific ICMPCS block has all these calls.

### Style and report-state timeline

| Source point | Under conditional/loop | Runtime effect | Output/data read |
| --- | --- | --- | --- |
| HTML-RUN TYPE CSS | Can occur inside native Python branch/loop | Read one editable static CSS asset; copy into current workdir and choose active CSS path | CSS source at action time |
| HTML-RUN TYPE HTML | At its native Python source point | Use immediate CSV spec and shell | Render output now |
| HTML-DEFER ID=R | At its native source point, possibly redefined | Assign/replace per-job report spec | JSON read now if long; CSV deferred |
| HTML-LAYOUT | Only if branch/iteration reached | Read authored HTML source and selected style; render ordered report contents; record successful report use | CSV and CSS read at layout |
| HTML-DELETE | Only if reached | Remove recorded consumed report IDs; keep CSS and unconsumed reports | No generated source assets deleted |
| Next run(workdir=...) | New \`JobRuntime\` | Independent CSS/reports; independent output workdir | Sources are reloaded |

Static CSS edits change output on rerun; no speculative CSS-state combinations
or global registry. Styling values themselves containing dynamic runtime
macro tokens remain WIP, not incorrectly advertised as fully supported.

## Verification results and measurable user readability

**Baseline before code:** CI 38105594249 on docs-only
\`d2ef0c520b9b6a3e2f3da9c0fb426a23f45bb159\`:
**606 passed / six inherited failed**.

**Latest tested implementation:** \`59fed15c1fd5e44a3fbd45d052c53c924cbaaba6\`,
CI https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38106469080:
- \`python -m compileall -q src\` — passed.
- \`ruff check --select F821,F823 src/vg2c/runtime src/vg2c/emitter src/vg2c/utilities\` — passed.
- Existing extended Session 03/04 focused list — **163 passed**.
- Existing synthetic SQL join and crosstab benchmark step — passed (not proprietary source parity).
- \`PYTHONPATH=src:. python -m pytest -q\` — **614 passed / 6 failed**,
  **no new unexpected failures**.
- Connected offline SQL→pivot→CTARRAY→downstream SQL→HTML and asset rerun
  passed as part of focused/full testing.
- CI workflow result remains **red** due to six inherited failures; never
  claim full workflow is green.

Read-only ICMPCS generation in CI, no production execution, compares
checked-in old main.py vs newly compiled main.py:

| Observable size metric | Before | After |
| --- | ---: | ---: |
| Physical Python lines | 748 | 137 |
| AST simple statements | 75 | 59 |
| Longest simple statement, characters | 3028 | 748 |
| Explicit style property assignments | 9 | 0 |
| Explicit CSS-file assignments | 4 | 0 |

Re-emitted artifacts: **8 SQL, 2 HTML, 1 CSS, 1 report JSON**.
The physical line reduction is approximately **81.7%**; raw line count
comparison is not equivalent to proof of runtime semantic parity.

**Inherited six failing UI/JMP tests (unchanged from predecessor):**
1. \`tests/ui/test_document_store.py::test_shared_global_edits_persist_across_steps\`
2. \`tests/ui/test_document_store.py::test_reorder_persists_execution_order_and_generation_state\`
3. \`tests/ui/test_document_store.py::test_html_preview_is_safe_exact_approximate_and_path_bounded\`
4. \`tests/ui/test_html_preview.py::test_html_preview_replays_safely_and_does_not_write_outputs\`
5. \`tests/ui/test_workspace_sessions.py::test_sql_column_choices_read_uploaded_server_csv_headers\`
6. \`tests/ui/test_workspace_sessions.py::test_file_backed_sql_filter_uses_workspace_choices_through_save_and_generate\`

No existing active Session 03 pivot/crosstab engine file was changed. Full
tests include \`test_crosstab_script_host_parity.py\`,
\`test_crosstab_corrective_pass.py\`, CTARRAY and SQLite binding regressions.
The 50k chunk-first/LAST and combine_first policy remains Session 03's
source-derived, **not original-engine differential-verified**, contract.

## Remaining verification and Session 05 instructions

1. **Semantic fidelity WIP:** runtime COLUMN-DATA pattern expansion against
   actual CSV headers, COLUMN-FORMAT numeric/width/header-case rules,
   independent dynamic header count, repeated IDs with changed report schema,
   macro-dependent CSS declaration values, dynamic report IDs/paths, some
   output/default and deletion edge cases. Review authoritative
   \`utils.py:9092-9274,9324-9539,9715+\`, and original task paths.
2. **Review error policy:** source sometimes suppresses missing CSV and
   missing embed CSS errors; current generated output deliberately fails on
   missing required assets. Differentially verify original behavior and
   document narrow intentional safety repair, instead of restoring silent
   fallback solely for legacy vg2c tests.
3. **Dead code/callers:** \`utilities/html_report.py\` still renders for
   historic tests and UI preview. Inspect its callers first, especially
   \`tests/runtime/test_html_report.py\` and UI preview, before any Session 05
   removal/bridging. Do not delete semantic editing models or public APIs
   merely because direct Python uses the new asset contract.
4. **UI root-cause:** triage the six inherited failures separately, with
   real scoped fixes and unchanged Session 04 semantics. JMP/JSL remain out
   of scope unless future product approval.
5. **Linux release certification:** test a packaged/wheel-installed
   \`vg2c.runtime\` in a fresh Linux environment without compiler checkout,
   VG2 input file, proprietary ScriptHost import, network drives or
   production credentials; test relocated generated project, readonly source,
   Unicode/charset, CSS link relpaths, and artifacts.
6. **Original-source differential:** if sanitized isolated ScriptHost
   execution becomes possible, compare meaningful normalized DOM, CSS rules,
   source-order output, input selection and column width semantics; label
   previous source-derived cases honestly until then.
7. **Integration:** retain/extend the connected SQL→pivot→CTARRAY→downstream
   SQL→HTML test; no second pivot/HTML engines; preserve SQL output /HEADERS
   separate from display COLUMN-HEADERS.
8. **Metadata and save/generate:** add broader semantic editor and Save vs
   Generate replacement tests if shared UI/metadata changes later; preserve
   \`EmittedScript.assets\`, \`StepEmission\`, \`EmittedParameter\`,
   nonempty project overwrite guard, and per-workdir isolation.

Do not merge to \`main\`, \`html-and-sql-rework\`, or any other branch.
Session 05 starts only on explicitly requested follow-up, from the final
pushed Session 04 SHA reported by the agent, after remote ancestry checks.
