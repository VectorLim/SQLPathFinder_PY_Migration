

DROP INDEX IF EXISTS IdxA0;
Create Index IF NOT EXISTS IdxA0 ON [yeuchuan_a0_22697] ([lot],[operation]);

SELECT /*L0*/  DISTINCT 
          a1.[lot_1] AS [lot_1]
         ,a1.[operation_1] AS [operation_1]
         ,a1.[out_date] AS [out_date]
         ,a1.[oldqty1] AS [oldqty1]
         ,a1.[newqty1] AS [newqty1]
         ,a0.[facility] AS [facility]
         ,a0.[operation] AS [operation]
         ,a0.[module_name] AS [module_name]
         ,a0.[tool_entity] AS [tool_entity]
         ,a0.[primary_entity] AS [primary_entity]
         ,a0.[processing_start_date] AS [processing_start_date]
         ,a0.[processing_end_date] AS [processing_end_date]
         ,a0.[lot] AS [lot]
         ,a0.[product] AS [product]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,Replace(Replace(Replace(Replace(Replace(Replace(a0.[product_desc],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [product_desc]
         ,a0.[owner] AS [owner]
         ,a0.[visual_id] AS [visual_id]
         ,a0.[ws_loss_code] AS [ws_loss_code]
         ,a0.[media_in_x] AS [media_in_x]
         ,a0.[media_in_y] AS [media_in_y]
         ,CrossTab->[[a0,22697;:Y]]
         ,Replace(Replace(Replace(Replace(Replace(Replace(a1.[Interposer_SLI],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [Interposer_SLI]
         ,Replace(Replace(Replace(Replace(Replace(Replace(a1.[Patch_SLI],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [Patch_SLI]
         ,a1.[prodgroup3_1] AS [prodgroup3_1]
         ,a1.[entity] AS [entity]
         ,a1.[transaction] AS [transaction]
FROM 
           [yeuchuan_a1_22697] a1
 LEFT OUTER JOIN [yeuchuan_a0_22697] a0
  ON a1.[lot_1] = a0.[lot] 
 AND a1.[operation_1] = a0.[operation]

