

DROP TABLE IF EXISTS T_L0_Init;
CREATE TABLE T_L0_Init AS
SELECT /*L0*/  
          a0.[lot_1] AS [lot_1]
         ,a0.[newqty1] AS [newqty1]
         ,a0.[facility] AS [facility]
         ,a0.[operation] AS [operation]
         ,a0.[tool_entity] AS [tool_entity]
         ,a0.[primary_entity] AS [primary_entity]
         ,a0.[processing_end_date] AS [processing_end_date]
         ,a0.[lot] AS [lot]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,a0.[product] AS [product]
         ,a0.[visual_id] AS [visual_id]
         ,a0.[ws_loss_code] AS [ws_loss_code]
         ,a0.[media_in_x] AS [media_in_x]
         ,a0.[media_in_y] AS [media_in_y]
         ,a0.[height] AS [height]
         ,a0.[patch_lift_roi1] AS [patch_lift_roi1]
         ,a0.[patch_lift_roi2] AS [patch_lift_roi2]
         ,a0.[patch_lift_roi3] AS [patch_lift_roi3]
         ,a0.[patch_lift_roi4] AS [patch_lift_roi4]
         ,a0.[patch_lift_roi5] AS [patch_lift_roi5]
         ,a0.[patch_lift_roi6] AS [patch_lift_roi6]
         ,a0.[patch_lift_roi7] AS [patch_lift_roi7]
         ,a0.[patch_lift_roi8] AS [patch_lift_roi8]
         ,a0.[patch_lift_roi_max] AS [patch_lift_roi_max]
         ,a0.[patch_sli] AS [patch_sli]
         ,a0.[interposer_sli] AS [interposer_sli]
         ,CASE  WHEN a0.[patch_lift_roi4]  >= 2000 AND  a0.[patch_lift_roi_max]  >= 2030 THEN '1' WHEN a0.[patch_lift_roi8] >= 2000 AND  a0.[patch_lift_roi_max]  >= 2030 THEN '1' ELSE '0' END AS [NCO_Risk]
FROM 
[PARMI_IPM_RAW] a0
;

DROP TABLE IF EXISTS T_L0_1_1;
CREATE TABLE T_L0_1_1 AS
SELECT COUNT(DISTINCT  visual_id) AS AF$S1
,lot_1 AS AF$PB1
FROM T_L0_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L0_1_1_Idx ON T_L0_1_1 (AF$PB1);
DROP TABLE IF EXISTS T_L0_1_Result;
CREATE TABLE T_L0_1_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [VIDCount]
FROM T_L0_Init a0 LEFT JOIN T_L0_1_1 a1 ON 
lot_1 = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L0_1_1;

CREATE INDEX T_L0_1_Result_Idx ON T_L0_1_Result (orig_rowid);
DROP TABLE IF EXISTS T_L0_Result;
CREATE TABLE T_L0_Result AS
SELECT

[lot_1]
,[newqty1]
,[facility]
,[operation]
,[tool_entity]
,[primary_entity]
,[processing_end_date]
,[lot]
,[prodgroup3]
,[product]
,[visual_id]
,[ws_loss_code]
,[media_in_x]
,[media_in_y]
,[height]
,[patch_lift_roi1]
,[patch_lift_roi2]
,[patch_lift_roi3]
,[patch_lift_roi4]
,[patch_lift_roi5]
,[patch_lift_roi6]
,[patch_lift_roi7]
,[patch_lift_roi8]
,[patch_lift_roi_max]
,[patch_sli]
,[interposer_sli]
,[NCO_Risk]
,[VIDCount]
FROM T_L0_Init a0
LEFT JOIN T_L0_1_Result a1 ON a0.rowid = a1.orig_rowid
;
DROP TABLE IF EXISTS T_L0_1_Result;
DROP TABLE IF EXISTS T_L0_Init;

SELECT /*L3*/ 
          [lot_1] AS [lot_1]
         ,[newqty1] AS [newqty1]
         ,[facility] AS [facility]
         ,[operation] AS [operation]
         ,[tool_entity] AS [tool_entity]
         ,[primary_entity] AS [primary_entity]
         ,[processing_end_date] AS [processing_end_date]
         ,[lot] AS [lot]
         ,[prodgroup3] AS [prodgroup3]
         ,[product] AS [product]
         ,[visual_id] AS [visual_id]
         ,[ws_loss_code] AS [ws_loss_code]
         ,[media_in_x] AS [media_in_x]
         ,[media_in_y] AS [media_in_y]
         ,[height] AS [height]
         ,[patch_lift_roi1] AS [patch_lift_roi1]
         ,[patch_lift_roi2] AS [patch_lift_roi2]
         ,[patch_lift_roi3] AS [patch_lift_roi3]
         ,[patch_lift_roi4] AS [patch_lift_roi4]
         ,[patch_lift_roi5] AS [patch_lift_roi5]
         ,[patch_lift_roi6] AS [patch_lift_roi6]
         ,[patch_lift_roi7] AS [patch_lift_roi7]
         ,[patch_lift_roi8] AS [patch_lift_roi8]
         ,[patch_lift_roi_max] AS [patch_lift_roi_max]
         ,[patch_sli] AS [patch_sli]
         ,[interposer_sli] AS [interposer_sli]
         ,[NCO_Risk] AS [NCO_Risk]
         ,[VIDCount] AS [VIDCount]
         ,[FlagLot] AS [FlagLot]
FROM
(
SELECT /*L2*/ 
          [lot_1] AS [lot_1]
         ,[newqty1] AS [newqty1]
         ,[facility] AS [facility]
         ,[operation] AS [operation]
         ,[tool_entity] AS [tool_entity]
         ,[primary_entity] AS [primary_entity]
         ,[processing_end_date] AS [processing_end_date]
         ,[lot] AS [lot]
         ,[prodgroup3] AS [prodgroup3]
         ,[product] AS [product]
         ,[visual_id] AS [visual_id]
         ,[ws_loss_code] AS [ws_loss_code]
         ,[media_in_x] AS [media_in_x]
         ,[media_in_y] AS [media_in_y]
         ,[height] AS [height]
         ,[patch_lift_roi1] AS [patch_lift_roi1]
         ,[patch_lift_roi2] AS [patch_lift_roi2]
         ,[patch_lift_roi3] AS [patch_lift_roi3]
         ,[patch_lift_roi4] AS [patch_lift_roi4]
         ,[patch_lift_roi5] AS [patch_lift_roi5]
         ,[patch_lift_roi6] AS [patch_lift_roi6]
         ,[patch_lift_roi7] AS [patch_lift_roi7]
         ,[patch_lift_roi8] AS [patch_lift_roi8]
         ,[patch_lift_roi_max] AS [patch_lift_roi_max]
         ,[patch_sli] AS [patch_sli]
         ,[interposer_sli] AS [interposer_sli]
         ,[NCO_Risk] AS [NCO_Risk]
         ,[VIDCount] AS [VIDCount]
         ,CASE WHEN  [NCO_Risk]  = '1' OR  [VIDCount] < 2 THEN '1' ELSE '0' END AS [FlagLot]
FROM
(
SELECT /*L1*/ 
          [lot_1] AS [lot_1]
         ,[newqty1] AS [newqty1]
         ,[facility] AS [facility]
         ,[operation] AS [operation]
         ,[tool_entity] AS [tool_entity]
         ,[primary_entity] AS [primary_entity]
         ,[processing_end_date] AS [processing_end_date]
         ,[lot] AS [lot]
         ,[prodgroup3] AS [prodgroup3]
         ,[product] AS [product]
         ,[visual_id] AS [visual_id]
         ,[ws_loss_code] AS [ws_loss_code]
         ,[media_in_x] AS [media_in_x]
         ,[media_in_y] AS [media_in_y]
         ,[height] AS [height]
         ,[patch_lift_roi1] AS [patch_lift_roi1]
         ,[patch_lift_roi2] AS [patch_lift_roi2]
         ,[patch_lift_roi3] AS [patch_lift_roi3]
         ,[patch_lift_roi4] AS [patch_lift_roi4]
         ,[patch_lift_roi5] AS [patch_lift_roi5]
         ,[patch_lift_roi6] AS [patch_lift_roi6]
         ,[patch_lift_roi7] AS [patch_lift_roi7]
         ,[patch_lift_roi8] AS [patch_lift_roi8]
         ,[patch_lift_roi_max] AS [patch_lift_roi_max]
         ,[patch_sli] AS [patch_sli]
         ,[interposer_sli] AS [interposer_sli]
         ,[NCO_Risk] AS [NCO_Risk]
         ,[VIDCount] AS [VIDCount]
FROM
(
T_L0_Result
)
) t /*L1*/
) t /*L2*/
WHERE
              [FlagLot] = '1' 
;

