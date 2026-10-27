-- vg2c-output-headers: ["visual_id", "unit_process_date", "status"]

/*BEGIN SQL*/
SELECT 
          visual_id AS visual_id
         ,To_Char(Max(unit_process_date),'yyyy-mm-dd hh24:mi:ss') AS unit_process_date
         ,Max(status) AS status
FROM
(
SELECT  
          u0.visualid AS visual_id
         ,u0.unit_process_date AS unit_process_date
         ,u0.status_id AS status
FROM 
A_Ube_Unit_Hist2 u0
WHERE 1=1
 AND      (u0.visualid In 
SQL_Get_CSV_List(".\yeuchuan_SQL_3182.tab", visualid, "u0.visualid In") 
 AND      u0.status_id = 0 
)
GROUP BY 
          visual_id
/*END SQL*/


