using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class Lmms : Daw
{
    [SetsRequiredMembers]
    public Lmms()
    {
        ProcessName = "lmms";
        DisplayName = "LMMS";
        ImageKey = "lmms";
        ApplicationId = "1082950869856821308";
        WindowTrim = " - LMMS";
        TitleOffset = 0;
    }

    public override string ParseProjectName(string title)
    {
        return title.Contains(WindowTrim)
            ? title[..title.IndexOf(WindowTrim, StringComparison.Ordinal)]
            : "";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}