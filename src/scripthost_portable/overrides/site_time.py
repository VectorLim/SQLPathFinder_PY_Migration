"""Portable overrides for the archived GetSiteTimeTask class."""

import SPFLib.SPFSQL3 as legacy


class GetSiteTimeTask(legacy.GetSiteTimeTask):
    """Extend the original implementation with portable methods."""

    def executeTaskCommand(self):
        """
        ' overridden from base -- implementation of Do_Get_Time and Do_Get_Time_File
        ' Note: this task calls specific nqXXXTask (E.g., nqOracleTask, etc) Do_Get_Time() method implementation -- see below mapping
        ' Note: merged GetSiteTimeFileTask into this
        '==================================
        'Get TimeStamp at a Site or for GMT
        '
        'ARGS:
        '----
        'MyArg  : Site or GMT for GMT time
        'MyLocal: Local dir for interactive run;N for batch
        '
        'GLOBALS:
        '-------
        'gMyAbort : Global Job Abort Variable
        '==================================
        """

        handlerMapper = {'MICROSOFT' : ['nqOracleTask', 'Do_Get_Time']
                         ,'UBER' : ['nqUberTask', 'Do_Get_Time'] } #'===== v30.55 ==========

        #TODO : need to support for other backend DB -- only Oracle supported for now
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        try :
            self.Write_Prompt()
            MyArg = self.MyUtilities[1]
            self.logger.debug("{0} - MyArg: {1}".format(calling_func, MyArg))
            self.logger.debug("{0} - OLEDBopt: {1}".format(calling_func, self.OLEDBopt))
            #'===== v30.55 ==========
            if legacy.re.search("(XEUS|_DIS|_PROD_ARIES|_PROD_MARS|_PROD_OASYS|_PROD_YAS)", MyArg, legacy.re.IGNORECASE) :
                self.OLEDBopt = "UBER"
                self.logger.debug("{0} - updated OLEDBopt: {1}".format(calling_func, self.OLEDBopt))

            TaskHandlerName, HandlerFuncName = handlerMapper[self.OLEDBopt]
            self.logger.debug("{0} - TaskHandlerName: {1}".format(calling_func, TaskHandlerName))
            self.logger.debug("{0} - HandlerFuncName: {1}".format(calling_func, HandlerFuncName))

            TaskHandlerClsObject = getattr(legacy.sys.modules[legacy.__name__], TaskHandlerName)
            self.logger.debug("{0} - TaskHandlerClsObject: {1}".format(calling_func, TaskHandlerClsObject))
            myTask = TaskHandlerClsObject()
            self.logger.debug("{0} - myTask: {1}".format(calling_func, myTask))
            myFunc = getattr(myTask, HandlerFuncName)
            self.logger.debug("{0} - myFunc: {1}".format(calling_func, myFunc))
            self.gMySPFJobTime = myFunc(MyArg)

            if self.MyUtilities[0] == "{GET-SITE-TIME-FILE}" : # (Do_Get_Time_File)
                # [Removed from current version] self.SetIni(os.path.join(".\\", "{0}.spf$data".format(self.gSPFInstance)), "SITE-TIME", "TIME", self.gMySPFJobTime)
                self.SetIni(legacy.os.path.join(".", "{0}.spf$data".format(self.gSPFInstance)), "SITE-TIME", "TIME", self.gMySPFJobTime)

        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
