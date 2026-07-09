## Plan: HTML Report Runtime Refactor

Keep behavior same as current working output, but cut duplication hard. Reuse existing CSV and file utilities. Keep parser/emitter simple and extensible for more report variants.

**Steps**
1. Phase A: Remove structural duplication in emitter path
2. Replace four near-identical option-to-kwargs loops in HtmlReport emitters with one shared class helper that accepts allowed option keys and optional positional args. Depends on none.
3. Keep generated call shape unchanged for defer/run/layout/delete parity with generated script. Depends on 2.
4. Phase B: Normalize template parsing once
5. Add one internal parser for <\\>-rows, reused by run, _render_report, and layout fallback output-name scan. Depends on none.
6. Add one internal options extractor returning normalized scalar/list values (matching current semantics). Depends on 5.
7. Store parsed defer payload once at defer time (template text + parsed options) to stop reparsing in layout fallback. Depends on 6.
8. Phase C: Reuse project runtime utilities
9. Replace local csv.DictReader path in _render_report with ctx.csv_io.iter when available, then fallback to stdlib for non-PipelineContext callers. Depends on 6.
10. Keep macro substitution through ctx.macro.substitute_sql before read. Depends on 9.
11. Replace direct ctx.macro.write_file call in layout with ctx.write_file when present, fallback to current macro/path behavior only when context lacks it. Parallel with 9.
12. Phase D: Generalize hard-coded parts without behavior drift
13. Convert CSS block assembly in _build_css to declarative mapping (format-name -> selector template + default declarations) so adding new format keys is data-only. Depends on 6.
14. Parse all layout directives into a directives map; only apply current supported keys (FILE/CSS/CSSEMBED/TITLE), preserve unknown directives untouched for future extension. Depends on 5.
15. Keep current HTML placeholder replacement behavior and email:self fallback naming exactly as now. Depends on 7 and 14.
16. Phase E: Test hardening
17. Keep existing five runtime tests as baseline behavior lock.
18. Add tests for: parsed-option reuse in fallback OUTPUT-FILE resolution, ctx.csv_io.iter read path, ctx.write_file usage path, and unknown layout directives non-breaking pass-through. Depends on 9, 11, 14.
19. Add fixture parity assertion against html_test report flow (DEFER x2, RUN, LAYOUT, DELETE). Depends on 18.

**Relevant files**
- c:/Project/SQLPathFinder_PY_Migration/src/vg2c/emitter/utilities/html_report.py — primary refactor target; remove duplication and centralize parsing.
- c:/Project/SQLPathFinder_PY_Migration/src/vg2c/emitter/utilities/csv_io.py — reuse iter for CSV read path.
- c:/Project/SQLPathFinder_PY_Migration/src/vg2c/emitter/utilities/pipeline_context.py — reuse write_file and csv_io access pattern.
- c:/Project/SQLPathFinder_PY_Migration/src/vg2c/emitter/utilities/macro_state.py — preserve substitute_sql and write_file semantics.
- c:/Project/SQLPathFinder_PY_Migration/tests/runtime/test_html_report.py — baseline + new targeted regression tests.
- c:/Project/SQLPathFinder_PY_Migration/generated_script.py — emitted call-shape parity reference (step_0001..step_0005_html_report).
- c:/Project/SQLPathFinder_PY_Migration/tests/fixtures/html_test.txt — VG2 source parity reference for HTML report flow.

**Verification**
1. Run tests/runtime/test_html_report.py.
2. Run tests/runtime/test_csv_io.py.
3. Confirm emitted html_report call sequence still matches generated script ordering and method signatures.
4. Manual parity checks: CSS link/embed branch, HTM placeholder replacement, CE% formatting heuristic, alternating row classes, email:self fallback filename with instance prefix.

**Decisions**
- Include: remove redundancy, increase reuse, preserve output behavior.
- Exclude: JMP path, compatibility shims, semantic changes to currently ignored layout directives.
- Assumption: tests/fixtures/html_test.txt is authoritative VG2 source fixture for this report flow.

**Further Considerations**
1. Extension strategy: keep new parser private to html_report now; extract shared report parser module only if another utility needs <\\>-table parsing.
2. Formatting engine: keep CE%/percent heuristic in this pass; move to COLUMN-FORMAT-driven formatting in a future behavior-change ticket.
3. Performance: optional follow-up cache for rendered deferred report fragments if layout uses same HTM key repeatedly.
