from __future__ import annotations
from pathlib import Path
from typing import TYPE_CHECKING, Any
from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility
if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState

class GetWebTextUtility(Utility):
    def __init__(self,session:Any=None):
        if session is None:
            import requests
            session=requests.Session()
        self._session=session
    def apply(self,command:Command,state:RuntimeState)->None:
        """Amended port of current ScriptHost Get-Web-Text Version 2 requests path.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: GetWebTextTask and SPFUtilities/utils.py :: SPFWebCopyPyReqs.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: HTTP/file output, timeout, no-abort and V2 requests intent.
        Amendments: requests only with approved Linux auth supplied through session config.
        Intentionally discarded: V1, Windows SSPI/WinHTTP and certificate helper transports.
        """
        args=[state.substitute(v) for v in command.arguments]
        if len(args)<2: raise ValueError("Get_Web_Text requires URL and output file.")
        url,output=args[:2]; version=args[2].strip().upper().replace(" ","") if len(args)>2 else "VERSION2"
        if version not in {"","VERSION2"}: raise RuntimeError("Historical Get-Web-Text V1 is intentionally removed.")
        auth=args[3].strip().upper() if len(args)>3 else "N"; no_abort=args[4].strip().upper() in {"Y","YES","TRUE","1"} if len(args)>4 else False
        timeout=int(args[5]) if len(args)>5 and args[5].isdigit() else 60
        try:
            if auth in {"Y","YES","TRUE","1"}: raise RuntimeError("Integrated Windows authentication is not portable; configure an approved authenticated requests session.")
            response=self._session.get(url,timeout=timeout); response.raise_for_status()
            path=_path(command,state,output); path.parent.mkdir(parents=True,exist_ok=True); path.write_bytes(response.content)
        except Exception:
            if not no_abort: raise

def _path(command:Command,state:RuntimeState,value:str)->Path:
    return resolve_path(value,state,base=working_directory_for(command.option("WORKDIR"),state))
