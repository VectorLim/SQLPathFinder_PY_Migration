from __future__ import annotations
import re
from datetime import datetime,timedelta,timezone
from pathlib import Path
from typing import TYPE_CHECKING
import pandas as pd
from vg2c_new.paths import resolve_path,working_directory_for
from vg2c_new.utilities.base import Utility
from vg2c_new.utilities.csv import CsvUtility
if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState

class SmartAppendUtility(Utility):
    def apply(self,command:Command,state:RuntimeState)->None:
        """Amended port of ScriptHost SmartAppend V4 file/pandas behavior.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: SmartAppendTask.smartAppend4_file_pandas and process_csv.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: V4 union, cutoff delete, PK replacement, header order/case, old-file abort, default-new and update-time hook.
        Amendments: one pandas pass + atomic pathlib replacement. Intentionally discarded: V1/V3, Hadoop and Windows staging.
        """
        args=[state.substitute(v) for v in command.arguments]
        if len(args)<6:raise ValueError("SmartAppend V4 requires old file, new file, delete column/value, update column and sort-headers flag.")
        old,new=_path(command,state,args[0]),_path(command,state,args[1]); delete_col,delete_value,key_col,sort_headers=args[2:6]
        abort_old=_yn(args[6]) if len(args)>6 else False; update_file=args[7].strip() if len(args)>7 else ""
        version=args[8].strip().upper().replace(" ","") if len(args)>8 else "VERSION4"; default_new=args[10] if len(args)>10 and args[10].strip() else ""; upper=_yn(args[12]) if len(args)>12 else False
        if version and version!="VERSION4":raise RuntimeError("Historical SmartAppend versions are intentionally removed; use VERSION4 semantics.")
        if not new.exists():return
        new_frame=self._read(new,upper)
        if new_frame.empty:return
        if abort_old and not old.exists():raise FileNotFoundError(f"Old SmartAppend file not found: {old}")
        old_frame=self._read(old,upper) if old.exists() else pd.DataFrame()
        delete_col=delete_col.upper() if upper else delete_col; key_col=key_col.upper() if upper else key_col
        if delete_col and delete_value:
            cutoff=self._delete_cutoff(delete_value,state); new_frame=self._apply_delete(new_frame,delete_col,cutoff)
            if not old_frame.empty:old_frame=self._apply_delete(old_frame,delete_col,cutoff)
        if key_col and not old_frame.empty and not new_frame.empty:
            new_key=self._column(new_frame,key_col,"Update"); old_key=self._column(old_frame,key_col,"Update"); keys=set(new_frame[new_key].astype(str)); old_frame=old_frame[~old_frame[old_key].astype(str).isin(keys)]
        columns=self._union_columns(old_frame,new_frame,sort_headers); fill=_default_value(default_new)
        old_frame=old_frame.reindex(columns=columns,fill_value="") if not old_frame.empty else pd.DataFrame(columns=columns); new_frame=new_frame.reindex(columns=columns,fill_value=fill)
        result=pd.concat([new_frame,old_frame],ignore_index=True,sort=False).fillna(""); tmp=old.with_name(f".{old.name}.smartappend.tmp"); CsvUtility.write_dataframe(result,tmp); old.parent.mkdir(parents=True,exist_ok=True); tmp.replace(old)
        if update_file:
            from vg2c_new.model import Command as C,CommandKind,SourceSpan
            from vg2c_new.utilities.file_values import UpdateTimeUtility
            synthetic=C(command.index,CommandKind.UTILITY,"UTILITIES","update_time",(),"",command.raw,(update_file,),SourceSpan(command.span.file,command.span.start_line,command.span.end_line))
            UpdateTimeUtility().apply(synthetic,state)
    def _read(self,path:Path,upper:bool)->pd.DataFrame:
        """Amended port of SmartAppendTask.process_csv input normalization."""
        frame=pd.read_csv(path,sep=CsvUtility.delimiter(path),dtype=str,keep_default_na=False,na_values=["."],encoding="utf-8-sig")
        if upper:frame.columns=[str(c).upper() for c in frame.columns]
        if any(not str(c).strip() for c in frame.columns):raise ValueError(f"{path} contains an empty column header.")
        return frame.fillna("")
    def _apply_delete(self,frame:pd.DataFrame,column:str,cutoff:str)->pd.DataFrame:return frame[frame[self._column(frame,column,"Delete")].astype(str)>cutoff]
    def _delete_cutoff(self,value:str,state:RuntimeState)->str:
        """Amended port of SmartAppendTask.parseDelCriteria for Today/Today_GMT days/hours."""
        text=value.strip(); m=re.fullmatch(r"(?i)(Today(?:_gmt)?)\s*(?:-\s*)?(\d*(?:\.\d+)?)\s*(days?|hours?)",text)
        if not m:return text
        amount=int(float(m.group(2))); units=m.group(3).lower()
        if m.group(1).lower()=="today_gmt":
            raw=state.lookup("SPF_GW_DT"); base=datetime.strptime(raw,"%Y-%m-%d %H:%M:%S") if raw else datetime.now(timezone.utc).replace(tzinfo=None)
        else:
            raw=state.lookup("SPF_JOB_DT") or state.lookup("SPF_SITE_TIME"); base=datetime.strptime(raw,"%Y-%m-%d %H:%M:%S") if raw else datetime.now()
        if units.startswith("day"):base=base.replace(hour=0,minute=0,second=0,microsecond=0)-timedelta(days=amount)
        else:base-=timedelta(hours=amount)
        return base.strftime("%Y-%m-%d %H:%M:%S")
    @staticmethod
    def _column(frame:pd.DataFrame,requested:str,kind:str)->str:
        actual={str(c).casefold():str(c) for c in frame.columns}.get(requested.casefold())
        if actual is None:raise ValueError(f"{kind} Column does not exist: {requested}")
        return actual
    @staticmethod
    def _union_columns(old:pd.DataFrame,new:pd.DataFrame,sort_headers:str)->list[str]:
        columns=[]; seen=set()
        for source in (list(old.columns),list(new.columns)):
            for column in source:
                key=str(column).casefold()
                if key not in seen:seen.add(key);columns.append(str(column))
        if sort_headers.strip().upper()=="Y":columns.sort(key=str.casefold)
        return columns
def _default_value(value:str)->str:
    text=value.strip()
    if text in {'"\'\'"', "''",'""'}:return ""
    return text.strip('"').strip("'")
def _path(command:Command,state:RuntimeState,value:str)->Path:return resolve_path(value,state,base=working_directory_for(command.option("WORKDIR"),state))
def _yn(value:str)->bool:return str(value).strip().upper() in {"Y","YES","TRUE","1"}
