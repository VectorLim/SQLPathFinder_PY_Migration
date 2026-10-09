using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SQLPathFinder3.My;

namespace SQLPathFinder3;

[StandardModule]
internal sealed class Globals_Renamed
{
	public struct SQLiteAF_Type
	{
		public int Level;

		public string AF_Type;

		public string AF;

		public string ColHdr;

		public string fn;

		public string expr;

		public string PB;

		public string OB;
	}

	public struct SQLiteAF_Level_Type
	{
		public int Level;

		public bool AF_Exists;
	}

	public struct fPostExtArr_Type
	{
		public int row;

		public string mode;

		public string show;
	}

	public struct Report_Format_Type
	{
		public string ObjType;

		public string Name;

		public string Value;
	}

	public struct QSamples_Type
	{
		public string Name;

		public string Description;

		public string Misc;
	}

	public struct QColumns_Type
	{
		public string No;

		public string ColAlias;

		public string Header;

		public string Show;

		public string Sort;

		public string Statistics;

		public string Pivot;

		public string level;

		public string DataType;

		public string Comment;

		public string List;

		public string Column;

		public string ColID;

		public string AllStat;

		public string LHeader;
	}

	public struct QFilters_Type
	{
		public string And_Renamed;

		public string ParentO;

		public string Column;

		public string Operator_Renamed;

		public string Value1;

		public string Value2;

		public string ParenC;

		public string Key;

		public string level;

		public string DataType;

		public string List;

		public string ColHead;
	}

	public struct QJoins_Type
	{
		public string And_Renamed;

		public string ParenO;

		public string Col1;

		public string Join_Renamed;

		public string Col2;

		public string ParenC;
	}

	public struct Table_Type
	{
		public string ObjectType;

		public string DatabaseType;

		public string lbl0;

		public string lbl1;

		public string lbl2;

		public string lbl3;

		public string lbl4;

		public string lbl5;

		public string lbl6;

		public string lbl7;

		public string lbl8;

		public string lbl9;

		public string lbl10;

		public string Show;

		public string Name;

		public string Display;

		public string TableType;

		public string fileschema;

		public string Description;

		public string Tag;
	}

	public struct TableType_Type
	{
		public string DatabaseType;

		public string Database;
	}

	public struct Functions_Type
	{
		public string Name;

		public string Type;

		public string Description;
	}

	public struct gOperators_Type
	{
		public string DB_Type;

		public string Name;

		public string Type;

		public string Description;
	}

	public struct Nodes_Type
	{
		public string Name;

		public string ORCL;

		public string DBDATA;

		public string DBDATA2;

		public string Desc;

		public string Display;

		public string UN;

		public string PW;
	}

	public struct Schema_Type
	{
		public string Name;
	}

	public struct QueryGrid_Type
	{
		public string Col0;

		public string Col1;

		public string Col2;

		public string Col3;

		public string Col4;

		public string Col5;

		public string Col6;

		public string Col7;

		public string Col8;

		public string Col9;

		public string Col10;
	}

	public struct FromTable_Type
	{
		public string Table;

		public string Schema;

		public string Node;
	}

	public struct NoElements_Type
	{
		public short Count;
	}

	public struct gVar_Type
	{
		public string GMode;

		public string Variable;

		public string Value;

		public string SHValue;
	}

	public struct Table_Lists_Type
	{
		[VBFixedArray(400)]
		public string[] List;

		[VBFixedArray(400)]
		public string[] Table;

		[VBFixedArray(400)]
		public string[] set1;

		[VBFixedArray(400)]
		public string[] list_actual_set;

		public void Initialize()
		{
			List = new string[401];
			Table = new string[401];
			set1 = new string[401];
			list_actual_set = new string[401];
		}
	}

	public struct Table_Lists_Dtls_Type
	{
		[VBFixedArray(900)]
		public string[] List;

		[VBFixedArray(900)]
		public string[] Table;

		[VBFixedArray(900)]
		public string[] set1;

		[VBFixedArray(900)]
		public string[] list_actual_set;

		public void Initialize()
		{
			List = new string[901];
			Table = new string[901];
			set1 = new string[901];
			list_actual_set = new string[901];
		}
	}

	public struct Filter_Type
	{
		[VBFixedArray(50)]
		public string[] Col1;

		[VBFixedArray(50)]
		public string[] Col2;

		[VBFixedArray(50)]
		public string[] File;

		[VBFixedArray(50)]
		public string[] set1;

		[VBFixedArray(50)]
		public string[] set2;

		public void Initialize()
		{
			Col1 = new string[51];
			Col2 = new string[51];
			File = new string[51];
			set1 = new string[51];
			set2 = new string[51];
		}
	}

	public struct Join_Lists_Type
	{
		[VBFixedArray(400)]
		public string[] List;

		[VBFixedArray(400)]
		public string[] Join_Renamed;

		[VBFixedArray(400)]
		public string[] set1;

		[VBFixedArray(400)]
		public string[] list_actual_set;

		public void Initialize()
		{
			List = new string[401];
			Join_Renamed = new string[401];
			set1 = new string[401];
			list_actual_set = new string[401];
		}
	}

	public struct Set_Lists_Type
	{
		[VBFixedArray(400)]
		public string[] List1;

		[VBFixedArray(400)]
		public string[] list2;

		public void Initialize()
		{
			List1 = new string[401];
			list2 = new string[401];
		}
	}

	public struct Set_Hints_Type
	{
		[VBFixedArray(25)]
		public string[] List;

		[VBFixedArray(25)]
		public string[] Hint;

		[VBFixedArray(25)]
		public string[] list_actual_set;

		public void Initialize()
		{
			List = new string[26];
			Hint = new string[26];
			list_actual_set = new string[26];
		}
	}

	public struct Set_Hints_Filters_Type
	{
		[VBFixedArray(55)]
		public string[] List;

		[VBFixedArray(55)]
		public string[] Hint;

		[VBFixedArray(55)]
		public string[] list_actual_set;

		public void Initialize()
		{
			List = new string[56];
			Hint = new string[56];
			list_actual_set = new string[56];
		}
	}

	public struct PreSQL_Type
	{
		[VBFixedArray(100)]
		public string[] List1;

		[VBFixedArray(100)]
		public string[] list2;

		[VBFixedArray(100)]
		public string[] set1;

		[VBFixedArray(100)]
		public string[] list_actual_set;

		public void Initialize()
		{
			List1 = new string[101];
			list2 = new string[101];
			set1 = new string[101];
			list_actual_set = new string[101];
		}
	}

	public struct Client_Type
	{
		public string List1;

		public string list2;

		public string set_Renamed;

		public string list_actual_set;
	}

	public struct AliasArray_Type
	{
		public string Alias_Renamed;

		public string DBType;

		public string Tag;

		public string Prompt;
	}

	public struct Exe_Type
	{
		public string Type;

		public string ID;

		public string File;

		public string File2;
	}

	public struct SQL_Tables_Type
	{
		[VBFixedArray(3500)]
		public string[] List;

		[VBFixedArray(3500)]
		public string[] global_list;

		[VBFixedArray(3500)]
		public string[] Column;

		[VBFixedArray(3500)]
		public string[] Col_Type;

		[VBFixedArray(3500)]
		public string[] Column_True;

		[VBFixedArray(3500)]
		public string[] Label;

		[VBFixedArray(3500)]
		public string[] Column_Help;

		public void Initialize()
		{
			List = new string[3501];
			global_list = new string[3501];
			Column = new string[3501];
			Col_Type = new string[3501];
			Column_True = new string[3501];
			Label = new string[3501];
			Column_Help = new string[3501];
		}
	}

	public struct SubQueries_Type
	{
		public string ID;

		public string Data;

		public string OldNode;

		public string OldTag;

		public string OldText;
	}

	public struct gOptOutput_Type
	{
		public string Variable;

		public string JSL;

		public string Label;

		public string Show;

		public string Return_Value;

		public string Default_value;

		public string Help;

		public string Selected;
	}

	public struct StdControls_Type
	{
		public string ID;

		public string Type;

		public string Token;

		public string Value;

		public string Default_val;

		public string JSL;

		public string Arg1;

		public string Map;
	}

	public struct gGridColor_Type
	{
		public Color Color;
	}

	public const string gOracleLocalPath = "\\oracle\\instantclient_19_17";

	public const string gChartPostFix = "___";

	public const string gInstallShare0 = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Updates\\UpdatesIII";

	public const string gSecIni = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Config\\sec\\sec.ini";

	public const string g_RptDlm = "<\\\\>";

	public const string gCommaSubstitute = "</comma\\>";

	public const string g_ChartExtraDLM = " ** ";

	public static string gCmdWinColor = "WHITEDARKBLUE";

	public static string gCmdWinHist = "N";

	public static int gfrmlog_x = 0;

	public static int gfrmlog_y = 0;

	public static int gfrmlog_w = 0;

	public static int gfrmlog_h = 0;

	public const string gJSLDelimiter = "</@#;>";

	public const short gMinTreeNodes = 12;

	public const string gBatchHdr = "!SPF-BATCH-ATTRIBUTES->";

	public const string Site_ptn3 = "\\\\ATDFILE3.CH.INTEL.COM\\ATD-WEB\\PATHFINDING\\SQLPATHFINDER";

	public const string Site_ptn4 = "\\\\AZSACTAPP22.INTEL.COM\\";

	public const string Site_ptn5 = "\\\\AZSTMGTSPATH01";

	public const string g_PivotCol = "[MyPivotedColumn(s)]";

	public const string g_NewQ = "<---- New Query ---->";

	public const string gStackJMP = "StackResults.va";

	public const string Space9 = "         ";

	public const string gColon = "                                                                                                    : ";

	public const string g_CEcrlf = "!!!!!";

	public const string Site_ptn = "*SITE SPECIFIC]*";

	public const string Site_ptn2 = "[Site Specific] ";

	public const string g_Hlpf = "SPF_Help_Data.txt";

	public const string g_SPFSQL = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Library\\SPFSQL3.VA";

	public const string g_NETPath = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\NET\\";

	public const string g_TNSPath = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\tnsdir\\";

	public const string gUtilPath = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Software\\Library\\";

	public const string gInlineToken = "@IF@";

	public const string gSummPlus = "s+";

	public const string gImportXL = "ImportExcel.va";

	public const string gLoadXL = "LoadExcel.va";

	public const short gMaxTables = 32;

	public const short gNScripts = 600;

	public const string MyFetchSize = "FetchSize=500";

	public const short asc_enter = 13;

	public const short CF_TEXT = 1;

	public const short ColQ_Alias = 0;

	public const short ColQ_Header = 1;

	public const short ColQ_Display = 2;

	public const short ColQ_Sort = 3;

	public const short ColQ_Summ = 4;

	public const short ColQ_CrossTab = 5;

	public const short ColQ_Level = 6;

	public const short ColQ_GSumm = 7;

	public const short ColQ_DataType = 8;

	public const short COlQ_Comment = 9;

	public const short ColQ_List = 10;

	public const short ColQ_Name = 11;

	public const short ColQ_ID = 12;

	public const short gMaxCols = 3500;

	public const short gMaxSQLCols = 1999;

	public const short gMaxAliasArray = 133;

	public const short ColJ_And = 0;

	public const short ColJ_ParenO = 1;

	public const short Colj_Col1 = 2;

	public const short ColJ_Opr = 3;

	public const short ColJ_Col2 = 4;

	public const short ColJ_ParenC = 5;

	public const short ColF_And = 0;

	public const short ColF_ParenO = 1;

	public const short ColF_Alias = 2;

	public const short ColF_Opr = 3;

	public const short ColF_Val1 = 4;

	public const short ColF_ParenC = 5;

	public const short ColF_Level = 6;

	public const short ColF_Key = 7;

	public const short ColF_DataType = 8;

	public const short ColF_List = 9;

	public const short ColF_Column = 10;

	public const short ColF_Val2 = 11;

	public const short MaxJoinRows = 200;

	public const int g_MaxSQLiteAF = 100;

	public const string g_AFToken = "/*AF$*/";

	public static int g_NoSQLiteAF = 0;

	public const int g_MaxSQLiteLevels = 21;

	public static Collection g_UIDesc = new Collection();

	public static QColumns_Type[] g_QColumns = new QColumns_Type[2001];

	public static short g_NoQColumns;

	public static QFilters_Type[] g_QFilters = new QFilters_Type[1001];

	public static short g_NoQFilters;

	public static QJoins_Type[] g_QJoins = new QJoins_Type[1001];

	public static short g_NoQJoins;

	public static Table_Type[] Tables = new Table_Type[1401];

	public static int MaxTables;

	public static Functions_Type[] Functions = new Functions_Type[151];

	public static short nofunctions;

	public static gOperators_Type[] gOperators = new gOperators_Type[301];

	public static short gNoOperators;

	public static Nodes_Type[] AllNodes = new Nodes_Type[1501];

	public static short nonodes;

	public static Schema_Type[] AllSchema = new Schema_Type[41];

	public static short noschemas;

	public static QueryGrid_Type[] GridQuery_Curr = new QueryGrid_Type[1];

	public static FromTable_Type[] FromTables = new FromTable_Type[33];

	public static short MaxFromTables;

	public const int gMaxGlobals = 100;

	public static gVar_Type[] gGlobals = new gVar_Type[101];

	public static int gNoGlobals = -1;

	public static string gGlobalsFile = "";

	public static bool gISSHSave = false;

	public const short gNoTableLists = 400;

	public const short gNoTablelistsDtls = 900;

	public const short gNoFilters = 50;

	public const short gNoJoinLists = 400;

	public const short gNoSets = 400;

	public const short gNoHints = 25;

	public const short gNoHintsFilter = 55;

	public const short gnopresql = 100;

	public const short gMaxSubQueries = 100;

	public static SubQueries_Type[] SubQueries = new SubQueries_Type[101];

	public const int gNoOptions = 26;

	private static string[,] OptOutput = new string[27, 51];

	public static gOptOutput_Type[] gOptOptions = new gOptOutput_Type[27];

	public const int g_MaxStdCtrls = 125;

	public static StdControls_Type[] g_StdCtrls = new StdControls_Type[126];

	public static short g_NoStdCtrls;

	public static gGridColor_Type[] gGridColor = new gGridColor_Type[37];

	public static Color[] gStdColors = new Color[37];

	public static string[,] g_GVars = new string[2, 20];

	public static int g_VarNo = -1;

	public const int gMaxViewCount = 100;

	public static string[] gView_Dict = new string[101];

	public static int gViewCount = 0;

	public static string MyPy3Ver = "";

	public static string gPyDebug = "N";

	public static string gUseMinPath = "Y";

	public static string gWebNext = "N";

	public static string gEncodeFFS = "N";

	public static string gConvertMAOUber = "N";

	public static string gSHType = "PY";

	public static string gEncodeUTFBOM = "N";

	public static int gDelBatFile = 0;

	public static string gHadoopServer = "ATD_ATM.HADOOP";

	public static bool gSavePCDir = true;

	public static string gUseRegistry = "N";

	public static string gInstallShare = "\\\\atdfile3.ch.intel.com\\atd-web\\PathFinding\\SQLPathFinder\\Updates\\UpdatesIII";

	public static string gJSLOptions = "";

	public static string gROptions = "";

	public static string gLastSHSite = "Arizona";

	public static string gCBOptions = "";

	public static string gWFOptions = "";

	public static string ggnuRptOptions = "<OPTIONS>\r\n/REPORT=gnuchart\r\n</OPTIONS>\r\n";

	public static bool g_PrePostQueryOpen = false;

	public static string guDomain = "";

	public static bool g_IsSHGUI = false;

	public static string gEmailOutlook = "N";

	public static string gSPFCmdWin = "N";

	public static string gSPFCmdWinInit = "";

	public static string gUsePyEngine = "Y";

	public static string gUsePyEngineOVR = "U";

	public static string MYMSAccessTitle = "";

	public static string gSPFCache = "";

	public static string IniFileName = "";

	public static bool gMAOATM = false;

	public static bool gDETDARIES = false;

	public static int gATDMCS2 = -1;

	public static bool gMongoKSDie = false;

	public static bool gCheckedMongoKSDie = false;

	public static string gBaseSHGUIDir = "";

	public static string CRLF = "\r\n";

	public static string MySchemaDir = "";

	public static string gChartDir = "";

	public static string gWinuser = "";

	public static string MyPCDir = "";

	public static string gQueryDir = "";

	public static string MyExcelPath = "";

	public static string MyJMPPath = "";

	public static string gMyRPath = "";

	public static string gMyPyPath = MyProject.Application.Info.DirectoryPath + "\\Python3\\python.exe";

	public static bool gCanUsePyEE = false;

	public static string gMidasDriver = "Crystal Ball";

	public static Process Excel_hwndProcess = new Process();

	public static Process JMP_hwndProcess = new Process();

	public static short JMP_Connected = 0;

	public static Process CSV_hwndProcessM = new Process();

	public static short CSV_ConnectedM = 0;

	public static short Excel_Connected = 0;

	public static short Excel_Connected2 = 0;

	public static string gSHGUIDir = "";

	public static string MyDBSchema = "";

	public static string MyMARSServer = "";

	public static string MyARIESServer = "";

	public static string MyOASysServer = "";

	public static string MyOtherServer = "";

	public static string MyMARSServer1 = "Default";

	public static string MyARIESServer1 = "Default";

	public static string MyARIESClassServer1 = "CR.ARIES";

	public static string MyOASysServer1 = "Default";

	public static string MyOtherServer1 = "Default";

	public static string MyDBSchema1 = "Default";

	public static Process MS_hwndProcess = new Process();

	public static short MSAccess_Connected = 0;

	public static short Design_Mode = 0;

	public static bool g_RefreshViews = false;

	public static short g_FrmIdx = 0;

	public static string g_LastTblIdx = Conversions.ToString(0);

	public static string gOrclRetry = "N";

	public static bool gPurgeColPatternFiles = false;

	public static string currDataAny = "";

	public static string currlvltmp = "";

	public static string g_TmpSPFVG2 = "";

	public static short currcoltmp = -1;

	public static short currrowtmp = -1;

	public static string currtxttmp = "";

	public static string curroprtmp = "";

	public static string currdatatypetmp = "";

	public static string currprompttmp = "";

	public static string currkeytypetmp = "";

	public static string currvaluetmp = "";

	public static string currhelptmp = "";

	public static string currinputstrtmp = "";

	public static string currlisttmp = "";

	public static string MyCalValue = "";

	public static string gCalOffset = "";

	public static string MyCalDateType = "";

	public static short currinttmp = 0;

	public static short QueryCancel = 0;

	public static string gWorkQuery = "";

	public static string ODBC_Prompt = "";

	public static string ExcelPending = "";

	public static string g_TS = "";

	public const short SWP_NOMOVE = 2;

	public const short SWP_NOSIZE = 1;

	public const bool SWP_FLAGS = true;

	public const short HWND_TOP = 0;

	public const short HWND_TOPMOST = -1;

	public const short HWND_NOTOPMOST = -2;

	public const short SW_SHOWMINIMIZED = 2;

	public const short SW_SHOWNORMAL = 1;

	public const short GW_HWNDNEXT = 2;

	public const short GWW_HINSTANCE = -6;

	public const short GW_CHILD = 5;

	[DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "GetPrivateProfileStringA", ExactSpelling = true, SetLastError = true)]
	public static extern int GetPrivateProfileString([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpApplicationName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpKeyName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpDefault, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpReturnedString, int nSize, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName);

	[DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "WritePrivateProfileStringA", ExactSpelling = true, SetLastError = true)]
	public static extern int WritePrivateProfileString([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpApplicationName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpKeyName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFileName);

	[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern int SetWindowPos(int Hwnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int wFlags);

	[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern int ShowWindow(int Hwnd, int nCmdShow);

	[DllImport("kernel32", CharSet = CharSet.Ansi, EntryPoint = "GetWindowsDirectoryA", ExactSpelling = true, SetLastError = true)]
	public static extern int GetWindowsDirectory([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpBuffer, int nSize);

	[DllImport("advapi32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetUserNameA", ExactSpelling = true, SetLastError = true)]
	public static extern int GetUserName([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpBuffer, ref int nSize);

	[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "FindWindowA", ExactSpelling = true, SetLastError = true)]
	public static extern int FindWindow([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpClassName, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpWindowName);

	[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern int GetWindow(int Hwnd, int wCmd);

	[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "GetWindowTextA", ExactSpelling = true, SetLastError = true)]
	public static extern int GetWindowText(int Hwnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString, int cch);

	[DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern int GetDesktopWindow();

	[DllImport("user32", CharSet = CharSet.Ansi, EntryPoint = "SendMessageA", ExactSpelling = true, SetLastError = true)]
	public static extern int SendMessage(int Hwnd, int wMsg, int wParam, ref int lParam);

	[DllImport("shell32.dll", CharSet = CharSet.Ansi, EntryPoint = "ShellExecuteA", ExactSpelling = true, SetLastError = true)]
	public static extern int ShellExecute(int Hwnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpOperation, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpFile, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpParameters, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpDirectory, int nShowCmd);
}
