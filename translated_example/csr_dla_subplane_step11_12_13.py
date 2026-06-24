"""
CSR DLA Subplane Analysis - Steps 11, 12, 13: Row Check & STIM Lot at Cure
===========================================================================

Step 11: Row in File
- Count rows in CSR_Server_OIS_subplane_output.csv
- Store count as variable "Flag"

Step 12: Conditional Check
- If Flag > 0: Proceed to Step 13
- If Flag <= 0: Skip Step 13 (no units require CSR hold)

Step 13: STIM Lot at Cure
- Filter lots from Step 10 output
- Exclude lots in HIST.csv (historical exclusion list)
- Query MARS for current lot status
- Add Lot_MVIN_CURE flag based on current operation
- Filter to lots that have moved into cure operation
- Output final data to Data.csv

This script combines three steps with conditional execution.

Production Environment: Uses actual Step 10 output, no mock data
"""

import logging
import sys
import pandas as pd
import sqlite3
from typing import List, Optional

# Import shared logger and config
try:
    # Try relative import first (when part of package)
    from .utils import GlobalConfig, DataReader, ProductionDataReader, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT
except ImportError:
    # Fall back to absolute import (when run standalone)
    from core.utils import GlobalConfig, DataReader, ProductionDataReader, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT

logging.basicConfig(level=LOG_LEVEL, format=LOG_FORMAT, datefmt=LOG_DATE_FORMAT)

# ==============================================================================
# Step 11: Row Count in File
# ==============================================================================

def step_11_count_rows() -> int:
    logger = logging.getLogger("11 - row check")
    output_file = GlobalConfig.get_output_path("CSR_Server_OIS_subplane_output.csv")
    
    if not output_file.exists():
        # File doesn't exist means 0 rows
        logger.info("File not found, 0 rows returned")
        return 0
    
    # Read CSV and count rows
    df = pd.read_csv(output_file)
    row_count = len(df)
    logger.info(f"Rows returned: {row_count}")
    
    return row_count


# ==============================================================================
# Step 12: Conditional Check
# ==============================================================================

def step_12_check_condition(flag: int) -> bool:
    """
    Step 12: Check if Flag > 0
    
    Args:
        flag: Row count from Step 11
    
    Returns:
        bool: True if Flag > 0 (proceed to Step 13), False otherwise
    """
    # Note: Step 12 has no separate log output, returns boolean only
    logger = logging.getLogger("12 - condition check")
    logger.info(f"Flag value: {flag}")
    retval = flag > 0
    logger.info(f"Condition (Flag > 0): {retval}")
    return retval


# ==============================================================================
# Step 13: STIM Lot at Cure
# ==============================================================================

class Step13_StimLotAtCure:
    """
    Step 13: Identify lots at cure operation with subplane exceedances
    
    This step:
    1. Loads CSR_Server_OIS_subplane_output.csv (from Step 10)
    2. Filters out lots in HIST.csv (historical exclusion list)
    3. Queries MARS for current lot status
    4. Adds Lot_MVIN_CURE flag based on current operation
    5. Filters to lots that have moved into cure
    """
    
    def __init__(self, data_reader: Optional[DataReader] = None):
        self.logger = logging.getLogger("13 - stim lot at cure")
        self.data_reader = data_reader or ProductionDataReader()  # Use production data reader by default
    
    def step_1_fetch_sqlite_data(self) -> pd.DataFrame:
        """
        Step 1.1: Load CSR_Server_OIS_subplane_output.csv and filter by HIST.csv
        
        Returns:
            pd.DataFrame: Filtered lot list
        """
        self.logger.debug("Step 1.1: Fetching Text (SQLite) Data")
         # Blank line
        self.logger.debug("Data Import and SQL Query - Starting")
         # Blank line
        
        # Load output file from Step 10
        output_file = GlobalConfig.get_output_path("CSR_Server_OIS_subplane_output.csv")
        
        if not output_file.exists():
            self.logger.debug("Rows returned: 0")
            self.logger.debug("Data Import and SQL Query - Completed")
            return pd.DataFrame()
        
        df_output = pd.read_csv(output_file)
        
        # Check if HIST.csv exists (historical lots to exclude)
        hist_file = GlobalConfig.get_output_path(GlobalConfig.HISTORICAL_RECORD_FILENAME)
        
        if hist_file.exists():
            # Load historical lots to exclude
            df_hist = pd.read_csv(hist_file)
            
            # Filter out lots that are in history (handle both uppercase and lowercase)
            lot_col = None
            if 'lot' in df_hist.columns:
                lot_col = 'lot'
            elif 'LOT' in df_hist.columns:
                lot_col = 'LOT'
            
            if lot_col:
                hist_lots = df_hist[lot_col].unique().tolist()
                # Remove 'DUMMY' entries if present
                hist_lots = [lot for lot in hist_lots if lot != 'DUMMY']
                df_filtered = df_output[~df_output['lot'].isin(hist_lots)]
            else:
                # If HIST.csv has different structure, use all data
                df_filtered = df_output
        else:
            # No history file, use all data
            df_filtered = df_output
        
        # Select required columns for Step 13
        columns_needed = [
            'facility', 'lot', 'prodgroup3', 'operation',
            'entity', 'bond_station', 'carrier_x', 'carrier_y',
            'visual_id', 'sub_plane_x', 'sub_plane_y',
            'lower_x_limit', 'upper_x_limit', 'lower_y_limit', 'upper_y_limit'
        ]
        
        # Rename 'operation' to 'DLA_operation' to match expected output
        missing = [c for c in columns_needed if c not in df_filtered.columns]
        if missing or 'operation' not in df_filtered.columns:
            # Required columns missing — return empty result with correct schema
            df_result = pd.DataFrame(columns=[c if c != 'operation' else 'DLA_operation' for c in columns_needed])
        else:
            df_result = df_filtered[columns_needed].copy()
            df_result.rename(columns={'operation': 'DLA_operation'}, inplace=True)
        
        self.logger.debug(f"Rows returned: {len(df_result)}")
        self.logger.debug("Data Import and SQL Query - Completed")
        
        return df_result
    
    def step_2_fetch_mars_wip_status(self, lot_list: List[str]) -> pd.DataFrame:
        """
        Step 1.2-a1: Query MARS for current lot status
        
        Args:
            lot_list: List of lot IDs to query
        
        Returns:
            pd.DataFrame: Current WIP status for each lot
        """
        if len(lot_list) == 0:
            # Show empty result message
            self.logger.debug("=" * 80)
            self.logger.debug(r"No rows found in .\temp_sql_output.tab")
            self.logger.debug("An empty item list will be returned...")
            self.logger.debug("=" * 80)
             # Blank line
             # Blank line
            
            # Log MARS query step header
            site_info = f"({GlobalConfig.SITE}.[A15_PROD_21.].MARS)"
            self.logger.debug(
                f"Step 1.2-a1: Fetching MARS Data {site_info}")
            self.logger.debug("Connected to MARS")
            self.logger.debug("Rows returned: 0")
            self.logger.debug("MARS Data Fetch - Completed")
            
            return pd.DataFrame(columns=[
                'lot_1', 'Current_operation', 'movedin', 'onrework', 
                'onhold', 'quantity', 'route'
            ])
        
        # Build lot list for SQL IN clause
        lot_in_clause = "', '".join(lot_list)
        lot_in_clause = f"'{lot_in_clause}'"
        
        # MARS SQL query for WIP status
        sql_query = f"""
SELECT 
          f0.lot AS lot_1
         ,f0.operation AS Current_operation
         ,f0.movedin AS movedin
         ,f0.onrework AS onrework
         ,f0.onhold AS onhold
         ,f0.qty1 AS quantity
         ,f0.route AS route
FROM 
@[]@.F_Lot f0
WHERE f0.owner <> 'EMPTYFOUP'
 AND      f0.terminated = 'N' 
 AND      f0.qty1 > 0 
 AND      f0.src_erase_date Is Null  
 AND      f0.lot IN ({lot_in_clause})
"""
        
        # Log step header
        site_info = f"({GlobalConfig.SITE}.[A15_PROD_21.].MARS)"
        self.logger.debug(
            f"Step 1.2-a1: Fetching MARS Data {site_info}"
        )
        try:
            df = self.data_reader.read_mars(GlobalConfig.SITE, sql_query)
            
            # Convert column names to match expected output
            df.columns = df.columns.str.lower()
            
            # Rename to match expected names
            column_mapping = {
                'lot_1': 'lot_1',
                'current_operation': 'Current_operation',
                'movedin': 'movedin',
                'onrework': 'onrework',
                'onhold': 'onhold',
                'quantity': 'quantity',
                'route': 'route'
            }
            df = df.rename(columns=column_mapping)
            
            self.logger.debug(f"Rows returned: {len(df)}")
            self.logger.debug("MARS Data Fetch - Completed")
            
            return df
        
        except Exception as e:
            self.logger.error(f"MARS query failed: {str(e)}")
            raise
    
    def step_3_join_and_filter(self, df_output: pd.DataFrame, df_mars: pd.DataFrame) -> pd.DataFrame:
        """
        Step 3: Join SQLite data with MARS data and filter by Lot_MVIN_CURE
        
        Args:
            df_output: Output data from Step 10 (filtered)
            df_mars: MARS WIP status data
        
        Returns:
            pd.DataFrame: Final result with Lot_MVIN_CURE = 'Y'
        """
        self.logger.debug("Getting Data Using SQLite")
        self.logger.debug("Data Import and SQL Query - Starting")
         # Blank line
        
        # Define all expected columns for consistent output
        expected_columns = [
            'facility', 'lot', 'prodgroup3', 'DLA_operation', 'lot_1',
            'Current_operation', 'movedin', 'onrework', 'onhold', 'route',
            'quantity', 'Lot_MVIN_CURE', 'entity', 'bond_station',
            'carrier_x', 'carrier_y', 'visual_id', 'sub_plane_x',
            'sub_plane_y', 'lower_x_limit', 'upper_x_limit',
            'lower_y_limit', 'upper_y_limit'
        ]
        
        if len(df_output) == 0 or len(df_mars) == 0:
            self.logger.debug("Rows returned: 0")
            
            # Create empty DataFrame with all expected columns
            df_empty = pd.DataFrame(columns=expected_columns)
            self.logger.debug(f"Rows returned: {len(df_empty)}")
            
            return df_empty
        
        # Create SQLite in-memory database for join
        conn = sqlite3.connect(':memory:')
        
        try:
            # Load data into SQLite
            df_output.to_sql('temp_output_data', conn, index=False, if_exists='replace')
            df_mars.to_sql('temp_mars_wip', conn, index=False, if_exists='replace')
            
            # Create index for performance
            conn.execute("CREATE INDEX IF NOT EXISTS IdxA1 ON temp_mars_wip (lot_1)")
            
            # SQL query with JOIN and CASE statement for Lot_MVIN_CURE
            query = """
            SELECT DISTINCT
                  sql.[facility] AS [facility]
                 ,sql.[lot] AS [lot]
                 ,sql.[prodgroup3] AS [prodgroup3]
                 ,sql.[DLA_operation] AS [DLA_operation]
                 ,a1.[lot_1] AS [lot_1]
                 ,a1.[Current_operation] AS [Current_operation]
                 ,a1.[movedin] AS [movedin]
                 ,a1.[onrework] AS [onrework]
                 ,a1.[onhold] AS [onhold]
                 ,a1.[route] AS [route]
                 ,a1.[quantity] AS [quantity]
                 ,CASE 
                    WHEN a1.[Current_operation] IN ('1266') THEN 'N'
                    WHEN a1.[Current_operation] IN ('1501') THEN 'N'
                    WHEN a1.[Current_operation] IN ('1366') THEN 'N'
                    WHEN a1.[Current_operation] IN ('1265') THEN 'N'
                    WHEN a1.[Current_operation] IN ('1264') THEN 'N'
                    ELSE 'Y'
                  END AS [Lot_MVIN_CURE]
                 ,sql.[entity] AS [entity]
                 ,sql.[bond_station] AS [bond_station]
                 ,sql.[carrier_x] AS [carrier_x]
                 ,sql.[carrier_y] AS [carrier_y]
                 ,sql.[visual_id] AS [visual_id]
                 ,sql.[sub_plane_x] AS [sub_plane_x]
                 ,sql.[sub_plane_y] AS [sub_plane_y]
                 ,sql.[lower_x_limit] AS [lower_x_limit]
                 ,sql.[upper_x_limit] AS [upper_x_limit]
                 ,sql.[lower_y_limit] AS [lower_y_limit]
                 ,sql.[upper_y_limit] AS [upper_y_limit]
            FROM 
                temp_output_data sql
            LEFT OUTER JOIN temp_mars_wip a1
                ON sql.[lot] = a1.[lot_1]
            WHERE
                CASE 
                    WHEN a1.[Current_operation] IN ('1266') THEN 'N'
                    WHEN a1.[Current_operation] IN ('1501') THEN 'N'
                    WHEN a1.[Current_operation] IN ('1366') THEN 'N'
                    WHEN a1.[Current_operation] IN ('1265') THEN 'N'
                    WHEN a1.[Current_operation] IN ('1264') THEN 'N'
                    ELSE 'Y'
                END = 'Y'
            """
            
            # Execute query
            df_result = pd.read_sql_query(query, conn)
            
            self.logger.debug(f"Rows returned: {len(df_result)}")
            self.logger.debug("Join and Filter - Completed")
            
            return df_result
        
        finally:
            conn.close()
    
    def execute(self) -> pd.DataFrame:
        """
        Execute all steps of Step 13
        
        Returns:
            pd.DataFrame: Final result
        """
        # Step 1: Load and filter SQLite data
        df_output = self.step_1_fetch_sqlite_data()
        
        if len(df_output) == 0:
            # Continue to MARS query even with 0 rows (to match SPF behavior)
            lot_list = []
        else:
            # Get unique lot list for MARS query
            lot_list = df_output['lot'].unique().tolist()
        
        # Step 2: Fetch MARS WIP status
        df_mars = self.step_2_fetch_mars_wip_status(lot_list)
        
        # Step 3: Join and filter by Lot_MVIN_CURE
        df_result = self.step_3_join_and_filter(df_output, df_mars)
        
        return df_result


# ==============================================================================
# Main Execution
# ==============================================================================

def main(data_reader: Optional[DataReader] = None):
    """Main execution function for Steps 11-12-13"""
    step13 = Step13_StimLotAtCure(ProductionDataReader() if data_reader is None else data_reader)
    try:
        # Step 11: Count rows in output file
        flag = step_11_count_rows()
        should_proceed = step_12_check_condition(flag)
        
        if not should_proceed:
            step13.logger.info("Step 13: Skipped (no input data)")
            return
        
        # Condition TRUE - Proceed to Step 13
        
        step13.logger.info("\n[Step 12] Condition TRUE - Proceeding to Step 13")
        step13.logger.info("\n[Step 13] Fetching STIM lot at cure data...")
        
        df_result = step13.execute()
        
        # Save results
        output_file = GlobalConfig.get_output_path(GlobalConfig.FINAL_OUTPUT_FILENAME)
        
        if len(df_result) > 0:
            df_result.to_csv(output_file, index=False)
            step13.logger.info(f"Output file: {output_file}")
            
            step13.logger.info(f"Step 13: Retrieved {len(df_result)} STIM lots at cure")
            step13.logger.info(f"Output: {output_file}")
        else:
            step13.logger.info(f"Step 13: 0 STIM lots at cure (all filtered out)")
            step13.logger.info("No output file created (0 records)")
        
    except Exception as e:
        step13.logger.error(f"\nERROR in Steps 11-12-13: {str(e)}")
        import traceback
        traceback.print_exc()
        sys.exit(1)


if __name__ == "__main__":
    main()
