using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DAWPresence.DAWs;

/// <summary>
///     Bitwig Studio with the legacy "{project} - Bitwig Studio" title format. Also accepts the
///     newer "Bitwig Studio - {project}" format, since both builds share a process name.
/// </summary>
public class BitwigStudio : Daw
{
    private static readonly Regex LegacyTitleRegex =
        new(@"^(?<project>.+?)\s+-\s+Bitwig Studio(?:\s+[\d\.]+)?(?:\s+-\s*)?$",
            RegexOptions.Compiled);

    private static readonly BitwigStudioApp NewFormat = new();

    [SetsRequiredMembers]
    public BitwigStudio()
    {
        ProcessName = "BitwigStudioApp";
        DisplayName = "Bitwig Studio";
        ImageKey = "icon";
        ApplicationId = "1524459853417025606";
        WindowTrim = DisplayName + " - ";
        TitleOffset = WindowTrim.Length;
    }

    public override string ParseProjectName(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) return "";
        var match = LegacyTitleRegex.Match(title);
        return match.Success ? match.Groups["project"].Value.Trim() : "";
    }

    public override string GetProjectNameFromProcessWindow() =>
        BitwigWindowScanner.FindProjectName(ProcessName,
            t => ParseProjectName(t) is { Length: > 0 } n ? n : NewFormat.ParseProjectName(t));
}
