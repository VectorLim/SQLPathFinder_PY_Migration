# Stage 2 continuation handoff — verified scope and open acceptance gates

**Status: PARTIALLY COMPLETE. Do not mark Stage 2 fully accepted.**

## Exact commit boundaries

- Approved Stage 1 baseline: b8fc52ff41569129959a37487f5417b0fd339527.
- Independently verified Stage 2 checkpoint supplied by user: b89b7e4ff5caf28bdeaddb6008c524d3862a7efc.
- This continuation followed test-strengthening commits bcc5622d7cd8b55fff7a05d6766b935be2128db5 and aeeb6d6c6860dc7f405b3c79a13393af43842439. Before continuation writes, the remote already advanced concurrently to f8d1414eb24feedc6503ee69c30206257296b4cb. It was not overwritten.
- Last source/test change in this continuation: a3eb3fa8b02b21909022f2d7283094b1d2866881 (simplified a redundant SQL-asset test lookup). This is the frozen code SHA for this handoff; subsequent commits are documentation-only unless stated otherwise. The final branch HEAD after committing the handoff must be reported separately because a commit cannot embed its own hash.

## Source-grounded and actually executed fixture evidence

The fixture-by-fixture matrix covering 19 test scripts and two root scripts is in [FIXTURE_STATUS_LEDGER.md](FIXTURE_STATUS_LEDGER.md). Complete runtime evidence: original offline SQLite script_short.txt with synthetic ww_yield.csv, SQL asset edit, error path and relocated Linux-wheel execution.

New in this continuation: one source-preserved **two-step** downstream SQLite sequence from hamizah.txt using original blocks 3 and 4. Deterministic upstream TSV files replace unavailable MARS results; original INNER JOIN writes input.csv and the next original query reads that result into yeuchuan_SQL_5547.tab. The tests assert exact relation headers and rows, source-derived tab output, error propagation, and post-generation SQL editing followed by a changed rerun.

The previous nine isolated partial blocks remain verified (one Vietnam WRITE-FILE, five CSS/HTML-RUN, one sql_script LEFT JOIN, two reflow RIGHT JOIN). The additional hamizah two-block chain yields **11 source-preserved partial blocks over 10 partial execution scenarios**, plus **one complete offline original fixture**. This is not original ScriptHost engine parity, and replacing external upstream tables with synthetic data does not make the full source script offline.

[VERIFIED_FIXTURE_WALKTHROUGH.md](VERIFIED_FIXTURE_WALKTHROUGH.md) reproduces evidence directly from [CI 38182724183](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38182724183): original VG2, actually generated main.py and two SQL assets, controlled TSV inputs, and first/downstream outputs.

## Semantic authorities and explicit limits

- Original ScriptHost SPFLib/SPFUtilities/utils.py::GetFileDLM determines tab, pipe, plus and CSV delimiters; original memtable.py uses it for SQLite import. Red-first tests and changes were completed earlier in Stage 2. The continuation's sql_script.txt fixture test now rejects comma-delimited .tab output rather than auto-detecting.
- Original ScriptHost SPFLib/SPFUtilities/utils.py::SPFCopy supports source-derived ordinary file-copy semantics. Earlier red-first testing corrected directory-copy opt-in. Missing external Windows batch implementations are **not** verified.
- Original WRITE-FILE body: the test now compares all translated output text against the original body after the parser's one-newline framing trim on each edge, including its retained leading blank line. That trim is **observed vg2c parser behavior**; no unverified original-engine byte-parity assertion is made.
- An unrecognized filesystem utility no longer emits a misleading pass (commit f8d1414e). Unsupported/error paths are explicit and cannot be called ScriptHost engine-verified.
- Original-engine execution, real Oracle/MARS/ARIES/OASYS, corporate files, Windows batch/VA/JMP/JSL, SMTP and any production credentials remain unavailable. SQL_Get_CSV_List, advanced macro scope/conditionals, full pivot FIRST/LAST/CTARRAY and deferred HTML parity require more source-preserved coverage where safe.

## Symbol-level P0/P5 inventory and ownership

scripts/stage2_symbol_audit.py runs in the existing Session 03 CI workflow. It uses Python's AST to enumerate all source modules and their top-level definitions, methods and class attributes; observes from-import and possible AST name/attribute consumers, reflective usage and direct forbidden runtime dependencies; and lists compiler UtilitySpec/EmitterUtility registrants. CI reported **108 Python modules (89 vg2c, 19 vg2c_ui), 1,637 definitions, 17 static registrants including the abstract base, 12 modules with reflective patterns, and zero direct forbidden runtime-to-editor/compiler imports**.

[ARCHITECTURE_OWNERSHIP_REVIEW.md](ARCHITECTURE_OWNERSHIP_REVIEW.md) contains the source-backed keep/remove matrix for emitter/models.py, emitter/project.py, utility_metadata.py, utilities/_base.py, _emit_helpers.py, editing.py, workflow.py, semantics.py, reorder.py, dataflow/ and sql_editor/. Compiler metadata and dynamic registration have live translation/diagnostic consumers; generated JobRuntime does not import the editor layer. An internally called helper with no **external** references is not dead. The audit is a broad, reproducible symbol inventory, not a type-resolved proof of every dynamic caller, user-distributed API or field. No unproven mass deletion was made.

Cleanups completed across Stage 2: removed the unreachable generic UNKNOWN utility, eliminated misleading no-op malformed mail/filesystem fallbacks, corrected copy/delimiter behavior, and strengthened tests. This continuation additionally deleted redundant fallback logic in the test's SQL-asset identification. There are no new compatibility shims, duplicate execution stacks, runtime metadata decorators, registries, manifests or optional runtime metadata dependencies.

## P6 verification and known red suite

At [CI 38182606214](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38182606214), **435 runtime/emitter tests passed; 613 full-suite tests passed and six inherited UI/JMP cases failed**. The installed Linux wheel was green in [38182606154](https://github.com/VectorLim/SQLPathFinder_PY_Migration/actions/runs/38182606154). Later workflow runs with the AST registration inventory and printed fixture walkthrough also showed 435 runtime/emitter passes, 613 full-suite passes and the same six failures. Compileall and configured Ruff F821/F823 checks passed on the workflow code SHA. Verify the final documentation SHA's workflows independently if CI reruns.

The six preexisting failures are:
1. UI shared-global edits not persisting across steps.
2. UI reorder/persist execution index failure.
3. UI HTML preview referencing absent HtmlReport.render_layout.
4. UI HTML preview attempting unsupported JMP/JSL input.
5. UI uploaded CSV column choices returned empty.
6. UI SQL file-list filters returned empty.

Do not silence, deselect or delete these tests to claim success. Resolve them against an explicitly approved editor scope, or record them as sign-off exceptions. Further dynamic-field consumer proof and remaining feasible original fixture families also remain P0/P2/P3 gates.

**No branch merge, force-push, external service invocation or runtime metadata implementation occurred.**
