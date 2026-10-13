"""HTML template-slot validation and literal-dollar handling."""

from __future__ import annotations

from html.parser import HTMLParser
from string import Template
import re

# Template placeholders remain supported, but only explicit ${NAME} slots and
# known legacy $NAME slots are interpreted. Other dollars are authored text.
_DOLLAR = re.compile(r"\$\$|\$\{[A-Za-z_][A-Za-z0-9_]*\}|\$[A-Za-z_][A-Za-z0-9_]*|\$")


def _literal_dollars(source, legacy_names):
    def escape_literal(match):
        token = match.group(0)
        if token == "$":
            if source[match.end():].startswith("{"):
                raise ValueError("Malformed explicit HTML slot; use ${NAME} or $$ for a literal $")
            return "$$"
        if token.startswith("${") or token == "$$" or token[1:] in legacy_names:
            return token
        return "$$" + token[1:]
    return _DOLLAR.sub(escape_literal, source)


def _identifiers(text):
    template = Template(text)
    if not template.is_valid():
        raise ValueError("Malformed explicit HTML template slot")
    return template.get_identifiers()


class _Slots(HTMLParser):
    _VOID = {"area", "base", "br", "col", "embed", "hr", "img", "input", "link", "meta", "param", "source", "track", "wbr"}
    _URL = {"href", "src", "srcset", "action", "formaction", "poster", "data", "background", "xlink:href"}

    def __init__(self):
        super().__init__(convert_charrefs=False)
        self.stack = []

    def handle_starttag(self, tag, attrs):
        raw = self.get_starttag_text()
        if _identifiers(tag) or any(_identifiers(name) for name, _ in attrs):
            raise ValueError("Dynamic tag or attribute names are unsupported")
        if _identifiers(raw) and len({name for name, _ in attrs}) != len(attrs):
            raise ValueError("Dynamic slots in duplicate attributes are unsupported")
        for name, value in attrs:
            slots = _identifiers(value or "")
            if not slots:
                continue
            if (name in self._URL or name == "style" or name.startswith("on")
                    or any(slot.endswith(("_ROWS", "_TABLE")) or slot == "VG2C_CSS" for slot in slots)):
                raise ValueError(f"Unsupported slot in {tag}.{name}")
            match = re.search(r"(?:\s)" + re.escape(name) + r"\s*=\s*(['\"])", raw, re.I)
            if match is None:
                raise ValueError(f"HTML slot in {name} requires a quoted attribute")
        if tag not in self._VOID:
            self.stack.append(tag)

    def handle_startendtag(self, tag, attrs):
        self.handle_starttag(tag, attrs)
        if tag not in self._VOID:
            self.stack.pop()

    def handle_endtag(self, tag):
        if tag in self.stack:
            del self.stack[len(self.stack) - 1 - self.stack[::-1].index(tag):]

    def handle_data(self, data):
        for slot in _identifiers(data):
            if any(tag in {"script", "style"} for tag in self.stack):
                raise ValueError(f"Unsupported dynamic {self.stack[-1]} content")
            if (slot.endswith("_ROWS") or slot.startswith("VG2C_DYNAMIC_ROWS_")) and (not self.stack or self.stack[-1] != "tbody"):
                raise ValueError(f"Rows slot {slot} must be direct tbody content")
            if slot == "VG2C_CSS" and (not self.stack or self.stack[-1] != "head"):
                raise ValueError("CSS slot must be direct head content")

    def handle_comment(self, data):
        if _identifiers(data):
            raise ValueError("Slots in HTML comments are unsupported")

    def handle_decl(self, data):
        if _identifiers(data):
            raise ValueError("Slots in HTML declarations are unsupported")

    def handle_pi(self, data):
        if _identifiers(data):
            raise ValueError("Slots in HTML processing instructions are unsupported")


def _validate_slots(source):
    _identifiers(source)
    if re.search(r"<\s*/?\s*\$|\$\{[^}]+\}\s*=", source):
        raise ValueError("Dynamic tag or attribute names are unsupported")
    parser = _Slots()
    parser.feed(source)
    parser.close()
