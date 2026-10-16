-- vg2c-output-headers: ["out_ratio", "ww", "link_id", "prodgroup3", "program_name", "test_end_date", "tester_id", "lot", "visual_id", "complevel_1", "complevel_2", "theta", "wcc", "x_offset", "y_offset", "tray_or_carrier_id", "carrier_x", "carrier_y", "subs", "grouping", "headnumber", "entity", "x_25", "x_75", "y_25", "y_75", "wcc_25", "wcc_75", "theta_25", "theta_75", "x_lolimit", "y_lolimit", "wcc_lolimit", "theta_lolimit", "x_hilimit", "y_hilimit", "wcc_hilimit", "theta_hilimit", "x_flagged", "y_flagged", "wcc_flagged", "theta_flagged", "flagged", "sum_flagged", "qty", "outlier_pct"]


DROP TABLE IF EXISTS T_L2_Init;
CREATE TABLE T_L2_Init AS
SELECT /*L2*/ 
          [out_limit] AS [out_limit]
         ,[out_ratio] AS [out_ratio]
         ,[ww] AS [ww]
         ,[link_id] AS [link_id]
         ,[prodgroup3] AS [prodgroup3]
         ,[program_name] AS [program_name]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[complevel_1] AS [complevel_1]
         ,[complevel_2] AS [complevel_2]
         ,[theta] AS [theta]
         ,[wcc] AS [wcc]
         ,[x_offset] AS [x_offset]
         ,[y_offset] AS [y_offset]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[subs] AS [subs]
         ,[grouping] AS [grouping]
         ,[headnumber] AS [headnumber]
         ,[entity] AS [entity]
FROM
(
SELECT /*L1*/ 
          [out_limit] AS [out_limit]
         ,[out_ratio] AS [out_ratio]
         ,[ww] AS [ww]
         ,[link_id] AS [link_id]
         ,[prodgroup3] AS [prodgroup3]
         ,[program_name] AS [program_name]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[complevel_1] AS [complevel_1]
         ,[complevel_2] AS [complevel_2]
         ,[theta] AS [theta]
         ,[wcc] AS [wcc]
         ,[x_offset] AS [x_offset]
         ,[y_offset] AS [y_offset]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[subs] AS [subs]
         ,[grouping] AS [grouping]
         ,[headnumber] AS [headnumber]
         ,[entity] AS [entity]
FROM
(
SELECT /*L0*/  
          3.5 AS [out_limit]
         ,0.25 AS [out_ratio]
         ,a0.[ww] AS [ww]
         ,a0.[link_id] AS [link_id]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,a0.[program_name] AS [program_name]
         ,a0.[test_end_date] AS [test_end_date]
         ,a0.[tester_id] AS [tester_id]
         ,a0.[lot] AS [lot]
         ,a0.[visual_id] AS [visual_id]
         ,a0.[complevel_1] AS [complevel_1]
         ,a0.[complevel_2] AS [complevel_2]
         ,CASE WHEN a0.[theta] = '' THEN NULL ELSE CAST (a0.[theta] AS REAL) END AS [theta]
         ,CASE WHEN a0.[wcc] = '' THEN NULL ELSE CAST (a0.[wcc] AS REAL) END AS [wcc]
         ,CASE WHEN a0.[x offset] = '' THEN NULL ELSE CAST (a0.[x offset] AS REAL) END AS [x_offset]
         ,CASE WHEN a0.[y offset] = '' THEN NULL ELSE CAST (a0.[y offset] AS REAL) END AS [y_offset]
         ,a0.[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,a0.[carrier_x] AS [carrier_x]
         ,a0.[carrier_y] AS [carrier_y]
         ,a0.[subs] AS [subs]
         ,a0.[link_id]||'_'|| a0.[prodgroup3] ||'_'|| a0.[subs] ||'_'|| a0.[entity] ||'_'|| a0.[headnumber] ||'_'|| a0.[complevel_1] AS [grouping]
         ,a0.[headnumber] AS [headnumber]
         ,a0.[entity] AS [entity]
FROM 
[data] a0
WHERE
              a0.[entity] Like 'GSC%' 
) t /*L0*/
) t /*L1*/
;

DROP TABLE IF EXISTS T_L2_1_1;
CREATE TABLE T_L2_1_1 AS
SELECT P25(x_offset) AS AF$S1
,grouping AS AF$PB1
FROM T_L2_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L2_1_1_Idx ON T_L2_1_1 (AF$PB1);
DROP TABLE IF EXISTS T_L2_1_Result;
CREATE TABLE T_L2_1_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [x_25]
FROM T_L2_Init a0 LEFT JOIN T_L2_1_1 a1 ON 
grouping = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L2_1_1;

CREATE INDEX T_L2_1_Result_Idx ON T_L2_1_Result (orig_rowid);
DROP TABLE IF EXISTS T_L2_2_1;
CREATE TABLE T_L2_2_1 AS
SELECT P75(x_offset) AS AF$S1
,grouping AS AF$PB1
FROM T_L2_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L2_2_1_Idx ON T_L2_2_1 (AF$PB1);
DROP TABLE IF EXISTS T_L2_2_Result;
CREATE TABLE T_L2_2_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [x_75]
FROM T_L2_Init a0 LEFT JOIN T_L2_2_1 a1 ON 
grouping = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L2_2_1;

CREATE INDEX T_L2_2_Result_Idx ON T_L2_2_Result (orig_rowid);
DROP TABLE IF EXISTS T_L2_3_1;
CREATE TABLE T_L2_3_1 AS
SELECT P25(y_offset) AS AF$S1
,grouping AS AF$PB1
FROM T_L2_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L2_3_1_Idx ON T_L2_3_1 (AF$PB1);
DROP TABLE IF EXISTS T_L2_3_Result;
CREATE TABLE T_L2_3_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [y_25]
FROM T_L2_Init a0 LEFT JOIN T_L2_3_1 a1 ON 
grouping = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L2_3_1;

CREATE INDEX T_L2_3_Result_Idx ON T_L2_3_Result (orig_rowid);
DROP TABLE IF EXISTS T_L2_4_1;
CREATE TABLE T_L2_4_1 AS
SELECT P75(y_offset) AS AF$S1
,grouping AS AF$PB1
FROM T_L2_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L2_4_1_Idx ON T_L2_4_1 (AF$PB1);
DROP TABLE IF EXISTS T_L2_4_Result;
CREATE TABLE T_L2_4_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [y_75]
FROM T_L2_Init a0 LEFT JOIN T_L2_4_1 a1 ON 
grouping = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L2_4_1;

CREATE INDEX T_L2_4_Result_Idx ON T_L2_4_Result (orig_rowid);
DROP TABLE IF EXISTS T_L2_5_1;
CREATE TABLE T_L2_5_1 AS
SELECT P25(wcc) AS AF$S1
,grouping AS AF$PB1
FROM T_L2_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L2_5_1_Idx ON T_L2_5_1 (AF$PB1);
DROP TABLE IF EXISTS T_L2_5_Result;
CREATE TABLE T_L2_5_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [wcc_25]
FROM T_L2_Init a0 LEFT JOIN T_L2_5_1 a1 ON 
grouping = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L2_5_1;

CREATE INDEX T_L2_5_Result_Idx ON T_L2_5_Result (orig_rowid);
DROP TABLE IF EXISTS T_L2_6_1;
CREATE TABLE T_L2_6_1 AS
SELECT P75(wcc) AS AF$S1
,grouping AS AF$PB1
FROM T_L2_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L2_6_1_Idx ON T_L2_6_1 (AF$PB1);
DROP TABLE IF EXISTS T_L2_6_Result;
CREATE TABLE T_L2_6_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [wcc_75]
FROM T_L2_Init a0 LEFT JOIN T_L2_6_1 a1 ON 
grouping = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L2_6_1;

CREATE INDEX T_L2_6_Result_Idx ON T_L2_6_Result (orig_rowid);
DROP TABLE IF EXISTS T_L2_7_1;
CREATE TABLE T_L2_7_1 AS
SELECT P25(theta) AS AF$S1
,grouping AS AF$PB1
FROM T_L2_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L2_7_1_Idx ON T_L2_7_1 (AF$PB1);
DROP TABLE IF EXISTS T_L2_7_Result;
CREATE TABLE T_L2_7_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [theta_25]
FROM T_L2_Init a0 LEFT JOIN T_L2_7_1 a1 ON 
grouping = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L2_7_1;

CREATE INDEX T_L2_7_Result_Idx ON T_L2_7_Result (orig_rowid);
DROP TABLE IF EXISTS T_L2_8_1;
CREATE TABLE T_L2_8_1 AS
SELECT P75(theta) AS AF$S1
,grouping AS AF$PB1
FROM T_L2_Init GROUP BY 
AF$PB1
;
CREATE INDEX T_L2_8_1_Idx ON T_L2_8_1 (AF$PB1);
DROP TABLE IF EXISTS T_L2_8_Result;
CREATE TABLE T_L2_8_Result AS
SELECT a0.rowid AS orig_rowid, a1.AF$S1 AS [theta_75]
FROM T_L2_Init a0 LEFT JOIN T_L2_8_1 a1 ON 
grouping = a1.AF$PB1
;
DROP TABLE IF EXISTS T_L2_8_1;

CREATE INDEX T_L2_8_Result_Idx ON T_L2_8_Result (orig_rowid);
DROP TABLE IF EXISTS T_L2_Result;
CREATE TABLE T_L2_Result AS
SELECT

[out_limit]
,[out_ratio]
,[ww]
,[link_id]
,[prodgroup3]
,[program_name]
,[test_end_date]
,[tester_id]
,[lot]
,[visual_id]
,[complevel_1]
,[complevel_2]
,[theta]
,[wcc]
,[x_offset]
,[y_offset]
,[tray_or_carrier_id]
,[carrier_x]
,[carrier_y]
,[subs]
,[grouping]
,[headnumber]
,[entity]
,[x_25]
,[x_75]
,[y_25]
,[y_75]
,[wcc_25]
,[wcc_75]
,[theta_25]
,[theta_75]
FROM T_L2_Init a0
LEFT JOIN T_L2_1_Result a1 ON a0.rowid = a1.orig_rowid
LEFT JOIN T_L2_2_Result a2 ON a0.rowid = a2.orig_rowid
LEFT JOIN T_L2_3_Result a3 ON a0.rowid = a3.orig_rowid
LEFT JOIN T_L2_4_Result a4 ON a0.rowid = a4.orig_rowid
LEFT JOIN T_L2_5_Result a5 ON a0.rowid = a5.orig_rowid
LEFT JOIN T_L2_6_Result a6 ON a0.rowid = a6.orig_rowid
LEFT JOIN T_L2_7_Result a7 ON a0.rowid = a7.orig_rowid
LEFT JOIN T_L2_8_Result a8 ON a0.rowid = a8.orig_rowid
;
DROP TABLE IF EXISTS T_L2_1_Result;
DROP TABLE IF EXISTS T_L2_2_Result;
DROP TABLE IF EXISTS T_L2_3_Result;
DROP TABLE IF EXISTS T_L2_4_Result;
DROP TABLE IF EXISTS T_L2_5_Result;
DROP TABLE IF EXISTS T_L2_6_Result;
DROP TABLE IF EXISTS T_L2_7_Result;
DROP TABLE IF EXISTS T_L2_8_Result;
DROP TABLE IF EXISTS T_L2_Init;

SELECT /*L7*/ 
          [out_ratio] AS [out_ratio]
         ,[ww] AS [ww]
         ,[link_id] AS [link_id]
         ,[prodgroup3] AS [prodgroup3]
         ,[program_name] AS [program_name]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[complevel_1] AS [complevel_1]
         ,[complevel_2] AS [complevel_2]
         ,[theta] AS [theta]
         ,[wcc] AS [wcc]
         ,[x_offset] AS [x_offset]
         ,[y_offset] AS [y_offset]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[subs] AS [subs]
         ,[grouping] AS [grouping]
         ,[headnumber] AS [headnumber]
         ,[entity] AS [entity]
         ,[x_25] AS [x_25]
         ,[x_75] AS [x_75]
         ,[y_25] AS [y_25]
         ,[y_75] AS [y_75]
         ,[wcc_25] AS [wcc_25]
         ,[wcc_75] AS [wcc_75]
         ,[theta_25] AS [theta_25]
         ,[theta_75] AS [theta_75]
         ,[x_lolimit] AS [x_lolimit]
         ,[y_lolimit] AS [y_lolimit]
         ,[wcc_lolimit] AS [wcc_lolimit]
         ,[theta_lolimit] AS [theta_lolimit]
         ,[x_hilimit] AS [x_hilimit]
         ,[y_hilimit] AS [y_hilimit]
         ,[wcc_hilimit] AS [wcc_hilimit]
         ,[theta_hilimit] AS [theta_hilimit]
         ,[x_flagged] AS [x_flagged]
         ,[y_flagged] AS [y_flagged]
         ,[wcc_flagged] AS [wcc_flagged]
         ,[theta_flagged] AS [theta_flagged]
         ,[flagged] AS [flagged]
         ,[sum_flagged] AS [sum_flagged]
         ,[qty] AS [qty]
         ,CASE WHEN 3=0 THEN CAST(ROUND(([sum_flagged] /  [qty]) *100,0) AS Integer) ELSE Round(([sum_flagged] /  [qty]) *100,3) END AS [outlier_pct]
FROM
(
SELECT /*L6*/ 
          [out_limit] AS [out_limit]
         ,[out_ratio] AS [out_ratio]
         ,[ww] AS [ww]
         ,[link_id] AS [link_id]
         ,[prodgroup3] AS [prodgroup3]
         ,[program_name] AS [program_name]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[complevel_1] AS [complevel_1]
         ,[complevel_2] AS [complevel_2]
         ,[theta] AS [theta]
         ,[wcc] AS [wcc]
         ,[x_offset] AS [x_offset]
         ,[y_offset] AS [y_offset]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[subs] AS [subs]
         ,[grouping] AS [grouping]
         ,[headnumber] AS [headnumber]
         ,[entity] AS [entity]
         ,[x_25] AS [x_25]
         ,[x_75] AS [x_75]
         ,[y_25] AS [y_25]
         ,[y_75] AS [y_75]
         ,[wcc_25] AS [wcc_25]
         ,[wcc_75] AS [wcc_75]
         ,[theta_25] AS [theta_25]
         ,[theta_75] AS [theta_75]
         ,[x_lolimit] AS [x_lolimit]
         ,[y_lolimit] AS [y_lolimit]
         ,[wcc_lolimit] AS [wcc_lolimit]
         ,[theta_lolimit] AS [theta_lolimit]
         ,[x_hilimit] AS [x_hilimit]
         ,[y_hilimit] AS [y_hilimit]
         ,[wcc_hilimit] AS [wcc_hilimit]
         ,[theta_hilimit] AS [theta_hilimit]
         ,[x_flagged] AS [x_flagged]
         ,[y_flagged] AS [y_flagged]
         ,[wcc_flagged] AS [wcc_flagged]
         ,[theta_flagged] AS [theta_flagged]
         ,[flagged] AS [flagged]
         ,SUM([flagged]) OVER (PARTITION BY [grouping], [lot]) AS [sum_flagged]
         ,(COUNT([visual_id]) OVER (PARTITION BY [grouping], [lot])) * 1.0 AS [qty]
FROM
(
SELECT /*L5*/ 
          [out_limit] AS [out_limit]
         ,[out_ratio] AS [out_ratio]
         ,[ww] AS [ww]
         ,[link_id] AS [link_id]
         ,[prodgroup3] AS [prodgroup3]
         ,[program_name] AS [program_name]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[complevel_1] AS [complevel_1]
         ,[complevel_2] AS [complevel_2]
         ,[theta] AS [theta]
         ,[wcc] AS [wcc]
         ,[x_offset] AS [x_offset]
         ,[y_offset] AS [y_offset]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[subs] AS [subs]
         ,[grouping] AS [grouping]
         ,[headnumber] AS [headnumber]
         ,[entity] AS [entity]
         ,[x_25] AS [x_25]
         ,[x_75] AS [x_75]
         ,[y_25] AS [y_25]
         ,[y_75] AS [y_75]
         ,[wcc_25] AS [wcc_25]
         ,[wcc_75] AS [wcc_75]
         ,[theta_25] AS [theta_25]
         ,[theta_75] AS [theta_75]
         ,[x_lolimit] AS [x_lolimit]
         ,[y_lolimit] AS [y_lolimit]
         ,[wcc_lolimit] AS [wcc_lolimit]
         ,[theta_lolimit] AS [theta_lolimit]
         ,[x_hilimit] AS [x_hilimit]
         ,[y_hilimit] AS [y_hilimit]
         ,[wcc_hilimit] AS [wcc_hilimit]
         ,[theta_hilimit] AS [theta_hilimit]
         ,[x_flagged] AS [x_flagged]
         ,[y_flagged] AS [y_flagged]
         ,[wcc_flagged] AS [wcc_flagged]
         ,[theta_flagged] AS [theta_flagged]
         ,CASE WHEN ( [x_flagged] ==1) |  (  [y_flagged] ==1) | (  [wcc_flagged] ==1) | (  [theta_flagged] ==1) THEN 1 ELSE 0 END AS [flagged]
FROM
(
SELECT /*L4*/ 
          [out_limit] AS [out_limit]
         ,[out_ratio] AS [out_ratio]
         ,[ww] AS [ww]
         ,[link_id] AS [link_id]
         ,[prodgroup3] AS [prodgroup3]
         ,[program_name] AS [program_name]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[complevel_1] AS [complevel_1]
         ,[complevel_2] AS [complevel_2]
         ,[theta] AS [theta]
         ,[wcc] AS [wcc]
         ,[x_offset] AS [x_offset]
         ,[y_offset] AS [y_offset]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[subs] AS [subs]
         ,[grouping] AS [grouping]
         ,[headnumber] AS [headnumber]
         ,[entity] AS [entity]
         ,[x_25] AS [x_25]
         ,[x_75] AS [x_75]
         ,[y_25] AS [y_25]
         ,[y_75] AS [y_75]
         ,[wcc_25] AS [wcc_25]
         ,[wcc_75] AS [wcc_75]
         ,[theta_25] AS [theta_25]
         ,[theta_75] AS [theta_75]
         ,[x_lolimit] AS [x_lolimit]
         ,[y_lolimit] AS [y_lolimit]
         ,[wcc_lolimit] AS [wcc_lolimit]
         ,[theta_lolimit] AS [theta_lolimit]
         ,[x_hilimit] AS [x_hilimit]
         ,[y_hilimit] AS [y_hilimit]
         ,[wcc_hilimit] AS [wcc_hilimit]
         ,[theta_hilimit] AS [theta_hilimit]
         ,CASE WHEN  ((  [x_offset] >   [x_hilimit]) | (  [x_offset] <   [x_lolimit] )) THEN 1 ELSE 0 END AS [x_flagged]
         ,CASE WHEN  (( [y_offset] >  [y_hilimit]) | ( [y_offset] <  [y_lolimit] )) THEN 1 ELSE 0 END AS [y_flagged]
         ,CASE WHEN  ((  [wcc] >   [wcc_hilimit] ) | (  [wcc] <   [wcc_lolimit] )) THEN 1 ELSE 0 END AS [wcc_flagged]
         ,CASE WHEN  ((  [theta] >  [theta_hilimit] ) | (  [theta] <   [theta_lolimit] )) THEN 1 ELSE 0 END AS [theta_flagged]
FROM
(
SELECT /*L3*/ 
          [out_limit] AS [out_limit]
         ,[out_ratio] AS [out_ratio]
         ,[ww] AS [ww]
         ,[link_id] AS [link_id]
         ,[prodgroup3] AS [prodgroup3]
         ,[program_name] AS [program_name]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[lot] AS [lot]
         ,[visual_id] AS [visual_id]
         ,[complevel_1] AS [complevel_1]
         ,[complevel_2] AS [complevel_2]
         ,[theta] AS [theta]
         ,[wcc] AS [wcc]
         ,[x_offset] AS [x_offset]
         ,[y_offset] AS [y_offset]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[subs] AS [subs]
         ,[grouping] AS [grouping]
         ,[headnumber] AS [headnumber]
         ,[entity] AS [entity]
         ,[x_25] AS [x_25]
         ,[x_75] AS [x_75]
         ,[y_25] AS [y_25]
         ,[y_75] AS [y_75]
         ,[wcc_25] AS [wcc_25]
         ,[wcc_75] AS [wcc_75]
         ,[theta_25] AS [theta_25]
         ,[theta_75] AS [theta_75]
         ,[x_25] -( [out_limit] *( [x_75] - [x_25] )) AS [x_lolimit]
         ,[y_25] -( [out_limit] *(  [y_75] -  [y_25] )) AS [y_lolimit]
         ,[wcc_25] -( [out_limit] *(  [wcc_75] -  [wcc_25] )) AS [wcc_lolimit]
         ,[theta_25] -( [out_limit] *(  [theta_75] -  [theta_25] )) AS [theta_lolimit]
         ,[x_75] +( [out_limit] *( [x_75] - [x_25] )) AS [x_hilimit]
         ,[y_75] +( [out_limit] *(  [y_75] -  [y_25] )) AS [y_hilimit]
         ,[wcc_75] +( [out_limit] *(  [wcc_75] -  [wcc_25] )) AS [wcc_hilimit]
         ,[theta_75] +( [out_limit] *(   [theta_75] -   [theta_25] )) AS [theta_hilimit]
FROM
(
T_L2_Result
)
) t /*L3*/
) t /*L4*/
) t /*L5*/
) t /*L6*/
;

