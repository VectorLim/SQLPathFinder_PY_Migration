"""Portable overrides for the archived NormalQueryTaskBase class."""

import re

import SPFLib.SPFSQL3 as legacy


class NormalQueryTaskBase(legacy.NormalQueryTaskBase):
    """Extend the original implementation with portable methods."""

    def SubStitute_CT(self, MyQuery, IncludeAliasFlag=True, ConvertToUpperCaseFlag=False,
                     PerformCleanUpFlag=False):
        # CTARRAY is uppercased by the original parser. Match its INI filename
        # on case-sensitive hosts; the original task still expands the token.
        if legacy.os.name != "nt" and MyQuery is not None:
            MyQuery = re.sub(
                r"(CrossTab->\[\[)([\s\w]+)",
                lambda match: match[1] + match[2].upper(), MyQuery, flags=re.IGNORECASE,
            )
        return super().SubStitute_CT(
            MyQuery, IncludeAliasFlag, ConvertToUpperCaseFlag, PerformCleanUpFlag
        )

    def Prep_Inc_Process(self, TmpFile, TmpInc, TmpNode, Sitei, OleDBOpt, SQLEngine, Inc1Only):
        if legacy.os.sep == "/" and TmpFile.startswith(".\\"):
            TmpFile = "./" + TmpFile[2:].replace("\\", "/")
        return super().Prep_Inc_Process(
            TmpFile, TmpInc, TmpNode, Sitei, OleDBOpt, SQLEngine, Inc1Only
        )
