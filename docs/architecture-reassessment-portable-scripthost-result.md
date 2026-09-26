# Portable ScriptHost architecture reassessment — feasibility result

## Session authority and scope

This assessment follows `PROMPT_ARCHITECTURE_REASSESSMENT.md` from the portable ScriptHost reassessment execution pack.

Exact required base:

- branch: `direct-runtime/session-2.5a-portability-reuse-audit`
- commit: `9cb2ed8800073a21102e2d1c03441deaf86fb545`

Assessment branch:

- `direct-runtime/architecture-reassessment-portable-scripthost`

The assessment branch has the required commit as its exact merge base. No merge to `main`, no deletion of `vg2c_new`, and no final runtime cutover are part of this work.

The paused report branch `direct-runtime/session-2.5b-html-report-runtime` remains separate and is not a dependency of this assessment.

## Executive conclusion

The original ScriptHost semantic runtime is **feasible on Linux** without replacing its parser, task hierarchy, mutable tasks, or `SPFGlobals`.

The prototype executes the actual chain:

```
VG2 / SPFSQL text
    -> original SPFManager
    -> SPFManager.Run_SPFSQL
    -> SPFManager.Process_Query
    -> SPFManager.GetQuery
    -> original mutable SPFTaskBase task tree
    -> original controller tasks
    -> original Utilities / SPFGlobals behavior
    -> output
```

It does not translate the script into `vg2c_new.Command` for the original-runtime path.

The best target architecture is therefore **Option C: a hybrid that keeps the original ScriptHost parser/task semantic core authoritative, runs each top-level job in an isolated Linux worker process, and replaces only bounded Windows/obsolete integration transports with portable adapters**.

This is materially different from continuing `vg2c_new` as a second semantic runtime. The already-written `vg2c_new` code should remain intact during migration and is still valuable as:

- a parity oracle;
- a source of focused portable transport/utility implementations;
- a fallback during staged migration;
- a regression corpus.

It should not be deleted in this assessment.

## Why the previous Session 2.5A conclusion changes at the architecture level

Session 2.5A correctly concluded that importing individual original utility classes one-by-one was usually less clean than retaining the focused `vg2c_new` ports. That conclusion still stands at the *individual utility* level.

This reassessment tested a different question: whether reconstructing the original task host once allows all those mature task implementations to remain behind their original parser/task contract.

The answer is yes for the tested Linux slice.

This changes the reuse economics:

- utility-by-utility direct reuse requires repeatedly fighting `Utilities` / `SPFGlobals` coupling;
- whole-runtime reuse pays that portability cost once;
- existing task parsing, controller nesting, task mutation, macro substitution, report lifecycle, error routing, and many utility algorithms remain internally coherent;
- Windows-specific transports can be cut at explicit boundaries instead of causing a rewrite of the semantic layer above them.

## Evidence from the Linux prototype

### Linux importability

The following original components import successfully on Ubuntu/Python 3.12 in GitHub Actions:

- `SPFLib.SPFGlobals.SPFGlobals`;
- `SPFLib.SPFSQL3.SPFManager`;
- the portable ScriptHost foundations already prepared in Session 2.5A.

The packaged DB transport remains Windows-only. The original archive contains:

- `SPFLib/dbDrivers.cp39-win_amd64.pyd`;
- `SPFLib/dbDrivers.cp311-win_amd64.pyd`;
- `SPFLib/dbDrivers.cp313-win_amd64.pyd`;
- Windows ATTDMongoDB extension binaries.

Those binaries are now an explicit invocation-time transport boundary instead of an import-time blocker.

### Actual original-runtime vertical slice

The Linux probe executes a representative script through original `SPFManager.Run_SPFSQL()` with:

1. WRITE-FILE;
2. START-MACRO using a real CSV macro file;
3. IF/ELSE;
4. FOR-LOOP;
5. macro and loop-token substitution;
6. output file generation;
7. END-LOOP / END-IF / END-MACRO cleanup.

The same original manager successfully executes that representative local workflow twice.

The same script is also executed through `vg2c_new` for output comparison.

### SiteLoop parity

An original-runtime SiteLoop and the `vg2c_new` SiteLoop are run against the same script:

- sites: `A/B,C.D`;
- original `<<<spf-site>>>` token;
- original filename-safe `<<<spf-site-for-file-name>>>` token.

Both produce the same logical outputs.

### RunLoop parity

An original-runtime RunLoop and the `vg2c_new` RunLoop process the same three-row CSV in chunks of two.

Both preserve the original final-chunk behavior:

```
name,value
c,3
```

and both execute the child task.

### Original GetQuery routing on Linux

The original `SPFManager.GetQuery` successfully constructs the original task classes on Linux for representative semantic and boundary cases:

- WRITE-FILE -> `WriteFileTask`;
- BEGIN-HPC -> `BeginHPCTask`;
- HTML-DEFER -> `HTMLDeferTask`.

This matters because HPC and report commands remain part of the original parser/task model even when their transport or later lifecycle requires platform-specific replacement.

### Report lifecycle probe

The original `HTMLDeferTask` reaches its report lifecycle on Linux and calls the original report-spec persistence path.

The probe also exposed one bounded legacy integration: `Record_SPF` still attempts the historical internal SQLPathFinder log service. That request failure is already caught by the original code and is non-fatal, but a Linux production host should disable or replace this telemetry call rather than retain the network lookup.

The report stack therefore is not blocked by the parser/task architecture. Its external logging and any genuinely Windows-specific plot/process transports should be treated separately.

### Unsupported Windows DB path fails at invocation, not import

On Linux, constructing the legacy DB driver raises an explicit runtime error:

```
ScriptHost dbDrivers are unavailable on this host
```

Portable parsing, control flow, file tasks, and report routing still import and execute.

This is the desired failure boundary for a staged migration.

## Contained portability work relative to the exact 2.5A base

Relative to `9cb2ed8800073a21102e2d1c03441deaf86fb545`, the original ScriptHost source changes are limited to four source files:

| File | Change | Purpose |
|---|---:|---|
| `SPFLib/SPFGlobals.py` | 20 changed lines | skip .NET service probing on non-Windows; use native path separators |
| `SPFLib/SPFSQL3.py` | 17 changed lines | isolate Windows `dbDrivers`; deterministic UTF-8 task decompression |
| `SPFLib/SPFUtilities/spflogger.py` | 2 changed lines | portable user-home logger path |
| `SPFLib/SPFUtilities/utils.py` | 12 changed lines | optionalize Windows SMTP driver; remove unused eager Dask import |

Total: **51 changed ScriptHost source lines** in the current prototype, excluding tests, CI, and the 42-line prototype runner.

That is a bounded portability layer, not a wholesale rewrite.

## Original runtime architecture trace

### Top-level parser/executor path

`SPFManager.Run_SPFSQL` remains the real execution entrypoint.

It:

1. splits `MySPFSQLFileData` with `SQLFILE_DELIM`;
2. asks `Process_Query` to build tasks;
3. executes the returned original task objects;
4. handles `SPFNothingToProcessException` using the historical continue semantics;
5. calls `Final_CleanUp`;
6. raises real execution failures while setting `gMyAbort`.

`Process_Query` calls `GetQuery` for every block.

`GetQuery` still owns the historical routing precedence and maps a resolved task type to the real class in `SPFSQL3.py`.

`handleControlerTask` recursively builds the existing mutable controller tree for:

- macro blocks;
- IF/ELSE;
- ForLoop;
- SiteLoop;
- RunLoop;
- HPC blocks.

No replacement AST is required for this path.

### Task hierarchy

`SPFTaskBase(Utilities)` remains the base for the task handlers.

Representative original task classes include:

- `StartMacroTask`;
- `ForLoopTask`;
- `SiteLoopTask`;
- `RunLoopTask`;
- `IfThenTask`;
- `BeginHPCTask`;
- `WriteFileTask`;
- report tasks such as `HTMLRunTask`, `HTMLDeferTask`, `HTMLLayoutTask`, `HTMLJSTask`, and `HTMLPyPlotTask`;
- query tasks and the large set of historical file/data utilities.

Mutable tasks and deep-copy/substitution behavior are not Linux blockers.

## SPFGlobals state classification

`SPFGlobals` is large and intentionally class-state-heavy. That is an isolation concern, but it is not by itself a reason to reject the runtime.

| Category | Representative fields/behavior | Recommended treatment |
|---|---|---|
| CONSTANT / CONFIG | delimiters, token names, retry lookups, fixed version/config names | retain |
| PER-RUN MUTABLE | command-line arguments, `gSPFInstance`, SPFSQL file/data, `gMyLocal`, `gMyEXEDir`, macro files, random suffix, abort state, job timestamps | contain inside worker process |
| LOOP / CONTROL MUTABLE | `gAnyLoop`, `gAnyLoopCtr`, macro/loop bookkeeping | contain inside worker process; no architectural rewrite required |
| REPORT-RUN MUTABLE | `gHTMDelete`, `g_CWCtr`, `g_ChartCtr`, `gg_ChartCtr` | contain inside worker process |
| PROCESS-GLOBAL INTEGRATION | logger singleton, log-service URL/env, service identity, some environment-derived paths | initialize once per worker or replace boundary |
| WINDOWS-SPECIFIC | .NET service contexts, Win32 identity/registry paths, helper EXE locations, historical R/SH paths | guard/lazify or replace only when invoked |
| OBSOLETE TRANSPORT | SQLPFaaS/service transport, legacy helper processes, historical DB/mail mechanisms where modern services exist | replace/retire at boundary |

### Concrete reset leak

The current `gCommandLineArguments` setter invokes `__reInitStaticPropsDueToCmdUpdate`, but that reset is incomplete.

A real probe demonstrates:

```python
manager.gCommandLineArguments = ["probe", "/SPFINSTANCE=FIRST"]
manager.gSPFInstance == "FIRST"

manager.gCommandLineArguments = ["probe", "/SPFINSTANCE=SECOND"]
manager.gSPFInstance == "FIRST"
```

`gSPFInstance` is cached at class level and is not reset by the current reinitialization path.

This is evidence against running unrelated jobs sequentially in one long-lived Python interpreter without more cleanup.

It is **not** evidence against retaining `SPFGlobals` when process isolation is acceptable.

## Isolation and concurrency result

### Same process

A representative local script can run repeatedly on the same manager, but class-level state is not generally reset completely.

Therefore a long-lived multi-job interpreter is not currently a safe production isolation boundary.

### Separate process

A one-process-per-job runner has been prototyped in:

`tools/scripthost_portable_runner.py`

It:

1. imports the original ScriptHost package;
2. creates one `SPFManager`;
3. assigns an isolated working directory;
4. reads the original script;
5. executes `Run_SPFSQL`;
6. exits.

Two independent jobs run successfully one after another in separate processes.

Two independent jobs also run concurrently in separate processes with separate working directories.

This removes cross-job `SPFGlobals` leakage without rewriting the original state model.

### Startup cost

Ubuntu CI measurements for a minimal ScriptHost job have been approximately:

- cold process: about 1.4–1.7 seconds;
- subsequent independent process launches in the same CI job: about 0.58–0.65 seconds;
- observed three-run mean: about 0.9–0.96 seconds.

This is a real cost and should be measured against production job duration and throughput. It is nevertheless technically practical, especially compared with the migration risk of duplicating the whole semantic runtime.

If later performance data proves process startup material, a supervised worker pool can be investigated only after a complete reset contract exists. It should not be assumed safe now.

## Parser/runtime comparison

### Original parser/runtime

The original implementation already owns:

- block splitting;
- option recognition;
- `GetQuery` precedence;
- utility/task routing;
- normal query routing;
- controller nesting;
- mutable child task construction;
- macro substitution;
- current ForLoop Version 2 behavior;
- SiteLoop behavior;
- RunLoop chunk/error semantics;
- report task lifecycle;
- original no-op / dummy / historical fallback behavior;
- cleanup and error routing.

### vg2c_new

`vg2c_new` independently reimplements the same semantics.

At the current assessment head:

- `model.py + parser.py + runtime.py`: about **1,610 lines**;
- focused utility modules: about **2,359 lines**;
- the inspected `vg2c_new` source set totals about **4,011 lines**.

Its parser explicitly documents itself as an amended port of:

- `SPFManager.GetQuery`;
- `Process_Query`;
- `handleControlerTask`;
- `SPFTaskBase` option parsing.

Its runtime independently ports:

- IF;
- macro;
- ForLoop;
- SiteLoop;
- RunLoop.

This code is cleaner and easier to reason about in isolation, but it creates a second semantic authority that must continuously track the old runtime.

### Current coverage pressure

The manifest currently reports:

- 49 implemented targets;
- 46 documented current-platform gaps;
- 11 explicitly retired capabilities;
- 2 flattened obsolete transports.

A clean new runtime is therefore not automatically a smaller migration. Large semantic and report/query surfaces remain to be recreated or adapted.

### Observed semantic difference

The parity probe intentionally does not hide one concrete difference:

- original WRITE-FILE output contains its historical trailing newline;
- `vg2c_new` output currently does not.

The logical content matches after newline normalization, but the byte-level difference shows why maintaining two semantic engines creates continuing parity work.

## Code-size context

The original source is undeniably large:

| Existing original file | Approx. lines |
|---|---:|
| `SPFLib/SPFSQL3.py` | 22,401 |
| `SPFLib/SPFGlobals.py` | 2,597 |
| `SPFLib/SPFUtilities/utils.py` | 18,615 |
| `SPFLib/SPFUtilities/memtable.py` | 3,186 |

That size is a maintenance disadvantage of the original runtime.

However, those lines already contain the mature behavior. The decision is not “22k old lines versus zero lines”; it is “contained portability around proven old behavior versus continuing to recreate and validate that behavior in a new semantic engine.”

The hybrid recommendation keeps new Linux-specific code small while allowing gradual extraction later only where evidence justifies it.

## Platform-boundary classification

### KEEP as original semantic behavior

The current evidence supports retaining the original implementations as semantic authority for:

- `SPFManager` parser/routing;
- `Process_Query`;
- `GetQuery`;
- controller task construction;
- `SPFTaskBase`;
- macro behavior;
- IF/ELSE behavior;
- ForLoop Version 2;
- SiteLoop;
- RunLoop;
- task option parsing;
- report task orchestration;
- cleanup/error semantics that do not invoke obsolete transports.

### MINIMAL PORTABILITY AMENDMENT

Keep the original algorithm but amend host assumptions only where needed:

- filesystem separators;
- user-home/log path construction;
- non-Windows service-mode detection;
- import-time optional dependencies;
- encoding behavior that depended on host-specific detection;
- internal telemetry lookup.

### REPLACE TRANSPORT

Keep the original task contract/routing, replace the external mechanism:

- proprietary Windows DB `.pyd` drivers -> approved portable query/DataSyncX boundary;
- Mongo Windows extension -> supported current backend;
- Outlook / SMTPAuth Windows transport -> approved mail sender;
- `robocopy.exe` -> portable copy implementation;
- Windows Excel helper/COM -> portable Excel implementation;
- helper ZIP/UNZIP executables -> stdlib implementation;
- Windows/SH process launch assumptions -> Linux process adapter;
- legacy site-time/data-service lookups -> approved current data source;
- internal usage-log service -> optional portable telemetry or no-op.

### RETIRE where product behavior no longer requires it

Examples to validate against the product contract before final cutover:

- DOS fallback;
- JMP-only / COM-only paths when no longer supported;
- historical SQLPFaaS transport semantics if the new host owns scheduling;
- obsolete helper EXE/version-selection paths;
- legacy encryption/helper processes superseded by supported services.

Retirement should remain explicit and tested; it should not be inferred merely from platform incompatibility.

## Reports

The architecture evidence strengthens the report-reuse case beyond Session 2.5A.

The original report commands are already part of the same task hierarchy:

- HTML-RUN;
- HTML-DEFER;
- HTML-LAYOUT;
- HTML-TAB/MENU-LAYOUT;
- HTML-JS;
- HTML-PYPLOT;
- HTML-RPLOT;
- HTML-GNUPLOT;
- HTML-DELETE.

Their shared `Utilities`, `MemTable`, report counters, and lifecycle are a reason to retain the semantic host, not a reason to recreate that lifecycle elsewhere.

The paused `direct-runtime/session-2.5b-html-report-runtime` work should remain untouched for now. Its portability discoveries and tests can still be reused, but a future session should first ask whether any report code needs to be lifted out at all once the original task runtime is the Linux authority.

## DataSyncX and current integrations

A hybrid does not require restoring old DB or mail transports.

The integration direction should be:

```
original GetQuery / task contract
        |
        | original task owns VG2 semantics
        v
small portable adapter at transport boundary
        |
        +--> DataSyncX / approved query client
        +--> approved mail sender
        +--> portable filesystem/process implementation
```

The adapter should receive only the inputs required by that task and return results in the shape the original task expects.

Avoid:

```
original task
  -> convert to vg2c_new.Command
  -> Interpreter
  -> utility
```

That would preserve two semantic engines and defeat the main reuse benefit.

Existing `vg2c_new` focused utility implementations may still be called behind an adapter when they are already the best portable implementation; their `Command` parser/runtime need not become the execution authority.

## Objective A / B / C comparison

| Criterion | A — continue vg2c_new semantic runtime | B — portable original runtime only | C — original semantic core + portable transports |
|---|---|---|---|
| Linux parser/control feasibility | proven | proven by this session | proven by this session |
| Duplicate VG2 semantics | high | low | low |
| New parser/control maintenance | high | minimal | minimal |
| In-process state isolation | strong | weak today | use process isolation |
| Process isolation practicality | optional | proven | proven |
| Windows transport exposure | low | high unless bounded | bounded explicitly |
| DataSyncX integration | straightforward | requires adapter | requires small adapter |
| Mature report lifecycle reuse | low/currently separate work | strongest | strongest |
| Existing task/utility reuse | limited | strongest | strong |
| Legacy code complexity | low | highest | high internally but contained |
| Remaining implementation surface | 46 current gaps plus report/query work | transport/platform gaps | transport/platform gaps |
| Semantic parity risk | highest because behavior is recreated | lowest for retained tasks | low; concentrated at adapters |
| Migration/cutover risk | broad replacement | old transports can contaminate host | staged and bounded |
| Current evidence fit | weaker after prototype | technically viable | best balance |

## Recommendation

Adopt **Option C** as the architecture to validate in the next implementation session:

1. make the original ScriptHost parser/task runtime the candidate semantic authority;
2. execute one top-level VG2 job per isolated Linux worker process;
3. keep `SPFGlobals` and mutable task behavior initially;
4. replace only explicit Windows/obsolete transport boundaries;
5. use existing `vg2c_new` focused implementations as adapter implementations where they are already cleaner and portable;
6. keep `vg2c_new` intact as a parity oracle/fallback during migration;
7. require real-script parity before any final cutover;
8. do not resume broad report reimplementation until the original report path has been evaluated under this host.

This recommendation follows the session's reuse-first-but-don't-force-reuse principle. It reuses the original semantic architecture where Linux evidence supports it while preserving modern replacements where the original transport genuinely is the problem.

## Risks and open questions

The feasibility result is positive, but final cutover is not yet justified.

Remaining evidence needed before production authority changes:

- larger real VG2 corpus, especially nested mixed controllers;
- query tasks through the intended DataSyncX adapter;
- report HTML/JS/PyPlot lifecycle beyond defer/routing;
- explicit handling of HTML/report counters in isolated workers;
- unsupported Windows task inventory exercised at invocation boundaries;
- production throughput impact of process startup;
- cancellation/timeout/worker termination behavior;
- stdout/stderr/log capture contract;
- output artifact collection;
- temp directory cleanup after worker crash;
- security policy for embedded Python/R/process utilities;
- exact retirement decisions for SQLPFaaS, DOS/JMP, COM, and old helper processes.

None of these require replacing the original parser/task hierarchy merely to achieve Linux compatibility.

## Next-session implementation plan

The next session should remain pre-cutover and implement a thin worker/integration proof, not another semantic rewrite.

Recommended sequence:

1. define a small process request/result contract around the existing portable runner;
2. use one isolated working directory and one worker process per job;
3. make legacy usage telemetry optional/no-op on Linux;
4. choose one real DB-backed task and route only its transport through the approved current query adapter;
5. choose one already-ported filesystem utility and prove an original task can call that portable implementation without `vg2c_new.Command` translation;
6. run a real VG2 parity corpus through original Linux worker versus the current expected outputs;
7. exercise one complete HTML report workflow through the original report tasks;
8. document unsupported task boundaries;
9. only then decide whether a cutover session should be scheduled.

Do not delete `vg2c_new` or merge this assessment to `main` as part of those steps.
