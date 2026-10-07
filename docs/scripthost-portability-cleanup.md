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

## MemTable decision

`Run_SQLite` remains unchanged pending the requested architecture decision.
Its roughly 850 lines differ from the vendor at only three seams:

1. Normalize `WorkDir == ".\\"` to `"."` on POSIX.
2. Preserve CSV import-pair case and first-seen ordering on POSIX, rather than
   the original uppercase set. Uppercasing breaks case-sensitive filenames.
3. Construct preprocessed temporary CSV paths under `"."` on POSIX rather
   than the literal Windows current-directory spelling.

Recommended option: add two tiny vendor helpers for pair preparation and
temporary-path construction, with exactly the archived operations as defaults;
normalize WorkDir externally and delegate the full algorithm to `super()`.
Alternative: retain the large copy. The recommendation removes substantial
duplication but requires mixed-case/import-order and temporary-file cleanup
parity tests before deleting the copy. No deep helper rewrite is necessary.

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

- MemTable's two-hook architecture needs the user's decision before replacing
  `Run_SQLite`.
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
that file's pre-existing `SubStitute_CT` AST and prefix are unchanged. The large
`Run_SQLite` AST is also unchanged.
