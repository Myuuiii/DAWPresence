using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

/// <summary>
///     Newer Bitwig Studio builds (process "BitwigStudioApp") that report the window
///     title as "Bitwig Studio - {project}" instead of the legacy "{project} - Bitwig Studio".
/// </summary>
public class BitwigStudioApp : Daw
{
    private static readonly Regex TitleRegex =
        new(@"^Bitwig Studio(?:\s+[\d\.]+)?\s*-\s*(?<project>.+)$",
            RegexOptions.Compiled);

    [SetsRequiredMembers]
    public BitwigStudioApp()
    {
        ProcessName = "BitwigStudioApp";
        DisplayName = "Bitwig Studio";
        ImageKey = "icon";
        ApplicationId = "1524459853417025606";
        WindowTrim = "Bitwig Studio - ";
        TitleOffset = 0;
    }

    public override string ParseProjectName(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return "";
        var match = TitleRegex.Match(title);
        return match.Success ? match.Groups["project"].Value.Trim() : "";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}
