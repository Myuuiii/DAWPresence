using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class AbletonLive10Intro : Daw
{
    public AbletonLive10Intro()
    {
        ProcessName = "Ableton Live 10 Intro";
        DisplayName = ProcessName;
        ImageKey = "ableton-white";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 24;
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