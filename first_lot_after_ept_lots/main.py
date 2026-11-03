from vg2c.runtime import JobRuntime

TEST_MODE_EXCLUDED = {"csr", "email", "mms"}


def run():
    job = JobRuntime.from_script(__file__, test_mode_excluded=TEST_MODE_EXCLUDED)
    interventions = job.sql(
        "sql/query_001_first_lot_after_ept_lots.sql",
        output="ept_interventions.csv",
        node="KM.[A15_PROD_21.].MARS",
    )
    job.macros["LOT_HISTORY_START"] = min(interventions["event_end_key"], default="")[
        :14
    ]
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

    for i in job.read_csv("first_lot_after_ept_lots.csv"):
        print(i)


if __name__ == "__main__":
    run()
