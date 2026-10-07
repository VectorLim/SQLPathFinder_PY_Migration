from dataclasses import dataclass

from vg2c.frontend.models import ClassifiedBlock
from vg2c.operands import ScopeNode


@dataclass(frozen=True, slots=True)
class ResolvedProgram:
    blocks: tuple[ClassifiedBlock, ...]
    scope_tree: ScopeNode
