from __future__ import annotations

import sys
from pathlib import Path

from vg2c.emitter.utilities import (
    _scan_imports_and_dependencies,
    assemble_all_utilities,
)


def test_scan_imports_collects_external_and_filters_vg2c() -> None:
    csv_io_path = (
        Path(__file__).resolve().parents[2]
        / "src"
        / "vg2c"
        / "emitter"
        / "utilities"
        / "csv_io.py"
    )

    imports, deps, helpers = _scan_imports_and_dependencies(
        csv_io_path,
        current_name="csv_io",
        module_to_name={},
    )

    assert deps == set()
    assert "vg2c.emitter.utilities._base" in helpers
    assert "import csv" in imports
    assert "from pathlib import Path" in imports
    assert "from typing import Any, Iterator" in imports
    assert "import pandas" in imports
    assert all("vg2c." not in line for line in imports)


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
