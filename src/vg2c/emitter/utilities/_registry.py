"""Registry for embeddable utility classes and functions.

Each utility is registered with:
- A string key (e.g., "csv_io", "macro")
- The class or function itself
- Required imports for embedding

The emitter uses `inspect.getsource(obj)` at generation time to inline
registered utilities into the generated script.

The decorator also stamps routing metadata used by
:func:`vg2c.emitter.codegen.emit_call` so handlers can reference
utilities programmatically (e.g., ``emit_call(SqlMacros.sql_get_csv_list, …)``)
instead of hand-typing ``ctx.sql_macros.sql_get_csv_list(…)`` strings.
"""

from __future__ import annotations

import inspect
from typing import Callable, TypeVar

__all__ = [
    "UTILITIES",
    "UTILITY_IMPORTS",
    "CLASS_TO_UTILITY_NAME",
    "register_utility",
]

# Registry of utility classes/functions keyed by name
UTILITIES: dict[str, type | Callable] = {}

# Registry of imports for each utility
UTILITY_IMPORTS: dict[str, tuple[str, ...]] = {}

# Reverse map for class-typed utilities, used by emit_call to derive the
# ``ctx.<name>`` receiver from an unbound-method reference.
CLASS_TO_UTILITY_NAME: dict[type, str] = {}

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
        UTILITIES[name] = obj  # type: ignore[assignment]
        UTILITY_IMPORTS[name] = imports
        try:
            setattr(obj, "__vg2c_registered_name__", name)
        except (AttributeError, TypeError):
            pass
        if inspect.isclass(obj):
            CLASS_TO_UTILITY_NAME[obj] = name  # type: ignore[index]
        return obj

    return decorator
