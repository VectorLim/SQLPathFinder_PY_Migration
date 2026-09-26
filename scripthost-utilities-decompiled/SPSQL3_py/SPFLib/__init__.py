"""
License : Copyright (c) Intel Corporation 2017
Product: Intel.ATTD.Auto.SQLPathFinder
Module : SQLPathFinder Python Extract Engine

Linux portability note (SQLPathFinder migration Session 2.5A):
This package initializer intentionally exposes the portable common imports used by
legacy ScriptHost modules without eagerly importing Win32/COM/.NET infrastructure
or the giant SPFUtilities module. Windows-only dependencies remain available on
Windows, while Linux-safe modules can now be imported independently.
"""
import sys
import os
import warnings

isPYTHON2 = False
isPYTHON313 = sys.version_info.major == 3 and sys.version_info.minor >= 13
WINDOWS_RUNTIME_AVAILABLE = sys.platform == "win32"

if WINDOWS_RUNTIME_AVAILABLE and hasattr(sys, "_enablelegacywindowsfsencoding"):
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
import pandas as pd
import numpy as np
from xml.sax.saxutils import escape
from operator import itemgetter
from xml.dom.minidom import Document as xmlMiniDomDocument
from urllib.parse import urlparse, urlsplit
import pathlib
from pathlib import Path
import importlib
import requests
from requests.exceptions import HTTPError
import urllib3
from urllib3.exceptions import InsecureRequestWarning

urllib3.disable_warnings(category=InsecureRequestWarning)

try:
    import chardet
    from chardet.universaldetector import UniversalDetector
except ImportError:
    chardet = None
    UniversalDetector = None

try:
    import tabulate as tblate
except ImportError:
    tblate = None
else:
    tblate.PRESERVE_WHITESPACE = True

try:
    import pymongo
    from bson.son import SON
except ImportError:
    pymongo = None
    SON = None

try:
    import duckdb
    import pyarrow
    from pyarrow import dataset as pyarrowds
except ImportError:
    duckdb = None
    pyarrow = None
    pyarrowds = None

try:
    from requests_kerberos import HTTPKerberosAuth
except ImportError:
    HTTPKerberosAuth = None

# Python 3 aliases retained for legacy modules that import common names from SPFLib.
import configparser as ConfigParser
import io as StringIO_module
from io import StringIO
import urllib as urllib2


def require_windows_runtime(feature: str = "this ScriptHost operation") -> None:
    """Raise a clear error when a Windows-only legacy path is invoked on Linux."""
    if not WINDOWS_RUNTIME_AVAILABLE:
        raise RuntimeError(
            f"{feature} requires the legacy Windows ScriptHost runtime and is unavailable on "
            f"{sys.platform}."
        )


if WINDOWS_RUNTIME_AVAILABLE:
    import win32security, win32net, win32com, win32com.client, pythoncom, pywintypes
    from win32com.client import Dispatch
    import win32api, win32con
    import clr

    clr.AddReference("System")
    clr.AddReference("System.Web")
    clr.AddReference("System.Security")
    clr.AddReference("System.Security.Principal")
    clr.AddReference("System.DirectoryServices")
    clr.AddReference("System.DirectoryServices.AccountManagement")
    clr.AddReference("System.ServiceModel")
    clr.AddReference("System.Collections")

    import System.Security.Principal
    import System.DirectoryServices
    import System.DirectoryServices.ActiveDirectory
    import System.Security.Cryptography
    from System.Security.Principal import WindowsIdentity
    from System.Collections.Generic import List

    import winreg
    from winreg import *
