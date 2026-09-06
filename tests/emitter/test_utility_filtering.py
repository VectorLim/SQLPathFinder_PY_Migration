from __future__ import annotations

from vg2c.kind import Kind
from vg2c.utilities import assemble_all_utilities


def test_sqlite_only_kind_embeds_sqlite_reader_and_excludes_unused() -> None:
    # SqliteReader is a reader class, not Kind-mapped -- emitter forces it in via
    # extra_root_names, derived from the dispatched block's reader metadata.
    _, sources = assemble_all_utilities(
        required_kinds=frozenset({Kind.SQLITE_QUERY}),
        extra_root_names=frozenset({"sqlite_reader"}),
    )
    joined = "\n".join(sources)

    assert "class SqliteEngine" in joined
    assert "class SqliteReader" in joined
    assert "class PipelineContext" in joined  # fixed root
    assert "class Logger" in joined  # fixed root
    assert "class HtmlReport" not in joined
    assert "class MailService" not in joined
    assert "class ExternalProcess" not in joined


def test_email_only_kind_excludes_sql_handlers() -> None:
    _, sources = assemble_all_utilities(required_kinds=frozenset({Kind.EMAIL}))
    joined = "\n".join(sources)

    assert "class MailService" in joined
    assert "class SqliteEngine" not in joined
    assert "class SqliteReader" not in joined


def test_neither_db_kind_excludes_both_reader_utilities() -> None:
    _, sources = assemble_all_utilities(required_kinds=frozenset({Kind.WRITE_FILE}))
    joined = "\n".join(sources)

    assert "class FileSystemOps" in joined
    assert "class SqliteEngine" not in joined
    assert "class SqliteReader" not in joined


def test_required_kinds_none_preserves_include_everything_behavior() -> None:
    filtered_imports, filtered_sources = assemble_all_utilities(
        required_kinds=frozenset({Kind.EMAIL})
    )
    full_imports, full_sources = assemble_all_utilities(required_kinds=None)

    assert len(full_sources) > len(filtered_sources)
    assert "\n".join(full_sources).count("class SqliteEngine") == 1
    assert set(filtered_imports) <= set(full_imports)


def test_extra_root_names_force_includes_a_utility_not_mapped_by_kind() -> None:
    _, sources = assemble_all_utilities(
        required_kinds=frozenset({Kind.WRITE_FILE}),
        extra_root_names=frozenset({"sqlite_reader"}),
    )
    joined = "\n".join(sources)

    assert "class SqliteReader" in joined
