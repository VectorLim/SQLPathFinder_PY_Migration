# Stage 3 — Differential Parity and Production Cutover

## Goal

Prove that the clean generated-Python path behaves like the current direct ScriptHost path for the supported jobs, then make that path deployable without weakening the existing runtime.

## Differential test model

Use the current direct runtime as the oracle:

```
same VG2 source
   |
   +--> current PortableScriptHostRuntime ----------> baseline
   |
   +--> restored compiler -> generated Python ------> candidate
                                |
                                -> script_api
                                -> original SPFManager/tasks/Utilities
```

Compare observable effects, not merely exit status.

## Required parity checks

For the supported ICMPCS and migrated CSR jobs compare:

- files created/changed;
- CSV/table contents where deterministic;
- query backend selected;
- query SQL after the same legacy preprocessing path;
- row-count results;
- macro-driven branch decisions;
- `macros.load_csv()` activation/skip result, `macros["NAME"]` values, and confirmation that only row 1 is applied;
- HTML/CSS/report artifacts;
- AED candidate file handed to `aed_api`;
- success/failure category for induced failures;
- meaningful logs where they signal control behavior.

Do not perform a production AED write merely to prove compiler parity. Mock/dry-run the AED side unless a separately approved controlled live test is intended.

## Characterization tests before changing behavior

Where the adapter boundary is uncertain, capture current behavior first.

Priority cases:

1. first-row-only macro behavior, `macros["NAME"]` lookup parity, and empty/missing-file handling;
2. macro/environment/explicit-override precedence;
3. `CompareVars` numeric vs string operators;
4. query option preprocessing and output/header behavior;
5. report state across RUN -> DEFER -> LAYOUT -> DELETE;
6. error propagation from original tasks through the facade.

If generated Python disagrees, fix the adapter/emission mapping before changing original runtime code.

## Failure policy

Unsupported or unproven input must fail early.

Examples:

- unsupported utility token;
- `RUN-LOOP` before it is implemented;
- query option not covered by the adapter and potentially semantic;
- unknown report type;
- malformed macro control nesting;
- nested `START-MACRO` before its lifecycle has been explicitly implemented.

Never emit `pass` for unsupported work.

## Packaging and execution

Keep generated Python dependent on the installed `scripthost_portable` package.

Do not make generated files standalone by embedding the backend.

Minimal deployment shape:

```
generated_job.py
scripthost_portable/
vendored original ScriptHost runtime
current portability overrides
```

The generated script can run as a normal Python entrypoint:

```
python generated_job.py
```

Use the existing one-job-per-process assumption. Do not add an in-process scheduler or concurrency framework.

If imserverless needs a launcher wrapper, adapt the existing launcher minimally rather than introducing a second orchestration layer.

## Cutover sequence

1. Run the full existing `AED-integration` test suite unchanged.
2. Run new Stage 1 facade tests.
3. Compile both target VG2 jobs.
4. Run emitted-source shape tests.
5. Run offline differential parity tests.
6. Run approved live query-only validation using the current portability transport.
7. Only after parity is acceptable, point one non-production job invocation at generated Python.
8. Preserve the direct VG2 runtime path until the Python path has enough operational evidence to replace it.

## Definition of done

The first clean-Python implementation is done when:

- both target VG2 jobs compile;
- generated code has no `ctx`;
- generated code has no per-step functions;
- generated code has no generated metadata comments;
- generated code is manually editable;
- runtime calls still reach original ScriptHost semantics/current overrides;
- offline parity is meaningful and green for the supported paths;
- unsupported constructs fail explicitly;
- no main-branch runtime reimplementation has been restored.

## Follow-up only after cutover

Only after the above is stable should we consider:

- additional utilities based on real migrated jobs;
- automated Python edits using main's source-range/stable-binding ideas;
- a UI over those bindings;
- wider SQLPathFinder compatibility.

Those are extensions, not prerequisites for the minimal working architecture.
