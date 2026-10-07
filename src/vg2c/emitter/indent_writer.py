from __future__ import annotations


class IndentWriter:
    """Main's indentation helper without step tracking or wrapper detection."""

    def __init__(self):
        self.lines: list[str] = []
        self.indent_depth = 0
        self.line_number = 1

    def push_indent(self) -> None:
        self.indent_depth += 1

    def pop_indent(self) -> None:
        self.indent_depth -= 1

    def write(self, line: str = "") -> None:
        # Indent code, leaving embedded SQL/template newlines unchanged.
        self.lines.append(" " * (self.indent_depth * 4) + line if line else "")
        self.line_number += line.count("\n") + 1

    def source(self) -> str:
        return "\n".join(self.lines) + "\n"
