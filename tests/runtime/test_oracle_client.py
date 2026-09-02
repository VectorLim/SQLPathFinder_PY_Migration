from __future__ import annotations

from vg2c.utilities.oracle_client import OracleClient


def test_configure_accepts_a_full_client_with_oracle_net_files(monkeypatch, tmp_path):
    oracle_home = tmp_path / "client_k64"
    (oracle_home / "bin").mkdir(parents=True)
    (oracle_home / "bin" / "oci.dll").touch()
    network_dir = oracle_home / "network" / "admin"
    network_dir.mkdir(parents=True)
    for filename in ("tnsnames.ora", "sqlnet.ora"):
        (network_dir / filename).touch()
    monkeypatch.setenv("ORACLE_HOME", str(oracle_home))

    assert OracleClient.configure() == str(oracle_home)


def test_configure_requires_oracle_home(monkeypatch):
    monkeypatch.delenv("ORACLE_HOME", raising=False)

    try:
        OracleClient.configure()
    except RuntimeError as error:
        assert "ORACLE_HOME is not configured" in str(error)
    else:
        raise AssertionError("Expected ORACLE_HOME validation to fail")


def test_configure_rejects_an_invalid_oracle_home(monkeypatch, tmp_path):
    monkeypatch.setenv("ORACLE_HOME", str(tmp_path))

    try:
        OracleClient.configure()
    except RuntimeError as error:
        assert "bin\\oci.dll" in str(error)
    else:
        raise AssertionError("Expected ORACLE_HOME validation to fail")


def test_configure_explains_how_to_restore_missing_network_files(monkeypatch, tmp_path):
    oracle_home = tmp_path / "client_k64"
    (oracle_home / "bin").mkdir(parents=True)
    (oracle_home / "bin" / "oci.dll").touch()
    monkeypatch.setenv("ORACLE_HOME", str(oracle_home))

    try:
        OracleClient.configure()
    except RuntimeError as error:
        message = str(error)
        assert "tnsnames.ora" in message
        assert "sqlnet.ora" in message
        assert "C:\\Oracle\\network" in message
        assert str(oracle_home / "network" / "admin") in message
    else:
        raise AssertionError("Expected Oracle Net validation to fail")
