# SQLPathFinder Frontend Refactoring and UI Improvement Plan

> **Inspection baseline:** `VectorLim/SQLPathFinder_PY_Migration` `main` at `1166cb22c49905c1a71d770e2bb0bf3e4062e871` (`combined sqlite table being empty without column header fix`, 2026-09-14).
>
> **Verification note:** the execution host could not perform a literal network `git pull`, so the repository was inspected directly through the connected GitHub repository at the verified remote `main` HEAD above. The plan commit itself is documentation-only; no UI implementation is included.
>
> **Research inputs reviewed:** `SQLPathFinder_UI_Watermelon_Audit.md`, `SQLPathFinder_UI_Implementation_Plan.md`, and `SQLPathFinder_UI_Component_Matrix.md`. Their recommendations are treated as input and are overridden below where the current codebase supports a simpler or safer design.

## 1. Executive direction

The frontend should remain a small React workbench, not become a general-purpose application framework or a Watermelon showcase. The current code already has a sound core state architecture: backend-owned domain semantics are serialized into generated TypeScript contracts, `workspaceState.ts` owns tab/edit/projection state, and `useWorkspace.ts` mediates asynchronous API operations. Those pieces should be preserved.

The refactor should focus on the actual pressure points:

- make `App.tsx` a composition/orchestration root instead of a mixture of upload logic, shell layout, tabs, toolbars, notifications, dialogs, and editor rendering;
- separate transient upload/file-inventory state from document editing state without introducing a global store;
- replace the crowded top-bar upload flow with a real source intake workflow;
- add a keyboard-first command palette without building a plugin framework;
- keep the domain-specific `ScriptTree` and its existing accessibility behavior;
- replace browser `prompt()` flows in `StructuredSqlEditor` with controlled accessible forms;
- remove duplicated operation metadata/diagnostic UI;
- fix transient feedback so it is not hidden on narrower screens;
- convert the existing context panel into an accessible responsive inspector while preserving its data-flow functionality;
- remove obsolete CSS and old UI paths immediately after replacements are verified;
- keep styling source-owned and CSS-based rather than migrating the application to Tailwind/shadcn.

Target responsibility flow remains:

```text
compiler/domain semantics
        ↓
FastAPI contracts + serialization
        ↓
generated TypeScript contracts + api.ts
        ↓
controller hooks / reducer state
        ↓
feature components
        ↓
small shared UI primitives
```

No frontend component should rediscover compiler semantics that can be expressed by the backend contract.

---

## 2. Current-state assessment

### 2.1 Frontend structure and component hierarchy

The current React entry point is intentionally small:

```text
main.tsx
└─ App
   ├─ top bar / upload / translation / downloads
   ├─ translated-file tabs
   └─ workspace
      ├─ editor pane
      │  ├─ editor toolbar
      │  ├─ change toolbar
      │  ├─ ScriptTree
      │  │  └─ selected step → OperationEditor
      │  │      ├─ GenericOperationEditor
      │  │      └─ capability: structured-sql → StructuredSqlEditor
      │  ├─ ChangePreview
      │  └─ Diagnostics
      └─ ContextSidebar
         ├─ Data Flow
         ├─ CSV preview
         └─ File Details
```

There is no frontend router. Vite proxies `/api` in development and emits the built application into `src/vg2c_ui/static`; FastAPI mounts that static application at `/`. A router is not required for the current single-workspace application and should not be introduced as part of this refactor.

### 2.2 State ownership

The strongest part of the frontend is the document/workspace editing state.

`workspaceState.ts` owns:

- open document tabs and active tab;
- selected tree item and expanded scopes per tab;
- draft parameter values;
- undo/redo history;
- document mutation status;
- validated change preview;
- request ownership/stale-response protection;
- CSV preview request state;
- projected multi-document dependency state.

`useWorkspace.ts` is a focused controller around that reducer. It owns API orchestration for translation/open/reload/edit/validate/apply/CSV/structured-SQL operations and rejects stale asynchronous SQL responses before committing them. This is appropriate separation and should remain the application state backbone.

`App.tsx` currently owns unrelated shell/transient state together:

- workspace file inventory;
- selected source paths;
- operation search;
- a single app-wide `busy` flag shared by upload and translation;
- a single `message` string used for success, progress, and errors;
- batch translation diagnostics;
- context panel open/closed state.

This is the main ownership problem. The solution is **not** Redux/Zustand/context-heavy architecture. Instead, move only coherent transient responsibilities into feature-level hooks/components.

### 2.3 Backend/frontend boundary

The backend boundary is already well-designed and should be preserved:

- Pydantic API models are the source of truth;
- `scripts/generate_frontend_contracts.py` generates `contracts.generated.ts`;
- frontend builds verify generated contracts are current;
- `api.ts` is a thin typed transport layer;
- `serialization.py` explicitly serializes compiler-owned semantics instead of asking the frontend to infer them;
- structured SQL mutation is backend-owned through `/api/sql/*` and the frontend only renders and submits structured actions;
- workspace projection/data-flow semantics are backend-owned.

This means the UI refactor should **not** create parallel TypeScript domain models, client-side SQL parsing, dependency inference, operation classification, or new frontend business services.

### 2.4 Styling and responsive behavior

The existing CSS is more mature than the previous Watermelon plan assumed:

- semantic-ish design tokens already exist for text, surfaces, borders, accent, success, warning, danger, radii, and panel shadows;
- focus-visible styling is global;
- reduced-motion handling already exists;
- increased-contrast handling already exists;
- the context panel already changes from persistent desktop sidebar → right sheet → bottom sheet as viewport width decreases;
- the tree and operation editor already have usable dense workbench styling.

The problem is not lack of Tailwind. The problem is that `styles.css` has accumulated feature-specific control rules and `sql/sqlEditor.css` contains a large set of selectors for UI structures that the current `StructuredSqlEditor.tsx` no longer renders.

The refactor should preserve the existing CSS/token system and remove dead/duplicated rules. It should not introduce a second styling language.

### 2.5 Current upload workflow

Current behavior in `App.tsx`:

1. hidden file input or folder input returns a `FileList`;
2. all selected files are sent in one `FormData` request through `uploadWorkspaceFiles()`;
3. browser-relative folder paths are preserved with `webkitRelativePath`;
4. workspace files are refreshed after success;
5. uploaded `.txt` files are appended to the source selection;
6. source files are identified client-side by `.txt` suffix;
7. generated outputs are identified client-side by `generated/` prefix;
8. a global `busy` flag and message string communicate progress.

Backend behavior is authoritative and more specific:

- uploads are workspace-scoped;
- paths are sanitized server-side;
- allowed upload suffixes are defined by `WorkspaceManager`;
- per-file, file-count, and workspace-size limits are server-owned;
- duplicate paths fail because files are created exclusively;
- files are written one at a time within the request;
- a later failure can occur after earlier files in the same HTTP request were already saved;
- the current API does not expose byte-level upload progress or workspace-file deletion.

Therefore the new UI must not simulate percentages or claim atomic multi-file behavior that does not exist.

### 2.6 Existing behavior worth preserving

Preserve these areas unless a regression test demonstrates a problem:

- `workspaceState.ts` reducer and stale-request ownership checks;
- `useWorkspace.ts` controller role;
- generated backend→TypeScript contracts;
- `CAPABILITY_EDITORS` in `OperationEditor.tsx` as a small, useful capability dispatch seam;
- `ScriptTree` domain structure, search behavior, ARIA tree/treeitem roles, roving focus, and arrow/Home/End navigation;
- search-driven temporary tree expansion without mutating saved expansion state;
- backend structured SQL inspection/action APIs;
- native semantic table for CSV preview;
- durable diagnostics inline rather than as transient notifications;
- desktop data-flow inspector and its projection-aware upstream/downstream behavior;
- CSS reduced-motion and contrast accommodations;
- single-page application structure with no router.

### 2.7 Concrete maintainability problems and duplication

#### `App.tsx` mixes too many responsibilities

It currently owns transport orchestration, transient feedback, upload selection, download links, tabs, keyboard shortcuts, toolbars, change preview, diagnostics, and responsive context-panel coordination. This makes shell changes risky because unrelated behavior lives in the same render function.

#### Global `busy` is too coarse

Upload and translation use one boolean. Document mutation already has richer per-tab statuses. The new design should use operation-specific busy state rather than adding a bigger global status object.

#### Global `message` is overloaded and becomes invisible

`message` is used for loading, success, and failure. CSS hides `.status-message` below the desktop breakpoint, which means important upload/translation errors can disappear exactly where responsive behavior matters most.

#### Browser `prompt()` is an obsolete UI path

`StructuredSqlEditor` uses `prompt()` for adding selections, filters, and joins. These flows are not composable, are difficult to validate/test, provide weak accessibility, and cannot show field-level errors.

#### Duplicate operation metadata UI

`OperationEditor.tsx` and `StructuredSqlEditor.tsx` each implement file-reference chips and dependency-diagnostic rendering. These should become small shared feature components because the semantics are identical and already reused.

#### Redundant `<details>` state

`ContextSidebar.Panel` mirrors the native `<details open>` state with React `useState` even though no other component consumes that state. Use `defaultOpen` and let the semantic element own it unless controlled state becomes necessary.

#### Frontend reconstructs some workspace classification

`App.tsx` currently infers source files from `.txt` and generated files from `generated/`. That classification belongs in the workspace API if the UI depends on it.

#### Frontend maps scope kinds to domain-facing names

`formatScopeLabel()` understands `if`, `if-branch`, `else-branch`, `macro`, and `loop`. The backend already provides `ScopeView.label`; user-facing domain naming should be serialized there so React does not need to interpret compiler scope kinds.

#### `sqlEditor.css` has strong signs of dead/abandoned UI

The stylesheet contains selectors for structures such as SQL tabs, expression shells, drag handles, attribute pickers, add panels, filter cards, and join layouts that are not rendered by the current `StructuredSqlEditor.tsx`. Implementation should run a selector/reference audit, preserve only selectors reached by the replacement UI, and delete the obsolete remainder after parity is established.

#### Repeated styling rules

Buttons, form fields, focus states, errors, chips, and compact labels are styled independently in global and SQL CSS. Consolidate only the patterns with actual reuse; do not create a component wrapper for every HTML element.

---

## 3. Target frontend architecture

### 3.1 State and responsibility map

| Responsibility | Owner after refactor | Notes |
|---|---|---|
| document tabs, edits, undo/redo, validation/apply, selected tree item, projection, CSV | `workspaceState.ts` + `useWorkspace.ts` | Preserve existing architecture. |
| remote workspace-file inventory | new `useWorkspaceFiles.ts` | Fetch/refresh only; no domain semantics. |
| selected translation source paths | `App.tsx` or cohesive source-controls state | Shell-level user selection, not global app state. |
| staged upload queue | `SourceUpload.tsx` | Ephemeral local state; removed on close/clear as appropriate. |
| translation busy + translation diagnostics | source/translation feature owner | Do not reuse upload busy state. |
| tree search | workbench/editor owner | Keep independent from global command palette. |
| command palette query/selection | `CommandPalette.tsx` / `cmdk` | Command list is derived from existing state/callbacks. |
| inspector open state | `App.tsx` | One parent-controlled source of truth. |
| SQL add-form drafts | `StructuredSqlEditor.tsx` subforms | Local presentation state only. |
| transient success/failure notification | Sonner | Never replace persistent diagnostics/conflicts with a toast only. |

No new global state library is warranted.

### 3.2 Proposed file organization

Do not reorganize the whole frontend. Keep existing domain components where they are and add only files with clear ownership:

```text
src/vg2c_ui/frontend/src/
├─ components/
│  └─ ui/
│     ├─ Button.tsx
│     ├─ Badge.tsx
│     ├─ Dialog.tsx
│     └─ Tooltip.tsx
├─ App.tsx
├─ CommandPalette.tsx
├─ commandItems.ts
├─ FileTabs.tsx
├─ SourceUpload.tsx
├─ WorkspaceHeader.tsx
├─ ChangeToolbar.tsx
├─ ContextSidebar.tsx
├─ OperationEditor.tsx
├─ OperationShared.tsx
├─ ScriptTree.tsx
├─ StructuredSqlEditor.tsx
├─ useWorkspace.ts
├─ useWorkspaceFiles.ts
├─ useMediaQuery.ts          # only if needed for semantic desktop/sidebar vs modal-sheet switch
├─ workspaceState.ts
├─ api.ts
├─ styles.css
└─ sql/sqlEditor.css
```

This is intentionally smaller than the earlier proposed `components/ui` + `components/workbench` hierarchy. Do **not** create generic `Select`, `Checkbox`, `Textarea`, `Table`, `Collapsible`, `Sheet`, or `Combobox` files until at least two real consumers need behavior beyond native HTML plus shared CSS.

### 3.3 App after refactor

`App.tsx` should primarily:

- instantiate `useWorkspace()` and workspace-file inventory;
- own shell-level selection/open state;
- compose header, tabs, editor, inspector, and command palette;
- define orchestration callbacks that combine existing state operations, e.g. “go to operation” = select + expand ancestors + restore focus;
- pass domain data down rather than duplicate it.

It should no longer implement file-input markup, tab keyboard handling, toast message text, SQL form behavior, or full inspector markup inline.

### 3.4 API/domain boundary invariant

The frontend may format data for presentation, but must not infer compiler behavior.

Allowed frontend derivations:

- basename/ellipsis formatting;
- grouping commands by UI category;
- counting diagnostics/edits;
- filtering already-provided operations by label;
- displaying artifact relationships already supplied by the projection contract.

Move to backend/API contract when needed:

- whether a workspace file is translatable;
- whether a file is generated vs uploaded input;
- accepted upload suffixes and configured upload limits when the UI displays/pre-validates them;
- user-facing scope labels if they depend on compiler scope kinds.

---

## 4. Watermelon recommendations validated against the current code

### 4.1 Adopt or strongly adapt

| Watermelon candidate | Decision | SQLPathFinder use |
|---|---|---|
| Command Search | **Adopt interaction model** | `Ctrl/Cmd+K` global file/operation/action palette. Use restrained project styling. |
| Button / Button Group | **Adopt locally** | Shared variants and grouped toolbar actions. |
| Badge | **Adopt locally** | Dirty/valid/error/read-only/status/count indicators. |
| Dialog | **Adopt behavior** | SQL add forms and dirty-close confirmation. |
| Tooltip | **Adopt behavior** | Icon-only actions and shortcuts. |
| Sonner | **Adopt** | Transient success/error/info feedback. |
| File Upload block | **Adopt interaction pattern** | Drag/drop, queue, server-backed validation, per-file state, retry. Do not copy simulated upload logic. |
| Dropdown Menu | **Adopt only where needed** | Consolidate generated downloads/secondary workspace actions if header remains crowded. |
| Extended Toolbar | **Use composition ideas only** | Group primary vs history vs secondary actions; no morphing toolbar. |

### 4.2 Adapt with domain-specific behavior preserved

| Candidate | Decision |
|---|---|
| Tabs | Preserve the existing closeable file-tab semantics and keyboard behavior; extract them rather than replacing with a generic animated tab. Use accessible Tabs separately only where a true mode switch exists, such as Structured SQL sections. |
| Sheet | Use the Dialog primitive styled as a sheet for the compact inspector instead of adding a second independent Sheet abstraction. |
| Collapsible | Keep native `<details>/<summary>` where it already works; standardize styles rather than introducing a dependency. |
| Form controls | Keep native inputs/selects/checkboxes/textareas with shared styles unless a control needs richer behavior. |
| Table | Keep semantic HTML tables for CSV preview. A data-grid dependency is not justified. |
| Labeled Progress Indicator | Use only an indeterminate uploading state with the current API. Do not display fabricated percentages. |

### 4.3 Explicitly preserve existing SQLPathFinder implementations

- `ScriptTree` stays domain-specific and keeps its tree semantics.
- `workspaceState.ts` and `useWorkspace.ts` remain the state/controller backbone.
- `CAPABILITY_EDITORS` remains the extension seam for operation-specific editors.
- data-flow projection and structured SQL semantics remain backend-owned.
- native CSV table remains.

### 4.4 Explicitly avoid/defer

Do not adopt:

- Watermelon Tree Menu as a `ScriptTree` replacement;
- Tailwind/shadcn migration solely to consume Watermelon examples;
- Motion for decorative transitions;
- `react-icons` in addition to another icon set;
- Quick Switcher unless a real two-mode workflow appears;
- shimmer/gooey/wiggling/carousel/card-showcase patterns;
- marketing blocks, bento layouts, dashboards, hero/CTA composition;
- generic DataTable/data-grid packages;
- a command registration/plugin framework;
- a global application state framework;
- React Router without an actual deep-link/navigation requirement.

### 4.5 Dependency plan

#### Runtime dependencies to introduce

| Dependency | Reason |
|---|---|
| `radix-ui` | Accessible unstyled Dialog/AlertDialog/Tooltip/DropdownMenu/Tabs primitives with managed focus and keyboard behavior. The current Radix documentation recommends the unified tree-shakeable package to avoid duplicated primitive versions. Use only the primitives actually needed. |
| `cmdk` | Keyboard-first command list, grouping/filtering, arrow navigation, empty state, and selection without creating a custom command-menu framework. Render `Command` inside the project dialog styling rather than importing Watermelon motion. |
| `sonner` | Small React toast layer for transient feedback; removes the overloaded/hideable global status message. |
| `lucide-react` | One consistent icon set for close, search, chevrons, warning, undo/redo, menu, upload, etc. Replace text/CSS glyphs as components are touched; do not add `react-icons`. |

#### Dev dependencies to introduce for regression coverage

| Dependency | Reason |
|---|---|
| `vitest` | Fast React/component unit tests integrated with Vite. |
| `@testing-library/react` | Test behavior/semantics instead of implementation details. |
| `@testing-library/user-event` | Keyboard/pointer interaction tests. |
| `jsdom` | Component DOM environment. |
| `@playwright/test` | Representative browser-level workflows and responsive/focus behavior. |
| `@axe-core/playwright` | Accessibility regression checks on representative screens. |

Do not add all runtime primitives up front and then search for uses. Add the dependency set with the first concrete consumers and keep lockfile changes reviewable.

---

## 5. Command palette design

### 5.1 Ownership

Create `commandItems.ts` as a pure derivation layer, not a registry service.

```ts
export interface CommandItem {
  id: string
  group: 'Files' | 'Operations' | 'Actions' | 'Navigation'
  label: string
  keywords?: string[]
  shortcut?: string
  disabled?: boolean
  run: () => void
}
```

The builder receives current state plus callbacks from `App`; it does not import the reducer, mutate state directly, call fetch, or discover commands through global registration.

A future command is added by adding one entry/builder branch. Do not create provider/plugin/registration APIs unless independently loaded extensions become a real product requirement.

### 5.2 Initial command groups

**Files**

- activate each open translated file;
- optionally focus/select an uploaded translatable source in the source picker.

**Operations**

- one command per operation/scope in the active document;
- label/keywords come from existing display data;
- executing uses the existing selection/ancestor-expansion behavior and restores focus to the selected tree item.

**Actions**

- Translate selected sources;
- Undo;
- Redo;
- Preview changes;
- Apply changes;
- Reload after conflict;
- Expand all scopes;
- Collapse all scopes;
- open Add Sources;
- download current generated file/workspace archive when available.

**Navigation**

- focus operation search;
- focus ScriptTree;
- open File Context/Inspector;
- focus diagnostics/change preview when present.

### 5.3 Keyboard behavior

- `Ctrl/Cmd+K`: open and focus the palette input;
- arrows: move selection through results;
- Enter: execute selected command;
- Escape: close;
- query filters immediately across label/keywords;
- closing without action restores prior focus;
- executing a navigation command moves focus to the destination rather than back to the trigger;
- palette shortcuts must not interfere with existing Ctrl/Cmd+Z/Y/S behavior.

Keep tree search as local operation filtering. The palette is global navigation/actions, not a replacement for in-tree filtering.

### 5.4 Visual treatment

Borrow Watermelon Command Search’s grouping/density only. Use existing neutral surfaces, current blue accent, small icons, and short CSS opacity/transform transitions under `prefers-reduced-motion: no-preference`. No backdrop-heavy blur or spring animation.

---

## 6. Upload/source workflow redesign

### 6.1 Split remote inventory from staged queue

Add `useWorkspaceFiles.ts` for server state:

- `files`;
- `refresh()`;
- initial loading/error state if needed.

Keep staged queue state in `SourceUpload.tsx` because it is temporary presentation/workflow state.

Do not merge uploaded-file inventory into `workspaceState.ts`; document editing state and remote file intake have different lifecycles.

### 6.2 Small backend contract improvement

Before building richer upload UI, remove the React path/suffix inference that currently classifies workspace files.

Extend `WorkspaceFileView` with server-derived metadata such as:

```py
role: Literal["source", "data", "generated"]
translatable: bool
```

Add a small read-only workspace capabilities response if the uploader is expected to preflight/display exact constraints:

```py
class WorkspaceCapabilitiesView(BaseModel):
    allowed_upload_suffixes: list[str]
    max_upload_bytes: int
    max_file_count: int
    max_workspace_bytes: int
```

Expose it from `/api/workspace/capabilities`. Values come directly from `WorkspaceManager`; the server remains the final validator. This prevents a hard-coded React copy of suffixes/limits.

Update generated contracts rather than creating handwritten TypeScript duplicates.

### 6.3 Queue model

A staged upload item needs only UI state:

```ts
type UploadState = 'staged' | 'uploading' | 'done' | 'error'

interface UploadItem {
  id: string
  file: File
  relativePath: string
  state: UploadState
  error?: string
}
```

Use stable identity from staged relative path plus file metadata as needed. Reject duplicate staged relative paths before upload and show the problem inline.

### 6.4 Upload execution

For truthful per-file success/failure with the existing backend, submit queue items individually through the existing `/api/workspace/files` endpoint (`uploadWorkspaceFiles([file])`) rather than sending the entire queue in one opaque request.

Start sequentially. The backend has workspace-wide count/byte limits, and sequential submission avoids unnecessary races while remaining fast enough for the current maximum file count. Introduce bounded concurrency only if measured upload performance requires it.

Per item:

```text
staged → uploading → done
                   ↘ error
```

After queue completion or failure:

- refresh workspace inventory even if one item failed, because prior uploads may have succeeded;
- automatically add successful `translatable` files to the selected source set without using suffix logic;
- allow retry of failed items individually;
- allow removal of staged/failed items from the queue;
- allow clearing completed queue rows.

Do **not** label clearing a completed queue row as deleting an uploaded workspace file. The backend has no delete endpoint today. Workspace-file deletion should be a separate feature/API only if product requirements call for it.

### 6.5 Progress semantics

The current `fetch` upload path exposes no trustworthy byte-level percentage. Show:

- queued;
- uploading with indeterminate activity;
- done;
- error.

Do not copy Watermelon’s simulated percentage timer. If true upload percentage becomes important later, change transport deliberately (e.g. supported XHR/progress path) and treat that as a separate feature.

### 6.6 Drag/drop and folder behavior

- one drop zone accepts files from drag/drop and regular selection;
- retain a distinct “Choose folder” action because folder selection relies on `webkitdirectory`;
- preserve `webkitRelativePath` exactly when present;
- show relative path, size, status, and failure reason;
- keyboard users must be able to trigger both file and folder selection without drag/drop;
- dragging is an enhancement, never the only intake path.

### 6.7 Source selection

Replace the cramped `<select multiple>` with a clear source-selection list/popover using server-provided `translatable` metadata. Start with checkbox rows and counts; do not add a searchable Combobox until source counts demonstrate that scrolling is insufficient.

---

## 7. Workbench and inspector redesign

### 7.1 Preserve `ScriptTree`

The tree is not a target for component-library replacement.

Only make changes required for integration:

- stable focus destination for command-palette navigation;
- shared Button/Badge/icon styling where it does not alter semantics;
- visual cleanup that preserves `role="tree"`, `role="treeitem"`, roving focus, selection, expansion, and existing key behavior.

Do not convert it into accordion cards or Watermelon Tree Menu level transitions.

### 7.2 File tabs

Extract inline tab rendering and keyboard handling from `App.tsx` into `FileTabs.tsx`.

Preserve:

- tablist/tab roles;
- ArrowLeft/ArrowRight/Home/End behavior;
- active/dirty/error status;
- close action;
- focus transfer.

Add dirty close protection:

- clean tab closes immediately;
- dirty tab opens a confirmation alert dialog;
- confirm discards local draft and closes;
- cancel returns focus to the close control/tab;
- add `beforeunload` protection if any tab is dirty so browser refresh/close cannot silently discard edits.

Do not replace the closeable domain tabs with a generic tab primitive that cannot express these behaviors.

### 7.3 ContextSidebar → responsive inspector

Keep `ContextSidebar.tsx` and its domain content rather than creating a parallel Inspector implementation.

Refactor into reusable content plus responsive containers:

```text
ContextSidebar
├─ desktop: persistent <aside>
└─ compact: Radix Dialog content styled as sheet
   ├─ right sheet on tablet
   └─ bottom sheet on narrow phone
```

Use one `InspectorContent` subtree so data-flow/file-details logic is not duplicated.

A small `useMediaQuery` hook is justified here because the semantic container changes from persistent complementary content to modal dialog; CSS alone cannot provide correct focus trapping/inert behavior.

After the compact Dialog/Sheet path is verified, delete the old custom full-screen backdrop/button/focus behavior.

### 7.4 Inspector information hierarchy

Keep two primary sections:

1. **Data Flow** — dependency issues, required inputs, upstream files, produced files, downstream files, CSV preview.
2. **File Details** — source/generated path, revision, operation count, diagnostics count.

Native `<details>` is sufficient for section disclosure. Remove React state mirroring the element’s `open` state; use `defaultOpen` where appropriate.

CSV remains a native semantic table with sticky header and scroll container. Do not introduce DataTable unless sorting/filtering/pagination become actual requirements.

### 7.5 Operation editor cleanup

Preserve `CAPABILITY_EDITORS` and the generic editor fallback.

Extract only genuine duplicated UI:

- `FileReferences` / file-chip presentation;
- `OperationDiagnostics` / dependency-issue presentation.

Avoid a large `OperationEditorFrame` with dozens of layout props merely to remove a few header lines. If header markup remains trivially duplicated after the shared content extraction, leave it until a third use justifies a component.

Native form elements can share `.control`, `.field`, `.field-error`, etc. styling without requiring a React wrapper for each element.

### 7.6 Structured SQL editor

This is the highest-value editor-specific cleanup.

Preserve:

- backend inspection via `inspectSql`;
- backend mutation via `runSqlAction`;
- stale-response safeguards in `useWorkspace`;
- `CommitInput`-style local draft before mutation where appropriate;
- raw SQL as read-only presentation.

Replace all browser `prompt()` paths with controlled forms:

**Add selection dialog**

- expression required;
- alias optional;
- submit `add-selection`.

**Add filter dialog**

- left expression;
- operator from backend `filter_operators`;
- right expression;
- connector from backend `logical_connectors` when required;
- submit `add-filter`.

**Add join dialog**

- join type from backend `join_types`;
- source;
- left key;
- operator;
- right key;
- submit `add-join`.

Use server-provided options; do not hard-code SQL operators/join types in the frontend.

Use accessible Tabs for `Selected`, `Filters`, and `Joins` if all three sections are present, with item counts in tab labels. This is a genuine mode switch and a better fit for Tabs than the closeable document tabs. Raw SQL remains a separate collapsible section below the structured editor.

Each dialog owns temporary draft values and only calls the backend action on submit. Errors from the action remain visible in the editor/dialog as appropriate; do not reduce mutation errors to a toast only.

### 7.7 Dead SQL CSS migration

Before editing `sqlEditor.css`, generate/record a selector usage list against current and new TSX. Then:

1. establish current structured SQL interaction tests;
2. implement the controlled form/tab replacement;
3. verify interaction parity;
4. remove selectors no longer referenced by the new UI;
5. merge duplicated button/field/error styles into global shared control classes;
6. keep only SQL-specific layout rules in `sqlEditor.css`.

Do not preserve unused selectors “just in case”. Git history is the fallback.

---

## 8. Feedback and interaction model

### 8.1 Replace the global message string

The current `message` string combines loading, success, and failure, and is hidden by responsive CSS. Replace it with state at the point of action plus transient toasts.

Examples:

- Uploading → visible queue-item state;
- Translating → Translate button/section busy state;
- Validation/apply → existing per-tab status/change toolbar;
- Upload succeeded → short toast;
- Translation completed → short toast plus durable translation diagnostics if any;
- Apply succeeded → short toast;
- unexpected API failure → toast plus inline error if user must act on it;
- conflict → persistent change-toolbar state and Reload action, optionally announced once by toast.

After migration, delete `message`, `.status-message`, and message-setting wrappers that exist only for the old global string.

### 8.2 Toast rules

Use Sonner only for transient acknowledgement. Never rely on a disappearing toast for:

- validation issues;
- dependency errors;
- conflict resolution;
- upload item failures that need retry;
- form field errors;
- read-only reasons.

### 8.3 Tooltips

Use tooltips only on icon-only actions or where the shortcut is useful. Every icon button still needs an `aria-label`; tooltip text is supplemental, not its accessible name.

### 8.4 Dialogs and destructive actions

Use Dialog/AlertDialog for:

- dirty tab close;
- structured SQL add forms;
- any future explicit discard action.

Do not add confirmation dialogs for reversible, low-risk actions such as expand/collapse or selecting a file.

### 8.5 Motion

Use CSS transitions already gated by reduced-motion preference. No Motion dependency is planned.

Allowed:

- short overlay fade;
- sheet slide;
- small selected-tab indicator transition;
- tree expansion already present;
- subtle toast transition supplied by Sonner.

Avoid layout-changing morphs, shimmer, springs on every item, and decorative movement in the editor.

---

## 9. Responsive and accessibility plan

### 9.1 Root layout instead of breakpoint accumulation

Refactor the header into stable regions rather than continuing to patch `.translate-box` at each breakpoint:

```text
[Brand]
[Source / Add Sources / Translate]
[Command / Downloads / utility actions]
```

Use wrapping/grid minmax behavior so controls naturally reflow. Keep breakpoints only for real mode changes:

- desktop vs modal inspector;
- right-sheet vs bottom-sheet inspector;
- multi-column vs single-column form layout.

Remove CSS-generated pseudo-icons such as the compact context symbol once Lucide controls exist.

### 9.2 Keyboard requirements

Regression-gate:

- complete `ScriptTree` navigation;
- file tab arrow/Home/End behavior;
- Ctrl/Cmd+Z/Y/S existing behavior;
- Ctrl/Cmd+K palette;
- Escape closes topmost dialog/palette/sheet;
- command palette arrow/Enter navigation;
- Dialog/AlertDialog focus trap and focus return;
- Dropdown Menu arrow/typeahead behavior supplied by Radix;
- SQL forms are fully operable without a mouse;
- drag/drop has equivalent file/folder buttons.

### 9.3 Focus management

- opening palette focuses search;
- palette close restores prior focus;
- command navigation focuses its destination;
- compact inspector focuses its heading/first meaningful control and restores trigger focus on close;
- dirty-close dialog restores focus on cancel;
- newly added SQL items receive focus or an announcement after successful creation;
- validation failures focus or identify the first actionable error when appropriate.

### 9.4 Semantic controls

Continue using native controls wherever possible. Do not replace semantic buttons/inputs/tables/details with clickable divs for styling convenience.

### 9.5 Reduced motion and contrast

Preserve existing `prefers-reduced-motion` and `prefers-contrast` handling. New transitions/components must respect them. Radix/command/tooltip content should use the same project tokens and focus ring.

### 9.6 Responsive verification sizes

At minimum test representative browser widths around:

- 1440/1280 desktop;
- 1024 tablet / compact inspector threshold;
- 768 small tablet;
- 390 phone.

Validate scroll containment, no clipped toolbar actions, dialog/sheet reachability, CSV horizontal scrolling, tree indentation, and form usability rather than relying only on screenshots.

---

## 10. Testing and regression gates

### 10.1 Keep existing tests

Preserve and continue running:

- generated contract check;
- TypeScript build/typecheck;
- `workspaceState.test.ts` stale-response/edit-history checks;
- backend UI/workspace tests;
- SQL editor/backend tests;
- broader project test suite appropriate to touched backend contracts.

### 10.2 Add React characterization tests before refactoring

Use Vitest + Testing Library to capture behavior rather than markup snapshots.

**ScriptTree**

- correct tree/treeitem roles;
- selected item roving tabindex;
- Up/Down/Home/End;
- Right expands / enters child;
- Left collapses / focuses parent;
- search shows matching items and ancestors;
- selecting an operation shows its editor;
- dependency-error semantics remain.

**File tabs**

- activate by click and keyboard;
- close clean tab;
- dirty tab requires confirmation after refactor;
- focus moves predictably after close.

**Workspace state**

- retain current reducer tests;
- add any new actions only if state genuinely belongs there.

**Structured SQL**

- initial inspect request;
- stale model handling remains in controller;
- update actions emit server-defined action names/arguments;
- add selection/filter/join forms submit once with expected arguments;
- errors remain visible;
- no `window.prompt` usage after migration.

**Upload**

- file chooser and folder paths stage correctly;
- drag/drop stages equivalent items;
- capability-based preflight displays relevant errors without replacing server validation;
- sequential item success/failure state;
- retry failed item;
- refresh after partial failure;
- successful translatable files become selectable;
- no fake progress percentage.

**Command palette**

- Ctrl/Cmd+K opens;
- grouping/filtering;
- arrow navigation and Enter execute;
- disabled commands cannot execute;
- Escape closes;
- file activation and operation navigation call existing actions;
- focus restoration/destination behavior.

### 10.3 Browser-level E2E

Use Playwright for a small number of representative workflows, not every component state.

**Workflow A — intake and translate**

```text
open app
→ add source/data files
→ verify upload queue completion
→ select sources
→ translate
→ verify translated tabs and generated download availability
```

**Workflow B — edit and apply**

```text
open translated document
→ navigate ScriptTree by keyboard
→ edit parameter
→ undo / redo
→ preview changes
→ apply
→ verify clean state
```

**Workflow C — structured SQL**

```text
select structured SQL operation
→ open add filter/join form
→ submit
→ verify updated model
→ preview/apply document changes
```

**Workflow D — command palette**

```text
Ctrl/Cmd+K
→ search operation/file
→ Enter
→ verify activation/focus
→ invoke one safe action
```

**Workflow E — compact layout**

```text
set tablet/phone viewport
→ open inspector sheet
→ tab through controls
→ close with Escape
→ verify focus returns
→ verify upload/source controls remain reachable
```

### 10.4 Accessibility checks

Run Axe on representative desktop and compact workbench states. Also manually verify keyboard behavior because automated scans do not validate application-specific tree/tab behavior.

Regression gate should include:

- no unlabeled interactive controls;
- one visible focus indicator;
- dialog title/description semantics;
- no focus escaping modal sheet/dialog;
- no keyboard-inaccessible drag/drop action;
- status changes announced or persistently visible;
- reduced-motion behavior;
- color not used as the sole state indicator.

### 10.5 Avoid brittle tests

Do not make pixel snapshots or large DOM snapshots the primary gate. Prefer state, role, accessible name, keyboard action, API-call, and focus assertions.

---

## 11. Implementation phases

Each phase should be independently reviewable and leave the application in a valid state. Do not start the next phase with an intentionally permanent “temporary” parallel UI.

### Phase 0 — baseline and characterization

**Purpose:** lock down behavior before UI extraction.

**Files/modules affected**

- `src/vg2c_ui/frontend/package.json`
- `src/vg2c_ui/frontend/vite.config.ts` or dedicated Vitest config
- new frontend test setup/files
- existing `workspaceState.test.ts`
- no production UI behavior changes.

**Add**

- Vitest/Testing Library/user-event/jsdom setup;
- characterization tests for ScriptTree, file tabs/current App shell where practical, StructuredSqlEditor core actions;
- a lightweight checklist of current manual upload/translation behavior.

**Remove**

- nothing.

**Dependencies**

- test-only dependencies listed above.

**Regression checks**

- existing `npm test` / build;
- relevant pytest UI/SQL suites;
- new characterization tests.

**Completion criteria**

- tree keyboard behavior and reducer stale-response behavior are covered before structural changes;
- current SQL inspect/action path is covered;
- current upload/translation request shape is documented/tested.

---

### Phase 1 — contract/boundary cleanup

**Purpose:** prevent the improved UI from embedding workspace/compiler semantics in React.

**Files/modules affected**

- `src/vg2c_ui/api/models.py`
- `src/vg2c_ui/api/workspace.py`
- `src/vg2c_ui/api/serialization.py` for backend-owned scope display labels if changed
- `src/vg2c_ui/frontend/src/contracts.generated.ts` via generator
- `src/vg2c_ui/frontend/src/api.ts`
- `src/vg2c_ui/frontend/src/App.tsx`
- `src/vg2c_ui/frontend/src/operationLabels.ts`
- UI/workspace contract tests.

**Add/change**

- server-derived workspace file role/translatable metadata;
- optional workspace capabilities endpoint used by uploader for exact constraints;
- `getWorkspaceCapabilities()` transport;
- backend-friendly scope display labels so React no longer maps compiler scope kinds.

**Remove**

- React classification based on `.txt` and `generated/` once contract metadata is available;
- scope-kind-to-user-label mapping from the frontend once backend labels cover it.

**Dependencies**

- none beyond existing stack.

**Regression checks**

- contract generator/check;
- workspace session/upload tests;
- translation/open-document tests;
- frontend typecheck.

**Completion criteria**

- frontend consumes explicit contract fields for file role/translatability;
- upload limit/type UI, if shown, comes from backend capabilities;
- no new handwritten duplicate domain types.

---

### Phase 2 — minimal shared primitives and style foundation

**Purpose:** create only the reusable controls required by later phases.

**Files/modules affected**

- `package.json` / lockfile
- new `components/ui/*`
- `styles.css`
- `OperationEditor.tsx`
- `StructuredSqlEditor.tsx`
- new `OperationShared.tsx`.

**Add**

- `radix-ui`;
- `lucide-react`;
- Button and Badge source-owned primitives;
- Dialog and Tooltip project wrappers only where they centralize shared styling/required semantics;
- shared `FileReferences` and `OperationDiagnostics`.

**Remove**

- duplicate file-chip and operation-diagnostic implementations after both editors migrate;
- touched duplicate button/control CSS.

**Do not add**

- Tailwind/shadcn;
- Motion;
- React Router;
- state library;
- wrappers for every native form element.

**Regression checks**

- operation editors render/edit identically;
- focus-visible behavior;
- existing tree behavior untouched;
- typecheck/build.

**Completion criteria**

- shared primitive layer is small and has real consumers;
- no duplicate icon libraries;
- existing token palette remains source of truth.

---

### Phase 3 — shell and layout cleanup

**Purpose:** simplify `App.tsx` before adding new workflows.

**Files/modules affected**

- `App.tsx`
- new `FileTabs.tsx`
- new `WorkspaceHeader.tsx`
- new `ChangeToolbar.tsx`
- optional small editor-toolbar component if extraction is cohesive
- `styles.css`.

**Add/change**

- extract translated-file tab UI and its keyboard handler;
- extract header/source/download composition;
- extract change-toolbar presentation while keeping reducer state in `useWorkspace`;
- group generated downloads under a Radix Dropdown Menu only if the current variable-length link list remains a header-density problem;
- replace CSS-generated compact icons with Lucide icons;
- restructure header regions so flex/grid wrapping handles most width changes.

**Remove**

- corresponding inline markup/helpers from `App.tsx` after each extraction;
- superseded header/tab CSS hacks as layout is stabilized.

**Dependencies**

- Radix Dropdown Menu only through already-added `radix-ui` if used.

**Regression checks**

- tab keyboard/close behavior;
- translate/source selection;
- generated downloads;
- 1280/1024/768/390 layout smoke test.

**Completion criteria**

- `App.tsx` reads as orchestration/composition rather than a collection of feature implementations;
- no old and new header/tab paths coexist.

---

### Phase 4 — command palette

**Purpose:** add the highest-value Watermelon interaction pattern without introducing architecture overhead.

**Files/modules affected**

- `package.json` / lockfile
- new `CommandPalette.tsx`
- new `commandItems.ts`
- `App.tsx`
- `ScriptTree.tsx` only for stable focus integration if necessary
- `styles.css`.

**Add**

- `cmdk`;
- pure command derivation;
- Ctrl/Cmd+K listener;
- file/operation/action/navigation groups;
- focus restoration/destination logic.

**Remove**

- nothing unless an old ad-hoc shortcut becomes fully redundant; existing Z/Y/S shortcuts remain.

**Regression checks**

- keyboard navigation/filter/execute/Escape;
- operation navigation expands ancestors and focuses destination;
- disabled actions stay disabled;
- no command executes stale/closed tab data.

**Completion criteria**

- adding a normal future command means editing the pure command builder, not registering with a framework;
- palette owns no domain state and calls existing actions only.

---

### Phase 5 — upload/source intake workflow

**Purpose:** replace hidden file inputs + multiple select with a clear, truthful workflow.

**Files/modules affected**

- new `useWorkspaceFiles.ts`
- new `SourceUpload.tsx`
- `WorkspaceHeader.tsx`
- `App.tsx`
- `api.ts`
- `styles.css`
- related tests.

**Add/change**

- drag/drop and keyboard file/folder selection;
- staged queue;
- server-capability-based preflight;
- one-file-per-request sequential upload execution;
- staged removal, error retry, completed clear;
- indeterminate uploading state;
- source selection based on `translatable` contract metadata;
- refresh after partial failure.

**Remove**

- old `Upload files`/`Upload folder` hidden-input markup from header;
- old `<select multiple>` source picker;
- suffix/prefix classification logic if not already removed;
- upload usage of global `busy`/`message`.

**Dependencies**

- none beyond Phase 2 primitives.

**Regression checks**

- relative folder paths;
- duplicate/unsupported/oversized error display;
- server remains final validator;
- partial success reconciliation;
- source auto-selection;
- translation still receives exactly selected workspace paths.

**Completion criteria**

- no simulated progress;
- no UI claim of deleting uploaded files;
- every queue state corresponds to a real request state.

---

### Phase 6 — inspector and editor cleanup

**Purpose:** improve information hierarchy and remove obsolete SQL UI paths.

**Files/modules affected**

- `ContextSidebar.tsx`
- optional `useMediaQuery.ts`
- `StructuredSqlEditor.tsx`
- `OperationEditor.tsx`
- `OperationShared.tsx`
- `sql/sqlEditor.css`
- `styles.css`.

**Add/change**

- shared inspector content for desktop aside and compact modal sheet;
- Radix-managed compact focus/escape behavior;
- native uncontrolled details sections;
- controlled SQL add dialogs;
- server-option-driven SQL fields;
- structured SQL tabs for Selections/Filters/Joins where all are relevant;
- clear local form validation and mutation errors.

**Remove**

- `ContextSidebar.Panel` mirrored open state;
- custom compact backdrop once Radix sheet is live;
- all `window.prompt` calls;
- replaced SQL add path;
- dead SQL selectors confirmed unused after migration.

**Regression checks**

- data-flow dependency behavior;
- CSV preview;
- inspector desktop/compact behavior;
- focus trap/return;
- structured SQL inspect/update/add/remove/move;
- raw SQL remains read-only;
- backend action payloads unchanged.

**Completion criteria**

- no browser prompts;
- one inspector content implementation;
- SQL stylesheet describes the UI that actually exists.

---

### Phase 7 — feedback and interaction polish

**Purpose:** remove the overloaded global status channel and protect destructive actions.

**Files/modules affected**

- `package.json` / lockfile
- `main.tsx` or `App.tsx` for Toaster placement
- `App.tsx`
- `FileTabs.tsx`
- source/translation components
- `styles.css`.

**Add**

- `sonner`;
- transient success/error toasts;
- dirty-tab AlertDialog;
- `beforeunload` dirty-document protection;
- tooltips on icon-only actions;
- explicit persistent conflict/validation states.

**Remove**

- `message` state;
- `status-message` element/CSS;
- message-setting wrappers that only serviced that element;
- shared app-wide `busy` once upload and translation each have truthful state.

**Regression checks**

- mobile/tablet failures remain visible;
- persistent problems remain inline;
- close/refresh cannot silently discard dirty edits;
- toast does not steal focus.

**Completion criteria**

- every long-running action has an in-place busy state;
- every transient result has a consistent notification path;
- persistent issues never depend on a toast.

---

### Phase 8 — responsive and accessibility pass

**Purpose:** validate the integrated architecture and remove residual breakpoint patches.

**Files/modules affected**

- `styles.css`
- touched feature components
- Playwright accessibility/responsive tests.

**Add/change**

- final layout simplification;
- touch target sizing;
- focus destination fixes;
- reduced-motion overrides for new components;
- Axe checks and browser tests.

**Remove**

- obsolete narrow-width overrides superseded by the structural header/inspector solution;
- pseudo-content icons and one-off layout nudges no longer needed.

**Regression checks**

- keyboard-only full edit workflow;
- screen sizes listed above;
- no horizontal page overflow;
- dialog/palette/inspector focus;
- reduced-motion/contrast modes.

**Completion criteria**

- breakpoints correspond to actual mode changes rather than accumulated patches;
- no inaccessible action appears only on hover/drag.

---

### Phase 9 — redundant-code deletion and dependency audit

**Purpose:** make deletion an explicit deliverable rather than postponed cleanup.

**Files/modules affected**

- entire touched frontend tree;
- package manifest/lockfile;
- CSS.

**Delete after confirmed replacement**

- old top-bar upload markup and source multiple-select;
- old global status message path;
- old custom compact inspector backdrop/path;
- duplicated file-reference/diagnostic components;
- `window.prompt` SQL path;
- unused SQL CSS selectors;
- CSS selectors for deleted markup;
- dead imports/helpers exposed by refactor;
- old compatibility aliases added temporarily during migration, if any.

**Audit**

- every runtime dependency has at least one production consumer;
- no Tailwind/shadcn/Motion/react-icons accidentally remains;
- no unused copied Watermelon example code/assets;
- no frontend copy of backend domain models;
- no path/suffix semantic inference superseded by contract metadata;
- no two implementations solve the same UI responsibility.

**Regression checks**

- full frontend build/tests;
- relevant backend tests;
- E2E workflows;
- static search for deleted selectors/imports/prompts/legacy markup.

**Completion criteria**

- the replacement is the only implementation path;
- code and CSS size should decrease in the migrated areas despite the new features.

---

### Phase 10 — final architecture review

**Purpose:** challenge the result before declaring the modernization complete.

Review each new abstraction:

- Is it used by more than one place or does it own meaningful behavior?
- Could native HTML + shared CSS replace it more simply?
- Does it own state that belongs to `workspaceState`, a feature, or the backend?
- Is any domain semantic being inferred from strings/path conventions?
- Did a temporary migration adapter survive without reason?
- Is a component configurable because of real consumers or speculative future use?
- Can any dependency be removed?

Expected end state:

```text
Backend/core owns semantics and validation
        ↓
Generated contracts are the only domain model bridge
        ↓
api.ts is transport only
        ↓
useWorkspace/workspaceState own editor/document state
useWorkspaceFiles owns remote file inventory
        ↓
App composes feature components
        ↓
feature components own only local UI workflow state
        ↓
small reusable primitives provide consistent behavior/styling
```

Final completion gate:

```text
[ ] no old/new parallel UI paths
[ ] no browser prompt() flows
[ ] no fake upload percentages
[ ] no hidden-at-mobile global error channel
[ ] ScriptTree semantics and keyboard behavior preserved
[ ] generated contract check passes
[ ] frontend typecheck/build passes
[ ] reducer/component tests pass
[ ] relevant backend tests pass
[ ] Playwright critical workflows pass
[ ] Axe representative states pass
[ ] responsive desktop/tablet/phone checks pass
[ ] runtime dependencies are minimal and justified
[ ] dead SQL/global CSS from replaced paths is removed
```

---

## 12. Major deletions/refactors to expect

This work is successful only if the codebase becomes simpler after the UI improvement. The implementation should therefore plan to remove, not merely hide, the following once replacements are proven:

| Current implementation | Replacement | Delete afterward |
|---|---|---|
| inline top-bar file/folder inputs | `SourceUpload` | upload labels/hidden inputs and obsolete upload CSS |
| `<select multiple>` source picker | explicit source-selection UI | multiple-select layout CSS |
| App-wide `busy` | operation-specific states | global busy branches |
| App-wide `message` + responsive-hidden output | in-place status + Sonner | state, wrappers, `.status-message` |
| inline file-tabs code and `handleTabKeys` in App | `FileTabs` | old inline rendering/helper |
| browser `prompt()` SQL adds | controlled Dialog forms | all prompt calls |
| duplicated file chips | `FileReferences` | both local duplicate helpers |
| duplicated operation diagnostics | `OperationDiagnostics` | both local duplicate helpers |
| controlled React mirror of `<details>` open state | native `defaultOpen` | `Panel` state |
| custom compact inspector backdrop/modal behavior | Radix compact sheet using shared inspector content | old backdrop and compact modal CSS/path |
| stale SQL layout selectors | current SQL editor styles only | confirmed unused selectors |
| frontend scope-kind naming | backend `ScopeView.label` | scope-kind interpretation in React |
| frontend workspace path/suffix classification | explicit workspace-file contract metadata | `.endsWith('.txt')` / `startsWith('generated/')` semantic branching |

Do not touch unrelated compiler/runtime/CLI code for stylistic consistency.

---

## 13. Meaningful deviations from the earlier Watermelon plan

The previous research was directionally useful but broader than the current repository needs. The implementation should deliberately deviate in these areas:

1. **No Tailwind/shadcn migration.** The existing CSS token system, responsive rules, focus styling, reduced-motion behavior, and workbench density are already viable. Adding a second styling framework would increase complexity and leave legacy CSS in place during a long migration.
2. **No Motion dependency.** Current CSS transitions are sufficient. Watermelon motion is aesthetic reference only.
3. **No generic UI file for every catalog primitive.** Create Button/Badge/Dialog/Tooltip because they have concrete reuse. Keep native select/checkbox/textarea/table/details until richer behavior is justified.
4. **File tabs remain domain-specific.** They include close/status/focus behavior that generic Tabs do not model cleanly. Generic Tabs are appropriate only for a true mode switch such as SQL sections.
5. **Sheet is a Dialog presentation, not a second abstraction.** The compact inspector can use the same accessible overlay primitive styled responsively.
6. **Dropdown Menu is conditional on actual header density.** It is not mandatory simply because Watermelon ranks it highly.
7. **Combobox is deferred.** A checkbox list is simpler for current source selection; add search only if real source counts demand it.
8. **Labeled Progress is not used as a percentage today.** The current upload API cannot report byte progress. Use truthful indeterminate state.
9. **The existing state layer is preserved.** No context/store rewrite is needed.
10. **Structured SQL cleanup is promoted in priority.** The current browser prompts and stale CSS are more concrete maintainability problems than several visual primitives proposed in the earlier plan.
11. **Backend contract cleanup is added.** The current React suffix/path classification and scope-kind naming should be eliminated before polishing the corresponding UI.
12. **Deletion is a named phase.** Old upload/status/inspector/SQL paths and obsolete CSS must be removed after parity rather than retained as fallbacks.

---

## 14. Out of scope for this refactor

Unless a separate requirement is approved, do not include:

- authentication/Supabase work;
- execution of generated workflows;
- router/deep-link architecture;
- persisted user preferences/favorites/recent commands;
- backend workspace-file deletion;
- arbitrary extension/plugin command registration;
- a visual workflow graph replacing ScriptTree;
- a general data-grid system;
- theme system redesign;
- unrelated compiler/runtime refactors;
- cosmetic rewriting of unaffected backend code.

These can be added later against the cleaner boundaries if they become real product requirements.

---

## 15. Recommended review/commit cadence

Keep changes easy to review and revert. A sensible series is:

```text
1. test: characterize current frontend interactions
2. refactor(api): expose workspace file/capability presentation metadata
3. refactor(ui): add minimal shared primitives and remove duplicated editor UI
4. refactor(ui): extract header, file tabs, and change toolbar
5. feat(ui): add command palette
6. feat(ui): replace source upload workflow
7. refactor(ui): make context inspector accessible/responsive
8. refactor(ui): replace structured SQL prompt flows and clean SQL CSS
9. feat(ui): unify transient feedback and dirty-close protection
10. refactor(ui): responsive/accessibility cleanup and legacy deletion
11. test(ui): finalize E2E/accessibility gates
12. refactor(ui): final dependency/dead-code simplification
```

A phase may need more than one commit, but do not mix unrelated backend/compiler refactors into the same review.

The architecture should be considered successful when the user-facing UI is more capable while the implementation has fewer ad-hoc state paths, fewer duplicated components/styles, fewer frontend semantic assumptions, and no legacy UI left running beside its replacement.
