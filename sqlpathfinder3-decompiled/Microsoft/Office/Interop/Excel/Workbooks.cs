using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Office.Interop.Excel;

[ComImport]
[CompilerGenerated]
[Guid("000208DB-0000-0000-C000-000000000046")]
[TypeIdentifier]
public interface Workbooks : IEnumerable
{
	void _VtblGap1_4();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(277)]
	[LCIDConversion(0)]
	void Close();

	void _VtblGap2_5();

	[IndexerName("_Default")]
	[DispId(0)]
	Workbook this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	void _VtblGap3_2();

	[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	[DispId(1924)]
	[LCIDConversion(18)]
	void OpenText([In][MarshalAs(UnmanagedType.BStr)] string Filename, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Origin, [Optional][In][MarshalAs(UnmanagedType.Struct)] object StartRow, [Optional][In][MarshalAs(UnmanagedType.Struct)] object DataType, [In] XlTextQualifier TextQualifier = XlTextQualifier.xlTextQualifierDoubleQuote, [Optional][In][MarshalAs(UnmanagedType.Struct)] object ConsecutiveDelimiter, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Tab, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Semicolon, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Comma, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Space, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Other, [Optional][In][MarshalAs(UnmanagedType.Struct)] object OtherChar, [Optional][In][MarshalAs(UnmanagedType.Struct)] object FieldInfo, [Optional][In][MarshalAs(UnmanagedType.Struct)] object TextVisualLayout, [Optional][In][MarshalAs(UnmanagedType.Struct)] object DecimalSeparator, [Optional][In][MarshalAs(UnmanagedType.Struct)] object ThousandsSeparator, [Optional][In][MarshalAs(UnmanagedType.Struct)] object TrailingMinusNumbers, [Optional][In][MarshalAs(UnmanagedType.Struct)] object Local);
}
