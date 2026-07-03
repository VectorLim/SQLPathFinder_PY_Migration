"""Registry and direct-emission helpers for embeddable utility classes."""

from __future__ import annotations

import inspect
import re
from dataclasses import dataclass
from typing import Any, Callable

from vg2c.emitter.semtypes import (
    Crosstab,
    Header,
    OutputPath,
    RawExpr,
    SourceType,
    SqlText,
    TableInputs,
    WriteFileTemplate,
    option_to_python_expr,
)
from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.frontend.models import Kind
from vg2c.resolver.models import RowsInFile

__all__ = [
    "UTILITIES",
    "UTILITY_IMPORTS",
    "UTILITY_DEPENDENCIES",
    "CLASS_TO_UTILITY_NAME",
    "UtilityCommandMatch",
    "classify_utility_command",
    "mark_utility_used",
    "emit_block",
    "assemble_registered_utilities",
    "get_registered_source",
    "register_utility",
]

# Registry of utility classes keyed by name
UTILITIES: dict[str, type[UtilitySpec]] = {}

# Registry of imports for each utility
UTILITY_IMPORTS: dict[str, tuple[str, ...]] = {}

# Registry of dependencies for each utility
UTILITY_DEPENDENCIES: dict[str, tuple[str, ...]] = {}

# Optional block kind ownership metadata per utility name.
UTILITY_HANDLES: dict[str, tuple[Kind, ...]] = {}

# Reverse map for class-typed utilities.
CLASS_TO_UTILITY_NAME: dict[type, str] = {}


@dataclass(frozen=True, slots=True)
class UtilityCommandMatch:
    shape: str
    argv: tuple[str, ...]
    utility_cls: type[UtilitySpec] | None


def register_utility(
    cls: type[UtilitySpec] | None = None,
    *,
    name: str | None = None,
    imports: tuple[str, ...] | None = None,
    depends_on: tuple[str, ...] | None = None,
    handles: Kind | tuple[Kind, ...] | None = None,
) -> type[UtilitySpec] | Callable[[type[UtilitySpec]], type[UtilitySpec]]:
    """Register one utility class from decorator args or class metadata."""

    def _register(target: type[UtilitySpec]) -> type[UtilitySpec]:
        if not inspect.isclass(target) or not issubclass(target, UtilitySpec):
            raise TypeError("register_utility expects a UtilitySpec subclass")

        reg_name = (name or target.utility_name).strip()
        if not reg_name:
            raise ValueError(f"{target.__name__}: utility_name must be non-empty")
        if reg_name in UTILITIES:
            raise ValueError(f"duplicate utility_name: {reg_name}")

        reg_imports = tuple(imports if imports is not None else target.utility_imports)
        reg_deps = tuple(
            depends_on if depends_on is not None else target.utility_dependencies
        )

        UTILITIES[reg_name] = target
        UTILITY_IMPORTS[reg_name] = reg_imports
        UTILITY_DEPENDENCIES[reg_name] = reg_deps
        CLASS_TO_UTILITY_NAME[target] = reg_name

        if handles is not None:
            if isinstance(handles, tuple):
                UTILITY_HANDLES[reg_name] = handles
            else:
                UTILITY_HANDLES[reg_name] = (handles,)

        setattr(target, "__vg2c_registered_name__", reg_name)
        return target

    if cls is None:
        return _register
    return _register(cls)


def classify_utility_command(utilities_string: str) -> UtilityCommandMatch:
    text = utilities_string.strip()
    if not text:
        return UtilityCommandMatch(shape="unknown", argv=(), utility_cls=None)

    argv = tuple(text.split())
    if not argv:
        return UtilityCommandMatch(shape="unknown", argv=(), utility_cls=None)

    first = argv[0]
    basename = first.split("/")[-1].split("\\")[-1].lower()

    for cls in UTILITIES.values():
        for shape, markers in cls.utility_command_contains:
            if any(marker in basename for marker in markers):
                return UtilityCommandMatch(shape=shape, argv=argv, utility_cls=cls)
    for cls in UTILITIES.values():
        for shape, suffixes in cls.utility_command_suffixes:
            if any(basename.endswith(suffix) for suffix in suffixes):
                return UtilityCommandMatch(shape=shape, argv=argv, utility_cls=cls)

    return UtilityCommandMatch(shape="unknown", argv=argv, utility_cls=None)


def mark_utility_used(ctx: Any, *names: str) -> None:
    for name in names:
        if name not in UTILITIES:
            raise KeyError(f"Unknown utility: {name}")
        if name in ctx.needed_utilities:
            continue
        ctx.needed_utilities.add(name)
        for dep in UTILITY_DEPENDENCIES.get(name, ()):
            mark_utility_used(ctx, dep)


def assemble_registered_utilities(ctx) -> tuple[list[str], list[str]]:
    if not ctx.needed_utilities:
        return ([], [])

    ordered_keys: list[str] = []
    seen: set[str] = set()

    def _visit(key: str) -> None:
        if key in seen:
            return
        seen.add(key)
        for dep in UTILITY_DEPENDENCIES.get(key, ()):  # already validated in register
            _visit(dep)
        ordered_keys.append(key)

    for key in sorted(ctx.needed_utilities):
        _visit(key)

    imports: list[str] = []
    sources: list[str] = []
    for key in ordered_keys:
        imports.extend(UTILITY_IMPORTS[key])
        sources.append(get_registered_source(key))
    return imports, sources


def get_registered_source(name: str) -> str:
    cls = UTILITIES[name]
    source = inspect.getsource(cls)
    return _strip_embed_artifacts(source, cls.__name__)


_CLASS_SIG_RE = re.compile(r"^(\s*class\s+\w+)\(.*\):\s*$")


def _strip_embed_artifacts(source: str, class_name: str) -> str:
    lines = source.split("\n")

    while lines and lines[0].lstrip().startswith("@"):
        lines.pop(0)

    if not lines:
        return ""

    lines[0] = _CLASS_SIG_RE.sub(r"\1:", lines[0])
    lines[0] = lines[0].replace("(UtilitySpec):", ":")
    lines[0] = lines[0].replace(f"({class_name}, UtilitySpec):", f"({class_name}):")

    return "\n".join(lines).rstrip()


def _render_value(value: Any) -> str:
    if isinstance(value, RawExpr):
        return value.source
    return repr(value)


def render_method_call(
    ctx: Any,
    utility_name: str,
    method_name: str,
    *,
    args: tuple[Any, ...] = (),
    kwargs: dict[str, Any] | None = None,
) -> str:
    mark_utility_used(ctx, utility_name)
    receiver = "ctx" if utility_name == "ctx" else f"ctx.{utility_name}"
    parts: list[str] = [_render_value(arg) for arg in args]
    for key, value in (kwargs or {}).items():
        parts.append(f"{key}={_render_value(value)}")
    return f"{receiver}.{method_name}({', '.join(parts)})"


def _step_name(block, suffix: str) -> str:
    return f"step_{block.parsed.index:04d}_{suffix}"


def _emit_step_source(name: str, body_lines: list[str]) -> tuple[str, str]:
    lines = [f"def {name}(ctx) -> None:"]
    if body_lines:
        lines.extend([f"    {line}" for line in body_lines])
    else:
        lines.append("    pass")
    return "\n".join(lines), f"{name}(ctx)"


def _emit_rows_in_file(ctx: Any, block) -> tuple[str, str]:
    payload = block.control_payload
    if not isinstance(payload, RowsInFile):
        return _emit_step_source(_step_name(block, "macro_control"), ["pass"])

    csv_path_expr = option_to_python_expr(payload.csv_path)
    set_name = payload.var_name.upper()
    stmt = (
        f"ctx.macro.set_named({set_name!r}, "
        f"str(ctx.csv_io.row_count({csv_path_expr})))"
    )
    mark_utility_used(ctx, "macro", "csv_io")
    return _emit_step_source(_step_name(block, "rows_in_file"), [stmt])


def _emit_sql_like(ctx: Any, block, dispatched, sqlite: bool) -> tuple[str, str]:
    if dispatched is None:
        raise ValueError("SQL emission requires dispatch metadata")

    sql = SqlText.extract(block, dispatched)
    output = OutputPath.extract(block, dispatched)
    source_type = "sqlite" if sqlite else SourceType.extract(block, dispatched)
    crosstab = Crosstab.extract(block, dispatched)
    header = None if crosstab else Header.extract(block, dispatched)

    kwargs: dict[str, Any] = {
        "sql": sql,
        "output": output,
        "source_type": source_type,
    }
    if sqlite:
        kwargs["inputs"] = TableInputs.extract(block, dispatched)
    if header:
        kwargs["header"] = header
    if crosstab:
        kwargs["crosstab"] = crosstab

    stmt = render_method_call(ctx, "ctx", "run_query", kwargs=kwargs)
    suffix = "sqlite_query" if sqlite else "sql_query"
    return _emit_step_source(_step_name(block, suffix), [stmt])


def _emit_write_file(ctx: Any, block) -> tuple[str, str]:
    stmt = render_method_call(
        ctx,
        "ctx",
        "write_file",
        kwargs={
            "path": OutputPath.extract(block, None),
            "template": WriteFileTemplate.extract(block, None),
        },
    )
    return _emit_step_source(_step_name(block, "write_file"), [stmt])


def _utility_call_for_shape(ctx: Any, match: UtilityCommandMatch) -> str | None:
    shape = match.shape
    argv = list(match.argv)

    if shape in {"run-python-script", "bat-file", "exe-direct"}:
        expr_items = [option_to_python_expr(token) for token in argv]
        argv_expr = RawExpr("[" + ", ".join(expr_items) + "]")
        return render_method_call(
            ctx,
            "external",
            "run",
            kwargs={"argv": argv_expr},
        )

    if shape == "robocopy":
        # RoboCopy.va arg layout: <file_name> <source_dir> <dest_dir> [...]
        file_name = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        source_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        dest_dir = option_to_python_expr(argv[3]) if len(argv) > 3 else repr(".")
        src_expr = RawExpr(f"str(Path({source_dir}) / {file_name})")
        dst_expr = RawExpr(dest_dir)
        return render_method_call(
            ctx,
            "fs_ops",
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    if shape == "spf-copy":
        # SPFCopy.bat arg layout: <source_path> <dest_dir> [recurse]
        src = option_to_python_expr(argv[1]) if len(argv) > 1 else repr("")
        dst_dir = option_to_python_expr(argv[2]) if len(argv) > 2 else repr(".")
        src_expr = RawExpr(src)
        dst_expr = RawExpr(f"str(Path({dst_dir}) / Path({src}).name)")
        return render_method_call(
            ctx,
            "fs_ops",
            "copy",
            kwargs={"src": src_expr, "dst": dst_expr},
        )

    if shape == "spf-delete":
        raw = argv[1] if len(argv) > 1 else ""
        items = [p.strip() for p in raw.split(",") if p.strip()]
        paths_expr = RawExpr(
            "[" + ", ".join(option_to_python_expr(p) for p in items) + "]"
        )
        return render_method_call(
            ctx,
            "fs_ops",
            "delete",
            kwargs={"paths": paths_expr},
        )

    if shape == "email":
        return None

    return None


def _emit_utility(ctx: Any, block) -> tuple[str, str]:
    utilities_str = block.resolved_options.lookup.get("UTILITIES", "")
    match = classify_utility_command(utilities_str)
    stmt = _utility_call_for_shape(ctx, match)
    if stmt is None:
        return _emit_step_source(
            _step_name(block, "utility"),
            [f"pass  # TODO: utility shape not translated: {match.shape}"],
        )
    return _emit_step_source(_step_name(block, "utility"), [stmt])


def emit_block(ctx: Any, block, dispatched) -> tuple[str, str]:
    if block.kind is Kind.MACRO_CONTROL:
        return _emit_rows_in_file(ctx, block)
    if block.kind is Kind.SQL_QUERY:
        return _emit_sql_like(ctx, block, dispatched, sqlite=False)
    if block.kind is Kind.SQLITE_QUERY:
        return _emit_sql_like(ctx, block, dispatched, sqlite=True)
    if block.kind is Kind.WRITE_FILE:
        return _emit_write_file(ctx, block)
    if block.kind is Kind.UTILITY:
        return _emit_utility(ctx, block)
    if block.kind is Kind.HTML_REPORT:
        return _emit_step_source(
            _step_name(block, "html_report"),
            ["pass  # HTML report not translated"],
        )
    return _emit_step_source(
        _step_name(block, "unknown"),
        [f"pass  # TODO: unhandled kind={block.kind}"],
    )
