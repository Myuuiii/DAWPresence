using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class DebugW : Daw
{
    public DebugW()
    {
        ProcessName = "DAWPresenceBackgroundApp";
        DisplayName = ProcessName;
        ImageKey = "ableton-white";
        ApplicationId = "1053952444859686983";
        WindowTrim = "";
        TitleOffset = 0;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        Process? process = GetProcess();
        if (process is null) return "";
        string title = process.MainWindowTitle;
        return title.Contains(WindowTrim)
            ? TitleRegex().Match(title[..^TitleOffset]).Value
            : "";
    }

    [GeneratedRegex("[^\\[]*")]
    private static partial Regex TitleRegex();
}