# Final runtime convergence handoff

## Architecture and source

Branch: `direct-runtime/utility-convergence-audit`.
Starting local and remote SHA: `979fae6e9ecd1eafcb6b7a45ec7c9aa41e35904d`, clean.
No merge to main. Obtain the exact delivered revision with `git rev-parse HEAD`.

```
VG2 -> launcher/run_job -> fresh child -> PortableScriptHostRuntime
    -> original SPFManager/GetQuery/task hierarchy/SPFGlobals
    -> original ScriptHost semantics
```

Remaining portable source:

```
src/scripthost_portable/
    __init__.py
    runtime.py
    worker.py
    query_transport.py
    file_operations.py
    launcher.py
```

Original source and assets remain under `scripthost-utilities-decompiled/SPSQL3_py`;
its original ZIP remains provenance. There is no parser, interpreter, utility registry,
editor API, generated-Python workflow, or alternate report runtime in portable code.
The launcher only parses arguments, constructs ScriptHostJob, calls run_job, prints
its structured result and returns an exit status. Standalone packaging is out of scope.

## Deletions and independence

Removed all of `src/vg2c_new`, `src/vg2c`, `src/vg2c_ui`, their tests and generated
compiler/UI outputs, frontend workflow/contracts/launcher, stale architecture plans,
and editor Docker deployment. Shared VG2 source fixtures remain as test inputs.
Static import checks cover the entire original Python tree plus portable source;
a fresh worker also executes with all three retired imports explicitly blocked.
Supported tests execute after physical removal of those local source directories.

## Boundaries

Portable code owns fresh-process execution, DataSyncX query I/O, copy/delete/unzip/
RoboCopy-subset operations, bounded XML conversion, and CSV/TAB preprocessing.
Original helper amendments cover POSIX path/case handling (including SQLite's VG2 `.\\` workdir), file modes, native moves,
.xlsx conversion, local R temporary paths and public HTTP transport.
Report algorithms and control-flow semantics remain original. Linux email explicitly
fails at original SPFEmail: delivery and identity transport are UNRESOLVED, and no
recipient/role policy was weakened or replaced.

## Certification

- Existing utility contract suite: real worker jobs assert file contents, SQLite/UDF
  results, SmartAppend V4 changes, archive contents, Python/R output and failure behavior.
- Worker control-flow slice: macros, IF/ELSE, ForLoop, SiteLoop, RunLoop final chunk.
- Real 22844 MARS/ARIES/SQLite source slice: original preprocessing and output joins;
  deterministic readers live only in test fixtures at the external transport boundary.
- Reports: html_test and tcb_yield slices preserve report columns, sorting and layout;
  substituted local CSV/CSS/output paths, removed email destinations/attachments,
  and selected original batch mode. Assert data markers and cleanup, not just success.
- HTML-TAB/MENU/plotting are UNCERTIFIED, as are full network-heavy 22844 execution,
  remote Python/R and authenticated SharePoint. No claim of blanket VG2 parity.

## Actual DataSyncX contract and live evidence

Installed version inspected: 1.1.6. Canonical public imports:
`from datasyncx import MarsReader, AriesReader, OracleReader`.
Constructors: `MarsReader()`, `AriesReader()`, `OracleReader(database="OASYS")`.
Read call: `reader.read(site=site, query=query)` returns pandas.DataFrame.
Lazy imports keep local jobs independent of private transport installation.
Invalid return values are errors; speculative module/return-type fallbacks are gone.
Scoped reader injection remains solely useful for deterministic tests.

Live validation script now calls `worker.run_job` for every job, with no replacement
parser or execution layer. [Sanitized evidence](final-runtime-live-evidence.json):
MARS **4**, ARIES **12,750**, SQLite **1,454** rows; query-chain relationships,
empty header fallback and null/dot/Unicode checks passed. Raw rows/logs remain under
ignored `data/final-convergence-live`. Live host was Windows/Python 3.14.6 already
installed here; DataSyncX metadata declares Python 3.11-3.13, and Linux target is 3.12.
Python 3.14 installation compatibility is not promised.

## Validation and cutover limits

Windows suite and exact Linux/container outcomes are recorded in the final task
handoff and linked Actions runs. The first deletion/convergence checkpoint
[ab7e9cc Ubuntu validation](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/36323730788)
passed before expanded certification.

The container preserves the checkout layout, runs as non-root and contains no UI or
private credentials. See [container instructions](runtime-container.md). Local Docker
Desktop denies engine access, so container execution is validated in Actions.
Corporate DataSyncX inside the Linux container remains UNCERTIFIED until approved
private dependencies, Oracle/authentication prerequisites and network access are supplied.
Linux email delivery remains UNRESOLVED. These are explicit cutover limits; they do
not justify retaining or rebuilding a second runtime.
