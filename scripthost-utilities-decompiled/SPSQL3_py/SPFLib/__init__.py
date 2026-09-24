"""
License : Copyright (c) Intel Corporation 2017
Product: Intel.ATTD.Auto.SQLPathFinder
Module : SQLPathFinder Python Extract Engine 
Author : vishwas.Nataraj@intel.com;SQLPathFinder_Support@intel.com
File Version : 2.0.1.0
History: 
1.0.0.0 : vanatara : Initial Version
2.0.0.0 : vanatara : Updated to support both Python 2.7.15 & Python 3.6
2.0.0.1 : vanatara : Updated to import common modules used across the SPFLib module.
2.0.0.2 : vanatara : add pathlib import
2.0.0.3 : vanatara : add clr:System.Security.Cryptography
2.0.0.4 : vanatara : add clr: libraries needed for SQLPFaaS
2.0.0.5 : vanatara : add imp, duckdb, pyarrow modules 
2.0.0.6 : jmclarke : Added error trap when importing duckdb for legacy reasons 
2.0.0.7 : jmclarke : Added pyarrow to error trap for legacy reasons 
2.0.0.8 : vanatara : add requests, urlib3. remove Py2 related imports 
2.0.0.9 : vanatara : Added additional modules needed for parallelization support for InTemp/InGroup
2.0.1.0 : vanatara : Support Python 3.13
"""
import sys
import os, warnings
# warnings.simplefilter("always")
# os.environ["PYTHONWARNINGS"] = "default"

isPYTHON2 = False #default is Python 3. Python 2 is EOL'ed
isPYTHON313 = False
# print(sys.version_info)
if sys.version_info.major == 3:
    sys._enablelegacywindowsfsencoding() # this is required to overcome encoding issues across the module. See Python help page for more info
    if sys.version_info.minor >= 13:
        isPYTHON313 = True

import gc, re, csv, collections, itertools, io, subprocess, zlib, zipfile
from os.path import expanduser
from itertools import islice

import inspect, traceback, locale, binascii, ast, codecs
import shutil, copy, filecmp, difflib, html, functools, string, base64
import glob #used in AppendFileTask, Final_CleanUp, Delete_Tables, Get_Img_Dir

import datetime, dateutil, time, dateutil.tz 
from datetime import datetime, timedelta
import datetime as dt #this alias is used in nqMongoTask flow #V1.0.3.8_VA30_100

from io import BytesIO
from bson.son import SON
import json   #used in ijs_Gen_Grid

import math, random, numbers, types
from decimal import Decimal
from types import *
import pandas as pd, numpy as np
import pymongo
import win32security, win32net, win32com, win32com.client, pythoncom, pywintypes
#from winreg import *
from win32com.client import Dispatch #used in JMP/JSL task handlers
import win32api, win32con # used in SetFileROTask, FileIsLocked, etc
import chardet
from chardet.universaldetector import UniversalDetector
from xml.sax.saxutils import escape
import tabulate as tblate
tblate.PRESERVE_WHITESPACE = True
from operator import itemgetter
from xml.dom.minidom import Document as xmlMiniDomDocument
from urllib.parse import urlparse, urlsplit
import pathlib
from pathlib import Path
# import imp #depricated since Py 3.4 refer to : https://docs.python.org/3.11/library/imp.html
import importlib #Py 3.13 support
# import importlib.util
# import importlib.machinery
import requests
from requests.exceptions import HTTPError
import urllib3
from urllib3.exceptions import InsecureRequestWarning

urllib3.disable_warnings(category=InsecureRequestWarning)
from requests.exceptions import HTTPError
from requests_kerberos import HTTPKerberosAuth

#import duckdb
try:
    import duckdb
    import pyarrow
    from pyarrow import dataset as pyarrowds
except ImportError:
    pass

import clr
clr.AddReference('System')
clr.AddReference('System.Web')
clr.AddReference('System.Security')
clr.AddReference('System.Security.Principal')
clr.AddReference('System.DirectoryServices')
clr.AddReference('System.DirectoryServices.AccountManagement')
clr.AddReference('System.ServiceModel')
clr.AddReference('System.Collections')
import System.Security.Principal, System.DirectoryServices, System.DirectoryServices.ActiveDirectory, System.Security.Cryptography
from System.Security.Principal import WindowsIdentity 
from System.Collections.Generic import List

#python 3
import configparser as ConfigParser
import io as StringIO
from io import StringIO
import winreg
from winreg import *
import urllib as urllib2
from . import SPFUtilities
from .SPFUtilities.utils import Utilities, SPFCMDRunExitWithErrorCodeException, SPFMutedException 
#EOF