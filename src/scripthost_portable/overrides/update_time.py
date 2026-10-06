"""Portable overrides for the archived UpdateTimeFileTask class."""

import SPFLib.SPFSQL3 as legacy


class UpdateTimeFileTask(legacy.UpdateTimeFileTask):
    """Extend the original implementation with portable methods."""

    def executeTaskCommand(self):
        """
        ' Overridden base method -- implementation of Do_Update_Time_File
        '================================
        'Test if Last Query was successful &
        'update a TimeStamp in File if it was -- Note: this gets executed when in UI right click + 'run' of Update-Time task
        '================================
        sample input line: /UTILITIES={UPDATE-TIME-FILE} "testUpdateTime.csv"
        """

        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        fileToWrite = None
        try :
            self.Write_Prompt()

            fileToWrite = self.MyUtilities[1]
            self.logger.debug("{0} - fileToWrite: {1}".format(calling_func, fileToWrite))
            if self.IsEmptyOrNone(self.gMySPFJobTime) is True :
                # [Removed from current version] self.gMySPFJobTime = self.GetIni(os.path.join(".\\", "{0}.spf$data".format(self.gSPFInstance)),"SITE-TIME", "TIME")
                self.gMySPFJobTime = self.GetIni(legacy.os.path.join(".", "{0}.spf$data".format(self.gSPFInstance)),"SITE-TIME", "TIME")

            self.Do_Update_Time(fileToWrite)

        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
