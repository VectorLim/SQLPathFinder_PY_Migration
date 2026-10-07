"""Adapt SmartAppend's persisted time path, retaining its original algorithm."""

import SPFLib.SPFSQL3 as legacy


class SmartAppendTask(legacy.SmartAppendTask):
    def Do_Update_Time_File(self, MyFile, MyIni):
        if legacy.os.name != "nt" and MyIni.startswith(".\\"):
            MyIni = "./" + MyIni[2:].replace("\\", "/")
        return super().Do_Update_Time_File(MyFile, MyIni)
