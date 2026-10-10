# SQLPathFinder generated-code refactor — standalone agent prompts

**Planning branch:** `html-and-sql-rework` (documentation only; no implementation here).

## Sequential implementation with one shared branch for Sessions 02–05

| Session | Required predecessor | Work branch | Prompt |
|---|---|---|---|
| 01 — Macros, already started | Original planning publication | `refactor/generated-code/s01-macros` (unchanged) | [Session 01](SESSION_01_PROMPT.md) |
| 02 — Runtime context | Final tested and pushed Session 01 SHA | **Create** `refactor/generated-code/implementation` once | [Session 02](SESSION_02_PROMPT.md) |
| 03 — CSV/Pandas | Final tested and pushed Session 02 SHA | **Continue** `refactor/generated-code/implementation` | [Session 03](SESSION_03_PROMPT.md) |
| 04 — HTML/CSS/SQL | Final tested and pushed Session 03 SHA | **Continue** `refactor/generated-code/implementation` | [Session 04](SESSION_04_PROMPT.md) |
| 05 — Integration | Final tested and pushed Session 04 SHA | **Continue** `refactor/generated-code/implementation` | [Session 05](SESSION_05_PROMPT.md) |

**Decision (2026-10-10):** Sessions run serially and overlap in emitter/runtime files. Multiple per-session branches are unnecessary. Session 01 had already started under its original branch, so it stays unchanged. Session 02 creates one new shared implementation branch from Session 01's completed commit. Sessions 03–05 reuse that branch, preserving separation with commits, agent sessions and handoff files.

## Mandatory transition after Session 01

The updated planning files were published on `origin/html-and-sql-rework` **after Session 01 started**. The Session 01 implementation branch does not automatically inherit these later docs. When Session 01 finishes, Session 02 **must** use the latest Session 02 prompt and master plan from the planning branch (e.g. `git show origin/html-and-sql-rework:docs/generated-code-refactor/agent-prompts/SESSION_02_PROMPT.md`). Verify Session 01's exact pushed SHA against `origin/refactor/generated-code/s01-macros` HEAD and read its committed handoff.

Create `refactor/generated-code/implementation` **at that Session 01 SHA**, not at the newest planning-branch SHA. Review and copy **only the updated master, Sessions 02–05 plans, README, and Sessions 02–05 prompts** from the planning branch to the new implementation branch. Never merge the planning branch wholesale, overwrite Session 01 implementation/handoffs or drop unrelated edits. Include synced docs in a normal commit on the shared branch. Sessions 03–05 then have current prompts in-tree.

Each session writes a committed `docs/generated-code-refactor/handoffs/SESSION_0N_HANDOFF.md` with its starting SHA, test results, changes, contracts and issues. A committed handoff cannot include its own commit SHA without circular reference; the agent must report its **exact final pushed SHA in its final response**. The next agent verifies that SHA equals the fetched remote HEAD before coding. If missing or mismatched, stop. Each session runs focused plus full tests, compares generated behavior, fast-forward pushes only to the shared branch, and stops; agents must not overlap in time.

Use a clean checkout/worktree. Never `git reset --hard`, `git clean -fd`, force-push, implicitly merge or overwrite user edits. No automatic merge to `main` or `html-and-sql-rework`. Readability, semantic parity, native Python control flow, runtime isolation, SQL/CSV/crosstab, linked/embedded CSS, asset editability, source metadata and Linux execution all remain non-negotiable.

**Original inspection SHA:** `afb701c2013537ffbf3c1e1b86e147ad0c900d1d`. **Original published planning commit from which Session 01 started:** `d9211c81c4f72651329611aabfb2bc5c6abba504`. Neither is a substitute for the actual final Session 01 SHA.
