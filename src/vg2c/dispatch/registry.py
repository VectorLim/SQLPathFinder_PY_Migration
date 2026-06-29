from __future__ import annotations

from vg2c.dispatch.base import DialectHandler
from vg2c.dispatch.models import Dialect
from vg2c.frontend.models import Kind

HANDLERS: dict[Dialect, type[DialectHandler]] = {}
_KIND_TO_HANDLER: dict[Kind, type[DialectHandler]] = {}
SQL_BEARING_KINDS: frozenset[Kind] = frozenset()


def register(handler_cls: type[DialectHandler]) -> type[DialectHandler]:
    """Register a dialect handler. Called by @register decorator on handler classes."""
    dialect = handler_cls.dialect
    existing = HANDLERS.get(dialect)
    if existing is not None and existing is not handler_cls:
        raise ValueError(f"Duplicate dialect handler registered for {dialect!r}")
    HANDLERS[dialect] = handler_cls

    kind = handler_cls.kind
    if kind is not None:
        existing_kind = _KIND_TO_HANDLER.get(kind)
        if existing_kind is not None and existing_kind is not handler_cls:
            raise ValueError(f"Duplicate dialect handler registered for kind {kind!r}")
        _KIND_TO_HANDLER[kind] = handler_cls

    global SQL_BEARING_KINDS
    SQL_BEARING_KINDS = frozenset(_KIND_TO_HANDLER.keys())
    return handler_cls


def get_handler(dialect: Dialect) -> type[DialectHandler] | None:
    """Get handler by dialect string."""
    return HANDLERS.get(dialect)


def get_handler_for_kind(kind: Kind) -> type[DialectHandler] | None:
    """Get handler by Kind enum value."""
    return _KIND_TO_HANDLER.get(kind)


def resolve_dialect(kind: Kind) -> Dialect | None:
    """Resolve Kind to dialect string. Compatibility helper."""
    handler = get_handler_for_kind(kind)
    return None if handler is None else handler.dialect


def derive_handler_from_signals(
    node: str, engine: str, oledb: str
) -> type[DialectHandler] | None:
    """Derive handler from option signals for UNKNOWN blocks."""
    for handler in HANDLERS.values():
        if handler.matches_signals(node=node, engine=engine, oledb=oledb):
            return handler
    return None


def derive_from_signals(node: str, engine: str, oledb: str) -> Dialect | None:
    """Derive dialect string from option signals. Compatibility helper."""
    handler = derive_handler_from_signals(node=node, engine=engine, oledb=oledb)
    return None if handler is None else handler.dialect


# Import dialect modules to trigger registration
from vg2c.dispatch import dialects as _dialects  # noqa: E402,F401
