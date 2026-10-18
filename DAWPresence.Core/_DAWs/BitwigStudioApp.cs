using System.Diagnostics;
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

    public override string GetProjectNameFromProcessWindow() =>
        BitwigWindowScanner.FindProjectName(ProcessName, ParseProjectName);
}

/// <summary>
///     Bitwig spawns several top-level windows (arranger, mixer, editors) and
///     Process.MainWindowTitle may pick one without a project title. Scan every window
///     of every matching process; the first title that parses to a project name wins.
/// </summary>
internal static class BitwigWindowScanner
{
    public static string FindProjectName(string processName, Func<string, string> parse)
    {
        foreach (var process in Process.GetProcessesByName(processName))
        {
            var titles = NativeWindow.GetWindowTitles(process.Id).Append(process.MainWindowTitle);
            foreach (var title in titles)
            {
                var name = parse(title);
                if (!string.IsNullOrEmpty(name)) return name;
            }
        }

        return "";
    }
}
