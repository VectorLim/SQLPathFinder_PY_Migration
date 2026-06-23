# vg2c

`vg2c` is a staged VG2 compiler migration project. Step 1 implements the frontend reader, splitter, options parser, and parser composition. Step 2 implements the block classifier.

## Public API

### Step 1: Parsing

```python
from vg2c import ParsedBlock, SourceSpan, parse_vg2

blocks = parse_vg2("tests/fixtures/script_short.txt")
```

### Step 2: Classification

```python
from vg2c.classifier import classify_all, Kind

classification = classify_all(blocks)
for cb in classification.blocks:
    print(f"Block {cb.parsed.index}: {cb.kind} ({cb.reason})")
```

## CLI Examples

```bash
# Parse a VG2 file
python -m vg2c parse tests/fixtures/script_short.txt --json

# Classify blocks (summary)
python -m vg2c classify tests/fixtures/script_short.txt

# Classify blocks (detailed report)
python -m vg2c classify tests/fixtures/script_another.txt --report

# Classify with strict mode (exit 1 if any UNKNOWN)
python -m vg2c classify tests/fixtures/actual_script.txt --strict
```

## Roadmap

- ✅ Step 1: `vg2c/frontend/` — parse VG2 scripts into structured blocks
- ✅ Step 2: `vg2c/classifier/` — classify blocks by kind (SQL, utility, control flow)
- Step 3: `vg2c/validator/` — validate options and dependencies
- Step 4: `vg2c/resolver/` — resolve references and utilities
- Step 5: `vg2c/ir/` — intermediate representation
- Step 6: `vg2c/sqlrewrite/` — rewrite SQL for target engines
- Step 7: `vg2c/translators/` — translate to Python
- Step 8: `vg2c/runtime/` — runtime support library
- Step 9: `vg2c/emitter/` — emit final Python code
