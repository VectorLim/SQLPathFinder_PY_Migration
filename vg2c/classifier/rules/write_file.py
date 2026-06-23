from __future__ import annotations

from vg2c.classifier.coerce import as_bool_yn, as_int, as_path_string
from vg2c.classifier.model import Kind, Role, WriteFileSpec
from vg2c.classifier.rules.base import Match
from vg2c.model import ParsedBlock


class WriteFileRule:
    """Match /WRITE-FILE=Y blocks."""

    name = "write_file"

    def match(self, b: ParsedBlock) -> Match | None:
        """Match write-file directive."""
        if not as_bool_yn(b.options.get("WRITE-FILE")):
            return None

        csv_out = as_path_string(b.options.get("CSV")) or ""
        immediate = as_bool_yn(b.options.get("IMMEDIATE"))

        prompt_text = b.options.get("PROMPT-TEXT", "")
        if "immediate" in prompt_text.lower():
            immediate = True

        spec = WriteFileSpec(
            csv_out=csv_out,
            immediate=immediate,
            body=b.body,
            instance=as_int(b.options.get("INSTANCE")),
            prompt=prompt_text or None,
        )

        return Match(Kind.WRITE_FILE, Role.LEAF, spec, "/WRITE-FILE=Y")
