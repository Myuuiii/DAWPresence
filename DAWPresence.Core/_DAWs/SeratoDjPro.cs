using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class SeratoDjPro : Daw
{
    [SetsRequiredMembers]
    public SeratoDjPro()
    {
        ProcessName = "Serato DJ Pro";
        DisplayName = "Serato DJ Pro";
        ImageKey = "serrato";
        ApplicationId = "1400470240533545040";
        WindowTrim = string.Empty;
        TitleOffset = 0;
        HideDetails = true;
    }

    public override string ParseProjectName(string title) => string.Empty;

    public override string GetProjectNameFromProcessWindow()
    {
        // Does not have a project name in the window title
        return string.Empty;
    }
}