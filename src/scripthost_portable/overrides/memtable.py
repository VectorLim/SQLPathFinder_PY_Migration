"""Portable overrides for the archived MemTable class."""

import SPFLib.SPFUtilities.memtable as legacy


class MemTable(legacy.MemTable):
    """Extend the original implementation with portable methods."""

    def Run_SQLite(
        self, Site1=None, Command1=None, MyTables=None, WorkDir=None,
        OutTT=None, OutFile=None, OutExcel=None, FinalRow=None,
        ExPlans=None, MyLocal=None, MyText=None, PreProcCSV=None,
        MySQLite_DT=None, MySQLiteCache=None, ll_QuoteCSV=None,
        MyEmptyNull=True, ll_UniqueHdr=None, l_DBName=None, l_Null=None,
        ll_SQLiteExt=None, ll_NoHdrs=None, ll_Schema=None, MyInstance=None,
        isJQW=None, useSQLiteEXE=False, RowIdStartAt2=False, myAppend=False,
    ):
        """Normalize the POSIX working directory, then run original SQLite."""
        if legacy.os.name != "nt" and WorkDir == ".\\":
            WorkDir = "."
        return super().Run_SQLite(
            Site1=Site1, Command1=Command1, MyTables=MyTables, WorkDir=WorkDir,
            OutTT=OutTT, OutFile=OutFile, OutExcel=OutExcel, FinalRow=FinalRow,
            ExPlans=ExPlans, MyLocal=MyLocal, MyText=MyText, PreProcCSV=PreProcCSV,
            MySQLite_DT=MySQLite_DT, MySQLiteCache=MySQLiteCache,
            ll_QuoteCSV=ll_QuoteCSV, MyEmptyNull=MyEmptyNull, ll_UniqueHdr=ll_UniqueHdr,
            l_DBName=l_DBName, l_Null=l_Null, ll_SQLiteExt=ll_SQLiteExt,
            ll_NoHdrs=ll_NoHdrs, ll_Schema=ll_Schema, MyInstance=MyInstance,
            isJQW=isJQW, useSQLiteEXE=useSQLiteEXE,
            RowIdStartAt2=RowIdStartAt2, myAppend=myAppend,
        )

    def _prepare_sqlite_import_pairs(self, pairs):
        if legacy.os.name == "nt":
            return super()._prepare_sqlite_import_pairs(pairs)
        return list(dict.fromkeys(pairs))

    def _sqlite_preprocessed_temp_path(self, counter, rn):
        if legacy.os.name == "nt":
            return super()._sqlite_preprocessed_temp_path(counter, rn)
        return legacy.os.path.join(".", "{0}_{1}.tmp".format(counter, rn))

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
