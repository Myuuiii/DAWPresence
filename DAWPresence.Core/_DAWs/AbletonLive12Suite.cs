using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class AbletonLive12Suite : Daw
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