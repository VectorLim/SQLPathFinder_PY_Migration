# Session 03 agent prompt — CSV/table semantics and selective pandas

## Role and fixed constraints

You are a fresh coding agent working on `VectorLim/SQLPathFinder_PY_Migration`. Implement **only Session 03** from the approved, committed plans. Read `docs/generated-code-refactor/00_MASTER_PLAN.md`, `docs/generated-code-refactor/03_CSV_TABLE_PANDAS.md`, and `docs/generated-code-refactor/agent-prompts/README.md` in full. Treat plans as inspected design guidance, not permission to contradict actual source. The primary objective is readable user-editable generated Python, with no behavioral regressions or needless framework complexity. Do not change the UI/frontend or implement unsupported HPC/JMP/JSL and unrelated AED infrastructure. Do not merge into `main`, `html-and-sql-rework` or other branches; Session 02 creates the shared branch and Sessions 03–05 continue it.

## Shared implementation branch workflow — Session 03

- **Predecessor:** final pushed Session 02 commit on `origin/refactor/generated-code/implementation`. Read `docs/generated-code-refactor/handoffs/SESSION_02_HANDOFF.md` and the previous agent's final response for its full pushed SHA. This SHA may not be embedded in the handoff because of Git self-reference; verify it matches the fetched remote HEAD. If unverified, stop.
- **Work branch:** `refactor/generated-code/implementation`; continue it at the preceding session HEAD.
- Verify `git remote -v`, `git fetch origin --prune`, current remote HEAD, ancestry, `git status --porcelain` and local branches/worktrees. Never reset hard, clean destructively, force-push, overwrite dirty changes, or merge implicitly.
- **Continue `refactor/generated-code/implementation`** at the predecessor's verified remote HEAD. No new branch, no merge or rebase. Fast-forward a clean local checkout or use a new clean worktree. If the branch diverged or the previous handoff is incomplete, stop before editing. Confirm the updated master and prompt files from Session 02's documentation synchronization are present locally; if not, reconcile only the affected Markdown with `origin/html-and-sql-rework`, without merging unrelated history.
- Record starting SHA, baseline focused/full tests and any environmental blockers **before** implementation. Preserve session scope.

## Scoped implementation mandate

Inspect VG2/ScriptHost semantics and the current `runtime/{csv_io,crosstab,sqlite_reader,query}.py`, `utilities/{sqlite_engine,_emit_helpers}.py`, and `emitter/project.py`. Produce a table for each relevant `/HEADERS`, CSV column, crosstab, table-join, HTML display-header and alignment option: data transformation vs schema identity vs presentation vs inferable defaults. **Preserve downstream SQL references to CSV-renamed headers** and crosstab row/header/value keys. pandas is *already* present; use it selectively where it eliminates real complexity, but retain SQLite for SQL joins and its semantics. Test duplicate columns/keys, nulls, column order, types and leading zeros, case sensitivity, aliases, CSV quoting, `SQL_Get_CSV_List`, and `CrossTab->[[...]]`. Move verbose static table metadata into one discoverable editable contract only when needed; avoid opaque registries, implicit renames and silent schema shifts.

Work bottom-up from tests and authoritative runtime semantics to emission. Reuse existing code before adding helpers. Preserve `EmittedScript.assets`, `StepEmission`, editable parameters, source offsets and source-location errors. Preserve direct execution without VG2 input or ScriptHost imports. Use AST/source inspection and representative generated output to catch noise, not cosmetic line-count tricks. Keep public API compact, state explicit, source comments factual and file paths safe across Linux/container deployments.

## Verification and change discipline

Focus on `tests/runtime/{test_csv_io,test_direct_runtime,test_e2e_fixtures}.py`, `tests/emitter/{test_generated_project,test_sqlite_table_bindings}.py`, plus prior sessions/full suite. Add before/after downstream SQL using renamed headers and goldens for crosstab and duplicate/null/type behavior.

Compare old vs new behavior using deterministic sanitized fixtures and stubs for unavailable external dependencies; `ICMPCS/main.py` is a readability reference, **not** safe for unmocked execution. Run focused tests repeatedly and the complete baseline suite before declaring this session done. Never remove or soften a behavioral assertion merely to make the new design pass. Include representative generated-code before/after excerpts and readability measures; identify any moved complexity rather than calling it removed.

## Commit, push and handoff

1. Before completion, review the diff for scope creep, semantic duplication, dead code, formatting, and accidental secrets or private production output.
2. Create `docs/generated-code-refactor/handoffs/SESSION_03_HANDOFF.md` **on the shared implementation branch** with: repository + branch; exact parent SHA; exact committed result SHA (if recording the commit SHA in-file would require self-reference, store the final SHA in your final response and record the prior implementation SHA in-file); implementation summary/files; public runtime and emitter contract; measured generated examples; precise executed test commands and results; limitations and required next-session constraints. Keep it versioned along with the work.
3. Commit and push **only `refactor/generated-code/implementation`**, with normal fast-forward and no force. Record failures as WIP. Verify fetched remote HEAD equals your final pushed SHA, and compare the **exact starting SHA** to the final SHA rather than comparing the shared branch ref to itself.
4. Report the exact full final pushed SHA and hand off `refactor/generated-code/implementation` at that SHA. Do not automatically start another agent session or merge anything.

## Acceptance gate

The concrete acceptance criteria in `03_CSV_TABLE_PANDAS.md` and the shared master regression contract must be met or explicitly marked incomplete. Prior session behavior must continue to pass. Finish with a concise outcome: changes, results, generated-code example, risks, branch, full SHA and next-step handoff.
