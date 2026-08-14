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
        ApplicationId = "1537742239164407849";
        WindowTrim = "Cakewalk Sonar - [";
        TitleOffset = 0;
    }

    public override string ParseProjectName(string title)
    {
        if (string.IsNullOrEmpty(title) || !title.StartsWith(WindowTrim) || !title.EndsWith("]"))
            return "";

        var name = title[WindowTrim.Length..^1];
        return name.EndsWith(".cwp", StringComparison.OrdinalIgnoreCase)
            ? name[..^4]
            : name;
    }

    public override string GetProjectNameFromProcessWindow()
    {
        var process = GetProcess();
        if (process is null) return "";

        // Sonar exposes several top-level windows; the one .NET picks as
        // MainWindowTitle is often an empty-titled frame, not the "Sonar"-class
        // window that actually carries the "Cakewalk Sonar - [Project.cwp]" title.
        var title = NativeWindow.FindWindowTitleByClassName(process.Id, "Sonar") ?? process.MainWindowTitle;
        return ParseProjectName(title);
    }
}
