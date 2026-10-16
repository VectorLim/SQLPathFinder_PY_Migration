"""Windows companion for SQLPathFinder source previews and Python updates."""

from .compiler import CompilationOutcome, CompilationWorker, compile_source
from .export import export_preview, save_copy
from .models import EditCopy, NativeSnapshot, ObserverSnapshot, Selection
from .native import inspect_native
from .observer import (
    NativeTarget,
    ObserverClient,
    ObserverWorker,
    RefreshResult,
    parse_refresh,
    parse_snapshot,
    parse_targets,
)
from .source import (
    GeneratedAssetLink,
    SourceMapping,
    generated_asset_links,
    map_designer_label,
    map_generated_offset_to_asset,
    map_source_offset,
    python_to_utf16_offset,
    utf16_to_python_offset,
)

__all__ = [
    "CompilationOutcome",
    "CompilationWorker",
    "EditCopy",
    "NativeSnapshot",
    "NativeTarget",
    "ObserverClient",
    "ObserverSnapshot",
    "ObserverWorker",
    "RefreshResult",
    "Selection",
    "GeneratedAssetLink",
    "SourceMapping",
    "compile_source",
    "export_preview",
    "save_copy",
    "inspect_native",
    "map_source_offset",
    "generated_asset_links",
    "map_generated_offset_to_asset",
    "map_designer_label",
    "parse_snapshot",
    "parse_refresh",
    "parse_targets",
    "python_to_utf16_offset",
    "utf16_to_python_offset",
]

