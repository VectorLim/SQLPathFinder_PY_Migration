r"""
License : Copyright (c) Intel Corporation 2023
Product: Intel.ATTD.Auto.SQLPathFinder
Module : SQLPathFinder Python Extract Engine 
Author : vishwas.Nataraj@intel.com;SQLPathFinder_Support@intel.com
File Version : 2.0.2.9
History: 
1.0.0.0 : Nataraj : Initial Version
1.0.0.1 : Nataraj : Added giReportsVersion
2.0.0.0 : vanatara: Updated to support both Python 2.7.15 & Python 3.6
2.0.0.1 : jclarke : Changed how reference JSON Object keys with spaces. E.g., changed rowdata.qty to rowdata['qty']
2.0.0.2 : vanatara : Updated gSPFSQLFileData() for file reading encoding handling
                     Updated gOSDefaultEncoding to set encoding = 'UTF-8' for Python 3.x
2.0.0.3 : vanatara : Updated __G_SQLiteVer_64 = "sqlite64_v2.exe"
2.0.0.4 : jmclarke : Reverted __G_SQLiteVer_64 to "sqlite64.exe" until SH is updated
2.0.0.5 : jmclarke : Updated __G_SQLiteVer_64 to "sqlite64_v2.exe"
2.0.0.6 : vanatara : gSPFSQLFileData() to handle encoding issues while reading file, added detectFileEncoding(), detectCharacterEncoding() methods(moved from utils.py) (322019_Mueller-Matt)
2.0.0.7 : vanatara : detectFileEncoding() -- updated to handle large file full scan using 'chardet.universaldetector'.
2.0.0.8 : jmclarke : Added retry logic when testing that a file is locked in procedure: SPFRoboCopy in utils.py
2.0.0.9 : jmclarke : Handled issue with MAX and MIN and converting Y values to a FLOAT for Interactive Charts (utils.py->ijs_gen_charts_sql)
2.0.1.0 : vanatara : parseCMDArgsToken fix to handle occurance of '=' in parameter values
2.0.1.1 : vanatara : Added initializeSPFBin() - to initialize the 'SPFBin' folder
2.0.1.2 : vanatara : Added GetSPFFunctionIni() - to retrive the setting name from 'schema\spf_functions.ini' file
2.0.1.3 : jmclarke : Ensured Function GetSPFFunctionIni() works on ScriptHost
2.0.1.3a : vanatara : minor updated to changes made in 2.0.1.3
2.0.1.4 : vanatara : added 'gSPFColateralsServers' & 'gSPFReportsServers' properties. Updated gUN & gUDomain to use win32 module
2.0.1.4a : vanatara : added 'gSPFCollateralServerEnv' to get 'COLLATERALSENV' commandline params
2.0.1.5 : vanatara : added 'gDBErrorCodesForRetry' DB retry error codes.
2.0.1.6 : vanatara : added 'gDBGlobCon' global DB connection cache
2.0.1.7 : vanatara : added 'gIsSvc', 'gSvcSessionUser', 'gSQLPFSvcCmdArgsList', 'gSPFLogLevel', 'gUseSQLPFSVC', 'gSQLPFSVCENV'. 
2.0.1.8 : vanatara : update 'gOSDefaultEncoding' to use 'utf-8-sig' as default. update 'detectCharacterEncoding' to use default encoding from 'gOSDefaultEncoding' for ['ASCII', 'CP1252', 'GB2312'] encodings
2.0.1.8a : vanatara : update 'detectFileEncoding' handle empty file case and 'detectCharacterEncoding' use 'utf-8'
2.0.1.8b : vanatara : set 'gOSDefaultEncoding' to 'utf-8'. 'utf-8-sig' has issues with DOS utility
2.0.1.8c : vanatara : set 'gOSDefaultEncoding' to 'utf-8-sig'. fixed 'DOS' utility issues. 
                      Fixed smart-append issue of junk data build up due to encoding issues
                      Added 'detectFileEncoding_cnm' method to determine encoding using 'charset_normalizer' module 
                      Added 'detectFinalEncodingForDictOfFiles' method to determine final encoding to be used when multiple files are used in same utility (smart-append, CSV-SQLite)
                      Added 'gENCODING_FULLFILE_SCAN' property for commandline flag to control full-file scan during encoding determination in 'detectFileEncoding()'
2.0.1.8d : vanatara : Add strip_connection()
2.0.1.9  : vanatara : Add 'gENCODING_UTFBOM' property for commandline flag to control using 'UTF-8' or 'UTF-8-BOM' in gOSDefaultEncoding()
2.0.2.0  : vanatara : Add 'gSQLPF_LogSvc_ENV', 'gSQLPF_LogSvc_URL' for Logger service use.
2.0.2.1  : vanatara : Add 'gUse_UBER_MAO' for MAO UBER support in HPC.
2.0.2.2  : vanatara : Moved 'M_SQLToken'from spfsql3.py to globals constants
2.0.2.3  : vanatara : fix for logging Svc to use the prodNLB URL and drop using the 'goto' URL
2.0.2.4  : vanatara : Update gDBErrorCodesForRetry to support 'P' PostgreSQL error codes for retry(place holder only, actual errors codes TBD) 
2.0.2.5  : vanatara : Add loggerFileName property to get the logger-handler log file name. 
2.0.2.6  : vanatara : Add GetExeAppPath() to construct location of helper Exe app based on execution environment
2.0.2.7  : vanatara : Updated gDBErrorCodesForRetry for Oracle Error codes '03135','12631'
2.0.2.8  : vanatara : Updated to support Py3.13
2.0.2.9  : vanatara : Added IGNORE_LOCAL_PROXIES to be used in requests calls to ignore local proxy settings that are causing issues in some environments
"""
from SPFLib import * #isPYTHON2 #defined in SPFLib\__init__.py. IF True then 'Pyhton 2' IF False 'Python 3'

if isPYTHON2:
    from SPFUtilities.spflogger import SPFLogger
    from SPFUtilities.sh import ScriptHost
else:
    #sys._enablelegacywindowsfsencoding()    
    from .SPFUtilities.spflogger import SPFLogger
    from .SPFUtilities.sh import ScriptHost

class SPFGlobals(ScriptHost) :
    """
    This class provides a container for all global variables/constants for SPFLib. 
    This class is inherited by utilities class
    """    
    #static locals for this class internal use only
    __logger = None
    __isInitialized = False

    #region CONSTANTS
    TMP_F_NAME = "$spf$1$"
    UN_PW_DELIM = "@@@@@" #delimeter used for storing multiple UN/PW
    SQLFILE_DELIM = "<---- New Query ---->"
    M_SQLToken = "<<<spf-$item$-list>>>"  #Token to substitute in SQL Query
    IGNORE_LOCAL_PROXIES: dict = {
    "http": "",
    "https": "",
    } #V2.0.2.9 -- to ignore local proxy settings in requests calls that are causing issues in some environments
    #Hidden constants
    __CMD_ARGS_TKN_NM_VAL_SEP = "="
    __G_VARNO_MAX = 19 # maximum allowed global variables CL_xxx from command line
    __G_SQLiteVer_32 = "sqlite3_v2.exe" # 32bit sqlite3
    __G_SQLiteVer_64 = "sqlite64_v2.exe" # 64bit sqlite3
    __G_SH_FOLDER_SUFFIX = "Applications\\SQLPathFinder\\Software\\" #SH SPF Cfg dir
    __G_SH_R_PATH_SUFFIX = r"Applications\SQLPathFinder\R\R_Version.ini" #SH R Path
    __G_ANYLOOP_LOOKUP = ['N','YS', 'YR','ES', 'ER']
    __G_SH_PY_PATH = r"Applications\SQLPathFinder\Python" #'SH Py Path

    #endregion CONSTANTS

    #region static properties Start : property values will be same across any instance of this class/type
    __g_LoggerFileName = None
    @property
    def gLoggerFileName(self)->Path:
        """
        get : LoggerFileName
        """
        if SPFGlobals.__g_LoggerFileName is None:
            SPFGlobals.__g_LoggerFileName = Path(self.__logger.handlers[0].baseFilename)
        self.__logger.debug(f"get : gLoggerFileName : {SPFGlobals.__g_LoggerFileName}")    
        return SPFGlobals.__g_LoggerFileName
    __g_Use_UBER_MAO: bool = None
    @property
    def gUse_UBER_MAO(self)-> bool:
        """
        get : USE_UBER_MAO. Flag to control using UBER for MAO data sources 'Y' = use uber
        """
        if SPFGlobals.__g_Use_UBER_MAO is None:
            if self.gIsSvc is True:
                SPFGlobals.__g_Use_UBER_MAO = True
            else:
                _tmp = self.parseCMDArgsToken(self.gCommandLineArguments, 'USE_UBER_MAO', defaultTokenValue="N", doUCase=True, doTrim=True)
                SPFGlobals.__g_Use_UBER_MAO = True if _tmp.strip().upper() == "Y" else False
        self.__logger.debug(f"get : gUse_UBER_MAO : {SPFGlobals.__g_Use_UBER_MAO}")                
        return SPFGlobals.__g_Use_UBER_MAO
    __g_SQLPF_LogSvc_ENV:str = None
    @property
    def gSQLPF_LogSvc_ENV(self)->str:
        """
        get : SQLPF_LogSvc_ENV
        """
        if SPFGlobals.__g_SQLPF_LogSvc_ENV is None:
            SPFGlobals.__g_SQLPF_LogSvc_ENV = self.parseCMDArgsToken(self.gCommandLineArguments, 'SPF_LOGSVC_ENV', defaultTokenValue="-NA-", doUCase=True, doTrim=True)
            if SPFGlobals.__g_SQLPF_LogSvc_ENV == "-NA-":
                self.__logger.debug(f"SPF_LOGSVC_ENV not found in command line. Checking OS environment variable")
                SPFGlobals.__g_SQLPF_LogSvc_ENV = os.getenv("SPF_LOGSVC_ENV")
            if SPFGlobals.__g_SQLPF_LogSvc_ENV is None:
                self.__logger.debug(f"SPF_LOGSVC_ENV not found in command line or OS environment variable, using default")
                SPFGlobals.__g_SQLPF_LogSvc_ENV = "SQLPF_LogSvc"
        self.__logger.debug(f"get : gSQLPF_LogSvc_ENV : {SPFGlobals.__g_SQLPF_LogSvc_ENV}")
        return SPFGlobals.__g_SQLPF_LogSvc_ENV
    
    __g_SQLPF_LogSvc_URL:str = None
    @property
    def gSQLPF_LogSvc_URL(self)->str:
        """
        get : SQLPF_LogSvc_URL
        """
        spfLogServiceURL_preprod = "http://atdvdwspfwb2.amr.corp.intel.com/SQLPF_Log_Service/SQLPF_Log_Service.svc/InsertSQLPFLogData"
        spfLogServiceURL_dev = "http://dtdvdwspfwb1.amr.corp.intel.com/SQLPF_Log_Service/SQLPF_Log_Service.svc/InsertSQLPFLogData"
        spfLogServiceURL_prodN1 = "http://DTDVPWSPFWBN1.amr.corp.intel.com/SQLPF_Log_Service/SQLPF_Log_Service.svc/InsertSQLPFLogData"
        spfLogServiceURL_prodN2 = "http://DTDVPWSPFWBN2.amr.corp.intel.com/SQLPF_Log_Service/SQLPF_Log_Service.svc/InsertSQLPFLogData"
        spfLogServiceURL_prodNLB = "http://dtdpathwebnlb.ch.intel.com/SQLPF_Log_Service/SQLPF_Log_Service.svc/InsertSQLPFLogData"
        if SPFGlobals.__g_SQLPF_LogSvc_URL is None:               
            try:
                
                tmp_SQLPF_LogSvcURL = spfLogServiceURL_prodNLB #f"HTTPS://GOTO.INTEL.COM/{self.gSQLPF_LogSvc_ENV}"
                r = requests.get(tmp_SQLPF_LogSvcURL, verify = False)
                tmp_SQLPF_LogSvcURL = r.url 
                if (tmp_SQLPF_LogSvcURL.lower().find("error") > 0):
                    raise Exception(f"Service Endpoint not found for : '{self.SPF_LogSvc_Env}'")
                if tmp_SQLPF_LogSvcURL.lower()[-4:] == ".svc":
                    tmp_SQLPF_LogSvcURL = f"{tmp_SQLPF_LogSvcURL}/InsertSQLPFLogData"
                
                self.__logger.debug(f"goto : SQLPF_LogSvcURL : {tmp_SQLPF_LogSvcURL}")
                # return tmp_SQLPF_LogSvcURL
            except HTTPError as httpErr:
                self.__logger.exception(F"GOTO Error {httpErr.response.status_code}")
                raise Exception(f"Service Endpoint not found for : '{self.SQLPFLogSvcEnv}'")
            except Exception as err:
                self.__logger.error(f"Error while determining SPFLogSvc URL : {err}")
                raise
            SPFGlobals.__g_SQLPF_LogSvc_URL = tmp_SQLPF_LogSvcURL
        self.__logger.debug(f"get : gSQLPF_LogSvc_URL : {SPFGlobals.__g_SQLPF_LogSvc_URL}")
        return SPFGlobals.__g_SQLPF_LogSvc_URL
    
    __g_ENCODING_UTFBOM = None
    @property
    def gENCODING_UTFBOM(self):
        """
        #get : /ENCODING_UTFBOM from gCommandLineArguments -- 'Flag (Y/N) to indicate whether to use UTF-8-BOM encoding or 'UTF-8' encoding
               E.g., /ENCODING_UTFBOM="Y(True)/N(False)" -- > dedfault 'N'(False) i.e., use 'UTF-8'
        """

        if SPFGlobals.__g_ENCODING_UTFBOM is None:
            _tmp = self.parseCMDArgsToken(self.gCommandLineArguments, 'ENCODING_UTFBOM', defaultTokenValue="N", doUCase=True, doTrim=True)
            if _tmp in ["Y", "YES"]:
                SPFGlobals.__g_ENCODING_UTFBOM = True
            else:
                SPFGlobals.__g_ENCODING_UTFBOM = False
        self.__logger.debug(f"get : gENCODING_UTFBOM : {SPFGlobals.__g_ENCODING_UTFBOM}")
        return SPFGlobals.__g_ENCODING_UTFBOM
    #END : def gENCODING_UTFBOM
     
    __g_ENCODING_FULLFILE_SCAN = None
    @property
    def gENCODING_FULLFILE_SCAN(self):
        """
        #get : /ENCODING_FULLFILE_SCAN from gCommandLineArguments -- 'Flag to indicate whether full file scan needs to happen while determining charset of given source file
               E.g., /ENCODING_FULLFILE_SCAN="Y(True)/N(False)" -- > dedfault 'N'(False)
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__g_ENCODING_FULLFILE_SCAN is None:
            _tmp = self.parseCMDArgsToken(self.gCommandLineArguments, 'ENCODING_FULLFILE_SCAN', defaultTokenValue="N", doUCase=True, doTrim=True)
            if _tmp == "Y":
                SPFGlobals.__g_ENCODING_FULLFILE_SCAN = True
            else:
                SPFGlobals.__g_ENCODING_FULLFILE_SCAN = False
        self.__logger.debug(f"get : gENCODING_FULLFILE_SCAN : {SPFGlobals.__g_ENCODING_FULLFILE_SCAN}")
        return SPFGlobals.__g_ENCODING_FULLFILE_SCAN
    #END : def gENCODING_FULLFILE_SCAN

    __gIsSvc = None
    @property
    def gIsSvc(self)-> bool:
        """
        get: gIsSvc --> True if PyEE is running as Web service
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        if SPFGlobals.__gIsSvc is None:
            curHttpContext = System.Web.HttpContext.Current
            curSvcSecContext = System.ServiceModel.ServiceSecurityContext.Current
            if [curHttpContext, curSvcSecContext] == [None, None]:
                SPFGlobals.__gIsSvc = False #Http & Service contexts are empty -- PyEE is not running as Web service
            else:
                SPFGlobals.__gIsSvc = True #either http or Service context exists - PyEE is running as Web service
            self.logger.debug("{0} - set : {1}; {2}".format(calling_func, SPFGlobals.__gIsSvc, [curHttpContext, curSvcSecContext]))
        return SPFGlobals.__gIsSvc

    __gSvcSessionUser = None
    @property
    def gSvcSessionUser(self):
        """
        get : gSvcSessionUser --> WindowsIdentity object of impersonated user in Service session
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        if self.gIsSvc is True and SPFGlobals.__gSvcSessionUser is None:
            #PyEE Service mode & SessionUser not set
            _session_user_source = None
            try:
                SPFGlobals.__gSvcSessionUser = System.Web.HttpContext.Current.Request.LogonUserIdentity
                _session_user_source = f"HttpContext.Current.Request.LogonUserIdentity : {SPFGlobals.__gSvcSessionUser.Name}"
            except Exception as httpCtxErr:
                self.logger.exception(f"Unable to get identity from HttpContext", httpCtxErr)
                try:
                    SPFGlobals.__gSvcSessionUser = System.ServiceModel.ServiceSecurityContext.Current.WindowsIdentity
                    _session_user_source = f"HttpContext.Current.Request.LogonUserIdentity : {SPFGlobals.__gSvcSessionUser.Name}"
                except Exception as SvcSecCtxErr:
                    self.logger.exception(f"Unable to get identity from ServiceSecurityContext", SvcSecCtxErr)
                    raise Exception(f"Failed to get Session user identity")                                        
            self.logger.debug("{0} - _session_user_source : {1}".format(calling_func, _session_user_source))
        self.logger.debug("{0} - set : {1}".format(calling_func, SPFGlobals.__gSvcSessionUser))
        return SPFGlobals.__gSvcSessionUser

    __gSQLPFSvcCmdArgsList = None
    @property
    def gSQLPFSvcCmdArgsList(self):
        """
        get : List of command line args sent to PyEE, that need to be sent to SQLPFaaS service
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        if SPFGlobals.__gSQLPFSvcCmdArgsList is None:
            #remove unwanted CMD args that are not needed at Svc end
            #1: PyEE entry script. E.g., 'C:\\Users\\vanatara\\My Programs\\SQLPathFinder3\\SPFSQL3.py'
            #2: /SPFSQL arg. E.g., '/SPFSQL=sqlpathfinder.spfsql'
            SPFGlobals.__gSQLPFSvcCmdArgsList = [cmdArgitem for cmdArgitem in self.gCommandLineArguments if (cmdArgitem.upper().endswith('SPFSQL3.PY') is False 
                                                                                                        and  cmdArgitem.upper().startswith('/SPFSQL=') is False)]
        self.__logger.info("{0} get - {1}".format(calling_func, SPFGlobals.__gSQLPFSvcCmdArgsList))
        return SPFGlobals.__gSQLPFSvcCmdArgsList

    __gSPFLogLevel = None
    @property
    def gSPFLogLevel(self):
        """
        get : 
        e.g., /SPFLOGLEVEL=DEBUG
        """
        if (SPFGlobals.__gSPFLogLevel is None):
            SPFGlobals.__gSPFLogLevel = self.parseCMDArgsToken(self.gCommandLineArguments, "SPFLOGLEVEL", defaultTokenValue="ERROR", doUCase=True, doTrim=True)
        self.__logger.debug(f"get gSPFLogLevel : {SPFGlobals.__gSPFLogLevel}")
        return SPFGlobals.__gSPFLogLevel

    __dbGlobalConn = {}
    @property
    def gDBGlobCon(self):
        """
        get : global cache for DB connections
        """
        # e.g., {<conName> : [<conObject>, <creationTime>]}
        return SPFGlobals.__dbGlobalConn

    __dbErrorCodesForRetry = {"O" : ['28000', '12170', '02391', '21561', '12545', '03135', '12631'] #Oracle
                            , "P" : [] #Postgresql
                            }
    @property
    def gDBErrorCodesForRetry(self):
        """
        get : A dictionary of error code for retry for each DB type
        """
        return SPFGlobals.__dbErrorCodesForRetry

    __SPFCollateralsServers = {'PROD' : 'dtdpathwebnlb.ch.intel.com', 'DEV' : 'DTDVDWSPFWB1.amr.corp.intel.com'}   
    @property
    def gSPFColateralsServers(self):
        """
        get : list of servers that service SQLPF collaterals
        """
        return SPFGlobals.__SPFCollateralsServers

    __SPFReportsServers = __SPFCollateralsServers
    @property
    def gSPFReportsServers(self):
        """
        get : list of servers that service SQLPF reports
        """
        return SPFGlobals.__SPFReportsServers

    _gSPFCollateralServerEnv = None
    @property
    def gSPFCollateralServerEnv(self):
        """
        get: Get the COLLATERALSENV params - 
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        
        if SPFGlobals._gSPFCollateralServerEnv is None:
            try:
                _tmp = self.parseCMDArgsToken(self.gCommandLineArguments, "COLLATERALSENV", defaultTokenValue="PROD", doUCase=True, doTrim=True)
                if _tmp not in self.gSPFColateralsServers.keys():
                    _tmp = 'PROD'
                SPFGlobals._gSPFCollateralServerEnv = _tmp
            except Exception as err:
                SPFGlobals._gSPFCollateralServerEnv = 'PROD'
            self.__logger.debug(f"set gSPFCollateralServerEnv : {SPFGlobals._gSPFCollateralServerEnv}")
        self.__logger.debug(f"get gSPFCollateralServerEnv : {SPFGlobals._gSPFCollateralServerEnv}")
        return SPFGlobals._gSPFCollateralServerEnv
    __gDomains = ["amr.corp.intel.com", "ccr.corp.intel.com", "corp.intel.com", "gar.corp.intel.com", "ger.corp.intel.com"]
    @property
    def gDomains(self):
        """
        get : list of domains
        """
        return self.__gDomains

    __gUserPrincipal = None

    @property
    def gUserPrincipal(self):
        """
        get: current logged in user UserPrincipal using win32api e.g., : vishwas.nataraj@intel.com
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gUserPrincipal is None:
            if win32api is None or win32con is None:
                raise RuntimeError("Windows identity integration is unavailable on this platform")
            SPFGlobals.__gUserPrincipal = win32api.GetUserNameEx(win32con.NameUserPrincipal) 
        self.__logger.info("{0} get - {1}".format(calling_func, SPFGlobals.__gUserPrincipal))
        return SPFGlobals.__gUserPrincipal

    __gLoadToMemTable = False
    @property
    def gLoadToMemTable(self) :
        """
        #get: Job Start Day in local time
        gLoadToMemTable = False # flag to indicate if SQLite is to be used as datastore 
        instead of file
        """
        
        calling_func = self.getCallingFuncName()
        """
        Note: this is used in 
        1.) dbDriverCxOracle.execute
        """
        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gLoadToMemTable))
        return SPFGlobals.__gLoadToMemTable

    @gLoadToMemTable.setter
    def gLoadToMemTable(self, value) :
        """
        #set: gLoadToMemTable
        gLoadToMemTable = False # flag to indicate if SQLite is to be used as datastore 
        instead of file
        """
        calling_func = self.getCallingFuncName()
        """
        Note: 
        this is used in 
        1.) dbDriverCxOracle.execute
        this is set to False in 
        1.) GetSiteTimeTask
        """
        SPFGlobals.__gLoadToMemTable = value
        self.__logger.info("{0} - set: {1}".format(calling_func, SPFGlobals.__gLoadToMemTable))

    __gSQLiteVer = None
    @property
    def gSQLiteVer(self) :
        """
        get SQLite3 exe file name to be used
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSQLiteVer is None:
           if self.gIs64 == 1:
               SPFGlobals.__gSQLiteVer = SPFGlobals.__G_SQLiteVer_64
           else:
               SPFGlobals.__gSQLiteVer =  "sqlite3_v2.exe"

        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gSQLiteVer))
        return SPFGlobals.__gSQLiteVer
    
    @property
    def G_DT_FORMAT_YYMONDD(self) :
        return "%y%b%d" #vbscript format : YYMMDD -> '15Oct09'

    @property
    def G_DT_FORMAT_YYMMDD(self) :
        return "%y%m%d" #vbscript format : YYMMDD -> '151009'

    @property
    def G_DT_FORMAT_YYYYMONDD(self) :
        return "%Y%b%d" #vbscript format : YYYYMMDD -> '2015Oct09'

    @property
    def G_DT_FORMAT_YYYYMMDD(self) :
        return "%Y%m%d" #vbscript format : YYYYMMDD -> '20151009'

    @property
    def G_DT_FORMAT_YYYY_MON_DD_HH24_MM_SS(self) :
        return "%Y-%b-%d %H:%M:%S" #'2015-Oct-09 18:30:05'

    @property
    def G_DT_FORMAT_YYYY_MM_DD_HH24_MM_SS(self) :
        return "%Y-%m-%d %H:%M:%S" #'2015-10-09 18:30:05'

    @property
    def G_DT_FORMAT_YYYY_MON_DD_00_00_00(self) :
        return "%Y-%b-%d 00:00:00" #'2015-Oct-09 00:00:00'

    @property
    def G_DT_FORMAT_YYYY_MM_DD_00_00_00(self) :
        return "%Y-%m-%d 00:00:00" #'2015-10-09 00:00:00'

    @property
    def G_DT_FORMAT_YYYYMonDDhhnnss(self) :
        return "%Y%b%d%H%M%S" # '2015Oct09183037'

    @property
    def G_DT_FORMAT_YYYYMMDDhhnnss(self) :
        return "%Y%m%d%H%M%S" # '20151009183105'

    @property
    def G_DT_FORMAT_YYYY_M_DD_HH24_MM_SS(self) :
        #same as oracle's YYYY-MM-DD hh24:mi:ss --> '2015-10-09 18:31:31'
        return "%Y-%m-%d %H:%M:%S"

    @property
    def G_DT_FORMAT_YYYY_M_DD_00_00_00(self) :
        return "%Y-%m-%d 00:00:00" #'2015-10-09 00:00:00'

    @property
    def G_DT_FORMAT_DD_MON_YYYY_HH24_MM_SS(self) :
        return "%d-%b-%Y %H:%M:%S" #'09-Oct-2015 18:32:02'

    __gMySPFJobDay = None #Job Start Day in local time
    @property
    def gMySPFJobDay(self):
        """
        #get: Job Start Day in local time
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gMySPFJobDay is None :
            SPFGlobals.__gMySPFJobDay = str(datetime.now().strftime(self.G_DT_FORMAT_YYYY_M_DD_00_00_00))

        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gMySPFJobDay))
        return SPFGlobals.__gMySPFJobDay

    __gMySPFGWDT = None #Job Start Time in GMT 
    @property
    def gMySPFGWDT(self) :
        """
        #get: Job Start Time in GMT 
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gMySPFGWDT is None :
            #this is always GMT time when the job starts -- no need to get from Site
            SPFGlobals.__gMySPFGWDT = str(datetime.utcnow().strftime(self.G_DT_FORMAT_YYYY_M_DD_HH24_MM_SS))

        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gMySPFGWDT))
        return SPFGlobals.__gMySPFGWDT

    @gMySPFGWDT.setter
    def gMySPFGWDT(self, value) :
        """
        #set: Job Start Time in GMT 
        : this gets set from 'GetSiteTimeTask'
        """
        calling_func = self.getCallingFuncName()
        SPFGlobals.__gMySPFGWDT = value
        self.__logger.info("{0} - set : {1}".format(calling_func, SPFGlobals.__gMySPFGWDT))
        return SPFGlobals.__gMySPFGWDT

    __gMySPFJobDT = None   #Job Start Time for S-A -- Start Day/Time of Job at site
    @property
    def gMySPFJobDT(self):
        """
        #get: Job Start Time for S-A -- Start Day/Time of Job at site
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gMySPFJobDT is None : 
            SPFGlobals.__gMySPFJobDT = str(datetime.now().strftime(self.G_DT_FORMAT_YYYY_M_DD_HH24_MM_SS))

        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gMySPFJobDT))
        return SPFGlobals.__gMySPFJobDT

    __gMySPFJobTime = None #Job Start Day/Time
    @property
    def gMySPFJobTime(self):
        """
        #get: Job Start Day/Time
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gMySPFJobTime))
        return SPFGlobals.__gMySPFJobTime

    @gMySPFJobTime.setter
    def gMySPFJobTime(self, value) :
        """
        #set: Job Start Day/Time
        """
        calling_func = self.getCallingFuncName()
        if value is not None :
            SPFGlobals.__gMySPFJobTime = value

        self.__logger.info("{0} - set: {1}".format(calling_func, SPFGlobals.__gMySPFJobTime))

    __gMyAbort = False      #Global Abort Variable
    @property
    def gMyAbort(self):
        """
        #get: Global Abort Variable
        """
        calling_func = self.getCallingFuncName(levelValue=-2)
        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gMyAbort))
        return SPFGlobals.__gMyAbort

    @gMyAbort.setter
    def gMyAbort(self, value):
        """
        #set: Global Abort Variable
        """
        calling_func = self.getCallingFuncName(levelValue=-2)
        if value is not None:
            SPFGlobals.__gMyAbort = value

        self.__logger.info("{0} - set: {1}".format(calling_func, SPFGlobals.__gMyAbort))

    __gRunMode = "I"      #Run Mode - I,B,IB - Interactive, Batch, Both
    @property
    def gRunMode(self):
        """
        #get/set : Run Mode - I,B,IB - Interactive, Batch, Both
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gRunMode))
        return SPFGlobals.__gRunMode

    @gRunMode.setter
    def gRunMode(self, value):
        """
        #set : Run Mode - I,B,IB - Interactive, Batch, Both
        """
        calling_func = self.getCallingFuncName()
        validRunModes = ["I", "B", "IB"]

        self.__logger.debug(str(value))
        if value is not None:
            value = value.upper()
            if value not in validRunModes : 
                raise Exception("RunMode value should be anyone of " + str(validRunModes))

            SPFGlobals.__gRunMode = value
            self.__logger.info("{0} - set: {1}".format(calling_func, SPFGlobals.__gRunMode))
    
    __gMaxConnectRetry = None #Max Orcl conn retries...value is 1 in SPFSQL3.va?
    @property
    def gMaxConnectRetry(self):
        """
        #get/set: Global Abort Variable
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gMaxConnectRetry is None:
            if not self.gConnectRetry is None:
                SPFGlobals.__gMaxConnectRetry = self.gConnectRetry
            else:
                SPFGlobals.__gMaxConnectRetry = 10 #default value

        self.__logger.info("{0} - get: gMaxConnectRetry = {1}".format(calling_func, SPFGlobals.__gMaxConnectRetry))
        return SPFGlobals.__gMaxConnectRetry
    
    __gConnectRetry = None #this is passed as command line argument '/ConnectRetry'
    @property
    def gConnectRetry(self):
        """
        #get : command line argument '/ConnectRetry'
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gConnectRetry is None :
            # extract value from gCommandLineArguments property
            myConnectRetry = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                for argvItem in self.gCommandLineArguments # FROM
                                if "/CONNECTRETRY" in argvItem.upper()] # WHERE

            self.__logger.debug("len(myConnectRetry) : {0}".format(len(myConnectRetry)))
            self.__logger.debug("myConnectRetry : {0}".format(myConnectRetry))
                
            if len(myConnectRetry) == 0: 
                SPFGlobals.__gConnectRetry = None
            else:
                try:
                    SPFGlobals.__gConnectRetry = int(myConnectRetry[0].strip())
                except Exception as err:
                    self.__logger.exception("{0} - Error while trying to read /CONNECTRETRY argument from command line parameters : {1}".format(calling_func, err))
                    #continue with default value = None
                    SPFGlobals.__gConnectRetry = None

        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gConnectRetry))
        return SPFGlobals.__gConnectRetry

    __gRetryTime = None #this is passed as command line argument '/RetryTime'
    @property
    def gRetryTime(self):
        """
        #get : command line argument '/RetryTime'
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gRetryTime is None :
            # extract value from gCommandLineArguments property
            myRetryTime = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                for argvItem in self.gCommandLineArguments # FROM
                                if "/RETRYTIME" in argvItem.upper()] # WHERE

            self.__logger.debug("len(myRetryTime) : {0}".format(len(myRetryTime)))
            self.__logger.debug("myRetryTime : {0}".format(myRetryTime))
                
            if len(myRetryTime) == 0: 
                SPFGlobals.__gRetryTime = None
            else:
                try:
                    SPFGlobals.__gRetryTime = int(myRetryTime[0].strip())
                except Exception as err:
                    self.__logger.exception("{0} - Error while trying to read /RETRYTIME argument from command line parameters : {1}".format(calling_func, err))
                    #continue with default value = None

        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gRetryTime))
        return SPFGlobals.__gRetryTime

    __gOracleArraySize = None #this is passed as command line argument '/OracleArraySize'
    @property
    def gOracleArraySize(self):
        """
        #get : command line argument '/OracleArraySize'
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gOracleArraySize is None :
            # extract value from gCommandLineArguments property
            myOracleArraySize = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                for argvItem in self.gCommandLineArguments # FROM
                                if "/ORACLEARRAYSIZE" in argvItem.upper()] # WHERE

            self.__logger.debug("len(myOracleArraySize) : {0}".format(len(myOracleArraySize)))
            self.__logger.debug("myOracleArraySize : {0}".format(myOracleArraySize))
                
            if len(myOracleArraySize) == 0: 
                SPFGlobals.__gOracleArraySize = None
            else:
                try:
                    SPFGlobals.__gOracleArraySize = int(myOracleArraySize[0].strip())
                except Exception as err:
                    self.__logger.exception("{0} - Error while trying to read /ORACLEARRAYSIZE argument from command line parameters : {1}".format(calling_func, err))
                    #continue with default value = None
                    SPFGlobals.__gOracleArraySize = None

        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gOracleArraySize))
        return SPFGlobals.__gOracleArraySize

    __gMacroFile = None    #Macro File
    @property
    def gMacroFile(self):
        """
        #get/set: Macro File
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gMacroFile))
        return SPFGlobals.__gMacroFile

    __gRunTimer = None     #Time bt conn retries in seconds
    @property
    def gRunTimer(self):
        """
        #get/set: Time between conn retries in seconds
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gRunTimer is None:
            if not self.gRetryTime is None:
                SPFGlobals.__gRunTimer = self.gRetryTime
            else:
                SPFGlobals.__gRunTimer = 5 #default value
        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gRunTimer))
        return SPFGlobals.__gRunTimer

    __gSPFLib = '\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\'
    @property
    def gSPFLib(self):
        """
        #get/set: Path of SPFLib on common share
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gSPFLib))
        return SPFGlobals.__gSPFLib

    __gSHFolder = None
    @property
    def gSHFolder(self):
        """
        #get the SHFolder if running in SH
        """
        calling_func = self.getCallingFuncName()
        if self.SHisSHEntry == True :
            #SPFGlobals.__gSHFolder = self.SHNearestNASAnalysis.strip() + self.__G_SH_FOLDER_SUFFIX
            SPFGlobals.__gSHFolder = os.path.join(self.SHNearestNASAnalysis.strip(), self.__G_SH_FOLDER_SUFFIX)

        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gSHFolder))
        return SPFGlobals.__gSHFolder

    __gRPath =r"\\atdfile3.ch.intel.com\atd-web\PathFinding\SQLPathFinder\R\R\R-Latest\bin\i386\RTerm.exe"  #R Path
    @property
    def gRPath(self):
        """
        #get/set: Global RPath
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gRPath))
        return SPFGlobals.__gRPath

    __gSecIni=r"\\atdfile3.ch.intel.com\atd-web\PathFinding\SQLPathFinder\Software\Config\sec\hadoop_sec.ini"
    @property
    def gSecIni(self):
        """
        #get/set: Global hadoop configurtion
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gSecIni))
        return SPFGlobals.__gSecIni

    __gRPathSH = None
    @property
    def gRPathSH(self):
        """
        #get: Global RPath in SH
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gRPathSH is None :
            self.__logger.debug("SHisSHEntry : {0}".format(self.SHisSHEntry))
            if self.SHisSHEntry == True :
                if self.gisSPFonSH == True :
                    Tmp2 = self.GetIni(os.path.join(self.gSPFSWSHDir, "R_Version.ini"),"R_VERSION", self.gRPathVersion)
                    self.__logger.debug("Tmp2 : {0}".format(Tmp2))
                    if Tmp2 is None or len(str(Tmp2).strip()) == 0 :
                        SPFGlobals.__gRPathSH = self.gRPath
                    else :
                        #temp3 = os.path.join(self.gSPFRSWSHDir, *Tmp2.split("\\"))
                        #self.__logger.debug("temp3 : {0}".format(temp3))
                        SPFGlobals.__gRPathSH = os.path.join(self.gSPFRSWSHDir, *Tmp2.split("\\")) #temp3 #
                else : #SPF SW is NOT installed on SH drones
                    MyPrefix = self.SHNearestNASAnalysis
                    self.__logger.debug("MyPrefix : " + str(MyPrefix))
                    #Tmp = MyPrefix + SPFGlobals.__G_SH_R_PATH_SUFFIX
                    Tmp = os.path.join(MyPrefix,SPFGlobals.__G_SH_R_PATH_SUFFIX)
                    self.__logger.debug(str(Tmp))
                    Tmp2 = self.GetIni(Tmp,"R_VERSION", self.gRPathVersion)
                    self.__logger.debug("Tmp2 : " + str(Tmp2))
                    if Tmp2 is None or len(Tmp2) == 0 :
                        SPFGlobals.__gRPathSH = self.gRPath
                    else :
                        SPFGlobals.__gRPathSH = Tmp2

        SPFGlobals.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gRPathSH))
        return SPFGlobals.__gRPathSH

    __gSPFVaryLib = None    #Global - SPF Library which depends on how application is run
    @property
    def gSPFVaryLib(self):
        """
        #get/set: Global - SPF Library which depends on how application is run
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFVaryLib is None :
            if self.SHisSHEntry is True :
                if self.gisSPFonSH is True :
                    SPFGlobals.__gSPFVaryLib = self.gSPFSWSHDir
                else :
                    SPFGlobals.__gSPFVaryLib = self.gSHFolder
            elif SPFGlobals.gMyLocal == "N" : #'Interactive or run interactive outside SPF
                SPFGlobals.__gSPFVaryLib = os.path.join (self.gSPFLib, "library")
            else :
                SPFGlobals.__gSPFVaryLib = self.gMyLocal

        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gSPFVaryLib))
        return SPFGlobals.__gSPFVaryLib

    __gRPathVersion = None
    @property
    def gRPathVersion(self):
        """
        #get/set: R Path Version : Valid= DEFAULT,PREVIOUS,NEXT : Default = DEFAULT
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gRPathVersion is None :
            myRver = self.gR_VERSION
            if (myRver not in ["DEFAULT", "NEXT"]) : #(myRver != "DEFAULT" and myRver != "NEXT") :
                SPFGlobals.__gRPathVersion = "DEFAULT"
            else :
                SPFGlobals.__gRPathVersion = myRver

        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gRPathVersion))
        return SPFGlobals.__gRPathVersion

    __gLocalDir = None #Curr dir
    @property
    def gLocalDir(self):
        """
        #get/set: Curr dir
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gLocalDir is None :
            SPFGlobals.__gLocalDir = os.path.abspath(os.path.curdir) + "\\"
        
        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gLocalDir))
        return SPFGlobals.__gLocalDir

    __gTempDir = None  #SH Local Dir
    @property
    def gTempDir(self):
        """
        #get/set: SH Local Dir
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gTempDir is None :
            if self.SHisSHEntry == True :
                if self.gisSPFonSH == True  :
                    SPFGlobals.__gTempDir = self.gSPFSWSHDir
                else :
                    SPFGlobals.__gTempDir = self.SHtempFldr
            else :
                SPFGlobals.__gTempDir = os.getenv("temp")

        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gTempDir))
        return SPFGlobals.__gTempDir

    __gAnyLoop="N"     #Site/Run Loop Abort Flag (N,YS, YR,ES, ER)
    @property
    def gAnyLoop(self):
        """
        #get/set: Site/Run Loop Abort Flag (N,YS, YR,ES, ER)
        Default : N
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get : {1}".format(calling_func, SPFGlobals.__gAnyLoop))
        return SPFGlobals.__gAnyLoop
    #END : def gAnyLoop.getter

    @gAnyLoop.setter
    def gAnyLoop(self, value) :
        """
        #set: Site/Run Loop Abort Flag should be one in [N,YS, YR,ES, ER]
        """
        calling_func = self.getCallingFuncName()
        if value is not None and len(value.strip()) > 0 :
            if value in self.__G_ANYLOOP_LOOKUP :
                SPFGlobals.__gAnyLoop = value
            else :
                raise Exception("invalid value for gAnyLoop. Should be one of : " + str(self.__G_ANYLOOP_LOOKUP))

        self.__logger.info("{0} - set : {1}".format(calling_func, SPFGlobals.__gAnyLoop))
    #END : def gAnyLoop.setter

    __gAnyLoopCtr=0 #Nesting Lvl. 1=Topmost
    @property
    def gAnyLoopCtr(self):
        """
        #get/set: Nesting Lvl. 1=Topmost
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get : {1}".format(calling_func, SPFGlobals.__gAnyLoopCtr))
        return SPFGlobals.__gAnyLoopCtr

    @gAnyLoopCtr.setter
    def gAnyLoopCtr(self, value) :
        """
        set: Nesting Lvl. 1=Topmost
        """
        calling_func = self.getCallingFuncName()
        if value is not None :
            SPFGlobals.__gAnyLoopCtr = value
        self.__logger.info("{0} - set : {1}".format(calling_func, SPFGlobals.__gAnyLoopCtr))
     
    #gHTMDelete = []   #HTML-Report items to delete
    __gHTMDelete = []   #HTML-Report items to delete
    @property
    def gHTMDelete(self):
        """
        #get: HTML-Report items to delete
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get : {1}".format(calling_func, SPFGlobals.__gHTMDelete))
        return SPFGlobals.__gHTMDelete

    @gHTMDelete.setter
    def gHTMDelete(self, value):
        """
        #set: HTML-Report items to delete
        """
        calling_func = self.getCallingFuncName()
        if not value is None :
            SPFGlobals.__gHTMDelete= value
            self.__logger.info("{0} - set : {1}".format(calling_func, SPFGlobals.__gHTMDelete))

    __gLineTerminator = "\n"
    @property
    def gLineTerminator(self):
        """
        #Return Type : string
        #get/set: Global Abort Variable
        """
        return SPFGlobals.__gLineTerminator

    __g_gVars = {}
    @property
    def g_gVars(self):
        """
        #Return Type : Dictionary object
        #get/set : global variables from gCommandLineArguments
        E.g., /CL_Operation=1210 /CL_Lot=T510E045 --> 
        {'cl_operation' : '1210', 'cl_lot' : 'T510E045'}
        """
        calling_func = self.getCallingFuncName()
        if len(SPFGlobals.__g_gVars) == 0:
            try :
                #first get all the /CL_xx into a list
                mygVarsArg = [argvItem.strip(' /') # SELECT
                               for argvItem in self.gCommandLineArguments # FROM
                               if "/CL_" in argvItem.upper()] # WHERE
                self.__logger.debug("{0} - len(mygVarsArg) : '{1}'".format(calling_func, len(mygVarsArg)))

                if len(mygVarsArg) == 0 : 
                    self.__logger.info("{0} - /CL options not found in command line arguments".format(calling_func))
                    return SPFGlobals.__g_gVars

                #next step through each /CL_xx item 
                for i in range(len(mygVarsArg)) :
                #for item in mygVarsArg :
                    item = mygVarsArg.pop(0)
                    #split @ =
                    itemsTknValList = item.split(self.__CMD_ARGS_TKN_NM_VAL_SEP)
                    #1. check token is not incomplete
                    #2. support for only first 19 /CL_xx
                    if itemsTknValList[0] != ("cl_") and SPFGlobals.__g_VarNo < self.__G_VARNO_MAX :
                        SPFGlobals.__g_VarNo = SPFGlobals.__g_VarNo + 1
                        self.__logger.debug("{0} - SPFGlobals.__g_VarNo : {1} ".format(calling_func, SPFGlobals.__g_VarNo))
                        self.__logger.debug("{0} - Adding to g_gVars Key : '{1}' value : '{1}'".format(calling_func, itemsTknValList[0], itemsTknValList[1]))
                        SPFGlobals.__g_gVars[str(itemsTknValList[0])] = str(itemsTknValList[1])


                self.__logger.debug("{0} - len(g_gVars) : {1}".format(calling_func, len(SPFGlobals.__g_gVars)))
                #SPFGlobals.__g_gVars = mygVarsArg
            except Exception as err:
                self.__logger.exception("{0} - Error while trying to read /CL argument from command line parameters : {1}".format(calling_func, err))
                raise

        self.__logger.info("{0} - g_gVars : {1}".format(calling_func, SPFGlobals.__g_gVars))
        return SPFGlobals.__g_gVars

    __FOR_READING = 1
    @property
    def FOR_READING(self):
        """
        """
        calling_func = self.getCallingFuncName()
        return SPFGlobals.__FOR_READING

    __FOR_WRITING = 2
    @property
    def FOR_WRITING(self):
        """
        """
        calling_func = self.getCallingFuncName()
        return SPFGlobals.__FOR_WRITING


    __g_VarNo = -1
    @property
    def g_VarNo(self):
        """
        #get : global variables count
        """
        calling_func = self.getCallingFuncName()
        return SPFGlobals.__g_VarNo

    __gSPFJSHeader = r"<!--@SPF-JS-HEADER@-->"
    @property
    def gSPFJSHeader(self) :
        """
        """
        calling_func = self.getCallingFuncName()
        return SPFGlobals.__gSPFJSHeader

    __gIs64 = None
    @property
    def gIs64(self):
        """
        # may not be needed in py -- since we are targetting for 64bit python
        # Return type : Boolean
        # get: retrun True if OS is 64bit else False for 32bit OS
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gIs64 is None :
            MyOS = os.getenv("ProgramW6432")
            self.__logger.debug("MyOS : " + str(MyOS))
            if MyOS is None :
                SPFGlobals.__gIs64 = False
            else :
                SPFGlobals.__gIs64 = True

        self.__logger.debug(str(SPFGlobals.__gIs64))
        return SPFGlobals.__gIs64

    __SPFLibVersion = "2.2.9.9_VA30_168"
    @property
    def __version__(self):
        """
        # get : SPFLib version
        # return type : string
        """
        #'Procedure: SPFSQL3       PyEEDate: 03/14/2023   VaEEDate: 09/30/2020
        #region Version History 
        #'07/27/20 vanatara 2.0.0.99_VA30_161 Update : spfsql3.py[2.0.2.39] : nqSQLiteTask().Repl_Col_Pattern_GetLine1() -- support filtering columns in actual output for MongoDB/SQLite 'Column-Pattern', StartMacroTask().executeTaskCommand() - bug fix for nested macro task referring to parent macro
        #'08/07/20 vanatara 2.0.1.0_VA30_161 Update : spfsql3.py[2.0.2.40] : NormalQueryTaskBase().Repl_Col_Pattern() -- Bug fix for zero column match scenario
        #'08/11/20 vanatara 2.0.1.1_VA30_161 Update : spfsql3.py[2.0.2.41] : NormalQueryTaskBase().Repl_Col_Pattern() -- Update to support 'REGEX' & 'REGEX FILE' patterns
        #'08/19/20 vanatara 2.0.1.1a_VA30_161 Update : spfsql3.py[2.0.2.42] : NormalQueryTaskBase().Repl_Col_Pattern() -- Update to support 'FUNCTION FILE'
        #'08/25/20 vanatara 2.0.1.2_VA30_161 Update : spfsql3.py[2.0.2.43] : NormalQueryTaskBase().Repl_Col_Pattern() -- Update to support 'REGEX', 'REGEX FILE', 'FUNCTION FILE'
        #                                                                    nqMongoTask().Repl_Col_Pattern_ParseSPFN(), Repl_Col_Pattern_funcFile_GetLine1() to handle 'FUNCTIOn FILE' & Repl_Col_Pattern_regexFile_GetLine1() to handle 'REGEX FILE'
        #                                                                    nqSQLiteTask().perform_Repl_Col_Pattern_GetLine1_postCheck(), Repl_Col_Pattern_funcFile_GetLine1() to handle 'FUNCTIOn FILE' &  Repl_Col_Pattern_regexFile_GetLine1() to handle 'REGEX FILE'
        #                                     Update : utils.py[2.0.5.9] : SPFEmail() -- updated file attachment to use the actual file name when using outlook to send emails
        #                                     Update : SPFGlobals.py[2.0.1.2] : Added GetSPFFunctionIni() - to retrive the setting name from 'schema\spf_functions.ini' file
        #'09/01/20 vanatara 2.0.1.3a_VA30_161 BugFix : spfsql3.py[2.0.2.44] : nqMongoTask().Repl_Col_Pattern_funcFile_GetLine1() 'FUNCTION FILE' bug fix for nested SPF_FN$
        #'09/10/20 vanatara 2.0.1.5_VA30_161 BugFix : spfsql3.py[2.0.2.46] : nqMongoTask().Repl_Col_Pattern_funcFile_GetLine1().function_file_pattern() -- handle static fields & repeating fields specified in '<pattern>' col-pattern. 
        #'09/15/20 vanatara 2.0.1.5a_VA30_161 BugFix : spfsql3.py[2.0.2.47] : nqMongoTask().Repl_Col_Pattern_funcFile_GetLine1() & Repl_Col_Pattern_regexFile_GetLine1()  -- remove duplicate headers
        #'09/16/20 vanatara 2.0.1.5b_VA30_161 Update : spfsql3.py[2.0.2.48] : nqMongoTask().Repl_Col_Pattern_regexFile_GetLine1()  -- updated to handle all REGEX FILE patterns in single backend call to improve performance
        #'09/21/20 vanatara 2.0.1.6_VA30_161 Update : dbDrivers.py[1.0.0.2], utils.py[2.0.6.1] : Changes to use SMTPAuth
        #'09/24/20 vanatara 2.0.1.6a_VA30_161 Update : dbDrivers.py[1.0.0.2a], utils.py[2.0.6.1a],SPFGlobals.py[2.0.1.4a] : Changes to support getting collateral data (.datx)
        #'09/29/20 vanatara 2.0.1.6b_VA30_161 Update : spfsql3.py[2.0.2.50] : nqOracleTask().performCommand() fix create output file with headers when zero records output 
        #'10/05/20 vanatara 2.0.2.1_VA30_164  Update : spfsql3.py[2.0.2.52] :  nqTeradataTask().get_uni_pwi() fix logging issue, 
        #                                     Update : dbDrivers.py[1.0.0.4] :  bug fix for importing error handling clases
        #'11/11/20 vanatara 2.0.2.5_VA30_164  Update : spfsql3.py[2.0.2.56]  : IfThenTask() fix error handling, ENV variable handling in 2nd condition + value case handling for string comparision 
        #                                     Update : dbDrivers.py[1.0.0.6] : updated error message when user not member of role
        #'11/18/20 vanatara 2.0.2.7_VA30_164  Update : utils.py[2.0.6.4]  : Check_Column_Pattern() bugfix when only 1 column matches pattern causing 'list index out of range' error when processing updated column data
        #'11/26/20 vanatara 2.0.2.8_VA30_164  Update : utils.py[2.0.6.5]  : SPFEmail() - support 'useSMTPAuth' flag, if SMTP fails auto retry with outlook (only in non SH)
        #                                     Update : dbDrivers.py[1.0.0.7] : updated SPFSMTPAuthEmail.SednEMail() -- accept useSMTPAuth flag, use SMTPAuth.datx with new format and multiple SMTPAuth login usage logic
        #'12/14/20 vanatara 2.0.2.9_VA30_164  Update : dbDrivers.py[1.0.0.8]  : updated SPFSMTPAuthEmail.SednEMail() -- 'account expired' console message. dbDriverBase fix reference to 'writeCursorToTxtFile'
        #                                     Update : spfsql3.py[2.0.2.57] : NormalQueryTaskBase.Get_CSV_List_2() update console message when zero records in input file
        #'12/20/20 vanatara 2.0.3.0_VA30_164  Update : spfsql3.py[2.0.2.58] : nqMongoTask() updated '__init__()', override base method 'Repl_Col_Pattern()', add 'ParseMongoPost_Opts()' with nested methods, update 'Repl_Col_Pattern_funcFile_GetLine1()', update 'Run_PyMongoVA()', update 'Get_Mongo_Opt()' to handle MongoPost_Col_xxx options parsing
        #                                                                     nqSQLite() updated '__init__()', update 'Repl_Col_Pattern_GetLine1()', update 'Repl_Col_Pattern_regexFile_GetLine1()'
        #                                     Update : __init__.py[2.0.0.2] : add pathlib import
        #'01/06/21 vanatara 2.0.3.1_VA30_164  Update : spfsql3.py[2.0.2.59] : XLSToCSVTask() updated to check file extension missing and use dtype=object to avoid data conversion when reading excel using pandas.read_excel().
        #                                                                     nqMongoTask.Run_PyMongoVA() added support for <MongoMQCS=XXXX/> option
        #'01/15/21 vanatara 2.0.3.2_VA30_164  Update : spfsql3.py[2.0.2.61] : NormalQueryTaskBase().Repl_Col_Pattern() updated to handle 'O' coltype. nqMongoTask() add Repl_Col_Pattern_funcFile_GetLine1_Hdrs() to handle 'O' coltype to return only headers
        #'01/22/21 jmclarke 2.0.3.5_VA30_164  Update : spfsql3.py[2.0.2.63] : In proc Substitute_Schema, replace DB link tokens @pub_cptd_wip/@pub_cptd_ODS... for SIMS ODS production migration
        #'01/22/21 vanatara 2.0.3.6_VA30_164  Update : dbDrivers.py[1.0.1.1] : updated to use .Net Cryptography
        #                                     Update : memtable.py[2.0.1.6] : removed unused encryption feature
        #                                     Update : __init__.py[2.0.0.3] : add clr:System.Security.Cryptography     
        #'02/02/21 vanatara 2.0.3.6a_VA30_164  Update : dbDrivers.py[1.0.1.2] : updated to support AllowedUsers configuration
        #'02/23/21 vanatara 2.0.3.7_VA30_164  Update : spfsql3.py[2.0.2.65] : NormalQueryTaskBase().Get_Multi_Node_List(),.executeTaskCommand() updated to handle empty map_nodes scenario
        #                                     Update : memtable.py[2.0.1.7] : fix bug that removes leading spaces while reading column data in Create_SQL_In_Like_List()
        #                                     Update : utils.py[2.0.6.7] : SPOHandler() - raise error in SH with message to check troubleshooting webpage
        #'03/02/21 vanatara 2.0.3.8_VA30_164  BugFix : memtable.py[2.0.1.8] : bugfix handling dual join of single input .csv in Run_SQLite()
        #'03/16/21 vanatara 2.0.3.9_VA30_164  Update : utils.py[2.0.6.8] : Add ConsoleWarnMsgContinue(), ConsoleInfoMsgContinue() to standardize console messages.
        #                                                                  PivotTable(),pivotDF(): new 'pivotDupFunction' optional parameter + changes for handling 'duplicate data' issue during pivoting
        #                                     Update : spfsql3.py[2.0.2.66] : SPFTaskBase.parseTaskOptions(), NormalQueryTaskBase().pivotTable() support 'PIVOT_FUNCTION' spfsql option 
        #                                     Update : dbDrivers.py[1.0.1.4] : dbDriverCxOracle.execute() - handle multiple(batch) SQL statements execution
        #'03/19/21 vanatara 2.0.3.9a_VA30_164  Update : utils.py[2.0.6.8a] : Code finalize for PIVOT_FUNCTION support
        #                                     Update : spfsql3.py[2.0.2.66a] : nqSQLiteTask().performCommand() to support /APPENDMODE
        #                                     Update : dbDrivers.py[1.0.1.4c] : dbDriverCxOracle.execute() bug fix to return record count
        #                                     Update : memtable.py[2.0.1.9] : Run_SQLite() support /APPENDMODE
        #'03/22/21 vanatara 2.0.3.9b_VA30_164  bugfix : spfsql3.py[2.0.2.66b] : nqSQLiteTask().performCommand() /APPENDMODE use the derived 'FirstConnect' property similar to other DB handlers. NormalQueryTaskBase().Repl_Col_Pattern() updated REGEX syntax to allow column pattern searching
        #'03/23/21 vanatara 2.0.3.9c_VA30_165  Update : utils.py[2.0.6.8b] : pivotDF() commented out logging duplicate data
        #'04/22/21 vanatara 2.0.4.0_VA30_165  bugfix : spfsql3.py[2.0.2.67a] : GetFilesTask().getFilesInfoFromFolderGlob(),getFileInfo() update to use pathlib.Path() API to perform file search]
        #'04/30/21 vanatara 2.0.4a.0_VA30_165  Update : spfsql3.py[2.0.2.67b] : Add nqOracleTask().validateDBNodesAndPrepSubstituteArr() to handle IMO nodes subsitution in SH
        #'05/20/21 vanatara 2.0.5.0_VA30_165  Update : spfsql3.py[2.0.2.69] : Update nqMongoTask.Run_PyMongoVA(), Repl_Col_Pattern_funcFile_GetLine1()  to support 'MongoSubComp_NotRegexFile' & 'MongoSubComp_FunctionFileName' options
        #                                     Update : memtable.py[2.0.2.0] : MemTable.sqliteRegexSearch() added to support 'SPFRegexSearch' SQLite UDF to provide regex-search on column data
        #'05/21/21 vanatara 2.0.5a.0_VA30_165  bugfix : spfsql3.py[2.0.2.69a] : fix NormalQueryTaskBase.Repl_Col_Pattern() handling of J, B Coltypes for Mongo 'FUNCTION-FILE' case during SQLite join step
        #'06/02/21 vanatara 2.0.6.0_VA30_165  bugfix : spfsql3.py[2.0.2.70] : fix GetFilesTask() to support wildcard in folder part of path
        #                                                                     fix StackDataTask() for codec issue 
        #                                                                     fix RunLoopTask() codec issue 
        #                                     Update : utils.py[2.0.6.9] : StackTable() fix codec issue
        #                                                                  SPOHandler2() - changes to use SPOHandlerclient.exe with updated MS Graph packages
        #'06/04/21 vanatara 2.0.6a.0_VA30_165  bugfix : dbDrivers.py[1.0.1.5] : dbDriverCxOracle.execute() -- fix issue related to 'WITH' clause
        #'06/05/21 vanatara 2.0.6b.0_VA30_165  bugfix : utils.py[2.0.6.9a] : Process_R_HTM() -- comment out print statement
        #'06/14/21 vanatara 2.0.7.0_VA30_165   Update : spfsql3.py[2.0.2.71] : fix GetFilesTask() error if only single file specified
        #                                                                    : update NormalQueryTask(), nqMongoTask() - add Process_Get_CSV_List2_Other(), Process_Get_CSV_List2_Other_converter() to support InTemp for MongoDB queries
        #					                                                 : Update support BLOB for Oracle Queries
        #                                     Update : dbDrivers.py[1.0.1.6] : dbDriverCxOracle.execute() -- add support for handling BLOB data using 'output_handler' mechanism
        #                                     Update : memtable.py[2.0.2.1]  : MemTable.SPFWriteLOBToFile() added to support 'SPFWriteLOBToFile' SQLite UDF to write column value to a file specified 
        #endregion Version History  

		#'06/18/21 vanatara 2.0.7.1_VA30_165  Update : SPFGlobals.py[2.0.1.6] : added 'gDBGlobCon' global DB connection cache
        #                                     Update : dbDrivers.py[1.0.1.7] : dbDriverODBCSAPHana.genConnectionString() -- handle connection string. updated NodesInfo.NodeInfo() -- invoke Utils.Substitute_Global_Var() to perform substitution. Update support BULKLOAD, GLOBALCON options
        #                                     Update : utils.py[2.0.7.0] : Update support BULKLOAD, GLOBALCON options. Add parseDelimitedStringAsList(), class BulkloadDataHandler()
        #                                     Update : spfsql3.py[2.0.2.72] : Update support BULKLOAD, GLOBALCON options - parse new options
        #'06/18/21 vanatara 2.0.7.4_VA30_165  Update : spfsql3.py[2.0.2.76] : Added nqDenodoTask. Bug Fix GetFilesTask when search path points to directory without any search pattern
        #                                     Update : dbDrivers.py[1.0.1.8] : Add dbDriverODBCDenodo to support Denodo ODBC driver
        #'08/31/21 vanatara 2.0.7.7_VA30_165  Update : memtable.py[2.0.2.2] : MemTable.Create_SQL_In_Like_List() update to ignore rows with just '%' for LIKE case
        #'09/23/21 vanatara 2.0.9.0_VA30_165  Update : spfsql3.py[2.0.2.82] : Update to handle options(/OUTLOOK=Y|N|S|SA) & cmd-args(/USESMTPAUTH=Y|N) to support Email: Outlook/SMTP/SMTPAuth. Update SPFManager.GetQuery(), SPFTaskBase.parseTaskOptions(), EmailTask, HTMLLayoutTask 
        #                                                                     Support queries with OLEDB=PYSCRIPTDRIVER. Added 'nqPyScriptDriverTask', Added 'dbDriverPYScript'
        #                                                                     MIDAS-HBASE SQL_Get_CSV_List2 : Add NormalQueryTaskBase.Process_Get_CSV_List2_Other_MH_Prepare_SQLSelect(), Updated NormalQueryTaskBase.parseSQLStr_4_GET_CSV_LIST.CheckReplace_TokenVal(), Update NormalQueryTaskBase.Process_Get_CSV_List2_Other()
        #                                     Update : utils.py[2.0.7.3] : Bug fix SPOHandler2() cmd-args passing to external .exe.
        #                                                                  Updated SPFEmail() to handle Outlook/SMTP/SMTPAuth options/cmd-args and auto-retry with SMTP if SMTPAuth fails.
        #'10/12/21 vanatara 2.0.9.1_VA30_165  BugFix : spfsql3.py[2.0.2.83] : ForLoopTask() - for fractional iteration
        #'10/12/21 vanatara 2.0.9.3_VA30_165  BugFix : utils.py[2.0.7.5] : Bugfix handling encoding while writing SQLite3 query result to output file.
        #                                     BugFix : memtable.py[2.0.2.3] : Update MemTable.Run_SQLite() + writeCursorToFileWithQuote() + writeCursorToFileWithoutQuote() + writeCursorToFile() to use DB encoding, add get_SQLite_Encoding() to get DB encodig based on data loaded.
        #                                     BugFix : spfsql3.py[2.0.2.84] : Bug fix nqSQLiteTask.perform_Repl_Col_Pattern_GetLine1_postCheck() - handling duplicate columns bug
        #'11/08/21 vanatara 2.0.9.3a_VA30_165  BugFix : spfsql3.py[2.0.2.84b] : Bug fix nqSQLiteTask.perform_Repl_Col_Pattern_GetLine1_postCheck() - handling duplicate columns bug + update the logic
        #'11/11/21 vanatara 2.0.9.4_VA30_165 Update : dbDrivers.py[1.0.1.9] : Update oracle BLOB datatype handling due to cx_Oracle pkg update
        #'01/03/22 vanatara 2.0.9.9_VA30_165 BugFix : utils.py[2.0.7.6] : Bugfix in IntelWW() handling timepart of date input.
        #'01/20/22 vanatara 2.1.0.0_VA30_165 BugFix : spfsql3.py[2.0.2.88] : removed unused dbDriverMongo references. BugFix for Mongo RegexFile column pattern treat column names as string.
        #'01/28/22 vanatara 2.1.0.1_VA30_165 BugFix : utils.py[2.0.7.7] : Bugfix Pivot+sort pandas read_csv set dtype=object to avoid inferring datatype leading to altering a string number value + same applied in other places where pandas reads source files 
        #'02/02/22 vanatara 2.1.0.2_VA30_165 Update : spfsql3.py[2.0.2.89] : update for mongoaggregate_v2.py inline execution
        #'02/14/22 vanatara 2.1.0.3_VA30_165 Update : utils.py[2.0.7.9] : Update Pivot+sort : parsing+using /SORT option with '-1' indicating column is of 'int' datatype
        #'02/18/22 vanatara 2.1.0.3a_VA30_165 Update : utils.py[2.0.7.9a] : Sync JC changes + Update Pivot+sort : parsing+using /SORT option - specify Datatype only if sort column is character data, rest all columns pandas will infer
        #'02/22/22 vanatara 2.1.0.4_VA30_165 Update : spfsql3.py[2.0.2.89] : ForLoopTask incremental step convert to float
        #'03/03/22 vanatara 2.1.0.5_VA30_165 Update : spfsql3.py[2.0.2.90] : SmartAppendTask.smartAppend3_localSrcFile() determing encoding of new-file and use it while performing smart-append of old-file
        return SPFGlobals.__SPFLibVersion
    #END : def __version__

    __gSPFDir = None
    @property
    def gSPFDir(self) :
        """
        #get : /SPFDIR from gCommandLineArguments -- 'Optional SPF Install Directory
               E.g., /SPFDIR="<path>" -- > <path>
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFDir is None :
            try : 
                # extract value from gCommandLineArguments property
                mySPFDir = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                 for argvItem in self.gCommandLineArguments # FROM
                                 if "/SPFDIR" in argvItem.upper()] # WHERE

                self.__logger.debug("len(mySPFDir) : {0}".format(len(mySPFDir)))
                self.__logger.debug("mySPFDir : {0}".format(mySPFDir))
                
                if len(mySPFDir) == 0 : 
                    SPFGlobals.__gSPFDir = None
                else :
                    SPFGlobals.__gSPFDir = os.path.expandvars(mySPFDir[0])
                    if os.path.exists(SPFGlobals.__gSPFDir) is False :
                        errMsg = ("The SQLPathFinder Install directory passed to this job does not exist:\n"
                                  "{0}").format(SPFGlobals.__gSPFDir)
                        raise Exception(errMsg)
            except Exception as err:
                self.__logger.exception("{0} - Error while trying to read /SPFDIR argument from command line parameters : {1}".format(calling_func, err))
                raise

        self.__logger.info(str(SPFGlobals.__gSPFDir))
        return SPFGlobals.__gSPFDir
    #END : def gSPFDir(self)

    __gSPFInstance = None
    @property
    def gSPFInstance(self):
        """
        #get : /SPFINSTANCE from gCommandLineArguments
               E.g., /SPFINSTANCE="1234" -- > 1234
               Default : RnSTR
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFInstance is None :
            try :
                # extract value from gCommandLineArguments property
                mySPFInstanceArg = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                 for argvItem in self.gCommandLineArguments # FROM
                                 if "/SPFINSTANCE" in argvItem.upper()] # WHERE

                self.__logger.debug("{0} - len(mySPFInstanceArg) : {1}".format(calling_func, len(mySPFInstanceArg)))
                self.__logger.debug("{0} - mySPFInstanceArg : {1}".format(calling_func, mySPFInstanceArg))
                
                if len(mySPFInstanceArg) == 0 : 
                    mySPFInstanceArg.append("{0}".format(int(32767 * random.random() + 1)))
                    self.__logger.debug("{0} - setting to RnSTR : {1}".format(calling_func, mySPFInstanceArg))

                SPFGlobals.__gSPFInstance = mySPFInstanceArg[0]
            except Exception as err :
                self.__logger.exception("Error while trying to read /SPFINSTANCE argument from command line parameters", err)
                raise

        self.__logger.info("{0} - get: gSPFInstance : {1}".format(calling_func, SPFGlobals.__gSPFInstance))
        return SPFGlobals.__gSPFInstance

    @gSPFInstance.setter
    def gSPFInstance(self, value):
        """
         #set : /INSTANCE from <OPTIONS> section of SPF Query
        """
        calling_func = self.getCallingFuncName()
        if not value is None:
            SPFGlobals.__gSPFInstance  =value
            self.__logger.info("{0} - set: gSPFInstance : {1}".format(calling_func, SPFGlobals.__gSPFInstance))


    __gSPFSQLFileName = None
    @property
    def gSPFSQLFileName(self) :
        r"""
        #get : SPFSQL file name from gCommandLineArguments
                # handles below cases
                # 1. ".\sqlpathfinder.spfsql"
                # 2. /SPFSQL=".\sqlpathfinder.spfsql"
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFSQLFileName is None :
            try :
                mySPFSQLArg = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "')
                               for argvItem in self.gCommandLineArguments
                               if "/SPFSQL" in argvItem.upper() 
                               or 
                               ".SPFSQL" in argvItem.upper()]

                if len(mySPFSQLArg) == 0 : 
                    if self.gCommandLineArguments[1].lower().endswith(".spf") is True :
                        mySPFSQLArg = [self.gCommandLineArguments[1]]
                    else :
                        raise Exception("/SPFSQL not found in command line arguments")

                self.__logger.debug("{0} - len(mySPFSQLArg) : {1}".format(calling_func, len(mySPFSQLArg)))
                self.__logger.debug("{0} - mySPFSQLArg : {1}".format(calling_func, mySPFSQLArg))
                SPFGlobals.__gSPFSQLFileName  = os.path.abspath(mySPFSQLArg[0])

            except Exception as err:
                self.__logger.exception("{0} - Error while trying to read SPFQL argument from command line parameters \n{1}".format(calling_func, err))
                raise

        self.__logger.info("{0} - SPFSQLFileName : {1}".format(calling_func, SPFGlobals.__gSPFSQLFileName))
        return SPFGlobals.__gSPFSQLFileName

    __gSPFSQLFileNameOnly = None
    @property
    def gSPFSQLFileNameOnly(self) :
        """
        #get : SPFSQL file name part only from gSPFSQLFileName
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFSQLFileNameOnly is None :
            try :
                SPFGlobals.__gSPFSQLFilePath, SPFGlobals.__gSPFSQLFileNameOnly  = os.path.split(self.gSPFSQLFileName)
            except Exception as err:
                self.__logger.exception("{0} - Error while trying to read SPFQL argument from command line parameters \n{1}".format(calling_func, err))
                raise

        self.__logger.info("{0} - gSPFSQLFileNameOnly : {1}".format(calling_func, SPFGlobals.__gSPFSQLFileNameOnly))
        return SPFGlobals.__gSPFSQLFileNameOnly

    __gSPFSQLFilePath = None
    @property
    def gSPFSQLFilePath(self) :
        """
        #get : SPFSQL file name part only from gSPFSQLFileName
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFSQLFilePath is None :
            try :
                SPFGlobals.__gSPFSQLFilePath, SPFGlobals.__gSPFSQLFileNameOnly  = os.path.split(self.gSPFSQLFileName)
            except Exception as err:
                self.__logger.exception("{0} - Error while trying to read SPFQL argument from command line parameters \n{1}".format(calling_func, err))
                raise

        self.__logger.info("{0} - gSPFSQLFilePath : {1}".format(calling_func, SPFGlobals.__gSPFSQLFilePath))
        return SPFGlobals.__gSPFSQLFilePath

    __gSPFSQLFile_NamePart = None
    @property
    def gSPFSQLFile_NamePart(self) :
        """
        #get : SPFSQL file name part only from gSPFSQLFileName
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFSQLFile_NamePart is None :
            try :
                SPFGlobals.__gSPFSQLFile_NamePart, SPFGlobals.__gSPFSQLFile_ExtPart  = os.path.splitext(self.gSPFSQLFileNameOnly)
            except Exception as err:
                self.__logger.exception("{0} - Error while trying to read SPFQL argument from command line parameters \n{1}".format(calling_func, err))
                raise

        self.__logger.info("{0} - gSPFSQLFile_NamePart : {1}".format(calling_func, SPFGlobals.__gSPFSQLFile_NamePart))
        return SPFGlobals.__gSPFSQLFile_NamePart

    __gSPFSQLFile_ExtPart = None
    @property
    def gSPFSQLFile_ExtPart(self) :
        """
        #get : SPFSQL file name part only from gSPFSQLFileName
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFSQLFile_ExtPart is None :
            try :
                SPFGlobals.__gSPFSQLFile_NamePart, SPFGlobals.__gSPFSQLFile_ExtPart  = os.path.splitext(self.gSPFSQLFileNameOnly)
            except Exception as err:
                self.__logger.exception("{0} - Error while trying to read SPFQL argument from command line parameters \n{1}".format(calling_func, err))
                raise

        self.__logger.info("{0} - gSPFSQLFile_ExtPart : {1}".format(calling_func, SPFGlobals.__gSPFSQLFile_ExtPart))
        return SPFGlobals.__gSPFSQLFile_ExtPart

    __gSPFSQLFileData = None
    @property
    def gSPFSQLFileData(self) :
        r"""
        #get : SPFSQL file data, read from gSPFSQLFileName
                # handles below cases
                # 1. ".\sqlpathfinder.spfsql"
                # 2. /SPFSQL=".\sqlpathfinder.spfsql"
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFSQLFileData is None :
            try :
                mySPFSQLFileName = self.gSPFSQLFileName 
                if os.path.exists(mySPFSQLFileName) and os.path.isfile(mySPFSQLFileName):
                    with open(mySPFSQLFileName, "rU") if isPYTHON2 else open(mySPFSQLFileName, "rt", encoding=self.detectFileEncoding(mySPFSQLFileName,readall=True)) as fileToRead : #, encoding=self.gOSDefaultEncoding
                    #with open(mySPFSQLFileName, "rt") as fileToRead: 
                        SPFGlobals.__gSPFSQLFileData = fileToRead.read()

                    SPFGlobals.__gSPFSQLFileData = SPFGlobals.__gSPFSQLFileData.lstrip()
                    if SPFGlobals.__gSPFSQLFileData.startswith('{') is True and SPFGlobals.__gSPFSQLFileData.endswith("}") is True:
                        from . import dbDrivers
                        from .dbDrivers import spfsqlxParser  
                        SPFGlobals.__gSPFSQLFileData = spfsqlxParser().parse(SPFGlobals.__gSPFSQLFileData)
                    if len(SPFGlobals.__gSPFSQLFileData) == 0 :
                        raise Exception("Empty Query passed ...")
                else :
                    raise Exception("Could not Open : '{0}' \nCalling Routine: {1} ".format(mySPFSQLFileName, calling_func))

            except Exception as err:
                self.__logger.exception("{0} - Error while trying to read from SPFSQLFile : {1}".format(calling_func, err))
                raise
        self.__logger.debug("{0} - len(gSPFSQLFileData) : {1}".format(calling_func, len(str(SPFGlobals.__gSPFSQLFileData))))
        self.__logger.info("{0} - gSPFSQLFileData : {1}".format(calling_func, SPFGlobals.__gSPFSQLFileData))

        return SPFGlobals.__gSPFSQLFileData

    __gSPFMacroFile = None
    @property
    def gSPFMacroFile(self) :
        r"""
        #get : /MACROFILE from gCommandLineArguments
               E.g., /MACROFILE="c:\m.csv" --> c:\m.csv
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFMacroFile is None :
            try :
                
                mySPFMacroArg = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "') # SELECT
                                 for argvItem in self.gCommandLineArguments # FROM
                                 if "/MACROFILE" in argvItem.upper()] # WHERE
                
                self.__logger.debug("{0} - len(mySPFMacroArg) : {1}".format(calling_func, len(mySPFMacroArg)))
                self.__logger.debug("{0} - mySPFMacroArg : {1}".format(calling_func, mySPFMacroArg))
                if len(mySPFMacroArg) > 0 :
                    SPFGlobals.__gSPFMacroFile = mySPFMacroArg[0]
                    if os.path.exists(SPFGlobals.__gSPFMacroFile) == True :
                        self.__logger.info("MACROFILE found : {0}".format(SPFGlobals.__gSPFMacroFile) )
                    else :
                        SPFGlobals.__gMyAbort = True #abort 
                        
                        raise Exception("Macro File does not exist: '{0}' ".format(SPFGlobals.__gSPFMacroFile))

                else :
                    self.__logger.warn("Did not find /MACROFILE argument from command line parameters")
                    # dont raise error it is just that '/MACROFILE' was not specified in input arguments
            except Exception as err:
                SPFGlobals.__gSPFMacroFile = None
                self.__logger.exception("{0} - {1}".format(calling_func, err))
                raise

        self.__logger.info("{0} - gSPFMacroFile : {1}".format(calling_func, SPFGlobals.__gSPFMacroFile))
        return SPFGlobals.__gSPFMacroFile

    __gSPFImmediateMacroFile = None
    @property
    def gSPFImmediateMacroFile(self) :
        r"""
        #get : /IMMEDIATE_MACROFILE from gCommandLineArguments
               E.g., /IMMEDIATE_MACROFILE="c:\im.csv" --> c:\im.csv
        """
        calling_func = self.getCallingFuncName(levelValue=-2)

        #if SPFGlobals.__gSPFImmediateMacroFile is None :
        try :
            if SPFGlobals.__gSPFImmediateMacroFile is None :   
                mySPFImmediateMacroArg = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "') # SELECT
                                    for argvItem in self.gCommandLineArguments  # FROM
                                    if "/IMMEDIATE_MACROFILE" in argvItem.upper()]  # WHERE
                self.__logger.debug("{0} - len(mySPFImmediateMacroArg) : {1}".format(calling_func, len(mySPFImmediateMacroArg)))
                self.__logger.debug("{0} - mySPFImmediateMacroArg : {1}".format(calling_func, mySPFImmediateMacroArg))

                if len(mySPFImmediateMacroArg) > 0 :
                    SPFGlobals.__gSPFImmediateMacroFile = mySPFImmediateMacroArg[0]
            
                    if os.path.exists(SPFGlobals.__gSPFImmediateMacroFile) == True :
                        self.__logger.info("{0} - IMMEDIATE_MACROFILE found : {1}".format(calling_func, SPFGlobals.__gSPFImmediateMacroFile) )
                    else :
                        SPFGlobals.__gMyAbort = True #abort 
                        errMsg = ""
                        raise Exception("Macro File Passed to job does not exist: '{0}'".format(SPFGlobals.__gSPFImmediateMacroFile))
                else :
                    self.__logger.warn("{0} - /IMMEDIATE_MACROFILE argument not found in command line parameters".format(calling_func))
                    # dont raise error it is just that '/IMMEDIATE_MACROFILE' was not specified in input arguments
        except Exception as err:
            self.__logger.exception("{0} - {1}".format(calling_func, err))
            #reset to None on any error
            SPFGlobals.__gSPFImmediateMacroFile = None
            raise

        self.__logger.info("{0} - gSPFImmediateMacroFile : {1}".format(calling_func, SPFGlobals.__gSPFImmediateMacroFile))
        return SPFGlobals.__gSPFImmediateMacroFile

    __gR_VERSION = None
    @property
    def gR_VERSION(self) :
        """
        #get : /R_VERSION from gCommandLineArguments
               E.g., /R_VERSION="DEFAULT" --> DEFAULT
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gR_VERSION is None :
            try :
                # extract value from gCommandLineArguments property
                myR_VERSIONArg = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                 for argvItem in self.gCommandLineArguments # FROM
                                 if "/R_VERSION" in argvItem.upper()] # WHERE

                self.__logger.debug("len(myR_VERSIONArg) : " + str(len(myR_VERSIONArg)))
                self.__logger.debug("myR_VERSIONArg : " + str(myR_VERSIONArg))
                
                if len(myR_VERSIONArg) == 0 : myR_VERSIONArg.append("DEFAULT")

                SPFGlobals.__gR_VERSION = myR_VERSIONArg[0]                
            except Exception as err :
                self.__logger.exception("{0} - Error while trying to read /R_VERSION argument from command line parameters : {1}".format(calling_func, err))
                raise

        self.__logger.info(str(SPFGlobals.__gR_VERSION))
        return SPFGlobals.__gR_VERSION


    __gCommandLineArguments = []
    @property
    def gCommandLineArguments(self) :
        """
        #Return Type : List object
        #get/set : command line arguments passed during ivoke
        """
        calling_func = self.getCallingFuncName()

        self.__logger.info("{0} - get : {1} ".format(calling_func, SPFGlobals.__gCommandLineArguments))
        return SPFGlobals.__gCommandLineArguments

    @gCommandLineArguments.setter
    def gCommandLineArguments(self, value) :
        """
        #get/set : command line arguments passed during ivoke -- this is set in the main method SPFSQLManager class
        # this value is used in the get methods for below properties
        1. gSPFSQLFileName
        2. gSPFMacroFile
        3. gSPFImmediateMacroFile
        """
        calling_func = self.getCallingFuncName()

        if value is None :
            raise Exception("value is None...provide a valid value")
        errMsg = "value is not a list type: {0}".format(value)

        if isPYTHON2:
            assert type(value) is types.ListType, errMsg
        else: #Python 3
            assert type(value) is list, errMsg

        if len(SPFGlobals.__gCommandLineArguments) == 0 :
            SPFGlobals.__gCommandLineArguments = value

        else :
            self.__logger.info("{0} - SPFGlobals.__gCommandLineArguments overwritten".format(calling_func))
            SPFGlobals.__gCommandLineArguments = value
            self.__reInitStaticPropsDueToCmdUpdate()
            

        self.__logger.info("{0} - set gCommandLineArguments : {1} ".format(calling_func, SPFGlobals.__gCommandLineArguments))
    #END : def gCommandLineArguments

    __gMyEXEDir = None
    @property
    def gMyEXEDir(self) :
        """
        #Return type : string/None
        #get : Get exe dir to determine local or remote query
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gMyEXEDir is None :
            # extract value from gCommandLineArguments property
            my__gExeDir = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                for argvItem in self.gCommandLineArguments # FROM
                                if "/EXEDIR" in argvItem.upper()] # WHERE

            self.__logger.debug("len(my__gExeDir) : " + str(len(my__gExeDir)))
            self.__logger.debug("my__gExeDir : " + str(my__gExeDir))

            if len(my__gExeDir) != 0 : 
                MyEXEDir = my__gExeDir[0]
                MyExeFile = None
            else :
                osPathFileInfo = os.path.split(os.path.abspath(self.gCommandLineArguments[0]))
                self.__logger.debug("{0} - osPathFileInfo : {1}".format(calling_func, osPathFileInfo))

                MyEXEDir, MyExeFile = osPathFileInfo[0], osPathFileInfo[1]

            if MyEXEDir != '' : 
                MyEXEDir = MyEXEDir + "\\" #'Include \

                self.__logger.debug("{0} - MyEXEDir : {1}".format(calling_func, MyEXEDir))
                self.__logger.debug("{0} - MyExeFile : {1}".format(calling_func, MyExeFile))
                SPFGlobals.__gMyEXEDir = MyEXEDir
            else :
                self.__logger.warn("{0} - not able to determine the EXE dir...please check input".format(calling_func))

        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gMyEXEDir))
        return SPFGlobals.__gMyEXEDir
    #END : def gMyEXEDir

    __gSH_ERR_COPY_LOG = None
    @property
    def gSH_ERR_COPY_LOG(self) :
        """
        #Return type : string/None
        #get : Get SH_ERR_COPY_LOG parameter from command line...this is the path to where the log files need to be copied
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSH_ERR_COPY_LOG is None :
            # extract value from gCommandLineArguments property
            my__gSH_ERR_COPY_LOG = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                for argvItem in self.gCommandLineArguments # FROM
                                if "/SH_ERR_COPY_LOG" in argvItem.upper()] # WHERE

            self.__logger.debug("len(my__gSH_ERR_COPY_LOG) : " + str(len(my__gSH_ERR_COPY_LOG)))
            self.__logger.debug("my__gSH_ERR_COPY_LOG : " + str(my__gSH_ERR_COPY_LOG))

            if len(my__gSH_ERR_COPY_LOG) != 0 : 
                SPFGlobals.__gSH_ERR_COPY_LOG = my__gSH_ERR_COPY_LOG[0]

        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gSH_ERR_COPY_LOG))
        return SPFGlobals.__gSH_ERR_COPY_LOG
    #END : def gSH_ERR_COPY_LOG

    __gSPFGUI = None
    @property
    def gSPFGUI(self) :
        """
        #Return type : None/string
        #get : /SPFGUI parameter from command line. This is used to determine if should display select output to Grid
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFGUI is None :
            # extract value from gCommandLineArguments property
            my__gSPFGUI = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                for argvItem in self.gCommandLineArguments # FROM
                                if "/SPFGUI" in argvItem.upper()] # WHERE

            if len(my__gSPFGUI) != 0 : 
                SPFGlobals.__gSPFGUI = my__gSPFGUI[0]
            else:
                SPFGlobals.__gSPFGUI="N"
        return SPFGlobals.__gSPFGUI
    #END : def gSPFGUI
    
    __gMyLocal = None
    @property
    def gMyLocal(self) :
        """
        #Return type : string --> MyEXEDir or 'N' 
        #get : if running in local mode
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gMyLocal is None :
            #from VA Call_Process_Array()
            if not self.gSPFDir is None :
                SPFGlobals.__gMyLocal = self.gSPFDir
            else :
                #check if MYLOCAL was passed in gCommandLineArguments -- py pack/batch execution
                myMyLocalArg = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "') # SELECT
                                     for argvItem in self.gCommandLineArguments # FROM
                                     if "/MYLOCAL" in argvItem.upper()] # WHERE
                
                self.__logger.debug("{0} - len(myMyLocalArg) : {1}".format(calling_func, len(myMyLocalArg)))
                self.__logger.debug("{0} - myMyLocalArg : {1}".format(calling_func, myMyLocalArg))
                if len(myMyLocalArg) > 0 :
                    SPFGlobals.__gMyLocal = myMyLocalArg[0]
                else : 
                    #nothing in gCommandLineArguments -- continue
                    MyLocal = self.gMyEXEDir
                    self.__logger.debug("{0} - MyLocal : {1}".format(calling_func, MyLocal))
                    if (MyLocal.upper().startswith(r"\\AZSTMGTSPATH01") == True 
                        or MyLocal.upper().startswith(r"\\ATDFILE3") == True
                        or MyLocal.upper().find("ANALYSIS$\\APPLICATIONS\\") > 0 ) :
                        MyLocal = "N"

                    SPFGlobals.__gMyLocal = MyLocal

        self.__logger.info("{0} - {1}".format(calling_func, SPFGlobals.__gMyLocal))
        return SPFGlobals.__gMyLocal
    #END : def gMyLocal

    __un = None
    @property
    def gUN(self):
        """
        ' get : USERNAME from environment variables
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__un is None : 
            if win32api is None or win32con is None:
                raise RuntimeError("Windows identity integration is unavailable on this platform")
            SPFGlobals.__un = win32api.GetUserNameEx(win32con.NameSamCompatible).split("\\")[1] #os.environ['USERNAME']
        self.__logger.debug("{0} - get: {1}".format(calling_func, SPFGlobals.__un))

        return SPFGlobals.__un
    #END : def gUN

    __uDomain = None
    @property
    def gUDomain(self):
        """
        ' get : USERDOMAIN from environment variables
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__uDomain is None : 
            if win32api is None or win32con is None:
                raise RuntimeError("Windows identity integration is unavailable on this platform")
            SPFGlobals.__uDomain = win32api.GetUserNameEx(win32con.NameSamCompatible).split("\\")[0] #os.environ['USERDOMAIN']
        self.__logger.debug("{0} - get: {1}".format(calling_func, SPFGlobals.__uDomain))

        return SPFGlobals.__uDomain
    #END : def gUN

    __gExecutionMode = None
    @property
    def gExecutionMode(self) :
        """
        ' get : Execution mode 'ExecMode' if exists in cmdArguments
        'Note: this is used in unit testing from vstudio
        'used only in main method to raise actual exception rather return run status code 
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        if SPFGlobals.__gExecutionMode is None :
            try :
                # extract value from gCommandLineArguments property
                my__gExecutionMode = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                 for argvItem in self.gCommandLineArguments # FROM
                                 if "/EXECMODE" in argvItem.upper()] # WHERE

                self.__logger.debug("len(my__gExecutionMode) : " + str(len(my__gExecutionMode)))
                self.__logger.debug("my__gExecutionMode : " + str(my__gExecutionMode))
                
                if len(my__gExecutionMode) == 0 : 
                    my__gExecutionMode.append("Normal")

                SPFGlobals.__gExecutionMode = my__gExecutionMode[0]                
            except Exception as err:
                self.__logger.exception("{0} - Error while trying to read /EXECMODE argument from command line parameters : {1}".format(calling_func, err))
                raise

        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gExecutionMode))
        return SPFGlobals.__gExecutionMode

    #END : def gExecutionMode

    __gRNStr = None
    @property
    def gRNStr(self):
        """
        ' get: global static random number string 
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gRNStr is None : 
            SPFGlobals.__gRNStr = self.RNStrNew
        self.__logger.info("{0} - get: gRNStr: {1}".format(calling_func, SPFGlobals.__gRNStr))
        return SPFGlobals.__gRNStr

    __gSPFExe = None
    @property
    def gSPFExe(self):
        """
        ' get: global EXE path
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gSPFExe is None :
            if self.SHisSHEntry is True : 
                SPFGlobals.__gSPFExe = self.gTempDir
            else :
                if self.gMyLocal == "N" :
                    SPFGlobals.__gSPFExe = os.path.join(self.gSPFLib, "library")
                else :
                    SPFGlobals.__gSPFExe = self.gMyLocal
        self.__logger.info("{0} - get: gSPFExe: {1}".format(calling_func, SPFGlobals.__gSPFExe))
        return SPFGlobals.__gSPFExe

    __gSPFSWSHDir = r"d:\sqlpathfinder\software"
    @property
    def gSPFSWSHDir(self):
        """
        ' get: global SH Software folder
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get: gSPFSWSHDir: {1}".format(calling_func, SPFGlobals.__gSPFSWSHDir))
        return SPFGlobals.__gSPFSWSHDir

    __gSPFRSWSHDir = r"d:\sqlpathfinder\R"
    @property
    def gSPFRSWSHDir(self):
        """
        ' get: global SH Software folder
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get: gSPFRSWSHDir: {1}".format(calling_func, SPFGlobals.__gSPFRSWSHDir))
        return SPFGlobals.__gSPFRSWSHDir

    __gisSPFonSH = None #False #"N"
    @property
    def gisSPFonSH(self):
        """
        ' get: 'Is SPF Installed on SH
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gisSPFonSH is None :
            tmp1 = os.path.join(self.__gSPFSWSHDir, "R_Version.ini")
            self.__logger.info("{0} - gisSPFonSH: tmp1 : {1}".format(calling_func, tmp1))
            self.__logger.info("{0} - gisSPFonSH: os.path.exists : {1}".format(calling_func, os.path.exists(tmp1)))
            if os.path.exists(tmp1) is True :
                SPFGlobals.__gisSPFonSH = True # SPF SW is Installed on SH
            else :
                SPFGlobals.__gisSPFonSH = False # SPF SW is NOT Installed on SH
        self.__logger.info("{0} - get: gisSPFonSH: {1}".format(calling_func, SPFGlobals.__gisSPFonSH))
        return SPFGlobals.__gisSPFonSH

    @gisSPFonSH.setter
    def gisSPFonSH(self, value):
        """
        ' set: bool True/False 'Is SPF Installed on SH
        """
        calling_func = self.getCallingFuncName()
        SPFGlobals.__gisSPFonSH = value
        self.__logger.info("{0} - set: gisSPFonSH: {1}".format(calling_func, SPFGlobals.__gisSPFonSH))

    __g_CWCtr = 1
    @property
    def g_CWCtr(self):
        """
        ' get: global 'Create Win Rpt Ctr to ensure uniqueness for JS plots
        ' Note: This is reset for every job in PMQ @ SPFSQL3.py.PromptJobIDTask()
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get: g_CWCtr: {1}".format(calling_func, SPFGlobals.__g_CWCtr))
        return SPFGlobals.__g_CWCtr

    @g_CWCtr.setter
    def g_CWCtr(self, value):
        """
        ' set: global 'Create Win Rpt Ctr to ensure uniqueness for JS plots
        ' Note: This is reset for every job in PMQ @ SPFSQL3.py.PromptJobIDTask()
        """
        calling_func = self.getCallingFuncName()
        if not value is None :
            SPFGlobals.__g_CWCtr = value
        self.__logger.info("{0} - set: g_CWCtr: {1}".format(calling_func, SPFGlobals.__g_CWCtr))

    __g_ChartCtr = 1
    @property
    def g_ChartCtr(self):
        """
        ' get: global 'Chart "
        ' Note: This is reset for every job in PMQ @ SPFSQL3.py.PromptJobIDTask()
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get: g_ChartCtr: {1}".format(calling_func, SPFGlobals.__g_ChartCtr))
        return SPFGlobals.__g_ChartCtr

    @g_ChartCtr.setter
    def g_ChartCtr(self, value):
        """
        ' set: global 'Chart "
        ' Note: This is reset for every job in PMQ @ SPFSQL3.py.PromptJobIDTask()
        """
        calling_func = self.getCallingFuncName()
        if not value is None :
            SPFGlobals.__g_ChartCtr = value
            self.__logger.info("{0} - set: g_ChartCtr: {1}".format(calling_func, SPFGlobals.__g_ChartCtr))

    __gg_ChartCtr = 0
    @property
    def gg_ChartCtr(self):
        """
        ' get: global 'Global Chart Ctr"
        ' Note: This is reset to 0 for every job in PMQ @ SPFSQL3.py.PromptJobIDTask()
        """
        calling_func = self.getCallingFuncName()
        self.__logger.info("{0} - get: gg_ChartCtr: {1}".format(calling_func, SPFGlobals.__gg_ChartCtr))
        return SPFGlobals.__gg_ChartCtr

    @gg_ChartCtr.setter
    def gg_ChartCtr(self, value):
        """
        ' set: global 'Global Chart Ctr"
        ' Note: This is reset to 0 for every job in PMQ @ SPFSQL3.py.PromptJobIDTask()
        """
        calling_func = self.getCallingFuncName()
        if not value is None :
            #if value.isdigit() is True :
            SPFGlobals.__gg_ChartCtr = value
            self.__logger.info("{0} - set: gg_ChartCtr: {1}".format(calling_func, SPFGlobals.__gg_ChartCtr))

    __gPyPathSH = None
    @property
    def gPyPathSH(self) :
        """
        ' get: global static Python installation path 
        """
        calling_func = self.getCallingFuncName()
        if SPFGlobals.__gPyPathSH is None :
            if self.SHisSHEntry is True :
                MyPrefix = self.SHNearestNASAnalysis
                SPFGlobals.__gPyPathSH = os.path.join(MyPrefix, self.__G_SH_PY_PATH)
            elif self.gMyLocal == "N" :
                SPFGlobals.__gPyPathSH = r"\\atdfile3.ch.intel.com\atd-web\PathFinding\SQLPathFinder\Python"
            else :
                SPFGlobals.__gPyPathSH = self.gMyLocal
        
        self.__logger.info("{0} - get: gWinCoShare: {1}".format(calling_func, SPFGlobals.__gPyPathSH))
        return SPFGlobals.__gPyPathSH

    __giReportsVersion = None
    @property
    def giReportsVersion(self) :
        """
        #Return type : string Default Value = "DEFUALT"
        #get : Get Interactive Reports version parameter from commandline args
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        if SPFGlobals.__giReportsVersion is None :
            # extract value from gCommandLineArguments property
            my__giReportsVersion = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP)[-1].strip(' "').upper() # SELECT
                                for argvItem in self.gCommandLineArguments # FROM
                                if "/IREPORTS_VERSION" in argvItem.upper()] # WHERE

            self.__logger.debug("{0} - len(my__giReportsVersion) : {1}".format(calling_func, len(my__giReportsVersion)))
            self.__logger.debug("{0} - my__giReportsVersion : {1}".format(calling_func, my__giReportsVersion))

            MyiReportsVersion = None
            if len(my__giReportsVersion) != 0 : 
                MyiReportsVersion = my__giReportsVersion[0].strip().upper()
            if MyiReportsVersion not in ["DEFAULT", "NEXT"]:
                MyiReportsVersion = "DEFAULT"
            self.__logger.debug("{0} - MyiReportsVersion : {1}".format(calling_func, MyiReportsVersion))

            SPFGlobals.__giReportsVersion = MyiReportsVersion

        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__giReportsVersion))
        return SPFGlobals.__giReportsVersion
    #END : def giReportsVersion
    @property
    def G_Error_Msg_with_HelpPage(self):
        """
        get: G_Error_Msg_with_HelpPage -- Error message with SPf web help page
        """
        return  r"Please search for the error string on web page: https://wiki.ith.intel.com/display/SQLPathFinder/SQLPathFinder+Troubleshooting to determine next steps."

    __gOSDefaultEncoding = None
    @property
    def gOSDefaultEncoding(self):
        """
        get: OS default encoding using locale module. Required for Python 3 
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        if SPFGlobals.__gOSDefaultEncoding is None:
            # if isPYTHON2:
            #     SPFGlobals.__gOSDefaultEncoding = sys.getfilesystemencoding()
            # else:
            #     SPFGlobals.__gOSDefaultEncoding = locale.getdefaultlocale()[1] #set to UTF-8
            if self.gENCODING_UTFBOM is True:
                SPFGlobals.__gOSDefaultEncoding = 'utf-8-sig' #use default utf-8-sig
            else:
                SPFGlobals.__gOSDefaultEncoding = 'utf-8' #use default utf-8
        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gOSDefaultEncoding))
        return SPFGlobals.__gOSDefaultEncoding

    __gSPFPyEEPath = None
    @property
    def gSPFPyEEPath(self):
        """
        get: Get the path of PyEE based on spfsql3.py invocation  
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        if SPFGlobals.__gSPFPyEEPath is None:
            SPFGlobals.__gSPFPyEEPath, spfsql3_filename = os.path.split(self.gCommandLineArguments[0])
        self.__logger.info("{0} - get: {1}".format(calling_func, SPFGlobals.__gSPFPyEEPath))
        return SPFGlobals.__gSPFPyEEPath

    @property
    def g_SPFSPOParams(self):
        """
        get: Get the Sharepoint online params
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        SPOLogMode = False
        SPOenableMSALLogging = False

        try:
            _SPOLogMode = self.parseCMDArgsToken(self.gCommandLineArguments, "SPOLOGMODE", defaultTokenValue="N", doUCase=True, doTrim=True)
            if (_SPOLogMode == "Y"):
                SPOLogMode = True
        except Exception as err:
            SPOLogMode = False
        self.__logger.debug(f"set SPOLogMode : {SPOLogMode}")
        try:
            _SPOenableMSALLogging = self.parseCMDArgsToken(self.gCommandLineArguments, "SPOENABLEMSALLOGGING", defaultTokenValue="N", doUCase=True, doTrim=True)
            if (_SPOenableMSALLogging == "Y"):
                SPOenableMSALLogging = True
        except Exception as err:
            SPOenableMSALLogging = False
        self.__logger.debug(f"SPOenableMSALLogging : {SPOenableMSALLogging}")
        return ["e29ac29a-9c61-40e6-b4e8-8a755fd6b92c"      #"ClientID" :
              , "46c98d88-e344-4ed4-8496-4ed7712e255d"      #"TenantID" : 
              , SPOenableMSALLogging                        #"enableMSALLogging" : 
              , True                                        #"msalenablePiiLogging" :
              , False                                       #"msalenableDefaultPlatformLogging" : 
              , SPOLogMode                                  #"logMode" : 
              , "DEBUG"                                     #"logLevel" :
              ]

    _g_SPOMsallIWAAuthProvider = None
    @property
    def g_SPOMsallIWAAuthProvider(self):
        """
        get : Sharepoint online MsallIWAAuthProvider object. This a static object will be set by one of the methods utils.py.SPFWebCopy(), 
        """
        self.__logger.debug(f"get : g_SPOMsallIWAAuthProvider : {SPFGlobals._g_SPOMsallIWAAuthProvider}")
        return SPFGlobals._g_SPOMsallIWAAuthProvider

    @g_SPOMsallIWAAuthProvider.setter
    def g_SPOMsallIWAAuthProvider(self, MsallIWAAuthProviderObject):
        """
        set : Sharepoint online MsallIWAAuthProvider object. This a static object will be set by one of the methods utils.py.SPFWebCopy(), 
        """
        if (SPFGlobals._g_SPOMsallIWAAuthProvider is None):
            self.__logger.debug(f"set : MsallIWAAuthProviderObject : {MsallIWAAuthProviderObject}")
            SPFGlobals._g_SPOMsallIWAAuthProvider =  MsallIWAAuthProviderObject
        self.__logger.debug(f"set : g_SPOMsallIWAAuthProvider : {SPFGlobals._g_SPOMsallIWAAuthProvider}")

    _g_SMTPAuth = None
    @property
    def g_SMTPAuth(self):
        """
        get : /SMPTAUTH command line param 'Y': True, 'N':False
        """
        if SPFGlobals._g_SMTPAuth is None:
            _g_SMTPAuth_temp = self.parseCMDArgsToken(self.gCommandLineArguments, "USESMTPAUTH", defaultTokenValue="", doUCase=True, doTrim=True)
            if self.SHisSHEntry is True:
                #if SH env then only SMTP is supported
                SPFGlobals._g_SMTPAuth = False
            else:
                if _g_SMTPAuth_temp == "Y":
                    SPFGlobals._g_SMTPAuth = True
                elif _g_SMTPAuth_temp == "N":
                    SPFGlobals._g_SMTPAuth = False
                else:
                    SPFGlobals._g_SMTPAuth = "" # no value was passed
        self.__logger.debug(f"get : g_SMTPAuth : {SPFGlobals._g_SMTPAuth}")
        return SPFGlobals._g_SMTPAuth 
    
    _g_UseSQLPFSVC = None
    @property
    def gUseSQLPFSVC(self):
        """
        get: /USESQLPSVC command line param 'Y' : True (execute entire query using SQLPF Service), 'N' : False (Execute normall flow) 
        """
        if SPFGlobals._g_UseSQLPFSVC is None:
            _g_UseSQLPFSVC_temp = self.parseCMDArgsToken(self.gCommandLineArguments, "USESSQLPFSVC", defaultTokenValue="", doUCase=True, doTrim=True)
            
            if _g_UseSQLPFSVC_temp == "Y":
                SPFGlobals._g_UseSQLPFSVC = True
            else:
                SPFGlobals._g_UseSQLPFSVC = False  #!= "Y" or No value passed
        self.__logger.debug(f"get : gUseSQLPFSVC : {SPFGlobals._g_UseSQLPFSVC}")
        return SPFGlobals._g_UseSQLPFSVC 
    _g_SQLPFSVCENV = None
    @property
    def gSQLPFSVCENV(self):
        """
        get: /SQLPFSVCENV command line param 'Y' : True (execute entire query using SQLPF Service), 'N' : False (Execute normall flow) 
        """
        if SPFGlobals._g_SQLPFSVCENV is None:
            SPFGlobals._g_SQLPFSVCENV = self.parseCMDArgsToken(self.gCommandLineArguments, "SQLPFSVCENV", defaultTokenValue=None, doUCase=True, doTrim=True)

        self.__logger.debug(f"get : gSQLPFSVCENV : {SPFGlobals._g_SQLPFSVCENV}")
        return SPFGlobals._g_SQLPFSVCENV 
    #endregion static properties End

    #region -- non static properties
    @property
    def RNStrNew(self):
        """
        ' get : fresh random number
        """
        #return int(32767 * random.random() + 1)
        return str(int(32767 * random.random() + 1))
    #endregion -- non static properties

    def __init__(self, *args, **kwargs) :
        loggerName = None
        super(SPFGlobals, self).__init__()
        
        try :
            if SPFGlobals.__isInitialized is False :
                if not args is None and len(args) > 0:
                    loggerName = args[0]["loggerName"]

                if SPFGlobals.__logger is None :
                    SPFGlobals.__logger = SPFLogger.GetLogger(loggerName)

                #initialize below properties
                self.gMySPFGWDT
                self.gMySPFJobDay
                self.gMySPFJobDT
                #initialize log tracker
                SPFGlobals.gLogTracker = []
                SPFGlobals.__isInitialized = True
            return None
        except Exception as err:
            print("Error while initializing : {0}\n{1}".format(__name__, err.args[0]))
            raise
    #END : def __init__
    def GetExeAppPath(self, ExeAppName_local, ExeAppName_SH=None):
        """
        Build the path of the SPF helper Exe apps based on te execution environment
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        out_exeAppPath = ""

        self.__logger.debug(f"{calling_func} - Get path for : local : {ExeAppName_local}; SH : {ExeAppName_SH}")
        if self.SHisSHEntry is True:
            if ExeAppName_SH in [None, ""]:
                ExeAppName_SH = ExeAppName_local
            out_exeAppPath = os.path.join(self.gTempDir, ExeAppName_SH)
        elif self.gMyLocal == "N":
            out_exeAppPath = os.path.join(self.gSPFVaryLib, ExeAppName_local)
        else:
            out_exeAppPath = os.path.join(self.gMyLocal, ExeAppName_local)

        self.__logger.debug(f"{calling_func} - Set path for : local : {ExeAppName_local}; SH : {ExeAppName_SH} : {out_exeAppPath}")
        return out_exeAppPath
    #END : def GetExeAppPath

    #@staticmethod
    def __reInitStaticPropsDueToCmdUpdate(self) :
        """
        # This method is invoked by SPFGlobals.gCommandLineArguments setter if its value gets updated
        #   Note: Usually this is triggered only during testing and not during a prod run
        """        
        calling_func = self.getCallingFuncName()
        SPFGlobals.__logger.info("re Initializing properties due to gCommandLineArguments update")
        SPFGlobals.__g_gVars = {}
        SPFGlobals.__g_VarNo = -1
        SPFGlobals.__gAnyLoop = "N"
        SPFGlobals.__gAnyLoopCtr = 0
        SPFGlobals.__gHTMDelete = []
        SPFGlobals.__gIs64 = None
        SPFGlobals.__gLocalDir = None
        SPFGlobals.__gMacroFile = None
        SPFGlobals.__gMyEXEDir = None
        SPFGlobals.__gMyAbort = False
        SPFGlobals.__gMyLocal = None
        SPFGlobals.__gMySPFGWDT = None
        SPFGlobals.__gMySPFJobDay = None
        SPFGlobals.__gMySPFJobDT = None

        SPFGlobals.__gR_VERSION = None
        SPFGlobals.__gRPathVersion = None
        SPFGlobals.__gSPFImmediateMacroFile = None
        SPFGlobals.__gSPFSQLFileName = None
        SPFGlobals.__gSPFSQLFileNameOnly = None
        SPFGlobals.__gSPFSQLFilePath = None
        SPFGlobals.__gSPFSQLFile_NamePart = None
        SPFGlobals.__gSPFSQLFile_ExtPart = None
        SPFGlobals.__gSPFSQLFileData = None
        SPFGlobals.__gg_ChartCtr = 0
        SPFGlobals.__logger.info("{0} - re Initializing properties : Done".format(calling_func))
    #status : done
    #END : def __reInitStaticPropsDueToCmdUpdate
    
    def GetIni2(self, MyLocal, INIFileName, INI_SectionName, INI_SectionSettingName, CFParserStrictMode=True, CFParserReadRaw=False, CFReadDelimiters=('=', ':')):
        """
        '=========================
        'Retrive the setting name from INI file
        '
        '(input) :
        '------
        '1.) MyLocal -- Build ExePath based on SH & this value
        '2.) INIFileName (StringType) : INI file name without path info
        '3.) INI_SectionName (StringType) : Section name
        '4.) INI_SectionSettingName (StringType) : Setting name 
        '5.) CFParserStrictMode (StringType) : config parser read mode if True error on duplicates. if False ignore duplicates (get the last item)
        '6.) CFParserReadRaw (StringType) : config parser % character handling. if True parse % else ignore %
        '7.) CFReadDelimiters (tuple(string)) : config parser read method 'delimeters' 
        '
        '(output) :
        '------
        '1.) INI_SectionSettingValue (StringType) : Setting value 
        '   if setting not found then None is returned
        '
        '=========================
        """
        calling_func = self.getCallingFuncName()
        #locals
        ExePath = None
        INI_SectionSettingValue = None
        INIFilePath = None

        self.__logger.debug("{0} - MyLocal : {1}".format(calling_func, MyLocal))
        self.__logger.debug("{0} - INIFileName : {1}".format(calling_func, INIFileName))
        self.__logger.debug("{0} - INI_SectionName : {1}".format(calling_func,INI_SectionName))
        self.__logger.debug("{0} - INI_SectionSettingName : {1}".format(calling_func, INI_SectionSettingName))
        self.__logger.debug("{0} - CFParserStrictMode : {1}".format(calling_func, CFParserStrictMode))
        self.__logger.debug("{0} - CFParserReadRaw : {1}".format(calling_func, CFParserReadRaw))
        self.__logger.debug("{0} - CFReadDelimiters : {1}".format(calling_func, CFReadDelimiters))
                
        try :
            #determine ExePath
            if self.SHisSHEntry is True : #'Script Host
                ExePath = self.gTempDir
            elif MyLocal == "N" :
                ExePath = self.gSPFLib
            else : #'local dir
                ExePath = MyLocal.strip()
            self.__logger.debug("{0} - ExePath : {1}".format(calling_func, ExePath))
            INIFilePath = os.path.join(ExePath, INIFileName)

            #open the INI file to read
            INI_SectionSettingValue = self.GetIni(INIFilePath, INI_SectionName, INI_SectionSettingName, CFParserStrictMode, CFParserReadRaw)
            return INI_SectionSettingValue
        except Exception as err:
            self.__logger.exception("{0} - Error occured while reading the INI file : {1}\n{2}".format(calling_func, INIFilePath, err))
            raise
    #END : def GetIni2

    def GetIni(self, INIFilePath, INI_SectionName, INI_SectionSettingName, CFParserStrictMode=True, CFParserReadRaw=False, CFReadDelimiters=('=', ':')):
        """
        '=========================
        'Retrive the setting name from INI file
        '
        '(input) :
        '------
        '1.) INIFilePath (StringType) : INI file name with path
        '2.) INI_SectionName (StringType) : Section name
        '3.) INI_SectionSettingName (StringType) : Setting name 
        '4.) CFParserStrictMode (StringType) : config parser read mode if True error on duplicates. if False ignore duplicates (get the last item)
        '5.) CFParserReadRaw (StringType) : config parser % character handling. if True parse % else ignore % 
        '6.) CFReadDelimiters (tuple(string)) : config parser read method 'delimeters'
        '
        '(output) :
        '------
        '1.) INI_SectionSettingValue (StringType) : Setting value 
        '   if setting not found then None is returned
        '
        '=========================
        """
        calling_func = self.getCallingFuncName()
        #locals
        INI_SectionSettingValue = None
        INI_Config_Obj = None

        self.__logger.debug("{0} - INIFilePath : {1}".format(calling_func, INIFilePath))
        self.__logger.debug("{0} - INI_SectionName : {1}".format(calling_func,INI_SectionName))
        self.__logger.debug("{0} - INI_SectionSettingName : {1}".format(calling_func, INI_SectionSettingName))
        self.__logger.debug("{0} - CFParserStrictMode : {1}".format(calling_func, CFParserStrictMode))
        self.__logger.debug("{0} - CFParserReadRaw : {1}".format(calling_func, CFParserReadRaw))
        self.__logger.debug("{0} - CFReadDelimiters : {1}".format(calling_func, CFReadDelimiters))

        #open the INI file to read
        try :
            INI_Config_Obj = ConfigParser.ConfigParser(strict=CFParserStrictMode, delimiters=CFReadDelimiters)
            INI_Config_Obj.read(INIFilePath)
        except Exception as err:
            self.__logger.exception("{0} - Error occured while reading the INI file : {1}\n{2}".format(calling_func, INIFilePath, err))
            raise

        #now get the setting value
        try :
            INI_SectionSettingValue = INI_Config_Obj.get(INI_SectionName, INI_SectionSettingName, raw=CFParserReadRaw)
            self.__logger.debug("{0} - INI_SectionSettingValue : {1}".format(calling_func, INI_SectionSettingValue))

            # got the value return it
            return INI_SectionSettingValue
        except Exception as err:
            self.__logger.exception("{0} - Error occured while getting INI section : '{1}' ; setting : '{2}' \n{3}".format(calling_func, INI_SectionName, INI_SectionSettingName, err))
            return INI_SectionSettingValue
        finally :
            #clean up
            del INI_Config_Obj    
    #END : def GetIni

    def GetSPFFunctionIni(self, INI_SectionName, INI_SectionSettingName):
        r"""
        '=========================
        'Retrive the setting name from 'schema\spf_functions.ini' file
        '
        '(input) :
        '------
        '1.) INI_SectionName (StringType) : Section name
        '2.) INI_SectionSettingName (StringType) : Setting name 
        '
        '(output) :
        '------
        '1.) INI_SectionSettingValue (StringType) : Setting value 
        '   if setting not found then None is returned
        '2.) INI_FuncParamCount (int) : count of parameters e.g., count distinct of (<1>, <2>).
        '3.) INI_FuncParams (set) : set(sorted) of params of the func e.g., (<1>, <2>)
        '=========================
        """
        calling_func = self.getCallingFuncName()
        #region locals
        if self.SHisSHEntry is True:
            INIFileName = r'spf_functions.ini'
        else:
            INIFileName = r'schema\spf_functions.ini'
        INI_SectionSettingValue = None
        myFnParamsRePtrn = r'\<\d{1,2}\>'
        #endregion locals

        self.__logger.debug("{0} - INIFileName : {1}".format(calling_func, INIFileName))
        self.__logger.debug("{0} - INI_SectionName : {1}".format(calling_func,INI_SectionName))
        self.__logger.debug("{0} - INI_SectionSettingName : {1}".format(calling_func, INI_SectionSettingName))
        
        try:
            
            #open the INI file to read
            INI_SectionSettingValue = self.GetIni2(self.gMyLocal, INIFileName, INI_SectionName, INI_SectionSettingName, CFParserStrictMode=False, CFParserReadRaw=True, CFReadDelimiters=('='))
            if INI_SectionSettingValue is None:
                errMsg = f"function mapping not found for : '{INI_SectionName}'; '{INI_SectionSettingName}'"
                raise Exception(errMsg)
            INI_FuncParams = sorted(set(re.findall(myFnParamsRePtrn, INI_SectionSettingValue,re.IGNORECASE)))
            INI_FuncParamCount = len(INI_FuncParams)
            self.__logger.debug("{0} - INI_FuncParamCount : {1}".format(calling_func, INI_FuncParamCount))
            return (INI_SectionSettingValue, INI_FuncParamCount, INI_FuncParams)
        except Exception as err:
            self.__logger.exception("{0} - Error occured while reading the INI file : {1}\n{2}".format(calling_func, INIFileName, err))
            raise
    #END def GetSPFFunctionIni

    def getCallingFuncName(self, levelValue=-3,clsName=None, logStack=False) : 
        calling_func = ""
        try : 
            if logStack == True : self.__logger.info(inspect.stack())
            calling_func = inspect.stack()[levelValue][3]
            if clsName is not None :
                calling_func = "({0}) - {1}".format(clsName, calling_func)
        except BaseException as err :
            self.__logger.exception(err)
            pass

        return calling_func
    #END : def getCallingFuncName

    def parseCMDArgsToken(self, cmdArgs, tokenName, defaultTokenValue=None,
                          doUCase=False, doTrim=True,
                          errIfTokenNotFound=False, errMsgIfTokenNotFound=""):
        """
        Helper function to parse SPF command line token/values
        E.g., /sdb="AT_LocationLookup.sdb" /out="AT_LocationLookup.sdbschema$" /LIMIT="Y" /TABLE="LoCATioNLookUp"
        INPUT : 
        -------
        1. cmdArgs : Token/Value pair string E.g., '/sdb="AT_LocationLookup.sdb" /out="AT_LocationLookup.sdbschema$" /LIMIT="Y" /TABLE="LoCATioNLookUp"'
        2. tokenName : Token to parse E.g., '/sdb='
        3. defaultTokenValue : default value if token is not found
        4. doUCase : flag (default = False) to indicate to return final value in upper case if doUCase == True
        5. doTrim : flag (default = True) to indicate to strip spaces around value if doTrim == True
        6. errIfTokenNotFound : Flag == True/False, default = False : if True error if Token is not found
        7. errMsgIfTokenNotFound : Error message to raise if Token is not found (and errIfTokenNotFound == True)
        OUTPUT : 
        --------
        1. tokenValue : parsed token value
        """
        calling_func = self.getCallingFuncName()
        #locals

        self.__logger.debug("{0} - cmdArgs : {1}".format(calling_func, cmdArgs))
        self.__logger.debug("{0} - tokenName : {1}".format(calling_func, tokenName))
        self.__logger.debug("{0} - defaultTokenValue : {1}".format(calling_func, defaultTokenValue))
        try:
            myTokenValList = [argvItem.strip().split(self.__CMD_ARGS_TKN_NM_VAL_SEP, 1)[-1].strip(' "')
                               for argvItem in cmdArgs
                               if tokenName.upper() in argvItem.upper() 
                             ]

            self.__logger.debug("{0} - myTokenValList : {1}".format(calling_func, myTokenValList))
            if len(myTokenValList) > 0:
                myTokenValue = myTokenValList[0]
                if doUCase is True:
                    myTokenValue = myTokenValue.upper()
                if doTrim is True:
                    myTokenValue = myTokenValue.strip(' ')
            else:
                if errIfTokenNotFound is True:
                    raise Exception(errMsgIfTokenNotFound)
                myTokenValue = defaultTokenValue
            self.__logger.debug("{0} - {1} {2}".format(calling_func, tokenName, myTokenValue))
            return myTokenValue
        except Exception as err :
            self.__logger.exception("{0} - Error occured while parsing cmdArgs : '{1}' ; for Token : '{2}' ".format(calling_func, cmdArgs, tokenName))
            raise

    def gLogRecordExistsInTracker(self, logRecord=None, logToLocalLog=False):
        """
        Tracker of Log records. Ensure records are unique
        """
        calling_func = self.getCallingFuncName()
        if logToLocalLog is True:
            self.__logger.debug("{0} - logRecord : {1}".format(calling_func, logRecord))
            self.__logger.debug("{0} - logToLocalLog : {1}".format(calling_func, logToLocalLog))
        #locals
        gLogRecordExistsInTracker_output = False
        try:
            if not logRecord is None:
                if logRecord in SPFGlobals.gLogTracker:
                    gLogRecordExistsInTracker_output = True #exists
                else: #doesnt exist, so add to tracker & return False
                    SPFGlobals.gLogTracker.append(logRecord)
                    gLogRecordExistsInTracker_output = False
        except Exception as err:
            self.__logger.exception("{0} - Error while trying to read gLogTracker : {1}\n...conitinue execution".format(calling_func, err))
            pass

        if logToLocalLog is True:
            self.__logger.debug("{0} - gLogTracker : {1}".format(calling_func, SPFGlobals.gLogTracker))
        self.__logger.debug("{0} -For {1} --> gLogRecordExistsInTracker_output : {2}".format(calling_func, logRecord, gLogRecordExistsInTracker_output))
        return gLogRecordExistsInTracker_output

    def detectCharacterEncoding(self, textContent: str, default_encoding: str = None, use_utf_8_sig:bool = False) -> str:
        """
        determing the character encoding of the 'textContent' string
        Input:
        1. textContent : string content whose encoding needs to be determines
        2. default_encoding : default = None : if not None use the value directly
        3. use_utf_8_sig : if True - use 'utf-8-sig' for ASCII, CP1252 charsets
        """
        #region -- locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - default_encoding: {1}".format(calling_func, default_encoding))

        chardetDetectOutput = None
        #endregion -- locals

        if default_encoding is not None:
            #return the default_encoding without trying to determine from content
            self.logger.debug("{0} - returning default_encoding : {1}".format(calling_func, default_encoding))
            return default_encoding

        # continue to determine encoding from content
        chardetDetectOutput = chardet.detect(textContent)
        self.logger.debug("{0} - chardetDetectOutput: {1}".format(calling_func, chardetDetectOutput))
        tmpencoding = chardetDetectOutput['encoding']
            
        if (tmpencoding is None or tmpencoding.upper() in ['ASCII', 'CP1252'] and use_utf_8_sig == True): #, 'GB2312'
            tmpencoding = self.gOSDefaultEncoding #use default

        self.logger.debug("{0} - encoding: {1}".format(calling_func, tmpencoding))
        return tmpencoding
    #END :  def detectCharacterEncoding

    def detectFileEncoding(self, fileIn : str, readall: bool=False, default_encoding: str = None, mode: str=None, use_utf_8_sig:bool = False):
        """
        Determing the file encoding by reading starting bits of the file & using chardet module
        ' INPUT ARGS :
        '-------------
        ' 1.) fileIn : str : filename which needs to be scanned to determine encoding
        ' 2.) readall : if True read all content of file else read specified chunk
        ' 3.) default_encoding : If not None ...return this value
        ' 4.) mode : if == 'w' (write) then check if 'gOSDefaultEncoding' is to be used 
        'OUTPUT ARGS: 
        '--------
        'fileInEncoding : str : encoding as detected by chardet module 
        """
        #region -- locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - fileIn: {1}".format(calling_func, fileIn))
        self.logger.debug("{0} - readall: {1}".format(calling_func, readall))
        self.logger.debug("{0} - default_encoding: {1}".format(calling_func, default_encoding))
        self.logger.debug("{0} - mode: {1}".format(calling_func, mode))
        chardetDetectOutput = None
        fileContent = None
        chunkSizeToRead = 10000
        #endregion -- locals

        if default_encoding is not None:
            #return the default_encoding without trying to determine from content
            self.logger.debug("{0} - returning default_encoding : {1}".format(calling_func, default_encoding))
            return default_encoding
    
        #update based on command line argument
        readall = self.gENCODING_FULLFILE_SCAN
        self.logger.debug("{0} - updated readall: {1}".format(calling_func, readall))
        try:
            with open(fileIn, 'rb') as fileToCheck: #reading in binary mode no need to handle Python 3.x encoding
                if readall is False:
                    self.logger.debug("{0} - chunkSizeToRead: {1}".format(calling_func, chunkSizeToRead))
                    fileContent = fileToCheck.read(chunkSizeToRead)
                    chardetDetectOutput = chardet.detect(fileContent) #self.detectCharacterEncoding(fileContent)
                else:
                    #to handle large file 
                    chardetDetctor = UniversalDetector()
                    for line in fileToCheck.readlines():
                        chardetDetctor.feed(line)
                    chardetDetctor.close()
                    self.logger.debug("{0} - chardetDetctor.result: {1}".format(calling_func, chardetDetctor.result))
                    chardetDetectOutput = chardetDetctor.result

            myEncoding = chardetDetectOutput['encoding']
            if (myEncoding is None or myEncoding.upper() in ['ASCII', 'CP1252']) and (mode in [None, "w", "W"]) or use_utf_8_sig is True: #, 'ISO-8859-1', 'GB2312'
                myEncoding = self.gOSDefaultEncoding #use defualt

            self.logger.debug("{0} - myEncoding: {1}".format(calling_func, myEncoding))
            return myEncoding
            
        except Exception as err:
            self.logger.exception("{0} -{1}".format(calling_func, err))
            raise
    #END : def detectFileEncoding
    
    def detectFileEncoding_cnm(self, fileIn, readall=False, default_encoding=None, mode=None):
        """
        alternate implementation of determining charset using 'charset_normalizer' module
        """
        #region -- locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - fileIn: {1}".format(calling_func, fileIn))
        self.logger.debug("{0} - readall: {1}".format(calling_func, readall))
        self.logger.debug("{0} - default_encoding: {1}".format(calling_func, default_encoding))
        self.logger.debug("{0} - mode: {1}".format(calling_func, mode))
        #endregion -- locals

        import charset_normalizer
        _preemptive_behaviour = True if readall is False else False
        _CharsetMatches = charset_normalizer.from_path(fileIn, preemptive_behaviour=_preemptive_behaviour, explain=False)
        tmpencoding = _CharsetMatches.best().encoding
        del _CharsetMatches # no longer needed...
        self.logger.debug("{0} - cnm_myEncoding: {1}".format(calling_func, tmpencoding))
        return tmpencoding
    #END: detectFileEncoding_cnm

    def detectFinalEncodingForDictOfFiles(self, files_dict_w_encoding: dict, readll: bool=True) -> str:
        """
        determine final encoding for dict of files in 'files_dict_w_encoding' {file_name : file_encoding}
        """
        #region -- locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        
        final_encoding = None
        #endregion -- locals
        len_files_dict_w_encoding = len(files_dict_w_encoding)

        if len_files_dict_w_encoding == 0:
            raise Exception(f"Invalid files info list provided for determining encoding")
        elif len_files_dict_w_encoding == 1:
            files_encoding = list(files_dict_w_encoding.values())[0]    
            self.logger.debug(f"{calling_func} - final_encoding (single file): {files_encoding}")
            return files_encoding

        # len_files_dict_w_encoding > 1...continue 
        # get the encodings of all the files
        files_encoding = set(files_dict_w_encoding.values())
        self.logger.debug(f"{calling_func} - files_encoding: {files_encoding}")

        if len(files_encoding) == 1:
            self.logger.debug(f"{calling_func} - final_encoding (all files of same encoding): {files_encoding}")
            return list(files_encoding)[0] # all files have same encoding...return this as final encoding

        # files are of different encoding...find out the final supported encoding
        multi_detector = UniversalDetector()
        for fileItem in files_dict_w_encoding.keys():
            with open(fileItem, 'rb') as rdr:
                multi_detector.reset()
                for line in rdr:
                    multi_detector.feed(line)
        multi_detector.close()
        self.logger.debug("{0} - multi_detector.result: {1}".format(calling_func, multi_detector.result))
        chardetDetectOutput = multi_detector.result
        final_encoding = chardetDetectOutput['encoding']
        # final_encoding = detect_multiple_files_encoding(files_dict_w_encoding)
        self.logger.debug(f"{calling_func} - final_encoding: {final_encoding}")
        return final_encoding
    #END : def detectFinalEncodingForDictOfFiles

    def initializeSPFBin(self):
        """
        Initializes the 'SPFBin' environment
        """
        #region -- locals
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        SPFBin_ZipFileName = 'SPFBin.zip'
        SPFBin_FolderName = 'SPFBin'

        SPFBin_ZipFilePath_Source = None
        SPFBin_ZipFilePath_Destination = None
        SPFBin_Folder_location = None #location where the 'SPFBin' should be...without the 'SPFBin' in the path value
        SPFBin_FolderPath_Destination = None #include 'SPFBin' in the path value
        errMsg_SPFBin_ZipNotFound = f"{SPFBin_ZipFileName} not found"
        errMsg_SPFBin_FolderNotFound = f'{SPFBin_FolderName} not found'
        #endregion -- locals

        if self.SHisSHEntry is True:
            SPFBin_ZipFilePath_Source = os.path.join(self.gSPFExe, SPFBin_ZipFileName)
            SPFBin_ZipFilePath_Destination = os.path.join(self.SHtempFldr, SPFBin_ZipFileName)
            SPFBin_Folder_location = self.SHtempFldr

            if (os.path.exists(SPFBin_ZipFilePath_Source) is False):
                self.__logger.debug(f"{calling_func} - File Not found : {SPFBin_ZipFilePath_Source}")
                SPFBin_ZipFilePath_Source = os.path.join(self.gSPFLib, 'library', SPFBin_ZipFileName)
                if (os.path.exists(SPFBin_ZipFilePath_Source) is False):
                    self.__logger.debug(f"{calling_func} - File Not found : {SPFBin_ZipFilePath_Source}")
                    raise Exception(errMsg_SPFBin_ZipNotFound)
        elif self.gMyLocal == "N":
            SPFBin_ZipFilePath_Source = os.path.join(self.gSPFLib, 'library', SPFBin_ZipFileName)
            SPFBin_ZipFilePath_Destination = os.path.join(os.getcwd(), SPFBin_ZipFileName)
            SPFBin_Folder_location = os.getcwd()

            if (os.path.exists(SPFBin_ZipFilePath_Source) is False):
                self.__logger.debug(f"{calling_func} - File Not found : {SPFBin_ZipFilePath_Source}")
                raise Exception(errMsg_SPFBin_ZipNotFound)
        else:
            SPFBin_Folder_location = self.gMyLocal
        #END : if

        SPFBin_FolderPath_Destination = os.path.join(SPFBin_Folder_location, SPFBin_FolderName)

        if (os.path.exists(SPFBin_FolderPath_Destination) is False):
            #SPFBin folder not found
            if SPFBin_Folder_location == self.gMyLocal:
                #running in interactive mode...SPFBin folder should exist
                self.__logger.debug(f"{calling_func} - '{SPFBin_FolderName}' folder Not found : {SPFBin_FolderPath_Destination}")
                raise Exception(errMsg_SPFBin_FolderNotFound)
            if (os.path.exists(SPFBin_ZipFilePath_Destination) is False):
                #SPFBin.zip not found copy over the file
                self.SPFCopy(SPFBin_ZipFilePath_Source, SPFBin_ZipFilePath_Destination, emitConsoleMessages=False)
            if (os.path.exists(SPFBin_ZipFilePath_Destination) is True):
                #unzip SPFBin.zip
                self.UnzipFile(SPFBin_ZipFilePath_Destination, self.SHtempFldr, True)
        
        #All set...update the sys.path
        sys.path.insert(0, SPFBin_FolderPath_Destination)
        self.__logger.debug(f"{calling_func} - {SPFBin_FolderName} path set to : {SPFBin_FolderPath_Destination}")
    #END : def initializeSPFBin

    def strip_connection(self, conn):
        """
        ###########################
        #Strips connection string
        #
        #ARGS:
        #----
        # conn : Connection STring
        ###########################
        """
        # if conn is not None:
        #     pos = conn.upper().find("@SQL7@")
        #     if pos == -1:
        #         pos=conn.upper().find("@MONGO@")
        #         if pos == -1:
        #             pos=conn.upper().find("@MYSQL@")
        #             if pos != -1:
        #                 conn=conn[0:pos+1]
        #         else:
        #             conn=conn[0:pos+1]
        #     else:
        #         conn=conn[0:pos+1]
        if conn is not None:
            ptrn = "@(SQL7|MONGO|SAPHANAODBC|MYSQL)@.*"
            conn = re.sub(ptrn, "", conn, 0, re.IGNORECASE)
        return conn;
    #END : def strip_connection
#END : class SPFGlobals
    

if __name__ == "__main__" :
    print("Running as main...nothing to execute")