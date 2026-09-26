from __future__ import annotations
import re
from datetime import datetime, timezone
from pathlib import Path
from typing import TYPE_CHECKING, Any
import pandas as pd
from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility
from vg2c_new.utilities.csv import CsvUtility
if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState
_MARS_MISSING_DOT = re.compile(r"@\[\]@(?=[A-Za-z_])")

class OracleQueryUtility(Utility):
    def __init__(self, *, mars_reader: Any=None, aries_reader: Any=None, oasys_reader: Any=None, oracle_reader: Any=None):
        self._mars_reader=mars_reader; self._aries_reader=aries_reader; self._oasys_reader=oasys_reader; self._oracle_reader=oracle_reader
    def apply(self, command: Command, state: RuntimeState) -> None:
        """Amended port of current ScriptHost Oracle routing through DataSyncX.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: nqOracleTask plus src/vg2c/dispatch/dialects/{mars,aries,oasys}.py.
        Reference source/commit: SQLPathFinder 8ddd5e6463b43834d769057be48041ec657f0f9d; DataSyncX pin 1.1.6.
        Port mode: AMENDED PORT.
        Preserved: node-family routing, MARS missing-dot correction, OASYS token and site/query call contract.
        Amendments: DataSyncX reader injection replaces Oracle/.NET/SQLPFaaS transports.
        Intentionally discarded: credentials, direct drivers, service routing and generated Python.
        """
        node=state.substitute(command.option("NODE","") or ""); sql=state.substitute(command.body)
        reader,sql=self._route(node,sql)
        if reader is None: raise RuntimeError(f"No DataSyncX reader is available for Oracle node {node!r}.")
        frame=_as_frame(reader.read(site=self._site(node,state),query=sql))
        headers=command.option("HEADERS")
        if headers:
            requested=[h.strip() for h in state.substitute(headers).split(",") if h.strip()]
            if requested and len(requested)==len(frame.columns): frame.columns=requested
        output=command.option("CSV")
        if output: CsvUtility.write_dataframe(frame,_path(command,state,state.substitute(output)))
    def _route(self,node:str,sql:str)->tuple[Any,str]:
        upper=node.upper()
        if "MARS" in upper: return self._mars_reader,_MARS_MISSING_DOT.sub("@[]@.",sql)
        if "ARIES" in upper: return self._aries_reader,sql
        if "OASYS" in upper: return self._oasys_reader,sql.replace("@OASYSSCHEMA@","")
        return self._oracle_reader,sql
    @staticmethod
    def _site(node:str,state:RuntimeState)->str:
        value=node.strip()
        if not value or value.startswith("<<<"): value=state.lookup("NODE") or state.environment_value("VG2C_DEFAULT_NODE") or ""
        if "." in value: value=value.split(".",1)[0]
        return value or "KM"

class GetSiteTimeUtility(Utility):
    def __init__(self, *, mars_reader:Any=None, aries_reader:Any=None, oasys_reader:Any=None, oracle_reader:Any=None):
        self._query=OracleQueryUtility(mars_reader=mars_reader,aries_reader=aries_reader,oasys_reader=oasys_reader,oracle_reader=oracle_reader)
    def apply(self,command:Command,state:RuntimeState)->None:
        """Amended port of ScriptHost GetSiteTimeTask/Do_Get_Time.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: GetSiteTimeTask and nqOracleTask.Do_Get_Time.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: GMT shortcut and database sysdate query for a site.
        Amendments: DataSyncX reader + RuntimeState replace gMySPFJobTime.
        Intentionally discarded: temporary files, MemTable and .spf$data persistence.
        """
        args=[state.substitute(v) for v in command.arguments]
        if not args or not args[0].strip(): raise ValueError("{GET-SITE-TIME} requires a site or GMT.")
        site=args[0].strip()
        if site.upper()=="GMT": value=datetime.now(timezone.utc).strftime("%Y-%m-%d %H:%M:%S")
        else:
            reader,query=self._query._route(site,"SELECT To_Char(sysdate,'YYYY-MM-DD hh24:mi:ss') AS last_update_date FROM dual WHERE rownum <= 1")
            if reader is None: raise RuntimeError(f"No DataSyncX reader is available for site {site!r}.")
            frame=_as_frame(reader.read(site=OracleQueryUtility._site(site,state),query=query))
            if frame.empty: raise RuntimeError(f"No site time returned for {site!r}.")
            value=str(frame.iloc[0,0])
        state.set_global("SPF_SITE_TIME",value); state.set_global("SPF_JOB_DT",value)

class PlatformGapUtility(Utility):
    def __init__(self,target:str,reason:str): self._target=target; self._reason=reason
    def apply(self,command:Command,state:RuntimeState)->None: raise RuntimeError(f"Current-platform gap for {self._target}: {self._reason}")

def _as_frame(result:Any)->pd.DataFrame:
    if isinstance(result,pd.DataFrame): return result.copy()
    if hasattr(result,"to_pandas"):
        converted=result.to_pandas()
        if isinstance(converted,pd.DataFrame): return converted.copy()
    if result is None: return pd.DataFrame()
    return pd.DataFrame(result)

def _path(command:Command,state:RuntimeState,value:str)->Path:
    return resolve_path(value,state,base=working_directory_for(command.option("WORKDIR"),state))
