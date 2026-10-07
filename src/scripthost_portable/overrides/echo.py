"""Portable overrides for the archived EchoTask class."""
import SPFLib.SPFSQL3 as legacy

class EchoTask(legacy.EchoTask):

    def executeTaskCommand(self):
        if legacy.os.name == 'nt':
            return super().executeTaskCommand()
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        try:
            textToEcho = self.MyUtilitiesValue
            if not textToEcho.upper().startswith('@ECHO ') or any((char in textToEcho for char in '&|<>')):
                raise RuntimeError('Windows shell commands/redirection are unavailable in portable Echo')
            self.Console(textToEcho[6:])
            return
        except Exception as err:
            self.logger.exception('{0} - {1}'.format(calling_func, err.args[0]))
            try:
                runCode, runExitCode, runStatus = err
                self.logger.debug("{0} - err block runExitCode: '{1}'".format(calling_func, runExitCode))
                if runExitCode > 0:
                    self.gMyAbort = True
                    raise
            except Exception as err2:
                self.logger.exception('{0} - err2 {1}'.format(calling_func, err2))
                raise Exception(err)
