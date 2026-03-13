using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class AbletonLive10Standard : Daw
{
    [SetsRequiredMembers]
    public AbletonLive10Standard()
    {
        ProcessName = "Ableton Live 10 Standard";
        DisplayName = ProcessName;
        ImageKey = "ableton-white";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 27;
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
