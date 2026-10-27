
/*BEGIN SQL*/
SELECT  DISTINCT 
          ww AS ww
         ,link_id AS link_id
         ,prodgroup3 AS prodgroup3
         ,program_name AS program_name
         ,To_Char(test_end_date,'yyyy-mm-dd hh24:mi:ss') AS test_end_date
         ,tester_id AS tester_id
         ,lot AS lot
         ,visual_id AS visual_id
         ,complevel_1 AS complevel_1
         ,complevel_2 AS complevel_2
         ,test_name AS test_name
         ,Max(all_value) AS all_value
         ,tray_or_carrier_id AS tray_or_carrier_id
         ,carrier_x AS carrier_x
         ,carrier_y AS carrier_y
FROM
(
SELECT  
          ats.lao_start_ww AS ww
         ,ats.link_id AS link_id
         ,mp.prodgroup3 AS prodgroup3
         ,ats.program_name AS program_name
         ,ats.test_end_date_time AS test_end_date
         ,ats.tester_id AS tester_id
         ,ats.lot AS lot
         ,di.visual_id AS visual_id
         ,ctr.complevel_1 AS complevel_1
         ,ctr.complevel_2 AS complevel_2
         ,t.test_name AS test_name
         ,CASE WHEN ctr.string_value IS NULL THEN to_char(ctr.numeric_result) ELSE ctr.string_value END AS all_value
         ,dt.testing_session_tray_id AS tray_or_carrier_id
         ,dt.carrier_x AS carrier_x
         ,dt.carrier_y AS carrier_y
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
 AND      ats.operation = '1210' 
 AND      ats.latest_flag = 'Y' 
 AND      ats.valid_flag = 'Y' 
 AND      ctr.latest_flag = 'Y' 
 AND      dt.within_lao_latest_flag = 'Y' 
 AND      dt.within_lao_latest_flag = 'Y' 
 AND      dt.latest_flag = 'Y' 
 AND      ctr.complevel_1 Like 'U%' 
 AND      (ats.lot In 
SQL_Get_CSV_List(".\temp_cam_lot_list.csv", "5", "ats.lot In") 
 AND      ats.program_name = 'DPMS_PREBTU' 
)
GROUP BY 
          ww
         ,link_id
         ,prodgroup3
         ,program_name
         ,test_end_date
         ,tester_id
         ,lot
         ,visual_id
         ,complevel_1
         ,complevel_2
         ,test_name
         ,tray_or_carrier_id
         ,carrier_x
         ,carrier_y
/*END SQL*/


