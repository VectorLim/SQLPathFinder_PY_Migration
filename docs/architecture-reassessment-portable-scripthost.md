# Portable ScriptHost architecture reassessment

## Session authority and baseline

This reassessment follows PROMPT_ARCHITECTURE_REASSESSMENT.md from the supplied execution pack.

Required starting point:
- repository: VectorLim/SQLPathFinder_PY_Migration
- branch: direct-runtime/session-2.5a-portability-reuse-audit
- commit: 9cb2ed8800073a21102e2d1c03441deaf86fb545
- parent Session 2 commit: c51894176e89497c029036e825cd84bc0ecf1800

Assessment work is isolated on:
- direct-runtime/architecture-reassessment-portable-scripthost-v2

No main merge, vg2c_new deletion, or production cutover is part of this session.

## Executive conclusion

The original ScriptHost execution architecture is substantially more Linux-portable than the earlier utility-by-utility audit implied.

The experiment successfully exercised the real original chain on Ubuntu:

VG2/SPFSQL text
→ SPFManager.Run_SPFSQL
→ original query splitting
→ Process_Query
→ GetQuery
→ original task tree
→ original SPFTaskBase execution
→ original macro / IF / FOR control flow
→ original SPFGlobals and Utilities
→ original file utility
→ original HTML report task

This did not translate the test script into vg2c_new.Command objects.

Only a small set of ScriptHost source files needed portability amendments to get that representative runtime running. The mature task hierarchy, original parser/router, mutable task instances, SPFGlobals API, and HTML report task all remained in use.

The main architectural problem is not Linux itself. The concrete problems are:
1. a bounded set of Windows-only transports and binaries;
2. process-global mutable execution state in SPFGlobals;
3. legacy task routes whose implementation is intentionally not acceptable on the new host, such as arbitrary BAT/DOS fallback.

SPFGlobals is therefore not a reason to discard the original runtime. It is a reason to isolate one ScriptHost job per worker process unless and until the globals are refactored. That isolation was prototyped successfully and its startup cost was measured.

### Recommended direction

Use a hybrid architecture centered on the portable original ScriptHost parser/control/task runtime, with explicit portable replacements only at transport/integration boundaries.

In other words:

portable web/API host
→ one isolated worker process per VG2 job
→ original SPFManager / GetQuery / task tree / control semantics
→ original portable task algorithms and report lifecycle
→ narrow adapters for DB, mail, subprocess/file transport, and other host-specific integrations

Keep vg2c_new during migration as:
- a tested fallback;
- the source of already-proven portable transport implementations;
- a parity oracle for commands already ported;
- a rollback path until coverage proves the original-runtime worker can replace it.

Do not continue duplicating parser, control-flow, and report semantics in vg2c_new unless a specific original path proves infeasible.

## What was actually prototyped

### 1. Original runtime import on Linux

Ubuntu/Python 3.12 imports:
- SPFLib.SPFGlobals
- SPFLib.SPFSQL3
- SPFManager

The extracted Python source is usable on Linux after lazy/optional treatment of unavailable legacy dependencies.

The archived original distribution still contains Windows-only compiled modules:
- SPFLib/dbDrivers.cp39-win_amd64.pyd
- SPFLib/dbDrivers.cp311-win_amd64.pyd
- SPFLib/dbDrivers.cp313-win_amd64.pyd
- ATTDMongoDB/ATTDMongoDBDriver equivalents

Those are genuine transport blockers. They are not blockers for importing or running unrelated original tasks.

The prototype makes dbDrivers optional at import time and produces an explicit runtime error only if the unsupported legacy DB driver path is actually invoked.

### 2. Representative original-runtime workflow

The Linux test executes an original ScriptHost workflow containing:
- WRITE-FILE
- ROWS-IN-FILE against a real CSV
- IF / ELSE
- START-MACRO
- macro substitution
- nested IF
- FOR-LOOP
- nested WRITE-FILE

It runs through SPFManager.Run_SPFSQL and original task objects. It runs twice in the same process to expose reset behavior rather than only proving a one-shot import.

The equivalent supported slice is also executed through vg2c_new and output parity is checked. The two implementations agree on the produced files/content except for the already-observed original WriteFile newline convention.

### 3. Real repository script parsing

tests/fixtures/actual_script.txt is not a synthetic micro-fixture. It contains roughly 61 query blocks and exercises:
- HTML report commands;
- macros;
- IF / ELSE;
- ROWS-IN-FILE;
- write-file operations;
- SQLite queries;
- Oracle/SQLPlus-style queries;
- legacy helper/BAT routes.

The original Process_Query/GetQuery path constructs its task tree on Linux, including the expected:
- HTMLRunTask
- HTMLLayoutTask
- StartMacroTask
- IfThenTask
- RowsInFileTask
- WriteFileTask
- nqSQLiteTask
- nqOracleTask

This proves that the original parser/router is not intrinsically Windows-bound.

A direct comparison found one important compatibility difference: vg2c_new rejects the current actual script at getcsrsu.bat because shell fallback is deliberately disabled, while original GetQuery routes that legacy command to DOSCmdTask. This is useful evidence for the hybrid boundary: preserve original recognition/routing semantics, but replace or explicitly retire that named BAT integration rather than restoring arbitrary shell fallback.

### 4. Original HTML report lifecycle

The prototype executes the original HTMLRunTask on Ubuntu using the real CSS report block from actual_script.txt.

It successfully:
- saves and reads the original report specification;
- uses the original MemTable/report path;
- generates sqlpathfinder_style_1.css;
- cleans the temporary report specification.

This is materially stronger evidence than merely importing PyPlot_Class/PyGraphingMethods. The original task-level report lifecycle itself runs on Linux.

Broader report variants such as all chart/R/GNUPlot/external-helper combinations remain to be validated individually, but the core conclusion from Session 2.5A changes: the mature report stack is a direct-runtime candidate, not merely a library from which functions should be copied.

## Portability amendments made in this assessment

The ScriptHost source remains recognizable and authoritative. The experiment does not rewrite its architecture.

Modified legacy files:

### SPFLib/SPFGlobals.py
Minimal host portability:
- non-Windows service detection no longer dereferences unavailable .NET System APIs;
- local/executable directory values use native separators on Linux.

### SPFLib/SPFSQL3.py
Transport isolation:
- Windows-only dbDrivers import is optional;
- unsupported DB-driver construction fails explicitly when invoked rather than preventing SPFManager import.

### SPFLib/SPFUtilities/spflogger.py
Host path portability:
- logger home falls back to expanduser when USERPROFILE is absent.

### SPFLib/SPFUtilities/utils.py
Contained amendments:
- removes one unnecessary eager optional dependency;
- guards unavailable legacy SMTP auth transport;
- makes compressed-string decoding portable;
- Final_CleanUp uses native paths/native file deletion on non-Windows;
- SPFDelete uses native file deletion/glob handling on non-Windows while preserving the existing Windows command path.

These changes are host-boundary amendments. They do not replace the original parser, task hierarchy, macros, conditionals, loops, file algorithms, or report algorithms.

## SPFGlobals assessment

SPFGlobals contains substantial class/process-global mutable state. This is a concrete fact, not an architectural-style objection.

The source contains dozens of class-level fields, including per-run values such as:
- gMyAbort;
- gAnyLoop / gAnyLoopCtr;
- global variables;
- local/temp paths;
- command-line arguments;
- SPF instance;
- macro/script fields;
- HTML/report counters.

It also has an explicit reinitialization routine triggered by command-line changes. However that reset is incomplete for safe multi-tenant reuse.

### Proven same-process behavior

The test suite demonstrates:
- two SPFManager instances observe the same gAnyLoop value;
- overlapping threads can overwrite each other's gAnyLoop state;
- gSPFInstance can remain cached from a prior run even after command-line arguments are changed.

Therefore a multi-request web process must not run two original ScriptHost jobs concurrently in the same interpreter without further state refactoring.

### Classification

SPFGlobals is acceptable for migration when one ScriptHost job owns one worker process.

It should be classified as:
- acceptable legacy execution state under process isolation;
- not safe for concurrent jobs in one interpreter today;
- optional future refactor target, not a prerequisite to Linux migration.

This avoids a large speculative rewrite while giving the web host a strong isolation boundary.

## Process isolation feasibility

tools/scripthost_portable_runner.py is a process-level proof.

Tests cover:
- repeated separate-process execution;
- concurrent separate-process execution;
- original runtime output correctness in each isolated work directory.

GitHub Actions on Ubuntu measured process start + representative ScriptHost work in the sub-second-to-low-single-second range. Representative observed runs include:
- 1.415 / 0.723 / 0.712 s, mean 0.950 s;
- 1.050 / 0.437 / 0.462 s, mean 0.650 s.

The first iteration is consistently colder than later runs. These numbers are not a production throughput benchmark, but they establish that per-job process isolation is operationally plausible.

For long-running SQLPathFinder jobs, this fixed startup cost is likely small relative to database/report execution. Production sizing should still measure realistic jobs and concurrency before cutover.

## Parser and control-flow comparison

| Concern | Original ScriptHost | vg2c_new | Assessment |
|---|---|---|---|
| Query splitting | Mature Run_SPFSQL split + Process_Query | New parser implementation | Original proven on actual_script.txt |
| OPTIONS parsing | Original task parseTaskOptions | New parser option model | Both work for representative supported slice |
| GetQuery routing | Mature, broad legacy precedence | New explicit manifest/routing | Original covers more legacy shapes |
| Unknown/legacy BAT utility | Falls back to DOSCmdTask | Explicitly rejects shell fallback | Keep recognition, replace/retire named transport |
| Task tree | Mature mutable task objects | Immutable Command tree | Both viable; original has lower semantic migration risk |
| IF/ELSE | Original IfThenTask hierarchy | Reimplemented | Representative output parity passes |
| Macro | Original MemTable/StartMacroTask | Reimplemented RuntimeState frames | Representative output parity passes |
| FOR-LOOP | Original mature implementation | Reimplemented | Representative output parity passes |
| SITE/RUN loop | Original mature state/error semantics | New amended implementation | Original likely preserves more edge cases; needs dedicated parity fixtures |
| HPC | Original service/local behavior | vg2c_new intentionally flattens transport | Keep parsing/control, replace or retire remote service transport |
| Error/continue handling | Embedded in SPFTaskBase/tasks | New interpreter semantics | Original is the compatibility authority unless a path is retired |
| Report lifecycle | Mature task + report stack | Session 2.5B started a 650-line new report runtime | Original CSS lifecycle now executes on Linux |

## Platform-boundary classification

### KEEP

Keep original implementations where the Linux experiment shows no intrinsic host dependency:
- SPFManager.Run_SPFSQL orchestration;
- query splitting;
- Process_Query;
- GetQuery routing and precedence;
- SPFTaskBase/task hierarchy;
- controller nesting;
- IF/ELSE;
- macro processing;
- FOR/SITE/RUN control algorithms, subject to targeted parity tests;
- portable file/CSV algorithms when their invoked path is host-neutral;
- report task lifecycle and mature report algorithms when individually validated.

### MINIMAL PORTABILITY AMENDMENT

Amend without redesign:
- SPFGlobals OS/service detection;
- local path separators;
- logger home path;
- optional imports;
- file cleanup/delete primitives;
- encoding/decompression incompatibilities;
- other small os.name/platform guards discovered by real execution.

### REPLACE TRANSPORT / INTEGRATION

Do not try to make the old transport authoritative when the dependency itself is Windows-only, obsolete, unavailable, or superseded:
- compiled Windows dbDrivers and ATTDMongoDB binaries;
- Oracle/other DB access that should use the approved DataSyncX readers;
- Outlook/COM mail and legacy SMTP-auth helpers;
- robocopy.exe;
- BAT/helper-EXE implementations;
- Excel COM/helper executable paths;
- arbitrary DOS command fallback;
- Windows registry integration;
- JMP/other COM automation;
- Windows-specific R/interpreter discovery;
- legacy SQLPFaaS/service transport where it is no longer part of the required product contract.

The important design rule is to replace the transport below the mature VG2/task semantics where practical, not reparse/reimplement the entire command merely because its transport changes.

### RETIRE

A route should be retired only when product requirements confirm it is obsolete. Retirement should be explicit and produce a clear unsupported-command error. The parser may still recognize it so existing scripts fail at an understandable named boundary.

## Options A/B/C comparison

### A. Continue vg2c_new as the complete replacement runtime

Advantages:
- clean per-invocation RuntimeState;
- naturally suitable for same-process web concurrency;
- already-portable focused integrations;
- no inherited global host state.

Costs/risk shown by this assessment:
- duplicates the mature parser/router;
- duplicates macro/control-flow semantics;
- duplicates report semantics;
- currently rejects at least one command shape in the real repository script that original GetQuery accepts;
- Session 2.5B alone adds 650 lines for a new report runtime even though the original report task now demonstrably executes on Linux;
- future parity bugs are likely to cluster in edge cases already solved in ScriptHost.

This remains a valuable fallback, but the evidence no longer supports making it the default source of truth for every semantic layer.

### B. Rehost the original ScriptHost wholesale

Advantages:
- lowest semantic duplication;
- maximum compatibility with existing scripts;
- original report/control/error behavior retained.

Problems:
- Windows-only compiled DB drivers cannot run on Linux;
- same-process concurrent jobs are unsafe because SPFGlobals is shared;
- historical BAT/COM/Outlook/robocopy/helper routes are not acceptable production transports;
- some obsolete service behavior should not be restored.

A literal unchanged rehost is therefore not feasible.

### C. Portable original runtime plus explicit transport adapters

Advantages:
- keeps mature parser/control/task/report semantics;
- removes the largest duplication from vg2c_new;
- Linux changes observed so far are contained;
- process isolation solves the proven shared-state problem without a large SPFGlobals rewrite;
- existing vg2c_new/DataSyncX code can be reused where it is strongest: portable integration boundaries;
- old Windows binaries can fail only at the named path that needs replacement.

Costs:
- requires a deliberate adapter seam for normal database tasks and selected integrations;
- worker-process lifecycle/IPC must be productionized;
- unsupported legacy routes require an explicit inventory and migration decision;
- dedicated parity tests are still required for complex SITE/RUN/HPC/report variants.

Based on the evidence gathered here, C has the simplest ownership model with the lowest semantic-migration risk while satisfying Linux hosting.

## Proposed target architecture

### Host layer

The existing API/web application remains a modern Linux service. It should not import the mutable ScriptHost runtime into request-serving worker state.

Responsibilities:
- authorization/workspace isolation;
- job submission;
- work-directory preparation;
- cancellation/timeouts;
- stdout/stderr/log capture;
- result/artifact collection;
- worker exit-code translation.

### ScriptHost worker

One OS process owns one VG2 execution.

Responsibilities:
- import portable ScriptHost;
- create one SPFManager;
- establish per-job command-line/global inputs;
- run original Run_SPFSQL;
- execute original parser/task/control/report semantics;
- exit after the job.

This makes process teardown the authoritative reset of class/global state.

### Portable integration seam

Do not introduce a second generic plugin architecture unless proven necessary.

Prefer the smallest seam that fits each integration:
- adapt original normal-query task connection/execution to DataSyncX;
- use existing portable filesystem primitives for Windows shell-based file transports;
- use the approved mail sender for EMAIL;
- use normal Linux subprocesses only for explicitly supported commands;
- keep unsupported legacy routes named and explicit.

Where a vg2c_new utility is already the proven portable authority and the original task's only value is obsolete transport, call/reuse that focused implementation from an adapter rather than reimplementing it again.

## Implication for Session 2.5B

Pause direct continuation of direct-runtime/session-2.5b-html-report-runtime.

That branch currently contains a new src/vg2c_new/utilities/report.py of roughly 650 added lines. It should not be merged or deleted during this assessment.

Before extending it:
1. use this reassessment branch as the architecture baseline;
2. validate additional real HTML/report variants through original tasks;
3. identify only the original report operations that genuinely fail on Linux;
4. reuse any useful 2.5B code only as a narrow adapter/replacement for those failed boundaries.

Do not maintain two complete report semantic authorities.

## Remaining blockers / unanswered items

This assessment establishes feasibility, not final cutover readiness.

Still required before architecture cutover:
1. DB adapter prototype using a representative real SQLite and approved DataSyncX Oracle/query path while retaining original task routing.
2. Dedicated SITE-LOOP and RUN-LOOP parity fixtures, including error/continue semantics.
3. HPC product decision: local grouping semantics vs legacy remote SQLPFaaS transport.
4. Report matrix beyond CSS: HTML layout, deferred reports, plotting/chart paths, external helper dependencies.
5. Named inventory for all actual production legacy BAT/EXE/COM commands and an explicit keep/replace/retire decision for each.
6. Worker cancellation, timeout, log streaming, resource limits, and artifact contract.
7. Production concurrency/load measurements for process workers.
8. Security review of command/process routes. Arbitrary DOS/shell fallback should not be restored.
9. End-to-end production-like scripts across multiple products, not only actual_script.txt and the representative local fixture.

None of these findings require deleting vg2c_new or cutting over now.

## Validation evidence

The assessment branch has Ubuntu CI coverage for:
- ScriptHost archive inventory;
- SPFGlobals Linux import;
- SPFManager Linux import;
- unavailable Windows DB transport failing only when invoked;
- representative original Run_SPFSQL execution;
- repeated original run in one process;
- shared-state behavior across manager instances;
- deterministic overlapping-thread contamination;
- repeated isolated-process runs;
- concurrent isolated-process runs;
- output parity with vg2c_new for the representative supported slice;
- original parser/task-tree construction for actual_script.txt;
- explicit vg2c_new rejection of the real script's legacy BAT shell fallback;
- original HTML CSS report generation and cleanup.

The final implementation/test checkpoint before this report update is 66 tests passing on Ubuntu with compile and Ruff checks green. The explicit parser-difference assertion passes and documents vg2c_new's deliberate rejection of the legacy BAT shell fallback rather than masking it. The latest process-isolation sample was 2.138 / 0.554 / 0.553 s (mean 1.082 s), while earlier green runs measured means of 0.650 s and 0.950 s; this variance reinforces that the measurement is a feasibility probe, not a production throughput benchmark.

## Next implementation session

Recommended next session: prove the hybrid query boundary.

Start from the final commit of this architecture reassessment branch and:
1. leave original SPFManager/GetQuery/task classes authoritative;
2. choose one actual-script SQLite query and one Oracle-style query;
3. trace original NormalQueryTaskBase connection/execution seams;
4. inject the existing portable SQLite/DataSyncX transports with minimal changes;
5. compare files/row counts/error behavior against vg2c_new/current expectations;
6. keep worker-process isolation;
7. add Linux CI;
8. stop and reassess before broad integration replacement.

The objective is to prove that normal query transports can be swapped underneath the original semantic runtime without reconstructing the entire ScriptHost host environment.
