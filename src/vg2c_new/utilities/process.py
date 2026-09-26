from __future__ import annotations
import subprocess, sys
from pathlib import Path
from typing import TYPE_CHECKING
from vg2c_new.paths import resolve_path, working_directory_for
from vg2c_new.utilities.base import Utility
if TYPE_CHECKING:
    from vg2c_new.model import Command
    from vg2c_new.runtime import RuntimeState

class RunPythonUtility(Utility):
    def apply(self,command:Command,state:RuntimeState)->None:
        """Amended port of current ScriptHost RunPythonScriptTask.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: RunPythonScriptTask and SPFUtilities/utils.py :: Utilities.Run_Python.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: script-file execution, raw optional argument string and exit failure.
        Amendments: current sys.executable and direct portable subprocess.
        Intentionally discarded: Python-v3-old/next, app-server and Windows/Linux selector history.
        """
        args=[state.substitute(v) for v in command.arguments]
        if not args: raise ValueError("Run_Python_Script requires a script path.")
        if len(args)>4 and args[4].strip().upper() in {"PYTHON-V3-OLD","PYTHON-V3-NEXT"}:
            raise RuntimeError(f"Historical Python selector {args[4].strip().upper()!r} is intentionally removed.")
        script=_path(command,state,args[0])
        if not script.exists(): raise FileNotFoundError(script)
        run_args=[sys.executable,str(script)]
        if len(args)>1 and args[1].strip(): run_args.append(args[1])
        subprocess.run(run_args,cwd=state.working_directory,check=True)

class PyScriptUtility(Utility):
    def apply(self,command:Command,state:RuntimeState)->None:
        """Amended port of ScriptHost PyScriptTask.
        Source: SPSQL3_py/SPFLib/SPFSQL3.py :: PyScriptTask.executeTaskCommand.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT.
        Preserved: first argument script file, remaining individual argv values and exit-code 2 continue.
        Amendments: current interpreter with no sys.path/task-global mutation.
        Intentionally discarded: Python 2/custom engines and AutoCommonality special routing.
        """
        args=[state.substitute(v) for v in command.arguments]
        if not args: raise ValueError("{PYSCRIPT} requires a Python script file.")
        script=_path(command,state,args[0])
        if not script.exists(): raise FileNotFoundError(script)
        done=subprocess.run([sys.executable,str(script),*args[1:]],cwd=state.working_directory,check=False)
        if done.returncode not in {0,2}: raise RuntimeError(f"Python script exited with code {done.returncode}.")

class RunRUtility(Utility):
    def apply(self,command:Command,state:RuntimeState)->None:
        """Amended portable port of current ScriptHost RunRFileTask / Utilities.Run_R.
        Reference source/commit: vendored ScriptHost at 8ddd5e6463b43834d769057be48041ec657f0f9d.
        Port mode: AMENDED PORT. Preserved: R file + optional raw argument. Amendments: Rscript.
        Intentionally discarded: app-server/Windows selector routing.
        """
        args=[state.substitute(v) for v in command.arguments]
        if not args: raise ValueError("Run_R_Script requires a script path.")
        run_args=["Rscript",str(_path(command,state,args[0]))]
        if len(args)>1 and args[1].strip(): run_args.append(args[1])
        subprocess.run(run_args,cwd=state.working_directory,check=True)

class InlineRUtility(Utility):
    def apply(self,command:Command,state:RuntimeState)->None:
        script=state.substitute(command.body)
        if not script.strip(): raise ValueError("RSCRIPT contains no R code.")
        args=[state.substitute(v) for v in command.arguments]; run_args=["Rscript","-e",script]
        if len(args)>1 and args[1].strip(): run_args.append(args[1])
        subprocess.run(run_args,cwd=state.working_directory,check=True)

def _path(command:Command,state:RuntimeState,value:str)->Path:
    return resolve_path(value,state,base=working_directory_for(command.option("WORKDIR"),state))
