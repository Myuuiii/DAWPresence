using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class AbletonLive11Lite : Daw
{
    [SetsRequiredMembers]
    public AbletonLive11Lite()
    {
        ProcessName = "Ableton Live 11 Lite";
        DisplayName = ProcessName;
        ImageKey = "ableton";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 23;
    }

    public override string ParseProjectName(string title)
    {
        return title.Contains(WindowTrim)
            ? TitleRegex().Match(title[..^TitleOffset]).Value
            : "";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }

    [GeneratedRegex("[^\\[]*")]
    private static partial Regex TitleRegex();
}