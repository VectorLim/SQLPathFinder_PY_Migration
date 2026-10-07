"""Normalize the legacy current-directory spelling before writing site time."""

import SPFLib.SPFSQL3 as legacy


class GetSiteTimeTask(legacy.GetSiteTimeTask):
    def SetIni(self, INIFilePath, INI_SectionName, INI_SectionSettingName,
               INI_SectionSettingValue):
        if legacy.os.name != "nt" and INIFilePath.startswith(".\\"):
            INIFilePath = "./" + INIFilePath[2:].replace("\\", "/")
        return super().SetIni(
            INIFilePath, INI_SectionName, INI_SectionSettingName, INI_SectionSettingValue
        )
