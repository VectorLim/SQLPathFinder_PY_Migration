-- vg2c-output-headers: ["product", "ww", "link_id", "prodgroup3", "lot", "VisualId", "x_offset", "y_offset", "wcc", "theta", "A_status", "B_status", "C_status", "rule", "outlier_pct"]


DROP INDEX IF EXISTS IdxA0;
Create Index IF NOT EXISTS IdxA0 ON [flagged_data] ([prodgroup3]);

SELECT /*L1*/  DISTINCT 
          [product] AS [product]
         ,[ww] AS [ww]
         ,[link_id] AS [link_id]
         ,[prodgroup3] AS [prodgroup3]
         ,[lot] AS [lot]
         ,[VisualId] AS [VisualId]
         ,[x_offset] AS [x_offset]
         ,[y_offset] AS [y_offset]
         ,[wcc] AS [wcc]
         ,[theta] AS [theta]
         ,CASE WHEN (     (ABS([x_offset]) >= 45 AND ABS([wcc]) >= 55) OR     (ABS([y_offset]) >= 45 AND ABS([wcc]) >= 55) OR     (ABS([x_offset]) >= 40 AND ABS([y_offset]) >= 40 AND ABS([wcc]) >= 65) ) THEN 'A'   ELSE 'N' END AS [A_status]
         ,CASE WHEN (     (ABS([x_offset]) >= 40 AND ABS([wcc]) >= 47) OR     (ABS([y_offset]) >= 40 AND ABS([wcc]) >= 47) OR     (ABS([x_offset]) >= 30 AND ABS([y_offset]) >= 30 AND ABS([wcc]) >= 65) OR     (ABS( [theta] )>=0.65)  ) THEN 'B' ELSE 'N' END AS [B_status]
         ,CASE WHEN (     (ABS([x_offset]) >= 40 AND ABS([wcc]) >= 47) OR     (ABS([y_offset]) >= 30 AND ABS([wcc]) >= 37) OR     (ABS([y_offset]) >= 20 AND ABS([y_offset]) >= 20 AND ABS([wcc]) >= 45) OR     (ABS( [theta] )>=0.65) OR     (ABS ([x_offset]) >= 25 AND ABS ([theta]) >= 0.1 AND ABS([wcc]) >= 50) OR     (ABS ([y_offset]) >= 25 AND ABS ([theta]) >= 0.1 AND ABS([wcc]) >= 50)   ) THEN 'C' ELSE 'N' END AS [C_status]
         ,[rule] AS [rule]
         ,[outlier_pct] AS [outlier_pct]
FROM
(
SELECT /*L0*/  
          a1.[product] AS [product]
         ,a0.[ww] AS [ww]
         ,a0.[link_id] AS [link_id]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,a0.[lot] AS [lot]
         ,a0.[visual_id] AS [VisualId]
         ,CASE WHEN a0.[x_offset] = '' THEN NULL ELSE CAST (a0.[x_offset] AS REAL) END AS [x_offset]
         ,CASE WHEN a0.[y_offset] = '' THEN NULL ELSE CAST (a0.[y_offset] AS REAL) END AS [y_offset]
         ,CASE WHEN a0.[wcc] = '' THEN NULL ELSE CAST (a0.[wcc] AS REAL) END AS [wcc]
         ,CASE WHEN a0.[theta] = '' THEN NULL ELSE CAST (a0.[theta] AS REAL) END AS [theta]
         ,a1.[rule] AS [rule]
         ,a0.[outlier_pct] AS [outlier_pct]
FROM 
           [prod_kill] a1
 LEFT OUTER JOIN [flagged_data] a0
  ON a0.[prodgroup3] = a1.[product] 
WHERE
              a0.[outlier_pct] > '0.25' 
) t /*L0*/

