## Plan: Resolver Test Module Refresh

Refresh only resolver-layer tests so they match current resolver architecture (scope_builder + macro_resolver + resolve orchestration), replace shallow/legacy assertions with fixture-backed behavior checks, and relocate non-resolver SQL macro expander tests out of resolver scope.

**Pre-edit brief**
- Resolver modules under test:
  - c:\Project\SQLPathFinder_PY_Migration\src\vg2c\resolver\__init__.py (resolve orchestration)
  - c:\Project\SQLPathFinder_PY_Migration\src\vg2c\resolver\scope_builder.py (scope tree + structural diagnostics)
  - c:\Project\SQLPathFinder_PY_Migration\src\vg2c\resolver\macro_resolver.py (control payload parsing + scope_id mapping)
  - c:\Project\SQLPathFinder_PY_Migration\src\vg2c\resolver\models.py (payload/scope dataclasses)
- Fixture files to use:
  - c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\script_short.txt
  - c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\script_another.txt
  - c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\sql_script.txt
  - c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\actual_script.txt
  - Optional focused edge fixture: c:\Project\SQLPathFinder_PY_Migration\tests\fixtures\test_long.txt (only if needed for nested macro depth stability)
- Outdated/redundant tests to remove or rewrite:
  - tests/resolver/test_fixtures.py::test_pipeline_runs_end_to_end (too shallow)
  - tests/resolver/test_fixtures.py::test_no_error_diagnostics_on_clean_fixtures (duplicate shallow signal)
  - tests/resolver/test_fixtures.py::test_script_short_flat_scope_and_no_runtime_refs (weak value)
  - tests/resolver/test_macro_resolver.py token-by-token synthetic duplication (rewrite into compact paramized + fixture-backed checks)
  - tests/resolver/test_scope_builder.py duplicated orphan/unclosed patterns can be compacted into parameterized structural diagnostics tests
  - tests/resolver/test_sql_macro_expander.py is not resolver-owned (move to tests/dataflow)
- Behaviors refreshed tests must protect:
  - Correct scope construction for macro/if/else boundaries and leaf placement
  - Correct control payload parsing from real macro-control utilities strings
  - Resolver scope_id assignment for all blocks, including boundary/control indices
  - Resolver diagnostics for malformed/orphan/unclosed control flow
  - Resolver integration contract: parse -> classify -> resolve using real fixtures, with explicit expected outputs for selected fixtures

**Steps**
1. Phase 1 - Scope lock + ownership cleanup
2. Move c:\Project\SQLPathFinder_PY_Migration\tests\resolver\test_sql_macro_expander.py to c:\Project\SQLPathFinder_PY_Migration\tests\dataflow\test_sql_macro_expander.py without broadening assertions, only path/module ownership correction. This is the user-approved scope decision and unblocks resolver-only cleanup.
3. In tests/resolver, keep focus on resolver behavior only (scope_builder, macro_resolver, resolve). Avoid adding dataflow-only assertions in resolver tests.
4. Phase 2 - Shared test helpers for resolver fixture flow
5. Standardize one helper path in tests/resolver for parse+classify+resolve fixture loading (reuse tests/conftest.py FIXTURES). Keep helper minimal and local to resolver tests to reduce repeated boilerplate.
6. Add small traversal helpers only where they remove duplication: all scope nodes, diagnostics-by-code, macro-control blocks by token.
7. Phase 3 - Refresh tests/resolver/test_scope_builder.py
8. Replace repetitive single-purpose tests with grouped behavior tests:
9. Structural happy path from compact synthetic blocks: macro, if/else, loop nesting produces expected node kinds and child placement.
10. Parameterized structural diagnostics for orphan/end and unclosed flows (END-MACRO, END-LOOP, END-IF, ELSE, unclosed START-MACRO/RUN-LOOP/IF-THEN).
11. Malformed block handling test: malformed blocks become leaf + warning malformed-block-skipped.
12. Add one fixture-backed scope-tree test using actual_script.txt validating stable architecture signals (presence of macro + if nodes, nested depth threshold, no orphan/unclosed diagnostics for this fixture).
13. Phase 4 - Refresh tests/resolver/test_macro_resolver.py
14. Convert token parser checks into concise parameterized payload validation for supported control tokens (START-MACRO, END-MACRO, IF-THEN, ELSE, END-IF, ROWS-IN-FILE, RUN-LOOP, END-LOOP) using explicit expected fields.
15. Add fallback/edge checks:
16. Unknown macro token -> warning unknown-macro-control and control_payload None.
17. Invalid RUN-LOOP chunk size -> chunk_size coerced to 0.
18. Missing quoted args -> default empty strings and prompt_off semantics preserved.
19. Add fixture-backed assertions from actual_script.txt for explicit first-occurrence payload values (for example StartMacro macrotmp.csv, RowsInFile ICMPCS_config.csv/CONFIG, IfThen CONFIG/LE/0).
20. Validate scope_id assignment contract on fixture-derived resolved blocks: every block has non-negative scope_id and macro controls map to containing scopes.
21. Phase 5 - Refresh tests/resolver/test_fixtures.py
22. Replace shallow pipeline smoke tests with explicit fixture expectations:
23. script_short.txt: exact single-block resolved structure (program root with one leaf scope, no control payloads, no resolver errors).
24. script_another.txt: no macro scopes; all resolved blocks remain leaf-scoped; no resolver-introduced control payloads.
25. sql_script.txt: resolver preserves SQL body (no SQL macro expansion yet), sql_macro_calls empty at resolver stage, and control payload absence for non-macro blocks.
26. actual_script.txt: assert explicit resolver outputs:
27. Contains expected macro-control token family in resolved payloads.
28. Multiple macro and if scopes exist with meaningful nesting depth.
29. Macro control blocks have typed control_payload values for sampled known lines/tokens.
30. No orphan/unclosed diagnostics for this representative real fixture.
31. Keep expectations stable and behavior-level (avoid brittle full-tree snapshots for very large fixture).
32. Phase 6 - DRY cleanup + legacy test removal
33. Remove legacy tests superseded by stronger fixture-backed coverage.
34. Ensure no repeated assertion patterns across files; use paramization where it improves clarity.
35. Phase 7 - Validation (resolver scope only)
36. Run resolver tests only: .venv\Scripts\python -m pytest -q tests/resolver
37. Run relocated ownership test only: .venv\Scripts\python -m pytest -q tests/dataflow/test_sql_macro_expander.py
38. If failures occur, adjust tests to current intended behavior; if a real resolver bug is detected, report issue clearly before touching resolver source.

**Relevant files**
- c:\Project\SQLPathFinder_PY_Migration\tests\resolver\test_scope_builder.py - rewrite for structural + edge diagnostics coverage with less redundancy.
- c:\Project\SQLPathFinder_PY_Migration\tests\resolver\test_macro_resolver.py - rewrite into parameterized payload/scope behavior tests with fixture-backed assertions.
- c:\Project\SQLPathFinder_PY_Migration\tests\resolver\test_fixtures.py - replace shallow smoke tests with explicit fixture expectations.
- c:\Project\SQLPathFinder_PY_Migration\tests\resolver\test_sql_macro_expander.py - remove from resolver scope by relocation.
- c:\Project\SQLPathFinder_PY_Migration\tests\dataflow\test_sql_macro_expander.py - relocated file destination.
- c:\Project\SQLPathFinder_PY_Migration\tests\conftest.py - reuse FIXTURES fixture path, no behavior changes expected.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\resolver\scope_builder.py - behavior reference only.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\resolver\macro_resolver.py - behavior reference only.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\resolver\__init__.py - orchestration reference only.
- c:\Project\SQLPathFinder_PY_Migration\src\vg2c\resolver\models.py - payload type reference only.

**Verification**
1. Execute .venv\Scripts\python -m pytest -q tests/resolver.
2. Execute .venv\Scripts\python -m pytest -q tests/dataflow/test_sql_macro_expander.py (relocated ownership file only).
3. Confirm changed files are limited to resolver tests plus relocation target; no frontend/emitter/dataflow analyzer source edits.
4. Confirm assertions test resolver outcomes (payloads, scopes, diagnostics), not private implementation details.

**Decisions**
- Use the same refresh pattern from frontend pass: fixture-backed stable expectations + compact targeted synthetic edge tests.
- Keep this pass resolver-focused; no broad dataflow/frontend test rewrites.
- Relocate SQL macro expander test file out of resolver scope per user decision.
- Do not modify src resolver code unless tests expose a real defect; if exposed, surface first.

**Further Considerations**
1. If actual_script.txt expectations prove brittle, prefer sampled explicit payload checkpoints plus invariant counts/ranges over exact full-tree snapshots.
2. If resolver diagnostics include frontend parse/classify diagnostics in integration tests, separate resolver-added diagnostics from upstream diagnostics in assertions.
3. If relocation is blocked by import path tooling, keep a one-line shim file in tests/resolver temporarily and remove in next cleanup step.