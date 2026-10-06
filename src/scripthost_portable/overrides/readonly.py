"""Portable overrides for the archived SetFileROTask class."""

import SPFLib.SPFSQL3 as legacy


class SetFileROTask(legacy.SetFileROTask):
    """Extend the original implementation with portable methods."""

    def executeTaskCommand(self):
        r"""
        ' Overridden base method. Implementation of Set_File_RO
        '================================
        'Set File Attribute
        '
        'ARGS:
        '----
        'MyFile: File to set
        'MyRO  : File Attributes. E.g., READONLY, READWRITE, READONLY + HIDDEN...
        '================================
        sample input line: /UTILITIES=@EXEDIR@\SetFileRO.va "AppendFileDest.csv" "ReadOnly"
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        NoArgs = 1 # number of Args expected to process this Task
        MyUtilityName = "SETFILERO"
        destFile = None #MyFile
        MyRO = None
        MyROCode = 128 #'Normal
        errMsg = ""


        try :
            self.Write_Prompt()
            self.Console("Starting the Set File Attribute Utility, v1.0 ... {0}\n".format(self.DatetimeNow))

            destFile = self.MyUtilities[1]
            self.logger.debug("{0} - MyFile : {1}".format(calling_func, destFile))
            destFile = legacy.os.path.abspath(destFile)
            destFile_Path, destFile_Name = legacy.os.path.split(destFile)
            self.logger.debug("{0} - destFile_Path : {1}".format(calling_func, destFile_Path))
            self.logger.debug("{0} - destFile_Name : {1}".format(calling_func, destFile_Name))

            MyRO = self.MyUtilities[2]
            self.logger.debug("{0} - MyRO : {1}".format(calling_func, MyRO))

            if MyRO.strip().upper() == "READONLY" :
                MyROCode = 1
            self.logger.debug("{0} - MyROCode : {1}".format(calling_func, MyROCode))

            self.Console("  Ensure File is {0} : {1}".format(MyRO, destFile))

            if legacy.os.path.exists(destFile) is False :
                self.Console("  File Does not exist. Skipping ...")
                return

            #file exists continue

            if legacy.os.name != "nt":
                import stat
                mode = legacy.os.stat(destFile).st_mode
                legacy.os.chmod(destFile, mode & ~(stat.S_IWUSR | stat.S_IWGRP | stat.S_IWOTH)
                         if MyROCode == 1 else mode | stat.S_IWUSR)
                self.ConsoleDoneWithTimeStamp2()
                return
            destFileAttr = legacy.win32api.GetFileAttributes(destFile)
            self.logger.debug("{0} - destFileAttr : {1}".format(calling_func, destFileAttr))
            #note File attributes are differein VB and python win32api(follows std windows codes)
            #in VBscript Normal == 0, in py.win32api == 128
            #in VBscript readonly == 1, in py.win32api == 1
            if MyROCode == 1 :
                #'SET RO Attribute
                legacy.win32api.SetFileAttributes(destFile, legacy.win32con.FILE_ATTRIBUTE_READONLY)
            else :
                #'SET RW Attribute
                legacy.win32api.SetFileAttributes(destFile, legacy.win32con.FILE_ATTRIBUTE_NORMAL)

            self.ConsoleDoneWithTimeStamp2()
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
