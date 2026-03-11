using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using DAWPresence;

namespace DAWPresenceBackgroundApp.DAWs;

public partial class Rekordbox : Daw
{
    [SetsRequiredMembers]
    public Rekordbox()
    {
        ProcessName = "rekordbox";
        DisplayName = "Rekordbox";
        ImageKey = "rekordbox";
        ApplicationId = "1411684427909566506";
        WindowTrim = DisplayName;
        TitleOffset = 12;
        HideDetails = true;
    }

    public override string ParseProjectName(string title) => string.Empty;

    public override string GetProjectNameFromProcessWindow() => string.Empty;

    [GeneratedRegex("[^\\[]*")]
    private static partial Regex TitleRegex();
}