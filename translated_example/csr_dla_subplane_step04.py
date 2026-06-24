"""
CSR DLA Subplane Analysis - Step 04: Product Lookup Table
==========================================================
Translates Step 04 from VG2 workflow to Python using DataSyncX.

Step 04 performs:
1. Copy Product_Lookup.csv from network share
2. Load CSV data with product limits (upper/lower X/Y limits per site/prodgroup)

Original: CSR_DLA_subplane_Rev7 VG2
SQL Source: 04 - Product lookup table.txt
"""

import os
import shutil
from datetime import datetime
import pandas as pd
import logging

# Import shared logger and config
try:
    # Try relative import first (when part of package)
    from .utils import GlobalConfig, save_df_as_csv, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT
except ImportError:
    # Fall back to absolute import (when run standalone)
    from core.utils import GlobalConfig, save_df_as_csv, LOG_LEVEL, LOG_FORMAT, LOG_DATE_FORMAT

logging.basicConfig(level=LOG_LEVEL, format=LOG_FORMAT, datefmt=LOG_DATE_FORMAT)

# ==============================================================================
# Step 04: Product Lookup Table
# ==============================================================================

class Step04_ProductLookup:
    """
    Step 04: Load Product Lookup Table
    
    This step:
    1. Copies Product_Lookup.csv from network share to local directory
    2. Reads the CSV containing product group limits (X/Y bounds per site)
    3. Outputs to CSR_Server_OIS_Product_List.csv
    
    Columns:
    - site: Manufacturing site code (A01, A06, A15)
    - prodgroup3: Product group identifier
    - upper_y_limit: Upper Y-axis limit for subplane angle
    - lower_y_limit: Lower Y-axis limit for subplane angle
    - upper_x_limit: Upper X-axis limit for subplane angle
    - lower_x_limit: Lower X-axis limit for subplane angle
    """
    
    def __init__(self):
        self.logger = logging.getLogger("Step04 - ProductLookup")
    
    def step_1_copy_files(self) -> bool:
        """Step 1: Copy Product_Lookup.csv from network share"""
        self.logger.debug("Step 1: Copy Files/Folders")
        
        try:
            source = GlobalConfig.PRODUCT_LOOKUP_SOURCE
            dest = GlobalConfig.get_output_path(GlobalConfig.PRODUCT_LOOKUP_FILENAME)
            self.logger.debug(f"Copying {source} to {dest.parent}\\")
            
            # Copy file from network share
            if os.path.exists(source):
                shutil.copy2(source, dest)
                self.logger.debug("      1 file(s) copied.\n")
                self.logger.debug("Completed Distribution Applet ... " + 
                              datetime.now().strftime("%d-%b-%Y %H:%M:%S") + "\n")
                return True
            else:
                self.logger.debug(f"  WARNING: Source file not found: {source}")
                self.logger.debug(f"  Attempting to use local copy if available...\n")
                if dest.exists():
                    self.logger.debug(f"  Using existing local file: {dest}\n")
                    return True
                else:
                    raise FileNotFoundError(f"Neither source nor local file found")
        
        except Exception as e:
            self.logger.debug(f"  ERROR in file copy: {str(e)}\n")
            raise
    
    def step_2_fetch_sqlite_data(self) -> pd.DataFrame:
        """Step 2.1: Fetching Text (SQLite) Data - Load CSV as DataFrame"""
        self.logger.debug("Step 2.1: Fetching Text (SQLite) Data")
        
        try:
            # Read the CSV file
            csv_path = GlobalConfig.get_output_path(GlobalConfig.PRODUCT_LOOKUP_FILENAME)
            
            if not csv_path.exists():
                raise FileNotFoundError(f"Product lookup file not found: {csv_path}")
            
            # Read CSV - mimics SQLite query selecting all columns
            df = pd.read_csv(csv_path)
            
            # Convert column names to lowercase (CSV has uppercase columns)
            df.columns = df.columns.str.lower()
            
            # Ensure expected columns exist
            expected_cols = ['site', 'prodgroup3', 'upper_y_limit', 'lower_y_limit', 
                           'upper_x_limit', 'lower_x_limit']
            
            for col in expected_cols:
                if col not in df.columns:
                    raise ValueError(f"Missing expected column: {col}")
            
            # Select and order columns as per SQL query (exclude Remarks column)
            df = df[expected_cols]
            
            # Display the data in table format
            self.logger.debug(df.to_string())
            
            # Log row count
            self.logger.debug(f"Rows returned {len(df)}")
            
            # Save to output file
            self.logger.debug(save_df_as_csv(
                df, 
                GlobalConfig.CSR_SERVER_OIS_PRODUCT_LIST_FILENAME, 
                quote_all=True
            ))
            
            return df
        
        except Exception as e:
            self.logger.debug(f"  ERROR in data fetch: {str(e)}\n")
            raise
    
    def run(self) -> pd.DataFrame:
        """Execute Step 04 - Product Lookup Table"""
        try:
            # Step 1: Copy files
            self.step_1_copy_files()
            
            # Step 2: Load data
            df = self.step_2_fetch_sqlite_data()
            
            return df
        
        except Exception as e:
            self.logger.debug(f"\n*** Step 04 FAILED: {str(e)} ***\n")
            raise


# ==============================================================================
# Main Execution
# ==============================================================================

def main():
    """Main execution function for Step 04"""
    
    # Initialize logger
    
    try:
        # Execute Step 04
        step04 = Step04_ProductLookup()
        product_df = step04.run()
        
        step04.logger.info(f"Step 04 COMPLETED SUCCESSFULLY")
        step04.logger.info(product_df.to_string())
        step04.logger.info(f"Loaded {len(product_df)} product configurations")
        step04.logger.info(f"Output: {GlobalConfig.get_output_path(GlobalConfig.CSR_SERVER_OIS_PRODUCT_LIST_FILENAME)}")
        
        return product_df
    
    except Exception as e:
        step04.logger.error(f"Step 04 FAILED: {str(e)}")
        raise


if __name__ == "__main__":
    product_lookup_df = main()
    print(f"\nProduct Lookup Table loaded: {len(product_lookup_df)} rows")
    print("\nFirst few rows:")
    print(product_lookup_df.head(10))
