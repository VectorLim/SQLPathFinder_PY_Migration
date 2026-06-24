"""
SPF Logger Module - Reusable SPF-Style Logger
==============================================
Provides logging functionality that mimics SQLPathFinder output format.

This module can be imported by all step scripts to maintain consistent
logging format across the entire pipeline.
"""

from pathlib import Path
import re
from typing import Union
import pandas as pd
import logging
# ==============================================================================
# Logging Configuration
# ==============================================================================

# Standard logging configuration for all steps
LOG_LEVEL = logging.INFO
LOG_FORMAT = '%(asctime)s\t%(name)s\t-\t%(message)s'
LOG_DATE_FORMAT = '%Y-%m-%d %H:%M:%S'

# ==============================================================================
# Global Configuration
# ==============================================================================

class GlobalConfig:
    """Global configuration for CSR DLA Subplane Analysis"""
    
    # Site and facility settings
    SITE = "KM"
    FACILITY = "A15"
    
    # File paths
    WORK_DIR = Path.cwd()
    OUTPUT_DIR = WORK_DIR / "output_files"
    if not OUTPUT_DIR.exists():
        OUTPUT_DIR.mkdir(parents=True, exist_ok=True)
        print(f"Created output directory: {OUTPUT_DIR}")
    
    # Mock data directory (bundled with core module)
    MOCK_DATA_DIR = Path(__file__).parent / "mock_data" 
    
    
    # CSR Server paths (network share)
    CSR_BASE_PATH = r"//AZATSHFS.intel.com\AZATAnalysis$/MAOATM/Config/VF_POR_Cfg/ICM_PCS"
    PRODUCT_LOOKUP_SOURCE = f"{CSR_BASE_PATH}/ICMPCS_SUBPLANE_CSR_DLA/Product_Lookup.csv"
    HIST_NETWORK_PATH = f"{CSR_BASE_PATH}/ICMPCS_SUBPLANE_CSR_DLA/KM/HIST/HIST.txt"
    
    # Local working files
    PRODUCT_LOOKUP_FILENAME = "Product_Lookup.csv"
    CSR_SERVER_OIS_SUBPLANE_LOTLIST_FILENAME = "CSR_Server_OIS_subplane_lotlist.csv"
    CSR_SERVER_OIS_PRODUCT_LIST_FILENAME = "CSR_Server_OIS_Product_List.csv"
    CSR_SERVER_OIS_SUBPLANE_INTERIM_FILENAME = "CSR_Server_OIS_subplane_interim.csv"
    CSR_SERVER_OIS_SUBPLANE_OUTPUT_FILENAME = "CSR_Server_OIS_subplane_output.csv"
    CSR_SERVER_OIS_SUBPLANE_FILENAME = "CSR_Server_OIS_subplane.csv"
    
    
    # Step 04/05 Configuration
    OPERATIONS = ["2090", "1960"]
    LOOKBACK_HOURS = 8
    
    FINAL_OUTPUT_FILENAME = "data.csv"
    HISTORICAL_RECORD_FILENAME = "HIST.csv"
    
    DEBUG: bool = False
    
    @classmethod
    def get_output_path(cls, filename: str) -> Path:
        """Get the full path for an output file in OUTPUT_DIR"""
        if cls.DEBUG:
            return cls.MOCK_DATA_DIR / filename
        return cls.OUTPUT_DIR / filename

    @classmethod
    def find_mock_data_dir(cls) -> Path:
        """Get the mock data directory bundled with core module"""
        if cls.MOCK_DATA_DIR.exists():
            return cls.MOCK_DATA_DIR
        raise FileNotFoundError(
            f"Cannot find mock_data directory at: {cls.MOCK_DATA_DIR}"
        )

# ==============================================
# Data Reader Imports (from data_readers module)
# ==============================================================================
"""
Data Reader Adapters - Strategy Pattern for Flexible Data Access
==================================================================

Provides abstract DataReader interface with concrete implementations:
- ProductionDataReader: Uses DataSyncX for production database access
- MockDataReader: Uses CSV files for testing without database access

Supports dependency injection for clean testing and flexible design.
"""

from abc import ABC, abstractmethod
from pathlib import Path
import pandas as pd


# ==============================================================================
# Data Reader Abstract Interface
# ==============================================================================

class DataReader(ABC):
    """
    Abstract base class for data readers
    
    Implements Strategy pattern to allow switching between production
    and mock data sources for testing.
    """
    
    @abstractmethod
    def read_mars(self, site: str, sql_query: str) -> pd.DataFrame:
        pass
    
    @abstractmethod
    def read_aries(self, site: str, sql_query: str) -> pd.DataFrame:
        pass


# ==============================================================================
# Production Data Reader
# ==============================================================================

class ProductionDataReader(DataReader):
    """
    Production data reader using actual DataSyncX readers
    
    This adapter wraps the real MarsReader and AriesReader from DataSyncX
    to provide a consistent interface for dependency injection.
    """
    
    def __init__(self):
        """Initialize production readers from DataSyncX"""
        try:
            from datasyncx import MarsReader, AriesReader
            self.mars_reader = MarsReader()
            self.aries_reader = AriesReader()
        except ImportError:
            raise ImportError(
                "DataSyncX not available. Please ensure datasyncx is installed "
                "for production data access."
            )
    
    def read_mars(self, site: str, sql_query: str) -> pd.DataFrame:
        """Execute MARS query using DataSyncX MarsReader"""
        return self.mars_reader.read(site, sql_query)
    
    def read_aries(self, site: str, sql_query: str) -> pd.DataFrame:
        """Execute ARIES query using DataSyncX AriesReader"""
        return self.aries_reader.read(site, sql_query)


# ==============================================================================
# Mock Data Reader
# ==============================================================================

class MockDataReader(DataReader):
    """
    Mock data reader for testing - reads from CSV files
    
    This adapter reads pre-generated mock data from CSV files instead of
    querying actual databases. It infers which CSV file to read based on
    table names and patterns in the SQL query.
    
    Expected CSV files in mock_data_dir:
    - mars_lot_list.csv: Mock data for Step 05 (F_LotHist + F_Calendar)
    - mars_wip_status.csv: Mock data for Step 13 (WIP_Lot_Status)
    - aries_subplane_data.csv: Mock data for Steps 06-08 (A_Testing_Session)
    
    CSV files should have columns matching the expected output from real queries.
    """
    
    def __init__(self, mock_data_dir: Union[str, Path]):
        """
        Initialize mock data reader
        
        Args:
            mock_data_dir: Path to directory containing mock CSV files
        """
        self.mock_data_dir = Path(mock_data_dir)
        
        if not self.mock_data_dir.exists():
            raise FileNotFoundError(
                f"Mock data directory not found: {self.mock_data_dir}\n"
                f"Please create the directory and add mock CSV files."
            )
    
    def read_mars(self, site: str, sql_query: str) -> pd.DataFrame:
        """
        Read mock MARS data from CSV file
        
        Infers which CSV file to read based on SQL query patterns:
        - F_LotHist + F_Calendar → mars_lot_list.csv (Step 05)
        - WIP_Lot_Status → mars_wip_status.csv (Step 13)
        
        Args:
            site: Site code (ignored in mock mode)
            sql_query: SQL query string (used for pattern matching)
        
        Returns:
            DataFrame from corresponding mock CSV file
        """
        # Infer which step/query based on tables/keywords in SQL
        if 'F_LotHist' in sql_query and 'F_Calendar' in sql_query:
            # Step 05 - lot list query
            csv_path = self.mock_data_dir / 'mars_lot_list.csv'
        elif 'WIP_Lot_Status' in sql_query or 'F_Lot' in sql_query:
            # Step 13 - WIP status query
            csv_path = self.mock_data_dir / 'mars_wip_status.csv'
        else:
            raise ValueError(
                f"Unknown MARS query pattern. Cannot determine mock data file.\n"
                f"Query contains: {self._extract_tables(sql_query)}\n"
                f"Expected: F_LotHist+F_Calendar or WIP_Lot_Status"
            )
        
        if not csv_path.exists():
            raise FileNotFoundError(
                f"Mock data file not found: {csv_path}\n"
                f"Please create this file in {self.mock_data_dir}"
            )
        
        df = pd.read_csv(csv_path)
        
        # Convert to uppercase columns to match DataSyncX behavior
        # (DataSyncX returns lowercase, but scripts convert to uppercase)
        df.columns = df.columns.str.upper()
        
        return df
    
    def read_aries(self, site: str, sql_query: str) -> pd.DataFrame:
        """
        Read mock ARIES data from CSV file
        
        Infers which CSV file to read based on SQL query patterns:
        - A_Testing_Session → aries_subplane_data.csv (Steps 06-08)
        
        Args:
            site: Site code (ignored in mock mode)
            sql_query: SQL query string (used for pattern matching)
        
        Returns:
            DataFrame from corresponding mock CSV file
        """
        # Infer which step based on tables in SQL
        if 'A_Testing_Session' in sql_query:
            # Steps 06-08 - subplane metrology query
            csv_path = self.mock_data_dir / 'aries_subplane_data.csv'
        elif 'AV_dia_session' in sql_query or 'bonding_station' in sql_query:
            # Step 08 - bonding station query
            csv_path = self.mock_data_dir / 'aries_bonding_data.csv'
        else:
            raise ValueError(
                f"Unknown ARIES query pattern. Cannot determine mock data file.\n"
                f"Query contains: {self._extract_tables(sql_query)}\n"
                f"Expected: A_Testing_Session or AV_dia_session"
            )
        
        if not csv_path.exists():
            raise FileNotFoundError(
                f"Mock data file not found: {csv_path}\n"
                f"Please create this file in {self.mock_data_dir}"
            )
        
        df = pd.read_csv(csv_path)
        
        # Convert to lowercase columns to match DataSyncX behavior
        df.columns = df.columns.str.lower()
        
        return df
    
    def _extract_tables(self, sql_query: str) -> list:
        """Extract table names from SQL query for error messages"""
        # Simple pattern to find @[]@.TableName or FROM TableName
        pattern = r'@\[\]@\.(\w+)|FROM\s+(\w+)|JOIN\s+(\w+)'
        matches = re.findall(pattern, sql_query, re.IGNORECASE)
        tables = [m for group in matches for m in group if m]
        return list(set(tables))

    


# ==============================================================================
# Utility Functions
# ==============================================================================

def save_df_as_csv(df: pd.DataFrame, filename: str, quote_all: bool = True) -> Path:
    """
    Save DataFrame to CSV and log the output file path
    
    Args:
        df: DataFrame to save
        filename: Output filename
        logger: SPFLogger instance
        quote_all: If True, quote all fields in CSV
    
    Returns:
        Path to the saved file
    """
    output_path = GlobalConfig.get_output_path(filename)
    quoting = 1 if quote_all else 0  # 1 = csv.QUOTE_ALL
    df.to_csv(output_path, index=False, quoting=quoting)
    return output_path
