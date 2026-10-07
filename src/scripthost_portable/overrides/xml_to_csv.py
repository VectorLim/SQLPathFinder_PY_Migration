"""Replace the Excel XML converter at the original task's command boundary."""

import SPFLib.SPFSQL3 as legacy


class XMLToCSVTask(legacy.XMLToCSVTask):
    def Run(self, CMDToExecute, CMDArguments, *args, **kwargs):
        if (
            legacy.os.name != "nt"
            and legacy.os.path.basename(CMDToExecute) == "spfExcelUtility.exe"
            and len(CMDArguments) == 3
            and CMDArguments[0] == '/mode="XMLTOCSV"'
            and CMDArguments[1].startswith('/XML="')
            and CMDArguments[2].startswith('/out="')
        ):
            from scripthost_portable.file_operations import xml_to_csv

            source, destination = (argument[6:-1] for argument in CMDArguments[1:])
            xml_to_csv(source, destination, self.GetFileDLM(destination))
            return True, 0
        return super().Run(CMDToExecute, CMDArguments, *args, **kwargs)
