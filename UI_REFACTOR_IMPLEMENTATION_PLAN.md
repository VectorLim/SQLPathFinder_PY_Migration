# SQLPathFinder Frontend Refactoring and UI Improvement Plan

> **Current repository HEAD:** `3cd37769ae1cf00da2105d4d68c3c75d73584db4` on `main`.
>
> **Application-code baseline inspected:** `1166cb22c49905c1a71d770e2bb0bf3e4062e871`. The commit after it only added this planning document; no frontend implementation has changed since that application baseline.
>
> **Research inputs:** the Detailed Watermelon UI Audit, SQLPathFinder UI Implementation Plan, and Watermelon Component Suitability Matrix were reviewed as design input, then validated against the actual repository. This document is the consolidated implementation source of truth.
>
> **Status:** planning only. Do not implement UI changes as part of this document update.

## 1. Execution constraints that remain authoritative

Keep the current responsibility direction:

```text
compiler / domain semantics
        ↓
FastAPI models + serialization
        ↓
generated TypeScript contracts + api.ts
        ↓
workspace reducer / controller hooks
        ↓
feature UI state
        ↓
presentation components / primitives
```

Preserve these existing strengths unless a regression test proves a real problem:

- `workspaceState.ts` as the editor/document state machine;
- `useWorkspace.ts` as the controller for document mutations and backend operations;
- stale-response protection based on tab instance/version/request ownership;
- generated `contracts.generated.ts` as the frontend domain-contract source;
- backend-owned SQL parsing/mutation and data-flow projection;
- `CAPABILITY_EDITORS` as the small capability dispatch seam in `OperationEditor.tsx`;
- the existing domain-specific `ScriptTree`, including ARIA tree semantics, roving focus, arrow/Home/End navigation, search-driven temporary expansion, and scope behavior;
- native semantic tables for CSV preview;
- inline durable diagnostics and validation errors;
- CSS design tokens, focus-visible styling, reduced-motion support, and contrast handling;
- the single-page application model; do not add a frontend router for this refactor.

Do **not** introduce:

- Redux, Zustand, or another application-wide state framework;
- client-side SQL parsing or compiler-semantic reconstruction;
- duplicate TypeScript domain models beside generated contracts;
- a plugin framework for commands;
- Tailwind/shadcn as a second styling system;
- Motion purely for decorative transitions;
- fake upload percentages;
- a generic tree replacement for `ScriptTree`;
- compatibility layers that survive after their replacement is complete.

Watermelon remains a selective interaction/design reference. Copy only behavior that improves this workbench. Do not copy catalog components wholesale when native HTML, existing CSS, or a smaller accessible primitive is sufficient.

## 2. Current code areas this plan acts on

Primary frontend files:

```text
src/vg2c_ui/frontend/src/App.tsx
src/vg2c_ui/frontend/src/ContextSidebar.tsx
src/vg2c_ui/frontend/src/OperationEditor.tsx
src/vg2c_ui/frontend/src/ScriptTree.tsx
src/vg2c_ui/frontend/src/StructuredSqlEditor.tsx
src/vg2c_ui/frontend/src/api.ts
src/vg2c_ui/frontend/src/contracts.generated.ts
src/vg2c_ui/frontend/src/main.tsx
src/vg2c_ui/frontend/src/operationLabels.ts
src/vg2c_ui/frontend/src/styles.css
src/vg2c_ui/frontend/src/sql/sqlEditor.css
src/vg2c_ui/frontend/src/useWorkspace.ts
src/vg2c_ui/frontend/src/workspaceState.ts
src/vg2c_ui/frontend/src/workspaceState.test.ts
src/vg2c_ui/frontend/package.json
```

Backend/API boundary files likely to change early:

```text
src/vg2c_ui/api/models.py
src/vg2c_ui/api/contracts.py
src/vg2c_ui/api/serialization.py
src/vg2c_ui/api/workspace.py
src/vg2c_ui/services/workspaces.py
scripts/generate_frontend_contracts.py
tests/ui/test_contracts.py
tests/ui/test_workspace_sessions.py
```

The current concrete maintainability problems that drive the sequence are:

- `App.tsx` owns workspace file inventory, source selection, upload orchestration, translation orchestration, global busy/message state, tabs, toolbars, shortcuts, diagnostics, and inspector coordination;
- frontend code classifies workspace files using `.endsWith('.txt')` and `startsWith('generated/')` even though workspace semantics belong on the backend boundary;
- `formatScopeLabel()` interprets compiler scope kinds in React rather than consuming a fully presentation-ready backend label;
- upload feedback uses one app-wide `busy` flag and one overloaded `message` string;
- `.status-message` disappears at narrower viewports, hiding errors/progress;
- the top bar combines hidden file inputs, folder input, a multiple `<select>`, translation, generated downloads, and status messaging;
- `StructuredSqlEditor.tsx` uses browser `prompt()` for add-selection, add-filter, and add-join flows;
- operation file-reference and diagnostic presentation is duplicated between generic and SQL editors;
- `ContextSidebar.Panel` unnecessarily mirrors native `<details>` open state with React state;
- compact inspector behavior uses a custom backdrop/overlay implementation rather than a shared accessible dialog/sheet primitive;
- `sql/sqlEditor.css` contains substantial selectors for structures no longer rendered by the current TSX;
- reusable control styling is duplicated across global and SQL CSS.

## 3. Dependency-ordered execution map

The intended dependency flow is:

```text
1. Characterize current behavior
        ↓
2. Correct backend/frontend contracts
        ↓
3. Establish minimal shared UI primitives
        ↓
4. Separate workspace-file state from App
        ↓
5. Clean the stable workbench shell/layout
        ↓
6. Add one command system
        ↓
7. Replace upload/source intake
        ↓
8. Replace compact inspector infrastructure
        ↓
9. Replace Structured SQL prompt flows
        ↓
10. Unify feedback/dialog behavior
        ↓
11. Responsive + accessibility hardening
        ↓
12. Delete superseded/dead code and dependencies
        ↓
13. Final architecture and regression review
```

Steps 6 and 7 are low-coupled after Step 5, but keep the order above so the command palette has stable workbench actions before upload commands are added to it. Do not begin Steps 8–10 before the shared primitives are in place.

---

# Step-by-step execution plan

## Step 1 — Baseline and regression coverage

### Objective

Establish executable characterization of behavior that must survive the refactor, especially the state machine, `ScriptTree`, editing flow, file tabs, upload/translate flow, and responsive inspector. This step changes test infrastructure only unless a tiny accessibility/testability fix is required.

### Scope

Likely files/modules:

```text
src/vg2c_ui/frontend/package.json
src/vg2c_ui/frontend/package-lock.json
src/vg2c_ui/frontend/src/workspaceState.test.ts
src/vg2c_ui/frontend/playwright.config.ts               # new, if Playwright is selected
tests/ui/*                                               # existing backend UI/API tests
new browser test files under frontend/e2e or tests/ui-e2e
```

Existing production files may receive only stable semantic labels/roles if a test cannot locate an element accessibly. Prefer role/name selectors over `data-testid`.

### Implementation work

1. Capture the exact baseline commands that must stay green:
   - frontend contract check;
   - TypeScript typecheck;
   - reducer tests;
   - frontend build;
   - relevant Python UI/API tests;
   - SQL editor/domain tests that protect backend SQL semantics.
2. Add a browser-level regression harness. Prefer Playwright because the risky behavior is keyboard/focus/responsive interaction, not isolated rendering.
3. Add `@axe-core/playwright` only if it is used immediately in representative accessibility checks.
4. Build a minimal fixture workflow from existing repository fixtures rather than inventing a separate demo application.
5. Characterize these flows before changing UI structure:
   - load workspace file list;
   - upload an allowed source file;
   - upload data files/folder-relative files where fixture support exists;
   - translate one or more source files;
   - open/switch/close generated-document tabs;
   - retain edits on an inactive dirty tab;
   - search `ScriptTree`;
   - use ArrowUp/ArrowDown/Home/End and scope ArrowLeft/ArrowRight navigation;
   - select an operation and expose its editor;
   - edit a parameter, undo, redo, preview, and apply;
   - reject/ignore stale asynchronous results through existing reducer tests;
   - open and close file context on desktop and compact layouts;
   - preview CSV where an existing fixture supports it;
   - preserve generated download links/archive access.
6. Record viewport checkpoints used for the rest of the work:
   - desktop: 1440×900;
   - compact desktop/tablet landscape: 1024×768;
   - tablet portrait: 768×1024;
   - phone: 390×844;
   - narrow phone: 320×568.
7. Add one representative axe check for the initial workbench state and one for an open overlay state.

### Refactoring/deletion work

None beyond deleting any temporary selectors/fixtures created only while developing the tests. Do not refactor production components in this step.

### Dependencies

None. This is the prerequisite for all subsequent replacement work.

### Validation

- `npm test` passes using the existing project scripts;
- `npm run build` passes;
- relevant `pytest tests/ui` tests pass;
- new browser characterization tests pass against the unchanged UI;
- current `ScriptTree` keyboard behavior is explicitly covered;
- no baseline test depends on DOM class names where an accessible role/name is available.

### Expected completion state

This step is finished only when:

- there is an automated regression path for the main upload → translate → edit → preview/apply workflow;
- `ScriptTree` keyboard semantics are protected;
- file-tab switching/closing is protected;
- compact inspector open/close behavior is protected;
- reducer stale-response tests still pass;
- browser/accessibility tooling is actually exercised, not merely installed;
- no user-visible behavior has intentionally changed.

### Commit boundary

Use a small coherent group of at most two commits:

```text
test(ui): add browser regression harness
test(ui): characterize current workbench workflows
```

Combine them if the harness and first tests are small enough to review together.

---

## Step 2 — Move remaining workspace/presentation semantics into generated contracts

### Objective

Remove frontend inference that belongs to the workspace/compiler boundary before building new UI on top of those assumptions.

### Scope

Likely files/modules:

```text
src/vg2c_ui/api/models.py
src/vg2c_ui/api/workspace.py
src/vg2c_ui/api/serialization.py
src/vg2c_ui/services/workspaces.py
src/vg2c_ui/frontend/src/api.ts
src/vg2c_ui/frontend/src/contracts.generated.ts
src/vg2c_ui/frontend/src/App.tsx
src/vg2c_ui/frontend/src/operationLabels.ts
scripts/generate_frontend_contracts.py
tests/ui/test_contracts.py
tests/ui/test_workspace_sessions.py
```

### Implementation work

1. Extend the backend workspace-file contract so the frontend does not derive semantic role from paths/suffixes.
2. Keep classification centralized in the workspace/backend layer. A minimal useful file contract should expose concepts such as:
   - file role/kind: source input, data input, or generated output;
   - whether the file may be selected for translation;
   - existing path/size/modified metadata.
3. Do not duplicate the allowed-suffix set in the API layer. Reuse `WorkspaceManager` policy/configuration.
4. Expose the upload policy through one small read endpoint or equivalent generated contract if the upload UI needs preflight validation. Only expose values that are actually consumed:
   - allowed upload suffixes;
   - maximum bytes per file;
   - maximum file count;
   - maximum workspace bytes.
5. Keep server validation authoritative even after policy metadata is exposed.
6. Make `ScopeView.label` presentation-ready in backend serialization so React does not interpret `if`, `if-branch`, `else-branch`, `macro`, or `loop` kinds to generate user-facing names.
7. Regenerate `contracts.generated.ts` from the Pydantic models; never hand-edit the generated file.
8. Update current consumers immediately:
   - replace `.endsWith('.txt')` source classification with contract metadata;
   - replace `startsWith('generated/')` output classification with contract metadata;
   - simplify/remove scope-kind interpretation in `operationLabels.ts`.
9. Keep `api.ts` as transport only. It may add `getWorkspacePolicy()` but must not repeat backend classification rules.

### Refactoring/deletion work

Delete in the same step after the new contract is consumed:

- frontend `.endsWith('.txt')` semantic classification;
- frontend `startsWith('generated/')` semantic classification;
- redundant scope-kind-to-label mapping in React;
- any copied upload limit/suffix constants introduced during implementation.

### Dependencies

Step 1 regression baseline must be green.

### Validation

- generated contract check passes;
- backend tests prove every workspace file receives the intended role/capabilities;
- backend tests prove upload policy values come from the existing manager configuration;
- source/data/generated files still display/select correctly in the unchanged UI;
- static search confirms no frontend source/output classification remains based on suffix/path prefix;
- `ScriptTree` labels remain unchanged or intentionally improved by backend-provided labels;
- frontend test/build and relevant Python tests pass.

### Expected completion state

This step is finished only when:

- frontend code consumes explicit workspace-file semantics;
- upload policy has one backend source of truth;
- user-facing scope labels are backend-provided;
- generated TypeScript contracts are current;
- no duplicate frontend/backend models or policy constants exist;
- all baseline workflows still pass.

### Commit boundary

Prefer one atomic commit because backend contract, generated types, and consumers must move together:

```text
refactor(api): expose workspace presentation metadata
```

Do not leave a commit where generated contracts and frontend consumers disagree.

---

## Step 3 — Establish minimal shared UI primitives and remove obvious editor duplication

### Objective

Create only the shared UI building blocks that have immediate consumers in later steps, while eliminating existing duplicated presentation logic before adding more UI.

### Scope

Likely files/modules:

```text
src/vg2c_ui/frontend/package.json
src/vg2c_ui/frontend/package-lock.json
src/vg2c_ui/frontend/src/styles.css
src/vg2c_ui/frontend/src/OperationEditor.tsx
src/vg2c_ui/frontend/src/StructuredSqlEditor.tsx
src/vg2c_ui/frontend/src/ui/*                        # new, deliberately small
src/vg2c_ui/frontend/src/OperationPresentation.tsx   # new shared editor presentation, or equivalent
```

### Implementation work

1. Add the accessible primitive dependency selected in the existing plan (`radix-ui`) when it has an immediate production consumer.
2. Add `lucide-react` when icon-only controls begin using it. Do not carry both Lucide and another icon library.
3. Implement only shared primitives with concrete reuse:
   - Button variant/size behavior if repeated button semantics justify a component;
   - Badge/status-chip presentation;
   - Dialog foundation with focus trapping, Escape, overlay, labelled title/description, and focus return;
   - Tooltip for icon-only or abbreviated controls.
4. Keep native controls where native behavior is sufficient:
   - `<select>`;
   - checkbox;
   - `<textarea>`;
   - `<table>`;
   - `<details>`/`<summary>` unless later requirements need controlled collapsible behavior.
5. Treat Button Group primarily as layout/semantic grouping (`role="group"`) rather than a configurable framework component.
6. Consolidate genuinely duplicated editor presentation:
   - one `FileReferences`/equivalent component for Reads/Produces chips;
   - one `OperationDiagnostics`/equivalent component for dependency diagnostics.
7. Reuse existing CSS variables. Add primitive styles to one shared location; do not introduce utility-class infrastructure.
8. Keep visual changes intentionally small in this step so regressions can be attributed to component extraction rather than redesign.

### Refactoring/deletion work

Delete immediately after shared replacements are wired:

- `FileChips` duplicate in `OperationEditor.tsx`;
- `FileChips`/`FileSummary` duplicate path in `StructuredSqlEditor.tsx` where the shared component fully replaces it;
- duplicate operation-diagnostic rendering helpers;
- duplicated CSS rules that become identical shared primitive rules;
- any temporary wrapper that merely forwards all props without owning styling, semantics, or behavior.

### Dependencies

Steps 1–2.

### Validation

- generic and structured-SQL editors render identical file-reference information to baseline;
- diagnostics retain `role="alert"`/appropriate durable error semantics;
- Dialog focus trap/Escape/focus return has a focused browser test before it is used widely;
- icon-only controls have accessible names;
- typecheck/build/browser regression tests pass;
- dependency lockfile contains only packages actually imported by production or tests.

### Expected completion state

This step is finished only when:

- there is exactly one shared operation file-reference presentation path;
- there is exactly one shared operation diagnostic presentation path;
- the shared Dialog and Tooltip primitives have tests and real consumers;
- no Tailwind/shadcn/Motion/react-icons dependency has been added;
- no primitive exists solely because Watermelon provides one.

### Commit boundary

Use a small coherent pair if needed:

```text
refactor(ui): add minimal accessible primitives
refactor(ui): deduplicate operation presentation
```

A single commit is acceptable if both are compact.

---

## Step 4 — Separate workspace-file inventory/controller state from `App`

### Objective

Reduce `App.tsx` ownership before changing header/upload UI, while preserving the existing `workspaceState`/`useWorkspace` document state architecture.

### Scope

Likely files/modules:

```text
src/vg2c_ui/frontend/src/App.tsx
src/vg2c_ui/frontend/src/api.ts
src/vg2c_ui/frontend/src/useWorkspaceFiles.ts     # new
src/vg2c_ui/frontend/src/useWorkspace.ts          # only if a tiny interface adjustment is needed
```

### Implementation work

1. Add a focused `useWorkspaceFiles` hook/controller that owns **remote workspace inventory**, not document editing semantics.
2. Its responsibilities should remain narrow:
   - load workspace file list;
   - load/carry workspace upload policy;
   - expose refresh;
   - expose current loading/error state for the inventory request.
3. Do not move document tabs, edits, CSV state, projection, validation, or SQL actions out of `workspaceState`/`useWorkspace`.
4. Do not add Context providers; `App` can call both hooks and pass their outputs down explicitly.
5. Keep source-selection/upload queue state where it is temporarily until Step 7 rather than prematurely designing a second large controller.
6. Replace the initial `App` effect and direct `listWorkspaceFiles()` ownership with the new hook.
7. Preserve error visibility during this intermediate step; do not silently swallow inventory errors.

### Refactoring/deletion work

Delete from `App.tsx` once the hook is active:

- `workspaceFiles` fetch effect;
- local `refreshWorkspaceFiles()` implementation;
- direct list-policy fetching duplicated by the hook;
- imports used only by the removed inventory code.

### Dependencies

Steps 1–3, especially Step 2’s explicit workspace contracts.

### Validation

- initial file inventory appears exactly as before;
- upload/translation still refreshes file inventory through one hook API;
- generated downloads remain correct;
- inventory errors remain visible;
- `workspaceState.test.ts` is untouched or still passes unchanged;
- no new global state/store is introduced.

### Expected completion state

This step is finished only when:

- `App.tsx` no longer implements workspace-file fetching/refetch logic;
- there is one remote workspace-file inventory owner;
- document/editor state ownership remains unchanged;
- all baseline workflows pass.

### Commit boundary

One clean commit:

```text
refactor(ui): isolate workspace file inventory state
```

---

## Step 5 — Clean the stable workbench shell and layout

### Objective

Make `App.tsx` a composition/orchestration root by extracting stable workbench responsibilities before adding command/upload/inspector features.

### Scope

Likely files/modules:

```text
src/vg2c_ui/frontend/src/App.tsx
src/vg2c_ui/frontend/src/FileTabs.tsx          # new
src/vg2c_ui/frontend/src/EditorToolbar.tsx      # new only if it owns meaningful toolbar behavior
src/vg2c_ui/frontend/src/ChangeToolbar.tsx      # new
src/vg2c_ui/frontend/src/styles.css
```

Do not create an `AppShell`, `Layout`, `HeaderContainer`, or similar wrapper if it merely renders `children` and a class name.

### Implementation work

1. Extract `FileTabs` because it owns meaningful tab semantics:
   - tablist/tab roles;
   - active status;
   - status indicator;
   - close action;
   - ArrowLeft/ArrowRight/Home/End behavior;
   - focus movement.
2. Extract `ChangeToolbar` because it presents and drives a cohesive document-editing state:
   - unsaved change count/status copy;
   - undo/redo;
   - preview;
   - apply;
   - conflict reload.
3. Extract the editor toolbar only if doing so groups real behavior (tree search, expand/collapse, inspector launcher). Do not extract a markup-only wrapper.
4. Keep tree-search value ownership close to the workbench until/unless another consumer needs it.
5. Restructure the root layout around explicit regions:
   - top/header intake region;
   - file tabs;
   - editor toolbar/change toolbar;
   - scrollable workbench body;
   - inspector region.
6. Fix structural overflow/min-size rules at the grid/flex root instead of adding component-specific breakpoint patches.
7. Preserve the current desktop sidebar layout and current compact behavior until Step 8 replaces the compact overlay implementation.
8. Keep the existing upload controls functional but avoid extracting them into a temporary `LegacyUploadControls` component that would be deleted two steps later.

### Refactoring/deletion work

Delete after extraction:

- inline file-tab JSX from `App.tsx`;
- `handleTabKeys()` from `App.tsx`;
- inline change-toolbar JSX from `App.tsx`;
- obsolete shell CSS selectors made redundant by the corrected root layout;
- pass-through components created during experimentation.

### Dependencies

Steps 1–4.

### Validation

- file-tab keyboard regression suite passes unchanged;
- tab close behavior and active-tab fallback remain correct;
- unsaved state/status indicators remain correct;
- Ctrl/Cmd+Z, Ctrl/Cmd+Y, and Ctrl/Cmd+S behavior remains correct for now;
- no new horizontal page overflow at the five agreed viewport checkpoints;
- `ScriptTree` behavior remains unchanged;
- build/tests pass.

### Expected completion state

This step is finished only when:

- `App.tsx` contains orchestration and composition rather than tab keyboard algorithms/workspace-file fetch logic;
- file tabs have one implementation;
- change toolbar has one implementation;
- root layout owns sizing/overflow rather than a collection of one-off child fixes;
- upload and inspector behavior are still functionally baseline-compatible pending their dedicated replacement steps.

### Commit boundary

Prefer two independently reviewable commits if the root layout changes are substantial:

```text
refactor(ui): extract file tabs and change toolbar
refactor(ui): simplify workbench root layout
```

Otherwise use one commit.

---

## Step 6 — Add the command palette with one command registry

### Objective

Add Ctrl/Cmd+K workbench navigation/actions without creating a plugin architecture or duplicating action ownership.

### Scope

Likely files/modules:

```text
src/vg2c_ui/frontend/package.json
src/vg2c_ui/frontend/package-lock.json
src/vg2c_ui/frontend/src/App.tsx
src/vg2c_ui/frontend/src/CommandPalette.tsx      # new
src/vg2c_ui/frontend/src/commands.ts             # new single registry/derivation module
src/vg2c_ui/frontend/src/FileTabs.tsx
src/vg2c_ui/frontend/src/ScriptTree.tsx          # only public helper reuse if needed
src/vg2c_ui/frontend/src/styles.css
```

### Implementation work

1. Add `cmdk` and use the shared Dialog foundation rather than copying Watermelon Command Search code.
2. Define one small command descriptor type, for example:
   - stable `id`;
   - `group`;
   - label;
   - optional keywords/shortcut hint;
   - enabled/disabled state;
   - action callback.
3. Implement one `buildCommands(...)`/equivalent derivation path. Do not allow arbitrary component self-registration.
4. Initial command groups should come from current application state/actions:
   - **Navigation:** activate open document; jump to an operation/scope; open inspector;
   - **Editing:** undo, redo, preview changes, apply changes, reload conflict;
   - **View:** expand all, collapse all;
   - **Workspace:** commands that already have stable actions at this point, such as download archive if useful.
5. Use existing `selectItem()`/ancestor expansion behavior when a command jumps to a tree item; do not duplicate tree ancestry logic.
6. Implement Ctrl/Cmd+K globally, excluding cases where browser/native behavior would be damaged.
7. Preserve current Ctrl/Cmd+Z/Y/S shortcuts, but route them through the same action callbacks where practical so behavior does not diverge.
8. On close/execute:
   - restore focus appropriately;
   - clear query as appropriate;
   - do not trap focus after the dialog closes.
9. Let `cmdk` handle list filtering/keyboard mechanics; do not create a parallel filtering framework.
10. Step 7 may append upload/translate commands to the same command derivation function. It must not create a second registry.

### Refactoring/deletion work

- remove duplicated shortcut action branches if they can safely share the same callbacks as commands;
- delete any prototype registry or component-specific command arrays after the single derivation path is established;
- do not keep Watermelon demo code, Motion code, or unused command-search assets.

### Dependencies

Steps 1–5, especially stable shell actions and shared Dialog.

### Validation

Automate and manually verify:

- Ctrl+K on Windows/Linux and Cmd+K on macOS behavior;
- palette opens from workbench focus and from an editor control without corrupting input values;
- ArrowUp/ArrowDown navigate commands;
- Enter executes exactly one action;
- Escape closes;
- focus returns to the invoking context;
- disabled commands do not execute;
- document navigation activates the correct tab;
- operation navigation reveals ancestors and focuses/selects the intended operation;
- undo/redo/preview/apply command enablement matches toolbar enablement;
- axe reports no serious/critical issue for the open palette.

### Expected completion state

This step is finished only when:

- there is exactly one command registry/derivation path;
- Ctrl/Cmd+K is fully keyboard-operable;
- commands call existing application actions rather than reimplementing them;
- there is no plugin/registration framework;
- no upload-specific second registry is planned or present;
- existing tree and toolbar shortcuts still pass regression tests.

### Commit boundary

One clean feature commit:

```text
feat(ui): add keyboard command palette
```

Split test additions into the same commit unless the test harness itself changes substantially.

---

## Step 7 — Replace the upload/source-selection workflow

### Objective

Replace the crowded top-bar hidden-input/multiple-select flow with an explicit, truthful source-intake workflow that supports drag/drop, validation, queue state, failure recovery, and source selection while reusing backend policy/semantics.

### Scope

Likely files/modules:

```text
src/vg2c_ui/frontend/src/App.tsx
src/vg2c_ui/frontend/src/api.ts
src/vg2c_ui/frontend/src/useWorkspaceFiles.ts
src/vg2c_ui/frontend/src/SourceUpload.tsx       # new
src/vg2c_ui/frontend/src/useSourceIntake.ts      # new only if palette + UI need shared intake state/actions
src/vg2c_ui/frontend/src/commands.ts
src/vg2c_ui/frontend/src/styles.css
```

### Implementation work

1. Use the Watermelon file-upload pattern as an interaction reference only.
2. Provide clear entry actions:
   - Upload files;
   - Upload folder using the existing `webkitdirectory` capability;
   - drag/drop files onto the intake area.
3. Do not implement complex recursive folder drag/drop unless browser support can be delivered cleanly. Explicit folder browse already preserves folder-relative paths and is sufficient for this refactor.
4. Validate staged files against the backend-provided policy before network submission:
   - supported suffix;
   - per-file size;
   - obvious file-count/workspace-capacity violations when determinable.
5. Keep backend validation authoritative. If client and server disagree, display the server error.
6. Represent queue state truthfully:
   - queued;
   - uploading;
   - uploaded;
   - failed.
7. Do **not** show byte percentages because the current `fetch` upload API exposes no trustworthy progress callbacks.
8. Prefer one-file-at-a-time calls through the existing upload endpoint rather than one opaque multi-file request. This gives deterministic per-file success/failure and makes retry semantics correct without changing backend storage behavior.
9. Preserve `webkitRelativePath`/relative paths when each file is uploaded individually.
10. Allow:
    - removal of queued items before upload;
    - removal of failed queue items;
    - retry of a failed item.
11. Do not claim server-side removal of already uploaded files; no delete endpoint exists and adding one is outside this refactor.
12. Refresh remote workspace inventory after successful uploads and after failures where server state may have changed.
13. Replace the `<select multiple>` with an explicit source-selection list derived from `can_translate`/contract metadata.
14. Preserve useful existing behavior: newly uploaded source files should become selected for translation unless the user explicitly deselects them.
15. Keep source selection simple—checkbox list first. Do not add a combobox/search framework unless real file counts demonstrate the need.
16. Keep translation behavior in `useWorkspace`; the intake feature supplies selected source paths and calls the existing translation action.
17. If command palette actions need intake state (`Upload files`, `Upload folder`, `Translate selected`), introduce one focused `useSourceIntake` controller only because that state/actions are shared between the intake UI and command system. Do not use it as a general workspace service.
18. Add these actions to the existing command derivation; do not add another command registry.
19. Keep generated-file downloads and workspace ZIP access available but visually separate them from source intake.

### Refactoring/deletion work

Delete once the replacement passes parity:

- inline hidden file input labels in `App.tsx`;
- inline folder input label;
- current `upload(event)` handler;
- current multiple `<select>` source picker;
- old top-bar upload layout CSS;
- old source-path classification branches already superseded by Step 2;
- app-wide upload use of the global `busy` state;
- any temporary old/new upload switch.

### Dependencies

Steps 1–6. Step 2 policy/file metadata and Step 3 Dialog are required.

### Validation

Test at minimum:

- single source file upload;
- multiple mixed allowed files;
- folder upload preserves relative paths;
- drag/drop files;
- unsupported extension rejected before upload and still rejected server-side if bypassed;
- oversized file handling;
- duplicate path handling;
- one failed file does not obscure the successful state of other sequentially uploaded files;
- retry of a failed file;
- removal of queued/failed items;
- newly uploaded source becomes selectable/selected;
- data files never become translation sources unless backend marks them translatable;
- translate selected sources still opens documents and refreshes generated files;
- no fake percentage is displayed;
- keyboard-only file browse/source selection is usable;
- phone/tablet layout does not overflow.

### Expected completion state

This step is finished only when:

- old upload controls and multi-select source picker are no longer referenced;
- upload queue has deterministic per-file states;
- drag/drop and browse use one validation/upload path;
- client validation uses backend policy metadata;
- source selection uses backend file capability metadata;
- upload errors are visible at every viewport;
- no server-delete behavior is implied;
- command palette uses the same intake actions;
- all baseline translation behavior remains intact.

### Commit boundary

Use a small coherent group because the feature has separable behavior and presentation:

```text
refactor(ui): add source intake state and upload queue
feat(ui): replace workspace upload and source selection
```

Do not merge unrelated inspector/SQL work into these commits.

---

## Step 8 — Modernize the inspector without changing its domain content

### Objective

Preserve the valuable data-flow/file-context functionality while replacing duplicated desktop/compact overlay mechanics with one inspector content path and accessible compact presentation.

### Scope

Likely files/modules:

```text
src/vg2c_ui/frontend/src/ContextSidebar.tsx
src/vg2c_ui/frontend/src/Inspector.tsx          # optional clean-cut rename/replacement
src/vg2c_ui/frontend/src/App.tsx
src/vg2c_ui/frontend/src/styles.css
src/vg2c_ui/frontend/src/ui/Dialog.tsx
```

### Implementation work

1. Keep all backend/projected semantics unchanged:
   - required inputs;
   - produced files;
   - upstream/downstream open documents;
   - dependency issues;
   - CSV preview;
   - file details.
2. Separate **inspector content** from **presentation shell**:
   - desktop: persistent `<aside>` in the workbench grid;
   - compact/tablet: right-side modal sheet;
   - phone: bottom sheet.
3. Implement compact sheet behavior using the shared accessible Dialog primitive styled responsively. A Sheet is a presentation of Dialog, not a separate parallel abstraction.
4. Do not wrap the persistent desktop inspector in modal/dialog semantics.
5. Preserve one content component so desktop and compact views cannot drift.
6. Remove React-controlled mirror state from section `<details>` unless a real cross-component need appears. Use `defaultOpen` for initial expansion.
7. Keep native CSV table; do not introduce DataTable infrastructure.
8. Keep `Data Flow` prominent. Use Tabs only if a usability test shows `File Details` and `Data Flow` are mutually exclusive modes; otherwise the current section/collapsible hierarchy is simpler.
9. On compact overlay:
   - trap focus;
   - close with Escape;
   - close with overlay click when safe;
   - return focus to the opener;
   - prevent background interaction/scroll as provided by the dialog primitive.
10. Preserve `onActivateDocument` behavior and close compact inspector after activation.

### Refactoring/deletion work

Delete after parity:

- custom `.context-backdrop` element/path;
- compact custom visibility/focus workaround code superseded by Dialog;
- duplicated compact/desktop content if any appears during migration;
- `Panel` `useState` that only mirrors `<details open>`;
- obsolete context backdrop/sheet transition CSS;
- `ContextSidebar.tsx` itself if a clean `Inspector.tsx` replacement is completed and all imports are updated.

Do not leave both `ContextSidebar` and `Inspector` as aliases.

### Dependencies

Steps 1–5 and shared Dialog from Step 3. Command/upload work is not technically required, but keeping the sequence avoids simultaneous root-layout churn.

### Validation

- desktop inspector is persistently visible at desktop breakpoint;
- 1024×768 uses right sheet without clipping;
- phone uses bottom sheet without exceeding viewport;
- Escape/overlay close works;
- focus trap and focus return work;
- background controls are not reachable while compact modal inspector is open;
- CSV preview scrolls inside its own region and does not force page overflow;
- upstream/downstream activation still selects the correct document;
- axe checks pass for open compact inspector;
- `ScriptTree` remains usable with inspector closed/open as appropriate.

### Expected completion state

This step is finished only when:

- desktop and compact inspector share one content implementation;
- custom backdrop/modal infrastructure is removed;
- native section state is not redundantly mirrored in React;
- data-flow behavior is unchanged;
- compact inspector has correct dialog focus semantics;
- there is no `ContextSidebar` compatibility alias if the component was renamed.

### Commit boundary

One clean refactor commit is preferred:

```text
refactor(ui): unify responsive file inspector
```

Use a second cleanup commit only if CSS deletion is large enough to obscure the behavior change.

---

## Step 9 — Replace Structured SQL `prompt()` flows with controlled workbench forms

### Objective

Eliminate the browser-prompt UI and obsolete SQL presentation paths while keeping SQL semantics, parsing, validation, operators, joins, and mutation ownership entirely on the backend.

### Scope

Likely files/modules:

```text
src/vg2c_ui/frontend/src/StructuredSqlEditor.tsx
src/vg2c_ui/frontend/src/sql/SqlAddDialogs.tsx       # new
src/vg2c_ui/frontend/src/sql/sqlEditor.css
src/vg2c_ui/frontend/src/ui/Dialog.tsx
src/vg2c_ui/frontend/src/ui/Tabs.tsx                 # add only if real SQL mode tabs are implemented
src/vg2c_ui/frontend/src/OperationPresentation.tsx
```

Backend SQL API/model files should not change unless a concrete missing contract is discovered. Do not redesign backend SQL logic as part of this UI refactor.

### Implementation work

1. Replace `window.prompt()` add flows with controlled accessible forms:
   - add selection;
   - add filter;
   - add join.
2. Prefer three small purpose-specific form components over one deeply configurable mega-form.
3. Keep the three forms in one SQL-focused module if that is clearer than three files.
4. Initialize allowed operators/connectors/join types from the returned `SqlModelView`; never duplicate operator lists in React.
5. Validate only presentation-level requirements locally (for example required text is non-empty). Backend action validation remains authoritative.
6. Disable form submission while the structured SQL action is running.
7. Show backend failures inside the active SQL editor/dialog and preserve user-entered values when correction is possible.
8. Preserve existing update/remove/move action calls and stale-response protection through `useWorkspace.runSqlAction`.
9. Keep raw SQL display read-only.
10. Consider real Selected / Filters / Joins tabs because the editor is a dense mode-oriented workbench. If used:
    - use one accessible Tabs primitive;
    - expose counts with shared Badge;
    - keep all three modes reachable by keyboard;
    - do not recreate the stale CSS implementation verbatim.
11. If sequential sections remain clearer after implementation testing, keep them; do not add Tabs solely to satisfy the Watermelon matrix.
12. Audit `sqlEditor.css` against actual rendered class names after the replacement is working.
13. Delete dead selectors for abandoned structures such as old tabs, drag handles, attribute pickers, add panels, filter cards, or join layouts when no current TSX renders them.
14. Consolidate field/button/focus styling with shared primitive styles where semantics are actually identical.
15. Do not create client-side SQL AST/state separate from `SqlModelView`.

### Refactoring/deletion work

Mandatory deletion after parity:

- all `window.prompt`/`prompt()` calls;
- prompt-specific inline callbacks;
- stale SQL selectors not used by current replacement markup;
- duplicated operation file/diagnostic UI already superseded by Step 3;
- obsolete add-panel prototypes left from prior implementations;
- any frontend list of SQL operators/connectors/join types copied from the backend.

### Dependencies

Steps 1–3 are required. Completing Steps 4–8 first reduces simultaneous layout risk.

### Validation

Automated/manual checks:

- add selection with expression and optional alias;
- edit selection;
- move selection up/down;
- remove selection with existing minimum-count constraints preserved;
- add/update/remove filter;
- connector/operator options exactly match `SqlModelView`;
- add/update/remove join and predicates;
- backend error remains visible and does not corrupt current draft;
- stale SQL response still cannot overwrite a newer draft;
- read-only SQL model cannot perform unsupported actions;
- raw SQL remains display-only;
- dialog keyboard/focus behavior passes;
- SQL editor usable at desktop/tablet/phone widths;
- static search finds no `prompt(` in frontend production code;
- selector audit confirms deleted SQL styles are truly unused;
- backend SQL tests remain unchanged/green.

### Expected completion state

This step is finished only when:

- no browser prompt API remains in frontend production code;
- every SQL add action uses controlled accessible form state;
- frontend does not reconstruct SQL semantics;
- backend action/model APIs remain authoritative;
- obsolete SQL CSS for superseded markup is deleted;
- SQL editor behavior is protected by browser regression coverage.

### Commit boundary

Use two small commits if needed:

```text
refactor(ui): replace structured SQL prompt flows
refactor(ui): remove obsolete structured SQL styles
```

If real SQL tabs are added, include them with the first behavior commit rather than a separate cosmetic commit.

---

## Step 10 — Unify feedback, dialogs, loading, and dirty-close behavior

### Objective

Replace the app-wide overloaded status message with explicit durable states and lightweight transient feedback, while ensuring destructive/lossy actions use one dialog path.

### Scope

Likely files/modules:

```text
src/vg2c_ui/frontend/package.json
src/vg2c_ui/frontend/package-lock.json
src/vg2c_ui/frontend/src/main.tsx
src/vg2c_ui/frontend/src/App.tsx
src/vg2c_ui/frontend/src/FileTabs.tsx
src/vg2c_ui/frontend/src/SourceUpload.tsx
src/vg2c_ui/frontend/src/StructuredSqlEditor.tsx
src/vg2c_ui/frontend/src/styles.css
src/vg2c_ui/frontend/src/ui/Dialog.tsx
```

### Implementation work

1. Add `sonner` only now, when production feedback paths will use it immediately.
2. Mount one Toaster at the application root.
3. Define feedback ownership by durability:
   - **inline durable:** validation issues, dependency errors, SQL form errors, upload queue item errors, conflicts;
   - **transient toast:** upload completion, translation completion, successful apply/reload, copy/download-adjacent confirmations if added;
   - **local loading state:** upload item, translation action, validation/apply, SQL mutation.
4. Remove the single app-wide message as the source for unrelated operations.
5. Do not toast every state transition. Avoid duplicate inline + toast text unless the toast provides necessary global awareness.
6. Add dirty-tab close protection using the shared Dialog:
   - clean tab closes immediately;
   - dirty tab requires explicit discard/cancel;
   - validation-only states should follow the actual risk of losing edits, not generic status names.
7. If closing the browser/tab with unsaved document drafts needs protection, add `beforeunload` only while dirty tabs exist and keep the browser-native message behavior.
8. Ensure action buttons expose local truthful progress (`Translating…`, `Applying…`, `Uploading…`) without a global `busy` lock unless operations truly must be mutually exclusive.
9. Standardize empty/loading/error presentation patterns but do not create a universal `StateManager` component.
10. Keep dialogs for actions that need confirmation/form focus. Do not use dialogs for ordinary success messages.

### Refactoring/deletion work

Delete after new paths are active:

- `App` global `message` state;
- `.status-message` output and CSS;
- app-wide `busy` state if all remaining operations have scoped states;
- duplicate ad-hoc confirmation implementations;
- any dialog wrapper introduced by individual features instead of shared Dialog;
- responsive rule that hides status/error text on smaller screens.

### Dependencies

Steps 1–9. Dirty-close uses stable FileTabs; feedback integrates upload/SQL after their replacements exist.

### Validation

- upload errors remain inline per item and are never hidden on mobile;
- translation failure is visible and success is confirmed once;
- validation issues remain durable inline;
- successful apply gives a transient confirmation without masking the new document state;
- dirty tab close dialog traps focus, supports Escape/cancel, and never discards without explicit confirmation;
- clean tab closes with no unnecessary dialog;
- `beforeunload`, if used, exists only while dirty drafts exist;
- no two dialog systems exist;
- no global `message` or `busy` reference remains unless a concrete shared operation still requires it;
- axe checks pass for confirmation dialogs and toasts do not steal focus.

### Expected completion state

This step is finished only when:

- transient feedback has one toast system;
- confirmation/form overlays have one dialog system;
- durable errors remain inline;
- old global status output is deleted;
- dirty edits cannot be silently lost through tab close;
- feedback is visible at all viewport widths;
- there is no duplicate dialog implementation.

### Commit boundary

Prefer two commits:

```text
feat(ui): unify transient feedback and scoped loading states
feat(ui): protect dirty tab close with shared dialog
```

Combine if the changes are small and tightly coupled.

---

## Step 11 — Responsive and accessibility hardening

### Objective

Perform a dedicated pass after feature structure is stable so responsive/accessibility fixes address root layout and semantic behavior rather than being repeatedly patched during earlier migrations.

### Scope

Potentially all changed frontend feature files, with emphasis on:

```text
src/vg2c_ui/frontend/src/styles.css
src/vg2c_ui/frontend/src/sql/sqlEditor.css
src/vg2c_ui/frontend/src/FileTabs.tsx
src/vg2c_ui/frontend/src/CommandPalette.tsx
src/vg2c_ui/frontend/src/SourceUpload.tsx
src/vg2c_ui/frontend/src/Inspector.tsx
src/vg2c_ui/frontend/src/ScriptTree.tsx
src/vg2c_ui/frontend/src/StructuredSqlEditor.tsx
src/vg2c_ui/frontend/src/ui/*
```

`ScriptTree.tsx` should only change if the accessibility review finds a real regression/defect.

### Implementation work

1. Verify/fix the root layout first:
   - `min-width: 0` / `min-height: 0` on actual grid/flex boundaries;
   - intended scroll container owns scrolling;
   - overlays do not create page scroll;
   - horizontal scrolling is limited to intentional regions such as file tabs/tables.
2. Rationalize breakpoints rather than adding new one-off thresholds. Start from the current ranges:
   - desktop > 1100px;
   - compact/tablet ≤ 1100px;
   - stacked tablet behavior around ≤ 780px;
   - phone behavior around ≤ 640px;
   - narrow phone around ≤ 430px.
3. Remove a breakpoint if fluid layout makes it unnecessary; do not add a new breakpoint for one button.
4. Verify keyboard interaction end-to-end:
   - file tabs;
   - `ScriptTree`;
   - command palette;
   - upload dialog/queue/source selection;
   - inspector sheet;
   - SQL tabs/forms/dialogs;
   - dirty-close dialog.
5. Verify focus behavior:
   - visible focus indication;
   - modal initial focus;
   - focus trap;
   - focus return;
   - no focus moved merely because a toast appears;
   - no hidden tree item remains tabbable.
6. Verify semantic labels/names for icon-only controls and abbreviated buttons. Use Tooltip as supplemental help, never as the only accessible name.
7. Verify `aria-live` is used only where live announcement is actually needed; avoid competing live regions with Sonner.
8. Preserve and test `prefers-reduced-motion`; no essential state change should depend on animation.
9. Preserve `prefers-contrast` support and check status indicators are not color-only; accessible text should remain available.
10. Check touch target size on phone for primary controls and close/menu buttons.
11. Run axe against representative states, not only the empty page.
12. Manually inspect zoom at 200% on a desktop viewport and ensure key workflows remain operable.

### Refactoring/deletion work

Delete during this pass:

- breakpoint-specific overrides made obsolete by structural fixes;
- duplicate focus styles superseded by shared primitives;
- CSS icon hacks replaced by accessible Lucide icons where already adopted;
- visually hidden duplicate text that no longer serves accessibility;
- one-off mobile rules that only compensated for old upload/inspector markup.

### Dependencies

Steps 1–10. This pass should operate on the final feature structure.

### Validation

Required viewport matrix:

```text
1440×900
1024×768
768×1024
390×844
320×568
```

Required checks:

- no unintended document-level horizontal scrollbar;
- top-level actions remain reachable;
- file tabs scroll intentionally when necessary;
- operation editor fields do not overflow;
- compact inspector fits and scrolls internally;
- command palette fits phone width/height;
- SQL add forms fit or scroll within dialog;
- upload queue/source list remain usable;
- keyboard-only workflow can upload/browse, translate, navigate tree, edit, preview/apply, open inspector, and close overlays;
- automated axe checks have no serious/critical violations in agreed representative states;
- reduced-motion test does not expose hidden/unclickable state.

### Expected completion state

This step is finished only when:

- all agreed viewport checks pass;
- all overlay focus behaviors pass;
- all main actions are keyboard reachable;
- accessible names exist for icon-only controls;
- status is not conveyed only by color;
- reduced-motion behavior is correct;
- fixes are structural rather than a new collection of breakpoint hacks.

### Commit boundary

One focused commit unless CSS structural cleanup and accessibility behavior need separation:

```text
refactor(ui): harden responsive and accessible workbench behavior
```

---

## Step 12 — Delete obsolete/redundant code, styles, and dependencies

### Objective

Make clean-cut migration explicit: after all replacements pass regression, remove every superseded implementation and temporary compatibility path so the codebase becomes smaller and easier to reason about.

### Scope

All frontend files touched by Steps 2–11, especially:

```text
src/vg2c_ui/frontend/src/App.tsx
src/vg2c_ui/frontend/src/styles.css
src/vg2c_ui/frontend/src/sql/sqlEditor.css
src/vg2c_ui/frontend/src/ContextSidebar.tsx or Inspector.tsx
src/vg2c_ui/frontend/src/OperationEditor.tsx
src/vg2c_ui/frontend/src/StructuredSqlEditor.tsx
src/vg2c_ui/frontend/package.json
src/vg2c_ui/frontend/package-lock.json
```

### Implementation work

1. Run a deliberate static audit for:
   - `prompt(` / `window.prompt`;
   - `.endsWith('.txt')` semantic classification;
   - `startsWith('generated/')` semantic classification;
   - old status-message selectors/state;
   - old hidden upload controls;
   - old multiple source select;
   - old context backdrop;
   - deleted component names/imports;
   - stale SQL class names;
   - temporary aliases/adapter components;
   - unused icon imports;
   - copied Watermelon assets/example helpers.
2. Audit CSS selectors against current rendered/class source. Delete selectors with no production markup consumer unless they are documented global states (`:focus-visible`, media preferences, etc.).
3. Remove old files rather than retaining aliases when a component was renamed/replaced.
4. Audit runtime dependencies:
   - React/ReactDOM;
   - Radix package used by real primitives;
   - cmdk;
   - Sonner;
   - lucide-react;
   - remove anything else added experimentally and no longer imported.
5. Audit dev dependencies similarly; browser/a11y tooling must have real scripts/tests.
6. Run package pruning/lockfile regeneration through the normal npm workflow.
7. Remove dead imports/helpers revealed by the refactor.
8. If `noUnusedLocals`/`noUnusedParameters` can be enabled without unrelated churn, consider enabling them; do not turn this UI refactor into a repository-wide lint migration.
9. Confirm there is no old/new feature flag or compatibility branch for upload, inspector, SQL forms, feedback, or command palette.
10. Compare migrated-area code/CSS size qualitatively and quantitatively. New features may add code, but replaced paths should not retain both implementations.

### Refactoring/deletion work

This step **is** the deletion work. Expected candidates include:

- old top-bar upload labels/inputs and associated CSS;
- old multiple source picker CSS;
- app-wide `message`/`busy` remnants;
- `.status-message`;
- inline App file-tab helper/markup remnants;
- custom inspector backdrop and old compact overlay CSS;
- `ContextSidebar` alias/file if replaced by `Inspector`;
- duplicate file-reference/diagnostic helpers;
- all browser prompt paths;
- unused SQL selectors;
- CSS selectors for deleted markup;
- temporary migration helpers;
- unused dependencies and imports.

### Dependencies

Steps 1–11 must be passing. Do not delete fallback code before replacement parity is demonstrated.

### Validation

- full frontend tests/build pass;
- relevant Python tests pass;
- browser E2E/axe suite passes;
- static searches for known old paths return no production matches;
- package dependency audit shows every runtime dependency has a production import;
- no generated contract drift;
- no deleted CSS class is still referenced;
- no compatibility path is still callable.

### Expected completion state

This step is finished only when:

- each migrated responsibility has one implementation path;
- no obsolete component/import/helper/style remains intentionally “just in case”;
- no unused runtime dependency remains;
- no copied Watermelon demo implementation remains;
- no frontend duplicate of backend domain semantics remains;
- the replaced areas are measurably simpler than an old+new parallel implementation would be.

### Commit boundary

One cleanup commit is preferred so the deletion is easy to review:

```text
refactor(ui): remove superseded UI paths and dead styles
```

If dependency cleanup is substantial, use a second narrow commit:

```text
chore(ui): remove unused frontend dependencies
```

---

## Step 13 — Final maintainability, architecture, and regression review

### Objective

Challenge the completed result against the original goals and remove any abstraction or responsibility that survived only because it was convenient during implementation.

### Scope

All changed frontend/API files and this plan/documentation.

### Implementation work

Review every new module/component/hook with these questions:

1. Does it own meaningful state/semantics/behavior, or is it only forwarding props?
2. Does it have more than one real consumer where reuse was the reason for creating it?
3. Could native HTML plus existing CSS be simpler?
4. Is the state in the narrowest correct owner?
5. Did any domain/backend semantic leak into React string/path parsing?
6. Did any frontend model duplicate generated contracts?
7. Is there exactly one command registry?
8. Is there exactly one remote workspace-file inventory owner?
9. Is there exactly one upload/source-intake workflow?
10. Is there exactly one dialog foundation and one toast system?
11. Is there exactly one inspector content path?
12. Is `ScriptTree` still the domain-specific tree rather than being wrapped/replaced for visual consistency?
13. Did an abstraction become configurable for hypothetical consumers that do not exist?
14. Did any migration adapter or compatibility alias survive?
15. Can a dependency now be removed?
16. Did CSS grow because root causes were patched rather than fixed?
17. Is `App.tsx` primarily composition/orchestration rather than feature implementation?
18. Are `workspaceState.ts` and `useWorkspace.ts` still cohesive rather than accumulating unrelated UI state?

Update this document only if the implemented architecture intentionally differs from the plan. Document the final reason rather than preserving outdated planned structure.

### Refactoring/deletion work

Delete/simplify anything that fails the review above. Do not open unrelated compiler/runtime refactors simply because they are visible during the review.

### Dependencies

Steps 1–12 complete.

### Validation

Run the final gate from a clean checkout/install where practical:

```text
frontend contract check
frontend TypeScript typecheck
frontend reducer/unit tests
frontend production build
browser E2E suite
browser axe checks
relevant Python UI/API tests
relevant SQL editor/domain tests
static legacy/dead-code searches
runtime dependency audit
responsive viewport matrix
manual keyboard/focus smoke test
```

Also verify the final diff for accidental unrelated changes.

### Expected completion state

This step is finished only when:

- no review question identifies an unnecessary surviving abstraction/path;
- all regression gates pass;
- all intended deletions are committed;
- the final dependency set is justified;
- the final architecture matches the responsibility direction at the top of this document;
- the changed frontend is simpler to trace from event → controller → API/state → rendering than before the refactor.

### Commit boundary

Do not create a ceremonial code commit if no code changes are needed. If the review discovers simplifications, use one or more very small cleanup commits, then a documentation-only update if the final architecture description changed.

---

## 4. Expected target frontend structure

This is a target shape, not a requirement to create directories/components that have no real need. Keep the repository flat where that is simpler.

A reasonable end state is approximately:

```text
src/vg2c_ui/frontend/src/
├─ App.tsx                         # orchestration/composition
├─ api.ts                          # typed transport only
├─ contracts.generated.ts          # generated; backend is source
├─ useWorkspace.ts                 # document/editor controller
├─ workspaceState.ts               # document/editor reducer
├─ useWorkspaceFiles.ts            # remote workspace inventory/policy
├─ useSourceIntake.ts              # only if UI + palette share intake state
├─ commands.ts                     # one command derivation/registry
├─ CommandPalette.tsx
├─ SourceUpload.tsx
├─ FileTabs.tsx
├─ ChangeToolbar.tsx
├─ Inspector.tsx
├─ ScriptTree.tsx                  # existing domain-specific tree
├─ OperationEditor.tsx
├─ OperationPresentation.tsx       # shared file refs/diagnostics
├─ StructuredSqlEditor.tsx
├─ operationLabels.ts              # presentation helpers only; no compiler inference
├─ styles.css
├─ sql/
│  ├─ SqlAddDialogs.tsx
│  └─ sqlEditor.css
└─ ui/
   ├─ Dialog.tsx
   ├─ Tooltip.tsx
   ├─ Button.tsx                   # only if real shared variant behavior remains useful
   ├─ Badge.tsx
   └─ Tabs.tsx                     # only if SQL or another real mode switch uses it
```

Do not create `features/`, `controllers/`, `services/`, `providers/`, or additional index/barrel layers merely to imitate a larger frontend architecture. Add structure only when current file ownership becomes clearer because of it.

## 5. Watermelon adoption boundary after repository validation

### Adopt/adapt

- **Command Search:** interaction model for Ctrl/Cmd+K, implemented with `cmdk` and current application actions.
- **Button / Button Group:** consistent sizing/variants/grouping where current controls repeat the same semantics.
- **Badge:** counts/status/metadata where text chips already exist.
- **Tabs:** only for a true mode switch such as dense SQL sections if implementation testing confirms it improves navigation.
- **Tooltip:** icon-only controls and truncated meaning, never as the accessible label itself.
- **Dialog:** SQL add forms and dirty-close confirmation.
- **Sheet:** responsive presentation of the same Dialog infrastructure for the compact inspector; not a separate abstraction.
- **Collapsible:** prefer native `<details>` unless controlled behavior becomes necessary.
- **Form controls:** visual/interaction guidance; keep native controls where sufficient.
- **Sonner:** one transient feedback channel.
- **File Upload:** drag/drop, queue/status, validation, retry interaction model; no copied simulated-progress implementation.
- **Table:** visual guidance only; keep native CSV table.
- **Extended Toolbar ideas:** grouping/hierarchy only; avoid decorative pulse/spring effects.

### Explicitly avoid/defer

- Watermelon generic Tree Menu as a `ScriptTree` replacement;
- marketing/showcase blocks;
- finance/dashboard widgets unrelated to SQLPathFinder;
- decorative shimmer/gooey/morphing controls;
- Motion dependency for ordinary workbench state transitions;
- Tailwind/shadcn migration;
- generic data-grid framework;
- generic Combobox until source counts justify it;
- fake upload percentage/progress;
- copied Watermelon demo state/fixtures.

## 6. Dependency policy

Runtime dependencies should be introduced at the step where they become real production consumers, not all at once.

Expected justified additions, subject to implementation-time verification:

| Dependency | Step | Reason |
|---|---:|---|
| `radix-ui` | 3 | accessible Dialog/Tooltip and possibly Tabs primitives without building focus management manually |
| `lucide-react` | 3 | consistent accessible icon set for compact controls; replaces CSS/text glyph hacks |
| `cmdk` | 6 | command palette filtering/keyboard list behavior |
| `sonner` | 10 | one lightweight transient toast channel |

Expected browser-test dependencies:

| Dependency | Step | Reason |
|---|---:|---|
| `@playwright/test` | 1 | keyboard, focus, responsive, end-to-end workbench regression |
| `@axe-core/playwright` | 1 | automated representative accessibility checks |

Do not keep any of these if the final implementation no longer imports/uses them.

## 7. Out of scope

Unless separately approved, this refactor does not include:

- Supabase/authentication work;
- replacing anonymous workspace/session architecture;
- executing generated workflows;
- router/deep-link architecture;
- persisted user preferences, favorites, or recent-command history;
- backend workspace-file deletion;
- arbitrary command plugins/extensions;
- visual workflow graph replacement for `ScriptTree`;
- a general data-grid system;
- a theme-system redesign;
- unrelated compiler/runtime/CLI refactors;
- stylistic rewriting of unaffected Python code.

## 8. Overall definition of done

The entire refactor is complete only when **all** of the following are true.

### Functionality

- Upload files and folder-relative files works.
- Drag/drop file intake works.
- Supported files are validated against backend-owned policy.
- Failed queued uploads can be retried without confusing successful files.
- Translatable sources are selected using contract metadata, not filename inference.
- Translation still opens the expected generated documents.
- Generated file and workspace ZIP downloads remain available.
- Document tabs switch and close correctly.
- Dirty close cannot silently discard edits.
- Search/expand/collapse/select behavior in `ScriptTree` is preserved.
- Parameter editing, undo, redo, preview, apply, conflict reload, CSV preview, workspace projection, and structured SQL editing continue to work.
- Ctrl/Cmd+K command palette can navigate and execute enabled workbench actions.
- Structured SQL add-selection/filter/join no longer uses browser prompts.

### Maintainability

- `App.tsx` is an orchestration/composition root, not the implementation owner for file inventory, tab keyboard algorithms, upload queue behavior, or reusable feature presentation.
- `workspaceState.ts` remains focused on document/editor state.
- `useWorkspace.ts` remains focused on document/API controller behavior.
- remote workspace-file inventory has one owner.
- source-intake state has one owner if shared by upload UI and commands.
- command derivation has one registry/path.
- shared components exist only for demonstrated reuse or meaningful accessibility/state behavior.
- there are no pass-through wrappers created solely for architectural appearance.

### Redundant-code removal

- old hidden top-bar upload implementation is deleted;
- old multiple-select source picker is deleted;
- old global status message path is deleted;
- custom compact inspector backdrop path is deleted;
- duplicated operation file-reference/diagnostic presentation is deleted;
- browser prompt SQL paths are deleted;
- unused SQL CSS/selectors are deleted;
- CSS for deleted markup is deleted;
- dead imports/helpers are deleted;
- temporary migration aliases/adapters are deleted;
- old and new implementations do not run in parallel.

### Separation of concerns

- compiler/domain semantics are backend-owned;
- workspace policy/file classification is backend-owned;
- Pydantic/generated contracts remain the only domain-model bridge;
- `api.ts` remains transport-focused;
- React does not parse SQL semantics;
- React does not infer source/generated roles from paths/suffixes;
- React does not map compiler scope kinds to user-facing semantics;
- presentation components receive explicit data/actions rather than discovering backend rules.

### Responsive behavior

The full critical workflow is usable at:

```text
1440×900
1024×768
768×1024
390×844
320×568
```

At those sizes:

- no unintended document-level horizontal overflow exists;
- file tabs have intentional contained overflow;
- workbench controls remain reachable;
- inspector uses the intended persistent/right-sheet/bottom-sheet presentation;
- upload queue/source list fits or scrolls correctly;
- command palette fits;
- SQL forms fit or scroll within their overlay;
- root layout fixes, not one-off breakpoint hacks, account for the behavior.

### Accessibility

- `ScriptTree` keyboard behavior remains covered and working;
- command palette is fully keyboard-operable;
- file tabs retain correct tab keyboard semantics;
- dialogs/sheets trap focus, close with Escape, and return focus;
- icon-only controls have accessible names;
- tooltips are supplemental only;
- focus-visible styling remains clear;
- status is not color-only;
- durable errors are exposed in context;
- transient feedback does not steal focus;
- reduced-motion preference is respected;
- representative axe tests report no serious/critical violations;
- a keyboard-only smoke workflow passes.

### Tests/build

From a clean dependency install where practical:

- generated contract check passes;
- TypeScript typecheck passes;
- existing reducer tests pass;
- production frontend build passes;
- browser E2E tests pass;
- axe checks pass;
- relevant Python UI/API tests pass;
- relevant SQL editor/domain tests pass;
- no new warnings are introduced by the changed build/test paths.

### Dependency cleanliness

- every runtime dependency has a real production consumer;
- every browser-test dependency has an executed test/script;
- no Tailwind/shadcn/Motion/react-icons package remains accidentally;
- no unused Watermelon code/assets remain;
- lockfile is current;
- experimental packages are removed before completion.

### No unnecessary compatibility paths

- no feature flag selects old/new upload;
- no `ContextSidebar` alias remains if `Inspector` replaced it;
- no old dialog/backdrop implementation remains;
- no legacy prompt path remains;
- no duplicate command registry remains;
- no compatibility adapter exists without a documented active consumer.

### Simpler than before

The refactor is not complete merely because the UI looks better. The final implementation must make the main workflows easier to trace and maintain:

```text
user action
   ↓
feature component / command
   ↓
focused hook or existing workspace controller
   ↓
typed api.ts call or workspace reducer action
   ↓
backend-owned semantics / explicit state
   ↓
rendered result
```

A final reviewer should be able to confirm that migrated responsibilities have fewer ad-hoc state paths, less duplicated UI/CSS, fewer frontend semantic assumptions, and no old implementation retained beside the new one.

## 9. Recommended commit sequence

The execution should remain reviewable approximately as follows:

```text
1.  test(ui): add browser regression harness
2.  test(ui): characterize current workbench workflows
3.  refactor(api): expose workspace presentation metadata
4.  refactor(ui): add minimal accessible primitives
5.  refactor(ui): deduplicate operation presentation
6.  refactor(ui): isolate workspace file inventory state
7.  refactor(ui): extract file tabs and change toolbar
8.  refactor(ui): simplify workbench root layout
9.  feat(ui): add keyboard command palette
10. refactor(ui): add source intake state and upload queue
11. feat(ui): replace workspace upload and source selection
12. refactor(ui): unify responsive file inspector
13. refactor(ui): replace structured SQL prompt flows
14. refactor(ui): remove obsolete structured SQL styles
15. feat(ui): unify transient feedback and scoped loading states
16. feat(ui): protect dirty tab close with shared dialog
17. refactor(ui): harden responsive and accessible workbench behavior
18. refactor(ui): remove superseded UI paths and dead styles
19. chore(ui): remove unused frontend dependencies          # only if needed
20. docs(ui): update final architecture notes               # only if implementation deviated
```

This sequence is guidance, not a requirement to create twenty commits. Combine adjacent commits when the resulting diff remains small and cohesive; split any step whose behavior and cleanup become too large to review confidently. Never combine unrelated compiler/runtime refactors into these commits.