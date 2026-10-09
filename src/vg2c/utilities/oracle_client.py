"""Compiler registration for the dedicated Oracle client setup."""
from vg2c.runtime.oracle_client import _OracleClient as RuntimeOracleClient
from vg2c.utilities._base import UtilitySpec

class OracleClient(RuntimeOracleClient, UtilitySpec):
    utility_name = "oracle_client"
