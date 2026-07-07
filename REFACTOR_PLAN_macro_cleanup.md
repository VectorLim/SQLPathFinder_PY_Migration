# Emitter Macro Cleanup Refactor Plan

## 1. Current State Analysis

### File Responsibilities Today
- `src/vg2c/emitter/utilities/macro_state.py`
  - UtilitySpec runtime class for macro stack storage, placeholder substitution, file writes, and `MACRO_CONTROL` emit path.
  - Also contains crosstab SQL expansion logic (misplaced concern).
- `src/vg2c/emitter/macro.py`
  - Legacy consolidated module with duplicate macro state logic, duplicate placeholder/crosstab patterns, compile-time placeholder helpers, and crosstab helpers.
  - Must be removed.
- `src/vg2c/emitter/utilities/_emit_helpers.py`
  - General emitter helper functions (`emit_block`, render helpers). Intended new home for compile-time macro expression helpers.
- `src/vg2c/emitter/utilities/crosstab.py`
  - Runtime crosstab utility class (`CrosstabUtility.apply`) for DataFrame pivoting.
  - Intended new home for all crosstab-specific macro SQL logic.

### Redundancy Map (`macro.py` vs `macro_state.py`)

| Duplicated symbol/concept | Current location(s) | Authority after refactor | Action |
|---|---|---|---|
| Placeholder regexes (`PLACEHOLDER_RE`, `NAMED_PLACEHOLDER_RE`) | `macro.py` L47-48, `macro_state.py` L43-44 | `macro_state.py` for runtime; `_emit_helpers.py` for compile-time usage | Keep runtime in `macro_state.py`; compile-time consumers import helper APIs from `_emit_helpers.py` |
| Crosstab regex (`CROSSTAB_RE`) | `macro.py` L49-51, `macro_state.py` L45-47 | `crosstab.py` | Move to module-level in `crosstab.py`; remove from `macro_state.py` |
| SQL selected-column parser (`_extract_selected_columns_by_alias`) | `macro.py` L54-81, `macro_state.py` L50-71 | `crosstab.py` | Move to `crosstab.py`; remove duplicates |
| SQL crosstab substitution (`substitute_crosstab`) | `macro.py` L135-164, `macro_state.py` L81-106 | `crosstab.py` | Single module-level function in `crosstab.py`; call from all sites |
| Macro name normalizer (`normalize_macro_name`) | `macro.py` L167-172, `macro_state.py` L74-78 | `macro_state.py` | Keep canonical in `macro_state.py`; compile-time helper references canonical behavior |
| Macro lookup protocol (`MacroLookup`) | `macro.py` L175-180, `macro_state.py` L22-27 | `macro_state.py` | Keep only in `macro_state.py` |
| Runtime state class (`MacroState`) | `macro.py` L188+, `macro_state.py` L31+ | `macro_state.py` | Keep UtilitySpec-backed class only |

### Unique symbols in `macro.py` (not in `macro_state.py`)
- `placeholders_to_python_expr` (`macro.py` L308-342): move to `_emit_helpers.py`.
- `macro_token_to_python_expr` (`macro.py` L303-305): move to `_emit_helpers.py`.
- `apply_crosstab` (`macro.py` L95-132): move to `crosstab.py` (module-level helper).
- `_ci_get` (`macro.py` L84-92): dead code (no usage found), delete.

### Crosstab Extraction Table (required cited ranges in `macro_state.py`)

| Cited range | What it represents | Why crosstab-specific | Planned destination |
|---|---|---|---|
| L35 | `handles = (Kind.MACRO_CONTROL,)` | Not crosstab; macro control handler declaration | Keep in `macro_state.py` |
| L36-L38 | Start of `utility_imports` bundle | Imports used by mixed concerns; currently includes crosstab dependency footprint | Keep only imports needed by non-crosstab macro state logic |
| L39-L62 | `utility_imports` tail + regex setup + beginning of selected-column parser | Introduces SQL crosstab token pattern and parser setup | Move crosstab regex/parser logic to `crosstab.py` |
| L71-L97 | End of parser + start of crosstab substitution method | SQL crosstab expansion flow | Move to `crosstab.py` |
| L143-L155 | `substitute_sql` entry and crosstab call path | Runtime macro substitution currently coupled to crosstab expander | Keep named-placeholder substitution in `macro_state.py`; delegate crosstab to `crosstab.py` function |

## 2. Target State

### Final Module Shapes
- `src/vg2c/emitter/utilities/macro_state.py`
  - Lean runtime macro state only: frame stack, named/positional lookup, write-file substitution, condition eval, scope management, `emit_block`.
  - No crosstab regex, no crosstab parser, no crosstab substitution implementation.
  - `substitute_sql` delegates crosstab expansion to `crosstab.py`.
- `src/vg2c/emitter/utilities/_emit_helpers.py`
  - Existing emit helpers plus moved compile-time macro expression helpers:
    - `macro_token_to_python_expr`
    - `placeholders_to_python_expr`
  - Becomes canonical home for emitter-time placeholder expression lowering.
- `src/vg2c/emitter/utilities/crosstab.py`
  - Existing `CrosstabUtility` class.
  - New canonical crosstab support functions:
    - `CROSSTAB_RE`
    - `_extract_selected_columns_by_alias`
    - `substitute_crosstab`
    - `apply_crosstab`
- `src/vg2c/emitter/macro.py`
  - Deleted.

### Public API Surface After Change
- `macro_state.py`: `MacroLookup`, `MacroState` (runtime macro behavior only).
- `_emit_helpers.py`: `_emit_step_source`, `_render_value`, `_step_name`, `emit_block`, `render_method_call`, `macro_token_to_python_expr`, `placeholders_to_python_expr`.
- `crosstab.py`: `CrosstabUtility`, `substitute_crosstab`, `apply_crosstab` (and internal parser helpers).
- `macro.py`: no API (file removed).

## 3. Migration Plan (Ordered Steps)

### Step 1: Move compile-time placeholder helpers into `_emit_helpers.py`
- Goal: make `_emit_helpers.py` canonical compile-time home before deleting `macro.py`.
- Files touched: `_emit_helpers.py`, `src/vg2c/emitter/utilities/_emit_types.py`, `src/vg2c/emitter/walker.py`.
- Symbols moved:
  - `vg2c.emitter.macro.macro_token_to_python_expr` -> `vg2c.emitter.utilities._emit_helpers.macro_token_to_python_expr`
  - `vg2c.emitter.macro.placeholders_to_python_expr` -> `vg2c.emitter.utilities._emit_helpers.placeholders_to_python_expr`
- Call sites updated:
  - `_emit_types.py`: import source change for `placeholders_to_python_expr`.
  - `walker.py`: import source change for `macro_token_to_python_expr` and `NAMED_PLACEHOLDER_RE` dependency used in operand parsing.
- Risk / blast radius: low (compile-time expression generation only).
- Verification: emitter tests and golden comparisons pass.

### Step 2: Move crosstab SQL helpers into `crosstab.py`
- Goal: centralize crosstab logic into its domain module.
- Files touched: `crosstab.py`.
- Symbols moved:
  - `vg2c.emitter.macro.CROSSTAB_RE` -> `vg2c.emitter.utilities.crosstab.CROSSTAB_RE`
  - `vg2c.emitter.macro._extract_selected_columns_by_alias` -> `vg2c.emitter.utilities.crosstab._extract_selected_columns_by_alias`
  - `vg2c.emitter.macro.substitute_crosstab` -> `vg2c.emitter.utilities.crosstab.substitute_crosstab`
  - `vg2c.emitter.macro.apply_crosstab` -> `vg2c.emitter.utilities.crosstab.apply_crosstab`
- Call sites updated in later steps.
- Risk / blast radius: medium (runtime SQL rewrite path).
- Verification: `test_substitute_crosstab_*` and `test_apply_crosstab_*` behavior unchanged.

### Step 3: Strip crosstab implementation from `macro_state.py`
- Goal: leave single-responsibility macro state class.
- Files touched: `macro_state.py`.
- Symbols deleted/moved from class:
  - `MacroState.CROSSTAB_RE` (delete)
  - `MacroState._extract_selected_columns_by_alias` (delete)
  - `MacroState.substitute_crosstab` (delete)
  - `MacroState.substitute_sql` delegates to `vg2c.emitter.utilities.crosstab.substitute_crosstab`
- Call sites updated: internal call in `MacroState.substitute_sql`.
- Risk / blast radius: medium (all runtime SQL macro substitution paths).
- Verification: runtime macro tests + crosstab SQL expansion tests + e2e short fixture test.

### Step 4: Update remaining production imports away from `macro.py`
- Goal: eliminate production dependency on deleted module.
- Files touched: `src/vg2c/emitter/sqlite_engine.py`, `src/vg2c/emitter/utilities/_emit_types.py`, `src/vg2c/emitter/walker.py`.
- Import changes:
  - `substitute_crosstab`: `vg2c.emitter.macro` -> `vg2c.emitter.utilities.crosstab`
  - placeholder helper imports: `vg2c.emitter.macro` -> `vg2c.emitter.utilities._emit_helpers`
- Risk / blast radius: low to medium (import graph + SQL runtime path).
- Verification: import smoke check + targeted runtime/emitter tests.

### Step 5: Update test imports to canonical modules
- Goal: remove test-side references to `macro.py`.
- Files touched: `tests/runtime/test_macro_state.py`, `tests/runtime/test_write_file_and_readers.py`.
- Import changes:
  - `MacroState`: `vg2c.emitter.macro` -> `vg2c.emitter.utilities.macro_state`
  - `apply_crosstab`, `substitute_crosstab`: `vg2c.emitter.macro` -> `vg2c.emitter.utilities.crosstab`
- Risk / blast radius: low.
- Verification: both runtime test files pass.

### Step 6: Delete `macro.py` and remove dead code
- Goal: complete consolidation and enforce no shim policy.
- Files touched: delete `src/vg2c/emitter/macro.py`; cleanup unused imports/comments in touched files.
- Symbols deleted:
  - Entire `vg2c.emitter.macro` module.
  - `_ci_get` (dead, unused).
- Call sites updated: none remaining by design.
- Risk / blast radius: medium if any hidden imports remain.
- Verification:
  - repo-wide search confirms zero `vg2c.emitter.macro` imports.
  - full test suite green.

Import stability note: steps ordered to keep tree importable; module deletion is last.

## 4. Call-Site Impact Inventory

All files outside the 4 in-scope files that currently import from `macro.py` or consume migrated concerns:

1. `src/vg2c/emitter/sqlite_engine.py` (line 12)
   - Old: `from vg2c.emitter.macro import substitute_crosstab`
   - New: `from vg2c.emitter.utilities.crosstab import substitute_crosstab`
2. `src/vg2c/emitter/utilities/_emit_types.py` (line 6)
   - Old: `from vg2c.emitter.macro import placeholders_to_python_expr`
   - New: `from vg2c.emitter.utilities._emit_helpers import placeholders_to_python_expr`
3. `src/vg2c/emitter/walker.py` (line 6 block)
   - Old: `from vg2c.emitter.macro import (NAMED_PLACEHOLDER_RE, macro_token_to_python_expr)`
   - New: `from vg2c.emitter.utilities._emit_helpers import (NAMED_PLACEHOLDER_RE, macro_token_to_python_expr)`
4. `tests/runtime/test_macro_state.py` (line 7)
   - Old: `from vg2c.emitter.macro import MacroState, apply_crosstab, substitute_crosstab`
   - New: `from vg2c.emitter.utilities.macro_state import MacroState`
   - New: `from vg2c.emitter.utilities.crosstab import apply_crosstab, substitute_crosstab`
5. `tests/runtime/test_write_file_and_readers.py` (line 9)
   - Old: `from vg2c.emitter.macro import MacroState`
   - New: `from vg2c.emitter.utilities.macro_state import MacroState`
6. `src/vg2c/emitter/utilities/__init__.py` (line 22)
   - Current: `from vg2c.emitter.utilities.macro_state import MacroState`
   - Change: none required.
7. `src/vg2c/emitter/utilities/pipeline_context.py` (line 14)
   - Current: `from vg2c.emitter.utilities.macro_state import MacroState`
   - Change: none required.

## 5. Risks, Edge Cases, and Open Questions

### Risks / hidden coupling
- `macro_state.py` currently bundles runtime substitution and crosstab path in one method; moving parts can break subtle SQL formatting if replacement separators differ.
- `src/vg2c/emitter/utilities/sqlite_engine.py` has its own independent crosstab substitution implementation (parallel logic). Not in this scope, but behavior drift risk remains.
- Generated/golden script fixtures embed emitted utility code; moving helper ownership can require fixture refresh.

### Ambiguities to resolve before coding
1. `normalize_macro_name` ownership for compile-time helper:
   - Should `_emit_helpers.py` call `MacroState.normalize_macro_name`, or define module-level function with same semantics?
2. Regex ownership for `NAMED_PLACEHOLDER_RE` in compile-time path:
   - Should `_emit_helpers.py` define its own constant, or import from `macro_state.py`?
3. Should `apply_crosstab` remain module-level in `crosstab.py` (for tests and non-UtilitySpec use) in addition to `CrosstabUtility.apply`?

## 6. Test Strategy

### Existing tests covering affected paths
- `tests/runtime/test_macro_state.py` (MacroState behavior, crosstab substitution, crosstab pivot helper).
- `tests/runtime/test_write_file_and_readers.py` (MacroState write-file behavior).
- `tests/runtime/test_e2e_short.py` (end-to-end fixture pipeline).
- Emitter golden tests under `tests/emitter/` (compile-time helper impact).

### New tests to add before/with refactor (if gaps observed)
- Optional focused test asserting compile-time helper import location (`_emit_helpers.py`) still produces identical expression strings.
- Optional focused test asserting `MacroState.substitute_sql` delegates to crosstab helper without behavior change.

### Execution cadence
- Run targeted runtime + emitter tests after each major step.
- Run end-to-end tests with real VG2 fixtures after each major step boundary (after Step 3 and after Step 6).

## 7. Definition of Done

- [ ] `macro.py` deleted from the repository.
- [ ] No file in the repo imports from `vg2c.emitter.macro`.
- [ ] All cited crosstab line ranges have been removed from `macro_state.py` and live in `crosstab.py`.
- [ ] `macro_state.py` contains no crosstab references.
- [ ] No compatibility shims, aliases, or re-exports exist for the moved symbols.
- [ ] All tests pass, including end-to-end tests against real VG2 fixtures.
- [ ] Dead code and boilerplate from this reshuffle removed (unused imports, stale comments, pass-through leftovers).