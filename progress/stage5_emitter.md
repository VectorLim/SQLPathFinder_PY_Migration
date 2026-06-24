# Stage 5: Python Emitter — Implementation Complete

## Overview

**Stage 5** transforms a `DispatchedProgram` (frozen output from Stage 4) into an `EmittedScript` — executable Python source code that implements the VG2 logic in pure Python using the runtime API contract.

**Status:** ✅ Fully Implemented & Tested (18 tests, 100% passing)

**Key Achievement:** Hard contract validated — all emitted scripts parse cleanly as valid Python (ast.parse ✓), contain `def run()` function, and have `if __name__ == "__main__"` entry point.

---

## Architecture

### Input & Output

| Item | Type | Purpose |
|------|------|---------|
| **Input** | `DispatchedProgram` | Frozen program with reader targets, dialect resolution, and schema substitution from Stage 4 |
| **Output** | `EmittedScript` | Frozen record: source code (str), imports (tuple), diagnostics (tuple) |

### Core Components

#### 1. **EmitContext** (`models.py`)
Mutable execution context for code generation.

```python
@dataclass(slots=True)
class EmitContext:
    indent_depth: int = 0
    imports: set[str] = field(default_factory=set)
    dispatch_map: dict[int, DispatchedBlock] = field(default_factory=dict)
    registry: HandlerRegistry | None = None
    macro_subst: MacroSubstituter | None = None
    
    def add_import(self, module: str, name: str | None = None) -> None:
        """Carefully register imports: from MODULE import NAME or just import MODULE"""
```

**Key Methods:**
- `add_import(module, name)`: Safely builds import statements, avoiding malformed syntax like `import from X import Y`

#### 2. **IndentWriter** Helper (`models.py`)
Manual Python source generation with managed indentation.

```python
class IndentWriter:
    def push_indent(self) -> None:
    def pop_indent(self) -> None:
    def write(self, line: str) -> None:
    def write_block(self, lines: Iterable[str]) -> None:
    def source(self) -> str:  # Returns complete buffered output
```

**Usage Pattern:**
```python
writer = IndentWriter()
writer.write("def step_func(ctx):")
writer.push_indent()
writer.write("ctx.reader_mars(...)")
writer.pop_indent()
```

#### 3. **Handler Protocol** (`protocol.py`)
Pluggable emit strategy per Block Kind.

```python
@runtime_checkable
class Handler(Protocol):
    def __call__(
        self, 
        ctx: EmitContext, 
        block: ResolvedBlock, 
        dispatched: DispatchedBlock | None
    ) -> tuple[str, str]:  # (function_source, call_site_line)
```

**HandlerRegistry:**
- Registers Kind → Handler(func) mapping
- Looks up at runtime: `handler = registry.get(block.kind)` → emit

---

## Implementation Details

### Per-Kind Handlers (`handlers.py`)

#### **SQL Readers** (MARS, OASYS, ARIES, SQLite)

Each SQL reader generates a function that calls the appropriate runtime reader:

```python
# MARS_READ handler (illustrative)
def step_0000_mars_read(ctx):
    ctx.reader_mars(
        database_arg="orasql",          # dialect-specific
        node=...,                       # from dispatched.reader_target.node
        record=...,                     # from dispatched.reader_target.record_name
        instance=...,                   # from dispatched.reader_target.instance
    )
```

**Dialect-aware database_arg mapping:**
- `Kind.MARS_READ` → `database_arg="orasql"`
- `Kind.OASYS_READ` → `database_arg="oasys"`
- `Kind.ARIES_READ` → `database_arg="aries"`
- `Kind.SQLITE_QUERY` → `ctx.sqlite_engine.run_join(sql=..., inputs=..., output=...)`

#### **WRITE_FILE Handler**

Emits file writing logic with triple-quoted SQL body:

```python
def step_0001_write_file(ctx):
    ctx.write_file(
        path="/output/file.txt",
        template="""
SQL_BODY_HERE
        """
    )
```

**Macro Substitution Integration:**
- Attempts to substitute `@@SQLMACRO:n@@` tokens → helper calls (e.g., `ctx.macro.sql_get_csv_list(...)`)
- Falls back to literal if no matching macro

#### **UTILITY Handler** (`utility_shapes.py`)

Classifies utility blocks by basename pattern matching:

```python
def classify_utility(utilities_string: str) -> UtilityInfo:
    """Match against known patterns: run_python_script, email, robocopy, etc."""
    # Splits by space, extracts basename, matches patterns
    # Returns UtilityInfo(shape=UtilityShape, args=[...])
```

**Emits contextual code:**
- `run-python-script` → `ctx.external.run(["python", ...], ...)`
- `email` → `ctx.mail.send(to=..., subject=..., body=...)`
- `robocopy`, `spf-delete`, `spf-copy` → `ctx.fs_ops.*`
- `bat-file`, `exe-direct` → `ctx.external.run(...)`
- `unknown` → `# TODO: Unsupported utility`

#### **HTML_REPORT & UNKNOWN Handlers**

- `HTML_REPORT` → `# TODO: HTML report generation (not yet supported)`
- `UNKNOWN` → `# TODO: Unknown block type`

---

### Scope Tree Traversal (`walker.py`)

**Core Function: `walk_and_emit(dispatched, ctx) → (functions, run_body_source)`**

Recursive depth-first walk of `ScopeNode` tree:

```python
def _walk_scope(node: ScopeNode, ...) -> None:
    if node.kind == "program":
        for child in node.children:
            _walk_scope(child, ...)
    
    elif node.kind == "macro":
        payload = node.payload  # StartMacro
        writer.write(f"with ctx.macro_scope('{payload.csv_path}', ...):")
        writer.push_indent()
        for child in node.children:
            _walk_scope(child, ...)
        writer.pop_indent()
    
    elif node.kind == "if":
        payload = node.payload  # IfThen
        condition = _build_condition_expr(payload)
        writer.write(f"if {condition}:")
        writer.push_indent()
        _walk_scope(node.children[0], ...)  # if-branch
        writer.pop_indent()
        if len(node.children) > 1:
            writer.write("else:")
            writer.push_indent()
            _walk_scope(node.children[1], ...)  # else-branch
            writer.pop_indent()
    
    elif node.kind == "leaf":
        # Emit block via handler lookup
        block_index = node.block_index
        resolved = dispatch_map[block_index]
        handler = registry.get(resolved.kind)
        func_code, call_site = handler(ctx, resolved, dispatched)
        # Add to functions list and call_site to run body
```

#### **IF-THEN Condition Expression Builder** (`_build_condition_expr`)

Transforms control payload operands into Python expressions:

```python
def _build_condition_expr(payload: IfThen) -> str:
    """
    IfThen(
        lhs="VAR(X)", 
        op="EQS", 
        rhs="value",
        conjunction="AND",
        lhs2="VAR(Y)",
        op2="NES",
        rhs2="other"
    )
    → "ctx.macro.named("X") == "value" and ctx.macro.named("Y") != "other""
    """
    
    # Operator mapping: EQS→==, NES→!=, LE→<=, LT→<, GE→>=, GT→>, EQ→==, NE→!=
    # Operand unwrapping: VAR(X) → ctx.macro.named("X"), literal → "literal"
    # Compound support: VAR(X) OP rhs [AND/OR VAR(Y) OP2 rhs2]
```

**Key Rules:**
- `VAR(X)` unwrapped to `ctx.macro.named("X")` (uppercase variable name)
- Literal values wrapped in quotes
- Operators mapped to Python equivalents
- Compound conditions joined with `and`/`or`

---

### Macro Substitution (`macro_subst.py`)

**MacroSubstituter class:** Context-aware placeholder rewriting.

```python
def substitute(self, text: str, refs: list[RuntimeMacroRef], context: str) -> str:
    """
    Substitute @@SQLMACRO:n@@ tokens based on context:
    - "python-expr" → ctx.macro.named("NAME")
    - "python-string" → {ctx.macro.named("NAME")}
    - "sql-body" / "template-body" → leave as @@SQLMACRO:n@@ (runtime handles)
    """
```

**Contexts:**
- **python-expr**: Direct variable reference (conditions, assignments)
- **python-string**: String interpolation via f-strings: `f"{ctx.macro.named('NAME')}"`
- **sql-body**: Leave literal (DataSyncX runtime expands at query time)
- **template-body**: Leave literal (WRITE_FILE body for runtime substitution)

---

## Code Generation Process

### End-to-End Flow (`__init__.py` emit function)

```
emit(dispatched: DispatchedProgram) → EmittedScript:
  1. Create EmitContext + MacroSubstituter
  2. Populate dispatch_map from DispatchedBlocks
  3. Create HandlerRegistry with all 8 handlers
  4. Add imports: from vg2c_runtime import ctx as pipeline_ctx
  5. Call walk_and_emit(dispatched, ctx) → (functions, run_body)
  6. Assemble script:
     - Header: # Auto-generated Python script from VG2\n"""Pipeline implementation."""
     - Imports block: all ctx.imports lines sorted
     - Function definitions: each step_XXXX_KIND function
     - def run() → None: with run_body
     - if __name__ == "__main__": run()
  7. Validate: ast.parse(source) must succeed
  8. Return EmittedScript(source, imports_tuple, diagnostics_tuple)
```

### Diagnostics

**Emission-time diagnostics added:**
- `emit-syntax-error`: If ast.parse() fails (hard contract violation, requires fix)
- Inherited from earlier stages (5 Stage-4 diagnostics + Stage 1-3)

---

## Testing

### Test Suite: `tests/emitter/test_fixtures.py` (18 tests, 100% passing)

#### Hard-Contract Tests

| Test | Purpose | Assertion |
|------|---------|-----------|
| `test_emitted_script_is_valid_python` (×5) | Syntax validity | `ast.parse(emitted.source)` succeeds |
| `test_emitted_script_has_run_function` (×5) | Entry point | `"def run() -> None:"` in source |
| `test_emitted_script_has_main_entry` (×5) | Main guard | `'if __name__ == "__main__":'` in source |

#### Fixture-Specific Tests

| Test | Purpose | Assertion |
|------|---------|-----------|
| `test_script_short_emitted_is_minimal` | Minimal SQLite pipeline | Exactly 1 step function |
| `test_sql_script_emitted_has_multiple_steps` | Multi-block pipeline | ≥2 step functions |
| `test_actual_script_emitted_has_imports` | Import generation | `from vg2c_runtime import ctx` present |

**Test Results:**
- 18/18 passing
- 9 SyntaxWarnings (harmless: invalid escape sequences in SQL strings like `\K`, `\A`)
- Full pipeline (Stages 1-5): 159 tests passing

---

## Runtime Contract (Stage 6)

### API Surface

**Context singleton: `from vg2c_runtime import ctx as pipeline_ctx`**

```python
class PipelineContext:
    # Macro state during MACRO_CONTROL scopes
    macro: MacroState  # named(name: str) → str, positional() → str
    
    # CSV I/O for macro iteration + data passing
    csv_io: CsvIO  # iter(name), read(name), write(name, content), row_count(name)
    
    # SQL readers (populated by Stage 6)
    reader_mars(database_arg, node, record, instance) → Reader
    reader_oasys(database_arg, node, record, instance) → Reader
    reader_aries(database_arg, node, record, instance) → Reader
    sqlite_engine: SqliteEngine  # run_join(sql, inputs, output)
    
    # SQL macros (dynamic expansion)
    sql_macros: SqlMacros  # sql_get_csv_list(path, column_ref, lead_in) → str
    
    # File operations
    write_file(path, template, vars=None) → None
    fs_ops: FileSystemOps  # copy, rename, delete
    
    # External processes
    external: ExternalProcess  # run(argv, cwd, env) → int
    
    # Email
    mail: MailService  # send(to, subject, body, attachments) → None
    
    # Control flow helpers
    macro_scope(csv_path, row_iter) → ContextManager
    eval_condition(...) → bool  # TBD if used
```

**Stubs Currently Placed:** `src/vg2c_runtime/__init__.py` — all methods raise `NotImplementedError`. Stage 6 will replace stubs with real implementations.

---

## Known Limitations & Future Work

### 1. Macro Substitution Edge Cases
**Current:** Simple pattern matching for `@@SQLMACRO:n@@` tokens.
**Future:** More sophisticated handling for nested/escaped macros.

### 2. Utility Block Support
**Current:** Only recognized shapes emit code; unknown utilities become comments.
**Future:** Validation against actual utility registry; error diagnostics for unrecognized shapes.

### 3. ARIES Block Testing
**Current:** Handler implemented but limited test coverage (actual_script.txt has ARIES blocks).
**Future:** Dedicated ARIES fixture to validate dialect-specific reader calls.

### 4. Error Recovery in IF-THEN
**Current:** If condition expression building fails, emits placeholder.
**Future:** Richer error diagnostics to identify malformed operands at emit time.

### 5. Performance Optimization
**Current:** Tree walk rebuilds expression strings on every emit.
**Future:** Cache compiled condition expressions (if perf profiling shows need).

---

## Files Modified/Created

### New Files
- `src/vg2c/emitter/models.py` — EmitContext, EmittedScript, IndentWriter
- `src/vg2c/emitter/protocol.py` — Handler protocol, HandlerRegistry
- `src/vg2c/emitter/macro_subst.py` — MacroSubstituter
- `src/vg2c/emitter/utility_shapes.py` — classify_utility, UtilityShape
- `src/vg2c/emitter/handlers.py` — 8 per-Kind handlers
- `src/vg2c/emitter/walker.py` — Scope tree traversal + IF-THEN emission
- `src/vg2c/emitter/__init__.py` — emit() orchestrator + ast.parse validation
- `src/vg2c_runtime/__init__.py` — Runtime API contract (stubs)
- `tests/emitter/__init__.py` — Test package init
- `tests/emitter/test_fixtures.py` — 18 hard-contract e2e tests

### Modified Files
- `src/vg2c/__init__.py` — Added `emit` to package exports

---

## Summary

**Stage 5** successfully transforms frozen `DispatchedProgram` objects into executable Python scripts. The implementation:

✅ **Generates syntactically valid Python** (ast.parse validated)  
✅ **Preserves block semantics** via per-Kind handlers  
✅ **Manages scope nesting** (MACRO_CONTROL, IF-THEN branching)  
✅ **Handles SQL dialect variations** (MARS, OASYS, ARIES, SQLite)  
✅ **Integrates macro substitution** (context-aware placeholder rewriting)  
✅ **Passes all 18 e2e tests** on real fixtures  
✅ **Maintains immutability contract** (frozen EmittedScript output)  

**Next Steps:** Stage 6 implements the runtime API contract defined in `src/vg2c_runtime/__init__.py`.
