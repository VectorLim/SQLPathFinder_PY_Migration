from pathlib import Path

import pandas as pd
from dotenv import load_dotenv

from vg2c.runtime import AEDClient, JobRuntime

TEST_MODE_EXCLUDED = {"csr", "email", "mms"}
OUTPUT_COLUMNS = [
    "facility",
    "parent_entity",
    "ept_entity",
    "statehist_id",
    "ept_state",
    "event_start",
    "event_end",
    "lot",
    "operation",
    "first_lot_timestamp",
]


def run():
    job = JobRuntime.from_script(__file__, test_mode_excluded=TEST_MODE_EXCLUDED)
    interventions = job.sql(
        "sql/query_001_first_lot_after_ept_lots.sql",
        output="ept_interventions.csv",
        node="KM.[A15_PROD_21.].MARS",
    )
    if interventions.empty:
        pd.DataFrame(columns=OUTPUT_COLUMNS).to_csv(
            job.workdir / "first_lot_after_ept_lots.csv", index=False
        )
        return
    job.macros["LOT_HISTORY_START"] = min(interventions["event_end_key"])[:14]
    job.sql(
        "sql/query_002_lot_history.sql",
        output="lot_history.csv",
        node="KM.[A15_PROD_21.].MARS",
    )
    job.sql(
        "sql/query_003_match_first_lots.sql",
        output="first_lot_after_ept_lots.csv",
        inputs=[
            ("ept_interventions.csv", "interventions"),
            ("lot_history.csv", "lot_history"),
        ],
    )


if __name__ == "__main__":
    run()
