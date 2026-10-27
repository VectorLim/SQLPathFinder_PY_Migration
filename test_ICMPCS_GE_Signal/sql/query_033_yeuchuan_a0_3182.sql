-- vg2c-output-headers: ["ww", "prod", "subs", "lot", "qty", "out_date", "attribute_name", "owner", "current_time"]

/*BEGIN SQL*/
SELECT  DISTINCT 
          ww AS ww
         ,prod AS prod
         ,Replace(Replace(Replace(Replace(Replace(Replace(subs,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS subs
         ,lot AS lot
         ,qty AS qty
         ,To_Char(out_date,'yyyy-mm-dd hh24:mi:ss') AS out_date
         ,attribute_name AS attribute_name
         ,owner AS owner
         ,To_Char(SYSDATE,'yyyy-mm-dd hh24:mi:ss') AS current_time
FROM
(
SELECT  
          c0.ww AS ww
         ,p.prodgroup3 AS prod
         ,f12.attribute_value AS subs
         ,f0.lot AS lot
         ,f0.newqty1 AS qty
         ,f0.out_date AS out_date
         ,(SELECT alpha1 as attribute_name FROM @[]@.F_GTSData WHERE facility IN ('IMORA','IMO','COMETS') AND table_name = 'COM$ATTR' AND key1 = 'LOT' AND src_erase_date IS NULL AND key2_numeric = f12.attribute_number AND rownum <= 1) AS attribute_name
         ,f0.owner AS owner
FROM 
@[]@.F_LotHist f0
INNER JOIN @[]@.F_Calendar c0 ON f0.last_action_date BETWEEN c0.start_date AND c0.end_date AND c0.event_code = 'S' AND decode(f0.facility,'RA3','AAL',f0.facility)= c0.facility
LEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
INNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot
LEFT JOIN @[]@.F_LotAttribute f12 ON f9.lot = f12.lot
WHERE
NVL(f0.history_deleted_flag,'N') = 'N'
AND      f0.owner <> 'EMPTYFOUP'
 AND      f0.operation = '1210' 
 AND      f0.last_pass = 'Y' 
 AND      f0.movedout = 'Y' 
 AND      f0.owner = 'PROD' 
 AND      f0.out_date Between To_Date('28-Jun-2026 00:00:00','dd-Mon-yyyy hh24:mi:ss') AND To_Date('30-Jun-2026 00:00:00','dd-Mon-yyyy hh24:mi:ss') 
-- Tail A
)
WHERE
              attribute_name = 'SUB VEND ID' 
/*END SQL*/


