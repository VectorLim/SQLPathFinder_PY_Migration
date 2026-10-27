"""Emit direct Python and editable assets from the existing scope tree."""

from __future__ import annotations

import ast
from collections import Counter

from vg2c.emitter.indent_writer import IndentWriter
from vg2c.emitter.models import BlockEmission, EmittedScope, EmittedScript, SourceRange
from vg2c.emitter.source_map import finalize_steps, record_inline_step
from vg2c.kind import Kind
from vg2c.logger import Logger
from vg2c.runtime.environment import DEFAULT_TEST_MODE_EXCLUDED
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec
from vg2c.utilities.csr import CsrWorkflow
from vg2c.utilities.fs_ops import FileSystemOps
from vg2c.utilities.html_report import HtmlReport
from vg2c.utilities.rows_in_file import inline_row_count_conditions

log = Logger.getLogger(__name__)


def emit_project(dispatched):
    ensure_utility_checks_loaded()
    blocks = {block.index: block for block in dispatched.resolved.blocks}
    blocks.update({block.index: block for block in dispatched.dispatched})
    html_report = HtmlReport(blocks.values())
    csr_workflow = CsrWorkflow(dispatched.resolved.csr_submissions, html_report)
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
    assets = {}
    steps = []
    scope_lines = []
    imports = {"from pathlib import Path"}
    has_state = any(
        block.kind in {Kind.MACRO_CONTROL, Kind.ROWS_IN_FILE, Kind.HTML_REPORT}
        or "<<<" in block.resolved_body
        or any("<<<" in value for value in block.resolved_options.lookup.values())
        for block in blocks.values()
    )
    runtime_imports = {"JobRuntime"}
    writer = IndentWriter()
    writer.write("def run():")
    writer.push_indent()
    writer.write(
        "job = JobRuntime.from_script(__file__, test_mode_excluded=TEST_MODE_EXCLUDED)"
    )
    body_start = len(writer.lines)
    if has_state:
        writer.write("macros = job.macros")

    def walk(node, macros="macros"):
        if node.kind == "leaf":
            block = blocks[node.block_index]
            if block.index in csr_workflow.producers:
                return  # The CSR submission reads the producer's input directly.
            writer.write(f"# Source block {block.index}, line {block.span.start_line}")
            if block.index in inlined_counts:
                return  # The next IF header calls job.row_count directly.
            try:
                reason = None
                if csr_workflow.handles_block(block):
                    emission = csr_workflow.emit_block(block, macros=macros)
                elif block.kind is Kind.UNKNOWN:
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
                        external_parameters=emission.external_parameters,
                        functional_kind=(
                            "UNSUPPORTED"
                            if skipped
                            else (
                                "CSR_SUBMISSION"
                                if block.index in csr_workflow.submissions
                                else None
                            )
                        ),
                    )
                )
            except (ValueError, SyntaxError) as exc:
                raise ValueError(
                    f"{block.span.file or '<input>'}:{block.span.start_line}:1 "
                    f"(block {block.index}): {exc}"
                ) from exc
            return

        start_line = len(writer.lines)
        if not csr_workflow.emit_scope(
            writer,
            walk,
            node,
            macros=macros,
            imports=imports,
            runtime_imports=runtime_imports,
        ):
            node.emit(
                writer,
                walk,
                macros=macros,
                imports=imports,
                runtime_imports=runtime_imports,
            )
        if node.control_payload is not None and len(writer.lines) > start_line:
            scope_lines.append(
                (node.start_index, node.end_index, start_line, len(writer.lines))
            )

    walk(scope_tree)
    if not any(
        line.strip() and not line.lstrip().startswith("#")
        for line in writer.lines[body_start:]
    ):
        writer.write("pass")
    writer.pop_indent()
    if runtime_imports:
        imports.add("from vg2c.runtime import " + ", ".join(sorted(runtime_imports)))
    reader_imports = sorted(
        item for item in imports if item.startswith("from datasyncx ")
    )
    ordinary_imports = sorted(imports - set(reader_imports))
    setup = "\nOracleClient.configure()\n" if reader_imports else ""
    exclusions = ", ".join(repr(name) for name in sorted(DEFAULT_TEST_MODE_EXCLUDED))
    header = (
        "\n".join(ordinary_imports)
        + setup
        + "\n".join(reader_imports)
        + f"\n\nTEST_MODE_EXCLUDED = {{{exclusions}}}\n\n"
    )
    source = header + writer.source() + '\n\nif __name__ == "__main__":\n    run()\n'
    ast.parse(source)
    finalized = finalize_steps(source, steps)
    offsets = [len(header)]
    for line in writer.lines:
        offsets.append(offsets[-1] + len(line) + 1)
    scopes = tuple(
        EmittedScope(start, end, SourceRange(offsets[first], offsets[last] - 1))
        for start, end, first, last in scope_lines
    )
    return EmittedScript(
        source=source,
        imports=tuple(sorted(imports)),
        steps=tuple(finalized),
        assets=tuple(assets.items()),
        scopes=scopes,
    )
