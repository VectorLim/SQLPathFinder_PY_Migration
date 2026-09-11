"""Root-driven runtime dependency closure for standalone Python emission.

This is deliberately not a Python linker: unresolved namespace identity or import
timing is an error, while uncertain class membership widens the selected class.
"""

from __future__ import annotations

import ast
import copy
import logging
from collections import deque
from dataclasses import dataclass, field
from pathlib import Path

from ._symbol_index import (
    COMPILER_BASES,
    COMPILER_DECORATORS,
    COMPILER_STATE,
    FUNCTIONS,
    IMPORTS,
    ModuleIndex,
    ResolutionError,
    SourceIndex,
    SymbolRef,
    bound_names,
    dotted,
)

log = logging.getLogger("vg2c.utilities")


@dataclass(frozen=True)
class Dependency:
    target: SymbolRef
    eager: bool


@dataclass(frozen=True)
class Fallback:
    symbol: SymbolRef
    line: int
    boundary: str
    reason: str


@dataclass
class Selection:
    index: SourceIndex
    nodes: dict[SymbolRef, list[ast.stmt]] = field(default_factory=dict)
    dependencies: dict[SymbolRef, set[Dependency]] = field(default_factory=dict)
    context: dict[str, SymbolRef] = field(default_factory=dict)
    fallbacks: list[Fallback] = field(default_factory=list)


class SymbolResolver:
    def __init__(self, source_root: Path, utilities: dict[str, SymbolRef] | None = None) -> None:
        self.index = SourceIndex(source_root)
        self.utilities = utilities or {}
        self.selection = Selection(self.index)
        self.pending: deque[SymbolRef] = deque()
        self.selected: set[SymbolRef] = set()
        self.whole: set[SymbolRef] = set()
        self.active_modules: set[str] = set()
        self.eager_calls: set[tuple[SymbolRef, SymbolRef]] = set()
        self.import_aliases: dict[SymbolRef, SymbolRef] = {}
        self.freeze_context = False

    def scan_workflow(self, source: str) -> None:
        """Scan runtime roots without changing text owned by the workflow writer."""
        module = ModuleIndex("__workflow__", Path("<generated-workflow>"), ast.parse(source))
        module.bindings.update({ref.name: ref for ref in self.utilities.values()})
        self.index.modules[module.name] = module
        for ref, symbol in module.symbols.items():
            if ref.name.startswith("step_") or ref.name == "run":
                visitor = _References(self, ref)
                for node in symbol.nodes:
                    visitor.visit(copy.deepcopy(node))
        for ref in list(self.selection.dependencies):
            if ref.module == module.name:
                del self.selection.dependencies[ref]

    def require(
        self,
        ref: SymbolRef,
        origin: SymbolRef | None = None,
        *,
        eager: bool = False,
    ) -> None:
        if ref.module == "__workflow__":
            return  # These definitions are emitted verbatim by the workflow writer.
        if ref.module in {"vg2c.utilities._base", "vg2c.emitter.models"}:
            raise ResolutionError(
                f"{origin or ref}: compiler-only dependency {ref} cannot be embedded at runtime"
            )
        symbol = self.index.symbol(ref)
        if symbol.is_class and len(symbol.nodes) != 1:
            raise ResolutionError(
                f"Repeated class definition {ref} requires preserving rebinding order"
            )
        if any(getattr(node, "type_params", None) for node in symbol.nodes):
            raise ResolutionError(f"{ref}: generic type-parameter scopes are not supported")
        if origin is not None:
            self.selection.dependencies.setdefault(origin, set()).add(Dependency(ref, eager))
        if ref in self.selected:
            return
        self.selected.add(ref)
        self.pending.append(ref)
        if symbol.owner:
            self.require(symbol.owner, ref)
        if ref.module not in self.active_modules:
            self.active_modules.add(ref.module)
            for effect in self.index.module(ref.module).effects:
                self.require(effect, ref)

    def import_target(self, ref: SymbolRef) -> SymbolRef | str | None:
        symbol = self.index.symbol(ref)
        node = symbol.nodes[0]
        if len(symbol.nodes) != 1 or not isinstance(node, IMPORTS):
            return None
        alias = node.names[0]
        if isinstance(node, ast.Import):
            return alias.name if self.index.path_for(alias.name) else None
        module = self.index.import_module(self.index.module(ref.module), node)
        if not self.index.path_for(module):
            return None
        target = self.index.module(module).bindings.get(alias.name)
        if target:
            return target
        child_module = f"{module}.{alias.name}"
        if self.index.path_for(child_module):
            return child_module
        raise ResolutionError(f"{ref}: missing imported symbol {module}.{alias.name}")

    def canonical(self, ref: SymbolRef) -> SymbolRef | str:
        seen: set[SymbolRef] = set()
        while ref not in seen:
            seen.add(ref)
            target = self.import_target(ref)
            if target is None:
                return ref
            if isinstance(target, str):
                return target
            ref = target
        raise ResolutionError(f"Cyclic import binding: {ref}")

    def qualified(self, module: ModuleIndex, node: ast.AST) -> str | None:
        name = dotted(node.func if isinstance(node, ast.Call) else node)
        if not name:
            return None
        first, _, tail = name.partition(".")
        ref = module.bindings.get(first)
        if ref:
            imp = self.index.symbol(ref).nodes[0]
            if isinstance(imp, ast.ImportFrom):
                base = self.index.import_module(module, imp)
                name = f"{base}.{imp.names[0].name}" + (f".{tail}" if tail else "")
            elif isinstance(imp, ast.Import):
                name = imp.names[0].name + (f".{tail}" if tail else "")
            else:
                name = str(ref) + (f".{tail}" if tail else "")
        return name

    def member(
        self, cls: SymbolRef, name: str, origin: SymbolRef | None = None, *, eager: bool = False
    ) -> None:
        self.require(cls, origin, eager=eager)
        symbol = self.index.symbol(cls)
        if name in symbol.members:
            child = symbol.members[name]
            self.require(child, origin, eager=eager)
            if self.index.symbol(child).is_class:
                self.widen(child, "nested class retained intact", origin=origin)

    def context_member(
        self, name: str, member: str | None, origin: SymbolRef | None = None
    ) -> None:
        if name not in self.utilities:
            raise ResolutionError(f"Unknown context utility {name!r}")
        ref = self.utilities[name]
        if name == "ctx":
            if member:
                self.member(ref, member, origin)
            else:
                self.require(ref, origin)
            return
        if self.freeze_context and name not in self.selection.context:
            return
        self.selection.context[name] = ref
        if member:
            self.member(ref, member, origin)
        else:
            self.widen(ref, "context instance escapes", origin=origin)

    def runtime_members(self, ref: SymbolRef) -> tuple[SymbolRef, ...]:
        """Return the runtime surface of a registered utility class."""
        symbol = self.index.symbol(ref)
        if not symbol.is_class or not self.is_utility(ref):
            return tuple(
                child
                for child, part in self.index.module(ref.module).symbols.items()
                if part.owner == ref
            )
        module = self.index.module(ref.module)
        members: list[SymbolRef] = []
        for name, child in symbol.members.items():
            if name in COMPILER_STATE or name in {"check", "emit_block"}:
                continue
            if not name.startswith("_") or name.startswith("__") and name.endswith("__"):
                members.append(child)
                continue
            if any(
                self.qualified(module, decorator) == "vg2c.emitter.models.emittable"
                for node in self.index.symbol(child).nodes
                if isinstance(node, FUNCTIONS)
                for decorator in node.decorator_list
            ):
                members.append(child)
        return tuple(members)

    def safe_decorator(self, module: ModuleIndex, node: ast.expr) -> bool:
        name = self.qualified(module, node)
        return name in {
            "property",
            "staticmethod",
            "classmethod",
            "contextlib.contextmanager",
        } or bool(name and name.endswith((".getter", ".setter", ".deleter")))

    def seed_runtime_api(self, ref: SymbolRef) -> None:
        if not self.is_utility(ref) or not self.index.symbol(ref).is_class:
            return
        self.require(ref)
        for member in self.runtime_members(ref):
            self.require(member, ref)

    def expand_runtime_apis(self) -> None:
        """Retain utility APIs without adding instances for uncalled ctx operations.

        The workflow closure has already chosen the constructor mapping. Extra
        methods still resolve their imports, helpers and state through this graph.
        """
        included = {
            ref for ref in self.selected if self.is_utility(ref) and self.index.symbol(ref).is_class
        }
        self.freeze_context = True
        for ref in sorted(included):
            self.seed_runtime_api(ref)

    def widen(
        self,
        ref: SymbolRef,
        reason: str,
        *,
        origin: SymbolRef | None = None,
        line: int | None = None,
    ) -> None:
        self.require(ref, origin)
        if ref in self.whole:
            return
        self.whole.add(ref)
        symbol = self.index.symbol(ref)
        line = line or symbol.nodes[0].lineno
        boundary = "class" if symbol.is_class else "module"
        fallback = Fallback(origin or ref, line, boundary, reason)
        self.selection.fallbacks.append(fallback)
        path = self.index.module(ref.module).path
        log.warning(
            "[symbol-fallback] %s:%d:1: %s: retain %s %s (%s)",
            path,
            line,
            fallback.symbol,
            boundary,
            ref,
            reason,
        )
        refs = (
            self.runtime_members(ref)
            if symbol.is_class
            else self.index.module(ref.module).bindings.values()
        )
        for child in refs:
            self.require(child, ref)

    def is_utility(self, ref: SymbolRef) -> bool:
        return ref in self.utilities.values()

    def local_import_alias(self, target: SymbolRef) -> SymbolRef:
        """A private global binding avoids `helper = helper` in a local scope."""
        if target in self.import_aliases:
            return self.import_aliases[target]
        module = self.index.module(target.module)
        name = "_vg2c_import_" + str(target).replace(".", "__")
        ref = SymbolRef(module.name, name)
        if name in module.bindings:
            raise ResolutionError(f"Reserved import alias collides with {ref}")
        node = ast.Assign(
            targets=[ast.Name(id=name, ctx=ast.Store())],
            value=ast.Name(id=target.name, ctx=ast.Load()),
        )
        ast.copy_location(node, self.index.symbol(target).nodes[-1])
        module.add(node, [name])
        module.symbols[ref].generated = True
        self.import_aliases[target] = ref
        return ref

    def drain(self) -> Selection:
        while self.pending:
            ref = self.pending.popleft()
            symbol = self.index.symbol(ref)
            if self.freeze_context:
                self.seed_runtime_api(ref)
            visitor = _References(self, ref)
            nodes = copy.deepcopy(symbol.nodes)
            if symbol.is_class:
                node = nodes[0]
                module = self.index.module(ref.module)
                node.bases = [
                    base
                    for base in node.bases
                    if self.qualified(module, base) not in COMPILER_BASES
                ]
                node.decorator_list = [
                    dec
                    for dec in node.decorator_list
                    if self.qualified(module, dec) not in COMPILER_DECORATORS
                ]
                if node.bases or node.keywords or node.decorator_list:
                    self.widen(ref, "inheritance, metaclass, or class decorator", line=node.lineno)
                if not self.is_utility(ref):
                    # Ordinary runtime helpers stay intact; only registered
                    # utilities mix runtime members with compiler machinery.
                    for child in self.runtime_members(ref):
                        self.require(child, ref)
                else:
                    for name, child in symbol.members.items():
                        if name.startswith("__") and name.endswith("__"):
                            self.require(child, ref)
                        if name in {
                            "__getattr__",
                            "__getattribute__",
                            "__setattr__",
                            "__delattr__",
                        }:
                            self.widen(ref, "dynamic attribute protocol", line=node.lineno)
                node.body = []
                node.bases = [visitor.visit(base) for base in node.bases]
                node.keywords = [visitor.visit(kw) for kw in node.keywords]
                node.decorator_list = [visitor.decorator(dec) for dec in node.decorator_list]
                self.selection.nodes[ref] = [node]
            else:
                self.selection.nodes[ref] = visitor._body(nodes)
        # Calls executed while defining a symbol also need their runtime closure ready.
        for origin, callee in sorted(self.eager_calls):
            seen: set[SymbolRef] = set()
            stack = [callee]
            while stack:
                target = stack.pop()
                if target in seen:
                    continue
                seen.add(target)
                stack.extend(edge.target for edge in self.selection.dependencies.get(target, ()))
            for target in seen:
                self.selection.dependencies.setdefault(origin, set()).add(Dependency(target, True))
        return self.selection


@dataclass
class _Scope:
    names: set[str]
    globals: set[str] = field(default_factory=set)
    receivers: dict[str, SymbolRef] = field(default_factory=dict)
    class_ref: SymbolRef | None = None
    is_class: bool = False


class _References(ast.NodeTransformer):
    def __init__(self, resolver: SymbolResolver, origin: SymbolRef) -> None:
        self.r = resolver
        self.origin = origin
        self.module = resolver.index.module(origin.module)
        self.eager = True
        self.annotation = False
        self.scopes: list[_Scope] = []
        owner = resolver.index.symbol(origin).owner
        self.owner = owner
        if owner:
            self.scopes.append(_Scope(set(), class_ref=owner))

    def error(self, node: ast.AST, message: str) -> None:
        raise ResolutionError(
            f"{self.module.path}:{getattr(node, 'lineno', 1)}: {self.origin}: {message}"
        )

    def dependency(self, ref: SymbolRef) -> None:
        self.r.require(ref, self.origin, eager=self.eager)

    def builtin(self, node: ast.AST, name: str) -> bool:
        return (
            isinstance(node, ast.Name)
            and node.id == name
            and self.binding(name) is None
            and not any(name in scope.names for scope in self.scopes)
        )

    def binding(self, name: str) -> SymbolRef | None:
        in_function = False
        for scope in reversed(self.scopes):
            if name in scope.globals:
                break
            if scope.class_ref or scope.is_class:
                if not in_function:
                    if scope.class_ref:
                        member = self.r.index.symbol(scope.class_ref).members.get(name)
                        if member:
                            return member
                    elif name in scope.names:
                        return None
            else:
                in_function = True
                if name in scope.names:
                    return None
        return self.module.bindings.get(name)

    def receiver(self, node: ast.AST) -> SymbolRef | None:
        if isinstance(node, ast.Attribute):
            parent = self.receiver(node.value)
            if parent == self.r.utilities.get("ctx") and parent is not None:
                return self.r.utilities.get(node.attr)
            if parent:
                member = self.r.index.symbol(parent).members.get(node.attr)
                if member and self.r.index.symbol(member).is_class:
                    return member
        if isinstance(node, ast.Name):
            for scope in reversed(self.scopes):
                if node.id in scope.receivers:
                    return scope.receivers[node.id]
                if node.id in scope.names:
                    return None
            ref = self.binding(node.id)
            if ref:
                target = self.r.canonical(ref)
                if isinstance(target, SymbolRef) and self.r.index.symbol(target).is_class:
                    return target
        return None

    def callable_ref(self, node: ast.AST) -> SymbolRef | None:
        if isinstance(node, ast.Name):
            ref = self.binding(node.id)
            if ref:
                target = self.r.canonical(ref)
                return target if isinstance(target, SymbolRef) else None
        if isinstance(node, ast.Attribute):
            recv = self.receiver(node.value)
            if recv:
                return self.r.index.symbol(recv).members.get(node.attr, recv)
            member = self.module_member(node)
            if member and len(member[1]) == 1:
                return member[0]
        return None

    def module_member(self, node: ast.Attribute) -> tuple[SymbolRef, list[str]] | None:
        chain = dotted(node)
        if not chain:
            return None
        first = chain.split(".", 1)[0]
        ref = self.binding(first)
        target = self.r.canonical(ref) if ref else None
        if not isinstance(target, str):
            return None
        imp = self.r.index.symbol(ref).nodes[0]
        prefix = (
            imp.names[0].name if isinstance(imp, ast.Import) and not imp.names[0].asname else first
        )
        if not chain.startswith(prefix + "."):
            self.error(node, "ambiguous project package reference")
        rest = chain[len(prefix) + 1 :].split(".")
        binding = self.r.index.module(target).bindings.get(rest[0])
        if binding is None:
            self.error(node, f"unknown member {target}.{rest[0]}")
        return binding, rest

    def visit_Name(self, node: ast.Name) -> ast.Name:
        if not isinstance(node.ctx, ast.Load):
            return node
        ref = self.binding(node.id)
        if ref is None and not any(node.id in scope.names for scope in self.scopes):
            if node.id == "__name__" and self.origin.module != "__workflow__":
                return ast.copy_location(ast.Constant(self.module.name), node)
            if node.id in {"__spec__", "__loader__", "__package__"}:
                self.error(
                    node, "package/loader identity cannot be represented in a standalone module"
                )
        if ref:
            self.dependency(ref)
            target = self.r.canonical(ref)
            if isinstance(target, str):
                self.error(
                    node, "project module objects cannot escape; use a statically named member"
                )
            if self.r.index.symbol(target).is_class and not self.annotation:
                self.r.widen(
                    target,
                    "class value or constructed instance escapes",
                    origin=self.origin,
                    line=node.lineno,
                )
        elif not self.annotation:
            recv = self.receiver(node)
            if recv and recv != self.r.utilities.get("ctx"):
                self.r.widen(recv, "instance escapes", origin=self.origin, line=node.lineno)
        return node

    def visit_Attribute(self, node: ast.Attribute) -> ast.AST:
        if node.attr in {"__globals__", "__code__"}:
            self.error(node, "function namespace/code reflection cannot be safely embedded")
        # The compiler's ctx parameter is a known protocol, not general type inference.
        chain = dotted(node)
        if chain:
            parts = chain.split(".")
            context = parts[0] == "ctx" and "ctx" in self.r.utilities
            recv = self.receiver(ast.Name(id=parts[0], ctx=ast.Load()))
            if recv is not None and recv == self.r.utilities.get("ctx"):
                context = True
            if context:
                if parts[1] in self.r.utilities and parts[1] != "ctx":
                    self.r.context_member(
                        parts[1], parts[2] if len(parts) > 2 else None, self.origin
                    )
                else:
                    self.r.member(self.r.utilities["ctx"], parts[1], self.origin, eager=self.eager)
                return node
            if recv:
                ref = self.binding(parts[0])
                if ref:
                    self.dependency(ref)
                if parts[1] == "__class__" or (ref and parts[1] in {"__dict__", "__annotations__"}):
                    self.r.widen(recv, "class reflection", origin=self.origin, line=node.lineno)
                self.r.member(recv, parts[1], self.origin, eager=self.eager)
                if isinstance(node.ctx, (ast.Store, ast.Del)) and ref:
                    self.r.widen(recv, "class mutation", origin=self.origin, line=node.lineno)
                return node
            member = self.module_member(node)
            if member:
                binding, rest = member
                self.dependency(binding)
                canonical = self.r.canonical(binding)
                if isinstance(canonical, str):
                    self.error(node, "nested module objects require a direct module import")
                if self.r.index.symbol(canonical).is_class:
                    if len(rest) > 1:
                        self.r.member(canonical, rest[1], self.origin, eager=self.eager)
                    else:
                        self.r.widen(
                            canonical,
                            "class value escapes",
                            origin=self.origin,
                            line=node.lineno,
                        )
                # A local named like the imported symbol must not capture the rewrite.
                if any(rest[0] in scope.names for scope in self.scopes):
                    alias = self.r.local_import_alias(binding)
                    self.dependency(alias)
                    rest[0] = alias.name
                return ast.copy_location(ast.parse(".".join(rest), mode="eval").body, node)
        return self.generic_visit(node)

    def visit_Import(self, node: ast.Import | ast.ImportFrom) -> ast.AST | list[ast.stmt]:
        result: list[ast.stmt] = []
        for alias in node.names:
            item = copy.deepcopy(node)
            item.names = [alias]
            module = (
                alias.name
                if isinstance(node, ast.Import)
                else self.r.index.import_module(self.module, node)
            )
            if not self.r.index.path_for(module):
                if isinstance(node, ast.ImportFrom) and node.level:
                    self.error(node, "unresolved relative import")
                result.append(item)
                continue
            if self.origin.module == "__workflow__":
                self.error(
                    node,
                    "project imports in raw workflow code require rewriting; use ctx operations",
                )
            if isinstance(node, ast.Import):
                if self.scopes and not all(s.class_ref for s in self.scopes):
                    self.error(
                        node, "runtime-local project module import requires namespace identity"
                    )
                continue
            target = self.r.index.module(module).bindings.get(alias.name)
            if target is None:
                if self.r.index.path_for(f"{module}.{alias.name}"):
                    continue
                self.error(node, f"unknown imported symbol {module}.{alias.name}")
            # Deferred project initialization cannot be hoisted without changing behavior.
            if not self.eager and self.r.index.module(module).effects:
                self.error(node, f"runtime-local import of {module} has initialization effects")
            self.dependency(target)
            local = alias.asname or alias.name
            if not self.eager:
                target = self.r.local_import_alias(target)
                if any(target.name in scope.names for scope in self.scopes):
                    self.error(node, "reserved local import alias is shadowed")
                self.dependency(target)
            if local != target.name:
                assignment = ast.Assign(
                    targets=[ast.Name(id=local, ctx=ast.Store())],
                    value=ast.Name(id=target.name, ctx=ast.Load()),
                )
                result.append(ast.copy_location(assignment, node))
        if not result and not self.eager:
            return [ast.copy_location(ast.Pass(), node)]
        return result

    visit_ImportFrom = visit_Import

    def _annotation(self, node: ast.AST | None) -> ast.AST | None:
        if node is None:
            return None
        eager, annotation = self.eager, self.annotation
        self.eager = self.eager and not self.module.future_annotations
        self.annotation = True
        result = self.visit(node)
        self.eager, self.annotation = eager, annotation
        # Localize postponed annotations instead of changing other modules' semantics.
        return (
            ast.copy_location(ast.Constant(ast.unparse(result)), node)
            if self.module.future_annotations
            else result
        )

    def visit_AnnAssign(self, node: ast.AnnAssign) -> ast.AnnAssign:
        node.annotation = self._annotation(node.annotation)
        node.target = self.visit(node.target)
        if node.value:
            node.value = self.visit(node.value)
        return node

    def visit_FunctionDef(self, node: ast.FunctionDef) -> ast.FunctionDef:
        node.decorator_list = [
            d
            for d in node.decorator_list
            if self.r.qualified(self.module, d) not in COMPILER_DECORATORS
        ]
        if self.owner:
            for dec in node.decorator_list:
                if not self.r.safe_decorator(self.module, dec):
                    self.r.widen(
                        self.owner, "custom method decorator", origin=self.origin, line=dec.lineno
                    )
        node.decorator_list = [self.decorator(d) for d in node.decorator_list]
        args = node.args
        args.defaults = [self.visit(n) for n in args.defaults]
        args.kw_defaults = [self.visit(n) if n else None for n in args.kw_defaults]
        all_args = [
            *args.posonlyargs,
            *args.args,
            *args.kwonlyargs,
            *([args.vararg] if args.vararg else []),
            *([args.kwarg] if args.kwarg else []),
        ]
        for arg in all_args:
            arg.annotation = self._annotation(arg.annotation)
        node.returns = self._annotation(node.returns)
        bindings = bound_names(node.body)
        scope = _Scope(bindings.names | {arg.arg for arg in all_args}, bindings.globals)
        if self.scopes and self.scopes[-1].class_ref and args.posonlyargs + args.args:
            first = (args.posonlyargs + args.args)[0].arg
            if not any(dotted(d) == "staticmethod" for d in node.decorator_list):
                scope.receivers[first] = self.scopes[-1].class_ref
        if "ctx" in scope.names and "ctx" in self.r.utilities:
            scope.receivers["ctx"] = self.r.utilities["ctx"]
        self.scopes.append(scope)
        eager = self.eager
        self.eager = False
        node.body = self._body(node.body) or [ast.Pass()]
        self.eager = eager
        self.scopes.pop()
        return node

    def visit_AugAssign(self, node: ast.AugAssign) -> ast.AugAssign:
        target = copy.deepcopy(node.target)
        if isinstance(target, (ast.Name, ast.Attribute, ast.Subscript)):
            target.ctx = ast.Load()
        self.visit(target)
        return self.generic_visit(node)

    visit_AsyncFunctionDef = visit_FunctionDef

    def decorator(self, node: ast.expr) -> ast.expr:
        target = self.callable_ref(node.func if isinstance(node, ast.Call) else node)
        if target and self.eager:
            self.r.eager_calls.add((self.origin, target))
        return self.visit(node)

    def _body(self, body: list[ast.stmt]) -> list[ast.stmt]:
        result: list[ast.stmt] = []
        for node in body:
            value = self.visit(node)
            result.extend(value if isinstance(value, list) else [value] if value else [])
        return result

    def visit_ClassDef(self, node: ast.ClassDef) -> ast.ClassDef:
        # A function-local class is kept intact, with its lexical scope preserved.
        node.bases = [self.visit(base) for base in node.bases]
        node.decorator_list = [self.visit(d) for d in node.decorator_list]
        self.scopes.append(_Scope(bound_names(node.body).names, is_class=True))
        node.body = self._body(node.body) or [ast.Pass()]
        self.scopes.pop()
        return node

    def visit_Lambda(self, node: ast.Lambda) -> ast.Lambda:
        return self._lambda(node, called=False)

    def _lambda(self, node: ast.Lambda, *, called: bool) -> ast.Lambda:
        node.args.defaults = [self.visit(n) for n in node.args.defaults]
        node.args.kw_defaults = [self.visit(n) if n else None for n in node.args.kw_defaults]
        names = {
            arg.arg for arg in [*node.args.args, *node.args.posonlyargs, *node.args.kwonlyargs]
        }
        names.update(a.arg for a in (node.args.vararg, node.args.kwarg) if a)
        self.scopes.append(_Scope(names))
        eager = self.eager
        self.eager = eager if called else False
        node.body = self.visit(node.body)
        self.eager = eager
        self.scopes.pop()
        return node

    def visit_ListComp(self, node: ast.ListComp) -> ast.AST:
        # The outer iterable is evaluated in the enclosing scope.
        node.generators[0].iter = self.visit(node.generators[0].iter)
        names = {
            n.id for gen in node.generators for n in ast.walk(gen.target) if isinstance(n, ast.Name)
        }
        self.scopes.append(_Scope(names))
        for index, gen in enumerate(node.generators):
            if index:
                gen.iter = self.visit(gen.iter)
            gen.ifs = [self.visit(n) for n in gen.ifs]
        if isinstance(node, ast.DictComp):
            node.key, node.value = self.visit(node.key), self.visit(node.value)
        else:
            node.elt = self.visit(node.elt)
        self.scopes.pop()
        return node

    visit_SetComp = visit_ListComp
    visit_DictComp = visit_ListComp
    visit_GeneratorExp = visit_ListComp

    def visit_Call(self, node: ast.Call) -> ast.AST:
        name = dotted(node.func)
        if (
            any(self.builtin(node.func, builtin) for builtin in ("eval", "exec", "__import__"))
            or name
            and name.endswith(".import_module")
        ):
            self.error(node, "dynamic code/import execution cannot be safely embedded")
        if self.builtin(node.func, "globals"):
            self.error(node, "namespace-sensitive globals() use cannot be safely embedded")
        # The existing path helper intentionally reads the generated script's __file__.
        if (
            isinstance(node.func, ast.Attribute)
            and node.func.attr == "get"
            and isinstance(node.func.value, ast.Call)
            and self.builtin(node.func.value.func, "globals")
            and node.args
            and isinstance(node.args[0], ast.Constant)
            and node.args[0].value == "__file__"
        ):
            node.args[1:] = [self.visit(arg) for arg in node.args[1:]]
            node.keywords = [self.visit(kw) for kw in node.keywords]
            return node
        if (
            any(
                self.builtin(node.func, builtin)
                for builtin in ("getattr", "hasattr", "setattr", "delattr")
            )
            and len(node.args) >= 2
        ):
            recv = self.receiver(node.args[0])
            if recv:
                attr = node.args[1]
                if isinstance(attr, ast.Constant) and isinstance(attr.value, str):
                    if recv == self.r.utilities.get("ctx") and attr.value in self.r.utilities:
                        self.r.context_member(attr.value, None, self.origin)
                    else:
                        self.r.member(recv, attr.value, self.origin, eager=self.eager)
                else:
                    self.r.widen(recv, "dynamic member name", origin=self.origin, line=node.lineno)
                node.args[1:] = [self.visit(arg) for arg in node.args[1:]]
                node.keywords = [self.visit(kw) for kw in node.keywords]
                return node
        if self.builtin(node.func, "locals"):
            self.error(node, "locals() namespace inspection is outside the static subset")
        if self.eager:
            target = self.callable_ref(node.func)
            if target:
                self.r.eager_calls.add((self.origin, target))
            if isinstance(node.func, ast.Lambda):
                node.func = self._lambda(node.func, called=True)
                node.args = [self.visit(arg) for arg in node.args]
                node.keywords = [self.visit(kw) for kw in node.keywords]
                return node
        return self.generic_visit(node)

    def visit_Subscript(self, node: ast.Subscript) -> ast.AST:
        if isinstance(node.value, ast.Call) and self.builtin(node.value.func, "globals"):
            if isinstance(node.slice, ast.Constant) and isinstance(node.slice.value, str):
                ref = self.module.bindings.get(node.slice.value)
                if ref:
                    self.dependency(ref)
                    target = self.r.canonical(ref)
                    if isinstance(target, str):
                        self.error(node, "module namespace identity cannot be embedded")
                    if self.r.index.symbol(target).is_class:
                        self.r.widen(
                            target,
                            "class obtained through globals",
                            origin=self.origin,
                            line=node.lineno,
                        )
                return node
            self.r.widen(self.origin, "dynamic global lookup", line=node.lineno)
            node.slice = self.visit(node.slice)
            return node
        return self.generic_visit(node)
