# SQLPathFinder generated-code refactor — standalone agent prompts

**Publication branch:** `html-and-sql-rework` (planning documents and prompts only). **Do not implement on this branch.**

## Serial, cumulative temporary branches

| Agent | Parent ref (must verify SHA & handoff) | New branch | Plan | Prompt |
|---|---|---|---|---|
| 01 — Macro state and substitution | `origin/html-and-sql-rework` | `refactor/generated-code/s01-macros` | [Plan](../01_MACRO_SIMPLIFICATION.md) | [Prompt](SESSION_01_PROMPT.md) |
| 02 — Runtime context and concise calls | `origin/refactor/generated-code/s01-macros` | `refactor/generated-code/s02-runtime` | [Plan](../02_RUNTIME_CONTEXT.md) | [Prompt](SESSION_02_PROMPT.md) |
| 03 — CSV/table semantics and selective pandas | `origin/refactor/generated-code/s02-runtime` | `refactor/generated-code/s03-tables` | [Plan](../03_CSV_TABLE_PANDAS.md) | [Prompt](SESSION_03_PROMPT.md) |
| 04 — HTML/CSS/SQL editable assets | `origin/refactor/generated-code/s03-tables` | `refactor/generated-code/s04-assets` | [Plan](../04_HTML_CSS_SQL_OUTPUT.md) | [Prompt](SESSION_04_PROMPT.md) |
| 05 — Integration, cleanup and Linux validation | `origin/refactor/generated-code/s04-assets` | `refactor/generated-code/s05-integration` | [Plan](../05_FINAL_INTEGRATION.md) | [Prompt](SESSION_05_PROMPT.md) |

These are **separate branches with a sequential commit ancestry**, not five parallel branches all forked from the baseline. After Session 01 pushes its tested temporary branch, Session 02 starts from that exact remote HEAD, and so on. Every session changes only its own branch and provides a committed handoff `docs/generated-code-refactor/handoffs/SESSION_0N_HANDOFF.md`; the following agent verifies it against the remote branch HEAD before coding. No merge/pull request is authorized automatically.

## Universal launch process

1. Copy the full matching `SESSION_0N_PROMPT.md` into a **new ChatGPT/coding agent session**. Give it the repository location if the agent cannot discover it. The prompt itself has all source/branch paths.
2. Use a safe clean worktree/checkout. Never destroy unrelated changes, rebase/force-push a predecessor branch, or guess a missing handoff commit. Record exact baseline SHA and tests.
3. Only after the predecessor has finished, pushed and passed its acceptance gate, launch the next prompt.
4. Review the final `s05-integration` branch. Merge to a long-lived branch only with explicit user approval.

## Shared quality constraints

Readable `main.py`, scoped `macros["NAME"]`, one per-job runtime, native Python control flow, reusable `vg2c.runtime` implementations, SQL/HTML/CSS assets, correct dynamic substitution and HTML escaping, table schema/crosstab parity, stable invocation metadata and Linux direct execution. No UI work or speculative architecture. The existing pandas dependency should be evaluated rather than reintroduced. Retain unsupported-feature diagnostics. Preserve original VG2/ScriptHost provenance when adapting specialized semantics. Every session runs focused plus full tests and reports exact environmental blockers.

## Source inspection anchor

Original inspected starting commit before planning publication: `afb701c2013537ffbf3c1e1b86e147ad0c900d1d`. This is **not** the Session 01 implementation parent; Session 01 starts from the newer planning-document commit at `origin/html-and-sql-rework`. Do not hardcode a guessed post-publication SHA.
