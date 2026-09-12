from __future__ import annotations

import ast
import sys
import types

import pytest

from vg2c.utilities._symbol_emit import render_symbols
from vg2c.utilities._symbol_index import ResolutionError, SymbolRef
from vg2c.utilities._symbols import SymbolResolver


def build(tmp_path, modules, roots):
    for name, source in modules.items():
        path = tmp_path.joinpath(*name.split(".")).with_suffix(".py")
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_text(source, encoding="utf-8")
    resolver = SymbolResolver(tmp_path)
    for root in roots:
        resolver.require(SymbolRef(*root))
    selection = resolver.drain()
    emitted = render_symbols(selection)
    source = "\n\n".join([*emitted.imports, *emitted.sources])
    return selection, source


def execute(source):
    module = types.ModuleType("_symbol_test")
    sys.modules[module.__name__] = module
    try:
        exec(compile(source, "<symbols>", "exec", dont_inherit=True), module.__dict__)
    finally:
        del sys.modules[module.__name__]
    return module.__dict__


def test_runtime_api_expansion_follows_new_utilities_and_private_emittables(tmp_path):
    (tmp_path / "first.py").write_text(
        "from vg2c.emitter.models import emittable\n"
        "from second import Second\n"
        "class First:\n"
        "    @emittable\n"
        "    def used(self): return 1\n"
        "    @emittable\n"
        "    def extra(self): return Second.value()\n",
        encoding="utf-8",
    )
    (tmp_path / "second.py").write_text(
        "import re\n"
        "from vg2c.emitter.models import emittable\n"
        "class Second:\n"
        "    _COMPILER_RE = re.compile('unused')\n"
        "    def check(self): return self._COMPILER_RE\n"
        "    @staticmethod\n"
        "    def value(): return 42\n"
        "    @emittable\n"
        "    def _emit_runtime(self): return self._emit_helper()\n"
        "    def _emit_helper(self): return 'runtime'\n"
        "    def _emit_compile(self): return 'compiler'\n",
        encoding="utf-8",
    )
    first = SymbolRef("first", "First")
    second = SymbolRef("second", "Second")
    resolver = SymbolResolver(tmp_path, {"first": first, "second": second})
    resolver.member(first, "used")
    resolver.drain()
    assert second not in resolver.selected
    resolver.expand_runtime_apis()
    emitted = render_symbols(resolver.drain())
    source = "\n\n".join([*emitted.imports, *emitted.sources])
    namespace = execute(source)
    assert namespace["First"]().extra() == 42
    assert namespace["Second"]()._emit_runtime() == "runtime"
    assert "_emit_compile" not in source
    assert "_COMPILER_RE" not in source
    assert "import re" not in source


def test_transitive_functions_aliases_constants_and_duplicate_roots(tmp_path):
    selection, source = build(
        tmp_path,
        {
            "a": "from b import helper as renamed\n"
            "def run(): return renamed()\ndef unused(): return 0\n",
            "b": "from c import leaf\ndef helper(): return leaf() + 1\ndef unused(): return -1\n",
            "c": "VALUE = 40\ndef leaf(): return VALUE + 1\n",
        },
        [("a", "run"), ("b", "helper"), ("a", "run")],
    )
    assert execute(source)["run"]() == 42
    assert "unused" not in source
    assert source.count("def leaf(") == 1
    assert set(selection.index.modules) == {"a", "b", "c"}


def test_helper_class_keeps_full_api_state_properties_and_method_dependencies(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
def helper(value): return value + 1
class Reader:
    LIMIT = 40
    UNUSED = 9
    def __init__(self): self.value = 1
    @property
    def amount(self): return self.value
    @amount.setter
    def amount(self, value): self.value = value
    @classmethod
    def limit(cls): return cls.LIMIT
    @staticmethod
    def inc(value): return helper(value)
    def run(self): return self.inc(self.limit()) + self.amount
    def unused(self): return helper(self.UNUSED)
"""
        },
        [("a", "Reader.run")],
    )
    reader = execute(source)["Reader"]()
    assert reader.run() == 42
    reader.amount = 3
    assert reader.run() == 44
    assert reader.unused() == 10
    assert reader.UNUSED == 9


def test_helper_class_keeps_initialization_statements(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
events = []
def record(): events.append('initialized')
class Helper:
    record()
    def run(self): return events
"""
        },
        [("a", "Helper.run")],
    )
    assert execute(source)["Helper"]().run() == ["initialized"]


def test_nested_functions_lambdas_comprehensions_and_shadowing(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
def wanted(x): return x + 1
def shadowed(): raise AssertionError('must not be included')
def run(shadowed):
    def nested(x): return wanted(x)
    fn = lambda x: nested(x)
    values = [shadowed for shadowed in range(2)]
    return fn(sum(values)) + shadowed
"""
        },
        [("a", "run")],
    )
    assert execute(source)["run"](40) == 42
    assert "def shadowed" not in source


def test_function_local_class_does_not_shadow_method_globals(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
def helper(): return 42
def run():
    class Local:
        helper = None
        def value(self): return helper()
    return Local().value()
"""
        },
        [("a", "run")],
    )
    assert execute(source)["run"]() == 42


def test_interleaved_rebinding_is_not_silently_reordered(tmp_path):
    with pytest.raises(ResolutionError, match="Interleaved rebinding"):
        build(
            tmp_path,
            {"a": "VALUE=1\ndef run(x=VALUE): return x + VALUE\nVALUE=2\n"},
            [("a", "run")],
        )


def test_recursive_functions_have_no_definition_time_cycle(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
def even(n): return n == 0 or odd(n - 1)
def odd(n): return n != 0 and even(n - 1)
"""
        },
        [("a", "even")],
    )
    assert execute(source)["even"](4)


def test_eager_defaults_and_decorators(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
def initialize(): return LIMIT
LIMIT = 42
def decorate(fn): return fn
@decorate
def run(value=initialize()): return value
"""
        },
        [("a", "run")],
    )
    assert execute(source)["run"]() == 42


def test_decorator_call_closure_is_ready_before_application(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
def decorate(fn):
    marker()
    return fn
def marker(): return 1
@decorate
def aaa(): return 42
"""
        },
        [("a", "aaa")],
    )
    assert execute(source)["aaa"]() == 42


def test_definition_time_cycle_is_diagnostic(tmp_path):
    with pytest.raises(ResolutionError, match="definition-time"):
        build(tmp_path, {"a": "def a(x=b()): return x\ndef b(x=a()): return x\n"}, [("a", "a")])


def test_partial_class_preserves_inheritance_and_super(tmp_path):
    selection, source = build(
        tmp_path,
        {
            "a": """
class Base:
    def run(self): return 40
class Child(Base):
    def run(self): return super().run() + 2
    def spare(self): return 5
"""
        },
        [("a", "Child.run")],
    )
    assert execute(source)["Child"]().run() == 42
    assert "def spare" in source
    assert any(f.boundary == "class" for f in selection.fallbacks)


def test_dataclass_and_nested_classes_remain_complete(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
from __future__ import annotations
from dataclasses import dataclass
@dataclass
class Value:
    number: int = 40
    def run(self): return self.number + 2
class Outer:
    class Inner:
        def value(self): return Value().run()
    def run(self): return self.Inner().value()
"""
        },
        [("a", "Outer.run")],
    )
    assert execute(source)["Outer"]().run() == 42


def test_optional_external_import_stays_local(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
def run(enabled=False):
    if enabled:
        import nonexistent_optional_dependency
    try:
        import another_missing_dependency as json
    except ImportError:
        import json
    return json.loads('42')
"""
        },
        [("a", "run")],
    )
    assert execute(source)["run"]() == 42
    assert not any(
        isinstance(node, (ast.Import, ast.ImportFrom)) for node in ast.parse(source).body
    )


def test_relative_and_module_qualified_imports(tmp_path):
    _, source = build(
        tmp_path,
        {
            "pkg.a": "from .b import value as answer\nimport pkg.b as other\n"
            "def run(): return answer() + other.value()\n",
            "pkg.b": "def value(): return 21\ndef unused(): return 0\n",
        },
        [("pkg.a", "run")],
    )
    assert execute(source)["run"]() == 42
    assert "import pkg" not in source
    assert "unused" not in source


def test_module_member_rewrite_cannot_be_captured_by_local(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": "import b\ndef run(value): return b.value()\n",
            "b": "def value(): return 42\n",
        },
        [("a", "run")],
    )
    assert execute(source)["run"](None) == 42


def test_local_project_symbol_import_preserves_binding(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": "def run():\n    from b import value as answer\n    return answer()\n",
            "b": "def value(): return 42\n",
        },
        [("a", "run")],
    )
    assert execute(source)["run"]() == 42
    assert "from b" not in source


def test_local_project_import_without_alias_and_eager_call(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": "def initialize():\n    from b import value\n    return value()\n"
            "def run(x=initialize()): return x\n",
            "b": "def value(): return 42\n",
        },
        [("a", "run")],
    )
    assert execute(source)["run"]() == 42


def test_default_method_call_includes_runtime_dependencies_before_use(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
class Factory:
    @staticmethod
    def build(): return later()
def later(): return 42
def first(value=Factory.build()): return value
def second(value=(lambda: later())()): return value
"""
        },
        [("a", "first"), ("a", "second")],
    )
    namespace = execute(source)
    assert namespace["first"]() == namespace["second"]() == 42


def test_instance_escape_keeps_opaque_callee_members(tmp_path):
    selection, source = build(
        tmp_path,
        {
            "a": """
def opaque(value): return value.hidden()
class Reader:
    def run(self): return opaque(self)
    def hidden(self): return 42
"""
        },
        [("a", "Reader.run")],
    )
    assert execute(source)["Reader"]().run() == 42
    assert any(f.reason == "instance escapes" for f in selection.fallbacks)


def test_descriptor_keeps_the_owning_class_complete(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
class Descriptor:
    def __get__(self, instance, owner): return instance.hidden()
class Reader:
    result = Descriptor()
    def hidden(self): return 42
    def run(self): return self.result
"""
        },
        [("a", "Reader.run")],
    )
    assert execute(source)["Reader"]().run() == 42


def test_runtime_project_import_with_effects_is_rejected(tmp_path):
    with pytest.raises(ResolutionError, match="initialization effects"):
        build(
            tmp_path,
            {
                "a": "def run():\n    from b import answer\n    return answer\n",
                "b": "answer = object()\n",
            },
            [("a", "run")],
        )


def test_global_augmented_assignment_keeps_initial_binding(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
VALUE = 41
def run():
    global VALUE
    VALUE += 1
    return VALUE
"""
        },
        [("a", "run")],
    )
    assert execute(source)["run"]() == 42


def test_unused_definition_effects_and_imported_main_guard(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": """
events = []
def record():
    events.append('defined')
    return 0
def unused(value=record()): pass
class Reader:
    def extra(self, value=record()): pass
    def run(self): return len(events)
if __name__ == '__main__':
    raise AssertionError('imported modules must not run their main guards')
"""
        },
        [("a", "Reader.run")],
    )
    assert execute(source)["Reader"]().run() == 2


def test_dynamic_member_fallback_has_reason(tmp_path):
    selection, source = build(
        tmp_path,
        {
            "a": """
class Reader:
    def run(self, name): return getattr(self, name)()
    def answer(self): return 42
    def alternate(self): return -1
"""
        },
        [("a", "Reader.run")],
    )
    assert execute(source)["Reader"]().run("answer") == 42
    assert "def alternate" in source
    assert any(f.reason == "dynamic member name" for f in selection.fallbacks)


def test_dynamic_global_lookup_retains_module(tmp_path):
    selection, source = build(
        tmp_path,
        {
            "a": """
def answer(): return 42
def run(name): return globals()[name]()
def alternate(): return -1
"""
        },
        [("a", "run")],
    )
    assert execute(source)["run"]("answer") == 42
    assert "alternate" in source
    assert selection.fallbacks[0].boundary == "module"


@pytest.mark.parametrize(
    "expression", ["eval(text)", "exec(text)", "globals().values()", "__import__(text)"]
)
def test_unsafe_namespace_behavior_is_diagnostic(tmp_path, expression):
    with pytest.raises(ResolutionError):
        build(tmp_path, {"a": f"def run(text): return {expression}\n"}, [("a", "run")])


def test_colliding_bindings_are_not_silently_overwritten(tmp_path):
    with pytest.raises(ResolutionError, match="collision"):
        build(
            tmp_path,
            {
                "a": "VALUE=1\ndef a(): return VALUE\n",
                "b": "VALUE=2\ndef b(): return VALUE\n",
            },
            [("a", "a"), ("b", "b")],
        )


def test_external_reader_collision_is_checked_with_embedded_bindings(tmp_path):
    selection, _ = build(tmp_path, {"a": "class Reader: pass\n"}, [("a", "Reader")])
    with pytest.raises(ResolutionError, match="collision"):
        render_symbols(selection, external_imports={"from external import Reader"})


def test_repeated_class_definitions_are_rejected(tmp_path):
    with pytest.raises(ResolutionError, match="Repeated class definition"):
        build(tmp_path, {"a": "class A: pass\nclass A: pass\n"}, [("a", "A")])


def test_annotations_keep_eager_and_postponed_semantics(tmp_path):
    _, source = build(
        tmp_path,
        {
            "a": "from __future__ import annotations\nclass A: pass\ndef a(x: A): return x\n",
            "b": "class B: pass\ndef b(x: B): return x\n",
        },
        [("a", "a"), ("b", "b")],
    )
    ns = execute(source)
    assert ns["a"].__annotations__["x"] == "A"
    assert ns["b"].__annotations__["x"] is ns["B"]


def test_module_is_parsed_once_and_only_when_reached(tmp_path, monkeypatch):
    (tmp_path / "a.py").write_text(
        "def helper(): return 42\ndef a(): return helper()\ndef b(): return helper()\n"
    )
    (tmp_path / "unused.py").write_text("this is invalid python !")
    parse = ast.parse
    paths = []

    def tracked(source, filename="<unknown>", *args, **kwargs):
        paths.append(filename)
        return parse(source, filename, *args, **kwargs)

    monkeypatch.setattr(ast, "parse", tracked)
    resolver = SymbolResolver(tmp_path)
    for name in ("a", "b"):
        resolver.require(SymbolRef("a", name))
    render_symbols(resolver.drain())
    assert paths.count(str(tmp_path / "a.py")) == 1
    assert set(resolver.index.modules) == {"a"}


def test_new_utility_needs_no_dependency_list(tmp_path):
    (tmp_path / "new.py").write_text(
        "def helper(): return 42\n"
        "class NewUtility:\n"
        "    utility_name = 'new'\n"
        "    def work(self): return helper()\n"
        "    def unused(self): return -1\n"
    )
    resolver = SymbolResolver(tmp_path, {"new": SymbolRef("new", "NewUtility")})
    resolver.context_member("new", "work")
    embedded = render_symbols(resolver.drain())
    source = "\n".join(embedded.sources)
    assert execute(source)["NewUtility"]().work() == 42
    assert "'new': NewUtility()" in embedded.context_expression
    assert "unused" not in source
