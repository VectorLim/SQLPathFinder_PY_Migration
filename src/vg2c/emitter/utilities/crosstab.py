"""apply_crosstab — pivot utility for DataFrames (embeddable)."""

from __future__ import annotations

import re
from typing import Any, Callable

import pandas as pd

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import register_utility

__all__ = [
    "CROSSTAB_RE",
    "apply_crosstab",
    "substitute_crosstab",
    "CrosstabUtility",
]

CROSSTAB_RE = re.compile(
    r"(?P<prefix>,?)\s*CrossTab->\[\[\s*(?P<alias>[A-Za-z_][A-Za-z0-9_]*)\s*,\s*(?P<instance>[^;\]]+)\s*;\s*:(?P<mode>[YyNn])\s*\]\](?P<suffix>,?)"
)


def _extract_selected_columns_by_alias(sql: str) -> dict[str, set[str]]:
    by_alias: dict[str, set[str]] = {}
    match = re.search(
        r"\bSELECT\b(?P<select_part>.*?)\bFROM\b",
        sql,
        flags=re.IGNORECASE | re.DOTALL,
    )
    if not match:
        return by_alias

    select_part = match.group("select_part")
    col_ref_re = re.compile(
        r"\b([A-Za-z_][A-Za-z0-9_]*)\s*\.\s*(?:\[([^\]]+)\]|\"([^\"]+)\"|([A-Za-z_][A-Za-z0-9_]*))"
    )
    for col_match in col_ref_re.finditer(select_part):
        alias = col_match.group(1).lower()
        col_name = col_match.group(2) or col_match.group(3) or col_match.group(4)
        if not col_name:
            continue
        by_alias.setdefault(alias, set()).add(col_name.lower())

    return by_alias


def substitute_crosstab(
    sql: str,
    alias_columns_lookup: Callable[[str], list[str]] | None = None,
) -> str:
    if alias_columns_lookup is None or "CrossTab->[[" not in sql:
        return sql

    selected_by_alias = _extract_selected_columns_by_alias(sql)

    def _replace(match: re.Match[str]) -> str:
        prefix = match.group("prefix")
        suffix = match.group("suffix")
        alias = match.group("alias").strip()
        mode = match.group("mode").upper()
        all_cols = alias_columns_lookup(alias)
        selected = selected_by_alias.get(alias.lower(), set())
        dynamic_cols = [c for c in all_cols if c.lower() not in selected]

        if not dynamic_cols:
            return ""

        if mode == "N":
            body = ",".join(dynamic_cols)
            return f"{prefix}{body}{suffix}"

        return "\n         ,".join(f"{alias}.[{c}] AS [{c}]" for c in dynamic_cols)

    return CROSSTAB_RE.sub(_replace, sql)


def apply_crosstab(
    rows: Any,
    row_keys: list[str],
    header_key: str,
    value_key: str,
) -> Any:
    # ! To be DELETED
    pass


@register_utility
class CrosstabUtility(UtilitySpec):
    utility_name = "crosstab"
    utility_imports = (
        "from typing import Any",
        "import pandas as pd",
    )

    def apply(
        self,
        rows: pd.DataFrame,
        row_keys: list[str],
        header_key: str,
        value_key: str,
    ) -> Any:
        """Pivot row-oriented data into SQLPathFinder-style crosstab output."""
        if rows.empty or not row_keys or not header_key or not value_key:
            return pd.DataFrame(columns=row_keys)

        ci_lookup = {str(c).casefold(): c for c in rows.columns}
        rename_map = {
            ci_lookup[k.casefold()]: k for k in (*row_keys, header_key, value_key)
        }
        df = rows.rename(columns=rename_map)

        df = df[df[header_key].notna() & (df[header_key].astype(str) != "")]
        if df.empty:
            return pd.DataFrame(columns=row_keys)

        result = (
            df.groupby([*row_keys, header_key], dropna=False)[value_key]
            .first()
            .unstack(header_key, fill_value="")
            .reset_index()
            .rename_axis(columns=None)
        )
        result.columns = [str(col).lower() for col in result.columns]
        return result
