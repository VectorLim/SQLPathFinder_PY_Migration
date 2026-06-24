from __future__ import annotations

# View Registry — scaffold only (v1)
#
# In v1 this module loads nothing and returns None for every lookup.
# When authored registry YAML files are available, the load() and lookup()
# functions here are the only entry point that needs updating.
#
# Diagnostic code: dispatch-view-registry-missing-entry (info)
# Emitted only when view_registry_path is set in DispatchConfig (opt-in).

from pathlib import Path
from typing import Any


def load(path: Path | None) -> dict[str, Any]:
    """Load view registry entries from *path*.

    In v1 always returns an empty mapping regardless of the path argument.
    """
    return {}


def lookup(registry: dict[str, Any], dialect: str, view_name: str) -> None:
    """Look up a view expansion by dialect and view name.

    In v1 always returns None — no expansions are defined.
    """
    return None
