"""Live read-only check of the migrated CSR_IAM_v2 data path (MARS/ARIES -> SQLite -> AED candidates).

Stops before {AED} and the notification: no SMB, AED writes or email.
DataSyncX credentials come from the repo .env (DATASYNCX_USERNAME/PASSWORD).
"""

import csv
import sys
from pathlib import Path

from scripthost_portable import migrate_aed
from scripthost_portable.aed_api import SITE_FACILITY
from scripthost_portable.worker import ScriptHostJob, run_job

ROOT = Path(__file__).resolve().parents[2]
WORK = Path(__file__).resolve().parent / "work"
WORK.mkdir(exist_ok=True)

blocks = migrate_aed.parse(
    migrate_aed.migrate((ROOT / "CSR_IAM_v2.txt").read_text(encoding="utf-8-sig")).text
)
aed = next(i for i, block in enumerate(blocks) if block.utility == "{aed}")
kept = blocks[:aed] + [blocks[aed + 1], blocks[-1]]
assert [b.utility for b in kept[-2:]] == ["{end-if}", "{end-macro}"]
script = migrate_aed.DELIM.join(block.raw for block in kept)
migrate_aed.pairs(migrate_aed.parse(script))

site = sys.argv[1] if len(sys.argv) > 1 else "KM"
days = sys.argv[2] if len(sys.argv) > 2 else "1"
script = script.replace(
    "f0.out_date >= SYSDATE - 1 ", f"f0.out_date >= SYSDATE - {days} "
)
with (WORK / "configsets.csv").open("w", newline="", encoding="utf-8") as stream:
    writer = csv.writer(stream)
    writer.writerow(["SITE", "MARS", "ARIES", "AED_FACILITY"])
    facility = SITE_FACILITY[site]
    writer.writerow([site, f"{site}.[{facility}.].MARS", f"{site}.ARIES", facility])

result = run_job(
    ScriptHostJob(working_directory=str(WORK), script_text=script), timeout=1800
)
lines = [
    line
    for line in (result.stdout + result.stderr).splitlines()
    if any(k in line.lower() for k in ("rows", "mars", "aries", "ora-", "error"))
]
print("\n".join(lines[-40:]))
if not result.success:
    print(result.stderr[-4000:])
for name in (
    "yeuchuan_a1_28702.tab",
    "yeuchuan_a0_28702.tab",
    "PARMI_IPM_RAW.csv",
    "IPM_Data.csv",
    "AED_CANDIDATES.csv",
):
    path = WORK / name
    rows = (
        max(sum(1 for _ in path.open(encoding="utf-8", errors="replace")) - 1, 0)
        if path.exists()
        else None
    )
    print(f"{name}: {'missing' if rows is None else f'{rows} rows'}")
candidates = WORK / "AED_CANDIDATES.csv"
if candidates.exists():
    print(candidates.read_text(encoding="utf-8")[:1000])
