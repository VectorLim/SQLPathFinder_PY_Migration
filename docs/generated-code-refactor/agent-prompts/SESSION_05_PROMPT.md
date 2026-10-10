# Session 05 agent prompt — Integration, cleanup and Linux validation

## Role and fixed constraints

You are a fresh coding agent working on `VectorLim/SQLPathFinder_PY_Migration`. Implement **only Session 05** from the approved, committed plans. Read `docs/generated-code-refactor/00_MASTER_PLAN.md`, `docs/generated-code-refactor/05_FINAL_INTEGRATION.md`, and `docs/generated-code-refactor/agent-prompts/README.md` in full. Treat plans as inspected design guidance, not permission to contradict actual source. The primary objective is readable user-editable generated Python, with no behavioral regressions or needless framework complexity. Do not change the UI/frontend or implement unsupported HPC/JMP/JSL and unrelated AED infrastructure. Do not merge into `main`, `html-and-sql-rework` or other branches; Session 02 creates the shared branch and Sessions 03–05 continue it.

## Shared implementation branch workflow — Session 05

- **Predecessor:** final pushed Session 04 commit on `origin/refactor/generated-code/implementation`. Read `docs/generated-code-refactor/handoffs/SESSION_04_HANDOFF.md` and the previous agent's final response for its full pushed SHA. This SHA may not be embedded in the handoff because of Git self-reference; verify it matches the fetched remote HEAD. If unverified, stop.
- **Work branch:** `refactor/generated-code/implementation`; continue it at the preceding session HEAD.
- Verify `git remote -v`, `git fetch origin --prune`, current remote HEAD, ancestry, `git status --porcelain` and local branches/worktrees. Never reset hard, clean destructively, force-push, overwrite dirty changes, or merge implicitly.
- **Continue `refactor/generated-code/implementation`** at the predecessor's verified remote HEAD. No new branch, no merge or rebase. Fast-forward a clean local checkout or use a new clean worktree. If the branch diverged or the previous handoff is incomplete, stop before editing. Confirm the updated master and prompt files from Session 02's documentation synchronization are present locally; if not, reconcile only the affected Markdown with `origin/html-and-sql-rework`, without merging unrelated history.
- Record starting SHA, baseline focused/full tests and any environmental blockers **before** implementation. Preserve session scope.

## Scoped implementation mandate

Perform the final cross-session convergence on the Session 04 parent: audit all touched compiler/runtime utilities, callers and tests; identify redundant implementations, legacy facades, forwarding wrappers, unused imports/dependencies and duplicate semantic owners **with proof before deletion**. Verify emitted `main.py` readability, dynamic macro semantics, native control flow, schema/CSV/pivot/join/SQL and HTML/CSS parity, editing/source offsets, independent runs and direct execution with installed Linux `vg2c` (no original VG2/ScriptHost). Run all focused and full tests and a representative end-to-end differential harness, report hard blockers honestly (e.g. unavailable proprietary services). Update documentation and examples and provide measured before/after emitted metrics and runtime complexity audit. Do not expand feature scope, redesign architecture, weaken tests, touch frontend, or merge branches.

Work bottom-up from tests and authoritative runtime semantics to emission. Reuse existing code before adding helpers. Preserve `EmittedScript.assets`, `StepEmission`, editable parameters, source offsets and source-location errors. Preserve direct execution without VG2 input or ScriptHost imports. Use AST/source inspection and representative generated output to catch noise, not cosmetic line-count tricks. Keep public API compact, state explicit, source comments factual and file paths safe across Linux/container deployments.

## Verification and change discipline

Re-run all focused tests and `python -m pytest -q` or project-supported `uv run pytest -q`, compileall, configured Ruff, and installed Linux wheel/runtime probes. Compare controlled before/after generated jobs (CSV, HTML/CSS, SQL, exceptions, side effects, files) and record results.

Compare old vs new behavior using deterministic sanitized fixtures and stubs for unavailable external dependencies; `ICMPCS/main.py` is a readability reference, **not** safe for unmocked execution. Run focused tests repeatedly and the complete baseline suite before declaring this session done. Never remove or soften a behavioral assertion merely to make the new design pass. Include representative generated-code before/after excerpts and readability measures; identify any moved complexity rather than calling it removed.

## Commit, push and handoff

1. Before completion, review the diff for scope creep, semantic duplication, dead code, formatting, and accidental secrets or private production output.
2. Create `docs/generated-code-refactor/handoffs/SESSION_05_HANDOFF.md` **on the shared implementation branch** with: repository + branch; exact parent SHA; exact committed result SHA (if recording the commit SHA in-file would require self-reference, store the final SHA in your final response and record the prior implementation SHA in-file); implementation summary/files; public runtime and emitter contract; measured generated examples; precise executed test commands and results; limitations and required next-session constraints. Keep it versioned along with the work.
3. Commit and push **only `refactor/generated-code/implementation`**, with normal fast-forward and no force. Record failures as WIP. Verify fetched remote HEAD equals your final pushed SHA, and compare the **exact starting SHA** to the final SHA rather than comparing the shared branch ref to itself.
4. Report the exact full final pushed SHA and hand off `refactor/generated-code/implementation` at that SHA. Do not automatically start another agent session or merge anything; stop with the release-gate report.

## Acceptance gate

The concrete acceptance criteria in `05_FINAL_INTEGRATION.md` and the shared master regression contract must be met or explicitly marked incomplete. Prior session behavior must continue to pass. Finish with a concise outcome: changes, results, generated-code example, risks, branch, full SHA and next-step handoff.
