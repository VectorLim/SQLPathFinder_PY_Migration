from pathlib import Path
import sys

sys.path.insert(0, str(Path(__file__).resolve().parent / "dependencies"))

import pandas as pd

from vg2c.runtime import JobRuntime

# Keep AED enabled in test mode; lot_status is read-only.
TEST_MODE_EXCLUDED = {"email", "mms"}


def run():
    job = JobRuntime.from_script(
        __file__, site="KM", test_mode_excluded=TEST_MODE_EXCLUDED
    )
    interventions = job.sql(
        "sql/query_001_first_lot_after_ept_lots.sql",
        output="ept_interventions.csv",
        database="MARS",
    )
    event_ends = pd.to_datetime(
        interventions["event_end"], format="%Y-%m-%d %H:%M:%S"
    ).dropna()
    job.macros["LOT_HISTORY_START"] = min(
        event_ends.dt.strftime("%Y%m%d%H%M%S"), default=None
    )
    job.sql(
        "sql/query_002_lot_history.sql",
        output="lot_history.csv",
        database="MARS",
    )
    job.sql(
        "sql/query_003_match_first_lots.sql",
        output="first_lot_after_ept_lots.csv",
        inputs=[
            ("ept_interventions.csv", "interventions"),
            ("lot_history.csv", "lot_history"),
        ],
    )

    first_lot_after_ept_df = job.read_csv("first_lot_after_ept_lots.csv")["lot"]
    for lot_num in first_lot_after_ept_df:
        print(job.aed.lot_status(lot_num))


def handle(job_id=None, context=None, header=None, body=None):
    run()
    return True, "Function executed successfully", "Function executed successfully"


if __name__ == "__main__":
    run()
