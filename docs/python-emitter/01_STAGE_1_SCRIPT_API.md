# Stage 1 — Thin Script API Over the Original Runtime

## Goal

Prove that readable Python can drive the existing ScriptHost backend without recreating ScriptHost semantics.

No compiler work belongs in this stage.

## Scope

Add one small public module initially:

```
src/scripthost_portable/script_api.py
```

Keep it as one module until its size or test boundaries justify splitting it. Do not create a package hierarchy pre-emptively.

Public names:

```python
script_session
macros
utilities
query
reports
aed
```

Generated/user code must never import `_vendor`, `SPFLib`, `query_transport`, or override modules directly.

## Session model

The current runtime already assumes one job per fresh process. Reuse that constraint.

Use a small private current-session value inside `script_api.py` rather than passing `ctx` through every call.

```python
def run():
    with script_session(macro_overrides=MACRO_OVERRIDES):
        ...
```

The session owns one original `SPFManager` instance so query/report/macro state can survive across calls.

Avoid a new session manager/service abstraction.

### Small runtime refactor allowed

`PortableScriptHostRuntime.run_text()` currently contains the manager construction and command-line initialization.

Factor that setup into one private helper in `runtime.py` and reuse it from both:

- `PortableScriptHostRuntime`;
- `script_session()`.

Do not duplicate the initialization logic.

## Facade behavior

### `macros`

Minimum public surface:

```python
macros[name]
macros.get(name)
macros.set(name, value)
macros.compare(lhs, op, rhs)
macros.substitute(text)
macros.from_csv(path)
```

Rules:

- comparison delegates to original `Utilities.CompareVars`;
- substitution delegates to original `Utilities.Substitute_Macro` or the same original primitives it uses;
- CSV loading reuses original `MemTable` behavior rather than pandas or a new CSV macro implementation;
- `from_csv(path)` is an iterator because `START-MACRO` is a row loop;
- each yielded iteration installs the current row into the hidden macro state and removes it afterwards;
- nested macro CSV scopes must use the original nested-parent semantics, not a new independent stack if the original primitives can be reused;
- user `macro_overrides` are explicit job inputs, not a second macro engine.

Initial generated form:

```python
for _ in macros.from_csv("configsets.csv"):
    ...
```

Do not emit a fake one-row `with` scope.

### `utilities`

Implement only the utility needed by the two target jobs first:

```python
utilities.rows_in_file(path, variable)
```

Reuse original methods:

- `getRowCountFromFile(...)`;
- `setEnv(...)`.

Do not port main's `CsvIO.row_count` implementation.

Keep the adapter close to the original parameter contract. Only hide parameters that are truly backend/session plumbing.

### `query`

Minimum public shape:

```python
query.run(sql=..., ...)
```

Do not execute SQL through main's `SqliteEngine`, `OracleClient`, or `PipelineContext`.

Preferred implementation order:

1. reuse the same original `SPFManager.Process_Query` / original task construction path on the session's existing manager;
2. execute the resulting original task on that same manager/session;
3. allow current query portability overrides to be reached naturally.

If isolated `Process_Query` use is not safe, call the original concrete task class directly. Do not respond by reimplementing query semantics.

The public arguments should mirror the actual options needed by the current jobs, normalized to readable snake_case names. Examples include:

- `node`;
- `oledb`;
- `engine`;
- `output`;
- `tables`;
- `headers`;
- current crosstab fields;
- `record`;
- the few current flags such as reset/quote/t.

Do not design a complete SQLPathFinder option schema. Unknown compiler input must fail instead of being silently discarded.

Internally, it is acceptable for the adapter to reconstruct one legacy OPTIONS/body block and feed it to the original parser/task machinery. That is reuse, not duplication, provided the generated Python exposes normal parameters rather than raw legacy blocks.

### `reports`

Minimum surface:

```python
reports.run(...)
reports.defer(...)
reports.layout(...)
reports.delete(...)
```

These must route to original report task behavior because the original report path is stateful.

Do not use main's independent `HtmlReport` implementation.

As with query calls, reconstructing a small original report block internally is acceptable if that is the cleanest way to reuse the original task machinery.

### `aed`

Use the already-current implementation:

```python
aed.process("AED_CANDIDATES.csv")
```

Delegate directly to `scripthost_portable.aed_api.process_candidates`.

Do not create another AED adapter layer beyond the public call needed by generated code.

## Macro overrides

Keep user-edited values obvious at the top of generated files:

```python
OPERATION = "2303"
DURATION = "TRUNC(SYSDATE) - 2"

MACRO_OVERRIDES = {
    "OPERATION": OPERATION,
    "DURATION": DURATION,
}
```

`script_session(macro_overrides=...)` installs them using the same macro/environment mechanisms used by the backend.

Do not invent precedence rules. Add characterization tests against the existing runtime before finalizing override precedence relative to macro CSV rows and environment values.

## Tests

Add focused tests around behavior, not implementation shape.

Required:

- session creates and reuses one original manager;
- session restores cwd/state on exit;
- row count matches original `ROWS-IN-FILE`;
- `CompareVars` parity for the operators used by current jobs;
- macro CSV iterates every row;
- nested macro substitution behavior is characterized;
- a query adapter call reaches the existing portable query transport through original task handling;
- report calls preserve state across run/defer/layout/delete;
- AED facade calls existing `aed_api.process_candidates`;
- using any facade outside `script_session()` fails clearly.

## Exit criteria

Stage 1 is complete when a small hand-written Python script using only the public facade can express the current job flow and its calls reach the original backend.

Do not restore the compiler until this is proven.
