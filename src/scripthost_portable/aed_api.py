"""Linux IAM bootstrap and AED processing; called only inside an isolated SPF worker."""

from __future__ import annotations

import contextlib
import csv
import importlib
import io
import json
import logging
import ntpath
import os
import re
import shutil
import sys
import uuid
from datetime import UTC, datetime
from pathlib import Path

IAM_JOB = "ICMPCS_CWFNCO_CSR_IAM"
# Windows opens the UNC path natively; Linux reads it over SMB with CIFS_USERNAME/CIFS_PASSWORD.
DEFAULT_ROOT = r"\\AZATSHFS.intel.com\AZATAnalysis$\MAOATM\Config\VF_POR_Cfg\ICM_PCS"
# One job serves one site; same mapping as CSR_IAM.py.
SITE_FACILITY = {
    "CD": "A48_PROD_21", "PG": "A12_PROD_0", "KM": "A15_PROD_21",
    "VN": "A90_PROD_21", "CR": "A61_PROD_4",
}
_TASK = re.compile(r"^\s*/UTILITIES\s*=\s*\{AED\}(?:\s|$)", re.I | re.M)
_NAME = re.compile(r"[A-Za-z_][A-Za-z0-9_]*\Z")
_ID = re.compile(r"[A-Za-z0-9_.-]+\Z")
_PROTECTED = {
    "PATH", "HOME", "USERPROFILE", "SYSTEMROOT", "COMSPEC", "TEMP", "TMP",
    "PYTHONPATH", "PYTHONHOME", "LD_PRELOAD", "LD_LIBRARY_PATH", "ENV_MODE",
    "ICMPCS_ROOT", "AED_CONFIG_PATH", "AED_CA_BUNDLE", "CONFIG_SOURCE",
}


def _safe_name(name: str) -> None:
    upper = name.upper()
    if (
        not _NAME.fullmatch(name) or upper in _PROTECTED
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


def _read_text(path: str) -> str:
    if os.name != "nt" and _is_unc(path):
        import smbclient

        with smbclient.open_file(
            path, encoding="utf-8-sig", newline="",
            username=os.environ.get("CIFS_USERNAME"), password=os.environ.get("CIFS_PASSWORD"),
        ) as stream:
            return stream.read()
    with open(path, encoding="utf-8-sig", newline="") as stream:
        return stream.read()


def parse_config(text: str) -> dict[str, str]:
    """Read the existing icmpcs/parameter/value[/comment] text format."""
    reader = csv.reader(io.StringIO(text.lstrip("\ufeff")), strict=True)
    headers = next(reader, [])
    if len(headers) < 3 or [v.strip().lower() for v in headers[1:3]] != [
        "parameter", "value",
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
        record = {key: value for key, value in row.items() if key not in {
            lot_column, names.get("FACILITY"), names.get("OUT_DATE"),
        }}
        record.update(
            LOT=row[lot_column].strip(),
            FACILITY=row.get(names.get("FACILITY", ""), "").strip(),
            OUT_DATE=row.get(names.get("OUT_DATE", ""), ""),
        )
        if record["LOT"]:
            records.append(record)
    return records


def _save_history(path: str, records: list[dict[str, str]]) -> None:
    # ponytail: one writer per job/site; add locking if overlapping runs are needed.
    fields = list(dict.fromkeys(
        ["FACILITY", "LOT", "OUT_DATE"] + [key for record in records for key in record]
    ))
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


def _load_client(workdir: Path):
    # Both distributions parse sys.argv during import. Do not expose SPF worker arguments.
    previous = sys.argv
    try:
        sys.argv = ["aed-integration"]
        with contextlib.redirect_stdout(io.StringIO()):
            client = importlib.import_module("aed_client")
    finally:
        sys.argv = previous
    try:
        importlib.import_module("aed_client.sessions.apikey.Authentication")
        apikey = True
    except ModuleNotFoundError:
        apikey = False
    if os.name != "nt" and not apikey:
        raise RuntimeError("Linux requires aed-client-apikey, not aed-client-winauth")
    if apikey:
        if not os.environ.get("X_API_KEY"):
            raise ValueError("X_API_KEY is required for the AED API-key client")
        # The SOIMS 6.2.0 client resolves this certificate relative to the job cwd.
        certificate = Path(os.environ.get(
            "AED_CA_BUNDLE", str(Path(__file__).with_name("IntelSHA256RootCA-Base64.crt"))
        )).resolve(strict=True)
        destination = workdir / "IntelSHA256RootCA-Base64.crt"
        if certificate != destination:
            shutil.copyfile(certificate, destination)
    client.environment, client.protocol, client.is_debug = "PROD", "HTTPS", False
    return client


def prepare_job(workdir: Path, script: str, script_path: str | None = None) -> str:
    """Prepare only scripts opting into {AED}; ordinary SPF jobs are unchanged."""
    if not _TASK.search(script):
        return script
    site = _identifier(os.environ.get("SITE", "").strip().upper(), "SITE")
    # The share folder is named after the script, e.g. ICMPCS_CWFNCO_CSR_IAM.txt.
    named = Path(script_path).name.split(".")[0] if script_path else IAM_JOB
    job = _identifier(os.environ.get("SFOLDER", named).strip(), "SFOLDER")
    mode = os.environ.get("ENV_MODE", "test").lower()
    if mode not in {"test", "prod"}:
        raise ValueError("ENV_MODE must be test or prod")
    root = _absolute(os.environ.get("ICMPCS_ROOT", DEFAULT_ROOT).rstrip("\\/"))
    join = ntpath.join if _is_unc(root) else os.path.join
    supplied = parse_config(_read_text(join(root, job, site, "CONFIG", "config.txt")))
    facility = SITE_FACILITY.get(site, "")
    defaults = {
        # Bracketed node lets SPF substitute @[]@; plain "KM.MARS" silently falls back to mc_1_.
        "SITE": site, "SFOLDER": job,
        "MARS": f"{site}.[{facility}.].MARS",
        "ARIES": f"{site}.ARIES", "AED_FACILITY": facility,
        "dEmail": "", "ATTR_LIST": "", "SKIP_OPERATION": "",
        "AED_ENABLED": "Y", "HIST_PATH": str(workdir.resolve() / site / "HIST" / "HIST.txt"),
    }
    if job == IAM_JOB:
        defaults.update(ATTR_LIST="1064", SKIP_OPERATION="2446")
    # Match legacy macro names case-insensitively, retaining their original spelling.
    for name, value in supplied.items():
        previous = next((key for key in defaults if key.upper() == name.upper()), None)
        if previous is not None:
            del defaults[previous]
        defaults[name] = value
    values = defaults
    for name in values:
        overrides = [value for key, value in os.environ.items() if key.upper() == name.upper()]
        if len(set(overrides)) > 1:
            raise ValueError(f"Conflicting environment overrides for {name}")
        if overrides:
            values[name] = overrides[0]
    # Bootstrap identity comes from deployment selection, not remote file contents.
    for identity, value in (("SITE", site), ("SFOLDER", job)):
        key = next(key for key in values if key.upper() == identity)
        values[key] = value
    normalized = {key.upper(): value for key, value in values.items()}
    for required in ("MARS", "ARIES", "AED_FACILITY", "DEMAIL", "ATTR_LIST",
                     "SKIP_OPERATION", "HIST_PATH"):
        if not normalized.get(required):
            raise ValueError(f"Missing required configuration parameter: {required}")
    _identifier(normalized["AED_FACILITY"], "AED_FACILITY")
    for attribute in normalized["ATTR_LIST"].split(","):
        _identifier(attribute.strip(), "attribute ID")
    _identifier(normalized["SKIP_OPERATION"], "SKIP_OPERATION")
    if normalized["AED_ENABLED"].upper() not in {"Y", "N"}:
        raise ValueError("AED_ENABLED must be Y or N")
    _read_history(normalized["HIST_PATH"])
    _load_client(workdir)
    os.environ["ENV_MODE"] = mode
    snapshot = workdir / "config.json"
    snapshot.write_text(json.dumps(values, indent=2) + "\n", encoding="utf-8")
    with (workdir / "configsets.csv").open("w", encoding="utf-8", newline="") as stream:
        writer = csv.DictWriter(stream, fieldnames=list(values))
        writer.writeheader()
        writer.writerow(values)
    os.environ.update(values)
    os.environ["AED_CONFIG_PATH"] = str(snapshot)
    print(f"AED configuration loaded for {job}/{site}")
    # Portable entry enters after SPFManager.main's environment substitution.
    from .runtime import _spf_manager_type

    return _spf_manager_type()().Substitute_Env(script)


def _attributes(client, facility: str, lot: str) -> list[dict]:
    client.facility = facility
    with contextlib.redirect_stdout(io.StringIO()):
        response = client.ManufacturingService().lot_status(lot)
    response.raise_for_status()
    payload = response.json()
    if not isinstance(payload, dict) or payload.get("Success") is not True:
        raise RuntimeError("AED lot_status did not succeed")
    result = payload.get("Result")
    if result is None:
        return []
    if isinstance(result, dict) and result.get("Attributes") is None and "Attributes" in result:
        return []
    if not isinstance(result, dict) or not isinstance(result.get("Attributes"), list):
        raise ValueError("Malformed AED lot_status attributes")
    attributes = result["Attributes"]
    if any(not isinstance(attr, dict) or not isinstance(attr.get("ID"), str)
           for attr in attributes):
        raise ValueError("Malformed AED attribute record")
    return attributes


def process_candidates(candidates_path: str, logger=None) -> None:
    """Run once per distinct facility/lot and persist only confirmed production success."""
    logger = logger or logging.getLogger(__name__)
    config = {key.upper(): value for key, value in json.loads(
        Path(os.environ["AED_CONFIG_PATH"]).read_text(encoding="utf-8")
    ).items()}
    with Path(candidates_path).open(encoding="utf-8-sig", newline="") as stream:
        reader = csv.DictReader(stream, strict=True)
        if reader.fieldnames != ["FACILITY", "LOT"]:
            raise ValueError("AED candidates require exactly FACILITY,LOT columns")
        candidates = []
        for row in reader:
            if None in row or any(value is None for value in row.values()):
                raise ValueError("Malformed AED candidate row")
            candidates.append((
                _identifier(row["FACILITY"].strip(), "FACILITY"),
                _identifier(row["LOT"].strip(), "LOT"),
            ))
    history = _read_history(config["HIST_PATH"])
    previous = {(row["FACILITY"], row["LOT"]) for row in history}
    attributes = [value.strip() for value in config["ATTR_LIST"].split(",")]
    target = config["SKIP_OPERATION"]
    mode = os.environ.get("ENV_MODE", "test")
    client = _load_client(Path.cwd())
    import aed_updater

    aed_updater.ENV_MODE = mode
    outcomes = []
    failed = False
    for facility, lot in dict.fromkeys(candidates):
        outcome = {"FACILITY": facility, "LOT": lot, "STATUS": "", "ERROR": ""}
        try:
            if config["AED_ENABLED"].upper() != "Y":
                outcome["STATUS"] = "disabled"
            elif (facility, lot) in previous or ("", lot) in previous:
                outcome["STATUS"] = "history_excluded"
            else:
                before = _attributes(client, facility, lot)
                correct = any(attr["ID"] in attributes and attr.get("Value") == target
                              for attr in before)
                changed = aed_updater.update_lot_attributes(
                    facility, lot, attributes, target, logger,
                )
                after = _attributes(client, facility, lot)
                confirmed = any(attr["ID"] in attributes and attr.get("Value") == target
                                for attr in after)
                if mode != "prod":
                    outcome["STATUS"] = "dry_run"
                elif confirmed:
                    outcome["STATUS"] = "already_correct" if correct else "updated"
                    record = {"FACILITY": facility, "LOT": lot,
                              "OUT_DATE": datetime.now(UTC).isoformat()}
                    _save_history(config["HIST_PATH"], history + [record])
                    history.append(record)
                    previous.add((facility, lot))
                elif changed == "Y" or correct:
                    raise RuntimeError("AED target attribute could not be verified")
                else:
                    outcome["STATUS"] = "skipped"
        except Exception as exc:
            # Do not put arbitrary service response bodies or credentials into audit files.
            outcome["STATUS"], outcome["ERROR"] = "failed", type(exc).__name__
            failed = True
            logger.error("AED processing failed for facility=%s lot=%s (%s)",
                         facility, lot, type(exc).__name__)
        outcomes.append(outcome)
        print(f"AED {facility}/{lot}: {outcome['STATUS']}")
        with Path("AED_results.csv").open("w", encoding="utf-8", newline="") as stream:
            writer = csv.DictWriter(stream, fieldnames=["FACILITY", "LOT", "STATUS", "ERROR"])
            writer.writeheader()
            writer.writerows(outcomes)
    if not outcomes:
        Path("AED_results.csv").write_text("FACILITY,LOT,STATUS,ERROR\n", encoding="utf-8")
    if failed:
        raise RuntimeError("One or more AED lots failed; see AED_results.csv")
