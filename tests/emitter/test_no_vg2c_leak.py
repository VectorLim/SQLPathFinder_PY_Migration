from __future__ import annotations

from pathlib import Path

import pytest

from vg2c import translate

FIXTURES = Path(__file__).parents[1] / "fixtures"


def _vg2c_import_lines(generated: str) -> list[str]:
    """Import statements referencing vg2c (excludes the harmless `<vg2c:...>` region markers)."""
    return [
        line
        for line in generated.splitlines()
        if line.strip().startswith(("import vg2c", "from vg2c"))
    ]


@pytest.mark.parametrize(
    "fixture_name",
    [
        "oasys.txt",
        "aries_simple.txt",
        "script_short.txt",
        "hamizah.txt",
        "html_test.txt",
    ],
)
def test_generated_script_never_imports_vg2c(tmp_path, fixture_name: str) -> None:
    source = tmp_path / fixture_name
    source.write_text((FIXTURES / fixture_name).read_text(encoding="utf-8"))

    output = translate(source)
    generated = output.read_text(encoding="utf-8")

    assert _vg2c_import_lines(generated) == []


def test_oracle_only_fixture_omits_other_readers(tmp_path) -> None:
    source = tmp_path / "oasys.txt"
    source.write_text((FIXTURES / "oasys.txt").read_text(encoding="utf-8"))

    generated = translate(source).read_text(encoding="utf-8")

    assert "OracleReader(" in generated
    assert "AriesReader" not in generated
    assert "MarsReader" not in generated
    # SqliteEngine handles all SQL-bearing kinds (Oracle/Aries/Mars/SQLite alike) and
    # is expected here; only the SQLite *reader* class itself must be absent.
    assert "class SqliteReader" not in generated


def test_aries_only_fixture_omits_other_readers(tmp_path) -> None:
    source = tmp_path / "aries_simple.txt"
    source.write_text((FIXTURES / "aries_simple.txt").read_text(encoding="utf-8"))

    generated = translate(source).read_text(encoding="utf-8")

    assert "AriesReader(" in generated
    assert "MarsReader" not in generated
    assert "OracleReader" not in generated
    assert "class SqliteReader" not in generated


def test_sqlite_only_fixture_embeds_reader_and_omits_external_readers(tmp_path) -> None:
    source = tmp_path / "script_short.txt"
    source.write_text((FIXTURES / "script_short.txt").read_text(encoding="utf-8"))

    generated = translate(source).read_text(encoding="utf-8")

    assert "SqliteReader(" in generated
    assert "class SqliteReader" in generated  # embedded, not imported
    assert "AriesReader" not in generated
    assert "MarsReader" not in generated
    assert "OracleReader" not in generated


def test_mixed_mars_and_sqlite_fixture_includes_both_readers(tmp_path) -> None:
    source = tmp_path / "hamizah.txt"
    source.write_text((FIXTURES / "hamizah.txt").read_text(encoding="utf-8"))

    generated = translate(source).read_text(encoding="utf-8")

    assert "MarsReader(" in generated
    assert "SqliteReader(" in generated
    assert "class SqliteReader" in generated


def test_no_db_reader_fixture_omits_all_readers(tmp_path) -> None:
    source = tmp_path / "html_test.txt"
    source.write_text((FIXTURES / "html_test.txt").read_text(encoding="utf-8"))

    generated = translate(source).read_text(encoding="utf-8")

    assert "AriesReader" not in generated
    assert "MarsReader" not in generated
    assert "OracleReader" not in generated
    assert "SqliteReader" not in generated
