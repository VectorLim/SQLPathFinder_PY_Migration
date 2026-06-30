"""Registry for embeddable utility classes and functions.

Each utility is registered with:
- A string key (e.g., "csv_io", "macro")
- The class or function itself
- Required imports for embedding

The emitter uses `inspect.getsource(obj)` at generation time to inline
registered utilities into the generated script.
"""

from __future__ import annotations

from typing import Callable, TypeVar

__all__ = ["UTILITIES", "UTILITY_IMPORTS", "register_utility"]

# Registry of utility classes/functions keyed by name
UTILITIES: dict[str, type | Callable] = {}

# Registry of imports for each utility
UTILITY_IMPORTS: dict[str, tuple[str, ...]] = {}

T = TypeVar("T")


def register_utility(
    name: str,
    *,
    imports: tuple[str, ...] = (),
) -> Callable[[T], T]:
    """Decorator to register a utility class or function for embedding.

    Args:
        name: Unique key for this utility (e.g., "csv_io").
        imports: Tuple of import statements needed when embedding this utility.

    Example:
        @register_utility("csv_io", imports=(
            "import csv",
            "from pathlib import Path",
        ))
        class CsvIO:
            ...
    """

    def decorator(obj: T) -> T:
        UTILITIES[name] = obj  # type: ignore
        UTILITY_IMPORTS[name] = imports
        return obj

    return decorator
