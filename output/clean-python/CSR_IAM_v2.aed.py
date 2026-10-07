from scripthost_portable.script_api import aed, macros, query, reports, utilities

OPERATION = '2303'
DURATION = 'SYSDATE - 1'


def run():
    reports.run(
        """Type<\\\\>Key<\\\\>COL1<\\\\>COL2<\\\\>COL3<\\\\>COL4<\\\\>COL5<\\\\>COL6<\\\\>COL7<\\\\>COL8
TYPE<\\\\>CSS<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
CSS<\\\\>sqlpathfinder_style_1.css<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
FORMAT<\\\\>Column-Headers<\\\\>background-color:#dbd9c0<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>text-decoration:normal<\\\\>vertical-align:middle
FORMAT<\\\\>Column-Data<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>text-align:left<\\\\>vertical-align:middle<\\\\>
FORMAT<\\\\>Column-Alt-Row<\\\\>background-color:#f7f5dc<\\\\>color:#333<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>text-align:left<\\\\>vertical-align:middle<\\\\>
FORMAT<\\\\>At-Top-of-Report<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:15<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:center<\\\\>vertical-align:middle
FORMAT<\\\\>At-Top-of-Col1<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle
FORMAT<\\\\>At-Top-of-Col2<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle
FORMAT<\\\\>At-Top-of-Col3<\\\\>background-color:white<\\\\>color:#444<\\\\>font-family:Arial<\\\\>font-size:12<\\\\>font-style:normal<\\\\>font-weight:bold<\\\\>text-align:left<\\\\>vertical-align:middle
FORMAT<\\\\>JQX-All-IChart-Text<\\\\>background-color:white<\\\\>color:black<\\\\>font-family:Verdana<\\\\>font-size:11<\\\\>font-style:normal<\\\\>font-weight:normal<\\\\>text-align:left<\\\\>vertical-align:middle
FORMAT<\\\\>COLUMN-BORDER<\\\\>border-color:#cc9<\\\\>border-collapse:collapse<\\\\>border-style:solid<\\\\>border-width:1px<\\\\>border-spacing:4px<\\\\><\\\\><\\\\>
""",
        instance='28702',
        prompt='Step 1-1. create revision footer',
        app_server='atd_atm.hadoop',
    )
    reports.layout(
        """<table class="tblout"><tr class="tblout"><td class="tblout" valign="top">
:FILE:revision.htm
:CSS:sqlpathfinder_style_1.css
:CSSEMBED:Y
:RR:NO
:B:Y
:EM-A:
:EM-S:
:SEC:Y
:TITLE:revision
<table class="tblout">
<tr class="tblout">
<td class="tblout">
<p style="text-align: left" style="background-color: white"><i><font face="arial" size="2.66666666666667" color="silver"><script filename><br>- <changes made></font></i>
</td>
</tr>
</table>
</td><td class="tblout" valign="top">
<table class="tblout">
<tr class="tblout"><td class="tblout"></td></tr>
</table>
</td></tr></table>
""",
        outlook='N',
        instance='28702',
        json_only='N',
        chart_instance='3450',
        app_server='atd_atm.hadoop',
    )
    reports.delete(
        instance='28702',
    )
    if macros.load_csv('configsets.csv'):
        query.run(
            sql=f"""/*BEGIN SQL*/
SELECT 
          lot_1 AS lot_1
         ,operation_1 AS operation_1
         ,To_Char(out_date,'yyyy-mm-dd hh24:mi:ss') AS out_date
         ,oldqty1 AS oldqty1
         ,newqty1 AS newqty1
         ,Replace(Replace(Replace(Replace(Replace(Replace(Interposer_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Interposer_SLI
         ,Replace(Replace(Replace(Replace(Replace(Replace(Patch_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Patch_SLI
         ,prodgroup3_1 AS prodgroup3_1
         ,entity AS entity
         ,transaction AS transaction
FROM
(
SELECT  
          f0.lot AS lot_1
         ,f0.operation AS operation_1
         ,f0.out_date AS out_date
         ,f0.oldqty1 AS oldqty1
         ,f0.newqty1 AS newqty1
         ,(SELECT la.attribute_value FROM @[]@F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 5005 AND la.src_erase_date IS NULL AND rownum <= 1) AS Interposer_SLI
         ,(SELECT la.attribute_value FROM @[]@F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 5001 AND la.src_erase_date IS NULL AND rownum <= 1) AS Patch_SLI
         ,p.prodgroup3 AS prodgroup3_1
         ,f4.entity AS entity
         ,f5.transaction AS transaction
FROM 
@[]@F_LotHist f0
LEFT JOIN @[]@F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
INNER JOIN @[]@F_Lot f9 ON f9.lot = f0.lot
LEFT JOIN @[]@F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = 'Y'
 AND      f4.entity Like 'IAM%' 
LEFT JOIN @[]@F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource
LEFT JOIN @[]@F_LotTxnHist f5 ON f5.lot = f0.lot AND f5.operation = f0.operation AND f5.prevout_date = f0.prevout_date AND NVL(f5.history_deleted_flag,'N') = 'N'
 AND      f5.transaction = 'MVOU' 
WHERE
NVL(f0.history_deleted_flag,'N') = 'N'
AND      f0.owner <> 'EMPTYFOUP'
 AND      f0.operation = '{OPERATION.replace("'", "''")}' 
 AND      f0.out_date >= {DURATION} 
 AND      p.prodgroup3 Like 'CWF%' 
-- Tail A
)
WHERE
              Interposer_SLI Is Not Null  
/*END SQL*/


""",
            engine='VA',
            node=macros['MARS'],
            username='',
            password='',
            oledb='SQLPlus',
            workdir='.\\',
            show_result='',
            timestamp='_20261006134326',
            output='yeuchuan_a1_28702.tab',
            headers='lot_1,operation_1,out_date,oldqty1,newqty1,Interposer_SLI,Patch_SLI,prodgroup3_1,entity,transaction',
            instance='28702',
            prompt='Step 3-1.1-a1. Fetching MARS Data',
            record='WIP_Lot_History_v2@1.0.0.0',
            hadoop_server='ATD_ATM.HADOOP',
        )
        query.run(
            sql=f"""/*BEGIN SQL*/
SELECT 
          facility AS facility
         ,operation AS operation
         ,module_name AS module_name
         ,tool_entity AS tool_entity
         ,primary_entity AS primary_entity
         ,To_Char(processing_start_date,'yyyy-mm-dd hh24:mi:ss') AS processing_start_date
         ,To_Char(processing_end_date,'yyyy-mm-dd hh24:mi:ss') AS processing_end_date
         ,lot AS lot
         ,product AS product
         ,prodgroup3 AS prodgroup3
         ,Replace(Replace(Replace(Replace(Replace(Replace(product_desc,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS product_desc
         ,owner AS owner
         ,visual_id AS visual_id
         ,ws_loss_code AS ws_loss_code
         ,media_in_x AS media_in_x
         ,media_in_y AS media_in_y
         ,parameter AS parameter
         ,Max(numeric_value) AS numeric_value
FROM
(
SELECT  
          bams0.facility AS facility
         ,bams0.operation AS operation
         ,bams0.module_name AS module_name
         ,bams0.tool_entity AS tool_entity
         ,bams0.primary_entity AS primary_entity
         ,bams0.processing_start_time AS processing_start_date
         ,bams0.processing_end_time AS processing_end_date
         ,bams0.lot AS lot
         ,ml.product AS product
         ,mp.prodgroup3 AS prodgroup3
         ,mp.product_description AS product_desc
         ,ml.owner AS owner
         ,bams2.visual_id AS visual_id
         ,bams2.ws_loss_code AS ws_loss_code
         ,bams2.media_in_x AS media_in_x
         ,bams2.media_in_y AS media_in_y
         ,bams3.parameter AS parameter
         ,bams3.numeric_value AS numeric_value
FROM 
ARIES_Views.AV_BAMS_SESSION bams0
LEFT JOIN A_MARS_Lot ml ON bams0.lot=ml.lot
LEFT JOIN A_MARS_Product mp ON ml.product = mp.product AND ml.mars_schema=mp.mars_schema AND mp.facility=bams0.facility
INNER JOIN ARIES_Views.AV_BAMS_MEDIA_TESTING bams1 ON bams1.lao_start_ww = bams0.lao_start_ww AND bams1.obj_s_id = bams0.obj_s_id
INNER JOIN ARIES_Views.AV_BAMS_UNIT_TESTING bams2 ON bams2.lao_start_ww = bams1.lao_start_ww AND bams2.obj_s_id = bams1.obj_s_id AND bams2.obj_mt_id = bams1.obj_mt_id
LEFT JOIN ARIES_Views.AV_BAMS_DEVICE_RESULTS bams3 ON bams3.lao_start_ww = bams2.lao_start_ww AND bams3.obj_s_id = bams2.obj_s_id AND bams3.obj_mt_id = bams2.obj_mt_id AND bams3.obj_ut_id = bams2.obj_ut_id
WHERE
              (bams0.lot In 
SQL_Get_CSV_List(".\\yeuchuan_a1_28702.tab", lot_1, "bams0.lot In") 
 AND      bams0.operation = '{OPERATION.replace("'", "''")}' 
)
GROUP BY 
          facility
         ,operation
         ,module_name
         ,tool_entity
         ,primary_entity
         ,processing_start_date
         ,processing_end_date
         ,lot
         ,product
         ,prodgroup3
         ,product_desc
         ,owner
         ,visual_id
         ,ws_loss_code
         ,media_in_x
         ,media_in_y
         ,parameter
/*END SQL*/


""",
            engine='VA',
            node=macros['ARIES'],
            username='',
            password='',
            oledb='SQLPlus',
            workdir='.\\',
            show_result='',
            timestamp='_20261006134326',
            output='yeuchuan_a0_28702.tab',
            ct_rows='facility,operation,module_name,tool_entity,primary_entity,processing_start_date,processing_end_date,lot,product,prodgroup3,product_desc,owner,visual_id,ws_loss_code,media_in_x,media_in_y',
            ct_value='numeric_value',
            ct_header='parameter',
            ct_array='a0,28702',
            headers='facility,operation,module_name,tool_entity,primary_entity,processing_start_date,processing_end_date,lot,product,prodgroup3,product_desc,owner,visual_id,ws_loss_code,media_in_x,media_in_y,parameter,numeric_value',
            instance='28702',
            prompt='Step 3-1.1-a0. Fetching ARIES Data',
            record='AT_TDX_BAMS@1.0.0.0',
            hadoop_server='ATD_ATM.HADOOP',
        )
        query.run(
            sql="""DROP INDEX IF EXISTS IdxA0;
Create Index IF NOT EXISTS IdxA0 ON [yeuchuan_a0_28702] ([lot],[operation]);

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
         ,CrossTab->[[a0,28702;:Y]]
         ,Replace(Replace(Replace(Replace(Replace(Replace(a1.[Interposer_SLI],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [Interposer_SLI]
         ,Replace(Replace(Replace(Replace(Replace(Replace(a1.[Patch_SLI],',',';'),CAST(X'09' AS TEXT),' '),CAST(X'0A' AS TEXT),' '),CAST(X'0D' AS TEXT),' '),CAST(X'22' AS TEXT),''''),CAST(X'07' AS TEXT),' ') AS [Patch_SLI]
         ,a1.[prodgroup3_1] AS [prodgroup3_1]
         ,a1.[entity] AS [entity]
         ,a1.[transaction] AS [transaction]
FROM 
           [yeuchuan_a1_28702] a1
 LEFT OUTER JOIN [yeuchuan_a0_28702] a0
  ON a1.[lot_1] = a0.[lot] 
 AND a1.[operation_1] = a0.[operation]

""",
            engine='SQLite',
            reset='Y',
            node='.\\',
            oledb='SQLite',
            username='',
            password='',
            workdir='.\\',
            show_result='No',
            timestamp='_20261006134326',
            output='PARMI_IPM_RAW.csv',
            tables='yeuchuan_a1_28702.tab,yeuchuan_a0_28702.tab',
            headers='lot_1,operation_1,out_date,oldqty1,newqty1,facility,operation,module_name,tool_entity,primary_entity,processing_start_date,processing_end_date,lot,product,prodgroup3,product_desc,owner,visual_id,ws_loss_code,media_in_x,media_in_y,CrossTab->[[a0,28702;:N]],Interposer_SLI,Patch_SLI,prodgroup3_1,entity,transaction',
            delete='*Instance*',
            instance='28702',
            sqlite_types=',lot_1(c),operation_1(c),out_date(d),oldqty1(n),newqty1(n),facility(c),operation(c),module_name(c),tool_entity(c),primary_entity(c),processing_start_date(d),processing_end_date(d),lot(c),product(c),prodgroup3(c),product_desc(x),owner(c),visual_id(c),ws_loss_code(c),media_in_x(n),media_in_y(n),Interposer_SLI(x),Patch_SLI(x),prodgroup3_1(c),entity(c),transaction(c)',
            quote_csv='Y',
            hadoop_server='ATD_ATM.HADOOP',
        )
        utilities.rows_in_file('PARMI_IPM_RAW.csv', 'RowsInFile')
        if macros.compare('RowsInFile', 'GT', '0'):
            query.run(
                sql="""DROP TABLE IF EXISTS T_L0_Init;
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
WHERE
              [NCO_Risk] = '1' 
) t /*L1*/
) t /*L2*/
WHERE
              [FlagLot] = '1' 
;

""",
                engine='SQLite',
                node='.\\',
                oledb='SQLite',
                username='',
                password='',
                workdir='.\\',
                show_result='Yes',
                timestamp='_20261006134326',
                output='IPM_Data.csv',
                tables='PARMI_IPM_RAW.csv',
                headers='lot_1,newqty1,facility,operation,tool_entity,primary_entity,processing_end_date,lot,prodgroup3,product,visual_id,ws_loss_code,media_in_x,media_in_y,height,patch_lift_roi1,patch_lift_roi2,patch_lift_roi3,patch_lift_roi4,patch_lift_roi5,patch_lift_roi6,patch_lift_roi7,patch_lift_roi8,patch_lift_roi_max,patch_sli,interposer_sli,NCO_Risk,VIDCount,FlagLot',
                instance='28702',
                prompt='Step 6-1.1. Fetching Text (SQLite) Data',
                unique_headers='Y',
                quote_csv='Y',
                hadoop_server='ATD_ATM.HADOOP',
            )
            query.run(
                sql="""SELECT DISTINCT '<<<AED_FACILITY>>>' AS [FACILITY], a0.[lot] AS [LOT]
FROM [IPM_Data] a0;
""",
                engine='SQLite',
                node='.\\',
                oledb='SQLite',
                username='',
                password='',
                workdir='.\\',
                output='AED_CANDIDATES.csv',
                tables='IPM_Data.csv',
                headers='FACILITY,LOT',
                quote_csv='Y',
                instance='28702',
                prompt='Select distinct flagged lots for AED',
            )
            aed.process('AED_CANDIDATES.csv')
        utilities.rows_in_file('AED_CANDIDATES.csv', 'SIGNAL')
        if macros.compare('SIGNAL', 'GT', '0'):
            reports.defer(
                """Type<\\\\>Key<\\\\>COL1<\\\\>COL2<\\\\>COL3<\\\\>COL4<\\\\>COL5<\\\\>COL6<\\\\>COL7<\\\\>COL8<\\\\>COL9<\\\\>COL10<\\\\>COL11<\\\\>COL12<\\\\>COL13<\\\\>COL14<\\\\>COL15<\\\\>COL16<\\\\>COL17<\\\\>COL18<\\\\>COL19<\\\\>COL20<\\\\>COL21<\\\\>COL22<\\\\>COL23<\\\\>COL24<\\\\>COL25<\\\\>COL26
TYPE<\\\\>HTML<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
INPUT-FILE<\\\\>IPM_Data.csv<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
OUTPUT-FILE<\\\\>SQLPathFinder.htm<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
CSS<\\\\>sqlpathfinder_style_1.css<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
COLSPAN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
DRILLDOWN<\\\\>N<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
DYNAMICSORT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
DYNAMICFILTER<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
ATTOPDRILLDOWN<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
NOPREPROCESS<\\\\>Y<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
AT-TOP-OF-REPORT<\\\\><\\\\>CWF_MLINCO_LOTHOLD<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
COLUMN-DATA<\\\\><\\\\>facility<\\\\>operation<\\\\>tool_entity<\\\\>primary_entity<\\\\>processing_end_date<\\\\>lot<\\\\>prodgroup3<\\\\>product<\\\\>visual_id<\\\\>ws_loss_code<\\\\>media_in_x<\\\\>media_in_y<\\\\>height<\\\\>patch_lift_roi1<\\\\>patch_lift_roi2<\\\\>patch_lift_roi3<\\\\>patch_lift_roi4<\\\\>patch_lift_roi5<\\\\>patch_lift_roi6<\\\\>patch_lift_roi7<\\\\>patch_lift_roi8<\\\\>patch_lift_roi_max<\\\\>lot_1<\\\\>patch_sli<\\\\>interposer_sli<\\\\>nco_risk
COLUMN-HEADERS<\\\\><\\\\>Facility<\\\\>Operation<\\\\>Tool Entity<\\\\>Primary Entity<\\\\>Processing End Date<\\\\>Lot<\\\\>Prodgroup3<\\\\>Product<\\\\>Visual Id<\\\\>Ws Loss Code<\\\\>Media In X<\\\\>Media In Y<\\\\>Height<\\\\>Patch Lift Roi1<\\\\>Patch Lift Roi2<\\\\>Patch Lift Roi3<\\\\>Patch Lift Roi4<\\\\>Patch Lift Roi5<\\\\>Patch Lift Roi6<\\\\>Patch Lift Roi7<\\\\>Patch Lift Roi8<\\\\>Patch Lift Roi Max<\\\\>Lot 1<\\\\>Patch Sli<\\\\>Interposer Sli<\\\\>Nco Risk
COLUMN-ALIGNMENT<\\\\><\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left<\\\\>middle-left
COLUMN-FORMAT<\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\><\\\\>
""",
                report_id='MYREPORT3',
                instance='28702',
                prompt='Step 8-6. sending notification',
                app_server='atd_atm.hadoop',
            )
            reports.layout(
                """<table class="tblout"><tr class="tblout"><td class="tblout" valign="top">
:FILE:EMAIL:<<<dEmail>>>
:CSS:sqlpathfinder_style_1.css
:CSSEMBED:Y
:RR:NO
:B:Y
:EM-A:
:EM-S:
:SEC:Y
:TITLE:CWF_MLINCO_LOTHOLD
<table class="tblout">
<tr class="tblout">
<td class="tblout">
<p style="text-align: left" style="background-color: white"><font face="intel clear" size="3.66666666666667" color="black">Lot on hold due to suspected MLI NCO. Lot will need to go through 100% XRAY (2846)</font>
</td>
</tr>
<tr class="tblout">
<td class="tblout">
HTM:MYREPORT3
</td>
</tr>
<tr class="tblout">
<td class="tblout">
<p style="text-align: left" style="background-color: white"><font face="intel clear" size="3.66666666666667" color="black"><strong>Configurations applied</strong><br>AED enabled: <<<AED_ENABLED>>><br>Target attribute(s): <<<ATTR_LIST>>><br>Target value: <<<SKIP_OPERATION>>></font>
</td>
</tr>
<tr class="tblout">
<td class="tblout">
IHJ:revision.htm
</td>
</tr>
</table>
</td><td class="tblout" valign="top">
<table class="tblout">
<tr class="tblout"><td class="tblout"></td></tr>
</table>
</td></tr></table>
""",
                outlook='N',
                instance='28702',
                json_only='N',
                chart_instance='30819',
                app_server='atd_atm.hadoop',
            )
            reports.delete(
                instance='28702',
            )


if __name__ == "__main__":
    raise SystemExit("Use python -m scripthost_portable.launcher <job.py> --workdir <directory>.")
