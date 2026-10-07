"""One direct emission path targeting the five Stage 1 facade objects."""

from __future__ import annotations

import ast
import re

from scripthost_portable.api_contract import QUERY_OPTIONS, REPORT_OPTIONS
from vg2c.diagnostics import fail
from vg2c.emitter.globals import render_sql
from vg2c.emitter.indent_writer import IndentWriter
from vg2c.emitter.literals import string_literal
from vg2c.emitter.models import (
    EmittedBlock,
    EmittedInvocation,
    EmittedParameter,
    EmittedScript,
    SourceRange,
)
from vg2c.frontend.models import ClassifiedBlock
from vg2c.kind import Kind
from vg2c.operands import IfThen, ScopeNode, StartMacro, utility_arguments
from vg2c.resolver.models import ResolvedProgram

_POSITIONAL_NAMES = {
    "macros.load_csv": ("path",),
    "macros.compare": ("lhs", "operator", "rhs"),
    "utilities.rows_in_file": ("path", "variable"),
    "aed.process": ("path",),
    "reports.run": ("template",),
    "reports.defer": ("template",),
    "reports.layout": ("template",),
}
_MACRO_VALUE = re.compile(r"<<<([^<>]+)>>>")
_API_IMPORT = "from scripthost_portable.script_api import aed, macros, query, reports, utilities"


def emit(program: ResolvedProgram) -> EmittedScript:
    if not program.blocks:
        raise ValueError("Cannot compile an empty job.")
    # Validate every block, including structural end tokens, before writing source.
    for block in program.blocks:
        _validate_options(block)
    writer = IndentWriter()
    writer.write("def run():")
    writer.push_indent()
    records: dict[int, ClassifiedBlock] = {}
    constants: dict[str, str] = {}
    blocks = {block.index: block for block in program.blocks}

    def call(block, operation, positional=(), keywords=(), *, condition=False):
        records[writer.line_number] = block
        if condition:
            args = [*positional, *(f"{key}={value}" for key, value in keywords)]
            writer.write(f"if {operation}({', '.join(args)}):")
        elif not keywords and all("\n" not in value for value in positional):
            writer.write(f"{operation}({', '.join(positional)})")
        else:
            writer.write(f"{operation}(")
            writer.push_indent()
            for value in positional:
                writer.write(value + ",")
            for key, value in keywords:
                writer.write(f"{key}={value},")
            writer.pop_indent()
            writer.write(")")

    def walk(node: ScopeNode):
        if node.kind == "program":
            for child in node.children:
                walk(child)
            return
        block = blocks[node.start_index]
        payload = node.control_payload
        if isinstance(payload, StartMacro):
            keywords = (("continue_on_error", "True"),) if payload.continue_on_error else ()
            call(block, "macros.load_csv", (_value(payload.csv_path),), keywords, condition=True)
        elif isinstance(payload, IfThen):
            # The original IF task resolves these arguments; do not pre-evaluate them.
            call(
                block,
                "macros.compare",
                tuple(repr(v) for v in (payload.lhs, payload.op, payload.rhs)),
                condition=True,
            )
        elif block.kind in {Kind.SQL_QUERY, Kind.SQLITE_QUERY}:
            options = block.options.lookup
            if not options.get("CSV") or not block.body.strip():
                fail("query-arguments", "A query needs /CSV and a nonempty SQL body.", block)
            keywords = [("sql", render_sql(block.body, constants))]
            keywords.append(("engine", repr("VA" if block.kind is Kind.SQL_QUERY else "SQLite")))
            keywords.extend(
                (QUERY_OPTIONS[key], _value(value))
                for key, value in options.items()
                if key != "ENGINE"
            )
            call(block, "query.run", keywords=keywords)
        elif block.kind is Kind.HTML_REPORT:
            options = block.options.lookup
            report = options["REPORT"].upper().removeprefix("HTML-").lower()
            keywords = [
                (REPORT_OPTIONS[key], _value(value))
                for key, value in options.items()
                if key not in {"REPORT", "ID"}
            ]
            if report == "delete":
                if block.body.strip() not in {"", "N/A"}:
                    fail("report-body", "HTML-DELETE has no report template.", block)
                call(block, "reports.delete", keywords=keywords)
            else:
                if not block.body.strip():
                    fail("report-body", "A report needs a nonempty template.", block)
                if report == "defer":
                    if not options.get("ID"):
                        fail("report-id", "HTML-DEFER needs /ID.", block)
                    keywords.insert(0, ("report_id", _value(options["ID"])))
                call(block, f"reports.{report}", (string_literal(block.body),), keywords)
        elif block.kind is Kind.ROWS_IN_FILE:
            args = utility_arguments(block)
            if len(args) not in {2, 3, 4} or not all(args[:2]):
                fail("row-count-arguments", "ROWS-IN-FILE needs a path and variable.", block)
            if (len(args) > 2 and args[2].upper() != "N") or (len(args) > 3 and args[3]):
                fail(
                    "row-count-options",
                    "Count limits and archive names are unsupported by Stage 1.",
                    block,
                )
            call(block, "utilities.rows_in_file", (_value(args[0]), repr(args[1])))
        elif block.kind is Kind.AED:
            args = utility_arguments(block)
            if len(args) != 1 or not args[0]:
                fail("aed-arguments", "AED needs exactly one CSV path.", block)
            call(block, "aed.process", (_value(args[0]),))
        else:
            fail("unsupported-block", "No direct API mapping for this block.", block)
        if node.children:
            writer.push_indent()
            for child in node.children:
                walk(child)
            writer.pop_indent()

    walk(program.scope_tree)
    writer.pop_indent()
    header = _API_IMPORT + "\n\n"
    if constants:
        header += "".join(f"{name} = {value!r}\n" for name, value in constants.items()) + "\n\n"
    source = header + writer.source() + (
        '\n\nif __name__ == "__main__":\n'
        '    raise SystemExit("Use python -m scripthost_portable.launcher '
        '<job.py> --workdir <directory>.")\n'
    )
    shift = header.count("\n")
    records = {line + shift: block for line, block in records.items()}
    return EmittedScript(source, (_API_IMPORT,), _metadata(source, records))


def _value(value: str) -> str:
    match = _MACRO_VALUE.fullmatch(value)
    return f"macros[{match.group(1)!r}]" if match else repr(value)


def _validate_options(block: ClassifiedBlock) -> None:
    options = block.options.lookup
    if block.kind in {Kind.SQL_QUERY, Kind.SQLITE_QUERY}:
        allowed = QUERY_OPTIONS.keys()
    elif block.kind is Kind.HTML_REPORT:
        report = options["REPORT"].upper()
        allowed = (
            {"REPORT", "INSTANCE"} if report == "HTML-DELETE" else {"REPORT", *REPORT_OPTIONS}
        )
        if report == "HTML-DEFER":
            allowed.add("ID")
    else:
        # These task wrappers are unused by the selected original utility commands,
        # except PROMPT-TEXT, which changes console logging only. Stage 1 has no prompt.
        allowed = {"UTILITIES", "WORKDIR", "INSTANCE", "OUTLOOK", "PROMPT-TEXT"}
        if options.get("WORKDIR", ".\\") not in {".\\", "./", "."}:
            fail("utility-workdir", "Only the job's current directory is supported.", block)
        if options.get("OUTLOOK", "N").upper() != "N":
            fail(
                "utility-outlook",
                "Only the current utility /OUTLOOK=N wrapper is supported.",
                block,
            )
        if block.body.strip():
            fail("utility-body", "Utility/control blocks cannot carry an ignored body.", block)
    unknown = options.keys() - allowed
    if unknown:
        fail(
            "unsupported-option",
            "Unsupported options: " + ", ".join("/" + k for k in sorted(unknown)),
            block,
        )
    for value in options.values():
        if "<OPTIONS>" in value or "</OPTIONS>" in value:
            fail("option-block-token", "Option values cannot contain legacy block tokens.", block)
    if "<OPTIONS>" in block.body or "</OPTIONS>" in block.body:
        fail("body-block-token", "Bodies cannot contain legacy options blocks.", block)


def _metadata(source: str, records: dict[int, ClassifiedBlock]) -> tuple[EmittedBlock, ...]:
    tree = ast.parse(source)
    lines = source.splitlines(keepends=True)
    offsets = [0]
    for line in lines:
        offsets.append(offsets[-1] + len(line))

    def source_range(node):
        # AST columns count UTF-8 bytes; metadata ranges count Python characters.
        start = len(lines[node.lineno - 1].encode("utf-8")[: node.col_offset].decode("utf-8"))
        end = len(lines[node.end_lineno - 1].encode("utf-8")[: node.end_col_offset].decode("utf-8"))
        return SourceRange(offsets[node.lineno - 1] + start, offsets[node.end_lineno - 1] + end)

    emitted = []
    calls = sorted(
        (n for n in ast.walk(tree) if isinstance(n, ast.Call)),
        key=lambda n: (n.lineno, n.col_offset),
    )
    for node in calls:
        if node.lineno not in records or not isinstance(node.func, ast.Attribute):
            continue
        if not isinstance(node.func.value, ast.Name):
            continue
        operation = f"{node.func.value.id}.{node.func.attr}"
        if node.func.value.id not in {"macros", "query", "reports", "utilities", "aed"}:
            continue
        block = records[node.lineno]
        invocation_id = f"block-{block.index}:{operation}"
        parameters = []
        arguments = [
            (name, position, value)
            for position, (name, value) in enumerate(
                zip(_POSITIONAL_NAMES.get(operation, ()), node.args)
            )
        ]
        arguments += [(arg.arg, None, arg.value) for arg in node.keywords]
        for name, position, value in arguments:
            span = source_range(value)
            text = source[span.start_offset : span.end_offset]
            try:
                literal = ast.literal_eval(value)
            except (ValueError, TypeError):
                literal = None
            parameters.append(
                EmittedParameter(f"{invocation_id}:{name}", name, position, text, literal, span)
            )
        span = source_range(node)
        invocation = EmittedInvocation(invocation_id, operation, span, tuple(parameters))
        emitted.append(
            EmittedBlock(
                block.index,
                block.kind.value,
                source[span.start_offset : span.end_offset],
                span,
                (invocation,),
                block.span,
            )
        )
    return tuple(emitted)
