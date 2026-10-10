# Session 02 agent prompt — Runtime context and concise calls

## Role and fixed constraints

You are a fresh coding agent working on `VectorLim/SQLPathFinder_PY_Migration`. Implement **only Session 02** from the approved, committed plans. Read `docs/generated-code-refactor/00_MASTER_PLAN.md`, `docs/generated-code-refactor/02_RUNTIME_CONTEXT.md`, and `docs/generated-code-refactor/agent-prompts/README.md` in full. Treat plans as inspected design guidance, not permission to contradict actual source. The primary objective is readable user-editable generated Python, with no behavioral regressions or needless framework complexity. Do not change the UI/frontend or implement unsupported HPC/JMP/JSL and unrelated AED infrastructure. Do not merge into `main`, `html-and-sql-rework` or other branches; Session 02 creates the shared branch and Sessions 03–05 continue it.

## Shared implementation branch workflow — Session 02

- **Predecessor:** final pushed Session 01 commit on `origin/refactor/generated-code/s01-macros`. Read `docs/generated-code-refactor/handoffs/SESSION_01_HANDOFF.md` and the previous agent's final response for its full pushed SHA. This SHA may not be embedded in the handoff because of Git self-reference; verify it matches the fetched remote HEAD. If unverified, stop.
- **Work branch:** `refactor/generated-code/implementation`; create it from Session 01 HEAD.
- Verify `git remote -v`, `git fetch origin --prune`, current remote HEAD, ancestry, `git status --porcelain` and local branches/worktrees. Never reset hard, clean destructively, force-push, overwrite dirty changes, or merge implicitly.
- **Create the shared branch exactly once** at Session 01's verified final SHA. If `refactor/generated-code/implementation` exists locally/remotely, inspect and stop instead of overwriting it. No Session 02-specific branch. **Critical docs:** revised plans/prompts were pushed to `origin/html-and-sql-rework` after Session 01 started. Before editing source, read the latest master/README/Session 02 prompt from that branch (`git show origin/html-and-sql-rework:docs/generated-code-refactor/agent-prompts/SESSION_02_PROMPT.md`). Compare and import **only** the revised master, Sessions 02–05 plan Markdown, README and Sessions 02–05 prompt Markdown into the new branch. Do not merge the planning branch or overwrite Session 01's work; commit this synchronization on the shared branch.
- Record starting SHA, baseline focused/full tests and any environmental blockers **before** implementation. Preserve session scope.

## Scoped implementation mandate

Replace repeated `workdir=workdir`, `values=job_values`, and `macros=...` in generated business statements with the smallest explicit **per-run** runtime instance that has asset root, work root, global snapshot, scoped macros and minimal report state. Compare bounded methods with alternatives before deciding. Inspect `runtime/{values,files,query,html,controls,aed}.py`, `runtime/__init__.py`, `emitter/project.py` and `tests/runtime/test_direct_runtime.py`. Prefer reuse/delegation to real existing implementations rather than new service layers. Keep SQL reader/node/bind/inputs explicit where their choice affects execution, and ensure `run(workdir=override)`, project relocation, independent jobs, no `chdir`/process globals, pure runtime-package imports, direct Python editing and invocation-source offsets. Do not rewrite crosstab, pandas or CSS in this session.

Work bottom-up from tests and authoritative runtime semantics to emission. Reuse existing code before adding helpers. Preserve `EmittedScript.assets`, `StepEmission`, editable parameters, source offsets and source-location errors. Preserve direct execution without VG2 input or ScriptHost imports. Use AST/source inspection and representative generated output to catch noise, not cosmetic line-count tricks. Keep public API compact, state explicit, source comments factual and file paths safe across Linux/container deployments.

## Verification and change discipline

Focus on `tests/runtime/{test_direct_runtime,test_e2e_fixtures,test_control_parity,test_generated_symbols}.py`, `tests/emitter/{test_generated_project,test_emission_metadata,test_editing,test_no_vg2c_leak}.py`, plus full suite. Add independent concurrent per-run state, relocation, two output roots and asset reread cases.

Compare old vs new behavior using deterministic sanitized fixtures and stubs for unavailable external dependencies; `ICMPCS/main.py` is a readability reference, **not** safe for unmocked execution. Run focused tests repeatedly and the complete baseline suite before declaring this session done. Never remove or soften a behavioral assertion merely to make the new design pass. Include representative generated-code before/after excerpts and readability measures; identify any moved complexity rather than calling it removed.

## Commit, push and handoff

1. Before completion, review the diff for scope creep, semantic duplication, dead code, formatting, and accidental secrets or private production output.
2. Create `docs/generated-code-refactor/handoffs/SESSION_02_HANDOFF.md` **on the shared implementation branch** with: repository + branch; exact parent SHA; exact committed result SHA (if recording the commit SHA in-file would require self-reference, store the final SHA in your final response and record the prior implementation SHA in-file); implementation summary/files; public runtime and emitter contract; measured generated examples; precise executed test commands and results; limitations and required next-session constraints. Keep it versioned along with the work.
3. Commit and push **only `refactor/generated-code/implementation`**, with normal fast-forward and no force. Record failures as WIP. Verify fetched remote HEAD equals your final pushed SHA, and compare the **exact starting SHA** to the final SHA rather than comparing the shared branch ref to itself.
4. Report the exact full final pushed SHA and hand off `refactor/generated-code/implementation` at that SHA. Do not automatically start another agent session or merge anything.

## Acceptance gate

The concrete acceptance criteria in `02_RUNTIME_CONTEXT.md` and the shared master regression contract must be met or explicitly marked incomplete. Prior session behavior must continue to pass. Finish with a concise outcome: changes, results, generated-code example, risks, branch, full SHA and next-step handoff.
