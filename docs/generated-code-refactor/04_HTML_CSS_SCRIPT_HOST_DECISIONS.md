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
