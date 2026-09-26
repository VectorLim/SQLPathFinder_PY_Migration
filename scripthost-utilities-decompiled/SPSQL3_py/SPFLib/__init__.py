"""Shared imports for the decompiled SQLPathFinder ScriptHost library.

Session 2.5A portability note:
The historical module eagerly imported every Windows/COM/.NET dependency and then
imported SPFUtilities back from this package.  That made otherwise portable
ScriptHost algorithms impossible to import on Linux.  Portable dependencies stay
eager; platform/optional dependencies are best-effort and target methods remain
responsible for rejecting unsupported Windows-only execution paths.
"""
import sys
import os
import warnings

isPYTHON2 = False
isPYTHON313 = sys.version_info.major == 3 and sys.version_info.minor >= 13
if sys.platform == "win32" and hasattr(sys, "_enablelegacywindowsfsencoding"):
    sys._enablelegacywindowsfsencoding()

import gc, re, csv, collections, itertools, io, subprocess, zlib, zipfile
from os.path import expanduser
from itertools import islice
import inspect, traceback, locale, binascii, ast, codecs
import shutil, copy, filecmp, difflib, html, functools, string, base64
import glob
import datetime, dateutil, time, dateutil.tz
from datetime import datetime, timedelta
import datetime as dt
from io import BytesIO
import json
import math, random, numbers, types
from decimal import Decimal
from types import *
import pandas as pd, numpy as np
from xml.sax.saxutils import escape
from operator import itemgetter
from xml.dom.minidom import Document as xmlMiniDomDocument
from urllib.parse import urlparse, urlsplit
import pathlib
from pathlib import Path
import importlib
import configparser as ConfigParser
import io as StringIO
from io import StringIO
import urllib as urllib2

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
    duckdb = pyarrow = pyarrowds = None

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

# Deliberately do not import SPFUtilities here.  The historical eager back-import
# created a package cycle and initialized the entire host merely to reach helpers.
