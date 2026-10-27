"""Compiler-only composition for recognized CSR submission workflows."""

from __future__ import annotations

import re
from dataclasses import replace
from typing import TYPE_CHECKING

from vg2c.emitter.models import BlockEmission
from vg2c.operands import IfThen, ScopeNode
from vg2c.utilities._emit_helpers import native_macro_expression
from vg2c.utilities.html_report import HtmlReport

if TYPE_CHECKING:
    from vg2c.emitter.indent_writer import IndentWriter


class CsrWorkflow:
    """Own the resolved CSR-to-notification composition for one project."""

    def __init__(self, submissions, html_report: HtmlReport):
        self.submissions = {item.block_index: item for item in submissions}
        self.producers = {item.producer_index for item in submissions}
        self.notifications = {
            index: item
            for item in self.submissions.values()
            for index in item.notification_indexes
        }
        self.history = {
            scope_id: item.block_index
            for item in self.submissions.values()
            for scope_id in item.history_scope_ids
        }
        self.html_report = html_report

    def handles_block(self, block) -> bool:
        return block.index in self.submissions or block.index in self.notifications

    def emit_block(self, block, *, macros="macros") -> BlockEmission:
        submission = self.submissions.get(block.index)
        if submission is not None:
            lines = [
                f"aed = job.aed({macros}['FAC'])",
                "comment = job.csr_comment("
                f"{native_macro_expression(submission.note, macros)}, "
                f"ccb={native_macro_expression(submission.ccb, macros)})",
                f"for lot in job.csr_lots("
                f"{native_macro_expression(submission.input_file, macros)}, "
                f"{submission.lot_column!r}):",
            ]
            if submission.legacy_fields:
                lines.append(
                    "    # Legacy CSR fields: "
                    + ", ".join(f"{k}={v}" for k, v in submission.legacy_fields)
                )
            if submission.hold:
                lines += [
                    "    # Supply the confirmed AED hold operation and hold code.",
                    "    aed.lot_hold(lotid=lot, operation='', holdcode='', comment=comment)",
                ]
            if submission.contain:
                lines.append(
                    "    aed.lot_contain(lotid=lot, operation='', reference_number='')"
                )
            return BlockEmission(lines=tuple(lines))

        if block.index not in self.notifications:
            raise ValueError(f"Block {block.index} is not part of a CSR workflow")
        directives, body = self.html_report.prepare_layout(block)
        body = re.sub(r"(?is)<p\b[^>]*>\s*Lot on hold\b.*?</p>", "", body)
        body = "<p>${AED_OUTCOME}</p>\n" + body
        emission = self.html_report.emit_layout(
            block,
            directives,
            body,
            macros=macros,
            extra_values=(("AED_OUTCOME", "job.csr_status"),),
        )
        call = str(emission.lines[0])
        output = directives.get("FILE", "report.html")
        recipient = native_macro_expression(output[len("EMAIL:") :], macros)
        title = directives.get("TITLE", "CSR AED outcome")
        return replace(
            emission,
            lines=(
                f"csr_report = {call}",
                f"job.send_mail(to={recipient}, subject={title!r} "
                "+ ' - ' + job.csr_status, body=str(csr_report))",
            ),
        )

    def condition_header(self, scope_id, base_header: str) -> str:
        if scope_id not in self.history:
            return base_header
        # Test mode skips AED, so it must not record lots as handled.
        return f"if job.enabled('csr') and ({base_header[3:-1]}):"

    def emit_scope(
        self,
        writer: IndentWriter,
        walk,
        node: ScopeNode,
        *,
        macros: str,
        imports: set[str],
        runtime_imports: set[str],
    ) -> bool:
        payload = node.control_payload
        if not isinstance(payload, IfThen):
            return False
        production_gate = (
            payload.lhs.upper() in {"VAR(<<<DCSR>>>)", "VAR(<<<DMWF>>>)"}
            and payload.op == "EQS"
            and payload.rhs.upper() == "Y"
            and payload.conj is None
        )
        if node.scope_id not in self.history and not production_gate:
            return False
        header = self.condition_header(
            node.scope_id, payload.render_header(node.scope_id, macros=macros)
        )
        if production_gate:
            operation = "csr" if payload.lhs.upper() == "VAR(<<<DCSR>>>)" else "mms"
            header = f"if job.enabled({operation!r}) and {header[3:-1]}:"
        payload.emit_scope(
            writer,
            walk,
            node,
            macros=macros,
            imports=imports,
            runtime_imports=runtime_imports,
            header_override=header,
        )
        return True
