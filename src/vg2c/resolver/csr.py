"""Recognize legacy CSR submissions from producer, destination, and lexical gates."""

from dataclasses import dataclass
from fnmatch import fnmatchcase
import logging
from pathlib import PureWindowsPath
import re

from vg2c.kind import Kind
from vg2c.operands import IfThen
from vg2c.utilities._emit_helpers import parse_table_binding, split_utility_command
from vg2c.runtime.values import strip_quotes

_SELECT = re.compile(
    r"\s*SELECT\s+(?:DISTINCT\s+)?(?P<items>.*?)\s+FROM\s+\[?(?P<table>\w+)\]?"
    r"(?:\s+WHERE\s+1\s*=\s*1)?\s*;?\s*",
    re.IGNORECASE | re.DOTALL,
)
_ITEM = re.compile(
    r"\s*(?:\[(?P<column>[^\]]+)\]|'(?P<literal>(?:[^']|'')*)'|(?P<bare>\w+))"
    r"\s+AS\s+\[?(?P<name>\w+)\]?\s*(?:,|$)",
    re.IGNORECASE,
)


@dataclass(frozen=True, slots=True)
class CsrSubmission:
    block_index: int
    producer_index: int
    signal_scope_id: int
    notification_indexes: tuple[int, ...]
    history_scope_ids: tuple[int, ...]
    input_file: str = ""
    lot_column: str = ""
    hold: bool = True
    contain: bool = False
    note: str = ""
    ccb: str = ""
    # Literal CSR fields with no AED equivalent, kept as a hint for the user.
    legacy_fields: tuple[tuple[str, str], ...] = ()


def csr_request(producer):
    """Read lot column, actions and constants from a `SELECT ... FROM [T]` producer."""
    match = _SELECT.fullmatch(producer.resolved_body)
    if match is None:
        raise ValueError(
            "CSR producer must be SELECT [DISTINCT] <fields> FROM [table] [WHERE 1=1]"
        )
    fields, items, position = {}, match["items"], 0
    while position < len(items):
        item = _ITEM.match(items, position)
        if item is None or item.end() == position:
            raise ValueError(
                "CSR producer fields must be [column] or 'literal' AS name"
            )
        name = item["name"].upper()
        if name in fields:
            raise ValueError(f"Duplicate CSR field {name}")
        literal = item["literal"]
        fields[name] = (
            ("column", item["column"] or item["bare"])
            if literal is None
            else ("literal", literal.replace("''", "'"))
        )
        position = item.end()

    def flag(name, default):
        kind, value = fields.pop(name, ("literal", default))
        if kind == "column" or value.upper() not in {"Y", "N", ""}:
            return kind, value
        return "flag", value.upper() == "Y"

    contain = flag("AUTO_CONTAIN", "N")
    if contain[0] != "flag":
        raise ValueError("CSR AUTO_CONTAIN must be a literal Y or N")
    hold = flag("HOLD_LOT", "N" if contain[1] else "Y")
    lot = fields.pop("LOT", None)
    if hold[0] == "column":
        # Older exports put the lot ID in HOLD_LOT instead of a flag.
        if lot is not None and lot[1].casefold() != hold[1].casefold():
            raise ValueError("Conflicting CSR HOLD_LOT and LOT columns")
        lot, hold = hold, ("flag", True)
    if hold[0] != "flag":
        raise ValueError("CSR HOLD_LOT must be Y, N or the LOT column")
    if lot is None or lot[0] != "column":
        raise ValueError("CSR LOT must come from an input column")
    if not (hold[1] or contain[1]):
        raise ValueError("CSR producer requests neither hold nor containment")
    texts = {}
    for name in ("HOLD_NOTE", "CCB_NUMBER"):
        kind, value = fields.pop(name, ("literal", ""))
        if kind != "literal":
            raise ValueError(f"CSR {name} must be a literal or macro")
        texts[name] = value
    if not texts["CCB_NUMBER"].strip():
        raise ValueError("CSR requests require CCB_NUMBER")
    inputs = {}
    for key, value in producer.resolved_options.pairs:
        for binding in value.split(",") if key == "TABLE" else ():
            path, table = parse_table_binding(binding.strip())
            if table:
                inputs[table.upper()] = path
    if match["table"].upper() not in inputs:
        raise ValueError(f"CSR producer table {match['table']} has no /TABLE binding")
    return dict(
        input_file=inputs[match["table"].upper()],
        lot_column=lot[1],
        hold=hold[1],
        contain=contain[1],
        note=texts["HOLD_NOTE"],
        ccb=texts["CCB_NUMBER"],
        legacy_fields=tuple(
            (name, value) for name, (kind, value) in fields.items() if kind == "literal"
        ),
    )


def identify_csr(blocks, scope_tree):
    by_index = {block.index: block for block in blocks}
    submissions = []

    def fail(block, message):
        logging.getLogger(__name__).error(
            "[unsupported-csr] %s:%s:1 (block %s): %s",
            block.span.file or "<input>",
            block.span.start_line,
            block.index,
            message,
        )
        raise ValueError(f"{block.span.file}:{block.span.start_line}: {message}")

    def gate(node, lhs, op, rhs):
        payload = node.control_payload
        return (
            node.kind == "if"
            and isinstance(payload, IfThen)
            and (payload.lhs.upper(), payload.op, payload.rhs.upper()) == (lhs, op, rhs)
            and payload.conj is None
        )

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
                if candidate.kind in {
                    Kind.SQL_QUERY,
                    Kind.SQLITE_QUERY,
                    Kind.DUCKDB_QUERY,
                } and fnmatchcase(options.get("CSV", "").upper(), argv[1].upper()):
                    producers.append(candidate)
            payload_fields = {
                value.strip().upper()
                for candidate in producers
                for value in candidate.resolved_options.lookup.get("HEADERS", "").split(
                    ","
                )
            }
            if not payload_fields.intersection(
                {"HOLD_LOT", "LOT", "AUTO_CONTAIN", "HOLDCATEGORY"}
            ) and not {"EMAIL", "CCB_NUMBER"}.issubset(payload_fields):
                if producers or not (
                    argv[1].upper().startswith("RESULT_")
                    and argv[1].upper().endswith(".CSV")
                ):
                    continue
            context = (*ancestors, node)

            def positive_gates(lhs, op, rhs):
                return [
                    item
                    for item, branch in zip(context, context[1:])
                    if gate(item, lhs, op, rhs)
                    and branch.scope_id == item.children[0].scope_id
                ]

            signals = positive_gates("SIGNAL", "GT", "0")
            if (
                argv[3].upper() != "<<<CSRPATH>>>"
                or argv[2] not in {".", ".\\", "./"}
                or not signals
                or not positive_gates("VAR(<<<DCSR>>>)", "EQS", "Y")
            ):
                fail(
                    block,
                    "CSR submission requires a local producer and SIGNAL > 0 / DCSR = Y gates",
                )
            if len(producers) != 1:
                fail(
                    block,
                    "CSR submission requires exactly one preceding CSV producer in its branch",
                )
            producer = producers[0]
            try:
                request = csr_request(producer)
            except ValueError as error:
                fail(producer, str(error))
            signal = signals[-1]
            following = [
                item for item in blocks if block.index < item.index <= signal.end_index
            ]
            notifications = tuple(
                item.index
                for item in following
                if item.resolved_options.lookup.get("REPORT", "").upper()
                == "HTML-LAYOUT"
                and ":FILE:EMAIL:" in item.resolved_body.upper()
            )
            history_scopes = []

            def find_history(item):
                if gate(item, "HISTERR", "LE", "0") and item.start_index > block.index:
                    history_scopes.append(item.scope_id)
                for nested in item.children:
                    find_history(nested)

            find_history(signal)
            submissions.append(
                CsrSubmission(
                    block.index,
                    producer.index,
                    signal.scope_id,
                    notifications,
                    tuple(history_scopes),
                    **request,
                )
            )

    walk(scope_tree)
    claimed = set()
    signal_scopes = set()
    for submission in submissions:
        if submission.signal_scope_id in signal_scopes:
            fail(
                by_index[submission.block_index],
                "Multiple CSR submissions in one signal branch are unsupported",
            )
        signal_scopes.add(submission.signal_scope_id)
        for index in submission.notification_indexes:
            if index in claimed:
                fail(
                    by_index[submission.block_index],
                    "Multiple CSR submissions share one notification",
                )
            claimed.add(index)
    return tuple(submissions)
