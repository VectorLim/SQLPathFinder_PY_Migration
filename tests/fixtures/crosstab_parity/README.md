# Normal-query crosstab parity fixtures

Evidence class: **SOURCE-DERIVED**, not original-proprietary-engine-executed.

The tiny \`basic.csv\` dataset and \`expected_basic.csv\` were authored independently
from the behavior traced in the decompiled original:

- \`SPFLib/SPFSQL3.py:2442-2447\` classifies normal-query pivots.
- \`SPFLib/SPFSQL3.py:2604-2611\` forwards header/value, not /CTROW row keys.
- \`SPFLib/SPFUtilities/utils.py:4070-4088\` infers row keys by excluding
  the pivot header and value fields from all intermediate CSV columns.
- \`utils.py:4137-4218\` constructs uppercase, sorted pivot-column headers.
- \`utils.py:4280-4512\` performs positional duplicate resolution and unstack.

The fixture pins: row-key inference from \`lot,area\`; leading-zero ID as a
literal string; group order; uppercase output header; missing B/A cell as
an empty CSV field.

Additional in-memory source-derived fixtures and executable generated-Python
integration cases are in
\`tests/runtime/test_crosstab_script_host_parity.py\`.

Evidence rules:

- **SOURCE-DERIVED**: expected text independently hand-authored from inspected
  code; this is not a true ScriptHost oracle.
- **CURRENT-VG2C-ONLY**: intentionally retained old generated-project/API
  contracts; source parity may differ.
- **INTENTIONAL-CHANGE**: narrower or safer behavior than the decompiled engine,
  with a documented reason.
- **UNVERIFIED/WIP**: missing proprietary differential or important feature
  gap, never counted as exact parity.

Current high-priority WIP:

- Original cross-chunk \`combine_first()\` behavior vs this revision's global
  physical-row de-duplication, especially \`PIVOT_FUNCTION=LAST\`.
- A \`.csv\` target may save comma-delimited CTARRAY metadata, while
  \`SubStitute_CT\` reads TAB-delimited metadata. Runtime normalizes to tabs
  intentionally; do not certify byte-for-byte original INI parity.
- \`CTHEADER\` with multiple named columns is not proven to work in the
  original normal path, despite a partial split operation.
- No runnable proprietary ScriptHost execution or external backend oracle
  was used, only source inspection and independent source-derived fixtures.
