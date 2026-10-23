"""Compiler-only composition for recognized CSR submission workflows."""
from __future__ import annotations

import re
from dataclasses import replace
from typing import TYPE_CHECKING

from vg2c.emitter.models import BlockEmission
from vg2c.operands import IfThen, ScopeNode
from vg2c.utilities._emit_helpers import native_macro_expression, resolve_output_path
from vg2c.utilities.html_report import HtmlReport

if TYPE_CHECKING:
    from vg2c.emitter.indent_writer import IndentWriter


class CsrWorkflow:
    """Own the resolved CSR-to-notification composition for one project."""

    def __init__(self, submissions, blocks, html_report: HtmlReport):
        self.submissions = {item.block_index: item for item in submissions}
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
        self.blocks = blocks
        self.html_report = html_report

    def handles_block(self, block) -> bool:
        return block.index in self.submissions or block.index in self.notifications

    def emit_block(self, block, *, macros="macros") -> BlockEmission:
        submission = self.submissions.get(block.index)
        if submission is not None:
            path = native_macro_expression(
                resolve_output_path(self.blocks[submission.producer_index]), macros
            )
            index = block.index
            return BlockEmission(
                lines=(
                    "# CSR -> AED: direct submission; edit the action and client call together.",
                    f"csr_requests_{index} = job.csr_requests({path})",
                    f"if csr_requests_{index}:\n"
                    f"    request = csr_requests_{index}[0]\n"
                    "    aed_facility = os.environ.get('AED_FACILITY', AED_FACILITY)\n"
                    "    aed_action = 'lot_hold'\n"
                    "    try:\n"
                    "        aed = AEDClient(facility=aed_facility, "
                    "mode=os.environ.get('AED_MODE', AED_MODE))\n"
                    f"        for request in csr_requests_{index}:\n"
                    "            # Alternative: aed.lot_set_attr(\n"
                    "            #     lotid=..., attributeid=..., attributevalue=...)\n"
                    "            response = aed.lot_hold(\n"
                    "                lotid=request['HOLD_LOT'],\n"
                    "                operation=os.environ.get("
                    "'AED_HOLD_OPERATION', AED_HOLD_OPERATION),\n"
                    "                holdcode=os.environ.get('AED_HOLD_CODE', AED_HOLD_CODE),\n"
                    "                comment=request['COMMENT'])\n"
                    "            job.record_aed_result(request, facility=aed_facility, "
                    "action=aed_action, response=response)\n"
                    "    except Exception as error:\n"
                    "        job.record_aed_result(request, facility=aed_facility, "
                    "action=aed_action, error=error)\n"
                    f"        job.csr_failure_mail(csr_requests_{index})\n"
                    "        raise\n"
                    f"    csr_history_allowed_{index} = aed.mode == 'prod'",
                ),
                imports=("import os",),
                runtime_exports=("AEDClient",),
            )

        if block.index not in self.notifications:
            raise ValueError(f"Block {block.index} is not part of a CSR workflow")
        directives, body = self.html_report.prepare_layout(block)
        body = re.sub(r"(?is)<p\b[^>]*>\s*Lot on hold\b.*?</p>", "", body)
        body = '<p>${AED_OUTCOME}</p><pre>${AED_DETAILS}</pre>\n' + body
        emission = self.html_report.emit_layout(
            block,
            directives,
            body,
            macros=macros,
            extra_values=(("AED_OUTCOME", "job.csr_status"), ("AED_DETAILS", "job.csr_details")),
        )
        call = str(emission.lines[0])
        output = directives.get("FILE", "report.html")
        recipient = native_macro_expression(output[len("EMAIL:"):], macros)
        title = directives.get("TITLE", "CSR AED outcome")
        return replace(
            emission,
            lines=(
                f"csr_report = {call}",
                f"job.send_mail(to={recipient}, subject={title!r} "
                "+ ' - ' + job.csr_status, body=str(csr_report), attachments=['AED_results.csv'])",
            ),
        )

    def condition_header(self, scope_id, base_header: str) -> str:
        block_index = self.history.get(scope_id)
        if block_index is None:
            return base_header
        return f"if csr_history_allowed_{block_index} and ({base_header[3:-1]}):"

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
        prelude = self.scope_prelude(node.scope_id)
        if node.scope_id not in self.history and not prelude:
            return False
        base_header = payload.render_header(node.scope_id, macros=macros)
        payload.emit_scope(
            writer,
            walk,
            node,
            macros=macros,
            imports=imports,
            runtime_imports=runtime_imports,
            header_override=self.condition_header(node.scope_id, base_header),
            prelude=prelude,
        )
        return True

    def scope_prelude(self, scope_id) -> tuple[str, ...]:
        return tuple(
            line
            for submission in self.submissions.values()
            if submission.signal_scope_id == scope_id
            for line in (
                f"csr_history_allowed_{submission.block_index} = True",
                "job.begin_csr()",
            )
        )

    def settings_source(self) -> str:
        if not self.submissions:
            return ""
        return (
            "# Editable AED defaults; matching environment variables override these at run time.\n"
            "# Set request parameters explicitly; they are absent from the VG2 payload.\n"
            "AED_FACILITY = None\nAED_HOLD_OPERATION = None\nAED_HOLD_CODE = None\n"
            "# None uses ENV_MODE, which defaults to test (no manufacturing writes).\n"
            "AED_MODE = None\n\n"
        )
