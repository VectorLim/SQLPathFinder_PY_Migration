# Portable ScriptHost architecture reassessment

## Session authority and scope

This assessment follows `PROMPT_ARCHITECTURE_REASSESSMENT.md` from the
architecture-reassessment execution pack.

Exact starting point:

- branch: `direct-runtime/session-2.5a-portability-reuse-audit`
- commit: `9cb2ed8800073a21102e2d1c03441deaf86fb545`

Assessment branch:

- `direct-runtime/architecture-reassessment-portable-scripthost-v4`
- validated implementation/test head used by the final report refresh:
  `9a243d53c3ec3d7aa8c1a455d28088d259543f40`

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
- `SITE-LOOP`;
- `RUN-LOOP`;
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

## Existing real-script fixture result

The repository's existing `tests/fixtures/actual_script.txt` (about 38 KB) was
also used as migration evidence rather than relying only on synthetic scripts.

The original ScriptHost `Process_Query/GetQuery` path parses the fixture on
Linux and resolves its opening report lifecycle as:

```text
HTMLRunTask
HTMLLayoutTask
HTMLDeleteTask
```

Later in that same current fixture, `/UTILITIES=getcsrsu.bat` is not in the
explicit utility map. The original mature resolver falls back to
`DOSCmdTask`, so it still accepts the script structurally.

`vg2c_new` intentionally disables generic shell fallback and raises a
source-located `Vg2ParseError` for `getcsrsu.bat`.

This is a useful **real migration gap**, not a reason to restore arbitrary shell
execution. Before cutover, that currently used helper must be classified as one
of:

- a behavior still required, with a specific portable adapter;
- an obsolete step that can be removed from the current script; or
- an explicitly unsupported product behavior with an agreed migration path.

The test records this difference directly instead of weakening either resolver
to manufacture parity.

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

| Authority category | Examples | Assessment |
|---|---|---|
| **CONSTANT / CONFIG** | format constants, delimiter/name constants, fixed defaults, current environment paths | Keep constants; inject/configure values that legitimately vary by Linux deployment |
| **PER-RUN MUTABLE** | `gCommandLineArguments`, `g_gVars`, loop counters, `gMyAbort`, macro/SQL-file state, `gMyLocal`, `gMyEXEDir`, report/chart counters | Valid legacy job state when the entire job owns one process; process exit is the reset boundary |
| **PER-TASK STATE THAT SHOULD NOT BE GLOBAL** | helper/report/query scratch state that is class-scoped only for historical convenience | Do not rewrite speculatively; refactor only where representative intra-job tests show concrete cross-task interference, because process isolation does not fix interference inside one job |
| **PROCESS-GLOBAL INTEGRATION STATE** | `gDBGlobCon`, `gSPFInstance`, service/session identity caches, process/random identifiers and other integration caches | Do not rely on command-argument reset; contain with one fresh process per job and let modern adapters own external connection lifetimes |
| **WINDOWS-SPECIFIC STATE** | `gUN`, `gUDomain`, `gUserPrincipal`, `gIsSvc`, CLR/Win32/COM access, Windows-only paths | Keep lazy/unreached when irrelevant; provide a narrow portable replacement only for a required feature |
| **OBSOLETE STATE** | old SQLPFaaS/service infrastructure, historical shared locations, retired wrappers and host-only flags no longer required by the Linux product | Leave unreachable or retire only after current-script/product evidence confirms the behavior is not needed |

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

### Process startup/one-job overhead

A small CI feasibility probe launches three **fresh sequential Python
processes**. Each imports the original ScriptHost runtime and executes one
original `WRITE-FILE` job.

Across the final Ubuntu GitHub Actions runs, the three-job probe took
**1.54-2.44 s total**, or roughly **0.51-0.81 s/job**, including interpreter
startup, ScriptHost imports, runtime construction, parsing, execution, and
process shutdown. The final resolver-gap validation run measured **2.02 s total**
for the three jobs.

This is not a production throughput benchmark and no service SLA is inferred
from it. It is enough to reject the concern that fresh-process containment has
obviously prohibitive multi-second-to-tens-of-seconds startup cost for this
runtime. Production-scale scripts still require workload benchmarking.

## ScriptHost files changed by this prototype

Only four decompiled ScriptHost source files were changed. The changes are
portability guards/boundaries rather than a replacement runtime.

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

### `SPFLib/SPFGlobals.py`

Change:

- add explicit platform guards to the three Win32 identity accessors
  (`gUserPrincipal`, `gUN`, and `gUDomain`) so Linux import remains valid
  and the unsupported Windows identity integration fails clearly only if that
  property is invoked.

Diff:

- +6 / -0 lines.

No `SPFGlobals` state model was redesigned. The process-isolation result still
uses the original class/process-level global model.

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
| SITE-LOOP | **KEEP** | Original task executes on Linux and performs site substitution |
| RUN-LOOP | **KEEP** | Original pandas/chunking path executes on Linux and produces the expected final chunk |
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
| cleanup semantics | **KEEP / MINIMAL PORTABILITY AMENDMENT** | `Run_SPFSQL` calls `Final_CleanUp`; its old `%COMSPEC%` deletion transport should become portable filesystem deletion when that branch is needed; process exit remains final state containment |

## Parser/runtime comparison across the requested surface

The following comparison separates tested parity from code-grounded differences.

| Surface | Original ScriptHost | `vg2c_new` | Assessment |
|---|---|---|---|
| Query splitting | `Run_SPFSQL` splits the original SQL file delimiter and feeds `Process_Query` | `_split_segments` reimplements the delimiter scan while adding source line ranges | Same concept; new parser adds diagnostics, original remains semantic authority |
| OPTIONS parsing | `SPFTaskBase.parseTaskOptions` populates mutable task option state | `_parse_segment/_parse_options` emits immutable options with `SourceSpan` | New implementation is cleaner; still duplicate semantics |
| GetQuery precedence/routing | Mature `TaskHandlerMapDict` plus report/query selection in `GetQuery` | Manifest + resolver reproduce utility/control/report routing | New runtime explicitly says it is an amended port; keeping both creates two routing authorities |
| Utility arguments | Original `MyUtilities` tokenization uses `csv.reader` with space delimiter/quotes | `parse_utility_arguments` is a direct port with immutable tuple/source errors | Strong parity by construction |
| IF/ELSE | Original mutable controller tasks and `CompareVars` side effects | New interpreter reimplements conditions with runtime frames/pure comparison | Representative branch parity proven |
| Macro substitution | Original `StartMacroTask` + `MemTable` + deep-copied child task substitution | New interpreter uses scoped `RuntimeState` frames | Representative single-row parity proven; broad macro corpus still needed |
| FOR-LOOP V2 | Original mutable child copies and loop token substitution | New interpreter recreates V2 with frames | Representative V2 parity proven |
| Historical FOR-LOOP | Original retains Version-1 branch | New runtime explicitly rejects historical version selection | Proven semantic difference |
| SITE-LOOP | Original substitutes `spf-site` / filename token and stops after first failing site despite its console text | New amended port intentionally mirrors the actual `break` behavior using a frame | Original task now Linux-proven; cross-runtime full error-path parity remains to test |
| RUN-LOOP | Original pandas chunk reader, temp copy, global loop/abort bookkeeping | New runtime rewrites with stdlib CSV and drops global bookkeeping | Original task now Linux-proven; the two are intentionally different implementations |
| HPC | Original `BeginHPCTask` retains historical local/async/remote transport logic | New runtime flattens BEGIN/END-HPC to child execution | Known semantic/transport flattening; product requirement decides what transport survives |
| Error/continue behavior | `gMyAbort`, `ContinueOnError`, `SPFNothingToProcessException`, loop-specific cleanup/continuation | Exceptions are normalized into source-located runtime errors with per-utility handling | Cleaner new model, but not full behavioral parity yet |
| Report lifecycle | Original routes and owns HTML run/defer/layout/tab/menu/plot/delete/JS tasks and shared report state | Report entries exist in resolver manifest but are current-platform gaps at this baseline; paused 2.5B is separate checkpoint work | Original report/task lifecycle is a major reuse opportunity |
| Cleanup | `Run_SPFSQL` calls `Final_CleanUp`; errors set `gMyAbort` and re-raise; no-process case is specially continued | New runtime relies on scoped state/portable utility cleanup | Original cleanup semantics can stay, but the `%COMSPEC%` file-deletion edge is a transport replacement |

This matrix is why the recommendation is not to discard `vg2c_new` as
"incorrect." It has real advantages in diagnostics and isolation. The concern
is maintaining two independent implementations of mature VG2 semantics when the
original runtime itself is now demonstrably portable for core paths.

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

### Cross-option criteria

| Criterion | A — `vg2c_new` | B — portable original wholesale | C — original semantic core + selective adapters |
|---|---|---|---|
| Duplicated semantics | High: parser/runtime/control semantics are reimplemented | Low | Low: one semantic authority, transport adapters only |
| New/runtime LOC pressure | Already substantial and grows with every gap | Small for core, but large if obsolete transports are reconstructed | Small-to-moderate and concentrated at real platform edges |
| Original files changed in this spike | N/A to A | 4 files were enough for tested core import/execution | Same 4-file core feasibility plus bounded adapters |
| Current Windows dependencies | Avoided by rewrite | Still numerous and blocks literal wholesale use | Kept off portable paths; replaced only when invoked/required |
| Test/parity confidence | Strong unit tests; incomplete resolver coverage | Core Linux evidence strong; broad production task coverage incomplete | Strongest migration path because both engines can run side-by-side during validation |
| Concurrency | Safe in-process state model | Unsafe for unrelated jobs in same interpreter | Fresh process per job contains original globals |
| Report reuse | Requires parallel report implementation/integration | Can directly use mature report task lifecycle | Can directly reuse portable report algorithms and adapt plotting/transport edges |
| DataSyncX integration | Natural because runtime is new | Awkward if forced into missing legacy `dbDrivers` shape | Narrow adapter behind original query/task contract; do not recreate missing drivers |
| Future maintenance | Cleaner modules, but semantic duplication persists | Large decompiled monolith and legacy transport burden | Legacy semantic core remains large, but modernization is localized and duplication falls |
| Migration/cutover risk | Higher semantic reconstruction risk | Higher platform/integration risk if attempted wholesale | Lowest current evidence-based risk: preserve behavior while replacing bounded edges |

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
   The existing `actual_script.txt` still contains `getcsrsu.bat`, so generic
   DOS fallback cannot be treated as universally obsolete until that real step
   is mapped to a specific Linux behavior or intentionally removed.

6. **COM/Outlook/Excel-specific paths.**
   These should use already available portable libraries/integrations behind
   the original command contract.

7. **Hard-coded historical infrastructure paths.**
   Any path still required in current production needs configuration/injection;
   obsolete paths can remain unreachable.

8. **Full task coverage.**
   SITE-LOOP and RUN-LOOP are now Linux-proven in this spike. Normal query
   variants, report tasks, and the rest of the complete utility map still
   require Linux compatibility/parity cases before a cutover decision.

9. **Operational process model.**
   The production parent/worker protocol, cancellation, timeout, stdout/log
   capture, temp workspace lifecycle, resource limits, and result envelope have
   not yet been implemented.

10. **Performance beyond startup.**
    The three-fresh-process micro-probe measured 1.54-2.44 s total across the
    final GitHub Actions runs (2.02 s in the final resolver-gap run). A
    representative production-scale query/report throughput benchmark has not
    yet been performed, so no end-to-end performance claim is made.

## Validation evidence

Latest completed Linux validation for the assessed implementation/test head:

- GitHub Actions run: `36262476605`;
- implementation/test commit: `9a243d53c3ec3d7aa8c1a455d28088d259543f40`;
- environment: Ubuntu runner, Python 3.12;
- result: **67 passed, 50 warnings**;
- fresh-process probe: **2.02 s** for three sequential one-job processes;
- real `actual_script.txt` resolver-gap test: **0.37 s**;
- original RUN-LOOP test: **0.17 s**;
- original SITE-LOOP test: **0.13 s**;
- Ruff: **all checks passed**;
- existing `vg2c_new` tests remain in the same validation job.

The warnings are legacy deprecation warnings, principally old logging and
datetime APIs. They did not prevent execution.

The test set now covers:

- import of the original runtime on Linux;
- original WRITE-FILE execution;
- original macro / IF-ELSE / FOR-LOOP workflow;
- original SITE-LOOP execution;
- original RUN-LOOP execution;
- repeated sequential local runs;
- explicit incomplete `SPFGlobals` cache reset behavior;
- deterministic threaded shared-state contamination;
- concurrent fresh-process isolation;
- original-vs-`vg2c_new` representative output parity;
- an explicit historical loop semantic difference;
- missing legacy DB transport failing only when invoked;
- fresh-process startup/one-job timing;
- Windows identity integration remaining import-safe and failing clearly only
  when invoked on Linux;
- the existing `actual_script.txt` parsing through original `Process_Query`
  while `vg2c_new` intentionally rejects its `getcsrsu.bat` DOS-fallback
  step.

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

1. the real `getcsrsu.bat` step from `actual_script.txt`: portable adapter
   or explicit removal decision;
2. SQLite/local query path;
3. current production database query path through a modern transport adapter;
4. file copy/append/smart-append;
5. report runtime;
6. Python/process utilities;
7. Excel;
8. email;
9. additional production SITE/RUN-loop scripts and error paths;
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
