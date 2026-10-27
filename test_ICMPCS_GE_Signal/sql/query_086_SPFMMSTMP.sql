-- vg2c-output-headers: ["MODULE", "SIGNALTYPE", "ALARMTYPE", "EMAIL", "HOLD_LOT", "AUTO_CONTAIN", "PRODGROUP3", "ENTITY", "IMPACT_LOT_VI"]

SELECT DISTINCT 
 'CAM' AS [MODULE]
,'ICMPCS' AS [SIGNALTYPE]
,'CAM_GE' AS [ALARMTYPE]
,'EMAIL:<<<DEMAIL>>>' AS [EMAIL]
,'N' AS [HOLD_LOT]
,'N' AS [AUTO_CONTAIN]
,[prodgroup3] AS [PRODGROUP3]
,[link_id] AS [ENTITY]
,[lot] AS [IMPACT_LOT_VI]
               ,'Y' AS [MMS]
               ,'Y' AS [SHOULD_EMAIL]
               ,CAST(DATETIME('now','localtime') AS TEXT)||'.000000' AS [DATE]
               ,'' AS [FACILITY]
FROM [T0]
WHERE 1=1


