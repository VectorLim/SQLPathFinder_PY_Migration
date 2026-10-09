using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;

namespace SQLPathFinder3.My;

[StandardModule]
[HideModuleName]
[GeneratedCode("MyTemplate", "11.0.0.0")]
internal sealed class MyProject
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	[MyGroupCollection("System.Windows.Forms.Form", "Create__Instance__", "Dispose__Instance__", "My.MyProject.Forms")]
	internal sealed class MyForms
	{
		[ThreadStatic]
		private static Hashtable m_FormBeingCreated;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmAbout m_FrmAbout;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmAtBot m_FrmAtBot;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmBatch4 m_FrmBatch4;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmbinscounters m_frmbinscounters;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmCalendar m_FrmCalendar;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmChart m_FrmChart;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmCmdSim m_FrmCmdSim;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmComputedSQL m_FrmComputedSQL;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmconfigure m_frmconfigure;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmCSS m_FrmCSS;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmDTREdit m_FrmDTREdit;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmEmail m_FrmEmail;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmfilename m_frmfilename;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmFind m_frmFind;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmgetdata m_frmgetdata;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmGnuChart m_FrmGnuChart;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmInput m_FrmInput;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmJMPLoad m_FrmJMPLoad;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmJMPWindow m_FrmJMPWindow;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmJS m_FrmJS;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmJSChart m_FrmJSChart;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmMain m_FrmMain;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmMegaField m_FrmMegaField;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmMultiQuery m_FrmMultiQuery;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmNode m_FrmNode;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmNodeSel m_FrmNodeSel;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmReport m_FrmReport;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmReporti m_FrmReporti;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmRWMap m_FrmRWMap;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmSampleQs m_FrmSampleQs;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmSetGlobals m_FrmSetGlobals;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public frmSQLfilter m_frmSQLfilter;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmSQLQuerya m_FrmSQLQuerya;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public Frmtable2 m_Frmtable2;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmTabMenu m_FrmTabMenu;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmText m_FrmText;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public FrmUtil m_FrmUtil;

		public FrmAbout FrmAbout
		{
			[DebuggerHidden]
			get
			{
				m_FrmAbout = Create__Instance__(m_FrmAbout);
				return m_FrmAbout;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmAbout)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmAbout);
				}
			}
		}

		public FrmAtBot FrmAtBot
		{
			[DebuggerHidden]
			get
			{
				m_FrmAtBot = Create__Instance__(m_FrmAtBot);
				return m_FrmAtBot;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmAtBot)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmAtBot);
				}
			}
		}

		public FrmBatch4 FrmBatch4
		{
			[DebuggerHidden]
			get
			{
				m_FrmBatch4 = Create__Instance__(m_FrmBatch4);
				return m_FrmBatch4;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmBatch4)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmBatch4);
				}
			}
		}

		public frmbinscounters frmbinscounters
		{
			[DebuggerHidden]
			get
			{
				m_frmbinscounters = Create__Instance__(m_frmbinscounters);
				return m_frmbinscounters;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_frmbinscounters)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmbinscounters);
				}
			}
		}

		public FrmCalendar FrmCalendar
		{
			[DebuggerHidden]
			get
			{
				m_FrmCalendar = Create__Instance__(m_FrmCalendar);
				return m_FrmCalendar;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmCalendar)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmCalendar);
				}
			}
		}

		public FrmChart FrmChart
		{
			[DebuggerHidden]
			get
			{
				m_FrmChart = Create__Instance__(m_FrmChart);
				return m_FrmChart;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmChart)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmChart);
				}
			}
		}

		public FrmCmdSim FrmCmdSim
		{
			[DebuggerHidden]
			get
			{
				m_FrmCmdSim = Create__Instance__(m_FrmCmdSim);
				return m_FrmCmdSim;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmCmdSim)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmCmdSim);
				}
			}
		}

		public FrmComputedSQL FrmComputedSQL
		{
			[DebuggerHidden]
			get
			{
				m_FrmComputedSQL = Create__Instance__(m_FrmComputedSQL);
				return m_FrmComputedSQL;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmComputedSQL)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmComputedSQL);
				}
			}
		}

		public frmconfigure frmconfigure
		{
			[DebuggerHidden]
			get
			{
				m_frmconfigure = Create__Instance__(m_frmconfigure);
				return m_frmconfigure;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_frmconfigure)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmconfigure);
				}
			}
		}

		public FrmCSS FrmCSS
		{
			[DebuggerHidden]
			get
			{
				m_FrmCSS = Create__Instance__(m_FrmCSS);
				return m_FrmCSS;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmCSS)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmCSS);
				}
			}
		}

		public FrmDTREdit FrmDTREdit
		{
			[DebuggerHidden]
			get
			{
				m_FrmDTREdit = Create__Instance__(m_FrmDTREdit);
				return m_FrmDTREdit;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmDTREdit)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmDTREdit);
				}
			}
		}

		public FrmEmail FrmEmail
		{
			[DebuggerHidden]
			get
			{
				m_FrmEmail = Create__Instance__(m_FrmEmail);
				return m_FrmEmail;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmEmail)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmEmail);
				}
			}
		}

		public frmfilename frmfilename
		{
			[DebuggerHidden]
			get
			{
				m_frmfilename = Create__Instance__(m_frmfilename);
				return m_frmfilename;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_frmfilename)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmfilename);
				}
			}
		}

		public frmFind frmFind
		{
			[DebuggerHidden]
			get
			{
				m_frmFind = Create__Instance__(m_frmFind);
				return m_frmFind;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_frmFind)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmFind);
				}
			}
		}

		public frmgetdata frmgetdata
		{
			[DebuggerHidden]
			get
			{
				m_frmgetdata = Create__Instance__(m_frmgetdata);
				return m_frmgetdata;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_frmgetdata)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmgetdata);
				}
			}
		}

		public FrmGnuChart FrmGnuChart
		{
			[DebuggerHidden]
			get
			{
				m_FrmGnuChart = Create__Instance__(m_FrmGnuChart);
				return m_FrmGnuChart;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmGnuChart)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmGnuChart);
				}
			}
		}

		public FrmInput FrmInput
		{
			[DebuggerHidden]
			get
			{
				m_FrmInput = Create__Instance__(m_FrmInput);
				return m_FrmInput;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmInput)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmInput);
				}
			}
		}

		public FrmJMPLoad FrmJMPLoad
		{
			[DebuggerHidden]
			get
			{
				m_FrmJMPLoad = Create__Instance__(m_FrmJMPLoad);
				return m_FrmJMPLoad;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmJMPLoad)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmJMPLoad);
				}
			}
		}

		public FrmJMPWindow FrmJMPWindow
		{
			[DebuggerHidden]
			get
			{
				m_FrmJMPWindow = Create__Instance__(m_FrmJMPWindow);
				return m_FrmJMPWindow;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmJMPWindow)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmJMPWindow);
				}
			}
		}

		public FrmJS FrmJS
		{
			[DebuggerHidden]
			get
			{
				m_FrmJS = Create__Instance__(m_FrmJS);
				return m_FrmJS;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmJS)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmJS);
				}
			}
		}

		public FrmJSChart FrmJSChart
		{
			[DebuggerHidden]
			get
			{
				m_FrmJSChart = Create__Instance__(m_FrmJSChart);
				return m_FrmJSChart;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmJSChart)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmJSChart);
				}
			}
		}

		public FrmMain FrmMain
		{
			[DebuggerHidden]
			get
			{
				m_FrmMain = Create__Instance__(m_FrmMain);
				return m_FrmMain;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmMain)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmMain);
				}
			}
		}

		public FrmMegaField FrmMegaField
		{
			[DebuggerHidden]
			get
			{
				m_FrmMegaField = Create__Instance__(m_FrmMegaField);
				return m_FrmMegaField;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmMegaField)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmMegaField);
				}
			}
		}

		public FrmMultiQuery FrmMultiQuery
		{
			[DebuggerHidden]
			get
			{
				m_FrmMultiQuery = Create__Instance__(m_FrmMultiQuery);
				return m_FrmMultiQuery;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmMultiQuery)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmMultiQuery);
				}
			}
		}

		public FrmNode FrmNode
		{
			[DebuggerHidden]
			get
			{
				m_FrmNode = Create__Instance__(m_FrmNode);
				return m_FrmNode;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmNode)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmNode);
				}
			}
		}

		public FrmNodeSel FrmNodeSel
		{
			[DebuggerHidden]
			get
			{
				m_FrmNodeSel = Create__Instance__(m_FrmNodeSel);
				return m_FrmNodeSel;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmNodeSel)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmNodeSel);
				}
			}
		}

		public FrmReport FrmReport
		{
			[DebuggerHidden]
			get
			{
				m_FrmReport = Create__Instance__(m_FrmReport);
				return m_FrmReport;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmReport)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmReport);
				}
			}
		}

		public FrmReporti FrmReporti
		{
			[DebuggerHidden]
			get
			{
				m_FrmReporti = Create__Instance__(m_FrmReporti);
				return m_FrmReporti;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmReporti)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmReporti);
				}
			}
		}

		public FrmRWMap FrmRWMap
		{
			[DebuggerHidden]
			get
			{
				m_FrmRWMap = Create__Instance__(m_FrmRWMap);
				return m_FrmRWMap;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmRWMap)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmRWMap);
				}
			}
		}

		public FrmSampleQs FrmSampleQs
		{
			[DebuggerHidden]
			get
			{
				m_FrmSampleQs = Create__Instance__(m_FrmSampleQs);
				return m_FrmSampleQs;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmSampleQs)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmSampleQs);
				}
			}
		}

		public FrmSetGlobals FrmSetGlobals
		{
			[DebuggerHidden]
			get
			{
				m_FrmSetGlobals = Create__Instance__(m_FrmSetGlobals);
				return m_FrmSetGlobals;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmSetGlobals)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmSetGlobals);
				}
			}
		}

		public frmSQLfilter frmSQLfilter
		{
			[DebuggerHidden]
			get
			{
				m_frmSQLfilter = Create__Instance__(m_frmSQLfilter);
				return m_frmSQLfilter;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_frmSQLfilter)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_frmSQLfilter);
				}
			}
		}

		public FrmSQLQuerya FrmSQLQuerya
		{
			[DebuggerHidden]
			get
			{
				m_FrmSQLQuerya = Create__Instance__(m_FrmSQLQuerya);
				return m_FrmSQLQuerya;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmSQLQuerya)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmSQLQuerya);
				}
			}
		}

		public Frmtable2 Frmtable2
		{
			[DebuggerHidden]
			get
			{
				m_Frmtable2 = Create__Instance__(m_Frmtable2);
				return m_Frmtable2;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_Frmtable2)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_Frmtable2);
				}
			}
		}

		public FrmTabMenu FrmTabMenu
		{
			[DebuggerHidden]
			get
			{
				m_FrmTabMenu = Create__Instance__(m_FrmTabMenu);
				return m_FrmTabMenu;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmTabMenu)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmTabMenu);
				}
			}
		}

		public FrmText FrmText
		{
			[DebuggerHidden]
			get
			{
				m_FrmText = Create__Instance__(m_FrmText);
				return m_FrmText;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmText)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmText);
				}
			}
		}

		public FrmUtil FrmUtil
		{
			[DebuggerHidden]
			get
			{
				m_FrmUtil = Create__Instance__(m_FrmUtil);
				return m_FrmUtil;
			}
			[DebuggerHidden]
			set
			{
				if (value != m_FrmUtil)
				{
					if (value != null)
					{
						throw new ArgumentException("Property can only be set to Nothing");
					}
					Dispose__Instance__(ref m_FrmUtil);
				}
			}
		}

		[DebuggerHidden]
		private static T Create__Instance__<T>(T Instance) where T : Form, new()
		{
			if (Instance == null || Instance.IsDisposed)
			{
				if (m_FormBeingCreated != null)
				{
					if (m_FormBeingCreated.ContainsKey(typeof(T)))
					{
						throw new InvalidOperationException(Utils.GetResourceString("WinForms_RecursiveFormCreate"));
					}
				}
				else
				{
					m_FormBeingCreated = new Hashtable();
				}
				m_FormBeingCreated.Add(typeof(T), null);
				try
				{
					return new T();
				}
				catch (TargetInvocationException ex) when (((Func<bool>)delegate
				{
					// Could not convert BlockContainer to single expression
					ProjectData.SetProjectError(ex);
					return ex.InnerException != null;
				}).Invoke())
				{
					string resourceString = Utils.GetResourceString("WinForms_SeeInnerException", ex.InnerException.Message);
					throw new InvalidOperationException(resourceString, ex.InnerException);
				}
				finally
				{
					m_FormBeingCreated.Remove(typeof(T));
				}
			}
			return Instance;
		}

		[DebuggerHidden]
		private void Dispose__Instance__<T>(ref T instance) where T : Form
		{
			instance.Dispose();
			instance = null;
		}

		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public MyForms()
		{
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object o)
		{
			return base.Equals(RuntimeHelpers.GetObjectValue(o));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal new Type GetType()
		{
			return typeof(MyForms);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[MyGroupCollection("System.Web.Services.Protocols.SoapHttpClientProtocol", "Create__Instance__", "Dispose__Instance__", "")]
	internal sealed class MyWebServices
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override bool Equals(object o)
		{
			return base.Equals(RuntimeHelpers.GetObjectValue(o));
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		internal new Type GetType()
		{
			return typeof(MyWebServices);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerHidden]
		public override string ToString()
		{
			return base.ToString();
		}

		[DebuggerHidden]
		private static T Create__Instance__<T>(T instance) where T : new()
		{
			if (instance == null)
			{
				return new T();
			}
			return instance;
		}

		[DebuggerHidden]
		private void Dispose__Instance__<T>(ref T instance)
		{
			instance = default(T);
		}

		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public MyWebServices()
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[ComVisible(false)]
	internal sealed class ThreadSafeObjectProvider<T> where T : new()
	{
		[CompilerGenerated]
		[ThreadStatic]
		private static T m_ThreadStaticValue;

		internal T GetInstance
		{
			[DebuggerHidden]
			get
			{
				if (m_ThreadStaticValue == null)
				{
					m_ThreadStaticValue = new T();
				}
				return m_ThreadStaticValue;
			}
		}

		[DebuggerHidden]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ThreadSafeObjectProvider()
		{
		}
	}

	private static readonly ThreadSafeObjectProvider<MyComputer> m_ComputerObjectProvider = new ThreadSafeObjectProvider<MyComputer>();

	private static readonly ThreadSafeObjectProvider<MyApplication> m_AppObjectProvider = new ThreadSafeObjectProvider<MyApplication>();

	private static readonly ThreadSafeObjectProvider<User> m_UserObjectProvider = new ThreadSafeObjectProvider<User>();

	private static ThreadSafeObjectProvider<MyForms> m_MyFormsObjectProvider = new ThreadSafeObjectProvider<MyForms>();

	private static readonly ThreadSafeObjectProvider<MyWebServices> m_MyWebServicesObjectProvider = new ThreadSafeObjectProvider<MyWebServices>();

	[HelpKeyword("My.Computer")]
	internal static MyComputer Computer
	{
		[DebuggerHidden]
		get
		{
			return m_ComputerObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.Application")]
	internal static MyApplication Application
	{
		[DebuggerHidden]
		get
		{
			return m_AppObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.User")]
	internal static User User
	{
		[DebuggerHidden]
		get
		{
			return m_UserObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.Forms")]
	internal static MyForms Forms
	{
		[DebuggerHidden]
		get
		{
			return m_MyFormsObjectProvider.GetInstance;
		}
	}

	[HelpKeyword("My.WebServices")]
	internal static MyWebServices WebServices
	{
		[DebuggerHidden]
		get
		{
			return m_MyWebServicesObjectProvider.GetInstance;
		}
	}
}
