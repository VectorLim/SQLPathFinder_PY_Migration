from __future__ import annotations
from vg2c.dispatch.filter_detector import detect_filters

def test_detect_filters_joins_vs_real():
    sql = """
    SELECT * FROM t1 v1
    LEFT JOIN t2 v2 ON v2.batch_id = v1.batch_id AND v2.facility = v1.facility
    WHERE v4.equipment_sequence = 1
      AND v3.latest_flag = 'Y'
      AND v3.status <> 'I'
      AND v1.transaction_datetime >= SYSDATE - 1
      AND v1.operation = '2511'
    """
    filters = detect_filters(sql, "step_0001_sql_query")
    
    attrs = set()
    for f in filters:
        attrs.update(f.attributes)
        
    assert "v2.batch_id" not in attrs
    assert "v1.batch_id" not in attrs
    assert "v4.equipment_sequence" in attrs
    assert "v3.latest_flag" in attrs
    assert "v3.status" in attrs
    assert "v1.transaction_datetime" in attrs
    assert "v1.operation" in attrs

def test_detect_filters_unqualified():
    sql = "SELECT * FROM t WHERE facility = 'KM' AND val > 100"
    filters = detect_filters(sql, "step_0002_sql_query")
    attrs = {attr for f in filters for attr in f.attributes}
    assert attrs == {"facility", "val"}

def test_detect_filters_functions_and_keywords():
    sql = """
    SELECT * FROM t v1
    WHERE NVL(v1.history_deleted_flag, 'N') = 'N'
      AND v1.out_date >= TRUNC(SYSDATE) - 120
      AND rownum <= 1
    """
    filters = detect_filters(sql, "step_0003_sql_query")
    attrs = {attr for f in filters for attr in f.attributes}
    assert "v1.history_deleted_flag" in attrs
    assert "v1.out_date" in attrs
    assert "rownum" not in attrs
    assert "SYSDATE" not in attrs
