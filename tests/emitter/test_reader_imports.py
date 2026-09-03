from __future__ import annotations

import pytest

from vg2c.dispatch.dialects.sqlite import SqliteReader
from vg2c.emitter import _reader_import_or_root, _resolve_reader_imports_and_roots


class _FakeBlock:
    def __init__(self, reader_cls: type) -> None:
        self.reader_cls = reader_cls


class _FakeThirdPartyReader:
    """Stand-in for a datasyncx reader class."""


_FakeThirdPartyReader.__module__ = "datasyncx.readers.fake_reader"


class _FakeLocalReader:
    """A vg2c-local reader that was never registered as a UtilitySpec."""


def test_sqlite_only_yields_no_import_and_embeds_reader() -> None:
    imports, roots = _resolve_reader_imports_and_roots((_FakeBlock(SqliteReader),))

    assert imports == set()
    assert roots == {"sqlite_reader"}


def test_third_party_reader_yields_plain_import() -> None:
    imports, roots = _resolve_reader_imports_and_roots(
        (_FakeBlock(_FakeThirdPartyReader),)
    )

    assert imports == {
        "from datasyncx.readers.fake_reader import _FakeThirdPartyReader"
    }
    assert roots == set()


def test_both_readers_together_produce_both_without_duplicates() -> None:
    imports, roots = _resolve_reader_imports_and_roots(
        (
            _FakeBlock(SqliteReader),
            _FakeBlock(_FakeThirdPartyReader),
            _FakeBlock(_FakeThirdPartyReader),
        )
    )

    assert imports == {
        "from datasyncx.readers.fake_reader import _FakeThirdPartyReader"
    }
    assert roots == {"sqlite_reader"}


def test_no_dispatched_blocks_yields_nothing() -> None:
    imports, roots = _resolve_reader_imports_and_roots(())

    assert imports == set()
    assert roots == set()


def test_unregistered_local_reader_raises_clear_diagnostic() -> None:
    _FakeLocalReader.__module__ = "vg2c.dispatch.dialects.fake"

    with pytest.raises(ValueError, match="not a registered UtilitySpec"):
        _reader_import_or_root(_FakeLocalReader)
