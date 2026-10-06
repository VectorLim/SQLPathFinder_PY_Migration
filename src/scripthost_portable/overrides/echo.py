"""Portable overrides for the archived EchoTask class."""

import SPFLib.SPFSQL3 as legacy


class EchoTask(legacy.EchoTask):
    """Extend the original implementation with portable methods."""

    def executeTaskCommand(self):
        """
        ' Overridden base method. Implementation of write utility @Echo  -- Write_Prompt() [VA] in py just call console()
        '=================================================
        sample input line: /UTILITIES=@Echo hello
        """
        #locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        NoArgs = 1 # number of Args expected to process this Task
        MyUtilityName = "@Echo"
        b2 = "{0}_doscmd.bat".format(self.gSPFInstance)
        textToEcho = None # MyArgList(0)

        try :

            textToEcho = self.MyUtilitiesValue
            if legacy.os.name != "nt":
                if not textToEcho.upper().startswith("@ECHO ") or any(char in textToEcho for char in "&|<>"):
                    raise RuntimeError("Windows shell commands/redirection are unavailable in portable Echo")
                self.Console(textToEcho[6:])
                return
            self.logger.debug("{0} - textToEcho: '{1}'".format(calling_func, textToEcho))
            if textToEcho.upper().startswith("@ECHO ") is False:
                self.Write_Prompt()
            cmdToExecute = "%COMSPEC%"

            if self.MyUtilitiesValue.find("&&") > -1 :
                for item in self.MyUtilitiesValue.split("&&") :
                    #item = self.handle_DOSFC(item)
                    cmdArgs = ["/c"] + item.split(" ")
                    if item.lower().startswith("fc ") is True :
                        runStatus, runExitCode = self.Run(cmdToExecute, cmdArgs, usePopen=False)
                    else :
                        runStatus, runExitCode = self.Run(cmdToExecute, cmdArgs, usePopen=True)
            else :

                cmdArgs = ["/c"] + [self.MyUtilitiesValue]
                if self.MyUtilitiesValue.lower().startswith("fc ") is True :
                    runStatus, runExitCode = self.Run(cmdToExecute, cmdArgs, usePopen=False)
                else :
                    runStatus, runExitCode = self.Run(cmdToExecute, cmdArgs, usePopen=True)
        except Exception as err :
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            try :
                runCode, runExitCode, runStatus = err
                self.logger.debug("{0} - err block runExitCode: '{1}'".format(calling_func, runExitCode))
                if runExitCode > 0 :
                    self.gMyAbort = True
                    raise
            except Exception as err2 :
                self.logger.exception("{0} - err2 {1}".format(calling_func, err2))
                raise Exception(err)
