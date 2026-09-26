from __future__ import annotations

import ast
import importlib
import re
import sys
from pathlib import Path

ROOT = Path("scripthost-utilities-decompiled/SPSQL3_py")
TARGETS = {
    "SPFLib/SPFSQL3.py": [
        "WriteFileTask", "SPFDeleteTask", "SPFCopyTask", "SPFRenameTask",
        "AppendFileTask", "RoboCopyTask", "SetFileROTask", "WaitIntervalTask",
        "WaitFileTask", "SPFZipTask", "SPFUNZipTask", "LoadExcelTask",
        "ImportExcelTask", "GetWebTextTask", "RunPythonTask", "PyScriptTask",
        "OracleQueryTask", "GetSiteTimeTask", "EmailTask", "SmartAppendTask",
        "smartAppend4_file_pandas", "process_csv", "parseDelCriteria",
        "validateHDRS", "GetUnionOfCols",
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

print("### TARGET LOCATIONS")
for rel, names in TARGETS.items():
    text = (ROOT / rel).read_text(encoding="utf-8-sig", errors="replace")
    lines = text.splitlines()
    print(f"{rel}: {len(lines)} lines")
    for name in names:
        pattern = re.compile(rf"^\s*(?:class|def)\s+{re.escape(name)}\b")
        hits = [i for i, line in enumerate(lines, 1) if pattern.search(line)]
        if hits:
            print(f"  {name}: {','.join(map(str, hits))}")

print("\n### TOP-LEVEL IMPORTS")
for rel in [
    "SPFLib/__init__.py", "SPFLib/SPFGlobals.py", "SPFLib/SPFUtilities/utils.py",
    "SPFLib/SPFUtilities/memtable.py", "SPFLib/SPFUtilities/spflogger.py",
    "SPFLib/SPFUtilities/sh.py", "AutoComm_HTML_Report.py", "AutoComm_ChartData.py",
    "PyGraphingMethods.py", "PyPlot_Class.py", "PyUtils.py",
]:
    path = ROOT / rel
    tree = ast.parse(path.read_text(encoding="utf-8-sig", errors="replace"))
    imports = []
    for node in tree.body:
        if isinstance(node, ast.Import):
            imports.extend(alias.name for alias in node.names)
        elif isinstance(node, ast.ImportFrom):
            imports.append(("." * node.level) + (node.module or ""))
    print(f"{rel}: {', '.join(sorted(set(imports)))}")

print("\n### REUSE STRUCTURE")
for rel, class_name in [
    ("SPFLib/SPFSQL3.py", "SmartAppendTask"),
    ("SPFLib/SPFUtilities/utils.py", "Utilities"),
    ("SPFLib/SPFUtilities/memtable.py", "MemTable"),
]:
    tree = ast.parse((ROOT / rel).read_text(encoding="utf-8-sig", errors="replace"))
    node = next(item for item in tree.body if isinstance(item, ast.ClassDef) and item.name == class_name)
    bases = [ast.unparse(base) for base in node.bases]
    class_state = [
        target.id
        for item in node.body
        if isinstance(item, (ast.Assign, ast.AnnAssign))
        for target in (
            item.targets if isinstance(item, ast.Assign) else [item.target]
        )
        if isinstance(target, ast.Name)
    ]
    print(f"{rel}::{class_name} bases={bases} class_state={class_state[:20]}")
    if class_name == "SmartAppendTask":
        wanted = {
            "smartAppend4_file_pandas", "process_csv", "parseDelCriteria",
            "validateHDRS", "GetUnionOfCols",
        }
        for method in node.body:
            if isinstance(method, (ast.FunctionDef, ast.AsyncFunctionDef)) and method.name in wanted:
                attrs = sorted({
                    sub.attr
                    for sub in ast.walk(method)
                    if isinstance(sub, ast.Attribute)
                    and isinstance(sub.value, ast.Name)
                    and sub.value.id == "self"
                })
                names = sorted({
                    sub.id
                    for sub in ast.walk(method)
                    if isinstance(sub, ast.Name)
                    and sub.id.startswith(("g", "Mem", "SPF"))
                })
                print(f"  {method.name}: self={attrs} globals={names}")

print("\n### IMPORT PROBES")
sys.path.insert(0, str(ROOT.parent.resolve()))
for module in [
    "SPSQL3_py.SPFLib",
    "SPSQL3_py.SPFLib.SPFGlobals",
    "SPSQL3_py.SPFLib.SPFUtilities.utils",
    "SPSQL3_py.SPFLib.SPFUtilities.memtable",
    "SPSQL3_py.SPFLib.SPFSQL3",
    "SPSQL3_py.PyUtils",
    "SPSQL3_py.AutoComm_HTML_Report",
    "SPSQL3_py.PyGraphingMethods",
    "SPSQL3_py.PyPlot_Class",
]:
    try:
        importlib.import_module(module)
    except BaseException as exc:
        print(f"{module}: FAIL {type(exc).__name__}: {exc}")
    else:
        print(f"{module}: OK")
