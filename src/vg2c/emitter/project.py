"""Emit direct Python and editable assets from the existing scope tree."""

from __future__ import annotations

import ast

from vg2c.emitter.indent_writer import IndentWriter
from vg2c.emitter.models import EmittedScope, EmittedScript, SourceRange
from vg2c.emitter.source_map import finalize_steps, record_inline_step
from vg2c.kind import Kind
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec
from vg2c.utilities.csr import CsrWorkflow
from vg2c.utilities.html_report import HtmlReport


def emit_project(dispatched):
    ensure_utility_checks_loaded()
    blocks = {block.index: block for block in dispatched.resolved.blocks}
    blocks.update({block.index: block for block in dispatched.dispatched})
    html_report = HtmlReport(blocks.values())
    csr_workflow = CsrWorkflow(dispatched.resolved.csr_submissions, blocks, html_report)
    handlers = {
        kind: handler.emit_block
        for kind, handler in UtilitySpec._emit_handlers.items()
    }
    handlers[Kind.HTML_REPORT] = html_report.emit_block
    assets = {}
    steps = []
    scope_lines = []
    imports = {"from pathlib import Path"}
    has_state = any(block.kind in {Kind.MACRO_CONTROL, Kind.ROWS_IN_FILE, Kind.HTML_REPORT}
                    or "<<<" in block.resolved_body or any("<<<" in value for value in block.resolved_options.lookup.values())
                    for block in blocks.values())
    runtime_imports = {"JobRuntime"}
    writer = IndentWriter()
    writer.write("def run():")
    writer.push_indent()
    writer.write("job = JobRuntime.from_script(__file__)")
    body_start = len(writer.lines)
    if has_state:
        writer.write("macros = job.macros")

    def walk(node, macros="macros"):
        if node.kind == "leaf":
            block = blocks[node.block_index]
            try:
                writer.write(f"# Source block {block.index}, line {block.span.start_line}")
                if csr_workflow.handles_block(block):
                    emission = csr_workflow.emit_block(block, macros=macros)
                elif block.kind is Kind.UNKNOWN:
                    if block.resolved_options.lookup.get("JSL", "").upper() == "Y":
                        raise ValueError("JMP/JSL blocks are outside the supported core")
                    raise ValueError("Unclassified utility is unsupported")
                else:
                    handler = handlers.get(block.kind)
                    if handler is None:
                        raise ValueError(f"No compiler handler for {block.kind.value}")
                    emission = handler(block, macros=macros)
                if not emission.lines:
                    raise ValueError("Unsupported utility operation")
                imports.update(emission.imports)
                runtime_imports.update(emission.runtime_exports)
                assets.update(emission.assets)
                steps.append(record_inline_step(
                    writer,
                    block,
                    emission.lines,
                    external_parameters=emission.external_parameters,
                    functional_kind=(
                        "CSR_SUBMISSION" if block.index in csr_workflow.submissions else None
                    ),
                ))
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
            scope_lines.append((node.start_index, node.end_index, start_line, len(writer.lines)))

    walk(dispatched.resolved.scope_tree)
    if not any(line.strip() and not line.lstrip().startswith("#")
               for line in writer.lines[body_start:]):
        writer.write("pass")
    writer.pop_indent()
    if runtime_imports:
        imports.add("from vg2c.runtime import " + ", ".join(sorted(runtime_imports)))
    reader_imports = sorted(item for item in imports if item.startswith("from datasyncx "))
    ordinary_imports = sorted(imports - set(reader_imports))
    setup = "\nOracleClient.configure()\n" if reader_imports else ""
    header = "\n".join(ordinary_imports) + setup + "\n".join(reader_imports) + "\n\n"
    header += csr_workflow.settings_source()
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
    return EmittedScript(source=source, imports=tuple(sorted(imports)), steps=tuple(finalized),
                         assets=tuple(assets.items()), scopes=scopes)
