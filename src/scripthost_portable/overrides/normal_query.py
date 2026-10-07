"""Portable overrides for the archived NormalQueryTaskBase class."""

import SPFLib.SPFSQL3 as legacy


class NormalQueryTaskBase(legacy.NormalQueryTaskBase):
    """Extend the original implementation with portable methods."""

    def Prep_Inc_Process(self, TmpFile, TmpInc, TmpNode, Sitei, OleDBOpt, SQLEngine, Inc1Only):
        if legacy.os.sep == "/" and TmpFile.startswith(".\\"):
            TmpFile = "./" + TmpFile[2:].replace("\\", "/")
        return super().Prep_Inc_Process(
            TmpFile, TmpInc, TmpNode, Sitei, OleDBOpt, SQLEngine, Inc1Only
        )
