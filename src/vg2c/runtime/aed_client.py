"""AED client, attribute updates, and validated CSR rows; optional SDK, no app imports."""

from __future__ import annotations

import contextlib
import csv
import importlib
from importlib import metadata
import io
import json
import os
from pathlib import Path
import re
import sys
from urllib.parse import quote

from vg2c.runtime.values import job_path


def identifier(value, name):
    if not isinstance(value, str) or not re.fullmatch(r"[A-Za-z0-9_.-]+", value) or value in {".", ".."}:
        raise ValueError(f"Invalid {name}: expected a nonempty identifier")
    return value


def _load_sdk():
    try:
        metadata.version("aed-client-winauth")
    except metadata.PackageNotFoundError:
        pass
    else:
        raise RuntimeError("Use an isolated AED environment without aed-client-winauth")
    try:
        if metadata.version("aed-client-apikey") != "6.2.0":
            raise RuntimeError("AED requires the approved aed-client-apikey 6.2.0 wheel")
    except metadata.PackageNotFoundError:
        raise RuntimeError("Install the approved aed-client-apikey 6.2.0 wheel and vg2c[aed]") from None
    # The SDK parses host arguments at import; restore them even if import fails.
    arguments = sys.argv
    try:
        sys.argv = [arguments[0] if arguments else "vg2c"]
        with contextlib.redirect_stdout(io.StringIO()):
            return importlib.import_module("aed_client")
    finally:
        sys.argv = arguments


def _read_api_key():
    if os.environ.get("X_API_KEY", "").strip():
        return
    key, encrypted = os.environ.get("API_KEY"), os.environ.get("API_CONFIG")
    if key is None and encrypted is None:
        directory = os.environ.get("AED_SECRETS_DIR", "")
        if not directory or not Path(directory).is_absolute():
            raise ValueError("Supply X_API_KEY, API_KEY/API_CONFIG, or an absolute AED_SECRETS_DIR")
        try:
            key = (Path(directory) / "key.key").read_text(encoding="utf-8").strip()
            encrypted = (Path(directory) / "config_new.json").read_text(encoding="utf-8").strip()
        except OSError:
            raise ValueError("AED secret files are unavailable") from None
    if not key or not encrypted:
        raise ValueError("API_KEY and API_CONFIG must both contain encrypted-key configuration")
    try:
        from cryptography.fernet import Fernet
        value = Fernet(key.encode()).decrypt(encrypted.encode()).decode()
    except ImportError:
        raise RuntimeError("Install vg2c[aed] for encrypted AED credentials") from None
    except Exception:
        raise ValueError("Invalid encrypted AED credential configuration") from None
    if not value.strip():
        raise ValueError("Decrypted X_API_KEY must not be empty")
    os.environ["X_API_KEY"] = value


class AEDClient:
    """Explicit, sequential lot operations. Writes default to dry runs.

    service_factory(facility) supplies an SDK-shaped fake for offline tests.
    """

    def __init__(self, facility, *, mode=None, service_factory=None):
        self.facility = identifier(facility, "facility")
        self.mode = os.environ.get("ENV_MODE", "test") if mode is None else mode
        if isinstance(self.mode, str):
            self.mode = self.mode.lower()
        if self.mode not in {"test", "prod"}:
            raise ValueError("ENV_MODE must be test or prod")
        self._service_factory = service_factory

    def _service(self):
        if self._service_factory is not None:
            return self._service_factory(self.facility)
        ca = Path(os.environ.get("REQUESTS_CA_BUNDLE", ""))
        if not ca.is_absolute() or not ca.is_file():
            raise ValueError("REQUESTS_CA_BUNDLE must name an existing absolute CA bundle")
        sdk = _load_sdk()
        _read_api_key()
        bypass = ",".join((os.getenv("NO_PROXY", ""), os.getenv("no_proxy", ""), ".atmedispo.intel.com"))
        os.environ["NO_PROXY"] = os.environ["no_proxy"] = ",".join(
            dict.fromkeys(host.strip() for host in bypass.split(",") if host.strip())
        )
        # The SDK owns global context; restore this client's facility on every call.
        sdk.load_context(json.dumps({"properties": [
            {"name": "env", "value": "PROD"},
            {"name": "facility", "value": self.facility},
            {"name": "protocol", "value": "HTTPS"},
        ]}))
        return sdk.ManufacturingService()

    def _call(self, method, lot, *parameters, write=False):
        if self.mode not in {"test", "prod"}:
            raise ValueError("ENV_MODE must be test or prod")
        identifier(lot, "lot")
        if any(not isinstance(value, str) or any(ord(char) < 32 and char not in "\n\t" for char in value)
               for value in parameters):
            raise ValueError("AED parameters must be strings without control characters")
        if write and self.mode == "test":
            return {"Success": False, "DryRun": True, "Operation": method, "Lot": lot}
        arguments = tuple(quote(value, safe="") for value in (lot, *parameters))
        # The SDK prints URLs and transaction context; never expose them in job logs.
        try:
            with contextlib.redirect_stdout(io.StringIO()):
                response = getattr(self._service(), method)(*arguments)
                response.raise_for_status()
                result = response.json()
        except (ValueError, RuntimeError):
            raise
        except Exception:
            raise RuntimeError(f"AED {method} request failed") from None
        if not isinstance(result, dict) or result.get("Success") is not True:
            raise RuntimeError(f"AED {method} returned an unsuccessful or malformed response")
        return result

    def lot_status(self, lot):
        return self._call("lot_status", lot)

    def lot_hold(self, lotid, operation, holdcode, comment):
        identifier(operation, "hold operation")
        identifier(holdcode, "hold code")
        return self._call("lot_hold", lotid, operation, holdcode, comment, write=True)

    def lot_release(self, lotid, operation, comment):
        identifier(operation, "release operation")
        return self._call("lot_release", lotid, operation, comment, write=True)

    def lot_set_attr(self, lotid, attributeid, attributevalue):
        identifier(attributeid, "attribute ID")
        if not isinstance(attributevalue, str) or not attributevalue:
            raise ValueError("Attribute value must be a nonempty string")
        return self._call("lot_set_attr", lotid, attributeid, attributevalue, write=True)

    def lot_get_attr(self, lotid, attributeid):
        identifier(attributeid, "attribute ID")
        return self._call("lot_get_attr", lotid, attributeid)

    def lot_contain(self, lotid, operation, reference_number):
        identifier(operation, "contain operation")
        return self._call("lot_contain", lotid, operation, reference_number, write=True)

    def lot_uncontain(self, lotid, operation, reference_number, comment):
        identifier(operation, "uncontain operation")
        return self._call("lot_uncontain", lotid, operation, reference_number, comment, write=True)

    def update_lot_attributes(self, lot, attr_list, skip_value, *, overwrite=False):
        """Return Y/N using the reference's first-attribute and overwrite semantics."""
        identifier(lot, "lot")
        attributes = (attr_list,) if isinstance(attr_list, str) else tuple(attr_list)
        if not attributes or any(not isinstance(item, str) or not item.strip() for item in attributes):
            raise ValueError("attr_list must contain nonempty attribute ID strings")
        if not isinstance(skip_value, str) or not skip_value.strip():
            raise ValueError("skip_value must be a nonempty string")
        if self.mode != "prod":
            return "N"
        result = self.lot_status(lot).get("Result")
        if result is None or result == {}:
            return "N"
        if not isinstance(result, dict) or "Attributes" not in result:
            raise ValueError("Malformed AED lot status result")
        before = result["Attributes"]
        if before is None:
            before = []
        if not isinstance(before, list) or any(
            not isinstance(item, dict) or not isinstance(item.get("ID"), str) for item in before
        ):
            raise ValueError("Malformed AED attribute record")
        if not before and not overwrite:
            return "N"
        if any(item["ID"] in attributes and item.get("Value") == skip_value for item in before):
            return "Y" if overwrite else "N"
        for attribute_id in attributes:
            attribute = next((item for item in before if item["ID"] == attribute_id), None)
            if overwrite or attribute is None or attribute.get("Value") is None:
                self.lot_set_attr(lot, attribute_id, skip_value)
                return "Y"
        return "N"


CSR_HEADERS = ("HOLD_LOT", "AUTO_CONTAIN", "LOT", "EMAIL", "CCB_NUMBER",
               "HOLD_NOTE", "HOLDCATEGORY", "MODULE")

def csr_requests(path, *, workdir):
    """Validate the whole legacy CSR payload before the first manufacturing action."""
    required = set(CSR_HEADERS)
    requests = {}
    with job_path(path, workdir).open(encoding="utf-8-sig", newline="") as stream:
        reader = csv.DictReader(stream, strict=True)
        headers = [name.upper() for name in reader.fieldnames or []]
        if set(headers) != required or len(headers) != len(required):
            raise ValueError("CSR requests require the original unambiguous hold-request headers")
        for row in reader:
            if None in row or any(value is None for value in row.values()):
                raise ValueError("Malformed CSR request row")
            request = {name.upper(): value.strip() for name, value in row.items()}
            lot = identifier(request["HOLD_LOT"], "HOLD_LOT")
            if not request["EMAIL"]:
                raise ValueError("CSR request requires EMAIL for outcome notification")
            note, ccb = request["HOLD_NOTE"], request["CCB_NUMBER"]
            request["COMMENT"] = note + (f"\nCCB: {ccb}" if ccb else "")
            if any(ord(char) < 32 and char not in "\n\t" for char in request["COMMENT"]):
                raise ValueError("CSR hold comment contains control characters")
            if lot in requests and requests[lot] != request:
                raise ValueError(f"Conflicting CSR requests for lot {lot}")
            requests[lot] = request
    return list(requests.values())
