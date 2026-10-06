"""Rewrite legacy CSR/MMS publishing in an SPF script into one {AED} step.

Writes <script>.aed<suffix> beside the input (or in --out-dir) and prints a unified
diff plus one report line per rule. Blocks no rule touches are copied byte-for-byte.
Add a migration by appending a function to RULES.
"""

from __future__ import annotations

import argparse
import codecs
import csv
import difflib
import re
import sys
from dataclasses import dataclass, field
from pathlib import Path

DELIM = "<---- New Query ---->"
CANDIDATES = "AED_CANDIDATES.csv"
_OPTIONS = re.compile(r"<OPTIONS>(.*?)</OPTIONS>", re.I | re.S)
# Same token/value split as SPFTaskBase.parseTaskOptions.
_OPTION = re.compile(r"^/(\S*?)=(.*)$")
_OPEN = {"{if-then}": "{end-if}", "{start-macro}": "{end-macro}"}
_LOT = re.compile(r"([^\s,]+)\s+AS\s+\[?Lot_NCORisk\]?", re.I)
_FROM = re.compile(r"\bFROM\s+(\[[^\]]+\](?:\s+(?!WHERE\b)[A-Za-z_]\w*)?)", re.I)
_CONFIG_TEXT = re.compile(
    r"(<strong>Configurations applied</strong>).*?(</font>)", re.I | re.S
)
_AED_TEXT = (
    "<br>AED enabled: <<<AED_ENABLED>>><br>Target attribute(s): <<<ATTR_LIST>>>"
    "<br>Target value: <<<SKIP_OPERATION>>>"
)
_RETIRED = re.compile(
    r"CSRPATH|MIPPATH|MMSPATH|<<<D(?:CSR|MWF|MST)>>>|HIST\.(?:csv|txt)|\bdata\.csv\b"
    r"|getcsrsu|setsiteparam",
    re.I,
)


class MigrationError(ValueError):
    pass


@dataclass
class Block:
    raw: str
    line: int
    options: dict[str, str]
    body: str

    @property
    def args(self) -> list[str]:
        # Same quoting as SPFTaskBase.MyUtilities.
        value = self.options.get("UTILITIES", "")
        return next(
            csv.reader([value], delimiter=" ", quotechar='"', skipinitialspace=True), []
        )

    @property
    def utility(self) -> str:
        args = self.args
        return args[0].lower().removeprefix("@exedir@\\") if args else ""

    def arg(self, index: int) -> str:
        args = self.args
        return args[index] if len(args) > index else ""

    def writes(self, name: str) -> bool:
        return (
            self.options.get("WRITE-FILE", "").upper() == "Y"
            and self.options.get("CSV", "").lower() == name
        )


@dataclass
class Edit:
    start: int
    stop: int
    raws: list[str]
    rule: str


@dataclass
class Migration:
    text: str
    changes: list[str] = field(default_factory=list)
    warnings: list[str] = field(default_factory=list)


def parse(text: str) -> list[Block]:
    blocks, line = [], 1
    for raw in text.split(DELIM):
        match = _OPTIONS.search(raw)
        options: dict[str, str] = {}
        for entry in (match.group(1).splitlines() if match else []):
            option = _OPTION.match(entry.strip())
            if option:
                options.setdefault(option.group(1).upper(), option.group(2).strip())
        blocks.append(Block(raw, line, options, raw[match.end() :] if match else raw))
        line += raw.count("\n")
    return blocks


def pairs(blocks: list[Block]) -> dict[int, int]:
    stack: list[int] = []
    matched = {}
    for index, block in enumerate(blocks):
        if block.utility in _OPEN:
            stack.append(index)
        elif block.utility in _OPEN.values():
            if not stack or _OPEN[blocks[stack[-1]].utility] != block.utility:
                raise MigrationError(f"Unbalanced {block.utility} at line {block.line}")
            matched[stack.pop()] = index
    if stack:
        opener = blocks[stack[-1]]
        raise MigrationError(f"Unclosed {opener.utility} at line {opener.line}")
    return matched


def _render(options: list[str], body: str, nl: str) -> str:
    return nl.join(
        ["", "<OPTIONS>", *options, "</OPTIONS>", *([body] if body else []), ""]
    )


def _gates(blocks: list[Block], macro: str) -> list[int]:
    return [
        i
        for i, block in enumerate(blocks)
        if block.utility == "{if-then}" and f"<<<{macro}>>>" in block.arg(1).upper()
    ]


def _require(
    blocks: list[Block], start: int, stop: int, utility: str, needle: str
) -> None:
    if not any(
        block.utility == utility and needle in " ".join(block.args[1:]).upper()
        for block in blocks[start : stop + 1]
    ):
        raise MigrationError(
            f"Block at line {blocks[start].line} lacks {utility} with {needle}; not migrating"
        )


def _signal_rows(blocks: list[Block], gate: int) -> int | None:
    """Index of the ROWS-IN-FILE "data.csv" + IF-THEN pair directly wrapping a gate."""
    if gate < 2:
        return None
    rows, condition = blocks[gate - 2], blocks[gate - 1]
    if (
        rows.utility == "{rows-in-file}"
        and rows.arg(1).lower() == "data.csv"
        and condition.utility == "{if-then}"
        and condition.arg(1) == rows.arg(2)
    ):
        return gate - 2
    return None


def bootstrap(blocks, matched, nl):
    starts = [i for i, block in enumerate(blocks) if block.writes("macrotmp.csv")]
    ctime = [
        i
        for i, block in enumerate(blocks)
        if block.utility == "{start-macro}" and block.arg(1).lower() == "ctime.csv"
    ]
    if not starts and not ctime:
        return []
    if len(starts) != 1 or len(ctime) != 1 or ctime[0] < starts[0]:
        raise MigrationError(
            "Expected one macrotmp.csv bootstrap before {START-MACRO} ctime.csv"
        )
    start, stop = starts[0], matched[ctime[0]]
    if not any(
        block.writes("getcsrsu.bat")
        or block.utility in {"getcsrsu.bat", "setsiteparam.exe"}
        for block in blocks[start:stop]
    ):
        raise MigrationError(
            f"Bootstrap at line {blocks[start].line} has no getcsrsu/setsiteparam"
        )
    return [Edit(start, stop + 1, [], "CSR/MMS bootstrap")]


def signal_source(blocks, matched, nl):
    producers = [
        i
        for i, block in enumerate(blocks)
        if block.options.get("CSV", "").lower() == "data.csv"
        and block.options.get("ENGINE", "").lower() == "sqlite"
    ]
    if len(producers) != 1:
        raise MigrationError(
            f"Expected one SQLite DATA.csv producer, found {len(producers)}"
        )
    index = producers[0]
    block = blocks[index]
    lot, source = _LOT.search(block.body), _FROM.search(block.body)
    if not lot or not source or "TABLE" not in block.options:
        raise MigrationError(
            f"DATA.csv producer at line {block.line} has no Lot_NCORisk source"
        )
    instance = (
        [f"/INSTANCE={block.options['INSTANCE']}"]
        if "INSTANCE" in block.options
        else []
    )
    candidates = _render(
        [
            "/NODE=.\\",
            "/OLEDB=SQLite",
            "/ENGINE=SQLite",
            "/UN=",
            "/PW=",
            "/WORKDIR=.\\",
            f"/CSV={CANDIDATES}",
            f"/TABLE={block.options['TABLE']}",
            "/HEADERS=FACILITY,LOT",
            "/QUOTECSV=Y",
            *instance,
            "/PROMPT-TEXT=Select distinct flagged lots for AED",
        ],
        f"SELECT DISTINCT '<<<AED_FACILITY>>>' AS [FACILITY], {lot.group(1)} AS [LOT]{nl}"
        f"FROM {source.group(1)};",
        nl,
    )
    aed = _render(
        [
            "/WORKDIR=.\\",
            *instance,
            "/OUTLOOK=N",
            f'/UTILITIES={{AED}} "{CANDIDATES}"',
            "/PROMPT-TEXT=Apply and verify AED lot attributes",
        ],
        "",
        nl,
    )
    return [
        Edit(index, index + 1, [candidates, aed], "Signal source -> AED candidates")
    ]


def csr_publish(blocks, matched, nl):
    edits = []
    for gate in _gates(blocks, "DCSR"):
        _require(blocks, gate, matched[gate], "robocopy.va", "<<<CSRPATH>>>")
        edits.append(Edit(gate, matched[gate] + 1, [], "CSR publish"))
        rows = _signal_rows(blocks, gate)
        if rows is not None:
            retargeted = re.sub(
                r'"data\.csv"', f'"{CANDIDATES}"', blocks[rows].raw, count=1, flags=re.I
            )
            edits.append(
                Edit(
                    rows, rows + 1, [retargeted], "Notification gate -> AED candidates"
                )
            )
    return edits


def mms_publish(blocks, matched, nl):
    edits = []
    for gate in _gates(blocks, "DMWF"):
        stop = matched[gate]
        _require(blocks, gate, stop, "robocopy.va", "<<<MIPPATH>>>")
        rows = _signal_rows(blocks, gate)
        if rows is not None and matched[gate - 1] == stop + 1:
            edits.append(Edit(rows, stop + 2, [], "MMS publish"))
        else:
            edits.append(Edit(gate, stop + 1, [], "MMS publish"))
    return edits


def history_upload(blocks, matched, nl):
    edits = []
    for index, block in enumerate(blocks[:-1]):
        if (
            block.utility == "{rows-in-file}"
            and block.arg(1).lower() == "histerror.txt"
        ):
            gate = blocks[index + 1]
            if gate.utility != "{if-then}" or gate.arg(1) != block.arg(2):
                raise MigrationError(
                    f"HISTERROR check at line {block.line} has no matching IF-THEN"
                )
            _require(
                blocks, index + 1, matched[index + 1], "smartappend.va", "HIST.CSV"
            )
            edits.append(
                Edit(
                    index, matched[index + 1] + 1, [], "HIST upload (AED owns history)"
                )
            )
    return edits


def update_audit(blocks, matched, nl):
    return [
        Edit(i, i + 1, [], "update.bat audit")
        for i, block in enumerate(blocks)
        if block.writes("update.bat")
        or block.utility == "update.bat"
        or (block.utility == "spfdelete.bat" and block.arg(1).lower() == "update.bat")
    ]


def notification_text(blocks, matched, nl):
    edits = []
    for index, block in enumerate(blocks):
        if block.options.get("REPORT", "").upper() == "HTML-LAYOUT" and re.search(
            r"<<<D(?:CSR|MWF|MST)>>>", block.body, re.I
        ):
            raw, count = _CONFIG_TEXT.subn(
                lambda m: m.group(1) + _AED_TEXT + m.group(2), block.raw
            )
            if count:
                edits.append(
                    Edit(index, index + 1, [raw], "Notification configuration text")
                )
    return edits


RULES = [
    bootstrap,
    signal_source,
    csr_publish,
    mms_publish,
    history_upload,
    update_audit,
    notification_text,
]


def _check(blocks: list[Block]) -> None:
    matched = pairs(blocks)
    aed = [i for i, block in enumerate(blocks) if block.utility == "{aed}"]
    if len(aed) != 1:
        raise MigrationError(f"Expected exactly one {{AED}} step, found {len(aed)}")
    if not any(
        block.utility == "{start-macro}"
        and block.arg(1).lower() == "configsets.csv"
        and i < aed[0] < matched[i]
        for i, block in enumerate(blocks)
    ):
        raise MigrationError('{AED} must run inside {START-MACRO} "configsets.csv"')


def migrate(text: str) -> Migration:
    blocks = parse(text)
    if not (_gates(blocks, "DCSR") or _gates(blocks, "DMWF")):
        return Migration(text)
    if any(block.utility == "{aed}" for block in blocks):
        raise MigrationError("Script already contains {AED}")
    nl = "\r\n" if "\r\n" in text else "\n"
    matched = pairs(blocks)
    edits = sorted(
        (edit for rule in RULES for edit in rule(blocks, matched, nl)),
        key=lambda edit: (edit.start, edit.stop),
    )
    for before, after in zip(edits, edits[1:]):
        if after.start < before.stop:
            raise MigrationError(
                f"Rules '{before.rule}' and '{after.rule}' overlap "
                f"at line {blocks[after.start].line}"
            )
    segments, cursor = [], 0
    for edit in edits:
        segments += [block.raw for block in blocks[cursor : edit.start]] + edit.raws
        cursor = edit.stop
    segments += [block.raw for block in blocks[cursor:]]
    output = DELIM.join(segments)
    migrated = parse(output)
    _check(migrated)
    total = text.count("\n") + 1
    changes = [
        f"{edit.rule}: lines {blocks[edit.start].line}-"
        f"{blocks[edit.stop].line if edit.stop < len(blocks) else total} "
        f"{'replaced' if edit.raws else 'deleted'}"
        for edit in edits
    ]
    warnings = [
        f"line {block.line}: retired reference {match.group(0)!r}"
        for block in migrated
        for match in _RETIRED.finditer(block.raw)
    ]
    return Migration(output, changes, warnings)


def _read(path: Path) -> tuple[str, bool, str]:
    data = path.read_bytes()
    bom = data.startswith(codecs.BOM_UTF8)
    payload = data[len(codecs.BOM_UTF8) :] if bom else data
    try:
        return payload.decode("utf-8"), bom, "utf-8"
    except UnicodeDecodeError:
        return payload.decode("cp1252"), bom, "cp1252"


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser(
        description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter
    )
    parser.add_argument("scripts", nargs="+", type=Path)
    parser.add_argument("--out-dir", type=Path)
    args = parser.parse_args(argv)
    status = 0
    for path in args.scripts:
        text, bom, encoding = _read(path)
        try:
            result = migrate(text)
        except MigrationError as exc:
            print(f"{path}: ERROR {exc}", file=sys.stderr)
            status = 2
            continue
        if not result.changes:
            print(f"{path}: no CSR/MMS publishing found; unchanged")
            continue
        target = (args.out_dir or path.parent) / f"{path.stem}.aed{path.suffix}"
        target.parent.mkdir(parents=True, exist_ok=True)
        target.write_bytes(
            (codecs.BOM_UTF8 if bom else b"") + result.text.encode(encoding)
        )
        sys.stdout.writelines(
            difflib.unified_diff(
                text.splitlines(keepends=True),
                result.text.splitlines(keepends=True),
                str(path),
                str(target),
            )
        )
        print(f"\n{path} -> {target}")
        for change in result.changes:
            print(f"  {change}")
        for warning in result.warnings:
            print(f"  WARNING {warning}")
    return status


if __name__ == "__main__":
    raise SystemExit(main())
