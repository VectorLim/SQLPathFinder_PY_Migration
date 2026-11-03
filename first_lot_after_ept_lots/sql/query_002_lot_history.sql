SELECT DISTINCT
       history.facility,
       history.entity AS parent_entity,
       history.lot,
       history.operation,
       TO_CHAR(history.txn_date, 'yyyy-mm-dd hh24:mi:ss') AS first_lot_timestamp,
       TO_CHAR(CAST(history.txn_date AS TIMESTAMP(9)),
               'YYYYMMDDHH24MISSFF9') AS lot_timestamp_key
FROM @[]@.F_ENTITYLOTHIST history
WHERE (history.facility IN
    SQL_Get_CSV_List('ept_interventions.csv', 'facility', 'history.facility IN'))
  AND (history.entity IN
    SQL_Get_CSV_List('ept_interventions.csv', 'parent_entity', 'history.entity IN'))
  AND history.unique_flag = 'Y'
  AND NVL(history.history_deleted_flag, 'N') = 'N'
  AND history.txn_date >= TO_DATE('<<<LOT_HISTORY_START>>>', 'YYYYMMDDHH24MISS')