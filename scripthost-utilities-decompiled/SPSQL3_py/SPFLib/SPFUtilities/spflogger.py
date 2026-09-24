"""
License : Copyright (c) Intel Corporation 2017
Product: Intel.ATTD.Auto.SQLPathFinder
Module : SQLPathFinder Python Extract Engine 
Author : vishwas.Nataraj@intel.com;SQLPathFinder_Support@intel.com
File Version : 2.0.0.3
History: 
1.0.0.0 : Nataraj : Initial Version
2.0.0.0 : vanatara : Updated to support both Python 2.7.15 & Python 3.6
2.0.0.1 : vantara  : Updated to support initializing to custom loggerName
2.0.0.2 : vantara  : Updated to support delayed log file creation - on first log write, thus avoiding creation of empty .log files when run in non-debug mode
2.0.0.3 : vantara  : set handler encoding to 'utf-8' and added handleError() method to over-ride(monkey-patch) logging.handler.handleError method to handle error within logger in a custom fashion
"""
from SPFLib import * #isPYTHON2 #defined in SPFLib\__init__.py. IF True then 'Pyhton 2' IF False 'Python 3'
#import sys, os, 
import logging, logging.config
#import json
from logging.handlers import RotatingFileHandler
#import locale

if isPYTHON2:
    import sh
else:
    from . import sh

class SPFLogger(object):
    """This class provides a static method to obtain an instance of logger object for SPF application"""
    #__slots__ = ['a']
    __logger = None
    __OSDefaultEncoding = None
    #@property
    #def SPFInstallPath(self):
    #    return os.getenv("USERPROFILE") + "\\My Programs\\SQLPathFinder3"

    SPFInstallPath = os.getenv("USERPROFILE") + "\\My Programs\\SQLPathFinder3"

    #@staticmethod
    def __init__(self):
        #print("inside Init")
        super(SPFLogger, self).__init__()
        if isPYTHON2:
            SPFLogger.__OSDefaultEncoding = sys.getfilesystemencoding()
        else:
            SPFLogger.__OSDefaultEncoding = sys.stdout.encoding #locale.getdefaultlocale()[1]
    #END : def __init__(self):
     
    @staticmethod
    def handleError(_logRec):
        """
        (monky-pathced into logging.handler) method to handle any error during logging
        E.g., codec issues cannot be handled in a right way & causes emitting to the std_err(console)
        """
        errMsg = "Error occurred while logging message.;Exception Type:{0};Exception Value {1};Logged from file :{2},line {3};...continue" #\nData : {4} -- ignore the data as it logs in a single line, which may be very lenghty & mess up open/reading of log files
        _excepType, _excepVal = sys.exc_info()[:2] #get the exception-type & exception-value
        SPFLogger .__logger.warning(errMsg.format(_excepType, _excepVal, _logRec.filename, _logRec.lineno))
        #, _logRec.msg.encode("cp1252", errors='replace')
    #END :def handleError(_logRec): 
        
    @staticmethod
    def __getLogLevel() :
        """
        helper method to get/set logginglevel based on command line args
        """
        #check if /SPFLOGLEVEL is there in command line args
        mySPFLogLevel = logging.ERROR
        SPFLogLevelDict = {"INFO" : logging.INFO, #20
                           "DEBUG" : logging.DEBUG, #10
                           "ERROR" : logging.ERROR #40
                           }
        if len(sys.argv) > 1 :
            tmp = [argvItem.strip().split("=")[-1].strip(' "') # SELECT
                        for argvItem in sys.argv # FROM
                        if "/SPFLOGLEVEL" in argvItem.upper()] # WHERE
            #print("tmp = {0}".format(tmp))
            if len(tmp) > 0 :
                tmp2 = tmp[0].upper().strip()
                #print("tmp2 = {0}".format(tmp2))
                mySPFLogLevel = SPFLogLevelDict.get(tmp2, logging.ERROR)
            #print("mySPFLogLevel = {0}".format(mySPFLogLevel))
        return mySPFLogLevel
             
    @staticmethod
    def __initializeLogger(loggerName):
        #logging.raiseExceptions = False # this will cause ignoring any error within the logger. 
        executionEnv = None #execution environment like SH, non-SH, etc

        if SPFLogger.__logger is None :
            tempSH = sh.ScriptHost()
            SPFLogger.__logger = logging.getLogger(loggerName)
            SPFLogger.__logger.setLevel(SPFLogger.__getLogLevel())
            if tempSH.SHisSHEntry is True:
                executionEnv = "SH Environment"
                if loggerName != "SPFLib":
                    logFile = "{0}.log".format(loggerName)
                    tmpPath,tmpfilename = os.path.split(tempSH.SHSPFLog)
                    SPFLogFile = os.path.join(tmpPath, logFile)
                else :
                    SPFLogFile = tempSH.SHSPFLog
            else:
                executionEnv = "non SH Environment"
                if loggerName != "SPFLib":
                    SPFLogFile = "{0}.log".format(loggerName)
                else:
                    SPFLogFile = "{0}.log".format(os.getpid())
                if len(sys.argv) > 1 :
                    mySPFInstanceArg = [argvItem.strip().split("=")[-1].strip(' "') # SELECT
                                     for argvItem in sys.argv # FROM
                                     if "/SPFINSTANCE" in argvItem.upper()] # WHERE

                    if len(mySPFInstanceArg) > 0 :
                        mySPFInstanceID = mySPFInstanceArg[0]
                        if loggerName != "SPFLib":
                            SPFLogFile = "{0}_{1}.log".format(loggerName, mySPFInstanceID)
                        else:
                            SPFLogFile = "{0}.log".format(mySPFInstanceID)
            #END: if tempSH.SHisSHEntry is True

            #create the logger Handler
            if isPYTHON2: 
                LoggerHandler = RotatingFileHandler(SPFLogFile, maxBytes=52428800, backupCount=1000, delay=True)
            else:
                LoggerHandler = RotatingFileHandler(SPFLogFile, maxBytes=52428800, backupCount=1000, delay=True, encoding='utf-8')
                #LoggerHandler = RotatingFileHandler(SPFLogFile, maxBytes=52428800, backupCount=1000, encoding=SPFLogger.__OSDefaultEncoding)

            #create the logger formatter
            formatter = logging.Formatter("%(asctime)s - %(name)s - %(process)d.%(thread)d - %(levelname)s - %(module)s.%(funcName)s - %(message)s")
            LoggerHandler.setFormatter(formatter)

            LoggerHandler.handleError = SPFLogger.handleError #monkey-patch handler.handleError method with custom method 
            SPFLogger.__logger.addHandler(LoggerHandler)
            SPFLogger.__logger.info("SPFLogger Initialized in {0}".format(executionEnv))
            SPFLogger.__logger.info("Python Info :\nVersion: {0}\nExecutable : {1}\nPath: {2}".format(sys.version, sys.executable, sys.path))
    #END : def __initializeLogger():

    @staticmethod
    def GetLogger(loggerName="SPFLib"):
        if loggerName is None:
            loggerName = "SPFLib"
        SPFLogger.__initializeLogger(loggerName)
        return SPFLogger.__logger

if __name__ == "__main__":
    print("standalone execution")
    SPFLogger1 = SPFLogger()
    logger = SPFLogger.GetLogger()
    logger.info("testing")
    logger2 = SPFLogger.GetLogger()
    logger2.info("testing2")

