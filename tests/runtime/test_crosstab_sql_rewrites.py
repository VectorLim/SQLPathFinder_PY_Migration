"""Preserve legacy CrossTab SQL projection substitution coverage."""
from vg2c.utilities.crosstab import CrosstabUtility


def test_substitute_sql_expands_crosstab_projection():
    sql = "SELECT a0.[facility], CrossTab->[[a0,15507;:Y]] FROM [t] a0"
    out = CrosstabUtility.substitute_sql(
        sql,
        alias_columns_lookup=lambda alias: [
            "facility",
            "SUBPLANEANGLEX",
            "SUBPLANEANGLEY",
        ],
    )
    assert "CrossTab->" not in out
    assert "a0.[SUBPLANEANGLEX] AS [SUBPLANEANGLEX]" in out
    assert "a0.[SUBPLANEANGLEY] AS [SUBPLANEANGLEY]" in out


def test_CrosstabUtility_substitute_sql_header_mode_n():
    sql = "CrossTab->[[a0,15507;:N]]"
    out = CrosstabUtility.substitute_sql(
        sql,
        alias_columns_lookup=lambda alias: ["SUBPLANEANGLEX", "SUBPLANEANGLEY"],
    )
    assert out == "SUBPLANEANGLEX,SUBPLANEANGLEY"


