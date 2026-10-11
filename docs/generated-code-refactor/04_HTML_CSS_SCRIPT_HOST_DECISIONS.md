# Session 04 corrective pass — exact references and safe editable HTML

**Authority:** Original Python ScriptHost where behavior is source-observed; safe and predictable generated HTML editing where ScriptHost has no declarative-table counterpart. This section supersedes conflicting earlier pattern/template statements below. **Starting remote SHA:** `b0d08e239a98690c2fff0734bf8ad73c84ede8aa`. The original revision parent `d7891f3cbf98d3257fad2b4cf044e5b165c77e18` is a verified ancestor. Work is scoped to Session 04 on `refactor/generated-code/implementation`.

## Evidence and fixes

| Issue | Evidence | Root cause | Corrected contract |
|---|---|---|---|
| A: `STARTS/ENDS WITH (%):` | **SOURCE-OBSERVED**: `SPFUtilities/utils.py:9400–9475`, `Check_Column_Pattern()`, explicitly distinguishes percent present and absent | A fully anchored regex was used for both | Without `%`: starts-with, case-insensitive. With one/more `%`: entire name, percent as wildcard. Match current CSV headers in original CSV order; expand headings/alignments/body cells together. **INTENTIONAL-FIX**: escape literal regex metacharacters instead of trusting arbitrary regex input. |
| B: partial `HTM:` IDs | **SOURCE-OBSERVED**: `Create_HTML_Window()` (`utils.py:12175–12205`) and `Process_HTM()` (`utils.py:14003–14125`) process ordered layout lines | Repeated unbounded `str.replace()` corrupted IDs with shared prefixes and also replaced authored inline text | One shared, anchored full-line pattern is used both by reference counting and by one callback substitution. Preserve report ID, source order, repeat count and whitespace, case-sensitive exact identity, and shared/redefined fragments. Unknown IDs raise a diagnostic. Source-backed unsupported `HTMI/HTMIC` lines now raise instead of becoming stray page text. |
| C: literal currency | **SOURCE-DERIVED** editing usability; original ScriptHost does not use Python `string.Template` for authored HTML | Global Template parsing treated `$100` as malformed and `$USD` as a missing variable | **INTENTIONAL-FIX**: explicit braced slots `${NAME}` are always dynamic and missing names error; legacy bare `$NAME` is dynamic only when its name is registered in caller values or supported report/renderer slots. All other dollars are literal. `$$` retains the existing dollar-escape meaning. Malformed explicit `${` is rejected; to display a literal valid placeholder sequence, write `$${NAME}` (Template `$$` escape followed by text). Preserve structural slot context checks. |
| D: marked table body | **UNVERIFIED** original static-row analogue; the HTML-first marked tbody is a new rendering contract | The renderer silently overwrote arbitrary body markup | **INTENTIONAL-FIX**: marked `<tbody>` is renderer-owned. Whitespace, HTML comments and at most one same-report `${ID_ROWS}` / `$ID_ROWS` compatibility slot are preserved; static rows or other body elements raise a clear error before writing output. Unmarked authored tables are unchanged. |

### Template and attribute ownership

Dollar normalization occurs for validation and **after** declarative table parsing but **before** the one authoritative Template substitution. This is necessary for literal CSV headers such as `A$B`: the table locator reads the original HTML attribute without doubling the dollar, while the eventual Template sees safely escaped text. Rows are inserted as trusted renderer-owned slot *values*, after parsing, so CSV values containing `${NOT_A_SLOT}`, ampersands, angle brackets, Unicode or quotes cannot execute substitutions or inject markup.

Supported slots remain `${VG2C_CSS}`, `${ID_TABLE}`, `${ID_ROWS}`, numbered legacy `${ID_HEADER_n}`, value `${VALUE_n}` and currently supplied caller values (including their legacy bare syntax). Unknown explicitly braced names error. Caller values may not impersonate renderer CSS, dynamic rows, report tables, report rows or numbered report headers. Structural restrictions on URL/style/event attributes, script/style content, comments and dynamic HTML names continue to apply.

### Test-first evidence and bounded review

At the baseline `b0d08e2`, 172 focused and 445 runtime/emitter tests passed; 623 full-suite passed and the six previously recorded UI/JMP failures persisted. New corrective tests at `56b2aef30b751307e6d4f2bae66520b0967329a0` failed as designed: five parameterized cases covered A (1), B (2), C (1), D (1). CI: https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38110875588 ; 445 preexisting runtime/emitter tests continued passing. Correction checkpoint `148bdaa8d14c4fa3b63428a4280d8d410feddaee`: all 628 full-suite passing cases passed, with the SAME six inherited full-suite failures and no new regressions; https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38111007978 . Further pattern, CSV-dollar, repeated-reference and malformed directive tests were subsequently added and require exact-final-SHA CI verification.

Changed executable paths: `src/vg2c/runtime/html.py` and `src/vg2c/emitter/project.py`, with tests in `tests/runtime/test_html_corrective_session04.py`. SQL, pivot, CTARRAY, frontend and the original ScriptHost were not changed. Existing HTML-first inline/fragment design, CSS assets, SQL assets, runtime rereads, generated native Python and zero new `.report.json` remain intact. No second HTML renderer or new configuration format was introduced.

**Still WIP:** exact original-engine differential parity, full COLUMN-FORMAT behavior, interactive HTMI/HTMLI5/HTMIC, distributed/email flows and broader ScriptHost presentation nuances. The six existing UI/JMP failures remain explicitly inherited. Do not start Session 05 or merge any branches. Final complete fast-forward commit SHA and exact-CI results are reported in the final agent response, as a tracked handoff cannot self-reference its own SHA.

---

# REVISION — HTML-first report definitions (authoritative)

This revision supersedes the old large-report JSON sidecar design described below.
The original Session 04 source audit and the earlier CI records remain as history.

## Defect and ownership decision

Original Session 04 used Python column lists for short reports but emitted
html/report_N.report.json for longer reports (8 columns or 180 characters).
Layout templates held numbered HEADER placeholders fixed at translation.
Editing the JSON to add/remove/reorder columns changed body cells without
changing the number or order of HTML headings. This was incompatible with
runtime column patterns in the original ScriptHost.

Approaches evaluated:

| Alternative | Ordinary presentation edit | Extra file | Why selected/rejected |
| --- | --- | --- | --- |
| A: JSON sidecar plus dynamic headers | Edit JSON, inspect HTML | JSON | Reject: two confusing owners |
| B: lists in main.py | Edit Python | None | Reject: verbose, not user-oriented |
| C: declarative HTML table in the layout | Edit one HTML document | None | Adopt: one-use report |
| D: reusable HTML table fragment | Edit one shared HTML fragment | Fragment when needed | Adopt: same report reused or redefined |

One marked table uses HTML as the sole source of truth for fields, header
display labels, column order and alignment. Input CSV path and execution
sequence stay in concise native Python. SQL and CSS remain standalone.
No new report JSON is generated for any report size.

## HTML-first contract and a representative edit

    <table class="tblin" data-report="R">
      <thead><tr>
        <th data-field="LOT" data-align="left">Lot</th>
        <th data-field="A" data-align="center">Alpha</th>
        <th data-field="B" data-align="right">Beta</th>
      </tr></thead>
      <tbody></tbody>
    </table>

To add a column, add one th with data-field equal to a CSV column; to
remove/reorder columns, remove/reorder th elements. The th content is its
display header; data-align governs generated TD alignment. No generated
Python changes, header-slot maintenance, report JSON, or retranslating VG2.

For one-use reports, this table is in html/report_N.html and Python registers
the CSV path using csv_report. If a report is referenced by multiple layouts,
or a named report is redefined, each HTML-DEFER definition owns one editable
html/reports/ID_N.table.html fragment. The generated Python uses
job.report(fragment_path, input_file=...) and each layout contains a TABLE
slot for that ID. Layout-time resolution chooses the active per-job
definition, so redefinition retains its execution-time meaning.
Fragments are never duplicated between pages that use the same definition.

## Dynamic CSV schema, security and source grounding

- At HTML-LAYOUT time, existing runtime/html.py loads the actual CSV
  header and marked table declaration; source columns resolve case-
  insensitively, preserving the explicit visual order.
- Original ScriptHost families from SPFUtilities/utils.py:9324-9539 are
  recognized: STARTS WITH:, ENDS WITH:, CONTAINS: and
  STARTS/ENDS WITH (%):. A source pattern becomes a th whose data-field
  identifies the pattern type and data-pattern provides the original
  matching header text; it expands into matched columns in CSV order,
  with corresponding headings and aligned body cells.
- Empty CSV header, missing input, duplicate case-insensitive headers,
  missing requested columns and malformed table markup produce diagnostics.
  Header-only CSV produces a valid zero-row table.
- An HTMLParser locator records narrow element/body offsets rather than
  parsing/serializing the entire document. Authored attributes, comments,
  nested header markup, layout text and inline style are retained.
  The existing Template substitution, slot validation and atomic output
  writer remain the sole renderer. Dynamically produced row HTML enters
  only through a late, renderer-owned substitution value, so untrusted CSV
  strings containing dollar-brace syntax cannot be interpreted as
  template variables. CSV text and pattern-generated headings are escaped.
- HTML-RUN CSS publish-on-execution, immediate HTML-RUN, HTML-DEFER,
  HTML-LAYOUT ordering, HTML-DELETE of consumed IDs, linked/embedded CSS,
  asset rereads and independent workdir state are retained.
- Source provenance: SPFSQL3.py:20048-20120 immediate run;
  20123-20156 deferred named specs; 20159-20189 layout;
  20224-20255 cleanup; SPFUtilities/utils.py:9092-9274 report generation;
  9324-9539 dynamic column handling; 12000-12240 and 14003-14125 layout
  assembly. These are source-observed, not independent engine executions.
- Intentional source differences: bounded literal pattern matching instead
  of unrestricted regex patterns, HTML/attribute escaping, safe required-
  asset diagnostics, and no arbitrary user-to-CSS/JS interpolation.
  COLUMN-FORMAT widths/type conversions/header case rules and interactive
  HTMLI5/HTMI/JMP modes remain WIP or unsupported.
- Existing generated .report.json is accepted via the old
  JobRuntime.report_spec reader and existing numbered header/ROWS slots
  remain readable. No second renderer is introduced. SQL .header.json
  and old .crosstab.json readers are untouched.

## Evidence and measurements

Starting revision SHA: d7891f3cbf98d3257fad2b4cf044e5b165c77e18.
Tested revision checkpoint: 34187cdd93af7613bce423cc112910d0d979562b.
CI https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38109086822
reported 171 focused and 444 runtime/emitter passes; full suite 622 passes,
six inherited UI/JMP failures. Compilation, Ruff F821/F823 and synthetic
SQL/pivot integration/benchmark passed. Final exact-SHA CI is required
after any subsequent fixes or documentation.

Representative read-only ICMPCS compile-only measurement: previous Session
04 137 Python lines, 59 simple statements, longest 748 characters, one
.report.json; HTML-first 137 lines, 59 statements, longest 748 characters,
ZERO .report.json. Assets: 8 SQL, 2 HTML pages, 1 CSS; zero JSON report
sidecars. One-use report editing requires one HTML file (short or long);
reuse requires editing one shared HTML fragment, never copying a table into
multiple page HTML files. No proprietary ICMPCS execution was attempted.

## Session 05

Keep Session 03 canonical SQL/pivot/CTARRAY contracts unchanged. Review the
six inherited UI/JMP failures separately, legacy utilities/html_report.py
preview-only call sites before deletion, unsupported ScriptHost formatting and
interactive layout branches, and Linux wheel-installed offline release.
No original-engine differential parity was established by these tests.

---

# Session 04 — ScriptHost HTML/CSS ownership decisions

**Authority:** original Python ScriptHost, not existing vg2c convenience behavior.  
**Start:** `a1bc15fff6ba574c1f3361ab19ca4dec4f47e4ec` on `refactor/generated-code/implementation`.  
**Evidence levels:** SOURCE-OBSERVED means code path explicitly inspected (not an executed oracle); SOURCE-DERIVED is an interpretation needing differential execution; DIFFERENCE is a documented safe/maintainability change; UNSUPPORTED means a feature remains out of scope. No source-differential run was available.

## Original source audited

* `scripthost-utilities-decompiled/SPSQL3_py/SPFLib/SPFSQL3.py:20048-20119`: HTML-RUN saves a temporary spec and dispatches TYPE CSS to `Generate_Style_Sheet`, TYPE HTML to `Generate_HTML_Report("H",...)`, and HTMLI5 to a separate JS renderer.
* `SPFSQL3.py:20123-20156`: HTML-DEFER writes a named temporary spec `<instance>_<ID>_tmp_.ini`. Repeated definitions use the same pathname; overwriting that name is a source-derived result.
* `SPFSQL3.py:20159-20189`: HTML-LAYOUT invokes `Create_HTML_Window` at execution time.
* `SPFSQL3.py:20224-20255`: HTML-DELETE deletes filenames collected in `gHTMDelete` and resets *that list*, **not** global styles, every deferred spec, or `css_file`.
* `SPFUtilities/utils.py:8866-9050`: `Generate_Style_Sheet` formats a CSS spec and writes the named CSS file. `utils.py:9052-9090` uses last matching TYPE FORMAT/KEY row, normalizes font-size, and omits entries without a colon. `build_css` in vg2c is an approximation to be evaluated by selector/rule comparison.
* `utils.py:9092-9274`: HTML report requires input CSV, defaults output to SQLPathFinder.htm, expands column patterns after CSV headers are available, and renders a report immediately or as an HTML fragment.
* `utils.py:9324-9539`: COLUMN-DATA, COLUMN-HEADERS, COLUMN-FORMAT are separate; STARTS WITH, ENDS WITH, CONTAINS and percent-wildcard patterns expand against *runtime* CSV headers.
* `utils.py:12000-12240`: layout interprets :FILE:, :TITLE:, :CSS:, :CSSEMBED:, HTM:/HTMI:, and records **existing referenced reports** for later deletion.
* `utils.py:13303-13419`: missing stylesheet with requested CSS embedding falls back to linked CSS in original code. Generated *required* editable assets must not silently fall back to synthetic CSS.
* `utils.py:14003-14125`: `Process_HTM` assembles authored text/report fragments in source order; HTMI and HTMIC have separate interactive paths.

## Behavior and ownership matrix

| Option / task | Original ScriptHost flow / observable outcome | Current vg2c flow at start | Target owner / decision | Evidence tier |
|---|---|---|---|---|
| HTML-RUN TYPE CSS | Generate_Style_Sheet writes CSS immediately | Mutates `job.styles` and `css_file`, does not necessarily create CSS now | Emit source CSS as editable asset; execute only source-order stylesheet selection; no CSS formatting literals in main | SOURCE-OBSERVED / DIFFERENCE (build at translation time for static styles) |
| HTML-RUN TYPE HTML | Generate_HTML_Report("H") writes output immediately | Raises or ignores supported TYPE HTML | Implement via runtime CSV report with explicit output, without delaying until a later layout | SOURCE-OBSERVED |
| HTMLI5 | Separate interactive JS renderer | Not supported in direct output | Explicit diagnostic; no invented static HTML equivalent | UNSUPPORTED |
| HTML-DEFER | Save per-instance named spec; potentially replace on duplicate ID | `job.reports[ID]=csv_report(...)` | Preserve execution-time assignment and ID replacement; future layouts resolve runtime object | SOURCE-OBSERVED / SOURCE-DERIVED |
| HTML-LAYOUT | Create_HTML_Window processes references at execution | Generated shell and `job.html` with runtime reports | External editable HTML shell; runtime HTML renderer, source-order layout | SOURCE-OBSERVED |
| HTML-DELETE | Delete report spec files *recorded on layout*, reset deletion list | Clears all reports, styles and css_file | Remove only consumed deferred entries; preserve unconsumed reports and active stylesheet | SOURCE-OBSERVED / DIFFERENCE |
| Style state and branches | CSS generation occurs at HTML-RUN execution; layout chooses effective CSS then | Mutating global-style-like job dict per run | Per-job state only; static asset selection inside emitted native branches/loops | SOURCE-DERIVED |
| Input and output defaults | INPUT-FILE required; immediate HTML default SQLPathFinder.htm | Missing source CSV silently yields no rows; fallback often report.html | Distinguish immediate vs deferred defaults; preserve diagnostic for unsupported/invalid specs | SOURCE-OBSERVED |
| Dynamic report IDs | Named by /ID, per-instance spec path | Compile-time report ID identifiers | Keep named runtime assignments; dynamic IDs require source-confirmed support or explicit diagnostic | SOURCE-DERIVED |
| COLUMN-DATA | Selects/projects/orders source columns; pattern expands from input header | Literal `columns=[...]` and case-folded DictReader mapping | Explicit report projection remains separate from SQL /HEADERS; pattern handling must be runtime | SOURCE-OBSERVED |
| COLUMN-HEADERS | Displays labels independent of actual CSV column names | Literal `headers=[...]` in emitted code | Editable adjacent spec only when needed, or compact call | SOURCE-OBSERVED |
| COLUMN-ALIGNMENT | Formats HTML cells, not CSV schema | Literal `alignment=[...]` | Report formatting ownership, no conflation | SOURCE-OBSERVED |
| COLUMN-FORMAT | Distinct table number/width/display formatting | Runtime rejects active formats | Diagnose unsupported source options; do not claim parity | UNSUPPORTED |
| Placeholders/macros | Spec expansion uses original runtime global substitutions | Safe Template slots + runtime macro substitution | Preserve runtime resolution, escaping, and documented security differences | SOURCE-DERIVED / DIFFERENCE |
| CSS linked | Link external stylesheet, with path subject to original distribution mode | Copies CSS beside rendered HTML | Keep relocatable link/copy from project assets | SOURCE-OBSERVED / DIFFERENCE |
| CSS embedded | Reads CSS and writes inside head; missing file falls back to link | Inlines CSS; missing asset silently `build_css` | Generated required CSS must exist; preserve embed and reread semantics | SOURCE-OBSERVED / DIFFERENCE |
| Authored markup | Process_HTM appends text lines in order | Copies layout shell into .html | Preserve authored markup, validate dangerous dynamic insertion, never HTML-escape entire authored template | SOURCE-OBSERVED / DIFFERENCE |
| Multiple reports | Ordered HTM references, distinct indexed report markup | Compile-time HTM expansion and runtime CSV slots | Preserve order and identity; avoid compile-time required-value capture | SOURCE-OBSERVED |
| Repeated report IDs | Same per-instance temp filename used again | Compile-time disallows a different count of display headers | Permit source-sensible replacement at execution; unresolved dynamic column count is a known gap | SOURCE-DERIVED |
| Empty or missing CSV | Original generator errors for missing file; empty CSV handled as error in column-pattern path | Empty report string | Require an explicit, source-justified policy instead of silent success | SOURCE-OBSERVED |
| Malformed report spec | Original loader/generator reports or raises error | Some unknown active options rejected, formats silently absent | Explicit scoped error; never invent a renderer | SOURCE-OBSERVED / DIFFERENCE |
| SQL /HEADERS vs report headers | Different stages and distinct semantics | Separate SQL and HTML options | Preserve independent SQL schema and visual labels | SOURCE-OBSERVED |

## Smallest intended ownership model

`main.py` owns execution/control flow; `sql/query_*.sql` owns query text; `html/report_*.html` owns layout; `styles/*.css` owns static stylesheet content. `JobRuntime` retains only active stylesheet choice, deferred report specs, and the set of consumed report IDs; `runtime/html.py` is the sole HTML renderer and `runtime/html_format.py` is the sole CSS formatter when conversion is required. `csv_report` captures input/label/alignment details for runtime resolution. Asset reads happen at runtime and user edits are not overwritten. No compiler or ScriptHost import is required to run emitted Python.

CSS/HTML generation in the original may write outputs during HTML-RUN; static asset generation at translation is an intentional *artifact lifecycle* difference, while branch-selected subsequent output still occurs at runtime. Source's missing-CSS linking fallback must not mask accidentally deleted required generated assets. Dynamic styles, dynamic IDs, column patterns, width/number formats and full original layout/distribution modes require explicit tests before declaring parity.

## Verification rules

Inspect source and emitter snapshots; run exact-SHA Linux CI and focused/full tests. Label fixtures **SOURCE-DERIVED**, not original-engine verified. The six inherited UI/JMP failures are not an acceptable green baseline. Session 03 SQL/pivot behavior must remain unchanged.


## Session 04 implementation results and provenance (2026-10-11)

**Source parser correction:** SPF design rows begin with a schema header such as
`Type<delimiter>Key<delimiter>COL1...`; the first such TYPE/KEY row is *not* the active
report type. The actual TYPE/CSS or TYPE/HTML row further down dispatches
`HTMLRunTask`. The initial new emitter incorrectly treated KEY as semantic
TYPE; regression `test_type_key_header_precedes_actual_css_report_type` caught
this. The corrected emitter skips TYPE/KEY while selecting active TYPE.
Provenance: `SPFSQL3.py:20083-20100`; representative read-only
`ICMPCS.txt:55-66`.

### Implemented and tested ownership

* `emitter/project.py` stores immediate CSS in
  `styles/report_<block>.css` and emits one
  `job.define_css(logical_output_path, asset_path)` call at its original
  source point. CSS FORMAT keys are case-insensitive, and
  `html_format.build_css` remains the only stylesheet formatter.
  Generated CSS source remains editable; only workdir CSS output is written
  on execution.
* Immediate TYPE HTML emits one user-editable HTML source and performs
  `job.html(..., reports=...)` at that source point.
* HTML-DEFER captures a per-ID `CSVReport` object when executed and
  defers CSV reads until layout execution. Large schema arguments are stored
  in `html/report_<block>.report.json`, reread when the report is defined.
  Keep COLUMN-DATA, COLUMN-HEADERS and COLUMN-ALIGNMENT separate.
* HTML-LAYOUT uses the single `render_html` function and external HTML
  shell; output CSS may be linked or embedded and rereads the edited source.
  Required missing CSS now fails instead of silently synthesizing an unrelated
  stylesheet. Explicit relative CSS resolves against the workdir's earlier
  HTML-RUN result, then the generated project asset root, then the HTML
  template's sibling path.
* HTML-DELETE uses `job.delete_html()` to remove only consumed reports;
  stylesheet selection and unconsumed reports persist. This is a corrected
  ScriptHost-grounded semantic deviation from old vg2c.
* TYPE HTMLI5/chart/report distribution, unsafe arbitrary interpolation and
  unknown report modes remain unsupported. TYPE KEY is a CSV design-table
  header and **not** an executable report type.
* Emitted script source offsets and editing metadata follow the preexisting
  `StepEmission`/`EmittedParameter` flow; SQL/crosstab implementation was
  untouched.

### Evidence and quantitative readability

CI at `59fed15c1fd5e44a3fbd45d052c53c924cbaaba6`
([run 38106469080](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38106469080)):
163 focused passed, 614 full-suite passed, the **same six inherited UI/JMP
failures**. Compilation, scoped Ruff F821/F823, original Session 03 benchmarks,
and connected offline SQL→crosstab→CTARRAY→downstream SQL→HTML tests passed.
This is clean Linux **test** evidence, not original ScriptHost differential proof.

Read-only compile-only ICMPCS sample comparison in that CI:

| Metric | Older checked-in generated ICMPCS/main.py | Re-emitted Session 04 |
| --- | ---: | ---: |
| Physical lines | 748 | 137 |
| AST simple statements | 75 | 59 |
| Longest simple statement (characters) | 3028 | 748 |
| Inline style assignments | 9 | 0 |
| CSS-file assignments | 4 | 0 |
| Separately editable emitted assets | Not comparable from older main alone | 8 SQL, 2 HTML, 1 CSS, 1 HTML report JSON |

These compare checked-in older generated output against a *fresh compilation*
of the same source, without executing the original ICMPCS job or its private
network dependencies. They are readability measurements, not end-to-end
source-engine parity evidence.

### Remaining source-grounded parity gaps

1. `Check_Column_Pattern` supports runtime STARTS WITH, ENDS WITH, CONTAINS
   and percent-pattern selection against *actual CSV headers*, including
   presentation-header and alignment expansion. The current direct emitter
   still emits a template with a fixed number of HTML header slots; dynamic
   column cardinality, report redefinitions with different header counts,
   and all original COLUMN-FORMAT width/type rules are **not fully supported**.
2. Static CSS from immutable FORMAT rows is externalized; macro-dependent
   CSS *declaration values* and dynamic CSS output names are unverified or
   diagnosed as unsupported. Conditional choice of **static** CSS assets
   and per-job style isolation are tested.
3. Original source's blank/missing CSS fallback, absent/empty CSV error and
   invalid formatting paths have intentional safer diagnostics; exact original
   warning/error text and all UTF-8/meta charset behavior are unverified.
4. HTML-I5, interactive HTMLI/HTMIC, chart/JMP/JSL, distribution, JS and
   SharePoint/Outlook modes have no general stand-in here and must not be
   described as supported or differential-tested.
5. The old `utilities/html_report.py` renderer still exists for historic
   and UI-preview callers. It must not become a second authority for
   generated project rendering. Session 05 may remove/bridge only after
   verifying all callers and inherited UI tests.

No independent execution of proprietary ScriptHost against the synthetic
fixtures was done. SOURCE-OBSERVED and SOURCE-DERIVED labels remain
separate from ENGINE-DIFFERENTIAL, which has no evidence here.
