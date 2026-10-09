"""Focused AED validation/history adapted from AED-integration aed_api.py:97-183.

Config parsing and atomic single-writer history are extracted unchanged;
bootstrap/candidate processing use explicit values and services instead of ambient mutation.
See test_direct_aed.py for offline parity and production-guard checks.
"""
from __future__ import annotations
import contextlib
import csv
import io
import json
import logging
import ntpath
import os
import re
import uuid
from datetime import UTC, datetime
from pathlib import Path
from vg2c.runtime.values import job_path

IAM_JOB = "ICMPCS_CWFNCO_CSR_IAM"

DEFAULT_ROOT = r"\\AZATSHFS.intel.com\AZATAnalysis$\MAOATM\Config\VF_POR_Cfg\ICM_PCS"

SITE_FACILITY = {
    "CD": "A48_PROD_21",
    "PG": "A12_PROD_0",
    "KM": "A15_PROD_21",
    "VN": "A90_PROD_21",
    "CR": "A61_PROD_4",
}

_NAME = re.compile(r"[A-Za-z_][A-Za-z0-9_]*\Z")

_ID = re.compile(r"[A-Za-z0-9_.-]+\Z")

_PROTECTED = {
    "PATH",
    "HOME",
    "USERPROFILE",
    "SYSTEMROOT",
    "COMSPEC",
    "TEMP",
    "TMP",
    "PYTHONPATH",
    "PYTHONHOME",
    "LD_PRELOAD",
    "LD_LIBRARY_PATH",
    "ENV_MODE",
    "ICMPCS_ROOT",
    "AED_CONFIG_PATH",
    "AED_CA_BUNDLE",
    "CONFIG_SOURCE",
}

def _safe_name(name: str) -> None:
    upper = name.upper()
    if (
        not _NAME.fullmatch(name)
        or upper in _PROTECTED
        or upper.startswith(("DATASYNCX_", "CIFS_"))
        or any(word in upper for word in ("PASSWORD", "SECRET", "TOKEN", "API_KEY"))
    ):
        raise ValueError(f"Unsafe configuration parameter: {name!r}")

def _identifier(value: str, name: str) -> str:
    if not _ID.fullmatch(value) or value in {".", ".."}:
        raise ValueError(f"Invalid {name}: expected an identifier")
    return value

def _is_unc(path: str) -> bool:
    return path.startswith(("\\\\", "//"))

def _absolute(path: str) -> str:
    if not (_is_unc(path) or os.path.isabs(path)):
        raise ValueError("ICMPCS_ROOT/HIST_PATH must be absolute paths")
    return path

def parse_config(text: str) -> dict[str, str]:
    """Read the existing icmpcs/parameter/value[/comment] text format."""
    reader = csv.reader(io.StringIO(text.lstrip("\ufeff")), strict=True)
    headers = next(reader, [])
    if len(headers) < 3 or [v.strip().lower() for v in headers[1:3]] != [
        "parameter",
        "value",
    ]:
        raise ValueError("config.txt requires group,parameter,value columns")
    values: dict[str, str] = {}
    names: dict[str, str] = {}
    for row in reader:
        if not row or not any(v.strip() for v in row):
            continue
        if len(row) < 3:
            raise ValueError(f"Incomplete config.txt row {reader.line_num}")
        if row[0].strip().upper() != "ICMPCS":
            continue
        name, value = row[1].strip(), row[2].strip()
        _safe_name(name)
        previous = names.get(name.upper())
        if previous is not None and (previous != name or values[previous] != value):
            raise ValueError(f"Ambiguous configuration parameter: {name}")
        names[name.upper()] = name
        values[name] = value
    if not values:
        raise ValueError("config.txt contains no ICMPCS parameters")
    return values

def _read_history(path: str) -> list[dict[str, str]]:
    try:
        with open(_absolute(path), encoding="utf-8-sig", newline="") as stream:
            text = stream.read()
    except FileNotFoundError:
        return []
    if not text.strip():
        return []
    reader = csv.DictReader(io.StringIO(text), strict=True)
    headers = reader.fieldnames or []
    names = {name.strip().upper(): name for name in headers}
    lot_column = names.get("LOT") or names.get("LOT_NCORISK")
    if not lot_column or len(names) != len(headers):
        raise ValueError("History requires an unambiguous LOT or Lot_NCORisk column")
    records = []
    for row in reader:
        if None in row or any(value is None for value in row.values()):
            raise ValueError("Malformed history row")
        record = {
            key: value
            for key, value in row.items()
            if key
            not in {
                lot_column,
                names.get("FACILITY"),
                names.get("OUT_DATE"),
            }
        }
        record.update(
            LOT=row[lot_column].strip(),
            FACILITY=row.get(names.get("FACILITY", ""), "").strip(),
            OUT_DATE=row.get(names.get("OUT_DATE", ""), ""),
        )
        if record["LOT"]:
            records.append(record)
    return records

def _save_history(path: str, records: list[dict[str, str]]) -> None:
    # shortcut: one writer per job/site; add locking if overlapping runs are needed.
    fields = list(
        dict.fromkeys(
            ["FACILITY", "LOT", "OUT_DATE"]
            + [key for record in records for key in record]
        )
    )
    temporary = _absolute(path) + "." + uuid.uuid4().hex + ".tmp"
    os.makedirs(os.path.dirname(path), exist_ok=True)
    try:
        with open(temporary, "x", encoding="utf-8", newline="") as stream:
            writer = csv.DictWriter(stream, fieldnames=fields)
            writer.writeheader()
            writer.writerows(records)
        os.replace(temporary, path)
    finally:
        with contextlib.suppress(FileNotFoundError):
            os.remove(temporary)


def bootstrap_aed(*, workdir, source_name=IAM_JOB, environ=None, read_text=None):
    """Adapted from AED-integration aed_api.prepare_job:219-301; no source scan or env writes."""
    environment = dict(os.environ if environ is None else environ)
    site = _identifier(environment.get("SITE", "").strip().upper(), "SITE")
    job = _identifier(environment.get("SFOLDER", source_name).strip(), "SFOLDER")
    mode = environment.get("ENV_MODE", "test").lower()
    if mode not in {"test", "prod"}:
        raise ValueError("ENV_MODE must be test or prod")
    root = _absolute(environment.get("ICMPCS_ROOT", DEFAULT_ROOT).rstrip("\\/"))
    join = ntpath.join if _is_unc(root) else os.path.join
    config_path = join(root, job, site, "CONFIG", "config.txt")
    if read_text is None:
        if os.name != "nt" and _is_unc(config_path):
            raise RuntimeError("Linux UNC configuration requires an explicit authenticated read_text provider")
        read_text = lambda path: Path(path).read_text(encoding="utf-8-sig")
    supplied = parse_config(read_text(config_path))
    facility = SITE_FACILITY.get(site, "")
    values = {
        "SITE": site, "SFOLDER": job, "MARS": f"{site}.[{facility}.].MARS", "ARIES": f"{site}.ARIES",
        "AED_FACILITY": facility, "dEmail": "", "ATTR_LIST": "1064" if job == IAM_JOB else "",
        "SKIP_OPERATION": "2446" if job == IAM_JOB else "", "AED_ENABLED": "Y",
        "HIST_PATH": str(Path(workdir).resolve() / site / "HIST" / "HIST.txt"),
    }
    for name, value in supplied.items():
        previous = next((key for key in values if key.upper() == name.upper()), None)
        if previous is not None:
            del values[previous]
        values[name] = value
    for name in values:
        overrides = [value for key, value in environment.items() if key.upper() == name.upper()]
        if len(set(overrides)) > 1:
            raise ValueError(f"Conflicting environment overrides for {name}")
        if overrides:
            values[name] = overrides[0]
    for identity, value in (("SITE", site), ("SFOLDER", job)):
        values[next(key for key in values if key.upper() == identity)] = value
    normalized = {key.upper(): value for key, value in values.items()}
    for required in ("MARS", "ARIES", "AED_FACILITY", "DEMAIL", "ATTR_LIST", "SKIP_OPERATION", "HIST_PATH"):
        if not normalized.get(required):
            raise ValueError(f"Missing required configuration parameter: {required}")
    _identifier(normalized["AED_FACILITY"], "AED_FACILITY")
    for attribute in normalized["ATTR_LIST"].split(","):
        _identifier(attribute.strip(), "attribute ID")
    _identifier(normalized["SKIP_OPERATION"], "SKIP_OPERATION")
    if normalized["AED_ENABLED"].upper() not in {"Y", "N"}:
        raise ValueError("AED_ENABLED must be Y or N")
    _read_history(normalized["HIST_PATH"])
    workdir = Path(workdir).resolve()
    workdir.mkdir(parents=True, exist_ok=True)
    (workdir / "config.json").write_text(json.dumps(values, indent=2) + "\n", encoding="utf-8")
    with (workdir / "configsets.csv").open("w", encoding="utf-8", newline="") as stream:
        writer = csv.DictWriter(stream, fieldnames=list(values))
        writer.writeheader()
        writer.writerow(values)
    return {**normalized, "ENV_MODE": mode}


def process_candidates(candidates_path, *, config, workdir, service_factory, logger=None):
    """Adapted from AED-integration aed_api.process_candidates:332-430; writes require prod + readback."""
    logger = logger or logging.getLogger(__name__)
    config = {key.upper(): value for key, value in config.items()}
    mode = config.get("ENV_MODE", "test")
    if mode not in {"test", "prod"}:
        raise ValueError("ENV_MODE must be test or prod")
    with job_path(candidates_path, workdir).open(encoding="utf-8-sig", newline="") as stream:
        reader = csv.DictReader(stream, strict=True)
        if reader.fieldnames != ["FACILITY", "LOT"]:
            raise ValueError("AED candidates require exactly FACILITY,LOT columns")
        candidates = []
        for row in reader:
            if None in row or any(value is None for value in row.values()):
                raise ValueError("Malformed AED candidate row")
            candidates.append((_identifier(row["FACILITY"].strip(), "FACILITY"),
                               _identifier(row["LOT"].strip(), "LOT")))
    history = _read_history(config["HIST_PATH"])
    previous = {(row["FACILITY"], row["LOT"]) for row in history}
    attributes = [value.strip() for value in config["ATTR_LIST"].split(",")]
    target = config["SKIP_OPERATION"]
    for attribute in attributes:
        _identifier(attribute, "attribute ID")
    _identifier(target, "SKIP_OPERATION")
    if config["AED_ENABLED"].upper() not in {"Y", "N"}:
        raise ValueError("AED_ENABLED must be Y or N")
    outcomes = []
    for facility, lot in dict.fromkeys(candidates):
        outcome = {"FACILITY": facility, "LOT": lot, "STATUS": "", "ERROR": ""}
        try:
            if config["AED_ENABLED"].upper() != "Y":
                outcome["STATUS"] = "disabled"
            elif (facility, lot) in previous or ("", lot) in previous:
                outcome["STATUS"] = "history_excluded"
            else:
                if service_factory is None:
                    raise RuntimeError("AED requires an explicit service factory with API-key authentication and an absolute CA bundle")
                service = service_factory(facility)
                before = _attributes(service, lot)
                correct = any(attr["ID"] in attributes and attr.get("Value") == target for attr in before)
                changed = False
                # Adapted from AED-integration aed_updater.update_lot_attributes:43-79.
                # Update only the first requested missing/None attribute, and never an empty lot record.
                if mode == "prod" and not correct and before:
                    for attribute_id in attributes:
                        attribute = next((item for item in before if item["ID"] == attribute_id), None)
                        if attribute is None or attribute.get("Value") is None:
                            if service.set_attribute(lot, attribute_id, target) is not True:
                                raise RuntimeError("AED lot_set_attr did not succeed")
                            changed = True
                            break
                after = _attributes(service, lot)
                confirmed = any(attr["ID"] in attributes and attr.get("Value") == target for attr in after)
                if mode == "test":
                    outcome["STATUS"] = "dry_run"
                elif confirmed:
                    outcome["STATUS"] = "already_correct" if correct else "updated"
                    record = {"FACILITY": facility, "LOT": lot, "OUT_DATE": datetime.now(UTC).isoformat()}
                    _save_history(config["HIST_PATH"], history + [record])
                    history.append(record)
                    previous.add((facility, lot))
                elif changed or correct:
                    raise RuntimeError("AED target attribute could not be verified")
                else:
                    outcome["STATUS"] = "skipped"
        except Exception as exc:
            outcome["STATUS"], outcome["ERROR"] = "failed", type(exc).__name__
            logger.error("AED processing failed for facility=%s lot=%s (%s)", facility, lot, type(exc).__name__)
        outcomes.append(outcome)
    destination = job_path("AED_results.csv", workdir)
    destination.parent.mkdir(parents=True, exist_ok=True)
    with destination.open("w", encoding="utf-8", newline="") as stream:
        writer = csv.DictWriter(stream, fieldnames=["FACILITY", "LOT", "STATUS", "ERROR"])
        writer.writeheader()
        writer.writerows(outcomes)
    if any(item["STATUS"] == "failed" for item in outcomes):
        raise RuntimeError("One or more AED lots failed; see AED_results.csv")
    return outcomes


def _attributes(service, lot):
    attributes = service.attributes(lot)
    if not isinstance(attributes, list) or any(not isinstance(attr, dict) or not isinstance(attr.get("ID"), str) for attr in attributes):
        raise ValueError("Malformed AED attribute record")
    return attributes
