# [Removed from current version] """
# [Removed from current version] License : Copyright (c) Intel Corporation 2017
# [Removed from current version] Product: Intel.ATTD.Auto.SQLPathFinder
# [Removed from current version] Module : SQLPathFinder Python Extract Engine
# [Removed from current version] Author : vishwas.Nataraj@intel.com;SQLPathFinder_Support@intel.com
# [Removed from current version] File Version : 2.0.1.0
# [Removed from current version] History:
# [Removed from current version] 1.0.0.0 : vanatara : Initial Version
# [Removed from current version] 2.0.0.0 : vanatara : Updated to support both Python 2.7.15 & Python 3.6
# [Removed from current version] 2.0.0.1 : vanatara : Updated to import common modules used across the SPFLib module.
# [Removed from current version] 2.0.0.2 : vanatara : add pathlib import
# [Removed from current version] 2.0.0.3 : vanatara : add clr:System.Security.Cryptography
# [Removed from current version] 2.0.0.4 : vanatara : add clr: libraries needed for SQLPFaaS
# [Removed from current version] 2.0.0.5 : vanatara : add imp, duckdb, pyarrow modules
# [Removed from current version] 2.0.0.6 : jmclarke : Added error trap when importing duckdb for legacy reasons
# [Removed from current version] 2.0.0.7 : jmclarke : Added pyarrow to error trap for legacy reasons
# [Removed from current version] 2.0.0.8 : vanatara : add requests, urlib3. remove Py2 related imports
# [Removed from current version] 2.0.0.9 : vanatara : Added additional modules needed for parallelization support for InTemp/InGroup
# [Removed from current version] 2.0.1.0 : vanatara : Support Python 3.13
"""Shared imports for the decompiled SQLPathFinder ScriptHost library.

Session 2.5A portability note:
The historical module eagerly imported every Windows/COM/.NET dependency and then
imported SPFUtilities back from this package.  That made otherwise portable
ScriptHost algorithms impossible to import on Linux.  Portable dependencies stay
eager; platform/optional dependencies are best-effort and target methods remain
responsible for rejecting unsupported Windows-only execution paths.
"""
import sys
# [Removed from current version] import os, warnings
# [Removed from current version] # warnings.simplefilter("always")
# [Removed from current version] # os.environ["PYTHONWARNINGS"] = "default"
import os
import warnings

# [Removed from current version] isPYTHON2 = False #default is Python 3. Python 2 is EOL'ed
# [Removed from current version] isPYTHON313 = False
# [Removed from current version] # print(sys.version_info)
# [Removed from current version] if sys.version_info.major == 3:
    # [Removed from current version] sys._enablelegacywindowsfsencoding() # this is required to overcome encoding issues across the module. See Python help page for more info
    # [Removed from current version] if sys.version_info.minor >= 13:
        # [Removed from current version] isPYTHON313 = True
isPYTHON2 = False
isPYTHON313 = sys.version_info.major == 3 and sys.version_info.minor >= 13
if sys.platform == "win32" and hasattr(sys, "_enablelegacywindowsfsencoding"):
    sys._enablelegacywindowsfsencoding()

import gc, re, csv, collections, itertools, io, subprocess, zlib, zipfile
from os.path import expanduser
from itertools import islice

import inspect, traceback, locale, binascii, ast, codecs
import shutil, copy, filecmp, difflib, html, functools, string, base64
# [Removed from current version] import glob #used in AppendFileTask, Final_CleanUp, Delete_Tables, Get_Img_Dir

# [Removed from current version] import datetime, dateutil, time, dateutil.tz
import glob
import datetime, dateutil, time, dateutil.tz
from datetime import datetime, timedelta
# [Removed from current version] import datetime as dt #this alias is used in nqMongoTask flow #V1.0.3.8_VA30_100

import datetime as dt
from io import BytesIO
# [Removed from current version] from bson.son import SON
# [Removed from current version] import json   #used in ijs_Gen_Grid

import json
import math, random, numbers, types
from decimal import Decimal
from types import *
import pandas as pd, numpy as np
# [Removed from current version] import pymongo
# [Removed from current version] import win32security, win32net, win32com, win32com.client, pythoncom, pywintypes
# [Removed from current version] #from winreg import *
# [Removed from current version] from win32com.client import Dispatch #used in JMP/JSL task handlers
# [Removed from current version] import win32api, win32con # used in SetFileROTask, FileIsLocked, etc
# [Removed from current version] import chardet
# [Removed from current version] from chardet.universaldetector import UniversalDetector
from xml.sax.saxutils import escape
# [Removed from current version] import tabulate as tblate
# [Removed from current version] tblate.PRESERVE_WHITESPACE = True
from operator import itemgetter
from xml.dom.minidom import Document as xmlMiniDomDocument
from urllib.parse import urlparse, urlsplit
import pathlib
from pathlib import Path
# [Removed from current version] # import imp #depricated since Py 3.4 refer to : https://docs.python.org/3.11/library/imp.html
# [Removed from current version] import importlib #Py 3.13 support
# [Removed from current version] # import importlib.util
# [Removed from current version] # import importlib.machinery
# [Removed from current version] import requests
# [Removed from current version] from requests.exceptions import HTTPError
# [Removed from current version] import urllib3
# [Removed from current version] from urllib3.exceptions import InsecureRequestWarning
import importlib
import configparser as ConfigParser
import io as StringIO
from io import StringIO
import urllib as urllib2

# [Removed from current version] urllib3.disable_warnings(category=InsecureRequestWarning)
# [Removed from current version] from requests.exceptions import HTTPError
# [Removed from current version] from requests_kerberos import HTTPKerberosAuth

# [Removed from current version] #import duckdb
# Optional portable dependencies.  Absence must not block unrelated algorithms.
try:
    from bson.son import SON
except ImportError:
    SON = None
try:
    import pymongo
except ImportError:
    pymongo = None
try:
    import chardet
    from chardet.universaldetector import UniversalDetector
except ImportError:
    chardet = None
    UniversalDetector = None
try:
    import tabulate as tblate
    tblate.PRESERVE_WHITESPACE = True
except ImportError:
    tblate = None
try:
    import requests
    from requests.exceptions import HTTPError
except ImportError:
    requests = None
    HTTPError = None
try:
    import urllib3
    from urllib3.exceptions import InsecureRequestWarning
    urllib3.disable_warnings(category=InsecureRequestWarning)
except ImportError:
    urllib3 = None
try:
    from requests_kerberos import HTTPKerberosAuth
except ImportError:
    HTTPKerberosAuth = None
try:
    import duckdb
    import pyarrow
    from pyarrow import dataset as pyarrowds
except ImportError:
    # [Removed from current version] pass
    duckdb = pyarrow = pyarrowds = None

# [Removed from current version] import clr
# [Removed from current version] clr.AddReference('System')
# [Removed from current version] clr.AddReference('System.Web')
# [Removed from current version] clr.AddReference('System.Security')
# [Removed from current version] clr.AddReference('System.Security.Principal')
# [Removed from current version] clr.AddReference('System.DirectoryServices')
# [Removed from current version] clr.AddReference('System.DirectoryServices.AccountManagement')
# [Removed from current version] clr.AddReference('System.ServiceModel')
# [Removed from current version] clr.AddReference('System.Collections')
# [Removed from current version] import System.Security.Principal, System.DirectoryServices, System.DirectoryServices.ActiveDirectory, System.Security.Cryptography
# [Removed from current version] from System.Security.Principal import WindowsIdentity
# [Removed from current version] from System.Collections.Generic import List
# Windows-only host integrations are intentionally optional at module import.
# Methods that actually require them must fail explicitly when invoked.
if sys.platform == "win32":
    try:
        import win32security, win32net, win32com, win32com.client, pythoncom, pywintypes
        from win32com.client import Dispatch
        import win32api, win32con
        import winreg
        from winreg import *
    except ImportError:
        win32security = win32net = win32com = pythoncom = pywintypes = None
        Dispatch = win32api = win32con = winreg = None
    try:
        import clr
        clr.AddReference("System")
        clr.AddReference("System.Web")
        clr.AddReference("System.Security")
        clr.AddReference("System.Security.Principal")
        clr.AddReference("System.DirectoryServices")
        clr.AddReference("System.DirectoryServices.AccountManagement")
        clr.AddReference("System.ServiceModel")
        clr.AddReference("System.Collections")
        import System.Security.Principal, System.DirectoryServices
        import System.DirectoryServices.ActiveDirectory, System.Security.Cryptography
        from System.Security.Principal import WindowsIdentity
        from System.Collections.Generic import List
    except (ImportError, RuntimeError):
        clr = WindowsIdentity = List = None
else:
    win32security = win32net = win32com = pythoncom = pywintypes = None
    Dispatch = win32api = win32con = winreg = None
    clr = WindowsIdentity = List = None

# [Removed from current version] #python 3
# [Removed from current version] import configparser as ConfigParser
# [Removed from current version] import io as StringIO
# [Removed from current version] from io import StringIO
# [Removed from current version] import winreg
# [Removed from current version] from winreg import *
# [Removed from current version] import urllib as urllib2
# [Removed from current version] from . import SPFUtilities
# [Removed from current version] from .SPFUtilities.utils import Utilities, SPFCMDRunExitWithErrorCodeException, SPFMutedException
# [Removed from current version] #EOF# Deliberately do not import SPFUtilities here.  The historical eager back-import
# created a package cycle and initialized the entire host merely to reach helpers.
