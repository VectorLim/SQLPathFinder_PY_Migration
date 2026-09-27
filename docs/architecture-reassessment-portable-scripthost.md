# Portable ScriptHost Architecture Reassessment

Date: 2026-09-27  
Required base: `direct-runtime/session-2.5a-portability-reuse-audit` @ `9cb2ed8800073a21102e2d1c03441deaf86fb545`  
Assessment branch: `direct-runtime/architecture-reassessment-portable-scripthost-v5`

## Executive conclusion

The original ScriptHost runtime is substantially more Linux-portable than the previous utility-by-utility direction assumed.

The strongest next architecture is **Option C: a hybrid centered on the original ScriptHost semantic runtime**, not a continued independent reconstruction of parser/control/report semantics in `vg2c_new`.

Concretely:

- keep the original `SPFManager`, `GetQuery`, `Process_Query`, `handleControlerTask`, `SPFTaskBase`, task hierarchy, controller behavior, macro behavior, and mature pure-Python utility/report algorithms as the semantic authority where Linux execution is proven;
- execute each VG2 job in an isolated Linux worker process so `SPFGlobals`, `os.environ`, cwd, logger caches, report counters, and other process-global legacy state cannot leak between concurrent jobs;
- replace or adapt only platform/integration boundaries that are genuinely unavailable on Linux, especially the compiled `dbDrivers` layer, SMTP transport, viewer/UI launchers, Win32/COM helpers, and service/usage-log transports;
- keep `vg2c_new` intact during the next phase as a comparison/fallback implementation. Do not cut over or delete it yet;
- keep Session 2.5B paused. Its separate report rewrite is useful comparison evidence, but should not be extended until the remaining original-report Linux gaps are characterized.

Confidence is **high for the architecture direction**, but this is deliberately not a claim that the original runtime is ready for production cutover. Query execution through a modern Linux backend and full deferred report rendering/cleanup remain explicit gates.

## What was actually reused and executed

The reassessment traced and exercised the original path:

```
SPFManager.Run_SPFSQL
  -> split on SQLFILE_DELIM
  -> SPFManager.Process_Query
  -> SPFManager.GetQuery
  -> SPFManager.handleControlerTask
  -> original SPFTaskBase/task subclasses
  -> original execute/parseTaskOptions/parseTaskCommand/executeTaskCommand
  -> original child-task execution
```

The prototype does **not** translate VG2 into `vg2c_new.Command`. The facade enters the real original runtime at `Run_SPFSQL`.

Proven on Ubuntu 24.04 / Python 3.12:

- original package import;
- original query/task resolution;
- mutable controller task-tree construction;
- WRITE-FILE;
- START/END-MACRO;
- IF/ELSE/END-IF;
- FOR-LOOP;
- SITE-LOOP;
- RUN-LOOP;
- local BEGIN/END-HPC execution;
- ROWS-IN-FILE;
- repeated execution of the representative task tree;
- concurrent execution when each job is placed in a separate process;
- original HTML-RUN CSS generation;
- original HTML-DEFER / HTML-LAYOUT / HTML-DELETE path can be entered and produces an HTML shell;
- full task-tree construction for the real 281 KB `scripthost-utilities-decompiled/22844.spfsql` fixture.

The real `22844.spfsql` contains two IF blocks, four OLEDB queries, one WRITE-FILE block, and thirteen UTILITIES blocks. The original Linux task builder accepted the complete file and produced one task object per non-empty source segment. `vg2c_new.parse` also accepted the same fixture.

## Minimal Linux amendments made

The decompiled runtime was not redesigned.

### SPFLib/SPFGlobals.py

Service-host detection no longer assumes the .NET `System` namespace exists on non-Windows platforms. Non-Windows execution is treated as non-service unless that integration is actually present.

### SPFLib/SPFUtilities/spflogger.py

The eager Windows-only `USERPROFILE + "\\My Programs\\SQLPathFinder3"` construction now has a portable home-directory fallback.

### SPFLib/SPFUtilities/utils.py

Three bounded amendments:

1. `SPFSMTPAuthEmail` is optional at import time; email fails clearly only when the unavailable legacy SMTP transport is actually invoked.
2. `unzipString` first decodes task payloads as UTF-8, matching `zipString`'s Python-3 encoding behavior; the historical detector remains a fallback.
3. `IntelWW` no longer changes the process locale to Windows `English_United States.1252`; it uses equivalent locale-independent date arithmetic.

### SPFLib/SPFSQL3.py

The Windows-only compiled `dbDrivers` package is optional at module import. Missing legacy DB base classes are represented by a fail-on-use boundary so the parser/task hierarchy can load and run. This does **not** pretend DB execution works on Linux.

### src/scripthost_portable

A small facade creates a real `SPFManager`, supplies per-job command-line state, switches cwd for the legacy job contract, calls `Run_SPFSQL`, then restores the caller cwd.

The cwd switch is intentionally process-global and reinforces the process-per-job isolation requirement.

## Windows/platform dependency classification

### Keep as semantic authority

- `SPFManager` task resolution and parser behavior;
- `Process_Query` / `handleControlerTask`;
- `SPFTaskBase` execution lifecycle;
- mutable task hierarchy;
- IF/macro/loop semantics;
- local filesystem/CSV algorithms that execute on Linux;
- `MemTable` where its SQLite/Python path is portable;
- mature report-generation code that can be proven portable incrementally.

### Minimal amendment

- import-time assumptions such as `USERPROFILE`;
- service-host detection;
- locale-dependent work-week calculation;
- Python-3 task string packing/unpacking;
- Linux path/cwd handling.

### Replace behind an adapter/transport boundary

- compiled `SPFLib/dbDrivers.cp*-win_amd64.pyd`;
- ATTDMongoDB Windows extensions;
- SMTP implementation from the compiled driver bundle;
- Data source connectivity that currently depends on Win32/.NET/ODBC binaries unavailable in the Linux image;
- `spfviewer.bat` and other local Windows UI launchers;
- SharePoint/Win32 identity integrations;
- usage logging/service discovery where the corporate endpoint is unavailable.

### Retire or keep disabled unless a current requirement proves otherwise

- generic DOS fallback;
- COM/JMP-only execution;
- obsolete ScriptHost/service transport plumbing;
- Windows helper executables that have a current portable replacement.

## dbDrivers finding

The bundled original ZIP contains:

- `SPFLib/dbDrivers.cp39-win_amd64.pyd`
- `SPFLib/dbDrivers.cp311-win_amd64.pyd`
- `SPFLib/dbDrivers.cp313-win_amd64.pyd`

and Windows ATTDMongoDB extensions.

There is no portable Python `dbDrivers.py` source in the assessed commit. Therefore Linux cannot directly reuse the historical database transport binary.

This is the most important unresolved runtime boundary. It does **not** invalidate reuse of the original parser/task/controller runtime. The next phase should prove one real query family by adapting the original query task to an approved modern Linux backend such as the project's DataSyncX reader layer, rather than reconstructing query/task semantics again.

## SPFGlobals and state isolation

### What is not a problem by itself

Mutable task objects are not a reason to reject the original architecture. `SPFTaskBase.__init__` replaces `childTasksList` per task instance, and the representative task tree executed repeatedly with identical outputs.

### Concrete shared state

`SPFGlobals` contains real process-global mutable execution state. Its command-line setter already has a substantial reset routine, but the reset is incomplete.

Observed across manager instances in one process:

- `gExecutionMode` **does reset** to `Normal`;
- `gRNStr` persists;
- `g_CWCtr` persists;
- `g_ChartCtr` persists;
- task utilities can write values into `os.environ`, which a later job in the same process observes;
- cwd is necessarily process-global while preserving legacy relative-path behavior.

The two-thread test also proves that command-line/global state is shared between concurrent managers.

### Isolation conclusion

Do **not** execute concurrent VG2 jobs as threads or as multiple manager instances inside one long-lived process.

A one-job-per-process boundary is sufficient to contain the observed global state. Two concurrently executed subprocess jobs completed independently with correct outputs, and the parent environment remained unchanged.

Across the final Ubuntu CI validation runs:

- same-process representative execution was about **13–18 ms/job**;
- fresh process-isolated execution was about **648–824 ms/job**;
- measured cold process-boundary overhead was about **0.63–0.81 seconds/job**.

For normal SQLPathFinder jobs dominated by database/network/report work, this appears to be a practical price for strong isolation. If startup cost later matters, Linux pre-fork or recyclable workers with one job per child can be benchmarked, but state isolation must not be weakened merely to avoid this overhead.

## Parser/runtime parity findings

### Strong parity

For the representative local workflow, original ScriptHost and `vg2c_new` agreed on:

- branch selection;
- macro first-row behavior;
- forward loop values;
- generated file names;
- generated file contents after normalizing outer trailing newlines;
- SiteLoop behavior;
- RunLoop final chunk behavior;
- local BEGIN-HPC child execution;
- representative `GetQuery` routing precedence.

### Concrete vg2c_new semantic divergence

The original parser preserves the separator-adjacent blank line in the task body. `vg2c_new` trims that outer blank line.

That changes observable behavior:

- original WRITE-FILE output has an additional trailing newline;
- `ROWS-IN-FILE` then counts two data rows in the tested output;
- `vg2c_new` counts one.

So the independent rewrite is not semantically identical even in a small local path. This is exactly the type of mature edge behavior that creates migration risk when semantics are reconstructed rather than reused.

## Report-stack findings

This reassessment materially changes the report conclusion from Session 2.5A.

### Proven

- original report classes import on Linux after the bounded portability amendments;
- HTML-RUN CSS generation executes successfully on Ubuntu and emits the expected stylesheet;
- HTML-DEFER saves its deferred report specification;
- HTML-LAYOUT enters the original Create_HTML_Window / Generate_HTML_Report flow, finds the local CSV, and creates the final HTML shell;
- Windows locale dependence in `IntelWW` was a small removable blocker.

### Not yet proven / current blockers

With a production-shaped deferred report fixture and local CSV:

- the generated layout shell does not contain the expected report-row value (`80%`);
- HTML-DELETE attempts cleanup but the deferred `*_MYREPORT5_tmp_.ini` remains on Linux;
- the off-corporate usage-log endpoint is unreachable, though `Record_SPF` treats that failure as best-effort and execution continues.

Therefore the report stack is a **strong reuse candidate, but full HTML data rendering and cleanup are not yet cleared for cutover**.

This is still materially different from concluding that the report stack should be independently rewritten. The paused Session 2.5B branch already introduced a separate ~650-line report runtime authority; the original stack should get one focused Linux repair pass before accepting that duplication.

## Objective option comparison

| Dimension | A. Continue `vg2c_new` | B. Portable original wholesale | C. Hybrid original semantics + portable adapters |
|---|---|---|---|
| Linux core/control feasibility | Proven | Proven for assessed local path | Proven |
| Semantic fidelity | Requires continued parity reconstruction; one concrete divergence already found | Highest where original code executes | Highest in retained domains |
| Concurrent safety | Strong per-run state model | Unsafe in same process | Strong with one-job-per-process boundary |
| Database portability | Modern rewrite/adapters required | Compiled legacy DB transport unavailable | Modern adapter required only at transport seam |
| Report coverage | Base has report gaps; paused 2.5B adds separate authority | CSS proven; deferred layout partly proven, blockers remain | Reuse original report stack where repaired; replace only irreducible platform edges |
| New semantic code | Highest | Lowest, but carries full legacy platform surface | Lower than A while avoiding unusable legacy transports |
| Migration risk | Risk of subtle behavioral drift | Risk from platform integrations/global state | Best balance: preserve semantics, contain state, replace only real platform blockers |
| Maintainability | Cleanest new code, but duplicates mature behavior | Large monolith and historical dependencies | Legacy semantic core behind a narrow process/adapter boundary |

## Recommendation

Proceed with **Option C**.

The original parser/task/controller hierarchy should become the preferred semantic runtime candidate for the next vertical slice. The process boundary—not a large `SPFGlobals` rewrite—should provide job isolation.

Do not yet cut over.

The next phase must prove:

1. one real OLEDB/query family from the actual fixture through a Linux-native backend while retaining the original task/parser semantics;
2. the deferred HTML report data-body and cleanup issues with a focused repair, without replacing the whole report engine;
3. process-isolated execution through the intended server/job interface;
4. a broader real-script parity corpus.

Only after those gates pass should the project decide whether the corresponding duplicated `vg2c_new` parser/control/report implementations can be retired.

## Effect on Session 2.5B

The paused branch remains preserved at:

`direct-runtime/session-2.5b-html-report-runtime`  
`30befea25faead8ef891e6d59a0ab9c0bf4dd107`

Do not continue it during this reassessment.

Its ~650-line direct report runtime is useful comparison evidence, but the architecture should first test whether the two remaining original report blockers can be fixed with small portability changes. If so, continuing a second report authority would create unnecessary semantic duplication. If not, the 2.5B implementation remains a viable fallback.

## Latest Linux validation evidence

At reassessment commit `b5c1f40b2bf573381ca40eec9123160e4ee09bb5`:

- Ubuntu 24.04 / Python 3.12;
- original `SPFLib.SPFSQL3` import succeeds;
- compressed task payload round trip returns Python `str`;
- **66 tests passed**;
- real `22844.spfsql` original task-tree construction passes;
- concurrent process-isolated jobs pass;
- formatting and Ruff checks pass;
- `vg2c_new` manifest remains:
  - 49 implemented;
  - 46 documented current-platform gaps;
  - 11 explicitly retired capabilities;
  - 2 flattened obsolete transports.

Compared with the required base, the experiment modifies only four decompiled ScriptHost source files (`SPFGlobals.py`, `SPFSQL3.py`, `SPFUtilities/spflogger.py`, and `SPFUtilities/utils.py`), adds a small 61-line facade, and places most new code in tests/benchmarking. The legacy-source delta is approximately 38 additions and 28 deletions; the rest of the branch delta is evidence scaffolding rather than a replacement runtime.

## Next implementation sequence

1. Keep this reassessment branch as evidence; do not merge to main.
2. Create the next experimental branch from the reassessment result.
3. Wrap one VG2 job in a subprocess/worker contract with explicit input/output/result/error capture.
4. Introduce a narrow query transport adapter for one real Oracle/DataSyncX path while retaining the original `nq*Task` resolution and execution lifecycle.
5. Run `22844.spfsql` as far as its available external dependencies permit and compare outputs/errors with the current environment.
6. Diagnose original `Generate_Report` prepared-row count and HTML-DELETE Linux path handling; make only bounded portability changes.
7. Expand parity fixtures around macros, nested controls, query output, reports, errors, and cleanup.
8. Reassess cutover only after those gates; until then, keep `vg2c_new` and the paused 2.5B branch intact.
