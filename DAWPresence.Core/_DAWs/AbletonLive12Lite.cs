using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class AbletonLive12Lite : Daw
{
    [SetsRequiredMembers]
    public AbletonLive12Lite()
    {
        ProcessName = "Ableton Live 12 Lite";
        DisplayName = ProcessName;
        ImageKey = "ableton";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 23;
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
