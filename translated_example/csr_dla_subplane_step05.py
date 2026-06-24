"""
CSR DLA Subplane Analysis - Step 05: Last 8 Hour Lot List
==========================================================
Translates Step 05 from VG2 workflow to Python using DataSyncX.

Step 05 performs:
1. Query MARS database for lot history in the last 8 hours
2. Filter by operations 2090 and 1960 (OIS operations)
3. Filter by product groups from Product Lookup (Step 04 output)
4. Filter by DIA entities and MVOU transactions
5. Output lot list for further processing

Original: CSR_DLA_subplane_Rev7 VG2
SQL Source: 05 - last8hour lot list.txt
"""

from pathlib import Path
from typing import List, Optional
import pandas as pd
import logging

# Import shared logger and config
try:
    # Try relative import first (when part of package)
    from .utils import (
        GlobalConfig, 
        save_df_as_csv,
        DataReader,
        ProductionDataReader, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT
    )
except ImportError:
    # Fall back to absolute import (when run standalone)
    from core.utils import (
        GlobalConfig, 
        save_df_as_csv,
        DataReader,
        ProductionDataReader, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT
    )

logging.basicConfig(level=LOG_LEVEL, format=LOG_FORMAT, datefmt=LOG_DATE_FORMAT)

# ==============================================================================
# Helper Functions
# ==============================================================================

def read_csv_column(csv_path: Path, column_name: str) -> List[str]:
    """
    Read a column from CSV and return unique values
    
    This mimics SQLPathFinder's SQL_Get_CSV_List function.
    
    Args:
        csv_path: Path to the CSV file
        column_name: Name of the column to read
    
    Returns:
        List of unique values from the column
    """
    df = pd.read_csv(csv_path)
    
    if column_name not in df.columns:
        raise ValueError(f"Column '{column_name}' not found in {csv_path}")
    
    # Get unique non-null values
    values = df[column_name].dropna().unique().tolist()
    return values


def format_in_clause(values: List[str]) -> str:
    """
    Format a list of values for SQL IN clause
    
    Args:
        values: List of string values
    
    Returns:
        Formatted string like "'VAL1','VAL2','VAL3'"
    """
    return ",".join([f"'{v}'" for v in values])


# ==============================================================================
# Step 05: MARS Lot List Query
# ==============================================================================

class Step05_MarsLotList:
    """
    Step 05: Fetch lot list from MARS for last 8 hours
    
    This step:
    1. Reads product groups from Step 04 output
    2. Queries MARS WIP_Lot_History for lots at operations 2090/1960
    3. Filters by:
       - Last 8 hours (load_date >= SYSDATE - 8/24)
       - Product groups from lookup table
       - DIA entities
       - MVOU transactions (moveout transactions)
    4. Returns lot list with entity, route, quantities, etc.
    
    Columns returned:
    - site_work_week: Site work week
    - lot: Lot number
    - operation: Operation code
    - out_date: Load date (moveout date)
    - route: Manufacturing route
    - owner: Lot owner
    - oldqty1, newqty1: Quantities
    - entity: Equipment entity (DIA*)
    - prodgroup3: Product group
    - facility: Manufacturing facility
    """
    
    def __init__(self, data_reader: Optional[DataReader] = None):
        """
        Initialize Step 05
        
        Args:
            data_reader: DataReader instance for data access (production or mock)
                        If None, uses production reader by default
        """
        self.logger = logging.getLogger(name="Step05 - Mars Lot List")
        self.data_reader = data_reader or ProductionDataReader()
    
    def step_1_fetch_mars_data(self) -> pd.DataFrame:
        """Step 1.1-a0: Fetching MARS Data"""
        
        # Read product groups from Step 04 output
        product_list_path = GlobalConfig.get_output_path(GlobalConfig.CSR_SERVER_OIS_PRODUCT_LIST_FILENAME)
        if not product_list_path.exists():
            raise FileNotFoundError(
                f"Product list not found: {product_list_path}\n"
                f"Please run Step 04 first."
            )
        
        product_groups = read_csv_column(
            product_list_path, 
            'prodgroup3'
        )
        
        if not product_groups:
            raise ValueError("No product groups found in product list")
        
        # Format product groups for IN clause
        prodgroup_in = format_in_clause(product_groups)
        
        # Format operations for IN clause
        operations_in = format_in_clause(GlobalConfig.OPERATIONS)
        
        # Build MARS query
        # Note: @[]@ is used for MARS tables - DataSyncX will substitute with proper schema
        # The substitution becomes "SCHEMA.TABLE" so we need to include the dot
        sql_query = f"""
SELECT DISTINCT 
    c0.ww AS site_work_week
    ,f0.lot AS lot
    ,f0.operation AS operation
    ,TO_CHAR(f0.load_date,'yyyy-mm-dd hh24:mi:ss') AS out_date
    ,f0.route AS route
    ,f0.owner AS owner
    ,f0.oldqty1 AS oldqty1
    ,f0.newqty1 AS newqty1
    ,f4.entity AS entity
    ,p.prodgroup3 AS prodgroup3
    ,f0.facility AS facility
FROM 
@[]@.F_LotHist f0
INNER JOIN @[]@.F_Calendar c0 
    ON f0.last_action_date BETWEEN c0.start_date AND c0.end_date 
    AND c0.event_code = 'S' 
    AND DECODE(f0.facility,'RA3','AAL',f0.facility) = c0.facility
LEFT JOIN @[]@.F_Product p 
    ON p.product = f0.product 
    AND p.facility = f0.facility 
    AND NVL(p.latest_version,'Y') = 'Y'
INNER JOIN @[]@.F_Lot f9 
    ON f9.lot = f0.lot
LEFT JOIN @[]@.F_EntityLotHist f4 
    ON f4.lot = f0.lot 
    AND f4.operation = f0.operation 
    AND f4.prevout_date = f0.prevout_date 
    AND NVL(f4.history_deleted_flag,'N') = 'N' 
    AND f4.unique_flag = 'Y'
    AND f4.entity LIKE 'DIA%'
LEFT JOIN @[]@.F_EntityHist eh 
    ON f4.entity = eh.entity 
    AND f4.txn_date = eh.txn_date 
    AND f4.facility = eh.facility 
    AND f4.datasource = eh.datasource
LEFT JOIN @[]@.F_Entity en 
    ON f4.entity = en.entity 
    AND f4.facility = en.facility
WHERE
    NVL(f0.history_deleted_flag,'N') = 'N'
    AND f0.owner <> 'EMPTYFOUP'
    AND p.prodgroup3 IN ({prodgroup_in})
    AND f0.operation IN ({operations_in})
    AND f0.load_date >= (SYSDATE - {GlobalConfig.LOOKBACK_HOURS}/24)
    AND f0.movedout_txn IN ('MVOU')
"""
        
        # Log step header
        site_info = f"({GlobalConfig.SITE}.[A15_PROD_21.].MARS)"
        self.logger.debug(
            f"Step 1.1-a0: Fetching MARS Data {site_info}"
        )
        
        try:
            # Use the injected data_reader (production or mock)
            df = self.data_reader.read_mars(GlobalConfig.SITE, sql_query)
            
            # Convert column names to match expected output (uppercase)
            df.columns = df.columns.str.upper()
            
            # Log results
            self.logger.debug(f"{len(df)} records retrieved from MARS")
            
            # Display data table
            self.logger.debug(df.to_string())
            
            # Log row count again (SPF format)
            self.logger.debug(f"rows returned: {len(df)}")
            
            # Save to CSV
            self.logger.debug(save_df_as_csv(
                df, 
                GlobalConfig.CSR_SERVER_OIS_SUBPLANE_LOTLIST_FILENAME, 
                quote_all=False  # Don't quote all fields for this output
            ))
            
            return df
        
        except Exception as e:
            self.logger.error(f"MARS query failed: {str(e)}")
            raise
    
    def run(self) -> pd.DataFrame:
        """Execute Step 05 - MARS Lot List Query"""
        try:
            # Step 1: Fetch MARS data
            df = self.step_1_fetch_mars_data()
            return df
        
        except Exception as e:
            self.logger.error(f"Step 05 FAILED: {str(e)}")
            raise


# ==============================================================================
# Main Execution
# ==============================================================================

def main(data_reader: Optional[DataReader] = None) -> pd.DataFrame:
    """Main execution function for Step 05"""
    
    try:
        # Execute Step 05
        step05 = Step05_MarsLotList(ProductionDataReader() if data_reader is None else data_reader)
        lot_df = step05.run()
        
        step05.logger.info(f"Step 05 COMPLETED SUCCESSFULLY")
        step05.logger.info(f"Output: {GlobalConfig.get_output_path(GlobalConfig.CSR_SERVER_OIS_SUBPLANE_LOTLIST_FILENAME)}")
        step05.logger.info(lot_df.to_string(index=False))
        step05.logger.info(f"Retrieved {len(lot_df)} lots from MARS")
        
        return lot_df
    
    except Exception as e:
        step05.logger.error(f"Step 05 FAILED: {str(e)}")
        raise


if __name__ == "__main__":
    lot_list_df = main()
    print(f"\nLot List retrieved: {len(lot_list_df)} rows")
    print("\nFirst few rows:")
    print(lot_list_df.head(10))
