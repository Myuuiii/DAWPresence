using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class AbletonLive9Lite : Daw
{
    [SetsRequiredMembers]
    public AbletonLive9Lite()
    {
        ProcessName = "Ableton Live 9 Lite";
        DisplayName = ProcessName;
        ImageKey = "ableton";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 22;
    }

    public override string ParseProjectName(string title)
    {
        return title.Contains(WindowTrim)
            ? title[..^TitleOffset]
            : "";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}
