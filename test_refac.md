## Plan: Refresh Frontend Test Module

Refresh only frontend tests to align with current parser/classifier behavior, remove brittle/legacy assertions, and increase realistic coverage using real fixtures with stable key expectations (not full-sequence snapshots for large fixtures).

**Steps**
1. Phase 1 — Baseline + scope lock
2. Confirm frontend contract boundaries from source: parse/classify public API in c:\Project\SQLPathFinder_PY_Migration\src\vg2c\frontend\parser.py and c:\Project\SQLPathFinder_PY_Migration\src\vg2c\frontend\classifier.py; classification semantics sourced from utility checks in c:\Project\SQLPathFinder_PY_Migration\src\vg2c\emitter\utilities\*.py. This is read-only and blocks all test rewrites.
3. Confirm fixture set for this pass (representative + stable): script_short.txt, script_another.txt, sql_script.txt, actual_script.txt, plus focused oracle-node fixtures oasys.txt and aries_simple.txt. *parallel with step 4 planning, blocks implementation*
4. Phase 2 — Rewrite parser tests (tests/frontend/test_parser.py)
5. Remove redundant synthetic tests that assert identical body-preservation mechanics across SQL/Python/CSV/HTML with near-duplicate structure.
6. Keep/modernize one compact synthetic test each for parser-only edge behavior that fixtures do not isolate well: separator whitespace, inline-options diagnostic path, unclosed-options error path, duplicate option key diagnostic path, and source-span monotonicity.
7. Add fixture-driven parser assertions for realistic behavior: block counts, key option extraction, and expected diagnostics shape (including actual_script leading-separator empty-block warning behavior).
8. Phase 3 — Rewrite classifier tests (tests/frontend/test_classifier.py)
9. Replace brittle implementation-detail checks (notably utility registry class-name ordering) with behavior-level precedence tests.
10. Cover classification precedence and edge mapping with concise cases: WRITE-FILE .py -> PYTHON_EMBED, WRITE-FILE non-.py -> WRITE_FILE, macro-token UTILITIES -> MACRO_CONTROL, utility command mapping (external/fs/delete/mail), sqlite detection by OLEDB/ENGINE, Oracle SQL detection for MARS/OASYS/ARIES including <<<TOKEN>>> placeholders, unknown fallback and unknown-kind diagnostic.
11. Keep synthetic option blocks only where they directly represent classifier input contract; avoid asserting private/internal sequence assumptions.
12. Phase 4 — Refresh fixture integration tests (tests/frontend/test_fixtures.py)
13. Replace “has kind” shallow checks with explicit stable expectations per fixture:
14. script_short.txt: single SQLITE_QUERY block; key option values (CSV owner.csv, HEADERS owner); no error diagnostics.
15. script_another.txt: three blocks classified as SQL_QUERY, PYTHON_EMBED, EXTERNAL_RUN; preserve expected utility payload signature for Run_Python_Script.
16. sql_script.txt: mixed dialect flow includes SQL_QUERY and SQLITE_QUERY with expected table-list handling in SQLite block.
17. actual_script.txt: architecture-level behavior checks (contains HTML_REPORT, MACRO_CONTROL, FS_DELETE, FS_COPY, EXTERNAL_RUN, UTILITY/mail, SQLITE_QUERY, SQL_QUERY), no UNKNOWN blocks, no error diagnostics, and macro control token coverage for critical branch tokens.
18. oasys.txt + aries_simple.txt: SQL_QUERY classification for Oracle-engine variants.
19. Refactor helper utilities in test_fixtures.py only if duplication clearly reduced (single parse+classify fixture loader and common diagnostic/kind helpers).
20. Phase 5 — Validate only frontend scope
21. Run only frontend tests: .venv\Scripts\python -m pytest -q tests/frontend
22. If failures occur: update tests to match current intended behavior; if a real frontend source defect is exposed, stop and surface issue clearly before any source modification.
23. Report delta: removed obsolete assertions, rewritten tests, added fixture-based behavior guarantees, and any surfaced source-risk notes.

**Relevant files**
- c:\Project\SQLPathFinder_PY_Migration\tests\frontend\test_parser.py — remove redundant parser tests; add fixture-backed parser behavior/diagnostic tests.
- c:\Project\SQLPathFinder_PY_Migration\tests\frontend\test_classifier.py — replace brittle internals with behavior-first classification precedence tests.
- c:\Project\SQLPathFinder_PY_Migration\tests\frontend\test_fixtures.py — add explicit stable expected outcomes for representative fixtures.
- c:\Project\SQLPathFinder_PY_Migration\tests\conftest.py — reuse FIXTURES path fixture; no expected changes unless helper extraction is clearly beneficial.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\frontend\parser.py — reference parser contract and diagnostics behavior (no planned edits).
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\frontend\classifier.py — reference fallback behavior and unknown-kind diagnostic path (no planned edits).
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\emitter\utilities\sqlite_engine.py — reference SQL/SQLite/Oracle node check behavior.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\emitter\utilities\python_embed.py — reference .py WRITE-FILE mapping.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\emitter\utilities\fs_ops.py — reference WRITE_FILE/FS_COPY/FS_DELETE mapping.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\emitter\utilities\macro_state.py — reference macro-control detection.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\emitter\utilities\external.py — reference external-run detection.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\emitter\utilities\html_report.py — reference HTML_REPORT detection.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\emitter\utilities\mail.py — reference mail utility fallback as Kind.UTILITY.
- c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\script_short.txt — sqlite baseline fixture.
- c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\script_another.txt — SQL + Python embed + external run fixture.
- c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\sql_script.txt — mixed Oracle + SQLite fixture.
- c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\actual_script.txt — broad production-like coverage fixture.
- c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\oasys.txt — Oracle OASYS minimal fixture.
- c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\aries_simple.txt — Oracle ARIES minimal fixture.

**Verification**
1. Execute .venv\Scripts\python -m pytest -q tests/frontend
2. Confirm no changes outside tests/frontend (and optional helper-only changes if explicitly justified).
3. Ensure removed tests correspond to obsolete/redundant behavior, and replacement tests cover realistic fixture-driven behavior plus parser/classifier edge cases.
4. Re-check that assertions remain behavior-focused (public parse/classify contract), not internal class registry order or implementation internals.

**Decisions**
- Use stable key expectations for fixture assertions (user preference), not full-sequence snapshots for large fixtures.
- Scope limited to frontend test module in this pass; do not broaden to other test modules.
- No frontend source edits planned unless test rewrite reveals a genuine defect.

**Further Considerations**
1. If fixture files are expected to churn frequently, consider centralizing fixture expectations in a compact data table inside test_fixtures.py to minimize maintenance cost.
2. For actual_script.txt, keep assertions at architecture-signal level (critical kinds + no UNKNOWN/error + key token presence) to avoid brittle fixture-line coupling.
3. If frontend-only pytest still imports problematic modules due package import side effects, we may need test isolation tweaks while staying within frontend test scope.