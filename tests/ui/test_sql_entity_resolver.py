import pytest

from vg2c_ui.services.sql_entity_resolver import SqlEntityResolver
from vg2c_ui.services.sql_model import SqlModelService


def _make_ref(sql, kind, parsed_id):
    model = SqlModelService().parse(sql)
    return SqlEntityResolver().make_ref(
        model,
        document_id="doc",
        step_id="query",
        sql_parameter_id="query-sql",
        entity_kind=kind,
        parsed_id=parsed_id,
        document_revision=4,
        output_hash="output-4",
    )


def _resolve(sql, ref, **overrides):
    values = {
        "document_id": "doc",
        "step_id": "query",
        "sql_parameter_id": "query-sql",
        "document_revision": 4,
        "output_hash": "output-4",
        **overrides,
    }
    return SqlEntityResolver().resolve(SqlModelService().parse(sql), ref, **values)


def test_exact_current_parsed_id_resolves_first():
    sql = "SELECT a, b FROM foo"
    ref = _make_ref(sql, "selection", "selection-1")
    resolution = _resolve(sql, ref)

    assert resolution.status == "resolved"
    assert resolution.ref is not None
    assert resolution.ref.parsed_id == "selection-1"
    assert resolution.ref.ordinal_hint == 1


def test_exact_owner_resolves_identical_parsed_id_and_fingerprint():
    sql = "SELECT customer_id FROM customer"
    ref = _make_ref(sql, "selection", "selection-0")

    resolution = _resolve(sql, ref)

    assert resolution.status == "resolved"
    assert resolution.ref is not None
    assert resolution.ref.step_id == "query"
    assert resolution.ref.sql_parameter_id == "query-sql"


def test_same_document_wrong_step_with_identical_sql_is_stale():
    sql = "SELECT customer_id FROM customer"
    ref = _make_ref(sql, "selection", "selection-0")

    resolution = _resolve(sql, ref, step_id="other-query")

    assert resolution.status == "stale"
    assert resolution.ref is None
    assert "different step" in resolution.reason


def test_same_step_wrong_sql_parameter_with_identical_sql_is_stale():
    sql = "SELECT customer_id FROM customer"
    ref = _make_ref(sql, "selection", "selection-0")

    resolution = _resolve(sql, ref, sql_parameter_id="other-sql")

    assert resolution.status == "stale"
    assert resolution.ref is None
    assert "different SQL parameter" in resolution.reason


def test_reordered_entity_resolves_by_fingerprint_and_refreshes_id():
    ref = _make_ref("SELECT a, b, c FROM foo", "selection", "selection-1")
    resolution = _resolve("SELECT b, a, c FROM foo", ref)

    assert resolution.status == "resolved"
    assert resolution.ref is not None
    assert resolution.ref.parsed_id == "selection-0"
    assert resolution.ref.fingerprint == "b"
    assert resolution.ref.ordinal_hint == 0


def test_duplicate_fingerprint_is_ambiguous_when_exact_id_no_longer_matches():
    ref = _make_ref("SELECT a, b, c FROM foo", "selection", "selection-1")
    resolution = _resolve("SELECT b, a, b FROM foo", ref)

    assert resolution.status == "ambiguous"
    assert resolution.ref is None
    assert "2 current SQL entities" in resolution.reason


def test_removed_entity_returns_not_found():
    ref = _make_ref("SELECT a, b FROM foo", "selection", "selection-1")
    resolution = _resolve("SELECT a FROM foo", ref)

    assert resolution.status == "not_found"
    assert resolution.ref is None


@pytest.mark.parametrize(
    "overrides",
    [
        {"document_id": "other"},
        {"document_revision": 5},
        {"output_hash": "new-output"},
    ],
)
def test_stale_document_identity_revision_or_hash_is_explicit(overrides):
    ref = _make_ref("SELECT a FROM foo", "selection", "selection-0")
    resolution = _resolve("SELECT a FROM foo", ref, **overrides)

    assert resolution.status == "stale"
    assert resolution.ref is None


def test_filter_fingerprint_ignores_position_connector_but_not_predicate_content():
    ref = _make_ref(
        "SELECT a FROM foo WHERE x = 1 AND y = 2",
        "filter",
        "filter-1",
    )
    resolution = _resolve("SELECT a FROM foo WHERE y = 2 AND x = 1", ref)

    assert resolution.status == "resolved"
    assert resolution.ref is not None
    assert resolution.ref.parsed_id == "filter-0"
    assert resolution.ref.fingerprint == "y = 2"


def test_join_predicate_fingerprint_recovers_reordered_predicate():
    original = "SELECT a FROM x LEFT JOIN y ON x.a = y.a AND x.b = y.b"
    reordered = "SELECT a FROM x LEFT JOIN y ON x.b = y.b AND x.a = y.a"
    ref = _make_ref(original, "join_predicate", "join-0-predicate-1")
    resolution = _resolve(reordered, ref)

    assert resolution.status == "resolved"
    assert resolution.ref is not None
    assert resolution.ref.parsed_id == "join-0-predicate-0"


def test_join_predicate_ref_survives_unrelated_sibling_predicate_edit():
    original = "SELECT a FROM x LEFT JOIN y ON x.a = y.a AND x.b = y.b"
    changed = "SELECT a FROM x LEFT JOIN y ON x.a = y.a AND x.c = y.c"
    ref = _make_ref(original, "join_predicate", "join-0-predicate-0")

    resolution = _resolve(changed, ref)

    assert resolution.status == "resolved"
    assert resolution.ref is not None
    assert resolution.ref.parsed_id == "join-0-predicate-0"


def test_join_source_ref_survives_join_predicate_edit():
    original = "SELECT a FROM x LEFT JOIN y ON x.a = y.a AND x.b = y.b"
    changed = "SELECT a FROM x LEFT JOIN y ON x.a = y.a AND x.c = y.c"
    ref = _make_ref(original, "source", "source-join-0")

    resolution = _resolve(changed, ref)

    assert resolution.status == "resolved"
    assert resolution.ref is not None
    assert resolution.ref.parsed_id == "source-join-0"


def test_join_source_fallback_does_not_cross_duplicate_source_in_another_join():
    original = (
        "SELECT r.id FROM root r "
        "LEFT JOIN archive a ON r.id = a.id "
        "LEFT JOIN archive a ON r.alt_id = a.id"
    )
    changed = "SELECT r.id FROM root r LEFT JOIN archive a ON r.id = a.id"
    ref = _make_ref(original, "source", "source-join-1")

    resolution = _resolve(changed, ref)

    assert resolution.status == "not_found"
    assert resolution.ref is None


def test_duplicate_join_source_context_is_ambiguous_instead_of_guessed():
    sql = (
        "SELECT r.id FROM root r "
        "LEFT JOIN archive a ON r.id = a.id "
        "LEFT JOIN archive a ON r.alt_id = a.id"
    )
    ref = _make_ref(sql, "source", "source-join-1")

    resolution = _resolve(sql, ref)

    assert resolution.status == "ambiguous"
    assert resolution.ref is None


def test_join_predicate_fallback_does_not_cross_parent_join():
    original = (
        "SELECT r.id FROM root r "
        "LEFT JOIN archive_old a ON r.id = shared.id "
        "LEFT JOIN archive_new b ON r.id = shared.id"
    )
    changed = "SELECT r.id FROM root r LEFT JOIN archive_old a ON r.id = shared.id"
    ref = _make_ref(original, "join_predicate", "join-1-predicate-0")

    resolution = _resolve(changed, ref)

    assert resolution.status == "not_found"
    assert resolution.ref is None


def test_join_fingerprint_includes_type_source_and_predicates():
    sql = "SELECT a FROM x LEFT JOIN y ON x.a = y.a"
    ref = _make_ref(sql, "join", "join-0")
    changed = _resolve("SELECT a FROM x INNER JOIN y ON x.a = y.a", ref)

    assert changed.status == "not_found"


def test_make_ref_refreshes_identity_after_target_semantics_change():
    original = "SELECT customer_id FROM customer"
    changed = "SELECT customer_number FROM customer"
    old_ref = _make_ref(original, "selection", "selection-0")

    assert _resolve(changed, old_ref).status == "not_found"

    resolver = SqlEntityResolver()
    fresh_ref = resolver.make_ref(
        SqlModelService().parse(changed),
        document_id="doc",
        step_id="query",
        sql_parameter_id="query-sql",
        entity_kind="selection",
        parsed_id="selection-0",
        document_revision=4,
        output_hash="output-4",
    )

    assert fresh_ref.fingerprint == "customer_number"
    assert _resolve(changed, fresh_ref).status == "resolved"
