"""Programmatic call-site emission driven by ``@register_utility`` metadata.

``emit_call(target, **kwargs)`` derives the receiver/method strings from
the routing metadata stamped onto registered classes and functions, so
handlers never type ``ctx.x.y(...)`` strings by hand.

Routing rules
-------------

1. **Free function** registered via :func:`register_utility` (no ``.`` in
   ``__qualname__``) → ``{func.__name__}(...)``. No receiver.
2. **Method of a registered class** → receiver is ``CTX_VAR`` if the
   class is registered under the same name as ``CTX_VAR`` (i.e.
   ``PipelineContext`` registered as ``"ctx"``), otherwise
   ``f"{CTX_VAR}.{registered_name}"``. Method name is ``target.__name__``.
3. **Unregistered target** → :class:`EmitTargetError` at compile time.

Every successful call also marks the target's registered embed name as
``needed`` on the supplied :class:`EmitContext` (when provided).
"""

from __future__ import annotations

import inspect
import sys

from vg2c.emitter.codegen.call_spec import CallSpec
from vg2c.emitter.codegen.constants import CTX_VAR
from vg2c.emitter.codegen.expr import PyExpr
from vg2c.emitter.utilities._registry import CLASS_TO_UTILITY_NAME, UTILITIES

__all__ = ["emit_call", "EmitTargetError"]


class EmitTargetError(TypeError):
    """Raised when ``emit_call`` is given an unregistered target."""


def _resolve_class_from_qualname(target) -> type | None:
    """Walk ``target.__qualname__`` against ``sys.modules[target.__module__]``
    to recover the owning class for an unbound method."""
    module = sys.modules.get(target.__module__)
    if module is None:
        return None
    parts = target.__qualname__.split(".")
    if len(parts) < 2:
        return None
    obj: object = module
    for part in parts[:-1]:
        obj = getattr(obj, part, None)
        if obj is None:
            return None
    return obj if isinstance(obj, type) else None


def _registered_function_name(target) -> str | None:
    """Return the function's own ``__name__`` if it was registered as a
    free function via ``@register_utility``; otherwise ``None``."""
    if not inspect.isfunction(target):
        return None
    if "." in target.__qualname__:
        return None
    registered = getattr(target, "__vg2c_registered_name__", None)
    if registered is None:
        return None
    # Confirm the registry agrees (defends against name collisions).
    if UTILITIES.get(registered) is not target:
        return None
    return target.__name__


def emit_call(target, /, *args: PyExpr, **kwargs: PyExpr) -> CallSpec:
    """Build a :class:`CallSpec` for *target* using decorator metadata.

    Args:
        target: A callable registered via :func:`register_utility`. May be
            a free function or an unbound method (``ClassName.method``).
        *args: Positional arguments as :class:`PyExpr` instances.
        **kwargs: Keyword arguments as :class:`PyExpr` instances.

    Raises:
        EmitTargetError: When *target* is not registered.
    """
    # Free-function case
    free_name = _registered_function_name(target)
    if free_name is not None:
        embed = getattr(target, "__vg2c_registered_name__", None)
        return CallSpec(
            method=free_name,
            receiver=None,
            args=args,
            kwargs=kwargs,
            embed_key=embed,
        )

    # Class-method case
    if inspect.isfunction(target) and "." in target.__qualname__:
        cls = _resolve_class_from_qualname(target)
        if cls is not None:
            registered = CLASS_TO_UTILITY_NAME.get(cls)
            if registered is not None:
                receiver = (
                    CTX_VAR if registered == CTX_VAR else f"{CTX_VAR}.{registered}"
                )
                return CallSpec(
                    method=target.__name__,
                    receiver=receiver,
                    args=args,
                    kwargs=kwargs,
                    embed_key=registered,
                )

    raise EmitTargetError(
        f"emit_call: target {target!r} is not a @register_utility-decorated "
        "function or method"
    )


def register_call_embed(ctx, *specs: CallSpec) -> None:
    """Register every spec's ``embed_key`` on the emit context.

    Equivalent to calling ``register_utility_emission`` once per spec but
    keeps handlers free of repeated string literals.
    """
    if ctx is None:
        return
    # Import locally to avoid an import cycle: utilities_embed imports the
    # registry, and the registry would otherwise import the embed helpers.
    from vg2c.emitter.utilities_embed import register_utility_emission

    keys = tuple(s.embed_key for s in specs if s.embed_key)
    if keys:
        register_utility_emission(ctx, *keys)
