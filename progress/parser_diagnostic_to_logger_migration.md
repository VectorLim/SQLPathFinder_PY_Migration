# Parser Diagnostic to Logger Migration

Date: 2026-06-26
File: src/vg2c/frontend/parser.py

## Summary
- Replaced all parser-side `Diagnostic(...)` object creation and `diagnostics.append(...)` calls with logger calls.
- Added module logger setup:
  - `logger = logging.getLogger(__name__)`
  - `PARSER_LOG_LEVEL = logging.DEBUG` (predefined debug level)
- Added helper `_log_parser_event(...)` to centralize log format and metadata.
- Removed `diagnostics` plumbing from helper function parameters:
  - `_normalize_input(text)`
  - `_extract_options_and_body(segment, block_index, span)`
  - `_parse_options(options_region, block_index, span)`
- Kept `parse(...)` return signature stable (`tuple[list[ParsedBlock], list[Diagnostic]]`) for compatibility, returning an empty diagnostics list.

## Validation
- Checked `src/vg2c/frontend/parser.py` with workspace diagnostics.
- Result: no errors.
