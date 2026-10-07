# Stage 2 — Restore Compiler Core and Emit Clean Python

## Goal

Reuse the proven compiler work from `main` to translate the current VG2 job subset into readable Python that calls Stage 1's public script API.

This stage changes code generation, not runtime semantics.

## Implemented Stage 2 boundary

The compiler now uses `parse -> classify -> resolve -> emit` through
`vg2c.compile_document()`, with `translate()` / `python -m vg2c` writing output.
There is no runtime dispatch stage: Stage 1 receives the original SQL, table
bindings, crosstab syntax and option values, then delegates preprocessing to the
original backend. The reader hierarchy from `main` is not required for this path.

Reused/adapted core: frontend block/options/source-span models and separator
parsing, source-indexed `ScopeNode` opener/closer traversal, compiler facade,
indentation writer and the small source-range/invocation/parameter identity
models. `sql_lexer.py` is reused unchanged for narrowly selected editable settings.
The utility registry, descriptor/editor schemas, resolved text copies, operand
runtime emission, step wrappers and embedding are not restored.

One macro scope per job is supported, matching the actual Stage 1 API. The
compiler also rejects ordinary macro references after `END-MACRO`, because the
facade retains its table until cleanup while original VG2 limits substitution
to that scope. This is a compiler boundary, not a runtime lifecycle redesign.
Unnamed positional macros, ELSE, RUN-LOOP and nonempty second comparison clauses
remain unsupported. Malformed/duplicate options and unmatched control tokens
raise source-located compile errors instead of best-effort output.

Utility arguments use the original CSV quoting rules. `ROWS-IN-FILE` supports
only full counting (`N`) and an empty archive name, matching its public facade.
Utility `INSTANCE`, current-directory `WORKDIR` and `OUTLOOK=N` wrappers do not
affect these selected task commands. Utility `PROMPT-TEXT` is omitted (logging
only); query and report prompts remain explicit arguments.

SQL and report templates retain their readable content, including original
`SQL_Get_CSV_List`, `CrossTab->`, schema and named macro references. Only equality
predicates for `operation` and the observed simple `out_date >=` date expressions
are promoted to plain constants. Their default evaluated SQL is unchanged;
comment/string contents and longer arithmetic expressions are not promoted.
ICMPCS uses `TRUNC(SYSDATE) - 2`; CSR uses `SYSDATE - 1`. CSR's two comparisons
are sibling scopes, and AED belongs to its first comparison.

`EmittedScript.blocks` keeps stable block/invocation/parameter IDs, original
source spans and generated character ranges. Generated source has no metadata
comments, editor framework, generic context, session API or runtime imports
beyond the five public objects. The installed runtime and launcher remain unchanged.

## Restore only compiler-side code

Start from the `main` implementations and preserve them where they are already correct.

Likely required:

```
src/vg2c/
  compilation.py
  kind.py
  logger.py
  frontend/
  operands/
  resolver/
  dispatch/
  emitter/
  utility_metadata.py
```

Restore only compiler-side utility handlers/helpers needed to classify and emit the supported block types.

Do not copy the entire old `utilities/` runtime package back wholesale.

## Explicitly exclude

Do not restore these as runtime dependencies:

```
vg2c/embedding/
vg2c/utilities/pipeline_context.py
vg2c/utilities/macro_state.py        # runtime implementation
vg2c/utilities/csv_io.py             # runtime implementation
vg2c/utilities/oracle_client.py       # runtime implementation
vg2c/utilities/html_report.py         # runtime implementation
```

Compiler-only parsing/helper logic from those files may be extracted when useful, but executing generated code must never depend on those implementations.

## Direct block emission

Main currently wraps each leaf in:

```python
def step_0004_rows_in_file(ctx):
    ...
```

and then calls that function from `run()`.

Remove that layer.

The walker should write leaf source directly into the current indentation level.

Conceptually:

```python
def run():
    if macros.load_csv("configsets.csv"):
        query.run(...)
        utilities.rows_in_file(...)

        if macros.compare("RowsInFile", "GT", "0"):
            ...
```

No `step_000x` function declarations and no call sites.

## Internal emission metadata

Do not put comments or markers into generated Python.

Keep source identity in compiler objects.

Reuse main's useful metadata concepts:

- `SourceRange`;
- `ParameterDefinition`;
- `RenderedArgument`;
- `RenderedCall`;
- `EmittedInvocation`;
- `EmittedParameter`.

Replace only the step-specific shell that assumes generated functions.

A small `BlockEmission` / `EmittedBlock` model is acceptable if needed:

```
block_index
functional_kind
source
invocations
source_range
```

Do not introduce a second metadata framework.

Stable IDs remain based on block/invocation/parameter identity, not comments in the Python source.

## Remove `ctx` from call rendering

Main's `EmittableOperation.render_method_call()` currently hardcodes:

```
ctx.<utility>.<method>(...)
```

For the restored compiler, the receiver is simply the public script API object:

```
query.run(...)
utilities.rows_in_file(...)
macros.set(...)
reports.layout(...)
aed.process(...)
```

Use the existing `utility_name`/operation metadata rather than designing a generalized receiver/provider abstraction.

## Control-flow emission

### START-MACRO

Do **not** reuse `main`'s `StartMacro.prompt_off` interpretation or its independent `MacroState.scope(...)` runtime. The original `StartMacroTask` loads the macro file, uses `Rowidx=1`, and executes its child scope once. Its second utility argument is `ContinueOnError`.

Emit a normal function call and ordinary Python condition:

```python
if macros.load_csv("configsets.csv"):
    ...
```

For `START-MACRO ... "Y"`, emit:

```python
if macros.load_csv("configsets.csv", continue_on_error=True):
    ...
```

The facade keeps original `MemTable` / `Substitute_Macro` mechanics hidden. Values inside the body use simple lookups such as `macros["MARS"]`. No generated code may contain `parentMacTables`, `Rowidx`, `MyMode`, or direct `Substitute_Macro(...)` calls.

The current targets contain only one macro scope. Treat nested `START-MACRO` as unsupported in the first compiler cut rather than inventing lifecycle machinery before a real target requires it.

### IF-THEN

Do not rebuild comparison rules as native Python operators.

Emit calls to the Stage 1 macro facade so original `CompareVars` semantics stay authoritative:

```python
if macros.compare("RowsInFile", "GT", "0"):
```

For multi-clause conditions, first characterize original behavior and then emit the smallest equivalent facade call. Do not generalize ahead of a real target case.

### End tokens

`END-MACRO` and `END-IF` remain structural only. They do not emit calls.

### RUN-LOOP

Not in the first supported scope. Compilation must report it as unsupported rather than using main's independent chunk iterator.

## Query emission

Reuse main's parsing/classification work for:

- identifying Oracle/VA vs SQLite blocks;
- SQL/body extraction;
- table input parsing;
- headers;
- crosstab option parsing;
- placeholder/global discovery where useful.

Change only the emitted runtime call.

Do not emit main's reader objects or `PipelineContext.run_query`.

Emit `query.run(...)` with the current block's supported options. Preserve values required by the original task path instead of dropping them.

The first version only needs to cover options observed in:

- `ICMPCS.txt`;
- `output/aed-migration/CSR_IAM_v2.aed.txt`.

If a block contains an unhandled option that may affect behavior, fail compilation with a diagnostic.

## Report emission

Reuse classification of `/REPORT=HTML-*`, but emit:

```
reports.run(...)
reports.defer(...)
reports.layout(...)
reports.delete(...)
```

The template/body remains readable data in the generated call. Runtime execution goes to Stage 1's original-task-backed facade.

## ROWS-IN-FILE and AED

Emit:

```python
utilities.rows_in_file(path=..., variable=...)
aed.process(...)
```

Do not reproduce their logic in compiler modules.

## User-editable constants

Reuse main's useful literal/global extraction where it produces genuinely helpful top-level values.

Keep generated settings plain:

```python
OPERATION = "2303"
DURATION = "TRUNC(SYSDATE) - 2"
```

Do not emit banner comments around them.

Only promote values that are actually useful to edit. Avoid turning every literal into a global.

## Generated source requirements

Generated source must:

- parse with `ast.parse`;
- import only public runtime API from `scripthost_portable.script_api`;
- not import `vg2c`;
- not import `SPFLib` or `_vendor`;
- not import portability override modules directly;
- contain no `ctx`;
- contain no `step_000x` definitions;
- contain no generated metadata comments/markers;
- contain one normal `run()` entrypoint;
- be understandable as ordinary hand-written Python.

## Tests to reuse/adapt from main

Bring across focused tests for:

- parser/classifier behavior;
- scope nesting;
- SQL classification;
- source ranges;
- stable parameter identity;
- duplicate/repeated operation identity where relevant;
- emitted code syntax;
- no runtime import leakage.

Replace assertions that expect:

- embedded utility source;
- `ctx.*`;
- step functions;
- generated markers/comments.

Add golden-shape tests for the actual ICMPCS and migrated CSR job.

## Exit criteria

Stage 2 is complete when both target files compile successfully and the generated Python is structurally clean.

Execution parity is Stage 3; do not hide parity failures by expanding the compiler architecture in Stage 2.
