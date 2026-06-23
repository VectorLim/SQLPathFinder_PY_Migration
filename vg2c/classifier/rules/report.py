from __future__ import annotations

from typing import Literal

from vg2c.classifier.coerce import as_int
from vg2c.classifier.model import HtmlReportSpec, Kind, Role
from vg2c.classifier.rules.base import Match
from vg2c.model import ParsedBlock


class HtmlReportRule:
    """Match /REPORT= blocks."""

    name = "html_report"

    def match(self, b: ParsedBlock) -> Match | None:
        """Match HTML report generation."""
        report_val = b.options.get("REPORT", "").strip().upper()
        if report_val not in {"HTML-RUN", "HTML-LAYOUT", "HTML-DELETE"}:
            return None

        phase_map = {
            "HTML-RUN": "RUN",
            "HTML-LAYOUT": "LAYOUT",
            "HTML-DELETE": "DELETE",
        }
        phase: Literal["RUN", "LAYOUT", "DELETE"] = phase_map[report_val]  # type: ignore

        spec = HtmlReportSpec(
            phase=phase,
            raw_payload=b.body,
            instance=as_int(b.options.get("INSTANCE")),
            prompt=b.options.get("PROMPT-TEXT"),
        )

        return Match(Kind.HTML_REPORT, Role.LEAF, spec, f"/REPORT={report_val}")
