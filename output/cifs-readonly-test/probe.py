"""Read-only CIFS probe; mount .env at /run/secrets/cifs.env."""
import json
import ntpath
import os
import socket
import sys
from datetime import datetime, timezone
from pathlib import Path

from dotenv import dotenv_values

TARGET = r"\\KMatshfs.intel.com\KMATAnalysis$\MAOATM\KuAT\AE\reflow\TESTING-NEW"
IMAGE = os.environ.get("PROBE_IMAGE", "sqlpathfinder-aed:validation")
report = {"time_utc": datetime.now(timezone.utc).isoformat(), "image": IMAGE,
          "target": TARGET, "read_only": True, "checks": {}}
values = dotenv_values("/run/secrets/cifs.env", interpolate=False)
secrets = [str(value) for key, value in values.items() if value and
           any(word in key.upper() for word in ("PASSWORD", "USERNAME", "SECRET", "TOKEN", "API_KEY"))]

def safe_error(exc):
    text = str(exc)
    for secret in sorted(secrets, key=len, reverse=True):
        text = text.replace(secret, "[REDACTED]")
    return {"type": type(exc).__name__, "message": text}

stage = "credentials"
share = None
try:
    for suffix in ("USERNAME", "PASSWORD"):
        value = values.get("CIFS_" + suffix)
        if not value:
            raise ValueError("Missing CIFS_" + suffix)
        os.environ["SMB_" + suffix] = value
    report["checks"][stage] = "present; mapped to SMB_* in this process only"
    server = TARGET.split("\\")[2]
    stage = "dns"
    report["checks"][stage] = sorted({item[4][0] for item in socket.getaddrinfo(server, 445, type=socket.SOCK_STREAM)})
    stage = "tcp_445"
    with socket.create_connection((server, 445), timeout=5):
        pass
    report["checks"][stage] = "connected"
    stage = "authentication"
    from scripthost_portable.aed_api import _connect
    share = _connect(TARGET)
    report["checks"][stage] = "NTLM session established"
    stage = "directory_listing"
    entries = share.listdir(TARGET)
    report["checks"][stage] = {"count": len(entries), "sample": sorted(entries)[:12]}
    stage = "file_read"
    for name in sorted(entries):
        path = ntpath.join(TARGET, name)
        if share.path.isfile(path):
            with share.open_file(path, mode="rb") as stream:
                data = stream.read(1)
            report["checks"][stage] = {"file": name, "bytes_read": len(data), "contents_printed": False}
            break
    else:
        report["checks"][stage] = "not exercised: no regular file in the target directory"
    report["success"] = True
except Exception as exc:
    report.update(success=False, failed_stage=stage, error=safe_error(exc))
finally:
    if share is not None:
        share.reset_connection_cache()
    for suffix in ("USERNAME", "PASSWORD"):
        os.environ.pop("SMB_" + suffix, None)
    output = json.dumps(report, indent=2)
    Path("/probe/result.json").write_text(output + "\n", encoding="utf-8")
    print(output)
sys.exit(0 if report["success"] else 1)
