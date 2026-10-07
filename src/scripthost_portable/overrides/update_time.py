"""Normalize the legacy current-directory spelling before reading site time."""

import SPFLib.SPFSQL3 as legacy


class UpdateTimeFileTask(legacy.UpdateTimeFileTask):
    def GetIni(self, INIFilePath, INI_SectionName, INI_SectionSettingName,
               CFParserStrictMode=True, CFParserReadRaw=False, CFReadDelimiters=("=", ":")):
        if legacy.os.name != "nt" and INIFilePath.startswith(".\\"):
            INIFilePath = "./" + INIFilePath[2:].replace("\\", "/")
        return super().GetIni(
            INIFilePath, INI_SectionName, INI_SectionSettingName,
            CFParserStrictMode, CFParserReadRaw, CFReadDelimiters,
        )
