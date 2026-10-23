import os
import re
import time
import xml.etree.ElementTree as ET

import pandas as pd

from concurrent.futures import ThreadPoolExecutor, as_completed
from pathlib import Path

# ============================================================
# Configuration
# ============================================================

LOT_PATTERN = re.compile(r"^(61|62)\w{3}\.\w{3}")

WINDOWS_EVENT_TAG = "{http://schemas.microsoft.com/win/2004/08/events/event}Event"

OUTPUT_COLUMNS = [
    "FILE_NAME",
    "recipeId",
    "itemInstanceId",
    "mainLotId",
    "designator",
    "partId",
    "componentId",
    "row",
    "column",
    "feederType",
    "materialSupplyArea",
    "trackId",
    "feeder_partId",
    "reelId",
    "component_lotId",
    "Flag",
    "SUBFOLDER",
]


# ============================================================
# XML Helpers
# ============================================================


def compute_lot_flag(lot_id: str) -> int:
    if not lot_id:
        return 1

    return 0 if LOT_PATTERN.search(lot_id.strip()) else 1


def get_local_tag_name(tag: str) -> str:
    return tag.rsplit("}", 1)[-1]


def find_first_element(parent, tag_name: str):
    if parent is None:
        return None

    for child in parent:
        if get_local_tag_name(child.tag) == tag_name:
            return child

    return None


def find_all_elements(parent, tag_name: str):
    if parent is None:
        return []

    return [child for child in parent if get_local_tag_name(child.tag) == tag_name]


def find_first_anywhere(root, tag_name: str):
    for element in root.iter():
        if get_local_tag_name(element.tag) == tag_name:
            return element

    return None


def find_all_anywhere(root, tag_name: str):
    return [
        element
        for element in root.iter()
        if get_local_tag_name(element.tag) == tag_name
    ]


# ============================================================
# XML Placement Extraction
# ============================================================


def parse_placement_xml(
    file_path: Path,
    root: ET.Element,
    subfolder: str,
) -> list:
    """
    Extract placement records from an already-parsed XML tree.

    The XML file is not opened or parsed again.
    """

    records = []

    item_info = find_first_anywhere(root, "itemInfo")

    if item_info is not None:
        recipe_id = item_info.get("recipeId", "")
        item_instance_id = item_info.get("itemInstanceId", "")
        lot_id_main = item_info.get("lotId", "")
    else:
        recipe_id = ""
        item_instance_id = ""
        lot_id_main = ""

    summaries = find_all_anywhere(root, "MaterialHandlerSummary")

    for summary in summaries:
        handler = find_first_element(summary, "MaterialHandler")

        if handler is None:
            continue

        feeder_type = handler.get("feederType", "")
        supply_area = handler.get("materialSupplyArea", "")
        track_id = handler.get("trackId", "")
        feeder_part_id = handler.get("partId", "")

        component_info = find_first_element(summary, "Component")

        if component_info is not None:
            reel_id = component_info.get("reelId", "")
            component_lot_id = component_info.get("lotId", "")
        else:
            reel_id = ""
            component_lot_id = ""

        flag_value = compute_lot_flag(component_lot_id)

        placement_detail = find_first_element(summary, "PlacementDetail")

        if placement_detail is None:
            continue

        placement_components = find_all_elements(placement_detail, "Component")

        for component in placement_components:
            records.append(
                {
                    "FILE_NAME": file_path.name,
                    "recipeId": recipe_id,
                    "itemInstanceId": item_instance_id,
                    "mainLotId": lot_id_main,
                    "designator": component.get("designator", ""),
                    "partId": component.get("partId", ""),
                    "componentId": component.get("componentId", ""),
                    "row": component.get("row", ""),
                    "column": component.get("column", ""),
                    "feederType": feeder_type,
                    "materialSupplyArea": supply_area,
                    "trackId": track_id,
                    "feeder_partId": feeder_part_id,
                    "reelId": reel_id,
                    "component_lotId": component_lot_id,
                    "Flag": flag_value,
                    "SUBFOLDER": subfolder,
                }
            )

    return records


# ============================================================
# Single-Pass XML Processing
# ============================================================


def process_one_file(
    xml_file: Path,
    subfolder: str,
    target_recipes: set,
) -> tuple:
    """
    Process an XML file with only one file open and one parser.

    Nonmatching files:
        Stop reading immediately after finding itemInfo.

    Matching files:
        Continue the same parser until the XML is complete.

    No seek(0), second ET.parse(), or additional file open.
    """

    with xml_file.open("rb") as xml_stream:
        parser = ET.iterparse(
            xml_stream,
            events=("start",),
        )

        for index, (_, element) in enumerate(parser):

            # Skip Windows Event XML exports.
            if index == 0 and element.tag == WINDOWS_EVENT_TAG:
                return False, []

            if get_local_tag_name(element.tag) != "itemInfo":
                continue

            recipe_id = element.get("recipeId", "").strip()

            if recipe_id not in target_recipes:
                return False, []

            # Recipe matches: continue parsing this same file.
            break

        else:
            # No itemInfo found.
            return False, []

        # Complete the existing parser without reopening the file.
        for _ in parser:
            pass

        root = parser.root

    records = parse_placement_xml(
        xml_file,
        root,
        subfolder,
    )

    return True, records


# ============================================================
# Fast Network Directory Enumeration
# ============================================================


def get_xml_files(folder_path: Path) -> list:
    """
    Find XML files using os.scandir.

    Avoid per-file stat() calls and unnecessary metadata checks.

    Entries ending in .xml are treated as file candidates.
    Invalid candidates are handled by process_one_file.
    """

    xml_files = []

    try:
        with os.scandir(folder_path) as entries:
            for entry in entries:
                if entry.name.lower().endswith(".xml"):
                    xml_files.append(Path(entry.path))

    except OSError as error:
        raise OSError(f"Could not scan folder {folder_path}: {error}") from error

    return xml_files


# ============================================================
# Concurrent Network Processing
# ============================================================


def process_target_subfolders(
    base_path: str,
    target_folders: list,
    target_recipes,
    output_csv: str,
    max_workers: int = 32,
):
    """
    Process XML files concurrently with a shared thread pool.

    Optimizations:
        - Concurrent SMB file operations
        - Single-pass XML parsing
        - Early recipe rejection
        - No persistent cache
        - No per-file stat() calls
        - One ThreadPoolExecutor for all folders
        - pandas retained for CSV output
    """

    start_time = time.perf_counter()

    base = Path(base_path)

    target_recipes = {
        recipe.strip() for recipe in target_recipes if recipe and recipe.strip()
    }

    if max_workers < 1:
        raise ValueError("max_workers must be at least 1")

    all_data = []

    total_files = 0
    total_matching_files = 0
    total_failed_files = 0

    total_discovery_time = 0.0
    total_processing_time = 0.0

    print("=" * 60)
    print("XML NETWORK PROCESSING")
    print("=" * 60)

    print(f"[INFO] Source: {base}")
    print(f"[INFO] Worker threads: {max_workers}")
    print(f"[INFO] Target recipes: {target_recipes}")

    # Reuse the thread pool across all target folders.
    with ThreadPoolExecutor(max_workers=max_workers) as executor:

        for subfolder in target_folders:

            folder_path = base / subfolder

            # ------------------------------------------------
            # Directory Enumeration
            # ------------------------------------------------

            discovery_start = time.perf_counter()

            try:
                xml_files = get_xml_files(folder_path)

            except OSError as error:
                print(f"[ERROR] {error}")
                continue

            discovery_elapsed = time.perf_counter() - discovery_start

            total_discovery_time += discovery_elapsed

            file_count = len(xml_files)
            total_files += file_count

            print(f"\n[INFO] Folder: {subfolder}")
            print(f"[INFO] XML files found: {file_count}")

            print(f"[TIMING] Directory enumeration: " f"{discovery_elapsed:.3f}s")

            if not xml_files:
                continue

            # ------------------------------------------------
            # Concurrent XML Processing
            # ------------------------------------------------

            processing_start = time.perf_counter()

            folder_matching_files = 0
            folder_record_count = 0
            folder_failed_files = 0

            futures = {
                executor.submit(
                    process_one_file,
                    xml_file,
                    subfolder,
                    target_recipes,
                ): xml_file
                for xml_file in xml_files
            }

            for processed_count, future in enumerate(
                as_completed(futures),
                start=1,
            ):

                xml_file = futures[future]

                try:
                    matched, file_records = future.result()

                    if matched:
                        folder_matching_files += 1
                        total_matching_files += 1

                    if file_records:
                        all_data.extend(file_records)
                        folder_record_count += len(file_records)

                except Exception as error:
                    folder_failed_files += 1
                    total_failed_files += 1

                    print(f"[ERROR] Failed processing " f"{xml_file.name}: {error}")

                if processed_count % 500 == 0 or processed_count == file_count:
                    print(
                        f"[INFO] {subfolder}: "
                        f"processed {processed_count}/{file_count}; "
                        f"matched {folder_matching_files}"
                    )

            processing_elapsed = time.perf_counter() - processing_start

            total_processing_time += processing_elapsed

            # ------------------------------------------------
            # Folder Summary
            # ------------------------------------------------

            print(f"\n[INFO] Folder completed: {subfolder}")

            print(f"[INFO] Matching XML files: " f"{folder_matching_files}")

            print(f"[INFO] Extracted records: " f"{folder_record_count}")

            print(f"[TIMING] XML processing: " f"{processing_elapsed:.3f}s")

            if processing_elapsed > 0:
                throughput = file_count / processing_elapsed

                print(f"[TIMING] Processing throughput: " f"{throughput:.2f} files/sec")

            if folder_failed_files:
                print(f"[WARNING] Failed XML files: " f"{folder_failed_files}")

    # ========================================================
    # pandas CSV Output
    # ========================================================

    output_path = Path(output_csv)

    temporary_output = output_path.with_name(output_path.name + ".tmp")

    dataframe = pd.DataFrame(
        all_data,
        columns=OUTPUT_COLUMNS,
    )

    csv_start = time.perf_counter()

    try:
        output_path.parent.mkdir(
            parents=True,
            exist_ok=True,
        )

        dataframe.to_csv(
            temporary_output,
            index=False,
        )

        # Replace destination only after writing completes.
        os.replace(
            temporary_output,
            output_path,
        )

    except OSError as error:

        print(f"[ERROR] Could not save output report " f"{output_path}: {error}")

        try:
            if temporary_output.exists():
                temporary_output.unlink()

        except OSError:
            pass

        return

    csv_elapsed = time.perf_counter() - csv_start

    total_elapsed = time.perf_counter() - start_time

    # ========================================================
    # Final Performance Summary
    # ========================================================

    print("\n" + "=" * 60)
    print("PROCESSING SUMMARY")
    print("=" * 60)

    if dataframe.empty:
        print("[WARNING] No records matched your criteria.")

    elif total_failed_files:
        print("[WARNING] Completed with failed files. " "Report may be incomplete.")

    else:
        print("[SUCCESS] Processing completed.")

    print(f"[INFO] XML files inspected: {total_files}")

    print(f"[INFO] Matching XML files: " f"{total_matching_files}")

    print(f"[INFO] Extracted records: " f"{len(dataframe)}")

    print(f"[INFO] Failed XML files: " f"{total_failed_files}")

    print("\n" + "-" * 60)
    print("PERFORMANCE BREAKDOWN")
    print("-" * 60)

    print(f"[TIMING] Directory enumeration: " f"{total_discovery_time:.3f}s")

    print(f"[TIMING] XML network processing: " f"{total_processing_time:.3f}s")

    print(f"[TIMING] CSV output: " f"{csv_elapsed:.3f}s")

    print(f"[TIMING] Total execution: " f"{total_elapsed:.3f}s")

    if total_processing_time > 0:
        print(
            f"[TIMING] XML processing throughput: "
            f"{total_files / total_processing_time:.2f} files/sec"
        )

    print(f"\n[SUCCESS] Saved report to: {output_path}")


# ============================================================
# Entry Point
# ============================================================

if __name__ == "__main__":

    INPUT_PATH = r"\\KMatshfs.intel.com\KMATAnalysis$\MAOATM\KuAT\AE\reflow\test-jiale"

    # XML files sit directly in INPUT_PATH.
    TARGET_SUBFOLDERS = [""]

    TARGET_RECIPES = {
        "SQUARE_HILL_L_JDC_CS",
        "SQUARE HILL_JDC_VF_CS",
    }

    OUTPUT_FILE = (
        r"\\KMatshfs.intel.com\KMATAnalysis$"
        r"\MAOATM\KuAT\AE\reflow\test-jiale\IFS Supplier Mixing.csv"
    )

    process_target_subfolders(
        base_path=INPUT_PATH,
        target_folders=TARGET_SUBFOLDERS,
        target_recipes=TARGET_RECIPES,
        output_csv=OUTPUT_FILE,
        max_workers=32,
    )
