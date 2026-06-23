# vg2c

`vg2c` is a staged VG2 compiler migration project. Step 1 implements the frontend reader, splitter, options parser, and parser composition.

## Public API

```python
from vg2c import ParsedBlock, SourceSpan, parse_vg2

blocks = parse_vg2("tests/fixtures/script_short.txt")
```

## CLI Example

```bash
python -m vg2c parse tests/fixtures/script_short.txt --json
```

## Roadmap

- Step 2: `vg2c/classifier/`
- Step 3: `vg2c/validator/`
- Step 4: `vg2c/resolver/`
- Step 5: `vg2c/ir/`
- Step 6: `vg2c/sqlrewrite/`
- Step 7: `vg2c/translators/`
- Step 8: `vg2c/runtime/`
- Step 9: `vg2c/emitter/`
