"""Inside a Linux image: prepare_job reads config.txt over SMB with CIFS_* and keeps HIST in workdir."""

import os
import tempfile
from pathlib import Path

from scripthost_portable import aed_api

aed_api._load_client = (
    lambda workdir: None
)  # no X_API_KEY here; AED calls are not under test
work = Path(tempfile.mkdtemp())
aed_api.prepare_job(
    work, '/UTILITIES={AED} "AED_CANDIDATES.csv"', "/app/jobs/ICMPCS_CWFNCO_CSR_IAM.txt"
)
print("SFOLDER:", os.environ["SFOLDER"], "| SITE:", os.environ["SITE"])
print("MARS:", os.environ["MARS"], "| AED_FACILITY:", os.environ["AED_FACILITY"])
print(
    "config keys:",
    sorted(
        k for k in (work / "config.json").read_text().split('"') if k.isidentifier()
    )[:12],
)
print("HIST_PATH:", os.environ["HIST_PATH"])
