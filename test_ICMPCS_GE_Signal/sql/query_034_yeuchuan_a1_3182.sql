-- vg2c-output-headers: ["link", "lot_1", "program_name", "lot_2"]

/*BEGIN SQL*/
SELECT  DISTINCT 
          ats.link_id AS link
         ,ats.lot AS lot_1
         ,ats.program_name AS program_name
         ,ats.lot AS lot_2
FROM 
A_Testing_Session ats
WHERE ats.data_domain='METROLOGY'
 AND      ats.operation = '1210' 
 AND      (ats.lot In 
SQL_Get_CSV_List(".\yeuchuan_a0_3182.tab", lot, "ats.lot In") 
 AND      ats.latest_flag = 'Y' 
 AND      ats.valid_flag = 'Y' 
 AND      ats.program_name = 'DPMS_PREBTU' 
/*END SQL*/


