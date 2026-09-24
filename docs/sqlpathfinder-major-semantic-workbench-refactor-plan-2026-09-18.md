# SQLPathFinder Major Semantic Workbench Refactor Plan

> Planning only. Do not implement from this document until the local checkout is re-verified and the plan is reviewed against that checkout.
>
> Repository reviewed: VectorLim/SQLPathFinder_PY_Migration
>
> Remote branch reviewed: main
>
> Remote HEAD reviewed: 53668669f21b4e61ffd8c90fd7c90bc93d990b4d
>
> HEAD message: Revert "docs: add semantic workbench refactor plan"
>
> Review date: 2026-09-18
>
> Local checkout limitation: this planning session can inspect the connected GitHub repository but cannot see uncommitted files in C:\Project\SQLPathFinder_PY_Migration. A network-less analysis container also could not clone GitHub, so no local pytest/npm run was executed here. Before implementation, run git pull, verify branch/HEAD, run git status, and compare local-only changes with this plan. Do not overwrite unrelated work.

---

# 1. Executive direction

This refactor should make SQLPathFinder simpler in two dimensions at the same time:

1. a non-technical user should see a compact semantic workflow instead of generated-Python machinery;
2. a future programmer should have one clear owner for operation semantics, edits, file lifecycle, symbols, and UI configuration.

The compiler remains authoritative. The browser presents compiler-owned semantics and sends semantic edits back to the core. It must not reconstruct Python, SQL meaning, file lineage, macro resolution, or condition evaluation independently.

The target dependency direction is:

source script
→ parse / classify / resolve
→ compiler-owned source semantics and control structure
→ dispatch
→ emission
→ editable semantic bindings
→ authoritative workflow projections: files, symbols, comments
→ API transport
→ frontend state
→ feature UI

Generated Python remains the execution artifact and the persistence target of validated editor changes. It should no longer be the user-facing domain model.

The highest-value structural changes are:

- make IF, macro, and loop controls first-class editable semantic operations;
- give every user-facing operation an explicit semantic name and field contract;
- stop using runtime class names such as FileSystemOps or Pipeline Context as UI labels;
- stop exposing ctx.*, reader objects, source ranges, generated function names, and other compiler implementation details in the normal UI;
- consolidate the old producer/consumer analyzer and the newer file-effect/workflow path into one authoritative file lifecycle model;
- expose one symbol catalog for generated globals and runtime macro references;
- make file inputs selectable from known workflow files while retaining manual entry;
- use the existing backend SQL parser/transformer rather than adding frontend SQL semantics;
- keep email bulk editing as a batch of edits against the same underlying Send Email operations;
- reorganize the React frontend by feature and remove the stale old layout/CSS path after parity is proven.

---

# 2. Verified current-state architecture

## 2.1 Verification status

Verified against current remote main:

- branch: main;
- HEAD: 53668669f21b4e61ffd8c90fd7c90bc93d990b4d;
- repository default branch: main;
- primary core: src/vg2c;
- primary UI/backend: src/vg2c_ui;
- React UI: src/vg2c_ui/frontend/src;
- compiler and UI tests exist under tests and frontend Playwright/state tests.

Not verified in this session because the Windows checkout is not accessible:

- local branch name at C:\Project\SQLPathFinder_PY_Migration;
- local HEAD if it differs from remote;
- local git status;
- untracked/modified files;
- local test results;
- locally installed dependencies.

Implementation preflight must therefore start with local git inspection before touching source.

## 2.2 Compiler pipeline

src/vg2c/compilation.py currently performs:

parse
→ classify
→ resolve
→ analyze
→ dispatch
→ emit

CompilationResult retains the resolved, analyzed, dispatched, emitted, and diagnostic forms.

The stage separation is useful, but the analyze stage currently carries a legacy file producer/consumer model that is increasingly duplicated by the newer emitted file-effect/workflow model.

## 2.3 Source parsing and classification

src/vg2c/frontend:

- parser.py splits source into blocks and preserves options/body/raw text/source spans;
- classifier.py delegates source-block recognition to registered EmitterUtility checks;
- models.py contains ParsedBlock, ClassifiedBlock, BlockOptions, and source-span types.

This is a sound boundary and should remain.

Comments are generally preserved inside block bodies. They are not currently first-class workflow annotations. SQL comments are explicitly recognized by the structured SQL parser and make affected structural portions read-only when safe rewriting cannot be guaranteed.

## 2.4 Resolver and control structure

src/vg2c/resolver and src/vg2c/operands already provide a useful structural model:

- program;
- macro scope;
- loop scope;
- IF;
- true branch;
- else branch;
- leaf operations.

Control payloads include:

- StartMacro(csv_path, prompt_off);
- RunLoop(input_csv_path, chunk_csv_path, chunk_size, prompt_off);
- IfThen(lhs, op, rhs, optional conjunction and second comparison, prompt_text);
- structural closing/else payloads.

The major editor gap is that these controls are not represented in the same editable binding model as emitted utility parameters.

## 2.5 IF condition semantics

The authoritative operator table is currently in src/vg2c/operands/base.py:

- EQS → string equality;
- NES → string inequality;
- LE → numeric <=;
- LT → numeric <;
- GE → numeric >=;
- GT → numeric >;
- EQ → numeric equality;
- NE → numeric inequality.

Current IF emission in operands/conditional.py builds Python expressions directly through _operand_expr and _OPERATOR_TABLE.

Important finding: PipelineContext.eval_condition and MacroState.eval_condition are not currently equivalent to full IF semantics. MacroState.eval_condition only performs equality and incomplete macro resolution. Therefore the refactor must not redirect IF execution through those helpers unless they are first brought to exact behavioral parity and protected by characterization tests.

The safest plan is to keep the existing condition renderer authoritative, add a first-class editable control binding, and have the core re-render the complete condition expression after condition edits.

## 2.6 Utility registry and @emittable metadata

The strongest existing abstraction is the UtilitySpec / @emittable path:

- utility registration;
- Kind ownership;
- inferred parameter schemas from Python type annotations;
- optional/default handling;
- internal parameters;
- artifact roles;
- parameter capabilities;
- supported mutations;
- declared file effects;
- stable invocation/parameter IDs;
- source ranges for generated values;
- reconstruction of omitted optional arguments.

This should be preserved and extended rather than replaced with a frontend utility registry.

Current weakness: UtilityOperationDefinition.title is derived from the utility class name. That produces implementation-oriented titles such as File System Ops or Pipeline Context rather than the actual user operation Copy File, Move File, Run Query, or Write File.

## 2.7 Editing and persistence

src/vg2c/editing.py:

- accepts ParameterChange;
- validates against compiler-owned schemas;
- rewrites generated Python using compiler source ranges;
- reconstructs invocations when omitted optional parameters are set;
- validates the resulting Python.

src/vg2c_ui/services/document_store.py:

- recompiles source;
- verifies source/output/compiler hashes and opaque revision;
- previews changes without writing;
- applies validated generated-output changes atomically;
- persists editor changes in a sidecar;
- reopens by recompiling and replaying the sidecar;
- turns externally modified/unreconciled generated output read-only.

src/vg2c_ui/services/sidecar.py currently stores sidecar version 2 with parameter_id/value changes.

This safety model is valuable and should remain. The change model should be generalized from emitted utility parameters to semantic bindings so structural control fields can participate without introducing a separate persistence system.

## 2.8 SQL editor

src/vg2c/sql_editor already owns:

- SELECT parsing;
- selected columns;
- filters;
- joins;
- FROM/join sources;
- safety/read-only decisions;
- add/update/remove transforms;
- reorder-selection by target index.

The React UI currently calls backend SQL inspect/action endpoints. This is the correct dependency direction.

The frontend must not gain a second SQL parser.

The backend model already has a statement_span within the full SQL source. This can support explicit pre-query / editable SELECT / post-query presentation without making raw SQL the default UI.

## 2.9 File/dataflow duplication

There are currently two overlapping approaches.

Legacy source-level analyzer:

- src/vg2c/dataflow/analyzer.py;
- src/vg2c/dataflow/models.py;
- producer/consumer records;
- DataflowEdge;
- heuristics over /CSV, /TABLE, SQL_Get_CSV_List, macro controls, and external utility text.

Newer emitted-effect/workflow model:

- src/vg2c/dataflow/file_effects.py;
- src/vg2c/workflow.py;
- FileEffect / FileEndpoint;
- operation IDs;
- stable effect IDs;
- edited parameter values;
- state tracking;
- delete/move lifecycle;
- dependency IDs;
- workspace links/issues.

DocumentStore uses vg2c.workflow.project_workflow.

src/vg2c/dataflow/projection.py is a separate older dirty-workspace projection and is still directly covered by tests/dataflow/test_projection.py.

workflow.py currently has to supplement declared effects from the old analyzer for source/control dependencies and blocks without declared invocation effects. This is the main architectural reason both models still coexist.

Target: migrate every real source/control file relationship into the semantic operation/effect model, then remove the analyzer fallback and the old projection path.

## 2.10 Globals and macros

Current generated globals:

- are extracted by utility-specific extract_globals;
- become stable parameters such as global:LOT;
- can be shared across operations;
- already support shared edits.

Current runtime macros:

- are represented through MacroState;
- can be introduced by macro rows and ROWS-IN-FILE;
- are frequently compiled into ctx.macro.named(...) or related expressions;
- do not have a first-class UI symbol/reference graph.

Target: one UI-facing symbol catalog with distinct internal kinds, not one runtime implementation.

The catalog must distinguish:

- generated compile-time editable globals with known values;
- named runtime macros with known names but possibly unknown runtime values;
- row-bound macro columns whose value exists only at runtime;
- unresolved symbolic references.

The UI should normally display symbolic names, never ctx.macro.named(...).

## 2.11 API contract

Current schema version: 4.

The normal DocumentView exposes compiler details including:

- function_name;
- block_index;
- functional_kind;
- raw_code;
- UtilityView class_name/module/method;
- low-level effects;
- internal parameters.

These are useful debugging concepts but not the right default product contract for non-technical users.

The generated TypeScript contract path in api/contracts.py is strong and should remain.

## 2.12 Frontend

Current strengths:

- one workspace reducer with undo/redo;
- protection against stale async responses;
- document instance IDs;
- operation navigation;
- generated TS contracts;
- backend SQL actions;
- keyboard-aware script tree;
- reduced-motion CSS;
- Playwright coverage.

Current problems:

- source files are mostly flat;
- semantic labels are reconstructed in operationLabels.ts;
- OperationEditor renders generic compiler parameters and exposes Generated information/raw code;
- ScriptTree shows block-oriented details and scope I/O summaries;
- File Flow renders low-level reads/observes/status/reasons;
- SQL editor exposes raw SQL and move-up/move-down controls;
- styles.css contains both an older editor/context-sidebar layout and the newer workbench layout;
- the current responsive workbench switches all panes into one-of-three tab mode at a single width instead of supporting independent collapse/resizing.

## 2.13 Workspace and file-picker security

WorkspaceManager:

- creates cookie-scoped workspaces;
- fences paths beneath the workspace root;
- rejects symlink/path escape;
- limits file count/size/workspace size;
- currently allows .txt, .csv, .tab, .dat, .xlsx, .xls uploads;
- keeps generated execution disabled by default.

For email attachments, the browser-native file chooser should upload into this existing workspace boundary. Do not create arbitrary host filesystem browsing from the server.

Approved image suffixes can be added deliberately to the upload allowlist for attachment use.

---

# 3. Current redundancy and maintainability findings

## 3.1 KEEP

- compile pipeline separation, subject to removing the redundant legacy analyze stage after migration;
- source parser/classifier;
- resolver scope tree;
- UtilitySpec registry;
- @emittable metadata inference;
- ValueSchema inference;
- internal parameter capability;
- stable emitted operation/parameter IDs;
- source-range editing;
- SQL parser/transformer;
- generated TypeScript contracts;
- atomic write/revision/hash checks;
- sidecar persistence concept;
- workspace path sandbox;
- workspace reducer stale-response protections;
- reduced-motion support;
- backend-owned CSV preview security checks.

## 3.2 REFACTOR

- operation naming/presentation metadata;
- emitted parameter model into generalized editable bindings;
- resolver control payloads into user-facing control operations;
- workflow.py into the single authoritative semantic workflow/file projection;
- globals into a real symbol/reference projection;
- DocumentView into a user semantic contract;
- ParameterField into shared semantic controls;
- ScriptTree into compact Script Logic;
- StructuredSqlEditor into tabbed Columns/Filters/Joins;
- File Flow into lifecycle-only presentation plus required external inputs;
- workbench layout into independently resizable/collapsible panes;
- styles.css into feature-owned styles plus shared tokens.

## 3.3 MERGE

- old dataflow projection responsibilities into authoritative workflow projection;
- file artifact summary + effect references into one file resource/catalog projection;
- macro control scope and meaningless macro_control/pass presentation into one structural operation;
- generated globals and runtime macro references into one UI-facing symbol catalog while preserving distinct runtime kinds internally;
- duplicated file selectors across query/delete/copy/wait/email/etc. into one FileSelector control;
- repeated navigation logic into one operation-reference navigation service.

## 3.4 REPLACE

- class-name-derived UI titles with explicit semantic operation names;
- frontend effect-based label inference with backend semantic presentation;
- low-level FileEffectView as direct rendering input with compact FileLifecycle/FileResource views;
- compiler-oriented StepView as the primary user tree node with semantic operation/control nodes;
- explicit SQL move-up/down buttons with shared reorder interaction;
- nullable Not set / Set value blocks with compact optional controls;
- current single breakpoint pane-switching with a layout state machine.

## 3.5 DELETE candidates after parity proof

- src/vg2c/dataflow/projection.py;
- tests/dataflow/test_projection.py after assertions are moved to the authoritative workflow projection tests;
- eventually src/vg2c/dataflow/analyzer.py and legacy producer/consumer/DataflowEdge models after every required source/control file relation is explicitly projected;
- analyzer fallback branches in workflow.py;
- frontend operationLabels.ts once semantic display_name/summary comes from the API;
- default Generated information/raw-code UI;
- direct frontend rendering of low-level file reasons/path-base/compiler statuses;
- obsolete move-selection API/action if reorder-selection fully covers all consumers;
- old workbench/context-sidebar CSS and old responsive pane-tab rules;
- duplicate/stale CSS moved out of styles.css during feature extraction;
- any synthetic Compiler source/control operation rows created only because semantic controls are missing;
- user-facing macro_control pass rows.

Deletion is conditional on the ledger in section 16.

---

# 4. Target core architecture

Do not introduce a second utility registry.

Use one normalized semantic operation projection built from:

1. resolver-owned structural controls;
2. emitted @emittable invocations for ordinary operations;
3. small explicit adapters only for source operations whose useful semantics cannot currently be represented by their emitted runtime call.

Recommended final core layout:

src/vg2c/
  frontend/
  resolver/
  operands/
  dispatch/
  emitter/
  utilities/
  sql_editor/
  workflow/
    models.py
    projection.py
    file_effects.py
    symbols.py
    workspace.py
  editing.py

Do not create this package structure in one mechanical move. Create/move modules only when ownership is migrated.

The final workflow domain should own:

- semantic operation tree;
- editable bindings;
- file lifecycle/resources;
- symbol definitions/references;
- meaningful comments;
- operation references;
- workspace dependencies.

The emitter owns generated Python. The UI should not need the emitter’s class/module/function implementation details.

---

# 5. Phase A — Utility semantics

This section defines the UI semantics before frontend composition.

## 5.1 User-facing operation inventory

### Run Query

Source kinds:
- SQL_QUERY;
- SQLITE_QUERY.

Display name:
- Run Query.

Collapsed:
- Run Query
- optional concise secondary summary such as output.csv or input count, never raw SQL.

Expanded:
- source/input files where applicable;
- output file;
- selected columns count;
- filter count;
- join count;
- dialect/connection only if it materially changes user behavior;
- crosstab status when present;
- pre-query/post-query presence.

Editable:
- output;
- SQLite input/table bindings;
- structured Columns;
- structured Filters;
- structured Joins;
- optional header;
- crosstab fields when present and supported;
- relevant external SQL connection target only when source semantics make it intentionally configurable.

Internal/hidden:
- reader object;
- reader implementation class;
- ctx;
- generated node plumbing;
- compiler source expressions;
- standard generated/drop-table boilerplate;
- raw SQL by default.

File effects:
- transform file inputs → output for SQLite;
- SQL_Get_CSV_List file dependencies where discovered by core;
- output creation for query output.

Specialized capability:
- structured-sql.

### Write File

Display:
- Write File.

Collapsed:
- Write File
  output/path as concise secondary text.

Expanded:
- output path;
- short content/template summary;
- comment/note if present.

Editable:
- output path;
- content/template.

Hidden:
- ctx;
- vars runtime map unless a real source feature exposes it.

File effect:
- create/overwrite output.

### Copy File

Display:
- Copy File.

Collapsed:
- Copy File
  source → destination.

Editable:
- source;
- destination.

Hidden:
- recurse unless actual source syntax explicitly exposes supported recursive behavior.

File effect:
- copy; source remains live; destination created/overwritten.

### Move / Rename File

Display:
- Move File or Rename File. Use one explicit title consistently; preferred primary label: Move / Rename File.

Collapsed:
- source → destination.

Editable:
- source;
- destination.

File effect:
- source removed/replaced by destination.

### Delete File

Display:
- Delete File.

Collapsed:
- Delete File
  target or target count.

Editable:
- one or multiple paths via FileSelector/list.

Hidden:
- recurse while current source emission does not expose it as a normal user setting.

File effect:
- deletion.

### Append File

Source:
- SmartAppend.

Display:
- Append File.

Collapsed:
- source → destination.

Editable:
- source;
- destination.

File effect:
- source read;
- prior destination may be read;
- destination becomes next state.

### Check Row Count

Source:
- ROWS-IN-FILE.

Display:
- Check Row Count.

Collapsed:
- Check Row Count
  file → macro name.

Editable:
- file;
- target macro/global name.

Hidden:
- legacy prompt-suppression flag.

Important core change:
the file path must become a first-class semantic binding. It cannot remain buried inside MacroState.set_named(str(CsvIO.row_count(...))).

Preferred implementation:
emit or project a high-level Check Row Count semantic operation with explicit path and target-symbol bindings. Runtime implementation can still call CsvIO/MacroState internally.

File effect:
- observe/read file only;
- no file lifecycle event in normal File Flow;
- if external/unresolved, show it under Required Inputs.

Symbol effect:
- introduces/updates named runtime macro.

### Wait for File

Display:
- Wait for File.

Collapsed:
- Wait for File
  path.

Editable:
- path;
- timeout.

Hidden:
- low-level polling interval unless a product requirement appears.

File effect:
- observe only; not a lifecycle row;
- unresolved external path appears as Required Input.

### Run External Program

Display:
- Run External Program.

Collapsed:
- program basename.

Editable:
- program/arguments as one structured or list value;
- cwd only if meaningful and safe.

Hidden/advanced:
- env;
- check/low-level subprocess behavior unless source semantics expose them.

File effect:
- unknown. Do not guess arbitrary reads/writes from filename-looking arguments.

### Start HTML Report

Source:
- HTML-RUN.

Display:
- Start HTML Report.

Editable:
- meaningful template/style fields;
- instance;
- prompt text only if users actually use it as a meaningful label;
- server/default fields under Advanced only if they affect behavior.

Hidden:
- runtime object state.

### Define HTML Report Section

Source:
- HTML-DEFER.

Display:
- Define HTML Report Section.

Editable:
- id;
- template/report content;
- instance and relevant optional presentation fields.

Preview:
- previewable only as part of an HTML state sequence when dependencies are available.

### Generate HTML Report

Source:
- HTML-LAYOUT.

Display:
- Generate HTML Report.

Editable:
- template/layout;
- output-related directives exposed semantically;
- relevant options such as outlook/json-only/chart-instance only when they affect visible behavior.

Hidden:
- ctx positional runtime parameter;
- renderer internals.

File effects:
- output HTML;
- referenced CSV/CSS inputs determined from parsed HTML-report semantics.

Preview:
- safe backend preview using the real HtmlReport rendering functions, never generated-script execution.

### Clear HTML Report State

Source:
- HTML-DELETE.

Display:
- Clear HTML Report State.

Collapsed:
- one compact cleanup step.

Editable:
- instance when meaningful.

No file flow row unless a real file effect is proven.

### Send Email

Display:
- Send Email.

Collapsed:
- Send Email
  recipient / subject summary.

Editable:
- enabled;
- To;
- Subject;
- Body;
- Attachments;
- From address when source supports it.

New enabled field:
- default true;
- disabling must prevent SMTP interaction;
- preferably represented as a normal semantic binding on the same operation, not a separate email model.

Attachments:
- known/generated file selection;
- manual path;
- browser file picker upload into workspace;
- approved images supported.

File effects:
- attachment files are dependencies/usages;
- not normal lifecycle rows;
- external attachments appear as Required Inputs.

### For Each Macro Row

Source:
- START-MACRO scope.

Display:
- For Each Macro Row.

Collapsed:
- For Each Macro Row
  source CSV when present.

Editable:
- input CSV.

Hidden:
- prompt-off flag.

File effect:
- read/required input only.

Symbol effects:
- row column names become runtime macro symbols when headers are statically available;
- values remain runtime/unknown.

Important:
do not also render a separate Macro Control/pass operation for the same source construct.

### For Each Chunk

Source:
- RUN-LOOP scope.

Display:
- For Each Chunk.

Collapsed:
- input → chunk output, size N.

Editable:
- input file;
- chunk output file;
- chunk size.

Hidden:
- prompt-off flag.

File effects:
- input required/read;
- chunk file is a produced transient state inside loop.

### Condition

Source:
- IF-THEN scope.

Display:
- Condition.

Collapsed:
- concise human-readable expression:
  CONFIG <= 0
  or
  STATUS equals READY.

Editable first clause:
- left operand: symbol/global selector with manual type-in;
- operator: core-provided operator choice;
- right operand: literal value or symbol/global selector where supported.

Optional second clause:
- AND/OR;
- left operand;
- operator;
- right operand.

Prompt text:
- optional note/label, not the primary operation name.

Invalid typed symbol:
- retain exact user input;
- binding remains draft;
- mark unresolved with red/error state;
- no silent substitution.

Core authority:
- operator types and comparison semantics come from the core;
- frontend receives allowed choices and operand constraints;
- TypeScript never evaluates or normalizes the condition.

### Embedded Python

Source:
- PYTHON_EMBED.

Display:
- Embedded Python.

Default:
- compact read-only/advanced step;
- no raw implementation detail in the normal collapsed workflow.

Editing:
- not part of the first refactor unless a safe source-level editor is explicitly required later.

Advanced:
- raw body may be shown only under a deliberate developer/advanced disclosure.

### Unsupported / Unknown block

Source:
- Kind.UNKNOWN.

Display:
- Unsupported Operation, or a source-specific semantic name when a known unsupported family such as JSL can be identified safely.

Behavior:
- retain in workflow so users know something happens;
- read-only;
- meaningful source prompt/comment summary;
- do not expose generated pass/TODO as the operation.

## 5.2 Runtime/support operations that must stay hidden

These methods are useful compiler/runtime building blocks, not independent Script Logic rows:

CsvIO:
- iter;
- single_row;
- sql_get_csv_list;
- row_count;
- iter_chunks;
- write.

MacroState:
- named;
- set_named;
- positional;
- substitute;
- eval_condition;
- scope.

PipelineContext implementation helpers:
- ctx plumbing;
- reader;
- _read_datasyncx;
- utility container references.

Reader/runtime support:
- SqliteReader.execute;
- OracleClient setup;
- dialect reader classes.

Crosstab:
- expose as a Run Query configuration subsection, not a separate workflow step.

Compiler handler classes:
- SqliteEngine;
- source classifier utilities;
- UnknownUtility implementation;
- emitter helper functions.

## 5.3 Utility presentation metadata

Extend existing operation metadata minimally.

Add explicit semantic display metadata where inference is unsafe:

- display_name;
- optional description;
- parameter presentation overrides only where needed;
- visibility: normal / advanced / internal;
- semantic field role such as file-input, file-output, symbol-ref, attachment, sql;
- user label when humanizing the Python parameter name is insufficient.

Do not add:
- a TypeScript utility catalog;
- a second manual backend schema duplicating Python annotations;
- a summary-template language.

Summaries should be produced by the semantic projection from normalized field roles/file effects, with a small specialized projector only where necessary.

---

# 6. Phase B — Shared configuration controls

## 6.1 Normalized editable binding

Introduce one core-facing editable binding contract for the UI.

A binding needs:

- stable id;
- owner operation id;
- name;
- user label;
- schema;
- value;
- default;
- required/optional;
- visibility;
- semantic role/capabilities;
- validation state;
- unresolved state where applicable;
- reset support.

Existing EmittedParameter becomes one source of bindings.

Structural control fields become another source.

The frontend should not care whether a field originated from:
- an @emittable method argument;
- an IF payload;
- a loop payload;
- a macro payload.

## 6.2 FileSelector

One shared control used by:
- query inputs;
- query output where appropriate;
- Check Row Count;
- Copy;
- Move/Rename;
- Delete;
- Append;
- Wait for File;
- Macro input;
- loop input/chunk;
- email attachments;
- HTML inputs/outputs where exposed.

Behavior:
- searchable dropdown of known workflow files;
- labels distinguish Generated / Existing workspace / External required;
- manual entry always available when the field allows it;
- manual value remains exactly what user typed;
- projected workflow immediately classifies it;
- unproduced manual input becomes Required Input;
- generated choices use stable semantic file references, not filename string searches.

For list fields:
- compact rows;
- add another;
- drag handle;
- hover/focus × removal;
- keyboard reorder.

## 6.3 SymbolSelector

Used by:
- IF left/right symbolic operands;
- any future utility field explicitly declared as a symbol reference.

Behavior:
- dropdown of known symbols;
- type-ahead/manual text;
- known symbol shows type/source;
- unresolved text retained and marked invalid;
- no TypeScript macro normalization.

## 6.4 ConditionOperatorSelect

Choices come from core metadata.

Backend provides at least:
- source token;
- display label;
- comparison type/category.

Example display:
- EQS → equals (text);
- NES → does not equal (text);
- LE → ≤ (number);
- LT → < (number);
- GE → ≥ (number);
- GT → > (number);
- EQ → equals (number);
- NE → does not equal (number).

Do not encode this table separately in the frontend.

## 6.5 OptionalValue control

Replace repetitive:
- checkbox Not set;
- separate value field.

Use one compact one-line control:
- empty/default state;
- reset/default affordance;
- value editor integrated into the same row.

For booleans:
- tri-state only when the underlying schema genuinely supports null/default/value.

For nullable scalar:
- placeholder Default / Not set;
- typing/selecting sets value;
- reset icon returns to generated default.

## 6.6 ReorderableList

Shared by:
- SQL columns;
- future ordered attachment/file lists if semantic order matters;
- other ordered collection fields with multiple consumers.

Interaction:
- drag handle;
- pointer drag;
- keyboard grab/reorder/drop;
- screen-reader live announcement;
- focus preserved after move;
- × remove on hover/focus.

Avoid explicit Move Up / Move Down buttons.

Use the existing backend target-index reorder action for SQL.

A small accessibility-focused drag library may be adopted if it materially reduces bespoke pointer/keyboard complexity; do not add a general UI framework.

## 6.7 Validation/error presentation

One shared pattern:
- field-level red/error indicator;
- concise message;
- unresolved symbol state;
- backend validation errors mapped by binding id;
- no silent coercion.

## 6.8 Expandable details

Use a shared disclosure primitive for:
- advanced optional settings;
- comments/notes;
- preview details;
- unsupported source info.

Do not use nested cards for every subgroup.

---

# 7. Phase C — Utility configuration sections

## 7.1 Generic operation configuration

Normal operations should be generated from binding metadata.

Rendering rule:
- internal → not rendered;
- advanced → Advanced disclosure;
- file-input/output → FileSelector;
- symbol-ref → SymbolSelector;
- enum → select;
- boolean → compact toggle;
- multiline → textarea;
- collection → compact list;
- object schema → structured group only when needed;
- structured-sql → SQL editor;
- unsupported dynamic expression → semantic symbolic display if core can project one, otherwise read-only advanced note.

Avoid utility-name switches in React.

## 7.2 File operation sections

Copy / Move / Delete / Append:
- source/output file fields at top;
- no implementation diagnostics;
- lifecycle preview sentence optional;
- all file choices from the same file catalog.

## 7.3 Check Row Count section

Fields:
- File;
- Save count as macro.

File:
- known/generated selection;
- manual entry.

Macro:
- name input with symbol validation;
- newly introduced symbol appears immediately in projected Globals.

## 7.4 Condition section

Normal first clause in one compact line:

[ Global / variable ▼ or type ] [ operator ▼ ] [ value or symbol ]

Second clause:
- off by default when absent;
- Add condition;
- choose AND/OR;
- same row structure.

Expanded semantic summary:
- resolve known symbol display names;
- show runtime value as unknown when not statically known.

No ctx.macro.named expressions.

## 7.5 Query section

Top:
- input/source files;
- output file.

Then tabs:
- Columns;
- Filters;
- Joins.

Optional subsections:
- Crosstab;
- Header;
- Advanced connection settings only when useful.

Raw SQL:
- not shown in normal editor;
- explicit Advanced / View raw SQL only.

Pre-query/post-query:
- preserve backend source slices around editable statement;
- show compact collapsed sections when non-empty;
- standard internal boilerplate such as routine DROP TABLE IF EXISTS can be filtered from normal presentation when the core can identify it as standard behavior;
- never delete source text merely because it is hidden from the normal UI.

## 7.6 Email section

Fields:
- Enabled;
- To;
- Subject;
- Body;
- Attachments;
- From, advanced unless explicitly set.

Attachments:
- known file picker;
- manual;
- browser upload.

No SMTP credentials/host/port.

## 7.7 HTML section

Use meaningful fields, template editor, and preview.

Preview status:
- Loading;
- Ready;
- Approximate because runtime values unavailable;
- Error.

No arbitrary runtime execution.

---

# 8. Phase D — Feature modules

## 8.1 Script Logic

Responsibilities:
- render semantic operation/control tree;
- selection;
- expansion;
- navigation highlight;
- operation comments;
- validation indicator.

Default rows:
- operation name only;
- optional short secondary semantic summary.

Remove:
- Block X-Y labels;
- generated function names;
- utility class names;
- scope read/produce dumps;
- raw Python.

Structure:
- Condition owns true/else branches;
- For Each Macro Row owns children;
- For Each Chunk owns children;
- closing tokens are not rows;
- macro-control/pass duplicates are not rows.

## 8.2 Configuration

Responsibilities:
- selected operation title/summary;
- generic binding sections;
- specialized SQL/HTML capabilities;
- validation;
- reset.

Does not own:
- utility semantic definitions;
- file lineage;
- symbol resolution.

## 8.3 SQL editor

Responsibilities:
- tabs;
- row UI;
- drag reorder;
- filter/join forms;
- call backend actions.

Does not parse SQL.

## 8.4 Context pane

One shell with tabs:
- File Flow;
- Email;
- Globals.

The shell owns:
- tab selection;
- common header;
- navigation dispatch;
- compact empty/error/loading state.

Do not build three unrelated sidebars.

## 8.5 File Flow

Responsibilities:
- lifecycle visualization only;
- Required Inputs section;
- file usage references/navigation;
- optional CSV preview.

Lifecycle shown:
- create/write;
- copy;
- move/rename;
- transform;
- append;
- merge/fan-in;
- delete.

Ordinary reads/observes:
- not lifecycle rows;
- still recorded in the file catalog for Used by and Required Inputs.

## 8.6 Email

Responsibilities:
- list all Send Email operations;
- navigate to operation;
- individual enabled switch;
- multi-select;
- enable all/disable all;
- bulk edit fields only when compatible.

Implementation:
- bulk action emits ordinary binding edits for each selected operation.

No second email data model.

## 8.7 Globals

Responsibilities:
- list all meaningful symbols;
- known value or runtime/unknown status;
- definition/introduction;
- all references;
- click reference → Script Logic operation.

## 8.8 HTML preview

Responsibilities:
- call a dedicated safe preview endpoint;
- render returned sanitized/sandboxed HTML preview;
- show missing input/runtime symbol notices.

---

# 9. Phase E — Backend/core contracts

## 9.1 Semantic operation projection

Create a core-owned projection from CompilationResult.

Recommended domain concepts:

WorkflowOperation:
- id;
- kind;
- display_name;
- description;
- parent/branch;
- source span;
- bindings;
- capabilities;
- comments;
- validation state.

OperationReference:
- operation id;
- optional binding/source context.

Do not expose class_name/module/function_name in normal transport.

## 9.2 Structural control operations

Project resolver controls as operations.

Condition:
- stable control operation id by source block/scope;
- bindings for lhs/op/rhs and optional second clause;
- core validation;
- core renderer.

Macro/loop:
- bindings from StartMacro/RunLoop payloads;
- explicit file semantic roles.

Else/closing payloads:
- structural only.

## 9.3 Generalized semantic edits

Rename/generalize ParameterChange concept only once.

Recommended:
- SemanticChange(binding_id, value, reset).

Migration:
- existing emitted parameter IDs remain valid binding IDs;
- new control bindings use stable ids;
- sidecar v3 stores binding_id/value;
- v2 can be read and upgraded because parameter IDs map directly to existing binding IDs.

Projection flow:
1. validate all requested bindings;
2. apply ordinary emitted parameter changes using existing safe source-range path;
3. apply control operation changes by re-rendering the complete affected control expression/header through the same core logic that emitted it;
4. compile/ast-validate candidate generated Python;
5. project effective files/symbols from the resulting semantic values;
6. preserve atomic save/revision behavior.

Do not make TypeScript write generated Python fragments.

## 9.4 Control emission metadata

The emitter/walker must record stable source ranges for editable control headers.

Do not create one source range per Python token when the semantic unit is one condition/header.

For Condition:
- record entire rendered boolean expression range;
- keep semantic field values separately;
- edit renderer regenerates the expression through _OPERATOR_TABLE/_operand_expr.

For macro/loop:
- record the generated scope header expression range as needed.

This avoids fragile offset surgery across multiple dependent tokens.

## 9.5 Condition validation

Core validation:
- operator token must be one of the authoritative table;
- numeric operators validate numeric literal or known numeric-compatible symbol semantics as far as statically possible;
- symbol name resolution distinguishes known/unresolved;
- unresolved manually typed symbol is returned as an error but its draft is retained in the request/UI state;
- compound clause connector only AND/OR;
- absent second clause has no dangling values.

Generated parity tests must cover every operator.

## 9.6 Symbol catalog

Add a compiler/workflow projection with:

Symbol:
- id;
- normalized display name;
- kind: generated-global / named-macro / row-macro / setting;
- value_state: known / runtime / unresolved;
- known value when safe;
- definition operation/source;
- references.

Populate from:
- emitter globals;
- ROWS-IN-FILE target macro;
- StartMacro row headers when the input header is statically available;
- placeholder/macro references captured before they become Python expressions;
- condition operands;
- SQL global extraction;
- shared script settings that are actually meaningful to users.

Extend CodeExpr or the semantic projector to carry macro symbol references instead of later reparsing ctx.macro.named strings.

Do not fake runtime values.

## 9.7 File catalog and lifecycle authority

Build one file catalog from semantic operations/effects.

FileResource:
- stable semantic id/lineage id;
- current path if known;
- status: generated / workspace / external / missing / dynamic;
- producer operation refs;
- consumer/use refs;
- lifecycle event refs.

Known-file selector uses this catalog.

Manual external input:
- when a file-input binding is changed to an unproduced path, the projected catalog creates an external Required Input resource;
- no separate manual bookkeeping.

## 9.8 Remove analyzer fallback

Before removal, explicitly cover every dependency currently supplied by analyzer.py:

- SQLite /TABLE bindings;
- SQL_Get_CSV_List inputs;
- StartMacro input;
- RunLoop input and chunk output;
- ROWS-IN-FILE input;
- query output;
- write-file output;
- supported file utility effects;
- HTML file effects;
- email attachments;
- wait-file observations.

Do not preserve external-utility filename guessing. Unknown external process effects should remain unknown.

After parity:
- dispatch should accept the resolved program directly instead of needing AnalyzedProgram merely to reach resolved blocks;
- workflow projection stops reading result.analyzed;
- CompilationResult no longer needs legacy analyzed file records;
- delete dataflow analyzer/models/projection if no remaining core consumer exists.

## 9.9 Comments

Preserve current body comments.

Add semantic ownership:
- comments within SQL remain owned by Run Query and retain SQL editor safety behavior;
- template comments remain owned by HTML/Write File content;
- comment-only/unsupported blocks should be attached to the nearest meaningful operation/scope when source semantics make that ownership unambiguous;
- otherwise retain as a read-only Note operation rather than silently dropping.

Do not turn ordinary comments into generated runtime calls.

## 9.10 API v5

Bump transport schema.

Normal document payload should expose:
- semantic operations/tree;
- bindings;
- file catalog/lifecycle;
- symbols;
- diagnostics.

Debug/compiler details:
- either omitted from normal payload;
- or placed behind an explicit debug endpoint/flag not consumed by normal workbench.

Remove normal dependence on:
- function_name;
- raw_code;
- class_name;
- module;
- method;
- low-level internal parameters.

Keep generated TypeScript contracts.

## 9.11 Safe HTML preview endpoint

Extract rendering so HtmlReport runtime and preview share the same renderer.

Preferred shape:
- pure/in-memory rendering method returns HTML;
- runtime layout writes it;
- preview endpoint invokes only report rendering logic.

Preview may:
- read sandboxed workspace CSV/CSS through an injected safe provider;
- resolve known static symbols;
- replay prior HTML-RUN/DEFER state necessary for the selected layout.

Preview must not:
- execute generated workflow Python;
- call external programs;
- run arbitrary SQL;
- send email;
- access paths outside workspace.

If runtime-only inputs are unavailable:
- render partial/approximate output;
- return structured warnings.

## 9.12 Email enabled semantics

Add enabled: bool = True as an authoritative operation field, or an equivalent first-class semantic binding with the same runtime result.

Preferred minimal runtime behavior:
- MailService.send(..., enabled=True);
- return immediately when false before credential lookup/network I/O;
- translated source omits enabled when default true;
- editor writes enabled=False only when disabled.

Bulk UI simply edits this field across selected operations.

## 9.13 Attachment upload security

Extend allowed workspace upload suffixes deliberately for approved images, for example:
- .png;
- .jpg;
- .jpeg;
- .gif;
- .webp if required.

Do not allow executable/script suffixes merely to support email attachments.

Browser native file picker:
- selects file locally;
- uploads to inputs/...;
- resulting workspace path becomes attachment binding.

No unrestricted host filesystem API.

---

# 10. Phase F — Pane composition

## 10.1 Workbench selection flow

Single selected operation id is the main semantic focus.

Navigation sources:
- Script Logic row;
- file Used by reference;
- Email entry;
- Globals reference;
- command palette.

All call one navigation action:
navigateToOperation(documentId, operationId).

The action:
- activates document if needed;
- expands ancestor scopes;
- selects operation;
- scrolls/reveals;
- optionally flashes contextual highlight.

Do not duplicate navigation logic per feature.

## 10.2 Script Logic pane

Header:
- Script Logic;
- search;
- expand/collapse controls only if useful.

Body:
- compact semantic tree.

Default row height should remain small enough to scan the entire script.

Expanded row:
- concise semantic summary/comments;
- not the full edit form.

## 10.3 Configuration pane

Header:
- Configuration;
- selected operation name.

Body:
- utility config;
- no giant nested card per field;
- one-column form;
- compact option rows.

Collapsible from boundary pull-tab.

## 10.4 Context pane

Header:
- Context;
- tabs File Flow / Email / Globals.

Collapsible from boundary pull-tab.

Each child feature owns its content, not pane sizing.

---

# 11. Phase G — Responsive workbench

## 11.1 Centralized layout model

One WorkbenchLayout owns:
- pane widths;
- manual collapsed state;
- automatic collapsed state;
- resize handles;
- responsive mode.

Do not scatter viewport checks through features.

Use ResizeObserver on the workbench container rather than many CSS breakpoints.

## 11.2 Starting width constants

Centralize and tune through browser tests:

- Script Logic minimum: 300 px;
- Configuration minimum: 360 px;
- Context minimum: 300 px;
- preferred desktop proportions: approximately 38% / 34% / 28%;
- splitter width: one small consistent token.

These are starting product constraints, not duplicated magic numbers.

## 11.3 Automatic collapse priority

When space becomes insufficient:

1. preserve Script Logic;
2. auto-collapse Configuration first;
3. if still insufficient, auto-collapse Context;
4. enter narrow single-pane navigation only when even Script Logic + rails cannot remain usable.

This preserves the user’s requested priority.

Use hysteresis:
- collapse threshold and restore threshold differ slightly;
- prevents resize flicker.

Manual collapse:
- persists while the tab/workbench is open;
- auto-restore must not override a manual collapse.

Auto collapse:
- may restore automatically once sufficient space returns.

## 11.4 Resize handles

Boundary separator:
- pointer drag;
- role=separator;
- keyboard arrow resizing;
- visible focus;
- min/max clamping;
- small center pull-tab for collapse/restore.

Dragging Script Logic boundary should allow it to become the dominant pane.

## 11.5 Narrow/mobile mode

When only one full pane can fit:
- Script Logic is default;
- compact top/bottom pane switcher opens Configuration or Context;
- selecting an operation from Email/Globals/File Flow can return/focus Script Logic or Configuration as appropriate;
- no horizontal page overflow;
- touch targets remain usable.

Do not copy the current 1199px all-or-nothing pane hiding logic.

## 11.6 Motion

Keep central motion tokens:
- fast feedback;
- content reveal;
- pane collapse.

Add:
- pane collapse/restore;
- tab transition;
- navigation highlight.

Semantic state changes happen immediately. Animation only reflects them.

Respect prefers-reduced-motion with no delayed behavior.

---

# 12. Proposed frontend directory structure

Target after incremental moves:

src/vg2c_ui/frontend/src/
  app/
    App.tsx
    SourceIntake.tsx
    FileTabs.tsx
    ThemeSelector.tsx
  api/
    api.ts
    contracts.generated.ts
  state/
    workspaceState.ts
    workspaceGuards.ts
    selectors.ts
  workbench/
    Workbench.tsx
    WorkbenchLayout.tsx
    PaneBoundary.tsx
    CommandPalette.tsx
    ChangeToolbar.tsx
    commands.ts
  script-logic/
    ScriptLogicPane.tsx
    OperationRow.tsx
    ControlBranch.tsx
  configuration/
    ConfigurationPane.tsx
    OperationConfiguration.tsx
  sql-editor/
    SqlEditor.tsx
    ColumnsSection.tsx
    FiltersSection.tsx
    JoinsSection.tsx
  html-preview/
    HtmlPreview.tsx
  context/
    ContextPane.tsx
    file-flow/
      FileFlowTab.tsx
      FileLifecycleRow.tsx
      RequiredInputs.tsx
    email/
      EmailTab.tsx
      EmailBulkToolbar.tsx
    globals/
      GlobalsTab.tsx
      SymbolReferences.tsx
  shared/
    controls/
      FileSelector.tsx
      SymbolSelector.tsx
      OptionalValue.tsx
      ReorderableList.tsx
      ValidationMessage.tsx
      ExpandableDetails.tsx
    navigation/
      operationNavigation.ts
    diagnostics/
      OperationDiagnostics.tsx
    motion.css
    tokens.css

Rules:

- do not create an index.ts barrel for every folder;
- expose only the components/types actually shared;
- keep feature-specific helpers inside the feature;
- no giant utils.ts;
- keep state reducer centralized until there is demonstrated pressure to split it;
- CSS may be colocated per feature or use one feature stylesheet; do not return to one monolithic stylesheet.

---

# 13. Script Logic design details

Collapsed examples:

▸ Run Query
▸ Copy File
▸ Check Row Count
▸ Condition
▸ Send Email

Expanded examples:

▾ Copy File
  source.csv → archive.csv

▾ Condition
  CONFIG ≤ 0

▾ For Each Chunk
  input.csv → chunk.csv · 100 rows

Comments:
- show only meaningful comments/notes associated with the operation;
- do not surface SQL optimizer comments or technical compiler markers as separate workflow steps.

Validation:
- warning/error dot;
- unsupported operation marker;
- unresolved binding count only if useful.

No:
- block numbers by default;
- Block 4-6;
- Operation 7;
- generated function name;
- raw code;
- ctx plumbing.

---

# 14. SQL configuration design

Tabs:

Columns | Filters | Joins

## Columns

At top:
- source/input file(s);
- output file.

Rows:
- drag handle;
- expression;
- alias;
- × remove.

Keyboard:
- focus handle;
- Space/Enter grab;
- Arrow Up/Down move;
- Space/Enter drop;
- Escape cancel.

Backend:
- reorder-selection(target_index).

## Filters

Each row:
- left expression;
- operator;
- right expression;
- connector for following row;
- × remove.

Keep backend operator validation.

## Joins

Each join:
- type;
- source;
- predicate rows;
- × remove.

Do not parse or infer columns in TypeScript.

## Pre/post query

Backend returns:
- prelude text/summary;
- editable statement;
- postlude text/summary.

Normal UI:
- compact collapsed Before query / After query only when content is meaningful;
- hide known routine boilerplate presentation;
- raw source available only Advanced.

## Complex SQL

For CTEs, UNION, comments in unsafe regions, unsupported expressions:
- preserve;
- show structured portions read-only where current parser already does;
- do not weaken safety to make UI look editable.

---

# 15. File Flow design

## 15.1 Resource identity

A FileResource must have a stable semantic reference independent of displayed path where possible.

Do not navigate by searching matching filename strings.

## 15.2 Lifecycle rows

Examples:

a.csv ─┐
       ├→ merged.csv
b.csv ─┘

source.csv → transformed.csv

source.csv → copy → archive.csv

temporary.csv → deleted

Only show:
- creation;
- transformation;
- copy;
- move;
- append;
- fan-in;
- delete.

## 15.3 Required Inputs

Separate compact group:

Required Inputs
- config.csv — used by Check Row Count
- image.png — attached by Send Email

This reconciles two requirements:
- ordinary reads are not File Flow lifecycle rows;
- manually entered/unproduced files remain visible.

## 15.4 Hover/focus usage

For each file:

Used by:
- Run Query;
- Check Row Count;
- Send Email.

Each is an operation reference.

## 15.5 Advanced statuses

Missing/possible/dynamic states can appear as concise warnings/tooltips only when they affect the user.

Hide by default:
- path_base;
- compiler reason text;
- source/control dependency wording;
- state IDs;
- removed-on-success diagnostics.

---

# 16. Required deletion/consolidation ledger

| Existing code/path | Why redundant/problematic | Replacement | Known consumers | Proof before deletion | Delete phase |
|---|---|---|---|---|---|
| src/vg2c/dataflow/projection.py | Parallel dirty-workspace projection duplicates workflow.py | authoritative workflow workspace projection | direct tests/dataflow/test_projection.py; no DocumentStore use observed | local repo-wide import grep; migrate all assertions; workspace dependency parity | final core consolidation |
| tests/dataflow/test_projection.py | Protects old projection specifically | equivalent tests against workflow projection | pytest | move behavior assertions first | with old projection |
| src/vg2c/dataflow/analyzer.py producer/consumer heuristics | Duplicates explicit effects and guesses external filenames | semantic operation file roles + FileEffect lifecycle | compile pipeline; workflow fallback; stage3 tests | cover /TABLE, SQL_Get_CSV_List, macro/loop/row-count and workspace dependencies; dispatch no longer needs AnalyzedProgram | after file semantic migration |
| src/vg2c/dataflow/models.py legacy ProducerRecord/ConsumerRecord/DataflowEdge | Exists for analyzer path | FileResource/FileEffect/operation refs | analyzer, dispatch type, tests | no imports after analyzer/dispatch migration | same |
| workflow.py analyzer fallback | Keeps two authorities alive | declared/control/composite semantic effects | workflow projection | effect parity fixtures | file semantic migration |
| macro_control emitted pass presentation | Duplicates scope/control meaning | structural operation | current serializer/synthetic behavior | all macro/if/loop fixtures show one semantic row | control migration |
| synthetic Compiler source/control operation rows | implementation artifact | control/composite operation projection | serializer | no unowned effects remain | API v5 |
| operationLabels.ts | frontend reconstructs names/summary from implementation metadata/effects | operation display_name + summary from core | ScriptTree, commands | command/search/navigation parity | Script Logic migration |
| OperationEditor Generated information section | leaks compiler internals | explicit debug-only surface if needed | current normal UI | all useful support diagnostics represented elsewhere | Configuration migration |
| DocumentView raw_code/function_name/class/module/method normal fields | transport leaks compiler structure | semantic operation contract | current frontend/tests | no normal frontend consumer; debug need decided | API v5 cleanup |
| direct FileEffectView renderer path | too low-level/noisy | lifecycle + file resources | FileFlowPane/FlowRow | lifecycle/required input/browser tests | File Flow migration |
| flowRenderers read/observe/unknown normal rows | violates compact scope | lifecycle-only renderers | File Flow | required inputs still visible | File Flow migration |
| SQL move-up/down buttons | unwanted UX and duplicate reorder action | ReorderableList + reorder-selection | StructuredSqlEditor | pointer + keyboard tests | SQL UI migration |
| move-selection API action/function if no remaining caller | redundant with target-index reorder | reorder-selection | SQL tests/current UI | repo-wide caller grep; all tests migrated | SQL cleanup |
| raw SQL normal disclosure | overwhelms normal user | Advanced-only view | StructuredSqlEditor | complex SQL still inspectable | SQL UI migration |
| old context-sidebar/editor-pane CSS | stale layout coexists with current workbench | feature CSS + WorkbenchLayout | stylesheet only; verify selectors | browser snapshots + repo selector grep | frontend cleanup |
| current @media 1199 one-pane switching | all panes disappear together; no independent collapse | WorkbenchLayout state machine | styles.css/App pane state | responsive Playwright matrix | layout migration |
| giant flat frontend src placement | unclear ownership | feature modules | imports across frontend | typecheck + browser tests after each move | incremental throughout |
| duplicate per-feature file dropdowns introduced during refactor | would create drift | shared FileSelector | future features | no bespoke copies | prevent rather than delete |

No deletion should happen merely because a path looks old. The implementation PR/commit deleting an item must include the parity evidence named above.

---

# 17. Dependency-ordered implementation sequence

The design above is bottom-up A→G. Actual code changes should be executed in the following dependency order so the frontend never invents semantics that the core has not provided.

## Step 0 — Local preflight and characterization baseline

Goal:
establish safe starting state and behavior snapshots.

Current problem:
this planning session cannot see local uncommitted work or run local tests.

Scope:
git; tests; representative fixtures.

Core changes:
none.

Frontend changes:
none.

Refactoring:
none.

Deletion:
none.

Dependencies:
none.

Validation:
- git pull;
- git branch --show-current;
- git rev-parse HEAD;
- git status --short;
- preserve/stash unrelated local work only according to user direction;
- uv/pytest baseline;
- npm check/contracts/typecheck/state;
- Playwright baseline;
- translate representative fixtures: script_short, actual_script, maxlidheight, html_test and email/macro/file fixtures.

Completion criteria:
- baseline commit/HEAD recorded;
- local changes understood;
- failing baseline tests documented before refactor.

Commit boundary:
no product commit unless characterization tests are added.

## Step 1 — Add characterization tests for semantic parity

Goal:
lock down behavior before replacing projections.

Current problem:
important behavior is distributed between resolver, analyzer, emitter and workflow.

Scope:
tests/resolver, tests/emitter, tests/dataflow, tests/ui.

Core changes:
none except test helpers.

Frontend changes:
none.

Refactoring:
none.

Deletion:
none.

Dependencies:
Step 0.

Validation:
add cases for:
- every IF operator;
- compound AND/OR;
- macro placeholder vs bare macro numeric behavior;
- START-MACRO;
- RUN-LOOP;
- ROWS-IN-FILE;
- SQL_Get_CSV_List file input;
- copy/move/delete/append;
- email attachments;
- HTML output/input;
- generated global sharing;
- comments in SQL;
- unsupported/JSL block.

Completion criteria:
current generated-script behavior is asserted before architectural changes.

Commit boundary:
test: characterize semantic workbench inputs

## Step 2 — Explicit semantic operation names and field presentation metadata

Goal:
make the core able to name operations and fields without TypeScript maps.

Current problem:
operation titles derive from utility class names.

Scope:
emitter/models.py; utility_metadata.py; @emittable usages.

Core changes:
- add display_name;
- minimal parameter presentation overrides;
- mark known runtime-only fields internal: HTML ctx, WaitFile interval, etc.;
- declare file/symbol roles where inferable.

Frontend changes:
none initially.

Refactoring:
keep schema inference.

Deletion:
none.

Dependencies:
Step 1.

Validation:
registry tests prove ordinary new utilities still appear without frontend changes.

Completion criteria:
Copy, Rename, Delete, Write File, Run Query, Wait for File, Send Email, Append all have correct semantic titles.

Commit boundary:
core: add semantic operation presentation metadata

## Step 3 — Introduce generalized EditableBinding and SemanticChange

Goal:
one edit contract for utility fields and future control fields.

Current problem:
ParameterChange can only address emitted parameters.

Scope:
editing.py; emitter models; sidecar/API models.

Core changes:
- binding view over emitted parameters;
- SemanticChange(binding_id,...);
- maintain utility parameter projection behavior unchanged.

Frontend changes:
transport compatibility layer only.

Refactoring:
ParameterChange internals can temporarily alias SemanticChange to keep tests small.

Deletion:
old naming only after all consumers migrate.

Dependencies:
Step 2.

Validation:
all existing edit/apply/reset/global tests unchanged semantically.

Completion criteria:
every old parameter edit works through binding id without generated-output differences.

Commit boundary:
core: generalize editor changes to semantic bindings

## Step 4 — First-class control operations and editable IF

Goal:
make IF/macro/loop visible and editable without frontend reimplementation.

Current problem:
resolver controls are outside emitted parameter metadata.

Scope:
operands; emitter walker/metadata; editing; workflow projection.

Core changes:
- ControlOperation model;
- stable IDs;
- Condition bindings;
- Macro/Loop bindings;
- record control source range;
- render edited IF expression using existing operator table and operand renderer;
- core symbol-resolution validation.

Frontend changes:
contract only; no full editor yet.

Refactoring:
stop relying on synthetic control steps.

Deletion:
prepare macro_control/pass/synthetic rows for later removal.

Dependencies:
Step 3.

Validation:
- all eight operators;
- literal vs macro;
- unresolved symbol error;
- compound IF;
- save/reopen sidecar;
- generated Python parity for no-edit case.

Completion criteria:
a core API consumer can change lhs, operator and rhs and receive valid regenerated Python.

Commit boundary:
core: expose editable control operations

## Step 5 — Promote ROWS-IN-FILE and source-composite semantics

Goal:
make Check Row Count file and target macro editable.

Current problem:
file input is nested inside generated implementation and analyzer heuristics.

Scope:
rows_in_file.py; PipelineContext or semantic projection; file effects; bindings.

Core changes:
- first-class Check Row Count operation;
- explicit file binding;
- explicit introduced-symbol binding;
- observe file effect.

Frontend changes:
none beyond contract.

Refactoring:
hide nested CsvIO.row_count/MacroState.set_named as implementation.

Deletion:
legacy rows-in-file analyzer special case once file projection migrates.

Dependencies:
Steps 3-4.

Validation:
generated behavior and macro value parity.

Completion criteria:
file and target macro are both editable bindings and projected semantically.

Commit boundary:
core: model check-row-count as one semantic operation

## Step 6 — Build symbol catalog/reference graph

Goal:
authoritative Globals view and condition selectors.

Current problem:
generated globals and runtime macros are not unified for UI navigation.

Scope:
emitter/globals.py; emit helper macro reference metadata; resolver controls; workflow/symbols.

Core changes:
- Symbol and SymbolReference;
- definitions/introduction refs;
- known/runtime/unresolved state;
- macro refs captured before Python rendering.

Frontend changes:
contract generation only.

Refactoring:
remove need to parse ctx.macro.named in UI.

Deletion:
none yet.

Dependencies:
Steps 4-5.

Validation:
SQL globals, shared email globals, ROWS-IN-FILE macro, START-MACRO row names where available, IF refs.

Completion criteria:
every projected symbol reference points to stable operation id.

Commit boundary:
core: add workflow symbol catalog

## Step 7 — Make file effects authoritative and remove analyzer supplementation

Goal:
one file semantic owner.

Current problem:
workflow.py still imports source/analyzer records to fill gaps.

Scope:
workflow.py; file_effects.py; SQL_Get_CSV_List parsing; control/composite ops; HTML/email.

Core changes:
explicit effects/resources for all known source constructs.

Frontend changes:
none.

Refactoring:
dispatch prepared to consume ResolvedProgram directly.

Deletion:
remove workflow analyzer fallback in this step only after tests pass.

Dependencies:
Steps 4-6.

Validation:
compare legacy analyzer expectations against new file resources on fixtures.

Completion criteria:
project_workflow never consults result.analyzed for files.

Commit boundary:
core: make declared semantic file effects authoritative

## Step 8 — Consolidate workflow package and delete legacy dataflow projection/analyzer

Goal:
remove duplicated architecture.

Current problem:
old analyzer/models/projection remain after migration.

Scope:
dataflow; workflow; dispatch; compilation; tests.

Core changes:
- dispatch from resolved program;
- CompilationResult updated;
- workflow package owns file effects/workspace projection.

Frontend changes:
transport consumers updated if necessary.

Refactoring:
move only modules whose final ownership is clear.

Deletion:
- dataflow/projection.py;
- old projection tests after migration;
- analyzer.py;
- legacy Producer/Consumer/DataflowEdge models if no consumers.

Dependencies:
Step 7.

Validation:
full compiler fixtures and workspace dependency tests.

Completion criteria:
one producer of file lifecycle/dependency truth.

Commit boundary:
refactor: remove legacy dataflow projection path

## Step 9 — API v5 semantic transport and sidecar v3

Goal:
give frontend only user-semantic data.

Current problem:
DocumentView exposes compiler internals; sidecar field name is parameter-specific.

Scope:
api/models.py; serialization.py; contracts.py; sidecar.py; DocumentStore.

Core changes:
semantic operation/file/symbol serialization.

Frontend changes:
generated TS contract update; compatibility compile only.

Refactoring:
thin serializer over core projection.

Deletion:
old normal transport fields once no consumers.

Dependencies:
Steps 3-8.

Validation:
contract sync; sidecar v2 read/upgrade; stale/hash behavior; no host paths.

Completion criteria:
normal DocumentView contains enough information for all planned panes without class/module/raw Python data.

Commit boundary:
api: publish semantic workbench contract v5

## Step 10 — Shared frontend controls

Goal:
build the bottom-level UI primitives.

Current problem:
ParameterField is generic but not semantic; optional fields are verbose; no shared file/symbol selector.

Scope:
shared/controls; existing ParameterField logic.

Core changes:
none.

Frontend changes:
FileSelector, SymbolSelector, OptionalValue, ReorderableList, ValidationMessage.

Refactoring:
reuse current recursive schema editor internals where useful.

Deletion:
old ParameterField only after all consumers move.

Dependencies:
Step 9.

Validation:
component/browser tests; keyboard; unresolved states.

Completion criteria:
controls work independent of utility names.

Commit boundary:
ui: add semantic shared configuration controls

## Step 11 — Script Logic module

Goal:
compact whole-script semantic view.

Current problem:
ScriptTree exposes block/scope/compiler presentation.

Scope:
script-logic; navigation; commands.

Core changes:
none.

Frontend changes:
new rows/tree using semantic operations.

Refactoring:
preserve current keyboard tree behavior where possible.

Deletion:
operationLabels.ts after command palette migrates; old ScriptTree after parity.

Dependencies:
Steps 9-10.

Validation:
nested macro/if/loop navigation; collapsed/expanded state; search.

Completion criteria:
default tree shows utility/control names and no Block X-Y/function names.

Commit boundary:
ui: replace script tree with semantic Script Logic

## Step 12 — Configuration module including IF/file selectors

Goal:
make selected operations editable from normalized bindings.

Current problem:
OperationEditor exposes raw parameters and generated details.

Scope:
configuration.

Core changes:
none except small missing metadata fixes discovered through real usage.

Frontend changes:
binding renderer; condition rows; file selectors; optional settings.

Refactoring:
move useful ParameterField schema code into shared controls.

Deletion:
Generated information section; old OperationEditor.

Dependencies:
Steps 9-11.

Validation:
every operation inventory row; invalid condition symbol; Check Row Count file edit.

Completion criteria:
no utility-specific switch except explicitly specialized capability components.

Commit boundary:
ui: build semantic configuration pane

## Step 13 — SQL editor UX

Goal:
Columns/Filters/Joins tabs, no frontend parser, accessible reorder.

Current problem:
stacked sections, raw SQL normal disclosure, up/down buttons.

Scope:
sql-editor frontend and small API additions for pre/post slices if needed.

Core changes:
- expose pre/post statement slices;
- preserve current transforms;
- optionally retire move-selection.

Frontend changes:
tabs; ReorderableList; advanced raw source only.

Refactoring:
reuse current API calls/state.

Deletion:
move buttons; normal raw SQL; move-selection if unneeded.

Dependencies:
Steps 10-12.

Validation:
all SQL transform tests; drag + keyboard reorder; complex SQL read-only.

Completion criteria:
SQLite and external SQL share the same structured UX where supported.

Commit boundary:
ui: simplify structured SQL configuration

## Step 14 — Safe HTML preview

Goal:
real renderer preview without workflow execution.

Current problem:
no safe preview API; runtime layout mixes render/write.

Scope:
HtmlReport; preview service/API; html-preview frontend.

Core changes:
extract shared renderer and safe data provider.

Frontend changes:
preview pane/section with loading/error/approximate states.

Refactoring:
runtime layout calls extracted renderer.

Deletion:
duplicated preview renderer prohibited.

Dependencies:
Steps 9-12.

Validation:
representative html_test fixtures; missing CSV/CSS; path escape rejection; no external execution.

Completion criteria:
preview output comes from same rendering semantics as runtime.

Commit boundary:
feat: add sandboxed HTML report preview

## Step 15 — Context shell and File Flow

Goal:
compact lifecycle context with stable navigation.

Current problem:
low-level effects are directly rendered.

Scope:
context; file-flow.

Core changes:
none if API v5 complete.

Frontend changes:
ContextPane tabs; lifecycle renderers; Required Inputs; Used by navigation.

Refactoring:
reuse CSV preview endpoint.

Deletion:
old FileFlowPane/FlowRow renderers and noisy effect metadata UI.

Dependencies:
Steps 9,11.

Validation:
fan-in, copy, move, append, delete; external manual input; file→operation navigation.

Completion criteria:
ordinary reads are absent from lifecycle but external requirements remain visible.

Commit boundary:
ui: replace file effects with compact File Flow

## Step 16 — Email tab and attachment picker

Goal:
central overview and bulk control over actual email operations.

Current problem:
no aggregate email context; no enable flag; attachment picker not integrated.

Scope:
MailService; workspace upload allowlist; context/email.

Core changes:
enabled binding; approved image uploads.

Frontend changes:
email list; multi-select; bulk toolbar; attachment upload.

Refactoring:
bulk actions issue normal binding edits.

Deletion:
no parallel email state/model.

Dependencies:
Steps 9-12,15.

Validation:
enable/disable individual/all; mixed bulk values; image attachment upload; save/reopen; disabled send does not hit credentials/network.

Completion criteria:
Email tab is a projection of Send Email operations only.

Commit boundary:
feat: add semantic email context and bulk editing

## Step 17 — Globals tab

Goal:
symbol discovery/reference navigation.

Current problem:
globals/macros leak as implementation expressions or are not discoverable.

Scope:
context/globals.

Core changes:
none if symbol API complete.

Frontend changes:
symbol list, details, references, navigation.

Refactoring:
condition selector consumes same symbol catalog.

Deletion:
any frontend macro-expression parsing.

Dependencies:
Steps 6,9,11.

Validation:
known global; runtime macro; references across steps; unresolved state.

Completion criteria:
normal UI never needs ctx.macro.named text.

Commit boundary:
ui: add Globals context tab

## Step 18 — Resizable/collapsible responsive WorkbenchLayout

Goal:
three-pane desktop with Script Logic priority.

Current problem:
fixed grid + one breakpoint hides panes together.

Scope:
workbench layout; shared motion; CSS.

Core changes:
none.

Frontend changes:
resize handles; manual/auto collapse; narrow mode.

Refactoring:
centralize pane state.

Deletion:
old pane-tabs breakpoint logic and stale context-sidebar/editor-pane layout CSS.

Dependencies:
feature panes stable through Steps 11-17.

Validation:
viewport matrix; drag; keyboard separator; sequential auto-collapse; restore; reduced motion; 200% zoom.

Completion criteria:
no horizontal overflow and collapse priority matches specification.

Commit boundary:
ui: add adaptive resizable workbench layout

## Step 19 — Frontend module cleanup

Goal:
finish migration with one architecture.

Current problem:
temporary moved/legacy components may coexist.

Scope:
all frontend imports/styles.

Core changes:
none.

Frontend changes:
final directories; colocated feature styles; simplify exports.

Refactoring:
move components only after ownership is established.

Deletion:
old components/styles/helpers with zero consumers.

Dependencies:
Steps 10-18.

Validation:
typecheck; state tests; Playwright; repo-wide import/selector grep.

Completion criteria:
no old/new duplicate pane/editor implementation remains.

Commit boundary:
refactor: remove legacy workbench frontend paths

## Step 20 — Full parity and architecture review

Goal:
prove product and generated-script behavior.

Current problem:
large refactor crosses compiler/UI boundaries.

Scope:
all tests/fixtures.

Core changes:
bug fixes only.

Frontend changes:
bug fixes only.

Refactoring:
remove any now-obvious one-consumer abstractions.

Deletion:
final ledger items only with proof.

Dependencies:
all prior.

Validation:
section 18 testing strategy.

Completion criteria:
final Definition of Done.

Commit boundary:
test/refactor: close semantic workbench migration

---

# 18. Testing strategy

## 18.1 Core characterization

Every user operation:
- projection name;
- editable bindings;
- internal fields absent;
- defaults;
- validation;
- generated parity.

## 18.2 Conditions

At minimum:
- EQS;
- NES;
- LE;
- LT;
- GE;
- GT;
- EQ;
- NE;
- bare numeric macro;
- VAR(NAME);
- named placeholder;
- literal;
- empty operand;
- AND second predicate;
- OR second predicate;
- unresolved typed symbol;
- reset;
- save/reopen.

Assert both:
- semantic model;
- generated Python behavior.

## 18.3 Globals/macros

- generated SQL globals;
- shared global reused across steps;
- conflicting generated globals;
- email shared to/subject globals;
- ROWS-IN-FILE macro introduction;
- macro-row symbol;
- reference navigation IDs;
- runtime value represented unknown.

## 18.4 Files

- query input/output;
- Check Row Count editable file;
- copy;
- move;
- delete;
- append;
- merge/fan-in;
- loop chunk;
- macro input;
- SQL_Get_CSV_List;
- HTML input/output;
- email attachment;
- wait file;
- manual external input;
- missing generated input;
- dynamic path;
- file→operation refs;
- output edit updates resource projection.

## 18.5 SQL

- columns add/edit/remove;
- reorder by target index;
- filters;
- joins;
- join predicates;
- source edits;
- comments preserved/read-only;
- CTE/UNION safe fallback;
- one editable SELECT with pre/post non-SELECT statements;
- SQLite table bindings;
- external SQL reader parity;
- no frontend SQL parsing tests because frontend should not parse.

## 18.6 Email

- default enabled;
- disabled runtime returns before keyring/network;
- individual toggle;
- select multiple;
- enable/disable all;
- bulk compatible field;
- mixed value state;
- attachment known file;
- uploaded image;
- path escape blocked;
- save/reopen.

## 18.7 HTML preview

- exact fixture where all files available;
- missing input;
- runtime macro unresolved;
- CSS;
- deferred section;
- layout;
- preview never writes output;
- preview cannot escape workspace;
- no email/SQL/external execution.

## 18.8 Frontend behavior

- collapsed operation rows;
- expanded summaries;
- selection;
- file reference navigation;
- global reference navigation;
- email navigation;
- condition invalid state;
- file manual/selector;
- optional control;
- drag reorder;
- keyboard reorder;
- keyboard tree;
- undo/redo;
- preview/apply;
- save/reopen;
- conflict/reload;
- stale request protection.

## 18.9 Workbench layout

Viewport/container matrix:
- wide desktop: all 3 panes;
- first squeeze: Configuration auto-collapsed;
- narrower: Context auto-collapsed;
- narrow mode: one full pane;
- manual collapse persists;
- auto collapse restores;
- manual collapse does not auto-restore;
- resize drag clamps min widths;
- separator keyboard;
- no horizontal document overflow;
- 200% zoom.

## 18.10 Accessibility

- semantic labels;
- focus visible;
- tab order;
- tree keyboard;
- drag handle keyboard;
- live reorder announcement;
- tabs keyboard;
- pane separator role/values;
- reduced motion;
- no essential hover-only action;
- × removal appears on focus as well as hover.

## 18.11 Generated-script parity

For representative existing fixtures:
- unchanged source + no edits produces byte-equivalent output wherever architecture change does not intentionally alter runtime expression shape;
- where generated structure intentionally changes, execute or AST/semantic parity tests prove equivalent behavior;
- no vg2c imports leak into generated scripts;
- utility embedding remains minimal;
- reader selection unchanged;
- email, HTML, macros, SQL remain executable.

Do not update golden expectations merely to silence regressions. Every intentional difference needs a documented reason.

---

# 19. Current significant risks and unresolved questions

## 19.1 Local-only work

Highest immediate risk.

The remote plan cannot know local modified/untracked files. Implementation must not begin until local status is inspected.

## 19.2 Condition runtime helper mismatch

Current MacroState.eval_condition is not equivalent to the resolver IF operator semantics.

Do not “simplify” IF emission by routing through it without a parity implementation.

## 19.3 Runtime macro values

Many macro values are inherently unknown until execution.

UI must show symbolic/runtime status, not invented values.

## 19.4 Unknown external process effects

A .csv-looking argument does not prove read/write behavior.

The new model should be less speculative than the old analyzer.

## 19.5 Unsupported/JSL blocks

Existing fixtures contain JSL-style source that currently falls through unknown handling.

Do not claim it is editable. Present it honestly as an advanced/read-only operation unless a dedicated translator exists.

## 19.6 HTML preview exactness

HTML rendering can depend on earlier HTML state, workspace files and runtime macros.

Preview must return an exact/approximate flag and warnings.

## 19.7 File identity across edits

Path cannot be the sole stable ID because rename/output edits change it.

Resource identity should derive from semantic lineage/effect endpoint where possible.

## 19.8 Source vs generated-output editing

Current safety model intentionally edits generated Python through sidecar rather than rewriting VG2 source.

This plan preserves that invariant. Source rewriting would be a separate product decision and should not be smuggled into this refactor.

## 19.9 Drag-and-drop dependency

Accessible DnD is deceptively complex.

Choose one shared implementation. If a focused mature sortable dependency is accepted, use it rather than multiple bespoke drag implementations. Do not introduce a general component framework for this.

---

# 20. Final architecture challenge review

Before implementation approval, challenge each proposal:

Does every new module need to exist?
- workflow submodules are justified only because file projection, symbols and workspace dependency logic have independent responsibilities.
- keep editing.py as one module until size/ownership actually requires a package.
- no separate email domain package in core.

Is semantic ownership duplicated?
- no frontend utility registry;
- no frontend condition evaluator;
- no frontend file-lineage inference;
- no second SQL parser;
- no second email configuration model.

Can planned abstractions merge?
- file resources + effect usages share one workflow projection;
- generated globals + runtime macros share one UI symbol catalog but retain internal kind;
- file picker reused everywhere;
- operation navigation reused everywhere.

Are internals leaking?
- normal API should not require ctx, reader class, utility module/class, generated function name, source offsets or raw Python.

Do old/new paths coexist?
- only temporarily by explicit migration step;
- each temporary path has a deletion ledger entry.

Is a generic abstraction introduced for one consumer?
- no.
- HTML preview stays feature-specific.
- SQL editor stays feature-specific.
- ContextPane is shared because three tabs genuinely share placement/navigation.

Can more code be deleted?
- yes after effect parity: legacy dataflow analyzer/projection;
- old frontend label/render paths;
- stale CSS;
- move-selection wrapper if unused.

Can a new programmer locate ownership?
- source meaning: resolver/utilities;
- editable contract: workflow/editing;
- SQL semantics: sql_editor;
- file semantics: workflow/file_effects;
- symbols: workflow/symbols;
- API transport: vg2c_ui/api;
- feature UI: matching frontend feature folder.

Can a non-technical user understand the result?
- Script Logic uses what happens;
- Configuration uses what can I change;
- File Flow uses what files change;
- Globals uses what values are referenced;
- Email uses what messages will be sent;
- implementation Python is progressive/debug-only.

---

# 21. Final Definition of Done

The refactor is complete only when all of the following are true:

1. Script Logic shows one coherent semantic operation/control tree.
2. Every user-facing operation in the inventory has an explicit semantic name.
3. Default rows are compact and do not show Block X-Y/function names.
4. IF conditions are editable through global/symbol + operator + value controls.
5. IF comparison semantics remain core-owned and parity-tested.
6. Unresolved typed symbols are retained and clearly invalid.
7. For Each Macro Row and macro control are not duplicated.
8. Check Row Count file is editable and selectable from known files.
9. File selectors support known/generated choice plus manual input.
10. Manual external inputs appear under Required Inputs.
11. SQL normal UI is Columns / Filters / Joins.
12. SQL row ordering supports drag and keyboard.
13. Raw SQL is advanced-only.
14. Pre/post query structure is preserved.
15. Standard internal SQL boilerplate is not normal UI noise.
16. HTML template preview uses actual safe rendering semantics.
17. File Flow shows lifecycle changes, not every read.
18. File hover/focus shows stable Used by operation references.
19. Email tab lists all email operations.
20. Email enable/disable individual and bulk uses the same underlying operation edits.
21. Bulk field edits do not create a parallel email model.
22. Email attachments support known files, manual paths, and sandboxed browser upload including approved images.
23. Globals lists generated globals and meaningful macros with references.
24. Runtime-only macro values are shown symbolically/unknown, not fabricated.
25. Configuration and Context panes are independently collapsible.
26. Pane widths are resizable with pointer and keyboard.
27. Automatic collapse prioritizes Script Logic, then collapses Configuration, then Context.
28. Narrow mode remains usable without horizontal page overflow.
29. Motion is centralized and reduced-motion compliant.
30. Frontend source is feature-organized with no giant utility module.
31. Normal API/UI no longer depends on compiler implementation metadata.
32. One authoritative file/workflow projection remains.
33. Old dataflow projection/analyzer is deleted if all parity criteria are met.
34. Old workbench/editor/CSS paths are deleted.
35. Sidecar migration preserves existing saved edits safely.
36. Preview/apply/reopen behavior remains revision/hash safe.
37. Generated scripts retain behavioral parity.
38. Full Python, TypeScript, state and Playwright suites pass.
39. No tests are weakened solely to accommodate the refactor.
40. The deletion ledger has no unresolved old/new duplicate architecture entries.

