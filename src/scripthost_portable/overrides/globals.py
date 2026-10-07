"""Adapt unavailable .NET service contexts and the local directory separator."""

import SPFLib.SPFGlobals as legacy


class SPFGlobals(legacy.SPFGlobals):
    @property
    def gIsSvc(self) -> bool:
        if legacy.sys.platform != "win32" or "System" not in vars(legacy):
            if legacy.SPFGlobals.__gIsSvc is None:
                legacy.SPFGlobals.__gIsSvc = False
            return legacy.SPFGlobals.__gIsSvc
        return super().gIsSvc

    @property
    def gLocalDir(self):
        if legacy.os.name != "nt" and legacy.SPFGlobals.__gLocalDir is None:
            legacy.SPFGlobals.__gLocalDir = legacy.os.path.abspath(legacy.os.path.curdir) + legacy.os.sep
        return super().gLocalDir
