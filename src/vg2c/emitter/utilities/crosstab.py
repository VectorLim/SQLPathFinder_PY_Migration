"""apply_crosstab — pivot utility for DataFrames (embeddable)."""

from __future__ import annotations

from typing import Any

import pandas as pd

from vg2c.emitter.utilities._registry import register_utility


@register_utility(
    "crosstab",
    imports=(
        "from typing import Any",
        "import pandas as pd",
    ),
)
def apply_crosstab(
    rows: pd.DataFrame,  # pandas.DataFrame
    row_keys: list[str],
    header_key: str,
    value_key: str,
) -> Any:  # pandas.DataFrame
    """Pivot row-oriented data into SQLPathFinder-style crosstab output.

    Args:
        rows: pandas DataFrame.
        row_keys: Grouping columns (``/CTROW``).
        header_key: Dynamic column source (``/CTHEADER``).
        value_key: Dynamic value source (``/CTVALUE``).

    Returns:
        pandas DataFrame with pivoted data, including row_keys as columns.
    """

    if rows.empty or not row_keys or not header_key or not value_key:
        return pd.DataFrame(columns=row_keys)

    # Resolve requested keys against actual columns case-insensitively
    # (Oracle may uppercase names) and rename to the requested casing.
    ci_lookup = {str(c).casefold(): c for c in rows.columns}
    rename_map = {
        ci_lookup[k.casefold()]: k for k in (*row_keys, header_key, value_key)
    }
    df = rows.rename(columns=rename_map)

    df = df[df[header_key].notna() & (df[header_key].astype(str) != "")]
    if df.empty:
        return pd.DataFrame(columns=row_keys)

    # dropna=False so rows with NaN in any row_key are preserved
    # (groupby's default would silently drop them, yielding an empty result
    # when even one row_key column has NaN).
    result = (
        df.groupby([*row_keys, header_key], dropna=False)[value_key]
        .first()
        .unstack(header_key, fill_value="")
        .reset_index()
        .rename_axis(columns=None)
    )
    result.columns = [str(col).lower() for col in result.columns]
    return result
