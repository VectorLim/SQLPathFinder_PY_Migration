# Session 04 — HTML/CSS/SQL asset separation and report readability

**Status:** Plan only. **Parent:** tested final Session 03 SHA on `origin/refactor/generated-code/implementation`. Continue that same branch without creating an `s04` branch. Session 05 inherits this session's tested HEAD. Preserve Session 03 SQL/table semantics.

## Objective / scope

Keep business logic in generated Python while moving report content and **static styles** into standalone editable `.html` and `.css` assets. Preserve existing external `.sql` files and improve their reference/use if needed; do not redesign the working SQL engine. Eliminate generated giant `styles[...]` initialization, long HTML snippets in Python string literals, avoidable `csv_report(columns=[...], headers=[...], alignment=[...])` declarations and repetitive dynamic template substitution statements. Maintain per-report styles, runtime-dependent placeholders, intended CSS embedding mode, HTML escaping/safety, and editable/reloadable assets.

## Source paths to inspect

`src/vg2c/emitter/project.py`: `_table`, `_report_options`, inner `html(block, macros)` ~340–440. `HTML-RUN` currently appends Python `styles[...]` for FORMAT rows and `css_file = ...`; `HTML-DELETE` clears three runtime variables; `HTML-DEFER` emits potentially huge `csv_report(... columns..., headers..., alignment...)`; `HTML-LAYOUT` builds HTML in `assets` and passes reports, styles, macro values, output paths, `embed_css`. Inspect `src/vg2c/utilities/html_report.py` for authoritative supported options and report format rows; `src/vg2c/runtime/html.py`: `CSVReport`, `csv_report`, `_rows`, `_validate_slots`, `render_html`; `runtime/html_format.py`: `build_css`, `parse_alignment`, `format_cell`; `runtime/values.py`: substitutes; `runtime/query.py` for SQL asset links (avoid duplicate string rules); `src/vg2c/__init__.py` and `project_paths.py` for generated project write contracts.

Inspect `ICMPCS/html/report_001.html` and `report_055.html`: latter contains `<style type="text/css">...` and placeholders `${MYREPORT3_HEADER_1}` through `${MYREPORT3_HEADER_26}` plus `${MYREPORT3_ROWS}`. The generated `ICMPCS/main.py` begins with approximately 100 lines of CSS initialization and includes long report parameters. Inspect tests `tests/runtime/{test_direct_html,test_html_report,test_placeholder_substitution}.py`, `tests/emitter/{test_generated_project,test_emission_metadata,test_no_vg2c_leak}.py`, `tests/ui/test_html_preview.py` **only as a contract guard, not a UI implementation task**. README documents existing stylesheet copy-on-render and `CSSEMBED` embedding behavior.

## Existing capabilities and hazards

`render_html` already resolves a relative `css_file` beside the HTML shell, copies it beside the output and links it as `<link ... href=...>`, or embeds its contents for `embed_css=True`. If no CSS asset exists it builds CSS from styles. Raw CSS with unresolved `${...}` or `<<<...>>>` is rejected, as is unsafe closing `</style`. Do **not** create a second CSS renderer. HTML uses `string.Template` and validates allowed slots/contexts with an HTML parser; it escapes substituted CSV data and headers while preserving authored markup. CSS file and report shell must be re-read after user edits without recompilation. Report output may use runtime-generated name and fallback `email:` logic. `HTML-DELETE` resets current definitions; `HTML-RUN FORMAT` can change style **during execution**, so different report instances or branches may legitimately see different CSS.

`CSS` option and `CSSEMBED` may be explicit source requirements. Even when styles live in separate source assets, some outgoing HTML must embed CSS; this is **render-time behavior**, not a reason to return CSS to `main.py`. Raw authored HTML may include inline `<style>` and `style=` attributes; only externalize authored static CSS when output equivalence can be verified and style precedence is preserved. Do not "sanitize" away supported markup or weaken slot validation.

## Proposed asset contract

```text
<generated-project>/
  main.py                       # Control flow and calls only
  sql/query_*.sql               # Existing external, editable SQL, unchanged
  html/report_*.html            # Existing HTML shells/templates (possibly also report definitions)
  styles/report_*.css           # Emitted static style state, link/copy/embedding as requested
  output/                       # Runtime-generated CSV/HTML/CSS copies and transients
```

Do not impose this exact naming if a lower-friction relative layout respects current contract; `styles/` could be `html/styles/` if that removes path rewrites. The chosen scheme must be explicitly reproducible and survive relocating the full generated directory and using external `run(workdir=...)`.

`job.html("html/report_055.html", output="revision.htm", css="styles/report_055.css")` is the desired readable shape when CSS is materialized. CSS should be authored/external. A generated report with dynamic CSV selection can register a small `job.report(...)` call or reference an adjacent compact, human-editable report specification **only if** `columns/headers/alignment` cannot be inferred with proven identical semantics. Avoid a JSON/YAML framework for every simple report. Keep report ID and dynamic data-input dependencies visible in main code if needed for editing. Do not place user-defined HTML/CSS content back in multiline Python strings.

**Template philosophy:** use existing `${SLOT}` / `Template` mechanisms; no Jinja or new templating runtime unless a concrete unimplementable requirement is demonstrated. Runtime macro substitutions must remain deferred. Escaping rules must be context-appropriate; do not substitute raw untrusted values into tags/attributes or CSS. Keep SQL files external and preserve lexical text/comments/newlines. Don't generate HTML and SQL filenames that collide or depend on random order.

## Ordered implementation

1. Verify Session 03 HEAD/status and baseline tests; enumerate `HTML-RUN`, `HTML-DEFER`, `HTML-LAYOUT`, `HTML-DELETE` supported sequences and source-activated options. Compare sample `ICMPCS` layout and current stylesheet rendering (link vs embed).
2. Build a **style-state timeline**: at each HTML-LAYOUT identify styles/CSS in force, reset points, potential conditional branches and runtime-dependent names. Static compilation may produce one css asset per unique layout/state *when known*; if state is dynamic, keep a small runtime style mechanism and emit only state changes, not a giant block per report. Preserve per-run state isolation from Session 02.
3. Add source asset generation in the existing `emitter/project.py` asset bundle; use `html_format.build_css` logic as a single authoritative function (adjust ownership if compiler-free runtime import boundary requires it, avoiding duplicate formatting rules). Move static inline style blocks from synthesized HTML to CSS only after CSS cascade and formatting are tested. For user-authored inline styles, preserve exact raw markup or migrate with verified equivalent precedence.
4. Confirm HTML shells refer to external `.css` correctly and that `render_html`'s existing copy/link/embed behavior is reused. Allow user-edited CSS to take effect at next run; missing CSS must receive a clear source/path diagnostic rather than silently generate an unintended default when the asset should have been emitted.
5. Replace generated `styles[...]` setup with minimal runtime style load/reference. Preserve HTML-RUN/HTML-DELETE reset semantics and conditional changes. Move bulky report configuration to a compact report definition adjacent to HTML template only where Session 03 decided fields cannot be inferred; otherwise omit redundant defaults. Keep a single documented place to edit report headers and format.
6. Keep SQL asset emission and runtime rereading stable; ensure all generated SQL/HTML/CSS assets appear atomically and the existing nonempty-project guard prevents overwriting user edits. Add multi-job asset naming collision checks and manifest completeness tests. Preserve `EmittedScript.assets`, `StepEmission` and invocation metadata; update off-main report references accordingly.
7. Test HTML escaping, malformed slots/headers, dynamic output paths/instance, linked vs embedded CSS, missing/malformed CSS, literal `$` escaping, Unicode, conditional style timeline, multiple reports, report styles reset, workdir override, asset directory relocation, and direct Linux execution.
8. Run focused/full suites; compare normalized HTML DOM structure/content and CSS rules against baseline. Record regenerated `ICMPCS/main.py` size and remaining unavoidable declarative arguments, exact SHA and caveats for 05.

## Important failure cases

- `CSSEMBED=Y` must still embed CSS in output HTML; link mode must have a valid relative href and actual copied file in output folder.
- Editing an emitted stylesheet or HTML shell and rerunning without recompilation changes output predictably; report doesn't overwrite its source shell.
- Literal authored markup, escaped data, HTML slot structure validation and no raw HTML injection all survive.
- Runtime placeholders in report title, table source path, headers or output path resolve at the correct time, using same scoped macro snapshot that existed at the source report step.
- Per-report style states and successive HTML-DELETE boundaries do not leak across reports/jobs.
- Multi-report generated jobs don't copy all styles into one global CSS or overwrite similarly named assets.
- SQL file text, execution order and error provenance remain unchanged; no inline SQL reintroduced.
- CSS-specific formatting such as column alignment follows user/legacy declarations when specified, rather than a misleading default.

## Acceptance criteria

Main Python contains no long static CSS declarations or report HTML bodies, SQL/HTML/CSS are directly editable, emitter reuses current file-aware renderer, all assets survive deployment/relocation, dynamic behavior remains runtime-bound and existing supported HTML/SQL tests plus complete suite pass. No speculative template framework or duplicated CSS formatter. Defer only final consolidation/dead code pruning to 05, not core correctness.

## Out-of-scope / handoff

No UI preview implementation, extra email delivery/browser/security/chart integration, HPC/JMP or SQL backend rewrite. The following must be handed to Session 05: exact SHA, generated asset layout/spec schema, style-state decisions, all parity tests, benchmark/readability metrics, and a list of only those old helpers whose callers were positively audited. Do not leave unresolved CSS embedding or output-path semantics for integration.