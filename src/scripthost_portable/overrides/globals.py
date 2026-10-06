"""Portable overrides for the archived SPFGlobals class."""

import SPFLib.SPFGlobals as legacy


class SPFGlobals(legacy.SPFGlobals):
    """Extend the original implementation with portable methods."""

    @property
    def gIsSvc(self)-> bool:
        """
        get: gIsSvc --> True if PyEE is running as Web service
        """
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        if legacy.SPFGlobals.__gIsSvc is None:
            # [Removed from current version] curHttpContext = System.Web.HttpContext.Current
            # [Removed from current version] curSvcSecContext = System.ServiceModel.ServiceSecurityContext.Current
            # [Removed from current version] if [curHttpContext, curSvcSecContext] == [None, None]:
                # [Removed from current version] SPFGlobals.__gIsSvc = False #Http & Service contexts are empty -- PyEE is not running as Web service
            if legacy.sys.platform != "win32" or "System" not in vars(legacy):
                legacy.SPFGlobals.__gIsSvc = False
            else:
                # [Removed from current version] SPFGlobals.__gIsSvc = True #either http or Service context exists - PyEE is running as Web service
            # [Removed from current version] self.logger.debug("{0} - set : {1}; {2}".format(calling_func, SPFGlobals.__gIsSvc, [curHttpContext, curSvcSecContext]))
                curHttpContext = legacy.System.Web.HttpContext.Current
                curSvcSecContext = legacy.System.ServiceModel.ServiceSecurityContext.Current
                if [curHttpContext, curSvcSecContext] == [None, None]:
                    legacy.SPFGlobals.__gIsSvc = False #Http & Service contexts are empty -- PyEE is not running as Web service
                else:
                    legacy.SPFGlobals.__gIsSvc = True #either http or Service context exists - PyEE is running as Web service
                self.logger.debug("{0} - set : {1}; {2}".format(calling_func, legacy.SPFGlobals.__gIsSvc, [curHttpContext, curSvcSecContext]))
        return legacy.SPFGlobals.__gIsSvc

    @property
    def gLocalDir(self):
        """
        #get/set: Curr dir
        """
        calling_func = self.getCallingFuncName()
        if legacy.SPFGlobals.__gLocalDir is None :
            # [Removed from current version] SPFGlobals.__gLocalDir = os.path.abspath(os.path.curdir) + "\\"
            legacy.SPFGlobals.__gLocalDir = legacy.os.path.abspath(legacy.os.path.curdir) + legacy.os.sep

        self.__logger.info("{0} - {1}".format(calling_func, legacy.SPFGlobals.__gLocalDir))
        return legacy.SPFGlobals.__gLocalDir
