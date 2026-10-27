-- vg2c-output-headers: ["product", "ww", "link_id", "prodgroup3", "lot", "VisualId", "x_offset", "y_offset", "wcc", "theta", "A_status", "B_status", "C_status", "rule", "kill_flagged", "outlier_pct"]

SELECT /*L2*/ 
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
         ,[A_status] AS [A_status]
         ,[B_status] AS [B_status]
         ,[C_status] AS [C_status]
         ,[rule] AS [rule]
         ,[kill_flagged] AS [kill_flagged]
         ,[outlier_pct] AS [outlier_pct]
FROM
(
SELECT /*L1*/ 
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
         ,[A_status] AS [A_status]
         ,[B_status] AS [B_status]
         ,[C_status] AS [C_status]
         ,[rule] AS [rule]
         ,CASE WHEN  [rule] ==    [A_status] THEN '1' WHEN  [rule] ==     [B_status] THEN '1' WHEN  [rule] ==   [C_status] THEN '1'     ELSE 0 END AS [kill_flagged]
         ,[outlier_pct] AS [outlier_pct]
FROM
(
SELECT /*L0*/  
          sql.[product] AS [product]
         ,sql.[ww] AS [ww]
         ,sql.[link_id] AS [link_id]
         ,sql.[prodgroup3] AS [prodgroup3]
         ,sql.[lot] AS [lot]
         ,sql.[VisualId] AS [VisualId]
         ,sql.[x_offset] AS [x_offset]
         ,sql.[y_offset] AS [y_offset]
         ,sql.[wcc] AS [wcc]
         ,sql.[theta] AS [theta]
         ,sql.[A_status] AS [A_status]
         ,sql.[B_status] AS [B_status]
         ,sql.[C_status] AS [C_status]
         ,sql.[rule] AS [rule]
         ,sql.[outlier_pct] AS [outlier_pct]
FROM 
[yeuchuan_SQL_3182] sql
) t /*L0*/
) t /*L1*/
WHERE
              [kill_flagged] > 0

