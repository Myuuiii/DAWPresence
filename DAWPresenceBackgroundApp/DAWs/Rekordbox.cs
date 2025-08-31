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
        WindowTrim = " - " + DisplayName;
        TitleOffset = 12; 
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process == null) return "";
        var title = process.MainWindowTitle;
        return title.Contains(WindowTrim)
            ? TitleRegex().Match(title[..^TitleOffset]).Value
            : "";
    }

    [GeneratedRegex("[^\\[]*")]
    private static partial Regex TitleRegex();
}