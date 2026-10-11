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

At [CI 38183242029](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38183242029) (runtime implementation f605423d0e3bd1365667f13e76b89c525f2b9152), **437 runtime/emitter tests passed (one warning); 615 full-suite tests passed and the exact same six inherited UI/JMP failures persisted**. [Installed Linux-wheel CI 38183242073](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38183242073) passed. Red CI 38183108328 reproduced Windows-relative CSV-list path failure in the first-four-block fixture, resolved by the narrow runtime correction. Compilation, configured Ruff F821/F823 and live source-preserved fixture walkthrough passed. Check the final SHA's additional absolute-path regression after doc updates.

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
