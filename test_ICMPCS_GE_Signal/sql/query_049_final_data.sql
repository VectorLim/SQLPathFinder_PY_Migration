-- vg2c-output-headers: ["ww", "link_id", "prodgroup3", "grouping", "lot", "test_end_date", "x_offset", "y_offset", "theta", "wcc", "sum_flagged", "qty", "outlier_pct"]


DROP INDEX IF EXISTS IdxA0;
Create Index IF NOT EXISTS IdxA0 ON [flagged_data] ([prodgroup3]);

SELECT /*L1*/  DISTINCT 
          [ww] AS [ww]
         ,[link_id] AS [link_id]
         ,[prodgroup3] AS [prodgroup3]
         ,[grouping] AS [grouping]
         ,[lot] AS [lot]
         ,[test_end_date] AS [test_end_date]
         ,SUM ( [x_flagged] ) OVER (PARTITION BY  [grouping] , [lot] ) AS [x_offset]
         ,SUM (  [y_flagged] ) OVER (PARTITION BY  [grouping] , [lot] ) AS [y_offset]
         ,SUM (  [theta_flagged] ) OVER (PARTITION BY  [grouping] , [lot] ) AS [theta]
         ,SUM (  [wcc_flagged] ) OVER (PARTITION BY  [grouping] , [lot] ) AS [wcc]
         ,[sum_flagged] AS [sum_flagged]
         ,[qty] AS [qty]
         ,[outlier_pct] AS [outlier_pct]
FROM
(
SELECT /*L0*/  
          a0.[ww] AS [ww]
         ,a0.[link_id] AS [link_id]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,a0.[grouping] AS [grouping]
         ,a0.[lot] AS [lot]
         ,a0.[test_end_date] AS [test_end_date]
         ,CASE WHEN a0.[x_flagged] = '' THEN NULL ELSE CAST (a0.[x_flagged] AS REAL) END AS [x_flagged]
         ,CASE WHEN a0.[y_flagged] = '' THEN NULL ELSE CAST (a0.[y_flagged] AS REAL) END AS [y_flagged]
         ,CASE WHEN a0.[wcc_flagged] = '' THEN NULL ELSE CAST (a0.[wcc_flagged] AS REAL) END AS [wcc_flagged]
         ,CASE WHEN a0.[theta_flagged] = '' THEN NULL ELSE CAST (a0.[theta_flagged] AS REAL) END AS [theta_flagged]
         ,CASE WHEN a0.[sum_flagged] = '' THEN NULL ELSE CAST (a0.[sum_flagged] AS REAL) END AS [sum_flagged]
         ,CASE WHEN a0.[qty] = '' THEN NULL ELSE CAST (a0.[qty] AS REAL) END AS [qty]
         ,CASE WHEN a0.[outlier_pct] = '' THEN NULL ELSE CAST (a0.[outlier_pct] AS REAL) END AS [outlier_pct]
         ,a1.[product] AS [product]
FROM 
           [prod_hold] a1
 LEFT OUTER JOIN [flagged_data] a0
  ON a0.[prodgroup3] = a1.[product] 
WHERE
              CASE WHEN a0.[outlier_pct] = '' THEN NULL ELSE CAST (a0.[outlier_pct] AS REAL) END > 0.25 
 AND NOT      (a0.[lot] In 
SQL_Get_CSV_List(".\HIST.csv", "5", "a0.[lot] In") 
) t /*L0*/

