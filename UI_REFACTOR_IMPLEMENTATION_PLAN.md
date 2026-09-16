# SQLPathFinder Frontend Refactoring and UI Improvement — Implementation Record

> **Base branch:** `main` at `91bfea5d9ab7b38fd2a60e1950ec9b5865231062`.
>
> **Implementation branch:** `refactor/frontend-ui-workbench`.
>
> **Implementation code freeze before this documentation-only update:** `e6569ee7fae94c097dfe36d32d14e635efdc1b20`.
>
> **Status:** the planned workbench refactor is implemented on the branch. This document now records the resulting architecture, deliberate deviations from the original plan, and the validation/merge gate instead of describing unimplemented work.

## 1. Architecture that remains authoritative

The responsibility direction is unchanged:

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
presentation components
```

The refactor preserves these boundaries:

- `workspaceState.ts` remains the document/editor state machine.
- `useWorkspace.ts` remains the controller for document edits, validation, apply/reload, CSV preview, workspace projection, and structured SQL actions.
- stale asynchronous results remain rejected by tab instance/version/request ownership.
- `contracts.generated.ts` remains the frontend domain-contract source; React does not maintain parallel workflow models.
- compiler/workspace semantics stay on the backend. React does not parse SQL, infer data flow, or reconstruct compiler scope semantics.
- `CAPABILITY_EDITORS` remains the small dispatch seam for specialized operation editors.
- the domain-specific `ScriptTree` remains in place with its ARIA tree semantics and keyboard behavior.
- `App.tsx` is primarily a composition/orchestration root rather than the owner of upload, workspace-file, or editor state.

No Redux/Zustand, frontend router, client SQL parser, generic tree framework, Tailwind/shadcn migration, or application-wide state framework was introduced.

## 2. Implemented workbench structure

The resulting frontend responsibilities are intentionally narrow:

```text
App.tsx
├─ useWorkspace()                 document/editor controller
├─ useWorkspaceFiles()            workspace inventory + upload-policy state
├─ useSourceIntake()              source selection + upload queue + translation intake
├─ SourceIntake                   intake presentation / compact disclosure
├─ FileTabs                       translated-document navigation
├─ ChangeToolbar                  undo/redo/preview/apply/reload presentation
├─ CommandPalette + commands.ts   one command registry
├─ ScriptTree                     workflow navigation/editor host
├─ OperationEditor                capability dispatch
├─ StructuredSqlEditor            backend-driven structured SQL editing
├─ ContextSidebar                 shared inspector content + responsive shell
├─ DirtyCloseDialog               unsaved-close protection
└─ ThemeSelector/useTheme         system/light/dark preference
```

Feature state stays with the feature that owns it. Presentation components do not reproduce backend/workspace semantics.

## 3. Backend/frontend contract cleanup

Workspace presentation metadata is now explicit at the API boundary:

- workspace files expose backend-owned `role` (`source`, `data`, `generated`);
- workspace files expose whether they are `translatable`;
- upload limits and allowed suffixes are served by the workspace policy endpoint;
- user-facing scope labels are serialized by the backend;
- generated TypeScript contracts carry these values to the frontend.

The frontend no longer determines source/generated meaning using `.endsWith('.txt')`, `startsWith('generated/')`, or scope-kind label maps.

The server remains authoritative for path safety, upload limits, duplicate files, translation semantics, SQL edits, and workspace projection. Client-side intake validation is only a usability preflight.

## 4. Source intake and upload workflow

The old header-based upload/multi-select workflow has been replaced by `SourceIntake` + `useSourceIntake`.

Implemented behavior:

- file upload, folder upload, and drag/drop;
- preservation of `webkitRelativePath` for folder uploads;
- per-file deterministic queue states: queued, uploading, uploaded, failed;
- sequential one-file requests so a failed item does not make other queue state ambiguous;
- retry for server failures;
- removal of queued/failed items and clearing completed items;
- backend-policy-based suffix/size/count/capacity preflight;
- explicit checkbox selection of backend-declared translatable sources;
- successful uploaded source files are added to source selection;
- partial batch-translation diagnostics remain owned by Source Intake rather than contaminating document diagnostics;
- workspace inventory failures and upload-policy failures are separate failure domains;
- upload/translation/retry capabilities use shared derived controller guards rather than duplicated button-state logic;
- retry requires a freshly refreshed workspace inventory before client-side revalidation.

No fake byte-progress percentage is displayed because the transport does not provide trustworthy byte progress.

The frontend transport exposes only the actual frontend upload operation (`uploadWorkspaceFile`). The obsolete bulk frontend helper was removed; the backend may continue accepting the existing list-shaped endpoint contract.

## 5. Workbench navigation and editing

### File tabs

`FileTabs` owns translated-document tab presentation and keyboard navigation. Dirty documents are protected by `DirtyCloseDialog`, and `beforeunload` is registered only while unsaved workspace changes exist.

When no translated document exists, the tab strip is not rendered and its 44px grid track is not reserved.

### Command palette

There is one command registry in `commands.ts` for:

- workspace upload/translation actions;
- document navigation;
- scope/operation navigation;
- inspector opening;
- undo/redo/preview/apply/reload;
- expand/collapse all scopes.

Global shortcuts execute the same command/action paths rather than maintaining separate business logic.

### Script tree

`ScriptTree` was preserved rather than replaced by a generic library tree. Existing roving focus, Arrow/Home/End navigation, parent/child navigation, search visibility, and scope expansion behavior remain domain-specific.

Dependency-error markers use semantic danger tokens and remain labelled for assistive technology.

## 6. Structured SQL editor

Browser `prompt()` flows were removed.

Selection, filter, and join additions now use controlled inline forms. Allowed operators, join types, logical connectors, capabilities, and read-only reasons are consumed from `SqlModelView`; the frontend does not reconstruct SQL grammar or maintain parallel semantic lists.

Raw SQL remains a read-only reference. Structural changes continue through backend SQL actions and the existing document edit/version pipeline.

Obsolete SQL selectors from superseded editor structures were removed during the CSS cleanup.

## 7. Inspector and dialog behavior

The inspector keeps one content implementation for both responsive modes:

- desktop: persistent aside;
- compact layouts: native modal `<dialog>` styled as the inspector surface.

`useModalDialog` owns the small amount of reusable native-dialog behavior that is genuinely shared: `showModal`/close synchronization, backdrop close, and focus return.

`<details>` sections use native open state rather than mirrored React state.

After the empty-workspace cleanup, `ContextSidebar` is mounted only when an active document exists, so its former nullable/empty compatibility branch was deleted.

## 8. Feedback and failure ownership

The previous app-wide overloaded status/message path is gone.

Durable failures are rendered where they can be acted on:

- validation/apply state in the document change surface;
- structured SQL failures in the SQL editor;
- upload item failures in the queue;
- translation failures and diagnostics in Source Intake;
- CSV preview failures beside the relevant artifact;
- workspace inventory and upload-policy failures in Source Intake.

The refactor intentionally did **not** add a global toast dependency. Native/local durable feedback was sufficient for the implemented workflows, avoiding another application-level notification state path.

## 9. Theme and styling architecture

System/light/dark theming is implemented with semantic CSS variables. Components use semantic tokens rather than branching on theme names.

Styling remains CSS-based; no second styling framework was added. Feature-specific CSS files isolate command palette, source intake, inspector, dirty-close dialog, operation presentation, theme selector, and change-toolbar rules while `styles.css` retains shared workbench structure/tokens.

Confirmed dead global status/header-chip selectors and superseded SQL selectors were removed rather than left as compatibility CSS.

## 10. Responsive behavior verified during implementation

The implementation was repeatedly rendered in Chromium while iterating. Representative widths included:

- 1440px desktop;
- 1024px compact desktop/tablet;
- 820/821px source-intake disclosure breakpoint boundary;
- 768px tablet portrait;
- 390px phone;
- 320px narrow phone.

High-risk rendered states included:

- no translated document;
- active translated document;
- compact collapsed/expanded Source Intake;
- partial translation failure;
- upload-policy failure in light and dark themes;
- dependency-error tree rows;
- structured SQL desktop/mobile states;
- dirty/conflict document state;
- compact inspector/dialog states.

The final no-document composition removes document-only toolbar/diagnostic/inspector surfaces instead of displaying them disabled. On short phones the expanded Source Intake scrolls internally and the empty-state message remains visible. Browser geometry checks showed no document-level horizontal overflow at the representative final layouts.

## 11. Deliberate deviations from the original plan

The original planning document proposed Radix, cmdk, Sonner, Lucide, Playwright, and axe dependencies as possible implementation tools. They were not added merely to satisfy the plan.

The implementation achieved the required production behavior with React, native HTML/browser primitives, generated contracts, and the existing CSS architecture:

- native `<dialog>` replaced the need for a Radix runtime dependency;
- the command palette remained small enough for a project-owned implementation and one command registry rather than cmdk;
- durable feature-owned feedback removed the need for Sonner;
- the existing visual language did not justify an icon-package dependency;
- browser rendering was used during implementation without adding unused test packages to the production repository.

This is intentional dependency minimization, not an incomplete migration. `package.json` retains React/ReactDOM as the only runtime frontend dependencies.

## 12. Clean-code/DRY invariants after the refactor

The branch should continue to preserve these invariants:

1. One generated frontend contract source; no parallel workflow interfaces.
2. One document/editor state machine (`workspaceState`).
3. One document controller (`useWorkspace`).
4. One workspace-file inventory owner (`useWorkspaceFiles`).
5. One source-intake controller (`useSourceIntake`).
6. One command registry (`commands.ts`).
7. One shared operation file-reference renderer.
8. One shared operation diagnostic renderer.
9. One inspector content path across responsive modes.
10. One native-dialog behavior helper where focus/close behavior is actually shared.
11. Backend-owned workspace/SQL/compiler semantics.
12. No old/new UI compatibility branch after a replacement is complete.
13. No dependency introduced without a current production/test consumer.

Do not turn these focused owners into a generic service/provider framework unless a concrete second consumer proves the need.

## 13. Validation record and merge gate

Validation performed during implementation included:

- repeated source/diff review against `main` to prevent scope creep;
- generated-contract/backend-consumer consistency review;
- existing reducer regression coverage retained and expanded on the branch;
- repeated Chromium rendering and geometry checks for the responsive/failure states listed above;
- keyboard/focus semantics reviewed while implementing tabs, tree, command palette, dialogs, and inspector;
- static removal checks for old prompt/status/upload/backdrop/semantic-inference paths;
- final branch comparison confirms the refactor is limited to the workbench, its small backend API/workspace boundary, and relevant UI/API tests.

The connected repository currently exposes no CI/status workflow for this branch. The execution environment used for this implementation could not clone the public repository through normal shell networking, so it was not possible to truthfully claim a fresh local `npm test`, `npm run build`, and full `pytest` run from the final remote branch snapshot.

Before merging to `main`, run these commands from a normal checkout with the project environment available:

```text
cd src/vg2c_ui/frontend
npm test
npm run build

# repository root / configured Python environment
pytest tests/ui
```

Also run the repository's SQL/domain tests normally used for changes touching structured SQL behavior.

A failure in those commands is a merge blocker; do not work around it with compatibility code or by weakening generated-contract checks.

## 14. Definition of done

The implementation is considered architecturally complete when the branch is reviewed with the following outcomes intact:

- upload/source selection/translation are feature-owned and backend-policy-aware;
- tabs, dirty-close protection, commands, ScriptTree, editing, preview/apply/reload, inspector, CSV preview, and structured SQL editing remain functional;
- browser `prompt()` and global overloaded status messaging are absent;
- the no-document state contains no empty tab row, blank inspector, disabled editor toolbar, or meaningless `Diagnostics 0` panel;
- mobile/compact surfaces remain usable without document-level horizontal overflow;
- system/light/dark themes use semantic tokens;
- no redundant old UI path or unused frontend runtime dependency remains;
- frontend semantics continue flowing from backend/generated contracts rather than path/suffix/compiler inference;
- future automated/AI editing can use the same validated backend edit/action boundaries as the human UI rather than bypassing document validation/version handling;
- the full project test/build merge gate above passes in the normal development environment.

This document should now be maintained as an implementation/architecture record. Future UI work should modify it only when a responsibility boundary or accepted workbench behavior materially changes.
