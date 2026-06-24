# Stage 1 — Parser & Classifier Summary

## 🎯 Goal of Stage 1
- Convert raw VG2 script text into a stable, ordered list of parsed blocks.
- Classify each parsed block into a practical Stage 1 kind for downstream stages.
- Preserve raw block content and source location so later stages can resolve and emit safely.
- Surface parsing/classification issues as diagnostics without stopping the full run.

---

## ✅ What Has Been Implemented

### 1. Parser
- VG2 content is split into block segments using the New Query separator line, with whitespace-tolerant matching.
- Empty segments between separators are skipped and reported as diagnostics.
- Two option parsing paths are implemented:
  - Explicit OPTIONS mode using <OPTIONS> ... </OPTIONS>
  - Inline mode for leading slash options when OPTIONS tags are missing
- Option lines are parsed as slash key/value pairs, keys are normalized to uppercase, duplicates are preserved in ordered pairs, and lookup uses last-write-wins.
- Body text is preserved as-is except for trimming one outer leading newline and one outer trailing newline.
- Each block carries stable metadata:
  - Dense source-order index (0-based)
  - SourceSpan with file path and absolute start/end lines
  - Raw segment text for debugging and downstream use

---

### 2. Block Classification
- Classification is rule-based and deterministic (ordered first-match rules).
- Kinds currently supported:
  - MARS_READ
  - OASYS_READ
  - ARIES_READ
  - SQLITE_QUERY
  - WRITE_FILE
  - HTML_REPORT
  - UTILITY
  - MACRO_CONTROL
  - UNKNOWN
  - MALFORMED (enum slot reserved)
- Main decision logic:
  - HTML report from REPORT values starting with HTML-
  - Write-file from WRITE-FILE=Y
  - Macro control vs utility via UTILITIES value shape (starts with { for macro control)
  - SQLite from OLEDB=SQLite or ENGINE=SQLite
  - MARS/OASYS/ARIES from NODE signal + ENGINE=VA, including placeholder NODE patterns such as <<<MARS>>> and <<<ARIES>>>
  - Unknown fallback when no rule matches
- ARIES detection adds a one-time informational diagnostic noting that the rule path is not fixture-covered by dedicated ARIES fixtures.

---

### 3. Data Models
- ParsedBlock: Parsed unit of source text with options, body, raw text, index, and span.
- ClassifiedBlock: ParsedBlock plus assigned Kind and short classification reason.
- BlockOptions: Ordered option pairs plus normalized key lookup map.
- Diagnostic: Structured issue record with severity, code, message, and optional block/span context.
- SourceSpan: File and absolute line range for source traceability.
- Kind enum: Canonical Stage 1 block categories used by classifier output.

---

### 4. Diagnostics System
- Detects and reports practical parser/classifier issues such as:
  - non-utf8-bytes-replaced
  - empty-block
  - unclosed-options
  - inline-options
  - malformed-option-line
  - duplicate-option-key
  - unknown-kind
  - aries-rule-untested
- Diagnostics are accumulated as plain data and returned alongside results.
- Stage 1 is diagnostics-first: per-block issues do not crash the overall parse/classify run.

---

### 5. Test Coverage

#### Unit Tests
- Parser tests validate separator splitting, options parsing modes, option normalization/duplicates, body preservation, spans, and diagnostics.
- Classifier tests validate major rule branches (HTML, WRITE_FILE, MACRO_CONTROL, UTILITY, SQLITE, MARS, OASYS) plus UNKNOWN fallback.

#### Edge Case Tests
- Missing closing OPTIONS block (best-effort parsing + error diagnostic)
- Inline options parsing without OPTIONS tags
- Quoted utility values and UNC path preservation
- Macro placeholders preserved verbatim (<<>> and <<<...>>>)
- SQL, Python, CSV, and HTML body preservation behavior

---

### 6. End-to-End Testing (Important)

Stage 1 was validated against real fixture scripts:
- script_short.txt
- script_another.txt
- sql_script.txt
- script_from_vietnam.txt
- actual_script.txt

Validation outcomes:
- Parsing/classification executes without runtime crashes.
- Non-empty classified output is produced for each script.
- Expected major kinds are detected across fixtures (MARS/OASYS/SQLite, WRITE_FILE, UTILITY, MACRO_CONTROL, HTML).
- Diagnostics are returned in-band; fixture assertions ensure no parser error-level regressions on the clean fixture set and enforce practical classification expectations on the demanding real script.

---

## ⚠️ Current Limitations (Stage 1)
- No macro resolution or scope execution semantics yet.
- No SQL transformation or SQL utility pre-processing.
- No View Registry expansion or logical-view resolution.
- No Python code generation/emission.
- No utility execution mapping beyond coarse Stage 1 kind tagging.
- No typed per-kind spec extraction (raw options/body only).
- No runtime orchestration, CLI flow, or strict-mode policy layer yet.

---

## 🧱 Dependencies for Next Stage
Future Resolver/Emitter stages can rely on:
- Stable block boundaries and dense source ordering.
- Reliable first-pass kind classification for routing.
- Preserved raw body text for later interpretation/transformation.
- Preserved raw options (ordered pairs + normalized lookup).
- Source file and line span metadata for precise diagnostics.
- Structured diagnostics already available from parse/classify output.

---

## 🧠 Simplicity Check
- Intentionally not implemented: resolver logic, typed specs, macro execution model, SQL rewriting, and code emission/runtime helpers.
- Most likely future complexity: utility/macro semantics and resolver-stage interpretation (especially control-flow and cross-block dependencies).
- Minimal working flow currently: parse VG2 text into ParsedBlock list, classify into ClassifiedBlock list, return diagnostics, and validate behavior via unit + fixture tests.
