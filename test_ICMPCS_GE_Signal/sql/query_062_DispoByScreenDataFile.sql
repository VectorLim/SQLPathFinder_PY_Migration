-- vg2c-output-headers: ["visualid", "visual_id", "unit_process_date", "status"]


DROP INDEX IF EXISTS IdxA1;
Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_3182] ([visual_id]);

SELECT /*L0*/ 
          sql.[visualid] AS [visualid]
         ,a1.[visual_id] AS [visual_id]
         ,a1.[unit_process_date] AS [unit_process_date]
         ,a1.[status] AS [status]
FROM 
           [yeuchuan_SQL_3182] sql
 LEFT OUTER JOIN [yeuchuan_a1_3182] a1
  ON sql.[visualid] = a1.[visual_id]

