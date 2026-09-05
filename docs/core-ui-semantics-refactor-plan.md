# Core/UI Semantic Ownership Refactor — Implementation Plan

Status: proposed implementation plan

Baseline reviewed: `main` at `7192a63a7316b5a53aed17ce2eff7e388eadc20f` (2026-09-03)

## 1. Purpose

Refactor SQLPathFinder so that `vg2c` is the authoritative owner of translation and workflow semantics, while `vg2c_ui` becomes a thin adapter/presentation layer.

The target rule is simple:

> If information affects what a translated operation means, what it consumes or produces, whether a change is valid, or how the generated workflow must be changed, that information belongs in `vg2c`.

The browser may hold temporary values for responsive controls, but it must not contain a second implementation of dataflow, utility semantics, SQL parsing/transformation, artifact ownership, parameter validation, or operation capability rules.

This plan deliberately avoids a plugin framework. The target is one concrete core workflow service, immutable semantic models, the existing utility registry as the catalog source, and a small amount of explicit metadata only where semantics cannot be inferred from utility signatures.

## 2. Goals

1. Make `vg2c` the single source of truth for:
   - compilation/translation semantics;
   - workflow steps and scopes;
   - utility definitions;
   - editable parameters and constraints;
   - artifact input/output bindings;
   - dependency/dataflow projection;
   - semantic capabilities;
   - supported mutations;
   - validation;
   - generated-workflow mutation;
   - structured SQL interpretation and transformation.
2. Give all consumers one stable public workflow API rather than requiring knowledge of compiler internals.
3. Use the same utility metadata for existing-step editing and the future add-utility catalog.
4. Support draft/unsaved edits by projecting them through the core instead of reproducing semantic rules in TypeScript.
5. Remove generic frontend imports of SQL-specific modules.
6. Replace handwritten Python/TypeScript contract duplication with generated TypeScript types.
7. Fix multi-tab ownership and async race problems with a small reducer/custom hook.
8. Reduce total semantic code in `vg2c_ui`, deleting obsolete modules after each migration stage.
9. Keep every stage runnable and testable; do not maintain old/new semantic engines in parallel after a stage lands.

## 3. Non-goals

This refactor should not simultaneously:

- redesign the visual appearance of the editor;
- introduce Redux or another global state framework;
- invent a general plugin system;
- implement arbitrary user-written utility plugins;
- replace the SQL parser with a new third-party SQL library merely for style;
- change working compiler translation behavior unless required to expose stable semantic information;
- implement the full future add/remove/reorder-utility UX before the core mutation support exists;
- preserve obsolete API aliases solely for compatibility with the current frontend.

The add-utility feature is a design constraint for the catalog and editing model, not a requirement to ship the entire feature in the first refactor.

## 4. Current-state architecture

### 4.1 Core compiler

`src/vg2c/compilation.py` currently performs:

```text
parse -> classify -> resolve -> analyze -> dispatch -> emit
```

and returns `CompilationResult` containing:

- generated Python;
- `ResolvedProgram`;
- `AnalyzedProgram`;
- `DispatchedProgram`;
- compiler diagnostics;
- a `function_to_block` mapping reconstructed by parsing emitted Python and matching `step_<index>_*` function names.

The compiler already owns authoritative dataflow through `vg2c.dataflow`, including producers, consumers and `DataflowEdge` ordering information.

### 4.2 Current UI backend semantic layer

`src/vg2c_ui` currently contains a second semantic layer:

- `domain/models.py` — Pydantic workflow/API models;
- `services/utility_catalog.py` — derives parameter metadata from `UtilitySpec` and `@emittable` methods;
- `services/python_document.py` — reparses generated Python, discovers calls/parameters, determines editable literal types and source offsets;
- `services/workflow_builder.py` — projects compiler internals plus generated-Python AST information into `WorkflowDocument`;
- `services/command_service.py` — validates parameter mutations, serializes values, patches generated Python and validates the candidate;
- `services/document_store.py` — workspace-safe file access/orchestration;
- API route modules — transport only, mostly thin.

Much of this logic is not inherently UI-specific. It describes the generated workflow and how it may safely change, so it belongs in core `vg2c`.

### 4.3 Current React semantic layer

The frontend additionally owns semantic rules:

- `sql/operation.ts` recognizes `SQL_QUERY`/`SQLITE_QUERY`, finds `sql`, `inputs`, and `output` parameters by name, and recalculates effective artifacts;
- `dependencyValidation.ts` rebuilds producer/consumer maps, duplicate-output checks, missing inputs and effective artifacts;
- `dataFlow.ts` reconstructs cross-document dependencies and guesses header parameters by regex;
- `operationLabels.ts` maps functional kinds and guesses identifying parameter names;
- `sql/parser.ts` (~26 KB) parses SQL into an editable structure;
- `sql/transform.ts` (~15 KB) performs SQL structural mutations;
- `sql/model.ts` owns the structured SQL domain model;
- `sql/metadata.ts` defines a metadata capability/provider contract that is not connected to the backend;
- `OperationEditor.tsx` directly branches on SQL operation kinds.

This creates three sources of semantic truth: compiler, UI backend, and browser.

## 5. Target architecture

```text
VG2 source / generated workflow
            |
            v
+---------------------------------------+
|                 vg2c                  |
|                                       |
| compiler pipeline                     |
| utility registry + operation metadata |
| workflow semantic models              |
| generated-document editor             |
| dataflow/workspace projection          |
| structured SQL semantics              |
| validation                            |
|                                       |
|        WorkflowEngine (public)         |
+-------------------+-------------------+
                    |
              stable models
                    |
+-------------------v-------------------+
|              vg2c_ui backend           |
| path/workspace security                |
| file persistence / atomic writes       |
| revision/conflict handling             |
| HTTP serialization/routes              |
| external-provider wiring               |
+-------------------+-------------------+
                    |
          generated OpenAPI types
                    |
+-------------------v-------------------+
|               React UI                 |
| rendering / controls                   |
| selection/navigation                   |
| per-tab draft intent                   |
| request/loading/error state            |
| presentation-only component registry   |
+---------------------------------------+
```

### Dependency rule

Allowed:

```text
vg2c_ui -> vg2c
frontend -> HTTP contract produced from vg2c-backed API
```

Forbidden:

```text
vg2c -> vg2c_ui
core-generic frontend -> frontend/sql semantic modules
frontend semantic implementation parallel to vg2c
```

## 6. Core public interface

### 6.1 Use a concrete service, not an ABC/Protocol hierarchy

Introduce one concrete `WorkflowEngine` in `src/vg2c/workflow/engine.py`.

There is currently one authoritative compiler/editor implementation. An ABC or protocol for the entire workflow engine would add ceremony without a second implementation.

Proposed public surface:

```python
class WorkflowEngine:
    def inspect(
        self,
        source_path: Path,
        generated_source: str | None = None,
    ) -> WorkflowSnapshot: ...

    def catalog(self) -> tuple[UtilityOperationDefinition, ...]: ...

    def project(
        self,
        source_path: Path,
        generated_source: str,
        changes: ChangeSet,
    ) -> WorkflowProjection: ...

    def project_workspace(
        self,
        documents: tuple[WorkflowProjectionInput, ...],
    ) -> WorkspaceProjection: ...

    def preview(
        self,
        source_path: Path,
        generated_source: str,
        changes: ChangeSet,
    ) -> EditPreview: ...
```

The engine does not write files. It returns candidate generated source plus semantic projection/validation. Persistence stays outside core.

`compile_document()` may remain available as the lower-level compiler API. `WorkflowEngine` is the consumer-facing semantic API that composes the existing compiler stages with workflow/editing semantics.

### 6.2 Use a Protocol only for external metadata providers

A protocol is justified for metadata that can genuinely have multiple providers:

```python
class CapabilityMetadataProvider(Protocol):
    def capabilities(self, context: CapabilityContext) -> CapabilityAvailability: ...
    def options(self, request: CapabilityOptionsRequest) -> CapabilityOptions: ...
```

SQL/database implementations can implement this protocol. A null provider may return no optional metadata. React should never implement this interface.

Do not create a protocol for every utility or every editor.

## 7. Core domain model

### 7.1 Move the authoritative workflow model into `vg2c`

Create `src/vg2c/workflow/models.py` and move/refine the semantic portion of `vg2c_ui/domain/models.py` into it.

Recommended decision: make these models frozen Pydantic models and move `pydantic` from the UI-only optional dependency into the base package dependency.

Reason:

- one model can be the core public model;
- the same model can be used directly by FastAPI;
- OpenAPI can be generated from the same source;
- TypeScript can then be generated from OpenAPI;
- this avoids core dataclass -> API DTO -> handwritten TypeScript model duplication.

The core must not depend on FastAPI; Pydantic is used only as a serialization/validation model library.

Use `ConfigDict(frozen=True)` for core snapshots/definitions. Mutable application state remains in the UI.

### 7.2 Proposed model responsibilities

#### `ParameterDefinition`

Definition-level metadata that is identical for catalog and existing operations:

- `key` / semantic parameter name;
- value kind (`string`, `integer`, `boolean`, `list`, etc.);
- required/default;
- constraints/choices;
- description;
- semantic roles (if any);
- optional presentation hint such as `multiline` where it cannot be inferred safely.

Do not encode React component names.

#### `ParameterState`

Instance-level information:

- stable semantic parameter ID;
- `definition` or definition key;
- current value;
- source representation;
- editable/read-only state;
- read-only reason;
- generated-source span used by the core editor.

The UI should not receive raw patch offsets if it does not need them. Keep offsets internal where possible.

#### `UtilityOperationDefinition`

Represents one usable utility method, e.g. `sqlite.run_query`, not merely one utility class:

- stable ID: `<utility_name>.<method>`;
- utility/class/module identity;
- title/description;
- parameter definitions derived from the method signature;
- capabilities;
- artifact role declarations;
- supported mutations;
- return type;
- whether it is safe/available for catalog insertion.

This same model is returned by the utility catalog and attached/referenced by an existing workflow step.

#### `WorkflowStep`

- semantic step ID;
- compiler block index/source span;
- functional kind;
- operation definition ID;
- effective parameter states;
- effective artifact bindings;
- semantic title/summary;
- scope relationship;
- validation/read-only status;
- structured capabilities.

Remove UI-only fields such as icon selection from the core. The UI may map capability/kind to icons.

#### `ArtifactBinding`

Represents how an operation parameter participates in dataflow:

- direction: input/output;
- artifact kind (`csv` initially);
- parameter key/ID when the path is parameter-driven;
- cardinality: one/many;
- effective paths.

This is the replacement for frontend assumptions that `inputs` and `output` have special names only for SQL.

#### `WorkflowArtifact`

Generalize `CsvArtifact` to `WorkflowArtifact` while retaining `kind="csv"` initially:

- normalized artifact identity/path;
- producers;
- consumers;
- conditional/loop information;
- ordering validity;
- optional declared schema/header metadata when the compiler/utility knows it.

#### `WorkflowSnapshot`

The authoritative semantic representation returned to consumers:

- document identity/source information;
- steps/scopes;
- artifacts;
- diagnostics;
- source/output hash data required by the persistence layer;
- semantic schema version.

#### `WorkflowProjection`

A snapshot generated from unsaved change intent:

- effective `WorkflowSnapshot`;
- validation issues;
- changed semantic values;
- no persisted mutation.

#### `WorkspaceProjection`

For multiple open documents:

- projected document snapshots;
- cross-document artifact relationships;
- workspace-level diagnostics such as duplicate outputs/broken dependencies.

This moves cross-tab dependency semantics out of TypeScript.

## 8. Utility definition/catalog design

### 8.1 Move `UtilityCatalog` into core

Move/refactor:

```text
src/vg2c_ui/services/utility_catalog.py
    -> src/vg2c/workflow/catalog.py
```

The catalog must enumerate definitions, not only resolve an already-emitted call.

`UtilitySpec.registered()` remains the registry source. Do not create a second registry.

The catalog should scan registered utility classes for `@emittable` methods and derive by inspection:

- method name;
- signature;
- parameter names;
- Python annotations;
- required/default;
- `Literal` choices;
- method/class documentation;
- return annotation.

### 8.2 Add only non-inferable semantics explicitly

Add a small decorator/helper in core, for example `operation_spec(...)`, which attaches metadata directly to the utility method before `@emittable` wraps it:

```python
@emittable
@operation_spec(
    capabilities=("structured-sql",),
    parameter_roles={
        "sql": QueryText(),
        "inputs": ArtifactInput(kind="csv", many=True),
        "output": ArtifactOutput(kind="csv"),
    },
)
def run_query(...):
    ...
```

Exact names may change during implementation; the important rule is that this metadata is co-located with the actual utility method and contains only semantics that cannot be derived from the Python signature.

Do not create a separate manually maintained catalog dictionary.

Methods that need no special semantics should require no new metadata.

### 8.3 Supported mutations

Do not advertise operations that core cannot perform.

Initially most catalog definitions expose:

```text
supported_mutations = [set-parameter]
```

Read-only operations expose none.

When insert/remove support is implemented later, the same catalog model gains those supported mutations. The UI add menu filters on the capability rather than containing a hardcoded allow-list.

## 9. Stable semantic identity and generated-document editing

### 9.1 Move generated Python interpretation into core

Move/refactor:

```text
src/vg2c_ui/services/python_document.py
    -> src/vg2c/workflow/generated_document.py
```

Generated Python is an output of the compiler and its editable structure is a core concern.

### 9.2 Improve IDs during the move

Current parameter identity uses:

```text
<function_name>:<global-call-index>:<position/keyword>
```

This means adding an unrelated earlier call can change later parameter IDs.

In this refactor, change the ID basis to semantic call identity:

```text
<step-id>:<utility>.<method>:<same-target-occurrence>:<parameter-key>
```

This is still source-derived, but it is stable against insertion/removal of unrelated call targets inside the same generated step.

Add explicit tests that inserting an unrelated generated call does not renumber IDs for the operation being edited.

### 9.3 Defer a full emitter source-map manifest

The ideal long-term solution is an emitter-produced edit manifest containing stable call IDs and source spans. The current string-based emitter API makes a full manifest more invasive because utility emitters return strings.

Do not redesign the complete emitter in the first refactor unless implementation shows that semantic IDs cannot be made safe enough by core AST matching.

Document a later source-map improvement as deferred work. If emitter work becomes cheap while implementing the move, prefer it, but do not block the ownership refactor on it.

### 9.4 Eliminate `CompilationResult.function_to_block` reconstruction if practical

`DispatchedBlock` already carries `step_name`. During the core migration, verify whether workflow step/function mapping can be derived directly from dispatch/emission information instead of reparsing emitted function names with `_STEP_NAME_RE` in `compilation.py`.

If yes, remove the AST/regex reconstruction from `CompilationResult`. If not, keep it temporarily inside core and cover it with tests; do not reproduce it elsewhere.

## 10. Editing architecture

### 10.1 Core change model

Move command semantics into core. Start with the mutations that actually exist:

```text
SetParameter
CapabilityAction    # required when SQL structural actions move to core
```

Wrap them in `ChangeSet`.

Do not define speculative `AddUtility`, `RemoveUtility`, or `ReorderUtility` commands until the core renderer can execute them safely.

The catalog may expose future readiness, but unsupported commands must not exist as no-op placeholders.

### 10.2 Standard flow

All consumers use:

```text
inspect
   -> user creates change intent
project
   -> core returns effective semantic snapshot + validation
preview
   -> core returns validated candidate + diff/semantic issues
apply
   -> persistence adapter writes candidate atomically
inspect again
```

`project` and `preview` are side-effect free.

### 10.3 Mutation ownership

Core owns:

- finding the semantic parameter;
- checking operation/parameter editability;
- type validation;
- choice/range constraints;
- capability-action validation;
- serialization into generated Python;
- source-span replacement;
- candidate Python syntax validation;
- generated-document structural validation;
- recalculating effective artifacts/dependencies;
- structured SQL transformations.

UI backend owns:

- resolving paths within the configured workspace;
- checking revision/hash conflicts against disk;
- atomic file writes;
- sidecar persistence if sidecars remain necessary;
- HTTP error mapping.

React owns only the user's draft intent and display state.

### 10.4 Refactor `CommandService`

Move its semantic portions into `vg2c/workflow/editor.py`.

After the core editor is used by the API, delete `src/vg2c_ui/services/command_service.py` rather than leaving it as a wrapper whose only job is forwarding calls.

Fold the remaining conflict/persistence orchestration into `DocumentStore` (or rename it `WorkspaceStore` if its responsibility naturally expands to multiple documents). Do not add another service layer unless needed.

## 11. Dataflow and artifact ownership

### 11.1 Baseline semantics

Continue using `vg2c.dataflow` as the source for compiler-derived producers/consumers and scope/order information.

Move the projection currently performed in `vg2c_ui/services/workflow_builder.py` into core workflow construction.

### 11.2 Draft semantics

Draft edits can change artifact paths without recompiling the original VG2. Core handles this by applying `ArtifactBinding` definitions to projected parameter values, then running a core workspace artifact analyzer.

The browser sends change intent; it does not calculate new inputs/outputs.

### 11.3 Cross-document projection

Introduce a side-effect-free core function used by `WorkflowEngine.project_workspace()` that accepts projected workflow snapshots and computes:

- producers and consumers by normalized artifact identity;
- upstream/downstream document relationships;
- duplicate outputs;
- missing/broken dependencies;
- order validity where meaningful;
- effective artifacts after unsaved edits.

Normalization (`./`, slash direction, case policy) must be defined once in core.

### 11.4 Remove browser semantic implementations

After the workspace projection endpoint is consumed, delete:

```text
src/vg2c_ui/frontend/src/dependencyValidation.ts
src/vg2c_ui/frontend/src/sql/operation.ts
```

`dataFlow.ts` should either be deleted or reduced to pure presentation selectors over `WorkspaceProjection`. It must contain no producer/consumer construction, artifact normalization, parameter-name heuristics or SQL imports.

Header/schema information must be returned by core artifact/capability metadata; delete frontend regex guessing for `header|headers|columns|fieldnames|fields`.

## 12. Structured SQL ownership

### 12.1 Move SQL domain code to core

Under the requested ownership rule, the current TypeScript parser/transformer is domain logic and must move.

Port the current behavior into:

```text
src/vg2c/workflow/sql/models.py
src/vg2c/workflow/sql/parser.py
src/vg2c/workflow/sql/transform.py
src/vg2c/workflow/sql/capability.py
```

Do not introduce a new SQL parsing dependency in the same migration. First port current behavior and lock it down with parity fixtures/tests. A later internal replacement with a mature SQL parser can be evaluated independently.

### 12.2 Core capability model

A step whose utility declares `structured-sql` receives a capability payload such as:

```text
StructuredSqlCapability
- kind = "structured-sql"
- query_parameter_id
- parsed query model
- supported actions
- optional metadata availability
```

The exact SQL AST/view model may closely follow the current `sql/model.ts` so UI behavior does not need redesign.

### 12.3 SQL mutations

React should send semantic actions, for example:

```text
replace-select-expression
add-select-expression
remove-select-expression
reorder-select-expression
add-filter
update-filter
remove-filter
add-join
update-join
remove-join
```

Core transforms the SQL, updates the query parameter, reparses it, validates it and returns a new `WorkflowProjection`.

The frontend must not generate SQL text itself.

### 12.4 SQL metadata provider

Move the conceptual provider interface from TypeScript into core using `CapabilityMetadataProvider` or a SQL-specific implementation protocol beneath that generic boundary.

The backend application wires the configured provider into `WorkflowEngine`; the browser requests normalized capability options through HTTP.

Delete the frontend `SqlMetadataProvider` abstraction after the API path is live.

### 12.5 Frontend SQL files after migration

Keep presentation code:

```text
sql/components/*
sql/presentation.ts   # only if it remains purely formatting/view logic
sql/sqlEditor.css
```

Delete semantic code:

```text
sql/parser.ts
sql/transform.ts
sql/model.ts
sql/operation.ts
sql/metadata.ts
sql/useSqlMetadata.ts
```

Replace `useSqlMetadata.ts` with a small generic API/query hook if needed; it must not define SQL semantics.

## 13. Frontend editor selection

Replace the hardcoded SQL conditional in `OperationEditor.tsx` with one UI-only component registry keyed by core capability:

```text
structured-sql -> SqlOperationEditor
default        -> GenericOperationEditor
```

This registry is allowed because it maps semantic capability to visual component. It must not contain domain rules about parameter names, artifacts or validation.

Adding a new ordinary utility requires no registry change.

Adding a genuinely different visual interaction requires exactly:

1. a core capability declaration/model;
2. one frontend component;
3. one registry entry.

Generic workflow components remain unchanged.

## 14. Operation labels and presentation metadata

`operationLabels.ts` currently contains domain knowledge through a functional-kind map and identifying parameter-name heuristics.

Move authoritative operation title/summary generation into the core catalog/workflow projection where the utility definition and semantic roles are known.

The core may return:

```text
title: "SQLite Query"
summary: "output: result.csv"
```

The browser may shorten/truncate strings for layout, but it must not decide that `target_table`, `recipient`, `path`, etc. are semantically identifying parameters.

`operationLabels.ts` should therefore be removed or reduced to presentation-only truncation/path-basename helpers.

## 15. API and generated contracts

### 15.1 Core models directly back FastAPI responses

After semantic models move to `vg2c.workflow.models`, API routes import those models directly. Remove `vg2c_ui/domain/models.py` once route/request models have moved to their final homes.

UI-specific request models that only represent HTTP transport may remain in `vg2c_ui/api`, but they must not redefine workflow semantics.

### 15.2 Generate TypeScript from OpenAPI

Add `openapi-typescript` as a frontend dev dependency.

Add a deterministic script, e.g.:

```text
scripts/export_ui_openapi.py
```

that imports the FastAPI app and writes its OpenAPI JSON without starting a server.

Add frontend scripts:

```text
npm run generate:api
npm run typecheck
npm test
```

Generate into:

```text
src/vg2c_ui/frontend/src/generated/api-schema.ts
```

Delete handwritten semantic `types.ts`. A small `types.ts` may remain only if it contains UI-local view state types, not copies of backend models.

Keep `api.ts` as a small handwritten transport wrapper if useful; its request/response types must reference generated types.

### 15.3 Replace command route aliases

Consolidate the current multiple aliases around parameter preview/apply. After frontend migration, expose one canonical set of endpoints, conceptually:

```text
GET  /api/catalog
POST /api/documents/open
POST /api/workspace/project
POST /api/changes/preview
POST /api/changes/apply
POST /api/capabilities/options
```

Exact existing route prefixes may be retained when sensible, but there must be one operation for each semantic action. Delete obsolete aliases once the frontend uses the canonical route.

## 16. Workspace and React state

### 16.1 Replace `App.tsx` state orchestration with a reducer/custom hook

Do not introduce Redux.

Create something similar to:

```text
src/vg2c_ui/frontend/src/workspace/useWorkspace.ts
src/vg2c_ui/frontend/src/workspace/reducer.ts
src/vg2c_ui/frontend/src/workspace/types.ts
```

Per-tab state should contain:

```text
TabState
- id
- persisted snapshot
- draft changes
- projected snapshot
- selection
- expansion state
- preview/apply state
- request sequence/status
- UI errors
```

Workspace state contains:

```text
- tabs by ID
- tab order
- active tab ID
- latest WorkspaceProjection
- shared catalog
```

### 16.2 Async ownership rule

Every async request captures the initiating tab ID and a request token before awaiting.

Reducer actions must be explicit:

```text
projectionStarted(tabId, requestId)
projectionCompleted(tabId, requestId, projection)
previewCompleted(tabId, requestId, preview)
applyCompleted(tabId, requestId, snapshot)
```

Ignore stale responses whose request token is no longer current.

Never update “the active tab” after an await.

### 16.3 All dirty tabs participate in workspace projection

`project_workspace` receives draft changes for every open dirty tab, not only the currently active tab.

This fixes cross-tab dependencies when an output filename is changed in one tab while another tab consumes it.

### 16.4 Responsive inputs

For text-entry responsiveness, React may display the locally typed raw value immediately. Semantic indicators (dependencies, validation, structured SQL state) come from a debounced core projection.

Suggested flow:

```text
onChange
  -> reducer stores draft intent immediately
  -> 100–250 ms debounce
  -> POST workspace/project
  -> update projected snapshots/diagnostics
```

Do not reproduce semantic calculations locally to avoid the round trip.

## 17. Files/modules to modify

### Core

Modify:

```text
src/vg2c/compilation.py
src/vg2c/emitter/models.py              # only if metadata/source identity requires it
src/vg2c/utilities/_base.py             # registry integration as required
relevant utility modules                 # add non-inferable operation specs
src/vg2c/dataflow/*                      # only to expose/reuse existing semantic information cleanly
src/vg2c/__init__.py                     # export stable public workflow API
pyproject.toml
uv.lock
```

Add:

```text
src/vg2c/workflow/__init__.py
src/vg2c/workflow/models.py
src/vg2c/workflow/catalog.py
src/vg2c/workflow/metadata.py            # small operation_spec/role definitions if not placed in catalog.py
src/vg2c/workflow/generated_document.py
src/vg2c/workflow/editor.py
src/vg2c/workflow/projector.py
src/vg2c/workflow/engine.py
src/vg2c/workflow/sql/__init__.py
src/vg2c/workflow/sql/models.py
src/vg2c/workflow/sql/parser.py
src/vg2c/workflow/sql/transform.py
src/vg2c/workflow/sql/capability.py
```

Module count is a guide, not a requirement. Merge small modules during implementation if separation does not improve understanding.

### UI backend

Modify substantially:

```text
src/vg2c_ui/services/document_store.py
src/vg2c_ui/services/compiler_adapter.py
src/vg2c_ui/api/commands.py
src/vg2c_ui/api/documents.py
src/vg2c_ui/api/translation.py
src/vg2c_ui/api/__init__.py
src/vg2c_ui/app.py
```

Likely remove after responsibilities move:

```text
src/vg2c_ui/domain/models.py
src/vg2c_ui/services/utility_catalog.py
src/vg2c_ui/services/python_document.py
src/vg2c_ui/services/workflow_builder.py
src/vg2c_ui/services/command_service.py
```

Retain:

```text
src/vg2c_ui/services/atomic_io.py
src/vg2c_ui/services/document_store.py       # thinner persistence/workspace adapter
src/vg2c_ui/services/csv_preview.py          # presentation support / file preview, not workflow semantics
src/vg2c_ui/services/sidecar.py              # if sidecars remain necessary
```

### React frontend

Modify:

```text
App.tsx
OperationEditor.tsx
ScriptTree.tsx
ContextSidebar.tsx
api.ts
dataFlow.ts or replacement presentation selectors
operationLabels.ts or replacement presentation helpers
sql/components/*
sql/presentation.ts
package.json
```

Remove after migration:

```text
types.ts semantic model copies
dependencyValidation.ts
sql/operation.ts
sql/parser.ts
sql/transform.ts
sql/model.ts
sql/metadata.ts
sql/useSqlMetadata.ts
```

Add:

```text
generated/api-schema.ts
workspace/reducer.ts
workspace/useWorkspace.ts
editorRegistry.tsx
```

## 18. Implementation sequence

Each stage should be its own coherent commit or small commit series. Finish its tests and delete superseded code before starting the next stage.

### Stage 0 — Characterization tests and architectural guardrails

Purpose: create a safety net around behavior before moving ownership.

Actions:

1. Add Vitest to the frontend.
2. Add characterization fixtures for current SQL parsing/transformation.
3. Add tests for current artifact/dependency behavior, including rename cases.
4. Add backend tests for current parameter editing and utility metadata.
5. Add an import-boundary test or simple repository check asserting generic frontend modules do not import `./sql/*` after the later cut.
6. Capture representative generated Python fixtures for parameter identity/editing.

Acceptance:

- existing Python tests pass;
- frontend typecheck passes;
- new characterization tests pass against current behavior;
- no production behavior change.

### Stage 1 — Core semantic models and utility catalog

Purpose: move definitions/source-of-truth metadata into core without changing UI behavior.

Actions:

1. Add `vg2c.workflow.models`.
2. Move Pydantic to the base dependency and freeze semantic models.
3. Move `UtilityCatalog` to `vg2c.workflow.catalog`.
4. Add `operation_spec`/parameter-role metadata mechanism.
5. Add metadata only to utilities that need non-inferable semantics, starting with SQL/SQLite and artifact-producing/consuming operations used by the editor.
6. Make current UI backend import the core models/catalog directly.
7. Add `/api/catalog` returning `UtilityOperationDefinition` entries.
8. Delete `vg2c_ui/services/utility_catalog.py` and semantic definitions from `vg2c_ui/domain/models.py`; retain transport-only models temporarily only if needed within this stage.

Acceptance:

- adding an ordinary parameter with a Python default/type to an `@emittable` method automatically appears in the catalog without frontend changes;
- `Literal` values become choices from core metadata;
- existing workflow JSON remains semantically equivalent;
- existing UI still renders/edit parameters;
- no utility metadata registry exists outside core.

### Stage 2 — Core generated-workflow editor and improved identity

Purpose: move editability, parameter discovery and mutation validation out of `vg2c_ui`.

Actions:

1. Move generated-document parsing into `vg2c.workflow.generated_document`.
2. Adopt semantic call-target-based parameter IDs.
3. Move type/choice validation, serialization and source replacement into `vg2c.workflow.editor`.
4. Expose `WorkflowEngine.inspect/project/preview` for single documents.
5. Update `DocumentStore` to call the engine and perform only conflict checks/persistence.
6. Delete `vg2c_ui/services/python_document.py`.
7. Delete `CommandService` once its remaining persistence behavior is folded into `DocumentStore`.
8. Remove duplicate command aliases during the API update.

Acceptance:

- preview remains side-effect free;
- apply remains atomic;
- stale revision/hash conflicts still fail;
- invalid type/choice changes fail in core;
- generated candidate is parsed/compiled before apply;
- inserting an unrelated generated call does not alter semantic IDs of unaffected parameters;
- no generated-Python AST editing logic remains in `vg2c_ui`.

### Stage 3 — Core workflow projection and workspace dataflow

Purpose: eliminate TypeScript artifact/dependency semantics and solve dirty multi-tab projection.

Actions:

1. Move workflow construction from `vg2c_ui/services/workflow_builder.py` into core `WorkflowEngine`/projector.
2. Implement artifact bindings from utility operation metadata.
3. Implement single-document effective artifact projection.
4. Implement `project_workspace()` for multiple open documents/drafts.
5. Add `/api/workspace/project`.
6. Update frontend to send draft changes for all dirty tabs and consume returned workspace semantics.
7. Remove frontend `dependencyValidation.ts` and `sql/operation.ts`.
8. Strip `dataFlow.ts` of semantic calculation and header-name guessing.
9. Delete `vg2c_ui/services/workflow_builder.py`.

Acceptance:

- baseline artifacts match compiler dataflow;
- renaming an output through a draft changes core-projected producer/consumer relationships;
- consumer diagnostics update when the producer is dirty in another tab;
- duplicate output diagnostics are produced by core;
- `ScriptTree` and `ContextSidebar` display the same projection;
- no generic frontend module imports SQL semantics;
- no artifact normalization/dataflow algorithm exists in TypeScript.

### Stage 4 — Structured SQL semantics into core

Purpose: remove the largest remaining frontend-domain implementation.

Actions:

1. Port current `sql/model.ts` models to core.
2. Port parser behavior to Python with parity fixtures.
3. Port transformations to Python with parity fixtures.
4. Add `structured-sql` capability to relevant utility definitions.
5. Add `CapabilityAction` to the core change model.
6. Return structured SQL state in workflow projections.
7. Move external metadata provider contract to core/backend.
8. Add generic capability-options API.
9. Update `SqlOperationEditor` to render core-returned model and dispatch capability actions only.
10. Delete semantic TS SQL modules listed above.

Acceptance:

- current supported SQL statements produce equivalent structured rows before/after port;
- all existing select/join/filter mutations produce equivalent SQL or documented normalized output;
- malformed/unsupported SQL returns core diagnostics/read-only capability state rather than frontend parser failures;
- no SQL parser/transformer/domain model remains in TypeScript;
- SQL metadata availability is determined by backend/core, not a browser-side provider.

### Stage 5 — Generated API contracts and thin frontend editor registry

Purpose: remove contract drift and hardcoded operation dispatch.

Actions:

1. Add deterministic OpenAPI export script.
2. Add `openapi-typescript` generation.
3. Replace handwritten semantic `types.ts` with generated types.
4. Add `editorRegistry.tsx` mapping core capabilities to visual editors.
5. Split the generic parameter renderer into a reusable component if `OperationEditor.tsx` remains too large.
6. Replace functional-kind/parameter-name label heuristics with core title/summary fields.
7. Add a generated-contract freshness check to development/test instructions.

Acceptance:

- changing a backend/core API field and regenerating types causes TypeScript compile errors at affected consumers;
- `OperationEditor` contains no `SQL_QUERY`/`SQLITE_QUERY` branch;
- generic UI has no utility-specific parameter-name lists;
- ordinary new parameters render using generated metadata without frontend source changes.

### Stage 6 — Workspace reducer and async correctness

Purpose: make user-facing state ownership explicit without introducing a larger framework.

Actions:

1. Move tab state into `useWorkspace` + reducer.
2. Store persisted snapshot, draft changes and projected snapshot per tab.
3. Include `tabId` and `requestId` on every async lifecycle action.
4. Debounce semantic projection requests.
5. Ignore stale async responses.
6. Make all tree/sidebar/editor views derive from the same per-tab projection/workspace projection.
7. Reduce `App.tsx` to layout and high-level action wiring.

Acceptance:

- switching tabs during preview/apply/projection cannot update the wrong tab;
- unsaved edits in inactive tabs participate in cross-document dependencies;
- stale projection responses cannot overwrite newer drafts;
- `App.tsx` no longer contains domain calculations;
- reducer tests cover open/close/switch/edit/project/preview/apply/conflict flows.

### Stage 7 — Cleanup and extensibility proof

Purpose: verify the architectural result, not only functional behavior.

Actions:

1. Delete all superseded compatibility aliases and semantic modules.
2. Search for hardcoded `functional_kind` branches in generic frontend code; retain only presentation cases with explicit justification.
3. Search for special parameter-name semantics in frontend.
4. Search for duplicated producer/consumer/artifact normalization logic.
5. Add a test-only sample utility with ordinary parameters and artifact roles.
6. Demonstrate it appears in the catalog and existing-step editor without changes to generic React components.
7. Document extension steps in developer docs.

Acceptance:

A hypothetical new normal utility requires changes only to:

```text
1. utility implementation / registration in vg2c;
2. optional co-located operation_spec metadata for non-inferable roles.
```

No generic frontend file changes are required.

A hypothetical new rich visual capability requires:

```text
1. core capability model/behavior;
2. one presentation component;
3. one editor registry mapping.
```

Dataflow/tree/sidebar/API state code remains untouched.

## 19. Test strategy

### 19.1 Core catalog tests

- enumerate every registered emittable operation;
- stable operation IDs;
- signature/default/required extraction;
- `Literal` choices;
- operation-spec roles/capabilities;
- no duplicate operation IDs;
- catalog insertion eligibility reflects actual supported mutations.

### 19.2 Workflow construction tests

- representative VG2 fixtures map to expected steps/scopes/artifacts;
- compiler dataflow maps to workflow artifacts exactly;
- unsupported blocks remain read-only;
- diagnostics map to correct semantic step IDs;
- operation title/summary comes from core metadata/roles.

### 19.3 Generated-document/editor tests

- safe literal types;
- dynamic expression remains read-only;
- source spans patch the intended value only;
- Unicode offsets;
- syntax/compile validation;
- invalid type/choice;
- duplicate changes;
- stale semantic ID failure;
- stable IDs under unrelated-call insertion.

### 19.4 Projection/dataflow tests

- rename output;
- rename input;
- one-to-many input bindings;
- duplicate producers;
- producer removed/renamed while consumer draft remains old;
- conditional/loop artifact flags;
- multi-document dirty-state projection;
- path normalization rules.

### 19.5 SQL parity tests

Before deleting TypeScript semantics, capture fixtures for:

- SELECT expressions/aliases;
- joins and join keys;
- filters;
- nested expressions currently supported;
- comments/quoting currently supported;
- unsupported query forms;
- add/remove/reorder/update transformations.

The Python port must match expected outputs/semantic structures. Once parity is demonstrated, delete the TypeScript parser/transformer in the same stage.

### 19.6 API contract tests

- catalog serialization;
- open/inspect;
- workspace projection;
- preview/apply;
- capability actions;
- capability options;
- conflicts/path confinement;
- OpenAPI schema export.

### 19.7 Frontend tests

Use Vitest + React Testing Library where component behavior is material.

Priority tests:

- generic parameter controls by metadata;
- editor capability registry/fallback;
- workspace reducer transitions;
- async stale-response rejection;
- inactive-tab dirty projection request payload;
- SQL component emits semantic actions rather than SQL strings;
- tree/sidebar consume the same projection.

### 19.8 End-to-end/manual validation

At the end of each affected stage verify:

1. translate/open a representative VG2 workflow;
2. edit a normal scalar parameter;
3. preview diff;
4. apply and reopen;
5. edit a SQL query structurally;
6. rename SQL output and observe dependency update;
7. open producer and consumer in separate tabs, edit producer while inactive/active switching;
8. trigger a conflict by externally changing the generated file;
9. verify read-only unsupported steps;
10. verify catalog entries match registered utilities.

## 20. Architectural guardrails

Add lightweight checks/tests for these invariants:

1. `src/vg2c` never imports `vg2c_ui`.
2. Generic frontend modules do not import `src/sql/*` semantic modules (those modules should eventually be presentation-only).
3. No handwritten TypeScript interface duplicates `WorkflowSnapshot`, `WorkflowStep`, `ParameterState`, `WorkflowArtifact`, command or capability API models.
4. Artifact normalization and producer/consumer analysis exist only under core.
5. SQL parsing/transformation exists only under core.
6. Utility semantic metadata is attached to registered utility/method definitions, not a second catalog table.
7. API routes do not contain business validation beyond transport/path/conflict concerns.

These checks may be simple unit tests/import scans; do not add a custom architecture framework.

## 21. Future add-utility path

After this refactor, the future feature should use the same definitions already used for editing.

### Existing operation

```text
WorkflowStep
  -> UtilityOperationDefinition
  -> ParameterDefinition[]
  -> render standard/specialized editor
  -> ChangeSet
  -> core project/preview/apply
```

### Add operation later

```text
GET catalog
  -> UtilityOperationDefinition
  -> user selects operation
  -> create draft ParameterState values from same definitions/defaults
  -> render the same standard/specialized editor
  -> future AddUtility change
  -> core validates/renders/inserts
```

There must not be an “add utility metadata” model separate from the existing-operation metadata model.

When implementing add/remove later, extend the core editor with real mutation types and advertise them through `supported_mutations`. Do not add frontend allow-lists.

Starting an empty workflow can then be implemented as a core-generated empty editable document plus `AddUtility` changes rather than a separate editor mode.

## 22. Risks and mitigations

### Risk: Pydantic becomes a core dependency

This is intentional to eliminate schema duplication. If preserving a no-Pydantic core becomes a hard requirement, stop before Stage 1 and choose standard dataclasses plus generated API DTOs. Do not proceed with two manually maintained models.

### Risk: SQL port changes formatting/edge behavior

Mitigation: capture parity fixtures before the port and do not change parser technology at the same time.

### Risk: projection endpoint feels slower than browser-only calculations

Mitigation: local raw input state + debounced projection + stale-request cancellation. The semantic contract is more important than sub-100 ms local dependency calculation; optimize the core projection after measuring.

### Risk: generated-source parameter IDs remain imperfect

Mitigation: improve IDs now to semantic call target + occurrence and test unrelated-call stability. Defer full emitter manifest only if current emitter architecture makes it disproportionately invasive.

### Risk: broad refactor crosses compiler and UI

Mitigation: staged ownership moves, characterization tests, and deletion of old implementation at each stage. Never change SQL parser technology, visual design and state architecture in the same stage.

### Risk: utility signatures do not contain enough semantics for catalog insertion

Mitigation: `operation_spec` supplies only non-inferable roles/capabilities. Keep it adjacent to the utility method and test catalog consistency.

## 23. Explicitly deferred work

Unless implementation reveals it is required for correctness, defer:

- full emitter-produced source/edit manifest;
- full add/remove/reorder utility implementation;
- drag/drop workflow editing;
- third-party/plugin utility discovery;
- replacement of the SQL parser with SQLGlot or another parser;
- generalized non-file artifact graph beyond types actually supported by vg2c;
- Redux/global state library;
- offline/browser semantic engine;
- automatic external database metadata discovery when no provider is configured.

The architecture must permit these features, but this refactor should not implement them speculatively.

## 24. Completion criteria

The refactor is complete when all of the following are true:

- `vg2c` exposes one documented workflow/catalog/editing public API;
- workflow semantic models live in core;
- utility definitions are derived from the real registry and signatures;
- non-inferable semantic roles are co-located with utility methods;
- generated workflow editability/validation lives in core;
- draft dependency/artifact projection lives in core;
- cross-document dirty-state projection lives in core;
- SQL parsing/transformation lives in core;
- frontend SQL code is presentation-only;
- generic frontend modules have no SQL-specific semantic imports;
- handwritten backend/TypeScript semantic contract duplication is removed;
- all tabs have independent draft/request state with request ownership;
- obsolete semantic modules and route aliases are deleted;
- tests demonstrate that a new ordinary utility/parameter appears without generic frontend changes.

The desired final dependency chain is therefore:

```text
VG2 / generated workflow
        |
        v
       vg2c
        |
        v
WorkflowEngine + immutable semantic models
        |
        v
thin persistence/HTTP adapter
        |
        v
thin React presentation/state layer
```

No browser-side semantic reconstruction should remain.