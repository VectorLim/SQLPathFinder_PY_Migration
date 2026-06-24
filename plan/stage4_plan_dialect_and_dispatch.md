# Stage 4 Plan — SQL Dialect Resolver & Reader Dispatch

Audience: the coding agent (or human) about to implement Stage 4.
Source-of-truth for prior stages: [progress/stage1_parser_classifier.md](progress/stage1_parser_classifier.md), [progress/stage2_resolver_macro_sql.md](progress/stage2_resolver_macro_sql.md), [progress/stage3_dataflow_analyzer.md](progress/stage3_dataflow_analyzer.md).

Scope: take `AnalyzedProgram` (Stage 3) and produce a `DispatchedProgram` carrying, per SQL-bearing block, the resolved dialect, the reader-target metadata DataSyncX will need, and the schema-substituted SQL body. Nothing more.

Anchors carried forward: deterministic, diagnostics-first, no AI, no code emission, no file I/O at analysis time, no SQL parsing.

---

## Step 1 — Re-Grounding

### Plan vs. reality (Stages 1–3)
- **Stage 1** matches the plan. `Kind` covers the eight block families I called for; `ClassifiedBlock` is `parsed + kind + reason`; diagnostics are in-band.
- **Stage 2** matches with two intentional refinements: an explicit `if` scope-node kind (added for emitter clarity, per its implementation issues file); the SQL macro behaviour resolved to structural capture (no compile-time `IN(...)` materialisation), which is what the plan required.
- **Stage 3** matches with two notes:
  - **Location deviation**: package landed at `src/vg2c/dataflow/` (the user's deliverable wording) instead of the plan's `src/vg2c/analyzer/`. Cosmetic; nothing else depends on the name.
  - **Side-effect patch**: Stage 2's `_collect_csv_consumers` was patched in-flight to split comma-separated `/TABLE=a.tab,b.tab`. This was already flagged as a Stage 3 prerequisite. Good.

### Constraints Stage 4 inherits
- `ResolvedBlock` and `AnalyzedProgram` are immutable. Stage 4 must add a new top-level wrapper, not mutate.
- `Kind` already distinguishes `MARS_READ`, `OASYS_READ`, `ARIES_READ`, `SQLITE_QUERY`. Stage 4 reads these; it does not invent new ones.
- `ResolvedBlock.resolved_body` is the post-macro-resolution SQL text (with `<<<NAME>>>` already rewritten or marked). Stage 4 operates on this, not on raw text.
- `SqlMacroCall` placeholders (e.g. `@@SQLMACRO:0@@`) are already embedded in `resolved_body`. Stage 4 leaves them alone — they're emitter-territory tokens.
- Dataflow diagnostics already cover ordering / scope crossings. Stage 4 does NOT redo that.

---

## Step 2 — Critical Review of Stage 3

### What Stage 3 accomplishes (and Stage 4 can rely on)
- Per-CSV-path producer/consumer records with `producer_kind` ∈ {`write-file`, `db-read`, `sqlite-query`, `external-presumed`, `unknown`} — sufficient to identify reader-target context at the path level.
- `DataflowEdge.scope_relation` and `order_ok` — fully cover ordering and scope-crossing.
- `dataflow-likely-external-producer` heuristic — softens noise around utility-produced files.
- Stage 2's `/TABLE=` comma-split fix → multi-input SQLite blocks now link correctly. Verified end-to-end.
- 86 tests passing; the four "clean" fixtures emit no error-severity diagnostics.

### Gaps / Weaknesses (real, evidence-based)

| # | Issue | Severity | Stage 4 implication |
|---|---|---|---|
| W1 | `Kind` alone is not enough to choose a DataSyncX reader. MARS/OASYS/ARIES all are `*_READ`, but the *exact* DataSyncX call is `OracleReader(database='MARS' \| 'OASYS' \| 'ARIES')` keyed off `/NODE` — Stage 1's classifier puts the dialect into the `Kind` enum, but the runtime call needs more (the `/NODE` string, the `/RECORD` identity). | Medium | Stage 4 must extract these and tag the block. |
| W2 | `@OASYSSCHEMA@` is **not** rewritten anywhere in stages 1–3. `sql_script.txt` has three such references; emitting them as-is to DataSyncX would produce a runtime SQL error (DataSyncX only substitutes MARS `@[]@`). | High | Stage 4 must resolve `@OASYSSCHEMA@` at compile time. |
| W3 | `@[]@` markers are passed through (correct), but no validator confirms they appear only in MARS-dialect blocks. A mis-classified block could silently carry `@[]@` into OASYS context or vice versa. | Low | Stage 4 emits an info diagnostic when a placeholder doesn't match the resolved dialect. |
| W4 | `/RECORD=Calendar@1.0.0.0`, `/RECORD=WIP_Lot_History_v2@1.0.0.0`, `/RECORD=Spc_Chart_or_Raw@1.0.0.0`, `/RECORD=AT_Metrology@1.0.0.0` — currently stored only in raw options. Future view-registry lookup and emitter logging both need this parsed out. | Low | Stage 4 parses `/RECORD=` into `(name, version)`. |
| W5 | `script_another.txt` and `script_from_vietnam.txt` both contain a MARS block followed by an inline Python `WRITE_FILE` whose body reads `calendar_ref.csv` and overwrites it with cleaned data. Stage 3 surfaced this as `dataflow-unused-output` (the MARS-written CSV has no structural consumer). Stage 4 inherits this honest signal; no action — but the emitter will need to know the inline-Python case exists. | Low | Out of scope for Stage 4; note for Stage 5. |
| W6 | The `/RECORD` value is the SPF view identity; the body uses `@[]@F_<name>` and `@OASYSSCHEMA@P_<name>` table references whose names need not match `/RECORD`. There's no static check that they align. | Low | Skip; we don't have authoritative mapping data. |

### What Stage 3 does NOT need fixing for
- Cycle detection: by-design omitted (sequential VG2). Confirmed correct.
- Body parsing for inline-Python `to_csv`: by-design omitted. Confirmed correct.
- The location of the package (`dataflow/` vs `analyzer/`): cosmetic; leave as-is.

---

## Step 3 — Stage 4 Focus

### Pushback on "full View Expander"
The user's prompt lists "View Expander (SPF logical view expansion using View Registry)" as the leading candidate. I am **rejecting that scope for Stage 4** on three grounds:

1. **No data.** A View Registry would have to encode physical tables, intra-view joins, bridge aliases, and implicit predicates **per logical view**. We have zero authored YAML and no documented source for it. Implementing the expander now means hand-fabricating registry entries by guessing — exactly the speculative work my architecture forbids.
2. **No need to verify.** Real Intel MARS likely exposes `F_*` as actual Oracle views; DataSyncX's schema substitution would be sufficient at runtime. Whether OASYS `P_*` views are also real-view-backed is unverified. Building expansion logic without a fixture that fails without it is premature.
3. **There IS a near-term blocker** that View Expander does NOT solve: `@OASYSSCHEMA@` is unresolved everywhere downstream, and the Emitter cannot pick the right DataSyncX reader call from `Kind` alone.

### Stage 4 focus (decided)
**SQL Dialect Resolver & Reader Dispatch.**

Stage 4 does exactly four things, each justified by a current fixture or by Stage 5's known need:

1. **Dialect classification** — every block with SQL (`MARS_READ`, `OASYS_READ`, `ARIES_READ`, `SQLITE_QUERY`) is tagged with a `Dialect` (`oracle_mars`, `oracle_oasys`, `oracle_aries`, `sqlite`) derived from `/NODE`, `/ENGINE`, `/OLEDB` — the same signals Stage 1's classifier used, now made first-class data.
2. **Schema placeholder substitution** — `@OASYSSCHEMA@` is rewritten to a configured schema string at compile time. `@[]@` is preserved (DataSyncX substitutes at runtime). `Schema-mismatch` diagnostic if a placeholder appears in the wrong dialect.
3. **Reader target metadata** — for each SQL-bearing block, attach a `ReaderTarget` carrying the DataSyncX class hint (`OracleReader` / `SQLiteReader`), the `database` argument (`MARS` / `OASYS` / `ARIES`), the parsed `/RECORD=` `(name, version)`, the `/NODE` string, and the `/INSTANCE`. This is the data the Emitter needs to write `Reader(...)` calls without re-walking options.
4. **View Registry scaffold (optional, empty-by-default)** — a YAML loader is wired in but the file may be absent or empty. When the file is empty, Stage 4 emits no expansion. When entries exist, future iterations can grow the registry without touching code. **No view expansion is performed in v1.**

The slot for the eventual View Expander is reserved by item 4; the implementation is one stub that returns identity.

This is the smallest stage that closes the blockers between Stage 3 and a usable Stage 5 emitter, and nothing more.

---

## Step 4 — Architecture

All in `src/vg2c/dispatch/` (new package). Two small modules + one optional helper.

### `DialectResolver` (`src/vg2c/dispatch/dialect.py`)
- **Responsibility:** assign a `Dialect` to each `ResolvedBlock` whose `kind` is SQL-bearing, derived deterministically from `/NODE`, `/ENGINE`, `/OLEDB`.
- **In → Out:** `AnalyzedProgram` → list[`(block_index, Dialect)`].
- **Why it exists:** `Kind` is too coarse for reader dispatch (`MARS_READ` and `OASYS_READ` are distinct enum members but their distinction is currently re-derived in two places). One canonical resolver, one source of truth.

### `SchemaSubstituter` (`src/vg2c/dispatch/schema.py`)
- **Responsibility:** rewrite `@OASYSSCHEMA@` in OASYS blocks to a configured value; verify `@[]@` only appears in MARS blocks; preserve `@@SQLMACRO:n@@` tokens untouched.
- **In → Out:** SQL body string + `Dialect` + config → new SQL body string + diagnostics.
- **Why it exists:** DataSyncX does **not** handle `@OASYSSCHEMA@`. Leaving it in the emitted SQL guarantees a runtime failure. One small text-substitution pass.

### `ReaderDispatcher` (`src/vg2c/dispatch/dispatcher.py`)
- **Responsibility:** build a `ReaderTarget` per SQL-bearing block: `dialect`, `reader_class_hint`, `database_arg`, `record_name`, `record_version`, `node`, `instance`.
- **In → Out:** `(ResolvedBlock, Dialect)` → `ReaderTarget`.
- **Why it exists:** the emitter must produce calls like `OracleReader(database='OASYS', node='KM.OASYS', ...)`. Centralising the extraction in one component keeps emitter templates simple and lets us swap reader libraries later.

### `ViewRegistry` (`src/vg2c/dispatch/view_registry.py`) — scaffold only
- **Responsibility:** load `config/view_registry/*.yaml` if present; expose `lookup(dialect, view_name) -> ViewExpansion | None`. In v1, file is empty/missing → returns `None` for everything.
- **In → Out:** path → mapping.
- **Why it exists:** reserves the slot for future expansion work without forcing it now. **Zero behaviour in v1 except an info diagnostic when a `@[]@F_X` / `@OASYSSCHEMA@P_X` reference has no registry entry — and even that is gated behind a `--with-view-registry` opt-in to avoid noise on the empty default.**

### `dispatch.analyze` (`src/vg2c/dispatch/__init__.py`)
- **Responsibility:** one-pass orchestrator: `dispatch(analyzed: AnalyzedProgram, config: DispatchConfig | None = None) -> DispatchedProgram`.

### Data model

All new types in `src/vg2c/dispatch/models.py`. Frozen, slotted dataclasses.

- **`Dialect`** (`Literal`): `"oracle_mars"`, `"oracle_oasys"`, `"oracle_aries"`, `"sqlite"`.
- **`ReaderTarget`**:
  - `dialect: Dialect`
  - `reader_class_hint: Literal["OracleReader", "SQLiteReader"]`
  - `database_arg: str | None` — `"MARS"` / `"OASYS"` / `"ARIES"`, `None` for SQLite
  - `record_name: str | None`, `record_version: str | None` — parsed `/RECORD=Name@version`
  - `node: str` — raw `/NODE` (preserved for diagnostics and emitter logging)
  - `instance: str | None` — raw `/INSTANCE`
- **`DispatchedBlock`**: `block_index: int`, `dialect: Dialect`, `reader_target: ReaderTarget`, `rewritten_sql: str` — the body after schema substitution; same as `ResolvedBlock.resolved_body` for non-SQL blocks (passed through identity).
- **`DispatchConfig`**: `oasys_schema: str` (default: empty string with a diagnostic), `aries_schema: str | None`, `view_registry_path: Path | None`.
- **`DispatchedProgram`**:
  - `analyzed: AnalyzedProgram` — pass-through reference
  - `dispatched: tuple[DispatchedBlock, ...]` — one per SQL-bearing block, indexed by `block_index`
  - `diagnostics: tuple[Diagnostic, ...]` — Stage 1+2+3+4 merged

`ResolvedBlock` / `AnalyzedProgram` are not modified. Stage 4 is an additive wrapper, same pattern as Stage 3.

### New diagnostic codes
- `dispatch-oasys-schema-unset` (error if encountered with `@OASYSSCHEMA@` and no config; warning if `oasys_schema=""` was explicitly passed)
- `dispatch-placeholder-dialect-mismatch` (warning) — `@[]@` in OASYS block, or `@OASYSSCHEMA@` in MARS/SQLite
- `dispatch-record-malformed` (info) — `/RECORD=` present but not in `Name@version` form
- `dispatch-unknown-dialect` (warning) — SQL-bearing block whose `/NODE`/`/ENGINE` doesn't fall into a known dialect
- `dispatch-view-registry-missing-entry` (info, **opt-in**) — `@[]@F_X` or `@OASYSSCHEMA@P_X` not in the registry. Only emitted when `--with-view-registry` is explicitly enabled, to avoid noise.

---

## Step 5 — Pitfalls (Real, Ranked)

### High risk

**H1. `@OASYSSCHEMA@` resolution semantics.**
`sql_script.txt` has `@OASYSSCHEMA@P_SPC_Batch_Lot v1` — three references. If `oasys_schema=""` (unset), naive substitution produces `P_SPC_Batch_Lot v1`, which is syntactically valid SQL but resolves against the *connecting user's default schema* — usually wrong. Stage 4 must:
- Refuse to silently substitute empty: emit `dispatch-oasys-schema-unset` and **leave the placeholder in place** (so a downstream `--strict` mode can fail and the SQL is at least diagnosable).
- Strip the trailing `@` and any optional separator the SPF wiki documents (varies; safest: replace `@OASYSSCHEMA@` literally with `oasys_schema + "."` when configured, so `@OASYSSCHEMA@P_SPC_Batch_Lot` → `OASYS_OWN.P_SPC_Batch_Lot`).

### Medium risk

**M1. `/NODE` parsing for dialect.**
Observed forms:
- `KM.[A15_PROD_21.].MARS` — MARS with a square-bracketed sub-token containing a dot
- `KM.OASYS` — bare OASYS
- `.\` — local (SQLite)
- `.\` with `/OLEDB=SQLite` / `/ENGINE=SQLite` — also SQLite

A naive `node.endswith(".MARS")` works on the current set, but the bracketed `[A15_PROD_21.]` token can confuse future parsers. **Stay simple:** Stage 1 already classifies by `Kind`; Stage 4's `DialectResolver` should derive dialect from `Kind` first, and only fall back to parsing `/NODE` when `Kind` is ambiguous (e.g. classifier returned `UNKNOWN` but the block has a SQL body). This avoids re-implementing classifier logic.

**M2. Multiple `@OASYSSCHEMA@` occurrences per body.**
`sql_script.txt` has three in one body. Substitution must be a global replace, not first-match. (Trivial but easy to get wrong with `str.replace(..., 1)`.)

**M3. SQL macro placeholders coexist with schema placeholders.**
A body can contain `@OASYSSCHEMA@P_SPC_Batch_Lot v1 ... SQL_Get_CSV_List(...) ...` which Stage 2 has already rewritten to `@OASYSSCHEMA@P_SPC_Batch_Lot v1 ... @@SQLMACRO:0@@ ...`. Stage 4 must scan for `@OASYSSCHEMA@` only and **not** touch `@@SQLMACRO:n@@`. The regex / search must be specific (`@OASYSSCHEMA@`, not `@\w+@`).

### Low risk

**L1. `/RECORD=` parsing.**
Observed values: `Calendar@1.0.0.0`, `WIP_Lot_History_v2@1.0.0.0`, `AT_Metrology@1.0.0.0`, `Spc_Chart_or_Raw@1.0.0.0`. Format is consistent (`Name@dotted_version`). One `split("@", 1)` covers all current cases. Malformed → `dispatch-record-malformed` info, store raw.

**L2. Missing `/RECORD` on a read block.**
Spotted in `script_short.txt`'s SQLite block — no `/RECORD=` because SQLite blocks don't have one. Expected; `record_name` is `None`. No diagnostic.

**L3. ARIES coverage.**
No ARIES fixture exists. The classifier rule is speculative (per Stage 1 plan). Stage 4 inherits this — `oracle_aries` dialect is defined but untested by real data. Mirror Stage 1's `aries-rule-untested` info one-shot.

**L4. View Registry empty in v1.**
The opt-in flag ensures no diagnostics from an empty registry. No risk to existing tests.

---

## Step 6 — Testing Strategy

All under `tests/dispatch/`. Reuse existing fixtures; no new fixture files.

### Unit tests — `tests/dispatch/test_dialect.py`
- `Kind.MARS_READ` → `oracle_mars`.
- `Kind.OASYS_READ` → `oracle_oasys`.
- `Kind.ARIES_READ` → `oracle_aries`.
- `Kind.SQLITE_QUERY` → `sqlite`.
- `Kind.UNKNOWN` with SQL-shaped body and `/NODE=KM.OASYS` → fallback derivation returns `oracle_oasys` + `dispatch-unknown-dialect` info.
- `Kind.UNKNOWN` with no dialect signals → no `Dialect`, no diagnostic (skipped).

### Unit tests — `tests/dispatch/test_schema.py`
- OASYS body with three `@OASYSSCHEMA@P_*` references + `oasys_schema="OASYS_OWN"` → all three replaced; no placeholder remains.
- OASYS body + `oasys_schema=""` (explicit) → unchanged body + `dispatch-oasys-schema-unset` warning.
- OASYS body + no config (None) → unchanged body + `dispatch-oasys-schema-unset` error.
- MARS body with `@[]@F_LotHist` → unchanged; no diagnostic.
- MARS body that wrongly contains `@OASYSSCHEMA@` → unchanged + `dispatch-placeholder-dialect-mismatch` warning.
- OASYS body that wrongly contains `@[]@` → unchanged + `dispatch-placeholder-dialect-mismatch` warning.
- Body containing `@@SQLMACRO:0@@` and `@OASYSSCHEMA@` — only the schema token is rewritten; the SQL macro placeholder is byte-identical.

### Unit tests — `tests/dispatch/test_dispatcher.py`
- MARS block builds `ReaderTarget(dialect="oracle_mars", reader_class_hint="OracleReader", database_arg="MARS", record_name="Calendar", record_version="1.0.0.0", node="KM.[A15_PROD_21.].MARS", instance="8486")`.
- OASYS block builds the OASYS-equivalent.
- SQLite block builds `ReaderTarget(dialect="sqlite", reader_class_hint="SQLiteReader", database_arg=None, record_name=None, record_version=None, node=".\\", instance=...)`.
- `/RECORD=Calendar` (no `@`) → `record_name="Calendar"`, `record_version=None`, `dispatch-record-malformed` info.

### Unit tests — `tests/dispatch/test_view_registry.py`
- Empty registry path → loader returns empty mapping; no diagnostic on any block.
- With `--with-view-registry` and no entry for `F_LotHist` → `dispatch-view-registry-missing-entry` info on the consuming block.
- With registry containing `F_LotHist` → `lookup` returns the expansion record (verified by structure, not used by anything in v1).

### Edge cases
- Body with both `@[]@F_X` and `@OASYSSCHEMA@P_Y` (impossible in real fixtures but synthesise) — both dialect-mismatch diagnostics emitted.
- Body with no SQL at all (e.g. `UTILITY` block, `WRITE_FILE` block) → dispatcher skips entirely; no `DispatchedBlock` entry. (Stage 4 only emits for SQL-bearing kinds.)
- `script_short.txt` SQLite block with empty `/NODE=.\` and no `/RECORD` → `ReaderTarget(dialect="sqlite", ...)` constructed cleanly.

### End-to-end fixture tests — `tests/dispatch/test_fixtures.py`
Pipeline `parse → classify → resolve → analyze → dispatch` over the five fixtures. For each:
- Runs without exception.
- No new error-severity diagnostics on `script_short.txt`, `script_another.txt`, `script_from_vietnam.txt`. (`actual_script.txt` and `sql_script.txt` may emit `dispatch-oasys-schema-unset` if run with no config — see test fixture for `DispatchConfig`.)
- `DispatchedProgram.dispatched` length equals the count of SQL-bearing blocks in that fixture.

Per-fixture spot checks:
- **`script_short.txt`**: one `DispatchedBlock` with `dialect="sqlite"`.
- **`script_another.txt`** / **`script_from_vietnam.txt`**: one `oracle_mars` block with `record_name="Calendar"`, `record_version="1.0.0.0"`, body unchanged (no `@OASYSSCHEMA@`).
- **`sql_script.txt`**: one `oracle_mars`, one `oracle_oasys`, one `sqlite`. With `DispatchConfig(oasys_schema="OASYS_OWN")`, the OASYS block's `rewritten_sql` contains no `@OASYSSCHEMA@` and three occurrences of `OASYS_OWN.P_`.
- **`actual_script.txt`**: at least two `oracle_mars` blocks and several `sqlite` blocks; record names match the observed `/RECORD` values (`WIP_Lot_History_v2`, `AT_Metrology`, etc.).

### What is deliberately not tested
- Whether the substituted schema is "correct" against a live database (out of scope; no DB).
- View expansion (not implemented).
- Per-call DataSyncX behaviour (Stage 5+).
- Performance (whole pipeline should still be sub-second; if it isn't, the impl is wrong).

---

## Step 7 — Non-Goals for Stage 4

- **No Python code emission.** Stage 5.
- **No DataSyncX runtime calls.** Same.
- **No actual View Expansion logic** (registry returns identity in v1).
- **No re-resolution of macros.** Trust Stage 2.
- **No recalculation of dataflow.** Trust Stage 3.
- **No agentic AI.** Constraint carried.
- **No new `Kind` values** or modifications to Stage 1.
- **No mutation of `ResolvedBlock` / `AnalyzedProgram`.** Stage 4 produces a side-table wrapper.
- **No CLI surfacing.** Diagnostics returned; caller decides.
- **No registry contents shipped.** The YAML loader is plumbed; the registry is empty.
- **No new `Kind`-level dispatch logic in the classifier.** Dialect refinement is Stage 4's job, not Stage 1's.
- **No SQL parsing.** Schema substitution is one literal string replace.

---

## Step 8 — Simplicity Check

### Intentionally not implemented
- The full View Expander (deferred until we have authored registry data and a fixture that proves it's needed).
- A "Reader Builder" that constructs concrete DataSyncX classes — Stage 4 hands the Emitter metadata, not objects.
- Cross-dialect SQL transformation (e.g. translating MARS-style SQL to OASYS-style).
- TimeRangeBuilder / QueryBuilder integration — DataSyncX-specific, handled at emission.
- A `Validator` stage. Stage 4 emits new diagnostics; gating is still a CLI concern.

### Most likely future complexity
- **`@OASYSSCHEMA@` config sourcing.** `DispatchConfig` is currently passed in by the caller. When a CLI exists, where does the value come from — env, TOML, command-line flag, per-instance YAML? Punt; today it's a constructor argument and that's enough.
- **View registry growth.** When the first authored entry lands, the registry needs schema versioning, validation, and an expansion executor. None of that exists in v1; the slot is reserved so it doesn't require a refactor when it lands.
- **Bridge alias rules for OASYS-style multi-view queries** (P5 from the original notes). `sql_script.txt` already exhibits `v1`/`v2`/`v3` with `v2` as a bridge. This belongs to the future View Expander, not Stage 4.

### Minimum viable Stage 4 (first commit path)
1. Create `src/vg2c/dispatch/models.py` with `Dialect`, `ReaderTarget`, `DispatchedBlock`, `DispatchConfig`, `DispatchedProgram`. ~70 lines.
2. Create `src/vg2c/dispatch/dialect.py` — `resolve_dialect(block) -> Dialect | None`. ~25 lines.
3. Create `src/vg2c/dispatch/schema.py` — `substitute(body, dialect, config) -> tuple[str, list[Diagnostic]]`. ~40 lines.
4. Create `src/vg2c/dispatch/dispatcher.py` — `build_target(block, dialect) -> ReaderTarget`. ~30 lines.
5. Create `src/vg2c/dispatch/view_registry.py` — stub loader with `lookup` always-None in v1. ~20 lines.
6. Create `src/vg2c/dispatch/__init__.py` — `dispatch(analyzed, config=None) -> DispatchedProgram` orchestrator. ~40 lines.
7. Wire export from `src/vg2c/__init__.py`.
8. Write unit and end-to-end tests from §6.
9. Run `pytest`; iterate until green; commit.

Estimated effort: smaller than Stage 3. The hard part is the schema-substitution test matrix (a handful of cases) and getting the diagnostic codes right.

---

*End of Stage 4 plan. Implementation proceeds only after this plan is approved.*
