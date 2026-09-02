"""Validation for the supported DataSyncX Oracle client setup."""

from __future__ import annotations

import os
from pathlib import Path

from vg2c.utilities._base import UtilitySpec


class OracleClient(UtilitySpec):
    """Validate the approved full Oracle Client before DataSyncX uses it."""

    utility_name = "oracle_client"
    _REQUIRED_NET_FILES = ("tnsnames.ora", "sqlnet.ora")
    _SQLPATHFINDER_NET_SOURCE = Path(r"C:\Oracle\network")

    @classmethod
    def configure(cls) -> str:
        """Return the validated Oracle Home required by DataSyncX.

        The installed full Oracle Client reads its network configuration from
        ``ORACLE_HOME\\network\\admin``. No environment variables are changed.
        """

        configured_home = os.getenv("ORACLE_HOME")
        if not configured_home:
            raise RuntimeError(
                "ORACLE_HOME is not configured. Set ORACLE_HOME to the approved "
                "full Oracle Client root, for example "
                r"C:\Oracle\Product\11.2.0\client_k64."
            )

        oracle_home = Path(configured_home).expanduser()
        if not (oracle_home / "bin" / "oci.dll").is_file():
            raise RuntimeError(
                f"ORACLE_HOME is invalid: {oracle_home}. It must point to the "
                "full Oracle Client root containing bin\\oci.dll."
            )

        network_dir = oracle_home / "network" / "admin"
        missing_files = [
            filename
            for filename in cls._REQUIRED_NET_FILES
            if not (network_dir / filename).is_file()
        ]
        if missing_files:
            raise RuntimeError(
                "Oracle Net configuration is incomplete. Expected "
                f"{', '.join(missing_files)} in {network_dir}. Copy the provided "
                "SQLPathFinder Oracle Net files from "
                f"{cls._SQLPATHFINDER_NET_SOURCE} to {network_dir}."
            )

        return str(oracle_home)
