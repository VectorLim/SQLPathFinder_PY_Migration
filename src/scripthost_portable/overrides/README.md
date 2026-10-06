# ScriptHost inheritance overrides

Comparison baseline: `scripthost-utilities-decompiled/SPSQL3_py.zip`.
The `original/SPSQL3_py` folder is the snapshot from the previous preservation edit. It already includes portable changes and cannot identify additions by itself.

An AST comparison found 27 modified methods/properties and one added method across 12 classes. Their implementations now live in subclasses under `src/scripthost_portable/overrides`. Legacy modules re-export each subclass after defining its original base, so existing callers and later subclasses reach the portable implementation.

Historical method bodies remain in the base classes. Previous `[Removed from current version]` comments remain beside changed code in the override files. Explicit `super` calls in the affected base classes now use `super()` to prevent recursion after re-exporting. Derived classes keep legacy class names to preserve private attributes and task routing.

Overrides use `legacy` to access the original module globals, shared state, and monkeypatchable dependencies. The SSPI import still targets the original package.

| Original module | Class | Method/property | Override file | Change |
| --- | --- | --- | --- | --- |
| `SPFLib/SPFGlobals.py` | `SPFGlobals` | `gIsSvc` | `globals.py` | Modified |
| `SPFLib/SPFGlobals.py` | `SPFGlobals` | `gLocalDir` | `globals.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `SPFRoboCopy` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `File_Lock_Move` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `setEnv` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `unzipString` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `SPFDelete` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `ConvertDLM` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `IntelWW` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `Run_R` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `SPFWebCopyPyReqs` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `SPFEmail` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `UnzipFile` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `GetFilePattern` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `SPFCopy` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/utils.py` | `Utilities` | `LoadExcel2` | `utilities.py` | Modified |
| `SPFLib/SPFUtilities/memtable.py` | `MemTable` | `Run_SQLite` | `memtable.py` | Modified |
| `SPFLib/SPFUtilities/memtable.py` | `MemTable` | `getStandaloneCon` | `memtable.py` | Modified |
| `SPFLib/SPFUtilities/memtable.py` | `MemTable` | `sqliteCharIndex_v2` | `memtable.py` | Added |
| `SPFLib/SPFSQL3.py` | `NormalQueryTaskBase` | `Prep_Inc_Process` | `normal_query.py` | Modified |
| `SPFLib/SPFSQL3.py` | `nqOracleTask` | `OpenConnection` | `oracle.py` | Modified |
| `SPFLib/SPFSQL3.py` | `GetSiteTimeTask` | `executeTaskCommand` | `site_time.py` | Modified |
| `SPFLib/SPFSQL3.py` | `UpdateTimeFileTask` | `executeTaskCommand` | `update_time.py` | Modified |
| `SPFLib/SPFSQL3.py` | `SmartAppendTask` | `performUpdateTime` | `smart_append.py` | Modified |
| `SPFLib/SPFSQL3.py` | `SetFileROTask` | `executeTaskCommand` | `readonly.py` | Modified |
| `SPFLib/SPFSQL3.py` | `XMLToCSVTask` | `executeTaskCommand` | `xml_to_csv.py` | Modified |
| `SPFLib/SPFSQL3.py` | `EchoTask` | `executeTaskCommand` | `echo.py` | Modified |
| `SPFLib/SPFSQL3.py` | `GetFilesTask` | `getFilesInfoFromFolderGlob` | `get_files.py` | Modified |

## Other additions

Two new fallback classes, `_UnavailableLegacyDBDriverBase` and `NodesInfo`, now live in `overrides/legacy_drivers.py`. Their compiled counterparts may be unavailable, so there is no importable original class to inherit from.

No added or modified standalone functions were found in the 35 archived source/asset files. Helper functions already in `file_operations.py` and `query_transport.py` remain module functions, called by the overrides. Direct imports or explicit delegation suit functions without requiring inheritance.

Import guards, package-relative imports, package initialization, and the logger install path remain at the import boundary. A subclass cannot repair an import failure that happens before its original base class is defined.

No source or asset files were deleted.

## Verification

All 28 copied method bodies match the pre-refactor snapshot in an AST comparison after reversing module qualification. The 27 restored base methods match the archived ZIP. All 147 preservation comment markers remain in the runtime sources and overrides.

The final focused run of the inheritance and query transport tests passed: 30 passed. It includes further subclass construction, private property storage, portable/legacy driver selection, and direct registration of the added SQLite character-index method.

The ScriptHost suite with SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT=1 produced 96 passed, 15 skipped, and 2 failed on Windows. Both remaining failures reproduced against a separate pre-refactor source copy with the same installed compiled drivers: native driver authorization, and native node parsing rejecting a Windows C: path in the SQLite UDF worker test.

An undefined-name check found the inherited unzipString exception fallback after its input parameter has been deleted. That existing behavior was preserved during this refactor.
