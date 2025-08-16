using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class AbletonLive12Lite : Daw
{
    public AbletonLive12Lite()
    {
        ProcessName = "Ableton Live 12 Lite";
        DisplayName = ProcessName;
        ImageKey = "ableton";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 23;
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