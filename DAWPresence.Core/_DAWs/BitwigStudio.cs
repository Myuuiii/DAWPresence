using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

public class BitwigStudio : Daw
{
    private static readonly Regex TitleRegex =
        new(@"^(?<project>.+)\s+-\s+Bitwig Studio(?:\s+[\d\.]+)?(?:\s*-\s*)?$",
            RegexOptions.Compiled);

    [SetsRequiredMembers]
    public BitwigStudio()
    {
        ProcessName = "Bitwig Studio";
        DisplayName = "Bitwig Studio";
        ImageKey = "icon";
        ApplicationId = "";
        WindowTrim = " - Bitwig Studio";
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
