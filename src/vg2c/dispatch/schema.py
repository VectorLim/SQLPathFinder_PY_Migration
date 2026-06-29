from __future__ import annotations

# Compatibility shim - delegates to dialect handlers
from vg2c.dispatch.dialects.aries import AriesDialect
from vg2c.dispatch.dialects.mars import MarsDialect
from vg2c.dispatch.dialects.oasys import OasysDialect
from vg2c.dispatch.dialects.sqlite import SqliteDialect
from vg2c.dispatch.models import Dialect, DispatchConfig
from vg2c.frontend.models import Diagnostic, SourceSpan

_OASYS_PLACEHOLDER = "@OASYSSCHEMA@"
_MARS_PLACEHOLDER = "@[]@"


def substitute(
    body: str,
    dialect: Dialect,
    config: DispatchConfig | None,
    span: SourceSpan | None,
    block_index: int,
) -> tuple[str, list[Diagnostic]]:
    """Rewrite schema placeholders in *body* according to *dialect* and *config*."""
    diags: list[Diagnostic] = []

    # Legacy cross-dialect mismatch checks
    has_mars_token = _MARS_PLACEHOLDER in body
    has_oasys_token = _OASYS_PLACEHOLDER in body

    if has_mars_token and dialect != MarsDialect.dialect:
        diags.append(
            Diagnostic(
                severity="warning",
                code="dispatch-placeholder-dialect-mismatch",
                message=(
                    f"@[]@ schema placeholder found in {dialect} block; "
                    "expected oracle_mars context."
                ),
                block_index=block_index,
                span=span,
            )
        )

    if has_oasys_token and dialect != OasysDialect.dialect:
        diags.append(
            Diagnostic(
                severity="warning",
                code="dispatch-placeholder-dialect-mismatch",
                message=(
                    f"@OASYSSCHEMA@ schema placeholder found in {dialect} block; "
                    "expected oracle_oasys context."
                ),
                block_index=block_index,
                span=span,
            )
        )

    # Delegate to appropriate handler
    if dialect == MarsDialect.dialect:
        rewritten, handler_diags = MarsDialect.substitute(
            body, config, span, block_index
        )
        diags.extend(handler_diags)
        return rewritten, diags
    if dialect == OasysDialect.dialect:
        rewritten, handler_diags = OasysDialect.substitute(
            body, config, span, block_index
        )
        diags.extend(handler_diags)
        return rewritten, diags
    if dialect == AriesDialect.dialect:
        rewritten, handler_diags = AriesDialect.substitute(
            body, config, span, block_index
        )
        diags.extend(handler_diags)
        return rewritten, diags
    if dialect == SqliteDialect.dialect:
        rewritten, handler_diags = SqliteDialect.substitute(
            body, config, span, block_index
        )
        diags.extend(handler_diags)
        return rewritten, diags

    return body, diags
