from __future__ import annotations

from abc import ABC, abstractmethod
import inspect
import re
from typing import TYPE_CHECKING, Any, ClassVar

from vg2c.kind import Kind

if TYPE_CHECKING:
    from vg2c.frontend.models import BlockOptions


__all__ = ["CheckedUtilitySpec", "UtilitySpec"]


_CLASS_SIG_RE = re.compile(r"^(\s*class\s+\w+)\(.*\):\s*$")


def _strip_embed_artifacts(source: str, class_name: str) -> str:
    lines = source.split("\n")

    while lines and lines[0].lstrip().startswith("@"):
        lines.pop(0)

    if not lines:
        return ""

    lines[0] = _CLASS_SIG_RE.sub(r"\1:", lines[0])
    lines[0] = lines[0].replace("(CheckedUtilitySpec):", ":")
    lines[0] = lines[0].replace("(UtilitySpec):", ":")
    lines[0] = lines[0].replace(f"({class_name}, UtilitySpec):", f"({class_name}):")

    lines = [
        line
        for line in lines
        if not line.lstrip().startswith("handles =")
        and "@EmitContext.step_emitter" not in line
    ]

    return "\n".join(lines).rstrip()


class UtilitySpec(ABC):
    """Base contract for all embeddable utilities."""

    utility_name: ClassVar[str]
    handles: ClassVar[tuple[Kind, ...]] = ()
    _registry: ClassVar[dict[str, type[UtilitySpec]]] = {}
    _emit_handlers: ClassVar[dict[Kind, type[UtilitySpec]]] = {}

    def __init_subclass__(cls, **kwargs: Any) -> None:
        super().__init_subclass__(**kwargs)

        raw_name = cls.__dict__.get("utility_name")
        if not isinstance(raw_name, str):
            return

        name = raw_name.strip()
        if not name:
            raise ValueError(f"{cls.__name__}: utility_name must be non-empty")

        existing = UtilitySpec._registry.get(name)
        if existing is not None and existing is not cls:
            raise ValueError(f"duplicate utility_name: {name}")

        UtilitySpec._registry[name] = cls

        for handled_kind in tuple(getattr(cls, "handles", ())):
            owner = UtilitySpec._emit_handlers.get(handled_kind)
            if owner is not None and owner is not cls:
                raise ValueError(
                    "duplicate handler for "
                    f"{handled_kind}: {owner.__name__} and {cls.__name__}"
                )
            UtilitySpec._emit_handlers[handled_kind] = cls

    @classmethod
    def get_source(cls) -> str:
        custom = getattr(cls, "__vg2c_source__", None)
        if custom is not None:
            return str(custom).rstrip()

        source = inspect.getsource(cls)
        return _strip_embed_artifacts(source, cls.__name__)

    @staticmethod
    def emit_block(block: Any) -> tuple[str, str] | None:
        return None


class CheckedUtilitySpec(UtilitySpec):
    """Utility that participates in Stage 1 classification."""

    _check_handlers: ClassVar[list[type[CheckedUtilitySpec]]] = []

    def __init_subclass__(cls, **kwargs: Any) -> None:
        super().__init_subclass__(**kwargs)

        if inspect.isabstract(cls):
            return

        CheckedUtilitySpec._check_handlers.append(cls)

    @staticmethod
    @abstractmethod
    def check(options: BlockOptions) -> tuple[Kind, str] | None:
        raise NotImplementedError

    @classmethod
    def iter_checks(cls) -> tuple[type[CheckedUtilitySpec], ...]:
        return tuple(cls._check_handlers)
