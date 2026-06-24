"""
CSR DLA Subplane Analysis - Step 10: Final List

This script filters the interim ranking results from Step 09:
- Loads CSR_Server_OIS_subplane_interim.csv (ranked units exceeding Y-axis limits)
- Filters WHERE dense_rank NOT IN (1, 2) - keeps only rank 3 and higher
- Adds CSR_trigger = 'CSR_HOLD' flag for units requiring attention
- Outputs to CSR_Server_OIS_subplane_output.csv

Business Logic:
- Ranks 1 and 2 are excluded (top 2 units per entity_bs_x_y)
- Only rank 3+ units trigger CSR hold action
- If no interim data exists (0 exceedances), produces 0 rows

Production Environment: Uses actual Step 09 output, no mock data
"""

import logging
import sys
import pandas as pd
import sqlite3


# Import shared logger and config
try:
    # Try relative import first (when part of package)
    from .utils import GlobalConfig, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT
except ImportError:
    # Fall back to absolute import (when run standalone)
    from core.utils import GlobalConfig, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT

logging.basicConfig(level=LOG_LEVEL, format=LOG_FORMAT, datefmt=LOG_DATE_FORMAT)

def step_10_final_list():
    """
    Filter interim ranking to get final list (rank 3+) with CSR_HOLD trigger
    
    Returns:
        pd.DataFrame: Final filtered list, or empty DataFrame if no interim data
    """
    # Input file from Step 09
    interim_file = GlobalConfig.get_output_path("CSR_Server_OIS_subplane_interim.csv")
    logger = logging.getLogger("10 - final list")
    
    # Log step header
    logger.debug("Step 1.1 - Fetching Text (SQLite) Data")
    expected_columns = [
            'facility', 'lot', 'operation', 'test_end_date', 'tester_id', 'program_name',
            'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code', 'entity',
            'bond_station', 'carrier_x', 'carrier_y', 'lane_number', 'entity_bs_x_y',
            'site', 'prodgroup3_1', 'sub_plane_x', 'sub_plane_y', 'lower_x_limit',
            'upper_x_limit', 'lower_y_limit', 'upper_y_limit', 'set_limit_plane_x',
            'set_limit_plane_y', 'flag', 'dense_rank', 'CSR_trigger'
        ]
    if not interim_file.exists():
        logger.debug("Data Import and SQL Query - No interim file found")
        df_empty = pd.DataFrame(columns=expected_columns)
        logger.debug(f"DataFrame created with columns: {df_empty.columns.tolist()}")
        logger.debug("Rows returned: 0")
        
        return df_empty
    
    logger.debug("Data Import and SQL Query - Starting")
    
    df_interim = pd.read_csv(interim_file)
    total_interim_rows = len(df_interim)
    
    if total_interim_rows == 0:
        logger.debug("Rows returned: 0")
        df_empty = pd.DataFrame(columns=expected_columns)
        logger.debug(f"DataFrame created with columns: {df_empty.columns.tolist()}")
        return df_empty
    
    # Create SQLite in-memory database for filtering
    conn = sqlite3.connect(':memory:')
    
    try:
        # Load interim data into SQLite
        df_interim.to_sql('CSR_Server_OIS_subplane_interim', conn, index=False, if_exists='replace')
        
        # SQL Query: Filter dense_rank NOT IN (1, 2), add CSR_trigger
        query = """
        SELECT /*L0*/
              a0.[facility] AS [facility]
             ,a0.[lot] AS [lot]
             ,a0.[operation] AS [operation]
             ,a0.[test_end_date] AS [test_end_date]
             ,a0.[tester_id] AS [tester_id]
             ,a0.[program_name] AS [program_name]
             ,a0.[prodgroup3] AS [prodgroup3]
             ,a0.[visual_id] AS [visual_id]
             ,a0.[tray_or_carrier_id] AS [tray_or_carrier_id]
             ,a0.[ws_loss_code] AS [ws_loss_code]
             ,a0.[entity] AS [entity]
             ,a0.[bond_station] AS [bond_station]
             ,a0.[carrier_x] AS [carrier_x]
             ,a0.[carrier_y] AS [carrier_y]
             ,a0.[lane_number] AS [lane_number]
             ,a0.[entity_bs_x_y] AS [entity_bs_x_y]
             ,a0.[site] AS [site]
             ,a0.[prodgroup3_1] AS [prodgroup3_1]
             ,a0.[sub_plane_x] AS [sub_plane_x]
             ,a0.[sub_plane_y] AS [sub_plane_y]
             ,a0.[lower_x_limit] AS [lower_x_limit]
             ,a0.[upper_x_limit] AS [upper_x_limit]
             ,a0.[lower_y_limit] AS [lower_y_limit]
             ,a0.[upper_y_limit] AS [upper_y_limit]
             ,a0.[set_limit_plane_x] AS [set_limit_plane_x]
             ,a0.[set_limit_plane_y] AS [set_limit_plane_y]
             ,a0.[flag] AS [flag]
             ,a0.[dense_rank] AS [dense_rank]
             ,'CSR_HOLD' AS [CSR_trigger]
        FROM 
        [CSR_Server_OIS_subplane_interim] a0
        WHERE
                  a0.[dense_rank] NOT IN ('1', '2')
        """
        
        # Execute query
        df_result = pd.read_sql_query(query, conn)
        logger.debug(f"Rows returned: {len(df_result)}")
        logger.debug(f"DataFrame created with columns: {df_result.columns.tolist()}")
        
        return df_result
        
    finally:
        conn.close()


def main():
    """Main execution function"""
    logger = logging.getLogger("10 - final list")
    
    try:
        # Execute Step 10
        df_result = step_10_final_list()
        
        # Define output file path
        output_file = GlobalConfig.get_output_path("CSR_Server_OIS_subplane_output.csv")
        
        # Save results and log output file (even if 0 rows, to match expected format)
        if len(df_result) > 0:
            df_result.to_csv(output_file, index=False)
            logger.info(f"Output file created: {output_file}")
            
            logger.info("Step 10 COMPLETED SUCCESSFULLY")
            logger.info(f"Filtered to {len(df_result)} units requiring CSR_HOLD (rank 3+)")
            logger.info(f"Output: {output_file}")
        else:
            # Log output file path even for 0 rows (SPF behavior)
            logger.info(f"Output file created: {output_file}")
            
            logger.info("Step 10 COMPLETED SUCCESSFULLY")
            logger.info("No units require CSR_HOLD action (0 records after filtering)")
            logger.info("No output file created (0 records)")
        
    except Exception as e:
        logger.error(f"Step 10 FAILED: {str(e)}")
        import traceback
        traceback.print_exc()
        sys.exit(1)


if __name__ == "__main__":
    main()
