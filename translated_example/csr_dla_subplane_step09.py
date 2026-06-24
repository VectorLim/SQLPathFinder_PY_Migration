"""
CSR DLA Subplane Analysis - Step 09: Interim Ranking
======================================================
Translates Step 09 from VG2 workflow to Python using DataSyncX.

Step 09 performs:
1. Join raw subplane data with product limits
2. Check if subplane X/Y values exceed limits
3. Filter to records that exceed Y limits
4. Add DENSE_RANK by entity_bs_x_y, ordered by visual_id

This identifies units that exceed the Y-axis subplane angle limits.

Original: CSR_DLA_subplane_Rev7 VG2
SQL Source: 09 - interim ranking.txt
"""

import pandas as pd
import sqlite3
import logging

# Import shared logger and config
try:
    # Try relative import first (when part of package)
    from .utils import GlobalConfig, save_df_as_csv, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT
except ImportError:
    # Fall back to absolute import (when run standalone)
    from core.utils import GlobalConfig, save_df_as_csv, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT



# ==============================================================================
# Step 09: Interim Ranking
# ==============================================================================
logging.basicConfig(level=LOG_LEVEL, format=LOG_FORMAT, datefmt=LOG_DATE_FORMAT)

class Step09_InterimRanking:
    """
    Step 09: Join subplane data with product limits and identify exceedances
    
    This step:
    1. Load CSR_Server_OIS_subplane.csv (raw subplane data from Step 08)
    2. Load CSR_Server_OIS_Product_List.csv (product limits from Step 04)
    3. Join on prodgroup3 and facility/site
    4. Convert string values to numeric (SUBPLANEANGLEX, SUBPLANEANGLEY, limits)
    5. Check if values exceed limits:
       - Set_Limit_plane_X: 'X_flag' if sub_plane_x NOT BETWEEN lower_x_limit AND upper_x_limit
       - Set_Limit_plane_Y: 'Y_flag' if sub_plane_y NOT BETWEEN lower_y_limit AND upper_y_limit
    6. Calculate Flag:
       - 'flag' if Set_Limit_plane_Y = 'Y_flag'
    7. Filter WHERE Flag = 'flag' (only records exceeding Y limit)
    8. Add DENSE_RANK partitioned by entity_bs_x_y, ordered by visual_id ASC
    
    Output columns:
    - All columns from raw subplane data
    - site, prodgroup3_1 (from product list)
    - sub_plane_x, sub_plane_y (numeric converted)
    - lower_x_limit, upper_x_limit, lower_y_limit, upper_y_limit
    - Set_Limit_plane_X, Set_Limit_plane_Y (flags)
    - Flag (filter condition)
    - Dense_rank (ranking within entity_bs_x_y group)
    """
    
    def __init__(self):
        self.logger = logging.getLogger("Step09 - InterimRanking")
    
    def run(self) -> pd.DataFrame:
        """Execute Step 09 - Interim Ranking"""
        
        # Input files
        subplane_path = GlobalConfig.get_output_path(GlobalConfig.CSR_SERVER_OIS_SUBPLANE_FILENAME)
        product_list_path = GlobalConfig.get_output_path(GlobalConfig.CSR_SERVER_OIS_PRODUCT_LIST_FILENAME)
        
        # Check inputs exist
        if not subplane_path.exists():
            raise FileNotFoundError(
                f"Subplane data not found: {subplane_path}\n"
                f"Please run Steps 06-08 first."
            )
        
        if not product_list_path.exists():
            raise FileNotFoundError(
                f"Product list not found: {product_list_path}\n"
                f"Please run Step 04 first."
            )
        
        self.logger.debug("Step 1.1: Fetching Text (SQLite) Data")
        
        try:
            # Create in-memory SQLite database
            conn = sqlite3.connect(':memory:')
            
            # Load dataframes
            subplane_df = pd.read_csv(subplane_path)
            product_df = pd.read_csv(product_list_path)
            
            # Write to SQLite
            subplane_df.to_sql('CSR_Server_OIS_subplane', conn, index=False, if_exists='replace')
            product_df.to_sql('CSR_Server_OIS_Product_List', conn, index=False, if_exists='replace')
            
            # Create index for performance
            conn.execute('CREATE INDEX IF NOT EXISTS IdxA0 ON CSR_Server_OIS_Product_List (prodgroup3, site)')
            
            # Build SQL query - multi-level nested query
            sql_query = """
SELECT DISTINCT 
    facility
    ,lot
    ,operation
    ,test_end_date
    ,tester_id
    ,program_name
    ,prodgroup3
    ,visual_id
    ,tray_or_carrier_id
    ,ws_loss_code
    ,entity
    ,bond_station
    ,carrier_x
    ,carrier_y
    ,lane_number
    ,entity_bs_x_y
    ,site
    ,prodgroup3_1
    ,sub_plane_x
    ,sub_plane_y
    ,lower_x_limit
    ,upper_x_limit
    ,lower_y_limit
    ,upper_y_limit
    ,Set_Limit_plane_X
    ,Set_Limit_plane_Y
    ,Flag
    ,DENSE_RANK() OVER (PARTITION BY entity_bs_x_y ORDER BY visual_id ASC) AS Dense_rank
FROM
(
    SELECT 
        facility
        ,lot
        ,operation
        ,test_end_date
        ,tester_id
        ,program_name
        ,prodgroup3
        ,visual_id
        ,tray_or_carrier_id
        ,ws_loss_code
        ,entity
        ,bond_station
        ,carrier_x
        ,carrier_y
        ,lane_number
        ,entity_bs_x_y
        ,site
        ,prodgroup3_1
        ,sub_plane_x
        ,sub_plane_y
        ,lower_x_limit
        ,upper_x_limit
        ,lower_y_limit
        ,upper_y_limit
        ,Set_Limit_plane_X
        ,Set_Limit_plane_Y
        ,CASE 
            WHEN Set_Limit_plane_Y = 'Y_flag' AND Set_Limit_plane_X <> 'X_flag' THEN 'Y_flag_only' 
            ELSE '' 
        END AS BeyondY_Flag
        ,CASE 
            WHEN Set_Limit_plane_Y = 'Y_flag' THEN 'flag' 
            ELSE '' 
        END AS Flag
    FROM
    (
        SELECT 
            facility
            ,lot
            ,operation
            ,test_end_date
            ,tester_id
            ,program_name
            ,prodgroup3
            ,visual_id
            ,tray_or_carrier_id
            ,ws_loss_code
            ,entity
            ,bond_station
            ,carrier_x
            ,carrier_y
            ,lane_number
            ,entity_bs_x_y
            ,site
            ,prodgroup3_1
            ,sub_plane_x
            ,sub_plane_y
            ,lower_x_limit
            ,upper_x_limit
            ,lower_y_limit
            ,upper_y_limit
            ,CASE 
                WHEN sub_plane_x NOT BETWEEN lower_x_limit AND upper_x_limit THEN 'X_flag' 
                ELSE '' 
            END AS Set_Limit_plane_X
            ,CASE 
                WHEN sub_plane_y NOT BETWEEN lower_y_limit AND upper_y_limit THEN 'Y_flag' 
                ELSE '' 
            END AS Set_Limit_plane_Y
        FROM
        (
            SELECT 
                a1.facility AS facility
                ,a1.lot AS lot
                ,a1.operation AS operation
                ,a1.test_end_date AS test_end_date
                ,a1.tester_id AS tester_id
                ,a1.program_name AS program_name
                ,a1.prodgroup3 AS prodgroup3
                ,a1.visual_id AS visual_id
                ,a1.tray_or_carrier_id AS tray_or_carrier_id
                ,a1.ws_loss_code AS ws_loss_code
                ,a1.entity AS entity
                ,a1.bond_station AS bond_station
                ,a1.carrier_x AS carrier_x
                ,a1.carrier_y AS carrier_y
                ,a1.lane_number AS lane_number
                ,a1.Entity_BS_X_Y AS entity_bs_x_y
                ,a0.site AS site
                ,a0.prodgroup3 AS prodgroup3_1
                ,CASE 
                    WHEN a1.SUBPLANEANGLEX = '' THEN NULL 
                    ELSE CAST(a1.SUBPLANEANGLEX AS REAL) 
                END AS sub_plane_x
                ,CASE 
                    WHEN a1.SUBPLANEANGLEY = '' THEN NULL 
                    ELSE CAST(a1.SUBPLANEANGLEY AS REAL) 
                END AS sub_plane_y
                ,CASE 
                    WHEN a0.lower_x_limit = '' THEN NULL 
                    ELSE CAST(a0.lower_x_limit AS REAL) 
                END AS lower_x_limit
                ,CASE 
                    WHEN a0.upper_x_limit = '' THEN NULL 
                    ELSE CAST(a0.upper_x_limit AS REAL) 
                END AS upper_x_limit
                ,CASE 
                    WHEN a0.lower_y_limit = '' THEN NULL 
                    ELSE CAST(a0.lower_y_limit AS REAL) 
                END AS lower_y_limit
                ,CASE 
                    WHEN a0.upper_y_limit = '' THEN NULL 
                    ELSE CAST(a0.upper_y_limit AS REAL) 
                END AS upper_y_limit
            FROM 
                CSR_Server_OIS_subplane a1
            LEFT OUTER JOIN CSR_Server_OIS_Product_List a0
                ON a0.prodgroup3 = a1.prodgroup3 
                AND a0.site = a1.facility
        ) t
    ) t
) t
WHERE Flag = 'flag'
"""
            
            # Execute query
            result_df = pd.read_sql_query(sql_query, conn)
            conn.close()
            
            self.logger.debug(f"Rows returned: {len(result_df)}")
            
            return result_df
        
        except Exception as e:
            self.logger.error(f"SQLite query failed: {str(e)}")
            raise


# ==============================================================================
# Main Execution
# ==============================================================================

def main():
    """Main execution function for Step 09"""
    
    # Initialize logger
    try:
        # Execute Step 09
        step09 = Step09_InterimRanking()
        interim_df = step09.run()
        
        # Save output
        if len(interim_df) > 0:
            output_path = save_df_as_csv(
                interim_df,
                "CSR_Server_OIS_subplane_interim.csv",
                quote_all=True
            )
            
            step09.logger.info(f"Step 09 COMPLETED SUCCESSFULLY")
            step09.logger.info(interim_df.to_string())
            step09.logger.info(f"Found {len(interim_df)} units exceeding Y-axis subplane angle limits")
            step09.logger.info(f"Output: {output_path}")
            
        else:
            # No records exceed limits - still success
            step09.logger.info(f"Step 09 COMPLETED SUCCESSFULLY")
            step09.logger.info(f"No units exceed Y-axis subplane angle limits")
            step09.logger.info(f"No output file created (0 records)")
            
        
        return interim_df
    
    except Exception as e:
        step09.logger.error(f"Step 09 FAILED: {str(e)}")
        
        raise


if __name__ == "__main__":
    main()
