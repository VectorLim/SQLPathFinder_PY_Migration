"""
CSR DLA Subplane Analysis - Steps 06-08: Row Check & Raw Subplane Data
========================================================================
Translates Steps 06-08 from VG2 workflow to Python using DataSyncX.

Step 06: Count rows in lot list CSV
Step 07: Check if rows > 0, proceed if true, exit if false
Step 08: Query ARIES for subplane metrology and bonding data, join results

Original: CSR_DLA_subplane_Rev7 VG2
SQL Source: 08 - Raw Subplane.txt
"""

from datetime import datetime
import logging
from pathlib import Path
from typing import List, Optional
import pandas as pd
import sqlite3

# Import shared logger and config
try:
    # Try relative import first (when part of package)
    from .utils import GlobalConfig, save_df_as_csv, DataReader, ProductionDataReader, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT
except ImportError:
    # Fall back to absolute import (when run standalone)
    from core.utils import GlobalConfig, save_df_as_csv, DataReader, ProductionDataReader, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT

logging.basicConfig(level=logging.DEBUG, format=LOG_FORMAT, datefmt=LOG_DATE_FORMAT)

# ==============================================================================
# Helper Functions
# ==============================================================================

def read_csv_column(csv_path: Path, column_name: str) -> List[str]:
    """Read a column from CSV and return unique values"""
    df = pd.read_csv(csv_path)
    
    if column_name not in df.columns:
        raise ValueError(f"Column '{column_name}' not found in {csv_path}")
    
    values = df[column_name].dropna().unique().tolist()
    return values


def format_in_clause(values: List[str]) -> str:
    """Format a list of values for SQL IN clause"""
    return ",".join([f"'{v}'" for v in values])


# ==============================================================================
# Step 06: Count Rows in File
# ==============================================================================

def step_06_count_rows(csv_path: Path) -> int:
    """
    Step 06: Count Rows in a File
    
    Counts the number of data rows in the lot list CSV file.
    This mimics SQLPathFinder's ROWS-IN-FILE utility.
    
    Args:
        csv_path: Path to the CSV file
    
    Returns:
        Number of rows (excluding header)
    """
    logger = logging.getLogger("Step06 - Count Rows in File")
    
    try:
        if not csv_path.exists():
            raise FileNotFoundError(f"File not found: {csv_path}")
        
        # Read CSV and count rows
        df = pd.read_csv(csv_path)
        row_count = len(df)
        
        logger.info(f'Counting rows in "{csv_path.name}"')
        logger.info(f"Variable 'Lots' = {row_count}\n")
        
        return row_count
    
    except Exception as e:
        logger.error(f"Failed to count rows: {str(e)}")
        raise


# ==============================================================================
# Step 07: Conditional Logic (IF-THEN)
# ==============================================================================

def step_07_check_condition(lots: int) -> bool:
    """
    Step 07: Apply Conditional Logic
    
    Checks if lots > 0 (GT = Greater Than).
    If true, continue to next step.
    If false, end execution.
    
    Args:
        lots: Number of lots from Step 06
    
    Returns:
        True if should continue, False if should stop
    """
    logger = logging.getLogger("Step07 - Apply Conditional Logic")
        
    try:
        condition = lots > 0
        
        logger.info(f"Condition: Lots ({lots}) > 0")
        logger.info(f"Result: {condition}\n")
        
        if condition:
            logger.info("Condition TRUE - Proceeding to next step\n")
            return True
        else:
            logger.info("Condition FALSE - No lots to process")
            logger.info("Stopping execution\n")
            return False
    
    except Exception as e:
        logger.error(f"Conditional check failed: {str(e)}")
        raise


# ==============================================================================
# Step 08: Raw Subplane Data
# ==============================================================================

class Step08_RawSubplane:
    """
    Step 08: Fetch raw subplane metrology and bonding data from ARIES
    
    This step:
    1. Query ARIES AT_Metrology for subplane angle measurements
       - SUBPLANEANGLEX and SUBPLANEANGLEY test names
       - Pivot these into columns
    2. Query ARIES AT_TDX_DIA for bonding station data
    3. Join the two datasets using SQLite
    4. Create Entity_BS_X_Y composite key
    
    Output columns:
    - facility, lot, operation, test_end_date, tester_id, program_name
    - prodgroup3, visual_id, tray_or_carrier_id, ws_loss_code
    - entity, bond_station, carrier_x, carrier_y, lane_number
    - SUBPLANEANGLEX, SUBPLANEANGLEY (pivoted from test results)
    - Entity_BS_X_Y (composite key)
    """
    
    def __init__(self, data_reader: Optional[DataReader] = None):
        self.logger = logging.getLogger("Step08 - Raw Subplane Data")
        self.data_reader = data_reader or ProductionDataReader()
        
    
    def step_1_fetch_aries_metrology(self, lot_list_path: Path) -> pd.DataFrame:
        """Step 1.1-a0: Fetching ARIES Metrology Data"""
        
        # Read lots and operations from lot list (CSV has uppercase columns)
        lot_list_df = pd.read_csv(lot_list_path)
        
        # Handle case-insensitive column names
        col_map = {col.upper(): col for col in lot_list_df.columns}
        lot_col = col_map.get('LOT')
        op_col = col_map.get('OPERATION')
        
        if not lot_col or not op_col:
            raise ValueError(f"Required columns LOT/OPERATION not found in {lot_list_path}")
        
        lots = lot_list_df[lot_col].dropna().unique().tolist()
        operations = lot_list_df[op_col].dropna().unique().tolist()
        
        if not lots:
            raise ValueError("No lots found in lot list")
        
        lots_in = format_in_clause(lots)
        operations_in = format_in_clause(operations)
        
        # Build ARIES query for metrology data
        sql_query = f"""
SELECT 
    ats.facility AS facility
    ,ats.lot AS lot
    ,ats.operation AS operation
    ,ats.test_end_date_time AS test_end_date
    ,ats.tester_id AS tester_id
    ,ats.program_name AS program_name
    ,mp.prodgroup3 AS prodgroup3
    ,di.visual_id AS visual_id
    ,dt.testing_session_tray_id AS tray_or_carrier_id
    ,t.test_name AS test_name
    ,dt.ws_loss_code AS ws_loss_code
    ,dt.carrier_x AS carrier_x
    ,dt.carrier_y AS carrier_y
    ,dt.lane_number AS lane_number
    ,CASE WHEN ctr.string_value IS NULL THEN TO_CHAR(ctr.numeric_result) ELSE ctr.string_value END AS Sub_plane
FROM 
A_Testing_Session ats
LEFT JOIN A_MARS_Lot ml ON ats.lot = ml.lot
LEFT JOIN A_MARS_Product mp ON ml.product = mp.product AND ml.mars_schema = mp.mars_schema AND ats.facility = mp.facility
INNER JOIN A_All_Component_Testing_Result ctr 
    ON ctr.lao_start_ww = ats.lao_start_ww 
    AND ctr.ts_id = ats.ts_id 
    AND (ctr.numeric_result IS NOT NULL OR ctr.string_value IS NOT NULL)
INNER JOIN A_Test t ON t.t_id = ctr.t_id
INNER JOIN A_Device_Testing dt 
    ON dt.lao_start_ww = ats.lao_start_ww 
    AND dt.ts_id = ats.ts_id
    AND dt.lao_start_ww = ctr.lao_start_ww 
    AND dt.ts_id = ctr.ts_id 
    AND dt.dt_id = ctr.dt_id
LEFT JOIN A_Device_Item di ON di.di_id = dt.di_id
WHERE 
    ats.data_domain = 'METROLOGY'
    AND ats.lot IN ({lots_in})
    AND ats.operation IN ({operations_in})
    AND ats.tester_id LIKE 'OIS%'
    AND t.test_name IN ('SUBPLANEANGLEX', 'SUBPLANEANGLEY')
    AND dt.ws_loss_code IS NULL
"""
        
        # Log step header
        site_info = f"({GlobalConfig.SITE}.ARIES)"
        self.logger.debug(f"Step 1.1-a0: Fetching ARIES Data {site_info}")
        
        try:
            # Use the injected data_reader (production or mock)
            df = self.data_reader.read_aries(GlobalConfig.SITE, sql_query)
            
            # Convert column names to lowercase for consistency
            df.columns = df.columns.str.lower()
            
            self.logger.debug(f"{len(df)} rows returned from ARIES metrology query")
            self.logger.debug(f"     {datetime.now().strftime('%d-%b-%Y %H:%M:%S')}\n")
            
            # Pivot operation
            self.logger.debug(f"Pivoting (Python)... {datetime.now().strftime('%d-%b-%Y %H:%M:%S')}\n")
            
            # Check if we have the required columns
            if df.empty:
                raise ValueError("No data returned from ARIES metrology query")
            
            # Group by unique device identifiers and get max test_end_date and sub_plane per test_name
            # First, create grouping columns
            group_cols = ['facility', 'lot', 'operation', 'tester_id', 'program_name',
                         'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code',
                         'carrier_x', 'carrier_y', 'lane_number', 'test_name']
            
            # Aggregate to get max values
            agg_df = df.groupby(group_cols, dropna=False).agg({
                'test_end_date': 'max',
                'sub_plane': 'max'
            }).reset_index()
            
            # Now pivot test_name into columns
            pivot_df = agg_df.pivot(
                index=['facility', 'lot', 'operation', 'tester_id', 'program_name',
                      'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code',
                      'carrier_x', 'carrier_y', 'lane_number'],
                columns='test_name',
                values='sub_plane'
            ).reset_index()
            
            # Get test_end_date (take max across all test_names)
            test_date_df = agg_df.groupby(
                ['facility', 'lot', 'operation', 'tester_id', 'program_name',
                 'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code',
                 'carrier_x', 'carrier_y', 'lane_number'],
                dropna=False
            )['test_end_date'].max().reset_index()
            
            # Merge to add test_end_date
            pivot_df = pivot_df.merge(test_date_df, on=[
                'facility', 'lot', 'operation', 'tester_id', 'program_name',
                'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code',
                'carrier_x', 'carrier_y', 'lane_number'
            ])
            
            # Format test_end_date as string
            pivot_df['test_end_date'] = pd.to_datetime(pivot_df['test_end_date']).dt.strftime('%Y-%m-%d %H:%M:%S')
            
            # Reorder columns - convert pivot column names to uppercase
            pivot_df.columns = [col.upper() if col in ['subplaneanglex', 'subplaneangley'] else col for col in pivot_df.columns]
            
            base_cols = ['facility', 'lot', 'operation', 'test_end_date', 'tester_id', 'program_name',
                        'prodgroup3', 'visual_id', 'tray_or_carrier_id', 'ws_loss_code',
                        'carrier_x', 'carrier_y', 'lane_number']
            pivot_cols = [col for col in pivot_df.columns if col in ['SUBPLANEANGLEX', 'SUBPLANEANGLEY']]
            pivot_df = pivot_df[base_cols + pivot_cols]
            
            # Convert carrier_x and carrier_y to numeric for consistency
            pivot_df['carrier_x'] = pd.to_numeric(pivot_df['carrier_x'], errors='coerce')
            pivot_df['carrier_y'] = pd.to_numeric(pivot_df['carrier_y'], errors='coerce')
            pivot_df['lane_number'] = pd.to_numeric(pivot_df['lane_number'], errors='coerce')
            
            self.logger.debug(f"Rows returned {len(pivot_df)}")
            
            return pivot_df
        
        except Exception as e:
            self.logger.error(f"ARIES metrology query failed: {str(e)}")
            raise
    
    def step_2_fetch_aries_bonding(self, metrology_df: pd.DataFrame) -> pd.DataFrame:
        """Step 1.1-a2: Fetching ARIES Bonding Data"""
        
        # Get unique lots and operations from metrology data
        lots = metrology_df['lot'].dropna().unique().tolist()
        operations = metrology_df['operation'].dropna().unique().tolist()
        
        if not lots:
            raise ValueError("No lots found in metrology data")
        
        if not operations:
            raise ValueError("No operations found in metrology data")
        
        lots_in = format_in_clause(lots)
        operations_in = format_in_clause(operations)
        
        # Build ARIES query for bonding data
        sql_query = f"""
SELECT DISTINCT 
    z0.primary_entity AS entity
    ,z2.bonding_station AS bond_station
    ,z0.lot AS lot_2
    ,z8.visual_id AS visual_id_1
FROM 
ARIES_Views.AV_dia_session z0
LEFT JOIN ARIES_Views.AV_dia_media_testing z2 
    ON z2.lao_start_ww = z0.lao_start_ww 
    AND z2.obj_s_id = z0.obj_s_id
INNER JOIN ARIES_Views.AV_dia_Unit_Testing z8 
    ON z8.lao_start_ww = z2.lao_start_ww 
    AND z8.obj_s_id = z2.obj_s_id 
    AND z8.obj_mt_id = z2.obj_mt_id
WHERE
    z0.lot IN ({lots_in})
    AND z0.tool_entity LIKE 'TGB%'
    AND z0.operation IN ({operations_in})
"""
        
        # Log step header
        site_info = f"({GlobalConfig.SITE}.ARIES)"
        self.logger.debug(f"Step 1.1-a2: Fetching ARIES Data {site_info}")
        
        try:
            aries_reader = self.data_reader
            df = aries_reader.read_aries(GlobalConfig.SITE, sql_query)
            
            # Convert column names to lowercase for consistency
            df.columns = df.columns.str.lower()
            
            self.logger.debug(f"{len(df)} rows returned from ARIES bonding query")
            
            return df
        
        except Exception as e:
            self.logger.error(f"ARIES bonding query failed: {str(e)}")
            raise
    
    def step_3_join_data(self, metrology_df: pd.DataFrame, bonding_df: pd.DataFrame) -> pd.DataFrame:
        """Join metrology and bonding data using SQLite"""
        
        self.logger.debug(f"Getting Data Using SQLite... {datetime.now().strftime('%d-%b-%Y %H:%M:%S')}\n")
        
        try:
            # Create in-memory SQLite database
            conn = sqlite3.connect(':memory:')
            
            # Write dataframes to SQLite
            metrology_df.to_sql('yeuchuan_a0_6805', conn, index=False, if_exists='replace')
            bonding_df.to_sql('yeuchuan_a2_6805', conn, index=False, if_exists='replace')
            
            # Create index
            conn.execute('CREATE INDEX IF NOT EXISTS IdxA2 ON yeuchuan_a2_6805 (visual_id_1)')
            
            # Join query - use lowercase column names, then uppercase the output
            join_query = """
SELECT DISTINCT 
    a0.facility AS facility
    ,a0.lot AS lot
    ,a0.operation AS operation
    ,a0.test_end_date AS test_end_date
    ,a0.tester_id AS tester_id
    ,a0.program_name AS program_name
    ,a0.prodgroup3 AS prodgroup3
    ,a0.visual_id AS visual_id
    ,a0.tray_or_carrier_id AS tray_or_carrier_id
    ,a0.ws_loss_code AS ws_loss_code
    ,a2.entity AS entity
    ,a2.bond_station AS bond_station
    ,a0.carrier_x AS carrier_x
    ,a0.carrier_y AS carrier_y
    ,a0.lane_number AS lane_number
    ,a0.SUBPLANEANGLEX AS SUBPLANEANGLEX
    ,a0.SUBPLANEANGLEY AS SUBPLANEANGLEY
    ,a2.entity || '_' || a2.bond_station || '_' || CAST(a0.carrier_x AS TEXT) || '_' || CAST(a0.carrier_y AS TEXT) AS Entity_BS_X_Y
FROM 
    yeuchuan_a0_6805 a0
LEFT OUTER JOIN yeuchuan_a2_6805 a2
    ON a0.visual_id = a2.visual_id_1
"""
            
            # Execute join
            result_df = pd.read_sql_query(join_query, conn)
            conn.close()
            
            self.logger.debug(f"Rows returned: {len(result_df)}")
            
            return result_df
        
        except Exception as e:
            self.logger.error(f"SQLite join failed: {str(e)}")
            raise
    
    def run(self, lot_list_path: Path) -> pd.DataFrame:
        """Execute Step 08 - Raw Subplane Data"""
        try:
            # Step 1: Fetch ARIES metrology data
            metrology_df = self.step_1_fetch_aries_metrology(lot_list_path)
            
            # Step 2: Fetch ARIES bonding data
            bonding_df = self.step_2_fetch_aries_bonding(metrology_df)
            
            # Step 3: Join data
            result_df = self.step_3_join_data(metrology_df, bonding_df)
            
            return result_df
        
        except Exception as e:
            self.logger.error(f"Step 08 FAILED: {str(e)}")
            raise


# ==============================================================================
# Main Execution
# ==============================================================================

def main(data_reader: Optional[DataReader] = None) -> pd.DataFrame | None:
    """Main execution function for Steps 06-08"""
    
    # Initialize logger
    step08 = Step08_RawSubplane(ProductionDataReader() if data_reader is None else data_reader)
    
    try:
        # Input: Lot list from Step 05
        lot_list_path = GlobalConfig.get_output_path(GlobalConfig.CSR_SERVER_OIS_SUBPLANE_LOTLIST_FILENAME)
        
        if not lot_list_path.exists():
            raise FileNotFoundError(
                f"Lot list not found: {lot_list_path}\n"
                f"Please run Step 05 first."
            )
        
        # Step 06: Count rows
        lots = step_06_count_rows(lot_list_path)
        
        # Step 07: Check condition
        should_continue = step_07_check_condition(lots)
        
        if not should_continue:
            step08.logger.info("Steps 06-07 COMPLETED - No lots to process")
            return None
        
        # Step 08: Fetch and join raw subplane data
        
        
        raw_df = step08.run(lot_list_path)
        
        # Save output
        output_path = save_df_as_csv(
            raw_df,
            "CSR_Server_OIS_subplane.csv",
            quote_all=True
        )
        
        step08.logger.info(f"Steps 06-08 COMPLETED SUCCESSFULLY")
        step08.logger.info(f"Processed {lots} lots")
        step08.logger.info(raw_df.head(10).to_string())
        step08.logger.info(f"Retrieved {len(raw_df)} raw subplane measurements")
        step08.logger.info(f"Output: {output_path}")
        
        return raw_df
    
    except Exception as e:
        step08.logger.error(f"Steps 06-08 FAILED: {str(e)}")
        
        raise


if __name__ == "__main__":
    raw_subplane_df = main()
    if raw_subplane_df is not None:
        print(f"\nRaw Subplane Data retrieved: {len(raw_subplane_df)} rows")
        print("\nFirst few rows:")
        print(raw_subplane_df.head(10))
    else:
        print("\nNo lots to process - execution stopped at Step 07")
