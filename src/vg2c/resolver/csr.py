"""Recognize legacy CSR submissions from producer, destination, and lexical gates."""

from dataclasses import dataclass
from fnmatch import fnmatchcase
import logging
from pathlib import PureWindowsPath

from vg2c.kind import Kind
from vg2c.operands import IfThen
from vg2c.runtime.aed_client import CSR_HEADERS
from vg2c.utilities._emit_helpers import split_utility_command
from vg2c.utilities._runtime_helpers import strip_quotes

@dataclass(frozen=True, slots=True)
class CsrSubmission:
    block_index: int
    producer_index: int
    signal_scope_id: int
    notification_indexes: tuple[int, ...]
    history_scope_ids: tuple[int, ...]


def identify_csr(blocks, scope_tree):
    by_index = {block.index: block for block in blocks}
    submissions = []

    def fail(block, message):
        logging.getLogger(__name__).error(
            "[unsupported-csr] %s:%s:1 (block %s): %s",
            block.span.file or "<input>", block.span.start_line, block.index, message,
        )
        raise ValueError(f"{block.span.file}:{block.span.start_line}: {message}")

    def gate(node, lhs, op, rhs):
        payload = node.control_payload
        return (node.kind == "if" and isinstance(payload, IfThen)
                and (payload.lhs.upper(), payload.op, payload.rhs.upper()) == (lhs, op, rhs)
                and payload.conj is None)

    def walk(node, ancestors=()):
        for position, child in enumerate(node.children):
            if child.kind != "leaf":
                walk(child, (*ancestors, node))
                continue
            block = by_index[child.block_index]
            if block.kind is not Kind.FS_COPY:
                continue
            command = block.resolved_options.lookup.get("UTILITIES", "")
            argv = [strip_quotes(value) for value in split_utility_command(command)]
            if not argv or PureWindowsPath(argv[0]).name.lower() != "robocopy.va":
                continue
            if len(argv) < 4 or "<<<CSRPATH>>>" not in argv[3].upper():
                continue
            producers = []
            for sibling in node.children[:position]:
                if sibling.kind != "leaf":
                    continue
                candidate = by_index[sibling.block_index]
                options = candidate.resolved_options.lookup
                if candidate.kind in {Kind.SQL_QUERY, Kind.SQLITE_QUERY, Kind.DUCKDB_QUERY} and fnmatchcase(
                    options.get("CSV", "").upper(), argv[1].upper()
                ):
                    producers.append(candidate)
            payload_fields = {value.strip().upper() for candidate in producers for value in
                              candidate.resolved_options.lookup.get("HEADERS", "").split(",")}
            if not payload_fields.intersection({"HOLD_LOT", "AUTO_CONTAIN", "HOLDCATEGORY"}):
                if producers or not (argv[1].upper().startswith("RESULT_")
                                     and argv[1].upper().endswith(".CSV")):
                    continue
            context = (*ancestors, node)
            def positive_gates(lhs, op, rhs):
                return [item for item, branch in zip(context, context[1:])
                        if gate(item, lhs, op, rhs)
                        and branch.scope_id == item.children[0].scope_id]

            signals = positive_gates("SIGNAL", "GT", "0")
            if (argv[3].upper() != "<<<CSRPATH>>>" or argv[2] not in {".", ".\\", "./"}
                    or not signals
                    or not positive_gates("VAR(<<<DCSR>>>)", "EQS", "Y")):
                fail(block, "CSR submission requires a local producer and SIGNAL > 0 / DCSR = Y gates")
            if len(producers) != 1:
                fail(block, "CSR submission requires exactly one preceding CSV producer in its branch")
            producer = producers[0]
            headers = [value.strip().upper() for value in
                       producer.resolved_options.lookup.get("HEADERS", "").split(",")]
            if set(headers) != set(CSR_HEADERS) or len(headers) != len(CSR_HEADERS):
                fail(block, "CSR producer must declare the supported hold-request headers")
            signal = signals[-1]
            following = [item for item in blocks if block.index < item.index <= signal.end_index]
            notifications = tuple(item.index for item in following
                                  if item.resolved_options.lookup.get("REPORT", "").upper() == "HTML-LAYOUT"
                                  and ":FILE:EMAIL:" in item.resolved_body.upper())
            history_scopes = []

            def find_history(item):
                if gate(item, "HISTERR", "LE", "0") and item.start_index > block.index:
                    history_scopes.append(item.scope_id)
                for nested in item.children:
                    find_history(nested)

            find_history(signal)
            submissions.append(CsrSubmission(block.index, producer.index, signal.scope_id,
                                             notifications, tuple(history_scopes)))

    walk(scope_tree)
    claimed = set()
    signal_scopes = set()
    for submission in submissions:
        if submission.signal_scope_id in signal_scopes:
            fail(by_index[submission.block_index], "Multiple CSR submissions in one signal branch are unsupported")
        signal_scopes.add(submission.signal_scope_id)
        for index in submission.notification_indexes:
            if index in claimed:
                fail(by_index[submission.block_index], "Multiple CSR submissions share one notification")
            claimed.add(index)
    return tuple(submissions)
