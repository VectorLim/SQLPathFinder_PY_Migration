using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public sealed class SelectionResult
{
    [DataMember(Name = "text")] public string Text;
    [DataMember(Name = "truncated")] public bool Truncated;
    [DataMember(Name = "start", EmitDefaultValue = false)] public int? Start;
    [DataMember(Name = "end", EmitDefaultValue = false)] public int? End;
}

[DataContract]
public sealed class SnapshotResult
{
    [DataMember(Name = "targetPid", EmitDefaultValue = false)] public int? TargetPid;
    [DataMember(Name = "windowTitle")] public string WindowTitle;
    [DataMember(Name = "sourceText")] public string SourceText;
    [DataMember(Name = "sourceTruncated")] public bool SourceTruncated;
    [DataMember(Name = "selection")] public SelectionResult Selection;
    [DataMember(Name = "designerValues")] public List<string> DesignerValues;
    [DataMember(Name = "activeControl")] public string ActiveControl;
    [DataMember(Name = "fingerprint")] public string Fingerprint;
    [DataMember(Name = "designerFingerprint", EmitDefaultValue = false)] public string DesignerFingerprint;
    [DataMember(Name = "designerWindowTitle")] public string DesignerWindowTitle;
    [DataMember(Name = "designerBounds", EmitDefaultValue = false)] public int[] DesignerBounds;
    [DataMember(Name = "status")] public string Status;
    [DataMember(Name = "error", EmitDefaultValue = false)] public string Error;
    [DataMember(Name = "bounds", EmitDefaultValue = false)] public int[] Bounds;
}

[DataContract]
public sealed class RefreshResult
{
    [DataMember(Name = "targetPid", EmitDefaultValue = false)] public int? TargetPid;
    [DataMember(Name = "designerWindowTitle")] public string DesignerWindowTitle;
    [DataMember(Name = "designerFingerprint")] public string DesignerFingerprint;
    [DataMember(Name = "sourceText")] public string SourceText;
    [DataMember(Name = "status")] public string Status;
    [DataMember(Name = "error", EmitDefaultValue = false)] public string Error;
}

[DataContract]
public sealed class NativeTargetResult
{
    [DataMember(Name = "targetPid", EmitDefaultValue = false)] public int? TargetPid;
    [DataMember(Name = "designerHwnd", EmitDefaultValue = false)] public long? DesignerHwnd;
    [DataMember(Name = "designerWindowTitle")] public string DesignerWindowTitle;
    [DataMember(Name = "documentName")] public string DocumentName;
}

[DataContract]
public sealed class TargetListResult
{
    [DataMember(Name = "status")] public string Status;
    [DataMember(Name = "targets")] public List<NativeTargetResult> Targets;
    [DataMember(Name = "error", EmitDefaultValue = false)] public string Error;
}