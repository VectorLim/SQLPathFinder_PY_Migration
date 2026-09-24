"""
License : Copyright (c) Intel Corporation 2023
Product: Intel.ATTD.Auto.SQLPathFinder
Module : SQLPathFinder Python Extract Engine 
Author : vishwas.Nataraj@intel.com;SQLPathFinder_Support@intel.com
File Version : 2.0.3.1
History: 
1.0.0.0 : Nataraj : Initial Version
1.0.0.1 : Nataraj : updated to handle ll_QuoteCSV and output of .txt file
1.0.0.2 : Nataraj : LoadFromFileUsingDF, LoadFromFile --> check reserved SQLITE3 column names -- to raise error if user data file to be loaded to SQLITE has 'rowid', '_rowid_' or 'oid' (reserved SQLITE3 column names)
2.0.0.0 : vanatara : Updated to support both Python 2.7.15 & Python 3.6
2.0.0.1 : vanatara : LoadFromFile() --> Updated to handle special characters in Pyhton 3.x
2.0.0.2 : vanatara : LoadFromFile(), writeCursorToFileWithoutQuote(), writeCursorToFileWithQuote() --> Updated to handle special characters in Pyhton 3.x
2.0.0.3 : vanatara : Updated to handle file encoding for Python 3.x
2.0.0.4 : vanatara : added 'sqliteRegexReplace' to be used as SQLite UDF-'SPFRegexReplace'
2.0.0.5 : vanatara : added support for 'WITH' & 'WITH RECURSIVE' commands in Run_SLQITE() to have output files
2.0.0.6 : vanatara : updated Run_SQLite() -- '07/02/19 jclarke  30.142 Support new sqlite version & need to reverse .separator and .mode & eliminated 32 bit SQLite exe
2.0.0.7 : vanatara : LoadFromFile() -- bugfix : while reading columns with embedded '[]' convert them to '()'. LoadFromFileUsingDF() -- method is removed & replaced with LoadFromFile() 
2.0.0.8 : vanatara : Create_SQL_In_Like_List() -- bugfix : '252019_Nguyen-Dinh_Quyen' : in case (# 'Normal (MyInc<=0)) fulltrim of column value in source file issue. --> changed TRIM --> RTRIM in the SQL query
2.0.0.9 : vanatara : LoadFromFile() -- fix : '252019_Nguyen-Dinh_Quyen' : data has leading spaces, turned off cleaning up initial space in values in the 'csvreader' : skipinitialspace=False
2.0.1.0 : vanatara : Run_SQLite() -- 272019_SKChew_TaskScheduler_issue -- fix : corrected logic to determine location of 'SQLite exe'
2.0.1.1 : vanatara : LoadFromFile() -- 342019_Meier-Thomas_SQLiteLoad_NoneTypeError -- fix : update to handle new-line character in column names and raise proper error instead of NoneType error
2.0.1.2 : vanatara : LoadFromFile() -- updated logic for 'EANImport is True' to perform SQL based update-set value = null for empty string value
2.0.1.3 : vanatara : LoadFromFile() -- added additional parameter to 'displayColumnMismatchWarnings=True' to toggle display of column count mismatch warnings
                                    -- updated to use defaultColValueIfEmpty provided by caller, when performing update when EANImport=True 
                     Load_Table_Design() -- update to pass 'displayColumnMismatchWarnings=False' when calling LoadFromFile() to suppress column count mismatch warning to console
2.0.1.4 : vanatara : LoadFromFile() -- added logic to skip blank lines
2.0.1.5 : vanatara : Process_Get_CSV_List2_Other() -- updated to support InGroupOR feature
2.0.1.5a : vanatara : Process_Get_CSV_List2_Other() -- updated to support Incremental InGroupOR
2.0.1.6 : vanatara : removed unused encryption feature
2.0.1.7 : vanatara : fix bug that removes leading spaces while reading column data in Create_SQL_In_Like_List()
2.0.1.8 : vanatara : bugfix handling dual join of single input .csv in Run_SQLite()
2.0.1.9 : vanatara : Run_SQLite() support /APPENDMODE
2.0.2.0 : vanatara : MemTable.sqliteRegexSearch() added to support 'SPFRegexSearch' SQLite UDF to provide regex-search on column data
2.0.2.1 : vanatara : MemTable.SPFWriteLOBToFile() added to support 'SPFWriteLOBToFile' SQLite UDF to write column value to a file specified 
2.0.2.2 : vanatara : MemTable.Create_SQL_In_Like_List() update to ignore rows with just '%' for LIKE case
2.0.2.3 : vanatara : Update MemTable.Run_SQLite() + writeCursorToFileWithQuote() + writeCursorToFileWithoutQuote() + writeCursorToFile() to use DB encoding, add get_SQLite_Encoding() to get DB encodig based on data loaded.
2.0.2.4 : vanatara : bugfix Create_SQL_In_Like_List() handling like group filtering with values having leading spaces
2.0.2.5 : jmclarke : Replaced time.clock(0 with time.perf_counter()
2.0.2.6 : jmclarke : Fixed issue with SQLite Delete statement: Change to: DELETE FROM [{0}] WHERE rowid = 1".format(tableName)
2.0.2.6 : vanatara : Update LoadFromFile() to accept encoding for file to be loaded. Update Run_SQLite() to determine+use file encoding for files to be loaded
2.0.2.8 : vanatara : Add SQLite UDF MemTable.sqlitePrepLikeValue() to support InGroup : 'Like/Not Like' parsing. Update MemTable.getStandaloneCon() with UDF 'SPFPrepLikeValue' (sqlitePrepLikeValue)
2.0.2.8a : vanatara : Update MemTable.Create_SQL_In_Like_List() to align InGroup 'LIKE' changes
2.0.2.9 : jmclarke : Replaced <$!> with " in "Create_SQL_In_Like_List":
2.0.3.0 : vanatara : Add MemTable.Update_DBNode_in_Multi_Node_List() ro handle node value update in memtable data
2.0.3.1 : vanatara : Update to support Python 3.13
"""
from SPFLib import * #isPYTHON2 #defined in SPFLib\__init__.py. IF True then 'Pyhton 2' IF False 'Python 3'

import sqlite3
from SPFLib.SPFUtilities.spflogger import SPFLogger 
from SPFLib.SPFUtilities.utils import Utilities, SPFNothingToProcessException

#if isPYTHON2 is False:
#    sys._enablelegacywindowsfsencoding()

class MemTable(object):    
    
    #locals
    #CONSTANTS
    MEM_IN_GROUP_TBL_NAME = "memingroup"
    #static
    myMemTable = None
    myUtils = None
    logger = None
    __mykwargs = None
    __myargs = None
    __sqlite3WriterInitialized = False
    #instance variables
    __tableName = None
    __colNamesList = None
    __sourceDataFileName = None
    __sourceDataFileHasHeaders = None
    tbl_ColNames_Dict = {}
    reservedColNames = ['rowid', '_rowid_'] #, 'oid'
    #def __init__(self, tableName=None, colNamesList=None, *args, **kwargs):
    def __init__(self, tableName=None, colNamesList=None, sourceDataFileName=None, sourceDataFileHasHeaders=True):
        """
        """
        if not MemTable.logger :
            MemTable.logger = SPFLogger.GetLogger()

        if not MemTable.myUtils :
            MemTable.myUtils = Utilities()
            self.logger.info("MemTable.myUtils created")

        if not MemTable.myMemTable :
            MemTable.myMemTable = self.getStandaloneCon()
            #MemTable.myMemTable.row_factory = sqlite3.Row -- commenting this out...writing to file large/wide dataset slows down immensly
            #MemTable.myMemTable.text_factory = str #-- to force string output is of non unicode
            self.logger.info(__name__ + " Initialized")
         
        if tableName is not None :
            self.__tableName = tableName
            self.logger.debug("MemTable : tableName = " + str(self.__tableName))

        if colNamesList is not None :
            self.__colNamesList = colNamesList
            self.logger.debug("MemTable : colNamesList = " + str(self.__colNamesList))

        if sourceDataFileName is not None : 
            self.__sourceDataFileName = sourceDataFileName
            self.logger.debug("MemTable : sourceDataFileName = " + str(self.__sourceDataFileName))

        self.__sourceDataFileHasHeaders = sourceDataFileHasHeaders
        self.logger.debug("MemTable : sourceDataFileHasHeaders = " + str(self.__sourceDataFileHasHeaders))

        if (self.__tableName is not None and self.__sourceDataFileName is not None) :
            self.LoadFromFile(self.__sourceDataFileName, self.__tableName, self.__colNamesList, self.__sourceDataFileHasHeaders)
        
        self.SQLITE_GET_ENCODING_SQL = "PRAGMA ENCODING"
        return

    def __enter__(self):
        self.logger.info("__enter__")
        self.logger.info(self.__mykwargs['FileName'])
        self.logger.info(self.__mykwargs['TableName'])
        self.logger.info(self.__mykwargs['ColNames'])
        self.logger.info("sqlite3WriterInitialized = " + self.__sqlite3WriterInitialized)

        return self

    def __exit__(self, type, value, traceback):
        self.logger.info("__exit__")
        pass
    @property
    def TableName(self) :
        return self.__tableName

    @TableName.setter
    def TableName(self, value) :
        if value is None or len(value.strip()) == 0 :
            raise Exception("TableName is invalid : {0}".format(TableName))
        self.__tableName = value

    @property
    def ColNamesList(self) :
        return self.__colNamesList

    @property
    def RowCount(self) :
        """
        'Gets the row count of [MemInGrp] table
        """
        #locals
        __rowCount = 0 #default to 0 rows

        if (self.myMemTable is not None and self.__tableName is not None) :
            
            sqlStmnt = "SELECT count(*) FROM [" + self.__tableName + "]"
            self.logger.debug("sqlStr : " + sqlStmnt)
            """
            create cursor object
            """
            try :
                MemTableCursor = self.myMemTable.cursor()
            except Exception as err:
                self.logger.exception("Error while creating cursor object : {0}".format(err))
                raise 
        
            """
            Execute SQL statment 
            """
            try :
                MemTableCursor.execute(sqlStmnt)
            except Exception as err:
                self.logger.error("Error while executing SQL statement : {0}".format(err))
                raise

            try :
                """
                start processing the cursor results...there should be only 1 row
                """
                rows = MemTableCursor.fetchall()
        
                if not rows : 
                    """
                    no row returned, meaning no records in memtable
                    return 
                    """
                    self.logger.debug("Reached end of MemInGrp table")
                    __rowCount = 0
                else :
                    if len(rows) > 1 :
                        raise Exception("More than one row returned...expected only 1")
                    else :
                        __rowCount = rows[0][0] #just get the first row & first column value

                del MemTableCursor # clean up the cursor object as it is no longer needed...note: connection still exists
        
                #Done
            except Exception as err:
                self.logger.error(err)
                raise

        self.logger.debug("rowcount : " + str(__rowCount))
        return __rowCount

    @property
    def has_MemInGrp(self) :
        tblList = self.GetTablesInMemTable()
        if self.MEM_IN_GROUP_TBL_NAME in tblList :
            return True
        else :
            return False

    def Get_Multi_Node_List(self):
        """
        '============================================================
        'Returns a comma delimited list of unique nodes
        '
        '(input) :
        '----
        'None
        '============================================================
        '(Output) : 
        ' Nodes : a ',' seperated Nodes list
        """
        Nodes = ""
        sqlStmnt = "SELECT GROUP_CONCAT(t1.Node) as ConcatedNodes FROM (SELECT DISTINCT Node from [{0}] order by 1 ASC) AS T1".format(self.MEM_IN_GROUP_TBL_NAME)
        """
        create cursor object
        """
        try :
            MemTableCursor = self.myMemTable.cursor()
        except Exception as err:
            self.logger.error("Error while creating cursor object : ")
            self.logger.error(err)
            raise 

        """
        Execute SQL statment 
        """
        try :
            MemTableCursor.execute(sqlStmnt)
        except Exception as err:
            self.logger.error("Error while executing SQL statement : ")
            self.logger.error(err)
            raise

        try :
            """
            start processing the cursor results...there should be only 1 row
            """
            rows = MemTableCursor.fetchall()
        
            if not rows : 
                """
                no row returned, meaning no records in memtable
                return 
                """
                self.logger.debug("Reached end of MemInGrp table")
                return Nodes
            else :
                if len(rows) > 1 :
                    raise Exception("More than one row returned...expected only 1")
                else :
                    Nodes = rows[0][0] #just get the first row & first column value

            del MemTableCursor # clean up the cursor object as it is no longer needed...note: connection still exists
        
            self.logger.debug("Nodes : " + str(Nodes))

            #Done
            return Nodes

        except Exception as err:
            self.logger.error("Error while processing the cursor results : " )
            self.logger.error(err)
            raise
        self.logger.debug(sqlStmnt)
        return None

    def Update_DBNode_in_Multi_Node_List(self, original_dbnode:str, updated_dbnode:str):
        """
        this will be called when a node conversion is happening and DB node value needs to be updated in the memtable
        '(input) :
        '----
        1. original_dbnode:str : original DBNode value that already exists in memtable
        2. updated_dbnode:str : new DBNode value that will replace the original DBNode value in memtable
        """
        if self.has_MemInGrp is False:
            self.logger.debug(f"No MemTable is being used...return")
            return

        #region locals
        sqlStmnt = f"UPDATE [{self.MEM_IN_GROUP_TBL_NAME}] SET Node = '{updated_dbnode}' WHERE Node = '{original_dbnode}'"
        self.logger.debug(f"update_sql : {sqlStmnt}")
        #endregion locals

        self.Run_SQLQuery(sqlStmnt)
        self.logger.debug(f"updated DBNode")
    #END : def Update_DBNode_in_Multi_Node_List

    def Get_SQL_Multi(self, currentNode) :
        """
        ' get replacement values for given node -- new design
        ' Input Args: 
        '------------
        ' 1.) currentNode : stringType : currentNode (site) value for which get the list of chunk values that need to be replaced in SQLQuery
        'Output Args :
        '------------
        ' 1.) keysList : listType : list of values for give currentNode...that will be used to substitute in SQLQuery in calling method
        """
        #locals
        calling_func = self.myUtils.getCallingFuncName()
        keysList = []
        try :
            self.logger.debug("{0} - currentNode : {1}".format(calling_func, currentNode))
            sqlStmnt = "select [Key] from {0} where [Node] IN ('{1}', '')".format(self.MEM_IN_GROUP_TBL_NAME, currentNode)
            self.logger.debug("{0} - sqlQuery : {1}".format(calling_func, sqlStmnt))
            """
            create cursor object
            """
            try :
                MemTableCursor = self.myMemTable.cursor()
            except Exception as err:
                self.logger.exception("{0} - Error while creating cursor object : {1}".format(calling_func, err.args[0]))
                raise 

            """
            Execute SQL statment 
            """
            try :
                MemTableCursor.execute(sqlStmnt)
            except Exception as err:
                self.logger.exception("{0} - Error while executing SQL statement : {1}".format(calling_func, err.args[0]))
                raise

            rows = MemTableCursor.fetchall()
            rowsCtr = 0
            while len(rows) > 0:
                rowsCtr = rowsCtr + 1
                row = rows.pop(0)
            #for row in rows :
                #keysList.append(row['Key']) #-- this needs SQLite3 RowFactory to be set
                keysList.append(row[0])
            self.logger.debug("{0} - keysList count : {1}".format(calling_func, rowsCtr))
            return keysList
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def Process_Multi_in(self, MyColnmList, mn2, IncNode, MyDTList, MyInc) :
        """
        """
        calling_func = self.myUtils.getCallingFuncName()

        def converter(idx, item) :
            """
                nested function of Process_Multi_in...to handle Datatype of column
                '************
                'Get DataType
                '************
            """

            output = ""
            if MyDTList[idx] == "D" : #'Date Data Type
                #myStart = "to_date('"
                #myEnd = "','YYYY-MM-DD hh24:mi:ss')"
                output = "\"to_date('\" || [{0}] ||\"','YYYY-MM-DD hh24:mi:ss')\"".format(item)
            elif MyDTList[idx] == "N" : #'Numeric Data Type
                #myStart = ""
                #myEnd = ""
                output = "[{0}]".format(item)
            else : #'Character Data Type
                #myStart = "'"
                #myEnd = "'"
                output = "\"'\" || [{0}] || \"'\"".format(item)

            return output
        #END : def converter

        #start of parent function Create_SQL_In_Like_List
        #local variables
        nodesList = ['']
        try : 
            self.logger.debug("{0} - MyColnmList : {1}".format(calling_func, MyColnmList))
            self.logger.debug("{0} - mn2 : {1}".format(calling_func, mn2))
            self.logger.debug("{0} - IncNode : {1}".format(calling_func, IncNode))
            self.logger.debug("{0} - MyDTList : {1}".format(calling_func, MyDTList))
            self.logger.debug("{0} - MyInc : {1}".format(calling_func, MyInc))
            self.logger.debug("{0} - TableName : {1}".format(calling_func, self.TableName))

            
            
            #print curSQLite.description
            if MyInc > 0 :
                self.logger.debug("{0} - MyInc > 0 block".format(calling_func))
                if IncNode is True : # include node information
                    #get distinct nodes ordered ASC
                    sqlStr_SelDistNodes = "SELECT DISTINCT [{0}] AS [{0}] FROM [{1}] ORDER BY [{0}] ASC".format(mn2.strip("[]"), self.TableName)
                    curSQLite = self.myMemTable.cursor()
                    curSQLite.execute(sqlStr_SelDistNodes)
                    nodesList = [rowItem[0] for rowItem in curSQLite.fetchall()]
                    del curSQLite

                    self.logger.debug("{0} - IncNode nodesList : {1}".format(calling_func, nodesList))
                    if len(nodesList) == 0 :
                        errMsg = "No rows were found in {0}\nAn empty item list will be returned...".format(l_csv)
                        #l_Command1 = ""
                        raise Exception(errMsg)
                #END : if IncNode is True :

                sqlstr_drop = "DROP TABLE IF EXISTS [{0}]".format(self.MEM_IN_GROUP_TBL_NAME)
                sqlstr_create = "CREATE TABLE IF NOT EXISTS [{0}] ([Key], [Node], [MyInc])".format(self.MEM_IN_GROUP_TBL_NAME)
                sqlstr_insert = "INSERT INTO [{0}] ([Key], [Node], [MyInc]) values (?, ?, ?)".format(self.MEM_IN_GROUP_TBL_NAME)
                curSQLite = self.myMemTable.cursor()
                self.logger.debug("{0} - sqlstr_drop : {1}".format(calling_func, sqlstr_drop))
                curSQLite.execute(sqlstr_drop)
                self.logger.debug("{0} - sqlstr_create : {1}".format(calling_func, sqlstr_create))
                curSQLite.execute(sqlstr_create)
                self.logger.debug("{0} - sqlstr_insert : {1}".format(calling_func, sqlstr_insert))

                sqlStr_select1 = "SELECT DISTINCT \"(\" || {0} || \")\" as [Key], {1} as [Node]".format(" || ',' || ".join(converter(idx, item) 
                                                                                                                          for idx, item in enumerate(MyColnmList)),
                                                                                                       "''" if IncNode is False else "[{0}]".format(mn2)
                                                                                                       )

                sqlStr_from = " FROM [{0}] ".format(self.TableName)
                sqlStr_where1 = " WHERE ({0}) ".format(" AND ".join(["([{0}] is not null and [{0}] != '')".format(col) #SELECT 
                                                                    for col in MyColnmList] #FROM #{3} WHERE CLAUSE ignore NULL & empty
                                                                    ))

                sqlStr_order1 = "ORDER BY [{0}]".format("], [".join(MyColnmList)) # ORDER BY Clause)

                for nodeItem in nodesList :
                    self.logger.debug("{0} - nodeItem : {1}".format(calling_func, nodeItem))

                    
                    if IncNode is True :
                        sqlStr = "{0} {1} {2} {3}".format(sqlStr_select1, 
                                                          sqlStr_from,
                                                          "{0} {1}".format(sqlStr_where1, 
                                                                   "AND [{0}] = '{1}'".format(mn2, nodeItem)),
                                                          sqlStr_order1)
                    else :
                        sqlStr = "{0} {1} {2} {3}".format(sqlStr_select1, 
                                                          sqlStr_from,
                                                          sqlStr_where1,
                                                          sqlStr_order1)
                    #sqlStr = """SELECT DISTINCT \"(\" || {0} || \")\" as [Key], [{1}] as [Node]
                    #from [{2}] 
                    #where ({3}) AND [{1}] = '{5}'
                    #order by [{4}]""".format(
                    #                        " || ',' || ".join(converter(idx, item) for idx, item in enumerate(MyColnmList)),
                    #                        mn2,                      #{1}
                    #                        self.TableName,           #{2}
                    #                        " AND ".join(["([{0}] is not null and [{0}] != '')".format(col) #SELECT 
                    #                                    for col in MyColnmList] #FROM #{3} WHERE CLAUSE ignore NULL & empty
                    #                                    ),
                    #                        "], [".join(MyColnmList), #{4} ORDER BY Clause
                    #                        nodeItem    #{5} node for which Key column data needs to be fetched
                    #                        )
                                        
                    self.logger.debug("{0} - sqlStr : {1}".format(calling_func, sqlStr))

                    curSQLite = self.myMemTable.cursor()
                    curSQLite.execute(sqlStr)

                    #save data into memingroup table
                
                    rows = curSQLite.fetchall()
                    #now chunk with MyInc
                    rowList = self.myUtils.chunkAList(rows, MyInc)
                    for rowGrpItemIdx, rowGrpItem in enumerate(rowList) :
                            rowGrp = "\n,".join([rowItem[0] # SELECT first column
                                                                  for rowItem in rowGrpItem])
                            rowList[rowGrpItemIdx] = ["\n{0}\n)".format(rowGrp), nodeItem, MyInc]

                    #now insert into memingroup
                    curSQLite.executemany(sqlstr_insert, rowList)
                    
                
                #if debug logging enabled log the memingroup content
                if self.logger.isEnabledFor(10) == True :
                    sqlstr_selMem = "SELECT * from [{0}]".format(self.MEM_IN_GROUP_TBL_NAME)
                    self.logger.debug("{0} - sqlstr_selMem : {1}".format(calling_func, sqlstr_selMem))
                    curSQLite.execute(sqlstr_selMem)
                
                    for rowsInMeminGrp in curSQLite.fetchall() :
                        #self.logger.debug("{0} - rowsInMeminGrp : {1}, {2}, {3}".format(calling_func, rowsInMeminGrp["Key"], rowsInMeminGrp["Node"], rowsInMeminGrp["MyInc"]))
                        self.logger.debug("{0} - rowsInMeminGrp : {1}, {2}, {3}".format(calling_func, rowsInMeminGrp[0], rowsInMeminGrp[1], rowsInMeminGrp[2]))

                del curSQLite
                return "<<<spf-$item$-list>>>"
            else :
                self.logger.debug("{0} - Normal (MyInc<=0) block".format(calling_func))
                #, [{1}] as [Node]
                sqlStr = """SELECT DISTINCT \"(\" || {0} || \")\" as [Key]
                from [{2}] 
                where ({3})
                order by [{4}]""".format(
                                        " || ',' || ".join(converter(idx, item) for idx, item in enumerate(MyColnmList)),
                                        '"{0}"'.format(mn2),                      #{1}
                                        self.TableName,           #{2}
                                        " AND ".join(["([{0}] is not null and [{0}] != '')".format(col) #SELECT 
                                                    for col in MyColnmList] #FROM #{3} WHERE CLAUSE ignore NULL & empty
                                                    ),
                                        #"], [".join([mn2] + MyColnmList if IncNode[0] == "Y" else MyColnmList)  , #{4} ORDER BY Clause -- IncNode is bool in Py
                                        "], [".join([mn2] + MyColnmList if IncNode is True else MyColnmList)  , #{4} ORDER BY Clause
                                        )
                                        
                self.logger.debug("{0} - sqlStr : {1}".format(calling_func, sqlStr))

                curSQLite = self.myMemTable.cursor()
                curSQLite.execute(sqlStr)

                rowList = []
                rows = curSQLite.fetchall()
                #chunkSize = 1000
                #chunk the rows not exceeding chunkSize
                #rowList = [rowGrp for rowGrp in (rows[pos:pos + chunkSize] 
                #               for pos in xrange(0,len(rows), chunkSize))]
                rowList = self.myUtils.chunkAList(rows)

                self.logger.debug("{0} - rowList : {1}".format(calling_func, len(rowList)))
                
                #del rows # clean up no longer need after this

                #concat the Key items in each group with "\n,"
                for rowGrpItemIdx, rowGrpItem in enumerate(rowList) : #rowItem["Key"] 
                    rowList[rowGrpItemIdx] = "\n, ".join([rowItem[0] 
                                                          for rowItem in rowGrpItem])
                
                self.logger.debug("{0} - rowList : {1}".format(calling_func, len(rowList)))

                return rowList
            #END : if MyInc > 0
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
    #END : def Process_Multi_in

    def Process_Get_CSV_List2_Other(self, MyColnmList, MyOpList, MyAliasList, UseOR=False, mn2="", IncNode=False, MyInc=-1, MyOr=""):
        calling_func = self.myUtils.getCallingFuncName()

        def converter(idx, item) :
            """
                nested function of Process_Get_CSV_List2_Other...to handle Datatype of column + embed colName & operator
            """

            output = ""
            l_Operator = MyOpList[idx].strip().upper()
            MyDT = l_Operator[-1]
            myAlias = MyAliasList[idx]
            if MyDT == "D" : #'Date Data Type
                l_Operator = l_Operator[:-1] #strip of D
                output = "\"{0} {1} to_date('\" || trim([{2}]) ||\"','YYYY-MM-DD hh24:mi:ss')\"".format(myAlias, l_Operator, item)
            elif MyDT == "N" : #'Numeric Data Type
                l_Operator = l_Operator[:-1] #strip of N
                output = "\"{0} {1} \" || [{2}]".format(myAlias, l_Operator, item)
            elif l_Operator in ["LIKE", "NOT LIKE"] :
                output = "\"{0} {1} '\" || trim([{2}]) || \"%'\"".format(myAlias, l_Operator, item)
            else : #'Character Data Type
                #output = "\"{0} {1} '\" || trim([{2}]) || \"'\"".format(myAlias, l_Operator, item)
                output = "\"{0} {1} '\" || [{2}] || \"'\"".format(myAlias, l_Operator, item)

            return output
        #END : def converter

        nodesList = ['']
        try : 
            self.logger.debug("{0} - MyColnmList : {1}".format(calling_func, MyColnmList))
            self.logger.debug("{0} - MyOpList : {1}".format(calling_func, MyOpList))
            self.logger.debug("{0} - MyAliasList : {1}".format(calling_func, MyAliasList))
            self.logger.debug("{0} - TableName : {1}".format(calling_func, self.TableName))
            self.logger.debug("{0} - mn2 : {1}".format(calling_func, mn2))
            self.logger.debug("{0} - IncNode : {1}".format(calling_func, IncNode))
            self.logger.debug("{0} - MyInc : {1}".format(calling_func, MyInc))
            if MyInc > 0 :
                self.logger.debug("{0} - MyInc > 0 block".format(calling_func))
                if IncNode is True : # include node information
                    #get distinct nodes ordered ASC
                    sqlStr_SelDistNodes = "SELECT DISTINCT [{0}] AS [{0}] FROM [{1}] ORDER BY [{0}] ASC".format(mn2.strip("[]"), self.TableName)
                    curSQLite = self.myMemTable.cursor()
                    curSQLite.execute(sqlStr_SelDistNodes)
                    nodesList = [rowItem[0] for rowItem in curSQLite.fetchall()]
                    del curSQLite

                    self.logger.debug("{0} - IncNode nodesList : {1}".format(calling_func, nodesList))
                    if len(nodesList) == 0 :
                        errMsg = "No rows were found in {0}\nAn empty item list will be returned...".format(l_csv)
                        #l_Command1 = ""
                        raise Exception(errMsg)
                #END : if IncNode is True :
                sqlstr_drop = "DROP TABLE IF EXISTS [{0}]".format(self.MEM_IN_GROUP_TBL_NAME)
                sqlstr_create = "CREATE TABLE IF NOT EXISTS [{0}] ([Key], [Node], [MyInc])".format(self.MEM_IN_GROUP_TBL_NAME)
                sqlstr_insert = "INSERT INTO [{0}] ([Key], [Node], [MyInc]) values (?, ?, ?)".format(self.MEM_IN_GROUP_TBL_NAME)
                curSQLite = self.myMemTable.cursor()
                self.logger.debug("{0} - sqlstr_drop : {1}".format(calling_func, sqlstr_drop))
                curSQLite.execute(sqlstr_drop)
                self.logger.debug("{0} - sqlstr_create : {1}".format(calling_func, sqlstr_create))
                curSQLite.execute(sqlstr_create)
                self.logger.debug("{0} - sqlstr_insert : {1}".format(calling_func, sqlstr_insert))
                sqlStr_select1 = """SELECT DISTINCT \"(\" || {0} || \")\" as [Key], {1} as [Node]""".format(" || ' {0} ' || ".format("AND" if UseOR is False else "OR").join(converter(idx, item) for idx, item in enumerate(MyColnmList))
                                                                                                             ,"''" if IncNode is False else "[{0}]".format(mn2)
                                                                                                            )
                 
                sqlStr_from =  " FROM [{0}] ".format(self.TableName)
                sqlStr_where1 = " WHERE ({0}) ".format(" AND ".join(["([{0}] is not null and [{0}] != '')".format(col) #SELECT 
                                                    for col in MyColnmList] #FROM #{2} WHERE CLAUSE ignore NULL & empty
                                                    ))
                sqlStr_order1 = " ORDER BY [{0}]".format(
                                        "], [".join(MyColnmList)  , #{3} ORDER BY Clause
                                        )
                
                for nodeItem in nodesList:
                    self.logger.debug("{0} - nodeItem : {1}".format(calling_func, nodeItem))

                    
                    if IncNode is True :
                        sqlStr = "{0} {1} {2} {3}".format(sqlStr_select1, 
                                                          sqlStr_from,
                                                          "{0} {1}".format(sqlStr_where1, 
                                                                   "AND [{0}] = '{1}'".format(mn2, nodeItem)),
                                                          sqlStr_order1)
                    else :
                        sqlStr = "{0} {1} {2} {3}".format(sqlStr_select1, 
                                                          sqlStr_from,
                                                          sqlStr_where1,
                                                          sqlStr_order1)
                    self.logger.debug("{0} - sqlStr : {1}".format(calling_func, sqlStr))
                    curSQLite = self.myMemTable.cursor()
                    curSQLite.execute(sqlStr)

                    #save data into memingroup table
                    rows = curSQLite.fetchall()
                    #now chunk with MyInc
                    rowList = self.myUtils.chunkAList(rows, MyInc)
                    for rowGrpItemIdx, rowGrpItem in enumerate(rowList):
                        #"(\n{0}{1}\n)".format(" " * (len(MyOr)-1), MyOr.join(rowList))
                        #rowGrp = "\n,".join([rowItem[0] # SELECT first column
                        #                                        for rowItem in rowGrpItem])
                        rowGrp = "(\n{0}{1}\n)".format(" " * (len(MyOr)-1), MyOr.join([rowItem[0] # SELECT first column
                                                                                        for rowItem in rowGrpItem]
                                                                                        ))
                        #rowList[rowGrpItemIdx] = ["\n{0}\n)".format(rowGrp), nodeItem, MyInc]
                        rowList[rowGrpItemIdx] = ["\n{0}\n".format(rowGrp), nodeItem, MyInc]

                    #now insert into memingroup
                    curSQLite.executemany(sqlstr_insert, rowList)
                #END : for nodeItem in nodesList

                #if debug logging enabled log the memingroup content
                if self.logger.isEnabledFor(10) == True :
                    sqlstr_selMem = "SELECT * from [{0}]".format(self.MEM_IN_GROUP_TBL_NAME)
                    self.logger.debug("{0} - sqlstr_selMem : {1}".format(calling_func, sqlstr_selMem))
                    curSQLite.execute(sqlstr_selMem)
                
                    for rowsInMeminGrp in curSQLite.fetchall() :
                        #self.logger.debug("{0} - rowsInMeminGrp : {1}, {2}, {3}".format(calling_func, rowsInMeminGrp["Key"], rowsInMeminGrp["Node"], rowsInMeminGrp["MyInc"]))
                        self.logger.debug("{0} - rowsInMeminGrp : {1}, {2}, {3}".format(calling_func, rowsInMeminGrp[0], rowsInMeminGrp[1], rowsInMeminGrp[2]))

                del curSQLite
                return "<<<spf-$item$-list>>>"
            else:
                sqlStr = """SELECT DISTINCT \"(\" || {0} || \")\" as [Key] 
                from [{1}] 
                where ({2})
                order by [{3}]""".format(
                                        " || ' {0} ' || ".format("AND" if UseOR is False else "OR").join(converter(idx, item) for idx, item in enumerate(MyColnmList)),
                                        self.TableName,           #{1}
                                        " AND ".join(["([{0}] is not null and [{0}] != '')".format(col) #SELECT 
                                                    for col in MyColnmList] #FROM #{2} WHERE CLAUSE ignore NULL & empty
                                                    ),
                                        "], [".join(MyColnmList)  , #{3} ORDER BY Clause
                                        )
                                        
                self.logger.debug("{0} - sqlStr : {1}".format(calling_func, sqlStr))

                curSQLite = self.myMemTable.cursor()
                curSQLite.execute(sqlStr)
           
                #rowList = [rowItem["Key"] for rowItem in curSQLite.fetchall()]
                rowList = [rowItem[0] for rowItem in curSQLite.fetchall()]
                del curSQLite
                del sqlStr

                return "(\n{0}{1}\n)".format(" " * (len(MyOr)-1), MyOr.join(rowList))
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
    #END : def Process_Get_CSV_List2_Other

    def Create_SQL_In_Like_List(self, l_Command1, MyInc, IncNode, NodeNo, l_Operator,
                                l_ESC, l_Alias, IsCharDT, l_CSV, MyCol, mn2,
                                myDataType, ReverseLike=False) :
        """
        '=================================
        'Create SQL for In/Like Temp/Group
        '
        'ARGS:
        '----
        'SQOut     : File to Process -- Dropped
        'MemInGrp  : (In/Out) Memtbl w/ inc itemlist -- Dropped
        'l_Command1: (In/Out) SQL to generate
        'MyInc     : Inc item list to query or -1 if ~ set
        'IncNode   : Y|Y2 if Nodes should be mapped in MemInGrp
        'NodeNo    : Col No Containing MapNode or MapNodes2. -1 if Error
        'l_Operator: Filter Operator (In,Like)
        'MyDT0     : Data type start char (e.g., ') -- replaced with myDataType
        'MyDT1     : Data type End char (e.g., ') -- replaced with myDataType
        'l_ESC     : escape Clause for Like filter
        'l_Alias   : Alias and col (e.g., a0.program) in SQL Filter
        'IsCharDT  : True (Y) for a char col
        'l_CSV     : CSV File
        'MyCol     : Col# for filter col
        'mn2       : Hdr of Map_Nodes col in Src file
        'DLM       :File Delimiter ("#@$%!") -- Dropped
        'ReverseLike : Reverse Like Temp/Group
        '
        'NEW INPUT ARGS :
        ' myDataType -- D/N for date datatype or num datatype
        '=================================
        Note: Create_In_Like_List is merged into this in py 
        """
        calling_func = self.myUtils.getCallingFuncName()
        def converter(item) :
            """
                nested function of Create_SQL_In_Like_List...to handle Datatype of column + embed colName & operator
            """

            output = ""
            MyDT = myDataType
            myAlias = l_Alias
            if MyDT == "D" : #'Date Data Type
                output = "\"to_date('\" || {0} || \"','YYYY-MM-DD hh24:mi:ss')\"".format(item)
            elif MyDT == "N" : #'Numeric Data Type
                output = "{0}".format(item)
            else : #'Character Data Type
                output = "\"'\" || {0} || \"'\"".format(item)

            return output
        #END : def converter

        #start of parent function Create_SQL_In_Like_List
        #local variables
        nodesList = ['']
        try :
            self.logger.debug("{0} - l_Command1 : {1}".format(calling_func, l_Command1)) 
            self.logger.debug("{0} - MyInc : {1}".format(calling_func, MyInc)) 
            self.logger.debug("{0} - IncNode : {1}".format(calling_func, IncNode)) 
            self.logger.debug("{0} - NodeNo : {1}".format(calling_func, NodeNo)) 
            self.logger.debug("{0} - l_Operator : {1}".format(calling_func, l_Operator)) 
            self.logger.debug("{0} - l_ESC : {1}".format(calling_func, l_ESC)) 
            self.logger.debug("{0} - l_Alias : {1}".format(calling_func, l_Alias)) 
            self.logger.debug("{0} - IsCharDT : {1}".format(calling_func, IsCharDT)) 
            self.logger.debug("{0} - l_CSV : {1}".format(calling_func, l_CSV)) 
            self.logger.debug("{0} - MyCol : {1}".format(calling_func, MyCol)) 
            self.logger.debug("{0} - mn2 : {1}".format(calling_func, mn2)) 
            self.logger.debug("{0} - myDataType : {1}".format(calling_func, myDataType)) 
            self.logger.debug("{0} - TableName : {1}".format(calling_func, self.TableName))
            self.logger.debug("{0} - ReverseLike : {1}".format(calling_func, ReverseLike))
            
            
            if NodeNo == -1 :
                errMsg = "You chose node \"{0}\" but do not have a column named {0} in the sub-query\ncontaining nodes to query".format(mn2)
                l_Command1 = self.Set_Empty_In_Temp(l_Operator, IsCharDT)
                raise Exception(errMsg)
            
            if MyInc > 0 : #'Inc req
                self.logger.debug("{0} - MyInc > 0 block".format(calling_func))

                if IncNode is True : # include node information
                    #get distinct nodes ordered ASC
                    sqlStr_SelDistNodes = "SELECT DISTINCT [{0}] AS [{0}] FROM [{1}] ORDER BY [{0}] ASC".format(mn2.strip("[]"), self.TableName)

                    curSQLite = self.myMemTable.cursor()
                    curSQLite.execute(sqlStr_SelDistNodes)
                    nodesList = [rowItem[0] for rowItem in curSQLite.fetchall()]
                    del curSQLite

                    self.logger.debug("{0} - IncNode nodesList : {1}".format(calling_func, nodesList))
                    if len(nodesList) == 0 :
                        errMsg = "No rows were found in {0}\nAn empty item list will be returned...".format(l_csv)
                        l_Command1 = self.Set_Empty_In_Temp(l_Operator, IsCharDT)
                        raise Exception(errMsg)
                #END : if IncNode is True :

                sqlstr_drop = "DROP TABLE IF EXISTS [{0}]".format(self.MEM_IN_GROUP_TBL_NAME)
                sqlstr_create = "CREATE TABLE IF NOT EXISTS [{0}] ([Key], [Node], [MyInc])".format(self.MEM_IN_GROUP_TBL_NAME)
                sqlstr_insert = "INSERT INTO [{0}] ([Key], [Node], [MyInc]) values (?, ?, ?)".format(self.MEM_IN_GROUP_TBL_NAME)
                curSQLite = self.myMemTable.cursor()
                self.logger.debug("{0} - sqlstr_drop : {1}".format(calling_func, sqlstr_drop))
                curSQLite.execute(sqlstr_drop)
                
                self.logger.debug("{0} - sqlstr_create : {1}".format(calling_func, sqlstr_create))
                curSQLite.execute(sqlstr_create)
                
                self.logger.debug("{0} - sqlstr_insert : {1}".format(calling_func, sqlstr_insert))

                sqlStr_from = "FROM (SELECT DISTINCT TRIM({0}, \"\'\") AS {0} FROM [{1}] ".format(MyCol, self.TableName)
                
                sqlStr_where1 = "WHERE ({0} IS NOT null AND {0} NOT IN ('', '%') ) ".format(MyCol)
                sqlStr_order1 = "ORDER BY {0} ASC)".format(MyCol)
                #sqlstr_tmp_drop = "DROP TABLE IF EXISTS temp"
                sqlStr_List = []

                for nodeItem in nodesList :
                    self.logger.debug("{0} - nodeItem : {1}".format(calling_func, nodeItem))

                    #sqlStr_SelNodeData = ""
                    if IncNode is True :
                        sqlStr_where2 = "AND [{0}] = '{1}'".format(mn2, nodeItem)
                        sqlStr_from1 = "{0}{1}{2}{3}".format(sqlStr_from, sqlStr_where1, sqlStr_where2, sqlStr_order1)
                    else :
                        sqlStr_from1 = "{0}{1}{2}".format(sqlStr_from, sqlStr_where1, sqlStr_order1)

                    #sqlStr_from1 = "FROM (SELECT DISTINCT TRIM({0}, \" \'\") AS {0} FROM [{1}] WHERE ({0} IS NOT null AND {0} != '') AND [{2}] = '{3}' ORDER BY {0} ASC)".format(MyCol, self.TableName, mn2, nodeItem)
                    self.logger.debug("{0} - sqlStr_from1 : {1}".format(calling_func, sqlStr_from1))
                    if l_Operator == "IN" :
                        sqlStr_select1 = "SELECT {0} AS [Key]".format(converter(MyCol))
                        sqlStr = "{0} {1}".format(sqlStr_select1, sqlStr_from1)
                        
                        sqlStr_List.append(sqlStr)

                        self.logger.debug("{0} - IN sqlStr : {1}".format(calling_func, sqlStr))
                    elif l_Operator == "LIKE" :
                        #sqlstr_tmp = ("CREATE TEMP TABLE temp AS SELECT * {0}").format(sqlStr_from1)

                        #updated for ReversLike
                        #updated for PrepLikeValue
                        sqlStr_select1 = ("SELECT "
                                          " CASE WHEN '{1}' = 'False' " #LIKE
                                              " THEN \"'\" || SPFPrepLikeValue(trim({0})) || \"\' {4}\" "
                                            " ELSE \"\'\" || {0} || \"\' {2} {3} {4}\" " # ReverseLike -- contruct the full clause
                                            " END AS [Key]").format(MyCol, ReverseLike, l_Operator, l_Alias, l_ESC)

                        sqlStr = "{0} {1}".format(sqlStr_select1, sqlStr_from1)
                        #sqlStr = "{0} FROM temp".format(sqlStr_select1)
                        self.logger.debug("{0} - LIKE sqlstr : {1}".format(calling_func, sqlStr))
                        #self.logger.debug("{0} - LIKE sqlStr_frm_tmp : {1}".format(calling_func, sqlStr_frm_tmp))
                    elif re.match(r"'\$IN'", l_Operator, re.IGNORECASE) : #'===== v30.74 ==========
                        sqlStr_select1 = "SELECT {0} AS [Key]".format(converter(MyCol))
                        sqlStr = "{0} {1}".format(sqlStr_select1, sqlStr_from1)
                        
                        sqlStr_List.append(sqlStr)

                        self.logger.debug("{0} - $IN sqlStr : {1}".format(calling_func, sqlStr))
                        #raise Exception("$IN not implemented")
                    elif re.match(r"'\$REGEX'", l_Operator, re.IGNORECASE) : #'===== v30.74 ==========
                        raise Exception("$REGEX not implemented for Inc req")
                        #sqlStr_select1 = ("SELECT "
                        #                  " CASE WHEN INSTR({0}, '_') = 0 "
                        #                    " AND INSTR({0}, '%') = 0 "
                        #                    " AND '{1}' = 'False' "
                        #                    " THEN CASE WHEN ROWID = 1 THEN \"'\" || {0} || \"%\' {4}\" ELSE \"{3} {2} '\" || {0} || \"%\' {4}\" END"
                        #                    " ELSE \"\'\" || {0} || \"\' {2} {3} {4}\" "
                        #                    " END AS [Key]").format(MyCol, ReverseLike, l_Operator, l_Alias, l_ESC)


                        

                    curSQLite = self.myMemTable.cursor()
                    #curSQLite.execute(sqlstr_tmp_drop)
                    #curSQLite.execute(sqlstr_tmp)
                    curSQLite.execute(sqlStr)
                    
                    rowList = []
                    rows = curSQLite.fetchall()
                    #now chunk with MyInc
                    rowList = self.myUtils.chunkAList(rows, MyInc)
                    #now build required clauses
                    if l_Operator == "IN" :
                        #concat the items in each group with "\n,"
                        for rowGrpItemIdx, rowGrpItem in enumerate(rowList) :
                            rowGrp = "\n,".join([rowItem[0] # SELECT first column
                                                                  for rowItem in rowGrpItem])
                            rowList[rowGrpItemIdx] = ["\n({0}\n))".format(rowGrp), nodeItem, MyInc]
                    elif l_Operator == "LIKE" :
                        #concat the items in each group with required format
                        for rowGrpItemIdx, rowGrpItem in enumerate(rowList) :
                            
                            if ReverseLike is False :
                                rowGrp = "{0}\n)".format("\n OR".join([" {0} ".format(rowItem[0]) if Idx == 0 else " {2} {1} {0} ".format(rowItem[0], l_Operator, l_Alias)
                                                                  for Idx, rowItem in enumerate(rowGrpItem)]))
                            else : # ReverseLike is True
                                rowGrp = "{0}\n)".format("\n OR".join([" {0} ".format(rowItem[0])
                                                                  for Idx, rowItem in enumerate(rowGrpItem)]))

                            rowList[rowGrpItemIdx] = [rowGrp, nodeItem, MyInc]
                    elif re.match(r"'\$IN'", l_Operator, re.IGNORECASE) : #'===== v30.74 ==========
                        #concat the items in each group with "\n,"
                        for rowGrpItemIdx, rowGrpItem in enumerate(rowList) :
                            rowGrp = "\n    ,".join([rowItem[0] # SELECT first column
                                                                  for rowItem in rowGrpItem])
                            rowList[rowGrpItemIdx] = ["\n      {0}".format(rowGrp), nodeItem, MyInc]
                        #raise Exception("$IN not implemented")
                    elif re.match(r"'\$REGEX'", l_Operator, re.IGNORECASE) : #'===== v30.74 ==========
                        raise Exception("$REGEX not implemented for Inc req")

                    self.logger.debug("{0} - length of formatted rowList : {1}".format(calling_func, len(rowList)))                   
                    self.logger.debug("{0} - formatted rowList : {1}".format(calling_func, rowList))

                    #now insert into memingroup
                    curSQLite.executemany(sqlstr_insert, rowList)
                #END : for nodeItem in nodesList :

                #if debug logging enabled log the memingroup content
                if self.logger.isEnabledFor(10) == True :
                    sqlstr_selMem = "SELECT * from [{0}]".format(self.MEM_IN_GROUP_TBL_NAME)
                    self.logger.debug("{0} - sqlstr_selMem : {1}".format(calling_func, sqlstr_selMem))
                    curSQLite.execute(sqlstr_selMem)
                
                    for rowsInMeminGrp in curSQLite.fetchall() :
                        #self.logger.debug("{0} - rowsInMeminGrp : Key : {1}, Node : {2}, MyInc : {3}".format(calling_func, rowsInMeminGrp["Key"], rowsInMeminGrp["Node"], rowsInMeminGrp["MyInc"])) -- requires SQLite3 RowFactory set
                        self.logger.debug("{0} - rowsInMeminGrp : Key : {1}, Node : {2}, MyInc : {3}".format(calling_func, rowsInMeminGrp[0], rowsInMeminGrp[1], rowsInMeminGrp[2]))

                #curSQLite.execute(sqlstr_tmp_drop)
                del curSQLite #cleanup 

                l_Command1 = "<<<spf-$item$-list>>>\n"
            else : # 'Normal (MyInc<=0)
                self.logger.debug("{0} - Normal (MyInc<=0) block".format(calling_func))
                sqlStr = "SELECT {2} AS {0} FROM (SELECT DISTINCT RTRIM({0}, \" \'\") AS {0} FROM [{1}] WHERE {0} IS NOT null AND {0} NOT IN ('', '%') ORDER BY {0})".format(MyCol, self.TableName, converter(MyCol))
                self.logger.debug("{0} - IN sqlStr : {1}".format(calling_func, sqlStr))
                #execute sqlStr to get output
                curSQLite = self.myMemTable.cursor()
                curSQLite.execute(sqlStr)

                rows = curSQLite.fetchall()
                self.logger.debug("{0} - len rows : {1}".format(calling_func, len(rows)))
                if len(rows) == 0:
                    errMsg = "No column data values found in {0}\nAn empty item list will be returned...".format(l_CSV)
                    raise SPFNothingToProcessException(errMsg)
                if l_Operator == "IN" :
                    #continue with same 'rows' object created above
                    #now chunk with MyInc = 999
                    MyInc = 999
                    rowList = []
                    rowList = self.myUtils.chunkAList(rows, MyInc)
                    self.logger.debug("{0} - len rowList : {1}".format(calling_func, len(rowList)))

                    #a = (") OR {0}".join([for rowsItem in rowList]))
                    for rowGrpItemIdx, rowGrpItem in enumerate(rowList) :
                        rowGrp = "\n,".join([rowItem[0] # SELECT first column
                                                                for rowItem in rowGrpItem])
                        rowList[rowGrpItemIdx] = ["\n{0}\n".format(rowGrp)]
                    self.logger.debug("{0} - len rowList : {1}".format(calling_func, len(rowList)))
                    self.logger.debug("{0} - rowList : {1}".format(calling_func, rowList))

                    tmp_inc = ") OR {0} {1} (".format(l_Alias, l_Operator).join([rowItem[0] # SELECT first column 
                                                                                                    for rowItem in rowList])
                    
                    l_Command1 = "({0}))".format(tmp_inc)
                    #l_Command1 = "({0}) \n)".format("\n,".join([rowItem[0] # SELECT first column
                    #                                          for rowItem in rowList]))
                    del rowList
                elif l_Operator == "LIKE" :
                    #updated for ReversLike
                    sqlstr_tmp_drop = "DROP TABLE IF EXISTS temp"
                    sqlstr_tmp = ("CREATE TEMP TABLE temp AS SELECT "
                                    "DISTINCT TRIM({0}, \"\'\") AS {0} "
                                    "FROM [{5}] "
                                    "WHERE {0} IS NOT null "
                                    "AND {0} NOT IN ('', '%') ORDER BY {0}").format(MyCol, ReverseLike, l_Operator, l_Alias, l_ESC, self.TableName)

                    sqlStr = ("SELECT "
                                " CASE WHEN '{1}' = 'False' " #LIKE
                                    " THEN CASE WHEN ROWID = 1 "
                                        " THEN \"'\" || SPFPrepLikeValue(trim({0})) || \"' {4}\" " # rowID = 1...first like exists in the SQL query from SQLPF
                                        " ELSE \"{3} {2} '\" || SPFPrepLikeValue(trim({0})) || \"' {4}\" " # rowID > 1...embed like clause
                                        " END"
                                " ELSE \"\'\" || {0} || \"\' {2} {3} {4}\" " # ReverseLike -- contruct the full clause
                                " END FROM temp").format(MyCol, ReverseLike, l_Operator, l_Alias, l_ESC, self.TableName)
                    
                    self.logger.debug("{0} - LIKE sqlstr : {1}".format(calling_func, sqlStr))
                    #execute sqlStr to get output
                    curSQLite = self.myMemTable.cursor()
                    curSQLite.execute(sqlstr_tmp_drop)
                    curSQLite.execute(sqlstr_tmp)
                    curSQLite.execute(sqlStr)
                    
                    rows = curSQLite.fetchall()
                    self.logger.debug("{0} - LIKE rows : {1}".format(calling_func, rows))
                    self.logger.debug("{0} - LIKE len(rows) : {1}".format(calling_func, len(rows)))

                    #construct the LIKE clause output
                    # if Idx == 0 else " {0}".format(row[0])
                    l_Command1 = "{0}\n)".format("\n OR".join([" {0} ".format(row[0])  #SELECT
                                                               for Idx, row in enumerate(rows) #FROM
                                                              ]))
                    curSQLite.execute(sqlstr_tmp_drop)
                    del curSQLite #cleanup 
                elif re.match(r"'\$IN'", l_Operator, re.IGNORECASE) : #'===== v30.74 ==========
                    sqlStr = "SELECT {2} AS {0} FROM (SELECT DISTINCT TRIM({0}, \" \'\") AS {0} FROM [{1}] WHERE {0} IS NOT null AND {0} != '' ORDER BY {0})".format(MyCol, self.TableName, converter(MyCol))
                    self.logger.debug("{0} - IN sqlStr : {1}".format(calling_func, sqlStr))
                    #execute sqlStr to get output
                    curSQLite = self.myMemTable.cursor()
                    curSQLite.execute(sqlStr)

                    rowList = curSQLite.fetchall()
                    self.logger.debug("{0} - len rowList : {1}".format(calling_func, len(rowList)))

                    l_Command1 = "    {0}".format("\n   ,".join([rowItem[0] # SELECT first column
                                                              for rowItem in rowList]))
                    self.logger.debug("{0} - l_Command1 : {1}".format(calling_func, l_Command1))
                    del rowList
                elif re.match(r"'\$REGEX'", l_Operator, re.IGNORECASE) : #'===== v30.74 ==========
                    sqlStr = "SELECT {2} AS {0} FROM (SELECT DISTINCT TRIM({0}, \" \'\") AS {0} FROM [{1}] WHERE {0} IS NOT null AND {0} != '' ORDER BY {0})".format(MyCol, self.TableName, converter(MyCol))
                    self.logger.debug("{0} - IN sqlStr : {1}".format(calling_func, sqlStr))
                    #execute sqlStr to get output
                    curSQLite = self.myMemTable.cursor()
                    curSQLite.execute(sqlStr)

                    rowList = curSQLite.fetchall()
                    
                    self.logger.debug("{0} - len rowList : {1}".format(calling_func, len(rowList)))
                    l_Command1 = "    {0}".format("\n   ,".join(["{{ '{0}' : {{ '$regex' : {1}}}}}".format(l_Alias, rowItem[0]) # SELECT first column
                                                              for rowItem in rowList]))
                    #if IsCharDT is True :
                    #    l_Command1 = "    {0}".format("\n   ,".join(["{{'{0}' : {{ '$regex' : '{1}'}}}}".format(l_Alias, rowItem[0]) # SELECT first column
                    #                                          for rowItem in rowList]))
                    #else : #numeric. No Single Quote
                    #    l_Command1 = "    {0}".format("\n   ,".join(["{{'{0}' : {{ '$regex' : {1}}}}}".format(l_Alias, rowItem[0]) # SELECT first column
                    #                                          for rowItem in rowList]))
                    self.logger.debug("{0} - l_Command1 : {1}".format(calling_func, l_Command1))
                    del rowList
                    #raise Exception("$REGEX not implemented")
            #END : if MyInc > 0 : else :
            if l_Command1.find("<$!>") != -1:
                l_Command1=l_Command1.replace('<$!>','"')
                #print(l_Command1)
            return l_Command1
        except SPFNothingToProcessException as SPFNTPerr:
            raise
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    #END : def Create_SQL_In_Like_List

    def LoadMemInGrpTableFromFile(self, FileName=None, sourceDataFileHasHeaders=True) : 
        """
        'Load data from a file
        '(input) : 
        '----
        'FileName  : delimited-data-file name with path, which needs to be loaded to MemTable table
        'Note: ensure that MemTable instance is created with correct values for tableName & colNamesList 
        ' sourceDataFileHasHeaders : Indicates whether source file has header or not
        '(output) : 
        '----
        'rowCount
        """

        """
        re-route call with updated input
        """
        #locals
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        try : 
            rowCount = self.LoadFromFile(fileName=FileName, tableName=self.MEM_IN_GROUP_TBL_NAME, sourceDataFileHasHeaders=sourceDataFileHasHeaders)
        except Exception as err:
            self.logger.exception("{0} -{1}".format(calling_func, err.args[0]))
            raise

    def LoadFromFile(self, fileName=None, tableName=None, colNamesList=None, colNamesListWithDT=None, 
                     sourceDataFileHasHeaders=True, EANImport=True, EANImportDefaultDict=None, conObj=None, 
                     fileDLM=None, defaultColValueIfEmpty=None, RowIdStartAt2=False, displayColumnMismatchWarnings=True, encoding=None):
        def readerChunks(reader, chunkSize=20000):
            """
            iterate thru the reader and yield chunks
            """
            rowChunk = []
            for idx, row in enumerate(reader):
                try :
                    if (idx % chunkSize == 0 and idx > 0):
                        yield rowChunk
                        rowChunk = []
                    rowChunk.append(row)
                except Exception as err:
                    self.logger.exception("{0} - processing row :{1} ->  : {2}\nerror : {3}".format(calling_func, rowCnt, row, err.args[0]))
                    raise
            yield rowChunk
        """
        '(input) : 
        '----
        '1.) fileName  : delimited-data-file name with path, which needs to be loaded to MemTable
        '2.) tableName : SQLite3 table name which will store data
        '3.) colNamesList : List of column names that will be used to create the table. 
        '               If None then headers will be determined from file
        '4.) colNamesListWithDT : List of column names with SQLite DT specified...will be used as is
        '5.) sourceDataFileHasHeaders : booleanType : Flag to indicate if data file has headers 
        '6.) EANImport : booleanType : Empty-As-Null import
        '7.) EANImportDefaultDict : dictionaryType : Dictionary of default values for each column
        '8.) ConObj : SQLite3 connection object to attached DB
        '9.) fileDLM : specific delimiter -- overide deciphering based on file extension
        10.) defaultColValueIfEmpty : None - default EANImport value based on EANImport flag value or explicit value --> 'None' will insert NULL if column value is empty
        11.) RowIdStartAt2 : (Default: False) -- if True start Rowid at 2, if False start Rowid at 1
        12.) displayColumnMismatchWarnings : (Default : True) -- if True display the Column mismatch warning messages to the console 
        13.) encoding : encoding to use while reading from file
        '(output) : 
        '----
        'rowCount
        """
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        loadStatus = False

        fileNameWithPath = None
        fileNameWithExt = None
        fileNameWithoutExt = None
        fileExt = None
        createTblStmnt = None
        dropTblStmnt = None
        #tableName = None
        rowCount = 0
        #defaultColValueIfEmpty = ""
        colNamesListWithoutDT = []
        chunkSize1 = 5000 #if self.myUtils.SHisSHEntry is True else 1000000

        self.logger.debug("{0} - fileName : {1}".format(calling_func, fileName))
        self.logger.debug("{0} - tableName : {1}".format(calling_func, tableName))
        self.logger.debug("{0} - colNamesList : {1}".format(calling_func, colNamesList))
        self.logger.debug("{0} - colNamesListWithDT : {1}".format(calling_func, colNamesListWithDT))
        self.logger.debug("{0} - sourceDataFileHasHeaders : {1}".format(calling_func, sourceDataFileHasHeaders))
        self.logger.debug("{0} - EANImport : {1}".format(calling_func, EANImport))
        self.logger.debug("{0} - EANImportDefaultDict : {1}".format(calling_func, EANImportDefaultDict))
        self.logger.debug("{0} - conObj : {1}".format(calling_func, conObj))
        self.logger.debug("{0} - fileDLM : {1}".format(calling_func, fileDLM))
        self.logger.debug("{0} - defaultColValueIfEmpty : {1}".format(calling_func, defaultColValueIfEmpty))
        self.logger.debug("{0} - RowIdStartAt2 : {1}".format(calling_func, RowIdStartAt2))
        self.logger.debug("{0} - displayColumnMismatchWarnings : {1}".format(calling_func, displayColumnMismatchWarnings))
        self.logger.debug("{0} - encoding : {1}".format(calling_func, encoding))
        try:
            if self.myMemTable is None:
                raise Exception("MemTable not initialised")
            
            fileNameWithPath = fileName

            if len(fileNameWithPath) == 0 : 
                raise Exception("FileName is empty")

            if os.path.exists(fileNameWithPath) is False :
                raise Exception("File doesn't exist : {0}".format(fileNameWithPath))

            filePath, fileNameWithExt = os.path.split(fileNameWithPath)
            fileNameWithoutExt, fileExt = os.path.splitext(fileNameWithExt)
            
            if self.myUtils.IsEmptyOrNone(tableName) is True :
                tableName = fileNameWithExt
                self.logger.debug("{0} - set tableName : {1}".format(calling_func, tableName))

            tableName = re.sub(r"[^\w\.]", "_", tableName, re.IGNORECASE|re.VERBOSE)
            #set the TableName for this instance
            self.TableName = tableName            

            #get dlm
            if fileDLM is None :
                self.logger.debug("{0} - resetting fileDLM : {1}".format(calling_func, fileDLM))
                fileDLM = self.myUtils.GetFileDLM(fileNameWithExt)
                self.logger.debug("{0} - new fileDLM : {1}".format(calling_func, fileDLM))

            if colNamesList is None and colNamesListWithDT is None :
                #get headers from File
                headersListFromFile = self.myUtils.GetHeadersFromFile(fileNameWithPath, fileDLM, l_Replace=True, AFile_encoding=encoding)

                if sourceDataFileHasHeaders == False :
                    #no headers so gen pseudo cols of format colx...where x is index of column  
                    colNamesListWithoutDT = ["col{0}".format(Idx) for Idx, item #select
                                             in enumerate(headersListFromFile)] #from
                    self.logger.debug("{0} - headersLessFile colNamesListWithoutDT : {1}".format(calling_func, colNamesListWithoutDT))
                else :
                    colNamesListWithoutDT = headersListFromFile
                    self.logger.debug("{0} - headersFromFile colNamesListWithoutDT : {1}".format(calling_func, colNamesListWithoutDT))

                colNamesForCreateStmnt = "[{0}]".format("],[".join(colNamesListWithoutDT))
                
            elif colNamesList is None and colNamesListWithDT is not None :
                #get colNames without DT  
                try:
                    colNamesListWithoutDT = [re.match(r"^(?P<colName>\[.*?\])(?: |$)", 
                                                        colItem, 
                                                        re.IGNORECASE).group("colName").strip("[]") for colItem #select
                                                in colNamesListWithDT] #from
                except AttributeError as attrErr:
                    if attrErr.args[0] == "'NoneType' object has no attribute 'group'" :
                        errMsg = "Please consider checking option: 'PreProcess CSV Files' under SQLite Query Options."
                        raise Exception(errMsg)
                    raise # for any error raise it
                self.logger.debug("{0} - headersFromcolNamesListWithDT colNamesListWithoutDT : {1}".format(calling_func, colNamesListWithoutDT))
                colNamesForCreateStmnt = ",".join(colNamesListWithDT)
                
            else :  
                #colNamesList is not None....use this strip off []   
                colNamesListWithoutDT =  [colItem.strip("[]") for colItem #select
                                            in colNamesList] #from]   
                self.logger.debug("{0} - headersFromcolNamesList colNamesListWithoutDT : {1}".format(calling_func, colNamesListWithoutDT))
                colNamesForCreateStmnt = "[{0}]".format("],[".join(colNamesListWithoutDT))
            #END : if colNamesList is None and colNamesListWithDT is None :

            #check if reserved column names exist in user file
            errMsg = "Error while loading file : {fileNameWithPath}\n'rowid', '_rowid_' and 'oid' are reserved column names. \nPlease rename column(s): {reservedColsFoundList} \n\tin file : {fileNameWithPath}"
            reservedColsFoundList = self.myUtils.getMatchingValListFrmList(self.reservedColNames, colNamesListWithoutDT)
            self.logger.debug("{0} - reservedColsFoundList : {1}".format(calling_func, reservedColsFoundList))
            if len(reservedColsFoundList) > 0:
                #found reserved column names...raise error
                raise Exception(errMsg.format(reservedColsFoundList=", ".join(reservedColsFoundList),
                                              fileNameWithPath=fileNameWithPath))

            #create colnNames for insert stmnt
            colNamesForInsertStmnt = "[{0}]".format("],[".join(colNamesListWithoutDT))
            self.logger.debug("{0} - colNamesForCreateStmnt : {1}".format(calling_func, colNamesForCreateStmnt))
            self.logger.debug("{0} - colNamesForInsertStmnt : {1}".format(calling_func, colNamesForInsertStmnt))             
            
            dropTblStmnt = "DROP TABLE IF EXISTS [{0}]".format(tableName)
            self.logger.debug("{0} - dropTblStmnt : '{1}'".format(calling_func, dropTblStmnt))
            createTblStmnt = "CREATE TABLE IF NOT EXISTS [{0}] ({1})".format(tableName, colNamesForCreateStmnt)                
            self.logger.debug("{0} - createTblStmnt : '{1}'".format(calling_func, createTblStmnt))
            deleteRowsStmnt = "DELETE FROM [{0}]".format(tableName)
            self.logger.debug("{0} - deleteRowsStmnt : '{1}'".format(calling_func, deleteRowsStmnt))
            if conObj is None :
                curSQLite = self.myMemTable.cursor()
                self.logger.debug("{0} - Cursor created from internal memTable Connection".format(calling_func))
                self.tbl_ColNames_Dict[tableName] = colNamesListWithoutDT #add to tbl_cols dict....refered in getColumnsForTable
            else : 
                curSQLite = conObj.cursor()
                self.logger.debug("{0} - Cursor created from conObj parameter".format(calling_func))

            #IMPORTANT : start explicit transaction...Py SQLite STARTS-ENDS a transaction for every DML statement -- performance hog for bulk loading
            curSQLite.execute("BEGIN TRANSACTION") #need to explicitly commit on the Con object --> myConObj.commit()

            #run DDL statements
            curSQLite.execute(dropTblStmnt) # drop the table
            curSQLite.execute(createTblStmnt)
            curSQLite.execute(deleteRowsStmnt)
            if conObj is None:
                self.myMemTable.commit()
            else:
                conObj.commit()

            #start loading from file
            if encoding in [None, ""]:
                encoding = self.myUtils.detectFileEncoding(fileNameWithPath, readall=True, mode="r")
            # with open(fileNameWithPath,'r') as fileToLoad: #, encoding=encoding, errors='replace') as fileToLoad:
            with open(fileNameWithPath,'rt', encoding=encoding, errors='replace') as fileToLoad:
            # with open(fileNameWithPath,'rb') if isPYTHON2 else open(fileNameWithPath,'rt', encoding=self.myUtils.detectFileEncoding(fileNameWithPath), errors='replace') as fileToLoad: #, encoding=sys.getfilesystemencoding(), self.myUtils.gOSDefaultEncoding , encoding=sys.getfilesystemencoding()
            #with open(fileNameWithPath,'rb') if isPYTHON2 else open(fileNameWithPath,'rt', encoding=sys.getfilesystemencoding()) as fileToLoad: #, encoding=sys.getfilesystemencoding(), self.myUtils.gOSDefaultEncoding , encoding=sys.getfilesystemencoding()
            #with open(fileNameWithPath,'r') as fileToLoad: #, encoding=sys.getfilesystemencoding(), self.myUtils.gOSDefaultEncoding , encoding=sys.getfilesystemencoding()
                #region : increase column read size of csv reader
                import ctypes as ct
                l1 = csv.field_size_limit(int(ct.c_ulong(-1).value // 2))
                l2 = csv.field_size_limit()
                #print(f"l1 : {l1} ; l2 : {l2}")
                #endregion : increase column read size of csv reader
                myReader = csv.reader(fileToLoad, delimiter=fileDLM, skipinitialspace=False)
                    
                firstRow = next(myReader) #read the first row -- to get headers or construct pseudo headers
                self.logger.debug("{0} - firstRow : '{1}'".format(calling_func, firstRow))

                insertStmnt = "INSERT INTO [{0}] ({1}) values ({2})".format(tableName,
                                                                                colNamesForInsertStmnt,
                                                                                ", ".join(["?"] * len(firstRow)))

                self.logger.debug("{0} - insertStmnt : '{1}'".format(calling_func, insertStmnt))
                                
                row = None
                rowCnt = 0
                row1 = None
                    
                if sourceDataFileHasHeaders is False or RowIdStartAt2 is True:
                    #means that file doesn't have headers so insert firstrow as data
                    #OR : Rowid needs to start at 2 needed for compatibility with VA -- insert header row...will be deleted later to set Rowid = 2 like in VA                        
                    rowCnt = rowCnt + 1
                    curSQLite.execute(insertStmnt, firstRow)
                    self.logger.debug("{0} - executed headerRow insert : '{1}'".format(calling_func, firstRow))

                #insert rest of the rows
                chunkCount = 0
                lessColsThanExpectedCount = 0
                lessColsThanExpected = []
                moreColsThanExpectedCount = 0
                moreColsThanExpected = []
                lenColsExpected = len(colNamesListWithoutDT)
                    
                for  rowChunk in  readerChunks(myReader, chunkSize=chunkSize1):
                    chunkCount = chunkCount + 1
                    curSQLite.execute("BEGIN TRANSACTION")
                    try:
                        curSQLite.executemany(insertStmnt, rowChunk)
                        rowCnt = rowCnt + len(rowChunk)
                    except sqlite3.ProgrammingError as ProgErr:
                        if ProgErr.args[0].startswith("Incorrect number of bindings supplied"):
                            curSQLite.execute("ROLLBACK")
                            rowChunk_tmp = []
                            idx = 0
                            while len(rowChunk) > 0:
                                idx = idx + 1
                                row = rowChunk.pop(0)
                                rowCnt = rowCnt + 1
                                lenRow = len(row)
                                self.logger.debug("{0} -idx : {1}; lenRow : '{2}' ; rowCnt : {3}".format(calling_func, idx, lenRow, rowCnt))
                                if lenRow != lenColsExpected:
                                    if lenRow < lenColsExpected and lenRow > 0:
                                        rowChunk_tmp.append(row + ([None] * (lenColsExpected - lenRow))) 
                                        #self.logger.warn("{0} - expected {1} columns but found {2} - filling the rest with NULL".format(calling_func, lenColsExpected, lenRow))
                                        if lessColsThanExpectedCount == 0: #just add the first row #...as this might cause memory issue if there many records
                                            lessColsThanExpected.append(rowCnt) # = lessColsThanExpected + 1
                                        lessColsThanExpectedCount = lessColsThanExpectedCount + 1
                                    elif lenRow > lenColsExpected:
                                        rowChunk_tmp.append(row[:lenColsExpected])
                                        #self.logger.warn("{0} - expected {1} columns but found {2} - extras ignored".format(calling_func, lenColsExpected, lenRow))
                                        if moreColsThanExpectedCount == 0 :#just add the first row #...as this might cause memory issue if there many records
                                            moreColsThanExpected.append(rowCnt) # = moreColsThanExpected + 1
                                        moreColsThanExpectedCount = moreColsThanExpectedCount + 1
                                else:
                                    rowChunk_tmp.append(row) #row is all good, just add and continue
                                #END : if lenRow != lenColsExpected:
                            #END : while len(rowChunk) > 0:
                            self.logger.debug("{0} - after blanks del len(rowChunk_tmp) : '{1}'".format(calling_func, len(rowChunk_tmp)))
                            #now process the updated rowChunk
                            curSQLite.executemany(insertStmnt, rowChunk_tmp) 
                            del rowChunk_tmp
                        #END : if ProgErr.message.startswith("Incorrect number of bindings supplied"):
                    #END: try block
                    if conObj is None:
                        self.myMemTable.commit()
                    else:
                        conObj.commit()
                #END : for  rowChunk in  readerChunks(myReader, chunkSize=chunkSize1):
                if EANImport is True: # update empty values as provided in defaultColValueIfEmpty
                    #updateStmnt_sql = "Update [{tableName}] set [{columnName}] = {columnValue} where [{columnName}] = '';".format(defaultColValueIfEmpty)
                    for colItem in colNamesListWithoutDT:
                        updateStmnt = "Update [{tableName}] set [{columnName}] = ? where [{columnName}] = '';".format(columnName=colItem, tableName=tableName)
                        self.logger.debug("{0} - executing updateStmnt : '{1}'".format(calling_func, updateStmnt))
                        curSQLite.execute(updateStmnt, (defaultColValueIfEmpty,))
                        
                self.logger.debug("{0} - processed total chunks : {1} --> Cummulative inserted rows : {2}".format(calling_func, chunkCount, rowCnt))
            #END : with open(fileNameWithPath,'rUb') as fileToLoad

            if displayColumnMismatchWarnings is True:
                if len(lessColsThanExpected) > 0:
                    #self.logger.warn("{0} - expected {1} columns but found less columns in {2} rows\n filled the rest with NULL...continue".format(calling_func, lenColsExpected, lessColsThanExpected))
                    self.myUtils.Console("WARNING : expected {1} columns but found less columns in {2} rows, starting at row {3} of table {0} \n - filled the rest with NULL...continue".format(fileName, lenColsExpected, lessColsThanExpectedCount, lessColsThanExpected))
                if len(moreColsThanExpected) > 0:
                    #self.logger.warn("{0} - expected {1} columns but found more columns in rows: {2}\n - extras ignored...continue".format(fileName, lenColsExpected, moreColsThanExpected))
                    self.myUtils.Console("WARNING : expected {1} columns but found more columns in {2} rows, starting at row {3} of table {0} \n - extras ignored...continue".format(fileName, lenColsExpected,moreColsThanExpectedCount, moreColsThanExpected))
            
            if sourceDataFileHasHeaders is False or RowIdStartAt2 is True: #needed for compatibility with VA -- header row...will be deleted to set Rowid = 2 like in VA
                deleteHeaderRow_SQL = "DELETE FROM [{0}] WHERE rowid = 1".format(tableName)
                curSQLite.execute(deleteHeaderRow_SQL)
                self.logger.debug("{0} - executed deleteHeaderRow_SQL : '{1}'".format(calling_func, deleteHeaderRow_SQL))
            #close the cursor
            curSQLite.close()
            curSQLite = None
            rowCount = myReader.line_num - 1
            self.logger.debug("{0} - Records inserted : {1}".format(calling_func, rowCount))
            if conObj is not None :
                conObj.commit() #commit the data if this is referring to an external SQLite DB 
                self.logger.debug("{0} - Commit Done - conObj".format(calling_func))
            else:
                self.myMemTable.commit() #commit the data in memTable
                self.logger.debug("{0} - Commit Done - myMemTable".format(calling_func))
            return rowCount
        except Exception as err:
            self.logger.exception("{0} -{1}".format(calling_func, err.args[0]))
            raise

    def LoadDF(self, dfToLoad=None, tableName=None, if_exists='fail') :
        """
        ' loads a pandas DF into SQLite table
        """
        #locals
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)

        self.logger.debug("{0} - tableName: {1}".format(calling_func, tableName))
        self.logger.debug("{0} - if_exists: {1}".format(calling_func, if_exists))

        try : 
            if dfToLoad is None : 
                errMsg = "dfToLoad is None"
                raise Exception(errMsg)

            if self.myUtils.IsEmptyOrNone(tableName) == True : 
                errMsg = "tableName is None or empty"
                raise Exception(errMsg)
            else :
                tableName_Path, tableName = os.path.split(tableName)
                self.logger.debug("{0} - removed path info tableName: {1}".format(calling_func, tableName))
            dfToLoad.to_sql(tableName, self.myMemTable, if_exists=if_exists,index=False)
            self.tbl_ColNames_Dict[tableName] = list(dfToLoad.columns)
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
    #END : def LoadDF

    def LoadList(self, listToLoad=None, tableName=None, colName='COL') :
        """
        ' load a list into SQLite table
        """
        #locals
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - listToLoad: {1}".format(calling_func, listToLoad))
        self.logger.debug("{0} - tableName: {1}".format(calling_func, tableName))
        self.logger.debug("{0} - colName: {1}".format(calling_func, colName))
        try :
            if listToLoad is None : 
                errMsg = "listToLoad is None"
                raise Exception(errMsg)
            listToLoad = [[item1] for item1 in listToLoad]
            self.logger.debug("{0} - listToLoad: {1}".format(calling_func, listToLoad))
            if self.myUtils.IsEmptyOrNone(tableName) == True : 
                errMsg = "tableName is None or empty"
                raise Exception(errMsg)
            if colName is None : 
                errMsg = "colName is None"
                raise Exception(errMsg)

            dropSqlStr = "DROP TABLE IF EXISTS [{0}]".format(tableName)

            self.tbl_ColNames_Dict[tableName] = colName
            
            self.logger.debug("{0} - createSqlStr: {1}".format(calling_func, dropSqlStr))
            createSqlStr = "CREATE TABLE [{0}] ([{1}] TEXT NULL)".format(tableName, colName)
            self.logger.debug("{0} - createSqlStr: {1}".format(calling_func, createSqlStr))

            insertSqlStr = "insert into [{0}] ([{1}]) values (?)".format(tableName, colName)
            self.logger.debug("{0} - insertSqlStr: {1}".format(calling_func, insertSqlStr))
            myCur = self.myMemTable.cursor()
            myCur.execute(dropSqlStr) #drop first
            myCur.execute(createSqlStr) #create table

            myCur.executemany(insertSqlStr, listToLoad) #insert data

        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
    #END : def LoadList

    def LoadFromCursor(self, cursorToLoad=None, tableName=None, FirstConnect=True,
                       fetchChunkSize=50000, MyHeaders=None) : 
        """
        ' Load data from a DB cursor into memTable. 
        '
        'INPUT ARGS : 
        '------------
        ' 1.) cursorToLoad : DB cursor from data needs to be loaded into memTable
        ' 2.) tableName : Name of the table that needs to be created/used for inserting data
        ' 3.) FirstConnect : True create table, False table already exists insert into it
        ' 4.) fetchChunkSize : chunkSize to read from cursorToLoad object
        ' 5.) MyHeaders : None : E.g., MyHeaders = END,SYSDATE,COMB,SHIFT,START --> comma seperated column names to be used (ignore column names sent by backend DB)
        """
        #locals
        calling_func = self.myUtils.getCallingFuncName()
        try :
            self.logger.debug("{0} - cursorToLoad : {1}".format(calling_func, cursorToLoad))
            self.logger.debug("{0} - tableName : {1}".format(calling_func, tableName))
            self.logger.debug("{0} - FirstConnect : {1}".format(calling_func, FirstConnect))

            #set the TableName for this instance
            self.TableName = tableName

            #create memTable cursor instance
            curSQLite = self.myMemTable.cursor()

            if FirstConnect == True:
                if self.myUtils.IsEmptyOrNone(MyHeaders) is False:
                    columns = MyHeaders.split(",")
                else:
                    columns = [i[0] for i in cursorToLoad.description]
                self.logger.debug("{0} - columns : {1}".format(calling_func, columns))
                colNamesForInsertStmnt = "[{0}]".format("],[".join(columns))
                dropTblStmnt = "DROP TABLE IF EXISTS [{0}]".format(tableName)
                self.logger.debug("{0} - dropTblStmnt : '{1}'".format(calling_func, dropTblStmnt))
                createTblStmnt = "CREATE TABLE IF NOT EXISTS [{0}] ({1})".format(tableName, colNamesForInsertStmnt)
                self.logger.debug("{0} - createTblStmnt : '{1}'".format(calling_func, createTblStmnt))

                curSQLite.execute(dropTblStmnt) # drop the table
                curSQLite.execute(createTblStmnt)
            #END : if FirstConnect == True:

            insertStmnt = "INSERT INTO [{0}] ({1}) values ({2})".format(tableName,
                                                                            colNamesForInsertStmnt,
                                                                            ", ".join(["?".format(colItem.strip("[]")) for colItem in columns]))
            self.logger.debug("{0} - insertStmnt : '{1}'".format(calling_func, insertStmnt))

            rows = cursorToLoad.fetchmany(fetchChunkSize)
            rowCount = 0 #len(rows)
            self.logger.debug("about to start processing rows")
            while rows:
                rowCount = rowCount + len(rows)
                curSQLite.executemany(insertStmnt, rows)
                rows = cursorToLoad.fetchmany(fetchChunkSize)
                
            self.logger.debug("{0} - rowCount : '{1}'".format(calling_func, rowCount))
            return rowCount

        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def GetRecCountFromTable(self, tableName, conObj=None):
        """
        (input) : 
        'FileName  : delimited-data-file name with path, which needs to be loaded to MemTable
        (ouput) : 
        status = True : if passed 
            else raise error
        """
        calling_func = self.myUtils.getCallingFuncName()
        try:   
            filePath, fileNameWithExt = os.path.split(tableName)
            fileNameWithoutExt, fileExt = os.path.splitext(fileNameWithExt)

            if conObj is None :
                self.logger.debug("{0} - creating cursor from internal memTable Connection".format(calling_func))
                curSQLite = self.myMemTable.cursor()
            else : 
                self.logger.debug("{0} - creating cursor from conObj parameter".format(calling_func))
                curSQLite = conObj.cursor()

            tableName = ("t_{0}".format(fileNameWithoutExt)) if fileNameWithoutExt.isdigit() else fileNameWithoutExt
            curSQLite.execute("select count(*) from [" + tableName + "]" )
            return curSQLite.fetchall()[0][0]
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def GetColumnNamesForTable(self, tableName = None, conToExternalSDB = None, convertToUpperCase=False):
        """
        return a list of column names for given tableName...in DB mentioned in conToExternalSDB (if present)
        """
        calling_func = self.myUtils.getCallingFuncName()
        self.logger.debug("{0} - tableName : {1}".format(calling_func, tableName))
        self.logger.debug("{0} - conToExternalSDB : {1}".format(calling_func, conToExternalSDB))
        self.logger.debug("{0} - convertToUpperCase : {1}".format(calling_func, convertToUpperCase))
        try :
            if self.myUtils.IsEmptyOrNone(tableName) is True :
                tableName = self.TableName
            
            try :
                colNames = self.tbl_ColNames_Dict[tableName]
            except Exception as err:
                sqlStr = "select * from [{0}]".format(tableName)
                self.logger.debug("{0} - sqlStr : {1}".format(calling_func, sqlStr))
                if not conToExternalSDB is None :
                    #external DB connection provided...
                    curSQLite = conToExternalSDB.cursor()
                else :
                    # use memtable connection
                    curSQLite = self.myMemTable.cursor()
                curSQLite.execute(sqlStr)
                colNames = [(descr[0] if convertToUpperCase is False else descr[0].upper()).strip("[] ") for descr in curSQLite.description]
                self.tbl_ColNames_Dict[tableName] = colNames
                del curSQLite
                del sqlStr

            if convertToUpperCase is True :
                colNames = [item.upper() for item in colNames]
            self.logger.debug("{0} - colNames : {1}".format(calling_func, colNames))
            
            return colNames

        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise

    def GetTablesInMemTable(self):
        """
        (input) : None

        (ouput) : 
        tblList = list of tables in MemTable : if passed 
            else raise error
        """
        calling_func = self.myUtils.getCallingFuncName()
        tblList = None
        sqlQry = "Select name from sqlite_master where type = 'table'"
        try:
            self.logger.info("sqlQry = " + sqlQry)
            curSQLite = MemTable.myMemTable.cursor()
            curSQLite.execute(sqlQry)
            rows = curSQLite.fetchall()
            tblList = [k[0] for k in rows]
            return tblList

        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
        finally:
            self.logger.info(" in finally block")
    

    def __genColumnsFromCursorDescription(self, **kwargs):
        """
        definition of kwargs(input) : None

        (ouput) : 
        tblList = list of tables in MemTable : if passed 
            else raise error
        """
        __columnsList = None
        try:
                _cursor_description = kwargs["CursorDescription"]
        except IndexError as idxErr:
            self.logger.error(idxErr)
            raise

        return __columnsList

    def map2SQLiteDataType(curDescriptionItem):
        in_DataType = curDescriptionItem[1].__name__
        in_display_size = curDescriptionItem[2]
        in_internal_size = curDescriptionItem[3]
        in_precison = curDescriptionItem[4]
        in_scale = curDescriptionItem[5]
        in_null_ok = curDescriptionItem[6]
        if in_DataType in ["STRING", "str"]:
            return "TEXT"
        if in_DataType in ["NUMBER",'float']:
            return "NUMERIC"
        if in_DataType in ["DATETIME", 'datetime']:
            return "NUMERIC"

    def getRowByIdx(self, Idx) :
        """
        """
        myRow = None

        if type(Idx) is not int:
            raise BaseException("please pass integer value for IDx")
        sqlStmnt = "select * from [{tableName}] where rowid = {IDx}".format(IDx=Idx, tableName=self.__tableName)
        #self.logger.info(sqlStmnt)
        curSQLite = self.myMemTable.cursor()
        curSQLite.execute(sqlStmnt)
        row = curSQLite.fetchone()

        return row

    def dropTable(self, tableName=None, useIfExistsClause=True):
        """
        'Drop a table from memTable
        ' useIfExistsClause : flag to indicate - whether to use IF EXISTS clause
        """
        calling_func = self.myUtils.getCallingFuncName()
        if tableName is None : 
            tableName = self.__tableName 
        if useIfExistsClause is False :
            sqlStmnt = "DROP TABLE [{0}]".format(tableName)
        else : 
            sqlStmnt = "DROP TABLE IF EXISTS [{0}]".format(tableName)

        self.logger.info("{0} - sqlStmnt : '{1}'".format(calling_func, sqlStmnt))
        curSQLite = self.myMemTable.cursor()
        curSQLite.execute(sqlStmnt)
    #END : def dropTable

    def Get_SQlite_Cols(self, MySDB, MyT, MyLocal=None) :
        """
        '======================================
        'Returns Cols in SQLite Table to MyData.
        'Returns Y if OK, else N if error #Dropped in py
        'VA30_47 : added MyLocal input param
        '
        'INPUT ARGS:
        '-----------
        'MySDB     : SQLite DB
        'MyT       : SQLite Table
        'MyInstance: Unique # #Dropped in py -- no file will be created -- direct access .sdb file
        'MyLocal   : N for Remote... (VA30_47) -- not needed in py as the SQLite is built in. In VA if MyLocal == "N" then needed to copy over the SQLIte exe to local
        '
        'OUTPUT :
        '--------
        'MyHdrs    : Holds , dlm Col Hdrs
        '=======================================
        """
        calling_func = self.myUtils.getCallingFuncName()
        conToExternalSDB = None
        try :
            if self.myUtils.IsEmptyOrNone(MyLocal) is True :
                MyLocal = "N"

            self.logger.debug("{0} - MySDB : {1}".format(calling_func, MySDB))
            self.logger.debug("{0} - MyT : {1}".format(calling_func, MyT))
            
            if MySDB == ".\\" :
                MyHdrs = self.GetColumnNamesForTable(MyT, conToExternalSDB)
            else :
                if os.path.isfile(MySDB) is False :
                    errMsg = "Could not find file : {0}".format(MySDB)
                    raise Exception(errMsg)
                else :
                    conToExternalSDB = self.getStandaloneCon()
                    conToExternalSDB.execute(r"ATTACH DATABASE '{0}' AS spf99999".format(MySDB))
                    MyHdrs = self.GetColumnNamesForTable(MyT, conToExternalSDB)
            
            return MyHdrs
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
        finally :
            if not conToExternalSDB is None :
                try :
                    self.logger.debug("{0} - in finally block...closing connection to {1}".format(calling_func, MySDB))
                    conToExternalSDB.close()
                except Exception as err:
                    self.logger.exception("{0} - finally block error not able to close connection to {1}".format(calling_func, MySDB))
                    pass
    #END : def Get_SQlite_Cols

    #Status : dev WIP
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
                lSDPath = os.path.join(lSDPath, "Data\\")
                self.logger.debug("{0} - lSDPath updated: {1}".format(calling_func, lSDPath))
                Site1 = re.sub(__SQLITE_DATA_PATH, lambda x : lSDPath, Site1, 0,re.IGNORECASE)
                self.logger.debug("{0} - Site1 path: {1}".format(calling_func, Site1))
            elif Site1.upper().find(__SQLITE_DATA_PATH2) != -1 :
                lSDPath = self.myUtils.gSPFLib
                self.logger.debug("{0} - lSDPath: {1}".format(calling_func, lSDPath))
                lSDPath = os.path.join(lSDPath, "Data\\")
                self.logger.debug("{0} - lSDPath updated: {1}".format(calling_func, lSDPath))
                Site1 = re.sub(__SQLITE_DATA_PATH2, lambda x : lSDPath, Site1, 0,re.IGNORECASE)
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
            MyInstance2 = "{0}_{1}_{2}".format(os.getenv("USERNAME"), MyInstance, RNStr)
            self.logger.debug("{0} - MyInstance2: {1}".format(calling_func, MyInstance2))

            s1 = "{0}.sql".format(RNStr)
            self.logger.debug("{0} - s1: {1}".format(calling_func, s1))

            if self.myUtils.IsEmptyOrNone(Site1) is True : #'csv File processing
                if self.myUtils.IsEmptyOrNone(l_DBName) is False : #'Fixed DB Name passed
                    D_SSMDF = l_DBName.strip()
                    D_SSMDFFileName, l_DBStem = os.path.splitext(D_SSMDF.lower())
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
                    D_SSMDF = os.path.abspath(D_SSMDF)
                    self.logger.debug("{0} - Abs path : D_SSMDF : {1}".format(calling_func, D_SSMDF))
                #AttachDBName = D_SSMDF
            else : #'Site1 <>"" - SQL DB passed
                MySQLiteCache = "Y"
                self.logger.debug("{0} - MySQLiteCache : {1}".format(calling_func, MySQLiteCache))
                if bool(re.search("NEW:",Site1, re.IGNORECASE)) == True and len(Site1.strip()) > 5 :
                    #'New & persist
                    D_SSMDF = Site1[5:]
                    self.logger.debug("{0} - NEW: found in Site1 : D_SSMDF : {1}".format(calling_func, D_SSMDF))
                elif bool(re.search(":::",Site1, re.IGNORECASE)) == True :
                    #'DB on an App Server
                    MySServer, D_SSMDF = Site1.split(":::")
                    self.logger.debug("{0} - MySServer : {1}; D_SSMDF : {2}".format(calling_func, MySServer, D_SSMDF))
                    D_SSMDFPath, D_SSMDFFileName = os.path.split(D_SSMDF)
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
                    if os.path.exists(D_SSMDF) == False :
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
                SSEExe = os.path.join(self.myUtils.gTempDir, MySQExt)
                MyCvtExe = os.path.join(self.myUtils.gTempDir, "CleanDelimsCRLF.exe")
                self.logger.debug("{0} - MyCvtExe : {1}".format(calling_func, MyCvtExe))
                ExePath2 = self.myUtils.gTempDir
            elif MyLocal == "N":
                SSEExe = os.path.join(self.myUtils.gSPFLib, "SQLite\\{0}".format(MySQExt))
                SSEExeTo = os.path.join(WorkDir, "{0}".format(MySQExt))
                self.logger.debug("{0} - SSEExe : {1}".format(calling_func, SSEExe))
                self.logger.debug("{0} - SSEExeTo : {1}".format(calling_func, SSEExeTo))
                SSEExe = self.myUtils.DoCopySPFLib(SSEExe, SSEExeTo)
                self.logger.debug("{0} - SSEExe : {1}".format(calling_func, SSEExe))
                del SSEExeTo
                #'=== SQLite64===
                MySQLiteExt = os.path.join(self.myUtils.gSPFLib, "SQLite\\{0}".format(MySExt))
                MySQLiteExtTo = os.path.join(WorkDir, "{0}".format(MySExt))
                self.logger.debug("{0} - MySQLiteExt : {1}".format(calling_func, MySQLiteExt))
                self.logger.debug("{0} - MySQLiteExtTo : {1}".format(calling_func, MySQLiteExtTo))
                MySQLiteExt = self.myUtils.DoCopySPFLib(MySQLiteExt, MySQLiteExtTo)
                self.logger.debug("{0} - MySQLiteExt : {1}".format(calling_func, MySQLiteExt))
                del MySQLiteExtTo
                #'=== SQLite64===

                MyCvtExe = os.path.join(self.myUtils.gSPFLib, "SQLite\\CleanDelimsCRLF.exe")
                MyCvtExeTo = os.path.join(WorkDir, "CleanDelimsCRLF.exe")
                self.logger.debug("{0} - MyCvtExe : {1}".format(calling_func, MyCvtExe))
                self.logger.debug("{0} - MyCvtExeTo : {1}".format(calling_func, MyCvtExeTo))
                MyCvtExe = self.myUtils.DoCopySPFLib(MyCvtExe, MyCvtExeTo)
                self.logger.debug("{0} - MyCvtExe : {1}".format(calling_func, MyCvtExe))
                del MyCvtExeTo

                ExePath2 = self.myUtils.gSPFLib
            else : #'local
                SSEExe = os.path.join(MyLocal, MySQExt)
                self.logger.debug("{0} - SSEExe : {1}".format(calling_func, SSEExe))
                MyCvtExe = os.path.join(MyLocal, "CleanDelimsCRLF.exe")
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
                uniqueMyPair = list(set(item.upper() for item in MyPair)) #'Ignore this Table <--- this logic is not needed in PyEE as this step will get Unique items
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
                        SrcFilePath, SrcFileName = os.path.split(MyData)
                        self.logger.debug("{0} - SrcFilePath: {1}".format(calling_func, SrcFilePath))
                        self.logger.debug("{0} - SrcFileName: {1}".format(calling_func, SrcFileName))
                        SrcFile = SrcFileName 
                        SrcTable = ""
                        #'=== v30.104 ===== <--- is not needed in PyEE as the filePath and fileName are split above
                        if SrcFileName.find(":") > -1 :
                            SrcFile, SrcTable = re.split(r"\s?:\s?", SrcFileName) #SrcFileName.partition(" : ")[::2]
                        self.logger.debug("{0} - SrcFile: {1}".format(calling_func, SrcFile))
                        self.logger.debug("{0} - SrcTable: {1}".format(calling_func, SrcTable))                       

                        SrcFileNamePart, SrcFileNameExtPart = os.path.splitext(SrcFileName)
                        self.logger.debug("{0} - SrcFileNamePart: {1}".format(calling_func, SrcFileNamePart))
                        self.logger.debug("{0} - SrcFileNameExtPart: {1}".format(calling_func, SrcFileNameExtPart))

                        if self.myUtils.IsEmptyOrNone(SrcFilePath.strip()) is True :
                            MySrcTxt = os.path.join(WorkDir, SrcFile)
                        else : #has path info use it
                            MySrcTxt = os.path.join(SrcFilePath, SrcFile)
                        MySrcTxt = os.path.abspath(MySrcTxt)
                        #region '===v30.50  ===========
                        #if SrcFileNameExtPart.lower() != ".$db" :
                        #endregion '===v30.50  =========== Note if you uncomment then indent below if-else block
                        """
                        '===== v30.71 ==========
                        '===========================
                        'Chk for commas in file name
                        '===========================
                        """
                        MySrcTxt = re.sub(r"<c>", ",", MySrcTxt, 0, re.IGNORECASE) #MySrcTxt.replace("<c>",",")
                        #'=======================

                        """
                        '==============
                        'Chk File Exists
                        '==============
                        """
                        if os.path.exists(MySrcTxt) == False :
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
                                SrcTable, SrcTableStem = os.path.splitext(SrcFileName)  #SrcFile.partition(".")[::2]
                                self.logger.debug("{0} - SrcTable: {1}".format(calling_func, SrcTable))
                                self.logger.debug("{0} - SrcTableStem: {1}".format(calling_func, SrcTableStem))
                            #END : if len(SrcTable.strip()) == 0 :
                            """       
                            '=====================
                            'Create Valid Tbl Name
                            '=====================
                            """
                            SrcTable = re.sub(r"[^\d\w__]", "_", SrcTable.strip(), re.IGNORECASE|re.VERBOSE)   #'Invalid Character. Replace with _
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
                                MySrcTxt2 = os.path.join(".\\", "{0}_{1}.tmp".format(uniqueMyPairCounter, RNStr))
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
                                MyNullSQLDict[re.sub(r"[^\w]", "_", colItem, re.IGNORECASE|re.VERBOSE)] = MyEmptyNull_val

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
                OutExcelFileName, OutExcelFileExt = os.path.splitext(OutExcel)
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
                    MyOutSQL.append(".load '{0}'".format(os.path.join(".\\", MySExt)))
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
                                    if cmdCnt == 0 and lenCommand1List > 1 and re.match("select", cmd1.strip(),re.IGNORECASE) is None:
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
                                    tmpMatch = re.match("select|WITH RECURSIVE|WITH", cmd1.strip(),re.IGNORECASE)
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
                        self.logger.debug("{0} - {1}".format(calling_func, sys.stderr))
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
                    MyJobArgs.append('"{0}"'.format(os.path.abspath(s1)))
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
                    MyJobArgs.append('"{0}"'.format(os.path.join(ExePath2, "Get_SQLite_Mid.exe")))
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
                    MyJobArgs.append('"{0}"'.format(os.path.join(ExePath2, "GetHadoop.exe")))
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

    #END : def Run_SQLite

    def parse_GetSQLStrList(self, sqlStr) :
        """
        Clean up SQL string for embeded ; ' " in column names
        '(input)
        '-------
        '1.) sqlStr (StringType) : SQL String to parse

        '(output)
        '-------
        '1.) sqlStrList (ListType) : List of SQL string split ";"
        """
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)

        def CheckReplace_TokenVal(matchObj):
            """
                A nested function of parse_GetSQLStrList. 
                Perform check/replace logic.
                This function is called in regex sub function. 
            (input) :
            1. matchObj : macthed regex object (will contain the matched string and its subroups)
            (output) : 
            1. updated string value
            """
            if matchObj is not None :
                matcheValueFull = matchObj.group()
                self.logger.debug ("{0} - matcheValueFull : {1}".format(calling_func, matcheValueFull))
                #get the named tokens
                colName = matchObj.group('colName')
                #spchar = matchObj.group('spchar')
                self.logger.debug("{0} - colName : {1}".format(calling_func, colName))
                #self.logger.debug("{0} - spchar : {1}".format(calling_func, spchar))
                #replace special characters with markers
                colName = colName.replace("';'", "<µµµµµ>").replace('<;>', "<µµµµ>").replace(";", "<µ>").replace("'", "<µµ>").replace('"', "<µµµ>")

                self.logger.debug("{0} - updated colName : {1}".format(calling_func, colName))
                return colName
        #END : CheckReplace_TokenVal nested function

        #mail function starts here
        #locals
        sqlStrList = []
        try : 
            sqlStr = re.sub(r"/\*BEGIN SQL\*/|/\*END SQL\*/","",sqlStr,0, re.IGNORECASE|re.MULTILINE)          
            self.logger.debug("{0} - sqlStr : {1}".format(calling_func, sqlStr))
            if sqlStr.strip().upper().startswith("SELECT") is True and sqlStr.find(";") == -1:
                sqlStrList.append(sqlStr)
                return sqlStrList
            """
            regex sub : for every pattern matched string substitue required value in func CheckReplace_TokenVal
            """
            #sqlStr = sqlStr.replace('<;>', "<µµµµ>")
            ##1st replace that match E.g., --> ,'[' || a0.[ABCD;DEF] || ']' or ,'[' || [ABCD;DEF] || ']'
            #regexPatStr1 = "(?P<colName>\'\[.*?\]\')"
            #regexPatern1 = re.compile(regexPatStr1, re.VERBOSE|re.MULTILINE|re.IGNORECASE)
            #sqlStr = regexPatern1.sub(CheckReplace_TokenVal, sqlStr)
            #self.logger.debug("{0} - myDataOut sqlStr : {1}".format(calling_func,sqlStr))

            ##2nd replace that match E.g., --> ,a0.[ABCD;DEF] or ,[ABCD;DEF] or [ABCD;DEF]
            #regexPatStr2 = "(?P<colName>\.?\[.*?\])"
            #regexPatern2 = re.compile(regexPatStr2, re.VERBOSE|re.MULTILINE|re.IGNORECASE)
            #sqlStr = regexPatern2.sub(CheckReplace_TokenVal, sqlStr)
            #self.logger.debug("{0} - myDataOut sqlStr : {1}".format(calling_func,sqlStr))

            ##now split ';' to get multiple queries
            ##sqlStrList_tmp = re.findall(r'''((?:[^;"']|"[^"]*"|'[^']*')+)''', sqlStr, re.MULTILINE) #sqlStr.split(";")
            sqlStrList_tmp = re.split("(;$)", sqlStr,0, re.MULTILINE)#sqlStr.split(";")

            #now updated markers with actual special characters
            #sqlStrList = [sqlItem.replace("<µµµµµ>","';'").replace("<µµµµ>", '<;>').replace("<µµµ>", '"').replace("<µµ>","'").replace("<µ>",";") for sqlItem in sqlStrList_tmp if sqlItem.strip() not in ['', ";", None] ]
            sqlStrList = []
            sqlStrList_tmp = [sqlItem for sqlItem in sqlStrList_tmp if sqlItem.strip() not in ['', ";", None] ]
            self.logger.debug("{0} - len(sqlStrList_tmp) : {1}".format(calling_func,len(sqlStrList_tmp)))
            if len(sqlStrList_tmp) > 1:
                sqlStrList_tmp2 = []
                for idx, sqlItem in enumerate(sqlStrList_tmp):
                    self.logger.debug("{0} - sqlItem : {1}".format(calling_func,sqlItem))
                    if re.match("select", sqlItem.strip(),re.IGNORECASE):
                        #came across a 'select' statement concat all the stmnts till this 'select' and add to list
                        sqlStrList.append(";\n".join(sqlStrList_tmp2))
                        #re-init sqlStrList_tmp2
                        sqlStrList_tmp2 = []
                        #append the 'select'
                        sqlStrList.append(sqlItem)
                    else:
                        #not a 'select'
                        sqlStrList_tmp2.append(sqlItem)

                #sqlStrList.append(";\n".join(sqlStrList_tmp[:-1]))
                #sqlStrList.append(sqlStrList_tmp[-1])
            else:
                sqlStrList.append(sqlStrList_tmp[0]) 


            del sqlStrList_tmp
            del sqlStr
            #self.logger.debug("{0} - len(sqlStrList) : {1}".format(calling_func,len(sqlStrList)))
            self.logger.debug("{0} - len(sqlStrList) : {1}".format(calling_func,len(sqlStrList)))
            return sqlStrList
        except Exception as err:
            self.logger.exception("{0} - ".format(calling_func, err.args[0]))
            raise
    #END : parse_GetSQLStrList

    #status : dev done, UT WIP
    def writeCursorToFile(self, _cursor, outFile, OutExcel, outTTin, append=False, fetchChunkSize=50000, FinalRow=0, incrementalRunCtr=0, ll_NoHdrs=False, ll_QuoteCSV=False, SQLite_Encoding='UTF-8'):
        """
        ' standard logic to write to file #V1.0.0.8 --> updated to divert calls to writeCursorToFileWithoutQuote or writeCursorToFileWithQuote
        '(input)
        '-------
        '1.) _cursor (ObjectType) : backend con-cursor object
        '2.) outFile (StringType) : output file name
        '3.) OutExcel (StringType) :
        '4.) outTTin (BooleanType) : 
        '5.) append (BooleanType) :
        '6.) fetchChunkSize (IntType) : default 50000
        '7.) FinalRow (IntType) : default = 0 
        '8.) incrementalRunCtr (IntType) : incremental run counter
         9.) ll_NoHdrs(BooleanType) : write header to output file or not: default -> False 
        10.) ll_QuoteCSV(BooleanType) : Quote column data in output default -> False
        '(output)
        '-------
        '1.) FinalRow (IntType) :
        """
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - outFile: '{1}'".format(calling_func, outFile))
        self.logger.debug("{0} - OutExcel: '{1}'".format(calling_func, OutExcel))
        self.logger.debug("{0} - outTTin: '{1}'".format(calling_func, outTTin))
        self.logger.debug("{0} - append: '{1}'".format(calling_func, append))
        self.logger.debug("{0} - fetchChunkSize: '{1}'".format(calling_func, fetchChunkSize))
        self.logger.debug("{0} - FinalRow: '{1}'".format(calling_func, FinalRow))
        self.logger.debug("{0} - incrementalRunCtr: '{1}'".format(calling_func, incrementalRunCtr))
        self.logger.debug("{0} - ll_NoHdrs: '{1}'".format(calling_func, ll_NoHdrs))
        self.logger.debug("{0} - ll_QuoteCSV: '{1}'".format(calling_func, ll_QuoteCSV))
        self.logger.debug("{0} - SQLite_Encoding: '{1}'".format(calling_func, SQLite_Encoding))
        startTime = time.perf_counter()
        rowCount = 0
        isFirstRow = True
        try:
            self.logger.debug("{0} - cursor: '{1}'".format(calling_func, _cursor))
            if _cursor is None :
                self.logger.debug("{0} - cursor is None return without writing anything".format(calling_func))
                return

            columns = [i[0] for i in _cursor.description]
            #self.logger.debug("{0} - columns : {1}".format(calling_func, columns))   

            fileDLM = self.myUtils.GetFileDLM(OutExcel, mode="O")
            if fileDLM == "": 
                rowCount = self.myUtils.writeCursorToTxtFile(_cursor, fetchChunkSize, FirstConnect=not append, OutExcel=OutExcel, outTTin=outTTin, incrementalRunCtr=incrementalRunCtr, ll_NoHdrs=ll_NoHdrs,ll_QuoteCSV=ll_QuoteCSV,columns=columns)
            else:
                
                if ll_QuoteCSV is True :
                    rowCount = self.writeCursorToFileWithQuote(_cursor,outFile, OutExcel, outTTin, append,fetchChunkSize,FinalRow,incrementalRunCtr,ll_NoHdrs,ll_QuoteCSV,columns, SQLite_Encoding=SQLite_Encoding)
                else:
                    rowCount = self.writeCursorToFileWithoutQuote(_cursor,outFile, OutExcel, outTTin, append,fetchChunkSize,FinalRow,incrementalRunCtr,ll_NoHdrs,ll_QuoteCSV,columns, SQLite_Encoding=SQLite_Encoding)              
        except IOError as IOerr:
            errMsg = "I/O Error({0}): {1} \n File : {2}".format(IOerr.errno, IOerr.strerror, IOerr.filename)
            self.logger.exception("{0} - {1}".format(calling_func, errMsg))
            raise Exception(errMsg)
        except Exception as err:
            self.logger.exception("{0} -{1}".format(calling_func, err.args[0]))
            raise
        finally:
            timeTaken = time.perf_counter() - startTime
            self.logger.info("writeCursorToFile : written " + str(rowCount) + " records to file : " + OutExcel + ";Response Time : " + str(timeTaken))

            if outTTin is False or rowCount == 0:
                self.myUtils.DoWriteDonePromptText(rowCount, incrementalRunCtr)
        return FinalRow + rowCount
    #END : def writeCursorToFile

#status : dev done, UT WIP
    def writeCursorToFileWithoutQuote(self, _cursor, outFile, OutExcel, outTTin, append=False, fetchChunkSize=50000, FinalRow=0, incrementalRunCtr=0, ll_NoHdrs=False, ll_QuoteCSV=False, columns=None, SQLite_Encoding='UTF-8'):
        r"""
        ' standard logic to write to file without embedded quote handling
            E.g.,
           Input column value : c:\windows\system32\XCOPY "%~2MCLARKE\DOCUMENTS\SQLPATHFINDER_NET_CHARTING\UPDATE\ARIES\VIEWS\AT_E3_Data_2.dat" "%~1JMCLARKE\DOCUMENTS\SQLPATHFINDER_NET_CHARTING\UPDATE\ARIES\VIEWS\*" /D/Y/C/R
           Output column value : c:\windows\system32\XCOPY "%~2MCLARKE\DOCUMENTS\SQLPATHFINDER_NET_CHARTING\UPDATE\ARIES\VIEWS\AT_E3_Data_2.dat" "%~1JMCLARKE\DOCUMENTS\SQLPATHFINDER_NET_CHARTING\UPDATE\ARIES\VIEWS\*" /D/Y/C/R
        '(input)
        '-------
        '1.) _cursor (ObjectType) : backend con-cursor object
        '2.) outFile (StringType) : output file name
        '3.) OutExcel (StringType) :
        '4.) outTTin (BooleanType) : 
        '5.) append (BooleanType) :
        '6.) fetchChunkSize (IntType) : default 50000
        '7.) FinalRow (IntType) : default = 0 
        '8.) incrementalRunCtr (IntType) : incremental run counter
         9.) ll_NoHdrs(BooleanType) : write header to output file or not: default -> False 
        10.) ll_QuoteCSV(BooleanType) : Quote column data in output default -> False
        11.) columns (listtype) : List of columns
        '(output)
        '-------
        '1.) FinalRow (IntType) :"""
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - outFile: '{1}'".format(calling_func, outFile))
        self.logger.debug("{0} - OutExcel: '{1}'".format(calling_func, OutExcel))
        self.logger.debug("{0} - outTTin: '{1}'".format(calling_func, outTTin))
        self.logger.debug("{0} - append: '{1}'".format(calling_func, append))
        self.logger.debug("{0} - fetchChunkSize: '{1}'".format(calling_func, fetchChunkSize))
        self.logger.debug("{0} - FinalRow: '{1}'".format(calling_func, FinalRow))
        self.logger.debug("{0} - incrementalRunCtr: '{1}'".format(calling_func, incrementalRunCtr))
        self.logger.debug("{0} - ll_NoHdrs: '{1}'".format(calling_func, ll_NoHdrs))
        self.logger.debug("{0} - ll_QuoteCSV: '{1}'".format(calling_func, ll_QuoteCSV))
        self.logger.debug("{0} - SQLite_Encoding: '{1}'".format(calling_func, SQLite_Encoding))
        startTime = time.perf_counter()

        fileDLM = self.myUtils.GetFileDLM(OutExcel)
        if isPYTHON2:
            fileMode = 'ab' if append == True else 'wb'
        else:
            fileMode = 'a' if append == True else 'w'
        self.logger.debug("{0} - fileMode: '{1}'".format(calling_func, fileMode))
        file_OutExcel = None
        rowCount = 0
        isFirstRow = True

        try:
            self.logger.debug("{0} - cursor: '{1}'".format(calling_func, _cursor))
            if _cursor is None :
                self.logger.debug("{0} - cursor is None return without writing anything".format(calling_func))
                return
            if columns is None:
                columns = [i[0] for i in _cursor.description]
                #if isPYTHON2 is False:
                #    columns = columns.decode()
            
                #, encoding=self.myUtils.gOSDefaultEncoding, errors='replace'
            with open(OutExcel,fileMode) if isPYTHON2 is True else open(OutExcel,fileMode, encoding=SQLite_Encoding, errors='replace') as file_OutExcel:
                if append is False and ll_NoHdrs is False: #in overwrite mode
                    file_OutExcel.write("{0}\n".format(fileDLM.join(columns)))
                    self.logger.info("{0} - write header completed : total  columns : {1}".format(calling_func, len(columns)))

                rows = _cursor.fetchmany(fetchChunkSize)
                while rows:
                    rowCount = rowCount + len(rows)
                    for row in rows:
                        if row is not None:
                            file_OutExcel.write("{0}\n".format(fileDLM.join(map(lambda x: str(x) if x is not None else "", row))))
                    #self.logger.info("written rows : " + str(rowCount))
                    rows = _cursor.fetchmany(fetchChunkSize)
                    isFirstRow = False
            #END : with open(OutExcel,fileMode) as file_OutExcel_writer

            return FinalRow + rowCount
        except IOError as IOerr:
            errMsg = "I/O Error({0}): {1} \n File : {2}".format(IOerr.errno, IOerr.strerror, IOerr.filename)
            self.logger.exception("{0} - {1}".format(calling_func, errMsg))
            raise Exception(errMsg)
        except Exception as err:
            self.logger.exception("{0} -{1}".format(calling_func, err.args[0]))
            raise
        finally:
            self.logger.info("in finally block")
            if file_OutExcel != None: 
                if not file_OutExcel.closed: 
                    self.logger.info("File was not closed...will close now")
                    file_OutExcel.close()
                else:
                    self.logger.info("File is closed...")
    #END : def writeCursorToFileWithoutQuote

#status : dev done, UT WIP
    def writeCursorToFileWithQuote(self, _cursor, outFile, OutExcel, outTTin, append=False, fetchChunkSize=50000, FinalRow=0, incrementalRunCtr=0, ll_NoHdrs=False, ll_QuoteCSV=True, columns=None, SQLite_Encoding='UTF-8'):
        r"""
        ' standard logic to write to file with embedded quote handling
            E.g.,
           Input column value : c:\windows\system32\XCOPY "%~2MCLARKE\DOCUMENTS\SQLPATHFINDER_NET_CHARTING\UPDATE\ARIES\VIEWS\AT_E3_Data_2.dat" "%~1JMCLARKE\DOCUMENTS\SQLPATHFINDER_NET_CHARTING\UPDATE\ARIES\VIEWS\*" /D/Y/C/R
           Output column value : "c:\windows\system32\XCOPY ""%~2MCLARKE\DOCUMENTS\SQLPATHFINDER_NET_CHARTING\UPDATE\ARIES\VIEWS\AT_E3_Data_2.dat"" ""%~1JMCLARKE\DOCUMENTS\SQLPATHFINDER_NET_CHARTING\UPDATE\ARIES\VIEWS\*"" /D/Y/C/R"
        '(input)
        '-------
        '1.) _cursor (ObjectType) : backend con-cursor object
        '2.) outFile (StringType) : output file name
        '3.) OutExcel (StringType) :
        '4.) outTTin (BooleanType) : 
        '5.) append (BooleanType) :
        '6.) fetchChunkSize (IntType) : default 50000
        '7.) FinalRow (IntType) : default = 0 
        '8.) incrementalRunCtr (IntType) : incremental run counter
         9.) ll_NoHdrs(BooleanType) : write header to output file or not: default -> False 
        10.) ll_QuoteCSV(BooleanType) : Quote column data in output default -> True
        11.) columns (listtype) : List of columns
        '(output)
        '-------
        '1.) FinalRow (IntType) :
        """
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - outFile: '{1}'".format(calling_func, outFile))
        self.logger.debug("{0} - OutExcel: '{1}'".format(calling_func, OutExcel))
        self.logger.debug("{0} - outTTin: '{1}'".format(calling_func, outTTin))
        self.logger.debug("{0} - append: '{1}'".format(calling_func, append))
        self.logger.debug("{0} - fetchChunkSize: '{1}'".format(calling_func, fetchChunkSize))
        self.logger.debug("{0} - FinalRow: '{1}'".format(calling_func, FinalRow))
        self.logger.debug("{0} - incrementalRunCtr: '{1}'".format(calling_func, incrementalRunCtr))
        self.logger.debug("{0} - ll_NoHdrs: '{1}'".format(calling_func, ll_NoHdrs))
        self.logger.debug("{0} - ll_QuoteCSV: '{1}'".format(calling_func, ll_QuoteCSV))
        self.logger.debug("{0} - SQLite_Encoding: '{1}'".format(calling_func, SQLite_Encoding))
        startTime = time.perf_counter()
        self.logger.debug("{0} - row_factory: '{1}'".format(calling_func, self.myMemTable.row_factory))
        
        fileDLM = self.myUtils.GetFileDLM(OutExcel)
        if isPYTHON2:
            fileMode = 'ab' if append == True else 'wb'
        else:
            fileMode = 'a' if append == True else 'w'

        self.logger.debug("{0} - fileMode: '{1}'".format(calling_func, fileMode))
        file_OutExcel = None
        rowCount = 0
        isFirstRow = True

        try:
            self.logger.debug("{0} - cursor: '{1}'".format(calling_func, _cursor))
            if _cursor is None :
                self.logger.debug("{0} - cursor is None return without writing anything".format(calling_func))
                return

            if columns is None:
                columns = [i[0] for i in _cursor.description]
            #self.logger.debug("{0} - columns : {1}".format(calling_func, columns))
            with open(OutExcel,fileMode) if isPYTHON2 is True else open(OutExcel,fileMode, encoding=SQLite_Encoding, errors='replace') as file_OutExcel :
            # with open(OutExcel,fileMode) if isPYTHON2 is True else open(OutExcel,fileMode, encoding=SQLite_Encoding, errors='replace') as file_OutExcel :
                if self.myMemTable.row_factory == sqlite3.Row : 
                    file_OutExcel_writer = csv.DictWriter(file_OutExcel, delimiter=fileDLM, lineterminator=self.myUtils.gLineTerminator, fieldnames=columns, extrasaction='ignore')                
                else :
                    file_OutExcel_writer = csv.writer(file_OutExcel, delimiter=fileDLM, lineterminator=self.myUtils.gLineTerminator) #, quoting=csv.QUOTE_NONE
                
                if append is False and ll_NoHdrs is False: #in overwrite mode
                    if self.myMemTable.row_factory is sqlite3.Row :
                        file_OutExcel_writer.writeheader()
                    else :
                        file_OutExcel_writer.writerow(columns)
                    self.logger.info("{0} - write header completed : total  columns : {1}".format(calling_func, len(columns)))
                    
                rows = _cursor.fetchmany(fetchChunkSize)
                
                if self.myMemTable.row_factory is sqlite3.Row : 
                    while rows:
                        rowCount = rowCount + len(rows)
                        isFirstRow = False
                        for row in rows :
                            file_OutExcel_writer.writerow(dict(row))
                        #self.logger.info("written rows : " + str(rowCount))
                        rows = _cursor.fetchmany(fetchChunkSize)
                else : 
                    while rows:
                        rowCount = rowCount + len(rows)
                        isFirstRow = False
                        file_OutExcel_writer.writerows(rows)
                        #self.logger.info("written rows : " + str(rowCount))
                        rows = _cursor.fetchmany(fetchChunkSize)
            #END : with open(OutExcel,fileMode) as file_OutExcel   
            return FinalRow + rowCount
        except IOError as IOerr:
            errMsg = "I/O Error({0}): {1} \n File : {2}".format(IOerr.errno, IOerr.strerror, IOerr.filename)
            self.logger.exception("{0} - {1}".format(calling_func, errMsg))
            raise Exception(errMsg)
        except Exception as err:
            self.logger.exception("{0} -{1}".format(calling_func, err.args[0]))
            raise
        finally:
            self.logger.info("in finally block")
            if file_OutExcel != None: 
                if not file_OutExcel.closed: 
                    self.logger.info("File was not closed...will close now")
                    file_OutExcel.close()
                else:
                    self.logger.info("File is closed...")
    #END : def writeCursorToFileWithQuote

    def Run_SQLQuery(self, myQuery, useRowFactory=False) :
        """
        '******************************************************************
        ' Execute a query against memTable connection
        '
        'INPUT ARGS :
        '==========
        'myQuery  : SQL query to be executed
        '******************************************************************
        """
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        #self.logger.debug("{0} - myQuery: '{1}'".format(calling_func, myQuery))
        try :
            if useRowFactory is True :
                self.myMemTable.row_factory = sqlite3.Row
            else :
                self.myMemTable.row_factory = None
            myCursor = self.myMemTable.cursor()
            myCursor.execute(myQuery)
            return myCursor
        except Exception as err:
            self.logger.exception("{0} -{1}".format(calling_func, err.args[0]))
            raise
        finally :
            self.myMemTable.row_factory = None
    #END : def Run_SQLQuery

    def Run_SQLQueryGet1RowColVal(self, myQuery, defaultVal=None, errorIfColumnNotFound=True) :
        """
        '******************************************************************
        ' Execute a query against memTable connection and get a single row/col value
        '
        'INPUT ARGS :
        '==========
        'myQuery  : SQL query to be executed
        'defaultVal : default value if data is not found
        'errorIfColumnNotFound : if == True -> Raise error if column is not found, if == False return default value
        '******************************************************************
        """
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        #self.logger.debug("{0} - myQuery: '{1}'".format(calling_func, myQuery))
        #self.logger.debug("{0} - defaultVal: '{1}'".format(calling_func, defaultVal))
        outVal = defaultVal
        try :
            
            myCursor = self.Run_SQLQuery(myQuery)
            rows = myCursor.fetchall()
            #self.logger.debug("{0} - len(rows): '{1}'".format(calling_func, len(rows)))
            for row in rows :
                #self.logger.debug("{0} - row: '{1}'".format(calling_func, row))
                outVal = row[0]
            #self.logger.debug("{0} - outVal: '{1}'".format(calling_func, outVal))
            return outVal
        except Exception as err:
            self.logger.exception("{0} -{1}".format(calling_func, err.args[0]))
            if errorIfColumnNotFound is False :
                return defaultVal
            else :
                raise
    #END : def Run_SQLQuery

    def Load_Table_Design(self, MyFName) :
        """
        '===============================================
        'Assign the Report Spec or Format to a MEM Table
        '
        'ARGS:
        '----
        'l_RptType   : Type of Report (HTML or CSS)
        'MyFName     : Name of Report Spec File

        'DROPPED in Py: 
        'Table2Design: Table Design Mem Table # -- will use py memtable which is always available in mem
        '===============================================
        """
        #locals
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        #self.logger.debug("{0} - l_RptType: '{1}'".format(calling_func, l_RptType))
        self.logger.debug("{0} - MyFName: '{1}'".format(calling_func, MyFName))
        
        try : 
            
            MyFName_Path, MyFName_FileNameWExt = os.path.split(MyFName)
            MyFName_FilePart, MyFName_FileExt = os.path.splitext(MyFName_FileNameWExt)
            self.LoadFromFile(fileName=MyFName, tableName=MyFName_FileNameWExt, EANImport=True, fileDLM="\t", defaultColValueIfEmpty="", displayColumnMismatchWarnings=False)
            sqlStr = "select KEY FROM [{0}] WHERE TYPE = 'TYPE'".format(MyFName_FileNameWExt)
            self.logger.debug("{0} - sqlStr: '{1}'".format(calling_func, sqlStr))

            l_RptType = self.Run_SQLQuery(sqlStr).fetchone()[0]
            self.logger.debug("{0} - l_RptType: '{1}'".format(calling_func, l_RptType))
            return l_RptType
        except Exception as err:
            self.logger.exception("{0} - {1}".format(calling_func, err.args[0]))
            raise
    #END : def Load_Table_Design

    def getStandaloneCon(self, dbFileToOpen='', AttachSQL=None):
        """
        helper function to not use memTable global connection but generate a standalone SQLite3 connection 
        """
        #locals
        calling_func = self.myUtils.getCallingFuncName(2, self.__class__.__name__)
        self.logger.debug("{0} - dbFileToOpen: '{1}'".format(calling_func, dbFileToOpen))
        self.logger.debug("{0} - AttachSQL: '{1}'".format(calling_func, AttachSQL))
        sqlite3.enable_callback_tracebacks(False) #True : prints error trace.
        conTemp = sqlite3.connect(dbFileToOpen,isolation_level=None)
        conTemp.text_factory = str
        conTemp.create_function("JsonValue", 1, self.sqliteJsonValue)
        conTemp.create_function("JsonKeyValPair", 2, self.sqliteJsonKeyValPair)
        conTemp.create_function("SPFRegexReplace", 5, self.sqliteRegexReplace)
        conTemp.create_function("SPFRegexSearch", 3, self.sqliteRegexSearch)
        conTemp.create_function("SPFWriteLOBToFile", 2, self.sqliteSaveLOBToFile)
        conTemp.create_function("SPFPrepLikeValue", 1, self.sqlitePrepLikeValue)
        if AttachSQL is not None:
            conTemp.execute(AttachSQL)
        return conTemp

    def get_SQLite_Encoding(self, conObj):
        """
        get : Encoding of the data loaded into SQLite DB
        """
        out_Encoding = None
        try:
            #create a cursor object from conObj
            mycur = conObj.cursor()
            rows = mycur.execute(self.SQLITE_GET_ENCODING_SQL).fetchall()
            self.logger.debug(f"fetch {self.SQLITE_GET_ENCODING_SQL} : {len(rows)}")
            out_Encoding = rows[0][0]
            self.logger.debug(f"out_Encoding : {out_Encoding}")
            return out_Encoding
        except Exception as err:
            raise

    def sqliteJsonValue(self, val1):
        """
        SQLite3 custom function to output the value as a proper JSON string
        """
        return json.dumps(val1)
    
    def sqliteJsonKeyValPair(self, keyName, keyVal):
        """
        SQLite3 custom function to output the value as a proper JSON key:value pair
        """
        return "{0}:{1}".format(json.dumps(keyName), json.dumps(keyVal))

    def sqliteRegexReplace(self, rePattern, stringtocheck, repl="", count=0, flags=2):
        """
        SQLite3 user-defined function to perfrom re.sub on stringtocheck
        defaults:
        repl="" : remove matched text
        count = 0 : replace all
        flags=2 : re.IGNORECASE(2) 
        """
        if self.myUtils.IsEmptyOrNone(rePattern) is False:
            rePatternCompiled = re.compile(rePattern,flags)
            return rePatternCompiled.sub(repl, stringtocheck, count) # will return original stringtocheck if nothing matches the pattern & substitution is not done
        else:
            return stringtocheck

    def sqliteRegexSearch(self, rePattern, stringtocheck, flags=0):
        """
        SQLite3 user-defined function to perform re.search on stringtoCheck
        defaults: (defaults will not work when called from SQLite)
        flags=2 : re.IGNORECASE(2) 
        flags=0 : no regex flags chosing i.e., will perform case sensitive search
        """
        
        if self.myUtils.IsEmptyOrNone(rePattern) is False:
            try:
                flags = int(flags)
            except ValueError as valErr:
                errMsg = f"Unknown RegexFlag value : {flags}\nPlease specify any one of below integer values for RegexFlags. \n0 : case sensitive search\n2 : Case insensitive search"
                self.myUtils.ConsoleWithCons80(errMsg)
                raise Exception(errMsg)

            if flags not in [0,2]:
                errMsg = f"Unknown RegexFlag value : {flags}\nPlease specify any one of below integer values for RegexFlags. \n0 : case sensitive search\n2 : Case insensitive search"
                self.myUtils.ConsoleWithCons80(errMsg)
                raise Exception(errMsg)

            rePatternCompiled = re.compile(rePattern,flags)
            myMatch = rePatternCompiled.search(stringtocheck) # will return None (NULL) if nothing matches the pattern
            if myMatch is not None:
                return myMatch.group()
            else:
                return None
        else:
            return stringtocheck

    def sqliteSaveLOBToFile(self, lobCol, filename):
                            #, writeMode="w"
                            #, writeDataType="b", exitOnError="N"
                            #, outputValueOnError=""
                            #, appendErrorMessageToOutput="Y"
                            #, displayErrorToConsole = "Y"):
        """
        SQLite3 user-defined function to handle LOB data 
        perform read a column of base64 string data, convert to binary and save binary(BLOB) data to a file
        (BLOB column data retreived from db and stored in csv file as base64)
        """
        try:
            #self.myUtils.Console(len(lobCol))
            #with open(filename, "f{writeMode}{writeDataType}") as wrtr:
            with open(filename, "wb") as wrtr:
                wrtr.write(base64.b64decode(lobCol))
        except Exception as err:
            #if self.myUtils.IsEmptyOrNone(outputValueOnError) is True:
            #    outputValueOnError = f"Fail : '{filename}'"
            #if appendErrorMessageToOutput.upper() in ['Y', 'YES']:
            #    outputValueOnError = f"{outputValueOnError} -- Error : '{err}'"
            #if displayErrorToConsole.upper() in ['Y', 'YES']:
            #    self.myUtils.ConsoleWithCons80(outputValueOnError)
            #if exitOnError.upper() in ["Y", "YES"]:
            #    return outputValueOnError
            outputValueOnError = f"Fail : '{filename}'"
            outputValueOnError = f"{outputValueOnError} -- Error : '{err}'"
            return outputValueOnError
        return f"{filename}"

    def sqlitePrepLikeValue(self, val1)-> str:
        r"""
        SQLite3 user-defined function (UDF) to prepare the column value for 'LIKE/NOT LIKE' clause. Handling '%' & '_' wild card
        
        if '%' or '_' present in val1 ...then use the value as is
            e.g., 
            if value = 'A%BC' --> 'A%BC'
            if value = 'A_BC' --> 'A_BC'
            if value = 'A\%BC' --> 'A\%BC%'
            if value = 'A\_BC' --> 'A\_BC%'
        else
            e.g., if value = 'ABC' --> 'ABC%'
        INPUT : 
        val1 : str --> column value
        """
        try:
            percentWildCar_found = False #unescpaed wild card not found
            # print(f"val1 : {val1}")
            if '%' in val1 or '_' in val1:
                percentWildCardPosMulti = [match.start() for match in re.finditer("%|_", val1)]
                # print(f'percentWildCardPosMulti : {percentWildCardPosMulti}')
                if percentWildCardPosMulti[0] == 0:
                    #first value is an unescaped wildcard
                    # print('found % at pos 0')
                    percentWildCar_found = True

                if percentWildCar_found is False: 
                    #unescaped wildcard not found check all occurances until found
                    for percentWildCardPos in percentWildCardPosMulti:
                        if val1[percentWildCardPos - 1] != "\\":
                            percentWildCar_found = True
                            break
            # print(f"percentWildCar_found : {percentWildCar_found}")
            if percentWildCar_found is True:
                return val1
            else:
                return f"{val1}%"
        except Exception as err:
            return val1
#END : class MemTable(object)
if __name__ == "__main__":
    print("Running as main...noting to execute")
