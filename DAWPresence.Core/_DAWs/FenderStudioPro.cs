using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class FenderStudioPro : Daw
{
    [SetsRequiredMembers]
    public FenderStudioPro()
    {
        ProcessName = "Studio Pro";
        DisplayName = "Fender Studio Pro";
        ImageKey = "fender-studio";
        ApplicationId = "1481334491325665384";
        WindowTrim = "Studio Pro - ";
        TitleOffset = 0;
    }

    public override string ParseProjectName(string title)
    {
        return title.StartsWith(WindowTrim) ? title[WindowTrim.Length..].TrimEnd('*') : "";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}
