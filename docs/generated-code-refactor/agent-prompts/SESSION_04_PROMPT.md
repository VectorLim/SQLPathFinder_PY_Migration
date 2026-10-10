# Session 04 agent prompt — HTML/CSS/SQL editable assets

## Role and fixed constraints

You are a fresh coding agent working on `VectorLim/SQLPathFinder_PY_Migration`. Implement **only Session 04** from the approved, committed plans. Read `docs/generated-code-refactor/00_MASTER_PLAN.md`, `docs/generated-code-refactor/04_HTML_CSS_SQL_OUTPUT.md`, and `docs/generated-code-refactor/agent-prompts/README.md` in full. Treat plans as inspected design guidance, not permission to contradict actual source. The primary objective is readable user-editable generated Python, with no behavioral regressions or needless framework complexity. Do not change the UI/frontend or implement unsupported HPC/JMP/JSL and unrelated AED infrastructure. Do not merge into `main`, `html-and-sql-rework` or another session branch.

## Exact branch workflow — this session owns a fresh temporary branch

- Parent remote ref: `origin/refactor/generated-code/s03-tables`.
- New temporary branch: `refactor/generated-code/s04-assets`.
- Read `docs/generated-code-refactor/handoffs/SESSION_03_HANDOFF.md` from the predecessor. Its result SHA MUST equal the fetched `origin/refactor/generated-code/s03-tables` HEAD and its test status must be understood. If the handoff is absent, wrong or incomplete, do not guess a parent or start modifications.
- Start by verifying `git remote -v`, `git fetch origin --prune`, the remote parent commit and ancestry, `git status --porcelain`, and existing local branches/worktrees. If the checkout is dirty, **preserve all user edits**: use a safe additional worktree/clone, or stop with an explanation; never `git reset --hard`, `git clean -fd`, overwrite a branch, or force push.
- Create/checkout the new branch **at the exact verified parent SHA**. Do not base it on local `main`, the old inspection SHA, or an arbitrary latest commit. If this temporary branch already exists remotely, inspect its history/status and do not overwrite it.
- Record the full parent commit hash before touching files. Run the available baseline tests before changing code. If the environment cannot run a test, record the dependency/blocker; do not claim success.

## Scoped implementation mandate

Finish external asset separation without duplicating the existing HTML renderer. Inspect `emitter/project.py`, `runtime/{html,html_format,query}.py`, `utilities/html_report.py`, `ICMPCS/html/report_055.html` and current direct HTML tests. Existing SQL and HTML files already work and CSS file linking/copying is partly supported. Generate independent CSS assets, eliminate giant `styles[...]` declarations and lengthy report/template literals from `main.py`. Respect HTML-RUN/HTML-DELETE style-state order, dynamic substitutions, secure escaping, unique asset paths, read-after-user-edit behavior, report schema from Session 03, linked vs `CSSEMBED` inline CSS, nonempty-project safety and relocatable Linux paths. Preserve SQL text and backend query semantics. Avoid Jinja or a framework unless a proven blocker demands it.

Work bottom-up from tests and authoritative runtime semantics to emission. Reuse existing code before adding helpers. Preserve `EmittedScript.assets`, `StepEmission`, editable parameters, source offsets and source-location errors. Preserve direct execution without VG2 input or ScriptHost imports. Use AST/source inspection and representative generated output to catch noise, not cosmetic line-count tricks. Keep public API compact, state explicit, source comments factual and file paths safe across Linux/container deployments.

## Verification and change discipline

Focus on `tests/runtime/{test_direct_html,test_html_report,test_direct_runtime}.py`, `tests/emitter/{test_generated_project,test_emission_metadata}.py`, plus full suite. Test stylesheet edits on rerun, linked/embedded modes, multiple conditional style states and HTML escaping and relocation.

Compare old vs new behavior using deterministic sanitized fixtures and stubs for unavailable external dependencies; `ICMPCS/main.py` is a readability reference, **not** safe for unmocked execution. Run focused tests repeatedly and the complete baseline suite before declaring this session done. Never remove or soften a behavioral assertion merely to make the new design pass. Include representative generated-code before/after excerpts and readability measures; identify any moved complexity rather than calling it removed.

## Commit, push and handoff

1. Before completion, review the diff for scope creep, semantic duplication, dead code, formatting, and accidental secrets or private production output.
2. Create `docs/generated-code-refactor/handoffs/SESSION_04_HANDOFF.md` **on your temporary branch** with: repository + branch; exact parent SHA; exact committed result SHA (if recording the commit SHA in-file would require self-reference, store the final SHA in your final response and record the prior implementation SHA in-file); implementation summary/files; public runtime and emitter contract; measured generated examples; precise executed test commands and results; limitations and required next-session constraints. Keep it versioned along with the work.
3. Commit and push **only `refactor/generated-code/s04-assets`**. If a test remains failing, label the branch/handoff WIP and report it clearly rather than claiming acceptance. Verify fetched remote HEAD equals the push result, and provide GitHub compare/diff link against `origin/refactor/generated-code/s03-tables`.
4. Report the exact final commit SHA and give the next agent its parent branch + SHA. Never automatically start Session 05 or merge anything.

## Acceptance gate

The concrete acceptance criteria in `04_HTML_CSS_SQL_OUTPUT.md` and the shared master regression contract must be met or explicitly marked incomplete. Prior session behavior must continue to pass. Finish with a concise outcome: changes, results, generated-code example, risks, branch, full SHA and next-step handoff.
