"""Emit direct Python and editable assets from the existing scope tree."""

from __future__ import annotations

import ast
import json
import logging
from dataclasses import replace
from html import escape
from pathlib import Path
import re

from vg2c.emitter.indent_writer import IndentWriter
from vg2c.emitter.models import (CodeExpr, EmittableOperation, EmittedScript, RenderedCall,
                                 SourceRange, StepEmission, EmittedParameter, _RelativeInvocation, finalize_steps)
from vg2c.kind import Kind
from vg2c.project_paths import project_name
from vg2c.operands import IfThen, StartMacro, RunLoop, ForLoop, SiteLoop
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec
from vg2c.utilities._emit_helpers import resolve_output_path, extract_crosstab_options, split_utility_command
from vg2c.utilities._runtime_helpers import strip_quotes
from vg2c.utilities.html_report import HtmlReport
from vg2c.runtime.html_format import build_css
from vg2c.utilities.sqlite_engine import SqliteEngine


class _Expressions(ast.NodeTransformer):
    def __init__(self, macros):
        self.macros = macros

    def visit_Call(self, node):
        node = self.generic_visit(node)
        name = ast.unparse(node.func)
        if name == "ctx.macro.named":
            key = ast.literal_eval(node.args[0])
            return ast.Subscript(value=ast.Name(id=self.macros, ctx=ast.Load()),
                                 slice=ast.Constant(value=key), ctx=ast.Load())
        if name == "ctx.macro.positional":
            raise ValueError("Positional macro <<>> has no supported source cursor")
        if name == "ctx.csv_io.row_count":
            node.func = ast.Attribute(value=ast.Name(id="job", ctx=ast.Load()), attr="row_count", ctx=ast.Load())
        return node


def _expression(source, macros):
    return ast.unparse(_Expressions(macros).visit(ast.parse(source, mode="eval").body))


def control_header(payload, scope_id, macros="macros"):
    """One native header renderer shared by emission and semantic edits."""
    if isinstance(payload, IfThen):
        return "if " + _expression(payload._build_condition_expr(), macros) + ":"
    if isinstance(payload, StartMacro):
        if not payload.csv_path:
            return f"with {macros}.scope():"
        return f"macro_row_{scope_id} = job.read_macro_row({payload.csv_path!r})"
    if isinstance(payload, RunLoop):
        return f"with closing(job.csv_chunks({payload.input_csv_path!r}, {payload.chunk_csv_path!r}, {payload.chunk_size})) as chunks_{scope_id}:"
    function = "for_values" if isinstance(payload, ForLoop) else "site_values"
    args = payload.args if isinstance(payload, ForLoop) else (payload.nodes,)
    expr = ", ".join(f"{macros}.substitute({arg!r})" for arg in args)
    return f"for loop_values_{scope_id} in {function}({expr}):"


def _inline(writer, block, lines, steps):
    prefix = " " * writer.indent_depth * writer.indent_step
    fragments = []
    invocations = []
    cursor = 0
    for line in lines:
        source = str(line)
        fragment = "\n".join(prefix + part if part.strip() else "" for part in source.splitlines())
        if isinstance(line, RenderedCall):
            start = cursor + len(prefix)
            invocations.append(_RelativeInvocation(
                operation=line.definition, source_range=SourceRange(start, cursor + len(fragment)),
                arguments=tuple(replace(argument, source_range=SourceRange(
                    start + argument.source_range.start_offset + len(prefix) * source[:argument.source_range.start_offset].count("\n"),
                    start + argument.source_range.end_offset + len(prefix) * source[:argument.source_range.end_offset].count("\n")))
                    for argument in line.arguments), semantic_key=line.semantic_key))
        fragments.append(fragment)
        writer.write_block(source)
        cursor += len(fragment) + 1
    if not fragments:
        fragments = [prefix + "pass"]
        writer.write("pass")
    suffix = {Kind.EXTERNAL_RUN: "external", Kind.EMAIL: "email"}.get(block.kind, block.kind.value.lower())
    steps.append(StepEmission(function_name=f"step_{block.index:04d}_{suffix}", block_index=block.index,
                              functional_kind=block.kind.value, source="\n".join(fragments),
                              call_site="", invocations=tuple(invocations)))


def _call(utility, method, function, *args, **kwargs):
    definition = UtilitySpec.operation_definition(utility, method)
    if definition is None:
        raise ValueError(f"Missing compiler operation {utility}.{method}")
    if function == "job.html":
        path = replace(definition.parameter("template"), name="template_path", position=0, display_label="HTML asset")
        definition = replace(definition, parameters=(path, *(item for item in definition.parameters if item.name not in {"ctx", "template"})))
    return EmittableOperation.render_method_call(definition, function=function, args=args, kwargs=kwargs)


def _lower_lines(lines, macros):
    result = []
    targets = {"ctx.write_file": "job.write_file", "fs_ops.copy": "job.copy_file",
               "fs_ops.rename": "job.rename_file", "fs_ops.delete": "job.delete_files",
               "wait_file.poll": "job.wait_file", "external.run": "job.run_program",
               "smart_append.append": "job.smart_append", "email.send": "job.send_mail"}
    for line in lines:
        if not isinstance(line, RenderedCall):
            body = ast.parse(line).body
            if any(isinstance(item, ast.Pass) for item in body):
                raise ValueError("Unsupported utility operation")
            if any(isinstance(item, ast.Name) and item.id == "ctx" for statement in body for item in ast.walk(statement)):
                raise ValueError("Embedded Python using the retired ctx API must use explicit runtime functions and workdir")
            def top_level_exit(node):
                if isinstance(node, (ast.FunctionDef, ast.AsyncFunctionDef, ast.ClassDef)):
                    return False
                return isinstance(node, (ast.Return, ast.Yield, ast.YieldFrom)) or any(top_level_exit(child) for child in ast.iter_child_nodes(node))
            if any(top_level_exit(statement) for statement in body):
                raise ValueError("Embedded Python return/yield must be inside an authored function")
            result.append(line)
            continue
        operation = line.definition
        key = operation.utility_name + "." + operation.method
        arguments = []
        kwargs = {}
        for argument in line.arguments:
            value = CodeExpr(_expression(argument.source, macros), argument.value,
                             symbol_names=argument.symbol_names)
            if argument.position is not None:
                arguments.append(value)
            else:
                kwargs[argument.name] = value
        if key == "macro.set_named":
            name, value = arguments
            source = f"{macros}[{name.source}] = {value.source}"
            rendered = EmittableOperation.render_method_call(operation, args=tuple(arguments))
            start = len(macros) + 1
            value_start = start + len(name.source) + 4
            tracked = tuple(replace(argument, source_range=SourceRange(offset, offset + len(argument.source)))
                            for argument, offset in zip(rendered.arguments, (start, value_start), strict=True))
            result.append(RenderedCall(source, operation, tracked))
            continue
        target = targets.get(key)
        if target is None:
            raise ValueError(f"Unsupported direct operation {key}")
        result.append(EmittableOperation.render_method_call(operation, function=target,
                                                           args=tuple(arguments), kwargs=kwargs))
    return result


def _table_option(value, kind, sql_asset, assets):
    """Externalize long static schema options into one editable JSON asset."""
    if kind != "header":
        raise ValueError("New pivots are emitted as concise parameters, not JSON")
    size = len(value)
    if size < 8:
        return value
    path = sql_asset.removesuffix(".sql") + f".{kind}.json"
    assets[path] = json.dumps(value, ensure_ascii=False, indent=2) + "\n"
    return CodeExpr(f"job.table_spec({path!r})")


def _list(value):
    return value if isinstance(value, list) else [value] if value else []


def _report_options(block):
    options = HtmlReport._parse_options(block.resolved_body)
    allowed = {"TYPE", "INPUT-FILE", "OUTPUT-FILE", "CSS", "COLUMN-DATA", "COLUMN-HEADERS",
               "COLUMN-ALIGNMENT", "AT-TOP-OF-REPORT", "NOPREPROCESS"}
    for name, value in options.items():
        if name not in allowed and value and str(value).upper() not in {"N", "NO", "FALSE"}:
            raise ValueError(f"Active report option {name} is unsupported")
    return options


def _table(report_id, options):
    headers = "".join('<th>${' + report_id + f'_HEADER_{index + 1}' + '}</th>'
                      for index, _ in enumerate(_list(options.get("COLUMN-HEADERS"))))
    top = options.get("AT-TOP-OF-REPORT")
    heading = '<p class="at-top-of-report">' + " ".join(_list(top)).replace("$", "$$") + "</p>\n" if top else ""
    return heading + '<table class="tblin"><thead><tr id="colhdr">' + headers + \
        '</tr></thead><tbody>${' + report_id + '_ROWS}</tbody><tfoot></tfoot></table>'


def emit_project(dispatched):
    ensure_utility_checks_loaded()
    blocks = {block.index: block for block in dispatched.resolved.blocks}
    blocks.update({block.index: block for block in dispatched.dispatched})
    assets = {}
    report_options = {}
    sql_parameters = {}
    html_parameters = {}
    steps = []
    imports = {"from pathlib import Path"}
    has_state = any(block.kind in {Kind.MACRO_CONTROL, Kind.ROWS_IN_FILE, Kind.HTML_REPORT}
                    or "<<<" in block.resolved_body or any("<<<" in value for value in block.resolved_options.lookup.values())
                    for block in blocks.values())
    runtime_imports = {"JobRuntime"}
    writer = IndentWriter()
    has_aed = any(block.kind is Kind.AED for block in blocks.values())
    writer.write("def run(workdir=WORK_DIR" + (", *, aed_service_factory=None" if has_aed else "") + "):")
    writer.push_indent()
    writer.write("workdir = Path(workdir).resolve()")
    if has_aed:
        runtime_imports.add("bootstrap_aed")
        source_name = Path(dispatched.resolved.blocks[0].span.file).stem
        writer.write(f"aed_config = bootstrap_aed(workdir=workdir, source_name={source_name!r})")
    writer.write("job = JobRuntime(BASE_DIR, workdir" + (", values=aed_config, initial_macros=aed_config" if has_aed else "") + ")")
    if has_state:
        writer.write("macros = job.macros")

    def fail(block, error):
        raise ValueError(f"{block.span.file or '<input>'}:{block.span.start_line}:1 (block {block.index}): {error}")

    def walk(node, macros="macros"):
        payload = node.control_payload
        if isinstance(payload, StartMacro):
            if not payload.csv_path:
                writer.write(control_header(payload, node.scope_id, macros))
                writer.push_indent()
                before = len(writer.lines)
                for child in node.children:
                    walk(child, macros)
                if len(writer.lines) == before:
                    writer.write("pass")
                writer.pop_indent()
                return
            row = f"macro_row_{node.scope_id}"
            writer.write(control_header(payload, node.scope_id, macros))
            writer.write(f"if {row} is not None:")
            writer.push_indent()
            writer.write(f"with {macros}.scope({row}):")
            writer.push_indent()
            before = len(writer.lines)
            for child in node.children:
                walk(child, macros)
            if len(writer.lines) == before:
                writer.write("pass")
            writer.pop_indent()
            writer.pop_indent()
        elif isinstance(payload, IfThen):
            writer.write(control_header(payload, node.scope_id, macros))
            writer.push_indent()
            before = len(writer.lines)
            for child in node.children[0].children:
                walk(child, macros)
            if len(writer.lines) == before:
                writer.write("pass")
            writer.pop_indent()
            if len(node.children) > 1:
                writer.write("else:")
                writer.push_indent()
                before = len(writer.lines)
                for child in node.children[1].children:
                    walk(child, macros)
                if len(writer.lines) == before:
                    writer.write("pass")
                writer.pop_indent()
        elif isinstance(payload, (RunLoop, ForLoop, SiteLoop)):
            if isinstance(payload, RunLoop):
                imports.add("from contextlib import closing")
                writer.write(control_header(payload, node.scope_id, macros))
                writer.push_indent()
                writer.write(f"for chunk_{node.scope_id} in chunks_{node.scope_id}:")
            else:
                function = "for_values" if isinstance(payload, ForLoop) else "site_values"
                runtime_imports.add(function)
                writer.write(control_header(payload, node.scope_id, macros))
            writer.push_indent()
            overlay = "{}" if isinstance(payload, RunLoop) else f"loop_values_{node.scope_id}"
            writer.write(f"with {macros}.scope({'' if isinstance(payload, RunLoop) else overlay}):")
            writer.push_indent()
            trapped = isinstance(payload, SiteLoop) or isinstance(payload, RunLoop) and payload.prompt_off
            if trapped:
                writer.write("try:")
                writer.push_indent()
            before = len(writer.lines)
            for child in node.children:
                walk(child, macros)
            if len(writer.lines) == before:
                writer.write("pass")
            if trapped:
                writer.pop_indent()
                writer.write("except Exception:")
                writer.push_indent()
                writer.write("break" if isinstance(payload, SiteLoop) else "continue")
                writer.pop_indent()
            writer.pop_indent()
            writer.pop_indent()
            if isinstance(payload, RunLoop):
                writer.pop_indent()
        elif node.kind != "leaf":
            for child in node.children:
                walk(child, macros)
        else:
            block = blocks[node.block_index]
            try:
                leaf(block, macros)
            except (ValueError, SyntaxError) as exc:
                fail(block, exc)

    def leaf(block, macros):
        writer.write(f"# Source block {block.index}, line {block.span.start_line}")
        if block.kind in {Kind.SQL_QUERY, Kind.SQLITE_QUERY}:
            reader = block.reader
            if reader.utility_name == "sqlite_reader":
                runtime_imports.add("SqliteReader")
            else:
                imports.add(f"from {reader.module} import {reader.name}")
                runtime_imports.add("OracleClient")
            name = f"sql/query_{block.index:03d}_{project_name(Path(resolve_output_path(block)).stem)}.sql"
            assets[name] = SqliteEngine._sql_source(block)
            kwargs = {"reader": CodeExpr(f"{reader.name}(" + ", ".join(f"{key}={value!r}" for key, value in block.reader_kwargs.items()) + ")"),
                      "output": resolve_output_path(block)}
            if block.kind is Kind.SQLITE_QUERY:
                kwargs["inputs"] = SqliteEngine._extract_table_inputs(block)
            else:
                kwargs["node"] = block.reader_target.node
            crosstab = extract_crosstab_options(block)
            if crosstab:
                kwargs.update(crosstab)
            else:
                header = SqliteEngine._extract_header(block)
                if header:
                    kwargs["header"] = _table_option(header, "header", name, assets)
            definition = UtilitySpec.operation_definition("ctx", "run_query")
            sql_definition = definition.parameter("sql")
            sql_parameters[block.index] = (sql_definition, assets[name])
            path_definition = replace(sql_definition, name="path", display_label="SQL asset", capabilities=())
            definition = replace(definition, parameters=tuple(path_definition if parameter.name == "sql" else parameter for parameter in definition.parameters))
            call = EmittableOperation.render_method_call(definition, function="job.sql",
                                                         args=(CodeExpr(repr(name)),), kwargs=kwargs)
            _inline(writer, block, [call], steps)
        elif block.kind is Kind.HTML_REPORT:
            html(block, macros)
        elif block.kind is Kind.AED:
            argv = split_utility_command(block.resolved_options.lookup["UTILITIES"])
            if len(argv) != 2:
                raise ValueError("AED requires exactly one candidate CSV path")
            path = strip_quotes(argv[1])
            if "<<<" in path:
                path = CodeExpr(f"{macros}.substitute({path!r})")
            _inline(writer, block, [f"job.process_candidates({path.source if isinstance(path, CodeExpr) else repr(path)}, config=aed_config, service_factory=aed_service_factory)"], steps)
        elif block.kind is Kind.UNKNOWN:
            if block.resolved_options.lookup.get("JSL", "").upper() == "Y":
                raise ValueError("JMP/JSL blocks are outside the supported core")
            raise ValueError("Unclassified utility is unsupported")
        elif block.kind is Kind.MACRO_CONTROL:
            raise ValueError("Unexpected or unmatched control token")
        else:
            handler = UtilitySpec._emit_handlers.get(block.kind)
            if handler is None:
                raise ValueError(f"No compiler handler for {block.kind.value}")
            lines = handler.emit_block(block)
            if isinstance(lines, tuple):
                _, lines = lines
            if not lines:
                raise ValueError("Unsupported utility operation")
            lines = _lower_lines(lines, macros)
            _inline(writer, block, lines, steps)

    def html(block, macros):
        report_type = block.resolved_options.lookup.get("REPORT", "").upper()
        method = report_type.removeprefix("HTML-").lower()
        definition = UtilitySpec.operation_definition("html_report", method)
        metadata = {"template": block.resolved_body}
        metadata.update({name: block.resolved_options.lookup.get(option) for name, option in
                         {"id": "ID", "instance": "INSTANCE", "prompt_text": "PROMPT-TEXT", "app_server_default": "APP_SERVER_DEFAULT",
                          "outlook": "OUTLOOK", "json_only": "JSON-ONLY", "chart_instance": "CHART-INSTANCE"}.items()
                         if definition is not None and definition.parameter(name) is not None})
        html_parameters[block.index] = (definition, metadata)
        def tracked(lines):
            return [RenderedCall("\n".join(lines), definition, ())]
        if report_type == "HTML-DELETE":
            # SPFSQL3.py:20244-20250 deletes consumed spec files, not CSS.
            _inline(writer, block, tracked(["job.delete_html()"]), steps)
        elif report_type == "HTML-RUN":
            # SPFSQL3.py:20083-20100: CSS and HTML have immediate effects.
            rows = list(HtmlReport._iter_rows(block.resolved_body))
            kind = next((parts[1].upper() for parts in rows if parts[0].upper() == "TYPE"), "")
            if kind == "CSS":
                logical_name = next((parts[1] for parts in rows if parts[0].upper() == "CSS"), "")
                if not logical_name:
                    _inline(writer, block, tracked(["pass"]), steps)
                else:
                    if "<<<" in logical_name:
                        raise ValueError("Dynamic HTML-RUN CSS filenames require source-backed runtime generation")
                    styles = {parts[1]: parts[2:] for parts in rows if parts[0].upper() == "FORMAT"}
                    name = f"styles/report_{block.index:03d}.css"
                    # SPFUtilities/utils.py:8866-9050: one CSS file per immediate spec.
                    assets[name] = build_css(styles) + "\n"
                    _inline(writer, block, tracked([f"job.define_css({logical_name!r}, {name!r})"]), steps)
            elif kind == "HTML":
                runtime_imports.add("csv_report")
                options = _report_options(block)
                report_id = f"RUN_{block.index:03d}"
                names = _list(options.get("COLUMN-DATA"))
                labels = _list(options.get("COLUMN-HEADERS")) or names
                shell = HtmlReport._HTML_SCAFFOLD.format(
                    title="SQLPathFinder Report", css_decl="${VG2C_CSS}",
                    body=_table(report_id, {**options, "COLUMN-HEADERS": labels}))
                name = f"html/report_{block.index:03d}.html"
                assets[name] = shell
                report = (f"csv_report({options.get('INPUT-FILE', '')!r}, columns={names!r}, "
                          f"headers={labels!r}, alignment={_list(options.get('COLUMN-ALIGNMENT'))!r})")
                output = options.get("OUTPUT-FILE") or "SQLPathFinder.htm"
                _inline(writer, block, tracked([
                    f"job.html({name!r}, output={output!r}, reports={{{report_id!r}: {report}}})"
                ]), steps)
            else:
                raise ValueError(f"Unsupported HTML-RUN TYPE {kind!r}; expected CSS or HTML")
        elif report_type == "HTML-DEFER":
            runtime_imports.add("csv_report")
            options = _report_options(block)
            report_id = block.resolved_options.lookup.get("ID", "")
            if not re.fullmatch(r"[A-Za-z_][A-Za-z0-9_]*", report_id):
                raise ValueError(f"Invalid report ID {report_id!r}")
            report_options[report_id] = options
            call = f"job.reports[{report_id!r}] = csv_report({options.get('INPUT-FILE', '')!r}, columns={_list(options.get('COLUMN-DATA'))!r}, headers={_list(options.get('COLUMN-HEADERS'))!r}, alignment={_list(options.get('COLUMN-ALIGNMENT'))!r}, output_file={options.get('OUTPUT-FILE')!r})"
            _inline(writer, block, tracked([call]), steps)
        elif report_type == "HTML-LAYOUT":
            directives = {}
            body = []
            for line in block.resolved_body.splitlines(keepends=True):
                if line.startswith(":") and ":" in line[1:]:
                    key, value = line[1:].split(":", 1)
                    directives[key.upper()] = value.strip()
                else:
                    body.append(line)
            unsupported = [key for key, value in directives.items()
                           if key not in {"FILE", "TITLE", "CSS", "CSSEMBED"}
                           and value and value.upper() not in {"N", "NO", "FALSE"}]
            unsupported += [key for key in ("OUTLOOK", "JSON-ONLY", "CHART-INSTANCE", "APP_SERVER_DEFAULT")
                            if (value := block.resolved_options.lookup.get(key)) and value.upper() not in {"N", "NO", "FALSE"}]
            if unsupported:
                logging.getLogger(__name__).warning(
                    "[local-html-only] %s:%s:1 (block %s): Local HTML output does not implement %s; delivery/browser/security/chart integration is outside this renderer.",
                    block.span.file or "<input>", block.span.start_line, block.index, ", ".join(unsupported))
            source = "".join(body).replace("$", "$$")
            for report_id in re.findall(r"HTM:([A-Za-z0-9_]+)", source):
                if report_id not in report_options:
                    raise ValueError(f"Unknown deferred report {report_id}")
                source = source.replace("HTM:" + report_id, _table(report_id, report_options[report_id]))
            if "<html" not in source.lower():
                source = HtmlReport._HTML_SCAFFOLD.format(title=escape(directives.get("TITLE", "SQLPathFinder Report")).replace("$", "$$"), css_decl="${VG2C_CSS}", body=source)
            slots = {}
            def slot(match):
                key = f"VALUE_{len(slots) + 1}"
                slots[key] = match.group(0)
                return "${" + key + "}"
            source = re.sub(r"<<<[^>]+>>>", slot, source)
            name = f"html/report_{block.index:03d}.html"
            assets[name] = source
            output = directives.get("FILE", "report.html")
            instance = block.resolved_options.lookup.get("INSTANCE")
            css = directives.get("CSS")
            kwargs = {"output": output, "instance": instance,
                      "embed_css": directives.get("CSSEMBED", "").upper() in {"Y", "YES", "TRUE"}}
            if slots:
                kwargs["values"] = CodeExpr("{" + ", ".join(f"{key!r}: {macros}.substitute({value!r})" for key, value in slots.items()) + "}")
            if css:
                kwargs["css_file"] = css
            _inline(writer, block, [_call("html_report", "layout", "job.html", CodeExpr(repr(name)), **kwargs)], steps)
        else:
            raise ValueError(f"Unsupported report type {report_type}")

    walk(dispatched.resolved.scope_tree)
    writer.pop_indent()
    if runtime_imports:
        imports.add("from vg2c.runtime import " + ", ".join(sorted(runtime_imports)))
    reader_imports = sorted(item for item in imports if item.startswith("from datasyncx "))
    ordinary_imports = sorted(imports - set(reader_imports))
    setup = "\nOracleClient.configure()\n" if reader_imports else ""
    header = "\n".join(ordinary_imports) + setup + "\n".join(reader_imports) + "\n\nBASE_DIR = Path(__file__).resolve().parent\nWORK_DIR = BASE_DIR / 'output'\n\n"
    source = header + writer.source() + '\n\nif __name__ == "__main__":\n    run()\n'
    ast.parse(source)
    finalized = []
    for step in finalize_steps(source, steps):
        if step.block_index in sql_parameters:
            definition, sql = sql_parameters[step.block_index]
            invocations = []
            for invocation in step.invocations:
                parameter = EmittedParameter(
                    id=f"{invocation.id}:sql", name="sql", position=None, source=repr(sql), value=sql,
                    editor_type="multiline", editable=False,
                    read_only_reason="SQL is an external editable asset; generated Python offsets cannot edit it.",
                    definition=replace(definition, default=sql), artifact_role=None, source_range=None)
                invocations.append(replace(invocation, parameters=(*invocation.parameters, parameter)))
            step = replace(step, invocations=tuple(invocations))
        if step.block_index in html_parameters:
            definition, metadata = html_parameters[step.block_index]
            invocations = []
            for invocation in step.invocations:
                parameters = list(invocation.parameters)
                for name, value in metadata.items():
                    if any(item.name == name and item.source_range is not None for item in parameters):
                        continue
                    parameter_definition = definition.parameter(name)
                    if parameter_definition is None:
                        continue
                    parameters = [item for item in parameters if item.name != name]
                    parameters.append(EmittedParameter(
                        id=f"{invocation.id}:{name}", name=name, position=None, source=repr(value), value=value,
                        editor_type="multiline" if name == "template" else "string", editable=False,
                        read_only_reason="Report definitions and shells are external assets; edit the project assets or regenerate from source.",
                        definition=replace(parameter_definition, default=value), artifact_role=None, source_range=None))
                invocations.append(replace(invocation, parameters=tuple(parameters)))
            step = replace(step, invocations=tuple(invocations))
        finalized.append(step)
    return EmittedScript(source=source, imports=tuple(sorted(imports)), steps=tuple(finalized),
                         assets=tuple(assets.items()))
