# Session 04 revised handoff — HTML-first report editing

**Status:** Revision implemented and tested against source-derived fixtures; exact original Python ScriptHost differential parity remains WIP. Do NOT start Session 05 or merge branches.

Repository: VectorLim/SQLPathFinder_PY_Migration

Only implementation branch: refactor/generated-code/implementation

- Verified original Session 03 parent: a1bc15fff6ba574c1f3361ab19ca4dec4f47e4ec
- Verified prior Session 04 final SHA / revision parent: d7891f3cbf98d3257fad2b4cf044e5b165c77e18
- Revision test implementation checkpoint: 34187cdd93af7613bce423cc112910d0d979562b
- Later label escaping change and documentation are fast-forward descendants. Full final pushed SHA belongs in the final agent response because this document cannot include its own SHA.
- Prior Session 04 baseline CI: https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38106666226
- Revised checkpoint CI: https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38109086822

## Verified remote workflow

At revision start, the GitHub branch ref exactly matched d7891f3cbf98d3257fad2b4cf044e5b165c77e18. The initial branch descended from the Session 03 parent by 10 commits. Local git ls-remote could not connect due to DNS failure; the container had no checked-out repository. Consequently, local worktree/uncommitted status was not independently verifiable. Each commit used explicit parent/tree Git object creation and an expected-SHA, non-force branch update through the connected GitHub API. Exact commit CI ran in clean GitHub Actions Linux checkouts. No production ICMPCS job or private external network was executed.

## Source authority

Original Python ScriptHost is the semantic authority. The source call chain reviewed in the initial Session 04 decisions remains binding:

- SPFSQL3.py:20048-20120 — immediate CSS/HTML dispatch and distinct unsupported HTMLI5.
- SPFSQL3.py:20123-20156 — named deferred report definitions.
- SPFSQL3.py:20159-20189 — layout resolves reports at execution time.
- SPFSQL3.py:20224-20255 — HTML-DELETE removes recorded consumed report spec objects, not all CSS and definitions.
- SPFUtilities/utils.py:8866-9090 — stylesheet generation and formatting.
- SPFUtilities/utils.py:9092-9274 — immediate/deferred HTML report, source CSV and output defaults.
- SPFUtilities/utils.py:9324-9539 — live CSV header-driven column selection and four pattern families.
- SPFUtilities/utils.py:12000-12240,13303-13419,14003-14125 — layout, CSS embed/link and ordered HTML fragments.

These are SOURCE-OBSERVED file/function analyses. New tests are SOURCE-DERIVED; no independent original-engine execution was performed. Intentional safety deviations and unsupported original branches are listed below. See 04_HTML_CSS_SCRIPT_HOST_DECISIONS.md, especially its authoritative REVISION section.

## The problem found and the chosen design

Previous Session 04 introduced html/report_N.report.json when a report used >=8 fields or a long parameter list. In that design, output HTML still had a fixed number of numbered header placeholders based on the original VG2 source. An edited JSON field list could make headings and body cells disagree. Small reports kept presentation configuration in generated Python instead; ordinary users had no consistent place to edit table presentation.

Alternatives reviewed: keep JSON with dynamic headings (two editing locations); put all columns in main.py (long generated calls); embed a declarative table in HTML (chosen for reports referenced once); reusable table fragments (chosen only when a report is referenced by multiple layouts or has multiple HTML-DEFER definitions).

**Final ownership:** native Python controls execution and input/output paths; HTML owns source-column selection, display headings, column ordering, report layout and alignment; CSS owns presentation styling; SQL owns query text and data transformations. There is no new configuration format, generic page builder, browser dependency, Jinja or parallel rendering engine.

## New asset contract

    generated_project/
      main.py
      sql/query_*.sql
      html/report_*.html
      html/reports/ID_N.table.html       # only when shared/redefined
      styles/report_*.css
      output/                         # runtime files, not editable sources

One-use report:

    job.reports["R"] = csv_report("final.csv", output_file=None)
    job.html("html/report_056.html", output="revision.htm",
             css_file="report.css", embed_css=True)

Inside html/report_056.html:

    <table class="tblin" data-report="R">
      <thead><tr id="colhdr">
        <th data-field="LOT" data-align="left">Lot</th>
        <th data-field="A" data-align="center">Alpha</th>
        <th data-field="B" data-align="right">Beta</th>
      </tr></thead>
      <tbody></tbody>
    </table>

To change ordinary presentation, edit only this HTML file. Editing the label within th changes its display text; editing data-field chooses the CSV source column; moving/removing/adding th changes the table order/count; editing data-align changes generated cell alignment. No numbered header slots or JSON list synchronization. Runtime reevaluates the current input CSV header. All generated SQL/CSS/HTML remain editable without retranslation.

Reused/redefined report:

    job.reports["R"] = job.report(
        "html/reports/R_055.table.html", input_file="final.csv")

The layout source uses a dedicated TABLE slot for the report. The existing
render_html function inserts the selected HTML fragment at layout execution,
then performs the SAME declarative table transformation. A subsequent
definition of report ID R can select a different fragment. A report appearing
twice or in several pages has only one declaration to edit for that definition.
A one-use report needs no extra fragment.

There are now no automatically emitted .report.json files, even for large
reports. The existing JobRuntime.report_spec(path) reader and old numbered
header/row placeholder semantics are retained as an input compatibility path
for already-translated projects; new emission never invokes them. Do not
remove historic SQL .header.json or crosstab adapter support.

## Implementation surfaces

- src/vg2c/emitter/project.py: deleted report-size threshold and
  _report_call JSON emission; _table now builds one directly editable
  declaration. Prepass determines IDs referenced multiple times or defined
  multiple times. Immediate HTML-RUN produces its editable HTML and
  renders when encountered; HTML-DEFER registers concise csv_report or
  job.report; HTML-LAYOUT chooses inline declaration or a reusable HTML
  fragment. Source metadata still uses StepEmission/EmittedParameter.
- src/vg2c/runtime/html.py: CSVReport supports an optional table_template;
  csv_report now accepts no static columns (old columns remain accepted for
  existing generated projects). _ReportTables is a narrowly scoped HTMLParser
  locator for marked tables, headers, and tbody ranges. The current CSV header
  is read when the layout runs, resolved case-insensitively, and patterns are
  expanded. _rows remains the one cell formatting implementation.
  Only narrow source spans are transformed; authored HTML markup is retained.
  Generated rows are passed as renderer-owned Template substitution values
  after structural validation, not inserted early where CSV dollar-brace
  tokens could be incorrectly reinterpreted.
- src/vg2c/runtime/job.py: small job.report(fragment, input_file=...)
  constructor resolves the editable fragment via asset_path and returns
  an existing CSVReport. Per-job deferral and deletion state remain isolated.
  job.report_spec is unchanged solely for existing JSON projects.
- tests/runtime/test_html_assets_session04.py: stronger editing tests for
  short/long and shared/redefined reports, exact case-folded fields, all
  four source column-pattern types, changing input schema on rerun,
  header-only/empty/missing/duplicate CSV, malformed th, injection-safe CSV
  values with dollar-brace tokens, generated SQL→crosstab→CTARRAY→downstream
  SQL→HTML→linked/embedded CSS, and user HTML edits after generation.
- .github/workflows/session03-validation.yml: existing focused/full,
  runtime/emitter, F821/F823, compile, and representative source metrics.

## Actual HTML runtime column resolution

For an explicit th data-field, the loader resolves the user-authored field
against actual CSV column names case-insensitively, then outputs a matching
header and one corresponding cell per data row, preserving th order.
Duplicate case-insensitive source CSV names are rejected as ambiguous.
Unavailable explicit fields yield an error listing available fields.
Header-only CSV yields the editable headers and no rows. Empty CSV with no
header yields an error.

For SOURCE-OBSERVED original pattern families STARTS WITH:, ENDS WITH:,
CONTAINS:, STARTS/ENDS WITH (%):, the emitted th stores the
matching source pattern in data-pattern. The runtime dynamically expands
matching CSV headers in source order, with matching alignment and body cells.
Generated names follow original basic capitalization and underscore
substitution conventions. The unchecked legacy regex interpretation is
replaced with bounded literal matching and percent-as-wildcard for safety;
this is an intentional repair and not exact proprietary engine parity.
More involved COLUMN-FORMAT transformations are not implemented.

No runtime CSV value or pattern-generated heading is inserted unescaped into
HTML. Existing Template/HTMLParser structural slot checks and atomic output
writes remain authoritative. User-authored static markup, valid custom
classes/attributes and inline styles are trusted as source assets, not
sanitized indiscriminately. Explicit missing generated CSS stays diagnostic.

## Before/after UX and measured output

The checked-in old ICMPCS generated main.py was 748 lines, with nine inline
style assignments and maximum simple AST statement size 3028 characters.
Session 04 first revision achieved 137 lines, 59 statements, maximum simple
statement length 748, zero style assignments, and ONE report JSON asset.
The HTML-first revision maintains 137 lines / 59 simple statements / max
748 characters / zero inline style assignments, removes the report JSON
asset entirely, and still emits 8 SQL files, 2 HTML pages and 1 CSS
file for a read-only ICMPCS compilation. No production execution or overwrite.

One-use report: one layout HTML file, zero report JSON or extra fragments.
Multi-use report: one table fragment is reused across layout pages.
Adding/removing/reordering columns: HTML-only edit, no Python/JSON update or
retranslation. Dynamic pattern columns follow the runtime CSV schema.

## Verification and known inherited failures

Original Session 04 exact parent: 163 focused, 436 runtime/emitter,
614 full suite passed with six inherited UI/JMP failures; full CI was red.
Revision tested checkpoint 34187cdd93af7613bce423cc112910d0d979562b:
171 focused passed, 444 runtime/emitter passed, 622 full-suite passed,
the SAME six inherited failures. No additional full-suite failures.
compileall, scoped Ruff, synthetic pivot benchmark and offline SQL→pivot→
CTARRAY→SQL→HTML/CSS tests passed. See linked checkpoint CI.
Later targeted label-dollar-escape fix and docs require final exact-SHA CI;
its results and the final pushed SHA belong in the final agent response.

Six inherited failures, to carry into Session 05:
1. tests/ui/test_document_store.py::test_shared_global_edits_persist_across_steps
2. tests/ui/test_document_store.py::test_reorder_persists_execution_order_and_generation_state
3. tests/ui/test_document_store.py::test_html_preview_is_safe_exact_approximate_and_path_bounded
4. tests/ui/test_html_preview.py::test_html_preview_replays_safely_and_does_not_write_outputs (JMP/JSL)
5. tests/ui/test_workspace_sessions.py::test_sql_column_choices_read_uploaded_server_csv_headers
6. tests/ui/test_workspace_sessions.py::test_file_backed_sql_filter_uses_workspace_choices_through_save_and_generate

These are genuinely FAILED tests, not ignored/successful tests. Do not
expand this HTML-focused revision into UI/JMP work.

## WIP and precise Session 05 boundaries

- Original ScriptHost engine differential execution is still absent;
  reference output tests are source-derived only.
- COLUMN-FORMAT numeric/width/header-case subformats; complex style-cascade
  rules, dynamic CSS declaration values/filenames; original regex edge
  cases; exact encoding/default/error behavior; advanced HTMI/HTMLI5,
  chart/JS, distributed/email report variants are unsupported/unverified.
- Review old utilities/html_report.py only with static caller evidence:
  it remains for historic/preview callers and should not become a competing
  generated HTML renderer.
- Retain Session 03 canonical crosstab, runtime inferred grouping, 50k
  chunk FIRST/LAST, CTARRAY/CrossTab expansion and all SQLite/SQL bind
  semantics; Session 04 revision intentionally modifies NO SQL/pivot engine.
- Maintain EmittedScript.assets, semantic editing offsets, Save/Generate
  separation, nonempty-project guard and no compiler/VG2 import in generated
  runtime. Recheck end-to-end Linux wheel-installed, offline, relocated,
  read-only asset certification before release. Existing CI validates a
  clean Linux checkout with project runtime installed, but is not full
  release certification.
- Reconcile all six inherited UI/JMP failures separately with scoped evidence.
- Do not merge to main/html-and-sql-rework, do not force-push, do not start
  Session 05 without a new explicit instruction.
