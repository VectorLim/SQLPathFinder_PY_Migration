from __future__ import annotations

import re

from vg2c.dispatch.models import Dialect, DispatchConfig
from vg2c.frontend.models import Diagnostic, SourceSpan

_OASYS_PLACEHOLDER = "@OASYSSCHEMA@"
_MARS_PLACEHOLDER = "@[]@"
_MARS_MISSING_DOT_PATTERN = re.compile(r"@\[\]@(?=[A-Za-z_])")
# SQL macro placeholders from Stage 2 must not be touched
# @@SQLMACRO:n@@ — the double-@ prefix / suffix distinguishes them


def substitute(
    body: str,
    dialect: Dialect,
    config: DispatchConfig | None,
    span: SourceSpan | None,
    block_index: int,
) -> tuple[str, list[Diagnostic]]:
    """Rewrite schema placeholders in *body* according to *dialect* and *config*.

    Rules:
    - @OASYSSCHEMA@ in an OASYS block → replace with ``config.oasys_schema + "."``
      when schema is non-empty; otherwise emit diagnostic and leave placeholder.
    - @OASYSSCHEMA@ in a non-OASYS block → dialect-mismatch warning, leave as-is.
    - @[]@ in a non-MARS block → dialect-mismatch warning, leave as-is.
    - In MARS blocks, normalize malformed ``@[]@F_*`` to ``@[]@.F_*``.
    - @@SQLMACRO:n@@ tokens are never touched.
    """
    diags: list[Diagnostic] = []

    if dialect == "oracle_mars":
        body = _MARS_MISSING_DOT_PATTERN.sub("@[]@.", body)

    has_oasys_token = _OASYS_PLACEHOLDER in body
    has_mars_token = _MARS_PLACEHOLDER in body

    # Dialect mismatch checks (always run regardless of config)
    if has_mars_token and dialect != "oracle_mars":
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

    if has_oasys_token and dialect != "oracle_oasys":
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

    # No OASYS token to substitute → return body unchanged
    if not has_oasys_token:
        return body, diags

    # @OASYSSCHEMA@ in a non-OASYS block: mismatch already noted; leave as-is
    if dialect != "oracle_oasys":
        return body, diags

    # OASYS block with @OASYSSCHEMA@ — attempt substitution
    if config is None:
        diags.append(
            Diagnostic(
                severity="error",
                code="dispatch-oasys-schema-unset",
                message=(
                    "@OASYSSCHEMA@ placeholder present but no DispatchConfig provided; "
                    "placeholder left in place."
                ),
                block_index=block_index,
                span=span,
            )
        )
        return body, diags

    if config.oasys_schema == "":
        diags.append(
            Diagnostic(
                severity="warning",
                code="dispatch-oasys-schema-unset",
                message=(
                    "@OASYSSCHEMA@ placeholder present but oasys_schema is empty; "
                    "placeholder left in place."
                ),
                block_index=block_index,
                span=span,
            )
        )
        return body, diags

    # Non-empty schema: global replace (M2: multiple occurrences per body)
    new_body = body.replace(_OASYS_PLACEHOLDER, config.oasys_schema + ".")
    return new_body, diags
