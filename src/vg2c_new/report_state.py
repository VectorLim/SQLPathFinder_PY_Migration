from __future__ import annotations

from dataclasses import dataclass, field
from pathlib import Path


@dataclass(frozen=True, slots=True)
class DeferredReport:
    """One report specification intentionally retained for a later layout command."""

    kind: str
    template: str
    path: Path


@dataclass(slots=True)
class ReportSession:
    """Minimal report-only state scoped to one RuntimeState invocation.

    ScriptHost stored these values in process/task globals. The direct runtime keeps
    only the state that genuinely crosses report commands: deferred specs, generated
    artifacts awaiting cleanup, and the two counters used to make report/chart IDs
    unique. No general runtime resources or services belong here.
    """

    deferred: dict[str, DeferredReport] = field(default_factory=dict)
    cleanup_paths: list[Path] = field(default_factory=list)
    window_counter: int = 1
    chart_counter: int = 1

    def defer(self, report_id: str, report: DeferredReport) -> None:
        self.deferred[report_id.upper()] = report
        self.track_cleanup(report.path)

    def lookup(self, report_id: str) -> DeferredReport | None:
        return self.deferred.get(report_id.upper())

    def track_cleanup(self, path: Path) -> None:
        if path not in self.cleanup_paths:
            self.cleanup_paths.append(path)

    def complete_layout(self) -> None:
        self.window_counter += 1
        self.chart_counter = 1

    def clear_artifacts(self) -> None:
        self.deferred.clear()
        self.cleanup_paths.clear()
