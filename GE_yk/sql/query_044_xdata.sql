

DROP INDEX IF EXISTS IdxA2;
Create Index IF NOT EXISTS IdxA2 ON [yeuchuan_a2_18379] ([pnp_lot],[visual_id_2],[complevel_1_1]);
DROP INDEX IF EXISTS IdxA1;
Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_18379] ([visual_id_1]);

SELECT /*L0*/  DISTINCT 
          a0.[ww] AS [ww]
         ,a0.[link_id] AS [link_id]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,a0.[program_name] AS [program_name]
         ,a0.[test_end_date] AS [test_end_date]
         ,a0.[tester_id] AS [tester_id]
         ,a0.[lot] AS [lot]
         ,a0.[visual_id] AS [visual_id]
         ,a0.[complevel_1] AS [complevel_1]
         ,a0.[complevel_2] AS [complevel_2]
         ,CrossTab->[[a0,18379;:Y]]
         ,a0.[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,a0.[carrier_x] AS [carrier_x]
         ,a0.[carrier_y] AS [carrier_y]
         ,a1.[subs] AS [subs]
         ,CrossTab->[[a2,18379;:Y]]
         ,a2.[entity] AS [entity]
FROM 
           [yeuchuan_a0_18379] a0
 LEFT OUTER JOIN [yeuchuan_a2_18379] a2
  ON a0.[visual_id] = a2.[visual_id_2] 
 AND a0.[complevel_1] = a2.[complevel_1_1] 
 AND a0.[lot] = a2.[pnp_lot]
 LEFT OUTER JOIN [yeuchuan_a1_18379] a1
  ON a0.[visual_id] = a1.[visual_id_1]

