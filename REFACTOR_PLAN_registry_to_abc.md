# Refactor Plan: `_registry.py` → ABC `__init_subclass__`

## 1. Current State

### `_registry.py` (L1–143)
Central registry module. Global dicts (`UTILITIES`, `UTILITY_IMPORTS`, `UTILITY_DEPENDENCIES`, `KIND_HANDLERS`, `CLASS_TO_UTILITY_NAME`) populated by `register_utility` decorator. Provides `mark_utility_used` (recursive dep-walk gating emission), `assemble_registered_utilities` (ordered source assembly with "is used?" filtering), `get_registered_source` + `_strip_embed_artifacts` (source extraction via `inspect.getsource`).

### `_base.py` (L1–24)
ABC with ClassVars: `utility_name`, `utility_imports`, `utility_dependencies`, `handles`. One classmethod `emit_block` returning `None` by default.

### `_emit_types.py` (L1–52)
`RawExpr` dataclass, `strip_quotes`, `option_to_python_expr`, `resolve_output_path`. No registry dependency.

### `_emit_helpers.py` (L1–115)
`emit_block` (dispatches via `KIND_HANDLERS`), `render_method_call` (calls `mark_utility_used`), `_emit_step_source`, `_step_name`, placeholder regex/helpers. Imports `KIND_HANDLERS` + `mark_utility_used` from `_registry`.

### `__init__.py` (L1–47)
Re-exports from `_registry`. Imports all concrete utility modules to trigger `@register_utility`. Public entry for `assemble_registered_utilities` and `emit_block`.

### `kind_enum.py` (L1–46)
Defines a runtime `Kind` str-enum + `KindEnum` wrapper emitting the enum source via `__vg2c_source__`. **To be deleted** — Kind enum will no longer be emitted into generated scripts.

### Concrete utilities (10 files + `emitter/readers.py`)
Each decorated with `@register_utility`, declares `utility_name`, `utility_imports`, optionally `utility_dependencies` and `handles`.

### `_registry.py` symbol classification

| Symbol | Lines | Destination |
|--------|-------|-------------|
| `UTILITIES` dict | 24 | → `_base.py` class-level `_registry` dict on `UtilitySpec` |
| `UTILITY_IMPORTS` dict | 27 | **DELETE** — imports collected via AST |
| `UTILITY_DEPENDENCIES` dict | 30 | **DELETE** — deps inferred via AST |
| `KIND_HANDLERS` dict | 34 | → `_base.py` class-level `_kind_handlers` dict on `UtilitySpec` |
| `CLASS_TO_UTILITY_NAME` dict | 37 | **DELETE** — zero external consumers |
| `register_utility()` | 40–82 | **DELETE** → replaced by `__init_subclass__` |
| `mark_utility_used()` | 85–91 | **DELETE** — all utilities always emitted |
| `assemble_registered_utilities()` | 94–117 | Rewritten as `assemble_all_utilities()` in `__init__.py` |
| `get_registered_source()` | 120–127 | → classmethod `get_source()` on `UtilitySpec` |
| `_CLASS_SIG_RE` | 130 | → `_base.py` (co-located with `get_source`) |
| `_strip_embed_artifacts()` | 133–143 | → `_base.py` (co-located with `get_source` to avoid circular import with `_emit_helpers`) |

---

## 2. Target State

### `_base.py`
```
UtilitySpec(ABC):
    # ClassVars kept
    utility_name: ClassVar[str]
    handles: ClassVar[tuple[Kind, ...]] = ()

    # ClassVars REMOVED: utility_imports, utility_dependencies

    # Class-level registries (populated by __init_subclass__)
    _registry: ClassVar[dict[str, type[UtilitySpec]]] = {}
    _kind_handlers: ClassVar[dict[Kind, type[UtilitySpec]]] = {}

    def __init_subclass__(cls, **kw):
        - Skip abstract subclasses (no utility_name)
        - Validate: non-empty utility_name, no duplicate names
        - Register in _registry[cls.utility_name] = cls
        - For each Kind in cls.handles: validate no duplicate handler, populate _kind_handlers

    @classmethod
    def get_source(cls) -> str:
        - Check __vg2c_source__ override
        - Fallback: inspect.getsource(cls) → _strip_embed_artifacts

    @classmethod
    def emit_block(cls, ctx, block, dispatched) -> tuple[str, str] | None:
        return None  # override in subclasses
```

`_strip_embed_artifacts` + `_CLASS_SIG_RE` defined in `_base.py` (NOT in `_emit_helpers.py`) to avoid circular import: `_emit_helpers.py` → `_base.py` → `_emit_helpers.py`.

### `_emit_types.py`
Unchanged.

### `_emit_helpers.py`
- `emit_block`: access `UtilitySpec._kind_handlers` from `_base` (replaces `KIND_HANDLERS` from `_registry`)
- `render_method_call`: remove `mark_utility_used(ctx, utility_name)` call
- Remove all imports from `_registry`

### `_topo_sort.py` (NEW)
Pure topological sort function. No dependencies on other project modules.

### `__init__.py`
- New `assemble_all_utilities()` function:
  1. Get all registered subclasses from `UtilitySpec._registry`
  2. AST-scan each utility's source file → build dep graph + collect external imports
  3. Topo-sort via `_topo_sort.topological_sort`
  4. Collect source for each utility via `cls.get_source()`
  5. Group + dedup + sort imports
  6. Return `(import_lines, utility_sources)`
- Still imports all concrete utility modules (triggers `__init_subclass__`)
- Removes all re-exports of deleted registry symbols

### `kind_enum.py` → **DELETED**
Kind enum no longer emitted in generated scripts.

### `_registry.py` → **DELETED**

### Concrete utility subclasses
- Remove `@register_utility` decorator + its import
- Remove `utility_imports` ClassVar
- Remove `utility_dependencies` ClassVar
- Keep: `utility_name`, `handles`, `emit_block`, runtime methods

### `pipeline_context.py`
- Remove hardcoded `__init__` assignments (`self.macro = MacroState()` etc.)
- Dynamic: iterate `UtilitySpec._registry`, do `setattr(self, name, cls())` for each non-ctx utility
- Add `__getattr__` fallback → prints "not implemented yet" for unregistered utilities
- Remove explicit imports of concrete utility classes
- Remove explicit `utility_dependencies` tuple

### `emitter/models.py`
- Remove `needed_utilities: set[str]` field from `EmitContext`

---

## 3. Topological Sort Design

**File:** `src/vg2c/emitter/utilities/_topo_sort.py`

**Signature:**
```python
def topological_sort(
    nodes: dict[str, type],
    edges: dict[str, set[str]],
) -> list[str]:
```

- **Input:** `nodes` = `{utility_name: cls}`, `edges` = `{utility_name: {dep_names}}`
- **Output:** list of utility names, dependencies first
- **Algorithm:** Kahn's (BFS). Process zero-indegree nodes alphabetically → stable deterministic output
- **Cycle:** raises `ValueError` listing the cycle participants
- **Tiebreaker:** alphabetical by `utility_name` for nodes at same depth — simple, stable, no extra code

---

## 4. AST Import Collection Design

**Location:** helper function in `__init__.py` (only consumer is `assemble_all_utilities`)

**Collected nodes:** `ast.Import`, `ast.ImportFrom` — top-level statements only

**Per-utility file, AST produces two sets:**

| Category | Rule | Usage |
|----------|------|-------|
| External imports | Module does NOT start with `vg2c.` | → include in emitted import block |
| Inter-utility deps | `from vg2c.emitter.utilities.<module> import ...` where `<module>` is not `_`-prefixed (i.e., a concrete utility module) | → edge in dependency graph |
| Translator internals | All other `vg2c.*` imports (`_base`, `_emit_helpers`, `_emit_types`, `frontend.models`, etc.) | → discard |

**Dedup key:** normalized import statement string (e.g. `"from pathlib import Path"`)

**Rendering — grouped (PEP 8 style):**
1. Stdlib imports (detect via `sys.stdlib_module_names`)
2. Third-party imports (`pandas`, `datasyncx`, etc.)
3. Blank line between groups; each group alphabetically sorted

**`readers.py` note:** lives in `vg2c.emitter.readers`, not `vg2c.emitter.utilities.*`. It registers via `__init_subclass__` when imported by `__init__.py`. Its imports of `datasyncx.*` are external → included. It imports no other concrete utility → no dep edges.

---

## 5. Emit Site Changes (`__init__.py`)

### New flow — `assemble_all_utilities()`
1. Collect `UtilitySpec._registry` → all registered utilities
2. For each class: `inspect.getfile(cls)` → AST-parse → extract external imports + inter-utility dep edges
3. `topological_sort(registry, edges)` → ordered names
4. For each name in order: `cls.get_source()` → collect source strings
5. Group + dedup + sort external imports → rendered import block
6. Return `(import_lines: list[str], utility_sources: list[str])`

### Removals in `emitter/__init__.py`
- `mark_utility_used(ctx, "ctx")` (L29) → delete
- `mark_utility_used(ctx, "kind_enum")` (L30) → delete
- Replace `assemble_registered_utilities(ctx)` with `assemble_all_utilities()` (no `ctx` arg — not gated on `needed_utilities`)
- `ctx.needed_utilities` never touched

---

## 6. Migration Steps

### Step 1 — Extend `_base.py` with `__init_subclass__`
**Goal:** ABC auto-registers subclasses. Both old (`@register_utility`) and new paths coexist temporarily.
**Files:** `_base.py`
**Changes:**
- Add `_registry`, `_kind_handlers` class dicts
- Add `__init_subclass__` with name/Kind validation
- Add `get_source()` classmethod + `_strip_embed_artifacts` + `_CLASS_SIG_RE`
- Remove `utility_imports`, `utility_dependencies` ClassVars from ABC signature
**Verify:** existing tests pass (decorators still fire, ABC also registers)

### Step 2 — Create `_topo_sort.py`
**Goal:** Pure topo-sort function, independently testable.
**Files:** `_topo_sort.py` (new)
**Verify:** unit tests (linear chain, diamond, cycle, no-deps, single, empty)

### Step 3 — Build AST import collector
**Goal:** Function to parse utility source file → `(external_imports, utility_deps)`.
**Files:** `__init__.py` (internal helper)
**Verify:** unit test against known utility file (e.g. `csv_io.py`)

### Step 4 — Implement `assemble_all_utilities` in `__init__.py`
**Goal:** New emit flow: registry → AST → topo-sort → grouped imports → sources.
**Files:** `utilities/__init__.py`
**Verify:** end-to-end emission test

### Step 5 — Remove `@register_utility` from all concrete utilities
**Goal:** Each utility auto-registers via `__init_subclass__` alone.
**Files:** `crosstab.py`, `csv_io.py`, `external.py`, `fs_ops.py`, `macro_state.py`, `mail.py`, `pipeline_context.py`, `sql_macros.py`, `sqlite_engine.py`, `emitter/readers.py`
**Per file:** remove `@register_utility` decorator, remove `from ... import register_utility`, remove `utility_imports` ClassVar, remove `utility_dependencies` ClassVar.
**Verify:** all tests pass

### Step 6 — Delete `kind_enum.py`
**Goal:** Kind enum no longer emitted.
**Files:** `kind_enum.py` (delete), `utilities/__init__.py` (remove import), `emitter/__init__.py` (remove `mark_utility_used(ctx, "kind_enum")`)
**Verify:** emitted scripts lack Kind enum; golden test files updated

### Step 7 — Update `_emit_helpers.py`
**Goal:** Remove all `_registry` deps.
**Files:** `_emit_helpers.py`
**Changes:**
- `from ._base import UtilitySpec` (for `_kind_handlers`)
- `emit_block`: `UtilitySpec._kind_handlers.get(block.kind)` replaces `KIND_HANDLERS.get(block.kind)`
- `render_method_call`: remove `mark_utility_used(ctx, utility_name)` call
- Remove `from ._registry import ...`
**Verify:** all tests pass

### Step 8 — Update `walker.py`
**Goal:** Remove `mark_utility_used` calls.
**Files:** `emitter/walker.py`
**Changes:** remove `from ... import mark_utility_used`, remove all `mark_utility_used(ctx, ...)` calls (L163, etc.)
**Verify:** all tests pass

### Step 9 — Update `emitter/__init__.py`
**Goal:** Use new `assemble_all_utilities`.
**Files:** `emitter/__init__.py`
**Changes:**
- Import `assemble_all_utilities` instead of `assemble_registered_utilities, mark_utility_used`
- Remove `mark_utility_used(ctx, "ctx")` + `mark_utility_used(ctx, "kind_enum")`
- `utility_imports, utility_sources = assemble_all_utilities()` (no `ctx` arg)
**Verify:** end-to-end test

### Step 10 — Make `PipelineContext` dynamic
**Goal:** No hardcoded utility assignments; auto-discover from registry.
**Files:** `pipeline_context.py`
**Changes:**
- Remove explicit imports of `CrosstabUtility`, `CsvIO`, `ExternalProcess`, etc.
- `__init__`: loop over `UtilitySpec._registry`, `setattr(self, name, cls())` for each entry except `"ctx"`
- Add `__getattr__`: return stub that prints "not implemented yet" for unknown attrs
- Remove `utility_dependencies` tuple
**Verify:** runtime tests pass

### Step 11 — Remove `needed_utilities` from `EmitContext`
**Goal:** Dead field cleanup.
**Files:** `emitter/models.py`
**Changes:** delete `needed_utilities: set[str] = field(default_factory=set)` (L29)
**Verify:** no references remain (`grep`)

### Step 12 — Delete `_registry.py`
**Goal:** Final deletion.
**Files:** `_registry.py` (delete)
**Verify:** `grep -r "_registry" src/ tests/` → zero hits. Full test suite passes.

### Step 13 — Clean up `__init__.py` exports
**Goal:** `__all__` reflects new API surface.
**Files:** `utilities/__init__.py`
**Changes:** remove stale re-exports (`UTILITIES`, `UTILITY_DEPENDENCIES`, `UTILITY_IMPORTS`, `register_utility`, `mark_utility_used`, `get_registered_source`). Keep: `assemble_all_utilities`, `emit_block`, concrete utility classes, `ReaderRuntime`.
**Verify:** all tests pass

---

## 7. Call-Site Impact

| File | Current Import | Replacement |
|------|---------------|-------------|
| `emitter/__init__.py` (L5–7) | `from .utilities import assemble_registered_utilities, mark_utility_used` | `from .utilities import assemble_all_utilities` |
| `emitter/walker.py` (L13) | `from .utilities._registry import mark_utility_used` | *(removed entirely)* |
| `emitter/utilities/_emit_helpers.py` (L7–9) | `from ._registry import KIND_HANDLERS, mark_utility_used` | `from ._base import UtilitySpec` — use `UtilitySpec._kind_handlers` |
| `emitter/utilities/__init__.py` (L8–16) | `from ._registry import UTILITIES, UTILITY_DEPENDENCIES, UTILITY_IMPORTS, assemble_registered_utilities, get_registered_source, mark_utility_used, register_utility` | *(removed; `assemble_all_utilities` defined locally)* |
| `tests/runtime/test_write_file_and_readers.py` (L10) | `from vg2c.emitter.utilities import get_registered_source` | `from vg2c.emitter.utilities._base import UtilitySpec` → `UtilitySpec._registry["reader_runtime"].get_source()` or import `ReaderRuntime` directly and call `ReaderRuntime.get_source()` |
| All 10 concrete utility files + `readers.py` | `from ._registry import register_utility` | *(removed)* |

---

## 8. Test Strategy

All tests run in local `.venv`.

### `_topo_sort.py` unit tests
- Linear chain: A → B → C → order `[C, B, A]`
- Diamond: A → {B, C}, B → D, C → D → `D` before `B`/`C` before `A`
- Cycle: A → B → A → `ValueError` raised
- No deps: alphabetical order
- Single node: `[A]`
- Empty input: `[]`

### AST import collector unit tests
- Parse `csv_io.py` → external imports = `{"import csv", "from pathlib import Path", "from typing import Any, Iterator", "import pandas"}`, inter-utility deps = `set()`
- Parse `pipeline_context.py` → deps = `{"crosstab", "csv_io", "external", ...}`, external imports = `{"from typing import Any, ContextManager"}`
- Verify `vg2c.*` imports excluded from external set
- Verify dedup across multiple utilities

### End-to-end emission tests
- Emit against existing VG2 fixture files in `tests/emitter/fixtures/`
- Update golden expected files to reflect: no Kind enum, grouped imports, all utilities always present
- Verify emitted script passes `ast.parse`

---

## 9. Definition of Done

- [ ] `_registry.py` deleted
- [ ] No import of `vg2c.emitter.utilities._registry` anywhere in codebase
- [ ] `_base.py` auto-registers all subclasses via `__init_subclass__`
- [ ] `_topo_sort.py` exists, unit-tested, detects cycles with `ValueError`
- [ ] AST import collector exists, unit-tested, filters intra-package imports
- [ ] `__init__.py` emits **all** utilities in topo-sorted order with grouped deduped import block
- [ ] `kind_enum.py` deleted; Kind enum no longer emitted in generated scripts
- [ ] No `mark_utility_used` / `needed_utilities` logic remains
- [ ] `CLASS_TO_UTILITY_NAME` deleted
- [ ] `utility_imports` / `utility_dependencies` ClassVars removed from all utilities
- [ ] `PipelineContext` uses dynamic `setattr` loop + `__getattr__` fallback
- [ ] All symbols with no remaining references deleted
- [ ] No shims, aliases, or re-exports from old registry
- [ ] Tests pass in local `.venv`, including end-to-end against real VG2 fixture
