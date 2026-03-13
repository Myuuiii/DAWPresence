using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class AbletonLive12Suite : Daw
{
    [SetsRequiredMembers]
    public AbletonLive12Suite()
    {
        ProcessName = "Ableton Live 12 Suite";
        DisplayName = ProcessName;
        ImageKey = "ableton";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 24;
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
