using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class AbletonLive9Lite : Daw
{
    public AbletonLive9Lite()
    {
        ProcessName = "Ableton Live 9 Lite";
        DisplayName = ProcessName;
        ImageKey = "ableton";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 22;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        var title = process.MainWindowTitle;
        return title.Contains(WindowTrim)
            ? TitleRegex().Match(title[..^TitleOffset]).Value
            : "";
    }

    [GeneratedRegex("[^\\[]*")]
    private static partial Regex TitleRegex();
}