"""Portable overrides for the archived Utilities class."""

import SPFLib.SPFUtilities.utils as legacy


class Utilities(legacy.Utilities):
    """Extend the original implementation with portable methods."""

    def SPFRoboCopy(self, MyLocal, MyInstance, MyFile, MySrc, MyDest, MyRetry, MyWait, MyOpen, MyArg, MyErrOp, MyPassExitCodes=[]) :
        """
        '====================================================
        'Robocopy File
        '
        'Args:
        '====
        'MyLocal: Local Exe Dir or N or ...
        'MyFile : File to copy
        'MySrc  ; Source Dir
        'MyDest : Dest Dir
        'MyRetry: # retries
        'MyWait : Wait time between retries
        'MyOpen : Y to not copy if src file open
        'MyArg  : Addnal Robocopy args. E.g., /MOV
        'MyErrOp: Y=Abort if Src File ~found
        'MyPassExitCodes : List of exit codes that shall be treated as Pass exit codes E.g., : [13, 14, 15]
        '
        'GLOBALS:
        '-------
        'gLocalDir: Local Folder
        'gMyAbort :Job Abort global
        '====================================================
        """

        self.ConsoleWithTimeStamp("Starting Robocopy Applet, v1.6") #VA30.86
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        SPFRoboCopyStatus = False # assume robocopy failed
        MyData = None
        lMyArrRC = None
        i = None
        Tmp = None
        MyModStr = None
        TmpD = None
        MyDModStr = None
        SPFRoboCopyExitCode = {16 : "***Robocopy Fatal Error. No files copied***",
                                15 : "Robocopy OKCOPY + FAIL + MISMATCHES + XTRA",
                                14 : "Robocopy FAIL + MISMATCHES + XTRA",
                                13 : "Robocopy OKCOPY + FAIL + MISMATCHES",
                                12 : "Robocopy FAIL + MISMATCHES",
                                11 : "Robocopy OKCOPY + FAIL + XTRA",
                                10 : "Robocopy FAIL + XTRA",
                                9 : "Robocopy OKCOPY + FAIL",
                                8 : "Robocopy error. Some files/folders could not be copied",
                                7 : "Robocopy OKCOPY + MISMATCHES + XTRA",
                                6 : "Robocopy MISMATCHES + XTRA",
                                5 : "Robocopy OKCOPY + MISMATCHES",
                                4 : "Robocopy Mismatched files and folders detected",
                                3 : "Robocopy OKCOPY + XTRA",
                                2 : "Robocopy Extra files/folders detected",
                                1 : "Robocopy Successful (1)",
                                0 : "Robocopy Successful (0) No files need be copied"}

        SPFRoboCopy_pass_ExitCode = [0, 1, 2, 3]
        SPFRoboCopy_err_ExitCode = [item for item in SPFRoboCopyExitCode.keys() if item not in SPFRoboCopy_pass_ExitCode]

        self.logger.debug("{0} - MyLocal: '{1}'".format(calling_func, MyLocal))
        self.logger.debug("{0} - MyInstance: '{1}'".format(calling_func, MyInstance))
        self.logger.debug("{0} - MyFile: '{1}'".format(calling_func, MyFile))
        self.logger.debug("{0} - MySrc: '{1}'".format(calling_func, MySrc))
        self.logger.debug("{0} - MyDest: '{1}'".format(calling_func, MyDest))
        self.logger.debug("{0} - MyRetry: '{1}'".format(calling_func, MyRetry))
        self.logger.debug("{0} - MyWait: '{1}'".format(calling_func, MyWait))
        self.logger.debug("{0} - MyOpen: '{1}'".format(calling_func, MyOpen))
        self.logger.debug("{0} - MyArg: '{1}'".format(calling_func, MyArg))
        self.logger.debug("{0} - MyErrOp: '{1}'".format(calling_func, MyErrOp))
        self.logger.debug("{0} - MyPassExitCodes: '{1}'".format(calling_func, MyPassExitCodes))
        MySrc = MySrc.strip().rstrip("\\") if MySrc is not None else ""
        MyDest = MyDest.strip().rstrip("\\") if MyDest is not None else ""
        MyWait = 30 if MyWait is None else MyWait
        MyFile = MyFile.strip() if MyFile is not None else ""
        MyRetry = 100 if MyRetry is None else MyRetry
        MyOpen = MyOpen.strip().upper() if MyOpen is not None else ""
        MyArg = MyArg.strip().upper() if MyArg is not None else ""
        MyErrOp = MyErrOp.strip().upper() if MyErrOp is not None else ""

        if type(MyPassExitCodes) is not list:
            errMsg = "MyPassExitCodes should be a list of Exit Codes(numbers) E.g., [13, 14, 15] "
            raise Exception(errMsg)
        else:
            SPFRoboCopy_pass_ExitCode = SPFRoboCopy_pass_ExitCode + MyPassExitCodes
            self.logger.debug("{0} - updated : SPFRoboCopy_pass_ExitCode: '{1}'".format(calling_func, SPFRoboCopy_pass_ExitCode))


        if len(MyFile) == 0 :
            self.Console("  No Files specified to copy. Exiting ...")
        elif len(MySrc) == 0 :
            self.Console("  No Source Path specified. Exiting ...")
        elif len(MyDest) == 0 :
            self.Console("  No Destination Path specified. Exiting ...")
        #elif os.path.isdir(MySrc) == False :
        #'===v30.59  ===========
        elif self.SHisSHEntry is False and  legacy.os.path.isdir(MySrc) is False : #' Folder ~exist for Interactive runs as SH may have network issues which robocopy will retry for
            self.Console("  Source path not found ({0}). Exiting ...".format(MySrc))
            self.gMyAbort = True
        else :
            if MyOpen != "Y" : MyOpen = "N"
            if MyErrOp != "Y" : MyErrOp = "N"
            if MySrc in ["\\", ".\\"] : MySrc = "."
            if MyDest in ["\\", ".\\"] : MyDest = "."

            #print MyRetry.isdigit()
            try :
                MyRetry = int(MyRetry)
                MyWait = int(MyWait)
            except Exception as err:
                self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))

            if type(MyRetry) != int:
                self.Console("  Retry Argument must be numeric ({0}). Exiting ...".format(MyRetry))
            elif type(MyWait) != int:
                self.Console("  Wait Argument must be numeric ({0}). Exiting ...".format(MyWait))
            elif MySrc == MyDest :
                self.Console("  Source and Destination folders are the same ({0}). Exiting ...{0}".format(MySrc))
            else : #proceed
                if MyFile.find("*") != -1 or MyFile.find(",") != -1 :
                    if MyOpen == "Y" or MyErrOp == "Y" :
                        self.Console("Sorry but neither the Open test nor the Abort if source file not found test can be done if wildcards are used or multiple files are to be copied")
                        MyOpen = "N"

                if MyOpen == "Y" or MyErrOp == "Y" :
                    Tmp = TmpD = MyFile

                    if MySrc != "." :
                        Tmp = legacy.os.path.join(MySrc, Tmp) #MySrc + "\\" + Tmp
                    if MyDest != "." :
                        TmpD = legacy.os.path.join(MyDest, TmpD) #MyDest + "\\" + TmpD

                    file_Exists_Retry, MyModStr = self.File_Exists_Retry(Tmp, MyErrOp)

                    if file_Exists_Retry is False :
                        #self.Console("  Source File does not exist : {0}".format(Tmp))
                        errMsg = "  Source File does not exist : {0}".format(Tmp)
                        if MyErrOp == "Y" :
                            errMsg = "{0}\n  RoboCopy will exit with an error...".format(errMsg)
                            self.gMyAbort = True
                            raise Exception(errMsg)
                        else :
                            self.Console("  Copy will be skipped ...")
                            return #exit sub
                    elif MyOpen == "Y" :
                        #if self.FileIsLocked(Tmp) is True :
                        #    errMsg = "   Source File is currently open : {0}\n   RoboCopy will exit with an error...".format(Tmp)
                        #    self.gMyAbort = True
                        #    raise Exception(errMsg) #return #exit Sub
                        fileRetry = True
                        maxFileRetryCount = 5 #100
                        myFileRetryCount = 0
                        fileRetrySleep = 15#30 #seconds
                        isMyFileLocked= False
                        while fileRetry is True:
                            isMyFileLocked = self.FileIsLocked(Tmp)
                            if isMyFileLocked == True:
                                myFileRetryCount = myFileRetryCount + 1
                                if myFileRetryCount > maxFileRetryCount :
                                    fileRetry = False
                                    self.Console("      File appears locked even after {0} retries".format(maxFileRetryCount))
                                else:
                                    self.Console("      File appears locked ... Retry {0} ... {1}".format(myFileRetryCount, self.DatetimeNow))
                                    legacy.time.sleep(fileRetrySleep)
                            else:
                                fileRetry = False
                        #END : while fileRetry == True
                        if isMyFileLocked == True:
                            errMsg = "   Source File is currently open : {0}\n   RoboCopy will exit with an error...".format(Tmp)
                            self.gMyAbort = True
                            raise Exception(errMsg) #return #exit Sub
                    #end : if + elif : file_Exists_Retry == False :

                #End : if MyOpen == "Y" or MyErrOp == "Y" :

                lMyArrRC = MyFile.replace('"','').strip().split(",")
                del MyFile #no longer need this

                self.logger.debug("Before clean up MyFilesArry -lMyArrRC : {0}".format(lMyArrRC))
                #remove empty items
                #'space is dlm. " = !!!
                lMyArrRC = ['"{0}"'.format(item.strip()) for item in lMyArrRC if self.IsEmptyOrNone(item) is False]

                self.logger.debug("After clean up MyFilesArry - lMyArrRC :{0}".format(lMyArrRC))

                if len(lMyArrRC) == 0 :
                    self.Console("  No Files specified to copy. Exiting ...")
                else :
                    #run command
                    myCMDToExecute = "ROBOCOPY"

                    myCMDArgs = ['"{0}"'.format(MySrc),
                                 '"{0}"'.format(MyDest)] + lMyArrRC + ['/R:{0}'.format(MyRetry),
                                 '/W:{0}'.format(MyWait)]
                                                            #'/W:{0}'.format(MyWait),
                                                            #"/NP",
                                                            #"/IS"]

                    if MyArg is not None and len(MyArg.strip()) != 0 :
                        if type(MyArg) is str: # types.StringType :
                            MyArg = MyArg.split(' ')
                        myCMDArgs = myCMDArgs + MyArg

                    self.logger.debug(myCMDToExecute)
                    self.logger.debug(myCMDArgs)
                    runVal, runExitCode = False, -1
                    try :
                      # [Removed from current version] runVal, runExitCode =  self.Run(myCMDToExecute, myCMDArgs, SPFRoboCopy_pass_ExitCode, CMDErrorExitCodes=SPFRoboCopy_err_ExitCode,usePopen=True)
                      if legacy.os.name != "nt":
                          from scripthost_portable.file_operations import robocopy_files
                          try:
                              runExitCode = robocopy_files(MySrc, MyDest, lMyArrRC, MyRetry, MyWait, MyArg or [])
                              runVal = runExitCode in SPFRoboCopy_pass_ExitCode
                          except (OSError, ValueError) as err:
                              self.Console(str(err))
                              raise legacy.SPFCMDRunExitWithErrorCodeException(str(err), 16, False) from err
                      else:
                          runVal, runExitCode = self.Run(myCMDToExecute, myCMDArgs, SPFRoboCopy_pass_ExitCode, CMDErrorExitCodes=SPFRoboCopy_err_ExitCode,usePopen=True)
                    except Exception as err:
                        self.logger.exception("Error : {0}".format(err))
                        runVal = err.args[2]
                        runExitCode = err.args[1]
                        #raise

                    self.Console("{0} - ExitCode : {1} ".format(SPFRoboCopyExitCode[runExitCode], runExitCode))
                    if runVal is False :
                        errMsg = "   RoboCopy considered unsuccessful..."
                        self.gMyAbort = True
                        raise Exception(errMsg)
                    elif MyErrOp == "Y" :
                        MyDModStr = self.GetFileDate(TmpD)
                        self.logger.debug("{0} - MyDModStr: '{1}'".format(calling_func, MyDModStr))
                        #if MyModStr != MyDModStr : '===== v30.86 ========== -- replace with below line
                        if str(MyDModStr).find("1980-01-01") > -1 or MyDModStr == -1:
                            errMsg = ("   Source and Destination File Dates do not match..."
                                      "\n     Source     = {MyModStr}"
                                      "\n     Destination= {MyDModStr}"
                                      "\n   RoboCopy considered unsuccessful...").format(MyModStr=MyModStr, MyDModStr=MyDModStr)
                            self.gMyAbort = True
                            raise Exception(errMsg)
                        else :
                            #self.Console("   Source and Destination File Dates Match: %s" % str(MyModStr))#'===== v30.86 ========== -- replaced with below line
                            self.Console("   Acceptable Destination (and source) File Dates: {MyDModStr} ({MyModStr})".format(MyDModStr=MyDModStr, MyModStr=MyModStr))
                            SPFRoboCopyStatus = True
                    else :
                        SPFRoboCopyStatus = True
                self.ConsoleDoneWithoutTimeStamp()
            #end : else : #proceed
            return SPFRoboCopyStatus

    def File_Lock_Move(self, srcFile, DstFile):
        """
        '===========================
        'Move File. If Locked, retry
        '
        'INPUT ARGS:
        '-----
        'SrcFile : Src File
        'DstFile : Dst File
        ' 
        'ON ERROR:
        ' raise error
        '============================
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        File_Lock_Move_Status = True #is locked
        maxRetryCount = 5 #100
        myRetryCount = 1
        retrySleep = 30 #seconds

        self.logger.debug("{0} - srcFile : {1}".format(calling_func, srcFile))
        self.logger.debug("{0} - DstFile : {1}".format(calling_func, DstFile))
        self.logger.debug("{0} - os.path.exists(DstFile) : {1}".format(calling_func, legacy.os.path.exists(DstFile)))
        try :
            self.ConsoleWithTimeStampNoLF("  Preparing to Rename")
            if legacy.os.path.exists(DstFile) :
                self.logger.debug("{0} - DstFile exists : {1}".format(calling_func, legacy.os.path.exists(DstFile)))

            retry = True
            while retry is True :
                try:
                    if self.FileIsLocked(DstFile) is True:
                        #self.Console("      Dest. File locked ... Retry {0} ... {1}".format(myRetryCount, self.DatetimeNow))
                        errMsg = "      Dest. File locked ... Retry {0} ... {1}".format(myRetryCount, self.DatetimeNow)
                        finalErrMsg = "      File is locked even after {0} retries".format(maxRetryCount)
                        raise Exception(errMsg)
                    else :
                        self.Console("     Renaming ... {0}".format(self.DatetimeNow))
                        cmdToExecute = "%COMSPEC%"
                        cmdArgs = ['/c', 'move', '/Y', '"{0}"'.format(srcFile), '"{0}"'.format(DstFile)]
                        try:
                            # [Removed from current version] File_Lock_Move_Status, runExitCode = self.Run(cmdToExecute, cmdArgs)
                            if legacy.os.name != "nt":
                                try:
                                    legacy.shutil.move(srcFile, DstFile)
                                    File_Lock_Move_Status, runExitCode = True, 0
                                except OSError as err:
                                    raise legacy.SPFCMDRunExitWithErrorCodeException(str(err), 1, False) from err
                            else:
                                File_Lock_Move_Status, runExitCode = self.Run(cmdToExecute, cmdArgs)
                        except legacy.SPFCMDRunExitWithErrorCodeException as SPFCmdExErr:
                            errMsg = ("      Error during move "
                                      "\n      srcFile : {0}"
                                      "\n      to "
                                      "\n      DstFile : {1} file. "
                                      "\n      ... Retry {2} ... {3}").format(srcFile, DstFile, myRetryCount, self.DatetimeNow)
                            finalErrMsg = ("      Error during move "
                                           "\n      srcFile : {0}"
                                           "\n      to "
                                           "\n      DstFile : {1} file. "
                                           "\n      Even after {2} retries").format(srcFile, DstFile, myRetryCount, self.DatetimeNow)
                            #"Error during move srcFile : {0} to DstFile : {1} file. even after {2} retries".format(srcFile, DstFile, maxRetryCount)
                            raise Exception(errMsg)
                    self.Console("     Renamed ... {0}".format(self.DatetimeNow))
                    retry = False
                except Exception as err:
                    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))

                    myRetryCount = myRetryCount + 1
                    legacy.time.sleep(retrySleep)
                    if myRetryCount > maxRetryCount :
                        retry = False
                        #errMsg = "File is locked even after {0} retries"
                        self.ConsoleWithCons80(finalErrMsg)
                        raise Exception(finalErrMsg)
                    self.ConsoleWithCons80(err.args[0])

            #END : while retry == True


        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def setEnv(self, EnvVarName, EvnVarValue) :
        """
        '====================================================
        'set Environment variables for this session
        '
        'INPUT ARGS:
        '-----------
        ' 1.) EnvVarName : StringType : Env't variable name.
        ' 2.) EvnVarValue : StringType : Env't variable Value. 
        '
        'OUTPUT : (single)
        '----------------
        ' 1.) setEnvStatus : BooleanType : True if Env't var was set ELSE False
        '
        'ON ERROR : (no output)
        '----------
        ' 1.) raise exception
        '
        '----------- 
        ' HISTORY :
        '-----------
        '   Ver         WW'YY           Author      Description
        '   ----        -----           ------      -----------
        '   1.0         WW29.5'15       Nataraj     Initial Version
        '   1.1         WW11.4'20       Nataraj     Update console statement post createing environment variable
        '======================================================
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)

        setEnvStatus = True
        try :
            self.logger.debug("{0} - EnvVar Name : '{1}'; Value : '{2}'; valueType : '{3}' ".format(calling_func, EnvVarName, EvnVarValue, type(EvnVarValue)))

            if legacy.os.name != "nt":
                # Scripts rely on Windows' case-insensitive env names (set RowsInFile, test ROWSINFILE).
                for existingName in [k for k in legacy.os.environ if k.upper() == EnvVarName.upper()]:
                    del legacy.os.environ[existingName]
                legacy.os.environ[EnvVarName.upper()] = str(EvnVarValue)
            legacy.os.environ[EnvVarName] = str(EvnVarValue)

            self.logger.debug("{0} - Env Var set : {1} = {2}".format(calling_func, EnvVarName, legacy.os.environ[EnvVarName]))

        except Exception as err:
            setEnvStatus = False
            self.logger.exception("{0} - ".format(calling_func, err.args[0]))
            raise

        return setEnvStatus

    def unzipString(self, inputStringToDeCompress) :
        """
        ' Helper function decompress a compressed string
        ' Note: on error : return input string as is 
        """
        #local variables
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)

        try :
            self.logger.info("{0} - len(inputStringToDeCompress) : {1}".format(calling_func, len(inputStringToDeCompress)))
            #self.logger.debug("{0} - inputStringToDeCompress : {1}".format(calling_func, inputStringToDeCompress))
            if legacy.isPYTHON2:
                decompressedOutputString = legacy.zlib.decompress(legacy.base64.standard_b64decode(inputStringToDeCompress), legacy.zlib.MAX_WBITS|32).replace("\r\n", "\n")
            else:
                #decompressedOutputString = zlib.decompress(base64.standard_b64decode(inputStringToDeCompress), zlib.MAX_WBITS|32).replace("\r\n", "\n")
                #decompressedOutputString = zlib.decompress(base64.standard_b64decode(inputStringToDeCompress), zlib.MAX_WBITS|32).decode(encoding=self.gOSDefaultEncoding).replace("\r\n", "\n")
                __t = legacy.zlib.decompress(legacy.base64.standard_b64decode(inputStringToDeCompress), legacy.zlib.MAX_WBITS|32)
                # [Removed from current version] decompressedOutputString =  __t.decode(encoding=self.detectCharacterEncoding(__t)).replace("\r\n", "\n")
                try:
                    decompressedOutputString = __t.decode("utf-8").replace("\r\n", "\n")
                except UnicodeDecodeError:
                    decompressedOutputString = __t.decode(encoding=self.detectCharacterEncoding(__t)).replace("\r\n", "\n")

            del inputStringToDeCompress

            self.logger.info("{0} - len(decompressedOutputString) : {1}".format(calling_func, len(decompressedOutputString)))
            #self.logger.debug("{0} - decompressedOutputString : {1}".format(calling_func, decompressedOutputString))

            return decompressedOutputString
        except Exception as err:
            self.logger.warn("{0} - Error in unzipString : {1}".format(calling_func, err))
            return inputStringToDeCompress #if error return back input string

    def SPFDelete(self, MySrc, quietMode=True, forceDelete=True, displayPrompt=True) :
        """v2.5
        '==================================================================
        'Deletes files in a folder. Wildcards may
        'be used. If a dir is specified,
        'all files within folder will be deleted
        '
        'Arg:
        '===
        'MySrc: File to delete. *=wild card
        'MyOpt: Y to not prompt if DEL *.*
        '==================================================================
        """
        #local variables
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - To delete MySrc: {1}".format(calling_func, MySrc))
        self.logger.debug("{0} - quietMode: {1}".format(calling_func, quietMode))
        self.logger.debug("{0} - forceDelete: {1}".format(calling_func, forceDelete))
        cmdRunPassCodes = [0]
        cmdToRun = "%COMSPEC%"
        cmdArgsList = ["/c"]
        try :

            if displayPrompt is True :
                self.ConsoleWithTimeStamp("Starting Delete Applet, v2.5")

            if self.IsEmptyOrNone(MySrc) is True :
                self.Console("No delete files specified ...")
                return

            rdr = legacy.csv.reader(legacy.StringIO(MySrc),
                             delimiter=",",
                             skipinitialspace=True)

            #MySrc = ",".join(['"{0}"'.format(re.sub("<c>", ",", item1.strip('"'), re.IGNORECASE)) for item1 in (rdr.next() if isPYTHON2 else next(rdr)) if item1.strip('\'" ') != ""])
            MySrc = ",".join(['"{0}"'.format(legacy.re.sub("<c>", ",", item1.strip('"'), legacy.re.IGNORECASE)) for item1 in next(rdr) if item1.strip('\'" ') != ""])
            self.logger.debug("{0} - parsed MySrc: {1}".format(calling_func, MySrc))
            if self.IsEmptyOrNone(MySrc) is True :
                self.Console("No delete files specified ...")
                return

            if legacy.os.name != "nt":
                from scripthost_portable.file_operations import delete_files
                delete_files(next(legacy.csv.reader(legacy.StringIO(MySrc), skipinitialspace=True)), forceDelete)
                if displayPrompt:
                    self.ConsoleDoneWithoutTimeStamp()
                return

            #start building args to run in DOS
            FileToDeleteFound = []
            cmdArgsList.append("&&Del")

            if forceDelete is True :
                cmdArgsList.append("/F")

            if quietMode is True :
                cmdArgsList.append("/Q")
                if displayPrompt is True :
                    self.Console(' Deleting "{0}" with No Prompt for *.*, including Read-Only Files ...'.format('","'.join(MySrc.split(","))))
            else :
                if displayPrompt is True :
                    self.Console(' Deleting "{0}"'.format(MySrc))

            FileToDeleteFound.append(MySrc)


            cmdArgsList = cmdArgsList + FileToDeleteFound
            try :
                Final_CleanUpStatus, runExitCode = self.Run(cmdToRun, cmdArgsList, cmdRunPassCodes, usePopen=True)
                self.logger.debug("{0} - deleted File : {1}".format(calling_func, MySrc))
            except Exception as err:
                pass # no action needed pass

            if displayPrompt is True :
                self.ConsoleDoneWithoutTimeStamp()
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def ConvertDLM (self, InFile, OutFile, CvtExe, IsQuiet=True) :
        """
        '================================================
        'Cleans a delimited file. Removes double quotes and replaces commas & tabs
        'with semicolons & spaces. Embedded CR & LF are also replaced with spaces
        '
        'INPUT Args:
        '-----------
        'MyInstance: SPF Instance  #dropped in py : use self.gSPFInstance
        'MyLocal : N for remote run or path to the SPF files #Dropped in py #VA30_47 --> 'MyLocal : Exe dir or N for remote Q
        'InFile  : File to process
        'OutFile : Cleaned File to create
        'CvtExe  : Path to Conversion Exe
        'IsQuiet : True ("Y") to do quietly
            default = True
        'OUTPUT : 
        '-------
        ' None
        '================================================
        """
        if legacy.os.name != "nt":
            from scripthost_portable.file_operations import clean_delimited_file
            clean_delimited_file(InFile, OutFile, self.GetFileDLM(InFile))
            return

        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        MyLog = r".\CleanDelimsCRLF.log"
        self.logger.debug("{0} - InFile : {1}".format(calling_func, InFile))
        self.logger.debug("{0} - OutFile : {1}".format(calling_func, OutFile))
        self.logger.debug("{0} - CvtExe : {1}".format(calling_func, CvtExe))
        self.logger.debug("{0} - IsQuiet : {1}".format(calling_func, IsQuiet))

        try :
            InFilePath, InFileName = legacy.os.path.split(InFile)
            self.logger.debug("{0} - InFilePath : {1}".format(calling_func, InFilePath))
            self.logger.debug("{0} - InFileName : {1}".format(calling_func, InFileName))

            CvtExeFilePath, CvtExeFileName = legacy.os.path.split(CvtExe)
            self.logger.debug("{0} - CvtExeFilePath : {1}".format(calling_func, CvtExeFilePath))
            self.logger.debug("{0} - CvtExeFileName : {1}".format(calling_func, CvtExeFileName))
            if IsQuiet is False :
                self.Console(" Preprocessing {0}...{1}".format(InFileName, self.DatetimeNow))

            #MyInstance = "TClean{0}_{1}_{2}".format(self.gSPFInstance, self.RandomNumStr, self.RandomNumStr)

            self.DelAFile(MyLog)
            self.DelAFile(OutFile)
            #setup the args that need to be passed to CvtExe
            DOSCommandArgs = ['"{0}"'.format(item) for item in [InFile, OutFile]]
            if self.SHisSHEntry is True : #'SH
                DOSCommand = legacy.os.path.join(self.gTempDir, CvtExeFileName)
                #DOSCommandArgs.append(self.gLocalDir) # this is not needed as the Exe is directly invoked and the log is checked...removed dependency on 'Run_CleanDelimsCRLF.bat'
            elif self.gMyLocal == "N" :
                DOSCommand  = CvtExe
            else : #'Interactive or /SPFEXE
                DOSCommand  = CvtExe
                #DOSCommandArgs.append(self.gLocalDir) # this is not needed as the Exe is directly invoked and the log is checked...removed dependency on 'Run_CleanDelimsCRLF.bat'

            self.logger.debug("{0} - DOSCommand : {1}".format(calling_func, DOSCommand))
            self.logger.debug("{0} - DOSCommandArgs : {1}".format(calling_func, DOSCommandArgs))
            legacy.os.environ["MyLog"] = "CleanDelimsCRLF.log"
            self.logger.debug("{0} - os.environ['MyLog'] : {1}".format(calling_func, legacy.os.environ["MyLog"]))
            runStatus, runCode = self.Run(DOSCommand, DOSCommandArgs,CMDErrorExitCodes=[1],usePopen=True)

            #delete the log file...no longer needed
            self.DelAFile(MyLog)
            #check if OutFile is generated
            self.logger.debug("{0} - os.path.isfile(OutFile) : {1}".format(calling_func, legacy.os.path.isfile(OutFile)))
            if legacy.os.path.isfile(OutFile) is False :
                self.SPFCopy(InFile, OutFile, False) #'===== v30.79 ========== dont raise error
                #raise Exception("OutFile not generated")

        except Exception as err:
            errMsg = err.args[0]
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))

            if legacy.os.path.exists(MyLog) is True :
                try :
                    #read the log file for error message
                    with open(MyLog, 'r') as fileToRead: #no need to handle for Python 3.x encoding issues
                        errMsg = fileToRead.read()
                    self.DelAFile(MyLog)
                except Exception as err:
                    self.logger.exception("{0} - Error while trying to read MyLog file {1}".format(calling_func, err.args[0]))
                    pass

            self.gMyAbort = True
            raise Exception(errMsg)

    def IntelWW(self, date) :
        r"""
        'Note: Migrated from MBTools\scripts\lib\intel.va 
        'adapted from faacc1::access$library:ACC_DTR_INTEL_WORKWEEK.PAS
        'Directory FAACC1::ACCESS:[LIBRARY]
        'ACC_DTR_INTEL_WORKWEEK.PAS;7    16/18   3-SEP-1998 09:54:19.00
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        #remove the timepart from date
        date = date.date()
        #self.logger.debug("{0} - date: '{1}'".format(calling_func, date))
        yyyy = date.year
        ww = None
        jan1 = None
        dow = None
        #self.logger.debug("{0} - yyyy: '{1}'".format(calling_func, yyyy))
        #self.logger.debug("{0} - date.month: '{1}'".format(calling_func, date.month))
        # [Removed from current version] #new set locale to en_US -- and reset back to defaultlocale
        # [Removed from current version] import locale
        # [Removed from current version] defaultLocale = locale.setlocale(locale.LC_ALL,'')
        # [Removed from current version] #self.logger.debug("{0} -defaultLocale: '{1}'".format(calling_func, defaultLocale))

        # [Removed from current version] usLocale = 'English_United States.1252'
        # [Removed from current version] usLocaleSet = locale.setlocale(locale.LC_ALL,usLocale)
        # [Removed from current version] #self.logger.debug("{0} -updated locale: '{1}'".format(calling_func, locale.getlocale()))

        try :
            if date.month == 12 :
                jan1 = date.day - 32
                #self.logger.debug("{0} - jan1: '{1}'".format(calling_func, jan1))

                dow = date.isoweekday() #+ 1 #removed  + 1 not needed after removing the timepart from date input
                #self.logger.debug("{0} - dow: '{1}'".format(calling_func, dow))

                if dow < 7 :
                    jan1 = jan1 - dow
                    #self.logger.debug("{0} - jan1: '{1}'".format(calling_func, jan1))

                if jan1 > -7 :
                    yyyy = yyyy + 1
                    #self.logger.debug("{0} - yyyy: '{1}'".format(calling_func, yyyy))
                    ww = 1
                self.logger.debug("{0} - date.month == 12 : ww: '{1}'".format(calling_func, ww))
            if ww is None:
                # [Removed from current version] jan1 = datetime.strptime("1-JAN-{0}".format(yyyy),"%d-%b-%Y").date() #just get the datepart
                jan1 = legacy.datetime(yyyy, 1, 1).date() # locale-independent equivalent of 1-JAN-yyyy
                #self.logger.debug("{0} - jan1: '{1}'".format(calling_func, jan1))

                dow = jan1.isoweekday() #+ 1  #removed  + 1 not needed after removing the timepart from date input
                #self.logger.debug("{0} - dow: '{1}'".format(calling_func, dow))

                if dow < 7 :
                    jan1 = jan1 - legacy.timedelta(days=dow)
                    #self.logger.debug("{0} - jan1: '{1}'".format(calling_func, jan1))

                ww = (((date - jan1).days) / 7) + 1
                self.logger.debug("{0} - ww: '{1}'".format(calling_func, ww))

            intelww_output = int((yyyy * 100) + ww) #Py3.x requires conversion to int
            self.logger.debug("{0} - intelww_output: '{1}'".format(calling_func, intelww_output))

            return str(intelww_output)
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err))
            raise

    def Run_R(self, MyMode, Command1, MyLocal, WorkDir, ll_AppSvr, MyShowSQL=False, MyShowOut=False, MyArgs="", MyOS="W") :
        """
        '==========================================================
        'Runs R on Windows or Linux
        '
        'ARG:
        '===
        'MyMode   : S if Command1 is a Script and F if a file path
        'Command1 : R Script to run
        'MyLocal  : Local Exe folder (ending with a /) for local query, or N for remote 
        '           query using the SPF/ACT.. public File Share
        'WorkDir  : Default Folder
        'MyShowSQL: If script should be shown
        'MyShowOut: Whether to Show charts after generation
        'MyArgs   : Opt. Arg. to Pass to R job
        'MyOS     : L for Linux
        'll_AppSvr:Linux ApplicatioN Server for running R
        '
        'GLOBALS:
        '--------
        'gMyAbort      :Global-Job Abort Variable
        'gRPathSH      :Path to nearest SH R share when on SH
        'gRPath        :Path to readable R
        'gLocalDir     :Local work dir
        'gSPFVaryLib   :SPF lib depending how run
        '==========================================================
        """

        def Run_R_W(MyMode, Command1, MyLocal, WorkDir, ll_AppSvr, MyShowSQL=False, MyShowOut=False, MyArgs="") :
            """
            Run R Script/File on Windows OS
            """
            self.logger.debug("{0} - MyMode: '{1}'".format(calling_func, MyMode))
            self.logger.debug("{0} - Command1: '{1}'".format(calling_func, Command1))
            self.logger.debug("{0} - MyLocal: '{1}'".format(calling_func, MyLocal))
            self.logger.debug("{0} - WorkDir: '{1}'".format(calling_func, WorkDir))
            self.logger.debug("{0} - MyShowSQL: '{1}'".format(calling_func, MyShowSQL))
            self.logger.debug("{0} - MyShowOut: '{1}'".format(calling_func, MyShowOut))
            self.logger.debug("{0} - MyOS: '{1}'".format(calling_func, MyOS))
            self.logger.debug("{0} - MyArgs: '{1}'".format(calling_func, MyArgs))
            self.logger.debug("{0} - ll_AppSvr: '{1}'".format(calling_func, ll_AppSvr))
            #locals
            MyOut = ""
            try :
                MyShowOut = False
                """
                 '==========
                 'Get R-Path
                 '==========
                """
                if self.SHisSHEntry is True :
                    MyAppPath = self.gRPathSH
                elif MyLocal == "N" :
                    MyAppPath = self.gRPath
                elif MyLocal != "N" :
                    MyAppPath = self.GetIni(legacy.os.path.join(MyLocal, "SQLPathFinder.ini"), "SQLPATHFINDER", "RTERM").strip() #''Local or EE is installed

                self.logger.debug("{0} - MyAppPath: '{1}'".format(calling_func, MyAppPath))

                if (self.IsEmptyOrNone(MyAppPath) is True
                    or legacy.os.path.exists(MyAppPath) is False) :
                    errMsg = ("Could not find your local R install. To install R from the SQLPathFinder\n"
                              "interface, choose menu option Tools->Update/Install R and follow\n"
                              "instructions. If you already have R installed, simply reference it in the\n"
                              "Configure SQLPathfinder form. If you are running the SQLPathFinder Extract\n"
                              "Engine, add RTERM=<path to Rterm.exe> to file SQLPATHFINDER.INI in the\n"
                              "Extract Engine install folder. E.g.,:\n"
                              "\n"
                              "[SQLPATHFINDER]\n"
                              "RTERM=C:\\Program Files\\R\\R-2.15.1\\bin\\i386\\RTerm.exe\n"
                              "")
                    self.gMyAbort = True
                    raise Exception(errMsg)

                else:
                    RDefPath=legacy.os.path.join(MyLocal.upper(), r"R\R-LATEST\BIN\X64\RTERM.EXE")
                    if self.SHisSHEntry is False and MyLocal != "N" and MyAppPath.upper() == RDefPath.upper():
                        #when using SPF's R install,  set the R_LIBS env var to point to the SPF-R library so SPF libraries are first used, if present
                        legacy.os.environ["R_LIBS"] =legacy.os.path.join(MyLocal, r"R\R-LatestR\Library")

                self.Console("R Path={0}\n".format(MyAppPath))
                if MyMode == "S" :
                    """
                    '======================
                   'Substitute def. Folder
                   '======================
                    """
                    MyDefDir = legacy.os.path.abspath(".")
                    self.logger.debug("{0} - MyDefDir: '{1}'".format(calling_func, MyDefDir))
                    MyDefDir = MyDefDir.replace("\\", "/") #'Reverse slashes for R
                    MyDefExe = self.gSPFExe.replace("\\","/")
                    #Command1 = Command1.replace("$spf$dir$", "{0}\\".format(self.gSPFVaryLib.replace("\\", "/")))
                    Command1 = legacy.re.sub(r"\$spf\$dir\$(?P<source>.*\.r)",
                                      lambda m : legacy.os.path.join(self.gSPFVaryLib, m.group("source")).replace("\\", "/"),
                                      Command1, 0, legacy.re.IGNORECASE)
                    # [Removed from current version] q2 = os.path.join(WorkDir, "sqlpathfinder.R")
                    q2 = legacy.os.path.join("." if legacy.os.name != "nt" and WorkDir == ".\\" else WorkDir, "sqlpathfinder.R")
                    q2 = legacy.os.path.abspath(q2)
                    self.logger.debug("{0} - q2: '{1}'".format(calling_func, q2))

                    Command1 = Command1.replace("$default$dir$", MyDefDir if MyDefDir[-1] == "/" else "{0}/".format(MyDefDir))
                    Command1 = Command1.replace("$default$exe$", MyDefExe if MyDefExe[-1] == "/" else "{0}/".format(MyDefExe))
                    self.logger.debug("{0} - Command1: '{1}'".format(calling_func, Command1))

                    """
                    '===========
                    'Write script
                    '===========
                    """
                    self.DoCreateFileA(q2, Command1, "Write-File")
                    #raise Exception("Run_R : Run_R_W debug 01")
                    if MyShowSQL is True :
                        self.Console("\n{0}".format(Command1))

                    """
                    '=======================
                    'Get o/p file for display
                    '=======================
                    """
                    MyOut = ""
                    try :
                        MyOut = Command1.split(MyToken2)[1].strip()
                        self.logger.debug("{0} - MyOut : '{1}'".format(calling_func, MyOut))
                        self.DelAFile(MyOut)
                    except Exception as err:
                        pass #no o/p file for display...
                elif MyMode == "F" :
                    q2 = legacy.os.path.abspath(Command1)
                    if legacy.os.path.exists(q2) is False :
                        errMsg = "Could not locate R Script to run: {0}".format(q2)
                        raise Exception(errMsg)
                """
                '===
                'Run
                '===
                """

                MySrcExe = MyAppPath
                CmdArgs = ["-q", "--vanilla", "--slave", '--file="{0}"'.format(q2), MyArgs]
                runStatus, runExitCode = self.Run(MySrcExe, CmdArgs, usePopen=True)
                return runExitCode, q2, MyOut
            except Exception as err:
                self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                raise

        def Run_R_L(MyMode, Command1, MyLocal, WorkDir, ll_AppSvr, MyShowSQL=False, MyShowOut=False, MyArgs="") :
            """
            Run R Script/File on Linux OS
            """
            self.logger.debug("{0} - Command1: '{1}'".format(calling_func, Command1))
            self.logger.debug("{0} - MyMode: '{1}'".format(calling_func, MyMode))
            self.logger.debug("{0} - MyLocal: '{1}'".format(calling_func, MyLocal))
            self.logger.debug("{0} - WorkDir: '{1}'".format(calling_func, WorkDir))
            self.logger.debug("{0} - MyShowSQL: '{1}'".format(calling_func, MyShowSQL))
            self.logger.debug("{0} - MyShowOut: '{1}'".format(calling_func, MyShowOut))
            self.logger.debug("{0} - MyOS: '{1}'".format(calling_func, MyOS))
            self.logger.debug("{0} - MyArgs: '{1}'".format(calling_func, MyArgs))
            self.logger.debug("{0} - ll_AppSvr: '{1}'".format(calling_func, ll_AppSvr))
            #locals
            MyOut = ""
            MyDefExe = ""
            try :
                """
                 '============
                 'Get Exe Path
                 '============
                """
                if self.SHisSHEntry is True :
                    ExePath = self.gTempDir
                elif MyLocal == "N" :
                    ExePath = self.gSPFLib
                else : #'local dir
                    ExePath = MyLocal.strip()

                MyAppPath="R on Linux"

                if (MyShowOut is False
                    and
                    Command1.find(MyToken3) > -1) :
                    MyShowOut = True

                ll_AppSvr = ll_AppSvr.strip().lower()
                if self.IsEmptyOrNone(ll_AppSvr) is False :
                    l_LNode = self.GetIni(legacy.os.path.join(ExePath, "hadoop_linux_config.ini"), ll_AppSvr, "node").strip() # 'Linux App svr
                    lwinCoShare = self.GetIni(legacy.os.path.join(ExePath, "hadoop_linux_config.ini"), ll_AppSvr, "win-cross-over-share").strip()
                    lLinCOShare = self.GetIni(legacy.os.path.join(ExePath, "hadoop_linux_config.ini"), ll_AppSvr, "linux-cross-over-share").strip()
                    lspflinux = self.GetIni(legacy.os.path.join(ExePath, "hadoop_linux_config.ini"), ll_AppSvr, "spf-linux-share").strip()

                if self.IsEmptyOrNone(ll_AppSvr) is True or self.IsEmptyOrNone(l_LNode) is True :
                    errMsg = ("An invalid Linux Application server was passed. Contact\n"
                              "SQLPathFinder if you need assistance. Exiting ...")
                    raise Exception(errMsg)
                elif (self.IsEmptyOrNone(lwinCoShare) is True
                      or self.IsEmptyOrNone(lLinCOShare) is True
                      or self.IsEmptyOrNone(lspflinux) is True) :
                    errMsg = ("Could not locate your Windows/Linux/SPF Cross-Over Share. Please\n"
                              "check that you have configured a valid App server. Exiting ...")
                    raise Exception(errMsg)
                elif (MyMode == "S"
                      and legacy.os.path.exists(legacy.os.path.join(lwinCoShare, self.gUN)) is False):
                    errMsg = ("Could not find your Windows/Linux Crossover File share. Contact\n"
                              "SQLPathFinder support if you wish to run R programs on Linux\n")
                    raise Exception(errMsg)

                if MyMode == "S" :
                    MyDefDir = "{0}{1}/".format(lLinCOShare, self.gUN)
                    self.logger.debug("{0} - MyDefDir: '{1}'".format(calling_func, MyDefDir))
                    Command1 = Command1.replace("$spf$dir$", lspflinux)
                    q2 = "{0}_sqlpathfinder.R".format(RNStr)
                    q3 = "{0}{1}/{2}".format(lLinCOShare, self.gUN, q2)
                    q2 = "{0}{1}\\{2}".format(lwinCoShare, self.gUN, q2)

                    Command1 = Command1.replace("$default$dir$", MyDefDir) #'Substitute Def Dir
                    self.logger.debug("{0} - Command1 : replaced $default$dir$ : '{1}'".format(calling_func, Command1))
                    Command1 = Command1.replace("{0}{1}\\".format(lwinCoShare, self.gUN).replace("\\", "/"), "{0}{1}/".format(lLinCOShare, self.gUN))
                    Command1 = Command1.replace("$default$exe$", MyDefExe)
                    self.logger.debug("{0} - Command1 : replace paths : '{1}'".format(calling_func, Command1))

                    """
                    '===========
                    'Write script
                    '===========
                    """
                    self.DoCreateFileA(q2, Command1, "Write-File")

                    if MyShowSQL is True :
                        self.Console("\n{0}".format(Command1))

                    """
                    '=======================
                    'Get o/p file for display
                    '=======================
                    """
                    MyOut = ""
                    try :
                        MyOut = Command1.split(MyToken2)[1].strip()
                        MyOut = "{0}{1}\\{2}".format(lwinCoShare, self.gUN, MyOut)
                        self.logger.debug("{0} - MyOut : '{1}'".format(calling_func, MyOut))
                        self.DelAFile(MyOut)
                    except Exception as err:
                        pass #no o/p file for display...


                elif MyMode == "F" :
                    q3 = Command1

                """
                '===
                'Run
                '===
                """

                MySrcExe = legacy.os.path.join(ExePath, "GetHadoop")
                CmdArgs = ['/NODE="{0}"'.format(l_LNode),
                           '/NODEGROUP="{0}"'.format(ll_AppSvr),
                           '/R="{0}"'.format(q3),
                           '/TT="N"',
                           '/Quiet="N"',
                           '/Instance="{0}"'.format("{0}_{1}".format(self.gUN, RNStr)),
                           '/Mode="CROSSOVER"'
                           ]
                #'===== v30.72 ========== Commented out
                #if self.SHisSHEntry is False :
                #    CmdArgs.append('/KeyFile="Y"')
                #'=======================

                runStatus, runExitCode = self.Run(MySrcExe, CmdArgs)

                legacy.time.sleep(5) # seconds
                if MyMode == "S" :
                    if legacy.os.path.exists(legacy.os.path.join("{0}{1}".format(lwinCoShare, self.gUN), "r.r$schema")) :
                        srcFile = legacy.os.path.join("{0}{1}".format(lwinCoShare, self.gUN), "r.r$schema")
                        destFile = legacy.os.path.join(self.gLocalDir, "r.r$schema")
                        self.logger.debug("{0} - srcFile : '{1}'".format(calling_func, srcFile))
                        self.logger.debug("{0} - destFile : '{1}'".format(calling_func, destFile))
                        legacy.shutil.copyfile(srcFile, destFile)
                        self.DelAFile(srcFile)

                return runExitCode, q2, MyOut
            except Exception as err:
                self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                raise

        #main body of Run_R starts from here
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - MyMode: '{1}'".format(calling_func, MyMode))
        self.logger.debug("{0} - Command1: '{1}'".format(calling_func, Command1))
        self.logger.debug("{0} - MyLocal: '{1}'".format(calling_func, MyLocal))
        self.logger.debug("{0} - WorkDir: '{1}'".format(calling_func, WorkDir))
        self.logger.debug("{0} - MyShowSQL: '{1}'".format(calling_func, MyShowSQL))
        self.logger.debug("{0} - MyShowOut: '{1}'".format(calling_func, MyShowOut))
        self.logger.debug("{0} - MyOS: '{1}'".format(calling_func, MyOS))
        self.logger.debug("{0} - MyArgs: '{1}'".format(calling_func, MyArgs))
        self.logger.debug("{0} - ll_AppSvr: '{1}'".format(calling_func, ll_AppSvr))
        MyAppPath = None
        MyToken2="#SPF-REQUIRED-OUT:"
        MyToken3="#DISPLAY-LINUX-CROSSOVER-=Y"
        RNStr = None
        #l_Node = None

        try :
            self.Console("Starting the R Applet, v1.4 ...")
            ##fix for occurance of "\r\n" messes up the R script -- not needed fixed in reading file to use universal line break option
            #if Command1.find("\r\n") != -1 :
            #    #Command1 = "\n".join(Command1.splitlines()) #--use this if below doesn't work
            #    Command1 = Command1.replace("\r\n", "\n")
            RNStr = self.RandomNumStr

            if self.IsEmptyOrNone(MyOS) is True :
                MyOS = "W"
            if MyMode.upper() != "F" :
                MyMode = "S"

            #'Optional R Args
            if self.IsEmptyOrNone(MyArgs) is False :
                MyArgs = " --args {0}".format(MyArgs.replace("'", '"'))
            else :
                MyArgs = ""
            self.logger.debug("{0} - MyArgs: '{1}'".format(calling_func, MyArgs))
            Command1 = self.Substitute_Std_Tokens(Command1, "0")

            if MyOS == "W" :
                runExitCode, q2, MyOut = Run_R_W(MyMode, Command1, MyLocal, WorkDir, ll_AppSvr, MyShowSQL, MyShowOut, MyArgs)
            elif MyOS == "L" :
                runExitCode, q2, MyOut = Run_R_L(MyMode, Command1, MyLocal, WorkDir, ll_AppSvr, MyShowSQL, MyShowOut, MyArgs)

            if MyLocal != "N" and MyShowOut is True and self.IsEmptyOrNone(MyOut) is False :
                q1 = legacy.os.path.join(MyLocal, "spfviewer.bat")
                CmdArgs = ['H', '"{0}"'.format(MyOut), '"{0}"'.format(self.gLocalDir)]
                q1RunStatus, q1RunExitCode = self.Run(q1, CmdArgs)

            if MyMode == "S" :
                self.DelAFile(q2)

            self.ConsoleDoneWithTimeStamp2()
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def SPFWebCopyPyReqs(self, MyLocal, MyURL, MyFile, lInter, lNoAbort, reqtimeout=(10, 60), returnDirectData=False, runSilent=False) :
        """
       '====================================================
        'Copy file from URL -- uses python requests, requests_negotiate_sspi, wincertstore library instead of "Get_Web_Text.exe"
        '
        'Args:
        '====
        'MyLocal: N for batch run
        'MyURL  : source 
        'MyFile : Path & Name of text File to create
        'lInter : Run Interactively Only
        'lNoAbort : Y:No Abort if Fail
        'reqtimeout : (connectTimeout, readTimeout) in seconds. default -> (10, 60)
        'returnDirectData : True/False (defualt - False) : True - ignore MyFile and return fetched data as method response
        'runSilent : True/False (default - False) : True : don't display console messages
        'GLOBALS:
        '-------
        'gTempDir : SH SPF SW 
        '
        'Invoke:
        '======
        ' ---To create local file from fetched output---
        ' Call SPFWebCopyPyReqs ("N","http://moss.amr.ith.intel.com/sites/TME_TMGT/ngdss/SQLPathFinderWork/test.csv","c:\\users\\<user>\\Test.csv",N, N)
        ' ---To return fetched data as method response---
        ' Call SPFWebCopyPyReqs ("N","http://moss.amr.ith.intel.com/sites/TME_TMGT/ngdss/SQLPathFinderWork/test.csv","c:\\users\\<user>\\Test.csv",N, N, True)
        '====================================================
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - MyLocal: '{1}'".format(calling_func, MyLocal))
        self.logger.debug("{0} - MyURL: '{1}'".format(calling_func, MyURL))
        self.logger.debug("{0} - MyFile: '{1}'".format(calling_func, MyFile))
        self.logger.debug("{0} - lInter: '{1}'".format(calling_func, lInter))
        self.logger.debug("{0} - lNoAbort: '{1}'".format(calling_func, lNoAbort))
        self.logger.debug("{0} - reqtimeout: '{1}'".format(calling_func, reqtimeout))
        self.logger.debug("{0} - returnDirectData: '{1}'".format(calling_func, returnDirectData))
        self.logger.debug("{0} - runSilent: '{1}'".format(calling_func, runSilent))

        myExePath = None
        try:
            try:
                if runSilent is False:
                    self.Console("Starting PyWeb Copy V1.1 ...")
                if not lNoAbort is True :
                    lNoAbort = False
                    self.logger.debug("{0} - lNoAbort: '{1}'".format(calling_func, lNoAbort))

                if self.SHisSHEntry is True : #'SH
                    if lInter == "Y" :
                        self.Cons80()
                        self.Console("Option 'Run Interactively' set. Exiting Job as running on ScriptHost ...")
                        self.Cons80()
                        return
                    myExePath = self.gTempDir
                elif MyLocal == "N" :
                    myExePath = self.gSPFVaryLib
                else : #'Interactive
                    myExePath = MyLocal

                self.logger.debug("{0} - myExePath: '{1}'".format(calling_func, myExePath))

                if self.IsEmptyOrNone(MyFile) is True or self.IsEmptyOrNone(MyURL) is True :
                    self.Cons80()
                    self.Console("Some input arguments are missing ...\nExiting ...")
                    self.Cons80()
                    return
                """
                '============================
                'Handle CommodityCost Reports
                '=============================
                """
                tmp1 = MyURL.lower()
                try :
                    if tmp1[39] == "http://commoditycost.intel.com/reports/" and tmp1[:-11] != "&output=tab" :
                        MyURL = "{0}&output=tab".format(MyURL)
                        self.logger.debug("{0} - MyURL: '{1}'".format(calling_func, MyURL))
                except Exception as err:
                    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))

                if runSilent is False:
                    self.Cons80()
                    self.Console(" Source File       : {0}".format(MyURL))
                    self.Console(" Destination       : {0}".format(MyFile))
                    self.Console(" Run Interactively : {0}".format(lInter))
                    self.Console(" Continue if fail  : {0}".format(lNoAbort))
                    self.Console(" Read Timeout      : {0} seconds".format(reqtimeout[1]))
                    self.Cons80()

                MyFile = legacy.os.path.abspath(MyFile) #'Get Abs Path of file
                tmpOrigProxy = None
                try :
                    tmpOrigProxy = legacy.os.environ['http_proxy']
                    self.logger.debug("{0} - tmpOrigProxy: '{1}'".format(calling_func, tmpOrigProxy))
                    legacy.os.environ['http_proxy'] = ''
                    self.logger.debug("{0} - os.environ['http_proxy']: '{1}'".format(calling_func, legacy.os.environ['http_proxy']))
                except Exception as err:
                    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))

                if runSilent is False:
                    self.Console("Retrieving {0} ...{1}".format(MyURL, self.DatetimeNow))

                import requests
                # [Removed from current version] from .requests_negotiate_sspi import HttpNegotiateAuth

                auth = None
                verify = True
                if legacy.os.name == "nt":
                    from SPFLib.SPFUtilities.requests_negotiate_sspi import HttpNegotiateAuth
                    auth = HttpNegotiateAuth()
                    verify = self.generate_pems()
                # POSIX uses requests' system CA verification. Integrated Windows
                # credentials and SharePoint remain separate platform integrations.
                if self.isSPO_URL(MyURL) is True:
                    self.SPOHandler2("DOWNLOADFILE", MyURL, MyFile)
                    return

                myChunkSize = 64 * 1024
                with requests.get(url=MyURL,
                                  # [Removed from current version] verify=self.generate_pems(),
                                  # [Removed from current version] auth=HttpNegotiateAuth(),
                                  verify=verify,
                                  auth=auth,
                                  stream=True,
                                  timeout=reqtimeout,
                                  proxies=self.IGNORE_LOCAL_PROXIES) as response:
                    self.logger.debug("response.status_code : {0} ...{1}".format(MyURL, response.status_code))
                    response.raise_for_status() #rais if any error
                    tempOut = legacy.StringIO()
                    if returnDirectData is False:
                        with open(MyFile, "wb") as destFile:
                            if runSilent is False:
                                self.Console("Saving to : {0}".format(MyFile))
                            for chunk in response.iter_content(chunk_size=myChunkSize):
                                if chunk:
                                    destFile.write(chunk)
                    else:
                        for chunk in response.iter_content(chunk_size=myChunkSize):
                            if chunk:
                                tempOut.write(chunk.decode())
                if not tmpOrigProxy is None :
                    legacy.os.environ['http_proxy'] = tmpOrigProxy
                    self.logger.debug("{0} - reset : os.environ['http_proxy']: '{1}'".format(calling_func, legacy.os.environ['http_proxy']))
                if returnDirectData is True:
                    return tempOut.getvalue()
            except requests.exceptions.HTTPError as reqErr:
                self.logger.exception("{0} - {1}".format(calling_func, reqErr))
                errMsg = "The following error occurred : Error accessing web resource: {0}  \n(The remote server returned an error: ({1} : {2}))".format(MyURL, reqErr.response.status_code, reqErr.response.reason)
                raise Exception(errMsg)
            except requests.exceptions.RequestException as reqErr:
                self.logger.exception("{0} - {1}".format(calling_func, reqErr))
                errMsg = "The following error occurred : Error accessing web resource: {0}  \n(The remote server returned an error: ({1}))".format(MyURL, reqErr.args[0])
                raise Exception(errMsg)
            except IOError as ioErr:
                errMsg = "The following error occurred : Could not write to file: {0}  ({1}.).".format(MyFile, ioErr.strerror)
                self.logger.exception("{0} - {1}".format(calling_func, ioErr))
                raise Exception(errMsg)
            except Exception as err:
                errMsg = err.args[0]
                self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                raise Exception(errMsg)
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def SPFEmail(self, MyLocal, MyCSVFile, MailToIn, Subject, BodyF, MailCC, MailBCC, MyRole, OnlyIntel, ll_Outlook, EmailUtility="SA") :
        """
       '==============================================================================
        'Emails One or more files to a distribution list
        '
        'Args:
        '====
        'MyLocal  : N if query is being run in batch
        'MyCSVFile: File(s) to Email, separated by commas
        'MailtoIn : To List separated by commas, or file w/ hdr w/ addss, 1 adss/line
        'Subject  : Mail Subj. (opt)
        'BodyF    : Text File w/ contents of Mail Msg. Bounded with <HTML...</HTML> 
        '           if an HTML file (opt)
        'MailCC   :CC List per MailToIn (opt)
        'MailBCC  :BCC List per MailToIn (opt)
        'MyRole   :Opt Security Role to test (opt)
        'll_Outlook: True (Y) to use Outlook, else use SMTP
        'EmailUtility : 'O' or 'S' or 'SA'. Default 'SA' : SMTPAuth
        '===============================================================================
        """
        is_linux = legacy.os.name != "nt"
        if is_linux and self.IsEmptyOrNone(MyRole) is False:
            raise RuntimeError(
                "UNRESOLVED: Linux role verification (verifyrole.exe) is unavailable; "
                "original role restriction must be retained."
            )

        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - MyLocal: '{1}'".format(calling_func, MyLocal))
        self.logger.debug("{0} - MyCSVFile: '{1}'".format(calling_func, MyCSVFile))
        self.logger.debug("{0} - MailToIn: '{1}'".format(calling_func, MailToIn))
        self.logger.debug("{0} - Subject: '{1}'".format(calling_func, Subject))
        self.logger.debug("{0} - BodyF: '{1}'".format(calling_func, BodyF))
        self.logger.debug("{0} - MailCC: '{1}'".format(calling_func, MailCC))
        self.logger.debug("{0} - MailBCC: '{1}'".format(calling_func, MailBCC))
        self.logger.debug("{0} - MyRole: '{1}'".format(calling_func, MyRole))
        self.logger.debug("{0} - OnlyIntel: '{1}'".format(calling_func, OnlyIntel))
        self.logger.debug("{0} - ll_Outlook: '{1}'".format(calling_func, ll_Outlook))
        self.logger.debug("{0} - EmailUtility: '{1}'".format(calling_func, EmailUtility))

        Body = cBody = "Please see attached files..."
        IsHTM = False # "N" #'Assume Text Body
        myExePath = None
        MyCSVFileList = []
        useSMTPAuth = False
        try :
            self.Console("Starting Email Applet v4.4...")

            if self.IsEmptyOrNone(Subject) is True :
                Subject = "Email from SQLPathFinder - 2"
                self.logger.debug("{0} - Subject: '{1}'".format(calling_func, Subject))
            if BodyF == "Y" :
                BodyF = "" #'Backwards compatibility change
                self.logger.debug("{0} - BodyF: '{1}'".format(calling_func, BodyF))

            if self.SHisSHEntry is True :
                ll_Outlook = False #'Never use Outlook on SH
                EmailUtility = 'S' # SMTPauth is not supported on SH
                useSMTPAuth = False
                self.logger.debug("{0} - updated SH ll_Outlook: '{1}'".format(calling_func, ll_Outlook))
                self.logger.debug("{0} - updated SH EmailUtility: '{1}'".format(calling_func, EmailUtility))
                self.logger.debug("{0} - updated SH useSMTPAuth: '{1}'".format(calling_func, useSMTPAuth))

            if EmailUtility == "SA":
                ll_Outlook = False
                useSMTPAuth = True
                self.logger.debug("{0} - updated SA ll_Outlook: '{1}'".format(calling_func, ll_Outlook))
                self.logger.debug("{0} - updated SA useSMTPAuth: '{1}'".format(calling_func, useSMTPAuth))
            elif EmailUtility == "S":
                ll_Outlook = False
                useSMTPAuth = False
                self.logger.debug("{0} - updated S ll_Outlook: '{1}'".format(calling_func, ll_Outlook))
                self.logger.debug("{0} - updated S useSMTPAuth: '{1}'".format(calling_func, useSMTPAuth))

            if is_linux:
                ll_Outlook = False

            if (self.IsEmptyOrNone(MailToIn) is True
                and self.IsEmptyOrNone(MailCC) is True
                and self.IsEmptyOrNone(MailBCC) is True) :
                self.Cons80()
                self.Console("The Email TO Addresses are missing ...\nExiting ...")
                self.Cons80()

            if self.IsEmptyOrNone(BodyF) is False :
                """
                '****************************************
                'Test for validity of Email File Contents
                'If valid, read contents & assign to Body
                '****************************************
                """
                self.Console("Testing for existence of file with Email Contents ...")
                BodyF = self.Substitute_Std_Tokens(BodyF, "1") #'<TS> ...
                if legacy.os.path.exists(BodyF) is True : #'File Exists
                    self.Console(" File exists. Reading Contents ...\n")
                    with open(BodyF, "r", encoding=self.detectFileEncoding(BodyF,readall=True)) as BodyContentReader:
                        Body = BodyContentReader.read()
                    #self.logger.debug("{0} - Body: '{1}'".format(calling_func, Body))
                    """
                    '****************************
                    'Test if Body is HTML or Text
                    '****************************
                    """
                    if Body.upper().find("<HTML") != -1 :
                        IsHTM = True
                        """
                        '========================================
                        'Ensure JMP images in same dir as HTM File
                        '========================================
                        """
                        Body = Body.replace('<img src="gfx/image', '<img src="image')
                        Body = Body.replace(r'<img src=".\gfx\image', '<img src="image')
                else :
                    self.Console("  File does not exist. Assigning a Default Message...\n")
                    Body = cBody

            #self.logger.debug("{0} - Body: '{1}'".format(calling_func, Body))
            self.logger.debug("{0} - IsHTM: '{1}'".format(calling_func, IsHTM))
            if self.IsEmptyOrNone(MyCSVFile) is False :
                MyCSVFile = self.Substitute_Std_Tokens(MyCSVFile, "1") # '<TS> ...
                MyCSVFileList = [csvItem.strip() if ll_Outlook is False else "{0}|{1}".format(legacy.os.path.basename(csvItem.strip()),
                                                                                                         legacy.os.path.abspath(csvItem.strip()))
                                 for csvItem_idx, csvItem in enumerate(MyCSVFile.split(","))]
            else : # 'No Attachments
                if Body == cBody :
                    Body = ""
            self.logger.debug("{0} - len(MyCSVFileList): '{1}'".format(calling_func, len(MyCSVFileList)))
            self.logger.debug("{0} - MyCSVFileList: '{1}'".format(calling_func, MyCSVFileList))
            if self.IsEmptyOrNone(Subject) is False :
                Subject = self.Substitute_Std_Tokens(Subject, "1") # '<TS> ...

            userEmailAddress = ""
            # [Removed from current version] try:
                # [Removed from current version] userEmailAddress = self.gUserPrincipal #self.getEmailForUser(self.gUN, self.gUDomain, MyLocal)
            # [Removed from current version] except pythoncom.com_error  as comErr:
                # [Removed from current version] if self.IsEmptyOrNone(comErr.args[2][2]) is True or "The directory property cannot be found in the cache." == comErr.args[2][2].strip():
                    # [Removed from current version] self.ConsoleWithCons80("Email ID not found for '{0}\\{1}'".format(self.gUDomain, self.gUN))
                # [Removed from current version] else:
            if is_linux:
                from datasyncx.utils.config import get_config_value
                userEmailAddress = (get_config_value("scripthost_user_email") or "").strip()
                if not userEmailAddress:
                    self.logger.warning("{0} - SCRIPTHOST_USER_EMAIL not set; 'self' recipients skipped".format(calling_func))
            else:
                try:
                    userEmailAddress = self.gUserPrincipal #self.getEmailForUser(self.gUN, self.gUDomain, MyLocal)
                except legacy.pythoncom.com_error  as comErr:
                    if self.IsEmptyOrNone(comErr.args[2][2]) is True or "The directory property cannot be found in the cache." == comErr.args[2][2].strip():
                        self.ConsoleWithCons80("Email ID not found for '{0}\\{1}'".format(self.gUDomain, self.gUN))
                    else:
                        raise
                except Exception as err:
                    raise
            # [Removed from current version] except Exception as err:
                # [Removed from current version] raise

            """
            '*****************
            'Assign Email prop.
            '*****************
            """
            MailToF = []
            ctr = 0
            if self.IsEmptyOrNone(MailToIn) is False :
                MailToF, ctr, GetEmailAdss_ret = self.GetEmailAdss(MailToIn.strip(), "TO", userEmailAddress, MyRole, MailToF, OnlyIntel, ctr, MyLocal)
                if GetEmailAdss_ret == "Y" :
                    return
                else :
                    MailToIn = MailToF
            else :
                MailToIn = []

            MailToF = []
            if self.IsEmptyOrNone(MailCC) is False :
                MailToF, ctr, GetEmailAdss_ret = self.GetEmailAdss(MailCC, "CC", userEmailAddress, MyRole, MailToF, OnlyIntel, ctr, MyLocal)
                if GetEmailAdss_ret == "Y" :
                    return
                else :
                    MailCC = MailToF
            else :
                MailCC = MailToF

            MailToF = []
            if self.IsEmptyOrNone(MailBCC) is False :
                MailToF, ctr, GetEmailAdss_ret = self.GetEmailAdss(MailBCC, "BCC", userEmailAddress, MyRole, MailToF, OnlyIntel, ctr, MyLocal)
                if GetEmailAdss_ret == "Y" :
                    return
                else :
                    MailBCC = MailToF
            else :
                MailBCC = MailToF

            if is_linux:
                MailToIn, MailCC, MailBCC = ([m for m in lst if m] for lst in (MailToIn, MailCC, MailBCC))
                if not (MailToIn or MailCC or MailBCC):
                    self.ConsoleWithCons80("No resolvable email addresses ...\nSkipping email ...")
                    self.logger.warning("{0} - no resolvable recipients; email skipped".format(calling_func))
                    return

            if ctr == 0 :
                self.Cons80()
                self.Console("There are no valid email addresses ...\nExiting ...")
                self.Cons80()
            elif ll_Outlook is False : #'OK for SMTP
                smtpObj = None
                try :
                    ctype = 'application/octet-stream'
                    maintype, subtype = ctype.split('/', 1)
                    contentType = 'plain'
                    if IsHTM is True :
                        contentType = 'html'
                    if len(MyCSVFileList) > 0 :
                        emailMessage = legacy.MIMEMultipart()
                        #now attach the body part
                        emailMessage.attach(legacy.MIMEText(Body, contentType, "utf-8"))
                    else : #No attachments
                        emailMessage = legacy.MIMEText(Body, contentType, "utf-8")

                    emailMessage['Subject'] = Subject
                    # [Removed from current version] emailMessage['From'] = userEmailAddress
                    emailMessage['From'] = "atmanalytic@intel.com" if is_linux else userEmailAddress
                    emailMessage['To'] = ",".join(MailToIn)
                    if len(MailCC) > 0 :
                        emailMessage['Cc'] = ",".join(MailCC)

                    if len(MailBCC) > 0 :
                        emailMessage['Bcc'] = ",".join(MailBCC)

                    for csvFileItem in MyCSVFileList :
                        csvFileItemPath, csvFileItemName = legacy.os.path.split(csvFileItem)
                        self.logger.debug("{0} - csvFileItem: '{1}'".format(calling_func, csvFileItem))
                        self.logger.debug("{0} - csvFileItemPath: '{1}'".format(calling_func, csvFileItemPath))
                        self.logger.debug("{0} - csvFileItemName: '{1}'".format(calling_func, csvFileItemName))

                        csvFileItem_exists, csvFileItem_modstr = self.File_Exists_Retry(csvFileItem, NameError, 3, 5, showConsoleMsgs=False) # retry 3 times with a sleep of 5 seconds
                        if csvFileItem_exists is False :
                            errMsg = "Error: Error while opening file : {0}".format(csvFileItem)
                            raise Exception(errMsg)
                        if legacy.os.path.isfile(csvFileItem) is False :
                            errMsg = "Error: Is not a file : {0}".format(csvFileItem)
                            raise Exception(errMsg)
                        attachObj = legacy.MIMEBase(maintype, subtype)
                        with open(csvFileItem, 'rb') as attachementFileToRead : #read in bytes in both Py2 & Py3
                            attachObj.set_payload(attachementFileToRead.read())
                        legacy.encoders.encode_base64(attachObj)
                        attachObj.add_header('Content-Disposition', 'attachment', filename=csvFileItemName) #just the file name exclude the path info
                        emailMessage.attach(attachObj)
                    #END : for csvFileItem in MyCSVFileList :
                    #self.logger.debug("{0} - emailMessage.as_string(): '{1}'".format(calling_func, emailMessage.as_string()))
                    #if EmailUtility == "S" or self.SHisSHEntry is True:
                    #    useSMTPAuth = False
                    #else:
                    #    useSMTPAuth = False
                    self.logger.debug("{0} - useSMTPAuth : '{1}'".format(calling_func, useSMTPAuth))
                    #useSMTPAuth = True if self.SHisSHEntry is False else False # Default use SMTPAuth
                    if is_linux:
                        from datasyncx.utils.send_mail import get_smtp_service_module
                        del emailMessage['Bcc']
                        smtpObj = get_smtp_service_module().get_smtp_service()
                        try:
                            smtpObj.sendmail(emailMessage['From'], MailToIn + MailCC + MailBCC, emailMessage.as_string())
                        finally:
                            smtpObj.quit()
                        self.ConsoleDoneWithTimeStamp()
                        return
                    if legacy.SPFSMTPAuthEmail is None:
                        raise RuntimeError("Legacy ScriptHost SMTP transport is unavailable on this platform")
                    try:
                        SPFSMTPAuthEmail_ = legacy.SPFSMTPAuthEmail().SendEmail(userEmailAddress, MailToIn + MailCC + MailBCC, emailMessage.as_string(), useSMTPAuth=useSMTPAuth)
                    except Exception as err:
                        self.logger.error("{0} - {1}".format(calling_func, err.args[0]))
                        if self.SHisSHEntry is False: #if not on SH...auto try using SMTP, outlook
                            useSMTPAuth = not useSMTPAuth # Negate the value
                            try:
                                #...failed with SMTPAuth....auto try using SMTP
                                self.ConsoleWithCons80(f"{err}\nError using SMTPAuth. Trying with SMTP... {self.DatetimeNow}")
                                SPFSMTPAuthEmail_ = legacy.SPFSMTPAuthEmail().SendEmail(userEmailAddress, MailToIn + MailCC + MailBCC, emailMessage.as_string(), useSMTPAuth=useSMTPAuth)
                            except Exception as err:
                                #...failed with SMTP....auto try using outlook
                                ll_Outlook = True
                                self.ConsoleWithCons80(f"{err}\nError using SMTP. Trying with Outlook... {self.DatetimeNow}")
                                try:
                                    MyCSVFileList = [csvItem.strip() if ll_Outlook is False else "{0}|{1}".format(legacy.os.path.basename(csvItem.strip()),
                                                                                                             legacy.os.path.abspath(csvItem.strip()))
                                                    for csvItem_idx, csvItem in enumerate(MyCSVFile.split(","))]
                                    self.logger.debug("{0} - len(MyCSVFileList): '{1}'".format(calling_func, len(MyCSVFileList)))
                                    self.logger.debug("{0} - MyCSVFileList: '{1}'".format(calling_func, MyCSVFileList))
                                    self.ol_email(IsHTM, MailToIn, MailCC, MailBCC, Subject, Body, MyCSVFileList)
                                except Exception as err:
                                    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                                    raise
                except Exception as err:
                    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                    raise
                finally :
                    smtpObj = None
            elif ll_Outlook is True : # 'OK for Outlook
                self.ol_email(IsHTM, MailToIn, MailCC, MailBCC, Subject, Body, MyCSVFileList)
            self.ConsoleDoneWithTimeStamp()
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def UnzipFile(self, sFile, sTarget, bDelDirs) :
        """
        '===============================
        'Zip files
        '
        'ARGUMENTS:
        '---------
        ' sFile     : Zip file name
        ' sTarget   : Path of Folder to UnZip
        ' bDelDirs  : path of sourcefiles implemented during unzip
        '===============================
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - sFile: '{1}'".format(calling_func, sFile))
        self.logger.debug("{0} - sTarget: '{1}'".format(calling_func, sTarget))
        self.logger.debug("{0} - bDelDirs: '{1}'".format(calling_func, bDelDirs))
        if legacy.os.name != "nt":
            from scripthost_portable.file_operations import unzip_file
            return unzip_file(sFile, sTarget, bDelDirs)
        cmdToExecute = "unzip.exe"
        sDirs = "-j"
        try :
            if self.SHisSHEntry is True :
                cmdToExecute = "unzip6.exe"

            elif self.gMyLocal == "N" :
                if legacy.os.path.exists(cmdToExecute) is False :
                    MySrcExe = legacy.os.path.join(self.gSPFVaryLib, cmdToExecute)
                    MyExe = ".\\unzip.exe"
                    cmdToExecute = self.DoCopySPFLib(MySrcExe, MyExe)
            else :
                cmdToExecute = legacy.os.path.join(self.gMyLocal, cmdToExecute)

            self.logger.debug("{0} - cmdToExecute: '{1}'".format(calling_func, cmdToExecute))
            if bDelDirs is True :
                sDirs = ""
            cmdArgs = ["-o", sDirs,
                       '"{0}"'.format(sFile),
                       '-d',
                       '"{0}"'.format(sTarget)]
            runStatus, runCode = self.Run(cmdToExecute, cmdArgs)
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def GetFilePattern(self, FP) :
        """
        '===============================================================================
        'Return full path to a file based on a Path Pattern
        '
        ' Args:
        ' ====
        '   1) FP  : File Path.May contain folder/file tokens such as
        '            <folder-DateLastModified>, <file-DateLastModified>
        '            One file token can be usedand if so, it must be the
        '            last entry in the path.
        '
        ' RETURNS:
        ' -------
        '  Actual file or empty if pattern was not found
        '===============================================================================
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - FP: '{1}'".format(calling_func, FP))

        Token1 ="<folder-datelastmodified>"
        Token2 ="<file-datelastmodified>"
        Token3 = "                      "
        MaxF = ""
        MaxFDate = ""
        GetFilePattern_ret = ""

        try :
            self.Console("Get File Path Based on a Pattern, v1.0 ... {0}".format(self.DatetimeNow))
            """
            '*****************
            'Perform Arg Check
            '*****************
            """
            GetFilePattern_ret = FP

            if self.IsEmptyOrNone(FP) is True :
                errMsg = "Missing File Path argument ..."
                raise Exception(errMsg)

            self.Console("Looking for: {0}".format(FP))
            # [Removed from current version] FPList = FP.split("\\")
            FPList = FP.split("\\" if legacy.os.name == "nt" else "/")
            TmpF = ""
            while len(FPList) > 0 :
                FPListItem = FPList.pop(0)
                self.logger.debug("{0} - FPListItem: '{1}'".format(calling_func, FPListItem))
                MaxF = ""
                MaxFDate = ""
                if self.IsEmptyOrNone(FPListItem) is True : # this a \\
                    # [Removed from current version] TmpF = "{0}\\".format(TmpF)
                    TmpF = "{0}{1}".format(TmpF, legacy.os.sep)
                #backward compatible Token1Token2 format and new format Token1\Token2
                elif FPListItem.lower().find(Token1) != -1 : #get the folder with max modified date
                    if FPListItem.lower().find(Token2) != -1 :
                        FPList.insert(0, Token2)
                    FPListItem = FPListItem.lower().replace(Token1, '').replace(Token2, '')
                    self.logger.debug("{0} - FPListItem-Token1-Token2: '{1}'".format(calling_func, FPListItem))
                    TmpF = legacy.os.path.join(TmpF, FPListItem)
                    self.logger.debug("{0} - get folder with Max mod timestamp from TmpF: '{1}'".format(calling_func, TmpF))
                    for FName in legacy.os.listdir(TmpF) :
                        self.logger.debug("{0} - FName: '{1}'".format(calling_func, FName))

                        FNameWithPath = legacy.os.path.join(TmpF, FName)
                        self.logger.debug("{0} - FNameWithPath: '{1}'".format(calling_func, FNameWithPath))
                        if legacy.os.path.isdir(FNameWithPath) is True :
                            MyModDate = "{0}{1}".format(legacy.datetime.fromtimestamp(legacy.os.path.getmtime(FNameWithPath)).strftime(self.G_DT_FORMAT_YYYYMMDDhhnnss),
                                                        FName)
                            self.logger.debug("{0} - MyModDate: '{1}'".format(calling_func, MyModDate))
                            if MyModDate > MaxFDate :
                                MaxFDate = MyModDate
                                MaxF = FName

                    self.logger.debug("{0} - MaxF: '{1}'".format(calling_func, MaxF))
                    if self.IsEmptyOrNone(MaxF) is True :
                        errMsg = "Could not find matching file pattern..."
                        raise Exception(errMsg)

                    TmpF = legacy.os.path.join(TmpF, MaxF.strip("\\"))
                    self.logger.debug("{0} - TmpF updated with folder MaxF: '{1}'".format(calling_func, TmpF))
                elif FPListItem.lower().find(Token2) != -1 : #get the file with max modified date
                    FPListItem = FPListItem.lower().replace(Token2, '')
                    TmpF = legacy.os.path.join(TmpF, FPListItem)
                    self.logger.debug("{0} - get file with Max mod timestamp from TmpF: '{1}'".format(calling_func, TmpF))
                    for FName in legacy.os.listdir(TmpF) :
                        self.logger.debug("{0} - FName: '{1}'".format(calling_func, FName))

                        FNameWithPath = legacy.os.path.join(TmpF, FName)
                        self.logger.debug("{0} - FNameWithPath: '{1}'".format(calling_func, FNameWithPath))
                        if legacy.os.path.isfile(FNameWithPath) is True :
                            MyModDate = "{0}{1}".format(legacy.datetime.fromtimestamp(legacy.os.path.getmtime(FNameWithPath)).strftime(self.G_DT_FORMAT_YYYYMMDDhhnnss),
                                                        FName)
                            self.logger.debug("{0} - MyModDate: '{1}'".format(calling_func, MyModDate))
                            if MyModDate > MaxFDate :
                                MaxFDate = MyModDate
                                MaxF = FName

                    self.logger.debug("{0} - MaxF: '{1}'".format(calling_func, MaxF))
                    if self.IsEmptyOrNone(MaxF) is True :
                        errMsg = "Could not find matching file pattern..."
                        raise Exception(errMsg)
                    TmpF = legacy.os.path.join(TmpF, MaxF.strip("\\"))
                    self.logger.debug("{0} - TmpF updated with file MaxF: '{1}'".format(calling_func, TmpF))
                else :
                    if FPListItem[-1] == ":" :
                        FPListItem = "{0}\\".format(FPListItem) # this is drive need concat with path separator
                    if TmpF == r"\\": #fix for Py311
                        TmpF = f"{TmpF}{FPListItem}"
                    else:
                        TmpF = legacy.os.path.join(TmpF, FPListItem)
            FP = TmpF
            GetFilePattern_ret = FP
            return GetFilePattern_ret
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            self.Cons80()
            self.Console(err.args[0])
            self.Cons80()
            return GetFilePattern_ret
        finally :
            self.Console("  Done at {0}\n".format(self.DatetimeNow))

    def SPFCopy(self, MySrc, MyDest, continueOnError=False, emitConsoleMessages=True, usePyCopy=False) :
        """
        '======================================================
        'Copies a file and all sub-directories to a destination
        '
        'Args:
        '====
        'MySrc   : Path of source directory & files to copy
        'MyDest  : Path and name of dest. directory & files
        'continueOnError : default = False -- raise error. If True display error and continue
        ' Output : 
        '=========
        'MyReturn: Returns 0 if successful or 1 or more for errors
        '
        'Input Dropped in Py Migration
        '=======================
        'MyReturn: Returns 0 if successful or 1 or more for errors
        '=======================================================
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - MySrc: '{1}'".format(calling_func, MySrc))
        self.logger.debug("{0} - MyDest: '{1}'".format(calling_func, MyDest))
        self.logger.debug("{0} - continueOnError: '{1}'".format(calling_func, continueOnError))
        self.logger.debug("{0} - usePyCopy: '{1}'".format(calling_func, usePyCopy))

        MyReturn = 1 #'~ successful (Failed)

        try :
            if emitConsoleMessages is True:
                self.Console("Starting Copy Applet, v3.1 ... {0}".format(self.DatetimeNow))
            if self.IsEmptyOrNone(MySrc) is True :
                self.Console("  No source path specified. Exiting ...")
                return MyReturn

            if self.IsEmptyOrNone(MyDest) is True :
                self.Console("  No destination path specified. Exiting ...")
                return MyReturn

            if emitConsoleMessages is True:
                self.Console("  Copying {0} to {1}".format(MySrc, MyDest))
            # [Removed from current version] if usePyCopy is True:
            if legacy.os.name != "nt":
                from scripthost_portable.file_operations import copy_files
                copy_files(MySrc, MyDest)
                runExitCode = 0
            elif usePyCopy is True:
                try:
                    #supports only files
                    legacy.shutil.copy2(MySrc, MyDest)
                    runExitCode = 0
                except Exception as err:
                    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                    errMsg = "  Error occurred during copy"
                    raise Exception(errMsg)
            else:
                cmdToExecute = "%COMSPEC%"
                cmdArgs = ["/c", "COPY", "/Y","/V", '"{0}"'.format(MySrc), '"{0}"'.format(MyDest)]
                try:
                    runStatus, runExitCode = self.Run(cmdToExecute, cmdArgs, usePopen=True)
                except legacy.SPFCMDRunExitWithErrorCodeException as SPFCMDRunErr:
                    self.logger.exception("{0} - {1}".format(calling_func, SPFCMDRunErr.args[0]))
                    errMsg = "  Error occurred during copy"
                    raise Exception(errMsg)
            if runExitCode == 0 :
                MyReturn = 0
                if emitConsoleMessages is True:
                    self.ConsoleDoneWithoutTimeStamp()
            return MyReturn
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def LoadExcel2(self, MyMode, DoContinue, MyCSVFile, ExcelResultFile, MyXLSFile="", MyWorkSheet="", MyVBProc="") :
        """
        '==========================================================
        'Either Loads a CSV File to Excel or Imports one or more CSV 
        'Files to Excel worksheets & opt. runs a Macro
        '
        'Args:
        '====
        'MyMode         : IMPORT or LOAD
        'DoContinue     : Y=Continue if Job Fails
        'MyCSVFile      : CSV File to Load or comma delimited list of files to Import
        'ExcelResultFile: Excel File to Save [Default=SQLPathFinder.xlsx]
        'MyXLSFile      : Input XL Template File (IMPORT MODE only)
        'MyWorkSheet    : Comma delimited XL Worksheet(s) to receive files (IMPORT MODE only)
        'MyVBProc       : Opt. VBA Procedure to Run (IMPORT MODE only)
        '
        'GLOBALS:
        '-------
        'gSPFExe   : SPF Executable Folder (SH, local,Non-local)
        'gMyABort  : Y=Abort overall job
        '==========================================================
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - MyMode: '{1}'".format(calling_func, MyMode))
        self.logger.debug("{0} - DoContinue: '{1}'".format(calling_func, DoContinue))
        self.logger.debug("{0} - MyCSVFile: '{1}'".format(calling_func, MyCSVFile))
        self.logger.debug("{0} - ExcelResultFile: '{1}'".format(calling_func, ExcelResultFile))
        self.logger.debug("{0} - MyXLSFile: '{1}'".format(calling_func, MyXLSFile))
        self.logger.debug("{0} - MyWorkSheet: '{1}'".format(calling_func, MyWorkSheet))
        self.logger.debug("{0} - MyVBProc: '{1}'".format(calling_func, MyVBProc))

        MyExe0 = "spfExcelUtility.exe"
        try :
            if legacy.os.name != "nt":
                from openpyxl import Workbook, load_workbook
                if MyVBProc:
                    raise RuntimeError("Excel VBA execution requires the original Windows Excel integration")
                output = legacy.Path(ExcelResultFile or "SQLPathFinder.xlsx")
                if output.suffix.lower() != ".xlsx" or (MyXLSFile and legacy.Path(MyXLSFile).suffix.lower() != ".xlsx"):
                    raise RuntimeError("Portable Excel LOAD/IMPORT supports .xlsx workbooks only")
                files = next(legacy.csv.reader(legacy.StringIO(MyCSVFile), skipinitialspace=True))
                sheets = next(legacy.csv.reader(legacy.StringIO(MyWorkSheet), skipinitialspace=True)) if MyWorkSheet else []
                if MyMode.upper() == "LOAD":
                    files, sheets = [MyCSVFile], ["Sheet1"]
                elif len(files) != len(sheets):
                    raise ValueError("Excel IMPORT requires one worksheet name per CSV file")
                frames = [legacy.pd.read_csv(name, sep=self.GetFileDLM(name), dtype=str,
                                     keep_default_na=False, encoding=self.detectFileEncoding(name, readall=True))
                          for name in files]
                workbook = load_workbook(MyXLSFile) if MyXLSFile else Workbook()
                if not MyXLSFile:
                    workbook.remove(workbook.active)
                for frame, name in zip(frames, sheets):
                    sheet = workbook[name] if name in workbook.sheetnames else workbook.create_sheet(name)
                    sheet.delete_rows(1, sheet.max_row)
                    sheet.append(list(frame.columns))
                    for row in frame.itertuples(index=False, name=None):
                        sheet.append(list(row))
                workbook.save(output)
                workbook.close()
                return
            MyExe0 = legacy.os.path.join(self.gSPFExe, MyExe0)
            self.logger.debug("{0} - MyExe0: '{1}'".format(calling_func, MyExe0))

            if MyMode.upper() == "LOAD" :
                self.Record_SPF("LoadExcel2_LOAD",self.gMyLocal, self.gMyEXEDir,MyInstance=self.gSPFInstance)
                cmdArgs = ['/mode="LOAD"',
                           '/csv="{0}"'.format(MyCSVFile if self.IsEmptyOrNone(MyCSVFile) is False else ""),
                           '/out="{0}"'.format(ExcelResultFile if self.IsEmptyOrNone(ExcelResultFile) is False else "")]
            else :
                self.Record_SPF("LoadExcel2_IMPORT_{0}".format(MyVBProc), self.gMyLocal, self.gMyEXEDir, MyInstance=self.gSPFInstance)
                cmdArgs = ['/mode="IMPORT"',
                           '/csv="{0}"'.format(MyCSVFile),
                           '/out="{0}"'.format(ExcelResultFile),
                           '/sheets="{0}"'.format(MyWorkSheet),
                           '/template="{0}"'.format(MyXLSFile),
                           '/macro="{0}"'.format(MyVBProc)]
            runStatus, runExitCode = self.Run(MyExe0, cmdArgs, usePopen=True)
            if runExitCode > 0 :
                raise Exception("")
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            if DoContinue is False :
                self.gMyAbort = True
                raise
