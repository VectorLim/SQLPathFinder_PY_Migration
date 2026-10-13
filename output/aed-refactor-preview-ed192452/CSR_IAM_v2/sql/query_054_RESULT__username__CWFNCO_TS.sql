-- vg2c-output-headers: ["HOLD_LOT", "AUTO_CONTAIN", "LOT", "EMAIL", "CCB_NUMBER", "HOLD_NOTE", "HOLDCATEGORY", "Module"]

SELECT DISTINCT 
 [Lot_NCORisk] AS [HOLD_LOT]
,'Y' AS [AUTO_CONTAIN]
,[Lot_NCORisk] AS [LOT]
,'<<<dEmail>>>' AS [EMAIL]
,'<<<dCCB>>>' AS [CCB_NUMBER]
,'<<<dHNote>>>' AS [HOLD_NOTE]
,'<<<dHCat>>>' AS [HOLDCATEGORY]
,'IAM' AS [Module]
FROM [T0] 
WHERE 1=1



