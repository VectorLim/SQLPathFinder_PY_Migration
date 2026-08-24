import hashlib
import json
from pathlib import Path

FIXTURE = Path(__file__).parents[1] / 'fixtures' / 'sql_semantic_parity.v1.json'


def load_fixture():
    return json.loads(FIXTURE.read_text(encoding='utf-8'))


def test_fixture_hash_is_deterministic():
    fixture = load_fixture()
    expected_hash = fixture.pop('fixture_hash')
    canonical = json.dumps(
        fixture, sort_keys=True, separators=(',', ':'), ensure_ascii=False
    ).encode('utf-8')
    assert expected_hash == f"sha256:{hashlib.sha256(canonical).hexdigest()}"


def test_fixture_has_required_parser_transform_and_dependency_coverage():
    fixture = load_fixture()
    parser_names = {case['name'] for case in fixture['parser_cases']}
    transform_names = {case['name'] for case in fixture['transform_cases']}
    dependency_names = {case['name'] for case in fixture['dependency_cases']}

    assert {
        'selections-and-aliases',
        'filters-and-connectors',
        'joins-and-sources',
        'commented-selection-read-only',
        'cte-read-only',
        'set-operation-read-only',
        'multiple-select-statements-read-only',
    } <= parser_names
    assert {
        'add-selection', 'update-selection', 'remove-selection', 'reorder-selection',
        'add-filter', 'update-filter', 'remove-filter',
        'add-join', 'update-join-type', 'update-join-source', 'update-join-predicate',
        'remove-join', 'update-source',
    } <= transform_names
    assert {
        'pending-output-rename-breaks-consumer',
        'matching-consumer-edit-repairs-dependency',
        'duplicate-outputs-use-artifact-normalization',
        'external-input-without-known-producer',
        'path-normalization-matches-producer',
        'conditional-loop-metadata-preserved',
    } <= dependency_names


def test_fixture_records_current_read_only_and_dependency_behavior():
    fixture = load_fixture()
    parser = {case['name']: case for case in fixture['parser_cases']}
    deps = {case['name']: case for case in fixture['dependency_cases']}

    assert parser['commented-selection-read-only']['expected']['selections'][1]['editable'] is False
    assert parser['cte-read-only']['expected']['readOnlyReason'].startswith('CTEs are preserved')
    assert parser['set-operation-read-only']['expected']['capabilities']['selected'] is False
    diagnostics = deps['pending-output-rename-breaks-consumer']['expected']['diagnostics']
    assert [item['code'] for item in diagnostics] == ['BROKEN_DEPENDENCY']
    assert deps['matching-consumer-edit-repairs-dependency']['expected']['diagnostics'] == []
    assert deps['path-normalization-matches-producer']['normalized_key'] == 'folder/data.csv'
    metadata = deps['conditional-loop-metadata-preserved']['expected']['artifacts'][0]
    assert metadata['conditional'] and metadata['in_loop']
