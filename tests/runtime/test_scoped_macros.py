"""The direct generated runtime owns a fresh lexical macro stack per run."""

import pytest

from vg2c.runtime import MacroStore
from vg2c.runtime.controls import for_values, site_values


def test_named_lookup_write_missing_and_values_namespace():
    macros = MacroStore(
        values={"CL_SITE": "KM", "%ENV_NAME%": "one", "SPF-JOB-START-DAY": "today"},
        initial={"site": "PG", "empty": None, "blank": ""},
    )
    assert macros["SITE"] == "PG"
    assert macros["empty"] == ""
    assert macros["BLANK"] == ""
    macros["Site"] = "VN"
    assert macros["site"] == "VN"
    assert macros["CL_SITE"] == "KM"
    assert macros["%ENV_NAME%"] == "one"
    assert macros["SPF-JOB-START-DAY"] == "today"
    assert macros.substitute("<<<site>>>/<<<CL_SITE>>>/<<<%ENV_NAME%>>>") == "VN/KM/one"
    with pytest.raises(ValueError, match="Unknown value"):
        _ = macros["UNKNOWN"]
    with pytest.raises(ValueError, match="Unknown value"):
        macros.substitute("<<<UNKNOWN>>>")
    assert macros.substitute("<<<SPF-UNKNOWN>>>") == "<<<SPF-UNKNOWN>>>"
    assert macros.get("missing", "fallback") == "fallback"


def test_nested_scope_shadow_mutation_exception_and_newlines():
    macros = MacroStore(initial={"LABEL": "base"})
    with macros.scope({"label": "outer", "NONE": None}):
        assert macros["label"] == "outer"
        with pytest.raises(RuntimeError):
            with macros.scope({"LABEL": "inner"}):
                macros["ONLY_INNER"] = "local"
                assert macros.substitute("\n<<<LABEL>>>:<<<NONE>>>") == "\ninner:"
                raise RuntimeError("probe")
        assert macros["label"] == "outer"
        with pytest.raises(ValueError, match="Unknown value"):
            _ = macros["ONLY_INNER"]
    assert macros["label"] == "base"
    with pytest.raises(ValueError, match="Unknown value"):
        _ = macros["NONE"]


def test_independent_jobs_loop_frames_and_failure_diagnostics():
    first = MacroStore(initial={"VALUE": "one"})
    second = MacroStore(initial={"VALUE": "two"})
    with first.scope():
        first["VALUE"] = "local"
    assert first["VALUE"] == "one"
    assert second["VALUE"] == "two"
    for row in site_values("PG,KM"):
        with first.scope(row):
            assert first["SPF-SITE"] in {"PG", "KM"}
            first["temporary"] = "loop-only"
    assert first["SPF-SITE"] == "<<<SPF-SITE>>>"  # Reserved SPF tokens remain literal.
    with pytest.raises(ValueError, match="Unknown value"):
        _ = first["temporary"]
    rows = list(for_values("0", "2", "1", "A", "N"))
    for row in rows:
        with first.scope(row):
            assert first["SPF-LOOP-CTR-A"] == row["SPF-LOOP-CTR-A"]
    with pytest.raises(ValueError, match="Positional macro"):
        first.substitute("<<>>")
    with pytest.raises(ValueError, match="Missing closing"):
        first.substitute("<<<BROKEN")
    with pytest.raises(ValueError, match="Unknown value"):
        first.substitute("<<<%BROKEN>>>")
