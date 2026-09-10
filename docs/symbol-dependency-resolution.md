# Standalone symbol dependency resolution

## 1. Current architecture

Compilation still follows `parse → classify → resolve → analyze → dispatch → emit`.
Utility registration/classification and semantic operation metadata remain compiler-side.
The emitter now produces steps before resolving their runtime dependencies.

The replacement has three internal responsibilities:

| Responsibility | Implementation | Output |
| --- | --- | --- |
| Source indexing | `utilities/_symbol_index.py` | Module bindings, symbols, class members, initialization groups |
| Root selection and closure | `utilities/_symbols.py` | Selected nodes, reasoned dependency edges, context instances, fallback records |
| Ordering and rendering | `utilities/_symbol_emit.py` | Imports, definitions, generated context expression |

`assemble_utilities()` is the integration entry point. There is one dependency
model, including conservative selections; no legacy resolver switch remains.

## 2. What whole-file inclusion did

Previously, `assemble_all_utilities()` scanned every registered utility module,
followed module-name edges, and called `UtilitySpec.get_source()` for entire
classes. `_extract_module_body()` included every supported helper-module definition.
The result included classification/emission methods, `_base` infrastructure,
unrelated imports, and helper functions absent from the workflow's runtime calls.
Helper discovery was not fully recursive. A module cycle always failed ordering,
even when it consisted only of mutually recursive function bodies.

## 3. Real dependency examples

- `SqliteEngine` renders calls to `PipelineContext.run_query`; its own `check`,
  `emit_block`, and SQL-literal construction methods need not ship.
- `SqliteReader.execute` requires `_load_csv_as_table`, `_split_statements`,
  `STMT_SPLIT_RE`, and `CrosstabUtility.substitute_sql`. Its nested
  `_lookup_alias_columns` stays inside `execute`.
- `MacroState.scope` retains `@contextmanager`, `push_frame`, and `pop_frame`.
- `HtmlReport.layout` reaches `_render_report` through a lambda, then
  `_load_csv_rows`, `ctx.macro.resolve_file_path`, and `ctx.csv_io.iter`.
- `Logger._ensure_logger_class` passes `PrettyLogger` to logging; the nested class
  remains complete, including its dependency on `Logger._format_table`.
- `OracleClient.configure` and `log_active_client` retain their local `oracledb`
  imports. Merely loading a generated SQLite script does not promote those imports.

## 4. Symbol model

`SymbolRef(module, qualified_name)` identifies definitions. `ModuleIndex` maps
names to definitions/import bindings. A `Symbol` contains its source statements,
optional owning class, and members. Reassignments/property accessors are grouped.
`Dependency` records target, eager/deferred timing, and reason. `Selection` owns
the selected AST nodes, edges, context map, and `Fallback` records.

Selection starts from actual emitted operations, not all possible operations in
the registry. Modules are indexed only when reached. Indexing a reached module
does not mean emitting its complete contents.

## 5. Function resolution

Function bodies contribute deferred global dependencies; decorators/defaults
contribute definition-time dependencies. A lexical scope walk distinguishes
parameters, local assignments/imports, closures, `global`, `nonlocal`, lambdas,
comprehensions, exception bindings, and pattern bindings.

Nested functions and function-local classes remain in their owning definitions.
Calls executed in defaults/decorators also require the callee's runtime closure
to exist before invocation. Immediate lambdas and statically resolved method
calls participate in this rule. Mutually recursive function bodies are valid.

## 6. Class/member resolution

For ordinary classes, selecting a method retains the class shell and recursively
selects `self`, `cls`, and explicit class-member references. Construction/protocol
methods are retained conservatively. Properties retain their accessor group.
Literal class state is selected by use; effectful state and definition-time
initialization are retained even when a method is otherwise unused.

Real bases and runtime decorators are preserved. Only bindings resolved to the
known compiler bases/decorators are removed. Compiler state such as `handles`
does not become a runtime dependency.

Inheritance, metaclasses, dataclasses, custom decorators/descriptors, dynamic
attribute hooks, class reflection, and escaping class/instance values widen to
the complete affected class. Nested classes are retained intact. This intentionally
keeps more code than an aggressive optimizer would.

## 7. Imports

- Required external import bindings are emitted once. Original aliases remain
  valid. Side-effect-only imports are retained conservatively.
- Local external imports stay inside their original functions/guards/handlers.
- Local project symbols resolve through absolute/relative filesystem module paths;
  inspection does not import those source modules.
- Module-qualified project references are rewritten to selected bindings.
- Runtime-local project symbol imports use a private binding where needed to avoid
  an accidental `helper = helper` local-variable capture. These aliases are nodes
  in the same graph and are checked for collisions.
- Project imports requiring deferred initialization or actual module-object
  identity are rejected, rather than implemented through a custom loader.
- Flattened binding collisions, including external reader/class collisions, are
  errors rather than last-definition-wins behavior.

Postponed annotations are rendered as strings locally; the renderer does not turn
on one global future flag that would change eagerly evaluated annotations from
another source module. Known annotation dependencies remain available.

## 8. Cross-module closure

The resolver follows imported bindings/re-exports, member references, helper calls,
and constants until its worklist is empty. Shared symbols appear once. A newly
discovered helper can lead to another module, without needing a manually listed
helper-module root. Source names remain readable; conflicting global names produce
a diagnostic instead of introducing a general name-mangling/linker system.

## 9. Dynamic fallback and diagnostics

Every widening records the triggering symbol, location, boundary, and reason.
`symbol-fallback` warnings enter the existing compilation diagnostic collector.

| Construct | Policy |
| --- | --- |
| Literal `getattr`/`hasattr` on a known receiver | Select the referenced member/capability |
| Dynamic member name on a known class | Complete class and its dependencies |
| Instance escaping to an opaque callee | Complete owning class |
| Dynamic `globals()[name]` in one self-contained module | Complete module selection |
| Dynamic lookup needing distinct module namespaces | Clear `ResolutionError` |
| `eval`, `exec`, dynamic import execution, loader/code/namespace inspection | Clear `ResolutionError` |
| Fallback reaches compiler-only `_base`/emitter metadata code | Clear `ResolutionError` |

Raw embedded Python is scanned for its actual static calls, including nested
expressions. It can invoke ordinary `ctx` operations. Opaque behavior that cannot
be made standalone by widening is rejected. No new dependency-declaration decorator
or manual utility dependency list was introduced.

## 10. Ordering and initialization

All edges drive reachability. Only eager edges and observable initialization order
constrain definition order. The existing deterministic topological sorter is reused.
Class members retain source order. Definition-time calls expand their hard
dependencies before ordering. An unsatisfiable eager cycle reports an error.

Top-level initialization statements and observable definition effects are retained.
Imported modules' ordinary `if __name__ == '__main__'` entrypoints are excluded;
guards with an `else` require normalization and are rejected. The existing path
helper intentionally continues using the generated script's `__file__`.

## 11. Caching and performance

`SourceIndex` caches parsed modules for one resolver/compilation only. It is discarded
after compilation. The parse-count test verifies one parse of a source file reached
through multiple roots and verifies that an unrelated invalid module is not parsed.
There is no persistent/global source cache or repository-wide analysis phase.

## 12. Compiler/emitter integration

`walk_and_emit()` runs first. Roots combine `StepEmission.invocations`, AST
references from exact step/workflow bodies, bootstrap calls, and dispatched readers.
The AST scan is needed because nested `CodeExpr` calls are not all top-level
semantic invocations.

Registration stays automatic at compile time. Generated context construction is
explicit, for example:

```python
ctx = PipelineContext({'csv_io': CsvIO(), 'macro': MacroState()})
```

The compiler generates this map from its closure; a developer does not maintain it.
The internal `PipelineContext` constructor now accepts that instance mapping.
The existing utility-registration import convention remains unchanged.

`Logger.basicConfig`, `Logger.condition`, and `OracleClient.configure` remain roots
when called by generated code. Oracle startup policy was not changed. Steps remain
verbatim so final edit/semantic spans and SQL-filter line references can still be
calculated by the existing finalization path. Public compilation/CLI result shapes
and region markers remain unchanged.

## 13. Refactored modules

The emitter now assembles dependency roots after step emission. Utilities' package
entry point delegates indexing/resolution/rendering. `_base.py` remains responsible
for registration and classification rather than source extraction. The runtime
context accepts explicit instances; its repository callers were migrated.

Generated execution also exposed a pre-existing SmartAppend emission defect:
already-rendered path strings were being rendered again. The emitter now passes
`MacroState.to_code_expr()` values, preserving correct filenames and semantic
argument metadata. Its stale result-access test now uses `result.emitted.source`.

## 14. Deleted redundant paths

- `assemble_all_utilities` and its implicit include-everything default.
- `UtilityDependencyInfo` and parallel import/helper/module dependency maps.
- Separate helper traversal and whole helper-body extraction.
- Blanket function-import promotion and source-line stripping.
- `UtilitySpec.get_source`, regex base removal, and unused `__vg2c_source__` hook.
- Runtime context discovery through `_registry` and `globals()`.
- Manual logger-first ordering and unconditional `Kind` source inclusion.

## 15. Migration stages

1. Captured fresh fixture byte/definition counts without overwriting existing artifacts.
2. Added the per-compilation index, worklist closure, scoped dependency walk, and renderer.
3. Integrated emitted roots and automatic context instances; migrated direct callers.
4. Added conservative class/member selection, import handling, ordering, and diagnostics.
5. Replaced obsolete tests, deleted the old path, and exercised actual standalone outputs.

No production compatibility switch or duplicate implementation remains. Existing
user-modified generated scripts and data files are not migration inputs and were
not regenerated in place.

## 16. Tests

`tests/emitter/test_symbol_resolution.py` covers functions/transitivity, aliases,
relative imports, shadowing/closures, classes/state/properties, inheritance,
dataclasses/nested classes, decorators/default evaluation, recursion, eager cycles,
local imports, fallback, collisions, definition effects, and parse caching.

`tests/runtime/test_generated_symbols.py` writes actual compiler output to temporary
files and executes it in isolated subprocesses. An import blocker rejects every
`vg2c` import. Cases verify exact SQLite CSV rows, repeated SmartAppend headers/rows,
macro scopes/filesystem operations, rendered HTML contents, and a controlled
DataSyncX/Oracle double. Repeat compilation and semantic source slices are checked.
No live Oracle/DataSyncX service validation is claimed.

Run the repository's existing environment:

```powershell
.\.venv\Scripts\python.exe -m pytest -q
```

## 17. Acceptance evidence

All 17 fresh fixture scripts became smaller and compiled successfully. Counts below
include all function/method definitions in each complete generated script, including
workflow steps; they are not claims about runtime speed.

Final validation on Python 3.13: **329 tests passed**, with 23 existing invalid-escape
warnings from raw workflow strings. Ruff checks passed for the changed implementation
and focused test files. No live database or network query was executed by validation.

| Fixture | Before bytes | After bytes | Before definitions | After definitions |
| --- | ---: | ---: | ---: | ---: |
| `script_short.txt` | 54,662 | 21,926 | 98 | 34 |
| `actual_script.txt` | 122,346 | 80,484 | 184 | 108 |
| `html_test.txt` | 53,949 | 31,584 | 92 | 47 |
| `hamizah.txt` | 121,075 | 87,937 | 149 | 83 |

Unused compiler methods disappear, shared dependencies are emitted once, and context
instances are generated from the closure. Tests prove that a new utility needs no
dependency list, that fallback records have reasons, and that source parsing is cached.
Existing invalid-escape warnings in some raw workflow SQL strings remain unrelated
to dependency selection.

## 18. Deliberate limits and risks

This is a standalone runtime projection of project source, not full Python import
machinery. It does not reconstruct package/module objects, arbitrary package startup
hooks, loader identity, or compiler descriptor APIs at runtime. The `ctx` parameter
is the compiler's established runtime protocol, not a general inferred object type.

Star imports, generic type-parameter scopes, namespace-sensitive dynamic loading,
conflicting flattened bindings, repeated class definitions, and interleaved module
rebindings that cannot be safely ordered are rejected.
Runtime-local imports of modules with initialization effects are rejected. Raw
workflow project imports requiring text rewriting are also rejected: raw step text
and its edit metadata remain owned by the emitter. Use registered runtime operations
or an external import that remains available in the standalone environment.

Full-class fallback can retain substantially more code and can expose a dependency
on compiler-only functionality; that produces a diagnostic rather than incomplete
output. Arbitrary reflective Python is intentionally not guaranteed. Extend the
supported subset only with a concrete source pattern and a failing execution test.
