using System.Text.RegularExpressions;
using DAWPresence;

namespace DAWPresenceBackgroundApp.DAWs;

public partial class Rekordbox : Daw
{
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

    public override string GetProjectNameFromProcessWindow() => string.Empty;

    [GeneratedRegex("[^\\[]*")]
    private static partial Regex TitleRegex();
}