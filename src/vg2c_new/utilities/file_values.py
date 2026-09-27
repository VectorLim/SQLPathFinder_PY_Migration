from __future__ import annotations

from datetime import datetime, timedelta
from pathlib import Path
from typing import TYPE_CHECKING

import pandas as pd

from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility
from vg2c_new.utilities.csv import CsvUtility

if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState


class UpdateTimeUtility(Utility):
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of ScriptHost Utilities.Do_Update_Time.
        Source: SPSQL3_py/SPFLib/SPFUtilities/utils.py :: Utilities.Do_Update_Time.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: site-time CSV and standard incremental offset columns.
        Amendments: RuntimeState replaces gMySPFJobTime. Intentionally discarded: .spf$data/global abort state.
        """
        args = [state.substitute(v) for v in command.arguments]
        if not args:
            raise ValueError("{UPDATE-TIME} requires an output file.")
        text = state.lookup("SPF_SITE_TIME") or state.lookup("SPF_JOB_DT")
        if not text:
            raise RuntimeError("No site time is available; execute {GET-SITE-TIME} first.")
        dt = datetime.strptime(text, "%Y-%m-%d %H:%M:%S")
        midnight = dt.replace(hour=0, minute=0, second=0, microsecond=0)
        values = {
            "last_Date": dt,
            "Last_Date-15m": dt - timedelta(minutes=15),
            "Last_Date-30m": dt - timedelta(minutes=30),
            "Last_Date-45m": dt - timedelta(minutes=45),
            "Last_Date-60m": dt - timedelta(minutes=60),
            "Last_Date-90m": dt - timedelta(minutes=90),
            "Last_Date-120m": dt - timedelta(minutes=120),
            "Last_Date-1d": dt - timedelta(days=1),
            "Last_Date-2d": dt - timedelta(days=2),
            "Last_Date-7d": dt - timedelta(days=7),
            "Last_Date-6h": dt - timedelta(hours=6),
            "Last_Date-8h": dt - timedelta(hours=8),
            "Last_Date-12h": dt - timedelta(hours=12),
            "Last_Date-1d-Midnight": midnight - timedelta(days=1),
            "Last_Date-2d-Midnight": midnight - timedelta(days=2),
            "Last_Date-7d-Midnight": midnight - timedelta(days=7),
        }
        CsvUtility.write_dataframe(
            pd.DataFrame([{k: v.strftime("%Y-%m-%d %H:%M:%S") for k, v in values.items()}]),
            _path(command, state, args[0]),
        )


def _path(command: Command, state: RuntimeState, value: str) -> Path:
    return resolve_path(value, state, base=working_directory_for(command.option("WORKDIR"), state))
