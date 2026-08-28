"""Opt-in Oracle Instant Client setup for DataSyncX-generated workflows."""

from __future__ import annotations

import os
import sys
from pathlib import Path

from vg2c.utilities._base import UtilitySpec


class OracleClient(UtilitySpec):
    """Select an Oracle client before DataSyncX opens its first connection."""

    utility_name = "oracle_client"

    @classmethod
    def configure(cls) -> str | None:
        """Prepare the current process for the configured DataSyncX Oracle client.

        Set ``DATASYNCX_ORACLE_CLIENT=instant`` to opt in.  The normal
        ORACLE_HOME-based setup remains untouched when it is unset or ``home``.
        """

        mode = os.getenv("DATASYNCX_ORACLE_CLIENT", "home").strip().lower()
        if mode in {"", "home"}:
            return None
        if mode != "instant":
            raise RuntimeError(
                "DATASYNCX_ORACLE_CLIENT must be 'home' or 'instant', "
                f"not {mode!r}."
            )
        if sys.platform != "win32":
            raise RuntimeError(
                "DataSyncX 1.1.6 initializes python-oracledb without lib_dir. "
                "On Linux, configure Instant Client with ldconfig (preferred) or "
                "LD_LIBRARY_PATH before starting Python; on macOS, update DataSyncX "
                "to pass lib_dir before using this selector."
            )

        client_dir = cls._find_instant_client()
        network_dir = cls._configure_network_files(client_dir)
        if network_dir is not None:
            import oracledb

            oracledb.defaults.config_dir = str(network_dir)
        os.environ.pop("ORACLE_HOME", None)
        cls._prepend_path(client_dir)
        return str(client_dir)

    @staticmethod
    def _find_instant_client() -> Path:
        configured = (
            os.getenv("DATASYNCX_INSTANT_CLIENT_DIR")
            or os.getenv("ORACLE_INSTANT_CLIENT_DIR")
        )
        candidates = (
            [configured] if configured else os.getenv("PATH", "").split(os.pathsep)
        )
        for candidate in candidates:
            if not candidate:
                continue
            path = Path(candidate).expanduser()
            if "instantclient" in path.name.lower() and (path / "oci.dll").is_file():
                return path.resolve()

        raise RuntimeError(
            "Oracle Instant Client was requested but no usable directory was "
            "found. "
            "Set DATASYNCX_INSTANT_CLIENT_DIR to the directory containing "
            "oci.dll."
        )

    @staticmethod
    def _configure_network_files(client_dir: Path) -> Path | None:
        configured = os.getenv("DATASYNCX_ORACLE_NET_CONFIG_DIR")
        network_dir = (
            Path(configured).expanduser()
            if configured
            else client_dir / "network" / "admin"
        )
        if configured and not network_dir.is_dir():
            raise RuntimeError(
                "DATASYNCX_ORACLE_NET_CONFIG_DIR does not exist or is not a "
                "directory: "
                f"{network_dir}"
            )
        if network_dir.is_dir():
            network_dir = network_dir.resolve()
            os.environ["TNS_ADMIN"] = str(network_dir)
            return network_dir
        return None

    @staticmethod
    def _prepend_path(client_dir: Path) -> None:
        entries = [
            entry for entry in os.getenv("PATH", "").split(os.pathsep) if entry
        ]
        selected = str(client_dir)
        os.environ["PATH"] = os.pathsep.join(
            [selected, *(entry for entry in entries if Path(entry) != client_dir)]
        )
