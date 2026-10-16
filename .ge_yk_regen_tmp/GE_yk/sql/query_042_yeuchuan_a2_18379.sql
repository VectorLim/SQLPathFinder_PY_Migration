
SELECT * FROM (
SELECT  DISTINCT 
          "test_name_1" AS "test_name_1"
         ,"complevel_1_1" AS "complevel_1_1"
         ,Max ("all_value_1") AS "all_value_1"
         ,"visual_id_2" AS "visual_id_2"
         ,"entity" AS "entity"
FROM
(
SELECT  
          "test_name_1" AS "test_name_1"
         ,"complevel_1_1" AS "complevel_1_1"
         ,"all_value_1" AS "all_value_1"
         ,"visual_id_2" AS "visual_id_2"
         ,"entity" AS "entity"
FROM 
<DDBTable>t0</DDBTable>
)
GROUP BY 
          "test_name_1"
         ,"complevel_1_1"
         ,"visual_id_2"
         ,"entity"
)

