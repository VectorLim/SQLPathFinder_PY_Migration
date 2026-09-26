from __future__ import annotations

from pathlib import Path
import ast
import re

ROOT = Path("scripthost-utilities-decompiled/SPSQL3_py")
TARGETS = {
    "SPFLib/SPFSQL3.py": [
        "WriteFileTask", "SPFDeleteTask", "SPFCopyTask", "SPFRenameTask",
        "AppendFileTask", "RoboCopyTask", "SetFileROTask", "WaitIntervalTask",
        "WaitFileTask", "SPFZipTask", "SPFUNZipTask", "LoadExcelTask",
        "ImportExcelTask", "GetWebTextTask", "RunPythonTask", "PyScriptTask",
        "OracleQueryTask", "GetSiteTimeTask", "EmailTask", "SmartAppendTask",
    ],
    "SPFLib/SPFUtilities/utils.py": [
        "SPFDistribute", "GetFilePattern", "SPFAppendFile", "SPFRoboCopy",
        "CompareVars", "LoadExcel2", "SPFWebCopyPyReqs", "Run_Python",
        "SPFEmail", "SmartAppend", "SmartAppend4", "SPFDelete", "SPFRenameFile",
        "ZipFiles2",
    ],
    "SPFLib/SPFUtilities/memtable.py": [
        "LoadFromFile", "Run_SQLite", "Create_SQL_In_Like_List", "getStandaloneCon",
    ],
}

for rel, names in TARGETS.items():
    path = ROOT / rel
    text = path.read_text(encoding="utf-8-sig", errors="replace")
    lines = text.splitlines()
    print(f"\n### {rel} ({len(lines)} lines)")
    for name in names:
        pattern = re.compile(rf"^\s*(?:class|def)\s+{re.escape(name)}\b")
        hits = [i for i, line in enumerate(lines, 1) if pattern.search(line)]
        if hits:
            print(name, hits)
            for hit in hits:
                lo=max(1, hit-3); hi=min(len(lines), hit+12)
                print(f"-- {lo}:{hi} --")
                for n in range(lo, hi+1):
                    print(f"{n:06d}: {lines[n-1]}")

print("\n### IMPORTS")
for rel in [
    "SPFLib/__init__.py", "SPFLib/SPFGlobals.py", "SPFLib/SPFUtilities/utils.py",
    "SPFLib/SPFUtilities/memtable.py", "SPFLib/SPFUtilities/spflogger.py",
    "SPFLib/SPFUtilities/sh.py", "AutoComm_HTML_Report.py", "PyGraphingMethods.py",
    "PyPlot_Class.py", "PyUtils.py",
]:
    path=ROOT/rel
    try:
        tree=ast.parse(path.read_text(encoding="utf-8-sig", errors="replace"))
    except SyntaxError as exc:
        print(rel, "SYNTAX_ERROR", exc)
        continue
    imports=[]
    for node in tree.body:
        if isinstance(node, ast.Import):
            imports.extend(alias.name for alias in node.names)
        elif isinstance(node, ast.ImportFrom):
            imports.append(("."*node.level)+(node.module or ""))
    print(rel, sorted(set(imports)))
