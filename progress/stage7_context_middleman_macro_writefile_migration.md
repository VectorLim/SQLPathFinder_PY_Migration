# Context middleman + macro/write_file migration

Date: 2026-06-26

## What was changed

1. Moved write-file implementation into emitter:
- Added src/vg2c/emitter/write_file.py with the full placeholder substitution + file write logic.
- Kept src/vg2c_runtime/write_file.py as a compatibility shim that re-exports write_file from emitter.

2. Combined context-owned logic into MacroState:
- Updated src/vg2c/emitter/macro.py:
  - Added MacroState.write_file(path, template, vars=None) to centralize file-writing behavior in macro state.
  - Added MacroState.eval_condition(lhs, op, rhs) for legacy condition behavior.

3. Reduced PipelineContext to a middleman holder/delegator:
- Updated src/vg2c_runtime/context.py:
  - Removed direct dependency on runtime write_file implementation.
  - macro_scope now delegates to MacroState.scope.
  - write_file now delegates to MacroState.write_file.
  - eval_condition now delegates to MacroState.eval_condition.
  - Removed now-unused imports.

4. Updated runtime package export path:
- Updated src/vg2c_runtime/__init__.py to export write_file from vg2c.emitter.write_file.

## Dependency resolution notes

- Kept backward compatibility for existing imports of vg2c_runtime.write_file.write_file by turning that file into a shim.
- Avoided circular imports by making emitter/write_file.py depend on a lightweight Protocol instead of importing MacroState.

## Validation

- Passed:
  - tests/runtime/test_macro_state.py
  - tests/runtime/test_write_file_and_readers.py

- Ran but failing (appears unrelated to this refactor):
  - tests/runtime/test_e2e_short.py::test_e2e_script_short
  - Failure: MarsReader.read() missing required positional argument: site
