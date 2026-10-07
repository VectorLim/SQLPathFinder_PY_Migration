"""Portable overrides for the archived MemTable class."""

import SPFLib.SPFUtilities.memtable as legacy


class MemTable(legacy.MemTable):
    """Extend the original implementation with portable methods."""

    def Run_SQLite(self,
                    Site1 = None,
                    Command1 = None,
                    MyTables = None,
                    WorkDir = None,
                    OutTT = None,
                    OutFile = None,
                    OutExcel = None,
                    FinalRow = None,
                    ExPlans = None,
                    MyLocal = None,
                    MyText = None,
                    PreProcCSV = None,
                    MySQLite_DT = None,
                    MySQLiteCache = None,
                    ll_QuoteCSV = None,
                    MyEmptyNull = True,
                    ll_UniqueHdr = None,
                    l_DBName = None,
                    l_Null = None,
                    ll_SQLiteExt = None,
                    ll_NoHdrs = None,
                    ll_Schema = None,
                    MyInstance = None,
                    isJQW = None,
                    useSQLiteEXE = False,
                    RowIdStartAt2=False,
                    myAppend=False) :
        """
        '==========================================================
        'Run a SQLite Query
        '
        'ARGS:
        '====
        'Site1        : SQLite db to query or blank or ".\" to query Text Files
        'Command1     : Query to run
        'MyTables     : Comma delimited list of files to import & table name pairs. If no file path
        '               is specified for the importfile, it is assumed to be in the default working
        '               directory. The path & file to import & the SQLite table name are separated 
        '               by a semicolon. E.g., "Class_MARS_1.csv:Class_MARS_1". 
        '               Set to N/A if no files are to be imported
        'WorkDir      : Work folder
        'OutTT        : Y for console O/P
        'OutFile      : Txt O/P file
        'OutExcel     : CSV O/P file
        'FinalRow     : Rows Returned
        'ExPlans      : "" or Explain Plan Code 
        'MyLocal      : Exe dir, or N if remote query
        'MyText       : Display Text for query or QUIET for no display
        'PreProcCSV   : Whether delimited I/P files should be pre-processed in case of embedded delimiters in cols
        'MySQLite_DT  : List of SQLite cols & associated datatypes. E.g., lot (c), yield (f), NewQty1 (n), Out_Date (d)
        'MySQLiteCache: Y if CSV import Files are cached to a SQLite DB (unused)
        'll_QuoteCSV  : Whether to quote SQLite or Final Join O/P cols which contain embedded commas
        'MyEmptyNull  : If True (Y), convert empty strings to NULL - default = True if False calculations like AVG will go wrong
        'll_UniqueHdr : If Y, ensure column headers are unique
        'l_DBName     : SQLIte Database Name to load data to
        'l_Null       : Print value for NULL col. E.g., &nbsp; for HTML reports
        'll_SQLiteExt : Y to use extended SQLite Libraries
        'll_NoHdrs    : Do not print any col headers
        'll_Schema    : Y to Create the $db schema file for l_DBName
        'MyInstance   : Instance #
        'isJQW        : Handle invalid JQWidgets Column Names
        'useSQLiteEXE  : True/False : Flag to indicate which SQLite to use. True = use SQLite.exe, False = use inbuild module
        'RowIdStartAt2 : True/False : Flag to indicate if rowid should start at 2. Default : False --> rowid starts at 1, True --> rowid starts at 2
        'myAppend      : True/False : Flag to indicate if output file is overwrite(=False) or append(=True). From /APPENDMODE options
        '
        'GLOBALS:
        '------
        'gSQLiteVer: SQLite Exe
        'gLocalDir : Def Dir
        'gMyAbort  : Y to Abort Entire Job
        'gIs64     : 1=64 bit SQLite, 0=32 bit SQLite
        'gSHFolder : SH An Path
        'g_isSPFonSH: Is SPF SW on Drones
        '==========================================================
        """
        # VG2's historical current-directory spelling is not a POSIX folder.
        if legacy.os.name != "nt" and WorkDir == ".\\":
            WorkDir = "."

        #constants
        __SQLITE_DATA_PATH = "@SQLITE-SPF-DATA-PATH@"
        __SQLITE_DATA_PATH2 = "@SQLITE-SPF-DATA-PATH2@"
        #region'===v30.50  ===========
        #__l_SCHEMA_EXT = ".$db"
        #endregion '===v30.50  ===========
        __SPW = "30 " * 45

        #local variables
        calling_func = self.myUtils.getCallingFuncName(2)
        lSDPath = None
        AnyFile = None
        MyReturn = None
        IsQuiet = None
        Col0 = None
        IsLinux = None
        MyInstance2 = None

        #'=== SQLite64===
        MySQLiteExt = None
        MySExt = None
        #'=== SQLite64===

        D_SSMDF = None
        l_DBStem = None

        AnyFile = 1 #'OK

        SSEExe = MyCvtExe = None
        MyJob = MySSE = s1 = MySQL = SrcFile = ColNames = DLM00 = Line1 = None
        dts = acomma = MySQLCols = D_SSMDF = MySRCTxt = MySrcTxt2 = None
        Table1 = MySServer = None
        SQLTable = SrcTable = MyDelSQL = MyOutSQL = Curr_Char = NewSrcTable = None
        MyPair = MyData = MyPos = RNStr = DelCSVLst = DoContinue = MyNullSQL = None
        DLM01 = l_DBSchema = l_DBStem = l_DBAux = l_DBFile =MySQExt = ExePath2 = None
        AttachSQL = None
        MyNullSQLDict = None
        #AttachDBName = None
        FilesToImportDict = None

        try :
            self.logger.debug("{0} - Site1 = {1}".format(calling_func, Site1))
            self.logger.debug("{0} - Command1 = {1}".format(calling_func, Command1))
            self.logger.debug("{0} - MyTables = {1}".format(calling_func, MyTables))
            self.logger.debug("{0} - WorkDir = {1}".format(calling_func, WorkDir))
            self.logger.debug("{0} - OutTT = {1}".format(calling_func, OutTT))
            self.logger.debug("{0} - OutFile = {1}".format(calling_func, OutFile))
            self.logger.debug("{0} - OutExcel = {1}".format(calling_func, OutExcel))
            self.logger.debug("{0} - FinalRow = {1}".format(calling_func, FinalRow))
            self.logger.debug("{0} - ExPlans = {1}".format(calling_func, ExPlans))
            self.logger.debug("{0} - MyLocal = {1}".format(calling_func, MyLocal))
            self.logger.debug("{0} - MyText = {1}".format(calling_func, MyText))
            self.logger.debug("{0} - PreProcCSV = {1}".format(calling_func, PreProcCSV))
            self.logger.debug("{0} - MySQLite_DT = {1}".format(calling_func, MySQLite_DT))
            self.logger.debug("{0} - MySQLiteCache = {1}".format(calling_func, MySQLiteCache))
            self.logger.debug("{0} - ll_QuoteCSV = {1}".format(calling_func, ll_QuoteCSV))
            self.logger.debug("{0} - MyEmptyNull = {1}".format(calling_func, MyEmptyNull))
            self.logger.debug("{0} - ll_UniqueHdr = {1}".format(calling_func, ll_UniqueHdr))
            self.logger.debug("{0} - l_DBName = {1}".format(calling_func, l_DBName))
            self.logger.debug("{0} - l_Null = {1}".format(calling_func, l_Null))
            self.logger.debug("{0} - ll_SQLiteExt = {1}".format(calling_func, ll_SQLiteExt))
            self.logger.debug("{0} - ll_NoHdrs = {1}".format(calling_func, ll_NoHdrs))
            self.logger.debug("{0} - ll_Schema = {1}".format(calling_func, ll_Schema))
            self.logger.debug("{0} - MyInstance = {1}".format(calling_func, MyInstance))
            self.logger.debug("{0} - isJQW = {1}".format(calling_func, isJQW))
            self.logger.debug("{0} - useSQLiteEXE = {1}".format(calling_func, useSQLiteEXE))
            self.logger.debug("{0} - RowIdStartAt2 = {1}".format(calling_func, RowIdStartAt2))
            self.logger.debug("{0} - myAppend = {1}".format(calling_func, myAppend))

            #initialize local variables
            IsQuiet = True if MyText == "QUIET" else False
            IsLinux = "False"
            DelCSVLst = [] #store to hold file names that need to be deleted
            self.logger.debug("{0} - IsQuiet = {1}".format(calling_func, IsQuiet))
            MySExt = "SQLiteExtFunc_v2.dll"
            MySQExt = self.myUtils.gSQLiteVer
            RNStr = self.myUtils.RandomNumStr
            SQLTable = "{0}.csv".format(RNStr)
            AttachSQL = []
            FilesToImportDict = {}
            """
            '=======
            'Chk Args
            '=======
            """
            if Site1 == ".\\" :
                Site1 = None # ("") in VA
                self.logger.debug("{0} - Site1: {1}".format(calling_func, Site1))
            elif Site1.upper().find(__SQLITE_DATA_PATH) != -1 :
                lSDPath = self.myUtils.gSPFLib
                self.logger.debug("{0} - lSDPath: {1}".format(calling_func, lSDPath))
                lSDPath = legacy.os.path.join(lSDPath, "Data\\")
                self.logger.debug("{0} - lSDPath updated: {1}".format(calling_func, lSDPath))
                Site1 = legacy.re.sub(__SQLITE_DATA_PATH, lambda x : lSDPath, Site1, 0,legacy.re.IGNORECASE)
                self.logger.debug("{0} - Site1 path: {1}".format(calling_func, Site1))
            elif Site1.upper().find(__SQLITE_DATA_PATH2) != -1 :
                lSDPath = self.myUtils.gSPFLib
                self.logger.debug("{0} - lSDPath: {1}".format(calling_func, lSDPath))
                lSDPath = legacy.os.path.join(lSDPath, "Data\\")
                self.logger.debug("{0} - lSDPath updated: {1}".format(calling_func, lSDPath))
                Site1 = legacy.re.sub(__SQLITE_DATA_PATH2, lambda x : lSDPath, Site1, 0,legacy.re.IGNORECASE)
                self.logger.debug("{0} - Site1 path2: {1}".format(calling_func, Site1))
            #END : if Site1 == ".\\"

            #Parse Command1 for query format that is input to SQLite3.exe -- so that it can be executed using inbuilt SQLite module
            #MyTables,
            self.logger.debug("{0} - MyTables: {1}".format(calling_func, MyTables))
            self.logger.debug("{0} - Site1: {1}".format(calling_func, Site1))
            if self.myUtils.IsEmptyOrNone(MyTables) is True and self.myUtils.IsEmptyOrNone(Site1) is True :
                errMsg = ("*********************************************************************************\n"
                          "* No Files to Import. Specify /TABLE=<CSVFile>:<Table> to show files to import  *\n"
                          "* and SQLite table to hold data. If no file path is specified, SQLPathfinder    *\n"
                          "* will look in the work folder. The file and table must be separated by colons  *\n"
                          "* and pairs delimited by commas. Phrase ':<TABLE>' is optional and, if omitted, *\n"
                          "* will equal the file name  (no ext.) with any spaces converted to underscores. *\n"
                          "*                                                                               *\n"
                          "* E.g., <OPTIONS>                                                               *\n"
                          "*       /TABLE=1.csv:T1,A2.csv                                                  *\n"
                          "*       </OPTIONS>                                                              *\n"
                          "*********************************************************************************")
                raise Exception(errMsg)

            if self.myUtils.IsEmptyOrNone(MyTables) is False:
                if MyTables.upper() == "N/A" :
                    MySQLiteCache="YT"
                self.logger.debug("{0} - MySQLiteCache: {1}".format(calling_func, MySQLiteCache))

            RNStr = self.myUtils.RandomNumStr
            SQLTable = "{0}.csv".format(RNStr)
            self.logger.debug("{0} - SQLTable: {1}".format(calling_func, SQLTable))

            if MyInstance is None :
                MyInstance = RNStr
            MyInstance2 = "{0}_{1}_{2}".format(legacy.os.getenv("USERNAME"), MyInstance, RNStr)
            self.logger.debug("{0} - MyInstance2: {1}".format(calling_func, MyInstance2))

            s1 = "{0}.sql".format(RNStr)
            self.logger.debug("{0} - s1: {1}".format(calling_func, s1))

            if self.myUtils.IsEmptyOrNone(Site1) is True : #'csv File processing
                if self.myUtils.IsEmptyOrNone(l_DBName) is False : #'Fixed DB Name passed
                    D_SSMDF = l_DBName.strip()
                    D_SSMDFFileName, l_DBStem = legacy.os.path.splitext(D_SSMDF.lower())
                    self.logger.debug("{0} - D_SSMDFFileName : {1}; l_DBStem {2}".format(calling_func, D_SSMDFFileName, l_DBStem))

                    if l_DBStem.lower() != ".sdb" :
                        errMsg = ("*********************************************************************************\n"
                                  "* You specified the following SQLite db:{0}\n"
                                  "* Sorry but SQLPathFinder SQLite databases must have an extension of .sdb\n"
                                  "*********************************************************************************"
                                  ).format(D_SSMDF)
                        raise Exception(errMsg)

                    l_DBStem = D_SSMDFFileName
                    self.logger.debug("{0} - l_DBStem : {1}".format(calling_func, l_DBStem))
                    del D_SSMDFFileName
                #END : if l_DBName is not None

                if self.myUtils.IsEmptyOrNone(D_SSMDF) is False :
                    D_SSMDF = legacy.os.path.abspath(D_SSMDF)
                    self.logger.debug("{0} - Abs path : D_SSMDF : {1}".format(calling_func, D_SSMDF))
                #AttachDBName = D_SSMDF
            else : #'Site1 <>"" - SQL DB passed
                MySQLiteCache = "Y"
                self.logger.debug("{0} - MySQLiteCache : {1}".format(calling_func, MySQLiteCache))
                if bool(legacy.re.search("NEW:",Site1, legacy.re.IGNORECASE)) == True and len(Site1.strip()) > 5 :
                    #'New & persist
                    D_SSMDF = Site1[5:]
                    self.logger.debug("{0} - NEW: found in Site1 : D_SSMDF : {1}".format(calling_func, D_SSMDF))
                elif bool(legacy.re.search(":::",Site1, legacy.re.IGNORECASE)) == True :
                    #'DB on an App Server
                    MySServer, D_SSMDF = Site1.split(":::")
                    self.logger.debug("{0} - MySServer : {1}; D_SSMDF : {2}".format(calling_func, MySServer, D_SSMDF))
                    D_SSMDFPath, D_SSMDFFileName = legacy.os.path.split(D_SSMDF)
                    self.logger.debug("{0} - D_SSMDFPath : {1}; D_SSMDFFileName : {2}".format(calling_func, D_SSMDFPath, D_SSMDFFileName))

                    if D_SSMDFPath[-1] == "/" :
                        IsLinux = True
                        AttachSQL.append("ATTACH DATABASE '{0}' AS spf99999;".format(D_SSMDF))
                        #AttachDBName = D_SSMDF
                        self.logger.debug("{0} - AttachSQL : {1}".format(calling_func, AttachSQL))
                        #self.logger.debug("{0} - AttachDBName : {1}".format(calling_func, AttachDBName))
                        D_SSMDF = D_SSMDFFileName.lower()
                        s1 = "{0}.sql".format(MyInstance2)
                        self.logger.debug("{0} - IsLinux : {1}; D_SSMDF : {2}; s1 : {3}".format(calling_func, IsLinux, D_SSMDF, s1))

                    del D_SSMDFPath
                    del D_SSMDFFileName
                else : #'Existing on Win Accessible share
                    D_SSMDF = Site1
                    if legacy.os.path.exists(D_SSMDF) == False :
                        errMsg = "SQLite Database not found:\n{0}".format(D_SSMDF)
                        raise Exception(errMsg)
                    AttachSQL.append("ATTACH DATABASE '{0}' AS spf99999;".format(D_SSMDF))
                    #AttachDBName = D_SSMDF
                    self.logger.debug("{0} - AttachSQL : {1}".format(calling_func, AttachSQL))
                    #self.logger.debug("{0} - AttachDBName : {1}".format(calling_func, AttachDBName))
                    D_SSMDF = None
            #END : if Site1 is None

            self.myUtils.DelAFile(SQLTable)

            """
            '=========
            'SQLite exe
            '=========
            """
            if self.myUtils.SHisSHEntry is True : #'SH
                SSEExe = legacy.os.path.join(self.myUtils.gTempDir, MySQExt)
                MyCvtExe = legacy.os.path.join(self.myUtils.gTempDir, "CleanDelimsCRLF.exe")
                self.logger.debug("{0} - MyCvtExe : {1}".format(calling_func, MyCvtExe))
                ExePath2 = self.myUtils.gTempDir
            elif MyLocal == "N":
                SSEExe = legacy.os.path.join(self.myUtils.gSPFLib, "SQLite\\{0}".format(MySQExt))
                SSEExeTo = legacy.os.path.join(WorkDir, "{0}".format(MySQExt))
                self.logger.debug("{0} - SSEExe : {1}".format(calling_func, SSEExe))
                self.logger.debug("{0} - SSEExeTo : {1}".format(calling_func, SSEExeTo))
                SSEExe = self.myUtils.DoCopySPFLib(SSEExe, SSEExeTo)
                self.logger.debug("{0} - SSEExe : {1}".format(calling_func, SSEExe))
                del SSEExeTo
                #'=== SQLite64===
                MySQLiteExt = legacy.os.path.join(self.myUtils.gSPFLib, "SQLite\\{0}".format(MySExt))
                MySQLiteExtTo = legacy.os.path.join(WorkDir, "{0}".format(MySExt))
                self.logger.debug("{0} - MySQLiteExt : {1}".format(calling_func, MySQLiteExt))
                self.logger.debug("{0} - MySQLiteExtTo : {1}".format(calling_func, MySQLiteExtTo))
                MySQLiteExt = self.myUtils.DoCopySPFLib(MySQLiteExt, MySQLiteExtTo)
                self.logger.debug("{0} - MySQLiteExt : {1}".format(calling_func, MySQLiteExt))
                del MySQLiteExtTo
                #'=== SQLite64===

                MyCvtExe = legacy.os.path.join(self.myUtils.gSPFLib, "SQLite\\CleanDelimsCRLF.exe")
                MyCvtExeTo = legacy.os.path.join(WorkDir, "CleanDelimsCRLF.exe")
                self.logger.debug("{0} - MyCvtExe : {1}".format(calling_func, MyCvtExe))
                self.logger.debug("{0} - MyCvtExeTo : {1}".format(calling_func, MyCvtExeTo))
                MyCvtExe = self.myUtils.DoCopySPFLib(MyCvtExe, MyCvtExeTo)
                self.logger.debug("{0} - MyCvtExe : {1}".format(calling_func, MyCvtExe))
                del MyCvtExeTo

                ExePath2 = self.myUtils.gSPFLib
            else : #'local
                SSEExe = legacy.os.path.join(MyLocal, MySQExt)
                self.logger.debug("{0} - SSEExe : {1}".format(calling_func, SSEExe))
                MyCvtExe = legacy.os.path.join(MyLocal, "CleanDelimsCRLF.exe")
                self.logger.debug("{0} - MyCvtExe : {1}".format(calling_func, MyCvtExe))
                ExePath2 = MyLocal
            #END : if self.myUtils.SHisSHEntry == True :
            """
            '============
            'Create Script -- 
            '============
            """
            MySQL = [] # keep appending SQL lines into this list...in end join all
            MySQL.append(".echo OFF")
            if ll_NoHdrs == True :
                MySQL.append(".headers OFF")
            else :
                MySQL.append(".headers ON")

            if MySQLiteCache not in ["Y", "YT"] :
                if MyTables is not None :
                    AnyFile = 0  #'Files expected for IMPORT. Make sure 1+ file is imported
                    self.logger.debug("{0} - AnyFile: {1}".format(calling_func, AnyFile))

                MyPair = MyTables.split(",") #'Extract File & Tbl Name Pairs
                self.logger.debug("{0} - MyPair: {1}".format(calling_func, MyPair))
                # [Removed from current version] uniqueMyPair = list(set(item.upper() for item in MyPair)) #'Ignore this Table <--- this logic is not needed in PyEE as this step will get Unique items
                uniqueMyPair = list(set(item.upper() for item in MyPair)) if legacy.os.name == "nt" else list(dict.fromkeys(MyPair)) #'Ignore this Table <--- this logic is not needed in PyEE as this step will get Unique items
                self.logger.debug("{0} - uniqueMyPair: {1}".format(calling_func, uniqueMyPair))
                uniqueMyPairCounter = 0
                while len(uniqueMyPair) > 0 : #'For Each Text File
                    uniqueMyPairCounter = uniqueMyPairCounter + 1
                    MyData = uniqueMyPair.pop(0).strip()
                    self.logger.debug("{0} - MyData: {1}".format(calling_func, MyData))
                    """
                    '=====================
                    'Chk that file & table 
                    'not already processed
                    '=====================
                    """
                    if len(MyData) != 0 :
                        SrcFilePath, SrcFileName = legacy.os.path.split(MyData)
                        self.logger.debug("{0} - SrcFilePath: {1}".format(calling_func, SrcFilePath))
                        self.logger.debug("{0} - SrcFileName: {1}".format(calling_func, SrcFileName))
                        SrcFile = SrcFileName
                        SrcTable = ""
                        #'=== v30.104 ===== <--- is not needed in PyEE as the filePath and fileName are split above
                        if SrcFileName.find(":") > -1 :
                            SrcFile, SrcTable = legacy.re.split(r"\s?:\s?", SrcFileName) #SrcFileName.partition(" : ")[::2]
                        self.logger.debug("{0} - SrcFile: {1}".format(calling_func, SrcFile))
                        self.logger.debug("{0} - SrcTable: {1}".format(calling_func, SrcTable))

                        SrcFileNamePart, SrcFileNameExtPart = legacy.os.path.splitext(SrcFileName)
                        self.logger.debug("{0} - SrcFileNamePart: {1}".format(calling_func, SrcFileNamePart))
                        self.logger.debug("{0} - SrcFileNameExtPart: {1}".format(calling_func, SrcFileNameExtPart))

                        if self.myUtils.IsEmptyOrNone(SrcFilePath.strip()) is True :
                            MySrcTxt = legacy.os.path.join(WorkDir, SrcFile)
                        else : #has path info use it
                            MySrcTxt = legacy.os.path.join(SrcFilePath, SrcFile)
                        MySrcTxt = legacy.os.path.abspath(MySrcTxt)
                        #region '===v30.50  ===========
                        #if SrcFileNameExtPart.lower() != ".$db" :
                        #endregion '===v30.50  =========== Note if you uncomment then indent below if-else block
                        """
                        '===== v30.71 ==========
                        '===========================
                        'Chk for commas in file name
                        '===========================
                        """
                        MySrcTxt = legacy.re.sub(r"<c>", ",", MySrcTxt, 0, legacy.re.IGNORECASE) #MySrcTxt.replace("<c>",",")
                        #'=======================

                        """
                        '==============
                        'Chk File Exists
                        '==============
                        """
                        if legacy.os.path.exists(MySrcTxt) == False :
                            errMsg = "Could not locate File :{0}. File import will be skipped ...".format(MySrcTxt)
                            self.myUtils.Console(errMsg)
                        else : #'CSV Found
                            """
                            '========================
                            'Get Tbl Name i& validate
                            '========================
                            """
                            AnyFile = 1 #'at least 1 file found
                            if len(SrcTable.strip()) == 0 :
                                SrcTable, SrcTableStem = legacy.os.path.splitext(SrcFileName)  #SrcFile.partition(".")[::2]
                                self.logger.debug("{0} - SrcTable: {1}".format(calling_func, SrcTable))
                                self.logger.debug("{0} - SrcTableStem: {1}".format(calling_func, SrcTableStem))
                            #END : if len(SrcTable.strip()) == 0 :
                            """       
                            '=====================
                            'Create Valid Tbl Name
                            '=====================
                            """
                            SrcTable = legacy.re.sub(r"[^\d\w__]", "_", SrcTable.strip(), legacy.re.IGNORECASE|legacy.re.VERBOSE)   #'Invalid Character. Replace with _
                            if SrcTable[0].isdigit() == True :
                                SrcTable = "T{0}".format(SrcTable)
                            self.logger.debug("{0} - after scrubbing SrcTable: {1}".format(calling_func, SrcTable))
                            DLM00 = self.myUtils.GetFileDLM(SrcFile)
                            """
                            '============================================
                            'Potentially pre-process dlm files as SQLite
                            'import will break on delimiters bounded by ""
                            '============================================
                            """
                            self.logger.debug("{0} - PreProcCSV: {1}".format(calling_func, PreProcCSV))
                            if PreProcCSV == True :
                                # [Removed from current version] MySrcTxt2 = os.path.join(".\\", "{0}_{1}.tmp".format(uniqueMyPairCounter, RNStr))
                                MySrcTxt2 = legacy.os.path.join("." if legacy.os.name != "nt" else ".\\", "{0}_{1}.tmp".format(uniqueMyPairCounter, RNStr))
                                self.logger.debug("{0} - MySrcTxt2: {1}".format(calling_func, MySrcTxt2))
                                self.myUtils.ConvertDLM(MySrcTxt, MySrcTxt2, MyCvtExe,IsQuiet)
                                MySrcTxt = MySrcTxt2
                                DelCSVLst.append(MySrcTxt)
                            #END : if PreProcCSV == True

                            #get colNames (headers)
                            try :
                                ColNames = self.myUtils.GetHeadersFromFile(MySrcTxt,DLM00)
                            except Exception as err:
                                errMsg = err.args[0]
                                if err.args[0] == "Empty File" :
                                    errMsg = "SQLite query will return zero rows as source file is empty. {0}".format(SrcFile)
                                raise Exception(errMsg)

                            self.logger.debug("{0} - ColNames: {1}".format(calling_func, ColNames))
                            """
                            '=====================================================
                            'Get DLM for parsing & Potentially make col hdrs unique
                            '=====================================================
                            """
                            if ll_UniqueHdr == True :
                                ColNames = self.myUtils.Make_Headers_Unique2(ColNames)
                                self.logger.debug("{0} - unique ColNames: {1}".format(calling_func, ColNames))
                            MySQLCols = [] #list of column names with DT
                            MyNullSQL = [] #list of update empty to null statements
                            MyNullSQLDict = {} #this dict will store default values for a column if it is Empty
                            """
                            '==================================
                            'If this is for JQWidgets, ensure
                            'any invalid col names end with $
                            '==================================
                            """
                            if self.myUtils.IsEmptyOrNone(isJQW) is False :
                                if isJQW.strip().upper() == "Y" :
                                    ColNames = list(map(self.myUtils.Test_Valid_JQW_Cols, ColNames))
                            #region '===v30.50  ===========
                            #if ll_Schema is True :
                            #    l_DBSchema = 'CSV,Columns,"row_id (n)",,"[rowid]","Unique Row Identifier. Increments from 1..n based on how the file is physically ordered","STD->TXT"'
                            #endregion '===v30.50  ===========
                            for colIdx, colItem in enumerate(ColNames) :
                                dts = "{0} NULL".format(self.myUtils.Get_SQLite_Hive_DT(MySQLite_DT, colItem, "S"))  #'dt
                                #self.logger.debug("{0} - colItem : {1} dts: {2}".format(calling_func, colItem, dts))
                                if colItem.strip() == "[]" :
                                    colItem = "SPF$UNK$99"
                                colItem = colItem.replace('"',"").replace("[", "(").replace("]", ")")

                                MySQLCols.append("[{0}] {1}".format(colItem, dts))

                                #region '===v30.50  ===========
                                #if (not l_DBName is None) and ll_Schema == True :
                                #    l_DBSchema = '{0}\n{1},Columns,"{2} (c)",C,"[{2}],"","STD->TXT"'.format(l_DBSchema, SrcTable, colItem)
                                #endregion '===v30.50  ===========
                                MyEmptyNull_val = ""
                                if MyEmptyNull is True :
                                    MyNullSQL.append("UPDATE [{0}] SET [{1}] = null where [{1}] = '';".format(SrcTable, colItem ))
                                    MyEmptyNull_val = None
                                MyNullSQLDict[legacy.re.sub(r"[^\w]", "_", colItem, legacy.re.IGNORECASE|legacy.re.VERBOSE)] = MyEmptyNull_val

                                if colIdx == 0 :
                                    MyDelSQL = "DELETE FROM [{0}] WHERE rowid = 1;  --Get Rid of Headers".format(SrcTable)

                            #END : for colItem in ColNames :
                            self.logger.debug("{0} - MySQLCols : {1}".format(calling_func, MySQLCols))
                            self.logger.debug("{0} - MyNullSQL : {1}".format(calling_func, MyNullSQL))
                            self.logger.debug("{0} - MyNullSQLDict : {1}".format(calling_func, MyNullSQLDict))
                            self.logger.debug("{0} - MyDelSQL : {1}".format(calling_func, MyDelSQL))

                            #region '===v30.50  ===========
                            #if not l_DBName is None and ll_Schema == True :
                            #    """
                            #    '==================
                            #    'Create Schema File
                            #    '==================
                            #    """
                            #    self.myUtils.DoCreateFileA("{0}.{1}{2}".format(l_DBStem,
                            #                                                    SrcTable,
                            #                                                    __l_SCHEMA_EXT),
                            #                                "!START\n{0}".format(l_DBSchema),
                            #                                "Run_SQLite")
                            ##END : if not l_DBName is None and ll_Schema == True
                            #endregion '===v30.50  ===========

                            """
                            '===========
                            'Add to Script
                            '===========
                            """
                            MySQL.append('.separator "{0}"'.format(DLM00))
                            if l_DBName is None :
                                MySQL.append("CREATE TABLE [{0}]".format(SrcTable))
                            else :
                                MySQL.append("DROP TABLE IF EXISTS [{0}]; CREATE TABLE IF NOT EXISTS [{0}]".format(SrcTable))

                            MySQL.append("({0}\n);".format("\n,".join(MySQLCols)))
                            MySQL.append("\n.import '{0}' {1}".format(MySrcTxt, SrcTable))
                            MySQL.append(MyDelSQL)

                            #update FilesToImportDict with details
                            FilesToImportDict[uniqueMyPairCounter] = {"TableName" : SrcTable,
                                                            "Columns" : MySQLCols,
                                                            "NullCols" : MyNullSQLDict,
                                                            "fileDLM" : DLM00,
                                                            "fileToImport" : MySrcTxt}
                            if MyEmptyNull is True :
                                MySQL = MySQL + MyNullSQL
                        #END : else : #'CSV Found
                    #END : if len(MyData) != 0 :
                #END : while len(uniqueMyPair) > 0
                del uniqueMyPair
                #del MyPair
            #END : if MySQLiteCache not in ["Y", "YT"] :
            if AnyFile == 0 :
                FinalRow = -2
                return FinalRow #'Files expected for SQLite Import & none found

            """
            '===========
            'Set O/P Fmt
            '===========
            """
            MyOutSQL = []
            if  self.myUtils.IsEmptyOrNone(OutExcel) is False:
                DLM00 = ""
                OutExcelFileName, OutExcelFileExt = legacy.os.path.splitext(OutExcel)
                if OutExcelFileExt.lower() == ".txt" : #'No Dlm. E.g., .txt extension
                    MyOutSQL.append(".mode column")
                else :
                    DLM00 = self.myUtils.GetFileDLM(OutExcel)
                    if DLM00 == "," and ll_QuoteCSV is True :
                        MyOutSQL.append(".mode csv")
                    else : #'Delimiter present
                        MyOutSQL.append(".mode list")
                        MyOutSQL.append('.separator "{0}"'.format(DLM00))

                if self.myUtils.IsEmptyOrNone(MySServer) is True:
                    MyOutSQL.append(".output '{0}'".format(OutExcel))
                else :
                    if IsLinux == False :
                        MyOutSQL.append(".output $tempout")
                    else : #Linux
                        MyOutSQL.append(".output {0}.h".format(MyInstance2))
                #END : if MySServer is None
            elif self.myUtils.IsEmptyOrNone(OutFile) is False:
                OutFile, atab = self.myUtils.ChKTab(OutFile)
                DLM00 = self.myUtils.GetFileDLM(OutFile)

                if atab is True or DLM00 == "\t" : #'Tab Delimited File requested
                    MyOutSQL.append(".mode list")
                    MyOutSQL.append(r'.separator "{0}"'.format(r"\t"))
                else : #'Normal
                    MyOutSQL.append(".mode column")

                MyOutSQL.append(".output '{0}'".format(OutFile))
                #'Tab delimited or Normal Test
            else :
                MyOutSQL.append(".mode column")
                MyOutSQL.append(".output stdout")
            #END : if not OutExcel is None :

            #'=== SQLite64===
            if IsLinux is True :
                MyOutSQL.append(".load /hadoop/staging/applications/sqlpathfinder/SQLite/sqliteextfunc_v2")
            else :
                if self.myUtils.gIs64 == 0:
                    MyOutSQL.append(".load '{0}'".format(legacy.os.path.join(".\\", MySExt)))
            #'=== SQLite64===
            MyOutSQL.append(".width {0}".format(__SPW * 14))

            if self.myUtils.IsEmptyOrNone(l_Null) is False :
                MyOutSQL.append(".nullvalue {0}".format(l_Null))

            MySQLStr = "{0}\n{1}\n;".format("\n".join(MySQL + MyOutSQL + AttachSQL), Command1)
            self.logger.debug("{0} - MySQLStr: {1}".format(calling_func, MySQLStr))

            if self.myUtils.IsEmptyOrNone(MyText) is True :
                self.myUtils.ConsoleWithTimeStamp("\nGetting Data Using SQLite")
            elif IsQuiet is False:
                self.myUtils.Console("\n{0} ...{1}\n".format(MyText, self.myUtils.DatetimeNow))

            MyJobCmd = "%COMSPEC%"
            MyJobArgs = []
            MyJobArgs.append("/c")

            #usePySQLite = False #temporary setting
            if IsQuiet is False:
                if MySQLiteCache in ["Y", "YT"] :
                    MyJobArgs.append("&&@Echo Starting SQL Query using SQLiteEXE ...")
                    if useSQLiteEXE is False:
                        self.myUtils.Console("Starting SQL Query ...\n")
                else:
                    MyJobArgs.append("&&@Echo Starting Data Import and SQL Query using SQLiteEXE ...")
                    if useSQLiteEXE is False:
                        self.myUtils.Console("Starting Data Import and SQL Query ...\n")

            if self.myUtils.IsEmptyOrNone(MySServer) is True:

                if useSQLiteEXE is False:
                    #region -- use Python SQLite module
                    #interactive or SH -- will be run using python SQLITE3 library
                    conObj = None
                    self.logger.debug("{0} - AttachSQL: {1}".format(calling_func, AttachSQL))
                    self.logger.debug("{0} - D_SSMDF: {1}".format(calling_func, D_SSMDF))
                    if len(AttachSQL) > 0:
                        #need to attach an external DB file to 'in memory' instance
                        try :
                            conObj = self.getStandaloneCon(AttachSQL="\n".join(AttachSQL))
                            #conObj.execute("\n".join(AttachSQL))
                        except Exception as err:
                            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                            raise
                    elif self.myUtils.IsEmptyOrNone(D_SSMDF) is False:
                        #need to open an external file directly
                        #conObj = sqlite3.connect(D_SSMDF)
                        conObj = self.getStandaloneCon(dbFileToOpen=D_SSMDF)
                    else:
                        conObj = self.getStandaloneCon()
                    try :
                        self.logger.debug("{0} - FilesToImportDict: {1}".format(calling_func, FilesToImportDict))
                        _rowCount = 0
                        files_dict_w_encoding = {}
                        for uniqepairCntr, ImportDetails in FilesToImportDict.items():
                            fileNameToImp = ImportDetails["fileToImport"]
                            _encoding = self.myUtils.detectFileEncoding(fileNameToImp, readall=True)
                            files_dict_w_encoding[fileNameToImp] = _encoding
                            self.logger.debug("{0} - {1} - fileNameToImp: '{1}'; encoding : {2}".format(calling_func, uniqepairCntr, fileNameToImp, _encoding))
                            _tblName = ImportDetails["TableName"]
                            _cols = ImportDetails["Columns"]
                            _nullcols = ImportDetails["NullCols"]
                            _fileDLM = ImportDetails["fileDLM"]
                            self.logger.debug("{0} - _tblName: {1}".format(calling_func, _tblName))
                            self.logger.debug("{0} - _cols: {1}".format(calling_func, _cols))
                            self.logger.debug("{0} - _nullcols: {1}".format(calling_func, _nullcols))
                            self.logger.debug("{0} - _fileDLM: {1}".format(calling_func, _fileDLM))
                            _rowCount = self.LoadFromFile(fileNameToImp, _tblName,colNamesListWithDT=_cols,
                                              EANImport = MyEmptyNull, EANImportDefaultDict=_nullcols,
                                              conObj = conObj, fileDLM = _fileDLM, sourceDataFileHasHeaders = not ll_NoHdrs
                                              , RowIdStartAt2=RowIdStartAt2, encoding=_encoding)
                            if conObj is not None :
                                conObj.commit() # commit the data
                            FinalRow = FinalRow + _rowCount
                        #END : for uniqepairCntr, ImportDetails in FilesToImportDict.items():
                        final_write_encoding = None
                        if len(files_dict_w_encoding) > 0:
                            self.logger.debug("{0} - get final encoding for : {1}".format(calling_func, files_dict_w_encoding))
                            final_write_encoding = self.myUtils.detectFinalEncodingForDictOfFiles(files_dict_w_encoding)

                        if conObj is not None :
                            curObj = conObj.cursor()
                            if final_write_encoding is None:
                                final_write_encoding = self.get_SQLite_Encoding(conObj) #data has been loaded...get the
                        else:
                            if final_write_encoding is None:
                                final_write_encoding = 'UTF-8' # this is the default encoding used by SQLite3
                            curObj = self.myMemTable.cursor()
                        self.logger.debug("{0} - final_write_encoding : {1}".format(calling_func, final_write_encoding))
                        self.logger.debug("{0} - Command1: {1}".format(calling_func, Command1))
                        if self.myUtils.IsEmptyOrNone(Command1) is False : #proceed only if there is SQL qeury to execute...else skip...might be to only load into .sdb from a sourc file
                            Command1 = Command1.replace('"[', '[').replace(']"', ']') #this is needed sometime UI inserts " around []...sqlite doesnt like this
                            Command1List = self.parse_GetSQLStrList(Command1)

                            lenCommand1List = len(Command1List)
                            self.logger.debug("{0} - lenCommand1List: {1}".format(calling_func, lenCommand1List))
                            cmdCnt = 0
                            while len(Command1List) > 0 :
                                cmd1 = Command1List.pop(0).strip()
                                if self.myUtils.IsEmptyOrNone(cmd1) is False :
                                    cmd1 = cmd1.replace('"[', '[').replace(']"', ']') #this is needed sometime UI inserts " around []...sqlite doesnt like this
                                    self.logger.debug("{0} - Executing Command1 item : {1}".format(calling_func, cmd1))
                                    if cmdCnt == 0 and lenCommand1List > 1 and legacy.re.match("select", cmd1.strip(),legacy.re.IGNORECASE) is None:
                                        self.logger.debug("{0} - Multiquery using executescript()".format(calling_func))
                                        curObj.execute("BEGIN TRANSACTION")
                                        curObj.executescript(cmd1)
                                        if conObj is not None :
                                            conObj.commit()
                                        else :
                                            self.myMemTable.commit()
                                        cmdCnt = cmdCnt + 1
                                    else:
                                        self.logger.debug("{0} - Select query using execute()".format(calling_func))
                                        curObj.execute(cmd1)
                                    tmpMatch = legacy.re.match("select|WITH RECURSIVE|WITH", cmd1.strip(),legacy.re.IGNORECASE)
                                    self.logger.debug("{0} - match : {1}".format(calling_func, tmpMatch))
                                    if tmpMatch:# or re.match("WITH RECURSIVE", cmd1.strip(),re.IGNORECASE):
                                        if self.myUtils.IsEmptyOrNone(OutExcel) is False :
                                            self.logger.debug("{0} - ll_NoHdrs1 = {1}".format(calling_func, ll_NoHdrs))
                                            FinalRow = self.writeCursorToFile(curObj,None, OutExcel, OutTT, append=myAppend, fetchChunkSize=50000, FinalRow=0, ll_NoHdrs=ll_NoHdrs, ll_QuoteCSV=ll_QuoteCSV, SQLite_Encoding=final_write_encoding)
                                        else :
                                            #write to temp table to output to console
                                            #to test this logic block run Query: \\atdfile3.ch.intel.com\atd-web\PathFinding\SQLPathFinder_Other\Regression_Library\Version_2\Regression_SQLite_Comma.vg2 --> "Process CSV File in SQLite With Embedded Commas" in Regression_SQLite.spf
                                            self.logger.debug("{0} - check if output to console is needed OutTT : {1}".format(calling_func, OutTT))
                                            if OutTT is True:
                                                tmp_outExcel = r".\run_sqlite_tmp{0}.csv".format(self.myUtils.RNStrNew)
                                                FinalRow = self.writeCursorToFile(curObj,None, tmp_outExcel, OutTT, append=myAppend, fetchChunkSize=50000, FinalRow=0, ll_NoHdrs=ll_NoHdrs, ll_QuoteCSV=ll_QuoteCSV, SQLite_Encoding=final_write_encoding)
                                                #columns = [i[0] for i in curObj.description]
                                                self.myUtils.Send_To_Term(tmp_outExcel, OutTT, ll_NoHdrs, FinalRow, ll_QuoteCSV=ll_QuoteCSV)
                                                #if FinalRow == 0 and ll_NoHdrs is False:
                                                #    #looks like zero records -- send to terminal just the columns
                                                #    self.myUtils.Send_To_Term_Row(None, columns, OutTT, ll_NoHdrs=ll_NoHdrs, UsesRowFactory=False, isFirstRow=True)
                                                #else :

                                                self.myUtils.DelAFile(tmp_outExcel)
                        return FinalRow
                    except Exception as err:
                        self.logger.debug("{0} - {1}".format(calling_func, legacy.sys.stderr))
                        self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                        raise
                    finally :
                        if conObj is not None :
                            try :
                                conObj.close()
                                self.logger.debug("{0} - closed conObj".format(calling_func))
                            except Exception as err:
                                self.logger.exception("{0} - Error in finally block while closing the conObj : {1}".format(calling_func, err.args[0]))
                                pass
                    #endregion -- use Python SQLite module
                else: #use SQLite64.exe
                    #region -- use SQLite64.exe
                    """
                    '============
                    'Save/Run Script
                    '============
                    """
                    #save script
                    self.myUtils.DoCreateFileA(s1, MySQLStr, "Run_SQLite_Exe")
                    MyJobArgs.append("&&@Echo.")
                    MyJobArgs.append('&&"{0}"'.format(SSEExe))
                    MyJobArgs.append('"{0}"'.format(D_SSMDF if self.myUtils.IsEmptyOrNone(D_SSMDF) is False else ""))
                    MyJobArgs.append('<')
                    MyJobArgs.append('"{0}"'.format(legacy.os.path.abspath(s1)))
                    MyJobArgs.append("&&IF")
                    MyJobArgs.append("%ERRORLEVEL%")
                    MyJobArgs.append("GTR")
                    MyJobArgs.append("0")
                    MyJobArgs.append("EXIT")
                    MyJobArgs.append("1&&EXIT")
                    MyJobArgs.append("0")
                    try :
                        runStatus, runExitCode = self.myUtils.Run(MyJobCmd, MyJobArgs, usePopen=True)
                        self.logger.debug("{0} - runStatus : {1}".format(calling_func, runStatus))
                        self.logger.debug("{0} - runExitCode : {1}".format(calling_func, runExitCode))
                        FinalRow = 0
                        return FinalRow
                    except Exception as err:
                        self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                        errMsg = "SQlite Error detected. Exiting ..."
                        raise Exception(errMsg)
                    #endregion -- use SQLite64.exe
            else : #'SQLite DB on App Svr
                """
                '============
                'Save/Run Script
                '============
                """
                #save script
                self.myUtils.DoCreateFileA(s1, MySQLStr, "Run_SQLite")
                MyJobCmd = None
                MyJobArgs = []
                if IsLinux is False :
                    #MyJobCmd = "{0}".format(os.path.join(ExePath2, "Get_SQLite_Mid.exe"))
                    MyJobArgs.append('CD')
                    MyJobArgs.append('/d')
                    MyJobArgs.append('"{0}"'.format(legacy.os.path.join(ExePath2, "Get_SQLite_Mid.exe")))
                    MyJobArgs.append('/NODE="{0}"'.format(MySServer))
                    MyJobArgs.append('/DB="{0}"'.format(D_SSMDF))
                    MyJobArgs.append('/TT="{0}"'.format(OutTT))
                    MyJobArgs.append('/OUT="{0}"'.format(OutExcel))
                    MyJobArgs.append('/SQL="{0}"'.format(s1))
                    MyJobArgs.append('/INSTANCE="{0}"'.format(MyInstance))
                else : #'Linux
                    #MyJobCmd = "{0}".format(os.path.join(ExePath2, "GetHadoop.exe"))
                    MyJobArgs.append('CD')
                    MyJobArgs.append('/d')
                    MyJobArgs.append('"{0}"'.format(legacy.os.path.join(ExePath2, "GetHadoop.exe")))
                    MyJobArgs.append('/NODE="{0}"'.format(MySServer))
                    MyJobArgs.append('/NODEGROUP="{0}"'.format(D_SSMDF))
                    MyJobArgs.append('/TT="{0}"'.format(OutTT))
                    MyJobArgs.append('/OUTFILE="{0}"'.format(OutExcel))
                    MyJobArgs.append('/SQLITE="{0}"'.format(s1))
                    MyJobArgs.append('/MODE="{0}"'.format("PSCP"))
                    MyJobArgs.append('/KEYFILE="{0}"'.format("N"))
                    MyJobArgs.append('/INSTANCE="{0}"'.format(MyInstance2))
                #END : if IsLinux == False :
                #call Run
                try :
                    if IsQuiet is False : #'N
                        self.myUtils.Console()
                    runStatus, runExitCode = self.myUtils.Run(MyJobCmd, MyJobArgs)
                    self.logger.debug("{0} - runStatus : {1}".format(calling_func, runStatus))
                    self.logger.debug("{0} - runExitCode : {1}".format(calling_func, runExitCode))
                except Exception as err:
                    self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
                    errMsg = "SQlite Error detected. Exiting ..."
                    raise Exception(errMsg)
            #END : else : #'SQLite DB on App Svr
            #END : if MySServer is None :
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
        finally :
            """
            '========
            'Clean Up
            '========
            """
            try :
                if s1 is not None :
                    self.myUtils.DelAFile(s1)
                if PreProcCSV is True and not DelCSVLst is None :
                    self.logger.debug("{0} - cleaning up DelCSVLst : {1}".format(calling_func, DelCSVLst))
                    while len(DelCSVLst) > 0 :
                        fileItem = DelCSVLst.pop()
                        self.myUtils.DelAFile(fileItem)

                    del DelCSVLst
                if self.myUtils.SHisSHEntry is True :
                    pass
                elif MyLocal == "N" : #'Procs copied local
                    if MyCvtExe is not None :
                        self.myUtils.DelAFile(MyCvtExe)

            except Exception as err:
                self.logger.exception("{0} - error during final cleanup : {1}".format(calling_func, err.args[0]))
                pass

    def getStandaloneCon(self, dbFileToOpen='', AttachSQL=None):
        """Add the missing UDF before running the caller's attachment SQL."""
        conTemp = super().getStandaloneCon(dbFileToOpen)
        conTemp.create_function("CharIndex_v2", 4, self.sqliteCharIndex_v2)
        if AttachSQL is not None:
            conTemp.execute(AttachSQL)
        return conTemp

    def sqliteCharIndex_v2(self, needle, haystack, start=1, occurrence=1):
        """Return the 1-based position expected by the legacy CSV-list SQL."""
        if needle is None or haystack is None:
            return 0
        needle = str(needle)
        haystack = str(haystack)
        try:
            offset = max(int(start) - 1, 0)
            occurrence = int(occurrence)
        except (TypeError, ValueError):
            return 0
        if not needle or occurrence < 1:
            return 0

        found = -1
        for _ in range(occurrence):
            found = haystack.find(needle, offset)
            if found < 0:
                return 0
            offset = found + 1
        return found + 1
