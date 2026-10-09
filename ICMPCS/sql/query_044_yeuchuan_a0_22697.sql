
/*BEGIN SQL*/
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
SQL_Get_CSV_List(".\yeuchuan_a1_22697.tab", lot_1, "bams0.lot In") 
 AND      bams0.operation = '2303' 
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


