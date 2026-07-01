"""``FunctionDef`` — builder for the one-call step functions emitted per block."""

from __future__ import annotations

import re
from dataclasses import dataclass

from vg2c.emitter.codegen.call_spec import CallSpec
from vg2c.emitter.codegen.constants import CTX_PARAM, CTX_VAR

__all__ = ["FunctionDef"]


_SLUG_RE = re.compile(r"[^a-z0-9]+")


def _strip_quotes(value: str) -> str:
    value = value.strip()
    if len(value) >= 2 and value[0] == value[-1] and value[0] in {'"', "'"}:
        return value[1:-1]
    return value


@dataclass(frozen=True, slots=True)
class FunctionDef:
    """One emitted step function (``def step_NNNN_…(ctx): …``)."""

    name: str
    body: tuple[str, ...]

    @classmethod
    def from_call(
        cls, name: str, call: CallSpec, *, multiline: bool = False
    ) -> "FunctionDef":
        if multiline and (call.args or call.kwargs):
            body = cls._render_multiline_call(call)
        else:
            body = (call.render(),)
        return cls(name=name, body=body)

    @classmethod
    def from_body(cls, name: str, body_lines: list[str]) -> "FunctionDef":
        return cls(name=name, body=tuple(body_lines))

    @staticmethod
    def _render_multiline_call(call: CallSpec) -> tuple[str, ...]:
        prefix = f"{call.receiver}.{call.method}" if call.receiver else call.method
        lines: list[str] = [f"{prefix}("]
        for arg in call.args:
            lines.append(f"    {arg.source},")
        for key, value in call.kwargs.items():
            lines.append(f"    {key}={value.source},")
        lines.append(")")
        return tuple(lines)

    @property
    def source(self) -> str:
        body_lines = self.body or ("pass",)
        indented = "\n".join(f"    {line}" for line in body_lines)
        return f"def {self.name}({CTX_PARAM}):\n{indented}\n"

    @property
    def call_site(self) -> str:
        return f"{self.name}({CTX_VAR})"

    # ------------------------------------------------------------------
    # Name minting (was handlers._function_name)
    # ------------------------------------------------------------------

    @staticmethod
    def name_for(block, suffix: str) -> str:
        """Build ``step_NNNN_<slug>`` from a block's ``PROMPT-TEXT`` option."""
        prompt_text = _strip_quotes(
            block.resolved_options.lookup.get("PROMPT-TEXT", "")
        )
        slug = _SLUG_RE.sub("_", prompt_text.lower()).strip("_")
        base = slug or suffix

        prefix = f"step_{block.parsed.index:04d}_"
        max_total = 80
        keep = max(8, max_total - len(prefix))
        base = base[:keep].strip("_") or suffix
        return prefix + base
