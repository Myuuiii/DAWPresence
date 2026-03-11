using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class AbletonLive11Intro : Daw
{
    [SetsRequiredMembers]
    public AbletonLive11Intro()
    {
        ProcessName = "Ableton Live 11 Intro";
        DisplayName = ProcessName;
        ImageKey = "ableton-white";
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