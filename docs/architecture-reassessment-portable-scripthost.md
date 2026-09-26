# Portable ScriptHost architecture reassessment

## Session authority and scope

This assessment follows `PROMPT_ARCHITECTURE_REASSESSMENT.md` from the
architecture-reassessment execution pack.

Exact starting point:

- branch: `direct-runtime/session-2.5a-portability-reuse-audit`
- commit: `9cb2ed8800073a21102e2d1c03441deaf86fb545`

Assessment branch:

- `direct-runtime/architecture-reassessment-portable-scripthost-v4`
- validated implementation/test head before this report:
  `3879e2c8062a96a7ad710e4c9b9e2f4be13388ba`

The branch was created directly from the requested commit. Nothing was merged to
`main`. `vg2c_new` was not deleted or cut over. The paused Session 2.5B report
branch was not modified.

The hard migration constraint used throughout this reassessment is Linux
compatibility. Architectural style alone was not treated as a reason to reject
`SPFGlobals`, mutable task objects, the existing task hierarchy, or the original
parser/runtime.

## Conclusion

The original ScriptHost execution core is **feasible on Linux** and is a materially
stronger reuse candidate than Session 2.5A established.

The experiment successfully ran the decompiled runtime itself on
`ubuntu-24.04` / Python 3.12 through:

```text
SPFManager.Run_SPFSQL
    -> Process_Query
    -> GetQuery
    -> handleControlerTask
    -> original SPFTaskBase subclasses
    -> original task execution
```

The tested workflow used the real original task hierarchy for:

- `WRITE-FILE`;
- `START-MACRO` / `END-MACRO`;
- `IF-THEN` / `ELSE` / `END-IF`;
- `FOR-LOOP` / `END-LOOP`;
- macro CSV loading and substitution;
- loop-token substitution;
- actual filesystem output.

No reimplementation of those semantics was introduced for the prototype.

The strongest resulting architecture is a **hybrid portable ScriptHost core**:

1. retain the original parser, `SPFManager`, task hierarchy, `SPFGlobals`,
   and portable task/helper algorithms as the semantic authority;
2. execute one VG2 job per fresh OS process so process-scoped legacy state is
   contained instead of being redesigned merely for style;
3. replace only platform-bound or unavailable transports at narrow boundaries
   such as legacy database drivers, COM/Win32 operations, DOS/helper executables,
   and obsolete mail/service integrations;
4. retain `vg2c_new` during migration as a tested comparison/fallback and as a
   source of already-proven Linux transport implementations where reusing them
   behind the original task contracts is simpler than rebuilding a transport.

This is a feasibility recommendation, **not a cutover**.

## What was actually prototyped on Linux

### Importability

Session 2.5A had already made `SPFLib` substantially more granular, but the
whole runtime still did not import because `SPFLib/SPFSQL3.py` eagerly imported
`SPFLib.dbDrivers`.

The repository does not contain that `dbDrivers` package. This is a missing
integration/source boundary, not simply a Linux incompatibility.

The prototype made database-driver imports optional at module load. If an old
driver path is actually invoked, it now fails explicitly rather than silently
pretending that the driver exists. This allows unrelated original parser,
controller, and local utility paths to run.

The original runtime then imports on Linux:

- `SPFLib.SPFGlobals.SPFGlobals`;
- `SPFLib.SPFUtilities.utils.Utilities`;
- `SPFLib.SPFSQL3.SPFManager`;
- `SPFLib.SPFSQL3.SPFTaskBase`.

### Original runtime execution

A real `WRITE-FILE` VG2 task was executed through
`SPFManager.Run_SPFSQL()`. It reached the original `WriteFileTask` and wrote
the expected file.

The first attempt exposed a Python-3 dependency issue in the original compressed
task payload path: `SPFTaskBase.SPFTaskItem` is compressed by `zipString()`,
then decompressed by `unzipString()`, which uses `detectCharacterEncoding()`.
Without `chardet`, the legacy helper returned the compressed `bytes` payload
on error, causing downstream string parsing to fail.

The fix was not to rewrite task semantics. `chardet` was made an explicit
portable runtime dependency in Linux validation. The original string
compression/decompression path then worked unchanged.

### Representative control-flow workflow

The Linux prototype then ran an original-runtime script containing:

```text
WRITE-FILE
START-MACRO
  WRITE-FILE with <<<macro-column>>> substitution
END-MACRO
IF-THEN
  WRITE-FILE
ELSE
  WRITE-FILE
END-IF
FOR-LOOP
  WRITE-FILE with <<<spf-loop-ctr-...>>> substitution
END-LOOP
```

The test verifies:

- the initial file;
- macro CSV expansion;
- true IF branch execution;
- ELSE branch suppression;
- loop outputs for 0, 1, and 2.

This ran through the decompiled ScriptHost parser/task objects, not
`vg2c_new`.

### Direct parity with `vg2c_new`

The same representative semantic shape is also executed by `vg2c_new`.

A cross-runtime test runs an original ScriptHost copy and a `vg2c_new` copy
against isolated directories and compares their resulting files.

For the tested portable slice, outputs are identical:

```text
initial.txt   -> initial
macro.txt     -> macro-expanded
if-true.txt   -> if-branch
if-false.txt  -> absent
loop-0.txt    -> loop-0
loop-1.txt    -> loop-1
loop-2.txt    -> loop-2
```

This is intentionally a representative parity result, not a claim of complete
runtime parity.

### A concrete semantic difference

The reassessment also tested a historical `FOR-LOOP` version selector.

The original `ForLoopTask` still contains and executes its Version-1 branch.
`vg2c_new.runtime.Interpreter` explicitly rejects historical version selection
and states that only Version-2 semantics remain.

The Linux test proves this difference rather than inferring it from comments:
the original executes the historical one-pass behavior, while `vg2c_new`
raises the expected runtime error.

This is evidence that continuing to reconstruct the full runtime in
`vg2c_new` can intentionally or accidentally narrow mature ScriptHost
semantics.

## Runtime trace and ownership

### Top-level executable

`scripthost-utilities-decompiled/SPSQL3_py/SPFSQL3.py` is still a Windows host
wrapper. It eagerly imports `win32console` and drives host-specific setup.

It should **not** be the Linux service entry point.

This does not require rejecting the underlying runtime. A portable worker can
invoke `SPFManager.Run_SPFSQL()` directly, which is what the Linux prototype
does.

### `SPFManager`

The original `SPFManager` remains the central execution coordinator.

Relevant flow:

```text
Run_SPFSQL
  -> split VG2 input into query segments
  -> Process_Query
       -> GetQuery
            -> resolve original task type
            -> instantiate original SPFTaskBase subclass
       -> handleControlerTask
            -> construct nested controller hierarchy
  -> execute task list
```

`GetQuery` still contains the mature resolver map for utilities, control
tasks, report tasks, query handlers, and special tasks. Replacing this map with
a separately maintained resolver is therefore a duplication of a substantial
semantic authority.

### `SPFTaskBase` and mutable tasks

`SPFTaskBase` provides the original task lifecycle:

```text
execute
  -> parseTaskOptions
  -> parseTaskCommand
  -> executeTaskCommand
  -> executeChildTasks
```

Mutable task state is not, by itself, a migration blocker. The control
implementations legitimately mutate/copy child tasks during macro and loop
substitution.

The actual problem is **scope of shared mutable state**, not mutability in
general. Process isolation gives each job its own task graph and makes the
existing mutable task model usable without first rewriting it into immutable
domain objects.

### Control tasks

The original implementations remain directly reusable candidates:

- `StartMacroTask` / `EndMacroTask`;
- `IfThenTask` / `ElseTask` / `EndIfTask`;
- `ForLoopTask`;
- `SiteLoopTask`;
- `RunLoopTask`;
- `EndLoopTask`;
- `BeginHPCTask` / `EndHPCTask` for semantic routing, with old remote/HPC
  transport evaluated separately.

### Query tasks

The original normal-query hierarchy contains mature query semantics, but the
historical database transport layer cannot be adopted wholesale because the
referenced `SPFLib.dbDrivers` package is absent from this repository.

Therefore:

- preserve parsing, task selection, options, query/macro/control semantics where
  useful;
- replace the database connection/transport edge with the approved Linux
  backend, such as the existing DataSyncX-based integration where applicable;
- do not reconstruct the missing Windows-era driver package merely to preserve
  class shape.

### Report tasks

`GetQuery` still routes the full report family, including:

- `HTML-RUN`;
- `HTML-DEFER`;
- `HTML-LAYOUT`;
- tab/menu layout;
- GNU/R/Python plot tasks;
- HTML delete and JS report variants.

Session 2.5A already established that the standalone report modules are among
the strongest direct-reuse candidates. This reassessment does not invalidate
that result.

The paused Session 2.5B branch is therefore preserved, but final report
integration should be resumed only after the runtime worker boundary is
implemented so report code is evaluated in its intended original task context
rather than as a parallel replacement runtime.

## `SPFGlobals` state classification

The reassessment did not reject `SPFGlobals` because it contains mutable or
class-level state. It traced how that state behaves.

| State category | Examples | Assessment |
|---|---|---|
| Static/read-mostly constants and path definitions | format constants, names, fixed defaults | Keep where portable; move environment-specific values to config only when needed |
| Per-job mutable state that the existing command reset handles | `g_gVars`, loop counters, abort flag, macro file, local/exe-dir caches, SQL file fields, chart counter | Usable inside a single-job process |
| Process caches not fully reset when command arguments change | `gSPFInstance`, execution/service/session-related caches, random/process identifiers and other class caches | Unsafe as the isolation mechanism for a reusable multi-job worker |
| Windows-bound accessors | `gUN`/`gUDomain` via `win32api`, service detection through `.NET/System.*`, Windows-path assumptions | Replace/lazify only if the Linux execution path needs them |
| Legacy environment/service constants | old shared paths, service locations, historical infrastructure defaults | Inject/configure when still required; otherwise leave outside supported path |

A direct source finding is important here:
`__reInitStaticPropsDueToCmdUpdate()` resets many fields, but it does **not**
reset every class-level cache. In particular, `__gSPFInstance` is not reset.

The test suite demonstrates this directly: a second manager can replace
`gCommandLineArguments` and still observe the first job's cached
`gSPFInstance`.

That means a long-lived Python worker that executes unrelated jobs sequentially
cannot claim complete isolation merely by updating command-line arguments.

## Isolation experiments

### Repeated sequential execution in one process

Two simple local `WRITE-FILE` runs can execute sequentially in one interpreter
and produce the expected files.

However, the explicit `gSPFInstance` cache test proves that this successful
local slice is **not evidence of complete reset semantics**.

Result:

- mechanically possible for some workloads;
- not a safe default production isolation boundary.

### Concurrent execution in threads

A synchronized two-thread test uses two separate `SPFManager` instances.

Thread A sets one `/MYLOCAL`; Thread B then sets another. After B updates the
shared `SPFGlobals.gCommandLineArguments`, Thread A observes B's local
directory.

Result:

- separate manager objects do not isolate jobs;
- thread-per-job execution in one interpreter is unsafe without a major
  `SPFGlobals` redesign.

That redesign is not necessary if a process boundary is acceptable.

### Concurrent execution in fresh processes

Two Python subprocesses are started concurrently. Each process imports the
original runtime, creates its own `SPFManager`, runs an original
`WRITE-FILE` task, and writes to its own workspace.

Both complete successfully and produce independent expected output.

Result:

- process-per-job is a valid containment mechanism for the original global/task
  model on Linux.

The production form should be a fresh process per job, or a worker process that
is guaranteed to execute exactly one job before exiting. A conventional
long-lived process pool that reuses workers for unrelated jobs should not be
treated as isolated unless a complete reset contract is separately proven.

## ScriptHost files changed by this prototype

Only three decompiled ScriptHost source files were changed.

### `SPFLib/SPFSQL3.py`

Change:

- make the repository-missing `dbDrivers` integration optional at import;
- retain explicit failure if a missing legacy driver is actually invoked.

Diff against the requested base at the validated test head:

- +22 / -2 lines.

No parser, controller, macro, IF, loop, or local `WRITE-FILE` semantics were
rewritten.

### `SPFLib/SPFUtilities/utils.py`

Change:

- make the missing legacy SMTP-auth driver optional at import.

Diff:

- +3 / -4 lines.

### `SPFLib/SPFUtilities/spflogger.py`

Change:

- allow a Linux home-directory fallback when `USERPROFILE` is absent.

Diff:

- +1 / -1 line.

### Not changed

Notably, this reassessment did **not** modify `SPFGlobals.py` to make the
prototype work.

The process-isolation result was obtained with the existing class-level global
model.

## Component classification

| Component | Classification | Reason |
|---|---|---|
| Linux worker entry point | **MINIMAL PORTABLE ADAPTER** | Top wrapper is Windows-specific; call original manager from a portable process worker |
| `SPFManager.Run_SPFSQL` | **KEEP** | Executed successfully on Linux |
| `Process_Query` | **KEEP** | Part of successful original pipeline |
| `GetQuery` resolver | **KEEP** | Mature routing authority; successful portable control/file tasks |
| `handleControlerTask` | **KEEP** | Builds original nested task graph successfully |
| `SPFTaskBase` lifecycle | **KEEP** | Original lifecycle executes successfully |
| mutable task hierarchy | **KEEP** | Per-job mutation is valid when job is process-isolated |
| `SPFGlobals` | **KEEP INSIDE ONE-JOB PROCESS** | Style is not blocker; class state is contained by OS process |
| `MemTable` | **KEEP INSIDE ONE-JOB PROCESS** for proven paths | Macro path works; class-level state should not cross jobs |
| macro tasks | **KEEP** | Linux-proven |
| IF/ELSE tasks | **KEEP** | Linux-proven |
| FOR-LOOP | **KEEP** | Linux-proven and preserves semantics not present in `vg2c_new` |
| SITE/RUN loop | **KEEP CANDIDATE; TEST NEXT** | Same hierarchy; not yet fully exercised in this spike |
| local `WriteFileTask` | **KEEP** | Linux-proven |
| portable `Utilities` helpers | **KEEP SELECTIVELY** | Do not replace merely because class is large |
| old `dbDrivers` transport | **REPLACE TRANSPORT** | Package is absent from repository; old transports are not a viable authority |
| DB/query semantic routing | **KEEP / ADAPT** | Preserve VG2 semantics while delegating transport to modern Linux backend |
| `win32console` executable wrapper | **REPLACE ENTRY TRANSPORT** | Windows-only shell, not semantic runtime |
| `gIsSvc` .NET service probing | **REPLACE/OMIT AT WORKER EDGE** | Not needed by direct Linux worker path |
| `gUN`/`gUDomain` Win32 identity | **REPLACE TRANSPORT IF NEEDED** | Use portable identity/config only for commands that require it |
| DOS/`COMSPEC` task transport | **REPLACE TRANSPORT** | Windows-specific |
| `robocopy.exe`, BAT/helper EXEs | **REPLACE TRANSPORT** | Linux filesystem implementations already exist in `vg2c_new` |
| Excel COM/helper transport | **REPLACE TRANSPORT** | Keep command semantics; use portable libraries |
| Outlook/legacy SMTP transport | **REPLACE TRANSPORT** | Keep task contract; use approved sender |
| legacy service/HPC remote transport | **REPLACE OR RETIRE BY PRODUCT REQUIREMENT** | Control scope can remain; historical host mechanism is not Linux authority |
| report parser/task routing | **KEEP** | Mature original semantic surface |
| portable report algorithms | **KEEP / DIRECT REUSE CANDIDATE** | Consistent with Session 2.5A findings |
| process cleanup | **KEEP**, with process exit as final containment | Existing cleanup still useful; OS exit guarantees job-global disposal |

## Objective comparison with `vg2c_new`

### Size and semantic ownership

At the assessed head:

- original `SPFLib/SPFSQL3.py`: about 22,408 lines;
- original `SPFGlobals.py`: about 2,593 lines;
- original `SPFUtilities/utils.py`: about 18,614 lines;
- `vg2c_new/parser.py`: about 1,002 lines;
- `vg2c_new/runtime.py`: about 553 lines;
- all files under `src/vg2c_new`: about 159 KB at the Session 2.5A base.

The original runtime is clearly much larger and less modular. That is a
maintenance cost.

However, the smaller `vg2c_new` runtime is not merely an adapter. It contains a
second parser/resolver, control-flow interpreter, runtime-state model, macro
implementation, loop implementation, condition implementation, and many ported
utility semantics. Its cleanliness therefore comes with semantic duplication.

The Linux feasibility result changes the tradeoff: the duplication is no
longer necessary solely to escape Windows imports.

### Coverage

Current `vg2c_new.RESOLVER_MANIFEST` status in Linux CI:

- 49 implemented;
- 46 documented current-platform gaps;
- 11 explicitly retired capabilities;
- 2 flattened obsolete transports.

This accounting is useful and should be retained as migration evidence, but it
also shows how much behavior a fully replacement runtime still has to decide,
reconstruct, or intentionally drop.

The original `GetQuery` already routes the mature task surface. The new problem
becomes narrower: determine which original handlers are portable and replace
the platform transport only where required.

### Isolation

`vg2c_new` has a clear advantage for in-process isolation. Its
`RuntimeState` explicitly owns per-run globals and frame state, and its command
tree is cleaner for diagnostics and tests.

The original runtime has a clear disadvantage for same-interpreter concurrency
because `SPFGlobals` and some helper classes use process/class state.

With process-per-job execution, that disadvantage becomes contained rather than
requiring a broad semantic rewrite.

### Diagnostics and maintainability

`vg2c_new` provides cleaner source spans, smaller modules, explicit runtime
state, and easier unit isolation.

The original code is decompiled, monolithic, and carries historical branches
and unused platform code. It should not be idealized.

The architecture decision is therefore not "old code is cleaner." It is:

> preserve the mature semantic authority where it already works on Linux, and
> put a small, explicit process/transport shell around it instead of maintaining
> a second implementation of the same language/runtime.

## Option comparison

### Option A — continue `vg2c_new` as the replacement runtime

**Feasibility:** high.

**Strengths:**

- Linux-native design;
- good in-process isolation;
- cleaner modules and tests;
- explicit source spans and runtime state;
- many modern transport implementations already exist.

**Costs/risks:**

- duplicates the original language/runtime semantics;
- current manifest still contains many gaps/retired/flattened entries;
- semantic drift must be discovered and maintained indefinitely;
- proven example: historical FOR-LOOP behavior differs;
- every obscure mature ScriptHost behavior becomes a reverse-engineering task.

This remains a valid fallback if the original runtime later proves unportable
on representative production workloads, but the Linux spike no longer supports
choosing it merely because the original architecture is stateful.

### Option B — use the original ScriptHost runtime wholesale

**Feasibility:** partial, not complete.

The parser, control hierarchy, globals, macro processing, loop processing, and
local tasks are feasible.

A literal wholesale restoration is not feasible because:

- the top executable wrapper is Windows-specific;
- the referenced `dbDrivers` package is absent;
- some identity/service paths directly require Win32/.NET;
- multiple utilities rely on DOS/COM/helper executables;
- historical deployment/service transports are obsolete.

Rebuilding those missing/Windows layers would violate the reuse-first principle
by recreating infrastructure that is not required on the Linux host.

### Option C — original semantic core + process isolation + selective portable transports

**Feasibility:** strongest based on current evidence.

Shape:

```text
Linux API / job service
        |
        | spawn one fresh worker process
        v
portable ScriptHost worker entry point
        |
        v
original SPFManager / GetQuery / task hierarchy / SPFGlobals
        |
        +-- portable original task/helper path ----------> use directly
        |
        +-- platform/missing transport boundary ---------> modern adapter
                                                            |
                                                            +-- DataSyncX DB access
                                                            +-- pathlib/shutil
                                                            +-- portable Excel libs
                                                            +-- approved mail sender
                                                            +-- other explicit integrations
```

This option preserves mature VG2 semantics while constraining legacy global
state and minimizing the amount of code that must become authoritative twice.

It is the recommended architecture to prototype further.

## Remaining Linux blockers and unknowns

The reassessment proves feasibility of the core; it does not prove every task.

Remaining blockers/unknowns include:

1. **Missing database-driver package.**
   The original import surface references `SPFLib.dbDrivers`, but that package
   is not in this repository. Query transport needs a modern adapter.

2. **Windows executable wrapper.**
   The historical top-level entry uses `win32console`. Linux should use a new
   thin worker entry point.

3. **Service detection / .NET.**
   `gIsSvc` and related paths reference `System.*`. They should not be
   touched by normal Linux worker execution unless a task actually requires
   equivalent service semantics.

4. **Windows user/domain APIs.**
   `gUN` and `gUDomain` directly use Win32 APIs.

5. **DOS/helper executables.**
   `COMSPEC`, BAT files, `robocopy.exe`, and old helper executables need
   portable transport replacements where the feature remains required.

6. **COM/Outlook/Excel-specific paths.**
   These should use already available portable libraries/integrations behind
   the original command contract.

7. **Hard-coded historical infrastructure paths.**
   Any path still required in current production needs configuration/injection;
   obsolete paths can remain unreachable.

8. **Full task coverage.**
   SITE-LOOP, RUN-LOOP, normal query variants, report tasks, and the complete
   utility map require Linux compatibility/parity cases before a cutover
   decision.

9. **Operational process model.**
   The production parent/worker protocol, cancellation, timeout, stdout/log
   capture, temp workspace lifecycle, resource limits, and result envelope have
   not yet been implemented.

10. **Performance.**
    Correctness and isolation were tested. A representative production-scale
    startup/throughput benchmark has not yet been performed, so no performance
    claim is made.

## Validation evidence

Latest completed Linux validation before this report:

- GitHub Actions run: `36261787639`;
- environment: Ubuntu runner, Python 3.12;
- result: **61 passed, 24 warnings**;
- Ruff: **all checks passed**;
- existing `vg2c_new` tests remain in the same validation job.

The warnings are legacy deprecation warnings, principally old logging and
datetime APIs. They did not prevent execution.

The test set now covers:

- import of the original runtime on Linux;
- original WRITE-FILE execution;
- original macro / IF-ELSE / FOR-LOOP workflow;
- repeated sequential local runs;
- explicit incomplete `SPFGlobals` cache reset behavior;
- deterministic threaded shared-state contamination;
- concurrent fresh-process isolation;
- original-vs-`vg2c_new` representative output parity;
- an explicit historical loop semantic difference.

## Session 2.5B status

The existing branch:

`direct-runtime/session-2.5b-html-report-runtime`

is preserved and was not modified by this reassessment. Its observed head was:

`30befea25faead8ef891e6d59a0ab9c0bf4dd107`

No report cutover should be performed from this session.

If the portable-original-core direction continues, the report work should be
re-evaluated as a transport/component inside the original task runtime. The
standalone report modules remain strong direct-reuse candidates, but their
integration should not establish a second parallel runtime authority.

## Recommended next implementation plan

The next session should still avoid a production cutover.

### 1. Add a real portable one-job worker boundary

Create a small Linux entry module that:

- accepts one job payload;
- creates an isolated working directory;
- configures only the minimum environment/arguments required by ScriptHost;
- invokes `SPFManager.Run_SPFSQL()`;
- serializes a stable result/error envelope;
- exits after one job.

Do not call the historical Windows top-level wrapper.

### 2. Keep process isolation as an explicit contract

The parent service may run multiple workers concurrently, but each job gets a
fresh process.

Do not use thread-per-job execution inside one ScriptHost interpreter.

Do not reuse a process for a second unrelated job until/unless every relevant
class/process cache has a tested reset contract.

### 3. Introduce transport seams only where execution reaches a blocker

For each currently needed production task:

```text
original task/parser semantics
    -> run on Linux as-is
    -> if blocked, identify exact platform transport
    -> replace/inject that transport only
    -> keep original task contract
```

Prefer reusing proven `vg2c_new` filesystem/Excel/process adapters behind the
original task where they already implement the correct Linux transport.

For database query tasks, use the approved modern backend rather than
reconstructing the missing `dbDrivers` package.

### 4. Build a real-script compatibility corpus

Run representative current VG2 scripts through:

- original portable worker;
- current `vg2c_new`.

Compare:

- files;
- query/result datasets;
- macros;
- report artifacts;
- exit/error behavior;
- control flow;
- observable variables used by later tasks.

Record differences explicitly as:

- original behavior to preserve;
- obsolete Windows transport to replace;
- intentional product-level retirement;
- actual bug.

### 5. Expand the original-runtime Linux matrix

Prioritize:

1. SITE-LOOP;
2. RUN-LOOP;
3. SQLite/local query path;
4. current production database query path through a modern transport adapter;
5. file copy/append/smart-append;
6. report runtime;
7. Python/process utilities;
8. Excel;
9. email;
10. remaining used resolver entries.

The goal is not to make every historical function portable. The goal is to
prove every currently required production path.

### 6. Keep `vg2c_new` intact during the migration

Do not delete it.

Use it as:

- a regression oracle where parity is intended;
- a source of portable transport implementations;
- a fallback if a specific original task proves impractical to isolate;
- evidence for which historical features were intentionally retired.

Only after production workload parity and operational worker testing should a
separate cutover decision be made.

## Final assessment

The reassessment changes the architectural premise established after Session
2.5A.

It is no longer accurate to assume that the giant ScriptHost task/runtime stack
must be replaced because it uses `SPFGlobals`, mutable tasks, or historical
Windows imports.

The actual Linux experiment shows:

- the original core can import with very small portability amendments;
- the original parser/controller/task path can execute representative VG2;
- original and `vg2c_new` match on the tested modern portable slice;
- the original retains at least one historical semantic branch that
  `vg2c_new` intentionally rejects;
- shared process state makes same-interpreter concurrency unsafe;
- fresh process isolation contains that state successfully;
- the remaining hard problems are concentrated at platform/integration
  boundaries, especially the absent database-driver stack and Windows-specific
  transports.

Therefore the next implementation should treat the original ScriptHost runtime
as the leading semantic-core candidate, isolated one job per process, with
selective modern Linux transports. `vg2c_new` should remain intact until a
later evidence-based cutover decision.
