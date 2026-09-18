# SQLPathFinder Semantic Workbench Refactor Plan

> Planning status: planning only. No implementation changes are authorized by this document.
>
> Repository verified: VectorLim/SQLPathFinder_PY_Migration
>
> Verified remote branch: main
>
> Verified remote HEAD: 1499da19caf8726b63b8951d47732397d09a4c10
>
> Verification date: 2026-09-18
>
> Local checkout limitation: this planning pass can inspect the connected GitHub repository but cannot inspect the uncommitted state of C:\Project\SQLPathFinder_PY_Migration. Therefore local git pull, current local branch, local HEAD, and git status must be rechecked before implementation begins. No conclusion in this document assumes that local-only changes do not exist.

## 1. Executive direction

This refactor should be treated as a semantic-editor redesign, not as a visual redesign.

The target product has three primary user questions:

1. What does this script do?
2. What can I safely change?
3. What files, variables, macros, and email actions are affected?

The compiler must remain the semantic authority. The browser should present compiler-owned meaning rather than reverse-engineering generated Python, SQL, file relationships, macros, or conditions.

The target dependency direction is:

source script
→ parse/classify/resolve
→ compiler-owned semantic operations
→ emission/runtime implementation
→ editable semantic bindings
→ file/symbol/context projections
→ API transport views
→ workbench state
→ feature UI

The most important architectural shift is to stop treating raw emitted runtime invocations as the primary user-facing workflow model. Ordinary utility steps may still derive their semantic operation automatically from @emittable metadata, but structural controls and composite blocks need first-class compiler-owned semantic operations.

Examples:

- a SQLite query remains one Run Query operation even though runtime setup contains a reader object and PipelineContext plumbing;
- ROWS-IN-FILE becomes one Check Row Count operation even though the generated implementation combines CsvIO.row_count and MacroState.set_named;
- START-MACRO becomes one For Each Macro Row operation instead of a scope plus a meaningless Macro Control/pass step;
- IF-THEN becomes one editable Condition operation whose operands are represented semantically, not as ctx.macro.named(...) Python;
- email remains one Send Email operation, and bulk email actions edit those same authoritative operations rather than a parallel email model.

## 2. Verified current-state architecture

### 2.1 Compiler pipeline

The current compile pipeline in src/vg2c/compilation.py is coherent and should be preserved:

read source
→ vg2c.frontend.parse
→ vg2c.frontend.classify
→ vg2c.resolver.resolve
→ vg2c.dataflow.analyze
→ vg2c.dispatch.dispatch
→ vg2c.emitter.emit
→ CompilationResult

CompilationResult contains the resolved program, analyzed dataflow, dispatched SQL information, emitted source/metadata, and diagnostics.

This is a useful separation of concerns. The refactor should extend this pipeline rather than build a UI-only semantic pipeline.

### 2.2 Utility registration and metadata

src/vg2c/utilities/_base.py provides:

- UtilitySpec registry;
- Kind-to-emitter registration;
- lazy loading of concrete utility checks;
- @emittable operation discovery.

src/vg2c/emitter/models.py and src/vg2c/utility_metadata.py already provide a strong metadata foundation:

- stable operation and parameter IDs;
- parameter schemas inferred from Python annotations;
- required/default/nullable metadata;
- internal parameter flags;
- capabilities and parameter capabilities;
- artifact roles;
- file-effect declarations;
- supported mutation metadata;
- source ranges for editable generated values;
- omitted/default parameter reconstruction.

This should be kept and extended. A separate frontend utility registry would be a regression in ownership.

### 2.3 Structural controls

Structural semantics currently live primarily in:

- src/vg2c/operands/base.py
- src/vg2c/operands/conditional.py
- src/vg2c/operands/loop.py
- src/vg2c/operands/macro.py
- src/vg2c/resolver/scope_builder.py

The scope tree owns program, macro, loop, if, branch, and leaf structure.

The emitter walks this scope tree in src/vg2c/emitter/walker.py. Structural payloads emit Python directly through IndentWriter, while leaf blocks are emitted through UtilitySpec.

This distinction is internally reasonable, but it creates an editor gap: structural controls do not currently participate in the same editable semantic manifest as @emittable utility parameters.

### 2.4 Editing and persistence

src/vg2c/editing.py projects ParameterChange objects against compiler-owned emitted metadata.

src/vg2c_ui/services/document_store.py:

- recompiles the source;
- validates source/output/compiler hashes;
- applies semantic parameter changes;
- previews a generated-Python diff;
- writes validated generated output;
- persists parameter changes in a sidecar;
- reopens the document by recompiling and replaying the sidecar.

src/vg2c_ui/services/sidecar.py currently persists only parameter_id/value pairs in sidecar version 2.

This is a valuable safety boundary. The source VG2 file is not silently rewritten, and stale generated output becomes read-only. The refactor should preserve this invariant while broadening the set of compiler-owned bindings that the existing ParameterChange model can address.

### 2.5 File/dataflow models

There are two overlapping projection families.

Current legacy/static analysis:

- src/vg2c/dataflow/analyzer.py
- src/vg2c/dataflow/models.py
- producer/consumer records and DataflowEdge
- AnalyzedProgram.artifacts

Current effect/state projection:

- src/vg2c/dataflow/file_effects.py
- src/vg2c/workflow.py
- FileEffect and FileEndpoint
- operation IDs, step IDs, dependency state, external/missing/possible status
- workspace links/issues after pending edits

DocumentStore currently uses vg2c.workflow.project_workflow and not vg2c.dataflow.projection.

src/vg2c/dataflow/projection.py remains separately tested by tests/dataflow/test_projection.py and overlaps substantially with the newer workflow projection. It is a strong consolidation candidate, but must not be deleted until its tests and any hidden consumers are migrated.

### 2.6 SQL editing

src/vg2c/sql_editor is already backend-owned and should remain so.

It provides:

- tokenization/parsing;
- editable SELECT columns;
- editable filters;
- editable joins;
- source expressions;
- structural capability checks;
- safe transforms;
- add/update/remove actions;
- selection reorder by target index;
- preservation of unsupported or complex SQL as read-only/raw.

The frontend must not add a second SQL parser.

The parser already isolates one unambiguous SELECT statement within multi-statement SQL. That can support a pre-query/main-query/post-query presentation without teaching TypeScript SQL semantics.

### 2.7 Globals

src/vg2c/emitter/globals.py hoists repeated literal values into generated globals and gives them stable parameter IDs such as global:LOT.

Current strengths:

- shared values reuse one global;
- conflicting values are disambiguated;
- shared edits already propagate through the same parameter ID;
- tests protect shared-edit behavior.

Current gap:

- there is no explicit symbol/reference graph for the UI;
- runtime macro references are rendered as implementation expressions such as MacroState.named/ctx.macro.named;
- introduction and usage locations are not projected as first-class semantic references;
- script settings are generated but not presented as a coherent Globals view.

### 2.8 API contracts

src/vg2c_ui/api/models.py currently uses schema version 4.

Important current transport concepts include:

- DocumentView
- StepView
- ScopeView
- OperationView
- ParameterView
- ArtifactView
- FileEffectView
- workspace projection models
- structured SQL models

src/vg2c_ui/api/serialization.py mostly serializes compiler-owned information, which is good. However it also exposes technical fields and contains fallback behavior that should disappear after semantic coverage is complete:

- function_name;
- utility module/class names;
- raw generated code;
- internal parameters in the transport;
- fallback Unsupported operation records;
- fallback Compiler source/control operation steps;
- frontend-facing display labels that can be dominated by PROMPT-TEXT rather than the actual utility operation name.

### 2.9 Workspace/security model

src/vg2c_ui/services/workspaces.py provides cookie-scoped workspaces with:

- inputs and generated directories;
- workspace-safe path resolution;
- upload size/count/total limits;
- an upload suffix allowlist;
- no arbitrary host filesystem access;
- disabled generated-workflow execution.

The browser already uses native file/folder pickers and uploads selected files into the workspace.

Therefore email attachment picking should reuse this security model. A browser picker may select a local file/image, but the server should receive an uploaded workspace copy, not an arbitrary Windows path.

### 2.10 Current frontend

The current workbench is already a three-column layout on wide screens:

- Script Logic
- Configuration
- File Flow

At narrower widths it switches to pane tabs.

Important current components:

- App.tsx: top-level composition, workbench pane selection, navigation wiring
- ScriptTree.tsx: ARIA tree, scope hierarchy, search, keyboard navigation
- OperationEditor.tsx: generic operation editor
- ParameterField.tsx: generic ValueSchema-driven fields
- StructuredSqlEditor.tsx: backend-driven SQL actions
- file-flow/FileFlowPane.tsx and FlowRow.tsx
- useWorkspace.ts and workspaceState.ts: controller/state machine
- api.ts: transport

Current CSS is still heavily concentrated in styles.css. Newer workbench rules coexist with older workspace/editor/context selectors, producing clear cleanup opportunities.

## 3. Maintainability classification

### KEEP

Keep these responsibilities unless characterization tests prove a defect:

- compile_document stage boundaries;
- UtilitySpec and @emittable registry;
- ValueSchema/type-derived parameter metadata;
- artifact roles and FileEffectDefinition;
- stable semantic parameter IDs;
- backend-owned SQL parser/transforms;
- generated TypeScript contracts from Pydantic;
- DocumentStore hash/revision validation;
- sidecar-based validated edit persistence;
- workspace path isolation;
- useWorkspace stale-response protection;
- workspaceState undo/redo/edit state machine;
- ScriptTree keyboard/ARIA behavior as a behavioral baseline;
- motion tokens and reduced-motion policy;
- current browser upload boundary.

### REFACTOR

- emitted metadata: add a first-class user semantic operation projection;
- structural scope payloads: expose editable semantic bindings;
- globals: add explicit symbol/reference projection;
- ArtifactView: use stable operation references, not only step IDs;
- ParameterView: expose normalized editor/file/symbol hints instead of technical internals;
- OperationEditor/ParameterField: move into feature/shared modules;
- SQL editor presentation: tabs, file sources, reorder UX;
- workbench layout: persistent resizable/collapsible panes;
- CSS: feature ownership instead of one large styles.css;
- workspace upload policy: extend for safe attachment image types where required.

### MERGE

- vg2c.dataflow.projection with the canonical workflow/effect projection after parity;
- duplicated user-facing macro-control representations into one structural operation;
- operation-label derivation into compiler-owned semantic presentation metadata;
- file reference/navigation data into one artifact participation model;
- email overview edits into the same underlying operation/edit model;
- shared global edits and Globals view references into one symbol graph.

### REPLACE

- raw emitted invocation list as the primary UI workflow model;
- generic scope labels plus separate pass-like control steps;
- path text boxes as the default UX for known file inputs;
- nullable checkbox + separate value layout for every optional field;
- SQL Move Up/Move Down buttons;
- current File Flow rendering of reads/observes/diagnostic internals;
- breakpoint-only pane switching as the primary desktop resizing model;
- Generated information disclosure in each normal operation editor.

### DELETE after migration/parity

- src/vg2c/dataflow/projection.py if the repository-wide consumer sweep confirms only its characterization tests remain after migration;
- fallback Compiler source/control operation serialization;
- redundant Macro Control user-facing pass steps;
- frontend operationLabels semantic inference once backend summaries are authoritative;
- read/observe File Flow renderers from the user-facing lifecycle feature;
- raw SQL details from normal SQL configuration;
- move-selection action if reorder-selection fully replaces it for all consumers;
- unused technical transport fields such as module/class/function/raw_code if no approved debug consumer remains;
- stale workbench/sidebar/editor CSS selectors after feature CSS migration;
- duplicate imports in vg2c/emitter/__init__.py;
- any compatibility path introduced during migration as soon as all consumers move.

## 4. Phase A — utility semantic inventory

The inventory below distinguishes user-facing workflow operations from runtime helpers. Runtime helpers remain available to generated code but should not normally appear in Script Logic.

### 4.1 User-facing operations

| Compiler source | Proposed user name | Collapsed form | Expanded semantic summary | Editable user fields | Hidden/internal | File effects | Symbols/macros | Specialized behavior |
|---|---|---|---|---|---|---|---|---|
| PipelineContext.run_query via SqliteEngine | Run Query | Run Query | output plus source/data inputs and query summary | output; SQLite input/table bindings; header where meaningful; crosstab fields where supported; node only where meaningful; structured SQL | reader object, runtime context, dialect implementation | SQLite inputs → output transform; external query → output write; SQL_Get_CSV_List reads represented separately | lifted SQL globals; macro references; node/site | Columns/Filters/Joins editor; preserve pre/main/post query structure |
| FileSystemOps.copy | Copy File | Copy File | source → destination | source, destination | recurse when not source-configurable | copy | macro substitutions when present | file selector + manual path |
| FileSystemOps.rename | Move File | Move File | source → destination | source, destination | runtime helpers | move | macro substitutions | file selector + manual path |
| FileSystemOps.delete | Delete File | Delete File | file(s) ✕ | paths; only expose recurse if source syntax genuinely supports it | implementation traversal | delete | macro substitutions | multi-file selector + manual |
| FileSystemOps.write_file | Write File | Write File | destination | path, content when source content is a real user value | helper machinery | write | macro substitutions | multiline content |
| ExternalProcess.run | Run External Command | Run External Command | executable/arguments summary | argv; cwd when source-configurable; check only if meaningful | env runtime injection | unknown by definition | macro-expanded arguments | clearly mark file effects unknown; do not invent them |
| WaitFile.poll | Wait for File | Wait for File | path, timeout | path; timeout; interval only under optional/advanced settings | polling implementation | observe only, hidden from File Flow lifecycle | macro-expanded path | file selector/manual path |
| RowsInFile composite | Check Row Count | Check Row Count | file → target variable | file; target variable if source format permits safe editing | CsvIO.row_count + MacroState.set_named implementation | observe/read semantic dependency only | introduces named macro/global target | file selector; target symbol validation |
| StartMacro structural scope | For Each Macro Row | For Each Macro Row | source file if present | macro source file where present; prompt behavior only if meaningful | MacroState.scope/CsvIO.single_row | reads source file; no lifecycle mutation | introduces row macro namespace, usually runtime symbolic | one operation owns nested branch/body |
| RunLoop structural scope | For Each File Chunk | For Each File Chunk | input → temporary chunk | input file; chunk file; chunk size | iterator implementation and temporary loop variable | transform/chunk lifecycle if useful; temporary outputs can be marked ephemeral | runtime loop symbols | nested body |
| IfThen structural scope | Condition | Condition | concise semantic expression | lhs/ref, operator, rhs/value; conjunction and second clause where supported | Logger.condition, _operand_expr, ctx.macro.named | none | explicit symbol refs | structured condition editor; unresolved refs retained and invalid |
| SmartAppend.append | Append File | Append File | source → destination | source, destination | CSV runtime implementation | append: reads source and prior destination, writes destination | macro substitutions | lifecycle shows append/in-place |
| MailService.send | Send Email | Send Email | recipient + subject summary | enabled; to; subject; body; attachments; from only if intentionally configurable | SMTP host/port, credentials, transport plumbing | attachment reads; body may be literal or file | lifted EMAIL_TO/EMAIL_SUBJECT globals and macro values | Email tab, bulk edits, attachments |
| HtmlReport.run | Configure HTML Report | Configure HTML Report | template/report setup summary | semantic template fields that are safe | parser/runtime state internals | declared input dependencies as discovered | template macro references | preview participation |
| HtmlReport.defer | Define HTML Report | Define HTML Report | report ID + template summary | report ID where safe; template fields | deferred-state dictionary | input files referenced by template | template macro refs | preview participation |
| HtmlReport.layout | Build HTML Layout | Build HTML Layout | output file + layout summary | layout/template values and supported output | ctx and runtime report state | inputs → HTML output | template macro refs | server-side safe preview |
| HtmlReport.delete | Clear HTML Report State | Clear HTML Report State | clear report state | normally none | state reset implementation | none | none | may remain collapsed/advanced if it has low user value |
| PythonEmbed | Embedded Python | Embedded Python | read-only custom code | none by default | all Python internals | unknown | unknown | advanced/read-only, clearly marked |
| UnknownUtility | Unsupported Operation | Unsupported Operation | source command summary | none until classified | implementation placeholder | unknown | unknown | diagnostic, never pretend editability |

### 4.2 Runtime/helper operations that should normally be hidden

These remain compiler/runtime implementation details:

- Logger;
- OracleClient.configure/logging;
- SqliteReader;
- CrosstabUtility implementation;
- CsvIO.iter;
- CsvIO.single_row;
- CsvIO.sql_get_csv_list as an independent operation;
- CsvIO.row_count when owned by Check Row Count;
- CsvIO.iter_chunks when owned by For Each File Chunk;
- CsvIO.write when owned by a higher-level semantic operation;
- MacroState.named;
- MacroState.set_named when owned by a higher-level operation;
- MacroState.positional;
- MacroState.substitute;
- MacroState.eval_condition;
- MacroState.scope;
- PipelineContext.eval_condition;
- PipelineContext internals;
- reader construction;
- generated helper objects.

If one of these appears directly in generated code without an owning semantic operation, that is a compiler semantic-coverage gap to fix, not a reason to show the helper to ordinary users.

### 4.3 Parameter exposure principles

Expose a parameter only when at least one is true:

- it changes workflow behavior in a way the user can reasonably understand;
- it names a file/data source/output;
- it is a global/shared script setting intentionally meant to be changed;
- it controls a supported condition/query/email/template behavior.

Hide a parameter when it exists only to satisfy runtime plumbing, dependency injection, generated helper construction, logging, context access, credentials, or implementation bookkeeping.

Use existing internal_parameters metadata first. Add explicit editor metadata only when type/schema/role/capability cannot safely infer the desired control.


### 4.4 Verified parameter matrix and defaults

This matrix is based on the current method signatures and current source-to-emission paths, not on prior planning documents.

| Semantic operation | Current emitted/runtime parameters | User-facing classification | Default/optional behavior | Required metadata correction |
|---|---|---|---|---|
| Run Query | sql, output, reader, inputs=None, header=None, crosstab=None, node=None | sql = specialized SQL editor; output = editable file output; inputs = editable file/table bindings where SQLite; header = optional advanced output setting; crosstab = specialized optional setting; node = expose only where source query genuinely uses it; reader = internal | inputs/header/crosstab/node are omitted defaults today and can be reconstructed by @emittable | keep reader internal; add presentation/file-role hints without duplicating schema |
| Copy File | src, dst, recurse=False | src/dst editable file refs; recurse internal | source syntax currently emits src/dst only | existing recurse internal flag is correct |
| Move File | src, dst | both editable file refs | no optional fields | none beyond user title |
| Delete File | paths, recurse=False | paths editable multi-file refs; recurse should be internal for the current SPFDelete emission path | current source emission supplies paths only | add recurse to internal_parameters unless a verified source syntax actually exposes it |
| Write File | path, content | path editable output; content editable only when it originates from user source rather than a higher-level helper | no defaults | distinguish direct FileSystemOps.write_file from PipelineContext.write_file runtime use |
| Run External Command | argv, cwd=None, env=None, check=False | argv editable; cwd/check should remain hidden unless a verified source syntax emits them; env internal | current source emission supplies argv only | current env-only internal list is insufficient; characterize then mark cwd/check internal for this source form |
| Wait for File | path, timeout=30, interval=1 | path and timeout editable; interval advanced/internal by default | current source syntax supplies path and optional timeout; interval is not emitted | mark interval internal unless a real source form exposes it |
| Check Row Count | source block arguments: csv_path, var_name, prompt_off | file and target variable editable; prompt_off hidden because generated Python intentionally ignores it | prompt_off is parsed but intentionally ignored | create composite semantic binding instead of exposing CsvIO.row_count/MacroState.set_named |
| For Each Macro Row | csv_path, prompt_off | csv_path editable when non-empty; prompt_off only if it has real generated behavior | missing csv_path means static macro scope; prompt flag defaults N | do not expose MacroState.scope or CsvIO.single_row |
| For Each File Chunk | input_csv_path, chunk_csv_path, chunk_size, prompt_off | input/chunk/size editable; prompt_off hidden unless behavior is implemented | chunk size becomes 0 when omitted/invalid in current parser; prompt defaults N | validate chunk_size as a meaningful positive value before enabling edits |
| Condition | lhs, op, rhs, optional conj/lhs2/op2/rhs2, prompt_text | condition operands/operators editable; prompt may be secondary description, not primary identity | second clause optional | represent operands as semantic literal/symbol refs and reuse core _operand_expr/operator table |
| Append File | destination, source | both editable file refs | no defaults | none beyond file roles/title |
| Send Email | to, subject, body, attachments=None, from_addr=None | to/subject/body editable; attachments editable file list; from_addr optional only where current long form supplies it; planned enabled=true semantic field | short form supplies to/subject/body; long form may supply attachments/from_addr; globals can replace to/subject | add email capability, attachment artifact role, enabled field; keep SMTP/credentials internal |
| HTML run | instance=None, prompt_text=None, app_server_default=None, template=None | template and only genuinely meaningful report identity fields; prompt may be secondary; server default should be hidden unless user-relevant | all optional | explicitly classify each field after fixture characterization |
| HTML defer | id, instance=None, prompt_text=None, app_server_default=None, template=None | id and template are principal; other fields only if user-relevant | id required; rest optional | add file/symbol effects discovered from template |
| HTML layout | ctx, template, outlook=None, instance=None, json_only=None, chart_instance=None, app_server_default=None | template plus supported output/layout options; ctx always internal; other switches exposed only if fixtures prove user value | template required, others optional | current ctx is not marked internal and must be corrected |
| HTML delete | instance=None | normally no configuration or only instance when semantically meaningful | optional | keep as low-noise operation |
| Embedded Python | raw body | read-only | n/a | no editable runtime fields |
| Unsupported Operation | raw source command/body | read-only | n/a | no guessed fields |

Additional verified helper settings:

- CsvIO declares VG2C_SQL_GET_CSV_LIST_CHUNK_SIZE = 1000 as a generated script setting.
- WaitFile defaults timeout to 30 seconds and polling interval to 1 second.
- ROWS-IN-FILE's prompt-suppression argument is explicitly documented in current code as parsed but intentionally ignored.
- FileSystemOps current SPFDelete emission passes only paths; recurse is therefore currently an implementation-level optional parameter.
- ExternalProcess current source emission passes only argv; cwd/check are omitted runtime defaults and should not automatically appear just because @emittable can reconstruct them.
- HtmlReport.layout currently accepts ctx as a normal @emittable parameter even though it is runtime plumbing; this is a concrete metadata leak to correct.

## 5. Phase B — shared configuration controls

Shared controls should be created only for repeated semantic needs.

### FileReferenceField

Responsibilities:

- show known/generated workflow files from the current projected document/workspace;
- accept manual text;
- support external/manual values without overwriting them;
- display unresolved/missing state;
- use stable artifact/operation IDs for navigation;
- optionally upload a local file into the workspace when the feature allows it;
- never expose unrestricted host filesystem browsing on the server.

Variants:

- one file;
- multiple files;
- table binding file + table name;
- attachment file;
- output destination.

### SymbolReferenceField

Responsibilities:

- list known globals/macros/settings;
- permit free text;
- preserve unresolved text;
- mark unresolved references invalid in place;
- navigate to symbol details when resolved;
- never evaluate conditions in TypeScript.

### OptionalValueField

One-line unset/default/value interaction.

Preferred behavior:

- compact leading state control or clear/reset affordance;
- value control stays on the same row where practical;
- distinguish Generated/default from Override;
- reset returns to compiler-generated value.

Do not repeat a checkbox row saying Not set plus a second form block unless the value is genuinely complex.

### ReorderableList

Shared interaction primitive only after SQL columns plus at least one additional collection actually use it.

Requirements:

- drag handle;
- pointer drag;
- keyboard move command;
- announced position/change for assistive technology;
- hover/focus remove action;
- stable item IDs from backend;
- semantic mutation still performed by backend action, not by client parsing.

### CompactRow / ExpandableSection

Use for:

- operation expanded summaries;
- file participation lists;
- email overview rows;
- global reference lists.

Do not build a generic card framework.

### ValidationMessage

One consistent inline error/warning pattern for:

- unresolved symbol;
- missing/external file;
- SQL unsupported row;
- invalid field;
- stale revision;
- preview failure.

## 6. Phase C — utility section composition

### Generic utility section

For straightforward @emittable operations:

operation title
semantic summary
required fields
optional settings
validation

The form should be generated from normalized configuration metadata.

No utility-specific TypeScript switch should exist merely to render strings, booleans, enums, optional values, paths, or lists.

### Specialized capability sections

Keep specialized editors only where the interaction is genuinely domain-specific:

- structured SQL;
- condition editing;
- email attachments/bulk context;
- HTML preview/template;
- crosstab if its structure cannot be represented cleanly by generic schema controls.

Capability dispatch should be small and compiler-declared.

## 7. Phase D — feature-module architecture

Recommended frontend organization after migration:

src/vg2c_ui/frontend/src/
  app/
    App.tsx
    commands.ts
    theme.ts
  workbench/
    Workbench.tsx
    WorkbenchToolbar.tsx
    WorkbenchLayout.tsx
    workbenchState.ts
    useWorkspace.ts
    workspaceGuards.ts
  script-logic/
    ScriptLogicPane.tsx
    OperationTree.tsx
    OperationRow.tsx
    operationNavigation.ts
  configuration/
    ConfigurationPane.tsx
    OperationConfiguration.tsx
  sql-editor/
    SqlConfiguration.tsx
    SqlColumns.tsx
    SqlFilters.tsx
    SqlJoins.tsx
  context/
    ContextPane.tsx
    file-flow/
      FileFlowTab.tsx
      FileLifecycleRow.tsx
    email/
      EmailTab.tsx
      EmailBulkActions.tsx
    globals/
      GlobalsTab.tsx
      SymbolReferences.tsx
  html-preview/
    HtmlPreview.tsx
  intake/
    SourceIntake.tsx
    useSourceIntake.ts
    useWorkspaceFiles.ts
  shared/
    controls/
      FileReferenceField.tsx
      SymbolReferenceField.tsx
      OptionalValueField.tsx
      ReorderableList.tsx
      ValidationMessage.tsx
    navigation/
      OperationLink.tsx
    layout/
      ResizablePane.tsx
    motion/
      motion.css
  api/
    api.ts
    contracts.generated.ts

This is a target ownership map, not a requirement to create every directory immediately.

Rules:

- if a directory contains one tiny file with no clear future responsibility, keep it colocated instead;
- contracts remain generated from backend models;
- no giant shared/utils.ts;
- no frontend semantic registry;
- no second workspace state store;
- CSS should live with the feature that owns the selectors, with shared design/motion tokens remaining global.

## 8. Phase E — target backend/core contracts

### 8.1 Editor semantic operation

Introduce one normalized compiler-owned editor projection, conceptually:

EditorOperation
- id
- kind
- display_name
- summary
- source/block reference
- parent/branch relationship
- editable
- enabled where applicable
- configuration fields
- file participations
- symbol references
- comments
- capabilities

This should be a projection, not a second compiler.

For normal @emittable leaf operations, derive it automatically.

For structural/composite operations, the owning compiler component supplies only the metadata that cannot be inferred.

### 8.2 Editor field binding

Generalize the current emitted-parameter concept so a configuration field can point to:

- an emitted invocation argument;
- a generated global;
- a structural condition operand;
- a structural loop/macro parameter;
- a synthetic operation setting such as email enabled.

Each binding must have:

- stable semantic ID;
- current/generated value;
- schema;
- serializer/rebuilder owned by the core;
- validation;
- source range or operation reconstruction strategy;
- reset behavior.

The frontend must not know how a condition or runtime call is rendered into Python.

### 8.3 Preserve the existing ParameterChange model unless evidence requires more

The second architecture review found that introducing a parallel SemanticChange hierarchy would add abstraction before it is proven necessary.

Preferred implementation:

- keep ParameterChange(parameter_id, value, reset) as the persisted/edit transport;
- broaden what can have a stable parameter/binding ID;
- allow structural controls and synthetic operation settings to contribute EmittedParameter-compatible editor bindings;
- let the core binding own validation and reconstruction of generated Python;
- keep the existing sidecar v2 shape if these new bindings can be persisted as parameter_id/value pairs;
- increment the sidecar version only if a concrete edit cannot be represented safely by this existing shape.

This keeps one edit model for:

- ordinary @emittable arguments;
- globals;
- condition operands/operators;
- macro/loop control fields;
- Check Row Count composite fields;
- email enabled state.

The implementation should first attempt to extend the existing emitted/editor binding registry rather than create feature-specific change classes or a new generic change hierarchy.

A sidecar v3 migration is therefore a contingency, not a planned requirement.

### 8.4 Symbol graph

Compiler projection should produce:

Symbol
- stable ID
- display name
- kind: global, macro, script-setting, runtime-symbol
- value when statically known
- runtime_symbolic flag
- introduced_at operation/source reference
- references: operation ID + field role
- editable target ID when applicable

Extend CodeExpr/emission metadata or the resolver semantic representation so macro/global references are recorded structurally.

Do not scrape generated Python strings for ctx.macro.named.

### 8.5 Artifact participation graph

Artifact/file projection should produce stable operation references:

Artifact
- stable file ID/path identity
- current path/expression
- external/generated/temporary status
- participations:
  - operation_id
  - relation: create, read, observe, copy-source, copy-target, move-source, move-target, modify, append, delete, attachment, query-input, etc.
- producer operation IDs
- consumer operation IDs

File Flow renders only lifecycle relations.

File hover/focus and selectors may still use read/observe participation.

### 8.6 Comment association

First characterize actual comment forms in repository fixtures and source files.

Then:

- keep SQL comments inside SQL and let SQL safety rules preserve them;
- associate workflow-level comments with the nearest semantic operation or control owner using parser/source spans;
- preserve meaningful comments in the expanded operation view;
- never emit a fake Comment operation unless the source contains a standalone comment whose location cannot be associated safely.

Comments must not be inferred from generated implementation comments such as the current SQL filter diagnostic comment block.

## 9. Script Logic design

Default presentation is intentionally terse:

Run Query
Copy File
Check Row Count
Send Email

A selected or expanded operation may show one or two high-value lines, for example:

Copy File
source.csv → archive.csv

Condition
MY_GLOBAL >= 5

Run Query
input: source.csv
output: result.csv

Rules:

- actual semantic operation name is primary;
- PROMPT-TEXT may appear as secondary context, not replace the operation identity;
- remove Block X-Y labels;
- remove generated function names;
- remove utility module/class/method names;
- remove ctx.* expressions;
- do not show ordinary implementation helper operations;
- preserve nesting for conditions/loops;
- branch labels should be compact structural labels, not full operations;
- expanded comments appear only when meaningful;
- unsupported custom Python remains read-only and clearly identified.

Keep the existing ARIA tree/keyboard behavior unless a replacement proves parity.

## 10. Configuration design

The selected Script Logic operation drives Configuration.

Configuration should receive one normalized operation contract and should not independently discover semantic fields.

Ordering:

1. primary files/data sources;
2. principal user settings;
3. domain-specific section;
4. optional settings;
5. validation/read-only explanation.

The current Generated information section should be removed from the normal editor.

If a debug/developer view remains necessary, put it behind an explicit developer/debug command and keep it outside the normal non-technical workflow.

## 11. SQL configuration design

Top-level structure:

Source/Input
Columns | Filters | Joins

### Source/Input

For SQLite/file-backed queries:

- show input file/table bindings first;
- use FileReferenceField;
- preserve table alias/name where applicable;
- support manual external file path;
- external paths become required external artifacts.

For external Oracle-style queries:

- show meaningful connection/node/source information only if it is user-configurable;
- do not pretend SQL table names are local files.

### Columns tab

- compact rows;
- expression;
- alias where applicable;
- drag handle;
- keyboard reorder;
- hover/focus remove;
- add column action;
- no Move Up/Move Down buttons.

Backend already has reorder-selection. Prefer target-index reorder as the single mutation primitive.

### Filters tab

- connector;
- left operand;
- operator;
- right operand;
- add/remove;
- reorder only after a safe backend transform exists.

If filter reordering changes semantics due AND/OR grouping, do not enable arbitrary drag until the core can prove preservation. Characterization tests must establish the supported subset.

### Joins tab

- join type;
- source;
- join keys/predicates;
- add/remove;
- reorder only if the backend can safely rewrite joins and preserve references.

### Raw/pre/post SQL

Normal UI should not show a raw SQL block.

Use the parser's statement span to derive:

- pre-query statements;
- one editable SELECT;
- post-query statements.

Known boilerplate such as standard DROP TABLE IF EXISTS should be classified as internal/implicit and hidden when the runtime already guarantees it. Likewise, do not expose a separate SET VALUE CSV control when the utility already derives or owns that value.

If non-boilerplate pre/post statements have real user meaning, show a compact Pre-query/Post-query section using progressive disclosure.

Complex SQL that the structured editor cannot safely modify should remain visible as a concise read-only explanation, with raw SQL available only through an explicit advanced/debug mechanism.

Do not add a TypeScript SQL parser.

## 12. File Flow design

File Flow is a lifecycle view, not a complete dependency debugger.

Display these effect kinds:

- create/write;
- copy;
- move/rename;
- transform/modify;
- append/in-place;
- fan-in/merge;
- delete.

Hide from the lifecycle list:

- ordinary read;
- observe/wait;
- attachment read;
- query reads that do not mutate the file.

Those hidden relations remain in the artifact participation graph for selectors, Used By, validation, and navigation.

Examples:

a.csv ─┐
       ├→ merged.csv
b.csv ─┘

source.csv → transformed.csv

source.csv → copy → archive.csv

temporary.csv ✕

Do not show by default:

- conditional badge;
- repeated badge;
- path base;
- prior/next state labels;
- Source removed on success;
- compiler reason strings;
- state IDs;
- dependency IDs.

Show an error/warning only when it changes what the user must do, such as missing required input or conflicting output.

## 13. File reference navigation

Every displayed file should support focus/hover details:

result.csv
Used by:
- Run Query
- Check Row Count
- Send Email

Each reference uses operation_id from the semantic/artifact projection.

No filename substring search is permitted.

Clicking a reference:

1. activates the document if necessary;
2. expands ancestor scopes;
3. selects the operation;
4. scrolls Script Logic to it;
5. optionally focuses the tree row for keyboard-originated navigation.

This should reuse one shared operation-navigation service/hook rather than each context tab reinventing navigation.

## 14. Email design

Context pane Email tab lists semantic Send Email operations.

Each row shows only useful summary:

- enabled state;
- recipient;
- subject;
- attachment count;
- validation state.

Actions:

- click row → navigate/select operation;
- enable/disable one;
- multi-select;
- enable selected;
- disable selected;
- enable all;
- disable all;
- bulk-edit shared fields only when valid.

Individual email configuration remains in Configuration.

### Email enable/disable

Recommended core approach:

- add a user-editable enabled boolean semantic field to Send Email;
- default true;
- generated call may omit the default;
- when false, runtime send returns without transport side effects;
- workflow projection suppresses inactive attachment effects;
- sidecar persists the same semantic target mechanism.

Do not maintain an EmailTab-only disabled list in React.

### Bulk edits

Bulk actions should create the same underlying semantic changes as individual editing.

Shared global IDs such as EMAIL_TO and EMAIL_SUBJECT already support one-to-many edits where values are truly shared.

For non-shared fields, a bulk action may submit multiple target changes in one validated batch.

The backend/core still validates conflicts.

### Attachments

Use:

- known/generated workflow files;
- manual external workspace-relative paths;
- Upload local attachment action using the existing browser picker/upload boundary.

Extend allowed upload types only as necessary, including common images if required.

Do not expose server-side native Windows path browsing.

## 15. Globals/macros design

Context pane Globals tab contains:

- generated globals;
- meaningful macros;
- shared script settings;
- runtime symbolic macro names;
- references/usages.

Each symbol entry should show:

- name;
- kind;
- value if statically known;
- Runtime value when not statically known;
- introduction/source operation;
- reference count.

Selecting a symbol shows all references as operation links.

Examples:

LOT = 1234
Introduced: Run Query
Used by:
- Run Query
- Condition

CURRENT_ROW
Runtime macro value
Introduced: For Each Macro Row
Used by:
- Condition

Do not show ctx.macro.named(...).

Current script setting VG2C_SQL_GET_CSV_LIST_CHUNK_SIZE is a candidate for Shared Script Settings if product owners judge it meaningful. If it is not a user-level concept, keep it in advanced settings rather than Globals.

## 16. HTML preview design

Do not execute the generated script to preview HTML.

Create a constrained server-side preview service that reuses HtmlReport parsing/rendering semantics.

Recommended model:

1. compile/project pending semantic changes;
2. identify the selected HTML operation;
3. reconstruct only the relevant HtmlReport state on a fresh preview instance;
4. replay preceding HTML-report semantic operations needed for state;
5. use a preview context that:
   - allows read-only access only within the workspace;
   - captures write_file output in memory;
   - does not run external commands;
   - does not send email;
   - does not execute embedded Python;
6. return rendered HTML plus warnings and exact/approximate status.

Frontend:

- sandboxed iframe without script permission;
- loading state;
- error state;
- stale request protection;
- update after a short debounce following valid changes;
- manual Refresh Preview fallback;
- clear warning when referenced runtime-only data is unavailable.

Characterization tests must establish whether current HtmlReport layout output can be reproduced exactly for representative fixtures.

## 17. Pane composition

Wide workbench:

Script Logic | Configuration | Context

Context tabs:

File Flow | Email | Globals

The Context tabs are one feature shell, not three sidebars.

Selection/data flow:

Script Logic selection
→ selected operation ID in workbench state
→ Configuration renders operation configuration
→ Context tabs highlight related file/email/global references

Context navigation
→ shared navigateToOperation(document_id, operation_id)
→ Script Logic reveal/select
→ Configuration follows selection

Do not duplicate selected-operation state in every feature.

## 18. Resizing/collapse architecture

Desktop uses one coherent splitter model.

Suggested initial constraints to validate in browser tests:

- Script Logic preferred: 30%, min 280 px;
- Configuration preferred: 38%, min 360 px;
- Context preferred: 32%, min 300 px;
- splitter hit target: at least 8 px with a visually subtle center handle;
- Script Logic can consume remaining width when others collapse.

Manual state:

- Configuration collapsed yes/no;
- Context collapsed yes/no;
- last non-collapsed widths.

Automatic behavior:

1. preserve manual user collapse choices;
2. when available width cannot satisfy all minimums, auto-collapse Configuration first as requested;
3. if still insufficient, auto-collapse Context;
4. Script Logic gets the remaining workspace;
5. when width returns, auto-collapsed panes may restore to remembered widths unless the user manually collapsed them;
6. never overwrite a user's manual collapsed state because a resize occurred.

This ordering should be validated against representative edit workflows. If testing proves Context is more frequently needed than Configuration for a specific narrow mode, document the evidence before changing the requested priority.

Implementation should use pointer events and CSS variables/grid tracks, not dozens of breakpoint-specific widths.

Persist only lightweight layout preferences, not document semantics.

## 19. Narrow/mobile behavior

Do not render three squeezed columns.

Recommended sequence:

- wide: three resizable panes;
- medium after sequential auto-collapse: Script Logic plus one opened auxiliary pane;
- narrow/tablet/phone: one primary pane at a time with accessible pane tabs or a sheet for auxiliary panes.

Script Logic remains first priority.

Configuration and Context retain state when hidden.

Do not remount feature state unnecessarily when switching views.

Touch targets remain at least approximately 44 px where necessary.

All core tasks must work at 200% zoom without horizontal page overflow.

## 20. Motion architecture

Keep current centralized motion tokens and expand them deliberately.

One motion token source should cover:

- operation expand/collapse;
- selection highlight;
- context tab transition;
- pane collapse/restore;
- reveal highlight;
- tooltip/focus reveal.

Rules:

- semantic state changes happen immediately;
- animation only visualizes the state change;
- no timer controls correctness;
- reduced-motion disables nonessential transitions;
- no component-specific arbitrary duration constants.

## 21. Migration implementation steps

The following steps are dependency ordered. Each step is intended to be a reviewable commit boundary or small coherent commit group.

### Step 0 — local baseline verification and characterization lock

Goal

Establish the exact implementation starting point and protect existing behavior.

Current Problem

This planning session verified GitHub main but could not inspect the local checkout. Existing behavior is not fully characterized for every utility/control.

Scope

Repository root, tests, frontend Playwright coverage, no product behavior changes.

Core Changes

None except tiny testability hooks if unavoidable.

Frontend Changes

Add characterization tests only.

Refactoring

None.

Deletion

None.

Dependencies

None.

Validation

Before coding:

- git pull or otherwise reconcile intentionally;
- git branch --show-current;
- git rev-parse HEAD;
- git status --short;
- record local-only changes and exclude them from refactor commits;
- run Python suite relevant to compiler/editor/dataflow;
- npm test;
- npm run build;
- Playwright workbench test.

Add characterization fixtures for every operation listed in Phase A.

Completion Criteria

A failing future migration can be distinguished from pre-existing local behavior.

Commit Boundary

test: characterize semantic workbench baseline

### Step 1 — introduce compiler-owned EditorOperation projection

Goal

Create one normalized user-facing semantic operation model.

Current Problem

API StepView currently mirrors emitted invocations and generic scopes. Composite/structural operations are under-modeled.

Scope

vg2c emitter/resolver semantic metadata; new editor projection module; tests.

Core Changes

- introduce EditorOperation/domain projection;
- derive ordinary operations from @emittable;
- add explicit projection hooks for structural/composite controls;
- assign stable operation IDs independent of display labels;
- separate runtime invocations from user semantic operation identity.

Frontend Changes

None initially; keep old contract until projection is tested.

Refactoring

Reuse existing UtilityOperationDefinition, ParameterDefinition, scope tree, source spans, and invocation IDs.

Deletion

None yet.

Dependencies

Step 0.

Validation

- every fixture block maps to exactly one intended user operation or explicit unsupported operation;
- runtime helpers are not emitted as independent user operations where an owner exists;
- deterministic IDs across recompilation;
- generated Python unchanged.

Completion Criteria

A test can inventory the entire workflow using semantic operation names without reading generated Python.

Commit Boundary

refactor(core): add editor semantic operation projection

### Step 2 — model structural controls and composite row-count semantics

Goal

Make Condition, For Each Macro Row, For Each File Chunk, and Check Row Count first-class editable operations.

Current Problem

Structural controls write Python directly; RowsInFile is implemented through nested runtime helper calls; current API cannot edit these safely.

Scope

operands, resolver, emitter metadata, editing.

Core Changes

- structured condition model with operand/reference/operator/value;
- semantic binding/serializer for condition edits;
- semantic bindings for macro source and loop file/chunk parameters;
- explicit Check Row Count operation with file + target symbol;
- stable control target IDs;
- generated-source rebuild logic owned by core.

Frontend Changes

None beyond contract tests if needed.

Refactoring

Reuse existing _operand_expr semantics for rendering rather than duplicating condition rules.

Deletion

Prepare removal of user-facing Macro Control/pass representation, but retain until API/frontend migration.

Dependencies

Step 1.

Validation

- edited condition preserves exact core operator semantics;
- valid and invalid symbol references are distinguished;
- macro/loop generated source parity before edits;
- Check Row Count generated behavior parity;
- reset restores generated values;
- no ctx.* representation required by editor projection.

Completion Criteria

All structural controls have semantic fields and can be projected through the same validation pipeline.

Commit Boundary

refactor(core): make workflow controls semantically editable

### Step 3 — extend existing editable bindings without replacing ParameterChange

Goal

Make utility and structural edits persist through the existing validated edit mechanism.

Current Problem

Current ParameterChange lookup only knows emitted invocation/global parameters even though its persisted shape is already generic.

Scope

vg2c.editing, emitted/editor binding metadata, DocumentStore, sidecar tests.

Core Changes

- extend the editable target registry so structural/composite fields can expose stable parameter IDs;
- reuse ParameterChange(parameter_id,value,reset);
- let each binding own schema validation and generated-source reconstruction;
- preserve shared-global conflict handling;
- preserve reset/generated-value behavior;
- keep sidecar v2 if the parameter_id/value representation remains sufficient;
- introduce a new sidecar version only after a concrete counterexample proves v2 insufficient.

Frontend Changes

No new change type should be introduced. Existing change batches remain the default contract.

Refactoring

Prefer adapting existing EmittedParameter/ParameterDefinition-compatible metadata over a new change-class hierarchy.

Deletion

Delete any transitional condition-change/email-change/macro-change classes if experimentation creates them.

Dependencies

Steps 1–2.

Validation

- existing v2 sidecars reopen unchanged;
- structural edits save/reopen;
- stale hashes still reject edits;
- rollback on sidecar write failure still works;
- preview remains side-effect free;
- ordinary parameter edits remain byte-for-byte behavior compatible.

Completion Criteria

Every planned editable field can persist through ParameterChange, or the implementation has a documented concrete reason why a narrowly scoped sidecar evolution is required.

Commit Boundary

refactor(editor): extend validated editable bindings

### Step 4 — add symbol/global/macro reference graph

Goal

Provide authoritative Globals data and condition symbol validation.

Current Problem

Globals exist as editable generated parameters, but references and macro symbols are not projected structurally.

Scope

resolver/emitter globals, CodeExpr/reference metadata, editor projection.

Core Changes

- add Symbol and SymbolReference domain structures;
- record global references from existing global_names;
- record named/positional macro references structurally;
- record symbol introductions by macro scopes and Check Row Count;
- expose script settings with descriptions;
- preserve runtime-only symbols as symbolic.

Frontend Changes

None initially.

Refactoring

Do not parse generated Python to find references.

Deletion

Any temporary regex-based symbol extraction used during development.

Dependencies

Steps 1–3.

Validation

- global reference counts match fixtures;
- shared globals link to all operations;
- unresolved typed identifiers remain present and invalid;
- runtime macro values are not fabricated.

Completion Criteria

Globals can be rendered without ctx.macro.named text or frontend inference.

Commit Boundary

feat(core): project workflow symbols and references

### Step 5 — consolidate file/artifact projection

Goal

Create one canonical file participation and lifecycle projection.

Current Problem

Legacy analyzer projection and workflow FileEffect projection overlap; ArtifactView currently focuses on step IDs.

Scope

vg2c.dataflow, vg2c.workflow, API serialization tests.

Core Changes

- add operation-level ArtifactParticipation;
- keep read/observe relations for dependency/reference use;
- derive lifecycle events for UI;
- ensure manual input path edits become external required files;
- represent fan-in from multiple inputs;
- mark temporary/chunk outputs where appropriate;
- port useful tests from dataflow.projection to canonical projection.

Frontend Changes

None initially.

Refactoring

Merge behavior from dataflow.projection into the canonical projection only where it adds behavior not already covered by workflow.py.

Deletion

After repository-wide consumer verification and test parity, delete src/vg2c/dataflow/projection.py and obsolete duplicate projection models/functions.

Dependencies

Steps 1–3.

Validation

- pending file edits reproject immediately;
- external/missing/duplicate output behavior preserved;
- stable operation refs;
- cross-document workspace links preserved;
- all old projection tests have equivalent canonical tests.

Completion Criteria

One projection path answers file lifecycle, dependency, and navigation questions.

Commit Boundary

refactor(dataflow): consolidate artifact and lifecycle projection

### Step 6 — normalize configuration contracts and file/symbol editor hints

Goal

Expose enough semantic metadata for generic controls without frontend registries.

Current Problem

ParameterView carries low-level metadata but lacks a complete normalized file/symbol/editor contract.

Scope

core editor projection, API models/serialization/contracts.

Core Changes

- expose normalized user label/help only where needed;
- expose file role/direction/multiplicity;
- expose symbol-reference role;
- expose optional/default/reset state;
- keep internal fields filtered from normal editor contract;
- use existing schemas/capabilities as source of truth.

Frontend Changes

Regenerate contracts; no major UI rewrite yet.

Refactoring

Reduce technical UtilityView transport fields if no current consumer requires them.

Deletion

Remove fields proven unused after current UI is migrated, not before.

Dependencies

Steps 1–5.

Validation

Contract tests plus utility inventory snapshot test.

Completion Criteria

A generic frontend can choose file/select/optional/symbol/string/number/list controls without knowing utility names.

Commit Boundary

refactor(api): expose normalized operation configuration contract

### Step 7 — build shared controls and migrate generic operation forms

Goal

Create bottom-up UI primitives used by multiple operations.

Current Problem

ParameterField renders all paths as plain strings and optional nullable fields as bulky checkbox/value pairs.

Scope

shared controls, configuration module, current OperationEditor/ParameterField.

Core Changes

None beyond fixes discovered by contract use.

Frontend Changes

- FileReferenceField;
- SymbolReferenceField;
- OptionalValueField;
- compact validation;
- generic schema field decomposition;
- operation configuration shell.

Refactoring

Split ParameterField by real control responsibility; preserve generic schema recursion for nested data.

Deletion

Delete old monolithic field branches after all generic consumers migrate.

Dependencies

Step 6.

Validation

Keyboard, reset/default state, manual path preservation, unresolved symbol display, screen reader labels.

Completion Criteria

Straightforward utilities need no utility-name-specific rendering.

Commit Boundary

refactor(ui): introduce semantic configuration controls

### Step 8 — finish SQL configuration contract and UX

Goal

Deliver Source + Columns/Filters/Joins configuration with backend-owned mutation semantics.

Current Problem

Current SQL UI is vertically stacked, shows raw SQL, uses explicit arrow buttons, and does not present query input binding as part of SQL configuration.

Scope

vg2c.sql_editor, PipelineContext query metadata, SQL API, sql-editor frontend.

Core Changes

- expose pre/main/post statement segmentation;
- classify known internal boilerplate;
- add safe reorder actions for filters/joins only if semantics can be guaranteed;
- otherwise explicitly declare reorder unsupported for those collections;
- keep target-index reorder for columns;
- expose file-backed query inputs via operation config, not SQL text parsing.

Frontend Changes

- Source/Input block;
- Columns/Filters/Joins tabs;
- drag/keyboard reorder;
- compact remove;
- no normal raw SQL section;
- concise unsupported-complex-query state.

Refactoring

Reuse current StructuredSqlEditor action plumbing and parser.

Deletion

- Move Up/Down buttons;
- normal raw SQL details;
- move-selection action after reorder-selection is the sole consumer path;
- obsolete SQL CSS.

Dependencies

Steps 6–7.

Validation

SQL columns, filters, joins, reorder, complex read-only cases, comments, multi-statement pre/post preservation, SQLite and external query fixtures.

Completion Criteria

No SQL semantics are implemented in TypeScript; all supported edits round-trip through backend actions.

Commit Boundary

feat(sql-ui): restructure semantic query configuration

### Step 9 — add safe HTML preview

Goal

Preview HTML using actual report semantics without executing arbitrary workflows.

Current Problem

No preview exists, and a naive browser renderer would duplicate semantics or create a security risk.

Scope

HtmlReport, preview service/API, html-preview feature.

Core Changes

Add isolated preview/replay service with workspace-safe reads and captured writes.

Frontend Changes

Sandboxed iframe, loading/error/exactness state, refresh/debounce.

Refactoring

Reuse HtmlReport parsing/rendering functions; extract pure helpers only where the preview and runtime both genuinely need them.

Deletion

Delete any temporary approximate frontend template renderer.

Dependencies

Steps 3, 6, 7.

Validation

Current runtime HTML fixtures, malicious/script-containing template sandbox test, missing input warning, stale request test.

Completion Criteria

Representative preview matches runtime output where all inputs are available and cannot execute external/email/Python operations.

Commit Boundary

feat(html): add isolated template preview

### Step 10 — make email a semantic capability with bulk actions

Goal

Support Email context overview, enable/disable, bulk actions, and safe attachments.

Current Problem

MailService.send is editable only as ordinary fields; no operation enabled state or overview/bulk feature exists.

Scope

MailService metadata/runtime, semantic change projection, workspace uploads, Email tab.

Core Changes

- enabled field/capability;
- inactive operation effect suppression;
- explicit attachment file roles;
- image upload allowlist additions with existing quotas;
- bulk change validation using existing semantic target mechanism.

Frontend Changes

- Email tab;
- selection and mass enable/disable;
- valid bulk field editing;
- attachment FileReferenceField;
- Upload attachment via native browser picker.

Refactoring

Do not create a second email configuration object.

Deletion

Any Email-tab-only copied field state.

Dependencies

Steps 3–7.

Validation

single/multiple/all enable-disable; save/reopen; attachment upload; image attachment selection; workspace isolation; shared global bulk changes.

Completion Criteria

Email overview and Configuration edit the same operations and same target IDs.

Commit Boundary

feat(email): add semantic email overview and bulk controls

### Step 11 — build Globals context feature

Goal

Expose global/macro/script-setting values and reference navigation.

Current Problem

No current Globals view; references are not discoverable.

Scope

Globals tab and shared navigation.

Core Changes

None if Step 4 contract is sufficient.

Frontend Changes

Symbol list, detail view, references, introduction link, unresolved/runtime labels.

Refactoring

Reuse operation navigation and shared compact rows.

Deletion

Any frontend parsing of generated expressions introduced previously.

Dependencies

Steps 4, 6, 7.

Validation

global/shared values, macro symbols, runtime values, unresolved refs, operation navigation.

Completion Criteria

Every meaningful projected symbol is inspectable and navigable without Python expressions.

Commit Boundary

feat(ui): add globals and macro references

### Step 12 — refactor Script Logic around semantic operations

Goal

Make the left pane a compact understandable workflow.

Current Problem

Current tree mixes generic scopes, operation labels derived in React, block numbers, Block X-Y scope text, and multiple invocation rows.

Scope

ScriptTree replacement/evolution, operation navigation.

Core Changes

None unless semantic projection gaps are discovered.

Frontend Changes

- render semantic operation display names;
- compact collapsed rows;
- expandable summaries;
- branches/loops preserved;
- meaningful comments;
- warning state;
- operation-reference reveal.

Refactoring

Preserve ARIA tree and keyboard behavior through either incremental ScriptTree refactor or a replacement with parity tests.

Deletion

- block-range text;
- block number as semantic identity;
- extra runtime invocation rows;
- operationLabels semantic inference;
- Macro Control duplicate row;
- old ScopeSummary reads/produces if replaced by context navigation.

Dependencies

Steps 1–7, 11.

Validation

search, Arrow navigation, Home/End, scope expand/collapse, reveal from context, collapsed density, unsupported operation behavior.

Completion Criteria

A non-technical user can scan the entire workflow without seeing generated implementation concepts.

Commit Boundary

refactor(ui): render compiler semantic workflow operations

### Step 13 — create unified Context pane and compact File Flow

Goal

Replace File Flow-only third pane with File Flow / Email / Globals.

Current Problem

Current file flow is noisy and includes reads, observes, implementation status, reasons, path bases, and phase labels.

Scope

context feature, file-flow feature, existing Email/Globals modules.

Core Changes

None if canonical projection is complete.

Frontend Changes

- ContextPane tabs;
- lifecycle-only File Flow;
- fan-in visualization;
- file hover/focus Used By;
- operation navigation;
- retain meaningful errors only.

Refactoring

Reuse shared navigation and compact rows.

Deletion

- read/observe lifecycle renderers;
- status/reason/path-base/phase presentation;
- current FileFlowPane/FlowRow implementations after replacement;
- duplicated context navigation code.

Dependencies

Steps 5, 10, 11, 12.

Validation

create/copy/move/transform/append/delete/fan-in; hidden reads; hover/focus refs; cross-document links where supported.

Completion Criteria

Context answers What files/emails/globals are affected without becoming a debugger.

Commit Boundary

refactor(ui): unify contextual workflow pane

### Step 14 — reorganize frontend modules and styles

Goal

Make feature ownership obvious to future programmers.

Current Problem

Frontend src is still mostly flat and styles.css contains both old and new workbench implementations.

Scope

frontend file moves/imports/CSS only.

Core Changes

None.

Frontend Changes

Move modules toward the target ownership map, colocate feature CSS, keep shared tokens global.

Refactoring

Move, do not rewrite stable logic unnecessarily.

Deletion

- dead context-sidebar/editor-pane/context-toggle selectors;
- old workspace layout rules superseded by current workbench;
- duplicated control styles;
- stale SQL selectors;
- empty compatibility barrels.

Dependencies

Feature modules from Steps 7–13 should exist first so moves follow real ownership.

Validation

Typecheck, build, static CSS usage sweep, Playwright screenshot/interaction parity.

Completion Criteria

A programmer can identify the owner of Script Logic, Configuration, SQL, File Flow, Email, Globals, layout, and shared controls from directory structure.

Commit Boundary

refactor(frontend): organize workbench by feature ownership

### Step 15 — implement resizable/collapsible workbench

Goal

Give Script Logic priority while allowing users to allocate workspace intentionally.

Current Problem

Desktop widths are fixed grid fractions; below a breakpoint the UI jumps to pane tabs.

Scope

WorkbenchLayout, shared ResizablePane, persisted layout preferences.

Core Changes

None.

Frontend Changes

- draggable splitters;
- remembered widths;
- compact center pull-tab/arrow;
- independent Config/Context collapse;
- Script Logic expansion;
- keyboard-accessible resizing or equivalent accessible controls.

Refactoring

Replace fixed grid columns and pane-specific breakpoint state with one layout model.

Deletion

Old workbench--logic/config/flow desktop switching rules once no longer needed for the corresponding mode.

Dependencies

Step 14.

Validation

pointer resize, keyboard controls, min widths, manual collapse/restore, focus preservation.

Completion Criteria

All three panes can coexist, resize, and collapse without content overlap or semantic state loss.

Commit Boundary

feat(ui): add resizable collapsible workbench panes

### Step 16 — sequential responsive collapse and narrow layout

Goal

Make the same layout model adapt cleanly instead of accumulating breakpoint hacks.

Current Problem

Current implementation uses fixed breakpoint pane replacement.

Scope

WorkbenchLayout responsive policy and CSS.

Core Changes

None.

Frontend Changes

- automatic Configuration collapse first;
- Context collapse second;
- remembered auto-collapse state;
- restore behavior;
- one-pane narrow mode;
- touch/zoom hardening.

Refactoring

Use measured available width/minimums rather than many magic breakpoints.

Deletion

Superseded fixed breakpoint pane hiding rules.

Dependencies

Step 15.

Validation

width sweep, 1440/1024/768/390/320, 200% zoom, manual-vs-auto state tests.

Completion Criteria

No horizontal page overflow and no surprise override of user manual collapse choices.

Commit Boundary

feat(ui): add sequential responsive pane behavior

### Step 17 — motion/accessibility hardening

Goal

Polish the finished interaction model without coupling correctness to animation.

Current Problem

Current motion is centralized but only covers older tree/flow structures.

Scope

motion tokens, focus/reveal behavior, all new shared controls.

Core Changes

None.

Frontend Changes

Add feature transitions using shared tokens; reduced motion; live announcements for reorder; focus-visible behavior.

Refactoring

Remove feature-local animation constants.

Deletion

Obsolete animation selectors for deleted components.

Dependencies

Steps 12–16.

Validation

prefers-reduced-motion; keyboard-only complete workflow; reorder announcements; focus after context navigation/pane restore.

Completion Criteria

All essential tasks work with animation disabled and without a mouse.

Commit Boundary

refactor(ui): harden workbench accessibility and motion

### Step 18 — final redundancy/deletion pass

Goal

Ensure old and new architectures do not coexist.

Current Problem

Migration work can leave stale models, transport fields, wrappers, CSS, actions, and compatibility code.

Scope

Entire repository.

Core Changes

Repository-wide import/call-site sweep.

Frontend Changes

Static import/style sweep.

Refactoring

Consolidate any abstractions that ended with one trivial consumer.

Deletion

Apply the ledger in Section 22.

Dependencies

All prior steps.

Validation

- full Python tests;
- frontend contract check;
- typecheck;
- state tests;
- build;
- Playwright;
- static search for deleted concepts;
- generated-script parity fixtures;
- no weakening of tests.

Completion Criteria

There is exactly one semantic workflow projection, one edit model, one file participation projection, one operation navigation path, and one workbench layout system.

Commit Boundary

refactor: remove superseded semantic and UI paths

## 22. Deletion and consolidation ledger

| Existing code/concept | Classification | Why redundant/problematic | Replacement | Current/known consumers | Proof before deletion | Delete phase |
|---|---|---|---|---|---|---|
| src/vg2c/dataflow/projection.py | MERGE → DELETE candidate | overlaps newer workflow/FileEffect projection | canonical workflow/artifact projection | tests/dataflow/test_projection.py is verified; re-run full import sweep before deletion | all projection tests ported; zero runtime imports | Step 5 / Step 18 |
| legacy user ArtifactSummary-only projection path | REFACTOR/MERGE | lacks operation-level effect state | ArtifactParticipation + FileEffect projection | analyzer/tests | parity for external/missing/order cases | Step 5 / Step 18 |
| serializer fallback Compiler source/control operation | DELETE | exposes compiler internals because semantic coverage is missing | first-class control/composite operations | serialization/UI | every control block maps to semantic operation | Step 12/18 |
| user-facing Macro Control/pass step | DELETE | duplicates macro scope concept | For Each Macro Row semantic operation | current emitted step projection | generated parity + one UI operation per macro scope | Step 12 |
| frontend operationLabels semantic inference | DELETE/REDUCE | browser derives primary/secondary semantics | backend operation display_name/summary | ScriptTree, OperationEditor | all labels supplied by contract | Step 12 |
| StepView technical function_name | DELETE from normal contract candidate | generated Python identity not needed by user | operation/source reference | current Generated information only | no transport consumer | Step 18 |
| UtilityView class_name/module/method | DELETE from normal contract candidate | implementation detail | semantic operation metadata; optional debug endpoint | OperationEditor Generated information, secondary op label | debug need reviewed; normal UI migrated | Step 18 |
| StepView raw_code | DELETE from normal contract candidate | Python implementation leak | explicit unsupported-operation source/debug projection | OperationEditor | unsupported UX retains useful source safely | Step 18 |
| internal parameters transported to normal UI | REFACTOR/DELETE from standard view | frontend filters them after transport | backend-filtered normal config contract | OperationEditor generated details | no debug dependency | Step 6/18 |
| PROMPT-TEXT as primary step identity | REPLACE | hides actual utility name | operation display name primary, prompt secondary | serialization/ScriptTree | UX snapshots | Step 12 |
| Block X-Y scope labels | DELETE | no user semantic value | condition/loop operation plus branch structure | ScriptTree | navigation parity | Step 12 |
| multiple runtime invocation rows in ScriptTree | DELETE | exposes helper decomposition | one semantic operation | ScriptTree | composite operation tests | Step 12 |
| Generated information disclosure | DELETE from normal workflow | implementation leakage | optional developer/debug route if justified | OperationEditor | confirm no user requirement | Step 12/18 |
| ParameterField nullable Not set/Set value block | REPLACE | repetitive visual clutter | OptionalValueField | generic config | optional value tests | Step 7 |
| plain path text inputs | REPLACE default UX | users must memorize paths | FileReferenceField + manual input | ParameterField | every file-role field uses selector | Step 7 |
| SQL Move Up/Down controls | DELETE | requested drag/keyboard UX | ReorderableList + reorder backend action | StructuredSqlEditor | keyboard/pointer tests | Step 8 |
| sql move-selection action | DELETE candidate | target-index reorder subsumes one-step move | reorder-selection | StructuredSqlEditor | zero consumers after UI migration | Step 8/18 |
| normal Raw SQL details | DELETE | overwhelms normal user | structured UI + explicit advanced/debug view | StructuredSqlEditor | complex query diagnostics adequate | Step 8 |
| File Flow read/observe rows | DELETE from lifecycle UI | clutter; not lifecycle change | hidden artifact participation | FileFlowPane | Used By and validation still see them | Step 13 |
| FlowRow path base/phase/status/reason labels | DELETE from normal flow | implementation/state diagnostics | concise lifecycle + actionable warning | FlowRow | missing/external warnings preserved elsewhere | Step 13 |
| separate File Flow-only third-pane identity | REPLACE | context requires Email/Globals too | ContextPane tabs | App | navigation/state tests | Step 13 |
| flat frontend feature layout | REFACTOR | ownership difficult | feature modules | imports | build/typecheck | Step 14 |
| large mixed styles.css workbench rules | SPLIT/DELETE stale parts | old/new layouts coexist | feature CSS + shared tokens | entire UI | selector usage sweep + visual tests | Step 14/18 |
| .context-sidebar/.context-toggle stale selectors if still unused | DELETE | no current component in source tree | ContextPane new owned CSS | styles.css only after migration check | static selector search | Step 14 |
| .editor-pane/.editor-scroll old layout selectors if unused | DELETE | superseded by workbench | Workbench CSS | styles.css only after migration check | static selector search | Step 14 |
| fixed workbench fractional grid + breakpoint-only pane switch | REPLACE | cannot resize; abrupt behavior | splitter layout state | styles.css/App pane state | responsive tests | Steps 15–16 |
| duplicate imports in vg2c/emitter/__init__.py | DELETE | literal duplicate imports | single import | emitter module | tests | Step 18 |
| feature-specific navigation implementations | MERGE | risk inconsistent reveal/focus | shared operation navigation | File Flow, Email, Globals | all context navigation tests | Step 13/18 |
| email-specific copied config state | PROHIBIT/DELETE | second model would drift | semantic operation edits | new code only | code review/static search | Step 10/18 |
| frontend SQL parser/condition evaluator/file-name search | PROHIBIT | duplicate core semantics | backend/core projections | should have none | static search and review | every phase |

## 23. Testing strategy

### Characterization before replacement

Before changing behavior, add or confirm tests for every user-facing operation:

- Run Query: SQLite and each external dialect path;
- Copy File;
- Move File;
- Delete File;
- Write File;
- Run External Command;
- Wait for File;
- Check Row Count;
- For Each Macro Row;
- For Each File Chunk;
- Condition/Else;
- Smart Append;
- Send Email;
- each HTML report operation;
- Embedded Python read-only;
- Unsupported Operation.

### Core semantic tests

Cover:

- one semantic operation per source operation;
- stable IDs;
- hidden internal args;
- reset/default handling;
- composite operations;
- generated source parity;
- comments;
- control-source spans;
- symbol introductions/references;
- runtime-only macro values;
- unresolved symbol validation.

### File tests

Cover:

- known generated file selection;
- manual external path;
- changed path reprojects immediately;
- missing after delete/move;
- external required input;
- create/copy/move/transform/append/delete;
- fan-in;
- file → operation navigation IDs;
- cross-document dependencies;
- duplicate outputs.

### SQL tests

Cover:

- columns;
- aliases;
- filters;
- connectors;
- joins;
- join predicates;
- source changes;
- add/remove;
- reorder;
- comments preserved;
- complex unsupported SQL;
- CTE/set-operation read-only behavior;
- multiple statements;
- pre/main/post structure;
- hidden internal boilerplate;
- SQLite table bindings;
- external dialect parity;
- SQL globals.

### Condition tests

Cover:

- string vs numeric operators;
- VAR(...);
- placeholder macros;
- bare macro identifiers where allowed;
- typed literal;
- second predicate/conjunction;
- valid symbol selection;
- manual unresolved identifier retained and marked invalid;
- reset;
- generated Python semantic parity.

### Email tests

Cover:

- existing SMTP runtime parity;
- enabled true default;
- disabled no transport;
- one/multi/all toggles;
- shared bulk fields;
- conflicting bulk fields;
- known attachment;
- external/manual attachment;
- uploaded image attachment;
- workspace path isolation;
- save/reopen.

### HTML preview tests

Cover:

- CSS/template behavior from current runtime tests;
- deferred report state;
- layout;
- input CSV;
- missing input;
- exact/approximate indication;
- no email/external/Python execution;
- sandbox behavior.

### Frontend state tests

Cover:

- selected operation;
- ancestor reveal;
- undo/redo;
- stale async result protection;
- context tab state;
- pane collapse/manual-vs-auto state;
- remembered widths;
- no duplicate semantic state stores.

### Browser/Playwright tests

Cover at minimum:

- upload → translate → inspect → edit → preview → apply → reopen;
- compact Script Logic collapsed/expanded rows;
- file selector known/manual;
- external required file indicator;
- condition invalid symbol;
- SQL tabs and reorder;
- drag pointer;
- keyboard reorder;
- File Flow navigation;
- Globals reference navigation;
- Email bulk enable/disable;
- attachment upload;
- HTML preview;
- Configuration collapse;
- Context collapse;
- drag resizing;
- sequential auto collapse;
- restoration;
- 1440, 1024, 768, 390, 320 widths;
- 200% zoom;
- reduced motion;
- keyboard-only workflow.

### Generated-script parity

For every migration phase touching compiler semantics:

- compile representative fixture before change;
- compile after change with no edits;
- generated source must be byte-identical unless a deliberate runtime change is explicitly part of that phase;
- if deliberate, assert runtime behavior parity or intentional change separately.

Do not weaken existing tests to make the refactor pass.

## 24. Phase completion criteria summary

Phase A is complete when every source operation maps to a documented user semantic operation and runtime helpers are classified hidden/internal.

Phase B is complete when repeated configuration needs use shared controls driven by core metadata.

Phase C is complete when utility forms compose generic controls and specialized editors exist only for genuine domain behavior.

Phase D is complete when Script Logic, Configuration, SQL, File Flow, Email, Globals, and HTML Preview each have a clear owner and minimal public interface.

Phase E is complete when semantic operations, edit targets, symbols, artifacts, and specialized capabilities originate in compiler/core projections rather than TypeScript inference.

Phase F is complete when one selected-operation state drives all three panes and context navigation consistently reveals Script Logic.

Phase G is complete when pane resizing/collapse/responsive behavior follows one coherent model and Script Logic remains usable across narrow layouts.

## 25. Final architecture review

Before implementation is considered complete, repeat a repository-wide review and explicitly answer:

- Does each new module have more than a cosmetic reason to exist?
- Does any user semantic concept have two owners?
- Does React derive anything the compiler already knows?
- Is any utility name checked in TypeScript solely to decide basic field rendering?
- Are file references backed by operation IDs rather than path searches?
- Are condition semantics evaluated only in core?
- Are SQL semantics parsed/mutated only in core?
- Are email bulk edits the same semantic changes as individual edits?
- Are globals/macros represented symbolically instead of Python expressions?
- Does File Flow hide non-lifecycle reads while preserving dependency data internally?
- Are technical runtime parameters absent from the default UI?
- Did all compatibility paths get deleted after migration?
- Is dataflow projection singular?
- Is edit persistence singular?
- Is navigation singular?
- Is layout state singular?
- Can a new programmer find each feature owner quickly?
- Can a non-technical user understand each collapsed Script Logic row?

If any answer indicates duplicate ownership, revise before merge.

## 26. Significant risks and open decisions

### Local checkout divergence

The remote main state is verified, but local uncommitted/ahead changes are not. Implementation must begin by reconciling local status without discarding work.

### Structural-edit source ranges

Condition/macro/loop controls currently emit through structural writers rather than @emittable calls. The design must prove stable reconstruction/source-range behavior before UI work depends on it.

### Composite operation ownership

Check Row Count and loop/macro operations cross existing utility boundaries. The new semantic projection must not create a second runtime implementation; it should only own editor semantics.

### SQL reorder semantics

Column reorder is safe with current backend support. Filter/join drag reorder must not be promised for combinations where reordering changes boolean/join semantics. Only enable a subset the core can prove safe.

### HTML preview fidelity/security

HtmlReport has mutable deferred state and can read files/write output. Preview must replay only HTML semantics inside a workspace-limited capture context and render in a sandbox.

### Email enable semantics

The exact generated representation of enabled=false should be chosen to minimize runtime changes and file-effect inconsistencies. A boolean semantic field on the existing operation is preferable to a separate suppression layer.

### Attachment images

Workspace upload policy currently excludes common image formats. If images are approved, extend the same workspace upload policy and quotas; do not bypass the workspace sandbox.

### User comments

Current parsing does not expose a dedicated workflow comment model. Comment syntax and intended ownership must be characterized before adding a parser rule.

### Advanced/debug information

The user workflow should not contain generated function/module/class/raw-code details. Decide whether developers still need a separate debug view before removing those API fields completely.

## 27. Definition of done

The refactor is done only when all of the following are true:

- remote/local implementation baseline is reconciled before coding;
- every supported source operation has one compiler-owned semantic operation;
- Script Logic uses actual user operation names;
- collapsed Script Logic is compact;
- no normal UI shows ctx.*, reader objects, helper classes, generated function names, or block-number ranges;
- conditions are editable through core semantics;
- invalid typed symbol references are preserved and visibly invalid;
- Check Row Count file is editable with known/manual file selection;
- macro controls are not duplicated;
- Globals shows meaningful variables/macros/settings and navigable references;
- SQL uses Source + Columns/Filters/Joins with backend actions;
- normal SQL UI does not show raw SQL boilerplate;
- SQL reorder has pointer and keyboard support;
- HTML preview reuses real semantics in a safe sandbox;
- File Flow shows lifecycle changes only;
- files have stable Used By operation references;
- Email overview, bulk enable/disable, and bulk-valid field editing use the same underlying operations;
- attachments support known files plus safe local upload, including approved images;
- Configuration and Context are independently collapsible/resizable;
- automatic collapse occurs sequentially with Script Logic priority;
- responsive narrow layouts retain state;
- reduced-motion is respected;
- apply/save/reopen works for utility and structural edits;
- generated-script parity is preserved where no intentional runtime change exists;
- one canonical dataflow/workflow projection remains;
- stale fallback/compatibility paths are deleted;
- stale CSS and old UI implementations are deleted;
- full Python, contract, TypeScript, build, state, and Playwright suites pass;
- no tests were weakened to accommodate the refactor.
