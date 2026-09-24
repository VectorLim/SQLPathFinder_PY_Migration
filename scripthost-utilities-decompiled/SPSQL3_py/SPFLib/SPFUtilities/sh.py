"""
License : Copyright (c) Intel Corporation 2017
Product: Intel.ATTD.Auto.SQLPathFinder
Module : SQLPathFinder Python Extract Engine 
Author : vishwas.Nataraj@intel.com;SQLPathFinder_Support@intel.com
File Version : 2.0.0.0
History: 
1.0.0.0 : vanatara : Initial Version
2.0.0.0 : vanatara : Updated to support both Python 2.7.15 & Python 3.6
2.0.0.1 : vanatara : removed redundant import statements
"""
from SPFLib import * #isPYTHON2 #defined in SPFLib\__init__.py. IF True then 'Pyhton 2' IF False 'Python 3'
#if isPYTHON2:
#    import _winreg
#    from _winreg import *
#    import ConfigParser as ConfigParser
#else: 
#    #sys._enablelegacywindowsfsencoding()
#    import winreg
#    from winreg import *
#    import configparser as ConfigParser

"""
Decription : This py script provides class object which exposes properties for ScriptHost environment
History :
WW          Author  version     Description
WW07'15     Nataraj 1.0.0.0     Initial Release
WW07'15     Nataraj 1.0.0.1     1.) Removed inheritence of SPFLibBC as it results in a cyclic reference in some scenarios. 
                                2.) removed logger object
                                3.) removed dependency on win32, win32com & pythoncom modules, as these modules are not packaged in interactive mode py installation (will need to revisit in future if needed in interactive mode)
 
"""
class ScriptHost(object):
    """
    Methods from ScriptHost.va
    """

    __CLASS_UserMain="{768E4AF3-1136-48B8-B957-BD798CA9F3F9}"
    __CLASS_PingMain="{DFB25043-5028-4051-A078-853EB2A6532E}"

    __server = None
    __entryID = None
    __isSHEntry = None
    __jobName = None
    __scriptFile = None
    __NearestNASAnalysis = None
    __tempFldr = None
    __jobDir = None
    __userArea = None
    __queueAttributes = None
    __logFile = None
    __logFileNamePart = None
    __spfLogFile = None
    __initialized = False

    def __init__(self):
        
        #get required env variables
        if not ScriptHost.__initialized:
            ScriptHost.__server = os.getenv("SHServer")

            ScriptHost.__entryID = os.getenv("SHEntry")
            ScriptHost.__NearestNASAnalysis = os.getenv("NearestNASAnalysis")
            

            #continue to get other env variables
            ScriptHost.__jobName = os.getenv("SHJob")
            ScriptHost.__scriptFile = os.getenv("SHScript")
            ScriptHost.__tempFldr = os.getenv("temp")
            ScriptHost.__jobDir = os.getenv("SHJobDir")
            ScriptHost.__userArea = os.getenv("SHUserArea")
            ScriptHost.__queueAttributes = os.getenv("SHQueueAttributes")
            ScriptHost.__logFile = os.getenv("SHLog")
            if ScriptHost.__logFile is not None :                
                ScriptHost.__logFileNamePart = os.path.splitext(os.path.split(ScriptHost.__logFile)[1])[0]
                ScriptHost.__spfLogFile = os.path.join(ScriptHost.__jobDir, "{0}.spf{1}".format(*os.path.splitext(ScriptHost.__logFile)))

            #print("SH Initialized")
            ScriptHost.__initialized = True

    @property
    def SHisSHEntry(self):
        #set prop SHisSHEntry 
        if ScriptHost.__isSHEntry is None :
            if self.SHserver is not None and self.SHentryID.isdigit():
                    ScriptHost.__isSHEntry = True
            else:
                ScriptHost.__isSHEntry = False

        return ScriptHost.__isSHEntry

    @property
    def SHserver(self):
        return ScriptHost.__server

    @property
    def SHentryID(self):
        return ScriptHost.__entryID

    @property
    def SHNearestNASAnalysis(self):
        return ScriptHost.__NearestNASAnalysis

    @property
    def SHtempFldr(self):
        return ScriptHost.__tempFldr

    @property
    def SHscriptFile(self):
        return ScriptHost.__scriptFile
        
    @property
    def SHjobName(self):
        return ScriptHost.__jobName

    @property
    def SHJobDir(self):
        return ScriptHost.__jobDir

    @property
    def SHUserArea(self):
        return ScriptHost.__userArea

    @property
    def SHQueueAttributes(self):
        return ScriptHost.__queueAttributes

    @property
    def SHLog(self):
        return ScriptHost.__logFile

    @property
    def SHSPFLog(self):
        return ScriptHost.__spfLogFile

    @property
    def SHLogNamePart(self):
        return ScriptHost.__logFileNamePart

    @property
    def SHalreadyRunning(self):
        errMsg = " not implemented"
        raise NotImplementedError(errMsg)

    @property
    def SHisLastJobEntry(self):
        errMsg = " not implemented"
        raise NotImplementedError(errMsg)

    #status : dev done, UT pending
    def SHPathForCB(self, release) :
        """
        ' get CB path on SH
        """
        #locals
        myPathForCB = None
        aReg = None
        aKey = None
        cbVersionsIni = ""

        try :
            try :
                try : 
                    aReg = ConnectRegistry(None, HKEY_LOCAL_MACHINE)
                    aKey = OpenKey(aReg, r"Software\ACTools\ScriptHost\CrystalBall")
                except Exception as err:
                    errMsg = "Error '{0}' occurred while looking for Crystal Ball in registry.".format(err.strerror)
                    raise Exception(errMsg)

                try :
                    pathKey = "BaseInstallPath"
                    cbVersionsIni = ExpandEnvironmentStrings(QueryValueEx(aKey, pathKey))
                except Exception as err:
                    errMsg = "Error '{0}' occurred while looking for Key '{1}' in registry.".format(err.strerror, pathKey)
                    raise Exception(errMsg)
            except Exception as err:
                pass #just pass...it errors out in SH...looks like it is an env variable

            if len(cbVersionsIni) > 0 :
                cbVersionsIni = os.path.join(cbVersionsIni, "CBVersions.ini")
            else :
                myPathForCB = os.getenv("CB")
                return myPathForCB
            
            INI_Config_Obj = ConfigParser.ConfigParser()
            INI_Config_Obj.read(cbVersionsIni)

            cbVersion = INI_Config_Obj.get("Releases", release)

            if cbVersion is None :
                cbVersion = release

            myPathForCB = os.getenv(cbVersion)
            if myPathForCB is None:
                myPathForCB = os.getenv("CB")

            return myPathForCB
        except Exception as err:
            raise

if __name__ == '__main__':
    print("Running as main")

    def __testSHIsSHEntry():
        print("testing : SHIsSHEntry")
        tmpSH = ScriptHost()
        print(tmpSH.SHisSHEntry)
        print("SHserver : " + str(tmpSH.SHserver))
        print("SHentryID : " + str(tmpSH.SHentryID))
        print("SHNearestNASAnalysis : " + str(tmpSH.SHNearestNASAnalysis))
        print("SHjobName : " + str(tmpSH.SHjobName))



    def __testSHserver():
        print("testing : SHserver")
        tmpSH = ScriptHost()
        print(tmpSH.SHserver)

    def __testSHentryID():
        print("testing : SHentryID")
        tmpSH = ScriptHost()
        print(tmpSH.SHentryID)

    def __testSHNearestNASAnalysis():
        print("testing : SHNearestNASAnalysis")
        tmpSH = ScriptHost()
        print(tmpSH.SHNearestNASAnalysis)

    def __testSHjobName():
        print("testing : SHjobName")
        tmpSH = ScriptHost()
        print(tmpSH.SHjobName)


    __testSHIsSHEntry()
    __testSHserver()
    __testSHentryID()
    __testSHNearestNASAnalysis()
    __testSHjobName()