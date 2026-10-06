"""Portable overrides for the archived nqOracleTask class."""

import SPFLib.SPFSQL3 as legacy


class nqOracleTask(legacy.nqOracleTask):
    """Extend the original implementation with portable methods."""

    def OpenConnection(self, uni, pwi, nodei):
        calling_func = self.getCallingFuncName(2, self.__class__.__name__)
        dbDriverMap = {"VA" : "dbDriverCxOracle",
                       "SQLPLUS" : "dbDriverCxOracle",
                       "VA (.NET)" : "DotNetOracleDriver"}
        #Note: above mapping is only for Oracle handler
        dbDriver = None
        try :

            dbDriverMapped = dbDriverMap[self.SQLEngine]
            self.logger.debug(f"{calling_func} - dbDriverMapped : {dbDriverMapped}")
            #get from modules attribute -- expects the class to be in current module
            # [Removed from current version] dbDriverClsObject = getattr(sys.modules[__name__], dbDriverMapped)
            dbDriverClsObject = getattr(legacy.sys.modules[legacy.__name__], dbDriverMapped, None)
            forcePortable = legacy.os.getenv("SCRIPTHOST_FORCE_PORTABLE_QUERY_TRANSPORT") == "1"
            usePortable = forcePortable or dbDriverClsObject is None
            if usePortable:
                # The original parser/query-task lifecycle remains authoritative.
                # Explicit opt-in also permits portable validation on Windows.
                from scripthost_portable.query_transport import PortableOracleConnection
                dbDriverClsObject = PortableOracleConnection
                self.logger.info("Oracle query transport: PortableOracleConnection (forced=%s)", forcePortable)
            #create the instance
            dbDriver = dbDriverClsObject(queryOptions=self.queryOptions)
            if usePortable:
                dbDriver.load_to_memtable = self.gLoadToMemTable

        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            errMsg = "No valid dbDrivers found for SQLEngine : {0}".format(self.SQLEngine)
            raise Exception(errMsg)

        #open the connection and return the connection object
        dbDriver.openConnection(uni, pwi, nodei, self.ll_ConnRetry)
        return dbDriver
