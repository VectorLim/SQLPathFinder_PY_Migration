"""
License : Copyright (c) Intel Corporation 2022
Product: Intel.ATTD.Auto.SQLPathFinder
Module : SQLPathFinder Python Extract Engine 
Author : jolyon.m.clarke@intel.com;SQLPathFinder_Support@intel.com
History: (Update the 'Version' value)
1.0.0.0 - 07/03/2020 - JC - Initial version
2.0.0.0 - 03/17/2022 - VN - Support SQLPFaaS added Added additional clr references
3.0.0.0 - 06/14/2022 - JC - Added support for ReadPreference
4.0.0.0 - 07/15/2025 - VN - Added support for Python 3.13 & oracledb module (replaces cx_oracle)
4.0.0.0a - 11/12/2025 - VN - Oracle driver fix for Py3.11 backward compatibility
"""
isPYTHON313 = False
import sys
if sys.version_info.major == 3 and sys.version_info.minor >= 13:
    isPYTHON313 = True

import winreg
import os
import datetime as dt
import pymongo
from pymongo import ReadPreference
import numpy as np
import pandas as pd
import clr
clr.AddReference('System')
clr.AddReference('System.Web')
clr.AddReference('System.Security')
clr.AddReference('System.Security.Principal')
clr.AddReference('System.DirectoryServices')
clr.AddReference('System.DirectoryServices.AccountManagement')
clr.AddReference('System.ServiceModel')
import System.Security.Principal
import System.DirectoryServices
import System.DirectoryServices.ActiveDirectory
from System.Security.Principal import WindowsIdentity 
import win32api
import win32con
import re
import logging
import logging.config
from logging.handlers import RotatingFileHandler
import pathlib
from pathlib import Path


if isPYTHON313 is True:
    # _t = os.path.join('C:\\Users\\vanatara\\My Programs\\SQLPathFinder3', r"Oracle\instantclient_19_17")
    import oracledb as dbDriver
    # dbDriver.init_oracle_client(lib_dir = _t)
else:
    import cx_Oracle as dbDriver
    import cx_Oracle #keeping backward compatibility with Py3.11
    