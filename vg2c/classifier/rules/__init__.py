from __future__ import annotations

from vg2c.classifier.rules.base import Rule
from vg2c.classifier.rules.control_flow import ControlFlowRule
from vg2c.classifier.rules.report import HtmlReportRule
from vg2c.classifier.rules.sql_execution import SqlExecutionRule
from vg2c.classifier.rules.utility import UtilityRule
from vg2c.classifier.rules.write_file import WriteFileRule

__all__ = ["RULE_CHAIN"]

RULE_CHAIN: list[Rule] = [
    ControlFlowRule(),
    UtilityRule(),
    WriteFileRule(),
    HtmlReportRule(),
    SqlExecutionRule(),
]
