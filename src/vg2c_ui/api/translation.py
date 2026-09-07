from __future__ import annotations

from pathlib import Path
from uuid import uuid4

from fastapi import APIRouter, Request

from vg2c_ui.api.models import (
    BatchTranslationRequest,
    BatchTranslationResponse,
    DiagnosticView,
    TranslationOutcomeView,
)
from vg2c_ui.services.atomic_io import atomic_write_text
from vg2c_ui.services.document_store import DocumentStore

router = APIRouter(prefix="/api/translations", tags=["translations"])

_SUPPORTED_SOURCE_SUFFIXES = {".txt", ".vg2"}
_UPLOAD_ROOT = ".vg2c-ui/uploads"


@router.post("/batch", response_model=BatchTranslationResponse)
def translate_batch(
    payload: BatchTranslationRequest, request: Request
) -> BatchTranslationResponse:
    """Translate browser-selected files independently and return one outcome per file."""
    store: DocumentStore = request.app.state.document_store
    batch_dir = store.workspace / _UPLOAD_ROOT / uuid4().hex
    results: list[TranslationOutcomeView] = []
    seen_names: set[str] = set()

    for selected in payload.files:
        display_name = selected.name.strip() or "Unnamed file"
        try:
            file_name = _validated_file_name(selected.name)
            key = file_name.casefold()
            if key in seen_names:
                raise ValueError(f"Duplicate file name in selection: {file_name}")
            seen_names.add(key)

            source = batch_dir / file_name
            source.parent.mkdir(parents=True, exist_ok=True)
            atomic_write_text(source, selected.content)
            opened = store.translate(str(source))
            results.append(
                TranslationOutcomeView(
                    file_name=file_name,
                    status="success",
                    document=opened.view,
                    diagnostics=list(opened.view.diagnostics),
                )
            )
        except Exception as exc:
            # Per-file isolation is intentional: one invalid/failed source must not
            # prevent the remaining selected files from reaching the compiler.
            results.append(
                TranslationOutcomeView(
                    file_name=display_name,
                    status="error",
                    diagnostics=[
                        DiagnosticView(
                            level="error",
                            code="translation-failed",
                            message=str(exc) or "Translation failed.",
                        )
                    ],
                )
            )

    return BatchTranslationResponse(results=results)


def _validated_file_name(value: str) -> str:
    name = value.strip()
    candidate = Path(name)
    if not name or candidate.name != name or name in {".", ".."}:
        raise ValueError("Invalid source file name.")
    if candidate.suffix.lower() not in _SUPPORTED_SOURCE_SUFFIXES:
        raise ValueError("Unsupported VG2 source file type. Select a .txt or .vg2 file.")
    return name


__all__ = ["router"]
