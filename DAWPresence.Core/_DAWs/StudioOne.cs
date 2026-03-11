using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class StudioOne : Daw
{
    [SetsRequiredMembers]
    public StudioOne()
    {
        ProcessName = "Studio One";
        DisplayName = "Studio One";
        ImageKey = "studio-one";
        ApplicationId = "1222887221577781278";
        WindowTrim = DisplayName + " - ";
        TitleOffset = 13;
    }

    public override string ParseProjectName(string title)
    {
        return title.StartsWith(WindowTrim) ? title[WindowTrim.Length..] : "";
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";
        return ParseProjectName(process.MainWindowTitle);
    }
}