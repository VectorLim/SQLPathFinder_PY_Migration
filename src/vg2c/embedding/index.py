"""Per-compilation source and lexical binding index. Never imports inspected code."""

from __future__ import annotations

import ast
from dataclasses import dataclass, field
from pathlib import Path

FUNCTIONS = (ast.FunctionDef, ast.AsyncFunctionDef)
IMPORTS = (ast.Import, ast.ImportFrom)
COMPILER_BASES = {"vg2c.utilities._base.UtilitySpec", "vg2c.utilities._base.EmitterUtility"}
COMPILER_DECORATORS = {"vg2c.emitter.models.emittable", "vg2c.emitter.models.operation_spec"}
COMPILER_STATE = {"handles", "check_priority", "script_settings", "utility_name"}


class ResolutionError(ValueError):
    """Source cannot be safely represented in one standalone module."""


@dataclass(frozen=True, order=True)
class SymbolRef:
    module: str
    name: str

    def __str__(self) -> str:
        return f"{self.module}.{self.name}"


def dotted(node: ast.AST) -> str | None:
    if isinstance(node, ast.Name):
        return node.id
    if isinstance(node, ast.Attribute):
        parent = dotted(node.value)
        if parent:
            return f"{parent}.{node.attr}"
    return None


class Bindings(ast.NodeVisitor):
    """Collect one lexical scope; nested scopes have their own bindings."""

    def __init__(self) -> None:
        self.names: set[str] = set()
        self.globals: set[str] = set()
        self.nonlocals: set[str] = set()

    def visit_Name(self, node: ast.Name) -> None:
        if isinstance(node.ctx, (ast.Store, ast.Del)):
            self.names.add(node.id)

    def visit_FunctionDef(self, node: ast.FunctionDef) -> None:
        self.names.add(node.name)

    visit_AsyncFunctionDef = visit_FunctionDef
    visit_ClassDef = visit_FunctionDef

    def visit_Lambda(self, node: ast.Lambda) -> None:
        pass

    def visit_Import(self, node: ast.Import) -> None:
        self.names.update(a.asname or a.name.split(".")[0] for a in node.names)

    def visit_ImportFrom(self, node: ast.ImportFrom) -> None:
        self.names.update(a.asname or a.name for a in node.names)

    def visit_Global(self, node: ast.Global) -> None:
        self.globals.update(node.names)

    def visit_Nonlocal(self, node: ast.Nonlocal) -> None:
        self.nonlocals.update(node.names)

    def visit_ExceptHandler(self, node: ast.ExceptHandler) -> None:
        if node.name:
            self.names.add(node.name)
        self.generic_visit(node)

    def visit_MatchAs(self, node: ast.MatchAs) -> None:
        if node.name:
            self.names.add(node.name)
        self.generic_visit(node)

    def visit_MatchStar(self, node: ast.MatchStar) -> None:
        if node.name:
            self.names.add(node.name)

    def visit_MatchMapping(self, node: ast.MatchMapping) -> None:
        if node.rest:
            self.names.add(node.rest)
        self.generic_visit(node)

    def visit_ListComp(self, node: ast.ListComp) -> None:
        # Assignment expressions bind in the containing scope, loop targets do not.
        for child in ast.walk(node):
            if isinstance(child, ast.NamedExpr):
                self.visit(child.target)

    visit_SetComp = visit_ListComp
    visit_DictComp = visit_ListComp
    visit_GeneratorExp = visit_ListComp


def bound_names(nodes: list[ast.stmt]) -> Bindings:
    result = Bindings()
    for node in nodes:
        result.visit(node)
    result.names.difference_update(result.globals | result.nonlocals)
    return result


@dataclass
class Symbol:
    ref: SymbolRef
    nodes: list[ast.stmt]
    owner: SymbolRef | None = None
    members: dict[str, SymbolRef] = field(default_factory=dict)
    generated: bool = False

    @property
    def is_class(self) -> bool:
        return isinstance(self.nodes[0], ast.ClassDef)


@dataclass
class ModuleIndex:
    name: str
    path: Path
    tree: ast.Module
    symbols: dict[SymbolRef, Symbol] = field(default_factory=dict)
    bindings: dict[str, SymbolRef] = field(default_factory=dict)
    effects: list[SymbolRef] = field(default_factory=list)
    future_annotations: bool = False

    def __post_init__(self) -> None:
        loads = {n.id for n in ast.walk(self.tree) if isinstance(n, ast.Name)}
        for node in self.tree.body:
            if isinstance(node, ast.ImportFrom) and node.module == "__future__":
                unsupported = {a.name for a in node.names} - {"annotations"}
                if unsupported:
                    raise ResolutionError(f"{self.path}: unsupported future flags: {unsupported}")
                self.future_annotations = True
                continue
            if isinstance(node, ast.If) and dotted(node.test) in {
                "TYPE_CHECKING",
                "typing.TYPE_CHECKING",
            }:
                continue
            if (
                isinstance(node, ast.If)
                and isinstance(node.test, ast.Compare)
                and dotted(node.test.left) == "__name__"
                and len(node.test.ops) == 1
                and isinstance(node.test.ops[0], ast.Eq)
                and isinstance(node.test.comparators[0], ast.Constant)
                and node.test.comparators[0].value == "__main__"
            ):
                if node.orelse:
                    raise ResolutionError(
                        f"{self.path}:{node.lineno}: main guard with else needs normalization"
                    )
                continue  # These modules are embedded as imports, never run as entrypoints.
            if isinstance(node, ast.Expr) and isinstance(node.value, ast.Constant):
                continue
            if isinstance(node, IMPORTS):
                for alias in node.names:
                    if alias.name == "*":
                        raise ResolutionError(
                            f"{self.path}:{node.lineno}: star imports are not supported"
                        )
                    item = type(node)(**{**vars(node), "names": [alias]})
                    name = alias.asname or (
                        alias.name.split(".")[0] if isinstance(node, ast.Import) else alias.name
                    )
                    ref = self.add(item, [name])
                    if name not in loads:
                        self.effects.append(ref)
                continue
            names = sorted(bound_names([node]).names)
            if names == ["__all__"]:
                continue
            ref = self.add(node, names)
            if not isinstance(node, (*FUNCTIONS, ast.ClassDef, ast.Assign, ast.AnnAssign)):
                self.effects.append(ref)
            elif isinstance(node, (ast.Assign, ast.AnnAssign)) and not pure_value(node.value):
                self.effects.append(ref)
            elif isinstance(node, FUNCTIONS) and definition_effects(node):
                self.effects.append(ref)
            elif isinstance(node, ast.ClassDef) and definition_effects(node):
                # Base hooks, decorators and class bodies execute during import.
                # Retaining the shell still permits safe member pruning afterwards.
                self.effects.append(ref)

    def add(self, node: ast.stmt, names: list[str], owner: SymbolRef | None = None) -> SymbolRef:
        bindings = self.bindings if owner is None else self.symbols[owner].members
        name = names[0] if names else f"@{node.lineno}"
        ref = SymbolRef(self.name, f"{owner.name}.{name}" if owner else name)
        # Reassignments and property getter/setter definitions are an indivisible group.
        existing = next((bindings[n] for n in names if n in bindings), None)
        if existing is not None:
            ref = existing
            self.symbols[ref].nodes.append(node)
        else:
            self.symbols[ref] = Symbol(ref, [node], owner)
        for name in names:
            if name in bindings and bindings[name] != ref:
                raise ResolutionError(f"{self.path}:{node.lineno}: overlapping assignment groups")
            bindings[name] = ref
        if isinstance(node, ast.ClassDef):
            for child in node.body:
                self.add(child, sorted(bound_names([child]).names), ref)
        return ref


def pure_value(node: ast.AST | None) -> bool:
    """Only literals are known to have no definition-time effects."""
    if node is None:
        return True
    try:
        ast.literal_eval(node)
        return True
    except (ValueError, TypeError):
        return False


def definition_effects(node: ast.AST) -> bool:
    if isinstance(node, ast.ClassDef):
        if node.bases or node.keywords or node.decorator_list:
            return True
        for child in node.body:
            if isinstance(child, (ast.Assign, ast.AnnAssign)):
                if not pure_value(child.value):
                    return True
            elif isinstance(child, (*FUNCTIONS, ast.ClassDef)):
                if definition_effects(child):
                    return True
            elif not isinstance(child, ast.Pass) and not (
                isinstance(child, ast.Expr) and isinstance(child.value, ast.Constant)
            ):
                return True
        return False
    if not isinstance(node, FUNCTIONS):
        return False
    return bool(node.decorator_list) or any(
        not pure_value(value) for value in [*node.args.defaults, *node.args.kw_defaults]
    )


class SourceIndex:
    def __init__(self, source_root: Path) -> None:
        self.source_root = source_root.resolve()
        self.modules: dict[str, ModuleIndex] = {}

    def path_for(self, module: str) -> Path | None:
        path = self.source_root.joinpath(*module.split("."))
        for candidate in (path.with_suffix(".py"), path / "__init__.py"):
            if candidate.is_file():
                return candidate
        return None

    def module(self, name: str) -> ModuleIndex:
        if name not in self.modules:
            path = self.path_for(name)
            if path is None:
                raise ResolutionError(
                    f"Project module {name!r} has no source under {self.source_root}"
                )
            tree = ast.parse(path.read_text(encoding="utf-8-sig"), filename=str(path))
            self.modules[name] = ModuleIndex(name, path, tree)
        return self.modules[name]

    def symbol(self, ref: SymbolRef) -> Symbol:
        module = self.module(ref.module)
        try:
            return module.symbols[ref]
        except KeyError as exc:
            raise ResolutionError(f"Unknown symbol {ref}") from exc

    def import_module(self, module: ModuleIndex, node: ast.ImportFrom) -> str:
        if not node.level:
            return node.module or ""
        package = (
            module.name.split(".")
            if module.path.name == "__init__.py"
            else module.name.split(".")[:-1]
        )
        if node.level > len(package):
            raise ResolutionError(f"{module.path}:{node.lineno}: relative import escapes package")
        return ".".join(
            package[: len(package) - node.level + 1] + ([node.module] if node.module else [])
        )
