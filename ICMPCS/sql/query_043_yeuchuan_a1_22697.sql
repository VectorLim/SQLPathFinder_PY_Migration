
/*BEGIN SQL*/
SELECT 
          lot_1 AS lot_1
         ,operation_1 AS operation_1
         ,To_Char(out_date,'yyyy-mm-dd hh24:mi:ss') AS out_date
         ,oldqty1 AS oldqty1
         ,newqty1 AS newqty1
         ,Replace(Replace(Replace(Replace(Replace(Replace(Interposer_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Interposer_SLI
         ,Replace(Replace(Replace(Replace(Replace(Replace(Patch_SLI,',',';'),chr(9),' '),chr(10),' '),chr(13),' '),chr(34),''''),chr(7),' ') AS Patch_SLI
         ,prodgroup3_1 AS prodgroup3_1
         ,entity AS entity
         ,transaction AS transaction
FROM
(
SELECT  
          f0.lot AS lot_1
         ,f0.operation AS operation_1
         ,f0.out_date AS out_date
         ,f0.oldqty1 AS oldqty1
         ,f0.newqty1 AS newqty1
         ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 5005 AND la.src_erase_date IS NULL AND rownum <= 1) AS Interposer_SLI
         ,(SELECT la.attribute_value FROM @[]@.F_LotAttribute la where la.lot= f9.lot AND la.attribute_number = 5001 AND la.src_erase_date IS NULL AND rownum <= 1) AS Patch_SLI
         ,p.prodgroup3 AS prodgroup3_1
         ,f4.entity AS entity
         ,f5.transaction AS transaction
FROM 
@[]@.F_LotHist f0
LEFT JOIN @[]@.F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
INNER JOIN @[]@.F_Lot f9 ON f9.lot = f0.lot
LEFT JOIN @[]@.F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = 'Y'
 AND      f4.entity Like 'IAM%' 
LEFT JOIN @[]@.F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource
LEFT JOIN @[]@.F_LotTxnHist f5 ON f5.lot = f0.lot AND f5.operation = f0.operation AND f5.prevout_date = f0.prevout_date AND NVL(f5.history_deleted_flag,'N') = 'N'
 AND      f5.transaction = 'MVOU' 
WHERE
NVL(f0.history_deleted_flag,'N') = 'N'
AND      f0.owner <> 'EMPTYFOUP'
 AND      f0.operation = '2303' 
 AND      f0.out_date >= TRUNC(SYSDATE) - 2 
 AND      p.prodgroup3 Like 'CWF%' 
-- Tail A
)
WHERE
              Interposer_SLI Is Not Null  
/*END SQL*/


