"""Portable overrides for the archived SetFileROTask class."""
import SPFLib.SPFSQL3 as legacy

class SetFileROTask(legacy.SetFileROTask):

    def executeTaskCommand(self):
        if legacy.os.name == 'nt':
            return super().executeTaskCommand()
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        MyROCode = 128
        try:
            self.Write_Prompt()
            self.Console('Starting the Set File Attribute Utility, v1.0 ... {0}\n'.format(self.DatetimeNow))
            destFile = self.MyUtilities[1]
            self.logger.debug('{0} - MyFile : {1}'.format(calling_func, destFile))
            destFile = legacy.os.path.abspath(destFile)
            destFile_Path, destFile_Name = legacy.os.path.split(destFile)
            self.logger.debug('{0} - destFile_Path : {1}'.format(calling_func, destFile_Path))
            self.logger.debug('{0} - destFile_Name : {1}'.format(calling_func, destFile_Name))
            MyRO = self.MyUtilities[2]
            self.logger.debug('{0} - MyRO : {1}'.format(calling_func, MyRO))
            if MyRO.strip().upper() == 'READONLY':
                MyROCode = 1
            self.logger.debug('{0} - MyROCode : {1}'.format(calling_func, MyROCode))
            self.Console('  Ensure File is {0} : {1}'.format(MyRO, destFile))
            if legacy.os.path.exists(destFile) is False:
                self.Console('  File Does not exist. Skipping ...')
                return
            import stat
            mode = legacy.os.stat(destFile).st_mode
            legacy.os.chmod(destFile, mode & ~(stat.S_IWUSR | stat.S_IWGRP | stat.S_IWOTH) if MyROCode == 1 else mode | stat.S_IWUSR)
            self.ConsoleDoneWithTimeStamp2()
            return
        except Exception as err:
            self.logger.exception('{0} - {1}'.format(calling_func, err.args[0]))
            raise
