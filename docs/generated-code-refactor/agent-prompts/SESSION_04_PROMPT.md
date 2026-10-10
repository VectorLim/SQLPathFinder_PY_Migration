# Session 04 agent prompt — HTML/CSS/SQL editable assets

## Role and fixed constraints

You are a fresh coding agent working on `VectorLim/SQLPathFinder_PY_Migration`. Implement **only Session 04** from the approved, committed plans. Read `docs/generated-code-refactor/00_MASTER_PLAN.md`, `docs/generated-code-refactor/04_HTML_CSS_SQL_OUTPUT.md`, and `docs/generated-code-refactor/agent-prompts/README.md` in full. Treat plans as inspected design guidance, not permission to contradict actual source. The primary objective is readable user-editable generated Python, with no behavioral regressions or needless framework complexity. Do not change the UI/frontend or implement unsupported HPC/JMP/JSL and unrelated AED infrastructure. Do not merge into `main`, `html-and-sql-rework` or other branches; Session 02 creates the shared branch and Sessions 03–05 continue it.

## Shared implementation branch workflow — Session 04

- **Predecessor:** final pushed Session 03 commit on `origin/refactor/generated-code/implementation`. Read `docs/generated-code-refactor/handoffs/SESSION_03_HANDOFF.md` and the previous agent's final response for its full pushed SHA. This SHA may not be embedded in the handoff because of Git self-reference; verify it matches the fetched remote HEAD. If unverified, stop.
- **Work branch:** `refactor/generated-code/implementation`; continue it at the preceding session HEAD.
- Verify `git remote -v`, `git fetch origin --prune`, current remote HEAD, ancestry, `git status --porcelain` and local branches/worktrees. Never reset hard, clean destructively, force-push, overwrite dirty changes, or merge implicitly.
- **Continue `refactor/generated-code/implementation`** at the predecessor's verified remote HEAD. No new branch, no merge or rebase. Fast-forward a clean local checkout or use a new clean worktree. If the branch diverged or the previous handoff is incomplete, stop before editing. Confirm the updated master and prompt files from Session 02's documentation synchronization are present locally; if not, reconcile only the affected Markdown with `origin/html-and-sql-rework`, without merging unrelated history.
- Record starting SHA, baseline focused/full tests and any environmental blockers **before** implementation. Preserve session scope.

## Scoped implementation mandate

Finish external asset separation without duplicating the existing HTML renderer. Inspect `emitter/project.py`, `runtime/{html,html_format,query}.py`, `utilities/html_report.py`, `ICMPCS/html/report_055.html` and current direct HTML tests. Existing SQL and HTML files already work and CSS file linking/copying is partly supported. Generate independent CSS assets, eliminate giant `styles[...]` declarations and lengthy report/template literals from `main.py`. Respect HTML-RUN/HTML-DELETE style-state order, dynamic substitutions, secure escaping, unique asset paths, read-after-user-edit behavior, report schema from Session 03, linked vs `CSSEMBED` inline CSS, nonempty-project safety and relocatable Linux paths. Preserve SQL text and backend query semantics. Avoid Jinja or a framework unless a proven blocker demands it.

Work bottom-up from tests and authoritative runtime semantics to emission. Reuse existing code before adding helpers. Preserve `EmittedScript.assets`, `StepEmission`, editable parameters, source offsets and source-location errors. Preserve direct execution without VG2 input or ScriptHost imports. Use AST/source inspection and representative generated output to catch noise, not cosmetic line-count tricks. Keep public API compact, state explicit, source comments factual and file paths safe across Linux/container deployments.

## Verification and change discipline

Focus on `tests/runtime/{test_direct_html,test_html_report,test_direct_runtime}.py`, `tests/emitter/{test_generated_project,test_emission_metadata}.py`, plus full suite. Test stylesheet edits on rerun, linked/embedded modes, multiple conditional style states and HTML escaping and relocation.

Compare old vs new behavior using deterministic sanitized fixtures and stubs for unavailable external dependencies; `ICMPCS/main.py` is a readability reference, **not** safe for unmocked execution. Run focused tests repeatedly and the complete baseline suite before declaring this session done. Never remove or soften a behavioral assertion merely to make the new design pass. Include representative generated-code before/after excerpts and readability measures; identify any moved complexity rather than calling it removed.

## Commit, push and handoff

1. Before completion, review the diff for scope creep, semantic duplication, dead code, formatting, and accidental secrets or private production output.
2. Create `docs/generated-code-refactor/handoffs/SESSION_04_HANDOFF.md` **on the shared implementation branch** with: repository + branch; exact parent SHA; exact committed result SHA (if recording the commit SHA in-file would require self-reference, store the final SHA in your final response and record the prior implementation SHA in-file); implementation summary/files; public runtime and emitter contract; measured generated examples; precise executed test commands and results; limitations and required next-session constraints. Keep it versioned along with the work.
3. Commit and push **only `refactor/generated-code/implementation`**, with normal fast-forward and no force. Record failures as WIP. Verify fetched remote HEAD equals your final pushed SHA, and compare the **exact starting SHA** to the final SHA rather than comparing the shared branch ref to itself.
4. Report the exact full final pushed SHA and hand off `refactor/generated-code/implementation` at that SHA. Do not automatically start another agent session or merge anything.

## Acceptance gate

The concrete acceptance criteria in `04_HTML_CSS_SQL_OUTPUT.md` and the shared master regression contract must be met or explicitly marked incomplete. Prior session behavior must continue to pass. Finish with a concise outcome: changes, results, generated-code example, risks, branch, full SHA and next-step handoff.
