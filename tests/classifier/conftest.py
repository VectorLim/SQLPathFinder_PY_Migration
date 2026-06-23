from __future__ import annotations

from pathlib import Path

import pytest

from vg2c.model import ParsedBlock, SourceSpan


@pytest.fixture(scope="session")
def FIXTURES() -> Path:
    """Return path to fixture directory."""
    return Path(__file__).parent.parent / "fixtures"


@pytest.fixture(scope="session")
def SNAPSHOTS() -> Path:
    """Return path to classification snapshot directory."""
    return Path(__file__).parent.parent / "fixtures" / "classification"


def make_block(options: dict[str, str], body: str = "", index: int = 0) -> ParsedBlock:
    """Create a synthetic parsed block for testing."""
    span = SourceSpan(file="<synthetic>", start_line=1, end_line=1)
    return ParsedBlock(index=index, span=span, options=options, body=body, raw="")
