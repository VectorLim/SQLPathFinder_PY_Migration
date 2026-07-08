## Plan: Close HTML Translation Fallouts

Generated flow maps HTML-RUN/LAYOUT/DELETE blocks from [tests/fixtures/actual_script.txt](tests/fixtures/actual_script.txt) into calls in [generated_script.py](generated_script.py), but behavior parity is incomplete. Main fallout: fixture expects CSS-driven styling chain (RUN style metadata + LAYOUT :CSS/:CSSEMBED), while runtime only embeds CSS if external file already exists, and DELETE is no-op. Plan fixes parity with minimal architecture churn and adds regression coverage.

**Steps**
1. Phase 1: Lock expected behavior from fixture pairs
2. Compare first HTML triplet in [tests/fixtures/actual_script.txt](tests/fixtures/actual_script.txt#L4), [tests/fixtures/actual_script.txt](tests/fixtures/actual_script.txt#L81), [tests/fixtures/actual_script.txt](tests/fixtures/actual_script.txt#L112) against emitted calls in [generated_script.py](generated_script.py#L1145), [generated_script.py](generated_script.py#L1148), [generated_script.py](generated_script.py#L1151).
3. Define explicit contract doc in tests/comments for: style source precedence, css embed/link behavior, instance scoping, delete semantics. This step blocks implementation details.
4. Phase 2: Runtime parity fixes in HtmlReport
5. Update RUN parser in [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L79) to retain both FORMAT rules and CSS target metadata from rows near fixture lines [tests/fixtures/actual_script.txt](tests/fixtures/actual_script.txt#L68) and [tests/fixtures/actual_script.txt](tests/fixtures/actual_script.txt#L69).
6. Update LAYOUT parser in [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L98) to keep directive parsing centralized, with known directives at [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L119), [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L121), [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L123).
7. Implement deterministic CSS resolution order: LAYOUT :CSS first, fallback to RUN metadata, then inline synthesized CSS from RUN FORMAT if file absent and embed requested. This resolves current silent skip at [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L130) and [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L132).
8. Add non-embed path: when CSS present and embed is false, inject stylesheet link instead of no-op.
9. Implement HTML-DELETE state cleanup in [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L150) so per-instance caches do not leak.
10. Remove dead import at [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L6).
11. Phase 3: Emission and ownership cleanup
12. Keep current emission wiring unless tests show need for API change: [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L54) and [src/vg2c/emitter/utilities/html_report.py](src/vg2c/emitter/utilities/html_report.py#L62).
13. If needed, follow-up refactor to reduce explicit ctx positional argument pattern only after parity is stable; do not mix with behavior fixes.
14. Phase 4: Regression coverage
15. Add focused runtime tests for HtmlReport run/layout/delete in new test module under tests/runtime.
16. Add fixture-driven emitter assertion that HTML triplet from [tests/fixtures/actual_script.txt](tests/fixtures/actual_script.txt) emits executable calls (not pass stubs) and preserves options.
17. Add scenario tests for: missing css file + embed true, css file present + embed true, css present + embed false, multi-instance run/delete isolation.
18. Add one smoke e2e that executes emitted HTML triplet and validates revision.htm output content and style placement.

**Relevant files**
- c:/Project/SQLPathFinder_PY_Migration/src/vg2c/emitter/utilities/html_report.py — primary runtime and emission parity work.
- c:/Project/SQLPathFinder_PY_Migration/generated_script.py — reference artifact for expected emitted call shape.
- c:/Project/SQLPathFinder_PY_Migration/tests/fixtures/actual_script.txt — source fixture driving fallback analysis.
- c:/Project/SQLPathFinder_PY_Migration/src/vg2c/emitter/models.py — only if fallback behavior needs adjustment after tests.
- c:/Project/SQLPathFinder_PY_Migration/tests/runtime — new HtmlReport runtime tests.
- c:/Project/SQLPathFinder_PY_Migration/tests/emitter — new emission-level tests for HTML triplet.

**Verification**
1. Run targeted tests for HtmlReport runtime module and emitter module.
2. Run existing classifier/dispatch/emitter suites to ensure no regressions in utility routing.
3. Execute fixture translation for actual_script and confirm generated HTML steps still map 1:1 order (RUN -> LAYOUT -> DELETE).
4. Validate produced HTML file exists at fixture-defined target and contains either embedded style block or stylesheet link per directive.

**Decisions**
- In scope: behavior parity for CSS and lifecycle semantics between fixture source and runtime implementation.
- Out of scope (this pass): full semantic implementation of currently ignored LAYOUT directives (:RR, :B, :EM-A, :EM-S, :SEC, :TITLE) beyond preserving safe parsing.
- Recommendation: land parity fixes first, then evaluate deeper legacy directive semantics in separate PR.

**Further Considerations**
1. Choose CSS synthesis format from FORMAT rows. Option A: literal concatenation (fast, lower fidelity). Option B: map known style keys to CSS selectors (higher fidelity). Recommended: Option A first, Option B later if needed.
2. Choose DELETE behavior. Option A: clear only in-memory state (safe default). Option B: also delete generated files (riskier). Recommended: Option A unless legacy spec explicitly requires file deletion.
