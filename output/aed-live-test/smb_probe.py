"""Inside the SOIMS base image: can the ICM_PCS shares be read without SMB_USERNAME/SMB_PASSWORD?

Control: no ticket. Then kinit with the DataSyncX Kerberos account and retry with no SMB credentials.
"""

import os
import subprocess

import smbclient

TARGETS = [
    (
        "AZATSHFS.intel.com",
        r"\\AZATSHFS.intel.com\AZATAnalysis$\MAOATM\Config\VF_POR_Cfg\ICM_PCS\ICMPCS_CWFNCO_CSR_IAM\KM",
    ),
    (
        "kmfile5.kssm.intel.com",
        r"\\kmfile5.kssm.intel.com\ICM_PCS\ICMPCS_CWFNCO_CSR_IAM",
    ),
]


def attempt(label):
    smbclient.reset_connection_cache()
    for server, path in TARGETS:
        try:
            smbclient.register_session(
                server, auth_protocol="kerberos", connection_timeout=30
            )
            print(f"[{label}] {server}: OK {sorted(smbclient.listdir(path))[:8]}")
        except Exception as exc:
            print(f"[{label}] {server}: {type(exc).__name__}: {str(exc)[:200]}")


attempt("no ticket")
account = os.environ["DATASYNCX_USERNAME"].split("\\")[-1]
subprocess.run(
    ["kinit", f"{account}@GAR.CORP.INTEL.COM"],
    input=os.environ["DATASYNCX_PASSWORD"].encode(),
    check=True,
    stdout=subprocess.DEVNULL,
)
subprocess.run(["klist"], check=False)
attempt("kerberos ticket")

smbclient.reset_connection_cache()
server, path = TARGETS[0]
try:
    smbclient.register_session(
        server,
        username=f"GAR\\{account}",
        password=os.environ["DATASYNCX_PASSWORD"],
        auth_protocol="ntlm",
        connection_timeout=30,
    )
    print(f"[ntlm user+password] {server}: OK {sorted(smbclient.listdir(path))}")
except Exception as exc:
    print(f"[ntlm user+password] {server}: {type(exc).__name__}: {str(exc)[:200]}")
