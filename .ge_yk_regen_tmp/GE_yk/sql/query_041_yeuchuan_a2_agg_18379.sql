-- vg2c-output-headers: ["test_name_1", "complevel_1_1", "all_value_1", "visual_id_2", "entity"]

/*BEGIN SQL*/
SELECT 
          t.test_name AS test_name_1
         ,ctr.complevel_1 AS complevel_1_1
         ,CASE WHEN ctr.string_value IS NULL THEN to_char(ctr.numeric_result) ELSE ctr.string_value END AS all_value_1
         ,di.visual_id AS visual_id_2
         ,ats.tester_id AS entity
FROM 
A_Testing_Session ats
INNER JOIN A_All_Component_Testing_Result ctr ON ctr.lao_start_ww = ats.lao_start_ww AND ctr.ts_id = ats.ts_id AND (ctr.numeric_result IS NOT NULL or ctr.string_value is NOT NULL)
INNER JOIN A_Test t ON t.t_id = ctr.t_id
INNER JOIN A_Device_Testing dt ON dt.lao_start_ww = ats.lao_start_ww AND dt.ts_id = ats.ts_id
AND dt.lao_start_ww = ctr.lao_start_ww AND dt.ts_id = ctr.ts_id AND dt.dt_id = ctr.dt_id
LEFT JOIN A_Device_Item di ON di.di_id = dt.di_id
WHERE ats.data_domain='METROLOGY'
 AND      (ats.lot In 
SQL_Get_CSV_List(".\yeuchuan_a0_18379.tab->100", lot, "ats.lot In") 
 AND      ats.program_name Like 'PNP%' 
 AND      ctr.complevel_1 Like 'U%' 
/*END SQL*/


