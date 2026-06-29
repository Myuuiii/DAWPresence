using System.Diagnostics.CodeAnalysis;

namespace DAWPresence.DAWs;

public class CakewalkSonar : Daw
{
    [SetsRequiredMembers]
    public CakewalkSonar()
    {
        ProcessName = "Sonar";
        DisplayName = "Cakewalk Sonar";
        ImageKey = "icon";
        ApplicationId = "";
        WindowTrim = "Sonar - ";
        TitleOffset = 0;
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