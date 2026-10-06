"""Legacy CSR/MMS publishing rewritten into the single {AED} flow."""

from __future__ import annotations

import codecs
from pathlib import Path

import pytest

from scripthost_portable import migrate_aed as m

ROOT = Path(__file__).resolve().parents[2]
LEGACY = ROOT / "tests" / "fixtures" / "aed_migration"


def keys(text):
    return [(b.utility, b.options.get("CSV", "").lower(), b.options.get("REPORT", ""))
            for b in m.parse(text) if b.options]


@pytest.fixture(params=["ICMPCS.txt", "CSR_IAM_v2.txt"])
def legacy(request):
    return (LEGACY / request.param).read_text(encoding="utf-8-sig")


def test_icmpcs_matches_hand_converted_reference():
    migrated = m.migrate((LEGACY / "ICMPCS.txt").read_text(encoding="utf-8-sig"))
    assert keys(migrated.text) == keys((ROOT / "ICMPCS.txt").read_text(encoding="utf-8-sig"))
    assert migrated.warnings == []


def test_only_rule_outputs_differ_from_source(legacy):
    migrated = m.migrate(legacy)
    source = {block.raw for block in m.parse(legacy)}
    new = [block for block in m.parse(migrated.text) if block.raw not in source]
    assert [block.utility or block.options.get("REPORT") or block.options["CSV"]
            for block in new] == [m.CANDIDATES, "{aed}", "{rows-in-file}", "HTML-LAYOUT"]
    candidates = new[0]
    assert candidates.options["HEADERS"] == "FACILITY,LOT"
    assert "'<<<AED_FACILITY>>>' AS [FACILITY], a0.[lot] AS [LOT]" in candidates.body
    assert new[2].args[1] == m.CANDIDATES
    assert "<<<AED_ENABLED>>>" in new[3].body and "<<<DCSR>>>" not in new[3].body
    assert not any(token in migrated.text for token in (
        "getcsrsu", "<<<CSRPATH>>>", "<<<MIPPATH>>>", "HISTERROR", "update.bat",
    ))


@pytest.mark.parametrize("name", ["new_icmpcs.txt", "maxlidheight.txt", "actual_script.txt"])
def test_scripts_without_publishing_are_untouched(FIXTURES, name):
    text = (FIXTURES / name).read_text(encoding="utf-8-sig")
    migrated = m.migrate(text)
    assert migrated.text == text and migrated.changes == []


def test_partial_pattern_is_rejected(legacy):
    broken = legacy.replace('"<<<CSRPATH>>>"', '"<<<ELSEWHERE>>>"')
    with pytest.raises(m.MigrationError, match="CSRPATH"):
        m.migrate(broken)


def test_cli_preserves_bom_and_crlf(tmp_path, capsys):
    text = (LEGACY / "CSR_IAM_v2.txt").read_text(encoding="utf-8-sig").replace("\n", "\r\n")
    source = tmp_path / "job.txt"
    source.write_bytes(codecs.BOM_UTF8 + text.encode())
    assert m.main([str(source)]) == 0
    output = (tmp_path / "job.aed.txt").read_bytes()
    assert output.startswith(codecs.BOM_UTF8)
    assert output.count(b"\n") == output.count(b"\r\n")
    assert source.read_bytes() == codecs.BOM_UTF8 + text.encode()
    assert "CSR publish" in capsys.readouterr().out


def test_cli_reports_errors_without_writing(tmp_path):
    source = tmp_path / "job.txt"
    source.write_text((LEGACY / "ICMPCS.txt").read_text(encoding="utf-8-sig")
                      .replace('"<<<MIPPATH>>>"', '"<<<ELSEWHERE>>>"'))
    assert m.main([str(source)]) == 2
    assert not (tmp_path / "job.aed.txt").exists()
