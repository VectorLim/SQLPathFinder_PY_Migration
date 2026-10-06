"""Prove the live MARS connection returns rows: counts op 2303 lot history per window."""

from pathlib import Path

from scripthost_portable.worker import ScriptHostJob, run_job

WORK = Path(__file__).resolve().parent / "probe"
WORK.mkdir(exist_ok=True)
script = (
    """
<OPTIONS>
/NODE=KM.MARS
/UN=
/PW=
/OLEDB=SQLPlus
/ENGINE=VA
/WORKDIR=.\\
/CSV=probe.csv
/HEADERS=schema_name,any_d1,op2303_d14
</OPTIONS>
/*BEGIN SQL*/
"""
    + "\nUNION ALL\n".join(
        f"SELECT '{s}' AS schema_name,"
        f" (SELECT COUNT(*) FROM {s}.F_LotHist WHERE out_date >= SYSDATE - 1) AS any_d1,"
        f" (SELECT COUNT(DISTINCT lot) FROM {s}.F_LotHist WHERE operation = '2303'"
        f" AND out_date >= SYSDATE - 14) AS op2303_d14 FROM dual"
        for s in ("A13_PROD_20", "A15_PROD_21", "A18_PROD_25")
    )
    + """
/*END SQL*/
"""
)
result = run_job(
    ScriptHostJob(working_directory=str(WORK), script_text=script), timeout=900
)
print("success:", result.success, result.error_category, result.message[-1500:])
out = WORK / "probe.csv"
print(out.read_text() if out.exists() else "probe.csv missing")
