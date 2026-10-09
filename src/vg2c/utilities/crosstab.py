"""Compiler metadata for the single-sourced runtime implementation."""

from __future__ import annotations
from vg2c.runtime.crosstab import _CrosstabUtility as RuntimeCrosstabUtility
from vg2c.utilities._base import UtilitySpec


class CrosstabUtility(RuntimeCrosstabUtility, UtilitySpec):
    utility_name = "crosstab"
