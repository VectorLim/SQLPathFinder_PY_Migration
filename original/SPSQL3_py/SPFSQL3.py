"""
License : Copyright (c) Intel Corporation 2017
Product: Intel.ATTD.Auto.SQLPathFinder
Module : SQLPathFinder Python Extract Engine 
Author : vishwas.Nataraj@intel.com;SQLPathFinder_Support@intel.com
File Version : 1.1.0.1
History: 
1.0.0.0 : Nataraj : Initial Version
1.1.0.0 : Nataraj : Updated to set exit code
1.1.0.1 : Nataraj : Updated to check & reset command window title
"""
import sys
import SPFLib
from SPFLib.SPFSQL3 import SPFManager
from SPFLib.SPFUtilities.utils import Utilities
import win32console
#locals
cmdTitle = "SQLPathfinder - SQL Engine"

#get the command window title
try:
    cmdTitle = win32console.GetConsoleTitle()
except Exception as err:
    pass

runStatus = 1
try : 
    mySPFManager = SPFManager()
    runStatus = mySPFManager.main(sys.argv)
except Exception as err: 
    print(err)
finally:
    if win32console.GetConsoleTitle() != cmdTitle:
        win32console.SetConsoleTitle(cmdTitle)
    sys.exit(runStatus)