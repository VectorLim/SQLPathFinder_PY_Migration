"""Unknown utilities fail at emission; no redundant UNKNOWN handler is needed."""

from __future__ import annotations

import pytest

from vg2c import compile_document
from vg2c.kind import Kind
from vg2c.utilities import ensure_utility_checks_loaded
from vg2c.utilities._base import UtilitySpec


def test_unrecognized_utility_is_rejected_at_emission(tmp_path):
    source = tmp_path / "source.txt"
    source.write_text(
        "<OPTIONS>\n/UTILITIES=not_a_supported_utility\n</OPTIONS>\n",
        encoding="utf-8",
    )
    with pytest.raises(ValueError, match="Unclassified utility is unsupported"):
        compile_document(source)


def test_unknown_kind_has_no_noop_emission_handler():
    ensure_utility_checks_loaded()
    assert Kind.UNKNOWN not in UtilitySpec._emit_handlers
    assert UtilitySpec.for_name("utility") is None
