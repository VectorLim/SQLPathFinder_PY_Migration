from __future__ import annotations

from vg2c import compile_document
from vg2c.workflow import project_document


def _block(options: str, body: str = "") -> str:
    return f"<OPTIONS>\n{options.strip()}\n</OPTIONS>\n{body}\n<---- New Query ---->\n"


def _workflow(tmp_path, *blocks: str):
    source = tmp_path / "script.txt"
    source.write_text("\n".join(blocks), encoding="utf-8")
    return project_document(compile_document(source))


def _paths(effect, phase: str) -> set[str]:
    endpoints = effect.outputs if phase == "output" else effect.inputs
    return {endpoint.path for endpoint in endpoints if endpoint.path}


def test_detects_write_file_and_table_consumer(tmp_path) -> None:
    workflow = _workflow(
        tmp_path,
        _block("/WRITE-FILE=Y\n/CSV=foo.csv", "seed"),
        _block("/ENGINE=SQLite\n/OLEDB=SQLite\n/CSV=result.csv\n/TABLE=foo.csv", "SELECT 1"),
    )
    producer = next(effect for effect in workflow.effects if "foo.csv" in _paths(effect, "output"))
    consumer = next(effect for effect in workflow.effects if "foo.csv" in _paths(effect, "input"))

    assert producer.kind == "write"
    assert producer.id in consumer.dependency_ids


def test_table_binding_links_to_its_csv_producer(tmp_path) -> None:
    workflow = _workflow(
        tmp_path,
        _block("/WRITE-FILE=Y\n/CSV=data.csv", "seed"),
        _block("/ENGINE=SQLite\n/OLEDB=SQLite\n/CSV=result.csv\n/TABLE=data.csv:T0", "SELECT 1"),
    )
    consumer = next(effect for effect in workflow.effects if "data.csv" in _paths(effect, "input"))
    assert consumer.dependency_ids


def test_incremental_csv_list_marker_links_to_unmarked_producer(tmp_path) -> None:
    workflow = _workflow(
        tmp_path,
        _block("/WRITE-FILE=Y\n/CSV=lots.tab", "seed"),
        _block(
            "/ENGINE=SQLite\n/OLEDB=SQLite\n/CSV=result.csv",
            "SELECT 1 WHERE SQL_Get_CSV_List('lots.tab->500', lot, 't.lot In')",
        ),
    )
    consumer = next(
        effect
        for effect in workflow.effects
        if effect.id.endswith("sql-get-csv-list:0")
    )
    assert _paths(consumer, "input") == {"lots.tab"}
    assert consumer.dependency_ids


def test_sqlite_block_can_be_producer_and_consumer(tmp_path) -> None:
    workflow = _workflow(
        tmp_path,
        _block("/WRITE-FILE=Y\n/CSV=a.csv", "seed"),
        _block("/ENGINE=SQLite\n/OLEDB=SQLite\n/CSV=b.csv\n/TABLE=a.csv", "SELECT 1"),
    )
    query = next(effect for effect in workflow.effects if "b.csv" in _paths(effect, "output"))
    assert "a.csv" in _paths(query, "input")
    assert query.dependency_ids


def test_table_comma_split_inputs_are_explicit_effects(tmp_path) -> None:
    workflow = _workflow(
        tmp_path,
        _block("/WRITE-FILE=Y\n/CSV=a.csv", "a"),
        _block("/WRITE-FILE=Y\n/CSV=b.csv", "b"),
        _block(
            "/ENGINE=SQLite\n/OLEDB=SQLite\n/CSV=result.csv\n/TABLE=a.csv,b.csv",
            "SELECT 1",
        ),
    )
    query = next(effect for effect in workflow.effects if "result.csv" in _paths(effect, "output"))
    assert {"a.csv", "b.csv"} <= _paths(query, "input")
    assert len(query.dependency_ids) >= 2
