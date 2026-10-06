"""Portable overrides for the archived NormalQueryTaskBase class."""

import SPFLib.SPFSQL3 as legacy


class NormalQueryTaskBase(legacy.NormalQueryTaskBase):
    """Extend the original implementation with portable methods."""

    def Prep_Inc_Process(self, TmpFile,TmpInc,TmpNode,Sitei,OleDBOpt,SQLEngine, Inc1Only) :
        """
        '=======================================
        'Prepare Get_CSV args for Incremental
        'processing
        '
        'ARGS:
        '----
        'TmpFile : CSV File. May end in ->n with n being incremental count (e.g., "a0_123.tab->50")
        'TmpInc  : Will store the Incremental count, or -1 if no incremental process requested
        'TmpNode : Stores True or False (Y|Y2 or N) to denote whether node mapping is requested
        'Sitei   : Node. Special case of Node = "map_nodes" may occur
        'OLEDBOpt: Stores SQLPlus for an Oracle SQLPlus driver, else...
        'SQLEngine: E.g., UBER
        'Inc1Only: True : If ok to do incremental query
        '
        ' RETURN:
        ' -------
        'TmpFile : CSV File. May end in ->n with n being incremental count (e.g., "a0_123.tab->50")
        'Inc1Only: True : If ok to do incremental query
        'TmpInc  : Will store the Incremental count, or -1 if no incremental process requested
        ' "" if successful, or error msg
        '=======================================
        """
        calling_func = self.getCallingFuncName(2,clsName = self.__class__.__name__)
        try :
            # SPFSQL files commonly spell relative paths as ".\\file". Preserve
            # that Windows meaning on portable hosts without changing CSV-list logic.
            if legacy.os.sep == "/" and TmpFile.startswith(".\\"):
                TmpFile = "./" + TmpFile[2:].replace("\\", "/")
            TmpInc= -1
            if TmpFile.find("->") > 0 :
                TmpFile, TmpInc = TmpFile.split("->",1)
                self.logger.debug("{0} - TmpFile : {1}".format(calling_func, TmpFile))
                self.logger.debug("{0} - TmpInc : {1}".format(calling_func, TmpInc))
                if TmpInc.isdigit() is True :
                    TmpInc = int(TmpInc)
                    if TmpInc in range(0, 1000) == False :
                        TmpInc = -1
                else :
                    TmpInc = -1

                if Inc1Only is True and TmpInc != -1 :
                    errMsg = "Error: You are only allowed to specify one incremental search per sub-query using\nfilters In Temp, Like Temp, Regex Temp, In Group and In Temp()"
                    raise Exception(errMsg)

            if TmpInc > -1 :
                Inc1Only = True

            #END : if TmpFile.find("->")
            if (self.OLEDBopt.upper() not in ["SQLPLUS", "ORACLE"]
               and self.SQLEngine[:4] != "UBER"
               and self.TmpNode is True):
                errMsg = "Error: Node Mapping (i.e., use of Node=Map_Nodes) \nare not supported for {0} ({1}) drivers.".format(self.OLEDBopt, self.SQLEngine)
                raise Exception(errMsg)

            if (self.OLEDBopt.upper() not in ["SQLPLUS", "ORACLE", "SQLSERVER", "MONGO"]
               and self.SQLEngine[:4] != "UBER"
               and (self.OLEDBopt.upper() + self.SQLEngine) != "TERADATAVA (.NET)"
               and TmpInc > 0):
                errMsg = "Error: Incremental In Temp queries\nare not supported for {0} ({1}) drivers.".format(self.OLEDBopt, self.SQLEngine)
                raise Exception(errMsg)
            self.logger.debug("{0} - Inc1Only: {1} -- TmpInc : {2}".format(calling_func, Inc1Only, TmpInc))
            return TmpFile, Inc1Only, TmpInc
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
