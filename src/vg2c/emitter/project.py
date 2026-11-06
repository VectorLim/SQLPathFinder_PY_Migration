"""Emit direct Python and editable assets from the existing scope tree."""

from __future__ import annotations

import ast
import csv
import re
from collections import Counter
from importlib.resources import files
from itertools import groupby

from vg2c.emitter.indent_writer import IndentWriter
from vg2c.emitter.models import BlockEmission, EmittedScope, EmittedScript, SourceRange
from vg2c.emitter.source_map import finalize_steps, record_inline_step
from vg2c.kind import Kind
from vg2c.logger import Logger
from vg2c.project_bundle import HOST_HANDLER, RUNTIME_BOOTSTRAP
from vg2c.runtime.environment import DEFAULT_TEST_MODE_EXCLUDED
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec
from vg2c.utilities.fs_ops import FileSystemOps
from vg2c.utilities.html_report import HtmlReport
from vg2c.utilities.rows_in_file import inline_row_count_conditions

log = Logger.getLogger(__name__)


def emit_project(dispatched):
    ensure_utility_checks_loaded()
    blocks = {block.index: block for block in dispatched.resolved.blocks}
    blocks.update({block.index: block for block in dispatched.dispatched})
    html_report = HtmlReport(blocks.values())
    handlers = {
        kind: handler.emit_block for kind, handler in UtilitySpec._emit_handlers.items()
    }
    handlers[Kind.HTML_REPORT] = html_report.emit_block
    file_names = Counter(
        FileSystemOps.asset_name(block).casefold()
        for block in blocks.values()
        if block.kind is Kind.WRITE_FILE
    )

    def emit_file(block, *, macros):
        asset = FileSystemOps.asset_name(block)
        if file_names[asset.casefold()] > 1:
            asset = f"files/step_{block.index:04d}/" + asset.removeprefix("files/")
        return FileSystemOps.emit_block(block, macros=macros, file_asset=asset)

    if Kind.WRITE_FILE in handlers:
        handlers[Kind.WRITE_FILE] = emit_file
    scope_tree, inlined_counts = inline_row_count_conditions(
        dispatched.resolved.scope_tree, blocks
    )
    certificate = "IntelSHA256RootCA-Base64.crt"
    assets = {
        certificate: files("vg2c.runtime")
        .joinpath(certificate)
        .read_text(encoding="ascii")
    }
    scope_lines = []
    imports = {"from pathlib import Path"}
    has_state = any(
        block.kind in {Kind.MACRO_CONTROL, Kind.ROWS_IN_FILE, Kind.HTML_REPORT}
        or "<<<" in block.resolved_body
        or any("<<<" in value for value in block.resolved_options.lookup.values())
        for block in blocks.values()
    )
    runtime_imports = {"JobRuntime"}
    main_writer = IndentWriter()
    helper_writer = IndentWriter()
    main_steps = []
    helper_steps = []
    writer = main_writer
    steps = main_steps
    writer.write("def run():")
    writer.push_indent()
    writer.write(
        "job = JobRuntime.from_script(__file__, test_mode_excluded=TEST_MODE_EXCLUDED)"
    )
    writer.write('# job.environ["NAME"] = ""')
    writer.write('# job.environ["PW"] = ""')
    body_start = len(writer.lines)
    if has_state:
        writer.write("macros = job.macros")

    def walk(node, macros="macros", *, grouped=True):
        if grouped:
            if id(node) in grouped_siblings:
                return
            if id(node) in prepost_groups:
                root, members = prepost_groups[id(node)]
                emit_prepost(root, members, macros)
                return
        if node.kind == "leaf":
            block = blocks[node.block_index]
            writer.write(f"# Source block {block.index}, line {block.span.start_line}")
            if block.index in inlined_counts:
                return  # The next IF header calls job.row_count directly.
            try:
                reason = None
                if block.kind is Kind.UNKNOWN:
                    reason = (
                        "JMP/JSL blocks are outside the supported core"
                        if block.resolved_options.lookup.get("JSL", "").upper() == "Y"
                        else "Unclassified utility is unsupported"
                    )
                    emission = None
                else:
                    handler = handlers.get(block.kind)
                    if handler is None:
                        reason = f"No compiler handler for {block.kind.value}"
                        emission = None
                    else:
                        emission = handler(block, macros=macros)
                skipped = emission is None or not emission.lines
                if skipped:
                    reason = reason or "Unsupported utility operation"
                    log.warning(
                        "[skipped-block] %s:%s:1 (block %s): %s; skipped. "
                        "Dependent steps may require its missing outputs.",
                        block.span.file or "<input>",
                        block.span.start_line,
                        block.index,
                        reason,
                    )
                    emission = BlockEmission(
                        lines=("pass  # Skipped unsupported query block.",)
                    )
                imports.update(emission.imports)
                runtime_imports.update(emission.runtime_exports)
                assets.update(emission.assets)
                steps.append(
                    record_inline_step(
                        writer,
                        block,
                        emission.lines,
                        functional_kind="UNSUPPORTED" if skipped else None,
                    )
                )
            except (ValueError, SyntaxError) as exc:
                raise ValueError(
                    f"{block.span.file or '<input>'}:{block.span.start_line}:1 "
                    f"(block {block.index}): {exc}"
                ) from exc
            return

        start_line = len(writer.lines)
        node.emit(
            writer,
            walk,
            macros=macros,
            imports=imports,
            runtime_imports=runtime_imports,
        )
        if node.control_payload is not None and len(writer.lines) > start_line:
            scope_lines.append(
                (
                    node.start_index,
                    node.end_index,
                    start_line,
                    len(writer.lines),
                    writer is helper_writer,
                )
            )

    prepost_pattern = re.compile(
        r"^Step\s+([0-9]+)-[0-9]+(?:[.-][0-9A-Za-z]+)*\.(?:\s|$)"
    )

    def prepost_root(node):
        first = blocks[node.start_index].resolved_options.lookup.get("PROMPT-TEXT", "")
        match = prepost_pattern.match(first)
        if match is None:
            return None
        root = match.group(1)
        for index in range(node.start_index, node.end_index + 1):
            prompt = blocks[index].resolved_options.lookup.get("PROMPT-TEXT", "")
            if prompt:
                member = prepost_pattern.match(prompt)
                if member is None or member.group(1) != root:
                    return None
        return root

    prepost_groups = {}
    grouped_siblings = set()

    def find_groups(node):
        children = node.children
        roots = [prepost_root(child) for child in children]
        for position, child in enumerate(children):
            if position == 0 or roots[position] is not None or child.kind != "leaf":
                continue
            options = blocks[child.block_index].resolved_options.lookup
            if (
                options.get("PROMPT-TEXT")
                or options.get("DELETE", "").casefold() != "*instance*"
            ):
                continue
            previous = children[position - 1]
            prompt = blocks[previous.start_index].resolved_options.lookup.get(
                "PROMPT-TEXT", ""
            )
            root = roots[position - 1]
            if root is None or not re.search(r"-a[0-9]+\.(?:\s|$)", prompt):
                continue
            tables = {
                name.strip().casefold()
                for name in next(csv.reader([options.get("TABLE", "")]))
                if name.strip()
            }
            produced = set()
            group_start = position - 1
            while group_start > 0 and roots[group_start - 1] == root:
                group_start -= 1
            for member in children[group_start:position]:
                for index in range(member.start_index, member.end_index + 1):
                    source_options = blocks[index].resolved_options.lookup
                    if source_options.get("INSTANCE") == options.get("INSTANCE"):
                        produced.add(source_options.get("CSV", "").strip().casefold())
            if tables and tables <= produced:
                roots[position] = root
        position = 0
        while position < len(children):
            if roots[position] is not None:
                position += 1
                continue
            gap_start = position
            while (
                position < len(children)
                and roots[position] is None
                and not any(
                    blocks[index].resolved_options.lookup.get("PROMPT-TEXT")
                    for index in range(
                        children[position].start_index, children[position].end_index + 1
                    )
                )
            ):
                position += 1
            if (
                gap_start > 0
                and position < len(children)
                and roots[gap_start - 1] is not None
                and roots[gap_start - 1] == roots[position]
            ):
                roots[gap_start:position] = [roots[position]] * (position - gap_start)
            if position == gap_start:
                position += 1
        for root, entries in groupby(
            zip(roots, children, strict=True), key=lambda entry: entry[0]
        ):
            members = tuple(child for _, child in entries)
            if root is None:
                for member in members:
                    find_groups(member)
            else:
                prepost_groups[id(members[0])] = (root, members)
                grouped_siblings.update(id(member) for member in members[1:])

    find_groups(scope_tree)
    function_names = set()

    def emit_prepost(root, members, macros):
        nonlocal writer, steps
        caller_writer, caller_steps = writer, steps
        prompt = blocks[members[0].start_index].resolved_options.lookup["PROMPT-TEXT"]
        description = prompt[prepost_pattern.match(prompt).end() :]
        slug = re.sub(r"[^a-z0-9]+", "_", description.lower()).strip("_")
        base = f"prepost_{root}" + (f"_{slug}" if slug else "")
        name = base
        suffix = 2
        while name in function_names:
            name = f"{base}_{suffix}"
            suffix += 1
        function_names.add(name)
        arguments = "job, macros" if has_state else "job"
        call_arguments = f"job, {macros}" if has_state else "job"
        caller_writer.write(f"{name}({call_arguments})")
        writer = helper_writer
        steps = helper_steps
        writer.write(f"def {name}({arguments}):")
        writer.push_indent()
        function_start = len(writer.lines)
        for member in members:
            walk(member, grouped=False)
        if not any(
            line.strip() and not line.lstrip().startswith("#")
            for line in writer.lines[function_start:]
        ):
            writer.write("pass")
        writer.pop_indent()
        writer.write("")
        writer.write("")
        writer, steps = caller_writer, caller_steps

    walk(scope_tree)
    if not any(
        line.strip() and not line.lstrip().startswith("#")
        for line in writer.lines[body_start:]
    ):
        writer.write("pass")
    writer.pop_indent()
    helper_line_count = len(helper_writer.lines)
    writer = IndentWriter()
    writer.lines.extend(helper_writer.lines)
    writer.lines.extend(main_writer.lines)
    if runtime_imports:
        imports.add("from vg2c.runtime import " + ", ".join(sorted(runtime_imports)))
    reader_imports = sorted(
        item for item in imports if item.startswith("from datasyncx ")
    )
    ordinary_imports = sorted(
        imports - set(reader_imports) - {"from pathlib import Path"}
    )
    setup = "\nOracleClient.configure()\n" if reader_imports else ""
    exclusions = ", ".join(repr(name) for name in sorted(DEFAULT_TEST_MODE_EXCLUDED))
    header = (
        RUNTIME_BOOTSTRAP
        + "\n".join(ordinary_imports)
        + setup
        + "\n".join(reader_imports)
        + f"\n\nTEST_MODE_EXCLUDED = {{{exclusions}}}\n\n"
    )
    source = (
        header
        + writer.source()
        + HOST_HANDLER
        + '\n\nif __name__ == "__main__":\n    run()\n'
    )
    ast.parse(source)
    finalized = sorted(
        finalize_steps(source, helper_steps + main_steps),
        key=lambda step: step.block_index,
    )
    offsets = [len(header)]
    for line in writer.lines:
        offsets.append(offsets[-1] + len(line) + 1)
    scopes = tuple(
        EmittedScope(
            start,
            end,
            SourceRange(
                offsets[first + (0 if helper else helper_line_count)],
                offsets[last + (0 if helper else helper_line_count)] - 1,
            ),
        )
        for start, end, first, last, helper in scope_lines
    )
    return EmittedScript(
        source=source,
        steps=tuple(finalized),
        assets=tuple(assets.items()),
        scopes=scopes,
    )
