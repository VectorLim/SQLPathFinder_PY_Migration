"""Inside a Linux image: prepare_job reads config/HIST from a mounted share with no SMB_* variables."""

import os
import tempfile
from pathlib import Path

from scripthost_portable import aed_api

assert not [k for k in os.environ if k.upper().startswith("SMB_")], "SMB_* must be absent"
aed_api._load_client = lambda workdir: None  # no X_API_KEY here; AED calls are not under test
work = Path(tempfile.mkdtemp())
aed_api.prepare_job(work, '/UTILITIES={AED} "AED_CANDIDATES.csv"')
print("ICMPCS_ROOT:", os.environ.get("ICMPCS_ROOT", aed_api.DEFAULT_ROOT))
print("config keys:", sorted(os.environ[k] and k for k in ("SITE", "MARS", "ARIES", "AED_FACILITY",
                                                         "ATTR_LIST", "SKIP_OPERATION", "HIST_PATH")))
print("MARS:", os.environ["MARS"], "| AED_FACILITY:", os.environ["AED_FACILITY"])
print("HIST_PATH:", os.environ["HIST_PATH"])
print("history rows:", len(aed_api._read_history(os.environ["HIST_PATH"])))
