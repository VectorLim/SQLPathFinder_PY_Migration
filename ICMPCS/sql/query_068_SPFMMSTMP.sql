
SELECT DISTINCT 
 'BA' AS [MODULE]
,'ICMPCS' AS [SIGNALTYPE]
,'IAM_CWF_NCO' AS [ALARMTYPE]
,'<<<dEmail>>>' AS [EMAIL]
,'Y' AS [HOLD_LOT]
,'N' AS [AUTO_CONTAIN]
,[prodgroup3] AS [PRODGROUP3]
,[lot] AS [IMPACT_LOT_VI]
,[visual_id] AS [IMPACT_VIDS]
               ,'Y' AS [MMS]
               ,'Y' AS [SHOULD_EMAIL]
               ,CAST(DATETIME('now','localtime') AS TEXT)||'.000000' AS [DATE]
               ,'' AS [FACILITY]
FROM [T0]
WHERE 1=1


