# Plan: Extensible SQL Macro Expander

## TL;DR
Split sql_macro_expander.py into two concerns:
1. A generic driver (the expander) that scans SQL bodies, finds `SQL_*` calls, looks up the right handler in a name → handler mapping, and threads diagnostics + CSV-consumer tracking.
2. A new `sql_macros/` **package** where each specific macro lives in its own file as a self-contained handler class. A `HANDLERS` mapping in the package `__init__.py` is the only thing the expander imports.

This mirrors the registry pattern already used elsewhere in the project (see repo notes P4 / §2.2). To add a new macro: drop a new file `sql_macros/sql_<name>.py` containing a `SqlMacroHandler` subclass, and add one line in `sql_macros/__init__.py` to register it. The expander is never touched.

Also widen `SqlMacroCall.name` from `Literal["SQL_Get_CSV_List"]` to `str` so other macro names are type-legal.

## Usage Analysis (read-only findings)

- `expand_sql_macros` is imported only by `src/vg2c/resolver/__init__.py` in `resolve(...)`. Public API.
- `SqlMacroCall` is consumed in:
  - `src/vg2c/dataflow/analyzer.py` (line 238) — iterates `block.sql_macro_calls`, reads `name`, `csv_path`.
  - `src/vg2c/emitter/handlers.py` (line 68+) — reads `csv_path`, `column_ref`, `lead_in`, builds `ctx.sql_macros.sql_get_csv_list(...)` call. The emitter currently hard-codes the runtime function name; future macros will need emitter-side dispatch (out of scope here).
- Tests:
  - `tests/resolver/test_sql_macro_expander.py` exercises only the public `expand_sql_macros` contract — all current tests will keep passing.
  - `tests/runtime/test_sql_macros.py` is commented out; not impacted.

## Current Redundancies

1. `SQL_CALL_RE` defined at module scope AND re-compiled inside `_next_sql_call`.
2. `_unquote` called 3 times; `_parse_column_ref` calls `_unquote` again.
3. Diagnostic construction repeated 3 times with similar argument shape.
4. Per-macro logic (arg-count check, field extraction, call-site wrap detection) is interleaved with the generic scan loop.

## Steps

1. **Create the `sql_macros/` package under `src/vg2c/resolver/`** with these files:

   - **[src/vg2c/resolver/sql_macros/base.py](src/vg2c/resolver/sql_macros/base.py)** — shared types and helpers:
     - `MacroExpansion` frozen dataclass: `call: SqlMacroCall`, `consumed_csv_path: str | None`, `appended_text: str = ""`.
     - `MacroParseError` frozen dataclass: `message: str`.
     - `SqlMacroHandler` base class with `name: str` class attr and `build_call(args, placeholder, span, before_text) -> MacroExpansion | MacroParseError`. Docstring explains the extension recipe (subclass, set `name`, implement `build_call`, register in `__init__.py`).
     - `unquote_arg(value: str) -> str` — moved from the expander (was `_unquote`); shared helper for handlers.
     - `parse_column_ref(raw: str) -> int | str` — moved from the expander (was `_parse_column_ref`).

   - **[src/vg2c/resolver/sql_macros/sql_get_csv_list.py](src/vg2c/resolver/sql_macros/sql_get_csv_list.py)** — one handler per file:
     - Imports `SqlMacroHandler`, `MacroExpansion`, `MacroParseError`, `unquote_arg`, `parse_column_ref` from `.base`.
     - Imports `SqlMacroCall` from `vg2c.resolver.models`.
     - Holds the private `_CALL_SITE_WRAP_RE` regex (specific to this macro — other macros likely won't need this exact `(<col> In …` wrap detection).
     - Defines `SqlGetCsvListHandler(SqlMacroHandler)`:
       - `name = "SQL_Get_CSV_List"`.
       - `build_call`: returns `MacroParseError(...)` if `len(args) != 3`; otherwise builds `SqlMacroCall` from `unquote_arg(args[0])`, `parse_column_ref(args[1].strip())`, `unquote_arg(args[2])`. Sets `consumed_csv_path` to the raw csv path (expander normalizes). Sets `appended_text=")"` when `_CALL_SITE_WRAP_RE.search(before_text)` matches.

   - **[src/vg2c/resolver/sql_macros/__init__.py](src/vg2c/resolver/sql_macros/__init__.py)** — the package façade and registry:
     - Imports `MacroExpansion`, `MacroParseError`, `SqlMacroHandler` from `.base`.
     - Imports `SqlGetCsvListHandler` from `.sql_get_csv_list`.
     - Defines `HANDLERS: dict[str, SqlMacroHandler] = {h.name: h for h in (SqlGetCsvListHandler(),)}`.
     - `__all__ = ["HANDLERS", "MacroExpansion", "MacroParseError", "SqlMacroHandler"]`.
     - This is the ONLY file that needs to change when adding a new handler: import the new class, append it to the tuple.

2. **Rewrite [src/vg2c/resolver/sql_macro_expander.py](src/vg2c/resolver/sql_macro_expander.py)** as a thin generic driver:
   - Single import line for handler dispatch: `from vg2c.resolver.sql_macros import HANDLERS, MacroExpansion, MacroParseError`.
   - Keep `expand_sql_macros(...)` signature unchanged (public API consumed by `vg2c.resolver.__init__.resolve`).
   - Keep `_find_matching_paren`, `_split_args`, `_next_sql_call`, `_SqlCallMatch` (generic SQL-text utilities).
   - Replace per-macro logic in `_expand_body` with registry dispatch:
     - `handler = HANDLERS.get(match.name)`
     - `handler is None` → emit `unknown-sql-macro` info diag + leave text untouched.
     - Call `outcome = handler.build_call(args, placeholder, span, body[:match.start])`.
     - `isinstance(outcome, MacroParseError)` → emit `sql-macro-parse-failed` warning diag + leave text untouched.
     - Otherwise → append placeholder + `outcome.appended_text`, record `outcome.call`, normalize+register `outcome.consumed_csv_path`, emit `sql-macro-csv-unknown-producer` info diag if path is unknown.
   - Compile `_SQL_CALL_RE` once at module scope; remove the duplicate inside `_next_sql_call`.
   - Hoist `_SCANNED_KINDS = {Kind.MARS_READ, Kind.OASYS_READ, Kind.ARIES_READ, Kind.SQLITE_QUERY}` to module scope.
   - Collapse the 3 `Diagnostic(...)` constructions into a tiny `_diag(severity, code, message, block_index, span)` helper.
   - Use `len(calls)` for placeholder index instead of a separate `macro_index` counter.

3. **Widen [src/vg2c/resolver/models.py](src/vg2c/resolver/models.py)** `SqlMacroCall.name`:
   - Change `name: Literal["SQL_Get_CSV_List"]` to `name: str`.
   - This is the minimum model change needed to let future handlers register new names.

4. **Verify** — see Verification.

## Resulting File Layout

```
src/vg2c/resolver/
  __init__.py                  (unchanged — still imports expand_sql_macros)
  macro_resolver.py            (unchanged)
  models.py                    (one-line widen of SqlMacroCall.name)
  scope_builder.py             (unchanged)
  sql_macro_expander.py        (rewritten — generic driver only)
  sql_macros/                  (NEW package)
    __init__.py                (NEW — HANDLERS registry + package re-exports)
    base.py                    (NEW — SqlMacroHandler, MacroExpansion, MacroParseError, unquote_arg, parse_column_ref)
    sql_get_csv_list.py        (NEW — SqlGetCsvListHandler + _CALL_SITE_WRAP_RE)
```

## Extension Recipe (becomes part of base.py docstring)

To add support for a new SQL macro (e.g. `SQL_Time_Range`):
1. Create `src/vg2c/resolver/sql_macros/sql_time_range.py`.
2. Subclass `SqlMacroHandler`, set `name = "SQL_Time_Range"`, implement `build_call`.
3. In `sql_macros/__init__.py`, import the class and add an instance to the `HANDLERS` tuple.
No edits to `sql_macro_expander.py` required.

## Relevant files

- `src/vg2c/resolver/sql_macro_expander.py` — rewrite as generic driver. Drop `_unquote`, `_parse_column_ref`, `_CALL_SITE_WRAP_RE`, the module-level `SQL_CALL_RE` duplicate. Now imports from `vg2c.resolver.sql_macros`.
- `src/vg2c/resolver/sql_macros/__init__.py` — NEW. Defines `HANDLERS` mapping; only place to edit when adding a new handler.
- `src/vg2c/resolver/sql_macros/base.py` — NEW. `SqlMacroHandler`, `MacroExpansion`, `MacroParseError`, shared helpers `unquote_arg` / `parse_column_ref`.
- `src/vg2c/resolver/sql_macros/sql_get_csv_list.py` — NEW. `SqlGetCsvListHandler` + private `_CALL_SITE_WRAP_RE`.
- `src/vg2c/resolver/models.py` — widen `SqlMacroCall.name` Literal → str.
- `src/vg2c/resolver/__init__.py` — no change (still imports `expand_sql_macros` and `SqlMacroCall`).
- `tests/resolver/test_sql_macro_expander.py` — no change; existing public-API tests must still pass.

## Verification

1. Run `.venv\Scripts\python -m pytest -q` — all existing tests must pass (especially `tests/resolver/test_sql_macro_expander.py` covering column-by-name, column-by-index, multi-call, unknown-macro, unknown-producer, call-site-wrap, and unwrapped-call-site cases).
2. Run `.venv\Scripts\python -m pytest tests/resolver/ tests/dataflow/ tests/emitter/ -q` to confirm downstream consumers of `SqlMacroCall` still work end-to-end.
3. Optional smoke check: import `from vg2c.resolver.sql_macros import HANDLERS` and assert `"SQL_Get_CSV_List" in HANDLERS`.

## Decisions

- **`sql_macros/` package, one file per handler.** Per user preference: each specific macro's logic is fully encapsulated in its own file (`sql_get_csv_list.py`, future `sql_time_range.py`, etc.). Shared base class + helpers live in `base.py`. The expander only knows about the `HANDLERS` mapping exposed from the package `__init__.py`.
- **`HANDLERS` is an explicit dict in `__init__.py`**, populated by instantiating each handler class. Adding a macro = one new file + two lines in `__init__.py` (import + tuple entry). A decorator-based auto-registration is possible later (see Further Considerations) but explicit is clearer for now.
- **`_CALL_SITE_WRAP_RE` stays inside `sql_get_csv_list.py`** — it's specific to SPF's `(<col> In SQL_Get_CSV_List(...)` wrap. Other macros likely won't share this exact pattern; if they do, promote it to `base.py` later.
- **Shared helpers (`unquote_arg`, `parse_column_ref`) live in `base.py`** — these will be reused by most future handlers (SQL args are commonly quoted).
- **Widen `name` to plain `str`, not a wider `Literal`.** New handlers register any string. Type narrowness is not load-bearing — emitter/analyzer match on the string explicitly.
- **Keep `_find_matching_paren` / `_split_args` in the expander.** They are SQL-text utilities, not macro-specific.
- **Emitter (`src/vg2c/emitter/handlers.py`) and dataflow analyzer (`src/vg2c/dataflow/analyzer.py`) are out of scope.** They will need similar handler-registry treatment when a second macro lands. Flagged as follow-up.
- **No new tests added.** The refactor is behavior-preserving; existing tests cover the contract.

## Further Considerations

1. **Should the emitter dispatch (`handlers._sql_macro_expr`) also become registry-driven now, or wait for the second macro?**
   - A. Defer until a second macro is concrete (recommended — avoids speculative abstraction).
   - B. Add an `emit_runtime_call(call) -> str` method to `SqlMacroHandler` now, so each handler in `sql_macros/` owns both its parse AND its emitted runtime call. This is the natural next step but expands this refactor's scope.
2. **`HANDLERS` as dict literal vs `@register` decorator.** A plain dict literal in `__init__.py` is simplest today. A `@register("SQL_X")` decorator (handlers self-register on import; `__init__.py` just imports each module) is nicer once there are 5+ handlers. Defer until then.
3. **`MacroParseError` vs raising an exception from handlers?** Returning a value (planned) keeps the driver loop simple and avoids exception-as-control-flow.
