-- vg2c-output-headers: ["ww", "link", "prod", "subs", "lot", "qty", "out_date", "current_time", "LOT_GROUP", "time_gap", "SEQ", "lot_2"]


DROP INDEX IF EXISTS IdxA1;
Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_18379] ([lot_1]);

SELECT /*L4*/  DISTINCT 
          [ww] AS [ww]
         ,[link] AS [link]
         ,[prod] AS [prod]
         ,Replace(Replace(Replace(Replace(Replace(Replace([subs],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [subs]
         ,[lot] AS [lot]
         ,[qty] AS [qty]
         ,[out_date] AS [out_date]
         ,[current_time] AS [current_time]
         ,[LOT_GROUP] AS [LOT_GROUP]
         ,CASE WHEN 2=0 THEN CAST(ROUND([time_gap],0) AS Integer) ELSE Round([time_gap],2) END AS [time_gap]
         ,[SEQ] AS [SEQ]
         ,[lot_2] AS [lot_2]
FROM
(
SELECT /*L3*/ 
          [ww] AS [ww]
         ,[link] AS [link]
         ,[prod] AS [prod]
         ,[subs] AS [subs]
         ,[lot] AS [lot]
         ,[qty] AS [qty]
         ,[out_date] AS [out_date]
         ,[attribute_name] AS [attribute_name]
         ,[owner] AS [owner]
         ,[lot_1] AS [lot_1]
         ,[program_name] AS [program_name]
         ,[current_time] AS [current_time]
         ,[LOT_GROUP] AS [LOT_GROUP]
         ,[time_gap] AS [time_gap]
         ,RANK () OVER (PARTITION BY  [LOT_GROUP]  ORDER BY  [out_date]  DESC) AS [SEQ]
         ,MIN ( [time_gap] ) OVER (PARTITION BY  [LOT_GROUP]  ) AS [MIN_TIME]
         ,[lot_2] AS [lot_2]
FROM
(
SELECT /*L2*/ 
          [ww] AS [ww]
         ,[link] AS [link]
         ,[prod] AS [prod]
         ,[subs] AS [subs]
         ,[lot] AS [lot]
         ,[qty] AS [qty]
         ,[out_date] AS [out_date]
         ,[attribute_name] AS [attribute_name]
         ,[owner] AS [owner]
         ,[lot_1] AS [lot_1]
         ,[program_name] AS [program_name]
         ,[current_time] AS [current_time]
         ,[link] ||'_'|| [prod] ||'_'|| [subs] AS [LOT_GROUP]
         ,[time_gap] AS [time_gap]
         ,[lot_2] AS [lot_2]
FROM
(
SELECT /*L1*/ 
          [ww] AS [ww]
         ,[link] AS [link]
         ,[prod] AS [prod]
         ,[subs] AS [subs]
         ,[lot] AS [lot]
         ,[qty] AS [qty]
         ,[out_date] AS [out_date]
         ,[attribute_name] AS [attribute_name]
         ,[owner] AS [owner]
         ,[lot_1] AS [lot_1]
         ,[program_name] AS [program_name]
         ,[current_time] AS [current_time]
         ,[time_gap] AS [time_gap]
         ,[lot_2] AS [lot_2]
FROM
(
SELECT /*L0*/  
          a0.[ww] AS [ww]
         ,a1.[link] AS [link]
         ,a0.[prod] AS [prod]
         ,a0.[subs] AS [subs]
         ,a0.[lot] AS [lot]
         ,a0.[qty] AS [qty]
         ,a0.[out_date] AS [out_date]
         ,a0.[attribute_name] AS [attribute_name]
         ,a0.[owner] AS [owner]
         ,a1.[lot_1] AS [lot_1]
         ,a1.[program_name] AS [program_name]
         ,a0.[current_time] AS [current_time]
         ,(( strftime('%s',[current_time]) -  strftime('%s',[out_date]) ) / 3600.0 / 24.0)*24 AS [time_gap]
         ,a1.[lot_2] AS [lot_2]
FROM 
           [yeuchuan_a0_18379] a0
 LEFT OUTER JOIN [yeuchuan_a1_18379] a1
  ON a0.[lot] = a1.[lot_1] 
) t /*L0*/
) t /*L1*/
) t /*L2*/
WHERE
              [LOT_GROUP] Is Not Null  
) t /*L3*/
WHERE
              [SEQ] < 30 
ORDER BY
           [LOT_GROUP] Asc

