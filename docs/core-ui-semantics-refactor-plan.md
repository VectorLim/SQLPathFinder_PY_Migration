# Compiler-Centric Semantic Ownership Refactor — Implementation Plan

Status: proposed implementation plan

Baseline reviewed: `main` at `7192a63a7316b5a53aed17ce2eff7e388eadc20f` (2026-09-03)

This document replaces the earlier workflow-layer proposal. The revised design deliberately does **not** introduce a `vg2c.workflow` domain hierarchy or a `WorkflowEngine` that mirrors compiler state.

The compiler stage/result chain is already the natural semantic model. The refactor should enrich that chain where information first becomes authoritative, then expose it through a small set of core functions and thin HTTP serialization.

---

## 1. Current architecture and duplication points

### 1.1 Existing compiler chain

The current core pipeline is already cumulative:

```text
VG2 source
   ↓
parse
   ↓
ParsedBlock
   ↓
classify
   ↓
ClassifiedBlock
   ↓
resolve
   ↓
ResolvedProgram / ResolvedBlock / scope tree
   ↓
analyze
   ↓
AnalyzedProgram
   ├─ resolved program
   ├─ producers
   ├─ consumers
   └─ dataflow edges
   ↓
dispatch
   ↓
DispatchedProgram / DispatchedBlock
   ├─ analyzed program
   ├─ reader selection
   ├─ reader kwargs / target
   ├─ rewritten SQL
   └─ SQL filter information
   ↓
emit
   ↓
EmittedScript
   └─ generated Python source
   ↓
CompilationResult
```

`CompilationResult` already aggregates the important stage outputs: resolved state, analyzed dataflow, dispatched state, generated Python and diagnostics. This is the correct authoritative chain to extend.

### 1.2 Where semantic duplication currently occurs

The compiler stops short of exposing enough information for safe editing, so `vg2c_ui` rebuilds it.

Current backend duplication:

- `src/vg2c_ui/services/python_document.py`
  - reparses generated Python;
  - rediscovers utility calls;
  - rediscovers arguments;
  - infers editable literal types;
  - assigns parameter IDs using generated call order;
  - calculates generated-source offsets.
- `src/vg2c_ui/services/utility_catalog.py`
  - reinspects `UtilitySpec` and `@emittable` methods;
  - rebuilds utility/method/parameter metadata for the UI.
- `src/vg2c_ui/services/workflow_builder.py`
  - combines compiler results, reparsed Python and utility metadata into a second semantic representation (`WorkflowDocument`).
- `src/vg2c_ui/domain/models.py`
  - defines a parallel hierarchy for steps, parameters, artifacts, diagnostics and utilities.
- `src/vg2c_ui/services/command_service.py`
  - owns semantic validation and generated-source patching that should belong to the converter/editor core.

Current browser duplication:

- `src/vg2c_ui/frontend/src/dependencyValidation.ts` rebuilds producer/consumer relationships and validates broken/missing/duplicate dependencies.
- `src/vg2c_ui/frontend/src/dataFlow.ts` reconstructs file flow and guesses header semantics from parameter names.
- `src/vg2c_ui/frontend/src/sql/operation.ts` recognizes SQL kinds and hardcodes `sql`, `inputs` and `output` parameter meaning.
- `src/vg2c_ui/frontend/src/operationLabels.ts` hardcodes operation kinds and identifying parameter-name heuristics.
- `src/vg2c_ui/frontend/src/sql/parser.ts`, `transform.ts` and `model.ts` own SQL parsing and mutation semantics in TypeScript.
- `src/vg2c_ui/frontend/src/sql/metadata.ts` defines a second capability model that is not owned by the core utility system.
- `src/vg2c_ui/frontend/src/types.ts` manually mirrors backend models.

The current architecture therefore has four representations of overlapping workflow meaning:

```text
compiler stages
    +
WorkflowDocument/backend domain models
    +
generated-Python AST rediscovery
    +
frontend semantic reconstruction
```

The refactor removes the last three as semantic authorities.

---

## 2. Revised target architecture

```text
VG2 source
   ↓
parse
   ↓
classify
   ↓
resolve
   ↓
analyze
   ↓
dispatch
   ↓
emit
   ↓
CompilationResult
   ├─ resolved semantic blocks and scopes
   ├─ producers / consumers / dataflow edges
   ├─ dispatched reader/utility information
   ├─ utility operation definitions
   ├─ emitted invocations and arguments
   ├─ editable parameter metadata
   ├─ stable semantic parameter identities
   ├─ generated source spans
   ├─ capabilities / semantic roles
   ├─ supported modifications
   ├─ diagnostics
   └─ emitted Python
          ↓
small vg2c editing/inspection functions
          ↓
vg2c_ui
   ├─ workspace path security
   ├─ revision/conflict handling
   ├─ sidecar persistence
   ├─ atomic file writes
   └─ thin HTTP serialization
          ↓
generated TypeScript contract
          ↓
React
   ├─ rendering
   ├─ controls
   ├─ selection/navigation
   ├─ dialogs
   ├─ per-tab draft intent/history
   └─ request/loading/error state
```

### Dependency rule

Allowed:

```text
vg2c_ui Python -> vg2c
React -> HTTP API
```

Forbidden after migration:

```text
vg2c -> vg2c_ui
vg2c_ui semantic models parallel to compiler models
React -> SQL/dataflow semantic implementation
React -> hardcoded utility parameter semantics
```

### Core design rule

Do not create a new object merely because the UI wants a convenient shape.

For every new field, ask which existing stage first knows it correctly. Put it there. `CompilationResult` then aggregates or references the enriched stage output.

A transport serializer may flatten or rename fields for JSON, but it must not calculate new semantic facts.

---

## 3. Authoritative ownership by compiler stage

| Information | Authoritative owner | Reason |
| --- | --- | --- |
| Parsed block identity/order/raw source | parser / `ParsedBlock` | First stage that knows source structure |
| Functional kind | classifier / `ClassifiedBlock` | Classification decision is made here |
| Resolved options/body/macros/scopes | resolver / `ResolvedProgram` | Resolution is authoritative here |
| Baseline artifact producers/consumers | dataflow analysis / `AnalyzedProgram` | Existing analyzer already owns this |
| Scope/order validity of dependencies | dataflow analysis | Existing dataflow semantics |
| Reader/dialect/reader target | dispatch / `DispatchedBlock` | Selected during dispatch |
| Rewritten SQL and detected SQL filters | dispatch | Already produced here |
| Utility method definition | `UtilitySpec` + `@emittable` | Actual runtime/emission utility source |
| Parameter type/default/required/`Literal` choices | Python method signature | No parallel metadata required |
| Non-inferable parameter roles | metadata attached to the utility method | Must live beside the real utility definition |
| Structured SQL capability | metadata attached to the responsible utility operation | Domain capability, not a React condition |
| Exact emitted utility/method call | emitter | Only emitter knows what it generated |
| Exact generated argument source | emitter | Renderer creates it |
| Exact generated argument span | emitter | Writer knows final source placement |
| Editability of an emitted argument | emitter + utility definition | Requires both emitted form and parameter definition |
| Stable parameter identity | emitter, keyed by compiler block + utility method + semantic argument key | Avoid incidental AST call order |
| Generated Python | emitter / `EmittedScript` | Existing owner |
| Draft artifact projection | core dataflow functions using emitted argument roles + change intent | Must not be duplicated in TypeScript |
| Cross-document dependency analysis | `vg2c.dataflow` workspace analysis | Same dataflow domain, composed across compilations |
| SQL parsing and structural transformations | core SQL domain code | A SQL mutation changes workflow meaning |
| Validation of requested mutations | core editing functions | Semantic safety belongs in core |
| Revision/hash/conflict checks | `vg2c_ui` persistence layer | Filesystem/application concern |
| Atomic write | `vg2c_ui` persistence layer | Persistence concern |
| Tab selection/loading/dialogs | React | Presentation/application state |

---

## 4. Exact `CompilationResult` and stage-model enrichments

The goal is to add information to existing outputs, not create a second workflow model.

### 4.1 `EmittedScript` becomes the authoritative generated-code description

Current:

```python
@dataclass(frozen=True, slots=True)
class EmittedScript:
    source: str
    imports: tuple[str, ...]
```

Extend it with stage-specific emission records:

```python
@dataclass(frozen=True, slots=True)
class GeneratedSpan:
    start: int
    end: int
    start_line: int
    start_column: int
    end_line: int
    end_column: int

@dataclass(frozen=True, slots=True)
class EmittedArgument:
    id: str
    name: str
    position: int | None
    source: str
    value: object
    span: GeneratedSpan
    editable: bool
    read_only_reason: str | None
    definition: UtilityParameterDefinition

@dataclass(frozen=True, slots=True)
class EmittedInvocation:
    id: str
    block_index: int
    step_name: str
    utility_name: str
    method_name: str
    capability_ids: tuple[str, ...]
    arguments: tuple[EmittedArgument, ...]
    span: GeneratedSpan

@dataclass(frozen=True, slots=True)
class EmittedStep:
    block_index: int
    step_name: str
    function_span: GeneratedSpan
    invocation_ids: tuple[str, ...]

@dataclass(frozen=True, slots=True)
class EmittedScript:
    source: str
    imports: tuple[str, ...]
    steps: tuple[EmittedStep, ...]
    invocations: tuple[EmittedInvocation, ...]
```

Do **not** add a separate `EmissionManifest` container unless implementation proves it materially simplifies the emitter. The `steps` and `invocations` fields on the existing `EmittedScript` are the manifest.

These classes are emitter-stage facts, not UI models.

### 4.2 Replace `CompilationResult.generated_python` + reconstructed mapping with the actual emitter result

Prefer:

```python
@dataclass(frozen=True, slots=True)
class CompilationResult:
    input_path: Path
    resolved: ResolvedProgram
    analyzed: AnalyzedProgram
    dispatched: DispatchedProgram
    emitted: EmittedScript
    diagnostics: tuple[CompilationDiagnostic, ...]
    utility_definitions: tuple[UtilityOperationDefinition, ...]
```

Repository call sites should use `result.emitted.source`.

Remove the current AST pass in `compile_document()` that reparses `emitted.source` and builds `function_to_block` by matching `step_<index>_*`. The emitter itself already creates each step name while holding its source block, so `EmittedStep.block_index` is authoritative.

Do not keep both `function_to_block` and `EmittedStep.block_index` after migration.

### 4.3 Keep existing stage models rather than copying them into `CompilationResult`

`CompilationResult` should continue to reference:

- `ResolvedProgram`;
- `AnalyzedProgram`;
- `DispatchedProgram`;
- `EmittedScript`.

Do not copy their fields into new `CompilationStep`, `WorkflowStep`, `WorkflowArtifact`, or `WorkflowSnapshot` classes.

Consumer code joins the stages by existing block index / emitted step identity when needed.

### 4.4 Dispatch identity enrichment

`DispatchedBlock.reader_cls` is useful internally but is not a good public semantic identity.

Add a small stable identity alongside it where dispatch chooses the reader, for example:

```python
reader_id: str  # e.g. module-qualified class or registered utility identity
```

Do not create a separate reader descriptor hierarchy. Keep `reader_cls` for runtime emission and `reader_id` for stable inspection/serialization.

### 4.5 Dataflow model reuse

Do not invent `WorkflowArtifact` equivalents.

Continue using:

- `ProducerRecord`;
- `ConsumerRecord`;
- `DataflowEdge`;
- `AnalyzedProgram`.

For draft edits, add core functions that produce another `AnalyzedProgram` with projected producer/consumer paths while retaining the same resolved program. This reuses the existing dataflow result type rather than creating a parallel draft-artifact model.

---

## 5. Emitter semantic/edit manifest design

This is the central refactor because it eliminates generated-Python semantic rediscovery.

### 5.1 Problem in the current renderer

`emittable.render_method_call()` currently treats every Python `str` argument as already-rendered Python source instead of as a string literal.

Call sites therefore pass values such as:

```python
output=repr(output)
reader=inst_expr
sql=triple_quoted_sql_source
```

By the time the UI reparses the generated Python, it reconstructs which values were literals and which were dynamic expressions.

The emitter can remove this ambiguity before rendering.

### 5.2 Introduce one small raw-expression wrapper

Add a tiny emitter value type:

```python
@dataclass(frozen=True, slots=True)
class CodeExpr:
    source: str
    value: object = UNSET
```

Rendering rule becomes:

- ordinary Python values, including `str`, are literals and are emitted with `repr()`/the existing multiline-string formatter;
- `CodeExpr` means "this is already Python source";
- `CodeExpr.value` may carry a safe semantic value when the expression corresponds to an editable domain value;
- `CodeExpr` without a semantic value is dynamic/read-only.

Example SQL emission becomes conceptually:

```python
PipelineContext.run_query.render(
    sql=CodeExpr(sql_source, value=rewritten_sql),
    output=output,
    reader=CodeExpr(reader_expression),
    inputs=inputs,
    header=header,
)
```

This is more explicit than the current string-means-code convention and lets the emitter know the actual edit value without calling `ast.literal_eval()` later.

Audit and migrate all `.render(...)` call sites in `src/vg2c/utilities/` in the same stage. Do not retain mixed semantics where some raw strings mean code and others mean literals.

### 5.3 `@emittable` records arguments while rendering

`EmittableMethod.render()` should produce a tracked rendered call that preserves:

- utility identity;
- method identity;
- ordered arguments;
- semantic parameter names;
- local source ranges for each argument;
- actual values where known;
- utility parameter definition references.

Implementation may use a small `RenderedInvocation` internal emitter helper. It is not exposed as another domain model; it exists only until the invocation is placed into the final `EmittedScript`.

Avoid global collectors/context variables. Metadata should travel explicitly with the emitted statement until final assembly.

### 5.4 Adapt the existing utility emission contract, do not build a new emitter framework

The current path is:

```text
UtilitySpec.emit_block()
    ↓
UtilitySpec._wrap_in_step()
    ↓
walker
    ↓
IndentWriter
```

Refine those existing functions so a step carries tracked statement metadata alongside source text.

The smallest acceptable implementation is:

- utility handlers can still produce plain source lines for non-editable/generated structural code;
- tracked `@emittable` calls preserve invocation metadata;
- `_wrap_in_step()` converts tracked local spans into step-local spans;
- `IndentWriter.write_block()` converts step-local spans into final absolute spans.

Do not introduce a generic IR for every Python statement.

### 5.5 Make final offsets authoritative

`IndentWriter` currently tracks lines but not absolute offsets. Extend it with:

- current character offset;
- current line number;
- the ability to return the final span of a written block.

Use those values to translate tracked invocation/argument local offsets into absolute generated-source offsets.

### 5.6 Remove post-assembly source mutation

`post_process_comments()` currently prepends SQL filter comments **after** source assembly. That would invalidate recorded offsets.

Refactor this so the SQL-filter comment block is computed before final source assembly and written through `IndentWriter` as part of the normal preamble.

After this change, the emitter must never modify `source` after final spans have been recorded.

### 5.7 Stable parameter identity

Delete the current identity scheme:

```text
<function>:<global-call-index>:<pos/kw>
```

Use compiler/emitter ownership instead:

```text
<block-index>:<utility-name>.<method-name>:<invocation-key>:<parameter-name>
```

Rules:

1. `block-index` comes from the compiler block, not generated Python position.
2. `utility-name.method-name` comes from the actual `@emittable` owner.
3. `parameter-name` comes from the method signature.
4. `invocation-key` is omitted/defaulted when a method appears once in a step.
5. If the same method is intentionally emitted multiple times in one block, the utility emitter supplies a short semantic key; do **not** fall back to a global AST call index.

This is the smallest robust improvement required now. It is stable when unrelated generated calls are inserted before an editable invocation.

No UUID/identity registry is needed.

### 5.8 Validation parser, if retained

After emission-manifest migration, AST parsing may remain only for final syntax validation:

```python
ast.parse(candidate_source)
compile(...)
```

It must not be used to discover operations, arguments, ownership, types, IDs or editable spans.

---

## 6. Utility metadata and capability design

### 6.1 No separate `UtilityCatalog` class

The existing `UtilitySpec` registry is already the catalog.

Replace `vg2c_ui.services.utility_catalog.UtilityCatalog` with core functions operating directly on the registry, for example:

```python
def list_utility_operations() -> tuple[UtilityOperationDefinition, ...]: ...

def utility_operation(utility_name: str, method_name: str) -> UtilityOperationDefinition | None: ...
```

These can live beside `UtilitySpec` or in a small `vg2c.utilities.metadata` module if `_base.py` becomes crowded.

Do not create a second registry.

### 6.2 Derive everything inferable

For each `@emittable` method derive:

- utility name;
- class/module name;
- method name;
- class and method descriptions;
- signature order;
- parameter names;
- annotations;
- required/default state;
- `Literal[...]` choices;
- return annotation.

The current UI catalog logic should be ported to core as functions and then deleted from `vg2c_ui`.

### 6.3 Add only non-inferable semantics beside the real method

Add one lightweight decorator/metadata attachment, conceptually:

```python
@emittable
@operation_metadata(
    capabilities=("structured-sql",),
    parameter_roles={
        "sql": QueryText(),
        "inputs": ArtifactInput(kind="csv", cardinality="many"),
        "output": ArtifactOutput(kind="csv", cardinality="one"),
        "header": ArtifactSchema(kind="csv"),
    },
)
def run_query(...):
    ...
```

Exact helper names can change, but the design constraints are fixed:

- metadata is attached to the method itself;
- no external mapping keyed by utility/kind;
- no React component names in core metadata;
- no metadata for facts already available from the signature;
- plain operations should need zero extra declarations.

Useful non-inferable roles are limited to:

- artifact direction/type/cardinality;
- structured-editor capability;
- declared schema/header role;
- special semantic constraints;
- whether insertion/removal is supported once those operations exist.

### 6.4 Catalog and existing steps use the same definition

An existing emitted invocation references the same `UtilityOperationDefinition` that `list_utility_operations()` returns.

Therefore:

```text
existing emitted invocation
    ↓
UtilityOperationDefinition
    ↓
parameter editor
```

and later:

```text
utility menu
    ↓
UtilityOperationDefinition
    ↓
parameter editor
```

use one metadata path.

Do not build a separate "new utility form" model later.

---

## 7. Core mutation/projection API

### 7.1 Prefer functions over a service class

Do **not** add the previously proposed `WorkflowEngine`.

Add one small core module, tentatively `src/vg2c/editing.py`, containing functions and a few immutable request/result dataclasses.

Initial public operations:

```python
compile_document(path) -> CompilationResult

project_changes(
    result: CompilationResult,
    changes: tuple[Change, ...],
) -> ChangeProjection

validate_changes(
    result: CompilationResult,
    changes: tuple[Change, ...],
) -> tuple[CompilationDiagnostic, ...]

preview_changes(
    result: CompilationResult,
    changes: tuple[Change, ...],
) -> EditPreview

apply_changes(
    result: CompilationResult,
    changes: tuple[Change, ...],
) -> AppliedEdit
```

`apply_changes()` means apply to generated source **in memory** and return the candidate/result. Filesystem writes remain in `vg2c_ui` so core stays reusable and testable.

If `validate_changes()` is only a thin extraction from `project_changes()`, implement it as a function delegating to projection rather than creating a validator class.

### 7.2 Minimal change types

Start only with changes that exist:

```python
SetParameter(step_id, parameter_id, value)
SqlAction(step_id, action, payload)
```

`SqlAction` is introduced only when the TypeScript SQL transformations move to core.

Do not define `AddUtility`, `RemoveUtility` or `ReorderUtility` until the emitter/core can implement them safely.

When implemented later, add them to the same change union and reuse utility definitions.

### 7.3 Parameter mutation flow

```text
UI sends SetParameter
   ↓
core locates EmittedArgument by stable ID
   ↓
core validates value against UtilityParameterDefinition
   ↓
core serializes value using emitter rules
   ↓
core replaces the exact argument span
   ↓
core updates emitted argument value/span metadata
   ↓
core projects any affected artifact/dataflow roles
   ↓
core validates final Python syntax + semantic constraints
   ↓
preview/apply result returned
```

The UI never serializes Python literals and never calculates source offsets.

### 7.4 Draft dataflow projection

Add projection functions inside `vg2c.dataflow`, not in a workflow package.

Given a `CompilationResult` plus pending changes:

1. start from existing `AnalyzedProgram.producers` and `.consumers`;
2. locate changed emitted arguments whose utility metadata declares artifact roles;
3. update the corresponding producer/consumer paths;
4. rebuild `producers_by_path` and edges using existing analyzer rules;
5. return an `AnalyzedProgram` containing projected records.

Do not ask React to infer that a parameter called `output` is a producer.

### 7.5 Cross-document projection

Add a small composition function to `vg2c.dataflow`, for example:

```python
def analyze_workspace(
    compilations: Sequence[CompilationResult],
    changes_by_document: Mapping[str, tuple[Change, ...]],
) -> WorkspaceDataflow:
    ...
```

A small `WorkspaceDataflow` result is justified because cross-document relationships do not currently exist in any stage. It should contain only cross-document edges/diagnostics and references to document/block IDs; it must not copy full step/utility models.

This function owns:

- duplicate outputs across open documents;
- broken dependencies after draft output renames;
- missing inputs relative to previously known producers;
- upstream/downstream document relationships.

---

## 8. API serialization and contracts

### 8.1 `vg2c_ui` should not recreate a semantic hierarchy

Delete the concept that `WorkflowDocument` is an independent semantic source.

The API should serialize selected fields directly from:

- `CompilationResult.resolved`;
- `CompilationResult.analyzed`;
- `CompilationResult.dispatched`;
- `CompilationResult.emitted`;
- utility operation definitions;
- compiler diagnostics;
- workspace dataflow analysis.

A response envelope is allowed for JSON transport, but it must be a mechanical projection only. It may reference block IDs and invocation IDs; it must not recalculate producers, infer utility semantics or rediscover parameters.

### 8.2 Avoid moving the current UI Pydantic hierarchy into core

Do not solve contract synchronization by moving `WorkflowDocument`, `StepNode`, `CsvArtifact`, `UtilityDescriptor`, etc. wholesale into `vg2c`. That would merely relocate the duplicate model.

Instead:

- keep compiler/stage dataclasses authoritative;
- make new emitter/utility metadata dataclasses JSON-safe where practical;
- use a thin serializer for stage fields that contain Python-only runtime objects such as `reader_cls`;
- serialize stable identities such as `reader_id` instead of Python class objects.

### 8.3 Generated TypeScript contracts

Once response shape is stable:

1. expose one canonical OpenAPI schema from FastAPI;
2. add a frontend generation script using a lightweight OpenAPI-to-TypeScript generator;
3. generate `src/vg2c_ui/frontend/src/api.generated.ts` (name may vary);
4. import API types from that file;
5. add a check that regeneration produces no diff.

`types.ts` must no longer manually mirror Python response models. It may remain only for pure UI-only types such as tab status if useful, or be deleted entirely.

### 8.4 Request models may remain transport-only

Small API request models such as path requests, revision tokens and change batches are acceptable because they represent transport/application commands rather than workflow semantics.

Prefer the core `Change` dataclasses directly where FastAPI/Pydantic can support them cleanly. Otherwise keep a tiny request adapter whose only job is conversion to core changes.

Do not duplicate validation rules in the request model.

---

## 9. Frontend simplification

### 9.1 Generic operation rendering

React receives:

- emitted invocation identity;
- utility/method title/description;
- parameter definitions and values;
- editability/read-only reason;
- capabilities;
- artifact/dataflow information from core projection.

Generic parameter controls choose widgets from core-provided value type/constraints, not utility name.

A tiny presentation registry is acceptable for genuinely specialized UIs:

```text
structured-sql -> SqlOperationEditor
otherwise      -> GenericParameterEditor
```

This registry maps semantic capabilities to React components. It must not decide what an operation means.

### 9.2 SQL UI after migration

Retain SQL React components that render controls:

- selected attributes tab;
- joins tab;
- filters tab;
- pickers/fields/operator controls;
- SQL editor CSS.

They receive a core-produced SQL structure and send semantic action intents such as:

```text
add-selection
remove-selection
reorder-selection
set-selection-expression
add-filter
update-filter
remove-filter
add-join
update-join
remove-join
```

They no longer parse or rewrite SQL themselves.

### 9.3 Labels and descriptions

Operation title/description should come from utility/classification metadata.

Remove hardcoded `KIND_LABELS` and parameter-name guessing from generic label code. React may still perform purely visual shortening, basename extraction and capitalization.

### 9.4 Headers/schema

Do not regex-match parameter names such as `header`, `columns`, `fieldnames`, `fields` in React.

If an operation declares an output schema/header, the corresponding utility parameter role tells core that. API returns declared schema information directly.

CSV file inspection remains an application feature: `csv_preview.py` may read actual files and return detected columns when no declared schema exists.

---

## 10. Workspace state refactor

The semantic refactor does not by itself fix current tab races.

### 10.1 Replace active-tab mutation helpers with ID-addressed state

Current async flows capture one tab before awaiting but call `updateActive()` after completion, so switching tabs can apply status/results to a different tab.

Replace the array + `updateActive()` pattern with a reducer/custom hook keyed by document ID:

```text
WorkspaceState
├─ activeId
├─ tabOrder[]
└─ tabs[id]
   ├─ compilation/inspection response
   ├─ draft changes
   ├─ undo/redo history
   ├─ selection
   ├─ expanded scopes
   ├─ preview
   ├─ CSV preview
   └─ request status
```

No Redux dependency is needed.

### 10.2 Every async action carries the originating document ID

Examples:

```text
previewStarted(documentId, requestId)
previewCompleted(documentId, requestId, result)
applyCompleted(documentId, requestId, result)
csvLoaded(documentId, requestId, csv)
reloadCompleted(documentId, requestId, compilation)
```

Ignore responses whose request ID is no longer current for that tab.

### 10.3 Project all dirty tabs

When requesting workspace dataflow, send pending changes for **every** open document, not only the active tab.

This fixes the current inconsistency where cross-file dependencies can depend on which tab is active.

### 10.4 Keep transient state in React

The reducer owns UI/application state only. It must not implement validation, dependency rules or SQL transformations.

---

## 11. Modules to delete, shrink, move or retain

### 11.1 Core modules

| Module | Action | Notes |
| --- | --- | --- |
| `src/vg2c/compilation.py` | **Modify** | Aggregate `EmittedScript`, utility definitions; remove AST `function_to_block` reconstruction |
| `src/vg2c/frontend/models.py` | **Mostly retain** | Existing parsed/classified source models remain authoritative |
| `src/vg2c/resolver/models.py` | **Retain** | Existing semantic/scoping model |
| `src/vg2c/dataflow/models.py` | **Retain/extend minimally** | Reuse `AnalyzedProgram`; add only cross-document result types if needed |
| `src/vg2c/dataflow/analyzer.py` | **Extend** | Add projection/re-analysis helpers rather than a new workflow analyzer |
| `src/vg2c/dispatch/models.py` | **Extend minimally** | Add stable `reader_id`; retain runtime reader class internally |
| `src/vg2c/emitter/models.py` | **Extend** | Add `CodeExpr`, generated spans, emitted invocation/argument/step records |
| `src/vg2c/emitter/__init__.py` | **Modify** | Assemble source and tracked metadata in one pass; remove post-assembly mutation |
| `src/vg2c/emitter/walker.py` | **Modify** | Preserve block-to-emitted-step ownership explicitly |
| `src/vg2c/emitter/indent_writer.py` | **Modify** | Track offsets/lines and return written spans |
| `src/vg2c/utilities/_base.py` | **Modify** | Carry operation metadata through utility registration/emission |
| `src/vg2c/utilities/*` | **Targeted modify** | Replace raw-string-as-code calls with `CodeExpr`; attach only needed semantic roles |
| `src/vg2c/editing.py` | **New, small** | Core change/project/validate/preview/apply functions; no engine class |
| `src/vg2c/sql/` or equivalent | **New, justified** | Python port of structured SQL domain parser/transform; this moves existing domain logic rather than adding a parallel system |

### 11.2 UI backend modules

| Module | Action | Notes |
| --- | --- | --- |
| `src/vg2c_ui/domain/models.py` | **Delete** | Do not retain parallel `WorkflowDocument` semantic hierarchy |
| `src/vg2c_ui/domain/__init__.py` | **Delete** | Remove directory if no longer needed |
| `src/vg2c_ui/services/python_document.py` | **Delete** | Replaced by emitter-owned metadata; AST may remain only inside core final syntax validation |
| `src/vg2c_ui/services/utility_catalog.py` | **Delete** | Replaced by `UtilitySpec`-derived core functions |
| `src/vg2c_ui/services/workflow_builder.py` | **Delete** | No second semantic projection |
| `src/vg2c_ui/services/compiler_adapter.py` | **Delete** if it remains only a wrapper | Call `vg2c.compile_document()` directly |
| `src/vg2c_ui/services/command_service.py` | **Delete after migration** | Core owns mutation/validation; persistence duties fold into `DocumentStore` |
| `src/vg2c_ui/services/document_store.py` | **Reduce/retain** | Paths, open/compile orchestration, revision/hash conflict checks, persistence |
| `src/vg2c_ui/services/sidecar.py` | **Retain/reduce** | Persist revision and user change state only; no semantic reconstruction |
| `src/vg2c_ui/services/atomic_io.py` | **Retain unchanged** | Persistence utility |
| `src/vg2c_ui/services/csv_preview.py` | **Retain** | Real file inspection is UI/application service, not translation semantics |
| `src/vg2c_ui/api/*.py` | **Reduce/retain** | Thin request conversion and response serialization only |

### 11.3 React modules

| Module | Action | Notes |
| --- | --- | --- |
| `src/.../dependencyValidation.ts` | **Delete** | Core dataflow/workspace projection replaces it |
| `src/.../dataFlow.ts` | **Delete or reduce to display selectors** | No producer/consumer/header inference remains |
| `src/.../sql/operation.ts` | **Delete** | No SQL kind/parameter-name inference in generic UI |
| `src/.../sql/parser.ts` | **Delete** | Port semantic parser to core |
| `src/.../sql/transform.ts` | **Delete** | Core handles SQL mutation actions |
| `src/.../sql/model.ts` | **Delete** | Use generated API contract for core SQL structure |
| `src/.../sql/metadata.ts` | **Delete** | Capability/metadata contract belongs to core/API |
| `src/.../sql/useSqlMetadata.ts` | **Delete or replace with generic API query hook** | No frontend-owned provider abstraction |
| `src/.../sql/presentation.ts` | **Audit and retain only presentation functions** | Move any parsing/semantic interpretation to core |
| `src/.../sql/components/*` | **Retain/refactor** | Presentation and action dispatch only |
| `src/.../operationLabels.ts` | **Substantially reduce** | Keep visual formatting only; remove kind/parameter semantics |
| `src/.../OperationEditor.tsx` | **Refactor** | Capability-to-component presentation registry + generic parameter renderer |
| `src/.../types.ts` | **Delete/replace** | Generated API types; optional separate file for UI-only state types |
| `src/.../editState.ts` | **Retain/refactor** | Generic undo/redo intent history is UI state |
| `src/.../App.tsx` | **Substantially reduce** | Move tab/request transitions to workspace reducer/custom hook |
| built `src/vg2c_ui/static/*` | **Regenerate** | Never hand-edit generated assets |

---

## 12. Staged migration sequence

Every stage must end with tests passing and immediately remove the implementation it replaces. Do not leave old/new semantic engines side by side across later stages.

### Stage 0 — Characterize current behavior and add architectural guardrails

**Work**

- Record current `main` fixture outputs for representative SQL, SQLite, CSV, file, mail, loop/macro and unsupported blocks.
- Add emitter tests for current generated Python before changing renderer semantics.
- Add API tests covering open/translate/preview/apply behavior.
- Add frontend test tooling (Vitest is sufficient initially) because the frontend currently only has build/typecheck scripts.
- Add focused tests for current SQL parse/transform behavior before porting it.
- Add a source-level architecture test/lint check that generic frontend modules must not import semantic SQL modules once the migration reaches that point.

**Immediate cleanup**

- None. This is the safety baseline.

**Acceptance**

- Existing Python tests pass.
- `npm run typecheck` and `npm run build` pass.
- New characterization tests cover the behaviors being migrated.

### Stage 1 — Make utility metadata authoritative in core

**Work**

- Add lightweight `operation_metadata` support beside `@emittable`.
- Add `UtilityParameterDefinition` and `UtilityOperationDefinition` as utility-definition facts, not workflow models.
- Implement `list_utility_operations()` / `utility_operation()` directly over `UtilitySpec.registered()`.
- Derive types/defaults/required/`Literal` choices/docs from signatures.
- Annotate existing key operations only where non-inferable roles are required.
- Update the existing UI backend path temporarily to consume these core definitions.

**Immediate cleanup**

- Delete `src/vg2c_ui/services/utility_catalog.py`.
- Move/update its tests under core utility/emitter tests.
- Remove duplicate utility descriptor-building code from `workflow_builder.py` while that module still exists temporarily.

**Acceptance**

- Adding a normal annotated parameter to an `@emittable` method appears in the core utility definition without UI-specific code.
- `Literal` choices/defaults/docs match current UI behavior.
- No utility metadata registry exists outside `UtilitySpec`/method metadata.

### Stage 2 — Add emitter-owned invocation metadata and stable IDs

**Work**

- Introduce `CodeExpr` and migrate all raw-expression render call sites.
- Extend tracked `@emittable` rendering.
- Extend `IndentWriter` to calculate final spans.
- Extend `EmittedScript` with emitted steps/invocations/arguments.
- Generate stable parameter IDs from block + utility method + semantic argument key.
- Move SQL-filter preamble generation before final source assembly.
- Change `CompilationResult` to carry the complete `EmittedScript`.
- Replace `function_to_block` AST matching with `EmittedStep.block_index`.

**Immediate cleanup**

- Delete the AST/regex `function_to_block` reconstruction in `compilation.py`.
- Update backend code to read emitted invocation metadata instead of discovering calls.
- Delete `src/vg2c_ui/services/python_document.py` as soon as all current call sites use emitter metadata.
- Delete `tests/ui/test_python_document.py`; replace with emitter manifest tests.

**Acceptance**

- Generated Python remains byte-for-byte equivalent except for deliberately normalized rendering changes approved by fixture tests.
- Every editable argument has utility/method ownership, stable ID, actual value and exact final source span.
- Inserting an unrelated generated call does not change another argument's ID.
- No production code reparses generated Python to discover editable semantics.

### Stage 3 — Move parameter mutation and validation into `vg2c`

**Work**

- Add `vg2c/editing.py` with `SetParameter`, projection/validate/preview/apply functions.
- Validate values against the core utility definition.
- Patch generated source using emitter-owned spans.
- Recalculate subsequent spans deterministically after replacements.
- Final candidate validation may use `ast.parse`/`compile` for syntax only.
- Return diffs/candidate source/diagnostics from core.
- Make `DocumentStore` perform revision checks and atomic persistence around core results.

**Immediate cleanup**

- Remove type/choice serialization/validation logic from `vg2c_ui/services/command_service.py`.
- Fold the remaining persistence coordination into `document_store.py`.
- Delete `command_service.py` once no route imports it.
- Remove obsolete API aliases that existed only to expose the same preview operation under multiple names.

**Acceptance**

- preview and apply use the same core projection path.
- UI/backend never serialize Python literals themselves.
- stale revision/hash writes still return conflicts before persistence.
- invalid choices/types are rejected by core regardless of caller.

### Stage 4 — Remove the backend `WorkflowDocument` semantic layer

**Work**

- Change document/open/translation endpoints to return a thin compilation inspection payload directly serialized from stage outputs and emitter metadata.
- Join stage data by block index only for transport/presentation convenience; do not infer new semantics.
- Keep source/output hashes and revision in the UI application envelope because they are persistence concerns.
- Update sidecar references to stable emitted parameter IDs.

**Immediate cleanup**

- Delete `src/vg2c_ui/services/workflow_builder.py`.
- Delete `tests/ui/test_workflow_builder.py` after equivalent compilation/API tests exist.
- Delete semantic classes from `src/vg2c_ui/domain/models.py`; remove the entire domain package if only transport request types remain.
- Move any remaining small request models next to API routes or use core change types.
- Delete `compiler_adapter.py` if it only delegates to `compile_document()`.

**Acceptance**

- There is no `WorkflowDocument`, `StepNode`, `CsvArtifact` or `UtilityDescriptor` semantic copy in the Python UI package.
- An endpoint inspection can be traced directly to compiler stage fields and emitted metadata.
- No UI backend function computes producers/consumers or parameter semantics.

### Stage 5 — Move draft and workspace dependency projection into core dataflow

**Work**

- Add projected-path support using emitted argument artifact roles.
- Reuse `AnalyzedProgram` for per-document projected dataflow.
- Add minimal cross-document analysis result types/functions under `vg2c.dataflow`.
- Add API endpoint/request shape that accepts all open document change sets and returns projected dataflow + diagnostics.
- Debounce projection requests in React if necessary; do not approximate semantics locally.

**Immediate cleanup**

- Delete `frontend/src/dependencyValidation.ts`.
- Delete semantic portions of `frontend/src/dataFlow.ts`; if only display helpers remain, move them into a presentation helper and delete the original module.
- Remove imports of `effectiveStepFiles` from generic components.
- Remove header-name regex semantics from React.

**Acceptance**

- output rename on one dirty tab updates producer/consumer relationships for every other dirty/open tab through core projection.
- duplicate outputs, broken dependencies and missing producer behavior match current behavior or documented intentional fixes.
- switching active tabs cannot change dependency truth.
- repository search finds no frontend implementation of producer/consumer inference.

### Stage 6 — Move structured SQL semantics into core

**Work**

- Port the existing SQL model/parser/transform behavior to Python under a focused core SQL package.
- Preserve supported SELECT, join and filter operations before expanding grammar.
- Expose structured SQL state through the `structured-sql` capability attached to the real utility operation.
- Add `SqlAction` changes handled by core editing functions.
- Return the updated SQL structure plus compilation projection after every semantic SQL action.
- Do not change SQL library/parser strategy during the ownership migration unless existing behavior cannot be reproduced reliably.

**Immediate cleanup**

- Delete `frontend/src/sql/parser.ts`.
- Delete `frontend/src/sql/transform.ts`.
- Delete `frontend/src/sql/model.ts`.
- Delete `frontend/src/sql/operation.ts`.
- Delete `frontend/src/sql/metadata.ts` and `useSqlMetadata.ts` unless a remaining file is purely a generic HTTP hook; prefer renaming/replacing rather than leaving SQL-owned provider abstractions in React.
- Audit `sql/presentation.ts`; move semantic pieces to core and retain only visual formatting.

**Acceptance**

- SQL fixture parity tests cover parse + add/update/remove/reorder for selections, joins and filters.
- React sends actions and renders returned state; it never rewrites SQL text.
- Generic frontend modules contain no `SQL_QUERY`, `SQLITE_QUERY`, `sql`, `inputs`, or `output` semantic branching.

### Stage 7 — Generate the frontend API contract and simplify operation rendering

**Work**

- Stabilize the thin API response schema.
- Generate TypeScript types from OpenAPI.
- Add a contract-generation check.
- Refactor `OperationEditor.tsx` around generic parameter metadata and a tiny capability-to-component presentation registry.
- Make labels/titles consume compiler/utility metadata.

**Immediate cleanup**

- Delete handwritten semantic interfaces from `frontend/src/types.ts`; delete the file if it has no UI-only types.
- Remove hardcoded `KIND_LABELS` and identifying-parameter heuristics from `operationLabels.ts`.
- Remove any remaining SQL-specific branch in generic operation/dataflow/tree code other than the explicit presentation registry entry.

**Acceptance**

- changing the Python API schema and failing to regenerate TS causes CI/test failure.
- a new ordinary utility parameter renders generically with no React feature code.
- a genuinely specialized capability requires one component + one registry mapping only.

### Stage 8 — Fix workspace state ownership and async races

**Work**

- Introduce `useWorkspace`/`workspaceReducer`.
- Key all state by document ID.
- Carry document ID + request ID through preview/apply/reload/CSV/projection requests.
- Store pending changes for every tab.
- Feed all dirty tab change sets to workspace projection.
- Keep undo/redo local to each tab.

**Immediate cleanup**

- Remove `updateActive()` async mutation usage from `App.tsx`.
- Remove duplicated tab-state transition helpers that the reducer supersedes.
- Reduce `App.tsx` to composition/event wiring.

**Acceptance**

- switch tabs during preview: result updates only the originating tab.
- switch tabs during CSV load: result updates only the originating tab.
- two dirty tabs both participate in dependency projection.
- closing/reopening tabs does not leak stale request results.

### Stage 9 — Final deletion audit and extensibility proof

**Work**

- Repository-wide search for old semantic model names and hardcoded parameter semantics.
- Add a small test-only utility with:
  - scalar parameter;
  - `Literal` choice;
  - artifact input/output roles;
  - optional specialized capability.
- Verify the utility catalog exposes it automatically.
- Verify an emitted instance and a future catalog entry share the same definition.
- Verify generic edit/preview/apply works without editing unrelated frontend modules.

**Immediate cleanup**

- Delete any compatibility aliases/shims created during migration that no longer have a proven external consumer.
- Remove dead exports/imports and obsolete tests.
- Regenerate static frontend assets.

**Acceptance**

A normal new utility should require changes only in its core utility definition/emission logic. A specialized UX should additionally require one explicit frontend presentation mapping. No dataflow or generic editor modules should need changes.

---

## 13. Tests and parity requirements

### 13.1 Compiler/emitter tests

Add tests for:

- `CodeExpr` vs literal string rendering;
- utility signature metadata extraction;
- `Literal` choice extraction;
- non-inferable role metadata;
- emitted step -> block mapping;
- emitted invocation utility/method identity;
- argument values and generated spans;
- span correctness with indentation/multiline SQL;
- span correctness when SQL-filter summary comments exist;
- stable IDs after unrelated generated-call insertion;
- read-only dynamic expression behavior;
- generated Python fixture parity.

### 13.2 Core editing tests

Cover:

- set string/integer/boolean/list;
- multiline SQL/string replacement;
- invalid type;
- invalid choice;
- read-only argument;
- duplicate changes to one parameter;
- syntax-invalid candidate;
- preview/apply parity;
- changed argument lengths correctly shift later spans;
- unchanged IDs after projection.

### 13.3 Dataflow tests

Reuse and extend current dataflow tests for:

- baseline producer/consumer behavior;
- projected output rename;
- projected input rename;
- multiple artifact inputs;
- output collision;
- order validity;
- branch/loop relationships;
- cross-document producer/consumer matching;
- dirty producer + dirty consumer in different documents.

### 13.4 SQL parity tests

Before deleting TypeScript SQL semantics, port fixture cases for:

- selected expressions/aliases;
- wildcard handling;
- joins and join keys;
- nested expressions used by current parser;
- filter operators/value forms;
- add/update/remove/reorder selection;
- add/update/remove join;
- add/update/remove filter;
- preservation of unaffected SQL text where current behavior depends on it;
- unsupported/ambiguous SQL returning safe read-only/diagnostic behavior.

### 13.5 API tests

Cover:

- translate/open inspection payload;
- utility definitions exposed from core;
- preview/apply;
- revision/hash conflict;
- workspace projection;
- SQL action endpoint/path;
- CSV preview;
- workspace confinement.

### 13.6 Frontend tests

Add Vitest tests for:

- workspace reducer;
- request ownership/stale responses;
- generic parameter editor selection from metadata;
- capability registry fallback;
- SQL components dispatching actions rather than parsing SQL;
- two-tab dirty projection request construction.

Build/typecheck remain mandatory.

### 13.7 Architecture checks

Add simple source-level tests that fail if:

- `vg2c_ui` imports a deleted semantic builder/catalog/parser;
- generic React code imports from `sql/parser`, `sql/transform`, or `sql/operation`;
- frontend code defines `SQL_QUERY`/`SQLITE_QUERY` semantic branches outside the specialized presentation mapping;
- `dependencyValidation.ts` or equivalent reappears;
- handwritten API semantic types replace generated ones.

---

## 14. Final acceptance criteria

The refactor is complete only when all of the following are true:

1. `CompilationResult` plus its referenced compiler-stage outputs is the authoritative semantic state for a compiled file.
2. No `vg2c.workflow` package or equivalent duplicate workflow domain hierarchy exists.
3. `EmittedScript` records editable emitted invocations/arguments and exact generated-source spans.
4. No production code reparses generated Python to discover parameter ownership, type, value, ID or source span.
5. `function_to_block` is no longer reconstructed from generated function-name regex/AST scanning.
6. Utility definitions come from `UtilitySpec`, `@emittable`, signatures and co-located non-inferable metadata.
7. There is no separately maintained utility catalog dictionary/class in the UI.
8. Parameter IDs do not depend on global generated call order.
9. Core owns value validation, source mutation and SQL transformations.
10. Core dataflow owns baseline, draft and cross-document dependency semantics.
11. React contains no producer/consumer inference, SQL parser/transformer, artifact-role inference or parameter-name semantic guessing.
12. The Python UI package contains no `WorkflowDocument`-style semantic copy of compiler state.
13. TypeScript API contracts are generated rather than manually mirrored.
14. Multi-tab async results are applied by originating document/request ID.
15. Dirty edits in inactive tabs participate in workspace dependency projection.
16. Existing translate -> inspect -> edit -> preview -> apply behavior remains functional.
17. Existing SQL structured-edit behavior has parity tests before the TypeScript implementation is deleted.
18. A newly registered ordinary utility can expose parameters in the UI without changes to generic React semantic code.
19. A future add-utility flow can reuse the exact same `UtilityOperationDefinition` used by existing emitted invocations.
20. No compatibility shim or duplicate semantic implementation remains without a documented external consumer.

---

## 15. Deferred work

The following should remain explicitly out of scope until the ownership migration is complete:

### 15.1 Add/remove/reorder utility execution

The architecture must support these later through the same utility definitions and change API, but do not implement them during the initial refactor unless required for a migration test.

When added, the core must own insertion position, generated code, dataflow implications and validation.

### 15.2 External SQL/database metadata provider

Do not create a provider framework before a real backend source exists.

When the first provider is implemented, a small `Protocol` is justified for database/schema metadata because multiple providers are plausible. Until then, expose the `structured-sql` capability and keep optional external choices unavailable.

### 15.3 New SQL parser library

Do not combine ownership migration with a SQL parser replacement. First port current behavior and tests. Evaluate a third-party parser only afterward based on demonstrated grammar/maintenance needs.

### 15.4 Durable identity across arbitrary VG2 source rewrites

The new parameter identity removes generated-call-order instability. It does not attempt to preserve step identity across arbitrary insertion/reordering of VG2 source blocks.

If future source-level editing requires that, design it from actual requirements rather than adding UUID infrastructure now.

### 15.5 General plugin architecture

Do not add plugin managers, strategy registries, handler factories or generic extension frameworks. `UtilitySpec` is already the utility registry. A small React capability registry is presentation-only and sufficient.

---

## Final architecture review

Before implementation of each stage, apply these questions to every proposed addition:

- **Does this duplicate information already present in a compiler stage?** If yes, reuse/enrich that stage.
- **Could this model be removed?** Prefer extending `EmittedScript`, `AnalyzedProgram`, `DispatchedBlock` or `CompilationResult` over introducing a parallel model.
- **Could this service be a function?** Prefer functions; do not add a service/ABC unless multiple implementations or stateful lifetime is real.
- **Is this abstraction solving an actual dependency problem?** If not, do not add it.
- **Does the UI still know domain semantics it should not know?** If yes, move that decision into the responsible core stage/domain function.
- **Are we keeping old code simply because it already exists?** If its responsibility moved, delete it in the same stage.
- **Can a future utility be exposed without modifying unrelated UI modules?** Ordinary utilities should be automatic from utility metadata; specialized UX should require only an explicit capability renderer.

The intended end state is not a workflow framework. It is the existing compiler pipeline made sufficiently self-describing and editable that every consumer can rely on the semantic information the compiler already owns.
