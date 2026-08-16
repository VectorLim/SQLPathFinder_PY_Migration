# VG2 Visual Editor — Execution Plan

Status: Stages 0–7 implemented; legacy-suite follow-up remains documented.

This document is the durable execution reference for a local visual editor that
turns generated VG2 Python into an approachable, editable workflow. It is
intentionally separate from the existing compiler package and should be updated
as stages are completed.

## Status legend

- `[x]` complete
- `[~]` in progress
- `[ ]` not started
- `Blocked:` followed by the decision or dependency preventing progress

Current status: Stages 0, 2, and 3 `[x]`; Stage 1 implementation `[x]` with a
legacy-suite acceptance follow-up recorded below; Stages 4–7 `[x]`.

## Product outcome

Users can translate one or many VG2 files, open each result in browser-like
tabs, and understand or modify the generated workflow without learning Python.
The canvas presents steps as labelled, icon-bearing blocks; conditions branch;
loops visibly return to their body; and CSV inputs/outputs are explicit dataflow
connections. Selecting a block opens its real function docstring (or a clear
placeholder) and typed parameter editors. Changes are validated and written
only to the generated step/orchestration region.

The first release is a local browser application. A later desktop wrapper is
optional. LLM assistance is deliberately the final stage and must use the same
validated command path as a human editor.

## Architecture and ownership

```text
VG2 files -> existing vg2c compiler -> generated Python + workflow metadata
                                      |
                              FastAPI document service
                                      |
                            versioned workflow JSON
                                      |
                         React/TypeScript visual editor
                                      |
                     typed edits -> AST validation -> atomic save
```

### Technology defaults

- Frontend: React, TypeScript, `@xyflow/react` (React Flow), CSS variables,
  CSS transitions, and a small reducer-based state layer.
- Backend: FastAPI, Pydantic models, and the Python standard-library `ast`
  module for generated-code inspection and edits.
- Packaging: serve the built frontend from the Python service. Consider a
  `pywebview` launcher only after browser UX and packaging are stable.
- The UI must not reproduce compiler parsing, resolution, or dataflow logic;
  it consumes a compiler-owned result/model.

### Source-of-truth rules

1. The VG2 source remains the input for a fresh translation.
2. The generated `.py` file is authoritative for values currently displayed and
   executed by the user.
3. A neighbouring, versioned `.vg2c-ui.json` sidecar stores layout, source
   hashes, revision metadata, and replayable UI overrides; it is not a hidden
   replacement for the Python program.
4. On disagreement, reparse Python and show a reconciliation warning. Never
   silently overwrite user edits during retranslation: replay matching stable
   identities and surface unresolved changes as conflicts.
5. Dependency/helper code is read-only. UI writes are limited to explicit
   generated regions and preserve all bytes outside those regions.

## Minimal `vg2c` seam

Keep existing CLI and `translate()` behaviour compatible. Add a small public
compilation facade, conceptually:

```text
compile_document(input_path) -> CompilationResult
```

`CompilationResult` should expose generated Python, resolved blocks, scope tree,
CSV/dataflow edges, diagnostics, and function-name-to-block mapping. Existing
translation can delegate to it while retaining its current return value.

The emitter adds deterministic comment markers (comments preserve Python
compatibility):

```python
# <vg2c:dependencies:end>
# <vg2c:steps:start>
def step_0001_example(ctx) -> None:
    ...
# <vg2c:steps:end>
# <vg2c:workflow:start>
def run() -> None:
    ...
# <vg2c:workflow:end>
```

Consumers locate markers by content, never by fixed line numbers. Existing
post-processing that prepends SQL/filter comments must remain compatible.

## Workflow model

Use a versioned Pydantic schema (and generated TypeScript types/OpenAPI client)
with this top-level shape:

```text
WorkflowDocument
├── identity, source/output paths, hashes, revision
├── steps[]
├── scopes[]
├── controlEdges[]
├── dataEdges[]
├── diagnostics[]
├── layout
└── overrides[]
```

Each step records a stable ID, generated function name, original block index and
source span, functional kind, display label/icon key, actual docstring, typed
parameter descriptors, CSV inputs/outputs, parent scope/branch, validation
state, and raw code when unsupported. Parameter descriptors include name or
position, source representation, typed value, editor type, constraints, and
editable/read-only state.

Use discriminated node kinds (`step`, `if`, `branch`, `loop`,
`csv-artifact`) rather than a generic untyped node. Stable identity must survive
layout changes and allow retranslation override matching.

## Safe generated-Python editing

The backend parses only functions inside the marked step/workflow regions:

1. Locate each `FunctionDef` and extract its actual `ast.get_docstring()`.
2. Recognize known calls and literal positional/keyword arguments.
3. Convert safe literals into typed controls; keep dynamic expressions read-only.
4. Apply precise source-span edits while retaining formatting where possible.
5. Reparse and `compile()` the complete candidate before saving.
6. Save atomically and update revision/hash metadata.

Initially editable: strings and multiline SQL, integers, booleans, known enums,
literal lists, and CSV paths. Initially read-only: arbitrary expressions,
`PYTHON_EMBED`, unknown utilities, dependency code, and side-effectful values.
Use LibCST only if real fixtures demonstrate that AST/source-span edits cannot
preserve required formatting; do not add it speculatively.

Missing docstrings render as `No description provided` (or equivalent), with a
reserved description area. `PROMPT-TEXT` may inform a label/subtitle but is not
the function docstring.

## UI and interaction design

### Shell

- Top: browser-style file tabs with dirty, valid, saving, and conflict states.
- Left: file/workflow navigator, search, and scope tree.
- Center: draggable React Flow canvas with controls, minimap, fit-to-selection,
  and persisted viewport/layout.
- Right: selected-node inspector with description, parameters, inputs/outputs,
  and validation messages.
- Bottom: collapsible diagnostics, translation results, and bounded CSV preview.

### Visual language and behaviour

- Cards show a meaningful functional label, icon, step number, docstring preview,
  input/output ports, CSV badges, and validation indicator.
- Normal execution edges are solid directional arrows. CSV/dataflow edges are
  dashed and labelled with filenames/artifacts.
- `if` is a split/diamond gateway with labelled true/false lanes; `else` is
  visually paired with its condition. Loops are outlined groups with a visible
  return arrow and iteration/chunk label. Nested scopes collapse into containers.
- Dragging, selection, edge emphasis, expand/collapse, and tab transitions use
  restrained motion. Respect `prefers-reduced-motion`; never encode meaning by
  colour alone.
- Support keyboard navigation, focus-visible styling, logical tab order,
  accessible names/tooltips, high contrast, and usable empty/loading/error states.
- Each tab owns graph state, viewport, undo/redo history, dirty state,
  diagnostics, and save revision independently.

## Proposed repository layout

```text
src/
├── vg2c/                         # existing compiler; minimal seams only
│   ├── __init__.py               # compilation facade
│   └── emitter/                  # region markers and metadata hooks
└── vg2c_ui/                      # isolated UI/backend package
    ├── __init__.py
    ├── __main__.py
    ├── app.py                    # FastAPI construction
    ├── domain/
    │   ├── models.py             # workflow schema
    │   ├── commands.py           # typed edit commands
    │   └── diagnostics.py
    ├── services/
    │   ├── compiler_adapter.py
    │   ├── python_document.py    # markers, AST, atomic writes
    │   ├── workflow_builder.py
    │   ├── document_store.py
    │   ├── csv_preview.py
    │   └── validation.py
    ├── api/
    │   ├── documents.py
    │   ├── translation.py
    │   └── csv.py
    ├── frontend/                 # React/TypeScript source and package files
    └── static/                   # packaged frontend build
tests/
├── ui/
└── fixtures/generated_workflows/
```

Keep backend domain/services independent of HTTP and frontend components
independent of compiler internals. Reducer commands should mirror backend edit
commands, providing a direct path to undo/redo and later agent proposals.

## Staged delivery checklist

Dependencies flow from top to bottom. Do not begin a stage until its stated
prerequisites and acceptance criteria are met.

### Stage 0 — Behaviour contract and fixtures

Status: `[x]` prerequisite contracts and fixtures implemented.

- [x] Collect representative generated files: SQL, CSV, macros, nested
  conditions, loops, unknown utilities, and Python embeds.
- [x] Freeze marker syntax, authority/conflict rules, supported parameter types,
  sidecar version, and unsupported-code behaviour.
- [x] Record CLI/test contract drift before using CLI tests as a gate.

Acceptance: examples and explicit read-only expectations exist for every
supported construct.

### Stage 1 — Compiler seam and region markers

Status: `[~]` implementation complete. The focused compiler/UI tests pass;
full-suite acceptance remains gated by the pre-existing CLI/emitter test drift.

- [x] Add `CompilationResult` facade without breaking `translate()` or CLI.
- [x] Emit dependency/steps/workflow markers.
- [x] Export scope, control-flow, CSV, and function mapping metadata.
- [x] Preserve existing generated output except for deterministic marker comments.

Acceptance: existing compiler tests pass and dependency bytes are unchanged.

### Stage 2 — Read-only backend workflow model

Status: `[x]` complete. Depends on Stage 1.

- [x] Parse marked Python regions and combine AST values with compiler metadata.
- [x] Produce versioned workflow JSON and stable identities.
- [x] Add document-open and batch-translation endpoints.
- [x] Add bounded diagnostics and sidecar read/write model.

Acceptance: fixtures serialize stable steps, branches, loops, and CSV edges.

### Stage 3 — Read-only visual application

Status: `[x]` complete. Production build and interactive browser smoke tests pass.

- [x] Build the React shell, multi-file tabs, canvas, custom nodes/edges, and
  inspector.
- [x] Add deterministic initial layout, dragging, zoom, minimap, and persisted
  viewport.
- [x] Render docstring placeholders and unsupported/read-only indicators.

Acceptance: multiple translated files can be navigated without modifying Python.

### Stage 4 — Typed parameter editing

Status: `[x]` complete.

- [x] Implement safe editors, typed command history, undo/redo, and dirty state.
- [x] Validate changes, preview diffs, and save atomically.
- [x] Detect source/output revision changes and show reloadable conflicts.

Acceptance: edit → validate → save → reopen preserves values and dependency bytes.

### Stage 5 — Dataflow and CSV experience

Status: `[x]` complete.

- [x] Highlight producer/consumer paths on selection.
- [x] Add size-limited CSV preview, missing-input warnings, conditional-output
  indicators, and ordering diagnostics.
- [x] Enforce workspace path safety and preview byte/row limits.

Acceptance: a non-programmer can trace a CSV from source through transformations
to output.

### Stage 6 — Accessibility, polish, and packaging

Status: `[x]` complete for the browser application; native wrapper remains deferred.

- [x] Complete keyboard/screen-reader/high-contrast/reduced-motion pass.
- [x] Add loading, empty, failure, conflict, and save-progress states.
- [x] Enable visible-node rendering for large workflows and package static assets for a local
  browser launch.
- [x] Consider optional pywebview wrapper; defer it because browser packaging works
  without another runtime dependency.

Acceptance: distributable local app needs no Node installation at runtime and
remains usable without animation or colour perception.

### Stage 7 — LLM command integration

Status: `[x]` complete.

Expose constrained operations such as `get_workflow`, `get_step`,
`set_parameter`, `validate_changes`, `preview_diff`, and `apply_changes`.
Agents emit structured `WorkflowCommand` objects only; unrestricted replacement
Python is forbidden. Every proposal uses the same validator and requires a
human-readable diff/approval path.

Acceptance: an agent can propose a safe parameter change, show its diff and
diagnostics, and apply it only through the validated command service.

## Testing, security, and packaging gates

- Golden tests for emitter markers and generated output.
- AST extraction/edit tests for positional, keyword, multiline, nested, and
  Unicode values; full-file parse/`compile()` before every save.
- Dependency-region immutability, stable identity, sidecar migration,
  retranslation replay, external-edit conflict, and API revision/ETag tests.
- Scope-tree, CSV-edge, reducer, node, inspector, and Playwright end-to-end
  flows for two tabs, dragging, editing, saving, reopening, and keyboard use.
- Prevent path traversal and arbitrary filesystem reads; restrict CSV previews
  by resolved path, byte/row limits, and timeout. Avoid executing translated
  code in the service. Keep writes atomic and log actionable diagnostics without
  leaking secrets.
- Package the frontend as static assets with the Python service; keep source
  maps/dev dependencies out of the runtime bundle where appropriate.

## Explicit decisions and defaults

- Default deployment is a local browser app; pywebview is deferred.
- React Flow is selected over native graph toolkits for the first UI.
- Standard-library AST editing is the default; LibCST is conditional, not a
  starting dependency.
- Python and the marked generated regions are authoritative; the sidecar stores
  presentation and replay metadata only.
- Human-approved, typed commands are the sole edit interface for future LLM use.
- No unrestricted code execution, dependency-region editing, or speculative
  plugin framework in the first release.

## Risks and mitigations

| Risk | Mitigation |
| --- | --- |
| Generated Python changes shape between compiler versions | Version markers/schema, source hashes, diagnostics, and migration tests |
| AST edits damage formatting or comments | Span-based minimal edits, golden fixtures, and conditional LibCST evaluation |
| Retranslation discards UI changes | Stable identities, sidecar overrides, conflict UI, never silent overwrite |
| Large workflows overwhelm the canvas | Collapsible scopes, deterministic layout, virtualization/performance budget |
| CSV previews expose sensitive data | Local-only service, path allow-list, bounded reads, explicit preview action |
| Unsupported Python gives false editability | Read-only raw-code cards and clear diagnostics |
| LLM proposes unsafe changes | Typed command schema, validation, diff, human approval, no arbitrary Python |
| Desktop packaging adds lifecycle complexity | Browser-first release; evaluate pywebview only after acceptance gates |

## Non-goals for the first release

- Reimplementing the VG2 compiler in the UI.
- Editing embedded dependency/helper code.
- Arbitrary Python/code execution from the editor or agent.
- Full visual programming-language authoring or a general plugin marketplace.
- Running translated pipelines automatically from the web service.
- Electron/Tauri or native-only packaging before browser UX is proven.

## Progress log

Update this section at each handoff with date, stage, changed files, validation,
and follow-up. At the time this plan was created, no implementation work had
been completed.

| Date | Stage | Status | Notes |
| --- | --- | --- | --- |
| 2026-08-09 | Planning/research | `[x]` | Architecture, boundaries, staged acceptance criteria, risks, and non-goals recorded. No code implemented. |
| 2026-08-09 | 0 | `[x]` | Existing representative fixtures adopted; marker/schema/type/read-only contracts encoded in code and focused tests. Baseline drift recorded: the untouched full suite has 8 failures and 4 errors in legacy CLI/emitter/classifier tests. |
| 2026-08-09 | 1 | `[~]` | Added compiler facade, structured diagnostics, metadata mapping, and deterministic generated regions. Focused tests pass; legacy full-suite drift prevents a clean repository-wide gate. |
| 2026-08-09 | 2 | `[x]` | Added versioned Pydantic workflow/sidecar models, marked-region AST parsing, stable graph construction, workspace-safe document service, and open/batch/layout APIs. |
| 2026-08-09 | 3 | `[x]` | Added React/TypeScript multi-tab canvas, custom nodes/edges, navigator, inspector, diagnostics, deterministic/persisted layout, and static serving. Production build and interactive browser smoke tests pass for two translated tabs, selection, read-only indicators, dragging, and layout reopening without Python changes. |
| 2026-08-11 | 4 | `[x]` | Added registry-derived utility metadata, typed editors, per-tab history, validation/diff preview, atomic apply, persisted overrides, and hash/revision conflicts. |
| 2026-08-11 | 5 | `[x]` | Added producer/consumer highlighting, conditional and ordering diagnostics, and bounded workspace-safe CSV preview. |
| 2026-08-11 | 6 | `[x]` | Added keyboard and screen-reader states, high-contrast/reduced-motion support, visible-node rendering, responsive polish, and packaged production assets. Native wrapper intentionally deferred. |
| 2026-08-11 | 7 | `[x]` | Added constrained workflow/step/set/validate/diff/apply endpoints using the same command validator and atomic persistence service as human edits. |
