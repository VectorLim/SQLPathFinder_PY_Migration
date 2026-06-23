from __future__ import annotations

import dataclasses
import json
import os
from pathlib import Path

from vg2c.classifier import classify_all
from vg2c.frontend.parser import parse_vg2


def test_snapshot_script_short(FIXTURES: Path, SNAPSHOTS: Path) -> None:
    """Test classification snapshot for script_short.txt."""
    _verify_snapshot(FIXTURES / "script_short.txt", SNAPSHOTS)


def test_snapshot_script_another(FIXTURES: Path, SNAPSHOTS: Path) -> None:
    """Test classification snapshot for script_another.txt."""
    _verify_snapshot(FIXTURES / "script_another.txt", SNAPSHOTS)


def test_snapshot_sql_script(FIXTURES: Path, SNAPSHOTS: Path) -> None:
    """Test classification snapshot for sql_script.txt."""
    _verify_snapshot(FIXTURES / "sql_script.txt", SNAPSHOTS)


def _verify_snapshot(fixture_path: Path, snapshot_dir: Path) -> None:
    """Verify classification matches stored snapshot or generate if UPDATE_SNAPSHOTS=1."""
    # Ensure paths are absolute to avoid working directory issues
    fixture_path = fixture_path.resolve()
    snapshot_dir = snapshot_dir.resolve()
    
    blocks = parse_vg2(fixture_path)
    classification = classify_all(blocks)

    snapshot_file = snapshot_dir / f"{fixture_path.stem}_classification.json"

    # Normalize via JSON round-trip so enums become strings consistently
    actual = json.loads(json.dumps(dataclasses.asdict(classification), default=str))

    # Generate snapshot if UPDATE_SNAPSHOTS env var is set
    if os.environ.get("UPDATE_SNAPSHOTS") == "1":
        snapshot_dir.mkdir(parents=True, exist_ok=True)
        with snapshot_file.open("w", encoding="utf-8") as f:
            json.dump(actual, f, indent=2, sort_keys=True)
        return

    # Verify snapshot exists
    if not snapshot_file.exists():
        raise FileNotFoundError(
            f"Snapshot file missing: {snapshot_file}. "
            f"Run with UPDATE_SNAPSHOTS=1 to generate. "
            f"Current directory: {Path.cwd()}"
        )

    # Load and compare
    with snapshot_file.open("r", encoding="utf-8") as f:
        expected = json.load(f)

    # Add detailed error message for debugging
    if actual != expected:
        # Find first difference for better error message
        if len(actual.get("blocks", [])) != len(expected.get("blocks", [])):
            raise AssertionError(
                f"Classification mismatch for {fixture_path.name}: "
                f"Expected {len(expected.get('blocks', []))} blocks, "
                f"got {len(actual.get('blocks', []))} blocks. "
                f"Run with UPDATE_SNAPSHOTS=1 to update."
            )
        raise AssertionError(
            f"Classification mismatch for {fixture_path.name}. "
            f"Run with UPDATE_SNAPSHOTS=1 to update. "
            f"Snapshot file: {snapshot_file}"
        )
