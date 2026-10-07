from vg2c.frontend.models import ClassifiedBlock
from vg2c.resolver.models import ResolvedProgram
from vg2c.resolver.scope_builder import build_scope_tree


def resolve(blocks: list[ClassifiedBlock]) -> ResolvedProgram:
    return ResolvedProgram(tuple(blocks), build_scope_tree(blocks))
