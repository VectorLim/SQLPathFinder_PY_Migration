
SELECT * FROM (
SELECT  DISTINCT 
          "test_name_1" AS "test_name_1"
         ,"complevel_1_1" AS "complevel_1_1"
         ,Max ("all_value_1") AS "all_value_1"
         ,"visual_id_2" AS "visual_id_2"
         ,"entity" AS "entity"
         ,"pnp_lot" AS "pnp_lot"
FROM
(
SELECT  
          "test_name_1" AS "test_name_1"
         ,"complevel_1_1" AS "complevel_1_1"
         ,"all_value_1" AS "all_value_1"
         ,"visual_id_2" AS "visual_id_2"
         ,"entity" AS "entity"
         ,"pnp_lot" AS "pnp_lot"
FROM 
<DDBTable>t0</DDBTable>
)
GROUP BY 
          "test_name_1"
         ,"complevel_1_1"
         ,"visual_id_2"
         ,"entity"
         ,"pnp_lot"
)

