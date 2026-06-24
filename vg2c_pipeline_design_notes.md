# vg2c automated-pipeline — design reference

Audience: future self designing the post-classifier stages (validator → resolver → IR →
sqlrewrite → translators → runtime → emitter) that turn parsed/classified VG2 blocks into
runnable Python (typically using DataSyncX).

This file is a *reference*, not a spec. Findings come from:
- `.venv/Lib/site-packages/datasyncx/` (verified source, not just docs)
- `docs for DataSyncX/`, `docs for SQLPathFinder/`, self-paced PPTs
- Existing `tests/` (classifier kinds + actual_script.txt fixtures)

---

## 1. Pitfalls — ordered by blast radius

### P1. VG2 scripts are strictly sequential. LIFO is the wrong primitive.
Every real fixture (`actual_script.txt`, etc.) is structured as `Step 1-1, 1-2, 1-3, …`
where later steps depend on files / macro variables produced by earlier ones. A LIFO
stack reverses execution and would silently break correctness with no parser error.

What the script actually needs:
- An **ordered list (FIFO)** of compiled units executed in source order.
- A **scope stack** for nested macro / control blocks (`{START-MACRO}` … `{END-MACRO}`,
  `{IF-THEN}` … `{ELSE}` … `{END-IF}`). The *stack* is for parse/compile, not for
  runtime ordering.
- A **shared registry / data pool** for cross-block state (see P3).

Recommendation: do not implement the runtime as a LIFO queue. Implement it as a
linear `Program = list[CompiledBlock]` plus a `ScopeTree` produced by a one-pass
structural pass that pairs START/END and IF/ELSE/END-IF.

### P2. DataSyncX silently strips processor `__init__` state per chunk.
`datasyncx/core/task.py::Task.process` does:
```python
processor = type(processor)()
input_df = processor.process(input_df, *args, **kwargs)
```
This re-instantiates each processor **with no args** for every chunk, *discarding*
anything set in `__init__` (e.g. `FieldKeeperProcessor(fields_to_keep=[...])`
becomes `fields_to_keep=None` and raises `ValueError` on call). The base class even
warns: *"Do NOT modify self attributes defined in the base class."*

Implications for our emitter:
- Treat DataSyncX `Processor` subclasses as **effectively stateless**. Any
  configuration must flow through `kwargs` from the executor (or be embedded in the
  class itself, hard-coded per-task).
- For per-task transforms we generate, the safest pattern is one **dedicated
  Processor subclass per emitted task** with config baked in as class-level
  constants, *not* constructor arguments. Or: bypass `Task.process` entirely and
  apply transforms in a custom Reader/Writer wrapper.
- Add a regression test: if we ever pass configured built-in processors, verify
  upstream behavior matches our assumption (this may be a DataSyncX bug that gets
  fixed — pin the version we validated against in `pyproject.toml` and the
  classifier `version_marker`).

### P3. Inter-block state is real and load-bearing — model it explicitly.
VG2 blocks communicate through:
- **CSV files on disk** (`/CSV=foo.csv` produced by step N, `/TABLE=foo.csv` consumed
  by step N+M). Compute-then-join pattern is the entire SQLite-engine path.
- **Macro variables** like `<<<SFOLDER>>>`, `<<<UNDERDEV>>>` set by `{START-MACRO}` /
  config transposition steps and string-substituted into later utility/SQL bodies.
- **Working directory** (`/WORKDIR=.\`) — many utilities are CWD-sensitive.
- **"Loaded files" history** (cf. DataSyncX `FileListBuilder.loaded_files_path`).
- **Environment** (Python interpreter for `Run_Python_Script.va`, server names,
  etc.).
- **Implicit ordering / fall-through control** (`{IF-THEN}` blocks gate the
  *following* blocks until `{ELSE}` / `{END-IF}`).

Recommendation: introduce a single **runtime context object** (call it
`PipelineContext` / `DataPool`) with explicit, typed slots:
```python
@dataclass
class PipelineContext:
    workdir: Path
    csv_registry: dict[str, CsvArtifact]    # logical name -> path + schema
    macro_vars: ChainMap[str, str]          # nested scopes via ChainMap.new_child()
    loaded_files: set[Path]
    env: Mapping[str, str]
    logger: logging.Logger
```
- Use `collections.ChainMap` for macro scopes so `{START-MACRO}` pushes a child and
  `{END-MACRO}` pops without copying.
- `csv_registry` lets the emitter / validator detect dangling consumers (step reads
  a CSV no prior step produced) before runtime.

### P4. The block kinds are an open set. Closed-set assumptions will hurt.
Classifier already has SQL_FETCH, SQLITE_JOIN, WRITE_FILE, RUN_PYTHON, COPY, RENAME,
DELETE_FILE, EMAIL, plus REPORT (HTML-RUN / HTML-LAYOUT / HTML-DELETE) and the macro
family ({START-MACRO}, {END-MACRO}, {IF-THEN}, {ELSE}, {ROWS-IN-FILE},
{SQLPathFinder_Email.va}, …). `actual_script.txt` alone shows ~6 new utility shapes
not in the unit tests.

Recommendation: use a **plugin registry pattern** for block handlers — *not* a
giant `if/elif`. One handler per `Kind`, discovered via a module-level
`@register(Kind.RUN_PYTHON)` decorator into a single `HANDLERS: dict[Kind, Handler]`.
Each handler is a small class implementing a uniform protocol:
```python
class BlockHandler(Protocol):
    kind: Kind
    def validate(self, spec: BlockSpec, ctx: PipelineContext) -> list[Diagnostic]: ...
    def lower(self, spec: BlockSpec, ctx: PipelineContext) -> IRNode: ...
    def emit(self, ir: IRNode, ctx: EmitContext) -> str: ...   # produces Python source
```
Unknown kinds emit a `# TODO: unhandled kind=<name>` stub *and* surface a diagnostic
so the developer sees it, instead of being silently dropped.

### P5. SPF logical views are NOT real Oracle views.
Recorded in `datasyncx_reader_fact.txt` and confirmed by the existing routing tests:
- `F_*` views (MARS) and `P_*` views (OASYS) are **client-side expansions**. SPF
  injects physical tables, intra-view JOINs, and implicit WHERE clauses that the VG2
  does *not* serialise.
- The VG2 carries `physical_table` and `expression` (`a0->p.prodgroup3`) per
  column/filter but **not** the join keys or the bridge sub-aliases (e.g. `v2`
  needed for `v1↔v3`).
- DataSyncX does *not* expand these — it only does schema substitution and only
  for `database='MARS'` (hardcoded dict in `utils/oracle_db.py::query_mao`).

Recommendation:
- The emitter cannot just paste the VG2 body into `QueryBuilder`. It must run a
  **view-expansion pass** against a hand-maintained registry (YAML/JSON) that
  encodes physical tables + join graph + implicit predicates per logical view.
- Detect dialect from the routing module (`EngineKind.ORACLE_MARS` vs
  `ORACLE_OASYS`). For MARS, emit `@[]@TableName` and let DataSyncX substitute the
  schema. For OASYS, emit `@OASYSSCHEMA@` literally OR resolve at compile time —
  there is no MARS-style substitution.
- Bridge-alias problem: warn loudly when a view's join graph references an alias
  not present in the column/filter set; require the registry to declare bridges.

### P6. Writer process_type has hidden state mutations.
In `writers/sqlserver_writer.py::write`:
- `process_type='replace'` → calls `__to_sql(..., process_type='replace')` once.
  Then the **TaskExecutor** in `task_executor.py::_db_writer_worker` mutates the
  writer object: `writer.process_type = 'append'` after the first chunk so
  subsequent chunks don't re-truncate. This means **the writer is not safe to
  reuse across runs** — second invocation starts in `'append'` mode.
- `process_type='replace2'` has its own `self.not_truncate_once` flag with the same
  one-shot semantic.
- `process_type='merge'` requires `include_columns_merge` to be set explicitly;
  falling back to `exclude_columns_merge` only emits a warning, not a hard fail.

Recommendation:
- Never reuse a writer instance between pipeline runs. The emitter should always
  *construct* writers inside the per-run function (the demos already do this).
- Generate an explicit `include_columns_merge` whenever `process_type='merge'`.
  Treat missing merge keys as a compile-time error in our validator stage.
- For the `replace_where` mode, the writer requires `replace_where_clause` — the
  validator must enforce this too.

### P7. TimeRangeBuilder / QueryBuilder timezone semantics are non-obvious.
- `TimeRangeBuilder` converts `current_time` / `current_time - 12h` to **UTC**
  immediately (via `convert_to_utc`). Absolute dates are NOT converted.
- `QueryBuilder(timezone=True)` injects `timestamp'<value>'+:timezone` into the SQL
  and rewrites `>=` / `<=` to `=` and `<` respectively (open-end interval).
- The doc-string for `freq` lists `d/h/m/w` but the implementation only handles
  `d`, `h`, `m`. `w` raises.

Recommendation: when emitting `TimeRangeBuilder`, always be explicit about
`date_format` and whether the source is absolute or relative; never mix. Add
emitter-side validation that rejects `freq='*w'` and unsupported units.

### P8. `email_on_exception` and TaskRun alerts are two separate channels.
- `@email_on_exception` (utility decorator) fires for *demo-level* failures before
  `TaskRun` is entered.
- `Task.read` + `_db_writer_worker` send their own emails via `send_msg` during
  `TaskRun`.
- A failed write does **not** raise from `TaskExecutor.TaskRun` unless
  `raise_on_task_failure=True` (default true) — but it *does* still drain the queue
  and join workers first, so partial writes are possible.

Recommendation: in emitted code, always set `raise_on_task_failure=True` and let
the outer `@email_on_exception` catch. Do not rely on per-chunk emails alone for
job status — they are noisy and can leak query text via `args_to_str` if
`query` / `file_path` aren't filtered (the framework filters these in the *info*
log path but not in the *error* alert).

### P9. Credentials and `resolve_password`.
- `resolve_password(username, password)` (utils/config.py) auto-resolves from a
  credential store when `password=None`. Hard-coded `password='...'` is treated as
  a defect by the DataSyncX playbook.
- `.env` is the supported channel; `.gitignore` must exclude it.
- The DataSyncX playbook explicitly flags inline plaintext credentials as a
  validation failure.

Recommendation: the emitter must NEVER inline credentials. Always emit
`username=os.getenv("…")` and `password=None` (let `resolve_password` handle it),
or `password=os.getenv("…")` for non-batch accounts. The validator stage should
fail the compile if a VG2 `/UN=…/PW=…` carries a non-empty literal value.

### P10. Concurrency model is read-parallel / write-serial-per-writer.
`TaskExecutor.TaskRun`:
- A `ThreadPoolExecutor(max_workers=thread)` runs reads + processors in parallel
  per `task_range`.
- One `threading.Thread` per writer drains a `Queue` (single-threaded write per
  writer).
- `thread=True` is coerced to `thread=4` (gotcha: `True` is `int`-ish in Python so
  the explicit `isinstance(thread, bool)` check matters — don't pass `True`).
- Writer-side errors are appended to `self._writer_errors` and surfaced only at the
  end.

Recommendation: pick `thread` deterministically based on source. For SQL fetches
default to 4; for file-list readers, scale to file count but cap. Never pass
`thread=True` from the emitter — pass an integer.

### P11. `Task` validates components in `__init__` but only by hasattr.
`__validate_input` checks `hasattr(reader, 'read')` etc. — duck-typed, not
isinstance. A typo (`writer.wrtie`) passes; the failure is deferred to runtime.

Recommendation: at the emitter level, type-annotate everything (`Reader`,
`Writer`, `Processor` from `datasyncx.core.base_component`) and run mypy/pyright
in CI so typos are caught at compile of the emitted script, not at first run.

### P12. SQLite-engine blocks are local pandas joins, not real SQL.
`/ENGINE=SQLite` blocks (the `SQLITE_JOIN` kind) load CSVs into an in-process
SQLite DB and run a SELECT against them. They do *not* hit a server. The reader
in DataSyncX has no built-in equivalent.

Recommendation: do not try to map `SQLITE_JOIN` to a DataSyncX `Reader`. Emit it
as a free-style helper that:
1. Reads required CSVs from `ctx.csv_registry` (validated against `/TABLE=` list).
2. Loads them into an `sqlite3.connect(":memory:")` or uses `pandas.read_csv` +
   `duckdb` / `pandasql`.
3. Writes the result back to the registry under `/CSV=…`.
DuckDB is the cleanest fit; sqlite3 is the conservative fit and matches the
original semantic exactly.

### P13. `WRITE_FILE` blocks embed *content*, not SQL.
The body of a `/WRITE-FILE=Y` block is literal text (a `.bat` script, a
`.csv` payload, a `.py` script). The classifier already handles this kind but
downstream stages must NOT try to SQL-rewrite or token-expand the body. Macro
substitution (`<<<VAR>>>`) is the only allowed transformation.

### P14. Macro variable substitution scope is global-ish but step-bound.
`<<<SFOLDER>>>` set inside one `{START-MACRO}/{END-MACRO}` pair is visible to
nested blocks until `{END-MACRO}`. Naive global substitution will leak values
across independent macros if both define the same name.

Recommendation: use `ChainMap.new_child()` on `{START-MACRO}` push, drop on
`{END-MACRO}` pop. Resolution always reads through the chain.

### P15. Don't trust the docs over the code.
The DataSyncX playbook itself says: *"If docs and code disagree, report the
mismatch explicitly and trust current code plus validation over stale prose."*
Verified examples in this repo:
- `datasyncx_reader_fact.txt` already corrects "OasysReader" → no such class;
  use `OracleReader(database='OASYS')`.
- `TimeRangeBuilder.freq` docstring lists `w` but `w` raises at runtime.

Recommendation: any DataSyncX behaviour we *depend on* gets a pinned test in
`tests/` that imports from the installed package and exercises the contract. If
DataSyncX updates and breaks it, our tests fail loudly.

---

## 2. Suggested architecture (starting point, not a hard rule)

### 2.1 Pipeline shape — staged compiler, linear runtime
```
VG2 file
  → frontend.parser       → list[ParsedBlock]              (Step 1 ✅)
  → classifier            → list[ClassifiedBlock]          (Step 2 ✅)
  → validator             → list[ValidatedBlock]+Diagnostics (Step 3)
  → resolver              → resolves view-expansion,
                            macro pairing, CSV graph        (Step 4)
  → ir                    → list[IRNode] + ScopeTree        (Step 5)
  → sqlrewrite            → dialected SQL per IRNode        (Step 6)
  → translators           → per-Kind Python AST/source     (Step 7)
  → emitter               → single Python script           (Step 9)
                            using runtime helpers           (Step 8)
```
Linear stages, each pure (input → output + diagnostics). Easy to test in
isolation. Each stage has its own subpackage and `__init__` re-exports only the
stage's `run(blocks) -> result` function.

### 2.2 Block dispatch — registry of handlers
One handler module per `Kind`, registered into a dict at import time:

```python
# vg2c/translators/run_python.py
from vg2c.translators._registry import register
from vg2c.classifier import Kind

@register(Kind.RUN_PYTHON)
class RunPythonHandler:
    def validate(self, spec, ctx): ...
    def lower(self, spec, ctx): ...
    def emit(self, ir, ectx): ...
```
- New kinds = new file + decorator. No central switch to edit.
- Unhandled kinds emit a stub + diagnostic, never silently disappear.
- `register` lives in a private `_registry.py` so the table itself is hidden.

### 2.3 Runtime context — single mutable object, explicit slots
See P3 above. Key properties:
- Constructed once at the top of the emitted script.
- Passed (explicitly, not via thread-locals or globals) to every handler.
- Macro scopes via `ChainMap.new_child()` / `ChainMap.parents`.
- CSV registry is the *only* legal channel between blocks — no implicit
  filesystem reads from raw `/CSV=` strings; everything goes through
  `ctx.csv_registry.path_of("foo.csv")`.

### 2.4 Why NOT a single global block queue
The user's "LIFO queue of blocks that access a shared registry" instinct is half
right (shared registry: yes; queue of blocks: no, because order matters and
must match source). The right primitive is:
- **Compile time**: a stack for structural pairing (macros, IF/ELSE/END-IF).
- **Run time**: a deterministic linear sequence of compiled units.
- **Cross-cutting**: the shared `PipelineContext`.

If we *do* want extensibility for new block kinds, that comes from the
**handler registry** (§2.2), not from queueing semantics.

### 2.5 Emitted runtime shape
Keep the emitted Python boring and readable. One function per VG2 file:
```python
def run() -> None:
    ctx = PipelineContext.bootstrap(...)
    step_1_1_fetch_owners(ctx)           # SQLITE_JOIN
    step_1_2_write_macrotmp(ctx)         # WRITE_FILE
    step_1_3_run_getcsrsu(ctx)           # COPY (utility)
    with macro_scope(ctx, "macrotmp.csv") as scope:   # {START-MACRO}/{END-MACRO}
        step_1_4_run_setsiteparam(scope)
    step_1_5_cleanup(ctx)                # DELETE_FILE
    ...
```
- Each step is one function, named for its `PROMPT-TEXT` (snake-cased).
- DataSyncX `Task`/`TaskExecutor` is used inside step functions that are
  `SQL_FETCH` against MARS/OASYS/Aries/Xeus.
- Local-pandas joins (`SQLITE_JOIN`) emit free-style helpers, not `Task`.
- Macro scopes are context managers — no manual stack manipulation in emitted
  code.

### 2.6 What to keep stateless vs configured
- **Reader/Writer instances**: configured per call, never reused across runs.
  Emit `with closing(Reader(...))` patterns where applicable.
- **Processors**: emit one bespoke `Processor` subclass per task with config
  baked in as class attributes (sidesteps P2). Or skip processors and inline the
  transform in a custom reader/writer.
- **PipelineContext**: a single instance per emitted `run()` call. Reset between
  invocations.

### 2.7 Diagnostics, not exceptions, at compile time
Stages should return `(result, list[Diagnostic])` rather than raise. The CLI
layer decides whether to fail-fast or continue. This is what makes `--strict`
mode meaningful and matches the existing classifier behaviour (`Kind.UNKNOWN` +
`--strict` exit 1).

### 2.8 Testing strategy
- **Unit tests per kind** (already present for classifier — extend to translator).
- **Fixture snapshot tests**: each fixture script → snapshot of emitted Python +
  snapshot of `PipelineContext` final state for a dry-run mode.
- **Contract tests against DataSyncX**: small `tests/datasyncx_contracts/`
  module that imports from the installed `datasyncx` and asserts the behaviours
  we depend on (P2 processor reinstantiation, P6 process_type mutation, etc.).
  These break loudly when the library upgrades and saves debugging time.
- **No live DB tests**. Mock at the DataSyncX boundary; trust their tests for
  the connection layer.

### 2.9 Anti-patterns to avoid
- Global mutable state outside `PipelineContext`.
- `**kwargs` pass-through across more than one layer (the DataSyncX
  `TaskExecutor._run_and_enqueue` already abuses this — passing `task_range` as
  *both* `task_range=` and `file_path=` because it doesn't know which the reader
  wants. Don't propagate the pattern; explicitly route in handlers).
- Reflection-driven dispatch (`getattr(self, f"handle_{kind}")`). Use the
  registry.
- Inheritance hierarchies of handlers. Keep handlers flat; share via composition
  (helpers in `vg2c/runtime/` or `vg2c/_common/`).
- Re-implementing DataSyncX features (TimeRange, QueryBuilder, retry) — call
  them. Re-implement only when we have a verified pitfall (e.g. SQLITE_JOIN).

---

## 3. DataSyncX-specific cheat-sheet

| Topic | Source of truth |
|---|---|
| Reader list (no `OasysReader`!) | `docs for DataSyncX/datasyncx_reader_fact.txt` |
| MARS schema mapping | `utils/oracle_db.py::query_mao` (hardcoded dict) |
| MARS placeholder | `@[]@TableName` — substituted by DataSyncX, NOT by us |
| OASYS placeholder | `@OASYSSCHEMA@` — NOT substituted by DataSyncX, resolve at compile |
| Time range placeholders | `:from_when`, `:till_when` in SQL; rewritten by `QueryBuilder` |
| Timezone | `QueryBuilder(timezone=True)` makes runtime UTC + offset injection |
| Token range placeholder | `:token` or `:<token_name>` |
| Static keys | `QueryBuilder(keys={'k': [...]})` → `:k` substitution; values must be `list[str|int]` |
| Process types | `append` / `replace` / `replace2` / `merge` / `replace_where` |
| Merge keys | Always set `include_columns_merge=[...]` explicitly |
| Email on demo-level failure | `@email_on_exception(extra_title=...)` |
| Email on TaskRun-level failure | Built into `Task` and `_db_writer_worker` |
| Logger | `set_log(log_file_path)` returns a `logging.Logger`; pass to `TaskRun(log=...)` |
| Credentials | `resolve_password(username, None)` from env / `.env` |

---

## 4. Open questions to revisit during implementation

1. SPF view-expansion registry — schema and storage location? (Likely
   `vg2c/resolver/view_registry/*.yaml`.)
2. SQLite-engine: stay on `sqlite3` for semantic parity, or move to `duckdb` for
   speed? Decide per-fixture once perf tests exist.
3. Macro variables: do they ever survive across `{END-MACRO}`? (Check more
   fixtures before designing scope semantics.)
4. HTML report blocks (`REPORT=HTML-RUN` / `HTML-LAYOUT` / `HTML-DELETE`) — are
   these in-scope for migration or out-of-scope? Currently classified but no
   handler planned.
5. ScriptHost scheduling — does the emitted Python need to be triggerable from
   ScriptHost, or is it expected to run standalone?

---

*Last revised: 2026-06-24. Versions verified: datasyncx (whatever is currently in
.venv — pin in pyproject before relying on P2/P6 specifics).*
