"""KindEnum — emits the Kind str-enum into the generated script.

Every utility whose class body references ``Kind`` (e.g. via ``handles``)
must declare ``"kind_enum"`` as a ``utility_dependency`` so the enum is always
present in the generated output before those utility classes are defined.
"""

from __future__ import annotations

import inspect
from enum import Enum

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import register_utility


class Kind(str, Enum):
    """Block-kind discriminator shared between the translator and the runtime."""

    SQL_QUERY = "SQL_QUERY"
    SQLITE_QUERY = "SQLITE_QUERY"
    WRITE_FILE = "WRITE_FILE"
    FS_COPY = "FS_COPY"
    FS_DELETE = "FS_DELETE"
    EXTERNAL_RUN = "EXTERNAL_RUN"
    HTML_REPORT = "HTML_REPORT"
    UTILITY = "UTILITY"
    MACRO_CONTROL = "MACRO_CONTROL"
    UNKNOWN = "UNKNOWN"
    MALFORMED = "MALFORMED"


@register_utility
class KindEnum(UtilitySpec):
    """Thin registration wrapper; emits only the ``Kind`` enum class above."""

    utility_name = "kind_enum"
    utility_imports = ("from enum import Enum",)

    # Emit the standalone Kind class verbatim instead of this wrapper class.
    __vg2c_source__ = inspect.getsource(Kind)
