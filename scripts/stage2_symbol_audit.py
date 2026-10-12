"""Stage 2 static symbol/consumer inventory (not a dead-code detector).

Run from any working directory: python scripts/stage2_symbol_audit.py
Only standard-library AST is used; dynamic registrations, callable reflection,
string-based lookups and instance method dispatch require manual verification.
"""
from __future__ import annotations

import ast
import json
from collections import defaultdict
from importlib.util import resolve_name
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1] / "src"
TARGET_MODULES = (
    "vg2c.emitter.models",
    "vg2c.emitter.project",
    "vg2c.utility_metadata",
    "vg2c.utilities._base",
    "vg2c.utilities._emit_helpers",
    "vg2c.utilities.sqlite_engine",
    "vg2c.editing",
    "vg2c.workflow",
    "vg2c.semantics",
    "vg2c.reorder",
    "vg2c.dataflow.file_effects",
    "vg2c.runtime.job",
    "vg2c.runtime.query",
)


def module_name(path: Path) -> str:
    parts = path.relative_to(ROOT).with_suffix("").parts
    return ".".join(parts[:-1] if parts[-1] == "__init__" else parts)


def scan():
    paths = sorted(
        path
        for directory in ("vg2c", "vg2c_ui")
        for path in (ROOT / directory).rglob("*.py")
    )
    trees = {module_name(path): ast.parse(path.read_text(encoding="utf-8"), filename=str(path))
             for path in paths}
    # Relative imports resolve from the importing package, not the module itself.
    packages = {module_name(path): (module_name(path) if path.stem == "__init__"
                                    else module_name(path).rpartition(".")[0])
                for path in paths}
    names: dict[str, set[str]] = {}
    attributes: dict[str, set[str]] = {}
    imports: dict[str, list[tuple[str, str]]] = defaultdict(list)
    dynamic: dict[str, list[str]] = defaultdict(list)
    definitions: list[dict[str, object]] = []
    registrants: list[dict[str, object]] = []
    explicit_exports: dict[str, set[str]] = {}
    for module, tree in trees.items():
        exported: set[str] = set()
        for statement in tree.body:
            if isinstance(statement, ast.Assign) and any(
                isinstance(target, ast.Name) and target.id == "__all__"
                for target in statement.targets
            ):
                try:
                    declared = ast.literal_eval(statement.value)
                except (ValueError, TypeError):
                    continue
                if isinstance(declared, (tuple, list, set)) and all(
                    isinstance(value, str) for value in declared
                ):
                    exported.update(declared)
        explicit_exports[module] = exported
        names[module] = {node.id for node in ast.walk(tree) if isinstance(node, ast.Name)}
        attributes[module] = {node.attr for node in ast.walk(tree) if isinstance(node, ast.Attribute)}
        for node in ast.walk(tree):
            if isinstance(node, ast.ImportFrom):
                owner = "." * node.level + (node.module or "")
                if node.level:
                    owner = resolve_name(owner, packages[module])
                for alias in node.names:
                    imports[module].append((owner, alias.name))
            elif isinstance(node, ast.Import):
                imports[module].extend((alias.name, "*module*") for alias in node.names)
            elif isinstance(node, ast.Call):
                callee = node.func
                name = callee.id if isinstance(callee, ast.Name) else (
                    callee.attr if isinstance(callee, ast.Attribute) else "")
                if name in {"getattr", "setattr", "globals", "import_module", "__import__"}:
                    dynamic[module].append(name)
        for node in tree.body:
            if isinstance(node, ast.ClassDef):
                bases = {
                    base.id if isinstance(base, ast.Name) else (
                        base.attr if isinstance(base, ast.Attribute) else "")
                    for base in node.bases
                }
                if bases & {"UtilitySpec", "EmitterUtility"}:
                    registry_name = None
                    handled = []
                    for member in node.body:
                        if not isinstance(member, ast.Assign):
                            continue
                        keys = [target.id for target in member.targets if isinstance(target, ast.Name)]
                        if "utility_name" in keys:
                            try:
                                registry_name = ast.literal_eval(member.value)
                            except (ValueError, TypeError):
                                registry_name = "<dynamic>"
                        if "handles" in keys and isinstance(member.value, (ast.Tuple, ast.List)):
                            handled = [
                                ast.unparse(value) for value in member.value.elts
                            ]
                    registrants.append({
                        "module": module, "class": node.name,
                        "registry_name": registry_name, "handles": handled,
                    })
            if isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef, ast.ClassDef)):
                definitions.append({"module": module, "symbol": node.name,
                                    "kind": type(node).__name__, "line": node.lineno})
                if isinstance(node, ast.ClassDef):
                    for member in node.body:
                        if isinstance(member, (ast.FunctionDef, ast.AsyncFunctionDef)):
                            definitions.append({
                                "module": module, "symbol": node.name + "." + member.name,
                                "kind": "method", "line": member.lineno,
                            })
                        elif isinstance(member, (ast.Assign, ast.AnnAssign)):
                            targets = member.targets if isinstance(member, ast.Assign) else [member.target]
                            for target in targets:
                                if isinstance(target, ast.Name):
                                    definitions.append({
                                        "module": module, "symbol": node.name + "." + target.id,
                                        "kind": "class field", "line": member.lineno,
                                    })
    for item in definitions:
        owner, symbol = str(item["module"]), str(item["symbol"])
        leaf = symbol.rsplit(".", 1)[-1]
        users = sorted(
            m for m in trees if m != owner
            and (leaf in names[m] or leaf in attributes[m]
                 or (owner, leaf) in imports[m])
        )
        item["possible_external_users"] = users
        item["possible_local_use"] = leaf in names[owner] or leaf in attributes[owner]
        item["explicit_export"] = symbol in explicit_exports[owner]
        item["exact_importers"] = sorted(
            m for m, imported in imports.items() if (owner, leaf) in imported
        )
    forbidden = []
    for module, imported in imports.items():
        for owner, name in imported:
            if module.startswith("vg2c.runtime.") and (
                owner.startswith(("vg2c_ui", "vg2c.emitter", "vg2c.sql_editor",
                                  "vg2c.editing", "vg2c.workflow", "vg2c.compilation",
                                  "vg2c.semantics", "vg2c.dataflow"))
            ):
                forbidden.append((module, owner, name))
    return {
        "module_count": len(trees),
        "core_modules": sum(m == "vg2c" or m.startswith("vg2c.") for m in trees),
        "ui_modules": sum(m == "vg2c_ui" or m.startswith("vg2c_ui.") for m in trees),
        "definitions": definitions,
        "compiler_utility_registrants": registrants,
        "forbidden_runtime_imports": forbidden,
        "dynamic_modules": {m: sorted(set(x)) for m, x in dynamic.items()},
        "imports": {m: refs for m, refs in imports.items()},
        "explicit_exports": {m: sorted(x) for m, x in explicit_exports.items() if x},
    }


def main():
    data = scan()
    records = data["definitions"]
    print("STAGE2_SYMBOL_AUDIT_SUMMARY", json.dumps({
        "modules": data["module_count"], "core": data["core_modules"],
        "ui": data["ui_modules"], "definitions": len(records),
        "forbidden_runtime_imports": data["forbidden_runtime_imports"],
        "dynamic_modules": len(data["dynamic_modules"]),
        "utility_registrants": len(data["compiler_utility_registrants"]),
    }, sort_keys=True))
    for registrant in data["compiler_utility_registrants"]:
        print("STAGE2_COMPILER_REGISTRY", json.dumps(registrant, sort_keys=True))
    for module in TARGET_MODULES:
        definitions = [d for d in records if d["module"] == module]
        possible_unused = [
            d["symbol"] for d in definitions
            if not d["possible_external_users"] and not str(d["symbol"]).endswith(
                ("__init__", "__call__", "__init_subclass__", "__get__"))
        ]
        exact = [
            (d["symbol"], d["exact_importers"]) for d in definitions
            if d["exact_importers"]
        ]
        print("STAGE2_SYMBOL_MODULE", json.dumps({
            "module": module, "definitions": len(definitions),
            "possible_unreferenced_externally": possible_unused,
            "possible_unreferenced_locally_or_externally": [
                d["symbol"] for d in definitions if not d["possible_external_users"]
                and not d["possible_local_use"] and not d["explicit_export"]
                and not str(d["symbol"]).split(".")[-1].startswith("__")
            ],
            "explicit_exports": data["explicit_exports"].get(module, []),
            "exact_imported_symbols": exact,
            "dynamic_lookup": data["dynamic_modules"].get(module, []),
        }, sort_keys=True))
    print("STAGE2_SYMBOL_AUDIT_CAUTION: AST name/attribute usage is conservative, "
          "not type-resolved; zero external mentions does not prove dead code. "
          "Check same-module callers, exports, dynamic registry and generated imports "
          "before deletion.")


if __name__ == "__main__":
    main()
