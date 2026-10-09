
SELECT /*L0*/  DISTINCT 
          a0.[lot] AS [Lot_NCORisk]
FROM 
[IPM_Data] a0
WHERE
 NOT          (a0.[lot] In 
SQL_Get_CSV_List(".\HIST.csv", "1", "a0.[lot] In")

