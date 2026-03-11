using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public partial class AbletonLive9Standard : Daw
{
    [SetsRequiredMembers]
    public AbletonLive9Standard()
    {
        ProcessName = "Ableton Live 9 Standard";
        DisplayName = ProcessName;
        ImageKey = "ableton-white";
        ApplicationId = "1053952444859686983";
        WindowTrim = " - " + DisplayName;
        TitleOffset = 26;
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