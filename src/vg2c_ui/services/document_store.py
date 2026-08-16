from __future__ import annotations

from pathlib import Path

from vg2c_ui.domain.models import (
    CommandBatch,
    CommandPreview,
    CommandResult,
    CsvPreview,
    WorkflowDocument,
    WorkflowSidecar,
)
from vg2c_ui.services.atomic_io import atomic_write_text
from vg2c_ui.services.command_service import CommandService
from vg2c_ui.services.compiler_adapter import compile_source, output_path_for
from vg2c_ui.services.csv_preview import CsvPreviewService
from vg2c_ui.services.sidecar import read_sidecar, write_sidecar
from vg2c_ui.services.workflow_builder import build_workflow


class PathOutsideWorkspace(ValueError):
    pass


class DocumentStore:
    def __init__(self, workspace: Path) -> None:
        self.workspace = Path(workspace).resolve()
        self._commands = CommandService(self.resolve, self.open_document)
        self._csv = CsvPreviewService(self.resolve)

    def open_document(
        self, source_path: str | Path, output_path: str | Path | None = None
    ) -> WorkflowDocument:
        source = self.resolve(source_path)
        output = self.resolve(output_path) if output_path else output_path_for(source)
        result = compile_source(source)
        generated = (
            output.read_text(encoding="utf-8")
            if output.is_file()
            else result.generated_python
        )
        return build_workflow(result, output, generated, self._read_sidecar(output))

    def translate_document(
        self, source_path: str | Path, out_dir: str | Path | None = None
    ) -> WorkflowDocument:
        source = self.resolve(source_path)
        destination = self.resolve(out_dir) if out_dir else source.parent
        destination.mkdir(parents=True, exist_ok=True)
        output = output_path_for(source, destination)
        result = compile_source(source)
        atomic_write_text(output, result.generated_python)
        previous = self._read_sidecar(output)
        document = build_workflow(result, output, result.generated_python, previous)
        write_sidecar(
            output,
            WorkflowSidecar(
                source_hash=document.source_hash,
                output_hash=document.output_hash,
                revision=document.revision,
                overrides=document.overrides,
            ),
        )
        return document

    def preview_commands(self, batch: CommandBatch) -> CommandPreview:
        return self._commands.preview(batch)

    def apply_commands(self, batch: CommandBatch) -> CommandResult:
        document, diff = self._commands.apply(batch)
        return CommandResult(document=document, diff=diff)

    def preview_csv(self, source_path: str, csv_path: str) -> CsvPreview:
        return self._csv.preview(source_path, csv_path)

    def resolve(self, value: str | Path) -> Path:
        path = Path(value)
        resolved = (self.workspace / path).resolve() if not path.is_absolute() else path.resolve()
        if not resolved.is_relative_to(self.workspace):
            raise PathOutsideWorkspace(f"path is outside workspace: {value}")
        return resolved

    @staticmethod
    def _read_sidecar(output: Path) -> WorkflowSidecar | None:
        return read_sidecar(output)


__all__ = ["DocumentStore", "PathOutsideWorkspace"]
