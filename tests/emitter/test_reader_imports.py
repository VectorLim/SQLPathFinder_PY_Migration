from __future__ import annotations

import pytest

from vg2c.dispatch.models import ReaderSpec
from vg2c.emitter import _reader_import_or_root, _resolve_reader_imports_and_roots


class _FakeBlock:
    def __init__(self, reader: ReaderSpec) -> None:
        self.reader = reader


def test_sqlite_only_yields_no_import_and_embeds_reader() -> None:
    sqlite = ReaderSpec(
        module="vg2c.utilities.sqlite_reader",
        name="SqliteReader",
        utility_name="sqlite_reader",
    )
    imports, roots = _resolve_reader_imports_and_roots((_FakeBlock(sqlite),))

    assert imports == set()
    assert roots == {"sqlite_reader"}


def test_third_party_reader_yields_plain_import() -> None:
    reader = ReaderSpec(module="datasyncx.readers.fake_reader", name="FakeReader")
    imports, roots = _resolve_reader_imports_and_roots((_FakeBlock(reader),))

    assert imports == {"from datasyncx.readers.fake_reader import FakeReader"}
    assert roots == set()


def test_both_readers_together_produce_both_without_duplicates() -> None:
    sqlite = ReaderSpec(
        module="vg2c.utilities.sqlite_reader",
        name="SqliteReader",
        utility_name="sqlite_reader",
    )
    external = ReaderSpec(module="datasyncx.readers.fake_reader", name="FakeReader")
    imports, roots = _resolve_reader_imports_and_roots(
        (_FakeBlock(sqlite), _FakeBlock(external), _FakeBlock(external))
    )

    assert imports == {"from datasyncx.readers.fake_reader import FakeReader"}
    assert roots == {"sqlite_reader"}


def test_no_dispatched_blocks_yields_nothing() -> None:
    imports, roots = _resolve_reader_imports_and_roots(())

    assert imports == set()
    assert roots == set()


def test_unregistered_local_reader_raises_clear_diagnostic() -> None:
    reader = ReaderSpec(module="vg2c.dispatch.dialects.fake", name="FakeReader")

    with pytest.raises(ValueError, match="has no utility_name"):
        _reader_import_or_root(reader)
