from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any, Callable, Generic, TypeVar, overload, ParamSpec

from vg2c.frontend.models import Diagnostic
from vg2c.kind import Kind

P = ParamSpec("P")
R = TypeVar("R")

__all__ = ["EmittedScript", "IndentWriter", "emittable"]


@dataclass(frozen=True, slots=True)
class EmittedScript:
    """Stage 5 final output."""

    source: str
    imports: tuple[str, ...]
    diagnostics: tuple[Diagnostic, ...]


class IndentWriter:
    """Simple indentation helper for code generation."""

    def __init__(self, indent_step: int = 4):
        self.lines: list[str] = []
        self.indent_depth: int = 0
        self.indent_step = indent_step
        self.step_lines: dict[str, int] = {}

    def push_indent(self) -> None:
        self.indent_depth += 1

    def pop_indent(self) -> None:
        self.indent_depth = max(0, self.indent_depth - 1)

    def write(self, line: str) -> None:
        """Write a single line with current indentation."""
        if line.strip():
            self.lines.append(" " * (self.indent_depth * self.indent_step) + line)
        else:
            self.lines.append("")

    def write_block(self, lines: str) -> None:
        """Write multiple lines."""
        import re

        match = re.match(r"^\s*def\s+(step_\w+)\b", lines)
        if match:
            self.step_lines[match.group(1)] = len(self.lines) + 1
        for line in lines.split("\n"):
            self.write(line)

    def source(self) -> str:
        """Get the full source text."""
        return "\n".join(self.lines)


class emittable(Generic[P, R]):
    """Decorator / descriptor to make utility methods renderable at emit-time."""

    def __init__(self, func: Callable[P, R]) -> None:
        self.func = func
        self.__name__ = func.__name__
        self.__doc__ = func.__doc__

    @overload
    def __get__(self, instance: None, owner: Any) -> EmittableMethod[P, R]: ...

    @overload
    def __get__(self, instance: object, owner: Any) -> BoundEmittableMethod[P, R]: ...

    def __get__(self, instance: Any, owner: Any) -> Any:
        if instance is None:
            return EmittableMethod(self.func, owner)
        return BoundEmittableMethod(self.func, instance, owner)

    @staticmethod
    def render_method_call(
        utility_name: str,
        method_name: str,
        *,
        args: tuple[Any, ...] = (),
        kwargs: dict[str, Any] | None = None,
    ) -> str:
        """Render a Python method-call expression for the generated script."""

        def _render_value(value: Any) -> str:
            if isinstance(value, str):
                return value
            return repr(value)

        receiver = "ctx" if utility_name == "ctx" else f"ctx.{utility_name}"
        parts: list[str] = [_render_value(arg) for arg in args]
        for key, value in (kwargs or {}).items():
            parts.append(f"{key}={_render_value(value)}")
        return f"{receiver}.{method_name}({', '.join(parts)})"


class EmittableMethod(Generic[P, R]):
    """Unbound emittable method descriptor wrapper (accessed via Class)."""

    def __init__(self, func: Callable[P, R], owner: Any) -> None:
        self.func = func
        self.owner = owner

    def __call__(self, instance: Any, *args: P.args, **kwargs: P.kwargs) -> R:
        # Standard unbound method call: first argument is the instance
        return self.func(instance, *args, **kwargs)

    def render(self, *args: Any, **kwargs: Any) -> str:
        """Render the Python method call statement string for emission."""
        utility_name = getattr(self.owner, "utility_name", self.owner.__name__.lower())
        return emittable.render_method_call(
            utility_name=utility_name,
            method_name=self.func.__name__,
            args=args,
            kwargs=kwargs,
        )


class BoundEmittableMethod(Generic[P, R]):
    """Bound emittable method wrapper (accessed via Instance)."""

    def __init__(self, func: Callable[P, R], instance: Any, owner: Any) -> None:
        self.func = func
        self.instance = instance
        self.owner = owner

    def __call__(self, *args: P.args, **kwargs: P.kwargs) -> R:
        # Standard bound method call: self.instance is already bound
        return self.func(self.instance, *args, **kwargs)

    def render(self, *args: Any, **kwargs: Any) -> str:
        """Render the Python method call statement string for emission."""
        utility_name = getattr(self.owner, "utility_name", self.owner.__name__.lower())
        return emittable.render_method_call(
            utility_name=utility_name,
            method_name=self.func.__name__,
            args=args,
            kwargs=kwargs,
        )
