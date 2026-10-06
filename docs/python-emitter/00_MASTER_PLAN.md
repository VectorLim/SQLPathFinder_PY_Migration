# Minimal Clean-Python Emitter Plan

## Baseline

This plan is based on `AED-integration` at `db18ff7dc709a75e8468e11b6f9c93c58daa6d40`.

The current branch already has the correct runtime authority:

```
VG2 text
  -> worker / PortableScriptHostRuntime
  -> original SPFManager / original task classes / original Utilities / SPFGlobals
  -> current portability overrides
```

That authority must not be replaced.

The compiler code on `main` is useful for parsing, scope reconstruction, emission metadata, and editable Python generation, but its embedded/reimplemented runtime is not the runtime we want to restore.

## Goal

Compile supported VG2 jobs into normal, readable Python that a user can edit without knowing the ScriptHost backend.

Target shape:

```python
from scripthost_portable.script_api import (
    aed,
    macros,
    query,
    reports,
    script_session,
    utilities,
)

OPERATION = "2303"
DURATION = "TRUNC(SYSDATE) - 2"

MACRO_OVERRIDES = {
    "OPERATION": OPERATION,
    "DURATION": DURATION,
}

def run():
    with script_session(macro_overrides=MACRO_OVERRIDES):
        for _ in macros.from_csv("configsets.csv"):
            query.run(
                sql="""SELECT ...""",
                node=macros["MARS"],
                output="PARMI_IPM_RAW.csv",
            )

            utilities.rows_in_file(
                path="PARMI_IPM_RAW.csv",
                variable="RowsInFile",
            )

            if macros.compare("RowsInFile", "GT", "0"):
                query.run(
                    sql="""SELECT ...""",
                    engine="SQLite",
                    output="AED_CANDIDATES.csv",
                )

                utilities.rows_in_file(
                    path="AED_CANDIDATES.csv",
                    variable="SIGNAL",
                )

                if macros.compare("SIGNAL", "GT", "0"):
                    aed.process("AED_CANDIDATES.csv")

if __name__ == "__main__":
    run()
```

`for _ in macros.from_csv(...)` is intentional. Original `START-MACRO` iterates rows; a `with macros.from_csv(...)` API would incorrectly model multi-row macro files.

## Non-negotiable design rules

1. **One semantic runtime authority.** Original ScriptHost remains responsible for actual utility, task, query, macro-comparison, and report behavior wherever practical.
2. **No exposed `ctx`.** Generated code imports small public facade objects directly.
3. **No `step_000x()` functions.** A normal VG2 leaf emits directly into `run()`.
4. **No generated metadata comments or markers.** Source ranges and semantic identity stay in compiler data structures, not in user-facing Python.
5. **No embedded runtime.** Do not restore `vg2c.embedding` or copy utility implementations into generated files.
6. **No new macro engine.** Do not restore main's `MacroState` as runtime authority.
7. **No duplicate query/report engine.** Generated calls must ultimately reach the original ScriptHost task/runtime behavior and current portability overrides.
8. **Current jobs first.** Unsupported constructs fail compilation clearly. They do not emit `pass` and do not trigger a generalized framework.
9. **Manual Python editability first.** Rebuilding a visual editor or generic semantic-editing service is not required to get the new path running.
10. **Prefer small functions and direct calls.** Do not introduce manager/service/provider/facade registries unless an actual requirement forces them.

## Current production-shaped scope

The checked-in `ICMPCS.txt` and migrated `output/aed-migration/CSR_IAM_v2.aed.txt` define the first supported surface.

Both contain:

- 2 external Oracle/VA query blocks;
- 3 SQLite query blocks;
- `HTML-RUN` x1;
- `HTML-DEFER` x1;
- `HTML-LAYOUT` x2;
- `HTML-DELETE` x2;
- `START-MACRO`;
- `ROWS-IN-FILE`;
- `IF-THEN`;
- `AED`;
- matching end-control tokens.

The CSR job has two row-count checks and two conditions; ICMPCS has one of each.

This is the initial compatibility target. Historical utilities such as email, robocopy, wait-file, run-loop, arbitrary external utilities, and full legacy SQLPathFinder coverage are deferred until a real migrated job needs them.

## What to reuse from `main`

### Reuse with small changes

- `vg2c.frontend`: parsing and classification.
- `vg2c.kind`.
- `vg2c.operands`: scope-tree construction.
- `vg2c.resolver`.
- `vg2c.dispatch`: query/reader classification where it remains useful.
- `vg2c.emitter.indent_writer`.
- emitter metadata that is independent of `ctx`: `CodeExpr`, parameter definitions, rendered arguments/calls, source ranges, emitted invocation metadata.
- compiler-side helpers such as utility token parsing, table binding, SQL placeholder scanning, and literal/global extraction when they do not execute runtime semantics.
- focused tests from main that verify parsing, scope structure, source ranges, and stable parameter identity.

### Do not restore as production runtime

- `vg2c.embedding`.
- `PipelineContext`.
- main's runtime `MacroState`.
- main's independent CSV/query/report/filesystem/mail utility implementations.
- utility source embedding and dependency closure.
- generated `ctx.*` calls.
- per-block generated step functions.
- SQL-filter comments or generated workflow markers.
- `vg2c_ui` or editor services.
- broad backward-compatibility shims.

### Reuse from `AED-integration`

- `PortableScriptHostRuntime` setup conventions.
- original vendored `SPFManager`, task classes, `Utilities`, `SPFGlobals`, and `MemTable`.
- `query_transport.py` and portability overrides.
- `aed_api.py`.
- existing worker isolation model and test fixtures.

## Minimal architecture

```
                 compiler only
VG2 -> parse -> resolve -> emit readable Python
                              |
                              v
              scripthost_portable.script_api
                 |       |       |       |
              macros  query  reports  utilities
                 \       |       |      /
                  hidden current session
                          |
                    original SPFManager
                          |
              original tasks / Utilities
                          |
                portability overrides
```

The compiler and runtime must remain separable:

- compiling imports `vg2c`;
- generated Python imports only `scripthost_portable.script_api`;
- executing generated Python must not import `vg2c`.

## Stages

### Stage 1 — prove the thin script API

See `01_STAGE_1_SCRIPT_API.md`.

Build only enough public API to execute hand-written clean Python against the original backend. Do not restore the compiler yet.

Exit condition: a hand-written Python translation of the current job subset can exercise the same original backend semantics in tests.

### Stage 2 — restore the compiler and emit clean Python

See `02_STAGE_2_COMPILER_EMITTER.md`.

Restore only the compiler pieces needed for the actual job subset and change emission so it writes direct calls and Python control flow.

Exit condition: both target VG2 jobs compile to valid readable Python with no `ctx`, no step wrappers, no runtime embedding, and no generated metadata comments.

### Stage 3 — differential parity and cutover

See `03_STAGE_3_PARITY_CUTOVER.md`.

Compare current direct ScriptHost execution against generated-Python execution and only then make the Python path deployable.

Exit condition: the supported jobs have meaningful output/control-flow parity and unsupported features fail explicitly.

## Deliberately deferred

These are not prerequisites for the first working version:

- full historical utility coverage;
- `RUN-LOOP`;
- a visual editor;
- automatic generated-Python mutation;
- restoring all of `editing.py` / `semantics.py`;
- standalone generated files containing their own runtime;
- plugin architecture;
- generalized adapter registry;
- dynamic `__getattr__` forwarding to the monolithic Utilities class;
- refactoring the vendored ScriptHost code itself.

If automated Python editing is required later, reuse main's source-range/stable-binding ideas after runtime parity is established. Do not add that complexity to the first cut.
