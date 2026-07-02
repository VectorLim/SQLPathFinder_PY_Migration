"""apply_crosstab — pivot utility for DataFrames (embeddable)."""

from __future__ import annotations

from typing import Any

import pandas as pd

from vg2c.emitter.utilities._base import UtilitySpec
from vg2c.emitter.utilities._registry import register_utility


@register_utility
class CrosstabUtility(UtilitySpec):
    utility_name = "crosstab"
    utility_imports = (
        "from typing import Any",
        "import pandas as pd",
    )

    @classmethod
    def emit(
        cls,
        ctx,
        block,
        dispatched,
    ) -> tuple[str, str]:
        raise NotImplementedError("CrosstabUtility has no direct block emitter")

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
