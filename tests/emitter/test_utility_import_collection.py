from __future__ import annotations

import ast
import sys
from pathlib import Path

from vg2c.frontend.models import BlockOptions, ClassifiedBlock, ParsedBlock, SourceSpan
from vg2c.kind import Kind
from vg2c.resolver.models import ResolvedBlock
from vg2c.utilities import (
    _scan_imports_and_dependencies,
    assemble_all_utilities,
)
from vg2c.utilities._base import UtilitySpec


def test_scan_imports_collects_external_and_filters_vg2c() -> None:
    csv_io_path = Path(__file__).resolve().parents[2] / "src" / "vg2c" / "utilities" / "csv_io.py"

    info = _scan_imports_and_dependencies(
        csv_io_path,
        current_name="csv_io",
        module_to_name={},
    )

    assert info.dependencies == set()
    assert "vg2c.utilities._base" in info.helper_modules
    assert "import csv" in info.external_imports
    assert "from pathlib import Path" in info.external_imports
    assert "from typing import Any" in info.external_imports
    assert "from collections.abc import Iterator" in info.external_imports
    assert "import pandas" in info.external_imports
    assert all("vg2c." not in line for line in info.external_imports)


def test_nested_unconditional_import_is_promoted_and_stripped(tmp_path) -> None:
    module_path = tmp_path / "sample_utility.py"
    module_path.write_text(
        "class Sample:\n"
        "    def build(self):\n"
        "        from pathlib import Path\n"
        "        return Path('.')\n"
        "\n"
        "    def optional(self):\n"
        "        try:\n"
        "            import ujson as json\n"
        "        except ImportError:\n"
        "            import json\n"
        "        return json\n",
        encoding="utf-8",
    )

    info = _scan_imports_and_dependencies(module_path, current_name="sample", module_to_name={})

    assert "from pathlib import Path" in info.external_imports
    assert "from pathlib import Path" not in info.cleaned_source
    assert "pass" in info.cleaned_source
    assert "import ujson as json" in info.cleaned_source
    assert "except ImportError" in info.cleaned_source
    assert "import ujson as json" not in info.external_imports

    ast.parse(info.cleaned_source)


def test_assemble_all_utilities_imports_are_deduped_and_grouped() -> None:
    import_lines, _ = assemble_all_utilities()

    non_empty = [line for line in import_lines if line]
    assert len(non_empty) == len(set(non_empty))
    assert all(not line.startswith("from vg2c.") for line in non_empty)
    assert all(not line.startswith("import vg2c") for line in non_empty)

    roots = []
    for line in non_empty:
        if line.startswith("import "):
            module = line[len("import ") :].split(" as ", 1)[0]
        else:
            module = line.split()[1]
        roots.append(module.split(".", 1)[0])

    first_third_party_index = next(
        (
            i
            for i, root in enumerate(roots)
            if root not in sys.stdlib_module_names and root != "__future__"
        ),
        None,
    )
    if first_third_party_index is not None:
        assert all(
            root in sys.stdlib_module_names or root == "__future__"
            for root in roots[:first_third_party_index]
        )


def _make_utility_block(
    index: int, utilities: str, body: str = "", kind: Kind = Kind.EMAIL
) -> ResolvedBlock:
    options = BlockOptions.from_pairs([("UTILITIES", utilities)])
    parsed = ParsedBlock(
        index=index,
        options=options,
        body=body,
        raw="",
        span=SourceSpan(None, 1, 1),
    )
    classified = ClassifiedBlock(parsed, kind, "test")
    return ResolvedBlock(classified, options, body, None, 0)


def test_emit_block_routes_email_utility_before_generic_fallback() -> None:
    block = _make_utility_block(
        8,
        '@EXEDIR@\\SQLPathFinder_Email.va "report.csv" "self" "Subject" '
        '"body.txt" "user@example.com" "" "" "N" "N"',
    )

    emission = UtilitySpec.dispatch_and_emit(block)

    assert "def step_0008_email(ctx)" in emission.source
    expected = (
        "ctx.email.send(to='user@example.com', subject='Subject', body='body.txt', "
        "attachments=['report.csv'])"
    )
    assert expected in emission.source
    assert emission.call_site == "step_0008_email(ctx)"


def test_emit_block_keeps_unknown_utility_fallback() -> None:
    block = _make_utility_block(9, '@EXEDIR@\\SomeOtherUtility.va "x"', kind=Kind.UNKNOWN)

    emission = UtilitySpec.dispatch_and_emit(block)

    assert "utility command not classified" in emission.source
    assert emission.call_site == "step_0009_utility(ctx)"
