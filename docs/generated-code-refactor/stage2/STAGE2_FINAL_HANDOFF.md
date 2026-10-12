# Stage 2 continuation handoff — verified scope and open acceptance gates

**Status: PARTIALLY COMPLETE. Do not mark Stage 2 fully accepted.**

## Exact commit boundaries

- Approved Stage 1 baseline: b8fc52ff41569129959a37487f5417b0fd339527.
- Independently verified Stage 2 checkpoint supplied by user: b89b7e4ff5caf28bdeaddb6008c524d3862a7efc.
- This continuation followed test-strengthening commits bcc5622d7cd8b55fff7a05d6766b935be2128db5 and aeeb6d6c6860dc7f405b3c79a13393af43842439. Before continuation writes, the remote already advanced concurrently to f8d1414eb24feedc6503ee69c30206257296b4cb. It was not overwritten.
- Last runtime implementation change: f605423d0e3bd1365667f13e76b89c525f2b9152 (Windows-relative CSV-list path fix); a subsequent targeted regression commit 35350139f188b371917043c4bdf996f094bf0217 checks rejection of absolute Windows/UNC paths. The final branch HEAD must be verified after the handoff commit, which cannot embed its own hash. The final branch HEAD after committing the handoff must be reported separately because a commit cannot embed its own hash.

## Source-grounded and actually executed fixture evidence

The fixture-by-fixture matrix covering 19 test scripts and two root scripts is in [FIXTURE_STATUS_LEDGER.md](FIXTURE_STATUS_LEDGER.md). Complete runtime evidence: original offline SQLite script_short.txt with synthetic ww_yield.csv, SQL asset edit, error path and relocated Linux-wheel execution.

New in this continuation: one source-preserved **two-step** downstream SQLite sequence from hamizah.txt using original blocks 3 and 4. Deterministic upstream TSV files replace unavailable MARS results; original INNER JOIN writes input.csv and the next original query reads that result into yeuchuan_SQL_5547.tab. The tests assert exact relation headers and rows, source-derived tab output, error propagation, and post-generation SQL editing followed by a changed rerun.

The previous nine isolated partial blocks remain verified (one Vietnam WRITE-FILE, five CSS/HTML-RUN, one sql_script LEFT JOIN, two reflow RIGHT JOIN). Hamizah adds two more distinct local SQLite blocks for eleven source-preserved local partial blocks, plus a separate four-block original chain where two MARS readers are faked and the same two SQLite blocks are executed for real. Thus thirteen distinct original partial blocks have evidence, alongside one complete offline original fixture. The two mocked MARS blocks do not execute Oracle SQL, and no full Hamizah run or ScriptHost engine parity is claimed.

[VERIFIED_FIXTURE_WALKTHROUGH.md](VERIFIED_FIXTURE_WALKTHROUGH.md) reproduces evidence directly from [CI 38182724183](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38182724183): original VG2, actually generated main.py and two SQL assets, controlled TSV inputs, and first/downstream outputs.

## Semantic authorities and explicit limits

- Original ScriptHost SPFLib/SPFUtilities/utils.py::GetFileDLM determines tab, pipe, plus and CSV delimiters; original memtable.py uses it for SQLite import. Red-first tests and changes were completed earlier in Stage 2. The continuation's sql_script.txt fixture test now rejects comma-delimited .tab output rather than auto-detecting.
- Original ScriptHost SPFLib/SPFUtilities/utils.py::SPFCopy supports source-derived ordinary file-copy semantics. Earlier red-first testing corrected directory-copy opt-in. Missing external Windows batch implementations are **not** verified.
- Original WRITE-FILE body: the test now compares all translated output text against the original body after the parser's one-newline framing trim on each edge, including its retained leading blank line. That trim is **observed vg2c parser behavior**; no unverified original-engine byte-parity assertion is made.
- An unrecognized filesystem utility no longer emits a misleading pass (commit f8d1414e). Unsupported/error paths are explicit and cannot be called ScriptHost engine-verified.
- Original-engine execution, real Oracle/MARS/ARIES/OASYS, corporate files, Windows batch/VA/JMP/JSL, SMTP and any production credentials remain unavailable. SQL_Get_CSV_List has a four-block original-source fixture exercise with mocked external reader and real downstream SQLite; broader list variants, advanced macro scope/conditionals, full pivot FIRST/LAST/CTARRAY and deferred HTML parity still need source-preserved coverage.

## Symbol-level P0/P5 inventory and ownership

scripts/stage2_symbol_audit.py runs in the existing Session 03 CI workflow. It uses Python's AST to enumerate all source modules and their top-level definitions, methods and class attributes; observes from-import and possible AST name/attribute consumers, reflective usage and direct forbidden runtime dependencies; and lists compiler UtilitySpec/EmitterUtility registrants. CI reported **108 Python modules (89 vg2c, 19 vg2c_ui), 1,637 definitions, 17 static registrants including the abstract base, 12 modules with reflective patterns, and zero direct forbidden runtime-to-editor/compiler imports**.

[ARCHITECTURE_OWNERSHIP_REVIEW.md](ARCHITECTURE_OWNERSHIP_REVIEW.md) contains the source-backed keep/remove matrix for emitter/models.py, emitter/project.py, utility_metadata.py, utilities/_base.py, _emit_helpers.py, editing.py, workflow.py, semantics.py, reorder.py, dataflow/ and sql_editor/. Compiler metadata and dynamic registration have live translation/diagnostic consumers; generated JobRuntime does not import the editor layer. An internally called helper with no **external** references is not dead. The audit is a broad, reproducible symbol inventory, not a type-resolved proof of every dynamic caller, user-distributed API or field. No unproven mass deletion was made.

Cleanups completed across Stage 2: removed unreachable generic UNKNOWN utility, eliminated misleading no-op malformed mail/filesystem fallbacks, corrected copy/delimiter and relative Windows CSV-list input semantics, and strengthened tests. This continuation additionally deleted redundant fallback logic in the test's SQL-asset identification. There are no new compatibility shims, duplicate execution stacks, runtime metadata decorators, registries, manifests or optional runtime metadata dependencies.

## P6 verification and known red suite

At [CI 38183396305](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38183396305) (guarded test commit 35350139f188b371917043c4bdf996f094bf0217), **439 runtime/emitter tests passed (one warning); 617 full-suite tests passed and the same six inherited UI/JMP failures persisted**. [Installed Linux-wheel CI 38183396317](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38183396317) passed. Red CI 38183108328 reproduced Windows-relative CSV-list path failure in the first-four-block fixture; the targeted runtime fix resolved it. The two guard cases verify Windows drive/UNC paths raise ValueError, while supported relative paths work. Compilation, configured Ruff F821/F823 and the source-preserved fixture walkthrough passed. The latest documentation-only HEAD triggers fresh checks before final sign-off.

The six preexisting failures are:
1. UI shared-global edits not persisting across steps.
2. UI reorder/persist execution index failure.
3. UI HTML preview referencing absent HtmlReport.render_layout.
4. UI HTML preview attempting unsupported JMP/JSL input.
5. UI uploaded CSV column choices returned empty.
6. UI SQL file-list filters returned empty.

Do not silence, deselect or delete these tests to claim success. Resolve them against an explicitly approved editor scope, or record them as sign-off exceptions. Further dynamic-field consumer proof and remaining feasible original fixture families also remain P0/P2/P3 gates.

**No branch merge, force-push, external service invocation or runtime metadata implementation occurred.**


## Four-block original fixture extension and remaining P0 gate

- The first four original hamizah.txt source blocks translate unchanged. A temporary fake DataSyncX MarsReader supplies deterministic output DataFrames for the first two blocks. The second block expands SQL_Get_CSV_List using the first block's actual TSV output; the next two original SQLite blocks execute locally. Assertions verify output headers, rows, TSV delimiters and SQL substitution. Neither Oracle SQL nor the original ScriptHost engine runs.
- The original source uses Windows-style relative CSV-list input paths. On Linux this failed with FileNotFoundError despite the correct prior output (red CI 38183108328). The minimal CSV-list reader correction normalizes only relative Windows separators, rejects UNC/drive-absolute inputs and leaves the rest of the file APIs unchanged.
- The AST inventory is broad but not an exhaustive type-resolved callgraph: runtime reflection, public exports and user code require manual verification before any code can be deleted solely for lack of static mentions. This P0/P5 gate remains open. Six old UI/JMP tests remain red and were not removed or deselected.

## Follow-up fixture/consumer audit checkpoint (12 October 2026)

A subsequent review discovered that the original Vietnam WRITE-FILE test accidentally contained literal backslash-n sequences inside a Python comment, preventing its claimed payload assertions from executing. Commit `3bc1bebfa4070b19063998b63857a2127302e0c7` replaced that disabled block with executable **exact UTF-8 byte** equality, including the source's retained leading blank line and parser-framing trim. Core and end-to-end CI passed afterward (439 core passing; 617 full-suite passing, six inherited UI/JMP failures). No runtime behavior changed.

Commit `5fe15ddfbeef3631e32beaabcdd3698f9bb8edf8` added a source-preserved HTML-RUN CSS asset edit/rerun check for `actual_script.txt`. CI [38188026303](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38188026303) confirms 440 runtime/emitter passing, 618 full-suite passing, the **same six inherited UI failures**, and the style-edit test; the corresponding [Linux wheel run 38188026263](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38188026263) passed. This extends behavior evidence, not the distinct fixture-block count.

The AST consumer audit now recognizes explicit exports, local references and resolved relative from-imports; this improves the cautionary static inventory but is **not** a complete dynamic type-resolved proof and must not authorize deletion alone. No extra core symbols met the deletion gate; supported metadata and editor boundaries were deliberately preserved. Stage 2 remains **partially complete**, pending remaining feasible source-preserved semantic cases and explicit UI/JMP acceptance decisions. The final HEAD and newest CI must be verified independently after documentation is committed.

## Generated main.py cleanup continuation (12 October 2026)

**Starting remote SHA for this additional cleanup:** `441e6e0f1b99cb7877c9ac6eb4593eb99d1536e1`. Subsequent commits on the same `refactor/generated-code/implementation` branch remove ordinary generated entrypoint boilerplate and redundant main.py SQL header arguments. Verify the final remote HEAD after the documentation commit rather than embedding the SHA of a commit in its own contents.

Current normal generated execution is:
```python
from vg2c.runtime import SqliteReader, job_entrypoint

@job_entrypoint(__file__)
def run(job):
    job.sql("sql/query_000_input.sql", reader=SqliteReader(),
            output="input.csv", inputs=["yeuchuan_a0_5547.tab", "yeuchuan_a1_5547.tab"])

if __name__ == "__main__":
    run()
```
The decorator's backend wrapper preserves `run()` default output, `run(workdir=...)` overrides and project relocation. Embedded Python projects receive `workdir` only when appropriate; AED bootstrap/config values are initialized by the backend without exposing `bootstrap_aed` or `JobRuntime` setup in main.py.

Original `/HEADERS` can be more than an SQL SELECT alias: source-derived, name-based selection and output casing must be retained. Normal SQL output uses SQL result names without an extra header setting; explicit legacy overrides are now represented in the same editable SQL asset as a `-- vg2c-output-headers: ["..."]` first-line directive. The runtime validates it and does not pass the comment to the SQL engine. Removing the directive makes the output follow current SQL result column names. No `.header.json` is generated. The compiler-only `_table_option` helper was deleted; legacy public `JobRuntime.table_spec` is retained for older already-generated projects.

**Red-first evidence:** [38202171001](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38202171001) had five intentionally failing new tests before the implementation. **Current tested implementation checkpoint:** [38203059174](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38203059174): 445 runtime/emitter passed, 623 full-suite passed, the same six inherited UI/JMP failures; Python compilation, configured Ruff lint, static dependency audit and original multistep fixture walkthrough passed. [38203059146](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38203059146): installed Linux wheel passed. New tests cover default/override workdirs, relocated project, exact SQL output, SQL-asset edit/re-execution, malformed header directive rejection, embedded Python workdir access and AED config injection.

The preceding Stage 2 fixture statuses remain accurate. This change **does not** establish actual ScriptHost engine parity, real Oracle/SMTP/Windows integration, nor close the six existing UI failures. Do not promote the overall Stage 2 acceptance status to fully complete solely because generated-code cleanup tests are green. No merge or force-push occurred.

## Additional continuation: verified code reduction and consumer-gated rollback

**Starting checkpoint for this iteration:** `12de79f12ba823ad022c9c801d96311bdc445108`. The default SQLite emission no longer includes `inputs=[]`; it relies on the existing `JobRuntime.sql(inputs=None)` and `runtime.query.run_query(inputs or [])` defaults. The focused test was committed red-first in `74f40fb6691c0c635f2c755733ef1b585c29fdf7` and failed only that new assertion in [CI 38203567137](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38203567137). Commit `42a4a9a669c276a18c142a7a34f5c7b3360afced` removed the redundant argument, leaving true `/TABLE` inputs intact.

An attempted removal of `JobRuntime.table_spec`/`report_spec` (believed to be leftover JSON readers) was **reverted** when four core regression tests exposed real consumers, including legacy JSON configuration rereading and validation. See [red CI 38203667770](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38203667770) and restore commit `37ae674a27da7566edc02f2ec0b5535880197801`. No deletion of the tests, no compatibility fallback and no parallel JSON engine were introduced; the existing methods simply remain part of the supported runtime API.

**Post-restoration verification:** [CI 38203759540](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38203759540) reports **445 runtime/emitter passed**, **623 full-suite passed and the unchanged six inherited UI/JMP failures**. Python compilation, configured Ruff checks, source-preserved multistep execution and consumer audit passed. [Installed Linux wheel 38203759573](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38203759573) passed. These are verified checks of the implementation, not original ScriptHost engine parity.

The full symbol-level inventory remains conservative: static zero-use is insufficient to delete externally callable, dynamically registered or regression-tested methods. This iteration deliberately made no additional speculative change to `SqliteEngine._emit_sql`, compiler metadata, source diagnostics or editor layers. Final HEAD must be recorded from remote after the documentation commits, and the six UI/JMP gates remain open.
