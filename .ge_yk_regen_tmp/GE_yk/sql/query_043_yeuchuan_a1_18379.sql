-- vg2c-output-headers: ["visual_id_1", "attribute_name", "subs"]

/*BEGIN SQL*/
SELECT  DISTINCT 
          u0.visualid AS visual_id_1
         ,u1.attr_name AS attribute_name
         ,u1.attr_value AS subs
FROM 
A_Ube_Unit_Hist2 u0
INNER JOIN A_Ube_Unit_Hist2_Attr u1 ON u0.creation_ww = u1.creation_ww AND u0.unit_hist_key = u1.unit_hist_key AND u0.facility = u1.facility
 AND      u1.attr_name = 'VENDOR_ID' 
WHERE 1=1
 AND      u0.ws_operation = '0614' 
 AND      (u0.visualid In 
SQL_Get_CSV_List(".\yeuchuan_a0_18379.tab->500", visual_id, "u0.visualid In") 
/*END SQL*/


