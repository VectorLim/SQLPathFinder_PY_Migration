# Simple logger module update

Date: 2026-06-26

## Summary
- Implemented a concise logger in src/vg2c/logger.py using Python's built-in logging module.
- Added PrettyLogger (inherits logging.Logger) with one extra method: table(...).
- Added table pretty-printing for list-of-dicts and list-of-lists.
- Reused logging.basicConfig and logging.getLogger via thin wrappers.
- Exported common logging level constants.

## Validation
- Static check: no errors in src/vg2c/logger.py.
- Runtime smoke test: info logging and table output printed successfully.
