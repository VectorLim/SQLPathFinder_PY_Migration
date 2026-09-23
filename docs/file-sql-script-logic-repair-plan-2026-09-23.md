# File Selection, Structured SQL, and Script Logic Repair Plan

**Planning baseline:** remote `main` at `568e2e52ffb0a4e03f46e22f61371592f123443a`  
**Planning branch:** `planning/file-sql-script-logic-2026-09-23`  
**Scope:** planning only; no production implementation in this document

## 1. Baseline and verification status

### Repository state that is verified

The remote default branch is `main`, and its current HEAD is still:

```text
568e2e52ffb0a4e03f46e22f61371592f123443a
```

That commit is the current remote `main` head at the time of this planning review.

The current GitHub Actions workflow `.github/workflows/frontend-workbench-validation.yml` is configured to run only on pushes to `improv-frontend`, not on `main`. Therefore there is no current CI run that proves the present `main` commit passes its backend, frontend, build, and Playwright suites. The latest historical successful run visible on `improv-frontend` predates the current `main` merge and is not accepted as the current baseline.

### Environment limitation in this planning session

This session can inspect and write the GitHub repository, but it does **not** have access to the local Windows checkout at:

```text
C:\Project\SQLPathFinder_PY_Migration
```

and it does not expose a runnable desktop/browser session for that checkout.

Consequently, this planning session cannot truthfully claim to have:

- run `git pull` in the Windows checkout;
- inspected its local uncommitted working tree;
- executed the current Python/TypeScript/Playwright test suites;
- launched the current application and reproduced the defects interactively.

The implementation session must therefore begin with the mandatory Gate 0 below and must not treat the code-level findings in this document as a substitute for live reproduction.

## 2. Mandatory Gate 0 — reproduce and establish the real baseline before editing

Before touching production code, the implementation session must perform this exact gate locally.

### Git state

From `C:\Project\SQLPathFinder_PY_Migration`:

```bash
git status --short --branch
git pull --ff-only
git branch --show-current
git rev-parse HEAD
git status --short
```

Expected remote baseline at planning time is `main` / `568e2e52ffb0a4e03f46e22f61371592f123443a`, but the implementation session must accept a newer pulled HEAD if one exists and re-check affected code before proceeding.

Stop implementation if the working tree contains unrelated local changes that would make the baseline ambiguous.

### Test baseline

Run the targeted backend tests first:

```bash
pytest -q   tests/dataflow/test_file_effects.py   tests/sql_editor/test_capability.py   tests/sql_editor/test_operations.py   tests/sql_editor/test_sql_editor.py   tests/ui/test_document_store.py   tests/ui/test_workspace_sessions.py
```

Then run the complete backend suite:

```bash
pytest -q
```

From `src/vg2c_ui/frontend`:

```bash
npm ci
npm test
npm run build
npm run test:browser
```

Record pass/fail counts and preserve Playwright traces/screenshots for any existing failures. Do not weaken or skip an existing test to obtain a green baseline.

### Browser reproduction before code changes

Use the actual running application, not component assumptions.

Record the DOM behavior and screenshots for:

1. input file selector;
2. output file selector;
3. prior generated file offered to a later operation;
4. future generated file excluded from an earlier operation;
5. structured SQL with ordinary editable SQL;
6. structured SQL containing `SQL_Get_CSV_List`;
7. nested Script Logic mouse expansion;
8. nested Script Logic keyboard expansion;
9. current Expand All / Collapse All controls;
10. vertical positions of at least two following sibling operations before collapse, after collapse, and after re-expand.

These observations become characterization evidence for the changes below.

---

## 3. Current ownership map

The existing ownership is mostly sound and should be preserved.

### File availability

```text
utility file-effect declarations
    ↓
bind_file_effects()
    ↓
project_document()
    ↓
order_file_effects()
    ↓
FileEffect.available_before
    ↓
DocumentStore + workspace inventory
    ↓
file_choices_by_operation()
    ↓
serialization.semantic_operation_views()
    ↓
SemanticBindingView.file_choices
    ↓
frontend input selector
```

Important ownership boundary:

- `vg2c.dataflow.file_effects` owns execution-order/lifecycle semantics.
- `EffectiveDocument` owns the effective semantic workflow/effects.
- `DocumentStore` owns the user-specific server workspace boundary and inventory.
- `file_choices_by_operation()` combines compiler lifecycle facts with that physical inventory.
- TypeScript must consume the resulting choices and must not reimplement execution ordering.

Do **not** move physical workspace inventory into `EffectiveDocument`; that would leak server/session concerns into the compiler/core.

### Structured SQL

```text
resolved/dispatched SQL
    ↓
SqliteEngine SQL emission
    ↓
ctx.run_query(sql=...)
    ↓
structured_sql_model()
    ↓
existing parser + SqlEditableModel
    ↓
existing SQL operation classes
    ↓
SemanticChange
    ↓
project_changes()
    ↓
generated Python
```

The parser and SQL operation classes are already the correct structural model. The defect is in edit/projection ownership around a SQL argument whose generated Python representation is dynamic.

### Script Logic

```text
TabState.expandedScopeIds
    ↓
workspaceReducer toggle/set-all actions
    ↓
SemanticScriptTree
    ↓
natural DOM flow / CSS
```

Expansion state belongs in workspace state. Rendering and accessible pointer/keyboard behavior belong in the Script Logic feature. The command palette and toolbar should derive from the same expansion helper/state.

---

## 4. Verified code-level failure modes and root causes

These are verified from the current `main` implementation. Live visual impact still must be confirmed in Gate 0.

### 4.1 Output bindings are rendered through an input-oriented existing-file selector

Relevant files:

- `src/vg2c_ui/frontend/src/features/configuration/SemanticBindingField.tsx`
- `src/vg2c_ui/frontend/src/features/configuration/SemanticControls.tsx`
- `src/vg2c_ui/api/serialization.py`

Current code treats either capability as a generic file binding:

```text
file-input OR file-output
    → FileSelector / FileListSelector
```

`FileSelector` always renders:

```text
Enter server workspace path…
```

as a selector option and then conditionally renders a manual-path text box.

Meanwhile serialization only supplies `file_choices` to bindings carrying `file-input`, which is correct backend behavior. Therefore an output binding receives no choices but is still rendered through the selector, forcing the output UI into a meaningless existing-file/manual-mode chooser.

**Root cause:** frontend control selection collapses two distinct semantic roles into one UI component.

### 4.2 Generated-file ordering logic already exists; the reported missing prior output is not yet proven to be a core ordering bug

Relevant files:

- `src/vg2c/dataflow/file_effects.py`
- `src/vg2c_ui/services/file_choices.py`
- `tests/dataflow/test_file_effects.py`

The core already calculates `FileEffect.available_before` while walking ordered effects and tracks guaranteed / possible / missing resources.

Existing tests already cover, in isolation:

- generated later is not offered earlier;
- generated earlier is offered later;
- deleted output is not offered after deletion;
- conditional output remains possible and is not offered as guaranteed;
- unrelated physical uploads remain available;
- paths outside the workspace are not exposed.

Therefore do **not** rewrite `order_file_effects()` or duplicate ordering in TypeScript simply because the browser is missing a choice.

The missing choice must first be characterized through:

```text
project_document()
→ EffectiveDocument.effects
→ file_choices_by_operation()
→ DocumentStore.open_document()
→ semantic_operation_views()
→ SemanticBindingView.file_choices
→ browser
```

A targeted full-stack regression should be added before changing core logic.

### 4.3 There is a separate confirmed stale-choice gap for unsaved draft output edits

Relevant files:

- `src/vg2c_ui/frontend/src/workspace/useWorkspace.ts`
- `src/vg2c_ui/services/document_store.py`
- `src/vg2c_ui/api/models.py`

`useWorkspace.edit()` only updates local draft values.

`refreshFileChoices()` reopens the persisted document and is currently used after upload; it does not include the active draft changes.

The existing workspace projection does include current draft changes and recomputes `EffectiveDocument`, but `ProjectedDocumentView` does not currently carry per-operation file choices back to the frontend.

Therefore, if Step A's output path is changed in an unsaved draft, Step B's normal file selector cannot immediately receive the corresponding revised backend availability.

This is a real ownership gap, but it should only be expanded in scope if Gate 0 confirms it is part of the reported workflow. The minimal solution is to extend the already-existing backend projection path rather than invent a second frontend availability engine.

### 4.4 Dynamic emitted SQL is incorrectly equated with structurally read-only SQL

Relevant files/symbols:

- `vg2c/utilities/sqlite_engine.py::SqliteEngine._extract_sql_text`
- `vg2c/emitter/models.py::_dynamic_metadata`
- `vg2c/semantics.py::_build_semantics`
- `vg2c/sql_editor/capability.py::structured_sql_model`
- `vg2c/sql_editor/capability.py::apply_sql_action`
- `vg2c/editing.py::project_changes`

When SQL contains runtime substitutions such as `SQL_Get_CSV_List` or compiler-extracted SQL globals, `SqliteEngine._extract_sql_text()` emits a Python expression composed from literals plus runtime calls.

That `CodeExpr` intentionally has no direct literal value. The emitter consequently marks the generated Python argument:

```text
editable = False
read_only_reason = "Dynamic Python expressions are read-only"
```

That statement is valid for **raw Python parameter editing**, but the SQL capability layer currently treats it as a statement about SQL semantics.

`structured_sql_model()` detects the non-editable emitted parameter, parses the underlying SQL, then deliberately replaces the parser's real capabilities with:

```python
SqlEditCapabilities(False, False, False)
```

and returns:

```text
SQL structure is read-only; file-list inputs can be changed.
```

`apply_sql_action()` also requires the emitted parameter to be editable for every action except `update-file-list`.

**Root cause:** representational editability of generated Python has become the gate for semantic SQL editability.

### 4.5 The frontend repeats the same SQL editability conflation

Relevant file:

- `src/vg2c_ui/frontend/src/features/configuration/SemanticBindingField.tsx`

The structured SQL editor receives:

```tsx
readOnly={readOnly || !binding.editable}
```

Thus even if the SQL parser can safely expose Columns / Filters / Joins, the UI disables structural actions whenever the generated Python argument itself is not directly editable.

The backend `SqlModelView.capabilities` should be authoritative for SQL structural actions. The frontend must not infer SQL domain capability from raw-parameter editability.

### 4.6 The existing SQL parser/operations should be retained

Relevant files:

- `vg2c/sql_editor/parser.py`
- `vg2c/sql_editor/schema.py`
- `vg2c/sql_editor/operations/`
- `vg2c/sql_editor/models.py`

The parser already identifies which SELECT-list, WHERE, and JOIN structures are safely editable and already retains read-only reasons for unsupported structures such as ambiguous/multiple SELECTs, CTEs, set operators, comments, and complex unaliased expressions.

The operation classes already perform structural edits and return a reparsed model.

There is no justification for introducing a second SQL AST/model in either backend or frontend.

### 4.7 Nested mouse expansion is broken by the rendered control structure

Relevant file:

- `src/vg2c_ui/frontend/src/features/script-logic/SemanticScriptTree.tsx`

The entire row is one selection button with `role="treeitem"`.

The chevron is only:

```tsx
<span className="tree-toggle" aria-hidden="true">...</span>
```

It is not a button and has no toggle handler. Clicking it bubbles to the row's `onClick`, which only selects the operation.

Keyboard ArrowRight/ArrowLeft calls `onToggle()`, explaining why keyboard expansion can work while pointer expansion cannot.

**Root cause:** expansion has keyboard logic but no actual pointer control.

### 4.8 The collapse DOM does not match the CSS mechanism

Relevant files:

- `SemanticScriptTree.tsx`
- `scriptLogic.css`

CSS defines:

```css
.tree-branch { grid-template-rows: 0fr; ... }
.tree-branch.is-open { grid-template-rows: 1fr; ... }
.tree-branch__inner { min-height: 0; overflow: hidden; }
```

but the rendered tree is:

```tsx
<div className="tree-branch">
  <ul role="group">...</ul>
</div>
```

There is no `.tree-branch__inner` element. The nested `ul` also does not receive the existing `.tree-children` class.

The CSS therefore describes a collapse structure that the component no longer renders.

The clean repair is not to add more sizing tricks. Prefer conditional subtree rendering in natural document flow unless live reproduction demonstrates that an animated height transition is an explicit product requirement.

### 4.9 Expand/collapse-all state calculation is duplicated

Relevant files:

- `workspace/state.ts`
- `app/App.tsx`
- `workbench/commands.ts`

"Which operations are meaningful expandable scopes?" is recomputed independently in:

- reducer `set-all-scopes`;
- tab creation;
- toolbar enablement;
- command palette enablement.

The app renders two independent toolbar buttons and commands expose two independent actions.

**Root cause:** a simple piece of tree-domain state has multiple calculations and commands rather than one derived state/action.

---

## 5. KEEP / REFACTOR / MERGE / REPLACE / DELETE classification

| Classification | Code / responsibility | Rationale |
| --- | --- | --- |
| **KEEP** | `FileEffect`, `FileAvailability`, `order_file_effects()`, resource identity | This is already the correct backend execution-order authority. |
| **KEEP** | `EffectiveDocument` as compiler-owned effective semantic projection | Correct ownership; do not inject physical workspace inventory into core. |
| **KEEP** | `DocumentStore` as workspace/session boundary | Correct place to combine semantic projection with server workspace state. |
| **KEEP** | SQL parser, `SqlEditableModel`, schema enrichment, operation classes/registry | Already separates safe structured operations from unsupported SQL. |
| **KEEP** | `TabState.expandedScopeIds` ownership | Expansion is UI/workspace state and belongs here. |
| **REFACTOR** | file control dispatch in `SemanticBindingField` | Distinguish input selection from output path editing explicitly. |
| **REFACTOR** | structured SQL projection in `editing.py` / SQL capability | Allow semantic SQL editability independent of raw emitted-expression editability. |
| **REFACTOR** | SQL emission helper boundary | Reuse one renderer for original compile and edited structured SQL so runtime substitutions cannot drift. |
| **REFACTOR** | `SemanticScriptTree` row/toggle DOM | Give expansion its own icon-only control without breaking tree keyboard behavior. |
| **MERGE** | meaningful-scope / all-expanded calculations | One pure helper in existing workspace state module. |
| **MERGE** | SQL structural + file-list projection onto the SQL emitted argument where possible | Avoid overlapping whole-argument and nested replacements. |
| **REPLACE** | generic output `FileSelector` usage | Replace with a simple output-path editor. |
| **REPLACE** | two global toolbar expand/collapse buttons | One state-driven icon-only toggle. |
| **REPLACE** | two command-palette expand/collapse commands | One state-driven command using the same derived state. |
| **DELETE** | forced `SqlEditCapabilities(False, False, False)` compatibility path for otherwise safely parsed dynamic SQL | It encodes the incorrect ownership rule. Keep parser-derived read-only behavior for genuinely unsupported SQL. |
| **DELETE** | stale tree collapse CSS if conditional rendering is adopted | Remove `.tree-branch` 0fr machinery / unused `.tree-branch__inner`. |
| **DELETE** | duplicate nested `some()` calculations for expandable scopes | Replaced by the shared pure helper. |
| **DELETE/SIMPLIFY** | output-side manual-selector sentinel behavior | Output path is not a selection mode. |

---

## 6. Recommended ownership changes

### 6.1 File controls

No backend architecture change is needed merely to distinguish input from output.

Use capabilities explicitly:

```text
file-input + scalar
    → input file selector
       known backend choices + manual workspace path

file-input + list
    → input file-list selector
       known backend choices + upload/manual behavior as currently supported

file-output + scalar
    → output path editor
       text/path field only

file-output + list (only if an actual utility exposes it)
    → generic output path-list editor
       no input inventory chooser semantics
```

Prefer renaming `FileSelector` / `FileListSelector` to make their input role explicit if that improves call-site clarity. Do not create a selector service or hook.

### 6.2 File availability

Keep the current backend lifecycle ownership.

Add characterization at the integration boundary before changing logic.

Only if the failing integration test shows an actual defect should `file_choices_by_operation()` or effect projection be changed.

For live draft-dependent choices, if Gate 0 confirms the need:

- reuse the existing `project_workspace()` request because it already carries current draft changes;
- have its backend projection include per-document/per-operation file choices computed by the same `file_choices_by_operation()`;
- merge those backend-provided choices into frontend binding views/state;
- do not calculate execution order or file availability in TypeScript.

This is preferable to a new endpoint or a new frontend manager.

### 6.3 Structured SQL

Adopt one core rule:

> A SQL binding can be structurally editable even when the generated Python expression that implements it is not directly literal-editable.

Keep the existing binding identity and existing `SemanticChange` pipeline if feasible.

The recommended internal flow is:

```text
authoritative logical SQL for the binding
    ↓
parse_sql()
    ↓
existing SQL operation class
    ↓
updated logical SQL
    ↓
core structured-SQL projection
    ↓
reuse the same SQL emission renderer used by SqliteEngine
    ↓
one valid replacement for ctx.run_query(sql=...)
```

The structured-SQL projection must preserve/rebuild:

- `SQL_Get_CSV_List` runtime calls;
- current file-list binding path overrides;
- extracted SQL globals and their generated references;
- macro/symbol behavior already supported by emission;
- unchanged query text outside the structural mutation.

Do **not** solve this by setting a dynamic `CodeExpr` to ordinary `editable=True` and serializing the transformed SQL with `repr()`; that would silently discard runtime substitutions.

Do **not** make the frontend patch raw SQL or generated Python.

#### Preferred implementation boundary

Refactor the SQL-expression-building portion of `SqliteEngine._extract_sql_text()` into a reusable core function/method that can render a supplied logical SQL string using the same runtime substitution semantics as normal emission.

Then make `project_changes()` recognize the existing `structured-sql` capability and route that binding through this renderer rather than through ordinary `_serialize_parameter()`.

Avoid a generic plugin/strategy framework unless a second real semantic capability requires the same mechanism during implementation. One focused structured-SQL projection path is less abstract and easier to maintain.

#### File-list + structural edit interaction

Today file-list edits are projected as nested literal-range replacements while structural SQL edits replace the SQL parameter as a whole.

After structural editing is enabled for dynamic SQL, those replacements can overlap.

Prefer one SQL-argument projection per affected invocation:

1. obtain effective logical SQL;
2. apply structural SQL override if present;
3. apply effective `sql-file-list` path values to the proven calls;
4. render the final Python SQL expression once.

This should allow deletion of SQL-specific nested-replacement special cases that become redundant.

### 6.4 Frontend SQL editor

`StructuredSqlEditor` should use:

- document/session read-only state for global disablement;
- `SqlModelView.capabilities.selected`;
- `SqlModelView.capabilities.filters`;
- `SqlModelView.capabilities.joins`;
- row/item-level `editable` fields;
- backend-provided read-only reasons.

It should **not** use `SemanticBindingView.editable` as the gate for structured actions.

The binding's raw editability may still control any direct raw-value editor/reset semantics, but the structured UI is a separate semantic capability.

### 6.5 Script Logic expansion

Keep the row as the selection/tree-navigation surface.

Add a separate icon-only toggle for nodes with children:

- button;
- `aria-label="Expand <name>"` / `"Collapse <name>"`;
- `title` matching action;
- `aria-expanded`;
- click invokes `onToggle(operation.id)`;
- click does not select the operation.

The treeitem must retain `aria-expanded` as well so tree semantics remain clear.

To preserve the roving tree keyboard model, do not add unnecessary sequential tab stops. The treeitem remains the primary keyboard focus target; ArrowRight/ArrowLeft continue expansion/collapse. The toggle can remain pointer/programmatic-only within the row if that gives the cleanest WAI tree behavior, but it must still have an accessible name.

For layout, prefer:

```tsx
{expanded && (
  <ul className="tree-children" role="group">
    ...
  </ul>
)}
```

rather than keeping a zero-height subtree mounted.

This guarantees:

- collapse removes child layout height;
- following operations move upward naturally;
- expansion pushes them downward naturally;
- no hard-coded height;
- no stale grid intrinsic-size behavior.

If a collapse animation is later desired, add it only after the natural-flow behavior is correct and measured; do not reintroduce reserved layout height.

### 6.6 Global expand/collapse toggle

Add one pure helper to existing `workspace/state.ts`, for example conceptually:

```text
expandableScopeIds(document)
allMeaningfulScopesExpanded(document, expandedScopeIds)
```

Exact naming can follow current conventions.

Reuse it in:

- `createTab`;
- reducer `set-all-scopes`;
- App toolbar;
- command creation/tests.

UI behavior:

```text
if any meaningful scope is collapsed:
    action = Expand All
    icon = expand
else:
    action = Collapse All
    icon = collapse
```

Replace the two command-palette commands with one dynamic command, e.g. `view.toggle-all-scopes`, whose label follows the same derived state.

---

## 7. Incremental implementation sequence

Every step must build and retain tests before proceeding.

### Step 0 — baseline + browser characterization

**Goal:** establish factual failures on the actual pulled checkout.

Actions:

- perform Git verification and full test baseline from Section 2;
- capture browser behavior/screenshots;
- record the exact input file / generated file / SQL / nested tree fixtures that reproduce each issue.

**Exit:** no production edits yet; a reproducible failing scenario exists for every issue being changed.

### Step 1 — add characterization regressions before fixes

**Goal:** turn each reproduced failure into a small failing automated test.

Backend additions:

- `tests/dataflow/test_file_effects.py`: retain existing lifecycle tests; add only missing edge cases discovered by reproduction.
- `tests/ui/test_document_store.py` or `tests/ui/test_workspace_sessions.py`: add a full integration test proving operation-scoped `SemanticBindingView.file_choices`.

Required generated-file assertions:

```text
Step A generates file.csv
Step B consumes a file
Step C generates later.csv

B choices:
  includes file.csv
  excludes later.csv
```

Also cover:

- deletion before B → deleted file excluded;
- conditional producer before B → not guaranteed, excluded from ordinary guaranteed choices;
- unrelated physical external/uploaded file → included;
- generated file that physically exists but is only produced later → excluded.

Frontend/Playwright characterization:

- input selector has backend choices and manual path mode;
- output binding currently demonstrates unwanted selector mode;
- dynamic/file-backed SQL demonstrates current read-only behavior;
- nested chevron pointer click fails before fix;
- collapse geometry check demonstrates any retained height.

**Exit:** failures are encoded without changing production behavior.

### Step 2 — separate input and output file controls

**Goal:** correct UI semantics without touching execution-order logic.

Files:

- `SemanticBindingField.tsx`
- `SemanticControls.tsx`
- `configuration.css` only if needed
- frontend browser/component/state tests as appropriate

Changes:

- route `file-input` to input selector(s);
- route `file-output` to simple path editor(s);
- remove the output use of the `__manual__` selection sentinel;
- preserve input manual workspace entry;
- preserve upload support only where it makes semantic sense for input lists.

Update the existing Playwright test that currently expects an `Output file` combobox and selects `__manual__`; it should instead interact directly with the output path field.

**Exit:** no output binding displays "Enter server workspace path…"; input behavior remains intact.

### Step 3 — fix only the proven file-choice integration defect

**Goal:** make the backend-provided choices match actual execution-point availability.

Start by inspecting the new failing integration test.

If `EffectiveDocument.effects[*].available_before` is already correct, fix downstream mapping/serialization only.

If the defect only exists for draft output changes:

- extend the existing workspace projection response with backend-computed operation file choices;
- reuse `file_choices_by_operation()`;
- merge those choices into active binding state when the projection corresponds to the current document revision/edit version;
- keep the existing upload refresh behavior or simplify it only if the projection path fully replaces it without regressions.

Do not add TypeScript lifecycle/order calculations.

**Exit:** generated-before/generated-after/deleted/conditional/external scenarios pass through DocumentStore/API and browser.

### Step 4 — decouple structured SQL capability from emitted-Python literal editability

**Goal:** make safely parsed SQL structurally editable even when its emitted Python representation is dynamic.

Files likely involved:

- `vg2c/sql_editor/capability.py`
- `vg2c/editing.py`
- `vg2c/utilities/sqlite_engine.py`
- possibly a small existing SQL/emission helper module if extracting the renderer prevents a circular dependency
- SQL tests

Changes:

1. establish one helper for the authoritative/effective logical SQL source for a structured SQL binding;
2. remove the rule that `not parameter.editable` automatically zeroes all SQL capabilities;
3. let `parse_sql()` remain the capability authority;
4. allow existing SQL actions when the requested structural part is parser-capable;
5. project the resulting logical SQL through the same runtime SQL-expression renderer used during normal emission;
6. coalesce structural SQL + file-list path changes into one emitted SQL-argument replacement where necessary;
7. keep genuinely unsafe SQL read-only according to parser/item capabilities and read-only reasons.

Keep the SQL operation registry and classes unchanged except for fixes proven by tests.

Do not add a second AST/model.

**Exit:** a file-backed query can edit Columns / Filters / Joins where parser capabilities allow, and generated Python still contains the correct runtime `sql_get_csv_list`/global behavior.

### Step 5 — make the frontend consume backend SQL capabilities directly

**Goal:** remove frontend assumptions about SQL editability.

Files:

- `SemanticBindingField.tsx`
- `StructuredSqlEditor.tsx`
- generated contracts only if the backend contract truly needs a change

Changes:

- stop passing `!binding.editable` as the blanket structured-SQL read-only flag;
- retain document/session read-only state;
- use model capabilities and row-level flags for buttons/actions;
- preserve backend read-only reason display for unsupported SQL.

No frontend parsing of SQL.

**Exit:** Columns / Filters / Joins are enabled exactly where the backend model says they are.

### Step 6 — repair Script Logic pointer expansion and natural-flow collapse

**Goal:** correct interaction and layout with minimal DOM/CSS.

Files:

- `SemanticScriptTree.tsx`
- `scriptLogic.css`
- browser tests

Changes:

- add a real icon-only scope toggle;
- prevent toggle click from selecting/reordering;
- retain treeitem selection on the rest of the row;
- keep ArrowRight/ArrowLeft tree navigation;
- conditional-render child groups when collapsed;
- apply `tree-children` to nested groups if the indentation rule is still needed;
- delete obsolete branch-collapse selectors.

Browser assertions:

- mouse toggle expands/collapses;
- toggle `aria-expanded` and label change;
- row click selects without accidental toggle;
- keyboard Right/Left still works;
- focus order remains sane;
- following siblings change Y position after collapse/expand with no reserved blank region.

**Exit:** pointer, keyboard, accessibility, and flow all agree.

### Step 7 — replace global Expand All / Collapse All with one DRY toggle

**Goal:** one derived tree state and one action.

Files:

- `workspace/state.ts`
- `workspace/state.test.ts`
- `app/App.tsx`
- `workbench/commands.ts`
- command/browser tests

Changes:

- extract shared expandable-scope helper(s);
- replace duplicated nested `some()` calculations;
- replace two toolbar buttons with one icon-only button;
- replace two commands with one dynamic command;
- switch icon, `aria-label`, `title`, and command label based on derived state.

**Exit:** partially expanded → action says Expand All; fully expanded → action says Collapse All; empty/non-nested tree disables the action.

### Step 8 — cleanup and deletion pass

**Goal:** leave one obvious ownership path.

Delete/simplify only after replacements are covered:

- forced SQL all-false compatibility branch for parser-capable dynamic SQL;
- SQL-specific overlapping replacement code made redundant by unified SQL projection;
- stale `.tree-branch` / `.tree-branch__inner` CSS;
- duplicate expandable-scope calculations;
- old two-command/two-button expand-collapse wiring;
- output selector/manual-mode code paths no longer used by outputs.

Run a call-site/import search before deleting each symbol.

### Step 9 — full regression and browser validation

Run:

```bash
pytest -q
cd src/vg2c_ui/frontend
npm test
npm run build
npm run test:browser
```

Run browser validation on desktop, tablet, mobile/reduced-motion, and Firefox according to the existing Playwright configuration.

No test may be weakened to accommodate the changes.

---

## 8. Test strategy

### Core file availability

Keep and extend `tests/dataflow/test_file_effects.py` for pure lifecycle semantics:

- generated-before;
- generated-after;
- deleted;
- conditional/possible;
- external;
- equivalent path spellings/path-base cases if reproduction exposes them.

### File-choice integration

Add DocumentStore/workspace tests that verify `SemanticBindingView.file_choices`, not just raw `FileEffect.available_before`.

Required checks:

- previous generated output reaches later input binding;
- future generated output does not;
- output binding has no input choices;
- physical stale output produced later is not accidentally offered;
- user isolation remains intact;
- paths outside workspace remain excluded.

### Input-vs-output selector

Browser assertions:

- input has choice selector;
- input can switch to manual workspace path where supported;
- output is a direct path editor;
- output never shows existing-file choices or "Enter server workspace path…";
- undo/redo still works on output path.

### SQL capability/edit tests

Extend `tests/sql_editor/test_capability.py` with:

1. ordinary literal SQL remains editable;
2. `SQL_Get_CSV_List` query retains parser-derived Columns / Filters / Joins capabilities;
3. structural edit on such a query produces valid Python and preserves runtime list expansion;
4. file-list edit + structural edit work in both orders;
5. save/reopen/generate preserves both;
6. SQL globals plus a structural edit preserve global references;
7. genuinely unsupported SQL remains read-only for the correct parser reason;
8. stale opaque IDs are still rejected.

Extend DocumentStore/workspace integration tests to prove the same through API transport.

### Script Logic interaction

Browser tests:

- pointer toggle does not select another node;
- pointer toggle updates `aria-expanded`;
- ArrowRight expands;
- ArrowRight on expanded parent moves focus to child;
- ArrowLeft collapses;
- ArrowLeft on collapsed child moves focus to parent;
- Home/End/Up/Down still work;
- reorder controls still work independently.

### Expand/collapse all

State tests:

- no meaningful scopes;
- none expanded;
- partially expanded;
- all expanded;
- document replacement prunes stale scope IDs.

Browser tests:

- one visible icon-only control;
- label/title changes between Expand all scopes and Collapse all scopes;
- command palette exposes the same current action;
- action state matches tree.

### Responsive/layout

On desktop/tablet/mobile:

- no horizontal overflow from new icon controls;
- compact row height is preserved;
- nested indentation remains readable;
- collapsing a large nested branch causes following siblings to move upward;
- expanding restores normal downward flow;
- no fixed-height blank area is left behind;
- reduced-motion configuration remains usable.

---

## 9. Explicit browser-validation scenarios

### Scenario A — prior generated file is selectable

```text
Step A generates file.csv
Step B consumes a file
```

Verify in B:

- `file.csv` appears exactly once;
- selecting it updates the draft;
- backend preview/save accepts it.

### Scenario B — future generated file is not selectable

```text
Step A generates file.csv
Step B consumes a file
Step C generates later.csv
```

Verify in B:

- `file.csv` is selectable;
- `later.csv` is not selectable, even if a stale physical `later.csv` happens to exist in the workspace.

### Scenario C — deleted and conditional files

Verify:

- a guaranteed output deleted before B is not offered;
- a conditional/loop output that is only possible is not presented as guaranteed;
- unrelated uploaded/external workspace input is still offered.

### Scenario D — output binding

Select an operation with `file-output`.

Verify:

- direct path editor;
- no "Enter server workspace path…" selector;
- no existing input-file inventory options;
- editing/undo/redo/save/reopen preserve the value.

### Scenario E — file-backed structured SQL

Use a safe SELECT containing `SQL_Get_CSV_List`.

Verify:

- Columns, Filters, Joins tabs render;
- parser-safe actions are enabled;
- file-list selector remains backend-constrained;
- add/reorder/update a column;
- add/update a filter;
- add/edit a join where schema evidence exists;
- save/reopen/generate;
- generated Python still contains the runtime CSV-list helper rather than flattening it into a static SQL literal.

### Scenario F — unsupported SQL remains safely limited

Use a CTE/set-operation or another parser-declared unsupported structure.

Verify the UI remains read-only for the relevant structural capability and displays the backend reason. This proves the implementation did not merely remove safety checks.

### Scenario G — nested tree natural flow

Create:

```text
Parent scope
  Child 1
  Child 2
Following sibling A
Following sibling B
```

Capture bounding boxes:

- expanded: siblings appear below children;
- collapsed: sibling A's Y coordinate moves upward by approximately the removed child group height;
- sibling B follows A with normal spacing;
- re-expanded: children return and siblings move downward naturally.

There must be no reserved blank block between Parent and Following sibling A.

### Scenario H — global toggle

Start partially collapsed:

- control label is Expand all scopes;
- click → all meaningful scopes expanded;
- same control now labels Collapse all scopes;
- click → all meaningful scopes collapsed;
- command palette shows the same current action.

---

## 10. Completion criteria

Implementation is complete only when all of the following are true:

- current checkout branch/HEAD/working tree were explicitly recorded before implementation;
- backend and frontend baselines were actually run;
- reported behaviors were reproduced in the browser before modification;
- output bindings use a dedicated/simple output-path editor;
- input choices are entirely backend-derived and execution-order-correct;
- guaranteed prior generated outputs are offered and future/deleted/possible outputs are not;
- no TypeScript file-lifecycle ordering logic was added;
- parser-safe dynamic SQL is structurally editable through existing Columns / Filters / Joins capabilities;
- runtime SQL substitutions and SQL globals survive structured edits;
- unsupported SQL remains safely read-only according to parser capabilities;
- chevrons are real icon-only expand/collapse controls;
- toggle clicks do not cause unrelated row selection;
- tree keyboard navigation remains correct;
- collapsed nested scopes consume no layout height;
- one DRY global expand/collapse toggle replaces the two old controls/commands;
- compact/responsive behavior remains correct;
- stale compatibility/CSS/duplicate calculations are deleted after coverage exists;
- `pytest -q`, `npm test`, `npm run build`, and `npm run test:browser` all pass;
- browser evidence covers the explicit scenarios above.

---

## 11. Architectural decision

There is one meaningful architectural choice around structured SQL.

### Recommended: core semantic SQL projection

Keep the existing parser/model/operation classes and make the core edit projection capable of re-rendering a structured SQL binding through the existing SQL emission logic.

Advantages:

- one SQL model;
- one source of truth for parser safety;
- frontend remains transport/UI only;
- preserves runtime substitutions;
- keeps sidecar edits semantic;
- directly removes the incorrect "dynamic Python means read-only SQL" coupling.

### Rejected: frontend/raw generated-Python workaround

Examples would include enabling controls despite backend capability flags, patching raw SQL in TypeScript, or simply marking the generated `CodeExpr` editable and serializing with `repr()`.

These approaches are smaller superficially but create semantic duplication or can destroy runtime substitutions. They should not be used.

No additional architecture decision is required before implementation unless Gate 0 reveals that the reported generated-file omission comes from a different dataflow case than the current core model covers.
