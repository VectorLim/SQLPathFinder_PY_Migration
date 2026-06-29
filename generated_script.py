# Auto-generated Python script from VG2
"""Pipeline implementation."""

from vg2c.emitter.macro import apply_crosstab
from vg2c_runtime import ctx as pipeline_ctx

# --- Embedded reader runtime ------------------------------------------------
from datasyncx.readers import AriesReader, MarsReader, OracleReader

# DATABASE_TYPE_MAP is the single extension point for adding a new database
# type: map the /ENGINE= identifier used in the VG2 source to a datasyncx
# Reader subclass. ``read`` below dispatches to it.
DATABASE_TYPE_MAP = {
    "MARS": MarsReader,
    "OASYS": OracleReader,
    "ARIES": AriesReader,
}


def read(sql, db_type, macro_state=None):
    """Run *sql* against the Reader registered for *db_type*.

    ``macro_state`` (when given) substitutes ``<<<NAME>>>`` macro
    placeholders that survive into the SQL body via its own
    ``substitute_sql`` helper.
    """
    if macro_state is not None:
        sql = macro_state.substitute_sql(sql)
    if db_type not in DATABASE_TYPE_MAP:
        raise ValueError(f"Unsupported database type: {db_type!r}")
    return DATABASE_TYPE_MAP[db_type]().read(site="KM", query=sql)


# --- end embedded reader runtime --------------------------------------------


def step_0000_step_1_1_create_an_html_report(ctx):
    pass  # HTML report not translated


def step_0001_html_report(ctx):
    pass  # HTML report not translated


def step_0002_html_report(ctx):
    pass  # HTML report not translated


def step_0003_step_1_2_create_macro_tmp_update_script_name_here(ctx):
    ctx.write_file(path='macrotmp.csv', template='\nSfolder,underDEV,useCSR,useMMS\nICMPCS_SUBPLANE_CSR_DLA,Y,Y,Y')


def step_0004_step_1_4_create_getcsrsu_bat(ctx):
    ctx.write_file(path='getcsrsu.bat', template='\n@echo off\nset PriCSR="\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset SecCSR="\\\\KMATSHFS.intel.com\\KMATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\Patrol\\*.___"\nset BakCSR="\\\\SHUser-ProdAT.intel.com\\SHProdATUser$\\%username%\\Patrol\\*.___"\ncopy %PriCSR% . || copy %SecCSR% . || copy %BAKCSR% .\nren setsiteparam.___ setsiteparam.exe')


def step_0005_step_1_5_run_getcsrsu_bat(ctx):
    ctx.external.run(['getcsrsu.bat'])


def step_0007_step_1_7_run_setsiteparam_exe(ctx):
    ctx.external.run(['setsiteparam.exe', 'KM', ctx.macro.named("SFOLDER"), ctx.macro.named("UNDERDEV"), ctx.macro.named("USECSR"), ctx.macro.named("USEMMS")])


def step_0009_step_1_8_delete_temporary_files(ctx):
    ctx.fs_ops.delete(paths=['"macrotmp.csv', 'getcsrsu.bat', 'setsiteparam.exe', 'csrsu.txt"'])


def step_0011_rows_in_file(ctx):
    ctx.macro.set_named('CONFIG', str(ctx.csv_io.row_count('ICMPCS_config.csv')))


def step_0013_step_1_12_trigger_if_config_file_not_found(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0015_step_1_13_1_transpose_config_file_to_macro_friendly_format(ctx):
    ctx.sqlite_engine.run_join(
        sql="""
SELECT /*L10*/  DISTINCT 
          [icmpcs] AS [icmpcs]
         ,[parameter] AS [parameter]
         ,Max([value]) AS [value]
         ,[STARTTS] AS [STARTTS]
         ,[UTC] AS [UTC]
         ,[SFOLDER] AS [SFOLDER]
         ,[FAC] AS [FAC]
         ,[MARS] AS [MARS]
         ,[RIMS] AS [RIMS]
         ,[EIMS] AS [EIMS]
         ,[ARIES] AS [ARIES]
         ,[OASYS] AS [OASYS]
         ,[MMS] AS [MMS]
         ,[MMSI] AS [MMSI]
         ,[TOOLLOG] AS [TOOLLOG]
         ,[VFMARS] AS [VFMARS]
         ,[VFARIES] AS [VFARIES]
         ,[CSRPATH] AS [CSRPATH]
         ,[MMSPATH] AS [MMSPATH]
         ,[UNDERDEV] AS [UNDERDEV]
         ,[CSRV] AS [CSRV]
         ,[MMSV] AS [MMSV]
FROM
(
SELECT /*L0*/  
          a0.[icmpcs] AS [icmpcs]
         ,a0.[parameter] AS [parameter]
         ,a0.[value] AS [value]
         ,'<<<STARTTS>>>' AS [STARTTS]
         ,'<<<UTC>>>' AS [UTC]
         ,'<<<SFOLDER>>>' AS [SFOLDER]
         ,'<<<FAC>>>' AS [FAC]
         ,'<<<MARS>>>' AS [MARS]
         ,'<<<RIMS>>>' AS [RIMS]
         ,'<<<EIMS>>>' AS [EIMS]
         ,'<<<ARIES>>>' AS [ARIES]
         ,'<<<OASYS>>>' AS [OASYS]
         ,'<<<MMS>>>' AS [MMS]
         ,'<<<MMSI>>>' AS [MMSI]
         ,'<<<TOOLLOG>>>' AS [TOOLLOG]
         ,'<<<VFMARS>>>' AS [VFMARS]
         ,'<<<VFARIES>>>' AS [VFARIES]
         ,'<<<CSRPATH>>>' AS [CSRPATH]
         ,'<<<MMSPATH>>>' AS [MMSPATH]
         ,'<<<UNDERDEV>>>' AS [UNDERDEV]
         ,'<<<CSRV>>>' AS [CSRV]
         ,'<<<MMSV>>>' AS [MMSV]
FROM 
[ICMPCS_config] a0
WHERE
              a0.[icmpcs] = 'ICMPCS' 
) t /*L0*/
GROUP BY 
          [icmpcs]
         ,[parameter]
         ,[STARTTS]
         ,[UTC]
         ,[SFOLDER]
         ,[FAC]
         ,[MARS]
         ,[RIMS]
         ,[EIMS]
         ,[ARIES]
         ,[OASYS]
         ,[MMS]
         ,[MMSI]
         ,[TOOLLOG]
         ,[VFMARS]
         ,[VFARIES]
         ,[CSRPATH]
         ,[MMSPATH]
         ,[UNDERDEV]
         ,[CSRV]
         ,[MMSV]
""",
        inputs=['ICMPCS_config.csv'],
        output='configsets.csv',
    )


def step_0016_rows_in_file(ctx):
    ctx.macro.set_named('CONFIGSETS', str(ctx.csv_io.row_count('configsets.csv')))


def step_0018_step_1_16_trigger_if_converted_config_file_contains_not_equal_to_1_row(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0022_step_1_19_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx):
    ctx.write_file(path='CSRVerror.htm', template='\n<!DOCTYPE html>\n<html>\n<body>\n<p>It is detected that you cannot access to CSR depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>CSR Superuser</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong>\nPath: <<<CSRPATH>>></p>\n</body>\n</html>')


def step_0023_step_1_20_email_when_user_have_no_access_to_csr(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0026_step_1_22_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx):
    ctx.write_file(path='MMSVerror.htm', template='\n<!DOCTYPE html>\n<html\n<body>\n<p>It is detected that you cannot access to MMS Signal Tracer depository path for <strong>KM</strong> site.</p>\n\n<p>This could be due to you do NOT have the <strong>MMS Signal Tracer Admin</strong> access.</p>\n\n<p>Script Name: <strong><<<SFOLDER>>></strong><br/>\nPath: <<<MMSPATH>>></p>\n</body>\n</html>')


def step_0027_step_1_23_email_when_user_have_no_access_to_mms_signal_tracer(ctx):
    pass  # TODO: email utility — argv positions unresolved


def step_0029_step_1_24_robocopy_hist_txt(ctx):
    ctx.fs_ops.copy(src='HIST.txt', dst='\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\' + ctx.macro.named("SFOLDER") + '\\KM\\HIST')


def step_0030_rows_in_file(ctx):
    ctx.macro.set_named('HIST', str(ctx.csv_io.row_count('HIST.txt')))


def step_0032_step_1_27_create_dummy_hist_csv(ctx):
    ctx.write_file(path='HIST.csv', template='\nLOT,OUT_DATE\nDUMMY,2000-01-01 00:00:00')


def step_0033_step_1_28_create_histerror_txt(ctx):
    ctx.write_file(path='HISTERROR.txt', template='\nERROR\nERROR\nERROR')


def step_0035_step_1_29_convert_hist_txt_to_hist_csv(ctx):
    pass  # TODO: unhandled utility shape=unknown


def step_0042_step_4_1_copy_files_folders(ctx):
    ctx.fs_ops.copy(src='\\\\AZATSHFS.intel.com\\AZATAnalysis$\\MAOATM\\Config\\VF_POR_Cfg\\ICM_PCS\\ICMPCS_SUBPLANE_CSR_DLA\\Product_Lookup.csv', dst='.\\')


def step_0043_step_4_2_1_fetching_text_sqlite_data(ctx):
    ctx.sqlite_engine.run_join(
        sql="""
SELECT /*L0*/ 
          a0.[site] AS [site]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,a0.[upper_y_limit] AS [upper_y_limit]
         ,a0.[lower_y_limit] AS [lower_y_limit]
         ,a0.[upper_x_limit] AS [upper_x_limit]
         ,a0.[lower_x_limit] AS [lower_x_limit]
FROM 
[Product_Lookup] a0
""",
        inputs=['Product_Lookup.csv'],
        output='CSR_Server_OIS_Product_List.csv',
    )


def step_0044_step_5_1_1_a0_fetching_mars_data(ctx):
    result = read(sql="""
/*BEGIN SQL*/
SELECT  DISTINCT 
          c0.ww AS site_work_week
         ,f0.lot AS lot
         ,f0.operation AS operation
         ,To_Char(f0.load_date,'yyyy-mm-dd hh24:mi:ss') AS out_date
         ,f0.route AS route
         ,f0.owner AS owner
         ,f0.oldqty1 AS oldqty1
         ,f0.newqty1 AS newqty1
         ,f4.entity AS entity
         ,p.prodgroup3 AS prodgroup3
         ,f0.facility AS facility
FROM 
@[]@.F_LotHist f0
INNER JOIN @[]@.F_Calendar c0 ON f0.last_action_date BETWEEN c0.start_date AND c0.end_date AND c0.event_code = 'S' AND decode(f0.facility,'RA3','AAL',f0.facility)= c0.facility
LEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
INNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot
LEFT JOIN @[]@.F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = 'Y'
 AND      f4.entity Like 'DIA%' 
LEFT JOIN @[]@.F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource
LEFT JOIN @[]@.F_Entity en ON f4.entity = en.entity AND f4.facility = en.facility
WHERE
NVL(f0.history_deleted_flag,'N') = 'N'
AND      f0.owner <> 'EMPTYFOUP'
 AND      p.prodgroup3 In 
""" + ctx.sql_macros.sql_get_csv_list('.\\CSR_Server_OIS_Product_List.csv', 2, 'p.prodgroup3 In') + """ 
 AND      f0.operation In ('2090'
,'1960') 
 AND      f0.load_date >= (SYSDATE - 8/24) 
 AND      f0.movedout_txn In ('MVOU') 
-- Tail A
/*END SQL*/

""", db_type='MARS', macro_state=ctx.macro)

    ctx.csv_io.write('CSR_Server_OIS_subplane_lotlist.csv', result)


def step_0045_rows_in_file(ctx):
    ctx.macro.set_named('LOTS', str(ctx.csv_io.row_count('CSR_Server_OIS_subplane_lotlist.csv')))


def step_0047_step_8_1_1_a0_fetching_aries_data(ctx):
    result = read(sql="""
/*BEGIN SQL*/
SELECT 
          facility AS facility
         ,lot AS lot
         ,operation AS operation
         ,To_Char(Max(test_end_date),'yyyy-mm-dd hh24:mi:ss') AS test_end_date
         ,tester_id AS tester_id
         ,program_name AS program_name
         ,prodgroup3 AS prodgroup3
         ,visual_id AS visual_id
         ,tray_or_carrier_id AS tray_or_carrier_id
         ,test_name AS test_name
         ,ws_loss_code AS ws_loss_code
         ,carrier_x AS carrier_x
         ,carrier_y AS carrier_y
         ,lane_number AS lane_number
         ,Max(Sub_plane) AS Sub_plane
FROM
(
SELECT 
          facility AS facility
         ,lot AS lot
         ,operation AS operation
         ,test_end_date AS test_end_date
         ,tester_id AS tester_id
         ,program_name AS program_name
         ,prodgroup3 AS prodgroup3
         ,visual_id AS visual_id
         ,tray_or_carrier_id AS tray_or_carrier_id
         ,test_name AS test_name
         ,ws_loss_code AS ws_loss_code
         ,carrier_x AS carrier_x
         ,carrier_y AS carrier_y
         ,lane_number AS lane_number
         ,TO_CHAR(  carrier_y   ||   carrier_x   ) AS Socket
         ,Sub_plane AS Sub_plane
FROM
(
SELECT 
          facility AS facility
         ,lot AS lot
         ,operation AS operation
         ,test_end_date AS test_end_date
         ,tester_id AS tester_id
         ,program_name AS program_name
         ,prodgroup3 AS prodgroup3
         ,visual_id AS visual_id
         ,tray_or_carrier_id AS tray_or_carrier_id
         ,test_name AS test_name
         ,ws_loss_code AS ws_loss_code
         ,carrier_x AS carrier_x
         ,carrier_y AS carrier_y
         ,lane_number AS lane_number
         ,Sub_plane AS Sub_plane
FROM
(
SELECT  
          ats.facility AS facility
         ,ats.lot AS lot
         ,ats.operation AS operation
         ,ats.test_end_date_time AS test_end_date
         ,ats.tester_id AS tester_id
         ,ats.program_name AS program_name
         ,mp.prodgroup3 AS prodgroup3
         ,di.visual_id AS visual_id
         ,dt.testing_session_tray_id AS tray_or_carrier_id
         ,t.test_name AS test_name
         ,dt.ws_loss_code AS ws_loss_code
         ,dt.carrier_x AS carrier_x
         ,dt.carrier_y AS carrier_y
         ,dt.lane_number AS lane_number
         ,CASE WHEN ctr.string_value IS NULL THEN to_char(ctr.numeric_result) ELSE ctr.string_value END AS Sub_plane
FROM 
A_Testing_Session ats
LEFT JOIN A_MARS_Lot ml ON ats.lot=ml.lot
LEFT JOIN A_MARS_Product mp ON ml.product = mp.product AND ml.mars_schema=mp.mars_schema AND ats.facility = mp.facility
INNER JOIN A_All_Component_Testing_Result ctr ON ctr.lao_start_ww = ats.lao_start_ww AND ctr.ts_id = ats.ts_id AND (ctr.numeric_result IS NOT NULL or ctr.string_value is NOT NULL)
INNER JOIN A_Test t ON t.t_id = ctr.t_id
INNER JOIN A_Device_Testing dt ON dt.lao_start_ww = ats.lao_start_ww AND dt.ts_id = ats.ts_id
AND dt.lao_start_ww = ctr.lao_start_ww AND dt.ts_id = ctr.ts_id AND dt.dt_id = ctr.dt_id
LEFT JOIN A_Device_Item di ON di.di_id = dt.di_id
WHERE ats.data_domain='METROLOGY'
 AND      (ats.lot In 
""" + ctx.sql_macros.sql_get_csv_list('.\\CSR_Server_OIS_subplane_lotlist.csv', 2, 'ats.lot In') + """) 
 AND      (ats.operation In 
""" + ctx.sql_macros.sql_get_csv_list('.\\CSR_Server_OIS_subplane_lotlist.csv', 3, 'ats.operation In') + """) 
 AND      (ats.tester_id LIKE  'OIS%'
) 
 AND      t.test_name In ('SUBPLANEANGLEX'
,'SUBPLANEANGLEY') 
 AND      dt.ws_loss_code Is Null  
)
)
)
GROUP BY 
          facility
         ,lot
         ,operation
         ,tester_id
         ,program_name
         ,prodgroup3
         ,visual_id
         ,tray_or_carrier_id
         ,test_name
         ,ws_loss_code
         ,carrier_x
         ,carrier_y
         ,lane_number
/*END SQL*/

""", db_type='ARIES', macro_state=ctx.macro)
    result = apply_crosstab(result, row_keys=['facility', 'lot', 'operation', 'test_end_date', 'tester_id', 'program_name', 'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code', 'carrier_x', 'carrier_y', 'lane_number'], header_key='test_name', value_key='Sub_plane')

    ctx.csv_io.write('yeuchuan_a0_15507.tab', result)


def step_0048_step_8_1_1_a2_fetching_aries_data(ctx):
    result = read(sql="""
/*BEGIN SQL*/
SELECT  DISTINCT 
          z0.primary_entity AS entity
         ,z2.bonding_station AS bond_station
         ,z0.lot AS lot_2
         ,z8.visual_id AS visual_id_1
FROM 
ARIES_Views.AV_dia_session z0
LEFT JOIN ARIES_Views.AV_dia_media_testing z2 ON z2.lao_start_ww = z0.lao_start_ww AND z2.obj_s_id = z0.obj_s_id
INNER JOIN ARIES_Views.AV_dia_Unit_Testing z8 ON z8.lao_start_ww = z2.lao_start_ww AND z8.obj_s_id = z2.obj_s_id AND z8.obj_mt_id = z2.obj_mt_id
WHERE
              (z0.lot In 
""" + ctx.sql_macros.sql_get_csv_list('.\\yeuchuan_a0_15507.tab', 'lot', 'z0.lot In') + """) 
 AND      z0.tool_entity Like 'TGB%' 
 AND      (z0.operation In 
""" + ctx.sql_macros.sql_get_csv_list('.\\yeuchuan_a0_15507.tab', 'operation', 'z0.operation In') + """) 
/*END SQL*/

""", db_type='ARIES', macro_state=ctx.macro)

    ctx.csv_io.write('yeuchuan_a2_15507.tab', result)


def step_0049_sqlite_query(ctx):
    ctx.sqlite_engine.run_join(
        sql="""

DROP INDEX IF EXISTS IdxA2;
Create Index IF NOT EXISTS IdxA2 ON [yeuchuan_a2_15507] ([visual_id_1]);

SELECT /*L0*/  DISTINCT 
          a0.[facility] AS [facility]
         ,a0.[lot] AS [lot]
         ,a0.[operation] AS [operation]
         ,a0.[test_end_date] AS [test_end_date]
         ,a0.[tester_id] AS [tester_id]
         ,a0.[program_name] AS [program_name]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,a0.[visual_id] AS [visual_id]
         ,a0.[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,a0.[ws_loss_code] AS [ws_loss_code]
         ,a2.[entity] AS [entity]
         ,a2.[bond_station] AS [bond_station]
         ,a0.[carrier_x] AS [carrier_x]
         ,a0.[carrier_y] AS [carrier_y]
         ,a0.[lane_number] AS [lane_number]
         ,CrossTab->[[a0,15507;:Y]]
         ,[entity]  ||  '_' || [bond_station]  ||  '_' ||  [carrier_x]  ||   '_' || [carrier_y] AS [Entity_BS_X_Y]
FROM 
           [yeuchuan_a0_15507] a0
 LEFT OUTER JOIN [yeuchuan_a2_15507] a2
  ON a0.[visual_id] = a2.[visual_id_1]
""",
        inputs=['yeuchuan_a0_15507.tab', 'yeuchuan_a2_15507.tab'],
        output='CSR_Server_OIS_subplane.csv',
    )


def step_0050_step_9_1_1_fetching_text_sqlite_data(ctx):
    ctx.sqlite_engine.run_join(
        sql="""

DROP INDEX IF EXISTS IdxA0;
Create Index IF NOT EXISTS IdxA0 ON [CSR_Server_OIS_Product_List] ([prodgroup3],[site]);

SELECT /*L3*/  DISTINCT 
          [facility] AS [facility]
         ,[lot] AS [lot]
         ,[operation] AS [operation]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[program_name] AS [program_name]
         ,[prodgroup3] AS [prodgroup3]
         ,[visual_id] AS [visual_id]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[ws_loss_code] AS [ws_loss_code]
         ,[entity] AS [entity]
         ,[bond_station] AS [bond_station]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[lane_number] AS [lane_number]
         ,[entity_bs_x_y] AS [entity_bs_x_y]
         ,[site] AS [site]
         ,[prodgroup3_1] AS [prodgroup3_1]
         ,[sub_plane_x] AS [sub_plane_x]
         ,[sub_plane_y] AS [sub_plane_y]
         ,[lower_x_limit] AS [lower_x_limit]
         ,[upper_x_limit] AS [upper_x_limit]
         ,[lower_y_limit] AS [lower_y_limit]
         ,[upper_y_limit] AS [upper_y_limit]
         ,[Set_Limit_plane_X] AS [Set_Limit_plane_X]
         ,[Set_Limit_plane_Y] AS [Set_Limit_plane_Y]
         ,[Flag] AS [Flag]
         ,DENSE_RANK () OVER (PARTITION BY  [entity_bs_x_y]  ORDER BY    [visual_id]    ASC) AS [Dense_rank]
FROM
(
SELECT /*L2*/ 
          [facility] AS [facility]
         ,[lot] AS [lot]
         ,[operation] AS [operation]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[program_name] AS [program_name]
         ,[prodgroup3] AS [prodgroup3]
         ,[visual_id] AS [visual_id]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[ws_loss_code] AS [ws_loss_code]
         ,[entity] AS [entity]
         ,[bond_station] AS [bond_station]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[lane_number] AS [lane_number]
         ,[entity_bs_x_y] AS [entity_bs_x_y]
         ,[site] AS [site]
         ,[prodgroup3_1] AS [prodgroup3_1]
         ,[sub_plane_x] AS [sub_plane_x]
         ,[sub_plane_y] AS [sub_plane_y]
         ,[lower_x_limit] AS [lower_x_limit]
         ,[upper_x_limit] AS [upper_x_limit]
         ,[lower_y_limit] AS [lower_y_limit]
         ,[upper_y_limit] AS [upper_y_limit]
         ,[Set_Limit_plane_X] AS [Set_Limit_plane_X]
         ,[Set_Limit_plane_Y] AS [Set_Limit_plane_Y]
         ,CASE  WHEN   [Set_Limit_plane_Y]  = 'Y_flag' AND   [Set_Limit_plane_X]   <> 'X_flag' THEN 'Y_flag_only'  ELSE '' END AS [BeyondY_Flag]
         ,CASE  WHEN  [Set_Limit_plane_Y]    = 'Y_flag' THEN 'flag'   ELSE '' END AS [Flag]
FROM
(
SELECT /*L1*/ 
          [facility] AS [facility]
         ,[lot] AS [lot]
         ,[operation] AS [operation]
         ,[test_end_date] AS [test_end_date]
         ,[tester_id] AS [tester_id]
         ,[program_name] AS [program_name]
         ,[prodgroup3] AS [prodgroup3]
         ,[visual_id] AS [visual_id]
         ,[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,[ws_loss_code] AS [ws_loss_code]
         ,[entity] AS [entity]
         ,[bond_station] AS [bond_station]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[lane_number] AS [lane_number]
         ,[entity_bs_x_y] AS [entity_bs_x_y]
         ,[site] AS [site]
         ,[prodgroup3_1] AS [prodgroup3_1]
         ,[sub_plane_x] AS [sub_plane_x]
         ,[sub_plane_y] AS [sub_plane_y]
         ,[lower_x_limit] AS [lower_x_limit]
         ,[upper_x_limit] AS [upper_x_limit]
         ,[lower_y_limit] AS [lower_y_limit]
         ,[upper_y_limit] AS [upper_y_limit]
         ,CASE WHEN     [sub_plane_x]    Not Between    [lower_x_limit]  AND     [upper_x_limit]  THEN 'X_flag' ELSE '' END AS [Set_Limit_plane_X]
         ,CASE WHEN     [sub_plane_y]    Not Between    [lower_y_limit]    AND      [upper_y_limit]  THEN 'Y_flag' ELSE '' END AS [Set_Limit_plane_Y]
FROM
(
SELECT /*L0*/  
          a1.[facility] AS [facility]
         ,a1.[lot] AS [lot]
         ,a1.[operation] AS [operation]
         ,a1.[test_end_date] AS [test_end_date]
         ,a1.[tester_id] AS [tester_id]
         ,a1.[program_name] AS [program_name]
         ,a1.[prodgroup3] AS [prodgroup3]
         ,a1.[visual_id] AS [visual_id]
         ,a1.[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,a1.[ws_loss_code] AS [ws_loss_code]
         ,a1.[entity] AS [entity]
         ,a1.[bond_station] AS [bond_station]
         ,a1.[carrier_x] AS [carrier_x]
         ,a1.[carrier_y] AS [carrier_y]
         ,a1.[lane_number] AS [lane_number]
         ,a1.[entity_bs_x_y] AS [entity_bs_x_y]
         ,a0.[site] AS [site]
         ,a0.[prodgroup3] AS [prodgroup3_1]
         ,CASE WHEN a1.[subplaneanglex] = '' THEN NULL ELSE CAST (a1.[subplaneanglex] AS REAL) END AS [sub_plane_x]
         ,CASE WHEN a1.[subplaneangley] = '' THEN NULL ELSE CAST (a1.[subplaneangley] AS REAL) END AS [sub_plane_y]
         ,CASE WHEN a0.[lower_x_limit] = '' THEN NULL ELSE CAST (a0.[lower_x_limit] AS REAL) END AS [lower_x_limit]
         ,CASE WHEN a0.[upper_x_limit] = '' THEN NULL ELSE CAST (a0.[upper_x_limit] AS REAL) END AS [upper_x_limit]
         ,CASE WHEN a0.[lower_y_limit] = '' THEN NULL ELSE CAST (a0.[lower_y_limit] AS REAL) END AS [lower_y_limit]
         ,CASE WHEN a0.[upper_y_limit] = '' THEN NULL ELSE CAST (a0.[upper_y_limit] AS REAL) END AS [upper_y_limit]
FROM 
           [CSR_Server_OIS_subplane] a1
 LEFT OUTER JOIN [CSR_Server_OIS_Product_List] a0
  ON a0.[prodgroup3] = a1.[prodgroup3] 
 AND a0.[site] = a1.[facility] 
) t /*L0*/
) t /*L1*/
) t /*L2*/
WHERE
              [Flag] = 'flag'
""",
        inputs=['CSR_Server_OIS_subplane.csv', 'CSR_Server_OIS_Product_List.csv'],
        output='CSR_Server_OIS_subplane_interim.csv',
    )


def step_0051_step_10_1_1_fetching_text_sqlite_data(ctx):
    ctx.sqlite_engine.run_join(
        sql="""
SELECT /*L0*/ 
          a0.[facility] AS [facility]
         ,a0.[lot] AS [lot]
         ,a0.[operation] AS [operation]
         ,a0.[test_end_date] AS [test_end_date]
         ,a0.[tester_id] AS [tester_id]
         ,a0.[program_name] AS [program_name]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,a0.[visual_id] AS [visual_id]
         ,a0.[tray_or_carrier_id] AS [tray_or_carrier_id]
         ,a0.[ws_loss_code] AS [ws_loss_code]
         ,a0.[entity] AS [entity]
         ,a0.[bond_station] AS [bond_station]
         ,a0.[carrier_x] AS [carrier_x]
         ,a0.[carrier_y] AS [carrier_y]
         ,a0.[lane_number] AS [lane_number]
         ,a0.[entity_bs_x_y] AS [entity_bs_x_y]
         ,a0.[site] AS [site]
         ,a0.[prodgroup3_1] AS [prodgroup3_1]
         ,a0.[sub_plane_x] AS [sub_plane_x]
         ,a0.[sub_plane_y] AS [sub_plane_y]
         ,a0.[lower_x_limit] AS [lower_x_limit]
         ,a0.[upper_x_limit] AS [upper_x_limit]
         ,a0.[lower_y_limit] AS [lower_y_limit]
         ,a0.[upper_y_limit] AS [upper_y_limit]
         ,a0.[set_limit_plane_x] AS [set_limit_plane_x]
         ,a0.[set_limit_plane_y] AS [set_limit_plane_y]
         ,a0.[flag] AS [flag]
         ,a0.[dense_rank] AS [dense_rank]
         ,'CSR_HOLD' AS [CSR_trigger]
FROM 
[CSR_Server_OIS_subplane_interim] a0
WHERE
              a0.[dense_rank] Not In ('1'
,'2')
""",
        inputs=['CSR_Server_OIS_subplane_interim.csv'],
        output='CSR_Server_OIS_subplane_output.csv',
    )


def step_0052_rows_in_file(ctx):
    ctx.macro.set_named('FLAG', str(ctx.csv_io.row_count('CSR_Server_OIS_subplane_output.csv')))


def step_0054_step_13_1_1_fetching_text_sqlite_data(ctx):
    ctx.sqlite_engine.run_join(
        sql="""
SELECT /*L0*/ 
          a0.[facility] AS [facility]
         ,a0.[lot] AS [lot]
         ,a0.[prodgroup3] AS [prodgroup3]
         ,a0.[operation] AS [DLA_operation]
         ,a0.[entity] AS [entity]
         ,a0.[bond_station] AS [bond_station]
         ,a0.[carrier_x] AS [carrier_x]
         ,a0.[carrier_y] AS [carrier_y]
         ,a0.[visual_id] AS [visual_id]
         ,a0.[sub_plane_x] AS [sub_plane_x]
         ,a0.[sub_plane_y] AS [sub_plane_y]
         ,a0.[lower_x_limit] AS [lower_x_limit]
         ,a0.[upper_x_limit] AS [upper_x_limit]
         ,a0.[lower_y_limit] AS [lower_y_limit]
         ,a0.[upper_y_limit] AS [upper_y_limit]
FROM 
[CSR_Server_OIS_subplane_output] a0
WHERE
 NOT          (a0.[lot] In 
""" + ctx.sql_macros.sql_get_csv_list('.\\HIST.csv', 1, 'a0.[lot] In') + """)
""",
        inputs=['CSR_Server_OIS_subplane_output.csv'],
        output='yeuchuan_SQL_15507.tab',
    )


def step_0055_step_13_1_2_a1_fetching_mars_data(ctx):
    result = read(sql="""
/*BEGIN SQL*/
SELECT 
          f0.lot AS lot_1
         ,f0.operation AS Current_operation
         ,f0.movedin AS movedin
         ,f0.onrework AS onrework
         ,f0.onhold AS onhold
         ,f0.route AS route
         ,f0.qty1 AS quantity
FROM 
@[]@.F_Lot f0
WHERE f0.owner <> 'EMPTYFOUP'
 AND      f0.terminated = 'N' 
 AND      f0.qty1 > 0 
 AND      f0.src_erase_date Is Null  
 AND      (f0.lot In 
""" + ctx.sql_macros.sql_get_csv_list('.\\yeuchuan_SQL_15507.tab', 'lot', 'f0.lot In') + """) 
/*END SQL*/

""", db_type='MARS', macro_state=ctx.macro)

    ctx.csv_io.write('yeuchuan_a1_15507.tab', result)


def step_0056_sqlite_query(ctx):
    ctx.sqlite_engine.run_join(
        sql="""

DROP INDEX IF EXISTS IdxA1;
Create Index IF NOT EXISTS IdxA1 ON [yeuchuan_a1_15507] ([lot_1]);

SELECT /*L1*/  DISTINCT 
          [facility] AS [facility]
         ,[lot] AS [lot]
         ,[prodgroup3] AS [prodgroup3]
         ,[DLA_operation] AS [DLA_operation]
         ,[lot_1] AS [lot_1]
         ,[Current_operation] AS [Current_operation]
         ,[movedin] AS [movedin]
         ,[onrework] AS [onrework]
         ,[onhold] AS [onhold]
         ,[route] AS [route]
         ,[quantity] AS [quantity]
         ,[Lot_MVIN_CURE] AS [Lot_MVIN_CURE]
         ,[entity] AS [entity]
         ,[bond_station] AS [bond_station]
         ,[carrier_x] AS [carrier_x]
         ,[carrier_y] AS [carrier_y]
         ,[visual_id] AS [visual_id]
         ,[sub_plane_x] AS [sub_plane_x]
         ,[sub_plane_y] AS [sub_plane_y]
         ,[lower_x_limit] AS [lower_x_limit]
         ,[upper_x_limit] AS [upper_x_limit]
         ,[lower_y_limit] AS [lower_y_limit]
         ,[upper_y_limit] AS [upper_y_limit]
FROM
(
SELECT /*L0*/  
          sql.[facility] AS [facility]
         ,sql.[lot] AS [lot]
         ,sql.[prodgroup3] AS [prodgroup3]
         ,sql.[DLA_operation] AS [DLA_operation]
         ,a1.[lot_1] AS [lot_1]
         ,a1.[Current_operation] AS [Current_operation]
         ,a1.[movedin] AS [movedin]
         ,a1.[onrework] AS [onrework]
         ,a1.[onhold] AS [onhold]
         ,a1.[route] AS [route]
         ,a1.[quantity] AS [quantity]
         ,CASE  WHEN [Current_operation]  IN ('1266') THEN 'N' WHEN [Current_operation]  IN ('1501') THEN 'N' WHEN [Current_operation]  IN ('1366') THEN 'N' WHEN [Current_operation]  IN ('1265') THEN 'N' WHEN [Current_operation]  IN ('1264') THEN 'N'  ELSE 'Y' END AS [Lot_MVIN_CURE]
         ,sql.[entity] AS [entity]
         ,sql.[bond_station] AS [bond_station]
         ,sql.[carrier_x] AS [carrier_x]
         ,sql.[carrier_y] AS [carrier_y]
         ,sql.[visual_id] AS [visual_id]
         ,sql.[sub_plane_x] AS [sub_plane_x]
         ,sql.[sub_plane_y] AS [sub_plane_y]
         ,sql.[lower_x_limit] AS [lower_x_limit]
         ,sql.[upper_x_limit] AS [upper_x_limit]
         ,sql.[lower_y_limit] AS [lower_y_limit]
         ,sql.[upper_y_limit] AS [upper_y_limit]
FROM 
           [yeuchuan_SQL_15507] sql
 LEFT OUTER JOIN [yeuchuan_a1_15507] a1
  ON sql.[lot] = a1.[lot_1] 
) t /*L0*/
WHERE
              [Lot_MVIN_CURE] = 'Y'
""",
        inputs=['yeuchuan_SQL_15507.tab', 'yeuchuan_a1_15507.tab'],
        output='Data.csv',
    )


def run() -> None:
    ctx = pipeline_ctx
    step_0000_step_1_1_create_an_html_report(ctx)
    step_0001_html_report(ctx)
    step_0002_html_report(ctx)
    step_0003_step_1_2_create_macro_tmp_update_script_name_here(ctx)
    step_0004_step_1_4_create_getcsrsu_bat(ctx)
    step_0005_step_1_5_run_getcsrsu_bat(ctx)
    for __row in ctx.csv_io.iter('macrotmp.csv'):
        with ctx.macro_scope(__row):
            step_0007_step_1_7_run_setsiteparam_exe(ctx)
    step_0009_step_1_8_delete_temporary_files(ctx)
    for __row in ctx.csv_io.iter('ctime.csv'):
        with ctx.macro_scope(__row):
            step_0011_rows_in_file(ctx)
            if int(ctx.macro.named("CONFIG")) <= int('0'):
                step_0013_step_1_12_trigger_if_config_file_not_found(ctx)
            else:
                step_0015_step_1_13_1_transpose_config_file_to_macro_friendly_format(ctx)
                step_0016_rows_in_file(ctx)
                if int(ctx.macro.named("CONFIGSETS")) != int('1'):
                    step_0018_step_1_16_trigger_if_converted_config_file_contains_not_equal_to_1_row(ctx)
                else:
                    for __row in ctx.csv_io.iter('configsets.csv'):
                        with ctx.macro_scope(__row):
                            if ctx.macro.named("CSRV") == 'FAIL' and ctx.macro.named("UNDERDEV") == 'N':
                                step_0022_step_1_19_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx)
                                step_0023_step_1_20_email_when_user_have_no_access_to_csr(ctx)
                            if ctx.macro.named("MMSV") == 'FAIL' and ctx.macro.named("UNDERDEV") == 'N':
                                step_0026_step_1_22_write_text_to_a_file_optionally_use_eof_to_mark_end_of_file(ctx)
                                step_0027_step_1_23_email_when_user_have_no_access_to_mms_signal_tracer(ctx)
                            step_0029_step_1_24_robocopy_hist_txt(ctx)
                            step_0030_rows_in_file(ctx)
                            if int(ctx.macro.named("HIST")) <= int('0'):
                                step_0032_step_1_27_create_dummy_hist_csv(ctx)
                                step_0033_step_1_28_create_histerror_txt(ctx)
                            else:
                                step_0035_step_1_29_convert_hist_txt_to_hist_csv(ctx)
    for __row in ctx.csv_io.iter('configsets.csv'):
        with ctx.macro_scope(__row):
            step_0042_step_4_1_copy_files_folders(ctx)
            step_0043_step_4_2_1_fetching_text_sqlite_data(ctx)
            step_0044_step_5_1_1_a0_fetching_mars_data(ctx)
            step_0045_rows_in_file(ctx)
            if int(ctx.macro.named("LOTS")) > int('0'):
                step_0047_step_8_1_1_a0_fetching_aries_data(ctx)
                step_0048_step_8_1_1_a2_fetching_aries_data(ctx)
                step_0049_sqlite_query(ctx)
                step_0050_step_9_1_1_fetching_text_sqlite_data(ctx)
                step_0051_step_10_1_1_fetching_text_sqlite_data(ctx)
                step_0052_rows_in_file(ctx)
                if int(ctx.macro.named("FLAG")) > int('0'):
                    step_0054_step_13_1_1_fetching_text_sqlite_data(ctx)
                    step_0055_step_13_1_2_a1_fetching_mars_data(ctx)
                    step_0056_sqlite_query(ctx)

if __name__ == "__main__":
    run()