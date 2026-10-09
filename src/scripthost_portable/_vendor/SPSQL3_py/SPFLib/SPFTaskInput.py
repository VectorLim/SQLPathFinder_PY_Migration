"""Lexical form of one ScriptHost task item: <OPTIONS> lines, command text and
/UTILITIES values.

Pure functions shared by SPFTaskBase option parsing and the portable Python
API. Option meaning stays in SPFTaskBase.parseTaskOptions and /UTILITIES
splitting stays in SPFTaskBase.MyUtilities; this module only splits text and
builds the canonical text that parses back to the same input.
"""

import csv
import re
from io import StringIO

TASK_DELIMITER = "<---- New Query ---->"
OPTION_LINE = re.compile(
    r"^/(?P<optToken>[\S\w]*?)=(?P<optVal>[\S\w ]*)", re.MULTILINE | re.IGNORECASE
)


def split_task_item(task_item):
    """Return (option lines, command); raise the original structural errors."""
    if task_item.strip().find("<OPTIONS>") == -1:
        raise Exception("Error: Missing the <OPTIONS> Token : {0}".format(task_item))
    if task_item.strip().find("</OPTIONS>") == -1:
        raise Exception("Error: Missing the Options terminator token </OPTIONS>")
    options_text, command = task_item.lstrip().split("</OPTIONS>", 1)
    options_text = options_text.strip()
    if command.strip().find("<OPTIONS>") > 0:
        raise Exception(
            "Error: <OPTIONS> Token found in SQL Region. May be missing a\n<---- New Query ----> delimiter"
        )
    if len(options_text) == 0:
        raise Exception("Options data is invalid {0}".format(options_text))
    lines = options_text.split("\n")
    if lines.pop(0).strip() != "<OPTIONS>":
        raise Exception("Options list is not starting with <OPTIONS>")
    return lines, command.lstrip()


def iter_options(lines):
    """Yield (token, value) pairs lazily, so a bad line fails in source order."""
    for line in lines:
        line = line.strip()
        if len(line) == 0:
            continue
        found = OPTION_LINE.findall(line)
        if not found:
            raise Exception(
                "Error: Missing = in Token Line. Problem Line is: {0}".format(line)
            )
        yield found[0]


def parse_task_item(task_item):
    """Return ([(token, value), ...], command) for one task item."""
    lines, command = split_task_item(task_item)
    return list(iter_options(lines)), command


def encode_task_item(options, command=""):
    """Canonical task item text; raise ValueError unless it parses back unchanged."""
    options = [(str(token), str(value)) for token, value in options]
    text = "\n".join(
        [
            "<OPTIONS>",
            *("/{0}={1}".format(*pair) for pair in options),
            "</OPTIONS>",
            command,
        ]
    )
    try:
        exact = TASK_DELIMITER not in command and parse_task_item(text) == (
            options,
            command.lstrip(),
        )
    except Exception:
        exact = False
    if not exact:
        raise ValueError(
            "Options/command cannot be written as one ScriptHost task item: {0!r}".format(
                options
            )
        )
    return text


def encode_utility(name, arguments=()):
    """Canonical /UTILITIES value: the route name, then every argument quoted."""
    if not name or any(char.isspace() or char == '"' for char in name):
        raise ValueError("Utility name must be one unquoted token: {0!r}".format(name))
    stream = StringIO()
    csv.writer(
        stream, delimiter=" ", quotechar='"', quoting=csv.QUOTE_ALL, lineterminator=""
    ).writerow([str(argument) for argument in arguments])
    return name + (" " + stream.getvalue() if arguments else "")


def decode_utility(value):
    """Return [name, *arguments] when encode_utility rewrites 'value' exactly, else None."""
    parts = next(
        csv.reader([value], delimiter=" ", skipinitialspace=True, quotechar='"'), None
    )
    if not parts:
        return None
    try:
        return parts if encode_utility(parts[0], parts[1:]) == value else None
    except ValueError:
        return None
