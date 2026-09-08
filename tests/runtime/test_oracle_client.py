from __future__ import annotations

import sys

import pytest

from vg2c.utilities.oracle_client import OracleClient


def test_home_mode_leaves_existing_setup_untouched(monkeypatch):
    monkeypatch.delenv("DATASYNCX_ORACLE_CLIENT", raising=False)
    monkeypatch.setenv("ORACLE_HOME", "C:/Oracle/full-client")

    assert OracleClient.configure() is None
    assert "ORACLE_HOME" in __import__("os").environ


def test_instant_client_is_selected_before_datasyncx_initialization(monkeypatch, tmp_path):
    client_dir = tmp_path / "instantclient_19_17"
    client_dir.mkdir()
    (client_dir / "oci.dll").touch()
    network_dir = tmp_path / "network"
    network_dir.mkdir()
    monkeypatch.setattr("vg2c.utilities.oracle_client.sys.platform", "win32")
    monkeypatch.setenv("DATASYNCX_ORACLE_CLIENT", "instant")
    monkeypatch.setenv("DATASYNCX_INSTANT_CLIENT_DIR", str(client_dir))
    monkeypatch.setenv("DATASYNCX_ORACLE_NET_CONFIG_DIR", str(network_dir))
    monkeypatch.setenv("ORACLE_HOME", "C:/Oracle/full-client")
    monkeypatch.setenv("PATH", "C:/other")

    assert OracleClient.configure() == str(client_dir.resolve())
    assert "ORACLE_HOME" not in __import__("os").environ
    assert __import__("os").environ["TNS_ADMIN"] == str(network_dir.resolve())
    assert __import__("os").environ["PATH"].split(";")[0] == str(client_dir.resolve())


def test_instant_client_reports_missing_library(monkeypatch, tmp_path):
    monkeypatch.setattr("vg2c.utilities.oracle_client.sys.platform", "win32")
    monkeypatch.setenv("DATASYNCX_ORACLE_CLIENT", "instant")
    monkeypatch.setenv("DATASYNCX_INSTANT_CLIENT_DIR", str(tmp_path))

    with pytest.raises(RuntimeError, match="containing oci.dll"):
        OracleClient.configure()


def test_active_client_is_logged_once(monkeypatch, capsys, tmp_path):
    class FakeOracleDb:
        @staticmethod
        def is_thin_mode():
            return False

        @staticmethod
        def clientversion():
            return (23, 26, 0, 0, 0)

    monkeypatch.setitem(sys.modules, "oracledb", FakeOracleDb)
    monkeypatch.setattr(OracleClient, "_reported_client", False)
    monkeypatch.setattr(
        OracleClient, "_selected_instant_client", tmp_path / "instantclient_23_26"
    )

    OracleClient.log_active_client()
    OracleClient.log_active_client()

    assert capsys.readouterr().out == (
        "\n"
        + "=" * 72
        + "\n"
        + " Oracle client: 23.26.0.0.0 | mode=thick | source=Instant Client "
        + f"({tmp_path / 'instantclient_23_26'})\n"
        + "=" * 72
        + "\n"
    )
