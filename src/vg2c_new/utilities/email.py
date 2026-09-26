from __future__ import annotations
from pathlib import Path
from typing import TYPE_CHECKING, Any, Callable
from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility
if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState

class EmailUtility(Utility):
    def __init__(self,send_msg:Callable[...,Any]|None): self._send_msg=send_msg
    def apply(self,command:Command,state:RuntimeState)->None:
        """Rewrite of ScriptHost EmailTask through DataSyncX send_msg.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: EmailTask.executeTaskCommand/SPFEmail.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d; DataSyncX pin 1.1.6.
        Port mode: REWRITE.
        Preserved: attachments, recipients, subject, body-file, CC and BCC semantic inputs.
        Amendments: one injected DataSyncX send_msg boundary.
        Intentionally discarded: Outlook/SMTP/SMTPAuth selection, role and OnlyIntel transport flags.
        """
        if self._send_msg is None: raise RuntimeError("DataSyncX send_msg is unavailable in this environment.")
        args=[state.substitute(v) for v in command.arguments]
        if len(args)<3: raise ValueError("Email requires attachment list, recipients and subject.")
        attachments=_split(args[0]); recipients=_split(args[1]); subject=args[2]; body_value=args[3] if len(args)>3 else ""
        body_path=_path(command,state,body_value) if body_value else None
        body=body_path.read_text(encoding="utf-8",errors="replace") if body_path and body_path.exists() else body_value
        cc=_split(args[4]) if len(args)>4 else []; bcc=_split(args[5]) if len(args)>5 else []
        resolved=[str(_path(command,state,v)) for v in attachments]
        self._send_msg(to=recipients,subject=subject,body=body,attachments=resolved,cc=cc,bcc=bcc)

def _split(value:str)->list[str]: return [x.strip() for x in value.replace(";",",").split(",") if x.strip()]
def _path(command:Command,state:RuntimeState,value:str)->Path:
    return resolve_path(value,state,base=working_directory_for(command.option("WORKDIR"),state))
