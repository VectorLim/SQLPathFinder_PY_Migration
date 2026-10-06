"""Portable overrides for the archived SmartAppendTask class."""

import SPFLib.SPFSQL3 as legacy


class SmartAppendTask(legacy.SmartAppendTask):
    """Extend the original implementation with portable methods."""

    def performUpdateTime(self) :
        """
         helper method to call Update_time or Update_time_file methods
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        try :
            self.logger.debug("{0} - MyUpdateFile : {1}".format(calling_func,self.MyUpdateFile))
            if self.IsEmptyOrNone(self.MyUpdateFile) == False :
                if self.IsEmptyOrNone(self.gSPFInstance) == False :
                    # [Removed from current version] self.Do_Update_Time_File(self.MyUpdateFile, os.path.join(".\\","{0}.spf$data".format(self.gSPFInstance)))
                    self.Do_Update_Time_File(self.MyUpdateFile, legacy.os.path.join(".","{0}.spf$data".format(self.gSPFInstance)))
                else :
                    self.Do_Update_Time(self.MyUpdateFile)
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
