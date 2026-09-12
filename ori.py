# %%
from datetime import datetime, timedelta

# from utils.send_msg import send_msg
from DataSyncX import email_on_exception
from DataSyncX import MarsReader
from DataSyncX import set_log
import pandas as pd
import numpy as np
import shutil
import os
import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).parent.parent))

# main


def mars(site, operation, duration_lot, node):
    try:

        sql_query = f"""

        SELECT DISTINCT
                f0.facility AS facility
                ,f0.lot AS lot
                ,f0.operation AS operation
                ,p.prodgroup3 AS prodgroup3
        FROM 
        {node}F_LotHist f0
        LEFT JOIN {node}F_Product p ON p.product = f0.product AND p.facility = f0.facility AND NVL(p.latest_version,'Y') = 'Y' -- AND p.product_version = f0.product_version
        LEFT JOIN {node}F_EntityLotHist f4 ON f4.lot = f0.lot AND f4.operation = f0.operation AND f4.prevout_date = f0.prevout_date AND NVL(f4.history_deleted_flag,'N') = 'N' AND f4.unique_flag = 'Y'
        LEFT JOIN {node}F_EntityHist eh ON f4.entity = eh.entity AND f4.txn_date = eh.txn_date AND f4.facility = eh.facility AND f4.datasource = eh.datasource
        WHERE
        NVL(f0.history_deleted_flag,'N') = 'N'
        AND      f0.owner <> 'EMPTYFOUP'
        AND      f0.operation IN ({operation}) 
        AND      f0.out_date >= {duration_lot} 
        AND      p.prodgroup3 = 'BDR' 

        """

        mars_df = MarsReader().read(site, sql_query)
        # mars_conn = PyUber.connect(f'{site}_PROD_MARS')
        # mars_df = pd.read_sql(
        #     sql=sql_query,
        #     con=mars_conn,
        # )
        logger.info("MARS Data retrieved successfully")
        return mars_df
    except Exception as e:
        raise e


def oasys(site, operation, oasys_lot, monitorset):
    try:
        sql_query = f"""

        SELECT 
                v1.lot AS lot
                ,v3.monitor_set_name AS monitor_set_name
        FROM 
            P_SPC_Batch_Lot v1
            ,P_SPC_Batch v2
            ,P_SPC_Session v3
        WHERE 
                    v2.batch_id = v1.batch_id
        AND      v2.facility = v1.facility
        AND      v2.batch_id = v3.batch_id
        AND      v2.facility = v3.facility
        AND      v2.data_collection_ww = v3.data_collection_ww
        AND      v3.latest_flag = 'Y' 
        AND      v3.status <> 'I' 
        AND      v1.lot In ({oasys_lot}) 
        AND      v1.operation = ({operation}) 
        AND      v3.monitor_set_name = ({monitorset}) 
        """
        # mars_conn = PyUber.connect('KM_PROD_MARS')
        # oasys_df = pd.read_sql(
        #     sql=sql_query,
        #     con=mars_conn,
        # )
        # return oasys_df

        oasys_df = MarsReader().read(site, sql_query)
        # mars_conn = PyUber.connect(f'{site}_PROD_MARS')
        # oasys_df = pd.read_sql(
        #     sql=sql_query,
        #     con=mars_conn,
        # )
        logger.info("MARS Data retrieved successfully")
        return oasys_df
    except Exception as e:
        raise e


def get_history_records(hist_path):
    try:
        if os.path.exists(hist_path):
            hist_df = pd.read_csv(hist_path)
            logger.info(f"Successfully read history records from {hist_path}")
            return hist_df
        else:
            return pd.DataFrame(columns=["LOT"])
    except Exception as e:
        logger.info(f"Failed to read history records from {hist_path}")
        raise e


def get_facility_lot():
    try:
        # get MARS data
        mars_df = mars(site, operation, duration_lot, node)
        if mars_df.empty:
            logger.warning("Mars DataFrame is empty. Stopping execution.")
            sys.exit()

        oasys_lot = ", ".join(
            f"'{lot}'" for lot in (tuple(mars_df["LOT"].tolist()))
        )  # input to oasys

        # get OASYS data
        oasys_df = oasys(site, operation, oasys_lot, monitorset)
        if oasys_df.empty:
            logger.warning("EPT DataFrame is empty.")
        oasys_df = oasys_df.drop_duplicates(keep="first")

        # Filter out lot that is in oasys_df
        skip_lot = mars_df[~mars_df["LOT"].isin(oasys_df["LOT"])]

        # compare skip_lot to hist.csv. Exclude lot that is already in hist.csv
        hist_df = get_history_records(hist_path)
        data_df = skip_lot[~skip_lot["LOT"].isin(hist_df["LOT"])]

        return data_df

    except Exception as e:
        logger.error(f"ERROR in get_facility_lot: {e}")
        raise e


@email_on_exception()
def process_facility_attribute_update():
    lots_df = get_facility_lot()
    if lots_df is None or lots_df.empty:
        logger.info("No lots to process after filtering. Exiting function.")
        return
    from aed_updater import update_lot_attributes

    result = []
    try:
        total_lots = len(lots_df) - 1
        for index, row in lots_df.iterrows():
            facility = row["FACILITY"]
            lot = row["LOT"]
            # skip_value = row['OPERATION_DVI']
            skip_value = {skip_operation}
            skip = update_lot_attributes(facility, lot, attr_list, skip_value, logger)
        result = lots_df

    except Exception as e:
        logger.error(f"ERROR in process_facility_attribute_updates: {e}")
        raise e
    finally:
        result_df = pd.DataFrame(result)
        # result_y_df = result_df.loc[result_df['Lot_Skip_7666'] == 'Y']
        # save data to csv
        current_date = datetime.now().strftime("%Y%m%d%H%M%S")
        result_name = f"{site}_Lot_Skip.csv".replace(".csv", f"_{current_date}.csv")
        # result_y_path = os.path.join(os.getcwd(), result_name)
        result_path = os.path.join(os.getcwd(), result_name)
        result_df.to_csv(result_path, index=False)

        # save history record
        if os.path.exists(hist_path):
            hist_df = pd.read_csv(hist_path)
            hist_df = pd.concat([hist_df, result_df])
            # hist_df['OUT_DATE'] = pd.to_datetime(hist_df['OUT_DATE'])
            # hist_df = hist_df.loc[hist_df['OUT_DATE']
            #                       >= datetime.now() - timedelta(days=180)]
            # hist_df.sort_values('OUT_DATE', ascending=False, inplace=True)
            # hist_df.drop_duplicates(
            #     subset=['LOT', 'Lot_Skip_7666'], keep='first', inplace=True)
            hist_df.to_csv(hist_path, index=False)
        else:
            shutil.copy(result_path, hist_path)
        logger.info(f"File: {result_name} saved to history path: {hist_path}")


if __name__ == "__main__":
    node_mapping = {
        "CD": "A48_PROD_21",
        "PG": "A12_PROD_0",
        "KM": "A15_PROD_21",
        "VN": "A90_PROD_21",
        "CR": "A61_PROD_4",
    }

    args = sys.argv[1:]
    site = args[0]
    source_path = args[1]
    sys.argv = [sys.argv[0]]

    node = node_mapping.get(site)
    hist_dir = os.path.join(site, "HIST", "HIST.csv")
    hist_path = os.path.join(source_path, hist_dir)
    operation = "'1225'"  # epoxy operation
    duration_lot = "SYSDATE - 3/24"  # pull last 3 hours
    monitorset = "'V_PRE_NDLE_OFFSTS_SING'"  # setup monset
    attr_list = ("6266", "6666", "9666", "6727", "6728")
    skip_operation = "1237"  # csam operation

    # read config.txt
    with open(config_path, "r") as f:
        columns = next(f).strip().split(",")
        lines = f.readlines()
    data = []
    for line in lines:
        parts = line.split(",", 3)
        parts += [""] * (4 - len(parts))
        data.append(
            {
                columns[0]: parts[0].strip(),
                columns[1]: parts[1].strip(),
                columns[2]: parts[2].strip(),
                columns[3]: parts[3].strip(),
            }
        )
    config_df = pd.DataFrame(data, columns=columns)
    receiver = config_df.loc[config_df[columns[1]] == "dEmail", columns[2]].iloc[0]
    subject = config_df.loc[config_df[columns[1]] == "dSubject", columns[2]].iloc[0]
    file_path = Path(__file__).parent.parent / "logs" / f"EPX_CSAM_{site}.log"
    logger = set_log(file_path)
    process_facility_attribute_update()
