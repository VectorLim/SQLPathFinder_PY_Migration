"""
CSR DLA Subplane Analysis - Main Execution Script
==================================================

This module orchestrates the complete CSR DLA Subplane Analysis workflow
by executing all steps in sequence.

Workflow Steps:
    Step 00: Setup - HIST File Management
    Step 04: Product Lookup Table
    Step 05: Last 8 Hour Lot List (MARS)
    Steps 06-08: Row Check & Raw Subplane Data (ARIES)
    Step 09: Interim Ranking (Apply Product Limits)
    Step 10: Final List (Filter Rank 3+)
    Steps 11-13: Row Check & STIM Lot at Cure (MARS)

Usage:
    # Run as zipapp (packaged executable)
    python core.pyz
    python core.pyz -o ./output
    python core.pyz --output C:/results/analysis
    
"""

import argparse
import sys
import tempfile
import zipfile
from pathlib import Path
import pandas as pd
import traceback

# Import all step modules
try:
    # Try relative imports first (when part of package / pyz)
    from core.csr_dla_subplane_step00_setup import main as step00_main
    from core.csr_dla_subplane_step04 import main as step04_main
    from core.csr_dla_subplane_step05 import main as step05_main
    from core.csr_dla_subplane_step06_07_08 import main as step06_08_main
    from core.csr_dla_subplane_step09 import main as step09_main
    from core.csr_dla_subplane_step10 import main as step10_main
    from core.csr_dla_subplane_step11_12_13 import main as step11_13_main
    from core.utils import GlobalConfig, ProductionDataReader, MockDataReader, DataReader
except ImportError:
    # Fall back to absolute imports (when run standalone)
    from core.csr_dla_subplane_step00_setup import main as step00_main
    from core.csr_dla_subplane_step04 import main as step04_main
    from core.csr_dla_subplane_step05 import main as step05_main
    from core.csr_dla_subplane_step06_07_08 import main as step06_08_main
    from core.csr_dla_subplane_step09 import main as step09_main
    from core.csr_dla_subplane_step10 import main as step10_main
    from core.csr_dla_subplane_step11_12_13 import main as step11_13_main
    from core.utils import GlobalConfig, ProductionDataReader, MockDataReader, DataReader

def parse_arguments() -> argparse.Namespace:
    """
    Parse command-line arguments.
    
    Returns:
        Parsed arguments namespace
    """
    parser = argparse.ArgumentParser(
        description='CSR DLA Subplane Analysis Workflow',
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog=""""""
    )
    
    parser.add_argument(
        '-o', '--output',
        type=str,
        metavar='PATH',
        help='Output directory for all intermediate and final CSV files (default: current directory)'
    )

    parser.add_argument(
        '-t', '--test',
        action='store_true',
        help='Run in test mode using MockDataReader with bundled mock CSV data instead of production databases'
    )

    return parser.parse_args()


def get_mock_data_dir() -> Path:
    """
    Locate the mock data directory.

    When running as a pyz archive, extracts mock_data/ files from the zip
    into a temporary directory. When running as a module, uses configured
    search locations from GlobalConfig.

    Returns:
        Path to a directory containing mock CSV files.

    Raises:
        FileNotFoundError: If no mock data directory can be found.
    """
    pyz_path = Path(sys.argv[0])

    if zipfile.is_zipfile(pyz_path):
        # Running as pyz — extract bundled mock_data/ to a temp dir
        temp_dir = Path(tempfile.mkdtemp(prefix="core_pyz_test_"))
        mock_dir = temp_dir / "mock_data"
        mock_dir.mkdir()
        with zipfile.ZipFile(pyz_path) as zf:
            for name in zf.namelist():
                if name.startswith("mock_data/") and not name.endswith("/"):
                    fname = Path(name).name
                    (mock_dir / fname).write_bytes(zf.read(name))
        print(f"[TEST] Extracted mock data to: {mock_dir}")
        return mock_dir

    # Running as module — use GlobalConfig to find mock data
    return GlobalConfig.find_mock_data_dir()


def setup_test_mode() -> MockDataReader:
    """
    Configure GlobalConfig for test mode and return a MockDataReader.

    - Sets OUTPUT_DIR to test_output/ in the current working directory.
    - Overrides PRODUCT_LOOKUP_LOCAL to point at the mock Product_Lookup.csv
      so Step 04's local-fallback logic picks it up without network access.

    Returns:
        Configured MockDataReader instance.
    """
    mock_data_dir = get_mock_data_dir()

    test_output = Path.cwd() / "test_output"
    test_output.mkdir(exist_ok=True)
    GlobalConfig.OUTPUT_DIR = test_output

    # Block the network source path so step04 always falls back to the local mock file.
    GlobalConfig.PRODUCT_LOOKUP_SOURCE = "/nonexistent/test/path/Product_Lookup.csv"
    
    # Block the HIST network path so step00 creates a dummy HIST.csv
    # (mock_data/HIST.csv is not used directly; step00 creates a dummy when network fails)
    GlobalConfig.HIST_NETWORK_PATH = "/nonexistent/test/path/HIST.txt"

    print(f"[TEST MODE] Mock data dir : {mock_data_dir}")
    print(f"[TEST MODE] Output dir    : {test_output}")

    return MockDataReader(mock_data_dir)


def configure_output_directory(output_path: str| None = None) -> Path:
    """
    Configure the global output directory for all workflow steps.
    
    Args:
        output_path: Custom output path (if None, uses default)
    
    Returns:
        Resolved absolute output directory path
        
    Raises:
        ValueError: If the specified path cannot be created or accessed
    """
    if output_path:
        # If only a name is provided (no path separators), treat it as a folder in current directory
        if '/' not in output_path and '\\' not in output_path:
            output_path = f"./{output_path}"
        
        # Convert relative paths to absolute, preserve absolute paths as-is
        output_dir = Path(output_path).resolve()
        
        # Create directory if it doesn't exist
        try:
            output_dir.mkdir(parents=True, exist_ok=True)
        except Exception as e:
            raise ValueError(f"Cannot create output directory '{output_dir}': {e}")
        
        # Verify directory is writable
        if not output_dir.is_dir():
            raise ValueError(f"Output path '{output_dir}' is not a directory")
        
        # Update global configuration
        GlobalConfig.OUTPUT_DIR = output_dir
        print(f"Output directory set to: {output_dir}")
    else:
        # Ensure default directory is also resolved to absolute path
        output_dir = Path(GlobalConfig.OUTPUT_DIR).resolve()
        print(f"Using default output directory: {output_dir}")
    
    return output_dir


def print_sep(step_num: str, step_name: str):
    """Print step separator"""
    print("\n" + "-"*80)
    print(f"  EXECUTING: {step_num} - {step_name}")
    print("-"*80 + "\n")


def print_footer(success: bool = True):
    """Print workflow completion footer"""
    if success:
        print(" " * 25 + "WORKFLOW COMPLETED SUCCESSFULLY")
        print("\nAll steps executed successfully!")
    else:
        print(" " * 30 + "WORKFLOW FAILED")
        print("\nWorkflow terminated due to error.")


def run_workflow(data_reader: DataReader | None = None) -> int:
    """
    Execute the complete CSR DLA Subplane Analysis workflow.
    
    This function runs all workflow steps in sequence with conditional logic:
    
    Workflow Structure:
    1. Step 00: Setup (always run)
    2. Step 04: Product Lookup (always run)
    3. Step 05: Last 8 Hour Lot List (always run)
    4. **GATE 1**: IF lot list has rows > 0:
         - Steps 06-08: Raw Subplane Data
         - Step 09: Interim Ranking
         - Step 10: Final List
         - **GATE 2**: IF final list has rows > 0:
              - Steps 11-13: STIM Lot at Cure
           ELSE: Skip Steps 11-13
       ELSE: Skip all processing (Steps 06-13)
    
    Returns:
        int: Exit code (0 for success, 1 for failure)
    """
    
    data_reader = data_reader or ProductionDataReader()
    
    try:
        print_sep("STEP 01", "Setup - HIST File Management")
        step00_main()
        
        print_sep("STEP 04", "Product Lookup Table")
        step04_main()
        
        print_sep("STEP 05", "Last 8 Hour Lot List (MARS)")
        lot_list_df = step05_main(data_reader)
        lot_list_path = GlobalConfig.get_output_path("CSR_Server_OIS_subplane_lotlist.csv")
        if not lot_list_path.exists() or len(lot_list_df) == 0:
            print("\n" + "="*80)
            print("No lots to process (lot list empty)")
            print("="*80)

            print_footer(success=True)
            return 0
        
        print("\n" + "="*80)
        print(f"Lot list has {len(lot_list_df)} rows - proceeding")
        print("="*80)
        
        print_sep("STEPS 06-08", "Row Check & Raw Subplane Data (ARIES)")
        raw_df = step06_08_main(data_reader)
        if raw_df is None:
            print("No data after row check - workflow complete")
            print_footer(success=True)
            return 0
        
        print_sep("STEP 09", "Interim Ranking - Apply Product Limits")
        step09_main()
        
        print_sep("STEP 10", "Final List - Filter Rank 3+")
        step10_main()
        
        output_file_path = GlobalConfig.get_output_path("CSR_Server_OIS_subplane_output.csv")
        
        if not output_file_path.exists():
            # Gate 2 FAILED - No output file (0 rows after filtering)
            print("\n" + "="*80)
            print("No units require CSR_HOLD (final list empty)")
            print("\n (no CSR hold actions required)")
            print("="*80)
            print_footer(success=True)
            return 0
        
        # Check row count in output file
        final_df = pd.read_csv(output_file_path)
        if len(final_df) == 0:
            # Gate 2 FAILED - Output file exists but empty
            print("\n" + "="*80)
            print("Final list has 0 rows - no CSR hold actions")
            print("\n (no CSR hold actions required)")
            print("="*80)
            print_footer(success=True)
            return 0
        
        print("\n" + "="*80)
        print(f"Final list has {len(final_df)} rows - proceeding")
        print("="*80)
        
        print_sep("STEPS 11-13", "Row Check & STIM Lot at Cure (MARS)")
        step11_13_main(data_reader)
        
        # Print success footer
        print_footer(success=True)
        return 0
        
    except KeyboardInterrupt:
        print("\n\nWorkflow interrupted by user (Ctrl+C)")
        print_footer(success=False)
        return 1
        
    except Exception as e:
        print(f"\n\nFATAL ERROR: {str(e)}")
        print("\nFull traceback:")
        traceback.print_exc()
        print_footer(success=False)
        return 1


def main():
    """Main entry point for the workflow"""
    # Parse command-line arguments
    args = parse_arguments()

    # Test mode: configure mock reader BEFORE output directory setup
    data_reader: DataReader | None = None
    if args.test:
        print("\n[TEST MODE] Using MockDataReader with anomalous mock data\n")
        data_reader = setup_test_mode()
        GlobalConfig.DEBUG = True  # Enable debug mode for test runs (if needed)
        # test_output is already set by setup_test_mode(); skip normal output config
    else:
        # Configure output directory (production path)
        try:
            configure_output_directory(args.output)
        except ValueError as e:
            print(f"\nERROR: {e}")
            print("Workflow aborted.\n")
            sys.exit(1)

    # Run workflow
    exit_code = run_workflow(data_reader)
    sys.exit(exit_code)


if __name__ == "__main__":
    main()
