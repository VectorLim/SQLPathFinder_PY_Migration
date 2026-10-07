# ScriptHost portability cleanup, 2026-10-07

## Scope and repository state

Starting commit: `1c03d9004f8fb1859ed3b2269f708651dd11f557`, branch
`AED-integration`. Fetch succeeded; local HEAD and `origin/AED-integration`
matched with zero commits ahead/behind. No clone or merge was performed.

The checkout started with 24 modified tracked files and three untracked files.
Those changes were saved outside the checkout and retained. This session's
cleanup is separate from that existing compiler/API/container/job work.
`normal_query.py` already contained `SubStitute_CT`; its implementation remains
unchanged. A clean checkout cannot be obtained by silently discarding or
committing that unrelated work.

Canonical comparison source: `scripthost-utilities-decompiled/SPSQL3_py.zip`.
The `original/SPSQL3_py` folder already contains portable modifications.
Before cleanup, all 14 targeted vendor Utilities methods, `Run_SQLite`,
`getStandaloneCon`, and `Prep_Inc_Process` matched the ZIP by AST.

## Implemented simplifications

Nine copied implementations were removed from compatibility subclasses:

- Utilities: `SPFRoboCopy`, `File_Lock_Move`, `SPFWebCopyPyReqs`, `GetFilePattern`.
- Tasks: `GetSiteTimeTask.executeTaskCommand`,
  `UpdateTimeFileTask.executeTaskCommand`, `SmartAppendTask.performUpdateTime`,
  `XMLToCSVTask.executeTaskCommand`, `GetFilesTask.getFilesInfoFromFolderGlob`.

The first four now inherit their complete original algorithms and replace only
command/authentication/separator dependencies. GetFiles inherits the scan and
metadata algorithm with two path-construction hooks. XMLToCSV inherits the
task's validation, destination handling, completion, and cleanup, replacing
only its generated converter command. Time tasks normalize the INI input at
the original helper boundary.

Six copied methods/properties became short wrappers: `Run_R`, `setEnv`,
`Prep_Inc_Process`, `gIsSvc`, `gLocalDir`, and `getStandaloneCon`.
Windows branches of delete/copy/unzip, delimiter conversion, Intel work week,
email, Excel loading, echo, and readonly now delegate to original methods.
The Linux branches retain their existing operations and support limits.

The vendor received small default-preserving hooks in `utils.py` and
`SPFSQL3.py`, plus the existing missing-SMTP dependency guard moved into the
inherited SMTP branch. Default command arguments/flags, SSPI import position,
and PEM/auth evaluation order remain those of the archived implementation.
No vendor formatting, algorithm modernization, or Linux implementation was
introduced. The full member-by-member disposition and exact Linux reasons are
in [the override inventory](../src/scripthost_portable/overrides/README.md).

Measured reduction across the eleven changed override modules: **3,327 to
1,490 physical lines**, a reduction of **1,837 lines (55%)**. AST statement
nodes fell from 1,924 to 1,023, so this includes removed executable duplication,
not only comments. Utilities alone fell from **1,793 to 413 lines**. The two
vendor files together add 26 net lines. No override module or supported helper
function was deleted.

## MemTable cleanup

The focused follow-up replaces the 856-line portable `Run_SQLite` copy with a
23-line wrapper. The 850-line original vendored algorithm remains authoritative,
with only two expressions routed through tiny default-preserving hooks.
Linux compatibility is limited to three seams:

1. Normalize `WorkDir == ".\\"` to `"."` on POSIX.
2. Preserve CSV import-pair case and first-seen ordering on POSIX, rather than
   the original uppercase set. Uppercasing breaks case-sensitive filenames.
3. Construct preprocessed temporary CSV paths under `"."` on POSIX rather
   than the literal Windows current-directory spelling.

`_prepare_sqlite_import_pairs(pairs)` defaults to
`list(set(item.upper() for item in pairs))`. Its POSIX override uses
`list(dict.fromkeys(pairs))`. `_sqlite_preprocessed_temp_path(counter, rn)`
defaults to `os.path.join(".\\", "{0}_{1}.tmp".format(counter, rn))`;
the POSIX override joins the same filename under `"."`. Windows delegates both
hooks to their defaults. The wrapper preserves the original signature and
forwards every argument to `super().Run_SQLite` after WorkDir normalization.

Behavior tests were added and passed against the copied method before removing
it: Windows **2 passed, 3 skipped**; actual Linux **4 passed, 1 skipped**.
They cover case/order, the historical current-directory spelling, preprocessing
and cleanup on success/failure, quoting/aliases, empty headers, and append.
The portable MemTable module shrinks from **894 to 71 lines**, removing
**823 lines (92%)** without expanding any other compatibility implementation.

Independent safe cleanup was completed: standalone connections inherit all
original UDF registration, then register the missing `CharIndex_v2` before
executing attachment SQL. Original CSV-list SQL generation uses that UDF;
neither the ZIP nor the original base defines it.

## Retained boundaries

`nqOracleTask.OpenConnection` and `PortableOracleConnection` implement the
missing compiled-driver boundary. The original query task calls `execute`
with output path, FirstConnect, header flags, and other options and receives a
row count, not a result frame (`SPFSQL3.py`, `__ExceuteSQLQuery__`). Consequently
CSV writing, delimiter choice, header handling, append/overwrite, and returned
row counts remain driver responsibilities. No transport ownership was moved.
Unsupported explain-plan/backend behavior remains explicit.

All six file helpers remain required by supported runtime operations and
integration tests: XML conversion, copy, delete, RoboCopy, unzip, and delimiter
cleanup. Keeping them preserves documented broader portability. No historical
support was removed or new support added.

Runtime remains manager loading, runtime state/cwd, original execution, Python
binding, and cleanup. The Python API constructs task inputs and calls original
tasks. Worker/launcher provide process isolation and protocol; the original
runtime's mutable cwd, environment, and global/class state justify isolation.
Minimal compiled-driver and NodesInfo fallbacks remain unchanged.

Compiler `frontend/classifier.py` currently imports private facade helpers
`_block` and `_query_task_options` and uses task introspection. That coupling
deserves a separate compiler/API boundary review, but changing it is unnecessary
for this cleanup. Existing compiler/API/worker/launcher edits were retained.

## Domain packages

`aed_api.py` combines the ScriptHost entry points `prepare_job` and
`process_candidates` with config, SMB access, bootstrap, site mapping, history,
and candidate orchestration. Runtime consumers are worker preparation, the
Python `aed.process` facade, and vendor `{AED}`. There is no existing AED
application package suitable for moving orchestration. Root `aed_updater.py`
owns only the narrow API update operation; adding all orchestration there
would mix responsibilities.

If restructuring is approved separately, an application package such as
`icmpcs_aed` can own config/history/service orchestration, while stable adapter
entry points and compatibility imports remain in ScriptHost. That is a package
and public-import decision, not an automatic cleanup. No AED module was moved.

`migrate_aed.py` is migration tooling, not runtime portability. Tests and an
existing live-check script import it; the module also exposes a CLI. There is
no established packaged tools destination. A future tools/domain location
should retain an import/CLI shim. No move was made without compatibility proof.

## Deliberate deferrals and decisions

- `unzipString` retains UTF-8-first decoding rather than the legacy detector.
  This is an existing cross-platform semantic difference; an encoding decision
  was requested. Valid UTF-8, short strings, and invalid input were characterized
  against both implementations. The existing deleted-input exception fallback
  is unchanged.
- The existing Linux email branch still shares MIME/recipient/task handling
  with the original. Removing more duplication would require several broader
  identity/role/SMTP seams; it was not expanded into a new provider framework.
- Pre-existing changes remain separate from session cleanup. No resets, stash,
  package relocation, dependency installation, or production service writes.

## Validation

Before production edits, Windows portability baseline: **169 passed, 2 failed,
15 skipped**. Full baseline: **246 passed, 8 failed, 15 skipped**, with 19
subtests passed. The two portability failures are native Windows driver
authorization and native NodesInfo rejecting a `C:` SQLite path. Full-suite
additional failures are four DataSyncX constructor/environment contract cases
and two CSR_IAM SQLite cases. They are pre-existing, not cleanup regressions.

New characterization before simplification: Windows **5 passed, 2 skipped**;
actual Linux **7 passed**. It covers incremental paths, the legacy acceptance of
1000, repeated incremental errors, UDF registration before attachment SQL,
file-move retry count/error, and newest-folder selection. Subsequent checks add
Windows original-method forwarding and the unavailable-SMTP guard.

Actual Linux baseline used existing image
`sqlpathfinder-clean-python:stage3-validation` (Python 3.12.15, pytest 9.1.1,
R 4.5.0), immutable source snapshot, network disabled, and writable tmpfs.
Selected utility integration baseline: **18 passed, 0 failed**. Cases include
real file output, R execution, Excel, XML, time persistence, and Linux email.
An initial broad run was stopped after about ten minutes without usable
progress; bounded split runs were used instead. A combined subset reached its
120-second limit after 17 cases; all 18 completed in split runs.

Final targeted checks: Windows adapter/inheritance/boundary tests **25 passed,
2 skipped**; Linux adapter checks **18 passed**; the same actual Linux utility
subset **18 passed, 0 failed**. Baseline and final utility output assertions
match.

| Windows suite | Baseline passed/failed/skipped | Final passed/failed/skipped |
| --- | --- | --- |
| `tests/scripthost_portable` | 169 / 2 / 15 | 185 / 2 / 17 |
| Full repository | 246 / 8 / 15 | 262 / 8 / 17 |

All failures have exactly the same node IDs as the baseline. The added tests
account for 16 additional Windows passes and two POSIX skips. The final full
suite includes 19 successful subtests. Compiler verification separately passed
all **67 tests**. Windows final timings: portability 290.69 seconds, full
320.43 seconds, compiler 22.26 seconds.

Pre-existing full-suite failure identities:

- `tests/scripthost_portable/test_original_scripthost_runtime.py::test_windows_db_transport_fails_only_when_invoked`
- `tests/scripthost_portable/test_scripthost_query_transport.py::test_datasyncx_public_constructor_contract[mars-MarsReader-kwargs0]`
- `tests/scripthost_portable/test_scripthost_query_transport.py::test_datasyncx_public_constructor_contract[aries-AriesReader-kwargs1]`
- `tests/scripthost_portable/test_scripthost_query_transport.py::test_datasyncx_username_from_env[mars-MarsReader-kwargs0]`
- `tests/scripthost_portable/test_scripthost_query_transport.py::test_datasyncx_username_from_env[aries-AriesReader-kwargs1]`
- `tests/scripthost_portable/test_utility_contracts.py::test_sqlite_original_udfs`
- `tests/test_csr_iam.py::ConditionalFlowTests::test_sqlite_reports_first_error_for_incomplete_dummy_data`
- `tests/test_csr_iam.py::ConditionalFlowTests::test_sqlite_with_complete_dummy_data`

The first full-snapshot attempt reported two collection errors because root
modules were omitted from the test copy. A complete snapshot corrected that
harness issue before the final full-suite comparison. Source was not changed
to accommodate the harness.

Additional actual Linux checks passed: boundaries (7), inheritance (2), and
migration tooling (10). Certification cases were split after the module's
120-second limit; all **7 expanded cases passed** (control flow, two reports,
role rejection, two launcher cases, and a real query slice). The entire Linux
repository suite was not completed. Selected changed runtime paths have actual
Linux evidence; remaining broad integration/backend coverage is a limitation,
not a claimed pass.

The staged cleanup was also exported through read-only Git blob access and
tested independently of the unstaged API/compiler work: **25 passed, 2 skipped
on Windows; 27 passed on Linux** for adapter, inheritance, and boundary checks.
No untracked API contract/introspection files were copied, and unstaged source
versions were not used. This checks the actual cleanup commit, while the full
working-snapshot suite checks compatibility with the retained user changes.

Commands were `python -m pytest tests/scripthost_portable -q`,
`python -m pytest -q`, `python -m pytest tests/compiler -q`, and targeted
adapter/inheritance/boundary or utility selections. Windows used the existing
project virtualenv, `PYTHONPATH` pointing to the relevant snapshot source/root,
and `SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT=1`. Linux used the existing
validation image with `--network none`, read-only snapshot, writable tmpfs,
`PYTHONDONTWRITEBYTECODE=1`, and bounded split pytest commands.

Full Windows baseline artifacts are under the temporary
`sqlpathfinder_baseline_20261007_20261007_155935` directory; sanitized copies are
under `sqlpathfinder_baseline_20261007_char_20261007_161301/sanitized_logs`.
Final complete-snapshot logs are under
`sqlpathfinder_final_verify_complete_20261007_164152_logs`; staged verification
logs are under `sqlpathfinder_staged_verify_logs`. Logs contain exact commands
and exit codes. Failure comparison uses node IDs; credentials are not part of
this report.

No mocked platform flag is presented as filesystem/process Linux validation.
No live manufacturing or AED write was performed. All 26 pre-existing dirty
files outside `normal_query.py` are byte-identical to the saved starting copies;
that file's pre-existing `SubStitute_CT` AST and prefix are unchanged. That earlier
cleanup retained the copied `Run_SQLite`; the focused follow-up above removes it.

## Focused MemTable follow-up validation

This follow-up started from clean `AED-integration` commit
`b132ce00ab28dd1fe5b4f50a5b015aea8014e743`. Fetch confirmed that local and
remote HEAD matched. Only the two MemTable modules, the override inventory,
this report, and `test_memtable_sqlite.py` changed.

The pristine ZIP comparison found exactly the three documented portability
differences. Substituting the archived expressions back for the two vendor
hook calls yields the exact archived `Run_SQLite` AST. The wrapper signature
also matches the ZIP, and an independent call check verifies all 27 arguments
through positional and keyword calls. `getStandaloneCon` and
`sqliteCharIndex_v2` remain byte/AST-identical to the starting commit; registration
still precedes attachment SQL. No new compatibility framework was added.

Counts below are **passed / failed / skipped**. Characterization was run against
the copied implementation before its removal, then against the wrapper.

| Platform / suite | Baseline | Final |
| --- | --- | --- |
| Windows focused SQLite | 4 / 1 / 0 | 4 / 1 / 0 |
| Windows new MemTable characterization | 2 / 0 / 3 | 2 / 0 / 3 |
| Windows `tests/scripthost_portable` | 185 / 2 / 17 | 187 / 2 / 20 |
| Windows `tests/compiler` | 67 / 0 / 0 | 67 / 0 / 0 |
| Windows full `tests` | 262 / 8 / 17 | 264 / 8 / 20 |
| Linux focused four modules | 92 / 2 / 0 | 92 / 2 / 0 |
| Linux new MemTable characterization | 4 / 0 / 1 | 4 / 0 / 1 |
| Linux `tests/scripthost_portable` | 202 / 2 / 0 | 206 / 2 / 1 |
| Linux `tests/compiler` | 67 / 0 / 0 | 67 / 0 / 0 |
| Linux remaining repository tests | 14 / 2 / 0 | 14 / 2 / 0 |
| Linux full coverage, summed disjoint suites | 283 / 4 / 0 | 287 / 4 / 1 |

Both full-platform runs include 19 successful subtests. Every baseline/final
failure identity matches. Windows retains the same eight IDs listed above:
native driver authorization, four DataSyncX credential/environment contract
cases, the `C:` SQLite node-path case, and two missing CSR fixture cases.
Linux's four unchanged failures are:

- `tests/scripthost_portable/test_utility_contracts.py::test_r_original_local_interpreter[False]`
- `tests/scripthost_portable/test_utility_contracts.py::test_r_original_local_interpreter[True]`
- `tests/test_csr_iam.py::ConditionalFlowTests::test_sqlite_reports_first_error_for_incomplete_dummy_data`
- `tests/test_csr_iam.py::ConditionalFlowTests::test_sqlite_with_complete_dummy_data`

The Linux image lacks R, and the CSR cases lack
`tests/fixtures/csr_iam/PARMI_IPM_RAW.csv`. Neither failure cause was changed.
Windows final suite durations were 312.39 seconds (portable), 15.14 seconds
(compiler), and 337.78 seconds (full). Linux durations were 165.00 seconds
(portable), 8.46 seconds (compiler), and 4.25 seconds (remaining tests).

Real Linux validation used Python 3.12.11 in existing image
`sqlpathfinder-aed:validation`, image ID
`sha256:8cf80eab937a5fb6ebb5843bbe3f967d1ba0cda41c88570c6b681f2d635f426f`.
Current checkout files were copied to the container's `/tmp/sqlpf-checkout`,
their hashes verified, and tested with `--network none`. This exercises the
Linux filesystem, not a mocked platform flag or a Windows-mounted test folder.
Windows used the existing project Python 3.13.14 environment. Both used
`SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT=1` and the current source/root on
`PYTHONPATH`.

Both `ICMPCS.txt` and `output/aed-migration/CSR_IAM_v2.aed.txt` were re-run through
the existing offline target fixtures. Seven scenarios per platform cover
positive, no-candidates, zero-input, and CSR seed behavior. Baseline and final
snapshots match exactly: CSV bytes/hashes, headers, row counts, control-driving
files, task/branch events, and AED candidate bytes. Existing original/generated
parity tests also pass. No live manufacturing or AED service was invoked.

Commands were the baseline focused selection, the new MemTable test file,
`python -m pytest -q tests/scripthost_portable`,
`python -m pytest -q tests/compiler`, and full `tests` coverage (split into
disjoint suites on Linux). Exact commands, logs, JUnit failure comparisons and
the seven-scenario before/after JSON files are retained outside the repository:

- Windows: `%TEMP%/sqlpathfinder-baseline-20261007-b132ce0`
- Linux: `%TEMP%/sqlpf-linux-3bd956cf7ff84aef9eeeb8754d9d6047`

No additional MemTable issue was observed in the tested Python SQLite paths.
Native SQLite executable/app-server branches and Windows executable CSV
preprocessing were preserved through archived defaults, without new execution
coverage. There is one authoritative ScriptHost `Run_SQLite` implementation,
and Linux portability is expressed only through small compatibility seams.
